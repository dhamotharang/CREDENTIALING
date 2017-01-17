$(function () {

    function nFormatter(num, digits) {
        //var num = isInt(num) ? parseInt(num) : parseFloat(num);
        var si = [
          { value: 1E18, symbol: "E" },
          { value: 1E15, symbol: "P" },
          { value: 1E12, symbol: "T" },
          { value: 1E9, symbol: "B" },
          { value: 1E6, symbol: "M" },
          { value: 1E3, symbol: "k" }
        ], rx = /\.0+$|(\.[0-9]*[1-9])0+$/, i;
        for (i = 0; i < si.length; i++) {
            if (num >= si[i].value) {
                return (num / si[i].value).toFixed(digits).replace(rx, "$1") + si[i].symbol;
            }
        }
        return num.toFixed(digits).replace(rx, "$1");
    }


    function DrawStatusBar(countList) {


        var selectedClass = 'primary-circle';

        

        trackingArray = [
   { cx: '3', cy: '70', ClaimedAmt: 0, statusText: 'Created', count: 0, type: 'success', highlightCircle: ['0'], highlightLine: [] },
   { cx: '3', cy: '30', ClaimedAmt: 0, statusText: 'Incoming', count: 0, type: 'success', highlightCircle: ['1'], highlightLine: [] },
   { cx: '10', cy: '50', ClaimedAmt: 0, statusText: 'Open', count: 0, type: 'success', highlightCircle: ['0', '1', '2'], highlightLine: ['Line0', 'Line1', 'Line2'] },
   { cx: '20', cy: '30', ClaimedAmt: 0, statusText: 'Rejected', count: 0, type: 'danger', highlightCircle: ['0', '1', '2', '3'], highlightLine: ['Line0', 'Line1', 'Line2', 'Line3', 'Line4'] },
   { cx: '20', cy: '70', ClaimedAmt: 0, statusText: 'Onhold', count: 0, type: 'warning', highlightCircle: ['0', '1', '2', '4'], highlightLine: ['Line0', 'Line1', 'Line2', 'Line3', 'Line5'] },
   { cx: '28', cy: '50', ClaimedAmt: 0, statusText: 'Accepted', count: 0, type: 'success', highlightCircle: ['0', '1', '2', '5'], highlightLine: ['Line0', 'Line1', 'Line2', 'Line3', 'Line6'] },
   { cx: '40', cy: '50', ClaimedAmt: 0, statusText: 'Dispatched', count: 0, type: 'success', highlightCircle: ['0', '1', '2', '5', '6'], highlightLine: ['Line0', 'Line1', 'Line2', 'Line3', 'Line6', 'Line7'] },
   { cx: '50', cy: '70', ClaimedAmt: 0, statusText: 'Unack by CH', count: 0, type: 'warning', highlightCircle: ['0', '1', '2', '5', '6', '7'], highlightLine: ['Line0', 'Line1', 'Line2', 'Line3', 'Line6', 'Line7', 'Line8', 'Line10'] },
   { cx: '50', cy: '30', ClaimedAmt: 0, statusText: 'Rejected by CH', count: 0, type: 'danger', highlightCircle: ['0', '1', '2', '5', '6', '8'], highlightLine: ['Line0', 'Line1', 'Line2', 'Line3', 'Line6', 'Line7', 'Line8', 'Line9'] },
   { cx: '57', cy: '50', ClaimedAmt: 0, statusText: 'Accepted by CH', count: 0, type: 'success', highlightCircle: ['0', '1', '2', '5', '6', '9'], highlightLine: ['Line0', 'Line1', 'Line2', 'Line3', 'Line6', 'Line7', 'Line8', 'Line11'] },
   { cx: '68', cy: '30', ClaimedAmt: 0, statusText: 'Rejected by payer', count: 0, type: 'danger', highlightCircle: ['0', '1', '2', '5', '6', '9', '10'], highlightLine: ['Line0', 'Line1', 'Line2', 'Line3', 'Line6', 'Line7', 'Line8', 'Line11', 'Line12', 'Line13'] },
   { cx: '75', cy: '50', ClaimedAmt: 0, statusText: 'Accepted by payer', count: 0, type: 'success', highlightCircle: ['0', '1', '2', '5', '6', '9', '11'], highlightLine: ['Line0', 'Line1', 'Line2', 'Line3', 'Line6', 'Line7', 'Line8', 'Line11', 'Line12', 'Line14'] },
   { cx: '85', cy: '70', ClaimedAmt: 0, statusText: 'Denied by payer', count: 0, type: 'danger', highlightCircle: ['0', '1', '2', '5', '6', '9', '11', '12'], highlightLine: ['Line0', 'Line1', 'Line2', 'Line3', 'Line6', 'Line7', 'Line8', 'Line11', 'Line12', 'Line14', 'Line15', 'Line16'] },
   { cx: '85', cy: '30', ClaimedAmt: 0, statusText: 'Pending', count: 0, type: 'warning', highlightCircle: ['0', '1', '2', '5', '6', '9', '11', '13'], highlightLine: ['Line0', 'Line1', 'Line2', 'Line3', 'Line6', 'Line7', 'Line8', 'Line11', 'Line12', 'Line14', 'Line15', 'Line17'] },
   { cx: '93', cy: '50', ClaimedAmt: 0, statusText: 'EOB received', count: 0, type: 'success', highlightCircle: ['0', '1', '2', '5', '6', '9', '11', '14'], highlightLine: ['Line0', 'Line1', 'Line2', 'Line3', 'Line6', 'Line7', 'Line8', 'Line11', 'Line12', 'Line14', 'Line15', 'Line18'] }
        ];

        lineArray = [{ x1: '3', y1: '70', x2: '3', y2: '50', className: 'success' },
                   { x1: '3', y1: '30', x2: '3', y2: '50', className: 'success' },
                   { x1: '3', y1: '50', x2: '10', y2: '50', className: 'success' },
                   { x1: '10', y1: '50', x2: '20', y2: '50', className: 'success' },
                   { x1: '20', y1: '30', x2: '20', y2: '50', className: 'danger' },
                   { x1: '20', y1: '70', x2: '20', y2: '50', className: 'warning' },
                   { x1: '28', y1: '50', x2: '20', y2: '50', className: 'success' },
                   { x1: '28', y1: '50', x2: '40', y2: '50', className: 'success' },
                   { x1: '40', y1: '50', x2: '50', y2: '50', className: 'success' },
                   { x1: '50', y1: '30', x2: '50', y2: '50', className: 'danger' },
                   { x1: '50', y1: '70', x2: '50', y2: '50', className: 'warning' },
                   { x1: '50', y1: '50', x2: '57', y2: '50', className: 'success' },
                   { x1: '57', y1: '50', x2: '68', y2: '50', className: 'success' },
                   { x1: '68', y1: '30', x2: '68', y2: '50', className: 'danger' },
                   { x1: '68', y1: '50', x2: '75', y2: '50', className: 'success' },
                   { x1: '75', y1: '50', x2: '85', y2: '50', className: 'success' },
                   { x1: '85', y1: '70', x2: '85', y2: '50', className: 'danger' },
                   { x1: '85', y1: '30', x2: '85', y2: '50', className: 'warning' },
                   { x1: '85', y1: '50', x2: '93', y2: '50', className: 'success' }];

        _.each(countList, function (row) {

            trackingArray[parseInt(row.Status)].count = row.ClaimCount;
            trackingArray[parseInt(row.Status)].ClaimedAmt = row.ClaimedAmt;
        });

        var svg = d3.select(".tracking-container").append("svg")
                                   .attr("width", "100%")
                               .attr("height", "240");
        var svgContainer = svg.append('g');


        var gatewaycontainer = svgContainer.append('rect')
            .attr('x', '0')
            .attr('y', '0')
            .attr('class', 'gateway-container')
        .attr("width", "46.8%")
        .attr("height", "100%");


        var gatewayText = svgContainer.append("text")
            .attr("class", "container-text")
    .attr("x", "20%")
    .attr("y", "96%")
    .text('Gateway');

        var chcontainer = svgContainer.append('rect')
            .attr('x', '46.8%')
            .attr('y', '0')
            .attr('class', 'clearinghouse-container')
        .attr("width", "17.5%")
        .attr("height", "100%");

        var clearinghouseText = svgContainer.append("text")
        .attr("class", "container-text")
.attr("x", "49%")
.attr("y", "96%")
.text('Clearing House');

        var payercontainer = svgContainer.append('rect')
            .attr('x', '64.3%')
            .attr('y', '0')
            .attr('class', 'payer-container')
        .attr("width", "35.7%")
        .attr("height", "100%");


        var payerText = svgContainer.append("text")
           .attr("class", "container-text")
   .attr("x", "80%")
   .attr("y", "96%")
   .text('Payer');


        var connectingLine = svgContainer.append("line")
                      .attr("class", "connecting-line")
                      .attr("id", "connectingline");

        var connectingLine1 = svgContainer.append("line")
                      .attr("class", "connecting-line")
                      .attr("id", "connectingline1");

        var arrow1 = svgContainer.append("line")
                     .attr("class", "connecting-line")
                     .attr("id", "arrowLine1");

        var arrow2 = svgContainer.append("line")
                     .attr("class", "connecting-line")
                     .attr("id", "arrowLine2");



        for (var i = 0; i < lineArray.length; i++) {

            svgContainer.append("line")
                        .attr("class", lineArray[i].className + "-line")
                        .attr("id", "Line" + i)
                        .attr("x1", lineArray[i].x1 + '%')
                        .attr("y1", lineArray[i].y1 + '%')
                        .attr("x2", lineArray[i].x2 + '%')
                        .attr("y2", lineArray[i].y2 + '%');
        }

        var template = '';

        for (var i = 0; i < trackingArray.length; i++) {


            if (parseInt(trackingArray[i].cy) <= 50) {

                template = '<div class="status-div ' + trackingArray[i].type + '-status-div ' + trackingArray[i].type + '-status-div-upper"><div class="status-header"><div class="status-text"><span class="' + trackingArray[i].type + '-text">' + trackingArray[i].statusText + '</span></div></div><div class="status-body"><div class="status-amt"><span class="' + trackingArray[i].type + '-text">$' + nFormatter(trackingArray[i].ClaimedAmt, 2) + '</span></div><div class="status-count"><span class="' + trackingArray[i].type + '-text">#' + nFormatter(trackingArray[i].count, 2) + '</span></div></div><div class="clearfix"></div></div>';


                svgContainer.append('foreignObject')
                     .attr("id", "StatusPanel" + i)
                       .attr('x', parseInt(trackingArray[i].cx) - 3 + '%')
                       .attr('y', parseInt(trackingArray[i].cy) - 23 + '%')
                       .attr('width', 80)
                       .attr('height', 10)
                       .html(template)
                    .on('mouseover', function () {
                        currentElement = $(this).find('.status-div');
                        if (!currentElement.hasClass('active-status-div') && currentElement.hasClass('inactive-status-div'))
                            currentElement.css({ 'transform': 'scale(1.04)', 'transition': 'all linear 0.2s', 'opacity': '1' });

                    })
                .on('mouseout', function () {
                    currentElement = $(this).find('.status-div');
                    currentElement.removeAttr('style');

                })
                        .on('click', function () {
                            currentElement = this;
                            $('.status-div').removeClass('active-status-div');
                            $('.status-div').addClass('inactive-status-div');
                            $(currentElement).find('.status-div').addClass('active-status-div');
                            $(currentElement).find('.status-div').removeClass('inactive-status-div');
                            $(currentElement).find('.status-div').removeAttr('style');
                            console.log("  Status Panel id : " +  parseInt(currentElement.id.substr(11, currentElement.id.length - 10)));
                            var TableDataObj = GetTrackingDataforTable(parseInt(currentElement.id.substr(11, currentElement.id.length - 10)));

                            if ($('#EDITrackingTable').hasClass('editrackingtableActive')) {
                                $('#EDITrackingTable').removeClass('editrackingtableActive');
                                $('#EDITrackingTable').addClass('editrackingtableActive', setTimeout(function () {
                                    GenerateTableBody(TableDataObj);
                                }, 1000));
                            } else {
                                setTimeout(function () {
                                    GenerateTableBody(TableDataObj);
                                    $('#EDITrackingTable').addClass('editrackingtableActive');
                                }, 1000);
                            }
                            $('#NoteId').remove();
                            //var headerObj = UpdateHeader(parseInt(currentElement.id.substr(11, currentElement.id.length - 10)));

                            $('circle').removeAttr('style');
                            _.each(trackingArray[parseInt(currentElement.id.substr(11, currentElement.id.length - 10))].highlightCircle, function (ele) {
                                $('#circle' + ele).css('fill', '#20709f');
                            });

                            $('line').removeAttr('style');
                            _.each(trackingArray[parseInt(currentElement.id.substr(11, currentElement.id.length - 10))].highlightLine, function (ele) {
                                $('#' + ele).css('stroke', '#20709f');
                            });

                           
                        });
            } else {

                template = '<div class="status-div ' + trackingArray[i].type + '-status-div ' + trackingArray[i].type + '-status-div-lower"><div class="status-header"><div class="status-text"><span class="' + trackingArray[i].type + '-text">' + trackingArray[i].statusText + '</span></div></div><div class="status-body"><div class="status-amt"><span class="' + trackingArray[i].type + '-text">$' + nFormatter(trackingArray[i].ClaimedAmt, 2) + '</span></div><div class="status-count"><span class="' + trackingArray[i].type + '-text">#' + nFormatter(trackingArray[i].count, 2) + '</span></div></div><div class="clearfix"></div></div>';
                svgContainer.append('foreignObject')
                     .attr("id", "StatusPanel" + i)
                        .attr('x', parseInt(trackingArray[i].cx) + 1 + '%')
                        .attr('y', parseInt(trackingArray[i].cy) - 3 + '%')
                        .attr('width', 70)
                        .attr('height', 10)
                        .html(template)
                    .on('mouseover', function () {
                        currentElement = $(this).find('.status-div');
                        if (!currentElement.hasClass('active-status-div') && currentElement.hasClass('inactive-status-div'))
                            currentElement.css({ 'transform': 'scale(1.04)', 'transition': 'all linear 0.2s', 'opacity': '1' });

                    })
                .on('mouseout', function () {
                    currentElement = $(this).find('.status-div');
                    currentElement.removeAttr('style');
                })
                .on('click', function () {
                    currentElement = this;
                    $('.status-div').removeClass('active-status-div');
                    $('.status-div').addClass('inactive-status-div');
                    $(currentElement).find('.status-div').addClass('active-status-div');
                    $(currentElement).find('.status-div').removeClass('inactive-status-div');
                    $(currentElement).find('.status-div').removeAttr('style');
                    console.log("  Status Panel id : " + parseInt(currentElement.id.substr(11, currentElement.id.length - 10)));
                    var TableDataObj = GetTrackingDataforTable(parseInt(currentElement.id.substr(11, currentElement.id.length - 10)));
                    if ($('#EDITrackingTable').hasClass('editrackingtableActive')) {
                        $('#EDITrackingTable').removeClass('editrackingtableActive');
                        $('#EDITrackingTable').addClass('editrackingtableActive', setTimeout(function () {
                            GenerateTableBody(TableDataObj);
                        }, 1000));
                    } else {
                        setTimeout(function () {
                            GenerateTableBody(TableDataObj);
                            $('#EDITrackingTable').addClass('editrackingtableActive');
                        }, 1000);
                    }
                    $('#NoteId').remove();
                    //var headerObj = UpdateHeader(parseInt(currentElement.id.substr(11, currentElement.id.length - 10)));

                    $('circle').removeAttr('style');
                    _.each(trackingArray[parseInt(currentElement.id.substr(11, currentElement.id.length - 10))].highlightCircle, function (ele) {
                        $('#circle' + ele).css('fill', '#20709f');
                    });

                    $('line').removeAttr('style');
                    _.each(trackingArray[parseInt(currentElement.id.substr(11, currentElement.id.length - 10))].highlightLine, function (ele) {
                        $('#' + ele).css('stroke', '#20709f');
                    });

                  
                });
            }



            svgContainer.append("circle")
                          .attr("id", "circle" + i)
                          .attr("class", "circle " + trackingArray[i].type + "-circle")
                          .attr("cx", trackingArray[i].cx + '%')
                          .attr("cy", trackingArray[i].cy + '%');
            
        }

        
    }


    var countList = [
        { "ClaimCount": 421, 'ClaimedAmt': 8937, "Status": "0" },
        { "ClaimCount": 598, 'ClaimedAmt': 8937, "Status": "1" },
        { "ClaimCount": 324, 'ClaimedAmt': 8937, "Status": "2" },
        { "ClaimCount": 523, 'ClaimedAmt': 8937, "Status": "3" },
        { "ClaimCount": 17, 'ClaimedAmt': 8937, "Status": "4" },
        { "ClaimCount": 17, 'ClaimedAmt': 8937, "Status": "5" },
        { "ClaimCount": 17, 'ClaimedAmt': 8937, "Status": "6" },
        { "ClaimCount": 635, 'ClaimedAmt': 8937, "Status": "7" },
        { "ClaimCount": 129, 'ClaimedAmt': 8937, "Status": "8" },
        { "ClaimCount": 523, 'ClaimedAmt': 8937, "Status": "9" },
        { "ClaimCount": 17, 'ClaimedAmt': 8937, "Status": "10" },
        { "ClaimCount": 635, 'ClaimedAmt': 8937, "Status": "11" },
        { "ClaimCount": 129, 'ClaimedAmt': 8937, "Status": "12" },
        { "ClaimCount": 598, 'ClaimedAmt': 8937, "Status": "13" },
        { "ClaimCount": 856, 'ClaimedAmt': 8956, "Status": "14" }];

    DrawStatusBar(countList);

    
    reqdata = [];
    function GetTrackingDataforTable(id) {
        $.ajax({
            type: "GET",
            url: "/EDITracking/GetTrackingData",
            data: { input: id },
            dataType: "json",
            async: false,
            success: function (data) {
                console.log(data);
                reqdata = data;
            }
        });
        return reqdata;
    }

    function GenerateTableBody(TableData) {
        $("#EDITableBody").html("");
        $("#EDITableHeader").html("");
        $("#TitleDiv").html("");
        var ttitle = "";
        var thead = "";
        var tbody = "";
        var tbdy = [];
        var td = "";
        ttitle = ttitle + '<div class="TableTitle" style="text-align:center;margin-bottom:8px">' + '<a class="selected-status">' + TableData.TTitle + '</a>' + '</div>' + 
                                                                     '<div class="straight-line">' + '</div>';
        $("#TitleDiv").append(ttitle);
        for (var i = 0; i < TableData.TBodyData.length; i++)
        {
            td = "";
            td = td + '<tr>';
            if (i == 0)
            {
                for (var proper in TableData.THeadData)
                {
                    thead = thead + '<th>' + TableData.THeadData[proper] + '</th>';
                }

            }
            for (var prop in TableData.TBodyData[i])
            {
                td = td + '<td>' + TableData.TBodyData[i][prop] + '</td>';
            }
            if (TableData.TTitle === "EOB Received") {
                td = td + '<td>' + ' <a class="btn btn-primary btn-xs" style="Padding:0px 1px">View Claim</a>'  +' <a class="btn btn-primary btn-xs" style="Padding:0px 1px">View EOB</a>'  + '</td>';
            } else {
                td = td + '<td>' + ' <a class="btn btn-primary btn-xs" style="Padding:0px 1px">View Claim</a>' + '</td>';
            }
            td = td + '</tr>';
            $("#EDITableBody").append(td);        
        }
        $("#EDITableHeader").append(thead);
        //if (TableData.TBodyData.length>8)
        //{
        //      var divHeightToBeSet = (TableData.TBodyData.length + 1) * 50 + 'px';
        //        $('#mainBody > div:nth-child(5)').height(divHeightToBeSet);
        //}
      
        
    }

})