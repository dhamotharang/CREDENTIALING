var selectedIndex = 0;

var UpdateEventBind = function () {
    $("#QuestionTypeTableContent").find("[name='edit']").off("click").on("click", function () {
        var button = $(this);
        selectedIndex = button.closest('tr').index();
    });
};

$(function () {
    UpdateEventBind();
});

var onSuccessQuestionType = function (data) {
    if (data.Status) {
        if (data.Type == "Add") {
            $("#QuestionTypeTableContent").prepend(data.Template);
            //$("#QuestionTypeTableContent tr").first().effect('highlight', { color: 'red !important' }, 5000);
        } else {
            $("#QuestionTypeTableContent tr").eq(selectedIndex).remove();
            if (selectedIndex != 0) {
                $("#QuestionTypeTableContent tr").eq(selectedIndex - 1).after(data.Template);
            } else {
                $("#QuestionTypeTableContent").prepend(data.Template);
            }
        }
        PopNotify("QuestionType", data.Message, "success");
        $("#ResetFormButton").click();
        UpdateEventBind();
    } else {
        PopNotify("QuestionType", data.Message, "error");
    }
};
