var selectedIndex = 0;

var UpdateEventBind = function () {
    $("#IncomeSourceTableContent").find("[name='edit']").off("click").on("click", function () {
        var button = $(this);
        selectedIndex = button.closest('tr').index();
    });
};

$(function () {
    UpdateEventBind();
});

var onSuccessIncomeSource = function (data) {
    if (data.Status) {
        if (data.Type == "Add") {
            $("#IncomeSourceTableContent").prepend(data.Template);
            //$("#IncomeSourceTableContent tr").first().effect('highlight', { color: 'red !important' }, 5000);
        } else {
            $("#IncomeSourceTableContent tr").eq(selectedIndex).remove();
            if (selectedIndex != 0) {
                $("#IncomeSourceTableContent tr").eq(selectedIndex - 1).after(data.Template);
            } else {
                $("#IncomeSourceTableContent").prepend(data.Template);
            }
        }
        PopNotify("IncomeSource", data.Message, "success");
        $("#ResetFormButton").click();
        UpdateEventBind();
    } else {
        PopNotify("IncomeSource", data.Message, "error");
    }
};
