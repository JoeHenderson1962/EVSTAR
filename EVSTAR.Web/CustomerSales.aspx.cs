using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Newtonsoft.Json;
using System.Configuration;
using EVSTAR.DB;
using EVSTAR.Models;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace EVSTAR.Web
{
    public partial class CustomerSales : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Page.Title = "Admin";
        }

        protected void btnCustomerSales_Click(object sender, EventArgs e)
        {

        }

        protected void btnHornbeamSales_Click(object sender, EventArgs e)
        {
            string errorMsg = string.Empty;
            StringBuilder output = new StringBuilder();
            try
            {
                if (ddlClient.SelectedIndex >= 0)
                {
                    string client = ddlClient.SelectedValue;
                    string constr = ConfigurationManager.ConnectionStrings[client].ConnectionString;
                    using (SqlConnection con = new SqlConnection(constr))
                    {
                        con.Open();
                        StringBuilder sql = new StringBuilder();
                        sql.AppendLine("HornbeamSalesData");
                        sql.AppendLine("WHERE EnrollmentDate BETWEEN @StartDate AND DATEADD(day, 1, @EndDate) ");

                        using (SqlCommand cmd = new SqlCommand(sql.ToString(), con))
                        {
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.Parameters.AddWithValue("@StartDate", calStartDate.SelectedDate);
                            cmd.Parameters.AddWithValue("@StartDate", calEndDate.SelectedDate);

                            SqlDataReader r = cmd.ExecuteReader();
                            while (r.Read())
                            {
                                output.Append("HORN,EVSTAR,EVSTAR,EVSTAR-TECH CYCLE,KS,USA,O,1,A,12,N,,");
                            }
                            r.Close();
                        }
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