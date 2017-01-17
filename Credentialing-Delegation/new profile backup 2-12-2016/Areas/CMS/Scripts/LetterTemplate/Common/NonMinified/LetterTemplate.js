var selectedIndex = 0;

var UpdateEventBind = function () {
    $("#LetterTemplateTableContent").find("[name='edit']").off("click").on("click", function () {
        var button = $(this);
        selectedIndex = button.closest('tr').index();
    });
};

$(function () {
    UpdateEventBind();
});

var onSuccessLetterTemplate = function (data) {
    if (data.Status) {
        if (data.Type == "Add") {
            $("#LetterTemplateTableContent").prepend(data.Template);
            //$("#LetterTemplateTableContent tr").first().effect('highlight', { color: 'red !important' }, 5000);
        } else {
            $("#LetterTemplateTableContent tr").eq(selectedIndex).remove();
            if (selectedIndex != 0) {
                $("#LetterTemplateTableContent tr").eq(selectedIndex - 1).after(data.Template);
            } else {
                $("#LetterTemplateTableContent").prepend(data.Template);
            }
        }
        PopNotify("LetterTemplate", data.Message, "success");
        $("#ResetFormButton").click();
        UpdateEventBind();
    } else {
        PopNotify("LetterTemplate", data.Message, "error");
    }
};
