var MeanTimeChart = null;

$(function () {
    //--------------------------Mean process time---------------------------
    var AvailWidth = $('#MeanProcessTimeReport').width();
     MeanTimeChart = c3.generate({
        bindto: '#MeanProcessTimeReport',
        data: {
            x: 'x',
            columns: [
                 ['x', '6040 Anne Lee', '9918 Ann Ray', '9685 Carl Fields', '7055 Janet Boyd', '6483 Stephen Freeman'],
                ['Previous Year', 30, 40, 55, 65, 150],
                ['Current Year', 10, 30, 45, 80, 118]
            ],
            labels: true
        },
        legend: {
            position:'inset',
            inset: {
                anchor: 'top-left',
                x: AvailWidth-200
            }
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
