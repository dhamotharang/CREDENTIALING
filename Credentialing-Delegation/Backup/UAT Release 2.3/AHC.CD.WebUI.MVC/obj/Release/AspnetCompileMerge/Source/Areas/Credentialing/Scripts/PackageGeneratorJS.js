
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
    $scope.IsFileSelected = true;
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

   
    console.log('fsgfsgdfsghd');
    console.log(CredID);
    $http.get(rootDir + '/Credentialing/CnD/GetAllDocuments?&profileID=' + CredID.Profile.ProfileID).success(function (data) {
        if (data != null) {
            console.log(data);
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

        }
    }).error(function () {

    })


    $scope.tempPdfFileList = angular.copy($scope.pdfTemplateList);

    $scope.ViewTemplate = function (id, templateName) {
        $scope.pdfLoadingAjax = true;

        $http.get(rootDir + '/Credentialing/PDFMapping/GetPDFMappingProfileData?profileId=' + id + '&templateName=' + templateName).

            success(function (data) {
                //console.log(data);
                $scope.pdfLoadingAjax = false;

                //var open_link = window.open('', '_blank');
                //open_link.location = '/Document/View?path=/ApplicationDocument/GeneratedTemplatePdf/' + JSON.parse(data);
                var substringvalue = $scope.tempObject.selectedFileName;
                $scope.tempObject.selectedFileName = "/ApplicationDocument/GeneratedTemplatePdf/" + JSON.parse(data);
                $scope.PlanForm.push({ Title: 'GeneratedForm' + JSON.parse(data) + '_' + substringvalue, FilePath: $scope.tempObject.selectedFileName, isType: "PlanForm", isCheck: true });

                $scope.ToggleSelection($scope.PlanForm[$scope.PlanForm.length - 1]);
                $scope.cancelAdd();
                $scope.tempObject = null;
                $scope.IsFileSelected = true;
            });

    };
    $scope.searchCumDropDownforGeneratePlan = function () {
        $scope.pdfTemplateList = angular.copy($scope.tempPdfFileList);
        $("#ForTypePlan").show();
    };
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
                    console.log('San');
                    console.log(data);
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


        });
    }
    $scope.saveProfileDocument = function (Form_Div_Id, profileId) {
        var $form = $("#" + Form_Div_Id).find("form");
        ResetFormForValidation($form);
        //console.log($scope.TempObject.Title);
        $scope.isError = false;
        //var status = false;

        //status = $scope.check($scope.TempObject.Title);

        if ($form.valid()) {
            $.ajax({
                url: rootDir + '/Profile/DocumentRepository/AddProfileDocumentAsync?profileId=' + profileId,
                type: 'POST',
                data: new FormData($form[0]),
                async: false,
                cache: false,
                contentType: false,
                processData: false,
                success: function (data) {
                    //console.log(data);
                    if (data.status == "true") {

                        $scope.IsAddNewDocument = false;
                        $scope.SuccessMessage = "Document Updated Successfully !!!!!!!!!!!";
                        $scope.IsMessage = true;
                        $scope.UpdateDocumentArray(data.otherDocument);
                        $scope.TempObject = null;
                        $scope.resetForm();
                        window.DocumentForm.reset();
                        FormReset($form);

                        messageAlertEngine.callAlertMessage("alertOtherDocumentSuccess", "Other Document saved successfully.", "success", true);
                    } else {

                        $scope.message = "Please Select the Document";
                        $scope.isError = true;

                        //messageAlertEngine.callAlertMessage("alertOtherDocumentError", data.status.split(","), "danger", true);
                    }
                }
            });
        } 
    };
    $scope.savePlanForm = function (Form_Div_Id, profileId) {
        var $form = $("#" + Form_Div_Id).find("form");
        ResetFormForValidation($form);
        if ($form.valid()) {
            $.ajax({
                url: rootDir + '/Profile/DocumentRepository/AddOtherDocumentAsync?profileId=' + profileId,
                type: 'POST',
                data: new FormData($form[0]),
                async: false,
                cache: false,
                contentType: false,
                processData: false,
                success: function (data) {
                    if (data.status == "true") {
                        $scope.IsAddNewDocument = false;
                        $scope.SuccessMessage = "Document Updated Successfully !!!!!!!!!!!";
                        $scope.IsMessage = true;
                        $scope.UpdateDocumentArray(data.otherDocument);
                        $scope.TempObject = null;
                        $scope.resetForm();
                        window.DocumentForm.reset();
                        FormReset($form);
                        messageAlertEngine.callAlertMessage("alertOtherDocumentSuccess", "Other Document saved successfully.", "success", true);
                    } else {

                        $scope.message = "Please Select the Document";
                        $scope.isError = true;

                        //messageAlertEngine.callAlertMessage("alertOtherDocumentError", data.status.split(","), "danger", true);
                    }
                }
            });
        }
    };





    $scope.AttachDocumentForProfileDocuments = function () {
        $scope.AttachProfileDocumentPanel = true;

    }
    $scope.cancelAddProfileDocuments = function () {
        $scope.AttachProfileDocumentPanel = false;
    }
    $scope.AttachDocumentForPlanForm = function () {
        $scope.AttachPlanFormPanel = true;
        $scope.isGeneratePdf = false;

    }
    $scope.GeneratePdf = function () {
        $scope.isGeneratePdf = true;
        $scope.AttachPlanFormPanel = false;
    }
    $scope.cancelAdd = function () {
        $scope.isGeneratePdf = false;
    }
    $scope.cancelAddPlanForm = function () {
        $scope.AttachPlanFormPanel = false;
    }
    $scope.saveProfileDocuments = function (Form_Div_Id, profileId) {
        var $form = $("#" + Form_Div_Id).find("form");
        ResetFormForValidation($form);
        //console.log($scope.TempObject.Title);
        $scope.isError = false;
        //var status = false;

        //status = $scope.check($scope.TempObject.Title);

        if ($form.valid()) {
            $.ajax({
                url: rootDir + '/Profile/DocumentRepository/AddProfileDocumentAsync?profileId=' + profileId,
                type: 'POST',
                data: new FormData($form[0]),
                //async: false,
                cache: false,
                contentType: false,
                processData: false,
                success: function (data) {
                    //console.log(data);
                    if (data.status == "true") {
                        $scope.TempObject = null;
                        $scope.ProfileDocument.push({ Title: data.otherDocument.Title, FilePath: data.otherDocument.DocumentPath, isType: "PlanProfileDocument", isCheck: true });
                        $scope.ToggleSelection($scope.ProfileDocument[$scope.ProfileDocument.length - 1]);
                        $scope.AttachProfileDocumentPanel = false;
                        messageAlertEngine.callAlertMessage("DocumentAdded", "Profile Document saved successfully.", "success", true);
                    } else {

                        $scope.message = "Please Select the Document";
                        $scope.isError = true;

                        //messageAlertEngine.callAlertMessage("alertOtherDocumentError", data.status.split(","), "danger", true);
                    }
                }
            });
        } else {

            //if ($scope.TempObject.Title != "") {
            //    if ($scope.FilePath != "") {
            //        $scope.IsAddNewDocument = false;
            //        $("#ATTACHmore").hide();
            //        $scope.IsFail = true;
            //        $scope.FailureMessage = "Document Already Present";
            //        $timeout(function () {
            //            $scope.IsFail = false;
            //        }, 5000);
            //    }
            //    $scope.FilePath = "";
            //}

        }

    };

    $scope.savePlanDocuments = function (Form_Div_Id, profileId) {
        var $form = $("#" + Form_Div_Id).find("form");
        ResetFormForValidation($form);
        $scope.isError = false;
        if ($form.valid()) {
            $.ajax({
                url: rootDir + '/Profile/DocumentRepository/AddPlanDocumentAsync?profileId=' + profileId,
                type: 'POST',
                data: new FormData($form[0]),
                //async: false,
                cache: false,
                contentType: false,
                processData: false,
                success: function (data) {
                    //console.log(data);
                    if (data.status == "true") {
                        $scope.TempObject = null;
                        $scope.PlanForm.push({ Title: data.otherDocument.Title, FilePath: data.otherDocument.DocumentPath, isType: "PlanForm", isCheck: true });
                        $scope.ToggleSelection($scope.PlanForm[$scope.PlanForm.length - 1]);
                        $scope.AttachPlanFormPanel = false;
                        messageAlertEngine.callAlertMessage("PlanFormAdded", "Plan Form saved successfully.", "success", true);
                    } else {

                        $scope.message = "Please Select the Document";
                        $scope.isError = true;

                        //messageAlertEngine.callAlertMessage("alertOtherDocumentError", data.status.split(","), "danger", true);
                    }
                }
            });
        } else {

            //if ($scope.TempObject.Title != "") {
            //    if ($scope.FilePath != "") {
            //        $scope.IsAddNewDocument = false;
            //        $("#ATTACHmore").hide();
            //        $scope.IsFail = true;
            //        $scope.FailureMessage = "Document Already Present";
            //        $timeout(function () {
            //            $scope.IsFail = false;
            //        }, 5000);
            //    }
            //    $scope.FilePath = "";
            //}

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




