var selectedIndex = 0;

var UpdateEventBind = function () {
    $("#SpecialityBoardTableContent").find("[name='edit']").off("click").on("click", function () {
        var button = $(this);
        selectedIndex = button.closest('tr').index();
    });
};

$(function () {
    UpdateEventBind();
});

var onSuccessSpecialityBoard = function (data) {
    if (data.Status) {
        if (data.Type == "Add") {
            $("#SpecialityBoardTableContent").prepend(data.Template);
            //$("#SpecialityBoardTableContent tr").first().effect('highlight', { color: 'red !important' }, 5000);
        } else {
            $("#SpecialityBoardTableContent tr").eq(selectedIndex).remove();
            if (selectedIndex != 0) {
                $("#SpecialityBoardTableContent tr").eq(selectedIndex - 1).after(data.Template);
            } else {
                $("#SpecialityBoardTableContent").prepend(data.Template);
            }
        }
        PopNotify("SpecialityBoard", data.Message, "success");
        $("#ResetFormButton").click();
        UpdateEventBind();
    } else {
        PopNotify("SpecialityBoard", data.Message, "error");
    }
};
