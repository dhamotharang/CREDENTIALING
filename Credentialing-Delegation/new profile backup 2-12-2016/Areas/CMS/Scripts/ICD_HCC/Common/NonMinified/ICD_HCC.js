var selectedIndex = 0;

var UpdateEventBind = function () {
    $("#ICD_HCCTableContent").find("[name='edit']").off("click").on("click", function () {
        var button = $(this);
        selectedIndex = button.closest('tr').index();
    });
};

$(function () {
    UpdateEventBind();
});

var onSuccessICD_HCC = function (data) {
    if (data.Status) {
        if (data.Type == "Add") {
            $("#ICD_HCCTableContent").prepend(data.Template);
            //$("#ICD_HCCTableContent tr").first().effect('highlight', { color: 'red !important' }, 5000);
        } else {
            $("#ICD_HCCTableContent tr").eq(selectedIndex).remove();
            if (selectedIndex != 0) {
                $("#ICD_HCCTableContent tr").eq(selectedIndex - 1).after(data.Template);
            } else {
                $("#ICD_HCCTableContent").prepend(data.Template);
            }
        }
        PopNotify("ICD_HCC", data.Message, "success");
        $("#ResetFormButton").click();
        UpdateEventBind();
    } else {
        PopNotify("ICD_HCC", data.Message, "error");
    }
};
