var selectedIndex = 0;

var UpdateEventBind = function () {
    $("#ClaimValueCodeTableContent").find("[name='edit']").off("click").on("click", function () {
        var button = $(this);
        selectedIndex = button.closest('tr').index();
    });
};

$(function () {
    UpdateEventBind();
});

var onSuccessClaimValueCode = function (data) {
    if (data.Status) {
        if (data.Type == "Add") {
            $("#ClaimValueCodeTableContent").prepend(data.Template);
            //$("#ClaimValueCodeTableContent tr").first().effect('highlight', { color: 'red !important' }, 5000);
        } else {
            $("#ClaimValueCodeTableContent tr").eq(selectedIndex).remove();
            if (selectedIndex != 0) {
                $("#ClaimValueCodeTableContent tr").eq(selectedIndex - 1).after(data.Template);
            } else {
                $("#ClaimValueCodeTableContent").prepend(data.Template);
            }
        }
        PopNotify("Claim Value Code", data.Message, "success");
        $("#ResetFormButton").click();
        UpdateEventBind();
    } else {
        PopNotify("Claim Value Code", data.Message, "error");
    }
};
