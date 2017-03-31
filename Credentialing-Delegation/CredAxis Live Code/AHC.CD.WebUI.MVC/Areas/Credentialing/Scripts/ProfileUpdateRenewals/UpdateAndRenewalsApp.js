var UpdateAndRenewalsApp = angular.module("UpdateAndRenewalsApp", ['toaster', 'smart-table', 'nvd3', 'uiSwitch', 'ServiceTracker']);


UpdateAndRenewalsApp.value("MasterSettings",
    [{ ActionType: "Updates", TableCaption: "Profile Updates", HistorySwitchButton: false, RequestSwitchButton: false,TableType:1 },
     { ActionType: "Renewals", TableCaption: "Profile Renewals", HistorySwitchButton: false, RequestSwitchButton: false, TableType: 1 },
     { ActionType: "Requests", TableCaption: "Credentialing Requests", HistorySwitchButton: false, RequestSwitchButton: true, TableType: 2 },
     { ActionType: "History", TableCaption: "Updates & Renewals History", HistorySwitchButton: true, RequestSwitchButton: false, TableType: 1 }]);

UpdateAndRenewalsApp.constant("$loadash",window._);

UpdateAndRenewalsApp.run(["$rootScope", function ($rootScope) {
    $rootScope.ProfileUpdates = [];
    $rootScope.TempProfileUpdates = [];
    $rootScope.CredentialingRequests = [];
    $rootScope.TempCredentialingRequests = [];
    $rootScope.filtered = [];
    $rootScope.TemporaryObject = {};
    $rootScope.IsProvider = IsProvider;
}])

UpdateAndRenewalsApp.config(['$httpProvider', function ($httpProvider) {
    $httpProvider.defaults.headers.common["X-Requested-With"] = 'XMLHttpRequest';
    //cfpLoadingBarProvider.includeSpinner = true;
    $httpProvider.interceptors.push('interceptHttp');
}]);

