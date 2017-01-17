$('.ReportSubTable').find('.lowerInfoArea td').css('display', 'none');
$('.ReportSubTable').off('click', 'tbody tr td a.expand').on('click', 'tbody tr td a.expand', function () {
    toggleTableRow(this);
});

function toggleTableRow(object) {
    if ($(object).parent().parent().hasClass('open')) {
        $(object).parent().parent().removeClass('open').addClass('closed');
        $(object).find('i').removeClass('fa-angle-double-up').addClass('fa-angle-double-down');
        $(object).parent().parent().next().find('td').css('display', 'none');
    }
    else {
        $(object).parent().parent().removeClass('closed').addClass('open');
        $(object).find('i').removeClass('fa-angle-double-down').addClass('fa-angle-double-up');
        $(object).parent().parent().next().find('td').css('display', 'table-cell');
    }
}

var profileDocsChart = c3.generate({
    bindto: '#ProfileDocsGraph',
    data: {
        columns: [
            ['Available', 3, 6, 2, 1, 3, 10],
            ['Missing', 1, 4, 0, 3, 1, 5]
        ],
        type: 'bar',
        groups: [
            ['Available', 'Missing']
        ]
    },
    grid: {
        y: {
            lines: [{ value: 0 }]
        }
    },
    axis: {
        x: {
            type: 'category',
            categories: ['State Licenses', 'Driving License', 'CV', 'DEA', 'Medicare', 'Medicaid']
        }
    }
});

var PSVChart = c3.generate({
    bindto: '#PSVChart',
    data: {
        columns: [
            ['Passed', 10, 2, 3, 7, 5, 5],
            ['Failed', 8, 0, 1, 3, 8, 9]
        ],
        type: 'bar',
        groups: [
            ['Passed', 'Failed']
        ]
    },
    grid: {
        y: {
            lines: [{ value: 0 }]
        }
    },
    axis: {
        x: {
            type: 'category',
            categories: ['21/11/2016', '25/11/2016', '28/11/2016', '29/11/2016', '01/12/2016', '02/12/2016']
        }
    }
});

var GeneratedFormsChart = c3.generate({
    bindto: '#GeneratedFormsChart',
    data: {
        columns: [
            ['Submitted', 10],
            ['Not Submitted', 10],
        ],
        type: 'donut',
        onclick: function (d, i) { console.log("onclick", d, i); },
        onmouseover: function (d, i) { console.log("onmouseover", d, i); },
        onmouseout: function (d, i) { console.log("onmouseout", d, i); }
    },
    donut: {
        title: "Generated Forms"
    }
});

var FormsChart = c3.generate({
    bindto: '#FormsChart',
    data: {
        columns: [
            ['Provider Related Forms', 5],
            ['Ultimate Forms', 3],
            ['Freedom Forms', 2]
        ],
        type: 'donut',
        onclick: function (d, i) { console.log("onclick", d, i); },
        onmouseover: function (d, i) { console.log("onmouseover", d, i); },
        onmouseout: function (d, i) { console.log("onmouseout", d, i); }
    },
    donut: {
        title: "Forms"
    }
});