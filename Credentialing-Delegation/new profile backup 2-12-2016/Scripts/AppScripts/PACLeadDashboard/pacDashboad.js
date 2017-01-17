$(document).ready(function () {
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
        resetscreen();
    }

    $('#Show_Detailed_Report_Refferral').click(function () {
        MakeItFullScreen('Referrals', "~/Views/PACLeadDashboard/_RefferralsDetailedReport.cshtml");
    })

    $('#Show_Detailed_Report_PPT').click(function () {
        MakeItFullScreen('PeerProcessTime', "~/Views/PACLeadDashboard/_PeerPTDetailedReport.cshtml");
    })

    $('.drag_btn_drop').mousedown(function () {
        $("#sortable").sortable();
        $("#sortable").sortable("enable");
    })

    $('.drag_btn_drop').mouseup(function () {
        $("#sortable").sortable("disable");
    })

    InitICheckFinal();

});

