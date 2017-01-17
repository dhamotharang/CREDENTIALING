var selectedIndex = 0;

var UpdateEventBind = function () {
    $("#ClaimFormStatusTableContent").find("[name='edit']").off("click").on("click", function () {
        var button = $(this);
        selectedIndex = button.closest('tr').index();
    });
};

$(function () {
    UpdateEventBind();
});

var onSuccessClaimFormStatus = function (data) {
    if (data.Status) {
        if (data.Type == "Add") {
            $("#ClaimFormStatusTableContent").prepend(data.Template);
            //$("#ClaimFormStatusTableContent tr").first().effect('highlight', { color: 'red !important' }, 5000);
        } else {
            $("#ClaimFormStatusTableContent tr").eq(selectedIndex).remove();
            if (selectedIndex != 0) {
                $("#ClaimFormStatusTableContent tr").eq(selectedIndex - 1).after(data.Template);
            } else {
                $("#ClaimFormStatusTableContent").prepend(data.Template);
            }
        }
        PopNotify("ClaimFormStatus", data.Message, "success");
        $("#ResetFormButton").click();
        UpdateEventBind();
    } else {
        PopNotify("ClaimFormStatus", data.Message, "error");
    }
};
