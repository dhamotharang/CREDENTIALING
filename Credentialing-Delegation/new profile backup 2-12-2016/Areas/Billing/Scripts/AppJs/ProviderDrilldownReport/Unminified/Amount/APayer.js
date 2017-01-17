/** 
@description Object for creating donut chart
 */

var chartCount = c3.generate({
    bindto: '#amount_payer_report_donut_chart',
    data: {
        columns: [
            ['Paid', 80],
            ['Pending', 30],
            ['Denied', 5]
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



$('.view_amount_payer_btn').on('click', function () {
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

    RenderPage('ClaimedAmount');

});

$('#aPayer_back_btn').on('click', function () {
    currentPage = 'BillingProviderReport';
    RenderPage('ClaimedAmount');
});