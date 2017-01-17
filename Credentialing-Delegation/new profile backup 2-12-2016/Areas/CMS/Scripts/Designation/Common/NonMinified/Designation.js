var selectedIndex = 0;

var UpdateEventBind = function () {
    $("#DesignationTableContent").find("[name='edit']").off("click").on("click", function () {
        var button = $(this);
        selectedIndex = button.closest('tr').index();
    });
};

$(function () {
    UpdateEventBind();
});

var onSuccessDesignation = function (data) {
    if (data.Status) {
        if (data.Type == "Add") {
            $("#DesignationTableContent").prepend(data.Template);
            //$("#DesignationTableContent tr").first().effect('highlight', { color: 'red !important' }, 5000);
        } else {
            $("#DesignationTableContent tr").eq(selectedIndex).remove();
            if (selectedIndex != 0) {
                $("#DesignationTableContent tr").eq(selectedIndex - 1).after(data.Template);
            } else {
                $("#DesignationTableContent").prepend(data.Template);
            }
        }
        PopNotify("Designation", data.Message, "success");
        $("#ResetFormButton").click();
        UpdateEventBind();
    } else {
        PopNotify("Designation", data.Message, "error");
    }
};
