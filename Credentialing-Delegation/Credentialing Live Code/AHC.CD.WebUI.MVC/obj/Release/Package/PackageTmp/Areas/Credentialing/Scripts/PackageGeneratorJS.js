
Cred_SPA_App.service('messageAlertEngine', ['$rootScope', '$timeout', function ($rootScope, $timeout) {

    $rootScope.messageDesc = "";
    $rootScope.activeMessageDiv = "";
    $rootScope.messageType = "";



    var animateMessageAlertOff = function () {
        $rootScope.closeAlertMessage();
    };

    this.callAlertMessage = function (calledDiv, msg, msgType, dismissal) { //messageAlertEngine.callAlertMessage('updateHospitalPrivilege' + IndexValue, "Data Updated Successfully !!!!", "success", true);                            
        $rootScope.activeMessageDiv = calledDiv;
        $rootScope.messageDesc = msg;
        $rootScope.messageType = msgType;
        if (dismissal) {
            $timeout(animateMessageAlertOff, 5000);
        }
    }

    $rootScope.closeAlertMessage = function () {
        $rootScope.messageDesc = "";
        $rootScope.activeMessageDiv = "";
        $rootScope.messageType = "";
    }
}])
//--------------------------------------------------------------------
//---------------------------------------------------------------------------------------------------------------------------
//-----------------------------------------------check in test---------------------------------------------------------------
//---------------------------------------------------------------------------------------------------------------------------

Cred_SPA_App.controller('PackageGeneratorController', function ($scope, $rootScope, $http, $filter, messageAlertEngine) {

   
    $scope.loadingAjax = true;
    $scope.AddPackageStatus = false;
    $scope.AddPackageStatus1 = false;
    $scope.PlanFormTemp = [];
    $scope.ProfileDocumentTemp = [];
    $scope.PlanForm = [];
    $scope.ProfileDocument = [];
    $scope.PackageGenerated = [];
    $scope.AttachPlanFormPanel = false;
    $scope.AttachProfileDocumentPanel = false;
    $scope.GetAllPackage = [];
    $scope.PackageDocList = [];
    var CredID = JSON.parse(credFilterInfo);
    $scope.pdfProfileId = CredID.ProfileID;
    $scope.pdfLoadingAjax = false;
    $scope.ViewOnlyMode = false;

    $scope.isGeneratePdf = false;
    $scope.isEdit = false;

    $scope.editPlanForm = function () {
        $scope.isEdit = true;
    }



    $scope.cancelEditPlanForm = function () {
        $scope.isEdit = false;
    }



    //$scope.pdfTemplateList = [
       
    //   {
    //    Name: "GHI HOSPITALIST FORM",
    //    FileName: "GHI_HOSPITALIST_FORM",
    //    Payer: "GHI",
    //    State: "N"
    //   },
    //    { 
    //        Name: "GHI PROVIDER FORM",
    //        FileName: "GHI_Provider_Form",
    //        Payer: "GHI",
    //        State: "NN"
    //    },
    //    {
    //        Name: "AETNA - NEW PROVIDER CREDENTIALING FORM",
    //        FileName: "AETNACOVENTRYTemplate",
    //        Payer: "AETNA",
    //        State: "N"
    //    },
    //    {
    //        Name: "ATPL 2016",
    //        FileName: "ATPL2016",
    //        Payer: "ATPL",
    //        State: "FL"
    //    },
    //    {
    //        Name: "TRICARE PROVIDER APPLICATION - SOUTH",
    //        FileName: "TRICARE_PROVIDER_APPLICATION_SOUTH",
    //        Payer: "TRICARE",
    //        State: "SOUTH"
    //    },
    //    {
    //        Name: "TRICARE PROVIDER APPLICATION",
    //        FileName: "TRICARE_PROVIDER_APPLICATION",
    //        Payer: "TRICARE",
    //        State: "SOUTH"
    //    },
    //    {
    //        Name: "LETTER OF INTENT",
    //        FileName: "Letter_of_Intent",
    //        Payer: "OTHERS",
    //        State: "N"
    //    },
    //    {
    //        Name: "TRICARE PA APPLICATION - SOUTH",
    //        FileName: "TRICARE_PA_APPLICATION_SOUTH",
    //        Payer: "TRICARE",
    //        State: "SOUTH"
    //    },
    //    {
    //        Name: "TRICARE PA APPLICATION",
    //        FileName: "TRICARE_PA_APPLICATION",
    //        Payer: "TRICARE",
    //        State: "SOUTH"
    //    },
    //    {
    //        Name: "ALLIED CREDENTIALING APPLICATION ACCESS2",
    //        FileName: "ALLIED_CREDENTIALING_APPLICATION_ACCESS2",
    //        Payer: "Freedom & Optimum",
    //        State: "FL"
    //    },
    //    {
    //        Name: "ALLIED CREDENTIALING APPLICATION ACCESS",
    //        FileName: "ALLIED_CREDENTIALING_APPLICATION_ACCESS",
    //        Payer: "Freedom & Optimum",
    //        State: "FL"
    //    },
    //    {
    //        Name: "ULTIMATE CREDENTIALING APPLICATION",
    //        FileName: "Ultimate_Credentialing_Application",
    //        Payer: "Ultimate Health Plan",
    //        State: "FL"
    //    },
    //    {
    //        Name: "ULTIMATE RE-CREDENTIALING APPLICATION",
    //        FileName: "Ultimate_Re-Credentialing_Application",
    //        Payer: "Ultimate Health Plan",
    //        State: "FL"
    //    },
    //    {
    //        Name: "BCBS PAYMENT AUTH FORM",
    //        FileName: "BCBS_PAYMENT_AUTH_FORM",
    //        Payer: "Blue Cross and Blue Shield",
    //        State: "FL"
    //    },
    //    {
    //        Name: "BCBS PROVIDER UPDATE FORM",
    //        FileName: "BCBS_PROVIDER_UPDATE_FORM",
    //        Payer: "Blue Cross and Blue Shield",
    //        State: "FL"
    //    }, {
    //        Name: "BCBS PROVIDER REGISTRATION FORM",
    //        FileName: "BCBS_PROVIDER_REGISTRATION_FORM",
    //        Payer: "Blue Cross and Blue Shield",
    //        State: "FL"
    //    },
    //    {
    //        Name: "MEDICAID GROUP MEMBERSHIP AUTHORIZATION FORM",
    //        FileName: "MEDICAID_GROUP_MEMBERSHIP_AUTHORIZATION_FORM",
    //        Payer: "Medicare & Medicaid",
    //        State: "FL"
    //    },
    //    {
    //        Name: "TRICARE ARNP APPLICATION FORM - SOUTH",
    //        FileName: "TRICARE_ARNP_APPLICATION_FORM_SOUTH",
    //        Payer: "TRICARE",
    //        State: "SOUTH"
    //    },
    //     {
    //         Name: "TRICARE ARNP APPLICATION FORM",
    //         FileName: "TRICARE_ARNP_APPLICATION_FORM",
    //         Payer: "TRICARE",
    //         State: "SOUTH"
    //     },
    //    {
    //        Name: "FL INSURENCE PROVIDER ATTESTATION",
    //        FileName: "FL_INSURANCE_PROVIDER_ATTESTATION",
    //        Payer: "Freedom & Optimum",
    //        State: "FL"
    //    },
    //    {
    //        Name: "Freedom Optimum IPA",
    //        FileName: "Freedom_Optimum_IPA_Enrollment_Provider_PCP",
    //        Payer: "Freedom & Optimum",
    //        State: "FL"
    //    },
    //    {
    //        Name: "Freedom Optimum Specialist",
    //        FileName: "Freedom_Optimum_Specialist_Package",
    //        Payer: "Freedom & Optimum",
    //        State: "FL"
    //    },
    //    {
    //        Name: "Humana IPA",
    //        FileName: "Humana_IPA_New_PCP_Package",
    //        Payer: "HUMANA",
    //        State: "FL"
    //    },
    //    {
    //        Name: "Humana Specialist",
    //        FileName: "Humana_Specialist_New_Provider",
    //        Payer: "HUMANA",
    //        State: "FL"
    //    },        
    //    {
    //        Name: "FL HOSPITAL ADMIT ATTESTATION",
    //        FileName: "FL_HOSPITAL_ADMIT_ATTESTATION",
    //        Payer: "Freedom & Optimum",
    //        State: "FL"
    //    },
    //    {
    //        Name: "FL 3000 PCP ATTESTATION OF PATIENT LOAD 2016 GLOBAL",
    //        FileName: "FL_3000_PCP_Attestation_of_Patient_Load_2016_Global",
    //        Payer: "OTHER",
    //        State: "FL"
    //    },
    //    {
    //        Name: "WELLCARE MIDLEVEL FORM",
    //        FileName: "WELLCARE_MIDLEVEL_FORMS",
    //        Payer: "WELLCARE",
    //        State: "FL"
    //    },
    //    {
    //        Name: "ADMITING ARRANGEMENT FORM",
    //        FileName: "Admitting_Arrangement_Form",
    //        Payer: "Ultimate Health Plan",
    //        State: "FL"
    //    },
    //    {
    //        Name: "ATTESTATION OF SITE VISIT",
    //        FileName: "ATTESTATION_OF_SITE_VISIT",
    //        Payer: "WELLCARE",
    //        State: "FL"
    //    },
    //    {
    //        Name: "AHC PROVIDER PROFILE FOR WELLCARE FORM",
    //        FileName: "AHC_Provider_Profile_for_Wellcare",
    //        Payer: "WELLCARE",
    //        State: "FL"
    //    },
    //    {
    //        Name: "A2HC PROVIDER PROFILE FOR WELLCARE FORM",
    //        FileName: "A2HC_Provider_Profile_for_Wellcare",
    //        Payer: "WELLCARE",
    //        State: "FL"
    //    },
    //    {
    //        Name: "TRICARE PRIME CREDENTIALING APPLICATION-Primecare",
    //        FileName: "Tricare_Prime_Credentialing_Application",
    //        Payer: "TRICARE",
    //        State: "NORTH"
    //    },
    //    {
    //        Name: "LOI FORM 2016",
    //        FileName: "LOI_Template_2016",
    //        Payer: "OTHER",
    //        State: "N"
    //    },
    //     {

    //         Name: "STANDARD AUTHORIZATION, ATTESTATION AND RELEASE",
    //         FileName: "Authorization_Attestation_and_Release",
    //         Payer: "OTHER",
    //         State: "N"
    //     },
    //    {

    //        Name: "TRICARE CLINICAL PSYCHOLOGIST PROVIDER APPLICATION-Primecare",
    //        FileName: "TRICARE_CLINICAL_PSYCHOLOGIST_PROVIDER_APPLICATION",
    //        Payer: "TRICARE",
    //        State: "NORTH"
    //    },
    //    {

    //        Name: "TRICARE CLINICAL SOCIAL WORKER PROVIDER APPLICATION-Primecare",
    //        FileName: "TRICARE_CLINICAL_SOCIAL_WORKER_PROVIDER_APPLICATION",
    //        Payer: "TRICARE",
    //        State: "NORTH"
    //    },
    //    {
    //        Name: "TRICARE PHYSICIAN / DENTIST PROVIDER APPLICATION-Primecare",
    //        FileName: "TRICARE_PHYSICIAN_DENTIST_PROVIDER_APPLICATION",
    //        Payer: "TRICARE",
    //        State: "NORTH"
    //    },
    //    {

    //        Name: "TRICARE PHYSICIAN ASSISTANT PROVIDER APPLICATION-Primecare",
    //        FileName: "TRICARE_PHYSICIAN_ASSISTANT_PROVIDER_APPLICATION",
    //        Payer: "TRICARE",
    //        State: "NORTH"
    //    },
    //    {
    //        Name: "TRICARE RN LPN NP PROVIDER APPLICATION-Primecare",
    //        FileName: "TRICARE_RN_LPN_NP_PROVIDER_APPLICATION",
    //        Payer: "TRICARE",
    //        State: "NORTH"
    //    },
    //     {
    //         Name: "ELECTRONIC FUNDS TRANSFER AUTHORIZATION AGREEMENT-Primecare",
    //         FileName: "ELECTRONIC_FUNDS_TRANSFER_AUTHORIZATION_AGREEMENT",
    //         Payer: "TRICARE",
    //         State: "NORTH"
    //     },
    //     {
    //         Name: "NON-NETWORK TRICARE PROVIDER FILE GROUP APPLICATION-Primecare",
    //         FileName: "NON-NETWORK_TRICARE_PROVIDER_FILE_GROUP_APPLICATION",
    //         Payer: "TRICARE",
    //         State: "NORTH"
    //     },
    //    {
    //        Name: "CAREFIRST CAQH FORM-Primecare",
    //        FileName: "CAREFIRST_CAQH_FORM",
    //        Payer: "CAREFIRST",
    //        State: "MD"
    //    },
    //    {
    //        Name: "NPI REGISTRATION FORM-Primecare",
    //        FileName: "NPI_REGISTRATION_FORM",
    //        Payer: "CAREFIRST",
    //        State: "MD"
    //    },
    //     {
    //         Name: "CAREFIRST PRACTICE QUESTIONNAIRE-Primecare",
    //         FileName: "CAREFIRST_PRACTICE_QUESTIONNAIRE",
    //             Payer:"CAREFIRST",
    //              State:"MD"
    //     },
          //{
          //    Name: "CONFIDENTIAL PROTECTED PEER REVIEW DOCUMENT",
          //    FileName: "CONFIDENTIAL_PROTECTED_PEER_REVIEW_DOCUMENT",
          //    Payer: "",
          //    State: ""
          //},
          // {
          //     Name: "HCA-REQUEST FOR CONSIDERATION",
          //     FileName: "HCA-REQUEST_FOR_CONSIDERATION",
          //     Payer: "",
          //     State: ""
          // },
          // {
          //     Name: "HCA-PINELLAS MARKET BYLAWS AGREEMENT",
          //     FileName: "HCA-PINELLAS_MARKET_BYLAWS_AGREEMENT",
          //     Payer: "",
          //     State: ""

          // },
          // {
          //     Name: "PROVIDER SIGNATURE STATEMENT",
          //     FileName: "PROVIDER_SIGNATURE_STATEMENT",
          //     Payer: "",
          //     State: ""
          // },
          // {
          //     Name: "OHH REFLEX TESTING ACKNOWLEDGEMENT",
          //     FileName: "OHH_REFLEX_TESTING_ACKNOWLEDGEMENT",
          //     Payer: "",
          //     State: ""
          // },

          // {
          //     Name: "LARGO MEDICAL CENTER REFLEX TESTING ACKNOWLEDGEMENT",
          //     FileName: "LARGO_MEDICAL_CENTER_REFLEX_TESTING_ACKNOWLEDGEMENT",
          //     Payer: "",
          //     State: ""
          // },
          // {
          //     Name: "NORTHSIDE HOSPITAL REFLEX TESTING ACKNOWLEDGEMENT",
          //     FileName: "NORTHSIDE_HOSPITAL_REFLEX_TESTING_ACKNOWLEDGEMENT",
          //     Payer: "",
          //     State: ""
          // },
          // {
          //     Name: "ST. PETE GENERAL HOSPITAL REFLEX TESTING ACKNOWLEDGEMENT",
          //     FileName: "ST_PETE_GENERAL_HOSPITAL_REFLEX_TESTING_ACKNOWLEDGEMENT",
          //     Payer: "",
          //     State: ""
          // },
          // {
          //     Name: "NORTHSIDE HOSPITAL ACKNOWLEDGMENT CARD",
          //     FileName: "NORTHSIDE_HOSPITAL_ACKNOWLEDGMENT_CARD",
          //     Payer: "",
          //     State: ""
          // },
          // {
          //     Name: "NORTHSIDE HOSPITAL CODE OF CONDUCT AGREEMENT",
          //     FileName: "NORTHSIDE_HOSPITAL_CODE_OF_CONDUCT_AGREEMENT",
          //     Payer: "",
          //     State: ""
          // },
          // {
          //     Name: "NORTHSIDE HOSPITAL HAND HYGIENE",
          //     FileName: "NORTHSIDE_HOSPITAL_HAND_HYGIENE",
          //     Payer: "",
          //     State: ""
          // },
          // {
          //     Name: "LARGO MEDICAL CENTER MEDICAL STAFF COVERAGE AGREEMENT",
          //     FileName: "LARGO_MEDICAL_CENTER_MEDICAL_STAFF_COVERAGE_AGREEMENT",
          //     Payer: "",
          //     State: ""
          // },
          // {
          //     Name: "BAYFRONT-ALLIED PRE APPLICATION",
          //     FileName: "BAYFRONT-ALLIED_PRE_APPLICATION",
          //     Payer: "",
          //     State: ""
          // },
          // {
          //     Name: "BAYFRONT-PHYSICIAN PRE APPLICATION",
          //     FileName: "BAYFRONT-PHYSICIAN_PRE_APPLICATION",
          //     Payer: "",
          //     State: ""
          // },
          // {
          //     Name: "FLORIDA FINANCIAL RESPONSIBILITY FORM",
          //     FileName: "FLORIDA_FINANCIAL_RESPONSIBILITY_FORM",
          //     Payer: "",
          //     State: ""
          // },
          // {
          //     Name: "BCBS NC RELEASE ATTESTATION STATEMENT",
          //     FileName: "BCBS_NC_RELEASE_ATTESTATION_STATEMENT",
          //     Payer: "",
          //     State: ""
          // },
    //       {
    //           Name: "FEDERAL COMMUNICATIONS COMMISSION ATTESTATION STATEMENT-Primecare",
    //           FileName: "FEDERAL_COMMUNICATIONS_COMMISSION_ATTESTATION_STATEMENT",
    //           Payer: "First Carolina Care Insurance",
    //           State: "N"
    //       },
    //       {
    //           Name: "FIRST CAROLINA CARE PROVIDER INFORMATION CHANGE FORM-Primecare",
    //           FileName: "FIRST_CAROLINA_CARE_PROVIDER_INFORMATION_CHANGE_FORM",
    //           Payer: "First Carolina Care Insurance",
    //           State: "N"
    //       }
    //       ,
    //       {
    //           Name: "NORTH CAROLINA COVENTRY UNIFORM CREDENTIALING APPLICATION-Primecare",
    //           FileName: "North_Carolina_Coventry_Uniform_Credentialing_Application",
    //           Payer: "COVENTRY",
    //           State: "NC"
    //       }
    //];

    $scope.loadingAjax = true;
    
    //==============Get All Form Data form Back-end=========================//
        $http.get(rootDir + '/Credentialing/PDFMapping/GetAllPlanFormData').success(function (data) {
            try {
                if (data != null) {
                   
                     var NewFormData = [];
                    
                     $scope.NewData = data;
                     console.log("FormData");
                    console.log($scope.NewData);
                    for (var i = 0; i < $scope.NewData.length; i++) {
                        var temp = [];
                        if ($scope.NewData[i].PlanFormPayer != null) {
                            temp.Payer = $scope.NewData[i].PlanFormPayer.Payer;
                        }
                        if ($scope.NewData[i].PlanFormRegion != null) {
                            temp.Region = $scope.NewData[i].PlanFormRegion.Region;
                        }
                        temp.Name = $scope.NewData[i].PlanFormName;
                        temp.FileName = $scope.NewData[i].PlanFormFileName;
                        NewFormData.push(temp);
                    }
                  
                    $scope.tempPdfFileList = NewFormData;

                }
            } catch (e) {

            }
        }).error(function () {
        })
    
    $http.get(rootDir + '/Credentialing/CnD/GetAllDocuments?&profileID=' + CredID.Profile.ProfileID).success(function (data) {
        try {
            if (data != null) {
                if (data.ProfileDocuments != null) {
                    $scope.ProfileDocumentTemp = angular.copy(data.ProfileDocuments);
                    for (var i = 0; i < $scope.ProfileDocumentTemp.length; i++) {
                        $scope.ProfileDocument.push({ Title: $scope.ProfileDocumentTemp[i].Title, FilePath: $scope.ProfileDocumentTemp[i].DocPath, isType: "PlanProfileDocument", isCheck: false });
                    }
                }
                if (data.OtherDocuments != null) {
                    $scope.PlanFormTemp = angular.copy(data.OtherDocuments);
                    for (var i = 0; i < $scope.PlanFormTemp.length; i++) {
                        if ($scope.PlanFormTemp[i].DocumentCategory == "PlanForm") {
                            $scope.PlanForm.push({ Title: $scope.PlanFormTemp[i].Title, FilePath: $scope.PlanFormTemp[i].DocumentPath, isType: "PlanForm", isCheck: false });
                        }
                        else if ($scope.PlanFormTemp[i].DocumentCategory == "ProfileDocument") {
                            $scope.ProfileDocument.push({ Title: $scope.PlanFormTemp[i].Title, FilePath: $scope.PlanFormTemp[i].DocumentPath, isType: "PlanProfileDocument", isCheck: false });
                        }
                    }
                }

                if (data.PackageGenerator != null) {
                    for (var c = 0; c < data.PackageGenerator.length; c++) {
                        if (data.PackageGenerator[c].PlanID == CredID.Plan.PlanID) {
                            $scope.GetAllPackage.push(data.PackageGenerator[c]);
                        }
                    }
                }
                $scope.loadingAjax = false;
            }
        } catch (e) {
         
        }
    }).error(function () {

    })

  
    $scope.AttachPlanPanelLoadingAjax = true;

   
    $scope.ViewTemplate = function (id, templateName) {
        $scope.AttachPlanPanelLoadingAjax = false;

        if (templateName == "North_Carolina_Coventry_Uniform_Credentialing_Application" || templateName == "Tricare_Prime_Credentialing_Application")
        {
            $http.get(rootDir + '/Credentialing/PDFMapping/GetOldPDFMappingProfileData?profileId=' + id + '&templateName=' + templateName).

            success(function (data) {
                try {
            //var open_link = window.open('', '_blank');
            //open_link.location = '/Document/View?path=/ApplicationDocument/GeneratedTemplatePdf/' + JSON.parse(data);
                    var substringvalue = $scope.tempObject.selectedFileName;
                    $scope.tempObject.selectedFileName = "/ApplicationDocument/GeneratedTemplatePdf/" + JSON.parse(data);
            //$scope.PlanForm.push({ Title: 'GeneratedForm' + JSON.parse(data) + '_' + substringvalue, FilePath: $scope.tempObject.selectedFileName, isType: "PlanForm", isCheck: true });
                    $scope.PlanForm.push({ Title: 'GeneratedForm' + JSON.parse(data), FilePath: $scope.tempObject.selectedFileName, isType: "PlanForm", isCheck: true });

                    $scope.ToggleSelection($scope.PlanForm[$scope.PlanForm.length - 1]);
                    $scope.cancelAdd();
        } catch (e) {
                 
        }

        });
    }
     else
     {
        $http.get(rootDir + '/Credentialing/PDFMapping/GetPDFMappingProfileData?profileId=' + id + '&templateName=' + templateName).

           success(function (data) {
               try {
                   //var open_link = window.open('', '_blank');
                   //open_link.location = '/Document/View?path=/ApplicationDocument/GeneratedTemplatePdf/' + JSON.parse(data);
                   var substringvalue = $scope.tempObject.selectedFileName;
                   $scope.tempObject.selectedFileName = "/ApplicationDocument/GeneratedTemplatePdf/" + JSON.parse(data);
                   //$scope.PlanForm.push({ Title: 'GeneratedForm' + JSON.parse(data) + '_' + substringvalue, FilePath: $scope.tempObject.selectedFileName, isType: "PlanForm", isCheck: true });
                   $scope.PlanForm.push({ Title: 'GeneratedForm' + JSON.parse(data), FilePath: $scope.tempObject.selectedFileName, isType: "PlanForm", isCheck: true });

                   $scope.ToggleSelection($scope.PlanForm[$scope.PlanForm.length - 1]);
                   $scope.cancelAdd();
               } catch (e) {
                 
               }

           });
        }
       $scope.NewpdfTemplateList = [];
        $scope.NewpdfTemplateList = $scope.tempPdfFileList;
    };

    $scope.searchCumDropDownforSelectPayer = function (divId)
    {
        if (($scope.tempObject.seletedPDFName != "" && $scope.tempObject.seletedPDFName != undefined) && ($scope.tempObject.seletedPDFStateName != "" && $scope.tempObject.seletedPDFStateName != undefined)) {

            $scope.NewpdfTemplateList = [];
            for (var i = 0 ; i < $scope.tempPdfFileList.length ; i++) {
                if ($scope.tempObject.seletedPDFStateName == $scope.tempPdfFileList[i].Region) {
                    if ($scope.tempObject.seletedPDFName == $scope.tempPdfFileList[i].Name) {
                        $scope.NewpdfTemplateList.push($scope.tempPdfFileList[i]);
                    }
                }

            }
        }
        else if ($scope.tempObject.seletedPDFName != "" && $scope.tempObject.seletedPDFName != undefined) {
            $scope.NewpdfTemplateList = [];
            for (var i = 0 ; i < $scope.tempPdfFileList.length ; i++) {
                if ($scope.tempObject.seletedPDFName == $scope.tempPdfFileList[i].Name) {
                    $scope.NewpdfTemplateList.push($scope.tempPdfFileList[i]);
                }
            }
        }
        else if ($scope.tempObject.seletedPDFStateName != "" && $scope.tempObject.seletedPDFStateName != undefined) {
            $scope.NewpdfTemplateList = [];
            for (var i = 0 ; i < $scope.tempPdfFileList.length ; i++) {
                if ($scope.tempObject.seletedPDFStateName == $scope.tempPdfFileList[i].Region) {
                    $scope.NewpdfTemplateList.push($scope.tempPdfFileList[i]);
                }
            }
        }
        else {
            $scope.NewpdfTemplateList = $scope.tempPdfFileList;
        }

        $("#" + divId).show();
        $("#ForTypePlanState").hide();
        $("#ForTypePlan").hide();
    };

    //---------Code to get the selected option of Payer-------//

    $scope.addIntoTypePdfDropDown1 = function (payer, div)
    {
        $scope.tempObject.seletedPDFPayerName = payer.Payer;

        $scope.NewpdfTemplateList = [];
        if ($scope.tempObject.seletedPDFName != "" || $scope.tempObject.seletedPDFName != undefined)
        {
            for (var i = 0 ; i < $scope.tempPdfFileList.length ; i++) {
                if ($scope.tempObject.seletedPDFName == $scope.tempPdfFileList[i].Name) {
                    $scope.NewpdfTemplateList.push($scope.tempPdfFileList[i]);
                }
            }
        }
       else if ($scope.tempObject.seletedPDFPayerName == "" ||  $scope.tempObject.seletedPDFPayerName == undefined)
        {
            $scope.tempObject.seletedPDFName = "";
            $scope.tempObject.selectedFileName = "";
        }
        else
        {
            $scope.NewpdfTemplateList = [];
        for (var i = 0 ; i < $scope.tempPdfFileList.length ; i++)
        {
            if ($scope.tempObject.seletedPDFPayerName == $scope.tempPdfFileList[i].Payer)
            {
                $scope.NewpdfTemplateList.push($scope.tempPdfFileList[i]);
            }
        }
        }
        $scope.IsFileSelected = false;
        $("#" + div).hide();
    }


    $scope.searchCumDropDownforSelectState = function (divId)
    {
        if (($scope.tempObject.seletedPDFName != "" && $scope.tempObject.seletedPDFName != undefined) && ($scope.tempObject.seletedPDFPayerName != "" && $scope.tempObject.seletedPDFPayerName != undefined))
        {
            $scope.NewpdfTemplateList = [];
            for (var i = 0 ; i < $scope.tempPdfFileList.length ; i++)
            {
                if ($scope.tempObject.seletedPDFPayerName == $scope.tempPdfFileList[i].Payer)
                {
                    if ($scope.tempObject.seletedPDFName == $scope.tempPdfFileList[i].Name)
                    {
                        $scope.NewpdfTemplateList.push($scope.tempPdfFileList[i]);
                    }
                }
                
            }
        }
        else if ($scope.tempObject.seletedPDFName != "" && $scope.tempObject.seletedPDFName != undefined)
        {
            $scope.NewpdfTemplateList = [];
            for (var i = 0 ; i < $scope.tempPdfFileList.length ; i++) {
                    if ($scope.tempObject.seletedPDFName == $scope.tempPdfFileList[i].Name) {
                        $scope.NewpdfTemplateList.push($scope.tempPdfFileList[i]);
                    }
            }
        }
        else if ($scope.tempObject.seletedPDFPayerName != "" && $scope.tempObject.seletedPDFPayerName != undefined)
        {
            $scope.NewpdfTemplateList = [];
            for (var i = 0 ; i < $scope.tempPdfFileList.length ; i++) {
                if ($scope.tempObject.seletedPDFPayerName == $scope.tempPdfFileList[i].Payer) {
                    $scope.NewpdfTemplateList.push($scope.tempPdfFileList[i]);
                }
            }
        }
        else {
            $scope.NewpdfTemplateList = $scope.tempPdfFileList;
        }
     
        $scope.IsFileSelected = false;
        $("#" + divId).show();
        $("#ForTypePlanPayer").hide();
        $("#ForTypePlan").hide();
    };

    //---------Code to get the selected option of Region-------//
    $scope.addIntoTypePdfDropDown2 = function (state, div) {

        $scope.tempObject.seletedPDFStateName = state.Region;
        $scope.NewpdfTemplateList = [];
        if ($scope.tempObject.seletedPDFName != "" || $scope.tempObject.seletedPDFName != undefined)
        {
            for (var i = 0 ; i < $scope.tempPdfFileList.length ; i++) {
                if ($scope.tempObject.seletedPDFName == $scope.tempPdfFileList[i].Name) {
                    $scope.NewpdfTemplateList.push($scope.tempPdfFileList[i]);
                }
            }
        }
        else if ($scope.tempObject.seletedPDFStateName == "" || $scope.tempObject.seletedPDFStateName == undefined)
        {
            $scope.tempObject.seletedPDFName = "";
            $scope.tempObject.selectedFileName = "";
        }
        else {
            $scope.NewpdfTemplateList = [];
            for (var i = 0 ; i < $scope.tempPdfFileList.length ; i++) {
                if ($scope.tempObject.seletedPDFStateName == $scope.tempPdfFileList[i].Region) {
                    $scope.NewpdfTemplateList.push($scope.tempPdfFileList[i]);
                }
            }
        }

        $scope.IsFileSelected = false;
        // $("#ForTypePlanState").hide();
        $("#" + div).hide();
    }


    $scope.searchCumDropDownforGeneratePlan = function () {
        $scope.IsFileSelected = true;

        if (($scope.tempObject.seletedPDFPayerName == undefined && $scope.tempObject.seletedPDFStateName == undefined) && ($scope.tempObject.seletedPDFPayerName == "" && $scope.tempObject.seletedPDFStateName == ""))
        {
            $scope.NewpdfTemplateList = $scope.tempPdfFileList;
        }
        else
        {
            if (($scope.tempObject.seletedPDFPayerName != "" && $scope.tempObject.seletedPDFPayerName != undefined) && ($scope.tempObject.seletedPDFStateName != "" && $scope.tempObject.seletedPDFStateName != undefined))
                {
                    $scope.NewpdfTemplateList = [];
                    for (var i = 0 ; i < $scope.tempPdfFileList.length ; i++) {
                        if ($scope.tempObject.seletedPDFPayerName == $scope.tempPdfFileList[i].Payer) {
                            if ($scope.tempObject.seletedPDFStateName == $scope.tempPdfFileList[i].Region) {
                                $scope.NewpdfTemplateList.push($scope.tempPdfFileList[i]);
                            }
                        }
                    }
                }
            else if ($scope.tempObject.seletedPDFPayerName != "" && $scope.tempObject.seletedPDFPayerName != undefined)
                {
                $scope.NewpdfTemplateList = [];
                    for (var i = 0 ; i < $scope.tempPdfFileList.length ; i++)
                    {
                       if ($scope.tempObject.seletedPDFPayerName == $scope.tempPdfFileList[i].Payer)
                       {
                           $scope.NewpdfTemplateList.push($scope.tempPdfFileList[i]);
                        }
                    }
                 }
            else if ($scope.tempObject.seletedPDFStateName != "" && $scope.tempObject.seletedPDFStateName != undefined)
            {
                $scope.NewpdfTemplateList = [];
                   for (var i = 0 ; i < $scope.tempPdfFileList.length ; i++)
                   {
                       if ($scope.tempObject.seletedPDFStateName == $scope.tempPdfFileList[i].Region)
                       {
                           $scope.NewpdfTemplateList.push($scope.tempPdfFileList[i]);
                        }
                    }
            } else {
                $scope.NewpdfTemplateList = $scope.tempPdfFileList;
            }
        }
        $("#ForTypePlanState").hide();
        $("#ForTypePlanPayer").hide();
        $("#ForTypePlan").show();
    };

    $scope.tempObject.seletedPDFName = "";
    $scope.tempObject.seletedPDFPayerName = "";
    $scope.tempObject.seletedPDFStateName = "";

    $scope.NewpdfTemplateList = [];
    
    $scope.addIntoTypePdfDropDown = function (type) {
        
        $scope.tempObject.seletedPDFName = type.Name;
        $scope.tempObject.selectedFileName = type.FileName;

        if ($scope.tempObject.seletedPDFName != "" || $scope.tempObject.seletedPDFName != undefined)
        { 
            $scope.NewpdfTemplateList = [];
            for(var i = 0 ; i < $scope.tempPdfFileList.length ; i++)
            {
                if ($scope.tempObject.seletedPDFName == $scope.tempPdfFileList[i].Name)
                {
                     $scope.NewpdfTemplateList.push($scope.tempPdfFileList[i]);
                }
            }
        }
       
        $scope.IsFileSelected = false;
        $("#ForTypePlan").hide();
        
    }

   

    $scope.ToggleSelection = function (data) {


        var id = $scope.PackageGenerated.indexOf(data);
        if (id > -1) {
            $scope.PackageGenerated.splice(id, 1);
        }
        else {
            $scope.PackageGenerated.push(data);
        }
    }
    $scope.moveDocumentUp = function (index) {

        $scope.tempObj = $scope.PackageGenerated[index];
        $scope.PackageGenerated[index] = $scope.PackageGenerated[index - 1];
        $scope.PackageGenerated[index - 1] = $scope.tempObj;
        $scope.tempObj = {};
    }

    $scope.moveDocumentDown = function (index) {

        $scope.tempObj = $scope.PackageGenerated[index];
        $scope.PackageGenerated[index] = $scope.PackageGenerated[index + 1];
        $scope.PackageGenerated[index + 1] = $scope.tempObj;
        $scope.tempObj = {};
    }
    $scope.AddPackage = function () {
        $scope.AddPackageStatus = true;
        $scope.AddPackageStatus1 = true;
        for (var c = 0; c < $scope.ProfileDocument.length; c++) {
            $scope.ProfileDocument[c].isCheck = false;
        }
        for (var c = 0; c < $scope.PlanForm.length; c++) {
            $scope.PlanForm[c].isCheck = false;
        }
        $scope.PackageGenerated = [];
        $scope.myvar = 0;
        //document.getElementById("DocumentForm2").reset();
        $scope.AttachProfileDocumentPanel = false;
        $scope.isErrorForPlanDocument = false;
        //document.getElementById("DocumentForm1").reset();
        $scope.AttachPlanFormPanel = false;
        // $scope.isErrorForDocumentPath = false;
    }

    $scope.GeneratePackage = function () {
        $scope.loadingAjax = true;
        for (var i = 0; i < $scope.PackageGenerated.length; i++) {
            $scope.PackageDocList.push($scope.PackageGenerated[i].FilePath);
        }

        $http({
            url: rootDir + '/Credentialing/CnD/GeneratePackage',
            method: "GET",
            params: { profileId: CredID.Profile.ProfileID, LastCount: $scope.GetAllPackage.length, pdflist: $scope.PackageDocList, planId: CredID.Plan.PlanID }
        })
                .success(function (data) {

                    try {
                        //var open_link = window.open('', '_blank');
                        //open_link.location = '/Document/View?path=/ApplicationDocument/GeneratedTemplatePdf/' + data.PackageName;
                        $scope.AddPackageStatus1 = false;
                        $scope.PackageGenerated = [];
                        $scope.PackageDocList = [];
                        $scope.GetAllPackage.push(data);
                        $scope.$emit('package', { Package: data });
                        for (var i = 0; i < $scope.PlanForm.length; i++) {
                            $scope.PlanForm[i].isCheck = false;
                        }
                        for (var i = 0; i < $scope.ProfileDocument.length; i++) {
                            $scope.ProfileDocument[i].isCheck = false;
                        }
                        $scope.loadingAjax = false;
                        $scope.AddPackageStatus = false;
                        messageAlertEngine.callAlertMessage('PackageAdd', "Package Added Successfully !!!", "success", true);
                    } catch (e) {
                       
                    }


                });
    }
    $scope.AttachDocumentForProfileDocuments = function () {
        $scope.myvar = 0;
        $scope.ProfileDocumentSaveStatus = "";
        $scope.AttachProfileDocumentPanel = true;

    }
    $scope.cancelAddProfileDocuments = function () {
        $scope.myvar = 0;
        $scope.ProfileDocumentSaveStatus = "";
        $("#" + "newDocumentForm2").find("form")[0].reset();
        $scope.AttachProfileDocumentPanel = false;

    }
    $scope.isPlanDocument = false;
    $scope.AttachDocumentForPlanForm = function () {
        $scope.AttachPlanPanelLoadingAjax = true;
        $scope.AttachPlanFormPanel = true;
        $scope.isPlanDocument = true;
        $scope.isGeneratePdf = false;
        $scope.PlanDocumentSaveStatus = "";
    }
    $scope.GeneratePdf = function () {
        $scope.isGeneratePdf = true;
        $scope.isPlanDocument = false;
        $scope.AttachPlanFormPanel = true;
       // $scope.NewpdfTemplateList = [];
        //$scope.NewpdfTemplateList = $scope.tempPdfFileList;
    }

    $scope.cancelAdd = function () {
        $scope.tempObject = { "BusinessEntity": "", "ContractLOBs": [], "ContractSpecialties": [], "ContractPracticeLocations": [], "AllSpecialtiesSelectedYesNoOption": 0, "AllLOBsSelectedYesNoOption": 0, "AllPracticeLocationsSelectedYesNoOption": 0, "UseExistingTemplate": "NO" };
        $scope.AttachPlanFormPanel = false;
        $scope.tempObject.seletedPDFName = '';
        $scope.NewpdfTemplateList = [];
        $scope.NewpdfTemplateList = $scope.tempPdfFileList;
        $scope.IsFileSelected = true;
        //dirty changed false to true
        $scope.AttachPlanPanelLoadingAjax = true;
    }
    $scope.cancelAddPlanForm = function () {
        //$("#" + "newDocumentForm1").find("form")[0].reset();
        $scope.IsFileSelected = false;
        $scope.AttachPlanFormPanel = false;
        $scope.isErrorForPlanDocument = false;
    };


    $scope.clearForm = function () {
        $scope.myvar = 0;
        $scope.cancelAddPlanForm();

    }

    $scope.myvar = 0;
    //$scope.isErrorForDocumentPath = false;
    $scope.ProfileDocumentSaveStatus = "";
    $scope.AttachProfileDocumentPanelLoadingAjax = true;
    $scope.addnew = false;
    $scope.saveProfileDocuments = function (Form_Div_Id, profileId) {
        $scope.addnew = true;
        var $formProfileDocuments = $("#" + Form_Div_Id).find("form");
        ResetFormForValidation($formProfileDocuments);
        validationProfileDocuments = $formProfileDocuments.valid();
        if ($('#filepath2').val() == "") {
            //$scope.myvar = 1;
            $scope.ProfileDocumentSaveStatus = "Please select the Attach Document *.";
            validationProfileDocuments = false;
        } else {
            $scope.myvar = 0;
            $scope.ProfileDocumentSaveStatus = "";
        }
        if (validationProfileDocuments) {
            $scope.AttachProfileDocumentPanelLoadingAjax = true;
            $.ajax({
                url: rootDir + '/Profile/DocumentRepository/AddProfileDocumentAsync?profileId=' + profileId,
                type: 'POST',
                data: new FormData($formProfileDocuments[0]),
                //async: false,
                cache: false,
                contentType: false,
                processData: false,
                success: function (data) {
                    try {
                        if (data.status == "true") {
                            $scope.TempObject = null;
                            $scope.ProfileDocument.push({ Title: data.otherDocument.Title, FilePath: data.otherDocument.DocumentPath, isType: "PlanProfileDocument", isCheck: true });
                            $scope.ToggleSelection($scope.ProfileDocument[$scope.ProfileDocument.length - 1]);
                            $scope.AttachProfileDocumentPanel = false;
                            //messageAlertEngine.callAlertMessage("DocumentAdded", "Profile Document saved successfully.", "success", true);
                            $formProfileDocuments[0].reset();
                            $scope.addnew = false;
                        } else {
                            $scope.myvar = 1;
                            $scope.AttachProfileDocumentPanelLoadingAjax = true;
                            $scope.ProfileDocumentSaveStatus = "Please select the file of type .pdf, jpeg, .png, .jpg, .bmp";
                            //$formProfileDocuments[0].reset();                           
                        }
                        $scope.$apply();
                    } catch (e) {
                    }
                }
            });
        }
       //$scope.ProfileDocumentSaveStatus = "";
    };
    $scope.isErrorForPlanDocument = false;
    $scope.PlanDocumentSaveStatus = "";
    $scope.AttachPlanPanelLoadingAjax = true;
    $scope.savePlanDocuments = function (Form_Div_Id, profileId) {
        var $formDataPlanDocuments = $("#" + Form_Div_Id).find("form");
        ResetFormForValidation($formDataPlanDocuments);
        console.log($('#filepath1').val());
        if ($('#filepath1').val() == "") {
            //$scope.isErrorForPlanDocument = true;
            $scope.PlanDocumentSaveStatus = "Please select the Attach Document *.";
        }
        else {
            $scope.isErrorForPlanDocument = false;
        }
        if ($formDataPlanDocuments.valid()) {
            //$scope.AttachPlanPanelLoadingAjax = false;
            $.ajax({
                url: rootDir + '/Profile/DocumentRepository/AddPlanDocumentAsync?profileId=' + profileId,
                type: 'POST',
                data: new FormData($formDataPlanDocuments[0]),
                //async: false,
                cache: false,
                contentType: false,
                processData: false,
                success: function (data) {
                    try {
                        //$scope.AttachPlanPanelLoadingAjax = true;
                        if (data.status == "true") {
                            $scope.TempObject = null;
                            $scope.PlanForm.push({ Title: data.otherDocument.Title, FilePath: data.otherDocument.DocumentPath, isType: "PlanForm", isCheck: true });
                            $scope.ToggleSelection($scope.PlanForm[$scope.PlanForm.length - 1]);
                            $scope.AttachPlanFormPanel = false;
                            $scope.isPlanDocument = false;
                            //messageAlertEngine.callAlertMessage("PlanFormAdded", "Plan Form saved successfully.", "success", true);
                            $formDataPlanDocuments[0].reset();
                        } else {
                            $scope.isErrorForPlanDocument = true;
                            $scope.PlanDocumentSaveStatus = "Please select the file of type .pdf, jpeg, .png, .jpg, .bmp";
                            //ResetFormForValidation($formDataPlanDocuments[0].reset());
                            //messageAlertEngine.callAlertMessage("alertOtherDocumentError", data.status.split(","), "danger", true);
                        }
                        $scope.$apply();
                    }
                    catch (e) {
                    }
                }
            });
        }
        else {
            //$scope.AttachPlanPanelLoadingAjax = true;
        }
       
    };

    $scope.loadingAjax = false;
    $scope.MyPanelToggle = function (divId) {
        $("#" + divId).slideToggle();
    };
})

function ResetFormForValidation(form) {
    form.removeData('validator');
    form.removeData('unobtrusiveValidation');
    $.validator.unobtrusive.parse(form);
}
