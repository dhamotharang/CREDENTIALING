/** @description Popover.
 */
$(document).ready(function () {
    $('[data-toggle="popover"]').popover();
});

/** @description Navigate to EOBReceved ClaimList.
 */
$('#GoBackFromEOBBtn').on('click', function () {

    $('#cms1500_container').html('');
    $('#claims_tracking_overall_page').show();
});