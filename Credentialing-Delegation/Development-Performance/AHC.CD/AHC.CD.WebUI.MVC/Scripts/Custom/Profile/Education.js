
//=========================== Controller declaration ==========================
profileApp.controller('educationController', function ($scope, $rootScope, $http, countryDropDownService, masterDataService) {

    
    /////==================================Master Data============================================

    $scope.masterSpecialties = [];

    masterDataService.getMasterData(rootDir + "/Profile/MasterData/GetAllSpecialities").then(function (masterSpecialties) {
        $scope.masterSpecialties = masterSpecialties;
        
    });

    $scope.masterSchools = [];

    masterDataService.getMasterData(rootDir + "/Profile/MasterData/GetAllSchools").then(function (masterSchools) {
        $scope.masterSchools = masterSchools;
    });

    $scope.masterCertifications = [];

    masterDataService.getMasterData(rootDir + "/Profile/MasterData/GetAllCertificates").then(function (masterCertifications) {
        $scope.masterCertifications = masterCertifications;
    });

    $scope.masterDegrees = [];

    masterDataService.getMasterData(rootDir + "/Profile/MasterData/GetAllQualificationDegrees").then(function (masterDegrees) {
        $scope.masterDegrees = masterDegrees;
    });

    $scope.masterHospitals = [];

    masterDataService.getMasterData(rootDir + "/Profile/MasterData/GetAllHospitals").then(function (masterHospitals) {
        $scope.masterHospitals = masterHospitals;
    });
   
    
    $scope.educationDetailViewModels = [];
    $scope.GraduationDetailViewModel = [];


    $scope.schools = [{ Name: "University of Alabama School of Dentistry", Id:1 },
        { Name: "California College of Podiatric Medicine", Id: 2 },
        { Name: "Cleveland Chiropractic College of Los Angele", Id: 3 },
        { Name: "Alabama School of Dentistry", Id: 4 },
        { Name: "University of Alabama ", Id: 5 },
        { Name: "University of Alabama School of Dentistry", Id: 6 },
        { Name: " School of Dentistry", Id: 7},
        { Name: "University of Alabama School of Dentistry", Id: 8 },
        { Name: "University of Alabama  of Dentistry", Id: 9 },
        { Name: "University of Alabama School of Dentistry", Id: 10 },
        { Name: " Alabama School ", Id: 11 },

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
                url: rootDir + url,
                type: 'POST',
                data: new FormData($formData[0]),
                async: false,
                cache: false,
                contentType: false,
                processData: false,
                success: function (data) {
                    
                    try {
                        if (data.status == "true") {
                            $scope.ErrorInUGDetails = false;
                            if ($scope.visibilityControl == 'addeducationDetailViewModel') {
                                $rootScope.visibilityControl = "addedNewUGDetails";
                                $scope.educationDetailViewModels.push(educationDetailViewModel);
                            }
                            else if ($scope.visibilityControl == (IndexValue + '_editeducationDetailViewModel')) {
                                $rootScope.visibilityControl = "updatedUGDetails";
                                $scope.educationDetailViewModels[IndexValue] = educationDetailViewModel;
                            }
                            $rootScope.tempObject = {};
                            $rootScope.visibilityControl = "";
                            FormReset($formData);
                        } else {
                            $scope.ErrorInUGDetails = true;
                            $scope.UGDetailsErrorList = data.status.split(",");
                        }
                    } catch (e) {
                       
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
            //validationStatus = $('#newGraduationFormDiv').find('form').valid();
            $formData = $('#newGraduationFormDiv').find('form');
            url = "/Profile/EducationHistory/AddEducationDetailAsync?profileId=" + profileId;
        }
        else if ($scope.visibilityControl == (IndexValue+'_editgraduationDetailViewModel')) {
            //Update Details - Denote the URL
            //validationStatus = $('#graduationEditDiv' + IndexValue).find('form').valid();
            $formData = $('#graduationEditDiv' + IndexValue).find('form');
            url = "/Profile/EducationHistory/UpdateEducationDetailAsync?profileId=" + profileId;
        }

        ResetFormForValidation($formData);
        validationStatus = $formData.valid();
        if (validationStatus) {
            //Simple POST request example (passing data) :
            
            $.ajax({
                url: rootDir + url,
                type: 'POST',
                data: new FormData($formData[0]),
                async: false,
                cache: false,
                contentType: false,
                processData: false,
                success: function (data) {
                    
                    try {
                        if (data.status == "true") {
                            $scope.ErrorInPGDetails = false;
                            if ($scope.visibilityControl == 'addgraduationDetailViewModel') {
                                $rootScope.visibilityControl = "addedNewPGDetails";
                                $scope.GraduationDetailViewModel.push(graduationDetailViewModel);
                            }
                            else if ($scope.visibilityControl == (IndexValue + '_editgraduationDetailViewModel')) {
                                $rootScope.visibilityControl = "updatedPGDetails";
                                $scope.GraduationDetailViewModel[IndexValue] = graduationDetailViewModel;
                            }
                            $rootScope.visibilityControl = "";
                            $formData
                        } else {
                            $scope.ErrorInPGDetails = true;
                            $scope.PGDetailsErrorList = data.status.split(",");
                        }
                    } catch (e) {
                       
                    }
                },
                error: function (e) {
                    $scope.PGDetailsErrorList = "Please Ensure the details and try again";
                }
            });
        }
    };

    //=================================ECFMG Details===========================

    //$scope.ecfmgDetail = {};
    

    // rootScoped on emitted value catches the value for the model and insert to get the old data for ECFMG Details
    $rootScope.$on('ECFMGDetail', function (event, val) {

        val.ECFMGIssueDate = ConvertDateFormat(val.ECFMGIssueDate);              
        $scope.ecfmgDetail = val;
        
                  

    });

    $scope.saveECFMGDetails = function (ecfmgDetail) {

        var validationStatus;
        var url;
        var $formData;

        if ($scope.visibilityControl == 'editecfmgDetail') {
            //Add Details - Denote the URL
            //validationStatus = $('#newUGFormDiv').find('form').valid();
            $formData = $('#ecfmgEditDiv').find('form');
            url = "/Profile/EducationHistory/UpdateECFMGDetailAsync?profileId=" + profileId;
        }
        

        ResetFormForValidation($formData);
        validationStatus = $formData.valid();

        if (validationStatus) {
            //Simple POST request example (passing data) :

            $.ajax({
                url: rootDir + url,
                type: 'POST',
                data: new FormData($formData[0]),
                async: false,
                cache: false,
                contentType: false,
                processData: false,
                success: function (data) {
                    try {

                        if (data.status == "true") {
                            $scope.ErrorInUGDetails = false;
                            if ($scope.visibilityControl == 'editecfmgDetail') {
                                $rootScope.visibilityControl = "addedNewECFMGDetails";
                                $scope.ecfmgDetail = ecfmgDetail;
                            }
                            else if ($scope.visibilityControl == (IndexValue + 'updateecfmgDetail')) {
                                $rootScope.visibilityControl = "updatedECFMGDetails";
                                $scope.ecfmgDetail = ecfmgDetail;
                            }
                            $rootScope.tempObject = {};
                            $rootScope.visibilityControl = "";

                        } else {
                            $scope.ErrorInECFMGDetails = true;
                            $scope.ECFMGDetailsErrorList = data.status.split(",");
                        }
                    } catch (e) {
                     
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
    $scope.Residencys = [];

    $scope.saveProgram = function (Form_Id) {
        var $form = $("#" + Form_Id);
        
        if ($form) {
            //var training = $form[0];
            //training.ResidencyinternshipDetails = $scope.Residencys;
            $.ajax({
                url: rootDir + '/Profile/EducationHistory/AddTrainingDetailAsync?profileId=' + profileId,
                type: 'POST',
                data: new FormData($form[0]),
                async: false,
                cache: false,
                contentType: false,
                processData: false,
                success: function (data) {
                    try {
                        if (data.status == "true") {
                            $scope.ResidencyInternshipViewModel.push(data.residencyInternshipViewModel);
                            $scope.ErrorInProgramDetails = false;
                            $rootScope.tempObject = {};
                            $rootScope.visibilityControl = "";
                            FormReset($form);
                        } else {
                            $scope.ErrorInProgramDetails = true;
                            $scope.ProgramDetailsErrorList = data.status.split(",");
                        }
                    } catch (e) {
                       
                    }
                }
            });
        } else {
            
        }
    };
  
    

    //======================Residency/Internship/Fellowship/Other================

    $scope.saveResidency = function (residencyInternshipDetails) {

        
       
        $scope.Residencys.push(residencyInternshipDetails);        
        $rootScope.tempSecondObject = {};
        $rootScope.visibilitySecondControl = "";
    }

    //$scope.saveProgram = function (residencyInternshipViewModel, IndexValue) {

    //    var validationStatus;
    //    var url;
    //    var $formData;
       
    //    if ($scope.visibilityControl == 'addresidencyInternshipViewModel') {
    //        //Add Details - school and list of residency/internship/fellowship details
    //        validationStatus = $('#newProgramFormDiv').find('form').valid();
    //        //$formData = $('#newProgramFormDiv').find('form');
    //        residencyInternshipViewModel.ResidencyInternshipDetails = $scope.Residencys;

    //        url = "/Profile/EducationHistory/AddTrainingDetailAsync?profileId=" + profileId;
    //    }
    //    else if ($scope.visibilityControl == (IndexValue+'_editresidencyInternshipViewModel')) {
    //        //Update Details - update school details
    //        validationStatus = $('#programEditDiv' + IndexValue).find('form').valid();
    //        //$formData = $('#programEditDiv' + IndexValue).find('form');
    //        url = "/Profile/EducationHistory/UpdateTrainingDetailAsync?profileId=" + profileId;
    //    }


        
    //    if (validationStatus) {
    //        $http.post(url, residencyInternshipViewModel).success(function (data) {

    //            if(data.status == "true"){
    //            $scope.ErrorInProgramDetails = false;
    //            if ($scope.visibilityControl == 'addresidencyInternshipViewModel') {
    //                $rootScope.visibilityControl = "addedNewProgramDetails";
    //                $scope.ResidencyInternshipViewModel.push(residencyInternshipViewModel);
    //            }
    //            else if ($scope.visibilityControl == (IndexValue + '_editresidencyInternshipViewModel')) {
    //                $rootScope.visibilityControl = "updatedProgramDetails";
    //                $scope.ResidencyInternshipViewModel[IndexValue] = residencyInternshipViewModel;
    //            }
    //            $rootscope.visibilityControl = "";
    //            FormReset($formData);
    //            }
    //            else {
    //                $scope.ErrorInProgramDetails = true;
    //                $scope.ProgramDetailsErrorList = data.status.split(",");
    //            }
    //        }).error(function (data) {
    //            $scope.ErrorInProgramDetails = true;
    //            $scope.ProgramDetailsErrorList = data.status.split(",");
    //        })

    //    };

    //};


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
            //////Simple POST request example (passing data) :
            
            $.ajax({
                url: rootDir + url,
                type: 'POST',
                data: new FormData($formData[0]),
                async: false,
                cache: false,
                contentType: false,
                processData: false,
                success: function (data) {
                    
                    try {
                        if (data.status == "true") {
                            $scope.ErrorInResidencyDetails = false;
                            if ($scope.visibilitySecondControl == 'addresidencyInternship' || $scope.visibilitySecondControl == 'addresidency') {
                                $rootScope.visibilityControl = "addedNewResidencyDetails";
                                $scope.residencyInternship.ResidencyInternshipDetails.push(residencyInternship);
                            }
                            else if ($scope.visibilitySecondControl == (IndexValue + '_editresidency')) {
                                $rootScope.visibilityControl = "updatedResidencyDetails";
                                $scope.residencyInternship.ResidencyInternshipDetails[IndexValue] = residencyInternship.ResidencyInternshipDetails;
                            }
                            $rootScope.tempSecondObject = {};
                            $rootScope.visibilitySecondControl = "";
                            FormReset($formData);
                        } else {
                            $scope.ErrorInResidencyDetails = true;
                            $scope.ResidencyDetailsErrorList = data.status.split(",");
                        }
                    } catch (e) {
                       
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
            ///Simple POST request example (passing data) :
           
            $.ajax({
                url: rootDir + url,
                type: 'POST',
                data: new FormData($formData[0]),
                async: false,
                cache: false,
                contentType: false,
                processData: false,
                success: function (data) {
                    
                    try {
                        if (data.status == "true") {
                            $scope.ErrorInCMEDetails = false;
                            if ($scope.visibilityControl == 'addcertificationCMEViewModel') {
                                $rootScope.visibilityControl = "addedNewCMEDetails";
                                $scope.CertificationCMEViewModel.push(certificationCMEViewModel);
                            }
                            else if ($scope.visibilityControl == (IndexValue + '_editcertificationCMEViewModel')) {
                                $rootScope.visibilityControl = "updatedCMEDetails";
                                $scope.certificationCMEViewModel[IndexValue] = certificationCMEViewModel;
                            }
                            $rootScope.tempObject = {};
                            $rootScope.visibilityControl = "";
                            $formData
                        } else {
                            $scope.ErrorInCMEDetails = true;
                            $scope.CMEDetailsErrorList = data.status.split(",");
                        }
                    } catch (e) {
                       
                    }
                },
                error: function (e) {
                    $scope.CMEDetailsErrorList = "Please Ensure the details and try again";
                }
            });
        }
    };
    
});