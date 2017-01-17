/** @description Dual list for payers.
 */
console.log("..clearing House script loaded");
var demo2 = $('.PayerList').bootstrapDualListbox({
    nonSelectedListLabel: 'Payer List',
    selectedListLabel: 'Selected Payers',
    preserveSelectionOnMove: 'moved',
    moveOnSelect: true
});


/** @description Navigating to previous clearingHouse.
 */
$('.previous_clearingHouse_btn').on('click', function () {
    $('#selected_clearingHouse').html('');
    $('#clearinghouse_list').show();
})

/** @description Save clearingHouse.
 */
//$('.save_clearingHouse_btn').on('click', function () {
//    $('#selected_clearingHouse').html('');
//    $('#clearinghouse_list').show();
//})
var onSuccessFn = function () {
    TabManager.closeCurrentlyActiveTab();
    var tab = {
        "tabAction": "ClearingHouse",
        "tabTitle": "Clearing House",
        "tabPath": "/Billing/ClearingHouse/GetClearingHouseList",
        "tabContainer": "fullBodyContainer"
    }
    TabManager.navigateToTab(tab);
}
var validateForm = function () {
    var form = $('#ClearingHouseSaveForm');
    form.removeData("validator").removeData("unobtrusiveValidation");
    $.validator.unobtrusive.parse(form);
    if (form.valid()) {
        return true;
    }
    return false;

};