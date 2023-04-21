using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using Newtonsoft.Json;
using Microsoft.Ajax.Utilities;
using System.Web.SessionState;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Text;
using EVSTAR.Models;
using EVSTAR.DB.NET;

namespace EVSTAR.Web
{
    public partial class FileProcess : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            int productId = DBHelper.GetInt32Value(Request.Params["product"]);
            string clientCode = DBHelper.GetStringValue(HttpContext.Current.Request.Params["clientCode"]);
            ClaimHelper ch = new ClaimHelper();
            string errorMsg = string.Empty;
            List<Claim> claims = ch.Select(0, 0, productId, clientCode, out errorMsg);
            if (claims != null && claims.Count > 0)
            {
                Claim currentClaim = claims[0];

                if (claims.Count > 1)
                    foreach (Claim claim in claims)
                    {
                        if (claim.StatusHistory != null && claim.StatusHistory.Count > 0 && claim.StatusHistory[claim.StatusHistory.Count - 1].Status == "OPEN")
                            currentClaim = claim;
                    }

                CustomerHelper custHelper = new CustomerHelper();
                List<Customer> customers = custHelper.Select(string.Empty, string.Empty, String.Empty, currentClaim.CustomerID, 0, clientCode, out errorMsg);
                if (customers != null && customers.Count > 0)
                {
                    if (Request.Files != null && Request.Files.Count > 0)
                    {
                        string filename = String.Empty;
                        filename = @"\" + Request.Files[0].FileName;
                        string ext = filename.Substring(filename.LastIndexOf('.') + 1);
                        ext = ext.ToUpper();
                        if (ext.Equals("PDF") || ext.Equals("JPG") || ext.Equals("PNG") || ext.Equals("JPEG"))
                        {
                            using (BinaryReader sr = new BinaryReader(Request.Files["userfile"].InputStream))
                            {
                                byte[] data = sr.ReadBytes((int)Request.Files["userfile"].ContentLength);
                                using (BinaryWriter sw = new BinaryWriter(System.IO.File.Open(Server.MapPath(@"~\Uploads\") +
                                    $"{customers[0].MobileNumber}_{customers[0].PrimaryLastName}_{claims[0].ID}.{ext}", 
                                    System.IO.FileMode.Create)))
                                {
                                    sw.Write(data);
                                    sw.Flush();
                                    sw.Close();
                                }
                                sr.Close();
                                txtResults.Text = $"File {filename} uploaded successfully.";
                                currentClaim.StatusHistory.Add(new ClaimStatusHistory() { ClaimID = currentClaim.ID, StatusID = 2, StatusDate = DateTime.Now });
                                ch.Update(currentClaim, clientCode, out errorMsg);
                            }
                        }
                        else
                            txtResults.Text = "Invalid file format. Please upload either a PDF, a JPG, or a PNG file.";
                    }
                }
                else
                    txtResults.Text = "Unable to find the associated customer.";
            }
            else
                txtResults.Text = "Unable to find an associated claim.";
        }
    }
}