using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Web.Http;
using System.Configuration;
using System.Text;
using Newtonsoft.Json;
using EVSTAR.Models;
using EVSTAR.Models.FedEx;
using System.Net.Mail;
using System.IO;
using System.Web.Services.Protocols;
using System.Net;
using RestSharp;

namespace ERPS.api
{
    public class FedexController : ApiController
    {
        Random random = new Random();

        // GET api/<controller>
        public string Get()
        {
            string result = string.Empty;
            string code = DBHelper.GetStringValue(HttpContext.Current.Request.Params["code"]);
            string address = DBHelper.GetStringValue(HttpContext.Current.Request.Params["address"]);
            string hashed = DBHelper.GetStringValue(HttpContext.Current.Request.Params["hashed"]);
            string provided = Encryption.MD5(code + address);
            return provided == hashed ? "TRUE" : "FALSE";
        }

        // POST api/<controller>
        public string Post([FromBody] string value)
        {
            StringBuilder sb = new StringBuilder();
            string result = string.Empty;
            string code = DBHelper.GetStringValue(HttpContext.Current.Request.Params["code"]);
            string address = DBHelper.GetStringValue(HttpContext.Current.Request.Params["address"]);
            string phone = DBHelper.GetStringValue(HttpContext.Current.Request.Params["phone"]);
            string hashed = DBHelper.GetStringValue(HttpContext.Current.Request.Params["hashed"]);
            string provided = Encryption.MD5(code + address);
            if (hashed != provided)
            {
                provided = Encryption.MD5(code + phone);
                if (hashed != provided)
                {
                    result = "Authentication error";
                    return result;
                }
            }

            try
            {
                string bearerToken = GetBearerToken();

                result = CreateShipment(bearerToken);
                return result;
            }
            catch (Exception ex)
            {
                result = ex.Message;
            }
            return result;
        }

        private string GetBearerToken()
        {
            string bearerToken = string.Empty;
            string clientId = ConfigurationManager.AppSettings["FedexKey"];
            string clientSecret = ConfigurationManager.AppSettings["FedexPassword"];
            string fedexUrl = ConfigurationManager.AppSettings["FedexURL"];
            try
            {
                var client = new RestClient(String.Format("{0}oauth/token", fedexUrl));
                var request = new RestRequest(String.Format("{0}oauth/token", fedexUrl), Method.Post);
                request.AddHeader("X-locale", "en_US");
                request.AddHeader("Content-Type", "application/x-www-form-urlencoded");
                // Content type is not required when adding parameters this way
                // This will also automatically UrlEncode the values
                request.AddParameter("grant_type", "client_credentials", ParameterType.GetOrPost);
                request.AddParameter("client_id", clientId, ParameterType.GetOrPost);
                request.AddParameter("client_secret", clientSecret, ParameterType.GetOrPost);
                RestResponse response = client.Execute(request);

                System.Diagnostics.Debug.WriteLine(response.Content);
                OAuthResponse resp = JsonConvert.DeserializeObject<OAuthResponse>(response.Content);
                bearerToken = resp.access_token;
            }
            catch (WebException ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
                return ex.Message;
                //System.Diagnostics.Debug.WriteLine(new StreamReader(ex.Response.GetResponseStream()).ReadToEnd()); ;
            }

            return bearerToken;
        }

        private string CreateShipment(string bearerToken)
        {
            string url = string.Empty;
            string name = DBHelper.GetStringValue(HttpContext.Current.Request.Params["name"]);
            string company = DBHelper.GetStringValue(HttpContext.Current.Request.Params["company"]);
            string address1 = DBHelper.GetStringValue(HttpContext.Current.Request.Params["address1"]);
            string address2 = DBHelper.GetStringValue(HttpContext.Current.Request.Params["address2"]);
            string city = DBHelper.GetStringValue(HttpContext.Current.Request.Params["city"]);
            string state = DBHelper.GetStringValue(HttpContext.Current.Request.Params["state"]);
            string postalCode = DBHelper.GetStringValue(HttpContext.Current.Request.Params["postalCode"]);
            string country = DBHelper.GetStringValue(HttpContext.Current.Request.Params["country"]);
            string contactphone = DBHelper.GetStringValue(HttpContext.Current.Request.Params["contactphone"]);
            string residential = DBHelper.GetStringValue(HttpContext.Current.Request.Params["residential"]);
            string fedexUrl = ConfigurationManager.AppSettings["FedexURL"];
            try
            {
                Shipment shipment = new Shipment();
                shipment.accountNumber = new AccountNumber()
                {
                    value = ConfigurationManager.AppSettings["FedexAccountNumber"]
                };
                shipment.labelResponseOptions = "URL_ONLY";
                shipment.requestedShipment = new RequestedShipment();
                //shipment.requestedShipment.shipmentSpecialServices =
                //new ShipmentSpecialServices();
                //shipment.requestedShipment.shipmentSpecialServices.pendingShipmentDetail = new PendingShipmentDetail();
                //shipment.requestedShipment.shipmentSpecialServices.pendingShipmentDetail.pendingShipmentType = "";
                //shipment.requestedShipment.shipmentSpecialServices.specialServiceTypes = new List<string>();

                shipment.requestedShipment.shipDatestamp = DateTime.Now.ToString("yyyy-MM-dd");
                shipment.requestedShipment.labelSpecification = new LabelSpecification();
                shipment.requestedShipment.labelSpecification.labelOrder = "SHIPPING_LABEL_FIRST";
                shipment.requestedShipment.labelSpecification.labelStockType = "PAPER_85X11_TOP_HALF_LABEL";
                shipment.requestedShipment.labelSpecification.labelFormatType = "COMMON2D";
                shipment.requestedShipment.labelSpecification.imageType = "PDF";
                shipment.requestedShipment.requestedPackageLineItems = new List<RequestedPackageLineItem>();
                shipment.requestedShipment.requestedPackageLineItems.Add(new RequestedPackageLineItem()
                {
                    weight = new Weight()
                    {
                        units = "KG",
                        value = 2
                    }
                });
                shipment.requestedShipment.blockInsightVisibility = false;
                shipment.requestedShipment.packagingType = "YOUR_PACKAGING";
                shipment.requestedShipment.pickupType = (residential == "true" ? "DROPOFF_AT_FEDEX_LOCATION" : "USE_SCHEDULED_PICKUP");
                shipment.requestedShipment.serviceType = ConfigurationManager.AppSettings["FedexServiceType"];
                shipment.mergeLabelDocOption = "LABELS_AND_DOCS";

                shipment.requestedShipment.recipients = new List<Recipient>();
                shipment.requestedShipment.recipients.Add(new Recipient());
                shipment.requestedShipment.recipients[0].contact = new EVSTAR.Models.FedEx.Contact();
                shipment.requestedShipment.recipients[0].contact.companyName = "Techcycle Solutions";
                shipment.requestedShipment.recipients[0].contact.personName = "Claims";
                shipment.requestedShipment.recipients[0].contact.phoneNumber = ConfigurationManager.AppSettings["TCSClaimsPhone"];

                shipment.requestedShipment.recipients[0].address = new EVSTAR.Models.FedEx.Address();
                shipment.requestedShipment.recipients[0].address.stateOrProvinceCode = ConfigurationManager.AppSettings["TCSClaimsState"];
                shipment.requestedShipment.recipients[0].address.postalCode = ConfigurationManager.AppSettings["TCSClaimsPostalCode"];
                shipment.requestedShipment.recipients[0].address.countryCode = "US";
                shipment.requestedShipment.recipients[0].address.city = ConfigurationManager.AppSettings["TCSClaimsCity"];
                shipment.requestedShipment.recipients[0].address.residential = residential == "false";
                shipment.requestedShipment.recipients[0].address.streetLines = new List<string>();
                shipment.requestedShipment.recipients[0].address.streetLines.Add(ConfigurationManager.AppSettings["TCSClaimsAddress"]);

                shipment.requestedShipment.shipper = new Shipper();
                shipment.requestedShipment.shipper.address = new EVSTAR.Models.FedEx.Address();
                shipment.requestedShipment.shipper.address.stateOrProvinceCode = state;
                shipment.requestedShipment.shipper.address.postalCode = postalCode;
                shipment.requestedShipment.shipper.address.countryCode = country;
                shipment.requestedShipment.shipper.address.city = city;
                shipment.requestedShipment.shipper.address.residential = residential == "true";
                shipment.requestedShipment.shipper.address.streetLines = new List<string>();
                shipment.requestedShipment.shipper.address.streetLines.Add(address1);
                if (!string.IsNullOrEmpty(address2))
                    shipment.requestedShipment.shipper.address.streetLines.Add(address2);

                shipment.requestedShipment.shipper.contact = new EVSTAR.Models.FedEx.Contact();
                shipment.requestedShipment.shipper.contact.companyName = company;
                shipment.requestedShipment.shipper.contact.personName = name;
                shipment.requestedShipment.shipper.contact.phoneNumber = contactphone;

                shipment.requestedShipment.shippingChargesPayment = new ShippingChargesPayment();
                shipment.requestedShipment.shippingChargesPayment.paymentType = "RECIPIENT";
                shipment.requestedShipment.shippingChargesPayment.payor = new Payor();
                shipment.requestedShipment.shippingChargesPayment.payor.responsibleParty = new ResponsibleParty();
                shipment.requestedShipment.shippingChargesPayment.payor.responsibleParty.accountNumber = new AccountNumber()
                {
                    value = shipment.accountNumber.value
                };

                var client = new RestClient(String.Format("{0}ship/v1/shipments", fedexUrl));
                var request = new RestRequest(String.Format("{0}ship/v1/shipments", fedexUrl), Method.Post);
                request.AddHeader("Authorization", "Bearer " + bearerToken);
                request.AddHeader("X-locale", "en_US");
                request.AddHeader("Content-Type", "application/json");
                request.AddJsonBody(shipment);
                RestResponse response = client.Execute(request);

                System.Diagnostics.Debug.WriteLine(response.Content);
                ShipmentResponse resp = JsonConvert.DeserializeObject<ShipmentResponse>(response.Content);
                if (resp.output == null || resp.output == null || resp.output.transactionShipments.Count == 0 ||
                    resp.output.transactionShipments[0].pieceResponses.Count == 0 ||
                    resp.output.transactionShipments[0].pieceResponses[0].packageDocuments.Count == 0)
                    url = response.Content;
                else
                    url = resp.output.transactionShipments[0].pieceResponses[0].packageDocuments[0].url;
            }
            catch (WebException ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
                return ex.Message;
                //System.Diagnostics.Debug.WriteLine(new StreamReader(ex.Response.GetResponseStream()).ReadToEnd()); ;
            }

            return url;
        }

    }
}
