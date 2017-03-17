/* 
    Provider Directory Angular Module and Controller
    Author : Kalaram Verma
*/


(function () {
    "use strict";

    $(function () {

        $('.single_dual').on('click', function () {
            $('.single_dual').each(function () {
                $(this).removeClass('active').addClass('notActive');
            });
            $(this).removeClass('notActive').addClass('active');
        })

        $('.biscuit').on('click', function () {
            $('.biscuit').each(function () {
                $(this).removeClass('active');
            });
            $(this).addClass('active');
        })

        $('.provider_active').on('click', function () {
            $('.provider_active').each(function () {
                $(this).removeClass('active').addClass('notActive');
            });
            $(this).removeClass('notActive').addClass('active');
        })
    });



    // Provider Directory Module
    var PDApp = angular.module("PDApp", ["ngTable"]);
    
    // Provider Directory Controller
    PDApp.controller("PDController", PDController);

    // Provider Directory Controller Injection
    PDController.$inject = ["$scope", "$http", "$filter", "NgTableParams"];

    function PDController($scope, $http, $filter, NgTableParams) {

        $scope.GetProviderData = function (count) {
            $http.get(rootDir + '/Prototypes/GetProviders?count=' + count).success(function (data) {
                console.log(data);
                $scope.init_table(data);
            }).error(function () {
                console.log('method not available.......');
            });
        };

        $scope.ProviderTitle = "Providers";

        //Created function to be called when data loaded dynamically
        $scope.init_table = function (data) {
            $scope.tableParams = new NgTableParams({}, {
                dataset: data
            });
        };

        $scope.GetSpecialities = function (con) {
            if (con == 2) {
                $scope.GetProviderData(35);
                $scope.ProviderTitle = "Dual Specialities";
            } else {
                $scope.GetProviderData(20);
                $scope.ProviderTitle = "Single Specialities";
            }
        };

        $scope.GetMidLevels = function () {
            $scope.GetProviderData(70);
            $scope.ProviderTitle = "Mid Levels";
        };

        $scope.GetPCPs = function () {
            $scope.GetProviderData(60);
            $scope.ProviderTitle = "PCPs";
        };

        $scope.GetAll = function () {
            $scope.GetProviderData(212);
            $scope.ProviderTitle = "Providers";
        };

        $scope.GetActiveInActive = function (con) {
            if (con == 2) {
                $scope.GetProviderData(36);
                $scope.ProviderTitle = "Inactive Providers";

            } else {
                $scope.GetProviderData(212);
                $scope.ProviderTitle = "Active Providers";
            }
        };

        //$scope.GetSpecialtiesData = function (count) {
        //    $http.get(rootDir + '/Prototypes/GetSpecialties').success(function (data) {
        //        console.log(data);
        //        $scope.Specialties = data;
        //        $scope.GetAll();
        //    }).error(function () {
        //        console.log('method not available.......');
        //    });
        //};

        $scope.GetAll();
        //$scope.GetSpecialtiesData();

        // current not in use, pending to apply
        $scope.SpecificFilter = [
            {
                field: "HospitalList",
                title: "Hospital List",
            },
            {
                field: "HospitalList",
                title: "Hospital List",
            },
            {
                field: "HospitalList",
                title: "Hospital List",
            },
            {
                field: "HospitalList",
                title: "Hospital List",
            },
            {
                field: "HospitalList",
                title: "Hospital List",
            },
            {
                field: "HospitalList",
                title: "Hospital List",
            },
            ];

        $scope.Columns = [
            {
                field: "ProviderID",
                title: "S. No.",
                //filter: {
                //    name: "text" // template alias name
                //},
                show: true
            },
            {
                field: "Salutation",
                title: "Salutation",
                filter: {
                    //Salutation: "salutation" // template alias name
                    name: "text"
                },
                sortable: "Salutation",
                show: true
            },
            {
                field: "Name",
                title: "Provider Name",
                filter: {
                    name: "text" // template alias name
                },
                sortable: "Name",
                show: true
            },
            {
                field: "NPI",
                title: "NPI Number",
                filter: {
                    name: "text" // template alias name
                },
                sortable: "NPI",
                show: true
            },
            {
                field: "Specialty",
                title: "Specialty",
                filter: {
                    name: "text" // template alias name
                },
                sortable: "Specialty",
                show: true
            },
            {
                field: "PrimaryPracticeLocation",
                title: "Primary Practice Location",
                filter: {
                    name: "text" // template alias name
                },
                sortable: "PrimaryPracticeLocation",
                show: true
            },
            {
                field: "Groups",
                title: "IPA/Group",
                filter: {
                    name: "text" // template alias name
                },
                sortable: "Groups",
                show: true
            },
            {
                field: "Plan",
                title: "Plan",
                filter: {
                    name: "text" // template alias name
                },
                sortable: "Plan",
                show: true
            },
            {
                field: "LOB",
                title: "LOB",
                filter: {
                    name: "text" // template alias name
                },
                sortable: "LOB",
                show: true
            }
        ];
    }
})();