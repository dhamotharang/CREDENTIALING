//--------------------- Angular Module ----------------------
var MasterDataProfileSubsection = angular.module("MasterDataProfileSubsection", ['ui.bootstrap']);

//Service for getting master data
MasterDataProfileSubsection.service('masterDataService', ['$http', '$q', function ($http, $q) {

    this.getMasterData = function (URL) {
        return $http.get(URL).then(function (value) { return value.data; });
    };

}]);

MasterDataProfileSubsection.service('messageAlertEngine', ['$rootScope', '$timeout', function ($rootScope, $timeout) {

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
MasterDataProfileSubsection.controller('profilesubsectionccontroller', ['$scope', '$http', '$filter', '$rootScope', 'masterDataService', 'messageAlertEngine', function ($scope, $http, $filter, $rootScope, masterDataService, messageAlertEngine) {

    $scope.ProfileSubSections = [];

    $http.get(rootDir + "/MasterDataNew/GetAllProfileSubSections").success(function (value) {

        try {
            for (var i = 0; i < value.length ; i++) {
                if (value[i] != null) {
                    value[i].LastModifiedDate = ($scope.ConvertDateFormat(value[i].LastModifiedDate)).toString();
                }

            }

            $scope.ProfileSubSections = angular.copy(value);
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


    $scope.tempProfileSubSections = {};
    $scope.ProfileSubSections = [];

    //------------ gets the template to ng-include for a table row / item -------------------
    $scope.getTemplate = function (ProfileSubSections) {
        if (ProfileSubSections.ProfileSubSectionId === $scope.tempProfileSubSections.ProfileSubSectionId)
            return 'editSubsectionName';
        else
            return 'displaySubsections';
    };

    //-------------------- Edit Insuarance Company names ----------
    $scope.editProfileSubSections = function (SubsectionName) {
        $scope.tempProfileSubSections = angular.copy(SubsectionName);
        $scope.disableAdd = true;
    };

    //------------------- Add Insuarance Company Names ---------------------
    $scope.addProfileSubsection = function (profilesubsection) {
        $scope.disableEdit = true;
        $scope.disableAdd = true;
        var Month = new Date().getMonth() + 1;
        var _month = Month < 10 ? '0' + Month : Month;
        var _date = new Date().getDate() < 10 ? '0' + new Date().getDate() : new Date().getDate();
        var _year = new Date().getFullYear();
        var temp = {
            addProfileSubSections: 0,
            SubSectionName: "",
            Status: "Active",
            LastModifiedDate: _month + "-" + _date + "-" + _year,
        };
        $scope.ProfileSubSections.splice(0, 0, angular.copy(temp));
        $scope.tempProfileSubSections = angular.copy(temp);
    };

    //------------------- Save Insuarance Company Names ---------------------
    $scope.saveProfileSubsection = function (idx) {

        var addData = {
            ProfileSubSectionId: 0,
            SubSectionName: $scope.tempProfileSubSections.SubSectionName,
            StatusType: 1,
        };



        var isExist = true;

        for (var i = 0; i < $scope.ProfileSubSections.length; i++) {

            if (addData.SubSectionName && $scope.ProfileSubSections[i].SubSectionName.replace(" ", "").toLowerCase().trim() == addData.SubSectionName.replace(" ", "").toLowerCase().trim()) {

                isExist = false;
                $scope.existErr = "This SubSection already exists";
                break;
            }
        }

        if (!addData.SubSectionName) { $scope.error = "Please Enter the Subsection Name"; }


        if (addData.SubSectionName && isExist) {
            $http.post(rootDir + '/MasterDataNew/AddProfileSubSection', addData).
                success(function (data, status, headers, config) {
                    try {
                        //----------- success message -----------
                        if (data.status == "true") {
                            messageAlertEngine.callAlertMessage("profilesubsection", "Profile SubSection Added Successfully !!!!", "success", true);
                            data.profileSubSection.LastModifiedDate = $scope.ConvertDateFormat(data.profileSubSection.LastModifiedDate);
                            $scope.ProfileSubSections[idx] = angular.copy(data.profileSubSection);
                            $scope.reset();
                            $scope.error = "";
                            $scope.existErr = "";
                        }
                        else {
                            messageAlertEngine.callAlertMessage("profilesubsectionerror", "Sorry Unable To Add new SubSection !!!!", "danger", true);
                            $scope.ProfileSubSections.slpice(idx, 1);
                        }
                    } catch (e) {

                    }
                }).
                error(function (data, status, headers, config) {
                    //----------- error message -----------
                    messageAlertEngine.callAlertMessage("profilesubsectionerror", "Sorry Unable To Add new SubSection !!!!", "danger", true);
                    $scope.ProfileSubSections.slpice(idx, 1);
                });
        }


    };

    //------------------- Update Insuarance Company Names ---------------------
    $scope.updateProfileSubSections = function (idx) {

        var updateData = {
            ProfileSubSectionId: $scope.tempProfileSubSections.ProfileSubSectionId,
            SubSectionName: $scope.tempProfileSubSections.SubSectionName,
            StatusType: 1,
        };

        var isExist = true;

        for (var i = 0; i < $scope.ProfileSubSections.length; i++) {

            if (updateData.SubSectionName && $scope.ProfileSubSections[i].ProfileSubSectionId != updateData.ProfileSubSectionId && $scope.ProfileSubSections[i].SubSectionName.replace(" ", "").toLowerCase() == updateData.SubSectionName.replace(" ", "").toLowerCase()) {

                isExist = false;
                $scope.existErr = "This SubSection already exists";
                break;
            }
        }
        if (!updateData.CompanyName) { $scope.error = "Please Enter the Subsection Name"; }



        if (updateData.CompanyName && isExist) {
            $http.post(rootDir + '/MasterDataNew/UpdateProfileSubSections', updateData).
                success(function (data, status, headers, config) {
                    try {
                        //----------- success message -----------
                        if (data.status == "true") {
                            messageAlertEngine.callAlertMessage("profilesubsection", "SubSection Details Updated Successfully !!!!", "success", true);
                            data.insuaranceCompanyNameDetails.LastModifiedDate = $scope.ConvertDateFormat(data.insuaranceCompanyNameDetails.LastModifiedDate);
                            $scope.ProfileSubSections[idx] = angular.copy(data.insuaranceCompanyNameDetails);
                            $scope.reset();
                            $scope.error = "";
                            $scope.existErr = "";
                        }
                    } catch (e) {

                    }

                }).
                error(function (data, status, headers, config) {
                    //----------- error message -----------
                    messageAlertEngine.callAlertMessage("profilesubsectionerror", "Sorry Unable To Update SubSection Details !!!!", "danger", true);
                });
        }


    };

    //----------------- Insuarance Company Names new add cancel ---------------
    $scope.cancelAdd = function () {
        $scope.ProfileSubSections.splice(0, 1);
        $scope.tempProfileSubSections = {};
        $scope.disableEdit = false;
        $scope.disableAdd = false;
        $scope.error = "";
        $scope.existErr = "";
    };

    //-------------------- Reset Insuarance Company Names ----------------------
    $scope.reset = function () {
        $scope.tempProfileSubSections = {};
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
        if ($scope.ProfileSubSections) {
            for (startIndex; startIndex <= endIndex ; startIndex++) {
                if ($scope.ProfileSubSections[startIndex]) {
                    $scope.CurrentPage.push($scope.ProfileSubSections[startIndex]);
                } else {
                    break;
                }
            }
        }
    });
    //-------------- License Scope Watch ---------------------
    $scope.$watchCollection('ProfileSubSections', function (newValue, oldValue) {
        if (newValue) {
            $scope.bigTotalItems = newValue.length;

            $scope.CurrentPage = [];
            $scope.bigCurrentPage = 1;

            var startIndex = ($scope.bigCurrentPage - 1) * 10;
            var endIndex = startIndex + 9;

            for (startIndex; startIndex <= endIndex ; startIndex++) {
                if ($scope.ProfileSubSections[startIndex]) {
                    $scope.CurrentPage.push($scope.ProfileSubSections[startIndex]);
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
