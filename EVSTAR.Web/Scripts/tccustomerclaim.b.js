var customer;
var categories = {};
var useCustomAddress = false;
var passcodeDisabled = true;
var client;
var clientProgram = 'REACH';
var primaryColor = '#8dc63f';
var buttonClass = 'btn-green';
var errorCount = 0;
var isDobson = false;
var isReach = false;
var isEVSTAR = false;
//var borderClass = 'thick-border-tcs';

function getUniqueArray(_array) {
    // in the newArray get only the elements which pass the test implemented by the filter function.
    // the test is to check if the element's index is same as the index passed in the argument.
    let newArray = _array.filter((element, index, array) => array.indexOf(element) === index);
    return newArray
}

$(function () {
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

    $('#divWait').hide();

    var curPage = window.location.pathname.toUpperCase();
    var curHost = window.location.host.toUpperCase();
    isDobson = (curHost.indexOf("DOBSON.") >= 0); // || curHost.indexOf("LOCALHOST") >= 0);
    isReach = (curHost.indexOf("REACH") >= 0 || curHost.indexOf("LOCALHOST") >= 0);
    isEVSTAR = !(isDobson || isReach);
    if (isDobson && curPage == "/DEFAULT.ASPX") {
        //window.location.href = "Dobson.aspx";
    }

    /////////////////////////////////////////////////
    // TESTING
    //isDobson = false;
    /////////////////////////////////////////////////

    if (isDobson) {
        $('#hdrNextSteps').hide();
        $('#pNextSteps').hide();
        $('#divWholeHomeIcons').hide();
        $('#divDobsonIcons').show();

        primaryColor = '#8dc63f';
        btnClass = 'btn-green';

        $('.thick-border-tcs').removeClass('thick-border-tcs');
        var curPage = window.location.pathname.toUpperCase();
        if (curPage.indexOf("DOBSONPORTAL") >= 0) {
            $('a').css('color', '#FFFFFF');
            $('#lnkHome').hide();
            $('#lnkHome2').show();
        }
        else {
            $('#lnkHome2').hide();
            $('#lnkHome').show();
        }

        clientProgram = 'DOB';
        $.ajax({
            type: 'GET',
            url: '/api/client/?program=' + clientProgram,
            contentType: 'application/json; charset=utf-8',
            success: function (response) {
                if (response == null || response.length == 0) {
                    $('#lblError').html('Sorry, we are experiencing technical difficulties.');
                    $('#divError').show();
                }
                else {
                    client = response;
                    if (client != null && client.length > 0) {
                        $('#logo').show();
                        //$('#logo').attr('src', client[0].LogoFile);
                        $('#logo').attr('alt', client[0].Name);
                        sessionStorage.setItem('client', JSON.stringify(client[0]));
                    }
                }
            },
            error: function (response) {
                $('#lblError').html('Sorry, we are experiencing technical difficulties.');
                $('#divError').show();
            }
        });
    }
    else {
        $('#lnkHome').show();
    }

    if (isReach) {
        clientProgram = 'REACH';

        $.ajax({
            type: 'GET',
            url: '/api/program/?program=' + clientProgram,
            contentType: 'application/json; charset=utf-8',
            success: function (response) {
                if (response == null || response.length == 0) {
                    $('#lblError').html('Sorry, we are experiencing technical difficulties.');
                    $('#divError').show();
                }
                else {
                    let program = response;
                    $('#lblPlanName').html(program[0].Description);
                    sessionStorage.setItem('program', JSON.stringify(program[0]));
                }
            },
            error: function (response) {
                $('#lblError').html('Sorry, we are experiencing technical difficulties.');
                $('#divError').show();
            }
        });
    }

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

    function setupPage(onPage) {
        $('#divError').hide();
        $('#divLogin').hide();
        $('#divStartDobson').hide();
        $('#divCreateCustomer').hide();
        $('#divVerifyCustomer').hide();
        $('#divSelectRouter').hide();
        $('#divRouterShipping').hide();
        $('#divNewClaim').hide();
        $('#divOrderSubmitted').hide();

        $('#btnLogin').removeClass('btn-default');
        $('#btnCreateCustomer').removeClass('btn-default');
        $('#btnNewCustomer').removeClass('btn-default');
        $('#btnVerifyCustomer').removeClass('btn-default');
        $('#btnSelectRouter').removeClass('btn-default');
        $('#btnSubmitRouter').removeClass('btn-default');

        if (onPage === 'divLogin') {
            $('#txtLoginUser').focus();
            $('#btnLogin').addClass('btn-default');
        }
        else if (onPage === 'divStartDobson') {
            $('#btnNewCustomer').addClass('btn-default');
        }
        else if (onPage === 'divCreateCustomer') {
            $('#btnCreateCustomer').addClass('btn-default');
            $('#txtFirstName').focus();
        }
        else if (onPage === 'divVerifyCustomer') {
            let firstName = sessionStorage.getItem('firstName');
            let lastName = sessionStorage.getItem('lastName');
            let address1 = sessionStorage.getItem('address1');
            let address2 = sessionStorage.getItem('address2');
            let city = sessionStorage.getItem('city');
            let postalCode = sessionStorage.getItem('postalCode');
            let email = sessionStorage.getItem('email');
            let phone = sessionStorage.getItem('phone');
            let state = '';
            let customer = sessionStorage.getItem('Customer');
            if (firstName == null && lastName == null && customer != null && customer.length > 0) {
                customer = JSON.parse(customer);
                firstName = customer.PrimaryFirstName;
                lastName = customer.PrimaryLastName;
                address1 = customer.BillingAddress != null ? customer.BillingAddress.Line1 : '';
                address2 = customer.BillingAddress != null ? customer.BillingAddress.Line2 : '';
                city = customer.BillingAddress != null ? customer.BillingAddress.City : '';
                state = customer.BillingAddress != null ? customer.BillingAddress.State : '';
                postalCode = customer.BillingAddress != null ? customer.BillingAddress.PostalCode : '';
                email = customer.Email;
                phone = formatPhoneNumber2(customer.MobileNumber);
            }

            $('#txtFirstNameV').focus();
            $('#txtFirstNameV').val(firstName);
            $('#txtLastNameV').val(lastName);
            $('#txtAddress1V').val(address1);
            $('#txtAddress2V').val(address2);
            $('#txtCityV').val(city);
            $('#txtPostalCodeV').val(postalCode);
            $('#txtEmailV').val(email);
            $('#txtPhoneV').val(phone);

            $('#btnVerifyCustomer').addClass('btn-default');
        }
        else if (onPage === 'divSelectRouter') {

            $('#btnSelectRouter').addClass('btn-default');
        }
        else if (onPage === 'divRouterShipping') {
            let firstName = sessionStorage.getItem('firstName');
            let lastName = sessionStorage.getItem('lastName');
            let address1 = sessionStorage.getItem('address1');
            let address2 = sessionStorage.getItem('address2');
            let city = sessionStorage.getItem('city');
            let postalCode = sessionStorage.getItem('postalCode');
            let email = sessionStorage.getItem('email');
            let phone = sessionStorage.getItem('phone');
            let router = sessionStorage.getItem('router');

            $('#lblName').html(firstName + ' ' + lastName);
            $('#lblAddress1').html(address1);
            $('#lblAddress2').html(address2);
            $('#lblCity').html(city);
            $('#lblPostalCode').html(postalCode);
            $('#lblEmail').html(email);
            $('#lblPhone').html(phone);
            $('#lblRouter').html(router);

            $('#btnSubmitRouter').addClass('btn-default');
        }
        else if (onPage === 'divOrderSubmitted') {
            let router = sessionStorage.getItem('router');
            let cust = sessionStorage.getItem('Customer');
            if (cust != null && cust.length > 0) {
                cust = JSON.parse(cust);
                let email = sessionStorage.getItem('email');
                let phone = sessionStorage.getItem('phone');
                let router = sessionStorage.getItem('router');

                let Properties = {
                    Make: router,
                    Originator: 'Dobson'
                };

                Properties['Repair Type'] = 'Warranty Repair - Other';

                let Ticket = {
                    id: 0,
                    customer_id: cust.RepairShoprCustomerID,
                    subject: 'Dobson router ' + router,
                    status: 'New',
                    priority: '0 Urgent',
                    ticket_type_id: 28877,
                    properties: Properties,
                    do_not_email: true,
                    created_at: new Date(),
                    updated_at: new Date(),
                    user_id: 105648         // Jen Ozment
                };

                $('#divOrderSuccess').hide();

                $.ajax({
                    url: "/api/RSTicket",
                    data: JSON.stringify(Ticket),
                    dataType: "json",
                    type: "POST",
                    contentType: "application/json; charset=utf-8",
                    success: function (data) {
                        if (data != null && (data.subject.toUpperCase().indexOf('"ERROR":') > -1 ||
                            data.subject.toUpperCase().indexOf('ERROR:') > -1 || data.subject.toUpperCase().indexOf("EXCEPTION") > -1)) {
                            alert(data.subject);
                        }
                        else {
                            $('#divOrderSuccess').show();
                            //                            sessionStorage.setItem('onPage', 'divLogin');
                            //                            setupPage('divLogin');
                        }
                        return true;
                    },
                    error: function (XMLHttpRequest, textStatus, errorThrown) {
                        alert(XMLHttpRequest.responseJSON.Message + '\r\n' + textStatus);
                        return false;
                    }
                });
            }
        }
        else if (onPage === 'divNewClaim') {
            $('#btnFindCustomer').addClass('btn-default');
        }

        $('#' + onPage).show();
    }

    function saveCustomer() {
        let firstName = sessionStorage.getItem('firstName');
        let lastName = sessionStorage.getItem('lastName');
        let address1 = sessionStorage.getItem('address1');
        let address2 = sessionStorage.getItem('address2');
        let city = sessionStorage.getItem('city');
        let postalCode = sessionStorage.getItem('postalCode');
        let email = sessionStorage.getItem('email');
        let phone = sessionStorage.getItem('phone');
        let state = sessionStorage.getItem('state');

        let thisClient = sessionStorage.getItem('client');
        if (thisClient != null)
            thisClient = JSON.parse(thisClient);

        let thisCustomer = sessionStorage.getItem('Customer');
        if (thisCustomer != null)
            thisCustomer = JSON.parse(thisCustomer);
        let thisFunc = "update";

        if (thisCustomer == null || thisCustomer.ID == 0) {
            thisFunc = "create";

            let address = {
                "ID": 0,
                "Line1": address1,
                "Line2": address2,
                "Line3": "",
                "City": city,
                "State": state,
                "PostalCode": postalCode,
                "Country": "US"
            }

            thisCustomer = {
                "ID": 0,
                "ClientID": thisClient != null ? thisClient.ID : 0,
                "MobileNumber": phone,
                "HomeNumber": "",
                "PrimaryName": firstName + ' ' + lastName,
                "PrimaryFirstName": firstName,
                "PrimaryLastName": lastName,
                "AuthorizedName": "",
                "BillingAddress": address,
                "ShippingAddress": address,
                "MailingAddress": address,
                "BillingAddressID": 0,
                "ShippingAddressID": 0,
                "MailingAddressID": 0,
                "Email": email,
                "AccountNumber": "",
                "SequenceNumber": 1,
                "SubscriptionID": "",
                "StatusCode": "Active",
                "EnrollmentDate": "",
                "CompanyName": "",
                "Password": "",
                "DateSubscriptionEmailSent": "",
                "ProgramID": 0,
                "Result": "",
                "RepairShoprCustomerID": 0
            };
        }

        $.ajax({
            url: "/api/customer?function=" + thisFunc,
            data: JSON.stringify(thisCustomer),
            dataType: "json",
            type: "POST",
            contentType: "application/json; charset=utf-8",
            success: function (data) {
                if (data != null) {
                    if (data.toUpperCase().indexOf('INVALID DATA') > -1) {
                        alert(data);
                    }
                    else if (data.toUpperCase().indexOf("ERROR:") > -1 || data.toUpperCase().indexOf("EXCEPTION") > -1) {
                        alert(data);
                    }
                    else {
                        sessionStorage.setItem('Customer', data);

                        sessionStorage.setItem('onPage', 'divSelectRouter');
                        setupPage('divSelectRouter');
                    }
                }
                return true;
            },
            error: function (XMLHttpRequest, textStatus, errorThrown) {
                alert(XMLHttpRequest.responseJSON.Message + '\r\n' + textStatus);
                return false;
            }
        });

    }

    if (curPage.includes("/DOBSONPORTAL") || curPage.includes("\\DOBSONPORTAL")) {
        let page = sessionStorage.getItem('onPage');
        if (page == null || page == undefined || page.length == 0)
            page = 'divLogin';

        setupPage(page);

        primaryColor = '#8dc63f';
        btnClass = 'btn-green';

        //$('.btn-green').addClass(btnClass);
        $('.thick-border-tcs').removeClass('thick-border-tcs'); //.addClass(borderClass);
        $('#divBody').css('background-color', '#316371');
        $('a').css('color', '#FFFFFF');

        clientProgram = 'DOB';
        GetClientInfo(clientProgram);

        var error = sessionStorage.getItem('error');
        if (error != null && error != undefined && error != 'undefined') {
            $('#lblError').html(error);
            $('#divError').show();
            sessionStorage.removeItem('error');
        }

        $('#btnFinished').click(function (e) {
            sessionStorage.setItem('onPage', 'divStartDobson');
            setupPage('divStartDobson');
        });

        $('#btnFindCustomer').click(function (e) {

            let firstName = $('#txtFirstNameClaim').val();
            let lastName = $('#txtLastNameClaim').val();
            let phone = $('#txtPhoneClaim').val();

            $.ajax({
                type: 'GET',
                url: "/api/customer?first=" + firstName + "&last=" + lastName + "&phone=" + phone,
                contentType: 'application/json; charset=utf-8',
                success: function (response) {
                    if (response == null || (response.Result != null && response.Result.includes("NOTFOUND"))) {
                        $('#lblError').html('Sorry, we were unable to find the customer. Please check your entries and try again.');
                        $('#divError').show();
                    }
                    else if (response.Result == "INVALID") {
                        $('#lblError').html('Incorrect login or password. Please check your entries and try again.');
                        $('#divError').show();
                    }
                    else if (response.Result != null && response.Result.toUpperCase().includes("EXCEPTION")) {
                        $('#lblError').html(response);
                        $('#divError').show();
                    }
                    else {
                        response.MobileNumber = response.MobileNumber.replaceAll('-', '').replaceAll('(', '').replaceAll(')', '').replaceAll(' ', '');
                        var customer = JSON.stringify(response);
                        sessionStorage.setItem('Customer', customer);
                        sessionStorage.setItem('onPage', 'divVerifyCustomer');
                        setupPage('divVerifyCustomer');
                    }
                },
                error: function (response) {
                    $('#lblError').html('Sorry, a technical issue prevented us from finding your information. Please try again later.');
                    $('#divError').show();
                }
            });

        });

        $('#btnBeginClaim').click(function (e) {
            sessionStorage.setItem('onPage', 'divNewClaim');
            setupPage('divNewClaim');
        });

        $('#btnSubmitRouter').click(function (e) {
            sessionStorage.setItem('onPage', 'divOrderSubmitted');
            setupPage('divOrderSubmitted');
        });

        $('#btnSelectRouter').click(function (e) {
            let radioValue = $("input[name='router']:checked").val();
            if (radioValue) {
                sessionStorage.setItem('router', radioValue);
                sessionStorage.setItem('onPage', 'divRouterShipping');
                setupPage('divRouterShipping');
            }
        });

        $('#btnVerifyCustomer').click(function (e) {
            let firstName = $('#txtFirstNameV').val();
            let lastName = $('#txtLastNameV').val();
            let address1 = $('#txtAddress1V').val();
            let address2 = $('#txtAddress2V').val();
            let city = $('#txtCityV').val();
            let postalCode = $('#txtPostalCodeV').val();
            let email = $('#txtEmailV').val();
            let phone = $('#txtPhoneV').val();

            sessionStorage.setItem('firstName', firstName);
            sessionStorage.setItem('lastName', lastName);
            sessionStorage.setItem('address1', address1);
            sessionStorage.setItem('address2', address2);
            sessionStorage.setItem('city', city);
            sessionStorage.setItem('postalCode', postalCode);
            sessionStorage.setItem('email', email);
            sessionStorage.setItem('phone', phone);

            $.ajax({
                type: 'GET',
                url: "/api/usps?street=" + encodeURIComponent(address1) + "&postalCode=" + encodeURIComponent(postalCode),
                //headers: { "username": username, "auth": pass },
                contentType: 'application/json; charset=utf-8',
                success: function (response) {
                    if (response == null) {
                        //$('#lblError').html('');
                        //$('#divError').show();
                    }
                    else {
                        let addr = response;
                        $('#lblState').html(addr.State);
                        sessionStorage.setItem('state', addr.State);
                        saveCustomer();
                    }
                },
                error: function (response) {
                    $('#lblError').html('Sorry, a technical issue prevented us from finding your information. Please try again later.');
                    $('#divError').show();
                }
            });
        });

        $('#btnCreateCustomer').click(function (e) {
            let firstName = $('#txtFirstName').val();
            let lastName = $('#txtLastName').val();
            let address1 = $('#txtAddress1').val();
            let address2 = $('#txtAddress2').val();
            let city = $('#txtCity').val();
            let postalCode = $('#txtPostalCode').val();
            let email = $('#txtEmail').val();
            let phone = $('#txtPhone').val();

            sessionStorage.setItem('firstName', firstName);
            sessionStorage.setItem('lastName', lastName);
            sessionStorage.setItem('address1', address1);
            sessionStorage.setItem('address2', address2);
            sessionStorage.setItem('city', city);
            sessionStorage.setItem('postalCode', postalCode);
            sessionStorage.setItem('email', email);
            sessionStorage.setItem('phone', phone);

            sessionStorage.setItem('onPage', 'divVerifyCustomer');
            setupPage('divVerifyCustomer');
        });

        $('#btnNewCustomer').click(function (e) {
            sessionStorage.setItem('onPage', 'divCreateCustomer');
            setupPage('divCreateCustomer');
        });
    }
    else {
        var client = sessionStorage.getItem('client');
        if (client != null && client != undefined && client != 'undefined' && client.length > 0) {
            client = JSON.parse(client);
            client.LogoFile = './Content/images/EVSTAR_dark_logo_transparent_small.png';
            $('#logo').attr('src', client.LogoFile);
            $('#logo').attr('alt', client.Name);
            clientProgram = client.Code;
        }

        if (clientProgram == null)
            clientProgram = sessionStorage.getItem('clientProgram');
        primaryColor = sessionStorage.getItem('primaryColor');
        btnClass = sessionStorage.getItem('btnClass');
        //borderClass = sessionStorage.getItem('borderClass');

        //$('.btn-green').removeClass('btn-green').addClass(btnClass);
        $('.thick-border-tcs').removeClass('thick-border-tcs'); //.addClass(borderClass);

        isDobson = (curHost.indexOf("DOBSON.") >= 0); // || curHost.indexOf("LOCALHOST") >= 0);
        isReach = (curHost.indexOf("REACH") >= 0 || curHost.indexOf("LOCALHOST") >= 0);
        isEVSTAR = !(isDobson || isReach);
        if (clientProgram == null || clientProgram == '')
            clientProgram = isReach ? 'REACH' : (isDobson ? 'DOB' : 'EVSTAR');
        if (client == null)
            GetClientInfo(clientProgram);

        var customer = sessionStorage.getItem('Customer');
        customer = JSON.parse(customer);
        if (customer != null && customer != undefined && customer != 'undefined') {
            setLoggedIn(customer);
        }
        else {
            setLoggedOut();
        }
    }


    if (curPage.includes("/LOGIN") || curPage.includes("\\LOGIN")) {
        let action = getUrlParameter('action');
        if (action != null && action.length > 0)
            sessionStorage.setItem('action', action);
        else
            sessionStorage.setItem('action', '');
        primaryColor = '#8dc63f';
        buttonClass = 'btn-green';
        borderClass = 'thick-border-awh';
        btnClass = 'btn-green';
        sessionStorage.setItem('clientProgram', clientProgram);
        sessionStorage.setItem('primaryColor', primaryColor);
        sessionStorage.setItem('btnClass', btnClass);
        sessionStorage.setItem('borderClass', borderClass);
        $('.thick-border-tcs').removeClass('thick-border-tcs'); //.addClass(borderClass);
        var startEmail = sessionStorage.getItem("email");
        var error = sessionStorage.getItem('error');
        if (error != null && error != undefined && error != 'undefined') {
            $('#lblError').html(error);
            $('#divError').show();
            sessionStorage.removeItem('error');
        }
        $('#btnMyAccount').hide();

        $('#txtLoginEmail').val(startEmail);
        $('#txtLoginEmail').focus();

        if (isDobson) {
            $('#divDobsonRouter').show();
        }
        else {
            $('#divDobsonRouter').hide();
        }

        if (isReach) {
            $('#divLoginWithText').html('Log in with your mobile number and password.');
            $('#loginType').html('Mobile Number');
            $('#loginTypeDescription').html('The mobile number on your plan');
        }
        else {
            $('#divLoginWithText').html('Log in with your email address and password.');
            $('#loginType').html('Email');
            $('#loginTypeDescription').html('The email address on your plan');
        }

        $('#btnLoginCancel').click(function (e) {
            window.location.href = "Default.aspx";
        });

        $('#btnLogin').click(function (e) {
            $('#divError').hide();
            //$('#divRegister').show();

            var email = $('#txtLoginEmail').val();
            var pass = $('#txtLoginPassword').val();

            if (email.trim() === '' || pass.trim() === '') {
                showDialog('You are missing one or more required values.');
            }
            else {
                $.ajax({
                    type: 'GET',
                    url: "/api/customer",
                    headers: { "email": email, "password": pass, "login": isReach ? "mdn" : "email" },
                    contentType: 'application/json; charset=utf-8',
                    success: function (response) {
                        if (response == null || (response.Result != null && response.Result.includes("NOTFOUND"))) {
                            showDialog('Sorry, we were unable to find your information using those credentials. Please check your entries and try again.');
                        }
                        else if (response.Result == "INVALID") {
                            showDialog('Incorrect login or password. Please check your entries and try again.');
                        }
                        else if (response.Result != null && response.Result.toUpperCase().includes("EXCEPTION")) {
                            showDialog(response);
                        }
                        else if (response.ID == 0 && response.Result.length > 0) {
                            showDialog("Missing or invalid field: " + response.Result);
                        }
                        else {
                            response.MobileNumber = response.MobileNumber.replaceAll('-', '').replaceAll('(', '').replaceAll(')', '').replaceAll(' ', '');
                            var customer = JSON.stringify(response);
                            sessionStorage.setItem('Customer', customer);
                            setUpVerification(response);
                        }
                    },
                    error: function (response) {
                        showDialog('Sorry, a technical issue prevented us from finding your information. Please try again later.');
                    }
                });
            }
        });
    }
    else if (curPage.includes("/CONTACT.") || curPage.includes("\\CONTACT.")) {
        //$('#divWait').show();
    }
    else if (curPage.includes("/FAQ.") || curPage.includes("\\FAQ.")) {

        $('#btnFAQBack').click(function (e) {
            //window.history.back();
            window.location.href = "Default.aspx";
        });
    }
    else if (curPage.includes("/WHATSCOVERED.") || curPage.includes("\\WHATSCOVERED.")) {

        $('#btnFAQBack').click(function (e) {
            //window.history.back();
            window.location.href = "Default.aspx";
        });
    }
    else if (curPage.includes("/REGISTERFROM") || curPage.includes("\\REGISTERFROM")) {
        let client = sessionStorage.getItem("client");
        client = JSON.parse(client);
        if (client != null) {
            let customer = sessionStorage.getItem('Customer');
            if (customer != null && customer != undefined && customer != 'undefined') {
                customer = JSON.parse(customer);

                var regCode = sessionStorage.getItem('registrationCode');
                if (regCode != null && regCode != undefined) {
                    $('#txtRegName').val(customer.PrimaryName);
                    let address = customer.BillingAddress;
                    if (address == null)
                        address = customer.MailingAddress;
                    if (address == null)
                        address = customer.ShippingAddress;
                    $('#txtRegAddress1').val(address.Line1);
                    $('#txtRegAddress2').val(address.Line2);
                    $('#txtRegCity').val(address.City);
                    $('#txtRegState').val(address.State);
                    $('#txtRegPostalCode').val(address.PostalCode);
                    $('#txtRegPhone').val(customer.MobileNumber);
                    $('#txtRegEmail').val(customer.Email);
                }
            }
        }

        $('#btnRegisterFromCode').click(function (e) {
            $('#divError').hide();
            var email = $('#txtRegEmail').val();
            var pass1 = $('#txtRegPassword1').val();
            var pass2 = $('#txtRegPassword2').val();
            var name = $('#txtRegName').val();
            var address1 = $('#txtRegAddress1').val();
            var address2 = $('#txtRegAddress2').val();
            var city = $('#txtRegCity').val();
            var state = $('#txtRegState').val();
            var zipCode = $('#txtRegPostalCode').val();
            var phone = $('#txtRegPhone').val();
            var code = sessionStorage.getItem('registrationCode');
            var customer = sessionStorage.getItem('Customer');
            customer = JSON.parse(customer);

            if (email.trim() === '' || pass1.trim() === '' || pass2.trim() === '' ||
                name.trim() === '' || address1.trim() === '' || city.trim() === '' || state.trim() === '' ||
                zipCode.trim() === '' || phone.trim() === '') {
                $('#lblError').html('You are missing one or more required values.');
                $('#divError').show();
            }
            else {
                if (pass1.trim() != pass2.trim()) {
                    $('#lblError').html('Your passwords do not match.');
                    $('#divError').show();
                }
                else {
                    var client = sessionStorage.getItem('client');
                    if (client != null && client != undefined && client != 'undefined' && client.length > 0) {
                        client = JSON.parse(client);
                    }
                    $.ajax({
                        type: 'PUT',
                        url: "/api/customer",
                        headers: {
                            "id": customer != null ? customer.ID.toString() : 0, "code": code, "email": email, "password": pass1, "confirm": pass2,
                            "name": name, "address1": address1, "address2": address2, "city": city, "state": state,
                            "postalCode": zipCode, "phone": phone, "client": client.ID.toString()
                        },
                        contentType: 'application/json; charset=utf-8',
                        success: function (response) {
                            if (response == null || (response.Result != null && response.Result.includes("NOTFOUND"))) {
                                $('#lblError').html('Sorry, we were unable to find your information. Please check your entries and try again.');
                                $('#divError').show();
                            }
                            else if (response.toUpperCase().indexOf('ERROR:') >= 0 || response.toUpperCase().indexOf('EXCEPTION') >= 0) {
                                $('#lblError').html(response);
                                $('#divError').show();
                            }
                            else if (response == "MISMATCH") {
                                sessionStorage.setItem('error', 'Your information does not match our records.');
                                //    window.location.href = "Login.aspx";
                            }
                            else if (response.toUpperCase().includes("EXCEPTION")) {
                                $('#lblError').html(response);
                                $('#divError').show();
                            }
                            else {
                                alert('Your registration was successful', client.Code, '');
                                window.location.href = 'Login.aspx';
                            //    $('#divError').hide();
                            //    $('#divLogin').hide();
                            //    $('#divLanding').show();
                            //    $.ajax({
                            //        type: 'GET',
                            //        url: "/api/customer?function=LOGIN",
                            //        headers: { "email": email, "password": pass1 },
                            //        contentType: 'application/json; charset=utf-8',
                            //        success: function (response) {
                            //            if (response == null || (response.Result != null && response.Result.includes("NOTFOUND"))) {
                            //                $('#lblError').html('Sorry, we were unable to find your information using those credentials. Please check your entries and try again.');
                            //                $('#divError').show();
                            //            }
                            //            else if (response.Result == "INVALID") {
                            //                $('#lblError').html('Incorrect login or password. Please check your entries and try again.');
                            //                $('#divError').show();
                            //            }
                            //            else if (response.Result != null && response.Result.toUpperCase().includes("EXCEPTION")) {
                            //                $('#lblError').html(response);
                            //                $('#divError').show();
                            //            }
                            //            else {
                            //                response.MobileNumber = response.MobileNumber.replaceAll('-', '').replaceAll('(', '').replaceAll(')', '').replaceAll(' ', '');
                            //                var customer = JSON.stringify(response);
                            //                sessionStorage.setItem('Customer', customer);
                            //                setUpVerification(response);
                            //            }
                            //        },
                            //        error: function (response) {
                            //            $('#lblError').html('Sorry, a technical issue prevented us from finding your information. Please try again later.');
                            //            $('#divError').show();
                            //        }
                            //    });
                            }
                        },
                        error: function (response) {
                            $('#lblError').html('Sorry, a technical issue prevented us from finding your information. Please try again later.');
                            $('#divError').show();
                        }
                    });
                }
            }
        });
    }
    else if (curPage.includes("/ADDPRODUCT") || curPage.includes("\\ADDPRODUCT")) {

        $('#btnAddProductBack').click(function (e) {
            window.location.href = 'CustomerLanding.aspx';
        });

        let customer = sessionStorage.getItem("Customer");
        if (customer != null && customer.length > 0) {
            customer = JSON.parse(customer);
            let client = sessionStorage.getItem("client");
            client = JSON.parse(client);

            //if (isDobson) {
            //    $('#dobsonlogo').show();
            //}
            //else {
            //    $('#dobsonlogo').hide();
            //}

            let code = sessionStorage.getItem('code');
            let hashed = sessionStorage.getItem('hashed');
            let method = sessionStorage.getItem('method');

            if (customer != null) {
                let productCategories = {};
                let headers = {};
                let url = '';
                if (method == "email") {
                    headers = { "address": customer.Email, "hashed": hashed, "code": code };
                    url = "/api/category/0?program=" + customer.ProgramID;
                } else if (method == "sms") {
                    headers = { "phone": customer.MobileNumber, "hashed": hashed, "code": code };
                    url = "/api/category/0?program=" + customer.ProgramID;
                }
                $.ajax({
                    type: 'GET',
                    url: url,
                    headers: headers,
                    contentType: 'application/json; charset=utf-8',
                    success: function (response) {
                        if (response == null) {
                            $('#lblError').html('Sorry, an error has occurred.');
                            $('#divError').show();
                        }
                        else {
                            productCats = response;
                            sessionStorage.setItem('productCategories', JSON.stringify(productCategories));

                            let prodType = '';
                            let rowNum = 0;
                            //let rowId = 'divProductsRow_0';
                            let catProdCount = 0;
                            let divName = '';
                            for (var j = 0; j < productCats.length; j++) {
                                if (productCats[j].ProductType != prodType) {
                                    prodType = productCats[j].ProductType;
                                    divName = 'div_' + prodType.replace(' ', '');
                                    $('#divProducts').append('<div class="device-category-name-desktop" style="order:' + rowNum.toString() + ';height: auto;" >'
                                        + '<span class="device-category-text-desktop" >' + prodType + '</span></div>');
                                    rowNum++;
                                    $('#divProducts').append('<div id="' + divName + '"_inner" class="divProductsInner" style="order:' + rowNum.toString() + ';" ></div>');
                                    $('#' + divName + '_inner').append('<div id="' + divName + '" class="device-category-block-desktop" style="order:' + j.toString() + '" ></div>');
                                    catProdCount = 0;
                                    rowNum++;
                                }
                                let colId = 'divProductsCol_' + productCats[j].ID.toString();
                                $('#' + divName).append('<div id="' + colId + '" class="product-widget-selectable" style="order:' + catProdCount + ';" />');
                                $('#' + colId).append('<a href="ProductType.aspx?category=' + productCats[j].ID.toString() + '&program=' + customer.ProgramID + '">' +
                                    '<img src="Content/images/' + productCats[j].LogoFile + '" alt="' + productCats[j].CategoryName + '"  class="product-widget-icon" /></a>');
                                $('#' + colId).append('<div class="product-widget-dev-text"><div style="margin: 0 auto;">' + productCats[j].CategoryName + '</div></div>');

                                catProdCount++;
                            }
                        }
                    },
                    error: function (response) {
                        $('#lblError').html('Sorry, an error has occurred: ' + response);
                        $('#divError').show();
                    }
                });
            }
            else {
                $('#lblError').html('Sorry, your session appears to have expired. Please click the link to return to the home page.</br></br><a href="/">Home</a>');
                $('#divError').show();
            }
        }
        else {
            $('#lblError').html('Sorry, your session appears to have expired. Please click the link to return to the home page.</br></br><a href="/">Home</a>');
            $('#divError').show();
        }
    }
    else if (curPage.includes("/CUSTOMERLAND") || curPage.includes("\\CUSTOMERLAND")) {
        let customer = sessionStorage.getItem("Customer");
        if (customer != null && customer.length > 0) {
            customer = JSON.parse(customer);
            let client = sessionStorage.getItem("client");
            if (client != null && client.length > 0)
                client = JSON.parse(client);

            let action = sessionStorage.getItem('action');
            if (action !== null || action.length > 0 || action.toUpperCase() != 'CLAIM') {
                $('#lblIncorrectProduct').hide();
            }

            sessionStorage.setItem('productSelected', '');
            sessionStorage.setItem('perilSelected', '');
            sessionStorage.setItem('subcategorySelected', '');
            sessionStorage.setItem('lossDescription', '');
            sessionStorage.setItem('dateSelected', '');

            //$('#lblProgramPhone').html(formatPhoneNumber2(client.ContactPhone1));
            //if (isDobson) {
            //    $('#dobsonlogo').show();
            //}
            //else {
            //    $('#dobsonlogo').hide();
            //}

            let code = sessionStorage.getItem('code');
            let hashed = sessionStorage.getItem('hashed');
            let method = sessionStorage.getItem('method');

            if (customer != null) {
                $('#lblLandingFirstName').html(customer.PrimaryFirstName);

                let headers = {};
                let url = "/api/product?customer=" + customer.ID;
                if (method == "email") {
                    headers = { "address": customer.Email, "hashed": hashed, "code": code };
                } else if (method == "sms") {
                    headers = { "phone": customer.MobileNumber, "hashed": hashed, "code": code };
                }
                $.ajax({
                    type: 'GET',
                    url: url,
                    headers: headers,
                    contentType: 'application/json; charset=utf-8',
                    success: function (response) {
                        if (response == null) {
                            $('#lblError').html('Sorry, an error has occurred.');
                            $('#divError').show();
                        }
                        else {
                            let products = response;
                            sessionStorage.setItem('products', JSON.stringify(products));
                            if (products.length > 0)
                                $('#lblLandingTotalDevices').html('You have ' + products.length.toString() + ' registered devices.');
                            else {
                                $('#lblLandingTotalDevices').html('Looks like you haven&apos;t added any devices.');
                                $('#lblSelectDevice').hide();
                            }
                            let productCats = [];

                            let rowNum = 0;
                            let rowId = 'divProductsRow_0';
                            for (var j = 0; j < products.length; j++) {

                                let categoryname = '';
                                let logofile = '';
                                let categoryID = 0;
                                if (products[j].ProdCategory != null && products[j].ProdCategory.LogoFile != null)
                                    logofile = products[j].ProdCategory.LogoFile;
                                if (products[j].ProdCategory != null && products[j].ProdCategory.CategoryName != null)
                                    categoryname = products[j].ProdCategory.CategoryName;
                                if (products[j].ProdCategory != null && products[j].ProdCategory.ID != null)
                                    categoryID = products[j].ProdCategory.ID;

                                if (j % 4 == 0) {
                                    rowId = 'divProductsRow_' + rowNum;
                                    rowNum++;
                                    $('#divProducts').append('<div class="row" id="' + rowId + '" />');
                                }

                                let colId = 'divProductsCol_' + products[j].ID.toString();
                                $('#' + rowId).append('<a href="ProductAction.aspx?product=' + products[j].ID.toString() + '"><div id="' + colId + '" class="product-widget-selectable" />')

                                $('#' + colId).append('<img src="Content/images/'
                                    + logofile + '" alt="' + categoryname + '" class="product-widget-icon" />');
                                $('#' + colId).append('<div class="product-widget-dev-text"><div style="margin: 0 auto;">' + products[j].Manufacturer + ' ' + products[j].Model + '</div></div></a>');
                            }
                            if (products.length === 0) {
                                $('#divProducts').append('<div class="row" id="' + rowId + '" />');
                            }
                            if (client.Code != 'EVSTAR') {
                                $('#' + rowId).append('<div class="add-product-widget-selectable" id="9999999" />')
                                $('#9999999').append('<a href="AddProduct.aspx"><div class="add-product-button" ><span class="add-product-button-text">+</span></div>');
                                $('#9999999').append('<div class="product-widget-dev-text"><div style="margin: 0 auto; color:#FB8C26;">Add new device</div></div>');
                            }
                        }
                    },
                    error: function (response) {
                        $('#lblError').html('Sorry, an error has occurred: ' + response);
                        $('#divError').show();
                    }
                });
            }
            else {
                $('#lblError').html('Sorry, your session appears to have expired. Please click the link to return to the home page.</br></br><a href="/">Home</a>');
                $('#divError').show();
            }
        }
        else {
            $('#lblError').html('Sorry, your session appears to have expired. Please click the link to return to the home page.</br></br><a href="/">Home</a>');
            $('#divError').show();
        }
    }
    else if (curPage.includes("/CUSTOMERPROD") || curPage.includes("\\CUSTOMERPROD")) {
        var customer = sessionStorage.getItem("Customer");
        customer = JSON.parse(customer);
        var client = sessionStorage.getItem("client");
        client = JSON.parse(client);

        $('#lblCustomerName').html(customer.PrimaryName);
        if (isDobson) {
            $('#dobsonlogo').show();
        }
        else {
            $('#dobsonlogo').hide();
        }

        var code = sessionStorage.getItem('code');
        var hashed = sessionStorage.getItem('hashed');
        var method = sessionStorage.getItem('method');

        if (customer != null) {

            let headers = {};
            let url = "/api/product?customer=" + customer.ID;
            if (method == "email") {
                headers = { "address": customer.Email, "hashed": hashed, "code": code };
            } else if (method == "sms") {
                headers = { "phone": customer.MobileNumber, "hashed": hashed, "code": code };
            }
            $.ajax({
                type: 'GET',
                url: url,
                headers: headers,
                contentType: 'application/json; charset=utf-8',
                success: function (response) {
                    if (response == null) {
                        $('#lblError').html('Sorry, an error has occurred.');
                        $('#divError').show();
                    }
                    else {
                        let products = response;
                        sessionStorage.setItem('products', JSON.stringify(products));
                        //let categoryID = getUrlParameter('category');
                        let prodcount = 0;
                        let prodId = 0;
                        //for (var j = 0; j < products.length; j++) {
                        //    if (categoryID.toString() == products[j].ProdCategory.ID.toString()) {
                        //        prodId = products[j].ID;
                        //        prodcount++;
                        //    }
                        //}

                        //if (prodcount === 1) {
                        //    window.location.href = 'ProductAction.aspx?product=' + prodId.toString();
                        //}
                        //else {

                        let rowNum = 0;
                        let rowId = 'divProductsRow_0';
                        for (var j = 0; j < products.length; j++) {
                            //if (categoryID.toString() == products[j].ProdCategory.ID.toString()) {
                            rowId = 'divProductsRow_' + rowNum;
                            rowNum++;
                            $('#divProducts').append('<div class="row" id="' + rowId + '" />');

                            let colId = 'divProductsCol_' + products[j].ID.toString();
                            $('#' + rowId).append('<div class="col-md-3" id="' + colId + '" style="text-align:center;align-content:center;" />');
                            $('#' + colId).append('<a href="ProductAction.aspx?product=' + products[j].ID.toString() + '"><img src="Content/images/'
                                + products[j].ProdCategory.LogoFile + '" alt="' + products[j].ProdCategory.CategoryName + '" height="120px" width="120px" /></a><br /><br />');
                            $('#' + rowId).append('<div class="col-md-3" id="' + colId + '_description" style="text-align:left;align-content:left;" />');
                            $('#' + colId + '_description').append('<b>' + products[j].ProdCategory.CategoryName.substring(0, products[j].ProdCategory.CategoryName.length - 1) + ' ' + rowNum.toString() + '</b><br />')
                                .append(products[j].Manufacturer + '<br />')
                                .append(products[j].Model + '<br />')
                                .append(products[j].SerialNumber + '<br />')
                                .append('Added: ' + products[j].ProductAdded + '<br />');
                            //}
                        }
                        //}
                    }
                },
                error: function (response) {
                    $('#lblError').html('Sorry, an error has occurred: ' + response);
                    $('#divError').show();
                }
            });
        }
        else {
            $('#lblError').html('Sorry, your session appears to have expired. Please click the link to return to the home page.</br></br><a href="/">Home</a>');
            $('#divError').show();
        }

    }
    else if (curPage.includes("/PRODUCTACT") || curPage.includes("\\PRODUCTACT")) {
        let customer = sessionStorage.getItem("Customer");
        customer = JSON.parse(customer);
        let client = sessionStorage.getItem("client");
        client = JSON.parse(client);
        let productID = getUrlParameter('product');
        //if (isDobson) {
        //    $('#dobsonlogo').show();
        //}
        //else {
        //    $('#dobsonlogo').hide();
        //}

        $('#lblCustomerName').html(customer.PrimaryName);

        let code = sessionStorage.getItem('code');
        let hashed = sessionStorage.getItem('hashed');
        let method = sessionStorage.getItem('method');

        if (customer != null) {

            let headers = {};
            let url = "/api/product?customer=" + customer.ID;
            if (method == "email") {
                headers = { "address": customer.Email, "hashed": hashed, "code": code };
            } else if (method == "sms") {
                headers = { "phone": customer.MobileNumber, "hashed": hashed, "code": code };
            }
            $.ajax({
                type: 'GET',
                url: url,
                headers: headers,
                contentType: 'application/json; charset=utf-8',
                success: function (response) {
                    if (response == null) {
                        $('#lblError').html('Sorry, an error has occurred.');
                        $('#divError').show();
                    }
                    else {
                        let products = response;
                        sessionStorage.setItem('products', JSON.stringify(products));
                        for (var j = 0; j < products.length; j++) {
                            if (products[j].ID.toString() == productID.toString()) {
                                sessionStorage.setItem('productSelected', JSON.stringify(products[j]));
                                sessionStorage.setItem('product', products[j].ID.toString());
                                sessionStorage.setItem('category', products[j].ProductCategoryID);

                                $('#divProductWidget').append('<img src="Content/images/' + products[j].ProdCategory.LogoFile + '" alt="'
                                    + products[j].ProdCategory.CategoryName + '" class="product-widget-icon" />');
                                $('#divProductWidget').append('<div class="product-widget-dev-text"><div style="margin: 0 auto;">' + products[j].Manufacturer + ' ' + products[j].Model + '</div></div>');
                                $('#lblActionDeviceType').html(products[j].ProdCategory.CategoryName);
                                $('#lblActionDeviceMake').html(products[j].Manufacturer);
                                $('#lblActionDeviceModel').html(products[j].Model);
                                $('#lblActionDeviceSerial').html(products[j].SerialNumber);
                                $('#lblActionDeviceAdded').html('Added ' + formatDate(products[j].CoverageDate));
                                $('#divActionFrameLink').append('<a href="ProductTerms.aspx?category=' + products[j].ProdCategory.ID.toString()
                                    + '&product=' + products[j].ID + '" class="action-frame-link-text-desktop" >View terms &amp; conditions</a>');
                            }
                        }
                    };

                    let headers = {};
                    let url = "/api/claim?customer=" + customer.ID;
                    if (method == "email") {
                        headers = { "address": customer.Email, "hashed": hashed, "code": code };
                    } else if (method == "sms") {
                        headers = { "phone": customer.MobileNumber, "hashed": hashed, "code": code };
                    }
                    $.ajax({
                        type: 'GET',
                        url: url,
                        headers: headers,
                        contentType: 'application/json; charset=utf-8',
                        success: function (response) {
                            if (response == null) {
                                $('#lblError').html('Sorry, an error has occurred.');
                                $('#divError').show();
                            }
                            else {
                                let claims = response;

                                if (claims.length == 0) {
                                    $('#divClaims').append('<div class="newprod-content-text-field1-desktop" id="divClaimsRow_0" style="order: 0" >');

                                    let colId = 'divClaimsCol_0';
                                    $('#divClaimsRow_0').append('<div id="' + colId + '" style="text-align:left;align-content:left;" />');
                                    $('#' + colId).append('<p>No claims to date</p>');

                                }
                                else {
                                    let rowNum = 0;
                                    let rowId = 'divClaimsRow_0';
                                    for (var j = 0; j < claims.length; j++) {
                                        if (claims[j].CoveredProductID == productID) {

                                            let status = 'OPEN';
                                            let statusDate = '01/01/2022';
                                            if (claims[j].StatusHistory.length > 0) {
                                                status = claims[j].StatusHistory[0].Status;
                                                statusDate = formatDate(claims[j].StatusHistory[0].StatusDate);
                                            }
                                            rowId = 'divClaimsRow_' + rowNum;
                                            rowNum++;
                                            $('#divClaims').append('<div class="newprod-content-text-field1-desktop" id="' + rowId + '" style="order: ' + rowNum + '" />');

                                            let colId = 'divClaimsCol_' + claims[j].ID.toString();
                                            $('#' + rowId).append('<div id="' + colId + '" style="text-align:left;align-content:left;" />');
                                            $('#' + colId).append('Claim Number:&nbsp;' + claims[j].ID.toString() + '<br />');
                                            $('#' + colId).append('Started:&nbsp;' + new Date(claims[j].DateSubmitted).toLocaleDateString() + '<br />');
                                            $('#' + colId).append('Status:&nbsp;' + status);

                                            if (status.toUpperCase() == "OPEN") {
                                                $('#btnStartCustomerClaimText').html('Finish Claim');
                                                sessionStorage.setItem('curClaim', JSON.stringify(claims[j]));
                                            }
                                            else if (status.toUpperCase() == "RECEIPT SUBMITTED") {
                                                $('#btnStartCustomerClaim').hide();
                                                sessionStorage.setItem('curClaim', '')
                                            }
                                            else {
                                                $('#btnStartCustomerClaimText').html('Start a Claim');
                                                sessionStorage.setItem('curClaim', '')
                                            }
                                        }
                                    }
                                }
                            }
                        },
                        error: function (response) {
                            $('#lblError').html('Sorry, an error has occurred: ' + response);
                            $('#divError').show();
                        }
                    });

                },
                error: function (response) {
                    $('#lblError').html('Sorry, an error has occurred: ' + response);
                    $('#divError').show();
                }
            });
        }
        else {
            $('#lblError').html('Sorry, your session appears to have expired. Please click the link to return to the home page.</br></br><a href="/">Home</a>');
            $('#divError').show();
        }

        $('#btnStartCustomerClaim').click(function (e) {
            let productID = getUrlParameter('product');

            let customer = sessionStorage.getItem('Customer');
            if (customer != null && customer.length > 0)
                customer = JSON.parse(customer);
            let txt = $('#btnStartCustomerClaimText').html();
            if (txt.toUpperCase().indexOf('FINISH') > -1) {
                window.location.href = "SubmitReceipt.aspx?product=" + productID;
            }
            else {
                let date1 = new Date(customer.EnrollmentDate);
                let date2 = new Date();

                // To calculate the time difference of two dates
                let Difference_In_Time = date2.getTime() - date1.getTime();

                // To calculate the no. of days between two dates
                let Difference_In_Days = Difference_In_Time / (1000 * 3600 * 24);

                if (customer.WarrantyProgram != null && customer.WarrantyProgram.WaitingPeriodDays != null) {
                    if (Difference_In_Days <= customer.WarrantyProgram.WaitingPeriodDays) {
                        showDialog("You are unable to make a claim within the first " + customer.WarrantyProgram.WaitingPeriodDays + " days from the purchase date of the plan.", 'EVSTAR');
                        return;
                    }
                }

                if (customer.NumClaimsLast12Months > 1) {
                    showDialog("You cannot start a new claim if you already have two claims within the past 12 months.", 'EVSTAR');
                    return;
                }

                window.location.href = "StartClaim.aspx?clear=1";
            }
        });

    }
    else if (curPage.includes("/REGISTER.") || curPage.includes("\\REGISTER.")) {
        isDobson = (curHost.indexOf("DOBSON.") >= 0); // || curHost.indexOf("LOCALHOST") >= 0);
        isReach = (curHost.indexOf("REACH") >= 0 || curHost.indexOf("LOCALHOST") >= 0);
        isEVSTAR = !(isDobson || isReach);

        //if (isDobson || isReach) {
        //    $('#divRegInfo').hide();
        //    $('#divRegCode').show();
        //    $('#btnRegister').removeClass('btn-default');
        //}
        //else {
        //    $('#divRegInfo').show();
        //    $('#divRegCode').hide();
        //    $('#btnValidateCode').removeClass('btn-default');
        //}

        $('#btnValidateCode').click(function (e) {
            $('#divError').hide();
            var regCode = $('#txtRegCode').val();

            if (regCode.trim() === '') {
                $('#lblError').html('Please enter your Registration Code.');
                $('#divError').show();
            }
            else {
                $.ajax({
                    type: 'GET',
                    url: "/api/customer",
                    headers: { "regCode": regCode },
                    contentType: 'application/json; charset=utf-8',
                    success: function (response) {
                        if (response == null || response.ID === 0) {
                            $('#lblError').html('Sorry, we were unable to find your information. Please check your Registration Code and try again.');
                            $('#divError').show();
                        }
                        else {
                            if (response.StatusCode == 'Active') {
                                sessionStorage.setItem('error', 'Our records indicate you are already registered.  Please log in if you wish to file a claim.');
                                window.location.href = "Login.aspx";
                            }
                            else {
                                alert('Great! Your registration code has been successfully validated! Click OK to continue', 'EVSTAR');
                                $('#divError').hide();
                                $('#lblError').html('');
                                sessionStorage.setItem('Customer', JSON.stringify(response));
                                sessionStorage.setItem('registrationCode', regCode);
                                $.ajax({
                                    type: 'GET',
                                    url: '/api/client/' + response.ClientID.toString(),
                                    contentType: 'application/json; charset=utf-8',
                                    success: function (response) {
                                        if (response == null || response.length == 0) {
                                            $('#lblError').html('Sorry, we are experiencing technical difficulties.');
                                            $('#divError').show();
                                        }
                                        else {
                                            client = response;
                                            if (client != null) {
                                                $('#logo').attr('src', client.LogoFile);
                                                $('#logo').attr('alt', client.Name);
                                                $('#logo').show();
                                                sessionStorage.setItem('client', JSON.stringify(client));
                                                window.location.href = "RegisterFromCode.aspx";
                                            }
                                        }
                                    },
                                    error: function (response) {
                                        $('#lblError').html('Sorry, we are experiencing technical difficulties.');
                                        $('#divError').show();
                                    }
                                });
                            }
                        }
                    },
                    error: function (response) {
                        $('#lblError').html('Sorry, a technical issue prevented us from finding your information. Please try again later.');
                        $('#divError').show();
                    }
                });
            }
        });
    }
    else if (curPage.includes("/DEFAULT") || curPage.includes("\\DEFAULT") || curPage === '\\' || curPage === '/'
        || curPage.includes("/DOBSON.") || curPage.includes("\\DOBSON.")) {
        var path = window.location.href;
        if (path.includes("logout")) {
            sessionStorage.removeItem('Customer');
            sessionStorage.removeItem('productSelected');
            sessionStorage.removeItem('perilSelected');
            sessionStorage.removeItem('subcategorySelected');
            sessionStorage.removeItem('lossDescription');
            sessionStorage.removeItem('dateSelected');
            sessionStorage.removeItem('nameOnPlan');
            sessionStorage.removeItem('addressSelected');
            sessionStorage.removeItem('passcodeDisabled');
            sessionStorage.removeItem('passcode');
            sessionStorage.removeItem('hashed');
            sessionStorage.removeItem('code');
            sessionStorage.removeItem('method');
            sessionStorage.removeItem('registrationCode');

            setLoggedOut();
        }

        primaryColor = '#8dc63f';
        buttonClass = 'btn-green';
        borderClass = 'thick-border-awh';
        btnClass = 'btn-green';
        sessionStorage.setItem('clientProgram', clientProgram);
        sessionStorage.setItem('primaryColor', primaryColor);
        sessionStorage.setItem('btnClass', btnClass);
        sessionStorage.setItem('borderClass', borderClass);

        $('.btn-green').removeClass('btn-green').addClass(btnClass);
        $('.thick-border-tcs').removeClass('thick-border-tcs'); //.addClass(borderClass);

        isDobson = (curHost.indexOf("DOBSON.") >= 0); // || curHost.indexOf("LOCALHOST") >= 0);
        isReach = (curHost.indexOf("REACH") >= 0 || curHost.indexOf("LOCALHOST") >= 0);
        isEVSTAR = !(isDobson || isReach);
        if (isDobson || isEVSTAR) {
            $('#divEVSTARLanding').hide();
            $('#divWholeHomeLanding').show();
        }
        else {
            $('#divEVSTARLanding').show();
            $('#divWholeHomeLanding').hide();
        }

        $('#btnRegisterProduct').click(function () {
            customer = sessionStorage.getItem('Customer');
            if (customer == null || customer.length == 0)
                window.location.href = "Login.aspx?action=register";
            else
                window.location.href = "CustomerLanding.aspx?action=register";
        });

        $('#btnNewClaim').click(function () {
            customer = sessionStorage.getItem('Customer');
            if (customer == null || customer.length == 0)
                window.location.href = "Login.aspx?action=claim";
            else
                window.location.href = "CustomerLanding.aspx?action=claim";
        });
    }
    else if (curPage.includes("/ADMINVERIFY.") || curPage.includes("\\ADMINVERIFY.")) {

        $('#btnSend2FABack').click(function (e) {
            window.location.href = "AdminLogin.aspx";
        });

        $('#btnSend2FA').click(function (e) {
            $('#divWaitVerify').show();
            $('#divError').hide();

            let user = sessionStorage.getItem('selUser');
            if (user != null && user.length > 0) {
                user = JSON.parse(user);
                if (user != null)
                    email2 = user.Email;
            }
            var selectedOption = $("input:radio[name=verification]:checked").val();

            if (selectedOption == "email") {
                url = "/api/email?address=" + email2;
            } else if (selectedOption == "sms") {
                url = "/api/sms?phone=" + user.Phone;
            }
            $('#divWait').show();
            $.ajax({
                type: 'POST',
                url: url,
                headers: { "func": "CODE" },
                contentType: 'application/json; charset=utf-8',
                success: function (response) {
                    $('#divWait').hide();
                    if (response == null) {
                        $('#lblError').html('Sorry, we were unable to send the verification request. Please make sure the phone you registered with is a valid mobile number.');
                        $('#divError').show();
                    }
                    else if (response.toUpperCase().indexOf("ERROR") >= 0) {
                        $('#lblError').html(response);
                        $('#divError').show();
                    }
                    else {
                        hashed = response;
                        sessionStorage.setItem('hashed', hashed);
                        window.location.href = "AdminVerifyCode.aspx";
                    }
                },
                error: function (response) {
                    $('#lblError').html('Sorry, we were unable to send the verification request. Please make sure the phone you registered with is a valid mobile number.');
                    $('#divError').show();
                    $('#divWait').hide();
                }
            });
        });

        let user = sessionStorage.getItem('selUser');
        if (user != null && user != undefined && user != 'undefined' && user.length > 0) {
            user = JSON.parse(user);

            $('#mainBody').css('width', '960px');

            if (user.Phone != null && user.Phone.length > 0) {
                var sms = formatMobileForVerification(user.Phone);
                $('#lblSmsVerify').html(sms);
                $('#radVerSms').show();
            }
            else if (customer.contacts != null && customer.contacts.length > 0 &&
                customer.contacts[x].mobile != null && customer.contacts[x].mobile.length > 0) {
                var sms = formatMobileForVerification(customer.contacts[x].mobile);
                $('#lblSmsVerify').html(sms);
                $('#radVerSms').show();
            }
            else {
                $('#radVerSms').hide();
            }

            if (user.Email != null && user.Email.length > 0) {
                var email = formatEmailForVerification(user.Email);
                $('#lblEmailVerify').html(email);
                $('#radVerEmail').show();
            }
            else if (customer.contacts != null && customer.contacts.length > 0 &&
                customer.contacts[x].email != null && customer.contacts[x].email.length > 0) {
                var email = formatEmailForVerification(customer.contacts[x].email);
                $('#lblEmailVerify').html(email);
                $('#radVerEmail').show();
            }
            else {
                $('#radVerEmail').hide();
            }
        }
        else {
            $('#lblError').html("Your session has expired.  Please log in again.");
            $('#divError').show();
        }
    }
    else if (curPage.includes("/VERIFY.") || curPage.includes("\\VERIFY.")) {

        $('#btnSendVerificationBack').click(function (e) {
            window.location.href = "Login.aspx";
        });

        $('#btnSendVerification').click(function (e) {
            $('#divError').hide();
            $('#divWaitVerify').show();

            var customer = sessionStorage.getItem('Customer');
            if (customer != null && customer != undefined && customer != 'undefined') {
                customer = JSON.parse(customer);
                var url = '';
                var selectedOption = $("input:radio[name=verification]:checked").val();
                var method = selectedOption;
                sessionStorage.setItem('method', method);
                if (selectedOption == "email") {
                    url = "/api/email?address=" + customer.Email;
                } else if (selectedOption == "sms") {
                    url = "/api/sms?phone=" + customer.MobileNumber;
                }
                $('#divWait').show();
                $.ajax({
                    type: 'POST',
                    url: url,
                    headers: { "func": "CODE" },
                    contentType: 'application/json; charset=utf-8',
                    success: function (response) {
                        $('#divWait').hide();
                        if (response == null) {
                            $('#lblError').html('Sorry, we were unable to send the verification request. Please make sure the phone you registered with is a valid mobile number.');
                            $('#divError').show();
                        }
                        else {
                            hashed = response;
                            sessionStorage.setItem('hashed', hashed);
                            setUpVerificationCheck();
                        }
                    },
                    error: function (response) {
                        $('#lblError').html('Sorry, we were unable to send the verification request. Please make sure the phone you registered with is a valid mobile number.');
                        $('#divError').show();
                        $('#divWait').hide();
                    }
                });
            }
            else {
                $('#lblError').html('Sorry, your session appears to have expired. Please click the link to return to the home page.</br></br><a href="/">Home</a>');
                $('#divError').show();
            }
        });

        if (customer != null && customer != undefined && customer != 'undefined') {
            //customer = JSON.parse(customer);

            if (customer.WarrantyProgram != null && customer.WarrantyProgram.ProgramClient != null) {
                $('#logo').attr('src', customer.WarrantyProgram.ProgramClient.LogoFile);
                $('#logo').attr('alt', customer.WarrantyProgram.ProgramClient.Name);
            }

            if (customer.MobileNumber.length > 0) {
                var sms = formatMobileForVerification(customer.MobileNumber);
                $('#lblSmsVerify').html(sms);
                $('#radVerSms').show();
            }
            else {
                $('#radVerSms').hide();
            }
            if (customer.Email.length > 0) {
                var email = formatEmailForVerification(customer.Email);
                $('#lblEmailVerify').html(email);
                $('#radVerEmail').show();
            }
            else {
                $('#radVerEmail').hide();
            }
        }
        else {
            $('#lblError').html("Your session has expired.  Please log in again.");
            $('#divError').show();
        }
    }
    else if (curPage.includes("/VERIFYCODE") || curPage.includes("\\VERIFYCODE")) {
        $('#divWaitVerify').hide();
        var method = sessionStorage.getItem('method');

        if (method == "email") {
            $('#divCheckSpam').show();
        } else if (method == "sms") {
            $('#divCheckSpam').hide();
        }
        //$('#txtVerificationCode').focus();

        $('#btnVerifyCodeBack').click(function (e) {
            window.location.href = "Verify.aspx";
        });

        $('#btnVerifyCode').click(function (e) {
            let code = $('#txtVerificationCode').val();
            let hashed = sessionStorage.getItem('hashed');
            let method = sessionStorage.getItem('method');
            let customer = sessionStorage.getItem('Customer');
            if (customer != null && customer != undefined && customer != 'undefined') {
                customer = JSON.parse(customer);

                if (customer != null) {
                    let url = '';
                    let headers = {};
                    if (method == "email") {
                        headers = { "address": customer.Email, "hashed": hashed, "code": code };
                        url = "/api/email";
                    } else if (method == "sms") {
                        headers = { "phone": customer.MobileNumber, "hashed": hashed, "code": code };
                        url = "/api/sms";
                    }
                    $.ajax({
                        type: 'GET',
                        url: url,
                        headers: headers,
                        contentType: 'application/json; charset=utf-8',
                        success: function (response) {
                            if (response == null) {
                                showDialog('Sorry, we were unable to verify the code. Please check the code and try again.');
                            }
                            else {
                                if (response == 'FALSE') {
                                    showDialog('Sorry, we were unable to verify the code. Please check the code and try again.');
                                }
                                else {
                                    sessionStorage.setItem('code', code);
                                    let action = sessionStorage.getItem('action');
                                    if (action == null || action == undefined)
                                        action = '';
                                    window.location.href = 'CustomerLanding.aspx?action=' + action;
                                }
                            }
                        },
                        error: function (response) {
                            showDialog('Sorry, we were unable to verify the code. Please check the code and try again.');
                        }
                    });
                }
                else {
                    showDialog('Sorry, your session appears to have expired. Please click the link to return to the home page.</br></br><a href="/">Home</a>');
                }
            }
            else {
                showDialog('Sorry, your session appears to have expired. Please click the link to return to the home page.</br></br><a href="/">Home</a>');
            }
        });
    }
    else if (curPage.includes("/ADMINVERIFYCODE") || curPage.includes("\\ADMINVERIFYCODE")) {
        $('#divWaitVerify').hide();
        var method = sessionStorage.getItem('method');

        if (method == "email") {
            $('#divCheckSpam').show();
        } else if (method == "sms") {
            $('#divCheckSpam').hide();
        }
        $('#txtVerificationCode').focus();
    }
    else if (curPage.includes("/PRODUCTTYPE") || curPage.includes("\\PRODUCTTYPE")) {
        //if (isDobson) {
        //    $('#dobsonlogo').show();
        //}
        //else {
        //    $('#dobsonlogo').hide();
        //}
        setUpProductIdentification();
    }
    else if (curPage.includes("/STARTCLAIM") || curPage.includes("\\STARTCLAIM")) {
        setUpDescribeEvent();
    }
    else if (curPage.includes("/REIMBURSEMENTFEE.") || curPage.includes("\\REIMBURSEMENTFEE.")) {
        $('#btnReimburseFeeBack').click(function (e) {
            window.location.href = "SubmitDetails.aspx";
        });

        $('#btnReimburseFee').click(function (e) {
            window.location.href = "SubmitDetails.aspx";
        });
    }
    else if (curPage.includes("/SUBMITREC") || curPage.includes("\\SUBMITREC")) {
        $('#txtUploadStatus').val('');

        let dropArea = document.getElementById('pnlFacImport');

        ['dragenter', 'dragover', 'dragleave', 'drop'].forEach(eventName => {
            dropArea.addEventListener(eventName, preventDefaults, false);
        });

        ['dragenter', 'dragover'].forEach(eventName => {
            dropArea.addEventListener(eventName, highlight, false);
        });

        ['dragleave', 'drop'].forEach(eventName => {
            dropArea.addEventListener(eventName, unhighlight, false);
        });

        dropArea.addEventListener('drop', handleDrop, false);

        function highlight(e) {
            dropArea.classList.add('highlight');
        }

        function unhighlight(e) {
            dropArea.classList.remove('highlight');
        }

        $('#btnSubmitReceiptContinue').click(function () {
            window.location.href = "Reimbursement.aspx";
        });

        $('#btnSubmitReceiptBack').click(function () {
            let productID = getUrlParameter('product');
            window.location.href = "ProductAction.aspx?product=" + productID;
        });
    }
    else if (curPage.includes("/SUBMITDETAILS") || curPage.includes("\\SUBMITDETAILS")) {

        $('#btnSubmitDetailsBack').click(function (e) {
            window.location.href = "StartClaim.aspx";
        });

        $('#btnSubmitDetails').click(function (e) {
            isDobson = (curHost.indexOf("DOBSON.") >= 0); // || curHost.indexOf("LOCALHOST") >= 0);
            isReach = (curHost.indexOf("REACH") >= 0 || curHost.indexOf("LOCALHOST") >= 0);
            isEVSTAR = !(isDobson || isReach);
            let productSelected = sessionStorage.getItem('productSelected');
            if (productSelected != null) {
                productSelected = JSON.parse(productSelected);

                let clientProgram = sessionStorage.getItem('clientProgram');
                if (clientProgram != null) {
                    if (productSelected.ProdCategory.FulfillmentType === 1) {  // Reimbursement only
                        window.location.href = 'ReimbursementFee.aspx';
                    }
                    if (productSelected.ProdCategory.FulfillmentType === 5) {  // The REACH Mobile hybrid fulfillment.
                        window.location.href = 'PhoneRepair.aspx'
                    }
                    else if (productSelected.ProdCategory.FulfillmentType === 3) { // In-Home
                        window.location.href = 'InHomeRepair.aspx'
                    }
                    else if (productSelected.CategoryName.toUpperCase() === 'PHONES') {
                        window.location.href = 'PhoneRepair.aspx';
                    }
                    else {
                        if ((isDobson || isReach) &&
                            productSelected.ProdCategory != null && productSelected.ProdCategory.ServiceFee > 0.0) {
                            sessionStorage.setItem('productSelected', JSON.stringify(productSelected));
                            window.location.href = 'CollectPayment.aspx';
                        }
                        else {
                            window.location.href = 'ClaimComplete.aspx';
                        }
                    }
                }
                else
                    showDialog('Unknown client program', 'EVSTAR');
            }
            else
                showDialog('Unknown product.', 'EVSTAR');
        });

        if (customer != null && customer != undefined && customer != 'undefined') {
            //customer = JSON.parse(customer);

            var perilText = sessionStorage.getItem('perilText');
            var subcategoryText = sessionStorage.getItem('subcategoryText');
            var lossDescription = sessionStorage.getItem('lossDescription');
            var dateSelected = sessionStorage.getItem('dateSelected');
            var nameOnPlan = sessionStorage.getItem('nameOnPlan');
            var addressSelected = sessionStorage.getItem('addressSelected');

            $('#lblPerilToSubmit').html(perilText);
            $('#lblSubcategoryToSubmit').html(subcategoryText);
            if (perilText.toUpperCase() === subcategoryText.toUpperCase())
                $('#lblSubcategoryToSubmit').hide();
            else
                $('#lblSubcategoryToSubmit').show();
            $('#lblDescriptionToSubmit').html(lossDescription);
            $('#lblDateToSubmit').html(dateSelected);
            $('#lblNameOnPlan').html(nameOnPlan);
            $('#lblAddressOnPlan').html(addressSelected);
            $('#lblPhoneNumberOnPlan').html(customer.MobileNumber);
            $('#lblEmailAddressOnPlan').html(customer.Email);

            isDobson = (curHost.indexOf("DOBSON.") >= 0); // || curHost.indexOf("LOCALHOST") >= 0);
            isReach = (curHost.indexOf("REACH") >= 0 || curHost.indexOf("LOCALHOST") >= 0);
            isEVSTAR = !(isDobson || isReach);
            if (isReach) {
                $('#lblSubmitButton').text('Submit');
                $('#lblSubmitText').html('Submit');
            }
            else if (isEVSTAR || isDobson) {
                $('#lblSubmitButton').text('Pay Service Fee');
                $('#lblSubmitText').html('Pay Service Fee');
            }
            else {
                $('#lblSubmitButton').text('Submit');
                $('#lblSubmitText').html('Submit');
            }
            $('#lblProgramPhone').html(formatPhoneNumber2(customer.WarrantyProgram.ProgramClient.ContactPhone1));
        }
        else {
            $('#lblError').html('Sorry, your session appears to have expired. Please click the link to return to the home page.</br></br><a href="/">Home</a>');
            $('#divError').show();
        }
    }
    else if (curPage.includes("/PHONEREPAIR.") || curPage.includes("\\PHONEREPAIR.")) {
        let perilText = sessionStorage.getItem('perilText');
        //if (perilText.toUpperCase().includes("LIQUID")) {
        //    $('#divLocalRepair').hide();
        //    $('#spnLocalRepair').show();
        //    $('#spnTwoOptions').hide();
        //    $('#divMailInRepair').hide();
        //    $('#spnMailInRepair').hide();
        //    $('#lblLocalContinue').html('Select Continue for next steps.');
        //    $("input:radio[name=repair][value='mailin']").prop("checked", true).trigger("click");
        //}
        //else {
        if (isReach) {
            $('#spnTwoOptions').hide();
            $('#divMailInRepair').hide();
            $('#spnMailInRepair').hide();
            $('#divLocalRepair').hide();
            $('#spnLocalRepair').show();
            $('#lblLocalContinue').html('Select Continue for next steps.');
            $("input:radio[name=repair][value='local']").prop("checked", true).trigger("click");
        }
        else {
            $('#divLocalRepair').show();
            $('#spnLocalRepair').show();
            $('#divMailInRepair').show();
            $('#spnMailInRepair').show();
            $('#spnTwoOptions').show();
            $('#lblLocalContinue').html('Please pick the option you want and select Continue.');
            $("input:radio[name=repair][value='mailin']").prop("checked", true).trigger("click");
        }
        //}

        let productSelected = sessionStorage.getItem('productSelected');
        if (productSelected != null && productSelected != undefined) {
            productSelected = JSON.parse(productSelected);
            if (productSelected != null && productSelected.ProdCategory != null) {
                // Create our number formatter.
                let formatter = new Intl.NumberFormat('en-US', {
                    style: 'currency',
                    currency: 'USD',

                    // These options are needed to round to whole numbers if that's what you want.
                    //minimumFractionDigits: 0, // (this suffices for whole numbers, but will print 2500.10 as $2,500.1)
                    //maximumFractionDigits: 0, // (causes 2500.99 to be printed as $2,501)
                });
                $('#lblServiceFee').html(formatter.format(productSelected.ProdCategory.ServiceFee));
                $('#lblServiceFee2').html(formatter.format(productSelected.ProdCategory.ServiceFee));
            }
        }

        $('#btnSelectRepairBack').click(function (e) {
            window.location.href = 'SubmitDetails.aspx';
        });

        $('#btnSelectRepair').click(function (e) {
            let repairOption = $("input:radio[name=repair]:checked").val();
            sessionStorage.setItem('repairOption', repairOption.toUpperCase());

            if (repairOption.toUpperCase() == 'LOCAL') {
                let customer = sessionStorage.getItem('Customer');
                if (customer != null && customer != undefined && customer != 'undefined') {
                    //    customer = JSON.parse(customer);
                    window.location.href = 'PhoneRepairProviders.aspx';
                    //    let productSelected = sessionStorage.getItem('productSelected');
                    //    productSelected = JSON.parse(productSelected);
                    //    let subcategorySelected = sessionStorage.getItem('subcategorySelected');
                    //    let dateSelected = sessionStorage.getItem('dateSelected');

                    //    let url = "/api/email?address=" + encodeURIComponent(customer.Email) + "&dev=" + productSelected.ID + "&date="
                    //        + encodeURIComponent(dateSelected) + "&option=" + repairOption + "&cat=" + encodeURIComponent(subcategorySelected);
                    //    $('#divWait').show();
                    //    $.ajax({
                    //        type: 'POST',
                    //        url: url,
                    //        headers: { "func": "EMAIL" },
                    //        contentType: 'application/json; charset=utf-8',
                    //        success: function (response) {
                    //            $('#divWait').hide();
                    //            if (response == null) {
                    //                $('#lblError').html('Sorry, we were unable to send the confirmation email. Please make sure the email address you registered with is a valid email address.');
                    //                $('#divError').show();
                    //            }
                    //            else {
                    //                window.location.href = 'PhoneRepairProviders.aspx';
                    //            }
                    //        },
                    //        error: function (response) {
                    //            $('#lblError').html('Sorry, we were unable to send the confirmation email. Please make sure the email address you registered with is a valid email address.');
                    //            $('#divError').show();
                    $('#divWait').hide();
                    //        }
                    //    });
                }
                else {
                    $('#lblError').html('Sorry, your session appears to have expired. Please click the link to return to the home page.</br></br><a href="/">Home</a>');
                    $('#divError').show();
                }

            }
            else {
                window.location.href = 'CollectPayment.aspx';
            }
        });
    }
    else if (curPage.includes("/DEVICEREPAIR") || curPage.includes("\\DEVICEREPAIR")) {
        $('#lblError').html('');
        $('#divError').hide();
        if (customer != null && customer != undefined && customer != 'undefined') {
            //customer = JSON.parse(customer);
            $('#lblAccountAddress').html(formatAddress(customer.ShippingAddress));
            if (customer.WarrantyProgram.ProgramClient.Code === 'CE') {
                $('#divDeviceSecurity').show();
            }
            else {
                $('#divDeviceSecurity').hide();
            }
        }
        else {
            $('#lblError').html('Sorry, your session appears to have expired. Please click the link to return to the home page.</br></br><a href="/">Home</a>');
            $('#divError').show();
        }

        $('#btnDeviceRepairBack').click(function (e) {
            window.location.href = 'SubmitDetails.aspx';
        });

        $('#btnCreateShippingLabel').click(function (e) {
            $('#divWait').show();
            let customer = sessionStorage.getItem('Customer');
            if (customer != null && customer != undefined && customer != 'undefined') {
                customer = JSON.parse(customer);
                getFedexLink(customer);
                $('#divWait').hide();
                //    isDobson = (curHost.indexOf("DOBSON.") >= 0); // || curHost.indexOf("LOCALHOST") >= 0);
                //    isReach = (curHost.indexOf("REACH") >= 0 || curHost.indexOf("LOCALHOST") >= 0);
                //    isEVSTAR = !(isDobson || isReach);
                //    let repairOption = sessionStorage.getItem('repairOption');

                //    if (repairOption.toUpperCase() == "MAIL-IN") {
                //        window.location.href = 'CollectPayment.aspx';
                //    }
                //    else {
                //        window.location.href = 'ClaimComplete.aspx';
                //    }
            }
            else {
                $('#lblError').html('Sorry, your session appears to have expired. Please click the link to return to the home page.</br></br><a href="/">Home</a>');
                $('#divError').show();
                $('#divWait').hide();
            }
        });
    }
    else if (curPage.includes("/PHONEREPAIRPRO") || curPage.includes("\\PHONEREPAIRPRO")) {
        $('#lblLocalRepairSelected').hide();
        isDobson = (curHost.indexOf("DOBSON.") >= 0); // || curHost.indexOf("LOCALHOST") >= 0);
        isReach = (curHost.indexOf("REACH") >= 0 || curHost.indexOf("LOCALHOST") >= 0);
        isEVSTAR = !(isDobson || isReach);
        let perilText = sessionStorage.getItem('perilText');
        if (!isReach && !perilText.toUpperCase().includes("LIQUID")) {
            $('#lblLocalRepairSelected').show();
        }

        if (customer != null && customer != undefined && customer != 'undefined') {
            //customer = JSON.parse(customer);
            $('#btnSelectRepairProviderBack').show();
            $('#btnSelectRepairProvider').show();

            let productSelected = sessionStorage.getItem('productSelected');
            if (productSelected != null && productSelected != undefined) {
                productSelected = JSON.parse(productSelected);
                // Create our number formatter.
                var formatter = new Intl.NumberFormat('en-US', {
                    style: 'currency',
                    currency: 'USD',

                    // These options are needed to round to whole numbers if that's what you want.
                    //minimumFractionDigits: 0, // (this suffices for whole numbers, but will print 2500.10 as $2,500.1)
                    //maximumFractionDigits: 0, // (causes 2500.99 to be printed as $2,501)
                });
                //if (productSelected != null && productSelected.ProdCategory != null)
                //    $('#lblRepairLimit').html(formatter.format(productSelected.ProdCategory.MaxAmountPerClaim));
            }
        }

        $('#btnSelectRepairProviderBack').click(function (e) {
            window.location.href = 'PhoneRepair.aspx';
        });

        $('#btnSelectRepairProvider').click(function (e) {
            window.location.href = 'ClaimComplete.aspx';
        });
    }
    else if (curPage.includes("/INHOMEREPAIR.") || curPage.includes("\\INHOMEREPAIR.")) {
        var client = sessionStorage.getItem("client");
        client = JSON.parse(client);
        sessionStorage.removeItem('invoice');
        sessionStorage.setItem('repairOption', 'IN-HOME');

        $('#btnInHomeRepairBack').click(function () {
            window.location.href = 'CollectPayment.aspx';
        });

        $('#btnInHomeRepair').click(function () {
            window.location.href = 'ClaimComplete.aspx';
        });

        if (customer != null && customer != undefined && customer != 'undefined') {
            $('#lblAddress').html(formatAddress(customer.ShippingAddress));
            $('#lblPhoneNumber').html(formatPhoneNumber2(customer.MobileNumber));
        }
    }
    else if (curPage.includes("/REIMBURSEMENT.") || curPage.includes("\\REIMBURSEMENT.")) {
        //alert("test");
        $('#btnReimbursementBack').click(function (e) {
            window.location.href = "SubmitReceipt.aspx";
        });

        $('#btnReimbursement').click(function (e) {
            let selectedOption = $("input:radio[name=reimbursement]:checked").val();
            sessionStorage.setItem('reimbursement', selectedOption);
            window.location.href = "PaymentOption.aspx";
        });
    }
    else if (curPage.includes("/PAYMENTOPTION.") || curPage.includes("\\PAYMENTOPTION.")) {
        let selectedOption = sessionStorage.getItem('reimbursement');
        $('#divPayPal').hide();
        $('#divVenmo').hide();
        $('#divCheck').hide();
        if (selectedOption == 'paypal')
            $('#divPayPal').show();
        else if (selectedOption == 'venmo')
            $('#divVenmo').show();
        else
            $('#divCheck').show();

        let customer = sessionStorage.getItem("Customer");
        if (customer != null && customer.length > 0) {
            customer = JSON.parse(customer);

            $('#lblProgramPhone').html(formatPhoneNumber2(customer.WarrantyProgram.ProgramClient.ContactPhone1));
            $('#lblProgramPhone2').html(formatPhoneNumber2(customer.WarrantyProgram.ProgramClient.ContactPhone1));
        }

        $('#btnPaymentOptionBack').click(function (e) {
            window.location.href = "Reimbursement.aspx";
        });

        $('#btnPaymentOption').click(function (e) {
            if ($('#btnPaymentOptionText').html() == 'Return to Home') {
                window.location.href = 'CustomerLanding.aspx';
            }
            else {
                let selectedOption = sessionStorage.getItem('reimbursement');
                let account = '';
                let account2 = '';
                if (selectedOption == 'paypal') {
                    account = $('#txtPayPal').val();
                    account2 = $('#txtPayPalConfirm').val();
                }
                else if (selectedOption == 'venmo') {
                    account = $('#txtVenmo').val();
                    account2 = $('#txtVenmoConfirm').val();
                }

                if (account != account2) {
                    showDialog("The account and confirmation values do not match.");
                }
                else {
                    let customer = sessionStorage.getItem('Customer');
                    if (customer != null && customer != undefined && customer != 'undefined') {
                        customer = JSON.parse(customer);
                        let claim = sessionStorage.getItem('curClaim');
                        if (claim != null && claim != undefined && claim.length > 0) {
                            claim = JSON.parse(claim);
                            claim.ReimbursementMethod = selectedOption;
                            claim.ReimbursementAccount = account;

                            let hashed = sessionStorage.getItem('hashed');
                            let code = sessionStorage.getItem('code');
                            let method = sessionStorage.getItem('method');

                            let headers = {};
                            let url = "/api/claim";
                            if (method == "email") {
                                headers = { "address": customer.Email, "hashed": hashed, "code": code };
                            } else if (method == "sms") {
                                headers = { "phone": customer.MobileNumber, "hashed": hashed, "code": code };
                            }

                            // Save the claim to the database
                            $.ajax({
                                type: 'POST',
                                url: url,
                                headers: headers,
                                data: JSON.stringify(claim),
                                contentType: 'application/json; charset=utf-8',
                                success: function (response) {
                                    if (response != null) {
                                        sessionStorage.setItem('curClaim', JSON.stringify(response));
                                        //window.location.href = "CustomerLanding.aspx";
                                        $('#divPaymentInfoComplete').show();
                                        $('#divPaymentInfo').hide();
                                        $('#btnPaymentOptionText').html('Return to Home');
                                    }
                                    else {
                                        showDialog('An error occurred while saving the claim.<br />');
                                    }
                                },
                                error: function (response) {
                                    showDialog('An error occurred while saving the claim.<br />' + response);
                                }
                            });
                        }
                        else {
                            showDialog('Unable to find associated claim.');
                        }
                    }
                    else {
                        showDialog('Sorry, your session appears to have expired. Please click the link to return to the home page.</br></br><a href="/">Home</a>');
                    }
                }
            }
        });
    }
    else if (curPage.includes("/PAYINVOICE.") || curPage.includes("\\PAYINVOICE.")) {
        var client = sessionStorage.getItem("client");
        client = JSON.parse(client);
        sessionStorage.removeItem('invoice');

        $('#btnValidateInvoice').click(function () {
            var email = $('#txtEmailAddress').val();
            var invoice = $('#txtInvoiceNumber').val();

            if (email.trim() === '' || invoice.trim() === '') {
                showDialog('You are missing one or more required values.');
            }
            else {
                $.ajax({
                    type: 'GET',
                    url: "/api/invoice",
                    headers: { "email": email, "invoice": invoice },
                    contentType: 'application/json; charset=utf-8',
                    success: function (response) {
                        if (response == null) {
                            showDialog('Sorry, we were unable to find your invoice using that information. Please check your entries and try again.');
                        }
                        else {
                            sessionStorage.setItem('invoice', JSON.stringify(response));
                            let invoice = response;

                            // Get the customer using the CustomerID from the invoice.

                            $.ajax({
                                type: 'GET',
                                url: "/api/customer/" + response.CustomerID,
                                contentType: 'application/json; charset=utf-8',
                                success: function (response) {
                                    if (response == null || (response.Result != null && response.Result.includes("NOTFOUND"))) {
                                        showDialog('Sorry, we were unable to find your customer information.');
                                    }
                                    else if (response.Result != null && response.Result.toUpperCase().includes("EXCEPTION")) {
                                        showDialog.html(response);
                                    }
                                    else {
                                        response.MobileNumber = response.MobileNumber.replaceAll('-', '').replaceAll('(', '').replaceAll(')', '').replaceAll(' ', '');
                                        var customer = JSON.stringify(response);
                                        sessionStorage.setItem('Customer', customer);

                                        if (invoice.AmountPaid == invoice.AmountDue) {
                                            showDialog('Sorry, this invoice has already been paid.');
                                        }
                                        else
                                            window.location.href = 'CollectPayment.aspx';
                                    }
                                },
                                error: function (response) {
                                    showDialog('Sorry, a technical issue prevented us from finding your information. Please try again later.');
                                }
                            });
                        }
                    },
                    error: function (response) {
                        showDialog('Sorry, a technical issue prevented us from finding your information. Please try again later.');
                    }
                });
            }
        });
    }
    else if (curPage.includes("/COLLECTPAY") || curPage.includes("\\COLLECTPAY")) {
        $('#lblError').html('');
        $('#divError').hide();
        $('#txtCardName').focus();
        $('#spnCollectPayment').html('Process Payment');
        let invoice = sessionStorage.getItem('invoice');
        let customer = sessionStorage.getItem('Customer');

        if (customer != null && customer != undefined && customer != 'undefined') {
            customer = JSON.parse(customer);
            $('#btnCollectPaymentBack').show();
            $('#btnCollectPayment').show();
        }

        // Create our number formatter.
        var formatter = new Intl.NumberFormat('en-US', {
            style: 'currency',
            currency: 'USD',

            // These options are needed to round to whole numbers if that's what you want.
            //minimumFractionDigits: 0, // (this suffices for whole numbers, but will print 2500.10 as $2,500.1)
            //maximumFractionDigits: 0, // (causes 2500.99 to be printed as $2,501)
        });

        if (invoice != null && invoice != undefined && invoice != 'undefined') {
            invoice = JSON.parse(invoice);
            if (invoice != null) {
                $('#txtAmountToPay').val(formatter.format(invoice.AmountDue));

                $('#btnCollectPaymentBack').click(function (e) {
                    window.location.href = 'PayInvoice.aspx';
                });
            }
        }
        else {

            $('#btnCollectPaymentBack').click(function (e) {
                window.location.href = 'SubmitDetails.aspx';
            });

            let productSelected = sessionStorage.getItem('productSelected');
            if (productSelected != null && productSelected != undefined) {
                productSelected = JSON.parse(productSelected);
                if (productSelected != null && productSelected.ProdCategory != null)
                    $('#txtAmountToPay').val(formatter.format(productSelected.ProdCategory.ServiceFee));
            }
        }

        $('#btnCollectPayment').click(function (e) {
            let cardType = $('#ddlCardType option:selected').val();
            let cardName = $('#txtCardName').val();
            let cardNumber = $('#txtCardNumber').val();
            let cardExpMonth = $('#txtCardExpMonth').val();
            let cardExpYear = $('#txtCardExpYear').val();
            let cardVerifCode = $('#txtCardVerifCode').val();
            let cardZIPCode = $('#txtCardZIPCode').val();
            let amountToPay = $('#txtAmountToPay').val();
            let firstName = '';
            let lastName = '';
            let productSelected = sessionStorage.getItem('productSelected');
            let program = sessionStorage.getItem('clientProgram');
            if (productSelected != null) {
                productSelected = JSON.parse(productSelected);
            }
            if (productSelected != null) {
                let nameparts = cardName.split(' ');
                if (nameparts.length > 0) {
                    firstName = nameparts[0];
                    if (nameparts.length > 1) {
                        lastName = nameparts[1];
                    }
                }

                let customer = sessionStorage.getItem('Customer');
                if (customer != null && customer != undefined && customer != 'undefined') {
                    customer = JSON.parse(customer);
                }
                let ccTrans = {
                    "ID": 0,
                    "FirstName": firstName,
                    "LastName": lastName,
                    "Address": customer.BillingAddress.Line1,
                    "City": customer.BillingAddress.City,
                    "State": customer.BillingAddress.State,
                    "PostalCode": customer.BillingAddress.PostalCode,
                    "Amount": amountToPay.substr(1),
                    "CardNumber": cardNumber,
                    "ExpDate": cardExpMonth + cardExpYear,
                    "CardCode": cardVerifCode,
                    "RSCustomerID": 0,
                    "RSContactID": 0,
                    "RSInvoice": 0,
                    "TransactionDateTime": "",
                    "TransactionID": "",
                    "Response": "",
                    "Status": "",
                    "AuthCode": "",
                    "ClaimID": 0,
                    "CustomerID": customer.ID
                };
                //customer = sessionStorage.getItem('Customer');
                //if (customer != null && customer != undefined && customer != 'undefined') {
                //    customer = JSON.parse(customer);
                //}
                $('#lblError').html('');
                $('#divError').hide();
                $('#lblMessage').html('');
                $('#divBanner').hide();
                if ($('#spnCollectPayment').html().toUpperCase().indexOf('SHIPPING') > -1) {
                    window.location.href = 'DeviceRepair.aspx';
                }
                else {
                    if ((cardType === 'AMEX' && cardNumber.length === 15) || (cardType != 'AMEX' && cardNumber.length === 16)) {
                        if (cardName.length > 0) {
                            if (cardExpMonth.length == 2 && cardExpYear.length == 2) {
                                let cardMonth = parseInt(cardExpMonth);
                                let cardYear = parseInt(cardExpYear);
                                let currentYear = new Date().getFullYear();
                                if (cardMonth >= 1 && cardMonth <= 12) {
                                    if ((cardYear + 2000) >= currentYear) {
                                        var cardCode = parseInt(cardVerifCode);
                                        if ((cardType === 'AMEX' && cardCode > 999 && cardCode < 10000) ||
                                            (cardType != 'AMEX' && cardCode > 99 && cardCode < 1000)) {
                                            if (cardZIPCode.length === 5) {
                                                // Send the credit card transaction.
                                                //let value = '{ "FirstName": "' + customer.PrimaryFirstName + '", "LastName": "' + customer.PrimaryLastName + '", ' +
                                                //    '"Address": "' + customer.BillingAddress.Line1 + '", "City": "' + customer.BillingAddress.City + '", ' +
                                                //    '"State": "' + customer.BillingAddress.State + '", "PostalCode": "' + customer.BillingAddress.PostalCode + '", ' +
                                                //    '"Amount": ' + amountToPay.substr(1) + ', "CardNumber": "' + cardNumber + '", "ExpDate": "' + cardExpMonth + cardExpYear + '", ' +
                                                //    '"CardCode": "' + cardVerifCode + '" } ';
                                                var url = "/api/payment?f=charge";
                                                $('#divWait').show();
                                                $.ajax({
                                                    type: 'POST',
                                                    url: url,
                                                    contentType: 'application/json; charset=utf-8',
                                                    data: JSON.stringify(ccTrans),
                                                    success: function (trans) {
                                                        $('#divWait').hide();
                                                        let invoice2 = sessionStorage.getItem('invoice');
                                                        let response = '';
                                                        if (trans == null) {
                                                            showDialog('Sorry, we were unable to process this transaction.', 'EVSTAR');
                                                        }
                                                        else if (trans.transactionResponse != null) {
                                                            response = trans.transactionResponse;
                                                        }
                                                        else if (trans.Response != null) {
                                                            response = trans.Response;
                                                        }
                                                        if (response != null) {
                                                            response = JSON.parse(response);
                                                            if (response.transactionResponse != null)
                                                                response = response.transactionResponse;

                                                            if (response.responseCode === '1') {
                                                                if (response.messages != null) {
                                                                    if (invoice2 == null || invoice2 == undefined || invoice2.length === 0) {
                                                                        if (productSelected.ProdCategory != null && productSelected.ProdCategory.FulfillmentType === 3) { // In-Home
                                                                            sessionStorage.setItem('repairOption', 'IN-HOME');
                                                                            window.location.href = 'InHomeRepair.aspx';
                                                                        }
                                                                        else {
                                                                            sessionStorage.setItem('authCode', response.authCode);
                                                                            $('#lblMessage').html('Your credit card transaction was approved.  Your confirmation code is ' + response.authCode + '.');
                                                                            $('#divBanner').show();
                                                                            $('#spnCollectPayment').html('Create Shipping Label');
                                                                        }
                                                                    }
                                                                    else {
                                                                        sessionStorage.removeItem('invoice');
                                                                        $('#btnCollectPayment').hide();
                                                                        invoice2 = JSON.parse(invoice2);
                                                                        invoice2.AmountPaid = invoice2.AmountDue;

                                                                        var url = "/api/invoice?";
                                                                        $.ajax({
                                                                            type: 'PUT',
                                                                            url: url,
                                                                            contentType: 'application/json; charset=utf-8',
                                                                            data: JSON.stringify(invoice2),
                                                                            success: function (resp) {
                                                                                sessionStorage.setItem('authCode', response.authCode);
                                                                                $('#lblMessage').html('Your credit card transaction was approved.  Your confirmation code is ' + response.authCode + '.');
                                                                                $('#divBanner').show();
                                                                            },
                                                                            error: function (response) {
                                                                                showDialog('Sorry, we were unable to process this transaction.<br />The response was ' + JSON.stringify(response) + '.', 'EVSTAR');
                                                                            }
                                                                        });
                                                                    }
                                                                }
                                                                else {
                                                                    if (response.transactionResponse.errors != null) {
                                                                        showDialog('Sorry, we were unable to process this transaction.<br />The response was:<br />' +
                                                                            'Error Code: ' + response.transactionResponse.errors[0].errorCode + '<br />' +
                                                                            'Error Message: ' + response.transactionResponse.errors[0].errorText, 'EVSTAR');
                                                                    }
                                                                }
                                                            }
                                                            else {
                                                                if (response != null && response.errors != null) {
                                                                    showDialog('Sorry, we were unable to process this transaction.<br />The response was:<br />' +
                                                                        'Error Code: ' + response.errors[0].errorCode + '<br />' +
                                                                        'Error Message: ' + response.errors[0].errorText, 'EVSTAR');
                                                                }
                                                                else {
                                                                    showDialog('Sorry, we were unable to process this transaction.<br />The response was:<br />' +
                                                                        'Error Code: ' + response.messages.message[0].code + '<br />' +
                                                                        'Error Message: ' + response.messages.message[0].text, 'EVSTAR');
                                                                }
                                                            }
                                                        }
                                                        else {
                                                            if (trans.messages != null && trans.messages.length > 0) {
                                                                showDialog('Sorry, we were unable to process this transaction.<br />The response was ' + trans.messages[0], 'EVSTAR');

                                                            }
                                                            else {
                                                                if (trans.messages != null && trans.messages.message != null && trans.messages.message.length > 0) {
                                                                    showDialog('Sorry, we were unable to process this transaction.<br />The response was ' + trans.messages.message[0].text, 'EVSTAR');
                                                                }
                                                                else {
                                                                    showDialog('Sorry, we were unable to process this transaction.<br />The response was ' + JSON.stringify(response), 'EVSTAR');
                                                                }
                                                            }
                                                        }
                                                    },
                                                    error: function (response) {
                                                        showDialog('Sorry, we were unable to process this transaction.<br />The response was ' + JSON.stringify(response), 'EVSTAR');
                                                        $('#divWait').hide();
                                                    }
                                                });
                                            }
                                            else {
                                                showDialog('Please enter a valid five-digit ZIP code.', 'EVSTAR');
                                            }

                                        }
                                        else {
                                            showDialog('Please enter a valid card verification code.', 'EVSTAR');
                                        }
                                    }
                                    else {
                                        showDialog('Please enter a valid year for card expiration.', 'EVSTAR');
                                    }
                                }
                                else {
                                    showDialog('Please enter a valid month for card expiration.', 'EVSTAR');
                                }
                            }
                            else {
                                showDialog('Please enter a two-digit month and a two-digit year for card expiration.', 'EVSTAR');
                            }
                        }
                        else {
                            showDialog('Please enter the name of the cardholder.', 'EVSTAR');
                        }
                    }
                    else {
                        showDialog('Please enter a valid card number for the type of card you selected.', 'EVSTAR');
                    }
                }
            }
            else {
                showDialog('Your session seems to be expired. Please log in and try again.');
            }
        });
    }
    else if (curPage.includes("/CLAIMCOMPLETE") || curPage.includes("\\CLAIMCOMPLETE")) {
        $('#lblError').html('');
        $('#divError').hide();

        //if (isDobson) {
        //    $('#dobsonlogo').show();
        //}
        //else {
        //    $('#dobsonlogo').hide();
        //}

        let program = sessionStorage.getItem('clientProgram');
        if (program == 'EVSTAR') {
            $('#divMailRepair').hide();
            $('#divLocalRepair').hide();
            $('#divHomeRepair').hide();
            $('#divEVSTAR').show();
        }
        else {
            let repairOption = sessionStorage.getItem('repairOption');
            if (repairOption === 'LOCAL') {
                $('#divMailRepair').hide();
                $('#divLocalRepair').show();
                $('#divHomeRepair').hide();
                $('#divEVSTAR').hide();
            }
            else if (repairOption === 'IN-HOME') {
                $('#divMailRepair').hide();
                $('#divLocalRepair').hide();
                $('#divHomeRepair').show();
                $('#divEVSTAR').hide();
            }
            else {
                $('#divMailRepair').show();
                $('#divLocalRepair').hide();
                $('#divEVSTAR').hide();
                $('#divHomeRepair').hide();
                shippingLabel = sessionStorage.getItem('shippingLabel');
                if (shippingLabel != null && shippingLabel.length > 0) {
                    $('#lnkShipping').attr('href', shippingLabel);
                    $('#lblShipping').html(shippingLabel);
                }
            }
        }
        let customer = sessionStorage.getItem('Customer');
        if (customer != null && customer != undefined && customer != 'undefined') {
            customer = JSON.parse(customer);
            $('#divWait').show();
            const timeElapsed = Date.now();
            const today = new Date(timeElapsed);

            let productSelected = sessionStorage.getItem('productSelected');
            if (productSelected != null && productSelected != undefined && productSelected != 'undefined') {
                productSelected = JSON.parse(productSelected);
                let perilSelected = sessionStorage.getItem('perilSelected');
                let subcategorySelected = sessionStorage.getItem('subcategorySelected');
                let lossDescription = sessionStorage.getItem('lossDescription');
                let dateSelected = sessionStorage.getItem('dateSelected');
                let nameOnPlan = sessionStorage.getItem('nameOnPlan');
                var addressSelected = sessionStorage.getItem('addressSelected');
                let addressSelectedId = 0;
                if (addressSelected != null && addressSelected.startsWith('{')) {
                    addressSelected = JSON.parse(addressSelected);
                    addressSelectedId = addressSelected != null ? addressSelected.ID : 0;
                }
                let passcodeDisabled = sessionStorage.getItem('passcodeDisabled');
                let passcode = sessionStorage.getItem('passcode');
                let repairOption = sessionStorage.getItem('repairOption');

                let claim = sessionStorage.getItem('curClaim');
                let claimStr = "";
                if (claim != null && claim != undefined && claim.length > 0)
                    claimStr = JSON.parse(claim);
                else {
                    claimStr = '{ "ID": 0, "CustomerID": {0}, "CoveredProductID": {1}, "LocalRepair": {2}, "AddressID": {3}, "DateSubmitted": "{4}", "DateCompleted": null, ' +
                        '"DateReturned": null, "DateReceivedAtTCS": null, "CoveredPerilID": {5}, "PasscodeDisabled": {6}, "PassCode": "{7}", "EventDate": "{8}", ' +
                        '"RepairVendorID": 0, "InboundTrackingNumber": "", "OutboundTrackingNumber": "", "PerilSubcategoryID": "{9}", "DateOfLoss": "{8}" }';
                    claimStr = claimStr.replace('{0}', customer.ID).replace('{1}', productSelected.ID).replace('{2}', (repairOption === 'LOCAL' ? 1 : 0));
                    claimStr = claimStr.replace('{3}', addressSelectedId).replace('{4}', today.toISOString()).replace('{5}', perilSelected);
                    claimStr = claimStr.replace('{6}', (passcodeDisabled != null ? passcodeDisabled : 0)).replace('{7}', (passcode != null ? passcode : "")).replace('{8}', dateSelected);
                    claimStr = claimStr.replace('{9}', (subcategorySelected != null ? subcategorySelected : 0));
                }

                let hashed = sessionStorage.getItem('hashed');
                let code = sessionStorage.getItem('code');
                let method = sessionStorage.getItem('method');

                let headers = {};
                let url = "/api/claim";
                if (method == "email") {
                    headers = { "address": customer.Email, "hashed": hashed, "code": code };
                } else if (method == "sms") {
                    headers = { "phone": customer.MobileNumber, "hashed": hashed, "code": code };
                }

                // Save the claim to the database
                $.ajax({
                    type: 'POST',
                    url: url,
                    headers: headers,
                    data: JSON.stringify(JSON.parse(claimStr)),
                    contentType: 'application/json; charset=utf-8',
                    success: function (response) {
                        if (response != null) {
                            let claim = response;
                            if (claim.DenialReason.length > 0) {
                            }
                            else {
                                SendConfirmationEmail(customer, claim);
                                sessionStorage.removeItem('productSelected');
                                sessionStorage.removeItem('perilSelected');
                                sessionStorage.removeItem('subcategorySelected');
                                sessionStorage.removeItem('lossDescription');
                                sessionStorage.removeItem('dateSelected');
                                sessionStorage.removeItem('nameOnPlan');
                                sessionStorage.removeItem('addressSelected');
                                sessionStorage.removeItem('passcodeDisabled');
                                sessionStorage.removeItem('passcode');
                                sessionStorage.setItem('curClaim', '');
                            }
                        }
                        else {
                            $('#lblError').html('An error occurred while saving the claim.<br />');
                            $('#divError').show();
                        }
                        $('#divWait').hide();
                    },
                    error: function (response) {
                        $('#lblError').html('An error occurred while saving the claim.<br />' + response);
                        $('#divError').show();
                        $('#divWait').hide();
                    }
                });
            }
            else {
                showDialog("Unable to file the claim.");
                $('#divWait').hide();
            }
        }
        else {
            $('#divWait').hide();
        }

        $('#btnClaimCompleteContinue').click(function (e) {
            window.location.href = 'CustomerLanding.aspx';
        });
    }
    else if (curPage.includes("/MANUFACTURERS") || curPage.includes("\\MANUFACTURERS")) {
        var repairOption = sessionStorage.getItem('repairOption');
        if (repairOption === 'LOCAL') {

        }
        else {
            //$('#divEmailSent').show();
        }
        if (customer != null && customer != undefined && customer != 'undefined') {
            //customer = JSON.parse(customer);

            //    const timeElapsed = Date.now();
            //    const today = new Date(timeElapsed);

            //    var productSelected = sessionStorage.getItem('productSelected');
            //    productSelected = JSON.parse(productSelected);
            //    var perilSelected = sessionStorage.getItem('perilSelected');
            //    var subcategorySelected = sessionStorage.getItem('subcategorySelected');
            //    var lossDescription = sessionStorage.getItem('lossDescription');
            //    var dateSelected = sessionStorage.getItem('dateSelected');
            //    var nameOnPlan = sessionStorage.getItem('nameOnPlan');
            //    //var addressSelected = sessionStorage.getItem('addressSelected');
            //    addressSelected = null;
            //    //addressSelected = JSON.parse(addressSelected);
            //    var passcodeDisabled = sessionStorage.getItem('passcodeDisabled');
            //    var passcode = sessionStorage.getItem('passcode');

            //    var claimStr = '{ "ID": 0, "CustomerID": {0}, "CoveredProductID": {1}, "LocalRepair": {2}, "AddressID": {3}, "DateSubmitted": "{4}", "DateCompleted": null, ' +
            //        '"DateReturned": null, "DateReceivedAtTCS": null, "CoveredPerilID": {5}, "PasscodeDisabled": {6}, "PassCode": "{7}", "DateOfEvent": "{8}", ' +
            //        '"RepairVendorID": 0, "InboundTrackingNumber": "", "OutboundTrackingNumber": "", "PerilSubcategoryID": "{9}", "DenialReason": "Manufacturers Warranty", ' +
            //        '"DateDenied": "{10}" }';
            //    claimStr = claimStr.replace('{0}', customer.ID).replace('{1}', productSelected.ID).replace('{2}', (repairOption === 'LOCAL' ? 1 : 0));
            //    claimStr = claimStr.replace('{3}', (addressSelected != null ? addressSelected.ID : 0)).replace('{4}', today.toISOString()).replace('{5}', perilSelected);
            //    claimStr = claimStr.replace('{6}', (passcodeDisabled != null ? passcodeDisabled : 0)).replace('{7}', (passcode != null ? passcode : "")).replace('{8}', dateSelected);
            //    claimStr = claimStr.replace('{9}', (subcategorySelected != null ? subcategorySelected : 0)).replace('{10}', today.toISOString());

            //    var url = '';
            //    var hashed = sessionStorage.getItem('hashed');
            //    var code = sessionStorage.getItem('code');
            //    var method = sessionStorage.getItem('method');

            //    if (method == "email") {
            //        url = "/api/claim?address=" + customer.Email + "&hashed=" + hashed + "&code=" + code;
            //    } else if (method == "sms") {
            //        url = "/api/claim?phone=" + customer.MobileNumber + "&hashed=" + hashed + "&code=" + code;
            //    }

            //    // Save the claim to the database
            //    $.ajax({
            //        type: 'POST',
            //        url: url,
            //        data: JSON.stringify(JSON.parse(claimStr)),
            //        contentType: 'application/json; charset=utf-8',
            //        success: function (response) {
            //            if (response != null) {
            //                sessionStorage.removeItem('productSelected');
            //                sessionStorage.removeItem('perilSelected');
            //                sessionStorage.removeItem('subcategorySelected');
            //                sessionStorage.removeItem('lossDescription');
            //                sessionStorage.removeItem('dateSelected');
            //                sessionStorage.removeItem('nameOnPlan');
            //                sessionStorage.removeItem('addressSelected');
            //                sessionStorage.removeItem('passcodeDisabled');
            //                sessionStorage.removeItem('passcode');
            //            }
            //            else {
            //                $('#lblError').html(response);
            //                $('#divError').show();
            //            }
            //            $('#divWait').hide();
            //        },
            //        error: function (response) {
            //            $('#lblError').html('An error occurred while saving the claim.<br />' + response);
            //            $('#divError').show();
            //            $('#divWait').hide();
            //        }
            //    });
        }
    }
    else if (curPage.includes("/RESET.") || curPage.includes("\\RESET.")) {
        $('#mainBody').css('width', '1200px');
        $('#txtPassword1').val();
        $('#txtPassword2').val();

        $('#btnReset').click(function (e) {
            let pw1 = $('#txtPassword').val();
            let pw2 = $('#txtPassword2').val();
            let email = $('#txtCustEmail').val();
            if (pw1 != pw2) {
                alert('The passwords must be the same.');
            }
            else {
                if (validatePassword(pw1)) {
                    $.ajax({
                        url: "/api/user/",
                        headers: { "username": email, "pwd": pw1 },
                        dataType: "json",
                        type: "PUT",
                        contentType: "application/json; charset=utf-8",
                        success: function (data) {
                            if (data != null) {
                                user = data;
                                if (user.Error != null && user.Error.length > 0) {
                                    alert(user.Error);
                                }
                                else {
                                    sessionStorage.setItem('selUser', JSON.stringify(user));
                                    alert('Your password was changed successfully.');
                                    window.location.href = 'Login.aspx';
                                }
                            }
                            return true;
                        },
                        error: function (XMLHttpRequest, textStatus, errorThrown) {
                            alert(XMLHttpRequest.responseJSON.Message + '\r\n' + textStatus);
                            return false;
                        }
                    });
                }
                else {

                }
            }
        });
    }
    else if (curPage.includes("\\ADMINLOGIN") || curPage.includes("/ADMINLOGIN")) {
        $('#mainBody').css('width', '960px');
        $('#divMyAccount').hide();
        $('#btnSendAdminLogin').enable();

        $('#btnSendAdminLogin').click(function (e) {

            $('#divError').hide();
            $('#lblError').html('');
            let code = $('#txtLoginPassword').val();
            let email = $('#txtLoginUsername').val();
            $.ajax({
                type: 'GET',
                url: "/api/user",
                headers: { "username": email, "auth": code },
                contentType: 'application/json; charset=utf-8',
                success: function (response) {
                    response = JSON.parse(response);
                    if (response == null && response.data != null && response.data.length > 0) {
                        errorCount++;
                        if (errorCount < 2)
                            $('#lblError').html('Sorry, we were unable to verify your account. Please check the email address and password then try again.');
                        else {
                            $('#lblError').html('Sorry, we were unable to verify your account. Please contact us via email at haudiss@evstar.com.');
                            errorCount = 0;
                        }
                        $('#divError').show();
                    }
                    else {
                        if (response.data[0].Error.includes('NOTFOUND')) {
                            errorCount++;
                            if (errorCount < 2)
                                $('#lblError').html('Sorry, we were unable to verify your account. Please check the email address and password then try again.');
                            else {
                                $('#lblError').html('Sorry, we were unable to verify your account. Please contact us via email at haudiss@evstar.com.');
                                errorCount = 0;
                            }
                            $('#divError').show();
                        }
                        else if (response.data[0].Error != null) {
                            errorCount++;
                            if (errorCount < 2)
                                $('#lblError').html(response[0].Error);
                            else {
                                $('#lblError').html(response[0].Error);
                                errorCount = 0;
                            }
                            $('#divError').show();
                        }
                        else {
                            if (response.data[0].ParentClient != null) {
                                if (response.data[0].Reset) {
                                    window.location.href = 'Reset.aspx';
                                }
                                else {
                                    selUserID = response.data[0].ID;
                                    sessionStorage.setItem('selUser', JSON.stringify(response.data[0]));
                                    window.location.href = 'AdminVerify.aspx';
                                }
                            }
                            else {
                                selContactID = '0';
                                customer = null;
                                selCustName = '';
                                errorCount++;
                                if (errorCount < 2)
                                    $('#lblError').html('Sorry, we were unable to verify your account. Please check the email address and password then try again.');
                                else {
                                    $('#lblError').html('Sorry, we were unable to verify your account. Please contact us via email at haudiss@techcyclesolutions.com.');
                                    errorCount = 0;
                                }
                                $('#divError').show();
                            }
                        }
                    }
                },
                error: function (response) {
                    errorCount++;
                    if (errorCount < 2)
                        $('#lblError').html('Sorry, we were unable to verify your account. Please check the email address and password then try again.');
                    else {
                        $('#lblError').html('Sorry, we were unable to verify your account. Please contact us via email at haudiss@techcyclesolutions.com.');
                        errorCount = 0;
                    }
                    $('#divError').show();
                }
            });
        });
    }

    $('#btnLandingBack').click(function (e) {
        window.location.href = "Verify.aspx";
    });

    $('#btnProductBack').click(function (e) {
        window.location.href = "CustomerLanding.aspx";
    });

    $('#btnActionBack').click(function (e) {
        //        let category = sessionStorage.getItem('category');
        //        if (category != null && category != undefined && category.length > 0)
        //            window.location.href = 'CustomerProduct.aspx?category=' + category;
        //        else
        window.location.href = "CustomerLanding.aspx";
    });

    $('#btnRegCancel').click(function (e) {
        window.location.href = "Default.aspx";
    });

    $('#btnMfrWarr').click(function (e) {
        window.location.href = 'CustomerLanding.aspx';
    });

    $('#btnLogout').click(function (e) {
        sessionStorage.removeItem('Customer');
        sessionStorage.removeItem('productSelected');
        sessionStorage.removeItem('perilSelected');
        sessionStorage.removeItem('subcategorySelected');
        sessionStorage.removeItem('lossDescription');
        sessionStorage.removeItem('dateSelected');
        sessionStorage.removeItem('nameOnPlan');
        sessionStorage.removeItem('addressSelected');
        sessionStorage.removeItem('passcodeDisabled');
        sessionStorage.removeItem('passcode');
        sessionStorage.removeItem('hashed');
        sessionStorage.removeItem('code');
        sessionStorage.removeItem('method');
        window.location.href = 'Default.aspx';
    });


    $('#btnRegister').click(function (e) {
        $('#divError').hide();
        var email = $('#txtRegEmail').val();
        var pass1 = $('#txtRegPassword1').val();
        var pass2 = $('#txtRegPassword2').val();

        if (email.trim() === '' || pass1.trim() === '' || pass2.trim() === '') {
            $('#lblError').html('You are missing one or more required values.');
            $('#divError').show();
        }
        else {
            if (pass1.trim() != pass2.trim()) {
                $('#lblError').html('Your passwords do not match.');
                $('#divError').show();
            }
            else {
                $.ajax({
                    type: 'POST',
                    url: "/api/customer?function=REGISTER",
                    headers: { "email": email, "password": pass1, "confirm": pass2 },
                    contentType: 'application/json; charset=utf-8',
                    success: function (response) {
                        if (response == null || (response.Result != null && response.Result.includes("NOTFOUND"))) {
                            $('#lblError').html('Sorry, we were unable to find your information. Please check your entries and try again.');
                            $('#divError').show();
                        }
                        else if (response == "ALREADYREGISTERED") {
                            sessionStorage.setItem('error', 'Our records indicate you are already registered.  Please log in if you wish to file a claim.');
                            window.location.href = "Login.aspx";
                        }
                        else if (response.toUpperCase().includes("EXCEPTION")) {
                            $('#lblError').html(response);
                            $('#divError').show();
                        }
                        else {
                            alert('Your registration was successful');
                            $('#divError').hide();
                            $('#divLogin').hide();
                            $('#divLanding').show();
                            $.ajax({
                                type: 'GET',
                                url: "/api/customer?function=LOGIN",
                                headers: { "email": email, "password": pass1 },
                                contentType: 'application/json; charset=utf-8',
                                success: function (response) {
                                    if (response == null || (response.Result != null && response.Result.includes("NOTFOUND"))) {
                                        $('#lblError').html('Sorry, we were unable to find your information using those credentials. Please check your entries and try again.');
                                        $('#divError').show();
                                    }
                                    else if (response.Result == "INVALID") {
                                        $('#lblError').html('Incorrect login or password. Please check your entries and try again.');
                                        $('#divError').show();
                                    }
                                    else if (response.Result != null && response.Result.toUpperCase().includes("EXCEPTION")) {
                                        $('#lblError').html(response);
                                        $('#divError').show();
                                    }
                                    else {
                                        response.MobileNumber = response.MobileNumber.replaceAll('-', '').replaceAll('(', '').replaceAll(')', '').replaceAll(' ', '');
                                        var customer = JSON.stringify(response);
                                        sessionStorage.setItem('Customer', customer);
                                        setUpVerification(response);
                                    }
                                },
                                error: function (response) {
                                    $('#lblError').html('Sorry, a technical issue prevented us from finding your information. Please try again later.');
                                    $('#divError').show();
                                }
                            });
                        }
                    },
                    error: function (response) {
                        $('#lblError').html('Sorry, a technical issue prevented us from finding your information. Please try again later.');
                        $('#divError').show();
                    }
                });
            }
        }
    });

    $('#btnReset').click(function (e) {
        $('#divError').hide();
        var email = $('#txtRegEmail').val();
        var pass1 = $('#txtRegPassword1').val();
        var pass2 = $('#txtRegPassword2').val();

        if (email.trim() === '' || pass1.trim() === '' || pass2.trim() === '') {
            $('#lblError').html('You are missing one or more required values.');
            $('#divError').show();
        }
        else {
            if (pass1.trim() != pass2.trim()) {
                $('#lblError').html('Your passwords do not match.');
                $('#divError').show();
            }
            else {
                $.ajax({
                    type: 'POST',
                    url: "/api/customer?function=RESET",
                    headers: { "email": email, "password": pass1, "confirm": pass2 },
                    contentType: 'application/json; charset=utf-8',
                    success: function (response) {
                        if (response == null || (response.Result != null && response.Result.includes("NOTFOUND"))) {
                            $('#lblError').html('Sorry, we were unable to find your information. Please check your entries and try again.');
                            $('#divError').show();
                        }
                        else if (response.toUpperCase().includes("EXCEPTION")) {
                            $('#lblError').html(response);
                            $('#divError').show();
                        }
                        else {
                            alert('Your registration was successful');
                            $('#divError').hide();
                            $('#divLogin').hide();
                            $('#divLanding').show();
                            $.ajax({
                                type: 'GET',
                                url: "/api/customer?function=LOGIN",
                                headers: { "email": email, "password": pass1 },
                                contentType: 'application/json; charset=utf-8',
                                success: function (response) {
                                    if (response == null || (response.Result != null && response.Result.includes("NOTFOUND"))) {
                                        $('#lblError').html('Sorry, we were unable to find your information using those credentials. Please check your entries and try again.');
                                        $('#divError').show();
                                    }
                                    else if (response.Result == "INVALID") {
                                        $('#lblError').html('Incorrect login or password. Please check your entries and try again.');
                                        $('#divError').show();
                                    }
                                    else if (response.Result != null && response.Result.toUpperCase().includes("EXCEPTION")) {
                                        $('#lblError').html(response);
                                        $('#divError').show();
                                    }
                                    else {
                                        response.MobileNumber = response.MobileNumber.replaceAll('-', '').replaceAll('(', '').replaceAll(')', '').replaceAll(' ', '');
                                        var customer = JSON.stringify(response);
                                        sessionStorage.setItem('Customer', customer);
                                        setUpVerification(response);
                                    }
                                },
                                error: function (response) {
                                    $('#lblError').html('Sorry, a technical issue prevented us from finding btnyour information. Please try again later.');
                                    $('#divError').show();
                                }
                            });
                        }
                    },
                    error: function (response) {
                        $('#lblError').html('Sorry, a technical issue prevented us from finding your information. Please try again later.');
                        $('#divError').show();
                    }
                });
            }
        }
    });

    $('#btnClaimCompleteNo').click(function (e) {
        window.location.href = "ThankYou.aspx";
        //window.close();
    });

    $('#btnClaimCompleteYes').click(function (e) {
        window.location.href = "Verify.aspx";
        //$('#txtFirstName').focus();
    });

    $('#btnDescribeEvent').click(function (e) {
        var dateSelected = $('#datepicker').val();
        var perilSelected = $('#ddlPerils option:selected').val();
        var perilText = '';
        var subcategorySelected = $('#ddlSubcategories option:selected').val();
        var subcategoryText = '';

        for (var i = 0; i < perils.length; i++) {
            if (perils[i].ID == perilSelected) {
                perilText = perils[i].Peril;
                sessionStorage.setItem('perilSelected', perils[i].ID.toString());

                if (perils[i].SubcategoryID == subcategorySelected) {
                    subcategoryText = perils[i].Subcategory;
                    sessionStorage.setItem('subcategorySelected', perils[i].SubcategoryID.toString());
                    break;
                }
            }
        }

        lossDescription = $('#txtClaimDescription').val();
        $('#divError').hide();

        var customer = sessionStorage.getItem('Customer');
        if (customer != null && customer != undefined && customer != 'undefined') {
            customer = JSON.parse(customer);

            addressSelected = customer.MailingAddress.Line1 + '<br />';
            if (customer.MailingAddress.Line2.length > 0)
                addressSelected += customer.MailingAddress.Line2 + '<br />';
            addressSelected += customer.MailingAddress.City + ', ' + customer.MailingAddress.State + ' ' + customer.MailingAddress.PostalCode;

            sessionStorage.setItem('perilText', perilText);
            sessionStorage.setItem('subcategoryText', subcategoryText);
            sessionStorage.setItem('lossDescription', lossDescription);
            sessionStorage.setItem('dateSelected', dateSelected);
            sessionStorage.setItem('nameOnPlan', customer.PrimaryFirstName + ' ' + customer.PrimaryLastName)
            sessionStorage.setItem('addressSelected', addressSelected);

            var prodSelected = sessionStorage.getItem('productSelected');
            prodSelected = JSON.parse(prodSelected);
            var purchaseDate = prodSelected.PurchaseDate;
            let purchdate = new Date(purchaseDate.substr(0, 10));
            // use customer enrollmentdate instead.
            //let covgdate = new Date(prodSelected.CoverageDate.substr(0, 10));
            let covgdate = new Date(customer.EnrollmentDate.substr(0, 10));
            let currentDate = new Date(dateSelected);
            let newDate = new Date(purchdate.valueOf());
            newDate.setDate(purchdate.getDate() + 365);
            //if (newDate.valueOf() > currentDate.valueOf() && perilSelected == '1')
            //    window.location.href = 'ManufacturersWarranty.aspx';
            //else
            if (covgdate.valueOf() > currentDate.valueOf())
                showDialog('You cannot file a claim for an event that happened before the date your coverage started.', 'EVSTAR');
            else
                window.location.href = 'SubmitDetails.aspx';
        }
        else {
            showDialog('Sorry, your session appears to have expired. Please click the link to return to the home page.</br></br><a href="/">Home</a>');
        }
    });

    $('#btnViewCoverage').click(function (e) {
        $('#divError').hide();
        window.location.href = 'ProductTerms.aspx';
    });

    $('#btnSelectProductBack').click(function (e) {
        window.location.href = "VerifyCode.aspx";
    });

    $('#btnAdminVerifyCodeBack').click(function (e) {
        window.location.href = "AdminVerify.aspx";
    });

    $('#btnLookupCustomer').click(function (e) {
        $('#divError').hide();

        var firstName = $('#txtFirstName').val();
        var lastName = $('#txtLastName').val();
        var phoneNumber = $('#txtPhoneNumber').val();
        phoneNumber = phoneNumber.replaceAll('-', '').replaceAll('(', '').replaceAll(')', '').replaceAll(' ', '');
        var postalCode = $('#txtPostalCode').val();
        $.ajax({
            type: 'GET',
            url: "/api/customer?first=" + firstName + "&last=" + lastName + "&zip=" + postalCode + "&phone=" + phoneNumber,
            contentType: 'application/json; charset=utf-8',
            success: function (response) {
                if (response == null) {
                    $('#lblError').html('Sorry, we were unable to find your information. Please check your entries and try again.');
                    $('#divError').show();
                }
                else {
                    window.location.href = 'StartClaim.aspx?clear=1';
                }
            },
            error: function (response) {
                $('#lblError').html('Sorry, a technical issue prevented us from finding your information. Please try again later.');
                $('#divError').show();
            }
        });
    });

    $('#radVerSms').mouseout(function () {
        var selectedOption = $("input:radio[name=verification]:checked").val()
        if (selectedOption != "sms") {
            $('#radVerSms').css('border-color', 'lightgray');
        }
    });

    $('#radVerEmail').mouseout(function () {
        var selectedOption = $("input:radio[name=verification]:checked").val()
        if (selectedOption != "email") {
            $('#radVerEmail').css('border-color', 'lightgray');
        }
    });

    $('#radVerSms').mouseover(function () {
        $('#radVerSms').css('border-color', primaryColor);
    });

    $('#radVerEmail').mouseover(function () {
        $('#radVerEmail').css('border-color', primaryColor);
    });

    $('input[type=radio][name=verification]').change(function () {
        var selectedOption = $("input:radio[name=verification]:checked").val()
        if (selectedOption == "email") {
            $('#radVerSms').css('border-color', 'lightgray');
            $('#radVerEmail').css('border-color', primaryColor);
        } else if (selectedOption == "sms") {
            $('#radVerEmail').css('border-color', 'lightgray');
            $('#radVerSms').css('border-color', primaryColor);
        }
    });

    $('#radRepairLocal').mouseout(function () {
        var selectedOption = $("input:radio[name=repair]:checked").val()
        if (selectedOption != "local") {
            $('#radRepairLocal').css('border-color', 'lightgray');
        }
    });

    $('#radRepairSendIn').mouseout(function () {
        var selectedOption = $("input:radio[name=repair]:checked").val()
        if (selectedOption != "sendin") {
            $('#radRepairSendIn').css('border-color', 'lightgray');
        }
    });

    $('#radRepairSendIn').mouseover(function () {
        $('#radRepairSendIn').css('border-color', primaryColor);
    });

    $('#radRepairLocal').mouseover(function () {
        $('#radRepairLocal').css('border-color', primaryColor);
    });

    $('input[type=radio][name=repair]').change(function () {
        var selectedOption = $("input:radio[name=repair]:checked").val();
        repairOption = selectedOption;
        sessionStorage.setItem('repairOption', repairOption);
        if (selectedOption == "local") {
            $('#radRepairSendIn').css('border-color', 'lightgray');
            $('#radRepairLocal').css('border-color', primaryColor);
        } else if (selectedOption == "sendin") {
            $('#radRepairLocal').css('border-color', 'lightgray');
            $('#radRepairSendIn').css('border-color', primaryColor);
        }
    });

    $('#btnAdminVerifyCode').click(function (e) {
        let code = $('#txtVerificationCode').val();
        let hashed = sessionStorage.getItem('hashed');
        let method = sessionStorage.getItem('method');

        let user = sessionStorage.getItem('selUser');
        if (user != null && user.length > 0) {
            user = JSON.parse(user);
            if (user != null)
                email2 = user.Email;
        }
        var url = '';
        if (method == "email") {
            url = "/api/email?address=" + email2 + "&hashed=" + hashed + "&code=" + code;

        } else if (method == "sms") {
            url = "/api/sms?phone=" + user.PhoneNumber + "&hashed=" + hashed + "&code=" + code;
        }
        $.ajax({
            type: 'GET',
            url: url,
            contentType: 'application/json; charset=utf-8',
            success: function (response) {
                if (response == null) {
                    $('#lblError').html('Sorry, we were unable to verify the code. Please check the code and try again.');
                    $('#divError').show();
                }
                else {
                    if (response == 'FALSE') {
                        $('#lblError').html('Sorry, we were unable to verify the code. Please check the code and try again.');
                        $('#divError').show();
                    }
                    else {
                        sessionStorage.setItem('code', code);
                        window.location.href = 'Admin.aspx';
                    }
                }
            },
            error: function (response) {
                $('#lblError').html('Sorry, we were unable to verify the code. Please check the code and try again.');
                $('#divError').show();
            }
        });
    });

    $("#ddlProducts").change(
        function () {
            var products = sessionStorage.getItem('products');
            products = JSON.parse(products);

            var productId = $('#ddlProducts option:selected').val();
            if (productId == '99999999') {
                loadProductCategories();
            }
            else {
                $('#divSelectedProduct').show();
                $('#divAddProduct').hide();
                for (var i = 0; i < products.length; i++) {
                    if (products[i].ID == productId)
                        displaySelectedProduct(products[i]);
                }
            }
        }
    );

    $("#ddlPerils").change(
        function () {
            var perilId = $('#ddlPerils option:selected').val();
            let count = 0;
            let hideIt = false;
            $('#ddlSubcategories').empty();
            for (i = 0; i < perils.length; i++) {
                if (perils[i].ID == perilId) {
                    count++;
                    addDropdownOption(perils[i].Subcategory, perils[i].SubcategoryID.toString(), 'ddlSubcategories');
                    if (perils[i].Subcategory.toUpperCase() === perils[i].Peril.toUpperCase())
                        hideIt = true;
                }
            }
            if (count === 0 || hideIt)
                $('#divSubcategories').hide();
            else
                $('#divSubcategories').show();


            if (perilId == '1')
                $('#lblWhatHappened').html('Describe what is wrong with the device');
            else
                $('#lblWhatHappened').html('Describe what happened to the device');

        }
    );

    $("#ddlSubcategories").change(
        function () {
        }
    );

    $('input[type=radio][name=repairaddress]').change(function () {
        var selectedOption = $("input:radio[name=repairaddress]:checked").val()
        if (selectedOption == "account") {
            useCustomAddress = false;
            $('#divAccountRepairAddress').show();
            $('#divCustomRepairAddress').hide();
        } else if (selectedOption == "custom") {
            useCustomAddress = true;
            $('#divAccountRepairAddress').hide();
            $('#divCustomRepairAddress').show();
            $('txtCustomAddress1').focus();
        }
    });

    $('input[type=radio][name=security]').change(function () {
        var selectedOption = $("input:radio[name=security]:checked").val()
        if (selectedOption == "disabled") {
            passcodeDisabled = true;
            $('#txtPasscode').disable();
        } else if (selectedOption == "passcode") {
            passcodeDisabled = false;
            $('#txtPasscode').enable();
            $('#txtPasscode').focus();
        }
        sessionStorage.setItem('passcodeDisabled', passcodeDisabled);
    });

    $('#btnAddDevice').click(
        function () {
            var products = sessionStorage.getItem('products');
            products = JSON.parse(products);

            loadProductCategories();
        }
    );
});

function convertDate(date) {
    var yyyy = date.getFullYear().toString();
    var mm = (date.getMonth() + 1).toString();
    var dd = date.getDate().toString();

    var mmChars = mm.split('');
    var ddChars = dd.split('');

    return yyyy + '-' + (mmChars[1] ? mm : "0" + mmChars[0]) + '-' + (ddChars[1] ? dd : "0" + ddChars[0]);
}

function GetClientInfo(clientProgram) {
    $('#divError').hide();

    $.ajax({
        type: 'GET',
        url: '/api/client/?program=' + clientProgram,
        contentType: 'application/json; charset=utf-8',
        success: function (response) {
            if (response == null || response.length == 0) {
                $('#lblError').html('Sorry, we are experiencing technical difficulties.');
                $('#divError').show();
            }
            else {
                client = response;
                if (client != null && client.length > 0) {
                    $('#logo').attr('src', client[0].LogoFile);
                    $('#logo').attr('alt', client[0].Name);
                    $('#logo').show();
                    sessionStorage.setItem('client', JSON.stringify(client[0]));
                }
            }
        },
        error: function (response) {
            $('#lblError').html('Sorry, we are experiencing technical difficulties.');
            $('#divError').show();
        }
    });
}

function loadProductCategories() {
    var productCategories = {};
    var code = sessionStorage.getItem('code');
    var hashed = sessionStorage.getItem('hashed');
    var method = sessionStorage.getItem('method');
    var customer = sessionStorage.getItem('Customer');
    if (customer != null && customer != undefined && customer != 'undefined') {
        customer = JSON.parse(customer);

        if (customer != null) {
            let headers = {};
            let url = "";
            if (method == "email") {
                url = "/api/category?program=" + customer.ProgramID;
                headers = { "address": customer.Email, "hashed": hashed, "code": code };
            } else if (method == "sms") {
                url = "/api/category?program=" + customer.ProgramID;
                headers = { "phone": customer.MobileNumber, "hashed": hashed, "code": code };
            }
            $.ajax({
                type: 'GET',
                url: url,
                headers: headers,
                contentType: 'application/json; charset=utf-8',
                success: function (response) {
                    if (response == null) {
                        $('#lblError').html('Sorry, an error has occurred.');
                        $('#divError').show();
                    }
                    else {
                        productCategories = response;
                        sessionStorage.setItem('productCategories', JSON.stringify(productCategories));

                        $('#divSelectedProduct').hide();
                        $('#divAddProduct').show();

                        $('#ddlProductCategories').empty();
                        for (i = 0; i < response.length; i++) {
                            addDropdownOption(response[i].CategoryName + ' ($' + response[i].ServiceFee + ')', response[i].ID.toString(), 'ddlProductCategories');
                        }
                    }
                },
                error: function (response) {
                    $('#lblError').html('Sorry, an error has occurred: ' + response);
                    $('#divError').show();
                }
            });
        }
        else {
            $('#lblError').html('Sorry, your session appears to have expired. Please click the link to return to the home page.</br></br><a href="/">Home</a>');
            $('#divError').show();
        }
    }
    else {
        $('#lblError').html('Sorry, your session appears to have expired. Please click the link to return to the home page.</br></br><a href="/">Home</a>');
        $('#divError').show();
    }
}

function setUpProductIdentification() {
    //$('#ddlProducts').focus();
    var products = {};
    //$('#lblManufacturer').html('');
    //$('#lblModel').html('');
    //$('#lblColor').html('');
    //$('#lblSerialNumber').html('');
    //$('#lblProductAdded').html('');
    //$('#hdnProductID').val('0');
    //$('#ddlProducts').empty();

    $('#btnCancelDevice').click(function () {
        window.location.href = 'AddProduct.aspx';
    });

    $('#btnSaveDevice').click(function () {
        //var productCategoryId = $('#ddlProductCategories option:selected').val();
        //var productCategoryName = $('#ddlProductCategories option:selected').text();
        const urlParams = new URLSearchParams(window.location.search);
        let productCategoryId = urlParams.get('category');
        let productCategoryName = '';

        var make = $('#txtNewProductMake').val();
        make = make.replace('"', '\"')
        var model = $('#txtNewProductModel').val();
        model = model.replace('"', '\"');
        //var desc = $('#txtNewProductDescription').val();
        var color = $('#txtNewProductColor').val();
        var serial = $('#txtNewProductSerial').val();
        serial = serial.replace('"', '\"');
        //var purchaseDate = $('#txtNewProductPurchDate').val();

        if (make.trim().length === 0 || model.trim().length === 0 || serial.trim().length === 0) {
            showDialog("Please complete the required fields.");
            return false;
        }

        let productCategory = sessionStorage.getItem('productCategory');
        if (productCategory != null && productCategory != undefined && productCategory != 'undefined') {
            productCategory = JSON.parse(productCategory);
            if (productCategory != null) {
                productCategoryId = productCategory.ID;
                productCategoryName = productCategory.CategoryName;
            }
        }

        var customer = sessionStorage.getItem('Customer');
        if (customer != null && customer != undefined && customer != 'undefined') {
            customer = JSON.parse(customer);

            var todaysDate = new Date();

            var productStr = '{ "ID": 0, "CustomerID": {0}, "ProductCategoryID": {1}, "CategoryName": "{2}", "Manufacturer": "{3}", "Model": "{4}", "SerialNumber": "{5}", ' +
                '"IMEI": "", "Color": "{6}", "PurchaseDate": "{7}", "CoverageDate": "{8}", "Description": "{9}" }';
            productStr = productStr.replace('{0}', customer.ID).replace('{1}', productCategoryId).replace('{2}', productCategoryName);
            productStr = productStr.replace('{3}', make).replace('{4}', model).replace('{5}', serial);
            productStr = productStr.replace('{6}', color).replace('{7}', '').replace('{8}', convertDate(todaysDate)).replace('{9}', '');

            var hashed = sessionStorage.getItem('hashed');
            var code = sessionStorage.getItem('code');
            var method = sessionStorage.getItem('method');
            let headers = {};
            let url = "";
            if (method == "email") {
                url = "/api/product";
                headers = { "address": customer.Email, "hashed": hashed, "code": code };
            } else if (method == "sms") {
                url = "/api/product";
                headers = { "phone": customer.MobileNumber, "hashed": hashed, "code": code };
            }

            // Save the product to the database
            $.ajax({
                type: 'POST',
                url: url,
                headers: headers,
                data: JSON.stringify(JSON.parse(productStr)),
                contentType: 'application/json; charset=utf-8',
                success: function (response) {
                    if (response != null) {
                        $('#txtNewProductMake').val('');
                        $('#txtNewProductModel').val('');
                        $('#txtNewProductColor').val('');
                        $('#txtNewProductSerial').val('');
                        //$('#divSelectedProduct').show();
                        $('#divAddProduct').hide();
                        $('#lblSuccessMessage').html('Your new device was successfully added.');
                        $('#divBanner').show();
                    }
                    else {
                        $('#lblError').html(response);
                        $('#divError').show();
                    }
                    $('#divWait').hide();
                },
                error: function (response) {
                    $('#lblError').html('An error occurred while saving the device.<br />' + response);
                    $('#divError').show();
                    $('#divWait').hide();
                }
            });
        }
    });

    var code = sessionStorage.getItem('code');
    var hashed = sessionStorage.getItem('hashed');
    var method = sessionStorage.getItem('method');
    var customer = sessionStorage.getItem('Customer');
    if (customer != null && customer != undefined && customer != 'undefined') {
        customer = JSON.parse(customer);
    }

    if (customer != null) {

        let categoryID = getUrlParameter('category');
        let programID = getUrlParameter('program');

        if (customer.WarrantyProgram != null && customer.WarrantyProgram.ProgramClient != null &&
            (customer.WarrantyProgram.ProgramClient.Code === 'REACH') || (customer.WarrantyProgram.ProgramClient.Code === 'DOB')) {
            $('#btnCancelDevice').show();
            //$('#btnAddDevice').show();
            //$('#divMyProducts').hide();
            $('#divAddProduct').show();
            //$('#ddlProductCategories').hide();
            $('#lblProductCategory').show();
            let headers = {};
            let url = "";
            if (method == "email") {
                url = "/api/category/" + categoryID;
                headers = { "address": customer.Email, "hashed": hashed, "code": code };
            } else if (method == "sms") {
                url = "/api/category/" + categoryID;
                headers = { "phone": customer.MobileNumber, "hashed": hashed, "code": code };
            }

            $.ajax({
                type: 'GET',
                url: url,
                headers: headers,
                contentType: 'application/json; charset=utf-8',
                success: function (response) {
                    if (response == null || response.length == 0) {
                        $('#lblError').html('Sorry, an error has occurred.');
                        $('#divError').show();
                    }
                    else {
                        $('#lblProductCategory').html(response[0].CategoryName);
                        $('#lblServiceFee').html('$' + response[0].ServiceFee);
                        sessionStorage.setItem('productCategory', JSON.stringify(response[0]));
                    }
                },
                error: function (response) {
                    $('#lblError').html('Sorry, an error has occurred: ' + response);
                    $('#divError').show();
                }
            });
        }
        else {
            $('#btnAddDevice').hide();
            $('#ddlProductCategories').show();
            $('#lblProductCategory').hide();
            $('#btnCancelDevice').hide();

            let headers = {};
            let url = "";
            if (method == "email") {
                url = "/api/product?customer=" + customer.ID;
                headers = { "address": customer.Email, "hashed": hashed, "code": code };
            } else if (method == "sms") {
                url = "/api/product?customer=" + customer.ID;
                headers = { "phone": customer.MobileNumber, "hashed": hashed, "code": code };
            }

            $.ajax({
                type: 'GET',
                url: url,
                headers: headers,
                contentType: 'application/json; charset=utf-8',
                success: function (response) {
                    if (response == null) {
                        $('#lblError').html('Sorry, an error has occurred.');
                        $('#divError').show();
                    }
                    else {
                        products = response;
                        sessionStorage.setItem('products', JSON.stringify(products));
                        for (i = 0; i < response.length; i++) {
                            addDropdownOption(response[i].Manufacturer + ' ' + response[i].Model, response[i].ID.toString(), 'ddlProducts');
                        }
                        if (response.length > 0) {
                            $('#ddlProducts').val(response[0].ID.toString());
                            displaySelectedProduct(products[0]);
                        }
                    }
                },
                error: function (response) {
                    $('#lblError').html('Sorry, an error has occurred: ' + response);
                    $('#divError').show();
                }
            });
        }
    }
    else {
        $('#lblError').html('Sorry, your session appears to have expired. Please click the link to return to the home page.</br></br><a href="/">Home</a>');
        $('#divError').show();
    }
}

function setUpVerificationCheck() {
    let action = sessionStorage.getItem('action');
    if (action == null || action == undefined)
        action = '';
    window.location.href = "VerifyCode.aspx?action=" + action;
}

function setUpVerification(cust) {
    let action = sessionStorage.getItem('action');
    if (action == null || action == undefined)
        action = '';
    window.location.href = "Verify.aspx?action=" + action;
}

function displaySelectedProduct(product) {
    $('#lblManufacturer').html(product.Manufacturer);
    $('#lblModel').html(product.Model);
    $('#lblColor').html(product.Color);
    $('#lblSerialNumber').html(product.SerialNumber);
    $('#lblProductAdded').html(product.PurchaseDate.substr(0, 10));
    $('#hdnProductID').val(product.ID);
    sessionStorage.setItem('productSelected', JSON.stringify(product));
}

function formatMobileForVerification(mobile) {
    return '(XXX) XXX-' + mobile.substring(6);
}

function formatPhoneNumber(phone) {
    return '(' + phone.substring(0, 3) + ') ' + phone.substring(3, 6) + '-' + phone.substring(6);
}

function formatPhoneNumber2(phone) {
    return phone.substring(0, 3) + '-' + phone.substring(3, 6) + '-' + phone.substring(6);
}

function formatEmailForVerification(email) {
    var i = email.indexOf('@');
    var formatted = '';
    var c = 0;
    if (i > 0) {
        while (c < i - 1) {
            formatted = formatted + 'X';
            c++;
        }
        while (c < email.length) {
            formatted = formatted + email.substr(c, 1);
            c++;
        }
    }
    return formatted;
}

function formatAddress(address) {
    var formatted = '';
    if (address.Line1.length > 0)
        formatted += address.Line1 + '<br />';
    if (address.Line2.length > 0)
        formatted += address.Line2 + '<br />';
    if (address.City.length > 0)
        formatted += address.City + ', ';
    if (address.State.length > 0)
        formatted += address.State + ' ';
    if (address.PostalCode.length > 0)
        formatted += address.PostalCode;
    return formatted;
}

function formatDate(dateIn) {
    let dateOut = dateIn.toString().substr(5, 2) + '/'
        + dateIn.toString().substr(8, 2) + '/'
        + dateIn.toString().substr(0, 4);
    return dateOut;
}

function setUpDescribeEvent() {
    const querystring = window.location.search;
    const urlParams = new URLSearchParams(querystring);
    const clearParam = urlParams.get("clear");


    $('#btnStartClaimBack').click(function (e) {
        let product = sessionStorage.getItem('product');
        if (product != null && product != undefined && product.length > 0)
            window.location.href = 'ProductAction.aspx?product=' + product;
        else
            window.location.href = 'ProductAction.aspx';

        //window.location.href = 'ProductType.aspx';
    });

    $('#btnStartClaim').click(function (e) {
        let productSelected = sessionStorage.getItem('productSelected');
        let program = sessionStorage.getItem('clientProgram');
        if (productSelected != null) {
            productSelected = JSON.parse(productSelected);

            let customer = sessionStorage.getItem('Customer');
            if (customer != null && customer != undefined && customer != 'undefined') {
                customer = JSON.parse(customer);
                let lossDescription = $('#txtClaimDescription').val().trim();
                let dateSelected = $('#datepicker').val().trim();
                let perilSelected = $('#ddlPerils option:selected').val();
                let perilText = '';
                let subcategorySelected = $('#ddlSubcategories option:selected').val();
                let subcategoryText = '';

                for (var i = 0; i < perils.length; i++) {
                    if (perils[i].ID == perilSelected) {
                        perilText = perils[i].Peril;
                        sessionStorage.setItem('perilSelected', perils[i].ID.toString());

                        if (perils[i].SubcategoryID == subcategorySelected) {
                            subcategoryText = perils[i].Subcategory;
                            sessionStorage.setItem('subcategorySelected', perils[i].SubcategoryID.toString());
                            break;
                        }
                    }
                }

                if (perilText.length === 0 || subcategoryText.length === 0 || lossDescription.length === 0 || dateSelected.length < 8) {
                    showDialog("Please provide all the required information.");
                }
                else {
                    addressSelected = customer.MailingAddress.Line1 + '<br />';
                    if (customer.MailingAddress.Line2.length > 0)
                        addressSelected += customer.MailingAddress.Line2 + '<br />';
                    addressSelected += customer.MailingAddress.City + ', ' + customer.MailingAddress.State + ' ' + customer.MailingAddress.PostalCode;

                    sessionStorage.setItem('perilText', perilText);
                    sessionStorage.setItem('subcategoryText', subcategoryText);
                    sessionStorage.setItem('lossDescription', lossDescription);
                    sessionStorage.setItem('dateSelected', dateSelected);
                    sessionStorage.setItem('nameOnPlan', customer.PrimaryFirstName + ' ' + customer.PrimaryLastName)
                    sessionStorage.setItem('addressSelected', addressSelected);

                    var prodSelected = sessionStorage.getItem('productSelected');
                    prodSelected = JSON.parse(prodSelected);
                    var purchaseDate = prodSelected.PurchaseDate;
                    let purchdate = new Date(purchaseDate.substr(0, 10));
                    //let covgdate = new Date(prodSelected.CoverageDate.substr(0, 10));
                    let covgdate = new Date(customer.EnrollmentDate.substr(0, 10));
                    if (customer.WarrantyProgram != null && customer.WarrantyProgram.ProgramClient != null
                        && customer.WarrantyProgram.ProgramClient.Code === 'DOB')
                        covgdate = new Date(customer.EnrollmentDate.substr(0, 10));

                    let currentDate = new Date(dateSelected);
                    let newDate = new Date(purchdate.valueOf());
                    newDate.setDate(purchdate.getDate() + 365);
                    //if (newDate.valueOf() > currentDate.valueOf() && perilSelected == '1')
                    //    window.location.href = 'ManufacturersWarranty.aspx';
                    //else
                    if (covgdate.valueOf() > currentDate.valueOf()) {
                        showDialog('You cannot file a claim for an event that happened before the date your coverage started.', 'EVSTAR');
                        return;
                    }
                    window.location.href = 'SubmitDetails.aspx';
                }
            }
        }
        else {
            showDialog('No product selected.', 'EVSTAR');
        }
    });

    let selProduct = sessionStorage.getItem('productSelected');
    if (selProduct != null && selProduct != undefined) {
        selProduct = JSON.parse(selProduct);

        let datepicker = new DatePicker(document.getElementById('datepicker'));
        $('#datepicker').click(function (e) {
            datepicker.showCalender();
        });

        var desc = $('#txtClaimDescription').val();
        if (desc.trim().length === 0) {
            $('#txtClaimDescription').val('');
        }

        perils = {};
        $('#ddlPerils').empty();
        $('#ddlSubcategories').empty();

        let code = sessionStorage.getItem('code');
        let hashed = sessionStorage.getItem('hashed');
        let method = sessionStorage.getItem('method');
        let customer = sessionStorage.getItem('Customer');
        if (customer != null && customer != undefined && customer != 'undefined') {
            customer = JSON.parse(customer);
        }

        if (customer != null) {
            let perilSelected = sessionStorage.getItem('perilSelected');
            let subcategorySelected = sessionStorage.getItem('subcategorySelected');

            let program = customer.WarrantyProgram.ProgramName;
            let headers = {};
            let url = "";
            if (method == "email") {
                url = "/api/peril?category=" + selProduct.ProdCategory.ID + "&program=" + program;
                headers = { "address": customer.Email, "hashed": hashed, "code": code };
            } else if (method == "sms") {
                url = "/api/peril?category=" + selProduct.ProdCategory.ID + "&program=" + program;
                headers = { "phone": customer.MobileNumber, "hashed": hashed, "code": code };
            }
            $.ajax({
                type: 'GET',
                url: url,
                headers: headers,
                contentType: 'application/json; charset=utf-8',
                success: function (response) {
                    if (response == null) {
                        $('#lblError').html('Sorry, an error has occurred.');
                        $('#divError').show();
                    }
                    else {
                        var lastPeril = '';
                        perils = response;
                        for (i = 0; i < response.length; i++) {
                            if (lastPeril != response[i].ID.toString()) {
                                addDropdownOption(response[i].Peril, response[i].ID.toString(), 'ddlPerils');
                                lastPeril = response[i].ID.toString();
                            }
                        }
                        if (response.length > 0) {
                            let perilId = '';
                            if (perilSelected != null) {
                                $('#ddlPerils').val(perilSelected);
                                perilId = parseInt(perilSelected);
                            }
                            else {
                                $('#ddlPerils').val(response[0].ID.toString());
                                perilId = response[0].ID;
                            }
                            $('#ddlSubcategories').empty();
                            for (i = 0; i < perils.length; i++) {
                                if (perils[i].ID === perilId)
                                    addDropdownOption(perils[i].Subcategory, perils[i].SubcategoryID.toString(), 'ddlSubcategories');
                            }
                            if (subcategorySelected != null)
                                $('#ddlPerils').val(subcategorySelected);
                            else
                                $('#ddlPerils').val(perils[0].SubcategoryID.toString());
                        }

                        if (lastPeril == 1)
                            $('#lblWhatHappened').html('Describe what is wrong with the device');
                        else
                            $('#lblWhatHappened').html('Describe what happened to the device');

                        var perilText = sessionStorage.getItem('perilSelected');
                        var subcategoryText = sessionStorage.getItem('subcategorySelected');
                        var lossDescription = sessionStorage.getItem('lossDescription');
                        var dateSelected = sessionStorage.getItem('dateSelected');
                        $('#txtClaimDescription').val(lossDescription);
                        $('#datepicker').val(dateSelected);
                        $('#ddlPerils').val(perilText);
                        $('#ddlSubcategories').val(subcategoryText);
                    }
                },
                error: function (response) {
                    $('#lblError').html('Sorry, an error has occurred: ' + response);
                    $('#divError').show();
                }
            });
        }
        else {
            $('#lblError').html('Sorry, your session appears to have expired. Please click the link to return to the home page.</br></br><a href="/">Home</a>');
            $('#divError').show();
        }
    }

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

function setNext(next) {
    window.location.href = next;
}

function addDropdownOption(text, value, cmbId) {
    var newOption = new Option(text, value);
    var lst = document.getElementById(cmbId);
    if (lst) lst.options[lst.options.length] = newOption;
}

function sendEmail(to, subject, body) {
    var url = "/api/email?address=" + to;
    $('#divWait').show();
    $.ajax({
        type: 'POST',
        url: url,
        headers: { "func": "MAIL", "subj": btoa(subject), "body": btoa(body) },
        contentType: 'application/json; charset=utf-8',
        success: function (response) {
            if (response == null) {
                $('#lblError').html('Sorry, we were unable to send the email');
                $('#divError').show();
            }
            else {
            }
            $('#divWait').hide();
        },
        error: function (response) {
            $('#lblError').html('Sorry, we were unable to send the email.');
            $('#divError').show();
            $('#divWait').hide();
        }
    });
}


function showRegister() {
    isDobson = (curHost.indexOf("DOBSON.") >= 0); // || curHost.indexOf("LOCALHOST") >= 0);
    isReach = (curHost.indexOf("REACH") >= 0 || curHost.indexOf("LOCALHOST") >= 0);
    isEVSTAR = !(isDobson || isReach);
    let customer = sessionStorage.getItem('Customer');
    if (customer == null || customer.length == 0) {
        if (isDobson || isReach)
            window.location.href = "RegisterFromCode.aspx";
        else
            window.location.href = "Register.aspx";
    }
    else
        window.location.href = 'Login.aspx';
}

$('#btnCloseBanner').click(function () {
    $('#divBanner').hide();
    //    $('#divBanner').css("height", "1px");
    customer = sessionStorage.getItem('Customer');
    if (customer == null || customer.length == 0)
        window.location.href = "Login.aspx";
    else {
        var curPage = window.location.pathname.toUpperCase();
        if (curPage.includes("COLLECTPAYMENT."))
            $('#divBanner').hide();
        else
            window.location.href = "CustomerLanding.aspx";
    }
});

$('#btnMyAccount').click(function () {
    customer = sessionStorage.getItem('Customer');
    if (customer == null || customer.length == 0)
        window.location.href = "Login.aspx";
    else
        window.location.href = "CustomerLanding.aspx";
});

$('#btnHome').click(function () {
    window.location.href = 'Default.aspx';
});

$('#btnThankYou').click(function () {
    window.location.href = 'CustomerLanding.aspx';
});

function setLoggedIn(customer) {
    if (customer.WarrantyProgram != null && customer.WarrantyProgram.ProgramClient != null) {
        $('#logo').attr('src', customer.WarrantyProgram.ProgramClient.LogoFile);
        $('#logo').attr('alt', customer.WarrantyProgram.ProgramClient.Name);
    }
    $('#lnkHome').attr('href', 'CustomerLanding.aspx');
}

function setLoggedOut() {
    $('#logo').attr('src', '/Content/images/EVSTAR_dark_logo_transparent_small.png');
    $('#logo').attr('alt', 'EVSTAR');
    $('#lnkHome').attr('href', 'Default.aspx');
}

function whySecurity() {
    if ($('#whysecurity').is(":visible")) {
        $('#whysecurity').hide();
    }
    else {
        $('#whysecurity').show();
    }
}

$.prototype.enable = function () {
    $.each(this, function (index, el) {
        $(el).removeAttr('disabled');
    });
}

$.prototype.disable = function () {
    $.each(this, function (index, el) {
        $(el).attr('disabled', 'disabled');
    });
}

var getUrlParameter = function getUrlParameter(sParam) {
    var sPageURL = window.location.search.substring(1),
        sURLVariables = sPageURL.split('&'),
        sParameterName,
        i;

    for (i = 0; i < sURLVariables.length; i++) {
        sParameterName = sURLVariables[i].split('=');

        if (sParameterName[0] === sParam) {
            return sParameterName[1] === undefined ? true : decodeURIComponent(sParameterName[1]);
        }
    }
    return false;
};


function loadUsersGrid() {
    var user = sessionStorage.getItem('selUser');
    if (user != null && user != undefined) {
        user = JSON.parse(user);
    }

    //define common ajax object for addition, update and delete.
    var ajaxObj = {
        dataType: "JSON",
        beforeSend: function () {
            this.showLoading();
        },
        complete: function () {
            this.hideLoading();
        },
        error: function () {
            this.rollback();
        }
    };

    //to check whether any row is currently being edited.
    function isEditing(grid) {
        var rows = grid.getRowsByClass({ cls: 'pq-row-edit' });
        if (rows.length > 0) {
            var rowIndx = rows[0].rowIndx;
            grid.goToPage({ rowIndx: rowIndx });
            //focus on editor if any 
            grid.editFirstCellInRow({ rowIndx: rowIndx });
            return true;
        }
        return false;
    }

    //called by add button in toolbar.
    function addRow(grid) {
        var rows = grid.getRowsByClass({ cls: 'pq-row-edit' });
        if (rows.length > 0) {//already a row currently being edited.
            var rowIndx = rows[0].rowIndx;

            //focus on editor if any 
            grid.editFirstCellInRow({ rowIndx: rowIndx });
        }
        else {
            let user = sessionStorage.getItem('selUser');
            if (user != null && user != undefined) {
                user = JSON.parse(user);
                if (user != null && user.ClientID > 0) {
                    //append empty row in the first row.                            
                    var rowData = {
                        UserName: "", FirstName: "", LastName: "", Title: "", ClientID: user.ClientID, Authentication: "ChangeYourPassword", StoreID: 0,
                        Department: 0, ReportsTo: 0, Email: "", Phone: "", Reset: true, Active: true, UserTypeID: 1, Error: "", AddressID: user.AddressID
                    }; //empty row template
                    var rowIndx = grid.addRow({ rowIndxPage: 0, rowData: rowData, checkEditable: false });

                    //start editing the new row.
                    editRow(rowIndx, grid);
                }
            }
        }
    }

    //called by delete button.
    function deleteRow(rowIndx, grid) {
        grid.addClass({ rowIndx: rowIndx, cls: 'pq-row-delete' });

        var ans = window.confirm("Are you sure you want to delete this user?");
        if (ans) {
            var userID = grid.getRecId({ rowIndx: rowIndx });

            $.ajax($.extend({}, ajaxObj, {
                context: grid,
                type: 'DELETE',
                url: "/api/user/" + userID, //for ASP.NET, java
                //data: '{ "ID": ' + userID + '}',
                dataType: 'json',
                contentType: 'application/json; charset=utf-8',
                success: function (response) {
                    this.refreshDataAndView(); //reload fresh page data from server.
                    response = JSON.parse(response);
                    if (response.data == null && response.message != null && response.message != '')
                        alert(response.message);
                    this.removeClass({ rowIndx: rowIndx, cls: 'pq-row-delete' });
                },
                error: function (response) {
                    response = JSON.parse(response);
                    this.removeClass({ rowIndx: rowIndx, cls: 'pq-row-delete' });
                    if (response.data == null && response.message != null && response.message != '')
                        alert(response.message);
                }
            }));
        }
        else {
            grid.removeClass({ rowIndx: rowIndx, cls: 'pq-row-delete' });
        }
    }
    //called by edit button.
    function editRow(rowIndx, grid) {

        grid.addClass({ rowIndx: rowIndx, cls: 'pq-row-edit' });
        grid.refreshRow({ rowIndx: rowIndx });

        grid.editFirstCellInRow({ rowIndx: rowIndx });
    }

    //called by update button.
    function update(rowIndx, grid) {

        if (grid.saveEditCell() == false) {
            return false;
        }

        if (!grid.isValid({ rowIndx: rowIndx, focusInvalid: true }).valid) {
            return false;
        }

        if (grid.isDirty()) {
            var ID = grid.getRecId({ rowIndx: rowIndx });
            var url,
                rowData = grid.getRowData({ rowIndx: rowIndx }),
                recIndx = grid.option("dataModel.recIndx"),
                type;

            grid.removeClass({ rowIndx: rowIndx, cls: 'pq-row-edit' });

            if (rowData[recIndx] == null) {
                console.log(rowData);
                //add record.
                type = 'add';
                url = "api/user"; //ASP.NET, java
            }
            else {
                //update record.
                type = 'update';
                url = "api/user/" + ID; //ASP.NET, java
            }
            $.ajax($.extend({}, ajaxObj, {
                context: grid,
                url: url,
                data: rowData,
                type: (type == 'add' ? 'POST' : 'PUT'),
                success: function (response) {
                    //response = JSON.parse(response);
                    if (type == 'add') {
                        rowData[recIndx] = response.ID;
                    }
                    //debugger;
                    grid.commit({ type: type, rows: [rowData] });
                    grid.refreshRow({ rowIndx: rowIndx });
                    if (response != null && response.Error != null && response.Error != '')
                        alert(response.Error);
                },
                error: function (response) {
                    if (response != null && response.Error != null && response.Error != '')
                        alert(response.Error);
                }
            }));
        }
        else {
            grid.quitEditMode();
            grid.removeClass({ rowIndx: rowIndx, cls: 'pq-row-edit' });
            grid.refreshRow({ rowIndx: rowIndx });
        }
    }

    //define the grid.
    var obj = {
        height: '600',
        width: '1200',
        rowHt: 25,
        wrap: false,
        hwrap: false,
        columnBorders: true,
        selectionModel: { type: 'row' },
        trackModel: { on: true }, //to turn on the track changes.            
        toolbar: {
            items: [
                {
                    type: 'button',
                    icon: 'ui-icon-plus',
                    label: 'Add User',
                    listener: function () {
                        addRow(this);
                    }
                }
            ]
        },
        scrollModel: { autoFit: true },
        validation: { icon: 'ui-icon-info' },
        title: "<b>Users</b>",
        colModel: [
            {
                title: 'ID', width: '50', dataType: 'integer', dataIndx: 'ID', hidden: true
            },
            {
                title: 'FIRST', width: '150', dataType: 'string', dataIndx: 'FirstName', hidden: false
            },
            {
                title: 'LAST', width: '150', dataType: 'string', dataIndx: 'LastName', hidden: false
            },
            {
                title: 'TITLE', width: '150', dataType: 'string', dataIndx: 'Title', hidden: false
            },
            {
                title: 'EMAIL', width: '300', dataType: 'string', dataIndx: 'Email', hidden: false
            },
            {
                title: 'PHONE', width: '150', dataType: 'string', dataIndx: 'Phone', hidden: false
            },
            {
                title: 'RESET PASSWORD', width: '175', dataType: 'bool', dataIndx: 'Reset', hidden: false,
                menuIcon: false,
                type: 'checkBoxSelection', cls: 'ui-state-default', sortable: false, editor: false,
                cb: {
                    all: false, //header checkbox to affect checkboxes on all pages.
                    header: false //for header checkbox.
                }
            },
            {
                title: "ACTIVE", width: 100, dataIndx: "Active",
                menuIcon: false,
                type: 'checkBoxSelection', cls: 'ui-state-default', sortable: false, editor: false,
                dataType: 'bool',
                cb: {
                    all: false, //header checkbox to affect checkboxes on all pages.
                    header: false //for header checkbox.
                }
            },
            {
                title: "", editable: false, minWidth: 110, sortable: false,
                render: function (ui) {
                    return "<button type='button' class='edit_btn'></button><button type='button' class='delete_btn'></button>";
                },
                postRender: function (ui) {
                    var rowIndx = ui.rowIndx,
                        grid = this,
                        $cell = grid.getCell(ui);

                    if (grid.hasClass({ rowData: ui.rowData, cls: 'pq-row-edit' })) {

                        //update button
                        $cell.find(".edit_btn")
                            .button({ label: "", icons: { primary: 'ui-icon-check' } })
                            .off("click")
                            .on("click", function () {
                                return update(rowIndx, grid);
                            });

                        //cancel button
                        $cell.find(".delete_btn")
                            .button({ label: "", icons: { primary: 'ui-icon-cancel' } })
                            .off("click")
                            .on("click", function () {
                                grid.quitEditMode();
                                grid.removeClass({ rowIndx: rowIndx, cls: 'pq-row-edit' })
                                grid.rollback();
                            });
                    }
                    else {

                        //edit button
                        $cell.find(".edit_btn").button({ icons: { primary: 'ui-icon-pencil' } })
                            .off("click")
                            .on("click", function (evt) {
                                if (isEditing(grid)) {
                                    return false;
                                }
                                editRow(rowIndx, grid);
                            });

                        //delete button
                        $cell.find(".delete_btn").button({ icons: { primary: 'ui-icon-trash' } })
                            .off("click")
                            .on("click", function (evt) {
                                deleteRow(rowIndx, grid);
                            });
                    }
                }
            }
        ],
        postRenderInterval: -1,
        dataModel: {
            dataType: "JSON",
            location: "remote",
            recIndx: "ID",
            url: "/api/user?clientId=" + user.ClientID,
            getData: function (response) {
                response = JSON.parse(response);
                if (response.data == null && response.message != null && response.message != '') {
                    alert(response.message);
                }
                return { data: response.data, curPage: response.curPage, totalRecords: response.totalRecords };
            }
        },

        //make rows editable based upon the class.
        editable: function (ui) {
            return this.hasClass({ rowIndx: ui.rowIndx, cls: 'pq-row-edit' });
        },
        create: function (evt, ui) {
            this.widget().pqTooltip();
        }
    }

    if (userGrid == null)
        userGrid = pq.grid("#divUsersGrid", obj);
    else {
        userGrid.options.dataModel.url = "/api/user?clientID=" + user.ClientID;
        userGrid.refreshDataAndView();
    }

    //var $grid = $("#divUsersGrid").pqGrid(obj);
    //$grid.pqGrid('option', 'bottomVisible', true);
    ////$grid.refresh();
    $('#divWait').hide();
}


function handleDrop(e) {
    let dt = e.dataTransfer;
    let files = dt.files;

    handleFiles(files);
}

function handleFiles(files) {
    ([...files]).forEach(uploadFile);
    event.preventDefault();
}

function uploadFile(file) {
    let product = sessionStorage.getItem('product');
    if (product != null && product != undefined && product.length > 0)
        url = 'FileProcess.aspx?product=' + product;
    else
        url = 'FileProcess.aspx';

    var formData = new FormData();
    formData.append("userfile", file);

    var xhr = new XMLHttpRequest()
    xhr.addEventListener('readystatechange', function (e) {
        if (xhr.readyState == 4 && xhr.status == 200) {
            // Done. Inform the user
            var resp = xhr.responseText;
            var arr = xhr.responseText.split('textarea');
            if (arr.length > 1) {
                var arr2 = arr[1].split('>');
                if (arr2.length > 1) {
                    resp = htmlDecode(arr2[1].substr(0, arr2[1].length - 2));
                }
            }
            if (file.type.substr(0, 5) != 'audio') {
                $('#txtUploadStatus').val(resp);
                $('#fileUpload').val('');
            }
            else {
                $('#hidUploadFileName').val(file.name);
            }
        }
        else if (xhr.readyState == 4 && xhr.status != 200) {
            // Error. Inform the user
            alert('File upload failed. ' + xhr.responseText);
        }
    });

    xhr.open('POST', url, true);
    xhr.send(formData);
}

function preventDefaults(e) {
    e.preventDefault();
    e.stopPropagation();
}

function htmlDecode(value) {
    return $("<textarea/>").html(value).text();
}

function htmlEncode(value) {
    return $('<textarea/>').text(value).html();
}

function SendConfirmationEmail(customer, claim) {
    if (customer != null && customer != undefined && customer != 'undefined') {

        //sendEmail(customer.Email, "EVSTAR - Shipping Label",
        //    '<html><body>Click the provided link to get your shipping label.<br /><br /><a href="' +
        //    response + '">Click here to retrieve your shipping label.</a><br /></body></html>');

        let productSelected = sessionStorage.getItem('productSelected');
        productSelected = JSON.parse(productSelected);
        let subcategorySelected = sessionStorage.getItem('subcategorySelected');
        let dateSelected = sessionStorage.getItem('dateSelected');
        let repairOption = sessionStorage.getItem('repairOption');
        let shippingLabel = sessionStorage.getItem('shippingLabel');

        let url = '';
        url = "/api/email?address=" + encodeURIComponent(customer.Email) + "&dev=" + productSelected.ID + "&date="
            + encodeURIComponent(dateSelected) + "&option=" + repairOption + "&cat=" + encodeURIComponent(subcategorySelected) +
            "&label=" + encodeURIComponent(shippingLabel) + "&claim=" + encodeURIComponent(claim.ID);
        ;
        $('#divWait').show();
        $.ajax({
            type: 'POST',
            url: url,
            headers: { "func": "EMAIL", "address": customer.Email },
            contentType: 'application/json; charset=utf-8',
            success: function (response) {
                $('#divWait').hide();
                if (response == null) {
                    $('#lblError').html('Sorry, we were unable to send the confirmation email. Please make sure the email address you registered with is a valid email address.');
                    $('#divError').show();
                }
                else {
                }
            },
            error: function (response) {
                $('#lblError').html('Sorry, we were unable to send the confirmation email. Please make sure the email address you registered with is a valid email address.');
                $('#divError').show();
                $('#divWait').hide();
            }
        });
    }
    else {
        $('#lblError').html('Sorry, your session appears to have expired. Please click the link to return to the home page.</br></br><a href="/">Home</a>');
        $('#divError').show();
    }
}

function getFedexLink(customer) {
    $('#divWait').show();
    let hashed = sessionStorage.getItem('hashed');
    let code = sessionStorage.getItem('code');
    let method = sessionStorage.getItem('method');

    let claim = '';
    let address1 = '';
    let address2 = '';
    let city = '';
    let state = '';
    let postalCode = '';
    let country = 'US';
    let residential = 'true';
    let company = '';
    let passCode = '';

    let name = customer.PrimaryName;
    company = customer.CompanyName;
    var phone = customer.MobileNumber.length == 0 ? customer.HomeNumber : customer.MobileNumber;

    $('#divError').hide();

    if (!passcodeDisabled) {
        passCode = $('#txtPasscode').val();
        sessionStorage.setItem('passcode', passCode);
    }
    var addr = customer.MailingAddress;

    //if (useCustomAddress) {
    //    addr.Line1 = $('#txtCustomAddress1').val();
    //    addr.Line2 = $('#txtCustomAddress2').val();
    //    addr.Line3 = '';
    //    addr.City = $('#txtCustomCity').val();
    //    addr.State = $('#txtCustomState').val();
    //    addr.PostalCode = $('#txtCustomPostalCode').val();
    //    addr.Residential = $('#chkCustomResidential').is(":checked") ? "true" : "false";
    //}

    let headers = {};
    let url = "/api/address";
    if (method == "email") {
        headers = { "address": customer.Email, "hashed": hashed, "code": code };
    } else if (method == "sms") {
        headers = { "phone": customer.MobileNumber, "hashed": hashed, "code": code };
    }
    // Save the address to the database
    $.ajax({
        type: 'POST',
        url: url,
        headers: headers,
        data: JSON.stringify(addr),
        contentType: 'application/json; charset=utf-8',
        success: function (response) {
            if (response != null) {
                sessionStorage.setItem('addressSelected', JSON.stringify(response));

                let headers = {};
                let url = "/api/fedex";
                if (method == "email") {
                    headers = { "address": customer.Email, "hashed": hashed, "code": code };
                } else if (method == "sms") {
                    headers = { "phone": customer.MobileNumber, "hashed": hashed, "code": code };
                }
                $.ajax({
                    type: 'POST',
                    url: url + "?name=" + encodeURIComponent(name) + "&company=" + encodeURIComponent(company) +
                        "&address1=" + encodeURIComponent(addr.Line1) + "&address2=" + encodeURIComponent(addr.Line2) +
                        "&city=" + encodeURIComponent(addr.City) + "&state=" + encodeURIComponent(addr.State) +
                        "&postalCode=" + encodeURIComponent(addr.PostalCode) + "&country=" + encodeURIComponent(country) +
                        "&contactphone=" + encodeURIComponent(phone) + "&residential=" + encodeURIComponent(residential) +
                        "&claim=" + encodeURIComponent(claim),
                    headers: headers,
                    contentType: 'application/json; charset=utf-8',
                    success: function (response) {
                        if (response.toUpperCase().includes('HTTPS:')) {
                            sessionStorage.setItem('shippingLabel', response);
                            $('#divWait').hide();
                            window.location.href = "ClaimComplete.aspx";
                            //                                    let customer = sessionStorage.getItem('Customer');
                            //                                    if (customer != null && customer != undefined && customer != 'undefined') {
                            //                                        customer = JSON.parse(customer);

                            //                                        //sendEmail(customer.Email, "EVSTAR - Shipping Label",
                            //                                        //    '<html><body>Click the provided link to get your shipping label.<br /><br /><a href="' +
                            //                                        //    response + '">Click here to retrieve your shipping label.</a><br /></body></html>');

                            //                                        let productSelected = sessionStorage.getItem('productSelected');
                            //                                        productSelected = JSON.parse(productSelected);
                            //                                        let subcategorySelected = sessionStorage.getItem('subcategorySelected');
                            //                                        let dateSelected = sessionStorage.getItem('dateSelected');

                            //                                        let url = '';
                            //                                        url = "/api/email?address=" + encodeURIComponent(customer.Email) + "&dev=" + productSelected.ID + "&date="
                            //                                            + encodeURIComponent(dateSelected) + "&option=" + repairOption + "&cat=" + encodeURIComponent(subcategorySelected) +
                            //                                            "&label=" + encodeURIComponent(response) + "&claim=" + encodeURIComponent(claim);
                            //;
                            //                                        $('#divWait').show();
                            //                                        $.ajax({
                            //                                            type: 'POST',
                            //                                            url: url,
                            //                                            headers: { "func": "EMAIL" },
                            //                                            contentType: 'application/json; charset=utf-8',
                            //                                            success: function (response) {
                            //                                                $('#divWait').hide();
                            //                                                if (response == null) {
                            //                                                    $('#lblError').html('Sorry, we were unable to send the confirmation email. Please make sure the email address you registered with is a valid email address.');
                            //                                                    $('#divError').show();
                            //                                                }
                            //                                                else {
                            //                                                    window.location.href = 'ClaimComplete.aspx';
                            //                                                }
                            //                                            },
                            //                                            error: function (response) {
                            //                                                $('#lblError').html('Sorry, we were unable to send the confirmation email. Please make sure the email address you registered with is a valid email address.');
                            //                                                $('#divError').show();
                            //                                                $('#divWait').hide();
                            //                                            }
                            //                                        });
                            //                                    }
                            //                                    else {
                            //                                        $('#lblError').html('Sorry, your session appears to have expired. Please click the link to return to the home page.</br></br><a href="/">Home</a>');
                            //                                        $('#divError').show();
                            //                                    }
                        }
                        else {
                            $('#lblError').html(response);
                            $('#divError').show();
                        }
                        $('#divWait').hide();
                    },
                    error: function (response) {
                        $('#lblError').html('Sorry, a technical issue prevented us from creating your shipping label. Please try again later.<br />' + response);
                        $('#divError').show();
                        $('#divWait').hide();
                    }
                });
            }
            else {
                $('#lblError').html(response);
                $('#divError').show();
            }
            $('#divWait').hide();
        },
        error: function (response) {
            $('#lblError').html('Sorry, a technical issue prevented us from creating your shipping label. Please try again later.<br />' + response);
            $('#divError').show();
            $('#divWait').hide();
        }
    });
}