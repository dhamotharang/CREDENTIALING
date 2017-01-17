var selectedIndex = 0;

var UpdateEventBind = function () {
    $("#AdjustmentReasonCodeTableContent").find("[name='edit']").off("click").on("click", function () {
        var button = $(this);
        selectedIndex = button.closest('tr').index();
    });
};

$(function () {
    UpdateEventBind();
});

var onSuccessAdjustmentReasonCode = function (data) {
    if (data.Status) {
        if (data.Type == "Add") {
            $("#AdjustmentReasonCodeTableContent").prepend(data.Template);
            //$("#AdjustmentReasonCodeTableContent tr").first().effect('highlight', { color: 'red !important' }, 5000);
        } else {
            $("#AdjustmentReasonCodeTableContent tr").eq(selectedIndex).remove();
            if (selectedIndex != 0) {
                $("#AdjustmentReasonCodeTableContent tr").eq(selectedIndex - 1).after(data.Template);
            } else {
                $("#AdjustmentReasonCodeTableContent").prepend(data.Template);
            }
        }
        PopNotify("AdjustmentReasonCode", data.Message, "success");
        $("#ResetFormButton").click();
        UpdateEventBind();
    } else {
        PopNotify("AdjustmentReasonCode", data.Message, "error");
    }
};
