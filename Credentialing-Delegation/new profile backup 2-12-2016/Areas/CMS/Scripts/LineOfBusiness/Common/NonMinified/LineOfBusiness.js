var selectedIndex = 0;

var UpdateEventBind = function () {
    $("#LineOfBusinessTableContent").find("[name='edit']").off("click").on("click", function () {
        var button = $(this);
        selectedIndex = button.closest('tr').index();
    });
};

$(function () {
    UpdateEventBind();
});

var onSuccessLineOfBusiness = function (data) {
    if (data.Status) {
        if (data.Type == "Add") {
            $("#LineOfBusinessTableContent").prepend(data.Template);
            //$("#LineOfBusinessTableContent tr").first().effect('highlight', { color: 'red !important' }, 5000);
        } else {
            $("#LineOfBusinessTableContent tr").eq(selectedIndex).remove();
            if (selectedIndex != 0) {
                $("#LineOfBusinessTableContent tr").eq(selectedIndex - 1).after(data.Template);
            } else {
                $("#LineOfBusinessTableContent").prepend(data.Template);
            }
        }
        PopNotify("LineOfBusiness", data.Message, "success");
        $("#ResetFormButton").click();
        UpdateEventBind();
    } else {
        PopNotify("LineOfBusiness", data.Message, "error");
    }
};
