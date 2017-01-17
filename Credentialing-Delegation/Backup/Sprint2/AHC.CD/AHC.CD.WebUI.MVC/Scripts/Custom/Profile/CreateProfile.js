var profileApp = angular.module('profileApp', []);

//Controller declaration

profileApp.controller('profileAppCtrl', function ($scope, $http) {

    // work history control variable initialization

    $scope.newWork = false;
    $scope.legalNames = 0;
    $scope.legalNamesList = [];
    $scope.addressDetails = 0;
    $scope.addressDetailsList = [];

    $scope.$watch("addressDetails", function (newValue, oldValue) {
        for (var i = oldValue; i < newValue; i++) {
            $scope.addressDetailsList.push(i);
        }
    });

    $scope.deleteaddressDetails = function (index) {
        for (var i in $scope.addressDetailsList) {
            if (index == i) {
                $scope.addressDetailsList.splice(index, 1);
                break;
            }
        }
    };

    $scope.$watch("legalNames", function (newValue, oldValue) {
        for (var i = oldValue; i < newValue; i++) {
            $scope.legalNamesList.push(i);
        }
    });
    
    $scope.deleteLegalName = function (index) {
        for (var i in $scope.legalNamesList) {
            if (index == i) {
                $scope.legalNamesList.splice(index, 1);
                break;
            }
        }
    };
   
});