

/** 
@description adjusting height of container in cms 1500 form
*/
$('#cms1500Form').ready(function () {
    $('#cms1500Form').find('.row').each(function () {
        var current_row = $(this);
        var row_height = current_row.height();
        current_row.find('.cms_div').each(function () {
            $(this).height(row_height);
        });
    });
});

/** 
@description navigating back to claims info page
*/
$('.x_content').off('click', '#GoToClaimsInfoBtn').on('click', '#GoToClaimsInfoBtn', function () {
    TabManager.closeCurrentlyActiveTab();
    TabManager.navigateToTab({
        "tabAction": "Claims", "tabTitle": "Home", "tabPath": "~/Areas/Billing/Views/ClaimsTracking/Index.cshtml",
        "tabContainer": "fullBodyContainer"
    })
   
});


/** 
@description modal for reason to hold
*/
$('#cmsOnHold').click(function () {
    event.preventDefault();
    ShowModal('~/Areas/Billing/Views/ClaimsTracking/_ReasonToHold.cshtml', 'Reason to hold');
})


/** 
@description modal for reason to Reject
*/
$('#cmsOnReject').click(function () {
    event.preventDefault();
    ShowModal('~/Areas/Billing/Views/ClaimsTracking/_ReasonToReject.cshtml', 'Reason to Reject');
})

/** 
@description display success message after changing status
*/
$('.status_list li.no_reason').on('click',function(){

       new PNotify({
        title: "Claim's Status changed Successfully",
        text: "Claim's status has been changed successfully with Claim ID - P20160922C37",
        type: 'success',
        animate: {
            animate: true,
            in_class: "lightSpeedIn",
            out_class: "slideOutRight"
        }
    });


       TabManager.closeCurrentlyActiveTab();
       TabManager.navigateToTab({
           "tabAction": "Claims", "tabTitle": "Home", "tabPath": "~/Areas/Billing/Views/ClaimsTracking/Index.cshtml",
           "tabContainer": "fullBodyContainer"
       })

});