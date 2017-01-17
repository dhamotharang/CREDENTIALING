//--------------------- Angular Module ----------------------
var masterDataStaffCategorys = angular.module("masterDataStaffCategorys", ['ui.bootstrap']);

//Service for getting master data
masterDataStaffCategorys.service('masterDataService', ['$http', '$q', function ($http, $q) {

    this.getMasterData = function (URL) {
        return $http.get(URL).then(function (value) { return value.data; });
    };

}]);

masterDataStaffCategorys.service('messageAlertEngine', ['$rootScope', '$timeout', function ($rootScope, $timeout) {

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
masterDataStaffCategorys.controller('masterDataStaffCategorysController', ['$scope', '$http', '$filter', '$rootScope', 'masterDataService', 'messageAlertEngine', function ($scope, $http, $filter, $rootScope, masterDataService, messageAlertEngine) {

    $scope.StaffCategorys = [];

    $http.get(rootDir + "/MasterDataNew/GetAllStaffCategories").success(function (value) {
        try {

            for (var i = 0; i < value.length ; i++) {
                if (value[i] != null) {
                    value[i].LastModifiedDate = ($scope.ConvertDateFormat(value[i].LastModifiedDate)).toString();
                }

            }

            $scope.StaffCategorys = angular.copy(value);
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


    

    $scope.tempStaffCategorys = {};

    //------------ gets the template to ng-include for a table row / item -------------------
    $scope.getTemplate = function (StaffCategorys) {
        if (StaffCategorys.StaffCategoryID === $scope.tempStaffCategorys.StaffCategoryID) return 'editStaffCategorys';
        else return 'displayStaffCategorys';
    };

    //-------------------- Edit StaffCategory ----------
    $scope.editStaffCategorys = function (StaffCategory) {
        $scope.tempStaffCategorys = angular.copy(StaffCategory);
        $scope.disableAdd = true;
    };

    //------------------- Add StaffCategory ---------------------
    $scope.addStaffCategorys = function (StaffCategory) {
        $scope.disableEdit = true;
        $scope.disableAdd = true;
        var Month = new Date().getMonth() + 1;
        var _month = Month < 10 ? '0' + Month : Month;
        var _date = new Date().getDate() < 10 ? '0' + new Date().getDate() : new Date().getDate();
        var _year = new Date().getFullYear();
        var temp = {
            StaffCategoryID: 0,
            Title: "",            
            Status: "Active",
            LastModifiedDate: _month + "-" + _date + "-" + _year
        };
        $scope.StaffCategorys.splice(0, 0, angular.copy(temp));
        $scope.tempStaffCategorys = angular.copy(temp);
    };

    //------------------- Save StaffCategory ---------------------
    $scope.saveStaffCategorys = function (idx) {

        var addData = {
            StaffCategoryID: 0,
            Title: $scope.tempStaffCategorys.Title,
            StatusType: 1,
        };

        var isExist = true;

        for (var i = 0; i < $scope.StaffCategorys.length; i++) {

            if (addData.Title && $scope.StaffCategorys[i].Title.replace(" ", "").toLowerCase() == addData.Title.replace(" ", "").toLowerCase()) {

                isExist = false;
                $scope.existErr = "The Title is Exist";
                break;
            }
        }
        if (!addData.Title) { $scope.existErr = "";$scope.error = "Please Enter the Title"; }

        
        if (addData.Title && isExist) {
            $http.post(rootDir + '/MasterDataNew/AddStaffCategory', addData).
                success(function (data, status, headers, config) {
                    try {
                        //----------- success message -----------
                        if (data.status == "true") {
                            messageAlertEngine.callAlertMessage("StaffCategory", "New Staff Category Details Added Successfully !!!!", "success", true);
                            data.staffCategoryDetails.LastModifiedDate = $scope.ConvertDateFormat(data.staffCategoryDetails.LastModifiedDate);
                            $scope.StaffCategorys[idx] = angular.copy(data.staffCategoryDetails);
                            $scope.reset();
                            $scope.error = "";
                            $scope.existErr = "";
                        }
                        else {
                            messageAlertEngine.callAlertMessage("StaffCategoryError", "Sorry Unable To Add Staff Category !!!!", "danger", true);
                            $scope.StaffCategorys.slpice(idx, 1);
                        }
                    } catch (e) {
                       
                    }
                }).
                error(function (data, status, headers, config) {
                    //----------- error message -----------
                    messageAlertEngine.callAlertMessage("StaffCategoryError", "Sorry Unable To Add Staff Category !!!!", "danger", true);
                    $scope.StaffCategorys.slpice(idx, 1);
                });
        }

        
    };


    //------------------- Update StaffCategory ---------------------
    $scope.updateStaffCategorys = function (idx) {

        var updateData = {
            StaffCategoryID: $scope.tempStaffCategorys.StaffCategoryID,
            Title: $scope.tempStaffCategorys.Title,
            StatusType: 1,
        };

        var isExist = true;

        for (var i = 0; i < $scope.StaffCategorys.length; i++) {

            if (updateData.Title && $scope.StaffCategorys[i].StaffCategoryID != updateData.StaffCategoryID && $scope.StaffCategorys[i].Title.replace(" ", "").toLowerCase() == updateData.Title.replace(" ", "").toLowerCase()) {

                isExist = false;
                $scope.existErr = "The Title is Exist";
                break;
            }
            if ($scope.StaffCategorys[i].StaffCategoryID == updateData.StaffCategoryID) {
                idx = i;
            }

        }
        if (!updateData.Title) { $scope.error = "Please Enter the Title"; }


        if (updateData.Title && isExist) {
            $http.post(rootDir + '/MasterDataNew/UpdateStaffCategory', updateData).
                success(function (data, status, headers, config) {
                    try {
                        //----------- success message -----------
                        if (data.status == "true") {
                            messageAlertEngine.callAlertMessage("StaffCategory", "Staff Category Details Updated Successfully !!!!", "success", true);
                            data.staffCategoryDetails.LastModifiedDate = $scope.ConvertDateFormat(data.staffCategoryDetails.LastModifiedDate);
                            $scope.StaffCategorys[idx] = angular.copy(data.staffCategoryDetails);
                            $scope.reset();
                            $scope.error = "";
                            $scope.existErr = "";
                        }
                    } catch (e) {
                      
                    }
                }).
                error(function (data, status, headers, config) {
                    //----------- error message -----------
                    messageAlertEngine.callAlertMessage("StaffCategoryError", "Sorry Unable To Update Staff Category !!!!", "danger", true);                    
                });
        }


    };

    //----------------- StaffCategory new add cancel ---------------
    $scope.cancelAdd = function () {
        $scope.StaffCategorys.splice(0, 1);
        $scope.tempStaffCategorys = {};
        $scope.disableEdit = false;
        $scope.disableAdd = false;
        $scope.error = "";
        $scope.existErr = "";
    };

    //-------------------- Reset StaffCategory ----------------------
    $scope.reset = function () {
        $scope.tempStaffCategorys = {};
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
        if ($scope.StaffCategorys) {
            for (startIndex; startIndex <= endIndex ; startIndex++) {
                if ($scope.StaffCategorys[startIndex]) {
                    $scope.CurrentPage.push($scope.StaffCategorys[startIndex]);
                } else {
                    break;
                }
            }
        }
    });
    //-------------- License Scope Watch ---------------------
    $scope.$watchCollection('StaffCategorys', function (newValue, oldValue) {
        if (newValue) {
            $scope.bigTotalItems = newValue.length;

            $scope.CurrentPage = [];
            $scope.bigCurrentPage = 1;

            var startIndex = ($scope.bigCurrentPage - 1) * 10;
            var endIndex = startIndex + 9;

            for (startIndex; startIndex <= endIndex ; startIndex++) {
                if ($scope.StaffCategorys[startIndex]) {
                    $scope.CurrentPage.push($scope.StaffCategorys[startIndex]);
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
