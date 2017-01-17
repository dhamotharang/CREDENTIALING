//--------------------- Angular Module ----------------------
var masterDataMilitaryPresentDuty = angular.module("masterDataMilitaryPresentDuty", ['ui.bootstrap']);

masterDataMilitaryPresentDuty.service('messageAlertEngine', ['$rootScope', '$timeout', function ($rootScope, $timeout) {

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
masterDataMilitaryPresentDuty.controller('masterDataMilitaryPresentDutyController', ['$scope', '$http', '$filter', '$rootScope', 'messageAlertEngine', function ($scope, $http, $filter, $rootScope, messageAlertEngine) {

    $http.get(rootDir + "/MasterDataNew/GetAllMilitaryPresentDuties").then(function (value) {
        //console.log("MilitaryPresentDuties");
        //console.log(value);

        for (var i = 0; i < value.data.length ; i++) {
            if (value.data[i] != null) {
                value.data[i].LastModifiedDate = $scope.ConvertDateFormat(value.data[i].LastModifiedDate);
            }
        }

        $scope.MilitaryPresentDuties = value.data;
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



    $scope.tempMPD = {};

    //------------ gets the template to ng-include for a table row / item -------------------
    $scope.getTemplate = function (militaryDischarge) {
        if (militaryDischarge.MilitaryPresentDutyID === $scope.tempMPD.MilitaryPresentDutyID) return 'editMilitaryPresentDuty';
        else return 'displayMilitaryPresentDuty';
    };

    //-------------------- Edit AdmittingPrivilege ----------
    $scope.editMilitaryPresentDuty = function (militaryDischarge) {
        $scope.tempMPD = angular.copy(militaryDischarge);
        $scope.disableAdd = true;
    };

    //------------------- Add AdmittingPrivilege ---------------------
    $scope.addMilitaryPresentDuty = function (militaryDischarge) {
        $scope.disableEdit = true;
        $scope.disableAdd = true;
        var temp = {
            MilitaryPresentDutyID: 0,
            Title: "",
            Status: "Active",
            LastModifiedDate: new Date()
        };
        $scope.MilitaryPresentDuties.splice(0, 0, angular.copy(temp));
        $scope.tempMPD = angular.copy(temp);
    };

    //------------------- Save AdmittingPrivilege ---------------------
    $scope.saveMilitaryPresentDuty = function (idx) {
        //$scope.tempMPD.LastModifiedDate = "02/03/2015"
        var addData = {
            MilitaryPresentDutyID: 0,
            Title: $scope.tempMPD.Title,
            StatusType: 1

        };
        var isExist = true;

        for (var i = 0; i < $scope.MilitaryPresentDuties.length; i++) {

            if (addData.Title && $scope.MilitaryPresentDuties[i].Title.replace(" ", "").toLowerCase() == addData.Title.replace(" ", "").toLowerCase()) {

                isExist = false;
                $scope.existErr = "The Title is Exist";
                break;
            }
        }
        if (!addData.Title) { $scope.error = "Please Enter the Title"; }
        if (addData.Title && isExist) {
            $http.post(rootDir + '/MasterDataNew/AddMilitaryPresentDuties', addData).
                success(function (data, status, headers, config) {
                    //----------- success message -----------
                    if (data.status == "true") {
                        messageAlertEngine.callAlertMessage("MilitaryPresentDuty", "New Military Present Duty Details Added Successfully !!!!", "success", true);
                        data.militaryPresentDutyDetails.LastModifiedDate = $scope.ConvertDateFormat(data.militaryPresentDutyDetails.LastModifiedDate);
                        $scope.MilitaryPresentDuties[idx] = angular.copy(data.militaryPresentDutyDetails);
                        $scope.reset();
                        $scope.error = "";
                        $scope.existErr = "";
                    }
                    else {
                        messageAlertEngine.callAlertMessage("MilitaryPresentDutyError", "Sorry Unable To Add Military Present Duty !!!!", "success", true);
                        $scope.MilitaryPresentDuties.slpice(idx, 1);
                    }

                }).
                error(function (data, status, headers, config) {
                    //----------- error message -----------
                    messageAlertEngine.callAlertMessage("MilitaryPresentDutyError", "Sorry Unable To Add Military Present Duty !!!!", "success", true);
                    $scope.MilitaryPresentDuties.slpice(idx, 1);
                });

        }
    };

    //------------------- Update AdmittingPrivilege ---------------------
    $scope.updateMilitaryPresentDuty = function (idx) {
        //$scope.tempMPD.LastModifiedDate = "02/03/2015"
        var updateData = {
            MilitaryPresentDutyID: $scope.tempMPD.MilitaryPresentDutyID,
            Title: $scope.tempMPD.Title,
            StatusType: 1,

        };
        var isExist = true;

        for (var i = 0; i < $scope.MilitaryPresentDuties.length; i++) {

            if (updateData.Title && $scope.MilitaryPresentDuties[i].MilitaryPresentDutyID != updateData.MilitaryPresentDutyID && $scope.MilitaryPresentDuties[i].Title.replace(" ", "").toLowerCase() == updateData.Title.replace(" ", "").toLowerCase()) {

                isExist = false;
                $scope.existErr = "The Title is Exist";
                break;
            }
        }
        if (!updateData.Title) { $scope.error = "Please Enter the Title"; }
        //console.log("Update MilitaryPresentDuties");
        if (updateData && isExist) {
            $http.post(rootDir + '/MasterDataNew/UpdateMilitaryPresentDuties', updateData).
                success(function (data, status, headers, config) {
                    //----------- success message -----------
                    if (data.status == "true") {
                        messageAlertEngine.callAlertMessage("MilitaryPresentDuty", "Military Present Duty Details Updated Successfully !!!!", "success", true);
                        data.militaryPresentDutyDetails.LastModifiedDate = $scope.ConvertDateFormat(data.militaryPresentDutyDetails.LastModifiedDate);
                        $scope.MilitaryPresentDuties[idx] = angular.copy(data.militaryPresentDutyDetails);
                        $scope.reset();
                        $scope.error = "";
                        $scope.existErr = "";
                    }

                }).
                error(function (data, status, headers, config) {
                    //----------- error message -----------
                    messageAlertEngine.callAlertMessage("MilitaryPresentDutyError", "Sorry Unable To Update Military Present Duty !!!!", "danger", true);
                });
        }
    };

    //----------------- AdmittingPrivilege new add cancel ---------------
    $scope.cancelAdd = function () {
        $scope.MilitaryPresentDuties.splice(0, 1);
        $scope.tempMPD = {};
        $scope.disableEdit = false;
        $scope.disableAdd = false;
        $scope.existErr = "";
        $scope.error = "";
    };

    //-------------------- Reset AdmittingPrivilege ----------------------
    $scope.reset = function () {
        $scope.tempMPD = {};
        $scope.disableAdd = false;
        $scope.disableEdit = false;
        $scope.existErr = "";
        $scope.error = "";
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
        if ($scope.MilitaryPresentDuties) {
            for (startIndex; startIndex <= endIndex ; startIndex++) {
                if ($scope.MilitaryPresentDuties[startIndex]) {
                    $scope.CurrentPage.push($scope.MilitaryPresentDuties[startIndex]);
                } else {
                    break;
                }
            }
        }
        //console.log($scope.CurrentPageProviders);
    });
    //-------------- License Scope Watch ---------------------
    $scope.$watchCollection('MilitaryPresentDuties', function (newValue, oldValue) {
        if (newValue) {
            $scope.bigTotalItems = newValue.length;

            $scope.CurrentPage = [];
            $scope.bigCurrentPage = 1;

            var startIndex = ($scope.bigCurrentPage - 1) * 10;
            var endIndex = startIndex + 9;

            for (startIndex; startIndex <= endIndex ; startIndex++) {
                if ($scope.MilitaryPresentDuties[startIndex]) {
                    $scope.CurrentPage.push($scope.MilitaryPresentDuties[startIndex]);
                } else {
                    break;
                }
            }
            //console.log($scope.CurrentPageProviders);
        }
    });
    //------------------- end ------------------
}]);
