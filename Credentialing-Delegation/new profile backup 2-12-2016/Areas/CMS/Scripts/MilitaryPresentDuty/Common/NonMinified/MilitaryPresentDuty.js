var selectedIndex = 0;

var UpdateEventBind = function () {
    $("#MilitaryPresentDutyTableContent").find("[name='edit']").off("click").on("click", function () {
        var button = $(this);
        selectedIndex = button.closest('tr').index();
    });
};

$(function () {
    UpdateEventBind();
});

var onSuccessMilitaryPresentDuty = function (data) {
    if (data.Status) {
        if (data.Type == "Add") {
            $("#MilitaryPresentDutyTableContent").prepend(data.Template);
            //$("#MilitaryPresentDutyTableContent tr").first().effect('highlight', { color: 'red !important' }, 5000);
        } else {
            $("#MilitaryPresentDutyTableContent tr").eq(selectedIndex).remove();
            if (selectedIndex != 0) {
                $("#MilitaryPresentDutyTableContent tr").eq(selectedIndex - 1).after(data.Template);
            } else {
                $("#MilitaryPresentDutyTableContent").prepend(data.Template);
            }
        }
        PopNotify("MilitaryPresentDuty", data.Message, "success");
        $("#ResetFormButton").click();
        UpdateEventBind();
    } else {
        PopNotify("MilitaryPresentDuty", data.Message, "error");
    }
};
