var selectedIndex = 0;

var UpdateEventBind = function () {
    $("#ComplaintCategoryTableContent").find("[name='edit']").off("click").on("click", function () {
        var button = $(this);
        selectedIndex = button.closest('tr').index();
    });
};

$(function () {
    UpdateEventBind();
});

var onSuccessComplaintCategory = function (data) {
    if (data.Status) {
        if (data.Type == "Add") {
            $("#ComplaintCategoryTableContent").prepend(data.Template);
            //$("#ComplaintCategoryTableContent tr").first().effect('highlight', { color: 'red !important' }, 5000);
        } else {
            $("#ComplaintCategoryTableContent tr").eq(selectedIndex).remove();
            if (selectedIndex != 0) {
                $("#ComplaintCategoryTableContent tr").eq(selectedIndex - 1).after(data.Template);
            } else {
                $("#ComplaintCategoryTableContent").prepend(data.Template);
            }
        }
        PopNotify("ComplaintCategory", data.Message, "success");
        $("#ResetFormButton").click();
        UpdateEventBind();
    } else {
        PopNotify("ComplaintCategory", data.Message, "error");
    }
};
