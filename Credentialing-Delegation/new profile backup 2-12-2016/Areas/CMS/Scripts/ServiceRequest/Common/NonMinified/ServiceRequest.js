var selectedIndex = 0;

var UpdateEventBind = function () {
    $("#ServiceRequestTableContent").find("[name='edit']").off("click").on("click", function () {
        var button = $(this);
        selectedIndex = button.closest('tr').index();
    });
};

$(function () {
    UpdateEventBind();
});

var onSuccessServiceRequest = function (data) {
    if (data.Status) {
        if (data.Type == "Add") {
            $("#ServiceRequestTableContent").prepend(data.Template);
            //$("#ServiceRequestTableContent tr").first().effect('highlight', { color: 'red !important' }, 5000);
        } else {
            $("#ServiceRequestTableContent tr").eq(selectedIndex).remove();
            if (selectedIndex != 0) {
                $("#ServiceRequestTableContent tr").eq(selectedIndex - 1).after(data.Template);
            } else {
                $("#ServiceRequestTableContent").prepend(data.Template);
            }
        }
        PopNotify("ServiceRequest", data.Message, "success");
        $("#ResetFormButton").click();
        UpdateEventBind();
    } else {
        PopNotify("ServiceRequest", data.Message, "error");
    }
};
