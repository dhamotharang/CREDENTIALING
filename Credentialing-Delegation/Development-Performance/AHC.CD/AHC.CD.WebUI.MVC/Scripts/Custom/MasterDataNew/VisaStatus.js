//--------------------- Angular Module ----------------------
var masterDataVisaStatuses = angular.module("masterDataVisaStatuses", ['ui.bootstrap']);


//Service for getting master data
masterDataVisaStatuses.service('masterDataService', ['$http', '$q', function ($http, $q) {

    this.getMasterData = function (URL) {
        return $http.get(URL).then(function (value) { return value.data; });
    };

}]);

masterDataVisaStatuses.service('messageAlertEngine', ['$rootScope', '$timeout', function ($rootScope, $timeout) {

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
masterDataVisaStatuses.controller('masterDataVisaStatusesController', ['$scope', '$http', '$filter', '$rootScope', 'masterDataService', 'messageAlertEngine', function ($scope, $http, $filter, $rootScope, masterDataService, messageAlertEngine) {

    $scope.VisaStatuses = [];

    $http.get(rootDir + "/MasterDataNew/GetAllVisaStatus").success(function (value) {
        try {

            for (var i = 0; i < value.length ; i++) {
                if (value[i] != null) {
                    value[i].LastModifiedDate = ($scope.ConvertDateFormat(value[i].LastModifiedDate)).toString();
                }

            }

            $scope.VisaStatuses = angular.copy(value);
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

    

    $scope.tempVisaStatuses = {};

    //------------ gets the template to ng-include for a table row / item -------------------
    $scope.getTemplate = function (VisaStatuses) {
        if (VisaStatuses.VisaStatusID === $scope.tempVisaStatuses.VisaStatusID) return 'editVisaStatuses';
        else return 'displayVisaStatuses';
    };

    //-------------------- Edit VisaStatus ----------
    $scope.editVisaStatuses = function (VisaStatus) {
        $scope.tempVisaStatuses = angular.copy(VisaStatus);
        $scope.disableAdd = true;
    };

    //------------------- Add VisaStatus ---------------------
    $scope.addVisaStatuses = function (VisaStatus) {
        $scope.disableEdit = true;
        $scope.disableAdd = true;
        var Month = new Date().getMonth() + 1;
        var _month = Month < 10 ? '0' + Month : Month;
        var _date = new Date().getDate() < 10 ? '0' + new Date().getDate() : new Date().getDate();
        var _year = new Date().getFullYear();
        var temp = {
            VisaStatusID: 0,
            Title: "",            
            Status: "Active",
            LastModifiedDate: _month + "-" + _date + "-" + _year
        };
        $scope.VisaStatuses.splice(0, 0, angular.copy(temp));
        $scope.tempVisaStatuses = angular.copy(temp);
    };

    //------------------- Save VisaStatus ---------------------
    $scope.saveVisaStatuses = function (idx) {

        var addData = {
            VisaStatusID: 0,
            Title: $scope.tempVisaStatuses.Title,
            StatusType: 1,
        };

        var isExist = true;

        for (var i = 0; i < $scope.VisaStatuses.length; i++) {

            if (addData.Title && $scope.VisaStatuses[i].Title.replace(" ", "").toLowerCase() == addData.Title.replace(" ", "").toLowerCase()) {

                isExist = false;
                $scope.existErr = "The Title is Exist";
                break;
            }
           
        }
        if (!addData.Title) {$scope.error = "Please Enter the Title"; }

        
        if (addData.Title && isExist) {
            $http.post(rootDir + '/MasterDataNew/AddVisaStatus', addData).
                success(function (data, status, headers, config) {
                    try {
                        //----------- success message -----------
                        if (data.status == "true") {
                            messageAlertEngine.callAlertMessage("VisaStatus", "New Visa Status Details Added Successfully !!!!", "success", true);
                            data.visaStatusDetails.LastModifiedDate = $scope.ConvertDateFormat(data.visaStatusDetails.LastModifiedDate);
                            $scope.VisaStatuses[idx] = angular.copy(data.visaStatusDetails);
                            $scope.reset();
                            $scope.error = "";
                            $scope.existErr = "";
                        }
                        else {
                            messageAlertEngine.callAlertMessage("VisaStatusError", "Sorry Unable To Add Visa Status !!!!", "danger", true);
                            $scope.VisaStatuses.slpice(idx, 1);
                        }
                    } catch (e) {
                       
                    }
                }).
                error(function (data, status, headers, config) {
                    //----------- error message -----------
                    messageAlertEngine.callAlertMessage("VisaStatusError", "Sorry Unable To Add Visa Status !!!!", "danger", true);
                    $scope.VisaStatuses.slpice(idx, 1);
                });
        }

        
    };

    //------------------- Update VisaStatus ---------------------
    $scope.updateVisaStatuses = function (idx) {

        var updateData = {
            VisaStatusID: $scope.tempVisaStatuses.VisaStatusID,
            Title: $scope.tempVisaStatuses.Title,
            StatusType: 1,
        };

        var isExist = true;

        for (var i = 0; i < $scope.VisaStatuses.length; i++) {

            if (updateData.Title && $scope.VisaStatuses[i].VisaStatusID != updateData.VisaStatusID && $scope.VisaStatuses[i].Title.replace(" ", "").toLowerCase() == updateData.Title.replace(" ", "").toLowerCase()) {

                isExist = false;
                $scope.existErr = "The Title is Exist";
                break;
            }
        }
        if (!updateData.Title) { $scope.error = "Please Enter the Title"; }


        if (updateData.Title && isExist) {
            $http.post(rootDir + '/MasterDataNew/UpdateVisaStatus', updateData).
                success(function (data, status, headers, config) {
                    try {
                        //----------- success message -----------
                        if (data.status == "true") {
                            messageAlertEngine.callAlertMessage("VisaStatus", "Visa Status Details Updated Successfully !!!!", "success", true);
                            data.visaStatusDetails.LastModifiedDate = $scope.ConvertDateFormat(data.visaStatusDetails.LastModifiedDate);
                            $scope.VisaStatuses[idx] = angular.copy(data.visaStatusDetails);
                            $scope.reset();
                            $scope.error = "";
                            $scope.existErr = "";
                        }

                    } catch (e) {
                       
                    }
                }).
                error(function (data, status, headers, config) {
                    //----------- error message -----------
                    messageAlertEngine.callAlertMessage("VisaStatusError", "Sorry Unable To Update Visa Status !!!!", "danger", true);                    
                });
        }


    };

    //----------------- VisaStatus new add cancel ---------------
    $scope.cancelAdd = function () {
        $scope.VisaStatuses.splice(0, 1);
        $scope.tempVisaStatuses = {};
        $scope.disableEdit = false;
        $scope.disableAdd = false;
        $scope.error = "";
        $scope.existErr = "";
    };

    //-------------------- Reset VisaStatus ----------------------
    $scope.reset = function () {
        $scope.tempVisaStatuses = {};
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
        if ($scope.VisaStatuses) {
            for (startIndex; startIndex <= endIndex ; startIndex++) {
                if ($scope.VisaStatuses[startIndex]) {
                    $scope.CurrentPage.push($scope.VisaStatuses[startIndex]);
                } else {
                    break;
                }
            }
        }
    });
    //-------------- License Scope Watch ---------------------
    $scope.$watchCollection('VisaStatuses', function (newValue, oldValue) {
        if (newValue) {
            $scope.bigTotalItems = newValue.length;

            $scope.CurrentPage = [];
            $scope.bigCurrentPage = 1;

            var startIndex = ($scope.bigCurrentPage - 1) * 10;
            var endIndex = startIndex + 9;

            for (startIndex; startIndex <= endIndex ; startIndex++) {
                if ($scope.VisaStatuses[startIndex]) {
                    $scope.CurrentPage.push($scope.VisaStatuses[startIndex]);
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
