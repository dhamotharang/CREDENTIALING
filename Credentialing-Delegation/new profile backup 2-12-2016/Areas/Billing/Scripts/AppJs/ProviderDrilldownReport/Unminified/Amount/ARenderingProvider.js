/** 
@description Object for creating donut chart
 */

var ARenderingProviderChart = c3.generate({
    bindto: '#amount_renderingProvider_report_donut_chart',
    data: {
        columns: [
            ['Paid', 600],
            ['Pending', 330],
            ['Denied', 605]
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
    selectedRenderingProviderId = id;
    currentPage = 'BillingProviderReport';
    pageDetails[1].CountReport.parameters = [{ key: 'RenderingProviderId', value: id }];
    pageDetails[1].AmountReport.parameters = [{ key: 'RenderingProviderId', value: id }];
    

    pageDetails[1].searchResult = [];
    selectedRenderingProviderSearchString = { key: 'RENDERING PROVIDER', value: $(this).parent().parent().find('.renderingProvider_name').text() };
    pageDetails[1].searchResult.push(selectedRenderingProviderSearchString);

    RenderPage('ClaimedAmount');

});


