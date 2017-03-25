CCMDashboard.controller("AppointmentGridController", ["$rootScope", "$scope", "toaster", "$timeout", "$filter", "CCMDashboardService", "CCMDashboardFactory", function ($rootScope, $scope, toaster, $timeout, $filter, CCMDashboardService, CCMDashboardFactory) {
    //================================= Variable Declaration Satrt====================================================================================
    var $self = this;
    this.displayed = [];

    //================================= Variable Declaration End====================================================================================
    //================================== Temporary function Declaration Start ===============================================================================
    this.callServer = function callServer(tableState) {
        if (tableState===undefined) {
            return;
        }
        $self.isLoading = true;
        var pagination = tableState.pagination;
        var start = pagination.start || 0;
        var number = pagination.number || 5;
        $scope.tableStateValue = tableState;
        CCMDashboardFactory.getPage(start, number, tableState).then(function (result) {
            $self.displayed = result.data;
            tableState.pagination.numberOfPages = result.numberOfPages;
            $self.isLoading = false;
            $rootScope.ToHighLightRowObject = "";
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


    $rootScope.$on('AppointmentsGrid', function (event, args) {
        $rootScope.TempObjectForStatus.CredentailingApprovalRequest = true;
        $rootScope.TempObjectForStatus.QuickApprovalAction = false;
        $rootScope.TempObjectForStatus.AppointmentDashboard = false;
        $rootScope.VisibilityControl = '';
        CCMDashboardFactory.ClearSelectRowStatus();
        if (args.RowObject !== null && args.RowObject !== undefined) {
            $rootScope.ToHighLightRowObject = args.RowObject;
            $scope.TempToHighLightRowObject = args.RowObject;
        }
        switch (args.type) {
            case "New":
                $rootScope.TempCCMAppointments = $rootScope.CCMAppointments;
                $rootScope.tableCaption = "Appointments";
                break;
            case "Approved":
                $rootScope.TempCCMAppointments = $filter('filter')($rootScope.CCMAppointments, { Status: "Approved" });
                $rootScope.tableCaption = "Approved";
                break;
            case "Rejected":
                $rootScope.TempCCMAppointments = $filter('filter')($rootScope.CCMAppointments, { Status: "Rejected" });
                $rootScope.tableCaption = "Rejected";
                break;
            case "Pending":
                $rootScope.TempCCMAppointments = $filter('filter')($rootScope.CCMAppointments, { Status: "Pending" });
                $rootScope.tableCaption = "Pending";
                break;
        }
        $scope.tableStateValue = CCMDashboardFactory.resetTableState($scope.tableStateValue);
        $self.callServer($scope.tableStateValue, args.RowObject);
        $timeout(countUp, 8000);
    });

    var countUp = function () {
        $scope.TempToHighLightRowObject = "";
    }
    $scope.OpenAppointmentDetailsControl = function (sectionValue) {
        $rootScope.VisibilityControl = sectionValue;
        $rootScope.TempObjectForStatus.SingleDetailedApprovalAction = true;
    }
    $scope.CloseAppointmentDetailsControl = function (sectionValue) {
        $rootScope.VisibilityControl = '';
        $rootScope.TempObjectForStatus.SingleDetailedApprovalAction = false;
    }
    //==================================================Report Approval Form Submission Script Start ======================================================
    $scope.cleardigitalsignature = function () {
        context.clearRect(0, 0, canvas.width, canvas.height);
    }
    //==================================================Report Approval Form Submission Script End   ======================================================
    //$scope.CheckStatus = function (appointment,event) {
    //    if ((appointment.Status == 'Pending' || appointment.Status == 'New') && !$rootScope.TempObjectForStatus.SingleDetailedApprovalAction) {
    //        $rootScope.TempCCMAppointments[$rootScope.TempCCMAppointments.indexOf(appointment)].SelectStatus = !appointment.SelectStatus;
    //        //$scope.tableStateValue = CCMDashboardFactory.resetTableState($scope.tableStateValue);
    //        //$self.callServer($scope.tableStateValue);
    //        if ($filter('filter')($rootScope.TempCCMAppointments, { SelectStatus: true }).length > 0) {
    //            $rootScope.TempObjectForStatus.QuickApprovalAction = true;
    //        } else {
    //            $rootScope.TempObjectForStatus.QuickApprovalAction = false;
    //        }
    //    }
    //}
    $scope.SwitchViews = function () {
        $rootScope.TempObjectForStatus.CredentailingApprovalRequest = false;
        $rootScope.TempObjectForStatus.AppointmentDashboard = true
        $scope.$parent.GridType = "";
    }
    $scope.TableExport = function (type, tableID) {
        CCMDashboardFactory.exportToTable(type, tableID);
    }

    //================================== Temporary function Declaration End ===============================================================================








































































    //================================= Chart Container Starts  ===========================================================================================
    //$scope.options = {
    //    chart: {
    //        type: 'multiBarChart',
    //        height: 450,
    //        margin: {
    //            top: 20,
    //            right: 20,
    //            bottom: 45,
    //            left: 45
    //        },
    //        clipEdge: true,
    //        duration: 500,
    //        stacked: true,
    //        xAxis: {
    //            axisLabel: 'Time (ms)',
    //            showMaxMin: false,
    //            tickFormat: function (d) {
    //                return d3.format(',f')(d);
    //            }
    //        },
    //        yAxis: {
    //            axisLabel: 'Y Axis',
    //            axisLabelDistance: -20,
    //            tickFormat: function (d) {
    //                return d3.format(',.1f')(d);
    //            }
    //        }
    //    }
    //};

    //$scope.data = generateData();

    ///* Random Data Generator (took from nvd3.org) */
    //function generateData() {
    //    return stream_layers(3, 50 + Math.random() * 50, .1).map(function (data, i) {
    //        return {
    //            key: 'Stream' + i,
    //            values: data
    //        };
    //    });
    //}

    ///* Inspired by Lee Byron's test data generator. */
    //function stream_layers(n, m, o) {
    //    if (arguments.length < 3) o = 0;
    //    function bump(a) {
    //        var x = 1 / (.1 + Math.random()),
    //            y = 2 * Math.random() - .5,
    //            z = 10 / (.1 + Math.random());
    //        for (var i = 0; i < m; i++) {
    //            var w = (i / m - y) * z;
    //            a[i] += x * Math.exp(-w * w);
    //        }
    //    }
    //    return d3.range(n).map(function () {
    //        var a = [], i;
    //        for (i = 0; i < m; i++) a[i] = o + o * Math.random();
    //        for (i = 0; i < 5; i++) bump(a);
    //        return a.map(stream_index);
    //    });
    //}

    ///* Another layer generator using gamma distributions. */
    //function stream_waves(n, m) {
    //    return d3.range(n).map(function (i) {
    //        return d3.range(m).map(function (j) {
    //            var x = 20 * j / m - i / 3;
    //            return 2 * x * Math.exp(-.5 * x);
    //        }).map(stream_index);
    //    });
    //}

    //function stream_index(d, i) {
    //    return { x: i, y: Math.max(0, d) };
    //}

    //$scope.options1 = {
    //    chart: {
    //        type: 'discreteBarChart',
    //        height: 450,
    //        margin: {
    //            top: 20,
    //            right: 20,
    //            bottom: 50,
    //            left: 55
    //        },
    //        x: function (d) { return d.label; },
    //        y: function (d) { return d.value + (1e-10); },
    //        showValues: true,
    //        valueFormat: function (d) {
    //            return d3.format(',.4f')(d);
    //        },
    //        duration: 500,
    //        xAxis: {
    //            axisLabel: 'X Axis'
    //        },
    //        yAxis: {
    //            axisLabel: 'Y Axis',
    //            axisLabelDistance: -10
    //        }
    //    }
    //};

    //$scope.data1 = [
    //    {
    //        key: "Cumulative Return",
    //        values: [
    //            {
    //                "label": "A",
    //                "value": -29.765957771107
    //            },
    //            {
    //                "label": "B",
    //                "value": 0
    //            },
    //            {
    //                "label": "C",
    //                "value": 32.807804682612
    //            },
    //            {
    //                "label": "D",
    //                "value": 196.45946739256
    //            },
    //            {
    //                "label": "E",
    //                "value": 0.19434030906893
    //            },
    //            {
    //                "label": "F",
    //                "value": -98.079782601442
    //            },
    //            {
    //                "label": "G",
    //                "value": -13.925743130903
    //            },
    //            {
    //                "label": "H",
    //                "value": -5.1387322875705
    //            }
    //        ]
    //    }
    //]
    //================================= Chart Container Ends  ===============================================================================================


}]);
