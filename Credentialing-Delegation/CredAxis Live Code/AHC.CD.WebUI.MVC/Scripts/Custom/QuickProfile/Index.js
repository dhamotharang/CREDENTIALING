var quickProfile = angular.module('QuickProfileApp', []);

quickProfile.controller("QuickProfileController", function ($scope, $http, $sce) {

    $scope.callView = function () {
        $http({
            method: 'GET',
            url: rootDir + '/Profile/QuickUpdate/CallSubSection?ViewName=' + "_AddEditPersonalIdentification"
            //url: '/Profile/MasterProfile/get?profileId=' + profileId
        }).success(function (data, status, headers, config) {
            try {
                $scope.sectionload = $sce.trustAsHtml(data);
            } catch (e) {
              
            }
            
        }).error(function (data, status, headers, config) {
            
        });
    };

});