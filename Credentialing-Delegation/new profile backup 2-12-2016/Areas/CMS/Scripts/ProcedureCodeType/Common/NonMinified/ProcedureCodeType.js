var selectedIndex = 0;

var UpdateEventBind = function () {
    $("#ProcedureCodeTypeTableContent").find("[name='edit']").off("click").on("click", function () {
        var button = $(this);
        selectedIndex = button.closest('tr').index();
    });
};

$(function () {
    UpdateEventBind();
});

var onSuccessProcedureCodeType = function (data) {
    if (data.Status) {
        if (data.Type == "Add") {
            $("#ProcedureCodeTypeTableContent").prepend(data.Template);
            //$("#ProcedureCodeTypeTableContent tr").first().effect('highlight', { color: 'red !important' }, 5000);
        } else {
            $("#ProcedureCodeTypeTableContent tr").eq(selectedIndex).remove();
            if (selectedIndex != 0) {
                $("#ProcedureCodeTypeTableContent tr").eq(selectedIndex - 1).after(data.Template);
            } else {
                $("#ProcedureCodeTypeTableContent").prepend(data.Template);
            }
        }
        PopNotify("ProcedureCodeType", data.Message, "success");
        $("#ResetFormButton").click();
        UpdateEventBind();
    } else {
        PopNotify("ProcedureCodeType", data.Message, "error");
    }
};
