
$(document).ready(function () {

    $('.p_headername').html('Billing');

    //----------------------------------------test code----------------------------------------


    var TrackerObject = {
        containerID: 'container',
        xBlocks: 10,
        yBlocks: 3,
        statusContainer: [
        { x: 0, y: 1, statusId: '0', connectTo: [1, 2, 3], category: 'claims_gateway', type: 'default', text: 'Open', fullTitle: 'Open', amount: 231.232, count: 322.2332 },
        { x: 1, y: 0, statusId: '1', category: 'claims_gateway', type: 'warning', text: 'On Hold', fullTitle: 'On Hold', amount: 231.232, count: 322.2332 },
        //2{ x: 2, y: 0, statusId: '4', category: 'claims_gateway', type: 'warning', text: 'Phy. On hold', fullTitle: 'Physician On Hold', amount: 231.232, count: 322.2332 },
        //3{ x: 1, y: 2, statusId: '3', category: 'claims_gateway', type: 'warning', text: 'Comm. Review', fullTitle: 'Committee Review', amount: 231.232, count: 322.2332 },
        { x: 1, y: 2, statusId: '2', category: 'claims_gateway', type: 'danger', text: 'Rejected', fullTitle: 'Rejected', amount: 231.232, count: 322.2332 },
        { x: 2, y: 1, statusId: '3', connectTo: [4], category: 'claims_gateway', type: 'success', text: 'Accepted', fullTitle: 'Accepted', amount: 231.232, count: 322.2332 },
        { x: 3, y: 1, statusId: '4', connectTo: [5,6,7], category: 'claims_gateway', type: 'success', text: 'Dispatched', fullTitle: 'Dispatched', amount: 231.232, count: 322.2332 },
        { x: 4, y: 0, statusId: '5', category: 'clearing_house', type: 'warning', text: 'Unack by CH', fullTitle: 'Unacknowledged by CH', amount: 231.232, count: 322.2332 },
        { x: 4, y: 2, statusId: '6', category: 'clearing_house', type: 'danger', text: 'Rejected by CH', fullTitle: 'Rejected by CH', amount: 231.232, count: 322.2332 },
        { x: 5, y: 1, statusId: '7', connectTo: [8,9,10], category: 'clearing_house', type: 'success', text: 'Accepted by CH', fullTitle: 'Accepted by CH', amount: 231.232, count: 322.2332 },
        { x: 6, y: 0, statusId: '8', category: 'payer', type: 'warning', text: 'Unack by payer', fullTitle: 'Unacknowledged by payer', amount: 231.232, count: 322.2332 },
        { x: 6, y: 2, statusId: '9', category: 'payer', type: 'danger', text: 'Rej by payer', fullTitle: 'Rejected by payer', amount: 231.232, count: 322.2332 },
        { x: 7, y: 1, statusId: '10', connectTo: [11,12,13], category: 'payer', type: 'success', text: 'Acc by payer', fullTitle: 'Accepted by payer', amount: 231.232, count: 322.2332 },
        { x: 8, y: 0, statusId: '11', category: 'payer', type: 'warning', text: 'Pending', fullTitle: 'Pending', amount: 231.232, count: 322.2332 },
        { x: 8, y: 2, statusId: '12', category: 'payer', type: 'danger', text: 'Denied by payer', fullTitle: 'Denied by payer', amount: 231.232, count: 322.2332 },
        { x: 9, y: 1, statusId: '13', category: 'payer', type: 'success', text: 'eob received', fullTitle: 'EOB received', amount: 231.232, count: 322.2332 }
        ]
    };

    var track = new TrackingMapp(TrackerObject);


    /** 
@description Preview specific claim on click of preview
*/

    $('#claims_list').on('click', 'table tbody tr td .editBtn', function () {

        var claimId = $(this).parent().parent().find('.ClaimIdTd').text();


        TabManager.navigateToTab({
            "tabAction": "View Claim Details", "tabTitle": "Edit #" + claimId, "tabPath": "~/Areas/Billing/Views/ClaimsTracking/_ClaimPreviewOfTracking.cshtml",
            "tabContainer": "fullBodyContainer"
        })
    })

    /** 
@description Get EOB report of claim
*/

    $('#claims_list').on('click', 'table tbody tr td .eobBtn', function () {
        var ClaimId = 1;
        $.ajax({
            type: 'GET',
            url: '/Billing/ClaimsTracking/GetEobReport?ClaimId=' + ClaimId,
            success: function (data) {

                $('#claims_tracking_overall_page').hide();
                $('#cms1500_container').html(data);
            }
        });
    })


    /** 
@description showing filter container
*/
    $('#filterData').on('click', function () {
        $('#filterContainer').slideToggle();
    });


    /** 
@description highlighting types of claims gateway on mouse over
*/
    $('.claims_gatway_type').on('mouseenter', function () {
        $('.tracking_status_container').each(function () {
            var ele = $(this).find('.type_claims_gateway')
            if (ele.length > 0) {
                $(this).find('.status_title').css('text-decoration', 'underline');
                $(this).css({ 'box-shadow': '1px 3px 10px 1px #666666' });
            }
        })

        $('.clearing_house_type').css('opacity', '0.5');
        $('.payer_type').css('opacity', '0.5');

    });


    $('.claims_gatway_type').on('mouseleave', function () {
        $('.tracking_status_container').each(function () {
            var ele = $(this).find('.type_claims_gateway')
            if (ele.length > 0) {
                $(this).find('.status_title').css('text-decoration', 'none');
                $(this).css({ 'box-shadow': '0px 0px 0px 0px #b6b6b6' });
            }
        })
        $('.clearing_house_type').css('opacity', '1');
        $('.payer_type').css('opacity', '1');
    });


    /** 
@description highlighting types of clearing house on mouse over
*/
    $('.clearing_house_type').on('mouseenter', function () {
        $('.tracking_status_container').each(function () {
            var ele = $(this).find('.type_clearing_house')
            if (ele.length > 0) {
                $(this).find('.status_title').css('text-decoration', 'underline');
                $(this).css({ 'box-shadow': '1px 3px 10px 1px #666666' });
            }
        })

        $('.claims_gatway_type').css('opacity', '0.5');
        $('.payer_type').css('opacity', '0.5');
    });


    $('.clearing_house_type').on('mouseleave', function () {
        $('.tracking_status_container').each(function () {
            var ele = $(this).find('.type_clearing_house')
            if (ele.length > 0) {
                $(this).find('.status_title').css('text-decoration', 'none');
                $(this).css({ 'box-shadow': '0px 0px 0px 0px #b6b6b6' });
            }
        })

        $('.claims_gatway_type').css('opacity', '1');
        $('.payer_type').css('opacity', '1');
    });



    /** 
@description highlighting types of payer on mouse over
*/
    $('.payer_type').on('mouseenter', function () {
        $('.tracking_status_container').each(function () {
            var ele = $(this).find('.type_payer')
            if (ele.length > 0) {
                $(this).find('.status_title').css('text-decoration', 'underline');
                $(this).css({ 'box-shadow': '1px 3px 10px 1px #666666' });
            }
        })

        $('.claims_gatway_type').css('opacity', '0.5');
        $('.clearing_house_type').css('opacity', '0.5');
    });


    $('.payer_type').on('mouseleave', function () {
        $('.tracking_status_container').each(function () {
            var ele = $(this).find('.type_payer')
            if (ele.length > 0) {
                $(this).find('.status_title').css('text-decoration', 'none');
                $(this).css({ 'box-shadow': '0px 0px 0px 0px #b6b6b6' });
            }
        })

        $('.claims_gatway_type').css('opacity', '1');
        $('.clearing_house_type').css('opacity', '1');
    });




});