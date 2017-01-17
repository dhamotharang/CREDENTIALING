
profileApp.controller('WorkHistoryController', ['$scope', '$rootScope', '$http', '$filter', 'countryDropDownService', 'masterDataService', 'locationService', function ($scope, $rootScope, $http, $filter, countryDropDownService, masterDataService, locationService) {



    /*
       Method addressAutocomplete() gets the details of a location
       Method takes input of location details entered in the text box.
    */
    $scope.addressAutocomplete = function (location) {
        if (location.length > 2 && !angular.isObject(location)) {         //As soon as the length of the string reaches 3 and the location is not an object
            $scope.Locations = locationService.getLocations(location);    //A call is made to the locations service which returns a list of relevant locations
        }
        else if (angular.isObject(location)) {                            //When user select a city the location variable then holds the object of the respective location.
            if (!$scope.tempObject) {
                $scope.tempObject = {};
            }
            //Setting City name, ZipCode,State Name,Country Name,County Name to scope models
            
            $scope.tempObject.City = location.City;
            $scope.tempObject.ZipCode = location.Zipcode;
            $scope.tempObject.State = location.State;
            $scope.tempObject.Country = location.Country;
            $scope.tempObject.County = location.County;
        }
    }

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


    $scope.IndexValue = 0;

    $scope.professionalWorkExperiences = [];

    // rootScoped on emitted value catches the value for the model and insert to get the old data
    //calling the method using $on(PSP-public subscriber pattern)
    $rootScope.$on('ProfessionalWorkExperiences', function (event, val) {
        console.log(val);
        $scope.professionalWorkExperiences = val;
        for (var i = 0; i < $scope.professionalWorkExperiences.length ; i++) {
            $scope.professionalWorkExperiences[i].StartDate = ConvertDateFormat($scope.professionalWorkExperiences[i].StartDate);
            $scope.professionalWorkExperiences[i].EndDate = ConvertDateFormat($scope.professionalWorkExperiences[i].EndDate);
        }
    });

    //------------SAVE--------------
    $scope.saveProfessionalWorkExperience = function (professionalWorkExperience, index) {

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
                            $rootScope.visibilityControl = "addedNewProfessionalWorkExperience"
                        }

                        else {
                            $scope.professionalWorkExperiences[index] = data.professionalWorkExperience;
                            $rootScope.visibilityControl = "updatedProfessionalWorkExperience"
                        }

                        $scope.IsProfessionalWorkExperienceHasError = false;
                        FormReset(form);

                    } else {
                        $scope.IsProfessionalWorkExperienceHasError = true;
                        $scope.ProfessionalWorkExperienceErrorList = data.status.split(",");
                    }
                }
            });
        }
    };

    $scope.publicHealthServices = [];

    // rootScoped on emited value catches the value for the model and insert to get the old data
    $rootScope.$on('PublicHealthServices', function (event, val) {
        $scope.publicHealthServices = val;
        for (var i = 0; i < $scope.publicHealthServices.length ; i++) {
            $scope.publicHealthServices[i].StartDate = ConvertDateFormat($scope.publicHealthServices[i].StartDate);
            $scope.publicHealthServices[i].EndDate = ConvertDateFormat($scope.publicHealthServices[i].EndDate);
        }
    });

    $scope.savePublicHealthService = function (publicHealthService, index) {

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
                            $rootScope.visibilityControl = "addedNewPublicHealthService"
                        }
                        else {
                            $scope.publicHealthServices[index] = data.publicHealthService;
                            $rootScope.visibilityControl = "updatedPublicHealthService"
                        }
                        $scope.IsPublicHealthServiceHasError = false;
                        FormReset(form);

                    } else {
                        $scope.IsPublicHealthServiceHasError = true;
                        $scope.PublicHealthServiceErrorList = data.status.split(",");
                    }
                }
            });
        }
    };

    $scope.workGaps = [];

    // rootScoped on emited value catches the value for the model and insert to get the old data
    $rootScope.$on('WorkGaps', function (event, val) {
        $scope.workGaps = val;
        for (var i = 0; i < $scope.workGaps.length ; i++) {
            $scope.workGaps[i].StartDate = ConvertDateFormat($scope.workGaps[i].StartDate);
            $scope.workGaps[i].EndDate = ConvertDateFormat($scope.workGaps[i].EndDate);
        }
    });

    $scope.saveWorkGap = function (workGap, index) {

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
                            $rootScope.visibilityControl = "addedNewWorkGap"
                        }
                        else {
                            $scope.workGaps[index] = data.workGap;
                            $rootScope.visibilityControl = "updatedWorkGap"
                        }
                        $scope.IsWorkGapHasError = false;
                        FormReset(form);

                    } else {
                        $scope.IsWorkGapHasError = true;
                        $scope.WorkGapErrorList = data.status.split(",");
                    }
                }
            });
        }
    };

    $scope.militaryServiceInformations = [];

    // rootScoped on emited value catches the value for the model and insert to get the old data
    $rootScope.$on('MilitaryServiceInformations', function (event, val) {
        $scope.militaryServiceInformations = val;
        for (var i = 0; i < $scope.militaryServiceInformations.length ; i++) {
            $scope.militaryServiceInformations[i].StartDate = ConvertDateFormat($scope.militaryServiceInformations[i].StartDate);
            $scope.militaryServiceInformations[i].DischargeDate = ConvertDateFormat($scope.militaryServiceInformations[i].DischargeDate);
        }
    });

    // Method for Asyn saving Military Service Information

    $scope.saveMilitaryServiceInformation = function (militaryServiceInformation, index) {

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
                        data.militaryServiceInformation.StartDate = ConvertDateFormat(data.militaryServiceInformation.StartDate);
                        data.militaryServiceInformation.DischargeDate = ConvertDateFormat(data.militaryServiceInformation.DischargeDate);
                        if ($scope.visibilityControl != (index + '_editMilitaryServiceInformation')) {
                            $scope.militaryServiceInformations.push(data.militaryServiceInformation);
                            $rootScope.visibilityControl = "addedNewMilitaryServiceInformation"
                        }
                        else {
                            $scope.militaryServiceInformations[index] = data.militaryServiceInformation;
                            $rootScope.visibilityControl = "updatedMilitaryServiceInformation"
                        }
                        $scope.IsMilitaryServiceInfoHasError = false;
                        FormReset(form);

                    } else {
                        $scope.IsMilitaryServiceInfoHasError = true;
                        $scope.MilitaryServiceInfoErrorList = data.status.split(",");
                    }
                }
            });
        }




    };


}]);