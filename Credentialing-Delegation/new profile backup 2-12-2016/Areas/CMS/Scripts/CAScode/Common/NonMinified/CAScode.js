var selectedIndex = 0;

var UpdateEventBind = function () {
    $("#CAScodeTableContent").find("[name='edit']").off("click").on("click", function () {
        var button = $(this);
        selectedIndex = button.closest('tr').index();
    });
};

$(function () {
    UpdateEventBind();
});

var onSuccessCAScode = function (data) {
    if (data.Status) {
        if (data.Type == "Add") {
            $("#CAScodeTableContent").prepend(data.Template);
            //$("#CAScodeTableContent tr").first().effect('highlight', { color: 'red !important' }, 5000);
        } else {
            $("#CAScodeTableContent tr").eq(selectedIndex).remove();
            if (selectedIndex != 0) {
                $("#CAScodeTableContent tr").eq(selectedIndex - 1).after(data.Template);
            } else {
                $("#CAScodeTableContent").prepend(data.Template);
            }
        }
        PopNotify("CAScode", data.Message, "success");
        $("#ResetFormButton").click();
        UpdateEventBind();
    } else {
        PopNotify("CAScode", data.Message, "error");
    }
};
