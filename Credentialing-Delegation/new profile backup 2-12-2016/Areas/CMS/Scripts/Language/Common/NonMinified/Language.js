var selectedIndex = 0;

var UpdateEventBind = function () {
    $("#LanguageTableContent").find("[name='edit']").off("click").on("click", function () {
        var button = $(this);
        selectedIndex = button.closest('tr').index();
    });
};

$(function () {
    UpdateEventBind();
});

var onSuccessLanguage = function (data) {
    if (data.Status) {
        if (data.Type == "Add") {
            $("#LanguageTableContent").prepend(data.Template);
            //$("#LanguageTableContent tr").first().effect('highlight', { color: 'red !important' }, 5000);
        } else {
            $("#LanguageTableContent tr").eq(selectedIndex).remove();
            if (selectedIndex != 0) {
                $("#LanguageTableContent tr").eq(selectedIndex - 1).after(data.Template);
            } else {
                $("#LanguageTableContent").prepend(data.Template);
            }
        }
        PopNotify("Language", data.Message, "success");
        $("#ResetFormButton").click();
        UpdateEventBind();
    } else {
        PopNotify("Language", data.Message, "error");
    }
};
