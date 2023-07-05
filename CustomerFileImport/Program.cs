using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;
using Microsoft.VisualBasic.FileIO;
using EVSTAR.DB.NET;
using EVSTAR.Models;
using System.Security.Cryptography;
using System.Configuration;

namespace CustomerFileImport
{
    public static class LocalExtensions
    {
        public static string StringConcatenate(this IEnumerable<string> source)
        {
            StringBuilder sb = new StringBuilder();
            foreach (string s in source)
                sb.Append(s);
            return sb.ToString();
        }

        public static string StringConcatenate<T>(this IEnumerable<T> source,
            Func<T, string> func)
        {
            StringBuilder sb = new StringBuilder();
            foreach (T item in source)
                sb.Append(func(item));
            return sb.ToString();
        }

        public static string StringConcatenate(this IEnumerable<string> source, string separator)
        {
            StringBuilder sb = new StringBuilder();
            foreach (string s in source)
                sb.Append(s).Append(separator);
            return sb.ToString();
        }

        public static string StringConcatenate<T>(this IEnumerable<T> source,
            Func<T, string> func, string separator)
        {
            StringBuilder sb = new StringBuilder();
            foreach (T item in source)
                sb.Append(func(item)).Append(separator);
            return sb.ToString();
        }
    }

    internal class Program
    {
        private static CustomerHelper customerHelper = new CustomerHelper();
        private static ProgramHelper programHelper = new ProgramHelper();
        private static AddressHelper addressHelper = new AddressHelper();
        private static CoverageHelper coverageHelper = new CoverageHelper();
        private static CoveredProductHelper coveredProductHelper = new CoveredProductHelper();
        private static string filePath = @"C:\SFTP_Root\dobson";
        private static string clientCode = "DOB";

        static void Main(string[] args)
        {
            string client = string.Empty;

            if (args.Length > 0 && args[0].ToLower().Contains("-client"))
            {
                client = args[0].ToUpper();
            }
            if (args.Length > 1 && args[1].ToLower().Contains("-client"))
            {
                client = args[1].ToUpper();
            }

            if (!String.IsNullOrEmpty(client))
            {
                string[] parts = client.Split('=');
                if (parts.Length > 1)
                    clientCode = parts[1];

                if (clientCode == "DOB")
                    filePath = @"C:\SFTP_Root\dobson";
                else if (clientCode == "REACH")
                    filePath = @"C:\SFTP_Root\reachmobile";
                else if (clientCode == "EVSTAR")
                    filePath = @"C:\SFTP_Root\evstar";

                LogMessage($"Processing files for {clientCode} from folder {filePath}.");
            }

            DirectoryInfo di = new DirectoryInfo(filePath);
            FileInfo[] files = di.GetFiles("*.csv");
            LogMessage($"Found {files.Length} files.");
            foreach (FileInfo fi in files)
            {
                switch (clientCode)
                {
                    case "DOB":
                        ImportDobsonFile(fi);
                        break;

                    case "EVSTAR":
                        ImportEVSTARFile(fi);
                        break;
                }
            }

            if (args.Length > 0 && args[0].ToLower() == "-pause")
                Console.ReadLine();
        }
        public static string RandomString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            var random = new Random();
            return new string(Enumerable.Repeat(chars, length).Select(s => s[random.Next(s.Length)]).ToArray());
        }

        private static bool ImportDobsonFile(FileInfo fi)
        {
            const int CUSTOMER_BILLING_ACCT_NEW = 0;
            const int SALE_PURCHASE_DATE_NEW = 1;
            const int CANCELLATION_DATE_NEW = 2;
            const int CUSTOMER_FIRST_NAME_NEW = 3;
            //const int CUSTOMER_LAST_NAME_NEW = 4;
            const int CUSTOMER_ADDRESS_1_NEW = 4;
            const int CUSTOMER_ADDRESS_2_NEW = 5;
            const int CUSTOMER_CITY_NEW = 6;
            const int CUSTOMER_STATE_NEW = 7;
            const int CUSTOMER_ZIP_CODE_NEW = 8;
            const int CUSTOMER_PHONE_NEW = 9;
            const int CUSTOMER_EMAIL_NEW = 10;
            const int SKU_COVERAGE_CODE_NEW = 11;

            string error = string.Empty;

            try
            {
                List<EVSTAR.Models.Program> programList = programHelper.Select(0, "DOB", out error);
                LogMessage(error);

                LogMessage("Processing " + fi.FullName);
                using (TextFieldParser csvReader = new TextFieldParser(fi.FullName))
                {
                    csvReader.CommentTokens = new string[] { "#" };
                    csvReader.SetDelimiters(new string[] { "," });
                    csvReader.HasFieldsEnclosedInQuotes = false;

                    // Skip the row with the column names
                    string[] header = csvReader.ReadFields();
                    if (header.Length > 0)
                    {
                        bool isNew = fi.FullName.ToUpper().Contains("ADDS.") || fi.FullName.ToUpper().Contains("INITIAL");
                        int i = 0;
                        while (!csvReader.EndOfData)
                        {
                            i++;

                            Address address = new Address();

                            // Read current line fields, pointer moves to the next line.
                            string[] fields = csvReader.ReadFields();

                            if (fields.Length < 12)
                            {
                                LogMessage("Invalid number of fields in record # {0}", i);

                                continue;
                            }


                            if (isNew)
                            {
                                if (!String.IsNullOrEmpty(fields[0]) && fields[0].Trim().Length > 0)
                                {
                                    LogMessage("Processing new record.");
                                    int clientID = DBHelper.GetInt32Value(ConfigurationManager.AppSettings["DOBClientID"]);
                                    Customer customer = customerHelper.SelectByClientAndAccountNumber(clientID, fields[CUSTOMER_BILLING_ACCT_NEW].Trim().PadLeft(9, '0'),
                                        null, "DOB", out error);
                                    if (customer == null || String.IsNullOrEmpty(customer.PrimaryName))
                                    {
                                        if (customer == null)
                                            customer = new Customer();
                                        LogMessage("Customer not found in database...creating.");
                                        if (fields.Length > CUSTOMER_ADDRESS_1_NEW)
                                            address.Line1 = fields[CUSTOMER_ADDRESS_1_NEW];
                                        if (fields.Length > CUSTOMER_ADDRESS_2_NEW)
                                            address.Line2 = fields[CUSTOMER_ADDRESS_2_NEW];
                                        if (fields.Length > CUSTOMER_CITY_NEW)
                                            address.City = fields[CUSTOMER_CITY_NEW];
                                        if (fields.Length > CUSTOMER_STATE_NEW)
                                            address.State = fields[CUSTOMER_STATE_NEW];
                                        if (fields.Length > CUSTOMER_ZIP_CODE_NEW)
                                            address.PostalCode = fields[CUSTOMER_ZIP_CODE_NEW];
                                        address.Country = "USA";
                                        address = addressHelper.Insert(address, "DOB", out error);
                                        if (!string.IsNullOrEmpty(error))
                                        {
                                            LogMessage(error);
                                        }

                                        if (address != null)
                                        {
                                            customer.MailingAddressID = address.ID;
                                            customer.BillingAddressID = address.ID;
                                            customer.ShippingAddressID = address.ID;

                                            if (fields.Length > CUSTOMER_BILLING_ACCT_NEW)
                                                customer.AccountNumber = fields[CUSTOMER_BILLING_ACCT_NEW];
                                            customer.HomeNumber = string.Empty;
                                            customer.SequenceNumber = "1";
                                            if (fields.Length > CUSTOMER_PHONE_NEW)
                                                customer.MobileNumber = fields[CUSTOMER_PHONE_NEW];
                                            if (customer.MobileNumber != null)
                                            {
                                                customer.MobileNumber = customer.MobileNumber.Replace("(", "").Replace(")", "").Replace("-", "").Replace(" ", "");
                                            }
                                            if (fields.Length > CUSTOMER_EMAIL_NEW)
                                                customer.Email = fields[CUSTOMER_EMAIL_NEW];
                                            customer.AuthorizedName = string.Empty;
                                            customer.ClientID = clientID;
                                            customer.CompanyName = string.Empty;
                                            if (fields.Length > SALE_PURCHASE_DATE_NEW)
                                                customer.EnrollmentDate = DBHelper.GetDateTimeValue(fields[SALE_PURCHASE_DATE_NEW]);
                                            if (fields.Length > CUSTOMER_FIRST_NAME_NEW)
                                            {
                                                string temp = fields[CUSTOMER_FIRST_NAME_NEW];
                                                string[] parts = temp.Split(' ');
                                                if (parts.Length > 0)
                                                    customer.PrimaryFirstName = parts[0];
                                                if (parts.Length > 1)
                                                    customer.PrimaryLastName = parts[parts.Length - 1];
                                                customer.PrimaryName = fields[CUSTOMER_FIRST_NAME_NEW];
                                            }
                                            //if (fields.Length > CUSTOMER_LAST_NAME_NEW)
                                            //{
                                            //    customer.PrimaryLastName = fields[CUSTOMER_LAST_NAME_NEW];
                                            //    customer.PrimaryName = customer.PrimaryFirstName + " " + customer.PrimaryLastName;
                                            //}
                                            if (string.IsNullOrEmpty(customer.StatusCode))
                                                customer.StatusCode = "New";
                                            customer.SubscriptionID = Path.GetRandomFileName().Substring(0, 8).ToUpper();
                                            foreach (EVSTAR.Models.Program program in programList)
                                            {
                                                if (fields.Length > SKU_COVERAGE_CODE_NEW)
                                                    if (program.ProgramName.ToUpper() == fields[SKU_COVERAGE_CODE_NEW].ToUpper())
                                                    {
                                                        customer.ProgramID = program.ID;
                                                        break;
                                                    }
                                            }
                                            customerHelper.Insert(customer, "DOB", out error);
                                            if (!string.IsNullOrEmpty(error))
                                            {
                                                LogMessage(error);
                                            }
                                        }
                                    }
                                    else
                                    {
                                        LogMessage($"Customer found in database...updating address: {customer.PrimaryName}");
                                        if (customer.BillingAddress == null)
                                        {
                                            customer.BillingAddress = addressHelper.Select(customer.BillingAddressID, "DOB", out error).FirstOrDefault();
                                            if (customer.BillingAddress == null || customer.BillingAddress.ID == 0)
                                                customer.BillingAddress = new Address();

                                            if (fields.Length > CUSTOMER_ADDRESS_1_NEW)
                                                customer.BillingAddress.Line1 = fields[CUSTOMER_ADDRESS_1_NEW];
                                            if (fields.Length > CUSTOMER_ADDRESS_2_NEW)
                                                customer.BillingAddress.Line2 = fields[CUSTOMER_ADDRESS_2_NEW];
                                            if (fields.Length > CUSTOMER_CITY_NEW)
                                                customer.BillingAddress.City = fields[CUSTOMER_CITY_NEW];
                                            if (fields.Length > CUSTOMER_STATE_NEW)
                                                customer.BillingAddress.State = fields[CUSTOMER_STATE_NEW];
                                            if (fields.Length > CUSTOMER_ZIP_CODE_NEW)
                                                customer.BillingAddress.PostalCode = fields[CUSTOMER_ZIP_CODE_NEW];
                                            customer.BillingAddress.Country = "USA";
                                            if (customer.BillingAddress.ID == 0)
                                                customer.BillingAddress = addressHelper.Insert(customer.BillingAddress, "DOB", out error);
                                            else
                                                customer.BillingAddress = addressHelper.Update(customer.BillingAddress, "DOB", out error);

                                            if (string.IsNullOrEmpty(error))
                                            {
                                                customer.ShippingAddressID = customer.BillingAddress.ID;
                                                customer.BillingAddressID = customer.BillingAddress.ID;
                                                customer.MailingAddressID = customer.BillingAddress.ID;
                                                customer = customerHelper.Update(customer, "DOB", out error);
                                                if (!string.IsNullOrEmpty(error))
                                                    LogMessage($"Error updating customer: {error}");
                                            }
                                            else
                                            {
                                                LogMessage($"Error updating address: {error}");
                                            }
                                        }
                                    }
                                }
                            }
                            else
                            {
                                //if (!String.IsNullOrEmpty(fields[0]) && fields[0].Trim().Length > 0)
                                //{
                                LogMessage("Processing cancellation.");
                                if (fields.Length > SKU_COVERAGE_CODE_NEW)
                                {
                                    LogMessage("Finding customer with account number {0} and name {1} in the database.", fields[CUSTOMER_BILLING_ACCT_NEW],
                                        fields[CUSTOMER_FIRST_NAME_NEW]);
                                    // Get the current customer info then set the cancellation date and update the customer.

                                    Customer cust = customerHelper.SelectByClientAndAccountNumber(5, fields[CUSTOMER_BILLING_ACCT_NEW].Trim().PadLeft(9, '0'),
                                        string.IsNullOrEmpty(fields[SALE_PURCHASE_DATE_NEW]) ? new DateTime?() : Convert.ToDateTime(fields[SALE_PURCHASE_DATE_NEW]), "DOB", out error);
                                    if (cust != null && String.IsNullOrEmpty(error))
                                    {
                                        cust.StatusCode = "Cancel";
                                        cust.CancellationDate = Convert.ToDateTime(fields[CANCELLATION_DATE_NEW]);
                                        customerHelper.Update(cust, "DOB", out error);
                                        if (!string.IsNullOrEmpty(error))
                                            LogMessage("Error cancelling customer: {0}", error);
                                        else
                                        {
                                            Coverage coverage = coverageHelper.Select(0, cust.ID, cust.ProgramID, cust.ClientID, "DOB", out error).FirstOrDefault();
                                            if (coverage != null)
                                            {
                                                coverage.CancelDate = DBHelper.GetNullableDateTimeValue(fields[CANCELLATION_DATE_NEW]);
                                                coverage = coverageHelper.Update(coverage, "DOB", out error);
                                                if (!string.IsNullOrEmpty(error))
                                                    LogMessage("Error updating coverage: {0}", error);
                                            }
                                        }
                                    }
                                    else if (!string.IsNullOrEmpty(error))
                                        LogMessage("Error looking up customer to cancel: {0}", error);
                                    else if (cust == null)
                                    {
                                        LogMessage("Unable to find customer with account number {0} and name {1} in the database.", fields[CUSTOMER_BILLING_ACCT_NEW],
                                            fields[CUSTOMER_FIRST_NAME_NEW]);
                                    }
                                }
                                //}
                            }
                        }
                    }
                    else
                    {
                        LogMessage("Invalid header length.");
                    }
                    csvReader.Close();
                }
                LogMessage("Moving file {0} to {1}.", fi.FullName, fi.FullName.ToLower().Replace(@"\dobson\", @"\dobson\complete\"));
                File.Move(fi.FullName, fi.FullName.ToLower().Replace(@"\dobson\", @"\dobson\complete\"));
            }
            catch (Exception ex)
            {
                LogMessage(ex.ToString());
                return false;
            }
            return true;
        }

        private static bool ImportEVSTARFile(FileInfo fi)
        {
            const int SALE_PURCHASE_DATE = 0;
            const int WARRANTY_END_DATE = 1;
            const int CANCELLATION_DATE = 2;
            const int CUSTOMER_FIRST_NAME = 3;
            const int CUSTOMER_LAST_NAME = 4;
            const int CUSTOMER_ADDRESS_1 = 5;
            const int CUSTOMER_ADDRESS_2 = 6;
            const int CUSTOMER_CITY = 7;
            const int CUSTOMER_STATE = 8;
            const int CUSTOMER_ZIP_CODE = 9;
            const int CUSTOMER_PHONE = 10;
            const int CUSTOMER_EMAIL = 11;
            const int SKU_COVERAGE_CODE = 12;

            string error = string.Empty;

            try
            {
                List<EVSTAR.Models.Program> programList = programHelper.Select(0, "EVSTAR", out error);
                int clientID = DBHelper.GetInt32Value(ConfigurationManager.AppSettings["EVSTARClientID"]);

                LogMessage("Processing " + fi.FullName);
                using (TextFieldParser csvReader = new TextFieldParser(fi.FullName))
                {
                    csvReader.CommentTokens = new string[] { "#" };
                    csvReader.SetDelimiters(new string[] { "," });
                    csvReader.HasFieldsEnclosedInQuotes = false;

                    // Skip the row with the column names
                    string[] header = csvReader.ReadFields();
                    if (header.Length > 0)
                    {
                        int i = 0;
                        while (!csvReader.EndOfData)
                        {
                            i++;

                            Address address = new Address();
                            Customer customer = new Customer();
                            CoveredProduct coveredProduct = new CoveredProduct();

                            // Read current line fields, pointer moves to the next line.
                            string[] fields = csvReader.ReadFields();

                            if (fields.Length < 12)
                            {
                                LogMessage("Invalid number of fields in record # {0}", i);

                                continue;
                            }

                            if (fields[CANCELLATION_DATE].Trim().Length == 0)  // New record
                            {
                                LogMessage("Processing new record.");
                                Customer cust = customerHelper.Select(fields[CUSTOMER_EMAIL], string.Empty, fields[CUSTOMER_PHONE], 0, clientID, "EVSTAR", out error).FirstOrDefault();
                                if (cust == null || String.IsNullOrEmpty(cust.PrimaryName))
                                {
                                    LogMessage("Customer not found in database...creating.");
                                    if (fields.Length > CUSTOMER_ADDRESS_1)
                                        address.Line1 = fields[CUSTOMER_ADDRESS_1];
                                    if (fields.Length > CUSTOMER_ADDRESS_2)
                                        address.Line2 = fields[CUSTOMER_ADDRESS_2];
                                    if (fields.Length > CUSTOMER_CITY)
                                        address.City = fields[CUSTOMER_CITY];
                                    if (fields.Length > CUSTOMER_STATE)
                                        address.State = fields[CUSTOMER_STATE];
                                    if (fields.Length > CUSTOMER_ZIP_CODE)
                                        address.PostalCode = fields[CUSTOMER_ZIP_CODE];
                                    address.Country = "USA";
                                    address = addressHelper.Insert(address, "EVSTAR", out error);
                                    if (!string.IsNullOrEmpty(error))
                                    {
                                        LogMessage(error);
                                    }

                                    if (address != null)
                                    {
                                        customer.MailingAddressID = address.ID;
                                        customer.BillingAddressID = address.ID;
                                        customer.ShippingAddressID = address.ID;

                                        customer.HomeNumber = string.Empty;
                                        customer.SequenceNumber = "1";
                                        if (fields.Length > CUSTOMER_PHONE)
                                            customer.MobileNumber = fields[CUSTOMER_PHONE];
                                        if (customer.MobileNumber != null)
                                        {
                                            customer.MobileNumber = customer.MobileNumber.Replace("(", "").Replace(")", "").Replace("-", "").Replace(" ", "");
                                        }
                                        if (fields.Length > CUSTOMER_EMAIL)
                                            customer.Email = fields[CUSTOMER_EMAIL];
                                        customer.AuthorizedName = string.Empty;
                                        customer.ClientID = clientID;
                                        customer.CompanyName = string.Empty;
                                        if (fields.Length > SALE_PURCHASE_DATE)
                                            customer.EnrollmentDate = DBHelper.GetDateTimeValue(fields[SALE_PURCHASE_DATE]);
                                        if (fields.Length > CUSTOMER_FIRST_NAME)
                                        {
                                            customer.PrimaryFirstName = fields[CUSTOMER_FIRST_NAME];
                                            //string temp = fields[CUSTOMER_FIRST_NAME];
                                            //string[] parts = temp.Split(' ');
                                            //if (parts.Length > 0)
                                            //    customer.PrimaryFirstName = parts[0];
                                            //if (parts.Length > 1)
                                            //    customer.PrimaryLastName = parts[parts.Length - 1];
                                            //customer.PrimaryName = fields[CUSTOMER_FIRST_NAME];
                                        }
                                        if (fields.Length > CUSTOMER_LAST_NAME)
                                        {
                                            customer.PrimaryLastName = fields[CUSTOMER_LAST_NAME];
                                            customer.PrimaryName = customer.PrimaryFirstName + " " + customer.PrimaryLastName;
                                        }
                                        customer.StatusCode = "New";
                                        customer.SubscriptionID = Path.GetRandomFileName().Substring(0, 8).ToUpper();
                                        customer.ProgramID = programList[0].ID;
                                        //foreach (EVSTAR.Models.Program program in programList)
                                        //{
                                        //    if (fields.Length > SKU_COVERAGE_CODE)
                                        //        if (program.ProgramName.ToUpper() == fields[SKU_COVERAGE_CODE].ToUpper())
                                        //        {
                                        //            customer.ProgramID = program.ID;
                                        //            break;
                                        //        }
                                        //}
                                        if (fields.Length > SKU_COVERAGE_CODE)
                                        {
                                            string[] temp = fields[SKU_COVERAGE_CODE].Split(' ');
                                            if (temp.Length > 1)
                                            {
                                                coveredProduct.Manufacturer = temp[0];
                                                for (int c = 1; c < temp.Length; c++)
                                                {
                                                    coveredProduct.Model += temp[c] + ' ';
                                                }
                                                coveredProduct.Model = coveredProduct.Model.Trim();
                                            }
                                            else if (temp.Length == 1)
                                            {
                                                coveredProduct.Manufacturer = temp[0];
                                            }
                                        }
                                        customer = customerHelper.Insert(customer, "EVSTAR", out error);
                                        if (!string.IsNullOrEmpty(error))
                                        {
                                            LogMessage(error);
                                        }
                                        else if (coveredProduct.Manufacturer.Length > 0)
                                        {
                                            coveredProduct.CustomerID = customer.ID;
                                            coveredProduct.CoverageDate = DBHelper.GetDateTimeValue(fields[SALE_PURCHASE_DATE]);
                                            coveredProduct.CategoryName = "EV Chargers";
                                            coveredProduct.Description = fields[SKU_COVERAGE_CODE];
                                            coveredProduct.ProductCategoryID = 5;
                                            coveredProduct = coveredProductHelper.Insert(coveredProduct, "EVSTAR", out error);

                                            Coverage coverage = new Coverage();
                                            coverage.CustomerID = customer.ID;
                                            coverage.CoveredProductID = coveredProduct.ID;
                                            coverage.ClientID = clientID;
                                            coverage.EffectiveDate = DBHelper.GetDateTimeValue(fields[SALE_PURCHASE_DATE]);
                                            coverage.ProgramID = customer.ProgramID;
                                            coverage.UpdatedOn = DateTime.Now;
                                            coverage = coverageHelper.Insert(coverage, "EVSTAR", out error);
                                        }
                                    }
                                }
                                else
                                {
                                    LogMessage($"Customer found in database...skipping: {cust.PrimaryName}");
                                }
                            }
                            else
                            {
                                LogMessage("Processing cancellation.");
                                // Get the current customer info then set the cancellation date and update the customer.

                                Customer cust = customerHelper.Select(fields[CUSTOMER_EMAIL], string.Empty, fields[CUSTOMER_PHONE], 0, clientID, "EVSTAR", out error).FirstOrDefault();
                                if (cust != null && String.IsNullOrEmpty(error))
                                {
                                    cust.StatusCode = "Cancel";
                                    customerHelper.Update(cust, "EVSTAR", out error);

                                    Coverage coverage = coverageHelper.Select(0, cust.ID, cust.ProgramID, cust.ClientID, "EVSTAR", out error).FirstOrDefault();
                                    if (coverage != null)
                                    {
                                        coverage.CancelDate = DBHelper.GetNullableDateTimeValue(fields[CANCELLATION_DATE]);
                                        coverage = coverageHelper.Update(coverage, "EVSTAR", out error);
                                    }
                                }
                            }
                        }
                    }
                    else
                    {
                        LogMessage("Invalid header length.");
                    }
                    csvReader.Close();
                }
                File.Move(fi.FullName, fi.FullName.ToLower().Replace(".csv", ".done"));
            }
            catch (Exception ex)
            {
                LogMessage(ex.ToString());
                return false;
            }
            return true;
        }

        private static string swLogFileName = "";
        private static StreamWriter swLogFile = null;

        private static void LogMessage(string message, params object[] arg)
        {
            try
            {
                string newLogFileName = String.Format("CustomerFileImport_{0}.log", DateTime.Today.ToString("yyyy-MM-dd"));
                if (swLogFileName != newLogFileName)
                {
                    try
                    {
                        if (swLogFile != null)
                            swLogFile.Close();
                    }
                    catch (Exception e)
                    {

                    }
                    swLogFileName = newLogFileName;
                    swLogFile = new StreamWriter(swLogFileName, true, Encoding.ASCII);
                }
                swLogFile.WriteLine(message, arg);
                swLogFile.Flush();
                if (Environment.UserInteractive)
                    Console.WriteLine(message, arg);
            }
            catch (Exception ex)
            {
                string msg = ex.Message;
            }
        }
    }
}
