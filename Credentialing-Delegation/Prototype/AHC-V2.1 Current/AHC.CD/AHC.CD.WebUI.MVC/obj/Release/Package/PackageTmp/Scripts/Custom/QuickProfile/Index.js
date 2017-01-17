var quickProfile = angular.module('QuickProfileApp', []);

quickProfile.controller("QuickProfileController", function ($scope, $http, $sce) {

    $scope.callView = function () {
        $http({
            method: 'GET',
            url: '/Profile/QuickUpdate/CallSubSection?ViewName=' +"_AddEditPersonalIdentification"
            //url: '/Profile/MasterProfile/get?profileId=' + profileId
        }).success(function (data, status, headers, config) {
            console.log(data);
            $scope.sectionload = $sce.trustAsHtml(data);
            
        }).error(function (data, status, headers, config) {
            
        });
    };

});