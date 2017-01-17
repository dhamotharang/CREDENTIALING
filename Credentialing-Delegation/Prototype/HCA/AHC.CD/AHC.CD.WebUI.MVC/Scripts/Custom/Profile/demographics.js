//============================ Directive for return Template ===========================
//========= Dynamic Form Generator ==================================
profileApp.service('dynamicFormGenerateService', function ($compile) {
    this.getForm = function (scope, formContain) {
        return $compile(formContain)(scope);
    };
});

//========= Country data and UniqueData Provider ==================================
profileApp.service('countryDropDownService', function ($compile) {
    this.getStates = function (countries, countryCode) {
        for (var i in countries) {
            if (countries[i].Code == countryCode) {
                return countries[i].States;
            }
        }
    };
    this.getCounties = function (states, state) {
        var Counties = [];
        for (var i in states) {
            if (states[i].State == state) {
                Counties.push(states[i]);
            }
        }
        return Counties;
    };
    this.getCities = function (counties, county) {
        var Cities = [];
        for (var i in counties) {
            if (counties[i].County == county) {
                Cities.push(counties[i]);
            }
        }
        return Cities;
    };
});

//=========================== Angular Filter method Unique array list ===========================
profileApp.filter('unique', function () {
    return function (collection, keyname) {
        var output = [], keys = [];

        angular.forEach(collection, function (item) {
            var key = item[keyname];
            if (keys.indexOf(key) === -1) {
                keys.push(key);
                output.push(item);
            }
        });
        return output;
    };
});
//=========================== Controller declaration ==========================
profileApp.controller('profileDemographicsController', function ($scope, $rootScope, $http, countryDropDownService) {

    //Get all the data for the profile on document on ready

    //$(document).ready(function () {
    //    console.log("Getting data....");
    //    $("#loadingData").show();
    //    $http({
    //        method: 'GET',
    //        url: '/Profile/MasterProfile/get?profileId=' + profileId
    //    }).success(function (data, status, headers, config) {
    //        console.log(data);
    //        try {
    //            for (key in data) {
    //                console.log(key);
    //                $rootScope.$emit(key, data[key]);
    //                //call respective controller to load data (PSP)
    //            }               

    //        } catch (e) {
    //            console.log("error getting data back");
                
    //        }
    //        $("#loadingData").hide("fast");
    //    }).error(function (data, status, headers, config) {
    //        console.log(status);
    //       $("#loadingData").hide("fast");
    //    });
    //});
    // Demographics control variable initialization
    $scope.Provider = {};
    $scope.Provider.PersonalDetails = {
        PersonalDetailID: 1,
        Salutation: "Medical Doctor (MD)",
        Title: "Dr",
        TitleType: 4,
        FirstName: "Pariksith",
        MiddleName: "",
        LastName: "Singh",
        Suffix: "",
        Gender: "Male",
        GenderType: 1,
        MaidenName: "",
        MaritalStatus: "Married",
        MaritalStatusType: 1,
        SpouseName: "Maria Scunziano-Singh, MD",
        ProfilePhotoPath: "/Content/Images/Providers/Pariksith_Singh.jpg",
        HasOtherName: false,
        PhotoImage: null
    };

    $scope.IsUseAnotherName = false;

    $scope.Provider.OtherLegalNames = [
	//{
	//OtherLegalNameID: 1,
	//OtherFirstName: "Sophia",
	//OtherMiddleName: "Emma",
	//OtherLastName: "Madison",
	//Suffix: "JR",
	//StartDate: new Date(2008, 9, 22),
	//EndDate: new Date(2013, 12, 29),
	//DocumentPath: "/Content/Document/DocPreview.pdf",
	//File: null,
	//Status: "Active",
	//ActiveInactive:1
	//}
    ];

    $scope.HomeAddresses = [{
        HomeAddressID: "1",
        Number: "",
        UnitNumber: "######",
        Country: "######",
        Street: "######",
        State: "######",
        City: "######",
        County: "######",
        ZipCode: "#####-####",
        LivingFromDate: "",
        LivingEndDate: "",
        AddressPreference: "Primary",
        Status: "Active",
        IsPresentlyStaying: true,
        IsPrimary: true
    }];

    $scope.PreferredWrittenContact = [{
        ContactType: "Email",
        IsChecked: false,
        PreferredIndex: 1
    }, {
        ContactType: "Fax",
        IsChecked: false,
        PreferredIndex: 2
    }, {
        ContactType: "HomeAddress",
        IsChecked: false,
        PreferredIndex: 3
    }, {
        ContactType: "Pager",
        IsChecked: false,
        PreferredIndex: 4
    }
    ];
    $scope.PreferredContact = [{
        ContactType: "Email",
        IsChecked: false,
        PreferredIndex: 1
    }, {
        ContactType: "Fax",
        IsChecked: false,
        PreferredIndex: 2
    }, {
        ContactType: "Mobile",
        IsChecked: false,
        PreferredIndex: 3
    }, {
        ContactType: "Pager",
        IsChecked: false,
        PreferredIndex: 4
    }
    ];
    $scope.phoneTypes = ["Home", "Fax", "Mobile", "Work"];

    // new latest entity model according
    $scope.Provider.ContactDetails = {
        ContactDetailID: 1,
        PhoneDetails: [{
            PhoneDetailID: 1,
            Number: "##########",
            CountryCode: "+1",
            PhoneType: "Home",
            Preference: "Primary",
            Status: "Active"
        }, {
            PhoneDetailID: 2,
            Number: "##########",
            CountryCode: "+1",
            PhoneType: "Fax",
            Preference: "Primary",
            Status: "Active"
        }, {
            PhoneDetailID: 3,
            Number: "##########",
            CountryCode: "+1",
            PhoneType: "Mobile",
            Preference: "Primary",
            Status: "Active"
        }],
        EmailIDs: [
            {
                EmailDetailID: 1,
                EmailAddress: "psingh@accesshealthcarellc.net",
                Preference: "Primary",
                Status: "Active"
            }
        ],
        PreferredWrittenContacts: [{
            PreferredWrittenContactID: 1,
            ContactType: "Email",
            PreferredIndex: 1
        }],
        PreferredContacts: [{
            PreferredContactID: 1,
            ContactType: "Email",
            PreferredIndex: 1
        }]
    };

    //--------------------------------- need modification for required data to present in ui --------------------------
    angular.element(document).ready(function () {
        //---------------------- Contact details Data Arrangement According to requirements -------------------
        $scope.Provider.UserContactDetails = $scope.GetContactDetailsRequiredOutPutFormate($scope.Provider.ContactDetails);
    });
    //------------------- method for return Phones according types ---------------
    $scope.GetContactDetailsRequiredOutPutFormate = function (ContactDetails) {
        var userContactDetails = {};

        userContactDetails.ContactDetailID = ContactDetails.ContactDetailID;
        userContactDetails.EmailIDs = ContactDetails.EmailIDs;
        userContactDetails.PreferredWrittenContacts = ContactDetails.PreferredWrittenContacts;
        userContactDetails.PreferredContacts = ContactDetails.PreferredContacts;

        userContactDetails.PhoneDetails = {};
        userContactDetails.PhoneDetails.HomePhones = [];
        userContactDetails.PhoneDetails.HomeFaxes =[];
        userContactDetails.PhoneDetails.HomeMobiles =[];

        for (var i in ContactDetails.PhoneDetails) {
            if (ContactDetails.PhoneDetails[i].PhoneType == "Home") {
                userContactDetails.PhoneDetails.HomePhones.push(ContactDetails.PhoneDetails[i]);
            } else if (ContactDetails.PhoneDetails[i].PhoneType == "Fax") {
                userContactDetails.PhoneDetails.HomeFaxes.push(ContactDetails.PhoneDetails[i]);
            } else if (ContactDetails.PhoneDetails[i].PhoneType == "Mobile") {
                userContactDetails.PhoneDetails.HomeMobiles.push(ContactDetails.PhoneDetails[i]);
            }
        }
        return userContactDetails;
    };

    $scope.Provider.PersonalIdentification = {
        PersonalIdentificationID: 1,
        SSN: "#########",
        DL: "S520-660-68-167-1",
        SSNCertificatePath: "/Content/Document/DocPreview.pdf",
        SSNCertificateFile: null,
        DLCertificatePath: "/Content/Document/SINGH - DRIVERS LIC.pdf",
        DLCertificateFile: null
    };

    //==================================== Country Code and Country List ========================
    //-------------- country data comes from CountryList.js and countryDialCodes.js---------------
    $scope.Countries = Countries;
    $scope.States = $scope.Countries[1].States;
    $scope.CountryDialCodes = countryDailCodes;
    //---------------------- get states ---------------------
    $scope.getStates = function (countryCode) {
        $scope.States = countryDropDownService.getStates($scope.Countries, countryCode);
        $scope.Counties = [];
        $scope.Cities = [];
        $scope.HomeAddress.State = $scope.putempty;
        $scope.HomeAddress.County = $scope.putempty;
        $scope.HomeAddress.City = $scope.putempty;
        resetStateSelectTwoStyle();
    };
    $scope.getCounties = function (state) {
        $scope.Counties = countryDropDownService.getCounties($scope.States, state);
        $scope.Cities = [];
        $scope.HomeAddress.County = $scope.putempty;
        $scope.HomeAddress.City = $scope.putempty;
    };
    $scope.getCities = function (county) {
        $scope.Cities = countryDropDownService.getCities($scope.Counties, county);
        $scope.HomeAddress.City = $scope.putempty;
    };

    $scope.ViewPersoanlDetails = true;
    $scope.viewProfilePicture = true;
    $scope.ViewPersoanlIdentification = true;
    $scope.ViewContactDetails = true;
    //=============== Personal Details Edit View Toggle ===================================
    $scope.ProfilePictureToggle = function () {
        $scope.viewProfilePicture = false;
    };
    $scope.ProfileUpload = function () {
        $scope.viewProfilePicture = true;
    };

    $scope.tempMethod = function (condition) {
        if (condition) {
            $scope.addOtherLegalName();
        } else {
            $scope.IsOtherLegalNameHasError = false;
            $scope.newLegalNameForm = false;
            $scope.SelectedOtherLegalNameIndex = -1;
            $scope.OtherLegalName = {};
        }
    };
    $scope.PersonalDetailsToggle = function () {
        if ($scope.ViewPersoanlDetails) {
            $scope.ViewPersoanlDetails = false;
            $scope.TempPersonalDetailsForEdit = angular.copy($scope.Provider.PersonalDetails);
        } else {
            $scope.ViewPersoanlDetails = true;
        }
    };

    $scope.PersonalIdentificationToggle = function () {
        if ($scope.ViewPersoanlIdentification) {
            $scope.ViewPersoanlIdentification = false;
            $scope.TempPersonalIdentificationForEdit = angular.copy($scope.Provider.PersonalIdentification);
        } else {
            $scope.ViewPersoanlIdentification = true;
        }
    };

    $scope.ContactDetailsToggle = function () {
        if ($scope.ViewContactDetails) {
            $scope.ViewContactDetails = false;
            $scope.getTempContactDetailsForEdit();
        } else {
            $scope.ViewContactDetails = true;
        }
    };
    //----------------------- Get Temp Data for Edit data ---------------------------
    $scope.getTempContactDetailsForEdit = function () {
        $scope.TempContactDetailsForEdit = angular.copy($scope.Provider.UserContactDetails);
        $scope.TempPreferredWrittenContact = angular.copy($scope.PreferredWrittenContact);
        $scope.TempPreferredContact = angular.copy($scope.PreferredContact);

        for (var i in $scope.TempPreferredWrittenContact) {
            for (var j in $scope.TempContactDetailsForEdit.PreferredWrittenContacts) {
                if ($scope.TempContactDetailsForEdit.PreferredWrittenContacts[j].ContactType == $scope.TempPreferredWrittenContact[i].ContactType) {
                    $scope.TempPreferredWrittenContact[i].IsChecked = true;
                }
            }
        }
        for (var i in $scope.TempPreferredContact) {
            for (var j in $scope.TempContactDetailsForEdit.PreferredContacts) {
                if ($scope.TempContactDetailsForEdit.PreferredContacts[j].ContactType == $scope.TempPreferredContact[i].ContactType) {
                    $scope.TempPreferredContact[i].IsChecked = true;
                }
            }
        }
    };
    //=================== Demographics Primary Information Personal Details Save ================
    $scope.IsPersonalDetailsHasError = false;
    $scope.DemographicsUpdatePersonalDetails = function (Form_Id, Data) {
        if ($("#" + Form_Id).valid()) {
            $.ajax({
                url: '/Profile/Demographic/UpdatePersonalDetails',
                type: 'POST',
                data: new FormData($("#" + Form_Id)[0]),
                async: false,
                success: function (data) {
                    console.log(data);
                    if (data.status == "True") {
                        $scope.Provider.PersonalDetails = angular.copy($scope.TempPersonalDetailsForEdit);
                        $scope.TempPersonalDetailsForEdit = [];
                        //--------- temp Data -------------------
                        
                        if ($scope.Provider.PersonalDetails.TitleType == 1) {
                            $scope.Provider.PersonalDetails.Title = "Mr";
                        } else if ($scope.Provider.PersonalDetails.TitleType == 2) {
                            $scope.Provider.PersonalDetails.Title = "Miss";
                        } else if ($scope.Provider.PersonalDetails.TitleType == 3) {
                            $scope.Provider.PersonalDetails.Title = "Mrs";
                        } else if ($scope.Provider.PersonalDetails.TitleType == 4) {
                            $scope.Provider.PersonalDetails.Title = "Dr";
                        }

                        if ($scope.Provider.PersonalDetails.GenderType == 1) {
                            $scope.Provider.PersonalDetails.Gender = "Male";
                        } else if ($scope.Provider.PersonalDetails.GenderType == 2) {
                            $scope.Provider.PersonalDetails.Gender = "Female";
                        }
                        if ($scope.Provider.PersonalDetails.MaritalStatusType == 1) {
                            $scope.Provider.PersonalDetails.MaritalStatus = "Married";
                        } else if ($scope.Provider.PersonalDetails.MaritalStatusType == 2) {
                            $scope.Provider.PersonalDetails.MaritalStatus = "Unmarried";
                        } else if ($scope.Provider.PersonalDetails.MaritalStatusType == 3) {
                            $scope.Provider.PersonalDetails.MaritalStatus = "Divorced";
                        }
                        //---------- end ----------------------
                        $scope.ViewPersoanlDetails = true;
                        $scope.IsPersonalDetailsHasError = false;
                    } else {
                        $scope.IsPersonalDetailsHasError = true;
                        $scope.PersonalDetailsErrorList = data.status.split(",");
                    }
                },
                cache: false,
                contentType: false,
                processData: false
            });
        } else {
            console.log("Sorry Some thing Wrong with you dear!!!!!!!!!!!");
        }
    };
    //========================== Personal Identification Save Method ======================
    $scope.IsPersonalIdentificationHasError = false;
    $scope.DemographicsPersonalIdentificationSave = function (Form_Id) {
        if ($("#" + Form_Id).valid()) {
            $.ajax({
                url: '/Profile/Demographic/UpdatePersonalIdentification',
                type: 'POST',
                data: new FormData($("#" + Form_Id)[0]),
                async: false,
                success: function (data) {
                    console.log(data);
                    if (data.status == "True") {
                        $scope.Provider.PersonalIdentification = angular.copy($scope.TempPersonalIdentificationForEdit);
                        $scope.TempPersonalIdentificationForEdit = {};
                        $scope.ViewPersoanlIdentification = true;
                        $scope.IsPersonalIdentificationHasError = false;
                    } else {
                        $scope.IsPersonalIdentificationHasError = true;
                        $scope.PersonalIdentificationErrorList = data.status.split(",");
                    }
                },
                cache: false,
                contentType: false,
                processData: false
            });
        } else {
            console.log("Sorry Some thing Wrong with you !!!!!!!!!!!");
        }
    };
    //--------------------------- end ------------------------------

    //=============== Other Legal Name Conditions ==================
    $scope.SelectedOtherLegalNameIndex = -1;
    $scope.showingDetails = false;
    $scope.newLegalName = false;
    //$scope.selectedOtherLegalName = {};

    //=============== Home Address Conditions ==================
    $scope.homeAddressFormStatus = false;
    $scope.newHomeAddressForm = false;

    //======================================== Other Legal Name Methods ====================================
    $scope.addOtherLegalName = function () {
        $scope.newLegalNameForm = true;
        $scope.SelectedOtherLegalNameIndex = -1;
        $scope.OtherLegalName = {
            OtherLegalNameID: 2,
            OtherFirstName: "",
            OtherMiddleName: "",
            OtherLastName: "",
            Suffix: "",
            StartDate: new Date(),
            EndDate: new Date(),
            DocumentPath: "/Content/Document/DocPreview.pdf",
            File: null,
            Status: "Active",
            ActiveInactive: 1
        };
    };

    $scope.viewEditOtherLegalName = function (index, OtherLegalName, condition) {
        if (condition == 'edit') {
            $scope.legalNameFormStatus = true;
            $scope.newLegalNameForm = false;
            $scope.OtherLegalName = angular.copy(OtherLegalName);
            $scope.SelectedOtherLegalNameIndex = index;
        } else if (condition == 'view') {
            $scope.legalNameFormStatus = false;
            $scope.newLegalNameForm = false;
            $scope.OtherLegalName = OtherLegalName;
            $scope.SelectedOtherLegalNameIndex = index;
        } else if (condition == 'close') {
            $scope.OtherLegalName = {};
            $scope.SelectedOtherLegalNameIndex = -1;
        }
    };
    $scope.IsOtherLegalNameHasError = false;
    $scope.saveOtherLegalName = function (Form_Div_Id, OtherLegalName) {
        //================== Save Here ============
        var $form = $("#" + Form_Div_Id).find("form");
        console.log($form[0]);
        if ($form.valid()) {
            $.ajax({
                url: '/Profile/Demographic/AddOtherLegalName',
                type: 'POST',
                data: new FormData($form[0]),
                async: false,
                success: function (data) {
                    console.log(data);
                    if (data.status == "True") {
                        $scope.Provider.OtherLegalNames.push(OtherLegalName);
                        $scope.OtherLegalName = {};
                        $scope.closeOtherLegalNameForm($form);
                    } else {
                        $scope.IsOtherLegalNameHasError = true;
                        $scope.OtherLegalNameErrorList = data.status.split(",");
                    }
                },
                cache: false,
                contentType: false,
                processData: false
            });
        } else {
            //alert("Sorry Some Thing Wrong With You !!!!!!!!");
        }
    };

    $scope.updateOtherLegalName = function (Form_Div_Id, OtherLegalName, index) {
        var $form = $("#" + Form_Div_Id).find("form");
        if ($form.valid()) {
            $.ajax({
                url: '/Profile/Demographic/UpdateOtherLegalName',
                type: 'POST',
                data: new FormData($form[0]),
                async: false,
                success: function (data) {
                    console.log(data);
                    if (data.status == "True") {
                        $scope.Provider.OtherLegalNames[index] = OtherLegalName;
                        $scope.OtherLegalName = {};
                        $scope.closeOtherLegalNameForm($form);
                    } else {
                        $scope.IsOtherLegalNameHasError = true;
                        $scope.OtherLegalNameErrorList = data.status.split(",");
                    }
                },
                cache: false,
                contentType: false,
                processData: false
            });
        } else {
            console.log("Sorry Some thing Wrong with you dear!!!!!!!!!!!");
        }
    };

    $scope.cancelOtherLegalName = function (Form_Div_Id) {
        $scope.closeOtherLegalNameForm($("#" + Form_Div_Id).find("form"));
    };

    $scope.removeOtherLegalName = function (index) {
        for (var i in $scope.OtherLegalNames) {
            if (index == i) {
                $scope.OtherLegalNames.splice(index, 1);
                break;
            }
        }
    };

    $scope.closeOtherLegalNameForm = function ($form) {
        $scope.IsOtherLegalNameHasError = false;
        $scope.newLegalNameForm = false;
        $scope.SelectedOtherLegalNameIndex = -1;
        $scope.OtherLegalName = {};
        FormReset($form);
    };
    //=================================================== Home Address ======================================================
    $scope.SelectedHomeAddressIndex = -1;
    $scope.addHomeAddress = function () {
        $scope.newHomeAddressForm = true;
        $scope.SelectedHomeAddressIndex = -1;
        $scope.HomeAddress = {
            Country: "US",
            AddressPreference: "Primary",
            Status: "Active",
            LivingFromDate: new Date(),
            LivingEndDate: new Date()
        };
    };

    $scope.viewEditHomeAddress = function (index, HomeAddress, condition) {
        if (condition == 'edit') {
            $scope.homeAddressFormStatus = true;
            $scope.newHomeAddressForm = false;
            $scope.SelectedHomeAddressIndex = index;
            $scope.HomeAddress = angular.copy(HomeAddress);
        } else if (condition == 'view') {
            $scope.homeAddressFormStatus = false;
            $scope.newHomeAddressForm = false;
            $scope.SelectedHomeAddressIndex = index;
            $scope.HomeAddress = HomeAddress;
        } else if (condition == 'close') {
            $scope.HomeAddress = {};
            $scope.SelectedHomeAddressIndex = -1;
        }
    };

    $scope.saveHomeAddress = function (Form_Div_Id, HomeAddress) {
        //================== Save Here ============
        var $form = $("#" + Form_Div_Id).find("form");

        if ($form.valid()) {
            $http.post('/Profile/Demographic/AddHomeAddress', HomeAddress).success(function (data, status, headers, config) {
                if (data.status == "True") {
                    console.log(data);
                    $scope.HomeAddresses.push(HomeAddress);
                    $scope.HomeAddress = {};
                    $scope.closeHomeAddressForm($form);
                } else {
                    $scope.IsHomeAddressHasError = true;
                    $scope.HomeAddressErrorList = data.status.split(",");
                }
            }).error(function (data, status, headers, config) {
                //alert("Sorry Some Thing Wrong With You!!!!!!!!");
            });
        } else {
            console.log("Sorry Some thing Wrong with you !!!!!!!!!!!");
        }
    };

    $scope.updateHomeAddress = function (Form_Div_Id, HomeAddress, index) {
        var $form = $("#" + Form_Div_Id).find("form");

        if ($form.valid()) {
            $http.post('/Profile/Demographic/UpdateHomeAddress', HomeAddress).success(function (data, status, headers, config) {
                console.log(data);
                if (data.status == "True") {
                    $scope.HomeAddresses[index] = HomeAddress;
                    $scope.HomeAddress = {};
                    $scope.closeHomeAddressForm($form);
                } else {
                    $scope.IsHomeAddressHasError = true;
                    $scope.HomeAddressErrorList = data.status.split(",");
                }
            }).error(function (data, status, headers, config) {
                alert("Sorry Some Thing Wrong With You Dear!!!!!!!!");
            });
        } else {
            console.log("Sorry Some thing Wrong with you !!!!!!!!!!!");
        }
    };

    $scope.cancelHomeAddress = function (Form_Div_Id) {
        $scope.closeHomeAddressForm($("#" + Form_Div_Id).find("form"));
    };

    $scope.closeHomeAddressForm = function ($form) {
        $scope.IsHomeAddressHasError = false;
        $scope.newHomeAddressForm = false;
        $scope.SelectedHomeAddressIndex = -1;
        $scope.HomeAddress = {};
        FormReset($form);
    };

    $scope.removeHomeAddress = function (index) {
        for (var i in $scope.HomeAddresses) {
            if (index == i) {
                $scope.HomeAddresses.splice(index, 1);
                break;
            }
        }
    };

    //============================== Contact Details Phone Number, Fax, Mobile Number and Email =================================
    //---------------------------- home Phone Number ------------------------------------
    $scope.AddPhones = function (obj, condition) {
        obj.push({
            PhoneDetailID: null,
            Number: "",
            CountryCode: "+1",
            PhoneType: condition,
            Preference: "Secondary",
            Status: "Active"
        });
        $('[data-toggle="tooltip"]').tooltip();
    };

    //=============================== Home Email Ids =============================
    $scope.AddEmail = function (obj) {
        obj.push({
            EmailDetailID: null,
            EmailAddress: "",
            Preference: "Secondary",
            Status: "Active"
        });
        $('[data-toggle="tooltip"]').tooltip();
    };
    //---------------- Remove Method for Contact Details -----------------------
    $scope.RemoveContactDetails = function (index, data) {
        data.splice(index, 1);
        if (data.length == 1) {
            data[0].Preference = "Primary";
        }
    };
    //-------------------- Preferred Written Contact and Preferred Contact action Method ----------------
    $scope.PreferredWrittenContactChange = function (obj, status) {
        if (status) {
            $scope.TempContactDetailsForEdit.PreferedWrittenContact.push(obj);
        } else {
            $scope.TempContactDetailsForEdit.PreferedWrittenContact.splice($scope.TempContactDetailsForEdit.PreferedWrittenContact.indexOf(obj), 1);
        }
    };
    $scope.PreferredContactChange = function (objArray) {
        objArray.sort(function (a, b) {
            return b.IsChecked - a.IsChecked
        });
    };
    $scope.PreferredContactPriority = function (condition, index, objArray) {
        if (condition == "increase") {
            objArray[index].PreferredIndex = index;
            objArray[index - 1].PreferredIndex = index + 1;
        } else if (condition == "decrease") {
            objArray[index].PreferredIndex = index + 2;
            objArray[index + 1].PreferredIndex = index + 1;
        }
        objArray.sort(function (a, b) {
            return a.PreferredIndex - b.PreferredIndex
           });
    };
    //========================== Country Code Popover Show by Id ==========================
    $scope.showContryCodeDiv = function (countryCodeDivId) {
        $("#" + countryCodeDivId).show();
    };
    //================== Contact Details Required Conditions ============================================
    $scope.ContactDetailsConditionFunction = function (index, data, condition) {
        if (condition == "IsPrimary" && data.length > 1) {
            for (var i in data) {
                if (i == index) {
                    if (data[i].Preference == "Primary") {
                        //data[i].Preference = "Secondary";
                    } else {
                        data[i].Status = "Active";
                        data[i].Preference = "Primary";
                    }
                } else {
                    data[i].Preference = "Secondary";
                }
            }
        } else if (condition == "IsActive" && data.length > 1) {
            for (var i in data) {
                if (i == index) {
                    if (data[i].Status == "Active") {
                        if (data[i].Preference) {
                            data[i].Preference = "Secondary";
                        }
                        data[i].Status = "Inactive";
                    } else {
                        data[i].Status = "Active";
                    }
                    break;
                }
            }
        }
        $('[data-toggle="tooltip"]').tooltip();
    };
    $scope.showConfirmation = function () {
        $('#ConfirmationContactDetails').modal();
    };
    //========================= Save Contact Details ====================================
    $scope.saveContactDetails = function (contaictDetails) {
        //-------------------- contact preferred selected adding function --------------------------------
        var contactdetails = angular.copy($scope.TempContactDetailsForEdit);
        contactdetails.PreferredWrittenContacts = [];
        contactdetails.PreferredContacts = [];

        for (var i in $scope.TempPreferredWrittenContact) {
            if ($scope.TempPreferredWrittenContact[i].IsChecked == true) {
                contactdetails.PreferredWrittenContacts.push({
                    PreferredWrittenContactID: null,
                    ContactType: $scope.TempPreferredWrittenContact[i].ContactType,
                    PreferredIndex: $scope.TempPreferredWrittenContact[i].PreferredIndex
                });
            }
        }
        for (var i in $scope.TempPreferredContact) {
            if ($scope.TempPreferredContact[i].IsChecked == true) {
                contactdetails.PreferredContacts.push({
                    PreferredWrittenContactID: null,
                    ContactType: $scope.TempPreferredContact[i].ContactType,
                    PreferredIndex: $scope.TempPreferredContact[i].PreferredIndex
                });
            }
        }
        contactdetails.PhoneDetails = [];
        contactdetails.PhoneDetails = $scope.TempContactDetailsForEdit.PhoneDetails.HomePhones.concat($scope.TempContactDetailsForEdit.PhoneDetails.HomeFaxes, $scope.TempContactDetailsForEdit.PhoneDetails.HomeMobiles);
        //-------------------- END --------------------------------
        $scope.Provider.UserContactDetails = $scope.GetContactDetailsRequiredOutPutFormate(contactdetails);
        console.log($scope.Provider.UserContactDetails);

        $scope.TempContactDetailsForEdit = [];
        $scope.ViewContactDetails = true;

        //================== Save Here ============
        //$http.post('/Profile/Demographic/UpdateContactDetails', contactdetails).success(function (data, status, headers, config) {
        //    console.log(data);
        //    if (data.status == "True") {
        //        $scope.Provider.UserContactDetails = $scope.GetContactDetailsRequiredOutPutFormate(contactdetails);

        //        $scope.TempContactDetailsForEdit = [];
        //        $scope.ViewContactDetails = true;
        //    } else {
        //        console.log("Sorry Some thing Wrong with you !!!!!!!!!!!");
        //    }
        //}).error(function (data, status, headers, config) {
        //    console.log("Sorry Some thing Wrong with you !!!!!!!!!!!");
        //});
    };

        //================================================== Preferred method for written contact ===================================

    $scope.selectedpwc = function (pwc, index) {
        $scope.pwcPriority = pwc;
        $scope.pwcIndex = index;
    }
    $scope.ChangePreferredWrittenContactsPriority = function (condition) {
        var index = $scope.TempPreferredWrittenContact.indexOf($scope.pwcPriority);
        if (condition == "increase") {
            $scope.TempPreferredWrittenContact[index].ProficiencyIndex = index;
            $scope.TempPreferredWrittenContact[index - 1].ProficiencyIndex = index + 1;
            $scope.pwcIndex = index-1;
        } else if (condition == "decrease") {
            $scope.TempPreferredWrittenContact[index].ProficiencyIndex = index + 2;
            $scope.TempPreferredWrittenContact[index + 1].ProficiencyIndex = index + 1;
            $scope.pwcIndex = index+1;
        }
        $scope.TempPreferredWrittenContact.sort(function (a, b) { return a.ProficiencyIndex - b.ProficiencyIndex });
    };

    //================================================== Preferred method for  contact ===================================

    $scope.selectedpc = function (pc, index) {
        $scope.pcPriority = pc;
        $scope.pcIndex = index;
    }
    $scope.ChangePreferredContactsPriority = function (condition) {
        var index = $scope.TempPreferredContact.indexOf($scope.pcPriority);
        if (condition == "increase") {
            $scope.TempPreferredContact[index].ProficiencyIndex = index;
            $scope.TempPreferredContact[index - 1].ProficiencyIndex = index + 1;
            $scope.pcIndex = index-1;
        } else if (condition == "decrease") {
            $scope.TempPreferredContact[index].ProficiencyIndex = index + 2;
            $scope.TempPreferredContact[index + 1].ProficiencyIndex = index + 1;
            $scope.pcIndex = index+1;
        }
        
        $scope.TempPreferredContact.sort(function (a, b) { return a.ProficiencyIndex - b.ProficiencyIndex });
    };
});

//========================================== Form Reset method for All the form =========================
var FormReset = function ($form) {

    // get validator object
    var $validator = $form.validate();

    // get errors that were created using jQuery.validate.unobtrusive
    var $errors = $form.find(".field-validation-error span");

    // trick unobtrusive to think the elements were successfully validated
    // this removes the validation messages
    $errors.each(function () {
        $validator.settings.success($(this));
    });
    // clear errors from validation
    $validator.resetForm();
};
//================================= Hide All country code popover =========================
$(document).click(function (event) {
    if (!$(event.target).hasClass("btn") && $(event.target).parents(".countryDailCodeContainer").length === 0) {
        $(".countryDailCodeContainer").hide();
    }
});

$(document).ready(function () {
    $(".countryDailCodeContainer").hide();

    //$("#HomeAddressCountry").select2({
    //    placeholder: "select country"
    //});
    //applySelectTwoStyle([{ element: "#HomeAddressCountry", placeholder: "select country" }, { element: "#HomeAddressState", placeholder: "select state" }, { element: "#HomeAddressCounty", placeholder: "select county" }, { element: "#HomeAddressCity", placeholder: "select city" }])
});

var applySelectTwoStyle = function (selectlist) {
    //console.log("Method called")
    $.each(selectlist, function (key, value) {
        $(value.element).select2({
            placeholder: value.placeholder
        });
    });
};

var resetStateSelectTwoStyle = function (countryId) {

};

