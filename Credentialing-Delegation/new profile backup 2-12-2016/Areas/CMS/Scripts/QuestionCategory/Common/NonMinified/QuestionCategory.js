var selectedIndex = 0;

var UpdateEventBind = function () {
    $("#QuestionCategoryTableContent").find("[name='edit']").off("click").on("click", function () {
        var button = $(this);
        selectedIndex = button.closest('tr').index();
    });
};

$(function () {
    UpdateEventBind();
});

var onSuccessQuestionCategory = function (data) {
    if (data.Status) {
        if (data.Type == "Add") {
            $("#QuestionCategoryTableContent").prepend(data.Template);
            //$("#QuestionCategoryTableContent tr").first().effect('highlight', { color: 'red !important' }, 5000);
        } else {
            $("#QuestionCategoryTableContent tr").eq(selectedIndex).remove();
            if (selectedIndex != 0) {
                $("#QuestionCategoryTableContent tr").eq(selectedIndex - 1).after(data.Template);
            } else {
                $("#QuestionCategoryTableContent").prepend(data.Template);
            }
        }
        PopNotify("QuestionCategory", data.Message, "success");
        $("#ResetFormButton").click();
        UpdateEventBind();
    } else {
        PopNotify("QuestionCategory", data.Message, "error");
    }
};
