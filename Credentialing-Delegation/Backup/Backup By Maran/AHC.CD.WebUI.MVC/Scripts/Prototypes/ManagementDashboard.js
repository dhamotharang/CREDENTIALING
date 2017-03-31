/* 
    Management Dashboard Angular Module and Controller
    Author : Kalaram Verma
*/

// Start - Management Dashboard Module
(function () {
    'use strict';

    angular.module("MDApp", ["ngTable"]);

})();
// END - Management Dashboard Module

// Start - Management Dashboard Factory
(function () {
    'use strict';

    angular.module("MDApp").factory('Manager', Manager);

    Manager.$inject = ['$http', '$q', 'NgTableParams', '$window'];

    function Manager($http, $q, NgTableParams, $window) {
        var svc = {};

        // Redirect provider directory
        svc.RedirectProviderDirectory = function () {
            $window.location.href = $window.location.origin + rootDir + '/Prototypes/ProviderDirectory';
        };

        // Expiry License Bar Chart View
        svc.LicenseBarChart = function (data) {
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

        // Expiry License Data
        svc.GetLicensesData = function () {
            var def = $q.defer();
            return $http.get(rootDir + '/Prototypes/GetLicenseData').success(function (data) {
                def.resolve(data);
            })
                .error(function () {
                    def.reject("method not available.......");
                });
        };

        // Expiry License Data with respect to license type
        svc.GetLicenseTypeData = function (license, con) {
            var def = $q.defer(), minDaysLeft, maxDaysLeft, count;

            //if (con) {
            //    con = con.slice(1, con.length);
            //}
            if (con == "<0") { minDaysLeft = -80; maxDaysLeft = 0; count = license.LicenseStatus.ExpiredLicense; }
            else if (con == "<30") { minDaysLeft = 0; maxDaysLeft = 30; count = license.LicenseStatus.PendingDaylicenseLessThan30; }
            else if (con == "<60") { minDaysLeft = 30; maxDaysLeft = 60; count = license.LicenseStatus.PendingDaylicenseLessThan60; }
            else if (con == "<90") { minDaysLeft = 60; maxDaysLeft = 90; count = license.LicenseStatus.PendingDaylicenseLessThan90; }
            else if (con == ">90") { minDaysLeft = 90; maxDaysLeft = 200; count = license.LicenseStatus.ValidLicense; }
                // Need To Update for Get All Data for License..............
            else { minDaysLeft = -80; maxDaysLeft = 200; count = license.TotalLicenses; }
            return $http.get(rootDir + '/Prototypes/GetProvidersLicenseInformation?count='
                + count + "&minDaysLeft=" + minDaysLeft + "&maxDaysLeft=" + maxDaysLeft + "&licenseTypeCode=" + license.LicenseTypeCode).success(function (data) {
                    def.resolve(data);
                })
                .error(function () {
                    def.reject("method not available.......");
                });
        };

        // Get Top 5 CCO Ranks
        svc.GetCCORanksData = function (count) {
            var def = $q.defer();
            return $http.get(rootDir + '/Prototypes/GetCCORankData').success(function (data) {
                def.resolve(data);
            })
                .error(function () {
                    def.reject("method not available.......");
                });
        };

        // Get Providers Data
        svc.GetProviderData = function (count) {
            var def = $q.defer();
            return $http.get(rootDir + '/Prototypes/GetProviders?count=' + count).success(function (data) {
                def.resolve(data);
            })
                .error(function () {
                    def.reject("method not available.......");
                });
        };

        // Get Profile Completion Report Pie Chart
        svc.GetProfileCompletionDataForPieChart = function (count) {
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

        // Get CCO Report Bar Chart
        svc.GetCCOReportsDataForBarChart = function () {
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

        // Get CCO Ranks Required Dynamic Columns
        svc.CCORankColumns = function () {
            return [
                    { field: "CCOID", title: "S. No.", show: true },
                    { field: "Name", title: "Name" },
                    { field: "AvgTime", title: "Avg. Time" },
                    { field: "TaskCompleted", title: "Task Completed" },
            ];
        };

        // Get New Added Provider Required Dynamic Columns
        svc.NAPColumns = function () {
            return [
                    { field: "Name", title: "Provider Name" },
                    { field: "AddedDays", title: "Days Added Before" },
                    { field: "Status", title: "Status" },
                    { field: "AssignedCCO", title: "CCO" },
            ];
        };

        // Get State License Required Dynamic Columns
        svc.StateLicenseColumns = function () {
            return [
                    { title: "NPI", field: "NPI", sortable: "NPI" },
                    { title: "Provider Relationship", field: "Relationship", sortable: "Relationship" },
                    { title: "Name", field: "Name", sortable: "Name" },
                    { title: "License Number", field: "LicenseNumber", sortable: "LicenseNumber" },
                    { title: "State", field: "IssueState", sortable: "IssueState" },
                    { title: "Status", field: "Status", sortable: "Status" },
                    { title: "Expiry Date", field: "ExpiryDate", sortable: "ExpiryDate" },
                    { title: "Days Left", field: "DaysLeft", sortable: "DaysLeft" },
            ];
        };

        // Get Federal DEA Required Dynamic Columns
        svc.FDEAColumns = function () {
            return [
                    { title: "NPI", field: "NPI", sortable: "NPI" },
                    { title: "Provider Relationship", field: "Relationship", sortable: "Relationship" },
                    { title: "Name", field: "Name", sortable: "Name" },
                    { title: "DEA Number", field: "LicenseNumber", sortable: "LicenseNumber" },
                    { title: "State", field: "IssueState", sortable: "IssueState" },
                    { title: "Expiry Date", field: "ExpiryDate", sortable: "ExpiryDate" },
                    { title: "Days Left", field: "DaysLeft", sortable: "DaysLeft" },
            ];
        };

        // Get CDS Required Dynamic Columns
        svc.CDSColumns = function () {
            return [
                    { title: "NPI", field: "NPI", sortable: "NPI" },
                    { title: "Provider Relationship", field: "Relationship", sortable: "Relationship" },
                    { title: "Name", field: "Name", sortable: "Name" },
                    { title: "CDS Number", field: "LicenseNumber", sortable: "LicenseNumber" },
                    { title: "State", field: "IssueState", sortable: "IssueState" },
                    { title: "Expiry Date", field: "ExpiryDate", sortable: "ExpiryDate" },
                    { title: "Days Left", field: "DaysLeft", sortable: "DaysLeft" },
            ];
        };

        // Get Speciality Board Required Dynamic Columns
        svc.SpecialityBoardColumns = function () {
            return [
                    { title: "NPI", field: "NPI", sortable: "NPI" },
                    { title: "Provider Relationship", field: "Relationship", sortable: "Relationship" },
                    { title: "Name", field: "Name", sortable: "Name" },
                    { title: "Specialty Board Name", field: "SpecialtyBoardName", sortable: "SpecialtyBoardName" },
                    { title: "Expiry Date", field: "ExpiryDate", sortable: "ExpiryDate" },
                    { title: "Days Left", field: "DaysLeft", sortable: "DaysLeft" },
            ];
        };

        // Get Hospital Previliges Required Dynamic Columns
        svc.HospitalPreviligesColumns = function () {
            return [
                    { title: "NPI", field: "NPI", sortable: "NPI" },
                    { title: "Provider Relationship", field: "Relationship", sortable: "Relationship" },
                    { title: "Name", field: "Name", sortable: "Name" },
                    { title: "Hospital Name", field: "HospitalName", sortable: "HospitalName" },
                    { title: "Expiry Date", field: "ExpiryDate", sortable: "ExpiryDate" },
                    { title: "Days Left", field: "DaysLeft", sortable: "DaysLeft" },
            ];
        };

        // Get Professional Liability Required Dynamic Columns
        svc.ProfessionalLiabilityColumns = function () {
            return [
                    { title: "NPI", field: "NPI", sortable: "NPI" },
                    { title: "Provider Relationship", field: "Relationship", sortable: "Relationship" },
                    { title: "Name", field: "Name", sortable: "Name" },
                    { title: "Policy Number", field: "LicenseNumber", sortable: "LicenseNumber" },
                    { title: "Insurance Carrier", field: "InsuranceCarrier", sortable: "InsuranceCarrier" },
                    { title: "Expiry Date", field: "ExpiryDate", sortable: "ExpiryDate" },
                    { title: "Days Left", field: "DaysLeft", sortable: "DaysLeft" },
            ];
        };

        // Get Workers Compensation Required Dynamic Columns
        svc.WorkersCompensationColumns = function () {
            return [
                    { title: "NPI", field: "NPI", sortable: "NPI" },
                    { title: "Provider Relationship", field: "Relationship", sortable: "Relationship" },
                    { title: "Name", field: "Name", sortable: "Name" },
                    { title: "Workers Compensation Number", field: "LicenseNumber", sortable: "LicenseNumber" },
                    { title: "Expiry Date", field: "ExpiryDate", sortable: "ExpiryDate" },
                    { title: "Days Left", field: "DaysLeft", sortable: "DaysLeft" },
            ];
        };

        // Get Medicare Information Required Dynamic Columns
        svc.MedicareInformationColumns = function () {
            return [
                    { title: "NPI", field: "NPI", sortable: "NPI" },
                    { title: "Provider Relationship", field: "Relationship", sortable: "Relationship" },
                    { title: "Name", field: "Name", sortable: "Name" },
                    { title: "Medicare Licence Number", field: "LicenseNumber", sortable: "LicenseNumber" },
                    { title: "Expiry Date", field: "ExpiryDate", sortable: "ExpiryDate" },
                    { title: "Days Left", field: "DaysLeft", sortable: "DaysLeft" },
            ];
        };

        // Get Medicaid Information Required Dynamic Columns
        svc.MedicaidInformationColumns = function () {
            return [
                    { title: "NPI", field: "NPI", sortable: "NPI" },
                    { title: "Provider Relationship", field: "Relationship", sortable: "Relationship" },
                    { title: "Name", field: "Name", sortable: "Name" },
                    { title: "Medicaid License Number", field: "LicenseNumber", sortable: "LicenseNumber" },
                    { title: "Expiry Date", field: "ExpiryDate", sortable: "ExpiryDate" },
                    { title: "Days Left", field: "DaysLeft", sortable: "DaysLeft" },
            ];
        };

        // Get CAQH Required Dynamic Columns
        svc.CAQHColumns = function () {
            return [
                    { title: "NPI", field: "NPI", sortable: "NPI" },
                    { title: "Provider Relationship", field: "Relationship", sortable: "Relationship" },
                    { title: "Name", field: "Name", sortable: "Name" },
                    { title: "CAQH Number", field: "LicenseNumber", sortable: "LicenseNumber" },
                    { title: "NextAttestation Date", field: "NextAttestationDate", sortable: "NextAttestationDate" },
                    { title: "Days Left", field: "DaysLeft", sortable: "DaysLeft" },
            ];
        };

        // Get Upcoming Recredential Required Dynamic Columns
        svc.UpcomingRecredentialColumns = function () {
            return [
                    { title: "NPI", field: "NPI", sortable: "NPI" },
                    { title: "Name", field: "Name", sortable: "Name" },
                    { title: "Plan", field: "Plan", sortable: "Plan" },
                    { title: "LOB", field: "LOB", sortable: "LOB" },
                    { title: "Participating Status", field: "Status", sortable: "Status" },
                    { title: "GroupID", field: "GroupID", sortable: "GroupID" },
                    { title: "ProviderID", field: "ProviderID", sortable: "ProviderID" },
                    { title: "Effective Date", field: "ExpiryDate", sortable: "ExpiryDate" },
                    { title: "Recredentialing Date", field: "NextAttestationDate", sortable: "NextAttestationDate" },
                    { title: "Days Left", field: "DaysLeft", sortable: "DaysLeft" },
            ];
        };

        // Get License Columns Based on License Types
        svc.GetLicenseColumns = function (licenseType) {
            if (licenseType == "StateLicense") { return svc.StateLicenseColumns(); }
            if (licenseType == "FederalDEA") { return svc.FDEAColumns(); }
            if (licenseType == "CDSInformation") { return svc.CDSColumns(); }
            if (licenseType == "SpecialityBoard") { return svc.SpecialityBoardColumns(); }
            if (licenseType == "HospitalPrivilages") { return svc.HospitalPreviligesColumns(); }
            if (licenseType == "ProfessionalLiability") { return svc.ProfessionalLiabilityColumns(); }
            if (licenseType == "WorkerCompensation") { return svc.WorkersCompensationColumns(); }
            if (licenseType == "MedicareInformation") { return svc.MedicareInformationColumns(); }
            if (licenseType == "MedicaidInformation") { return svc.MedicaidInformationColumns(); }
            if (licenseType == "CAQHExpiries") { return svc.CAQHColumns(); }
            if (licenseType == "UpComingRecredentials") { return svc.UpcomingRecredentialColumns(); }
        };

        return svc;
    }
})();
// END - Management Dashboard Factory

// Start - Management Dashboard Controller
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

    // Management Dashboard Controller
    angular.module("MDApp").controller("MDController", MDController);

    // Management Dashboard Controller Injection
    MDController.$inject = ["$scope", "$http", "$filter", "NgTableParams", "Manager"];

    function MDController($scope, $http, $filter, NgTableParams, Manager) {

        $scope.GetMidLevels = function () {
            //Manager.RedirectProviderDirectory();
        };

        $scope.GetPCPs = function () {
            //Manager.RedirectProviderDirectory();
        };

        $scope.GetAll = function () {
            Manager.RedirectProviderDirectory();
        };

        /* Start - Columns for Dynamic Table Columns */
        $scope.CCORankColumns = Manager.CCORankColumns();
        $scope.NAPColumns = Manager.NAPColumns();
        /* END - Columns for Dynamic Table Columns */

        // Expired License Information Bar Chart
        Manager.GetLicensesData().then(function (response) {
            Manager.LicenseBarChart(response.data);
            $scope.LicenseData = response.data;
        });

        // New Added Provider Data
        Manager.GetProviderData(4).then(function (response) {
            $scope.NewAddedProviderTableParams = new NgTableParams({}, {
                dataset: response.data, counts: [],
            });
        });

        // CCO Rank Data - top 5
        Manager.GetCCORanksData().then(function (response) {
            $scope.CCORankTableParams = new NgTableParams({}, {
                dataset: response.data, counts: [],
            });
        });

        // Profile Completion Report - Pie Chart
        Manager.GetProfileCompletionDataForPieChart();

        // CCO Report Data - Bar Chart
        Manager.GetCCOReportsDataForBarChart();

        // License Type Data by license type parameters
        $scope.GetLicenseTypeData = function (license, con) {
            Manager.GetLicenseTypeData(license, con).then(function (response) {
                $scope.LicenseType = license.LicenseType;
                $scope.LicenseTypeCode = license.LicenseTypeCode;
                $scope.LicenseColumns = Manager.GetLicenseColumns(license.LicenseTypeCode);
                $scope.LicenseTableParams = new NgTableParams({ sorting: { DaysLeft: "asc" } }, {
                    dataset: response.data
                });
            });
        };

    }
})();
// END - Management Dashboard Controller