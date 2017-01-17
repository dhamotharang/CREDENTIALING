$(function () {
    //masterData
    $('#NoteForm').off('click', '.noteTypeCms').on('click', '.noteTypeCms', function () {
        TabManager.openCenterModal('/CMSData/NoteType', 'Note Type');
    });
    $('#NoteForm').off('click', '.subjectCms').on('click', '.subjectCms', function () {
        TabManager.openCenterModal('/CMSData/NoteSubject', 'Note Subject');
    });

    $('#NoteForm').off('change', '#NoteType, #NoteSubject').on('change', '#NoteType, #NoteSubject', function () {
        SetDynamicNoteData('FromNoteTypeSubject');
    });

    $('#NoteForm').off('change', '.noteRadio').on('change', '.noteRadio', function () {
        SetDynamicNoteData('FromNoteStatus');
    });

    $('#listOfMedicalServices').off('click', '.plusButton').on('click', '.plusButton', function () {
        var index = $("#listOfMedicalServices > div").length;
        var div = '<div>' +
            '<div class="col-lg-10"> <textarea class="form-control input-xs inp Notes_Contact text-uppercase MedTextArea" cols="100" name="MedicalNecessaries[' + index + '].Description" onkeyup="do_resize(this)" placeholder="MEDICAL SERVICES" rows="1" style="font-size: 14px;margin-left:-10px"></textarea> </div>' +
            '<div class="col-lg-2"> <button class="btn btn-xs btn-danger minusButton"><i class="fa fa-minus"></i></button> <button class="btn btn-xs btn-success plusButton"><i class="fa fa-plus"></i></button> </div> <br/> </div>';
        $("#listOfMedicalServices").append(div);
        updateplusminusbtn();
    });

    $('#listOfMedicalServices').off('click', '.minusButton').on('click', '.minusButton', function () {
        var div = $(this).parent().parent()[0];
        $(div).remove();
        updateMedSevMapping();
        updateplusminusbtn();
    });
});

function SetDynamicNoteData(From)
{
    $('#NoteForm textarea').val('');
    var Notetype = $("#NoteType").val();
    var NoteSubject = $("#NoteSubject").val();
    var NoteStatus = "";
    if (From == "FromNoteTypeSubject")
    {
        if (Notetype.toUpperCase() == "MD REVIEW" && NoteSubject.toUpperCase() == "UM NOTE")
        {
            $("#ApproveRadio").prop('checked', true);
            NoteStatus = "APPROVE";
        }
    }
    else if (From == "FromNoteStatus")
    {
        if ($("#ApproveRadio").is(":checked"))
            NoteStatus = "APPROVE";
        else
            NoteStatus = "DENIAL";
    }
    DynamicNoteForm(Notetype, NoteSubject, NoteStatus);
    RemoveAllListOfMedSvc();
}

function DynamicNoteForm(Notetype, NoteSubject,NoteStatus)
{
    $("#NoteFormDiv > div").css('display', 'none');

    if(NoteSubject.toUpperCase() == 'UM NOTE')
    {
        if (Notetype.toUpperCase() == 'NURSE REVIEW')
        {
            $("#DenyNote").prependTo("#NoteFormDiv");
            $("#DenyNote").css('display', '');
        }
        else if (Notetype.toUpperCase() == 'MD REVIEW')
        {
            $("#ApproveDenyRadiobtns").prependTo("#NoteFormDiv");
            $("#ApproveDenyRadiobtns").css('display', '');
            if (NoteStatus == "APPROVE")
            {
                $("#ApproveNote").insertAfter("#ApproveDenyRadiobtns");
                $("#ApproveNote").css('display', '');
            }
            else
            {
                $("#DenyNote").insertAfter("#ApproveDenyRadiobtns");
                $("#DenyNote").css('display', '');
            }
        }
        else
        {
            $("#normalnote").prependTo("#NoteFormDiv");
            $("#normalnote").css('display', '');
        }
    }
    else
    {
        $("#normalnote").prependTo("#NoteFormDiv");
        $("#normalnote").css('display', '');
    }
}

function updateplusminusbtn()
{
    $('#listOfMedicalServices .minusButton').css('display', 'none');
    if ($("#listOfMedicalServices > div").length > 1)
        $('#listOfMedicalServices .minusButton').css('display', '');
    $('#listOfMedicalServices .plusButton').css('display', 'none');
    $('#listOfMedicalServices .plusButton').last().css('display', '');
}

function updateMedSevMapping()
{
    var $MedicalService = $('#listOfMedicalServices > div');
    var $MedTextArea = $('.MedTextArea');
    if ($MedicalService.length > 0) {
        for (var i = 0; i < $MedicalService.length; i++) 
            $MedTextArea[i].name = 'MedicalNecessaries[' + i + '].Description';
    }
}

function PrefillListOfMedSerList(ListOfMedSvc)
{
    $("#listOfMedicalServices > div").remove();
    for (var k = 0; k < ListOfMedSvc.length;k++)
    {
        var div = '<div>' +
            '<div class="col-lg-10"> <textarea class="form-control input-xs inp Notes_Contact text-uppercase MedTextArea" cols="100" name="MedicalNecessaries[' + k + '].Description" onkeyup="do_resize(this)" placeholder="MEDICAL SERVICES" style="font-size: 14px;margin-left:-10px" value="' + ListOfMedSvc[k] + '" rows="' + getRows(ListOfMedSvc[k]) + '">' + ListOfMedSvc[k] + '</textarea> </div>' +
            '<div class="col-lg-2"> <button class="btn btn-xs btn-danger minusButton"><i class="fa fa-minus"></i></button> <button class="btn btn-xs btn-success plusButton"><i class="fa fa-plus"></i></button> </div> <br/> </div>';
        $("#listOfMedicalServices").append(div);
    }
    updateplusminusbtn();
}

function RemoveAllListOfMedSvc()
{
    $("#listOfMedicalServices > div").remove();
    var div = '<div>' +
        '<div class="col-lg-10"> <textarea class="form-control input-xs inp Notes_Contact text-uppercase MedTextArea" cols="100" name="MedicalNecessaries[0].Description" onkeyup="do_resize(this)" placeholder="MEDICAL SERVICES" rows="1" style="font-size: 14px;margin-left:-10px"></textarea> </div>' +
        '<div class="col-lg-2"> <button class="btn btn-xs btn-danger minusButton" style="display:none"><i class="fa fa-minus"></i></button> <button class="btn btn-xs btn-success plusButton"><i class="fa fa-plus"></i></button> </div> <br/> </div>';
    $("#listOfMedicalServices").append(div);
}

function PrefillNoteFormData(note)
{
    for (var k in note)
    {
        if (k != "MedicalNecessaries")
            note[k] = (note[k] != null && isNaN(note[k])) ? (note[k].toUpperCase()) : note[k];
    }
    
    DynamicNoteForm(note.NoteType, note.Subject, note.NoteStatus);
    if (note.MedicalNecessaries && note.MedicalNecessaries.length > 0)
        PrefillListOfMedSerList(note.MedicalNecessaries);
    for (var i in note) {
        if (i != "MedicalNecessaries")
        {
            $("input[type = text][name='" + i + "']").val(note[i]);
            $("textarea[name='" + i + "']").val(note[i]);
            $("textarea[name='" + i + "']").attr('rows', getRows(note[i], true));
            $("select[name='" + i + "']").val(note[i]);
            $('input:radio[name="' + i + '"][value="' + note[i] + '"]').attr('checked', true);
            if (note[i] == "TRUE") {
                $('input:checkbox[name="' + i + '"]').attr('checked', true);
                $('input:checkbox[name="' + i + '"]').val(true);
            }
            else if (note[i] == "FALSE") 
                $('input:checkbox[name="' + i + '"]').val(false);
        }
    }
}

function getRows(text, NonMed) {
    if (text && text.length > 0) {
        var cols = 120;
        var arraytxt = text.split('\n');
        var rows = arraytxt.length;
        for (var i = 0; i < arraytxt.length; i++)
            rows += parseInt(arraytxt[i].length / cols);
        if (rows < 4 && NonMed)
            rows = 4;
        return rows;
    }
    return (NonMed==true)?4:1;
}

function do_resize10(textbox) {
    var maxrows = 4;
    var txt = textbox.value;
    var rows1 = textbox.rows;
    var cols = textbox.cols;
    var arraytxt = txt.split('\n');
    var rows = arraytxt.length;
    for (i = 0; i < arraytxt.length; i++)
        rows += parseInt(arraytxt[i].length / cols);
    if (rows > maxrows) textbox.rows = rows;
    else textbox.rows = maxrows;
}

function do_resize(textbox) {
    var maxrows = 1;
    var txt = textbox.value;
    var rows1 = textbox.rows;
    var cols = textbox.cols;
    var arraytxt = txt.split('\n');
    var rows = arraytxt.length;
    for (i = 0; i < arraytxt.length; i++)
        rows += parseInt(arraytxt[i].length / cols);
    if (rows > maxrows) textbox.rows = rows;
    else textbox.rows = maxrows;
}

NoteAdd = function () {
    $("#NoteSaveCancelButton .SaveBtn").addClass("SaveCreateNote");
    $("#NoteType").val("AUTH NOTE");
    $("#NoteSubject").val("UM NOTE");
};

$("#NoteSaveCancelButton").off("click", ".SaveCreateNote").on("click", ".SaveCreateNote", function () {
    $.ajax({
        url: '/Authorization/SaveNote',
        data: $('#note_form').serialize(),
        type: "POST",
        cache: false,
        dataType: "html",
        success: function (data) {
            $("#emptyNoteTemplate").remove();
            data = JSON.parse(data);
            var notes = data.data;
            notes.Date = moment(new Date()).format('MM/DD/YYYY HH:mm:ss');
            notes.UserName = "BARBARA JOY";
            var hiddeninputdata = "";
            for (var k in notes) {
                if (k == "MedicalNecessaries" && notes[k] && notes[k].length > 0) {
                    var Med = notes[k];
                    for (var i = 0; i < Med.length; i++) {
                        Med[i].Description = (Med[i].Description == null) ? '' : Med[i].Description;
                        hiddeninputdata = hiddeninputdata + ' <input type="hidden" name="Notes[0].MedicalNecessaries[' + i + '].Description" value="' + Med[i].Description + '" /> ';
                        Med[i].Description = (Med[i].Description == '') ? '-' : Med[i].Description;
                    }
                }
                else {
                    notes[k] = (notes[k] == null) ? '' : notes[k];
                    hiddeninputdata = hiddeninputdata + ' <input type="hidden" name="Notes[0].' + k + '" value="' + notes[k] + '" /> ';
                    notes[k] = (notes[k] == '') ? '-' : notes[k];
                }
            }
            var notedesc = "";
            if (notes.Subject.toUpperCase() == "UM NOTE") {
                if (notes.NoteType.toUpperCase() == "NURSE REVIEW" || (notes.NoteType.toUpperCase() == "MD REVIEW" && notes.NoteStatus == "DENIAL"))
                    notedesc = '<span style="cursor:pointer" data-container="body" data-toggle="popover" title="Note" data-trigger="hover" data-placement="top" data-html="true" data-content="' + notes.Description + '<br/>Rationale: ' + notes.RationaleDescription + '<br/>Alternate Plan: ' + notes.AlternatePlanDescription + '<br/>Criteria Used: ' + notes.CriteriaUsedDescription + '<br/>Service Subject To Notice: ' + notes.ServiceSubjectToNotice + '<br/>List Of Medical Services: ' + notes.MedicalNecessaries[0].Description + '" onmouseover="initPop()"><b>...</b></span>'
                else if (notes.NoteType.toUpperCase() == "MD REVIEW" && notes.NoteStatus == "APPROVE")
                    notedesc = '<span style="cursor:pointer" data-container="body" data-toggle="popover" title="Note" data-trigger="hover" data-placement="top" data-html="true" data-content="' + notes.Description + '<br/>Criteria Used: ' + notes.CriteriaUsedDescription + '" onmouseover="initPop()"><b>...</b></span>'
                else if (notes.Description && notes.Description.length > 10)
                    notedesc = '<span style="cursor:pointer" data-container="body" data-toggle="popover" title="Note" data-trigger="hover" data-placement="top" data-html="true" data-content="' + notes.Description + '" onmouseover="initPop()"><b>...</b></span>';
            }
            else if (notes.Description && notes.Description.length > 10)
                notedesc = '<span style="cursor:pointer" data-container="body" data-toggle="popover" title="Note" data-trigger="hover" data-placement="top" data-html="true" data-content="' + notes.Description + '" onmouseover="initPop()"><b>...</b></span>';

            var tr = '<tr><td class="theme_label_data"> ' + notes.Date + hiddeninputdata + ' </td>' +
                '<td class="theme_label_data">' + notes.NoteType + '</td>' +
                '<td class="theme_label_data">' + notes.UserName + '</td>' +
                '<td class="theme_label_data">' + notes.Subject + '</td>' +
                '<td class="theme_label_data">' + notes.Description.substr(0, 10) + notedesc + '</td>' +
                '<td class="theme_label_data"> <input type="checkbox" class="checkbox-radio noteincfax" name="Notes[0].IncludeFax" value="false"><label><span></span></label> </td>' +
                '<td class="theme_label_data"> <button type="button" class="btn btn-xs btn-success" onclick="ViewNote(this)"><i class="fa fa-eye"></i></button>' +
                '<button type="button" class="btn btn-xs btn-primary" onclick="EditNote(this)"><i class="fa fa-pencil"></i></button>' +
                '<button type="button" class="btn btn-danger btn-xs" onclick="DeleteNote(this)"><i class="fa fa-trash-o"></i></button></td>' +
            '</tr>';
            $('#NotesArea').prepend(tr);
            $(".close_modal_btn").click();
            updateNotesMapping();
        }
    });
});

NoteEdit = function (note) {
    $("#NoteSaveCancelButton .SaveBtn").addClass("SaveEditNote");
    PrefillNoteFormData(note);
};

$("#NoteSaveCancelButton").off("click", ".SaveEditNote").on("click", ".SaveEditNote", function () {
    var id = $("input[name=tempNoteID]").val();
    id = parseInt(id);
    $("#NotesArea > tr").eq(id).remove();
    $.ajax({
        url: '/Authorization/SaveNote',
        data: $('#note_form').serialize(),
        type: "POST",
        cache: false,
        dataType: "html",
        success: function (data) {
            data = JSON.parse(data);
            var notes = data.data;
            var index = id;
            notes.Date = moment(new Date()).format('MM/DD/YYYY HH:mm:ss');
            notes.UserName = "BARBARA JOY";

            var hiddeninputdata = "";
            for (var k in notes) {
                if (k == "MedicalNecessaries" && notes[k] && notes[k].length > 0) {
                    var Med = notes[k];
                    for (var i = 0; i < Med.length; i++) {
                        Med[i].Description = (Med[i].Description == null) ? '' : Med[i].Description;
                        hiddeninputdata = hiddeninputdata + ' <input type="hidden" name="Notes[' + index + '].MedicalNecessaries[' + i + '].Description" value="' + Med[i].Description + '" /> '
                        Med[i].Description = (Med[i].Description == '') ? '-' : Med[i].Description;
                    }
                }
                else {
                    notes[k] = (notes[k] == null) ? '' : notes[k];
                    hiddeninputdata = hiddeninputdata + ' <input type="hidden" name="Notes[' + index + '].' + k + '" value="' + notes[k] + '" /> ';
                    notes[k] = (notes[k] === '') ? '-' : notes[k];
                }
            }

            var notedesc = "";
            if (notes.Subject.toUpperCase() == "UM NOTE") {
                if (notes.NoteType.toUpperCase() == "NURSE REVIEW" || (notes.NoteType.toUpperCase() == "MD REVIEW" && notes.NoteStatus == "DENIAL"))
                    notedesc = '<span style="cursor:pointer" data-container="body" data-toggle="popover" title="Note" data-trigger="hover" data-placement="top" data-html="true" data-content="' + notes.Description + '<br/>Rationale: ' + notes.RationaleDescription + '<br/>Alternate Plan: ' + notes.AlternatePlanDescription + '<br/>Criteria Used: ' + notes.CriteriaUsedDescription + '<br/>Service Subject To Notice: ' + notes.ServiceSubjectToNotice + '<br/>List Of Medical Services: ' + notes.MedicalNecessaries[0].Description + '" onmouseover="initPop()"><b>...</b></span>'
                else if (notes.NoteType.toUpperCase() == "MD REVIEW" && notes.NoteStatus == "APPROVE")
                    notedesc = '<span style="cursor:pointer" data-container="body" data-toggle="popover" title="Note" data-trigger="hover" data-placement="top" data-html="true" data-content="' + notes.Description + '<br/>Criteria Used: ' + notes.CriteriaUsedDescription + '" onmouseover="initPop()"><b>...</b></span>'
                else if (notes.Description && notes.Description.length > 10)
                    notedesc = '<span style="cursor:pointer" data-container="body" data-toggle="popover" title="Note" data-trigger="hover" data-placement="top" data-html="true" data-content="' + notes.Description + '" onmouseover="initPop()"><b>...</b></span>';
            }
            else if (notes.Description && notes.Description.length > 10)
                notedesc = '<span style="cursor:pointer" data-container="body" data-toggle="popover" title="Note" data-trigger="hover" data-placement="top" data-html="true" data-content="' + notes.Description + '" onmouseover="initPop()"><b>...</b></span>';

            if (notes.IncludeFax == true)
                var incfax = '<input type="checkbox" class="checkbox-radio noteincfax" name="Notes[' + index + '].IncludeFax" value="true" checked="checked"/> <label><span></span></label>';
            else
                var incfax = '<input type="checkbox" class="checkbox-radio noteincfax" name="Notes[' + index + '].IncludeFax" value="false"/> <label><span></span></label>';

            var tr = '<tr><td class="theme_label_data"> ' + notes.Date + hiddeninputdata + ' </td>' +
                '<td class="theme_label_data">' + notes.NoteType + '</td>' +
                '<td class="theme_label_data">' + notes.UserName + '</td>' +
                '<td class="theme_label_data">' + notes.Subject + '</td>' +
                '<td class="theme_label_data">' + notes.Description.substr(0, 10) + notedesc + '</td>' +
                '<td class="theme_label_data">' + incfax + '</td>' +
                '<td class="theme_label_data"> <button type="button" class="btn btn-xs btn-success" onclick="ViewNote(this)"><i class="fa fa-eye"></i></button>' +
                '<button type="button" class="btn btn-xs btn-primary" onclick="EditNote(this)"><i class="fa fa-pencil"></i></button>' +
                '<button type="button" class="btn btn-danger btn-xs" onclick="DeleteNote(this)"><i class="fa fa-trash-o"></i></button></td>' +
            '</tr>';
            if (id == 0)
                $('#NotesArea').prepend(tr);
            else
                $('#NotesArea > tr').eq(id - 1).after(tr);
            $(".SaveBtn").removeClass("SaveEditNotes");
            $(".close_modal_btn").click();
        }
    });
});

NoteView = function (note) {
    PrefillNoteFormData(note);
    //To disable all input fields for View Notes
    $(".NotesModal :input").attr("disabled", true);
};
