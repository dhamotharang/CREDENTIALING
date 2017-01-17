var selectedIndex = 0;

var UpdateEventBind = function () {
    $("#ReasonTypeTableContent").find("[name='edit']").off("click").on("click", function () {
        var button = $(this);
        selectedIndex = button.closest('tr').index();
    });
};

$(function () {
    UpdateEventBind();
});

var onSuccessReasonType = function (data) {
    if (data.Status) {
        if (data.Type == "Add") {
            $("#ReasonTypeTableContent").prepend(data.Template);
            //$("#ReasonTypeTableContent tr").first().effect('highlight', { color: 'red !important' }, 5000);
        } else {
            $("#ReasonTypeTableContent tr").eq(selectedIndex).remove();
            if (selectedIndex != 0) {
                $("#ReasonTypeTableContent tr").eq(selectedIndex - 1).after(data.Template);
            } else {
                $("#ReasonTypeTableContent").prepend(data.Template);
            }
        }
        PopNotify("ReasonType", data.Message, "success");
        $("#ResetFormButton").click();
        UpdateEventBind();
    } else {
        PopNotify("ReasonType", data.Message, "error");
    }
};
