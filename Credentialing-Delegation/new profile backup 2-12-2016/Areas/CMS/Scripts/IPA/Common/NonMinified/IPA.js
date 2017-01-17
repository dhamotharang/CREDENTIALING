var selectedIndex = 0;

var UpdateEventBind = function () {
    $("#IPATableContent").find("[name='edit']").off("click").on("click", function () {
        var button = $(this);
        selectedIndex = button.closest('tr').index();
    });
};

$(function () {
    UpdateEventBind();
});

var onSuccessIPA = function (data) {
    if (data.Status) {
        if (data.Type == "Add") {
            $("#IPATableContent").prepend(data.Template);
            //$("#IPATableContent tr").first().effect('highlight', { color: 'red !important' }, 5000);
        } else {
            $("#IPATableContent tr").eq(selectedIndex).remove();
            if (selectedIndex != 0) {
                $("#IPATableContent tr").eq(selectedIndex - 1).after(data.Template);
            } else {
                $("#IPATableContent").prepend(data.Template);
            }
        }
        PopNotify("IPA", data.Message, "success");
        $("#ResetFormButton").click();
        UpdateEventBind();
    } else {
        PopNotify("IPA", data.Message, "error");
    }
};
