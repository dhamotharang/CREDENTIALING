var selectedIndex = 0;

var UpdateEventBind = function () {
    $("#CountryTableContent").find("[name='edit']").off("click").on("click", function () {
        var button = $(this);
        selectedIndex = button.closest('tr').index();
    });
};

$(function () {
    UpdateEventBind();
});

var onSuccessCountry = function (data) {
    if (data.Status) {
        if (data.Type == "Add") {
            $("#CountryTableContent").prepend(data.Template);
            //$("#CountryTableContent tr").first().effect('highlight', { color: 'red !important' }, 5000);
        } else {
            $("#CountryTableContent tr").eq(selectedIndex).remove();
            if (selectedIndex != 0) {
                $("#CountryTableContent tr").eq(selectedIndex - 1).after(data.Template);
            } else {
                $("#CountryTableContent").prepend(data.Template);
            }
        }
        PopNotify("Country", data.Message, "success");
        $("#ResetFormButton").click();
        UpdateEventBind();
    } else {
        PopNotify("Country", data.Message, "error");
    }
};
