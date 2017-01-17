
var htmlContent = null;

var selectedFilterList = [];

var selectedSlaveFilterList = [];

var tooltipArray = [];

function CloseFullScreen(targetID) {
    $('body').css("overflow", "scroll");
    $('#' + targetID).find('.detailed_view').remove();
    $('#' + targetID).find('.small_view').fadeIn('slow');
    $('#' + targetID).animate({ 'width': currentDetailedViewParameters.width + 'px' }, 700).animate({ 'height': currentDetailedViewParameters.height + 'px', 'top': currentDetailedViewParameters.top + 'px', 'left': currentDetailedViewParameters.left + 'px' }, 700, function () {

        $('#' + targetID).removeAttr('style');

    });
}


var currentDetailedViewParameters = {};

$(document).ready(function () { 
    //-------------------------- sortable----------------------------

    $("#sortable").sortable({
        group: 'no-drop',
        containment: 'parent',
        revert: 50,
        handle: 'a.drag_btn',
        helper: 'clone',
        forcePlaceholderSize: true,
        create: function () {
            var list = this;
            resize = function () {
                jQuery(list).css("height", "auto");
                jQuery(list).height(jQuery(list).height());
            };
            jQuery(list).height(jQuery(list).height());
        },
        sort: function (event, ui) {
            var $target = $(event.target);
            if (!/html|body/i.test($target.offsetParent()[0].tagName)) {
                var top = event.pageY - $target.offsetParent().offset().top - (ui.helper.outerHeight(true) / 2);
                ui.helper.css({ 'top': top + 'px' });
            }
        },
        start: function (event, ui) {
            var $target = $(event.target);
            var top = event.pageY - $target.offsetParent().offset().top - (ui.helper.outerHeight(true) / 2);
            ui.helper.css({ 'top': top + 'px' });

        }
    });



    //---------------------------------detailed report-----------------------------------


    function MakeItFullScreen(targetDivId, targetUrl) {
        currentDetailedViewParameters = { height: $('#' + targetDivId).height(), width: $('#' + targetDivId).width(), top: $('#' + targetDivId).offset().top - $(window).scrollTop(), left: $('#' + targetDivId).offset().left };
        htmlContent = $('#' + targetDivId).html();
        $('body').css("overflow", "hidden");
        $('#' + targetDivId).css({ 'position': 'fixed', 'z-index': '1000000', 'overflow': 'scroll', 'width': currentDetailedViewParameters.width, top: currentDetailedViewParameters.top });
        $('#' + targetDivId).animate({
            'top': '43px',
            'left': '0px'
        }, 500).animate({
            'width': '100%',
            'height': '100%'
        }, 500, function () {
            $.ajax({
                type: 'GET',
                url: '/ClaimsDashboard/GetPartialView?url=' + targetUrl,
                success: function (data) {
                    $('#' + targetDivId).find('.small_view').hide();
                    $('#' + targetDivId).append(data);

                }
            });

        });

    }


    $('#Show_Detailed_Report_Btn').click(function () {
        MakeItFullScreen('OverAllMeanProcessTimeId', "~/Views/ClaimsDashboard/_OverAllMeanProcessTimeDetailedReport.cshtml");
    });

    $('#Show_Detailed_Report_HCC').click(function () {
        MakeItFullScreen('HighCostClaims', "~/Views/ClaimsDashboard/_HighCostClaimFinancialYear.cshtml");
    });

    $('#Show_Detailed_Report_PC').click(function () {
        MakeItFullScreen('ProceduresClaimed', "~/Views/ClaimsDashboard/_ProceduresClaimedFinancialYear.cshtml");
    });

    $('#Show_Detailed_Report_MCD').click(function () {
        MakeItFullScreen('MostCommonDisease', "~/Views/ClaimsDashboard/_MostCommonDisesesFinancialYear.cshtml");
    });

    $('#Show_Detailed_Report_PenAdj').click(function () {
        MakeItFullScreen('PenalityFY', "~/Views/ClaimsDashboard/_RejectionFinancialYear.cshtml");
    });
    $('#Show_Detailed_Report_IMP').click(function () {
        MakeItFullScreen('internalMeanProcessTime', "~/Views/ClaimsDashboard/_InternalMeanProcessTimeFInancialYear.cshtml");
    });


    $('.filter_btn').showFilter();


    //$('select[multiple]').multipleSelect();

    var payerObejct = [{ id: 1, ListName: 'Access2', IsChecked: false }, { id: 2, ListName: 'Access2 Tampa', IsChecked: false }, { id: 3, ListName: 'All American Phy', IsChecked: false }];
    $('#payerFilter').multiselectSearch(payerObejct, 'Payer');


    var billingProviderObject = [{ id: 1, ListName: 'Dina Amundsen', IsChecked: false }, { id: 2, ListName: 'Sheila Trissel', IsChecked: false }, { id: 3, ListName: 'Tony Martin', IsChecked: false }, { id: 4, ListName: 'Marrisa Valenti', IsChecked: false }, { id: 5, ListName: 'Carpi Ritch', IsChecked: false }];
    $('#BillingProviderFilter').multiselectSearch(billingProviderObject, 'Billing Provider');

    var renderingProviderObject = [{ id: 1, ListName: 'Aasma Riaz', IsChecked: false }, { id: 2, ListName: 'Rajyalakshmi Kolli', IsChecked: false }, { id: 3, ListName: 'Gary Merlino', IsChecked: false }, { id: 4, ListName: 'Bernard Kurecki III', IsChecked: false }];
    $('#RenderingProviderFilter').multiselectSearch(renderingProviderObject, 'Rendering Provider');

    var claimTypeObject = [{ id: 1, ListName: 'CAP', IsChecked: false }, { id: 2, ListName: 'FFS', IsChecked: false }, { id: 3, ListName: 'UB04', IsChecked: false }];
    $('#ClaimTypeFilter').multiselectSearch(claimTypeObject, 'Claim Type');

    var billingTeamObject = [{ id: 1, ListName: 'Aasma Riaz', IsChecked: false }, { id: 2, ListName: 'Rajyalakshmi Kolli', IsChecked: false }, { id: 3, ListName: 'Gary Merlino', IsChecked: false }, { id: 4, ListName: 'Bernard Kurecki III', IsChecked: false }];
    $('#BillingTeamFilter').multiselectSearch(billingTeamObject, 'Billing Team');

    var billerObject = [{ id: 1, ListName: 'Aasma Riaz', IsChecked: false }, { id: 2, ListName: 'Rajyalakshmi Kolli', IsChecked: false }, { id: 3, ListName: 'Gary Merlino', IsChecked: false }, { id: 4, ListName: 'Bernard Kurecki III', IsChecked: false }];
    $('#BillerFilter').multiselectSearch(billerObject, 'Biller');



    $('#dos-from').daterangepicker({
        autoUpdateInput: false,
        "showDropdowns": true,
        "showWeekNumbers": true,
        "showISOWeekNumbers": true,
        "locale": {
            "format": "MM/DD/YYYY",
            "separator": " - ",
            "applyLabel": "Apply",
            "cancelLabel": "Cancel",
            "fromLabel": "From",
            "toLabel": "To",
            "customRangeLabel": "Custom",
            "weekLabel": "W",
            "daysOfWeek": [
                "Su",
                "Mo",
                "Tu",
                "We",
                "Th",
                "Fr",
                "Sa"
            ],
            "monthNames": [
                "January",
                "February",
                "March",
                "April",
                "May",
                "June",
                "July",
                "August",
                "September",
                "October",
                "November",
                "December"
            ],
            "firstDay": 1
        }

    }, function (start, end, label) {
        console.log("New date range selected: ' + start.format('YYYY-MM-DD') + ' to ' + end.format('YYYY-MM-DD') + ' (predefined range: ' + label + ')");

        selectedFilterList.push({ labelName: 'DOS', items: [start.format('MM/DD/YYYY') + ' to ' + end.format('MM/DD/YYYY')] });
        var dropTemplate = '<div class="clearfix">';
        for (var i = 0; i < selectedFilterList.length; i++) {
            dropTemplate = dropTemplate + '<span> ' + selectedFilterList[i].labelName + ' :</span><span>'
            for (var j = 0; j < selectedFilterList[i].items.length; j++) {
                dropTemplate = dropTemplate + '<span  class="filter_text_badge">' + selectedFilterList[i].items[j] + '</span>'
            }
            dropTemplate = dropTemplate + '</span>';
        }
        dropTemplate = dropTemplate + '<a class="pull-right btn" id="fav_filter_add"><i style="font-size: 15px" class="fa fa-heartbeat"></i></a></div>';
        $('#search_by_titles').html(dropTemplate);
        $('#search_by_titles').show();

        $('#fav_filter_add').on('click', function () {

            // var top = $(this).offset().top;
            var left = $(this).offset().left - 400;
            $('.add_fav_filter_container').remove();
            $('#fav_filter_add').after("<div style='top:-16px;left:" + left + "px' class='add_fav_filter_container'><div><h6>Favorite</h6></div><div class='fav_new_filter_field_container'><label>Filter Name: &nbsp;</label><input type='text' name='addNewFilterName' /></div><div class='clearfix'><a class='btn btn-xs btn-success pull-right' id='saveNewFavAdd'>Save</a><a class='btn btn-xs btn-warning pull-left' id='closeNewFavAdd'>Cancel</a></div></div>");

            $('#closeNewFavAdd').on('click', function () {
                $('.add_fav_filter_container').remove();
            })


            $('#saveNewFavAdd').on('click', function () {
                $('#search_by_titles').prepend('<label class="filterName_label"><i class="fa fa-heartbeat"></i> <span id="filterName_span">' + $('input[name="addNewFilterName"]').val() + '</span></label>');
                $('.add_fav_filter_container').remove();
                $('#fav_filter_add').remove();
                $('.tracking-filter-form').hide();
            })

        });
    });

    $('#dos-from').on('apply.daterangepicker', function (ev, picker) {
        $(this).val(picker.startDate.format('MM/DD/YYYY') + ' - ' + picker.endDate.format('MM/DD/YYYY'));
    });

    $('#dos-from').on('cancel.daterangepicker', function (ev, picker) {
        $(this).val('');
    });

   

    //-------------------------master filter----------------------------

    $('#master_filter').on('click', function () {
        $('.tracking-filter-form').show();
        $('#search_by_titles').hide();
        $('#master_filter_new').hide();
        $('#master_filter_apply').show();
        $('.master_filter_claims').slideToggle();
        selectedFilterList = [];

    })

    $('#close_master_filter_btn').on('click', function () {
        $('.master_filter_claims').slideUp();
    });

    $('#history_btn').on('click', function () {

        $.ajax({
            type: 'GET',
            url: '/ClaimsDashboard/GetFilterHistory',
            success: function (data) {
                $('.filter_history_dropdown_menu').html(data);
                $('.filter_history_dropdown_menu').slideDown();
            }
        });


    });

})


function ViewFilteredHistory(filterID) {
    $.ajax({
        type: 'GET',
        url: '/ClaimsDashboard/GetSelectedFavFilter?id=' + filterID,
        success: function (data) {
            $('#search_by_titles').html(data);
            $('.filter_history_dropdown_menu').find('li').remove();
            $('.filter_history_dropdown_menu').hide();

            $('#master_filter_apply').show();
            $('#master_filter_new').show();
            $('.tracking-filter-form').hide();
            $('#search_by_titles').show();
        }
    });

}


function ApplyFilteredHistory(filterID) {
    $.ajax({
        type: 'GET',
        url: '/ClaimsDashboard/GetSelectedFavFilter?id=' + filterID,
        success: function (data) {
            $('#search_by_titles').html(data);
            $('.filter_history_dropdown_menu').find('li').remove();
            $('.filter_history_dropdown_menu').hide();

            $('#master_filter_apply').hide();
            $('#master_filter_new').show();
            $('.tracking-filter-form').hide();
            $('#search_by_titles').show();


            ChangeDashboardDataDummy();
        }
    });

}



function ViewDetailedFilteredHistory(filterID) {
    $.ajax({
        type: 'GET',
        url: '/ClaimsDashboard/GetSelectedFavFilter?id=' + filterID,
        success: function (data) {
            $('#search_by_titles_detailed').html(data);
            $('.filter_history_dropdown_menu').find('li').remove();
            $('.filter_history_dropdown_menu').hide();

            $('#detailed_filter_apply').show();
            $('#detailed_filter_new').show();
            $('.tracking-filter-form').hide();
            $('#search_by_titles_detailed').show();
        }
    });

}


function ApplyDetailedFilteredHistory(filterID) {
    $.ajax({
        type: 'GET',
        url: '/ClaimsDashboard/GetSelectedFavFilter?id=' + filterID,
        success: function (data) {
            $('#search_by_titles_detailed').html(data);
            $('.filter_history_dropdown_menu').find('li').remove();
            $('.filter_history_dropdown_menu').hide();

            $('#detailed_filter_apply').hide();
            $('#detailed_filter_new').show();
            $('.tracking-filter-form').hide();
            $('#search_by_titles_detailed').show();


            ChangeDashboardDataDummy();
        }
    });

}


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


function dragStart(event) {
    event.dataTransfer.setData("Text", event.target.id);
    $(event.currentTarget).parent().addClass('drag_source');
}

function allowDrop(event) {
    event.preventDefault();
    $('.drop_target').removeClass('drop_target');
    $(event.currentTarget).addClass('drop_target');
    $('.claims_report_list').css('z-index', '12');
}

function drop(event) {
    event.preventDefault();
    filterChartsWithAjax(event.dataTransfer.getData("Text"), $(event.currentTarget).attr('data-url'));
    $('.drag_source').removeClass('drag_source');
    $('.drop_target').removeClass('drop_target');
    $('.claims_report_list').removeAttr('style');
}


function EditFavFilterName() {

    var value = $('#filterName_span').text();
    $('#filterName_span').html('<input type="text" name="favFilter" value="' + value + '" />');
    $('.edit_filterName').hide();
    $('.save_filterName').show();
}

function SaveFavFilterName() {
    var value = $('input[name="favFilter"]').val();
    $('#filterName_span').html(value);
    $('.edit_filterName').show();
    $('.save_filterName').hide();
}

function ApplyMasterFilter() {
    $('.tracking-filter-form').hide();
    $('#master_filter_new').show();
    $('#master_filter_apply').hide();

    ChangeDashboardDataDummy();
};

function NewMasterFilter() {
    $('.tracking-filter-form').show();
    $('#master_filter_new').hide();
    $('#master_filter_apply').show();
    $('#search_by_titles').hide();
}

function filterChartsWithAjax(filterId, chartId) {

    filterChartsWithAjaxdummy(filterId, chartId);
}



//--------------------------show tile data------------------------------------

function ShowTileDataAmount(targetId, data) {
    var division = parseInt(data / 10);
    var digit = 0;
    var index = 0;
    var interval = setInterval(function () {

        index++;
        digit = digit + division;
        $('#' + targetId).html('<small>$</small>' + digit.toLocaleString());
        if (index >= 10) {
            $('#' + targetId).html('<small>$</small>' + data.toLocaleString());
            clearInterval(interval);
        }
    }, 100);
}

ShowTileDataAmount('initiatedAmountNew', 78585235);

ShowTileDataAmount('AcceptedCHAmountNew', 875874);

ShowTileDataAmount('RejectedCHAmountNew', 8562);

ShowTileDataAmount('AcceptedPayerAmountNew', 895205);

ShowTileDataAmount('RejectedPayerAmountNew', 3001);

function ShowTileDataCount(targetId, data) {
    var division = parseInt(data / 10);
    var digit = 0;
    var index = 0;
    var interval = setInterval(function () {

        index++;
        digit = digit + division;
        $('#' + targetId).html('#' + digit.toLocaleString());
        if (index >= 10) {
            $('#' + targetId).html('#' + data.toLocaleString());
            clearInterval(interval);
        }
    }, 100);
}

ShowTileDataCount('initiatedCountNew', 874542);

ShowTileDataCount('AcceptedCHCountNew', 985422);

ShowTileDataCount('RejectedCHCountNew', 11231);

ShowTileDataCount('AcceptedPayerCountNew', 980354);

ShowTileDataCount('RejectedPayerCountNew', 12132);


function ShowTileDataPercentage(targetId, progressBarId, data) {
    var division = parseInt(data / 10);
    var digit = 0;
    var index = 0;
    var interval = setInterval(function () {
        index++;
        digit = digit + division;
        $('#' + targetId).html(digit + '%');
        $('#' + progressBarId).css({ width: digit + '%' });
        if (index >= 10) {
            $('#' + targetId).html(data + '%');
            $('#' + progressBarId).css({ width: data + '%' });
            clearInterval(interval);
        }
    }, 100);
}

ShowTileDataPercentage('initiated_percent', 'initiated_progress', 23);

ShowTileDataPercentage('acceptedCH_percent', 'acceptedCH_progress', 34);

ShowTileDataPercentage('rejectedCH_percent', 'rejectedCH_progress', 3);

ShowTileDataPercentage('acceptedPayer_percent', 'acceptedPayer_progress', 36);

ShowTileDataPercentage('rejectedPayer_percent', 'rejectedPayer_progress', 4);

