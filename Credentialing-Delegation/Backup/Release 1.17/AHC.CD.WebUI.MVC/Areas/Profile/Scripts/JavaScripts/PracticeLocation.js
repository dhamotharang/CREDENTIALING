profileApp.directive('samayTimePicker', function () {
    return function (scope, elem) {
        elem.clockface({
            format: 'HH:mm'
        });
    };
});

profileApp.directive('samayToggel', ['$compile', function ($compile) {
    return {
        restrict: 'AE',
        link: function (scope, element, attr) {
            element.bind('click', function (e) {
                e.stopPropagation();
                element.parent().parent().find("input").clockface('toggle');
            });
        }
    };
}]);

//profileApp.directive('trialdirective', function () {
//    return function (scope, elem) {
//        var timeInput = elem.getElementsByClassName("trialclass")[0];
//        var timeButton = elem.getElementsByClassName("trialclass")[1];

//        timeInput.clockface({
//            format: 'hh:mm A',
//            trigger: 'manual'
//        });

//        timeButton.click(function (e) {
//            e.stopPropagation();
//            timeInput.clockface('toggle');
//        });
//    };
//});


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

    // 
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

    //masterDataService.getMasterData("/Profile/MasterData/GetInitialPracticeDays").then(function (GetInitialPracticeDays) {
    //    //console.log(angular.fromJson(GetInitialPracticeDays));
    //    $scope.PracticeDays = angular.fromJson(GetInitialPracticeDays);
    //});

    //Calling master data service to get all Office Manager from master data
    masterDataService.getMasterData("/Profile/MasterData/GetAllBusinessContactPerson").then(function (GetAllOfficeManager) {
        //console.log(GetAllOfficeManager);
        $scope.managers = GetAllOfficeManager;
    });

    //Calling master data service to get all Billing Contact from master data
    masterDataService.getMasterData("/Profile/MasterData/GetAllBillingContact").then(function (GetAllBillingContact) {
        //console.log(GetAllBillingContact);
        $scope.billings = GetAllBillingContact;
    });

    $rootScope.getPractitionerData = function () {
        console.log(profileId);
        masterDataService.getPractitioners("/Profile/MasterData/GetPractitionersByProviderLevel", "Mid-Level", profileId).then(function (MidLevelPractitioners) {
            //console.log(MidLevelPractitioners);
           // console.log("Midlevel Called");
            $scope.midLevelPractitioners = MidLevelPractitioners;
        });

        masterDataService.getPractitioners("/Profile/MasterData/GetPractitionersByProviderLevel", "Supervisor", profileId).then(function (SupervisingPractitioners) {
            console.log("Supervisor called");
            console.log(SupervisingPractitioners);
            $scope.supervisingProviders = SupervisingPractitioners;
        });

        masterDataService.getProviderLevels("/Profile/MasterData/GetAllProviderLevelByProfileId", profileId).then(function (ProviderLevels) {
            //console.log("Provider Levels  ==>>  ");
            //console.log(ProviderLevels);
            //console.log(ProviderLevels.indexOf("Mid-Level"));
          //  console.log("practitioner Level called");
            $scope.providerLevels = ProviderLevels;
        });
    };
    
   // Calling master data service to get all Payment And Remittance from master data
    masterDataService.getMasterData("/Profile/PracticeLocation/GetAllPaymentAndRemittancePersons").then(function (GetAllPaymentAndRemittance) {
        $scope.payments = GetAllPaymentAndRemittance;
    });

    $scope.IsRenew = false;
    masterDataService.getMasterData("/Profile/MasterData/GetAllSpecialities").then(function (masterSpecialties) {
        $scope.masterSpecialties = masterSpecialties;
    });

    masterDataService.getMasterData("/Profile/MasterData/GetAllProviderTypes").then(function (masterProvidertypes) {
        
        $scope.masterProviderTypes = masterProvidertypes;
       
    });

    //Calling master data service to get all Payment And Remittance from master data
    //masterDataService.getMasterData("/Profile/PracticeLocation/GetAllPaymentAndRemittancePersons").then(function (GetAllPaymentAndRemittance) {
    //    //console.log(GetAllPaymentAndRemittance);
    //    $scope.payments = GetAllPaymentAndRemittance;
    //});


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

    // resetting the Date elements in practice location sub-sections

    //$scope.resetDates = function () {

    //    try {
    //        $scope.tempObject.StartDate = new Date();
    //        $scope.tempObject.EndDate = new Date();
    //        $scope.tempObject.DischargeDate = new Date();
    //    } catch (e) {
    //    }

    //}

    // rootScoped on emitted value catches the value for the model and insert to get the old data
    //calling the method using $on(PSP-public subscriber pattern)
    $rootScope.$on('PracticeLocationDetails', function (event, val) {
        console.log(val);
        $scope.PracticeLocationDetails = val;
        if (val) {
            for (var i = 0; i < $scope.PracticeLocationDetails.length ; i++) {
                /* Parsing the date format in client side */
                if ($scope.PracticeLocationDetails[i].StartDate)
                $scope.PracticeLocationDetails[i].StartDate = ConvertDateFormat($scope.PracticeLocationDetails[i].StartDate);

                if ($scope.PracticeLocationDetails[i].WorkersCompensationInformation) {
                    $scope.PracticeLocationDetails[i].WorkersCompensationInformation.IssueDate = ConvertDateFormat($scope.PracticeLocationDetails[i].WorkersCompensationInformation.IssueDate);
                    $scope.PracticeLocationDetails[i].WorkersCompensationInformation.ExpirationDate = ConvertDateFormat($scope.PracticeLocationDetails[i].WorkersCompensationInformation.ExpirationDate);
                }

                if (!$scope.PracticeLocationDetails[i].OfficeHour) {
                    $scope.PracticeLocationDetails[i].OfficeHour=$scope.PracticeLocationDetails[i].Facility.FacilityDetail.PracticeOfficeHour;
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
    $scope.changemade=function() {
        $('.field-validation-error').removeClass('field-validation-error').addClass('field-validation-valid');
        $('.input-validation-error').removeClass('input-validation-error').addClass('valid');
    };

    // to clear data on No click from Business Manager and Office Manger 
    $scope.clear = function(data)
    {   //to clear data
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

    // to clear data on No click from payment and remittance
    $scope.clearPayment = function (data) {
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
        $scope.tempSecondObject.PaymentAndRemittancePerson.POBoxAddress = "";
        $scope.tempSecondObject.PaymentAndRemittancePerson.State = "";
        $scope.tempSecondObject.PaymentAndRemittancePerson.Street = "";
        $scope.tempSecondObject.PaymentAndRemittancePerson.City = "";
        $scope.tempSecondObject.PaymentAndRemittancePerson.Building = "";
        $scope.tempSecondObject.PaymentAndRemittancePerson.ZipCode = "";
    }

    //For filling data for selected office Manager/Billing contact from drop down
    $scope.provideData = function (empId, array)
    {
        var data = $filter('filter')(array, { EmployeeID: empId })[0];
        $scope.tempSecondObject.FirstName = data.FirstName;
        $scope.tempSecondObject.MiddleName = data.MiddleName;
        $scope.tempSecondObject.LastName = data.LastName;
        $scope.tempSecondObject.Telephone = data.Telephone;
        $scope.tempSecondObject.EmailAddress = data.EmailAddress;
        $scope.tempSecondObject.Fax = data.Fax;
        $scope.tempSecondObject.CountryCodeFax = data.CountryCodeFax;
        $scope.tempSecondObject.CountryCodeTelephone = data.CountryCodeTelephone;
        $scope.tempSecondObject.County = data.County;
        $scope.tempSecondObject.POBoxAddress = data.POBoxAddress;
        $scope.tempSecondObject.State = data.State;
        $scope.tempSecondObject.Street = data.Street;
        $scope.tempSecondObject.City = data.City;
    }

    //For filling data for selected payment and remittance from drop down
    $scope.paymentData = function (empId, array) {
        var data = $filter('filter')(array, { EmployeeID: empId })[0]

        //$scope.tempSecondObject.ElectronicBillingCapabilityYesNoOption = data.ElectronicBillingCapabilityYesNoOption;
        //$scope.tempSecondObject.BillingDepartment = data.BillingDepartment;
        //$scope.tempSecondObject.CheckPayableTo = data.CheckPayableTo;
        //$scope.tempSecondObject.Office = data.Office;

        $scope.tempSecondObject.PaymentAndRemittancePerson.FirstName = data.FirstName;
        $scope.tempSecondObject.PaymentAndRemittancePerson.MiddleName = data.MiddleName;
        $scope.tempSecondObject.PaymentAndRemittancePerson.LastName = data.LastName;
        $scope.tempSecondObject.PaymentAndRemittancePerson.Telephone = data.Telephone;
        $scope.tempSecondObject.PaymentAndRemittancePerson.EmailAddress = data.EmailAddress;
        $scope.tempSecondObject.PaymentAndRemittancePerson.Fax = data.Fax;
        $scope.tempSecondObject.PaymentAndRemittancePerson.CountryCodeFax = data.CountryCodeFax;
        $scope.tempSecondObject.PaymentAndRemittancePerson.CountryCodeTelephone = data.CountryCodeTelephone;
        $scope.tempSecondObject.PaymentAndRemittancePerson.County = data.County;
        $scope.tempSecondObject.PaymentAndRemittancePerson.State = data.State;
        $scope.tempSecondObject.PaymentAndRemittancePerson.Street = data.Street;
        $scope.tempSecondObject.PaymentAndRemittancePerson.City = data.City;
    }


    /*************************************************************************************************/
    /*********************************** Office Manager **********************************************/
    /*************************************************************************************************/

    // Save office manager Details
    $scope.saveOfficeManager = function (PracticeLocationDetail, index) {
     
        var validationStatus = null;
        var url = null;
        var $formData = null;
        $formData = $('#BusinessOfficeContactPersonForm'+index);
        ResetFormForValidation($formData);
        if ($scope.manager=='Yes') {
            validationStatus = 'True';
            url = "/Profile/PracticeLocation/UpdateOfficeManagerAsync?profileId=" + profileId;
        }
        else {
            validationStatus = $formData.valid();
            url = "/Profile/PracticeLocation/AddOfficeManagerAsync?profileId=" + profileId;
        }
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
                        PracticeLocationDetail.BusinessOfficeManagerOrStaff = data.officemanager;
                        $scope.managers.push(data.officemanager);

                        $scope.PracticeLocationDetails.push(data.practiceLocationDetail);

                        $rootScope.operateSecondCancelControl('');
                        messageAlertEngine.callAlertMessage("businessManagerSuccessMsg", "Office manager/Business Office Staff Contact Information Updated successfully.", "success", true);
                    } else {
                        messageAlertEngine.callAlertMessage("alertOfficeManager", data.status, "danger", true);
                    }
                },
                error: function (e) {

                }
            });
            // $scope.changePartial = false;
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
        if ($scope.managerStatus) {
            validationStatus = 'True';
            url = "/Profile/PracticeLocation/UpdateBillingContactAsync?profileId=" + profileId;
        }
        else {
            validationStatus = $formData.valid();
            url = "/Profile/PracticeLocation/AddBillingContactAsync?profileId=" + profileId;
        }
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
                        PracticeLocationDetail.BillingContactPerson = data.billingcontact;
                        $scope.billings.push(data.billingcontact);
                        $rootScope.operateSecondCancelControl('');
                        messageAlertEngine.callAlertMessage("billingContactSuccessMsg", "Billing Contact Information Updated successfully.", "success", true);
                    } else {
                        messageAlertEngine.callAlertMessage("alertBillingContact", data.status, "danger", true);
                    }
                },
                error: function (e) {

                }
            });
            // $scope.changePartial = false;
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
        if ($scope.managerStatus) {
            validationStatus = 'True';
            url = "/Profile/PracticeLocation/UpdatePaymentAndRemittanceAsync?profileId=" + profileId;
        }
        else {
            validationStatus = $formData.valid();
            url = "/Profile/PracticeLocation/AddPaymentAndRemittanceAsync?profileId=" + profileId;
        }
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
                        //console.log(data.paymentandremittance)
                        PracticeLocationDetail.PaymentAndRemittance = data.paymentandremittance;
                       
                        $rootScope.operateSecondCancelControl('');
                        messageAlertEngine.callAlertMessage("paymentSuccessMsg", "Payment and Remittance Information Updated successfully.", "success", true);

                    } else {
                        messageAlertEngine.callAlertMessage("alertPayment", data.status, "danger", true);
                    }
                },
                error: function (e) {

                }
            });
            // $scope.changePartial = false;
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

    $scope.PracticeDays = [
        { DayName: "Sunday", DayOfWeek: 0, DailyHours: [{ StartTime: "08:00", EndTime: "17:00" }] },
        { DayName: "Monday", DayOfWeek: 1, DailyHours: [{ StartTime: "08:00", EndTime: "17:00" }] },
        { DayName: "Tuesday", DayOfWeek: 2, DailyHours: [{ StartTime: "08:00", EndTime: "17:00" }] },
        { DayName: "Wednesday", DayOfWeek: 3, DailyHours: [{ StartTime: "08:00", EndTime: "17:00" }] },
        { DayName: "Thursday", DayOfWeek: 4, DailyHours: [{ StartTime: "08:00", EndTime: "17:00" }] },
        { DayName: "Friday", DayOfWeek: 5, DailyHours: [{ StartTime: "08:00", EndTime: "17:00" }] },
        { DayName: "Saturday", DayOfWeek: 6, DailyHours: [{ StartTime: "08:00", EndTime: "17:00" }] },
    ];


    $scope.addDailyHour = function (DailyHours) {
        DailyHours.push({ StartTime: "08:00", EndTime: "17:00" });
    };

    $scope.removeDailyHour = function (DailyHours, index) {
        DailyHours.splice(index, 1);
    };


    $scope.validateDailyHours = function (PracticeDays, parent ,subsection, index, typeOfSave) {
        var status = true;
        for (practiceDay in PracticeDays) {
            var prevStartHour="";
            var prevStartMin = "";
            var prevEndHour = "";
            var prevEndMin=""
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

    $scope.hideClock = function(){
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
        $("#" +divToBeDisplayed).show();
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

    $scope.RenewDiv = function ()
    {
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

        $formData = $('#WorkerCompensationForm'+index);

        if($scope.IsRenew == false)
        {
            url = "/Profile/PracticeLocation/UpdateWorkersCompensationInformationAsync?profileId=" + profileId;
        }
        else
        {
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

                        if(data.status == "true") {
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

    $scope.getFacilityById=function(facilityId) {
        
            for (fac in $scope.facilities)
            {
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
        url = "/Profile/PracticeLocation/addMidLevelAsync?practiceLocationDetailID=" + PracticeLocationDetailID+"&profileId=" + profileId;
        
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


    //-----------------------------------------------------------------------------------------------------------------------------//
    //------------------------------------------------------ Mid Level Practitioners ----------------------------------------------//
    //-----------------------------------------------------------------------------------------------------------------------------//

    $scope.setMidLevelPractitioner = function (practitioner) {
        if (angular.isObject(practitioner)) {
            $scope.MidLevelPractitioner = practitioner;
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

        url = "/Profile/PracticeLocation/addMidlevelPractionersAsync?profileId=" + profileId;

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

                        //console.log(data.midlevelPractioner);
                        
                        PracticeLocationDetail.MidlevelPractioners.push({ 'MidLevelPractitionerID': data.midlevelPractioner.MidLevelPractitionerID, 'ProfileID': data.midlevelPractioner.ProfileID,'Profile': $filter('filter')($scope.midLevelPractitioners, { ProfileID: data.midlevelPractioner.ProfileID })[0] });
                        $rootScope.operateSecondCancelControl('');

                        $scope.resetMidLevelPractitioner();
                        
                        messageAlertEngine.callAlertMessage("midlevelPractionersSuccessMsg", "Mid-Level Practitioner Added successfully.", "success", true);
                    } else {
                        messageAlertEngine.callAlertMessage("midlevelPractionersErrorMsg", data.status, "danger", true);
                    }
                },
                error: function (e) {

                }
            });


        }
    };


    $scope.updateMidLevelPractitioner = function (PracticeLocationDetail) {
        //console.log("=================================");
        //console.log(PracticeLocationDetail);
        var validationStatus = false;
        var url = null;
        var $formData = null;

        $formData = $('#editMidlevelPractioner');

        url = "/Profile/PracticeLocation/UpdateMidLevelPractitionerAsync?profileId=" + profileId;

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
                        var obj = $filter('filter')(PracticeLocationDetail.MidlevelPractioners, { ProfileID: data.midlevelPractioner.MidLevelPractitionerID })[0];
                        PracticeLocationDetail.MidlevelPractioners.splice(PracticeLocationDetail.MidlevelPractioners.indexOf(obj), 1);
                        $('#warningModal').modal('hide');
                        PracticeLocationDetail.MidlevelPractioners.splice()
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
            $scope.SupervisingProvider = practitioner;
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

        url = "/Profile/PracticeLocation/AddSupervisingProviderAsync?profileId=" + profileId;

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

                        if (PracticeLocationDetail.SupervisingProviders == null)
                            PracticeLocationDetail.SupervisingProviders = [];

                        //console.log(data.midlevelPractioner);

                        PracticeLocationDetail.SupervisingProviders.push({ 'SupervisingProviderID': data.supervisingProvider.SupervisingProviderID, 'ProfileID': data.supervisingProvider.ProfileID, 'Profile': $filter('filter')($scope.supervisingProviders, { ProfileID: data.supervisingProvider.ProfileID })[0] });
                        $rootScope.operateSecondCancelControl('');

                        $scope.resetSupervisingProvider();

                        messageAlertEngine.callAlertMessage("supervisingProvidersSuccessMsg", "Supervising Provider Added successfully.", "success", true);
                    } else {
                        messageAlertEngine.callAlertMessage("supervisingProvidersErrorMsg", data.status, "danger", true);
                    }
                },
                error: function (e) {

                }
            });


        }
    };

    $scope.updateSupervisingProvider = function (PracticeLocationDetail) {
        //console.log("=================================");
        //console.log(PracticeLocationDetail);
        var validationStatus = false;
        var url = null;
        var $formData = null;

        $formData = $('#editSupervisingPractioner');

        url = "/Profile/PracticeLocation/UpdateSupervisingProviderAsync?profileId=" + profileId;

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
                        var obj = $filter('filter')(PracticeLocationDetail.SupervisingProviders, { ProfileID: data.supervisingProvider.SupervisingProviderID })[0];
                        PracticeLocationDetail.SupervisingProviders.splice(PracticeLocationDetail.SupervisingProviders.indexOf(obj), 1);
                        $('#supervisingWarningModal').modal('hide');
                        PracticeLocationDetail.SupervisingProviders.splice()
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

    // Save Covering Colleagues Details
    $scope.savePatner = function (PracticeLocationDetail) {        
       
       
        var url = "/Profile/PracticeLocation/AddPracticeColleagueAsync";
        
        //Simple POST request example (passing data) :
        $.ajax({
            url: url,
            type: 'POST',
            data: {
                PracticeLocationDetailID: $scope.PracticeLocationDetailID, ProfileID: $scope.SupervisingProvider.ProfileID, StatusType: "1"
            },
            async: false,
            cache: false,
            contentType: false,
            processData: false,
            success: function (data) {
                if (data.status == "true") {
                    PracticeLocationDetail.PracticeColleague.push({ 'PracticeColleagueID': data.PracticeColleague.PracticeColleagueID, 'ProfileID': data.PracticeColleague.ProfileID, 'Profile': $filter('filter')($scope.supervisingProvider, { ProfileID: data.PracticeColleague.ProfileID })[0] });
                    $rootScope.operateSecondCancelControl('');
                    $scope.resetSupervisingProvider();
                    messageAlertEngine.callAlertMessage("addedPartnerDetails", "Covering Colleagues Information Added successfully.!!!", "success", true);

                } else {
                    messageAlertEngine.callAlertMessage("ErrorInPartners", data.status, "danger", true);
                }
            },
            error: function (e) {

            }
        });
           
    }

    // Save Covering Colleagues Details
    $scope.removePatner = function (index) {


        var url = "/Profile/PracticeLocation/RemovePartnerAsync";

        //Simple POST request example (passing data) :
        $.ajax({
            url: url,
            type: 'POST',
            data: new {
                PracticeLocationDetailID: $scope.PracticeLocationDetail.PracticeLocationDetailID, ProfileID: $scope.partner.ProfileID, StatusType: "2"
            },
            async: false,
            cache: false,
            contentType: false,
            processData: false,
            success: function (data) {
                if (data.status == "true") {
                    PracticeLocationDetail.PracticeColleague.splice(index, 1);
                    $rootScope.operateSecondCancelControl('');
                    messageAlertEngine.callAlertMessage("addedPartnerDetails", "Covering Colleagues Information Removed successfully.!!!", "success", true);

                } else {
                    messageAlertEngine.callAlertMessage("ErrorInPartners", data.status, "danger", true);
                }
            },
            error: function (e) {

            }
        });

    }
   
  
}]);

