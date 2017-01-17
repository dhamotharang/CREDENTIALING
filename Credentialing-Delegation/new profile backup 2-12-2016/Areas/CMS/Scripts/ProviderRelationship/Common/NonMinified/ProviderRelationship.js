var selectedIndex = 0;

var UpdateEventBind = function () {
    $("#ProviderRelationshipTableContent").find("[name='edit']").off("click").on("click", function () {
        var button = $(this);
        selectedIndex = button.closest('tr').index();
    });
};

$(function () {
    UpdateEventBind();
});

var onSuccessProviderRelationship = function (data) {
    if (data.Status) {
        if (data.Type == "Add") {
            $("#ProviderRelationshipTableContent").prepend(data.Template);
            //$("#ProviderRelationshipTableContent tr").first().effect('highlight', { color: 'red !important' }, 5000);
        } else {
            $("#ProviderRelationshipTableContent tr").eq(selectedIndex).remove();
            if (selectedIndex != 0) {
                $("#ProviderRelationshipTableContent tr").eq(selectedIndex - 1).after(data.Template);
            } else {
                $("#ProviderRelationshipTableContent").prepend(data.Template);
            }
        }
        PopNotify("ProviderRelationship", data.Message, "success");
        $("#ResetFormButton").click();
        UpdateEventBind();
    } else {
        PopNotify("ProviderRelationship", data.Message, "error");
    }
};
