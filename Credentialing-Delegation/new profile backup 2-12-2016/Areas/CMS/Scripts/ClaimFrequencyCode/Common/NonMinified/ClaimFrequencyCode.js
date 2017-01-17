var selectedIndex = 0;

var UpdateEventBind = function () {
    $("#ClaimFrequencyCodeTableContent").find("[name='edit']").off("click").on("click", function () {
        var button = $(this);
        selectedIndex = button.closest('tr').index();
    });
};

$(function () {
    UpdateEventBind();
});

var onSuccessClaimFrequencyCode = function (data) {
    if (data.Status) {
        if (data.Type == "Add") {
            $("#ClaimFrequencyCodeTableContent").prepend(data.Template);
            //$("#ClaimFrequencyCodeTableContent tr").first().effect('highlight', { color: 'red !important' }, 5000);
        } else {
            $("#ClaimFrequencyCodeTableContent tr").eq(selectedIndex).remove();
            if (selectedIndex != 0) {
                $("#ClaimFrequencyCodeTableContent tr").eq(selectedIndex - 1).after(data.Template);
            } else {
                $("#ClaimFrequencyCodeTableContent").prepend(data.Template);
            }
        }
        PopNotify("Claim Frequency Code", data.Message, "success");
        $("#ResetFormButton").click();
        UpdateEventBind();
    } else {
        PopNotify("Claim Frequency Code", data.Message, "error");
    }
};
