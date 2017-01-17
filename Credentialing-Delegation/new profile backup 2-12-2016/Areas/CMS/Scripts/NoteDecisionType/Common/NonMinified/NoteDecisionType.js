var selectedIndex = 0;

var UpdateEventBind = function () {
    $("#NoteDecisionTypeTableContent").find("[name='edit']").off("click").on("click", function () {
        var button = $(this);
        selectedIndex = button.closest('tr').index();
    });
};

$(function () {
    UpdateEventBind();
});

var onSuccessNoteDecisionType = function (data) {
    if (data.Status) {
        if (data.Type == "Add") {
            $("#NoteDecisionTypeTableContent").prepend(data.Template);
            //$("#NoteDecisionTypeTableContent tr").first().effect('highlight', { color: 'red !important' }, 5000);
        } else {
            $("#NoteDecisionTypeTableContent tr").eq(selectedIndex).remove();
            if (selectedIndex != 0) {
                $("#NoteDecisionTypeTableContent tr").eq(selectedIndex - 1).after(data.Template);
            } else {
                $("#NoteDecisionTypeTableContent").prepend(data.Template);
            }
        }
        PopNotify("NoteDecisionType", data.Message, "success");
        $("#ResetFormButton").click();
        UpdateEventBind();
    } else {
        PopNotify("NoteDecisionType", data.Message, "error");
    }
};
