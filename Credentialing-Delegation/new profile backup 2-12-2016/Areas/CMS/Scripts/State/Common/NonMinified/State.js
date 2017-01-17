var selectedIndex = 0;

var UpdateEventBind = function () {
    $("#StateTableContent").find("[name='edit']").off("click").on("click", function () {
        var button = $(this);
        selectedIndex = button.closest('tr').index();
    });
};

$(function () {
    UpdateEventBind();
});

var onSuccessState = function (data) {
    if (data.Status) {
        if (data.Type == "Add") {
            $("#StateTableContent").prepend(data.Template);
            //$("#StateTableContent tr").first().effect('highlight', { color: 'red !important' }, 5000);
        } else {
            $("#StateTableContent tr").eq(selectedIndex).remove();
            if (selectedIndex != 0) {
                $("#StateTableContent tr").eq(selectedIndex - 1).after(data.Template);
            } else {
                $("#StateTableContent").prepend(data.Template);
            }
        }
        PopNotify("State", data.Message, "success");
        $("#ResetFormButton").click();
        UpdateEventBind();
    } else {
        PopNotify("State", data.Message, "error");
    }
};
