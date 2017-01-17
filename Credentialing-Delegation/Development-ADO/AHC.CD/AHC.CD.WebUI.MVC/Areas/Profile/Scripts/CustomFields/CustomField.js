var CustomFieldApp = angular.module("CustomFieldApp", []);

CustomFieldApp.service('messageAlertEngine', ['$rootScope', '$timeout', function ($rootScope, $timeout) {

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

CustomFieldApp.controller("CustomFieldController", function ($scope, $http, messageAlertEngine) {
    $scope.AddingPanelStatus = false;
    $scope.Category = ["Text Box"];
    $scope.SelectedCategory = 0;
    $scope.ListOfCustomField = [];
    $scope.TitleOfCategory = "";
    $scope.TitleNameAvailability = true;
    $scope.cancel = false;
    //$scope.$watch('Title', function () {
    //    alert();
    //})
    $scope.GetAllCustomFields = function () {
        $scope.CustomFieldListStatus = false;
        $http.get(rootDir + '/Profile/CustomFieldGeneration/GetCustomField').
        success(function (data, status, headers, config) {
            if (data.status == "true") {
                $scope.ListOfCustomField = angular.copy(data.ListOfCustomFields);
                $scope.CustomFieldListStatus = true;
                if ($scope.ListOfCustomField.length == 0) {
                    $scope.AddingPanelStatus = true;
                    $scope.cancel = true;
                }

            }
            else {
                messageAlertEngine.callAlertMessage("CustomFieldError", data.status, "danger", true);

            }
        }).
        error(function (data, status, headers, config) {

        });
    }
    $scope.GetAllCustomFields();
    
    $scope.AddingPanelForCustomField = function () {
        $scope.AddingPanelStatus = true;
    }
    $scope.SelectCategory = function (data) {
        $scope.SelectedCategory = $scope.Category.indexOf(data)+1;
    }
    $scope.Cancel = function () {
        $scope.AddingPanelStatus = false;
        $scope.TitleNameAvailability = true;

    }
    $scope.AddCustomField = function (TitleOfCategory) {
        
        console.log($scope.ListOfCustomField);
        var count = 0;
        for (var c = 0; c < $scope.ListOfCustomField.length;c++) {
            if ($scope.ListOfCustomField[c].CustomFieldTitle == TitleOfCategory) {
                $scope.TitleNameAvailability = false;
                $("#title").val("");
                count++;
                break;
            }

        }
        if (count == 0) {
            var customFieldViewModel = {
                CustomFieldTitle: TitleOfCategory,
                customFieldCategoryType: $scope.SelectedCategory
            }
            $http.post(rootDir + '/Profile/CustomFieldGeneration/AddCustomField', customFieldViewModel).
                success(function (data, status, headers, config) {
                    //----------- success message -----------
                    if (data.status == "true") {
                        $scope.ListOfCustomField.push(data.CustomField);
                        $scope.AddingPanelStatus = false;
                        $scope.cancel = false;
                        messageAlertEngine.callAlertMessage('CustomFieldSuccess', "New Custom Field Added Successfully. !!!!", "success", true);
                    }
                    else {

                    }
                }).
                error(function (data, status, headers, config) {
                    //----------- error message -----------
                    messageAlertEngine.callAlertMessage('errorInitiated', "", "danger", true);
                    $scope.errorInitiated = "Sorry for Inconvenience !!!! Please Try Again Later...";
                });
        }
    }
})
