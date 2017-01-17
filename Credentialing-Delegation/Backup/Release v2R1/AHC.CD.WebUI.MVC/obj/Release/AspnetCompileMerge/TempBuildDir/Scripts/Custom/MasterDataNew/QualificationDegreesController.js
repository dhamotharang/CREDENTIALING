//--------------------- Angular Module ----------------------
var masterDataQD = angular.module("masterDataQualificationDegrees", ['ui.bootstrap']);

//Service for getting master data
masterDataQD.service('masterDataService', ['$http', '$q', function ($http, $q) {

    this.getMasterData = function (URL) {
        return $http.get(URL).then(function (value) { return value.data; });
    };

}]);

masterDataQD.service('messageAlertEngine', ['$rootScope', '$timeout', function ($rootScope, $timeout) {

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
masterDataQD.controller('masterDataQualificationDegreesController', ['$scope', '$http', '$filter', '$rootScope', 'masterDataService', 'messageAlertEngine', function ($scope, $http, $filter, $rootScope, masterDataService, messageAlertEngine) {

    $scope.QualificationDegrees = [];

    $http.get("/MasterDataNew/GetAllQualificationDegrees").then(function (value) {
        console.log("QualificationDegrees");
        console.log(value);

        for (var i = 0; i < value.data.length ; i++) {
            if (value.data[i] != null) {
                value.data[i].LastModifiedDate = $scope.ConvertDateFormat(value.data[i].LastModifiedDate);
            }
        }

        $scope.QualificationDegrees = value.data;
        console.log($scope.QualificationDegrees);
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

    

    $scope.tempQD = {};

    //------------ gets the template to ng-include for a table row / item -------------------
    $scope.getTemplate = function (qd) {
        if (qd.QualificationDegreeID === $scope.tempQD.QualificationDegreeID) return 'editQD';
        else return 'displayQD';
    };

    //-------------------- Edit AdmittingPrivilege ----------
    $scope.editQD = function (qd) {
        $scope.tempQD = angular.copy(qd);
        $scope.disableAdd = true;
    };

    //------------------- Add AdmittingPrivilege ---------------------
    $scope.addQD = function (qd) {
        $scope.disableEdit = true;
        $scope.disableAdd = true;
        var temp = {
            QualificationDegreeID: 0,
            Title: "",
            Status: "Active",
            LastModifiedDate: new Date()
        };
        $scope.QualificationDegrees.splice(0, 0, angular.copy(temp));
        $scope.tempQD = angular.copy(temp);
    };

    //------------------- Save AdmittingPrivilege ---------------------
    $scope.saveQD = function (idx) {
        //$scope.tempQD.LastModifiedDate = "02/03/2015"
        var addData = {
            QualificationDegreeID: 0,
            Title: $scope.tempQD.Title,
            StatusType: 1            
        };
        var isExist = true;

        for (var i = 0; i < $scope.QualificationDegrees.length; i++) {

            if (addData.Title && $scope.QualificationDegrees[i].Title.replace(" ", "").toLowerCase() == addData.Title.replace(" ", "").toLowerCase()) {

                isExist = false;
                $scope.existErr = "The Title is Exist";
                break;
            }
        }
        if (!addData.Title) { $scope.error = "Please Enter the Title"; }
        console.log("Saving qd");
        if (addData.Title!="" && isExist){
        $http.post('/MasterDataNew/AddQualificationDegrees', addData).
            success(function (data, status, headers, config) {
                //----------- success message -----------
                if (data.status == "true") {
                    messageAlertEngine.callAlertMessage("QualificationDegree", "New QualificationDegree Details Added Successfully !!!!", "success", true);
                    data.qualificationDegreeDetails.LastModifiedDate = $scope.ConvertDateFormat(data.qualificationDegreeDetails.LastModifiedDate);
                    $scope.QualificationDegrees[idx] = angular.copy(data.qualificationDegreeDetails);                    
                    $scope.reset();
                    $scope.error = "";
                    $scope.existErr = "";
                }
                else {
                    messageAlertEngine.callAlertMessage("QualificationDegreeError", "Sorry Unable To Add Qualification Degree !!!!", "danger", true);
                    $scope.QualificationDegrees.splice(idx, 1);
                }
            }).
            error(function (data, status, headers, config) {
                //----------- error message -----------
                messageAlertEngine.callAlertMessage("QualificationDegreeError", "Sorry Unable To Add Qualification Degree !!!!", "danger", true);
                $scope.QualificationDegrees.splice(idx, 1);
            });      

        }
    };

    //------------------- Update AdmittingPrivilege ---------------------
    $scope.updateQD = function (idx) {
        //$scope.tempQD.LastModifiedDate = "02/03/2015"
        var updateData = {
            QualificationDegreeID: $scope.tempQD.QualificationDegreeID,
            Title: $scope.tempQD.Title,
            StatusType: 1
        };
        var isExist = true;

        for (var i = 0; i < $scope.QualificationDegrees.length; i++) {

            if (updateData.Title && $scope.QualificationDegrees[i].QualificationDegreeID != updateData.QualificationDegreeID && $scope.QualificationDegrees[i].Title.replace(" ", "").toLowerCase() == updateData.Title.replace(" ", "").toLowerCase()) {

                isExist = false;
                $scope.existErr = "The Title is Exist";
                break;
            }
        }
        if (!updateData.Title) { $scope.error = "Please Enter the Title"; }
        console.log("Updating qd");
        if (updateData && isExist) {
            $http.post('/MasterDataNew/UpdateQualificationDegrees', updateData).
                success(function (data, status, headers, config) {
                    //----------- success message -----------
                    if (data.status == "true") {
                        messageAlertEngine.callAlertMessage("QualificationDegree", "QualificationDegree Details Updated Successfully !!!!", "success", true);
                        data.qualificationDegreeDetails.LastModifiedDate = $scope.ConvertDateFormat(data.qualificationDegreeDetails.LastModifiedDate);
                        $scope.QualificationDegrees[idx] = angular.copy(data.qualificationDegreeDetails);                        
                        $scope.reset();
                        $scope.error = "";
                        $scope.existErr = "";
                    }
                }).
                error(function (data, status, headers, config) {
                    //----------- error message -----------
                    messageAlertEngine.callAlertMessage("QualificationDegreeError", "Sorry Unable To Update Qualification Degree !!!!", "danger", true);
                });
            

            
        }
    };

    //----------------- AdmittingPrivilege new add cancel ---------------
    $scope.cancelAdd = function () {
        $scope.QualificationDegrees.splice(0, 1);
        $scope.tempQD = {};
        $scope.disableEdit = false;
        $scope.disableAdd = false;
        $scope.error = "";
        $scope.existErr = "";
    };

    //-------------------- Reset AdmittingPrivilege ----------------------
    $scope.reset = function () {
        $scope.tempQD = {};
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
        if ($scope.QualificationDegrees) {
            for (startIndex; startIndex <= endIndex ; startIndex++) {
                if ($scope.QualificationDegrees[startIndex]) {
                    $scope.CurrentPage.push($scope.QualificationDegrees[startIndex]);
                } else {
                    break;
                }
            }
        }
        //console.log($scope.CurrentPageProviders);
    });
    //-------------- License Scope Watch ---------------------
    $scope.$watchCollection('QualificationDegrees', function (newValue, oldValue) {
        if (newValue) {
            $scope.bigTotalItems = newValue.length;

            $scope.CurrentPage = [];
            $scope.bigCurrentPage = 1;

            var startIndex = ($scope.bigCurrentPage - 1) * 10;
            var endIndex = startIndex + 9;

            for (startIndex; startIndex <= endIndex ; startIndex++) {
                if ($scope.QualificationDegrees[startIndex]) {
                    $scope.CurrentPage.push($scope.QualificationDegrees[startIndex]);
                } else {
                    break;
                }
            }
            //console.log($scope.CurrentPageProviders);
        }
    });
    //------------------- end ------------------

}]);
