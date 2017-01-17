
Cred_SPA_App.controller('PlanReportController', function ($scope, $rootScope, $http) {

    $http.get('/Credentialing/CnD/GetContractGrid?credentialingInfoID=' + credId).
   success(function (data, status, headers, config) {
       $scope.PlanReportList = angular.copy(data);
       console.log($scope.PlanReportList);
   }).
   error(function (data, status, headers, config) {
       //console.log("Sorry internal master data cont able to fetch.");
   });
});