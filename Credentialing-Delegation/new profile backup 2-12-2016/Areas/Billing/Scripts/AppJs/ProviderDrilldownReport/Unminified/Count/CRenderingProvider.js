
/** 
@description Object for creating donut chart
 */
var CRenderingProviderChart = c3.generate({
    bindto: '#count_renderingProvider_report_donut_chart',
    data: {
        columns: [
             ['Accepted', 706],
            ['Pending', 2065],
            ['Rejected', 1000]
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
    selectedRenderingProviderId = id;
    currentPage = 'BillingProviderReport';
    pageDetails[1].CountReport.parameters = [{ key: 'RenderingProviderId', value: id }];
    pageDetails[1].AmountReport.parameters = [{ key: 'RenderingProviderId', value: id }];


    pageDetails[1].searchResult = [];
    selectedRenderingProviderSearchString = { key: 'RENDERING PROVIDER', value: $(this).parent().parent().find('.renderingProvider_name').text() };
    pageDetails[1].searchResult.push(selectedRenderingProviderSearchString);
    RenderPage('NoOfClaims');

});

