var AvailWidth = $('#OverAllMeanProcessingTimeDetailedGraph').width();
var OverallMeanProcessTimeDG = c3.generate({
    bindto: '#OverAllMeanProcessingTimeDetailedGraph',
    data: {
        x: 'x',
        columns: [
             ['x', '50528 Amanda Larson', '74389 Thomas Dean', '62446 Charles Weaver', '58801 Christine Powell', '87282 Peter Richardson', '66055 Samuel Alvarez', '13162 Jack Griffin', '51028 Jack Ross', '20413 Karen Bell', '93044 Philip Weaver', '76544 Judith Henry', '43555 Annie Rice', '67766 Wanda Pierce', '23422 Louise Lopez', '34233 Paul Olson'],
            ['Previous Year', 30, 57, 35, 23, 20, 50, 20, 10, 40, 15, 57, 35, 23, 20, 50],
            ['Current Year', 43, 45, 50, 42, 29, 34, 54, 36, 47, 48, 54, 38, 58, 23, 30]
        ],
        labels: true
    },
    legend: {
        position: 'inset',
        inset: {
            anchor: 'top-left',
            x: AvailWidth - 175
        }
    },
    axis: {
        x: {
            label: 'Payer ID',
            type: 'category', // this needed to load string x value
            height: 50,
        },
        y: {
            label: 'Day'
        }
    }
});


$('#showPrevious').click(function () {
    $('#overAllMeanTimeInDetail').html('').fadeOut(300);
    var data = loadTableDataForMeanTime("OMT1");

})

function loadTableDataForMeanTime(GetThisData) {
    $.ajax({
        type: 'GET',
        data: { input: GetThisData },
        url: '/ClaimsDashboard/GetData',
        success: function (result) {
            result = JSON.parse(result);
            var data = result.data;
            var trTag = "<tr>"
            var trTagEnd = "</tr>"
            var tdTag = "<td class='col-lg-1'>";
            var tdTagEnd = "</td>"
            var xAxisArray = ['x'];
            var PreviousYearArray = ['Previous Year'];
            var CurrentYearArray = ['Current Year'];

            for (var i = 0; i < data.length; i++) {
                $('#overAllMeanTimeInDetail').append(trTag + tdTag + data[i].PayerId + tdTagEnd
                    + tdTag + data[i].PlanName + tdTagEnd
                    + tdTag + data[i].initialMPT + tdTagEnd
                    + tdTag + data[i].acceptedByCHMPT + tdTagEnd
                    + tdTag + data[i].acceptedByPayerMPT + tdTagEnd
                    + tdTag + data[i].paymentReceivedMPT + tdTagEnd
                    + tdTag + data[i].overall + tdTagEnd).fadeIn(300);

                xAxisArray.push(data[i].PayerId + ' ' + data[i].PayerName);
                PreviousYearArray.push(data[i].overallPreviousYear);
                CurrentYearArray.push(data[i].overall)
            }
            $('.fix_height').height($(window).height() - $('#OverAllMeanProcessingTimeDetailedGraph').height() - 100);
            OverallMeanProcessTimeDG.load({
                columns: [
                    xAxisArray, PreviousYearArray, CurrentYearArray
                ]
            });
        }

    });

}
loadTableDataForMeanTime("OMT1");


$('#showNext').click(function () {
    $('#overAllMeanTimeInDetail').html('').fadeOut(300);
    var data = loadTableDataForMeanTime("OMT2");
})

$('.width_3').height($(window).height());
$('[data-toggle="tooltip"]').tooltip();



$(document).on('click', function (e) {
    var isClose = true;
    if ($(e.target).hasClass('filter_history_dropdown_menu') || $(e.target).attr('id') == 'history_btn') {
        isClose = false;
    } else {
        $(e.target).parents().each(function () {
            if ($(this).hasClass('filter_history_dropdown_menu') || $(this).attr('id') == 'history_btn') {
                isClose = false;
            }
        })
    }

    if (isClose) {
        $('.filter_history_dropdown_menu').find('li').remove();
        $('.filter_history_dropdown_menu').hide();
    }
})




