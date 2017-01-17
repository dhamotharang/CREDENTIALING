//--------------------- Angular Module ----------------------
var masterDataPOSQ = angular.module("masterDataPOSQ", ['ui.bootstrap']);

//Service for getting master data
masterDataPOSQ.service('masterDataService', ['$http', '$q', function ($http, $q) {

    this.getMasterData = function (URL) {
        return $http.get(URL).then(function (value) { return value.data; });
    };

}]);

masterDataPOSQ.service('messageAlertEngine', ['$rootScope', '$timeout', function ($rootScope, $timeout) {

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
masterDataPOSQ.controller('masterDataPOSQController', ['$scope', '$http', '$filter', '$rootScope', 'masterDataService', 'messageAlertEngine', function ($scope, $http, $filter, $rootScope, masterDataService, messageAlertEngine) {

    $scope.PracticeOpenStatusQuestions = [];

    $http.get(rootDir + "/MasterDataNew/GetAllPracticeOpenStatusQuestions").then(function (value) {
        //console.log("PracticeOpenStatusQuestions");
        //console.log(value);

        for (var i = 0; i < value.data.length ; i++) {
            if (value.data[i] != null) {
                value.data[i].LastModifiedDate = $scope.ConvertDateFormat(value.data[i].LastModifiedDate);
            }
        }

        $scope.PracticeOpenStatusQuestions = value.data;
        //console.log($scope.PracticeOpenStatusQuestions);
    });

    //Convert the date from database to normal
    $scope.ConvertDateFormat = function (value) {
        //////console.log(value);
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

    

    $scope.tempPOSQ = {};

    //------------ gets the template to ng-include for a table row / item -------------------
    $scope.getTemplate = function (posq) {
        if (posq.PracticeOpenStatusQuestionID === $scope.tempPOSQ.PracticeOpenStatusQuestionID) return 'editPOSQ';
        else return 'displayPOSQ';
    };

    //-------------------- Edit AdmittingPrivilege ----------
    $scope.editPOSQ = function (posq) {
        $scope.tempPOSQ = angular.copy(posq);
        $scope.disableAdd = true;
    };

    //------------------- Add AdmittingPrivilege ---------------------
    $scope.addPOSQ = function (posq) {
        $scope.disableEdit = true;
        $scope.disableAdd = true;
        var temp = {
            PracticeOpenStatusQuestionID: 0,
            Title: "",
            Status: "Active",
            LastModifiedDate: new Date()
        };
        $scope.PracticeOpenStatusQuestions.splice(0, 0, angular.copy(temp));
        $scope.tempPOSQ = angular.copy(temp);
    };

    //------------------- Save AdmittingPrivilege ---------------------
    $scope.savePOSQ = function (idx) {
        //$scope.tempPOSQ.LastModifiedDate = "02/03/2015"

        var addData = {
            PracticeOpenStatusQuestionID: 0,
            Title: $scope.tempPOSQ.Title,
            StatusType: 1,
        };

        var isExist = true;

        for (var i = 0; i < $scope.PracticeOpenStatusQuestions.length; i++) {

            if (addData.Title && $scope.PracticeOpenStatusQuestions[i].Title.replace(" ", "").toLowerCase() == addData.Title.replace(" ", "").toLowerCase()) {

                isExist = false;
                $scope.existErr = "The Title is Exist";
                break;
            }
        }
        if (!addData.Title) { $scope.error = "Please Enter the Title"; }

        //console.log("Saving PracticeOpenStatusQuestion");
        
        if (addData.Title && isExist) {
            $http.post(rootDir + '/MasterDataNew/AddPracticeOpenStatusQuestions', addData).
                success(function (data, status, headers, config) {
                    //----------- success message -----------
                    if (data.status == "true") {
                        messageAlertEngine.callAlertMessage("PracticeOpenStatusQuestion", "New Practice Open Status Question Details Added Successfully !!!!", "success", true);
                        data.practiceOpenStatusQuestionDetails.LastModifiedDate = $scope.ConvertDateFormat(data.practiceOpenStatusQuestionDetails.LastModifiedDate);
                        $scope.PracticeOpenStatusQuestions[idx] = angular.copy(data.practiceOpenStatusQuestionDetails);
                        $scope.PracticeOpenStatusQuestions[idx].LastModifiedDate = new Date();
                        $scope.reset();
                        $scope.error = "";
                        $scope.existErr = "";
                    }
                    else {
                        messageAlertEngine.callAlertMessage("PracticeOpenStatusQuestionError", "Sorry Unable To Add Practice Open Status Question !!!!", "danger", true);
                        $scope.AdmittingPrivileges.slpice(idx, 1);
                    }
                }).
                error(function (data, status, headers, config) {
                    //----------- error message -----------
                    messageAlertEngine.callAlertMessage("PracticeOpenStatusQuestionError", "Sorry Unable To Add Practice Open Status Question !!!!", "danger", true);
                    $scope.AdmittingPrivileges.slpice(idx, 1);
                });
        }

        
    };

    //------------------- Save AdmittingPrivilege ---------------------
    $scope.updatePOSQ = function (idx) {
        //$scope.tempPOSQ.LastModifiedDate = "02/03/2015"

        var updateData = {
            PracticeOpenStatusQuestionID: $scope.tempPOSQ.PracticeOpenStatusQuestionID,
            Title: $scope.tempPOSQ.Title,
            StatusType: 1,
        };

        var isExist = true;

        for (var i = 0; i < $scope.PracticeOpenStatusQuestions.length; i++) {

            if (updateData.Title && $scope.PracticeOpenStatusQuestions[i].PracticeOpenStatusQuestionID != updateData.PracticeOpenStatusQuestionID && $scope.PracticeOpenStatusQuestions[i].Title.replace(" ", "").toLowerCase() == updateData.Title.replace(" ", "").toLowerCase()) {

                isExist = false;
                $scope.existErr = "The Title is Exist";
                break;
            }
        }
        if (!updateData.Title) { $scope.error = "Please Enter the Title"; }

        //console.log("Saving PracticeOpenStatusQuestion");

        if (updateData.Title && isExist) {
            $http.post(rootDir + '/MasterDataNew/UpdatePracticeOpenStatusQuestions', updateData).
                success(function (data, status, headers, config) {
                    //----------- success message -----------
                    if (data.status == "true") {
                        messageAlertEngine.callAlertMessage("PracticeOpenStatusQuestion", "Practice Open Status Question Details Updated Successfully !!!!", "success", true);
                        data.practiceOpenStatusQuestionDetails.LastModifiedDate = $scope.ConvertDateFormat(data.practiceOpenStatusQuestionDetails.LastModifiedDate);
                        $scope.PracticeOpenStatusQuestions[idx] = angular.copy(data.practiceOpenStatusQuestionDetails);                        
                        $scope.reset();
                        $scope.error = "";
                        $scope.existErr = "";
                    }
                }).
                error(function (data, status, headers, config) {
                    //----------- error message -----------
                    messageAlertEngine.callAlertMessage("PracticeOpenStatusQuestionError", "Sorry Unable To Update Practice Open Status Question !!!!", "danger", true);                    
                });
        }


    };

    //----------------- AdmittingPrivilege new add cancel ---------------
    $scope.cancelAdd = function () {
        $scope.PracticeOpenStatusQuestions.splice(0, 1);
        $scope.tempPOSQ = {};
        $scope.disableEdit = false;
        $scope.disableAdd = false;
        $scope.error = "";
        $scope.existErr = "";
    };

    //-------------------- Reset AdmittingPrivilege ----------------------
    $scope.reset = function () {
        $scope.tempPOSQ = {};
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
        if ($scope.PracticeOpenStatusQuestions) {
            for (startIndex; startIndex <= endIndex ; startIndex++) {
                if ($scope.PracticeOpenStatusQuestions[startIndex]) {
                    $scope.CurrentPage.push($scope.PracticeOpenStatusQuestions[startIndex]);
                } else {
                    break;
                }
            }
        }
        ////console.log($scope.CurrentPageProviders);
    });
    //-------------- License Scope Watch ---------------------
    $scope.$watchCollection('PracticeOpenStatusQuestions', function (newValue, oldValue) {
        if (newValue) {
            $scope.bigTotalItems = newValue.length;

            $scope.CurrentPage = [];
            $scope.bigCurrentPage = 1;

            var startIndex = ($scope.bigCurrentPage - 1) * 10;
            var endIndex = startIndex + 9;

            for (startIndex; startIndex <= endIndex ; startIndex++) {
                if ($scope.PracticeOpenStatusQuestions[startIndex]) {
                    $scope.CurrentPage.push($scope.PracticeOpenStatusQuestions[startIndex]);
                } else {
                    break;
                }
            }
            ////console.log($scope.CurrentPageProviders);
        }
    });
    //------------------- end ------------------

}]);
