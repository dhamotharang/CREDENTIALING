var selectedIndex = 0;

var UpdateEventBind = function () {
    $("#DecredentialingReasonTableContent").find("[name='edit']").off("click").on("click", function () {
        var button = $(this);
        selectedIndex = button.closest('tr').index();
    });
};

$(function () {
    UpdateEventBind();
});

var onSuccessDecredentialingReason = function (data) {
    if (data.Status) {
        if (data.Type == "Add") {
            $("#DecredentialingReasonTableContent").prepend(data.Template);
            //$("#DecredentialingReasonTableContent tr").first().effect('highlight', { color: 'red !important' }, 5000);
        } else {
            $("#DecredentialingReasonTableContent tr").eq(selectedIndex).remove();
            if (selectedIndex != 0) {
                $("#DecredentialingReasonTableContent tr").eq(selectedIndex - 1).after(data.Template);
            } else {
                $("#DecredentialingReasonTableContent").prepend(data.Template);
            }
        }
        PopNotify("DecredentialingReason", data.Message, "success");
        $("#ResetFormButton").click();
        UpdateEventBind();
    } else {
        PopNotify("DecredentialingReason", data.Message, "error");
    }
};
