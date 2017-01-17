var selectedIndex = 0;

var UpdateEventBind = function () {
    $("#GroupTableContent").find("[name='edit']").off("click").on("click", function () {
        var button = $(this);
        selectedIndex = button.closest('tr').index();
    });
};

$(function () {
    UpdateEventBind();
});

var onSuccessGroup = function (data) {
    if (data.Status) {
        if (data.Type == "Add") {
            $("#GroupTableContent").prepend(data.Template);
            //$("#GroupTableContent tr").first().effect('highlight', { color: 'red !important' }, 5000);
        } else {
            $("#GroupTableContent tr").eq(selectedIndex).remove();
            if (selectedIndex != 0) {
                $("#GroupTableContent tr").eq(selectedIndex - 1).after(data.Template);
            } else {
                $("#GroupTableContent").prepend(data.Template);
            }
        }
        PopNotify("Group", data.Message, "success");
        $("#ResetFormButton").click();
        UpdateEventBind();
    } else {
        PopNotify("Group", data.Message, "error");
    }
};
