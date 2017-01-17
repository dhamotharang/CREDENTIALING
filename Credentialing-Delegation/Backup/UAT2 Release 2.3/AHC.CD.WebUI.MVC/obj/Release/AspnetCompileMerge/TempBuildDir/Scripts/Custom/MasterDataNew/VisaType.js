//--------------------- Angular Module ----------------------
var masterDataVisaTypes = angular.module("masterDataVisaTypes", ['ui.bootstrap']);

//Service for getting master data
masterDataVisaTypes.service('masterDataService', ['$http', '$q', function ($http, $q) {

    this.getMasterData = function (URL) {
        return $http.get(URL).then(function (value) { return value.data; });
    };

}]);

masterDataVisaTypes.service('messageAlertEngine', ['$rootScope', '$timeout', function ($rootScope, $timeout) {

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
masterDataVisaTypes.controller('masterDataVisaTypesController', ['$scope', '$http', '$filter', '$rootScope', 'masterDataService', 'messageAlertEngine', function ($scope, $http, $filter, $rootScope, masterDataService, messageAlertEngine) {

    $scope.VisaTypes = [];

    $http.get(rootDir + "/MasterDataNew/GetAllVisaTypes").then(function (value) {
        //console.log("VisaTypes");
        //console.log(value);

        for (var i = 0; i < value.data.length ; i++) {
            if (value.data[i] != null) {
                value.data[i].LastModifiedDate = $scope.ConvertDateFormat(value.data[i].LastModifiedDate);
            }
        }

        $scope.VisaTypes = value.data;
        //console.log($scope.VisaTypes);
    });

    //Convert the date from database to normal
    $scope.ConvertDateFormat = function (value) {
        //////console.log(value);
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

    
    $scope.tempVisaTypes = {};

    //------------ gets the template to ng-include for a table row / item -------------------
    $scope.getTemplate = function (visaTypes) {
        if (visaTypes.VisaTypeID === $scope.tempVisaTypes.VisaTypeID) return 'editVisaTypes';
        else return 'displayVisaTypes';
    };

    //-------------------- Edit VisaType ----------
    $scope.editVisaTypes = function (visaType) {
        $scope.tempVisaTypes = angular.copy(visaType);
        $scope.disableAdd = true;
    };

    //------------------- Add VisaType ---------------------
    $scope.addVisaTypes = function (visaType) {
        $scope.disableEdit = true;
        $scope.disableAdd = true;
        var temp = {
            VisaTypeID: 0,
            Title: "",
            Status: "Active",
            LastModifiedDate: new Date()
        };
        $scope.VisaTypes.splice(0, 0, angular.copy(temp));
        $scope.tempVisaTypes = angular.copy(temp);
    };

    //------------------- Save VisaType ---------------------
    $scope.saveVisaTypes = function (idx) {

        var addData = {
            VisaTypeID: 0,
            Title: $scope.tempVisaTypes.Title,
            StatusType: 1,
        };

        var isExist = true;

        for (var i = 0; i < $scope.VisaTypes.length; i++) {

            if (addData.Title && $scope.VisaTypes[i].Title.replace(" ", "").toLowerCase() == addData.Title.replace(" ", "").toLowerCase()) {

                isExist = false;
                $scope.existErr = "The Title is Exist";
                break;
            }
        }
        if (!addData.Title) { $scope.error = "Please Enter the Title"; }

        //console.log("Saving VisaType");
        
        if (addData.Title && isExist) {
            $http.post(rootDir + '/MasterDataNew/AddVisaType', addData).
                success(function (data, status, headers, config) {
                    //----------- success message -----------
                    if (data.status == "true") {
                        messageAlertEngine.callAlertMessage("VisaType", "New Visa Type Details Added Successfully !!!!", "success", true);
                        data.visaTypeDetails.LastModifiedDate = $scope.ConvertDateFormat(data.visaTypeDetails.LastModifiedDate);
                        $scope.VisaTypes[idx] = angular.copy(data.visaTypeDetails);                        
                        $scope.reset();
                        $scope.error = "";
                        $scope.existErr = "";
                    }
                    else {
                        messageAlertEngine.callAlertMessage("VisaTypeError", "Sorry Unable To Add Visa Type !!!!", "danger", true);
                        $scope.VisaTypes.slpice(idx, 1);
                    }
                }).
                error(function (data, status, headers, config) {
                    //----------- error message -----------
                    messageAlertEngine.callAlertMessage("VisaTypeError", "Sorry Unable To Add Visa Type !!!!", "danger", true);
                    $scope.VisaTypes.slpice(idx, 1);
                });
        }

        
    };

    //------------------- Update VisaType ---------------------
    $scope.updateVisaTypes = function (idx) {

        var updateData = {
            VisaTypeID: $scope.tempVisaTypes.VisaTypeID,
            Title: $scope.tempVisaTypes.Title,
            StatusType: 1,
        };

        var isExist = true;

        for (var i = 0; i < $scope.VisaTypes.length; i++) {

            if (updateData.Title && $scope.VisaTypes[i].VisaTypeID != updateData.VisaTypeID && $scope.VisaTypes[i].Title.replace(" ", "").toLowerCase() == updateData.Title.replace(" ", "").toLowerCase()) {

                isExist = false;
                $scope.existErr = "The Title is Exist";
                break;
            }
        }
        if (!updateData.Title) { $scope.error = "Please Enter the Title"; }

        //console.log("Updating VisaType");

        if (updateData.Title && isExist) {
            $http.post(rootDir + '/MasterDataNew/UpdateVisaType', updateData).
                success(function (data, status, headers, config) {
                    //----------- success message -----------
                    if (data.status == "true") {
                        messageAlertEngine.callAlertMessage("VisaType", "Visa Type Details Updated Successfully !!!!", "success", true);
                        data.visaTypeDetails.LastModifiedDate = $scope.ConvertDateFormat(data.visaTypeDetails.LastModifiedDate);
                        $scope.VisaTypes[idx] = angular.copy(data.visaTypeDetails);                        
                        $scope.reset();
                        $scope.error = "";
                        $scope.existErr = "";
                    }
                    
                }).
                error(function (data, status, headers, config) {
                    //----------- error message -----------
                    messageAlertEngine.callAlertMessage("VisaTypeError", "Sorry Unable To Update Visa Type !!!!", "danger", true);                    
                });
        }


    };

    //----------------- VisaType new add cancel ---------------
    $scope.cancelAdd = function () {
        $scope.VisaTypes.splice(0, 1);
        $scope.tempVisaTypes = {};
        $scope.disableEdit = false;
        $scope.disableAdd = false;
        $scope.error = "";
        $scope.existErr = "";
    };

    //-------------------- Reset VisaType ----------------------
    $scope.reset = function () {
        $scope.tempVisaTypes = {};
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
        if ($scope.VisaTypes) {
            for (startIndex; startIndex <= endIndex ; startIndex++) {
                if ($scope.VisaTypes[startIndex]) {
                    $scope.CurrentPage.push($scope.VisaTypes[startIndex]);
                } else {
                    break;
                }
            }
        }
        ////console.log($scope.CurrentPageProviders);
    });
    //-------------- License Scope Watch ---------------------
    $scope.$watchCollection('VisaTypes', function (newValue, oldValue) {
        if (newValue) {
            $scope.bigTotalItems = newValue.length;

            $scope.CurrentPage = [];
            $scope.bigCurrentPage = 1;

            var startIndex = ($scope.bigCurrentPage - 1) * 10;
            var endIndex = startIndex + 9;

            for (startIndex; startIndex <= endIndex ; startIndex++) {
                if ($scope.VisaTypes[startIndex]) {
                    $scope.CurrentPage.push($scope.VisaTypes[startIndex]);
                } else {
                    break;
                }
            }
            ////console.log($scope.CurrentPageProviders);
        }
    });
    //------------------- end ------------------

}]);
