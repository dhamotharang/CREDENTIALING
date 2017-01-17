var HighCostClaimsChart = null;

$(function () {
    //------------------------High cost claims report-------------------------

     HighCostClaimsChart = c3.generate({
        bindto: '#HighCostClaimsReport',
        data: {
            x: 'x',
            columns: [
                 ['x', 'P20160802C2 P singh', 'P20160802C1 V manju', 'P20160801C7 Roy', 'P20160801C6 Michal', 'P20160801C5 Joe'],
                ['Cost', 500, 400, 300, 200, 100]
            ],
            labels: true,
            type: 'bar'
        },
        legend: {
            show: false
        },
        bar: {
            width: {
                ratio: 0.5 // this makes bar width 50% of length between ticks
            }
            // or
            //width: 100 // this makes bar width 100px
        },
        axis: {
            x: {
                label: 'Claim ID',
                type: 'category', // this needed to load string x value
                tick: {
                    rotate: 0,
                    multiline: true
                },
                height: 50
            },
            y: {
                label: 'Amount($)'
            }
        }
    });

})