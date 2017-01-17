
$("#AddMemberMainContainer").on('click', '.list-item', function (e) {
    e.preventDefault();
    var MethodName = '';
    var id = $(this).parent().attr('id');
    $(this).parent().addClass("current");
    $(this).parent().siblings().removeClass("current");
    var url = $(this).data("partial");
    TabManager.getDynamicContent(url, "AddMemberTabView", MethodName, '');
});
setTimeout(function () {
    $('#AddMemberMainContainer .list-item').first().click();
}, 1000);


//<<==================Tab Functions (Proceed and Back) Starts Here==========>>//

function goToTab(from , to) {
    $("#" + from).removeClass("current");
    $("#" + to).addClass("current");
    var url = $("#" + to).find('a').attr('data-partial');
    TabManager.getDynamicContent(url, "AddMemberTabView", '', '');
}


$("#AddMemberMainContainer").off('click', '.proceedToInsuredDetails').on('click', '.proceedToInsuredDetails', function () {
    goToTab('MemberDetailsTab', 'InsurerDetailsTab');
}).off('click', '.goBacktoPatientDetails').on('click', '.goBacktoPatientDetails', function () {
    goToTab('InsurerDetailsTab', 'MemberDetailsTab');
}).off('click', '.proceedToProviderDetails').on('click', '.proceedToProviderDetails', function () {
    goToTab('InsurerDetailsTab', 'ProviderDetailsTab');
}).off('click', '.goBacktoInsuredDetails').on('click', '.goBacktoInsuredDetails', function () {
    goToTab('ProviderDetailsTab', 'InsurerDetailsTab');
}).off('click', '.proceedToPremiumPaymentDetails').on('click', '.proceedToPremiumPaymentDetails', function () {
    goToTab('ProviderDetailsTab', 'PaymentDetailsTab');
}).off('click', '.goBacktoProviderDetails').on('click', '.goBacktoProviderDetails', function () {
    goToTab('PaymentDetailsTab', 'ProviderDetailsTab');
}).off('click', '.proceedToPreview').on('click', '.proceedToPreview', function () {
    goToTab('PaymentDetailsTab', 'PreviewDetailsTab');
}).off('click', '.goBacktoPremiumPaymentDetails').on('click', '.goBacktoPremiumPaymentDetails', function () {
    goToTab('PreviewDetailsTab', 'PaymentDetailsTab');
}).off('click', '.closeAddMember').on('click', '.closeAddMember', function () {
    TabManager.closeCurrentlyActiveTab()
}).off('click', '.cancelPreview').on('click', '.cancelPreview', function () { TabManager.closeCurrentlyActiveTab() })

//<<==================End of Tab Functions (Proceed and Back) Ends Here==========>>//