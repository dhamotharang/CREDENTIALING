

$('.proceedToAuditing').on('click', function () {

    if (createClaimByType == 'Multiple claims for a Provider') {
        currentProgressBarData[5].postData = new FormData($('#Coding_details_form')[0]);
        MakeItActive(6, currentProgressBarData);
    } else {
        currentProgressBarData[4].postData = new FormData($('#Coding_details_form')[0]);
        MakeItActive(5, currentProgressBarData);
    }

});