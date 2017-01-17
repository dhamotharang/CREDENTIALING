var emptyNotesTableTemplate = '<tr id="emptyNoteTemplate" style="text-align:center"><td colspan="7" align="center" class="theme_label_data">No Data Available</td></tr>';

//Add Note btn clicked
function AddNewNote() {
    TabManager.openFloatingModal('/Authorization/OpenNoteModal', '~/Areas/UM/Views/Common/Modal_Header_Footer/Note/Header/_AddNote.cshtml', '~/Areas/UM/Views/Common/Modal_Header_Footer/Note/Footer/_SaveCancel.cshtml', '', 'NoteAdd', '');
}

//Edit Note btn clicked
function EditNote(event) {
    //Getting parent tr of the btn, because inside tr(1st td) we have all hidden input data to be sent for edit/view
    var tr = $(event).parent().parent()[0];
    var noteobj = getNoteObject(tr);
    //noteobj is used in Callback Function (NoteEdit) to prefill note object for edit
    TabManager.openFloatingModal('/Authorization/OpenNoteModal', '~/Areas/UM/Views/Common/Modal_Header_Footer/Note/Header/_EditNote.cshtml', '~/Areas/UM/Views/Common/Modal_Header_Footer/Note/Footer/_SaveCancel.cshtml', ' ', 'NoteEdit', noteobj);
}

//View Note btn clicked
function ViewNote(event) {
    var tr = $(event).parent().parent()[0];
    var noteobj = getNoteObject(tr);
    TabManager.openFloatingModal('/Authorization/OpenNoteModal', '~/Areas/UM/Views/Common/Modal_Header_Footer/Note/Header/_ViewNote.cshtml', '~/Areas/UM/Views/Common/Modal_Header_Footer/Note/Footer/_Cancel.cshtml', ' ', 'NoteView', noteobj);
}

function getNoteObject(tr)
{
    var tempnote = {};
    var MedicalNecessaries = [];

    var id = $("#NotesArea > tr").index(tr);
    var element = $(tr);
    var NoteInputFields = element.find("input");
        
    //Inside tr, 1st td tag, list of all hidden input note data is put into temporary note object to send to view/edit (Retrieving data via name of all hidden input element)
    if (NoteInputFields && NoteInputFields.length > 0)
    {
        for (var k in NoteInputFields) {
            if (NoteInputFields[k].nodeName == "INPUT") {
                var name = NoteInputFields[k].name;
                var dotIndex = name.indexOf(".");
                var key = name.slice(dotIndex + 1);
                if (key.includes("MedicalNecessaries"))
                {
                    if (NoteInputFields[k].value)
                        MedicalNecessaries.push(NoteInputFields[k].value);
                }
                else
                {
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

function DeleteNote(event) {
    //Getting parent tr and Deleting complete row
    var tr = $(event).parent().parent()[0];
    $(tr).remove();
    var index = $("#NotesArea > tr").length;
    if (index == 0)
        $("#NotesArea").append(emptyNotesTableTemplate);
    else
        updateNotesMapping();
}

//All hidden input elements inside 1st td of each tr is updated after deletion of any row
var updateNotesMapping = function ()
{
    var $NoteRows = $('#NotesArea > tr');
    if ($NoteRows.length > 0) {
        for (var i = 0; i < $NoteRows.length; i++)
        {
            var element = $($NoteRows[i]);
            var NoteInputFields = element.find("input");
            if (NoteInputFields && NoteInputFields.length > 0) {
                for (var k in NoteInputFields) {
                    if (NoteInputFields[k].nodeName == "INPUT") {
                        var res = NoteInputFields[k].name.split("[");//splitting based on "[" so we get name divide into eg:-"Contacts[" and "].CallDateTime"
                        res[0] = res[0] + "[";//adding [
                        if (res.length == 3) {
                            if (NoteInputFields[k].value == "")
                                continue;
                            res[1] = res[1] + "[";
                            res[1] = (res[1] + res[2]).substring(1);
                        }
                        else 
                            res[1] = res[1].substring(1);//removing the previous index
                        NoteInputFields[k].name = (res[0] + i + res[1]).toString();//adding the updated index
                    }
                }
            }
        }
    }
}

//Print all note data from the note table
function PrintNotes() {
    $("#PrintMemberName").text($("#MemberName").text());
    $("#PrintMemberID").text($("#MemberID").text());
    $("#PrintMemberRefID").text($("#Refnum").text());
    $("#PrintMemberDOB").text($("#MemberDob").text());
    $("#PrintMemberAge").text($("#MemberAge").text());
    $("#DetailsNote").html('');
    var $NoteRow = $('#NotesArea > tr');
    if ($NoteRow.length > 0)
    {
        for (var i = 0; i < $NoteRow.length; i++) {
            if($NoteRow[i].id=="emptyNoteTemplate")
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
}

//Print note from above div created
var printNotesData = function () {
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

function initPop() {
    $('[data-toggle="popover"]').popover();
}

$("#NotesArea").off("click", ".noteincfax").on("click", ".noteincfax", function () {
    if (this.value == "false" || this.value == false) {
        $(this).attr("checked", true);
        $(this).val(true);
    }
    else if (this.value == "true" || this.value == true) {
        $(this).removeAttr('checked');
        $(this).val(false);
    }
});