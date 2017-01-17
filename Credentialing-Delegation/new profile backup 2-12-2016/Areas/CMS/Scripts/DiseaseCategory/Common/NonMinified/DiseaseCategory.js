var selectedIndex = 0;

var UpdateEventBind = function () {
    $("#DiseaseCategoryTableContent").find("[name='edit']").off("click").on("click", function () {
        var button = $(this);
        selectedIndex = button.closest('tr').index();
    });
};

$(function () {
    UpdateEventBind();
});

var onSuccessDiseaseCategory = function (data) {
    if (data.Status) {
        if (data.Type == "Add") {
            $("#DiseaseCategoryTableContent").prepend(data.Template);
            //$("#DiseaseCategoryTableContent tr").first().effect('highlight', { color: 'red !important' }, 5000);
        } else {
            $("#DiseaseCategoryTableContent tr").eq(selectedIndex).remove();
            if (selectedIndex != 0) {
                $("#DiseaseCategoryTableContent tr").eq(selectedIndex - 1).after(data.Template);
            } else {
                $("#DiseaseCategoryTableContent").prepend(data.Template);
            }
        }
        PopNotify("DiseaseCategory", data.Message, "success");
        $("#ResetFormButton").click();
        UpdateEventBind();
    } else {
        PopNotify("DiseaseCategory", data.Message, "error");
    }
};
