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
    public class ChargerQuoteHelper
    {
        public List<ChargerQuoteRequest> Select(int id, bool unprocessed, out string errorMsg)
        {
            List<ChargerQuoteRequest> result = new List<ChargerQuoteRequest>();
            errorMsg = string.Empty;
            try
            {
                string constr = ConfigurationManager.ConnectionStrings["EVSTAR"].ConnectionString;
                using (SqlConnection con = new SqlConnection(constr))
                {
                    con.Open();
                    StringBuilder sql = new StringBuilder();
                    sql.AppendLine("SELECT * ");
                    sql.AppendLine("FROM ChargerInstallQuoteRequests WITH(NOLOCK) ");
                    if (id > 0)
                        sql.AppendLine("WHERE ID=@ID ");
                    else if (unprocessed)
                        sql.AppendLine("WHERE ProcessedDateTime IS NULL ");

                    using (SqlCommand cmd = new SqlCommand(sql.ToString(), con))
                    {
                        cmd.CommandType = CommandType.Text;
                        if (id > 0)
                            cmd.Parameters.AddWithValue("@ID", id);


                        SqlDataReader r = cmd.ExecuteReader();
                        while (r.Read())
                        {
                            ChargerQuoteRequest cqr = new ChargerQuoteRequest(r);
                            result.Add(cqr);   
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

        public ChargerQuoteRequest Insert(ChargerQuoteRequest cqr, out string errorMsg)
        {

            errorMsg = string.Empty;
            try
            {
                if (cqr != null)
                {
                    string constr = ConfigurationManager.ConnectionStrings["EVSTAR"].ConnectionString;
                    using (SqlConnection con = new SqlConnection(constr))
                    {
                        con.Open();
                        StringBuilder sql = new StringBuilder();
                        sql.AppendLine("INSERT INTO ChargerInstallQuoteRequests ");
                        sql.AppendLine("(CreateDateTime, ProcessedDateTime, FirstName, LastName, Phone, Email, Address1, Address2, City, ");
                        sql.AppendLine("State, PostalCode, VehicleYear, VehicleMake, VehicleModel, PreferredCharger, ElectricCarDeliveryDate, ");
                        sql.AppendLine("HomeSquareFeet, HasAtticAccess, HasBasement, ElectricUtilityCo, HasGateCode, Parking, ParkingOrientation, ");
                        sql.AppendLine("NumberOfHVAC, DryerType, StoveRangeType, HasPool, HasOutdoorHotTub, HasSolarPanels, WantAffordablePayments, ");
                        sql.AppendLine("AdditionalInfo, PhotoStreetUrl, PhotoGarageUrl, PhotoGarageInteriorUrl, PhotoMainPanelUrl, ");
                        sql.AppendLine("PhotoCloseUpMainPanelUrl, PhotoSubpanelUrl, PhotoCloseUpSubpanelUrl, PhotoIdealChargerLocationUrl, SessionID, ");
                        sql.AppendLine("HasCarCharger, GateCode, ParkingPosition, SolarPanelSize");
                        sql.AppendLine(") VALUES ");
                        sql.AppendLine("(@CreateDateTime, @ProcessedDateTime, @FirstName, @LastName, @Phone, @Email, @Address1, @Address2, @City, ");
                        sql.AppendLine("@State, @PostalCode, @VehicleYear, @VehicleMake, @VehicleModel, @PreferredCharger, @ElectricCarDeliveryDate, ");
                        sql.AppendLine("@HomeSquareFeet, @HasAtticAccess, @HasBasement, @ElectricUtilityCo, @HasGateCode, @Parking, @ParkingOrientation, ");
                        sql.AppendLine("@NumberOfHVAC, @DryerType, @StoveRangeType, @HasPool, @HasOutdoorHotTub, @HasSolarPanels, @WantAffordablePayments, ");
                        sql.AppendLine("@AdditionalInfo, @PhotoStreetUrl, @PhotoGarageUrl, @PhotoGarageInteriorUrl, @PhotoMainPanelUrl, ");
                        sql.AppendLine("@PhotoCloseUpMainPanelUrl, @PhotoSubpanelUrl, @PhotoCloseUpSubpanelUrl, @PhotoIdealChargerLocationUrl, @SessionID, ");
                        sql.AppendLine("@HasCarCharger, @GateCode, @ParkingPosition, @SolarPanelSize);");
                        sql.AppendLine("SELECT SCOPE_IDENTITY(); ");
                        using (SqlCommand cmd = new SqlCommand(sql.ToString(), con))
                        {
                            cmd.CommandType = CommandType.Text;
                            cmd.Parameters.AddWithValue("@CreateDateTime", cqr.CreateDateTime);
                            cmd.Parameters.AddWithValue("@ProcessedDateTime", DBNull.Value);
                            cmd.Parameters.AddWithValue("@FirstName", cqr.FirstName);
                            cmd.Parameters.AddWithValue("@LastName", cqr.LastName);
                            cmd.Parameters.AddWithValue("@Phone", cqr.Phone);
                            cmd.Parameters.AddWithValue("@Email", cqr.Email);
                            cmd.Parameters.AddWithValue("@Address1", cqr.Address1);
                            cmd.Parameters.AddWithValue("@Address2", cqr.Address2);
                            cmd.Parameters.AddWithValue("@City", cqr.City);
                            cmd.Parameters.AddWithValue("@State", cqr.State);
                            cmd.Parameters.AddWithValue("@PostalCode", cqr.PostalCode);
                            cmd.Parameters.AddWithValue("@VehicleYear", cqr.VehicleYear);
                            cmd.Parameters.AddWithValue("@VehicleMake", cqr.VehicleMake);
                            cmd.Parameters.AddWithValue("@VehicleModel", cqr.VehicleModel);
                            cmd.Parameters.AddWithValue("@PreferredCharger", cqr.PreferredCharger);
                            cmd.Parameters.AddWithValue("@ElectricCarDeliveryDate", cqr.ElectricCarDeliveryDate);
                            cmd.Parameters.AddWithValue("@HomeSquareFeet", cqr.HomeSquareFeet);
                            cmd.Parameters.AddWithValue("@HasAtticAccess", cqr.HasAtticAccess);
                            cmd.Parameters.AddWithValue("@HasBasement", cqr.HasBasement);
                            cmd.Parameters.AddWithValue("@ElectricUtilityCo", cqr.ElectricUtilityCo);
                            cmd.Parameters.AddWithValue("@HasGateCode", cqr.HasGateCode);
                            cmd.Parameters.AddWithValue("@Parking", cqr.Parking);
                            cmd.Parameters.AddWithValue("@ParkingOrientation", cqr.ParkingOrientation);
                            cmd.Parameters.AddWithValue("@NumberOfHVAC", cqr.NumberOfHVAC);
                            cmd.Parameters.AddWithValue("@DryerType", cqr.DryerType);
                            cmd.Parameters.AddWithValue("@StoveRangeType", cqr.StoveRangeType);
                            cmd.Parameters.AddWithValue("@HasPool", cqr.HasPool);
                            cmd.Parameters.AddWithValue("@HasOutdoorHotTub", cqr.HasOutdoorHotTub);
                            cmd.Parameters.AddWithValue("@HasSolarPanels", cqr.HasSolarPanels);
                            cmd.Parameters.AddWithValue("@WantAffordablePayments", cqr.WantAffordablePayments);
                            cmd.Parameters.AddWithValue("@AdditionalInfo", cqr.AdditionalInfo);
                            cmd.Parameters.AddWithValue("@PhotoStreetUrl", cqr.PhotoStreetUrl);
                            cmd.Parameters.AddWithValue("@PhotoGarageUrl", cqr.PhotoGarageUrl);
                            cmd.Parameters.AddWithValue("@PhotoGarageInteriorUrl", cqr.PhotoGarageInteriorUrl);
                            cmd.Parameters.AddWithValue("@PhotoMainPanelUrl", cqr.PhotoMainPanelUrl);
                            cmd.Parameters.AddWithValue("@PhotoCloseUpMainPanelUrl", cqr.PhotoCloseUpMainPanelUrl);
                            cmd.Parameters.AddWithValue("@PhotoSubpanelUrl", cqr.PhotoSubpanelUrl);
                            cmd.Parameters.AddWithValue("@PhotoCloseUpSubpanelUrl", cqr.PhotoCloseUpSubpanelUrl);
                            cmd.Parameters.AddWithValue("@PhotoIdealChargerLocationUrl", cqr.PhotoIdealChargerLocationUrl);
                            cmd.Parameters.AddWithValue("@SessionID", cqr.SessionID);
                            cmd.Parameters.AddWithValue("@HasCarCharger", cqr.HasCarCharger);
                            cmd.Parameters.AddWithValue("@GateCode", cqr.GateCode);
                            cmd.Parameters.AddWithValue("@ParkingPosition", cqr.ParkingPosition);
                            cmd.Parameters.AddWithValue("@SolarPanelSize", cqr.SolarPanelSize);
                            cqr.ID = DBHelper.GetInt32Value(cmd.ExecuteScalar());
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
            return cqr;
        }


        public ChargerQuoteRequest Update(ChargerQuoteRequest cqr, out string errorMsg)
        {

            errorMsg = string.Empty;
            try
            {
                if (cqr != null)
                {
                    string constr = ConfigurationManager.ConnectionStrings["EVSTAR"].ConnectionString;
                    using (SqlConnection con = new SqlConnection(constr))
                    {
                        con.Open();
                        StringBuilder sql = new StringBuilder();
                        sql.AppendLine("UPDATE ChargerInstallQuoteRequests SET ");
                        sql.AppendLine("CreateDateTime=@CreateDateTime, ProcessedDateTime=@ProcessedDateTime, FirstName=@FirstName, LastName=@LastName, ");
                        sql.AppendLine("Phone=@Phone, Email=@Email, Address1=@Address1, Address2=@Address2, City=@City, State=@State, ");
                        sql.AppendLine("PostalCode=@PostalCode, VehicleYear=@VehicleYear, VehicleMake=@VehicleMake, VehicleModel=@VehicleModel, ");
                        sql.AppendLine("PreferredCharger=@PreferredCharger, ElectricCarDeliveryDate=@ElectricCarDeliveryDate, HomeSquareFeet=@HomeSquareFeet, ");
                        sql.AppendLine("HasAtticAccess=@HasAtticAccess, HasBasement=@HasBasement, ElectricUtilityCo=@ElectricUtilityCo, HasGateCode=@HasGateCode, ");
                        sql.AppendLine("Parking=@Parking, ParkingOrientation=@ParkingOrientation, NumberOfHVAC=@NumberOfHVAC, DryerType=@DryerType, ");
                        sql.AppendLine("StoveRangeType=@StoveRangeType, HasPool=@HasPool, HasOutdoorHotTub=@HasOutdoorHotTub, HasSolarPanels=@HasSolarPanels, ");
                        sql.AppendLine("WantAffordablePayments=@WantAffordablePayments, AdditionalInfo=@AdditionalInfo, PhotoStreetUrl=@PhotoStreetUrl, ");
                        sql.AppendLine("PhotoGarageUrl=@PhotoGarageUrl, PhotoGarageInteriorUrl=@PhotoGarageInteriorUrl, PhotoMainPanelUrl=@PhotoMainPanelUrl, ");
                        sql.AppendLine("PhotoCloseUpMainPanelUrl=@PhotoCloseUpMainPanelUrl, PhotoSubpanelUrl=@PhotoSubpanelUrl, PhotoCloseUpSubpanelUrl=@PhotoCloseUpSubpanelUrl, ");
                        sql.AppendLine("PhotoIdealChargerLocationUrl=@PhotoIdealChargerLocationUrl, SessionID=@SessionID, HasCarCharger=@HasCarCharger, ");
                        sql.AppendLine("SolarPanelSize=@SolarPanelSize, GateCode=@GateCode, ParkingPosition=@ParkingPosition ");
                        sql.AppendLine("WHERE ID=@ID");
                        using (SqlCommand cmd = new SqlCommand(sql.ToString(), con))
                        {
                            cmd.CommandType = CommandType.Text;
                            cmd.Parameters.AddWithValue("ID", cqr.ID);
                            cmd.Parameters.AddWithValue("@CreateDateTime", cqr.CreateDateTime);
                            cmd.Parameters.AddWithValue("@ProcessedDateTime", cqr.ProcessedDateTime);
                            cmd.Parameters.AddWithValue("@FirstName", cqr.FirstName);
                            cmd.Parameters.AddWithValue("@LastName", cqr.LastName);
                            cmd.Parameters.AddWithValue("@Phone", cqr.Phone);
                            cmd.Parameters.AddWithValue("@Email", cqr.Email);
                            cmd.Parameters.AddWithValue("@Address1", cqr.Address1);
                            cmd.Parameters.AddWithValue("@Address2", cqr.Address2);
                            cmd.Parameters.AddWithValue("@City", cqr.City);
                            cmd.Parameters.AddWithValue("@State", cqr.State);
                            cmd.Parameters.AddWithValue("@PostalCode", cqr.PostalCode);
                            cmd.Parameters.AddWithValue("@VehicleYear", cqr.VehicleYear);
                            cmd.Parameters.AddWithValue("@VehicleMake", cqr.VehicleMake);
                            cmd.Parameters.AddWithValue("@VehicleModel", cqr.VehicleModel);
                            cmd.Parameters.AddWithValue("@PreferredCharger", cqr.PreferredCharger);
                            cmd.Parameters.AddWithValue("@ElectricCarDeliveryDate", cqr.ElectricCarDeliveryDate);
                            cmd.Parameters.AddWithValue("@HomeSquareFeet", cqr.HomeSquareFeet);
                            cmd.Parameters.AddWithValue("@HasAtticAccess", cqr.HasAtticAccess);
                            cmd.Parameters.AddWithValue("@HasBasement", cqr.HasBasement);
                            cmd.Parameters.AddWithValue("@ElectricUtilityCo", cqr.ElectricUtilityCo);
                            cmd.Parameters.AddWithValue("@HasGateCode", cqr.HasGateCode);
                            cmd.Parameters.AddWithValue("@Parking", cqr.Parking);
                            cmd.Parameters.AddWithValue("@ParkingOrientation", cqr.ParkingOrientation);
                            cmd.Parameters.AddWithValue("@NumberOfHVAC", cqr.NumberOfHVAC);
                            cmd.Parameters.AddWithValue("@DryerType", cqr.DryerType);
                            cmd.Parameters.AddWithValue("@StoveRangeType", cqr.StoveRangeType);
                            cmd.Parameters.AddWithValue("@HasPool", cqr.HasPool);
                            cmd.Parameters.AddWithValue("@HasOutdoorHotTub", cqr.HasOutdoorHotTub);
                            cmd.Parameters.AddWithValue("@HasSolarPanels", cqr.HasSolarPanels);
                            cmd.Parameters.AddWithValue("@WantAffordablePayments", cqr.WantAffordablePayments);
                            cmd.Parameters.AddWithValue("@AdditionalInfo", cqr.AdditionalInfo);
                            cmd.Parameters.AddWithValue("@PhotoStreetUrl", cqr.PhotoStreetUrl);
                            cmd.Parameters.AddWithValue("@PhotoGarageUrl", cqr.PhotoGarageUrl);
                            cmd.Parameters.AddWithValue("@PhotoGarageInteriorUrl", cqr.PhotoGarageInteriorUrl);
                            cmd.Parameters.AddWithValue("@PhotoMainPanelUrl", cqr.PhotoMainPanelUrl);
                            cmd.Parameters.AddWithValue("@PhotoCloseUpMainPanelUrl", cqr.PhotoCloseUpMainPanelUrl);
                            cmd.Parameters.AddWithValue("@PhotoSubpanelUrl", cqr.PhotoSubpanelUrl);
                            cmd.Parameters.AddWithValue("@PhotoCloseUpSubpanelUrl", cqr.PhotoCloseUpSubpanelUrl);
                            cmd.Parameters.AddWithValue("@PhotoIdealChargerLocationUrl", cqr.PhotoIdealChargerLocationUrl);
                            cmd.Parameters.AddWithValue("@SessionID", cqr.SessionID);
                            cmd.Parameters.AddWithValue("@HasCarCharger", cqr.HasCarCharger);
                            cmd.Parameters.AddWithValue("@GateCode", cqr.GateCode);
                            cmd.Parameters.AddWithValue("@ParkingPosition", cqr.ParkingPosition);
                            cmd.Parameters.AddWithValue("@SolarPanelSize", cqr.SolarPanelSize);
                            cqr.ID = DBHelper.GetInt32Value(cmd.ExecuteScalar());
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
            return cqr;
        }
    }
}
