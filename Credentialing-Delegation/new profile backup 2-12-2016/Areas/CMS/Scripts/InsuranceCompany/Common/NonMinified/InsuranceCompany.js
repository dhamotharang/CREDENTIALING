var selectedIndex = 0;

var UpdateEventBind = function () {
    $("#InsuranceCompanyTableContent").find("[name='edit']").off("click").on("click", function () {
        var button = $(this);
        selectedIndex = button.closest('tr').index();
    });
};

$(function () {
    UpdateEventBind();
});

var onSuccessInsuranceCompany = function (data) {
    if (data.Status) {
        if (data.Type == "Add") {
            $("#InsuranceCompanyTableContent").prepend(data.Template);
            //$("#InsuranceCompanyTableContent tr").first().effect('highlight', { color: 'red !important' }, 5000);
        } else {
            $("#InsuranceCompanyTableContent tr").eq(selectedIndex).remove();
            if (selectedIndex != 0) {
                $("#InsuranceCompanyTableContent tr").eq(selectedIndex - 1).after(data.Template);
            } else {
                $("#InsuranceCompanyTableContent").prepend(data.Template);
            }
        }
        PopNotify("InsuranceCompany", data.Message, "success");
        $("#ResetFormButton").click();
        UpdateEventBind();
    } else {
        PopNotify("InsuranceCompany", data.Message, "error");
    }
};
