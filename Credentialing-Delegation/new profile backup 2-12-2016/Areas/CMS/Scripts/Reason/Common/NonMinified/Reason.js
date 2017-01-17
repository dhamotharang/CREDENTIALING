var selectedIndex = 0;

var UpdateEventBind = function () {
    $("#ReasonTableContent").find("[name='edit']").off("click").on("click", function () {
        var button = $(this);
        selectedIndex = button.closest('tr').index();
    });
};

$(function () {
    UpdateEventBind();
});

var onSuccessReason = function (data) {
    if (data.Status) {
        if (data.Type == "Add") {
            $("#ReasonTableContent").prepend(data.Template);
            //$("#ReasonTableContent tr").first().effect('highlight', { color: 'red !important' }, 5000);
        } else {
            $("#ReasonTableContent tr").eq(selectedIndex).remove();
            if (selectedIndex != 0) {
                $("#ReasonTableContent tr").eq(selectedIndex - 1).after(data.Template);
            } else {
                $("#ReasonTableContent").prepend(data.Template);
            }
        }
        PopNotify("Reason", data.Message, "success");
        $("#ResetFormButton").click();
        UpdateEventBind();
    } else {
        PopNotify("Reason", data.Message, "error");
    }
};
