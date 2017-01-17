var selectedIndex = 0;

var UpdateEventBind = function () {
    $("#StaffCategoryTableContent").find("[name='edit']").off("click").on("click", function () {
        var button = $(this);
        selectedIndex = button.closest('tr').index();
    });
};

$(function () {
    UpdateEventBind();
});

var onSuccessStaffCategory = function (data) {
    if (data.Status) {
        if (data.Type == "Add") {
            $("#StaffCategoryTableContent").prepend(data.Template);
            //$("#StaffCategoryTableContent tr").first().effect('highlight', { color: 'red !important' }, 5000);
        } else {
            $("#StaffCategoryTableContent tr").eq(selectedIndex).remove();
            if (selectedIndex != 0) {
                $("#StaffCategoryTableContent tr").eq(selectedIndex - 1).after(data.Template);
            } else {
                $("#StaffCategoryTableContent").prepend(data.Template);
            }
        }
        PopNotify("StaffCategory", data.Message, "success");
        $("#ResetFormButton").click();
        UpdateEventBind();
    } else {
        PopNotify("StaffCategory", data.Message, "error");
    }
};
