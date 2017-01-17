var selectedIndex = 0;

var UpdateEventBind = function () {
    $("#DateQualifierTableContent").find("[name='edit']").off("click").on("click", function () {
        var button = $(this);
        selectedIndex = button.closest('tr').index();
    });
};

$(function () {
    UpdateEventBind();
});

var onSuccessDateQualifier = function (data) {
    if (data.Status) {
        if (data.Type == "Add") {
            $("#DateQualifierTableContent").prepend(data.Template);
            //$("#DateQualifierTableContent tr").first().effect('highlight', { color: 'red !important' }, 5000);
        } else {
            $("#DateQualifierTableContent tr").eq(selectedIndex).remove();
            if (selectedIndex != 0) {
                $("#DateQualifierTableContent tr").eq(selectedIndex - 1).after(data.Template);
            } else {
                $("#DateQualifierTableContent").prepend(data.Template);
            }
        }
        PopNotify("DateQualifier", data.Message, "success");
        $("#ResetFormButton").click();
        UpdateEventBind();
    } else {
        PopNotify("DateQualifier", data.Message, "error");
    }
};
