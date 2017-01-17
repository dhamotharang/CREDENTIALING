var selectedIndex = 0;

var UpdateEventBind = function () {
    $("#SalutationTableContent").find("[name='edit']").off("click").on("click", function () {
        var button = $(this);
        selectedIndex = button.closest('tr').index();
    });
};

$(function () {
    UpdateEventBind();
});

var onSuccessSalutation = function (data) {
    if (data.Status) {
        if (data.Type == "Add") {
            $("#SalutationTableContent").prepend(data.Template);
            //$("#SalutationTableContent tr").first().effect('highlight', { color: 'red !important' }, 5000);
        } else {
            $("#SalutationTableContent tr").eq(selectedIndex).remove();
            if (selectedIndex != 0) {
                $("#SalutationTableContent tr").eq(selectedIndex - 1).after(data.Template);
            } else {
                $("#SalutationTableContent").prepend(data.Template);
            }
        }
        PopNotify("Salutation", data.Message, "success");
        $("#ResetFormButton").click();
        UpdateEventBind();
    } else {
        PopNotify("Salutation", data.Message, "error");
    }
};
