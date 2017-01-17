

/** 
@description adjusting height of circle in activity logger with respect to width.
 */
$('.progress_bar .bar .progress_container .center_circle').each(function () {
    $(this).height($(this).width());
});

$('.progress_bar .bar .progress_container').each(function () {
    var current_element = $(this);
    var margin_top = (current_element.find('.center_circle').height() / 2) - 2.5;
    current_element.find('.left_bar').css('margin-top', margin_top + 'px');
    current_element.find('.right_bar').css('margin-top', margin_top + 'px');
});

$('#GoBackBtn').on('click', function () {
    TabManager.closeCurrentlyActiveTab();
    TabManager.navigateToTab({
        "tabAction": "Claims", "tabTitle": "Home", "tabPath": "~/Areas/Billing/Views/ClaimsTracking/Index.cshtml",
        "tabContainer": "fullBodyContainer"
    })

})


$('a.claim_link').on('click', function () {
    var ClaimId = 1;
    var stat = [];
    var statCount = 0;

    $('.active_activity_logger').removeClass('active_activity_logger');
    $(this).addClass('active_activity_logger');
    $(this).closest('.bar').children().each(function () {
        if (statCount < 2) {
            stat.push($(this).text());
            statCount++;
        }
    })

    /** 
    @description Get cms1500 form on click of status of activity logger.
 */
    $.ajax({
        type: 'GET',
        url: '/Billing/ClaimsTracking/GetCms1500FormInstance?ClaimId=' + ClaimId,
        success: function (data) {
            $('#ClaimFormContainer').html(data);
            $('#ActivityLoggerCMS1500Form').find('.title_heading').text("CMS 1500 Form Preview  of Status - " + stat[0])

        }
    });
});