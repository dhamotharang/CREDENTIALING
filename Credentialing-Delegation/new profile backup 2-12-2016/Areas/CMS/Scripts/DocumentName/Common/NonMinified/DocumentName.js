var selectedIndex = 0;

var UpdateEventBind = function () {
    $("#DocumentNameTableContent").find("[name='edit']").off("click").on("click", function () {
        var button = $(this);
        selectedIndex = button.closest('tr').index();
    });
};

$(function () {
    UpdateEventBind();
});

var onSuccessDocumentName = function (data) {
    if (data.Status) {
        if (data.Type == "Add") {
            $("#DocumentNameTableContent").prepend(data.Template);
            //$("#DocumentNameTableContent tr").first().effect('highlight', { color: 'red !important' }, 5000);
        } else {
            $("#DocumentNameTableContent tr").eq(selectedIndex).remove();
            if (selectedIndex != 0) {
                $("#DocumentNameTableContent tr").eq(selectedIndex - 1).after(data.Template);
            } else {
                $("#DocumentNameTableContent").prepend(data.Template);
            }
        }
        PopNotify("DocumentName", data.Message, "success");
        $("#ResetFormButton").click();
        UpdateEventBind();
    } else {
        PopNotify("DocumentName", data.Message, "error");
    }
};
