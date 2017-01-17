var selectedIndex = 0;

var UpdateEventBind = function () {
    $("#SourcesTableContent").find("[name='edit']").off("click").on("click", function () {
        var button = $(this);
        selectedIndex = button.closest('tr').index();
    });
};

$(function () {
    UpdateEventBind();
});

var onSuccessSources = function (data) {
    if (data.Status) {
        if (data.Type == "Add") {
            $("#SourcesTableContent").prepend(data.Template);
            //$("#SourcesTableContent tr").first().effect('highlight', { color: 'red !important' }, 5000);
        } else {
            $("#SourcesTableContent tr").eq(selectedIndex).remove();
            if (selectedIndex != 0) {
                $("#SourcesTableContent tr").eq(selectedIndex - 1).after(data.Template);
            } else {
                $("#SourcesTableContent").prepend(data.Template);
            }
        }
        PopNotify("Sources", data.Message, "success");
        $("#ResetFormButton").click();
        UpdateEventBind();
    } else {
        PopNotify("Sources", data.Message, "error");
    }
};
