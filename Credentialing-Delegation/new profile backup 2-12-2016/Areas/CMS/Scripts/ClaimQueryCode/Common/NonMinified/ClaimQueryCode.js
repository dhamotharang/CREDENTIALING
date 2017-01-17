var selectedIndex = 0;

var UpdateEventBind = function () {
    $("#ClaimQueryCodeTableContent").find("[name='edit']").off("click").on("click", function () {
        var button = $(this);
        selectedIndex = button.closest('tr').index();
    });
};

$(function () {
    UpdateEventBind();
});

var onSuccessClaimQueryCode = function (data) {
    if (data.Status) {
        if (data.Type == "Add") {
            $("#ClaimQueryCodeTableContent").prepend(data.Template);
            //$("#ClaimQueryCodeTableContent tr").first().effect('highlight', { color: 'red !important' }, 5000);
        } else {
            $("#ClaimQueryCodeTableContent tr").eq(selectedIndex).remove();
            if (selectedIndex != 0) {
                $("#ClaimQueryCodeTableContent tr").eq(selectedIndex - 1).after(data.Template);
            } else {
                $("#ClaimQueryCodeTableContent").prepend(data.Template);
            }
        }
        PopNotify("Claim Query Code", data.Message, "success");
        $("#ResetFormButton").click();
        UpdateEventBind();
    } else {
        PopNotify("Claim Query Code", data.Message, "error");
    }
};
