//--------------------- Angular Module ----------------------
var masterDataPAQ = angular.module("masterDataPSQ", ['ui.bootstrap']);

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

//=========================== Controller declaration ==========================
masterDataPAQ.controller('masterDataPSQController', ['$scope', '$http', '$filter', '$rootScope', 'masterDataService', 'messageAlertEngine', function ($scope, $http, $filter, $rootScope, masterDataService, messageAlertEngine) {

    $scope.PracticeServiceQuestions = [];

    $http.get(rootDir + "/MasterDataNew/GetAllPracticeServiceQuestions").success(function (value) {
        
        try {
            for (var i = 0; i < value.length ; i++) {
                if (value[i] != null) {
                    value[i].LastModifiedDate = ($scope.ConvertDateFormat(value[i].LastModifiedDate)).toString();
                }

            }

            $scope.PracticeServiceQuestions = angular.copy(value);
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

    

    $scope.tempPSQ = {};

    //------------ gets the template to ng-include for a table row / item -------------------
    $scope.getTemplate = function (psq) {
        if (psq.FacilityServiceQuestionID === $scope.tempPSQ.FacilityServiceQuestionID) return 'editPSQ';
        else return 'displayPSQ';
    };

    //-------------------- Edit AdmittingPrivilege ----------
    $scope.editPSQ = function (psq) {
        $scope.tempPSQ = angular.copy(psq);
        $scope.disableAdd = true;
    };

    //------------------- Add AdmittingPrivilege ---------------------
    $scope.addPSQ = function (psq) {
        $scope.disableEdit = true;
        $scope.disableAdd = true;
        var Month = new Date().getMonth() + 1;
        var _month = Month < 10 ? '0' + Month : Month;
        var _date = new Date().getDate() < 10 ? '0' + new Date().getDate() : new Date().getDate();
        var _year = new Date().getFullYear();
        var temp = {
            FacilityServiceQuestionID: 0,
            Title: "",
            ShortTitle:"",
            Status: "Active",
            LastModifiedDate: _month + "-" + _date + "-" + _year
        };
        $scope.PracticeServiceQuestions.splice(0, 0, angular.copy(temp));
        $scope.tempPSQ = angular.copy(temp);
    };

    //------------------- Save AdmittingPrivilege ---------------------
    $scope.savePSQ = function (idx) {
        //$scope.tempPSQ.LastModifiedDate = "02/03/2015"

        var addData = {
            FacilityServiceQuestionID: 0,
            Title: $scope.tempPSQ.Title,
            ShortTitle: $scope.tempPSQ.ShortTitle,
            StatusType: 1,
        };

        var isExist = true;

        for (var i = 0; i < $scope.PracticeServiceQuestions.length; i++) {

            if (addData.ShortTitle && $scope.PracticeServiceQuestions[i].ShortTitle.replace(" ", "").toLowerCase() == addData.ShortTitle.replace(" ", "").toLowerCase()) {

                isExist = false;
                $scope.existErr = "The Short Title is Exist";
                break;
            }
        }
        if (!addData.ShortTitle) { $scope.error = "Please Enter the Short Title"; }

        
        if (addData.ShortTitle && isExist) {
            $http.post(rootDir + '/MasterDataNew/AddPracticeServiceQuestions', addData).
                success(function (data, status, headers, config) {
                    try {
                        //----------- success message -----------
                        if (data.status == "true") {
                            messageAlertEngine.callAlertMessage("PracticeServiceQuestion", "New Practice Service Question Details Added Successfully !!!!", "success", true);
                            data.practiceServiceQuestionDetails.LastModifiedDate = $scope.ConvertDateFormat(data.practiceServiceQuestionDetails.LastModifiedDate);
                            $scope.PracticeServiceQuestions[idx] = angular.copy(data.practiceServiceQuestionDetails);
                            $scope.reset();
                            $scope.error = "";
                            $scope.existErr = "";
                        }
                        else {
                            messageAlertEngine.callAlertMessage("PracticeServiceQuestionError", "Sorry Unable To Add Practice Service Question !!!!", "danger", true);
                            $scope.PracticeServiceQuestions.slpice(idx, 1);
                        }
                    } catch (e) {
                      
                    }
                }).
                error(function (data, status, headers, config) {
                    //----------- error message -----------
                    messageAlertEngine.callAlertMessage("PracticeServiceQuestionError", "Sorry Unable To Add Practice Service Question !!!!", "danger", true);
                    $scope.PracticeServiceQuestions.slpice(idx, 1);
                });
        }

        
    };

    //------------------- Update AdmittingPrivilege ---------------------
    $scope.updatePSQ = function (idx) {
        //$scope.tempPSQ.LastModifiedDate = "02/03/2015"

        var updateData = {
            FacilityServiceQuestionID: $scope.tempPSQ.FacilityServiceQuestionID,
            Title: $scope.tempPSQ.Title,
            ShortTitle: $scope.tempPSQ.ShortTitle,
            StatusType: 1,
        };

        var isExist = true;

        for (var i = 0; i < $scope.PracticeServiceQuestions.length; i++) {

            if (updateData.ShortTitle && $scope.PracticeServiceQuestions[i].FacilityServiceQuestionID != updateData.FacilityServiceQuestionID && $scope.PracticeServiceQuestions[i].ShortTitle.replace(" ", "").toLowerCase() == updateData.ShortTitle.replace(" ", "").toLowerCase()) {

                isExist = false;
                $scope.existErr = "The Title is Exist";
                break;
            }
        }
        if (!updateData.ShortTitle) { $scope.error = "Please Enter the Short Title"; }


        if (updateData.ShortTitle && isExist) {
            $http.post(rootDir + '/MasterDataNew/updatePracticeServiceQuestions', updateData).
                success(function (data, status, headers, config) {
                    try {
                        //----------- success message -----------
                        if (data.status == "true") {
                            messageAlertEngine.callAlertMessage("PracticeServiceQuestion", "Practice Service Question Details Updated Successfully !!!!", "success", true);
                            data.practiceServiceQuestionDetails.LastModifiedDate = $scope.ConvertDateFormat(data.practiceServiceQuestionDetails.LastModifiedDate);
                            $scope.PracticeServiceQuestions[idx] = angular.copy(data.practiceServiceQuestionDetails);
                            $scope.reset();
                            $scope.error = "";
                            $scope.existErr = "";
                        }
                    } catch (e) {
                       
                    }
                }).
                error(function (data, status, headers, config) {
                    //----------- error message -----------
                    messageAlertEngine.callAlertMessage("PracticeServiceQuestionError", "Sorry Unable To Update Practice Service Question !!!!", "danger", true);                   
                });
        }


    };

    //----------------- AdmittingPrivilege new add cancel ---------------
    $scope.cancelAdd = function () {
        $scope.PracticeServiceQuestions.splice(0, 1);
        $scope.tempPSQ = {};
        $scope.disableEdit = false;
        $scope.disableAdd = false;
        $scope.error = "";
        $scope.existErr = "";
    };

    //-------------------- Reset AdmittingPrivilege ----------------------
    $scope.reset = function () {
        $scope.tempPSQ = {};
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
        if ($scope.PracticeServiceQuestions) {
            for (startIndex; startIndex <= endIndex ; startIndex++) {
                if ($scope.PracticeServiceQuestions[startIndex]) {
                    $scope.CurrentPage.push($scope.PracticeServiceQuestions[startIndex]);
                } else {
                    break;
                }
            }
        }
    });
    //-------------- License Scope Watch ---------------------
    $scope.$watchCollection('PracticeServiceQuestions', function (newValue, oldValue) {
        if (newValue) {
            $scope.bigTotalItems = newValue.length;

            $scope.CurrentPage = [];
            $scope.bigCurrentPage = 1;

            var startIndex = ($scope.bigCurrentPage - 1) * 10;
            var endIndex = startIndex + 9;

            for (startIndex; startIndex <= endIndex ; startIndex++) {
                if ($scope.PracticeServiceQuestions[startIndex]) {
                    $scope.CurrentPage.push($scope.PracticeServiceQuestions[startIndex]);
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
