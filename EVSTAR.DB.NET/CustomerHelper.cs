using System;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EVSTAR.Models;
using System.Collections.Generic;

namespace EVSTAR.DB.NET
{
    public class CustomerHelper
    {
        public CustomerLookup AuthenticateCustomer(string email, string mdn, string password, out string errorMsg)
        {
            CustomerLookup customer = new CustomerLookup();
            errorMsg = string.Empty;
            try
            {
                string constr = ConfigurationManager.ConnectionStrings["Techcycle"].ConnectionString;
                using (SqlConnection con = new SqlConnection(constr))
                {
                    con.Open();
                    StringBuilder sql = new StringBuilder();
                    sql.AppendLine("SELECT * FROM CustomerMaster WITH(NOLOCK) ");

                    if (!string.IsNullOrEmpty(email))
                        sql.AppendLine("WHERE Email=@Email ");

                    if (!string.IsNullOrEmpty(mdn))
                        sql.AppendLine("WHERE MobileNumber=@MDN ");

                    if (!string.IsNullOrEmpty(password))
                        sql.AppendLine("AND Authentication=@Authentication");

                    sql.AppendLine("ORDER BY ID DESC");

                    using (SqlCommand cmd = new SqlCommand(sql.ToString(), con))
                    {
                        cmd.CommandType = CommandType.Text;

                        if (!string.IsNullOrEmpty(email))
                            cmd.Parameters.AddWithValue("@Email", email);

                        if (!string.IsNullOrEmpty(password))
                            cmd.Parameters.AddWithValue("@Authentication", password);

                        if (!string.IsNullOrEmpty(mdn))
                            cmd.Parameters.AddWithValue("@MDN", mdn);

                        AddressHelper addressHelper = new AddressHelper();
                        ProgramHelper programHelper = new ProgramHelper();
                        SqlDataReader r = cmd.ExecuteReader();
                        if (r.Read())
                        {
                            customer = new CustomerLookup(r);
                        }
                        r.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                errorMsg = String.Format("ERROR: {0}\r\n{1}", ex.Message, ex.StackTrace);
            }
            return customer;
        }

        public List<Customer> Select(string email, string password, string mdn, int id, int clientID, string clientCode, out string errorMsg)
        {
            List<Customer> result = new List<Customer>();
            errorMsg = string.Empty;
            try
            {
                string constr = ConfigurationManager.ConnectionStrings[clientCode].ConnectionString;
                using (SqlConnection con = new SqlConnection(constr))
                {
                    con.Open();
                    StringBuilder sql = new StringBuilder();
                    sql.AppendLine("SELECT * FROM Customer WITH(NOLOCK) ");
                    if (id > 0)
                        sql.AppendLine("WHERE ID=@ID ");

                    if (!string.IsNullOrEmpty(email))
                        sql.AppendLine("WHERE Email=@Email ");

                    if (!string.IsNullOrEmpty(mdn))
                        sql.AppendLine("WHERE MobileNumber=@MDN ");

                    if (!string.IsNullOrEmpty(password))
                        sql.AppendLine("AND Authentication=@Authentication");

                    //if (clientID > 0)
                    //    sql.AppendLine("AND ClientID=@ClientID ");

                    sql.AppendLine("ORDER BY ID DESC");

                    using (SqlCommand cmd = new SqlCommand(sql.ToString(), con))
                    {
                        cmd.CommandType = CommandType.Text;
                        if (id > 0)
                            cmd.Parameters.AddWithValue("@ID", id);

                        if (!string.IsNullOrEmpty(email))
                            cmd.Parameters.AddWithValue("@Email", email);

                        if (!string.IsNullOrEmpty(password))
                            cmd.Parameters.AddWithValue("@Authentication", password);

                        if (!string.IsNullOrEmpty(mdn))
                            cmd.Parameters.AddWithValue("@MDN", mdn);

                        //if (clientID > 0)
                        //    cmd.Parameters.AddWithValue("@ClientID", clientID);

                        AddressHelper addressHelper = new AddressHelper();
                        ProgramHelper programHelper = new ProgramHelper();
                        SqlDataReader r = cmd.ExecuteReader();
                        while (r.Read())
                        {
                            Customer customer = new Customer(r);
                            if (customer.BillingAddressID > 0)
                            {
                                List<Address> addresses = addressHelper.Select(customer.BillingAddressID, out errorMsg);
                                if (addresses != null && addresses.Count > 0)
                                    customer.BillingAddress = addresses[0];
                            }
                            if (customer.ShippingAddressID > 0)
                            {
                                List<Address> addresses = addressHelper.Select(customer.ShippingAddressID, out errorMsg);
                                if (addresses != null && addresses.Count > 0)
                                    customer.ShippingAddress = addresses[0];
                            }
                            if (customer.MailingAddressID > 0)
                            {
                                List<Address> addresses = addressHelper.Select(customer.MailingAddressID, out errorMsg);
                                if (addresses != null && addresses.Count > 0)
                                    customer.MailingAddress = addresses[0];
                            }
                            if (customer.ProgramID > 0)
                            {
                                List<Program> programs = programHelper.Select(customer.ProgramID, clientCode, out errorMsg);
                                if (programs != null && programs.Count > 0)
                                    customer.WarrantyProgram = programs[0];
                            }
                            result.Add(customer);
                        }
                        r.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                errorMsg = String.Format("ERROR: {0}\r\n{1}", ex.Message, ex.StackTrace);
            }
            return result;
        }

        public Customer Find(string email, string mdn, string lastName, int id, int clientID, string clientCode, out string errorMsg)
        {
            Customer result = null;
            errorMsg = string.Empty;
            try
            {
                string constr = ConfigurationManager.ConnectionStrings[clientCode].ConnectionString;
                using (SqlConnection con = new SqlConnection(constr))
                {
                    con.Open();
                    StringBuilder sql = new StringBuilder();
                    sql.AppendLine("SELECT * FROM Customer WITH(NOLOCK) ");
                    if (id > 0)
                        sql.AppendLine("WHERE ID=@ID ");

                    if (string.IsNullOrEmpty(email) && !string.IsNullOrEmpty(mdn))
                        sql.AppendLine("WHERE MobileNumber=@MDN ");

                    if (!string.IsNullOrEmpty(email) && string.IsNullOrEmpty(mdn))
                        sql.AppendLine("WHERE Email=@Email ");

                    if (!string.IsNullOrEmpty(email) && !string.IsNullOrEmpty(mdn))
                        sql.AppendLine("WHERE (Email=@Email OR MobileNumber=@MDN) ");

                    if (!string.IsNullOrEmpty(lastName))
                        sql.AppendLine("AND PrimaryLastName=@PrimaryLastName");

                    if (clientID > 0)
                        sql.AppendLine("AND ClientID=@ClientID ");

                    sql.AppendLine("ORDER BY ID DESC");

                    using (SqlCommand cmd = new SqlCommand(sql.ToString(), con))
                    {
                        cmd.CommandType = CommandType.Text;
                        if (id > 0)
                            cmd.Parameters.AddWithValue("@ID", id);

                        if (!string.IsNullOrEmpty(email))
                            cmd.Parameters.AddWithValue("@Email", email);

                        if (!string.IsNullOrEmpty(mdn))
                            cmd.Parameters.AddWithValue("@MDN", mdn);

                        if (!string.IsNullOrEmpty(lastName))
                            cmd.Parameters.AddWithValue("@PrimaryLastName", lastName);

                        if (clientID > 0)
                            cmd.Parameters.AddWithValue("@ClientID", clientID);

                        AddressHelper addressHelper = new AddressHelper();
                        ProgramHelper programHelper = new ProgramHelper();
                        SqlDataReader r = cmd.ExecuteReader();
                        while (r.Read())
                        {
                            result = new Customer(r);
                            if (result.BillingAddressID > 0)
                            {
                                List<Address> addresses = addressHelper.Select(result.BillingAddressID, out errorMsg);
                                if (addresses != null && addresses.Count > 0)
                                    result.BillingAddress = addresses[0];
                            }
                            if (result.ShippingAddressID > 0)
                            {
                                List<Address> addresses = addressHelper.Select(result.ShippingAddressID, out errorMsg);
                                if (addresses != null && addresses.Count > 0)
                                    result.ShippingAddress = addresses[0];
                            }
                            if (result.MailingAddressID > 0)
                            {
                                List<Address> addresses = addressHelper.Select(result.MailingAddressID, out errorMsg);
                                if (addresses != null && addresses.Count > 0)
                                    result.MailingAddress = addresses[0];
                            }
                            if (result.ProgramID > 0)
                            {
                                List<Program> programs = programHelper.Select(result.ProgramID, clientCode, out errorMsg);
                                if (programs != null && programs.Count > 0)
                                    result.WarrantyProgram = programs[0];
                            }
                        }
                        r.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                errorMsg = String.Format("ERROR: {0}\r\n{1}", ex.Message, ex.StackTrace);
            }
            return result;
        }

        public Customer FindContact(string phone, string first, string last, string zip, 
            int id, int clientID, string clientCode, out string errorMsg)
        {
            Customer customer = null;
            errorMsg = string.Empty;
            try
            {
                string constr = ConfigurationManager.ConnectionStrings[clientCode].ConnectionString;

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
                            customer = new Customer(r);
                            if (customer.BillingAddressID > 0)
                                customer.BillingAddress = FindAddress(customer.BillingAddressID, clientCode);
                            if (customer.ShippingAddressID > 0)
                                customer.ShippingAddress = FindAddress(customer.ShippingAddressID, clientCode);
                            if (customer.MailingAddressID > 0)
                                customer.MailingAddress = FindAddress(customer.MailingAddressID, clientCode);
                            if (customer.ProgramID > 0)
                            {
                                ProgramHelper ph = new ProgramHelper();
                                List<Program> programs = ph.Select(customer.ProgramID, clientCode, out errorMsg);
                                if (programs != null && programs.Count > 0)
                                    customer.WarrantyProgram = programs[0];
                            }
                        }
                        else
                            customer.Result = "NOTFOUND";
                        r.Close();
                    }
                    con.Close();
                }
            }
            catch (Exception ex)
            {
                errorMsg = String.Format("ERROR: {0}\r\n{1}", ex.Message, ex.StackTrace);
            }
            return customer;
        }

        public Customer Insert(Customer customer, out string errorMsg)
        {
            errorMsg = string.Empty;
            try
            {
                if (customer != null)
                {
                    string constr = ConfigurationManager.ConnectionStrings["Techcycle"].ConnectionString;
                    using (SqlConnection con = new SqlConnection(constr))
                    {
                        con.Open();
                        StringBuilder sql = new StringBuilder();
                        sql.AppendLine("INSERT INTO Customer ");
                        sql.AppendLine("(ClientID, MobileNumber, HomeNumber, PrimaryName, AuthorizedName, BillingAddressID, ShippingAddressID, MailingAddressID, ");
                        sql.AppendLine("Email, PrimaryFirstName, PrimaryLastName, AccountNumber, SequenceNumber, SubscriptionID, StatusCode, EnrollmentDate, ");
                        sql.AppendLine("CompanyName, Authentication, DateSubscriptionEmailSent, ProgramID, RepairShoprCustomerID, LastUpdated, DateAgreedToWaiver, ParentID)");
                        sql.AppendLine("VALUES ");
                        sql.AppendLine("(@ClientID, @MobileNumber, @HomeNumber, @PrimaryName, @AuthorizedName, @BillingAddressID, @ShippingAddressID, @MailingAddressID, ");
                        sql.AppendLine("@Email, @PrimaryFirstName, @PrimaryLastName, @AccountNumber, @SequenceNumber, @SubscriptionID, @StatusCode, @EnrollmentDate, ");
                        sql.AppendLine("@CompanyName, @Password, @DateSubscriptionEmailSent, @ProgramID, @RepairShoprCustomerID, @LastUpdated, @DateAgreedToWaiver, @ParentID); ");
                        sql.AppendLine("SELECT SCOPE_IDENTITY() ");
                        using (SqlCommand cmd = new SqlCommand(sql.ToString(), con))
                        {
                            cmd.CommandType = CommandType.Text;
                            cmd.Parameters.AddWithValue("@ClientID", customer.ClientID);
                            cmd.Parameters.AddWithValue("@MobileNumber", customer.MobileNumber);
                            cmd.Parameters.AddWithValue("@HomeNumber", customer.HomeNumber);
                            cmd.Parameters.AddWithValue("@PrimaryName", customer.PrimaryName);
                            cmd.Parameters.AddWithValue("@AuthorizedName", customer.AuthorizedName);
                            cmd.Parameters.AddWithValue("@BillingAddressID", customer.BillingAddressID);
                            cmd.Parameters.AddWithValue("@ShippingAddressID", customer.ShippingAddressID);
                            cmd.Parameters.AddWithValue("@MailingAddressID", customer.MailingAddressID);
                            cmd.Parameters.AddWithValue("@Email", customer.Email);
                            cmd.Parameters.AddWithValue("@PrimaryFirstName", customer.PrimaryFirstName);
                            cmd.Parameters.AddWithValue("@PrimaryLastName", customer.PrimaryLastName);
                            cmd.Parameters.AddWithValue("@AccountNumber", customer.AccountNumber);
                            cmd.Parameters.AddWithValue("@SequenceNumber", customer.SequenceNumber);
                            cmd.Parameters.AddWithValue("@SubscriptionID", customer.SubscriptionID);
                            cmd.Parameters.AddWithValue("@StatusCode", customer.StatusCode);
                            cmd.Parameters.AddWithValue("@EnrollmentDate", customer.EnrollmentDate);
                            cmd.Parameters.AddWithValue("@CompanyName", customer.CompanyName);
                            cmd.Parameters.AddWithValue("@Password", customer.Password);
                            cmd.Parameters.AddWithValue("@DateSubscriptionEmailSent", DBNull.Value);
                            cmd.Parameters.AddWithValue("@ProgramID", customer.ProgramID);
                            cmd.Parameters.AddWithValue("@RepairShoprCustomerID", customer.RepairShoprCustomerID);
                            cmd.Parameters.AddWithValue("@LastUpdated", DateTime.Now);
                            cmd.Parameters.AddWithValue("@DateAgreedToWaiver", DBNull.Value);
                            cmd.Parameters.AddWithValue("@ParentID", DBNull.Value);
                            customer.ID = DBHelper.GetInt32Value(cmd.ExecuteScalar());
                        }
                        con.Close();
                    }

                }
            }
            catch (Exception ex)
            {
                errorMsg = String.Format("{0}\r\n{1}", ex.Message, ex.StackTrace);
                return null;
            }
            return customer;
        }

        public Customer Update(Customer customer, out string errorMsg)
        {
            errorMsg = string.Empty;
            try
            {
                if (customer != null)
                {
                    string constr = ConfigurationManager.ConnectionStrings["Techcycle"].ConnectionString;
                    using (SqlConnection con = new SqlConnection(constr))
                    {
                        con.Open();
                        StringBuilder sql = new StringBuilder();
                        sql.AppendLine("UPDATE Customer ");
                        sql.AppendLine("SET ClientID=@ClientID, MobileNumber=@MobileNumber, HomeNumber=@HomeNumber, PrimaryName=@PrimaryName, AuthorizedName=@AuthorizedName, ");
                        sql.AppendLine("BillingAddressID=@BillingAddressID, ShippingAddressID=@ShippingAddressID, MailingAddressID=@MailingAddressID, ");
                        sql.AppendLine("Email=@Email, PrimaryFirstName=@PrimaryFirstName, PrimaryLastName=@PrimaryLastName, AccountNumber=@AccountNumber, ");
                        sql.AppendLine("SequenceNumber=@SequenceNumber, SubscriptionID=@SubscriptionID, StatusCode=@StatusCode, EnrollmentDate=@EnrollmentDate, ");
                        sql.AppendLine("CompanyName=@CompanyName, Authentication=@Password, DateSubscriptionEmailSent=@DateSubscriptionEmailSent, ProgramID=@ProgramID, ");
                        sql.AppendLine("RepairShoprCustomerID=@RepairShoprCustomerID, LastUpdated=@LastUpdated, DateAgreedToWaiver=@DateAgreedToWaiver, ParentID=@ParentID ");
                        sql.AppendLine("WHERE ID=@ID");

                        using (SqlCommand cmd = new SqlCommand(sql.ToString(), con))
                        {
                            cmd.CommandType = CommandType.Text;
                            cmd.Parameters.AddWithValue("@ID", customer.ID);
                            cmd.Parameters.AddWithValue("@ClientID", customer.ClientID);
                            cmd.Parameters.AddWithValue("@MobileNumber", customer.MobileNumber);
                            cmd.Parameters.AddWithValue("@HomeNumber", customer.HomeNumber);
                            cmd.Parameters.AddWithValue("@PrimaryName", customer.PrimaryName);
                            cmd.Parameters.AddWithValue("@AuthorizedName", customer.AuthorizedName);
                            cmd.Parameters.AddWithValue("@BillingAddressID", customer.BillingAddressID);
                            cmd.Parameters.AddWithValue("@ShippingAddressID", customer.ShippingAddressID);
                            cmd.Parameters.AddWithValue("@MailingAddressID", customer.MailingAddressID);
                            cmd.Parameters.AddWithValue("@Email", customer.Email);
                            cmd.Parameters.AddWithValue("@PrimaryFirstName", customer.PrimaryFirstName);
                            cmd.Parameters.AddWithValue("@PrimaryLastName", customer.PrimaryLastName);
                            cmd.Parameters.AddWithValue("@AccountNumber", customer.AccountNumber);
                            cmd.Parameters.AddWithValue("@SequenceNumber", customer.SequenceNumber);
                            cmd.Parameters.AddWithValue("@SubscriptionID", customer.SubscriptionID);
                            cmd.Parameters.AddWithValue("@StatusCode", customer.StatusCode);
                            if (customer.EnrollmentDate >= Convert.ToDateTime("2000-01-01"))
                                cmd.Parameters.AddWithValue("@EnrollmentDate", customer.EnrollmentDate);
                            else
                                cmd.Parameters.AddWithValue("@EnrollmentDate", DBNull.Value);
                            cmd.Parameters.AddWithValue("@CompanyName", customer.CompanyName);
                            cmd.Parameters.AddWithValue("@Password", customer.Password);
                            if (customer.DateSubscriptionEmailSent >= Convert.ToDateTime("2000-01-01"))
                                cmd.Parameters.AddWithValue("@DateSubscriptionEmailSent", customer.DateSubscriptionEmailSent);
                            else
                                cmd.Parameters.AddWithValue("@DateSubscriptionEmailSent", DBNull.Value);
                            cmd.Parameters.AddWithValue("@ProgramID", customer.ProgramID);
                            cmd.Parameters.AddWithValue("@RepairShoprCustomerID", customer.RepairShoprCustomerID);
                            cmd.Parameters.AddWithValue("@LastUpdated", DateTime.Now);
                            cmd.Parameters.AddWithValue("@DateAgreedToWaiver", DBNull.Value);
                            cmd.Parameters.AddWithValue("@ParentID", customer.ParentID);
                            int i = cmd.ExecuteNonQuery();
                            Console.WriteLine("{0} records updated.", i);
                        }
                        con.Close();
                    }

                }
            }
            catch (Exception ex)
            {
                errorMsg = String.Format("{0}\r\n{1}", ex.Message, ex.StackTrace);
                return null;
            }
            return customer;
        }

        public List<Customer> SelectNew(string clientCode, out string errorMsg)
        {
            List<Customer> result = new List<Customer>();
            errorMsg = string.Empty;
            try
            {
                ProgramHelper ph = new ProgramHelper();
                string constr = ConfigurationManager.ConnectionStrings[clientCode].ConnectionString;
                using (SqlConnection con = new SqlConnection(constr))
                {
                    con.Open();
                    StringBuilder sql = new StringBuilder();
                    sql.AppendLine("SELECT c.* FROM Customer c WITH(NOLOCK) ");
                    sql.AppendLine("INNER JOIN Client l WITH(NOLOCK) ON l.ID=c.ClientID ");
                    sql.AppendLine("WHERE l.Code=@Code AND c.StatusCode='New' AND DateSubscriptionEmailSent IS NULL ");
                    sql.AppendLine("ORDER BY ID DESC");

                    using (SqlCommand cmd = new SqlCommand(sql.ToString(), con))
                    {
                        cmd.CommandType = CommandType.Text;
                        cmd.Parameters.AddWithValue("@Code", clientCode);

                        SqlDataReader r = cmd.ExecuteReader();
                        while (r.Read())
                        {
                            Customer customer = new Customer(r);
                            List<Program> programs = ph.Select(customer.ProgramID, clientCode, out errorMsg);
                            if (programs.Count > 0)
                            {
                                customer.WarrantyProgram = programs[0];
                            }
                            result.Add(customer);
                        }
                        r.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                errorMsg = String.Format("ERROR: {0}\r\n{1}", ex.Message, ex.StackTrace);
            }
            return result;
        }

        public Customer SelectByClientAndAccountNumber(int clientID, string accountNumber, string clientCode, out string error)
        {
            Customer result = null;
            error = string.Empty;
            try
            {
                string constr = ConfigurationManager.ConnectionStrings[clientCode].ConnectionString;
                using (SqlConnection con = new SqlConnection(constr))
                {
                    con.Open();
                    StringBuilder sql = new StringBuilder();
                    sql.AppendLine("SELECT * FROM Customer WITH(NOLOCK) ");
                    sql.AppendLine("WHERE ClientID=@ClientID ");
                    sql.AppendLine("AND AccountNumber=@AccountNumber");
                    using (SqlCommand cmd = new SqlCommand(sql.ToString(), con))
                    {
                        cmd.CommandType = CommandType.Text;
                        cmd.Parameters.AddWithValue("@ClientID", clientID);
                        cmd.Parameters.AddWithValue("@AccountNumber", accountNumber);
                        SqlDataReader r = cmd.ExecuteReader();
                        if (r.Read())
                        {
                            result = new Customer(r);
                        }
                        r.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                error = String.Format("ERROR: {0}\r\n{1}", ex.Message, ex.StackTrace);
            }
            return result;
        }

        private EVSTAR.Models.Address FindAddress(int addressID, string clientCode)
        {
            EVSTAR.Models.Address address = null;

            string constr = ConfigurationManager.ConnectionStrings[clientCode].ConnectionString;
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
    }
}
