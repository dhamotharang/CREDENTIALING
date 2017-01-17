var selectedIndex = 0;

var UpdateEventBind = function () {
    $("#ClaimStatusTableContent").find("[name='edit']").off("click").on("click", function () {
        var button = $(this);
        selectedIndex = button.closest('tr').index();
    });
};

$(function () {
    UpdateEventBind();
});

var onSuccessClaimStatus = function (data) {
    if (data.Status) {
        if (data.Type == "Add") {
            $("#ClaimStatusTableContent").prepend(data.Template);
            //$("#ClaimStatusTableContent tr").first().effect('highlight', { color: 'red !important' }, 5000);
        } else {
            $("#ClaimStatusTableContent tr").eq(selectedIndex).remove();
            if (selectedIndex != 0) {
                $("#ClaimStatusTableContent tr").eq(selectedIndex - 1).after(data.Template);
            } else {
                $("#ClaimStatusTableContent").prepend(data.Template);
            }
        }
        PopNotify("ClaimStatus", data.Message, "success");
        $("#ResetFormButton").click();
        UpdateEventBind();
    } else {
        PopNotify("ClaimStatus", data.Message, "error");
    }
};
