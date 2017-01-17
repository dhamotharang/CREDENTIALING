var homeApp = angular.module("HomeApp", []);

homeApp.controller("homeCtrl", ['$rootScope', '$scope', '$filter', function ($rootScope, $scope, $filter) {
    $scope.selectedRole = "SuperAdmin";
    $scope.allMenuItems = angular.copy(menudata);
    $scope.filterMenu = function () {
        var filteredData = $filter('filter')($scope.allMenuItems, { Role: $scope.selectedRole });
        $scope.menuItem = filteredData[0].RoleMenu;
    };
    $scope.filterMenu();

    $scope.selectanewrole = function (role) {
        $scope.selectedRole = role;
        $scope.filterMenu();
    }

    //$rootScope.yo = "hello";

    //$scope.UMObject = {
    //    PlaceOfServiceID: [],
    //    Facility: '',
    //    primaryDX: ''
    //};
}]);


var createAuthorizations = function () {
    $.ajax({
        type: 'post',
        url: '/Home/Authorization',
        data: $('#UM_auth_form').serialize(),
        cache: false,
        error: function () {
            //alert(" There is an error...");
        },
        success: function (data, textStatus, XMLHttpRequest) {
            $('#auth_preview_modal').html(data);
            showModal('authPreviewModal');
            //$('.thePOS').html(data.data.PlaceOfService);
        }
    });
};

var pendAuthorizations = function () {
    $.ajax({
        type: 'post',
        url: '/Home/PendAuthorization',
        data: $('#UM_auth_form').serialize(),
        cache: false,
        error: function () {
            
        },
        success: function (data, textStatus, XMLHttpRequest) {
            $('#auth_preview_modal').html(data);
            showModal('authPendModalCreate');
            //$('.thePOS').html(data.data.PlaceOfService);
        }
    });
};

// Calling SetCulture action to change the culture :
$(function () {
    $("#menu-container a:contains('English')").attr('href', '/Home/SetCulture/en');
    $("#menu-container a:contains('Spanish')").attr("href", "/Home/SetCulture/es");
});