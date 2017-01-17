var companyApp = angular.module('companyApp', []);

companyApp.controller('company', function ($scope) {

    // scope valiable init

    $scope.companys = 0;
 

    $scope.newCompany = function () {
        $scope.companys = 1;
    }

    $scope.AddCompany = function () {
        //=========Save method here ===============
        $scope.companys = 0;
    }

    $scope.removeCompany = function () {
        $scope.companys = 0;
    };




});
//plan type association
companyApp.controller('planType', function ($scope) {

    // scope valiable init

    $scope.planType = 0;
    $scope.planTypesList = [];

    $scope.newPlanType = function () {
        $scope.planType = 1;
    }

    $scope.AddPlanType = function () {
        //=========Save method here ===============
        $scope.planType = 0;
    }

    $scope.removePlanType = function () {
        $scope.planType = 0;
    };


});