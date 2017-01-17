var selectedIndex = 0;

var UpdateEventBind = function () {
    $("#QualificationDegreeTableContent").find("[name='edit']").off("click").on("click", function () {
        var button = $(this);
        selectedIndex = button.closest('tr').index();
    });
};

$(function () {
    UpdateEventBind();
});

var onSuccessQualificationDegree = function (data) {
    if (data.Status) {
        if (data.Type == "Add") {
            $("#QualificationDegreeTableContent").prepend(data.Template);
            //$("#QualificationDegreeTableContent tr").first().effect('highlight', { color: 'red !important' }, 5000);
        } else {
            $("#QualificationDegreeTableContent tr").eq(selectedIndex).remove();
            if (selectedIndex != 0) {
                $("#QualificationDegreeTableContent tr").eq(selectedIndex - 1).after(data.Template);
            } else {
                $("#QualificationDegreeTableContent").prepend(data.Template);
            }
        }
        PopNotify("QualificationDegree", data.Message, "success");
        $("#ResetFormButton").click();
        UpdateEventBind();
    } else {
        PopNotify("QualificationDegree", data.Message, "error");
    }
};
