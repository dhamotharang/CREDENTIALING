CCMDashboard.controller("CCMDashboardController", ["$rootScope", "$scope", "toaster", "$timeout", "$filter", "CCMDashboardService", "CCMDashboardFactory", function ($rootScope, $scope, toaster, $timeout, $filter, CCMDashboardService, CCMDashboardFactory) {
    toaster.pop('Success', "Success", 'Welcome');

    //================================= Variable Declaration Satrt====================================================================================

    var $self = this;
    this.displayed = [];
    $scope.tableCaption = "Appointments";
    $scope.GridView = true;
    $scope.isPendingitemSelected = false;
    var AppointmentDate = new Date();
    //================================= Variable Declaration End====================================================================================




    //================================= Chart Container Starts  ===========================================================


    $scope.options = {
        chart: {
            type: 'multiBarChart',
            height: 450,
            margin: {
                top: 20,
                right: 20,
                bottom: 45,
                left: 45
            },
            clipEdge: true,
            duration: 500,
            stacked: true,
            xAxis: {
                axisLabel: 'Time (ms)',
                showMaxMin: false,
                tickFormat: function (d) {
                    return d3.format(',f')(d);
                }
            },
            yAxis: {
                axisLabel: 'Y Axis',
                axisLabelDistance: -20,
                tickFormat: function (d) {
                    return d3.format(',.1f')(d);
                }
            }
        }
    };

    $scope.data = generateData();

    /* Random Data Generator (took from nvd3.org) */
    function generateData() {
        return stream_layers(3, 50 + Math.random() * 50, .1).map(function (data, i) {
            return {
                key: 'Stream' + i,
                values: data
            };
        });
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

    /* Another layer generator using gamma distributions. */
    function stream_waves(n, m) {
        return d3.range(n).map(function (i) {
            return d3.range(m).map(function (j) {
                var x = 20 * j / m - i / 3;
                return 2 * x * Math.exp(-.5 * x);
            }).map(stream_index);
        });
    }

    function stream_index(d, i) {
        return { x: i, y: Math.max(0, d) };
    }


    $scope.options1 = {
        chart: {
            type: 'discreteBarChart',
            height: 450,
            margin: {
                top: 20,
                right: 20,
                bottom: 50,
                left: 55
            },
            x: function (d) { return d.label; },
            y: function (d) { return d.value + (1e-10); },
            showValues: true,
            valueFormat: function (d) {
                return d3.format(',.4f')(d);
            },
            duration: 500,
            xAxis: {
                axisLabel: 'X Axis'
            },
            yAxis: {
                axisLabel: 'Y Axis',
                axisLabelDistance: -10
            }
        }
    };

    $scope.data1 = [
        {
            key: "Cumulative Return",
            values: [
                {
                    "label": "A",
                    "value": -29.765957771107
                },
                {
                    "label": "B",
                    "value": 0
                },
                {
                    "label": "C",
                    "value": 32.807804682612
                },
                {
                    "label": "D",
                    "value": 196.45946739256
                },
                {
                    "label": "E",
                    "value": 0.19434030906893
                },
                {
                    "label": "F",
                    "value": -98.079782601442
                },
                {
                    "label": "G",
                    "value": -13.925743130903
                },
                {
                    "label": "H",
                    "value": -5.1387322875705
                }
            ]
        }
    ]


    //================================= Chart Container Ends  =============================================================




    //================================== Temporary function Declaration Start ===============================================================================


    $scope.MasterData = function () {
        CCMDashboardService.GetCCMAppointments().then(function (result) {
            $rootScope.CCMAppointments = result.data;
            $rootScope.TempCCMAppointments = result.data;
            $scope.tableStateValue = CCMDashboardFactory.resetTableState($scope.tableStateValue);
            $self.callServer($scope.tableStateValue);
            $scope.loadEvents();
            AppointmentDate = $filter('date')(AppointmentDate, "yyyy-MM-dd");
            $rootScope.filteredCCMAppointmentsByDate = $filter('CCMDashboardFilterByAppointmentDate')(AppointmentDate);
        }, function (error) {
            toaster.pop('error', "", 'Please try after sometime !!!');
        })
    }

    this.callServer = function callServer(tableState) {
        $self.isLoading = true;
        var pagination = tableState.pagination;
        var start = pagination.start || 0;
        var number = pagination.number || 5;
        $scope.tableStateValue = tableState;
        CCMDashboardFactory.getPage(start, number, tableState).then(function (result) {
            $self.displayed = result.data;
            tableState.pagination.numberOfPages = result.numberOfPages;
            $self.isLoading = false;
        });

    };

    //--Method to check the pending item is selected or not
    this.CheckPendingSelected = function isPendingSelected(records) {
       return records.some(function (record) {
            return record.SelectStatus == true;
        })
    }

    $scope.selectPlan = function (PlanName) {
        $rootScope.CCMAppointments = $filter('CCMDashboardFilterByPlan')(PlanName);
    }

    $scope.GridData = function (type) {
        switch (type) {
            case "Total":
                $rootScope.TempCCMAppointments = $rootScope.CCMAppointments;
                $scope.tableCaption = "Appointments";
                break;
            case "Approved":
                $rootScope.TempCCMAppointments = $filter('filter')($rootScope.CCMAppointments, { Status: "Approved" });
                $scope.tableCaption = "Approved";
                break;
            case "Rejected":
                $rootScope.TempCCMAppointments = $filter('filter')($rootScope.CCMAppointments, { Status: "Rejected" });
                $scope.tableCaption = "Rejected";
                break;
            case "Pending":
                $rootScope.TempCCMAppointments = $filter('filter')($rootScope.CCMAppointments, { Status: "Pending" });
                $scope.tableCaption = "Pending";
                break;
        }
        $scope.tableStateValue = CCMDashboardFactory.resetTableState($scope.tableStateValue);
        $self.callServer($scope.tableStateValue);
    }

    $scope.CheckStatus = function (appointment) {
        if (appointment.Status == 'Pending') {
            $rootScope.TempCCMAppointments[$rootScope.TempCCMAppointments.indexOf(appointment)].SelectStatus = !appointment.SelectStatus;
            $scope.tableStateValue = CCMDashboardFactory.resetTableState($scope.tableStateValue);
            $self.callServer($scope.tableStateValue);
        }
    }

    $scope.TableExport = function (type, tableID) {
        CCMDashboardFactory.exportToTable(type, tableID);
    }

    //================================== Temporary function Declaration End ===============================================================================
    //===================================CalendarPlugIn Script=============================================================================================
    'use strict';
    $scope.changeMode = function (mode) {
        $scope.mode = mode;
    };
    $scope.selectedDate = new Date();
    $scope.today = function () {
        $scope.currentDate = new Date();
    };

    $scope.isToday = function () {
        var today = new Date(),
            currentCalendarDate = new Date($scope.currentDate);

        today.setHours(0, 0, 0, 0);
        currentCalendarDate.setHours(0, 0, 0, 0);

        return today.getTime() === currentCalendarDate.getTime();
    };

    $scope.loadEvents = function () {
        $scope.eventSource = createRandomEvents();
        console.log($scope.eventSource);
    };

    $scope.onEventSelected = function (event) {
        $scope.event = event;
    };

    $scope.onTimeSelected = function (selectedTime) {
        $scope.selectedDate = selectedTime;
        AppointmentDate = $filter('date')($scope.selectedDate, "yyyy-MM-dd");
        $rootScope.filteredCCMAppointmentsByDate = $filter('CCMDashboardFilterByAppointmentDate')(AppointmentDate);
    };

    function createRandomEvents() {
        var events = [];
        var startDay = Math.floor(Math.random() * 90) - 45;
        for (var i = 0; i < $rootScope.CCMAppointments.length; i++) {
            var Sdate = new Date($rootScope.CCMAppointments[i].AppointmentDate);
            var Edate = new Date($rootScope.CCMAppointments[i].AppointmentDate);
            startTime = new Date(Date.UTC(Sdate.getUTCFullYear(), Sdate.getUTCMonth(), Sdate.getUTCDate()));
            endTime = startTime+1;
            events.push({
                title: "Provider Credentialing Examination",
                startTime: startTime,
                endTime: endTime,
                allDay: false
            });
        }
        return events;
    }
    //==================================================Script END=========================================================================================
}]);