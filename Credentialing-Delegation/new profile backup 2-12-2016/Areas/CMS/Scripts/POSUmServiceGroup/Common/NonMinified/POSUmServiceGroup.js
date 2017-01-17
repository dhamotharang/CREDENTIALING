var selectedIndex = 0;

var UpdateEventBind = function () {
    $("#POSUmServiceGroupTableContent").find("[name='edit']").off("click").on("click", function () {
        var button = $(this);
        selectedIndex = button.closest('tr').index();
    });
};

$(function () {
    UpdateEventBind();
});

var onSuccessPOSUmServiceGroup = function (data) {
    if (data.Status) {
        if (data.Type == "Add") {
            $("#POSUmServiceGroupTableContent").prepend(data.Template);
            //$("#POSUmServiceGroupTableContent tr").first().effect('highlight', { color: 'red !important' }, 5000);
        } else {
            $("#POSUmServiceGroupTableContent tr").eq(selectedIndex).remove();
            if (selectedIndex != 0) {
                $("#POSUmServiceGroupTableContent tr").eq(selectedIndex - 1).after(data.Template);
            } else {
                $("#POSUmServiceGroupTableContent").prepend(data.Template);
            }
        }
        PopNotify("POSUmServiceGroup", data.Message, "success");
        $("#ResetFormButton").click();
        UpdateEventBind();
    } else {
        PopNotify("POSUmServiceGroup", data.Message, "error");
    }
};
