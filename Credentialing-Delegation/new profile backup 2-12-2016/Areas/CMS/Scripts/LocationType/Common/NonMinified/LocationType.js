var selectedIndex = 0;

var UpdateEventBind = function () {
    $("#LocationTypeTableContent").find("[name='edit']").off("click").on("click", function () {
        var button = $(this);
        selectedIndex = button.closest('tr').index();
    });
};

$(function () {
    UpdateEventBind();
});

var onSuccessLocationType = function (data) {
    if (data.Status) {
        if (data.Type == "Add") {
            $("#LocationTypeTableContent").prepend(data.Template);
            //$("#LocationTypeTableContent tr").first().effect('highlight', { color: 'red !important' }, 5000);
        } else {
            $("#LocationTypeTableContent tr").eq(selectedIndex).remove();
            if (selectedIndex != 0) {
                $("#LocationTypeTableContent tr").eq(selectedIndex - 1).after(data.Template);
            } else {
                $("#LocationTypeTableContent").prepend(data.Template);
            }
        }
        PopNotify("LocationType", data.Message, "success");
        $("#ResetFormButton").click();
        UpdateEventBind();
    } else {
        PopNotify("LocationType", data.Message, "error");
    }
};
