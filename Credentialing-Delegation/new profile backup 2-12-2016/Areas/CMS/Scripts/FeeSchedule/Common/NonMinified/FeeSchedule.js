var selectedIndex = 0;

var UpdateEventBind = function () {
    $("#FeeScheduleTableContent").find("[name='edit']").off("click").on("click", function () {
        var button = $(this);
        selectedIndex = button.closest('tr').index();
    });
};

$(function () {
    UpdateEventBind();
});

var onSuccessFeeSchedule = function (data) {
    if (data.Status) {
        if (data.Type == "Add") {
            $("#FeeScheduleTableContent").prepend(data.Template);
            //$("#FeeScheduleTableContent tr").first().effect('highlight', { color: 'red !important' }, 5000);
        } else {
            $("#FeeScheduleTableContent tr").eq(selectedIndex).remove();
            if (selectedIndex != 0) {
                $("#FeeScheduleTableContent tr").eq(selectedIndex - 1).after(data.Template);
            } else {
                $("#FeeScheduleTableContent").prepend(data.Template);
            }
        }
        PopNotify("FeeSchedule", data.Message, "success");
        $("#ResetFormButton").click();
        UpdateEventBind();
    } else {
        PopNotify("FeeSchedule", data.Message, "error");
    }
};
