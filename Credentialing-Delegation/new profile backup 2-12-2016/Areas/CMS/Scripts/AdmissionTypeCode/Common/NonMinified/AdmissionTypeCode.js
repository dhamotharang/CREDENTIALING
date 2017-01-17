var selectedIndex = 0;

var UpdateEventBind = function () {
    $("#AdmissionTypeCodeTableContent").find("[name='edit']").off("click").on("click", function () {
        var button = $(this);
        selectedIndex = button.closest('tr').index();
    });
};

$(function () {
    UpdateEventBind();
});

var onSuccessAdmissionTypeCode = function (data) {
    if (data.Status) {
        if (data.Type == "Add") {
            $("#AdmissionTypeCodeTableContent").prepend(data.Template);
            //$("#AdmissionTypeCodeTableContent tr").first().effect('highlight', { color: 'red !important' }, 5000);
        } else {
            $("#AdmissionTypeCodeTableContent tr").eq(selectedIndex).remove();
            if (selectedIndex != 0) {
                $("#AdmissionTypeCodeTableContent tr").eq(selectedIndex - 1).after(data.Template);
            } else {
                $("#AdmissionTypeCodeTableContent").prepend(data.Template);
            }
        }
        PopNotify("Admission Type Code", data.Message, "success");
        $("#ResetFormButton").click();
        UpdateEventBind();
    } else {
        PopNotify("Admission Type Code", data.Message, "error");
    }
};
