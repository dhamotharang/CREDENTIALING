var selectedIndex = 0;

var UpdateEventBind = function () {
    $("#QuestionTableContent").find("[name='edit']").off("click").on("click", function () {
        var button = $(this);
        selectedIndex = button.closest('tr').index();
    });
};

$(function () {
    UpdateEventBind();
});

var onSuccessQuestion = function (data) {
    if (data.Status) {
        if (data.Type == "Add") {
            $("#QuestionTableContent").prepend(data.Template);
            //$("#QuestionTableContent tr").first().effect('highlight', { color: 'red !important' }, 5000);
        } else {
            $("#QuestionTableContent tr").eq(selectedIndex).remove();
            if (selectedIndex != 0) {
                $("#QuestionTableContent tr").eq(selectedIndex - 1).after(data.Template);
            } else {
                $("#QuestionTableContent").prepend(data.Template);
            }
        }
        PopNotify("Question", data.Message, "success");
        $("#ResetFormButton").click();
        UpdateEventBind();
    } else {
        PopNotify("Question", data.Message, "error");
    }
};
