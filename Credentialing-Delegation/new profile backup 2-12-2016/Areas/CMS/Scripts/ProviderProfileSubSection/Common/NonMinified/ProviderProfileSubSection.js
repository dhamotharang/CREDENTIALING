var selectedIndex = 0;

var UpdateEventBind = function () {
    $("#ProviderProfileSubSectionTableContent").find("[name='edit']").off("click").on("click", function () {
        var button = $(this);
        selectedIndex = button.closest('tr').index();
    });
};

$(function () {
    UpdateEventBind();
});

var onSuccessProviderProfileSubSection = function (data) {
    if (data.Status) {
        if (data.Type == "Add") {
            $("#ProviderProfileSubSectionTableContent").prepend(data.Template);
            //$("#ProviderProfileSubSectionTableContent tr").first().effect('highlight', { color: 'red !important' }, 5000);
        } else {
            $("#ProviderProfileSubSectionTableContent tr").eq(selectedIndex).remove();
            if (selectedIndex != 0) {
                $("#ProviderProfileSubSectionTableContent tr").eq(selectedIndex - 1).after(data.Template);
            } else {
                $("#ProviderProfileSubSectionTableContent").prepend(data.Template);
            }
        }
        PopNotify("ProviderProfileSubSection", data.Message, "success");
        $("#ResetFormButton").click();
        UpdateEventBind();
    } else {
        PopNotify("ProviderProfileSubSection", data.Message, "error");
    }
};
