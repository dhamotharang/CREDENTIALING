//--------------------- Angular Module ----------------------
var masterDataProviderType = angular.module("masterDataProviderType", ['ui.bootstrap']);

masterDataProviderType.service('messageAlertEngine', ['$rootScope', '$timeout', function ($rootScope, $timeout) {

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
masterDataProviderType.controller('masterDataProviderTypeController', ['$scope', '$http', '$filter', '$rootScope', 'messageAlertEngine', function ($scope, $http, $filter, $rootScope, messageAlertEngine) {

    $scope.ProviderTypes = [];

    $http.get("/MasterDataNew/GetAllProviderTypes").then(function (value) {
        //console.log("ProviderTypes");
        //console.log(value);

        for (var i = 0; i < value.data.length ; i++) {
            if (value.data[i] != null) {
                value.data[i].LastModifiedDate = $scope.ConvertDateFormat(value.data[i].LastModifiedDate);
            }
        }

        $scope.ProviderTypes = value.data;
        //console.log($scope.ProviderTypes);
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

    

    $scope.tempProviderType = {};

    //------------ gets the template to ng-include for a table row / item -------------------
    $scope.getTemplate = function (providerType) {
        if (providerType.ProviderTypeID === $scope.tempProviderType.ProviderTypeID) return 'editProviderType';
        else return 'displayProviderType';
    };

    //-------------------- Edit AdmittingPrivilege ----------
    $scope.editProviderType = function (providerType) {
        $scope.tempProviderType = angular.copy(providerType);
        $scope.disableAdd = true;
    };

    //------------------- Add AdmittingPrivilege ---------------------
    $scope.addProviderType = function (providerType) {
        $scope.disableEdit = true;
        $scope.disableAdd = true;
        var temp = {
            ProviderTypeID: 0,
            Title: "",
            Description: "",
            Code: "",
            Status: "Active",
            LastModifiedDate: new Date()
        };
        $scope.ProviderTypes.splice(0, 0, angular.copy(temp));
        $scope.tempProviderType = angular.copy(temp);
    };

    //------------------- Save AdmittingPrivilege ---------------------
    $scope.saveProviderType = function (idx) {
        //$scope.tempProviderType.LastModifiedDate = "02/03/2015"

        var addData = {
            ProviderTypeID: 0,
            Title: $scope.tempProviderType.Title,
            Description: $scope.tempProviderType.Description,
            Code: $scope.tempProviderType.Code,
            StatusType: 1
        }

        var isExist = true;
        //var isExist1 = true;

        for (var i = 0; i < $scope.ProviderTypes.length; i++) {

            if (addData.Title && $scope.ProviderTypes[i].Title.replace(" ", "").toLowerCase() == addData.Title.replace(" ", "").toLowerCase()) {

                isExist = false;
                $scope.existErr = "The Title is Exist";
                break;
            }
            //if (addData.Code && $scope.ProviderTypes[i].Code.replace(" ", "").toLowerCase() == addData.Code.replace(" ", "").toLowerCase()) {

            //    isExist1 = false;
            //    $scope.existErr1 = "The Code is Exist";
            //    break;
            //}
        }
        if (!addData.Title) { $scope.error = "Please Enter the Title"; }              

        //console.log("Saving ProviderTypes");
        if (addData.Title && isExist) {
            $http.post('/MasterDataNew/AddProviderTypes', addData).
                success(function (data, status, headers, config) {
                    //----------- success message -----------
                    if (data.status == "true") {
                        messageAlertEngine.callAlertMessage("ProviderType", "New Provider Type Details Added Successfully !!!!", "success", true);
                        data.providerTypeDetails.LastModifiedDate = $scope.ConvertDateFormat(data.providerTypeDetails.LastModifiedDate);
                        $scope.ProviderTypes[idx] = angular.copy(data.providerTypeDetails);                        
                        $scope.reset();
                        $scope.error = "";
                        $scope.existErr = "";
                        $scope.error1 = "";
                        //$scope.existErr1 = "";
                    }
                    else {
                        messageAlertEngine.callAlertMessage("ProviderTypeError", "Sorry Unable To Add Provider Type !!!!", "danger", true);
                        $scope.ProviderTypes.splice(idx, 1);
                    }
                }).
                error(function (data, status, headers, config) {
                    //----------- error message -----------
                    messageAlertEngine.callAlertMessage("ProviderTypeError", "Sorry Unable To Add Provider Type !!!!", "danger", true);
                    $scope.ProviderTypes.splice(idx, 1);
                });
        }

        
    };

    //------------------- Update ProviderType ---------------------
    $scope.updateProviderType = function (idx) {
        //$scope.tempProviderType.LastModifiedDate = "02/03/2015"

        var updateData = {
            ProviderTypeID: $scope.tempProviderType.ProviderTypeID,
            Title: $scope.tempProviderType.Title,
            Description: $scope.tempProviderType.Description,
            Code: $scope.tempProviderType.Code,
            StatusType: 1
        }

        var isExist = true;
        //var isExist1 = true;

        for (var i = 0; i < $scope.ProviderTypes.length; i++) {

            if (updateData.Title && $scope.ProviderTypes[i].ProviderTypeID != updateData.ProviderTypeID && $scope.ProviderTypes[i].Title.replace(" ", "").toLowerCase() == updateData.Title.replace(" ", "").toLowerCase()) {

                isExist = false;
                $scope.existErr = "The Title is Exist";
                break;
            }
            //if (updateData.Code && $scope.ProviderTypes[i].Code.replace(" ", "").toLowerCase() == updateData.Code.replace(" ", "").toLowerCase() && $scope.ProviderTypes[i].ProviderTypeID != updateData.ProviderTypeID) {

            //    isExist1 = false;
            //    $scope.existErr1 = "The Code is Exist";
            //    break;
            //}
        }
        if (!updateData.Title) { $scope.error = "Please Enter the Title"; }        

        //console.log("Updating ProviderTypes");
        if (updateData.Title && isExist) {
            $http.post('/MasterDataNew/UpdateProviderTypes', updateData).
                success(function (data, status, headers, config) {
                    //----------- success message -----------
                    if (data.status == "true") {
                        messageAlertEngine.callAlertMessage("ProviderType", "Provider Type Details Updated Successfully !!!!", "success", true);
                        data.providerTypeDetails.LastModifiedDate = $scope.ConvertDateFormat(data.providerTypeDetails.LastModifiedDate);
                        $scope.ProviderTypes[idx] = angular.copy(data.providerTypeDetails);                        
                        $scope.reset();
                        $scope.error = "";
                        $scope.existErr = "";
                        $scope.error1 = "";
                        //$scope.existErr1 = "";
                    }
                }).
                error(function (data, status, headers, config) {
                    //----------- error message -----------
                    messageAlertEngine.callAlertMessage("ProviderTypeError", "Sorry Unable To Update Provider Type !!!!", "danger", true);                    
                });
        }


    };

    //----------------- AdmittingPrivilege new add cancel ---------------
    $scope.cancelAdd = function () {
        $scope.ProviderTypes.splice(0, 1);
        $scope.tempProviderType = {};
        $scope.disableEdit = false;
        $scope.disableAdd = false;
        $scope.error = "";
        $scope.existErr = "";
        $scope.error1 = "";
        //$scope.existErr1 = "";
    };

    //-------------------- Reset AdmittingPrivilege ----------------------
    $scope.reset = function () {
        $scope.tempProviderType = {};
        $scope.disableEdit = false;
        $scope.disableAdd = false;
        $scope.error = "";
        $scope.existErr = "";
        $scope.error1 = "";
        //$scope.existErr1 = "";
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
        if ($scope.ProviderTypes) {
            for (startIndex; startIndex <= endIndex ; startIndex++) {
                if ($scope.ProviderTypes[startIndex]) {
                    $scope.CurrentPage.push($scope.ProviderTypes[startIndex]);
                } else {
                    break;
                }
            }
        }
        ////console.log($scope.CurrentPageProviders);
    });
    //-------------- License Scope Watch ---------------------
    $scope.$watchCollection('ProviderTypes', function (newValue, oldValue) {
        if (newValue) {
            $scope.bigTotalItems = newValue.length;

            $scope.CurrentPage = [];
            $scope.bigCurrentPage = 1;

            var startIndex = ($scope.bigCurrentPage - 1) * 10;
            var endIndex = startIndex + 9;

            for (startIndex; startIndex <= endIndex ; startIndex++) {
                if ($scope.ProviderTypes[startIndex]) {
                    $scope.CurrentPage.push($scope.ProviderTypes[startIndex]);
                } else {
                    break;
                }
            }
            ////console.log($scope.CurrentPageProviders);
        }
    });
    //------------------- end ------------------

}]);
