var selectedIndex = 0;

var UpdateEventBind = function () {
    $("#EmploymentTypeTableContent").find("[name='edit']").off("click").on("click", function () {
        var button = $(this);
        selectedIndex = button.closest('tr').index();
    });
};

$(function () {
    UpdateEventBind();
});

var onSuccessEmploymentType = function (data) {
    if (data.Status) {
        if (data.Type == "Add") {
            $("#EmploymentTypeTableContent").prepend(data.Template);
            //$("#EmploymentTypeTableContent tr").first().effect('highlight', { color: 'red !important' }, 5000);
        } else {
            $("#EmploymentTypeTableContent tr").eq(selectedIndex).remove();
            if (selectedIndex != 0) {
                $("#EmploymentTypeTableContent tr").eq(selectedIndex - 1).after(data.Template);
            } else {
                $("#EmploymentTypeTableContent").prepend(data.Template);
            }
        }
        PopNotify("EmploymentType", data.Message, "success");
        $("#ResetFormButton").click();
        UpdateEventBind();
    } else {
        PopNotify("EmploymentType", data.Message, "error");
    }
};
