
//Module declaration
var notificationApp = angular.module('AdminNotificationApp', []);

//Controller declaration

notificationApp.controller('NotificationCtrlNew', function ($scope, $http) {


    $scope.addNewNotification = false;

    $scope.reCredentialing = false;

    $('#recred').hide();
    $('#notificationTemplateContainer').hide();


    $('#recievers').select2();




});


function displayAdditionlinfo() {

    $('#recred').show(); 
    $('#notificationTemplateContainer').slideToggle();
}