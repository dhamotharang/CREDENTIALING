var selectedIndex = 0;

var UpdateEventBind = function () {
    $("#NoteTypeTableContent").find("[name='edit']").off("click").on("click", function () {
        var button = $(this);
        selectedIndex = button.closest('tr').index();
    });
};

$(function () {
    UpdateEventBind();
});

var onSuccessNoteType = function (data) {
    if (data.Status) {
        if (data.Type == "Add") {
            $("#NoteTypeTableContent").prepend(data.Template);
            //$("#NoteTypeTableContent tr").first().effect('highlight', { color: 'red !important' }, 5000);
        } else {
            $("#NoteTypeTableContent tr").eq(selectedIndex).remove();
            if (selectedIndex != 0) {
                $("#NoteTypeTableContent tr").eq(selectedIndex - 1).after(data.Template);
            } else {
                $("#NoteTypeTableContent").prepend(data.Template);
            }
        }
        PopNotify("NoteType", data.Message, "success");
        $("#ResetFormButton").click();
        UpdateEventBind();
    } else {
        PopNotify("NoteType", data.Message, "error");
    }
};
