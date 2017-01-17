var selectedIndex = 0;

var UpdateEventBind = function () {
    $("#CPTCodeTableContent").find("[name='edit']").off("click").on("click", function () {
        var button = $(this);
        selectedIndex = button.closest('tr').index();
    });
};

$(function () {
    UpdateEventBind();
});

var onSuccessCPTCode = function (data) {
    if (data.Status) {
        if (data.Type == "Add") {
            $("#CPTCodeTableContent").prepend(data.Template);
            //$("#CPTCodeTableContent tr").first().effect('highlight', { color: 'red !important' }, 5000);
        } else {
            $("#CPTCodeTableContent tr").eq(selectedIndex).remove();
            if (selectedIndex != 0) {
                $("#CPTCodeTableContent tr").eq(selectedIndex - 1).after(data.Template);
            } else {
                $("#CPTCodeTableContent").prepend(data.Template);
            }
        }
        PopNotify("CPT Code", data.Message, "success");
        $("#ResetFormButton").click();
        UpdateEventBind();
    } else {
        PopNotify("CPT Code", data.Message, "error");
    }
};
