
/** 
@description Object for creating donut chart
 */
var chartPayerCount = c3.generate({
    bindto: '#count_payer_report_donut_chart',
    data: {
        columns: [
            ['Accepted', 70],
            ['Pending', 20],
            ['Rejected', 10]
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


$('.view_count_payer_btn').on('click', function () {
    var id = $(this).attr('data-container');
    selectedPayerId = id;
    currentPage = 'MemberReport';
    pageDetails[3].CountReport.parameters = [{ key: 'RenderingProviderId', value: selectedRenderingProviderId }, { key: 'BillingProviderId', value: selectedBillingProviderId }, { key: 'PayerId', value: id }];
    pageDetails[3].AmountReport.parameters = [{ key: 'RenderingProviderId', value: selectedRenderingProviderId }, { key: 'BillingProviderId', value: selectedBillingProviderId }, { key: 'PayerId', value: id }];
    pageDetails[3].searchResult = [];
    pageDetails[3].searchResult.push(selectedRenderingProviderSearchString);
    pageDetails[3].searchResult.push(selectedBillingProviderSearchString);

    selectedPayerSearchString = { key: 'PAYER', value: $(this).parent().parent().find('.payer_name').text() };
    pageDetails[3].searchResult.push(selectedPayerSearchString);



    RenderPage('NoOfClaims');

});

$('#cPayer_back_btn').on('click', function () {
    currentPage = 'BillingProviderReport';
    RenderPage('ClaimedAmount');
});