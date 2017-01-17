var selectedIndex = 0;

var UpdateEventBind = function () {
    $("#DocumentTypeTableContent").find("[name='edit']").off("click").on("click", function () {
        var button = $(this);
        selectedIndex = button.closest('tr').index();
    });
};

$(function () {
    UpdateEventBind();
});

var onSuccessDocumentType = function (data) {
    if (data.Status) {
        if (data.Type == "Add") {
            $("#DocumentTypeTableContent").prepend(data.Template);
            //$("#DocumentTypeTableContent tr").first().effect('highlight', { color: 'red !important' }, 5000);
        } else {
            $("#DocumentTypeTableContent tr").eq(selectedIndex).remove();
            if (selectedIndex != 0) {
                $("#DocumentTypeTableContent tr").eq(selectedIndex - 1).after(data.Template);
            } else {
                $("#DocumentTypeTableContent").prepend(data.Template);
            }
        }
        PopNotify("DocumentType", data.Message, "success");
        $("#ResetFormButton").click();
        UpdateEventBind();
    } else {
        PopNotify("DocumentType", data.Message, "error");
    }
};
