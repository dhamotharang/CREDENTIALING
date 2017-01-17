profileApp.directive('popover', function () {
    return function (scope, elem) {
        elem.popover();
    };
});

profileApp.controller('DisclosureController', ['$scope', '$rootScope', '$http', 'masterDataService', 'messageAlertEngine', 'profileUpdates', function ($scope, $rootScope, $http, masterDataService, messageAlertEngine, profileUpdates) {

    //Get all the data for the Disclosure Question on document on ready
    //$(document).ready(function () {
    //    $("#Loading_Message").text("Gathering Profile Data...");
    //    //console.log("Getting data....");
    //    $http({
    //        method: 'GET',
    //        url: '/Profile/MasterProfile/GetDisclosureQuestionsProfileDataAsync?profileId=' + profileId
    //    }).success(function (data, status, headers, config) {
    //        //console.log(data);
    //        try {
    //            for (key in data) {
    //                //console.log(key);
    //                $rootScope.$emit(key, data[key]);
    //                //call respective controller to load data (PSP)
    //            }

    //            $rootScope.DisclosureQuestionLoaded = true;
    //            //$rootScope.$broadcast("LoadRequireMasterData");
    //        } catch (e) {
    //            //console.log("error getting data back");
    //            $rootScope.DisclosureQuestionLoaded = true;
    //        }

    //    }).error(function (data, status, headers, config) {
    //        //console.log(status);
    //        $rootScope.DisclosureQuestionLoaded = true;
    //    });
    //});

    // rootScoped on emitted value catches the value for the model and insert to get the old data for Practice Interest
    
    $scope.setAllNo = function () {
        $scope.isDataPresent = true;
        //$('input:radio[class^=selectNo][value=2]').each(function (i) {
        //    this.checked = true;
        //});
        $('input:radio[class^=selectNo][value=2]').trigger("click");
        $('input:radio[class^=selectNo][value=2]').trigger("click");
    }


    $rootScope.$on('ProfileDisclosure', function (event, val) {

        $scope.ProfileDisclosurePendingRequest = profileUpdates.getUpdates('Disclosure Question', 'Profile Disclosure');

        $scope.disclosureQuestions = val;
    });

    $rootScope.$on("LoadRequireMasterDataDisclosureQuestion", function () {

        masterDataService.getMasterData(rootDir + "/Profile/MasterData/getAllQuestions").then(function (masterQuestions) {
            $scope.masterQuestions = masterQuestions;
            console.log($scope.masterQuestions);
        });

        masterDataService.getMasterData(rootDir + "/Profile/MasterData/getAllQuestionCategories").then(function (masterQuestionCategories) {
            $scope.masterQuestionCategories = masterQuestionCategories;
        });

    });

    $scope.enableSave = function (obj, value) {
        if (value == 2) {
            obj.Reason = '';
        }
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
        url = rootDir + "/Profile/DisclosureQuestion/UpdateDisclosureQuestionAsync?profileId=" + profileId;

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
                            $scope.ProfileDisclosurePendingRequest = true;
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
    $rootScope.DisclosureQuestionLoaded = true;
    $scope.dataLoaded = false;
    $rootScope.$on('DisclosureQuestion', function () {
        if (!$scope.dataLoaded) {
            $rootScope.DisclosureQuestionLoaded = false;
            //console.log("Getting data....");
            $http({
                method: 'GET',
                url: rootDir + '/Profile/MasterProfile/GetDisclosureQuestionsProfileDataAsync?profileId=' + profileId
            }).success(function (data, status, headers, config) {
                //console.log(data);
                try {
                    for (key in data) {
                        //console.log(key);
                        $rootScope.$emit(key, data[key]);
                        //call respective controller to load data (PSP)
                    }

                    $rootScope.DisclosureQuestionLoaded = true;
                    $rootScope.$broadcast("LoadRequireMasterDataDisclosureQuestion");
                } catch (e) {
                    //console.log("error getting data back");
                    $rootScope.DisclosureQuestionLoaded = true;
                }

            }).error(function (data, status, headers, config) {
                //console.log(status);
                $rootScope.DisclosureQuestionLoaded = true;
            });
            $scope.dataLoaded = true;
        }
    });
}]);