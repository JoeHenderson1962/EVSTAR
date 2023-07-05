using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace EVSTAR.Models
{
    public class ChargerQuoteRequest
    {
        public int ID { get; set; } = 0;
        public DateTime CreateDateTime { get; set; } = DateTime.Now;
        public DateTime? ProcessedDateTime { get; set; } = null;
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = String.Empty;
        public string Phone { get; set; } = String.Empty;
        public string Email { get; set; } = String.Empty;
        public string Address1 { get; set; } = String.Empty;
        public string Address2 { get; set; } = String.Empty;
        public string City { get; set; } = String.Empty;    
        public string State { get; set; } = String.Empty;
        public string PostalCode { get; set; } = String.Empty;
        public short VehicleYear { get; set; } = 0; 
        public string VehicleMake { get; set; } = String.Empty;
        public string VehicleModel { get; set; } = String.Empty;    
        public string PreferredCharger { get; set; } = String.Empty;
        public DateTime? ElectricCarDeliveryDate { get; set; } = null;
        public short HomeSquareFeet { get; set; } = 0;
        public bool HasAtticAccess { get; set; } = false;
        public string HasBasement { get; set; } = string.Empty;
        public string ElectricUtilityCo { get;set; } = String.Empty;    
        public bool HasGateCode { get; set; } = false;
        public string Parking { get; set; } = string.Empty;
        public string ParkingOrientation { get; set; } = string.Empty;
        public short NumberOfHVAC { get; set; } = 0;
        public string DryerType { get; set; } = string.Empty;
        public string StoveRangeType { get; set; } = string.Empty;  
        public bool HasPool { get; set; } = false;
        public bool HasOutdoorHotTub { get; set; } = false;
        public bool HasSolarPanels { get; set; } = false;
        public bool WantAffordablePayments { get; set; } = false;
        public string AdditionalInfo { get; set; } = string.Empty;
        public string PhotoStreetUrl { get; set; } = string.Empty;
        public string PhotoGarageUrl { get; set; } = string.Empty;      
        public string PhotoGarageInteriorUrl { get; set; } = string.Empty;
        public string PhotoMainPanelUrl { get; set; } = string.Empty;
        public string PhotoCloseUpMainPanelUrl { get; set; } = string.Empty;
        public string PhotoSubpanelUrl { get; set; } = string.Empty;
        public string PhotoCloseUpSubpanelUrl { get; set; } = string.Empty;
        public string PhotoIdealChargerLocationUrl { get; set; } = string.Empty;
        public string SessionID { get; set; } = string.Empty;
        public string SolarPanelSize { get; set; } = string.Empty;
        public string GateCode { get; set; } = string.Empty;
        public string ParkingPosition { get; set; } = string.Empty;
        public bool HasCarCharger { get; set; } = false;

        public ChargerQuoteRequest()
        { 
        }

        public ChargerQuoteRequest(SqlDataReader r)
        {
            ID = DBHelper.GetInt32Value(r["ID"]);
            CreateDateTime = DBHelper.GetDateTimeValue(r["CreateDateTime"]);
            ProcessedDateTime = DBHelper.GetDateTimeValue(r["ProcessedDateTime"]);
            FirstName = DBHelper.GetStringValue(r["FirstName"]);
            LastName = DBHelper.GetStringValue(r["LastName"]);
            Phone = DBHelper.GetStringValue(r["Phone"]);
            Email = DBHelper.GetStringValue(r["Email"]);
            Address1 = DBHelper.GetStringValue(r["Address1"]);
            Address2 = DBHelper.GetStringValue(r["Address2"]);
            City = DBHelper.GetStringValue(r["City"]);
            State = DBHelper.GetStringValue(r["State"]);
            PostalCode = DBHelper.GetStringValue(r["PostalCode"]);
            VehicleYear = DBHelper.GetInt16Value(r["VehicleYear"]);
            VehicleMake = DBHelper.GetStringValue(r["VehicleMake"]);
            VehicleModel = DBHelper.GetStringValue(r["VehicleModel"]);
            PreferredCharger = DBHelper.GetStringValue(r["PreferredCharger"]);
            ElectricCarDeliveryDate = DBHelper.GetDateTimeValue(r["ElectricCarDeliveryDate"]);
            HomeSquareFeet = DBHelper.GetInt16Value(r["HomeSquareFeet"]);
            HasAtticAccess = DBHelper.GetBooleanValue(r["HasAtticAccess"]);
            HasBasement = DBHelper.GetStringValue(r["HasBasement"]);
            ElectricUtilityCo = DBHelper.GetStringValue(r["ElectricUtilityCo"]);
            HasGateCode = DBHelper.GetBooleanValue(r["HasGateCode"]);
            Parking = DBHelper.GetStringValue(r["Parking"]);
            ParkingOrientation = DBHelper.GetStringValue(r["ParkingOrientation"]);
            NumberOfHVAC = DBHelper.GetInt16Value(r["NumberOfHVAC"]);
            DryerType = DBHelper.GetStringValue(r["DryerType"]);
            StoveRangeType = DBHelper.GetStringValue(r["StoveRangeType"]);
            HasPool = DBHelper.GetBooleanValue(r["HasGateCode"]);
            HasOutdoorHotTub = DBHelper.GetBooleanValue(r["HasOutdoorHotTub"]);
            HasSolarPanels = DBHelper.GetBooleanValue(r["HasSolarPanels"]);
            WantAffordablePayments = DBHelper.GetBooleanValue(r["WantAffordablePayments"]);
            AdditionalInfo = DBHelper.GetStringValue(r["AdditionalInfo"]);
            PhotoStreetUrl = DBHelper.GetStringValue(r["PhotoStreetUrl"]);
            PhotoGarageUrl = DBHelper.GetStringValue(r["PhotoGarageUrl"]);
            PhotoGarageInteriorUrl = DBHelper.GetStringValue(r["PhotoGarageInteriorUrl"]);
            PhotoMainPanelUrl = DBHelper.GetStringValue(r["PhotoMainPanelUrl"]);
            PhotoCloseUpMainPanelUrl = DBHelper.GetStringValue(r["PhotoCloseUpMainPanelUrl"]);
            PhotoSubpanelUrl = DBHelper.GetStringValue(r["PhotoSubpanelUrl"]);
            PhotoCloseUpSubpanelUrl = DBHelper.GetStringValue(r["PhotoCloseUpSubpanelUrl"]);
            PhotoIdealChargerLocationUrl = DBHelper.GetStringValue(r["PhotoIdealChargerLocationUrl"]);
            SessionID = DBHelper.GetStringValue(r["SessionID"]);
            SolarPanelSize = DBHelper.GetStringValue(r["SolarPanelSize"]);
            GateCode = DBHelper.GetStringValue(r["GateCode"]);
            ParkingPosition = DBHelper.GetStringValue(r["ParkingPosition"]);
            HasCarCharger = DBHelper.GetBooleanValue(r["HasCarCharger"]);
        }
    }
}
