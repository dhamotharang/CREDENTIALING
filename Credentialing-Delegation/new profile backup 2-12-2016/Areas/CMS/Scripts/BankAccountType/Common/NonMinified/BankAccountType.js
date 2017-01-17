var selectedIndex = 0;

var UpdateEventBind = function () {
    $("#BankAccountTypeTableContent").find("[name='edit']").off("click").on("click", function () {
        var button = $(this);
        selectedIndex = button.closest('tr').index();
    });
};

$(function () {
    UpdateEventBind();
});

var onSuccessBankAccountType = function (data) {
    if (data.Status) {
        if (data.Type == "Add") {
            $("#BankAccountTypeTableContent").prepend(data.Template);
            //$("#BankAccountTypeTableContent tr").first().effect('highlight', { color: 'red !important' }, 5000);
        } else {
            $("#BankAccountTypeTableContent tr").eq(selectedIndex).remove();
            if (selectedIndex != 0) {
                $("#BankAccountTypeTableContent tr").eq(selectedIndex - 1).after(data.Template);
            } else {
                $("#BankAccountTypeTableContent").prepend(data.Template);
            }
        }
        PopNotify("BankAccountType", data.Message, "success");
        $("#ResetFormButton").click();
        UpdateEventBind();
    } else {
        PopNotify("BankAccountType", data.Message, "error");
    }
};
