//*************************Function for making the small report to fullscreen ************************************//
var currentDetailedViewParameters = {};
function MakeItFullScreen(targetDivId, targetUrl) {
    currentDetailedViewParameters = { height: $('#' + targetDivId).height(), width: $('#' + targetDivId).width(), top: $('#' + targetDivId).offset().top - $(window).scrollTop(), left: $('#' + targetDivId).offset().left };
    $('body').css("overflow", "hidden");
    $('#' + targetDivId).css({ 'position': 'fixed', 'z-index': '1000000', 'overflow': 'scroll', 'width': currentDetailedViewParameters.width, top: currentDetailedViewParameters.top });
    $('#' + targetDivId).animate({
        'top': '43px',
        'left': '0px'
    }, 500).animate({
        'width': '100%',
        'height': '100%'
    }, 500, function () {
        $.ajax({
            type: 'GET',
            url: '/ClaimsDashboard/GetPartialView?url=' + targetUrl,
            success: function (data) {
                $('#' + targetDivId).find('.small_view').hide();
                $('#' + targetDivId).append(data);
            }
        });

    });

}

//*************************Function closing FullScreen (Makes fullscreen of detailed report to small view(as usual report))************************************//
function CloseFullScreen(targetID) {
    $('body').css("overflow", "scroll");
    $('#' + targetID).find('.detailed_view').remove();
    $('#' + targetID).find('.small_view').fadeIn('slow');
    $('#' + targetID).animate({ 'width': currentDetailedViewParameters.width + 'px' }, 700).animate({ 'height': currentDetailedViewParameters.height + 'px', 'top': currentDetailedViewParameters.top + 'px', 'left': currentDetailedViewParameters.left + 'px' }, 700, function () {
        $('#' + targetID).removeAttr('style');
    });
    resetscreen();
}

//*************************Function calling FullScreen (To view::: Encounters Ready for coding Graph)************************************//
$('#view_Detailed_Report_Btn').off('click').on('click', function () {
    MakeItFullScreen('ReadytoCodeid', "~/Views/PBAS/PBASDashBoard/_EncountersReadyforCoding.cshtml");
});

//*************************Function calling FullScreen (To view::: Reasons for On-Hold Encounters)************************************//
function ViewOnHoldfullscreen() {
    MakeItFullScreen('ReasonforonHoldid', '~/Views/PBAS/PBASDashBoard/_ReasonforOnHoldDetailedReport.cshtml');
}

