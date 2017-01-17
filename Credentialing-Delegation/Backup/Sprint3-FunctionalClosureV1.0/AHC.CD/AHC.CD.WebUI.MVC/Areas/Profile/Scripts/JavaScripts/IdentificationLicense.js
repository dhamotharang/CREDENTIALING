
//=========================== Controller declaration ==========================

profileApp.controller('identificationLicenseController', ['$scope', '$rootScope', '$http', 'dynamicFormGenerateService', 'masterDataService', 'locationService', function ($scope, $rootScope, $http, dynamicFormGenerateService, masterDataService, locationService) {

    //=============================Get Master Data===========
        //=======Provider Types for state license

    masterDataService.getMasterData("/Profile/MasterData/GetAllProviderTypes").then(function (Providertypes) {
        $scope.ProviderTypes = Providertypes;
    });
  
    //=======================State License Status
    masterDataService.getMasterData("/Profile/MasterData/GetAllLicenseStatus").then(function (LicenseStatuses) {
       // console.log(LicenseStatuses);
        $scope.StateLicenseStatuses = LicenseStatuses;
    });

    //==============================States

    angular.element(document).ready(function () {
        $http.get("/Location/GetStates")     //locationService makes an ajax call to fetch all the cities which have relevent names as the query string.
        .then(function (response) {
          //  console.log(response.data);
            $scope.States = response.data;
        });
    });
  

    //====================== State License ===================

    $scope.StateLicenses = [];

    //==============Search cum drop down for Title & License Type & State============

    //$scope.showDiv = function (divId) {
    //    $("#" + divId).show();
    //};

    //$scope.SelectedProviderType = function (providertype) {
    //    $scope.tempObject.ProviderTypeID = providertype.ProviderTypeID;
    //    $scope.searchproviderType = providertype.Title;
    //    //$scope.tempProviderTypes.splice($scope.tempProviderTypes.indexOf(providertype), 1);
    //};

    //$scope.SelectedLicenseStatus = function (LicenseStatus) {
    //    $scope.tempObject.StateLicenseStatusID = LicenseStatus.StateLicenseStatusID;
    //    $scope.searchLicenseStatus = LicenseStatus.Title;
    //    //$scope.tempProviderTypes.splice($scope.tempProviderTypes.indexOf(providertype), 1);
    //};

    //$scope.SelectedState = function (StateObj) {
    //    $scope.tempObject.IssueState = StateObj.State;
    //    $scope.IssueState = StateObj.State;
    //    //$scope.tempProviderTypes.splice($scope.tempProviderTypes.indexOf(providertype), 1);
    //};

    //===============================================
 
    // rootScoped on emitted value catches the value for the model and insert to get the old data
    //calling the method using $on(PSP-public subscriber pattern)
    $rootScope.$on('StateLicenses', function (event, val) {
        $scope.StateLicenses = val;
        for (var i = 0; i < $scope.StateLicenses.length ; i++) {
            $scope.StateLicenses[i].IssueDate = ConvertDateFormat($scope.StateLicenses[i].IssueDate);
            $scope.StateLicenses[i].ExpiryDate = ConvertDateFormat($scope.StateLicenses[i].ExpiryDate);;
            $scope.StateLicenses[i].CurrentIssueDate = ConvertDateFormat($scope.StateLicenses[i].CurrentIssueDate);
            }
     //   console.log(val);
    });

    $scope.IndexValue1 = '';

    //------------SAVE--------------
    $scope.saveStateLicense = function (stateLicense,index) {

      //  console.log(stateLicense);
        var validationStatus;
        var url;
        var $formDataStateLicense;
        var tempProviderType;
        var providerTypeobj;
        var tempLicenseStatus;
        var LicenseStatusobj;
              $scope.SLError = '';
        tempProviderType = stateLicense.ProviderTypeID;
        tempLicenseStatus = stateLicense.StateLicenseStatusID;
        $scope.IndexValue1 = index;

        for (var type in $scope.ProviderTypes) {
            if ($scope.ProviderTypes[type].ProviderTypeID == tempProviderType) {
                providerTypeobj = $scope.ProviderTypes[type];
                break;
            }
        }

        for (var sls in $scope.StateLicenseStatuses) {
            if ($scope.StateLicenseStatuses[sls].StateLicenseStatusID == tempLicenseStatus) {
                LicenseStatusobj = $scope.StateLicenseStatuses[sls];
                break;
            }
        }

        if ($scope.visibilityControl == 'addStateLicenseInformation') {
            //Add Details - Denote the URL
           // validationStatus = $('#newStateLicenseDiv').find('form').valid();
            $formDataStateLicense = $('#newStateLicenseDiv').find('form');
            url = "/Profile/IdentificationAndLicense/AddStateLicenseAsync?profileId=" + profileId;
        }
        else if ($scope.visibilityControl == (index + '_editStateLicenseInformation')) {
            //Update Details - Denote the URL
            //validationStatus = $('#stateLicenseEditDiv' +index).find('form').valid();
            $formDataStateLicense = $('#stateLicenseEditDiv' + index).find('form');
            url = "/Profile/IdentificationAndLicense/UpdateStateLicenseAsync?profileId=" + profileId;
        }

      //  console.log($formDataStateLicense);
        
        ResetFormForValidation($formDataStateLicense);
        validationStatus = $formDataStateLicense.valid();
        if (validationStatus) {
          
         //   console.log($formDataStateLicense);
            $.ajax({
                url: url,
                type: 'POST',
                data: new FormData($formDataStateLicense[0]),
                async: false,
                cache: false,
                contentType: false,
                processData: false,
                success: function (data) {
                //    console.log(data);
                    if (data.status == "true") {
                        data.stateLicense.ProviderType = providerTypeobj;
                        data.stateLicense.StateLicenseStatus = LicenseStatusobj;
                        data.stateLicense.IssueDate = ConvertDateFormat(data.stateLicense.IssueDate);
                       data.stateLicense.ExpiryDate = ConvertDateFormat(data.stateLicense.ExpiryDate); 
                       data.stateLicense.CurrentIssueDate = ConvertDateFormat(data.stateLicense.CurrentIssueDate);

                        if (index == -1)
                        {
                            $scope.StateLicenses.push(data.stateLicense);
                            $rootScope.visibilityControl = "addedStateLicense";
                            $rootScope.tempObject = {};
                        }
                        else
                        {
                            $scope.StateLicenses[index] = data.stateLicense;
                            $rootScope.visibilityControl = "updatedStateLicense";
                        }
                       
                        $rootScope.tempObject = {};
                        FormReset($formDataStateLicense);

                    } else {
                        $scope.SLError = data.status.split(",");
                    }
                },
                 error: function (e) {
                     $scope.SLError = "Sorry for Inconvenience !!!! Please Try Again Later...";
                }
            });
        }
    };

    //****************************************Federal DEA Info********************************************
    //=============== Federal DEA License Conditions ==================

   // $scope.submitButtonText = "Add";
    

    //=================Data============
    masterDataService.getMasterData("/Profile/MasterData/GetAllDEASchedules").then(function (Schedules) {
        $scope.Schedules = Schedules;
    });
   
  
    $scope.FederalDEA = [];

   
    // rootScoped on emited value catches the value for the model and insert to get the old data
    $rootScope.$on('FederalDEAInformations', function (event, val) {
        $scope.FederalDEA = val;
        for (var i = 0; i < $scope.FederalDEA.length ; i++) {
            $scope.FederalDEA[i].IssueDate = ConvertDateFormat($scope.FederalDEA[i].IssueDate);
            $scope.FederalDEA[i].ExpiryDate = ConvertDateFormat($scope.FederalDEA[i].ExpiryDate);;
        }
    });

    $scope.IndexValue2 = '';
    //====================== Federal DEA License Save ===================


    $scope.saveFederalDEALicense = function (FederalDEALicense, index) {

     //   console.log(FederalDEALicense);
        $scope.IndexValue2 = index;

        var validationStatus;
        var url;
        var $formDataDEA;
        if ($scope.visibilityControl == 'addDEAInformation') {
            //Add Details - Denote the URL
           // validationStatus = $('#newShowFederalDEALicenseDiv').find('form').valid();
            $formDataDEA = $('#newShowFederalDEALicenseDiv').find('form');
            url = "/Profile/IdentificationAndLicense/AddFederalDEALicenseAsync?profileId="+profileId;
        }
        else if ($scope.visibilityControl == (index + '_editDEAInformation')) {
            //Update Details - Denote the URL
         //   validationStatus = $('#FederalDEALicenseEditDiv' + index).find('form').valid();
            $formDataDEA = $('#FederalDEALicenseEditDiv' + index).find('form');

            url = "/Profile/IdentificationAndLicense/UpdateFederalDEALicenseAsync?profileId=" + profileId;
        }


        ResetFormForValidation($formDataDEA);
        validationStatus = $formDataDEA.valid();
        $scope.DEAError = '';

        if (validationStatus) {
        //    console.log($formDataDEA);
            $.ajax({
                url: url,
                type: 'POST',
                data: new FormData($formDataDEA[0]),
                async: false,
                cache: false,
                contentType: false,
                processData: false,
                success: function (data) {
                //    console.log(data);
                    if (data.status == "true") {
                        data.federalDea.IssueDate = ConvertDateFormat(data.federalDea.IssueDate);
                        data.federalDea.ExpiryDate = ConvertDateFormat(data.federalDea.ExpiryDate);
                        if (index == -1) {
                            $scope.FederalDEA.push(data.federalDea);
                            $rootScope.visibilityControl = "addedDEA";
                            $rootScope.tempObject = {};
                        }
                        else {
                            $scope.FederalDEA[index]=data.federalDea; 
                            $rootScope.visibilityControl = "updatedDEA";
                        }
                        $rootScope.tempObject = {};
                        FormReset($formDataDEA);
                    } else {
                        $scope.DEAError = data.status.split(",");
                      
                    }
                },
                error: function (e) {
                    $scope.DEAError = "Sorry for Inconvenience !!!! Please Try Again Later...";
                }
            });
        }
    };

   

    //****************************************CDSC Information********************************************

    $scope.CDSCInformations = [];
   
    // rootScoped on emited value catches the value for the model and insert to get the old data
    $rootScope.$on('CDSCInformations', function (event, val) {
        $scope.CDSCInformations = val;
        for (var i = 0; i < $scope.CDSCInformations.length ; i++) {
            $scope.CDSCInformations[i].IssueDate = ConvertDateFormat($scope.CDSCInformations[i].IssueDate);
            $scope.CDSCInformations[i].ExpiryDate = ConvertDateFormat($scope.CDSCInformations[i].ExpiryDate);;
        }

    });

    //=============== CDSC Information Conditions ==================
   
 //   $scope.submitButtonText = "Add";
   
    $scope.CDSCError = '';
    $scope.IndexValue5 = '';
    //====================== CDSC Information Save===================

    $scope.saveCDSCInformation = function (cDSCInformation,index) {

     //   console.log(cDSCInformation);
        $scope.IndexValue5 = index;

        var validationStatus;
        var url;
        var $formDataCDSC;
        if ($scope.visibilityControl == 'addCDSCInformation') {
            //Add Details - Denote the URL
          //  validationStatus = $('#newShowCDSCInformationDiv').find('form').valid();
            $formDataCDSC = $('#newShowCDSCInformationDiv').find('form');

            url = "/Profile/IdentificationAndLicense/AddCDSCLicenseAsync?profileId="+profileId;
        }
        else if ($scope.visibilityControl == (index + '_editCDSCInformation')) {
            //Update Details - Denote the URL
         //   validationStatus = $('#cDSCInformationEditDiv' + index).find('form').valid();
            $formDataCDSC = $('#cDSCInformationEditDiv' + index).find('form');

            url = "/Profile/IdentificationAndLicense/UpdateCDSCLicenseAsync?profileId=" + profileId;
        }

        ResetFormForValidation($formDataCDSC);
        validationStatus = $formDataCDSC.valid();

        

        if (validationStatus) {
        //    console.log($formDataCDSC);
            $.ajax({
                url: url,
                type: 'POST',
                data: new FormData($formDataCDSC[0]),
                async: false,
                cache: false,
                contentType: false,
                processData: false,
                success: function (data) {
            //        console.log(data);
                    if (data.status == "true") {
                        data.CDSCInformation.IssueDate = ConvertDateFormat(data.CDSCInformation.IssueDate);
                        data.CDSCInformation.ExpiryDate = ConvertDateFormat(data.CDSCInformation.ExpiryDate);
                        if (index == -1) {
                            $scope.CDSCInformations.push(data.CDSCInformation);
                            $rootScope.visibilityControl = "addedCDS";
                            $rootScope.tempObject = {};
                        }
                        else {
                            $scope.CDSCInformations[index] = data.CDSCInformation;
                            $rootScope.visibilityControl = "updatedCDS";
                        }
                        $rootScope.tempObject = {};
                        FormReset($formDataCDSC);
                    } else {
                        $scope.CDSCError = data.status.split(",");

                    }
                },
                error: function (e) {
                    $scope.CDSCError = "Sorry for Inconvenience !!!! Please Try Again Later...";
                }
            });
        }
    };

    //****************************************Medicare Information********************************************

    $scope.MedicareInformations = [];
    
    // rootScoped on emited value catches the value for the model and insert to get the old data
    $rootScope.$on('MedicareInformations', function (event, val) {
        $scope.MedicareInformations = val;
        for (var i = 0; i < $scope.MedicareInformations.length ; i++) {
            $scope.MedicareInformations[i].IssueDate = ConvertDateFormat($scope.MedicareInformations[i].IssueDate);
        }

    });

    //=============== Medicare Information Conditions ==================
   
  //  $scope.submitButtonText = "Add";
    $scope.IndexValue3 = '';

    //====================== Medicare Information ===================

    $scope.saveMedicareInformation = function (MedicareInformation,index) {
    //    console.log(MedicareInformation);
        $scope.IndexValue3 = index;
        var validationStatus;
        var url;
        var $formDataMedicare;
        if ($scope.visibilityControl == 'addMedicareInformation') {
            //Add Details - Denote the URL
          //  validationStatus = $('#newShowMedicareInformationDiv').find('form').valid();
            $formDataMedicare = $('#newShowMedicareInformationDiv').find('form');
            url = "/Profile/IdentificationAndLicense/AddMedicareInformationAsync?profileId=" + profileId;
        }
        else if ($scope.visibilityControl == (index + '_editMedicareInformation')) {
            //Update Details - Denote the URL
         //   validationStatus = $('#MedicareInformationEditDiv' + index).find('form').valid();
            $formDataMedicare = $('#MedicareInformationEditDiv' + index).find('form');
            url = "/Profile/IdentificationAndLicense/UpdateMedicareInformationAsync?profileId=" + profileId;
        }

     //   console.log(MedicareInformation);
        ResetFormForValidation($formDataMedicare);
        validationStatus = $formDataMedicare.valid();
        $scope.MedicareError = '';
        
        if (validationStatus) {
        //    console.log($formDataMedicare);
            $.ajax({
                url: url,
                type: 'POST',
                data: new FormData($formDataMedicare[0]),
                async: false,
                cache: false,
                contentType: false,
                processData: false,
                success: function (data) {
              //      console.log(data);
                    if (data.status == "true") {
                        data.MedicareInformation.IssueDate = ConvertDateFormat(data.MedicareInformation.IssueDate);
                       // data.MedicareInformation.ExpiryDate = ConvertDateFormat(data.MedicareInformation.ExpiryDate);
                        if (index == -1) {
                            $scope.MedicareInformations.push(data.MedicareInformation);
                            $rootScope.visibilityControl = "addedMedicare";
                            $rootScope.tempObject = {};
                        }
                        else {
                            $scope.MedicareInformations[index] = data.MedicareInformation;
                            $rootScope.visibilityControl = "updatedMedicare";
                        }
                        $rootScope.tempObject = {};                        
                        FormReset($formDataMedicare);
                    } else {
                        $scope.MedicareError = data.status.split(",");

                    }
                },
                error: function (e) {
                    $scope.MedicareError = "Sorry for Inconvenience !!!! Please Try Again Later...";
                }
            });
        }
    };

    //****************************************Medicaid Information********************************************

    $scope.MedicaidInformations = [];
    
    // rootScoped on emited value catches the value for the model and insert to get the old data
    $rootScope.$on('MedicaidInformations', function (event, val) {
        $scope.MedicaidInformations = val;
        for (var i = 0; i < $scope.MedicaidInformations.length ; i++) {
            $scope.MedicaidInformations[i].IssueDate = ConvertDateFormat($scope.MedicaidInformations[i].IssueDate);
        }
    });

    //=============== Medicaid Information Conditions ==================
   
  //  $scope.submitButtonText = "Add";
    $scope.IndexValue4 = '';
   
    //====================== Medicaid Information ===================

    $scope.saveMedicaidInformation = function (MedicaidInformation,index) {
     //   console.log(MedicaidInformation);
        $scope.IndexValue4 = index;
        var validationStatus;
        var url;
        var $formDataMedicaid;
        if ($scope.visibilityControl == 'addMedicaidInformation') {
            //Add Details - Denote the URL
           // validationStatus = $('#newShowMedicaidInformationDiv').find('form').valid();
            $formDataMedicaid = $('#newShowMedicaidInformationDiv').find('form');
            url = "/Profile/IdentificationAndLicense/AddMedicaidInformationAsync?profileId=" + profileId;
        }
        else if ($scope.visibilityControl == (index + '_editMedicaidInformation')) {
            //Update Details - Denote the URL
          //  validationStatus = $('#MedicaidInformationEditDiv' +index).find('form').valid();
            $formDataMedicaid = $('#MedicaidInformationEditDiv' + index).find('form');
            url = "/Profile/IdentificationAndLicense/UpdateMedicaidInformationAsync?profileId=" + profileId;
        }
    //    console.log($formDataMedicaid);

        ResetFormForValidation($formDataMedicaid);
        validationStatus = $formDataMedicaid.valid();
        $scope.MedicaidError = '';

        if (validationStatus) {
          //  console.log($formDataMedicaid);
            $.ajax({
                url: url,
                type: 'POST',
                data: new FormData($formDataMedicaid[0]),
                async: false,
                cache: false,
                contentType: false,
                processData: false,
                success: function (data) {
                //    console.log(data);
                    if (data.status == "true") {
                        data.MedicaidInformation.IssueDate = ConvertDateFormat(data.MedicaidInformation.IssueDate);
                       // data.MedicaidInformation.ExpiryDate = ConvertDateFormat(data.MedicaidInformation.ExpiryDate);
                        if (index == -1) {
                            $scope.MedicaidInformations.push(data.MedicaidInformation);
                            $rootScope.visibilityControl = "addedMedicaid";
                            $rootScope.tempObject = {};
                        }
                        else {
                            $scope.MedicaidInformations[index] = data.MedicaidInformation;
                            $rootScope.visibilityControl = "updatedMedicaid";
                        }
                        $rootScope.tempObject = {};
                        FormReset($formDataMedicaid);
                    } else {
                        $scope.MedicaidError = data.status.split(",");

                    }
                },
                error: function (e) {
                    $scope.MedicaidError = "Sorry for Inconvenience !!!! Please Try Again Later...";
                }
            });
        }
    };

    //****************************************Other Identification Numbers********************************************

    $scope.OtherIdentificationNumbers ={};

    // rootScoped on emited value catches the value for the model and insert to get the old data
    $rootScope.$on('OtherIdentificationNumber', function (event, val) {
     //   console.log(val)
        $scope.OtherIdentificationNumbers = val;
    });

    $scope.showOtherId = false;
    $scope.OtherIdError = '';
    //====================== Other Identification Numbers ===================

    $scope.saveOtherIdentificationNumber = function (Form_Id,otherIdentificationNumber) {
        if ($("#" + Form_Id).valid()) {
            $.ajax({
                url: '/Profile/IdentificationAndLicense/UpdateOtherIdentificationNumberAsync?profileId=' + profileId,
                type: 'POST',
                data: new FormData($("#" + Form_Id)[0]),
                async: false,
                cache: false,
                contentType: false,
                processData: false,
                success: function (data) {
               //     console.log(data);
                    if (data.status == "true") {
                        
                        otherIdentificationNumber.OtherIdentificationNumberID = data.OtherIdentificationNumber.OtherIdentificationNumberID;
                       
                        
                      

                        $scope.OtherIdentificationNumbers = angular.copy(otherIdentificationNumber);
                        FormReset($("#" + Form_Id));
                        $scope.ErrorId = false;
                        $rootScope.visibilityControl = "updatedID";
                    } else {
                        $scope.OtherIdError = data.status.split(",");
                        if ($scope.OtherIdError.length == 0)
                            $scope.OtherIdError = data.status;

                        $scope.ErrorId = true;

                    }
                },
                error: function (e) {
                    $scope.OtherIdError = "Sorry for Inconvenience !!!! Please Try Again Later...";
                }
            });
        }
    };
}]);
