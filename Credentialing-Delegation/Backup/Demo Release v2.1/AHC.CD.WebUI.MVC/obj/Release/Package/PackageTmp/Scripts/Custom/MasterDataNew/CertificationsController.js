//--------------------- Angular Module ----------------------
var masterDataCertifications = angular.module("masterDataCertifications", ['ui.bootstrap']);

masterDataCertifications.service('messageAlertEngine', ['$rootScope', '$timeout', function ($rootScope, $timeout) {

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
masterDataCertifications.controller('masterDataCertificationsController', ['$scope', '$http', '$filter','$rootScope', 'messageAlertEngine', function ($scope, $http, $filter,$rootScope, messageAlertEngine) {


    $http.get("/MasterDataNew/GetAllCertificates").then(function (value) {
        console.log("GetAllCertificates");
        console.log(value);

        for (var i = 0; i < value.data.length ; i++) {
            if (value.data[i] != null) {
                value.data[i].LastModifiedDate = $scope.ConvertDateFormat(value.data[i].LastModifiedDate);
            }
        }

        $scope.Certifications = value.data;
        console.log($scope.Certifications);
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


    $scope.tempCertification = {};

    //------------ gets the template to ng-include for a table row / item -------------------
    $scope.getTemplate = function (certification) {
        if (certification.CertificationID === $scope.tempCertification.CertificationID) return 'editCertification';
        else return 'displayCertification';
    };

    //-------------------- Edit Certification ----------
    $scope.editCertification = function (certification) {
        $scope.tempCertification = angular.copy(certification);
        $scope.disableAdd = true;
    };

    //------------------- Add Certification ---------------------
    $scope.addCertification = function (certification) {
        $scope.disableEdit = true;
        $scope.disableAdd = true;
        var temp = {
            CertificationID: 0,
            Name: "",
            Code: "",
            Status: "Active",
            LastModifiedDate: new Date()
        };
        $scope.Certifications.splice(0,0,angular.copy(temp));
        $scope.tempCertification = angular.copy(temp);
    };

    //------------------- Save Certification ---------------------
    $scope.saveCertification = function (idx) {
        //$scope.tempCertification.LastModifiedDate = "02/03/2015"

        var addData = {
            CertificationID: 0,
            Name: $scope.tempCertification.Name,
            Code: $scope.tempCertification.Code,
            StatusType: 1            
        }

        var isExist = true;
        var isExist1 = true;

        for (var i = 0; i < $scope.Certifications.length; i++) {

            if (addData.Name && $scope.Certifications[i].Name.replace(" ", "").toLowerCase() == addData.Name.replace(" ", "").toLowerCase()) {

                isExist = false;
                $scope.existErr = "The Name is Exist";
                break;
            }
            if (addData.Code && $scope.Certifications[i].Code.replace(" ", "").toLowerCase() == addData.Code.replace(" ", "").toLowerCase()) {

                isExist1 = false;
                $scope.existErr1 = "The Code is Exist";
                break;
            }
        }

        if (!addData.Name) { $scope.error = "Please Enter the Name"; }
        if (!addData.Code) { $scope.error1 = "Please Enter the Code"; }

        console.log("Saving Certification");
        if (addData.Name && addData.Code && isExist && isExist1) {
        $http.post('/MasterDataNew/AddCertifications', addData).
            success(function (data, status, headers, config) {
                //----------- success message -----------
                if (data.status == "true") {
                    messageAlertEngine.callAlertMessage("CertificationDetails", "New Certification Details Added Successfully !!!!", "success", true);
                    data.certificationDetails.LastModifiedDate = $scope.ConvertDateFormat(data.certificationDetails.LastModifiedDate);
                    $scope.Certifications[idx] = angular.copy(data.certificationDetails);                    
                    $scope.reset();
                    $scope.error = "";
                    $scope.existErr = "";
                    $scope.error1 = "";
                    $scope.existErr1 = "";
                }
                else {
                    messageAlertEngine.callAlertMessage("CertificationDetailsError", "Sorry Unable To Add Certification !!!!", "danger", true);
                    $scope.Certifications.splice(idx, 1);
                }
            }).
            error(function (data, status, headers, config) {
                //----------- error message -----------
                messageAlertEngine.callAlertMessage("CertificationDetailsError", "Sorry Unable To Add Certification !!!!", "danger", true);
                $scope.Certifications.splice(idx, 1);
            });
        }

        
    };

    //------------------- Update Certification ---------------------
    $scope.updateCertification = function (idx) {
        //$scope.tempCertification.LastModifiedDate = "02/03/2015"
        var updateData = {
            CertificationID: $scope.tempCertification.CertificationID,
            Name: $scope.tempCertification.Name,
            Code: $scope.tempCertification.Code,
            StatusType: 1
        }
        var isExist = true;
        var isExist1 = true;

        for (var i = 0; i < $scope.Certifications.length; i++) {

            if (updateData.Name && $scope.Certifications[i].CertificationID != updateData.CertificationID && $scope.Certifications[i].Name.replace(" ", "").toLowerCase() == updateData.Name.replace(" ", "").toLowerCase()) {

                isExist = false;
                $scope.existErr = "The Name is Exist";
                break;
            }
            if (updateData.Code && $scope.Certifications[i].CertificationID != updateData.CertificationID && $scope.Certifications[i].Code.replace(" ", "").toLowerCase() == updateData.Code.replace(" ", "").toLowerCase()) {

                isExist1 = false;
                $scope.existErr1 = "The Code is Exist";
                break;
            }
        }
        if (!updateData.Name) { $scope.error = "Please Enter the Name"; }
        if (!updateData.Code) { $scope.error1 = "Please Enter the Code"; }
        console.log("Updating Certification");
        if (updateData && isExist && isExist1) {
            $http.post('/MasterDataNew/UpdateCertifications', updateData).
                success(function (data, status, headers, config) {
                    //----------- success message -----------
                    if (data.status == "true") {
                        messageAlertEngine.callAlertMessage("CertificationDetails", "Certification Details Updated Successfully !!!!", "success", true);
                        data.certificationDetails.LastModifiedDate = $scope.ConvertDateFormat(data.certificationDetails.LastModifiedDate);
                        $scope.Certifications[idx] = angular.copy(data.certificationDetails);                        
                        $scope.reset();
                        $scope.error = "";
                        $scope.existErr = "";
                        $scope.error1 = "";
                        $scope.existErr1 = "";
                    }


                }).
                error(function (data, status, headers, config) {
                    //----------- error message -----------
                    messageAlertEngine.callAlertMessage("CertificationDetailsError", "Sorry Unable To Update Certification !!!!", "danger", true);
                });

        }
       
    };

    //----------------- Group new add cancel ---------------
    $scope.cancelAdd = function () {
        $scope.Certifications.splice(0, 1);
        $scope.tempCertification = {};
        $scope.disableEdit = false;
        $scope.disableAdd = false;
        $scope.error = "";
        $scope.existErr = "";
        $scope.error1 = "";
        $scope.existErr1 = "";
    };

    //-------------------- Reset Group ----------------------
    $scope.reset = function () {
        $scope.tempCertification = {};
        $scope.disableEdit = false;
        $scope.disableAdd = false;
        $scope.error = "";
        $scope.existErr = "";
        $scope.error1 = "";
        $scope.existErr1 = "";
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
        if ($scope.Certifications) {
            for (startIndex; startIndex <= endIndex ; startIndex++) {
                if ($scope.Certifications[startIndex]) {
                    $scope.CurrentPage.push($scope.Certifications[startIndex]);
                } else {
                    break;
                }
            }
        }
        //console.log($scope.CurrentPageProviders);
    });
    //-------------- License Scope Watch ---------------------
    $scope.$watchCollection('Certifications', function (newValue, oldValue) {
        if (newValue) {
            $scope.bigTotalItems = newValue.length;

            $scope.CurrentPage = [];
            $scope.bigCurrentPage = 1;

            var startIndex = ($scope.bigCurrentPage - 1) * 10;
            var endIndex = startIndex + 9;

            for (startIndex; startIndex <= endIndex ; startIndex++) {
                if ($scope.Certifications[startIndex]) {
                    $scope.CurrentPage.push($scope.Certifications[startIndex]);
                } else {
                    break;
                }
            }
            //console.log($scope.CurrentPageProviders);
        }
    });
    //------------------- end ------------------

}]);
