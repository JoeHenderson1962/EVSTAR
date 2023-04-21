$(function () {

    var myParams = parameters();
    var make = '';
    var model = '';
    var returnurl = '';

    if (myParams != null) {
        myParams = myParams.params;
        if (myParams != null) {
            make = myParams.make;
            model = myParams.model;
            returnurl = myParams.returnurl;
            $('#lblDevice').html(decodeURI(make) + " " + decodeURI(model));
        }
    }

    //if (model != '') {
        $.ajax({
            type: 'GET',
            url: "/api/troubleshooting?make=" + make + "&model=" + model,
            success: function (response) {
                if (response == null) {
                    $('#divTroubleshooting').hide();
                }
                else if (response != null) {
                    $('#divTroubleshooting').append(response);
                    $('#divTroubleshooting').show();
                    $('.accordionx').accordion({ heightStyle: "content", collapsible: true, active: false });
                    $('.accordiony').accordion({ heightStyle: "content", collapsible: true, active: false });

                    $('.ts-yes').click(function (e) {
                        window.location.href = returnurl + '?r=y';
                    });

                    $('.ts-no').click(function (e) {
                        window.location.href = returnurl + '?r=n';
                    });
                 }
            },
            error: function (response) {
                $('#divTroubleshooting').hide();
            }
        });
    //}

});

function parameters() {
    let url = window.location.href;
    let paramString = new RegExp('(.*)[?](.*)').exec(url);
    if (null == paramString) {
        return { 'base': url, 'params': null };
    }

    if (paramString[2].includes("&amp;")) {
        var paramList = paramString[2].split("&amp;");
    } else {
        var paramList = paramString[2].split("&");
    }

    let params = [];

    for (let i = 0; i < paramList.length; i++) {
        let values = paramList[i].split("=");
        params[values[0]] = values[1];
    }
    return { "base": paramString[1], "params": params };
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
