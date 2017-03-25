//--------------------- Angular Module ----------------------
var masterDataLicenseStatuses = angular.module("masterDataLicenseStatuses", ['ui.bootstrap']);

//Service for getting master data
masterDataLicenseStatuses.service('masterDataService', ['$http', '$q', function ($http, $q) {

    this.getMasterData = function (URL) {
        return $http.get(URL).then(function (value) { return value.data; });
    };

}]);

masterDataLicenseStatuses.service('messageAlertEngine', ['$rootScope', '$timeout', function ($rootScope, $timeout) {

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


//=========================== Controller declaration ==========================
masterDataLicenseStatuses.controller('masterDataLicenseStatusesController', ['$scope', '$http', '$filter', '$rootScope', 'masterDataService', 'messageAlertEngine', function ($scope, $http, $filter, $rootScope, masterDataService, messageAlertEngine) {

    $scope.LicenseStatuses = [];

    $http.get(rootDir + "/MasterDataNew/GetAllLicenseStatus").success(function (value) {
       
        try {
            for (var i = 0; i < value.length ; i++) {
                if (value[i] != null) {
                    value[i].LastModifiedDate = ($scope.ConvertDateFormat(value[i].LastModifiedDate)).toString();
                }

            }

            $scope.LicenseStatuses = angular.copy(value);
        } catch (e) {
          
        }
    });

    //Convert the date from database to normal
    $scope.ConvertDateFormat = function (value) {
        var returnValue = value;
        var dateData = "";

        try {
            if (value.indexOf("/Date(") == 0) {
                returnValue = new Date(parseInt(value.replace("/Date(", "").replace(")/", ""), 10));
                var day = returnValue.getDate() < 10 ? '0' + returnValue.getDate() : returnValue.getDate();
                var tempo = returnValue.getMonth() + 1;
                var month = tempo < 10 ? '0' + tempo : tempo;
                var year = returnValue.getFullYear();
                dateData = month + "-" + day + "-" + year;
            }
            return dateData;
        } catch (e) {
            return dateData;
        }
        return dateData;
    };

    

    $scope.tempLicenseStatuses = {};

    //------------ gets the template to ng-include for a table row / item -------------------
    $scope.getTemplate = function (LicenseStatuses) {
        if (LicenseStatuses.StateLicenseStatusID === $scope.tempLicenseStatuses.StateLicenseStatusID) return 'editLicenseStatuses';
        else return 'displayLicenseStatuses';
    };

    //-------------------- Edit LicenseStatus ----------
    $scope.editLicenseStatuses = function (LicenseStatus) {
        $scope.tempLicenseStatuses = angular.copy(LicenseStatus);
        $scope.disableAdd = true;
    };

    //------------------- Add LicenseStatus ---------------------
    $scope.addLicenseStatuses = function (LicenseStatus) {
        $scope.disableEdit = !$scope.disableEdit;
        $scope.disableAdd = true;
        var Month = new Date().getMonth() + 1;
        var _month = Month < 10 ? '0' + Month : Month;
        var _date = new Date().getDate() < 10 ? '0' + new Date().getDate() : new Date().getDate();
        var _year = new Date().getFullYear();
        var temp = {
            StateLicenseStatusID: 0,
            Title: "",            
            Status: "Active",
            LastModifiedDate: _month + "-" + _date + "-" + _year
        };
        $scope.LicenseStatuses.splice(0, 0, angular.copy(temp));
        $scope.tempLicenseStatuses = angular.copy(temp);
    };

    //------------------- Save LicenseStatus ---------------------
    $scope.saveLicenseStatuses = function (idx) {

        var addData = {
            StateLicenseStatusID: 0,
            Title: $scope.tempLicenseStatuses.Title,
            StatusType: 1,
        };

        var isExist = true;

        for (var i = 0; i < $scope.LicenseStatuses.length; i++) {

            if (addData.Title && $scope.LicenseStatuses[i].Title.replace(" ", "").toLowerCase() == addData.Title.replace(" ", "").toLowerCase()) {

                isExist = false;
                $scope.existErr = "The Title is Exist";
                break;
            }
        }
        if (!addData.Title) { $scope.error = "Please Enter the Title"; }
        if (addData.Title && isExist) {
            $http.post(rootDir + '/MasterDataNew/AddLicenseStatus', addData).
                success(function (data, status, headers, config) {
                    try {
                        //----------- success message -----------
                        if (data.status == "true") {
                            messageAlertEngine.callAlertMessage("LicenseStatus", "New Status License Details Added Successfully !!!!", "success", true);
                            data.stateLicenseStatusDetails.LastModifiedDate = $scope.ConvertDateFormat(data.stateLicenseStatusDetails.LastModifiedDate);
                            $scope.LicenseStatuses[idx] = angular.copy(data.stateLicenseStatusDetails);
                            $scope.reset();
                            $scope.error = "";
                            $scope.existErr = "";
                        }
                        else {
                            messageAlertEngine.callAlertMessage("LicenseStatusError", "Sorry Unable To Add State License Status !!!!", "danger", true);
                            $scope.LicenseStatuses.slpice(idx, 1);
                        }
                    } catch (e) {
                      
                    }
                }).
                error(function (data, status, headers, config) {
                    //----------- error message -----------
                    messageAlertEngine.callAlertMessage("LicenseStatusError", "Sorry Unable To Add State License Status !!!!", "danger", true);
                    $scope.LicenseStatuses.slpice(idx, 1);
                });
        }

        
    };

    //------------------- Update LicenseStatus ---------------------
    $scope.updateLicenseStatuses = function (idx) {

        var updateData = {
            StateLicenseStatusID: $scope.tempLicenseStatuses.StateLicenseStatusID,
            Title: $scope.tempLicenseStatuses.Title,
            StatusType: 1,
        };

        var isExist = true;

        for (var i = 0; i < $scope.LicenseStatuses.length; i++) {

            if (updateData.Title && $scope.LicenseStatuses[i].StateLicenseStatusID != updateData.StateLicenseStatusID && $scope.LicenseStatuses[i].Title.replace(" ", "").toLowerCase() == updateData.Title.replace(" ", "").toLowerCase()) {

                isExist = false;
                $scope.existErr = "The Title is Exist";
                break;
            }
            if ($scope.LicenseStatuses[i].StateLicenseStatusID == updateData.StateLicenseStatusID) {
                idx = i;
            }
        }
        if (!updateData.Title) { $scope.error = "Please Enter the Title"; }


        if (updateData.Title && isExist) {
            $http.post(rootDir + '/MasterDataNew/UpdateLicenseStatus', updateData).
                success(function (data, status, headers, config) {
                    try {
                        //----------- success message -----------
                        if (data.status == "true") {
                            messageAlertEngine.callAlertMessage("LicenseStatus", "Status License Details Updated Successfully !!!!", "success", true);
                            data.stateLicenseStatusDetails.LastModifiedDate = $scope.ConvertDateFormat(data.stateLicenseStatusDetails.LastModifiedDate);
                            $scope.LicenseStatuses[idx] = angular.copy(data.stateLicenseStatusDetails);
                            $scope.reset();
                            $scope.error = "";
                            $scope.existErr = "";
                        }
                    } catch (e) {
                     
                    }
                }).
                error(function (data, status, headers, config) {
                    //----------- error message -----------
                    messageAlertEngine.callAlertMessage("LicenseStatusError", "Sorry Unable To Update State License Status !!!!", "danger", true);                    
                });
        }


    };

    //----------------- LicenseStatus new add cancel ---------------
    $scope.cancelAdd = function () {
        $scope.LicenseStatuses.splice(0, 1);
        $scope.tempLicenseStatuses = {};
        $scope.disableEdit = false;
        $scope.disableAdd = false;
        $scope.error = "";
        $scope.existErr = "";
    };

    //-------------------- Reset LicenseStatus ----------------------
    $scope.reset = function () {
        $scope.tempLicenseStatuses = {};
        $scope.disableAdd = false;
        $scope.disableEdit = false;
        $scope.error = "";
        $scope.existErr = "";
    };

    $scope.CurrentPage = [];

    //-------------------------- angular bootstrap pagger with custom-----------------
    $scope.maxSize = 5;
    $scope.bigTotalItems = 0;
    $scope.bigCurrentPage = 1;

    //-------------------- page change action ---------------------
    $scope.pageChanged = function (pagnumber) {
        $scope.bigCurrentPage = pagnumber;
    };

    //-------------- current page change Scope Watch ---------------------
    $scope.$watch('bigCurrentPage', function (newValue, oldValue) {
        $scope.CurrentPage = [];
        var startIndex = (newValue - 1) * 10;
        var endIndex = startIndex + 9;
        if ($scope.LicenseStatuses) {
            for (startIndex; startIndex <= endIndex ; startIndex++) {
                if ($scope.LicenseStatuses[startIndex]) {
                    $scope.CurrentPage.push($scope.LicenseStatuses[startIndex]);
                } else {
                    break;
                }
            }
        }
    });
    //-------------- License Scope Watch ---------------------
    $scope.$watchCollection('LicenseStatuses', function (newValue, oldValue) {
        if (newValue) {
            $scope.bigTotalItems = newValue.length;

            $scope.CurrentPage = [];
            $scope.bigCurrentPage = 1;

            var startIndex = ($scope.bigCurrentPage - 1) * 10;
            var endIndex = startIndex + 9;

            for (startIndex; startIndex <= endIndex ; startIndex++) {
                if ($scope.LicenseStatuses[startIndex]) {
                    $scope.CurrentPage.push($scope.LicenseStatuses[startIndex]);
                } else {
                    break;
                }
            }
        }
    });
    $scope.filterData = function () {
        $scope.pageChanged(1);
    }

    $scope.IsValidIndex = function (index) {

        var startIndex = ($scope.bigCurrentPage - 1) * 10;
        var endIndex = startIndex + 9;

        if (index >= startIndex && index <= endIndex)
            return true;
        else
            return false;

    }
    //------------------- end ------------------
}]);
