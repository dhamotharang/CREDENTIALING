
/** 
@description getting member list on selecting provider from provider result
 */

$('#MemberResultForProvider tbody tr').on('click', function () {
    selectedMemberId = parseInt($(this).attr('data-container'));

    $.ajax({
        type: 'GET',
        url: '/Billing/CreateClaim/GetSelectedMemberResult?MemberId=' + selectedMemberId,
        success: function (data) {
            $('#member_for_provider_result').html(data);
        }
    });
})