using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EVSTAR.Models;

namespace EVSTAR.DB.NET
{
    public class SubscriptionHelper
    {
        public int Insert(Subscription subscription, out string error)
        {
            int result = 0;
            error = string.Empty;

            try
            {
                string constr = ConfigurationManager.ConnectionStrings["Techcycle"].ConnectionString;
                using (SqlConnection con = new SqlConnection(constr))
                {
                    con.Open();
                    StringBuilder sql = new StringBuilder();
                    sql.AppendLine("INSERT INTO InboundSubscription (Sale_Purchase_Date, Coverage_Start_Date, Cancellation_Date, Cancel_Reason_Code, ");
                    sql.AppendLine("Customer_First_Name, Customer_Last_Name, Customer_Address_1, Customer_Address_2, Customer_City, Customer_State, ");
                    sql.AppendLine("Customer_Country, Customer_Phone, Customer_Email, SKU_Coverage_Code, Contract_Refund_Amount, Contract_Price_Retail_Cost, ");
                    sql.AppendLine("Subscribed_Phone, Subscribed_Make, Subscribed_Model, Subscribed_IMEI, Subscribed_ESN, Carrier, Action) VALUES ");
                    sql.AppendLine("(@Sale_Purchase_Date, @Coverage_Start_Date, @Cancellation_Date, @Cancel_Reason_Code, @Customer_First_Name, ");
                    sql.AppendLine("@Customer_Last_Name, @Customer_Address_1, @Customer_Address_2, @Customer_City, @Customer_State, ");
                    sql.AppendLine("@Customer_Country, @Customer_Phone, @Customer_Email, @SKU_Coverage_Code, @Contract_Refund_Amount, ");
                    sql.AppendLine("@Contract_Price_Retail_Cost, @Subscribed_Phone, @Subscribed_Make, @Subscribed_Model, @Subscribed_IMEI, ");
                    sql.AppendLine("@Subscribed_ESN, @Carrier, @Action); ");
                    sql.AppendLine("SELECT SCOPE_IDENTITY();");

                    DBHelper.LogMessage(sql.ToString());

                    using (SqlCommand cmd = new SqlCommand(sql.ToString(), con))
                    {
                        cmd.CommandType = CommandType.Text;
                        if (subscription.Sale_Purchase_Date > DateTime.Parse("2022-11-01"))
                            cmd.Parameters.AddWithValue("@Sale_Purchase_Date", subscription.Sale_Purchase_Date);
                        else
                            cmd.Parameters.AddWithValue("@Sale_Purchase_Date", DBNull.Value);
                        if (subscription.Coverage_Start_Date > DateTime.Parse("2022-11-01"))
                            cmd.Parameters.AddWithValue("@Coverage_Start_Date", subscription.Coverage_Start_Date);
                        else
                            cmd.Parameters.AddWithValue("@Coverage_Start_Date", DBNull.Value);
                        if (subscription.Cancellation_Date > DateTime.Parse("2022-11-01"))
                            cmd.Parameters.AddWithValue("@Cancellation_Date", subscription.Cancellation_Date);
                        else
                            cmd.Parameters.AddWithValue("@Cancellation_Date", DBNull.Value);
                        cmd.Parameters.AddWithValue("@Cancel_Reason_Code", subscription.Cancel_Reason_Code != null ? subscription.Cancel_Reason_Code : String.Empty);
                        cmd.Parameters.AddWithValue("@Customer_First_Name", subscription.Customer_First_Name != null ? subscription.Customer_First_Name : String.Empty);
                        cmd.Parameters.AddWithValue("@Customer_Last_Name", subscription.Customer_Last_Name != null ? subscription.Customer_Last_Name : String.Empty);
                        cmd.Parameters.AddWithValue("@Customer_Address_1", subscription.Customer_Address_1 != null ? subscription.Customer_Address_1 : String.Empty);
                        cmd.Parameters.AddWithValue("@Customer_Address_2", subscription.Customer_Address_2 != null ? subscription.Customer_Address_2 : String.Empty);
                        cmd.Parameters.AddWithValue("@Customer_City", subscription.Customer_City != null ? subscription.Customer_City : String.Empty);
                        cmd.Parameters.AddWithValue("@Customer_State", subscription.Customer_State != null ? subscription.Customer_State : String.Empty);
                        cmd.Parameters.AddWithValue("@Customer_Country", subscription.Customer_Country != null ? subscription.Customer_Country : String.Empty);
                        cmd.Parameters.AddWithValue("@Customer_Phone", subscription.Customer_Phone != null ? subscription.Customer_Phone : String.Empty);
                        cmd.Parameters.AddWithValue("@Customer_Email", subscription.Customer_Email != null ? subscription.Customer_Email : String.Empty);
                        cmd.Parameters.AddWithValue("@SKU_Coverage_Code", subscription.SKU_Coverage_Code != null ? subscription.SKU_Coverage_Code : String.Empty);
                        cmd.Parameters.AddWithValue("@Contract_Refund_Amount", subscription.Contract_Refund_Amount);
                        cmd.Parameters.AddWithValue("@Contract_Price_Retail_Cost", subscription.Contract_Price_Retail_Cost);
                        cmd.Parameters.AddWithValue("@Subscribed_Phone", subscription.Subscribed_Phone != null ? subscription.Subscribed_Phone : String.Empty);
                        cmd.Parameters.AddWithValue("@Subscribed_Make", subscription.Subscribed_Make != null ? subscription.Subscribed_Make : String.Empty);
                        cmd.Parameters.AddWithValue("@Subscribed_Model", subscription.Subscribed_Model != null ? subscription.Subscribed_Model : String.Empty);
                        cmd.Parameters.AddWithValue("@Subscribed_IMEI", subscription.Subscribed_IMEI != null ? subscription.Subscribed_IMEI : String.Empty);
                        cmd.Parameters.AddWithValue("@Subscribed_ESN", subscription.Subscribed_ESN != null ? subscription.Subscribed_ESN : String.Empty);
                        cmd.Parameters.AddWithValue("@Carrier", subscription.Carrier != null ? subscription.Carrier : String.Empty);
                        cmd.Parameters.AddWithValue("@Action", subscription.Action != null ? subscription.Action : String.Empty);

                        result = DBHelper.GetInt32Value(cmd.ExecuteScalar());
                    }
                }
            }
            catch (Exception ex)
            {
                string message = ex.Message + "\r\n" + ex.StackTrace + "\r\n" +
                    (ex.InnerException != null ? ex.InnerException.Message + "\r\n" + ex.InnerException.StackTrace : String.Empty);
                DBHelper.LogMessage(message);
                error = message;
            }

            return result;
        }
    }
}
