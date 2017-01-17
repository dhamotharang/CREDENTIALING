
Cred_SPA_App.controller('PlanReportController', function ($scope, $rootScope, $http) {
    //change
    $http.get(rootDir + '/Credentialing/CnD/GetContractGrid?credentialingInfoID=' + credId).
   success(function (data, status, headers, config) {
       $scope.PlanReportList = angular.copy(data);
      
   }).
   error(function (data, status, headers, config) {
      
   });
});