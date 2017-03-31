// -------------------- Dashboard controller Angular Module-------------------------------
//---------- Author KRGLV -------------

//------------------------- angular module ----------------------------
//var dashboardApp = angular.module('dashboardApp', ['ui.bootstrap', 'smart-table']);
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
dashboardApp.run(['$rootScope', function ($rootScope) {
    //----------------- filter day left ranges --------------------
    $rootScope.days = [5, 10, 15, 20, 25, 30, 35, 40, 45, 50, 55, 60, 70, 80, 90, 180];
}]);
