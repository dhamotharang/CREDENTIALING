var selectedIndex = 0;

var UpdateEventBind = function () {
    $("#SpecialityTableContent").find("[name='edit']").off("click").on("click", function () {
        var button = $(this);
        selectedIndex = button.closest('tr').index();
    });
};

$(function () {
    UpdateEventBind();
});

var onSuccessSpeciality = function (data) {
    if (data.Status) {
        if (data.Type == "Add") {
            $("#SpecialityTableContent").prepend(data.Template);
            //$("#SpecialityTableContent tr").first().effect('highlight', { color: 'red !important' }, 5000);
        } else {
            $("#SpecialityTableContent tr").eq(selectedIndex).remove();
            if (selectedIndex != 0) {
                $("#SpecialityTableContent tr").eq(selectedIndex - 1).after(data.Template);
            } else {
                $("#SpecialityTableContent").prepend(data.Template);
            }
        }
        PopNotify("Speciality", data.Message, "success");
        $("#ResetFormButton").click();
        UpdateEventBind();
    } else {
        PopNotify("Speciality", data.Message, "error");
    }
};
