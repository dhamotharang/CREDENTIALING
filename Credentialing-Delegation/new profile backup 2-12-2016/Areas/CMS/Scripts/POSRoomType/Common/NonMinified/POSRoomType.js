var selectedIndex = 0;

var UpdateEventBind = function () {
    $("#POSRoomTypeTableContent").find("[name='edit']").off("click").on("click", function () {
        var button = $(this);
        selectedIndex = button.closest('tr').index();
    });
};

$(function () {
    UpdateEventBind();
});

var onSuccessPOSRoomType = function (data) {
    if (data.Status) {
        if (data.Type == "Add") {
            $("#POSRoomTypeTableContent").prepend(data.Template);
            //$("#POSRoomTypeTableContent tr").first().effect('highlight', { color: 'red !important' }, 5000);
        } else {
            $("#POSRoomTypeTableContent tr").eq(selectedIndex).remove();
            if (selectedIndex != 0) {
                $("#POSRoomTypeTableContent tr").eq(selectedIndex - 1).after(data.Template);
            } else {
                $("#POSRoomTypeTableContent").prepend(data.Template);
            }
        }
        PopNotify("POSRoomType", data.Message, "success");
        $("#ResetFormButton").click();
        UpdateEventBind();
    } else {
        PopNotify("POSRoomType", data.Message, "error");
    }
};
