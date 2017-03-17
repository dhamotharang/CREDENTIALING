/* 
    Management Dashboard Angular Module and Controller
    Author : Kalaram Verma
*/

(function () {
    "use strict";

    $(function () {

        $('.biscuit').on('click', function () {
            $('.biscuit').each(function () {
                $(this).removeClass('active');
            });
            $(this).addClass('active');
        })
    });

    // Management Dashboard Module
    var MDApp = angular.module("MDApp", ["ngTable"]);

    // Management Dashboard Controller
    MDApp.controller("MDController", MDController);

    // Management Dashboard Controller Injection
    MDController.$inject = ["$scope", "$http", "$filter", "NgTableParams", "$window"];

    function MDController($scope, $http, $filter, NgTableParams, $window) {

        $scope.GetLicensesData = function (count) {
            $http.get(rootDir + '/Prototypes/GetLicenseData').success(function (data) {
                $scope.init_LicenseBarChart(data);
            }).error(function () {
                console.log('method not available.......');
            });
        };

        //Created function to be called when data loaded dynamically
        $scope.init_LicenseBarChart = function (data) {

            var LabelsX = ['x'], ValidLicense = ['> 90 Days'],
                ExpiredLicense = ['Expired License'],
                PendingDaylicenseLessThan30 = ['< 30 Days'],
                PendingDaylicenseLessThan60 = ['30 - 60 Days'],
                PendingDaylicenseLessThan90 = ['60 - 90 Days'];
            for (var i in data) {
                LabelsX.push(data[i].LicenseType);
                ValidLicense.push(data[i].LicenseStatus.ValidLicense);
                ExpiredLicense.push(data[i].LicenseStatus.ExpiredLicense);
                PendingDaylicenseLessThan30.push(data[i].LicenseStatus.PendingDaylicenseLessThan30);
                PendingDaylicenseLessThan60.push(data[i].LicenseStatus.PendingDaylicenseLessThan60);
                PendingDaylicenseLessThan90.push(data[i].LicenseStatus.PendingDaylicenseLessThan90);
            }

            var chart = c3.generate({
                bindto: '#licenseStatusChart',
                size: {
                    height: 560,
                    //width: 480
                },
                data: {
                    x: 'x',
                    columns: [
                        LabelsX,
                        ValidLicense,
                        PendingDaylicenseLessThan30,
                        PendingDaylicenseLessThan60,
                        PendingDaylicenseLessThan90,
                        //ExpiredLicense,
                    ],
                    groups: [
                        [
                            ValidLicense[0],
                            PendingDaylicenseLessThan30[0],
                            PendingDaylicenseLessThan60[0],
                            PendingDaylicenseLessThan90[0],
                            //ExpiredLicense[0]
                        ]
                    ],
                    type: 'bar',
                    bar: {
                        width: {
                            ratio: 0.5 // this makes bar width 50% of length between ticks
                        }
                        // or
                        //width: 100 // this makes bar width 100px
                    },
                    //labels: true,
                    colors: {
                        '> 90 Days': '#398439',
                        '< 30 Days': '#DC143C',
                        '30 - 60 Days': '#D2691E',
                        '60 - 90 Days': '#FFD700',
                        //'Expired License': '#DC143C',
                    },
                    //color: function (color, d) {
                    //    // d will be 'id' when called for legends
                    //    return d.id && d.id === 'data3' ? d3.rgb(color).darker(d.value / 150) : color;
                    //}
                },
                axis: {
                    x: {
                        type: 'category', // this needed to load string x value
                        tick: {
                            rotate: 75,
                            multiline: false
                        },
                        height: 130
                    }
                }
            });
        };

        $scope.ToggleTableChart = function (con) {
            if (con == 2) {

            } else {

            }
        };

        $scope.GetMidLevels = function () {
            //$scope.GetAll();
        };

        $scope.GetPCPs = function () {
            //$scope.GetAll();
        };

        $scope.GetAll = function () {
            $window.location.href = $window.location.origin + rootDir + '/Prototypes/ProviderDirectory';
        };
        
        $scope.CCORankColumns = [
            {
                field: "CCOID",
                title: "S. No.",
                show: true
            },
            {
                field: "Name",
                title: "Name",
                show: true
            },
            {
                field: "AvgTime",
                title: "Avg. Time",
                show: true
            },
            {
                field: "TaskCompleted",
                title: "Task Completed",
                show: true
            },
        ];

        $scope.GetCCORanksData = function (count) {
            $http.get(rootDir + '/Prototypes/GetCCORankData').success(function (data) {
                $scope.CCORankTableParams = new NgTableParams({}, {
                    dataset: data, counts: [],
                });
            }).error(function () {
                console.log('method not available.......');
            });
        };

        $scope.GetProfileCompletionData = function (count) {
            $http.get(rootDir + '/Prototypes/GetProfileCompletionData').success(function (data) {

                var pichartColumns = [];
                for (var i = 0; i < data.length; i++) {
                    pichartColumns.push([data[i].Label, data[i].ProviderCounts]);
                }
                var chart = c3.generate({
                    bindto: '#ProfileReportChart',
                    data: {
                        columns: pichartColumns,
                        type: 'donut',
                    },
                    donut: {
                        title: "Provider Profile Completion"
                    },
                    tooltip: {
                        format: {
                            value: function (value, ratio, id) {
                                return value + ", " + (ratio * 100).toFixed(1) + "%";
                            }
                        }
                    }
                });
            }).error(function () {
                console.log('method not available.......');
            });
        };

        $scope.GetCCOReportsData = function (count) {
            $http.get(rootDir + '/Prototypes/GetCCOReportsData').success(function (data) {

                var LabelsX = ['x'], count = ['No. of Provider'];
                for (var i in data) {
                    LabelsX.push(data[i].Name);
                    count.push(data[i].ProvidersCount);
                }

                var chart = c3.generate({
                    bindto: '#CCOReportChart',
                    size: {
                        //height: 250,
                        //width: 480
                    },
                    data: {
                        x: 'x',
                        columns: [
                            LabelsX,
                            count,
                        ],
                        type: 'bar',
                        labels: true,
                        bar: {
                            width: {
                                ratio: 0.5 // this makes bar width 50% of length between ticks
                            }
                            // or
                            //width: 100 // this makes bar width 100px
                        },
                    },
                    legend: {
                        show: false
                    },
                    axis: {
                        x: {
                            type: 'category', // this needed to load string x value
                            tick: {
                                rotate: 75,
                                multiline: false
                            },
                            height: 130
                        },
                        y: {
                            label: 'Providers',
                        }
                    }
                });
            }).error(function () {
                console.log('method not available.......');
            });
        };

        $scope.NAPColumns = [
            {
                field: "Name",
                title: "Provider Name",
                show: true
            },
            {
                field: "AddedDays",
                title: "Days Added Before",
                show: true
            },
            {
                field: "Status",
                title: "Status",
                show: true
            },
            {
                field: "AssignedCCO",
                title: "CCO",
                show: true
            },
        ];

        $scope.GetProviderData = function (count) {
            $http.get(rootDir + '/Prototypes/GetProviders?count=' + count).success(function (data) {
                $scope.NewAddedProviderTableParams = new NgTableParams({}, {
                    dataset: data, counts: []
                });
            }).error(function () {
                console.log('method not available.......');
            });
        };

        $scope.GetLicensesData();
        $scope.GetCCORanksData();
        $scope.GetProfileCompletionData();
        $scope.GetCCOReportsData();
        $scope.GetProviderData(4);
    }
})();