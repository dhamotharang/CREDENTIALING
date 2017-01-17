/** 
@description Object for creating donut chart
 */

var CBillingChart = c3.generate({
    bindto: '#count_billingProvider_report_donut_chart',
    data: {
        columns: [
             ['Accepted', 300],
            ['Pending', 200],
            ['Rejected', 100]
        ],
        type: 'donut',
        colors: {
            'Accepted': '#18a689',
            'Pending': '#ff7f0e',
            'Rejected': '#d62728'
        },
        onclick: function (d, i) { console.log("onclick", d, i); },
        onmouseover: function (d, i) { console.log("onmouseover", d, i); },
        onmouseout: function (d, i) { console.log("onmouseout", d, i); }
    },
    donut: {
        title: "Claims Submitted"
    }
});



$('.view_count_billingProvider_btn').on('click', function () {
    var id = $(this).attr('data-container');
    selectedBillingProviderId = id;
    currentPage = 'RenderingProviderReport';
    pageDetails[2].CountReport.parameters = [{ key: 'PayerId', value: selectedPayerId }, { key: 'BillingProviderId', value: id }];
    pageDetails[2].AmountReport.parameters = [{ key: 'PayerId', value: selectedPayerId }, { key: 'BillingProviderId', value: id }];
    
    pageDetails[2].searchResult = [];
    pageDetails[2].searchResult.push(selectedPayerSearchString);
    selectedBillingProviderSearchString = { key: 'BILLING PROVIDER', value: $(this).parent().parent().find('.billingProvider_name').text() };
    pageDetails[2].searchResult.push(selectedBillingProviderSearchString);
    RenderPage('NoOfClaims');

});

$('#cBillingProvider_back_btn').on('click', function () {
    currentPage = 'PayerReport';
    RenderPage('NoOfClaims');
});