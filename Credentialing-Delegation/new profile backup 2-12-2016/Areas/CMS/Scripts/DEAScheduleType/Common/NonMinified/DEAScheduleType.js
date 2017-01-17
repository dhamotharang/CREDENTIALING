var selectedIndex = 0;

var UpdateEventBind = function () {
    $("#DEAScheduleTypeTableContent").find("[name='edit']").off("click").on("click", function () {
        var button = $(this);
        selectedIndex = button.closest('tr').index();
    });
};

$(function () {
    UpdateEventBind();
});

var onSuccessDEAScheduleType = function (data) {
    if (data.Status) {
        if (data.Type == "Add") {
            $("#DEAScheduleTypeTableContent").prepend(data.Template);
            //$("#DEAScheduleTypeTableContent tr").first().effect('highlight', { color: 'red !important' }, 5000);
        } else {
            $("#DEAScheduleTypeTableContent tr").eq(selectedIndex).remove();
            if (selectedIndex != 0) {
                $("#DEAScheduleTypeTableContent tr").eq(selectedIndex - 1).after(data.Template);
            } else {
                $("#DEAScheduleTypeTableContent").prepend(data.Template);
            }
        }
        PopNotify("DEAScheduleType", data.Message, "success");
        $("#ResetFormButton").click();
        UpdateEventBind();
    } else {
        PopNotify("DEAScheduleType", data.Message, "error");
    }
};
