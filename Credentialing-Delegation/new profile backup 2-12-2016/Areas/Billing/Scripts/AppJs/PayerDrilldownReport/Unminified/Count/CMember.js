/** 
@description Object for creating donut chart
 */
var chartMemberCount = c3.generate({
    bindto: '#count_member_report_donut_chart',
    data: {
        columns: [
            ['Accepted', 1000],
            ['Pending', 2000],
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

$('#cMember_back_btn').on('click', function () {
    currentPage = 'RenderingProviderReport';
    RenderPage('NoOfClaims');
});