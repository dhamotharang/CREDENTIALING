var selectedIndex = 0;

var UpdateEventBind = function () {
    $("#AuthorizationTypeTableContent").find("[name='edit']").off("click").on("click", function () {
        var button = $(this);
        selectedIndex = button.closest('tr').index();
    });
};

$(function () {
    UpdateEventBind();
});

var onSuccessAuthorizationType = function (data) {
    if (data.Status) {
        if (data.Type == "Add") {
            $("#AuthorizationTypeTableContent").prepend(data.Template);
            //$("#AuthorizationTypeTableContent tr").first().effect('highlight', { color: 'red !important' }, 5000);
        } else {
            $("#AuthorizationTypeTableContent tr").eq(selectedIndex).remove();
            if (selectedIndex != 0) {
                $("#AuthorizationTypeTableContent tr").eq(selectedIndex - 1).after(data.Template);
            } else {
                $("#AuthorizationTypeTableContent").prepend(data.Template);
            }
        }
        PopNotify("AuthorizationType", data.Message, "success");
        $("#ResetFormButton").click();
        UpdateEventBind();
    } else {
        PopNotify("AuthorizationType", data.Message, "error");
    }
};
