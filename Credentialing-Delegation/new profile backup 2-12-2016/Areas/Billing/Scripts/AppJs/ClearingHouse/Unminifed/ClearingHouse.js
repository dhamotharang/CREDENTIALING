var clearingHouseId = null;
$('.p_headername').html('Billing');

$('#clearingHouseTable').on('click', '.view_clearinghouse_btn', function () {

    clearingHouseId = $(this).parent().attr("data-container");

    $.ajax({
        type: 'GET',
        url: '/Billing/ClearingHouse/ViewClearingHouse?ClearingHouseId=' + clearingHouseId,
        processData: false,
        contentType: false,
        success: function (result) {
            $('#clearinghouse_list').hide();
            $('#selected_clearingHouse').html(result);
        }
    });
});

/** @description Edit clearingHouse.
*/
$('#clearingHouseTable').on('click', '.edit_clearinghouse_btn', function () {
    clearingHouseId = $(this).parent().attr("data-container");

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
});
/** @description Add clearingHouse.
*/
$('.add_clearinghouse_btn').on('click', function () {

    $.ajax({
        type: 'GET',
        url: '/Billing/ClearingHouse/AddClearingHouse',
        processData: false,
        contentType: false,
        success: function (result) {
            $('#clearinghouse_list').hide();
            $('#selected_clearingHouse').html(result);
        }
    });
});



//-----------------------grid component------------------------------

$('#clearingHouseTable').prtGrid({
    url: "/Billing/ClearingHouse/GetClearingHouseListByIndex",
    dataLength: 50,
    columns: [{ type: 'text', name: 'ClearingHouseId', text: 'Clearing House ID', widthPercentage: 20, sortable: { isSort: true, defaultSort: 'ASC' } },
    { type: 'text', name: 'ClearingHouseName', text: 'Clearing House Name', widthPercentage: 35, sortable: { isSort: true, defaultSort: null } },
    { type: 'text', name: 'PayersCount', text: 'No. of Associated Payers', widthPercentage: 30, sortable: { isSort: true, defaultSort: null } },
    { type: 'none', name: '', text: '', widthPercentage: 15 }]
});

