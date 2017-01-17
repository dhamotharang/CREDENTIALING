var selectedIndex = 0;

var UpdateEventBind = function () {
    $("#ContactReasonTableContent").find("[name='edit']").off("click").on("click", function () {
        var button = $(this);
        selectedIndex = button.closest('tr').index();
    });
};

$(function () {
    UpdateEventBind();
});

var onSuccessContactReason = function (data) {
    if (data.Status) {
        if (data.Type == "Add") {
            $("#ContactReasonTableContent").prepend(data.Template);
            //$("#ContactReasonTableContent tr").first().effect('highlight', { color: 'red !important' }, 5000);
        } else {
            $("#ContactReasonTableContent tr").eq(selectedIndex).remove();
            if (selectedIndex != 0) {
                $("#ContactReasonTableContent tr").eq(selectedIndex - 1).after(data.Template);
            } else {
                $("#ContactReasonTableContent").prepend(data.Template);
            }
        }
        PopNotify("ContactReason", data.Message, "success");
        $("#ResetFormButton").click();
        UpdateEventBind();
    } else {
        PopNotify("ContactReason", data.Message, "error");
    }
};
