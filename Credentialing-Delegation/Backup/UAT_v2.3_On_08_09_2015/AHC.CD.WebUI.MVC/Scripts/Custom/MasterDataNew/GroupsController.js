//--------------------- Angular Module ----------------------
var masterDataGroup = angular.module("masterDataGroup", ["ahc.cd.util", 'ui.bootstrap']);

masterDataGroup.service('messageAlertEngine', ['$rootScope', '$timeout', function ($rootScope, $timeout) {

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
masterDataGroup.controller('masterDataGroupController', ['$scope', '$http', '$filter', '$rootScope', 'messageAlertEngine', function ($scope, $http, $filter, $rootScope, messageAlertEngine) {
    //MasterData/Organization/GetGroups
    $scope.Groups = [];

    $http.get(rootDir + "/MasterDataNew/GetAllGroups").then(function (value) {
        for (var i = 0; i < value.data.length ; i++) {
            if (value.data[i] != null) {
                value.data[i].LastModifiedDate = $scope.ConvertDateFormat(value.data[i].LastModifiedDate);
            }
        }
        $scope.Groups = value.data;
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

    
    
    $scope.tempGroup = {};

    //------------ gets the template to ng-include for a table row / item -------------------
    $scope.getTemplate = function (group) {
        if (group.GroupID === $scope.tempGroup.GroupID) return 'editGroup';
        else return 'displayGroup';
    };

    //-------------------- Edit Group ----------
    $scope.editGroup = function (group) {
        $scope.tempGroup = angular.copy(group);
        $scope.disableAdd = true;
    };

    //------------------- Add Group ---------------------
    $scope.addGroup = function (group) {
        $scope.disableEdit = true;
        $scope.disableAdd = true;
        var temp = {
            GroupID: 0,
            Name: "",
            Description: "",
            Status: "Active",
            LastModifiedDate: new Date()
        };
        $scope.Groups.splice(0, 0, angular.copy(temp));
        $scope.tempGroup = angular.copy(temp);
    };

    //------------------- Save Group ---------------------
    $scope.saveGroup = function (idx) {
        //$scope.tempGroup.LastModifiedDate = "02/03/2015"

        var addData = {
            GroupID: 0,
            Name: $scope.tempGroup.Name,
            Description: $scope.tempGroup.Description,
            StatusType: 1
        }

        var isExist = true;
        var isExist1 = true;

        for (var i = 0; i < $scope.Groups.length; i++) {

            if (addData.Name && $scope.Groups[i].Name.replace(" ", "").toLowerCase() == addData.Name.replace(" ", "").toLowerCase()) {

                isExist = false;
                $scope.existErr = "The Name is Exist";
                break;
            }
            
        }
        if (!addData.Name) { $scope.error = "Please Enter the Name"; }

        if (addData.Name && isExist) {
            $http.post(rootDir + '/MasterDataNew/AddGroups', addData).
                success(function (data, status, headers, config) {
                    //----------- success message -----------
                    if (data.status == "true") {
                        messageAlertEngine.callAlertMessage("Group", "New Group Details Added Successfully !!!!", "success", true);
                        data.organizationGroupDetails.LastModifiedDate = $scope.ConvertDateFormat(data.organizationGroupDetails.LastModifiedDate);
                        $scope.Groups[idx] = angular.copy(data.organizationGroupDetails);                        
                        $scope.reset();
                        $scope.error = "";
                        $scope.existErr = "";
                        
                    }
                    else {
                        messageAlertEngine.callAlertMessage("GroupError", "Sorry Unable To Add Group !!!!", "danger", true);
                        $scope.Groups.splice(idx, 1);
                    }
                }).
                error(function (data, status, headers, config) {
                    //----------- error message -----------
                    messageAlertEngine.callAlertMessage("GroupError", "Sorry Unable To Add Group !!!!", "danger", true);
                    $scope.Groups.splice(idx, 1);
                });
        }


        
    };

    //------------------- Update Group ---------------------
    $scope.updateGroup = function (idx) {
        //$scope.tempGroup.LastModifiedDate = "02/03/2015"

        var updateData = {
            GroupID: $scope.tempGroup.GroupID,
            Name: $scope.tempGroup.Name,
            Description: $scope.tempGroup.Description,
            StatusType: 1
        }

        var isExist = true;
        var isExist1 = true;

        for (var i = 0; i < $scope.Groups.length; i++) {

            if (updateData.Name && $scope.Groups[i].GroupID != updateData.GroupID && $scope.Groups[i].Name.replace(" ", "").toLowerCase() == updateData.Name.replace(" ", "").toLowerCase()) {

                isExist = false;
                $scope.existErr = "The Name is Exist";
                break;
            }

        }
        if (!updateData.Name) { $scope.error = "Please Enter the Name"; }

        //console.log("Updating Groups");
        if (updateData.Name && isExist) {
            $http.post(rootDir + '/MasterDataNew/UpdateGroups', updateData).
                success(function (data, status, headers, config) {
                    //----------- success message -----------
                    if (data.status == "true") {
                        messageAlertEngine.callAlertMessage("Group", "New Group Details Updated Successfully !!!!", "success", true);
                        data.organizationGroupDetails.LastModifiedDate = $scope.ConvertDateFormat(data.organizationGroupDetails.LastModifiedDate);
                        $scope.Groups[idx] = angular.copy(data.organizationGroupDetails);                        
                        $scope.reset();
                        $scope.error = "";
                        $scope.existErr = "";

                    }
                }).
                error(function (data, status, headers, config) {
                    //----------- error message -----------
                    messageAlertEngine.callAlertMessage("GroupError", "Sorry Unable To update Group !!!!", "danger", true);                    
                });
        }
    };

    //----------------- Group new add cancel ---------------
    $scope.cancelAdd = function () {
        $scope.Groups.splice(0, 1);
        $scope.tempGroup = {};
        $scope.disableEdit = false;
        $scope.disableAdd = false;
        $scope.error = "";
        $scope.existErr = "";
    };

    //-------------------- Reset Group ----------------------
    $scope.reset = function () {
        $scope.tempGroup = {};
        $scope.disableEdit = false;
        $scope.disableAdd = false;
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
        if ($scope.Groups) {
            for (startIndex; startIndex <= endIndex ; startIndex++) {
                if ($scope.Groups[startIndex]) {
                    $scope.CurrentPage.push($scope.Groups[startIndex]);
                } else {
                    break;
                }
            }
        }
        //console.log($scope.CurrentPageProviders);
    });
    //-------------- License Scope Watch ---------------------
    $scope.$watchCollection('Groups', function (newValue, oldValue) {
        if (newValue) {
            $scope.bigTotalItems = newValue.length;

            $scope.CurrentPage = [];
            $scope.bigCurrentPage = 1;

            var startIndex = ($scope.bigCurrentPage - 1) * 10;
            var endIndex = startIndex + 9;

            for (startIndex; startIndex <= endIndex ; startIndex++) {
                if ($scope.Groups[startIndex]) {
                    $scope.CurrentPage.push($scope.Groups[startIndex]);
                } else {
                    break;
                }
            }
            //console.log($scope.CurrentPageProviders);
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
