var selectedIndex = 0;

var UpdateEventBind = function () {
    $("#PlanTableContent").find("[name='edit']").off("click").on("click", function () {
        var button = $(this);
        selectedIndex = button.closest('tr').index();
    });
};

$(function () {
    UpdateEventBind();
});

var onSuccessPlan = function (data) {
    if (data.Status) {
        if (data.Type == "Add") {
            $("#PlanTableContent").prepend(data.Template);
            //$("#PlanTableContent tr").first().effect('highlight', { color: 'red !important' }, 5000);
        } else {
            $("#PlanTableContent tr").eq(selectedIndex).remove();
            if (selectedIndex != 0) {
                $("#PlanTableContent tr").eq(selectedIndex - 1).after(data.Template);
            } else {
                $("#PlanTableContent").prepend(data.Template);
            }
        }
        PopNotify("Plan", data.Message, "success");
        $("#ResetFormButton").click();
        UpdateEventBind();
    } else {
        PopNotify("Plan", data.Message, "error");
    }
};
