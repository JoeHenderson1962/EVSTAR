using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.IO;
using Newtonsoft.Json;
using EVSTAR.Models;
using System.Diagnostics;
using Comment = EVSTAR.Models.Comment;
using EVSTAR.RepairShopr.API;
using EVSTAR.DB.NET;

namespace DocumentAutomation
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
        private static ContactsManager contactMgr = new ContactsManager();
        private static CustomersManager customerMgr = new CustomersManager();
        private static TicketsManager ticketsMgr = new TicketsManager();
        private static AssetsManager assetsMgr = new AssetsManager();

        //static string delayedEmail = "<br />Subject Line: Your repair is delayed<br /><br />" +
        //    "Dear {CUSTNAME}<br /><br />" +
        //    "Our apologies, but there is a delay in your repair. Our team is working to resolve the issue as " +
        //    "quickly as possible. We will update you as soon as we complete the repair and ship your device " +
        //    "back to you.<br /><br />" +
        //    "Thanks, the EVSTAR Warranty Team<br />";
        //static string inProgressEmail = "<br />Subject Line: Your Device is Being Repaired<br /><br />" +
        //    "Dear {CUSTNAME}<br /><br />" +
        //    "We have completed our inspection of your device and it's currently being repaired. We will let " +
        //    "you know when it's on the way back to you.<br /><br />" +
        //    "Thanks, the EVSTAR Warranty Team<br />";
        //static string receivedEmail = "<br />Subject Line: We have received your device<br /><br />" +
        //    "Dear {CUSTNAME}<br /><br />" +
        //    "Good news, we have received your device.  We will take your {DEVICE} through our thorough " +
        //    "inspection process and get started on your repair.<br /><br />" +
        //    "Thanks, the EVSTAR Warranty Team<br />";
        //static string inProgress2Email = "<br />Subject Line: Your Device is Being Repaired<br /><br />" +
        //    "Dear {CUSTNAME}<br /><br />" +
        //    "We wanted to let you know, your device is being repaired.  We will let you know when we are " +
        //    "finished and your device is back in business and on its way to you.<br /><br />" +
        //    "Thanks, the EVSTAR Warranty Team<br />";
        //static string claimEmail = "<br />Subject Line: Your claim has been submitted<br /><br />" +
        //    "Dear {CUSTNAME}<br /><br />" +
        //    "Thank you for initiating your claim.  Your next step is to ship your device to us for repair. " +
        //    "Remember to follow the instructions from the claims portal for shipping and to print your prepaid " +
        //    "label.<br /><br />" +
        //    "We will let you know as soon as we receive your device. If you have any questions, please " +
        //    "access the <a href=\"https://repair.gotechcycle.com\">customer portal</a><br /><br />" +
        //    "Thanks, the EVSTAR Warranty Team<br />";
        //static string shippedEmail = "<br />Subject Line: Your device has been shipped<br /><br />" +
        //    "Dear {CUSTNAME}<br /><br />" +
        //    "Great news!  Your device has been repaired and is on the way back to you.  Your tracking " +
        //    "information is {TRACKING}.  Your claim is now complete.<br /><br />" +
        //    "Thanks, the EVSTAR Warranty Team<br />";

        public static string ParagraphText(XElement e)
        {
            XNamespace w = e.Name.Namespace;
            return e
                   .Elements(w + "r")
                   .Elements(w + "t")
                   .StringConcatenate(element => (string)element);
        }

        static void Main(string[] args)
        {
            bool autoFound = false;
            bool dobsonFound = false;
            bool asurionFound = false;
            bool evseFound = false;
            string toEmail = string.Empty;

            LogMessage("DocumentAutomation args: ");
            foreach (string arg in args)
            {
                LogMessage(arg);
                autoFound = autoFound || arg.ToLower().Contains("auto");
                dobsonFound = dobsonFound || arg.ToLower().Contains("dobson");
                asurionFound = asurionFound || arg.ToLower().Contains("asurion");
                evseFound = evseFound || arg.ToLower().Contains("evse");
                if (arg.ToLower().Contains("email"))
                {
                    string[] parts=arg.Split('=');
                    if (parts.Length == 2) 
                    {
                        toEmail = parts[1];
                    }
                }
            }

            if (evseFound)
                ProcessEVSEEmails();

            if (dobsonFound)
                ProcessDobsonEmails(toEmail);

            if (asurionFound)
                ProcessAsurionMails();

            Console.WriteLine("To see instructions, run the program without the -auto parameter.");
            if (!autoFound)
            {
                Console.WriteLine("To process Asurion ticket status update emails, use the -asurion parameter; e.g., DocumentAutomation.exe -asurion");
                Console.WriteLine("To process Dobson new subscriber emails, use the -dobson parameter; e.g., DocumentAutomation.exe -dobson");
                Console.WriteLine("*** NOTE: To process Dobson emails, the program must be running on the goevstar.com server in order to access the database.");
                Console.WriteLine("To run this from a scheduler without having it wait for a key, use the -auto parameter; e.g., DocumentAutomation.exe -auto");
                Console.WriteLine();
                Console.WriteLine("You can use the parameters in any combination; e.g., DocumentAutomation.exe -auto -dobson -asurion");
                Console.WriteLine("or DocumentAutomation.exe -asurion -auto");
                Console.WriteLine("etc.");
                Console.WriteLine();
                Console.WriteLine("Press any key to continue.");
                Console.ReadKey();
            }
        }

        private static void ProcessDobsonEmails(string toEmail)
        {
            Console.WriteLine("Checking for new Dobson customers.");
            LogMessage("Checking for new Dobson customers.");
            CustomerHelper customerHelper = new CustomerHelper();
            ProgramHelper program = new ProgramHelper();
            string error;
            string programNames = "TPRT,TPRTP,TPRTR";
            List<Customer> customerList = customerHelper.SelectNew("DOB", out error);
            Console.WriteLine(error);
            LogMessage(error);
            string html = string.Empty;
            using (StreamReader sr = new StreamReader("DobsonProtectionPlanWelcomeEmail.html"))
            {
                html = sr.ReadToEnd();
                sr.Close();
            }

            foreach (Customer customer in customerList)
            {
                if (!String.IsNullOrEmpty(customer.Email) && customer.WarrantyProgram != null && programNames.Contains(customer.WarrantyProgram.ProgramName) 
                    && (String.IsNullOrEmpty(toEmail) || customer.Email.ToUpper() == toEmail.ToUpper()))
                {
                    Console.WriteLine(customer.Email + " " + customer.WarrantyProgram.ProgramName);
                    LogMessage(customer.Email + " " + customer.WarrantyProgram.ProgramName);
                    string msgBody = html.Replace("{CUSTOMER NAME}", customer.PrimaryName)
                        .Replace("{PLAN NAME}", customer.WarrantyProgram.Description).Replace("{CODE}", customer.SubscriptionID);
                    string result = SendEmail(customer.Email, String.Format("Welcome to {0} by EVSTAR", customer.WarrantyProgram.Description), msgBody);
                    result = result.Trim();
                    Console.WriteLine(result);
                    LogMessage(result);
                    if (String.IsNullOrEmpty(result))
                    {
                        customer.DateSubscriptionEmailSent = DateTime.Now;
                        Console.WriteLine("Updating customer {0}", customer.Email);
                        customerHelper.Update(customer, "DOB", out error);
                        Console.WriteLine(error);
                    }
                }
            }
        }

        private static void ProcessEVSEEmails()
        {
            Console.WriteLine("Checking for new EVSE customers.");
            LogMessage("Checking for new EVSE customers.");
            CustomerHelper customerHelper = new CustomerHelper();
            ProgramHelper program = new ProgramHelper();
            CoveredProductHelper cph = new CoveredProductHelper();
            ClientHelper clientHelper = new ClientHelper();
            string error;
            string programNames = "EVSTAR";
            List<Customer> customerList = customerHelper.SelectNew("EVSTAR", out error);
            Console.WriteLine(error);
            LogMessage(error);
            string html = string.Empty;
            using (StreamReader sr = new StreamReader("EVSTARProtectionPlanWelcomeEmail.html"))
            {
                html = sr.ReadToEnd();
                sr.Close();
            }

            foreach (Customer customer in customerList)
            {
                if (!String.IsNullOrEmpty(customer.Email) && customer.WarrantyProgram != null && programNames.Contains(customer.WarrantyProgram.ProgramName))
                {
                    Console.WriteLine(customer.Email + " " + customer.WarrantyProgram.ProgramName);
                    LogMessage(customer.Email + " " + customer.WarrantyProgram.ProgramName);
                    string msgBody = html.Replace("{CUSTOMER NAME}", customer.PrimaryName)
                        .Replace("{PLAN NAME}", customer.WarrantyProgram.Description).Replace("{CODE}", customer.SubscriptionID);

                    List<CoveredProduct> products = cph.Select(0, customer.ID, "EVSTAR", out error);
                    if (products != null && products.Count > 0) 
                        msgBody = msgBody.Replace("{DEVICE NAME}", products[0].Manufacturer +  " " + products[0].Model);
                    customer.CustomerClient = clientHelper.Select(customer.ClientID, "EVSTAR", out error).FirstOrDefault();
                    string result = SendEmail(customer.Email, String.Format("Welcome to {0} by EVSTAR", customer.WarrantyProgram.Description), msgBody);
                    result = result.Trim();
                    Console.WriteLine(result);
                    LogMessage(result);
                    if (String.IsNullOrEmpty(result))
                    {
                        customer.DateSubscriptionEmailSent = DateTime.Now;
                        Console.WriteLine("Updating customer {0}", customer.Email);
                        customerHelper.Update(customer, customer.CustomerClient.Code, out error);
                        Console.WriteLine(error);
                    }
                }
            }
        }

        private static void ProcessAsurionMails()
        {
            LogMessage("Processing Asurion mail.");
            DateTime? lastUpdated = GetLastSyncTime("ServiceBenchMail");
            if (!lastUpdated.HasValue || lastUpdated.Value < DateTime.Now.AddDays(-10))
                lastUpdated = DateTime.Now.AddDays(-3);
            Console.WriteLine("Checking for Service Bench tickets in RepairShopr updated since {0}", lastUpdated.Value.ToString());
            LogMessage("Checking for Service Bench tickets in RepairShopr updated since {0}", lastUpdated.Value.ToString());

            int custID = (int)GetCustomerIDFromDB("SB");
            Tickets ticketList = ticketsMgr.GetRepairShoprTicketsSince(custID, lastUpdated);
            if (ticketList != null && ticketList.tickets != null)
            {
                if (ticketList.tickets.Count > 0 && ticketList.tickets[0].subject.StartsWith("ERROR:"))
                    LogMessage(ticketList.tickets[0].subject);
                else
                {
                    Console.WriteLine("Found {0} updated ServiceBench tickets.", ticketList.tickets.Count);
                    LogMessage("Found {0} updated ServiceBench tickets.", ticketList.tickets.Count);

                    foreach (Ticket ticket in ticketList.tickets)
                    {
                        Console.WriteLine("Processing ticket {0}", ticket.number);
                        LogMessage("Processing ticket {0}", ticket.number);

                        var t = Task.Run(() => RetrieveTicketInvoices(ticket.id));
                        t.Wait();
                        RSInvoices invoices = t.Result;
                        if (ticket.contact != null)
                        {
                            string name = ticket.contact.name;
                            string docText = string.Empty;
                            string serial = string.Empty;
                            string make = string.Empty;
                            string model = string.Empty;
                            string product = string.Empty;
                            string reason = string.Empty;
                            string comments = string.Empty;
                            string boxcontents = string.Empty;
                            string srNumber = string.Empty;
                            string purchasedFrom = string.Empty;
                            string phoneNumber = string.Empty;
                            string productLine = String.Empty;

                            if (ticket.properties != null)
                            {
                                if (!string.IsNullOrEmpty(ticket.properties.CustomerRefNumberPOTicketWorkOrderEtc))
                                {
                                    string[] temp = ticket.properties.CustomerRefNumberPOTicketWorkOrderEtc.Split('|');
                                    if (temp.Length > 1)
                                        srNumber = temp[0];
                                    else
                                        srNumber = ticket.properties.CustomerRefNumberPOTicketWorkOrderEtc;

                                    if (ticket.assets.Count > 0 && ticket.assets[0] != null)
                                    {
                                        make = ticket.assets[0].properties.Make;
                                        model = ticket.assets[0].properties.Model;
                                        serial = ticket.assets[0].asset_serial;
                                        product = ticket.assets[0].name;
                                        productLine = ticket.assets[0].properties.Product_Line_Description;
                                        boxcontents = @"\par " + ticket.properties.AsurionBoxContents;
                                        purchasedFrom = ticket.properties.Originator;

                                        switch (purchasedFrom.ToUpper())
                                        {
                                            case "AAFES":
                                                phoneNumber = "800-861-9389";
                                                break;
                                            case "VETERANS CANTEEN":
                                                phoneNumber = "866-442-3183";
                                                break;
                                            case "NEXCOM":
                                                phoneNumber = "866-368-2559";
                                                break;
                                            case "COASTGUARD EXCHANGE":
                                                phoneNumber = "800-254-1913";
                                                break;
                                            case "MARINE CORP. EXCHANGE":
                                                phoneNumber = "866-805-7924";
                                                break;
                                        }

                                        if (serial.ToUpper().Contains(String.Format("_{0}", make.ToUpper())))
                                        {
                                            serial = serial.Substring(0, serial.LastIndexOf(String.Format("_{0}", make.ToUpper())));
                                        }
                                    }

                                    if (ticket.comments.Count > 0)
                                        foreach (Comment comment in ticket.comments)
                                        {
                                            //if (comment.subject.ToUpper() == "UPDATE" && !comment.hidden && comment.tech.ToUpper() != "CUSTOMER-REPLY")
                                            //    comments += comment.body + "\r\n";
                                            if (comment.subject.ToUpper() == "UPDATE" && !comment.hidden && ticket.status.ToUpper().StartsWith("DENIED"))
                                                reason = comment.body;
                                        }

                                    if (invoices.invoices.Count > 0)
                                        foreach (RSInvoice invoice in invoices.invoices)
                                        {
                                            if (invoice.line_items != null && invoice.line_items.line_items != null && invoice.line_items.line_items.Count > 0)
                                            {
                                                foreach (LineItem lineItem in invoice.line_items.line_items)
                                                {
                                                    comments += lineItem.item + @"\par ";
                                                }
                                            }
                                        }
                                    Console.WriteLine("Ticket status is {0} for ticket {1}.", ticket.status, ticket.number);
                                    LogMessage("Ticket status is {0} for ticket {1}.", ticket.status, ticket.number);
                                    switch (ticket.status.ToUpper())
                                    {
                                        case "DENIED - RETURN TO CUSTOMER":

                                            try
                                            {
                                                string fileName = String.Format("{0}_{1}_denied.rtf", name, product);
                                                string buffer = string.Empty;
                                                using (StreamReader sr = new StreamReader("F-003-900 Denial letter.rtf"))
                                                {
                                                    buffer = sr.ReadToEnd();
                                                    buffer = buffer.Replace("[CUSTOMER NAME]", name);
                                                    buffer = buffer.Replace("[SRNUMBER]", srNumber);
                                                    buffer = buffer.Replace("[DATERECEIVED]", Convert.ToDateTime(ticket.properties.DateReceived).ToString("MM/dd/yyyy"));
                                                    buffer = buffer.Replace("[DATESHIPPED]", DateTime.Now.ToString("MM/dd/yyyy"));
                                                    buffer = buffer.Replace("[MAKE]", make);
                                                    buffer = buffer.Replace("[MODEL]", model);
                                                    buffer = buffer.Replace("[SERIAL]", serial);
                                                    buffer = buffer.Replace("[PRODUCT]", product);
                                                    buffer = buffer.Replace("[BOXCONTENTS]", boxcontents);
                                                    buffer = buffer.Replace("[COMMENTS]", comments);
                                                    buffer = buffer.Replace("[REASON]", reason);
                                                    buffer = buffer.Replace("[PHONENUMBER]", phoneNumber);
                                                    sr.Close();
                                                }

                                                using (StreamWriter sw = new StreamWriter(fileName))
                                                {
                                                    sw.Write(buffer);
                                                    sw.Flush();
                                                    sw.Close();
                                                }

                                                //Using below code we can print any document
                                                ProcessStartInfo info = new ProcessStartInfo(fileName);

                                                info.Verb = "Print";

                                                info.CreateNoWindow = false;

                                                info.WindowStyle = ProcessWindowStyle.Normal;

                                                Process.Start(info);
                                            }
                                            catch (Exception ex)
                                            {
                                                LogMessage("Error printing Denied document: {0}", ex.Message);
                                            }
                                            break;

                                        //case "RESOLVED- SHIPPED":
                                        case "REPAIR COMPLETE":
                                            try
                                            {
                                                string fileName2 = String.Format("{0}_{1}_resolved.rtf", name, product).Replace("\\", "_").Replace("/", "_").Replace(" ", "_");
                                                if (String.IsNullOrEmpty(ticket.properties.DateReceived))
                                                    ticket.properties.DateReceived = ticket.created_at;
                                                string buffer = string.Empty;
                                                using (StreamReader sr = new StreamReader("F-003-899 Repair complete form.rtf"))
                                                {
                                                    buffer = sr.ReadToEnd();
                                                    buffer = buffer.Replace("[CUSTOMER NAME]", name);
                                                    buffer = buffer.Replace("[SRNUMBER]", srNumber);
                                                    buffer = buffer.Replace("[DATERECEIVED]", Convert.ToDateTime(ticket.properties.DateReceived).ToString("MM/dd/yyyy"));
                                                    buffer = buffer.Replace("[DATESHIPPED]", DateTime.Now.ToString("MM/dd/yyyy"));
                                                    buffer = buffer.Replace("[MAKE]", make);
                                                    buffer = buffer.Replace("[MODEL]", model);
                                                    buffer = buffer.Replace("[SERIAL]", serial);
                                                    buffer = buffer.Replace("[PRODUCT]", product);
                                                    buffer = buffer.Replace("[BOXCONTENTS]", boxcontents);
                                                    buffer = buffer.Replace("[COMMENTS]", comments);
                                                    buffer = buffer.Replace("[PHONENUMBER]", phoneNumber);
                                                    sr.Close();
                                                }

                                                using (StreamWriter sw = new StreamWriter(fileName2))
                                                {
                                                    sw.Write(buffer);
                                                    sw.Flush();
                                                    sw.Close();
                                                }

                                                //Using below code we can print any document

                                                ProcessStartInfo info2 = new ProcessStartInfo(fileName2);

                                                info2.Verb = "Print";

                                                info2.CreateNoWindow = true;

                                                info2.WindowStyle = ProcessWindowStyle.Hidden;

                                                Process.Start(info2);
                                            }
                                            catch (Exception ex)
                                            {
                                                Console.WriteLine(ex.Message);
                                                LogMessage("Error printing Repair Complete document: {0}", ex.Message);
                                            }
                                            break;

                                    }
                                }
                                else
                                {
                                    Console.WriteLine("Ticket {0} is missing its CustomerRefNumberPOTicketWorkOrderEtc custom field.", ticket.number);
                                    LogMessage("Ticket {0} is missing its CustomerRefNumberPOTicketWorkOrderEtc custom field.", ticket.number);
                                }
                            }
                            else
                            {
                                Console.WriteLine("Ticket {0} is missing its custom fields.", ticket.number);
                                LogMessage("Ticket {0} is missing its custom fields.", ticket.number);
                            }
                        }
                        else
                        {
                            Console.WriteLine("Ticket {0} has no contacts.", ticket.number);
                            LogMessage("Ticket {0} has no contacts.", ticket.number);
                        }
                    }
                }
            }
            else
            {
                Console.WriteLine("Did not find any updated Service Bench tickets.");
                LogMessage("Did not find any updated Service Bench tickets.");
            }
            UpdateLastSyncTime("ServiceBenchMail");
        }

        private static string SendEmail(string recip, string subject, string message)
        {
            string result = string.Empty;

            string smtpReplyTo = ConfigurationManager.AppSettings["EmailReplyTo"];
            string smtpReplyToName = ConfigurationManager.AppSettings["EmailReplyToName"];
            string smtpHost = ConfigurationManager.AppSettings["SMTPHost"];
            string smtpPort = ConfigurationManager.AppSettings["SMTPPort"];
            string smtpUser = ConfigurationManager.AppSettings["SMTPUser"];
            string smtpPassword = ConfigurationManager.AppSettings["SMTPPassword"];
            string smtpSecurity = ConfigurationManager.AppSettings["SMTPSecurity"];

            try
            {
                try
                {
                    MailMessage msg = new MailMessage();
                    msg.To.Add(new MailAddress(recip.Trim()));
                    msg.Subject = subject;
                    msg.Body = message;
                    msg.IsBodyHtml = message.Length >= 5 && message.Substring(0, 5).Equals("<HTML", StringComparison.OrdinalIgnoreCase);
                    msg.ReplyToList.Add(new MailAddress(smtpReplyTo, smtpReplyToName));
                    msg.Sender = new MailAddress(smtpUser, smtpReplyToName);
                    msg.From = new MailAddress(smtpUser, smtpReplyToName);
                    SmtpClient client;
                    if (string.IsNullOrEmpty(smtpPort))
                        client = new SmtpClient(smtpHost);
                    else
                        client = new SmtpClient(smtpHost, Convert.ToInt32(smtpPort));
                    client.EnableSsl = !String.IsNullOrEmpty(smtpSecurity);
                    if (smtpUser.Length > 0 && smtpPassword.Length > 0)
                    {
                        client.UseDefaultCredentials = false;
                        client.Credentials = new System.Net.NetworkCredential(smtpUser, smtpPassword);
                    }
                    else
                        client.UseDefaultCredentials = true;
                    client.Send(msg);
                }
                catch (Exception ex)
                {
                    result = ex.Message;
                    LogMessage("Error in SendEmail: {0}", ex.Message);
                }

            }
            catch (Exception ex)
            {
                result = ex.Message;
                LogMessage("Error in SendEmail: {0}", ex.Message);
            }
            return result;
        }

        private static long GetCustomerIDFromDB(string code)
        {
            long customerID = 0;
            try
            {
                string constr = ConfigurationManager.ConnectionStrings["Techcycle"].ConnectionString;
                using (SqlConnection con = new SqlConnection(constr))
                {
                    con.Open();
                    StringBuilder sql = new StringBuilder();
                    sql.AppendLine("SELECT RepairShoprCustomerID FROM Client WITH(NOLOCK) WHERE Code=@Code");
                    using (SqlCommand cmd = new SqlCommand(sql.ToString(), con))
                    {
                        cmd.CommandType = CommandType.Text;
                        cmd.Parameters.AddWithValue("@Code", code);
                        SqlDataReader r = cmd.ExecuteReader();
                        while (r.Read())
                        {
                            customerID = DBHelper.GetInt64Value(r["RepairShoprCustomerID"]);
                        }
                        r.Close();
                    }
                    con.Close();
                }
            }
            catch (Exception ex)
            {
                LogMessage("Error in GetCustomerIDFromDB {0}\r\n{1}", ex.Message, ex.StackTrace);

            }

            return customerID;
        }

        private static DateTime? GetLastSyncTime(string name)
        {
            LogMessage("Getting LastSyncTime");
            DateTime? lastSyncTime = new DateTime();
            try
            {
                string constr = ConfigurationManager.ConnectionStrings["Techcycle"].ConnectionString;
                using (SqlConnection con = new SqlConnection(constr))
                {
                    con.Open();
                    StringBuilder sql = new StringBuilder();
                    sql.AppendLine("SELECT LastSyncDateTime FROM Synchronization WITH(NOLOCK) WHERE Name=@Name");
                    using (SqlCommand cmd = new SqlCommand(sql.ToString(), con))
                    {
                        cmd.CommandType = CommandType.Text;
                        cmd.Parameters.AddWithValue("@Name", name);
                        SqlDataReader r = cmd.ExecuteReader();
                        if (r.Read())
                        {
                            lastSyncTime = DBHelper.GetDateTimeValue(r["LastSyncDateTime"]);
                            LogMessage("Got LastSyncDateTime of {0}", lastSyncTime.ToString());
                        }
                        r.Close();
                    }
                    con.Close();
                }
            }
            catch (Exception ex)
            {
                LogMessage("Error in GetLastSyncTime {0}\r\n{1}", ex.Message, ex.StackTrace);
            }
            return lastSyncTime;
        }

        private static void UpdateLastSyncTime(string name)
        {
            LogMessage("Updating LastSyncTime");
            try
            {
                string constr = ConfigurationManager.ConnectionStrings["Techcycle"].ConnectionString;
                using (SqlConnection con = new SqlConnection(constr))
                {
                    con.Open();
                    StringBuilder sql = new StringBuilder();
                    sql.AppendLine(String.Format("UPDATE Synchronization SET LastSyncDateTime=@LastSyncDateTime WHERE Name='{0}'", name));
                    using (SqlCommand cmd = new SqlCommand(sql.ToString(), con))
                    {
                        cmd.CommandType = CommandType.Text;
                        cmd.Parameters.AddWithValue("@LastSyncDateTime", DateTime.Now);
                    }
                    con.Close();
                }
            }
            catch (Exception ex)
            {

            }
        }

        private static async Task<Tickets> RetrieveCustomerTickets(int custID, DateTime? lastUpdated)
        {
            LogMessage("Retrieving tickets for customer ({0}) since {1}", custID, lastUpdated.HasValue ? lastUpdated.Value : DateTime.Now);
            Tickets result = new Tickets();
            if (custID > 0)
            {
                string resp = String.Empty;
                HttpClient httpClient = new HttpClient();
                try
                {
                    httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    httpClient.DefaultRequestHeaders.Add("Authorization", String.Format("Bearer {0}", ConfigurationManager.AppSettings["RepairShopr"]));
                    string apiHost = "";
                    if (lastUpdated.HasValue)
                        apiHost = String.Format("https://techcyclesolutions.repairshopr.com/api/v1/tickets?customer_id={0}&since_updated_at={1}",
                            custID, lastUpdated.Value.ToString("yyyy-MM-dd"));
                    else
                        apiHost = String.Format("https://techcyclesolutions.repairshopr.com/api/v1/tickets?customer_id={0}", custID);
                    var uri = new Uri(apiHost);

                    var httpResponseMessage = await httpClient.GetAsync(uri);
                    resp = await httpResponseMessage.Content.ReadAsStringAsync();
                    result = JsonConvert.DeserializeObject<Tickets>(resp);
                    if (result != null && result.tickets != null)
                        for (int i = 0; i < result.tickets.Count; i++)
                        {
                            var t = Task.Run(() => RetrieveOneTicket(result.tickets[i].id));
                            t.Wait();
                            result.tickets[i] = t.Result;
                        }
                }
                catch (Exception ex)
                {
                    resp = String.Format("ERROR:\r\n{0}\r\n{1}", ex.Message, ex.StackTrace);
                    Ticket tck = new Ticket()
                    {
                        id = 0,
                        subject = resp
                    };
                    result.tickets.Add(tck);
                    LogMessage(tck.subject);
                }
            }
            else
            {
                Ticket tck = new Ticket()
                {
                    id = 0,
                    subject = String.Format("ERROR: Customer not found ({0})", custID)
                };
                result.tickets.Add(tck);
                LogMessage(tck.subject);
            }

            return result;
        }


        private static async Task<Ticket> RetrieveOneTicket(long ticketID)
        {
            LogMessage("Retrieving info for ticket {0}", ticketID);
            Ticket result = null;
            if (ticketID > 0)
            {
                string resp = String.Empty;
                HttpClient httpClient = new HttpClient();
                try
                {
                    httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    httpClient.DefaultRequestHeaders.Add("Authorization", String.Format("Bearer {0}", ConfigurationManager.AppSettings["RepairShopr"]));
                    string apiHost = String.Format("https://techcyclesolutions.repairshopr.com/api/v1/tickets/{0}", ticketID);
                    var uri = new Uri(apiHost);

                    var httpResponseMessage = await httpClient.GetAsync(uri);
                    resp = await httpResponseMessage.Content.ReadAsStringAsync();
                    resp = resp.Replace("{\"ticket\":", "");
                    resp = resp.Substring(0, resp.Length - 1);
                    result = JsonConvert.DeserializeObject<Ticket>(resp);
                }
                catch (Exception ex)
                {
                    resp = String.Format("ERROR:\r\n{0}\r\n{1}", ex.Message, ex.StackTrace);
                    Ticket tck = new Ticket()
                    {
                        id = 0,
                        subject = resp
                    };
                    result = tck;
                    LogMessage(tck.subject);
                }
            }
            else
            {
                Ticket tck = new Ticket()
                {
                    id = 0,
                    subject = String.Format("ERROR: Ticket not found ({0})", ticketID)
                };
                result = tck;
                LogMessage(tck.subject);
            }

            return result;
        }

        private static async Task<RSInvoices> RetrieveTicketInvoices(long ticketID)
        {
            LogMessage("Retrieving Ticket Invoices");
            RSInvoices result = new RSInvoices();
            if (ticketID > 0)
            {
                string resp = String.Empty;
                HttpClient httpClient = new HttpClient();
                try
                {
                    httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    httpClient.DefaultRequestHeaders.Add("Authorization", String.Format("Bearer {0}", ConfigurationManager.AppSettings["RepairShopr"]));
                    string apiHost = "";
                    apiHost = String.Format("https://techcyclesolutions.repairshopr.com/api/v1/invoices?ticket_id={0}", ticketID);
                    var uri = new Uri(apiHost);

                    var httpResponseMessage = await httpClient.GetAsync(uri);
                    resp = await httpResponseMessage.Content.ReadAsStringAsync();
                    result = JsonConvert.DeserializeObject<RSInvoices>(resp);
                    if (result != null && result.invoices != null)
                        for (int i = 0; i < result.invoices.Count; i++)
                        {
                            var t = Task.Run(() => RetrieveInvoiceWithLineItems(result.invoices[i].id));
                            t.Wait();
                            result.invoices[i] = t.Result;
                        }
                }
                catch (Exception ex)
                {
                    resp = String.Format("ERROR:\r\n{0}\r\n{1}", ex.Message, ex.StackTrace);
                    LogMessage("ERROR:\r\n{0}\r\n{1}", ex.Message, ex.StackTrace);
                    RSInvoice tck = new RSInvoice()
                    {
                        id = 0,
                        customer_business_then_name = resp
                    };
                    if (result.invoices == null)
                        result.invoices = new List<RSInvoice>();
                    result.invoices.Add(tck);
                }
            }
            else
            {
                RSInvoice tck = new RSInvoice()
                {
                    id = 0,
                    customer_business_then_name = String.Format("ERROR: Customer not found ({0})", ticketID)
                };
                if (result.invoices == null)
                    result.invoices = new List<RSInvoice>();
                result.invoices.Add(tck);
                LogMessage("ERROR: Customer not found for ticket ({0})", ticketID);
            }

            return result;
        }

        private static async Task<RSInvoice> RetrieveInvoiceWithLineItems(int invoiceID)
        {
            LogMessage("Getting invoice with line items.");
            RSInvoice result = new RSInvoice();
            if (invoiceID > 0)
            {
                string resp = String.Empty;
                HttpClient httpClient = new HttpClient();
                try
                {
                    httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    httpClient.DefaultRequestHeaders.Add("Authorization", String.Format("Bearer {0}", ConfigurationManager.AppSettings["RepairShopr"]));
                    string apiHost = "";
                    apiHost = String.Format("https://techcyclesolutions.repairshopr.com/api/v1/invoices/{0}", invoiceID);
                    var uri = new Uri(apiHost);

                    var httpResponseMessage = await httpClient.GetAsync(uri);
                    resp = await httpResponseMessage.Content.ReadAsStringAsync();
                    result = JsonConvert.DeserializeObject<RSInvoice>(resp);
                }
                catch (Exception ex)
                {
                    resp = String.Format("ERROR:\r\n{0}\r\n{1}", ex.Message, ex.StackTrace);
                    LineItem tck = new LineItem()
                    {
                        id = 0,
                        name = resp 
                    };
                    if (result.line_items == null)
                        result.line_items = new LineItems();
                    if (result.line_items.line_items == null)
                        result.line_items.line_items = new List<LineItem>();

                    result.line_items.line_items.Add(tck);
                    LogMessage(resp);
                }
            }
            else
            {
                LineItem tck = new LineItem()
                {
                    id = 0,
                    name = String.Format("ERROR: Customer not found ({0})", invoiceID)
                };
                if (result.line_items == null)
                    result.line_items = new LineItems();
                if (result.line_items.line_items == null)
                    result.line_items.line_items = new List<LineItem>();

                result.line_items.line_items.Add(tck);
                LogMessage("ERROR: Customer not found for invoice ({0})", invoiceID);
            }

            return result;
        }

        private static string swLogFileName = "";
        private static StreamWriter swLogFile = null;

        private static void LogMessage(string message, params object[] arg)
        {
            try
            {
                string newLogFileName = String.Format("DocumentAutomation_{0}.log", DateTime.Today.ToString("yyyy-MM-dd"));
                if (swLogFileName != newLogFileName)
                {
                    try
                    {
                        
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
            }
            catch (Exception ex)
            {
                string msg = ex.Message;
            }
        }
    }
}
