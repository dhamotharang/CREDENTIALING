
//var providerApp = angular.module('example', ['angularCharts']);

//providerApp.controller('MainController', function ($scope) {
//    $scope.data = lineChartData();

//    $scope.chartType = 'line';

//    $scope.ChangeGraph = function (chartType) {
//        $scope.chartType = chartType;

//        if (chartType == 'pie') {
//            $scope.data = pieChartData();

//        }
//        else if (chartType == 'line') {
//            $scope.data = lineChartData();
//        }

//    }

//    function pieChartData() {
//        return {
//            series: [],
//            data: [{
//                x: "Initiated",
//                y: [100],
//                tooltip: "this is tooltip"
//            }, {
//                x: "Submitted",
//                y: [300]
//            }, {
//                x: "Verified",
//                y: [351]
//            }, {
//                x: "Completed",
//                y: [154]
//            }, {
//                x: "Rejected",
//                y: [23]
//            }]
//        };
//    }


//    function lineChartData() {
//        return {
//            series: ['Submitted', 'Completed', 'Rejected'],
//            data: [{
//                x: "2011",
//                y: [250, 170, 80],
//                tooltip: "this is tooltip"
//            }, {
//                x: "2012",
//                y: [300, 289, 111]
//            }, {
//                x: "2013",
//                y: [351, 70, 281]
//            }, {
//                x: "2014",
//                y: [454, 341, 113]
//            }]
//        };
//    }



//    $scope.messages = [];

//    $scope.config = {
//        labels: false,
//        title: "",
//        legend: {
//            display: true,
//            position: 'right'
//        },
//        click: function (d) {
//            $scope.messages.push('clicked!');
//        },
//        mouseover: function (d) {
//            $scope.messages.push('mouseover!');
//        },
//        mouseout: function (d) {
//            $scope.messages.push('mouseout!');
//        },
//        innerRadius: 0,
//        lineLegend: 'lineEnd',
//    };
//});

angular.module('example', ['nvd3'])

    .controller('MainController', function ($scope) {
        
        $scope.options = GrowthScope();
        $scope.data = GrowthData();

        $scope.ChangeGraph = function (chartType) {
            $scope.chartType = chartType;

            if (chartType == 'Growth') {
                $scope.options = GrowthScope();
                $scope.data = GrowthData();

            }
            else if (chartType == 'Group') {
                $scope.options = GroupScope();
                $scope.data = GroupData();
            }

        }

        function GrowthScope() {
             return {
                chart: {
                    type: 'lineChart',
                    height: 265,
                    margin: {
                        top: 20,
                        right: 20,
                        bottom: 40,
                        left: 55
                    },
                    x: function (d) { return d.x; },
                    y: function (d) { return d.y; },
                    useInteractiveGuideline: true,
                    xScale: d3.time.scale(),
                    xAxis: {
                        axisLabel: 'Year',
                        tickFormat: function (d) {
                            return d ? d3.time.format('%b')(new Date(d)) : '';
                        }
                    },
                    yAxis: {
                        axisLabel: 'No. of Credential Application',
                        tickFormat: function (d) {
                            return d3.format('.02f')(d);
                        }
                    },
                    callback: function (chart) {
                        console.log("!!! lineChart callback !!!");
                    },
                    interpolate: "cardinal"
                },
                title: {
                    enable: true,
                    text: 'Credential Growth'
                }
            };
        };

        function GrowthData() {
            var submitted = [
            { x: new Date('Jan-2014'), y: 100 },
            {x:new Date('Feb-2014'), y:150},
            {x:new Date('Mar-2014'), y:110},
            {x:new Date('Apr-2014'), y:160},
            {x:new Date('May-2014'), y:180},
            {x:new Date('Jun-2014'), y:100},
            {x:new Date('Jul-2014'), y:40},
            {x:new Date('Aug-2014'), y:60},
            {x:new Date('Sep-2014'), y:140},
            {x:new Date('Oct-2014'), y:80},
            {x:new Date('Nov-2014'), y:90},
            {x:new Date('Dec-2014'), y:200}
            ];

            var rejected = [
            { x: new Date('Jan-2014'), y: 20 },
            {x:new Date('Feb-2014'), y:50},
            {x:new Date('Mar-2014'), y:60},
            {x:new Date('Apr-2014'), y:80},
            {x:new Date('May-2014'), y:80},
            {x:new Date('Jun-2014'), y:30},
            {x:new Date('Jul-2014'), y:20},
            {x:new Date('Aug-2014'), y:30},
            {x:new Date('Sep-2014'), y:70},
            {x:new Date('Oct-2014'), y:0},
            {x:new Date('Nov-2014'), y:20},
            { x: new Date('Dec-2014'), y: 50 }
            ];

            var completed = [
            { x: new Date('Jan-2014'), y: 80 },
            {x:new Date('Feb-2014'), y:100},
            {x:new Date('Mar-2014'), y:50},
            {x:new Date('Apr-2014'), y:80},
            {x:new Date('May-2014'), y:100},
            {x:new Date('Jun-2014'), y:70},
            {x:new Date('Jul-2014'), y:20},
            {x:new Date('Aug-2014'), y:30},
            {x:new Date('Sep-2014'), y:70},
            {x:new Date('Oct-2014'), y:80},
            {x:new Date('Nov-2014'), y:70},
            { x: new Date('Dec-2014'), y: 150 }
            ];

            //Line chart data should be sent as an array of series objects.
            return [
                {
                    values: submitted,      //values - represents the array of {x,y} data points
                    key: 'Submitted', //key  - the name of the series.
                    color: '#ff7f0e'  //color - optional: choose your own line color.
                
                },
                {
                    values: rejected,
                    key: 'Rejected',
                    color: '#2ca02c'
                },
                {
                    values: completed,
                    key: 'Completed',
                    color: '#7777ff',
                    curve:true
                    //area: true      //area - set to true if you want this line to turn into a filled area chart.
                }
            ];
        };        
        
        function GroupScope() {
            return {
                //chart: {
                //    type: 'multiBarChart',
                //    height: 265,
                //    margin: {
                //        top: 20,
                //        right: 20,
                //        bottom: 60,
                //        left: 45
                //    },
                //    clipEdge: true,
                //    staggerLabels: true,
                //    transitionDuration: 500,
                //    grouped: true,
                //    x: function (d) { return d.x; },
                //    xAxis: {
                //        //axisLabel: 'Group',
                //        ticksubdivide:10
                //        //axi
                //    },
                //    reduceXTicks: false,
                //    yAxis: {
                //        axisLabel: 'No. of Credentialing Application',
                //        axisLabelDistance: 40,
                //        tickFormat: function (d) {
                //            return d3.format(',.1f')(d);
                //        }
                //    }
                //},
                //title: {
                //    enable: true,
                //    text: 'Groups'
                //}

               chart: {
                        type: 'multiChart',
                        height: 265,
                        margin : {
                            top: 30,
                            right: 30,
                            bottom: 30,
                            left: 30
                        },
                        color: d3.scale.category10().range(),
                        useInteractiveGuideline: true,
                        transitionDuration: 500,
                        staggerLabels: true,
                        grouped: true,
                        x: function (d) { return d.x; },
                        xAxis: {
                            //tickFormat: function(d){
                            //    return d3.format(',f')(d);
                            //}
                        },
                        yAxis1: {
                            axisLabel: 'No. of Credentialing Application',
                            tickFormat: function (d) {
                                return d3.format(',.1f')(d);
                            }
                        },
                        yAxis2: {
                            axisLabel: 'Rejection Rate',
                            tickFormat: function (d) {
                                return d3.format(',.1f')(d);
                            }
                        }
                    }
    
            };
        }

        function GroupData() {

            //var inintData = [
            //    {
            //        key: 'Initiated',
            //        values: [
            //            { series: 0, size: 10, x: 'Group-1', y: 10, y0: 0, y1: 10 },
            //            { series: 0, size: 20, x: 'Group-2', y: 20, y0: 0, y1: 20 },
            //            { series: 0, size: 15, x: 'Group-3', y: 15, y0: 0, y1: 15 },
            //            { series: 0, size: 15, x: 'Group-4', y: 15, y0: 0, y1: 15 },
            //            { series: 0, size: 10, x: 'Group-5', y: 10, y0: 0, y1: 10 },
            //            { series: 0, size: 20, x: 'Group-6', y: 20, y0: 0, y1: 20 },
            //            { series: 0, size: 15, x: 'Group-7', y: 15, y0: 0, y1: 15 },
            //            { series: 0, size: 15, x: 'Group-8', y: 15, y0: 0, y1: 15 },
            //            { series: 0, size: 10, x: 'Group-9', y: 10, y0: 0, y1: 10 },
            //            { series: 0, size: 20, x: 'Group-10', y: 20, y0: 0, y1: 20 },
            //            { series: 0, size: 15, x: 'Group-11', y: 15, y0: 0, y1: 15 }
            //        ]
            //    },
            //    {
            //        key: 'Completed',
            //        values: [
            //            { series: 0, size: 30, x: 'Group-1', y: 30, y0: 10, y1: 40 },
            //            { series: 0, size: 50, x: 'Group-2', y: 50, y0: 20, y1: 70 },
            //            { series: 0, size: 40, x: 'Group-3', y: 40, y0: 15, y1: 55 },
            //            { series: 0, size: 40, x: 'Group-4', y: 40, y0: 15, y1: 55 },
            //            { series: 0, size: 30, x: 'Group-5', y: 30, y0: 10, y1: 40 },
            //            { series: 0, size: 50, x: 'Group-6', y: 50, y0: 20, y1: 70 },
            //            { series: 0, size: 40, x: 'Group-7', y: 40, y0: 15, y1: 55 },
            //            { series: 0, size: 40, x: 'Group-8', y: 40, y0: 15, y1: 55 },
            //            { series: 0, size: 30, x: 'Group-9', y: 30, y0: 10, y1: 40 },
            //            { series: 0, size: 50, x: 'Group-10', y: 50, y0: 20, y1: 70 },
            //            { series: 0, size: 40, x: 'Group-11', y: 40, y0: 15, y1: 55 }
            //        ]
            //    },
            //    {
            //        key: 'Rejected',
            //        values: [
            //            { series: 0, size: 3, x: 'Group-1', y: 3, y0: 40, y1: 43 },
            //            { series: 0, size: 5, x: 'Group-2', y: 5, y0: 70, y1: 75 },
            //            { series: 0, size: 4, x: 'Group-3', y: 4, y0: 55, y1: 59 },
            //            { series: 0, size: 4, x: 'Group-4', y: 4, y0: 55, y1: 59 },
            //            { series: 0, size: 3, x: 'Group-5', y: 3, y0: 40, y1: 43 },
            //            { series: 0, size: 5, x: 'Group-6', y: 5, y0: 70, y1: 75 },
            //            { series: 0, size: 4, x: 'Group-7', y: 4, y0: 55, y1: 59 },
            //            { series: 0, size: 4, x: 'Group-8', y: 4, y0: 55, y1: 59 },
            //            { series: 0, size: 3, x: 'Group-9', y: 3, y0: 40, y1: 43 },
            //            { series: 0, size: 5, x: 'Group-10', y: 5, y0: 70, y1: 75 },
            //            { series: 0, size: 4, x: 'Group-11', y: 4, y0: 55, y1: 59 }
            //        ]
            //    }
            //];

            var inintData = [
                {
                    key: 'Initiated',
                    type: 'bar',
                    values: [
                        { x: 'Group-1', y: 10 },
                        { x: 'Group-2', y: 20 },
                        { x: 'Group-3', y: 15 },
                        { x: 'Group-4', y: 15 },
                        { x: 'Group-5', y: 10 },
                        { x: 'Group-6', y: 20 },
                        { x: 'Group-7', y: 15 },
                        { x: 'Group-8', y: 15 },
                        { x: 'Group-9', y: 10 },
                        { x: 'Group-10', y: 20 },
                        { x: 'Group-11', y: 15 }
                    ],
                    yAxis: 1
                },
                {
                    key: 'Completed',
                    type: 'bar',
                    values: [
                        { x: 'Group-1', y: 30 },
                        { x: 'Group-2', y: 50 },
                        { x: 'Group-3', y: 40 },
                        { x: 'Group-4', y: 40 },
                        { x: 'Group-5', y: 30 },
                        { x: 'Group-6', y: 50 },
                        { x: 'Group-7', y: 40 },
                        { x: 'Group-8', y: 40 },
                        { x: 'Group-9', y: 30 },
                        { x: 'Group-10', y: 50 },
                        { x: 'Group-11', y: 40 }
                    ],
                    yAxis: 1
                },
                {
                    key: 'Rejected',
                    type: 'bar',
                    values: [
                        { x: 'Group-1', y: 3 },
                        { x: 'Group-2', y: 5 },
                        { x: 'Group-3', y: 4 },
                        { x: 'Group-4', y: 4 },
                        { x: 'Group-5', y: 3 },
                        { x: 'Group-6', y: 5 },
                        { x: 'Group-7', y: 4 },
                        { x: 'Group-8', y: 4 },
                        { x: 'Group-9', y: 3 },
                        { x: 'Group-10', y: 5 },
                        { x: 'Group-11', y: 4 }
                    ],
                    yAxis: 1
                }
                //{
                //    key: 'Rate',
                //    type: 'line',
                //    values: [
                //        { x: 'Group-1', y: 1 },
                //        { x: 'Group-2', y: 2 },
                //        { x: 'Group-3', y: 3 },
                //        { x: 'Group-4', y: 4 },
                //        { x: 'Group-5', y: 5 },
                //        { x: 'Group-6', y: 4 },
                //        { x: 'Group-7', y: 3 },
                //        { x: 'Group-8', y: 2 },
                //        { x: 'Group-9', y: 5 },
                //        { x: 'Group-10', y: 6 },
                //        { x: 'Group-11', y: 7 }
                //    ],
                //    yAxis: 2
                //}
            ];

            var testdata = stream_layers(7, 10 + Math.random() * 100, .1).map(function (data, i) {
                return {
                    key: 'Stream' + i,
                    values: data.map(function (a) { a.y = a.y * (i <= 1 ? -1 : 1); return a })
                };
            });

            testdata[0].type = "area"
            testdata[0].yAxis = 1
            testdata[1].type = "area"
            testdata[1].yAxis = 1
            testdata[2].type = "line"
            testdata[2].yAxis = 1
            testdata[3].type = "line"
            testdata[3].yAxis = 2
            testdata[4].type = "bar"
            testdata[4].yAxis = 2
            testdata[5].type = "bar"
            testdata[5].yAxis = 2
            testdata[6].type = "bar"
            testdata[6].yAxis = 2;

            console.log(testdata);

            return inintData;
        }

        /* Inspired by Lee Byron's test data generator. */
        function stream_layers(n, m, o) {
            if (arguments.length < 3) o = 0;
            function bump(a) {
                var x = 1 / (.1 + Math.random()),
                    y = 2 * Math.random() - .5,
                    z = 10 / (.1 + Math.random());
                for (var i = 0; i < m; i++) {
                    var w = (i / m - y) * z;
                    a[i] += x * Math.exp(-w * w);
                }
            }
            return d3.range(n).map(function () {
                var a = [], i;
                for (i = 0; i < m; i++) a[i] = o + o * Math.random();
                for (i = 0; i < 5; i++) bump(a);
                return a.map(stream_index);
            });
        }

        function stream_index(d, i) {
            return { x: i, y: Math.max(0, d) };
        }
    })


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

