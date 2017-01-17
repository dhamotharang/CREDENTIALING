var selectedIndex = 0;

var UpdateEventBind = function () {
    $("#ProviderTypeTableContent").find("[name='edit']").off("click").on("click", function () {
        var button = $(this);
        selectedIndex = button.closest('tr').index();
    });
};

$(function () {
    UpdateEventBind();
});

var onSuccessProviderType = function (data) {
    if (data.Status) {
        if (data.Type == "Add") {
            $("#ProviderTypeTableContent").prepend(data.Template);
            //$("#ProviderTypeTableContent tr").first().effect('highlight', { color: 'red !important' }, 5000);
        } else {
            $("#ProviderTypeTableContent tr").eq(selectedIndex).remove();
            if (selectedIndex != 0) {
                $("#ProviderTypeTableContent tr").eq(selectedIndex - 1).after(data.Template);
            } else {
                $("#ProviderTypeTableContent").prepend(data.Template);
            }
        }
        PopNotify("ProviderType", data.Message, "success");
        $("#ResetFormButton").click();
        UpdateEventBind();
    } else {
        PopNotify("ProviderType", data.Message, "error");
    }
};
