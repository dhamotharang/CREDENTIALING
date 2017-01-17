var selectedIndex = 0;

var UpdateEventBind = function () {
    $("#TextSnippetTableContent").find("[name='edit']").off("click").on("click", function () {
        var button = $(this);
        selectedIndex = button.closest('tr').index();
    });
};

$(function () {
    UpdateEventBind();
});

var onSuccessTextSnippet = function (data) {
    if (data.Status) {
        if (data.Type == "Add") {
            $("#TextSnippetTableContent").prepend(data.Template);
            //$("#TextSnippetTableContent tr").first().effect('highlight', { color: 'red !important' }, 5000);
        } else {
            $("#TextSnippetTableContent tr").eq(selectedIndex).remove();
            if (selectedIndex != 0) {
                $("#TextSnippetTableContent tr").eq(selectedIndex - 1).after(data.Template);
            } else {
                $("#TextSnippetTableContent").prepend(data.Template);
            }
        }
        PopNotify("TextSnippet", data.Message, "success");
        $("#ResetFormButton").click();
        UpdateEventBind();
    } else {
        PopNotify("TextSnippet", data.Message, "error");
    }
};
