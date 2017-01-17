var selectedIndex = 0;

var UpdateEventBind = function () {
    $("#ProviderProfileSectionTableContent").find("[name='edit']").off("click").on("click", function () {
        var button = $(this);
        selectedIndex = button.closest('tr').index();
    });
};

$(function () {
    UpdateEventBind();
});

var onSuccessProviderProfileSection = function (data) {
    if (data.Status) {
        if (data.Type == "Add") {
            $("#ProviderProfileSectionTableContent").prepend(data.Template);
            //$("#ProviderProfileSectionTableContent tr").first().effect('highlight', { color: 'red !important' }, 5000);
        } else {
            $("#ProviderProfileSectionTableContent tr").eq(selectedIndex).remove();
            if (selectedIndex != 0) {
                $("#ProviderProfileSectionTableContent tr").eq(selectedIndex - 1).after(data.Template);
            } else {
                $("#ProviderProfileSectionTableContent").prepend(data.Template);
            }
        }
        PopNotify("ProviderProfileSection", data.Message, "success");
        $("#ResetFormButton").click();
        UpdateEventBind();
    } else {
        PopNotify("ProviderProfileSection", data.Message, "error");
    }
};
