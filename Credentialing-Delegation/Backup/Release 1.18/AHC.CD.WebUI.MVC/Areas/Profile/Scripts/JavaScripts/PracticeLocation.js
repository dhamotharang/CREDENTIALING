
profileApp.controller('practiceLocationController', ['$scope', '$rootScope', '$http', '$filter', 'masterDataService', 'locationService', 'messageAlertEngine', function ($scope, $rootScope, $http, $filter, masterDataService, locationService, messageAlertEngine) {

    $scope.GeneralInformationEdit = false;
    // Toggle the view of passed DOM Element by ID
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

    // Calling master data service to get all Accessibility Questions
    masterDataService.getMasterData("/Profile/MasterData/GetAllAccessibilityQuestions").then(function (GetAllAccessibilityQuestions) {
        $scope.masterAccessibilityQuestions = GetAllAccessibilityQuestions;
        
    });

    // Calling master data service to get all Service Questions
    masterDataService.getMasterData("/Profile/MasterData/GetAllServiceQuestions").then(function (GetAllServiceQuestions) {
        $scope.masterServiceQuestions = GetAllServiceQuestions;
    });

    // Calling master data service to get all Practice Types
    masterDataService.getMasterData("/Profile/MasterData/GetAllPracticeTypes").then(function (GetAllPracticeTypes) {
        $scope.masterPracticeTypes = GetAllPracticeTypes;
    });

    masterDataService.getMasterData("/Profile/MasterData/GetAllOrganizations").then(function (GetAllOrganizations) {
        $scope.organizations = GetAllOrganizations;
    });

    masterDataService.getMasterData("/Profile/MasterData/GetAllFacilities").then(function (GetAllFacilities) {
       // console.log(GetAllFacilities);
        $scope.facilities = GetAllFacilities;
    });

    // Calling master data service to get all Open Practice Status Questions
    masterDataService.getMasterData("/Profile/MasterData/GetAllOpenPracticeStatusQuestions").then(function (GetAllOpenPracticeStatusQuestions) {
        $scope.masterOpenPracticeStatusQuestions = GetAllOpenPracticeStatusQuestions;
    });

    // Calling master data service to get all organization practicing group info
    masterDataService.getMasterData("/MasterData/Organization/GetGroups").then(function (GetAllPracticingGroup) {
        $scope.groups = GetAllPracticingGroup;
    });

    //Calling master data service to get all Office Manager from master data
    masterDataService.getMasterData("/Profile/MasterData/GetAllBusinessContactPerson").then(function (GetAllOfficeManager) {
        // console.log(GetAllOfficeManager);
        $scope.managers = GetAllOfficeManager;
    });

    //Calling master data service to get all Billing Contact from master data
    //masterDataService.getMasterData("/Profile/MasterData/GetAllBillingContact").then(function (GetAllBillingContact) {
    //    $scope.billings = GetAllBillingContact;
    //});

    // Calling master data service to get all Payment And Remittance from master data
    masterDataService.getMasterData("/Profile/MasterData/GetAllPaymentAndRemittance").then(function (GetAllPaymentAndRemittance) {
        console.log("============");
        console.log(GetAllPaymentAndRemittance);
        console.log("============");
        $scope.payments = GetAllPaymentAndRemittance;
    });

    masterDataService.getMasterData("/Profile/MasterData/GetAllCredentialingContact").then(function (GetAllCredentialingContact) {
        //console.log(GetAllBillingContact);
        $scope.credentialingContact = GetAllCredentialingContact;
        console.log(GetAllCredentialingContact);
    });

    $rootScope.getPractitionerData = function () {
        console.log(profileId);
        masterDataService.getPractitioners("/Profile/MasterData/GetPractitionersByProviderLevel", "Mid-Level", profileId).then(function (MidLevelPractitioners) {
            //console.log(MidLevelPractitioners);
           // console.log("Midlevel Called");
            $scope.midLevelPractitioners = MidLevelPractitioners;
        });

        masterDataService.getPractitioners("/Profile/MasterData/GetPractitionersByProviderLevel", "Supervisor", profileId).then(function (SupervisingPractitioners) {
            //console.log("Supervisor called");
            //console.log(SupervisingPractitioners);
            $scope.supervisingProviders = SupervisingPractitioners;
        });

        masterDataService.getProviderLevels("/Profile/MasterData/GetAllProviderLevelByProfileId", profileId).then(function (ProviderLevel) {
            console.log("Provider Level  ==>>  ");
            console.log(ProviderLevel);
            //console.log(ProviderLevels.indexOf("Mid-Level"));
          //  console.log("practitioner Level called");
            $scope.providerLevel = ProviderLevel;
        });
    };
    
    $scope.IsRenew = false;

    masterDataService.getMasterData("/Profile/MasterData/GetAllSpecialities").then(function (masterSpecialties) {
        console.log(masterSpecialties);
        $scope.masterSpecialties = masterSpecialties;
    });

    masterDataService.getMasterData("/Profile/MasterData/GetAllProviderTypes").then(function (masterProvidertypes) {
        console.log(masterProvidertypes);
        $scope.masterProviderTypes = masterProvidertypes;

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


    // ticetice Location Details Variable Initialization

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
        $scope.tempObject.PracticeOfficeHours = {};
    }

    $scope.editGeneralInfo = function (PracticeLocationDetail) {
        $scope.GeneralInformationEdit = true;
        $rootScope.operateSecondEditControl(null, PracticeLocationDetail);
    }

    $scope.editGeneralCancel = function (PracticeLocationDetail) {
        $scope.GeneralInformationEdit = false;
    }

    // rootScoped on emitted value catches the value for the model and insert to get the old data
    //calling the method using $on(PSP-public subscriber pattern)
    $rootScope.$on('PracticeLocationDetails', function (event, val) {
        console.log(val);
        $scope.PracticeLocationDetails = val;
        if (val) {
            for (var i = 0; i < $scope.PracticeLocationDetails.length ; i++) {
                $scope.PracticeLocationDetails[i].MidlevelPractioners = [];
                $scope.PracticeLocationDetails[i].SupervisingProviders = [];
                $scope.PracticeLocationDetails[i].PracticeColleagues = [];
                /* Parsing the date format in client side */
                if ($scope.PracticeLocationDetails[i].StartDate)
                $scope.PracticeLocationDetails[i].StartDate = ConvertDateFormat($scope.PracticeLocationDetails[i].StartDate);

                if ($scope.PracticeLocationDetails[i].WorkersCompensationInformation) {
                    $scope.PracticeLocationDetails[i].WorkersCompensationInformation.IssueDate = ConvertDateFormat($scope.PracticeLocationDetails[i].WorkersCompensationInformation.IssueDate);
                    $scope.PracticeLocationDetails[i].WorkersCompensationInformation.ExpirationDate = ConvertDateFormat($scope.PracticeLocationDetails[i].WorkersCompensationInformation.ExpirationDate);
                }

                if (!$scope.PracticeLocationDetails[i].OfficeHour) {
                    $scope.PracticeLocationDetails[i].OfficeHour = $scope.PracticeLocationDetails[i].Facility.FacilityDetail.PracticeOfficeHour;
                }

                for (var j = 0; j < $scope.PracticeLocationDetails[i].PracticeProviders.length; j++) {
                    if ($scope.PracticeLocationDetails[i].PracticeProviders[j].Practice == 'Supervisor' && $scope.PracticeLocationDetails[i].PracticeProviders[j].Status == 'Active') {
                        $scope.PracticeLocationDetails[i].SupervisingProviders.push($scope.PracticeLocationDetails[i].PracticeProviders[j]);
                    }
                    if ($scope.PracticeLocationDetails[i].PracticeProviders[j].Practice == 'Midlevel' && $scope.PracticeLocationDetails[i].PracticeProviders[j].Status == 'Active') {
                        $scope.PracticeLocationDetails[i].MidlevelPractioners.push($scope.PracticeLocationDetails[i].PracticeProviders[j]);
                    }
                    if ($scope.PracticeLocationDetails[i].PracticeProviders[j].Practice == 'CoveringColleague' && $scope.PracticeLocationDetails[i].PracticeProviders[j].Status == 'Active') {
                        $scope.PracticeLocationDetails[i].PracticeColleagues.push($scope.PracticeLocationDetails[i].PracticeProviders[j]);
                    }
                }
                
                console.log("New Bhagwan");
                console.log($scope.PracticeLocationDetails);
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
    $scope.clear = function (data) {
        $scope.tempSecondObject.ElectronicBillingCapabilityYesNoOption = "";
        $scope.tempSecondObject.BillingDepartment = "";
        $scope.tempSecondObject.CheckPayableTo = "";
        $scope.tempSecondObject.Office = "";
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
    }

    //To clear data on No click from payment and remittance
    //$scope.clearPayment = function (data) {
    //    $scope.tempSecondObject.ElectronicBillingCapabilityYesNoOption = "";
    //    $scope.tempSecondObject.BillingDepartment = "";
    //    $scope.tempSecondObject.CheckPayableTo = "";
    //    $scope.tempSecondObject.Office = "";
    //    $scope.tempSecondObject.PaymentAndRemittancePerson.FirstName = "";
    //    $scope.tempSecondObject.PaymentAndRemittancePerson.MiddleName = "";
    //    $scope.tempSecondObject.PaymentAndRemittancePerson.LastName = "";
    //    $scope.tempSecondObject.PaymentAndRemittancePerson.Telephone = "";
    //    $scope.tempSecondObject.PaymentAndRemittancePerson.EmailAddress = "";
    //    $scope.tempSecondObject.PaymentAndRemittancePerson.Fax = "";
    //    $scope.tempSecondObject.PaymentAndRemittancePerson.CountryCodeFax = "";
    //    $scope.tempSecondObject.PaymentAndRemittancePerson.CountryCodeTelephone = "";
    //    $scope.tempSecondObject.PaymentAndRemittancePerson.County = "";
    //    $scope.tempSecondObject.PaymentAndRemittancePerson.POBoxAddress = "";
    //    $scope.tempSecondObject.PaymentAndRemittancePerson.State = "";
    //    $scope.tempSecondObject.PaymentAndRemittancePerson.Street = "";
    //    $scope.tempSecondObject.PaymentAndRemittancePerson.City = "";
    //    $scope.tempSecondObject.PaymentAndRemittancePerson.Building = "";
    //    $scope.tempSecondObject.PaymentAndRemittancePerson.ZipCode = "";
    //}

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
        $scope.tempSecondObject.Country = PracticeLocationDetail.Facility.Country;
        $scope.tempSecondObject.County = PracticeLocationDetail.Facility.County;
        $scope.tempSecondObject.State = PracticeLocationDetail.Facility.State;
        $scope.tempSecondObject.Street = PracticeLocationDetail.Facility.Street;
        $scope.tempSecondObject.City = PracticeLocationDetail.Facility.City;
        $scope.tempSecondObject.Building = PracticeLocationDetail.Facility.Building;
        $scope.tempSecondObject.ZipCode = PracticeLocationDetail.Facility.ZipCode;
    }

    //For filling data for selected payment and remittance from drop down
    $scope.paymentData = function (empId, array) {
        var data = $filter('filter')(array, { PracticePaymentAndRemittanceID: empId })[0]
      //  console.log(data);
        $scope.tempSecondObject.ElectronicBillingCapabilityYesNoOption = data.ElectronicBillingCapabilityYesNoOption;
        $scope.tempSecondObject.BillingDepartment = data.BillingDepartment;
        $scope.tempSecondObject.CheckPayableTo = data.CheckPayableTo;
        $scope.tempSecondObject.Office = data.Office;
        $scope.tempSecondObject.FirstName = data.PaymentAndRemittancePerson.FirstName;
        $scope.tempSecondObject.MiddleName = data.PaymentAndRemittancePerson.MiddleName;
        $scope.tempSecondObject.LastName = data.PaymentAndRemittancePerson.LastName;
        $scope.tempSecondObject.Telephone = data.PaymentAndRemittancePerson.Telephone;
        $scope.tempSecondObject.EmailAddress = data.PaymentAndRemittancePerson.EmailAddress;
        $scope.tempSecondObject.Fax = data.PaymentAndRemittancePerson.Fax;
        $scope.tempSecondObject.CountryCodeFax = data.PaymentAndRemittancePerson.CountryCodeFax;
        $scope.tempSecondObject.CountryCodeTelephone = data.PaymentAndRemittancePerson.CountryCodeTelephone;
        $scope.tempSecondObject.County = data.PaymentAndRemittancePerson.County;
        $scope.tempSecondObject.State = data.PaymentAndRemittancePerson.State;
        $scope.tempSecondObject.Street = data.PaymentAndRemittancePerson.Street;
        $scope.tempSecondObject.City = data.PaymentAndRemittancePerson.City;
        $scope.tempSecondObject.Building = data.PaymentAndRemittancePerson.Building;
        $scope.tempSecondObject.Country = data.PaymentAndRemittancePerson.Country;
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
            url = "/Profile/PracticeLocation/AddOfficeManagerAsync?profileId=" + profileId;
       
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
                        $scope.PracticeLocationDetails.push(data.practiceLocationDetail);
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
            url = "/Profile/PracticeLocation/AddBillingContactAsync?profileId=" + profileId;
  
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
                        $scope.PracticeLocationDetails.push(data.practiceLocationDetail);
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
            url = "/Profile/PracticeLocation/AddPaymentAndRemittanceAsync?profileId=" + profileId;
       
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

        $formData = $('#newFacilityDataForm');
        url = "/Profile/PracticeLocation/AddFacilityAsync?profileId=" + profileId;
        if (typeOfSave == 'Update') {
            url = "/Profile/PracticeLocation/UpdateFacilityAsync?profileId=" + profileId;
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
                            $scope.operateCancelControl('');
                            messageAlertEngine.callAlertMessage("updatedFacility", "Facility Information updated successfully !!!!", "success", true);
                        }
                        else {
                        $scope.facilities.push(data.facility);
                        $scope.operateAddControlPracLoc('addPracticeLocation'); 
                            messageAlertEngine.callAlertMessage("addedNewFacility", "New Facility Information saved successfully !!!!", "success", true);
                            console.log(data.facility);
                            $scope.tempSecondObject.FacilityId = data.facility.FacilityID;
                            $scope.tempSecondObject.PracticeLocationCorporateName = data.facility.Name;
                        }
                        //$scope.resetDates();
                        $scope.PracticeDays = $scope.OriginalPracticeDays;
                        FormReset($formData);
                    } else {
                        messageAlertEngine.callAlertMessage("addedNewFacility", data.status, "danger", true);
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


        url = "/Profile/PracticeLocation/savePracticeLocationDetailInformaton?profileId=" + profileId;


        //ResetFormForValidation($formData);

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
                    //console.log(data);
                    if (data.status == "true") {
                        if ($scope.PracticeLocationDetails == null) {

                            $scope.PracticeLocationDetails = new Array();
                        }
                        data.practiceLocationDetail.StartDate = ConvertDateFormat(data.practiceLocationDetail.StartDate);
                        data.practiceLocationDetail.Facility = $scope.getFacilityById(data.practiceLocationDetail.FacilityId);
                        var groupName = $($formData[0]).find($("[name='PracticingGroupId'] option:selected")).text();
                        data.practiceLocationDetail.Group = {};
                        data.practiceLocationDetail.Group.Group = {};
                        data.practiceLocationDetail.Group.Group.Name = groupName;
                        if (!data.practiceLocationDetail.OfficeHour) {
                            data.practiceLocationDetail.OfficeHour = data.practiceLocationDetail.Facility.FacilityDetail.PracticeOfficeHour;
                        }
                        $scope.PracticeLocationDetails.push(data.practiceLocationDetail);
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
        { DayName: "Sunday", DayOfWeek: 0, DailyHours: [{ StartTime: "08:00", EndTime: "17:00" }] },
        { DayName: "Monday", DayOfWeek: 1, DailyHours: [{ StartTime: "08:00", EndTime: "17:00" }] },
        { DayName: "Tuesday", DayOfWeek: 2, DailyHours: [{ StartTime: "08:00", EndTime: "17:00" }] },
        { DayName: "Wednesday", DayOfWeek: 3, DailyHours: [{ StartTime: "08:00", EndTime: "17:00" }] },
        { DayName: "Thursday", DayOfWeek: 4, DailyHours: [{ StartTime: "08:00", EndTime: "17:00" }] },
        { DayName: "Friday", DayOfWeek: 5, DailyHours: [{ StartTime: "08:00", EndTime: "17:00" }] },
        { DayName: "Saturday", DayOfWeek: 6, DailyHours: [{ StartTime: "08:00", EndTime: "17:00" }] },
    ];

    $scope.PracticeDays = $scope.OriginalPracticeDays;

    $scope.addDailyHour = function (DailyHours) {
        DailyHours.push({ StartTime: "08:00", EndTime: "17:00" });
    };

    $scope.removeDailyHour = function (DailyHours, index) {
        DailyHours.splice(index, 1);
    };


    $scope.validateDailyHours = function (PracticeDays, parent, subsection, index, typeOfSave) {
        var status = true;
        for (practiceDay in PracticeDays) {
            var prevStartHour = "";
            var prevStartMin = "";
            var prevEndHour = "";
            var prevEndMin = ""
            for (dailyHour in PracticeDays[practiceDay].DailyHours) {

                var startTime = $('#startTime_' + practiceDay + dailyHour).val(); //PracticeDays[practiceDay].DailyHours[dailyHour].StartTime;
                var endTime = $('#endTime_' + practiceDay + dailyHour).val(); //PracticeDays[practiceDay].DailyHours[dailyHour].EndTime;

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

        if (status && subsection == 'facility') {
            $scope.saveFacilityInformaton(typeOfSave, index);
        }
        else if (status && subsection == 'ProviderOfficeHour') {
            $scope.updateOfficeHours(parent, index);
        }

    };


    $scope.updateOfficeHours = function (PracticeLocationDetail, index) {

        var validationStatus = false;
        var url = null;
        var $formData = null;

        $formData = $('#OfficeHourForm' + index);

        url = "/Profile/PracticeLocation/updateOfficeHours?profileId=" + profileId;

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
       
        $("#providerofficehoursdiv").find(":input").each(function () {
            $(this).clockface('hide');
        })
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
    };


    $scope.DeselectLanguage = function (language) {
        $rootScope.tempObject.FacilityDetail.Language.NonEnglishLanguages.splice($rootScope.tempObject.FacilityDetail.Language.NonEnglishLanguages.indexOf(language), 1);

        for (var i in Languages) {
            if (Languages[i].name == language.Language) {
                $scope.tempLanguages.push(Languages[i]);
            }
        }

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
            url = "/Profile/PracticeLocation/UpdateWorkersCompensationInformationAsync?profileId=" + profileId;
        }
        else {
            url = "/Profile/PracticeLocation/RenewWorkersCompensationInformationAsync?profileId=" + profileId;
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
        url = "/Profile/PracticeLocation/UpdateOpenPracticeStatusAsync?profileId=" + profileId;
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

    // Calling master data service to get all organization practicing group info
    masterDataService.getMasterData("/MasterData/Organization/GetGroups").then(function (GetAllPracticingGroup) {
        $scope.groups = GetAllPracticingGroup;
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

  
    $scope.updatePracticeLocationDetailInformaton = function (index) {

        var validationStatus = false;
        var url = null;
        var $formData = null;

        $formData = $('#updatePracticeLocationDetails');


        url = "/Profile/PracticeLocation/updatePracticeLocationDetailInformaton?profileId=" + profileId;


        //ResetFormForValidation($formData);

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
                        $scope.PracticeLocationDetails[index].PrimaryYesNoOption = data.practiceLocationDetail.PrimaryYesNoOption;
                        $scope.PracticeLocationDetails[index].PracticeExclusively = data.practiceLocationDetail.PracticeExclusively;
                        $scope.PracticeLocationDetails[index].PracticeExclusivelyYesNoOption = data.practiceLocationDetail.PracticeExclusivelyYesNoOption;
                        $scope.PracticeLocationDetails[index].CurrentlyPracticingAtThisAddress = data.practiceLocationDetail.CurrentlyPracticingAtThisAddress;
                        $scope.PracticeLocationDetails[index].CurrentlyPracticingYesNoOption = data.practiceLocationDetail.CurrentlyPracticingYesNoOption;
                        $scope.PracticeLocationDetails[index].SendGeneralCorrespondence = data.practiceLocationDetail.SendGeneralCorrespondence;
                        $scope.PracticeLocationDetails[index].GeneralCorrespondenceYesNoOption = data.practiceLocationDetail.GeneralCorrespondenceYesNoOption;
                        $scope.PracticeLocationDetails[index].PrimaryTaxId = data.practiceLocationDetail.PrimaryTaxId;
                        $scope.PracticeLocationDetails[index].PrimaryTax = data.practiceLocationDetail.PrimaryTax;
                        
                        // updating the visibility control

                        $scope.GeneralInformationEdit = false;


                        messageAlertEngine.callAlertMessage("UpdatePracticeLocation", "Practice Location Information updated successfully !!!!", "success", true);
                    } else {
                        messageAlertEngine.callAlertMessage("UpdatePracticeLocation", data.status, "danger", true);
                    }
                },
                error: function (e) {
                        messageAlertEngine.callAlertMessage("UpdatePracticeLocation", "Oops !!! Something went wrong , please try again", "danger", true);
                }
            });


        }
    };

   
    // Calling master data service to get all provider types

    masterDataService.getMasterData("/Profile/MasterData/GetAllProviderTypes").then(function (Providertypes) {
        $scope.ProviderTypes = Providertypes;
    });
   

    // Save Mid level Details
    $scope.saveMidLevel = function (PracticeLocationDetailID) {

        var validationStatus = null;
        var url = null;
        var $formData = null;
        $formData = $('#addnewMidLevel');
        
        validationStatus = $formData.valid();
        url = "/Profile/PracticeLocation/addMidLevelAsync?practiceLocationDetailID=" + PracticeLocationDetailID + "&profileId=" + profileId;
        
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



    //======================================Primary Credentialing Contact=================



    $scope.radioOption1 = function(index){
    
        $scope.credDropDown = false;
        if ($scope.PracticeLocationDetails[index].BusinessOfficeManagerOrStaff != null) {

            $scope.tempObject.FirstName = $scope.PracticeLocationDetails[index].BillingContactPerson.FirstName;
            $scope.tempObject.MiddleName = $scope.PracticeLocationDetails[index].BillingContactPerson.MiddleName;
            $scope.tempObject.LastName = $scope.PracticeLocationDetails[index].BillingContactPerson.LastName;
            $scope.tempObject.Telephone = $scope.PracticeLocationDetails[index].BillingContactPerson.Telephone;
            $scope.tempObject.Fax = $scope.PracticeLocationDetails[index].BillingContactPerson.Fax;
            $scope.tempObject.EmailAddress = $scope.PracticeLocationDetails[index].BillingContactPerson.EmailAddress;
            $scope.tempObject.Building = $scope.PracticeLocationDetails[index].Facility.Building;
            $scope.tempObject.City = $scope.PracticeLocationDetails[index].Facility.City;
            $scope.tempObject.Country = $scope.PracticeLocationDetails[index].Facility.Country;
            $scope.tempObject.Street = $scope.PracticeLocationDetails[index].Facility.Street;
            $scope.tempObject.ZipCode = $scope.PracticeLocationDetails[index].Facility.ZipCode;
            $scope.tempObject.State = $scope.PracticeLocationDetails[index].Facility.State;
            $scope.tempObject.County = $scope.PracticeLocationDetails[index].Facility.County;
          
        }
                 
    }

    $scope.radioOption3 = function () {

        $scope.credDropDown = false;
        $scope.tempObject = {};
    }

    $scope.radioOption2 = function () {

     
        $scope.tempObject = {};
        $scope.credDropDown = true;
    }

    $scope.getDropDownValue = function(empObj){
        $scope.tempObject = {};
        empObj=$filter('filter')($scope.credentialingContact, { EmployeeID: empObj })[0];
        $scope.tempObject = empObj;
    
    }

    $scope.saveCredentialingContact = function (index) {

        var validationStatus = false;
        var url = null;
        var formData = null;


        if ($scope.visibilityControl == 'addCc') {
            //Add Details - Denote the URL
            formData = $('#newShowCredentialingContactDiv').find('form');
            url = "/Profile/PracticeLocation/AddCredentialingContact";
        }
        else if ($scope.visibilityControl == ('editCc')) {
            //Update Details - Denote the URL
            formData = $('#CredentialingContactEditDiv').find('form');
            url = "/Profile/PracticeLocation/UpdateCredentialingContact?profileId=" + profileId;
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

    $scope.setMidLevelPractitioner = function (practitioner) {
        if (angular.isObject(practitioner)) {
            $scope.tempThirdObject.FirstName = practitioner.PersonalDetail.FirstName;
            $scope.tempThirdObject.MiddleName = practitioner.PersonalDetail.MiddleName;
            $scope.tempThirdObject.LastName = practitioner.PersonalDetail.LastName;
            $scope.tempThirdObject.NPINumber = practitioner.OtherIdentificationNumber.NPINumber;
            $(".ProviderTypeSelectAutoList").hide();
        }
    };

    $scope.resetMidLevelPractitioner = function () { 
        $scope.MidLevelPractitioner = {};   
    };

    $scope.initWarning = function (practitioner) {
        if (angular.isObject(practitioner)) {
            $scope.tempMidLevelpractitioner = practitioner;
        }
        $('#warningModal').modal();
    };

    $scope.addMidlevelPractioners = function (PracticeLocationDetail, index) {
        var validationStatus = false;
        var url = null;
        var $formData = null;

        $formData = $('#MidLevelPractitionerForm' + index);

        url = "/Profile/PracticeLocation/AddPracticeProviderAsync";
        if ($scope.tempThirdObject.PracticeProviderID) {
            url = "/Profile/PracticeLocation/UpdatePracticeProviderAsync";
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

        url = "/Profile/PracticeLocation/RemovePracticeProviderAsync";

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

        url = "/Profile/PracticeLocation/AddPracticeProviderAsync";
        if ($scope.tempThirdObject.PracticeProviderID) {
            url = "/Profile/PracticeLocation/UpdatePracticeProviderAsync";
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

        url = "/Profile/PracticeLocation/RemovePracticeProviderAsync";

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
        $scope.tempSecondObject.City = $scope.tempObject.City;
        $scope.tempSecondObject.State = $scope.tempObject.State;
        $scope.tempSecondObject.Country = $scope.tempObject.Country;
    }


    //-----------------------------------------------------------------------------------------------------------------------------//
    //------------------------------------------------------ Covering Colleagues ----------------------------------------------//
    //-----------------------------------------------------------------------------------------------------------------------------//

    
    //$scope.tempSecondObject = {};

    //To Display the drop down div
    $scope.searchCumDropDown = function (event) {
        $(event.target).parent().find(".ProviderTypeSelectAutoList").first().show();
    };

    //Bind the supervisor with model class to achieve search cum drop down
    $scope.addIntoSupervisorDropDown = function (supervisor, $event) {
        $scope.tempSecondObject.NPINumber = supervisor.OtherIdentificationNumber.NPINumber;
        $scope.tempSecondObject.FirstName = supervisor.PersonalDetail.FirstName;
        $scope.tempSecondObject.MiddleName = supervisor.PersonalDetail.MiddleName;
        $scope.tempSecondObject.LastName = supervisor.PersonalDetail.LastName;

        for (var i = 0; i < supervisor.PracticeLocationDetails.length; i++)
        {
            if(supervisor.PracticeLocationDetails[i].PrimaryYesNoOption == 1){                
                $scope.tempSecondObject.street = supervisor.PracticeLocationDetails[i].street;
                $scope.tempSecondObject.Building = supervisor.PracticeLocationDetails[i].Building;
                $scope.tempSecondObject.City = supervisor.PracticeLocationDetails[i].City;
                $scope.tempSecondObject.State = supervisor.PracticeLocationDetails[i].State;
                $scope.tempSecondObject.ZipCode = supervisor.PracticeLocationDetails[i].ZipCode;
                $scope.tempSecondObject.Country = supervisor.PracticeLocationDetails[i].Country;
                $scope.tempSecondObject.County = supervisor.PracticeLocationDetails[i].County;
                $scope.tempSecondObject.CountryCodeTelephone = PracticeLocationDetails[i].PersonalDetail.CountryCodeTelephone;
                $scope.tempSecondObject.Telephone = supervisor.PracticeLocationDetails[i].Telephone;
            }
        }
        
        $($event.target).parent().find(".ProviderTypeSelectAutoList").first().hide();
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
        
        $scope.PracticeProviderTypes.push(providertype);
        //$scope.tempProviderTypes.splice($scope.tempProviderTypes.indexOf(providertype), 1);
    };

    //------------------------------------- UN-select Provider type -----------------------------------------
    $scope.ActionProviderType = function (providertype) {
        
        $scope.PracticeProviderTypes.splice($scope.PracticeProviderTypes.indexOf(providertype), 1);
        
        
    };

    $scope.PracticeSpecialties = [];

    $scope.SelectSpecalties = function (specialty) {

        $scope.PracticeSpecialties.push(specialty);
        //$scope.tempProviderTypes.splice($scope.tempProviderTypes.indexOf(providertype), 1);
    };

    //------------------------------------- UN-select Provider type -----------------------------------------
    $scope.ActionSpecialty = function (specialty) {

        $scope.PracticeSpecialties.splice($scope.PracticeSpecialties.indexOf(specialty), 1);


    };

    // Save Covering Colleagues Details
    $scope.savePatner = function (PracticeLocationDetail, index) {        
       
        var validationStatus = true;
        var url = null;
        var $formData = null;
       
        $formData = $('#CoveringCollegueForm'+index);
        
        url = "/Profile/PracticeLocation/AddPracticeProviderAsync";

        if ($scope.tempSecondObject.PracticeProviderID) {
            url = "/Profile/PracticeLocation/UpdatePracticeProviderAsync";
        }

        ResetFormForValidation($formData);

        //console.log($formData);
        validationStatus = $formData.valid();       
        
        if(validationStatus){       
        
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
                        PracticeLocationDetail.PracticeColleagues[index] = data.practiceProvider;
                        $rootScope.operateSecondViewAndAddControl(index + '_viewPartner');
                        messageAlertEngine.callAlertMessage("updatedPartnerDetails"+ index, "Covering Colleagues information Updated successfully.", "success", true);
                    } else {
                        if (PracticeLocationDetail.PracticeColleagues == null)
                            PracticeLocationDetail.PracticeColleagues = [];

                        //console.log(data.midlevelPractioner);
                        var obj = $filter('filter')(PracticeLocationDetail.PracticeColleagues, { PracticeProviderID: data.practiceProvider.PracticeProviderID })[0];
                        //data.practiceProvider.PracticeProviderSpecialties.Specialty = $scope.PracticeSpecialties;
                        //data.practiceProvider.PracticeProviderTypes.ProviderType = $scope.PracticeProviderTypes;
                        PracticeLocationDetail.PracticeColleagues.push(data.practiceProvider);                        
                        $rootScope.operateSecondCancelControl('');                        
                        $scope.tempSecondObject = {};
                        $scope.PracticeSpecialties = [];
                        $scope.PracticeProviderTypes = [];
                        messageAlertEngine.callAlertMessage("addedPartnerDetails", "Covering Colleagues Information Added successfully.", "success", true);
                    }
                } else {
                    messageAlertEngine.callAlertMessage("ErrorInPartners", data.status, "danger", true);
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

        url = "/Profile/PracticeLocation/RemovePracticeProviderAsync";

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
  
  
}]);

