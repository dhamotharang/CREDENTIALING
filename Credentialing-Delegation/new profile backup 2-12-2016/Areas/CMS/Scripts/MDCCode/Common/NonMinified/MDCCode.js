var selectedIndex = 0;

var UpdateEventBind = function () {
    $("#MDCCodeTableContent").find("[name='edit']").off("click").on("click", function () {
        var button = $(this);
        selectedIndex = button.closest('tr').index();
    });
};

$(function () {
    UpdateEventBind();
});

var onSuccessMDCCode = function (data) {
    if (data.Status) {
        if (data.Type == "Add") {
            $("#MDCCodeTableContent").prepend(data.Template);
            //$("#MDCCodeTableContent tr").first().effect('highlight', { color: 'red !important' }, 5000);
        } else {
            $("#MDCCodeTableContent tr").eq(selectedIndex).remove();
            if (selectedIndex != 0) {
                $("#MDCCodeTableContent tr").eq(selectedIndex - 1).after(data.Template);
            } else {
                $("#MDCCodeTableContent").prepend(data.Template);
            }
        }
        PopNotify("MDC Code", data.Message, "success");
        $("#ResetFormButton").click();
        UpdateEventBind();
    } else {
        PopNotify("MDC Code", data.Message, "error");
    }
};
