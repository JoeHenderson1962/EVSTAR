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
    public class UserHelper
    {
        public List<User> Select(string username, string password, int id, string clientCode, out string errorMsg)
        {
            AddressHelper addressHelper = new AddressHelper();
            ClientHelper clientHelper = new ClientHelper();

            List<User> result = new List<User>();
            errorMsg = string.Empty;
            try
            {
                string constr = ConfigurationManager.ConnectionStrings[clientCode].ConnectionString;
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

        public User Insert(User user, string clientCode, out string errorMsg)
        {
            AddressHelper addressHelper = new AddressHelper();
            //ClientHelper clientHelper = new ClientHelper();

            errorMsg = string.Empty;
            try
            {
                if (user != null)
                {
                    if (user.UserAddress != null)
                    {
                        if (user.UserAddress.ID > 0)
                        {
                            user.UserAddress = addressHelper.Update(user.UserAddress, clientCode, out errorMsg);
                            user.AddressID = user.UserAddress.ID;
                        }
                        else
                        {
                            user.UserAddress = addressHelper.Insert(user.UserAddress, clientCode, out errorMsg);
                            user.AddressID = user.UserAddress.ID;
                        }
                    }

                    if (user.Reset)
                        user.Authentication = "ChangeYourPassword";

                    string constr = ConfigurationManager.ConnectionStrings["Techcycle"].ConnectionString;
                    using (SqlConnection con = new SqlConnection(constr))
                    {
                        con.Open();
                        StringBuilder sql = new StringBuilder();
                        sql.AppendLine("OPEN SYMMETRIC KEY SymKey_Techcycle");
                        sql.AppendLine("DECRYPTION BY CERTIFICATE Techcycle;");
                        sql.AppendLine("INSERT INTO Users ");
                        sql.AppendLine("(ClientID, Active, Department, Email, AddressID, FirstName, LastName, ");
                        if (!user.Authentication.Contains("***"))
                            sql.AppendLine("AuthenticationEnc, ");
                        sql.AppendLine("Phone, ReportsTo, StoreID, Title, UserTypeID, UserName)");
                        sql.AppendLine("VALUES ");
                        sql.AppendLine("(@ClientID, @Active, @Department, @Email, @AddressID, @FirstName, @LastName, ");
                        if (!user.Authentication.Contains("***"))
                            sql.AppendLine("EncryptByKey (Key_GUID('SymKey_Techcycle'), @Authentication), ");
                        sql.AppendLine("@Phone, @ReportsTo, @StoreID, @Title, @UserTypeID, @UserName); ");
                        sql.AppendLine("CLOSE SYMMETRIC KEY SymKey_Techcycle;");
                        sql.AppendLine("SELECT SCOPE_IDENTITY(); ");
                        using (SqlCommand cmd = new SqlCommand(sql.ToString(), con))
                        {
                            cmd.CommandType = CommandType.Text;
                            cmd.Parameters.AddWithValue("@ClientID", user.ClientID);
                            cmd.Parameters.AddWithValue("@Active", user.Active);
                            cmd.Parameters.AddWithValue("@Department", user.Department);
                            cmd.Parameters.AddWithValue("@Email", user.Email);
                            cmd.Parameters.AddWithValue("@UserName", user.Email);
                            cmd.Parameters.AddWithValue("@AddressID", user.AddressID);
                            if (user.Reset)
                            {
                                cmd.Parameters.AddWithValue("@Authentication", "ChangeYourPassword");
                            }
                            else
                                if (!user.Authentication.Contains("***"))
                                cmd.Parameters.AddWithValue("@Authentication", user.Authentication);
                            cmd.Parameters.AddWithValue("@FirstName", !String.IsNullOrEmpty(user.FirstName) ? user.FirstName : "");
                            cmd.Parameters.AddWithValue("@LastName", !String.IsNullOrEmpty(user.LastName) ? user.LastName : "");
                            cmd.Parameters.AddWithValue("@Phone", !String.IsNullOrEmpty(user.Phone) ? user.Phone : "");
                            cmd.Parameters.AddWithValue("@ReportsTo", user.ReportsTo);
                            cmd.Parameters.AddWithValue("@StoreID", user.StoreID);
                            cmd.Parameters.AddWithValue("@Title", !String.IsNullOrEmpty(user.Title) ? user.Title : "");
                            cmd.Parameters.AddWithValue("@UserTypeID", (int)user.UserTypeID);
                            user.ID = DBHelper.GetInt32Value(cmd.ExecuteScalar());
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
            return user;
        }

        public User Update(User user, string clientCode, out string errorMsg)
        {
            AddressHelper addressHelper = new AddressHelper();

            errorMsg = string.Empty;
            try
            {
                if (user != null)
                {
                    if (user.UserAddress != null && !string.IsNullOrEmpty(user.UserAddress.Line1) && !string.IsNullOrEmpty(user.UserAddress.City))
                    {
                        if (user.UserAddress.ID > 0)
                        {
                            user.UserAddress = addressHelper.Update(user.UserAddress, clientCode, out errorMsg);
                            if (user.UserAddress != null)
                                user.AddressID = user.UserAddress.ID;
                        }
                        else
                        {
                            user.UserAddress = addressHelper.Insert(user.UserAddress, clientCode, out errorMsg);
                            if (user.UserAddress != null)
                                user.AddressID = user.UserAddress.ID;
                        }
                    }

                    string constr = ConfigurationManager.ConnectionStrings["Techcycle"].ConnectionString;
                    using (SqlConnection con = new SqlConnection(constr))
                    {
                        if (user.Reset)
                            user.Authentication = "ChangeYourPassword";

                        con.Open();
                        StringBuilder sql = new StringBuilder();
                        sql.AppendLine("OPEN SYMMETRIC KEY SymKey_Techcycle");
                        sql.AppendLine("DECRYPTION BY CERTIFICATE Techcycle;");
                        sql.AppendLine("UPDATE Users ");
                        sql.AppendLine("SET ClientID=@ClientID, Active=@Active, Department=@Department, Email=@Email, AddressID=@AddressID, ");
                        if (!user.Authentication.Contains("***"))
                            sql.AppendLine("AuthenticationEnc=EncryptByKey (Key_GUID('SymKey_Techcycle'), @Authentication), ");
                        sql.AppendLine("FirstName=@FirstName, LastName=@LastName, ");
                        sql.AppendLine("Phone=@Phone, ReportsTo=@ReportsTo, StoreID=@StoreID, Title=@Title, UserTypeID=@UserTypeID, LastUpdated=GETDATE() ");
                        sql.AppendLine("WHERE ID=@ID;");
                        sql.AppendLine("CLOSE SYMMETRIC KEY SymKey_Techcycle;");

                        using (SqlCommand cmd = new SqlCommand(sql.ToString(), con))
                        {
                            cmd.CommandType = CommandType.Text;
                            cmd.Parameters.AddWithValue("@ClientID", user.ClientID);
                            cmd.Parameters.AddWithValue("@Active", user.Active);
                            cmd.Parameters.AddWithValue("@Department", user.Department);
                            cmd.Parameters.AddWithValue("@Email", user.Email);
                            cmd.Parameters.AddWithValue("@AddressID", user.AddressID);
                            if (!user.Authentication.Contains("***"))
                                cmd.Parameters.AddWithValue("@Authentication", user.Authentication);
                            cmd.Parameters.AddWithValue("@FirstName", user.FirstName);
                            cmd.Parameters.AddWithValue("@LastName", user.LastName);
                            cmd.Parameters.AddWithValue("@Phone", String.IsNullOrEmpty(user.Phone) ? "" : user.Phone);
                            cmd.Parameters.AddWithValue("@ReportsTo", user.ReportsTo);
                            cmd.Parameters.AddWithValue("@StoreID", user.StoreID);
                            cmd.Parameters.AddWithValue("@Title", String.IsNullOrEmpty(user.Title) ? "" : user.Title);
                            cmd.Parameters.AddWithValue("@UserTypeID", (int)user.UserTypeID);
                            cmd.Parameters.AddWithValue("@ID", user.ID);
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
            return user;
        }

        public List<User> SelectByClient(int clientID, out string errorMsg)
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
                    sql.AppendLine("WHERE ClientID=@ClientID ");
                    sql.AppendLine("ORDER BY ID DESC");
                    sql.AppendLine("CLOSE SYMMETRIC KEY SymKey_Techcycle;");

                    using (SqlCommand cmd = new SqlCommand(sql.ToString(), con))
                    {
                        cmd.CommandType = CommandType.Text;
                        cmd.Parameters.AddWithValue("@ClientID", clientID);

                        SqlDataReader r = cmd.ExecuteReader();
                        while (r.Read())
                        {
                            User user = new User(r);
                            user.Authentication = "************";
                            result.Add(user);
                            List<Client> clients = clientHelper.Select(user.ClientID, out errorMsg);
                            if (clients != null && clients.Count > 0)
                                user.ParentClient = clients[0];
                            List<Address> addresses = addressHelper.Select(user.AddressID, out errorMsg);
                            if (addresses != null && addresses.Count > 0)
                                user.UserAddress = addresses[0];
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

        public void Delete(int id, out string errorMsg)
        {
            errorMsg = string.Empty;
            try
            {
                string constr = ConfigurationManager.ConnectionStrings["Techcycle"].ConnectionString;
                using (SqlConnection con = new SqlConnection(constr))
                {
                    con.Open();
                    StringBuilder sql = new StringBuilder();
                    sql.AppendLine("DELETE FROM Users ");
                    sql.AppendLine("WHERE ID=@ID ");

                    using (SqlCommand cmd = new SqlCommand(sql.ToString(), con))
                    {
                        cmd.CommandType = CommandType.Text;
                        cmd.Parameters.AddWithValue("@ID", id);
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                errorMsg = String.Format("ERROR: {0}\r\n{1}", ex.Message, ex.StackTrace);
            }
        }
    }
}
