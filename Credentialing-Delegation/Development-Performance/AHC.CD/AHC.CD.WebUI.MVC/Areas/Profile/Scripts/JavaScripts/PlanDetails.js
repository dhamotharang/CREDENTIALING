
//--------------------- Angular Module ----------------------
var masterPlans = angular.module("masterPlans", []);

masterPlans.service('messageAlertEngine', ['$rootScope', '$timeout', function ($rootScope, $timeout) {

    
}])

//=========================== Controller declaration ==========================
masterPlans.controller('masterPlansController', ['$scope', '$http', '$filter', '$rootScope', 'messageAlertEngine', function ($scope, $http, $filter, $rootScope, messageAlertEngine) {

    $scope.AllPlans = [{
        logo: 'Freedom.jpg',
        PlanName: "FREEDOM SAVINGS PLAN RX (HMO)",
        ICName: "FREEDOM",
        planType: "MEDICARE RISK",
        ContactPersonName: 'Aakash Singh',
        planStatus: "Active",
        MailTo: 'aksdkj@sndkj.in',
    }, {
        logo: 'Freedom.jpg',
        PlanName: "FREEDOM VIP CARE COPD (HMO SNP)",
        ICName: "FREEDOM",
        planType: "MEDICARE LOB",
        ContactPersonName: 'Ved Kumar',
        planStatus: "Draft",
        MailTo: 'aksdkj@sndkj.in',
    }, {
        logo: 'Optimum.jpg',
        PlanName: "OPTIMUM GOLD REWARDS PLAN (HMO-POS)",
        ICName: "OPTIMUM",
        planType: "MEDICARE RISK",
        ContactPersonName: 'Sanjay C',
        planStatus: "Deactive",
        MailTo: 'aksdkj@sndkj.in',
    }, {
        logo: 'Optimum.jpg',
        PlanName: "OPTIMUM DIAMOND REWARDS (HMO-POS SNP)",
        ICName: "OPTIMUM",
        planType: "MEDICARE LOB",
        ContactPersonName: 'Ritesh Bilgaiyan',
        planStatus: "Active",
        MailTo: 'aksdkj@sndkj.in',
    }, {
        logo: 'Optimum.jpg',
        PlanName: "OPTIMUM PLATINUM PLAN (HMO-POS)",
        ICName: "OPTIMUM",
        planType: "MEDICARE RISK",
        ContactPersonName: 'sachin Garg',
        planStatus: "Active",
        MailTo: 'aksdkj@sndkj.in',
    }];
}]);
