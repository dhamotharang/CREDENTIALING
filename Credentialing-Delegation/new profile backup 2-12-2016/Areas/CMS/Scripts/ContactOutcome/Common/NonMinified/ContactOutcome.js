var selectedIndex = 0;

var UpdateEventBind = function () {
    $("#ContactOutcomeTableContent").find("[name='edit']").off("click").on("click", function () {
        var button = $(this);
        selectedIndex = button.closest('tr').index();
    });
};

$(function () {
    UpdateEventBind();
});

var onSuccessContactOutcome = function (data) {
    if (data.Status) {
        if (data.Type == "Add") {
            $("#ContactOutcomeTableContent").prepend(data.Template);
            //$("#ContactOutcomeTableContent tr").first().effect('highlight', { color: 'red !important' }, 5000);
        } else {
            $("#ContactOutcomeTableContent tr").eq(selectedIndex).remove();
            if (selectedIndex != 0) {
                $("#ContactOutcomeTableContent tr").eq(selectedIndex - 1).after(data.Template);
            } else {
                $("#ContactOutcomeTableContent").prepend(data.Template);
            }
        }
        PopNotify("ContactOutcome", data.Message, "success");
        $("#ResetFormButton").click();
        UpdateEventBind();
    } else {
        PopNotify("ContactOutcome", data.Message, "error");
    }
};
