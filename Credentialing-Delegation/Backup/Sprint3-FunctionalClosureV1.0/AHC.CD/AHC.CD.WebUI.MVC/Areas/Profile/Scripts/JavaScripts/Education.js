
//=========================== Controller declaration ==========================
profileApp.controller('educationController', ['$scope', '$rootScope', '$http', 'countryDropDownService', 'masterDataService', 'locationService', function ($scope, $rootScope, $http, countryDropDownService, masterDataService, locationService) {


    //==================================Master Data============================================

    $scope.masterSpecialties = [];

    masterDataService.getMasterData("/Profile/MasterData/GetAllSpecialities").then(function (masterSpecialties) {
        $scope.masterSpecialties = masterSpecialties;

    });

    $scope.masterSchools = [];

    masterDataService.getMasterData("/Profile/MasterData/GetAllSchools").then(function (masterSchools) {
        $scope.masterSchools = masterSchools;
    });

    $scope.masterCertifications = [];

    masterDataService.getMasterData("/Profile/MasterData/GetAllCertificates").then(function (masterCertifications) {
        $scope.masterCertifications = masterCertifications;
    });

    $scope.masterDegrees = [];

    masterDataService.getMasterData("/Profile/MasterData/GetAllQualificationDegrees").then(function (masterDegrees) {
        $scope.masterDegrees = masterDegrees;
    });

    $scope.masterHospitals = [];

    masterDataService.getMasterData("/Profile/MasterData/GetAllHospitals").then(function (masterHospitals) {
        $scope.masterHospitals = masterHospitals;
    });

    //------------------------------- Country Code Popover Show by Id ---------------------------------------
    $scope.CountryDialCodes = countryDailCodes;

    $scope.showContryCodeDiv = function (countryCodeDivId) {
        changeVisibilityOfCountryCode();
        $("#" + countryCodeDivId).show();       
    };

   

    //------------------------------------------------------------------Address Auto-Complete---------------------------------------------------------------------------//

    /*
     Method addressAutocomplete() gets the details of a location
         Method takes input of location details entered in the text box.
  */
    $scope.addressAutocomplete = function (location) {
        $scope.tempObject.SchoolInformation.City = "";
        $scope.tempObject.SchoolInformation.ZipCode = "";         //So we check if its an object and if it is then we asign the values like 
        $scope.tempObject.SchoolInformation.State = "";                          //city name, state name, etc to the respective models
        $scope.tempObject.SchoolInformation.Country = "";                      ////which is inturn is displayed to the users
        $scope.tempObject.SchoolInformation.County = "";
        if (location.length > 2 && !angular.isObject(location)) {         //As soon as the length of the string reaches 3 and the location is not an object
            $scope.Locations = locationService.getLocations(location);      //A call is made to the locations service which returns a list of relevant location
        }
        else if (angular.isObject(location)) {                      //When user select a city the location variable then holds the object of the respective location.
            $scope.tempObject.SchoolInformation.City = location.City;
            $scope.tempObject.SchoolInformation.ZipCode = location.Zipcode;         //So we check if its an object and if it is then we asign the values like 
            $scope.tempObject.SchoolInformation.State = location.State;                          //city name, state name, etc to the respective models
            $scope.tempObject.SchoolInformation.Country = location.Country;                      ////which is inturn is displayed to the users
            $scope.tempObject.SchoolInformation.County = location.County;
        }
    }

    //----------------------------------------------------------------------------------------------------------------------------------------------------------------//    

    //To Display the drop down div
    $scope.searchCumDropDown = function (divId) {
        //console.log("jhfhdfgfgfgf");
        $("#" + divId).show();
    };

    //Bind the school name with model class to achieve search cum drop down
    $scope.addIntoSchoolDropDown = function (name, div) {        
        $scope.tempObject.SchoolInformation.SchoolName = name;
        $("#" + div).hide();
    }
    //Bind the degree name with model class to achieve search cum drop down
    $scope.addIntoDegreeDropDown = function (degree, div) {
        $scope.tempObject.QualificationDegree = degree;
        $("#" + div).hide();
    }

    //Bind the hospital name with model class to achieve search cum drop down
    $scope.addIntoHospitalDropDown = function (hospital, div) {
        $scope.tempObject.HospitalName = hospital;
        $("#" + div).hide();
    }
    //Bind the Certificate name with model class to achieve search cum drop down
    $scope.addIntoCMEDropDown = function (certificate, div) {
        $scope.tempObject.Certification = certificate;
        $("#" + div).hide();
    }

    //===========================Education Details====================================================================


    $scope.educationDetailViewModels = [];
    $scope.GraduationDetailViewModel = [];

    // rootScoped on emitted value catches the value for the model and insert to get the old data for Education Details
    $rootScope.$on('EducationDetails', function (event, val) {
        
        for (var i = 0; i < val.length ; i++) {
            if (val[i] != null) {
                val[i].StartDate = ConvertDateFormat(val[i].StartDate);
                val[i].EndDate = ConvertDateFormat(val[i].EndDate);

                if (val[i].EducationQualificationType == 1) {
                    $scope.educationDetailViewModels.push(val[i]);

                }
                else {                    
                    $scope.GraduationDetailViewModel.push(val[i]);

                }
            }
        }

    });




    //===============================Under Graduate/Professional Schools Details============================================

    $scope.saveUG = function (educationDetailViewModel, IndexValue) {

        var validationStatus;
        var url;
        var $formData;

        if ($scope.visibilityControl == 'addeducationDetailViewModel') {
            //Add Details - Denote the URL            
            $formData = $('#newUGFormDiv').find('form');
            url = "/Profile/EducationHistory/AddEducationDetailAsync?profileId=" + profileId;
        }
        else if ($scope.visibilityControl == (IndexValue + '_editeducationDetailViewModel')) {
            //Update Details - Denote the URL            
            $formData = $('#ugEditDiv' + IndexValue).find('form');
            url = "/Profile/EducationHistory/UpdateEducationDetailAsync?profileId=" + profileId;
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
                        $scope.ErrorInUGDetails = false;
                        if ($scope.visibilityControl == 'addeducationDetailViewModel') {
                            $rootScope.visibilityControl = "addedNewUGDetails";
                            data.educationDetails.StartDate = ConvertDateFormat(data.educationDetails.StartDate);
                            data.educationDetails.EndDate = ConvertDateFormat(data.educationDetails.EndDate);
                            $scope.educationDetailViewModels.push(data.educationDetails);
                        }
                        else if ($scope.visibilityControl == (IndexValue + '_editeducationDetailViewModel')) {
                            $rootScope.visibilityControl = "updatedUGDetails";
                            data.educationDetails.StartDate = ConvertDateFormat(data.educationDetails.StartDate);
                            data.educationDetails.EndDate = ConvertDateFormat(data.educationDetails.EndDate);
                            $scope.educationDetailViewModels[IndexValue] = data.educationDetails;
                        }
                        $rootScope.tempObject = {};
                        FormReset($formData);
                    } else {
                        $scope.ErrorInUGDetails = true;
                        $scope.UGDetailsErrorList = data.status.split(",");
                    }
                },
                error: function (e) {
                    $scope.UGDetailsErrorList = "Please Ensure the details and try again";
                }
            });
        }
    };

    //============== Graduate Details  ================



    $scope.saveGraduation = function (graduationDetailViewModel, IndexValue) {

        var validationStatus;
        var url;
        var $formData;
        $scope.IndexValue = 0;
        if ($scope.visibilityControl == 'addgraduationDetailViewModel') {
            //Add Details - Denote the URL            
            $formData = $('#newGraduationFormDiv').find('form');
            url = "/Profile/EducationHistory/AddEducationDetailAsync?profileId=" + profileId;
        }
        else if ($scope.visibilityControl == (IndexValue + '_editgraduationDetailViewModel')) {
            //Update Details - Denote the URL            
            $formData = $('#graduationEditDiv' + IndexValue).find('form');
            url = "/Profile/EducationHistory/UpdateEducationDetailAsync?profileId=" + profileId;
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

                        $scope.ErrorInPGDetails = false;
                        if ($scope.visibilityControl == 'addgraduationDetailViewModel') {
                            $rootScope.visibilityControl = "addedNewPGDetails";
                            data.educationDetails.StartDate = ConvertDateFormat(data.educationDetails.StartDate);
                            data.educationDetails.EndDate = ConvertDateFormat(data.educationDetails.EndDate);
                            $scope.GraduationDetailViewModel.push(data.educationDetails);
                        }
                        else if ($scope.visibilityControl == (IndexValue + '_editgraduationDetailViewModel')) {
                            $rootScope.visibilityControl = "updatedPGDetails";
                            data.educationDetails.StartDate = ConvertDateFormat(data.educationDetails.StartDate);
                            data.educationDetails.EndDate = ConvertDateFormat(data.educationDetails.EndDate);
                            $scope.GraduationDetailViewModel[IndexValue] = data.educationDetails;
                        }
                        $rootScope.tempObject = {};
                        FormReset($formData);
                    } else {
                        $scope.ErrorInPGDetails = true;
                        $scope.PGDetailsErrorList = data.status.split(",");
                    }
                },
                error: function (e) {
                    $scope.PGDetailsErrorList = "Please Ensure the details and try again";
                }
            });
        }
    };

    //=================================ECFMG Details===========================


    // rootScoped on emitted value catches the value for the model and insert to get the old data for ECFMG Details
    $rootScope.$on('ECFMGDetail', function (event, val) {

        if (val != null) {
            val.ECFMGIssueDate = ConvertDateFormat(val.ECFMGIssueDate);
            $scope.ecfmgDetail = val;
        }

    });

    $scope.saveECFMGDetails = function (ecfmgDetail) {

        var validationStatus;
        var url;
        var $formData;

        if ($scope.visibilityControl == 'editecfmgDetail') {
            //Add Details - Denote the URL            
            $formData = $('#ecfmgEditDiv').find('form');
            url = "/Profile/EducationHistory/UpdateECFMGDetailAsync?profileId=" + profileId;
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
                        $scope.ErrorInUGDetails = false;
                        if ($scope.visibilityControl == 'editecfmgDetail') {
                            $rootScope.visibilityControl = "addedNewECFMGDetails";
                            data.ecfmgDetails.ECFMGIssueDate = ConvertDateFormat(data.ecfmgDetails.ECFMGIssueDate);
                            $scope.ecfmgDetail = data.ecfmgDetails;
                        }
                        else if ($scope.visibilityControl == (IndexValue + 'updateecfmgDetail')) {
                            $rootScope.visibilityControl = "updatedECFMGDetails";
                            data.ecfmgDetails.ECFMGIssueDate = ConvertDateFormat(data.ecfmgDetails.ECFMGIssueDate);
                            $scope.ecfmgDetail = data.ecfmgDetails;
                        }
                        $rootScope.tempObject = {};

                    } else {
                        $scope.ErrorInECFMGDetails = true;
                        $scope.ECFMGDetailsErrorList = data.status.split(",");
                    }
                },
                error: function (e) {
                    $scope.ECFMGDetailsErrorList = "Please Ensure the details and try again";
                }
            });
        }
    };

    //============ Training Details ==================

    $scope.ResidencyInternshipViewModel = [];


    // rootScoped on emitted value catches the value for the model and insert to get the old data for Residency/Internship/Fellowship Details
    $rootScope.$on('TrainingDetails', function (event, val) {

        for (var i = 0; i < val.length ; i++) {
            if (val[i] != null) {
                for (var j = 0; j < val[i].ResidencyInternshipDetails.length; j++) {
                    val[i].ResidencyInternshipDetails[j].StartDate = ConvertDateFormat(val[i].ResidencyInternshipDetails[j].StartDate);
                    val[i].ResidencyInternshipDetails[j].EndDate = ConvertDateFormat(val[i].ResidencyInternshipDetails[j].EndDate);

                }
                $scope.ResidencyInternshipViewModel.push(val[i]);
            }
        }


    });



    //======================Residency/Internship/Fellowship/Other================


    $scope.saveProgram = function (residencyInternshipViewModel, IndexValue) {

        var validationStatus;
        var url;
        var $formData;

        if ($scope.visibilityControl == 'addresidencyInternshipViewModel') {
            //Add Details - school and list of residency/internship/fellowship details            
            $formData = $('#newProgramFormDiv').find('form');
            url = "/Profile/EducationHistory/AddTrainingDetailAsync?profileId=" + profileId;
        }
        else if ($scope.visibilityControl == (IndexValue + '_editresidencyInternshipViewModel')) {
            //Update Details - update school details
            $formData = $('#programEditDiv' + IndexValue).find('form');
            url = "/Profile/EducationHistory/UpdateTrainingDetailAsync?profileId=" + profileId;
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

                        $scope.ErrorInProgramDetails = false;
                        if ($scope.visibilityControl == 'addresidencyInternshipViewModel') {
                            $rootScope.visibilityControl = "addedNewProgramDetails";
                            $scope.ResidencyInternshipViewModel.push(data.TrainingDetails);

                        }
                        else if ($scope.visibilityControl == (IndexValue + '_editresidencyInternshipViewModel')) {
                            $rootScope.visibilityControl = "updatedProgramDetails";
                            $scope.ResidencyInternshipViewModel[IndexValue] = data.TrainingDetails;

                        }
                        $rootScope.tempObject = {};
                        FormReset($formData);
                    }
                    else {
                        $scope.ErrorInProgramDetails = true;
                        $scope.ProgramDetailsErrorList = data.status.split(",");
                    }
                },
                error: function (e) {
                    $scope.ResidencyDetailsErrorList = "Please Ensure the details and try again";
                }
            });
        }


    };

    $scope.AddView = "Add";

    $scope.SetAddView = function (action) {
        $scope.AddView = action;
    };

    //Set primary to secondary
    $scope.setPrimary = function () {
        try {            
            for (var i = 0; i < $scope.ResidencyInternshipViewModel.length; i++) {
                for (var j = 0; j < $scope.ResidencyInternshipViewModel[i].ResidencyInternshipDetails.length; j++) {                    
                        $scope.ResidencyInternshipViewModel[i].ResidencyInternshipDetails[j].PreferenceType = "2";
                }
            }
        }
        catch (e) { }
    };

    $scope.updateResidency = function (tempObject, residencyInternship, IndexValue) {


        var url;
        var $formDataResidency;
        var trainingId = tempObject.TrainingDetailID;
        var SpecialtyName;

        if ($scope.visibilitySecondControl == 'addresidency') {
            //Add Details - Add residency/internship/fellowship for a existing school details            
            $formDataResidency = $('#newResidencyDivFor' + $scope.AddView + trainingId).find('form');
            SpecialtyName = $($formDataResidency[0]).find($("[name='SpecialtyID'] option:selected")).text();
            url = "/Profile/EducationHistory/AddResidencyInternshipDetailAsync?profileId=" + profileId + "&trainingId=" + trainingId;
        }
        else if ($scope.visibilitySecondControl == (IndexValue + '_editresidency')) {
            //Update Details - update residency/internship/fellowship details            
            $formDataResidency = $('#residencyEditDiv' + residencyInternship.ResidencyInternshipDetailID).find('form');
            SpecialtyName = $($formDataResidency[0]).find($("[name='SpecialtyID'] option:selected")).text();
            url = "/Profile/EducationHistory/UpdateResidencyInternshipDetailAsync?profileId=" + profileId + "&trainingId=" + trainingId;
        }

        ResetFormForValidation($formDataResidency);
        validationStatus = $formDataResidency.valid();

        if (validationStatus) {
            //Simple POST request example (passing data)
            $.ajax({
                url: url,
                type: 'POST',
                data: new FormData($formDataResidency[0]),
                async: false,
                cache: false,
                contentType: false,
                processData: false,
                success: function (data) {
                    if (residencyInternship.PreferenceType == "1") {
                        $scope.setPrimary();
                    }
                    if (data.status == "true") {                       
                        

                        $scope.ErrorInResidencyDetails = false;
                        if ($scope.visibilitySecondControl == 'addresidency') {
                            $rootScope.visibilitySecondControl = "addedNewResidencyDetails";
                            //$rootScope.visibilitySecondControl = "addedNewResidencyDetails1";
                            data.ResidencyDetails.Specialty = { ID: data.ResidencyDetails.SpecialtyID, Name: SpecialtyName };
                            data.ResidencyDetails.StartDate = ConvertDateFormat(data.ResidencyDetails.StartDate);
                            data.ResidencyDetails.EndDate = ConvertDateFormat(data.ResidencyDetails.EndDate);                           

                            for (var i = 0; i < $scope.ResidencyInternshipViewModel.length; i++) {
                                if ($scope.ResidencyInternshipViewModel[i].TrainingDetailID == trainingId) {
                                    $scope.ResidencyInternshipViewModel[i].ResidencyInternshipDetails.push(data.ResidencyDetails);
                                    tempObject.ResidencyInternshipDetails.push(data.ResidencyDetails);
                                }
                            }

                        }
                        else if ($scope.visibilitySecondControl == (IndexValue + '_editresidency')) {
                            $rootScope.visibilitySecondControl = "updatedResidencyDetails";
                            //$rootScope.visibilitySecondControl = "updatedResidencyDetails1";
                            data.ResidencyDetails.Specialty = { ID: data.ResidencyDetails.SpecialtyID, Name: SpecialtyName };
                            data.ResidencyDetails.StartDate = ConvertDateFormat(data.ResidencyDetails.StartDate);
                            data.ResidencyDetails.EndDate = ConvertDateFormat(data.ResidencyDetails.EndDate);
                            
                            for (var i = 0; i < $scope.ResidencyInternshipViewModel.length; i++) {
                                if ($scope.ResidencyInternshipViewModel[i].TrainingDetailID == trainingId) {
                                    $scope.ResidencyInternshipViewModel[i].ResidencyInternshipDetails[IndexValue] = data.ResidencyDetails;
                                    tempObject.ResidencyInternshipDetails[IndexValue] = data.ResidencyDetails;
                                }
                            }
                        }
                                                                        
                        FormReset($formDataResidency);
                    } else {
                        $scope.ErrorInResidencyDetails = true;
                        $scope.ResidencyDetailsErrorList = data.status.split(",");
                    }
                },
                error: function (e) {
                    $scope.ResidencyDetailsErrorList = "Please Ensure the details and try again";
                }
            });
        }
    };




    //======================Post graduate Training/CME==========================


    $scope.CertificationCMEViewModel = [];


    // rootScoped on emitted value catches the value for the model and insert to get the old data for PostGratuste Training/CME Details
    $rootScope.$on('CMECertifications', function (event, val) {

        for (var i = 0; i < val.length ; i++) {
            if (val[i] != null) {
                val[i].StartDate = ConvertDateFormat(val[i].StartDate);
                val[i].EndDate = ConvertDateFormat(val[i].EndDate);
                val[i].ExpiryDate = ConvertDateFormat(val[i].ExpiryDate);
                val[i].City = $rootScope.ConvertCity(val[i].City);
                $scope.CertificationCMEViewModel.push(val[i]);
            }
        }


    });

    $scope.saveCertificate = function (certificationCMEViewModel, IndexValue) {

        var validationStatus;
        var url;
        var $formData;

        if ($scope.visibilityControl == 'addcertificationCMEViewModel') {
            //Add Details - Denote the URL
            validationStatus = $('#newCertificateFormDiv').find('form').valid();
            $formData = $('#newCertificateFormDiv').find('form');
            url = "/Profile/EducationHistory/AddCMECertificationAsync?profileId=" + profileId;
        }
        else if ($scope.visibilityControl == (IndexValue + '_editcertificationCMEViewModel')) {
            //Update Details - Denote the URL
            validationStatus = $('#certificateEditDiv' + IndexValue).find('form').valid();
            $formData = $('#certificateEditDiv' + IndexValue).find('form');
            url = "/Profile/EducationHistory/UpdateCMECertificationAsync?profileId=" + profileId;
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

                        $scope.ErrorInCMEDetails = false;
                        if ($scope.visibilityControl == 'addcertificationCMEViewModel') {
                            $rootScope.visibilityControl = "addedNewCMEDetails";
                            data.CMEDetails.StartDate = ConvertDateFormat(data.CMEDetails.StartDate);
                            data.CMEDetails.EndDate = ConvertDateFormat(data.CMEDetails.EndDate);
                            data.CMEDetails.ExpiryDate = ConvertDateFormat(data.CMEDetails.ExpiryDate);
                            $scope.CertificationCMEViewModel.push(data.CMEDetails);
                        }
                        else if ($scope.visibilityControl == (IndexValue + '_editcertificationCMEViewModel')) {
                            $rootScope.visibilityControl = "updatedCMEDetails";
                            data.CMEDetails.StartDate = ConvertDateFormat(data.CMEDetails.StartDate);
                            data.CMEDetails.EndDate = ConvertDateFormat(data.CMEDetails.EndDate);
                            data.CMEDetails.ExpiryDate = ConvertDateFormat(data.CMEDetails.ExpiryDate);
                            $scope.CertificationCMEViewModel[IndexValue] = data.CMEDetails;
                        }
                        $rootScope.tempObject = {};
                        FormReset($formData);
                    } else {
                        $scope.ErrorInCMEDetails = true;
                        $scope.CMEDetailsErrorList = data.status.split(",");
                    }
                },
                error: function (e) {
                    $scope.CMEDetailsErrorList = "Please Ensure the details and try again";
                }
            });
        }
    };

}]);