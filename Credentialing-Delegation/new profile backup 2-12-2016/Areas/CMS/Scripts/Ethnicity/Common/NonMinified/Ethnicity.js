var selectedIndex = 0;

var UpdateEventBind = function () {
    $("#EthnicityTableContent").find("[name='edit']").off("click").on("click", function () {
        var button = $(this);
        selectedIndex = button.closest('tr').index();
    });
};

$(function () {
    UpdateEventBind();
});

var onSuccessEthnicity = function (data) {
    if (data.Status) {
        if (data.Type == "Add") {
            $("#EthnicityTableContent").prepend(data.Template);
            //$("#EthnicityTableContent tr").first().effect('highlight', { color: 'red !important' }, 5000);
        } else {
            $("#EthnicityTableContent tr").eq(selectedIndex).remove();
            if (selectedIndex != 0) {
                $("#EthnicityTableContent tr").eq(selectedIndex - 1).after(data.Template);
            } else {
                $("#EthnicityTableContent").prepend(data.Template);
            }
        }
        PopNotify("Ethnicity", data.Message, "success");
        $("#ResetFormButton").click();
        UpdateEventBind();
    } else {
        PopNotify("Ethnicity", data.Message, "error");
    }
};
