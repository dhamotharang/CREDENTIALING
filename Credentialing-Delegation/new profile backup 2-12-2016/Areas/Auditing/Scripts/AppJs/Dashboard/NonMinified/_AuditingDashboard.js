$('.p_headername').html('Auditing');

/** 
@description It shows and hides  DashBoard Filters
 */
$('#AuditingDashboard').off('click','#filterData').on('click','#filterData',function(){
    $('.Auditing_dashboard_filter').toggle();
})


/** 
@description generates a C3 line graph for Organization Accuracy Report (displaying count on Y-axis and Date on X-axis)
 */
var chart = c3.generate({
    bindto: '#Auditing_organisationreport',
    data: {
        x: 'x',
        columns: [
              ['x', '08Sep16 Thu', '09Sep16 Fri', '10Sep16 Sat', '11Sep16 Sun', '12Sep16 Mon', '13Sep16 Tue', '14Sep16 Wed'],
              ['Accuracy', 86, 92, 87, 86, 84, 87, 88],
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
                text: '---> Accuracy',
                position: 'outer-middle'
            }
        }
    }
});

var providersaccuracy = [{ NAME: "Andrew Springer", Accuracy: "47.46" },
                         { NAME: "Susan Schickley", Accuracy: "52.78" },
                         { NAME: "Bhupatiraju Raju", Accuracy: "57.35" },
                         { NAME: "Vinod Raxwal", Accuracy: "59.23" },
                         { NAME: "Sharona Loewenstein", Accuracy: "60.27" },
                         { NAME: "Ivan Diaz", Accuracy: "63.29" },
                         { NAME: "Lingappa Amarchand", Accuracy: "63.56" },
                         { NAME: "Daria Mazzoni", Accuracy: "67.5" },
                         { NAME: "Bassam Radwan", Accuracy: "68.29" },
                         { NAME: "Luke Kung", Accuracy: "69.05" },
                         { NAME: "Apurva Shah", Accuracy: "95.12" },
                         { NAME: "Husam Abuzarad", Accuracy: "95.27" },
                         { NAME: "Brian Kroll", Accuracy: "95.35" },
                         { NAME: "Kevin Bass", Accuracy: "96.12" },
                         { NAME: "Luis Jovel", Accuracy: "96.15" },
                         { NAME: "Brian McCarthy", Accuracy: "97.66" },
                         { NAME: "Farrukh Zaidi", Accuracy: "100" },
                         { NAME: "Muhammad Mughni", Accuracy: "100" },
                         { NAME: "Aziz Alkhafaji", Accuracy: "100" },
                         { NAME: "Maulik Bhalani", Accuracy: "100" }];


/** 
@description appends the Provider Name and Accuracy Percentage to the table (Provider Accuracy Report)
 */
$('#Auditing_reasonsforRejection').empty();
$.each(providersaccuracy, function (index, data) {
    $('#Auditing_reasonsforRejection').append(
                          '<tr>' +
                          '<td>' + data.NAME + '</td>' +
                          '<td>' + data.Accuracy + ' %' + '</td>' +
                          '</tr>'
                  );
});
