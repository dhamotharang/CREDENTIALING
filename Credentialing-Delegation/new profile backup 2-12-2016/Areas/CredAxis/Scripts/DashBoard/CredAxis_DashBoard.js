var chart = c3.generate({
    bindto: '#chart',
    data: {
        columns: [
            ['<30 Days', 15, 32, 43, 67, 23, 56, 56, 76, 56],
            ['60-90 Days', 23, 45, 23, 33, 12, 23, 21, 17, 23],
            ['180 Days', 87, 6, 7, 9, 4, 12, 15, 14, 12]
        ],
        type: 'bar',
        groups: [
            ['<30 Days', '60-90 Days']
        ]
    },
    title: {
        text: 'EXPIRIES'
    },
    axis: {
        y: {
            lines: [{ value: 0 }]
        },
        x: {
            type: 'category',
            categories: ['State License', 'DEA', 'CDS', 'Specialty/  Board', 'Hospital Privileges', 'Worker Compensation', 'Medicare', 'Medicaid', 'CAQH']
        }
    },
    legend: {
        show: true,
        position: 'inset',
        inset: {
            anchor: 'top-right',
            x: 430,
            y: -15,
            step: 1
        }
    },
    bar: {
        width: 20
    },
    zoom: {
        enabled: true
    },
    padding: {
        top: 40,
        right: 00,
        bottom: 50,
        left: 35,
    }
});

setTimeout(function () {
    chart.groups([['<30 Days', '60-90 Days', '180 Days']])
}, 1000);
//setTimeout(function () {
//    chart.load({
//        columns: [['data4', 100, -50, 150, 200, -300, -100]]
//    });
//}, 1500);

//setTimeout(function () {
//    chart.groups([['data1', 'data2', 'data3']])
//}, 2000);

var Donut = c3.generate({
    bindto: '#Donut',
    data: {
        columns: [
            ['data1', 30],
            ['data2', 120],
        ],
        type: 'donut',
        onclick: function (d, i) { console.log("onclick", d, i); },
        onmouseover: function (d, i) { console.log("onmouseover", d, i); },
        onmouseout: function (d, i) { console.log("onmouseout", d, i); }
    },
    title: {
        text: 'Profile Completion %'
    },
    legend: {
        show: true,
        position: 'inset',
        inset: {
            anchor: 'top-right',
            x: 40,
            y: 50,
            step: 5
        }
    },
    padding: {
        top: 40,
        right: 00,
        bottom: 50,
        left: 00,
    }
});

setTimeout(function () {
    Donut.load({
        columns: [
            ["<20%", 37],
            ["20%-50%", 30],
            ["50%-80%", 43],
            [">80%", 69],
            ["=100%", 56]
        ]
    });
}, 1000);

setTimeout(function () {
    Donut.unload({
        ids: 'data1'
    });
    Donut.unload({
        ids: 'data2'
    });
}, 1000);