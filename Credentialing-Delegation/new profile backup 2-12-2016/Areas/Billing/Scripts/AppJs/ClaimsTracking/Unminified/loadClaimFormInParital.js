  var ClaimId = 1;
$.ajax({
    type: 'GET',
    url: '/Billing/ClaimsTracking/GetCms1500Form?ClaimId=' + ClaimId,
    success: function (data) {
        $('#cms1500_container').html(data);
    }
});