//--------------------- Angular Module ----------------------
var masterDataInsuaranceCompanyNames = angular.module("masterDataInsuaranceCompanyNames", ['ui.bootstrap']);

//Service for getting master data
masterDataInsuaranceCompanyNames.service('masterDataService', ['$http', '$q', function ($http, $q) {

    this.getMasterData = function (URL) {
        return $http.get(URL).then(function (value) { return value.data; });
    };

}]);

masterDataInsuaranceCompanyNames.service('messageAlertEngine', ['$rootScope', '$timeout', function ($rootScope, $timeout) {

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
masterDataInsuaranceCompanyNames.controller('masterDataInsuaranceCompanyNameController', ['$scope', '$http', '$filter', '$rootScope', 'masterDataService', 'messageAlertEngine', function ($scope, $http, $filter, $rootScope, masterDataService, messageAlertEngine) {

    $scope.InsuaranceCompanyNames = [];

    $http.get(rootDir + "/MasterDataNew/GetAllInsuaranceCompanyNames").success(function (value) {

        try {
            for (var i = 0; i < value.length ; i++) {
                if (value[i] != null) {
                    value[i].LastModifiedDate = ($scope.ConvertDateFormat(value[i].LastModifiedDate)).toString();
                }

            }

            $scope.InsuaranceCompanyNames = angular.copy(value);
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


    $scope.tempInsuaranceCompanyNames = {};
    $scope.InsuaranceCompanyNames = [];

    //------------ gets the template to ng-include for a table row / item -------------------
    $scope.getTemplate = function (InsuaranceCompanyNames) {
        if (InsuaranceCompanyNames.InsuaranceCompanyNameID === $scope.tempInsuaranceCompanyNames.InsuaranceCompanyNameID)
            return 'editInsuaranceCompanyName';
        else
            return 'displayCompanyNames';
    };

    //-------------------- Edit Insuarance Company names ----------
    $scope.editInsuaranceCompanyName = function (insuaranceCompanyName) {
        $scope.tempInsuaranceCompanyNames = angular.copy(insuaranceCompanyName);
        $scope.disableAdd = true;
    };

    //------------------- Add Insuarance Company Names ---------------------
    $scope.addInsuaranceCompanyNames = function (insuaranceCompanyName) {
        $scope.disableEdit = true;
        $scope.disableAdd = true;
        var Month = new Date().getMonth() + 1;
        var _month = Month < 10 ? '0' + Month : Month;
        var _date = new Date().getDate() < 10 ? '0' + new Date().getDate() : new Date().getDate();
        var _year = new Date().getFullYear();
        var temp = {
            addInsuaranceCompanyNames: 0,
            CompanyName: "",
            Status: "Active",
            LastModifiedDate: _month + "-" + _date + "-" + _year,
        };
        $scope.InsuaranceCompanyNames.splice(0, 0, angular.copy(temp));
        $scope.tempInsuaranceCompanyNames = angular.copy(temp);
    };

    //------------------- Save Insuarance Company Names ---------------------
    $scope.saveInsuaranceCompanyNames = function (idx) {

        var addData = {
            InsuaranceCompanyNameID: 0,
            CompanyName: $scope.tempInsuaranceCompanyNames.CompanyName,
            StatusType: 1,
        };

        

        var isExist = true;

        for (var i = 0; i < $scope.InsuaranceCompanyNames.length; i++) {

            if (addData.CompanyName && $scope.InsuaranceCompanyNames[i].CompanyName.replace(" ", "").toLowerCase() == addData.CompanyName.replace(" ", "").toLowerCase()) {

                isExist = false;
                $scope.existErr = "The Company Name is Exist";
                break;
            }
        }

        if (!addData.CompanyName) { $scope.error = "Please Enter the CompanyName"; }


        if (addData.CompanyName && isExist) {
            $http.post(rootDir + '/MasterDataNew/AddInsuaranceCompany', addData).
                success(function (data, status, headers, config) {
                    try {
                        //----------- success message -----------
                        if (data.status == "true") {
                            messageAlertEngine.callAlertMessage("InsuaranceCompany", "Insuarance Company Added Successfully !!!!", "success", true);
                            data.InsuaranceCompanyNameDetails.LastModifiedDate = $scope.ConvertDateFormat(data.InsuaranceCompanyNameDetails.LastModifiedDate);
                            $scope.InsuaranceCompanyNames[idx] = angular.copy(data.InsuaranceCompanyNameDetails);
                            $scope.reset();
                            $scope.error = "";
                            $scope.existErr = "";
                        }
                        else {
                            messageAlertEngine.callAlertMessage("InsuaranceCompanyNameError", "Sorry Unable To Add new Insuarance Company Name !!!!", "danger", true);
                            $scope.InsuaranceCompanyNames.slpice(idx, 1);
                        }
                    } catch (e) {

                    }
                }).
                error(function (data, status, headers, config) {
                    //----------- error message -----------
                    messageAlertEngine.callAlertMessage("InsuaranceCompanyNameError", "Sorry Unable To Add new Insuarance Company Name !!!!", "danger", true);
                    $scope.InsuaranceCompanyNames.slpice(idx, 1);
                });
        }


    };

    //------------------- Update Insuarance Company Names ---------------------
    $scope.updateInsuaranceCompanyNames = function (idx) {

        var updateData = {
            InsuaranceCompanyNameID: $scope.tempInsuaranceCompanyNames.InsuaranceCompanyNameID,
            CompanyName: $scope.tempInsuaranceCompanyNames.CompanyName,
            StatusType: 1,
        };

        var isExist = true;

        for (var i = 0; i < $scope.InsuaranceCompanyNames.length; i++) {

            if (updateData.CompanyName && $scope.InsuaranceCompanyNames[i].InsuaranceCompanyNameID != updateData.InsuaranceCompanyNameID && $scope.InsuaranceCompanyNames[i].CompanyName.replace(" ", "").toLowerCase() == updateData.CompanyName.replace(" ", "").toLowerCase()) {

                isExist = false;
                $scope.existErr = "The Company Name is Exist";
                break;
            }
        }
        if (!updateData.CompanyName) { $scope.error = "Please Enter the Company Name"; }



        if (updateData.CompanyName && isExist) {
            $http.post(rootDir + '/MasterDataNew/UpdateInsuaranceCompanyNames', updateData).
                success(function (data, status, headers, config) {
                    try {
                        //----------- success message -----------
                        if (data.status == "true") {
                            messageAlertEngine.callAlertMessage("InsuaranceCompanyName", "Insuarance Company Details Updated Successfully !!!!", "success", true);
                            data.insuaranceCompanyNameDetails.LastModifiedDate = $scope.ConvertDateFormat(data.insuaranceCompanyNameDetails.LastModifiedDate);
                            $scope.InsuaranceCompanyNames[idx] = angular.copy(data.insuaranceCompanyNameDetails);
                            $scope.reset();
                            $scope.error = "";
                            $scope.existErr = "";
                        }
                    } catch (e) {

                    }

                }).
                error(function (data, status, headers, config) {
                    //----------- error message -----------
                    messageAlertEngine.callAlertMessage("insuaranceCompanyNameError", "Sorry Unable To Update Insuarance Company Name !!!!", "danger", true);
                });
        }


    };

    //----------------- Insuarance Company Names new add cancel ---------------
    $scope.cancelAdd = function () {
        $scope.InsuaranceCompanyNames.splice(0, 1);
        $scope.tempInsuaranceCompanyNames = {};
        $scope.disableEdit = false;
        $scope.disableAdd = false;
        $scope.error = "";
        $scope.existErr = "";
    };

    //-------------------- Reset Insuarance Company Names ----------------------
    $scope.reset = function () {
        $scope.tempInsuaranceCompanyNames = {};
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
        if ($scope.InsuaranceCompanyNames) {
            for (startIndex; startIndex <= endIndex ; startIndex++) {
                if ($scope.InsuaranceCompanyNames[startIndex]) {
                    $scope.CurrentPage.push($scope.InsuaranceCompanyNames[startIndex]);
                } else {
                    break;
                }
            }
        }
    });
    //-------------- License Scope Watch ---------------------
    $scope.$watchCollection('InsuaranceCompanyNames', function (newValue, oldValue) {
        if (newValue) {
            $scope.bigTotalItems = newValue.length;

            $scope.CurrentPage = [];
            $scope.bigCurrentPage = 1;

            var startIndex = ($scope.bigCurrentPage - 1) * 10;
            var endIndex = startIndex + 9;

            for (startIndex; startIndex <= endIndex ; startIndex++) {
                if ($scope.InsuaranceCompanyNames[startIndex]) {
                    $scope.CurrentPage.push($scope.InsuaranceCompanyNames[startIndex]);
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
