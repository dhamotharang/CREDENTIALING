var selectedIndex = 0;

var UpdateEventBind = function () {
    $("#ClaimRelatedConditionCodeTableContent").find("[name='edit']").off("click").on("click", function () {
        var button = $(this);
        selectedIndex = button.closest('tr').index();
    });
};

$(function () {
    UpdateEventBind();
});

var onSuccessClaimRelatedConditionCode = function (data) {
    if (data.Status) {
        if (data.Type == "Add") {
            $("#ClaimRelatedConditionCodeTableContent").prepend(data.Template);
            //$("#ClaimRelatedConditionCodeTableContent tr").first().effect('highlight', { color: 'red !important' }, 5000);
        } else {
            $("#ClaimRelatedConditionCodeTableContent tr").eq(selectedIndex).remove();
            if (selectedIndex != 0) {
                $("#ClaimRelatedConditionCodeTableContent tr").eq(selectedIndex - 1).after(data.Template);
            } else {
                $("#ClaimRelatedConditionCodeTableContent").prepend(data.Template);
            }
        }
        PopNotify("Claim Related Condition Code", data.Message, "success");
        $("#ResetFormButton").click();
        UpdateEventBind();
    } else {
        PopNotify("Claim Related Condition Code", data.Message, "error");
    }
};
