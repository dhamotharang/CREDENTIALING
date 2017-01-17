var selectedIndex = 0;

var UpdateEventBind = function () {
    $("#ProviderModeTableContent").find("[name='edit']").off("click").on("click", function () {
        var button = $(this);
        selectedIndex = button.closest('tr').index();
    });
};

$(function () {
    UpdateEventBind();
});

var onSuccessProviderMode = function (data) {
    if (data.Status) {
        if (data.Type == "Add") {
            $("#ProviderModeTableContent").prepend(data.Template);
            //$("#ProviderModeTableContent tr").first().effect('highlight', { color: 'red !important' }, 5000);
        } else {
            $("#ProviderModeTableContent tr").eq(selectedIndex).remove();
            if (selectedIndex != 0) {
                $("#ProviderModeTableContent tr").eq(selectedIndex - 1).after(data.Template);
            } else {
                $("#ProviderModeTableContent").prepend(data.Template);
            }
        }
        PopNotify("ProviderMode", data.Message, "success");
        $("#ResetFormButton").click();
        UpdateEventBind();
    } else {
        PopNotify("ProviderMode", data.Message, "error");
    }
};
