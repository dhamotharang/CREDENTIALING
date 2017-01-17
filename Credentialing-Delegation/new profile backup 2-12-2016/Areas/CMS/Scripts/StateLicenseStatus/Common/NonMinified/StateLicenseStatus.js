var selectedIndex = 0;

var UpdateEventBind = function () {
    $("#StateLicenseStatusTableContent").find("[name='edit']").off("click").on("click", function () {
        var button = $(this);
        selectedIndex = button.closest('tr').index();
    });
};

$(function () {
    UpdateEventBind();
});

var onSuccessStateLicenseStatus = function (data) {
    if (data.Status) {
        if (data.Type == "Add") {
            $("#StateLicenseStatusTableContent").prepend(data.Template);
            //$("#StateLicenseStatusTableContent tr").first().effect('highlight', { color: 'red !important' }, 5000);
        } else {
            $("#StateLicenseStatusTableContent tr").eq(selectedIndex).remove();
            if (selectedIndex != 0) {
                $("#StateLicenseStatusTableContent tr").eq(selectedIndex - 1).after(data.Template);
            } else {
                $("#StateLicenseStatusTableContent").prepend(data.Template);
            }
        }
        PopNotify("StateLicenseStatus", data.Message, "success");
        $("#ResetFormButton").click();
        UpdateEventBind();
    } else {
        PopNotify("StateLicenseStatus", data.Message, "error");
    }
};
