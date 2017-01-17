var selectedIndex = 0;

var UpdateEventBind = function () {
    $("#CityTableContent").find("[name='edit']").off("click").on("click", function () {
        var button = $(this);
        selectedIndex = button.closest('tr').index();
    });
};

$(function () {
    UpdateEventBind();
});

var onSuccessCity = function (data) {
    if (data.Status) {
        if (data.Type == "Add") {
            $("#CityTableContent").prepend(data.Template);
            //$("#CityTableContent tr").first().effect('highlight', { color: 'red !important' }, 5000);
        } else {
            $("#CityTableContent tr").eq(selectedIndex).remove();
            if (selectedIndex != 0) {
                $("#CityTableContent tr").eq(selectedIndex - 1).after(data.Template);
            } else {
                $("#CityTableContent").prepend(data.Template);
            }
        }
        PopNotify("City", data.Message, "success");
        $("#ResetFormButton").click();
        UpdateEventBind();
    } else {
        PopNotify("City", data.Message, "error");
    }
};
