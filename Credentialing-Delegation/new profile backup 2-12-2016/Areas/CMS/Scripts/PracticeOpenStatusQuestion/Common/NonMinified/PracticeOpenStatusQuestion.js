var selectedIndex = 0;

var UpdateEventBind = function () {
    $("#PracticeOpenStatusQuestionTableContent").find("[name='edit']").off("click").on("click", function () {
        var button = $(this);
        selectedIndex = button.closest('tr').index();
    });
};

$(function () {
    UpdateEventBind();
});

var onSuccessPracticeOpenStatusQuestion = function (data) {
    if (data.Status) {
        if (data.Type == "Add") {
            $("#PracticeOpenStatusQuestionTableContent").prepend(data.Template);
            //$("#PracticeOpenStatusQuestionTableContent tr").first().effect('highlight', { color: 'red !important' }, 5000);
        } else {
            $("#PracticeOpenStatusQuestionTableContent tr").eq(selectedIndex).remove();
            if (selectedIndex != 0) {
                $("#PracticeOpenStatusQuestionTableContent tr").eq(selectedIndex - 1).after(data.Template);
            } else {
                $("#PracticeOpenStatusQuestionTableContent").prepend(data.Template);
            }
        }
        PopNotify("PracticeOpenStatusQuestion", data.Message, "success");
        $("#ResetFormButton").click();
        UpdateEventBind();
    } else {
        PopNotify("PracticeOpenStatusQuestion", data.Message, "error");
    }
};
