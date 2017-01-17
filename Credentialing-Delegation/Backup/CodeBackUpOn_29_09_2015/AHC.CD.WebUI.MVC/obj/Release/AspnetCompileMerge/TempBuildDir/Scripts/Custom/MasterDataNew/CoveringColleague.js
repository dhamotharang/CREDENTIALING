//--------------------- Angular Module ----------------------
var coveringColleagueApp = angular.module("CoveringColleagueApp", ['ui.bootstrap']);

//Service for getting master data
coveringColleagueApp.service('masterDataService', ['$http', '$q', function ($http, $q) {

    this.getMasterData = function (URL) {
        return $http.get(URL).then(function (value) { return value.data; });
    };

}]);


//-------------------- Angulat controller----------------------
coveringColleagueApp.controller('CoveringColleagueController', function ($scope, masterDataService) {

    $scope.CoveringColleagues = [];
    masterDataService.getMasterData(rootDir + "/Profile/MasterData/GetAllPaymentAndRemittance").then(function (CoveringColleagues) {
        $scope.CoveringColleagues = CoveringColleagues;

        console.log('$scope.CoveringColleagues');
        console.log($scope.CoveringColleagues);
    });

    $scope.AddMode = false;
    $scope.EditMode = false;
    $scope.ViewMode = false;
    $scope.ListMode = true;
    //-------------------- Add office managers ----------------------------------------
    $scope.addCoveringColleagues = function () {

        $scope.ListMode = false;
        $scope.AddMode = true;
    }

    $scope.saveCoveringColleagues = function () {

        $scope.ListMode = true;
        $scope.AddMode = false;
        $scope.EditMode = false;
        $scope.ViewMode = false;
    }

    //-----------------------view office managers ----------------------------
    $scope.tempSecondObject = null;
    $scope.tempSecondObject = { "Country": "United States", "CountryCodeTelephone": "+1", "CountryCodeFax": "+1" };


    $scope.viewCoveringColleagues = function (obj) {

        $scope.tempSecondObject = obj;
        $scope.ViewMode = true;
        $scope.AddMode = false;
        $scope.ListMode = false;
        $scope.EditMode = false;


    }

    //---------------------edit office Manager ------------------------------------

    $scope.editCoveringColleagues = function (obj) {

        $scope.tempSecondObject = obj;
        $scope.ViewMode = false;
        $scope.AddMode = false;
        $scope.ListMode = false;
        $scope.EditMode = true;
    }

    $scope.updateCoveringColleagues = function () {

        $scope.EditMode = false;
        $scope.AddMode = false;
        $scope.ViewMode = false;
        $scope.ListMode = true;
    }

    //------------------------cancel ------------------------------
    $scope.cancel = function () {

        $scope.tempSecondObject = null;
        $scope.tempSecondObject = { "Country": "United States", "CountryCodeTelephone": "+1", "CountryCodeFax": "+1" };

        $scope.EditMode = false;
        $scope.AddMode = false;
        $scope.ViewMode = false;
        $scope.ListMode = true;
    }




    //--------------------table-----------------------------------------------------
    $scope.CurrentPage = [];

    //-------------------------- angular bootstrap pagger with custom-----------------
    $scope.maxSize = 5;
    $scope.bigTotalItems = 0;
    $scope.bigCurrentPage = 1;

    //-------------------- page change action ---------------------
    $scope.pageChanged = function (pagnumber) {
        $scope.bigCurrentPage = pagnumber;
    };

    //-------------- current page change Scope Watch ---------------------
    $scope.$watch('bigCurrentPage', function (newValue, oldValue) {
        $scope.CurrentPage = [];
        var startIndex = (newValue - 1) * 10;
        var endIndex = startIndex + 9;
        if ($scope.CoveringColleagues) {
            for (startIndex; startIndex <= endIndex ; startIndex++) {
                if ($scope.CoveringColleagues[startIndex]) {
                    $scope.CurrentPage.push($scope.CoveringColleagues[startIndex]);
                } else {
                    break;
                }
            }
        }
        ////console.log($scope.CurrentPageProviders);
    });
    //-------------- License Scope Watch ---------------------
    $scope.$watchCollection('CoveringColleagues', function (newValue, oldValue) {
        if (newValue) {
            $scope.bigTotalItems = newValue.length;

            $scope.CurrentPage = [];
            $scope.bigCurrentPage = 1;

            var startIndex = ($scope.bigCurrentPage - 1) * 10;
            var endIndex = startIndex + 9;

            for (startIndex; startIndex <= endIndex ; startIndex++) {
                if ($scope.CoveringColleagues[startIndex]) {
                    $scope.CurrentPage.push($scope.CoveringColleagues[startIndex]);
                } else {
                    break;
                }
            }
            ////console.log($scope.CurrentPageProviders);
        }
    });
    //------------------- end ------------------

});