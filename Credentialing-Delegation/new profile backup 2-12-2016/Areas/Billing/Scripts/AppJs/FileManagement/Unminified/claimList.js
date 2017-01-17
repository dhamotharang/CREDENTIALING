/** @description Edit the Claim navigate to CMS1500Form.
 */
$('#ClaimFileName').html(FileNameFor837);
$('#CNLabel837ClaimList').html(CNLabel837ClaimList);
$('#HiddenClaimListID').val(IncomingFileIdFor837List);


$('.editBtn').click(function () {
    $('#claimListTable').hide();
    var claimId = 1;
    $.ajax({
        type: 'GET',
        url: '/Billing/FileManagement/GetCms1500Form?ClaimId=' + claimId,
        success: function (data) {
            $('#claimInfo').show();
            $('#claimInfo').html(data);
        }
    });

})
/** @description Navigate to 837 List.
 */
$('#backTo837').click(function () {
    $('#837claimList').html('');
    $('#Table837List').show();
})

/** @description Navigate to claim List.
 */
function backTo837ClaimList() {
    $('#claimListTable').show();
    $('#claimInfo').hide(); $('#claimInfo').html('');
}