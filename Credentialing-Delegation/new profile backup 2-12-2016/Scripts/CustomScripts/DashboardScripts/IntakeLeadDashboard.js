$(document).ready(function () {
    var monthNames = ["January", "February", "March", "April", "May", "June",
  "July", "August", "September", "October", "November", "December"];
    var chart = c3.generate({
        bindto: '#chart',
        data: {
            x: 'x',
            columns: [
                ['x', "January", "February", "March", "April", "May", "June",
    "July", "August", "September", "October", "November", "December"],
               ['Standard', 130, 80, 200, 250, 400, 150, 250, 130, 80, 200, 250],
                ['Expedited', 60, 30, 100, 90, 200, 150, 50, 60, 30, 100, 90]
            ],

            type: 'bar',
            groups: [
                ['Standard', 'Expedited']
            ]

        },
        bar: {
            width: {
                ratio: 0.25 // this makes bar width 50% of length between ticks
            }
            // or
            //width: 100 // this makes bar width 100px
        },
        axis: {
            x: {
                type: 'category',
                categories: monthNames
                //type: 'timeseries',
                //tick: {
                //    format: function (x) { return x.getDate() + "th" + monthNames[x.getMonth()]; }
                //    //format: '%Y' // format string is also available for timeseries data
                //}
            }
        },

        grid: {

            y: {
                lines: [{ value: 0 }]
            }
        }, color: {
            pattern: ['#d58512', '#3C8DBC', '#ff7f0e', '#ffbb78', '#2ca02c', '#98df8a', '#d62728', '#ff9896', '#9467bd', '#c5b0d5', '#8c564b', '#c49c94', '#e377c2', '#f7b6d2', '#7f7f7f', '#c7c7c7', '#bcbd22', '#dbdb8d', '#17becf', '#9edae5']
        }
    });
});