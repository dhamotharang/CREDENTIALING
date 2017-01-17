var selectedIndex = 0;

var UpdateEventBind = function () {
    $("#LevelOfCareTableContent").find("[name='edit']").off("click").on("click", function () {
        var button = $(this);
        selectedIndex = button.closest('tr').index();
    });
};

$(function () {
    UpdateEventBind();
});

var onSuccessLevelOfCare = function (data) {
    if (data.Status) {
        if (data.Type == "Add") {
            $("#LevelOfCareTableContent").prepend(data.Template);
            //$("#LevelOfCareTableContent tr").first().effect('highlight', { color: 'red !important' }, 5000);
        } else {
            $("#LevelOfCareTableContent tr").eq(selectedIndex).remove();
            if (selectedIndex != 0) {
                $("#LevelOfCareTableContent tr").eq(selectedIndex - 1).after(data.Template);
            } else {
                $("#LevelOfCareTableContent").prepend(data.Template);
            }
        }
        PopNotify("LevelOfCare", data.Message, "success");
        $("#ResetFormButton").click();
        UpdateEventBind();
    } else {
        PopNotify("LevelOfCare", data.Message, "error");
    }
};
