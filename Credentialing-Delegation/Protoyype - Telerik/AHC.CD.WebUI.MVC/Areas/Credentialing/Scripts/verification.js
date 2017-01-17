
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

    $scope.progressbar = true;
    
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

    $scope.GetPSVList = function () {

        $http.get(rootDir + '/Credentialing/Verification/GetAllPSVList').
        success(function (data, status, headers, config) {
            
            try {

                $scope.progressbar = false;

                if (data.length > 0) {
                    $scope.showNoData = false;
                }
                else {
                    $scope.showNoData = true;
                }

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
                $scope.data = [];
                for (var i = 0; i < $scope.PrimarySourceVerfication.length; i++) {
                    //if ($scope.PrimarySourceVerfication[i].CredentialingLogs[0].CredentialingActivityLogs==null)
                    //{
                    var InitiationDate = $scope.PrimarySourceVerfication[i].InitiationDate;
                    if ($scope.PrimarySourceVerfication[i].Profile.PersonalDetail.ProviderTitles.length > 0) {
                        $scope.data.push({ 'Title': $scope.PrimarySourceVerfication[i].Profile.PersonalDetail.ProviderTitles[0].ProviderType.Title, 'FirstName': $scope.PrimarySourceVerfication[i].Profile.PersonalDetail.FirstName, 'LastName': $scope.PrimarySourceVerfication[i].Profile.PersonalDetail.LastName, 'PlanName': $scope.PrimarySourceVerfication[i].Plan.PlanName, 'InitiationDate': InitiationDate, 'ProfileID': $scope.PrimarySourceVerfication[i].ProfileID, 'CredentialingInfoID': $scope.PrimarySourceVerfication[i].CredentialingInfoID });
                    }
                    else {
                        $scope.data.push({ 'Title': '', 'FirstName': $scope.PrimarySourceVerfication[i].Profile.PersonalDetail.FirstName, 'LastName': $scope.PrimarySourceVerfication[i].Profile.PersonalDetail.LastName, 'PlanName': $scope.PrimarySourceVerfication[i].Plan.PlanName, 'InitiationDate': InitiationDate, 'ProfileID': $scope.PrimarySourceVerfication[i].ProfileID, 'CredentialingInfoID': $scope.PrimarySourceVerfication[i].CredentialingInfoID });
                    }
                    //}
                }

                $scope.tableParams = new ngTableParams({
                    page: 1,            // show first page
                    count: 10,
                    sorting: {

                        InitiationDate: 'desc'
                        //name: 'asc'     // initial sorting
                    }// count per page

                }, {
                    counts: counts,
                    total: $scope.data.length, // length of data
                    getData: function ($defer, params) {
                        var filteredData = params.filter() ?
                                $filter('filter')($scope.data, params.filter()) :
                                $scope.data;

                        var orderedData = params.sorting() ?
                                $filter('orderBy')(filteredData, params.orderBy()) :
                                $scope.data;

                        params.total(orderedData.length); // set total for recalc pagination
                        $defer.resolve(orderedData.slice((params.page() - 1) * params.count(), params.page() * params.count()));
                    }
                });
            } catch (e) {
             
            }
            // this callback will be called asynchronously
            // when the response is available
        }).
        error(function (data, status, headers, config) {
            // called asynchronously if an error occurs
            // or server returns response with an error status.
            $scope.showNoData = true;
        });
    };

   
    $scope.LoadData = function () {

        $scope.GetPSVList();
    };

    
});
