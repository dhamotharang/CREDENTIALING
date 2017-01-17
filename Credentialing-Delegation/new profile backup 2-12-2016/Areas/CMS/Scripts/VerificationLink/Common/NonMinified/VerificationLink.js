var selectedIndex = 0;

var UpdateEventBind = function () {
    $("#VerificationLinkTableContent").find("[name='edit']").off("click").on("click", function () {
        var button = $(this);
        selectedIndex = button.closest('tr').index();
    });
};

$(function () {
    UpdateEventBind();
});

var onSuccessVerificationLink = function (data) {
    if (data.Status) {
        if (data.Type == "Add") {
            $("#VerificationLinkTableContent").prepend(data.Template);
            //$("#VerificationLinkTableContent tr").first().effect('highlight', { color: 'red !important' }, 5000);
        } else {
            $("#VerificationLinkTableContent tr").eq(selectedIndex).remove();
            if (selectedIndex != 0) {
                $("#VerificationLinkTableContent tr").eq(selectedIndex - 1).after(data.Template);
            } else {
                $("#VerificationLinkTableContent").prepend(data.Template);
            }
        }
        PopNotify("VerificationLink", data.Message, "success");
        $("#ResetFormButton").click();
        UpdateEventBind();
    } else {
        PopNotify("VerificationLink", data.Message, "error");
    }
};
