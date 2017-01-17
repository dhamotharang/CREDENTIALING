//--------------------- Angular Module ----------------------
var masterDataLicenseStatuses = angular.module("masterDataLicenseStatuses", ['ui.bootstrap']);

//Service for getting master data
masterDataLicenseStatuses.service('masterDataService', ['$http', '$q', function ($http, $q) {

    this.getMasterData = function (URL) {
        return $http.get(rootDir + URL).then(function (value) { return value.data; });
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

    $http.get(rootDir + "/MasterDataNew/GetAllLicenseStatus").then(function (value) {
        //console.log("LicenseStatuses");
        //console.log(value);

        for (var i = 0; i < value.data.length ; i++) {
            if (value.data[i] != null) {
                value.data[i].LastModifiedDate = $scope.ConvertDateFormat(value.data[i].LastModifiedDate);
            }
        }

        $scope.LicenseStatuses = value.data;
        //console.log($scope.LicenseStatuses);
    });

    //Convert the date from database to normal
    $scope.ConvertDateFormat = function (value) {
        ////console.log(value);
        var returnValue = value;
        try {
            if (value.indexOf("/Date(") == 0) {
                returnValue = new Date(parseInt(value.replace("/Date(", "").replace(")/", ""), 10));
            }
            return returnValue;
        } catch (e) {
            return returnValue;
        }
        return returnValue;
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
        var temp = {
            StateLicenseStatusID: 0,
            Title: "",            
            Status: "Active",
            LastModifiedDate: new Date()
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

        //console.log("Saving LicenseStatus");
        
        if (addData.Title && isExist) {
            $http.post(rootDir + '/MasterDataNew/AddLicenseStatus', addData).
                success(function (data, status, headers, config) {
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
        }
        if (!updateData.Title) { $scope.error = "Please Enter the Title"; }

        //console.log("Updating LicenseStatus");

        if (updateData.Title && isExist) {
            $http.post(rootDir + '/MasterDataNew/UpdateLicenseStatus', updateData).
                success(function (data, status, headers, config) {
                    //----------- success message -----------
                    if (data.status == "true") {
                        messageAlertEngine.callAlertMessage("LicenseStatus", "Status License Details Updated Successfully !!!!", "success", true);
                        data.stateLicenseStatusDetails.LastModifiedDate = $scope.ConvertDateFormat(data.stateLicenseStatusDetails.LastModifiedDate);
                        $scope.LicenseStatuses[idx] = angular.copy(data.stateLicenseStatusDetails);                       
                        $scope.reset();
                        $scope.error = "";
                        $scope.existErr = "";
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
        //console.log($scope.CurrentPageProviders);
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
            //console.log($scope.CurrentPageProviders);
        }
    });
    //------------------- end ------------------
}]);
