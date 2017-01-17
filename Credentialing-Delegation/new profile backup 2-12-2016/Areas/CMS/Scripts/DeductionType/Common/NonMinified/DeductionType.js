var selectedIndex = 0;

var UpdateEventBind = function () {
    $("#DeductionTypeTableContent").find("[name='edit']").off("click").on("click", function () {
        var button = $(this);
        selectedIndex = button.closest('tr').index();
    });
};

$(function () {
    UpdateEventBind();
});

var onSuccessDeductionType = function (data) {
    if (data.Status) {
        if (data.Type == "Add") {
            $("#DeductionTypeTableContent").prepend(data.Template);
            //$("#DeductionTypeTableContent tr").first().effect('highlight', { color: 'red !important' }, 5000);
        } else {
            $("#DeductionTypeTableContent tr").eq(selectedIndex).remove();
            if (selectedIndex != 0) {
                $("#DeductionTypeTableContent tr").eq(selectedIndex - 1).after(data.Template);
            } else {
                $("#DeductionTypeTableContent").prepend(data.Template);
            }
        }
        PopNotify("DeductionType", data.Message, "success");
        $("#ResetFormButton").click();
        UpdateEventBind();
    } else {
        PopNotify("DeductionType", data.Message, "error");
    }
};
