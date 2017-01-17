var selectedIndex = 0;

var UpdateEventBind = function () {
    $("#RoomTypeTableContent").find("[name='edit']").off("click").on("click", function () {
        var button = $(this);
        selectedIndex = button.closest('tr').index();
    });
};

$(function () {
    UpdateEventBind();
});

var onSuccessRoomType = function (data) {
    if (data.Status) {
        if (data.Type == "Add") {
            $("#RoomTypeTableContent").prepend(data.Template);
            //$("#RoomTypeTableContent tr").first().effect('highlight', { color: 'red !important' }, 5000);
        } else {
            $("#RoomTypeTableContent tr").eq(selectedIndex).remove();
            if (selectedIndex != 0) {
                $("#RoomTypeTableContent tr").eq(selectedIndex - 1).after(data.Template);
            } else {
                $("#RoomTypeTableContent").prepend(data.Template);
            }
        }
        PopNotify("RoomType", data.Message, "success");
        $("#ResetFormButton").click();
        UpdateEventBind();
    } else {
        PopNotify("RoomType", data.Message, "error");
    }
};
