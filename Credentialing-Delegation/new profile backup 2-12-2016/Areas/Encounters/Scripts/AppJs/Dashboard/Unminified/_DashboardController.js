$('.p_headername').html('Encounters');

/******** Initialize The Calendar **************/
 pCalendarController('#pCalendar');
// can create like var name = new pCalen.....(...); to use 'name' in future

function DashboardFilter() {
    $('#showfilter').toggle();

}

var chart1 = c3.generate({
    bindto: '#chart1',
    data: {
        x: 'x',
        columns: [
              ['x', '08Sep16 Thu', '09Sep16 Fri', '10Sep16 Sat', '11Sep16 Sun', '12Sep16 Mon', '13Sep16 Tue', '14Sep16 Wed'],
              ['Ready to Code', 20, 12, 19, 15, 21, 4, 12],
              ['On-Hold', 12, 10, 5, 7, 8, 4, 7]
        ],
        labels: true,

        selection: {
            enabled: true,
            multiple: false,
        },
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
                position: 'outer-middle'
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
                text: '---> No. of Encounters',
                position: 'outer-middle'
            }
        }
    }
});


/*******Linear Graph(Encounters ON-HOLD)********/
var chart3 = c3.generate({
    bindto: '#chart3',
    data: {

        columns: [

          ['Encounters', 5, 12, 30]

        ],
    },

    axis: {

        y: {
            label: { // ADD
                text: 'No. of Encounters',
                position: 'outer-middle'
            }
        },
    }
});

/*******Bar Graph(Top 5 Reasons for ON-HOLD)********/
var chart2 = c3.generate({
    bindto: '#chart2',
    data: {
        columns: [
          ['Encounters', 20, 12, 14, 50, 5]
        ],
        types: {
            Encounters: 'bar'
        }
    },
    axis: {
        y: {
            label: {
                text: 'No. of Encounters',
                position: 'outer-middle'
            },
        },
    }
});


