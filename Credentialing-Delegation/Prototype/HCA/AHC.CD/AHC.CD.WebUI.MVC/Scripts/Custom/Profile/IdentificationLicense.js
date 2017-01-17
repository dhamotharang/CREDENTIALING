
//=========================== Controller declaration ==========================
profileApp.controller('identificationLicenseController', function ($scope, $http, dynamicFormGenerateService) {

    
    
    $scope.StateLicenses = [{
        LicenseNumber: "ME71088",
        StateLicenseType: "MD",
        StateLicenseStatus: "Active",
        IssuingState: "Florida",
        PracticeState: "Florida",
        PracticingState: "Yes",
        IssueDate:"",
        ExpiryDate:"01-31-2015",
            //new Date(2015, 1, 31),
        CurrentIssueDate:"11-16-2012",
            //new Date(2012, 10, 16),
        LicenseInGoodStanding: "Yes",
        StateLicenseDocumentPath: "/Content/Document/SINGH - LICENSE.pdf"
  
    }, {
        LicenseNumber: "MM29257/10494 N",
        StateLicenseType: "ME",
        StateLicenseStatus: "Active",
        IssuingState: "Florida",
        PracticeState: "Florida",
        PracticingState: "Yes",
        IssueDate: "",
        ExpiryDate: "08-31-2015",//new Date(2015, 8, 31),
        CurrentIssueDate: "05-22-2013",//new Date(2013, 5, 22),
        LicenseInGoodStanding: "Yes",
        StateLicenseDocumentPath: ""
    }, {
        LicenseNumber: "MM29304-7625 Cit",
        StateLicenseType: "ME",
        StateLicenseStatus: "Active",
        IssuingState: "Florida",
        PracticeState: "Florida",
        PracticingState: "Yes",
        IssueDate: "",
        ExpiryDate: "08-31-2015",//new Date(2015, 8, 31),
        CurrentIssueDate: "06-05-2012",//new Date(2012, 6, 5),
        LicenseInGoodStanding: "Yes",
        StateLicenseDocumentPath: ""
    }, {
        LicenseNumber: "MM26123",
        StateLicenseType: "ME",
        StateLicenseStatus: "Active",
        IssuingState: "Florida",
        PracticeState: "Florida",
        PracticingState: "Yes",
        IssueDate: "",
        ExpiryDate: "08-31-2015",//new Date(2015, 8, 31),
        CurrentIssueDate: "12-15-2010",//new Date(2010, 12, 15),
        LicenseInGoodStanding: "Yes",
        StateLicenseDocumentPath: ""
}];
    
    //=============== State License Conditions ==================
    $scope.editShowStateLicense = false;
    $scope.newShowStateLicense = false;
    $scope.submitButtonText = "Add";
    $scope.IndexValue = 0;
    $scope.viewStateLicense = false;
    //====================== State License ===================

 
    $scope.addStateLicense = function () {
        $scope.newShowStateLicense = true;
        $scope.viewStateLicense = false;
        $scope.submitButtonText = "Add";
        $scope.stateLicense = {};
        ResetStateLicenseForm();
    };

    $scope.editStateLicense = function (index, stateLicense) {
        $scope.viewStateLicense = false;
        $scope.newShowStateLicense = false;
        $scope.editShowStateLicense = true;
        $scope.submitButtonText = "Update";
        $scope.stateLicense = stateLicense;
        $scope.IndexValue = index;
    };

    $scope.StateControlledSubstanceRegistrationAvail="N"
    $scope.viewStateLicenseInfo = function (index, StateLicense) {
        $scope.editShowStateLicense = false;
        $scope.viewStateLicense = true;
        $scope.stateLicense = StateLicense;
        $scope.IndexValue = index;
    };

    $scope.cancelStateLicense = function (condition) {
        setStateLicenseCancelParameters();
    };

    $scope.saveStateLicense = function (stateLicense) {

        console.log(stateLicense);

        var validationStatus;
        var url;

        if ($scope.newShowStateLicense) {
            //Add Details - Denote the URL
            validationStatus = $('#newShowStateLicenseDiv').find('form').valid();
            url = "/Profile/IdentificationAndLicense/AddStateLicense?profileId=1";
        }
        else if ($scope.editShowStateLicense) {
            //Update Details - Denote the URL
            validationStatus = $('#stateLicenseEditDiv' + $scope.IndexValue).find('form').valid();
            url = "/Profile/IdentificationAndLicense/UpdateStateLicense?profileId=1";
        }

        console.log(stateLicense);

        if (validationStatus) {
            // Simple POST request example (passing data) :
            $http.post(url, stateLicense).
              success(function (data, status, headers, config) {
                  alert("Success");
                  setStateLicenseCancelParameters();
              }).
              error(function (data, status, headers, config) {
                  alert("Error");
              });
        }
    };

    function setStateLicenseCancelParameters() {
        $scope.viewStateLicense = false;
        $scope.editShowStateLicense = false;
        $scope.newShowStateLicense = false;
        $scope.stateLicense = {};
        $scope.IndexValue = 0;
    }

    function ResetStateLicenseForm() {
        $('#newShowStateLicenseDiv').find('.stateLicenseForm')[0].reset();
        $('#newShowStateLicenseDiv').find('span').html('');
    }

    //****************************************Federal DEA Info********************************************


    $scope.FederalDEA = [{
        DEANumber: "BS4971354",
        StateOfReg: "Florida",
        IssueDate: "02-24-2014",//new Date(2014, 2, 24),
        ExpiryDate: "02-28-2017",//new Date(2017, 2, 28),
        IsInGoodStanding: "Yes",
        DEALicenceCertPath: "/Content/Document/SINGH - DEA.pdf"
    }];
    $scope.ScheduleInformations = [
        {
            DEASchedule: { Description: "Schedule I", ScheduleStatus: "Inactive" },
            DEAScheduleTypes: [{ Description: "Narcotic", Status: "Disabled" }]
        },
         {
             DEASchedule: { Description: "Schedule II", ScheduleStatus: "Active" },
             DEAScheduleTypes: [{ Description: "Narcotic", Status: "Active" }]
         },
          {
              DEASchedule: { Description: "Schedule III", ScheduleStatus: "Active" },
              DEAScheduleTypes: [{ Description: "Narcotic", Status: "Active" }]
          },
           {
               DEASchedule: { Description: "Schedule IV", ScheduleStatus: "Active" },
               DEAScheduleTypes: [{ Description: "Narcotic", Status: "Disabled" }]
           },
            {
                DEASchedule: { Description: "Schedule V", ScheduleStatus: "Active" },
                DEAScheduleTypes: [{ Description: "Narcotic", Status: "Disabled" }]
            }

    ]
    $scope.States=Countries[1].States;
    //=============== Federal DEA License Conditions ==================
    $scope.editShowFederalDEALicense = false;
    $scope.newShowFederalDEALicense = false;
    $scope.submitButtonText = "Add";
    $scope.IndexValue = 0;
    $scope.viewFederalDEALicense=false

    //====================== Federal DEA License ===================

    $scope.addFederalDEALicense = function () {
        $scope.newShowFederalDEALicense = true;
        $scope.submitButtonText = "Add";
        $scope.FederalDEALicense = {};
        ResetFederalDEALicenseForm();
    };

    $scope.editFederalDEALicense = function (index, FederalDEALicense) {
        $scope.viewFederalDEALicense = false
        $scope.newShowFederalDEALicense = false;
        $scope.editShowFederalDEALicense = true;
        $scope.submitButtonText = "Update";
        $scope.FederalDEALicense = FederalDEALicense;
        $scope.IndexValue = index;
    };
    $scope.viewFederalDEALicenseInfo = function (index, FederalDEALicense) {
        $scope.editShowFederalDEALicense = false;
        $scope.viewFederalDEALicense = true;
        $scope.FederalDEALicense = FederalDEALicense;
        $scope.IndexValue = index;
    };
    $scope.cancelFederalDEALicense = function (condition) {
        setFederalDEALicenseCancelParameters();
    };

    $scope.saveFederalDEALicense = function (FederalDEALicense) {

        console.log(FederalDEALicense);

        var validationStatus;
        var url;

        if ($scope.newShowFederalDEALicense) {
            //Add Details - Denote the URL
            validationStatus = $('#newShowFederalDEALicenseDiv').find('form').valid();
            url = "/Profile/IdentificationAndLicense/AddFederalDEALicense?profileId=1";
        }
        else if ($scope.editShowFederalDEALicense) {
            //Update Details - Denote the URL
            validationStatus = $('#FederalDEALicenseEditDiv' + $scope.IndexValue).find('form').valid();
            url = "/Profile/IdentificationAndLicense/UpdateFederalDEALicense?profileId=1";
        }

        console.log(FederalDEALicense);

        if (validationStatus) {
            // Simple POST request example (passing data) :
            $http.post(url, FederalDEALicense).
              success(function (data, status, headers, config) {
                  alert("Success");
                  setFederalDEALicenseCancelParameters();
              }).
              error(function (data, status, headers, config) {
                  alert("Error");
              });
        }
    };

    function setFederalDEALicenseCancelParameters() {
        $scope.viewFederalDEALicense = false
        $scope.editShowFederalDEALicense = false;
        $scope.newShowFederalDEALicense = false;
        $scope.FederalDEALicense = {};
        $scope.IndexValue = 0;
    }

    function ResetFederalDEALicenseForm() {
        $('#newShowFederalDEALicenseDiv').find('.FederalDEALicenseForm')[0].reset();
        $('#newShowFederalDEALicenseDiv').find('span').html('');
    }


    //****************************************CDSC Information********************************************


    //$scope.CDSCInformations = [{
    //   CertNumber: "216281",
    //   State: "Florida",
    //   IssueDate: new Date(2006, 4, 3),
    //   ExpiryDate:new Date(2026, 9, 3),
    //   CDSCCertPath: "certificat.pdf"
    //}];


    //=============== CDSC Information Conditions ==================
    $scope.editShowCDSCInformation = false;
    $scope.newShowCDSCInformation = false;
    $scope.submitButtonText = "Add";
    $scope.IndexValue = 0;
    $scope.hideAllCDSC = false;

    //====================== CDSC Information ===================

    $scope.hideCDSC = function () {
        $scope.hideAllCDSC = true;
        //$scope.newShowCDSCInformation = false;

    };

    $scope.addCDSCInformation = function () {
        $scope.newShowCDSCInformation = true;
        $scope.submitButtonText = "Add";
        $scope.cDSCInformation = {};
        ResetCDSCInformationForm();
        $scope.hideAllCDSC = false;

    };

    $scope.editCDSCInformation = function (index, cDSCInformation) {
        $scope.newShowCDSCInformation = false;
        $scope.viewCDSC = false;
        $scope.editShowCDSCInformation = true;
        $scope.submitButtonText = "Update";
        $scope.cDSCInformation = cDSCInformation;
        $scope.IndexValue = index;
    };
    $scope.viewCDSCInformation = function (index, cDSCInformation) {
        $scope.editShowCDSCInformation = false;
        $scope.viewCDSC = true;
        $scope.cDSCInformation = cDSCInformation;
        $scope.IndexValue = index;
    };
    $scope.cancelCDSCInformation = function (condition) {
        $scope.hideAllCDSC = true;

        setCDSCInformationCancelParameters();
    };

    $scope.saveCDSCInformation = function (cDSCInformation) {

        console.log(cDSCInformation);

        var validationStatus;
        var url;

        if ($scope.newShowCDSCInformation) {
            //Add Details - Denote the URL
            validationStatus = $('#newShowCDSCInformationDiv').find('form').valid();
            url = "/Profile/IdentificationAndLicense/AddCDSCInformation?profileId=1";
        }
        else if ($scope.editShowCDSCInformation) {
            //Update Details - Denote the URL
            validationStatus = $('#cDSCInformationEditDiv' + $scope.IndexValue).find('form').valid();
            url = "/Profile/IdentificationAndLicense/UpdateCDSCInformation?profileId=1";
        }

        console.log(cDSCInformation);

        if (validationStatus) {
            // Simple POST request example (passing data) :
            $http.post(url, cDSCInformation).
              success(function (data, status, headers, config) {
                  alert("Success");
                  setCDSCInformationCancelParameters();
              }).
              error(function (data, status, headers, config) {
                  alert("Error");
              });
        }
    };

    function setCDSCInformationCancelParameters() {
        $scope.viewCDSC = false;
        $scope.editShowCDSCInformation = false;
        $scope.newShowCDSCInformation = false;       
        $scope.cDSCInformation = {};
        $scope.IndexValue = 0;
    }

    function ResetCDSCInformationForm() {
        $('#newShowCDSCInformationDiv').find('.cDSCInformationForm')[0].reset();
        $('#newShowCDSCInformationDiv').find('span').html('');
    }


    //****************************************Medicare Information********************************************


    $scope.MedicareInformations = [{
        LicenseNumber: "31545S",
        State: "",
        IssueDate:"",
        ExpiryDate: new Date(2026, 9, 22),
        LicenseCertPath: "/Content/Document/DocPreview.pdf"
    }];


    //=============== Medicare Information Conditions ==================
    $scope.editShowMedicareInformation = false;
    $scope.newShowMedicareInformation = false;
    $scope.submitButtonText = "Add";
    $scope.IndexValue = 0;
    $scope.viewMedicare = false;

    //====================== Medicare Information ===================

    $scope.addMedicareInformation = function () {
        $scope.newShowMedicareInformation = true;
        $scope.submitButtonText = "Add";
        $scope.MedicareInformation = {};
        ResetMedicareInformationForm();
    };

    $scope.editMedicareInformation = function (index, MedicareInformation) {
        $scope.viewMedicare = false;
        $scope.newShowMedicareInformation = false;
        $scope.editShowMedicareInformation = true;
        $scope.submitButtonText = "Update";
        $scope.MedicareInformation = MedicareInformation;
        $scope.IndexValue = index;
    };

    $scope.cancelMedicareInformation = function (condition) {
        setMedicareInformationCancelParameters();
    };

    $scope.saveMedicareInformation = function (MedicareInformation) {

        console.log(MedicareInformation);

        var validationStatus;
        var url;

        if ($scope.newShowMedicareInformation) {
            //Add Details - Denote the URL
            validationStatus = $('#newShowMedicareInformationDiv').find('form').valid();
            url = "/Profile/IdentificationAndLicense/AddMedicareInformation?profileId=1";
        }
        else if ($scope.editShowMedicareInformation) {
            //Update Details - Denote the URL
            validationStatus = $('#InformationEditDiv' + $scope.IndexValue).find('form').valid();
            url = "/Profile/IdentificationAndLicense/UpdateMedicareInformation?profileId=1";
        }

        console.log(MedicareInformation);

        if (validationStatus) {
            // Simple POST request example (passing data) :
            $http.post(url, MedicareInformation).
              success(function (data, status, headers, config) {
                  alert("Success");
                  setMedicareInformationCancelParameters();
              }).
              error(function (data, status, headers, config) {
                  alert("Error");
              });
        }
    };

    function setMedicareInformationCancelParameters() {
        $scope.viewMedicare=false
        $scope.editShowMedicareInformation = false;
        $scope.newShowMedicareInformation = false;
        $scope.MedicareInformation = {};
        $scope.IndexValue = 0;
    }

    function ResetMedicareInformationForm() {
        $('#newShowMedicareInformationDiv').find('.MedicareInformationForm')[0].reset();
        $('#newShowMedicareInformationDiv').find('span').html('');
    }
    $scope.viewMedicareInformation = function (index, MedicareInformation) {
        $scope.editShowMedicareInformation = false;
        $scope.viewMedicare = true;
        $scope.MedicareInformation = MedicareInformation;
        $scope.IndexValue = index;
    };

    //****************************************Medicaid Information********************************************


    $scope.MedicaidInformations = [{
        LicenseNumber: "253729000",
        State: "Florida",
        IssueDate: "",
        ExpiryDate:new Date(2021, 9, 2),
        LicenseCertPath:"/Content/Document/DocPreview.pdf"
    }];


    //=============== Medicaid Information Conditions ==================
    $scope.editShowMedicaidInformation = false;
    $scope.newShowMedicaidInformation = false;
    $scope.submitButtonText = "Add";
    $scope.IndexValue = 0;
    $scope.viewMedicaid = false;

    //====================== Medicaid Information ===================

    $scope.addMedicaidInformation = function () {
        $scope.newShowMedicaidInformation = true;
        $scope.submitButtonText = "Add";
        $scope.MedicaidInformation = {};
        ResetMedicaidInformationForm();
    };
    $scope.viewMedicaidInformation = function (index, MedicaidInformation) {
        $scope.editShowMedicaidInformation = false;
        $scope.viewMedicaid = true;
        $scope.MedicaidInformation = MedicaidInformation;
        $scope.IndexValue = index;
    };
    $scope.editMedicaidInformation = function (index, MedicaidInformation) {
        $scope.viewMedicaid = false;
        $scope.newShowMedicaidInformation = false;
        $scope.editShowMedicaidInformation = true;
        $scope.submitButtonText = "Update";
        $scope.MedicaidInformation = MedicaidInformation;
        $scope.IndexValue = index;
    };

    $scope.cancelMedicaidInformation = function (condition) {
        setMedicaidInformationCancelParameters();
    };

    $scope.saveMedicaidInformation = function (MedicaidInformation) {

        console.log(MedicaidInformation);

        var validationStatus;
        var url;

        if ($scope.newShowMedicaidInformation) {
            //Add Details - Denote the URL
            validationStatus = $('#newShowMedicaidInformationDiv').find('form').valid();
            url = "/Profile/IdentificationAndLicense/AddMedicaidInformation?profileId=1";
        }
        else if ($scope.editShowMedicaidInformation) {
            //Update Details - Denote the URL
            validationStatus = $('#MedicaidInformationEditDiv' + $scope.IndexValue).find('form').valid();
            url = "/Profile/IdentificationAndLicense/UpdateMedicaidInformation?profileId=1";
        }

        console.log(MedicaidInformation);

        if (validationStatus) {
            // Simple POST request example (passing data) :
            $http.post(url, MedicaidInformation).
              success(function (data, status, headers, config) {
                  alert("Success");
                  setMedicaidInformationCancelParameters();
              }).
              error(function (data, status, headers, config) {
                  alert("Error");
              });
        }
    };

    function setMedicaidInformationCancelParameters() {
        $scope.viewMedicaid = false;
        $scope.editShowMedicaidInformation = false;
        $scope.newShowMedicaidInformation = false;
        $scope.MedicaidInformation = {};
        $scope.IndexValue = 0;
    }

    function ResetMedicaidInformationForm() {
        $('#newShowMedicaidInformationDiv').find('.MedicaidInformationForm')[0].reset();
        $('#newShowMedicaidInformationDiv').find('span').html('');
    }

    //****************************************Other Identification Numbers********************************************


    $scope.OtherIdentificationNumbers = {
        NPIDetails: { Number: "1417989625", UserName: "", Password: "" },
        CAQHDetails: { Number: "10721240", UserName: "", Password: "" },
        UPINNumber: "G65154",
        USMLENumber: ""
    };
    $scope.showOtherId = false;
    //====================== Other Identification Numbers ===================


    $scope.editOtherId = function (OtherIdentificationNumbers) {
        $scope.showOtherId = true;
        $scope.TempOtherIdentificationNumbers = $scope.OtherIdentificationNumbers;
            //angular.copy($scope.OtherIdentificationNumbers);
    }
    $scope.saveOtherIdentificationNumber = function (otherIdentificationNumber) {

        console.log(otherIdentificationNumber);

        var validationStatus;
        var url;
                
        //Update Details - Denote the URL
        validationStatus = $('#otherIdenNumbers').find('form').valid();
        url = "/Profile/IdentificationAndLicense/UpdateOtherIdentificationNumber?profileId=1";
        
        console.log(otherIdentificationNumber);

        if (validationStatus) {
            // Simple POST request example (passing data) :
            $http.post(url, otherIdentificationNumber).
              success(function (data, status, headers, config) {
                  alert("Success");
                  setOtherIdentificationNumberCancelParameters();
              }).
              error(function (data, status, headers, config) {
                  alert("Error");
              });
        }
    };
    $scope.cancelOtherIdentificationNumber = function (condition) {
        setOtherIdentificationNumberCancelParameters();
    };
    function setOtherIdentificationNumberCancelParameters() {
        $scope.showOtherId = false;
       
        $scope.IndexValue = 0;
    }

});

$(document).ready(function () {
});
