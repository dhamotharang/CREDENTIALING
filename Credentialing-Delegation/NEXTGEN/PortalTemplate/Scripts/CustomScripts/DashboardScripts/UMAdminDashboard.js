$(document).ready(function () {

    function initializeChart(id, data, category) {
        c3.generate({
            bindto: id,
            data: {
                x: 'x',
                columns: data,
                type: 'bar',
                labels: true,
                groups: [
                    ['Standard', 'Expedited']
                ]
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
                }
            },

            grid: {

                y: {
                    lines: [{ value: 0 }]
                }
            }, color: {
                pattern: ['#d58512', '#3C8DBC', '#ff7f0e', '#ffbb78', '#2ca02c', '#98df8a', '#d62728', '#ff9896', '#9467bd', '#c5b0d5', '#8c564b', '#c49c94', '#e377c2', '#f7b6d2', '#7f7f7f', '#c7c7c7', '#bcbd22', '#dbdb8d', '#17becf', '#9edae5']
            },
        });
    };


    function switchCharts(dataWeekly, dataMonthly, dataQuarterly, dataHalfYearly, dataYearly) {
        // Weekly Auth Summary Chart//
        var Weeks = ["Monday", "Tuesday", "Wednesday", "Thursday", "Friday", "Saturday",
       "Sunday"];
        var chart = initializeChart('#Weekly_chart', dataWeekly, Weeks);

        // Monthly Auth Summary Chart//
        var monthNames = ["January", "February", "March", "April", "May", "June",
       "July", "August", "September", "October", "November", "December"];
        var chart = initializeChart('#Monthly_chart', dataMonthly, monthNames);

        // Quarterly Auth Summary Chart//
        var Quarters = ["Jan-March", "April-June", "July-September", "Oct-Dec"];
        var chart = initializeChart('#Quarterly_chart', dataQuarterly, Quarters);

        // Half-Yearly Auth Summary Chart//
        var HalfYears = ["January", "February", "March", "April", "May", "June"];
        var chart = initializeChart('#Halfyearly_chart', dataHalfYearly, HalfYears);

        // Yearly Auth Summary Chart//
        var Years = ["2016", "2015", "2014", "2013", "2012"];
        var chart = initializeChart('#Yearly_chart', dataYearly, Years);

    }

    function generateTableBody(authType, dataObject) {
        var length = dataObject[0].length;
        var standardCount = dataObject[1];
        var expediteCount = dataObject[2];
        var categoryValue = dataObject[0];
        var tbody = "";
        for (var i = 1; i < length; i++) {
            tbody = tbody + '<tr>' +
               '<th>' + i + '</th>' +
               '<th><span class="h6">' + authType + '</span></th>' +
               '<td>' + categoryValue[i] + '</td>' +
               '<td><span class="h6"><b>' + standardCount[i] + '</b></span></td>' +
               '<td><span class="h6"><b>' + expediteCount[i] + '</b></span></td>' +
                '</tr>';
        }
        return tbody;
    };

    function resetTableBody() {
        $("#weeklyTableBody").html("");
        $("#monthlyTableBody").html("");
        $("#quarterlyTableBody").html("");
        $("#halfYearlyTableBody").html("");
        $("#yearlyTableBody").html("");
    };

    function initializeTable(authType) {
        $("#weeklyTableBody").append(generateTableBody(authType, dataPendingWeekly));
        $("#monthlyTableBody").append(generateTableBody(authType, dataPendingMonthly));
        $("#quarterlyTableBody").append(generateTableBody(authType, dataPendingQuarterly));
        $("#halfYearlyTableBody").append(generateTableBody(authType, dataPendingHalfYearly));
        $("#yearlyTableBody").append(generateTableBody(authType, dataPendingYearly));
    };

    function switchTable(type) {
        resetTableBody();
        switch (type) {
            case "1":
                initializeTable("Pending Auth");
                break;
            case "2":
                authType = "Submitted Auth";
                $("#weeklyTableBody").append(generateTableBody(authType, dataSubmittedWeekly));
                $("#monthlyTableBody").append(generateTableBody(authType, dataSubmittedMonthly));
                $("#quarterlyTableBody").append(generateTableBody(authType, dataSubmittedQuarterly));
                $("#halfYearlyTableBody").append(generateTableBody(authType, dataSubmittedHalfYearly));
                $("#yearlyTableBody").append(generateTableBody(authType, dataSubmittedYearly));
                break;
            case "3":
                authType = "Completed Auth";
                $("#weeklyTableBody").append(generateTableBody(authType, dataCompletedWeekly));
                $("#monthlyTableBody").append(generateTableBody(authType, dataCompletedMonthly));
                $("#quarterlyTableBody").append(generateTableBody(authType, dataCompletedQuarterly));
                $("#halfYearlyTableBody").append(generateTableBody(authType, dataCompletedHalfYearly));
                $("#yearlyTableBody").append(generateTableBody(authType, dataCompletedYearly));
                break;
        }
    }

    initializeTable("Pending Auth");

    switchCharts(dataPendingWeekly, dataPendingMonthly, dataPendingQuarterly, dataPendingHalfYearly, dataPendingYearly);



    //Transistions for Graph and Table//
    var graphtypes = ['Weekly', 'Monthly', 'Quarterly', 'Halfyearly', 'Yearly'];
    var chart_id, table_id;
    var isTableView = false;
    var isChartView = true;

    $('.viewlist .btn-default').click(function () {
        id = $(this).attr('id');
        if (id === "chart") {
            $("#" + id).css("background-color", "skyblue");
            $("#" + "table").css("background-color", "white");
            $('.auth_table.active').removeClass('viewActive');
            $('.auth_chart.active').addClass('viewActive');
            isTableView = false;
        } else if (id === "table") {
            $("#" + id).css("background-color", "skyblue");
            $("#" + "chart").css("background-color", "white");
            $('.auth_chart.active').removeClass('viewActive');
            $('.auth_table.active').addClass('viewActive');
            isTableView = true;
        }
    });


    $('.AuthSummary .btn-default').click(function (e) {
        id = $(this).attr('id');
        for (var i = 0; i < graphtypes.length; i++) {
            if (graphtypes[i] !== id) {
                if ($("#" + graphtypes[i]).css("background-color")) {
                    $("#" + graphtypes[i]).css("background-color", "");
                }
            }
            else {
                $("#" + graphtypes[i]).css("background-color", "skyblue");
                chart_id = "#" + graphtypes[i] + "_chart";
                table_id = "#" + graphtypes[i] + "_table";
            }
        }
        function showSelectedGraph() {
            setTimeout(function () {
                $('.auth_chart').removeClass("active viewActive");
                if (!isTableView) $(chart_id).addClass("active viewActive");
                else $(chart_id).addClass("active");
            }, 1000);
        }
        function showSelectedTable() {
            setTimeout(function () {
                $('.auth_table').removeClass("active viewActive");
                if (isTableView) $(table_id).addClass("active viewActive");
                else $(table_id).addClass("active");
            }, 1000);
        }
        $('.auth_chart').hide("slide", { direction: "left" }, 1000, showSelectedGraph());
        $('.auth_table').hide("slide", { direction: "left" }, 1000, showSelectedTable());
    });

    $('select[name="DropdownforChart"]').change(function () {
        if ($(this).val() !== "" || $(this).val() !== null) {
            switch ($(this).val()) {
                case "1":
                    switchCharts(dataPendingWeekly, dataPendingMonthly, dataPendingQuarterly, dataPendingHalfYearly, dataPendingYearly);
                    switchTable($(this).val());
                    break;
                case "2":
                    switchCharts(dataSubmittedWeekly, dataSubmittedMonthly, dataSubmittedQuarterly, dataSubmittedHalfYearly, dataSubmittedYearly);
                    switchTable($(this).val());
                    break;
                case "3":
                    switchCharts(dataCompletedWeekly, dataCompletedMonthly, dataCompletedQuarterly, dataCompletedHalfYearly, dataCompletedYearly);
                    switchTable($(this).val());
                    break;
            }
        }

    });
});





