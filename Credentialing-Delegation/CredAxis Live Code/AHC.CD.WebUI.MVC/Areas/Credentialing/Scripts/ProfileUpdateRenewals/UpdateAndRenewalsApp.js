var UpdateAndRenewalsApp = angular.module("UpdateAndRenewalsApp", ['toaster', 'smart-table', 'nvd3', 'uiSwitch','chieffancypants.loadingBar', 'ServiceTracker']);


UpdateAndRenewalsApp.value("MasterSettings",
    [{ ActionType: "Updates", TableCaption: "Profile Updates", HistorySwitchButton: false, RequestSwitchButton: false,TableType:1 },
     { ActionType: "Renewals", TableCaption: "Profile Renewals", HistorySwitchButton: false, RequestSwitchButton: false, TableType: 1 },
     { ActionType: "Requests", TableCaption: "Credentialing Requests", HistorySwitchButton: false, RequestSwitchButton: true, TableType: 2 },
     { ActionType: "UpdatesHistory", TableCaption: "Updates History", HistorySwitchButton: true, RequestSwitchButton: false, TableType: 1 },
    { ActionType: "RenewalsHistory", TableCaption: "Renewals History", HistorySwitchButton: true, RequestSwitchButton: false, TableType: 1 },
    { ActionType: "RequestsHistory", TableCaption: "Credentialing History", HistorySwitchButton: true, CredRequestSwitchButton: true, TableType: 2 }]);

UpdateAndRenewalsApp.constant("$loadash",window._);

UpdateAndRenewalsApp.run(["$rootScope", function ($rootScope) {
    $rootScope.ProfileUpdates = [];
    $rootScope.TempProfileUpdates = [];
    $rootScope.CredentialingRequests = [];
    $rootScope.TempCredentialingRequests = [];
    $rootScope.filtered = [];
    $rootScope.TemporaryObject = {};
    $rootScope.IsProvider = IsProvider;
    //$rootScope.TableHighlight = {};

    //if (IsProvider)
    //    $rootScope.TableHighlight = { "background-color": "white", "color": "black" }
    //else
    //    $rootScope.TableHighlight = { "background-color": "white", "color": "black", "cursor": "pointer" }

}])

UpdateAndRenewalsApp.config(['$httpProvider', 'cfpLoadingBarProvider', function ($httpProvider, cfpLoadingBarProvider) {
    $httpProvider.defaults.headers.common["X-Requested-With"] = 'XMLHttpRequest';
    cfpLoadingBarProvider.includeSpinner = false;
    cfpLoadingBarProvider.includeBar = true;
    $httpProvider.interceptors.push('interceptHttp');
}]);

