var selectedIndex = 0;

var UpdateEventBind = function () {
    $("#EDISegmentQualifierTableContent").find("[name='edit']").off("click").on("click", function () {
        var button = $(this);
        selectedIndex = button.closest('tr').index();
    });
};

$(function () {
    UpdateEventBind();
});

var onSuccessEDISegmentQualifier = function (data) {
    if (data.Status) {
        if (data.Type == "Add") {
            $("#EDISegmentQualifierTableContent").prepend(data.Template);
            //$("#EDISegmentQualifierTableContent tr").first().effect('highlight', { color: 'red !important' }, 5000);
        } else {
            $("#EDISegmentQualifierTableContent tr").eq(selectedIndex).remove();
            if (selectedIndex != 0) {
                $("#EDISegmentQualifierTableContent tr").eq(selectedIndex - 1).after(data.Template);
            } else {
                $("#EDISegmentQualifierTableContent").prepend(data.Template);
            }
        }
        PopNotify("EDISegmentQualifier", data.Message, "success");
        $("#ResetFormButton").click();
        UpdateEventBind();
    } else {
        PopNotify("EDISegmentQualifier", data.Message, "error");
    }
};
