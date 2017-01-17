//--------------------- Angular Module ----------------------
var credentialingList = angular.module("credentialingList", []);

credentialingList.service('messageAlertEngine', ['$rootScope', '$timeout', function ($rootScope, $timeout) {


}])

//=========================== Controller declaration ==========================
credentialingList.controller('credentialingListController', ['$scope', '$http', '$filter', '$rootScope', 'messageAlertEngine', function ($scope, $http, $filter, $rootScope, messageAlertEngine) {

    $scope.data = [{
        FName: 'Aakash',
        LName: "Singh",
        PType: "MEDICARE RISK",
        Specialty: "Abc",
        Plan: 'FREEDOM SAVINGS PLAN RX (HMO)',
        InitiatedDate: " ",
        Status: 'Active',
    }, {
        FName: 'Abhishek',
        LName: "Verma",
        PType: "MEDICARE LOB",
        Specialty: "Def",
        Plan: 'FREEDOM VIP CARE COPD (HMO SNP)',
        InitiatedDate: " ",
        Status: 'Active',
    }, {
        FName: 'Tarun',
        LName: "Kumar",
        PType: "MEDICARE RISK",
        Specialty: "Ghj",
        Plan: 'OPTIMUM GOLD REWARDS PLAN (HMO-POS)',
        InitiatedDate: " ",
        Status: 'Active',
    }, {
        FName: 'Sanjay',
        LName: "C",
        PType: "MEDICARE LOB",
        Specialty: "MEDICARE RISK",
        Plan: 'OPTIMUM DIAMOND REWARDS (HMO-POS SNP)',
        InitiatedDate: " ",
        Status: 'Active',
    }];
}]);
