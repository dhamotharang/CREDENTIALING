var selectedIndex = 0;

var UpdateEventBind = function () {
    $("#DisabilityCategoryTableContent").find("[name='edit']").off("click").on("click", function () {
        var button = $(this);
        selectedIndex = button.closest('tr').index();
    });
};

$(function () {
    UpdateEventBind();
});

var onSuccessDisabilityCategory = function (data) {
    if (data.Status) {
        if (data.Type == "Add") {
            $("#DisabilityCategoryTableContent").prepend(data.Template);
            //$("#DisabilityCategoryTableContent tr").first().effect('highlight', { color: 'red !important' }, 5000);
        } else {
            $("#DisabilityCategoryTableContent tr").eq(selectedIndex).remove();
            if (selectedIndex != 0) {
                $("#DisabilityCategoryTableContent tr").eq(selectedIndex - 1).after(data.Template);
            } else {
                $("#DisabilityCategoryTableContent").prepend(data.Template);
            }
        }
        PopNotify("DisabilityCategory", data.Message, "success");
        $("#ResetFormButton").click();
        UpdateEventBind();
    } else {
        PopNotify("DisabilityCategory", data.Message, "error");
    }
};
