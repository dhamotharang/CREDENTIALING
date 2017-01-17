var selectedIndex = 0;

var UpdateEventBind = function () {
    $("#NotesCategoryTableContent").find("[name='edit']").off("click").on("click", function () {
        var button = $(this);
        selectedIndex = button.closest('tr').index();
    });
};

$(function () {
    UpdateEventBind();
});

var onSuccessNotesCategory = function (data) {
    if (data.Status) {
        if (data.Type == "Add") {
            $("#NotesCategoryTableContent").prepend(data.Template);
            //$("#NotesCategoryTableContent tr").first().effect('highlight', { color: 'red !important' }, 5000);
        } else {
            $("#NotesCategoryTableContent tr").eq(selectedIndex).remove();
            if (selectedIndex != 0) {
                $("#NotesCategoryTableContent tr").eq(selectedIndex - 1).after(data.Template);
            } else {
                $("#NotesCategoryTableContent").prepend(data.Template);
            }
        }
        PopNotify("NotesCategory", data.Message, "success");
        $("#ResetFormButton").click();
        UpdateEventBind();
    } else {
        PopNotify("NotesCategory", data.Message, "error");
    }
};
