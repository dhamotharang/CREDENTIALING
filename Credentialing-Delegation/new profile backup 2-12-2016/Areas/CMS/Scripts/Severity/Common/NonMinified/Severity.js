var selectedIndex = 0;

var UpdateEventBind = function () {
    $("#SeverityTableContent").find("[name='edit']").off("click").on("click", function () {
        var button = $(this);
        selectedIndex = button.closest('tr').index();
    });
};

$(function () {
    UpdateEventBind();
});

var onSuccessSeverity = function (data) {
    if (data.Status) {
        if (data.Type == "Add") {
            $("#SeverityTableContent").prepend(data.Template);
            //$("#SeverityTableContent tr").first().effect('highlight', { color: 'red !important' }, 5000);
        } else {
            $("#SeverityTableContent tr").eq(selectedIndex).remove();
            if (selectedIndex != 0) {
                $("#SeverityTableContent tr").eq(selectedIndex - 1).after(data.Template);
            } else {
                $("#SeverityTableContent").prepend(data.Template);
            }
        }
        PopNotify("Severity", data.Message, "success");
        $("#ResetFormButton").click();
        UpdateEventBind();
    } else {
        PopNotify("Severity", data.Message, "error");
    }
};
