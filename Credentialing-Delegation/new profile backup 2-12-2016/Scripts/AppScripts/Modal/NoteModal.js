

function toggleNoteView()
{
    ShowModal('/Home/_GetNoteModalView', 'Add New Note');
    SubjectNum = 1;
}

var SubjectOption = [{ id: 1, Name: "Claims Review" },
                      { id: 2, Name: "Denc" },
                      { id: 3, Name: "Negotiate rate" },
                      { id: 4, Name: "Nomnc" },
                      { id: 5, Name: "OON" },
                      { id: 6, Name: "UM Note" }
];

var SubjectNum = 1;
function SubjectMenu()
{
    if (SubjectNum == 1)
    {
        $("#Subject").append("<option>" + " " + "</option>");
        $.each(SubjectOption, function (key, value) {
            $("#Subject").append("<option>" + value.Name + "</option>");
            SubjectNum += 1;
        })
    }
    
}


var NoteData = [{
    DateTime: "06/30/2016 13:22:57",
    USER: "BARBARA JOY",
    INC: false,
    TYPE: "Auth Note",
    SUBJECT: "UM Note",
    NOTE: "PATIENT WAS TREATED FOR SAME CONDITION 2 YEARS AGO.",
    RATIONALE: "",
    ALTERNATEPLAN: "",
    CRITERIAUSED: "",
    SERVICESUBJECTTONOTICE: "",
    LISTOFMEDICALSERVICES: "",
    Approve: false,
    Denial: false,
    Flag: "NORMAL"
},
{
    DateTime: "06/30/2016 10:02:23",
    USER: "BARBARA JOY",
    INC: false,
    TYPE: "Auth Note",
    SUBJECT: "UM Note",
    NOTE: "THIS CASE REQUIRES IMMEDIATE MEDICAL ATTENTION OF MD.",
    RATIONALE: "",
    ALTERNATEPLAN: "",
    CRITERIAUSED: "",
    SERVICESUBJECTTONOTICE: "",
    LISTOFMEDICALSERVICES: "",
    Approve: false,
    Denial: false,
    Flag: "NORMAL"
},
];
var GetNoteTemplate = function () {
    $("#AppendNoteData").html(null);
    var content = "";
    for (var i = 0; i < NoteData.length; i++) {
        var content = content + '<tr><td>' + NoteData[i].DateTime + '</td><td>' + NoteData[i].TYPE + '</td><td>BARBARA JOY</td><td>' + NoteData[i].SUBJECT + '</td><td>' + NoteData[i].NOTE + '</td><td><input type="checkbox" name="incLetter" class="form-control input-xs noteStyle"></td><td class="text-uppercase button-styles-xs"><button class="copy-button" onclick="ShowNoteModalEdit(' + i + ');"><i class="fa fa-pencil"></i></button><button onclick="ShowNoteModal();" class="addNoteBtn light-green-button"><i class="fa fa-plus"></i></button></td></tr>';
    }
    $("#AppendNoteData").append(content);
    $('.noteStyle').iCheck({
        checkboxClass: 'icheckbox_square-green',
        radioClass: 'iradio_square-green'
    });
}
var ShowNoteModal = function () { ShowModal('~/Views/Home/_GetAuthNoteModalView.cshtml', 'Add New Note') }

var ShowNoteModalEdit = function (index) {
    ShowModal("~/Views/Home/_GetAuthNoteModalView.cshtml", "Add New Note", "LoadNoteData", index);
    $(".SaveBtn").addClass("EditNote");
    $("#modalSaveBtn").data("index", index);
};
 
LoadNoteData = function (index) {

    setTimeout(function () {
        if (NoteData[index].Flag == "NORMAL") {
            $("#MDReview").show();
            $("#UM_Note").hide();
            $("#textareas").hide();
            $("#Type").val(NoteData[index].TYPE.split(" ")[0]);
            $("#Subject").val(NoteData[index].SUBJECT.split(" ")[0]);
            $("#Normal-Note").html(NoteData[index].NOTE);
        }
        //------um--note
        if (NoteData[index].Flag == "UMNOTE") {
            $("#MDReview").hide();
            $("#UM_Note").show();
            $("#textareas").hide();
            $("#Type").val(NoteData[index].TYPE.split(" ")[0]);
            $("#Subject").val(NoteData[index].SUBJECT.split(" ")[0]);
            $("#UMNote-Note").html(NoteData[index].NOTE);
            $("#UMNote-Criteria").html(NoteData[index].CRITERIAUSED);
            //Approve: false,
            //Denial: false
        }
        //--MD--Nurse
        if (NoteData[index].Flag == "NURSEMD") {
            $("#MDReview").hide();
            $("#UM_Note").hide();
            $("#textareas").show();
            $("#Type").val(NoteData[index].TYPE.split(" ")[0]);
            $("#Subject").val(NoteData[index].SUBJECT.split(" ")[0]);
            $("#NurseMD-Note").html(NoteData[index].NOTE);
            $("#NurseMD-RATIONALE").html(NoteData[index].RATIONALE);
            $("#NurseMD-ALTERNATE").html(NoteData[index].ALTERNATEPLAN);
            $("#NurseMD-CRITERIA").html(NoteData[index].CRITERIAUSED);
            $("#NurseMD-SERVICE").html(NoteData[index].SERVICESUBJECTTONOTICE);
            $("#NurseMD-MEDICAL").html(NoteData[index].LISTOFMEDICALSERVICES);
            //Approve: false,
            //Denial: false
        }
    }, 200);
};

InitICheckFinal();

var printNotesData = function (id) {
    windowTitle = "Notes";
    id = "NotePrintDiv";
    var content = "";
    $('#DetailsNote').empty();
    for (var i = 0; i < NoteData.length; i++) {
        var content = content + NoteData[i].DateTime + ' BARBARA JOY ' + NoteData[i].TYPE + '  <br/>' + NoteData[i].NOTE + ' <br/><br/><br/>';
    }
    $('#DetailsNote').append(content);
    var divToPrint = document.getElementById(id);
    $('#HiddenPrintDiv').empty();
    $('#HiddenPrintDiv').append(divToPrint.innerHTML);

    var mywindow = window.open('', $('#HiddenPrintDiv').html(), 'height=' + screen.height + ',width=' + screen.width);
    mywindow.document.write('<html><head><title>' + windowTitle + '</title>');
    mywindow.document.write('<link rel="stylesheet" href="~/Content/bootstrap.min.css" type="text/css" />');
    mywindow.document.write('<style>@page{size: auto;margin-bottom:0.5cm;margin-top:0.5cm;margin-left:0cm;margin-right:0cm;} #DetailsNote{ text-align: justify;text-justify: inter-word;} .downspace{margin-bottom:-20px;} </style>');
    mywindow.document.write('</head><body style="background-color:white;word-wrap: break-word;">');
    mywindow.document.write($('#HiddenPrintDiv').html());
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


var printContactsData = function (id) {
    windowTitle = "Contacts";
    id = "NotePrintDiv";
    var content = "";
    $('#DetailsNote').empty();
    for (var i = 0; i < NoteData.length; i++) {
        var content = content + NoteData[i].DateTime + ' BARBARA JOY ' + NoteData[i].TYPE + '  <br/>' + NoteData[i].NOTE + ' <br/><br/><br/>';
    }
    $('#DetailsNote').append(content);
    var divToPrint = document.getElementById(id);
    $('#HiddenPrintDiv').empty();
    $('#HiddenPrintDiv').append(divToPrint.innerHTML);

    var mywindow = window.open('', $('#HiddenPrintDiv').html(), 'height=' + screen.height + ',width=' + screen.width);
    mywindow.document.write('<html><head><title>' + windowTitle + '</title>');
    mywindow.document.write('<link rel="stylesheet" href="~/Content/bootstrap.min.css" type="text/css" />');
    mywindow.document.write('<style>@page{size: auto;margin-bottom:0.5cm;margin-top:0.5cm;margin-left:0cm;margin-right:0cm;} #DetailsNote{ text-align: justify;text-justify: inter-word;} .downspace{margin-bottom:-20px;} </style>');
    mywindow.document.write('</head><body style="background-color:white;word-wrap: break-word;">');
    mywindow.document.write($('#HiddenPrintDiv').html());
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