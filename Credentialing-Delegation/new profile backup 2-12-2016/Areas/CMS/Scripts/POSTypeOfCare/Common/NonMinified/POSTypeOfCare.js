var selectedIndex = 0;

var UpdateEventBind = function () {
    $("#POSTypeOfCareTableContent").find("[name='edit']").off("click").on("click", function () {
        var button = $(this);
        selectedIndex = button.closest('tr').index();
    });
};

$(function () {
    UpdateEventBind();
});

var onSuccessPOSTypeOfCare = function (data) {
    if (data.Status) {
        if (data.Type == "Add") {
            $("#POSTypeOfCareTableContent").prepend(data.Template);
            //$("#POSTypeOfCareTableContent tr").first().effect('highlight', { color: 'red !important' }, 5000);
        } else {
            $("#POSTypeOfCareTableContent tr").eq(selectedIndex).remove();
            if (selectedIndex != 0) {
                $("#POSTypeOfCareTableContent tr").eq(selectedIndex - 1).after(data.Template);
            } else {
                $("#POSTypeOfCareTableContent").prepend(data.Template);
            }
        }
        PopNotify("POSTypeOfCare", data.Message, "success");
        $("#ResetFormButton").click();
        UpdateEventBind();
    } else {
        PopNotify("POSTypeOfCare", data.Message, "error");
    }
};
