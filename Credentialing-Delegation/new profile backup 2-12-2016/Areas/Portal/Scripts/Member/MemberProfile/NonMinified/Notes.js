
$("#Notes").ready(function(){
    setTimeout(function(){
        $('input.flat').iCheck({
            checkboxClass: 'icheckbox_square-green',
            radioClass: 'iradio_square-green'
        });
    },500)
})

//View Note btn clicked
function ViewNote(id,name){
$.ajax({
        type: 'GET',
        url: '/MemberManage/GetNoteDetailsByID?ID=' + id + '&&ModuleName=' +name,
        processData: false,
        contentType: false,
        cache: false,
        error: function () {
        },
        success: function (data) {
            TabManager.openFloatingModal('/Authorization/ViewNote', '~/Areas/UM/Views/Common/Modal_Header_Footer/Note/Header/_ViewNote.cshtml', '~/Areas/UM/Views/Common/Modal_Header_Footer/Note/Footer/_Cancel.cshtml', ' ', 'NoteView', data);
        }
    });
}
//Print all note data from the note table
//$('#NoteArea').off('click', '.notes_print_button_inmember').on('click', '.notes_print_button_inmember', function () {
function PrintNotes(){ 
    $("#DetailsNote").html('');
    var $NoteRow = $('#NotesArea > tr');
    if ($NoteRow.length > 0) {
        for (var i = 0; i < $NoteRow.length; i++) {
            if ($NoteRow[i].id == "emptyNoteTemplate")
                continue;
            //Getting note obj in proper format from hidden input element
            var tempnote = getNoteObject($NoteRow[i]);
            var notedesc = (tempnote.Description != '') ? '<div class="row col-lg-12 col-md-12 col-sm-12 col-xs-12">' + tempnote.Description + '</div>' : '-';
            var noterationale = (tempnote.RationaleDescription != '') ? '<div class="row col-lg-12 col-md-12 col-sm-12 col-xs-12"><b>Rationale:</b> ' + tempnote.RationaleDescription + '</div>' : '';
            var notealternateplan = (tempnote.AlternatePlanDescription != '') ? '<div class="row col-lg-12 col-md-12 col-sm-12 col-xs-12"><b>Alternate Plan:</b> ' + tempnote.AlternatePlanDescription + '</div>' : '';
            var notecriteria = (tempnote.CriteriaUsedDescription != '') ? '<div class="row col-lg-12 col-md-12 col-sm-12 col-xs-12"><b>Criteria Used:</b> ' + tempnote.CriteriaUsedDescription + '</div>' : '';
            var noteservicesubject = (tempnote.ServiceSubjectToNotice != '') ? '<div class="row col-lg-12 col-md-12 col-sm-12 col-xs-12"><b>Service Subject to Notice:</b> ' + tempnote.ServiceSubjectToNotice + '</div>' : '';
            var notemedicalneccessaries = (tempnote.MedicalNecessaries != '') ? '<div class="row col-lg-12 col-md-12 col-sm-12 col-xs-12"><b>List Of Medical Services:</b> ' + tempnote.MedicalNecessaries + '</div>' : '';

            var div = '<div class="row col-lg-12 col-md-12 col-sm-12 col-xs-12" style="font-weight:bold">' + tempnote.Date + ' ' + tempnote.UserName + ' Utilization Management - </div>' + notedesc + noterationale + notealternateplan + notecriteria + noteservicesubject + notemedicalneccessaries + '<br/>';
            $("#DetailsNote").append(div);
        }
    }
    printNotesData();
//});
};

//Print note from above div created
function printNotesData() {
    windowTitle = "Notes";
    id = "NotePrintDiv";
    var content = "";
    var divToPrint = document.getElementById(id);
    $('#HiddenNotePrintDiv').empty();
    $('#HiddenNotePrintDiv').append(divToPrint.innerHTML);
    var mywindow = window.open('', $('#HiddenNotePrintDiv').html(), 'height=' + screen.height + ',width=' + screen.width);
    mywindow.document.write('<html><head><title>' + windowTitle + '</title>');
    mywindow.document.write('<link rel="stylesheet" href="~/Content/bootstrap.min.css" type="text/css" />');
    mywindow.document.write('<style>@page{size: auto;margin-bottom:0.5cm;margin-top:0.5cm;margin-left:0cm;margin-right:0cm;} #DetailsNote{ text-align: justify;text-justify: inter-word;} .downspace{margin-bottom:-20px;} th{background-color: #0095ff;}</style>');
    mywindow.document.write('</head><body style="background-color:white;word-wrap: break-word;">');
    mywindow.document.write($('#HiddenNotePrintDiv').html());
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
function getNoteObject(tr) {
    var tempnote = {};
    var MedicalNecessaries = [];

    var id = $("#NotesArea > tr").index(tr);
    var element = $(tr);
    var NoteInputFields = element.find("input");

    //Inside tr, 1st td tag, list of all hidden input note data is put into temporary note object to send to view/edit (Retrieving data via name of all hidden input element)
    if (NoteInputFields && NoteInputFields.length > 0) {
        for (var k in NoteInputFields) {
            if (NoteInputFields[k].nodeName == "INPUT") {
                var name = NoteInputFields[k].name;
                var dotIndex = name.indexOf(".");
                var key = name.slice(dotIndex + 1);
                if (key.includes("MedicalNecessaries")) {
                    if (NoteInputFields[k].value)
                        MedicalNecessaries.push(NoteInputFields[k].value);
                }
                else {
                    var value = NoteInputFields[k].value;
                    tempnote[key] = value;
                }
            }
        }
    }
    tempnote['tempNoteID'] = id;
    tempnote['MedicalNecessaries'] = MedicalNecessaries;
    return tempnote;
}

function initPop() {
    $('[data-toggle="popover"]').popover();
}
