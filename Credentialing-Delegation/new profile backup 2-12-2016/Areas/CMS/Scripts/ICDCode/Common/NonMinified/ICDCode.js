var selectedIndex = 0;

var UpdateEventBind = function () {
    $("#ICDCodeTableContent").find("[name='edit']").off("click").on("click", function () {
        var button = $(this);
        selectedIndex = button.closest('tr').index();
    });
};

$(function () {
    UpdateEventBind();
});

var onSuccessICDCode = function (data) {
    if (data.Status) {
        if (data.Type == "Add") {
            $("#ICDCodeTableContent").prepend(data.Template);
            //$("#ICDCodeTableContent tr").first().effect('highlight', { color: 'red !important' }, 5000);
        } else {
            $("#ICDCodeTableContent tr").eq(selectedIndex).remove();
            if (selectedIndex != 0) {
                $("#ICDCodeTableContent tr").eq(selectedIndex - 1).after(data.Template);
            } else {
                $("#ICDCodeTableContent").prepend(data.Template);
            }
        }
        PopNotify("ICD Code", data.Message, "success");
        $("#ResetFormButton").click();
        UpdateEventBind();
    } else {
        PopNotify("ICD Code", data.Message, "error");
    }
};
