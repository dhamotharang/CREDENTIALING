var selectedIndex = 0;

var UpdateEventBind = function () {
    $("#PlanCategoryTableContent").find("[name='edit']").off("click").on("click", function () {
        var button = $(this);
        selectedIndex = button.closest('tr').index();
    });
};

$(function () {
    UpdateEventBind();
});

var onSuccessPlanCategory = function (data) {
    if (data.Status) {
        if (data.Type == "Add") {
            $("#PlanCategoryTableContent").prepend(data.Template);
            //$("#PlanCategoryTableContent tr").first().effect('highlight', { color: 'red !important' }, 5000);
        } else {
            $("#PlanCategoryTableContent tr").eq(selectedIndex).remove();
            if (selectedIndex != 0) {
                $("#PlanCategoryTableContent tr").eq(selectedIndex - 1).after(data.Template);
            } else {
                $("#PlanCategoryTableContent").prepend(data.Template);
            }
        }
        PopNotify("PlanCategory", data.Message, "success");
        $("#ResetFormButton").click();
        UpdateEventBind();
    } else {
        PopNotify("PlanCategory", data.Message, "error");
    }
};
