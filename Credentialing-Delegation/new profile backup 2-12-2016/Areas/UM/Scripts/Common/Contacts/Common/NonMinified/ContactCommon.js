var emptyContactTemplate = '<tr id="emptyContactTemplate" style="text-align:center"><td colspan="11" align="center" class="theme_label_data">No Data Available</td></tr>';

//Contact Add Btn CLicked
function AddNewContact() {
    TabManager.openFloatingModal('/Authorization/OpenContactModal', '~/Areas/UM/Views/Common/Modal_Header_Footer/Contact/Header/_AddContact.cshtml', '~/Areas/UM/Views/Common/Modal_Header_Footer/Contact/Footer/_SaveCancel.cshtml', '', 'IndexAdd', '');
}

//Contact Edit Btn CLicked
function EditContact(event) {
    //Getting parent tr of the btn, because inside tr(1st td) we have all hidden input data to be sent for edit/view
    var tr = $(event).parent().parent()[0];
    var contact = getContactObject(tr);
    //contact obj is used in Callback Function (IndexEdit) to prefill contact object for edit
    TabManager.openFloatingModal('/Authorization/OpenContactModal', '~/Areas/UM/Views/Common/Modal_Header_Footer/Contact/Header/_EditContact.cshtml', '~/Areas/UM/Views/Common/Modal_Header_Footer/Contact/Footer/_SaveCancel.cshtml', ' ', 'IndexEdit', contact);
}

//Contact View Btn CLicked
function ViewContact(event) {
    //Getting parent tr of the btn, because inside tr(1st td) we have all hidden input data to be sent for edit/view
    var tr = $(event).parent().parent()[0];
    var contact = getContactObject(tr);
    //contact obj is used in Callback Function (IndexView) to prefill contact object for view
    TabManager.openFloatingModal('/Authorization/OpenContactModal', '~/Areas/UM/Views/Common/Modal_Header_Footer/Contact/Header/_ViewContact.cshtml', '~/Areas/UM/Views/Common/Modal_Header_Footer/Contact/Footer/_Cancel.cshtml', ' ', 'IndexView', contact);
}

//Get Contact Object For Edit/View
function getContactObject(tr)
{
    var tempcontact = {};
    var id = $("#contactTbody > tr").index(tr);
    var element = $(tr);
    var ContactInputFields = element.find("input");
    //Inside tr, 1st td tag, list of all hidden input contact data is put into temporary contact object to send to view/edit (Retrieving data via name of all hidden input element)
    if (ContactInputFields && ContactInputFields.length > 0)
    {
        for(var k in ContactInputFields)
        {
            if(ContactInputFields[k].nodeName == "INPUT")
            {
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

//Delete Contact
function DeleteContact(event) {
    //Getting parent tr and Deleting complete row
    var tr = $(event).parent().parent()[0];
    $(tr).remove();
    var index = $("#contactTbody > tr").length;
    if (index == 0)
        $("#contactTbody").append(emptyContactTemplate);
    else
        updateContactMapping();
}

//All hidden input elements inside 1st td of each tr is updated after deletion of any row
function updateContactMapping()
{
    var $ContactRow = $('#contactTbody > tr');
    if ($ContactRow.length > 0)
    {
        for (var i = 0; i < $ContactRow.length; i++)
        {
            var element = $($ContactRow[i]);
            var ContactInputFields = element.find("input");
            if (ContactInputFields && ContactInputFields.length > 0)
            {
                for (var k in ContactInputFields) {
                    if (ContactInputFields[k].nodeName == "INPUT") {
                        var res = ContactInputFields[k].name.split("[");//splitting based on "[" so we get name divide into eg:-"Contacts[" and "].CallDateTime"
                        res[0] = res[0] + "[";//adding [
                        res[1] = res[1].substring(1);//removing the previous index
                        ContactInputFields[k].name = (res[0] + i + res[1]).toString();//adding the updated index
                    }
                }
            }
        }
    }
}

//Print all contact data from the contact table
function PrintContacts() 
{
   
        $("#PrintContactMemberName").text($("#MemberName").text());
        $("#PrintContactMemberID").text($("#MemberID").text());
        $("#PrintContactMemberRefID").text($("#Refnum").text());
        $("#PrintContactMemberDOB").text($("#MemberDob").text());
        $("#PrintContactMemberAge").text($("#MemberAge").text());
     
    $("#DetailsContact").html('');
    var $ContactRow = $('#contactTbody > tr');
    if ($ContactRow.length > 0) {
        for (var i = 0; i < $ContactRow.length; i++) {
            if($ContactRow[i].id=="emptyContactTemplate")
                continue;
            var tempcontact = getContactObject($ContactRow[i]);
            var div = '<div class="row col-lg-12 col-md-12 col-sm-12 col-xs-12" style="font-weight:bold">' + tempcontact.CallDateTime + ' ' + tempcontact.CreatedBy + ' Utilization Management - </div>' +
                '<div class="row col-lg-12 col-md-12 col-sm-12 col-xs-12">' + tempcontact.ContactEntity + ' - ' + tempcontact.ContactName + ' - ' + tempcontact.ContactType + ' - ' + tempcontact.Reason + ' - ' + tempcontact.OutcomeType + '</div>' +
                '<div class="row col-lg-12 col-md-12 col-sm-12 col-xs-12">' + tempcontact.Description + '</div> <br/> ';
            $("#DetailsContact").append(div);
        }
    }
    printContactsData();
}

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

$("#contactTbody").off("click", ".contactincfax").on("click", ".contactincfax", function () {
    if (this.value == "false" || this.value == false)
    {
        $(this).attr("checked", true);
        $(this).val(true);
    }
    else if (this.value == "true" || this.value == true)
    {
        $(this).removeAttr('checked');
        $(this).val(false);
    }
});