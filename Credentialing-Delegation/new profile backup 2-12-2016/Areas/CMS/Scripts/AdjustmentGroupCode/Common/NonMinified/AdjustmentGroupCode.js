var selectedIndex = 0;

var UpdateEventBind = function () {
    $("#AdjustmentGroupCodeTableContent").find("[name='edit']").off("click").on("click", function () {
        var button = $(this);
        selectedIndex = button.closest('tr').index();
    });
};

$(function () {
    UpdateEventBind();
});

var onSuccessAdjustmentGroupCode = function (data) {
    if (data.Status) {
        if (data.Type == "Add") {
            $("#AdjustmentGroupCodeTableContent").prepend(data.Template);
            //$("#AdjustmentGroupCodeTableContent tr").first().effect('highlight', { color: 'red !important' }, 5000);
        } else {
            $("#AdjustmentGroupCodeTableContent tr").eq(selectedIndex).remove();
            if (selectedIndex != 0) {
                $("#AdjustmentGroupCodeTableContent tr").eq(selectedIndex - 1).after(data.Template);
            } else {
                $("#AdjustmentGroupCodeTableContent").prepend(data.Template);
            }
        }
        PopNotify("AdjustmentGroupCode", data.Message, "success");
        $("#ResetFormButton").click();
        UpdateEventBind();
    } else {
        PopNotify("AdjustmentGroupCode", data.Message, "error");
    }
};
