
var verificationApp = angular.module('VerificationApp', ['ngTable']);

verificationApp.service('messageAlertEngine', ['$rootScope', '$timeout', function ($rootScope, $timeout) {

    $rootScope.messageDesc = "";
    $rootScope.activeMessageDiv = "";
    $rootScope.messageType = "";

    var animateMessageAlertOff = function () {
        $rootScope.closeAlertMessage();
    };


    this.callAlertMessage = function (calledDiv, msg, msgType, dismissal) { //messageAlertEngine.callAlertMessage('updateHospitalPrivilege' + IndexValue, "Data Updated Successfully !!!!", "success", true);                            
        $rootScope.activeMessageDiv = calledDiv;
        $rootScope.messageDesc = msg;
        $rootScope.messageType = msgType;
        if (dismissal) {
            $timeout(animateMessageAlertOff, 5000);
        }
    }

    $rootScope.closeAlertMessage = function () {
        $rootScope.messageDesc = "";
        $rootScope.activeMessageDiv = "";
        $rootScope.messageType = "";
    }
}])


verificationApp.controller('VerificationCtrl', function ($scope, $http, $filter, ngTableParams, messageAlertEngine) {

    
    $scope.PrimarySourceVerfication = [];
    
    //Convert the date from database to normal
    var monthNames = ["Jan", "Feb", "Mar", "Apr", "May", "Jun",
  "Jul", "Aug", "Sep", "Oct", "Nov", "Dec"
    ];

    $scope.closeAlertMessage = function () {
        $rootScope.messageDesc = "";
        $rootScope.activeMessageDiv = "";
        $rootScope.messageType = "";
    }

    $scope.ConvertDateFormat = function (value) {
        var shortDate = null;
        if (value) {
            var regex = /-?\d+/;
            var matches = regex.exec(value);
            var dt = new Date(parseInt(matches[0]));
            var month = dt.getMonth() + 1;
            var monthString = month > 9 ? month : '0' + month;
            var monthName = monthNames[month];
            var day = dt.getDate();
            var dayString = day > 9 ? day : '0' + day;
            var year = dt.getFullYear();
            shortDate = monthString + '/' + dayString + '/' + year;
            //shortDate = dayString + 'th ' + monthName + ',' + year;
        }
        return shortDate;
    };

    
    //$scope.PrimarySourceVerfication = [{ title: 'MD', firstName: 'PARIKSITH', lastName: 'SINGH', plan: 'ULTIMATE HEALTH PLANS', dateOfSubmission: '15th Nov, 2014' },
    //                                   { title: 'MD', firstName: 'QAHTAN A', lastName: 'ABDULFATTAH', plan: 'FREEDOM VIP CARE COPD (HMO SNP)', dateOfSubmission: '2nd Dec, 2014' },
    //                                   { title: 'MD', firstName: 'IAN', lastName: 'ADAM', plan: 'OPTIMUM GOLD REWARDS PLAN (HMO-POS)', dateOfSubmission: '25th Nov, 2014' },
    //                                   { title: 'DO', firstName: 'MARC A', lastName: 'ALESSANDRONI', plan: 'OPTIMUM DIAMOND REWARDS (HMO-POS SNP)', dateOfSubmission: '30th Nov, 2014' },
    //                                   { title: 'MD', firstName: 'CRIDER', lastName: 'NORMA', plan: 'OPTIMUM PLATINUM PLAN (HMO-POS)', dateOfSubmission: '10th Dec, 2014' }];

    $scope.GetPSVList = function () {

        $http.get('/Credentialing/Verification/GetAllPSVList').
        success(function (data, status, headers, config) {
            $scope.PrimarySourceVerfication = data;
            
            
            var counts = [];

            if ($scope.PrimarySourceVerfication.length <= 10) {
                counts = [];
            }
            else if ($scope.PrimarySourceVerfication.length <= 25) {
                counts = [10, 25];
            }
            else if ($scope.PrimarySourceVerfication.length <= 50) {
                counts = [10, 25, 50];
            }
            else if ($scope.PrimarySourceVerfication.length <= 100) {
                counts = [10, 25, 50, 100];
            }
            else if ($scope.PrimarySourceVerfication.length > 100) {
                counts = [10, 25, 50, 100];
            }

            //==================ngtable=======
            $scope.tableParams = new ngTableParams({
                page: 1,            // show first page
                count: 10,          // count per page
                
            }, {
                counts : counts,
                total: $scope.PrimarySourceVerfication.length, // length of data
                getData: function ($defer, params) {
                    // use build-in angular filter
                    //var orderedData = params.filter() ?
                    //        $filter('filter')($scope.PrimarySourceVerfication, params.filter()) :
                    //        $scope.PrimarySourceVerfication;

                    //$scope.PrimarySourceVerfication = orderedData.slice((params.page() - 1) * params.count(), params.page() * params.count());

                    //params.total(orderedData.length); // set total for recalc pagination
                    //$defer.resolve($scope.PrimarySourceVerfication);
                }
            });
            // this callback will be called asynchronously
            // when the response is available
        }).
        error(function (data, status, headers, config) {
            // called asynchronously if an error occurs
            // or server returns response with an error status.
        });
    };

    //$scope.GetPendingPSVList = function (id) {
    //    var credInfoId = id;
    //    $http.get('/Credentialing/Verification/GetPendingPSVList?credinfoId=' + credInfoId).
    //    success(function (data, status, headers, config) {
            
    //        if (data.status != "true") {
    //            messageAlertEngine.callAlertMessage("pendingPSVError", data.status, "danger", true);
    //        }
            
            
    //    }).
    //    error(function (data, status, headers, config) {
    //        messageAlertEngine.callAlertMessage("pendingPSVError", data.status, "danger", true);
    //    });
    //};

    $scope.hideErrorDiv = function () {
        $scope.hideError = true;
    }

    $scope.LoadData = function () {

        $scope.GetPSVList();
    };
});
