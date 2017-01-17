function loadTableDataForProcedureClaimed(GetThisData) {
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
            for (var i = 0; i <= 30 ; i++) {
                $('#ProcedureClaimedDR').append(trTag + tdTag + data[i].Procedure_Code + tdTagEnd
                    + tdTag + data[i].Procedure_Code_Description + tdTagEnd
                    + tdTag + data[i].Procedure_Count + tdTagEnd
                    + tdTag + data[i].Procedure_Percentage + tdTagEnd
                    + tdTag + data[i].Provider_Count + tdTagEnd
                    + tdTag + data[i].Patient_Count + tdTagEnd).fadeIn(300);
            }
            //$('.fix_height').css({ height: ($(window).height()) + 'px' });
        }

    });

}
loadTableDataForProcedureClaimed("PC1");
$('#showPrevious').click(function () {
    $('#ProcedureClaimedDR').html('').fadeOut(300);
    var data = loadTableDataForProcedureClaimed("PC1");

})


$('#showNext').click(function () {
    $('#ProcedureClaimedDR').html('').fadeOut(300);
    var data = loadTableDataForProcedureClaimed("PC2");
})


$('.width_3 ').height($(window).height())

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
