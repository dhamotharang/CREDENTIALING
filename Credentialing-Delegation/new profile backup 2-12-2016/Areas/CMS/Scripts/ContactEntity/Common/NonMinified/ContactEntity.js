var selectedIndex = 0;

var UpdateEventBind = function () {
    $("#ContactEntityTableContent").find("[name='edit']").off("click").on("click", function () {
        var button = $(this);
        selectedIndex = button.closest('tr').index();
    });
};

$(function () {
    UpdateEventBind();
});

var onSuccessContactEntity = function (data) {
    if (data.Status) {
        if (data.Type == "Add") {
            $("#ContactEntityTableContent").prepend(data.Template);
            //$("#ContactEntityTableContent tr").first().effect('highlight', { color: 'red !important' }, 5000);
        } else {
            $("#ContactEntityTableContent tr").eq(selectedIndex).remove();
            if (selectedIndex != 0) {
                $("#ContactEntityTableContent tr").eq(selectedIndex - 1).after(data.Template);
            } else {
                $("#ContactEntityTableContent").prepend(data.Template);
            }
        }
        PopNotify("ContactEntity", data.Message, "success");
        $("#ResetFormButton").click();
        UpdateEventBind();
    } else {
        PopNotify("ContactEntity", data.Message, "error");
    }
};
