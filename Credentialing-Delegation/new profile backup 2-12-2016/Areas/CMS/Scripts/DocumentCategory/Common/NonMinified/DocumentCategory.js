var selectedIndex = 0;

var UpdateEventBind = function () {
    $("#DocumentCategoryTableContent").find("[name='edit']").off("click").on("click", function () {
        var button = $(this);
        selectedIndex = button.closest('tr').index();
    });
};

$(function () {
    UpdateEventBind();
});

var onSuccessDocumentCategory = function (data) {
    if (data.Status) {
        if (data.Type == "Add") {
            $("#DocumentCategoryTableContent").prepend(data.Template);
            //$("#DocumentCategoryTableContent tr").first().effect('highlight', { color: 'red !important' }, 5000);
        } else {
            $("#DocumentCategoryTableContent tr").eq(selectedIndex).remove();
            if (selectedIndex != 0) {
                $("#DocumentCategoryTableContent tr").eq(selectedIndex - 1).after(data.Template);
            } else {
                $("#DocumentCategoryTableContent").prepend(data.Template);
            }
        }
        PopNotify("DocumentCategory", data.Message, "success");
        $("#ResetFormButton").click();
        UpdateEventBind();
    } else {
        PopNotify("DocumentCategory", data.Message, "error");
    }
};
