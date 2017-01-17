var selectedIndex = 0;

var UpdateEventBind = function () {
    $("#DisciplineTableContent").find("[name='edit']").off("click").on("click", function () {
        var button = $(this);
        selectedIndex = button.closest('tr').index();
    });
};

$(function () {
    UpdateEventBind();
});

var onSuccessDiscipline = function (data) {
    if (data.Status) {
        if (data.Type == "Add") {
            $("#DisciplineTableContent").prepend(data.Template);
            //$("#DisciplineTableContent tr").first().effect('highlight', { color: 'red !important' }, 5000);
        } else {
            $("#DisciplineTableContent tr").eq(selectedIndex).remove();
            if (selectedIndex != 0) {
                $("#DisciplineTableContent tr").eq(selectedIndex - 1).after(data.Template);
            } else {
                $("#DisciplineTableContent").prepend(data.Template);
            }
        }
        PopNotify("Discipline", data.Message, "success");
        $("#ResetFormButton").click();
        UpdateEventBind();
    } else {
        PopNotify("Discipline", data.Message, "error");
    }
};
