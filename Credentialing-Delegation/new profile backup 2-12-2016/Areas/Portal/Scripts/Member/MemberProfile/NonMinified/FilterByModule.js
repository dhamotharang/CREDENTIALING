$('document').ready(function() {

$("#ContactFromDatePrint").val(moment(new Date()).add(-180, 'days').format("L"));
$("#ContactToDatePrint").val(moment(new Date()).format("L"));
$("#NoteFromDatePrint").val(moment(new Date()).add(-180, 'days').format("L"));
$("#NoteToDatePrint").val(moment(new Date()).format("L"));

$("#ContactFromDatePrint").change(function () {
    var date = $(this).val();
    DisableEnablePrintBtn(date,"contact");
});

$("#ContactToDatePrint").change(function () {
    var date = $(this).val();
    DisableEnablePrintBtn(date, "contact");
});

function DisableEnablePrintBtn(date,content)
{
    if (content == "contact")
    {
        var newdate = moment(date).format("L");
        if (newdate == "Invalid date" || date.length != 10)
            $(".filter_print_button_contact").attr('disabled', 'disabled');
        else
            $(".filter_print_button_contact").removeAttr('disabled');
    }
    else if(content=="note")
    {

    }
}

$(".filter_print_button_contact").click(function () {
    var FromDate = $("#ContactFromDatePrint").val();
    FromDate = moment(FromDate).format("L");
    var ToDate = $("#ContactToDatePrint").val();
    ToDate = moment(ToDate).format("L");
    
    $("#PrintMemberName").text($("#MemberName").text());
    $("#PrintMemberID").text($("#MemberID").text());
    $("#PrintMemberRefID").text($("#Refnum").text());
    $("#PrintMemberDOB").text($("#MemberDob").text());
    $("#PrintMemberAge").text($("#MemberAge").text());

    $("#DetailsContact").html('');

    var $ContactRow = $('#contactTbody > tr');
    if ($ContactRow.length > 0) {
        for (var i = 0; i < $ContactRow.length; i++) {
            if($ContactRow[i].id=="emptyContactTemplate")
                continue;
            var tempcontact = getContactObject($ContactRow[i]);
            if (DateIsInRange(FromDate, ToDate,tempcontact.CallDateTime))
            {
                var div = '<div class="row col-lg-12 col-md-12 col-sm-12 col-xs-12" style="font-weight:bold">' + tempcontact.CallDateTime + ' ' + tempcontact.CreatedBy + ' Utilization Management - </div>' +
                '<div class="row col-lg-12 col-md-12 col-sm-12 col-xs-12">' + tempcontact.ContactEntity + ' - ' + tempcontact.ContactName + ' - ' + tempcontact.ContactType + ' - ' + tempcontact.Reason + ' - ' + tempcontact.OutcomeType + '</div>' +
                '<div class="row col-lg-12 col-md-12 col-sm-12 col-xs-12">' + tempcontact.Description + '</div> <br/> ';
                $("#DetailsContact").append(div);
            }
        }
    }
    printContactsData();

});

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

function DateIsInRange(FromDate,ToDate,CallDateTime)
{
    CallDateTime = moment(CallDateTime).format("L");
    var date1 = new Date(FromDate);
    var date2 = new Date(ToDate);
    var date3 = new Date(CallDateTime);
    if (date3 >= date1 && date3 <= date2)
        return true;
    else
        return false;
}

var Module = $(".moduleField").html();
var subscriberID = $(".subscriberField").html();
$(".moduleClase").click(function () {
    var $module = $("input[name=ModuleName]");
    
    var moduleArray = [];
    for (var i = 0 ; i < $module.length; i++) {
        if ($module[i].checked) {
            if ($module[i].value.toUpperCase() != 'ALL'.toUpperCase()) {
                moduleArray.push($module[i].value);
            }
        }
        else {
            $($module[i]).removeAttr('checked')
        }
    }
    if(moduleArray.length==0){
        moduleArray =["DUMMY"];
    }
    GetFilteredData(moduleArray);

});
$(".moduleClaseALL").click(function () {
    var moduleArray = []
    var isAllUnChecked = !$(this).prop("checked");
    $("input[name=ModuleName]").prop('checked', $(this).prop("checked"));
    moduleArray = ["UM", "MH", "DM", "CM", "AG"];
    if (isAllUnChecked) {
        $("input[value='UM']").prop('checked', !$(this).prop("checked"));
        moduleArray = ["UM"];
    }
    GetFilteredData(moduleArray);

});
function GetFilteredData(moduleArray) {
    var linkID = "NotesTableDiv";
    if (Module == "CONTACT") {
        linkID = "ContactTableDiv"
    }
    if (Module == "ATTACHMENT") {
        linkID = "AttachmentTBody"
    }
    $.ajax({
        url: "/Portal/MemberManage/GetFilteredDataViewBySubsriberID",
        data: { ID: Module, SubscriberId: subscriberID, ModuleArray: moduleArray },
        traditional: true,
        success: function (result) {
            $("#" + linkID).html(result);
        }
    })
}

});


