//--------------------- Angular Module ----------------------
var masterDataPAQ = angular.module("masterDataPAQ", ['ui.bootstrap']);

//Service for getting master data
masterDataPAQ.service('masterDataService', ['$http', '$q', function ($http, $q) {

    this.getMasterData = function (URL) {
        return $http.get(URL).then(function (value) { return value.data; });
    };

}]);

masterDataPAQ.service('messageAlertEngine', ['$rootScope', '$timeout', function ($rootScope, $timeout) {

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

///=========================== Controller declaration ==========================
masterDataPAQ.controller('masterDataPAQController', ['$scope', '$http', '$filter', '$rootScope', 'masterDataService', 'messageAlertEngine', function ($scope, $http, $filter, $rootScope, masterDataService, messageAlertEngine) {

    $scope.PracticeAccessibilityQuestions = [];

    $http.get(rootDir + "/MasterDataNew/GetAllPracticeAccessibilityQuestions").then(function (value) {
        
        for (var i = 0; i < value.data.length ; i++) {
            if (value.data[i] != null) {
                value.data[i].LastModifiedDate = $scope.ConvertDateFormat(value.data[i].LastModifiedDate);
            }
        }

        $scope.PracticeAccessibilityQuestions = value.data;
    });

    //Convert the date from database to normal
    $scope.ConvertDateFormat = function (value) {
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

    $scope.tempPAQ = {};

    //------------ gets the template to ng-include for a table row / item -------------------
    $scope.getTemplate = function (paq) {
        if (paq.FacilityAccessibilityQuestionId === $scope.tempPAQ.FacilityAccessibilityQuestionId) return 'editPAQ';
        else return 'displayPAQ';
    };

    //-------------------- Edit AdmittingPrivilege ----------
    $scope.editPAQ = function (paq) {
        $scope.tempPAQ = angular.copy(paq);
        $scope.disableAdd = true;
    };

    //------------------- Add AdmittingPrivilege ---------------------
    $scope.addPAQ = function (paq) {
        $scope.disableEdit = true;
        $scope.disableAdd = true;
        var temp = {
            FacilityAccessibilityQuestionId: 0,
            Title: "",
            ShortTitle: "",
            Status: "Active",
            LastModifiedDate: new Date()
        };
        $scope.PracticeAccessibilityQuestions.splice(0, 0, angular.copy(temp));
        $scope.tempPAQ = angular.copy(temp);
    };

    //------------------- Save AdmittingPrivilege ---------------------
    $scope.savePAQ = function (idx) {
        //$scope.tempPAQ.LastModifiedDate = "02/03/2015"

        var addData = {
            FacilityAccessibilityQuestionId: 0,
            Title: $scope.tempPAQ.Title,
            ShortTitle: $scope.tempPAQ.ShortTitle,
            StatusType: 1,
        };

        var isExist = true;

        for (var i = 0; i < $scope.PracticeAccessibilityQuestions.length; i++) {

            if (addData.ShortTitle && $scope.PracticeAccessibilityQuestions[i].ShortTitle.replace(" ", "").toLowerCase() == addData.ShortTitle.replace(" ", "").toLowerCase()) {

                isExist = false;
                $scope.existErr = "The Title is Exist";
                break;
            }
        }
        //if (!addData.Title) { $scope.error = "Please Enter the Title"; }
        if (!addData.ShortTitle) { $scope.error = "Please Enter the Short Title"; }
       
        if (addData.ShortTitle && isExist) {
            $http.post(rootDir + '/MasterDataNew/AddPracticeAccessibilityQuestions', addData).
                success(function (data, status, headers, config) {
                    try {
                        //----------- success message -----------
                        if (data.status == "true") {
                            messageAlertEngine.callAlertMessage("PracticeAccessibilityQuestion", "New Practice Accessibility Question Details Added Successfully !!!!", "success", true);
                            data.practiceAccessibilityQuestionDetails.LastModifiedDate = $scope.ConvertDateFormat(data.practiceAccessibilityQuestionDetails.LastModifiedDate);
                            $scope.PracticeAccessibilityQuestions[idx] = angular.copy(data.practiceAccessibilityQuestionDetails);
                            $scope.reset();
                            $scope.error = "";
                            $scope.existErr = "";
                        }
                        else {
                            messageAlertEngine.callAlertMessage("PracticeAccessibilityQuestionError", "Sorry Unable To Add Practice Accessibility Question !!!!", "danger", true);
                            $scope.PracticeAccessibilityQuestions.slpice(idx, 1);
                        }
                    } catch (e) {
                       
                    }
                }).
                error(function (data, status, headers, config) {
                    //----------- error message -----------
                    messageAlertEngine.callAlertMessage("PracticeAccessibilityQuestionError", "Sorry Unable To Add Practice Accessibility Question !!!!", "danger", true);
                    $scope.PracticeAccessibilityQuestions.slpice(idx, 1);
                });
        }

        
    };

    //------------------- Save AdmittingPrivilege ---------------------
    $scope.updatePAQ = function (idx) {
        //$scope.tempPAQ.LastModifiedDate = "02/03/2015"

        var updateData = {
            FacilityAccessibilityQuestionId: $scope.tempPAQ.FacilityAccessibilityQuestionId,
            Title: $scope.tempPAQ.Title,
            ShortTitle: $scope.tempPAQ.ShortTitle,
            StatusType: 1,
        };

        var isExist = true;

        for (var i = 0; i < $scope.PracticeAccessibilityQuestions.length; i++) {

            if (updateData.ShortTitle && $scope.PracticeAccessibilityQuestions[i].FacilityAccessibilityQuestionId != updateData.FacilityAccessibilityQuestionId && $scope.PracticeAccessibilityQuestions[i].ShortTitle.replace(" ", "").toLowerCase() == updateData.ShortTitle.replace(" ", "").toLowerCase()) {

                isExist = false;
                $scope.existErr = "The Title is Exist";
                break;
            }
        }
        //if (!updateData.Title) { $scope.error = "Please Enter the Title"; }
        if (!updateData.ShortTitle) { $scope.error = "Please Enter the Short Title"; }

        if (updateData.ShortTitle && isExist) {
            $http.post(rootDir + '/MasterDataNew/UpdatePracticeAccessibilityQuestions', updateData).
                success(function (data, status, headers, config) {
                    try {
                        //----------- success message -----------
                        if (data.status == "true") {
                            messageAlertEngine.callAlertMessage("PracticeAccessibilityQuestion", "Practice Accessibility Question Details Updated Successfully !!!!", "success", true);
                            data.practiceAccessibilityQuestionDetails.LastModifiedDate = $scope.ConvertDateFormat(data.practiceAccessibilityQuestionDetails.LastModifiedDate);
                            $scope.PracticeAccessibilityQuestions[idx] = angular.copy(data.practiceAccessibilityQuestionDetails);
                            $scope.reset();
                            $scope.error = "";
                            $scope.existErr = "";
                        }
                    } catch (e) {
                      
                    }
                }).
                error(function (data, status, headers, config) {
                    //----------- error message -----------
                    messageAlertEngine.callAlertMessage("PracticeAccessibilityQuestionError", "Sorry Unable To Update Practice Accessibility Question !!!!", "danger", true);
                    
                });
        }


    };

    //----------------- AdmittingPrivilege new add cancel ---------------
    $scope.cancelAdd = function () {
        $scope.PracticeAccessibilityQuestions.splice(0, 1);
        $scope.tempPAQ = {};
        $scope.disableEdit = false;
        $scope.disableAdd = false;
        $scope.error = "";
        $scope.existErr = "";
    };

    //-------------------- Reset AdmittingPrivilege ----------------------
    $scope.reset = function () {
        $scope.tempPAQ = {};
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
        if ($scope.PracticeAccessibilityQuestions) {
            for (startIndex; startIndex <= endIndex ; startIndex++) {
                if ($scope.PracticeAccessibilityQuestions[startIndex]) {
                    $scope.CurrentPage.push($scope.PracticeAccessibilityQuestions[startIndex]);
                } else {
                    break;
                }
            }
        }
    });
    //-------------- License Scope Watch ---------------------
    $scope.$watchCollection('PracticeAccessibilityQuestions', function (newValue, oldValue) {
        if (newValue) {
            $scope.bigTotalItems = newValue.length;

            $scope.CurrentPage = [];
            $scope.bigCurrentPage = 1;

            var startIndex = ($scope.bigCurrentPage - 1) * 10;
            var endIndex = startIndex + 9;

            for (startIndex; startIndex <= endIndex ; startIndex++) {
                if ($scope.PracticeAccessibilityQuestions[startIndex]) {
                    $scope.CurrentPage.push($scope.PracticeAccessibilityQuestions[startIndex]);
                } else {
                    break;
                }
            }
        }
    });
    //------------------- end ------------------
}]);
