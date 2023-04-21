var thisCall;
var thisCustomer;
var curPage = [];
var perilSubCategories = [];
var csrCallsGrid;
var csrCallsObj;
var customerCallsGrid;
var customerClaimsGrid;

function getUniqueArray(_array) {
    // in the newArray get only the elements which pass the test implemented by the filter function.
    // the test is to check if the element's index is same as the index passed in the argument.
    let newArray = _array.filter((element, index, array) => array.indexOf(element) === index);
    return newArray
}


$(function () {
    // Execute a function when the user releases a key on the keyboard
    $(document).on('keyup', function (event) {
        // Number 13 is the "Enter" key on the keyboard
        if (event.which == 13) {
            // Cancel the default action, if needed
            event.preventDefault();
            // Trigger the button element with a click
            $(".btn-default").click();
            return false;
        }
    });

    var defaults = {
        body: '',
        title: 'EVSTAR',
        show: true,
        footer: '<button class="dialog-button" data-dialog-action="hide">Close</button>',
        onShowing: function (callback) {
            $('.dialog-container').dialog('hide');
            callback();
        }
    };
    var curHost = window.location.host.toUpperCase();

    loadPerilSubcategories();

    if (topPage() == null)
        pushPage('Login');
    else
        setNext();

    $("#divContent :input[type='text']:first").focus()

    $('#btnCSRCalls').click(function (e) {
        $('#btnCSRCalls').css('opacity', '100%');
        $('#btnCustomerCalls').css('opacity', '50%');
        $('#btnRequests').css('opacity', '50%');
        let thisUser = sessionStorage.getItem('User');
        if (thisUser != null && thisUser.length > 0) {
            thisUser = JSON.parse(thisUser);
            getCallGridData('user', thisUser.ID);
        }
    });

    //$('#btnEscalatedCalls').click(function (e) {
    //    $('#btnCSRCalls').css('opacity', '100%;');
    //    $('#btnCustomerCalls').css('opacity', '50%;');
    //    $('#btnRequests').css('opacity', '50%;');
    //});

    $('#btnCustomerCalls').click(function (e) {
        $('#btnCSRCalls').css('opacity', '50%');
        $('#btnCustomerCalls').css('opacity', '100%');
        $('#btnRequests').css('opacity', '50%');
        $('#celHistory').html('');
        let ID = sessionStorage.getItem('Customer');
        getCallGridData('customer', ID);
    });

    $('#btnRequests').click(function (e) {
        $('#btnCSRCalls').css('opacity', '50%');
        $('#btnCustomerCalls').css('opacity', '50%');
        $('#btnRequests').css('opacity', '100%');
        $('#celHistory').html('');
        let ID = sessionStorage.getItem('Customer');
        getClaimsGridData(ID);
    });

    $('#btnCustInfo').click(function (e) {
        $('#btnCustInfo').css('opacity', '100%');
        $('#btnCoverageHistory').css('opacity', '50%');
        $('#btnRequest').css('opacity', '50%');
        $('#btnNotes').css('opacity', '50%');
        $('#celCustInfo').html('');
        let ID = sessionStorage.getItem('Customer');
        getCustomerInfo(ID);
    });

    $('#btnCoverageHistory').click(function (e) {
        $('#btnCustInfo').css('opacity', '50%');
        $('#btnCoverageHistory').css('opacity', '100%');
        $('#btnRequest').css('opacity', '50%');
        $('#btnNotes').css('opacity', '50%');
        $('#celCustInfo').html('');
        //let ID = sessionStorage.getItem('Customer');
        //getClaimsGridData(ID);
    });

    $('#btnRequest').click(function (e) {
        $('#btnCustInfo').css('opacity', '50%');
        $('#btnCoverageHistory').css('opacity', '50%');
        $('#btnRequest').css('opacity', '100%');
        $('#btnNotes').css('opacity', '50%');
        $('#celCustInfo').html('');
        //let ID = sessionStorage.getItem('Customer');
        //getClaimsGridData(ID);
    });

    $('#btnNotes').click(function (e) {
        $('#btnCustInfo').css('opacity', '50%');
        $('#btnCoverageHistory').css('opacity', '50%');
        $('#btnRequest').css('opacity', '50%');
        $('#btnNotes').css('opacity', '100%');
        $('#celCustInfo').html('');
        //let ID = sessionStorage.getItem('Customer');
        //getClaimsGridData(ID);
    });

    $('#btnNewNote').click(function (e) {
        //let ID = sessionStorage.getItem('Customer');
        //getClaimsGridData(ID);
    });

    $('#btnLogin').click(function (e) {
        var username = $('#txtLoginUsername').val();
        var pass = $('#txtLoginPassword').val();

        if (username.trim() === '' || pass.trim() === '') {
            showDialog('You are missing one or more required values.', 'ERPS', 'LOGIN');
        }
        else {
            $.ajax({
                type: 'GET',
                url: "/api/user",
                headers: { "username": username, "auth": pass },
                contentType: 'application/json; charset=utf-8',
                success: function (response) {
                    if (response == null || response.Error === "NOTFOUND") {
                        showDialog('Sorry, we were unable to find your information using those credentials. Please check your entries and try again.', 'ERPS', 'LOGIN');
                    }
                    else if (response.Error == "INVALID") {
                        showDialog('Incorrect login or password. Please check your entries and try again.', 'ERPS', 'LOGIN');
                    }
                    else if (response.Error != null && response.Error.toUpperCase().includes("EXCEPTION")) {
                        showDialog(response.Error, 'ERPS', 'LOGIN');
                    }
                    else if (response.ID == 0 && response.Error != null && response.Error.length > 0) {
                        showDialog("Missing or invalid field: " + response.Error, 'ERPS', 'LOGIN');
                    }
                    else if (response.ID == 0) {
                        showDialog("Error: " + JSON.stringify(response), 'ERPS', 'LOGIN');
                    }
                    else {
                        sessionStorage.setItem('User', JSON.stringify(response));
                        $('#btnCSRCalls').click();
                        buildCSRInfo(response);
                        pushPage('CallerName');
                        setNext();
                    }
                },
                error: function (response) {
                    showDialog('Sorry, a technical issue prevented us from finding your information. Please try again later.', 'ERPS', 'LOGIN');
                }
            });
        }
    });

    let thisUser = sessionStorage.getItem('User');
    if (thisUser != null && thisUser.length > 0) {
        thisUser = JSON.parse(thisUser);
        getCallGridData('user', thisUser.ID);
    }
    buildCSRInfo();

    $('#txtLoginUsername').focus();
});

function buildCSRInfo() {
    let custID = sessionStorage.getItem('Customer');
    if (custID == null)
        custID = 0;

    if (custID > 0) {
        getCustomerInfo(custID);
    }
    else {
        $('#celCustInfo').html('');
    }

}

function loadPerilSubcategories() {
    $.ajax({
        url: "/api/peril/0",
        dataType: "json",
        type: 'GET',
        contentType: "application/json; charset=utf-8",
        success: function (data) {
            if (data == null || data.ID == 0) {
                alert("Error retrieving perils.");
            }
            else {
                if (data != null && data.length > 0) {
                    perilSubCategories = data;
                }
            }
            return data;
        },
        error: function (XMLHttpRequest, textStatus, errorThrown) {
            alert(XMLHttpRequest.responseJSON.Message);
            return null;
        }
    });
}

function formatPhoneNumber(phone) {
    return '(' + phone.substring(0, 3) + ') ' + phone.substring(3, 6) + '-' + phone.substring(6);
}

function getCustomerInfo(custID) {
    $.ajax({
        url: "/api/customer/" + custID,
        dataType: "json",
        type: 'GET',
        contentType: "application/json; charset=utf-8",
        success: function (data) {
            if (data == null || data.ID == 0) {
                showDialog("Error retrieving customer info.", 'ERPS');
            }
            else {
                let line1 = data.BillingAddress != null ? data.BillingAddress.Line1 : '';
                let line2 = data.BillingAddress != null && data.BillingAddress.Line2 != null ? data.BillingAddress.Line2 : '';
                let csz = data.BillingAddress != null ? data.BillingAddress.City + ', ' + data.BillingAddress.State + ' ' + data.BillingAddress.PostalCode : '';

                $('#celCustInfo').html('');
                $('#celCustInfo').append('<table id="tblCustInfo" width="100%" height="100%" class="widelist" border="1" cellpadding="2" cellspacing="0"></table>');
                $('#tblCustInfo').append('<tr><td class="widelistbold">Primary Name</td><td>' + data.PrimaryName + '</td></tr>');
                $('#tblCustInfo').append('<tr><td class="widelistbold">Authorized User</td><td>' + data.AuthorizedName + '</td></tr>');
                $('#tblCustInfo').append('<tr><td class="widelistbold">Wireless Number</td><td>' + formatPhoneNumber(data.MobileNumber) + '</td></tr>');
                $('#tblCustInfo').append('<tr><td class="widelistbold">Account Number</td><td>' + data.AccountNumber + '</td></tr>');
                $('#tblCustInfo').append('<tr><td class="widelistbold">Billing Address Line 1</td><td>' + line1 + '</td></tr>');
                $('#tblCustInfo').append('<tr><td class="widelistbold">Billing Address Line 2</td><td>' + line2 + '</td></tr>');
                $('#tblCustInfo').append('<tr><td class="widelistbold">Billing City, State, ZIP Code</td><td>' + csz + '</td></tr>');
            }
        },
        error: function (XMLHttpRequest, textStatus, errorThrown) {
            alert(XMLHttpRequest.responseJSON.Message);
            return null;
        }
    });
}

function getCall(callID, user, current, next) {
    $.ajax({
        url: "/api/Call?call=" + callID,
        dataType: "json",
        //headers: { "data": JSON.stringify(call) },
        type: 'GET',
        contentType: "application/json; charset=utf-8",
        success: function (data) {
            if (data == null || data.length == 0) {
                alert("Error retrieving call.");
            }
            else {
                thisCall = data[0];

                thisCall.CsrName = user.FirstName + ' ' + user.LastName;
                thisCall.ClientID = user.ClientID;
                thisCall.ClientCode = user.ParentClient != null ? user.ParentClient.Code : "";
                thisCall.UserID = user.ID;
                thisCall.CurrClient = user.ParentClient;

                if (next == '')
                    CollectDataAndEndCall(thisCall, user, current, next);
                else
                    CollectDataAndContinue(thisCall, user, current, next);

            }
            return data;
        },
        error: function (XMLHttpRequest, textStatus, errorThrown) {
            alert(XMLHttpRequest.responseJSON.Message);
            return null;
        }
    });

}

function CollectDataAndContinue(call, user, current, next) {
    if (current == 'CallerName') {
        thisCall.CallerName = $('#txtCallerName').val();
    }
    else if (current == 'AccountSearch') {
        thisCall.SearchMDN = $('#txtCallerMDN').val();
    }
    else if (current == 'CallerAddress') {
        let selectedOption = '';
        if (document.querySelector('input[name="customers"]:checked') != null)
            selectedOption = document.querySelector('input[name="customers"]:checked').value;
        if (selectedOption != null && selectedOption.length > 0) {
            thisCall.CustomerID = parseInt(selectedOption);
            sessionStorage.setItem('Customer', thisCall.CustomerID);
            let hasEmail = false;

            let emails = $('#hdnCustomerEmail').val();
            if (emails != null && emails.length > 0) {
                let parts = emails.split(',');
                if (parts.length > 0) {
                    for (var i = 0; i < parts.length; i++) {
                        let parts2 = parts[i].split('|');
                        if (parts2.length > 1) {
                            hasEmail = (parseInt(parts2[0]) == thisCall.CustomerID && parts2[1].length > 10);
                            break;
                        }
                    }
                }
            }

            if (hasEmail)
                next = 'CustomerMatch';
            else
                next = 'CustomerEmail';
            getCustomerInfo(thisCall.CustomerID);
        }
    }
    else if (current == 'CustomerEmail') {
        let email = $('#txtCallerEmail').val();
        if (email != null && email.length > 0) {
            sessionStorage.setItem('email', email);
            saveCustomer(thisCall, current);
            next = 'CustomerMatch';
        }
        else
            next = current;
    }
    else if (current == 'CustomerMatch') {

        let selActionID = $('#selAction option:selected').val();
        let selActionName = '';
        let parts = selActionID.split('|');
        if (parts.length > 1) {
            selActionID = parts[0];
            selActionName = parts[1];
        }
        thisCall.ActionID = parseInt(selActionID);

        switch (selActionName) {
            case 'ENROLL':
                break;
            case 'VERIFY':
                break;
            case 'START CLAIM':
                if (thisCall.NumApprovedReq > 0) {
                    GetCustomerScript('RRISFApproveRequest', thisCall.ClientID, 'dialog', thisCall.CallerName);
                    //if (thisCall.CallClaim == null || thisCall.CallClaim.ID == 0) {
                    //GetOpenClaim(thisCall, next);
                    //}
                    next = 'FinishReplacement';
                }
                else if (thisCall.NumOpenReq > 0) {
                    GetCustomerScript('OpenRequestActionNoticeStore', thisCall.ClientID, 'dialog', thisCall.CallerName);
                    //if (thisCall.CallClaim == null || thisCall.CallClaim.ID == 0) {
                    //GetOpenClaim(thisCall, next);
                    //}
                    next = 'FinishReplacement';
                }
                else {
                    if (thisCall.CurrCoverage != null) {
                        if (thisCall.CurrCoverage.ClosedClaimsPastYear >= 2) {
                            GetCustomerScript('SubscriptionInMaxClaimMsg', thisCall.ClientID, 'dialog', thisCall.CallerName);
                            next = current;
                        }
                        else if (thisCall.CurrCoverage.ClosedClaimsPastYear <= 1) {
                            if (thisCall.CurrCoverage.ClosedClaimsPastYear == 1)
                                GetCustomerScript('TwoMaxClaim', thisCall.ClientID, 'dialog', thisCall.CallerName);
                            next = 'StartReplacement';
                        }
                    }
                    else
                        showDialog('Customer has reached his or maximum claim limit', 'ERPS');
                }
                break;
            case 'CLAIM PROBLEM':
                next = 'StartChangeOrder';
                break;
            case 'FINISH CLAIM':
                //GetOpenClaim(thisCall, next);
                next = 'FinishReplacement';
                break;
            case 'DROP COVERAGE':
                next = 'FeatureRequest';
                break;
            case 'CANCEL CLAIM':
                next = 'CancelReplacement';
                break;
        }
    }
    else if (current == 'StartReplacement') {
        if (thisCall.CallClaim == null) {
            let callClaim = {
                ID: 0,
                CustomerID: thisCall.customerID,
                UserName: thisCall.CallUser.UserName,
                ClientID: thisCall.ClientID,
                CoveredProductID: thisCall.CoveredProductID
            };
            thisCall.CallClaim = callClaim;
        }
        thisCall.CallClaim.CoveredPerilID = $('#selPeril option:selected').val();
        thisCall.CallClaim.PerilSubcategoryID = $('#selSubPeril option:selected').val();
        thisCall.CallClaim.EventDescription = $('#txtDescription').val();
        next = 'ReplacementEventDate';
    }
    else if (current == 'FinishReplacement') {
        if (thisCall.CallClaim != null) {
            thisCall.CallClaim.CoveredPerilID = $('#selPeril option:selected').val();
            thisCall.CallClaim.PerilSubcategoryID = $('#selSubPeril option:selected').val();
            thisCall.CallClaim.EventDescription = $('#txtDescription').val();
        }
        next = 'ReplacementEventDate';
    }
    else if (current == 'ReplacementEventDate') {
        let lossDate = new Date($('#datepicker').val());
        let currDate = new Date();
        if (lossDate > currDate) {
            showDialog('You cannot select a future date.', 'Date of Loss Error');
        }
        else if (lossDate < thisCall.CurrCoverage.EffectiveDate) {
            showDialog('The data of loss is prior to the effective date of coverage.');
        }
        else {
            thisCall.CallClaim.DateOfLoss = new Date($('#datepicker').val());
            next = 'ReplacementEquipment';
        }
    }
    else if (current == 'ReplacementEquipment') {
        let equipmentID = $('#selEquipment').val();
        if (equipmentID > 0) {
            thisCall.CurrCoverage.CoverageProduct.EquipmentID = equipmentID;
        }
        next = 'SelectFulfillment';
    }
    else if (current == 'SelectFulfillment') {
        if (thisCall.CallClaim.ClaimPeril.Peril.toUpperCase() === 'LIQUID DAMAGE') {
            next = 'FulfillmentInstructions';
        }
        else {
            let selectedOption = document.querySelector('input[name="fulfillment"]:checked').value;
            if (selectedOption != null && selectedOption.length > 0) {
                thisCall.CallClaim.LocalRepair = (selectedOption == 'local');
                next = 'FulfillmentInstructions';
            }
            else {
                showDialog('Please select an option.');
            }
        }
    }
    else if (current == 'FulfillmentInstructions') {
        // Need to send the email here.
        if (thisCall.CallClaim.ClaimPeril.Peril.toUpperCase() === 'LIQUID DAMAGE') {
            let selectedOption = document.querySelector('input[name="reimbursement"]:checked').value;
            if (selectedOption != null && selectedOption.length > 0) {
                thisCall.CallClaim.ReimbursementMethod = selectedOption;
                thisCall.CallClaim.ReimbursementAccount = $('#txtReimbursementAccount').val();
            }
            else {
                showDialog('Please select an option.');
            }
        }
        next = 'SubmitClaim';
    }
    else if (current == 'SubmitClaim') {
        thisCall.CallClaim.DateSubmitted = new Date();
        next = 'ClaimEndCall';
    }
    else if (current == 'ClaimEndCall') {
        thisCall.ResultID = 8; // Claim started and approved.
        next = 'CallerName';
    }
    else if (current == 'EndCall') {
        next = 'CallerName';
    }

    $.when(upsertCall(thisCall, next)).done(function (theCall) {
    });
}

function CollectDataAndEndCall(call, user, current, next) {
    next = 'CallerName';

    $.when(upsertCall(thisCall, next)).done(function (theCall) {
    });
}

function GetCustomerScript(scriptName, clientID, lblName, callerName) {
    $.ajax({
        url: "/api/script?name=" + scriptName + "&client=" + clientID.toString(),
        dataType: "json",
        //headers: { "data": JSON.stringify(theCustomer) },
        type: 'GET',
        contentType: "application/json; charset=utf-8",
        success: function (data) {
            if (data == null || data.ID == 0) {
                alert("Error retrieving script.");
            }
            else {
                if (data.Text != null && data.Text.length > 0) {
                    if (callerName != null) {
                        data.Text = data.Text.replace('|CALLERNAME|', callerName);
                    }
                    if (lblName === 'dialog')
                        showDialog('<span class="customer">' + data.Text + '</span>', 'Unable to Start New Claim');
                    else {
                        $('#' + lblName).html(data.Text);
                        $('#' + lblName).addClass(data.CssClass);
                    }
                }
            }
            return data;
        },
        error: function (XMLHttpRequest, textStatus, errorThrown) {
            alert(XMLHttpRequest.responseJSON.Message);
            return null;
        }
    });

}

function upsertCustomer(theCustomer) {

    let type = (theCustomer != null && theCustomer.ID > 0) ? "PUT" : "POST";
    $.ajax({
        url: "/api/Customer",
        data: theCustomer,
        dataType: "json",
        headers: { "data": JSON.stringify(theCustomer) },
        type: type,
        contentType: "application/json; charset=utf-8",
        success: function (data) {
            if (data == null || data.ID == 0) {
                alert("Error saving customer.");
            }
            else {
                thisCall = data;
                //sessionStorage.setItem('Customer', thisCustomer.ID.toString());
            }
            return data;
        },
        error: function (XMLHttpRequest, textStatus, errorThrown) {
            alert(XMLHttpRequest.responseJSON.Message);
            return null;
        }
    });
}

function saveCustomer(theCall, current) {
    $.ajax({
        url: "/api/Customer",
        dataType: "json",
        headers: { "customer": theCall.CustomerID },
        type: 'GET',
        contentType: "application/json; charset=utf-8",
        success: function (data) {
            if (data == null || data.length == 0) {
                alert("Error retrieving customer.");
            }
            else {
                thisCustomer = data[0];
                sessionStorage.setItem('Customer', thisCustomer.ID.toString());
                if (current == 'CustomerEmail' && thisCustomer != null) {
                    thisCustomer.Email = sessionStorage.getItem('email');

                }

                upsertCustomer(thisCustomer);
            }
        },
        error: function (XMLHttpRequest, textStatus, errorThrown) {
            alert(XMLHttpRequest.responseJSON.Message);
            return null;
        }
    });

}

function upsertCall(theCall, next) {

    let type = (theCall != null && theCall.ID > 0) ? "PUT" : "POST";
    $.ajax({
        url: "/api/Call" + (theCall.ID > 0 ? "/" + theCall.ID : ""),
        data: JSON.stringify(theCall),
        dataType: "json",
        headers: { "data": (type == 'POST' ? JSON.stringify(theCall) : "") },
        type: type,
        contentType: "application/json; charset=utf-8",
        success: function (data) {
            if (data == null || data.ID == 0) {
                alert("Error saving call.");
            }
            else {
                thisCall = data;
                sessionStorage.setItem('Call', JSON.stringify(thisCall));
                if (next != null) {
                    let x = topPage();
                    if (x != next)
                        pushPage(next);
                    setNext();
                }
            }
            return data;
        },
        error: function (XMLHttpRequest, textStatus, errorThrown) {
            alert(XMLHttpRequest.responseJSON.Message);
            return null;
        }
    });
}

function GetOpenClaim(theCall) {
    $.ajax({
        url: "/api/Claim?customer=" + theCall.CustomerID + "&op=open",
        data: JSON.stringify(theCall),
        dataType: "json",
        type: 'GET',
        contentType: "application/json; charset=utf-8",
        success: function (data) {
            if (data == null || data.ID == 0) {
                alert("Error getting open claim.");
            }
            else {
                return data;
            }
            return null;
        },
        error: function (XMLHttpRequest, textStatus, errorThrown) {
            alert(XMLHttpRequest.responseJSON.Message);
            return null;
        }
    });
}

function updateEquipment(theCoveredProduct) {
    $.ajax({
        url: "/api/product" + (theCoveredProduct.ID > 0 ? "/" + theCoveredProduct.ID : ""),
        //data: theCall,
        dataType: "json",
        headers: { "data": JSON.stringify(theCoveredProduct) },
        type: 'PUT',
        contentType: "application/json; charset=utf-8",
        success: function (data) {
            if (data == null || data.ID == 0) {
                alert("Error updating covered equipment.");
            }
            else {
            }
            return data;
        },
        error: function (XMLHttpRequest, textStatus, errorThrown) {
            alert(XMLHttpRequest.responseJSON.Message);
            return null;
        }
    });
}

function saveCall(current, next, user) {
    let thisCall = sessionStorage.getItem('Call');
    if (thisCall != null && thisCall.length > 0)
        thisCall = JSON.parse(thisCall);

    // If the Call object hasn't been created in the database yet,
    // call the API method to create it in the database and return it.
    if (next == '') {
        pushPage('CallerName');
        loadPage('CallerName');
    }
    else if (thisCall == null || thisCall == undefined) {
        let tempCall = {
            "ID": 0,
            "ClientID": user.ClientID,
            "CustomerID": 0,
            "ClaimID": 0,
            "ActionID": 0,
            "ResultID": 0,
            "LanguageID": 1,
            "UserID": user.ID,
            "CallerName": "",
            "CsrName": user.FirstName + ' ' + user.LastName,
            "StartTime": new Date("2000-01-01"),
            "EndTime": new Date("2000-01-01"),
            "EscalationResolvedDate": new Date("2000-01-01")
        };

        if (current == 'CallerName') {
            tempCall.CallerName = $('#txtCallerName').val();
        }
        if (current == 'AccountSearch') {
            tempCall.SearchMDN = $('#txtCallerMDN').val();
        }

        $.when(upsertCall(tempCall, next)).done(function (theCall) {
        });
    }
    else {
        // The Call object should always be created and have an ID by the time
        // it gets here.

        $.when(getCall(thisCall.ID, user, current, next)).done(function (theCall) {
        });
    }
}


function setFocus(thisPage) {
    $("#divContent :input[type='text']:first").focus()
}

function sendEmail(to, subject, body) {
    var url = "/api/email?address=" + to;
    $.ajax({
        type: 'POST',
        url: url,
        headers: { "func": "MAIL", "subj": btoa(subject), "body": btoa(body) },
        contentType: 'application/json; charset=utf-8',
        success: function (response) {
            if (response == null) {
                showDialog('Sorry, we were unable to send the email', 'ERPS');
            }
            else {
            }
            $('#divWait').hide();
        },
        error: function (response) {
            showDialog('Sorry, we were unable to send the email.', 'ERPS');
        }
    });
}

function showDialog(text, title, next) {
    $.dialog({
        title: title,
        show: true,
        modal: true,
        //width: 500,
        //height: 500,
        body: $('<p>' + text + '</p>'),
        footer: next != null ? '<button class="dialog-button" onclick="setNext(\'' + next + '\');" >OK</button>' :
            '<button class="dialog-button" data-dialog-action="hide" >OK</button>'
    });
}

function setNext() {

    let thisPage = topPage();
    if (thisPage.toUpperCase() == "LOGIN") {
        $('#divContent').html('');
        $('#divContent').hide();
        $('#divLogin').show();
        $("#divContent :input[type='text']:first").focus()
    }
    else {
        $('#divLogin').hide();
        loadPage(thisPage);
    }
}

function loadPage(name) {
    let user = sessionStorage.getItem('User');
    let thisCallID = 0;

    user = deserialize(user);
    if (user != null) {
        let thisCall = sessionStorage.getItem('Call');
        if (thisCall != null && thisCall.length > 0)
            thisCall = deserialize(thisCall);

        if (thisCall != null && thisCall != undefined)
            thisCallID = thisCall.ID;

        $.ajax({
            type: 'GET',
            url: '/api/page?name=' + name,
            headers: { "user": user.ID, "customer": 0, "call": thisCallID, "language": 1, "client": user.ClientID },
            contentType: 'application/json; charset=utf-8',
            success: function (response) {
                if (response == null || response.length === 0) {
                    showDialog('Sorry, we are unable to load the requested page.', 'ERPS');
                }
                else if (response.toUpperCase().includes('ERROR: ')) {
                    showDialog(response, 'ERPS');
                }
                else {
                    $('#divContent').html('');
                    $('#divContent').append($(response));
                    $('#divContent').show();
                    $("#divContent :input[type='text']:first").focus()

                    if (name == 'StartReplacement' || name == 'FinishReplacement') {
                        $('#selPeril').change(function (e) {
                            var perilId = parseInt($('#selPeril option:selected').val());
                            let count = 0;
                            $('#selSubPeril').empty();
                            for (i = 0; i < perilSubCategories.length; i++) {
                                if (perilSubCategories[i].ID == perilId) {
                                    count++;
                                    addDropdownOption(perilSubCategories[i].Subcategory, perilSubCategories[i].SubcategoryID.toString(), 'selSubPeril');
                                }
                            }
                            if (count === 0)
                                $('#selSubPeril').hide();
                            else {
                                $('#selSubPeril').show();
                                if (thisCall != null && thisCall.CallClaim != null && thisCall.CallClaim.PerilSubcategoryID > 0)
                                    $('#selSubPeril').val(thisCall.CallClaim.PerilSubcategoryID);
                            }
                        });

                        if (thisCall != null && thisCall.CallClaim != null && thisCall.CallClaim.CoveredPerilID > 0) {
                            $('#selPeril').val(thisCall.CallClaim.CoveredPerilID.toString());
                            $('#txtDescription').val(thisCall.CallClaim.EventDescription);
                        }
                    }
                    else if (name == 'AccountSearch') {
                        if (thisCall != null)
                            $('#txtCallerMDN').val(thisCall.SearchMDN);
                    }
                    else if (name == 'CustomerMatch') {
                        if (thisCall != null && thisCall.ActionID > 0)
                            $('#selAction').val(thisCall.ActionID.toString());
                    }
                    else if (name == 'CallerAddress') {
                        if (thisCall != null && thisCall.CustomerID > 0) {
                            $("[name='customers']").removeAttr('checked');
                            $("input[name='customers'][value='" + thisCall.CustomerID.toString() + "']").attr('checked', 'checked');
                        }
                    }
                    else if (name == 'CallerName') {
                        if (thisCall != null && thisCall.length > 0) {
                            if (thisCall.ResultID != null && thisCall.ResultID > 0)
                                $('#txtCallerName').val(thisCall.CallerName);
                            //else {
                            //    thisCall = null;
                            //    let pages = sessionStorage.getItem('curPage');
                            //    pages = [];
                            //    sessionStorage.setItem('curPage', pages);
                            //    pushPage('CallerName');
                            //}
                        }
                    }
                    else if (name == 'CustomerEmail') {
                        if (thisCall != null && thisCall.CallCustomer != null)
                            $('#txtCallerEmail').val(thisCall.CallCustomer.Email);
                    }
                    else if (name == 'ReplacementEventDate') {
                        $("#datepicker").datepicker();
                        $('#datepicker').focus();
                        if (thisCall != null && thisCall.CallClaim.EventDate != null) {
                            let thisDate = new Date(thisCall.CallClaim.EventDate);
                            if (thisDate < new Date('01/01/2000'))
                                thisDate = new Date();
                            $('#datepicker').val(thisDate.toLocaleDateString());
                        }
                    }
                    else if (name == 'ReplacementEquipment') {
                        $('#selEquipment').change(function (e) {
                            let equipID = $('#selEquipment').val();

                            // Update the equipment in the CoveredProduct for the customer.
                            if (equipID != thisCall.CallClaim.CoverageProduct.EquipmentID) {
                                thisCall.CallClaim.CoverageProduct.EquipmentID = equipID;
                                updateCoveredProduct(thisCall.CallClaim.CoverageProduct);
                            }

                        });
                    }
                    else if (name == 'SelectFulfillment') {
                        $("[name='fulfillment']").removeAttr('checked');
                        if (thisCall.CallClaim.ClaimPeril.Peril.toUpperCase() === 'LIQUID DAMAGE') {
                            $('#divRepairOptions').hide();
                        }
                        else {
                            $('#divRepairOptions').show();
                            if (thisCall != null && !thisCall.CallClaim.LocalRepair) {
                                $("input[name='fulfillment'][value='mailin']").attr('checked', 'checked');
                            }
                            else {
                                $("input[name='fulfillment'][value='local']").attr('checked', 'checked');
                            }
                        }
                    }
                    else if (name == 'FulfillmentInstructions') {
                        if (thisCall != null && thisCall.CallClaim.ClaimPeril.Peril.toUpperCase() == 'LIQUID DAMAGE') {
                            $('#divReimbursementOptions').show();
                            $("[name='reimbursement']").removeAttr('checked');
                            $("input[name='reimbursement'][value='" + thisCall.CallClaim.ReimbursementMethod + "']").attr('checked', 'checked');
                            if (thisCall.CallClaim.ReimbursementMethod === 'check') {
                                $('#txtReimbursementAccount').hide();
                                GetCustomerScript('ConfirmCheck', thisCall.ClientID, 'lblReimbursementAccount');
                            }
                            else {
                                $('#txtReimbursementAccount').show();
                                $('#txtReimbursementAccount').val(thisCall.CallClaim.ReimbursementAccount);
                                if (thisCall.CallClaim.ReimbursementMethod === 'venmo')
                                    GetCustomerScript('ConfirmVenmo', thisCall.ClientID, 'lblReimbursementAccount');
                                else if (thisCall.CallClaim.ReimbursementMethod === 'paypal')
                                    GetCustomerScript('ConfirmPayPal', thisCall.ClientID, 'lblReimbursementAccount');
                                else if (thisCall.CallClaim.ReimbursementMethod === 'check')
                                    GetCustomerScript('ConfirmCheck', thisCall.ClientID, 'lblReimbursementAccount');
                                else
                                    $('#lblReimbursementAccount').html('');
                            }

                            $('input[type=radio][name=reimbursement]').change(function () {
                                $('#txtReimbursementAccount').show();
                                if (this.value === 'venmo')
                                    GetCustomerScript('ConfirmVenmo', thisCall.ClientID, 'lblReimbursementAccount');
                                else if (this.value === 'paypal')
                                    GetCustomerScript('ConfirmPayPal', thisCall.ClientID, 'lblReimbursementAccount');
                                else if (this.value === 'check') {
                                    GetCustomerScript('ConfirmCheck', thisCall.ClientID, 'lblReimbursementAccount');
                                    $('#txtReimbursementAccount').hide();
                                }
                            });
                        }
                        else {
                            $('#divReimbursementOptions').hide();
                        }
                    }
                    else if (name == 'SubmitClaim') {

                    }

                    $('#btnBack').click(function (e) {
                        handleBackButton();
                    });

                    $('#btnContinue').click(function (e) {
                        handleContinueButton(user);
                    });

                    $('#btnEndCall').click(function (e) {
                        handleEndCallButton(user);
                    });

                    $('#btnCallEnded').click(function (e) {
                        handleCallEndedButton(user);
                    });
                }
            },
            error: function (response) {
                showDialog('Sorry, we were unable to load the requested page.<br />' + response, 'ERPS');
            }
        });
    }
}

function handleBackButton() {
    popPage();
    let thisPage = topPage();
    if (thisPage == null) {
        pushPage('Login');
    }
    setNext();
}

function handleContinueButton(user) {
    let thisPage = topPage();
    if (thisPage == 'Login') {
        saveCall(thisPage, 'CallerName', user);
        $('#btnCSRCalls').click();
    }
    else if (thisPage == 'CallerName') {
        saveCall(thisPage, 'AccountSearch', user);
    }
    else if (thisPage == 'AccountSearch') {
        saveCall(thisPage, 'CallerAddress', user);
    }
    else if (thisPage == 'CallerAddress') {
        saveCall(thisPage, 'CustomerMatch', user);
    }
    else if (thisPage == 'CustomerEmail') {
        saveCall(thisPage, 'CustomerMatch', user);
    }
    else if (thisPage == 'CustomerMatch') {
        saveCall(thisPage, 'StartReplacement', user);
    }
    else if (thisPage == 'StartReplacement' || thisPage == 'FinishReplacement') {
        saveCall(thisPage, 'ReplacementEventDate', user);
    }
    else if (thisPage == 'ReplacementEventDate') {
        saveCall(thisPage, 'ReplacementEquipment', user);
    }
    else if (thisPage == 'ReplacementEquipment') {
        saveCall(thisPage, 'SelectFulfillment', user);
    }
    else if (thisPage == 'SelectFulfillment') {
        saveCall(thisPage, 'FulfillmentInstructions', user);
    }
    else if (thisPage == 'FulfillmentInstructions') {
        saveCall(thisPage, 'SubmitClaim', user);
    }
    else if (thisPage == 'SubmitClaim') {
        saveCall(thisPage, 'ClaimEndCall', user);
    }
    else if (thisPage == 'ClaimEndCall') {
        saveCall(thisPage, 'CallerName', user);
    }
    else if (thisPage == 'EndCall') {
        saveCall(thisPage, 'CallerName', user);
    }
}

function handleEndCallButton(user) {
    let thisPage = topPage();
    saveCall(thisPage, 'EndCall', user);
}

function handleCallEndedButton(user) {
    let thisPage = topPage();
    let pages = sessionStorage.getItem('curPage');
    pages = [];
    sessionStorage.setItem('curPage', pages);
    sessionStorage.setItem('Call', '');
    sessionStorage.setItem('Customer', 0);
    sessionStorage.setItem('email', '');
    saveCall(thisPage, '', user);
}

function deserialize(obj) {
    if (obj != null && obj != undefined && obj.length > 0)
        return JSON.parse(obj);
}

function serialize(obj) {
    if (obj != null && obj != undefined && obj.length > 0)
        return JSON.stringify(obj);
}

function pushPage(pageName) {
    let pages = sessionStorage.getItem('curPage');
    if (pages != null) {
        pages = pages.split(',');
    }
    else
        pages = [];
    pages.push(pageName);
    sessionStorage.setItem('curPage', pages);
}

function popPage() {
    let thisPage = null;
    let pages = sessionStorage.getItem('curPage');
    if (pages != null) {
        pages = pages.split(',');
        thisPage = pages.pop();
        sessionStorage.setItem('curPage', pages);
    }
    return thisPage;
}

function topPage() {
    let pages = sessionStorage.getItem('curPage');
    if (pages != null && pages.length > 0) {
        pages = pages.split(',');
        return pages[pages.length - 1];
    }
    return null;
}

function addDropdownOption(text, value, cmbId) {
    var newOption = new Option(text, value);
    var lst = document.getElementById(cmbId);
    if (lst) lst.options[lst.options.length] = newOption;
}

function getClaimsGridData(id) {
    $('#divWait').show();
    let user = sessionStorage.getItem('User');
    let thisCallID = 0;

    user = deserialize(user);
    if (user != null) {
        let thisCall = sessionStorage.getItem('Call');
        if (thisCall != null && thisCall.length > 0)
            thisCall = deserialize(thisCall);

        if (thisCall != null && thisCall != undefined)
            thisCallID = thisCall.ID;

        $.ajax({
            type: 'GET',
            url: '/api/page?name=CustomerClaims',
            headers: { "user": user.ID, "customer": thisCall != null ? thisCall.CustomerID : 0, "call": thisCallID, "language": 1, "client": user.ClientID },
            contentType: 'application/json; charset=utf-8',
            success: function (response) {
                if (response == null || response.length === 0) {
                }
                else if (response.toUpperCase().includes('ERROR: ')) {
                    showDialog(response, 'ERPS');
                }
                else {
                    $('#celHistory').html(response);
                }
                $('#divWait').hide();
            },
            error: function (response) {
                showDialog('Sorry, we were unable to load the requested page.<br />' + response, 'ERPS');
                $('#divWait').hide();
            }
        });
    }
    $('#divWait').hide();
}

function getCallGridData(listFor, id) {
    $('#divWait').show();
    let user = sessionStorage.getItem('User');
    let thisCallID = 0;

    user = deserialize(user);
    if (user != null) {
        let thisCall = sessionStorage.getItem('Call');
        if (thisCall != null && thisCall.length > 0)
            thisCall = deserialize(thisCall);

        if (thisCall != null && thisCall != undefined)
            thisCallID = thisCall.ID;
        let url = '/api/page?name=' + (listFor == 'user' ? 'CsrCalls' : 'CustomerCalls');
        $.ajax({
            type: 'GET',
            url: url,
            headers: { "user": user.ID, "customer": thisCall != null ? thisCall.CustomerID : 0, "call": thisCallID, "language": 1, "client": user.ClientID },
            contentType: 'application/json; charset=utf-8',
            success: function (response) {
                if (response == null || response.length === 0) {
                }
                else if (response.toUpperCase().includes('ERROR: ')) {
                    showDialog(response, 'ERPS');
                }
                else {
                    $('#celHistory').html(response);
                }
                $('#divWait').hide();
            },
            error: function (response) {
                showDialog('Sorry, we were unable to load the requested page.<br />' + response, 'ERPS');
                $('#divWait').hide();
            }
        });
    }
    $('#divWait').hide();

}

function loadcall(callID) {
    alert('This is where we load the information for this previous call.');
} 

function loadclaim(claimID) {
    alert('This is where we load the information for this previous claim.');
}