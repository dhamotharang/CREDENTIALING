//--------------------- Angular Module ----------------------
var masterDataSpecialtys = angular.module("masterDataSpecialtys", ['ui.bootstrap']);

//Service for getting master data
masterDataSpecialtys.service('masterDataService', ['$http', '$q', function ($http, $q) {

    this.getMasterData = function (URL) {
        return $http.get(URL).then(function (value) { return value.data; });
    };

}]);

masterDataSpecialtys.service('messageAlertEngine', ['$rootScope', '$timeout', function ($rootScope, $timeout) {

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
masterDataSpecialtys.controller('masterDataSpecialtysController', ['$scope', '$http', '$filter', '$rootScope', 'masterDataService', 'messageAlertEngine', function ($scope, $http, $filter, $rootScope, masterDataService, messageAlertEngine) {

    $scope.Specialtys = [];

    $http.get(rootDir + "/MasterDataNew/GetAllSpecialities").success(function (value) {
        try {

            for (var i = 0; i < value.length ; i++) {
                if (value[i] != null) {
                    value[i].LastModifiedDate = ($scope.ConvertDateFormat(value[i].LastModifiedDate)).toString();
                }

            }

            $scope.Specialtys = angular.copy(value);
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

    

    $scope.tempSpecialtys = {};

    //------------ gets the template to ng-include for a table row / item -------------------
    $scope.getTemplate = function (Specialtys) {
        if (Specialtys.SpecialtyID === $scope.tempSpecialtys.SpecialtyID) return 'editSpecialtys';
        else return 'displaySpecialtys';
    };

    //-------------------- Edit Specialty ----------
    $scope.editSpecialtys = function (Specialty) {
        $scope.tempSpecialtys = angular.copy(Specialty);
        $scope.disableAdd = true;
    };

    //------------------- Add Specialty ---------------------
    $scope.addSpecialtys = function (Specialty) {
        $scope.disableEdit = true;
        $scope.disableAdd = true;
        var Month = new Date().getMonth() + 1;
        var _month = Month < 10 ? '0' + Month : Month;
        var _date = new Date().getDate() < 10 ? '0' + new Date().getDate() : new Date().getDate();
        var _year = new Date().getFullYear();
        var temp = {
            SpecialtyID: 0,
            Name: "",
            Status: "Active",
            LastModifiedDate: _month + "-" + _date + "-" + _year
        };
        $scope.Specialtys.splice(0, 0, angular.copy(temp));
        $scope.tempSpecialtys = angular.copy(temp);
    };

    //------------------- Save Specialty ---------------------
    $scope.saveSpecialtys = function (idx) {

        var addData = {
            SpecialtyID: 0,
            Name: $scope.tempSpecialtys.Name,
            StatusType: 1,
        };

        var isExist = true;

        for (var i = 0; i < $scope.Specialtys.length; i++) {

            if (addData.Name && $scope.Specialtys[i].Name.replace(" ", "").toLowerCase() == addData.Name.replace(" ", "").toLowerCase()) {

                isExist = false;
                $scope.existErr = "The Name is Exist";
                break;
            }
        }
        if (!addData.Name) { $scope.error = "Please Enter the Name"; }

        
        if (addData.Name && isExist) {
            $http.post(rootDir + '/MasterDataNew/AddSpecialty', addData).
                success(function (data, status, headers, config) {
                    try {
                        //----------- success message -----------
                        if (data.status == "true") {
                            messageAlertEngine.callAlertMessage("Specialty", "New Specialty Details Added Successfully !!!!", "success", true);
                            data.specialtyDetails.LastModifiedDate = $scope.ConvertDateFormat(data.specialtyDetails.LastModifiedDate);
                            $scope.Specialtys[idx] = angular.copy(data.specialtyDetails);
                            $scope.reset();
                            $scope.error = "";
                            $scope.existErr = "";
                        }
                        else {
                            messageAlertEngine.callAlertMessage("SpecialtyError", "Sorry Unable To Add Specialty !!!!", "danger", true);
                            $scope.Specialtys.slpice(idx, 1);
                        }
                    } catch (e) {
                       
                    }
                }).
                error(function (data, status, headers, config) {
                    //----------- error message -----------
                    messageAlertEngine.callAlertMessage("SpecialtyError", "Sorry Unable To Add Specialty !!!!", "danger", true);
                    $scope.Specialtys.slpice(idx, 1);
                });
        }

        
    };

    //------------------- Update Specialty ---------------------
    $scope.updateSpecialtys = function (idx) {

        var updateData = {
            SpecialtyID: $scope.tempSpecialtys.SpecialtyID,
            Name: $scope.tempSpecialtys.Name,
            StatusType: 1,
        };

        var isExist = true;

        for (var i = 0; i < $scope.Specialtys.length; i++) {

            if (updateData.Name && $scope.Specialtys[i].SpecialtyID != updateData.SpecialtyID && $scope.Specialtys[i].Name.replace(" ", "").toLowerCase() == updateData.Name.replace(" ", "").toLowerCase()) {

                isExist = false;
                $scope.existErr = "The Name is Exist";
                break;
            }
        }
        if (!updateData.Name) { $scope.error = "Please Enter the Name"; }


        if (updateData.Name && isExist) {
            $http.post(rootDir + '/MasterDataNew/UpdateSpecialty', updateData).
                success(function (data, status, headers, config) {
                    try {
                        //----------- success message -----------
                        if (data.status == "true") {
                            messageAlertEngine.callAlertMessage("Specialty", "New Specialty Details Updated Successfully !!!!", "success", true);
                            data.specialtyDetails.LastModifiedDate = $scope.ConvertDateFormat(data.specialtyDetails.LastModifiedDate);
                            $scope.Specialtys[idx] = angular.copy(data.specialtyDetails);
                            $scope.reset();
                            $scope.error = "";
                            $scope.existErr = "";
                        }
                    } catch (e) {
                       
                    }
                }).
                error(function (data, status, headers, config) {
                    //----------- error message -----------
                    messageAlertEngine.callAlertMessage("SpecialtyError", "Sorry Unable To Update Specialty !!!!", "danger", true);
                    
                });
        }


    };

    //----------------- Specialty new add cancel ---------------
    $scope.cancelAdd = function () {
        $scope.Specialtys.splice(0, 1);
        $scope.tempSpecialtys = {};
        $scope.disableAdd = false;
        $scope.disableEdit = false;
        $scope.error = "";
        $scope.existErr = "";
    };

    //-------------------- Reset Specialty ----------------------
    $scope.reset = function () {
        $scope.tempSpecialtys = {};
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
        if ($scope.Specialtys) {
            for (startIndex; startIndex <= endIndex ; startIndex++) {
                if ($scope.Specialtys[startIndex]) {
                    $scope.CurrentPage.push($scope.Specialtys[startIndex]);
                } else {
                    break;
                }
            }
        }
    });
    //-------------- License Scope Watch ---------------------
    $scope.$watchCollection('Specialtys', function (newValue, oldValue) {
        if (newValue) {
            $scope.bigTotalItems = newValue.length;

            $scope.CurrentPage = [];
            $scope.bigCurrentPage = 1;

            var startIndex = ($scope.bigCurrentPage - 1) * 10;
            var endIndex = startIndex + 9;

            for (startIndex; startIndex <= endIndex ; startIndex++) {
                if ($scope.Specialtys[startIndex]) {
                    $scope.CurrentPage.push($scope.Specialtys[startIndex]);
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
