using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using EVSTAR.Models;
using System.Configuration;
using Newtonsoft.Json.Linq;

namespace EVSTAR.DB.NET
{
    public class CoveredProductHelper
    {
        public List<CoveredProduct> Select(int id, string clientCode, out string errorMsg)
        {
            List<CoveredProduct> result = new List<CoveredProduct>();

            CustomerHelper customerHelper = new CustomerHelper();
            ProductCategoryHelper productCategoryHelper = new ProductCategoryHelper();
            EquipmentHelper equipmentHelper = new EquipmentHelper();
            
            errorMsg = string.Empty;
            try
            {
                string constr = ConfigurationManager.ConnectionStrings[clientCode].ConnectionString;
                using (SqlConnection con = new SqlConnection(constr))
                {
                    con.Open();
                    StringBuilder sql = new StringBuilder();
                    sql.AppendLine("SELECT cp.*, pc.CategoryName FROM CoveredProducts cp WITH(NOLOCK) ");
                    sql.AppendLine("LEFT JOIN ProductCategories pc WITH(NOLOCK) ON pc.ID = cp.ProductCategoryID ");
                    if (id > 0)
                        sql.AppendLine("WHERE cp.ID=@ID ");

                    sql.AppendLine("ORDER BY cp.ID DESC");

                    using (SqlCommand cmd = new SqlCommand(sql.ToString(), con))
                    {
                        cmd.CommandType = CommandType.Text;
                        if (id > 0)
                            cmd.Parameters.AddWithValue("@ID", id);

                        SqlDataReader r = cmd.ExecuteReader();
                        while (r.Read())
                        {
                            CoveredProduct product = new CoveredProduct(r);
                            ProductCategoryHelper categoryHelper = new ProductCategoryHelper();
                            List<ProductCategory> pcs = categoryHelper.Select(product.ProductCategoryID, clientCode, out errorMsg);
                            if (pcs != null && pcs.Count > 0)
                                product.ProdCategory = pcs[0];
                            result.Add(product);
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

        public CoveredProduct Insert(CoveredProduct product, string clientCode, out string errorMsg)
        {
            errorMsg = string.Empty;
            try
            {
                if (product != null)
                {
                    string constr = ConfigurationManager.ConnectionStrings[clientCode].ConnectionString;
                    using (SqlConnection con = new SqlConnection(constr))
                    {
                        con.Open();
                        StringBuilder sql = new StringBuilder();
                        sql.AppendLine("INSERT INTO CoveredProducts ");
                        sql.AppendLine("(CustomerID, ProductCategoryID, Manufacturer, Model, SerialNumber, IMEI, Color, PurchaseDate, ");
                        sql.AppendLine("CoverageDate, Features, MemorySize, WiFiMobileData, DriveType, ScreenSize, DriveSize, Resolution, ");
                        sql.AppendLine("Processor, YearVersion, Description, RepairShoprAssetID, EquipmentID) ");
                        sql.AppendLine("VALUES ");
                        sql.AppendLine("(@CustomerID, @ProductCategoryID, @Manufacturer, @Model, @SerialNumber, @IMEI, @Color, @PurchaseDate, ");
                        sql.AppendLine("@CoverageDate, @Features, @MemorySize, @WiFiMobileData, @DriveType, @ScreenSize, @DriveSize, @Resolution, ");
                        sql.AppendLine("@Processor, @YearVersion, @Description, @RepairShoprAssetID, @EquipmentID) ");
                        sql.AppendLine("SELECT SCOPE_IDENTITY() ");
                        using (SqlCommand cmd = new SqlCommand(sql.ToString(), con))
                        {
                            cmd.CommandType = CommandType.Text;
                            cmd.Parameters.AddWithValue("@CustomerID", product.CustomerID);
                            cmd.Parameters.AddWithValue("@ProductCategoryID", product.ProductCategoryID);
                            cmd.Parameters.AddWithValue("@Manufacturer", product.Manufacturer);
                            cmd.Parameters.AddWithValue("@Model", product.Model);
                            cmd.Parameters.AddWithValue("@SerialNumber", product.SerialNumber);
                            cmd.Parameters.AddWithValue("@IMEI", product.IMEI);
                            cmd.Parameters.AddWithValue("@Color", product.Color);
                            if (product.PurchaseDate > DateTime.MinValue)
                                cmd.Parameters.AddWithValue("@PurchaseDate", product.PurchaseDate);
                            else
                                cmd.Parameters.AddWithValue("@PurchaseDate", DBNull.Value);
                            if (product.CoverageDate > DateTime.MinValue)
                                cmd.Parameters.AddWithValue("@CoverageDate", product.CoverageDate);
                            else
                                cmd.Parameters.AddWithValue("@CoverageDate", product.CoverageDate);
                            cmd.Parameters.AddWithValue("@Features", product.Features);
                            cmd.Parameters.AddWithValue("@MemorySize", product.MemorySize);
                            cmd.Parameters.AddWithValue("@WiFiMobileData", product.WiFiMobileData);
                            cmd.Parameters.AddWithValue("@DriveType", product.DriveType);
                            cmd.Parameters.AddWithValue("@ScreenSize", product.ScreenSize);
                            cmd.Parameters.AddWithValue("@DriveSize", product.DriveSize);
                            cmd.Parameters.AddWithValue("@Resolution", product.Resolution);
                            cmd.Parameters.AddWithValue("@Processor", product.Processor);
                            cmd.Parameters.AddWithValue("@YearVersion", product.YearVersion);
                            cmd.Parameters.AddWithValue("@Description", product.Description);
                            cmd.Parameters.AddWithValue("@RepairShoprAssetID", product.RepairShoprAssetID);
                            cmd.Parameters.AddWithValue("@EquipmentID", product.EquipmentID);
                            product.ID = DBHelper.GetInt32Value(cmd.ExecuteScalar());
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
            return product;
        }

        public CoveredProduct Update(CoveredProduct product, string clientCode, out string errorMsg)
        {
            errorMsg = string.Empty;
            try
            {
                if (product != null)
                {
                    string constr = ConfigurationManager.ConnectionStrings[clientCode].ConnectionString;
                    using (SqlConnection con = new SqlConnection(constr))
                    {
                        con.Open();
                        StringBuilder sql = new StringBuilder();
                        sql.AppendLine("UPDATE CoveredProducts SET CustomerID=@CustomerID, ProductCategoryID=@ProductCategoryID, Manufacturer=@Manufacturer, ");
                        sql.AppendLine("Model=@Model, SerialNumber=@SerialNumber, IMEI=@IMEI, Color=@Color, PurchaseDate=@PurchaseDate, CoverageDate=@CoverageDate, ");
                        sql.AppendLine("Features=@Features, MemorySize=@MemorySize, WiFiMobileData=@WiFiMobileData, DriveType=@DriveType, ScreenSize=@ScreenSize, ");
                        sql.AppendLine("DriveSize=@DriveSize, Resolution=@Resolution, Processor=@Processor, YearVersion=@YearVersion, Description=@Description, ");
                        sql.AppendLine("RepairShoprAssetID=@RepairShoprAssetID, EquipmentID=@EquipmentID ");
                        sql.AppendLine("WHERE ID=@ID");

                        using (SqlCommand cmd = new SqlCommand(sql.ToString(), con))
                        {
                            cmd.CommandType = CommandType.Text;
                            cmd.Parameters.AddWithValue("@CustomerID", product.CustomerID);
                            cmd.Parameters.AddWithValue("@ProductCategoryID", product.ProductCategoryID);
                            cmd.Parameters.AddWithValue("@Manufacturer", product.Manufacturer);
                            cmd.Parameters.AddWithValue("@Model", product.Model);
                            cmd.Parameters.AddWithValue("@SerialNumber", product.SerialNumber);
                            cmd.Parameters.AddWithValue("@IMEI", product.IMEI);
                            cmd.Parameters.AddWithValue("@Color", product.Color);
                            if (product.PurchaseDate > DateTime.MinValue)
                                cmd.Parameters.AddWithValue("@PurchaseDate", product.PurchaseDate);
                            else
                                cmd.Parameters.AddWithValue("@PurchaseDate", DBNull.Value);
                            if (product.CoverageDate > DateTime.MinValue)
                                cmd.Parameters.AddWithValue("@CoverageDate", product.CoverageDate);
                            else
                                cmd.Parameters.AddWithValue("@CoverageDate", product.CoverageDate);
                            cmd.Parameters.AddWithValue("@Features", product.Features);
                            cmd.Parameters.AddWithValue("@MemorySize", product.MemorySize);
                            cmd.Parameters.AddWithValue("@WiFiMobileData", product.WiFiMobileData);
                            cmd.Parameters.AddWithValue("@DriveType", product.DriveType);
                            cmd.Parameters.AddWithValue("@ScreenSize", product.ScreenSize);
                            cmd.Parameters.AddWithValue("@DriveSize", product.DriveSize);
                            cmd.Parameters.AddWithValue("@Resolution", product.Resolution);
                            cmd.Parameters.AddWithValue("@Processor", product.Processor);
                            cmd.Parameters.AddWithValue("@YearVersion", product.YearVersion);
                            cmd.Parameters.AddWithValue("@Description", product.Description);
                            cmd.Parameters.AddWithValue("@RepairShoprAssetID", product.RepairShoprAssetID);
                            cmd.Parameters.AddWithValue("@EquipmentID", product.EquipmentID);
                            cmd.Parameters.AddWithValue("@ID", product.ID);
                            cmd.ExecuteNonQuery();
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
            return product;
        }
    }
}
