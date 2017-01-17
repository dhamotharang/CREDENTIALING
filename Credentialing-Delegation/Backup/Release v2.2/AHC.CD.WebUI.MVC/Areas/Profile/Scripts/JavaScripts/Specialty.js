
//=========================== Controller declaration ==========================
profileApp.controller('SpecialtyController', ['$scope', '$rootScope', '$http', 'masterDataService', 'messageAlertEngine', '$filter', function ($scope, $rootScope, $http, masterDataService, messageAlertEngine, $filter) {

    //Get all the data for the Speciality on document on ready
    //$(document).ready(function () {
    //    $("#Loading_Message").text("Gathering Profile Data...");
    //    //console.log("Getting data....");
    //    $http({
    //        method: 'GET',
    //        url: '/Profile/MasterProfile/GetBoardSpecialtiesProfileDataAsync?profileId=' + profileId
    //    }).success(function (data, status, headers, config) {
    //        //console.log(data);
    //        try {
    //            for (key in data) {
    //                //console.log(key);
    //                $rootScope.$emit(key, data[key]);
    //                //call respective controller to load data (PSP)
    //            }
    //            $rootScope.SpecialtyLoaded = true;
    //            //$rootScope.$broadcast("LoadRequireMasterData");
    //        } catch (e) {
    //            //console.log("error getting data back");
    //            $rootScope.SpecialtyLoaded = true;
    //        }

    //    }).error(function (data, status, headers, config) {
    //        //console.log(status);
    //        $rootScope.SpecialtyLoaded = true;
    //    });
    //});

    //profileApp.controller('SpecialtyController', function ($scope, $rootScope, $http, dynamicFormGenerateService, masterDataService) {

    //-----------------------------------------------------Master Data Specialties------------------------------------------------------------

    //------------------------------------Initializing variables to for display and fetch from database---------------------------------------
    $scope.Specialties = [];
    $scope.practiceInterest = {};

    $scope.masterSpecialties = [];
    $scope.masterSpecialtyBoards = [];

    $scope.ShowRenewDiv = false;

    $scope.RenewDiv = function (boardSpecialty) {
        if (boardSpecialty.SpecialtyBoardCertifiedDetail.ExpirationDate == null)
        { $scope.ShowRenewDiv = false; }
        else
        {
            $scope.ShowRenewDiv = true;
        }
    };
    $scope.hideDiv = function () {
        $('.ProviderTypeSelectAutoList1').hide();
        $('.ProviderTypeSelectAutoList').hide();
    }
    // rootScoped on emitted value catches the value for the model and insert to get the old data for Specialty Details
    $rootScope.$on('SpecialtyDetails', function (event, val) {
        $scope.Specialties = val;
        //for (var i = 0; i < $scope.Specialties.length ; i++) {
        //    if ($scope.Specialties[i].SpecialtyBoardCertifiedDetail != null) {
        //        $scope.Specialties[i].SpecialtyBoardCertifiedDetail.ExpirationDate = ConvertDateFormat($scope.Specialties[i].SpecialtyBoardCertifiedDetail.ExpirationDate);
        //        $scope.Specialties[i].SpecialtyBoardCertifiedDetail.InitialCertificationDate = ConvertDateFormat($scope.Specialties[i].SpecialtyBoardCertifiedDetail.InitialCertificationDate);
        //        $scope.Specialties[i].SpecialtyBoardCertifiedDetail.LastReCerificationDate = ConvertDateFormat($scope.Specialties[i].SpecialtyBoardCertifiedDetail.LastReCerificationDate);
        //    }
        //    if ($scope.Specialties[i].SpecialtyBoardNotCertifiedDetail != null) {
        //        $scope.Specialties[i].SpecialtyBoardNotCertifiedDetail.ExamDate = ConvertDateFormat($scope.Specialties[i].SpecialtyBoardNotCertifiedDetail.ExamDate);
        //    }
        //}
    });

    // rootScoped on emitted value catches the value for the model and insert to get the old data for Practice Interest
    $rootScope.$on('PracticeInterest', function (event, val) {
        $scope.practiceInterest = val;
    });


    $rootScope.$on("LoadRequireMasterDataSpecialty", function () {
        //--------------------------------------Fetching Master Data for Specialties and Specialty Boards-------------------------------------------------
        masterDataService.getMasterData(rootDir + "/Profile/MasterData/getAllSpecialities").then(function (masterSpecialties) {
            $scope.masterSpecialties = masterSpecialties;
        });

        masterDataService.getMasterData(rootDir + "/Profile/MasterData/getAllspecialtyBoards").then(function (masterSpecialtyBoards) {
            $scope.masterSpecialtyBoards = masterSpecialtyBoards;
        });
    });

    //----------------search cum dropdown
    $scope.showStates = function (event) {
        $(event.target).parent().find(".ProviderTypeSelectAutoList").first().show();
    };

    //=============== Specialty Conditions ==================
    $scope.submitButtonText = "Add";
    $scope.IndexValue = 0;

    //====================== Specialty ===================

    //To save and add a new Specialty Detail to the list of Specialties.
    $scope.saveSpecialty = function (specialty) {
        loadingOn();
        var validationStatus;
        var url;
        var myData = {};
        var $formData;

        //Add Details - Denote the URL
        $formData = $('#newShowSpecialtyDiv').find('form');
        url = rootDir + "/Profile/BoardSpecialty/AddSpecialityDetailAsync?profileId=" + profileId;

        //For Client side validation in the Edit Div
        ResetFormForValidation($formData);
        validationStatus = $formData.valid();

        var SpecialtyName = $($formData[0]).find($("[name='SpecialtyName']")).val();
        var SpecialtyBoardName = $($formData[0]).find($("[name='SpecialtyBoardCertifiedDetail.SpecialtyBoardID'] option:selected")).text();

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
                        if (data.specialty.PreferenceType == 1) {
                            $scope.setPrimary();
                            for (var i = 0; i < $scope.Specialties.length; i++) {
                                if ($scope.Specialties[i].SpecialtyPreference == "Primary") {
                                    $scope.Specialties[i].SpecialtyPreference = "Secondary";
                                    $scope.Specialties[i].PercentageOfTime = 50;
                                }
                            }
                        }
                        //To map Specialties and Specialty Boards and the dates across the view.
                        data.specialty.Specialty = { ID: data.specialty.SpecialtyID, Name: SpecialtyName };
                        if (data.specialty.SpecialtyBoardCertifiedDetail) {
                            data.specialty.SpecialtyBoardCertifiedDetail.SpecialtyBoard = { ID: data.specialty.SpecialtyBoardCertifiedDetail.SpecialtyBoardID, Name: SpecialtyBoardName };
                        }
                        if (data.specialty.SpecialtyBoardCertifiedDetail != null) {
                            data.specialty.SpecialtyBoardCertifiedDetail.ExpirationDate = ConvertDateFormat(data.specialty.SpecialtyBoardCertifiedDetail.ExpirationDate);
                            data.specialty.SpecialtyBoardCertifiedDetail.InitialCertificationDate = ConvertDateFormat(data.specialty.SpecialtyBoardCertifiedDetail.InitialCertificationDate);
                            data.specialty.SpecialtyBoardCertifiedDetail.LastReCerificationDate = ConvertDateFormat(data.specialty.SpecialtyBoardCertifiedDetail.LastReCerificationDate);
                        }
                        if (data.specialty.SpecialtyBoardNotCertifiedDetail != null) {
                            data.specialty.SpecialtyBoardNotCertifiedDetail.ExamDate = ConvertDateFormat(data.specialty.SpecialtyBoardNotCertifiedDetail.ExamDate);
                        }
                        $scope.Specialties.push(data.specialty);
                        myData = data;
                        $scope.ErrorInSpecialtyDetails = false;
                        $rootScope.operateCancelControl('');
                        messageAlertEngine.callAlertMessage("addedNewBoardSpecialty", "Specialty Details saved successfully !!!!", "success", true);
                        $scope.errorSpecialty = '';
                    } else {
                        messageAlertEngine.callAlertMessage('errorSpecialty', "", "danger", true);
                        $scope.errorSpecialty = data.status.split(",");
                        //$scope.ErrorInSpecialtyDetails = true;
                        //$scope.SpeciatyDetailsErrorList = data.status.split(",");
                    }
                },
                error: function (e) {
                    messageAlertEngine.callAlertMessage('errorSpecialty', "", "danger", true);
                    $scope.errorSpecialty = "Sorry for Inconvenience !!!! Please Try Again Later...";
                    //$scope.SpeciatyDetailsErrorList = "Sorry for Inconvenience !!!! Please Try Again Later...";
                }
            });
        }

        $rootScope.$broadcast('UpdateSpecialtyBoardCertifiedDetailDoc', myData);

        loadingOff();
    };

    //To save and update an already existing Specialty Detail.
    $scope.updateSpecialty = function (specialty, IndexValue) {
        loadingOn();
        var validationStatus;
        var url;
        var myData = {};
        var $formData;
        console.log(specialty);
        //Update Details - Denote the URL

        if ($scope.visibilityControl == (IndexValue + '_editboardSpecialty')) {
            try {
                $formData = $('#specialtyEditDiv' + IndexValue).find('form');
                url = rootDir + "/Profile/BoardSpecialty/UpdateSpecialityDetailAsync?profileId=" + profileId;
            }
            catch (e) { };
        }
        else {
            try {
                $formData = $('#specialtyRenewDiv' + IndexValue).find('form');
                url = rootDir + "/Profile/BoardSpecialty/RenewSpecialityDetailAsync?profileId=" + profileId;
            }
            catch (e) { };
        }
        //Update Details - Denote the URL


        //For Client side validation in the Edit Div
        ResetFormForValidation($formData);
        validationStatus = $formData.valid();

        //var SpecialtyName = $($formData[0]).find($("[name='SpecialtyID'] option:selected")).text();
        var SpecialtyName = $($formData[0]).find($("[name='SpecialtyName']")).val();
        var SpecialtyBoardName = $($formData[0]).find($("[name='SpecialtyBoardCertifiedDetail.SpecialtyBoardID'] option:selected")).text();

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

                    console.log(data);

                    if (data.status == "true") {
                        if (data.specialty.PreferenceType == 1) {
                            $scope.setPrimary();
                            for (var i = 0; i < $scope.Specialties.length; i++) {
                                if ($scope.Specialties[i].SpecialtyPreference == "Primary" && i != IndexValue) {
                                    $scope.Specialties[i].SpecialtyPreference = "Secondary";
                                    $scope.Specialties[i].PercentageOfTime = 50;
                                }
                            }
                        }
                        data.specialty.Specialty = { ID: data.specialty.SpecialtyID, Name: SpecialtyName };
                        if (data.specialty.SpecialtyBoardCertifiedDetail) {
                            data.specialty.SpecialtyBoardCertifiedDetail.SpecialtyBoard = { ID: data.specialty.SpecialtyBoardCertifiedDetail.SpecialtyBoardID, Name: SpecialtyBoardName };
                        }
                        if (data.specialty.SpecialtyBoardCertifiedDetail != null) {
                            data.specialty.SpecialtyBoardCertifiedDetail.ExpirationDate = ConvertDateFormat(data.specialty.SpecialtyBoardCertifiedDetail.ExpirationDate);
                            data.specialty.SpecialtyBoardCertifiedDetail.InitialCertificationDate = ConvertDateFormat(data.specialty.SpecialtyBoardCertifiedDetail.InitialCertificationDate);
                            data.specialty.SpecialtyBoardCertifiedDetail.LastReCerificationDate = ConvertDateFormat(data.specialty.SpecialtyBoardCertifiedDetail.LastReCerificationDate);
                        }
                        if (data.specialty.SpecialtyBoardNotCertifiedDetail != null) {
                            data.specialty.SpecialtyBoardNotCertifiedDetail.ExamDate = ConvertDateFormat(data.specialty.SpecialtyBoardNotCertifiedDetail.ExamDate);
                        }
                        $scope.Specialties[IndexValue] = data.specialty;
                        $scope.ErrorInSpecialtyDetails = false;
                        if ($scope.visibilityControl == (IndexValue + '_editboardSpecialty')) {
                            $rootScope.operateViewAndAddControl(IndexValue + '_viewboardSpecialty');
                            messageAlertEngine.callAlertMessage('updatedBoardSpecialty' + IndexValue, "Specialty Details Updated Successfully !!!!", "success", true);
                            $scope.errorSpecialty = '';
                        }
                        else {
                            $rootScope.operateViewAndAddControl(IndexValue + '_viewboardSpecialty');
                            messageAlertEngine.callAlertMessage('renewedBoardSpecialty' + IndexValue, "Specialty Details Renewed Successfully !!!!", "success", true);
                        }
                        myData = data;
                    }
                    else {
                        messageAlertEngine.callAlertMessage('errorSpecialty' + IndexValue, "", "danger", true);
                        $scope.errorSpecialty = data.status.split(",");
                        //$scope.ErrorInSpecialtyDetails = true;
                        //$scope.SpeciatyDetailsErrorList = data.status.split(",");
                    }
                },
                error: function (e) {
                    messageAlertEngine.callAlertMessage('errorSpecialty' + IndexValue, "", "danger", true);
                    $scope.errorSpecialty = "Sorry for Inconvenience !!!! Please Try Again Later...";
                    //$scope.SpeciatyDetailsErrorList = "Sorry for Inconvenience !!!! Please Try Again Later...";
                }
            });
        }

        $rootScope.$broadcast('UpdateSpecialtyBoardCertifiedDetailDoc', myData);

        loadingOff();
    };

    //set only one as Primary
    $scope.setPrimary = function () {
        try {
            for (var i = 0; i < $scope.Specialties.length ; i++) {
                $scope.Specialties[i].PreferenceType = "2";
            }
        }
        catch (e) { }
    };

    //To initiate Removal Confirmation Modal
    $scope.initSpecialtyWarning = function (boardSpecialty) {
        if (angular.isObject(boardSpecialty)) {
            $scope.tempSpecialty = boardSpecialty;
        }
        $('#specialtyWarningModal').modal();
    };

    //To remove Speciaty
    $scope.removeSpecialty = function (Specialties) {
        var validationStatus = false;
        var url = null;
        var myData = {};
        var $formData = null;
        $formData = $('#removeSpecialty');
        url = rootDir + "/Profile/BoardSpecialty/RemoveSpecialityDetailAsync?profileId=" + profileId;
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
                        var obj = $filter('filter')(Specialties, { SpecialtyDetailID: data.specialty.SpecialtyDetailID })[0];
                        Specialties.splice(Specialties.indexOf(obj), 1);
                        $('#specialtyWarningModal').modal('hide');
                        $rootScope.operateCancelControl('');
                        myData = data;
                        messageAlertEngine.callAlertMessage("addedNewBoardSpecialty", "Specialty Detail Removed successfully.", "success", true);
                        $scope.errorSpecialty = '';
                    } else {
                        $('#specialtyWarningModal').modal('hide');
                        messageAlertEngine.callAlertMessage("removeSpecialty", data.status, "danger", true);
                        $scope.errorSpecialty = "Sorry for Inconvenience !!!! Please Try Again Later...";
                    }
                },
                error: function (e) {

                }
            });
        }

        $rootScope.$broadcast('RemoveSpecialtyBoardCertifiedDetailDoc', myData);

    };

    //To save, add and update a Practice Interest.
    $scope.savePracticeInterest = function (practiceInterest) {
        loadingOn();
        var validationStatus;
        var url;
        var $formData;

        if (!practiceInterest.PracticeInterestID) {
            $scope.typeOfSaveForPracticeInterest = "Add";
        } else {
            $scope.typeOfSaveForPracticeInterest = "Edit";
        }

        if ($scope.visibilityControl == ('editpracticeInterest')) {
            //Update Details - Denote the URL
            $formData = $('#editShowPracticeInterest').find('form');
            url = rootDir + "/Profile/BoardSpecialty/UpdatePracticeInterestAsync?profileId=" + profileId;
        }

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
                        $scope.practiceInterest = data.practiceInterest;
                        $rootScope.visibilityControl = "";
                        $scope.ErrorInPracticeInterest = false;
                        if ($scope.typeOfSaveForPracticeInterest == "Add") {
                            $rootScope.visibilityControl = "addedNewPracticeInterest";
                            messageAlertEngine.callAlertMessage('addedNewPracticeInterest', "Practice Interest saved successfully !!!!", "success", true);
                        } else {
                            $rootScope.visibilityControl = "updatedPracticeInterest";
                            messageAlertEngine.callAlertMessage('updatedPracticeInterest', "Practice Interest updated successfully !!!!", "success", true);
                        }
                    } else {
                        messageAlertEngine.callAlertMessage('errorPracticeInterest', "", "danger", true);
                        $scope.errorPracticeInterest = data.status.split(",");
                        //$scope.ErrorInSpecialtyDetails = true;
                        //$scope.SpeciatyDetailsErrorList = data.status.split(",");
                    }
                },
                error: function (e) {
                    messageAlertEngine.callAlertMessage('errorPracticeInterest', "", "danger", true);
                    $scope.errorPracticeInterest = "Sorry for Inconvenience !!!! Please Try Again Later...";
                    //$scope.SpeciatyDetailsErrorList = "Sorry for Inconvenience !!!! Please Try Again Later...";
                }
            });
            $scope.changePartial = false;
        }
        loadingOff();
    };

    $rootScope.getSpecilityForThisUser = function () {
        return $scope.Specialties;
    };

    $scope.fillPercentageOfTime = function (value) {
        if (value == "1")
            $scope.tempObject.PercentageOfTime = 100.00;
        else
            $scope.tempObject.PercentageOfTime = 50.00;
    }

    $scope.fillPreferenceType = function (value) {
        if (value == 100)
            $scope.tempObject.PreferenceType = 1;
        else if (value < 100 && value >= 0)
            $scope.tempObject.PreferenceType = 2;
        else
            $scope.tempObject.PreferenceType = '';
    }

    $scope.clearOtherModel = function (value) {
        if ($rootScope.visibilityControl == 'addboardSpecialty') {
            if (value == 1) {
                $scope.tempObject.SpecialtyBoardNotCertifiedDetail = null;
            } else {
                $scope.tempObject.SpecialtyBoardCertifiedDetail = null;
            }
        }
    }

    $scope.clearOtherOptions = function (value) {
        if ($rootScope.visibilityControl == 'addboardSpecialty') {
            if (value == 1) {
                $scope.tempObject.SpecialtyBoardNotCertifiedDetail.ExamDate = null;
                $scope.tempObject.SpecialtyBoardNotCertifiedDetail.ReasonForNotTakingExam = null;
                $scope.tempObject.SpecialtyBoardNotCertifiedDetail.RemarkForExamFail = null;
            } else if (value == 2) {
                $scope.tempObject.SpecialtyBoardNotCertifiedDetail.ReasonForNotTakingExam = null;
                $scope.tempObject.SpecialtyBoardNotCertifiedDetail.RemarkForExamFail = null;
            } else if (value == 3) {
                $scope.tempObject.SpecialtyBoardNotCertifiedDetail.ExamDate = null;
                $scope.tempObject.SpecialtyBoardNotCertifiedDetail.RemarkForExamFail = null;
            } else {
                $scope.tempObject.SpecialtyBoardNotCertifiedDetail.ExamDate = null;
                $scope.tempObject.SpecialtyBoardNotCertifiedDetail.ReasonForNotTakingExam = null;
            }
        }
    }
    $rootScope.SpecialtyLoaded = true;
    $scope.dataLoaded = false;
    $rootScope.$on('Specialty', function () {
        if (!$scope.dataLoaded) {
            $rootScope.SpecialtyLoaded = false;
            //console.log("Getting data....");
            $http({
                method: 'GET',
                url: rootDir + '/Profile/MasterProfile/GetBoardSpecialtiesProfileDataAsync?profileId=' + profileId
            }).success(function (data, status, headers, config) {
                //console.log(data);
                try {
                    for (key in data) {
                        //console.log(key);
                        $rootScope.$emit(key, data[key]);
                        //call respective controller to load data (PSP)
                    }
                    $rootScope.SpecialtyLoaded = true;
                    $rootScope.$broadcast("LoadRequireMasterDataSpecialty");
                } catch (e) {
                    //console.log("error getting data back");
                    $rootScope.SpecialtyLoaded = true;
                }

            }).error(function (data, status, headers, config) {
                //console.log(status);
                $rootScope.SpecialtyLoaded = true;
            });
            $scope.dataLoaded = true;
        }
    });

}]);
