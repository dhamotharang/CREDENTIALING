
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



    $scope.pdfTemplateList = [
        {
            Name: "GHI",
            FileName: "GHI"
        },
        {
            Name: "AETNA",
            FileName: "AETNACOVENTRYTemplate"
        },
        {
            Name: "ATPL 2015",
            FileName: "ATPL2015"
        },
        {
            Name: "TRICARE PROVIDER APPLICATION",
            FileName: "TRICARE_PROVIDER_APPLICATION"
        }, {
            Name: "LETTER OF INTENT",
            FileName: "Letter_of_Intent"
        },
        {
            Name: "TRICARE PA APPLICATION",
            FileName: "TRICARE_PA_APPLICATION"
        },
        //{
        //    Name: "ALLIED CREDENTIALING APPLICATION",
        //    FileName: "ALLIED_CREDENTIALING_APPLICATION"
        //},
        {
            Name: "ULTIMATE CREDENTIALING APPLICATION PRACTIONER 2015",
            FileName: "UltimateCredentialingApplicationPractitioner_2015"
        }, {
            Name: "BCBS PAYMENT AUTH FORM",
            FileName: "BCBS_PAYMENT_AUTH_FORM"
        },
        {
            Name: "BCBS PROVIDER UPDATE FORM",
            FileName: "BCBS_PROVIDER_UPDATE_FORM"
        }, {
            Name: "BCBS PROVIDER REGISTRATION FORM",
            FileName: "BCBS_PROVIDER_REGISTRATION_FORM"
        },
        {
            Name: "MEDICAID GROUP MEMBERSHIP AUTHORIZATION FORM",
            FileName: "MEDICAID_GROUP_MEMBERSHIP_AUTHORIZATION_FORM"
        }, {
            Name: "MEDICAID MCO TREAT PROVIDER ATTESTATION FORM",
            FileName: "MEDICAID_MCO_TREAT_PROVIDER_ATTESTATION"
        },
        {
            Name: "TRICARE ARNP APPLICATION FORM",
            FileName: "TRICARE_ARNP_APPLICATION_FORM"
        },
        //{
        //    Name: "Freedom",
        //    FileName: "Freedom"
        //},
        //{
        //    Name: "Optimum",
        //    FileName: "Optimum"
        //},
        //{
        //    Name: "FL INSURENCE PROVIDER ATTESTATION",
        //    FileName: "FL_INSURANCE_PROVIDER_ATTESTATION"
        //},
        //{
        //    Name: "FL HOSPITAL ADMIT ATTESTATION",
        //    FileName: "FL_HOSPITAL_ADMIT_ATTESTATION"
        //},
        //{
        //    Name: "FL FINANCIAL RESPONSIBILITY STATEMENT GLOBAL",
        //    FileName: "FL_FINANCIAL_RESPONSIBILITY_STATEMENT_GLOBAL"
        //},
        //{
        //    Name: "FL 3000 PCP ATTESTATION OF PATIENT LOAD 2015 GLOBAL",
        //    FileName: "FL_3000_PCP_Attestation_of_Patient_Load_2015_Global"
        //},
        //{
        //    Name: "PHYSICIAN CREDENTIALING APPLICATION & DISCLOSURE OF OWNERSHIP FORM",
        //    FileName: "PHYSICIAN_CREDENTIALING_APPLICATION_DISCLOSURE_OF_OWNERSHIP"
        //},
        {
            Name: "WELLCARE MIDLEVEL FORM",
            FileName: "WELLCARE_MIDLEVEL_FORMS"
        }
    ];



    $scope.loadingAjax = true;
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


    $scope.tempPdfFileList = angular.copy($scope.pdfTemplateList);
    $scope.AttachPlanPanelLoadingAjax = true;

    $scope.ViewTemplate = function (id, templateName) {
        $scope.AttachPlanPanelLoadingAjax = false;

        $http.get(rootDir + '/Credentialing/PDFMapping/GetPDFMappingProfileData?profileId=' + id + '&templateName=' + templateName).

            success(function (data) {
                try {
                    //var open_link = window.open('', '_blank');
                    //open_link.location = '/Document/View?path=/ApplicationDocument/GeneratedTemplatePdf/' + JSON.parse(data);
                    var substringvalue = $scope.tempObject.selectedFileName;
                    $scope.tempObject.selectedFileName = "/ApplicationDocument/GeneratedTemplatePdf/" + JSON.parse(data);
                    $scope.PlanForm.push({ Title: 'GeneratedForm' + JSON.parse(data) + '_' + substringvalue, FilePath: $scope.tempObject.selectedFileName, isType: "PlanForm", isCheck: true });

                    $scope.ToggleSelection($scope.PlanForm[$scope.PlanForm.length - 1]);
                    $scope.cancelAdd();
                } catch (e) {
                 
                }

            });

    };
    $scope.searchCumDropDownforGeneratePlan = function () {
        $scope.IsFileSelected = true;
        $scope.pdfTemplateList = angular.copy($scope.tempPdfFileList);
        $("#ForTypePlan").show();
    };
    $scope.tempObject.seletedPDFName = '';
    $scope.addIntoTypePdfDropDown = function (type) {
        $scope.tempObject.seletedPDFName = type.Name;
        $scope.tempObject.selectedFileName = type.FileName;
        $scope.pdfTemplateList = angular.copy($scope.tempPdfFileList);
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
        $("#" + "newDocumentForm2").find("form")[0].reset();
        $scope.AttachProfileDocumentPanel = false;
        $scope.isErrorForPlanDocument = false;
        $("#" + "newDocumentForm1").find("form")[0].reset();
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

    }

    $scope.cancelAdd = function () {
        $scope.tempObject = { "BusinessEntity": "", "ContractLOBs": [], "ContractSpecialties": [], "ContractPracticeLocations": [], "AllSpecialtiesSelectedYesNoOption": 0, "AllLOBsSelectedYesNoOption": 0, "AllPracticeLocationsSelectedYesNoOption": 0, "UseExistingTemplate": "NO" };
        $scope.AttachPlanFormPanel = false;
        $scope.tempObject.seletedPDFName = '';
        $scope.IsFileSelected = true;
        //dirty changed false to true
        $scope.AttachPlanPanelLoadingAjax = true;
    }
    $scope.cancelAddPlanForm = function () {
        $("#" + "newDocumentForm1").find("form")[0].reset();
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

    $scope.saveProfileDocuments = function (Form_Div_Id, profileId) {
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
