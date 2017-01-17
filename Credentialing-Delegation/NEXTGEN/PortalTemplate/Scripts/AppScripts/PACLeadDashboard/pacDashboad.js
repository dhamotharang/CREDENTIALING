//$(document).ready(function () {
//    $('.centerDiv').css({
//        'transform': 'rotateX(-118deg)',
//        'top':'0',
//        'display':'block',
//    });
//    $('.centerDiv').animate({
//        'transform': 'rotateX(8deg)',
//        'top': '50',
//    },1000)
//})

function MakeItFullScreen(targetDivId, targetUrl) {
    currentDetailedViewParameters = { height: $('#' + targetDivId).height(), width: $('#' + targetDivId).width(), top: $('#' + targetDivId).offset().top - $(window).scrollTop(), left: $('#' + targetDivId).offset().left };
    htmlContent = $('#' + targetDivId).html();
    $('body').css("overflow", "hidden");
    $('#' + targetDivId).css({ 'position': 'fixed', 'z-index': '1000000', 'overflow': 'hidden', top: currentDetailedViewParameters.top });
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
                //$('#' + targetDivId).ready(function () {
                //    setTimeout(function () {
                //        nodeScriptReplace(document.getElementById(targetDivId));
                //    }, 1000);


                //})

            }
        });

    });

}


function CloseFullScreen(targetID) {
    $('#' + targetID).find('.detailed_view').remove();
    $('#' + targetID).find('.small_view').show();
    $('#' + targetID).animate({ 'width': currentDetailedViewParameters.width + 'px' }, 700).animate({ 'height': currentDetailedViewParameters.height + 'px', 'top': currentDetailedViewParameters.top + 'px', 'left': currentDetailedViewParameters.left + 'px' }, 700, function () {

        $('#' + targetID).removeAttr('style');

    });
}

$('#Show_Detailed_Report_Refferral').click(function () {
    MakeItFullScreen('Referrals', "~/Views/PACLeadDashboard/_RefferralsDetailedReport.cshtml");
})

$('#Show_Detailed_Report_PPT').click(function () {
    MakeItFullScreen('PeerProcessTime', "~/Views/PACLeadDashboard/_PeerPTDetailedReport.cshtml");
})