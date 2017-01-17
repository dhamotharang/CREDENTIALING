var selectedIndex = 0;

var UpdateEventBind = function () {
    $("#OutcomeTableContent").find("[name='edit']").off("click").on("click", function () {
        var button = $(this);
        selectedIndex = button.closest('tr').index();
    });
};

$(function () {
    UpdateEventBind();
});

var onSuccessOutcome = function (data) {
    if (data.Status) {
        if (data.Type == "Add") {
            $("#OutcomeTableContent").prepend(data.Template);
            //$("#OutcomeTableContent tr").first().effect('highlight', { color: 'red !important' }, 5000);
        } else {
            $("#OutcomeTableContent tr").eq(selectedIndex).remove();
            if (selectedIndex != 0) {
                $("#OutcomeTableContent tr").eq(selectedIndex - 1).after(data.Template);
            } else {
                $("#OutcomeTableContent").prepend(data.Template);
            }
        }
        PopNotify("Outcome", data.Message, "success");
        $("#ResetFormButton").click();
        UpdateEventBind();
    } else {
        PopNotify("Outcome", data.Message, "error");
    }
};
