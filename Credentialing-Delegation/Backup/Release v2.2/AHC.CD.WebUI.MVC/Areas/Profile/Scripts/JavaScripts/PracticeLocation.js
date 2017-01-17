profileApp.controller('practiceLocationController', ['$scope', '$rootScope', '$http', '$filter', 'masterDataService', 'locationService', 'messageAlertEngine', function ($scope, $rootScope, $http, $filter, masterDataService, locationService, messageAlertEngine) {

    $scope.GeneralInformationEdit = false;
    // Toggle the view of passed DOM Element by ID
    $scope.PracticeLocationToggle = false;

   //for office hours. By Reshma
    $scope.changeOtherTwo = function (isYesOption,questionNumber) {
        if (questionNumber == 'one' && isYesOption == '1')
        {
            $scope.tempSecondObject.VoiceMailToAnsweringServiceYesNoOption = '2';
            $scope.tempSecondObject.VoiceMailOtherYesNoOption = '2';
        }
        //else if (questionNumber == 'two' && isYesOption == '1') {
        //    $scope.tempSecondObject.AnsweringServiceYesNoOption = '2';
        //    $scope.tempSecondObject.VoiceMailOtherYesNoOption = '2';
        //}
        //else if (questionNumber == 'three' && isYesOption == '1') {
        //    $scope.tempSecondObject.AnsweringServiceYesNoOption = '2';
        //    $scope.tempSecondObject.VoiceMailToAnsweringServiceYesNoOption = '2';
        //}
        else if (isYesOption == '2' && questionNumber == 'AnyTimePhoneCoverageYesNoOption')
        {
            $scope.tempSecondObject.AnsweringServiceYesNoOption = '';
            $scope.tempSecondObject.VoiceMailToAnsweringServiceYesNoOption = '';
            $scope.tempSecondObject.VoiceMailOtherYesNoOption = '';

        }
    };


    $scope.ToggleScript = function () {
        if ($scope.PracticeLocationToggle) {
            $scope.PracticeLocationToggle = false;
        } else {
            $scope.PracticeLocationToggle = true;
        }
    };

    $scope.PanelToggle = function (id) {
        $('#' + id).toggle();
    };

    $scope.getOrganizationById = function (OrganizationById) {
        for (org in $scope.organizations) {
            if ($scope.organizations[org].OrganizationID === parseInt(OrganizationById)) {
                return $scope.organizations[org];
            }
        }
        return null;
    };

    $scope.facilities = [];

    $rootScope.$on("LoadRequireMasterDataPracticeLocation", function () {
    // Calling master data service to get all Accessibility Questions
        masterDataService.getMasterData(rootDir + "/Profile/MasterData/GetAllAccessibilityQuestions").then(function (GetAllAccessibilityQuestions) {
        $scope.masterAccessibilityQuestions = GetAllAccessibilityQuestions;        
    });

    // Calling master data service to get all Service Questions
        masterDataService.getMasterData(rootDir + "/Profile/MasterData/GetAllServiceQuestions").then(function (GetAllServiceQuestions) {
        $scope.masterServiceQuestions = GetAllServiceQuestions;
    });

    // Calling master data service to get all Practice Types
        masterDataService.getMasterData(rootDir + "/Profile/MasterData/GetAllPracticeTypes").then(function (GetAllPracticeTypes) {
        $scope.masterPracticeTypes = GetAllPracticeTypes;
    });

        masterDataService.getMasterData(rootDir + "/Profile/MasterData/GetAllOrganizations").then(function (GetAllOrganizations) {
        $scope.organizations = GetAllOrganizations;
    });

        masterDataService.getMasterData(rootDir + "/Profile/MasterData/GetAllFacilities").then(function (GetAllFacilities) {
       // console.log(GetAllFacilities);
        $scope.facilities = GetAllFacilities;
        for (var i = 0; i < $scope.facilities.length; i++) {
            var tempMidLevels = [];
            for (var j = 0; j < $scope.facilities[i].FacilityDetail.FacilityPracticeProviders.length; j++) {
                if ($scope.facilities[i].FacilityDetail.FacilityPracticeProviders[j].Status != 'Inactive') {
                    tempMidLevels.push($scope.facilities[i].FacilityDetail.FacilityPracticeProviders[j]);
                }
            }
            $scope.facilities[i].FacilityDetail.FacilityPracticeProviders = tempMidLevels;
        }
    });

    // Calling master data service to get all Open Practice Status Questions
        masterDataService.getMasterData(rootDir + "/Profile/MasterData/GetAllOpenPracticeStatusQuestions").then(function (GetAllOpenPracticeStatusQuestions) {
        $scope.masterOpenPracticeStatusQuestions = GetAllOpenPracticeStatusQuestions;
    });

    // Calling master data service to get all organization practicing group info
        masterDataService.getMasterData(rootDir + "/MasterData/Organization/GetGroups").then(function (GetAllPracticingGroup) {
        $scope.groups = GetAllPracticingGroup;
    });

    //Calling master data service to get all Office Manager from master data
        masterDataService.getMasterData(rootDir + "/Profile/MasterData/GetAllBusinessContactPerson").then(function (GetAllOfficeManager) {
        $scope.managers = GetAllOfficeManager;
    });

    //Calling master data service to get all Billing Contact from master data
        masterDataService.getMasterData(rootDir + "/Profile/MasterData/GetAllBillingContact").then(function (GetAllBillingContact) {
        $scope.billings = GetAllBillingContact;
    });

    // Calling master data service to get all Payment And Remittance from master data
        masterDataService.getMasterData(rootDir + "/Profile/MasterData/GetAllPaymentAndRemittance").then(function (GetAllPaymentAndRemittance) {
        $scope.payments = GetAllPaymentAndRemittance;
    });

        masterDataService.getMasterData(rootDir + "/Profile/MasterData/GetAllCredentialingContact").then(function (GetAllCredentialingContact) {
        //console.log(GetAllBillingContact);
        $scope.credentialingContact = GetAllCredentialingContact;
        //console.log(GetAllCredentialingContact);
    });
    });

    $rootScope.getPractitionerData = function () {
        //console.log(profileId);
        masterDataService.getPractitioners(rootDir + "/Profile/MasterData/GetPractitionersByProviderLevel", "Mid-Level", profileId).then(function (MidLevelPractitioners) {
            //console.log(MidLevelPractitioners);
            //console.log("Midlevel Called");
            $scope.midLevelPractitioners = MidLevelPractitioners;
        });

        masterDataService.getPractitioners(rootDir + "/Profile/MasterData/GetPractitionersByProviderLevel", "Doctor", profileId).then(function (SupervisingPractitioners) {
            //console.log("Supervisor called");
            //console.log(SupervisingPractitioners);
            $scope.supervisingProviders = SupervisingPractitioners;
        });


        masterDataService.getProviderLevels(rootDir + "/Profile/MasterData/GetAllProviderLevelByProfileId", profileId).then(function (ProviderLevel) {
            $scope.providerLevel = ProviderLevel;
        });
    };
    
    $scope.append = function (data) { $rootScope.tempObject.Name = data; }

    $scope.IsRenew = false;

    $scope.copyMasterSpecialties = [];
    $scope.copyMasterProviderTypes = [];

    $rootScope.$on("LoadRequireMasterDataPracticeLocation", function () {

        masterDataService.getMasterData(rootDir + "/Profile/MasterData/GetAllSpecialities").then(function (masterSpecialties) {
            //console.log(masterSpecialties);
        $scope.masterSpecialties = masterSpecialties;
        $scope.copyMasterSpecialties = angular.copy($scope.masterSpecialties);
    });

        masterDataService.getMasterData(rootDir + "/Profile/MasterData/GetAllProviderTypes").then(function (masterProvidertypes) {
            //console.log(masterProvidertypes);
        $scope.masterProviderTypes = masterProvidertypes;
        $scope.copyMasterProviderTypes = angular.copy($scope.masterProviderTypes);
    });

    });

    $scope.setFacilities = function (organizationID) {
        if (organizationID === "") {
            $scope.facilities = [];
        }
        $scope.facilities = $scope.getOrganizationById(organizationID).Facilities;
    };

    $scope.addressAutocomplete = function (location) {
        if (location.length == 0) {
            $scope.resetAddressModels();
        }
        
        $scope.tempSecondObject.City = location;
        if (location.length > 1 && !angular.isObject(location)) {
            locationService.getLocations(location).then(function (val) {
                $scope.Locations = val;
            });
        } else if (angular.isObject(location)) {
            $scope.setAddressModels(location);
        }
    };

    $scope.selectedLocation = function (location) {
        $scope.setAddressModels(location);
        $(".ProviderTypeSelectAutoList").hide();
    };

    $scope.resetAddressModels = function () {
        $scope.tempSecondObject.City = "";
        $scope.tempSecondObject.State = "";
        $scope.tempSecondObject.Country = "";
    };

    $scope.setAddressModels = function (location) {
        $scope.tempSecondObject.City = location.City;
        $scope.tempSecondObject.State = location.State;
        $scope.tempSecondObject.Country = location.Country;

    }


    // Practice Location Details Variable Initialization

    $scope.PracticeLocationDetails = [];

    /* Practice Location Information client side controller actions */

    // Controls the View practice location feature
    $scope.operateViewControlPracLoc = function (sectionValue) {
        $rootScope.tempObject = {}; //resets temp object
        $scope.visibilityControlPracLoc = sectionValue;
    };
    // Controls the add practice location
    $scope.operateAddControlPracLoc = function (sectionValue) {
        $rootScope.tempObject = {};
        $rootScope.tempSecondObject = {};
        $scope.visibilityControlPracLoc = sectionValue;
        $('.field-validation-error').removeClass('field-validation-error').addClass('field-validation-valid');
        $('.input-validation-error').removeClass('input-validation-error').addClass('valid');
        $rootScope.tempObject.FacilityDetail = { Language: { NonEnglishLanguages: [] } };
        $rootScope.tempObject.PracticeOfficeHours = {};
    }

    $scope.editGeneralInfo = function (PracticeLocationDetail) {
        $scope.GeneralInformationEdit = true;
        $rootScope.operateSecondEditControl(null, PracticeLocationDetail);
    }

    $scope.editGeneralCancel = function (PracticeLocationDetail) {
        $scope.GeneralInformationEdit = false;
    }

    //....................Practice Location History............................//
    $scope.PracticeLocationDetailsHistory = [];
    $scope.dataFetched = false;

    $scope.showPLHistory = function (loadingId) {
        if ($scope.PracticeLocationDetailsHistory.length == 0) {
            $("#" + loadingId).css('display', 'block');
            var url = rootDir + "/Profile/ProfileHistory/GetAllPracticeLocationDetailHistory?profileId=" + profileId;
            $http.get(url).success(function (data) {
                $scope.PracticeLocationDetailsHistory = data;
                $scope.dataFetched = true;
                for (var i = 0; i < $scope.PracticeLocationDetailsHistory.length; i++) {
                    if ($scope.PracticeLocationDetailsHistory[i].HistoryStatus == '' || !$scope.PracticeLocationDetailsHistory[i].HistoryStatus) {
                        $scope.PracticeLocationDetailsHistory[i].HistoryStatus = 'Renewed';
                    }
                    $scope.PracticeLocationDetailsHistory[i].SupervisingProviders = [];
                    $scope.PracticeLocationDetailsHistory[i].MidlevelPractioners = [];
                    $scope.PracticeLocationDetailsHistory[i].PracticeColleagues = [];
                    for (var j = 0; j < $scope.PracticeLocationDetailsHistory[i].PracticeProviders.length; j++) {
                        if ($scope.PracticeLocationDetailsHistory[i].PracticeProviders[j].Practice == 'Supervisor' && $scope.PracticeLocationDetailsHistory[i].PracticeProviders[j].Status == 'Active') {
                            $scope.PracticeLocationDetailsHistory[i].SupervisingProviders.push($scope.PracticeLocationDetailsHistory[i].PracticeProviders[j]);
                        }
                        if ($scope.PracticeLocationDetailsHistory[i].PracticeProviders[j].Practice == 'Midlevel' && $scope.PracticeLocationDetailsHistory[i].PracticeProviders[j].Status == 'Active') {
                            $scope.PracticeLocationDetailsHistory[i].MidlevelPractioners.push($scope.PracticeLocationDetailsHistory[i].PracticeProviders[j]);
                        }
                        if ($scope.PracticeLocationDetailsHistory[i].PracticeProviders[j].Practice == 'CoveringColleague' && $scope.PracticeLocationDetailsHistory[i].PracticeProviders[j].Status == 'Active') {
                            $scope.PracticeLocationDetailsHistory[i].PracticeColleagues.push($scope.PracticeLocationDetailsHistory[i].PracticeProviders[j]);
                            for (var k = 0; k < $scope.PracticeLocationDetailsHistory[i].PracticeColleagues.length; k++) {
                                tempPracticeProviderSpecialties = [];
                                tempPracticeProviderTypes = [];
                                for (var l = 0; l < $scope.PracticeLocationDetailsHistory[i].PracticeColleagues[k].PracticeProviderSpecialties.length; l++) {
                                    if ($scope.PracticeLocationDetailsHistory[i].PracticeColleagues[k].PracticeProviderSpecialties[l].StatusType == '1') {
                                        tempPracticeProviderSpecialties.push($scope.PracticeLocationDetailsHistory[i].PracticeColleagues[k].PracticeProviderSpecialties[l]);
                                    }
                                }
                                $scope.PracticeLocationDetailsHistory[i].PracticeColleagues[k].PracticeProviderSpecialties = tempPracticeProviderSpecialties;
                                for (var l = 0; l < $scope.PracticeLocationDetailsHistory[i].PracticeColleagues[k].PracticeProviderTypes.length; l++) {
                                    if ($scope.PracticeLocationDetailsHistory[i].PracticeColleagues[k].PracticeProviderTypes[l].StatusType == '1') {
                                        tempPracticeProviderTypes.push($scope.PracticeLocationDetailsHistory[i].PracticeColleagues[k].PracticeProviderTypes[l]);
                                    }
                                }
                                $scope.PracticeLocationDetailsHistory[i].PracticeColleagues[k].PracticeProviderTypes = tempPracticeProviderTypes;
                            }
                        }
                    }
                }
                $scope.showPracticeLocationHistoryTable = true;
                $("#" + loadingId).css('display', 'none');
            });
        }

        else {
            $scope.showPracticeLocationHistoryTable = true;
            }
    
    }

    $scope.cancelPLHistory = function () {
        $scope.showPracticeLocationHistoryTable = false;
    }

    // rootScoped on emitted value catches the value for the model and insert to get the old data
    //calling the method using $on(PSP-public subscriber pattern)
    $rootScope.$on('PracticeLocationDetails', function (event, val) {
        //console.log(val);
        $scope.PracticeLocationDetails = val;
        if (val) {
            for (var i = 0; i < $scope.PracticeLocationDetails.length ; i++) {
                if (!$scope.PracticeLocationDetails[i].PracticingGroupId) { $scope.PracticeLocationDetails[i].PracticingGroupId = ""; }
                $scope.PracticeLocationDetails[i].MidlevelPractioners = [];
                $scope.PracticeLocationDetails[i].SupervisingProviders = [];
                $scope.PracticeLocationDetails[i].PracticeColleagues = [];
                /* Parsing the date format in client side */
                //if ($scope.PracticeLocationDetails[i].StartDate)
                //$scope.PracticeLocationDetails[i].StartDate = ConvertDateFormat($scope.PracticeLocationDetails[i].StartDate);

                //if ($scope.PracticeLocationDetails[i].WorkersCompensationInformation) {
                //    $scope.PracticeLocationDetails[i].WorkersCompensationInformation.IssueDate = ConvertDateFormat($scope.PracticeLocationDetails[i].WorkersCompensationInformation.IssueDate);
                //    $scope.PracticeLocationDetails[i].WorkersCompensationInformation.ExpirationDate = ConvertDateFormat($scope.PracticeLocationDetails[i].WorkersCompensationInformation.ExpirationDate);
                //}

                if (!$scope.PracticeLocationDetails[i].Facility.FacilityDetail.PracticeOfficeHour) {
                    $scope.PracticeLocationDetails[i].Facility.FacilityDetail.PracticeOfficeHour = {};
                    $scope.PracticeLocationDetails[i].Facility.FacilityDetail.PracticeOfficeHour.PracticeDays = $scope.OriginalPracticeDays;
                }

                if (!$scope.PracticeLocationDetails[i].OfficeHour) {
                    $scope.PracticeLocationDetails[i].OfficeHour = $scope.PracticeLocationDetails[i].Facility.FacilityDetail.PracticeOfficeHour;
                }
                var FacilityMidLevels = [];
                for (var j = 0; j < $scope.PracticeLocationDetails[i].Facility.FacilityDetail.FacilityPracticeProviders.length; j++) {
                    if ($scope.PracticeLocationDetails[i].Facility.FacilityDetail.FacilityPracticeProviders[j].Status != 'Inactive') {
                        FacilityMidLevels.push($scope.PracticeLocationDetails[i].Facility.FacilityDetail.FacilityPracticeProviders[j]);
                    }
                }
                $scope.PracticeLocationDetails[i].Facility.FacilityDetail.FacilityPracticeProviders = FacilityMidLevels;
                for (var j = 0; j < $scope.PracticeLocationDetails[i].PracticeProviders.length; j++) {
                    if ($scope.PracticeLocationDetails[i].PracticeProviders[j].Practice == 'Supervisor' && $scope.PracticeLocationDetails[i].PracticeProviders[j].Status != 'Inactive') {
                        $scope.PracticeLocationDetails[i].SupervisingProviders.push($scope.PracticeLocationDetails[i].PracticeProviders[j]);
                    }
                    if ($scope.PracticeLocationDetails[i].PracticeProviders[j].Practice == 'Midlevel' && $scope.PracticeLocationDetails[i].PracticeProviders[j].Status != 'Inactive') {
                        $scope.PracticeLocationDetails[i].MidlevelPractioners.push($scope.PracticeLocationDetails[i].PracticeProviders[j]);
                    }
                    if ($scope.PracticeLocationDetails[i].PracticeProviders[j].Practice == 'CoveringColleague' && $scope.PracticeLocationDetails[i].PracticeProviders[j].Status != 'Inactive') {
                        $scope.PracticeLocationDetails[i].PracticeColleagues.push($scope.PracticeLocationDetails[i].PracticeProviders[j]);
                        for (var k = 0; k < $scope.PracticeLocationDetails[i].PracticeColleagues.length; k++) {
                            tempPracticeProviderSpecialties = [];
                            tempPracticeProviderTypes = [];
                            for (var l = 0; l < $scope.PracticeLocationDetails[i].PracticeColleagues[k].PracticeProviderSpecialties.length; l++) {
                                if ($scope.PracticeLocationDetails[i].PracticeColleagues[k].PracticeProviderSpecialties[l].StatusType == '1') {
                                    tempPracticeProviderSpecialties.push($scope.PracticeLocationDetails[i].PracticeColleagues[k].PracticeProviderSpecialties[l]);
                                }
                            }
                            $scope.PracticeLocationDetails[i].PracticeColleagues[k].PracticeProviderSpecialties = tempPracticeProviderSpecialties;
                            for (var l = 0; l < $scope.PracticeLocationDetails[i].PracticeColleagues[k].PracticeProviderTypes.length; l++) {
                                if ($scope.PracticeLocationDetails[i].PracticeColleagues[k].PracticeProviderTypes[l].StatusType == '1') {
                                    tempPracticeProviderTypes.push($scope.PracticeLocationDetails[i].PracticeColleagues[k].PracticeProviderTypes[l]);
                                }
                            }
                            $scope.PracticeLocationDetails[i].PracticeColleagues[k].PracticeProviderTypes = tempPracticeProviderTypes;
                        }
                    }
                }
            }
        }
    });

    //To get country code and to show the div
    $scope.CountryDialCodes = countryDailCodes;

    $scope.showContryCodeDiv = function (countryCodeDivId) {
        changeVisibilityOfCountryCode();
        $("#" + countryCodeDivId).show();
    };

    //To clear data and error message on change event
    $scope.changemade = function () {
        $('.field-validation-error').removeClass('field-validation-error').addClass('field-validation-valid');
        $('.input-validation-error').removeClass('input-validation-error').addClass('valid');
    };

    //To clear data on No click from Business Manager and Office Manger 
    $scope.clear = function () {
        $scope.tempSecondObject.FirstName = "";
        $scope.tempSecondObject.MiddleName = "";
        $scope.tempSecondObject.LastName = "";
        $scope.tempSecondObject.Telephone = "";
        $scope.tempSecondObject.EmailAddress = "";
        $scope.tempSecondObject.Fax = "";
        $scope.tempSecondObject.CountryCodeFax = "";
        $scope.tempSecondObject.CountryCodeTelephone = "";
        $scope.tempSecondObject.County = "";
        $scope.tempSecondObject.POBoxAddress = "";
        $scope.tempSecondObject.State = "";
        $scope.tempSecondObject.Street = "";
        $scope.tempSecondObject.City = "";
        $scope.tempSecondObject.Building = "";
        $scope.tempSecondObject.Country = "";
        $scope.PracticeSpecialties = [];
        $scope.PracticeProviderTypes = [];
        $scope.tempSecondObject.NPINumber = "";
    }

    //To clear data on No click from payment and remittance
    $scope.clearPayment = function () {
        $scope.tempSecondObject.ElectronicBillingCapabilityYesNoOption = "";
        $scope.tempSecondObject.BillingDepartment = "";
        $scope.tempSecondObject.CheckPayableTo = "";
        $scope.tempSecondObject.Office = "";
        $scope.tempSecondObject.PaymentAndRemittancePerson.FirstName = "";
        $scope.tempSecondObject.PaymentAndRemittancePerson.MiddleName = "";
        $scope.tempSecondObject.PaymentAndRemittancePerson.LastName = "";
        $scope.tempSecondObject.PaymentAndRemittancePerson.Telephone = "";
        $scope.tempSecondObject.PaymentAndRemittancePerson.EmailAddress = "";
        $scope.tempSecondObject.PaymentAndRemittancePerson.Fax = "";
        $scope.tempSecondObject.PaymentAndRemittancePerson.CountryCodeFax = "";
        $scope.tempSecondObject.PaymentAndRemittancePerson.CountryCodeTelephone = "";
        $scope.tempSecondObject.PaymentAndRemittancePerson.County = "";
        $scope.tempSecondObject.PaymentAndRemittancePerson.Country = "";
        $scope.tempSecondObject.PaymentAndRemittancePerson.POBoxAddress = "";
        $scope.tempSecondObject.PaymentAndRemittancePerson.State = "";
        $scope.tempSecondObject.PaymentAndRemittancePerson.Street = "";
        $scope.tempSecondObject.PaymentAndRemittancePerson.City = "";
        $scope.tempSecondObject.PaymentAndRemittancePerson.Building = "";
        $scope.tempSecondObject.PaymentAndRemittancePerson.ZipCode = "";
    }

    //For filling data for selected office Manager/Billing contact from drop down
    $scope.provideData = function (empId, array, PracticeLocationDetail) {
        var data = $filter('filter')(array, { EmployeeID: empId })[0];
        $scope.tempSecondObject.FirstName = data.FirstName;
        $scope.tempSecondObject.MiddleName = data.MiddleName;
        $scope.tempSecondObject.LastName = data.LastName;
        $scope.tempSecondObject.Telephone = data.Telephone;
        $scope.tempSecondObject.EmailAddress = data.EmailAddress;
        $scope.tempSecondObject.Fax = data.Fax;
        $scope.tempSecondObject.CountryCodeFax = data.CountryCodeFax;
        $scope.tempSecondObject.CountryCodeTelephone = data.CountryCodeTelephone;
        $scope.tempSecondObject.POBoxAddress = data.POBoxAddress;
        $scope.tempSecondObject.Country = data.Country;
        $scope.tempSecondObject.County = data.County;
        $scope.tempSecondObject.State = data.State;
        $scope.tempSecondObject.Street = data.Street;
        $scope.tempSecondObject.City = data.City;
        $scope.tempSecondObject.Building = data.Building;
        $scope.tempSecondObject.ZipCode = data.ZipCode;
    }

    //For filling data for selected payment and remittance from drop down
    $scope.paymentData = function (empId, array) {
        var data = $filter('filter')(array, { PracticePaymentAndRemittanceID: empId })[0]        
        $scope.tempSecondObject.ElectronicBillingCapabilityYesNoOption = data.ElectronicBillingCapabilityYesNoOption;
        $scope.tempSecondObject.BillingDepartment = data.BillingDepartment;
        $scope.tempSecondObject.CheckPayableTo = data.CheckPayableTo;
        $scope.tempSecondObject.Office = data.Office;
        $scope.tempSecondObject.PaymentAndRemittancePerson = {};
        $scope.tempSecondObject.PaymentAndRemittancePerson.FirstName = data.PaymentAndRemittancePerson.FirstName;
        $scope.tempSecondObject.PaymentAndRemittancePerson.MiddleName = data.PaymentAndRemittancePerson.MiddleName;
        $scope.tempSecondObject.PaymentAndRemittancePerson.LastName = data.PaymentAndRemittancePerson.LastName;
        $scope.tempSecondObject.PaymentAndRemittancePerson.Telephone = data.PaymentAndRemittancePerson.Telephone;
        $scope.tempSecondObject.PaymentAndRemittancePerson.EmailAddress = data.PaymentAndRemittancePerson.EmailAddress;
        $scope.tempSecondObject.PaymentAndRemittancePerson.Fax = data.PaymentAndRemittancePerson.Fax;
        $scope.tempSecondObject.PaymentAndRemittancePerson.CountryCodeFax = data.PaymentAndRemittancePerson.CountryCodeFax;
        $scope.tempSecondObject.PaymentAndRemittancePerson.CountryCodeTelephone = data.PaymentAndRemittancePerson.CountryCodeTelephone;
        $scope.tempSecondObject.PaymentAndRemittancePerson.County = data.PaymentAndRemittancePerson.County;
        $scope.tempSecondObject.PaymentAndRemittancePerson.State = data.PaymentAndRemittancePerson.State;
        $scope.tempSecondObject.PaymentAndRemittancePerson.Street = data.PaymentAndRemittancePerson.Street;
        $scope.tempSecondObject.PaymentAndRemittancePerson.City = data.PaymentAndRemittancePerson.City;
        $scope.tempSecondObject.PaymentAndRemittancePerson.Building = data.PaymentAndRemittancePerson.Building;
        $scope.tempSecondObject.PaymentAndRemittancePerson.Country = data.PaymentAndRemittancePerson.Country;
    }


    /*************************************************************************************************/
    /*********************************** Office Manager **********************************************/
    /*************************************************************************************************/

    // Save office manager Details
    $scope.saveOfficeManager = function (PracticeLocationDetail, index) {
     
        var validationStatus = null;
        var url = null;
        var $formData = null;

        $formData = $('#BusinessOfficeContactPersonForm' + index);
        ResetFormForValidation($formData);
            validationStatus = $formData.valid();
            url = rootDir + "/Profile/PracticeLocation/AddOfficeManagerAsync?profileId=" + profileId;
       
        if (validationStatus) {
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

                        PracticeLocationDetail.BusinessOfficeManagerOrStaff = data.officemanager;
                        //$scope.PracticeLocationDetails.push(data.practiceLocationDetail);
                        $scope.managers.push(data.officemanager);
                        $rootScope.operateSecondCancelControl('');
                        messageAlertEngine.callAlertMessage("businessManagerSuccessMsg", "Office manager/Business Office Staff Contact Information Updated successfully.", "success", true);

                    }
                    else {
                        messageAlertEngine.callAlertMessage("alertOfficeManager", data.status, "danger", true);
                    }
                },
                error: function (e) {

                }
            });
        }
    };


    /*************************************************************************************************/
    /*********************************** Billing Contact **********************************************/
    /*************************************************************************************************/

    // Save Billing Contact Details
    $scope.saveBillingContact = function (PracticeLocationDetail, index) {
        
        var validationStatus = null;
        var url = null;
        var $formData = null;

        $formData = $('#BillingContactForm' + index);
        ResetFormForValidation($formData);
            validationStatus = $formData.valid();
            url = rootDir + "/Profile/PracticeLocation/AddBillingContactAsync?profileId=" + profileId;
  
        if (validationStatus) {
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
                        PracticeLocationDetail.BillingContactPerson = data.billingcontact;
                        //$scope.PracticeLocationDetails.push(data.practiceLocationDetail);
                        //$scope.billings.push(data.billingcontact);
                        $rootScope.operateSecondCancelControl('');
                        messageAlertEngine.callAlertMessage("billingContactSuccessMsg", "Billing Contact Information Updated successfully.", "success", true);
                    }
                    else {
                        messageAlertEngine.callAlertMessage("alertBillingContact", data.status, "danger", true);
                    }
                },
                error: function (e) {

                }
            });
        }
    };

    //For Address
    $scope.addressAutocomplete1 = function (location1) {
        if (location1.length == 0) {
            $scope.resetAddressModels1();
        }
        $scope.tempSecondObject.City = location1;
        if (location1.length > 1 && !angular.isObject(location1)) {
            locationService.getLocations(location1).then(function (val) {
                $scope.Locations = val;
            });
        } else if (angular.isObject(location1)) {
            $scope.setAddressModels1(location1);
        }
    };

    $scope.selectedLocation1 = function (location1) {
        $scope.setAddressModels1(location1);
        $(".ProviderTypeSelectAutoList").hide();
    };

    $scope.resetAddressModels1 = function () {
        $scope.tempSecondObject.City = "";
        $scope.tempSecondObject.State = "";
        $scope.tempSecondObject.Country = "";
    };

    $scope.setAddressModels1 = function (location1) {

        $scope.tempSecondObject.City = location1.City;
        $scope.tempSecondObject.State = location1.State;
        $scope.tempSecondObject.Country = location1.Country;

        };






    //===================================address for credentialing contact================

    $scope.addressAutocomplete4 = function (location4) {
        if (location4.length == 0) {
            $scope.resetAddressModels4();
        }
        $rootScope.tempObject.City = location4;
        if (location4.length > 1 && !angular.isObject(location4)) {
            locationService.getLocations(location4).then(function (val) {
                $scope.Locations = val;
            });
        } else if (angular.isObject(location4)) {
            $scope.setAddressModels4(location4);
        }
    };

    $scope.selectedLocation4 = function (location4) {
        $scope.setAddressModels4(location4);
        $(".ProviderTypeSelectAutoList").hide();
    };

    $scope.resetAddressModels4 = function () {
        $rootScope.tempObject.City = "";
        $rootScope.tempObject.State = "";
        $rootScope.tempObject.Country = "";
    };

    $scope.setAddressModels4 = function (location4) {

        $rootScope.tempObject.City = location4.City;
        $rootScope.tempObject.State = location4.State;
        $rootScope.tempObject.Country = location4.Country;

    };

    //====================================================================================

    /*************************************************************************************************/
    /*********************************** Payment And Remittance **********************************************/
    /*************************************************************************************************/

    // Save Payment and remittance Details
    $scope.savePayment = function (PracticeLocationDetail, index) {

        var validationStatus = null;
        var url = null;
        var $formData = null;

        $formData = $('#PaymentForm' + index);
        ResetFormForValidation($formData);
            validationStatus = $formData.valid();
            url = rootDir + "/Profile/PracticeLocation/AddPaymentAndRemittanceAsync?profileId=" + profileId;
       
        if (validationStatus) {
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
                        PracticeLocationDetail.PaymentAndRemittance = data.paymentandremittance;
                       
                        $rootScope.operateSecondCancelControl('');
                        messageAlertEngine.callAlertMessage("paymentSuccessMsg", "Payment and Remittance Information Updated successfully.", "success", true);

                    }
                    else {
                        messageAlertEngine.callAlertMessage("alertPayment", data.status, "danger", true);
                    }
                },
                error: function (e) {

                }
            });
        }
    };

    //For Address
    $scope.addressAutocomplete2 = function (location2) {
        if (location2.length == 0) {
            $scope.resetAddressModels2();
        }
        $scope.tempSecondObject.City = location2;
        if (location2.length > 1 && !angular.isObject(location2)) {
            locationService.getLocations(location2).then(function (val) {
                $scope.Locations = val;
            });
        } else if (angular.isObject(location2)) {
            $scope.setAddressModels2(location2);
        }
    };

    $scope.selectedLocation2 = function (location2) {
        $scope.setAddressModels2(location2);
        $(".ProviderTypeSelectAutoList").hide();
    };

    $scope.resetAddressModels2 = function () {
        $scope.tempSecondObject.PaymentAndRemittancePerson.City = "";
        $scope.tempSecondObject.PaymentAndRemittancePerson.State = "";
        $scope.tempSecondObject.PaymentAndRemittancePerson.Country = "";
    };

    $scope.setAddressModels2 = function (location2) {

        $scope.tempSecondObject.PaymentAndRemittancePerson.City = location2.City;
        $scope.tempSecondObject.PaymentAndRemittancePerson.State = location2.State;
        $scope.tempSecondObject.PaymentAndRemittancePerson.Country = location2.Country;

        };

    

    /* Facility Information Action Methods Start*/

    $scope.saveFacilityInformaton = function (typeOfSave, index) {

        var validationStatus = false;
        var url = null;
        var $formData = null;

        $formData = $('#newFacilityDataForm' + index);
        url = rootDir + "/Profile/PracticeLocation/AddFacilityAsync?profileId=" + profileId;
        if (typeOfSave == 'Update') {
            url = rootDir + "/Profile/PracticeLocation/UpdateFacilityAsync?profileId=" + profileId;
        }

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


                        //var organization = $scope.getOrganizationById($scope.OrganizationID);

                        //if (organization.Facilities == null) {
                        //    organization.Facilities = new Array();
                        //}

                        if (typeOfSave == 'Update') {
                            $scope.PracticeLocationDetails[index].Facility = data.facility;
                            $scope.operateAddControlPracLoc(index + '_viewPracticeLOcation');
                            for (var i = 0; i < $scope.PracticeLocationDetails.length; i++) {
                                if ($scope.PracticeLocationDetails[i].FacilityId == data.facility.FacilityID) {
                                    $scope.PracticeLocationDetails[i].Facility = data.facility;
                                    if (typeof $scope.PracticeLocationDetails[i].OfficeHour.AnyTimePhoneCoverage == 'undefined') {
                                        $scope.PracticeLocationDetails[i].OfficeHour.PracticeDays = data.facility.FacilityDetail.PracticeOfficeHour.PracticeDays;
                                    }
                                }
                            }
                            $scope.operateCancelControl('');
                            messageAlertEngine.callAlertMessage("updatedFacility", "Facility Information updated successfully !!!!", "success", true);
                        }
                        else {
                        $scope.facilities.push(data.facility);
                        $scope.operateAddControlPracLoc('addPracticeLocation'); 
                            messageAlertEngine.callAlertMessage("addedNewFacility", "New Facility Information saved successfully !!!!", "success", true);
                            //console.log(data.facility);
                            $scope.tempSecondObject.FacilityId = data.facility.FacilityID;
                            $scope.tempSecondObject.PracticeLocationCorporateName = data.facility.Name;
                        }
                        //$scope.resetDates();
                        //$scope.resetPracticeDaysList();
                        FormReset($formData);
                    } else {
                        messageAlertEngine.callAlertMessage("facilityDataErrorMsg", data.status, "danger", true);
                    }
                },
                error: function (e) {

                }
            });


        }
    };

    /* Facility Information Action Methods End*/

    $scope.savePracticeLocationDetailInformaton = function () {

        var validationStatus = false;
        var url = null;
        var $formData = null;

        $formData = $('#addPracticeLocationDetails');


        url = rootDir + "/Profile/PracticeLocation/savePracticeLocationDetailInformaton?profileId=" + profileId;


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
                    //console.log("Saved data");
                    //console.log(data);
                    if (data.status == "true") {
                        if ($scope.PracticeLocationDetails == null) {

                            $scope.PracticeLocationDetails = new Array();
                        }
                        if (!data.PracticingGroupId) { data.PracticingGroupId = ""; }
                        data.practiceLocationDetail.StartDate = ConvertDateFormat(data.practiceLocationDetail.StartDate);
                        data.practiceLocationDetail.Facility = $scope.getFacilityById(data.practiceLocationDetail.FacilityId);
                        var groupName = $($formData[0]).find($("[name='PracticingGroupId'] option:selected")).text();
                        data.practiceLocationDetail.Group = {};
                        data.practiceLocationDetail.Group.Group = {};
                        if (groupName != '-- Select IPA --') {
                            data.practiceLocationDetail.Group.Group.Name = groupName;
                        } else {
                            data.practiceLocationDetail.Group.Group.Name = '';
                        }
                        
                        if (!data.practiceLocationDetail.OfficeHour) {
                            data.practiceLocationDetail.OfficeHour = data.practiceLocationDetail.Facility.FacilityDetail.PracticeOfficeHour;
                        }
                        
                        $scope.PracticeLocationDetails.push(data.practiceLocationDetail);
                        //console.log(">>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>");
                        //console.log($scope.PracticeLocationDetails);
                        $scope.operateAddControlPracLoc(($scope.PracticeLocationDetails.length - 1) + '_viewPracticeLOcation');
                        messageAlertEngine.callAlertMessage("addedNewPracticeLocation", "New Practice Location Information saved successfully !!!!", "success", true);
                    } else {
                        messageAlertEngine.callAlertMessage("addedNewPracticeLocation", data.status, "danger", true);
                    }
                },
                error: function (e) {

                }
            });


        }
    };

    //------------------------------------------------------------------------------------------------------------------//
    //-------------------------------------------------- Office Hours --------------------------------------------------//
    //------------------------------------------------------------------------------------------------------------------//

    $scope.OriginalPracticeDays = [
        { DayName: "Monday", DayOfWeek: 0, DayOff: 'NO', DailyHours: [{ StartTime: "08:30", EndTime: "16:30" }] },
        { DayName: "Tuesday", DayOfWeek: 1, DayOff: 'NO', DailyHours: [{ StartTime: "08:30", EndTime: "16:30" }] },
        { DayName: "Wednesday", DayOfWeek: 2, DayOff: 'NO', DailyHours: [{ StartTime: "08:30", EndTime: "16:30" }] },
        { DayName: "Thursday", DayOfWeek: 3, DayOff: 'NO', DailyHours: [{ StartTime: "08:30", EndTime: "16:30" }] },
        { DayName: "Friday", DayOfWeek: 4, DayOff: 'NO', DailyHours: [{ StartTime: "08:30", EndTime: "16:30" }] },
        { DayName: "Saturday", DayOfWeek: 5, DayOff: 'YES', DailyHours: [{ StartTime: "08:30", EndTime: "16:30" }] },
        { DayName: "Sunday", DayOfWeek: 6, DayOff: 'YES', DailyHours: [{ StartTime: "08:30", EndTime: "16:30" }] }
    ];

    $scope.resetPracticeDaysList = function () {
        $rootScope.tempObject.PracticeDays = $scope.OriginalPracticeDays;
        
    };

    $scope.setFacilityPracticeDays = function (practiceLocationDetail) {
        $rootScope.tempObject.PracticeDays = practiceLocationDetail.Facility.FacilityDetail.PracticeOfficeHour.PracticeDays;
};

    $scope.addDailyHour = function (DailyHours) {
        DailyHours.push({ StartTime: "08:00", EndTime: "17:00" });
    };

    $scope.removeDailyHour = function (DailyHours, index) {
        DailyHours.splice(index, 1);
    };

    $scope.dayOffToggel = function (PracticeDay) {
        PracticeDay.DayOff = PracticeDay.DayOff == 'YES' ? 'NO' : 'YES';
    };


    $scope.validateDailyHours = function (PracticeDays, parent, subsection, index, typeOfSave) {
        $scope.tempLanguages = [];
        $scope.tempLanguages = angular.copy(Languages);
        $scope.hideClock();
        var status = true;
        console.log("============================");
        console.log(PracticeDays);
        console.log("============================");
        for (practiceDay in PracticeDays) {
            var prevStartHour = "";
            var prevStartMin = "";
            var prevEndHour = "";
            var prevEndMin = ""
            for (dailyHour in PracticeDays[practiceDay].DailyHours) {
                if (!$('#startTime_' + practiceDay + dailyHour).prop('disabled') || !$('#endTime_' + practiceDay + dailyHour).prop('disabled')) {
                    var startTime = $('#startTime_' + practiceDay + dailyHour).val(); //PracticeDays[practiceDay].DailyHours[dailyHour].StartTime;
                    var endTime = $('#endTime_' + practiceDay + dailyHour).val(); //PracticeDays[practiceDay].DailyHours[dailyHour].EndTime;
                    if (startTime != 'Day Off' && endTime != 'Day Off') {
                        var startHour = parseInt(startTime.split(':')[0]);
                        var startMin = parseInt(startTime.split(':')[1]);

                        var endHour = parseInt(endTime.split(':')[0]);
                        var endMin = parseInt(endTime.split(':')[1]);

                        //console.log(startHour + " H " + startMin + " H " + endHour + " H " + endMin + " M");

                        if (!startTime.match(":") || !endTime.match(":") || startTime.indexOf(":") == 0 || endTime.indexOf(":") == 0) {
                            $('#msg_' + practiceDay + dailyHour).text("Please Enter a Valid Time.");
                            status = false;
                        }
                        else if ((startHour == endHour && startMin > endMin) || startHour > endHour) {
                            $('#msg_' + practiceDay + dailyHour).text("Start Time Should Not Be Greater than End Time.");
                            status = false;
                        }
                        else if (startHour == endHour && startMin == endMin) {
                            $('#msg_' + practiceDay + dailyHour).text("Start Time And End Time Should Not Be Same.");
                            status = false;
                        }
                        else if (dailyHour > 0) {
                            if ((prevEndHour == startHour && prevEndMin > startMin) || prevEndHour > startHour) {
                                $('#msg_' + practiceDay + dailyHour).text("Start Time Should not be Less than Previous End Time.");
                                status = false;
                            }
                            else {
                                $('#msg_' + practiceDay + dailyHour).text("");
                            }
                        }
                        else {
                            $('#msg_' + practiceDay + dailyHour).text("");
                        }
                        prevStartHour = startHour;
                        prevStartMin = startMin;
                        prevEndHour = endHour;
                        prevEndMin = endMin;
                    }
                }
            }
        }

        if (status && subsection == 'facility') {
            $scope.saveFacilityInformaton(typeOfSave, index);
        }
        else if (status && subsection == 'ProviderOfficeHour') {
            $scope.updateOfficeHours(parent, index);
        }

    };


    $scope.updateOfficeHours = function (PracticeLocationDetail, index) {
        $scope.hideClock();
        var validationStatus = false;
        var url = null;
        var $formData = null;

        $formData = $('#OfficeHourForm' + index);

        url = rootDir + "/Profile/PracticeLocation/updateOfficeHours?profileId=" + profileId;

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

                        PracticeLocationDetail.OfficeHour = data.providerPracticeOfficeHours;
                        $rootScope.operateSecondCancelControl('');

                        messageAlertEngine.callAlertMessage("providerPracticeOfficeHoursSuccessMsg", "Office Hour Updated successfully.", "success", true);
                        } else {
                        messageAlertEngine.callAlertMessage("providerPracticeOfficeHoursErrorMsg", data.status, "danger", true);
                    }
                    
                },
                error: function (e) {

                }
            });


        }
    };

    $scope.hideClock = function () {
        $(".clockface").hide();
        //$("#providerofficehoursdiv").find(".samay").each(function () {
        //    //try {
        //    //    if ($(this).hasClass("samay")) {
        //            $(this).clockface('hide');
        //    //    }
        //    //}
        //    //catch(err){}
            
        //})
    };

    //------------------------------------------------------------------------------------------------------------------//
    //----------------------------------------------- Facility Languages -----------------------------------------------//
    //------------------------------------------------------------------------------------------------------------------//

    //console.log("===============" + Languages);
    $scope.tempLanguages = angular.copy(Languages);
    //console.log($scope.tempLanguages);


    $scope.showLanguageList = function (divToBeDisplayed) {
        $("#" + divToBeDisplayed).show();
    };

    $scope.SelectLanguage = function (selectedLanguage) {

        $rootScope.tempObject.FacilityDetail.Language.NonEnglishLanguages.push({
            NonEnglishLanguageID: null,
            Language: selectedLanguage.name,
            InterpretersAvailableYesNoOption: 1,
            StatusType: 1
        });
        $scope.tempLanguages.splice($scope.tempLanguages.indexOf(selectedLanguage), 1);
        $scope.searchLang = "";
        $(".LanguageSelectAutoList").hide();
    };


    $scope.DeselectLanguage = function (language) {
        $rootScope.tempObject.FacilityDetail.Language.NonEnglishLanguages.splice($rootScope.tempObject.FacilityDetail.Language.NonEnglishLanguages.indexOf(language), 1);

        for (var i in Languages) {
            if (Languages[i].name == language.Language) {
                $scope.tempLanguages.push(Languages[i]);
            }
        }

        $scope.tempLanguages.sort(function (a, b) {
            if (a.name > b.name) {
                return 1;
            }
            if (a.name < b.name) {
                return -1;
            }
            // a must be equal to b
            return 0;
        });

    };


    //$scope.saveFacilityLanguage = function () {

    //    var validationStatus = false;
    //    var url = null;
    //    var $formData = null;

    //    $formData = $('#languageInfoForm');

    //    url = "/Profile/PracticeLocation/saveFacilityLanguage";

    //    // ResetFormForValidation($formData);

    //    validationStatus = true; //$formData.valid();

    //    if (validationStatus) {
    //        //Simple POST request example (passing data) :
    //        $.ajax({
    //            url: url,
    //            type: 'POST',
    //            data: new FormData($formData[0]),
    //            async: false,
    //            cache: false,
    //            contentType: false,
    //            processData: false,
    //            success: function (data) {
    //                //console.log("Reached Destination");
    //            },
    //            error: function (e) {

    //            }
    //        });


    //    }
    //};

    //--------------------------------------------------------------------------------------------------------------------//
    //----------------------------------------------- Workers Compensation -----------------------------------------------//
    //--------------------------------------------------------------------------------------------------------------------//

    $scope.RenewDiv = function () {
        $scope.IsRenew = true;
    }; 
    $scope.RenewHide = function () {
        $scope.IsRenew = false;
    };
    $scope.updateWorkersCompensationInformation = function (PracticeLocationDetail, index) {

        // the implementation has to be changed to angularjs http.post to incorporate the one to many relationship which cant be captured in form data

        var validationStatus = false;
        var url = null;
        var $formData = null;

        $formData = $('#WorkerCompensationForm' + index);

        if ($scope.IsRenew == false) {
            url = rootDir + "/Profile/PracticeLocation/UpdateWorkersCompensationInformationAsync?profileId=" + profileId;
        }
        else {
            url = rootDir + "/Profile/PracticeLocation/RenewWorkersCompensationInformationAsync?profileId=" + profileId;
         }

        ResetFormForValidation($formData);

        //console.log($formData);
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
                        data.workersCompensationInformation.IssueDate = ConvertDateFormat(data.workersCompensationInformation.IssueDate);
                        data.workersCompensationInformation.ExpirationDate = ConvertDateFormat(data.workersCompensationInformation.ExpirationDate);
                        //console.log(data.workersCompensationInformation);
                        PracticeLocationDetail.WorkersCompensationInformation = data.workersCompensationInformation;
                        $rootScope.operateSecondCancelControl('');

                        messageAlertEngine.callAlertMessage("workersCompensationInformationSuccessMsg", "Worker's Compensation Information Updated successfully.", "success", true);
                    } else {
                        messageAlertEngine.callAlertMessage("workersCompensationInformationErrorMsg", data.status, "danger", true);
                    }
                },
                error: function (e) {

                }
            });


        }
    };


    //--------------------------------------------------------------------------------------------------------------------//
    //----------------------------------------------- Open Practice Status -----------------------------------------------//
    //--------------------------------------------------------------------------------------------------------------------//

    $scope.saveOpenPracticeStatus = function (PracticeLocationDetail, index) {

        var validationStatus = false;
        var url = null;
        var $formData = null;

        $formData = $('#practiceOpenStatusForm' + index);
        url = rootDir + "/Profile/PracticeLocation/UpdateOpenPracticeStatusAsync?profileId=" + profileId;
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
                        PracticeLocationDetail.OpenPracticeStatus = data.openPracticeStatus;
                       
                        $rootScope.operateSecondCancelControl('');
                        messageAlertEngine.callAlertMessage("openPracticeStatusSuccessMsg", "Open Practice Status Updated successfully.", "success", true);
                    } else {
                        messageAlertEngine.callAlertMessage("openPracticeStatusErrorMsg", data.status, "danger", true);
                    }
                },
                error: function (e) {

                }
            });
        }
    };

    $scope.validateForAge = function (index) {
        $formData = $('#practiceOpenStatusForm' + index);
        ResetFormForValidation($formData);
        validationStatus = $formData.valid();
    }

    $rootScope.$on("LoadRequireMasterDataPracticeLocation", function () {
    // Calling master data service to get all organization practicing group info
        masterDataService.getMasterData(rootDir + "/MasterData/Organization/GetGroups").then(function (GetAllPracticingGroup) {
        $scope.groups = GetAllPracticingGroup;
    });
    });
    
    // Utility function to get facility by ID

    $scope.getFacilityById = function (facilityId) {
        
        for (fac in $scope.facilities) {
                if ($scope.facilities[fac].FacilityID == parseInt(facilityId)) {
                    return $scope.facilities[fac];
                }
            }
        
        return null;
    };

    $scope.fillCurrentlyPracticingYesNoOption = function (value) {
        if (value == '1') {
            $scope.tempSecondObject.CurrentlyPracticingYesNoOption = '1';
        }
    }
  
    $scope.updatePracticeLocationDetailInformaton = function (index) {

        var validationStatus = false;
        var url = null;
        var $formData = null;

        $formData = $('#updatePracticeLocationDetails' + index);


        url = rootDir + "/Profile/PracticeLocation/updatePracticeLocationDetailInformaton?profileId=" + profileId;


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
                        $scope.PracticeLocationDetails[index].StartDate = ConvertDateFormat(data.practiceLocationDetail.StartDate);
                        $scope.PracticeLocationDetails[index].IsPrimary = data.practiceLocationDetail.IsPrimary;
                        $scope.PracticeLocationDetails[index].GroupName = data.practiceLocationDetail.GroupName;
                        $scope.PracticeLocationDetails[index].PrimaryYesNoOption = data.practiceLocationDetail.PrimaryYesNoOption;
                        $scope.PracticeLocationDetails[index].PracticeExclusively = data.practiceLocationDetail.PracticeExclusively;
                        $scope.PracticeLocationDetails[index].PracticeExclusivelyYesNoOption = data.practiceLocationDetail.PracticeExclusivelyYesNoOption;
                        $scope.PracticeLocationDetails[index].CurrentlyPracticingAtThisAddress = data.practiceLocationDetail.CurrentlyPracticingAtThisAddress;
                        $scope.PracticeLocationDetails[index].CurrentlyPracticingYesNoOption = data.practiceLocationDetail.CurrentlyPracticingYesNoOption;
                        $scope.PracticeLocationDetails[index].SendGeneralCorrespondence = data.practiceLocationDetail.SendGeneralCorrespondence;
                        $scope.PracticeLocationDetails[index].GeneralCorrespondenceYesNoOption = data.practiceLocationDetail.GeneralCorrespondenceYesNoOption;
                        $scope.PracticeLocationDetails[index].PrimaryTaxId = data.practiceLocationDetail.PrimaryTaxId;
                        $scope.PracticeLocationDetails[index].PrimaryTax = data.practiceLocationDetail.PrimaryTax;
                        $scope.PracticeLocationDetails[index].PracticingGroupId = data.practiceLocationDetail.PracticingGroupId;
                        $scope.PracticeLocationDetails[index].PracticeLocationCorporateName = data.practiceLocationDetail.PracticeLocationCorporateName;
                        
                       
                        // updating the visibility control

                        $scope.GeneralInformationEdit = false;


                        messageAlertEngine.callAlertMessage("UpdatePracticeLocation", "Practice Location Information updated successfully !!!!", "success", true);
                    } else {
                        messageAlertEngine.callAlertMessage("otherInformationErrorMsg", data.status, "danger", true);
                    }
                },
                error: function (e) {
                    messageAlertEngine.callAlertMessage("otherInformationErrorMsg", "Sorry for Inconvenience !!!! Please Try Again Later...", "danger", true);
                }
            });


        }
    };

    $rootScope.$on("LoadRequireMasterDataPracticeLocation", function () {
    // Calling master data service to get all provider types

        masterDataService.getMasterData(rootDir + "/Profile/MasterData/GetAllProviderTypes").then(function (Providertypes) {
        $scope.ProviderTypes = Providertypes;
    });
    });
   
    // Save Mid level Details
    $scope.saveMidLevel = function (PracticeLocationDetailID) {

        var validationStatus = null;
        var url = null;
        var $formData = null;
        $formData = $('#addnewMidLevel');
        
        validationStatus = $formData.valid();
        url = rootDir + "/Profile/PracticeLocation/addMidLevelAsync?practiceLocationDetailID=" + PracticeLocationDetailID + "&profileId=" + profileId;
        
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
                        if ($scope.PracticeLocationDetails === null) { $scope.PracticeLocationDetails = [] };
                        $scope.PracticeLocationDetails.push(data.practiceLocationDetail);
                    } else {

                    }
                },
                error: function (e) {

                }
            });
            
        }
    };


    $scope.MidLevelProviderPractitioner = {};

    $scope.operateMidlevelAddControl = function (viewSwitch) {

        $scope.addmidlevelNew = true;
        if (viewSwitch == 'edit')
            $scope.addmidlevelNew = false;
    };
    
    
    $scope.validateMidLevelProviderPractitioner = function (dataObject, dataScope, action) {
        var status = true;

        var fname = '#FnameMsg';
        var mname = '#MnameMsg';
        var lname = '#LnameMsg';
        var npinumber = '#NPINumberMsg';

        if (dataScope == 'inner') {
            fname = '#FnameMsgInner';
            mname = '#MnameMsgInner';
            lname = '#LnameMsgInner';
            npinumber = '#NPINumberMsgInner';
        }

        $(fname).text("");
        $(mname).text("");
        $(lname).text("");
        $(npinumber).text("");

        var nameRegx = "^[a-zA-Z ,-.]*$";
        

        if (dataObject.FirstName == null || dataObject.FirstName == "") {
            status = false;
            $(fname).text("Please enter First Name.");
        }
        else if (dataObject.FirstName.match(nameRegx) == null && (dataObject.FirstName != null || dataObject.FirstName != "")) {
            status = false;
            $(fname).text("Please enter valid First Name. Only alphabets, spaces, comma, hyphen and dot accepted.");
        }
        else if (dataObject.FirstName.length < 2 || dataObject.FirstName.length > 50) {
            status = false;
            $(fname).text("First Name should be between 2 to 50 characters in length.");
        }


        if (dataObject.MiddleName == null || dataObject.MiddleName == "") {
            $(mname).text("");
        }
        else if (dataObject.MiddleName.match(nameRegx) == null && (dataObject.MiddleName != null || dataObject.MiddleName != "")) {
            status = false;
            $(mname).text("Please enter valid Middle Name. Only alphabets, spaces, comma, hyphen and dot accepted.");
        }
        else if (dataObject.MiddleName != null && (dataObject.MiddleName.length < 2 || dataObject.MiddleName.length > 50)) {
            status = false;
            $(mname).text("Middle Name should be between 2 to 50 characters in length.");
        }
        
        

        if (dataObject.LastName == null || dataObject.LastName == "") {
            status = false;
            $(lname).text("Please enter Last Name.");
        }
        else if (dataObject.LastName.match(nameRegx) == null && (dataObject.LastName != null || dataObject.LastName != "")) {
            status = false;
            $(lname).text("Please enter valid Last Name. Only alphabets, spaces, comma, hyphen and dot accepted.");
        }
        else if (dataObject.LastName.length < 2 || dataObject.LastName.length > 50) {
            status = false;
            $(lname).text("Last Name should be between 2 to 50 characters in length.");
        }
        

        if (dataObject.NPINumber == null || dataObject.NPINumber == "") {
            status = false;
            $(npinumber).text("Please enter NPI Number.");
                }
        else if (isNaN(dataObject.NPINumber) && (dataObject.NPINumber != null || dataObject.NPINumber != "")) {
            status = false;
            $(npinumber).text("Please enter valid NPI Number. Only digits accepted.");
        }
        else if (dataObject.NPINumber.length != 10) {
            status = false;
            $(npinumber).text("NPI Number should of 10 digits.");
        }
        else if (action == 'add' && $filter('filter')($rootScope.tempObject.FacilityDetail.FacilityPracticeProviders, { NPINumber: dataObject.NPINumber })[0] != null) {
            status = false;
            $(npinumber).text("Mid-Level Practioner with this NPI Number already added.");
        }
        else if (action == 'update' && $filter('filter')($rootScope.tempObject.FacilityDetail.FacilityPracticeProviders, { NPINumber: dataObject.NPINumber }).length > 1) {
            status = false;
            $(npinumber).text("Mid-Level Practioner with this NPI Number already added.");
        }

        return status;
    };

    $scope.addFacilityMidlevelPractioners = function (dataObject, dataScope) {
        if (!angular.isDefined($rootScope.tempObject.FacilityDetail.FacilityPracticeProviders)) {
            $rootScope.tempObject.FacilityDetail.FacilityPracticeProviders = [];
        }
        if ($scope.validateMidLevelProviderPractitioner(dataObject, dataScope, 'add')) {
            $rootScope.tempObject.FacilityDetail.FacilityPracticeProviders.push({
                'FacilityPracticeProviderID': $scope.MidLevelProviderPractitioner.FacilityPracticeProviderID,
                'PracticeType': 'Midlevel',
                'RelationType': $scope.MidLevelProviderPractitioner.RelationType,
                'FirstName': $scope.MidLevelProviderPractitioner.FirstName,
                'MiddleName': $scope.MidLevelProviderPractitioner.MiddleName,
                'LastName': $scope.MidLevelProviderPractitioner.LastName,
                'NPINumber': $scope.MidLevelProviderPractitioner.NPINumber,
                'StatusType': 'Active'
            });
            
            $scope.resetMidLevelProviderPractitioner();
        }
        
    };

    $scope.updateFacilityMidlevelPractioners = function (midLevels, tempThirdObject, index, dataScope) {
        if ($scope.validateMidLevelProviderPractitioner(tempThirdObject, dataScope, 'update')) {
            midLevels[index] = tempThirdObject;
            //console.log(tempSecondObject);
            $rootScope.operateThirdCancelControl();
        }
    };

    $scope.setFacilityMidLevelPractitioner = function (practitioner) {
        if (angular.isObject(practitioner)) {
            $scope.MidLevelProviderPractitioner.FirstName = practitioner.PersonalDetail.FirstName;
            $scope.MidLevelProviderPractitioner.MiddleName = practitioner.PersonalDetail.MiddleName;
            $scope.MidLevelProviderPractitioner.LastName = practitioner.PersonalDetail.LastName;
            $scope.MidLevelProviderPractitioner.NPINumber = practitioner.OtherIdentificationNumber.NPINumber;
            $(".ProviderTypeSelectAutoList").hide();
        }
    };

    $scope.removeMidLevelProviderPractitioner = function (practitioner, visibilityControlPracLoc) {
        //console.log("====>>>>"+visibilityControlPracLoc);
        if (visibilityControlPracLoc == "addPracticeLocationNew") {
            $rootScope.tempObject.FacilityDetail.FacilityPracticeProviders.splice($rootScope.tempObject.FacilityDetail.FacilityPracticeProviders.indexOf(practitioner), 1);
        }
        else {
            //practitioner.StatusType = "Inactive";
            practitioner.StatusType = 2;
            $rootScope.tempObject.FacilityDetail.FacilityPracticeProviders.splice($rootScope.tempObject.FacilityDetail.FacilityPracticeProviders.indexOf(practitioner), 1);
        }
        if ($rootScope.tempObject.FacilityDetail.FacilityPracticeProviders.length < 1) {
            $scope.resetMidLevelProviderPractitioner();
        }
    };

    $scope.resetMidLevelProviderPractitioner = function () {
        $scope.MidLevelProviderPractitioner = {};
        $scope.addmidlevelNew ? $scope.addmidlevelNew = false : $scope.addmidlevelNew = true;
    };

    //======================================Primary Credentialing Contact=================



    $scope.radioOption1 = function (index) {
    
        $scope.credDropDown = false;
        if ($scope.PracticeLocationDetails[index].BusinessOfficeManagerOrStaff != null) {

            $rootScope.tempObject.FirstName = $scope.PracticeLocationDetails[index].BusinessOfficeManagerOrStaff.FirstName;
            $rootScope.tempObject.MiddleName = $scope.PracticeLocationDetails[index].BusinessOfficeManagerOrStaff.MiddleName;
            $rootScope.tempObject.LastName = $scope.PracticeLocationDetails[index].BusinessOfficeManagerOrStaff.LastName;
            $rootScope.tempObject.Telephone = $scope.PracticeLocationDetails[index].BusinessOfficeManagerOrStaff.Telephone;
            $rootScope.tempObject.Fax = $scope.PracticeLocationDetails[index].BusinessOfficeManagerOrStaff.Fax;
            $rootScope.tempObject.EmailAddress = $scope.PracticeLocationDetails[index].BusinessOfficeManagerOrStaff.EmailAddress;
            $rootScope.tempObject.Building = $scope.PracticeLocationDetails[index].Facility.Building;
            $rootScope.tempObject.City = $scope.PracticeLocationDetails[index].Facility.City;
            $rootScope.tempObject.Country = $scope.PracticeLocationDetails[index].Facility.Country;
            $rootScope.tempObject.Street = $scope.PracticeLocationDetails[index].Facility.Street;
            $rootScope.tempObject.ZipCode = $scope.PracticeLocationDetails[index].Facility.ZipCode;
            $rootScope.tempObject.State = $scope.PracticeLocationDetails[index].Facility.State;
            $rootScope.tempObject.County = $scope.PracticeLocationDetails[index].Facility.County;
          
        }
                 
    }

    $scope.radioOption3 = function () {

        $scope.credDropDown = false;

        $rootScope.tempObject.FirstName = "";
        $rootScope.tempObject.MiddleName = "";
        $rootScope.tempObject.LastName = "";
        $rootScope.tempObject.Telephone = "";
        $rootScope.tempObject.Fax = "";
        $rootScope.tempObject.EmailAddress = "";
        $rootScope.tempObject.Building = "";
        $rootScope.tempObject.City = "";
        $rootScope.tempObject.Country = "";
        $rootScope.tempObject.Street = "";
        $rootScope.tempObject.ZipCode = "";
        $rootScope.tempObject.State = "";
        $rootScope.tempObject.County = "";
    }

    $scope.radioOption2 = function () {

     
        $rootScope.tempObject.FirstName = "";
        $rootScope.tempObject.MiddleName = "";
        $rootScope.tempObject.LastName = "";
        $rootScope.tempObject.Telephone = "";
        $rootScope.tempObject.Fax = "";
        $rootScope.tempObject.EmailAddress = "";
        $rootScope.tempObject.Building = "";
        $rootScope.tempObject.City = "";
        $rootScope.tempObject.Country = "";
        $rootScope.tempObject.Street = "";
        $rootScope.tempObject.ZipCode = "";
        $rootScope.tempObject.State = "";
        $rootScope.tempObject.County = "";
        $scope.credDropDown = true;
    }

    $scope.getDropDownValue = function (empObj) {
        empObj = $filter('filter')($scope.credentialingContact, { EmployeeID: empObj })[0];

        $rootScope.tempObject.FirstName = empObj.FirstName;
        $rootScope.tempObject.MiddleName = empObj.MiddleName;
        $rootScope.tempObject.LastName = empObj.LastName;
        $rootScope.tempObject.Telephone = empObj.Telephone;
        $rootScope.tempObject.Fax = empObj.Fax;
        $rootScope.tempObject.EmailAddress = empObj.EmailAddress;
        $rootScope.tempObject.Building = empObj.Building;
        $rootScope.tempObject.City = empObj.City;
        $rootScope.tempObject.Country = empObj.Country;
        $rootScope.tempObject.Street = empObj.Street;
        $rootScope.tempObject.ZipCode = empObj.ZipCode;
        $rootScope.tempObject.State = empObj.State;
        $rootScope.tempObject.County = empObj.County;
   
    
    }

    $scope.cancelCredendialingContact = function () {
    
        $scope.credDropDown = false;
        $rootScope.operateCancelControl('');
    
    }


    $scope.saveCredentialingContact = function (index) {

        var validationStatus = false;
        var url = null;
        var formData = null;
        $scope.credDropDown = false;

        if ($scope.visibilityControl == 'addCc') {
            //Add Details - Denote the URL
            formData = $('#newShowCredentialingContactDiv').find('form');
            url = rootDir + "/Profile/PracticeLocation/AddCredentialingContact";
        }
        else if ($scope.visibilityControl == ('editCc')) {
            //Update Details - Denote the URL
            formData = $('#CredentialingContactEditDiv').find('form');
            url = rootDir + "/Profile/PracticeLocation/UpdateCredentialingContact";
        }

        ResetFormForValidation(formData);
        validationStatus = formData.valid()


        if (validationStatus) {
        
            $.ajax({
                url: url,
                type: 'POST',
                data: new FormData(formData[0]),
                async: false,
                cache: false,
                contentType: false,
                processData: false,
                success: function (data) {


                    if (data.status == "true") {


                        if ($scope.visibilityControl == 'addCc') {
                        
                            $scope.PracticeLocationDetails[index].PrimaryCredentialingContactPerson = data.credentialingContact;
                            $rootScope.operateCancelControl('');
                            messageAlertEngine.callAlertMessage("addedNewCredentialingContactInformation", "Primary Credentialing Contact added successfully !!!!", "success", true);
                        }


                        else {
                    
                            $scope.PracticeLocationDetails[index].PrimaryCredentialingContactPerson = data.credentialingContact;
                            $rootScope.operateCancelControl('');
                            messageAlertEngine.callAlertMessage("updatedCredentialingContactInformation", "Primary Credentialing Contact updated successfully !!!!", "success", true);
                    
                    }

                        FormReset(formData);

                    }



                    else {
                        messageAlertEngine.callAlertMessage('errorCredentialingContractInformation', "", "danger", true);
                        $scope.errorCredentialingContractInformation = data.status.split(",");

                }

                },
        
                error: function (e) {
                    messageAlertEngine.callAlertMessage('errorCredentialingContractInformation' + index, "", "danger", true);
                    $scope.errorCredentialingContractInformation = "Sorry for Inconvenience !!!! Please Try Again Later...";
        }

            });

    }
        loadingOff();

    };
    
    
    
   







    //-----------------------------------------------------------------------------------------------------------------------------//
    //------------------------------------------------------ Mid Level Practitioners ----------------------------------------------//
    //-----------------------------------------------------------------------------------------------------------------------------//

    $scope.tempMidLevelPractitionersList = [];
    
    $scope.addMidLevelToTempList = function (practitioner) {
        $scope.tempMidLevelPractitionersList.push(practitioner);
    };

    $scope.removeFromMidLevelTempList = function (practitioner) {
        $scope.tempMidLevelPractitionersList.splice($scope.tempMidLevelPractitionersList.indexOf(practitioner), 1);
    };

    //$scope.setMidLevelPractitioner = function (practitioner) {
    //    if (angular.isObject(practitioner)) {
    //        $scope.tempThirdObject.FirstName = practitioner.PersonalDetail.FirstName;
    //        $scope.tempThirdObject.MiddleName = practitioner.PersonalDetail.MiddleName;
    //        $scope.tempThirdObject.LastName = practitioner.PersonalDetail.LastName;
    //        $scope.tempThirdObject.NPINumber = practitioner.OtherIdentificationNumber.NPINumber;
    //        $(".ProviderTypeSelectAutoList").hide();
    //    }
    //};

    $scope.setMidLevelPractitioner = function (practitioner) {
        if (angular.isObject(practitioner)) {
            $scope.tempThirdObject.FirstName = practitioner.FirstName;
            $scope.tempThirdObject.MiddleName = practitioner.MiddleName;
            $scope.tempThirdObject.LastName = practitioner.LastName;
            $scope.tempThirdObject.NPINumber = practitioner.NPINumber;
            $(".ProviderTypeSelectAutoList").hide();
        }
    };

    $scope.resetMidLevelPractitioner = function () { 
        $scope.MidLevelPractitioner = {};
        $scope.tempMidLevelPractitionersList = [];
    };

    $scope.initWarning = function (practitioner) {
        if (angular.isObject(practitioner)) {
            $scope.tempMidLevelpractitioner = practitioner;
        }
        $('#warningModal').modal();
    };

    $scope.addMultipleMidlevelPractioners = function (PracticeLocationDetail, index) {
        var validationStatus = false;
        var url = null;
        var $formData = null;

        $formData = $('#MidLevelPractitionerForm' + index);

        url = rootDir + "/Profile/PracticeLocation/AddPracticeProvidersAsync";

        ResetFormForValidation($formData);

        //console.log($formData);
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
                        if (PracticeLocationDetail.MidlevelPractioners == null)
                            PracticeLocationDetail.MidlevelPractioners = [];

                        PracticeLocationDetail.MidlevelPractioners = data.practiceProviders;
                        $rootScope.operateSecondCancelControl('');

                        $scope.resetMidLevelPractitioner();

                        messageAlertEngine.callAlertMessage("midlevelPractionersSuccessMsg", "Mid-Level Practitioners Added successfully.", "success", true);


                    } else {
                        messageAlertEngine.callAlertMessage("midlevelPractionersErrorMsg", data.status, "danger", true);
                    }
                    $scope.tempMidLevelPractitionersList = [];
                },
                error: function (e) {
                    
                }
            });


        }
    };


    $scope.addMidlevelPractioners = function (PracticeLocationDetail, index) {
        var validationStatus = false;
        var url = null;
        var $formData = null;

        $formData = $('#MidLevelPractitionerForm' + index);

        url = rootDir + "/Profile/PracticeLocation/AddPracticeProviderAsync";
        if ($scope.tempThirdObject.PracticeProviderID) {
            url = rootDir + "/Profile/PracticeLocation/UpdatePracticeProviderAsync?profileId=" + profileId;
        }

        ResetFormForValidation($formData);

        //console.log($formData);
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
                        if ($scope.tempThirdObject.PracticeProviderID) {
                            PracticeLocationDetail.MidlevelPractioners[index] = data.practiceProvider;
                            $rootScope.operateThirdViewAndAddControl(index + '_ViewMidLevelPractitioner');
                            messageAlertEngine.callAlertMessage("midlevelPractionersEditSuccessMsg", "Mid-Level Practitioner Updated successfully.", "success", true);
                        } else {
                            if (PracticeLocationDetail.MidlevelPractioners == null)
                                PracticeLocationDetail.MidlevelPractioners = [];

                            //console.log(data.midlevelPractioner);

                            PracticeLocationDetail.MidlevelPractioners.push(data.practiceProvider);
                            $rootScope.operateSecondCancelControl('');

                            $scope.resetMidLevelPractitioner();

                            messageAlertEngine.callAlertMessage("midlevelPractionersSuccessMsg", "Mid-Level Practitioner Added successfully.", "success", true);
                        }

                    } else {
                        messageAlertEngine.callAlertMessage("midlevelPractionersErrorMsg", data.status, "danger", true);
                    }
                },
                error: function (e) {

                }
            });


        }
    };


    $scope.removeMidLevelPractitioner = function (PracticeLocationDetail) {
        //console.log("=================================");
        //console.log(PracticeLocationDetail);
        var validationStatus = false;
        var url = null;
        var $formData = null;

        $formData = $('#editMidlevelPractioner');

        url = rootDir + "/Profile/PracticeLocation/RemovePracticeProviderAsync";

        ResetFormForValidation($formData);

        //console.log($formData);
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
                        var obj = $filter('filter')(PracticeLocationDetail.MidlevelPractioners, { PracticeProviderID: data.practiceProvider.PracticeProviderID })[0];
                        PracticeLocationDetail.MidlevelPractioners.splice(PracticeLocationDetail.MidlevelPractioners.indexOf(obj), 1);
                        $('#warningModal').modal('hide');
                        PracticeLocationDetail.MidlevelPractioners.splice();
                        $rootScope.operateThirdCancelControl('');
                        messageAlertEngine.callAlertMessage("midlevelPractionersSuccessMsg", "Mid-Level Practitioner Removed successfully.", "success", true);
                    } else {
                        messageAlertEngine.callAlertMessage("midlevelPractionersRemoveErrorMsg", data.status, "danger", true);
                    }
                },
                error: function (e) {

                }
            });


        }
    };

    $scope.toggleList = function () {
        //console.log($(".ProviderTypeSelectAutoList"));
        $(".ProviderTypeSelectAutoList").show();
    };




    //-----------------------------------------------------------------------------------------------------------------------------//
    //------------------------------------------------------ Supervising Providers ----------------------------------------------//
    //-----------------------------------------------------------------------------------------------------------------------------//

    
    $scope.setSupervisingProvider = function (practitioner) {
        if (angular.isObject(practitioner)) {
            $scope.tempThirdObject.FirstName = practitioner.PersonalDetail.FirstName;
            $scope.tempThirdObject.MiddleName = practitioner.PersonalDetail.MiddleName;
            $scope.tempThirdObject.LastName = practitioner.PersonalDetail.LastName;
            $scope.tempThirdObject.NPINumber = practitioner.OtherIdentificationNumber.NPINumber;
            $(".ProviderTypeSelectAutoList").hide();
        }
    };

    $scope.resetSupervisingProvider = function () {
        $scope.SupervisingProvider = {};
    };

    $scope.initSupervisingWarning = function (practitioner) {
        if (angular.isObject(practitioner)) {
            $scope.tempSupervisingProvider = practitioner;
        }
        $('#supervisingWarningModal').modal();
    };


    $scope.addSupervisingProvider = function (PracticeLocationDetail, index) {
        var validationStatus = false;
        var url = null;
        var $formData = null;

        $formData = $('#SupervisingProvider' + index);

        url = rootDir + "/Profile/PracticeLocation/AddPracticeProviderAsync";
        if ($scope.tempThirdObject.PracticeProviderID) {
            url = rootDir + "/Profile/PracticeLocation/UpdatePracticeProviderAsync?profileId=" + profileId;
        }

        ResetFormForValidation($formData);

        //console.log($formData);
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
                        if ($scope.tempThirdObject.PracticeProviderID) {
                            PracticeLocationDetail.SupervisingProviders[index] = data.practiceProvider;
                            $rootScope.operateThirdViewAndAddControl(index + '_ViewSupervisingPractitioner');
                            messageAlertEngine.callAlertMessage("supervisingProvidersEditSuccessMsg", "Supervising Provider Updated successfully.", "success", true);
                        } else {
                        if (PracticeLocationDetail.SupervisingProviders == null)
                            PracticeLocationDetail.SupervisingProviders = [];

                        //console.log(data.midlevelPractioner);

                            PracticeLocationDetail.SupervisingProviders.push(data.practiceProvider);
                        $rootScope.operateSecondCancelControl('');

                        $scope.resetSupervisingProvider();

                        messageAlertEngine.callAlertMessage("supervisingProvidersSuccessMsg", "Supervising Provider Added successfully.", "success", true);
                        }
                    } else {
                        messageAlertEngine.callAlertMessage("supervisingProvidersErrorMsg", data.status, "danger", true);
                    }
                },
                error: function (e) {

                }
            });


        }
    };

    $scope.removeSupervisingProvider = function (PracticeLocationDetail) {
        //console.log("=================================");
        //console.log(PracticeLocationDetail);
        var validationStatus = false;
        var url = null;
        var $formData = null;

        $formData = $('#editSupervisingPractioner');

        url = rootDir + "/Profile/PracticeLocation/RemovePracticeProviderAsync";

        ResetFormForValidation($formData);

        //console.log($formData);
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
                        var obj = $filter('filter')(PracticeLocationDetail.SupervisingProviders, { PracticeProviderID: data.practiceProvider.PracticeProviderID })[0];
                        PracticeLocationDetail.SupervisingProviders.splice(PracticeLocationDetail.SupervisingProviders.indexOf(obj), 1);
                        $('#supervisingWarningModal').modal('hide');
                        PracticeLocationDetail.SupervisingProviders.splice();
                        $rootScope.operateThirdCancelControl('');
                        messageAlertEngine.callAlertMessage("supervisingProvidersSuccessMsg", "Supervising Provider Removed successfully.", "success", true);
                    } else {
                        $('#supervisingWarningModal').modal('hide');
                        messageAlertEngine.callAlertMessage("supervisingProvidersRemoveErrorMsg", data.status, "danger", true);
                    }
                },
                error: function (e) {

                }
            });


        }
    };



    //---------------------------------------------------------------------------------------------------------------------------------------------------//

    $scope.fillCorporateName = function (val) {
        var selectedFacility = $filter('filter')($scope.facilities, { FacilityID: val })[0];
        $scope.tempSecondObject.PracticeLocationCorporateName = selectedFacility.Name;
    }

    $scope.assignCityComponentForFacility = function () {
        $scope.tempSecondObject.City = $rootScope.tempObject.City;
        $scope.tempSecondObject.State = $rootScope.tempObject.State;
        $scope.tempSecondObject.Country = $rootScope.tempObject.Country;
    }


    //-----------------------------------------------------------------------------------------------------------------------------//
    //------------------------------------------------------ Covering Colleagues ----------------------------------------------//
    //-----------------------------------------------------------------------------------------------------------------------------//

    
    //$scope.tempSecondObject = {};

    //To Display the drop down div
    $scope.searchCumDropDown = function (event) {
        $(event.target).parent().find(".ProviderTypeSelectAutoList").first().show();
    };

    $scope.showContryCodeDivForColleague = function (countryCodeDivId) {
        changeVisibilityOfCountryCode();
        $("#" + countryCodeDivId).show();
    };

    //Bind the supervisor with model class to achieve search cum drop down
    $scope.addIntoSupervisorDropDown = function (supervisor, $event) {
        $scope.tempSecondObject.NPINumber = supervisor.OtherIdentificationNumber.NPINumber;
        $scope.tempSecondObject.FirstName = supervisor.PersonalDetail.FirstName;
        $scope.tempSecondObject.MiddleName = supervisor.PersonalDetail.MiddleName;
        $scope.tempSecondObject.LastName = supervisor.PersonalDetail.LastName;

        $scope.PracticeSpecialties = [];

        for (var i = 0; i < supervisor.SpecialtyDetails.length; i++) {
            $scope.PracticeSpecialties.push({
                PracticeProviderSpecialtyId: null,
                Specialty: supervisor.SpecialtyDetails[i].Specialty,
                SpecialtyID: supervisor.SpecialtyDetails[i].SpecialtyID,
                StatusType: 1
            });
        }
        
        $scope.PracticeProviderTypes = [];

        for (var i = 0; i < supervisor.PersonalDetail.ProviderTitles.length; i++) {
            $scope.PracticeProviderTypes.push({
                PracticeProviderTypeId: null,
                ProviderType: supervisor.PersonalDetail.ProviderTitles[i].ProviderType,
                ProviderTypeID: supervisor.PersonalDetail.ProviderTitles[i].ProviderTypeId,
                StatusType: 1
            
            });
        }
        
        for (var i = 0; i < supervisor.PracticeLocationDetails.length; i++) {
            if (supervisor.PracticeLocationDetails[i].PrimaryYesNoOption == 1) {
                $scope.tempSecondObject.Street = supervisor.PracticeLocationDetails[i].Facility.Street;
                $scope.tempSecondObject.Building = supervisor.PracticeLocationDetails[i].Facility.Building;
                $scope.tempSecondObject.City = supervisor.PracticeLocationDetails[i].Facility.City;
                $scope.tempSecondObject.State = supervisor.PracticeLocationDetails[i].Facility.State;
                $scope.tempSecondObject.ZipCode = supervisor.PracticeLocationDetails[i].Facility.ZipCode;
                $scope.tempSecondObject.Country = supervisor.PracticeLocationDetails[i].Facility.Country;
                $scope.tempSecondObject.County = supervisor.PracticeLocationDetails[i].Facility.County;
                $scope.tempSecondObject.CountryCodeTelephone = supervisor.PracticeLocationDetails[i].Facility.CountryCodeTelephone;
                $scope.tempSecondObject.Telephone = supervisor.PracticeLocationDetails[i].Facility.Telephone;
            }
        }
        
        
        $(".ProviderTypeSelectAutoList").hide();
    }

    //$scope.setSupervisor = function (practitioner) {
    //    if (angular.isObject(practitioner)) {
    //        $scope.tempSecondObject = practitioner;
    //        $(".ProviderTypeSelectAutoList").hide();
    //    }
    //};

    

    //$scope.resetSupervisor = function () {
    //    $scope.tempSecondObject = {};
    //};

    //For Address
    $scope.addressAutocomplete3 = function (location3) {
        if (location3.length == 0) {
            $scope.resetAddressModels3();
        }
        $scope.tempSecondObject.City = location3;
        if (location3.length > 1 && !angular.isObject(location3)) {
            locationService.getLocations(location3).then(function (val) {
                $scope.Locations = val;
            });
        } else if (angular.isObject(location3)) {
            $scope.setAddressModels3(location3);
        }
    };

    $scope.selectedLocation3 = function (location3) {
        $scope.setAddressModels3(location3);
        $(".ProviderTypeSelectAutoList").hide();
    };

    $scope.resetAddressModels3 = function () {
        $scope.tempSecondObject.City = "";
        $scope.tempSecondObject.State = "";
        $scope.tempSecondObject.Country = "";
    };

    $scope.setAddressModels3 = function (location3) {

        $scope.tempSecondObject.City = location3.City;
        $scope.tempSecondObject.State = location3.State;
        $scope.tempSecondObject.Country = location3.Country;

    };


    //Selecting multiple provider types    

    $scope.showProviderTypeList = function (event) {
        $(event.target).parent().find(".ProviderTypeSelectAutoList").first().show();
    };

    $scope.PracticeProviderTypes = [];

    $scope.SelectProviderType = function (providertype) {
        
        $scope.PracticeProviderTypes.push({
            PracticeProviderTypeId: null,
            ProviderType: providertype,
            ProviderTypeID: providertype.ProviderTypeID,
            StatusType: 1
        });

        $(".ProviderTypeSelectAutoList").hide();
        $scope.masterProviderTypes.splice($scope.masterProviderTypes.indexOf(providertype), 1);
    };

    //------------------------------------- UN-select Provider type -----------------------------------------
    $scope.ActionProviderType = function (providertype) {
        
        for (var i = 0; i < $scope.PracticeProviderTypes.length; i++) {
            if ($scope.PracticeProviderTypes[i].ProviderTypeID == providertype.ProviderTypeID) {
                $scope.PracticeProviderTypes[i].StatusType = 2;
                $scope.masterProviderTypes.push(providertype);
            }
        }
        
        //$scope.PracticeProviderTypes.splice($scope.PracticeProviderTypes.indexOf(providertype), 1);
        
        
    };

    $scope.PracticeSpecialties = [];
    //$scope.tempMasterSpecialties = [];
    //$scope.tempMasterSpecialties = angular.copy($scope.masterSpecialties);

    $scope.SelectSpecalties = function (specialty) {

        $scope.PracticeSpecialties.push({
            PracticeProviderSpecialtyId: null,
            Specialty: specialty,
            SpecialtyID: specialty.SpecialtyID,
            StatusType: 1
        });
        $(".ProviderTypeSelectAutoList").hide();
        $scope.masterSpecialties.splice($scope.masterSpecialties.indexOf(specialty), 1);
    };

    //------------------------------------- UN-select Provider type -----------------------------------------
    $scope.ActionSpecialty = function (specialty) {

        for (var i = 0; i < $scope.PracticeSpecialties.length; i++) {
            if ($scope.PracticeSpecialties[i].SpecialtyID == specialty.SpecialtyID) {
                $scope.PracticeSpecialties[i].StatusType = 2;
                $scope.masterSpecialties.push(specialty);
            }
        }

        //$scope.PracticeSpecialties.splice($scope.PracticeSpecialties.indexOf(specialty), 1);

    };

    $scope.setValue = function (partner) {

        $scope.PracticeSpecialties = [];
        $scope.PracticeProviderTypes = [];

        for (var i = 0; i < partner.PracticeProviderSpecialties.length; i++) {
            if (partner.PracticeProviderSpecialties[i].StatusType == 1) {
                $scope.PracticeSpecialties.push({
                    PracticeProviderSpecialtyId: partner.PracticeProviderSpecialties[i].PracticeProviderSpecialtyId,
                    Specialty: partner.PracticeProviderSpecialties[i].Specialty,
                    SpecialtyID: partner.PracticeProviderSpecialties[i].SpecialtyID,
                    StatusType: partner.PracticeProviderSpecialties[i].StatusType
                });
            }
            
        }

        for (var i = 0; i < partner.PracticeProviderTypes.length; i++) {
            if (partner.PracticeProviderTypes[i].StatusType == 1) {
                $scope.PracticeProviderTypes.push({
                    PracticeProviderTypeId: partner.PracticeProviderTypes[i].PracticeProviderTypeId,
                    ProviderType: partner.PracticeProviderTypes[i].ProviderType,
                    ProviderTypeID: partner.PracticeProviderTypes[i].ProviderTypeID,
                    StatusType: partner.PracticeProviderTypes[i].StatusType
            
            });
            }

            
        }       
        
    }

    //$scope.clear = function () {
    //    $scope.PracticeSpecialties = [];
    //    $scope.PracticeProviderTypes = [];
    //    $scope.tempSecondObject.NPINumber = "";
    //    $scope.tempSecondObject.FirstName = "";
    //    $scope.tempSecondObject.MiddleName = "";
    //    $scope.tempSecondObject.LastName = "";
    //    $scope.tempSecondObject.Street = "";
    //    $scope.tempSecondObject.Building = "";
    //    $scope.tempSecondObject.City = "";
    //    $scope.tempSecondObject.State = "";
    //    $scope.tempSecondObject.ZipCode = "";
    //    $scope.tempSecondObject.Country = "";
    //    $scope.tempSecondObject.County = "";
    //    $scope.tempSecondObject.CountryCodeTelephone = "";
    //    $scope.tempSecondObject.Telephone = "";
    //}

    $scope.clearAll = function () {
        $scope.PracticeSpecialties = [];
        $scope.PracticeProviderTypes = [];
    }

    // Save Covering Colleagues Details
    $scope.savePatner = function (PracticeLocationDetail, index) {
        //$scope.tempMasterSpecialties = [];
        //$scope.tempMasterSpecialties = angular.copy($scope.masterSpecialties);
        var validationStatus = true;
        var url = null;
        var $formData = null;
       
        $formData = $('#newShowPartnersDiv').find('form');
        
        url = rootDir + "/Profile/PracticeLocation/AddPracticeProviderAsync";

        if ($scope.tempSecondObject.PracticeProviderID) {
            $formData = $('#partnersEditDiv' + index).find('form');
            url = rootDir + "/Profile/PracticeLocation/UpdatePracticeProviderAsync?profileId=" + profileId;
        }

        ResetFormForValidation($formData);

        //console.log($formData);
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
                    if ($scope.tempSecondObject.PracticeProviderID) {
                        tempPracticeProviderSpecialties = [];
                        for (var i = 0; i < $scope.PracticeSpecialties.length; i++) {
                            if ($scope.PracticeSpecialties[i].StatusType == '1') {
                                tempPracticeProviderSpecialties.push($scope.PracticeSpecialties[i]);
                                //data.practiceProvider.PracticeProviderSpecialties[i].Specialty = $scope.PracticeSpecialties[i].Specialty;
                            }                            
                        }
                        data.practiceProvider.PracticeProviderSpecialties = tempPracticeProviderSpecialties;

                        tempPracticeProviderTypes = [];
                        for (var i = 0; i < $scope.PracticeProviderTypes.length; i++) {
                            if ($scope.PracticeProviderTypes[i].StatusType == '1') {
                                tempPracticeProviderTypes.push($scope.PracticeProviderTypes[i]);
                                //data.practiceProvider.PracticeProviderTypes[i].ProviderType = $scope.PracticeProviderTypes[i].ProviderType;
                            }                            
                        }
                        data.practiceProvider.PracticeProviderTypes = tempPracticeProviderTypes;

                        $scope.masterSpecialties = [];
                        $scope.masterSpecialties = angular.copy($scope.copyMasterSpecialties);
                        $scope.masterProviderTypes = [];
                        $scope.masterProviderTypes = angular.copy($scope.copyMasterProviderTypes);
                        PracticeLocationDetail.PracticeColleagues[index] = data.practiceProvider;
                        $rootScope.operateSecondViewAndAddControl(index + '_viewPartner');
                            messageAlertEngine.callAlertMessage("updatedPartnerDetails" + index, "Covering Colleagues information Updated successfully.", "success", true);
                    } else {
                        if (PracticeLocationDetail.PracticeColleagues == null)
                            PracticeLocationDetail.PracticeColleagues = [];

                        //console.log(data.midlevelPractioner);
                        var obj = $filter('filter')(PracticeLocationDetail.PracticeColleagues, { PracticeProviderID: data.practiceProvider.PracticeProviderID })[0];

                        for (var i = 0; i < $scope.PracticeSpecialties.length; i++) {
                            if ($scope.PracticeSpecialties[i].StatusType == 1) {
                                try{
                                    data.practiceProvider.PracticeProviderSpecialties[i].Specialty = $scope.PracticeSpecialties[i].Specialty;
                                }
                                catch (e) {
                                    console.log("covering Colleague");
                                }
                            }
                            
                        }

                        for (var i = 0; i < $scope.PracticeProviderTypes.length; i++) {
                            if ($scope.PracticeProviderTypes[i].StatusType == 1) {
                                data.practiceProvider.PracticeProviderTypes[i].ProviderType = $scope.PracticeProviderTypes[i].ProviderType;
                            }
                            
                        }
                        $scope.masterSpecialties = [];
                        $scope.masterSpecialties = angular.copy($scope.copyMasterSpecialties);
                        $scope.masterProviderTypes = [];
                        $scope.masterProviderTypes = angular.copy($scope.copyMasterProviderTypes);
                        PracticeLocationDetail.PracticeColleagues.push(data.practiceProvider);                        
                        $rootScope.operateSecondCancelControl('');                        
                        //$scope.tempSecondObject = {};
                        $scope.PracticeSpecialties = [];
                        $scope.PracticeProviderTypes = [];
                        messageAlertEngine.callAlertMessage("addedPartnerDetails", "Covering Colleagues Information Added successfully.", "success", true);
                    }
                } else {
                    messageAlertEngine.callAlertMessage("ErrorInPartners" + index, data.status, "danger", true);
                }
            },
            error: function (e) {

            }
        });
           
    }
};


    $scope.initWarningMethod = function (practitioner) {
        if (angular.isObject(practitioner)) {
            $scope.tempPracticeColleague = practitioner;
    }
        $('#warningModalMethod').modal();
    };


    // remove Covering Colleagues Details    
    $scope.removePatner = function (PracticeLocationDetail) {
        
        var validationStatus = false;
        var url = null;
        var $formData = null;

        $formData = $('#editPracticeColleague');

        url = rootDir + "/Profile/PracticeLocation/RemovePracticeProviderAsync";

        ResetFormForValidation($formData);

        //console.log($formData);
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
                        var obj = $filter('filter')(PracticeLocationDetail.PracticeColleagues, { PracticeProviderID: data.practiceProvider.PracticeProviderID })[0];
                        PracticeLocationDetail.PracticeColleagues.splice(PracticeLocationDetail.PracticeColleagues.indexOf(obj), 1);
                        $('#warningModalMethod').modal('hide');
                        PracticeLocationDetail.PracticeColleagues.splice();
                    $rootScope.operateSecondCancelControl('');
                        messageAlertEngine.callAlertMessage("addedPartnerDetails", "Covering Colleague Removed successfully.", "success", true);
                } else {
                    messageAlertEngine.callAlertMessage("ErrorInPartners", data.status, "danger", true);
                }
            },
            error: function (e) {
            }
        });
    }
    };

    //---------------------------------Removal of Practice Location--------------------------------
    
    $scope.initPracticeLocationWarning = function (PracticeLocationDetail) {
        if (angular.isObject(PracticeLocationDetail)) {
            $scope.tempPracticeLocation = PracticeLocationDetail;
        }
        $('#practiceLocationWarningModal').modal();
    };

    $scope.removePracticeLocation = function (PracticeLocationDetails) {
        var validationStatus = false;
        var url = null;
        var $formData = null;
        $formData = $('#removePracticeLocation');
        url = rootDir + "/Profile/PracticeLocation/RemovePracticeLocationAsync?profileId=" + profileId;
        ResetFormForValidation($formData);
        //console.log($formData);
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
                        var obj = $filter('filter')(PracticeLocationDetails, { PracticeLocationDetailID: data.practiceLocation.PracticeLocationDetailID })[0];
                        PracticeLocationDetails.splice(PracticeLocationDetails.indexOf(obj), 1);
                        if ($scope.PracticeLocationDetailsHistory.length != 0) {
                            obj.HistoryStatus = 'Deleted';
                            $scope.PracticeLocationDetailsHistory.push(obj);
                        }
                        $('#practiceLocationWarningModal').modal('hide');
                        $scope.operateViewControlPracLoc('');
                        messageAlertEngine.callAlertMessage("addedNewFacility", "Practice Location Detail Removed successfully.", "success", true);
                    } else {
                        $('#practiceLocationWarningModal').modal('hide');
                        messageAlertEngine.callAlertMessage("removePracticeLocation", data.status, "danger", true);
                        $scope.errorPracticeLocation = "Sorry for Inconvenience !!!! Please Try Again Later...";
                    }
                },
                error: function (e) {

                }
            });
        }
    };

    $rootScope.PracticeLocationLoaded = true;
    $scope.dataLoaded = false;
    $rootScope.$on('PracticeLocation', function () {
        if (!$scope.dataLoaded) {
            $rootScope.PracticeLocationLoaded = false;
        //console.log("Getting data....");
        $http({
            method: 'GET',
            url: rootDir + '/Profile/MasterProfile/GetPracticeLocationsProfileDataAsync?profileId=' + profileId
        }).success(function (data, status, headers, config) {
            //console.log(data);
            try {
                for (key in data) {
                    //console.log(key);
                    $rootScope.$emit(key, data[key]);
                    //call respective controller to load data (PSP)
                }

                $rootScope.PracticeLocationLoaded = true;
                    $rootScope.$broadcast("LoadRequireMasterDataPracticeLocation");
            } catch (e) {
                //console.log("error getting data back");
                $rootScope.PracticeLocationLoaded = true;
            }

        }).error(function (data, status, headers, config) {
            //console.log(status);
            $rootScope.PracticeLocationLoaded = true;
        });
            $scope.dataLoaded = true;
        }
    });

}]);

