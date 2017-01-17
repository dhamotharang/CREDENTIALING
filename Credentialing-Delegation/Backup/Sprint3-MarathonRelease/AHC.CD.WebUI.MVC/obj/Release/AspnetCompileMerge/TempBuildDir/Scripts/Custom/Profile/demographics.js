//============================ Directive for return Template ===========================
//========= Dynamic Form Generator ==================================
profileApp.service('dynamicFormGenerateService', function ($compile) {
    this.getForm = function (scope, formContain) {
        return $compile(formContain)(scope);
    };
});

//=========================== Controller declaration ==========================
profileApp.controller('profileDemographicsController', function ($scope, $rootScope, $http, masterDataService) {

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
        if (val) {
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

    $scope.PreferredWrittenContacts = [{
        PreferredWrittenContactID: null,
        ContactType: "Email",
        PreferredWrittenContactType: 1,
        StatusType: 2,
        PreferredIndex: 1
    }, {
        PreferredWrittenContactID: null,
        ContactType: "Fax",
        PreferredWrittenContactType: 2,
        StatusType: 2,
        PreferredIndex: 2
    }, {
        PreferredWrittenContactID: null,
        ContactType: "HomeAddress",
        PreferredWrittenContactType: 3,
        StatusType: 2,
        PreferredIndex: 3
    }];
    $scope.PreferredContacts = [{
        PreferredContactID: null,
        ContactType: "Email",
        PreferredWrittenContactType: 1,
        StatusType: 2,
        PreferredIndex: 1
    }, {
        PreferredContactID: null,
        ContactType: "Fax",
        PreferredWrittenContactType: 2,
        StatusType: 2,
        PreferredIndex: 2
    }, {
        PreferredContactID: null,
        ContactType: "Mobile",
        PreferredWrittenContactType: 3,
        StatusType: 2,
        PreferredIndex: 3
    }];

    //-------------------------------------- File Upload Action ---------------------------------------------
    $scope.ProfileUpload = function (Form_Id) {

        if ($("#" + Form_Id).valid()) {

            var file = $("#ProfilePic")[0];

            $.ajax({
                url: '/Profile/Demographic/FileUploadAsync?profileId=' + profileId,
                type: 'POST',
                data: new FormData(file),
                async: false,
                cache: false,
                contentType: false,
                processData: false,
                success: function (data) {
                    //console.log(data);
                    if (data.status == "true") {
                        $scope.Provider.ProfilePhotoPath = data.ProfileImagePath;
                        $rootScope.visibilityControl = "";
                        //console.log($scope.Provider.ProfileImagePath);
                    } else {
                        alert(data.status);
                    }
                }
            });
        }
    };
    //----------------------------------- Update Personal Details Function --------------------------------------
    $scope.DemographicsUpdatePersonalDetails = function (Form_Id) {
        if ($("#" + Form_Id).valid()) {
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
                        $scope.Provider.PersonalDetails = data.personalDetail;
                        $scope.IsPersonalDetailsHasError = false;
                        $rootScope.tempObject = {};
                        $rootScope.visibilityControl = "";
                        FormReset($("#" + Form_Id));
                    } else {
                        $scope.IsPersonalDetailsHasError = true;
                        $scope.PersonalDetailsErrorList = data.status.split(",");
                    }
                }
            });
        } else {
            //console.log("Sorry Some thing Wrong with you dear!!!!!!!!!!!");
        }
    };

    //----------------------------------- Add Personal Details Function --------------------------------------
    $scope.saveOtherLegalName = function (Form_Div_Id) {
        var $form = $("#" + Form_Div_Id).find("form");
        //console.log($form[0]);
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
                        $scope.IsOtherLegalNameHasError = false;
                        $rootScope.tempObject = {};
                        $rootScope.visibilityControl = "";
                        FormReset($form);
                    } else {
                        $scope.IsOtherLegalNameHasError = true;
                        $scope.OtherLegalNameErrorList = data.status.split(",");
                    }
                }
            });
        } else {
            //alert("Sorry Some Thing Wrong With You !!!!!!!!");
        }
    };
    //--------------------------- Update Other Legal Name Function ------------------------------
    $scope.updateOtherLegalName = function (Form_Div_Id, index) {
        var $form = $("#" + Form_Div_Id).find("form");
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
                        $scope.IsOtherLegalNameHasError = false;
                        $rootScope.tempObject = {};
                        $rootScope.visibilityControl = "";
                        FormReset($form);
                    } else {
                        $scope.IsOtherLegalNameHasError = true;
                        $scope.OtherLegalNameErrorList = data.status.split(",");
                    }
                }
            });
        } else {
            //console.log("Sorry Some thing Wrong with you dear!!!!!!!!!!!");
        }
    };

    //--------------------------- Save Home Address Function ------------------------------
    $scope.saveHomeAddress = function (Form_Div_Id) {
        //================== Save Here ============
        var $form = $("#" + Form_Div_Id).find("form");
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
                        data.homeAddress.LivingEndDate = ConvertDateFormat(data.homeAddress.LivingEndDate);
                        data.homeAddress.LivingFromDate = ConvertDateFormat(data.homeAddress.LivingFromDate);
                        $scope.HomeAddresses.push(data.homeAddress);
                        $scope.IsHomeAddressHasError = false;
                        $rootScope.tempObject = {};
                        $rootScope.visibilityControl = "";
                        FormReset($form);
                    } else {
                        $scope.IsHomeAddressHasError = true;
                        $scope.HomeAddressErrorList = data.status.split(",");
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
                        data.homeAddress.LivingEndDate = ConvertDateFormat(data.homeAddress.LivingEndDate);
                        data.homeAddress.LivingFromDate = ConvertDateFormat(data.homeAddress.LivingFromDate);
                        $scope.HomeAddresses[index] = data.homeAddress;
                        $scope.IsHomeAddressHasError = false;
                        $rootScope.tempObject = {};
                        $rootScope.visibilityControl = "";
                        FormReset($form);
                    } else {
                        $scope.IsHomeAddressHasError = true;
                        $scope.HomeAddressErrorList = data.status.split(",");
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
                PhoneDetails: [{
                    PhoneDetailID: null,
                    Number: "",
                    CountryCode: "+1",
                    PhoneTypeEnum: 1,
                    PreferenceType: 1,
                    StatusType: 1
                }, {
                    PhoneDetailID: null,
                    Number: "",
                    CountryCode: "+1",
                    PhoneTypeEnum: 2,
                    PreferenceType: 1,
                    StatusType: 1
                }, {
                    PhoneDetailID: null,
                    Number: "",
                    CountryCode: "+1",
                    PhoneTypeEnum: 3,
                    PreferenceType: 1,
                    StatusType: 1
                }],
                EmailIDs: [{
                    EmailDetailID: 1,
                    EmailAddress: "",
                    PreferenceType: 1,
                    StatusType: 1
                }],
                PreferredWrittenContacts: $scope.TempPreferredWrittenContacts,
                PreferredContacts: $scope.TempPreferredContacts
            };
        }
    };

    //========================== Personal Identification Save Method ======================
    $scope.IsPersonalIdentificationHasError = false;
    $scope.DemographicsPersonalIdentificationSave = function (Form_Id) {
        //console.log($("#" + Form_Id)[0]);
        //console.log(new FormData($("#" + Form_Id)[0]));
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
                        FormReset($("#" + Form_Id));
                        $scope.IsPersonalIdentificationHasError = false;
                    } else {
                        $scope.IsPersonalIdentificationHasError = true;
                        $scope.PersonalIdentificationErrorList = data.status.split(",");
                    }
                }
            });
        } else {
            //console.log("Sorry Some thing Wrong with you !!!!!!!!!!!");
        }
    };
    //--------------------------- end ------------------------------
    //-------------------------------- Save Birth Information Function --------------------------------
    $scope.IsBirthInformationHasError = false;
    $scope.saveBirthInformation = function (Form_Id) {
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
                        $scope.IsBirthInformationHasError = false;
                        $rootScope.tempObject = {};
                        $rootScope.visibilityControl = "";
                        FormReset($("#" + Form_Id));
                    } else {
                        $scope.IsBirthInformationHasError = true;
                        $scope.BirthInformationErrorList = data.status.split(",");
                    }
                }
            });
        } else {
            //console.log("Sorry Some thing Wrong with you !!!!!!!!!!!");
        }
    };
    //--------------------------- Save Visa Details Function ------------------------------------
    $scope.IsVisaDetailsHasError = false;
    $scope.saveEthnicityVisaDetails = function (Form_Id) {
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
                        $scope.IsVisaDetailsHasError = false;
                        $rootScope.tempObject = {};
                        $rootScope.visibilityControl = "";
                        FormReset($("#" + Form_Id));
                    } else {
                        $scope.IsVisaDetailsHasError = true;
                        $scope.VisaDetailsErrorList = data.status.split(",");
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
        if (obj.length == 0) {
            obj.push({
                PhoneDetailID: null,
                Number: "",
                CountryCode: "+1",
                PhoneTypeEnum: condition,
                Preference: "Primary",
                PreferenceType: 1,
                Status: "Active",
                StatusType: 1
            });
        } else {
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
        }

        $('[data-toggle="tooltip"]').tooltip();
    };

    //---------------------------- Add Email Ids Function ------------------------------------
    $scope.AddEmail = function (obj) {
        if (obj.length == 0) {
            obj.push({
                EmailDetailID: null,
                EmailAddress: "",
                Preference: "Secondary",
                PreferenceType: 1,
                Status: "Active",
                StatusType: 1
            });
        } else {
            obj.push({
                EmailDetailID: null,
                EmailAddress: "",
                Preference: "Secondary",
                PreferenceType: 2,
                Status: "Active",
                StatusType: 1
            });
        }

        $('[data-toggle="tooltip"]').tooltip();
    };
    //---------------- Remove Method for Contact Details -----------------------
    $scope.RemoveContactDetails = function (index, data) {
        data.splice(index, 1);
        if (data.length == 1) {
            data[0].Preference = "Primary";
            data[0].PreferenceType = 1;
        }
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
            $scope.ConfirmationMessage = "Are you sure, want to remove Number  " + arrayObj[index].CountryCode + "-" + arrayObj[index].Number + " from DataBase";
        } else {
            $scope.ConfirmationMessage = "Are you sure, want to remove EmailId  " + arrayObj[index].EmailAddress + " from DataBase";
        }
        $('#ConfirmationContactDetails').modal('show');
    };
    //-------------------- Modal Confirmation Hide and Remove from Array ------------------------
    $scope.InactiveContactDetails = function () {
        $scope.selectedArrayObject[$scope.selectedObjectIndex].StatusType = 2;
        if ($scope.selectedArrayObject.length == 1) {
            $scope.selectedArrayObject[0].Preference = "Primary";
            $scope.selectedArrayObject[0].PreferenceType = 1;
        }
        $('#ConfirmationContactDetails').modal('hide');
    };
    //--------------------------------- Save Contact Details Function -----------------------------------
    $scope.IsContactDetailsError = false;
    $scope.ContactDetailsErrorList = [];
    $scope.saveContactDetails = function (Form_Id) {
        if ($("#" + Form_Id).valid()) {
            $.ajax({
                url: '/Profile/Demographic/UpdateContactDetailsAsync?profileId=' + profileId,
                type: 'POST',
                data: new FormData($("#" + Form_Id)[0]),
                async: false,
                success: function (data) {
                    //console.log(data);
                    if (data.status == "true") {
                        $scope.Provider.ContactDetails = data.contactDetail;

                        $scope.TempContactDetailsForEdit = [];
                        $scope.ViewContactDetails = true;
                        $scope.IsContactDetailsError = false;
                        $scope.ContactDetailsErrorList = [];
                    } else {
                        $scope.IsContactDetailsError = true;
                        $scope.ContactDetailsErrorList = data.status.split(",");
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

    $scope.PriorityChange = function (condition, index) {
        if (condition == "increase") {
            $scope.TempLanguageForEdit.KnownLanguages[index].ProficiencyIndex = index;
            $scope.TempLanguageForEdit.KnownLanguages[index - 1].ProficiencyIndex = index + 1;
        } else if (condition == "decrease") {
            $scope.TempLanguageForEdit.KnownLanguages[index].ProficiencyIndex = index + 2;
            $scope.TempLanguageForEdit.KnownLanguages[index + 1].ProficiencyIndex = index + 1;
        }
        $scope.TempLanguageForEdit.KnownLanguages.sort(function (a, b) {
            return a.ProficiencyIndex - b.ProficiencyIndex;
        });
    };
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
    };

    $scope.IsLanguageInfoHasError = false;
    $scope.saveLanguages = function (Form_Id) {
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
                        $scope.ViewLanguageKnown = true;
                        $scope.IsLanguageInfoHasError = false;

                    } else {
                        $scope.IsLanguageInfoHasError = true;
                        $scope.LanguageInfoErrorList = data.status.split(",");
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
    if (!$(event.target).hasClass("btn") && $(event.target).parents(".countryDailCodeContainer").length === 0) {
        $(".countryDailCodeContainer").hide();
    }
    if (!$(event.target).hasClass("form-control") && $(event.target).parents(".LanguageSelectAutoList").length === 0) {
        $(".LanguageSelectAutoList").hide();
    }
});

$(document).ready(function () {
    $(".countryDailCodeContainer").hide();
});