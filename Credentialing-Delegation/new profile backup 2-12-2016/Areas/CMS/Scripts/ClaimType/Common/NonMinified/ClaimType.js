var selectedIndex = 0;

var UpdateEventBind = function () {
    $("#ClaimTypeTableContent").find("[name='edit']").off("click").on("click", function () {
        var button = $(this);
        selectedIndex = button.closest('tr').index();
    });
};

$(function () {
    UpdateEventBind();
});

var onSuccessClaimType = function (data) {
    if (data.Status) {
        if (data.Type == "Add") {
            $("#ClaimTypeTableContent").prepend(data.Template);
            //$("#ClaimTypeTableContent tr").first().effect('highlight', { color: 'red !important' }, 5000);
        } else {
            $("#ClaimTypeTableContent tr").eq(selectedIndex).remove();
            if (selectedIndex != 0) {
                $("#ClaimTypeTableContent tr").eq(selectedIndex - 1).after(data.Template);
            } else {
                $("#ClaimTypeTableContent").prepend(data.Template);
            }
        }
        PopNotify("ClaimType", data.Message, "success");
        $("#ResetFormButton").click();
        UpdateEventBind();
    } else {
        PopNotify("ClaimType", data.Message, "error");
    }
};
