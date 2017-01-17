/** 
@description Object for creating donut chart
 */
var chartCount = c3.generate({
    bindto: '#amount_member_report_donut_chart',
    data: {
        columns: [
            ['Paid', 700],
            ['Pending', 200],
            ['Denied', 100]
        ],
        type: 'donut',
        colors: {
            'ACCEPTED': '#18a689',
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

$('#aMember_back_btn').on('click', function () {
    currentPage = 'RenderingProviderReport';
    RenderPage('ClaimedAmount');
});