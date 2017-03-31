﻿//--------------------- Angular Module ----------------------
var masterDataSpecialtyBoards = angular.module("masterDataSpecialtyBoards", ['ui.bootstrap']);

//Service for getting master data
masterDataSpecialtyBoards.service('masterDataService', ['$http', '$q', function ($http, $q) {

    this.getMasterData = function (URL) {
        return $http.get(URL).then(function (value) { return value.data; });
    };

}]);

masterDataSpecialtyBoards.service('messageAlertEngine', ['$rootScope', '$timeout', function ($rootScope, $timeout) {

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
masterDataSpecialtyBoards.controller('masterDataSpecialtyBoardsController', ['$scope', '$http', '$filter', '$rootScope', 'masterDataService', 'messageAlertEngine', function ($scope, $http, $filter, $rootScope, masterDataService, messageAlertEngine) {

    $scope.SpecialtyBoards = [];

    $http.get(rootDir + "/MasterDataNew/GetAllspecialtyBoards").success(function (value) {
        try {

            for (var i = 0; i < value.length ; i++) {
                if (value[i] != null) {
                    value[i].LastModifiedDate = ($scope.ConvertDateFormat(value[i].LastModifiedDate)).toString();
                }

            }

            $scope.SpecialtyBoards = angular.copy(value);
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

    

    $scope.tempSpecialtyBoards = {};

    //------------ gets the template to ng-include for a table row / item -------------------
    $scope.getTemplate = function (SpecialtyBoards) {
        if (SpecialtyBoards.SpecialtyBoardID === $scope.tempSpecialtyBoards.SpecialtyBoardID) return 'editSpecialtyBoards';
        else return 'displaySpecialtyBoards';
    };

    //-------------------- Edit SpecialtyBoard ----------
    $scope.editSpecialtyBoards = function (SpecialtyBoard) {
        $scope.tempSpecialtyBoards = angular.copy(SpecialtyBoard);
        $scope.disableAdd = true;
    };

    //------------------- Add SpecialtyBoard ---------------------
    $scope.addSpecialtyBoards = function (SpecialtyBoard) {
        $scope.disableEdit = true;
        $scope.disableAdd = true;
        var Month = new Date().getMonth() + 1;
        var _month = Month < 10 ? '0' + Month : Month;
        var _date = new Date().getDate() < 10 ? '0' + new Date().getDate() : new Date().getDate();
        var _year = new Date().getFullYear();
        var temp = {
            SpecialtyBoardID: 0,
            Name: "",
            Status: "Active",
            LastModifiedDate: _month + "-" + _date + "-" + _year
        };
        $scope.SpecialtyBoards.splice(0, 0, angular.copy(temp));
        $scope.tempSpecialtyBoards = angular.copy(temp);
    };

    //------------------- Save SpecialtyBoard ---------------------
    $scope.saveSpecialtyBoards = function (idx) {

        var addData = {
            SpecialtyBoardID: 0,
            Name: $scope.tempSpecialtyBoards.Name,
            StatusType: 1,
        };

        var isExist = true;

        for (var i = 0; i < $scope.SpecialtyBoards.length; i++) {

            if (addData.Name && $scope.SpecialtyBoards[i].Name.replace(" ", "").toLowerCase() == addData.Name.replace(" ", "").toLowerCase()) {

                isExist = false;
                $scope.existErr = "The Name is Exist";
                break;
            }
        }
        if (!addData.Name) { $scope.error = "Please Enter the Name"; }

        
        if (addData.Name && isExist) {
            $http.post(rootDir + '/MasterDataNew/AddSpecialtyBoard', addData).
                success(function (data, status, headers, config) {
                    try {
                        //----------- success message -----------
                        if (data.status == "true") {
                            messageAlertEngine.callAlertMessage("SpecialtyBoard", "New Specialty Board Details Added Successfully !!!!", "success", true);
                            data.specialtyBoardDetails.LastModifiedDate = $scope.ConvertDateFormat(data.specialtyBoardDetails.LastModifiedDate);
                            $scope.SpecialtyBoards[idx] = angular.copy(data.specialtyBoardDetails);
                            $scope.reset();
                            $scope.error = "";
                            $scope.existErr = "";
                        }
                        else {
                            messageAlertEngine.callAlertMessage("SpecialtyBoardError", "Sorry Unable To Add Specialty Board !!!!", "danger", true);
                            $scope.SpecialtyBoards.slpice(idx, 1);
                        }
                    } catch (e) {
                       
                    }
                }).
                error(function (data, status, headers, config) {
                    //----------- error message -----------
                    messageAlertEngine.callAlertMessage("SpecialtyBoardError", "Sorry Unable To Add Specialty Board !!!!", "danger", true);
                    $scope.SpecialtyBoards.slpice(idx, 1);
                });
        }

        
    };

    //------------------- Update SpecialtyBoard ---------------------
    $scope.updateSpecialtyBoards = function (idx) {

        var updateData = {
            SpecialtyBoardID: $scope.tempSpecialtyBoards.SpecialtyBoardID,
            Name: $scope.tempSpecialtyBoards.Name,
            StatusType: 1,
        };

        var isExist = true;

        for (var i = 0; i < $scope.SpecialtyBoards.length; i++) {

            if (updateData.Name && $scope.SpecialtyBoards[i].SpecialtyBoardID != updateData.SpecialtyBoardID && $scope.SpecialtyBoards[i].Name.replace(" ", "").toLowerCase() == updateData.Name.replace(" ", "").toLowerCase()) {

                isExist = false;
                $scope.existErr = "The Name is Exist";
                break;
            }
            if ($scope.SpecialtyBoards[i].SpecialtyBoardID == updateData.SpecialtyBoardID) {
                idx = i;
            }
        }
        if (!updateData.Name) { $scope.error = "Please Enter the Name"; }


        if (updateData.Name && isExist) {
            $http.post(rootDir + '/MasterDataNew/UpdateSpecialtyBoard', updateData).
                success(function (data, status, headers, config) {
                    try {
                        //----------- success message -----------
                        if (data.status == "true") {
                            messageAlertEngine.callAlertMessage("SpecialtyBoard", "Specialty Board Details Update Successfully !!!!", "success", true);
                            data.specialtyBoardDetails.LastModifiedDate = $scope.ConvertDateFormat(data.specialtyBoardDetails.LastModifiedDate);
                            $scope.SpecialtyBoards[idx] = angular.copy(data.specialtyBoardDetails);
                            $scope.reset();
                            $scope.error = "";
                            $scope.existErr = "";
                        }
                    } catch (e) {
                      
                    }
                    
                }).
                error(function (data, status, headers, config) {
                    //----------- error message -----------
                    messageAlertEngine.callAlertMessage("SpecialtyBoardError", "Sorry Unable To Update Specialty Board !!!!", "danger", true);
                   
                });
        }


    };

    //----------------- SpecialtyBoard new add cancel ---------------
    $scope.cancelAdd = function () {
        $scope.SpecialtyBoards.splice(0, 1);
        $scope.tempSpecialtyBoards = {};
        $scope.disableEdit = false;
        $scope.disableAdd = false;
        $scope.error = "";
        $scope.existErr = "";
    };

    //-------------------- Reset SpecialtyBoard ----------------------
    $scope.reset = function () {
        $scope.tempSpecialtyBoards = {};
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
        if ($scope.SpecialtyBoards) {
            for (startIndex; startIndex <= endIndex ; startIndex++) {
                if ($scope.SpecialtyBoards[startIndex]) {
                    $scope.CurrentPage.push($scope.SpecialtyBoards[startIndex]);
                } else {
                    break;
                }
            }
        }
    });
    //-------------- License Scope Watch ---------------------
    $scope.$watchCollection('SpecialtyBoards', function (newValue, oldValue) {
        if (newValue) {
            $scope.bigTotalItems = newValue.length;

            $scope.CurrentPage = [];
            $scope.bigCurrentPage = 1;

            var startIndex = ($scope.bigCurrentPage - 1) * 10;
            var endIndex = startIndex + 9;

            for (startIndex; startIndex <= endIndex ; startIndex++) {
                if ($scope.SpecialtyBoards[startIndex]) {
                    $scope.CurrentPage.push($scope.SpecialtyBoards[startIndex]);
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