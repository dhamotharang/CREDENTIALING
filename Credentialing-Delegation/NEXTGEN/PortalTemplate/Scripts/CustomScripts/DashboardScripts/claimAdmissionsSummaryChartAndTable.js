/*Script for Claims Summary Chart and Table*/
$(function () {

    function initializeAdmissionsChart(id, data, category) {
        c3.generate({
            bindto: id,
            data: {
                x: 'x',
                columns: data,
                type: 'bar',
                labels: true,
                groups: [
                    ['Sniff', 'Inpatient']
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
                pattern: ['green', '#0d47a1', '#ff7f0e', '#ffbb78', '#2ca02c', '#98df8a', '#d62728', '#ff9896', '#9467bd', '#c5b0d5', '#8c564b', '#c49c94', '#e377c2', '#f7b6d2', '#7f7f7f', '#c7c7c7', '#bcbd22', '#dbdb8d', '#17becf', '#9edae5']
            }
        });
    };


    function switchAdmissionCharts(dataWeekly, dataMonthly, dataQuarterly, dataHalfYearly, dataYearly) {
        // Weekly Auth Summary Chart//
        var Weeks = ["Monday", "Tuesday", "Wednesday", "Thursday", "Friday", "Saturday",
       "Sunday"];
        var chart = initializeAdmissionsChart('#Admissions_Weekly_chart', dataWeekly, Weeks);

        // Monthly Auth Summary Chart//
        var monthNames = ["January", "February", "March", "April", "May", "June",
       "July", "August", "September", "October", "November", "December"];
        var chart = initializeAdmissionsChart('#Admissions_Monthly_chart', dataMonthly, monthNames);

        // Quarterly Auth Summary Chart//
        var Quarters = ["Jan-March", "April-June", "July-September", "Oct-Dec"];
        var chart = initializeAdmissionsChart('#Admissions_Quarterly_chart', dataQuarterly, Quarters);

        // Half-Yearly Auth Summary Chart//
        var HalfYears = ["January", "February", "March", "April", "May", "June"];
        var chart = initializeAdmissionsChart('#Admissions_Halfyearly_chart', dataHalfYearly, HalfYears);

        // Yearly Auth Summary Chart//
        var Years = ["2016", "2015", "2014", "2013", "2012"];
        var chart = initializeAdmissionsChart('#Admissions_Yearly_chart', dataYearly, Years);

    }

    function generateAdmissionsTableBody(admissionType, dataObject) {
        var length = dataObject[0].length;
        var standardCount = dataObject[1];
        var expediteCount = dataObject[2];
        var categoryValue = dataObject[0];
        var tbody = "";
        for (var i = 1; i < length; i++) {
            tbody = tbody + '<tr>' +
               '<th>' + i + '</th>' +
               '<th><span class="h6">' + admissionType + '</span></th>' +
               '<td>' + categoryValue[i] + '</td>' +
               '<td><span class="h6"><b>' + standardCount[i] + '</b></span></td>' +
               '<td><span class="h6"><b>' + expediteCount[i] + '</b></span></td>' +
                '</tr>';
        }
        return tbody;
    };

    function resetAdmissionsTableBody() {
        $("#admissionsWeeklyTableBody").html("");
        $("#admissionsMonthlyTableBody").html("");
        $("#admissionsQuarterlyTableBody").html("");
        $("#admissionsHalfYearlyTableBody").html("");
        $("#admissionsYearlyTableBody").html("");
    };

    function initializeAdmissionsTable(admissionType) {
        $("#admissionsWeeklyTableBody").append(generateAdmissionsTableBody(admissionType, claimAdmissionsPendingWeekly));
        $("#admissionsMonthlyTableBody").append(generateAdmissionsTableBody(admissionType, claimAdmissionsPendingMonthly));
        $("#admissionsQuarterlyTableBody").append(generateAdmissionsTableBody(admissionType, claimAdmissionsPendingQuarterly));
        $("#admissionsHalfYearlyTableBody").append(generateAdmissionsTableBody(admissionType, claimAdmissionsPendingHalfYearly));
        $("#admissionsYearlyTableBody").append(generateAdmissionsTableBody(admissionType, claimAdmissionsPendingYearly));
    };

    function switchAdmissionsTable(type) {
        resetAdmissionsTableBody();
        switch (type) {
            case "1":
                initializeAdmissionsTable("Admissions Pending");
                break;
            case "2":
                admissionType = "Admissions Submitted";
                $("#admissionsWeeklyTableBody").append(generateAdmissionsTableBody(admissionType, claimAdmissionsSubmittedWeekly));
                $("#admissionsMonthlyTableBody").append(generateAdmissionsTableBody(admissionType, claimAdmissionsSubmittedMonthly));
                $("#admissionsQuarterlyTableBody").append(generateAdmissionsTableBody(admissionType, claimAdmissionsSubmittedQuarterly));
                $("#admissionsHalfYearlyTableBody").append(generateAdmissionsTableBody(admissionType, claimAdmissionsSubmittedHalfYearly));
                $("#admissionsYearlyTableBody").append(generateAdmissionsTableBody(admissionType, claimAdmissionsSubmittedYearly));
                break;
            case "3":
                admissionType = "Admissions Completed";
                $("#admissionsWeeklyTableBody").append(generateAdmissionsTableBody(admissionType, claimAdmissionsCompletedWeekly));
                $("#admissionsMonthlyTableBody").append(generateAdmissionsTableBody(admissionType, claimAdmissionsCompletedMonthly));
                $("#admissionsQuarterlyTableBody").append(generateAdmissionsTableBody(admissionType, claimAdmissionsCompletedQuarterly));
                $("#admissionsHalfYearlyTableBody").append(generateAdmissionsTableBody(admissionType, claimAdmissionsCompletedHalfYearly));
                $("#admissionsYearlyTableBody").append(generateAdmissionsTableBody(admissionType, claimAdmissionsCompletedYearly));
                break;
        }
    }
    initializeAdmissionsTable("Admissions Pending");
    switchAdmissionCharts(claimAdmissionsPendingWeekly, claimAdmissionsPendingMonthly, claimAdmissionsPendingQuarterly, claimAdmissionsPendingHalfYearly, claimAdmissionsPendingYearly);
    //Transistions for Graph and Table//
    var admissionsGraphTypes = ['Admissions_Weekly', 'Admissions_Monthly', 'Admissions_Quarterly', 'Admissions_Halfyearly', 'Admissions_Yearly'];
    var admissions_chart_id, admissions_table_id;
    var isAdmissionsTableView = false;
    var isAdmissionsChartView = true;

    $('.admissionsViewlist .btn-default').click(function () {
        id = $(this).attr('id');
        if (id === "chart") {
            $("#" + id).css("background-color", "skyblue");
            $("#" + "table").css("background-color", "white");
            $('.admissions_table.active').removeClass('viewActive');
            $('.admissions_chart.active').addClass('viewActive');
            isAdmissionsTableView = false;
        } else if (id === "table") {
            $("#" + id).css("background-color", "skyblue");
            $("#" + "chart").css("background-color", "white");
            $('.admissions_chart.active').removeClass('viewActive');
            $('.admissions_table.active').addClass('viewActive');
            isAdmissionsTableView = true;
        }
    });


    $('.AdmissionsSummary .btn-default').click(function (e) {
        id = $(this).attr('id');
        for (var i = 0; i < admissionsGraphTypes.length; i++) {
            if (admissionsGraphTypes[i] !== id) {
                if ($("#" + admissionsGraphTypes[i]).css("background-color")) {
                    $("#" + admissionsGraphTypes[i]).css("background-color", "");
                }
            }
            else {
                $("#" + admissionsGraphTypes[i]).css("background-color", "skyblue");
                admissions_chart_id = "#" + admissionsGraphTypes[i] + "_chart";
                admissions_table_id = "#" + admissionsGraphTypes[i] + "_table";
            }
        }
        function showSelectedGraph() {
            setTimeout(function () {
                $('.admissions_chart').removeClass("active viewActive");
                if (!isAdmissionsTableView) $(admissions_chart_id).addClass("active viewActive");
                else $(admissions_chart_id).addClass("active");
            }, 1000);
        }
        function showSelectedTable() {
            setTimeout(function () {
                $('.admissions_table').removeClass("active viewActive");
                if (isAdmissionsTableView) $(admissions_table_id).addClass("active viewActive");
                else $(admissions_table_id).addClass("active");
            }, 1000);
        }
        $('.admissions_chart').hide("slide", { direction: "left" }, 1000, showSelectedGraph());
        $('.admissions_table').hide("slide", { direction: "left" }, 1000, showSelectedTable());
    });

    $('select[name="DropdownforAdmissionChart"]').change(function () {
        if ($(this).val() !== "" || $(this).val() !== null) {
            switch ($(this).val()) {
                case "1":
                    switchAdmissionCharts(claimAdmissionsPendingWeekly, claimAdmissionsPendingMonthly, claimAdmissionsPendingQuarterly, claimAdmissionsPendingHalfYearly, claimAdmissionsPendingYearly);
                    switchAdmissionsTable($(this).val());
                    break;
                case "2":
                    switchAdmissionCharts(claimAdmissionsSubmittedWeekly, claimAdmissionsSubmittedMonthly, claimAdmissionsSubmittedQuarterly, claimAdmissionsSubmittedHalfYearly, claimAdmissionsSubmittedYearly);
                    switchAdmissionsTable($(this).val());
                    break;
                case "3":
                    switchAdmissionCharts(claimAdmissionsCompletedWeekly, claimAdmissionsCompletedMonthly, claimAdmissionsCompletedQuarterly, claimAdmissionsCompletedHalfYearly, claimAdmissionsCompletedYearly);
                    switchAdmissionsTable($(this).val());
                    break;
            }
        }

    });
});
