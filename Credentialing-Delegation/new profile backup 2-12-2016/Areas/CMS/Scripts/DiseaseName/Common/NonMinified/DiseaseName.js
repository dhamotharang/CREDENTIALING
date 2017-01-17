var selectedIndex = 0;

var UpdateEventBind = function () {
    $("#DiseaseNameTableContent").find("[name='edit']").off("click").on("click", function () {
        var button = $(this);
        selectedIndex = button.closest('tr').index();
    });
};

$(function () {
    UpdateEventBind();
});

var onSuccessDiseaseName = function (data) {
    if (data.Status) {
        if (data.Type == "Add") {
            $("#DiseaseNameTableContent").prepend(data.Template);
            //$("#DiseaseNameTableContent tr").first().effect('highlight', { color: 'red !important' }, 5000);
        } else {
            $("#DiseaseNameTableContent tr").eq(selectedIndex).remove();
            if (selectedIndex != 0) {
                $("#DiseaseNameTableContent tr").eq(selectedIndex - 1).after(data.Template);
            } else {
                $("#DiseaseNameTableContent").prepend(data.Template);
            }
        }
        PopNotify("DiseaseName", data.Message, "success");
        $("#ResetFormButton").click();
        UpdateEventBind();
    } else {
        PopNotify("DiseaseName", data.Message, "error");
    }
};
