function showLocationList(ele) {
    $(ele).parent().find(".ProviderTypeSelectAutoList").first().show();
}


profileApp.controller('WorkHistoryController', ['$scope', '$rootScope', '$http', '$filter', '$timeout', 'masterDataService', 'locationService', 'messageAlertEngine', 'profileUpdates', function ($scope, $rootScope, $http, $filter, $timeout, masterDataService, locationService, messageAlertEngine, profileUpdates) {


    // resetting the Date elements in work history scope
    var orderBy = $filter('orderBy');

    $scope.resetDates = function () {
        $scope.errormessageformilitarybranch = false;
        try {
            //$('#militarybrancherrorid').show();
            $rootScope.tempObject.StartDate = new Date();
            $rootScope.tempObject.EndDate = new Date();
            $rootScope.tempObject.DischargeDate = new Date();
        } catch (e) {
        }

    }

    $scope.setFiles = function (file) {
        $(file).parent().parent().find(".jancyFileWrapTexts").find("span").width($(file).parent().parent().width() < 243 ? $(file).parent().parent().width() : 243);

    }

    // new city autofill code

    $scope.addressAutocomplete = function (location) {
        if (location.length == 0) {
            $scope.resetAddressModels();
        }
        $rootScope.tempObject.CityOfBirth = location;
        if (location.length > 1 && !angular.isObject(location)) {
            locationService.getLocations(location).then(function (val) {
                $scope.Locations = val;
            });
        } else if (angular.isObject(location)) {
            $scope.setAddressModels(location);
        }
    };


    $scope.selectedLocation = function (location) {
        $scope.setAddressModels(location);
        $(".ProviderTypeSelectAutoList").hide();

    };

    $scope.resetAddressModels = function () {
        $rootScope.tempObject.City = "";
        $rootScope.tempObject.State = "";
        $rootScope.tempObject.Country = "";
    };

    $scope.setAddressModels = function (location) {
        $rootScope.tempObject.City = location.City;
        $rootScope.tempObject.State = location.State;
        $rootScope.tempObject.Country = location.Country;

    }

    // Assigning country dial codes to scope variable

    $scope.CountryDialCodes = countryDailCodes;

    // Function to pop up the country code select for passed div ID

    $scope.showContryCodeDiv = function (countryCodeDivId) {
        changeVisibilityOfCountryCode();
        $("#" + countryCodeDivId).show();
    };

    // Function called on changing the "Currently Working Here" option. Sets the EndDate & DepartureReason to null.

    $scope.$watch('tempObject.EndDate', function (newValue, oldValue) {
        if (newValue) {
            $scope.departReason = true;
            $rootScope.tempObject.CurrentlyWorkingOption = 2;
        }
        else {
            $scope.departReason = false;
            $rootScope.tempObject.CurrentlyWorkingOption = 1;
        }
    });

    $scope.currentlyWorkingChange = function (newValue) {
        //if (newValue == 1) { // Checks based on the Enum value 1.Yes , 2.NO
        //    $rootScope.tempObject.EndDate = null;
        //    $rootScope.tempObject.DepartureReason = null;
        //}
        if (!$rootScope.tempObject.EndDate) {
            $scope.departReason = false;
            $scope.tempObject.DepartureReason = "";
            $rootScope.tempObject.CurrentlyWorkingOption = 1;
        }
        else {
            $scope.departReason = true;

            $rootScope.tempObject.CurrentlyWorkingOption = 2;
        }
    }

    $scope.militaryDischargeChange = function (newValue) {
        if (newValue != 'Other than honourable') { // Checks based on the selected discharge type value
            $rootScope.tempObject.HonorableExplanation = null;
        }
    }

    //----------------------------------- Military Branch Search cum Drop Down -----------------------------
    $scope.SelectMilitaryBranch = function (data) {
        $rootScope.tempObject.MilitaryBranch = data.Title;
        $(".ProviderTypeSelectAutoList").hide();
        //if (data == "" || data == null) {
        //    $('#militarybrancherrorid').show();
        //}
        //else {
        //    $('#militarybrancherrorid').hide();
        //}
    };

    //to hide error in add military branch section
    //$('#Militarybranchtextboxid').change(function () {
    //    var military = $('#Militarybranchtextboxid').val();
    //    if (military == "" || military == null) {
    //        $('#militarybrancherrorid').show();
    //    }
    //    else {
    //        $('#militarybrancherrorid').hide();
    //    }
    //});


    $rootScope.$on("LoadRequireMasterDataWorkHistory", function () {
        // Calling master data service to get all provider types

        masterDataService.getMasterData(rootDir + "/Profile/MasterData/GetAllProviderTypes").then(function (Providertypes) {
            $scope.ProviderTypes = Providertypes;
        });

        // Calling master data service to get Military Branches

        masterDataService.getMasterData(rootDir + "/Profile/MasterData/GetAllMilitaryBranches").then(function (militaryBranches) {
            $scope.militaryBranches = militaryBranches;
        });

        masterDataService.getMasterData(rootDir + "/Profile/MasterData/GetAllMilitaryRanks").then(function (militaryRanks) {
            $scope.militaryRanks = militaryRanks;
        });

        masterDataService.getMasterData(rootDir + "/Profile/MasterData/GetAllMilitaryPresentDuties").then(function (militaryPresentDuties) {
            $scope.militaryPresentDuties = militaryPresentDuties;
        });

        masterDataService.getMasterData(rootDir + "/Profile/MasterData/GetAllMilitaryDischarges").then(function (militaryDischarges) {
            $scope.militaryDischarges = militaryDischarges;
        });

    });

    //-------------- ANgular sorting filter --------------
    $scope.order = function (predicate, reverse, section) {
        $scope.selectedSection = section;
        $scope.professionalWorkExperiences = orderBy($scope.professionalWorkExperiences, predicate, reverse);
    };

    $scope.IndexValue = 0;

    $scope.professionalWorkExperiences = [];

    // rootScoped on emitted value catches the value for the model and insert to get the old data
    //calling the method using $on(PSP-public subscriber pattern)
    $rootScope.$on('ProfessionalWorkExperiences', function (event, val) {

        $scope.ProfessionalWorkExperiencesPendingRequest = profileUpdates.getUpdates('Work History', 'Professional Work Experience');


        $scope.professionalWorkExperiences = val;

        $scope.selectedSection = 2;
        $scope.predicate = "EndDate";
        $scope.reverse = true;
        $scope.order($scope.predicate, $scope.reverse, $scope.selectedSection);
    });
    //-------------- CV Information data from database ------------------
    $rootScope.$on('CVInformation', function (event, val) {
        $scope.ProfessionalWorkExperiencesPendingRequest = profileUpdates.getUpdates('Work History', 'CV');
        $scope.CVInformation = val;
    });

    //$scope.isCVUploaddiv = true;
    $scope.uploadCV = function (Form_Id) {

        ResetFormForValidation($("#" + Form_Id));
        var url = rootDir + "/Profile/WorkHistory/AddCVAsync?profileId=" + profileId;
        //var form = $('#').find('form');
        var $form = $("#" + Form_Id)[0];
        if ($("#" + Form_Id).valid()) {
            $.ajax({
                url: url,
                type: 'POST',
                data: new FormData($form),
                async: false,
                cache: false,
                contentType: false,
                processData: false,
                success: function (data) {
                    try {
                        if (data.status == "true") {
                            //alert('cv uploaded successfully');
                            //if ($scope.CVInformation != null) {
                            //    $scope.ProfessionalWorkExperiencesPendingRequest = true;
                            //}
                            $scope.CVInformation = data.dataModelCVInformation;

                            //$('#_editCV').modal('hide');
                            $rootScope.operateCancelControl('');
                            messageAlertEngine.callAlertMessage("uploadedCVInformation", "CV Uploaded Successfully !!!!", "success", true);

                            if (data.CVInformationID != 0) {
                                $scope.ProfessionalWorkExperiencesPendingRequest = true;
                            }


                        } else {
                            //messageAlertEngine.callAlertMessage("removeCVUpload", data.status, "danger", true);
                            messageAlertEngine.callAlertMessage("removeCVUpload", "Sorry for Inconvenience !!!! Please Try Again Later...", "danger", true);
                            //$scope.errorCVUpload = "Sorry for Inconvenience !!!! Please Try Again Later...";
                        }
                    } catch (e) {

                    }
                }
            });
        }
    }


    //------------SAVE--------------
    $scope.saveProfessionalWorkExperience = function (professionalWorkExperience, index) {
        //loadingOn();
        var url;
        var form;
        var myData = {};
        if ($scope.visibilityControl == 'addProfessionalWorkExperience') {
            //Add Details - Denote the URL
            form = $('#newProfessionalWorkExperienceFormDiv').find('form');
            url = rootDir + "/Profile/WorkHistory/AddProfessionalWorkExperienceAsync?profileId=" + profileId;
            $('#UpdateHistoryForProfessionalWorkExperience').val(JSON.stringify($rootScope.TrackObjectChanges($rootScope.OldObject, $rootScope.tempObject)));
        }
        else if ($scope.visibilityControl == (index + '_editProfessionalWorkExperience')) {
            //Update Details - Denote the URL
            form = $('#professionalWorkExperienceEditDiv' + index).find('form');
            url = rootDir + "/Profile/WorkHistory/UpdateProfessionalWorkExperienceAsync?profileId=" + profileId;
            $('#UpdateHistoryForProfessionalWorkExperience').val(JSON.stringify($rootScope.TrackObjectChanges($rootScope.OldObject, $rootScope.tempObject)));
        }
        ResetFormForValidation(form);
        validationStatus = form.valid();
        if (validationStatus) {
            $.ajax({
                url: url,
                type: 'POST',
                data: new FormData(form[0]),
                async: false,
                cache: false,
                contentType: false,
                processData: false,
                success: function (data) {
                    try {
                        if (data.status == "true") {

                            data.professionalWorkExperience.StartDate = ConvertDateFormat(data.professionalWorkExperience.StartDate);
                            data.professionalWorkExperience.EndDate = ConvertDateFormat(data.professionalWorkExperience.EndDate);


                            var providerTypeTitle = $(form[0]).find($("[name='ProviderTypeID'] option:selected")).text();
                            if (providerTypeTitle == "--select--") {
                                data.professionalWorkExperience.ProviderType = { ProviderTypeID: null, Title: null };

                            }
                            else {
                                data.professionalWorkExperience.ProviderType = { ProviderTypeID: data.professionalWorkExperience.ProviderTypeID, Title: providerTypeTitle };
                            }

                            if ($scope.visibilityControl != (index + '_editProfessionalWorkExperience')) {
                                //$scope.ProfessionalWorkExperiencesPendingRequest = true;
                                $scope.professionalWorkExperiences.push(data.professionalWorkExperience);
                                $rootScope.visibilityControl = "addedNewProfessionalWorkExperience";
                                messageAlertEngine.callAlertMessage("addedNewProfessionalWorkExperience", "Professional Work experience Information saved successfully !!!!", "success", true);
                            }

                            else {
                                $scope.ProfessionalWorkExperiencesPendingRequest = true;
                                $scope.professionalWorkExperiences[index] = data.professionalWorkExperience;
                                $rootScope.visibilityControl = "updatedProfessionalWorkExperience";
                                messageAlertEngine.callAlertMessage("addedNewProfessionalWorkExperience", "Professional Work experience Information updated successfully !!!!", "success", true);

                            }
                            $scope.order($scope.predicate, $scope.reverse, $scope.selectedSection);
                            myData = data;
                            $scope.IsProfessionalWorkExperienceHasError = false;
                            FormReset(form);


                            $scope.resetDates();
                        } else {
                            messageAlertEngine.callAlertMessage("addedNewProfessionalWorkExperienceError", data.status, "danger", true);
                        }
                    } catch (e) {

                    }
                }
            });
        }

        $rootScope.$broadcast('UpdateProfessionalWorkExperienceDoc', myData);

        //loadingOff();
    };

    $scope.initProfessionalWorkExperienceWarning = function (workExperience) {
        if (angular.isObject(workExperience)) {
            $scope.tempProfessionalWorkExperience = workExperience;
        }
        $('#professionalWorkExperienceWarningModal').modal();
    };

    $scope.removeProfessionalWorkExperience = function (professionalWorkExperiences) {
        var validationStatus = false;
        var url = null;
        var myData = {};
        var $formData = null;
        $scope.isRemoved = true;
        $formData = $('#removeProfessionalWorkExperience');
        url = rootDir + "/Profile/WorkHistory/RemoveProfessionalWorkExperienceAsync?profileId=" + profileId;
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
                            var obj = $filter('filter')(professionalWorkExperiences, { ProfessionalWorkExperienceID: data.professionalWorkExperience.ProfessionalWorkExperienceID })[0];
                            professionalWorkExperiences.splice(professionalWorkExperiences.indexOf(obj), 1);

                            $scope.order($scope.predicate, $scope.reverse, $scope.selectedSection);

                            if ($scope.dataFetchedPWE == true) {
                                obj.HistoryStatus = 'Deleted';
                                obj.DeletedBy = data.UserName;
                                obj.DeletedDate = moment(new Date).format('MM/DD/YYYY, h:mm:ss a');
                                $scope.professionalWorkExperienceHistoryArray.push(obj);
                            }
                            $scope.isRemoved = false;
                            $('#professionalWorkExperienceWarningModal').modal('hide');
                            $rootScope.operateCancelControl('');
                            myData = data;
                            messageAlertEngine.callAlertMessage("addedNewProfessionalWorkExperience", "Professional Work Experience Removed successfully.", "success", true);
                        } else {
                            $('#professionalWorkExperienceWarningModal').modal('hide');
                            messageAlertEngine.callAlertMessage("removeProfessionalWorkExperience", data.status, "danger", true);
                            $scope.errorProfessionalWorkExperience = "Sorry for Inconvenience !!!! Please Try Again Later...";
                        }
                    } catch (e) {

                    }
                },
                error: function (e) {

                }
            });
        }
        $rootScope.$broadcast('RemoveProfessionalWorkExperienceDoc', myData);
    };

    $scope.publicHealthServices = [];

    // rootScoped on emited value catches the value for the model and insert to get the old data
    $rootScope.$on('PublicHealthServices', function (event, val) {

        $scope.PublicHealthServicesPendingRequest = profileUpdates.getUpdates('Work History', 'Public Health Service');


        $scope.publicHealthServices = val;
        //for (var i = 0; i < $scope.publicHealthServices.length ; i++) {
        //    $scope.publicHealthServices[i].StartDate = ConvertDateFormat($scope.publicHealthServices[i].StartDate);
        //    $scope.publicHealthServices[i].EndDate = ConvertDateFormat($scope.publicHealthServices[i].EndDate);
        //}
    });

    $scope.savePublicHealthService = function (publicHealthService, index) {
        //loadingOn();
        var url;
        var form;

        if ($scope.visibilityControl == 'addPublicHealthService') {
            //Add Details - Denote the URL
            form = $('#newPublicHealthServiceFormDiv').find('form');
            url = rootDir + "/Profile/WorkHistory/AddPublicHealthServiceAsync?profileId=" + profileId;
            $('#UpdateHistoryForPublicHealthService').val(JSON.stringify($rootScope.TrackObjectChanges($rootScope.OldObject, $rootScope.tempObject)));
        }
        else if ($scope.visibilityControl == (index + '_editPublicHealthService')) {
            //Update Details - Denote the URL
            form = $('#publicHealthServiceEditDiv' + index).find('form');
            url = rootDir + "/Profile/WorkHistory/UpdatePublicHealthServiceAsync?profileId=" + profileId;
            $('#UpdateHistoryForPublicHealthService').val(JSON.stringify($rootScope.TrackObjectChanges($rootScope.OldObject, $rootScope.tempObject)));
        }

        ResetFormForValidation(form);

        if (form.valid()) {

            $.ajax({
                url: url,
                type: 'POST',
                data: new FormData(form[0]),
                async: false,
                cache: false,
                contentType: false,
                processData: false,
                success: function (data) {
                    try {
                        if (data.status == "true") {
                            data.publicHealthService.StartDate = ConvertDateFormat(data.publicHealthService.StartDate);
                            data.publicHealthService.EndDate = ConvertDateFormat(data.publicHealthService.EndDate);
                            if ($scope.visibilityControl != (index + '_editPublicHealthService')) {
                                $scope.publicHealthServices.push(data.publicHealthService);
                                $rootScope.visibilityControl = "addedNewPublicHealthService";
                                messageAlertEngine.callAlertMessage("addedNewPublicHealthService", "Public Health Service Information saved successfully !!!!", "success", true);
                            }
                            else {
                                $scope.PublicHealthServicesPendingRequest = true;
                                $scope.publicHealthServices[index] = data.publicHealthService;
                                $rootScope.visibilityControl = "updatedPublicHealthService"
                                messageAlertEngine.callAlertMessage("addedNewPublicHealthService", "Public Health Service Information updated successfully !!!!", "success", true);
                            }
                            $scope.IsPublicHealthServiceHasError = false;
                            FormReset(form);


                            $scope.resetDates();
                        } else {
                            $scope.IsPublicHealthServiceHasError = true;
                            messageAlertEngine.callAlertMessage("publicHealthServiceError", data.status, "danger", true);
                            $scope.PublicHealthServiceErrorList = data.status.split(",");
                            //$rootScope.tempObject = "";


                        }
                    } catch (e) {

                    }
                }
            });

            //loadingOff();
            //$scope.resetDates();
        }
    };


    //$$$$$$$$$$$$$$$$$$$ Work History History#################################

    $scope.militaryServiceInformationHistoryArray = [];
    $scope.showMilitaryServiceInformationTable = false;





    $scope.showMilitaryServiceInformationHistory = function (loadingId) {
        if ($scope.militaryServiceInformationHistoryArray.length == 0) {
            $("#" + loadingId).css('display', 'block');
            var url = rootDir + "/Profile/ProfileHistory/GetAllMilitaryServiceInformationHistory?profileId=" + profileId;
            $http.get(url).success(function (data) {
                try {
                    $scope.militaryServiceInformationHistoryArray = data;
                    $rootScope.GetAllUserData();
                    for (var j = 0; j < $scope.militaryServiceInformationHistoryArray.length; j++) {
                        for (var i = 0; i < $rootScope.userslist.length; i++) {
                            if ($scope.militaryServiceInformationHistoryArray[j].DeletedById != null) {
                                if ($rootScope.userslist[i].CDUserID == $scope.militaryServiceInformationHistoryArray[j].DeletedById) {
                                    if ($rootScope.userslist[i].FullName != null) {
                                        $scope.militaryServiceInformationHistoryArray[j].DeletedBy = $rootScope.userslist[i].FullName;
                                        break;
                                    }
                                    else {
                                        $scope.militaryServiceInformationHistoryArray[j].DeletedBy = $rootScope.userslist[i].UserName;
                                        break;
                                    }
                                }
                            }
                        }
                        if ($scope.militaryServiceInformationHistoryArray[j].DeletedDate != null) {
                            var date = moment.utc($scope.militaryServiceInformationHistoryArray[j].DeletedDate).toDate();
                            $scope.militaryServiceInformationHistoryArray[j].DeletedDate = moment(date).format('MM/DD/YYYY, h:mm:ss a');
                        }
                    }
                    $scope.showMilitaryServiceInformationTable = true;
                    $("#" + loadingId).css('display', 'none');
                } catch (e) {

                }
            });
        }
        else {
            $scope.showMilitaryServiceInformationTable = true;
        }

    }

    $scope.cancelmilitaryServiceInformationHistory = function () {
        $scope.showMilitaryServiceInformationTable = false;
    }

    //--------------------------***--------------------------------------

    $scope.professionalWorkExperienceHistoryArray = [];
    $scope.dataFetchedPWE = false;

    $scope.showProfessionalWorkExperienceHistory = function (loadingId) {
        if ($scope.professionalWorkExperienceHistoryArray.length == 0) {
            $("#" + loadingId).css('display', 'block');
            var url = rootDir + "/Profile/ProfileHistory/GetAllProfessionalWorkExperienceHistory?profileId=" + profileId;
            $http.get(url).success(function (data) {
                try {
                    $scope.professionalWorkExperienceHistoryArray = data;
                    $scope.showProfessionalWorkExperienceHistoryTable = true;
                    $scope.dataFetchedPWE = true;
                    $rootScope.GetAllUserData();
                    for (var j = 0; j < $scope.professionalWorkExperienceHistoryArray.length; j++) {
                        for (var i = 0; i < $rootScope.userslist.length; i++) {
                            if ($scope.professionalWorkExperienceHistoryArray[j].DeletedById != null) {
                                if ($rootScope.userslist[i].CDUserID == $scope.professionalWorkExperienceHistoryArray[j].DeletedById) {
                                    if ($rootScope.userslist[i].FullName != null) {
                                        $scope.professionalWorkExperienceHistoryArray[j].DeletedBy = $rootScope.userslist[i].FullName;
                                        break;
                                    }
                                    else {
                                        $scope.professionalWorkExperienceHistoryArray[j].DeletedBy = $rootScope.userslist[i].UserName;
                                        break;
                                    }
                                }
                            }
                        }
                        if ($scope.professionalWorkExperienceHistoryArray[j].DeletedDate != null) {
                            var date = moment.utc($scope.professionalWorkExperienceHistoryArray[j].DeletedDate).toDate();
                            $scope.professionalWorkExperienceHistoryArray[j].DeletedDate = moment(date).format('MM/DD/YYYY, h:mm:ss a');
                        }
                    }
                    $("#" + loadingId).css('display', 'none');
                } catch (e) {

                }
            });
        } else {
            $scope.showProfessionalWorkExperienceHistoryTable = true;
        }

    }

    $scope.cancelProfessionalWorkExperienceHistory = function () {
        $scope.showProfessionalWorkExperienceHistoryTable = false;
    }

    //-------------------------------***----------------------------------
    $scope.publicHealthServiceHistoryArray = [];
    $scope.dataFetchedPHS = false;

    $scope.showPublicHealthServiceHistory = function (loadingId) {
        if ($scope.publicHealthServiceHistoryArray.length == 0) {
            $("#" + loadingId).css('display', 'block');
            var url = rootDir + "/Profile/ProfileHistory/GetAllPublicHealthServiceHistory?profileId=" + profileId;
            $http.get(url).success(function (data) {
                try {
                    $scope.publicHealthServiceHistoryArray = data;
                    $rootScope.GetAllUserData();
                    for (var j = 0; j < $scope.publicHealthServiceHistoryArray.length; j++) {
                        for (var i = 0; i < $rootScope.userslist.length; i++) {
                            if ($scope.publicHealthServiceHistoryArray[j].DeletedById != null) {
                                if ($rootScope.userslist[i].CDUserID == $scope.publicHealthServiceHistoryArray[j].DeletedById) {
                                    if ($rootScope.userslist[i].FullName != null) {
                                        $scope.publicHealthServiceHistoryArray[j].DeletedBy = $rootScope.userslist[i].FullName;
                                        break;
                                    }
                                    else {
                                        $scope.publicHealthServiceHistoryArray[j].DeletedBy = $rootScope.userslist[i].UserName;
                                        break;
                                    }
                                }
                            }
                        }
                        if ($scope.publicHealthServiceHistoryArray[j].DeletedDate != null) {
                            var date = moment.utc($scope.publicHealthServiceHistoryArray[j].DeletedDate).toDate();
                            $scope.publicHealthServiceHistoryArray[j].DeletedDate = moment(date).format('MM/DD/YYYY, h:mm:ss a');
                        }
                    }
                    $scope.showPublicHealthServiceTable = true;
                    $scope.dataFetchedPHS = true;
                    $("#" + loadingId).css('display', 'none');
                } catch (e) {

                }
            });
        }
        else {
            $scope.showPublicHealthServiceTable = true;
        }

    }

    $scope.cancelPublicHealthServiceHistory = function () {
        $scope.showPublicHealthServiceTable = false;
    }

    //------------------***--------------------------------------------------

    $scope.workGapHistoryArray = [];
    $scope.dataFetchedWG = false;

    $scope.showWorkGapHistory = function (loadingId) {
        if ($scope.workGapHistoryArray.length == 0) {
            $("#" + loadingId).css('display', 'block');
            var url = rootDir + "/Profile/ProfileHistory/GetAllWorkGapHistory?profileId=" + profileId;
            $http.get(url).success(function (data) {
                try {
                    $scope.workGapHistoryArray = data;
                    $rootScope.GetAllUserData();
                    for (var j = 0; j < $scope.workGapHistoryArray.length; j++) {
                        for (var i = 0; i < $rootScope.userslist.length; i++) {
                            if ($scope.workGapHistoryArray[j].DeletedById != null) {
                                if ($rootScope.userslist[i].CDUserID == $scope.workGapHistoryArray[j].DeletedById) {
                                    if ($rootScope.userslist[i].FullName != null) {
                                        $scope.workGapHistoryArray[j].DeletedBy = $rootScope.userslist[i].FullName;
                                        break;
                                    }
                                    else {
                                        $scope.workGapHistoryArray[j].DeletedBy = $rootScope.userslist[i].UserName;
                                        break;
                                    }
                                }
                            }
                        }
                        if ($scope.workGapHistoryArray[j].DeletedDate != null) {
                            var date = moment.utc($scope.workGapHistoryArray[j].DeletedDate).toDate();
                            $scope.workGapHistoryArray[j].DeletedDate = moment(date).format('MM/DD/YYYY, h:mm:ss a');
                        }
                    }
                    $scope.showWorkGapHistoryTable = true;
                    $scope.dataFetchedWG = true;
                    $("#" + loadingId).css('display', 'none');
                } catch (e) {

                }
            });
        }
        else {
            $scope.showWorkGapHistoryTable = true;
        }

    }

    $scope.cancelWorkGapHistory = function () {
        $scope.showWorkGapHistoryTable = false;
    }


    //$$$$$$$$$$$$$$$$$$$ Work History History End#################################


    $scope.initPublicHealthServiceWarning = function (publicHealthService) {
        if (angular.isObject(publicHealthService)) {
            $scope.tempPublicHealthService = publicHealthService;
        }
        $('#publicHealthServiceWarningModal').modal();
    };

    $scope.removePublicHealthService = function (publicHealthServices) {
        var validationStatus = false;
        var url = null;
        var $formData = null;
        $scope.isRemoved = true;
        $formData = $('#removePublicHealthService');
        url = rootDir + "/Profile/WorkHistory/RemovePublicHealthServiceAsync?profileId=" + profileId;
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
                            var obj = $filter('filter')(publicHealthServices, { PublicHealthServiceID: data.publicHealthService.PublicHealthServiceID })[0];
                            publicHealthServices.splice(publicHealthServices.indexOf(obj), 1);
                            if ($scope.dataFetchedPHS == true) {
                                obj.HistoryStatus = 'Deleted';
                                obj.DeletedBy = data.UserName;
                                obj.DeletedDate = moment(new Date).format('MM/DD/YYYY, h:mm:ss a');
                                $scope.publicHealthServiceHistoryArray.push(obj);
                            }
                            $scope.isRemoved = false;
                            $('#publicHealthServiceWarningModal').modal('hide');
                            $rootScope.operateCancelControl('');
                            messageAlertEngine.callAlertMessage("addedNewPublicHealthService", "Public Health Service Removed successfully.", "success", true);
                        } else {
                            $('#publicHealthServiceWarningModal').modal('hide');
                            messageAlertEngine.callAlertMessage("removePublicHealthService", data.status, "danger", true);
                            $scope.errorPublicHealthService = "Sorry for Inconvenience !!!! Please Try Again Later...";
                        }
                    } catch (e) {

                    }
                },
                error: function (e) {

                }
            });
        }
    };

    $scope.workGaps = [];

    // rootScoped on emited value catches the value for the model and insert to get the old data
    $rootScope.$on('WorkGaps', function (event, val) {
        $scope.WorkGapsPendingRequest = profileUpdates.getUpdates('Work History', 'Work Gap');


        $scope.workGaps = val;
        //for (var i = 0; i < $scope.workGaps.length ; i++) {
        //    $scope.workGaps[i].StartDate = ConvertDateFormat($scope.workGaps[i].StartDate);
        //    $scope.workGaps[i].EndDate = ConvertDateFormat($scope.workGaps[i].EndDate);
        //}
    });
    $scope.AutoHideError = function () {

        $scope.IsWorkGapHasError = false;
    };
    $scope.custHidefun = function () {
        $scope.IsPublicHealthServiceHasError = false;
    }
    $scope.saveWorkGap = function (workGap, index) {
        //loadingOn();
        var url;
        var form;

        if ($scope.visibilityControl == 'addWorkGap') {
            //Add Details - Denote the URL
            form = $('#newWorkGapFormDiv').find('form');
            url = rootDir + "/Profile/WorkHistory/AddWorkGapAsync?profileId=" + profileId;
            $('#UpdateHistoryForWorkGap').val(JSON.stringify($rootScope.TrackObjectChanges($rootScope.OldObject, $rootScope.tempObject)));
        }
        else if ($scope.visibilityControl == (index + '_editWorkGap')) {
            //Update Details - Denote the URL
            form = $('#workGapEditDiv' + index).find('form');
            url = rootDir + "/Profile/WorkHistory/UpdateWorkGapAsync?profileId=" + profileId;
            $('#UpdateHistoryForWorkGap').val(JSON.stringify($rootScope.TrackObjectChanges($rootScope.OldObject, $rootScope.tempObject)));
        }

        ResetFormForValidation(form);

        if (form.valid()) {

            $.ajax({
                url: url,
                type: 'POST',
                data: new FormData(form[0]),
                async: false,
                cache: false,
                contentType: false,
                processData: false,
                success: function (data) {
                    try {
                        if (data.status == "true") {
                            data.workGap.StartDate = ConvertDateFormat(data.workGap.StartDate);
                            data.workGap.EndDate = ConvertDateFormat(data.workGap.EndDate);
                            if ($scope.visibilityControl != (index + '_editWorkGap')) {
                                $scope.workGaps.push(data.workGap);
                                $rootScope.visibilityControl = "addedNewWorkGap";
                                messageAlertEngine.callAlertMessage("addedNewWorkGap", "Work Gap Information saved successfully !!!!", "success", true);
                            }
                            else {
                                $scope.WorkGapsPendingRequest = true;
                                $scope.workGaps[index] = data.workGap;
                                $rootScope.visibilityControl = "updatedWorkGap";
                                messageAlertEngine.callAlertMessage("addedNewWorkGap", "Work Gap Information updated successfully !!!!", "success", true);
                            }
                            $scope.IsWorkGapHasError = false;
                            FormReset(form);

                            $scope.resetDates();
                        } else {
                            $scope.IsWorkGapHasError = true;
                            $scope.WorkGapErrorList = data.status.split(",");
                            messageAlertEngine.callAlertMessage("workGapError", data.status, "danger", true);
                        }
                    } catch (e) {

                    }
                },
                error: function (data) {

                }
            });
            //loadingOff();
            //$scope.resetDates();
        }
    };

    $scope.initWorkGapWarning = function (workGap) {
        if (angular.isObject(workGap)) {
            $scope.tempWorkGap = workGap;
        }
        $('#workGapWarningModal').modal();
    };

    $scope.removeWorkGap = function (workGaps) {
        var validationStatus = false;
        var url = null;
        var $formData = null;
        $scope.isRemoved = true;
        $formData = $('#editWorkGap');
        url = rootDir + "/Profile/WorkHistory/RemoveWorkGap?profileId=" + profileId;
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
                            var obj = $filter('filter')(workGaps, { WorkGapID: data.workGap.WorkGapID })[0];
                            workGaps.splice(workGaps.indexOf(obj), 1);
                            if ($scope.dataFetchedWG == true) {
                                obj.HistoryStatus = 'Deleted';
                                obj.DeletedBy = data.UserName;
                                obj.DeletedDate = moment(new Date).format('MM/DD/YYYY, h:mm:ss a');
                                $scope.workGapHistoryArray.push(obj);
                            }
                            $scope.isRemoved = false;
                            $('#workGapWarningModal').modal('hide');
                            //PracticeLocationDetail.SupervisingProviders.splice();
                            $rootScope.operateCancelControl('');
                            messageAlertEngine.callAlertMessage("addedNewWorkGap", "Work Gap Removed successfully.", "success", true);
                        } else {
                            $('#workGapWarningModal').modal('hide');
                            messageAlertEngine.callAlertMessage("removeWorkGap", data.status, "danger", true);
                            $scope.errorWorkGap = "Sorry for Inconvenience !!!! Please Try Again Later...";
                        }
                    } catch (e) {

                    }
                },
                error: function (e) {

                }
            });
        }
    };

    $scope.militaryServiceInformations = [];

    // rootScoped on emited value catches the value for the model and insert to get the old data
    $rootScope.$on('MilitaryServiceInformations', function (event, val) {

        $scope.MilitaryServiceInformationsPendingRequest = profileUpdates.getUpdates('Work History', 'Military Service Information');

        $scope.militaryServiceInformations = val;
        for (var i = 0; i < $scope.militaryServiceInformations.length ; i++) {
            //$scope.militaryServiceInformations[i].StartDate = ConvertDateFormat($scope.militaryServiceInformations[i].StartDate);
            //$scope.militaryServiceInformations[i].DischargeDate = ConvertDateFormat($scope.militaryServiceInformations[i].DischargeDate);

            if (!$scope.militaryServiceInformations[i].MilitaryDischarge) { $scope.militaryServiceInformations[i].MilitaryDischarge = ""; }
            if (!$scope.militaryServiceInformations[i].MilitaryPresentDuty) { $scope.militaryServiceInformations[i].MilitaryPresentDuty = ""; }
            if (!$scope.militaryServiceInformations[i].MilitaryRank) { $scope.militaryServiceInformations[i].MilitaryRank = ""; }
        }
    });

    // Method for Asyn saving Military Service Information
    $scope.$watch('tempObject.MilitaryBranch', function (newV,oldV) {
        if (newV == oldV) return;
        if (newV == "") {
            $scope.errormessageformilitarybranch = true;
        }
        else { $scope.errormessageformilitarybranch = false; }
    });
    $scope.errormessageformilitarybranch = false;
    $scope.saveMilitaryServiceInformation = function (militaryServiceInformation, index) {
        //loadingOn();
        var url;
        var $form;
        $scope.errormessageformilitarybranch = false;
        if ($scope.visibilityControl == 'addMilitaryServiceInformation') {
            //Add Details - Denote the URL
            $form = $('#newMilitaryServiceInformationFormDiv').find('form');
            //ResetFormForValidation(form);
            url = rootDir + "/Profile/WorkHistory/AddMilitaryServiceInformationAsync?profileId=" + profileId;
            $('#UpdateHistoryForMilitaryServiceInformation').val(JSON.stringify($rootScope.TrackObjectChanges($rootScope.OldObject, $rootScope.tempObject)));
        }
        else if ($scope.visibilityControl == (index + '_editMilitaryServiceInformation')) {
            //Update Details - Denote the URL
            $form = $('#militaryServiceInformationEditDiv' + index).find('form');
            url = rootDir + "/Profile/WorkHistory/UpdateMilitaryServiceInformationAsync?profileId=" + profileId;
            $('#UpdateHistoryForMilitaryServiceInformation').val(JSON.stringify($rootScope.TrackObjectChanges($rootScope.OldObject, $rootScope.tempObject)));
        }
        if ($($form).find($('[name=MilitaryBranch]')).val() == "") { $scope.errormessageformilitarybranch = true; }
        ResetFormForValidation($form);
        //FormReset(form);

        if ($form.valid() && !$scope.errormessageformilitarybranch) {

            $.ajax({
                url: url,
                type: 'POST',
                data: new FormData($form[0]),
                async: false,
                cache: false,
                contentType: false,
                processData: false,
                success: function (data) {
                    try {
                        if (data.status == "true") {
                            if (!data.militaryServiceInformation.MilitaryDischarge) { data.militaryServiceInformation.MilitaryDischarge = ""; }
                            if (!data.militaryServiceInformation.MilitaryPresentDuty) { data.militaryServiceInformation.MilitaryPresentDuty = ""; }
                            if (!data.militaryServiceInformation.MilitaryRank) { data.militaryServiceInformation.MilitaryRank = ""; }
                            data.militaryServiceInformation.StartDate = ConvertDateFormat(data.militaryServiceInformation.StartDate);
                            data.militaryServiceInformation.DischargeDate = ConvertDateFormat(data.militaryServiceInformation.DischargeDate);
                            if ($scope.visibilityControl != (index + '_editMilitaryServiceInformation')) {
                                $scope.militaryServiceInformations.push(data.militaryServiceInformation);
                                $rootScope.operateCancelControl('');
                                $rootScope.visibilityControl = "addedNewMilitaryServiceInformation";
                                messageAlertEngine.callAlertMessage("addedNewMilitaryServiceInformation", "Military Service Information saved successfully !!!!", "success", true);
                            }
                            else {
                                $scope.MilitaryServiceInformationsPendingRequest = true;
                                $scope.militaryServiceInformations[index] = data.militaryServiceInformation;
                                $rootScope.operateCancelControl('');
                                $rootScope.visibilityControl = "updatedMilitaryServiceInformation";
                                messageAlertEngine.callAlertMessage("addedNewMilitaryServiceInformation", "Military Service Information updated successfully !!!!", "success", true);
                            }
                            $scope.IsMilitaryServiceInfoHasError = false;
                            FormReset(form);

                            $scope.resetDates();
                        } else {
                            $scope.IsMilitaryServiceInfoHasError = true;
                            //$scope.MilitaryServiceInfoErrorList = data.status.split(",");
                            messageAlertEngine.callAlertMessage("MilitaryserviceError", data.status, "danger", true);
                        }
                    } catch (e) {

                    }
                }

            });

            //loadingOff();
            $scope.resetDates();
        }

    };

    $scope.initMilitaryServiceInformationWarning = function (militaryService) {
        if (angular.isObject(militaryService)) {
            $scope.tempMilitaryServiceInformation = militaryService;
        }
        $('#militaryServiceInformationWarningModal').modal();
    };

    $scope.removeMilitaryServiceInformation = function (militaryServiceInformations) {
        var validationStatus = false;
        var url = null;
        var $formData = null;
        $scope.isRemoved = true;
        $formData = $('#removeMilitaryServiceInformation');
        url = rootDir + "/Profile/WorkHistory/RemoveMilitaryServiceInformationAsync?profileId=" + profileId;
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
                            var obj = $filter('filter')(militaryServiceInformations, { MilitaryServiceInformationID: data.militaryServiceInformation.MilitaryServiceInformationID })[0];
                            militaryServiceInformations.splice(militaryServiceInformations.indexOf(obj), 1);

                            obj.HistoryStatus = 'Deleted';
                            obj.DeletedBy = data.UserName;
                            obj.DeletedDate = moment(new Date).format('MM/DD/YYYY, h:mm:ss a');
                            $scope.militaryServiceInformationHistoryArray.push(obj);
                            $scope.isRemoved = false;
                            $('#militaryServiceInformationWarningModal').modal('hide');
                            $rootScope.operateCancelControl('');
                            messageAlertEngine.callAlertMessage("addedNewMilitaryServiceInformation", "Military Service Information Removed successfully.", "success", true);
                        } else {
                            $('#militaryServiceInformationWarningModal').modal('hide');
                            messageAlertEngine.callAlertMessage("removeMilitaryServiceInformation", data.status, "danger", true);
                            $scope.errorMilitaryServiceInformation = "Sorry for Inconvenience !!!! Please Try Again Later...";
                        }
                    } catch (e) {

                    }
                },
                error: function (e) {

                }
            });
        }
    };

    $rootScope.WorkHistoryLoaded = true;
    $scope.dataLoaded = false;
    $rootScope.$on('WorkHistory', function () {
        if (!$scope.dataLoaded) {
            $rootScope.WorkHistoryLoaded = false;
            $http({
                method: 'GET',
                url: rootDir + '/Profile/MasterProfile/GetWorkHistoriesProfileDataAsync?profileId=' + profileId
            }).success(function (data, status, headers, config) {
                try {
                    for (key in data) {
                        $rootScope.$emit(key, data[key]);
                        //call respective controller to load data (PSP)
                    }
                    $rootScope.WorkHistoryLoaded = true;
                    $rootScope.$broadcast("LoadRequireMasterDataWorkHistory");
                } catch (e) {
                    $rootScope.WorkHistoryLoaded = true;
                }

            }).error(function (data, status, headers, config) {
                $rootScope.WorkHistoryLoaded = true;
            });
            $scope.dataLoaded = true;
        }
    });

}]);
