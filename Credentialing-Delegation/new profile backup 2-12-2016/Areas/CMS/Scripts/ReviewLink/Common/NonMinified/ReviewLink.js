var selectedIndex = 0;

var UpdateEventBind = function () {
    $("#ReviewLinkTableContent").find("[name='edit']").off("click").on("click", function () {
        var button = $(this);
        selectedIndex = button.closest('tr').index();
    });
};

$(function () {
    UpdateEventBind();
});

var onSuccessReviewLink = function (data) {
    if (data.Status) {
        if (data.Type == "Add") {
            $("#ReviewLinkTableContent").prepend(data.Template);
            //$("#ReviewLinkTableContent tr").first().effect('highlight', { color: 'red !important' }, 5000);
        } else {
            $("#ReviewLinkTableContent tr").eq(selectedIndex).remove();
            if (selectedIndex != 0) {
                $("#ReviewLinkTableContent tr").eq(selectedIndex - 1).after(data.Template);
            } else {
                $("#ReviewLinkTableContent").prepend(data.Template);
            }
        }
        PopNotify("ReviewLink", data.Message, "success");
        $("#ResetFormButton").click();
        UpdateEventBind();
    } else {
        PopNotify("ReviewLink", data.Message, "error");
    }
};
