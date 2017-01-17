
//Module declaration
var searchApp = angular.module('SearchApp', []);

//Controller declaration

searchApp.controller('SearchProviderCtrlNew', function ($scope, $http) {
    $('#specialities').select2();
    $('#groups').select2();
    $('#plans').select2();
    $('#plansnew').select2();
});