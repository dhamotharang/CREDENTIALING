var selectedIndex = 0;

var UpdateEventBind = function () {
    $("#OutcomeTypeTableContent").find("[name='edit']").off("click").on("click", function () {
        var button = $(this);
        selectedIndex = button.closest('tr').index();
    });
};

$(function () {
    UpdateEventBind();
});

var onSuccessOutcomeType = function (data) {
    if (data.Status) {
        if (data.Type == "Add") {
            $("#OutcomeTypeTableContent").prepend(data.Template);
            //$("#OutcomeTypeTableContent tr").first().effect('highlight', { color: 'red !important' }, 5000);
        } else {
            $("#OutcomeTypeTableContent tr").eq(selectedIndex).remove();
            if (selectedIndex != 0) {
                $("#OutcomeTypeTableContent tr").eq(selectedIndex - 1).after(data.Template);
            } else {
                $("#OutcomeTypeTableContent").prepend(data.Template);
            }
        }
        PopNotify("OutcomeType", data.Message, "success");
        $("#ResetFormButton").click();
        UpdateEventBind();
    } else {
        PopNotify("OutcomeType", data.Message, "error");
    }
};
