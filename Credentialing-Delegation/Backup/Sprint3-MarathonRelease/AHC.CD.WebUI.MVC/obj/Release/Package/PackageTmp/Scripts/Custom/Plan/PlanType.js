var planApp = angular.module('planApp', []);

planApp.controller('PlanType', function ($scope) {

    // scope valiable init

    $scope.PlanTypes = 0;
  




    $scope.newPlanType = function () {
        $scope.PlanTypes = 1;
    }

    $scope.AddNewPlanType = function () {
        //=========Save method here ===============
        $scope.PlanTypes = 0;
    }

    $scope.removePlanType = function () {
        $scope.PlanTypes = 0;
    };



});