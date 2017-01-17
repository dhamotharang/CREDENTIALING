var selectedIndex = 0;

var UpdateEventBind = function () {
    $("#ContactEntityTypeTableContent").find("[name='edit']").off("click").on("click", function () {
        var button = $(this);
        selectedIndex = button.closest('tr').index();
    });
};

$(function () {
    UpdateEventBind();
});

var onSuccessContactEntityType = function (data) {
    if (data.Status) {
        if (data.Type == "Add") {
            $("#ContactEntityTypeTableContent").prepend(data.Template);
            //$("#ContactEntityTypeTableContent tr").first().effect('highlight', { color: 'red !important' }, 5000);
        } else {
            $("#ContactEntityTypeTableContent tr").eq(selectedIndex).remove();
            if (selectedIndex != 0) {
                $("#ContactEntityTypeTableContent tr").eq(selectedIndex - 1).after(data.Template);
            } else {
                $("#ContactEntityTypeTableContent").prepend(data.Template);
            }
        }
        PopNotify("ContactEntityType", data.Message, "success");
        $("#ResetFormButton").click();
        UpdateEventBind();
    } else {
        PopNotify("ContactEntityType", data.Message, "error");
    }
};
