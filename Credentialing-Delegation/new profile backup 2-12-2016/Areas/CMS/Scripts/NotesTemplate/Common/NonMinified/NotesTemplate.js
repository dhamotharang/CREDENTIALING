var selectedIndex = 0;

var UpdateEventBind = function () {
    $("#NotesTemplateTableContent").find("[name='edit']").off("click").on("click", function () {
        var button = $(this);
        selectedIndex = button.closest('tr').index();
    });
};

$(function () {
    UpdateEventBind();
});

var onSuccessNotesTemplate = function (data) {
    if (data.Status) {
        if (data.Type == "Add") {
            $("#NotesTemplateTableContent").prepend(data.Template);
            //$("#NotesTemplateTableContent tr").first().effect('highlight', { color: 'red !important' }, 5000);
        } else {
            $("#NotesTemplateTableContent tr").eq(selectedIndex).remove();
            if (selectedIndex != 0) {
                $("#NotesTemplateTableContent tr").eq(selectedIndex - 1).after(data.Template);
            } else {
                $("#NotesTemplateTableContent").prepend(data.Template);
            }
        }
        PopNotify("NotesTemplate", data.Message, "success");
        $("#ResetFormButton").click();
        UpdateEventBind();
    } else {
        PopNotify("NotesTemplate", data.Message, "error");
    }
};
