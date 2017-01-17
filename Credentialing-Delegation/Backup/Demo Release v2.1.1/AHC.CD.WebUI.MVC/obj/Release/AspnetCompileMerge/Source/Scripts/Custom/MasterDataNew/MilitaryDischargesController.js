//--------------------- Angular Module ----------------------
var masterDataMilitaryDischarges = angular.module("masterDataMilitaryDischarges", ['ui.bootstrap']);

masterDataMilitaryDischarges.service('messageAlertEngine', ['$rootScope', '$timeout', function ($rootScope, $timeout) {

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
masterDataMilitaryDischarges.controller('masterDataMilitaryDischargesController', ['$scope', '$http', '$filter', '$rootScope', 'messageAlertEngine', function ($scope, $http, $filter, $rootScope, messageAlertEngine) {


    $http.get("/MasterDataNew/GetAllMilitaryDischarges").then(function (value) {
        console.log("MilitaryDischarges");
        console.log(value);

        for (var i = 0; i < value.data.length ; i++) {
            if (value.data[i] != null) {
                value.data[i].LastModifiedDate = $scope.ConvertDateFormat(value.data[i].LastModifiedDate);
            }
        }

        $scope.MilitaryDischarges = value.data;
        console.log($scope.MilitaryDischarges);
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

    

    $scope.tempMD = {};

    //------------ gets the template to ng-include for a table row / item -------------------
    $scope.getTemplate = function (militaryDischarge) {
        if (militaryDischarge.MilitaryDischargeID === $scope.tempMD.MilitaryDischargeID) return 'editMilitaryDischarge';
        else return 'displayMilitaryDischarge';
    };

    //-------------------- Edit AdmittingPrivilege ----------
    $scope.editMilitaryDischarge = function (militaryDischarge) {
        $scope.tempMD = angular.copy(militaryDischarge);
        $scope.disableAdd = true;
    };

    //------------------- Add AdmittingPrivilege ---------------------
    $scope.addMilitaryDischarge = function (militaryDischarge) {
        $scope.disableEdit = true;
        $scope.disableAdd = true;
        var temp = {
            MilitaryDischargeID: 0,
            Title: "",
            Status: "Active",
            LastModifiedDate: new Date()
        };
        $scope.MilitaryDischarges.splice(0, 0, angular.copy(temp));
        $scope.tempMD = angular.copy(temp);
    };

    //------------------- Save AdmittingPrivilege ---------------------
    $scope.saveMilitaryDischarge = function (idx) {
        //$scope.tempMD.LastModifiedDate = "02/03/2015"
        var addData = {
            MilitaryDischargeID: 0,
            Title: $scope.tempMD.Title,
            StatusType: 1,
            
        };
        var isExist = true;

        for (var i = 0; i < $scope.MilitaryDischarges.length; i++) {

            if (addData.Title && $scope.MilitaryDischarges[i].Title.replace(" ", "").toLowerCase() == addData.Title.replace(" ", "").toLowerCase()) {

                isExist = false;
                $scope.existErr = "The Title is Exist";
                break;
            }
        }
        if (!addData.Title) { $scope.error = "Please Enter the Title"; }
        console.log("Saving militaryDischarge");
        if (addData.Title && isExist) {
            $http.post('/MasterDataNew/AddMilitaryDischarges', addData).
                success(function (data, status, headers, config) {
                    //----------- success message -----------
                    if (data.status == "true") {
                        messageAlertEngine.callAlertMessage("MilitaryDischarges", "New Military Discharge Details Added Successfully !!!!", "success", true);
                        data.militaryDischargeDetails.LastModifiedDate = $scope.ConvertDateFormat(data.militaryDischargeDetails.LastModifiedDate);
                        $scope.MilitaryDischarges[idx] = angular.copy(data.militaryDischargeDetails);                        
                        $scope.reset();
                        $scope.error = "";
                        $scope.existErr = "";
                    }
                    else {
                        messageAlertEngine.callAlertMessage("MilitaryDischargesError", "Sorry Unable To Add Military Discharge !!!!", "danger", true);
                        $scope.MilitaryDischarges.slpice(idx, 1);
                    }

                }).
                error(function (data, status, headers, config) {
                    //----------- error message -----------
                    messageAlertEngine.callAlertMessage("MilitaryDischargesError", "Sorry Unable To Add Military Discharge !!!!", "danger", true);
                    $scope.MilitaryDischarges.slpice(idx, 1);
                });

        }
        
    };

    //------------------- Update AdmittingPrivilege ---------------------
    $scope.updateMilitaryDischarge = function (idx) {
        //$scope.tempMD.LastModifiedDate = "02/03/2015"
        var updateData = {
            MilitaryDischargeID: $scope.tempMD.MilitaryDischargeID,
            Title: $scope.tempMD.Title,
            StatusType: 1,

        };
        var isExist = true;

        for (var i = 0; i < $scope.MilitaryDischarges.length; i++) {

            if (updateData.Title && $scope.MilitaryDischarges[i].MilitaryDischargeID != updateData.MilitaryDischargeID && $scope.MilitaryDischarges[i].Title.replace(" ", "").toLowerCase() == updateData.Title.replace(" ", "").toLowerCase()) {

                isExist = false;
                $scope.existErr = "The Title is Exist";
                break;
            }
        }
        if (!updateData.Title) { $scope.error = "Please Enter the Title"; }
        console.log("Updating militaryDischarge");
        if (updateData && isExist){
            $http.post('/MasterDataNew/UpdateMilitaryDischarges', updateData).
            success(function (data, status, headers, config) {
                //----------- success message -----------
                if (data.status == "true") {
                    messageAlertEngine.callAlertMessage("MilitaryDischarges", "Military Discharge Details Updated Successfully !!!!", "success", true);
                    data.militaryDischargeDetails.LastModifiedDate = $scope.ConvertDateFormat(data.militaryDischargeDetails.LastModifiedDate);
                    $scope.MilitaryDischarges[idx] = angular.copy(data.militaryDischargeDetails);                    
                    $scope.reset();
                }
                else {
                    messageAlertEngine.callAlertMessage("MilitaryDischargesError", "Sorry Unable To Update Military Discharge !!!!", "danger", true);
                }
                
            }).
            error(function (data, status, headers, config) {
                //----------- error message -----------
                messageAlertEngine.callAlertMessage("MilitaryDischargesError", "Sorry Unable To Update Military Discharge !!!!", "danger", true);
            });
        }
        
    };

    //----------------- AdmittingPrivilege new add cancel ---------------
    $scope.cancelAdd = function () {
        $scope.MilitaryDischarges.splice(0, 1);
        $scope.tempMD = {};
        $scope.disableEdit = false;
        $scope.disableAdd = false;
        $scope.existErr = "";
        $scope.error = "";
    };

    //-------------------- Reset AdmittingPrivilege ----------------------
    $scope.reset = function () {
        $scope.tempMD = {};
        $scope.disableEdit = false;
        $scope.disableAdd = false;
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
        if ($scope.MilitaryDischarges) {
            for (startIndex; startIndex <= endIndex ; startIndex++) {
                if ($scope.MilitaryDischarges[startIndex]) {
                    $scope.CurrentPage.push($scope.MilitaryDischarges[startIndex]);
                } else {
                    break;
                }
            }
        }
        //console.log($scope.CurrentPageProviders);
    });
    //-------------- License Scope Watch ---------------------
    $scope.$watchCollection('MilitaryDischarges', function (newValue, oldValue) {
        if (newValue) {
            $scope.bigTotalItems = newValue.length;

            $scope.CurrentPage = [];
            $scope.bigCurrentPage = 1;

            var startIndex = ($scope.bigCurrentPage - 1) * 10;
            var endIndex = startIndex + 9;

            for (startIndex; startIndex <= endIndex ; startIndex++) {
                if ($scope.MilitaryDischarges[startIndex]) {
                    $scope.CurrentPage.push($scope.MilitaryDischarges[startIndex]);
                } else {
                    break;
                }
            }
            //console.log($scope.CurrentPageProviders);
        }
    });
    //------------------- end ------------------

}]);
