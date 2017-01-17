var selectedIndex = 0;

var UpdateEventBind = function () {
    $("#ReviewTypeTableContent").find("[name='edit']").off("click").on("click", function () {
        var button = $(this);
        selectedIndex = button.closest('tr').index();
    });
};

$(function () {
    UpdateEventBind();
});

var onSuccessReviewType = function (data) {
    if (data.Status) {
        if (data.Type == "Add") {
            $("#ReviewTypeTableContent").prepend(data.Template);
            //$("#ReviewTypeTableContent tr").first().effect('highlight', { color: 'red !important' }, 5000);
        } else {
            $("#ReviewTypeTableContent tr").eq(selectedIndex).remove();
            if (selectedIndex != 0) {
                $("#ReviewTypeTableContent tr").eq(selectedIndex - 1).after(data.Template);
            } else {
                $("#ReviewTypeTableContent").prepend(data.Template);
            }
        }
        PopNotify("ReviewType", data.Message, "success");
        $("#ResetFormButton").click();
        UpdateEventBind();
    } else {
        PopNotify("ReviewType", data.Message, "error");
    }
};
