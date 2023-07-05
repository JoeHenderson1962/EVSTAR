
$(function () {
    //$("#calCarDeliveryDate").datepicker();
    let datepicker = new DatePicker(document.getElementById('calCarDeliveryDate'));
});

$('#ddlHasSolarPanels').change(
    function () {
        let answer = $('#ddlHasSolarPanels option:selected').val();
        if (answer === 'Yes') {
            $('#divSolarSize').css("display", "inline-block");
        }
        else {
            $('#divSolarSize').css("display", "none");
        }
    }
);

$('#ddlHaveCarCharger').change(
    function () {
        let answer = $('#ddlHaveCarCharger option:selected').val();
        if (answer === 'Yes') {
            $('#divNonTeslaYes').css("display", "inline-block");
        }
        else {
            $('#divNonTeslaYes').css("display", "none");
        }
    }
);

$('#ddlHasGateCode').change(
    function () {
        let answer = $('#ddlHasGateCode option:selected').val();
        if (answer === 'Yes') {
            $('#divGateCode').css("display", "inline-block");
        }
        else {
            $('#divGateCode').css("display", "none");
        }
    }
);

$('#ddlParking').change(
    function () {
        let answer = $('#ddlParking option:selected').val();
        $('#ddlPreferredSpot').empty();
        addDropdownOption('Left', 'Left', 'ddlPreferredSpot');
        if (answer != null && answer === 'Two Car Garage') {
            $('#divPreferredSpot').css("display", "inline-block");
            addDropdownOption('Right', 'Right', 'ddlPreferredSpot');
        }
        else if (answer != null && answer === 'Three Car Garage') {
            $('#divPreferredSpot').css("display", "inline-block");
            addDropdownOption('Middle', 'Middle', 'ddlPreferredSpot');
            addDropdownOption('Right', 'Right', 'ddlPreferredSpot');
        }
        else {
            $('#divPreferredSpot').css("display", "none");
        }
    }
);

$("#ddlCarMake").change(
    function () {
        let makeName = $('#ddlCarMake option:selected').val();

        $.ajax({
            type: 'GET',
            url: '/api/vehicle',
            //headers: headers,
            contentType: 'application/json; charset=utf-8',
            success: function (response) {
                if (response == null) {
                    showDialog('Sorry, an error has occurred.');
                }
                else {
                    let makes = response;
                    if (response.length > 0) {
                        let modelName = '';
                        $('#ddlCarModel').empty();
                        for (var i = 0; i < makes.length; i++) {
                            if (makes[i].Make === makeName) {
                                if (modelName === '')
                                    modelName = makes[i].ModelName;
                                addDropdownOption(makes[i].ModelName, makes[i].ModelName, 'ddlCarModel');
                            }
                        }
                        $('#ddlCarModel').val(modelName);
                        if (makeName.toUpperCase() == 'TESLA') {
                            $('#divTesla').css("display", "inline-block");
                            $('#divNonTesla').css("display", "none");
                        }
                        else {
                            $('#divNonTesla').css("display", "inline-block");
                            $('#divTesla').css("display", "none");
                        }
                    }
                }
            },
            error: function (response) {
                showDialog('Sorry, an error has occurred: ' + response);
            }
        });
    }
);

function addDropdownOption(text, value, cmbId) {
    var newOption = new Option(text, value);
    var lst = document.getElementById(cmbId);
    if (lst) lst.options[lst.options.length] = newOption;
}

$('#btnSubmitQuoteRequest').click(function () {
    saveQuoteRequest();
});

//$('#uplGarage').change(function (e) {
function uploadPhotos(sessionId) {
    const fd = new FormData();

    let e = document.getElementById('uplHomeFromStreet');
    if (e.value != null && e.value.length > 0) {
        e.files.forEach((file) => {
            fd.append(e.value, file, file.name);
        });

        sendUploadRequest(fd, sessionId);
    }

    e = document.getElementById('uplGarage');
    if (e.value != null && e.value.length > 0) {
        e.files.forEach((file) => {
            fd.append(e.value, file, file.name);
        });

        sendUploadRequest(fd, sessionId);
    }

    e = document.getElementById('uplGarageInterior');
    if (e.value != null && e.value.length > 0) {
        e.files.forEach((file) => {
            fd.append(e.value, file, file.name);
        });

        sendUploadRequest(fd, sessionId);
    }

    e = document.getElementById('uplMainElectricalPanel');
    if (e.value != null && e.value.length > 0) {
        e.files.forEach((file) => {
            fd.append(e.value, file, file.name);
        });

        sendUploadRequest(fd, sessionId);
    }

    e = document.getElementById('uplMainElectricalPanelCloseUp');
    if (e.value != null && e.value.length > 0) {
        e.files.forEach((file) => {
            fd.append(e.value, file, file.name);
        });

        sendUploadRequest(fd, sessionId);
    }

    e = document.getElementById('uplElectricalSubPanel');
    if (e.value != null && e.value.length > 0) {
        e.files.forEach((file) => {
            fd.append(e.value, file, file.name);
        });

        sendUploadRequest(fd, sessionId);
    }

    e = document.getElementById('uplElectricalSubPanelCloseUp');
    if (e.value != null && e.value.length > 0) {
        e.files.forEach((file) => {
            fd.append(e.value, file, file.name);
        });

        sendUploadRequest(fd, sessionId);
    }

    e = document.getElementById('uplIdealChargerLocation');
    if (e.value != null && e.value.length > 0) {
        e.files.forEach((file) => {
            fd.append(e.value, file, file.name);
        });

        sendUploadRequest(fd, sessionId);
    }
}

function sendUploadRequest(fd, sessionId) {

    // create the request
    const xhr = new XMLHttpRequest();
    xhr.onload = () => {
        if (xhr.status >= 200 && xhr.status < 300) {
            // we done!
        }
    };

    // path to server would be where you'd normally post the form to
    xhr.open('POST', '/api/file', true);
    xhr.setRequestHeader("sessionId", sessionId);
    xhr.send(fd);
}

//});

function validateData(cq) {
    var errors = [];
    if (cq.FirstName == null || cq.FirstName.length == 0)
        errors.push("First Name");
    if (cq.LastName == null || cq.LastName.length == 0)
        errors.push("Last Name");
    if (cq.Email == null || cq.Email.length == 0)
        errors.push("Email");
    if (cq.Phone == null || cq.Phone.length == 0)
        errors.push("Phone");
    if (cq.City == null || cq.City.length == 0)
        errors.push("City");
    if (cq.Address1 == null || cq.Address1.length == 0)
        errors.push("Address");
    if (cq.State == null || cq.State.length == 0)
        errors.push("State");
    if (cq.PostalCode == null || cq.PostalCode.length == 0)
        errors.push("ZIP Code");
    if (cq.PreferredCharger == null || cq.PreferredCharger.length == 0)
        errors.push("Preferred Charging Station");
    if (cq.PhotoMainPanelUrl == null || cq.PhotoMainPanelUrl.length == 0)
        errors.push("Overview Photo of Main Electrical Panel Placement");
    if (cq.PhotoCloseUpMainPanelUrl == null || cq.PhotoCloseUpMainPanelUrl.length == 0)
        errors.push("Close Up Photo of Main Electrical Panel");
    if (cq.PhotoIdealChargerLocationUrl == null || cq.PhotoIdealChargerLocationUrl.length == 0)
        errors.push("Photo of Ideal Charger Location");
    return errors;

}

function saveQuoteRequest() {
    $('#lblError2').html('');
    $('#divError2').hide();

    let SessionID = $('#hidSessionID').val();
    let AdditionalInfo = $('#txtAdditionalInformation').val();
    let Address1 = $('#txtStreetAddress1').val();
    let Address2 = $('#txtStreetAddress2').val();
    let City = $('#txtCity').val();
    let CreateDateTime = new Date();
    let DryerType = $('#ddlDryerType').val();
    let ElectricCarDeliveryDate = new Date($('#calCarDeliveryDate').val());
    let ElectricUtilityCo = $('#txtElectricCompany').val();
    let Email = $('#txtEmail').val();
    let FirstName = $('#txtFirstName').val();
    let LastName = $('#txtLastName').val();
    let HasAtticAccess = $('#ddlAtticAccess').val() === 'Yes';
    let HasBasement = $('#ddlHasBasement').val();
    let HasGateCode = $('#ddlHasGateCode').val() === 'Yes';
    let GateCode = $('#txtGateCode').val();
    let HasOutdoorHotTub = $('#ddlHasHotTub').val() === 'Yes';
    let HasPool = $('#ddlHasPool').val() === 'Yes';
    let HasSolarPanels = $('#ddlHasSolarPanels').val() === 'Yes';
    let HasCarCharger = $('#ddlHaveCarCharger').val() === 'Yes';
    let HomeSquareFeet = $('#txtHomeSquareFeet').val();
    let NumberOfHVAC = $('#ddlHVACUnits').val();
    let Parking = $('#ddlParking').val();
    let ParkingOrientation = $('#ddlParkingOrientation').val();
    let Phone = $('#txtPhoneNumber').val();
    let PostalCode = $('#txtPostalCode').val();
    let State = $('#ddlState').val();
    let StoveRangeType = $('#ddlStoveType').val();
    let VehicleMake = $('#ddlCarMake').val();
    let VehicleModel = $('#ddlCarModel').val();
    let VehicleYear = $('#ddlCarYear').val();
    let PreferredCharger = $('#ddlPreferredCharger').val();
    let SolarPanelSize = $('#txtSolarSize').val();
    let ParkingPosition = $('#ddlPreferredSpot').val();
    let uplHomeFromStreetUrl = $('#uplHomeFromStreet').val();
    let uplPhotoGarageUrl = $('#uplGarage').val();
    let uplPhotoGarageInteriorUrl = $('#uplGarageInterior').val();
    let uplPhotoMainPanelUrl = $('#uplMainElectricalPanel').val();
    let uplPhotoCloseUpMainPanelUrl = $('#uplMainElectricalPanelCloseUp').val();
    let uplPhotoSubpanelUrl = $('#uplElectricalSubPanel').val();
    let uplPhotoCloseUpSubPanelUrl = $('#uplElectricalSubPanelCloseUp').val();
    let uplPhotoIdealChargerLocationUrl = $('#uplIdealChargerLocation').val();

    let cqr = {
        "ID": 0,
        "CreateDateTime": new Date(),
        "ProcessedDateTime": null,
        "FirstName": FirstName,
        "LastName": LastName,
        "Phone": Phone,
        "Email": Email,
        "Address1": Address1,
        "Address2": Address2,
        "City": City,
        "State": State,
        "PostalCode": PostalCode,
        "VehicleYear": VehicleYear,
        "VehicleMake": VehicleMake,
        "VehicleModel": VehicleModel,
        "PreferredCharger": PreferredCharger,
        "ElectricCarDeliveryDate": ElectricCarDeliveryDate,
        "HomeSquareFeet": HomeSquareFeet,
        "HasAtticAccess": HasAtticAccess,
        "HasBasement": HasBasement,
        "ElectricUtilityCo": ElectricUtilityCo,
        "HasGateCode": HasGateCode,
        "Parking": Parking,
        "ParkingOrientation": ParkingOrientation,
        "NumberOfHVAC": NumberOfHVAC,
        "DryerType": DryerType,
        "StoveRangeType": StoveRangeType,
        "HasPool": HasPool,
        "HasOutdoorHotTub": HasOutdoorHotTub,
        "HasSolarPanels": HasSolarPanels,
        "WantAffordablePayments": false,
        "AdditionalInfo": AdditionalInfo,
        "PhotoStreetUrl": uplHomeFromStreetUrl,
        "PhotoGarageUrl": uplPhotoGarageUrl,
        "PhotoGarageInteriorUrl": uplPhotoGarageInteriorUrl,
        "PhotoMainPanelUrl": uplPhotoMainPanelUrl,
        "PhotoCloseUpMainPanelUrl": uplPhotoCloseUpMainPanelUrl,
        "PhotoSubpanelUrl": uplPhotoSubpanelUrl,
        "PhotoCloseUpSubpanelUrl": uplPhotoCloseUpSubPanelUrl,
        "PhotoIdealChargerLocationUrl": uplPhotoIdealChargerLocationUrl,
        "SessionID": SessionID,
        "SolarPanelSize": SolarPanelSize,
        "GateCode": GateCode,
        "ParkingPosition": ParkingPosition,
        "HasCarCharger": HasCarCharger
    }

    let errors = validateData(cqr, SessionID);
    if (errors.length == 0) {
        uploadPhotos(SessionID);

        $.ajax({
            type: 'POST',
            url: '/api/chargerquote',
            contentType: 'application/json; charset=utf-8',
            data: JSON.stringify(cqr),
            //headers: headers,
            contentType: 'application/json; charset=utf-8',
            success: function (response) {
                if (response == null) {
                    alert('Sorry, an error has occurred.');
                }
                else {
                    alert('Your request has been submitted.');
                }
            },
            error: function (response) {
                alert('Sorry, an error has occurred: ' + response);
            }
        });
    }
    else {
        let errorStr = '';
        for (var i = 0; i < errors.length; i++) {
            errorStr = errorStr + errors[i];
            if (i < (errors.length - 1))
                errorStr = errorStr + ', ';
        }
        alert('The following required fields must be completed: ' + errorStr);
    }
}
