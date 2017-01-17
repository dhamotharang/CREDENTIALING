var submitFn = function () {
    var form = $('#SaveAuditingDetails');
};
var onSuccessFn = function () {
    TabManager.closeCurrentlyActiveTab();
    var tab = {
        "tabAction": "Coding List",
        "tabTitle": "Coding List",
        "tabPath": "/Auditing/Auditing/GetAuditingList",
        "tabContainer": "fullBodyContainer"
    }
    TabManager.navigateToTab(tab);
}
var validateForm = function () {
    var form = $('#SaveAuditingDetailsFormDiv').find('form');
    form.removeData("validator").removeData("unobtrusiveValidation");
    $.validator.unobtrusive.parse(form);
    if (form.valid()) {
        return true;
    }
    return false;
};

