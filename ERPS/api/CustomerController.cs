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
using EVSTAR.Models.FedEx;
using Twilio.Rest.Trusthub.V1.CustomerProfiles;
using System.Threading.Tasks;
using System.Runtime.InteropServices.ComTypes;
using EVSTAR.DB.NET;

namespace ERPS.api
{
    public class CustomerController : ApiController
    {
        private CustomerHelper customerHelper = new CustomerHelper();

        // GET api/<controller>
        public async Task<List<Customer>> Get()
        {
            List<Customer> customers = new List<Customer>();

            string regCode = DBHelper.GetStringValue(HttpContext.Current.Request.Headers["regCode"]);
            int customerId = DBHelper.GetInt32Value(HttpContext.Current.Request.Headers["customer"]);
            int clientId = DBHelper.GetInt32Value(HttpContext.Current.Request.Headers["clientid"]);
            string email = DBHelper.GetStringValue(HttpContext.Current.Request.Headers["email"]);
            string password = DBHelper.GetStringValue(HttpContext.Current.Request.Headers["password"]);
            string mdn = DBHelper.GetStringValue(HttpContext.Current.Request.Headers["mdn"]);
            string clientCode = DBHelper.GetStringValue(HttpContext.Current.Request.Headers["clientCode"]);

            string errorMsg = string.Empty;
            customers = customerHelper.Select(email, password, mdn, customerId, clientId, clientCode, out errorMsg);
            if (!string.IsNullOrEmpty(errorMsg))
            {
                HttpContext.Current.Response.StatusCode = 500;
            }

            string constr = ConfigurationManager.ConnectionStrings[clientCode].ConnectionString;
            try
            {
                if (customerId > 0 || (!String.IsNullOrEmpty(email) && !String.IsNullOrEmpty(password))
                    || (!String.IsNullOrEmpty(mdn) && clientId > 0))
                {
                    customers = customerHelper.Select(email, password, mdn, customerId, clientId, clientCode, out errorMsg);
                }
                else if (!string.IsNullOrEmpty(regCode))
                {
                    Customer customer = LookupCustomerByCode(regCode);
                    customers.Add(customer);
                }
                else if ((String.IsNullOrEmpty(email) || string.IsNullOrEmpty(password)) && string.IsNullOrEmpty(mdn) && customerId == 0)
                {
                    string first = DBHelper.GetStringValue(HttpContext.Current.Request.Headers["first"]);
                    string last = DBHelper.GetStringValue(HttpContext.Current.Request.Headers["last"]);
                    string zip = DBHelper.GetStringValue(HttpContext.Current.Request.Headers["zip"]);
                    string phone = DBHelper.GetStringValue(HttpContext.Current.Request.Headers["phone"]).Replace("-", "");

                    using (SqlConnection con = new SqlConnection(constr))
                    {
                        con.Open();
                        StringBuilder sql = new StringBuilder();
                        sql.AppendLine("SELECT c.* FROM Customer c WITH(NOLOCK) ");
                        sql.AppendLine("LEFT JOIN Addresses ba WITH(NOLOCK) ON ba.ID = c.BillingAddressID ");
                        sql.AppendLine("LEFT JOIN Addresses sa WITH(NOLOCK) ON sa.ID = c.ShippingAddressID ");
                        sql.AppendLine("LEFT JOIN Addresses ma WITH(NOLOCK) ON ma.ID = c.MailingAddressID ");
                        sql.AppendLine("WHERE c.PrimaryFirstName = @FirstName AND c.PrimaryLastName = @LastName ");
                        sql.AppendLine("AND (LEFT(ba.PostalCode,5) = LEFT(@PostalCode,5) OR ");
                        sql.AppendLine(" LEFT(sa.PostalCode,5) = LEFT(@PostalCode,5) OR LEFT(ma.PostalCode,5) = LEFT(@PostalCode,5) ");
                        sql.AppendLine("OR c.MobileNumber = @PhoneNumber OR c.HomeNumber = @PhoneNumber) ");
                        using (SqlCommand cmd = new SqlCommand(sql.ToString(), con))
                        {
                            cmd.CommandType = CommandType.Text;
                            cmd.Parameters.AddWithValue("@FirstName", first);
                            cmd.Parameters.AddWithValue("@LastName", last);
                            cmd.Parameters.AddWithValue("@PostalCode", zip);
                            cmd.Parameters.AddWithValue("@PhoneNumber", phone);
                            SqlDataReader r = cmd.ExecuteReader();
                            if (r.Read())
                            {
                                Customer customer = new Customer(r);
                                if (customer.BillingAddressID > 0)
                                    customer.BillingAddress = FindAddress(customer.BillingAddressID);
                                if (customer.ShippingAddressID > 0)
                                    customer.ShippingAddress = FindAddress(customer.ShippingAddressID);
                                if (customer.MailingAddressID > 0)
                                    customer.MailingAddress = FindAddress(customer.MailingAddressID);
                                if (customer.ProgramID > 0)
                                    customer.WarrantyProgram = FindProgram(customer.ProgramID);
                                customers.Add(customer);
                            }
                            r.Close();
                        }
                        con.Close();
                    }
                }

                foreach (Customer customer in customers)
                {
                    if (customer.RepairShoprCustomerID == 0 && customer.ID > 0)   // create the customer in RS
                    {
                        // See if the customer is in RepairShopr by email address
                        RSCustomerController rscc = new RSCustomerController();
                        RSCustomer singleCustomer = await rscc.GetCustomerByEmail(customer.Email);
                        if (singleCustomer != null && singleCustomer != null)
                        {
                            customer.RepairShoprCustomerID = singleCustomer.id;
                            UpdateCustomer(customer);
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
                                UpdateCustomer(customer);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                HttpContext.Current.AddError(ex);
            }
            return customers;
        }

        public Customer Get(int id, string clientCode)
        {
            Customer customer = new Customer();
            customer.Result = string.Empty;
            try
            {
                string errorMsg;
                int customerId = id;
                CustomerHelper customerHelper = new CustomerHelper();
                List<Customer> customerList = customerHelper.Select(string.Empty, string.Empty, string.Empty, customerId, 0, clientCode, out errorMsg);
                if (!string.IsNullOrEmpty(errorMsg))
                {
                    HttpContext.Current.AddError(new Exception(errorMsg));
                }
                else
                    customer = customerList[0];
            }
            catch (Exception ex)
            {
                customer.Result = ex.Message;
                HttpContext.Current.AddError(ex);
            }
            return customer;
        }

        private Program FindProgram(int programID)
        {
            Program program = null;

            string constr = ConfigurationManager.ConnectionStrings["REACH"].ConnectionString;
            using (SqlConnection con = new SqlConnection(constr))
            {
                con.Open();
                StringBuilder sql = new StringBuilder();
                sql.AppendLine("SELECT * FROM Program WITH(NOLOCK) ");
                sql.AppendLine("WHERE ID = @ID");
                using (SqlCommand cmd = new SqlCommand(sql.ToString(), con))
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.AddWithValue("@ID", programID);
                    SqlDataReader r = cmd.ExecuteReader();
                    if (r.Read())
                    {
                        program = new Program(r);
                        if (program.ClientID > 0)
                            program.ProgramClient = FindClientByID(program.ClientID);
                    }
                    r.Close();
                }
                con.Close();
            }

            return program;
        }

        private Client FindClientByID(int clientID)
        {
            Client client = null;

            string constr = ConfigurationManager.ConnectionStrings["REACH"].ConnectionString;
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

        private EVSTAR.Models.Address FindAddress(int addressID)
        {
            EVSTAR.Models.Address address = null;

            string constr = ConfigurationManager.ConnectionStrings["REACH"].ConnectionString;
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
        public Customer Post([FromBody] Customer customer)
        {
            try
            {
                string data = DBHelper.GetStringValue(HttpContext.Current.Request.Headers["data"]);
                string clientCode = DBHelper.GetStringValue(HttpContext.Current.Request.Headers["clientCode"]);
                customer = JsonConvert.DeserializeObject<Customer>(data);
                string errorMsg;
                if (customer == null)
                {
                    if (customer.ID == 0)
                        customer = customerHelper.Insert(customer, clientCode, out errorMsg);
                    else
                        customer = customerHelper.Update(customer, clientCode, out errorMsg);

                    if (!string.IsNullOrEmpty(errorMsg))
                    {
                        HttpContext.Current.Response.StatusCode = 500;
                    }
                }
            }
            catch (Exception ex)
            {
                HttpContext.Current.Response.StatusCode = 500;
            }
            return customer;
        }

        private string CreateCustomer(Customer cust)
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

                string constr = ConfigurationManager.ConnectionStrings["REACH"].ConnectionString;
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
        public Customer Put([FromBody] Customer customer)
        {
            string result = string.Empty;
            try
            {
                string data = DBHelper.GetStringValue(HttpContext.Current.Request.Headers["data"]);
                string clientCode = DBHelper.GetStringValue(HttpContext.Current.Request.Headers["clientCode"]);
                customer = JsonConvert.DeserializeObject<Customer>(data);
                string errorMsg;
                if (customer != null)
                {
                    if (customer.ID == 0)
                        customer = customerHelper.Insert(customer, clientCode, out errorMsg);
                    else
                        customer = customerHelper.Update(customer, clientCode, out errorMsg);

                    if (!string.IsNullOrEmpty(errorMsg))
                    {
                        HttpContext.Current.Response.StatusCode = 500;
                    }
                }
            }
            catch (Exception ex)
            {
                HttpContext.Current.Response.StatusCode = 500;
            }
            return customer;
        }

        private Customer LookupCustomerByCode(string code)
        {
            Customer customer = null;

            string constr = ConfigurationManager.ConnectionStrings["REACH"].ConnectionString;
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
                        if (customer.BillingAddressID > 0)
                            customer.BillingAddress = FindAddress(customer.BillingAddressID);
                        if (customer.ShippingAddressID > 0)
                            customer.ShippingAddress = FindAddress(customer.ShippingAddressID);
                        if (customer.MailingAddressID > 0)
                            customer.MailingAddress = FindAddress(customer.MailingAddressID);
                    }
                    r.Close();
                }
                con.Close();
            }

            return customer;
        }

        private string UpdateCustomer(Customer cust)
        {
            string result = String.Empty;

            try
            {
                AddressController addressController = new AddressController();
                cust.BillingAddress = addressController.Post(cust.BillingAddress);
                if (cust.BillingAddress != null)
                {
                    cust.BillingAddressID = cust.BillingAddress.ID;
                }
                cust.ShippingAddress = addressController.Post(cust.ShippingAddress);
                if (cust.ShippingAddress != null)
                {
                    cust.ShippingAddressID = cust.ShippingAddress.ID;
                }
                cust.MailingAddress = addressController.Post(cust.MailingAddress);
                if (cust.MailingAddress != null)
                {
                    cust.MailingAddressID = cust.MailingAddress.ID;
                }

                string constr = ConfigurationManager.ConnectionStrings["REACH"].ConnectionString;
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

        private string RegisterCustomer(int id, string password)
        {
            string result = password;

            try
            {
                string constr = ConfigurationManager.ConnectionStrings["REACH"].ConnectionString;
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

        private Customer LookupCustomer(string email)
        {
            Customer customer = null;

            string constr = ConfigurationManager.ConnectionStrings["REACH"].ConnectionString;
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
                            customer.BillingAddress = FindAddress(customer.BillingAddressID);
                        if (customer.ShippingAddressID > 0)
                            customer.ShippingAddress = FindAddress(customer.ShippingAddressID);
                        if (customer.MailingAddressID > 0)
                            customer.MailingAddress = FindAddress(customer.MailingAddressID);
                    }
                    r.Close();
                }
                con.Close();
            }

            return customer;
        }
    }
}
