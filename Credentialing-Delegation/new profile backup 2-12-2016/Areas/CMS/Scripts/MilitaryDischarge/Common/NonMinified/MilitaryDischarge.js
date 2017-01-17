var selectedIndex = 0;

var UpdateEventBind = function () {
    $("#MilitaryDischargeTableContent").find("[name='edit']").off("click").on("click", function () {
        var button = $(this);
        selectedIndex = button.closest('tr').index();
    });
};

$(function () {
    UpdateEventBind();
});

var onSuccessMilitaryDischarge = function (data) {
    if (data.Status) {
        if (data.Type == "Add") {
            $("#MilitaryDischargeTableContent").prepend(data.Template);
            //$("#MilitaryDischargeTableContent tr").first().effect('highlight', { color: 'red !important' }, 5000);
        } else {
            $("#MilitaryDischargeTableContent tr").eq(selectedIndex).remove();
            if (selectedIndex != 0) {
                $("#MilitaryDischargeTableContent tr").eq(selectedIndex - 1).after(data.Template);
            } else {
                $("#MilitaryDischargeTableContent").prepend(data.Template);
            }
        }
        PopNotify("MilitaryDischarge", data.Message, "success");
        $("#ResetFormButton").click();
        UpdateEventBind();
    } else {
        PopNotify("MilitaryDischarge", data.Message, "error");
    }
};
