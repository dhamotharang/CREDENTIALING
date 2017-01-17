
var planApp = angular.module('planApp', ['angularCharts']);

planApp.controller('PlanController', function ($scope) {
    
    $scope.data = data1();

    $scope.chartType = 'bar';

    $scope.ChangeGraph = function (chartType) {
        
        if (chartType == 'specialty') {
            $scope.chartType = 'bar';
            $scope.data = data1();

        }
        else if (chartType == 'license') {
            $scope.chartType = 'bar';
            $scope.data = data2();
        }
        else if (chartType == 'location') {
            $scope.chartType = 'pie';
            $scope.data = data3();
        }

    }

    function data1() {
        return {
            series: ['Initiated', 'Completed', 'Rejected'],
            data: [{
                x: "Physician",
                y: [100, 300, 80],
                tooltip: "this is tooltip"
            }, {
                x: "Nurse",
                y: [159, 289, 90]
            }, {
                x: "Social Worker",
                y: [180, 430, 75]
            }, {
                x: "Therapist",
                y: [88, 341, 30]
            }, {
                x: "Facility",
                y: [159, 289, 90]
            }]
        };
    }


    function data2() {
        return {
            series: ['License', 'Recredentialing'],
            data: [{
                x: "Physician",
                y: [100, 98],
                tooltip: "this is tooltip"
            }, {
                x: "Nurse",
                y: [78, 67]
            }, {
                x: "Social Worker",
                y: [180, 75]
            }, {
                x: "Therapist",
                y: [88, 90]
            }, {
                x: "Facility",
                y: [78, 67]
            }]
        };
    }

    function data3() {
        return {
            series: ['Providers'],
            data: [{
                x: "Hernando",
                y: [50],
                tooltip: "this is tooltip"
            }, {
                x: "Citrus",
                y: [89]
            }, {
                x: "SYLACAUGA",
                y: [77]
            }, {
                x: "LINCOLN",
                y: [88]
            },{
                x: "Others",
                y: [25]
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
        colors: ['#3399FF', '#33CC33', '#CC0000'],
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

