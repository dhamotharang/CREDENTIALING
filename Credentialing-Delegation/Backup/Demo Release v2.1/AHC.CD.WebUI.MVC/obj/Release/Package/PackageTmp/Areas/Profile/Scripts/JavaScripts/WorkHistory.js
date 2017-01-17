
profileApp.controller('WorkHistoryController', ['$scope', '$rootScope', '$http', '$filter', 'masterDataService', 'locationService', 'messageAlertEngine', function ($scope, $rootScope, $http, $filter, masterDataService, locationService, messageAlertEngine) {

    //Get all the data for the Work History on document on ready
    //$(document).ready(function () {
    //    $("#Loading_Message").text("Gathering Profile Data...");
    //    //console.log("Getting data....");
    //    $http({
    //        method: 'GET',
    //        url: '/Profile/MasterProfile/GetWorkHistoriesProfileDataAsync?profileId=' + profileId
    //    }).success(function (data, status, headers, config) {
    //        //console.log(data);
    //        try {
    //            for (key in data) {
    //                //console.log(key);
    //                $rootScope.$emit(key, data[key]);
    //                //call respective controller to load data (PSP)
    //            }
    //            $rootScope.WorkHistoryLoaded = true;
    //            //$rootScope.$broadcast("LoadRequireMasterData");
    //        } catch (e) {
    //            //console.log("error getting data back");
    //            $rootScope.WorkHistoryLoaded = true;
    //        }

    //    }).error(function (data, status, headers, config) {
    //        //console.log(status);
    //        $rootScope.WorkHistoryLoaded = true;
    //    });
    //});

    // resetting the Date elements in work history scope

    $scope.resetDates = function () {

        try{
            $scope.tempObject.StartDate = new Date();
            $scope.tempObject.EndDate = new Date(); 
            $scope.tempObject.DischargeDate = new Date();
        }catch(e){
        }

    }

    // new city autofill code

    $scope.addressAutocomplete = function (location) {
        if (location.length == 0) {
            $scope.resetAddressModels();
        }
        $scope.tempObject.CityOfBirth = location;
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
        $scope.tempObject.City = "";
        $scope.tempObject.State = "";
        $scope.tempObject.Country = "";
    };

    $scope.setAddressModels = function (location) {
        $scope.tempObject.City = location.City;
        $scope.tempObject.State = location.State;
        $scope.tempObject.Country = location.Country;

    }

    /*
       Method addressAutocomplete() gets the details of a location
       Method takes input of location details entered in the text box.
    */
    //$scope.addressAutocomplete = function (location) {
       
    //    $scope.tempObject.City = location;
    //    if (location.length > 2 && !angular.isObject(location)) {         //As soon as the length of the string reaches 3 and the location is not an object
    //        $scope.Locations = locationService.getLocations(location);
    //        $scope.resetAddressModels();//A call is made to the locations service which returns a list of relevant locations
    //        //console.log($scope.Locations);

    //    }
    //    else if (angular.isObject(location)) {                      //When user select a city the location variable then holds the object of the respective location.
    //        if (!$scope.tempObject) {
    //            $scope.tempObject = {};
    //        }
    //        $scope.setAddressModels(location);
    //    }
    //};

    //$scope.resetAddressModels = function () {
    //    $scope.tempObject.City = "";
    //    $scope.tempObject.State = "";
    //    $scope.tempObject.Country = "";
    //};

    //$scope.setAddressModels = function (location) {
    //    $scope.tempObject.City = location.City;
    //    $scope.tempObject.State = location.State;
    //    $scope.tempObject.Country = location.Country;

    //}

    // Assigning country dial codes to scope variable

    $scope.CountryDialCodes = countryDailCodes;

    // Function to pop up the country code select for passed div ID

    $scope.showContryCodeDiv = function (countryCodeDivId) {
        changeVisibilityOfCountryCode();
        $("#" + countryCodeDivId).show();
    };

    // Function called on changing the "Currently Working Here" option. Sets the EndDate & DepartureReason to null.

    $scope.currentlyWorkingChange = function (newValue) {
        if (newValue == 1) { // Checks based on the Enum value 1.Yes , 2.NO
            $rootScope.tempObject.EndDate = null;
            $rootScope.tempObject.DepartureReason = null;
        }
    }

    $scope.militaryDischargeChange = function (newValue) {
        if (newValue !='Other than honourable') { // Checks based on the selected discharge type value
            $rootScope.tempObject.HonorableExplanation = null;
        }
    }


    $rootScope.$on("LoadRequireMasterDataWorkHistory", function () {
        // Calling master data service to get all provider types

        masterDataService.getMasterData("/Profile/MasterData/GetAllProviderTypes").then(function (Providertypes) {
            $scope.ProviderTypes = Providertypes;
        });

        // Calling master data service to get Military Branches

        masterDataService.getMasterData("/Profile/MasterData/GetAllMilitaryBranches").then(function (militaryBranches) {
            $scope.militaryBranches = militaryBranches;
        });

        masterDataService.getMasterData("/Profile/MasterData/GetAllMilitaryRanks").then(function (militaryRanks) {
            $scope.militaryRanks = militaryRanks;
        });

        masterDataService.getMasterData("/Profile/MasterData/GetAllMilitaryPresentDuties").then(function (militaryPresentDuties) {
            $scope.militaryPresentDuties = militaryPresentDuties;
        });

        masterDataService.getMasterData("/Profile/MasterData/GetAllMilitaryDischarges").then(function (militaryDischarges) {
            $scope.militaryDischarges = militaryDischarges;
        });

    });


    $scope.IndexValue = 0;

    $scope.professionalWorkExperiences = [];

    // rootScoped on emitted value catches the value for the model and insert to get the old data
    //calling the method using $on(PSP-public subscriber pattern)
    $rootScope.$on('ProfessionalWorkExperiences', function (event, val) {
        
        $scope.professionalWorkExperiences = val;
        //for (var i = 0; i < $scope.professionalWorkExperiences.length ; i++) {
        //    $scope.professionalWorkExperiences[i].StartDate = ConvertDateFormat($scope.professionalWorkExperiences[i].StartDate);
        //    $scope.professionalWorkExperiences[i].EndDate = ConvertDateFormat($scope.professionalWorkExperiences[i].EndDate);
            
        //}
        //console.log("00000000000000000000");
        //console.log($scope.professionalWorkExperiences);
    });
    //-------------- CV Information data from database ------------------
    $rootScope.$on('CVInformation', function (event, val) {
        $scope.CVInformation = val;
    });

    //------------SAVE--------------
    $scope.saveProfessionalWorkExperience = function (professionalWorkExperience, index) {
        //loadingOn();
        var url;
        var form;

        if ($scope.visibilityControl == 'addProfessionalWorkExperience') {
            //Add Details - Denote the URL
            form = $('#newProfessionalWorkExperienceFormDiv').find('form');
            url = "/Profile/WorkHistory/AddProfessionalWorkExperienceAsync?profileId=" + profileId;
        }
        else if ($scope.visibilityControl == (index + '_editProfessionalWorkExperience')) {
            //Update Details - Denote the URL
            form = $('#professionalWorkExperienceEditDiv' + index).find('form');
            url = "/Profile/WorkHistory/UpdateProfessionalWorkExperienceAsync?profileId=" + profileId;
            ResetFormForValidation(form);
        }
        
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
                    if (data.status == "true") {

                        data.professionalWorkExperience.StartDate = ConvertDateFormat(data.professionalWorkExperience.StartDate);
                        data.professionalWorkExperience.EndDate = ConvertDateFormat(data.professionalWorkExperience.EndDate);

                        var providerTypeTitle = $(form[0]).find($("[name='ProviderTypeID'] option:selected")).text();

                        data.professionalWorkExperience.ProviderType = { ProviderTypeID: data.professionalWorkExperience.ProviderTypeID, Title: providerTypeTitle };

                        if ($scope.visibilityControl != (index + '_editProfessionalWorkExperience')) {
                            $scope.professionalWorkExperiences.push(data.professionalWorkExperience);
                            $rootScope.visibilityControl = "addedNewProfessionalWorkExperience";
                            messageAlertEngine.callAlertMessage("addedNewProfessionalWorkExperience", "Professional Work experience Information saved successfully !!!!", "success", true);
                        }

                        else {
                            $scope.professionalWorkExperiences[index] = data.professionalWorkExperience;
                            $rootScope.visibilityControl = "updatedProfessionalWorkExperience";
                            messageAlertEngine.callAlertMessage("addedNewProfessionalWorkExperience", "Professional Work experience Information updated successfully !!!!", "success", true);

                        }

                        $scope.IsProfessionalWorkExperienceHasError = false;
                        FormReset(form);

                        
                        $scope.resetDates();
                    } else {
                        messageAlertEngine.callAlertMessage("addedNewProfessionalWorkExperience", data.status, "danger", true); 
                    }
                }
            });
        }
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
        var $formData = null;
        $formData = $('#removeProfessionalWorkExperience');
        url = "/Profile/WorkHistory/RemoveProfessionalWorkExperienceAsync?profileId=" + profileId;
        ResetFormForValidation($formData);
        console.log($formData);
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
                    //console.log(data.status);
                    if (data.status == "true") {
                        var obj = $filter('filter')(professionalWorkExperiences, { ProfessionalWorkExperienceID: data.professionalWorkExperience.ProfessionalWorkExperienceID })[0];
                        professionalWorkExperiences.splice(professionalWorkExperiences.indexOf(obj), 1);
                        if ($scope.dataFetchedPWE == true) {
                            obj.HistoryStatus = 'Deleted';
                            $scope.professionalWorkExperienceHistoryArray.push(obj);
                        }
                        $('#professionalWorkExperienceWarningModal').modal('hide');
                        $rootScope.operateCancelControl('');
                        messageAlertEngine.callAlertMessage("addedNewProfessionalWorkExperience", "Professional Work Experience Removed successfully.", "success", true);
                    } else {
                        $('#professionalWorkExperienceWarningModal').modal('hide');
                        messageAlertEngine.callAlertMessage("removeProfessionalWorkExperience", data.status, "danger", true);
                        $scope.errorProfessionalWorkExperience = "Sorry for Inconvenience !!!! Please Try Again Later...";
                    }
                },
                error: function (e) {

                }
            });
        }
    };

    $scope.publicHealthServices = [];

    // rootScoped on emited value catches the value for the model and insert to get the old data
    $rootScope.$on('PublicHealthServices', function (event, val) {
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
            url = "/Profile/WorkHistory/AddPublicHealthServiceAsync?profileId=" + profileId;
        }
        else if ($scope.visibilityControl == (index + '_editPublicHealthService')) {
            //Update Details - Denote the URL
            form = $('#publicHealthServiceEditDiv' + index).find('form');
            url = "/Profile/WorkHistory/UpdatePublicHealthServiceAsync?profileId=" + profileId;
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
                    if (data.status == "true") {
                        data.publicHealthService.StartDate = ConvertDateFormat(data.publicHealthService.StartDate);
                        data.publicHealthService.EndDate = ConvertDateFormat(data.publicHealthService.EndDate);
                        if ($scope.visibilityControl != (index + '_editPublicHealthService')) {
                            $scope.publicHealthServices.push(data.publicHealthService);
                            $rootScope.visibilityControl = "addedNewPublicHealthService";
                            messageAlertEngine.callAlertMessage("addedNewPublicHealthService", "Public Health Service Information saved successfully !!!!", "success", true);
                        }
                        else {
                            $scope.publicHealthServices[index] = data.publicHealthService;
                            $rootScope.visibilityControl = "updatedPublicHealthService"
                            messageAlertEngine.callAlertMessage("addedNewPublicHealthService", "Public Health Service Information updated successfully !!!!", "success", true);
                        }
                        $scope.IsPublicHealthServiceHasError = false;
                        FormReset(form);
                        

                        $scope.resetDates();
                    } else {
                        $scope.IsPublicHealthServiceHasError = true;
                        $scope.PublicHealthServiceErrorList = data.status.split(",");
                    }
                }
            });

            //loadingOff();
            $scope.resetDates();
        }
    };


    //$$$$$$$$$$$$$$$$$$$ Work History History#################################

    $scope.militaryServiceInformationHistoryArray = [];
    $scope.showMilitaryServiceInformationTable = false;

    $scope.showMilitaryServiceInformationHistory = function (loadingId) {
        if ($scope.militaryServiceInformationHistoryArray.length == 0) {
            $("#" + loadingId).css('display', 'block');
            var url = "/Profile/ProfileHistory/GetAllMilitaryServiceInformationHistory?profileId=" + profileId;
            $http.get(url).success(function (data) {
                $scope.militaryServiceInformationHistoryArray = data;
                $scope.showMilitaryServiceInformationTable = true;
                $("#" + loadingId).css('display', 'none');
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
            var url = "/Profile/ProfileHistory/GetAllProfessionalWorkExperienceHistory?profileId=" + profileId;
            $http.get(url).success(function (data) {
                $scope.professionalWorkExperienceHistoryArray = data;
                $scope.showProfessionalWorkExperienceHistoryTable = true;
                $scope.dataFetchedPWE = true;
                $("#" + loadingId).css('display', 'none');
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
            var url = "/Profile/ProfileHistory/GetAllPublicHealthServiceHistory?profileId=" + profileId;
            $http.get(url).success(function (data) {
                $scope.publicHealthServiceHistoryArray = data;
                $scope.showPublicHealthServiceTable = true;
                $scope.dataFetchedPHS = true;
                $("#" + loadingId).css('display', 'none');
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
            var url = "/Profile/ProfileHistory/GetAllWorkGapHistory?profileId=" + profileId;
            $http.get(url).success(function (data) {
                $scope.workGapHistoryArray = data;
                $scope.showWorkGapHistoryTable = true;
                $scope.dataFetchedWG = true;
                $("#" + loadingId).css('display', 'none');
            });
        }
        else {
            $scope.showWorkGapHistoryTable = true;
        }
       
    }

    $scope.cancelWorkGapHistory = function (){
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
        $formData = $('#removePublicHealthService');
        url = "/Profile/WorkHistory/RemovePublicHealthServiceAsync?profileId=" + profileId;
        ResetFormForValidation($formData);
        console.log($formData);
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
                    //console.log(data.status);
                    if (data.status == "true") {
                        var obj = $filter('filter')(publicHealthServices, { PublicHealthServiceID: data.publicHealthService.PublicHealthServiceID })[0];
                        publicHealthServices.splice(publicHealthServices.indexOf(obj), 1);
                        if ($scope.dataFetchedPHS == true) {
                            obj.HistoryStatus = 'Deleted';
                            $scope.publicHealthServieHistoryArray.push(obj);
                        }
                        $('#publicHealthServiceWarningModal').modal('hide');
                        $rootScope.operateCancelControl('');
                        messageAlertEngine.callAlertMessage("addedNewPublicHealthService", "Public Health Service Removed successfully.", "success", true);
                    } else {
                        $('#publicHealthServiceWarningModal').modal('hide');
                        messageAlertEngine.callAlertMessage("removePublicHealthService", data.status, "danger", true);
                        $scope.errorPublicHealthService = "Sorry for Inconvenience !!!! Please Try Again Later...";
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
        $scope.workGaps = val;
        //for (var i = 0; i < $scope.workGaps.length ; i++) {
        //    $scope.workGaps[i].StartDate = ConvertDateFormat($scope.workGaps[i].StartDate);
        //    $scope.workGaps[i].EndDate = ConvertDateFormat($scope.workGaps[i].EndDate);
        //}
    });

    $scope.saveWorkGap = function (workGap, index) {
        //loadingOn();
        var url;
        var form;

        if ($scope.visibilityControl == 'addWorkGap') {
            //Add Details - Denote the URL
            form = $('#newWorkGapFormDiv').find('form');
            url = "/Profile/WorkHistory/AddWorkGapAsync?profileId=" + profileId;
        }
        else if ($scope.visibilityControl == (index + '_editWorkGap')) {
            //Update Details - Denote the URL
            form = $('#workGapEditDiv' + index).find('form');
            url = "/Profile/WorkHistory/UpdateWorkGapAsync?profileId=" + profileId;
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
                    if (data.status == "true") {
                        data.workGap.StartDate = ConvertDateFormat(data.workGap.StartDate);
                        data.workGap.EndDate = ConvertDateFormat(data.workGap.EndDate);
                        if ($scope.visibilityControl != (index + '_editWorkGap')) {
                            $scope.workGaps.push(data.workGap);
                            $rootScope.visibilityControl = "addedNewWorkGap";
                            messageAlertEngine.callAlertMessage("addedNewWorkGap", "Work Gap Information saved successfully !!!!", "success", true);
                        }
                        else {
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
                    }
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
        $formData = $('#editWorkGap');
        url = "/Profile/WorkHistory/RemoveWorkGap?profileId=" + profileId;
        ResetFormForValidation($formData);
        console.log($formData);
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
                    //console.log(data.status);
                    if (data.status == "true") {
                        var obj = $filter('filter')(workGaps, { WorkGapID: data.workGap.WorkGapID })[0];
                        workGaps.splice(workGaps.indexOf(obj), 1);
                        if ($scope.dataFetchedWG == true) {
                            obj.HistoryStatus = 'Deleted';
                            $scope.workGapHistoryArray.push(obj);
                        }
                        $('#workGapWarningModal').modal('hide');
                        //PracticeLocationDetail.SupervisingProviders.splice();
                        $rootScope.operateCancelControl('');
                        messageAlertEngine.callAlertMessage("addedNewWorkGap", "Work Gap Removed successfully.", "success", true);
                    } else {
                        $('#workGapWarningModal').modal('hide');
                        messageAlertEngine.callAlertMessage("removeWorkGap", data.status, "danger", true);
                        $scope.errorWorkGap = "Sorry for Inconvenience !!!! Please Try Again Later...";
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
        $scope.militaryServiceInformations = val;
        for (var i = 0; i < $scope.militaryServiceInformations.length ; i++) {
            //$scope.militaryServiceInformations[i].StartDate = ConvertDateFormat($scope.militaryServiceInformations[i].StartDate);
            //$scope.militaryServiceInformations[i].DischargeDate = ConvertDateFormat($scope.militaryServiceInformations[i].DischargeDate);

            if (!$scope.militaryServiceInformations[i].MilitaryDischarge) { $scope.militaryServiceInformations[i].MilitaryDischarge = ""; }
            if (!$scope.militaryServiceInformations[i].MilitaryPresentDuty) { $scope.militaryServiceInformations[i].MilitaryPresentDuty = ""; }
        }
    });

    // Method for Asyn saving Military Service Information

    $scope.saveMilitaryServiceInformation = function (militaryServiceInformation, index) {
        //loadingOn();
        var url;
        var form;

        if ($scope.visibilityControl == 'addMilitaryServiceInformation') {
            //Add Details - Denote the URL
            form = $('#newMilitaryServiceInformationFormDiv').find('form');
            url = "/Profile/WorkHistory/AddMilitaryServiceInformationAsync?profileId=" + profileId;
        }
        else if ($scope.visibilityControl == (index + '_editMilitaryServiceInformation')) {
            //Update Details - Denote the URL
            form = $('#militaryServiceInformationEditDiv' + index).find('form');
            url = "/Profile/WorkHistory/UpdateMilitaryServiceInformationAsync?profileId=" + profileId;
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
                    if (data.status == "true") {
                        if (!data.militaryServiceInformation.MilitaryDischarge) { data.militaryServiceInformation.MilitaryDischarge = ""; }
                        if (!data.militaryServiceInformation.MilitaryPresentDuty) { data.militaryServiceInformation.MilitaryPresentDuty = ""; }
                        data.militaryServiceInformation.StartDate = ConvertDateFormat(data.militaryServiceInformation.StartDate);
                        data.militaryServiceInformation.DischargeDate = ConvertDateFormat(data.militaryServiceInformation.DischargeDate);
                        if ($scope.visibilityControl != (index + '_editMilitaryServiceInformation')) {
                            $scope.militaryServiceInformations.push(data.militaryServiceInformation);
                            $rootScope.visibilityControl = "addedNewMilitaryServiceInformation";
                            messageAlertEngine.callAlertMessage("addedNewMilitaryServiceInformation", "Military Service Information saved successfully !!!!", "success", true);
                        }
                        else {
                            $scope.militaryServiceInformations[index] = data.militaryServiceInformation;
                            $rootScope.visibilityControl = "updatedMilitaryServiceInformation";
                            messageAlertEngine.callAlertMessage("addedNewMilitaryServiceInformation", "Military Service Information updated successfully !!!!", "success", true);
                        }
                        $scope.IsMilitaryServiceInfoHasError = false;
                        FormReset(form);

                        $scope.resetDates();
                    } else {
                        $scope.IsMilitaryServiceInfoHasError = true;
                        $scope.MilitaryServiceInfoErrorList = data.status.split(",");
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
        $formData = $('#removeMilitaryServiceInformation');
        url = "/Profile/WorkHistory/RemoveMilitaryServiceInformationAsync?profileId=" + profileId;
        ResetFormForValidation($formData);
        console.log($formData);
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
                    //console.log(data.status);
                    if (data.status == "true") {
                        var obj = $filter('filter')(militaryServiceInformations, { MilitaryServiceInformationID: data.militaryServiceInformation.MilitaryServiceInformationID })[0];
                        militaryServiceInformations.splice(militaryServiceInformations.indexOf(obj), 1);
                        if ($scope.dataFetchedMSI == true) {
                            obj.HistoryStatus = 'Deleted';
                            $scope.militaryServiceInformationHistoryArray.push(obj);
                        }
                        $('#militaryServiceInformationWarningModal').modal('hide');
                        $rootScope.operateCancelControl('');
                        messageAlertEngine.callAlertMessage("addedNewMilitaryServiceInformation", "Military Service Information Removed successfully.", "success", true);
                    } else {
                        $('#militaryServiceInformationWarningModal').modal('hide');
                        messageAlertEngine.callAlertMessage("removeMilitaryServiceInformation", data.status, "danger", true);
                        $scope.errorMilitaryServiceInformation = "Sorry for Inconvenience !!!! Please Try Again Later...";
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
            //console.log("Getting data....");
            $http({
                method: 'GET',
                url: '/Profile/MasterProfile/GetWorkHistoriesProfileDataAsync?profileId=' + profileId
            }).success(function (data, status, headers, config) {
                //console.log(data);
                try {
                    for (key in data) {
                        //console.log(key);
                        $rootScope.$emit(key, data[key]);
                        //call respective controller to load data (PSP)
                    }
                    $rootScope.WorkHistoryLoaded = true;
                    $rootScope.$broadcast("LoadRequireMasterDataWorkHistory");
                } catch (e) {
                    //console.log("error getting data back");
                    $rootScope.WorkHistoryLoaded = true;
                }

            }).error(function (data, status, headers, config) {
                //console.log(status);
                $rootScope.WorkHistoryLoaded = true;
            });
            $scope.dataLoaded = true;
        }
    });

}]);