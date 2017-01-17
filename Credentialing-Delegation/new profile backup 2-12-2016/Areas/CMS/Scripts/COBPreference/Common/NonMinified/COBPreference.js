var selectedIndex = 0;

var UpdateEventBind = function () {
    $("#COBPreferenceTableContent").find("[name='edit']").off("click").on("click", function () {
        var button = $(this);
        selectedIndex = button.closest('tr').index();
    });
};

$(function () {
    UpdateEventBind();
});

var onSuccessCOBPreference = function (data) {
    if (data.Status) {
        if (data.Type == "Add") {
            $("#COBPreferenceTableContent").prepend(data.Template);
            //$("#COBPreferenceTableContent tr").first().effect('highlight', { color: 'red !important' }, 5000);
        } else {
            $("#COBPreferenceTableContent tr").eq(selectedIndex).remove();
            if (selectedIndex != 0) {
                $("#COBPreferenceTableContent tr").eq(selectedIndex - 1).after(data.Template);
            } else {
                $("#COBPreferenceTableContent").prepend(data.Template);
            }
        }
        PopNotify("COBPreference", data.Message, "success");
        $("#ResetFormButton").click();
        UpdateEventBind();
    } else {
        PopNotify("COBPreference", data.Message, "error");
    }
};
