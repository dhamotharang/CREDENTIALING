/** 
@description Object for creating donut chart
 */

var chartCount = c3.generate({
    bindto: '#amount_payer_report_donut_chart',
    data: {
        columns: [
            ['Paid', 567],
            ['Pending', 100],
            ['Denied', 100]
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
    currentPage = 'BillingProviderReport';
    pageDetails[1].CountReport.parameters = [{ key: 'PayerId', value: id }];
    pageDetails[1].AmountReport.parameters = [{ key: 'PayerId', value: id }];
    
    selectedPayerSearchString = { key: 'PAYER', value: $(this).parent().parent().find('.payer_name').text() };
    pageDetails[1].searchResult = [];
    pageDetails[1].searchResult.push(selectedPayerSearchString);

    RenderPage('ClaimedAmount');

});
