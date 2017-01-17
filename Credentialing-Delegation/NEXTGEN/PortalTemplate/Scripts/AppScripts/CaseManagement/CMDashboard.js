var reqDistributionChart;
var reqConversionChart;
$(document).ready(function () {
    reqDistributionChart = c3.generate({
        bindto: '#reqDistributionChart',
       data: {
        x: 'x',
        columns: [
            ['x', 'Week 1', 'Week 2', 'Week 3', 'Week 4'],
            ['Week', 4, 3, 7,2,8],
            
        ]
    },
        axis: {
            x: {
                type: 'category',
                categories: ['Week 1', 'Week 2', 'Week 3', 'Week 4']
            },
            y: {
                label: { // ADD
                    text: 'No. Of Requests',
                    position: 'outer-middle'
                }
            },
        },
    });
     
    reqConversionChart = c3.generate({
        bindto: '#reqConversionChart',
        data: {
            x: 'x',
            columns: [
                ['x', 'Week 1', 'Week 2', 'Week 3', 'Week 4'],
                ['CM', 15, 25, 20, 18],
                ['DM', 9, 22, 31, 14],
                ['NC', 30, 20, 20, 40],
                ['Review', 13, 10, 22, 14],
            ],
            type: 'bar',
            groups: [
                ['CM','DM','NC', 'Review']
            ]
        },
        bar : {
            width: {
                ratio: 0.25
            }
        },
        axis: {
            x: {
                type: 'category',
                categories: ['Week 1', 'Week 2', 'Week 3', 'Week 4']
            },
            y: {
                label: { // ADD
                    text: 'No. Of Requests',
                    position: 'outer-middle'
                }
            },
        },
        grid: {
            y: {
                lines: [{ value: 0 }]
            }
        }
    });
   

});
function ChangeRequestDistribution(yaxis) {
    switch (yaxis) {
        case 'Week':
            reqDistributionChart.load({
                columns: [['x', 'Week 1', 'Week 2', 'Week 3', 'Week 4'], ['Week', 4, 3, 7, 2, 8]],
                unload: ['Year', 'Month'],
            });
            break;
        case 'Month':
            reqDistributionChart.load({
                columns: [['x', 'Jan', 'Feb', 'Mar', 'Apr', 'May', 'Jun', 'Jul', 'Aug', 'Sept', 'Oct', 'Nov', 'Dec'], ['Month', 150, 200, 100, 150, 200, 350, 120, 180, 200, 200, 180, 300]],
                unload: ['Week', 'Year'],
            });
            break;
        case 'Year':
            reqDistributionChart.load({
                columns: [['x', '2012-2013', '2013-2014', '2014-2015', '2015-2016'], ['Year', 1000, 3090, 1789, 1009]],
                unload: ['Week', 'Month'],
            });
            break;
    }
}
var ChangeRequestConversion = function (yaxis) {
    switch (yaxis) {
        case 'Week':
            reqConversionChart.load({
                columns: [['x', 'Week 1', 'Week 2', 'Week 3', 'Week 4'], ['CM', 15, 25, 20, 18], ['DM', 9, 22, 31, 14], ['NC', 30, 20, 20, 14], ['Review', 13, 10, 22, 14]],
                unload: ['Year', 'Month'],
            });
            break;
        case 'Month':
            reqConversionChart.load({
                columns:
                    [['x', 'Jan', 'Feb', 'Mar', 'Apr', 'May', 'Jun', 'Jul', 'Aug', 'Sept', 'Oct', 'Nov', 'Dec'], ['CM', 55, 65, 75, 26, 44, 39, 49, 52, 22, 32, 34, 64], ['DM', 65, 35, 37, 29, 64, 43, 44, 35, 22, 32, 24, 64], ['NC', 58, 51, 27, 65, 36, 44, 24, 39, 28, 43, 62, 56], ['Review', 28, 31, 47, 35, 46, 24, 44, 29, 58, 33, 22, 66]],
                unload: ['Week', 'Year'],
            });
            break;
        case 'Year':
            reqConversionChart.load({
                columns: [['x', '2012-2013', '2013-2014', '2014-2015', '2015-2016'], ['CM', 230, 200, 200, 300], ['DM', 130, 120, 85, 140], ['NC', 30, 200, 200, 400], ['Review', 130, 100, 100, 200]],

                unload: ['Week', 'Month'],
            });
            break;
    }
}
