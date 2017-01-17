var selectedIndex = 0;

var UpdateEventBind = function () {
    $("#MilitaryRankMilitaryBranchTableContent").find("[name='edit']").off("click").on("click", function () {
        var button = $(this);
        selectedIndex = button.closest('tr').index();
    });
};

$(function () {
    UpdateEventBind();
});

var onSuccessMilitaryRankMilitaryBranch = function (data) {
    if (data.Status) {
        if (data.Type == "Add") {
            $("#MilitaryRankMilitaryBranchTableContent").prepend(data.Template);
            //$("#MilitaryRankMilitaryBranchTableContent tr").first().effect('highlight', { color: 'red !important' }, 5000);
        } else {
            $("#MilitaryRankMilitaryBranchTableContent tr").eq(selectedIndex).remove();
            if (selectedIndex != 0) {
                $("#MilitaryRankMilitaryBranchTableContent tr").eq(selectedIndex - 1).after(data.Template);
            } else {
                $("#MilitaryRankMilitaryBranchTableContent").prepend(data.Template);
            }
        }
        PopNotify("MilitaryRankMilitaryBranch", data.Message, "success");
        $("#ResetFormButton").click();
        UpdateEventBind();
    } else {
        PopNotify("MilitaryRankMilitaryBranch", data.Message, "error");
    }
};
