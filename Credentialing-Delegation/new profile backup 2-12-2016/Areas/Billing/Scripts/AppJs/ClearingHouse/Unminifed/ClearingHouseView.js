
/** @description Navigating to previous clearingHouse.
 */
$('.previous_clearingHouse_btn').on('click', function () {
    $('#selected_clearingHouse').html('');
    $('#clearinghouse_list').show();
})

/** @description Edit the ClearingHouse.
 */
$('.edit_clearingHouse_btn').on('click', function () {
    $.ajax({
        type: 'GET',
        url: '/Billing/ClearingHouse/EditClearingHouse?ClearingHouseId=' + clearingHouseId,
        processData: false,
        contentType: false,
        success: function (result) {
            $('#clearinghouse_list').hide();
            $('#selected_clearingHouse').html(result);
        }
    });
    
})