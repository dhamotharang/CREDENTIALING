/** 
@description Object for creating donut chart
 */

var ABillingChart = c3.generate({
    bindto: '#amount_billingProvider_report_donut_chart',
    data: {
        columns: [
            ['Paid', 800],
            ['Pending', 300],
            ['Denied', 500]
        ],
        type: 'donut',
        colors: {
            'Paid': '#18a689',
            'Pending': '#ff7f0e',
            'Denied': '#d62728'
        },
        onclick: function (d, i) { console.log("onclick", d, i); },
        onmouseover: function (d, i) { console.log("onmouseover", d, i); },
        onmouseout: function (d, i) { console.log("onmouseout", d, i); }
    },
    donut: {
        title: "Amount Billed"
    }
});


$('.view_amount_billingProvider_btn').on('click', function () {
    var id = $(this).attr('data-container');
    currentPage = 'RenderingProviderReport';
    selectedBillingProviderId = id;
    pageDetails[2].CountReport.parameters = [{ key: 'PayerId', value: selectedPayerId }, { key: 'BillingProviderId', value: id }];
    pageDetails[2].AmountReport.parameters = [{ key: 'PayerId', value: selectedPayerId }, { key: 'BillingProviderId', value: id }];
    pageDetails[2].searchResult = [];
    pageDetails[2].searchResult.push(selectedPayerSearchString);
    selectedBillingProviderSearchString = { key: 'BILLING PROVIDER', value: $(this).parent().parent().find('.billingProvider_name').text() };
    pageDetails[2].searchResult.push(selectedBillingProviderSearchString);
    RenderPage('ClaimedAmount');

});

$('#aBillingProvider_back_btn').on('click', function () {
    currentPage = 'PayerReport';
    RenderPage('ClaimedAmount');
});