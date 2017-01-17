var selectedIndex = 0;

var UpdateEventBind = function () {
    $("#MilitaryRankTableContent").find("[name='edit']").off("click").on("click", function () {
        var button = $(this);
        selectedIndex = button.closest('tr').index();
    });
};

$(function () {
    UpdateEventBind();
});

var onSuccessMilitaryRank = function (data) {
    if (data.Status) {
        if (data.Type == "Add") {
            $("#MilitaryRankTableContent").prepend(data.Template);
            //$("#MilitaryRankTableContent tr").first().effect('highlight', { color: 'red !important' }, 5000);
        } else {
            $("#MilitaryRankTableContent tr").eq(selectedIndex).remove();
            if (selectedIndex != 0) {
                $("#MilitaryRankTableContent tr").eq(selectedIndex - 1).after(data.Template);
            } else {
                $("#MilitaryRankTableContent").prepend(data.Template);
            }
        }
        PopNotify("MilitaryRank", data.Message, "success");
        $("#ResetFormButton").click();
        UpdateEventBind();
    } else {
        PopNotify("MilitaryRank", data.Message, "error");
    }
};
