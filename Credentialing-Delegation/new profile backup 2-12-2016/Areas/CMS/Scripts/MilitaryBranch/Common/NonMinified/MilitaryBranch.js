var selectedIndex = 0;

var UpdateEventBind = function () {
    $("#MilitaryBranchTableContent").find("[name='edit']").off("click").on("click", function () {
        var button = $(this);
        selectedIndex = button.closest('tr').index();
    });
};

$(function () {
    UpdateEventBind();
});

var onSuccessMilitaryBranch = function (data) {
    if (data.Status) {
        if (data.Type == "Add") {
            $("#MilitaryBranchTableContent").prepend(data.Template);
            //$("#MilitaryBranchTableContent tr").first().effect('highlight', { color: 'red !important' }, 5000);
        } else {
            $("#MilitaryBranchTableContent tr").eq(selectedIndex).remove();
            if (selectedIndex != 0) {
                $("#MilitaryBranchTableContent tr").eq(selectedIndex - 1).after(data.Template);
            } else {
                $("#MilitaryBranchTableContent").prepend(data.Template);
            }
        }
        PopNotify("MilitaryBranch", data.Message, "success");
        $("#ResetFormButton").click();
        UpdateEventBind();
    } else {
        PopNotify("MilitaryBranch", data.Message, "error");
    }
};
