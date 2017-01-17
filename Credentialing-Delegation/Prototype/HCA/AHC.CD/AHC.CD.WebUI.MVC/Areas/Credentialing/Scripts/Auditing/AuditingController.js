//------------------------- document ready state ------------------
$(document).ready(function () {

});

//----------------------- ng app -------------------------
var initCredApp = angular.module('InitCredApp', ['ngTable']);

initCredApp.controller('singleProviderCtrl', ['$scope', '$http', '$timeout', '$filter', 'ngTableParams', function ($scope, $http, $timeout, $filter, ngTableParams) {

    //----------------------- filter data ---------------------
    $scope.filterData = {
        planId: 0,
        BusinessEntityId: 0
    };

    //--------------------- init required data -----------------------
    $scope.DataSubmited = false;
    $scope.AuditDocSubmited = false;
    $scope.PSVDocSubmited = false;
    $scope.DocSubmited = false;


    var CredentialingInitiationViewModel = {};
    $scope.TwoDocuments = false;
    $scope.OneDocuments = false;
    var ConvertDateFormat = function (value) {
        if (value) {

            return new Date(parseInt(value.replace("/Date(", "").replace(")/", ""), 10));
        } else {
            return value;
        }
    };
    $scope.allPlanProviders = [
        { "Plan": "Humana", "Group": "Access", "Specialty": "Anesthesiology", "Location": "5350 Spring Hill Drive", "CredentialledDate": "05-03-2014" },
        { "Plan": "Freedom HMO", "Group": "Access", "Specialty": "Addiction Medicine", "Location": "5350 Spring Hill Drive", "CredentialledDate": "03-03-2014" },
        { "Plan": "Freedom HMO", "Group": "Access2", "Specialty": "Addiction Medicine", "Location": "5350 Spring Hill Drive", "CredentialledDate": "01-02-2014" },
        { "Plan": "Wellcare", "Group": "MIRRA", "Specialty": "Dermatology", "Location": "5350 Spring Hill Drive", "CredentialledDate": "05-03-2013" },
    ];

    $scope.temp = '';

    $scope.pushplan = function (data) {
        $scope.temp = data;
    }
    $scope.showDocDiv = function () {
        $scope.TwoDocuments = true;
    };
    $scope.showDocDecred = function () {
        $scope.OneDocuments = true;
    };
    $scope.SearchProviderPanelToggle = function (divId) {
        $("#" + divId).slideToggle();
    };
    $scope.SearchProviderPanelToggleDown = function (divId) {
        $(".closePanel").slideUp();
        $("#" + divId).slideToggle();

    };
    var data = [];
    $scope.SearchFilter = function (obj) {

        $scope.loadingAjax = true;
        $scope.allProviders = [];
        $scope.Npi = null;
        $http({
            method: 'GET',
            url: '/InitCredentialing/GetAllProviders?tempObject=obj'
        }).success(function (response, status, headers, config) {

            $scope.allProviders = response;
            data = response;

            for (var i = 0; i < $scope.allProviders.length ; i++) {
                $scope.allProviders[i].CredDate = ConvertDateFormat($scope.allProviders[i].CredDate).toDateString();
                $scope.allProviders[i].IsCkecked = false;
                data[i].IsCkecked = false;
            }
            //$scope.SearchProviderPanelToggleDown('SearchProviderResultPanel');
            $("#AuditSearch").slideToggle();
            //$("#SearchProviderResultPanel").slideToggle();
            //$("#SearchProviderResultPanel1").slideToggle();

            $scope.loadingAjax = false;
            $scope.SearchProviderTable.reload();
            $scope.SearchProviderTable1.reload();
            $scope.SearchProviderTable3.reload();
        }).error(function (data, status, headers, config) {
            $scope.loadingAjax = false;

        });
    };

    $scope.SearchProviderTable = new ngTableParams(
    {
        page: 1,            // show first page
        count: 5          // count per page
    },
    {
        total: data.length,
        getData: function ($defer, params) {
            var orderedData = params.filter() ?
                                $filter('filter')(data, params.filter()) :
                                data;
            params.total(orderedData.length);
            $defer.resolve(orderedData.slice((params.page() - 1) * params.count(), params.page() * params.count()));
        }
    });

    $scope.SearchProviderTable1 = new ngTableParams(
    {
        page: 1,            // show first page
        count: 5          // count per page
    },
    {
        total: data.length,
        getData: function ($defer, params) {
            var orderedData = params.filter() ?
                                $filter('filter')(data, params.filter()) :
                                data;
            params.total(orderedData.length);
            $defer.resolve(orderedData.slice((params.page() - 1) * params.count(), params.page() * params.count()));
        }
    });


    $scope.SearchProviderTable3 = new ngTableParams(
    {
        page: 1,            // show first page
        count: 5          // count per page
    },
    {
        total: data.length,
        getData: function ($defer, params) {
            var orderedData = params.filter() ?
                                $filter('filter')(data, params.filter()) :
                                data;
            params.total(orderedData.length);
            $defer.resolve(orderedData.slice((params.page() - 1) * params.count(), params.page() * params.count()));
        }
    });

    $scope.clearSearch = function () {
        $scope.allProviders = null;
        $scope.Npi = null;
        $scope.DataSubmited = false;
        $scope.AuditDocSubmited = false;
        $scope.PSVDocSubmited = false;
        $scope.DocSubmited = false;
        $scope.generate_package=false;
    };

    $scope.InitiateCredentialing = function () {
        $scope.DataSubmited = true;
        $scope.SearchProviderPanelToggleDown('SearchProviderResultPanel2');        
    };

    $scope.AuditDoctSubmit = function () {
        $scope.AuditDocSubmited = true;
        $scope.SearchProviderPanelToggleDown('SearchProviderResultPanel3');
    };

    $scope.PSVDoctSubmit = function () {
        $scope.PSVDocSubmited = true;
        $scope.SearchProviderPanelToggleDown('SearchProviderResultPanel4');
    };
    $scope.DoctSubmit = function () {
        $scope.DocSubmited = true;
        $(".closePanel").slideUp();
        $timeout(function () {
            $("#SuccessMessage").hide();
        }, 5000);
    };

    $scope.PSVChecked = '1';

    $scope.PSVCheck = function () {
        if ($scope.PSVChecked == '1') {
            $scope.PSVChecked = '2';
        }
        else {
            $scope.PSVChecked = '1';
        }
    }

    $scope.DocumentRepoChecked = '1';

    $scope.DocumentRepoCheck = function () {
        if ($scope.DocumentRepoChecked == '1') {
            $scope.DocumentRepoChecked = '2';
        }
        else {
            $scope.DocumentRepoChecked = '1';
        }
    }

    $scope.generatePackage = function () {
        $scope.generate_package = true;
        $scope.SearchProviderPanelToggle('SearchProviderResultPanel2');
    };

}]);