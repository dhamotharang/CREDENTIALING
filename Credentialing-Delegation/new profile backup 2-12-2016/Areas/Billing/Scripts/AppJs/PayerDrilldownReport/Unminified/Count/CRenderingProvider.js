
/** 
@description Object for creating donut chart
 */
var CRenderingProviderChart = c3.generate({
    bindto: '#count_renderingProvider_report_donut_chart',
    data: {
        columns: [
             ['Accepted', 100],
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

$('.view_count_renderingProvider_btn').on('click', function () {
    var id = $(this).attr('data-container');
    currentPage = 'MemberReport';
    pageDetails[3].CountReport.parameters = [{ key: 'PayerId', value: selectedPayerId }, { key: 'BillingProviderId', value: selectedBillingProviderId }, { key: 'RenderingProviderId', value: id }];
    pageDetails[3].AmountReport.parameters = [{ key: 'PayerId', value: selectedPayerId }, { key: 'BillingProviderId', value: selectedBillingProviderId }, { key: 'RenderingProviderId', value: id }];
    
    pageDetails[3].searchResult = [];
    pageDetails[3].searchResult.push(selectedPayerSearchString);
    pageDetails[3].searchResult.push(selectedBillingProviderSearchString);
    pageDetails[3].searchResult.push({ key: 'RENDERING PROVIDER', value: $(this).parent().parent().find('.renderingProvider_name').text() });


    RenderPage('NoOfClaims');

});

$('#cRenderingProvider_back_btn').on('click', function () {
    currentPage = 'BillingProviderReport';
    RenderPage('NoOfClaims');
});