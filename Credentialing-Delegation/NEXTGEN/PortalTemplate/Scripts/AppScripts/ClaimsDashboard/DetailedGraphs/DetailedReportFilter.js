

var payerObejct = [{ id: 1, ListName: 'Access2', IsChecked: false }, { id: 2, ListName: 'Access2 Tampa', IsChecked: false }, { id: 3, ListName: 'All American Phy', IsChecked: false }];
$('input[name="payerFilter"]').detailedMultiselectSearch(payerObejct, 'Payer');


var billingProviderObject = [{ id: 1, ListName: 'Dina Amundsen', IsChecked: false }, { id: 2, ListName: 'Sheila Trissel', IsChecked: false }, { id: 3, ListName: 'Tony Martin', IsChecked: false }, { id: 4, ListName: 'Marrisa Valenti', IsChecked: false }, { id: 5, ListName: 'Carpi Ritch', IsChecked: false }];
$('input[name="BillingProviderFilter"]').detailedMultiselectSearch(billingProviderObject, 'Billing Provider');

var renderingProviderObject = [{ id: 1, ListName: 'Aasma Riaz', IsChecked: false }, { id: 2, ListName: 'Rajyalakshmi Kolli', IsChecked: false }, { id: 3, ListName: 'Gary Merlino', IsChecked: false }, { id: 4, ListName: 'Bernard Kurecki III', IsChecked: false }];
$('input[name="RenderingProviderFilter"]').detailedMultiselectSearch(renderingProviderObject, 'Rendering Provider');

var claimTypeObject = [{ id: 1, ListName: 'CAP', IsChecked: false }, { id: 2, ListName: 'FFS', IsChecked: false }, { id: 3, ListName: 'UB04', IsChecked: false }];
$('input[name="ClaimTypeFilter"]').detailedMultiselectSearch(claimTypeObject, 'Claim Type');

var billingTeamObject = [{ id: 1, ListName: 'Aasma Riaz', IsChecked: false }, { id: 2, ListName: 'Rajyalakshmi Kolli', IsChecked: false }, { id: 3, ListName: 'Gary Merlino', IsChecked: false }, { id: 4, ListName: 'Bernard Kurecki III', IsChecked: false }];
$('input[name="BillingTeamFilter"]').detailedMultiselectSearch(billingTeamObject, 'Billing Team');

var billerObject = [{ id: 1, ListName: 'Aasma Riaz', IsChecked: false }, { id: 2, ListName: 'Rajyalakshmi Kolli', IsChecked: false }, { id: 3, ListName: 'Gary Merlino', IsChecked: false }, { id: 4, ListName: 'Bernard Kurecki III', IsChecked: false }];
$('input[name="BillerFilter"]').detailedMultiselectSearch(billerObject, 'Biller');


//$('#dos_details').daterangepicker({
//    autoUpdateInput: false,
//    "showDropdowns": true,
//    "showWeekNumbers": true,
//    "showISOWeekNumbers": true,
//    "locale": {
//        "format": "MM/DD/YYYY",
//        "separator": " - ",
//        "applyLabel": "Apply",
//        "cancelLabel": "Cancel",
//        "fromLabel": "From",
//        "toLabel": "To",
//        "customRangeLabel": "Custom",
//        "weekLabel": "W",
//        "daysOfWeek": [
//            "Su",
//            "Mo",
//            "Tu",
//            "We",
//            "Th",
//            "Fr",
//            "Sa"
//        ],
//        "monthNames": [
//            "January",
//            "February",
//            "March",
//            "April",
//            "May",
//            "June",
//            "July",
//            "August",
//            "September",
//            "October",
//            "November",
//            "December"
//        ],
//        "firstDay": 1
//    }

//}, function (start, end, label) {

//    selectedFilterList.push({ labelName: 'DOS', items: [start.format('MM/DD/YYYY') + ' to ' + end.format('MM/DD/YYYY')] });
//    var dropTemplate = '<div class="clearfix">';
//    for (var i = 0; i < selectedFilterList.length; i++) {
//        dropTemplate = dropTemplate + '<span> ' + selectedFilterList[i].labelName + ' :</span><span>'
//        for (var j = 0; j < selectedFilterList[i].items.length; j++) {
//            dropTemplate = dropTemplate + '<span  class="badge badge-success filter_text_badge">' + selectedFilterList[i].items[j] + '</span><span>/</span>'
//        }
//        dropTemplate = dropTemplate + '</span>';
//    }
//    dropTemplate = dropTemplate + '<a class="pull-right btn" id="fav_filter_add"><i style="font-size: 15px" class="fa fa-heartbeat"></i></a></div>';
//    $('#search_by_titles_detailed').html(dropTemplate);
//    $('#search_by_titles_detailed').show();

//});

//$('#dos_details').on('apply.daterangepicker', function (ev, picker) {
//    $(this).val(picker.startDate.format('MM/DD/YYYY') + ' - ' + picker.endDate.format('MM/DD/YYYY'));
//});

//$('#dos_details').on('cancel.daterangepicker', function (ev, picker) {
//    $(this).val('');
//});


$('#master_filter_overall_filter').on('click', function () {
    $('#search_by_titles_detailed').hide();
    $('#detailed_filter_new').hide();
    $('#detailed_filter_apply').show();
    $('#detailed_filter_claims').slideToggle();
    selectedFilterList = [];



});


$('#close_detailed_filter_btn').on('click', function () {
    $('#detailed_filter_claims').slideUp();
});

function ApplyDetailReportFilter() {
    $('.tracking-filter-form').hide();
    $('#detailed_filter_new').show();
    $('#detailed_filter_apply').hide();

};

function NewDetailReportFilter() {
    $('.tracking-filter-form').show();
    $('#detailed_filter_new').hide();
    $('#detailed_filter_apply').show();
    $('#search_by_titles').hide();
}

$('#history_btn_details').on('click', function () {

    $.ajax({
        type: 'GET',
        url: '/ClaimsDashboard/GetFilterHistoryForDetailedReport',
        success: function (data) {
            $('#filter_history_dropdown_menu_id').html(data);
            $('#filter_history_dropdown_menu_id').slideDown();
        }
    });


});
