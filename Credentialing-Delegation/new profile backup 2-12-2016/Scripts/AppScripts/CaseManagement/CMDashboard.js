$(document).ready(function () {
    reqDistributionViewVar = false;
    reqConversionViewVar = false;
    reqDistributionYaxis = 'Week';
    reqConversionYaxis = 'Week';
    function GetDashboardData(UserId) {
        $.ajax({
            type: "GET",
            url: "/CaseManagement/GetDashboardData",
            data: { input: UserId },
            dataType: "json",
            async: false,
            success: function (data) {
                console.log(data);
                userData = data;
            }
        });
        return userData;
    }
    var UserData = GetDashboardData();
    $("#reqDistributionTable").hide
    $("#reqConversionTable").hide();
    reqDistributionChart = c3.generate({
        bindto: '#reqDistributionChart',
        data: {
            x: 'x',
            columns: UserData.RequestDistribution.WeeklyDistribution,
            labels: true
        },
        axis: {
            x: {
                type: 'category',
                categories: ['Week 1', 'Week 2', 'Week 3', 'Week 4']
            },
            y: {
                label: { // ADD
                    text: 'No. Of Requests',
                    position: 'outer-middle'
                }
            },
        },
    });
    //reqDistributionChart.resize({ height: 320, width: 580 })
    reqConversionChart = c3.generate({
        bindto: '#reqConversionChart',
        data: {
            x: 'x',
            columns: UserData.RequestConversion.WeeklyConversion,
            labels:true,
            type: 'bar',
            groups: [
                ['CM', 'DM', 'NC', 'Review']
            ]
        },
        color: {
            pattern: ['#ff9896', '#9467bd', '#c5b0d5', '#8c564b']
        },
        bar: {
            width: {
                ratio: 0.25
            }
        },
        axis: {
            x: {
                type: 'category',
                categories: ['Week 1', 'Week 2', 'Week 3', 'Week 4']
            },
            y: {
                label: { // ADD
                    text: 'No. Of Requests',
                    position: 'outer-middle'
                }
            },
        },
        grid: {
            y: {
                lines: [{ value: 0 }]
            }
        }
    });

    $("#reqDistributionTableContent").html("");
    ChangeRequestDistribution = function (yaxis) {
        reqDistributionYaxis = yaxis;
        var data;
        switch (yaxis) {
            case 'Week':
                data = UserData.RequestDistribution.WeeklyDistribution;
                break;
            case 'Month':
                data = UserData.RequestDistribution.MonthlyDistribution;
                break;
            case 'Year':
                data = UserData.RequestDistribution.YearlyDistribution;
                break;
        }
        reqDistributionChart.load({
            columns: data,
            unload: reqDistributionChart.columns,
        });
    }
    ChangeRequestConversion = function (yaxis) {
        reqConversionYaxis = yaxis;
        var data;
        switch (yaxis) {
            case 'Week':
                data = UserData.RequestConversion.WeeklyConversion;
                break;
            case 'Month':
                data = UserData.RequestConversion.MonthlyConversion;
                break;
            case 'Year':
                data = UserData.RequestConversion.YearlyConversion;
                break;
        }
        reqConversionChart.load({
            columns: data,
            unload: reqConversionChart.columns,
        });
    }
    ChangeRequestDistributionView = function (viewVar) {
        if(viewVar){
            reqDistributionViewVar = false;
            $("#reqDistributionTable").hide("1000");
            $("#reqDistributionChart").show("1000");
      
        }
        else {
            reqDistributionViewVar = true;
            $("#reqDistributionChart").hide("1000");
            $("#reqDistributionTable").show("1000");
        }
    }
    ChangeRequestConversionView = function (viewVar) {
        if (viewVar) {
            reqConversionViewVar = false;
            $("#reqConversionTable").hide("1000");
            $("#reqConversionChart").show("1000");

        }
        else {
            reqConversionViewVar = true;
            $("#reqConversionChart").hide("1000");
            $("#reqConversionTable").show("1000");
        }
    }

});
