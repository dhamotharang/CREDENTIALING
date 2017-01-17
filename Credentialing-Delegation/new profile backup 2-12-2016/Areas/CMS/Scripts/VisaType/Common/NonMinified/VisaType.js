var selectedIndex = 0;

var UpdateEventBind = function () {
    $("#VisaTypeTableContent").find("[name='edit']").off("click").on("click", function () {
        var button = $(this);
        selectedIndex = button.closest('tr').index();
    });
};

$(function () {
    UpdateEventBind();
});

var onSuccessVisaType = function (data) {
    if (data.Status) {
        if (data.Type == "Add") {
            $("#VisaTypeTableContent").prepend(data.Template);
            //$("#VisaTypeTableContent tr").first().effect('highlight', { color: 'red !important' }, 5000);
        } else {
            $("#VisaTypeTableContent tr").eq(selectedIndex).remove();
            if (selectedIndex != 0) {
                $("#VisaTypeTableContent tr").eq(selectedIndex - 1).after(data.Template);
            } else {
                $("#VisaTypeTableContent").prepend(data.Template);
            }
        }
        PopNotify("VisaType", data.Message, "success");
        $("#ResetFormButton").click();
        UpdateEventBind();
    } else {
        PopNotify("VisaType", data.Message, "error");
    }
};
