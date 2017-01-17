profileApp.directive('popover', function () {
    return function (scope, elem) {
        elem.popover();
    };
});

profileApp.controller('DisclosureController', ['$scope', '$rootScope', 'masterDataService', 'messageAlertEngine', function ($scope, $rootScope, masterDataService, messageAlertEngine) {
    
// rootScoped on emitted value catches the value for the model and insert to get the old data for Practice Interest
    $rootScope.$on('ProfileDisclosure', function (event, val) {
        $scope.disclosureQuestions = val;
    });

    $rootScope.$on("LoadRequireMasterData", function () {

        masterDataService.getMasterData("/Profile/MasterData/getAllQuestions").then(function (masterQuestions) {
            $scope.masterQuestions = masterQuestions;
        });

        masterDataService.getMasterData("/Profile/MasterData/getAllQuestionCategories").then(function (masterQuestionCategories) {
            $scope.masterQuestionCategories = masterQuestionCategories;
        });

    });

    $scope.enableSave = function () {
        $scope.isDataPresent = true;
    }
    $scope.disableSave = function () {
        $scope.isDataPresent = false;
    }

    //To save, add and update Disclosure Questions.
    $scope.saveQuestions = function (disclosureQuestions, type) {
        loadingOn();

        var validationStatus;
        var url;
        var $formData;
        
        validationStatus = $('#editShowDisclosureQuestions').find('form').valid();
        
        $formData = $('#editShowDisclosureQuestions').find('form');
        url = "/Profile/DisclosureQuestion/UpdateDisclosureQuestionAsync?profileId=" + profileId;

        $scope.typeOfSaveForDisclosureQuestions = type;

        ResetFormForValidation($formData);
        validationStatus = $formData.valid();

        if (validationStatus) {
            //Simple POST request example (passing data) :
            $.ajax({
                url: url,
                type: 'POST',
                data: new FormData($formData[0]),
                async: false,
                cache: false,
                contentType: false,
                processData: false,
                success: function (data) {
                    if (data.status == "true") {
                        $scope.disclosureQuestions = data.disclosureQuestion;
                        $scope.ErrorInDisclosureQuestions = false;
                        $rootScope.operateCancelControl('');
                        if ($scope.typeOfSaveForDisclosureQuestions == "Add") {
                            $rootScope.visibilityControl = "addedNewDisclosureQuestions";
                            messageAlertEngine.callAlertMessage('addedNewDisclosureQuestions', "Disclosure Questions saved successfully !!!!", "success", true);
                        } else {
                            $rootScope.visibilityControl = "updatedDisclosureQuestions";
                            messageAlertEngine.callAlertMessage('updatedDisclosureQuestions', "Disclosure Questions updated successfully !!!!", "success", true);
                        }
                    } else {
                        messageAlertEngine.callAlertMessage('errorDisclosureQuestions', "", "danger", true);
                        $scope.errorDisclosureQuestions = data.status.split(",");
                    }
                },
                error: function (e) {
                    messageAlertEngine.callAlertMessage('errorDisclosureQuestions', "", "danger", true);
                    $scope.errorDisclosureQuestions = "Sorry for Inconvenience !!!! Please Try Again Later...";
                }
            });
        }
        loadingOff();
    }
}]);