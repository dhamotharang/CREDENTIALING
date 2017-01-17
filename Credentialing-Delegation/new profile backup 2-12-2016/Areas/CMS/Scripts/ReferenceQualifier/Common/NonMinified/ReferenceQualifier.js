var selectedIndex = 0;

var UpdateEventBind = function () {
    $("#ReferenceQualifierTableContent").find("[name='edit']").off("click").on("click", function () {
        var button = $(this);
        selectedIndex = button.closest('tr').index();
    });
};

$(function () {
    UpdateEventBind();
});

var onSuccessReferenceQualifier = function (data) {
    if (data.Status) {
        if (data.Type == "Add") {
            $("#ReferenceQualifierTableContent").prepend(data.Template);
            //$("#ReferenceQualifierTableContent tr").first().effect('highlight', { color: 'red !important' }, 5000);
        } else {
            $("#ReferenceQualifierTableContent tr").eq(selectedIndex).remove();
            if (selectedIndex != 0) {
                $("#ReferenceQualifierTableContent tr").eq(selectedIndex - 1).after(data.Template);
            } else {
                $("#ReferenceQualifierTableContent").prepend(data.Template);
            }
        }
        PopNotify("ReferenceQualifier", data.Message, "success");
        $("#ResetFormButton").click();
        UpdateEventBind();
    } else {
        PopNotify("ReferenceQualifier", data.Message, "error");
    }
};
