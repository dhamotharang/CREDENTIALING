var selectedIndex = 0;

var UpdateEventBind = function () {
    $("#DEAScheduleTableContent").find("[name='edit']").off("click").on("click", function () {
        var button = $(this);
        selectedIndex = button.closest('tr').index();
    });
};

$(function () {
    UpdateEventBind();
});

var onSuccessDEASchedule = function (data) {
    if (data.Status) {
        if (data.Type == "Add") {
            $("#DEAScheduleTableContent").prepend(data.Template);
            //$("#DEAScheduleTableContent tr").first().effect('highlight', { color: 'red !important' }, 5000);
        } else {
            $("#DEAScheduleTableContent tr").eq(selectedIndex).remove();
            if (selectedIndex != 0) {
                $("#DEAScheduleTableContent tr").eq(selectedIndex - 1).after(data.Template);
            } else {
                $("#DEAScheduleTableContent").prepend(data.Template);
            }
        }
        PopNotify("DEASchedule", data.Message, "success");
        $("#ResetFormButton").click();
        UpdateEventBind();
    } else {
        PopNotify("DEASchedule", data.Message, "error");
    }
};
