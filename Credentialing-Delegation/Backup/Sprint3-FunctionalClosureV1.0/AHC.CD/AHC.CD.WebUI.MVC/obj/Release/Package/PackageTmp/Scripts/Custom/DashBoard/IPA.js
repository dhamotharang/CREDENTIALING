
var providerApp = angular.module('example', ['angularCharts']);

providerApp.controller('MainController', function ($scope) {
    $scope.data = lineChartData();

    $scope.chartType = 'line';

    $scope.ChangeGraph = function (chartType) {
        $scope.chartType = chartType;

        if (chartType == 'pie') {
            $scope.data = pieChartData();

        }
        else if (chartType == 'line') {
            $scope.data = lineChartData();
        }

    }

    function pieChartData() {
        return {
            series: [],
            data: [{
                x: "Initiated",
                y: [100],
                tooltip: "this is tooltip"
            }, {
                x: "Submitted",
                y: [300]
            }, {
                x: "Verified",
                y: [351]
            }, {
                x: "Completed",
                y: [154]
            }, {
                x: "Rejected",
                y: [23]
            }]
        };
    }


    function lineChartData() {
        return {
            series: ['Submitted', 'Completed', 'Rejected'],
            data: [{
                x: "2011",
                y: [250, 170, 80],
                tooltip: "this is tooltip"
            }, {
                x: "2012",
                y: [300, 289, 111]
            }, {
                x: "2013",
                y: [351, 70, 281]
            }, {
                x: "2014",
                y: [454, 341, 113]
            }]
        };
    }



    $scope.messages = [];

    $scope.config = {
        labels: false,
        title: "",
        legend: {
            display: true,
            position: 'right'
        },
        click: function (d) {
            $scope.messages.push('clicked!');
        },
        mouseover: function (d) {
            $scope.messages.push('mouseover!');
        },
        mouseout: function (d) {
            $scope.messages.push('mouseout!');
        },
        innerRadius: 0,
        lineLegend: 'lineEnd',
    };
});


var allPlans = function () {
    $(".plancontainer").find(".panel").slideDown();
    $(".allplan").hide();
};

var togglePlan = function (classname) {
    //$(".plancontainer").find(".panel [style*='display:none;']").show();
    $(".plancontainer").find(".panel").not("." + classname).slideUp(function () {
        $(".plancontainer").find("." + classname).slideDown();
        $(".allplan").show();
    });
};