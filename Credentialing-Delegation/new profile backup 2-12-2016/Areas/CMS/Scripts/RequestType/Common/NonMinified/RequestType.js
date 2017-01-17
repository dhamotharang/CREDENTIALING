var selectedIndex = 0;

var UpdateEventBind = function () {
    $("#RequestTypeTableContent").find("[name='edit']").off("click").on("click", function () {
        var button = $(this);
        selectedIndex = button.closest('tr').index();
    });
};

$(function () {
    UpdateEventBind();
});

var onSuccessRequestType = function (data) {
    if (data.Status) {
        if (data.Type == "Add") {
            $("#RequestTypeTableContent").prepend(data.Template);
            //$("#RequestTypeTableContent tr").first().effect('highlight', { color: 'red !important' }, 5000);
        } else {
            $("#RequestTypeTableContent tr").eq(selectedIndex).remove();
            if (selectedIndex != 0) {
                $("#RequestTypeTableContent tr").eq(selectedIndex - 1).after(data.Template);
            } else {
                $("#RequestTypeTableContent").prepend(data.Template);
            }
        }
        PopNotify("RequestType", data.Message, "success");
        $("#ResetFormButton").click();
        UpdateEventBind();
    } else {
        PopNotify("RequestType", data.Message, "error");
    }
};
