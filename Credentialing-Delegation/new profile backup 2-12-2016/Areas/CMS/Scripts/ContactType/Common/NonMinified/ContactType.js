var selectedIndex = 0;

var UpdateEventBind = function () {
    $("#ContactTypeTableContent").find("[name='edit']").off("click").on("click", function () {
        var button = $(this);
        selectedIndex = button.closest('tr').index();
    });
};

$(function () {
    UpdateEventBind();
});

var onSuccessContactType = function (data) {
    if (data.Status) {
        if (data.Type == "Add") {
            $("#ContactTypeTableContent").prepend(data.Template);
            //$("#ContactTypeTableContent tr").first().effect('highlight', { color: 'red !important' }, 5000);
        } else {
            $("#ContactTypeTableContent tr").eq(selectedIndex).remove();
            if (selectedIndex != 0) {
                $("#ContactTypeTableContent tr").eq(selectedIndex - 1).after(data.Template);
            } else {
                $("#ContactTypeTableContent").prepend(data.Template);
            }
        }
        PopNotify("ContactType", data.Message, "success");
        $("#ResetFormButton").click();
        UpdateEventBind();
    } else {
        PopNotify("ContactType", data.Message, "error");
    }
};
