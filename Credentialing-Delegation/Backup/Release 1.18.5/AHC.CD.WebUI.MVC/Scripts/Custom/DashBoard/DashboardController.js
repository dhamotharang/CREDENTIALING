// -------------------- Dashbooard controller Angular Module-------------------------------
//---------- Author KRGLV -------------

//------------------------- angular module ----------------------------
var dashboardApp = angular.module('dashboardApp', ['ui.bootstrap']);

//------------- angular tool tip recall directive ---------------------------
dashboardApp.directive('tooltip', function () {
    return function (scope, elem) {
        elem.tooltip();
    };
});

//----------------------- Day Left function return Left Days ---------------------------
var GetRenewalDayLeft = function (datevalue) {
    if (datevalue) {
        var oneDay = 24 * 60 * 60 * 1000; // hours*minutes*seconds*milliseconds

        var currentdate = new Date(new Date().getFullYear(), new Date().getMonth(), new Date().getDate());

        var secondDate = new Date(2008, 01, 22);

        return Math.round((datevalue.getTime() - currentdate.getTime()) / (oneDay));
    }
    return null;
};
//------------------------------- convert date format -----------------------------
var ConvertDateFormat = function (value) {
    if (value) {
        return new Date(parseInt(value.replace("/Date(", "").replace(")/", ""), 10));
    } else {
        return value;
    }
};

// ------------------ dashboard root scope ------------------
//dashboardApp.run(['$rootScope', function ($rootScope) {
//    $rootScope.Role = "PRO";
//    //----------------- temp method for role base toggle --------------------
//    $rootScope.ToggleRoles = function () {
//        if ($rootScope.Role == "PRO") {
//            $rootScope.Role = "CCO";
//        } else {
//            $rootScope.Role = "PRO";
//        }
//    };
//}]);
