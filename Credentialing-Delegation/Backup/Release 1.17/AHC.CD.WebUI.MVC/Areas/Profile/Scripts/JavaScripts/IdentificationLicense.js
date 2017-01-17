
//=========================== Controller declaration ==========================

profileApp.controller('identificationLicenseController', ['$scope', '$rootScope', '$http', 'masterDataService', 'locationService', 'messageAlertEngine', function ($scope, $rootScope, $http, masterDataService, locationService, messageAlertEngine) {

    //=============================Get Master Data===========
        //=======Provider Types for state license

    //show renew div
    //StateLicense
    $scope.ShowRenewDivStateLicense = false;
    $scope.RenewDivSL = function (StateLicenseInformation) {
        if (StateLicenseInformation.CurrentIssueDate == null || StateLicenseInformation.ExpiryDate == null || StateLicenseInformation.StateLicenseDocumentPath == null)
        { $scope.ShowRenewDivStateLicense = false; }
        else
        {
            $scope.ShowRenewDivStateLicense = true;
        }
    };
    //DEA
    $scope.ShowRenewDivDEA = false;
    $scope.RenewDivDEA = function (DEAInformation) {
        if (DEAInformation.IssueDate == null || DEAInformation.ExpiryDate == null || DEAInformation.DEALicenceCertPath == null)
        { $scope.ShowRenewDivDEA = false; }
        else
        {
            $scope.ShowRenewDivDEA = true;
        }
    };
    //CDS
    $scope.ShowRenewDivCDS = false;
    $scope.RenewDivCDS = function (CDSCInformation) {
        if (CDSCInformation.IssueDate == null || CDSCInformation.ExpiryDate == null || CDSCInformation.CDSCCerificatePath == null)
        { $scope.ShowRenewDivCDS = false; }
        else
        {
            $scope.ShowRenewDivCDS = true;
        }
    };

    masterDataService.getMasterData("/Profile/MasterData/GetAllProviderTypes").then(function (Providertypes) {
        try {
            $scope.ProviderTypes = Providertypes;
        }
        catch (e) { };
    });
  
    //=======================State License Status
    masterDataService.getMasterData("/Profile/MasterData/GetAllLicenseStatus").then(function (LicenseStatuses) {
        try {
            $scope.StateLicenseStatuses = LicenseStatuses;
        }
        catch (e) { };
       
    });

    //==============================States

    angular.element(document).ready(function () {
        try {
            $http.get("/Location/GetStates")     //locationService makes an ajax call to fetch all the cities which have relevent names as the query string.
       .then(function (response) {
           //  console.log(response.data);
           $scope.States = response.data;
       });
        }
        catch (e) { };
       
    });
  

    //====================== State License ===================

    $scope.StateLicenses = [];

    // rootScoped on emitted value catches the value for the model and insert to get the old data
    //calling the method using $on(PSP-public subscriber pattern)
    $rootScope.$on('StateLicenses', function (event, val) {
        try {
            $scope.StateLicenses = val;
            for (var i = 0; i < $scope.StateLicenses.length ; i++) {
                if (!$scope.StateLicenses[i].ProviderTypeID) { $scope.StateLicenses[i].ProviderTypeID = ""; }
                if (!$scope.StateLicenses[i].StateLicenseStatusID) { $scope.StateLicenses[i].StateLicenseStatusID = ""; }
                if (!$scope.StateLicenses[i].IssueState) { $scope.StateLicenses[i].IssueState = ""; }
                $scope.StateLicenses[i].IssueDate = ConvertDateFormat($scope.StateLicenses[i].IssueDate);
                $scope.StateLicenses[i].ExpiryDate = ConvertDateFormat($scope.StateLicenses[i].ExpiryDate);;
                $scope.StateLicenses[i].CurrentIssueDate = ConvertDateFormat($scope.StateLicenses[i].CurrentIssueDate);
            }
        }
        catch (e) { };
       
     //   console.log(val);
    });

    $scope.IndexValue1 = '';

    //------------SAVE--------------
    $scope.saveStateLicense = function (stateLicense,index) {

        loadingOn();
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
            try {
                if ($scope.ProviderTypes[type].ProviderTypeID == tempProviderType) {
                    providerTypeobj = $scope.ProviderTypes[type];
                    break;
                }
            }
            catch (e) { };
            
        }

        for (var sls in $scope.StateLicenseStatuses) {
            try {
                if ($scope.StateLicenseStatuses[sls].StateLicenseStatusID == tempLicenseStatus) {
                    LicenseStatusobj = $scope.StateLicenseStatuses[sls];
                    break;
                }
            }
            catch (e)
            { };
          
        }

        if ($scope.visibilityControl == 'addStateLicenseInformation') {
            //Add Details - Denote the URL
            try {
                $formDataStateLicense = $('#newStateLicenseDiv').find('form');
                url = "/Profile/IdentificationAndLicense/AddStateLicenseAsync?profileId=" + profileId;
            }
            catch(e)
            {  };          
        }
        else if ($scope.visibilityControl == (index + '_editStateLicenseInformation')) {
            //Update Details - Denote the URL
            try {
                $formDataStateLicense = $('#stateLicenseEditDiv' + index).find('form');
                url = "/Profile/IdentificationAndLicense/UpdateStateLicenseAsync?profileId=" + profileId;
            }
            catch (e)
            { };
        }
        else if ($scope.visibilityControl == (index + '_renewStateLicenseInformation')) {
            //Update Details - Denote the URL
            try {
                $formDataStateLicense = $('#stateLicenseRenewDiv' + index).find('form');
                url = "/Profile/IdentificationAndLicense/RenewStateLicenseAsync?profileId=" + profileId;
            }
            catch (e)
            { };
        }

        //console.log($formDataStateLicense);
        
        ResetFormForValidation($formDataStateLicense);
        validationStatus = $formDataStateLicense.valid();
        if (validationStatus) {
          
         //console.log($formDataStateLicense);
            $.ajax({
                url: url,
                type: 'POST',
                data: new FormData($formDataStateLicense[0]),
                async: false,
                cache: false,
                contentType: false,
                processData: false,
                success: function (data) {
                    //console.log(data);
                    try {
                        if (data.status == "true") {
                            data.stateLicense.ProviderType = providerTypeobj;
                            data.stateLicense.StateLicenseStatus = LicenseStatusobj;
                            data.stateLicense.IssueDate = ConvertDateFormat(data.stateLicense.IssueDate);
                            data.stateLicense.ExpiryDate = ConvertDateFormat(data.stateLicense.ExpiryDate);
                            data.stateLicense.CurrentIssueDate = ConvertDateFormat(data.stateLicense.CurrentIssueDate);

                            if ($scope.visibilityControl == 'addStateLicenseInformation') {
                                $scope.StateLicenses.push(data.stateLicense);
                                for (var i = 0; i < $scope.StateLicenses.length ; i++) {
                                    if (!$scope.StateLicenses[i].ProviderTypeID) { $scope.StateLicenses[i].ProviderTypeID = ""; }
                                    if (!$scope.StateLicenses[i].StateLicenseStatusID) { $scope.StateLicenses[i].StateLicenseStatusID = ""; }
                                    if (!$scope.StateLicenses[i].IssueState) { $scope.StateLicenses[i].IssueState = ""; }
                                }
                                $rootScope.operateCancelControl('');
                                messageAlertEngine.callAlertMessage("addedStateLicense", "State License Information Saved Successfully!!!!", "success", true);
                            }
                            else if ($scope.visibilityControl == (index + '_editStateLicenseInformation')) {
                                $scope.StateLicenses[index] = data.stateLicense;
                                for (var i = 0; i < $scope.StateLicenses.length ; i++) {
                                    if (!$scope.StateLicenses[i].ProviderTypeID) { $scope.StateLicenses[i].ProviderTypeID = ""; }
                                    if (!$scope.StateLicenses[i].StateLicenseStatusID) { $scope.StateLicenses[i].StateLicenseStatusID = ""; }
                                    if (!$scope.StateLicenses[i].IssueState) { $scope.StateLicenses[i].IssueState = ""; }
                                }
                                $rootScope.operateViewAndAddControl(index + '_viewStateLicenseInformation');
                                messageAlertEngine.callAlertMessage("updatedStateLicense" + index, "State License Information Updated Successfully!!!!", "success", true);
                            }
                            else
                            {
                                $scope.StateLicenses[index] = data.stateLicense;
                                for (var i = 0; i < $scope.StateLicenses.length ; i++) {
                                    if (!$scope.StateLicenses[i].ProviderTypeID) { $scope.StateLicenses[i].ProviderTypeID = ""; }
                                    if (!$scope.StateLicenses[i].StateLicenseStatusID) { $scope.StateLicenses[i].StateLicenseStatusID = ""; }
                                    if (!$scope.StateLicenses[i].IssueState) { $scope.StateLicenses[i].IssueState = ""; }
                                }
                                $rootScope.operateViewAndAddControl(index + '_viewStateLicenseInformation');
                                messageAlertEngine.callAlertMessage("renewedStateLicense" + index, "State License Information Renewed Successfully!!!!", "success", true);

                            }


                            FormReset($formDataStateLicense);
                            $scope.resetDates();
                        }

                        else {
                            $scope.SLError = data.status.split(",");
                            messageAlertEngine.callAlertMessage('StateLicenseError', "", "danger", true);
                        }
                    }
                    catch (e) { };
                    

                },
                error: function (e) {
                    try {
                        $scope.SLError = "Sorry for Inconvenience !!!! Please Try Again Later...";
                        messageAlertEngine.callAlertMessage('StateLicenseError', "", "danger", true);
                    }
                    catch(e){};
                    

                 }
             
            });
        }
        loadingOff();
    };

    //****************************************Federal DEA Info********************************************
    //=============== Federal DEA License Conditions ==================

   // $scope.submitButtonText = "Add";
    

    //=================Data============
    masterDataService.getMasterData("/Profile/MasterData/GetAllDEASchedules").then(function (Schedules) {
        try {
            $scope.Schedules = Schedules;
        }
        catch (e) { };
    
    });
   
  
    $scope.FederalDEA = [];

   
    // rootScoped on emited value catches the value for the model and insert to get the old data
    $rootScope.$on('FederalDEAInformations', function (event, val) {
        try {
            $scope.FederalDEA = val;
            for (var i = 0; i < $scope.FederalDEA.length ; i++) {
                $scope.FederalDEA[i].IssueDate = ConvertDateFormat($scope.FederalDEA[i].IssueDate);
                $scope.FederalDEA[i].ExpiryDate = ConvertDateFormat($scope.FederalDEA[i].ExpiryDate);;
            }
        }
        catch (e) { };
       
    });

    $scope.IndexValue2 = '';
    //====================== Federal DEA License Save ===================


    $scope.saveFederalDEALicense = function (FederalDEALicense, index) {
        loadingOn();
     //   console.log(FederalDEALicense);
        $scope.IndexValue2 = index;

        var validationStatus;
        var url;
        var $formDataDEA;
        if ($scope.visibilityControl == 'addDEAInformation') {
            try {
                $formDataDEA = $('#newShowFederalDEALicenseDiv').find('form');
                url = "/Profile/IdentificationAndLicense/AddFederalDEALicenseAsync?profileId=" + profileId;
            }
            catch (e) { };
            //Add Details - Denote the URL
           
        }
        else if ($scope.visibilityControl == (index + '_editDEAInformation')) {
            try {
                $formDataDEA = $('#FederalDEALicenseEditDiv' + index).find('form');
                url = "/Profile/IdentificationAndLicense/UpdateFederalDEALicenseAsync?profileId=" + profileId;
            }
            catch (e) { };
            //Update Details - Denote the URL
           
        }
        else if ($scope.visibilityControl == (index + '_renewDEAInformation')) {
            try {
                $formDataDEA = $('#DEAInformationRenewDiv' + index).find('form');
                url = "/Profile/IdentificationAndLicense/RenewFederalDEALicenseAsync?profileId=" + profileId;
            }
            catch (e) { };
            //Update Details - Denote the URL

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
                    try {
                        if (data.status == "true") {
                            data.federalDea.IssueDate = ConvertDateFormat(data.federalDea.IssueDate);
                            data.federalDea.ExpiryDate = ConvertDateFormat(data.federalDea.ExpiryDate);
                            if ($scope.visibilityControl == 'addDEAInformation') {
                                $scope.FederalDEA.push(data.federalDea);
                                $rootScope.operateCancelControl('');
                                messageAlertEngine.callAlertMessage("addedDEA", "DEA information saved successfully. !!!!", "success", true);
                            }
                            else if ($scope.visibilityControl == (index + '_editDEAInformation')) {
                                $scope.FederalDEA[index] = data.federalDea;
                                $rootScope.operateViewAndAddControl(index + '_viewDEAInformation');
                                messageAlertEngine.callAlertMessage("updatedDEA" + index, "DEA Information Updated Successfully!!!!", "success", true);
                            }
                            else
                            {
                                $scope.FederalDEA[index] = data.federalDea;
                                $rootScope.operateViewAndAddControl(index + '_viewDEAInformation');
                                messageAlertEngine.callAlertMessage("renewedDEA" + index, "DEA Information Renewed Successfully!!!!", "success", true);
                            }
                            FormReset($formDataDEA);
                            $scope.resetDates();
                        } else {
                            $scope.DEAError = data.status.split(",");
                            messageAlertEngine.callAlertMessage('DEAError', "", "danger", true);

                        }
                    }
                    catch (e) { };
                   
               
                },
                error: function (e) {
                    try {
                        $scope.DEAError = "Sorry for Inconvenience !!!! Please Try Again Later...";
                        messageAlertEngine.callAlertMessage('DEAError', "", "danger", true);
                    }
                    catch (e) { };
                  
                }
            });
        }
        loadingOff();
    };

   

    //****************************************CDSC Information********************************************

    $scope.CDSCInformations = [];
   
    // rootScoped on emited value catches the value for the model and insert to get the old data
    $rootScope.$on('CDSCInformations', function (event, val) {
        try {
            $scope.CDSCInformations = val;
            for (var i = 0; i < $scope.CDSCInformations.length ; i++) {
                $scope.CDSCInformations[i].IssueDate = ConvertDateFormat($scope.CDSCInformations[i].IssueDate);
                $scope.CDSCInformations[i].ExpiryDate = ConvertDateFormat($scope.CDSCInformations[i].ExpiryDate);;
            }
        }
        catch (e) { };
      

    });

    //=============== CDSC Information Conditions ==================
   
 //   $scope.submitButtonText = "Add";
   
    $scope.CDSCError = '';
    $scope.IndexValue5 = '';
    //====================== CDSC Information Save===================

    $scope.saveCDSCInformation = function (cDSCInformation,index) {
        loadingOn();
     //   console.log(cDSCInformation);
        $scope.IndexValue5 = index;

        var validationStatus;
        var url;
        var $formDataCDSC;
        if ($scope.visibilityControl == 'addCDSCInformation') {
            try {
                $formDataCDSC = $('#newShowCDSCInformationDiv').find('form');
                url = "/Profile/IdentificationAndLicense/AddCDSCLicenseAsync?profileId=" + profileId;
            }
            catch (e) { };
            //Add Details - Denote the URL
          
        }
        else if ($scope.visibilityControl == (index + '_editCDSCInformation')) {
            try {
                $formDataCDSC = $('#cDSCInformationEditDiv' + index).find('form');
                url = "/Profile/IdentificationAndLicense/UpdateCDSCLicenseAsync?profileId=" + profileId;
            }
            catch (e) { };
            //Update Details - Denote the URL
           
        }
        else if ($scope.visibilityControl == (index + '_renewCDSCInformation')) {
            try {
                $formDataCDSC = $('#CDSCInformationRenewDiv' + index).find('form');
                url = "/Profile/IdentificationAndLicense/RenewCDSCLicenseAsync?profileId=" + profileId;
            }
            catch (e) { };
            //Update Details - Denote the URL

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
                    try {
                        if (data.status == "true") {
                            data.CDSCInformation.IssueDate = ConvertDateFormat(data.CDSCInformation.IssueDate);
                            data.CDSCInformation.ExpiryDate = ConvertDateFormat(data.CDSCInformation.ExpiryDate);
                            if ($scope.visibilityControl == 'addCDSCInformation') {
                                $scope.CDSCInformations.push(data.CDSCInformation);
                                $rootScope.operateCancelControl('');
                                messageAlertEngine.callAlertMessage("addedCDS", "CDS information saved successfully. !!!!", "success", true);
                            }
                            else if ($scope.visibilityControl == (index + '_editCDSCInformation')) {
                                $scope.CDSCInformations[index] = data.CDSCInformation;
                                $rootScope.operateViewAndAddControl(index + '_viewCDSCInformation');
                                messageAlertEngine.callAlertMessage("updatedCDS" + index, "CDS Information Updated Successfully!!!!", "success", true);
                            }
                            else {
                                $scope.CDSCInformations[index] = data.CDSCInformation;
                                $rootScope.operateViewAndAddControl(index + '_viewCDSCInformation');
                                messageAlertEngine.callAlertMessage("renewedCDS" + index, "CDS Information Renewed Successfully!!!!", "success", true);
                            }

                            FormReset($formDataCDSC);
                            $scope.resetDates();
                        } else {
                            $scope.CDSCError = data.status.split(",");
                            messageAlertEngine.callAlertMessage('CDSCError', "", "danger", true);

                        }
                    }
                    catch (e) { };
            //        console.log(data);
                  
                
                },
                error: function (e) {
                    try {
                        $scope.CDSCError = "Sorry for Inconvenience !!!! Please Try Again Later...";
                        messageAlertEngine.callAlertMessage('CDSCError', "", "danger", true);
                    }
                    catch (e) { };
                   
                }
            });
        }
        loadingOff();
    };

    //****************************************Medicare Information********************************************

    $scope.MedicareInformations = [];
    
    // rootScoped on emited value catches the value for the model and insert to get the old data
    $rootScope.$on('MedicareInformations', function (event, val) {
        try {
            $scope.MedicareInformations = val;
            for (var i = 0; i < $scope.MedicareInformations.length ; i++) {
                $scope.MedicareInformations[i].IssueDate = ConvertDateFormat($scope.MedicareInformations[i].IssueDate);
            }
        }
        catch (e) { };
       

    });

    //=============== Medicare Information Conditions ==================
   
  //  $scope.submitButtonText = "Add";
    $scope.IndexValue3 = '';

    //====================== Medicare Information ===================

    $scope.saveMedicareInformation = function (MedicareInformation,index) {
    //    console.log(MedicareInformation);
        loadingOn();
        $scope.IndexValue3 = index;
        var validationStatus;
        var url;
        var $formDataMedicare;
        if ($scope.visibilityControl == 'addMedicareInformation') {
            try {
                $formDataMedicare = $('#newShowMedicareInformationDiv').find('form');
                url = "/Profile/IdentificationAndLicense/AddMedicareInformationAsync?profileId=" + profileId;
            }
            catch (e) { };
            //Add Details - Denote the URL
        
        }
        else if ($scope.visibilityControl == (index + '_editMedicareInformation')) {
            try {
                $formDataMedicare = $('#MedicareInformationEditDiv' + index).find('form');
                url = "/Profile/IdentificationAndLicense/UpdateMedicareInformationAsync?profileId=" + profileId;
            }
            catch (e) { };
            //Update Details - Denote the URL
       
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
                    try {
                        if (data.status == "true") {
                            data.MedicareInformation.IssueDate = ConvertDateFormat(data.MedicareInformation.IssueDate);
                            // data.MedicareInformation.ExpiryDate = ConvertDateFormat(data.MedicareInformation.ExpiryDate);
                            if ($scope.visibilityControl == 'addMedicareInformation') {
                                $scope.MedicareInformations.push(data.MedicareInformation);
                                $rootScope.operateCancelControl('');
                                messageAlertEngine.callAlertMessage("addedMedicare", "Medicare information saved successfully. !!!!", "success", true);
                            }
                            else {
                                $scope.MedicareInformations[index] = data.MedicareInformation;
                                $rootScope.operateViewAndAddControl(index + '_viewMedicareInformation');
                                messageAlertEngine.callAlertMessage("updatedMedicare" + index, "Medicare Information Updated Successfully!!!!", "success", true);
                            }

                            FormReset($formDataMedicare);
                            $scope.resetDates();
                        } else {
                            $scope.MedicareError = data.status.split(",");
                            messageAlertEngine.callAlertMessage('MedicareError', "", "danger", true);

                        }
                    }
                    catch (e) { };
              //      console.log(data);
                  
                  
                },
                error: function (e) {
                    try {
                        $scope.MedicareError = "Sorry for Inconvenience !!!! Please Try Again Later...";
                        messageAlertEngine.callAlertMessage('MedicareError', "", "danger", true);
                    }
                    catch (e) { };
                  
                }
            });
        }
        loadingOff();
    };

    //****************************************Medicaid Information********************************************

    $scope.MedicaidInformations = [];
    
    // rootScoped on emited value catches the value for the model and insert to get the old data
    $rootScope.$on('MedicaidInformations', function (event, val) {
        try {
            $scope.MedicaidInformations = val;
            for (var i = 0; i < $scope.MedicaidInformations.length ; i++) {
                $scope.MedicaidInformations[i].IssueDate = ConvertDateFormat($scope.MedicaidInformations[i].IssueDate);
            }
        }
        catch (e) { };
       
    });

    //=============== Medicaid Information Conditions ==================
   
  //  $scope.submitButtonText = "Add";
    $scope.IndexValue4 = '';
   
    //====================== Medicaid Information ===================

    $scope.saveMedicaidInformation = function (MedicaidInformation,index) {
     //   console.log(MedicaidInformation);

        loadingOn();
        $scope.IndexValue4 = index;
        var validationStatus;
        var url;
        var $formDataMedicaid;
        if ($scope.visibilityControl == 'addMedicaidInformation') {
            try {
                $formDataMedicaid = $('#newShowMedicaidInformationDiv').find('form');
                url = "/Profile/IdentificationAndLicense/AddMedicaidInformationAsync?profileId=" + profileId;
            }
            catch (e) { };
            //Add Details - Denote the URL
          
        }
        else if ($scope.visibilityControl == (index + '_editMedicaidInformation')) {
            try {
                $formDataMedicaid = $('#MedicaidInformationEditDiv' + index).find('form');
                url = "/Profile/IdentificationAndLicense/UpdateMedicaidInformationAsync?profileId=" + profileId;
            }
            catch (e) { };
            //Update Details - Denote the URL
        
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
                    try {
                        if (data.status == "true") {
                            data.MedicaidInformation.IssueDate = ConvertDateFormat(data.MedicaidInformation.IssueDate);
                            // data.MedicaidInformation.ExpiryDate = ConvertDateFormat(data.MedicaidInformation.ExpiryDate);
                            if ($scope.visibilityControl == 'addMedicaidInformation') {
                                $scope.MedicaidInformations.push(data.MedicaidInformation);
                                $rootScope.operateCancelControl('');
                                messageAlertEngine.callAlertMessage("addedMedicaid", "Medicaid information saved successfully. !!!!", "success", true);
                            }
                            else {
                                $scope.MedicaidInformations[index] = data.MedicaidInformation;
                                $rootScope.operateViewAndAddControl(index + '_viewMedicaidInformation');
                                messageAlertEngine.callAlertMessage("updatedMedicaid" + index, "Medicaid Information Updated Successfully. !!!!", "success", true);
                            }

                            FormReset($formDataMedicaid);
                            $scope.resetDates();
                        } else {
                            $scope.MedicaidError = data.status.split(",");
                            messageAlertEngine.callAlertMessage('MedicaidError', "", "danger", true);
                        }

                    }
                    catch (e) { };
                //    console.log(data);
                   
                },
                error: function (e) {
                    try {
                        $scope.MedicaidError = "Sorry for Inconvenience !!!! Please Try Again Later...";
                        messageAlertEngine.callAlertMessage('MedicaidError', "", "danger", true);
                    }
                    catch (e) { };
                   
                }
            });
        }
        loadingOff();
    };

    //****************************************Other Identification Numbers********************************************

    $scope.OtherIdentificationNumbers ={};

    // rootScoped on emited value catches the value for the model and insert to get the old data
    $rootScope.$on('OtherIdentificationNumber', function (event, val) {
        try {
            $scope.OtherIdentificationNumbers = val;
        }
        catch (e) { };
     //   console.log(val)
     
    });

    $scope.clearCAQHCredentials = function (value) {
        try {
            if (value == "") {
                $scope.tempObject.CAQHUserName = "";
                $scope.tempObject.CAQHPassword = "";

            }
        }
        catch (e) { };
       
    };

  

    $scope.showOtherId = false;
    $scope.OtherIdError = '';
    //====================== Other Identification Numbers ===================

    $scope.saveOtherIdentificationNumber = function (Form_Id,otherIdentificationNumber) {
        loadingOn();
        if (otherIdentificationNumber.OtherIdentificationNumberID != 0) {
            $scope.typeOfSave = "Edit";
        } else {
            $scope.typeOfSave = "Add";
        }

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
                    try {
                        if (data.status == "true") {

                            otherIdentificationNumber.OtherIdentificationNumberID = data.OtherIdentificationNumber.OtherIdentificationNumberID;
                            $scope.OtherIdentificationNumbers = angular.copy(otherIdentificationNumber);
                            FormReset($("#" + Form_Id));
                            $scope.ErrorId = false;
                            if ($scope.typeOfSave == "Add") {
                                messageAlertEngine.callAlertMessage("addedID", "Other Identification information saved successfully. !!!!", "success", true);
                            } else {
                                messageAlertEngine.callAlertMessage("updatedID", "Other Identification information updated successfully. !!!!", "success", true);
                            }
                            $rootScope.visibilityControl = '';

                        } else {
                            $scope.OtherIdError = data.status.split(",");
                            messageAlertEngine.callAlertMessage('OtherIdError', "", "danger", true);



                        }
                    }
                    catch (e) { };
               //     console.log(data);
                  
                
                },
                error: function (e) {
                    try {
                        $scope.OtherIdError = "Sorry for Inconvenience !!!! Please Try Again Later...";
                        messageAlertEngine.callAlertMessage('OtherIdError', "", "danger", true);
                    }
                    catch (e) { };
                   

                }
            });
        }
        loadingOff();
    };

    $scope.resetDates = function () {
        try
        {
        $scope.tempObject.IssueDate = new Date();
        $scope.tempObject.ExpiryDate = new Date();
        $scope.tempObject.CurrentIssueDate = new Date();
        }
        catch (e)
        { }
    };
}]);
