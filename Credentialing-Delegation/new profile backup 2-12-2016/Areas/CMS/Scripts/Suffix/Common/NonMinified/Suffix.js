var selectedIndex = 0;

var UpdateEventBind = function () {
    $("#SuffixTableContent").find("[name='edit']").off("click").on("click", function () {
        var button = $(this);
        selectedIndex = button.closest('tr').index();
    });
};

$(function () {
    UpdateEventBind();
});

var onSuccessSuffix = function (data) {
    if (data.Status) {
        if (data.Type == "Add") {
            $("#SuffixTableContent").prepend(data.Template);
            //$("#SuffixTableContent tr").first().effect('highlight', { color: 'red !important' }, 5000);
        } else {
            $("#SuffixTableContent tr").eq(selectedIndex).remove();
            if (selectedIndex != 0) {
                $("#SuffixTableContent tr").eq(selectedIndex - 1).after(data.Template);
            } else {
                $("#SuffixTableContent").prepend(data.Template);
            }
        }
        PopNotify("Suffix", data.Message, "success");
        $("#ResetFormButton").click();
        UpdateEventBind();
    } else {
        PopNotify("Suffix", data.Message, "error");
    }
};
