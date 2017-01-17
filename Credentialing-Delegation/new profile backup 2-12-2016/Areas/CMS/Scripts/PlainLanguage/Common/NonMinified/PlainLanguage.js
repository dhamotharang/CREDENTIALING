var selectedIndex = 0;

var UpdateEventBind = function () {
    $("#PlainLanguageTableContent").find("[name='edit']").off("click").on("click", function () {
        var button = $(this);
        selectedIndex = button.closest('tr').index();
    });
};

$(function () {
    UpdateEventBind();
});

var onSuccessPlainLanguage = function (data) {
    if (data.Status) {
        if (data.Type == "Add") {
            $("#PlainLanguageTableContent").prepend(data.Template);
            //$("#PlainLanguageTableContent tr").first().effect('highlight', { color: 'red !important' }, 5000);
        } else {
            $("#PlainLanguageTableContent tr").eq(selectedIndex).remove();
            if (selectedIndex != 0) {
                $("#PlainLanguageTableContent tr").eq(selectedIndex - 1).after(data.Template);
            } else {
                $("#PlainLanguageTableContent").prepend(data.Template);
            }
        }
        PopNotify("PlainLanguage", data.Message, "success");
        $("#ResetFormButton").click();
        UpdateEventBind();
    } else {
        PopNotify("PlainLanguage", data.Message, "error");
    }
};
