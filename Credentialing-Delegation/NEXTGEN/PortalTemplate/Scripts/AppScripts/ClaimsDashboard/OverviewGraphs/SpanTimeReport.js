var SpanTimeChart = null;

//-------------------------Span time report--------------------------------
$(function () {

   SpanTimeChart = c3.generate({
        bindto: '#SpanTimeReport',
        data: {
            x: 'x',
            columns: [
                 ['x', '930449304 P Singh', '20412041 V Manju', '510285102 Roy', '660556605 Joe', '362733627 Heny'],
                     ['Current Year', 10, 22, 29, 38, 45]
            ],
            labels: true
        },
        legend: {
            show: false
        },
        axis: {
            x: {
                label: 'Payer ID',
                type: 'category', // this needed to load string x value
                tick: {
                    rotate: 0,
                    multiline: true
                },
                height: 50
            },
            y: {
                label: 'Day'
            }
        }
    });

})