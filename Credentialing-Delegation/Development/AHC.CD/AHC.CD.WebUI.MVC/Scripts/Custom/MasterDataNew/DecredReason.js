
//--------------------- Angular Module ----------------------
var MasterDataDecredentialingReasons = angular.module("MasterDataDecredentialingReasons", ['ui.bootstrap']);

//Service for getting master data
MasterDataDecredentialingReasons.service('masterDataService', ['$http', '$q', function ($http, $q) {

    this.getMasterData = function (URL) {
        return $http.get(URL).then(function (value) { return value.data; });
    };

}]);

MasterDataDecredentialingReasons.service('messageAlertEngine', ['$rootScope', '$timeout', function ($rootScope, $timeout) {

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
}]);


//=========================== Controller declaration ==========================
MasterDataDecredentialingReasons.controller('DecredentialingReasonscontroller', ['$scope', '$http', '$filter', '$rootScope', 'masterDataService', 'messageAlertEngine', function ($scope, $http, $filter, $rootScope, masterDataService, messageAlertEngine) {

    $scope.DecredentialingReasons = [];

    $http.get(rootDir + "/MasterDataNew/GetAllDecredentialingReasons").success(function (value) {

        try {
            for (var i = 0; i < value.length ; i++) {
                if (value[i] != null) {
                    value[i].LastModifiedDate = ($scope.ConvertDateFormat(value[i].LastModifiedDate)).toString();
                }

            }

            $scope.DecredentialingReasons = angular.copy(value);
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


    $scope.tempDecredentialingReasons = {};
    $scope.DecredentialingReasons = [];

    //------------ gets the template to ng-include for a table row / item -------------------
    $scope.getTemplate = function (DecredentialingReasons) {
        if (DecredentialingReasons.DecredentialingReasonId === $scope.tempDecredentialingReasons.DecredentialingReasonId)
            return 'editDecredentialingReason';
        else
            return 'displayDecredentialingReasons';
    };

    //-------------------- Edit Insuarance Company names ----------
    $scope.editDecredentialingReasons = function (Reason) {
        $scope.editDecredentialingReason = true;
        $scope.tempDecredentialingReasons = angular.copy(Reason);
        //$scope.disableAdd = true;
    };

    //------------------- Add Decredentialing Reason ---------------------
    $scope.addDecredentialingReason = function (Reason) {
        $scope.disableEdit = true;
        $scope.disableAdd = true;
        var Month = new Date().getMonth() + 1;
        var _month = Month < 10 ? '0' + Month : Month;
        var _date = new Date().getDate() < 10 ? '0' + new Date().getDate() : new Date().getDate();
        var _year = new Date().getFullYear();
        var temp = {
            addDecredentialingReasons: 0,
            Reason: "",
            Status: "Active",
            LastModifiedDate: _month + "-" + _date + "-" + _year,
        };
        $scope.DecredentialingReasons.splice(0, 0, angular.copy(temp));
        $scope.tempDecredentialingReasons = angular.copy(temp);
    };

    //------------------- Save Decredentialing Reason ---------------------
    $scope.saveDecredentialingReason = function (idx) {

        var addData = {
            DecredentialingReasonId: $scope.tempDecredentialingReasons.DecredentialingReasonId,
            Reason: $scope.tempDecredentialingReasons.Reason,
            StatusType: 1,
        };



        var isExist = true;

        for (var i = 0; i < $scope.DecredentialingReasons.length; i++) {

            if (addData.Reason && $scope.DecredentialingReasons[i].Reason.toLowerCase().trim() == addData.Reason.toLowerCase().trim()) {

                isExist = false;
                $scope.existErr = "This Reason already exists";
                break;
            }
        }

        if (!addData.Reason) { $scope.error = "Please Enter the Reason"; }


        if (addData.Reason && isExist) {
            $http.post(rootDir + '/MasterDataNew/AddDecredentialingReason', addData).
                success(function (data, status, headers, config) {
                    try {
                        //----------- success message -----------
                        if (data.status == "true") {
                            messageAlertEngine.callAlertMessage("decredreason", "Decredentialing Reason Added Successfully !!!!", "success", true);
                            data.decredentialReason.LastModifiedDate = $scope.ConvertDateFormat(data.decredentialReason.LastModifiedDate);
                            $scope.DecredentialingReasons[idx] = angular.copy(data.decredentialReason);
                            $scope.reset();
                            $scope.error = "";
                            $scope.existErr = "";
                        }
                        else {
                            messageAlertEngine.callAlertMessage("decredreasonerror", "Sorry Unable To Add new Decredentialing Reason !!!!", "danger", true);
                            $scope.DecredentialingReasons.slpice(idx, 1);
                        }
                    } catch (e) {

                    }
                }).
                error(function (data, status, headers, config) {
                    //----------- error message -----------
                    messageAlertEngine.callAlertMessage("decredreasonerror", "Sorry Unable To Add new Decredentialing Reason !!!!", "danger", true);
                    $scope.DecredentialingReasons.slpice(idx, 1);
                });
        }


    };

    //------------------- Update Decredentialing Reason ---------------------
    $scope.updateDecredentialingReason = function (idx) {

        var updateData = {
            DecredentialingReasonId: $scope.tempDecredentialingReasons.DecredentialingReasonId,
            Reason: $scope.tempDecredentialingReasons.Reason,
            StatusType: 1,
        };

        var isExist = true;

        for (var i = 0; i < $scope.DecredentialingReasons.length; i++) {

            if (updateData.Reason && $scope.DecredentialingReasons[i].DecredentialingReasonId != updateData.DecredentialingReasonId && $scope.DecredentialingReasons[i].Reason.replace(" ", "").toLowerCase() == updateData.Reason.replace(" ", "").toLowerCase()) {

                isExist = false;
                $scope.existErr = "This Decredentialing Reason already exists";
                break;
            }
            if ($scope.DecredentialingReasons[i].DecredentialingReasonId == updateData.DecredentialingReasonId) {
                idx = i;
            }
        }
        if (!updateData.Reason) { $scope.error = "Please Enter the Decredentialing Reason"; }



        if (updateData.Reason && isExist) {
            $http.post(rootDir + '/MasterDataNew/UpdateDecredentialingReasons', updateData).
                success(function (data, status, headers, config) {
                    try {
                        //----------- success message -----------
                        if (data.status == "true") {
                            messageAlertEngine.callAlertMessage("decredreason", "Decredentialing Reason Details Updated Successfully !!!!", "success", true);
                            data.decredentialingreason.LastModifiedDate = $scope.ConvertDateFormat(data.decredentialingreason.LastModifiedDate);
                            $scope.DecredentialingReasons[idx] = angular.copy(data.decredentialingreason);
                            $scope.reset();
                            $scope.error = "";
                            $scope.existErr = "";
                        }
                    } catch (e) {

                    }

                }).
                error(function (data, status, headers, config) {
                    //----------- error message -----------
                    messageAlertEngine.callAlertMessage("decrederror", "Sorry Unable To Update Decredentialing Reason Details !!!!", "danger", true);
                });
        }


    };

    //---------------------Delete Decredentialing Reason---------------------
    $scope.MasterDecredentialingReasonId = 0;
    $scope.Remove = function (DecredentialingReasonId) {
        $scope.MasterDecredentialingReasonId = DecredentialingReasonId;
        $('#reasonWarningModal').modal();
    }

   
    $scope.removeDecredReason = function () {
        $scope.savingStatus = true;
        $http.post(rootDir + '/MasterDataNew/InactivateDecredentialingReason/?decredentialingReasonID=' + $scope.MasterDecredentialingReasonId).
                success(function (data, status, headers, config) {
                    try {
                        //----------- success message -----------
                        if (data == "true") {
                            $scope.savingStatus = false;                            
                            
                            messageAlertEngine.callAlertMessage("decredreason", "Reason Deleted Successfully !!!!", "success", true);
                            for (var i in $scope.DecredentialingReasons) {
                                if ($scope.DecredentialingReasons[i].DecredentialingReasonId == $scope.MasterDecredentialingReasonId) {
                                    $scope.DecredentialingReasons.splice(i, 1);
                                    break;
                                }
                            }
                            $scope.reset();
                            $scope.error = "";
                            $scope.existErr = "";
                        }
                    } catch (e) {

                    }

                }).
                error(function (data, status, headers, config) {
                    //----------- error message -----------
                    $scope.savingStatus = false;
                    
                    messageAlertEngine.callAlertMessage("decredreasonerror", "Sorry Unable Delete Decredentialing Reasons  !!!!", "danger", true);
                });
    }

    //----------------- Decredentialing Reason new add cancel ---------------
    $scope.cancelAdd = function () {
        $scope.DecredentialingReasons.splice(0, 1);
        $scope.tempDecredentialingReasons = {};
        $scope.disableEdit = false;
        $scope.disableAdd = false;
        $scope.error = "";
        $scope.existErr = "";
    };

    //-------------------- Reset Decredentialing Reason ----------------------
    $scope.reset = function () {
        $scope.tempDecredentialingReasons = {};
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
        if ($scope.DecredentialingReasons) {
            for (startIndex; startIndex <= endIndex ; startIndex++) {
                if ($scope.DecredentialingReasons[startIndex]) {
                    $scope.CurrentPage.push($scope.DecredentialingReasons[startIndex]);
                } else {
                    break;
                }
            }
        }
    });
    //-------------- License Scope Watch ---------------------
    $scope.$watchCollection('DecredentialingReasons', function (newValue, oldValue) {
        if (newValue) {
            $scope.bigTotalItems = newValue.length;

            $scope.CurrentPage = [];
            $scope.bigCurrentPage = 1;

            var startIndex = ($scope.bigCurrentPage - 1) * 10;
            var endIndex = startIndex + 9;

            for (startIndex; startIndex <= endIndex ; startIndex++) {
                if ($scope.DecredentialingReasons[startIndex]) {
                    $scope.CurrentPage.push($scope.DecredentialingReasons[startIndex]);
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
