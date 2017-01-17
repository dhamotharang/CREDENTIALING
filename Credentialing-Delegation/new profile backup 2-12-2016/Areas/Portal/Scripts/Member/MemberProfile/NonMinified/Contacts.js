function ViewContact(id,name) {
    $.ajax({
        type: 'GET',
        url: '/MemberManage/GetContactDetailsByID?ID=' + id + '&&ModuleName='+name,
        processData: false,
        contentType: false,
        cache: false,
        error: function () {
        },
        success: function (data) {
            if(data){
                data1 = JSON.parse(data);
                if (data1.CallDateTime)
                    data1.CallDateTime = moment(data1.CallDateTime).format('MM/DD/YYYY HH:mm:ss');
                TabManager.openFloatingModal('/Authorization/ViewContact', '~/Areas/UM/Views/Common/Modal_Header_Footer/Contact/Header/_ViewContact.cshtml', '~/Areas/UM/Views/Common/Modal_Header_Footer/Contact/Footer/_Cancel.cshtml', ' ', 'IndexView', data1);
}
            
        }
    });
}

//Print all contact data from the contact table
//$('.contact_print_button_increate').on('click', function (e) {
//$('#contactDiv').off('click', '.contact_print_button_inmember').on('click', '.contact_print_button_inmember', function () {
function PrintContacts(){
    $("#DetailsContact").html('');
    var $ContactRow = $('#contactTbody > tr');
    if ($ContactRow.length > 0) {
        for (var i = 0; i < $ContactRow.length; i++) {
            if ($ContactRow[i].id == "emptyContactTemplate")
                continue;
            var tempcontact = getContactObject($ContactRow[i]);
            var div = '<div class="row col-lg-12 col-md-12 col-sm-12 col-xs-12" style="font-weight:bold">' + tempcontact.CallDateTime + ' ' + tempcontact.CreatedBy + ' Utilization Management - </div>' +
                '<div class="row col-lg-12 col-md-12 col-sm-12 col-xs-12">' + tempcontact.ContactEntity + ' - ' + tempcontact.ContactName + ' - ' + tempcontact.ContactType + ' - ' + tempcontact.Reason + ' - ' + tempcontact.OutcomeType + '</div>' +
                '<div class="row col-lg-12 col-md-12 col-sm-12 col-xs-12">' + tempcontact.Description + '</div> <br/> ';
            $("#DetailsContact").append(div);
        }
    }
    printContactsData();
//});
};

//Print contact from above div created
var printContactsData = function () {
    windowTitle = "Contacts";
    id = "ContactPrintDiv";
    var divToPrint = document.getElementById(id);
    $('#HiddenContactPrintDiv').empty();
    $('#HiddenContactPrintDiv').append(divToPrint.innerHTML);

    var mywindow = window.open('', $('#HiddenContactPrintDiv').html(), 'height=' + screen.height + ',width=' + screen.width);
    mywindow.document.write('<html><head><title>' + windowTitle + '</title>');
    mywindow.document.write('<link rel="stylesheet" href="~/Content/bootstrap.min.css" type="text/css" />');
    mywindow.document.write('<style>@page{size: auto;margin-bottom:0.5cm;margin-top:0.5cm;margin-left:0cm;margin-right:0cm;} #DetailsNote{ text-align: justify;text-justify: inter-word;} .downspace{margin-bottom:-20px;} </style>');
    mywindow.document.write('</head><body style="background-color:white;word-wrap: break-word;">');
    mywindow.document.write($('#HiddenContactPrintDiv').html());
    mywindow.document.write('</body><script> var logo = document.getElementById("noteLogo"); logo.style.width="159px"; logo.style.height="80px";</script></html>');

    mywindow.document.close();
    mywindow.focus();

    $(mywindow).ready(function () {
        setTimeout(function () {
            mywindow.print();
            mywindow.close();
        }, 2000);
    });
}

function initPop() {
    $('[data-toggle="popover"]').popover();
}

//Get Contact Object For Edit/View
function getContactObject(tr) {
    var tempcontact = {};
    var id = $("#contactTbody > tr").index(tr);
    var element = $(tr);
    var ContactInputFields = element.find("input");
    //Inside tr, 1st td tag, list of all hidden input contact data is put into temporary contact object to send to view/edit (Retrieving data via name of all hidden input element)
    if (ContactInputFields && ContactInputFields.length > 0) {
        for (var k in ContactInputFields) {
            if (ContactInputFields[k].nodeName == "INPUT") {
                var name = ContactInputFields[k].name;
                var dotIndex = name.indexOf(".");
                var key = name.slice(dotIndex + 1);
                var value = ContactInputFields[k].value;
                tempcontact[key] = value;
            }
        }
    }
    tempcontact['tempContactID'] = id;
    return tempcontact;
}