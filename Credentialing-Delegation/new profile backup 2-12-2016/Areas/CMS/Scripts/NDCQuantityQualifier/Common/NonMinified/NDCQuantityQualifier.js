var selectedIndex = 0;

var UpdateEventBind = function () {
    $("#NDCQuantityQualifierTableContent").find("[name='edit']").off("click").on("click", function () {
        var button = $(this);
        selectedIndex = button.closest('tr').index();
    });
};

$(function () {
    UpdateEventBind();
});

var onSuccessNDCQuantityQualifier = function (data) {
    if (data.Status) {
        if (data.Type == "Add") {
            $("#NDCQuantityQualifierTableContent").prepend(data.Template);
            //$("#NDCQuantityQualifierTableContent tr").first().effect('highlight', { color: 'red !important' }, 5000);
        } else {
            $("#NDCQuantityQualifierTableContent tr").eq(selectedIndex).remove();
            if (selectedIndex != 0) {
                $("#NDCQuantityQualifierTableContent tr").eq(selectedIndex - 1).after(data.Template);
            } else {
                $("#NDCQuantityQualifierTableContent").prepend(data.Template);
            }
        }
        PopNotify("NDCQuantityQualifier", data.Message, "success");
        $("#ResetFormButton").click();
        UpdateEventBind();
    } else {
        PopNotify("NDCQuantityQualifier", data.Message, "error");
    }
};
