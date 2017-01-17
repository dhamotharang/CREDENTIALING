var selectedIndex = 0;

var UpdateEventBind = function () {
    $("#NDCQualifierTableContent").find("[name='edit']").off("click").on("click", function () {
        var button = $(this);
        selectedIndex = button.closest('tr').index();
    });
};

$(function () {
    UpdateEventBind();
});

var onSuccessNDCQualifier = function (data) {
    if (data.Status) {
        if (data.Type == "Add") {
            $("#NDCQualifierTableContent").prepend(data.Template);
            //$("#NDCQualifierTableContent tr").first().effect('highlight', { color: 'red !important' }, 5000);
        } else {
            $("#NDCQualifierTableContent tr").eq(selectedIndex).remove();
            if (selectedIndex != 0) {
                $("#NDCQualifierTableContent tr").eq(selectedIndex - 1).after(data.Template);
            } else {
                $("#NDCQualifierTableContent").prepend(data.Template);
            }
        }
        PopNotify("NDCQualifier", data.Message, "success");
        $("#ResetFormButton").click();
        UpdateEventBind();
    } else {
        PopNotify("NDCQualifier", data.Message, "error");
    }
};
