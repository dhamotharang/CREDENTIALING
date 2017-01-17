var selectedIndex = 0;

var UpdateEventBind = function () {
    $("#PBPTableContent").find("[name='edit']").off("click").on("click", function () {
        var button = $(this);
        selectedIndex = button.closest('tr').index();
    });
};

$(function () {
    UpdateEventBind();
});

var onSuccessPBP = function (data) {
    if (data.Status) {
        if (data.Type == "Add") {
            $("#PBPTableContent").prepend(data.Template);
            //$("#PBPTableContent tr").first().effect('highlight', { color: 'red !important' }, 5000);
        } else {
            $("#PBPTableContent tr").eq(selectedIndex).remove();
            if (selectedIndex != 0) {
                $("#PBPTableContent tr").eq(selectedIndex - 1).after(data.Template);
            } else {
                $("#PBPTableContent").prepend(data.Template);
            }
        }
        PopNotify("PBP", data.Message, "success");
        $("#ResetFormButton").click();
        UpdateEventBind();
    } else {
        PopNotify("PBP", data.Message, "error");
    }
};
