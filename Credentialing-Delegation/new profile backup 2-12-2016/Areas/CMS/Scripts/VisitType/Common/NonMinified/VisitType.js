var selectedIndex = 0;

var UpdateEventBind = function () {
    $("#VisitTypeTableContent").find("[name='edit']").off("click").on("click", function () {
        var button = $(this);
        selectedIndex = button.closest('tr').index();
    });
};

$(function () {
    UpdateEventBind();
});

var onSuccessVisitType = function (data) {
    if (data.Status) {
        if (data.Type == "Add") {
            $("#VisitTypeTableContent").prepend(data.Template);
            //$("#VisitTypeTableContent tr").first().effect('highlight', { color: 'red !important' }, 5000);
        } else {
            $("#VisitTypeTableContent tr").eq(selectedIndex).remove();
            if (selectedIndex != 0) {
                $("#VisitTypeTableContent tr").eq(selectedIndex - 1).after(data.Template);
            } else {
                $("#VisitTypeTableContent").prepend(data.Template);
            }
        }
        PopNotify("VisitType", data.Message, "success");
        $("#ResetFormButton").click();
        UpdateEventBind();
    } else {
        PopNotify("VisitType", data.Message, "error");
    }
};
