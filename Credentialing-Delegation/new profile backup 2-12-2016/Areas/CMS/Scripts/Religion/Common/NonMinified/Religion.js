var selectedIndex = 0;

var UpdateEventBind = function () {
    $("#ReligionTableContent").find("[name='edit']").off("click").on("click", function () {
        var button = $(this);
        selectedIndex = button.closest('tr').index();
    });
};

$(function () {
    UpdateEventBind();
});

var onSuccessReligion = function (data) {
    if (data.Status) {
        if (data.Type == "Add") {
            $("#ReligionTableContent").prepend(data.Template);
            //$("#ReligionTableContent tr").first().effect('highlight', { color: 'red !important' }, 5000);
        } else {
            $("#ReligionTableContent tr").eq(selectedIndex).remove();
            if (selectedIndex != 0) {
                $("#ReligionTableContent tr").eq(selectedIndex - 1).after(data.Template);
            } else {
                $("#ReligionTableContent").prepend(data.Template);
            }
        }
        PopNotify("Religion", data.Message, "success");
        $("#ResetFormButton").click();
        UpdateEventBind();
    } else {
        PopNotify("Religion", data.Message, "error");
    }
};
