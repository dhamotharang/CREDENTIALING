var selectedIndex = 0;

var UpdateEventBind = function () {
    $("#DepartmentTableContent").find("[name='edit']").off("click").on("click", function () {
        var button = $(this);
        selectedIndex = button.closest('tr').index();
    });
};

$(function () {
    UpdateEventBind();
});

var onSuccessDepartment = function (data) {
    if (data.Status) {
        if (data.Type == "Add") {
            $("#DepartmentTableContent").prepend(data.Template);
            //$("#DepartmentTableContent tr").first().effect('highlight', { color: 'red !important' }, 5000);
        } else {
            $("#DepartmentTableContent tr").eq(selectedIndex).remove();
            if (selectedIndex != 0) {
                $("#DepartmentTableContent tr").eq(selectedIndex - 1).after(data.Template);
            } else {
                $("#DepartmentTableContent").prepend(data.Template);
            }
        }
        PopNotify("Department", data.Message, "success");
        $("#ResetFormButton").click();
        UpdateEventBind();
    } else {
        PopNotify("Department", data.Message, "error");
    }
};
