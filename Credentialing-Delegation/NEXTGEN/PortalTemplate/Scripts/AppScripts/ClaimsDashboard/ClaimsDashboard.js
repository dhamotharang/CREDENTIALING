
var htmlContent = null;

function CloseFullScreen(targetID) {
    $('#' + targetID).find('.detailed_view').remove();
    $('#' + targetID).find('.small_view').show();
    $('#' + targetID).animate({ 'width': currentDetailedViewParameters.width + 'px' }, 700).animate({ 'height': currentDetailedViewParameters.height + 'px', 'top': currentDetailedViewParameters.top + 'px', 'left': currentDetailedViewParameters.left + 'px' }, 700, function () {

        $('#' + targetID).removeAttr('style');

    });
}


var currentDetailedViewParameters = {};

$(document).ready(function () {

    //    function DrawMiniBarGraph(divId, data, dataObject) {

    //        var maxNum = _.max(data, function (ele) { return ele.y; });


    //        //------------create svg container--------------------------
    //        var svgContainer = d3.select(divId).append("svg")
    //                                   .attr("width", "100%")
    //                               .attr("height", "80");
    //        var svg = svgContainer.append('g').attr("id", dataObject).attr("class", "g_container");

    //        var diffTwobars = 0;
    //        for (var i = 0; i < data.length; i++) {
    //            diffTwobars = diffTwobars + 8;

    //            svg.append("text")
    //             .attr("class", "x_label_text")
    //                .attr("id", "x_label_text_id" + i)
    //   .attr("x", diffTwobars - 4 + '%')
    //   .attr("y", 80 - (data[i].y * 70 / maxNum.y) + '%')
    //   .text(nFormatter(data[i].y, 1));

    //            svg.append("line")
    //              .attr("class", "virtual_line")
    //                     .attr("id", "VirtualLine" + i)
    //                     .attr("x1", diffTwobars + '%')
    //                     .attr("y1", 0 + '%')
    //                     .attr("x2", diffTwobars + '%')
    //                     .attr("y2", 85 + '%');

    //            svg.append("line")
    //             .attr("class", "virtual_thin_line")
    //                    .attr("id", "VirtualThinLine" + i)
    //                    .attr("x1", diffTwobars + '%')
    //                    .attr("y1", 0 + '%')
    //                    .attr("x2", diffTwobars + '%')
    //                    .attr("y2", 85 + '%');

    //            svg.append("line")
    //             .attr("class", "bar_line")
    //                    .attr("id", "Line" + i)
    //                    .attr("x1", diffTwobars + '%')
    //                    .attr("y1", 85 + '%')
    //                    .attr("x2", diffTwobars + '%')
    //                    .attr("y2", 85 - (data[i].y * 70 / maxNum.y) + '%');
    //            svg.append("text")
    //                .attr("class", "month_text")
    //      .attr("x", diffTwobars - 2 + '%')
    //      .attr("y", "100%")
    //      .text(data[i].x.slice(0, 1));

    //        }

    //    }


    //    function UpdateMiniBarGraph(divId, data, dataObject) {
    //        var maxNum = _.max(data, function (ele) { return ele.y; });
    //        //------------update svg container--------------------------
    //        var svgContainer = d3.select(divId).select("svg");
    //        var svg = d3.select('#' + dataObject);
    //        var diffTwobars = 0;
    //        for (var i = 0; i < data.length; i++) {
    //            diffTwobars = diffTwobars + 8;
    //            svg.select("#x_label_text_id" + i)
    //   .attr("x", diffTwobars - 4 + '%')
    //   .attr("y", 80 - (data[i].y * 70 / maxNum.y) + '%')
    //   .text(nFormatter(data[i].y, 1));

    //            svg.select("#VirtualLine" + i)
    //                     .attr("x1", diffTwobars + '%')
    //                     .attr("y1", 0 + '%')
    //                     .attr("x2", diffTwobars + '%')
    //                     .attr("y2", 85 + '%');

    //            svg.select("#VirtualThinLine" + i)
    //                    .attr("x1", diffTwobars + '%')
    //                    .attr("y1", 0 + '%')
    //                    .attr("x2", diffTwobars + '%')
    //                    .attr("y2", 85 + '%');

    //            svg.select("#Line" + i)
    //                    .attr("x1", diffTwobars + '%')
    //                    .attr("y1", 85 + '%')
    //                    .attr("x2", diffTwobars + '%')
    //                    .attr("y2", 85 - (data[i].y * 70 / maxNum.y) + '%');

    //        }

    //    }


    //    function nFormatter(num, digits) {
    //        //var num = isInt(num) ? parseInt(num) : parseFloat(num);
    //        var si = [
    //          { value: 1E18, symbol: "E" },
    //          { value: 1E15, symbol: "P" },
    //          { value: 1E12, symbol: "T" },
    //          { value: 1E9, symbol: "B" },
    //          { value: 1E6, symbol: "M" },
    //          { value: 1E3, symbol: "k" }
    //        ], rx = /\.0+$|(\.[0-9]*[1-9])0+$/, i;
    //        for (i = 0; i < si.length; i++) {
    //            if (num >= si[i].value) {
    //                return (num / si[i].value).toFixed(digits).replace(rx, "$1") + si[i].symbol;
    //            }
    //        }
    //        return num.toFixed(digits).replace(rx, "$1");
    //    }



    //    //----------------------------Claims count------------------------------

    //    var tileData = { Count: {}, Amount: {} };

    //    var Count = {};

    //    Count.ClaimsCount = [{ x: 'January', y: 334 }, { x: 'February', y: 544 }, { x: 'March', y: 344 }, { x: 'April', y: 100 },
    //    { x: 'May', y: 2223 }, { x: 'June', y: 112 }, { x: 'July', y: 111 }, { x: 'August', y: 1233 }, { x: 'September', y: 333 },
    //{ x: 'October', y: 454 }, { x: 'November', y: 865 }, { x: 'December', y: 666 }];

    //    Count.ClaimsAcceptedByCH = [{ x: 'January', y: 888 }, { x: 'February', y: 2000 }, { x: 'March', y: 655 }, { x: 'April', y: 678 },
    //    { x: 'May', y: 324 }, { x: 'June', y: 2333 }, { x: 'July', y: 3000 }, { x: 'August', y: 534 }, { x: 'September', y: 678 },
    //{ x: 'October', y: 545 }, { x: 'November', y: 3444 }, { x: 'December', y: 453 }];

    //    Count.ClaimsRejectedByCH = [{ x: 'January', y: 334 }, { x: 'February', y: 544 }, { x: 'March', y: 344 }, { x: 'April', y: 500 },
    //    { x: 'May', y: 1111 }, { x: 'June', y: 112 }, { x: 'July', y: 111 }, { x: 'August', y: 1233 }, { x: 'September', y: 333 },
    //{ x: 'October', y: 454 }, { x: 'November', y: 865 }, { x: 'December', y: 666 }];

    //    Count.ClaimsAcceptedByPayer = [{ x: 'January', y: 1745 }, { x: 'February', y: 2000 }, { x: 'March', y: 1000 }, { x: 'April', y: 200 },
    //    { x: 'May', y: 300 }, { x: 'June', y: 520 }, { x: 'July', y: 3000 }, { x: 'August', y: 862 }, { x: 'September', y: 956 },
    //{ x: 'October', y: 302 }, { x: 'November', y: 865 }, { x: 'December', y: 666 }];

    //    Count.ClaimsRejectedByPayer = [{ x: 'January', y: 745 }, { x: 'February', y: 532 }, { x: 'March', y: 345 }, { x: 'April', y: 344 },
    //    { x: 'May', y: 1343 }, { x: 'June', y: 2424 }, { x: 'July', y: 435 }, { x: 'August', y: 555 }, { x: 'September', y: 343 },
    //{ x: 'October', y: 543 }, { x: 'November', y: 234 }, { x: 'December', y: 322 }];

    //    tileData.Count = Count;


    //    var Amount = {};

    //    Amount.ClaimsCount = [{ x: 'January', y: 1745 }, { x: 'February', y: 2000 }, { x: 'March', y: 655 }, { x: 'April', y: 678 },
    //    { x: 'May', y: 324 }, { x: 'June', y: 2333 }, { x: 'July', y: 3000 }, { x: 'August', y: 534 }, { x: 'September', y: 678 },
    //{ x: 'October', y: 545 }, { x: 'November', y: 3444 }, { x: 'December', y: 453 }];

    //    Amount.ClaimsAcceptedByCH = [{ x: 'January', y: 745 }, { x: 'February', y: 532 }, { x: 'March', y: 345 }, { x: 'April', y: 344 },
    //    { x: 'May', y: 1343 }, { x: 'June', y: 2424 }, { x: 'July', y: 435 }, { x: 'August', y: 555 }, { x: 'September', y: 343 },
    //{ x: 'October', y: 543 }, { x: 'November', y: 234 }, { x: 'December', y: 322 }];

    //    Amount.ClaimsRejectedByCH = [{ x: 'January', y: 1745 }, { x: 'February', y: 2000 }, { x: 'March', y: 655 }, { x: 'April', y: 678 },
    //    { x: 'May', y: 324 }, { x: 'June', y: 2333 }, { x: 'July', y: 3000 }, { x: 'August', y: 534 }, { x: 'September', y: 678 },
    //{ x: 'October', y: 545 }, { x: 'November', y: 3444 }, { x: 'December', y: 453 }];

    //    Amount.ClaimsAcceptedByPayer = [{ x: 'January', y: 334 }, { x: 'February', y: 544 }, { x: 'March', y: 344 }, { x: 'April', y: 3222 },
    //    { x: 'May', y: 3223 }, { x: 'June', y: 112 }, { x: 'July', y: 111 }, { x: 'August', y: 1233 }, { x: 'September', y: 333 },
    //{ x: 'October', y: 454 }, { x: 'November', y: 865 }, { x: 'December', y: 666 }];

    //    Amount.ClaimsRejectedByPayer = [{ x: 'January', y: 334 }, { x: 'February', y: 544 }, { x: 'March', y: 344 }, { x: 'April', y: 100 },
    //    { x: 'May', y: 2223 }, { x: 'June', y: 112 }, { x: 'July', y: 111 }, { x: 'August', y: 1233 }, { x: 'September', y: 333 },
    //{ x: 'October', y: 454 }, { x: 'November', y: 865 }, { x: 'December', y: 666 }];

    //    tileData.Amount = Amount;


    //    DrawMiniBarGraph('#ClaimCountMonthlyDistribution', tileData.Count.ClaimsCount, "ClaimsCount");

    //    DrawMiniBarGraph('#AcceptedByCHMonthlyDistribution', tileData.Count.ClaimsAcceptedByCH, "ClaimsAcceptedByCH");

    //    DrawMiniBarGraph('#RejectedByCHMonthlyDistribution', tileData.Count.ClaimsRejectedByCH, "ClaimsRejectedByCH");

    //    DrawMiniBarGraph('#AcceptedByPayerMonthlyDistribution', tileData.Count.ClaimsAcceptedByPayer, "ClaimsAcceptedByPayer");

    //    DrawMiniBarGraph('#RejectedByPayerMonthlyDistribution', tileData.Count.ClaimsRejectedByPayer, "ClaimsRejectedByPayer");

    //    //-----------tooltip-------------------
    //    var CurrentParentTileData = tileData.Count;
    //    var currentTileData = {};

    //    $('.g_container').mouseenter(function () {
    //        currentTileData = CurrentParentTileData[$(this).attr("id")];
    //    });


    //    $('.virtual_line').mousemove(function (e) {
    //        var y = e.pageY;
    //        var x = e.pageX;
    //        var tooltipHeaderText = currentTileData[parseInt(e.currentTarget.id.slice(11, e.currentTarget.id.length))].x;
    //        var tooltipBodyText = currentTileData[parseInt(e.currentTarget.id.slice(11, e.currentTarget.id.length))].y;
    //        $('.custom_tooltip .tooltip_header').html(tooltipHeaderText);
    //        $('.custom_tooltip .tooltip_body').html('$' + tooltipBodyText.toLocaleString());
    //        $('.custom_tooltip').css('top', y - 30).css('left', x + 15);
    //    });
    //    $('.virtual_line').mouseleave(function (e) {
    //        $('.custom_tooltip').hide();
    //    });
    //    $('.virtual_line').mouseenter(function (e) {
    //        $('.custom_tooltip').show();
    //    });

    //    $('.bar_line').mousemove(function (e) {
    //        var y = e.pageY;
    //        var x = e.pageX;
    //        var tooltipHeaderText = currentTileData[parseInt(e.currentTarget.id.slice(4, e.currentTarget.id.length))].x;
    //        var tooltipBodyText = currentTileData[parseInt(e.currentTarget.id.slice(4, e.currentTarget.id.length))].y;
    //        $('.custom_tooltip .tooltip_header').html(tooltipHeaderText);
    //        $('.custom_tooltip .tooltip_body').html('$' + tooltipBodyText.toLocaleString());
    //        $('.custom_tooltip').css('top', y - 30).css('left', x + 15);
    //    });
    //    $('.bar_line').mouseleave(function (e) {
    //        $('.custom_tooltip').hide();
    //    });
    //    $('.bar_line').mouseenter(function (e) {
    //        $('.custom_tooltip').show();
    //    });

    //    //----------------------default active----------------------

    //    $('.cnt_label').addClass('active_tile_type');

    //    $('.amt_label').click(function (ev) {
    //        ev.preventDefault();
    //        var currentDiv = $(this).attr("href");

    //        var dataProperty = '';
    //        if (currentDiv == '#ClaimCountMonthlyDistribution')
    //            dataProperty = 'ClaimsCount';
    //        else if (currentDiv == '#AcceptedByCHMonthlyDistribution')
    //            dataProperty = 'ClaimsAcceptedByCH';
    //        else if (currentDiv == '#RejectedByCHMonthlyDistribution')
    //            dataProperty = 'ClaimsRejectedByCH';
    //        else if (currentDiv == '#AcceptedByPayerMonthlyDistribution')
    //            dataProperty = 'ClaimsAcceptedByPayer';
    //        else if (currentDiv == '#RejectedByPayerMonthlyDistribution')
    //            dataProperty = 'ClaimsRejectedByPayer';

    //        $(this).next().removeClass('active_tile_type');
    //        $(this).addClass('active_tile_type');
    //        CurrentParentTileData = tileData.Amount;
    //        $(currentDiv).find('.body_title').html("Amount");
    //        UpdateMiniBarGraph(currentDiv, tileData.Amount[dataProperty], dataProperty);

    //    });
    //    $('.cnt_label').click(function (ev) {
    //        ev.preventDefault();
    //        var currentDiv = $(this).attr("href");

    //        var dataProperty = '';
    //        if (currentDiv == '#ClaimCountMonthlyDistribution')
    //            dataProperty = 'ClaimsCount';
    //        else if (currentDiv == '#AcceptedByCHMonthlyDistribution')
    //            dataProperty = 'ClaimsAcceptedByCH';
    //        else if (currentDiv == '#RejectedByCHMonthlyDistribution')
    //            dataProperty = 'ClaimsRejectedByCH';
    //        else if (currentDiv == '#AcceptedByPayerMonthlyDistribution')
    //            dataProperty = 'ClaimsAcceptedByPayer';
    //        else if (currentDiv == '#RejectedByPayerMonthlyDistribution')
    //            dataProperty = 'ClaimsRejectedByPayer';

    //        $(this).prev().removeClass('active_tile_type');
    //        $(this).addClass('active_tile_type');
    //        CurrentParentTileData = tileData.Count;
    //        $(currentDiv).find('.body_title').html("Count");
    //        UpdateMiniBarGraph(currentDiv, tileData.Count[dataProperty], dataProperty);

    //    });






    //$('select[name="OverallMeanProcessTimeSelect"]').change(function () {
    //    var currentValue = $(this).val();
    //    if (currentValue == 'bottom') {

    //        MeanTimeChart.load({
    //            columns: [
    //                  ['x', '66055', '13162', '51028', '20413', '93044'],
    //            ['Previous Year', 120, 100, 90, 78, 20],
    //            ['Current Year', 130, 50, 45, 30, 15]
    //            ]
    //        });
    //    } else {
    //        MeanTimeChart.load({
    //            columns: [
    //                  ['x', '93044', '20413', '51028', '66055', '36273'],
    //              ['Previous Year', 30, 40, 55, 65, 150],
    //            ['Current Year', 10, 30, 45, 80, 118]
    //            ]
    //        });

    //    }
    //});




    // var topCPTReport = [{ Code: '97814', Count: '9535', Cost: '20000', Desc: 'Acupunct w/s timul addl 15 m' },
    // { Code: '95504', Count: '9535', Cost: '20000', Desc: 'Percute allergy skin test' },
    // { Code: '95010', Count: '9535', Cost: '20000', Desc: 'Percute allergy titrate test' },
    // { Code: '95028', Count: '9535', Cost: '20000', Desc: 'Id allergy test delayed type' },
    // { Code: '95044', Count: '9535', Cost: '20000', Desc: 'Allergy patch test' },
    // { Code: '95056', Count: '9535', Cost: '20000', Desc: 'Photo Sensitivity Test' },
    // { Code: '95078', Count: '9535', Cost: '20000', Desc: 'NO Longer Valid-07 Provocative Test' },
    // { Code: '95024', Count: '9535', Cost: '20000', Desc: 'Id Allergy Test' },
    // { Code: '97811', Count: '9535', Cost: '20000', Desc: 'Acupunct w/s timul addl 15 m' },
    // { Code: '95074', Count: '9535', Cost: '20000', Desc: 'Ingestion Challenge test' }];

    // var bottomCPTReport = [{ Code: '50', Count: '9535', Cost: '20000', Desc: 'Acupunct w/s timul addl 15 m' },
    //{ Code: '95504', Count: '45', Cost: '20000', Desc: 'Percute allergy skin test' },
    //{ Code: '95010', Count: '33', Cost: '20000', Desc: 'Percute allergy titrate test' },
    //{ Code: '95028', Count: '31', Cost: '20000', Desc: 'Id allergy test delayed type' },
    //{ Code: '95044', Count: '30', Cost: '20000', Desc: 'Allergy patch test' },
    //{ Code: '95056', Count: '29', Cost: '20000', Desc: 'Photo Sensitivity Test' },
    //{ Code: '95078', Count: '20', Cost: '20000', Desc: 'NO Longer Valid-07 Provocative Test' },
    //{ Code: '95024', Count: '10', Cost: '20000', Desc: 'Id Allergy Test' },
    //{ Code: '97811', Count: '3', Cost: '20000', Desc: 'Acupunct w/s timul addl 15 m' },
    //{ Code: '95074', Count: '1', Cost: '20000', Desc: 'Ingestion Challenge test' }];

    // $('select[name="selectPCP"]').change(function () {
    //     var currentVal = $(this).val();
    //     var tableTemplate = '';
    //     if (currentVal == 'top') {
    //         for (var i = 0; i < topCPTReport.length; i++) {
    //             tableTemplate = tableTemplate + '<tr><td>' + topCPTReport[i].Code + '</td><td>' + topCPTReport[i].Count + '</td><td>' + topCPTReport[i].Cost + '</td><td>' + topCPTReport[i].Desc + '</td></tr>'
    //         }
    //     } else {
    //         for (var i = bottomCPTReport.length - 1; i >= 0; i--) {
    //             tableTemplate = tableTemplate + '<tr><td>' + bottomCPTReport[i].Code + '</td><td>' + bottomCPTReport[i].Count + '</td><td>' + bottomCPTReport[i].Cost + '</td><td>' + bottomCPTReport[i].Desc + '</td></tr>'
    //         }
    //     }

    //     $('#ProceduresClaimedReport').find('table').find('tbody').html(tableTemplate);
    // });



    //    var topAdjustmentReport = [{ Code: '97814', Count: '9535', Cost: '20000', Desc: 'Acupunct w/s timul addl 15 m' },
    //{ Code: '95504', Count: '9535', Cost: '20000', Desc: 'Percute allergy skin test' },
    //{ Code: '95010', Count: '9535', Cost: '20000', Desc: 'Percute allergy titrate test' },
    //{ Code: '95028', Count: '9535', Cost: '20000', Desc: 'Id allergy test delayed type' },
    //{ Code: '95044', Count: '9535', Cost: '20000', Desc: 'Allergy patch test' },
    //{ Code: '95056', Count: '9535', Cost: '20000', Desc: 'Photo Sensitivity Test' },
    //{ Code: '95078', Count: '9535', Cost: '20000', Desc: 'NO Longer Valid-07 Provocative Test' },
    //{ Code: '95024', Count: '9535', Cost: '20000', Desc: 'Id Allergy Test' },
    //{ Code: '97811', Count: '9535', Cost: '20000', Desc: 'Acupunct w/s timul addl 15 m' },
    //{ Code: '95074', Count: '9535', Cost: '20000', Desc: 'Ingestion Challenge test' }];

    //    var bottomAdjustmentReport = [{ Code: '50', Count: '9535', Cost: '20000', Desc: 'Acupunct w/s timul addl 15 m' },
    //   { Code: '95504', Count: '45', Cost: '20000', Desc: 'Percute allergy skin test' },
    //   { Code: '95010', Count: '33', Cost: '20000', Desc: 'Percute allergy titrate test' },
    //   { Code: '95028', Count: '31', Cost: '20000', Desc: 'Id allergy test delayed type' },
    //   { Code: '95044', Count: '30', Cost: '20000', Desc: 'Allergy patch test' },
    //   { Code: '95056', Count: '29', Cost: '20000', Desc: 'Photo Sensitivity Test' },
    //   { Code: '95078', Count: '20', Cost: '20000', Desc: 'NO Longer Valid-07 Provocative Test' },
    //   { Code: '95024', Count: '10', Cost: '20000', Desc: 'Id Allergy Test' },
    //   { Code: '97811', Count: '3', Cost: '20000', Desc: 'Acupunct w/s timul addl 15 m' },
    //   { Code: '95074', Count: '1', Cost: '20000', Desc: 'Ingestion Challenge test' }];

    //    $('select[name="selectPanelty"]').change(function () {
    //        var currentVal = $(this).val();
    //        var tableTemplate = '';
    //        if (currentVal == 'top') {
    //            for (var i = 0; i < topAdjustmentReport.length; i++) {
    //                tableTemplate = tableTemplate + '<tr><td>' + topAdjustmentReport[i].Code + '</td><td>' + topAdjustmentReport[i].Count + '</td><td>' + topAdjustmentReport[i].Cost + '</td><td>' + topAdjustmentReport[i].Desc + '</td></tr>'
    //            }
    //        } else {
    //            for (var i = bottomAdjustmentReport.length - 1; i >= 0; i--) {
    //                tableTemplate = tableTemplate + '<tr><td>' + bottomAdjustmentReport[i].Code + '</td><td>' + bottomAdjustmentReport[i].Count + '</td><td>' + bottomAdjustmentReport[i].Cost + '</td><td>' + bottomAdjustmentReport[i].Desc + '</td></tr>'
    //            }
    //        }

    //        $('#PenaltyAdjustmentReport').find('table').find('tbody').html(tableTemplate);
    //    });


    //-------------------------------ICD------------------------------------



    //$('select[name="InternalMeanProcessTimeSelect"]').change(function () {
    //    var currentValue = $(this).val();
    //    if (currentValue == 'bottom') {

    //        SpanTimeChart.load({
    //            columns: [
    //                  ['x', '95241', '75240', '75185', '36273', '87726'],
    //                 ['Current Year', 65, 52, 42, 38, 37]
    //            ]
    //        });
    //    } else {
    //        SpanTimeChart.load({
    //            columns: [
    //                  ['x', '93044', '20413', '51028', '66055', '36273'],
    //                 ['Current Year', 10, 22, 29, 38, 45]
    //            ]
    //        });

    //    }
    //});




    //-------------------------- sortable----------------------------

    $("#sortable").sortable({

        group: 'no-drop',
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

    });




    //$.fn.extend({
    //    animateCss: function (animationName) {
    //        var animationEnd = 'webkitAnimationEnd mozAnimationEnd MSAnimationEnd oanimationend animationend';
    //        $(this).addClass('animated ' + animationName).one(animationEnd, function () {
    //            $(this).removeClass('animated ' + animationName);
    //        });
    //    }
    //});

    //$('#Show_Detailed_Report_Btn').click(function () {
    //    $('.Detailed_Report').show();
    //    $('.Detailed_Report').animateCss('fadeInDown');
    //    DrawDetailedReport();
    //});

    //$('#Close_Detailed_Report').click(function () {
    //    $('.Detailed_Report').animateCss('bounceOut');
    //    setTimeout(function () {
    //        $('.Detailed_Report').hide();
    //    }, 500);
    //});


    //---------------------------------detailed report-----------------------------------


    function MakeItFullScreen(targetDivId, targetUrl) {
        currentDetailedViewParameters = { height: $('#' + targetDivId).height(), width: $('#' + targetDivId).width(), top: $('#' + targetDivId).offset().top - $(window).scrollTop(), left: $('#' + targetDivId).offset().left };
        htmlContent = $('#' + targetDivId).html();
        $('#' + targetDivId).css({ 'position': 'fixed', 'z-index': '1000000', 'overflow': 'hidden', top: currentDetailedViewParameters.top });
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



    //-------------------------tiles---------------------------

    //--------------------convert polar to cartesion co-ordinates-------------

    //  function PolarToCartesian(X0, Y0, r, angle) {
    //      var angleInRadians = (angle - 90) * Math.PI / 180.0;

    //      return {
    //          x: X0 + (r * Math.cos(angleInRadians)),
    //          y: Y0 + (r * Math.sin(angleInRadians))
    //      }
    //  }


    //  function DrawPercentageCurve(X0, Y0, r, percentage) {
    //      var endAngle = percentage * 3.6;
    //      var start = PolarToCartesian(X0, Y0, r, endAngle);
    //      var end = PolarToCartesian(X0, Y0, r, 0);
    //      var arcSweep = endAngle - 0 <= 180 ? "0" : "1";

    //      var d = [
    //          "M", start.x, start.y,
    //          "A", r, r, 0, arcSweep, 0, end.x, end.y
    //      ].join(" ");

    //      return d;
    //  }


    //  function DrawMiniBarGraph(X0, Y0, r, percentage, chartColor, chartId, divId, svg_size) {

    //      //------------create svg container--------------------------
    //      var svgContainer = d3.select('#' + divId).append("svg")
    //                                 .attr("width", "100%")
    //                             .attr("height", svg_size == 'md' ? 79 : 55);
    //      var svg = svgContainer.append('g').attr("class", "g_container");



    //      svg.append('path')
    //      .attr('id', chartId)
    //      .attr('stroke', chartColor)
    //      .attr('stroke-width', '15')
    //          .attr('fill', 'none')
    //      .attr('d', DrawPercentageCurve(X0, Y0, r, percentage));

    //      svg.append('circle')
    //          .attr('class', 'progress_circle')
    //      .attr('r', r)
    //      .attr('cx', X0)
    //      .attr('cy', Y0);


    //      svg.append("text")
    //.attr("class", svg_size + "_per_text")
    //.attr("x", X0 - 12)
    //.attr("y", Y0 + 4)
    //.text(percentage + '%');

    //  }


    //  DrawMiniBarGraph(40, 40, 30, 56, '#3196da', 'initiated_chart', 'initiated_chart_container', 'md');

    //  DrawMiniBarGraph(40, 28, 20, 44, 'rgba(255, 255, 255, 0.4)', 'created_chart', 'created_chart_container', 'sm');

    //  DrawMiniBarGraph(40, 28, 20, 44, 'rgba(255, 255, 255, 0.4)', 'received_chart', 'received_chart_container', 'sm');

    //  DrawMiniBarGraph(40, 28, 20, 35, '#ffffff', 'created_chart', 'acceptedbyCH_chart_container', 'sm');

    //  DrawMiniBarGraph(40, 28, 20, 76, 'rgba(255, 255, 255, 0.4)', 'received_chart', 'acceptedbyPayer_chart_container', 'sm');

    //  DrawMiniBarGraph(40, 28, 20, 25, 'rgba(255, 255, 255, 0.4)', 'created_chart', 'rejectedbyCH_chart_container', 'sm');

    //  DrawMiniBarGraph(40, 28, 20, 30, 'rgba(255, 255, 255, 0.4)', 'received_chart', 'rejectedbyPayer_chart_container', 'sm');


    $('select[multiple]').multipleSelect();


})

