function ShowSelectedMemberForProvider() {
    $.ajax({
        type: 'GET',
        url: '/Billing/CreateClaim/GetMemberListForProviderResult',
        success: function (data) {
            $('#member_for_provider_result').html(data);
        }
    });
}
$('#member_search_for_provider_btn').on('click', ShowSelectedMemberForProvider);

$('#member_search_for_provider_btn').click(function () {
    $('.form_Create_Provider_Member').show();
    $('.Member_Provider_search').hide();
    $('.DOSFrom').html($('input[name="DOSFromProvider"]').val());
    $('.DOSTo').html($('input[name="DOSToPrivuder"]').val());
    $('.Member').html($('input[name="Member_Provider"]').val());

    
})

$('.edit_create_claim_Form_Provider').click(function () {
    $('.form_Create_Provider_Member').hide();
    $('.Member_Provider_search').show();
})