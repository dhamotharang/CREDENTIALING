var selectedIndex = 0;

var UpdateEventBind = function () {
    $("#IdentificationTypeTableContent").find("[name='edit']").off("click").on("click", function () {
        var button = $(this);
        selectedIndex = button.closest('tr').index();
    });
};

$(function () {
    UpdateEventBind();
});

var onSuccessIdentificationType = function (data) {
    if (data.Status) {
        if (data.Type == "Add") {
            $("#IdentificationTypeTableContent").prepend(data.Template);
            //$("#IdentificationTypeTableContent tr").first().effect('highlight', { color: 'red !important' }, 5000);
        } else {
            $("#IdentificationTypeTableContent tr").eq(selectedIndex).remove();
            if (selectedIndex != 0) {
                $("#IdentificationTypeTableContent tr").eq(selectedIndex - 1).after(data.Template);
            } else {
                $("#IdentificationTypeTableContent").prepend(data.Template);
            }
        }
        PopNotify("IdentificationType", data.Message, "success");
        $("#ResetFormButton").click();
        UpdateEventBind();
    } else {
        PopNotify("IdentificationType", data.Message, "error");
    }
};
