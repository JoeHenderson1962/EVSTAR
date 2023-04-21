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
        private static string filePath = @"C:\SFTP_Root\dobson";
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

        const int SALE_PURCHASE_DATE_DIS = 0;
        const int CANCELLATION_DATE_DIS = 1;
        const int CUSTOMER_FIRST_NAME_DIS = 2;
        //const int CUSTOMER_LAST_NAME_DIS = 3;
        const int CUSTOMER_ADDRESS_1_DIS = 3;
        const int CUSTOMER_ADDRESS_2_DIS = 4;
        const int CUSTOMER_CITY_DIS = 5;
        const int CUSTOMER_STATE_DIS = 6;
        const int CUSTOMER_ZIP_CODE_DIS = 7;
        const int CUSTOMER_PHONE_DIS = 8;
        const int CUSTOMER_EMAIL_DIS = 9;
        const int SKU_COVERAGE_CODE_DIS = 10;
        const int CUSTOMER_BILLING_ACCT_DIS = 11;

        static void Main(string[] args)
        {
            DirectoryInfo di = new DirectoryInfo(filePath);
            foreach (FileInfo fi in di.GetFiles("*.csv"))
            {
                ImportFile(fi);
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

        private static bool ImportFile(FileInfo fi)
        {
            ProgramHelper programHelper = new ProgramHelper();
            AddressHelper addressHelper = new AddressHelper();
            string error = string.Empty;

            try
            {
                List<EVSTAR.Models.Program> programList = programHelper.Select(0, "DOB", out error);

                Console.WriteLine("Processing " + fi.FullName);
                using (TextFieldParser csvReader = new TextFieldParser(fi.FullName))
                {
                    csvReader.CommentTokens = new string[] { "#" };
                    csvReader.SetDelimiters(new string[] { "," });
                    csvReader.HasFieldsEnclosedInQuotes = false;

                    // Skip the row with the column names
                    string[] header = csvReader.ReadFields();
                    if (header.Length > 0)
                    {
                        bool isNew = fi.FullName.ToUpper().Contains("ADDS.");
                        int i = 0;
                        while (!csvReader.EndOfData)
                        {
                            i++;

                            Address address = new Address();

                            Customer customer = new Customer();
                            // Read current line fields, pointer moves to the next line.
                            string[] fields = csvReader.ReadFields();

                            if (fields.Length < 12)
                            {
                                Console.WriteLine("Invalid number of fields in record # {0}", i);

                                continue;
                            }


                            if (isNew)
                            {
                                Console.WriteLine("Processing new record.");
                                Customer cust = customerHelper.SelectByClientAndAccountNumber(5, fields[CUSTOMER_BILLING_ACCT_NEW].Trim(), "DOB", out error);
                                if (cust == null || String.IsNullOrEmpty(cust.PrimaryName))
                                {
                                    Console.WriteLine("Customer not found in database...creating.");
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
                                        Console.WriteLine(error);
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
                                        customer.ClientID = 5;
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
                                        customerHelper.Insert(customer, out error);
                                        if (!string.IsNullOrEmpty(error))
                                        {
                                            Console.WriteLine(error);
                                        }
                                    }
                                }
                                else
                                {
                                    Console.WriteLine($"Customer found in database...skipping: {cust.PrimaryName}");
                                }
                            }
                            else
                            {
                                Console.WriteLine("Processing cancellation.");
                                if (fields.Length > CUSTOMER_BILLING_ACCT_DIS)
                                {
                                    // Get the current customer info then set the cancellation date and update the customer.

                                    Customer cust = customerHelper.SelectByClientAndAccountNumber(5, fields[CUSTOMER_BILLING_ACCT_DIS].Trim(), "DOB", out error);
                                    if (cust != null && String.IsNullOrEmpty(error))
                                    {
                                        cust.StatusCode = "Cancel";
                                        customerHelper.Update(cust, out error);
                                    }
                                }

                            }
                        }
                    }
                    else
                    {
                        Console.WriteLine("Invalid header length.");
                    }
                    csvReader.Close();
                }
                File.Move(fi.FullName, fi.FullName.ToLower().Replace(".csv", ".done"));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return false;
            }
            return true;
        }
    }
}
