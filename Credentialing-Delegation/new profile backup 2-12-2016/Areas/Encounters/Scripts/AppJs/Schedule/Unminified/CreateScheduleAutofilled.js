TabManager.showLoadingSymbol('fullBodyContainer');
setTimeout(function () {
    TabManager.hideLoadingSymbol('fullBodyContainer');
    autofillData();
}, 800);


function autofillData() {
    $('.form_wizard').show();
    CreateProgressSteppedBar(progressBarDataForMember);
    MakeItActive(5, progressBarDataForProvider);    
    $('input[type="radio"][name="CreateClaimsBy"]')[2].checked = true;
}
