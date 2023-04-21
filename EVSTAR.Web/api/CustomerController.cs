using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Web.Http;
using System.Configuration;
using System.Text;
using Newtonsoft.Json;
using EVSTAR.Models;
using EVSTAR.DB.NET;
using EVSTAR.Models.FedEx;
using Twilio.Rest.Trusthub.V1.CustomerProfiles;
using System.Threading.Tasks;

namespace EVSTAR.Web.api
{
    public class CustomerController : ApiController
    {
        // GET api/<controller>
        public async Task<Customer> Get()
        {
            Customer customer = new Customer();
            customer.Result = string.Empty;

            string regCode = HttpContext.Current.Request.Headers["regCode"];
            int customerId = DBHelper.GetInt32Value(HttpContext.Current.Request.Headers["customer"]);
            string email = HttpContext.Current.Request.Headers["email"];
            string password = HttpContext.Current.Request.Headers["password"];
            string login = DBHelper.GetStringValue(HttpContext.Current.Request.Headers["login"]);
            string clientCode = DBHelper.GetStringValue(HttpContext.Current.Request.Headers["client"]); // Client Code
            string phone = DBHelper.GetStringValue(HttpContext.Current.Request.Params["phone"]).Replace("-", "");
            string constr = ConfigurationManager.ConnectionStrings["Techcycle"].ConnectionString;
            try
            {
                CustomerHelper ch = new CustomerHelper();
                string errorMsg = string.Empty;
                if (customerId > 0)
                {
                    customer = ch.Find(string.Empty, string.Empty, string.Empty, customerId, 0, clientCode, out errorMsg);
                    if (customer == null)
                    {
                        customer = new Customer();
                        customer.Result = "NOTFOUND";
                    }
                }
                else if (!string.IsNullOrEmpty(regCode))
                {
                    customer = LookupCustomerByCode(regCode, clientCode);
                }
                else if (String.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
                {
                    string first = DBHelper.GetStringValue(HttpContext.Current.Request.Params["first"]);
                    string last = DBHelper.GetStringValue(HttpContext.Current.Request.Params["last"]);
                    string zip = DBHelper.GetStringValue(HttpContext.Current.Request.Params["zip"]);
                    customer = ch.FindContact(phone, first, last, zip, 0, 0, clientCode, out errorMsg);
                }
                else
                {
                    // Customer login with username and password.

                    password = Encryption.MD5(password);
                    string mdn = string.Empty;
                    if (login.ToUpper() == "MDN")
                    {
                        mdn = email;
                        email = string.Empty;
                    }
                    List<Customer> custs = ch.Select(email, password, mdn, 0, 0, clientCode, out errorMsg);
                    if (custs == null || custs.Count == 0)
                        customer.Result = "NOTFOUND:" + errorMsg;
                    else
                        customer = custs[0];
                }


                if (customer != null && customer.RepairShoprCustomerID == 0 && customer.ID > 0)   // create the customer in RS
                {
                    // See if the customer is in RepairShopr by email address
                    RSCustomerController rscc = new RSCustomerController();
                    RSCustomer singleCustomer = await rscc.GetCustomerByEmail(customer.Email);
                    if (singleCustomer != null && singleCustomer != null)
                    {
                        customer.RepairShoprCustomerID = singleCustomer.id;
                        UpdateCustomer(customer, clientCode);
                    }
                    else
                    {
                        ClientController clientController = new ClientController();
                        Client client = clientController.Get(customer.ClientID);

                        RSCustomer rsCustomer = new RSCustomer()
                        {
                            business_name = customer.CompanyName,
                            firstname = customer.PrimaryFirstName,
                            lastname = customer.PrimaryLastName,
                            fullname = customer.PrimaryName,
                            email = customer.Email,
                            mobile = customer.MobileNumber,
                            referred_by = client != null ? client.Name : "EVSTAR",
                            address = customer.BillingAddress != null ? customer.BillingAddress.Line1 : "",
                            address_2 = customer.BillingAddress != null ? customer.BillingAddress.Line2 : "",
                            city = customer.BillingAddress != null ? customer.BillingAddress.City : "",
                            state = customer.BillingAddress != null ? customer.BillingAddress.State : "",
                            zip = customer.BillingAddress != null ? customer.BillingAddress.PostalCode : "",
                            updated_at = DateTime.Now,
                            created_at = DateTime.Now
                        };

                        rsCustomer = await rscc.Post(rsCustomer);
                        if (rsCustomer != null)
                        {
                            customer.RepairShoprCustomerID = rsCustomer.id;
                            UpdateCustomer(customer, clientCode);
                        }
                    }
                }
                if (customer != null && customer.Result != "NOTFOUND")
                {
                    ClaimHelper clh = new ClaimHelper();
                    List<Claim> customerClaims = clh.Select(0, customer.ID, 0, clientCode, out errorMsg);
                    if (customerClaims != null && customerClaims.Count > 0)
                    {
                        customer.NumClaimsLast12Months = 0;
                        foreach (Claim claim in customerClaims)
                        {
                            // Only count claims that have one or more History records.
                            if (claim.StatusHistory != null)
                            {
                                DateTime startDate = DateTime.Now.AddMonths(-12);
                                ClaimStatusHistory hist = claim.StatusHistory.OrderByDescending(x => x.StatusDate).FirstOrDefault();
                                if (hist != null && claim.OpenDate > startDate)
                                {
                                    customer.NumClaimsLast12Months++;
                                }
                            }
                        }
                    }

                }
            }
            catch (Exception ex)
            {
                customer.Result = ex.Message;
            }
            return customer;
        }

        public Customer Get(int id)
        {
            Customer customer = new Customer();
            customer.Result = string.Empty;
            CustomerHelper ch = new CustomerHelper();
            string errorMsg = string.Empty;
            int customerId = id;
            string clientCode = DBHelper.GetStringValue(HttpContext.Current.Request.Headers["client"]); // Client Code
            string constr = ConfigurationManager.ConnectionStrings["Techcycle"].ConnectionString;
            try
            {
                if (customerId > 0)
                {
                    List<Customer> customers = ch.Select(string.Empty, string.Empty, string.Empty, id, 0, clientCode, out errorMsg); 
                    if (customers.Count > 0)
                    {
                        customer = customers[0];
                    }
                }
                else
                    customer.Result = "NOTFOUND";
            }
            catch (Exception ex)
            {
                customer.Result = ex.Message;
            }
            return customer;
        }

        private Client FindClientByID(int clientID, string clientCode)
        {
            Client client = null;

            string constr = ConfigurationManager.ConnectionStrings[clientCode].ConnectionString;
            using (SqlConnection con = new SqlConnection(constr))
            {
                con.Open();
                StringBuilder sql = new StringBuilder();
                sql.AppendLine("SELECT * FROM Client WITH(NOLOCK) ");
                sql.AppendLine("WHERE ID = @ID");
                using (SqlCommand cmd = new SqlCommand(sql.ToString(), con))
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.AddWithValue("@ID", clientID);
                    SqlDataReader r = cmd.ExecuteReader();
                    if (r.Read())
                    {
                        client = new Client(r);
                    }
                    r.Close();
                }
                con.Close();
            }

            return client;
        }

        private EVSTAR.Models.Address FindAddress(int addressID, string clientCode)
        {
            EVSTAR.Models.Address address = null;

            string constr = ConfigurationManager.ConnectionStrings["Techcycle"].ConnectionString;
            using (SqlConnection con = new SqlConnection(constr))
            {
                con.Open();
                StringBuilder sql = new StringBuilder();
                sql.AppendLine("SELECT * FROM Addresses WITH(NOLOCK) ");
                sql.AppendLine("WHERE ID = @ID");
                using (SqlCommand cmd = new SqlCommand(sql.ToString(), con))
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.AddWithValue("@ID", addressID);
                    SqlDataReader r = cmd.ExecuteReader();
                    if (r.Read())
                    {
                        address = new EVSTAR.Models.Address(r);
                    }
                    r.Close();
                }
                con.Close();
            }

            return address;
        }

        // POST api/<controller>
        public async Task<string> Post([FromBody] Customer customer)
        {
            string result = string.Empty;
            string clientCode = DBHelper.GetStringValue(HttpContext.Current.Request.Headers["client"]); // Client Code
            string function = DBHelper.GetStringValue(HttpContext.Current.Request.Params["function"]);

            try
            {

                if (function.ToUpper() == "REGISTER" || function.ToUpper() == "RESET")
                {
                    string email = HttpContext.Current.Request.Headers["email"];
                    string password = HttpContext.Current.Request.Headers["password"];
                    string confirm = HttpContext.Current.Request.Headers["confirm"];

                    Customer cust = LookupCustomer(email, clientCode);
                    if (cust == null)
                    {
                        result = "NOTFOUND";
                    }
                    else
                    {
                        if (!string.IsNullOrEmpty(cust.Password) && function.ToUpper() == "REGISTER")
                        {
                            result = "ALREADYREGISTERED";
                        }
                        else
                        {
                            result = RegisterCustomer(cust.ID, Encryption.MD5(password), clientCode);
                        }
                    }
                }
                else if (function.ToUpper() == "CREATE")
                {
                    if (customer == null)
                    {
                        result = "INVALID DATA";
                    }
                    else
                    {
                        result = CreateCustomer(customer, clientCode);
                        ClientController clientController = new ClientController();
                        Client client = clientController.Get(customer.ClientID);

                        RSCustomer rsCustomer = new RSCustomer()
                        {
                            business_name = customer.CompanyName,
                            firstname = customer.PrimaryFirstName,
                            lastname = customer.PrimaryLastName,
                            fullname = customer.PrimaryName,
                            email = customer.Email,
                            mobile = customer.MobileNumber,
                            referred_by = client != null ? client.Name : "EVSTAR",
                            address = customer.BillingAddress != null ? customer.BillingAddress.Line1 : "",
                            address_2 = customer.BillingAddress != null ? customer.BillingAddress.Line2 : "",
                            city = customer.BillingAddress != null ? customer.BillingAddress.City : "",
                            state = customer.BillingAddress != null ? customer.BillingAddress.State : "",
                            zip = customer.BillingAddress != null ? customer.BillingAddress.PostalCode : "",
                            updated_at = DateTime.Now,
                            created_at = DateTime.Now
                        };

                        RSCustomerController rscc = new RSCustomerController();
                        rsCustomer = await rscc.Post(rsCustomer);
                        if (rsCustomer != null)
                        {
                            if (rsCustomer.notes != null && rsCustomer.notes.ToLower().Contains("message\":"))
                                result = "INVALID DATA: " + rsCustomer.notes;
                            else
                            {
                                customer.RepairShoprCustomerID = rsCustomer.id;
                                result = UpdateCustomer(customer, clientCode);
                            }
                        }
                    }
                }
                else if (function.ToUpper() == "UPDATE")
                {
                    if (customer == null)
                    {
                        result = "INVALID DATA";
                    }
                    else
                    {
                        if (customer.RepairShoprCustomerID == 0)
                        {
                            ClientController clientController = new ClientController();
                            Client client = clientController.Get(customer.ClientID);

                            RSCustomer rsCustomer = new RSCustomer()
                            {
                                business_name = customer.CompanyName,
                                firstname = customer.PrimaryFirstName,
                                lastname = customer.PrimaryLastName,
                                fullname = customer.PrimaryName,
                                email = customer.Email,
                                mobile = customer.MobileNumber,
                                referred_by = client != null ? client.Name : "EVSTAR",
                                address = customer.BillingAddress != null ? customer.BillingAddress.Line1 : "",
                                address_2 = customer.BillingAddress != null ? customer.BillingAddress.Line2 : "",
                                city = customer.BillingAddress != null ? customer.BillingAddress.City : "",
                                state = customer.BillingAddress != null ? customer.BillingAddress.State : "",
                                zip = customer.BillingAddress != null ? customer.BillingAddress.PostalCode : "",
                                updated_at = DateTime.Now,
                                created_at = DateTime.Now
                            };

                            RSCustomerController rscc = new RSCustomerController();
                            rsCustomer = await rscc.Post(rsCustomer);
                            if (rsCustomer != null)
                            {
                                customer.RepairShoprCustomerID = rsCustomer.id;
                            }
                        }
                        result = UpdateCustomer(customer, clientCode);
                    }
                }
                else
                {
                    string email = HttpContext.Current.Request.Headers["email"];
                    string password = HttpContext.Current.Request.Headers["password"];
                    string confirm = HttpContext.Current.Request.Headers["confirm"];

                    Customer cust = LookupCustomer(email, clientCode);
                    if (cust == null)
                    {
                        result = "NOTFOUND";
                    }
                    else
                    {
                        if (!string.IsNullOrEmpty(cust.Password))
                        {
                            if (cust.Password != Encryption.MD5(password))
                            {
                                return "INVALID";
                            }
                            else
                            {
                                if (cust.RepairShoprCustomerID == 0)   // create the customer in RS
                                {
                                    ClientController clientController = new ClientController();
                                    Client client = clientController.Get(cust.ClientID);

                                    RSCustomer rsCustomer = new RSCustomer()
                                    {
                                        business_name = cust.CompanyName,
                                        firstname = cust.PrimaryFirstName,
                                        lastname = cust.PrimaryLastName,
                                        fullname = cust.PrimaryName,
                                        email = cust.Email,
                                        mobile = cust.MobileNumber,
                                        referred_by = client != null ? client.Name : "EVSTAR",
                                        address = cust.BillingAddress != null ? cust.BillingAddress.Line1 : "",
                                        address_2 = cust.BillingAddress != null ? cust.BillingAddress.Line2 : "",
                                        city = cust.BillingAddress != null ? cust.BillingAddress.City : "",
                                        state = cust.BillingAddress != null ? cust.BillingAddress.State : "",
                                        zip = cust.BillingAddress != null ? cust.BillingAddress.PostalCode : ""
                                    };

                                    RSCustomerController rscc = new RSCustomerController();
                                    rsCustomer = await rscc.Post(rsCustomer);
                                    if (rsCustomer != null)
                                    {
                                        cust.RepairShoprCustomerID = rsCustomer.id;
                                        result = UpdateCustomer(cust, clientCode);
                                    }

                                }
                            }
                        }
                        else
                        {
                            return "INVALID";
                        }
                    }
                }
                return result;
            }
            catch (Exception ex)
            {
                result = ex.Message;
            }
            return result;
        }

        private string CreateCustomer(Customer cust, string clientCode)
        {
            string result = String.Empty;

            try
            {
                AddressController addressController = new AddressController();
                cust.BillingAddress = addressController.Post(cust.BillingAddress);
                cust.BillingAddressID = cust.BillingAddress.ID;
                cust.ShippingAddress = addressController.Post(cust.ShippingAddress);
                cust.ShippingAddressID = cust.ShippingAddress.ID;
                cust.MailingAddress = addressController.Post(cust.MailingAddress);
                cust.MailingAddressID = cust.MailingAddress.ID;

                string constr = ConfigurationManager.ConnectionStrings["Techcycle"].ConnectionString;
                using (SqlConnection con = new SqlConnection(constr))
                {
                    con.Open();
                    StringBuilder sql = new StringBuilder();
                    sql.AppendLine("INSERT Customer (PrimaryFirstName, PrimaryLastName, PrimaryName, Authentication, StatusCode, Email, HomeNumber, MobileNumber, ");
                    sql.AppendLine("ClientID, BillingAddressID, ShippingAddressID, MailingAddressID, LastUpdated) VALUES ");
                    sql.AppendLine("(@PrimaryFirstName, @PrimaryLastName, @PrimaryName, @Password, @StatusCode, @Email, @HomeNumber, @MobileNumber, ");
                    sql.AppendLine("@ClientID, @BillingAddressID, @ShippingAddressID, @MailingAddressID, GETDATE()); SELECT SCOPE_IDENTITY(); ");

                    using (SqlCommand cmd = new SqlCommand(sql.ToString(), con))
                    {
                        cmd.CommandType = CommandType.Text;
                        cmd.Parameters.AddWithValue("@PrimaryFirstName", cust.PrimaryFirstName);
                        cmd.Parameters.AddWithValue("@PrimaryLastName", cust.PrimaryLastName);
                        cmd.Parameters.AddWithValue("@PrimaryName", cust.PrimaryName);
                        cmd.Parameters.AddWithValue("@Email", cust.Email);
                        cmd.Parameters.AddWithValue("@ClientID", cust.ClientID);
                        cmd.Parameters.AddWithValue("@BillingAddressID", cust.BillingAddressID);
                        cmd.Parameters.AddWithValue("@ShippingAddressID", cust.ShippingAddressID);
                        cmd.Parameters.AddWithValue("@MailingAddressID", cust.MailingAddressID);
                        cmd.Parameters.AddWithValue("@MobileNumber", cust.MobileNumber.Replace("-", ""));
                        cmd.Parameters.AddWithValue("@HomeNumber", cust.HomeNumber.Replace("-", ""));
                        cmd.Parameters.AddWithValue("@Password", cust.Password);
                        cmd.Parameters.AddWithValue("@StatusCode", cust.StatusCode);
                        cust.ID = DBHelper.GetInt32Value(cmd.ExecuteScalar());
                        result = JsonConvert.SerializeObject(cust);
                    }
                    con.Close();
                }
            }
            catch (Exception ex)
            {
                result = String.Format("ERROR: {0}", ex.Message);
            }
            return result;
        }

        // PUT api/<controller>
        public string Put([FromBody] string value)
        {
            string result = string.Empty;
            try
            {
                int id = DBHelper.GetInt32Value(HttpContext.Current.Request.Headers["id"]);
                string code = HttpContext.Current.Request.Headers["code"];
                string name = HttpContext.Current.Request.Headers["name"];
                string address1 = HttpContext.Current.Request.Headers["address1"];
                string address2 = HttpContext.Current.Request.Headers["address2"];
                string city = HttpContext.Current.Request.Headers["city"];
                string state = HttpContext.Current.Request.Headers["state"];
                string postalCode = HttpContext.Current.Request.Headers["postalCode"];
                string phone = HttpContext.Current.Request.Headers["phone"];
                string email = HttpContext.Current.Request.Headers["email"];
                string password = HttpContext.Current.Request.Headers["password"];
                string confirm = HttpContext.Current.Request.Headers["confirm"];
                string client = HttpContext.Current.Request.Headers["client"];
                string clientCode = HttpContext.Current.Request.Headers["clientCode"];

                Customer cust = LookupCustomerByCode(code, clientCode);
                if (cust == null)
                {
                    result = "NOTFOUND";
                }
                else if (cust != null && cust.ID != id)
                {
                    result = "MISMATCH";
                }
                else
                {
                    cust.StatusCode = "Active";
                    cust.Email = email;
                    cust.MobileNumber = phone;
                    cust.Password = Encryption.MD5(password);
                    cust.PrimaryName = name;
                    string[] parts = name.Split(' ');
                    cust.PrimaryFirstName = parts.Length > 0 ? parts[0] : string.Empty;
                    cust.PrimaryLastName = parts.Length > 1 ? parts[1] : string.Empty;
                    cust.SequenceNumber = "1";
                    cust.ClientID = DBHelper.GetInt32Value(client);
                    cust.BillingAddress = new EVSTAR.Models.Address()
                    {
                        City = city,
                        State = state,
                        PostalCode = postalCode,
                        Country = "US",
                        Line1 = address1,
                        Line2 = address2
                    };
                    cust.ShippingAddress = new EVSTAR.Models.Address()
                    {
                        City = city,
                        State = state,
                        PostalCode = postalCode,
                        Country = "US",
                        Line1 = address1,
                        Line2 = address2
                    };
                    cust.MailingAddress = new EVSTAR.Models.Address()
                    {
                        City = city,
                        State = state,
                        PostalCode = postalCode,
                        Country = "US",
                        Line1 = address1,
                        Line2 = address2
                    };

                    result = UpdateCustomer(cust, clientCode);
                }
                return result;
            }
            catch (Exception ex)
            {
                result = ex.Message;
            }
            return result;
        }

        private Customer LookupCustomerByCode(string code, string clientCode)
        {
            Customer customer = null;

            string constr = ConfigurationManager.ConnectionStrings["Techcycle"].ConnectionString;
            using (SqlConnection con = new SqlConnection(constr))
            {
                con.Open();
                StringBuilder sql = new StringBuilder();
                sql.AppendLine("SELECT c.* FROM Customer c WITH(NOLOCK) ");
                sql.AppendLine("WHERE c.SubscriptionID = @Code");
                using (SqlCommand cmd = new SqlCommand(sql.ToString(), con))
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.AddWithValue("@Code", code);
                    SqlDataReader r = cmd.ExecuteReader();
                    if (r.Read())
                    {
                        customer = new Customer(r);
                        if (customer != null)
                        if (customer.BillingAddressID > 0)
                            customer.BillingAddress = FindAddress(customer.BillingAddressID, clientCode);
                        if (customer.ShippingAddressID > 0)
                            customer.ShippingAddress = FindAddress(customer.ShippingAddressID, clientCode);
                        if (customer.MailingAddressID > 0)
                            customer.MailingAddress = FindAddress(customer.MailingAddressID, clientCode);
                    }
                    r.Close();
                }
                con.Close();
            }

            return customer;
        }

        private string UpdateCustomer(Customer cust, string clientCode)
        {
            string result = String.Empty;

            try
            {
                AddressController addressController = new AddressController();
                cust.BillingAddress = addressController.Post(cust.BillingAddress);
                cust.BillingAddressID = cust.BillingAddress.ID;
                cust.ShippingAddress = addressController.Post(cust.ShippingAddress);
                cust.ShippingAddressID = cust.ShippingAddress.ID;
                cust.MailingAddress = addressController.Post(cust.MailingAddress);
                cust.MailingAddressID = cust.MailingAddress.ID;

                string constr = ConfigurationManager.ConnectionStrings[clientCode].ConnectionString;
                using (SqlConnection con = new SqlConnection(constr))
                {
                    con.Open();
                    StringBuilder sql = new StringBuilder();
                    sql.AppendLine("UPDATE Customer SET PrimaryFirstName=@FirstName, PrimaryLastName=@LastName, ");
                    sql.AppendLine("PrimaryName=@Name, Authentication=@Password, StatusCode=@StatusCode, Email=@Email, ");
                    sql.AppendLine("ClientID=@ClientID, BillingAddressID=@BillingAddressID, ShippingAddressID=@ShippingAddressID, ");
                    sql.AppendLine("MailingAddressID=@MailingAddressID, MobileNumber=@MobileNumber, HomeNumber=@HomeNumber, RepairShoprCustomerID=@RepairShoprCustomerID, ");
                    sql.AppendLine("LastUpdated=GETDATE() WHERE ID=@id");

                    using (SqlCommand cmd = new SqlCommand(sql.ToString(), con))
                    {
                        cmd.CommandType = CommandType.Text;
                        cmd.Parameters.AddWithValue("@ID", cust.ID);
                        cmd.Parameters.AddWithValue("@FirstName", cust.PrimaryFirstName);
                        cmd.Parameters.AddWithValue("@LastName", cust.PrimaryLastName);
                        cmd.Parameters.AddWithValue("@Name", cust.PrimaryName);
                        cmd.Parameters.AddWithValue("@Email", cust.Email);
                        cmd.Parameters.AddWithValue("@ClientID", cust.ClientID);
                        cmd.Parameters.AddWithValue("@BillingAddressID", cust.BillingAddressID);
                        cmd.Parameters.AddWithValue("@ShippingAddressID", cust.ShippingAddressID);
                        cmd.Parameters.AddWithValue("@MailingAddressID", cust.MailingAddressID);
                        cmd.Parameters.AddWithValue("@MobileNumber", cust.MobileNumber.Replace("-", ""));
                        cmd.Parameters.AddWithValue("@HomeNumber", cust.HomeNumber.Replace("-", ""));
                        cmd.Parameters.AddWithValue("@Password", cust.Password);
                        cmd.Parameters.AddWithValue("@StatusCode", cust.StatusCode);
                        cmd.Parameters.AddWithValue("@RepairShoprCustomerID", cust.RepairShoprCustomerID);
                        cmd.ExecuteNonQuery();
                    }
                    con.Close();
                    result = JsonConvert.SerializeObject(cust);
                }
            }
            catch (Exception ex)
            {
                result = String.Format("ERROR: {0}", ex.Message);
            }
            return result;
        }

        private string RegisterCustomer(int id, string password, string clientCode)
        {
            string result = password;

            try
            {
                string constr = ConfigurationManager.ConnectionStrings["clientCode"].ConnectionString;
                using (SqlConnection con = new SqlConnection(constr))
                {
                    con.Open();
                    StringBuilder sql = new StringBuilder();
                    sql.AppendLine("UPDATE Customer SET Authentication=@Password, StatusCode='Active' WHERE ID=@id");
                    using (SqlCommand cmd = new SqlCommand(sql.ToString(), con))
                    {
                        cmd.CommandType = CommandType.Text;
                        cmd.Parameters.AddWithValue("@ID", id);
                        cmd.Parameters.AddWithValue("@Password", password);
                        cmd.ExecuteNonQuery();
                    }
                    con.Close();
                }
            }
            catch (Exception ex)
            {
                result = ex.Message;
            }
            return result;
        }

        private Customer LookupCustomer(string email, string clientCode)
        {
            Customer customer = null;

            string constr = ConfigurationManager.ConnectionStrings["clientCode"].ConnectionString;
            using (SqlConnection con = new SqlConnection(constr))
            {
                con.Open();
                StringBuilder sql = new StringBuilder();
                sql.AppendLine("SELECT c.* FROM Customer c WITH(NOLOCK) ");
                //sql.AppendLine("LEFT JOIN Addresses ba WITH(NOLOCK) ON ba.ID = c.BillingAddressID ");
                //sql.AppendLine("LEFT JOIN Addresses sa WITH(NOLOCK) ON sa.ID = c.ShippingAddressID ");
                //sql.AppendLine("LEFT JOIN Addresses ma WITH(NOLOCK) ON ma.ID = c.MailingAddressID ");
                sql.AppendLine("WHERE c.Email = @Email");
                using (SqlCommand cmd = new SqlCommand(sql.ToString(), con))
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.AddWithValue("@Email", email);
                    SqlDataReader r = cmd.ExecuteReader();
                    if (r.Read())
                    {
                        customer = new Customer(r);
                        if (customer.BillingAddressID > 0)
                            customer.BillingAddress = FindAddress(customer.BillingAddressID, clientCode);
                        if (customer.ShippingAddressID > 0)
                            customer.ShippingAddress = FindAddress(customer.ShippingAddressID, clientCode);
                        if (customer.MailingAddressID > 0)
                            customer.MailingAddress = FindAddress(customer.MailingAddressID, clientCode);
                    }
                    r.Close();
                }
                con.Close();
            }

            return customer;
        }
    }
}
