using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using System.Web.UI;
using System.Web.UI.WebControls;
using EVSTAR.Models;
using System.Data.Sql;
using System.Data.SqlClient;
using System.Net;
using EVSTAR.DB.NET;
using System.Web.Management;

namespace EVSTAR.Web
{
    public partial class ChargerQuote : Page
    {
        private Dictionary<string, HashSet<string>> vehicleDict = new Dictionary<string, HashSet<string>>(); 
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (String.IsNullOrEmpty(hidSessionID.Value))
                    hidSessionID.Value = Guid.NewGuid().ToString();

                int startYear = DateTime.Now.Year + 2;
                int endYear = 2010;

                ddlCarYear.Items.Clear();
                for (int i = startYear; i >= endYear; i--)
                {
                    ddlCarYear.Items.Add(new ListItem(i.ToString(), i.ToString()));
                }

                LoadVehicleDictionaries();

                ddlCarMake.Items.Clear();
                foreach (string key in vehicleDict.Keys)
                {
                    ddlCarMake.Items.Add(new ListItem(key, key));
                }
                ddlCarMake.SelectedIndex = 0;

                ddlCarModel.Items.Clear();
                if (vehicleDict.ContainsKey(ddlCarMake.SelectedItem.Value))
                {
                    HashSet<string> models = vehicleDict[ddlCarMake.SelectedItem.Value];
                    foreach (string val in models)
                    {
                        ddlCarModel.Items.Add(new ListItem(val, val));
                    }
                }
                divTesla.Style.Add("display", "none");
                divNonTesla.Style.Add("display", "inline-block");
            }
            else
            {
            }
        }

        private void LoadVehicleDictionaries()
        {
            string errorMsg = "";
            VehicleHelper vh = new VehicleHelper();
            List<Vehicle> vehicles = vh.Select(0, "EVSTAR", out errorMsg);

            string lastMake = "";
            HashSet<string> tmp = new HashSet<string>();
            foreach (Vehicle vehicle in vehicles)
            {
                if (vehicle.Make != lastMake)
                {
                    if (!String.IsNullOrEmpty(lastMake))
                        vehicleDict.Add(lastMake, tmp);
                    lastMake = vehicle.Make;
                    tmp = new HashSet<string>();
                }

                tmp.Add(vehicle.ModelName);
            }
            if (!String.IsNullOrEmpty(lastMake))
                vehicleDict.Add(lastMake, tmp);

        }
        //protected void btnSubmitQuoteRequest_Click(object sender, EventArgs e)
        //{
        //    lblError2.Text = string.Empty;
        //    divError2.Visible = false;

        //    string folder = Server.MapPath("~/Uploads/");
        //    ChargerQuoteRequest cq = new ChargerQuoteRequest();
        //    cq.SessionID = hidSessionID.Value;
        //    cq.AdditionalInfo = txtAdditionalInformation.Text;
        //    cq.Address1 = txtStreetAddress1.Text;
        //    cq.Address2 = txtStreetAddress2.Text;
        //    cq.City = txtCity.Text;
        //    cq.CreateDateTime = DateTime.Now;
        //    cq.DryerType = ddlDryerType.Text;
        //    cq.ElectricCarDeliveryDate = calCarDeliveryDate.SelectedDate;
        //    cq.ElectricUtilityCo = txtElectricCompany.Text;
        //    cq.Email = txtEmail.Text;
        //    cq.FirstName = txtFirstName.Text;
        //    cq.LastName = txtLastName.Text;
        //    cq.HasAtticAccess = ddlAtticAccess.SelectedItem.Value.ToUpper() == "YES";
        //    cq.HasBasement = ddlHasBasement.SelectedItem.Value;
        //    cq.HasGateCode = ddlHasGateCode.SelectedItem.Value.ToUpper() == "YES";
        //    cq.GateCode = txtGateCode.Text.Trim();
        //    cq.HasOutdoorHotTub = ddlHasHotTub.SelectedItem.Value.ToUpper() == "YES";
        //    cq.HasPool = ddlHasPool.SelectedItem.Value.ToUpper() == "YES";
        //    cq.HasSolarPanels = ddlHasSolarPanels.SelectedItem.Value.ToUpper() == "YES";
        //    cq.HomeSquareFeet = DBHelper.GetInt16Value(txtHomeSquareFeet.Text);
        //    cq.NumberOfHVAC = DBHelper.GetInt16Value(ddlHVACUnits.SelectedItem.Value);
        //    cq.Parking = ddlParking.SelectedItem.Value;
        //    cq.ParkingOrientation = ddlParkingOrientation.SelectedItem.Value;
        //    cq.Phone = txtPhoneNumber.Text;
        //    cq.PostalCode = txtPostalCode.Text;
        //    cq.State = ddlState.SelectedItem.Value;
        //    cq.StoveRangeType = ddlStoveType.SelectedItem.Value;
        //    cq.VehicleMake = ddlCarMake.SelectedItem.Value;
        //    cq.VehicleModel = ddlCarModel.SelectedItem.Value;
        //    cq.VehicleYear = DBHelper.GetInt16Value(ddlCarYear.SelectedItem.Value);
        //    //cq.WantAffordablePayments = ddlAffordablePayments.SelectedItem.Value == "YES";
        //    cq.PreferredCharger = ddlPreferredCharger.SelectedItem.Value;
        //    cq.SolarPanelSize = txtSolarSize.Text;
        //    cq.ParkingPosition = ddlPreferredSpot.SelectedItem.Value;

        //    if (!String.IsNullOrEmpty(uplHomeFromStreet.PostedFile.FileName))
        //    {
        //        if (uplHomeFromStreet.PostedFile.ContentLength > 52428800)
        //            lblError2.Text = "File " + uplHomeFromStreet.PostedFile.FileName + " is too large.";

        //        cq.PhotoStreetUrl = cq.SessionID + "_" + uplHomeFromStreet.PostedFile.FileName;
        //        uplHomeFromStreet.PostedFile.SaveAs(folder + cq.SessionID + "_" + cq.PhotoStreetUrl);
        //    }

        //    if (!String.IsNullOrEmpty(uplMainElectricalPanelCloseUp.PostedFile.FileName))
        //    {
        //        if (uplMainElectricalPanelCloseUp.PostedFile.ContentLength > 52428800)
        //            lblError2.Text = "File " + uplMainElectricalPanelCloseUp.PostedFile.FileName + " is too large.";

        //        cq.PhotoCloseUpMainPanelUrl = cq.SessionID + "_" + uplMainElectricalPanelCloseUp.PostedFile.FileName;
        //        uplMainElectricalPanelCloseUp.PostedFile.SaveAs(folder + cq.SessionID + "_" + cq.PhotoCloseUpMainPanelUrl);
        //    }

        //    if (!String.IsNullOrEmpty(uplElectricalSubPanelCloseUp.PostedFile.FileName))
        //    {
        //        if (uplElectricalSubPanelCloseUp.PostedFile.ContentLength > 52428800)
        //            lblError2.Text = "File " + uplElectricalSubPanelCloseUp.PostedFile.FileName + " is too large.";

        //        cq.PhotoCloseUpSubpanelUrl = cq.SessionID + "_" + uplElectricalSubPanelCloseUp.PostedFile.FileName;
        //        uplElectricalSubPanelCloseUp.PostedFile.SaveAs(folder + cq.SessionID + "_" + cq.PhotoCloseUpSubpanelUrl);
        //    }

        //    if (!String.IsNullOrEmpty(uplElectricalSubPanel.PostedFile.FileName))
        //    {
        //        if (uplElectricalSubPanel.PostedFile.ContentLength > 52428800)
        //            lblError2.Text = "File " + uplElectricalSubPanel.PostedFile.FileName + " is too large.";

        //        cq.PhotoSubpanelUrl = cq.SessionID + "_" + uplElectricalSubPanel.PostedFile.FileName;
        //        uplElectricalSubPanel.PostedFile.SaveAs(folder + cq.SessionID + "_" + cq.PhotoSubpanelUrl);
        //    }

        //    if (!String.IsNullOrEmpty(uplGarageInterior.PostedFile.FileName))
        //    {
        //        if (uplGarageInterior.PostedFile.ContentLength > 52428800)
        //            lblError2.Text = "File " + uplGarageInterior.PostedFile.FileName + " is too large.";

        //        cq.PhotoGarageInteriorUrl = cq.SessionID + "_" + uplGarageInterior.PostedFile.FileName;
        //        uplGarageInterior.PostedFile.SaveAs(folder + cq.SessionID + "_" + cq.PhotoGarageInteriorUrl);
        //    }

        //    if (!String.IsNullOrEmpty(uplGarage.PostedFile.FileName))
        //    {
        //        if (uplGarage.PostedFile.ContentLength > 52428800)
        //            lblError2.Text = "File " + uplGarage.PostedFile.FileName + " is too large.";

        //        cq.PhotoGarageUrl = cq.SessionID + "_" + uplGarage.PostedFile.FileName;
        //        uplGarage.PostedFile.SaveAs(folder + cq.SessionID + "_" + cq.PhotoGarageUrl);
        //    }

        //    if (!String.IsNullOrEmpty(uplIdealChargerLocation.PostedFile.FileName))
        //    {
        //        if (uplIdealChargerLocation.PostedFile.ContentLength > 52428800)
        //            lblError2.Text = "File " + uplIdealChargerLocation.PostedFile.FileName + " is too large.";

        //        cq.PhotoIdealChargerLocationUrl = cq.SessionID + "_" + uplIdealChargerLocation.PostedFile.FileName;
        //        uplIdealChargerLocation.PostedFile.SaveAs(folder + cq.SessionID + "_" + cq.PhotoIdealChargerLocationUrl);
        //    }

        //    if (!String.IsNullOrEmpty(uplMainElectricalPanel.PostedFile.FileName))
        //    {
        //        if (uplMainElectricalPanel.PostedFile.ContentLength > 52428800)
        //            lblError2.Text = "File " + uplMainElectricalPanel.PostedFile.FileName + " is too large.";

        //        cq.PhotoMainPanelUrl = cq.SessionID + "_" + uplMainElectricalPanel.PostedFile.FileName;
        //        uplMainElectricalPanel.PostedFile.SaveAs(folder + cq.SessionID + "_" + cq.PhotoMainPanelUrl);
        //    }
        //    lblError2.Text = string.Empty;

        //    List<string> errors = ValidateData(cq);
        //    if (errors.Count == 0)
        //    {
        //        ChargerQuoteHelper cqh = new ChargerQuoteHelper();
        //        string errorMsg;
        //        cq = cqh.Insert(cq, out errorMsg);
        //        if (!String.IsNullOrEmpty(errorMsg))
        //        {
        //            lblError2.Text = errorMsg;
        //            divError2.Visible = true;
        //            //divContent.Visible = false;
        //            //divFooter.Visible = false;
        //        }
        //        else if (cq.ID > 0)
        //        {
        //            lblSuccess.Text = "Your request has been submitted.";
        //            divSuccess.Visible = true;
        //            //divContent.Visible = false;
        //            //divFooter.Visible = true;
        //            txtAdditionalInformation.Text = string.Empty;
        //            txtCity.Text = string.Empty;
        //            txtDealership.Text = string.Empty;
        //            txtDealershipAgent.Text = string.Empty;
        //            txtElectricCompany.Text = string.Empty;
        //            txtEmail.Text = string.Empty;
        //            txtFirstName.Text = string.Empty;
        //            txtHomeSquareFeet.Text = string.Empty;
        //            txtLastName.Text = string.Empty;
        //            txtPhoneNumber.Text = string.Empty;
        //            txtPostalCode.Text = string.Empty;
        //            txtStreetAddress1.Text = string.Empty;
        //            txtStreetAddress2.Text = string.Empty;
        //            //ddlAffordablePayments.SelectedIndex = 0;
        //            ddlAtticAccess.SelectedIndex = 0;
        //            ddlCarMake.SelectedIndex = 0;
        //            ddlCarModel.SelectedIndex = 0;
        //            ddlCarYear.SelectedIndex = 0;
        //            ddlDryerType.SelectedIndex = 0;
        //            ddlHasBasement.SelectedIndex = 0;
        //            ddlHasGateCode.SelectedIndex = 0;
        //            ddlHasHotTub.SelectedIndex = 0;
        //            ddlHasPool.SelectedIndex = 0;
        //            ddlHasSolarPanels.SelectedIndex = 0;
        //            ddlHaveCarCharger.SelectedIndex = 0;
        //            ddlHVACUnits.SelectedIndex = 0;
        //            ddlParking.SelectedIndex = 0;
        //            ddlParkingOrientation.SelectedIndex = 0;
        //            ddlPreferredCharger.SelectedIndex = 0;
        //            ddlState.SelectedIndex = 0;
        //            ddlStoveType.SelectedIndex = 0;
        //        }
        //    }
        //    else
        //    {
        //        lblError2.Text = "Please make sure all items marked with an asterisk (*) are populated. The following are missing: ";
        //        int count = 0;
        //        foreach (string error in errors)
        //        {
        //            lblError2.Text += error;
        //            count++;
        //            if (count < errors.Count)
        //                lblError2.Text += ", ";
        //        }
        //        divError2.Visible = true;
        //        //divContent.Visible = false;
        //        //divFooter.Visible = false;
        //    }
        //}

        //protected void ddlCarMake_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    LoadVehicleDictionaries();

        //    if (vehicleDict.ContainsKey(ddlCarMake.SelectedItem.Value))
        //    {
        //        HashSet<string> models = vehicleDict[ddlCarMake.SelectedItem.Value];
        //        ddlCarModel.Items.Clear();
        //        foreach (string val in models)
        //        {
        //            ddlCarModel.Items.Add(new ListItem(val, val));
        //        }

        //        if (ddlCarMake.SelectedItem.Value.ToUpper() == "TESLA")
        //        {
        //            divTesla.Style.Add("display", "inline-block");
        //            divNonTesla.Style.Add("display", "none");
        //        }
        //        else
        //        {
        //            divNonTesla.Style.Add("display", "inline-block");
        //            divTesla.Style.Add("display", "none");
        //        }
        //    }
        //}

        protected void btnErrorOK_Click(object sender, EventArgs e)
        {
            divError2.Visible = false;
            //divContent.Visible = true;
            divSuccess.Visible = false;
            //divFooter.Visible = true;
        }
    }
}