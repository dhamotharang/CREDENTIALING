//--------------------- Angular Module ----------------------
var masterDataDEASchedules = angular.module("masterDataDEASchedules", ['ui.bootstrap']);

masterDataDEASchedules.service('messageAlertEngine', ['$rootScope', '$timeout', function ($rootScope, $timeout) {

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
masterDataDEASchedules.controller('masterDataDEASchedulesController', ['$scope', '$http', '$filter', '$rootScope', 'messageAlertEngine', function ($scope, $http, $filter, $rootScope, messageAlertEngine) {

    $scope.DEASchedules = [];

    $http.get("/MasterDataNew/GetAllDEASchedules").then(function (value) {
        console.log("DEASchedules");
        console.log(value);

        for (var i = 0; i < value.data.length ; i++) {
            if (value.data[i] != null) {
                value.data[i].LastModifiedDate = $scope.ConvertDateFormat(value.data[i].LastModifiedDate);
            }
        }

        $scope.DEASchedules = value.data;
        console.log($scope.DEASchedules);
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

    

    $scope.tempDEASchedules = {};

    //------------ gets the template to ng-include for a table row / item -------------------
    $scope.getTemplate = function (DEASchedules) {
        if (DEASchedules.DEAScheduleID === $scope.tempDEASchedules.DEAScheduleID) return 'editDEASchedules';
        else return 'displayDEASchedules';
    };

    //-------------------- Edit DEASchedule ----------
    $scope.editDEASchedules = function (DEASchedule) {
        $scope.tempDEASchedules = angular.copy(DEASchedule);
        $scope.disableAdd = true;
    };

    //------------------- Add DEASchedule ---------------------
    $scope.addDEASchedules = function (DEASchedule) {
        $scope.disableEdit = true;
        $scope.disableAdd = true;
        var temp = {
            DEAScheduleID: 0,
            ScheduleTitle: "",
            ScheduleTypeTitle:"",
            Status: "Active",
            LastModifiedDate: new Date
        };
        $scope.DEASchedules.splice(0, 0, angular.copy(temp));
        $scope.tempDEASchedules = angular.copy(temp);
    };

    //------------------- Save DEASchedule ---------------------
    $scope.saveDEASchedules = function (idx) {

        var addData = {
            DEAScheduleID: 0,
            ScheduleTitle: $scope.tempDEASchedules.ScheduleTitle,
            ScheduleTypeTitle: $scope.tempDEASchedules.ScheduleTypeTitle,
            StatusType: 1
        }

        var isExist = true;
        

        for (var i = 0; i < $scope.DEASchedules.length; i++) {

            if (addData.ScheduleTitle && $scope.DEASchedules[i].ScheduleTitle.replace(" ", "").toLowerCase() == addData.ScheduleTitle.replace(" ", "").toLowerCase()) {

                isExist = false;
                $scope.existErr = "The ScheduleTitle is Exist";
                break;
            }
            
        }
        if (!addData.ScheduleTitle) { $scope.error = "Please Enter the ScheduleTitle"; }
        if (!addData.ScheduleTypeTitle) { $scope.error1 = "Please Enter the ScheduleTypeTitle"; }

        console.log("Saving DEASchedule");
        if (addData.ScheduleTitle && addData.ScheduleTypeTitle && isExist) {
            $http.post('/MasterDataNew/AddDEASchedule', addData).
                success(function (data, status, headers, config) {
                    //----------- success message -----------
                    if (data.status == "true") {
                        messageAlertEngine.callAlertMessage("DEASchedule", "New DEA Schedule Details Added Successfully !!!!", "success", true);
                        data.dEAScheduleDetails.LastModifiedDate = $scope.ConvertDateFormat(data.dEAScheduleDetails.LastModifiedDate);
                        $scope.DEASchedules[idx] = angular.copy(data.dEAScheduleDetails);                        
                        $scope.reset();
                        $scope.error = "";
                        $scope.existErr = "";
                        $scope.error1 = "";
                        $scope.existErr1 = "";
                    }
                    else {
                        messageAlertEngine.callAlertMessage("DEAScheduleError", "Sorry Unable To Add DEA Schedule !!!!", "danger", true);
                        $scope.DEASchedules.splice(idx, 1);
                    }
                }).
                error(function (data, status, headers, config) {
                    //----------- error message -----------
                    messageAlertEngine.callAlertMessage("DEAScheduleError", "Sorry Unable To Add DEA Schedule !!!!", "danger", true);
                    $scope.DEASchedules.splice(idx, 1);
                });
        }

        
    };

    //------------------- Update DEASchedule ---------------------
    $scope.updateDEASchedules = function (idx) {

        var updateData = {
            DEAScheduleID: $scope.tempDEASchedules.DEAScheduleID,
            ScheduleTitle: $scope.tempDEASchedules.ScheduleTitle,
            ScheduleTypeTitle: $scope.tempDEASchedules.ScheduleTypeTitle,
            StatusType: 1
        }

        var isExist = true;
        var isExist1 = true;

        for (var i = 0; i < $scope.DEASchedules.length; i++) {

            if (updateData.ScheduleTitle && $scope.DEASchedules[i].DEAScheduleID != updateData.DEAScheduleID && $scope.DEASchedules[i].ScheduleTitle.replace(" ", "").toLowerCase() == updateData.ScheduleTitle.replace(" ", "").toLowerCase()) {

                isExist = false;
                $scope.existErr = "The ScheduleTitle is Exist";
                break;
            }
            
        }
        if (!updateData.ScheduleTitle) { $scope.error = "Please Enter the ScheduleTitle"; }
        if (!updateData.ScheduleTypeTitle) { $scope.error1 = "Please Enter the ScheduleTypeTitle"; }

        console.log("Udating DEASchedule");
        if (updateData.ScheduleTitle && updateData.ScheduleTypeTitle && isExist) {
            $http.post('/MasterDataNew/UpdateDEASchedule', updateData).
                success(function (data, status, headers, config) {
                    //----------- success message -----------
                    if (data.status == "true") {
                        messageAlertEngine.callAlertMessage("DEAScheduleError", "DEA Schedule Details Updated Successfully !!!!", "success", true);
                        data.dEAScheduleDetails.LastModifiedDate = $scope.ConvertDateFormat(data.dEAScheduleDetails.LastModifiedDate);
                        $scope.DEASchedules[idx] = angular.copy(data.dEAScheduleDetails);                        
                        $scope.reset();
                        $scope.error = "";
                        $scope.existErr = "";
                        $scope.error1 = "";
                        $scope.existErr1 = "";
                    }
                }).
                error(function (data, status, headers, config) {
                    //----------- error message -----------
                    messageAlertEngine.callAlertMessage("DEAScheduleError", "Sorry Unable To Update DEA Schedule !!!!", "danger", true);
                    
                });
        }


    };

    //----------------- DEASchedule new add cancel ---------------
    $scope.cancelAdd = function () {
        $scope.DEASchedules.splice(0, 1);
        $scope.tempDEASchedules = {};
        $scope.disableEdit = false;
        $scope.disableAdd = false;
        $scope.error = "";
        $scope.existErr = "";
        $scope.error1 = "";
        $scope.existErr1 = "";
    };

    //-------------------- Reset DEASchedule ----------------------
    $scope.reset = function () {
        $scope.tempDEASchedules = {};
        $scope.disableEdit = false;
        $scope.disableAdd = false;
        $scope.error = "";
        $scope.existErr = "";
        $scope.error1 = "";
        $scope.existErr1 = "";
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
        if ($scope.DEASchedules) {
            for (startIndex; startIndex <= endIndex ; startIndex++) {
                if ($scope.DEASchedules[startIndex]) {
                    $scope.CurrentPage.push($scope.DEASchedules[startIndex]);
                } else {
                    break;
                }
            }
        }
        //console.log($scope.CurrentPageProviders);
    });
    //-------------- License Scope Watch ---------------------
    $scope.$watchCollection('DEASchedules', function (newValue, oldValue) {
        if (newValue) {
            $scope.bigTotalItems = newValue.length;

            $scope.CurrentPage = [];
            $scope.bigCurrentPage = 1;

            var startIndex = ($scope.bigCurrentPage - 1) * 10;
            var endIndex = startIndex + 9;

            for (startIndex; startIndex <= endIndex ; startIndex++) {
                if ($scope.DEASchedules[startIndex]) {
                    $scope.CurrentPage.push($scope.DEASchedules[startIndex]);
                } else {
                    break;
                }
            }
            //console.log($scope.CurrentPageProviders);
        }
    });
    //------------------- end ------------------
}]);
