var selectedIndex = 0;

var UpdateEventBind = function () {
    $("#GenderTableContent").find("[name='edit']").off("click").on("click", function () {
        var button = $(this);
        selectedIndex = button.closest('tr').index();
    });
};

$(function () {
    UpdateEventBind();
});

var onSuccessGender = function (data) {
    if (data.Status) {
        if (data.Type == "Add") {
            $("#GenderTableContent").prepend(data.Template);
            //$("#GenderTableContent tr").first().effect('highlight', { color: 'red !important' }, 5000);
        } else {
            $("#GenderTableContent tr").eq(selectedIndex).remove();
            if (selectedIndex != 0) {
                $("#GenderTableContent tr").eq(selectedIndex - 1).after(data.Template);
            } else {
                $("#GenderTableContent").prepend(data.Template);
            }
        }
        PopNotify("Gender", data.Message, "success");
        $("#ResetFormButton").click();
        UpdateEventBind();
    } else {
        PopNotify("Gender", data.Message, "error");
    }
};
