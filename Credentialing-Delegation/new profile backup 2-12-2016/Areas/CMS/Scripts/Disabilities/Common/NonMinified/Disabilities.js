var selectedIndex = 0;

var UpdateEventBind = function () {
    $("#DisabilitiesTableContent").find("[name='edit']").off("click").on("click", function () {
        var button = $(this);
        selectedIndex = button.closest('tr').index();
    });
};

$(function () {
    UpdateEventBind();
});

var onSuccessDisabilities = function (data) {
    if (data.Status) {
        if (data.Type == "Add") {
            $("#DisabilitiesTableContent").prepend(data.Template);
            //$("#DisabilitiesTableContent tr").first().effect('highlight', { color: 'red !important' }, 5000);
        } else {
            $("#DisabilitiesTableContent tr").eq(selectedIndex).remove();
            if (selectedIndex != 0) {
                $("#DisabilitiesTableContent tr").eq(selectedIndex - 1).after(data.Template);
            } else {
                $("#DisabilitiesTableContent").prepend(data.Template);
            }
        }
        PopNotify("Disabilities", data.Message, "success");
        $("#ResetFormButton").click();
        UpdateEventBind();
    } else {
        PopNotify("Disabilities", data.Message, "error");
    }
};
