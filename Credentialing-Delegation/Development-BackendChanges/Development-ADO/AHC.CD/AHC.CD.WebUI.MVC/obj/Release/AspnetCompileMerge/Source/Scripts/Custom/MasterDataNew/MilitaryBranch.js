//--------------------- Angular Module ----------------------
var masterDataMilitaryBranchs = angular.module("masterDataMilitaryBranchs", ['ui.bootstrap']);

masterDataMilitaryBranchs.service('messageAlertEngine', ['$rootScope', '$timeout', function ($rootScope, $timeout) {

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
masterDataMilitaryBranchs.controller('masterDataMilitaryBranchsController', ['$scope', '$http', '$filter', 'messageAlertEngine', function ($scope, $http, $filter, messageAlertEngine) {

    $http.get(rootDir + "/MasterDataNew/GetAllMilitaryBranches").then(function (value) {
        //console.log("Military Branches");
        //console.log(value);

        for (var i = 0; i < value.data.length ; i++) {
            if (value.data[i] != null) {
                value.data[i].LastModifiedDate = $scope.ConvertDateFormat(value.data[i].LastModifiedDate);
            }
        }

        $scope.MilitaryBranchs = angular.copy(value.data);
        //console.log($scope.MilitaryBranchs);
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
    

    $scope.tempMilitaryBranchs = {};

    //------------ gets the template to ng-include for a table row / item -------------------
    $scope.getTemplate = function (MilitaryBranchs) {
        if (MilitaryBranchs.MilitaryBranchID === $scope.tempMilitaryBranchs.MilitaryBranchID) return 'editMilitaryBranchs';
        else return 'displayMilitaryBranchs';
    };

    //-------------------- Edit MilitaryBranch ----------
    $scope.editMilitaryBranchs = function (MilitaryBranch) {
        $scope.tempMilitaryBranchs = angular.copy(MilitaryBranch);
    };

    //------------------- Add MilitaryBranch ---------------------
    $scope.addMilitaryBranchs = function (MilitaryBranch) {
        $scope.disableAdd = true;
        var temp = {
            MilitaryBranchID: 0,
            Title: "",
            Status: "Active",
            LastModifiedDate: new Date()
        };
        $scope.MilitaryBranchs.splice(0, 0, angular.copy(temp));
        $scope.tempMilitaryBranchs = angular.copy(temp);
    };

    //------------------- Save MilitaryBranch ---------------------
    $scope.saveMilitaryBranchs = function (idx) {
         
        var addData = {
            MilitaryBranchID: 0,
            Title: $scope.tempMilitaryBranchs.Title,
            StatusType: 1,
            LastModifiedDate: new Date()
        };

        if (!addData.Title) { $scope.branchError = "Please enter the Title"; };

        if (addData.Title) {
            $http.post(rootDir + '/MasterDataNew/AddMilitaryBranch', addData).
                success(function (data, status, headers, config) {
                    //----------- success message -----------
                    if (data.status == "true") {
                        messageAlertEngine.callAlertMessage("MilitaryBranch", "New Military Branch Details Added Successfully !!!!", "success", true);
                        data.militaryBranchDetails.LastModifiedDate = $scope.ConvertDateFormat(data.militaryBranchDetails.LastModifiedDate);
                        for (var i = 0; i < $scope.MilitaryBranchs.length; i++) {

                            if ($scope.MilitaryBranchs[i].MilitaryBranchID == 0) {
                                $scope.MilitaryBranchs[i] = angular.copy(data.militaryBranchDetails);
                            }
                        }
                        $scope.reset();
                    }
                }).
                error(function (data, status, headers, config) {
                    //----------- error message -----------
                    messageAlertEngine.callAlertMessage("MilitaryBranchError", "Sorry Unable To Add Military Branch !!!!", "danger", true);
                    $scope.MilitaryBranchs.splice(0, 1);
                });
        }
    };

    //------------------- Update MilitaryBranch ---------------------
    $scope.updateMilitaryBranchs = function (idx) {

        var updateData = {
            MilitaryBranchID: $scope.tempMilitaryBranchs.MilitaryBranchID,
            Title: $scope.tempMilitaryBranchs.Title,
            StatusType: 1,
            
        };
        if (!updateData.Title) { $scope.branchError = "Please enter the Title"; };
        if (updateData.Title) { 
            $http.post(rootDir + '/MasterDataNew/UpdateMilitaryBranch', updateData).
            success(function (data, status, headers, config) {
                //----------- success message -----------
                if (data.status == "true") {
                    messageAlertEngine.callAlertMessage("MilitaryBranch", "Military Branch Details Updated Successfully !!!!", "success", true);
                    data.militaryBranchDetails.LastModifiedDate = $scope.ConvertDateFormat(data.militaryBranchDetails.LastModifiedDate);
                    for (var i = 0; i < $scope.MilitaryBranchs.length; i++) {

                        if ($scope.MilitaryBranchs[i].MilitaryBranchID == data.militaryBranchDetails.MilitaryBranchID) {
                            $scope.MilitaryBranchs[i] = angular.copy(data.militaryBranchDetails);
                        }
                    }
                    $scope.reset();
                }
            }).
            error(function (data, status, headers, config) {
                //----------- error message -----------
                messageAlertEngine.callAlertMessage("MilitaryBranchError", "Sorry Unable To Update Military Branch !!!!", "danger", true);
            });
        }
    };

    //----------------- MilitaryBranch new add cancel ---------------
    $scope.cancelAdd = function () {
        $scope.disableAdd = false;
        $scope.MilitaryBranchs.splice(0, 1);
        $scope.tempMilitaryBranchs = {};
        $scope.branchError = "";
    };

    //-------------------- Reset MilitaryBranch ----------------------
    $scope.reset = function () {
        $scope.disableAdd = false;
        $scope.tempMilitaryBranchs = {};
        $scope.branchError = "";
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
        if ($scope.MilitaryBranchs) {
            for (startIndex; startIndex <= endIndex ; startIndex++) {
                if ($scope.MilitaryBranchs[startIndex]) {
                    $scope.CurrentPage.push($scope.MilitaryBranchs[startIndex]);
                } else {
                    break;
                }
            }
        }
        //console.log($scope.CurrentPageProviders);
    });
    //-------------- License Scope Watch ---------------------
    $scope.$watchCollection('MilitaryBranchs', function (newValue, oldValue) {
        if (newValue) {
            $scope.bigTotalItems = newValue.length;

            $scope.CurrentPage = [];
            $scope.bigCurrentPage = 1;

            var startIndex = ($scope.bigCurrentPage - 1) * 10;
            var endIndex = startIndex + 9;

            for (startIndex; startIndex <= endIndex ; startIndex++) {
                if ($scope.MilitaryBranchs[startIndex]) {
                    $scope.CurrentPage.push($scope.MilitaryBranchs[startIndex]);
                } else {
                    break;
                }
            }
            //console.log($scope.CurrentPageProviders);
        }
    });
    //------------------- end ------------------
}]);
