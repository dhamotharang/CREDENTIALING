var selectedIndex = 0;

var UpdateEventBind = function () {
    $("#TypeOfCareTableContent").find("[name='edit']").off("click").on("click", function () {
        var button = $(this);
        selectedIndex = button.closest('tr').index();
    });
};

$(function () {
    UpdateEventBind();
});

var onSuccessTypeOfCare = function (data) {
    if (data.Status) {
        if (data.Type == "Add") {
            $("#TypeOfCareTableContent").prepend(data.Template);
            //$("#TypeOfCareTableContent tr").first().effect('highlight', { color: 'red !important' }, 5000);
        } else {
            $("#TypeOfCareTableContent tr").eq(selectedIndex).remove();
            if (selectedIndex != 0) {
                $("#TypeOfCareTableContent tr").eq(selectedIndex - 1).after(data.Template);
            } else {
                $("#TypeOfCareTableContent").prepend(data.Template);
            }
        }
        PopNotify("TypeOfCare", data.Message, "success");
        $("#ResetFormButton").click();
        UpdateEventBind();
    } else {
        PopNotify("TypeOfCare", data.Message, "error");
    }
};
