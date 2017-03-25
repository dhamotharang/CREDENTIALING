
//=========================== Controller declaration ==========================

profileApp.controller('identificationLicenseController', ['$scope', '$rootScope', '$http', 'masterDataService', 'locationService', 'messageAlertEngine', '$filter', '$timeout', 'profileUpdates', function ($scope, $rootScope, $http, masterDataService, locationService, messageAlertEngine, $filter, $timeout, profileUpdates) {


    $scope.StateLicensePendingRequest = profileUpdates.getUpdates('Identification And License', 'State License');
    $scope.FederalDEAPendingRequest = profileUpdates.getUpdates('Identification And License', 'Federal DEA');
    $scope.MedicaidInformationPendingRequest = profileUpdates.getUpdates('Identification And License', 'Medicaid Information');
    $scope.MedicareInformationPendingRequest = profileUpdates.getUpdates('Identification And License', 'Medicare Information');
    $scope.CDSInformationPendingRequest = profileUpdates.getUpdates('Identification And License', 'CDS Information');
    $scope.OtherIdentificationNumbersPendingRequest = profileUpdates.getUpdates('Identification And License', 'Other Identification Number');

    //=============================Get Master Data===========
    //=======Provider Types for state license

    //show renew div
    //StateLicense

    $scope.setFiles = function (file) {
        $(file).parent().parent().find(".jancyFileWrapTexts").find("span").width($(file).parent().parent().width() < 243 ? $(file).parent().parent().width() : 243);

    }

    $scope.ShowRenewDivStateLicense = false;
    $scope.RenewDivSL = function (StateLicenseInformation) {
        if (StateLicenseInformation.ExpiryDate == null) {
            $scope.ShowRenewDivStateLicense = false;
            messageAlertEngine.callAlertMessage("StateLicenseError1", "Renewal cannot be initiated.Expiry Date is missing.", "danger", true);
            $rootScope.visibilityControl = '';
        }
        else {
            $scope.ShowRenewDivStateLicense = true;
        }
    };
    //DEA
    $scope.ShowRenewDivDEA = false;
    $scope.RenewDivDEA = function (DEAInformation) {
        if (DEAInformation.ExpiryDate == null) {
            $scope.ShowRenewDivDEA = false;
            messageAlertEngine.callAlertMessage("DEAError1", "Renewal cannot be initiated.Expiry Date is missing.", "danger", true);
            $rootScope.visibilityControl = '';
        }
        else {
            $scope.ShowRenewDivDEA = true;
        }

        //$timeout(function () {
        //    $rootScope.visibilityControl = "";
        //}, 2000);

    };
    //CDS
    $scope.ShowRenewDivCDS = false;
    $scope.RenewDivCDS = function (CDSCInformation) {
        if (CDSCInformation.ExpiryDate == null) {
            $scope.ShowRenewDivCDS = false;
            messageAlertEngine.callAlertMessage("CDSError1", "Renewal cannot be initiated.Expiry Date is missing.", "danger", true);
            $rootScope.visibilityControl = '';
        }
        else {
            $scope.ShowRenewDivCDS = true;
        }
    };

    $rootScope.$on("LoadRequireMasterDataIdentificationLicenses", function () {
        masterDataService.getMasterData(rootDir + "/Profile/MasterData/GetAllProviderTypes").then(function (Providertypes) {
            try {
                $scope.ProviderTypes = Providertypes;
            }
            catch (e) { };
        });

        //=======================State License Status
        masterDataService.getMasterData(rootDir + "/Profile/MasterData/GetAllLicenseStatus").then(function (LicenseStatuses) {
            try {
                $scope.StateLicenseStatuses = LicenseStatuses;
            }
            catch (e) { };

        });

        //==============================States


        try {
            $http.get(rootDir + "/Location/GetStates")     //locationService makes an ajax call to fetch all the cities which have relevent names as the query string.
       .then(function (response) {

           $scope.States = response.data;
       });
        }
        catch (e) { };


    });

    $scope.hideDiv = function () {
        $('.ProviderTypeSelectAutoList1').hide();
        $('.ProviderTypeSelectAutoList').hide();
        $scope.Errormessage = '';
    }

    $scope.HideErrorMessages = function () {
        $scope.ErrormessageforCDSstate = false;
        $scope.Errormessage = '';
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


    });

    $scope.IndexValue1 = '';





    //=================Delete for Federal Dea=====================//

    $scope.initFederalDEAWarning = function (FederalDea) {

        if (angular.isObject(FederalDea)) {
            $scope.tempReference = FederalDea;
        }

        $('#federalDeaWarningModal').modal();
    }


    $scope.removeFederalDEAInformation = function (FederalDea) {
        var validationStatus = false;
        var url = null;
        var myData = {};
        var $formData = null;
        $scope.isRemoved = true;
        $formData = $('#editFederalDea');
        url = rootDir + "/Profile/IdentificationAndLicense/RemoveFederalDEALicense?profileId=" + profileId;
        ResetFormForValidation($formData);

        validationStatus = $formData.valid();

        if (validationStatus) {
            //Simple POST request example (passing data) :
            $.ajax({
                url: url,
                type: 'POST',
                data: new FormData($formData[0]),
                cache: false,
                contentType: false,
                processData: false,
                success: function (data) {
                    try {

                        if (data.status == "true") {
                            var obj = $filter('filter')(FederalDea, { FederalDEAInformationID: data.federalDea.FederalDEAInformationID })[0];
                            $scope.FederalDEA.splice($scope.FederalDEA.indexOf(obj), 1);
                            if ($scope.dataFetchedFederalDEA == true) {
                                obj.FederalDEADocumentPath = data.federalDea.DEALicenceCertPath;
                                obj.HistoryStatus = 'Deleted';
                                obj.DeletedBy = data.UserName;
                                obj.DeletedDate = moment(new Date).format('MM/DD/YYYY, h:mm:ss a');
                                obj.DEAScheduleInfoHistory = angular.copy(obj.DEAScheduleInfoes);
                                $scope.FederalDEAHistory.push(obj);
                            }
                            $scope.isRemoved = false;
                            $('#federalDeaWarningModal').modal('hide');
                            $rootScope.operateCancelControl('');
                            myData = data;
                            messageAlertEngine.callAlertMessage("addedDEA", "Federal DEA Removed successfully.", "success", true);
                        } else {
                            $('#federalDeaWarningModal').modal('hide');
                            messageAlertEngine.callAlertMessage("DEAError", data.status, "danger", true);
                        }
                    } catch (e) {

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

    $scope.Statechange = function (state) {
        $scope.tempObject.State = state;

    }
    $scope.removeMedicaidInformation = function (MedicaidInfos) {
        var validationStatus = false;
        var url = null;
        var myData = {};
        var $formData = null;
        $scope.isRemoved = true;
        $formData = $('#editMedicaidInfo');
        url = rootDir + "/Profile/IdentificationAndLicense/RemoveMedicaidInformation?profileId=" + profileId;
        ResetFormForValidation($formData);

        validationStatus = $formData.valid();

        if (validationStatus) {
            //Simple POST request example (passing data) :
            $.ajax({
                url: url,
                type: 'POST',
                data: new FormData($formData[0]),
                cache: false,
                contentType: false,
                processData: false,
                success: function (data) {

                    try {
                        if (data.status == "true") {
                            var obj = $filter('filter')(MedicaidInfos, { MedicaidInformationID: data.MedicaidInfo.MedicaidInformationID })[0];
                            $scope.MedicaidInformations.splice($scope.MedicaidInformations.indexOf(obj), 1);
                            if ($scope.dataFetchedMedicaid == true) {
                                obj.HistoryStatus = 'Deleted';
                                obj.DeletedBy = data.UserName;
                                obj.DeletedDate = moment(new Date).format('MM/DD/YYYY, h:mm:ss a');
                                if(obj.State != 'state')
                                $scope.MedicaidInformationsHistory.push(obj);
                            }
                            $scope.isRemoved = false;
                            $('#medicaidWarningModal').modal('hide');
                            $rootScope.operateCancelControl('');
                            myData = data;
                            messageAlertEngine.callAlertMessage("addedMedicaid", "Medicaid Information Removed successfully.", "success", true);
                        } else {
                            $('#medicaidWarningModal').modal('hide');
                            messageAlertEngine.callAlertMessage("MedicaidError", data.status, "danger", true);
                        }
                    } catch (e) {

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
        $scope.isRemoved = true;
        $formData = $('#editMedicareInfo');
        url = rootDir + "/Profile/IdentificationAndLicense/RemoveMedicareInformation?profileId=" + profileId;
        ResetFormForValidation($formData);

        validationStatus = $formData.valid();

        if (validationStatus) {
            //Simple POST request example (passing data) :
            $.ajax({
                url: url,
                type: 'POST',
                data: new FormData($formData[0]),
                cache: false,
                contentType: false,
                processData: false,
                success: function (data) {

                    try {
                        if (data.status == "true") {
                            var obj = $filter('filter')(MedicareInfos, { MedicareInformationID: data.MedicareInfo.MedicareInformationID })[0];
                            $scope.MedicareInformations.splice($scope.MedicareInformations.indexOf(obj), 1);
                            if ($scope.dataFetchedMedicare == true) {
                                obj.HistoryStatus = 'Deleted';
                                obj.DeletedBy = data.UserName;
                                obj.DeletedDate = moment(new Date).format('MM/DD/YYYY, h:mm:ss a');
                                $scope.MedicareInformationsHistory.push(obj);
                            }
                            $timeout(function () {
                                $scope.isRemoved = false;
                            }, 5000);
                            $('#medicareWarningModal').modal('hide');
                            $rootScope.operateCancelControl('');
                            myData = data;
                            messageAlertEngine.callAlertMessage("addedMedicare", "Medicare Information Removed successfully.", "success", true);
                        } else {
                            $('#medicareWarningModal').modal('hide');
                            messageAlertEngine.callAlertMessage("MedicareError", data.status, "danger", true);
                        }
                    } catch (e) {

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
                url = rootDir + "/Profile/IdentificationAndLicense/AddStateLicenseAsync?profileId=" + profileId;
            }
            catch (e)
            { };
        }
        else if ($scope.visibilityControl == (index + '_editStateLicenseInformation')) {
            //Update Details - Denote the URL
            try {
                $formDataStateLicense = $('#stateLicenseEditDiv' + index).find('form');
                url = rootDir + "/Profile/IdentificationAndLicense/UpdateStateLicenseAsync?profileId=" + profileId;
            }
            catch (e)
            { };
        }
        else if ($scope.visibilityControl == (index + '_renewStateLicenseInformation')) {
            //Update Details - Denote the URL
            try {
                $formDataStateLicense = $('#stateLicenseRenewDiv' + index).find('form');
                url = rootDir + "/Profile/IdentificationAndLicense/RenewStateLicenseAsync?profileId=" + profileId;
            }
            catch (e)
            { };
        }
        if ($('#providertype').val() == '') {
            $($formDataStateLicense).find($("[name='ProviderTypeID']")).val('');
        } else {
            for (var type in $scope.ProviderTypes) {
                try {
                    if ($scope.ProviderTypes[type].ProviderTypeID == tempProviderType) {
                        providerTypeobj = $scope.ProviderTypes[type];
                        break;
                    }
                }
                catch (e) { };
            }
        }


        ResetFormForValidation($formDataStateLicense);
        validationStatus = $formDataStateLicense.valid();
        if (validationStatus) {


            $.ajax({
                url: url,
                type: 'POST',
                data: new FormData($formDataStateLicense[0]),
                async: false,
                cache: false,
                contentType: false,
                processData: false,
                success: function (data) {

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
                                $scope.StateLicensePendingRequest = true;
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
                                $scope.StateLicensePendingRequest = true;
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

    $scope.isRemoved = false;

    $scope.removeStateLicense = function (StateLicenses) {
        var validationStatus = false;
        var url = null;
        var myData = {};
        var $formData = null;
        $scope.isRemoved = true;
        $formData = $('#removeStateLicense');
        url = rootDir + "/Profile/IdentificationAndLicense/RemoveStateLicenseAsync?profileId=" + profileId;
        ResetFormForValidation($formData);
        validationStatus = $formData.valid();

        if (validationStatus) {
            //Simple POST request example (passing data) :
            $.ajax({
                url: url,
                type: 'POST',
                data: new FormData($formData[0]),
                cache: false,
                contentType: false,
                processData: false,
                success: function (data) {
                    try {
                        if (data.status == "true") {
                            var obj = $filter('filter')(StateLicenses, { StateLicenseInformationID: data.stateLicense.StateLicenseInformationID })[0];
                            StateLicenses.splice(StateLicenses.indexOf(obj), 1);
                            if ($scope.dataFetched == true) {
                                obj.HistoryStatus = 'Deleted';
                                obj.DeletedBy = data.UserName;
                                obj.DeletedDate = moment(new Date).format('MM/DD/YYYY, h:mm:ss a');
                                $scope.StateLicensesHistory.push(obj);
                            }
                            $scope.isRemoved = false;
                            $('#stateLicenseWarningModal').modal('hide');
                            $rootScope.operateCancelControl('');
                            myData = data;
                            messageAlertEngine.callAlertMessage("addedStateLicense", "State License Removed successfully.", "success", true);
                        } else {
                            $('#stateLicenseWarningModal').modal('hide');
                            messageAlertEngine.callAlertMessage("removeStateLicense", data.status, "danger", true);
                            $scope.errorStateLicense = "Sorry for Inconvenience !!!! Please Try Again Later...";
                        }
                    } catch (e) {

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
        $scope.StateLicensesHistory = [];
          
            $("#" + loadingId).css('display', 'block');
            var url = rootDir + "/Profile/ProfileHistory/GetAllStateLicensesHistory?profileId=" + profileId;
            $http.get(url).success(function (data) {
                try {
                    $scope.StateLicensesHistory = data;
                    $scope.dataFetched = true;
                    for (var i = 0; i < $scope.StateLicensesHistory.length; i++) {
                        if ($scope.StateLicensesHistory[i].HistoryStatus == '' || !$scope.StateLicensesHistory[i].HistoryStatus) {
                            $scope.StateLicensesHistory[i].HistoryStatus = 'Renewed';
                        }
                    }
                    $rootScope.GetAllUserData();
                    
                    for (var j = 0; j < $scope.StateLicensesHistory.length; j++) {
                        for (var i = 0; i < $rootScope.userslist.length; i++) {
                            if ($scope.StateLicensesHistory[j].DeletedById != null) {
                                if ($rootScope.userslist[i].CDUserID == $scope.StateLicensesHistory[j].DeletedById) {
                                    if ($rootScope.userslist[i].FullName != null) {
                                        $scope.StateLicensesHistory[j].DeletedBy = $rootScope.userslist[i].FullName;
                                        break;
                                    }
                                    else {
                                        $scope.StateLicensesHistory[j].DeletedBy = $rootScope.userslist[i].UserName;
                                        break;
                                    }
                                }
                            }
                        }
                        if ($scope.StateLicensesHistory[j].DeletedDate != null) {
                            var date = moment.utc($scope.StateLicensesHistory[j].DeletedDate).toDate();
                            $scope.StateLicensesHistory[j].DeletedDate = moment(date).format('MM/DD/YYYY, h:mm:ss a');
                        }
                    }
                    $scope.showStateLicenseHistoryTable = true;
                    $("#" + loadingId).css('display', 'none');
                } catch (e) {

                }
            });
        
    }

    $scope.cancelStateLicenseHistory = function () {
        $scope.showStateLicenseHistoryTable = false;
    }

    //****************************************Federal DEA Info********************************************
    //=============== Federal DEA License Conditions ==================

    // $scope.submitButtonText = "Add";


    //=================Data============
    $rootScope.$on("LoadRequireMasterDataIdentificationLicenses", function () {
        masterDataService.getMasterData(rootDir + "/Profile/MasterData/GetAllDEASchedules").then(function (Schedules) {
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

    $scope.$watch('tempObject.StateOfReg', function (newV, oldV) {
        if (newV == oldV) return; 
        if (newV == "") {
            $scope.Deastateerrormsg = true;
        }
        else {
            $scope.Deastateerrormsg = false;
        }
    });
    $scope.reseterrormessages = function () {
        $scope.Deastateerrormsg = false;
    }
    $scope.IndexValue2 = '';
    //====================== Federal DEA License Save ===================
    $scope.Deastateerrormsg = false;

    $scope.saveFederalDEALicense = function (FederalDEALicense, index) {
        //loadingOn();
        //$scope.Deastateerrormsg = false;
        $scope.IndexValue2 = index;
        var myData = {};
        var validationStatus;
        var url;
        var $formDataDEA;
        if ($scope.visibilityControl == 'addDEAInformation') {
            try {
                $formDataDEA = $('#newShowFederalDEALicenseDiv').find('form');
                url = rootDir + "/Profile/IdentificationAndLicense/AddFederalDEALicenseAsync?profileId=" + profileId;
            }
            catch (e) { };
            //Add Details - Denote the URL

        }
        else if ($scope.visibilityControl == (index + '_editDEAInformation')) {
            try {
                $formDataDEA = $('#FederalDEALicenseEditDiv' + index).find('form');
                url = rootDir + "/Profile/IdentificationAndLicense/UpdateFederalDEALicenseAsync?profileId=" + profileId;
            }
            catch (e) { };
            //Update Details - Denote the URL

        }
        else if ($scope.visibilityControl == (index + '_renewDEAInformation')) {
            try {
                $formDataDEA = $('#DEAInformationRenewDiv' + index).find('form');
                url = rootDir + "/Profile/IdentificationAndLicense/RenewFederalDEALicenseAsync?profileId=" + profileId;
            }
            catch (e) { };
            //Update Details - Denote the URL

        }


        ResetFormForValidation($formDataDEA);
        validationStatus = $formDataDEA.valid();
        if (typeof $scope.tempObject.StateOfReg == "undefined" || $scope.tempObject.StateOfReg == "") {
            $scope.Deastateerrormsg = true;
        }
        $scope.DEAError = '';

        if (validationStatus && !$scope.Deastateerrormsg) {

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
                                $scope.FederalDEAPendingRequest = true;
                                $scope.FederalDEA[index] = data.federalDea;
                                $rootScope.operateViewAndAddControl(index + '_viewDEAInformation');
                                messageAlertEngine.callAlertMessage("updatedDEA" + index, "DEA Information Updated Successfully!!!!", "success", true);
                            }
                            else {
                                $scope.FederalDEAPendingRequest = true;
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
            var url = rootDir + "/Profile/ProfileHistory/GetAllFederalDEALicensesHistory?profileId=" + profileId;
            $http.get(url).success(function (data) {
                try {
                    $scope.FederalDEAHistory = data;

                    $scope.dataFetchedFederalDEA = true;
                    for (var i = 0; i < $scope.FederalDEAHistory.length; i++) {
                        if ($scope.FederalDEAHistory[i].HistoryStatus == '' || !$scope.FederalDEAHistory[i].HistoryStatus) {
                            $scope.FederalDEAHistory[i].HistoryStatus = 'Renewed';

                        }

                    }
                    $rootScope.GetAllUserData();
                    for (var j = 0; j < $scope.FederalDEAHistory.length; j++) {
                        for (var i = 0; i < $rootScope.userslist.length; i++) {
                            if ($scope.FederalDEAHistory[j].DeletedById != null) {
                                if ($rootScope.userslist[i].CDUserID == $scope.FederalDEAHistory[j].DeletedById) {
                                    if ($rootScope.userslist[i].FullName != null) {
                                        $scope.FederalDEAHistory[j].DeletedBy = $rootScope.userslist[i].FullName;
                                        break;
                                    }
                                    else {
                                        $scope.FederalDEAHistory[j].DeletedBy = $rootScope.userslist[i].UserName;
                                        break;
                                    }
                                }
                            }
                        }
                        if ($scope.FederalDEAHistory[j].DeletedDate != null) {
                            var date = moment.utc($scope.FederalDEAHistory[j].DeletedDate).toDate();
                            $scope.FederalDEAHistory[j].DeletedDate = moment(date).format('MM/DD/YYYY, h:mm:ss a');
                        }
                    }
                    $scope.showFederalDEALicenseHistoryTable = true;
                    $("#" + loadingId).css('display', 'none');
                } catch (e) {

                }


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
    $scope.ErrormessageforCDSstate = false;
    $scope.IndexValue5 = '';
    //====================== CDSC Information Save===================

    $scope.$watch('tempObject.State', function (newVal, oldVal) {
        if (newVal == oldVal) { return; }
        if (newVal == "") {
            $scope.ErrormessageforCDSstate = true;
        } else { $scope.ErrormessageforCDSstate = false; }
    })

    $scope.saveCDSCInformation = function (cDSCInformation, index) {
        //loadingOn();
        $scope.ErrormessageforCDSstate = false;
        if (typeof $scope.tempObject.State == "undefined" || $scope.tempObject.State == "") {
            $scope.ErrormessageforCDSstate = true;
        }
        $scope.IndexValue5 = index;

        var validationStatus;
        var url;
        var myData = {};
        var $formDataCDSC;
        if ($scope.visibilityControl == 'addCDSCInformation') {
            try {
                $formDataCDSC = $('#newShowCDSCInformationDiv').find('form');
                url = rootDir + "/Profile/IdentificationAndLicense/AddCDSCLicenseAsync?profileId=" + profileId;
            }
            catch (e) { };
            //Add Details - Denote the URL

        }
        else if ($scope.visibilityControl == (index + '_editCDSCInformation')) {
            try {
                $formDataCDSC = $('#cDSCInformationEditDiv' + index).find('form');
                url = rootDir + "/Profile/IdentificationAndLicense/UpdateCDSCLicenseAsync?profileId=" + profileId;
            }
            catch (e) { };
            //Update Details - Denote the URL

        }
        else if ($scope.visibilityControl == (index + '_renewCDSCInformation')) {
            try {
                $formDataCDSC = $('#CDSCInformationRenewDiv' + index).find('form');
                url = rootDir + "/Profile/IdentificationAndLicense/RenewCDSCLicenseAsync?profileId=" + profileId;
            }
            catch (e) { };
            //Update Details - Denote the URL

        }
        ResetFormForValidation($formDataCDSC);
        validationStatus = $formDataCDSC.valid();

        if (validationStatus && !$scope.ErrormessageforCDSstate) {

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
                                $scope.CDSInformationPendingRequest = true;
                                $scope.CDSCInformations[index] = data.CDSCInformation;
                                $rootScope.operateViewAndAddControl(index + '_viewCDSCInformation');
                                messageAlertEngine.callAlertMessage("updatedCDS" + index, "CDS Information Updated Successfully!!!!", "success", true);
                            }
                            else {
                                $scope.CDSInformationPendingRequest = true;
                                $scope.CDSCInformations[index] = data.CDSCInformation;
                                $rootScope.operateViewAndAddControl(index + '_viewCDSCInformation');
                                messageAlertEngine.callAlertMessage("renewedCDS" + index, "CDS Information Renewed Successfully!!!!", "success", true);
                            }
                            myData = data;
                            FormReset($formDataCDSC);
                            $scope.resetDates();
                        } else {
                            //$scope.Errormessage = 'Please enter Issue State *.';
                            if (data.status.indexOf(",") > -1) {
                                $scope.CDSCError = data.status.split(",");
                                //$scope.Errormessage = $scope.CDSCError;
                                messageAlertEngine.callAlertMessage("CDSErrorForexpiryDate", $scope.CDSCError, "danger", true);
                            }
                            else {
                                $scope.CDSCError = data.status;
                                //$scope.Errormessage = $scope.CDSCError;
                                messageAlertEngine.callAlertMessage("CDSErrorForexpiryDate", $scope.CDSCError, "danger", true);
                            }
                            //messageAlertEngine.callAlertMessage('CDSCError', "", "danger", true);
                        }
                    }
                    catch (e) { };
                },
                error: function (e) {
                    try {
                        $scope.CDSCError = "Sorry for Inconvenience !!!! Please Try Again Later...";
                        //messageAlertEngine.callAlertMessage('CDSCError', "", "danger", true);
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
    //$scope.ConvertDateForTE = function (date) {
    //    var followupdateforupdatetask = "";
    //    if (date !== null || date !== "") {
    //        var date5 = date.split('T');
    //        var dates1 = date5;
    //        var date5 = date5[0].split('-');
    //        followupdateforupdatetask = date5[2] + "/" + date5[1] + "/" + date5[0] + " " + dates1[1];
    //    }
    //    return followupdateforupdatetask;
    //}
    $scope.removeCDSC = function (CDSCInformations) {
        var validationStatus = false;
        var url = null;
        var myData = {};
        var $formData = null;
        $scope.isRemoved = true;
        $formData = $('#removeCDSC');
        url = rootDir + "/Profile/IdentificationAndLicense/RemoveCDSCLicenseAsync?profileId=" + profileId;
        ResetFormForValidation($formData);
        validationStatus = $formData.valid();

        if (validationStatus) {
            //Simple POST request example (passing data) :
            $.ajax({
                url: url,
                type: 'POST',
                data: new FormData($formData[0]),
                cache: false,
                contentType: false,
                processData: false,
                success: function (data) {
                    try {
                        if (data.status == "true") {
                            var obj = $filter('filter')(CDSCInformations, { CDSCInformationID: data.cDSCInformation.CDSCInformationID })[0];
                            CDSCInformations.splice(CDSCInformations.indexOf(obj), 1);
                            if ($scope.dataFetchedCDSC) {
                                obj.HistoryStatus = 'Deleted';
                                obj.DeletedBy = data.UserName;
                                obj.DeletedDate = moment(new Date).format('MM/DD/YYYY, h:mm:ss a');
                                $scope.CDSCInformationsHistory.push(obj);
                            }
                            $scope.isRemoved = false;
                            $('#cDSCWarningModal').modal('hide');
                            $rootScope.operateCancelControl('');
                            myData = data;
                            messageAlertEngine.callAlertMessage("addedCDS", "CDS Information Removed successfully.", "success", true);
                        } else {
                            $('#cDSCWarningModal').modal('hide');
                            messageAlertEngine.callAlertMessage("removeCDS", data.status, "danger", true);
                            $scope.errorCDS = "Sorry for Inconvenience !!!! Please Try Again Later...";
                        }
                    } catch (e) {

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
            var url = rootDir + "/Profile/ProfileHistory/GetAllCDSCInformationHistory?profileId=" + profileId;
            $http.get(url).success(function (data) {
                try {
                    $scope.CDSCInformationsHistory = data;
                    $scope.dataFetchedCDSC = true;
                    for (var i = 0; i < $scope.CDSCInformationsHistory.length; i++) {
                        if ($scope.CDSCInformationsHistory[i].HistoryStatus == '' || !$scope.CDSCInformationsHistory[i].HistoryStatus) {
                            $scope.CDSCInformationsHistory[i].HistoryStatus = 'Renewed';
                        }
                    }
                    $rootScope.GetAllUserData();
                    for (var j = 0; j < $scope.CDSCInformationsHistory.length; j++) {
                        for (var i = 0; i < $rootScope.userslist.length; i++) {
                            if ($scope.CDSCInformationsHistory[j].DeletedById != null) {
                                if ($rootScope.userslist[i].CDUserID == $scope.CDSCInformationsHistory[j].DeletedById) {
                                    if ($rootScope.userslist[i].FullName != null) {
                                        $scope.CDSCInformationsHistory[j].DeletedBy = $rootScope.userslist[i].FullName;
                                        break;
                                    }
                                    else {
                                        $scope.CDSCInformationsHistory[j].DeletedBy = $rootScope.userslist[i].UserName;
                                        break;
                                    }
                                }
                            }
                        }
                        if ($scope.CDSCInformationsHistory[j].DeletedDate != null) {
                            var date = moment.utc($scope.CDSCInformationsHistory[j].DeletedDate).toDate();
                            $scope.CDSCInformationsHistory[j].DeletedDate = moment(date).format('MM/DD/YYYY, h:mm:ss a');
                        }
                    }
                    $scope.showCDSCInformationHistoryTable = true;
                    $("#" + loadingId).css('display', 'none');
                } catch (e) {

                }

            });
        }
        else {
            $scope.showCDSCInformationHistoryTable = true;
        }
        
    }
    //$scope.ConvertDateForTE = function (date) {
    //    var followupdateforupdatetask = "";
    //    if (date !== null || date !== "") {
    //        var date5 = date.split('T');
    //        var dates1 = date5;
    //        var date5 = date5[0].split('-');
    //        followupdateforupdatetask = date5[2] + "/" + date5[1] + "/" + date5[0] + " " + dates1[1];
    //    }
    //    return followupdateforupdatetask;
    //}
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
                url = rootDir + "/Profile/IdentificationAndLicense/AddMedicareInformationAsync?profileId=" + profileId;
            }
            catch (e) { };
            //Add Details - Denote the URL

        }
        else if ($scope.visibilityControl == (index + '_editMedicareInformation')) {
            try {
                $formDataMedicare = $('#MedicareInformationEditDiv' + index).find('form');
                url = rootDir + "/Profile/IdentificationAndLicense/UpdateMedicareInformationAsync?profileId=" + profileId;
            }
            catch (e) { };
            //Update Details - Denote the URL

        }

        ResetFormForValidation($formDataMedicare);
        validationStatus = $formDataMedicare.valid();
        $scope.MedicareError = '';

        if (validationStatus) {
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
                            data.MedicareInformation.ExpirationDate = ConvertDateFormat(data.MedicareInformation.ExpirationDate);
                            // data.MedicareInformation.ExpiryDate = ConvertDateFormat(data.MedicareInformation.ExpiryDate);
                            myData = data;
                            if ($scope.visibilityControl == 'addMedicareInformation') {
                                $scope.MedicareInformations.push(data.MedicareInformation);
                                $rootScope.operateCancelControl('');
                                messageAlertEngine.callAlertMessage("addedMedicare", "Medicare information saved successfully. !!!!", "success", true);
                            }
                            else {
                                $scope.MedicareInformationPendingRequest = true;
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
            var url = rootDir + "/Profile/ProfileHistory/GetAllMedicareInformationHistory?profileId=" + profileId;
            $http.get(url).success(function (data) {
                try {
                    $scope.MedicareInformationsHistory = data;
                    $scope.dataFetchedMedicare = true;
                    for (var i = 0; i < $scope.MedicareInformationsHistory.length; i++) {
                        if ($scope.MedicareInformationsHistory[i].HistoryStatus == '' || !$scope.MedicareInformationsHistory[i].HistoryStatus) {
                            $scope.MedicareInformationsHistory[i].HistoryStatus = 'Renewed';
                        }
                    }
                    $rootScope.GetAllUserData();
                    for (var j = 0; j < $scope.MedicareInformationsHistory.length; j++) {
                        for (var i = 0; i < $rootScope.userslist.length; i++) {
                            if ($scope.MedicareInformationsHistory[j].DeletedById != null) {
                                if ($rootScope.userslist[i].CDUserID == $scope.MedicareInformationsHistory[j].DeletedById) {
                                    if ($rootScope.userslist[i].FullName != null) {
                                        $scope.MedicareInformationsHistory[j].DeletedBy = $rootScope.userslist[i].FullName;
                                        break;
                                    }
                                    else {
                                        $scope.MedicareInformationsHistory[j].DeletedBy = $rootScope.userslist[i].UserName;
                                        break;
                                    }
                                }
                            }
                           
                        }
                        if ($scope.MedicareInformationsHistory[j].DeletedDate != null) {
                            var date = moment.utc($scope.MedicareInformationsHistory[j].DeletedDate).toDate();
                            $scope.MedicareInformationsHistory[j].DeletedDate = moment(date).format('MM/DD/YYYY, h:mm:ss a');
                        }
                    }
                    $scope.showMedicareHistoryTable = true;
                    $("#" + loadingId).css('display', 'none');
                } catch (e) {

                }

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
        //$scope.declinedDate = $filter('date')(new Date(1980, 00, 01), 'dd/MM/yyyy');
        $scope.declinedDate = new Date();
        var states = 'state';
        var myState;
        //var myDate = null;
        try {
            $scope.MedicaidInformations = val;
            for (var i = 0; i < $scope.MedicaidInformations.length ; i++) {
                //myDate = $filter('date')(new Date($scope.MedicaidInformations[i].IssueDate), 'dd/MM/yyyy');
                myState = $scope.MedicaidInformations[i].State;
                if (myState == states) {
                    $scope.MedicaidInformations[i].isDeclined = true;
                } else {
                    $scope.MedicaidInformations[i].isDeclined = false;
                }
            }
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


    $scope.isDeclined = function () {

        if ($scope.tempObject.isDeclined == true) {

            $scope.tempObject.LicenseNumber = $rootScope.lastName;
            $scope.tempObject.IssueDate = new Date();
            $scope.tempObject.State = 'state';
        }
        else if ($scope.tempObject.isDeclined == false) {

            $scope.tempObject.LicenseNumber = '';
            $scope.tempObject.IssueDate = '';
            $scope.tempObject.State = '';
            $scope.tempObject.CertificatePath = '';
        }

        //$scope.States = ["Florida", "Texas", "California", "Alaska"];

    }

    $scope.saveMedicaidInformation = function (MedicaidInformation, index) {
        //if ($scope.tempObject.isDeclined) {
        //    $scope.tempObject.LicenseNumber = $scope.OtherIdentificationNumbers.NPINumber;
        //}
        //loadingOn();
        $scope.IndexValue4 = index;
        var validationStatus;
        var url;
        var myData = {};
        var $formDataMedicaid;
        if ($scope.visibilityControl == 'addMedicaidInformation') {
            try {
                $formDataMedicaid = $('#newShowMedicaidInformationDiv').find('form');
                url = rootDir + "/Profile/IdentificationAndLicense/AddMedicaidInformationAsync?profileId=" + profileId;
            }
            catch (e) { };
            //Add Details - Denote the URL

        }
        else if ($scope.visibilityControl == (index + '_editMedicaidInformation')) {
            try {
                $formDataMedicaid = $('#MedicaidInformationEditDiv' + index).find('form');
                url = rootDir + "/Profile/IdentificationAndLicense/UpdateMedicaidInformationAsync?profileId=" + profileId;
            }
            catch (e) { };
            //Update Details - Denote the URL

        }

        ResetFormForValidation($formDataMedicaid);
        validationStatus = $formDataMedicaid.valid();
        $scope.MedicaidError = '';

        if (validationStatus) {
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
                            data.MedicaidInformation.ExpirationDate = ConvertDateFormat(data.MedicaidInformation.ExpirationDate);
                            // data.MedicaidInformation.ExpiryDate = ConvertDateFormat(data.MedicaidInformation.ExpiryDate);
                            myData = data;
                            if ($scope.visibilityControl == 'addMedicaidInformation') {
                                $scope.MedicaidInformations.push(data.MedicaidInformation);
                                if ($scope.tempObject.isDeclined) {
                                    $scope.MedicaidInformations[$scope.MedicaidInformations.length - 1].isDeclined = true;
                                    $rootScope.visibilityControl = "";
                                } else {
                                    $scope.MedicaidInformations[$scope.MedicaidInformations.length - 1].isDeclined = false;
                                }

                                $rootScope.operateCancelControl('');
                                messageAlertEngine.callAlertMessage("addedMedicaid", "Medicaid information saved successfully. !!!!", "success", true);
                            }
                            else {
                                $scope.MedicaidInformationPendingRequest = true;
                                $scope.MedicaidInformations[index] = data.MedicaidInformation;
                                if ($scope.tempObject.isDeclined) {
                                    $scope.MedicaidInformations[index].isDeclined = true;
                                    $rootScope.visibilityControl = "";
                                } else {
                                    $scope.MedicaidInformations[index].isDeclined = false;
                                    $rootScope.operateViewAndAddControl(index + '_viewMedicaidInformation');
                                }

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
            var url = rootDir + "/Profile/ProfileHistory/GetAllMedicaidInformationHistory?profileId=" + profileId;
            $http.get(url).success(function (data) {
                $scope.MedicaidInformationsHistory = data;
                $scope.dataFetchedMedicaid = true;
                for (var i = 0; i < $scope.MedicaidInformationsHistory.length ; i++) {
                    if ($scope.MedicaidInformationsHistory[i].HistoryStatus == '' || !$scope.MedicaidInformationsHistory[i].HistoryStatus) {
                        $scope.MedicaidInformationsHistory[i].HistoryStatus = 'Renewed';
                    }
                    if ($scope.MedicaidInformationsHistory[i].State == 'state') {
                        $scope.MedicaidInformationsHistory.splice(i, 1);
                        i--;
                    }
                }
                $rootScope.GetAllUserData();
                for (var j = 0; j < $scope.MedicaidInformationsHistory.length; j++) {
                    for (var i = 0; i < $rootScope.userslist.length; i++) {
                        if ($scope.MedicaidInformationsHistory[j].DeletedById != null) {
                            if ($rootScope.userslist[i].CDUserID == $scope.MedicaidInformationsHistory[j].DeletedById) {
                                if ($rootScope.userslist[i].FullName != null) {
                                    $scope.MedicaidInformationsHistory[j].DeletedBy = $rootScope.userslist[i].FullName;
                                    break;
                                }
                                else {
                                    $scope.MedicaidInformationsHistory[j].DeletedBy = $rootScope.userslist[i].UserName;
                                    break;
                                }
                            }
                        }
                       
                    }
                    if ($scope.MedicaidInformationsHistory[j].DeletedDate != null) {
                        var date = moment.utc($scope.MedicaidInformationsHistory[j].DeletedDate).toDate();
                        $scope.MedicaidInformationsHistory[j].DeletedDate = moment(date).format('MM/DD/YYYY, h:mm:ss a');
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
                url: rootDir + '/Profile/IdentificationAndLicense/UpdateOtherIdentificationNumberAsync?profileId=' + profileId,
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
                                $scope.OtherIdentificationNumbersPendingRequest = true;
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


    $scope.AutoFillNextAttestationDate = function (objstart) {
        if (objstart != null && objstart != "")
        {
            $scope.tempObject.NextAttestationDate = new Date(new Date(objstart).setDate(new Date(objstart).getDate() + 120));
        }
        else {
            $scope.hasError = true;
            $scope.tempObject.NextAttestationDate = "";
        //    //$scope.tempObject.NextAttestationDate = new Date(new Date(objstart).setMonth(new Date(objstart).getMonth() + 24));
        //    //$scope.tempObject.NextAttestationDate = new Date(new Date(objstart).setDate(new Date(objstart).getDate() + 120));
        //    //return $scope.tempObject.NextAttestationDate;
        }
    }

    //$scope.$watch("tempObject.LastCAQHAttestationDate", function (newV, oldV) {

    //    if (newV === "") { newV = null }
    //    if (oldV === "") { oldV = null }

    //    if (newV === oldV) {
    //        return;
    //    } else if (newV != null && newV != "") {
    //        $scope.tempObject.NextAttestationDate = new Date(new Date(newV).setDate(new Date(newV).getDate() + 120));
    //    }
    //    else {
    //        $scope.hasError = true;
    //        $scope.tempObject.NextAttestationDate = "";
    //    }

    //})
    $scope.AutoFillMedicareExpirationDate = function (objstart) {
        $scope.tempObject.ExpirationDate = $scope.ConvertDateBy5Years($scope.tempObject.IssueDate);
    }
    $scope.AutoFillMedicaidExpirationDate = function (objstart) {
        $scope.tempObject.ExpirationDate = $scope.ConvertDateBy5Years($scope.tempObject.IssueDate);
    }
    //$scope.$watch("tempObject.IssueDate", function (newV, oldV) {
    //    if (newV === oldV) {
    //        return;
    //    } else if (newV != null && newV != "") {
    //        $scope.tempObject.ExpirationDate = $scope.ConvertDateBy5Years($scope.tempObject.IssueDate);
    //    }
    //    else {
    //        $scope.hasError = true;
    //        $scope.tempObject.ExpirationDate = "";
    //    }

    //})

   
    
    $scope.ConvertDateBy5Years = function (date) {
        if (date != '' || date != 'null') {
            var dt = new Date(date);
            var month = dt.getMonth() + 1;
            var monthString = month > 9 ? month : '0' + month;
            //var monthName = monthNames[month];
            var day = dt.getDate();
            var dayString = day > 9 ? day : '0' + day;
            var year = dt.getFullYear() + 5;
            shortDate = monthString + '/' + dayString + '/' + year;
            return shortDate;
        } return null;
    }


    $scope.resetDates = function () {
        try {
            $scope.ErrormessageforCDSstate = false;
            $scope.Errormessage = '';
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
            //        $rootScope.$emit(key, data[key]);
            //        //call respective controller to load data (PSP)
            //    }
            //    $rootScope.IdentificationLicensesLoaded = true;
            //    //$rootScope.$broadcast("LoadRequireMasterData");
            //} catch (e) {
            //    $rootScope.IdentificationLicensesLoaded = true;
            //}
            $http({
                method: 'GET',
                url: rootDir + '/Profile/MasterProfile/GetIdentificationAndLicensesProfileDataAsync?profileId=' + profileId
            }).success(function (data, status, headers, config) {
                try {
                    for (key in data) {
                        $rootScope.$emit(key, data[key]);
                        //call respective controller to load data (PSP)
                    }
                    $rootScope.IdentificationLicensesLoaded = true;
                    $rootScope.$broadcast("LoadRequireMasterDataIdentificationLicenses");
                } catch (e) {
                    $rootScope.IdentificationLicensesLoaded = true;
                }

            }).error(function (data, status, headers, config) {
                $rootScope.IdentificationLicensesLoaded = true;
            });
            $scope.dataLoaded = true;
        }
    });

}]);
$(document).ready(function () {
    $("#EnterState").keydown(function (event) {
        $(this).next();
    });
});
//============l=end==========================================//