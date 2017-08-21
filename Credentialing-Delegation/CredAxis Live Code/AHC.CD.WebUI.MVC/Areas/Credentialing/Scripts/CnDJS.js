$(document).ready(function () {
   
    $("#sidemenu").addClass("menu-in");
    $("#page-wrapper").addClass("menuup");
    $('#redirecttocredentialinglistpage').removeClass('ng-hide', function () { $('#redirecttocredentialinglistpage').addClass('ng-show') });

    $("body").tooltip({ selector: '[data-toggle=tooltip]' });

    var specialElementHandlers = {
        '#editor': function (element, renderer) {
            return true;
        }
    };

    $('#printPDF').click(function () {
        var doc = new jsPDF();

        doc.fromHTML($('#render_me').get(0), 5, 5, {
            'width': 170,
            'elementHandlers': specialElementHandlers
        });

        doc.save('PSV.pdf');
    });

});

var toggleDiv = function (divId) {
    $('#' + divId).slideToggle();
};





var Cred_SPA_App = angular.module('cred_SPA_App', ['ui.filters', 'mgcrea.ngStrap', "wysiwyg.module", 'colorpicker.module']);
Cred_SPA_App.run(['$rootScope', function ($rootScope) {
    $rootScope.credentialingInfoList = {};
    //$rootScope.completeButtonStatus = false;
    //$rootScope.finalCompleteButtonStatus = false;
    if (typeof credInfo != 'undefined') {
        $rootScope.credentialingInfoList = credInfo;
        $//rootScope.completeButtonStatus = completeButtonStatus;
    }
    
}])
//Cred_SPA_App.factory('CompleteButtonSTATUS', ["$rootScope", function ($rootScope) {
//    var CompleteButtonSTATUS = {};

//    $rootScope.CompleteButtonCND = $rootScope.completeButtonStatus;
//    $rootScope.CompleteButtonCNDFinal = $rootScope.finalCompleteButtonStatus;
//    CompleteButtonSTATUS.changeCompleteButtonStatus = function (status) {
//        $rootScope.CompleteButtonCND = status;
//    };

//    return CompleteButtonSTATUS;
//}]);
Cred_SPA_App.directive('myModal', function () {
    return {
        restrict: 'A',
        link: function (scope, element, attr) {
            scope.dismiss = function () {
                element.modal('hide');
            };
        }
    }
});

Cred_SPA_App.directive('myModal1', function () {
    return {
        restrict: 'A',
        link: function (scope, element, attr) {
            scope.dismiss = function () {
                element.modal('hide');
            };
        }
    }
});

Cred_SPA_App.service('messageAlertEngine', ['$rootScope', '$timeout', 'messageAlertEngine', function ($rootScope, $timeout, $window, messageAlertEngine) {

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
            $timeout(animateMessageAlertOff, 1000);
        }
    }

    $rootScope.closeAlertMessage = function () {
        $rootScope.messageDesc = "";
        $rootScope.activeMessageDiv = "";
        $rootScope.messageType = "";
    }
}])

Cred_SPA_App.controller('Cred_SPA_Ctrl', function ($scope, $http, $location, $filter, $rootScope, $window, $timeout, messageAlertEngine) {
    //var self = this;
    //self.CompleteButtonCND = CompleteButtonSTATUS.completeButtonStatus;
    //self.CompleteButtonCNDFinal = CompleteButtonSTATUS.finalCompleteButtonStatus;

    //$rootScope.$on('CompletebuttonEvent', function (event, val) {
    //    $rootScope.completeButtonStatus = val;
    //});
    $rootScope.ShowCompleteButton = false;
    $rootScope.DisableCompleteButton = angular.copy(completeButtonStatus);
    $scope.restCompletebutton = function () {
     //   $rootScope.CompleteButtonCNDFinal = false;
        $rootScope.ShowCompleteButton = false;

    }

    $scope.EnambleCompleteButton = function () {
        $rootScope.ShowCompleteButton = true;
    }
    sessionStorage.setItem('CredID', credId);
    $scope.CredListIdView = localStorage.getItem("CreListId");
    $scope.testData = [{ Name: 'pritam', Age: '23' }, { Name: 'tannu', Age: '5' }];

    $scope.changeComplteButtonStatus = function () {
        //if ($rootScope.CompleteButtonCND) {
        //    $rootScope.CompleteButtonCNDFinal = true;
        //}
        $rootScope.ShowCompleteButton=true;

    }

    

    $scope.isDroped = false;
    $scope.$on('someEvent', function (event, data) {
        $scope.Li = angular.copy(data);
    });

    $scope.ButtonCLickName = sessionStorage.getItem('ButtonCLickName');
    $scope.users = [];
    $scope.Li = [];
    $scope.PlanReportList = [];
    //$rootScope.PlanCompleted = false;
    $scope.credentialingInfoID = credId;
    $scope.credentialingInfo = credInfo;
    //$scope.showcompletebutton = false;
    //if ($scope.credentialingInfo.CredentialingContractRequests != null && $scope.credentialingInfo.CredentialingContractRequests.length != 0) {
    //    $scope.showcompletebutton = true;
    //}
    
    $scope.CDUsers = JSON.parse(CDUsers);
    $scope.LoginUsers = JSON.parse(loginusers);
    for (var i = 0; i < $scope.LoginUsers.length; i++) {

        for (var j = 0; j < $scope.CDUsers.length; j++) {

            if ($scope.LoginUsers[i].Id == $scope.CDUsers[j].AuthenicateUserId) {

                $scope.users.push({ CDUserID: $scope.CDUsers[j].CDUserID, AuthenicateUserId: $scope.CDUsers[j].AuthenicateUserId, FullName: $scope.LoginUsers[i].FullName, Email: $scope.LoginUsers[i].Email, UserName: $scope.LoginUsers[i].UserName })
            }
        }
    }
    $scope.MonthsList = ["January", "February", "March", "April", "May", "June", "July", "August", "September", "October", "November", "December"];
    $scope.YearsList = [];
    for (i = 2015; i <= 2065; i++) {
        $scope.YearsList.push(i);
    }

    $scope.clearAssignedToCCO = function () {
        if ($scope.tempObject1.AssignedToCCO == "") {
            $scope.tempObject1.AssignedToCCOID = "";
            $scope.tempObject1.AssignedToCCO = "";
        }
    }

    $scope.SelectCCO = function (cconame) {
        $scope.tempObject1.AssignedToCCOID = cconame.CDUserID;
        $scope.tempObject1.AssignedToCCO = cconame.UserName;
        $(".ProviderTypeSelectAutoList1").hide();
    }

    $scope.selectwelcomeletterdate = function(value)
    {
        $scope.tempObject1.WelcomeLetterMailedDate = $scope.ConvertDateFormat(value);
    }

    $scope.editletter = false;
    $scope.showeditview = function () {
        $scope.editletter = true;
    }

    $scope.PrintElem = function (elem) {
        Popup($('#' + elem).html());
    }
   
    $scope.selectedmonth = "January";
    $scope.selectedyear = 2015;
    $('#monthid').change(function () {
        $scope.selectedmonth = this.value;
    })
    $('#yearid').change(function () {
        $scope.selectedyear = this.value;
    })
    //$scope.selectmonth = function()
    //{
    //    //$scope.selectedmonth = month;
    //}
    //$scope.selectyear = function (year) {
    //    $scope.selectedyear = year;
    //}

    function Popup(data) {
        var mywindow = window.open('', data, 'height=600,width=1000');
        mywindow.document.write('<html><head><title></title>');
        mywindow.document.write('<link rel="stylesheet" href="/Content/SharedCss/bootstrap.min.css" type="text/css" />');
        mywindow.document.write('<link rel="stylesheet" href="/Content/SharedCss/app.css" type="text/css" />');
        mywindow.document.write('</head><body >');
        mywindow.document.write(data);
        mywindow.document.write('</body></html>');

        mywindow.document.close(); // necessary for IE >= 10
        mywindow.focus(); // necessary for IE >= 10

        setTimeout(function () {
            mywindow.print();
            mywindow.close();

            window.close();
        }, 500);


        return true;
    }
    var proname = "";
    $scope.canceleditview = function () {
        $scope.editletter = false;
        if ($scope.tempObject1.WelcomeLetterPreparedDate == null) {
            $scope.tempObject1.WelcomeLetterPreparedDate = new Date();
        }
        $scope.welcomelettername = proname;
    }
    $scope.providername = "";
    $scope.Savewelcomeletter = function () {
        if (typeof $scope.tempObject1.WelcomeLetterPreparedDate === "undefined" || $scope.tempObject1.WelcomeLetterPreparedDate == null || $scope.tempObject1.WelcomeLetterPreparedDate =="")
        {
            $scope.tempObject1.WelcomeLetterPreparedDate = new Date();
        }
        var credentialinglogId = $scope.credentialingFilterInfo.CredentialingLogs[0].CredentialingLogID;
        console.log($scope.tempObject1.WelcomeLetterPreparedDate);
        $scope.tempObject1.ServiceCommencingDate = $scope.selectedmonth + "," + $scope.selectedyear;
        proname = $('#name').val();
        $scope.welcomelettername = $('#name').val();
        $http.get(rootDir + '/Credentialing/CnD/SaveWelcomeLetter', { params: { "profileid": $scope.credentialingFilterInfo.ProfileID, "credLogId": credentialinglogId, "name": $('#name').val(), "initialdate": $scope.tempObject1.WelcomeLetterPreparedDate, "servicecommencedate": $scope.tempObject1.ServiceCommencingDate } }).
             success(function (data) {
                 try {
                     $scope.editletter = false;
                     $scope.tempObject1.WelcomeLetterPath = data.WelcomeLetterPath;
                     $scope.tempObject1.WelcomeLetterPreparedDate = data.WelcomeLetterPreparedDate;
                     $scope.tempObject1.ServiceCommencingDate = data.ServiceCommencingDate;
                     if(data.status == "true")
                     {
                         $scope.editletter = false;
                     }
                 } catch (e) {

                 }
             });
    }
    $scope.loadData = function () {
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


                        temp.IsCkecked = false;
                        temp.IsLoading = false;
                        NewFormData.push(temp);
                    }

                    $scope.tempPdfFileList = NewFormData;
                    $scope.pdfTemplateList = NewFormData;
                    $scope.NewpdfTemplateList = $scope.pdfTemplateList;
                }
            } catch (e) {

            }
        }).error(function () {
        })
    }

    $scope.pdfProfileId = $scope.credentialingInfo.ProfileID;
    $scope.pdfLoadingAjax = false;
    $scope.ViewOnlyMode = false;
    $scope.IsFileSelected = true;
    //$scope.pdfTemplateList = [
    //    {
    //        Name: "GHI HOSPITALIST FORM",
    //        FileName: "GHI_HOSPITALIST_FORM",
    //        Payer: "GHI",
    //        State: "N"
    //    },
    //    {
    //        Name: "GHI PROVIDER FORM",
    //        FileName: "GHI_Provider_Form",
    //        Payer: "GHI",
    //        State: "NN"
    //    },
    //    {
    //        Name: "Aetna - New Provider Credentialing Form",
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
    //   {
    //       Name: "TRICARE PROVIDER APPLICATION - SOUTH",
    //       FileName: "TRICARE_PROVIDER_APPLICATION_SOUTH",
    //       Payer: "",
    //       State: ""
    //   },
    //   {
    //       Name: "TRICARE PROVIDER APPLICATION",
    //       FileName: "TRICARE_PROVIDER_APPLICATION",
    //       Payer: "",
    //       State: ""
    //   },
    //    {
    //        Name: "LETTER OF INTENT",
    //        FileName: "Letter_of_Intent",
    //        Payer: "",
    //        State: ""
    //    },
    //    {
    //        Name: "TRICARE PA APPLICATION - SOUTH",
    //        FileName: "TRICARE_PA_APPLICATION_SOUTH",
    //        Payer: "",
    //        State: ""
    //    },
    //     {
    //         Name: "TRICARE PA APPLICATION",
    //         FileName: "TRICARE_PA_APPLICATION",
    //         Payer: "",
    //         State: ""
    //     },
    //    {
    //        Name: "ALLIED CREDENTIALING APPLICATION ACCESS2",
    //        FileName: "ALLIED_CREDENTIALING_APPLICATION_ACCESS2",
    //        Payer: "",
    //        State: ""
    //    },
    //    {
    //        Name: "ALLIED CREDENTIALING APPLICATION ACCESS",
    //        FileName: "ALLIED_CREDENTIALING_APPLICATION_ACCESS",
    //        Payer: "",
    //        State: ""
    //    },
    //    //{
    //    //    Name: "ULTIMATE CREDENTIALING APPLICATION PRACTIONER FOR Specialist 2015",
    //    //    FileName: "Ultimate_Credentialing_Application_Practitioner_Specialist_2015"
    //    //},
    //    //{
    //    //    Name: "ULTIMATE CREDENTIALING APPLICATION PRACTIONER FOR PCP 2015",
    //    //    FileName: "Ultimate_Credentialing_Application_Practitioner_PCP_2015"
    //    //},
    //    //{
    //    //    Name: "ULTIMATE CREDENTIALING APPLICATION PRACTIONER FOR MIDLEVEL 2015",
    //    //    FileName: "Ultimate_Credentialing_Application_Practitioner_Midlevel_2015"
    //    //},
    //    {
    //        Name: "ULTIMATE CREDENTIALING APPLICATION",
    //        FileName: "Ultimate_Credentialing_Application",
    //        Payer: "",
    //        State: ""
    //    },
    //    {
    //        Name: "ULTIMATE RE-CREDENTIALING APPLICATION",
    //        FileName: "Ultimate_Re-Credentialing_Application",
    //        Payer: "",
    //        State: ""
    //    },
    //    {
    //        Name: "BCBS PAYMENT AUTH FORM",
    //        FileName: "BCBS_PAYMENT_AUTH_FORM",
    //        Payer: "",
    //        State: ""
    //    },
    //    {
    //        Name: "BCBS PROVIDER UPDATE FORM",
    //        FileName: "BCBS_PROVIDER_UPDATE_FORM",
    //        Payer: "",
    //        State: ""
    //    }, {
    //        Name: "BCBS PROVIDER REGISTRATION FORM",
    //        FileName: "BCBS_PROVIDER_REGISTRATION_FORM",
    //        Payer: "",
    //        State: ""
    //    },
    //    {
    //        Name: "MEDICAID GROUP MEMBERSHIP AUTHORIZATION FORM",
    //        FileName: "MEDICAID_GROUP_MEMBERSHIP_AUTHORIZATION_FORM",
    //        Payer: "",
    //        State: ""
    //    },
    //    //{
    //    //    Name: "MEDICAID MCO TREAT PROVIDER ATTESTATION FORM",
    //    //    FileName: "MEDICAID_MCO_TREAT_PROVIDER_ATTESTATION"
    //    //},
    //    {
    //        Name: "TRICARE ARNP APPLICATION FORM - SOUTH",
    //        FileName: "TRICARE_ARNP_APPLICATION_FORM_SOUTH",
    //        Payer: "",
    //        State: ""
    //    },
    //    {
    //        Name: "TRICARE ARNP APPLICATION FORM",
    //        FileName: "TRICARE_ARNP_APPLICATION_FORM",
    //        Payer: "",
    //        State: ""
    //    },
    //    {
    //        Name: "Freedom Optimum IPA",
    //        FileName: "Freedom_Optimum_IPA_Enrollment_Provider_PCP",
    //        Payer: "",
    //        State: ""

    //    },
    //    {
    //        Name: "Freedom Optimum Specialist",
    //        FileName: "Freedom_Optimum_Specialist_Package",
    //        Payer: "",
    //        State: ""
    //    },
    //    {
    //        Name: "Humana IPA",
    //        FileName: "Humana_IPA_New_PCP_Package",
    //        Payer: "",
    //        State: ""
    //    },
    //    {
    //        Name: "Humana Specialist",
    //        FileName: "Humana_Specialist_New_Provider",
    //        Payer: "",
    //        State: ""
    //    },
    //    {
    //        Name: "FL INSURENCE PROVIDER ATTESTATION",
    //        FileName: "FL_INSURANCE_PROVIDER_ATTESTATION",
    //        Payer: "",
    //        State: ""
    //    },
    //    {
    //        Name: "FL HOSPITAL ADMIT ATTESTATION",
    //        FileName: "FL_HOSPITAL_ADMIT_ATTESTATION",
    //        Payer: "",
    //        State: ""
    //    },
    //    //{
    //    //    Name: "FL FINANCIAL RESPONSIBILITY STATEMENT GLOBAL",
    //    //    FileName: "FL_FINANCIAL_RESPONSIBILITY_STATEMENT_GLOBAL"
    //    //},
    //    {
    //        Name: "FL 3000 PCP ATTESTATION OF PATIENT LOAD 2016 GLOBAL",
    //        FileName: "FL_3000_PCP_Attestation_of_Patient_Load_2016_Global",
    //        Payer: "",
    //        State: ""
    //    },
    //    //{
    //    //    Name: "PHYSICIAN CREDENTIALING APPLICATION & DISCLOSURE OF OWNERSHIP FORM",
    //    //    FileName: "PHYSICIAN_CREDENTIALING_APPLICATION_DISCLOSURE_OF_OWNERSHIP"
    //    //},
    //    {
    //        Name: "WELLCARE MIDLEVEL FORM",
    //        FileName: "WELLCARE_MIDLEVEL_FORMS",
    //        Payer: "",
    //        State: ""
    //    },
    //    //{
    //    //    Name: "ULTIMATE  RE-CREDENTIALING APPLICATION PRACTIONER FOR Specialist 2015",
    //    //    FileName: "Ultimate_Re-Credentialing_Application_Practitioner_Specialist_2015"
    //    //},
    //    //{
    //    //    Name: "ULTIMATE RE-CREDENTIALING APPLICATION PRACTIONER FOR PCP 2015",
    //    //    FileName: "Ultimate_Re-Credentialing_Application_Practitioner_PCP_2015"
    //    //},
    //    //{
    //    //    Name: "ULTIMATE RE-CREDENTIALING APPLICATION PRACTIONER FOR MIDLEVEL 2015",
    //    //    FileName: "Ultimate_Re-Credentialing_Application_Practitioner_Midlevel_2015"
    //    //},
    //    {
    //        Name: "ADMITING ARRANGEMENT FORM",
    //        FileName: "Admitting_Arrangement_Form",
    //        Payer: "",
    //        State: ""
    //    },
    //    {
    //        Name: "ATTESTATION OF SITE VISIT",
    //        FileName: "ATTESTATION_OF_SITE_VISIT",
    //        Payer: "",
    //        State: ""
    //    },
        
    //    //,
    //    //{
    //    //    Name: "UNITY JOINDER",
    //    //    FileName: "Unity_Joinder"
    //    //}
    //    {
    //        Name: "AHC PROVIDER PROFILE FOR WELLCARE FORM",
    //        FileName: "AHC_Provider_Profile_for_Wellcare",
    //        Payer: "",
    //        State: ""
    //    },
    //    {
    //        Name: "A2HC PROVIDER PROFILE FOR WELLCARE FORM",
    //        FileName: "A2HC_Provider_Profile_for_Wellcare",
    //        Payer: "",
    //        State: ""
    //    },
    //    {
    //        Name: "TRICARE PRIME CREDENTIALING APPLICATION-Primecare",
    //        FileName: "Tricare_Prime_Credentialing_Application",
    //        Payer: "",
    //        State: ""
    //    },
    //    {
    //        Name: "LOI FORM 2016",
    //        FileName: "LOI_Template_2016",
    //        Payer: "",
    //        State: ""
    //    },
    //     {

    //         Name: "STANDARD AUTHORIZATION, ATTESTATION AND RELEASE",
    //         FileName: "Authorization_Attestation_and_Release",
    //         Payer: "",
    //         State: ""
    //     } ,
    //    {

    //        Name: "TRICARE CLINICAL PSYCHOLOGIST PROVIDER APPLICATION-Primecare",
    //        FileName: "TRICARE_CLINICAL_PSYCHOLOGIST_PROVIDER_APPLICATION",
    //        Payer: "",
    //        State: ""
    //    },
    //    {

    //        Name: "TRICARE CLINICAL SOCIAL WORKER PROVIDER APPLICATION-Primecare",
    //        FileName: "TRICARE_CLINICAL_SOCIAL_WORKER_PROVIDER_APPLICATION",
    //        Payer: "",
    //        State: ""
    //    },
    //    {
    //        Name: "TRICARE PHYSICIAN / DENTIST PROVIDER APPLICATION-Primecare",
    //        FileName: "TRICARE_PHYSICIAN_DENTIST_PROVIDER_APPLICATION",
    //        Payer: "",
    //        State: ""
    //    },
    //    {

    //        Name: "TRICARE PHYSICIAN ASSISTANT PROVIDER APPLICATION-Primecare",
    //        FileName: "TRICARE_PHYSICIAN_ASSISTANT_PROVIDER_APPLICATION",
    //        Payer: "",
    //        State: ""
    //    },
    //    {
    //        Name: "TRICARE RN LPN NP PROVIDER APPLICATION-Primecare",
    //        FileName: "TRICARE_RN_LPN_NP_PROVIDER_APPLICATION",
    //        Payer: "",
    //        State: ""
    //    },
    //     {
    //         Name: "ELECTRONIC FUNDS TRANSFER AUTHORIZATION AGREEMENT-Primecare",
    //         FileName: "ELECTRONIC_FUNDS_TRANSFER_AUTHORIZATION_AGREEMENT",
    //         Payer: "",
    //         State: ""
    //     },
    //  {
    //      Name: "NON-NETWORK TRICARE PROVIDER FILE GROUP APPLICATION-Primecare",
    //      FileName: "NON-NETWORK_TRICARE_PROVIDER_FILE_GROUP_APPLICATION",
    //      Payer: "",
    //      State: ""
    //  },
    //    {
    //        Name: "CAREFIRST CAQH FORM-Primecare",
    //        FileName: "CAREFIRST_CAQH_FORM",
    //        Payer: "",
    //        State: ""
    //    },
    //    {
    //        Name: "NPI REGISTRATION FORM-Primecare",
    //        FileName: "NPI_REGISTRATION_FORM",
    //        Payer: "",
    //        State: ""
    //    },
    //     {
    //         Name: "CAREFIRST PRACTICE QUESTIONNAIRE-Primecare",
    //         FileName: "CAREFIRST_PRACTICE_QUESTIONNAIRE"
    //     },
    //      {
    //          Name: "CONFIDENTIAL PROTECTED PEER REVIEW DOCUMENT",
    //          FileName: "CONFIDENTIAL_PROTECTED_PEER_REVIEW_DOCUMENT",
    //          Payer: "",
    //          State:""
    //      },
    //       {
    //           Name: "HCA-REQUEST FOR CONSIDERATION",
    //           FileName: "HCA-REQUEST_FOR_CONSIDERATION",
    //           Payer: "",
    //           State: ""
    //       },
    //       {
    //           Name: "HCA-PINELLAS MARKET BYLAWS AGREEMENT",
    //           FileName: "HCA-PINELLAS_MARKET_BYLAWS_AGREEMENT",
    //           Payer: "",
    //           State: ""

    //       },
    //       {
    //           Name: "PROVIDER SIGNATURE STATEMENT",
    //           FileName: "PROVIDER_SIGNATURE_STATEMENT",
    //           Payer: "",
    //           State: ""
    //       },
    //       {
    //           Name: "OHH REFLEX TESTING ACKNOWLEDGEMENT",
    //           FileName: "OHH_REFLEX_TESTING_ACKNOWLEDGEMENT",
    //           Payer: "",
    //           State: ""
    //       },

    //       {
    //           Name: "LARGO MEDICAL CENTER REFLEX TESTING ACKNOWLEDGEMENT",
    //           FileName: "LARGO_MEDICAL_CENTER_REFLEX_TESTING_ACKNOWLEDGEMENT",
    //           Payer: "",
    //           State: ""
    //       },
    //       {
    //           Name: "NORTHSIDE HOSPITAL REFLEX TESTING ACKNOWLEDGEMENT",
    //           FileName: "NORTHSIDE_HOSPITAL_REFLEX_TESTING_ACKNOWLEDGEMENT",
    //           Payer: "",
    //           State: ""
    //       },
    //       {
    //           Name: "ST. PETE GENERAL HOSPITAL REFLEX TESTING ACKNOWLEDGEMENT",
    //           FileName: "ST_PETE_GENERAL_HOSPITAL_REFLEX_TESTING_ACKNOWLEDGEMENT",
    //           Payer: "",
    //           State: ""
    //       },
    //       {
    //           Name: "NORTHSIDE HOSPITAL ACKNOWLEDGMENT CARD",
    //           FileName: "NORTHSIDE_HOSPITAL_ACKNOWLEDGMENT_CARD",
    //           Payer: "",
    //           State: ""
    //       },
    //       {
    //           Name: "NORTHSIDE HOSPITAL CODE OF CONDUCT AGREEMENT",
    //           FileName: "NORTHSIDE_HOSPITAL_CODE_OF_CONDUCT_AGREEMENT",
    //           Payer: "",
    //           State: ""
    //       },
    //       {
    //           Name: "NORTHSIDE HOSPITAL HAND HYGIENE",
    //           FileName: "NORTHSIDE_HOSPITAL_HAND_HYGIENE",
    //           Payer: "",
    //           State: ""
    //       },
    //       {
    //           Name: "LARGO MEDICAL CENTER MEDICAL STAFF COVERAGE AGREEMENT",
    //           FileName: "LARGO_MEDICAL_CENTER_MEDICAL_STAFF_COVERAGE_AGREEMENT",
    //           Payer: "",
    //           State: ""
    //       },
    //       {
    //           Name: "BAYFRONT-ALLIED PRE APPLICATION",
    //           FileName: "BAYFRONT-ALLIED_PRE_APPLICATION",
    //           Payer: "",
    //           State: ""
    //       },
    //       {
    //           Name: "BAYFRONT-PHYSICIAN PRE APPLICATION",
    //           FileName: "BAYFRONT-PHYSICIAN_PRE_APPLICATION",
    //           Payer: "",
    //           State: ""
    //       },
    //       {
    //           Name: "FLORIDA FINANCIAL RESPONSIBILITY FORM",
    //           FileName: "FLORIDA_FINANCIAL_RESPONSIBILITY_FORM",
    //           Payer: "",
    //           State: ""
    //       },
    //       {
    //           Name: "BCBS NC RELEASE ATTESTATION STATEMENT",
    //           FileName: "BCBS_NC_RELEASE_ATTESTATION_STATEMENT",
    //           Payer: "",
    //           State: ""
    //       },
    //       {
    //           Name: "FEDERAL COMMUNICATIONS COMMISSION ATTESTATION STATEMENT-Primecare",
    //           FileName: "FEDERAL_COMMUNICATIONS_COMMISSION_ATTESTATION_STATEMENT",
    //           Payer: "",
    //           State: ""
    //       },
    //       {
    //           Name: "FIRST CAROLINA CARE PROVIDER INFORMATION CHANGE FORM-Primecare",
    //           FileName: "FIRST_CAROLINA_CARE_PROVIDER_INFORMATION_CHANGE_FORM",
    //           Payer: "",
    //           State: ""
    //       }
    //       ,
    //       {
    //           Name: "NORTH CAROLINA COVENTRY UNIFORM CREDENTIALING APPLICATION-Primecare",
    //           FileName: "North_Carolina_Coventry_Uniform_Credentialing_Application",
    //           Payer: "",
    //           State: ""
    //       }
    //];

   

   
    $scope.tempPdfFileList = angular.copy($scope.pdfTemplateList);
    $scope.generatedPdfTemplateList = [];

    $scope.ViewTemplate = function (id, templateName) {
        $scope.pdfLoadingAjax = true;

        $scope.isError = false;
        $http.get(rootDir + '/Credentialing/PDFMapping/GetPDFMappingProfileData?profileId=' + id + '&templateName=' + templateName).

            success(function (data) {
               
                try {
                    $scope.pdfLoadingAjax = false;

                    var open_link = window.open('', '_blank');
                    open_link.location = '/Document/View?path=/ApplicationDocument/GeneratedTemplatePdf/' + JSON.parse(data);
                    $scope.tempObject.selectedFileName = "/ApplicationDocument/GeneratedTemplatePdf/" + JSON.parse(data);
                    $scope.generatedPdfTemplateList.push({ fileName: $scope.tempObject.seletedPDFName, filePath: $scope.tempObject.selectedFileName });
                } catch (e) {
                  
                }
                
                //$scope.tempObject.fname = JSON.parse(data);}) {

            });

    };

    $scope.AddNewDocument = function (licenseType) {

        $scope.FilePath = "";
        $scope.isError = false;
        $("#ATTACHmore").show();
        $("#SubmitDone").hide();
        $("#AddDone").hide();
        $scope.IsAddNewDocument = true;
        $scope.isAdd = true;
        $scope.isUpdate = false;
    };

    $scope.saveOtherDocument = function (Form_Div_Id, profileId) {
        var $form = $("#" + Form_Div_Id).find("form");
        ResetFormForValidation($form);
        $scope.isError = false;

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

                    try {
                        if (data.status == "true") {

                            $scope.IsAddNewDocument = false;
                            $("#ATTACHmore").hide();
                            $scope.SuccessMessage = "Document Updated Successfully !!!!!!!!!!!";
                            $scope.IsMessage = true;
                            $rootScope.$broadcast('UpdateDocument', data.otherDocument);
                            $timeout(function () {
                                $scope.IsMessage = false;
                            }, 5000);
                            $scope.TempObject = null;
                            $rootScope.visibilityControl = "";

                            messageAlertEngine.callAlertMessage("alertOtherDocumentSuccess", "Other Document saved successfully.", "success", true);
                        } else {
                            $scope.message = "Please Select the Document";
                            $scope.isError = true;
                        }
                    } catch (e) {
                     
                    }
                }
            });
        } else {

        }

    };
    $scope.datesplitter = function(date){
        returndate = "";
        if(date != null)
        {
            var newdate = date.split('T')[0].split('-');
            returndate = newdate[1] +"/"+ newdate[2] +"/"+ newdate[0];
        }
        return returndate;
    }

    $scope.closemaildiv = function ()
    {
        $("#CCOList").hide();
    }

    //===============================Package Generation start===============================


    //===============================Package Generation end=================================


    $scope.resetForm = function () {
        $scope.DocumentForm.$setPristine();
    };

    $scope.PSVStatusForDisplaying = false;


    $scope.credentialingFilterInfo = JSON.parse(credFilterInfo);

   

    var credentialLogResize = [];
    var credentialLogResizeCount = 0;
    for (var c = 0; c < $scope.credentialingFilterInfo.CredentialingLogs.length; c++) {
        if ($scope.credentialingFilterInfo.CredentialingLogs[c].Credentialing == 'Credentialing' || $scope.credentialingFilterInfo.CredentialingLogs[c].Credentialing == 'ReCredentialing') {
            credentialLogResize.push($scope.credentialingFilterInfo.CredentialingLogs[c]);
            credentialLogResizeCount = c;
            for (var k = 0; k < $scope.credentialingFilterInfo.CredentialingLogs[c].CredentialingActivityLogs.length; k++) {
                if ($scope.credentialingFilterInfo.CredentialingLogs[c].CredentialingActivityLogs[k].Activity=="PSV") {
                    $scope.PSVStatusForDisplaying = true;
                    break;
                }
            }
        }
    }
    if ($scope.credentialingFilterInfo.Profile.PersonalDetail.MiddleName != null)
    $scope.welcomelettername = $scope.credentialingFilterInfo.Profile.PersonalDetail.Salutation +" "+ $scope.credentialingFilterInfo.Profile.PersonalDetail.FirstName +" "+ $scope.credentialingFilterInfo.Profile.PersonalDetail.MiddleName +" "+ $scope.credentialingFilterInfo.Profile.PersonalDetail.LastName;
    else
    {
        $scope.welcomelettername = $scope.credentialingFilterInfo.Profile.PersonalDetail.Salutation + " " + $scope.credentialingFilterInfo.Profile.PersonalDetail.FirstName + " " + $scope.credentialingFilterInfo.Profile.PersonalDetail.LastName;
    }
    proname = $scope.welcomelettername;
    $scope.credentialingFilterInfo.CredentialingLogs.splice(credentialLogResizeCount, 1);
    for (var c = 0; c < $scope.credentialingFilterInfo.CredentialingLogs.length; c++) {

        credentialLogResize.push($scope.credentialingFilterInfo.CredentialingLogs[c]);

    }
    $scope.credentialingFilterInfo.CredentialingLogs = [];
    $scope.credentialingFilterInfo.CredentialingLogs = angular.copy(credentialLogResize);
    for (var c = 0; c < $scope.credentialingFilterInfo.CredentialingLogs.length; c++) {

        if ($scope.credentialingFilterInfo.CredentialingLogs[c].Credentialing == 'Dropped') {
            $scope.isDroped = true;
            break;
        }

    }
    $scope.fName = $scope.credentialingInfo.Profile.PersonalDetail.FirstName;
    
    $scope.tempObject = [];
    if ($scope.credentialingFilterInfo.CredentialingLogs[0].CredentialingAppointmentDetail != null) {
        $scope.tempObject1 = angular.copy($scope.credentialingFilterInfo.CredentialingLogs[0].CredentialingAppointmentDetail);
        $scope.tempObject1.WelcomeLetterPreparedDate = $scope.datesplitter($scope.tempObject1.WelcomeLetterPreparedDate);
        $scope.tempObject1.FirstName = $scope.credentialingFilterInfo.Profile.PersonalDetail.FirstName;
        $scope.tempObject1.MiddleName = $scope.credentialingFilterInfo.Profile.PersonalDetail.MiddleName;
        $scope.tempObject1.LastName = $scope.credentialingFilterInfo.Profile.PersonalDetail.LastName;
        $scope.tempObject1.Salutation = $scope.credentialingFilterInfo.Profile.PersonalDetail.Salutation;
       

        for (var i = 0; i < $scope.tempObject1.CredentialingSpecialityLists.length; i++) {
            for (var j = 0; j < $scope.credentialingInfo.Profile.SpecialtyDetails.length; j++) {
                if ($scope.tempObject1.CredentialingSpecialityLists[i].Name == $scope.credentialingInfo.Profile.SpecialtyDetails[j].Specialty.Name && $scope.credentialingInfo.Profile.SpecialtyDetails[j].Status == "Inactive") {
                    $scope.tempObject1.CredentialingSpecialityLists[i].Status = $scope.credentialingInfo.Profile.SpecialtyDetails[j].Status;
                    $scope.tempObject1.CredentialingSpecialityLists[i].StatusType = $scope.credentialingInfo.Profile.SpecialtyDetails[j].StatusType;
                }
            }
        }

        for (var j = 0; j < $scope.users.length; j++) {
            if ($scope.tempObject1.AssignedToCCOID == $scope.users[j].CDUserID) {
                $scope.tempObject1.AssignedToCCO = $scope.users[j].UserName
                }
            }



        for (var i = 0; i < $scope.tempObject1.CredentialingCoveringPhysicians.length; i++) {
            for (var j = 0; j < $scope.credentialingInfo.Profile.PracticeLocationDetails.length; j++) {
                for (var k = 0; k < $scope.credentialingInfo.Profile.PracticeLocationDetails[j].PracticeProviders.length; k++) {
                    if ($scope.tempObject1.CredentialingCoveringPhysicians[i].FirstName == $scope.credentialingInfo.Profile.PracticeLocationDetails[j].PracticeProviders[k].FirstName && $scope.tempObject1.CredentialingCoveringPhysicians[i].LastName == $scope.credentialingInfo.Profile.PracticeLocationDetails[j].PracticeProviders[k].LastName && $scope.credentialingInfo.Profile.PracticeLocationDetails[j].PracticeProviders[k].Status == "Inactive") {
                        $scope.tempObject1.CredentialingCoveringPhysicians[i].Status = $scope.credentialingInfo.Profile.PracticeLocationDetails[j].PracticeProviders[k].Status;
                        $scope.tempObject1.CredentialingCoveringPhysicians[i].StatusType = $scope.credentialingInfo.Profile.PracticeLocationDetails[j].PracticeProviders[k].StatusType;
                    }
                }
            }
        }

       
        if ($scope.tempObject1.CredentialingAppointmentSchedule != null) {

            $scope.tempObject1.CredentialingAppointmentSchedule.AppointmentDate = (new Date($scope.tempObject1.CredentialingAppointmentSchedule.AppointmentDate).getMonth() + 1) + "/" + new Date($scope.tempObject1.CredentialingAppointmentSchedule.AppointmentDate).getDate() + "/" + new Date($scope.tempObject1.CredentialingAppointmentSchedule.AppointmentDate).getFullYear();
        }
        if ($scope.tempObject1.CredentialingAppointmentResult != null) {

            $scope.tempObject1.CredentialingAppointmentResult.SignedDate = (new Date($scope.tempObject1.CredentialingAppointmentResult.SignedDate).getMonth() + 1) + "/" + new Date($scope.tempObject1.CredentialingAppointmentResult.SignedDate).getDate() + "/" + new Date($scope.tempObject1.CredentialingAppointmentResult.SignedDate).getFullYear();
        }
    }
    else {
        $scope.tempObject1 = {};
        //$scope.tempObject1.CredentialingAppointmentDetailID = null;
        $scope.tempObject1.FirstName = $scope.credentialingFilterInfo.Profile.PersonalDetail.FirstName;
        $scope.tempObject1.MiddleName = $scope.credentialingFilterInfo.Profile.PersonalDetail.MiddleName;
        $scope.tempObject1.LastName = $scope.credentialingFilterInfo.Profile.PersonalDetail.LastName;
        $scope.tempObject1.CredentialingSpecialityLists = [];
        $scope.tempObject1.CredentialingCoveringPhysicians = [];
    }
    $scope.tempObject1.Salutation = $scope.credentialingFilterInfo.Profile.PersonalDetail.Salutation;
    $scope.tempObject1.Specialties = [];
    for (var i = 0; i < $scope.credentialingInfo.Profile.SpecialtyDetails.length; i++) {
        if ($scope.credentialingInfo.Profile.SpecialtyDetails[i].Status != "Inactive") {
            $scope.tempObject1.Specialties.push($scope.credentialingInfo.Profile.SpecialtyDetails[i]);
        }
    }
    $scope.tempObject1.PracticeLocationDetails = [];
    for (var i = 0; i < $scope.credentialingInfo.Profile.PracticeLocationDetails.length; i++) {
        for (var j = 0; j < $scope.credentialingInfo.Profile.PracticeLocationDetails[i].PracticeProviders.length; j++) {
            if ($scope.credentialingInfo.Profile.PracticeLocationDetails[i].PracticeProviders[j].Status != "Inactive") {
                $scope.tempObject1.PracticeLocationDetails.push($scope.credentialingInfo.Profile.PracticeLocationDetails[i].PracticeProviders[j]);
            }
        }
    }
    if($scope.tempObject1.WelcomeLetterPreparedDate == null || $scope.tempObject1.WelcomeLetterPreparedDate =="")
    {
        $scope.tempObject1.WelcomeLetterPreparedDate = new Date();
    }
    //$scope.tempObject1.PracticeLocationDetails = $scope.credentialingInfo.Profile.PracticeLocationDetails;
   
    //if ($scope.ButtonCLickName == "1") {
    //    $('#summary2').addClass('active');
    //    $('#SUMMARY').addClass('active');
    //    $('#credentialing_action').removeClass('active');
    //    $('#credentialing_action1').removeClass('active');
    //    //$scope.sumryClass = "active";
    //    //$scope.cmClass = "";
    //} else 

    
    var flagforCred = 0;
    var LogData = {};
    if ($scope.credentialingFilterInfo != null) {
        for (var c = 0; c < $scope.credentialingFilterInfo.CredentialingLogs.length; c++) {
            if ($scope.credentialingFilterInfo.CredentialingLogs[c].Credentialing == "Credentialing" || $scope.credentialingFilterInfo.CredentialingLogs[c].Credentialing == "ReCredentialing") {
                LogData = $scope.credentialingFilterInfo.CredentialingLogs[c];
                for (var d = 0; d < $scope.credentialingFilterInfo.CredentialingLogs[c].CredentialingActivityLogs.length; d++) {
                    if ($scope.credentialingFilterInfo.CredentialingLogs[c].CredentialingActivityLogs[d].Activity == "Loading") {
                        flagforCred = 1;
                        break;
                    }
                }
            }

        }
    }

    if ($scope.ButtonCLickName == "2" && $scope.credentialingInfo.Plan.DelegatedType == "YES" && flagforCred == 0) {
        var flagforCredPSV = 0
        if ($scope.credentialingFilterInfo != null) {
            for (var c = 0; c < $scope.credentialingFilterInfo.CredentialingLogs.length; c++) {
                if ($scope.credentialingFilterInfo.CredentialingLogs[c].Credentialing == "Credentialing" || $scope.credentialingFilterInfo.CredentialingLogs[c].Credentialing == "ReCredentialing") {
                    for (var d = 0; d < $scope.credentialingFilterInfo.CredentialingLogs[c].CredentialingActivityLogs.length; d++) {
                        if ($scope.credentialingFilterInfo.CredentialingLogs[c].CredentialingActivityLogs[d].Activity == "PSV") {
                            flagforCredPSV = 1;
                            break;
                        }
                    }
                }

            }
        }
        if (flagforCredPSV == 1) {
            if (LogData.CredentialingAppointmentDetail != null) {
                if (LogData.CredentialingAppointmentDetail.CredentialingAppointmentResult != null) {
                    if (LogData.CredentialingAppointmentDetail.CredentialingAppointmentResult.ApprovalStatus == 'Rejected') {
                        $scope.ccmStatus = true;
                        $('#credentialing_action').addClass('active');
                        $('#credentialing_action1').addClass('active');
                        $('#summary2').removeClass('active');
                        $('#SUMMARY').removeClass('active');
                    }
                    if (LogData.CredentialingAppointmentDetail.CredentialingAppointmentResult.ApprovalStatus == 'Onhold') {
                        $scope.ccmStatus = true;
                        $('#credentialing_action').addClass('active');
                        $('#credentialing_action1').addClass('active');
                        $('#summary2').removeClass('active');
                        $('#SUMMARY').removeClass('active');
                    }
                }
                else {
                    $scope.ccmStatus = true;
                    $('#credentialing_action').addClass('active');
                    $('#credentialing_action1').addClass('active');
                    $('#summary2').removeClass('active');
                    $('#SUMMARY').removeClass('active');
                }
            }
            else {
                $scope.ccmStatus = true;
                $('#credentialing_action').addClass('active');
                $('#credentialing_action1').addClass('active');
                $('#summary2').removeClass('active');
                $('#SUMMARY').removeClass('active');
            }
        }


        //$scope.sumryClass = "";
        //$scope.cmClass = "active";
    }

    $scope.tempObject.StatusType = "Active";
    if ($scope.credentialingFilterInfo.CredentialingLogs[0].CredentialingAppointmentDetail != null) {
        var data1 = $scope.credentialingFilterInfo.CredentialingLogs[0].CredentialingAppointmentDetail.FileUploadPath;
    }
    $scope.DocName = function (data) {
        if (data != null) {

            return data.substring(data.lastIndexOf('\\') + 1);

        }
    }

    $scope.DocName1 = $scope.DocName(data1);
   

    $scope.ViewcheckList = function () {
        $scope.ViewCheckList = true;
        $scope.View = true;
    }
    $scope.ViewCheckList = true;
    $scope.Edit = function () {
        $scope.ViewCheckList = false;
        $scope.View = false;
    }
    $scope.oldTempObject1 = angular.copy($scope.tempObject1);
    $scope.Cancel = function () {
        $scope.tempObject1 = angular.copy($scope.oldTempObject1);
        $scope.ViewCheckList = true;
        $scope.View = true;
    }


    $scope.AddSpacesInWords = function (input) {
        if (input == null)
        {
            input = "";
        }
        var newString = "";
        var wasUpper = false;
        if(input!=null)
        for (var i = 0; i < input.length; i++) {
            if (!wasUpper && input[i] == input.toUpperCase()[i]) {
                newString = newString + " ";
                wasUpper = true;
            }
            else {
                wasUpper = false;
            }
            newString = newString + input[i];
        }
        return newString;
    };

    $scope.cancelAdd = function () {
        $scope.IsAddNewDocument = false;
        $scope.isError = false;
    };
    $scope.IsMessage = false;
    //================http ==============
    $scope.checkForHttp = function (value) {
        if (value.indexOf('http') > -1) {
            value = value;
        } else {
            value = 'http://' + value;
        }
        var open_link = window.open('', '_blank');
        open_link.location = value;
    }


   

    //if()
    //{

    //}

    //=========================GetAllProfileVerificationParameter======================
    $scope.GetProfileVerificationParameter = function () {

        $http.get(rootDir + '/Profile/MasterData/GetAllProfileVerificationParameter').
        success(function (data, status, headers, config) {

            try {

                for (var i = 0; i < data.length; i++) {
                    if (data[i].Code == 'SL') {
                        $scope.StateLicenseParameterID = data[i].ProfileVerificationParameterId;
                    }
                    if (data[i].Code == 'BC') {
                        $scope.BoardCertificationParameterID = data[i].ProfileVerificationParameterId;
                    }
                    if (data[i].Code == 'DEA') {
                        $scope.DEAParameterID = data[i].ProfileVerificationParameterId;
                    }
                    if (data[i].Code == 'CDS') {
                        $scope.CDSParameterID = data[i].ProfileVerificationParameterId;
                    }
                    if (data[i].Code == 'NPDB') {
                        $scope.NPDBParameterID = data[i].ProfileVerificationParameterId;
                    }
                    if (data[i].Code == 'MOPT') {
                        $scope.MOPTParameterID = data[i].ProfileVerificationParameterId;
                    }
                    if (data[i].Code == 'OIG') {
                        $scope.OIGParameterID = data[i].ProfileVerificationParameterId;
                    }
                }
            } catch (e) {
            
            }

        }).
        error(function (data, status, headers, config) {
            // called asynchronously if an error occurs
            // or server returns response with an error status.
        });
    };


    $scope.getProviderTypeValue = function (type) {
        $scope.tempObject.AppointmentProviderType = type;
    };

    $scope.getBoardCertificationValue = function (type) {
        $scope.tempObject.BoardCertification = type;
    };

    $scope.getHospitalPrivilegesValue = function (type) {
        $scope.tempObject.HospitalPrivileges = type;
    };

    $scope.getGapsInPracticeValue = function (type) {
        $scope.tempObject.GapsInPractice = type;
    };

    $scope.getCleanLicenseValue = function (type) {
        $scope.tempObject.CleanLicense = type;
    };

    $scope.getNPDBIssuesValue = function (type) {
        $scope.tempObject.NPDBIssues = type;
    };

    $scope.getMalpracticeIssuesValue = function (type) {
        $scope.tempObject.MalpracticeIssues = type;
    };

    $scope.getAnyOtherIssueValue = function (type) {
        $scope.tempObject.AnyOtherIssue = type;
    };

    $scope.getProviderLevelValue = function (type) {
        $scope.tempObject.ProviderLevel = type;
    };

    $scope.SpecialityCCN = [];

    $scope.assignSpecialties = function () {
        for (index = 0; index < $scope.tempObject1.Specialties.length; index++) {
            var count = 0;
            if ($scope.credentialingFilterInfo.CredentialingLogs[0].CredentialingAppointmentDetail != null) {
                for (var i = 0; i < $scope.tempObject1.CredentialingSpecialityLists.length; i++) {
                    if ($scope.tempObject1.CredentialingSpecialityLists[i].Name == $scope.tempObject1.Specialties[index].Specialty.Name) {
                        count++;
                        break;
                    }
                }
            }
            if (count == 0) {
                $scope.SpecialityCCN.push($scope.tempObject1.Specialties[index].Specialty);
            }
        }
    }

    $scope.assignSpecialties();
    //$scope.SpecialityCCN = [
    //    { SpecialityID: 1, Name: 'Allergy & Immunology', Status: 'Active' },
    //    { SpecialityID: 2, Name: 'Dermatology', Status: 'Active' },
    //    { SpecialityID: 3, Name: 'Colon & Rectal Surgery', Status: 'Active' },
    //    { SpecialityID: 4, Name: 'Family Practice', Status: 'Active' }
    //];
    $scope.tempObject.seletedPDFName = null;
    $scope.addIntoTypeDropDown = function (type) {
        $scope.tempObject.Speciality = type.Name;
        $scope.tempObject.SpecialityID = type.SpecialtyID;
        $("#ForType").hide();
    }
    $scope.addIntoTypePdfDropDown = function (type) {
        $scope.tempObject.seletedPDFName = type.Name;
        $scope.tempObject.selectedFileName = type.FileName;
        $scope.pdfTemplateList = angular.copy($scope.tempPdfFileList);
        $scope.IsFileSelected = false;
        $("#ForType").hide();
    }

    $scope.searchCumDropDown = function () {
        
        $scope.pdfTemplateList = angular.copy($scope.tempPdfFileList);   
        $("#ForType").show();
    };

    $scope.CoveringPhysicians = [];

    $scope.assignCoveringColleagues = function () {
        for (var i = 0; i < $scope.tempObject1.PracticeLocationDetails.length; i++) {
            if ($scope.tempObject1.PracticeLocationDetails[i].Practice == 'CoveringColleague') {
                var count = 0;
                if ($scope.credentialingFilterInfo.CredentialingLogs[0].CredentialingAppointmentDetail != null) {
                    for (var j = 0; j < $scope.tempObject1.CredentialingCoveringPhysicians.length; j++) {
                        if ($scope.tempObject1.CredentialingCoveringPhysicians[j].FirstName == $scope.tempObject1.PracticeLocationDetails[i].FirstName && $scope.tempObject1.CredentialingCoveringPhysicians[j].LastName == $scope.tempObject1.PracticeLocationDetails[i].LastName) {
                            count++;
                            break;
                        }
                    }
                }
                if (count == 0) {
                    $scope.CoveringPhysicians.push($scope.tempObject1.PracticeLocationDetails[i]);
                }
            }
        }
    }
    $scope.viewc = true;

    $scope.assignCoveringColleagues();
    

    //$scope.CoveringPhysicians = [
    //    {CoveringPhysiciansID: 1, Name:'Dr. Maria G.', Status:'Active'},
    //    { CoveringPhysiciansID: 1, Name: 'Dr. Marry Grain', Status: 'Active' },
    //    { CoveringPhysiciansID: 1, Name: 'Dr. Kartik', Status: 'Active' },
    //];
    $scope.fixValue = false;
    for (var i = 0; i < $scope.tempObject1.CredentialingSpecialityLists.length; i++) {
        if ($scope.tempObject1.CredentialingSpecialityLists[i].StatusType == 1) {
            $scope.fixValue = true;
            break;
        }
    }
    $scope.fixValue1 = false;
    for (var i = 0; i < $scope.tempObject1.CredentialingCoveringPhysicians.length; i++) {
        if ($scope.tempObject1.CredentialingCoveringPhysicians[i].StatusType == 1) {
            $scope.fixValue1 = true;
            break;
        }
    }
    $scope.showContryCodeDiv = function (div_Id) {
        $("#" + div_Id).show();
    };
    $scope.showSpeciality = function (div_Id) {
        $("#" + div_Id).show();
    };
    $scope.SelectSpecialityType = function (specialitytype) {
        $scope.fixValue = true;
        $scope.tempObject1.CredentialingSpecialityLists.push({
            CredentialingSpecialityListID: specialitytype.SpecialtyID,
            //SpecialtyID: specialitytype.SpecialtyID,
            Name: specialitytype.Name,
            StatusType: specialitytype.StatusType,
        });
        $scope.SpecialityCCN.splice($scope.SpecialityCCN.indexOf(specialitytype), 1);
        $scope.SpecialityType = "";
    };

    $scope.RemoveSpecialityType = function (speciality) {
        $scope.tempObject1.CredentialingSpecialityLists.splice($scope.tempObject1.CredentialingSpecialityLists.indexOf(speciality), 1);
        var count = 0;
        for (var i = 0; i < $scope.tempObject1.CredentialingSpecialityLists.length; i++) {
            if ($scope.tempObject1.CredentialingSpecialityLists[i].StatusType == 1) {
                count++;
            }
        }
        if (count == 0) {
            $scope.fixValue = false;
        }
        $scope.SpecialityCCN.push(speciality);
    };

    $scope.SelectCoveringPhysiciansType = function (coveringPhysicianstype) {
        $scope.fixValue1 = true;
        $scope.tempObject1.CredentialingCoveringPhysicians.push({
            CredentialingCoveringPhysicianID: coveringPhysicianstype.PracticeProviderID,
            FirstName: coveringPhysicianstype.FirstName,
            LastName: coveringPhysicianstype.LastName,
            StatusType: coveringPhysicianstype.StatusType,
        });
        $scope.CoveringPhysicians.splice($scope.CoveringPhysicians.indexOf(coveringPhysicianstype), 1);
        $scope.CoveringPhysiciansType = "";
    };

    $scope.RemoveCoveringPhysiciansType1 = function (coveringPhysicians) {
        $scope.tempObject1.CredentialingCoveringPhysicians.splice($scope.tempObject1.CredentialingCoveringPhysicians.indexOf(coveringPhysicians), 1);
        var count = 0;
        for (var i = 0; i < $scope.tempObject1.CredentialingCoveringPhysicians.length; i++) {
            if ($scope.tempObject1.CredentialingCoveringPhysicians[i].StatusType == 1) {
                count++;
            }
        }
        if (count == 0) {
            $scope.fixValue1 = false;
        }
        $scope.CoveringPhysicians.push(coveringPhysicians);
    };
    $scope.temp = false;
    if ($scope.credentialingFilterInfo.CredentialingLogs[0].CredentialingAppointmentDetail != null) {
        $scope.temp = true;
    }



    $scope.View = true;
    $scope.credentialingCheckList = function (Form_Id) {
        ResetFormForValidation($("#" + Form_Id));
        if ($("#" + Form_Id).valid()) {

            var $form = $("#" + Form_Id)[0];

            $.ajax({
                url: rootDir + '/Credentialing/CnD/CCMAction?credentialingInfoID=' + credId,
                type: 'POST',
                data: new FormData($form),
                async: false,
                cache: false,
                contentType: false,
                processData: false,
                success: function (data) {
                    try {
                        if (data.status == "true") {
                            messageAlertEngine.callAlertMessage('ProviderCheckList', "Provider CheckList Information Updated Successfully. !!!!", "success", true);
                            $(window).scrollTop(0);
                            $scope.temp = true;
                            if (data.data.WelcomeLetterPreparedDate == null || data.data.WelcomeLetterPreparedDate =="") {
                                data.data.WelcomeLetterPreparedDate = new Date();
                            } else {
                                data.data.WelcomeLetterPreparedDate = $scope.ConvertDateFormat(data.data.WelcomeLetterPreparedDate);
                            }
                            data.data.WelcomeLetterMailedDate = $scope.ConvertDateFormat(data.data.WelcomeLetterMailedDate);
                            $scope.tempObject1 = angular.copy(data.data);
                            for (var j = 0; j < $scope.users.length; j++) {
                                if ($scope.tempObject1.AssignedToCCOID == $scope.users[j].CDUserID) {
                                    $scope.tempObject1.AssignedToCCO = $scope.users[j].UserName
                                }
                            }
                            $scope.oldTempObject1 = angular.copy(data.data);
                            $scope.ViewCheckList = true;
                            $scope.viewc = false;
                            $scope.View = true;
                        }
                    } catch (e) {
                     
                    }
                }
            });

        }
    }

    $scope.setFiles = function (element) {
        $scope.$apply(function (scope) {
            if (element.files[0]) {
                $scope.tempObject.FileUploadPath = element.files[0];
            } else {
                $scope.tempObject.FileUploadPath = {};
            }
        });
    };

    $scope.removeFile = function (FileId) {
        if (FileId == "ContractInfo_CVFile") {
            $scope.tempObject.FileUploadPath = {};
        }
    };

    $scope.setAppointment = function (obj) {
        
        var obj1 = {
            FirstName: obj.FirstName,
            MiddleName: obj.MiddleName,
            LastName: obj.LastName,
            AppointmentProviderType: obj.ProviderType,
            SpecialtyID: obj.SpecialityID,
            HospitalPrivilegesYesNoOption: obj.HospitalPrivileges,
            RemarksForHospitalPrivileges: obj.remarkForHospitalPrivileges,
            GapsInPracticeYesNoOption: obj.GapsInPractice,
            RemarksForGapsInPractice: obj.remarkForGapsInPractice,
            CleanLicenseYesNoOption: obj.CleanLicense,
            RemarksForCleanLicense: obj.remarkForCleanLicense,
            NPDBIssueYesNoOption: obj.NPDBIssues,
            RemarksForNPDBIssue: obj.remarkForNPDBIssues,
            MalpracticeIssueYesNoOption: obj.MalpracticeIssues,
            RemarksForMalpracticeIssue: obj.remarkForMalpracticeIssues,
            YearsInPractice: obj.YearsInPractice,
            SiteVisitRequiredYesNoOption: obj.AnyOtherIssue,
            RemarksForSiteVisitRequired: obj.remarkForAnyOtherIssue,
            RecommendedCredentialingLevel: obj.ProviderLevel,
            RemarksForBoardCertification: obj.remarkForBoardCertification,
            BoardCertifiedYesNoOption: obj.BoardCertification,
            StatusType: obj.StatusType,
            CredentialingCoveringPhysicians: angular.copy(obj.CoveringPhysicians1)
        };
        $http.post(rootDir + '/Credentialing/CnD/CCMAction?credentialingInfoID=' + credId, obj1).success(function (data, status, headers, config) {
            //----------- success message -----------

            try {

                $window.location = '/Credentialing/CnD/CredentialingAppointment';

            } catch (e) {
              
            }
        }).
            error(function (data, status, headers, config) {

            });

    };


    $scope.toggleDiv = function (divId, className) {
        $('.' + className).slideUp();
        $('#' + divId).slideDown();
    };

    //============date=================================
    var monthNames = ["Jan", "Feb", "Mar", "Apr", "May", "Jun",
"Jul", "Aug", "Sep", "Oct", "Nov", "Dec"
    ];
    $scope.ConvertDateFormat = function (value) {
        var shortDate = null;
        if (value) {
            var regex = /-?\d+/;
            var matches = regex.exec(value);
            var dt = new Date(parseInt(matches[0]));
            var month = dt.getMonth() + 1;
            var monthString = month > 9 ? month : '0' + month;
            var monthName = monthNames[month];
            var day = dt.getDate();
            var dayString = day > 9 ? day : '0' + day;
            var year = dt.getFullYear();
            shortDate = monthString + '/' + dayString + '/' + year;
            //shortDate = dayString + 'th ' + monthName + ',' + year;
        }
        return shortDate;
    };
    $scope.reformatDate = function (dateStr) {

        if (dateStr != null) {
            dArr1 = dateStr.split("T");//2015-06-26T00:00:00
            dArr2 = dArr1[0].split("-");

            // return dArr2[2] + 'th ' + monthNames[dArr2[1] - 1] + ',' + dArr2[0];
            return dArr2[1] + '/' + dArr2[2] + '/' + dArr2[0];
        }

    }

    $scope.ConvertDateFormat1 = function (value) {
       
        var returnValue = value;
        try {
            if (value.indexOf("/Date(") == 0) {
                returnValue = new Date(parseInt(value.replace("/Date(", "").replace(")/", ""), 10));
            }
            return returnValue;
        } catch (e) {
            return returnValue;
        }
        return returnValue;
    };

    //========================PSV starts======================================
    //$scope.LicenseList = [{ title: 'State License for Practicing State', tabId: 'stateLicenseInfo', tabPanelId: 'perstateLicenseInfo' },
    //       { title: 'Board Certifications for Physicians', tabId: 'boardCertificationInfo', tabPanelId: 'perboardCertificationInfo' },
    //       { title: 'DEA', tabId: 'DEALicenseInfo', tabPanelId: 'perDEALicenseInfo' },
    //       { title: 'CDS', tabId: 'CDSLicenseInfo', tabPanelId: 'perCDSLicenseInfo' },
    //       { title: 'NPDB', tabId: 'NPDBLicenseInfo', tabPanelId: 'perNPDBLicenseInfo' },
    //       { title: 'Medicare OPT', tabId: 'medicareInfo', tabPanelId: 'permedicareInfo' },
    //       { title: 'OIG', tabId: 'OIGInfo', tabPanelId: 'perOIGInfo' }, ];

    //$scope.stateLicenses = { tabId: 'stateLicenseInfo', info: [{ state: 'Florida', licenseNumber: 'ME71088', issueDate: '04/03/2015', expirationDate: '03/03/2015', status: 'Valid', source: 'Source1', remarks: 'required', verifiedDocument: '/Content/Document/DocPreview.pdf' }, { state: 'USA', licenseNumber: 'US71099', issueDate: '04/03/2015', expirationDate: '04/03/2015', status: '', source: '', remarks: '', verifiedDocument: '/Content/Document/DocPreview.pdf' }] };
    //$scope.BoradCertifications = { tabId: 'boardCertificationInfo', info: [{ specialityBoardName: 'American Board of Internal Medicine', certificationStatus: 'Active', certificationDate: '16/12/2012', expirationDate: '04/03/2015', status: '', source: '', remarks: '', verifiedDocument: '' }, { specialityBoardName: 'American Board of External Medicine', certificationStatus: 'Active', certificationDate: '20/12/2012', expirationDate: '04/04/2015', status: '', source: '', remarks: '', verifiedDocument: '' }] };
    //$scope.DEAs = { tabId: 'DEALicenseInfo', info: [{ licenseNumber: '24EC077', issueDate: '04/03/2015', expirationDate: '03/03/2015', status: '', source: '', remarks: '', verifiedDocument: '' }, { licenseNumber: '24EC078', issueDate: '04/03/2015', expirationDate: '04/03/2015', status: '', source: '', remarks: '', verifiedDocument: '' }, { licenseNumber: '24EC079', issueDate: '04/03/2015', expirationDate: '03/05/2015', status: '', source: '', remarks: '', verifiedDocument: '' }] };
    //$scope.CDSs = { tabId: 'CDSLicenseInfo', info: [{ licenseNumber: '24EC078', issueDate: '04/03/2015', expirationDate: '04/03/2015', status: '', source: '', remarks: '', verifiedDocument: '' }, { licenseNumber: '24EC072', issueDate: '04/03/2015', expirationDate: '04/03/2015', status: '', source: '', remarks: '', verifiedDocument: '' }] };
    //$scope.NPDBs = { tabId: 'NPDBLicenseInfo', info: [] };
    //$scope.Medicares = { tabId: 'medicareInfo', info: [] };
    //$scope.OIGs = { tabId: 'OIGInfo', info: [] };


    $scope.FormattedData = [];

    $scope.loadData = function (id) {

        $http.get(rootDir + '/Credentialing/Verification/GetPSVReport?credinfoId=' + id).
        success(function (data, status, headers, config) {
            try {

                if (data.status == "true") {
                    data.psvReport = JSON.parse(data.psvReport);
                    $scope.showPsvError = false;
                    $scope.FormatData(data.psvReport);
                }
                else {
                    $scope.showPsvError = true;
                    //messageAlertEngine.callAlertMessage("psvReportError", data.status, "danger", true);
                }

            } catch (e) {
                
            }

        }).
        error(function (data, status, headers, config) {
            messageAlertEngine.callAlertMessage("psvReportError", "Sorry Unable To get PSV Report !!!!", "danger", true);
        });
    };

    //--------------------data format------------------------
    $scope.FormatData = function (data) {

        var formattedData = [];
        for (var i in data) {
            var VerificationData = new Object();
            if (data[i].VerificationData != null)
                VerificationData = jQuery.parseJSON(data[i].VerificationData);
            var VerificationDate = $scope.ConvertDateFormat1(data[i].VerificationDate)

            formattedData.push({ Id: data[i].ProfileVerificationParameterId, info: { ProfileVerficationParameterObj: data[i].ProfileVerificationParameter, VerificationResultObj: data[i].VerificationResult, VerificationData: VerificationData, VerificationDate: VerificationDate } });
            //formattedData.push({ Id: data[i].ProfileVerificationParameterId, info: { ProfileVerficationParameterObj: data[i].ProfileVerificationParameter, VerificationResultObj: data[i].VerificationResult, VerificationData: data[i].VerificationData } });
        }

        var UniqueIds = [];
        UniqueIds.push(formattedData[0].Id);
        for (var i = 1; i < formattedData.length; i++) {

            var CurrObj = formattedData[i];
            var flag = 0;
            for (var j = 0; j < UniqueIds.length; j++) {
                if (CurrObj.Id == UniqueIds[j]) {
                    flag = 1;
                }
            }
            if (flag == 0) {
                UniqueIds.push(CurrObj.Id);
            }
        }


        for (var i = 0; i < UniqueIds.length; i++) {
            var info = [];
            for (var j = 0; j < formattedData.length; j++) {
                if (UniqueIds[i] == formattedData[j].Id) {
                    info.push(formattedData[j].info);
                }

            }
            $scope.FormattedData.push({ Id: UniqueIds[i], Info: info });
        }
        var sequenceData = [];
        var sequenceIds = [1, 2, 3, 4, 5, 6, 7];
        for (var i = 0; i < sequenceIds.length; i++) {
            for (var j = 0; j < $scope.FormattedData.length; j++) {
                if (sequenceIds[i] == $scope.FormattedData[j].Id) {
                    sequenceData.push($scope.FormattedData[j]);
                }
            }
        }

        $scope.FormattedData = sequenceData;
    };

    //=======================PSV ends=========================================

    $scope.profileData = function (id) {
        $http({
            method: "POST",
            url: rootDir + "/Credentialing/CnD/GetProfile?id=" + id,

        }).success(function (resultData) {
            
        }).error(function () { $scope.loadingAjax = false; $scope.error_message = "An Error occurred !! Please Try Again !!"; })
    }



    $scope.formPath = "";
    $scope.formTypeCheckbox = [{ FormName: 'CAQH', TemplateName: null, IsChecked: false, FileGenerationStatus: false, Pdfpath: null }, { FormName: 'Profile Access', TemplateName: 'A2HC Provider Profile for Wellcare2 - BLANK_new.pdf', IsChecked: false, FileGenerationStatus: false, Pdfpath: null }, { FormName: 'Profile Access 2', TemplateName: 'AHC Provider Profile for Wellcare - BLANK_new.pdf', IsChecked: false, FileGenerationStatus: false, Pdfpath: null }, { FormName: 'Template 2', TemplateName: null, IsChecked: false, FileGenerationStatus: false, Pdfpath: null }];
    $scope.selectedFormType = [];


    $scope.showPDF = function (pdfPath) {

        $scope.generatePDF = true;
        $scope.pdfSrc = pdfPath;
    };

    var GoToApplicationRepository = function (profileId, templateName, index) {
        $http.post(rootDir + '/Credentialing/CnD/ApplicationRepository?profileId=' + profileId + '&template=' + templateName).
        success(function (data, status, headers, config) {

            try {
                if (data.status == "true") {
                    $scope.selectedFormType[index].FileGenerationStatus = true;

                    $scope.formPath = angular.copy(data.path);
                    //$scope.pdfPath = $sce.trustAsResourceUrl("../../GeneratedForm/" + data.path);
                    //$scope.pdfPath = "/Document/View?path=/GeneratedForm/" + data.path;
                    $scope.selectedFormType[index].Pdfpath = "/Document/View?path=/GeneratedForm/" + data.path;

                    //$scope.pdfPath = data.path;
                    //$scope.generatePDF = true;
                    //$scope.addRepo = false;
                    $scope.progressbar = false;

                }
            } catch (e) {
               
            }

        }).
        error(function (data, status, headers, config) {
            //alert('Error');
        });
    };

    $scope.generateForm = function () {

        var profileId = 1;
        $scope.progressbar = true;
        //var templateName = $scope.template;
        //$scope.pdfPath = "Prakash_06-18-2015.pdf";
        //$scope.generatePDF = true;
        //$scope.addRepo = false;

        for (var i = 0; i < $scope.formTypeCheckbox.length; i++) {
            if ($scope.formTypeCheckbox[i].IsChecked == true) {
                $scope.selectedFormType.push($scope.formTypeCheckbox[i]);

            }
        }

        for (var i = 0; i < $scope.selectedFormType.length; i++) {
            if ($scope.selectedFormType[i].IsChecked == true) {
                GoToApplicationRepository(profileId, $scope.selectedFormType[i].TemplateName, i);
            }
        }


    }

    $scope.saveForm = function () {
        var path = $scope.formPath;
        var profileId = 1;
        $http.post(rootDir + '/Credentialing/CnD/AddApplication?profileId=' + profileId + '&path=' + path).
        success(function (data, status, headers, config) {
            try {
                if (data.status == "true") {

                    $scope.generatePDF = false;
                    $scope.addRepo = true;

                }
            } catch (e) {
            
            }

        }).
        error(function (data, status, headers, config) {

        });
    }

    $scope.showForm = function () {
        $scope.generatePDF = true;
    }

    $scope.setCreLid = function (id) {
        sessionStorage.setItem('CreListId', id);
    }
    jQuery.grep($scope.credentialingInfo.CredentialingLogs, function (ele) {
        if (ele.Credentialing = "Credentialing") {
            jQuery.grep(ele.CredentialingActivityLogs, function (log) { $scope.ViewOnlyMode = (log.Activity == "Closure" && log.ActivityStatus == "Completed") ? false : true; });
        }
    });
    sessionStorage.setItem('ViewOnlyMode', $scope.ViewOnlyMode);
    $scope.SubmitSPA = function (credId) {
        $http.post(rootDir + '/Credentialing/CnD/SubmitSPA?CredentialingInfoID=' + credId).
       success(function (data, status, headers, config) {
           try {
               $scope.ViewOnlyMode = data.status == 'true' ? false : true;
               sessionStorage.setItem('ViewOnlyMode', $scope.ViewOnlyMode);
               var statusfortimeline = data.status;
               $scope.$broadcast("event", statusfortimeline);
               var completedBy = "";
               for (var j = 0; j < $scope.users.length; j++) {
                   if ($scope.users[j].CDUserID == data.cdUserId) {
                       completedBy = $scope.users[j].FullName;
                   }
               }
               if ($rootScope.isrecredentialing == "Credentialing") {                  
                   var completedDate = $rootScope.changeDateTime(new Date());
                   try {
                       var tempactivity = {
                           Activity: $rootScope.providerfullName + "  Credentialing Process Completed for " + $rootScope.planNameUniv,
                           ActivityByName: completedBy,
                           LastModifiedDate: completedDate
                       };
                       $rootScope.timelineActivity.unshift(tempactivity);
                       $rootScope.timelineActivity = $rootScope.timelineActivity.unique();
                   } catch (e) {

                   }
               }
              else if ($rootScope.isrecredentialing == "ReCredentialing") {
                   var completedDate = $rootScope.changeDateTime(new Date());
                   try {
                       var tempactivity = {
                           Activity: $rootScope.providerfullName +" "+" Re-Credentialing Process Completed for " + $rootScope.planNameUniv,
                           ActivityByName: completedBy,
                           LastModifiedDate: completedDate
                       };
                       $rootScope.timelineActivity.unshift(tempactivity);
                       $rootScope.timelineActivity = $rootScope.timelineActivity.unique();
                   } catch (e) {

                   }
               }
               $timeout(function () { $("#summary2").find('a').trigger('click'); });
           } catch (e) {
              
           }
       }).
       error(function (data, status, headers, config) {

       });
    }

    $scope.setCredinfoId = function (credInfoId) {

        sessionStorage.setItem('CredInfoId', credInfoId);
    }

});




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
}]);
Cred_SPA_App.run(['$rootScope', function ($rootScope) {
    $rootScope.isrecredentialing = "";
}])
$(document).ready(function () {
    $(".ProviderTypeSelectAutoList").hide();
    $(".ProviderTypeSelectAutoList1").hide();
});
$(document).click(function (event) {

    if (!$(event.target).hasClass("form-control") && $(event.target).parents(".ProviderTypeSelectAutoList").length === 0) {
        $(".ProviderTypeSelectAutoList").hide();
    }
    if (!$(event.target).hasClass("form-control") && $(event.target).parents(".ProviderTypeSelectAutoList1").length === 0) {
        $(".ProviderTypeSelectAutoList1").hide();
    }
});
function ResetFormForValidation(form) {
    form.removeData('validator');
    form.removeData('unobtrusiveValidation');
    $.validator.unobtrusive.parse(form);
};
function setFileNameWith(file) {
    $(file).parent().parent().find(".jancyFileWrapTexts").find("span").width($(file).parent().parent().width() - 197);
};
Cred_SPA_App.config(function ($datepickerProvider) {

    angular.extend($datepickerProvider.defaults, {
        startDate: 'today',
        autoclose: true,
        useNative: true
    });

})

