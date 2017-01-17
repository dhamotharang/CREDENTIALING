var selectedIndex = 0;

var UpdateEventBind = function () {
    $("#AdmittingPrivilegeTableContent").find("[name='edit']").off("click").on("click", function () {
        var button = $(this);
        selectedIndex = button.closest('tr').index();
    });
};

$(function () {
    UpdateEventBind();
});

var onSuccessAdmittingPrivilege = function (data) {
    if (data.Status) {
        if (data.Type == "Add") {
            $("#AdmittingPrivilegeTableContent").prepend(data.Template);
            //$("#AdmittingPrivilegeTableContent tr").first().effect('highlight', { color: 'red !important' }, 5000);
        } else {
            $("#AdmittingPrivilegeTableContent tr").eq(selectedIndex).remove();
            if (selectedIndex != 0) {
                $("#AdmittingPrivilegeTableContent tr").eq(selectedIndex - 1).after(data.Template);
            } else {
                $("#AdmittingPrivilegeTableContent").prepend(data.Template);
            }
        }
        PopNotify("AdmittingPrivilege", data.Message, "success");
        $("#ResetFormButton").click();
        UpdateEventBind();
    } else {
        PopNotify("AdmittingPrivilege", data.Message, "error");
    }
};
