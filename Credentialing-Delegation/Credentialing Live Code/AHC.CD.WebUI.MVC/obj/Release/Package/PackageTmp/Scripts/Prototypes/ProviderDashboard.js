/* 
    Provider Dashboard Angular Module and Controller
    Author : Kalaram Verma
*/

// Start - Provider Dashboard Module
(function () {
    'use strict';

    angular.module("PDApp", []);

})();
// END - Provider Dashboard Module

// Start - Provider Dashboard Factory
(function () {
    'use strict';

    angular.module("PDApp").factory('Manager', Manager);

    Manager.$inject = ['$http', '$q', '$window'];

    function Manager($http, $q, $window) {
        var svc = {};

        // Provider Personal Details
        svc.PersonalDetails = function () {
            return [
                { Title: "Provider Name", Key: "Name" },
                { Title: "Provider Type", Key: "ProviderType" },
                { Title: "Specialty", Key: "Specialty" },
                { Title: "LOB", Key: "LOB" },
                { Title: "Board Certified", Key: "BoardCertified" },
                { Title: "Board Name", Key: "BoardName" },
                { Title: "Contact", Key: "PhoneNumber" }
            ];
        };

        // Profile Status Labels
        svc.PSLabels = function () {
            return [
                { Title: "Last PSV Date", Key: "LastPSVDate" },
                { Title: "Profile Status", Key: "Status" },
                { Title: "Active Contracts", Key: "ActiveContracts" },
                { Title: "inactive Contracts", Key: "InactiveContracts" },
            ];
        };

        // Data/Document Status Labels
        svc.DDSLabels = function () {
            return [
                { Title: "Medicare Information Details", Key: "Key1" },
                { Title: "CME Board Document", Key: "Key2" },
                { Title: "Hospital Privilege Information", Key: "Key3" },
                { Title: "DEA Supporting Document", Key: "Key4" },
                { Title: "Secondary Practice Location", Key: "Key5" },
                { Title: "Medicaid Supporting Document", Key: "Key6" },
            ];
        };

        // Expiry License Bar Chart View
        svc.LicenseBarChart = function (data) {

        };

        // Provider Personal Details
        svc.GetPersonalDetails = function () {
            var def = $q.defer();
            return $http.get(rootDir + '/Prototypes/GetProviderPersonalDetails').success(function (data) {
                def.resolve(data);
            })
                .error(function () {
                    def.reject("method not available.......");
                });
        };

        // Provider Tasks
        svc.GetProviderTasks = function (count) {
            var def = $q.defer();
            return $http.get(rootDir + '/Prototypes/GetProviderTasks?count=' + count).success(function (data) {
                def.resolve(data);
            })
                .error(function () {
                    def.reject("method not available.......");
                });
        };

        // Provider Credentialing Details
        svc.GetCredentialingDetails = function (count) {
            var def = $q.defer();
            return $http.get(rootDir + '/Prototypes/GetCredentialingDetails?count=' + count).success(function (data) {
                def.resolve(data);
            })
                .error(function () {
                    def.reject("method not available.......");
                });
        };

        // Provider Hospitals
        svc.GetProviderHospitals = function (count) {
            var def = $q.defer();
            return $http.get(rootDir + '/Prototypes/GetProviderHospitals?count=' + count).success(function (data) {
                def.resolve(data);
            })
                .error(function () {
                    def.reject("method not available.......");
                });
        };

        // Provider Groups
        svc.GetProviderGroups = function (count) {
            var def = $q.defer();
            return $http.get(rootDir + '/Prototypes/GetProviderGroups?count=' + count).success(function (data) {
                def.resolve(data);
            })
                .error(function () {
                    def.reject("method not available.......");
                });
        };

        return svc;
    }
})();
// END - Provider Dashboard Factory

// Start - Provider Dashboard Controller
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

    // Provider Dashboard Controller
    angular.module("PDApp").controller("PDController", PDController);

    // Provider Dashboard Controller Injection
    PDController.$inject = ["$scope", "$http", "$filter", "Manager"];

    function PDController($scope, $http, $filter, Manager) {

        $scope.GetMidLevels = function () {
            //Manager.RedirectProviderDirectory();
        };

        $scope.GetPCPs = function () {
            //Manager.RedirectProviderDirectory();
        };

        $scope.MyData = { "Provider Name": "dfsf fsfsf", BirthInfo: "IDOntKnow" };


        // Expired License Information Bar Chart
        Manager.GetPersonalDetails().then(function (response) {
            $scope.PD = response.data;
            $scope.PDPersonalDetails = Manager.PersonalDetails();
            $scope.PSLabels = Manager.PSLabels();
            console.log(response);
        });

        // Data Document Status
        $scope.DDSLabels = Manager.DDSLabels();

        // Provider Tasks
        Manager.GetProviderTasks(4).then(function (response) {
            $scope.ProviderTasks = response.data;
        });
        
        // Provider Credentialing Details
        Manager.GetCredentialingDetails(2).then(function (response) {
            $scope.ProviderCreds = response.data;
        });

        // Provider Hospitals
        Manager.GetProviderHospitals(5).then(function (response) {
            $scope.ProviderHospitals = response.data;
        });

        // Provider Groups
        Manager.GetProviderGroups(5).then(function (response) {
            $scope.ProviderGroups = response.data;
        });
    }
})();
// END - Provider Dashboard Controller