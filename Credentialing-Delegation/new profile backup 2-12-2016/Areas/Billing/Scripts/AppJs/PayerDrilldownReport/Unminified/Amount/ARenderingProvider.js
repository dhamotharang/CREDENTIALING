/** 
@description Object for creating donut chart
 */

var ARenderingProviderChart = c3.generate({
    bindto: '#amount_renderingProvider_report_donut_chart',
    data: {
        columns: [
            ['Paid', 60],
            ['Pending', 300],
            ['Denied', 50]
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


$('.view_amount_renderingProvider_btn').on('click', function () {
    var id = $(this).attr('data-container');
    currentPage = 'MemberReport';
    pageDetails[3].CountReport.parameters = [{ key: 'PayerId', value: selectedPayerId }, { key: 'BillingProviderId', value: selectedBillingProviderId }, { key: 'RenderingProviderId', value: id }];
    pageDetails[3].AmountReport.parameters = [{ key: 'PayerId', value: selectedPayerId }, { key: 'BillingProviderId', value: selectedBillingProviderId }, { key: 'RenderingProviderId', value: id }];
    

    pageDetails[3].searchResult = [];
    pageDetails[3].searchResult.push(selectedPayerSearchString);
    pageDetails[3].searchResult.push(selectedBillingProviderSearchString);
    pageDetails[3].searchResult.push({ key: 'RENDERING PROVIDER', value: $(this).parent().parent().find('.renderingProvider_name').text() });

    RenderPage('ClaimedAmount');

});


$('#aRenderingProvider_back_btn').on('click', function () {
    currentPage = 'BillingProviderReport';
    RenderPage('ClaimedAmount');
});