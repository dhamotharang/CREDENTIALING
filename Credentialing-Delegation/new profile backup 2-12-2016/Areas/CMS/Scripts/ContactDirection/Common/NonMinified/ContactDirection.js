var selectedIndex = 0;

var UpdateEventBind = function () {
    $("#ContactDirectionTableContent").find("[name='edit']").off("click").on("click", function () {
        var button = $(this);
        selectedIndex = button.closest('tr').index();
    });
};

$(function () {
    UpdateEventBind();
});

var onSuccessContactDirection = function (data) {
    if (data.Status) {
        if (data.Type == "Add") {
            $("#ContactDirectionTableContent").prepend(data.Template);
            //$("#ContactDirectionTableContent tr").first().effect('highlight', { color: 'red !important' }, 5000);
        } else {
            $("#ContactDirectionTableContent tr").eq(selectedIndex).remove();
            if (selectedIndex != 0) {
                $("#ContactDirectionTableContent tr").eq(selectedIndex - 1).after(data.Template);
            } else {
                $("#ContactDirectionTableContent").prepend(data.Template);
            }
        }
        PopNotify("ContactDirection", data.Message, "success");
        $("#ResetFormButton").click();
        UpdateEventBind();
    } else {
        PopNotify("ContactDirection", data.Message, "error");
    }
};
