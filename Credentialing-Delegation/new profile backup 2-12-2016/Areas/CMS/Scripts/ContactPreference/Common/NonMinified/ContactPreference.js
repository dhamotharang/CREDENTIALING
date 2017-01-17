var selectedIndex = 0;

var UpdateEventBind = function () {
    $("#ContactPreferenceTableContent").find("[name='edit']").off("click").on("click", function () {
        var button = $(this);
        selectedIndex = button.closest('tr').index();
    });
};

$(function () {
    UpdateEventBind();
});

var onSuccessContactPreference = function (data) {
    if (data.Status) {
        if (data.Type == "Add") {
            $("#ContactPreferenceTableContent").prepend(data.Template);
            //$("#ContactPreferenceTableContent tr").first().effect('highlight', { color: 'red !important' }, 5000);
        } else {
            $("#ContactPreferenceTableContent tr").eq(selectedIndex).remove();
            if (selectedIndex != 0) {
                $("#ContactPreferenceTableContent tr").eq(selectedIndex - 1).after(data.Template);
            } else {
                $("#ContactPreferenceTableContent").prepend(data.Template);
            }
        }
        PopNotify("ContactPreference", data.Message, "success");
        $("#ResetFormButton").click();
        UpdateEventBind();
    } else {
        PopNotify("ContactPreference", data.Message, "error");
    }
};
