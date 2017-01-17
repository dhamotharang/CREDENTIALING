var HighCostClaimBarGraph = c3.generate({
    bindto: '#HighCostClaimsDetailedReportFinancialYear',
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




function loadHighCostClaimDetailReport(GetThisData) {
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
            var Cost = ['Cost'];

            for (var i = 0; i < data.length; i++) {
                $('#HighCostClaimDRTable').append(trTag + tdTag + data[i].PatientInsuranceId + tdTagEnd
                    + tdTag + data[i].PatientName + tdTagEnd
                    + tdTag + data[i].npiNumber + tdTagEnd
                    + tdTag + data[i].ProviderName + tdTagEnd
                    + tdTag + data[i].Cost + tdTagEnd
                    + tdTag + data[i].Payer + tdTagEnd
                    + tdTag + data[i].DOS + tdTagEnd +
                    tdTag + data[i].E_M + tdTagEnd).fadeIn(300);

                xAxisArray.push(data[i].npiNumber + ' ' + data[i].ProviderName);
                Cost.push(data[i].Cost);
            }
            $('.fix_height').height($(window).height() - $('#HighCostClaimsDetailedReportFinancialYear').height() - 100);
            HighCostClaimBarGraph.load({
                columns: [
                    xAxisArray, Cost
                ]
            });
        }

    });

}
loadHighCostClaimDetailReport("HCC1");


$('#showPrevious').click(function () {
    $('#HighCostClaimDRTable').html('').fadeOut(300);
    var data = loadHighCostClaimDetailReport("HCC1");

})


$('#showNext').click(function () {
    $('#HighCostClaimDRTable').html('').fadeOut(300);
    var data = loadHighCostClaimDetailReport("HCC2");
})

$('.width_3').height($(window).height())
$('[data-toggle="tooltip"]').tooltip();



$(document).ready(function () {

    //$('.filter_btn').showFilter();

    //-------------------------master filter----------------------------

    $('#master_filter1').on('click', function () {
        $('.tracking-filter-form').show();
        $('.search_by_titles1').hide();
        $('.master_filter_new1').hide();
        $('.master_filter_apply1').show();
        $('.master_filter_claims').slideDown();

    })

    $('.close_master_filter_btn1').on('click', function () {
        $('.master_filter_claims').slideUp();
    });

    $('.history_btn1').on('click', function () {

        $.ajax({
            type: 'GET',
            url: '/ClaimsDashboard/GetFilterHistory',
            success: function (data) {
                $('.filter_history_dropdown_menu').html(data);
                $('.filter_history_dropdown_menu').slideDown();
            }
        });


    });


    //function ViewFilteredHistory(filterID) {
    //    $.ajax({
    //        type: 'GET',
    //        url: '/ClaimsDashboard/GetSelectedFavFilter?id=' + filterID,
    //        success: function (data) {
    //            $('#search_by_titles').html(data);
    //            $('.filter_history_dropdown_menu').find('li').remove();
    //            $('.filter_history_dropdown_menu').hide();

    //            $('#master_filter_apply').show();
    //            $('#master_filter_new').show();
    //            $('.tracking-filter-form').hide();
    //            $('#search_by_titles').show();
    //        }
    //    });

    //}


    //function ApplyFilteredHistory(filterID) {
    //    $.ajax({
    //        type: 'GET',
    //        url: '/ClaimsDashboard/GetSelectedFavFilter?id=' + filterID,
    //        success: function (data) {
    //            $('#search_by_titles').html(data);
    //            $('.filter_history_dropdown_menu').find('li').remove();
    //            $('.filter_history_dropdown_menu').hide();

    //            $('#master_filter_apply').hide();
    //            $('#master_filter_new').show();
    //            $('.tracking-filter-form').hide();
    //            $('#search_by_titles').show();


    //            ChangeDashboardDataDummy();
    //        }
    //    });

    //}


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


    //function dragStart(event) {
    //    event.dataTransfer.setData("Text", event.target.id);
    //    $(event.currentTarget).parent().addClass('drag_source');
    //}

    //function allowDrop(event) {
    //    event.preventDefault();
    //    $('.drop_target').removeClass('drop_target');
    //    $(event.currentTarget).addClass('drop_target');
    //    $('.claims_report_list').css('z-index', '12');
    //}

    //function drop(event) {
    //    event.preventDefault();
    //    filterChartsWithAjax(event.dataTransfer.getData("Text"), $(event.currentTarget).attr('data-url'));
    //    $('.drag_source').removeClass('drag_source');
    //    $('.drop_target').removeClass('drop_target');
    //    $('.claims_report_list').removeAttr('style');
    //}


    //function EditFavFilterName() {

    //    var value = $('#filterName_span').text();
    //    $('#filterName_span').html('<input type="text" name="favFilter" value="' + value + '" />');
    //    $('.edit_filterName').hide();
    //    $('.save_filterName').show();
    //}

    //function SaveFavFilterName() {
    //    var value = $('input[name="favFilter"]').val();
    //    $('#filterName_span').html(value);
    //    $('.edit_filterName').show();
    //    $('.save_filterName').hide();
    //}

    //function ApplyMasterFilter() {
    //    $('.tracking-filter-form').hide();
    //    $('#master_filter_new').show();
    //    $('#master_filter_apply').hide();

    //    ChangeDashboardDataDummy();
    //};

    //function NewMasterFilter() {
    //    $('.tracking-filter-form').show();
    //    $('#master_filter_new').hide();
    //    $('#master_filter_apply').show();
    //    $('#search_by_titles').hide();
    //}

    //function filterChartsWithAjax(filterId, chartId) {

    //    filterChartsWithAjaxdummy(filterId, chartId);
    //}
});
