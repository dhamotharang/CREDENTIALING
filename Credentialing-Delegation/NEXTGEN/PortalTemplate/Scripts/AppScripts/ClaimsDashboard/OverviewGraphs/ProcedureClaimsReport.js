$(function () {

    //------------------procedures claimed report---------------------------

    $.ajax({
        url: "/ClaimsDashboard/GetProcedureClaimsReport?type=1",
        type: 'GET',
        cache: false,
        success: function (result) {
            $('#ProceduresClaimedReport').html(result);
        }
    });
})