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

    //-------------------------------- Master Data get All Certificate -------------------------------
    $http.get(rootDir + "/MasterDataNew/GetAllCertificates").success(function (value) {
        try {
            for (var i = 0; i < value.length ; i++) {
                if (value[i] != null) {
                    value[i].LastModifiedDate = ($scope.ConvertDateFormat(value[i].LastModifiedDate)).toString();
                }

            }
            $scope.Certifications = angular.copy(value);
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
        var Month = new Date().getMonth() + 1;
        var _month = Month < 10 ? '0' + Month : Month;
        var _date = new Date().getDate() < 10 ? '0' + new Date().getDate() : new Date().getDate();
        var _year = new Date().getFullYear();
        var temp = {
            CertificationID: 0,
            Name: "",
            Code: "",
            LastModifiedDate: _month + "-" + _date + "-" + _year,
            Status: "Active",
            StatusType: 1
        };
        $scope.Certifications.splice(0,0,angular.copy(temp));
        $scope.tempCertification = angular.copy(temp);
    };

    //------------------- Save and update Certification ---------------------
    $scope.saveUpdateCertification = function (obj, idx) {
        if (!obj.Name) { $scope.error = "Please Enter the Name"; }
        else {
            var success1 = $scope.IsExistCertificate(obj, $scope.Certifications);

            if (!success1.name_Status) { $scope.existErr = "The Name is Exist"; }
            if (!success1.code_Status) { $scope.existErr1 = "The Code is Exist"; }

            if (success1.name_Status && success1.code_Status) {
                var url = "";
                var data_Id = obj.CertificationID;

                if (obj.CertificationID) { url = rootDir + "/MasterDataNew/UpdateCertifications"; }
                else { url = rootDir + "/MasterDataNew/AddCertifications" }

                $http.post(url, obj).
                    success(function (data, status, headers, config) {
                        try {
                            ///----------- success message -----------
                            if (data.status == "true") {
                                if (data_Id) {
                                    messageAlertEngine.callAlertMessage("CertificationDetails", "Certification Details Updated Successfully !!!!", "success", true);
                                } else {
                                    messageAlertEngine.callAlertMessage("CertificationDetails", "New Certification Details Added Successfully !!!!", "success", true);
                                }

                                data.certificationDetails.LastModifiedDate = $scope.ConvertDateFormat(data.certificationDetails.LastModifiedDate);
                                $scope.Certifications[idx] = angular.copy(data.certificationDetails);
                                $scope.reset();
                            }
                            else {
                                if (data_Id) {

                                } else {
                                    messageAlertEngine.callAlertMessage("CertificationDetailsError", "Sorry Unable To Add Certification !!!!", "danger", true);
                                    $scope.Certifications.splice(idx, 1);
                                }
                            }
                        } catch (e) {
                          
                        }
                    }).
                    error(function (data, status, headers, config) {
                        //----------- error message -----------
                        if (data_Id) {
                            messageAlertEngine.callAlertMessage("CertificationDetailsError", "Sorry Unable To Update Certification !!!!", "danger", true);
                        } else {
                            messageAlertEngine.callAlertMessage("CertificationDetailsError", "Sorry Unable To Add Certification !!!!", "danger", true);
                            $scope.Certifications.splice(idx, 1);
                        }
                    });
            }
        }
    };

    //----------------------- duplicate data check  author : krglv ------------------------
    $scope.IsExistCertificate = function (obj, arrayList) {
        var nameStatus = true;
        var codeStatus = true;
        //--------------------- replace white space and convert in lower case -------------------
        var name1 = "";
        var code1 = "";

        if (obj.Name) { name1 = obj.Name.replace(/\s+/g, '').toLowerCase(); }
        if (obj.Code) { code1 = obj.Code.replace(/\s+/g, '').toLowerCase(); }

        if (name1 != "" || code1 != "") {
            for (var i in arrayList) {
                var name2 = ""; var code2 = "";

                if (arrayList[i].Name) { name2 = arrayList[i].Name.replace(/\s+/g, '').toLowerCase(); }
                if (arrayList[i].Code) { code2 = arrayList[i].Code.replace(/\s+/g, '').toLowerCase(); }
                
                if (obj.CertificationID != arrayList[i].CertificationID && name1 == name2) { nameStatus = false; }
                if (code1 != "" && obj.CertificationID != arrayList[i].CertificationID && code1 == code2) { codeStatus = false; }
                if (!nameStatus && !codeStatus) { break; }
            }
        }
        return { name_Status: nameStatus, code_Status: codeStatus };
    };

    

    $scope.filterData = function () {
        $scope.pageChanged(1);
    }




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
        }
    });


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
