var selectedIndex = 0;

var UpdateEventBind = function () {
    $("#ResponsiblePersonTypeTableContent").find("[name='edit']").off("click").on("click", function () {
        var button = $(this);
        selectedIndex = button.closest('tr').index();
    });
};

$(function () {
    UpdateEventBind();
});

var onSuccessResponsiblePersonType = function (data) {
    if (data.Status) {
        if (data.Type == "Add") {
            $("#ResponsiblePersonTypeTableContent").prepend(data.Template);
            //$("#ResponsiblePersonTypeTableContent tr").first().effect('highlight', { color: 'red !important' }, 5000);
        } else {
            $("#ResponsiblePersonTypeTableContent tr").eq(selectedIndex).remove();
            if (selectedIndex != 0) {
                $("#ResponsiblePersonTypeTableContent tr").eq(selectedIndex - 1).after(data.Template);
            } else {
                $("#ResponsiblePersonTypeTableContent").prepend(data.Template);
            }
        }
        PopNotify("ResponsiblePersonType", data.Message, "success");
        $("#ResetFormButton").click();
        UpdateEventBind();
    } else {
        PopNotify("ResponsiblePersonType", data.Message, "error");
    }
};
