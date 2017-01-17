var selectedIndex = 0;

var UpdateEventBind = function () {
    $("#RelationshipTableContent").find("[name='edit']").off("click").on("click", function () {
        var button = $(this);
        selectedIndex = button.closest('tr').index();
    });
};

$(function () {
    UpdateEventBind();
});

var onSuccessRelationship = function (data) {
    if (data.Status) {
        if (data.Type == "Add") {
            $("#RelationshipTableContent").prepend(data.Template);
            //$("#RelationshipTableContent tr").first().effect('highlight', { color: 'red !important' }, 5000);
        } else {
            $("#RelationshipTableContent tr").eq(selectedIndex).remove();
            if (selectedIndex != 0) {
                $("#RelationshipTableContent tr").eq(selectedIndex - 1).after(data.Template);
            } else {
                $("#RelationshipTableContent").prepend(data.Template);
            }
        }
        PopNotify("Relationship", data.Message, "success");
        $("#ResetFormButton").click();
        UpdateEventBind();
    } else {
        PopNotify("Relationship", data.Message, "error");
    }
};
