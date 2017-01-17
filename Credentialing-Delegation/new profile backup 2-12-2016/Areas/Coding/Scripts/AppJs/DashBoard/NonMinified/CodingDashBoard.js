$('.p_headername').html('Coding');

/** 
@description Makes DashBoard Filters Enable and Disable
 */
var CodingDashboardFilter = function () {
    $('.Coding_dashboard_filter').toggle();
}

/** 
@description generates a C3 line graph for Coding Productivity (displaying count on Y-axis and Date on X-axis)
 */
var chart = c3.generate({
    bindto: '#codingcountgraph',
    data: {
        x: 'x',
        columns: [
              ['x', '08Sep16 Thu', '09Sep16 Fri', '10Sep16 Sat', '11Sep16 Sun', '12Sep16 Mon', '13Sep16 Tue', '14Sep16 Wed'],
              ['Ready to Audit', 20, 12, 19, 15, 21, 4, 12],
              ['On-Hold', 12, 10, 5, 7, 8, 4, 7],

        ],
    },
    legend: {
        position: 'inset',
        inset: {
            anchor: 'top-right',
        }
    },
    axis: {
        x: {
            label: {
                text: '---> Day',
                position: 'outer-center'
            },
            type: 'category',
            tick: {
                rotate: 0,
                multiline: true
            },
            height: 50
        },
        y: {
            label: {
                text: '---> Count',
                position: 'outer-middle'
            },
        }
    },

});

var percentages = [10, 15, 40, 20, 10, 15, 40, 45, 33, 25, 56, 12, 10, 18, 19, 15, 14];

/** 
@description generates a Stack Bar graph (C3 Charts) for Top 5 EnM Codes (Agree Count, UnderCoding Count, OverCoding Count)
 */
var chart3 = c3.generate({
    bindto: '#EnMGraph',
    data: {
        x: 'x',
        columns: [
        ['x', 99210, 99211, 99212, 99213, 99214, 99216, 99217, 99218, 99219, 99220, 99221, 99222, 99223, 99224, 99225],
        ['Agree', 10, 18, 19, 15, 14, 30, 25, 24, 13, 12, 24, 20, 10, 15, 40],
        ['UnderCoding', 14, 30, 25, 25, 5, 12, 10, 18, 19, 15, 14, 5, 25, 24, 13],
        ['OverCoding', 10, 15, 40, 20, 10, 15, 40, 45, 33, 25, 56, 12, 10, 18, 19]],

        type: 'bar',
        groups: [
            ['Agree', 'UnderCoding', 'OverCoding']
        ],
        labels: true,
        order: 'desc',

        legend: {
            position: 'inset',
            inset: {
                anchor: 'top-right',
            }
        },
    },
    axis: {
        x: {
            label: {
                text: '---> CPT Codes',
                position: 'outer-center'
            },
            type: 'category',
            tick: {
                rotate: 0,
                multiline: true
            },
            height: 50,
        },
        y: {
            label: {
                text: '---> Count',
                position: 'outer-middle'
            },
            tick: {
                format: function (d) {
                    return d;
                }
            }
        },
    },
    color: {
        pattern: ['#66b553', '#ff7f0e', '#1f77b4']
    },

    tooltip: {
        format: {
            title: function () { return "Percentage: 65%"; },
        }
    }
});