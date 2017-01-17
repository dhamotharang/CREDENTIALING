//--------------------- Angular Module ----------------------
var masterDataProviderLevels = angular.module("masterDataProviderLevels", ['ui.bootstrap']);

//Service for getting master data
masterDataProviderLevels.service('masterDataService', ['$http', '$q', function ($http, $q) {

    this.getMasterData = function (URL) {
        return $http.get(rootDir + URL).then(function (value) { return value.data; });
    };
    
}]);

masterDataProviderLevels.service('messageAlertEngine', ['$rootScope', '$timeout', function ($rootScope, $timeout) {

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
masterDataProviderLevels.controller('masterDataProviderLevelsController', ['$scope', '$http', '$filter', '$rootScope', 'masterDataService', 'messageAlertEngine', function ($scope, $http, $filter, $rootScope, masterDataService, messageAlertEngine) {

    $scope.ProviderLevels = [];

    $http.get(rootDir + "/MasterDataNew/GetAllProviderLevels").then(function (value) {
        //console.log("ProviderLevels");
        //console.log(value);

        for (var i = 0; i < value.data.length ; i++) {
            if (value.data[i] != null) {
                value.data[i].LastModifiedDate = $scope.ConvertDateFormat(value.data[i].LastModifiedDate);
            }
        }

        $scope.ProviderLevels = value.data;
        //console.log($scope.ProviderLevels);
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

    

    $scope.tempProviderLevels = {};

    //------------ gets the template to ng-include for a table row / item -------------------
    $scope.getTemplate = function (providerLevel) {
        if (providerLevel.ProviderLevelID === $scope.tempProviderLevels.ProviderLevelID) return 'editProviderLevels';
        else return 'displayProviderLevels';
    };

    //-------------------- Edit ProviderLevel ----------
    $scope.editProviderLevels = function (providerLevel) {
        $scope.tempProviderLevels = angular.copy(providerLevel);
        $scope.disableAdd = true;
    };

    //------------------- Add ProviderLevel ---------------------
    $scope.addProviderLevels = function (providerLevel) {
        $scope.disableEdit = true;
        $scope.disableAdd = true;
        var temp = {
            ProviderLevelID: 0,
            Name: "",            
            Status: "Active",
            LastModifiedDate: new Date()
        };
        $scope.ProviderLevels.splice(0, 0, angular.copy(temp));
        $scope.tempProviderLevels = angular.copy(temp);
    };

    //------------------- Save ProviderLevel ---------------------
    $scope.saveProviderLevel = function (idx) {
       
        var addData = {
            ProviderLevelID: 0,
            Name: $scope.tempProviderLevels.Name,
            StatusType: 1,
        };

        var isExist = true;

        for (var i = 0; i < $scope.ProviderLevels.length; i++) {

            if (addData.Name && $scope.ProviderLevels[i].Name.replace(" ", "").toLowerCase() == addData.Name.replace(" ", "").toLowerCase()) {

                isExist = false;
                $scope.existErr = "The Name is Exist";
                break;
            }
        }
        if (!addData.Name) { $scope.error = "Please Enter the Name"; }

        //console.log("Saving ProviderLevels");
        
        if (addData.Name && isExist) {
            $http.post(rootDir + '/MasterDataNew/AddProviderLevels', addData).
                success(function (data, status, headers, config) {
                    //----------- success message -----------
                    if (data.status == "true") {
                        messageAlertEngine.callAlertMessage("ProviderLevel", "New Provider Level Details Added Successfully !!!!", "success", true);
                        data.providerLevelDetails.LastModifiedDate = $scope.ConvertDateFormat(data.providerLevelDetails.LastModifiedDate);
                        $scope.ProviderLevels[idx] = angular.copy(data.providerLevelDetails);                        
                        $scope.reset();
                        $scope.error = "";
                        $scope.existErr = "";
                    }
                    else {
                        messageAlertEngine.callAlertMessage("ProviderLevelError", "Sorry Unable To Add Provider Level !!!!", "danger", true);
                        $scope.ProviderLevels.slpice(idx, 1);
                    }
                }).
                error(function (data, status, headers, config) {
                    //----------- error message -----------
                    messageAlertEngine.callAlertMessage("ProviderLevelError", "Sorry Unable To Add Provider Level !!!!", "danger", true);
                    $scope.ProviderLevels.slpice(idx, 1);
                });
        }

        
    };

    //------------------- Update ProviderLevel ---------------------
    $scope.updateProviderLevel = function (idx) {

        var updateData = {
            ProviderLevelID: $scope.tempProviderLevels.ProviderLevelID,
            Name: $scope.tempProviderLevels.Name,
            StatusType: 1,
        };

        var isExist = true;

        for (var i = 0; i < $scope.ProviderLevels.length; i++) {

            if (updateData.Name && $scope.ProviderLevels[i].ProviderLevelID != updateData.ProviderLevelID && $scope.ProviderLevels[i].Name.replace(" ", "").toLowerCase() == updateData.Name.replace(" ", "").toLowerCase()) {

                isExist = false;
                $scope.existErr = "The Name is Exist";
                break;
            }
        }
        if (!updateData.Name) { $scope.error = "Please Enter the Name"; }

        //console.log("Updating ProviderLevels");

        if (updateData.Name && isExist) {
            $http.post(rootDir + '/MasterDataNew/UpdateProviderLevels', updateData).
                success(function (data, status, headers, config) {
                    //----------- success message -----------
                    if (data.status == "true") {
                        messageAlertEngine.callAlertMessage("ProviderLevel", "Provider Level Details Updated Successfully !!!!", "success", true);
                        data.providerLevelDetails.LastModifiedDate = $scope.ConvertDateFormat(data.providerLevelDetails.LastModifiedDate);
                        $scope.ProviderLevels[idx] = angular.copy(data.providerLevelDetails);                        
                        $scope.reset();
                        $scope.error = "";
                        $scope.existErr = "";
                    }
                }).
                error(function (data, status, headers, config) {
                    //----------- error message -----------
                    messageAlertEngine.callAlertMessage("ProviderLevelError", "Sorry Unable To Update Provider Level !!!!", "danger", true);                    
                });
        }


    };

    //----------------- ProviderLevel new add cancel ---------------
    $scope.cancelAdd = function () {
        $scope.ProviderLevels.splice(0, 1);
        $scope.tempProviderLevels = {};
        $scope.disableEdit = false;
        $scope.disableAdd = false;
        $scope.error = "";
        $scope.existErr = "";
    };

    //-------------------- Reset ProviderLevel ----------------------
    $scope.reset = function () {
        $scope.tempProviderLevels = {};
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
        if ($scope.ProviderLevels) {
            for (startIndex; startIndex <= endIndex ; startIndex++) {
                if ($scope.ProviderLevels[startIndex]) {
                    $scope.CurrentPage.push($scope.ProviderLevels[startIndex]);
                } else {
                    break;
                }
            }
        }
        ////console.log($scope.CurrentPageProviders);
    });
    //-------------- License Scope Watch ---------------------
    $scope.$watchCollection('ProviderLevels', function (newValue, oldValue) {
        if (newValue) {
            $scope.bigTotalItems = newValue.length;

            $scope.CurrentPage = [];
            $scope.bigCurrentPage = 1;

            var startIndex = ($scope.bigCurrentPage - 1) * 10;
            var endIndex = startIndex + 9;

            for (startIndex; startIndex <= endIndex ; startIndex++) {
                if ($scope.ProviderLevels[startIndex]) {
                    $scope.CurrentPage.push($scope.ProviderLevels[startIndex]);
                } else {
                    break;
                }
            }
            ////console.log($scope.CurrentPageProviders);
        }
    });
    //------------------- end ------------------

}]);
