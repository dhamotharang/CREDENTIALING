var selectedIndex = 0;

var UpdateEventBind = function () {
    $("#RangeTableContent").find("[name='edit']").off("click").on("click", function () {
        var button = $(this);
        selectedIndex = button.closest('tr').index();
    });
};

$(function () {
    UpdateEventBind();
});

var onSuccessRange = function (data) {
    if (data.Status) {
        if (data.Type == "Add") {
            $("#RangeTableContent").prepend(data.Template);
            //$("#RangeTableContent tr").first().effect('highlight', { color: 'red !important' }, 5000);
        } else {
            $("#RangeTableContent tr").eq(selectedIndex).remove();
            if (selectedIndex != 0) {
                $("#RangeTableContent tr").eq(selectedIndex - 1).after(data.Template);
            } else {
                $("#RangeTableContent").prepend(data.Template);
            }
        }
        PopNotify("Range", data.Message, "success");
        $("#ResetFormButton").click();
        UpdateEventBind();
    } else {
        PopNotify("Range", data.Message, "error");
    }
};
