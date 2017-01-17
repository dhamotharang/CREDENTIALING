
//=========================== Controller declaration ==========================
profileApp.controller('SpecialtyController', ['$scope', '$rootScope', '$http', 'masterDataService', 'messageAlertEngine', '$filter','profileUpdates', function ($scope, $rootScope, $http, masterDataService, messageAlertEngine, $filter,profileUpdates) {

    //Get all the data for the Speciality on document on ready
    //$(document).ready(function () {
    //    $("#Loading_Message").text("Gathering Profile Data...");
    //    $http({
    //        method: 'GET',
    //        url: '/Profile/MasterProfile/GetBoardSpecialtiesProfileDataAsync?profileId=' + profileId
    //    }).success(function (data, status, headers, config) {
    //        try {
    //            for (key in data) {
    //                $rootScope.$emit(key, data[key]);
    //                //call respective controller to load data (PSP)
    //            }
    //            $rootScope.SpecialtyLoaded = true;
    //            //$rootScope.$broadcast("LoadRequireMasterData");
    //        } catch (e) {
    //            $rootScope.SpecialtyLoaded = true;
    //        }

    //    }).error(function (data, status, headers, config) {
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
        {
            $scope.ShowRenewDiv = false;
            messageAlertEngine.callAlertMessage('RenewerrorSpecialty', "", "danger", true);
        }
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
        console.log($scope.Specialties);
        $scope.SpecialtyDetailsPendingRequest = profileUpdates.getUpdates('Board Specialty', 'Specialty Details');

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

        $scope.PracticeInterestPendingRequest = profileUpdates.getUpdates('Board Specialty', 'Practice Interest');
    });

    $scope.setFiles = function (file) {
        $(file).parent().parent().find(".jancyFileWrapTexts").find("span").width($(file).parent().parent().width() < 243 ? $(file).parent().parent().width() : 243);

    }

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
    $scope.hasError = false;
    $scope.errormessage = false;
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
        
        var SpecialtyBoardName = $($formData[0]).find($("[name='SpecialtyBoardCertifiedDetail.SpecialtyBoardID'] option:selected")).text();
        //var SpecialtyName = $($formData[0]).find($("[name='SpecialtyID'] option:selected")).text();
        //if (SpecialtyName == "")
        //{
        //    $scope.errorSpecialty = "Please select the Speciality Name *.";
        //    $scope.hasSpecialityError = true;

        //}
        //else
        //{
        //    $scope.hasSpecialityError = false;
        //}

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
                    try {
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
                            //--------- speciality object binding ------------
                            for (var i in $scope.masterSpecialties) {
                                if ($scope.masterSpecialties[i].SpecialtyID == data.specialty.SpecialtyID) {
                                    data.specialty.Specialty = angular.copy($scope.masterSpecialties[i]);
                                    break;
                                }
                            }

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
                            $scope.errormessage = true;
                            //$scope.ErrorInSpecialtyDetails = true;
                            //$scope.SpeciatyDetailsErrorList = data.status.split(",");
                        }
                    } catch (e) {
                     
                    }
                },
                error: function (e) {
                    messageAlertEngine.callAlertMessage('errorSpecialty', "", "danger", true);
                    $scope.errorSpecialty = "Sorry for Inconvenience !!!! Please Try Again Later...";
                    //$scope.SpeciatyDetailsErrorList = "Sorry for Inconvenience !!!! Please Try Again Later...";
                }
            });
        }
        else {
            $scope.hasError = false;
        }
        $rootScope.$broadcast('UpdateSpecialtyBoardCertifiedDetailDoc', myData);

        loadingOff();
    };


    //deepu
    $scope.setErrorBit = function (name) {
        $scope.Length = name.length;

        console.log($scope.tempObject.Specialty.TaxonomyCode);
        if (name == "") {
            $scope.hasError = true;
            $scope.tempObject.Specialty.TaxonomyCode = "";
        } else {
            for (var i = 0; i < $scope.masterSpecialties.length; i++) {
                if($scope.masterSpecialties[i].Name==name)
                {
                    $scope.tempObject.Specialty.TaxonomyCode = $scope.masterSpecialties[i].TaxonomyCode;
                    
                }                
            }
            console.log($scope.tempObject.Specialty.TaxonomyCode);
            //if (Length == $scope.masterSpecialties[i].length)
            //alert($)
            $scope.errormessage = false;
            $scope.hasError = false;            
        }
    };

    $scope.setSpecialtyBoardNotCertifiedDetail = function (SpecialtyBoardExamStatusID) {
        switch (SpecialtyBoardExamStatusID.toString()) {
            case "1": SpecialtyBoardExamStatusID = "I have taken exam, result pending"; break;
            case "2": SpecialtyBoardExamStatusID = "I intend to sit for exam"; break;
            case "3": SpecialtyBoardExamStatusID = "I do not intend to take exam"; break;
            case "4": SpecialtyBoardExamStatusID = "I failed in the exam"; break;
            default: break;
        }
        return SpecialtyBoardExamStatusID;
    }

    $scope.ConstructOldandNewObject = function () {
        var oldObject = angular.copy($rootScope.OldObject);
        var newObject = angular.copy($rootScope.tempObject);
        oldObject.BoardCertifiedYesNoOption = oldObject.BoardCertifiedYesNoOption != null ? (oldObject.BoardCertifiedYesNoOption == 1 ? "Yes" : "No") : null;
        newObject.BoardCertifiedYesNoOption = newObject.BoardCertifiedYesNoOption != null ? (newObject.BoardCertifiedYesNoOption == 1 ? "Yes" : "No") : null;
        oldObject.CurrentlyPractingYesNoOption = oldObject.CurrentlyPractingYesNoOption != null ? (oldObject.CurrentlyPractingYesNoOption == 1 ? "Yes" : "No") : null;
        newObject.CurrentlyPractingYesNoOption = newObject.CurrentlyPractingYesNoOption != null ? (newObject.CurrentlyPractingYesNoOption == 1 ? "Yes" : "No") : null;
        oldObject.PreferenceType = oldObject.PreferenceType != null ? (oldObject.PreferenceType == 1 ? "Primary" : "Secondary") : null;
        newObject.PreferenceType = newObject.PreferenceType != null ? (newObject.PreferenceType == 1 ? "Primary" : "Secondary") : null;
        oldObject.ListedInHMOYesNoOption = oldObject.ListedInHMOYesNoOption != null ? (oldObject.ListedInHMOYesNoOption == 1 ? "Yes" : "No") : null;
        newObject.ListedInHMOYesNoOption = newObject.ListedInHMOYesNoOption != null ? (newObject.ListedInHMOYesNoOption == 1 ? "Yes" : "No") : null;
        newObject.ListedInPOSYesNoOption = newObject.ListedInPOSYesNoOption != null ? (newObject.ListedInPOSYesNoOption == 1 ? "Yes" : "No") : null;
        oldObject.ListedInPOSYesNoOption = oldObject.ListedInPOSYesNoOption != null ? (oldObject.ListedInPOSYesNoOption == 1 ? "Yes" : "No") : null;
        oldObject.ListedInPPOYesNoOption = oldObject.ListedInPPOYesNoOption != null ? (oldObject.ListedInPPOYesNoOption == 1 ? "Yes" : "No") : null;
        newObject.ListedInPPOYesNoOption = newObject.ListedInPPOYesNoOption != null ? (newObject.ListedInPPOYesNoOption == 1 ? "Yes" : "No") : null;
        oldObject.ListedInHMO = null; newObject.ListedInHMO = null; oldObject.ListedInPOS = null; newObject.ListedInPOS = null; oldObject.ListedInPPO = null; newObject.ListedInPPO = null;
        oldObject.SpecialtyPreference = null; newObject.SpecialtyPreference = null;
        if (oldObject.SpecialtyBoard!=null){
            oldObject.SpecialtyBoardCertifiedDetail != null ? oldObject.SpecialtyBoardCertifiedDetail.SpecialtyBoardID = oldObject.SpecialtyBoard.Name : null;
        }
        newObject.SpecialtyBoardCertifiedDetail != null ? newObject.SpecialtyBoardCertifiedDetail.SpecialtyBoardID = jQuery.grep($scope.masterSpecialtyBoards, function (ele) { return ele.SpecialtyBoardID == newObject.SpecialtyBoardCertifiedDetail.SpecialtyBoardID })[0].Name : null;
        oldObject.SpecialtyID = oldObject.Specialty.Name; oldObject.Specialty = null;
        newObject.SpecialtyID = newObject.Specialty.Name; newObject.Specialty = null;
        oldObject.SpecialtyBoardNotCertifiedDetail != null ? oldObject.SpecialtyBoardNotCertifiedDetail.SpecialtyBoardExamStatus = $scope.setSpecialtyBoardNotCertifiedDetail(oldObject.SpecialtyBoardNotCertifiedDetail.SpecialtyBoardExamStatus) : null;
        newObject.SpecialtyBoardNotCertifiedDetail != null ? newObject.SpecialtyBoardNotCertifiedDetail.SpecialtyBoardExamStatus = $scope.setSpecialtyBoardNotCertifiedDetail(newObject.SpecialtyBoardNotCertifiedDetail.SpecialtyBoardExamStatus) : null;
        return { "oldObject": oldObject, "newObject": newObject };
    }
    



    //To save and update an already existing Specialty Detail.
    $scope.updateSpecialty = function (specialty, IndexValue) {
        loadingOn();
        var validationStatus;
        var url;
        var myData = {};
        var $formData;
        //Update Details - Denote the URL
        var result = $scope.ConstructOldandNewObject();
        $('#UpdateHistoryHiddenSpeciialty').val(JSON.stringify($rootScope.TrackObjectChanges(result.oldObject, result.newObject)));
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
      
        var SpecialtyBoardName = $($formData[0]).find($("[name='SpecialtyBoardCertifiedDetail.SpecialtyBoardID'] option:selected")).text();
        
        if (validationStatus && !$scope.hasError) {
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
                    try {
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
                            //--------- speciality object binding ------------
                            for (var i in $scope.masterSpecialties) {
                                if ($scope.masterSpecialties[i].SpecialtyID == data.specialty.SpecialtyID) {
                                    data.specialty.Specialty = angular.copy($scope.masterSpecialties[i]);
                                    break
                                }
                            }

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
                                $scope.SpecialtyDetailsPendingRequest = true;
                                $rootScope.operateViewAndAddControl(IndexValue + '_viewboardSpecialty');
                                messageAlertEngine.callAlertMessage('updatedBoardSpecialty' + IndexValue, "Specialty Details Updated Successfully !!!!", "success", true);
                                $scope.errorSpecialty = '';
                            }
                            else {
                                $scope.SpecialtyDetailsPendingRequest = true;
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
                    } catch (e) {
                        
                    }
                },
                error: function (e) {
                    messageAlertEngine.callAlertMessage('errorSpecialty' + IndexValue, "", "danger", true);
                    $scope.errorSpecialty = "Sorry for Inconvenience !!!! Please Try Again Later...";
                    //$scope.SpeciatyDetailsErrorList = "Sorry for Inconvenience !!!! Please Try Again Later...";
                }
            });
        }
        else {
            //$scope.hasError = false;
        }
        $rootScope.$broadcast('UpdateSpecialtyBoardCertifiedDetailDoc', myData);

        loadingOff();
    };

    $scope.ClearError = function () {
        $scope.hasError = false;
    }

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
        $scope.isRemoved = true;
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
                cache: false,
                contentType: false,
                processData: false,
                success: function (data) {
                    try {
                        if (data.status == "true") {
                            var obj = $filter('filter')(Specialties, { SpecialtyDetailID: data.specialty.SpecialtyDetailID })[0];
                            Specialties.splice(Specialties.indexOf(obj), 1);
                            $scope.isRemoved = false;
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
                    } catch (e) {
                     
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
            $('#UpdateHistoryHiddenPracticeInterest').val(JSON.stringify($rootScope.TrackObjectChanges($scope.practiceInterest, $rootScope.tempObject)));
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
                    try {
                        if (data.status == "true") {
                            $scope.practiceInterest = data.practiceInterest;
                            $rootScope.visibilityControl = "";
                            $scope.ErrorInPracticeInterest = false;
                            if ($scope.typeOfSaveForPracticeInterest == "Add") {
                                $rootScope.visibilityControl = "addedNewPracticeInterest";
                                messageAlertEngine.callAlertMessage('addedNewPracticeInterest', "Practice Interest saved successfully !!!!", "success", true);
                            } else {
                                $scope.PracticeInterestPendingRequest = true;
                                $rootScope.visibilityControl = "updatedPracticeInterest";
                                messageAlertEngine.callAlertMessage('updatedPracticeInterest', "Practice Interest updated successfully !!!!", "success", true);
                            }
                        } else {
                            messageAlertEngine.callAlertMessage('errorPracticeInterest', "", "danger", true);
                            $scope.errorPracticeInterest = data.status.split(",");
                            //$scope.ErrorInSpecialtyDetails = true;
                            //$scope.SpeciatyDetailsErrorList = data.status.split(",");
                        }
                    } catch (e) {
                       
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
            $http({
                method: 'GET',
                url: rootDir + '/Profile/MasterProfile/GetBoardSpecialtiesProfileDataAsync?profileId=' + profileId
            }).success(function (data, status, headers, config) {
                try {
                    for (key in data) {
                        $rootScope.$emit(key, data[key]);
                        //call respective controller to load data (PSP)
                    }
                    $rootScope.SpecialtyLoaded = true;
                    $rootScope.$broadcast("LoadRequireMasterDataSpecialty");
                } catch (e) {
                    $rootScope.SpecialtyLoaded = true;
                }

            }).error(function (data, status, headers, config) {
                $rootScope.SpecialtyLoaded = true;
            });
            $scope.dataLoaded = true;
        }
    });
    
}]);
