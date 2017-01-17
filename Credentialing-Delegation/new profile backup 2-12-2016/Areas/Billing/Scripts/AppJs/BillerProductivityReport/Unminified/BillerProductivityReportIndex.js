//function getPartialForBPR(view) {
//    $.ajax({
//        type: 'GET',
//        url: '/Billing/BillerProductivityReport/' + view,
//        success: function (data) {
//            $('.eobTable').html(data);
//        }
//    });
//}
$.ajax({
        type: 'GET',
        url: '/Billing/BillerProductivityReport/GetAvgBillerProductivityReport',
        success: function (data) {
            $('.Avg_BPR').html(data);
        }
});

$.ajax({
    type: 'GET',
    url: '/Billing/BillerProductivityReport/GetBillerProductivityReportTable',
    success: function (data) {
        $('.BPRList').html(data);
    }
});