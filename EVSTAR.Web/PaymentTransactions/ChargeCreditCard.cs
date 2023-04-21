using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;
using AuthorizeNet.Api.Controllers;
using AuthorizeNet.Api.Contracts.V1;
using AuthorizeNet.Api.Controllers.Bases;
using System.Diagnostics;

namespace net.authorize.sample
{
    public class ChargeCreditCard
    {
        public static ANetApiResponse Run(String ApiLoginID, String ApiTransactionKey, decimal amount, string firstName, 
            string lastName, string address, string city, string postalCode, string card, string expDate, string cardCode)
        {
            Debug.WriteLine("Charge Credit Card Sample");

#if DEBUG
            ApiOperationBase<ANetApiRequest, ANetApiResponse>.RunEnvironment = AuthorizeNet.Environment.SANDBOX;
#else
            ApiOperationBase<ANetApiRequest, ANetApiResponse>.RunEnvironment = AuthorizeNet.Environment.PRODUCTION;
#endif

            // define the merchant information (authentication / transaction id)
            ApiOperationBase<ANetApiRequest, ANetApiResponse>.MerchantAuthentication = new merchantAuthenticationType()
            {
                name = ApiLoginID,
                ItemElementName = ItemChoiceType.transactionKey,
                Item = ApiTransactionKey,
            };

            var creditCard = new creditCardType
            {
                cardNumber = card,
                expirationDate = expDate,
                cardCode = cardCode
            };

            var billingAddress = new customerAddressType
            {
                firstName = firstName,
                lastName = lastName,
                address = address,
                city = city,
                zip = postalCode
            };

            //standard api call to retrieve response
            var paymentType = new paymentType { Item = creditCard };

            // Add line Items
            var lineItems = new lineItemType[2];
            lineItems[0] = new lineItemType { itemId = "1", name = "Fee", quantity = 1, unitPrice = amount };

            var transactionRequest = new transactionRequestType
            {
                transactionType = transactionTypeEnum.authCaptureTransaction.ToString(),    // charge the card

                amount = amount,
                payment = paymentType,
                billTo = billingAddress,
                lineItems = lineItems
            };
            
            var request = new createTransactionRequest { transactionRequest = transactionRequest };
            
            // instantiate the controller that will call the service
            var controller = new createTransactionController(request);
            controller.Execute();
            
            // get the response from the service (errors contained if any)
            var response = controller.GetApiResponse();

            // validate response
            if (response != null)
            {
                if (response.messages.resultCode == messageTypeEnum.Ok)
                {
                    if(response.transactionResponse.messages != null)
                    {
                        Debug.WriteLine("Successfully created transaction with Transaction ID: " + response.transactionResponse.transId);
                        Debug.WriteLine("Response Code: " + response.transactionResponse.responseCode);
                        Debug.WriteLine("Message Code: " + response.transactionResponse.messages[0].code);
                        Debug.WriteLine("Description: " + response.transactionResponse.messages[0].description);
                        Debug.WriteLine("Success, Auth Code : " + response.transactionResponse.authCode);
                    }
                    else
                    {
                        Debug.WriteLine("Failed Transaction.");
                        if (response.transactionResponse.errors != null)
                        {
                            Debug.WriteLine("Error Code: " + response.transactionResponse.errors[0].errorCode);
                            Debug.WriteLine("Error message: " + response.transactionResponse.errors[0].errorText);
                        }
                    }
                }
                else
                {
                    Debug.WriteLine("Failed Transaction.");
                    if (response.transactionResponse != null && response.transactionResponse.errors != null)
                    {
                        Debug.WriteLine("Error Code: " + response.transactionResponse.errors[0].errorCode);
                        Debug.WriteLine("Error message: " + response.transactionResponse.errors[0].errorText);
                    }
                    else
                    {
                        Debug.WriteLine("Error Code: " + response.messages.message[0].code);
                        Debug.WriteLine("Error message: " + response.messages.message[0].text);
                    }
                }
            }
            else
            {
                Debug.WriteLine("Null Response.");
            }

            return response;
        }
    }
}
