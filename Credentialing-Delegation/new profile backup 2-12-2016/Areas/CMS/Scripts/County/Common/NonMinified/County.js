var selectedIndex = 0;

var UpdateEventBind = function () {
    $("#CountyTableContent").find("[name='edit']").off("click").on("click", function () {
        var button = $(this);
        selectedIndex = button.closest('tr').index();
    });
};

$(function () {
    UpdateEventBind();
});

var onSuccessCounty = function (data) {
    if (data.Status) {
        if (data.Type == "Add") {
            $("#CountyTableContent").prepend(data.Template);
            //$("#CountyTableContent tr").first().effect('highlight', { color: 'red !important' }, 5000);
        } else {
            $("#CountyTableContent tr").eq(selectedIndex).remove();
            if (selectedIndex != 0) {
                $("#CountyTableContent tr").eq(selectedIndex - 1).after(data.Template);
            } else {
                $("#CountyTableContent").prepend(data.Template);
            }
        }
        PopNotify("County", data.Message, "success");
        $("#ResetFormButton").click();
        UpdateEventBind();
    } else {
        PopNotify("County", data.Message, "error");
    }
};
