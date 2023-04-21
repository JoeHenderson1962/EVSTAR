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
    public class TokenHelper
    {
        public List<User> Select(string username, string password, int id, out string errorMsg)
        {
            AddressHelper addressHelper = new AddressHelper();
            ClientHelper clientHelper = new ClientHelper();

            List<User> result = new List<User>();
            errorMsg = string.Empty;
            try
            {
                string constr = ConfigurationManager.ConnectionStrings["Techcycle"].ConnectionString;
                using (SqlConnection con = new SqlConnection(constr))
                {
                    con.Open();
                    StringBuilder sql = new StringBuilder();
                    sql.AppendLine("OPEN SYMMETRIC KEY SymKey_Techcycle");
                    sql.AppendLine("DECRYPTION BY CERTIFICATE Techcycle;");
                    sql.AppendLine("SELECT ID, UserName, ClientID, StoreID, FirstName, LastName, Title, LastUpdated, Department, ReportsTo, Email, Phone, Active, UserTypeID, AddressID, ");
                    sql.AppendLine("CONVERT(nvarchar(50), DecryptByKey([AuthenticationEnc])) AS 'Authentication'");
                    sql.AppendLine("FROM Users WITH(NOLOCK) ");
                    if (id > 0)
                        sql.AppendLine("WHERE ID=@ID ");

                    if (!string.IsNullOrEmpty(username))
                        sql.AppendLine("WHERE UserName=@UserName ");
                    sql.AppendLine("ORDER BY ID DESC");
                    sql.AppendLine("CLOSE SYMMETRIC KEY SymKey_Techcycle;");

                    //                    if (!string.IsNullOrEmpty(password))
                    //                        sql.AppendLine("AND Authentication=@Authentication OR Authentication='ChangeYourPassword' ");

                    using (SqlCommand cmd = new SqlCommand(sql.ToString(), con))
                    {
                        cmd.CommandType = CommandType.Text;
                        if (id > 0)
                            cmd.Parameters.AddWithValue("@ID", id);

                        if (!string.IsNullOrEmpty(username))
                            cmd.Parameters.AddWithValue("@UserName", username);

                        //                        if (!string.IsNullOrEmpty(password))
                        //                            cmd.Parameters.AddWithValue("@Authentication", password);

                        SqlDataReader r = cmd.ExecuteReader();
                        while (r.Read())
                        {
                            User user = new User(r);
                            if (user.Authentication == "ChangeYourPassword")
                                user.Reset = true;
                            if (user.Authentication == "ChangeYourPassword" || (string.IsNullOrEmpty(password) && id > 0) || user.Authentication == password)
                            {
                                result.Add(user);
                                if (user.ClientID > 0)
                                {
                                    List<Client> clients = clientHelper.Select(user.ClientID, out errorMsg);
                                    if (clients != null && clients.Count > 0)
                                        user.ParentClient = clients[0];
                                }
                                if (user.AddressID > 0)
                                {
                                    List<Address> addresses = addressHelper.Select(user.AddressID, out errorMsg);
                                    if (addresses != null && addresses.Count > 0)
                                        user.UserAddress = addresses[0];
                                }
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
    }
}
