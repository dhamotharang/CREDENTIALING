
//=========================== Controller declaration ==========================
profileApp.controller('educationController', function ($rootScope, $scope, $http, countryDropDownService, masterDataService) {

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
   
    
    $scope.educationDetailViewModels = [];
    $scope.GraduationDetailViewModel = [];


    $scope.schools = [{ Name: "University of Alabama School of Dentistry" },
        { Name: "California College of Podiatric Medicine" },
        { Name: "Cleveland Chiropractic College of Los Angele" },
        { Name: "Alabama School of Dentistry" },
        { Name: "University of Alabama " },
        { Name: "University of Alabama School of Dentistry" },
        { Name: " School of Dentistry" },
        { Name: "University of Alabama School of Dentistry" },
        { Name: "University of Alabama  of Dentistry" },
        { Name: "University of Alabama School of Dentistry" },
        { Name: " Alabama School " },

    ]


    //===========================Education Details====================================================================

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
                    if (val[i].ECFMGDetail != null) {
                        val[i].ECFMGDetail.ECFMGIssueDate = ConvertDateFormat(val[i].ECFMGDetail.ECFMGIssueDate);
                    }
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
            //validationStatus = $('#newUGFormDiv').find('form').valid();
            $formData = $('#newUGFormDiv').find('form');
            url = "/Profile/EducationHistory/AddEducationDetailAsync?profileId="+ profileId;
        }
        else if ($scope.visibilityControl == (IndexValue+'_editeducationDetailViewModel')) {
            //Update Details - Denote the URL
            //validationStatus = $('#ugEditDiv' + IndexValue).find('form').valid();
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
                            $scope.educationDetailViewModels.push(educationDetailViewModel);
                        }
                        else if ($scope.visibilityControl == (IndexValue + '_editeducationDetailViewModel')) {
                            $rootScope.visibilityControl = "updatedUGDetails";
                            $Scope.educationDetailViewModels[IndexValue] = educationDetailViewModel;
                        }
                        $rootscope.visibilityControl = "";
                    
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
            validationStatus = $('#newGraduationFormDiv').find('form').valid();
            $formData = $('#newGraduationFormDiv').find('form');
            url = "/Profile/EducationHistory/AddEducationDetailAsync?profileId=" + profileId;
        }
        else if ($scope.visibilityControl == (IndexValue+'_editgraduationDetailViewModel')) {
            //Update Details - Denote the URL
            validationStatus = $('#graduationEditDiv' + IndexValue).find('form').valid();
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
                            $scope.GraduationDetailViewModel.push(graduationDetailViewModel);
                        }
                        else if ($scope.visibilityControl == (IndexValue + '_editgraduationDetailViewModel')) {
                            $rootScope.visibilityControl = "updatedPGDetails";
                            $Scope.GraduationDetailViewModel[IndexValue] = graduationDetailViewModel;
                        }
                        $rootscope.visibilityControl = "";

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

    $scope.saveResidency = function (residencyInternshipDetails) {

        $scope.Residencys = [];
        $scope.Residencys.push(residencyInternshipDetails);
        $rootScope.visibilitySecondControl = "";

    }

    $scope.saveProgram = function (residencyInternshipViewModel, IndexValue) {

        var validationStatus;
        var url;
        var $formData;
       
        if ($scope.visibilityControl == 'addresidencyInternshipViewModel') {
            //Add Details - school and list of residency/internship/fellowship details
            validationStatus = $('#newProgramFormDiv').find('form').valid();
            //$formData = $('#newProgramFormDiv').find('form');
            residencyInternshipViewModel.ResidencyInternshipDetails = $scope.Residencys;

            url = "/Profile/EducationHistory/AddTrainingDetailAsync?profileId=" + profileId;
        }
        else if ($scope.visibilityControl == (IndexValue+'_editresidencyInternshipViewModel')) {
            //Update Details - update school details
            validationStatus = $('#programEditDiv' + IndexValue).find('form').valid();
            //$formData = $('#programEditDiv' + IndexValue).find('form');
            url = "/Profile/EducationHistory/UpdateTrainingDetailAsync?profileId=" + profileId;
        }


        
        if (validationStatus) {
            $http.post(url, residencyInternshipViewModel).success(function (data) {
                $scope.ErrorInProgramDetails = false;
                if ($scope.visibilityControl == 'addresidencyInternshipViewModel') {
                    $rootScope.visibilityControl = "addedNewProgramDetails";
                    $scope.ResidencyInternshipViewModel.push(residencyInternshipViewModel);
                }
                else if ($scope.visibilityControl == (IndexValue + '_editresidencyInternshipViewModel')) {
                    $rootScope.visibilityControl = "updatedProgramDetails";
                    $Scope.ResidencyInternshipViewModel[IndexValue] = residencyInternshipViewModel;
                }
                $rootscope.visibilityControl = "";
                            
            }).error(function (data) {
                $scope.ErrorInProgramDetails = true;
                $scope.ProgramDetailsErrorList = data.status.split(",");
            })

        };

    };


    $scope.updateResidency = function (residencyInternship, IndexValue) {
        
        var validationStatus = true;
        var url;
        var $formData;
        
        var trainingId = residencyInternship.TrainingDetailID;
        if ($scope.visibilitySecondControl == 'addresidencyInternship' || $scope.visibilitySecondControl == 'addresidency') {
            //Add Details - Add residency/internship/fellowship for a existing school details
            validationStatus = $('#newProgramFormDiv').find('form').valid();
            $formData = $('#newProgramFormDiv').find('form');
            url = "/Profile/EducationHistory/AddResidencyInternshipDetailAsync?profileId=" + profileId + "&trainingId=" + trainingId;
        }
        else if ($scope.visibilitySecondControl == (IndexValue + '_editresidency')) {
            var residency = residencyInternship.ResidencyInternshipDetails[IndexValue];

            //Update Details - update residency/internship/fellowship details
            validationStatus = $('#programEditDiv' + IndexValue).find('form').valid();
            $formData = $('#programEditDiv' + IndexValue).find('form');
            url = "/Profile/EducationHistory/UpdateResidencyInternshipDetailAsync?profileId=" + profileId + "&trainingId=" + trainingId;
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
                        $scope.ErrorInResidencyDetails = false;
                        if ($scope.visibilitySecondControl == 'addresidencyInternship' || $scope.visibilitySecondControl == 'addresidency') {
                            $rootScope.visibilityControl = "addedNewResidencyDetails";
                            $scope.residencyInternship.ResidencyInternshipDetails.push(residencyInternship);
                        }
                        else if ($scope.visibilitySecondControl == (IndexValue + '_editresidency')) {
                            $rootScope.visibilityControl = "updatedResidencyDetails";
                            $Scope.residencyInternship.ResidencyInternshipDetails[IndexValue] = residencyInternship.ResidencyInternshipDetails;
                        }
                        $rootscope.visibilitySecondControl = "";

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
                $scope.CertificationCMEViewModel.push(val[i]);
            }
        }
        

    });

    $scope.saveCertificate = function (certificationCMEViewModel, IndexValue) {

       

        var validationStatus;
        var url;
        var $formData;
        //$scope.IndexValue = 0;
        if ($scope.visibilityControl == 'addcertificationCMEViewModel') {
            //Add Details - Denote the URL
            validationStatus = $('#newCertificateFormDiv').find('form').valid();
            $formData = $('#newCertificateFormDiv').find('form');
            url = "/Profile/EducationHistory/AddCMECertificationAsync?profileId=" + profileId;
        }
        else if ($scope.visibilityControl == (IndexValue +'_editcertificationCMEViewModel')) {
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
                            $scope.CertificationCMEViewModel.push(certificationCMEViewModel);
                        }
                        else if ($scope.visibilityControl == (IndexValue + '_editcertificationCMEViewModel')) {
                            $rootScope.visibilityControl = "updatedCMEDetails";
                            $Scope.certificationCMEViewModel[IndexValue] = certificationCMEViewModel;
                        }
                        $rootscope.visibilityControl = "";

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
    
});