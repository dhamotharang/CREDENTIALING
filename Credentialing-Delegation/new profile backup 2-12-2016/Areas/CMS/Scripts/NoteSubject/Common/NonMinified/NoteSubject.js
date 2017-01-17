var selectedIndex = 0;

var UpdateEventBind = function () {
    $("#NoteSubjectTableContent").find("[name='edit']").off("click").on("click", function () {
        var button = $(this);
        selectedIndex = button.closest('tr').index();
    });
};

$(function () {
    UpdateEventBind();
});

var onSuccessNoteSubject = function (data) {
    if (data.Status) {
        if (data.Type == "Add") {
            $("#NoteSubjectTableContent").prepend(data.Template);
            //$("#NoteSubjectTableContent tr").first().effect('highlight', { color: 'red !important' }, 5000);
        } else {
            $("#NoteSubjectTableContent tr").eq(selectedIndex).remove();
            if (selectedIndex != 0) {
                $("#NoteSubjectTableContent tr").eq(selectedIndex - 1).after(data.Template);
            } else {
                $("#NoteSubjectTableContent").prepend(data.Template);
            }
        }
        PopNotify("NoteSubject", data.Message, "success");
        $("#ResetFormButton").click();
        UpdateEventBind();
    } else {
        PopNotify("NoteSubject", data.Message, "error");
    }
};
