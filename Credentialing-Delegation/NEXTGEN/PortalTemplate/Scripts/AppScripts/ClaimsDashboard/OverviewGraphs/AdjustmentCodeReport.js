$(function () {
    //------------------Adjustment code report---------------------------

    $.ajax({
        url: "/ClaimsDashboard/GetAdjustmentCodeReport?type=1",
        type: 'GET',
        cache: false,
        success: function (result) {
            $('#PenaltyAdjustmentReport').html(result);
        }
    });
})