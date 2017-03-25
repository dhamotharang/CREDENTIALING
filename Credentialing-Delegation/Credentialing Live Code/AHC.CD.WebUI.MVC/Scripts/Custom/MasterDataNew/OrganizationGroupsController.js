//--------------------- Angular Module ----------------------
var masterDataOrganizationGroup = angular.module("masterDataOrganizationGroup", ["ahc.cd.util", 'ui.bootstrap']);

masterDataOrganizationGroup.service('messageAlertEngine', ['$rootScope', '$timeout', function ($rootScope, $timeout) {

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
masterDataOrganizationGroup.controller('masterDataOrganizationGroupController', ['$scope', '$http', '$filter', '$rootScope', 'messageAlertEngine', function ($scope, $http, $filter, $rootScope, messageAlertEngine) {
    
    $scope.OrganizationGroups = [];

    $http.get(rootDir + "/MasterDataNew/GetAllOrganizationGroups").then(function (value) {
        $scope.OrganizationGroups = value.data;
    });


    $scope.tempGroup = {};

    //------------ gets the template to ng-include for a table row / item -------------------
    $scope.getTemplate = function (group) {
        if (group.OrganizationGroupID === $scope.tempGroup.OrganizationGroupID) return 'editOrganizationGroup';
        else return 'displayOrganizationGroup';
    };

    //-------------------- Edit Organization Group ----------
    $scope.editOrganizationGroup = function (group) {
        $scope.tempGroup = angular.copy(group);
        $scope.disableAdd = true;
    };

    //------------------- Add Organization Group ---------------------
    $scope.addOrganizationGroup = function (group) {
        $scope.disableEdit = true;
        $scope.disableAdd = true;
        var temp = {
            OrganizationGroupID: 0,
            GroupName: "",
            GroupDescription: "",
            Status: "Active",
        };
        $scope.OrganizationGroups.splice(0, 0, angular.copy(temp));
        $scope.tempGroup = angular.copy(temp);
    };

    //------------------- Save Group ---------------------
    $scope.saveOrganizationGroup = function (idx) {
        var addData = {
            OrganizationGroupID: 0,
            GroupName: $scope.tempGroup.GroupName,
            GroupDescription: $scope.tempGroup.GroupDescription,
            StatusType: 1
        }

        var isExist = true;
        var isExist1 = true;

        for (var i = 0; i < $scope.OrganizationGroups.length; i++) {

            if (addData.GroupName && $scope.OrganizationGroups[i].GroupName.replace(" ", "").toLowerCase() == addData.GroupName.replace(" ", "").toLowerCase()) {

                isExist = false;
                $scope.existErr = "The GroupName is Exist";
                break;
            }

        }
        if (!addData.GroupName) { $scope.error = "Please Enter the GroupName"; }

        if (addData.GroupName && isExist) {
            $http.post(rootDir + '/MasterDataNew/AddOrganizationGroups', addData).
                success(function (data, status, headers, config) {
                    try {
                        //----------- success message -----------
                        if (data.status == "true") {
                            messageAlertEngine.callAlertMessage("Group", "New Group Details Added Successfully !!!!", "success", true);
                            //data.organizationGroupDetails.LastModifiedDate = $scope.ConvertDateFormat(data.organizationGroupDetails.LastModifiedDate);
                            $scope.OrganizationGroups[idx] = angular.copy(data.organizationGroupDetails);
                            $scope.reset();
                            $scope.error = "";
                            $scope.existErr = "";

                        }
                        else {
                            messageAlertEngine.callAlertMessage("GroupError", "Sorry Unable To Add Group !!!!", "danger", true);
                            $scope.OrganizationGroups.splice(idx, 1);
                        }
                    } catch (e) {

                    }
                }).
                error(function (data, status, headers, config) {
                    //----------- error message -----------
                    messageAlertEngine.callAlertMessage("GroupError", "Sorry Unable To Add Group !!!!", "danger", true);
                    $scope.OrganizationGroups.splice(idx, 1);
                });
        }



    };

    //------------------- Update Group ---------------------
    $scope.updateOrganizationGroup = function (idx) {
        //$scope.tempGroup.LastModifiedDate = "02/03/2015"

        var updateData = {
            OrganizationGroupID: $scope.tempGroup.OrganizationGroupID,
            GroupName: $scope.tempGroup.GroupName,
            GroupDescription: $scope.tempGroup.GroupDescription,
            StatusType: 1
        }

        var isExist = true;
        var isExist1 = true;

        for (var i = 0; i < $scope.OrganizationGroups.length; i++) {

            if (updateData.GroupName && $scope.OrganizationGroups[i].OrganizationGroupID != updateData.OrganizationGroupID && $scope.OrganizationGroups[i].GroupName.replace(" ", "").toLowerCase() == updateData.GroupName.replace(" ", "").toLowerCase()) {

                isExist = false;
                $scope.existErr = "The GroupName is Exist";
                break;
            }

        }
        if (!updateData.GroupName) { $scope.error = "Please Enter the GroupName"; }

        if (updateData.GroupName && isExist) {
            $http.post(rootDir + '/MasterDataNew/UpdateOrganizationGroups', updateData).
                success(function (data, status, headers, config) {
                    try {
                        //----------- success message -----------
                        if (data.status == "true") {
                            messageAlertEngine.callAlertMessage("Group", "New Group Details Updated Successfully !!!!", "success", true);
                            //data.organizationGroupDetails.LastModifiedDate = $scope.ConvertDateFormat(data.organizationGroupDetails.LastModifiedDate);
                            $scope.OrganizationGroups[idx] = angular.copy(data.organizationGroupDetails);
                            $scope.reset();
                            $scope.error = "";
                            $scope.existErr = "";

                        }
                    } catch (e) {

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
        $scope.OrganizationGroups.splice(0, 1);
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
        if ($scope.OrganizationGroups) {
            for (startIndex; startIndex <= endIndex ; startIndex++) {
                if ($scope.OrganizationGroups[startIndex]) {
                    $scope.CurrentPage.push($scope.OrganizationGroups[startIndex]);
                } else {
                    break;
                }
            }
        }
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
                if ($scope.OrganizationGroups[startIndex]) {
                    $scope.CurrentPage.push($scope.OrganizationGroups[startIndex]);
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
