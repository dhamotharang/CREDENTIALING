var selectedIndex = 0;

var UpdateEventBind = function () {
    $("#RaceTableContent").find("[name='edit']").off("click").on("click", function () {
        var button = $(this);
        selectedIndex = button.closest('tr').index();
    });
};

$(function () {
    UpdateEventBind();
});

var onSuccessRace = function (data) {
    if (data.Status) {
        if (data.Type == "Add") {
            $("#RaceTableContent").prepend(data.Template);
            //$("#RaceTableContent tr").first().effect('highlight', { color: 'red !important' }, 5000);
        } else {
            $("#RaceTableContent tr").eq(selectedIndex).remove();
            if (selectedIndex != 0) {
                $("#RaceTableContent tr").eq(selectedIndex - 1).after(data.Template);
            } else {
                $("#RaceTableContent").prepend(data.Template);
            }
        }
        PopNotify("Race", data.Message, "success");
        $("#ResetFormButton").click();
        UpdateEventBind();
    } else {
        PopNotify("Race", data.Message, "error");
    }
};
