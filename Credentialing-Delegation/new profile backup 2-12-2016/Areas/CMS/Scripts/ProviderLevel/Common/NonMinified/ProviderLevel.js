var selectedIndex = 0;

var UpdateEventBind = function () {
    $("#ProviderLevelTableContent").find("[name='edit']").off("click").on("click", function () {
        var button = $(this);
        selectedIndex = button.closest('tr').index();
    });
};

$(function () {
    UpdateEventBind();
});

var onSuccessProviderLevel = function (data) {
    if (data.Status) {
        if (data.Type == "Add") {
            $("#ProviderLevelTableContent").prepend(data.Template);
            //$("#ProviderLevelTableContent tr").first().effect('highlight', { color: 'red !important' }, 5000);
        } else {
            $("#ProviderLevelTableContent tr").eq(selectedIndex).remove();
            if (selectedIndex != 0) {
                $("#ProviderLevelTableContent tr").eq(selectedIndex - 1).after(data.Template);
            } else {
                $("#ProviderLevelTableContent").prepend(data.Template);
            }
        }
        PopNotify("ProviderLevel", data.Message, "success");
        $("#ResetFormButton").click();
        UpdateEventBind();
    } else {
        PopNotify("ProviderLevel", data.Message, "error");
    }
};
