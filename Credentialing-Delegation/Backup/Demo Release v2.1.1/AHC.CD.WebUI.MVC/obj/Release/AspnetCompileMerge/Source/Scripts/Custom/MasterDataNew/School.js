//--------------------- Angular Module ----------------------
var masterDataSchools = angular.module("masterDataSchools", ['ui.bootstrap']);

//Service for getting master data
masterDataSchools.service('masterDataService', ['$http', '$q', function ($http, $q) {

    this.getMasterData = function (URL) {
        return $http.get(URL).then(function (value) { return value.data; });
    };

}]);

masterDataSchools.service('messageAlertEngine', ['$rootScope', '$timeout', function ($rootScope, $timeout) {

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
masterDataSchools.controller('masterDataSchoolsController', ['$scope', '$http', '$filter', '$rootScope', 'masterDataService', 'messageAlertEngine', function ($scope, $http, $filter, $rootScope, masterDataService, messageAlertEngine) {

    $scope.Schools = [];

    $http.get("/MasterDataNew/GetAllSchools").then(function (value) {
        console.log("Schools");
        console.log(value);

        for (var i = 0; i < value.data.length ; i++) {
            if (value.data[i] != null) {
                value.data[i].LastModifiedDate = $scope.ConvertDateFormat(value.data[i].LastModifiedDate);
            }
        }

        $scope.Schools = value.data;
        console.log($scope.Schools);
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

    
    $scope.tempSchools = {};

    //------------ gets the template to ng-include for a table row / item -------------------
    $scope.getTemplate = function (Schools) {
        if (Schools.SchoolID === $scope.tempSchools.SchoolID) return 'editSchools';
        else return 'displaySchools';
    };

    //-------------------- Edit School ----------
    $scope.editSchools = function (School) {
        $scope.tempSchools = angular.copy(School);
        $scope.disableAdd = true;
    };

    //------------------- Add School ---------------------
    $scope.addSchools = function (School) {
        $scope.disableEdit = true;
        $scope.disableAdd = true;
        var temp = {
            SchoolID: 0,
            Name: "",
            Status: "Active",
            LastModifiedDate: new Date()
        };
        $scope.Schools.splice(0, 0, angular.copy(temp));
        $scope.tempSchools = angular.copy(temp);
    };

    //------------------- Save School ---------------------
    $scope.saveSchools = function (idx) {

        var addData = {
            SchoolID: 0,
            Name: $scope.tempSchools.Name,
            StatusType: 1,
        };

        var isExist = true;

        for (var i = 0; i < $scope.Schools.length; i++) {

            if (addData.Name && $scope.Schools[i].Name.replace(" ", "").toLowerCase() == addData.Name.replace(" ", "").toLowerCase()) {

                isExist = false;
                $scope.existErr = "The Name is Exist";
                break;
            }
        }
        if (!addData.Name) { $scope.error = "Please Enter the Name"; }

        console.log("Saving schools");       
        if (addData.Name && isExist) {
            $http.post('/MasterDataNew/AddSchool', addData).
                success(function (data, status, headers, config) {
                    //----------- success message -----------
                    if (data.status == "true") {
                        messageAlertEngine.callAlertMessage("School", "New School Details Added Successfully !!!!", "success", true);
                        data.schoolDetails.LastModifiedDate = $scope.ConvertDateFormat(data.schoolDetails.LastModifiedDate);
                        $scope.Schools[idx] = angular.copy(data.schoolDetails);                        
                        $scope.reset();
                        $scope.error = "";
                        $scope.existErr = "";
                    }
                    else {
                        messageAlertEngine.callAlertMessage("SchoolError", "Sorry Unable To Add School !!!!", "danger", true);
                        $scope.Schools.slpice(idx, 1);
                    }
                }).
                error(function (data, status, headers, config) {
                    //----------- error message -----------
                    messageAlertEngine.callAlertMessage("SchoolError", "Sorry Unable To Add School !!!!", "danger", true);
                    $scope.Schools.slpice(idx, 1);
                });
        }
        
    };

    $scope.updateSchools = function (idx) {

        var updateData = {
            SchoolID: $scope.tempSchools.SchoolID,
            Name: $scope.tempSchools.Name,
            StatusType: 1,
        };

        var isExist = true;

        for (var i = 0; i < $scope.Schools.length; i++) {

            if (updateData.Name && $scope.Schools[i].SchoolID != updateData.SchoolID && $scope.Schools[i].Name.replace(" ", "").toLowerCase() == updateData.Name.replace(" ", "").toLowerCase()) {

                isExist = false;
                $scope.existErr = "The Name is Exist";
                break;
            }
        }
        if (!updateData.Name) { $scope.error = "Please Enter the Name"; }

        console.log("Updating schools");
        if (updateData.Name && isExist) {
            $http.post('/MasterDataNew/UpdateSchool', updateData).
                success(function (data, status, headers, config) {
                    //----------- success message -----------
                    if (data.status == "true") {
                        messageAlertEngine.callAlertMessage("School", "New School Details Updated Successfully !!!!", "success", true);
                        data.schoolDetails.LastModifiedDate = $scope.ConvertDateFormat(data.schoolDetails.LastModifiedDate);
                        $scope.Schools[idx] = angular.copy(data.schoolDetails);                        
                        $scope.reset();
                        $scope.error = "";
                        $scope.existErr = "";
                    }
                    
                }).
                error(function (data, status, headers, config) {
                    //----------- error message -----------
                    messageAlertEngine.callAlertMessage("School", "Sorry Unable To Update School !!!!", "danger", true);
                    
                });
        }

    };

    //----------------- School new add cancel ---------------
    $scope.cancelAdd = function () {
        $scope.Schools.splice(0, 1);
        $scope.tempSchools = {};
        $scope.disableEdit = false;
        $scope.disableAdd = false;
        $scope.error = "";
        $scope.existErr = "";
    };

    //-------------------- Reset School ----------------------
    $scope.reset = function () {
        $scope.tempSchools = {};
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
        if ($scope.Schools) {
            for (startIndex; startIndex <= endIndex ; startIndex++) {
                if ($scope.Schools[startIndex]) {
                    $scope.CurrentPage.push($scope.Schools[startIndex]);
                } else {
                    break;
                }
            }
        }
        //console.log($scope.CurrentPageProviders);
    });
    //-------------- License Scope Watch ---------------------
    $scope.$watchCollection('Schools', function (newValue, oldValue) {
        if (newValue) {
            $scope.bigTotalItems = newValue.length;

            $scope.CurrentPage = [];
            $scope.bigCurrentPage = 1;

            var startIndex = ($scope.bigCurrentPage - 1) * 10;
            var endIndex = startIndex + 9;

            for (startIndex; startIndex <= endIndex ; startIndex++) {
                if ($scope.Schools[startIndex]) {
                    $scope.CurrentPage.push($scope.Schools[startIndex]);
                } else {
                    break;
                }
            }
            //console.log($scope.CurrentPageProviders);
        }
    });
    //------------------- end ------------------
}]);
