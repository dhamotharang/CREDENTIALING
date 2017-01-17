
var providerApp = angular.module('example', ['angularCharts']);

providerApp.controller('MainController', function ($scope) {
    $scope.data = {
        series: ['Sales', 'Income', 'Expense'],
        data: [{
            x: "Jack",
            y: [100, 210, 384],
            tooltip: "this is tooltip"
        }, {
            x: "John",
            y: [300, 289, 456]
        }, {
            x: "Stacy",
            y: [351, 170, 255]
        }, {
            x: "Luke",
            y: [54, 341, 879]
        }]
    };

    $scope.chartType = 'bar';

    $scope.messages = [];

    $scope.config = {
        labels: false,
        title: "Products",
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

