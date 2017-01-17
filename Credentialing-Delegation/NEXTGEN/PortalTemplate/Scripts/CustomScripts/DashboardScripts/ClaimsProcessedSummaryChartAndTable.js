/*Script for Claims Summary Chart and Table*/
$(document).ready(function () {

    function initializeClaimsChart(id, data, category) {
        totalCount = [190, 110, 300, 340, 600, 300, 300];
        c3.generate({
            bindto: id,
            data: {
                x: 'x',
                columns: data,
                type: 'bar',
                labels: {
                    format: {
                        'CAP': d3.format('$'),
                        'FFS': d3.format('$'),
                    }
                },
                groups: [
                    ['CAP', 'FFS']
                ],
                
            },
            bar: {
                width: {
                    ratio: 0.25
                }
            },
            axis: {
                x: {
                    type: 'category',
                    categories: category
                },
                y: {
                    label: 'Count',
                }
            },

            grid: {
                y: {
                    lines: [{ value: 0 }]
                }
            }, color: {
                pattern: ['#16a085', '#bf2718', '#ff7f0e', '#ffbb78', '#2ca02c', '#98df8a', '#d62728', '#ff9896', '#9467bd', '#c5b0d5', '#8c564b', '#c49c94', '#e377c2', '#f7b6d2', '#7f7f7f', '#c7c7c7', '#bcbd22', '#dbdb8d', '#17becf', '#9edae5']
            },
            tooltip: {
                format: {
                   // title: function (d) { return 'Data ' + d; },
                    value: function (value, ratio, id) {
                        var format = id === 'data1' ? d3.format(',') : d3.format('$');
                        return format(value);
                    }
                    //            value: d3.format(',') // apply this format to both y and y2
                }
            }
           
        });
    };


    function switchClaimsCharts(dataWeekly, dataMonthly, dataQuarterly, dataHalfYearly, dataYearly) {
        // Weekly Auth Summary Chart//
        var Weeks = ["Monday", "Tuesday", "Wednesday", "Thursday", "Friday"];
        var chart = initializeClaimsChart('#Claims_Weekly_chart', dataWeekly, Weeks);

        // Monthly Auth Summary Chart//
        var monthNames = ["January", "February", "March", "April", "May", "June",
       "July", "August", "September", "October", "November", "December"];
        var chart = initializeClaimsChart('#Claims_Monthly_chart', dataMonthly, monthNames);

        // Quarterly Auth Summary Chart//
        var Quarters = ["Jan-March", "April-June", "July-September", "Oct-Dec"];
        var chart = initializeClaimsChart('#Claims_Quarterly_chart', dataQuarterly, Quarters);

        // Half-Yearly Auth Summary Chart//
        var HalfYears = ["January", "February", "March", "April", "May", "June"];
        var chart = initializeClaimsChart('#Claims_Halfyearly_chart', dataHalfYearly, HalfYears);

        // Yearly Auth Summary Chart//
        var Years = ["2016", "2015", "2014", "2013", "2012"];
        var chart = initializeClaimsChart('#Claims_Yearly_chart', dataYearly, Years);

    }

    function generateClaimsTableBody(claimType, dataObject) {
        var length = dataObject[0].length;
        var standardCount = dataObject[1];
        var expediteCount = dataObject[2];
        var categoryValue = dataObject[0];
        var tbody = "";
        for (var i = 1; i < length; i++) {
            tbody = tbody + '<tr>' +
               '<th>' + i + '</th>' +
               '<th><span class="h6">' + claimType + '</span></th>' +
               '<td>' + categoryValue[i] + '</td>' +
               '<td><span class="h6"><b>' + standardCount[i] + '</b></span></td>' +
               '<td><span class="h6"><b>' + expediteCount[i] + '</b></span></td>' +
                '</tr>';
        }
        return tbody;
    };

    function resetClaimsTableBody() {
        $("#claimsWeeklyTableBody").html("");
        $("#claimsMonthlyTableBody").html("");
        $("#claimsQuarterlyTableBody").html("");
        $("#claimsHalfYearlyTableBody").html("");
        $("#claimsYearlyTableBody").html("");
    };

    function initializeClaimsTable(claimType) {
        $("#claimsWeeklyTableBody").append(generateClaimsTableBody(claimType, claimsDataPendingWeekly));
        $("#claimsMonthlyTableBody").append(generateClaimsTableBody(claimType, claimsDataPendingMonthly));
        $("#claimsQuarterlyTableBody").append(generateClaimsTableBody(claimType, claimsDataPendingQuarterly));
        $("#claimsHalfYearlyTableBody").append(generateClaimsTableBody(claimType, claimsDataPendingHalfYearly));
        $("#claimsYearlyTableBody").append(generateClaimsTableBody(claimType, claimsDataPendingYearly));
    };

    function switchClaimsTable(type) {
        resetClaimsTableBody();
        switch (type) {
            case "1":
                initializeClaimsTable("Pending Claim");
                break;
            case "2":
                claimType = "Submitted Claim";
                $("#claimsWeeklyTableBody").append(generateClaimsTableBody(claimType, claimsDataSubmittedWeekly));
                $("#claimsMonthlyTableBody").append(generateClaimsTableBody(claimType, claimsDataSubmittedMonthly));
                $("#claimsQuarterlyTableBody").append(generateClaimsTableBody(claimType, claimsDataSubmittedQuarterly));
                $("#claimsHalfYearlyTableBody").append(generateClaimsTableBody(claimType, claimsDataSubmittedHalfYearly));
                $("#claimsYearlyTableBody").append(generateClaimsTableBody(claimType, claimsDataSubmittedYearly));
                break;
            case "3":
                claimType = "Rejected Claim";
                $("#claimsWeeklyTableBody").append(generateClaimsTableBody(claimType, claimsDataCompletedWeekly));
                $("#claimsMonthlyTableBody").append(generateClaimsTableBody(claimType, claimsDataCompletedMonthly));
                $("#claimsQuarterlyTableBody").append(generateClaimsTableBody(claimType, claimsDataCompletedQuarterly));
                $("#claimsHalfYearlyTableBody").append(generateClaimsTableBody(claimType, claimsDataCompletedHalfYearly));
                $("#claimsYearlyTableBody").append(generateClaimsTableBody(claimType, claimsDataCompletedYearly));
                break;
        }
    }
    initializeClaimsTable("Pending Claim");
    switchClaimsCharts(claimsDataPendingWeekly, claimsDataPendingMonthly, claimsDataPendingQuarterly, claimsDataPendingHalfYearly, claimsDataPendingYearly);
    //Transistions for Graph and Table//
    var claimsGraphTypes = ['Claims_Weekly', 'Claims_Monthly', 'Claims_Quarterly', 'Claims_Halfyearly', 'Claims_Yearly'];
    var claims_chart_id, claims_table_id;
    var isClaimsTableView = false;
    var isClaimsChartView = true;

    $('.claimsViewlist .btn-default').click(function () {
        id = $(this).attr('id');
        if (id === "chart") {
            $("#" + id).css("background-color", "skyblue");
            $("#" + "table").css("background-color", "white");
            $('.claims_table.active').removeClass('viewActive');
            $('.claims_chart.active').addClass('viewActive');
            isClaimsTableView = false;
        } else if (id === "table") {
            $("#" + id).css("background-color", "skyblue");
            $("#" + "chart").css("background-color", "white");
            $('.claims_chart.active').removeClass('viewActive');
            $('.claims_table.active').addClass('viewActive');
            isClaimsTableView = true;
        }
    });


    $('.ClaimsSummary .btn-default').click(function (e) {
        id = $(this).attr('id');
        for (var i = 0; i < claimsGraphTypes.length; i++) {
            if (claimsGraphTypes[i] !== id) {
                if ($("#" + claimsGraphTypes[i]).css("background-color")) {
                    $("#" + claimsGraphTypes[i]).css("background-color", "");
                }
            }
            else {
                $("#" + claimsGraphTypes[i]).css("background-color", "skyblue");
                claims_chart_id = "#" + claimsGraphTypes[i] + "_chart";
                claims_table_id = "#" + claimsGraphTypes[i] + "_table";
            }
        }
        function showSelectedGraph() {
            setTimeout(function () {
                $('.claims_chart').removeClass("active viewActive");
                if (!isClaimsTableView) $(claims_chart_id).addClass("active viewActive");
                else $(claims_chart_id).addClass("active");
            }, 1000);
        }
        function showSelectedTable() {
            setTimeout(function () {
                $('.claims_table').removeClass("active viewActive");
                if (isClaimsTableView) $(claims_table_id).addClass("active viewActive");
                else $(claims_table_id).addClass("active");
            }, 1000);
        }
        $('.claims_chart').hide("slide", { direction: "left" }, 1000, showSelectedGraph());
        $('.claims_table').hide("slide", { direction: "left" }, 1000, showSelectedTable());
    });

    $('select[name="DropdownforClaimsChart"]').change(function () {
        if ($(this).val() !== "" || $(this).val() !== null) {
            switch ($(this).val()) {
                case "1":
                    switchClaimsCharts(claimsDataPendingWeekly, claimsDataPendingMonthly, claimsDataPendingQuarterly, claimsDataPendingHalfYearly, claimsDataPendingYearly);
                    switchClaimsTable($(this).val());
                    break;
                case "2":
                    switchClaimsCharts(claimsDataSubmittedWeekly, claimsDataSubmittedMonthly, claimsDataSubmittedQuarterly, claimsDataSubmittedHalfYearly, claimsDataSubmittedYearly);
                    switchClaimsTable($(this).val());
                    break;
                case "3":
                    switchClaimsCharts(claimsDataCompletedWeekly, claimsDataCompletedMonthly, claimsDataCompletedQuarterly, claimsDataCompletedHalfYearly, claimsDataCompletedYearly);
                    switchClaimsTable($(this).val());
                    break;
            }
        }

    });
});
