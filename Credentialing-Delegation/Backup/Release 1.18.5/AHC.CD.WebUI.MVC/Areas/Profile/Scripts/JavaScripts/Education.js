
//=========================== Controller declaration ==========================
profileApp.controller('educationController', ['$scope', '$rootScope', '$http', 'masterDataService', 'locationService','messageAlertEngine', function ($scope, $rootScope, $http, masterDataService, locationService,messageAlertEngine) {

    $rootScope.$on("LoadRequireMasterData", function () {
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

    });

    //------------------------------- Country Code Popover Show by Id ---------------------------------------
    $scope.CountryDialCodes = countryDailCodes;

    $scope.showContryCodeDiv = function (countryCodeDivId) {
        changeVisibilityOfCountryCode();
        $("#" + countryCodeDivId).show();       
    };

   

    //------------------------------------------------------------------Address Auto-Complete---------------------------------------------------------------------------//

    /* Method addressAutocomplete() gets the details of a location
         Method takes input of location details entered in the text box.*/
  

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

    function showLocationList(ele) {
        $(ele).parent().find(".ProviderTypeSelectAutoList").first().show();
    }

    $scope.selectedLocation = function (location) {
        $scope.setAddressModels(location);
        $(".ProviderTypeSelectAutoList").hide();
    };
                                                                         
    $scope.resetAddressModels = function () {
        $scope.tempObject.SchoolInformation.City = "";
        $scope.tempObject.SchoolInformation.State = "";
        $scope.tempObject.SchoolInformation.Country = "";
    };

    $scope.setAddressModels = function (location) {
        $scope.tempObject.SchoolInformation.City = location.City;
        $scope.tempObject.SchoolInformation.State = location.State;
        $scope.tempObject.SchoolInformation.Country = location.Country;

    }


    //----------------------------------------------------------------------------------------------------------------------------------------------------------------//    

    //To Display the drop down div
    $scope.searchCumDropDown = function (divId) {        
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
        loadingOn();
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
                        if ($scope.visibilityControl == 'addeducationDetailViewModel') {                                                   
                            data.educationDetails.StartDate = ConvertDateFormat(data.educationDetails.StartDate);
                            data.educationDetails.EndDate = ConvertDateFormat(data.educationDetails.EndDate);
                            $scope.educationDetailViewModels.push(data.educationDetails);
                            $rootScope.operateCancelControl();
                            messageAlertEngine.callAlertMessage("addedNewUGDetails", "New Under Graduate/Professional School Details Added Successfully !!!!", "success", true);

                        }
                        else if ($scope.visibilityControl == (IndexValue + '_editeducationDetailViewModel')) {                                                        
                            data.educationDetails.StartDate = ConvertDateFormat(data.educationDetails.StartDate);
                            data.educationDetails.EndDate = ConvertDateFormat(data.educationDetails.EndDate);
                            $scope.educationDetailViewModels[IndexValue] = data.educationDetails;
                            $rootScope.operateViewAndAddControl(IndexValue + '_vieweducationDetailViewModel');
                            messageAlertEngine.callAlertMessage('updatedUGDetails' + IndexValue, "Under Graduate/Professional School Details Updated Successfully !!!!", "success", true);
                            
                        }
                        $scope.datePickerReset();
                        FormReset($formData);
                    } else {                        
                        messageAlertEngine.callAlertMessage('ErrorInUGDetails' + IndexValue, "", "danger", true);
                        $scope.UGDetailsErrorList = data.status.split(",");
                    }
                },
                error: function (e) {                    
                    messageAlertEngine.callAlertMessage('ErrorInUGDetails' + IndexValue, "", "danger", true);
                    $scope.UGDetailsErrorList = "Sorry for Inconvenience !!!! Please Try Again Later...";
                }
            });
        }
        loadingOff();      
    };

    //============== Graduate Details  ================

    $scope.saveGraduation = function (graduationDetailViewModel, IndexValue) {
        loadingOn();
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
                        if ($scope.visibilityControl == 'addgraduationDetailViewModel') {                                                       
                            data.educationDetails.StartDate = ConvertDateFormat(data.educationDetails.StartDate);
                            data.educationDetails.EndDate = ConvertDateFormat(data.educationDetails.EndDate);
                            $scope.GraduationDetailViewModel.push(data.educationDetails);
                            $rootScope.operateCancelControl();
                            messageAlertEngine.callAlertMessage("addedNewPGDetails", "New Graduate/Medical School Details Added Successfully !!!!", "success", true);
                        }
                        else if ($scope.visibilityControl == (IndexValue + '_editgraduationDetailViewModel')) {                                                    
                            data.educationDetails.StartDate = ConvertDateFormat(data.educationDetails.StartDate);
                            data.educationDetails.EndDate = ConvertDateFormat(data.educationDetails.EndDate);
                            $scope.GraduationDetailViewModel[IndexValue] = data.educationDetails;
                            $rootScope.operateViewAndAddControl(IndexValue + '_viewgraduationDetailViewModel');
                            messageAlertEngine.callAlertMessage('updatedPGDetails' + IndexValue, "Graduate/Medical School Details Updated Successfully !!!!", "success", true);
                        }
                        $scope.datePickerReset();
                        FormReset($formData);
                    } else {                       
                        messageAlertEngine.callAlertMessage('ErrorInPGDetails' + IndexValue, "", "danger", true);
                        $scope.PGDetailsErrorList = data.status.split(",");
                    }
                },
                error: function (e) {                    
                    messageAlertEngine.callAlertMessage('ErrorInPGDetails' + IndexValue, "", "danger", true);
                    $scope.PGDetailsErrorList = "Sorry for Inconvenience !!!! Please Try Again Later...";
                }
            });
        }
        loadingOff();
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
        loadingOn();
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
                            data.ecfmgDetails.ECFMGIssueDate = ConvertDateFormat(data.ecfmgDetails.ECFMGIssueDate);
                            $rootScope.operateCancelControl();
                            $scope.ecfmgDetail = data.ecfmgDetails;
                            messageAlertEngine.callAlertMessage("addedNewECFMGDetails", "ECFMG Detail Saved Successfully !!!!", "success", true);
                        }
                        else if ($scope.visibilityControl == (IndexValue + 'updateecfmgDetail')) {                                                      
                            data.ecfmgDetails.ECFMGIssueDate = ConvertDateFormat(data.ecfmgDetails.ECFMGIssueDate);
                            $scope.ecfmgDetail = data.ecfmgDetails;
                            $rootScope.operateViewAndAddControl(IndexValue + '!editecfmgDetail');
                            messageAlertEngine.callAlertMessage('updatedECFMGDetails' + IndexValue, "ECFMG Detail Saved Successfully !!!!", "success", true);
                        }
                        
                        $scope.datePickerReset();
                    } else {                        
                        messageAlertEngine.callAlertMessage('ErrorInECFMGDetails' + IndexValue, "", "danger", true);
                        $scope.ECFMGDetailsErrorList = data.status.split(",");
                    }
                },
                error: function (e) {                    
                    messageAlertEngine.callAlertMessage('ErrorInECFMGDetails' + IndexValue, "", "danger", true);
                    $scope.ECFMGDetailsErrorList = "Sorry for Inconvenience !!!! Please Try Again Later...";
                }
            });
        }
        loadingOff();
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
        loadingOn();
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

                        for (var i = 0; i < data.TrainingDetails.ResidencyInternshipDetails.length ; i++) {
                            if (data.TrainingDetails.ResidencyInternshipDetails != null) {
                                data.TrainingDetails.ResidencyInternshipDetails[i].StartDate = ConvertDateFormat(data.TrainingDetails.ResidencyInternshipDetails[i].StartDate);
                                data.TrainingDetails.ResidencyInternshipDetails[i].EndDate = ConvertDateFormat(data.TrainingDetails.ResidencyInternshipDetails[i].EndDate);
                            }
                        }

                        $scope.ErrorInProgramDetails = false;
                        if ($scope.visibilityControl == 'addresidencyInternshipViewModel') {
                            $rootScope.visibilityControl = "addedNewProgramDetails";
                            $scope.ResidencyInternshipViewModel.push(data.TrainingDetails);
                            $rootScope.operateCancelControl();
                            messageAlertEngine.callAlertMessage("addedNewProgramDetails", "School Details Added Successfully. Use edit action to add Residency/Internship/fellowship Details!!!", "success", true);

                        }
                        else if ($scope.visibilityControl == (IndexValue + '_editresidencyInternshipViewModel')) {                            
                            $scope.ResidencyInternshipViewModel[IndexValue] = data.TrainingDetails;
                            $rootScope.operateViewAndAddControl(IndexValue + '_viewresidencyInternshipViewModel');
                            messageAlertEngine.callAlertMessage('updatedProgramDetails' + IndexValue, "School Details Updated Successfully !!!!", "success", true);

                        }
                        $scope.datePickerReset();
                        FormReset($formData);
                    }
                    else {                        
                        messageAlertEngine.callAlertMessage('ErrorInProgramDetails' + IndexValue, "", "danger", true);
                        $scope.ProgramDetailsErrorList = data.status.split(",");
                    }
                },
                error: function (e) {                    
                    messageAlertEngine.callAlertMessage('ErrorInProgramDetails' + IndexValue, "", "danger", true);
                    $scope.ResidencyDetailsErrorList = "Sorry for Inconvenience !!!! Please Try Again Later...";
                }
            });
        }

        loadingOff();
    };

    $scope.AddView = "Add";

    $scope.SetAddView = function (action) {
        $scope.AddView = action;
    };

    //Set primary to secondary
    $scope.setPrimary = function () {
        try {
            if($scope.ResidencyInternshipViewModel.length > 0){
            for (var i = 0; i < $scope.ResidencyInternshipViewModel.length; i++) {
                for (var j = 0; j < $scope.ResidencyInternshipViewModel[i].ResidencyInternshipDetails.length; j++) {                    
                    $scope.ResidencyInternshipViewModel[i].ResidencyInternshipDetails[j].PreferenceType = "2";
                    $scope.ResidencyInternshipViewModel[i].ResidencyInternshipDetails[j].Preference = "Secondary";
                }
            }
          }
        }
        catch (e) { }
    };

    $scope.updateResidencyForView = function (residencyInternshipViewModel, residencyInternship, IndexValue) {

        loadingOn();
        var url;
        var $formDataResidency;
        var trainingId = residencyInternshipViewModel.TrainingDetailID;
        var SpecialtyName;

        
        if (($scope.visibilitySecondControl == 'addresidencyForView')) {
            //Add Details - Add residency/internship/fellowship for a existing school details            
            $formDataResidency = $('#newViewResidencyDivFor' + $scope.AddView + trainingId).find('form');
            SpecialtyName = $($formDataResidency[0]).find($("[name='SpecialtyID'] option:selected")).text();
            url = "/Profile/EducationHistory/AddResidencyInternshipDetailAsync?profileId=" + profileId + "&trainingId=" + trainingId;
        }
        else if ($scope.visibilitySecondControl == (IndexValue + '_editresidencyForView')) {
            //Update Details - update residency/internship/fellowship details            
            $formDataResidency = $('#viewResidencyEditDiv' + residencyInternship.ResidencyInternshipDetailID).find('form');
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
                        if ($scope.visibilitySecondControl == 'addresidencyForView') {      
                            data.ResidencyDetails.Specialty = { ID: data.ResidencyDetails.SpecialtyID, Name: SpecialtyName };
                            data.ResidencyDetails.StartDate = ConvertDateFormat(data.ResidencyDetails.StartDate);
                            data.ResidencyDetails.EndDate = ConvertDateFormat(data.ResidencyDetails.EndDate);                           

                            for (var i = 0; i < $scope.ResidencyInternshipViewModel.length; i++) {
                                if ($scope.ResidencyInternshipViewModel[i].TrainingDetailID == trainingId) {
                                    $scope.ResidencyInternshipViewModel[i].ResidencyInternshipDetails.push(data.ResidencyDetails);
                                    //residencyInternshipViewModel.ResidencyInternshipDetails.push(data.ResidencyDetails);
                                }
                            }
                            $rootScope.operateSecondCancelControl();
                            messageAlertEngine.callAlertMessage("addedNewResidencyDetailsForView", "New Residency/Internship/fellowship Details Added Successfully!!!", "success", true);

                        }
                        else if ($scope.visibilitySecondControl == (IndexValue + '_editresidencyForView')) { 
                            data.ResidencyDetails.Specialty = { ID: data.ResidencyDetails.SpecialtyID, Name: SpecialtyName };
                            data.ResidencyDetails.StartDate = ConvertDateFormat(data.ResidencyDetails.StartDate);
                            data.ResidencyDetails.EndDate = ConvertDateFormat(data.ResidencyDetails.EndDate);
                            
                            for (var i = 0; i < $scope.ResidencyInternshipViewModel.length; i++) {
                                if ($scope.ResidencyInternshipViewModel[i].TrainingDetailID == trainingId) {
                                    $scope.ResidencyInternshipViewModel[i].ResidencyInternshipDetails[IndexValue] = data.ResidencyDetails;
                                    residencyInternshipViewModel.ResidencyInternshipDetails[IndexValue] = data.ResidencyDetails;
                                }
                            }
                            $rootScope.operateSecondViewAndAddControl(IndexValue + '_viewresidencyForView');
                            messageAlertEngine.callAlertMessage('updatedResidencyDetailsForView' + IndexValue, "Residency/Internship/fellowship Details Updated Successfully !!!!", "success", true);
                        }
                        $scope.datePickerReset();
                        FormReset($formDataResidency);
                    } else {                        
                        messageAlertEngine.callAlertMessage('ErrorInResidencyDetailsForView' + IndexValue, "", "danger", true);
                        $scope.ResidencyDetailsErrorListForView = data.status.split(",");
                    }
                },
                error: function (e) {                    
                    messageAlertEngine.callAlertMessage('ErrorInResidencyDetailsForView' + IndexValue, "", "danger", true);
                    $scope.ResidencyDetailsErrorListForView = "Sorry for Inconvenience !!!! Please Try Again Later...";
                }
            });
        }

        loadingOff();
    };

    $scope.updateResidency = function (tempObject, residencyInternship, IndexValue) {

        loadingOn();
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
                            data.ResidencyDetails.Specialty = { ID: data.ResidencyDetails.SpecialtyID, Name: SpecialtyName };
                            data.ResidencyDetails.StartDate = ConvertDateFormat(data.ResidencyDetails.StartDate);
                            data.ResidencyDetails.EndDate = ConvertDateFormat(data.ResidencyDetails.EndDate);

                            for (var i = 0; i < $scope.ResidencyInternshipViewModel.length; i++) {
                                if ($scope.ResidencyInternshipViewModel[i].TrainingDetailID == trainingId) {
                                    $scope.ResidencyInternshipViewModel[i].ResidencyInternshipDetails.push(data.ResidencyDetails);
                                    tempObject.ResidencyInternshipDetails.push(data.ResidencyDetails);
                                }
                            }
                            $rootScope.operateSecondCancelControl();
                            messageAlertEngine.callAlertMessage("addedNewResidencyDetails", "New Residency/Internship/fellowship Details Added Successfully!!!", "success", true);

                        }
                        else if ($scope.visibilitySecondControl == (IndexValue + '_editresidency')) {                            
                            data.ResidencyDetails.Specialty = { ID: data.ResidencyDetails.SpecialtyID, Name: SpecialtyName };
                            data.ResidencyDetails.StartDate = ConvertDateFormat(data.ResidencyDetails.StartDate);
                            data.ResidencyDetails.EndDate = ConvertDateFormat(data.ResidencyDetails.EndDate);

                            for (var i = 0; i < $scope.ResidencyInternshipViewModel.length; i++) {
                                if ($scope.ResidencyInternshipViewModel[i].TrainingDetailID == trainingId) {
                                    $scope.ResidencyInternshipViewModel[i].ResidencyInternshipDetails[IndexValue] = data.ResidencyDetails;
                                    tempObject.ResidencyInternshipDetails[IndexValue] = data.ResidencyDetails;
                                }
                            }
                            $rootScope.operateSecondViewAndAddControl(IndexValue + '_viewresidency');
                            messageAlertEngine.callAlertMessage('updatedResidencyDetails' + IndexValue, "Residency/Internship/fellowship Details Updated Successfully !!!!", "success", true);
                        }
                        $scope.datePickerReset();
                        FormReset($formDataResidency);
                    } else {                        
                        messageAlertEngine.callAlertMessage('ErrorInResidencyDetails' + IndexValue, "", "danger", true);
                        $scope.ResidencyDetailsErrorListForView = data.status.split(",");
                    }
                },
                error: function (e) {                    
                    messageAlertEngine.callAlertMessage('ErrorInResidencyDetails' + IndexValue, "", "danger", true);
                    $scope.ResidencyDetailsErrorListForView = "Sorry for Inconvenience !!!! Please Try Again Later...";
                }
            });
        }

        loadingOff();
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
        loadingOn();
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
                            data.CMEDetails.StartDate = ConvertDateFormat(data.CMEDetails.StartDate);
                            data.CMEDetails.EndDate = ConvertDateFormat(data.CMEDetails.EndDate);
                            data.CMEDetails.ExpiryDate = ConvertDateFormat(data.CMEDetails.ExpiryDate);
                            $scope.CertificationCMEViewModel.push(data.CMEDetails);
                            $rootScope.operateCancelControl();
                            messageAlertEngine.callAlertMessage("addedNewCMEDetails", "New PostGraduate Training/CME Details Added Successfully!!!", "success", true);
                        }
                        else if ($scope.visibilityControl == (IndexValue + '_editcertificationCMEViewModel')) {                            
                            data.CMEDetails.StartDate = ConvertDateFormat(data.CMEDetails.StartDate);
                            data.CMEDetails.EndDate = ConvertDateFormat(data.CMEDetails.EndDate);
                            data.CMEDetails.ExpiryDate = ConvertDateFormat(data.CMEDetails.ExpiryDate);
                            $scope.CertificationCMEViewModel[IndexValue] = data.CMEDetails;
                            $rootScope.operateViewAndAddControl(IndexValue + '_viewcertificationCMEViewModel');
                            messageAlertEngine.callAlertMessage('updatedCMEDetails' + IndexValue, "PostGraduate Training/CME Details Updated Successfully !!!!", "success", true);
                        }
                        $scope.datePickerReset();
                        FormReset($formData);
                    } else {                        
                        messageAlertEngine.callAlertMessage('ErrorInCMEDetails' + IndexValue, "", "danger", true);
                        $scope.CMEDetailsErrorList = data.status.split(",");
                    }
                },
                error: function (e) {                    
                    messageAlertEngine.callAlertMessage('ErrorInCMEDetails' + IndexValue, "", "danger", true);
                    $scope.CMEDetailsErrorList = "Sorry for Inconvenience !!!! Please Try Again Later...";
                }
            });
        }
        loadingOff();
    };

    $scope.datePickerReset = function () {

        try{
        $scope.tempObject.StartDate = new Date();
        $scope.tempObject.EndDate = new Date();
        $scope.tempSecondObject.StartDate = new Date();
        $scope.tempSecondObject.EndDate = new Date();
        $scope.tempObject.ExpiryDate = new Date();
        $scope.tempObject.ECFMGIssueDate = new Date();  
        }
        catch (e) { }
        
    };

}]);