var selectedIndex = 0;

var UpdateEventBind = function () {
    $("#VisaStatusTableContent").find("[name='edit']").off("click").on("click", function () {
        var button = $(this);
        selectedIndex = button.closest('tr').index();
    });
};

$(function () {
    UpdateEventBind();
});

var onSuccessVisaStatus = function (data) {
    if (data.Status) {
        if (data.Type == "Add") {
            $("#VisaStatusTableContent").prepend(data.Template);
            //$("#VisaStatusTableContent tr").first().effect('highlight', { color: 'red !important' }, 5000);
        } else {
            $("#VisaStatusTableContent tr").eq(selectedIndex).remove();
            if (selectedIndex != 0) {
                $("#VisaStatusTableContent tr").eq(selectedIndex - 1).after(data.Template);
            } else {
                $("#VisaStatusTableContent").prepend(data.Template);
            }
        }
        PopNotify("VisaStatus", data.Message, "success");
        $("#ResetFormButton").click();
        UpdateEventBind();
    } else {
        PopNotify("VisaStatus", data.Message, "error");
    }
};
