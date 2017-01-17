//============================ Directive for return Template ===========================
//========= Dynamic Form Generator ==================================
profileApp.service('dynamicFormGenerateService', function ($compile) {
    this.getForm = function (scope, formContain) {
        return $compile(formContain)(scope);
    };
});
//------------------ File Selected validation ---------------------------
profileApp.directive('validFile', function () {
    return {
        require: 'ngModel',
        link: function (scope, el, attrs, ngModel) {
            ngModel.$render = function () {
                ngModel.$setViewValue(el.val());
            };

            el.bind('change', function () {
                scope.$apply(function () {
                    ngModel.$render();
                });
            });
        }
    };
});
//=========================== Controller declaration ==========================
profileApp.controller('profileDemographicsController', function ($scope, $rootScope, $http, masterDataService, locationService) {
    
    //Get all the data for the profile on document on ready
    $(document).ready(function () {
        $("#Loading_Message").text("Gathering Profile Data...");
        //console.log("Getting data....");
        $http({
            method: 'GET',
            url: '/Profile/MasterProfile/get?profileId=' + profileId
        }).success(function (data, status, headers, config) {
            //console.log(data);
            try {
                for (key in data) {
                    //console.log(key);
                    $rootScope.$emit(key, data[key]);
                    //call respective controller to load data (PSP)
                }
                $("#Loading_Message").text("Inserting Profile Data...");
                $("#loadingData").hide("fast");
            } catch (e) {
                //console.log("error getting data back");
                $("#loadingData").hide("fast");
            }

        }).error(function (data, status, headers, config) {
            //console.log(status);
            $("#loadingData").hide("fast");
        });
    });

    //--------------------------------- User Profile Data ----------------------------------
    $scope.Provider = {};
    $scope.message = {};
    //---------------------------- Personal Details ---------------------------
    $rootScope.$on('ProfilePhotoPath', function (event, val) {
        //console.log("ProviderPhotoPath............");
        //console.log(val);
        $scope.Provider.ProfilePhotoPath = val;
    });
    $rootScope.$on('PersonalDetail', function (event, val) {
        //console.log("PersonalDetail............");
        //console.log(val);
        $scope.Provider.PersonalDetails = val;
    });
    $rootScope.$on('OtherLegalNames', function (event, val) {
        //console.log("OtherLegalNames............");
        //console.log(val);
        if (val.length > 0) {
            for (var i in val) {
                val[i].StartDate = ConvertDateFormat(val[i].StartDate);
                val[i].EndDate = ConvertDateFormat(val[i].EndDate);
            }
        }
        $scope.Provider.OtherLegalNames = val;
    });
    $rootScope.$on('HomeAddresses', function (event, val) {
        //console.log("HomeAddresses............");
        //console.log(val);
        if (val.length > 0) {
            for (var i in val) {
                val[i].LivingEndDate = ConvertDateFormat(val[i].LivingEndDate);
                val[i].LivingFromDate = ConvertDateFormat(val[i].LivingFromDate);
            }
        }
        $scope.HomeAddresses = val;
    });
    $rootScope.$on('ContactDetail', function (event, val) {
        //console.log("ContactDetail............");
        //console.log(val);
        $scope.Provider.ContactDetails = val;
    });
    $rootScope.$on('PersonalIdentification', function (event, val) {
        //console.log("PersonalIdentification............");
        //console.log(val);
        $scope.Provider.PersonalIdentification = val;
    });
    $rootScope.$on('BirthInformation', function (event, val) {
        //console.log("BirthInformation............");
        //console.log(val);
        if (val) {
            val.DateOfBirth = ConvertDateFormat(val.DateOfBirth);
        }
        $scope.Provider.BirthInformation = val;
    });
    $rootScope.$on('VisaDetail', function (event, val) {
        //console.log("VisaDetail............");
        //console.log(val);
        if (val && val.VisaInfo) {
            val.VisaInfo.VisaExpirationDate = ConvertDateFormat(val.VisaInfo.VisaExpirationDate);
        }
        $scope.Provider.VisaDetails = val;
    });
    $rootScope.$on('LanguageInfo', function (event, val) {
        //console.log("LanguageInfo............");
        //console.log(val);
        $scope.Provider.LanguageInfo = val;
    });

    //============================= Data From Master Data Table Required For Visa Details ======================
    masterDataService.getMasterData("/Profile/MasterData/GetAllProviderTypes").then(function (val) {
        $scope.ProviderTypes = val;
        //console.log("jshjdhgjhdjghjhgj");
        //console.log(val);
    });

    masterDataService.getMasterData("/Profile/MasterData/getallvisatypes").then(function (val) {
        //console.log("visaTypes getting from data base...........");
        $scope.VisaTypes = val;
        //console.log(val);
    });
    masterDataService.getMasterData("/Profile/MasterData/getallvisastatus").then(function (val) {
        //console.log("VisaStatus getting from data base....");
        $scope.VisaStatuses = val;
        //console.log(val);
    });
    //========================== list of Languages =================
    $scope.Languages = Languages;

    $scope.CountryDialCodes = countryDailCodes;

    $scope.CountryOfissue = CountryOfissue;

    //$scope.PreferredWrittenContacts = [{
    //    PreferredWrittenContactID: null,
    //    ContactType: "Email",
    //    PreferredWrittenContactType: 1,
    //    StatusType: 2,
    //    PreferredIndex: 1
    //}, {
    //    PreferredWrittenContactID: null,
    //    ContactType: "Fax",
    //    PreferredWrittenContactType: 2,
    //    StatusType: 2,
    //    PreferredIndex: 2
    //}, {
    //    PreferredWrittenContactID: null,
    //    ContactType: "HomeAddress",
    //    PreferredWrittenContactType: 3,
    //    StatusType: 2,
    //    PreferredIndex: 3
    //}];
    $scope.PreferredContacts = [{
        PreferredContactID: null,
        ContactType: "Home Phone",
        PreferredWrittenContactType: 1,
        StatusType: 1,
        PreferredIndex: 1
    },{
        PreferredContactID: null,
        ContactType: "Fax",
        PreferredWrittenContactType: 2,
        StatusType: 1,
        PreferredIndex: 2
    }, {
        PreferredContactID: null,
        ContactType: "Mobile",
        PreferredWrittenContactType: 3,
        StatusType: 1,
        PreferredIndex: 3
    }, {
        PreferredContactID: null,
        ContactType: "Email",
        PreferredWrittenContactType: 4,
        StatusType: 1,
        PreferredIndex: 4
    }, {
        PreferredContactID: null,
        ContactType: "Pager",
        PreferredWrittenContactType: 5,
        StatusType: 1,
        PreferredIndex: 5
    }];

    //------------------------------------------------------------------Address Auto-Complete---------------------------------------------------------------------------//
    /*
     Method addressAutocomplete() gets the details of a location
         Method takes input of location details entered in the text box.
  */
    $scope.addressHomeAutocomplete = function (location) {
        if (location.length == 2 && !angular.isObject(location)) {         //As soon as the length of the string reaches 3 and the location is not an object
            $scope.Locations = locationService.getLocations(location);      //A call is made to the locations service which returns a list of relevant locations
            //console.log($scope.Locations);
        }
        else if (angular.isObject(location)) {                      //When user select a city the location variable then holds the object of the respective location.
            if (!$scope.tempObject) {
                $scope.tempObject = {};
            }
            $scope.tempObject.City = location.City;
            $scope.tempObject.ZipCode = location.Zipcode;         //So we check if its an object and if it is then we asign the values like 
            $scope.tempObject.State = location.State;                          //city name, state name, etc to the respective models
            $scope.tempObject.Country = location.Country;                      ////which is inturn is displayed to the users
            $scope.tempObject.County = location.County;
        }
    }

    //-------------------------------- Birth Information Change Status ------------------------------
    $scope.addressAutocompleteForBirthInformation = function (location) {
        if (location.length > 2 && !angular.isObject(location)) {         //As soon as the length of the string reaches 3 and the location is not an object
            $scope.Locations = locationService.getLocations(location);      //A call is made to the locations service which returns a list of relevant locations
        }
        else if (angular.isObject(location)) {                      //When user select a city the location variable then holds the object of the respective location.
            if (!$scope.tempObject) {
                $scope.tempObject = {};
            }
            $scope.tempObject.CityOfBirth = location.City;        //So we check if its an object and if it is then we asign the values like 
            $scope.tempObject.StateOfBirth = location.State;                          //city name, state name, etc to the respective models
            $scope.tempObject.CountryOfBirth = location.Country;                      ////which is inturn is displayed to the users
            $scope.tempObject.CountyOfBirth = location.County;
        }
    }
    //------------------ Provider Type (Title) Select un-select Method here -----------------------------
    $scope.ViewPersonalDetails = true;
    $scope.PersonalDetailsToggle = function () {
        if ($scope.ViewPersonalDetails) {
            $scope.ViewPersonalDetails = false;
            $scope.getProviderTypesAndTempData();
        } else {
            $scope.ViewPersonalDetails = true;
            $scope.isHasError = false;
        }
    };
    $scope.getProviderTypesAndTempData = function () {
        $scope.TempPersonalDetailsForEdit = angular.copy($scope.Provider.PersonalDetails);
        $scope.tempProviderTypes = angular.copy($scope.ProviderTypes);

        if ($scope.TempPersonalDetailsForEdit.ProviderTitles) {
            for (var i in $scope.TempPersonalDetailsForEdit.ProviderTitles) {
                for (var j in $scope.tempProviderTypes) {
                    if ($scope.TempPersonalDetailsForEdit.ProviderTitles[i].ProviderType.ProviderTypeID == $scope.tempProviderTypes[j].ProviderTypeID) {
                        $scope.tempProviderTypes.splice(j, 1);
                    }
                }
            }
        } else {
            $scope.TempPersonalDetailsForEdit.ProviderTitles = [];
        }
        //console.log($scope.TempPersonalDetailsForEdit);
    };

    //-------------------------------------- Select Provider type ------------------------------------------
    $scope.SelectProviderType = function (providertype) {
        $scope.TempPersonalDetailsForEdit.ProviderTitles.push({
            ProviderType: providertype,
            ProviderTitleID: null,
            ProviderTypeId: providertype.ProviderTypeID,
            StatusType: 1
        });
        $scope.tempProviderTypes.splice($scope.tempProviderTypes.indexOf(providertype), 1);
    };
    //------------------------------------- UN-select Provider type -----------------------------------------
    $scope.ActionProviderType = function (providerTitle, condition) {
        if (condition == "remove") {
            $scope.TempPersonalDetailsForEdit.ProviderTitles.splice($scope.TempPersonalDetailsForEdit.ProviderTitles.indexOf(providerTitle), 1)
            $scope.tempProviderTypes.push(providerTitle.ProviderType);
        } else if (condition == "deactivate") {
            providerTitle.StatusType = 2;
        } else if (condition == "activate") {
            providerTitle.StatusType = 1;
        }
    };
    
    //-------------------------------------- File Upload Action ---------------------------------------------
    $scope.ProfileUpload = function (Form_Id) {
        ResetFormForValidation($("#" + Form_Id));
        if ($("#" + Form_Id).valid()) {

            var $form = $("#ProfilePic")[0];

            $.ajax({
                url: '/Profile/Demographic/FileUploadAsync?profileId=' + profileId,
                type: 'POST',
                data: new FormData($form),
                async: false,
                cache: false,
                contentType: false,
                processData: false,
                success: function (data) {
                    //console.log(data);
                    if (data.status == "true") {
                        $scope.Provider.ProfilePhotoPath = data.ProfileImagePath;
                        $rootScope.visibilityControl = "";
                        $scope.message.status = "success";
                        $scope.message.desc = "Profile Picture uploaded successfully.";
                        $rootScope.callAlert('alertPersonalDetailsSuccess');
                        //console.log($scope.Provider.ProfileImagePath);
                    } else {
                        alert(data.status);
                    }
                }
            });
        }
    };

    //-------------------------------------- File Upload Action ---------------------------------------------
    $scope.ProfilePictureRemove = function (profilePicpath) {
        $.ajax({
            url: '/Profile/Demographic/FileRemoveAsync?profileId=' + profileId,
            type: 'POST',
            data: new FormData(profilePicpath),
            async: false,
            cache: false,
            contentType: false,
            processData: false,
            success: function (data) {
                //console.log(data);
                if (data.status == "true") {
                    $scope.Provider.ProfilePhotoPath = data.ProfileImagePath;
                    $rootScope.visibilityControl = "";
                    $scope.message.status = "success";
                    $scope.message.desc = "Profile Picture removed successfully";
                    $rootScope.callAlert('alertPersonalDetailsSuccess');
                } else {
                    alert(data.status);
                }
            }
        });
    };
    //----------------------------------- Update Personal Details Function --------------------------------------
    $scope.isHasError = false;
    $scope.DemographicsUpdatePersonalDetails = function (Form_Id) {
        ResetFormForValidation($("#" + Form_Id));
        if ($("#" + Form_Id).valid() && $scope.TempPersonalDetailsForEdit.ProviderTitles.length > 0) {
            $scope.isHasError = false;
            $.ajax({
                url: '/Profile/Demographic/UpdatePersonalDetailsAsync?profileId=' + profileId,
                type: 'POST',
                data: new FormData($("#" + Form_Id)[0]),
                async: false,
                cache: false,
                contentType: false,
                processData: false,
                success: function (data) {
                    //console.log(data);
                    if (data.status == "true") {
                        if (data.personalDetail.ProviderTitles.length > 0) {
                            for(var i in data.personalDetail.ProviderTitles){
                                for (var j = 0; j < $scope.ProviderTypes.length;j++){
                                    if (data.personalDetail.ProviderTitles[i].ProviderTypeId == $scope.ProviderTypes[j].ProviderTypeID) {
                                        data.personalDetail.ProviderTitles[i].ProviderType = $scope.ProviderTypes[j];
                                    }
                                }
                            }
                        }
                        $scope.Provider.PersonalDetails = data.personalDetail;
                        $scope.message.status = "success";
                        $scope.message.desc = "Personal Details updated successfully.";
                        $rootScope.callAlert('alertPersonalDetailsSuccess');
                        FormReset($("#" + Form_Id));
                        $scope.ViewPersonalDetails = true;
                    } else {
                        $scope.message.status = "danger";
                        $scope.message.desc = data.status.split(",");
                        $rootScope.callAlert('alertPersonalDetailsError');
                    }
                }
            });
        } else {
            $scope.isHasError = true;
            //console.log("Sorry! Personal Information could not be updated.");
        }
    };

    //----------------------------------- Add Personal Details Function --------------------------------------
    $scope.saveOtherLegalName = function (Form_Div_Id) {
        var $form = $("#" + Form_Div_Id).find("form");
        ResetFormForValidation($form);
        if ($form.valid()) {
            $.ajax({
                url: '/Profile/Demographic/AddOtherLegalNameAsync?profileId=' + profileId,
                type: 'POST',
                data: new FormData($form[0]),
                async: false,
                cache: false,
                contentType: false,
                processData: false,
                success: function (data) {
                    //console.log(data);
                    if (data.status == "true") {
                        data.otherLegalName.EndDate = ConvertDateFormat(data.otherLegalName.EndDate);
                        data.otherLegalName.StartDate = ConvertDateFormat(data.otherLegalName.StartDate);
                        $scope.Provider.OtherLegalNames.push(data.otherLegalName);
                        $rootScope.tempObject = {};
                        $rootScope.visibilityControl = "";
                        //$rootScope.visibilityControl = $scope.Provider.OtherLegalNames.length-1 + "_ViewOtherLegalName";
                        FormReset($form);
                        $scope.message.status = "success";
                        $scope.message.desc = "Other Legal Name saved successfully.";
                        $rootScope.callAlert('alertOtherLegalNameSuccess');
                    } else {
                        $scope.message.status = "danger";
                        $scope.message.desc = data.status.split(",");
                        $rootScope.callAlert('alertOtherLegalNameError');
                    }
                }
            });
        } else {
            //alert("Sorry! Other Legal Name could not be saved.");
        }
    };
    //--------------------------- Update Other Legal Name Function ------------------------------
    $scope.updateOtherLegalName = function (Form_Div_Id, index) {
        var $form = $("#" + Form_Div_Id).find("form");
        ResetFormForValidation($form);
        if ($form.valid()) {
            $.ajax({
                url: '/Profile/Demographic/UpdateOtherLegalNameAsync?profileId=' + profileId,
                type: 'POST',
                data: new FormData($form[0]),
                async: false,
                cache: false,
                contentType: false,
                processData: false,
                success: function (data) {
                    //console.log(data);
                    if (data.status == "true") {
                        data.otherLegalName.EndDate = ConvertDateFormat(data.otherLegalName.EndDate);
                        data.otherLegalName.StartDate = ConvertDateFormat(data.otherLegalName.StartDate);
                        $scope.Provider.OtherLegalNames[index] = data.otherLegalName;
                        //$rootScope.visibilityControl = "";
                        $rootScope.visibilityControl = index + "_ViewOtherLegalName";
                        FormReset($form);
                        $scope.message.status = "success";
                        $scope.message.desc = "Other Legal Name updated successfully.";
                        $rootScope.callAlert('alertOtherLegalNameSuccess' + index);
                    } else {
                        $scope.message.status = "danger";
                        $scope.message.desc = data.status.split(",");
                        $rootScope.callAlert('alertOtherLegalNameError'+ index);
                    }
                }
            });
        } else {
            //console.log("Sorry! Other Legal Name could not be updated.");
        }
    };

    //--------------------------- Save Home Address Function ------------------------------
    $scope.saveHomeAddress = function (Form_Div_Id) {
        //================== Save Here ============
        var $form = $("#" + Form_Div_Id).find("form");
        ResetFormForValidation($form);
        if ($form.valid()) {
            $.ajax({
                url: '/Profile/Demographic/AddHomeAddressAsync?profileId=' + profileId,
                type: 'POST',
                data: new FormData($form[0]),
                async: false,
                cache: false,
                contentType: false,
                processData: false,
                success: function (data) {
                    //console.log(data);
                    if (data.status == "true") {
                        if (data.homeAddress.AddressPreferenceType==1) {
                            for(var i in $scope.HomeAddresses){
                                $scope.HomeAddresses[i].AddressPreferenceType = 2;
                                $scope.HomeAddresses[i].AddressPreference = "Secondary";
                            }
                        }
                        data.homeAddress.LivingEndDate = ConvertDateFormat(data.homeAddress.LivingEndDate);
                        data.homeAddress.LivingFromDate = ConvertDateFormat(data.homeAddress.LivingFromDate);
                        $scope.HomeAddresses.push(data.homeAddress);
                        $rootScope.tempObject = {};
                        $rootScope.visibilityControl = "";
                        //$rootScope.visibilityControl = $scope.HomeAddresses.length - 1 + "_ViewHomeAddress";
                        FormReset($form);
                        $scope.message.status = "success";
                        $scope.message.desc = "Home Address saved successfully.";
                        $rootScope.callAlert('alertHomeAddressSuccess');
                    } else {
                        $scope.message.status = "danger";
                        $scope.message.desc = data.status.split(",");
                        $rootScope.callAlert('alertHomeAddressError');
                    }
                }
            });
        } else {
            //console.log("Sorry Some thing Wrong with you !!!!!!!!!!!");
        }
    };
    //--------------------------- Update Home Address Function ------------------------------
    $scope.updateHomeAddress = function (Form_Div_Id, index) {
        var $form = $("#" + Form_Div_Id).find("form");
        ResetFormForValidation($form);
        if ($form.valid()) {
            $.ajax({
                url: '/Profile/Demographic/UpdateHomeAddressAsync?profileId=' + profileId,
                type: 'POST',
                data: new FormData($form[0]),
                async: false,
                cache: false,
                contentType: false,
                processData: false,
                success: function (data) {
                    //console.log(data);
                    if (data.status == "true") {
                        if (data.homeAddress.AddressPreferenceType == 1) {
                            for (var i in $scope.HomeAddresses) {
                                $scope.HomeAddresses[i].AddressPreferenceType = 2;
                                $scope.HomeAddresses[i].AddressPreference = "Secondary";
                            }
                        }
                        data.homeAddress.LivingEndDate = ConvertDateFormat(data.homeAddress.LivingEndDate);
                        data.homeAddress.LivingFromDate = ConvertDateFormat(data.homeAddress.LivingFromDate);
                        $scope.HomeAddresses[index] = data.homeAddress;
                        //$rootScope.visibilityControl = "";
                        $rootScope.visibilityControl = index + "_ViewHomeAddress";
                        FormReset($form);
                        $scope.message.status = "success";
                        $scope.message.desc = "Home Address updated successfully.";
                        $rootScope.callAlert('alertHomeAddressSuccess' + index);
                    } else {
                        $scope.message.status = "danger";
                        $scope.message.desc = data.status.split(",");
                        $rootScope.callAlert('alertHomeAddressError' + index);
                    }
                }
            });
        } else {
            //console.log("Sorry Some thing Wrong with you !!!!!!!!!!!");
        }
    };
    //--------------------------- Remove Home Address Function ------------------------------
    $scope.removeHomeAddress = function (index) {
        for (var i in $scope.HomeAddresses) {
            if (index == i) {
                $scope.HomeAddresses.splice(index, 1);
                break;
            }
        }
    };

    $scope.ViewContactDetails = true;

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
        $scope.TempContactDetailsForEdit = angular.copy($scope.Provider.ContactDetails);
        $scope.TempPreferredWrittenContacts = angular.copy($scope.PreferredWrittenContacts);
        $scope.TempPreferredContacts = angular.copy($scope.PreferredContacts);

        if ($scope.TempContactDetailsForEdit == null) {
            $scope.TempContactDetailsForEdit = {
                PhoneDetails: [],
                EmailIDs: [],
                PreferredWrittenContacts: [],
                PreferredContacts: []
            };
        } else {
            $scope.PreferredContactTypesArray = $scope.TempContactDetailsForEdit.PreferredContacts;
        }
    };

    //Mehod for adding unique contact types to an array
    $scope.PreferredContactTypesArray = [];
    $scope.addPreferredContactTypesToArray = function (contactType) {
        var status = true;
        for (var i in $scope.PreferredContactTypesArray) {
            if ($scope.PreferredContactTypesArray[i].PreferredWrittenContactType == contactType) {
                status = false;
                break;
            }
        }
        if (status) {
            $scope.PreferredContactTypesArray.push($scope.PreferredContacts[contactType-1]);
        }
    };

    //Method to return the object based not the contact Type entered
    //$scope.getContactTypeInfo = function (contactType) {
    //    var PreferredContactsObj = {};
    //    switch (contactType) {
    //        case 1:
    //            PreferredContactsObj = $scope.PreferredContacts[4];
    //            PreferredContactsObj.StatusType = 1;
    //            return PreferredContactsObj;
    //        case 2:
    //            PreferredContactsObj = $scope.PreferredContacts[1];
    //            PreferredContactsObj.StatusType = 1;
    //            return PreferredContactsObj;
    //        case 3:
    //            PreferredContactsObj = $scope.PreferredContacts[2];
    //            PreferredContactsObj.StatusType = 1;
    //            return PreferredContactsObj;
    //        case 4:
    //            PreferredContactsObj = $scope.PreferredContacts[3];
    //            PreferredContactsObj.StatusType = 1;
    //            return PreferredContactsObj;
    //        case 5:
    //            PreferredContactsObj = $scope.PreferredContacts[0];
    //            PreferredContactsObj.StatusType = 1;
    //            return PreferredContactsObj;
    //    }
    //};

    //method to set prefferd contact priority and preffered contact index
    $scope.selectedPrefferdContact = function (pc, index) {
        $scope.pcPriority = pc;
        $scope.pcIndex = index;
    };

    //--------------------------------- Method to change the priority of contact ------------------------------
    $scope.ChangePreferredContactsPriority = function (condition) {
        var index = $scope.PreferredContactTypesArray.indexOf($scope.pcPriority);
        if (condition == "increase" && index !== 0) {
            $scope.PreferredContactTypesArray[index].ProficiencyIndex = index;
            $scope.PreferredContactTypesArray[index - 1].ProficiencyIndex = index + 1;
            $scope.pcIndex = index - 1;
        } else if (condition == "decrease" && index !== $scope.PreferredContactTypesArray.length - 1) {
            $scope.PreferredContactTypesArray[index].ProficiencyIndex = index + 2;
            $scope.PreferredContactTypesArray[index + 1].ProficiencyIndex = index + 1;
            $scope.pcIndex = index + 1;
        }

        $scope.PreferredContactTypesArray.sort(function (a, b) { return a.ProficiencyIndex - b.ProficiencyIndex });
    };

    //========================== Personal Identification Save Method ======================
    $scope.IsPersonalIdentificationHasError = false;
    $scope.DemographicsPersonalIdentificationSave = function (Form_Id) {
        ResetFormForValidation($("#" + Form_Id));
        if ($("#" + Form_Id).valid()) {
            $.ajax({
                url: '/Profile/Demographic/UpdatePersonalIdentificationAsync?profileId=' + profileId,
                type: 'POST',
                data: new FormData($("#" + Form_Id)[0]),
                async: false,
                cache: false,
                contentType: false,
                processData: false,
                success: function (data) {
                    //console.log(data);
                    if (data.status == "true") {
                        $scope.Provider.PersonalIdentification = data.personalIdentification;
                        $rootScope.tempObject = {};
                        $rootScope.visibilityControl = "";
                        $scope.message.status = "success";
                        $scope.message.desc = "Personal Identification updated successfully!!!!";
                        $rootScope.callAlert('alertPersonalIdentificationSuccess');
                        FormReset($("#" + Form_Id));
                    } else {
                        $scope.message.status = "danger";
                        $scope.message.desc = data.status.split(",");
                        $rootScope.callAlert('alertPersonalIdentificationError');
                    }
                }
            });
        } else {
            //console.log("Sorry Some thing Wrong with you !!!!!!!!!!!");
        }
    };
    //--------------------------- end ------------------------------
    //-------------------------------- Save Birth Information Function --------------------------------
    $scope.saveBirthInformation = function (Form_Id) {
        ResetFormForValidation($("#" + Form_Id));
        if ($("#" + Form_Id).valid()) {
            $.ajax({
                url: '/Profile/Demographic/UpdateBirthInformationAsync?profileId=' + profileId,
                type: 'POST',
                data: new FormData($("#" + Form_Id)[0]),
                async: false,
                cache: false,
                contentType: false,
                processData: false,
                success: function (data) {
                    //console.log(data);
                    if (data.status == "true") {
                        data.birthInformation.DateOfBirth = ConvertDateFormat(data.birthInformation.DateOfBirth);
                        $scope.Provider.BirthInformation = data.birthInformation;
                        $rootScope.tempObject = {};
                        $rootScope.visibilityControl = "";
                        $scope.message.status = "success";
                        $scope.message.desc = "Birth Information saved successfully.";
                        $rootScope.callAlert('alertBirthInformationSuccess');
                        FormReset($("#" + Form_Id));
                    } else {
                        $scope.message.status = "danger";
                        $scope.message.desc = data.status.split(",");
                        $rootScope.callAlert('alertBirthInformationError');
                    }
                }
            });
        } else {
            //console.log("Sorry! Birth Information could not be saved.");
        }
    };
    //--------------------------- Save Visa Details Function ------------------------------------
    $scope.saveEthnicityVisaDetails = function (Form_Id) {
        ResetFormForValidation($("#" + Form_Id));
        if ($("#" + Form_Id).valid()) {
            $.ajax({
                url: '/Profile/Demographic/UpdateVisaDetailAsync?profileId=' + profileId,
                type: 'POST',
                data: new FormData($("#" + Form_Id)[0]),
                async: false,
                success: function (data) {
                    //console.log(data);
                    if (data.status == "true") {
                        if (data.visaDetail.VisaInfo) {
                            data.visaDetail.VisaInfo.VisaExpirationDate = ConvertDateFormat(data.visaDetail.VisaInfo.VisaExpirationDate);
                            for (var i in $scope.VisaTypes) {
                                if (data.visaDetail.VisaInfo.VisaTypeID == $scope.VisaTypes[i].VisaTypeID) {
                                    data.visaDetail.VisaInfo.VisaType = $scope.VisaTypes[i];
                                    break;
                                }
                            }
                            for (var i in $scope.VisaStatuses) {
                                if (data.visaDetail.VisaInfo.VisaStatusID == $scope.VisaStatuses[i].VisaStatusID) {
                                    data.visaDetail.VisaInfo.VisaStatus = $scope.VisaStatuses[i];
                                    break;
                                }
                            }
                        }
                        $scope.Provider.VisaDetails = data.visaDetail;
                        $rootScope.tempObject = {};
                        $rootScope.visibilityControl = "";
                        $scope.message.status = "success";
                        $scope.message.desc = "Visa Details updated successfully.";
                        $rootScope.callAlert('alertVisaDetailsSuccess');
                        FormReset($("#" + Form_Id));
                    } else {
                        $scope.message.status = "danger";
                        $scope.message.desc = data.status.split(",");
                        $rootScope.callAlert('alertVisaDetailsError');
                    }
                },
                cache: false,
                contentType: false,
                processData: false
            });
        } else {
            //console.log("Sorry Some thing Wrong with you !!!!!!!!!!!");
        }
    };

    //============================== Contact Details Phone Number, Fax, Mobile Number and Email =================================
    //---------------------------- Add home Phone Number Function ------------------------------------
    $scope.AddPhones = function (obj, condition) {
        obj.push({
            PhoneDetailID: null,
            Number: "",
            CountryCode: "+1",
            PhoneTypeEnum: condition,
            Preference: "Secondary",
            PreferenceType: 2,
            Status: "Active",
            StatusType: 1
        });
        $('[data-toggle="tooltip"]').tooltip();
        $scope.addPreferredContactTypesToArray(condition);
    };

    //---------------------------- Add Email Ids Function ------------------------------------
    $scope.AddEmail = function (obj) {
        obj.push({
            EmailDetailID: null,
            EmailAddress: "",
            Preference: "Secondary",
            PreferenceType: 2,
            Status: "Active",
            StatusType: 1
        });
        $scope.addPreferredContactTypesToArray(4);
        $('[data-toggle="tooltip"]').tooltip();
    };
    //---------------- Remove Method for Contact Details -----------------------
    $scope.RemoveContactDetails = function (index, data) {
        data.splice(index, 1);
        //if (data.length == 1) {
        //    data[0].Preference = "Primary";
        //    data[0].PreferenceType = 1;
        //}
    };
    //---------------------------- Preferred Contact change function ------------------------
    $scope.PreferredContactChange = function (obj, status) {
        if (status) {
            obj.StatusType = 1;
        } else {
            obj.StatusType = 2;
        }
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
            return a.PreferredIndex - b.PreferredIndex;
        });
    };
    //------------------------------- Country Code Popover Show by Id ---------------------------------------
    $scope.showContryCodeDiv = function (countryCodeDivId) {
        changeVisibilityOfCountryCode();
        $("#" + countryCodeDivId).show();
    };
    //-------------------------- Contact Details Required Conditions-----------------------------------------
    $scope.ContactDetailsPhoneConditionFunction = function (obj, data, phoneType) {
        if (data.length > 1) {
            for (var i in data) {
                if (data[i].PhoneTypeEnum == phoneType) {
                    if (i == data.indexOf(obj)) {
                        if (data[i].PreferenceType == 2) {
                            data[i].StatusType = 1;
                            data[i].PreferenceType = 1;
                        }
                    } else {
                        data[i].PreferenceType = 2;
                    }
                }
            }
        }
        $('[data-toggle="tooltip"]').tooltip();
    };
    $scope.ContactDetailsEmailConditionFunction = function (index, data) {
        if (data.length > 1) {
            for (var i in data) {
                if (i == index) {
                    if (data[i].PreferenceType == 2) {
                        data[i].StatusType = 1;
                        data[i].PreferenceType = 1;
                    }
                } else {
                    data[i].PreferenceType = 2;
                }
            }
        }
        $('[data-toggle="tooltip"]').tooltip();
    };
    //--------------------- Modal confirmation for Inactive database Data ----------------------
    $scope.showConfirmation = function (arrayObj, index, condition) {
        $scope.selectedArrayObject = arrayObj;
        $scope.selectedObjectIndex = index;
        if (condition == "phone") {
            //$scope.ConfirmationMessage = "Are you sure, want to remove Number  " + arrayObj[index].CountryCode + "-" + arrayObj[index].Number + " from DataBase";
            $scope.ConfirmationMessage = "Are you sure";
        } else {
            //$scope.ConfirmationMessage = "Are you sure, want to remove EmailId  " + arrayObj[index].EmailAddress + " from DataBase";
            $scope.ConfirmationMessage = "Are you sure";
        }
        $('#ConfirmationContactDetails').modal('show');
    };
    //-------------------- Modal Confirmation Hide and Remove from Array ------------------------
    $scope.InactiveContactDetails = function () {
        if ($scope.selectedArrayObject[$scope.selectedObjectIndex].StatusType == 1) {
            $scope.selectedArrayObject[$scope.selectedObjectIndex].StatusType = 2;
        }
        else if ($scope.selectedArrayObject[$scope.selectedObjectIndex].StatusType == 2) {
            $scope.selectedArrayObject[$scope.selectedObjectIndex].StatusType = 1;
        }
        
        //if ($scope.selectedArrayObject.length == 1) {
        //    $scope.selectedArrayObject[0].Preference = "Primary";
        //    $scope.selectedArrayObject[0].PreferenceType = 1;
        //}



        $('#ConfirmationContactDetails').modal('hide');
    };
    //--------------------------------- Save Contact Details Function -----------------------------------
    $scope.ContactDetailsErrorList = [];
    $scope.saveContactDetails = function (Form_Id) {
        ResetFormForValidation($("#" + Form_Id));
        if ($("#" + Form_Id).valid()) {
            $.ajax({
                url: '/Profile/Demographic/UpdateContactDetailsAsync?profileId=' + profileId,
                type: 'POST',
                data: new FormData($("#" + Form_Id)[0]),
                async: false,
                success: function (data) {
                    //console.log(data);
                    if (data.status == "true") {
                        //if (data.contactDetail.PhoneDetails.length > 0) {
                        //    for (var i = 0; i < data.contactDetail.PhoneDetails.length; i++) {
                        //        if (data.contactDetail.PhoneDetails[i].StatusType == 2) {
                        //            data.contactDetail.PhoneDetails.splice(data.contactDetail.PhoneDetails.indexOf(data.contactDetail.PhoneDetails[i]), 1);
                        //        }
                        //    }
                        //} if (data.contactDetail.EmailIDs.length > 0) {
                        //    for (var i = 0; i < data.contactDetail.EmailIDs.length; i++) {
                        //        if (data.contactDetail.EmailIDs[i].StatusType == 2) {
                        //            data.contactDetail.EmailIDs.splice(data.contactDetail.EmailIDs.indexOf(data.contactDetail.EmailIDs[i]), 1);
                        //        }
                        //    }
                        //}
                         
                        $scope.Provider.ContactDetails = data.contactDetail;
                        $scope.TempContactDetailsForEdit = [];
                        $scope.message.status = "success";
                        $scope.message.desc = "Contact Details updated successfully.";
                        $rootScope.callAlert('alertContactDetailsSuccess');
                        FormReset($("#" + Form_Id));
                        $scope.ViewContactDetails = true;
                    } else {
                        $scope.message.status = "danger";
                        $scope.message.desc = data.status.split(",");
                        $rootScope.callAlert('alertContactDetailsError');
                    }
                },
                cache: false,
                contentType: false,
                processData: false
            });
        } else {
            //console.log("Sorry! Contact Details could not be updated.");
        }
    };

    //------------------------- Language know search select action function -----------------------
    $scope.TempLanguageForEdit = [];

    $scope.SelectLanguageKnown = function (lang) {
        $scope.TempLanguageForEdit.KnownLanguages.push({
            KnownLanguageID: null,
            Language: lang.name,
            ProficiencyIndex: $scope.TempLanguageForEdit.length
        });
        $scope.tempLanguages.splice($scope.tempLanguages.indexOf(lang), 1);
    };
    $scope.UnselectLanguage = function (lang) {
        for (i in $scope.TempLanguageForEdit.KnownLanguages) {
            if ($scope.TempLanguageForEdit.KnownLanguages[i] == lang) {
                $scope.TempLanguageForEdit.KnownLanguages.splice($scope.TempLanguageForEdit.KnownLanguages.indexOf(lang), 1);
            }
        }
        for (var i in $scope.Languages) {
            if ($scope.Languages[i].name == lang.Language) {
                $scope.tempLanguages.push($scope.Languages[i]);
            }
        }
    };

    $scope.selectedLanguage = function (lang, index) {
        $scope.langForPriority = lang;
        $scope.selectedIndex = index;
    };

    $scope.PriorityChange = function (condition) {
        var index = $scope.TempLanguageForEdit.KnownLanguages.indexOf($scope.langForPriority);
        if (condition == "increase") {
            $scope.TempLanguageForEdit.KnownLanguages[index].ProficiencyIndex = index;
            $scope.TempLanguageForEdit.KnownLanguages[index - 1].ProficiencyIndex = index + 1;
            $scope.selectedIndex = index - 1;
        } else if (condition == "decrease") {
            $scope.TempLanguageForEdit.KnownLanguages[index].ProficiencyIndex = index + 2;
            $scope.TempLanguageForEdit.KnownLanguages[index + 1].ProficiencyIndex = index + 1;
            $scope.selectedIndex = index + 1;
        }
        $scope.TempLanguageForEdit.KnownLanguages.sort(function (a, b) { return a.ProficiencyIndex - b.ProficiencyIndex });
    };


    //$scope.PriorityChange = function (condition, index) {
    //    if (condition == "increase") {
    //        $scope.TempLanguageForEdit.KnownLanguages[index].ProficiencyIndex = index;
    //        $scope.TempLanguageForEdit.KnownLanguages[index - 1].ProficiencyIndex = index + 1;
    //    } else if (condition == "decrease") {
    //        $scope.TempLanguageForEdit.KnownLanguages[index].ProficiencyIndex = index + 2;
    //        $scope.TempLanguageForEdit.KnownLanguages[index + 1].ProficiencyIndex = index + 1;
    //    }
    //    $scope.TempLanguageForEdit.KnownLanguages.sort(function (a, b) {
    //        return a.ProficiencyIndex - b.ProficiencyIndex;
    //    });
    //};
    //-------------- reusable for array generate ------------------
    $scope.getArray = function (number) {
        var temp = [];
        for (var i = 0; i < number; i++) {
            temp.push(i);
        }
        return temp;
    };

    $scope.shoLanguageList = function (divId) {
        $("#" + divId).show();
    };

    $scope.IsExistInArray = function (collection, key) {
        var keys = [];
        status = true;
        angular.forEach(collection, function (item) {
            if (keys.indexOf(key) === -1) {
                keys.push(key);
            } else {
                status = false;
            }
        });
        return status;
    };

    $scope.ViewLanguageKnown = true;

    $scope.LanguageKnownToggle = function () {
        if ($scope.ViewLanguageKnown) {
            $scope.ViewLanguageKnown = false;
            $scope.getLanguagesAndTempData();
        } else {
            $scope.ViewLanguageKnown = true;
        }
    };
    $scope.getLanguagesAndTempData = function () {
        $scope.TempLanguageForEdit = angular.copy($scope.Provider.LanguageInfo);
        $scope.tempLanguages = angular.copy($scope.Languages);

        if ($scope.TempLanguageForEdit) {
            for (var i in $scope.TempLanguageForEdit.KnownLanguages) {
                for (var j in $scope.tempLanguages) {
                    if ($scope.TempLanguageForEdit.KnownLanguages[i].Language == $scope.tempLanguages[j].name) {
                        $scope.tempLanguages.splice(j, 1);
                    }
                }
            }
            $scope.TempLanguageForEdit.KnownLanguages.sort(function (a, b) {
                return a.ProficiencyIndex - b.ProficiencyIndex;
            });
        } else {
            $scope.TempLanguageForEdit = {
                KnownLanguages: [],
            };
        }

        $scope.langForPriority = null;
        $scope.selectedIndex = {};
    };

    //------------------------------------------------------------------------------------------
    $scope.saveLanguages = function (Form_Id) {
        ResetFormForValidation($("#" + Form_Id));
        if ($("#" + Form_Id).valid()) {
            $.ajax({
                url: "/Profile/Demographic/UpdateLanguagesAsync?profileId=" + profileId,
                type: 'POST',
                data: new FormData($("#" + Form_Id)[0]),
                async: false,
                cache: false,
                contentType: false,
                processData: false,
                success: function (data) {
                    //console.log(data);
                    if (data.status == "true") {
                        $scope.Provider.LanguageInfo = data.languageInfo;
                        $scope.TempLanguageForEdit = [];
                        $scope.message.status = "success";
                        $scope.message.desc = "Languages Know updated successfully.";
                        $rootScope.callAlert('alertLanguageKnownSuccess');
                        FormReset($("#" + Form_Id));
                        $scope.ViewLanguageKnown = true;
                    } else {
                        $scope.message.status = "danger";
                        $scope.message.desc = data.status.split(",");
                        $rootScope.callAlert('alertLanguageKnownError');
                    }
                }
            });
        } else {
            //console.log("Sorry Some thing Wrong with you !!!!!!!!!!!");
        }
    };
});

//================================= Hide All country code popover =========================
$(document).click(function (event) {
    if (!$(event.target).hasClass("countryCodeClass") && $(event.target).parents(".countryDailCodeContainer").length === 0) {
        $(".countryDailCodeContainer").hide();
    }
    if (!$(event.target).hasClass("form-control") && $(event.target).parents(".LanguageSelectAutoList").length === 0) {
        $(".LanguageSelectAutoList").hide();
    }
    if (!$(event.target).hasClass("form-control") && $(event.target).parents(".ProviderTypeSelectAutoList").length === 0) {
        $(".ProviderTypeSelectAutoList").hide();
    }
});

//Method to change the visiblity of country code popover
 var changeVisibilityOfCountryCode = function () {
        $(".countryDailCodeContainer").hide(); // method will close any other country code div already open.
};

$(document).ready(function () {
    $(".countryDailCodeContainer").hide();
    $(".ProviderTypeSelectAutoList").hide();
});