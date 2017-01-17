var selectedIndex = 0;

var UpdateEventBind = function () {
    $("#MaritalStatusTableContent").find("[name='edit']").off("click").on("click", function () {
        var button = $(this);
        selectedIndex = button.closest('tr').index();
    });
};

$(function () {
    UpdateEventBind();
});

var onSuccessMaritalStatus = function (data) {
    if (data.Status) {
        if (data.Type == "Add") {
            $("#MaritalStatusTableContent").prepend(data.Template);
            //$("#MaritalStatusTableContent tr").first().effect('highlight', { color: 'red !important' }, 5000);
        } else {
            $("#MaritalStatusTableContent tr").eq(selectedIndex).remove();
            if (selectedIndex != 0) {
                $("#MaritalStatusTableContent tr").eq(selectedIndex - 1).after(data.Template);
            } else {
                $("#MaritalStatusTableContent").prepend(data.Template);
            }
        }
        PopNotify("MaritalStatus", data.Message, "success");
        $("#ResetFormButton").click();
        UpdateEventBind();
    } else {
        PopNotify("MaritalStatus", data.Message, "error");
    }
};
