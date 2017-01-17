var selectedIndex = 0;

var UpdateEventBind = function () {
    $("#PatientRelationTableContent").find("[name='edit']").off("click").on("click", function () {
        var button = $(this);
        selectedIndex = button.closest('tr').index();
    });
};

$(function () {
    UpdateEventBind();
});

var onSuccessPatientRelation = function (data) {
    if (data.Status) {
        if (data.Type == "Add") {
            $("#PatientRelationTableContent").prepend(data.Template);
            //$("#PatientRelationTableContent tr").first().effect('highlight', { color: 'red !important' }, 5000);
        } else {
            $("#PatientRelationTableContent tr").eq(selectedIndex).remove();
            if (selectedIndex != 0) {
                $("#PatientRelationTableContent tr").eq(selectedIndex - 1).after(data.Template);
            } else {
                $("#PatientRelationTableContent").prepend(data.Template);
            }
        }
        PopNotify("PatientRelation", data.Message, "success");
        $("#ResetFormButton").click();
        UpdateEventBind();
    } else {
        PopNotify("PatientRelation", data.Message, "error");
    }
};
