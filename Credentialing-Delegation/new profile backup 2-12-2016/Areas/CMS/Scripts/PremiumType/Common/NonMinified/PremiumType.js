var selectedIndex = 0;

var UpdateEventBind = function () {
    $("#PremiumTypeTableContent").find("[name='edit']").off("click").on("click", function () {
        var button = $(this);
        selectedIndex = button.closest('tr').index();
    });
};

$(function () {
    UpdateEventBind();
});

var onSuccessPremiumType = function (data) {
    if (data.Status) {
        if (data.Type == "Add") {
            $("#PremiumTypeTableContent").prepend(data.Template);
            //$("#PremiumTypeTableContent tr").first().effect('highlight', { color: 'red !important' }, 5000);
        } else {
            $("#PremiumTypeTableContent tr").eq(selectedIndex).remove();
            if (selectedIndex != 0) {
                $("#PremiumTypeTableContent tr").eq(selectedIndex - 1).after(data.Template);
            } else {
                $("#PremiumTypeTableContent").prepend(data.Template);
            }
        }
        PopNotify("PremiumType", data.Message, "success");
        $("#ResetFormButton").click();
        UpdateEventBind();
    } else {
        PopNotify("PremiumType", data.Message, "error");
    }
};
