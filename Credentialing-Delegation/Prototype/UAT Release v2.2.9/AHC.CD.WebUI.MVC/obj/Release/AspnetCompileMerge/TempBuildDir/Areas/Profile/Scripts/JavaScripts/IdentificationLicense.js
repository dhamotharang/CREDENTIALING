
//=========================== Controller declaration ==========================

profileApp.controller('identificationLicenseController', ['$scope', '$rootScope', '$http', 'masterDataService', 'locationService', 'messageAlertEngine', '$filter', function ($scope, $rootScope, $http, masterDataService, locationService, messageAlertEngine, $filter) {
    
    //=============================Get Master Data===========
        //=======Provider Types for state license

    //show renew div
    //StateLicense
    $scope.ShowRenewDivStateLicense = false;
    $scope.RenewDivSL = function (StateLicenseInformation) {
        if (StateLicenseInformation.ExpiryDate == null)
        { $scope.ShowRenewDivStateLicense = false; }
        else
        {
            $scope.ShowRenewDivStateLicense = true;
        }
    };
    //DEA
    $scope.ShowRenewDivDEA = false;
    $scope.RenewDivDEA = function (DEAInformation) {
        if (DEAInformation.ExpiryDate == null)
        { $scope.ShowRenewDivDEA = false; }
        else
        {
            $scope.ShowRenewDivDEA = true;
        }
    };
    //CDS
    $scope.ShowRenewDivCDS = false;
    $scope.RenewDivCDS = function (CDSCInformation) {
        if (CDSCInformation.ExpiryDate == null)
        { $scope.ShowRenewDivCDS = false; }
        else
        {
            $scope.ShowRenewDivCDS = true;
        }
    };

    $rootScope.$on("LoadRequireMasterDataIdentificationLicenses", function () {
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


        try {
            $http.get("/Location/GetStates")     //locationService makes an ajax call to fetch all the cities which have relevent names as the query string.
       .then(function (response) {
           //  console.log(response.data);
           $scope.States = response.data;
       });
        }
        catch (e) { };
       

    });
  
    $scope.hideDiv = function () {
        $('.ProviderTypeSelectAutoList1').hide();
        $('.ProviderTypeSelectAutoList').hide();
  }
    //====================== State License ===================

    $scope.StateLicenses = [];

    $scope.showStates = function (event) {
        $(event.target).parent().find(".ProviderTypeSelectAutoList").first().show();
    };
    // rootScoped on emitted value catches the value for the model and insert to get the old data
    //calling the method using $on(PSP-public subscriber pattern)
    $rootScope.$on('StateLicenses', function (event, val) {
        try {
            $scope.StateLicenses = val;
            for (var i = 0; i < $scope.StateLicenses.length ; i++) {
                if (!$scope.StateLicenses[i].ProviderTypeID) { $scope.StateLicenses[i].ProviderTypeID = ""; }
                if (!$scope.StateLicenses[i].StateLicenseStatusID) { $scope.StateLicenses[i].StateLicenseStatusID = ""; }
                if (!$scope.StateLicenses[i].IssueState) { $scope.StateLicenses[i].IssueState = ""; }
            }
        }
        catch (e) { };
       
     //   console.log(val);
    });

    $scope.IndexValue1 = '';


    //=================Delete for Federal Dea=====================//

    $scope.initFederalDEAWarning = function (FederalDea) {

        if (angular.isObject(FederalDea)) {
            $scope.tempReference = FederalDea;
        }        
        console.log(FederalDea);
        $('#federalDeaWarningModal').modal();
    } 

        
        $scope.removeFederalDEAInformation = function (FederalDea) {
            var validationStatus = false;
            var url = null;
            var myData = {};
            var $formData = null;
            $formData = $('#editFederalDea');
            url = "/Profile/IdentificationAndLicense/RemoveFederalDEALicense?profileId=" + profileId;
            ResetFormForValidation($formData);
            console.log($formData);
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
                        //console.log(data.status);
                        if (data.status == "true") {
                            var obj = $filter('filter')(FederalDea, { FederalDEAInformationID: data.federalDea.FederalDEAInformationID })[0];
                            $scope.FederalDEA.splice($scope.FederalDEA.indexOf(obj), 1);
                            if ($scope.dataFetchedFederalDEA == true) {
                                obj.HistoryStatus = 'Deleted';
                                obj.DEAScheduleInfoHistory = angular.copy(obj.DEAScheduleInfoes);
                                $scope.FederalDEAHistory.push(obj);
                            }
                            $('#federalDeaWarningModal').modal('hide');
                            $rootScope.operateCancelControl('');
                            myData = data;
                            messageAlertEngine.callAlertMessage("addedDEA", "Federal DEA Removed successfully.", "success", true);
                        } else {
                            $('#federalDeaWarningModal').modal('hide');
                            messageAlertEngine.callAlertMessage("DEAError", data.status, "danger", true);
                        }
                    },
                    error: function (e) {

                    }
                });
            }

            $rootScope.$broadcast('RemoveFederalDEAInformation', myData);

        };


    //------------------Remove Medicaid------------------//
        $scope.initMedicaidInformation = function (MedicaidInfo) {

            if (angular.isObject(MedicaidInfo)) {
                $scope.tempReference = MedicaidInfo;

            }
            $('#medicaidWarningModal').modal();
        }


        $scope.removeMedicaidInformation = function (MedicaidInfos) {
            var validationStatus = false;
            var url = null;
            var myData = {};
            var $formData = null;
            $formData = $('#editMedicaidInfo');
            url = "/Profile/IdentificationAndLicense/RemoveMedicaidInformation?profileId=" + profileId;
            ResetFormForValidation($formData);
            console.log($formData);
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
                        //console.log(data.status);
                        if (data.status == "true") {
                            var obj = $filter('filter')(MedicaidInfos, { MedicaidInformationID: data.MedicaidInfo.MedicaidInformationID })[0];
                            $scope.MedicaidInformations.splice($scope.MedicaidInformations.indexOf(obj), 1);
                            if ($scope.dataFetchedMedicaid == true) {
                                obj.HistoryStatus = 'Deleted';
                                $scope.MedicaidInformationsHistory.push(obj);
                            }
                            $('#medicaidWarningModal').modal('hide');
                            $rootScope.operateCancelControl('');
                            myData = data;
                            messageAlertEngine.callAlertMessage("addedMedicaid", "Medicaid Information Removed successfully.", "success", true);
                        } else {
                            $('#medicaidWarningModal').modal('hide');
                            messageAlertEngine.callAlertMessage("MedicaidError", data.status, "danger", true);
                        }
                    },
                    error: function (e) {

                    }
                });
            }

            $rootScope.$broadcast('RemoveMedicaidInformation', myData);

        };


    //--------------------------End----------------------//


    //--------------------Remove Medicare--------------------//

        $scope.initMedicareInformation = function (MedicareInfo) {

            if (angular.isObject(MedicareInfo)) {
                $scope.tempReference = MedicareInfo;

            }
            $('#medicareWarningModal').modal();
        }


        $scope.removeMedicareInformation = function (MedicareInfos) {
            var validationStatus = false;
            var url = null;
            var myData = {};
            var $formData = null;
            $formData = $('#editMedicareInfo');
            url = "/Profile/IdentificationAndLicense/RemoveMedicareInformation?profileId=" + profileId;
            ResetFormForValidation($formData);
            console.log($formData);
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
                        //console.log(data.status);
                        if (data.status == "true") {
                            var obj = $filter('filter')(MedicareInfos, { MedicareInformationID: data.MedicareInfo.MedicareInformationID })[0];
                            $scope.MedicareInformations.splice($scope.MedicareInformations.indexOf(obj), 1);
                            if ($scope.dataFetchedMedicare == true) {
                                obj.HistoryStatus = 'Deleted';
                                $scope.MedicareInformationsHistory.push(obj);
                            }
                            $('#medicareWarningModal').modal('hide');
                            $rootScope.operateCancelControl('');
                            myData = data;
                            messageAlertEngine.callAlertMessage("addedMedicare", "Medicare Information Removed successfully.", "success", true);
                        } else {
                            $('#medicareWarningModal').modal('hide');
                            messageAlertEngine.callAlertMessage("MedicareError", data.status, "danger", true);
                        }
                    },
                    error: function (e) {

                    }
                });
            }

            $rootScope.$broadcast('RemoveMedicareInformation', myData);

        };

    //--------------------End--------------------------------//


    //------------SAVE--------------
    $scope.saveStateLicense = function (stateLicense, index) {

        //loadingOn();
      //  console.log(stateLicense);
        var validationStatus;
        var url;
        var myData = {};
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
            catch (e)
            { };
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
                            myData = data;

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
                            else {
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
                    catch (e) { };
                    

                 }
             
            });
        }
        $rootScope.$broadcast('UpdateStateLicenses', myData);
        //loadingOff();
    };

    //To initiate Removal Confirmation Modal
    $scope.initStateLicenseWarning = function (StateLicenseInformation) {
        if (angular.isObject(StateLicenseInformation)) {
            $scope.tempStateLicense = StateLicenseInformation;
        }
        $('#stateLicenseWarningModal').modal();
    };

    $scope.removeStateLicense = function (StateLicenses) {
        var validationStatus = false;
        var url = null;
        var myData = {};
        var $formData = null;
        $formData = $('#removeStateLicense');
        url = "/Profile/IdentificationAndLicense/RemoveStateLicenseAsync?profileId=" + profileId;
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
                        var obj = $filter('filter')(StateLicenses, { StateLicenseInformationID: data.stateLicense.StateLicenseInformationID })[0];
                        StateLicenses.splice(StateLicenses.indexOf(obj), 1);
                        if ($scope.dataFetched == true) {
                            obj.HistoryStatus = 'Deleted';
                            $scope.StateLicensesHistory.push(obj);
                        }
                        $('#stateLicenseWarningModal').modal('hide');
                        $rootScope.operateCancelControl('');
                        myData = data;
                        messageAlertEngine.callAlertMessage("addedStateLicense", "State License Removed successfully.", "success", true);
                    } else {
                        $('#stateLicenseWarningModal').modal('hide');
                        messageAlertEngine.callAlertMessage("removeStateLicense", data.status, "danger", true);
                        $scope.errorStateLicense = "Sorry for Inconvenience !!!! Please Try Again Later...";
                    }
                },
                error: function (e) {

                }
            });
        }

        $rootScope.$broadcast('RemoveStateLicenses', myData);
    };
    
    //....................State Licenses History............................//
    $scope.StateLicensesHistory = [];
    $scope.dataFetched = false;

    $scope.showStateLicenseHistory = function (loadingId) {
        if ($scope.StateLicensesHistory.length == 0) {
            $("#" + loadingId).css('display', 'block');
            var url = "/Profile/ProfileHistory/GetAllStateLicensesHistory?profileId=" + profileId;
            $http.get(url).success(function (data) {
                $scope.StateLicensesHistory = data;
                $scope.dataFetched = true;
                for (var i = 0; i < $scope.StateLicensesHistory.length; i++) {
                    if ($scope.StateLicensesHistory[i].HistoryStatus == '' || !$scope.StateLicensesHistory[i].HistoryStatus) {
                        $scope.StateLicensesHistory[i].HistoryStatus = 'Renewed';
                    }
                }
                $scope.showStateLicenseHistoryTable = true;
                $("#" + loadingId).css('display', 'none');
            });
        }

        else {
            $scope.showStateLicenseHistoryTable = true;
        }
    }

    $scope.cancelStateLicenseHistory = function () {
        $scope.showStateLicenseHistoryTable = false;
    }
    
    //****************************************Federal DEA Info********************************************
    //=============== Federal DEA License Conditions ==================

   // $scope.submitButtonText = "Add";
    

    //=================Data============
    $rootScope.$on("LoadRequireMasterDataIdentificationLicenses", function () {
    masterDataService.getMasterData("/Profile/MasterData/GetAllDEASchedules").then(function (Schedules) {
        try {
            $scope.Schedules = Schedules;
        }
        catch (e) { };
    
    });
    });
   
    $scope.FederalDEA = [];


    $scope.resetScheduleOption = function () {
        for (var schedule in $rootScope.tempObject.DEAScheduleInfoes) {
            schedule.YesNoOption = 2;
        }
    };
   
    // rootScoped on emited value catches the value for the model and insert to get the old data
    $rootScope.$on('FederalDEAInformations', function (event, val) {
        try {
            $scope.FederalDEA = val;
        }
        catch (e) { };
       
    });

    $scope.IndexValue2 = '';
    //====================== Federal DEA License Save ===================


    $scope.saveFederalDEALicense = function (FederalDEALicense, index) {
        //loadingOn();
     //   console.log(FederalDEALicense);
        $scope.IndexValue2 = index;
        var myData = {};
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
                            myData = data;
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
                            else {
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

        $rootScope.$broadcast('UpdateFederalDEAInformation', myData);

        //loadingOff();
    };

    //....................Federal DEA History............................//
    $scope.FederalDEAHistory = [];
    $scope.dataFetchedFederalDEA = false;

    $scope.showFederalDEALicenseHistory = function (loadingId) {
        if ($scope.FederalDEAHistory.length == 0) {
            $("#" + loadingId).css('display', 'block');
            var url = "/Profile/ProfileHistory/GetAllFederalDEALicensesHistory?profileId=" + profileId;
            $http.get(url).success(function (data) {
                $scope.FederalDEAHistory = data;
                console.log($scope.FederalDEAHistory);
                $scope.dataFetchedFederalDEA = true;
                for (var i = 0; i < $scope.FederalDEAHistory.length; i++) {
                    if ($scope.FederalDEAHistory[i].HistoryStatus == '' || !$scope.FederalDEAHistory[i].HistoryStatus) {
                        $scope.FederalDEAHistory[i].HistoryStatus = 'Renewed';
                        
                    }
                }
                $scope.showFederalDEALicenseHistoryTable = true;
                $("#" + loadingId).css('display', 'none');

                //console.log($scope.FederalDEAHistory);
            });
        }
        else {
            $scope.showFederalDEALicenseHistoryTable = true;
        }
    }

    $scope.cancelFederalDEALicenseHistory = function () {
        $scope.showFederalDEALicenseHistoryTable = false;
    }

    //****************************************CDSC Information********************************************

    $scope.CDSCInformations = [];
   
    // rootScoped on emited value catches the value for the model and insert to get the old data
    $rootScope.$on('CDSCInformations', function (event, val) {
        try {
            $scope.CDSCInformations = val;
            //for (var i = 0; i < $scope.CDSCInformations.length ; i++) {
            //    $scope.CDSCInformations[i].IssueDate = ConvertDateFormat($scope.CDSCInformations[i].IssueDate);
            //    $scope.CDSCInformations[i].ExpiryDate = ConvertDateFormat($scope.CDSCInformations[i].ExpiryDate);;
            //}
        }
        catch (e) { };
      

    });

    //=============== CDSC Information Conditions ==================
   
 //   $scope.submitButtonText = "Add";
   
    $scope.CDSCError = '';
    $scope.IndexValue5 = '';
    //====================== CDSC Information Save===================

    $scope.saveCDSCInformation = function (cDSCInformation, index) {
        //loadingOn();
     //   console.log(cDSCInformation);
        $scope.IndexValue5 = index;

        var validationStatus;
        var url;
        var myData = {};
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
                            myData = data;
                            FormReset($formDataCDSC);
                            $scope.resetDates();
                        } else {
                            $scope.CDSCError = data.status.split(",");
                            messageAlertEngine.callAlertMessage('CDSCError', "", "danger", true);

                        }
                    }
                    catch (e) { };                
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

        $rootScope.$broadcast('UpdateCDSCInformation', myData);

        //loadingOff();
    };

    //To initiate Removal Confirmation Modal
    $scope.initCDSCWarning = function (CDSCInformation) {
        if (angular.isObject(CDSCInformation)) {
            $scope.tempCDSC = CDSCInformation;
        }
        $('#cDSCWarningModal').modal();
    };

    $scope.removeCDSC = function (CDSCInformations) {
        var validationStatus = false;
        var url = null;
        var myData = {};
        var $formData = null;
        $formData = $('#removeCDSC');
        url = "/Profile/IdentificationAndLicense/RemoveCDSCLicenseAsync?profileId=" + profileId;
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
                        var obj = $filter('filter')(CDSCInformations, { CDSCInformationID: data.cDSCInformation.CDSCInformationID })[0];
                        CDSCInformations.splice(CDSCInformations.indexOf(obj), 1);
                        if ($scope.dataFetchedCDSC) {
                            obj.HistoryStatus = 'Deleted';
                            $scope.CDSCInformationsHistory.push(obj);
                        }
                        $('#cDSCWarningModal').modal('hide');
                        $rootScope.operateCancelControl('');
                        myData = data;
                        messageAlertEngine.callAlertMessage("addedCDS", "CDS Information Removed successfully.", "success", true);
                    } else {
                        $('#cDSCWarningModal').modal('hide');
                        messageAlertEngine.callAlertMessage("removeCDS", data.status, "danger", true);
                        $scope.errorCDS = "Sorry for Inconvenience !!!! Please Try Again Later...";
                    }
                },
                error: function (e) {

                }
            });
        }

        $rootScope.$broadcast('RemoveCDSCInformation', myData);

    };

    //....................CDSC History............................//
    $scope.CDSCInformationsHistory = [];
    $scope.dataFetchedCDSC = false;

    $scope.showCDSCHistory = function (loadingId) {
        if ($scope.CDSCInformationsHistory.length == 0) {
            $("#" + loadingId).css('display', 'block');
            var url = "/Profile/ProfileHistory/GetAllCDSCInformationHistory?profileId=" + profileId;
            $http.get(url).success(function (data) {
                $scope.CDSCInformationsHistory = data;
                $scope.dataFetchedCDSC = true;
                for (var i = 0; i < $scope.CDSCInformationsHistory.length; i++) {
                    if ($scope.CDSCInformationsHistory[i].HistoryStatus == '' || !$scope.CDSCInformationsHistory[i].HistoryStatus) {
                        $scope.CDSCInformationsHistory[i].HistoryStatus = 'Renewed';
                    }
                }
                $scope.showCDSCInformationHistoryTable = true;
                $("#" + loadingId).css('display', 'none');

            });
        }
        else {
            $scope.showCDSCInformationHistoryTable = true;
        }
    }

    $scope.cancelCDSCHistory = function () {
        $scope.showCDSCInformationHistoryTable = false;
    }

    //****************************************Medicare Information********************************************

    $scope.MedicareInformations = [];
    
    // rootScoped on emited value catches the value for the model and insert to get the old data
    $rootScope.$on('MedicareInformations', function (event, val) {
        try {
            $scope.MedicareInformations = val;
        }
        catch (e) { };
       

    });

    //=============== Medicare Information Conditions ==================
   
    $scope.IndexValue3 = '';

    //====================== Medicare Information ===================

    $scope.saveMedicareInformation = function (MedicareInformation, index) {
        //loadingOn();
        $scope.IndexValue3 = index;
        var validationStatus;
        var url;
        var myData = {};
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
                            myData = data;
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

        $rootScope.$broadcast('UpdateMedicareInformation', myData);

        //loadingOff();
    };

    //....................Medicare History............................//
    $scope.MedicareInformationsHistory = [];
    $scope.dataFetchedMedicare = false;

    $scope.showMedicareHistory = function (loadingId) {
        if ($scope.MedicareInformationsHistory.length == 0) {
            $("#" + loadingId).css('display', 'block');
            var url = "/Profile/ProfileHistory/GetAllMedicareInformationHistory?profileId=" + profileId;
            $http.get(url).success(function (data) {
                $scope.MedicareInformationsHistory = data;
                $scope.dataFetchedMedicare = true;
                for (var i = 0; i < $scope.MedicareInformationsHistory.length; i++) {
                    if ($scope.MedicareInformationsHistory[i].HistoryStatus == '' || !$scope.MedicareInformationsHistory[i].HistoryStatus) {
                        $scope.MedicareInformationsHistory[i].HistoryStatus = 'Renewed';
                    }
                }
                $scope.showMedicareHistoryTable = true;
                $("#" + loadingId).css('display', 'none');

            });
        }
        else {
            $scope.showMedicareHistoryTable = true;
        }
    }

    $scope.cancelMedicareHistory = function () {
        $scope.showMedicareHistoryTable = false;
    }

    //****************************************Medicaid Information********************************************

    $scope.MedicaidInformations = [];
    
    // rootScoped on emited value catches the value for the model and insert to get the old data
    $rootScope.$on('MedicaidInformations', function (event, val) {
        try {
            $scope.MedicaidInformations = val;
            //for (var i = 0; i < $scope.MedicaidInformations.length ; i++) {
            //    $scope.MedicaidInformations[i].IssueDate = ConvertDateFormat($scope.MedicaidInformations[i].IssueDate);
            //}
        }
        catch (e) { };
       
    });

    //=============== Medicaid Information Conditions ==================
   
  //  $scope.submitButtonText = "Add";
    $scope.IndexValue4 = '';
   
    //====================== Medicaid Information ===================

    $scope.saveMedicaidInformation = function (MedicaidInformation, index) {
     //   console.log(MedicaidInformation);

        //loadingOn();
        $scope.IndexValue4 = index;
        var validationStatus;
        var url;
        var myData = {};
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
                            myData = data;
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

        $rootScope.$broadcast('UpdateMedicaidInformation', myData);

        //loadingOff();
    };

    //....................Medicaid History............................//
    $scope.MedicaidInformationsHistory = [];
    $scope.dataFetchedMedicaid = false;

    $scope.showMedicaidHistory = function (loadingId) {
        if ($scope.MedicaidInformationsHistory.length == 0) {
            $("#" + loadingId).css('display', 'block');
            var url = "/Profile/ProfileHistory/GetAllMedicaidInformationHistory?profileId=" + profileId;
            $http.get(url).success(function (data) {
                $scope.MedicaidInformationsHistory = data;
                $scope.dataFetchedMedicaid = true;
                for (var i = 0; i < $scope.MedicaidInformationsHistory.length; i++) {
                    if ($scope.MedicaidInformationsHistory[i].HistoryStatus == '' || !$scope.MedicaidInformationsHistory[i].HistoryStatus) {
                        $scope.MedicaidInformationsHistory[i].HistoryStatus = 'Renewed';
                    }
                }
                $scope.showMedicaidHistoryTable = true;
                $("#" + loadingId).css('display', 'none');

            });
        }
        else {
            $scope.showMedicaidHistoryTable = true;
        }
    }

    $scope.cancelMedicaidHistory = function () {
        $scope.showMedicaidHistoryTable = false;
    }

    //****************************************Other Identification Numbers********************************************

    $scope.OtherIdentificationNumbers = {};

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

    $scope.saveOtherIdentificationNumber = function (Form_Id, otherIdentificationNumber) {
    //    //loadingOn();
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
                          //  $scope.OtherIdentificationNumbers = data.OtherIdentificationNumber;
                           // FormReset($("#" + Form_Id));
                            $rootScope.tempObject = {};
                           
                            if ($scope.typeOfSave == "Add") {
                                messageAlertEngine.callAlertMessage("addedID", "Other Identification information saved successfully. !!!!", "success", true);
                            }
                            else {
                                messageAlertEngine.callAlertMessage("updatedID", "Other Identification information updated successfully. !!!!", "success", true);
                            }
                            $rootScope.visibilityControl = '';

                        }
                        else {
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
      //  //loadingOff();
    };

    $scope.resetDates = function () {
        try {
        $scope.tempObject.IssueDate = new Date();
        $scope.tempObject.ExpiryDate = new Date();
        $scope.tempObject.CurrentIssueDate = new Date();
        }
        catch (e)
        { }
    };

    $rootScope.IdentificationLicensesLoaded = true;
    $scope.dataLoaded = false;
    $rootScope.$on('IdentificationLicenses', function () {
        if (!$scope.dataLoaded) {
            $rootScope.IdentificationLicensesLoaded = false;
            //var data = JSON.parse(identificationLicenses);
            //try {            
            //    for (key in data) {
            //        //console.log(key);
            //        $rootScope.$emit(key, data[key]);
            //        //call respective controller to load data (PSP)
            //    }
            //    $rootScope.IdentificationLicensesLoaded = true;
            //    //$rootScope.$broadcast("LoadRequireMasterData");
            //} catch (e) {
            //    //console.log("error getting data back");
            //    $rootScope.IdentificationLicensesLoaded = true;
            //}
            //console.log("Getting data....");
            $http({
                method: 'GET',
                url: '/Profile/MasterProfile/GetIdentificationAndLicensesProfileDataAsync?profileId=' + profileId
            }).success(function (data, status, headers, config) {
                //console.log(data);
        try {            
            for (key in data) {
                //console.log(key);
                $rootScope.$emit(key, data[key]);
                //call respective controller to load data (PSP)
            }
            $rootScope.IdentificationLicensesLoaded = true;
                    $rootScope.$broadcast("LoadRequireMasterDataIdentificationLicenses");
        } catch (e) {
            //console.log("error getting data back");
            $rootScope.IdentificationLicensesLoaded = true;
        }

            }).error(function (data, status, headers, config) {
                //console.log(status);
                $rootScope.IdentificationLicensesLoaded = true;
            });
            $scope.dataLoaded = true;
        }
    });

}]);

//============l=end==========================================//