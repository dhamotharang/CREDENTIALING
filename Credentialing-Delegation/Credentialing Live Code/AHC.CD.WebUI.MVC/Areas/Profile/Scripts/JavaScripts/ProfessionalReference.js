
//=========================== Controller declaration ==========================
profileApp.controller('ProfessionalReference', ['$scope', '$rootScope', '$http', 'masterDataService', 'locationService', 'messageAlertEngine', '$filter', 'profileUpdates', function ($scope, $rootScope, $http, masterDataService, locationService, messageAlertEngine, $filter, profileUpdates) {

    //Get all the data for the Professional Reference on document on ready
    //$(document).ready(function () {
    //    $("#Loading_Message").text("Gathering Profile Data...");
    //    $http({
    //        method: 'GET',
    //        url: '/Profile/MasterProfile/GetProfessionalReferencesProfileDataAsync?profileId=' + profileId
    //    }).success(function (data, status, headers, config) {
    //        try {
    //            for (key in data) {
    //                $rootScope.$emit(key, data[key]);
    //                //call respective controller to load data (PSP)
    //            }
    //            $rootScope.ProfessionalReferenceLoaded = true;
    //            //$rootScope.$broadcast("LoadRequireMasterData");
    //        } catch (e) {
    //            $rootScope.ProfessionalReferenceLoaded = true;
    //        }

    //    }).error(function (data, status, headers, config) {
    //        $rootScope.ProfessionalReferenceLoaded = true;
    //    });
    //});

    $scope.ProfessionalReferences = [];

    //calling the method using $on(PSP-public subscriber pattern)
    $rootScope.$on('ProfessionalReferenceInfos', function (event, val) {
        $scope.ProfessionalReferencePendingRequest = profileUpdates.getUpdates('Professional Reference', 'Professional Reference Info');


        $scope.ProfessionalReferences = val;
        for (var i = 0; i < $scope.ProfessionalReferences.length ; i++) {
            if (!$scope.ProfessionalReferences[i].ProviderTypeID) { $scope.ProfessionalReferences[i].ProviderTypeID = ""; }
            if (!$scope.ProfessionalReferences[i].SpecialtyID) { $scope.ProfessionalReferences[i].SpecialtyID = ""; }
            if (!$scope.ProfessionalReferences[i].Degree) { $scope.ProfessionalReferences[i].Degree = ""; }
        }

    });

    $scope.setFiles = function (file) {
        $(file).parent().parent().find(".jancyFileWrapTexts").find("span").width($(file).parent().parent().width() < 243 ? $(file).parent().parent().width() : 243);

    }

    //------------------------------------------------------------------Address Auto-Complete---------------------------------------------------------------------------//

    /*
     Method addressAutocomplete() gets the details of a location
         Method takes input of location details entered in the text box.
  */

    $scope.addressAutocomplete = function (location) {
        if (location.length == 0) {
            $scope.resetAddressModels();
        }
        $scope.tempObject.CityOfBirth = location;
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
        $scope.tempObject.City = "";
        $scope.tempObject.State = "";
        $scope.tempObject.Country = "";
    };

    $scope.setAddressModels = function (location) {
        $scope.tempObject.City = location.City;
        $scope.tempObject.State = location.State;
        $scope.tempObject.Country = location.Country;

    }
    $scope.CountryDialCodes = countryDailCodes;
    //---------------------------------------------------------------------------------------------------------------------------------------------------------
    var providerType;

    $scope.removeDegree = function (val) {
        if (val = "? undefined:undefined ?" || val == "? object:null ?") {
            return 'Not Available';
        }
        else {
            $scope.tempObject.Degree = val;
            return val;
        }
    }

    $scope.SelectProviderTitle = function (titleType) {

        $scope.tempObject.ProviderTypeID = titleType.ProviderTypeID;

    }

    $scope.shoLanguageList = function (divId) {
        $("#" + divId).show();
    };

    $scope.submitButtonText = "Add";

    $scope.changeButtonText = function () {
        if ($scope.submitButtonText == "Update")
            $scope.submitButtonText = "Add";
        else
            $scope.submitButtonText = "Update";
    }

    //------------------------------- Country Code Popover Show by Id ---------------------------------------
    $scope.showContryCodeDiv = function (countryCodeDivId) {
        changeVisibilityOfCountryCode();
        $("#" + countryCodeDivId).show();
    };
    ////******************************Master Data*****************************
    $scope.masterSpecialties = [];
    $scope.masterDegrees = [];
    $scope.masterProviderTypes = [];

    $rootScope.$on("LoadRequireMasterDataProfessionalReference", function () {
        masterDataService.getMasterData(rootDir + "/Profile/MasterData/getAllSpecialities").then(function (masterSpecialties) {
            $scope.masterSpecialties = masterSpecialties;
        });

        masterDataService.getMasterData(rootDir + "/Profile/MasterData/GetAllProviderTypes").then(function (masterProviderTypes) {
            $scope.masterProviderTypes = masterProviderTypes;
        });

        masterDataService.getMasterData(rootDir + "/Profile/MasterData/GetAllQualificationDegrees").then(function (masterDegrees) {
            $scope.masterDegrees = masterDegrees;
        });
    });


    $scope.saveProfessionalReference = function (professionalReference, index) {
        loadingOn();
        var validationStatus;
        var url;
        var formData1;
        var providerTypeId;

        var tempSpecialtyID;
        var tempSpecialty;

        tempSpecialtyID = professionalReference.SpecialtyID;
        for (var spl in $scope.masterSpecialties) {
            if ($scope.masterSpecialties[spl].SpecialtyID == tempSpecialtyID) {
                tempSpecialty = $scope.masterSpecialties[spl];
                break;
            }
        }

        providerTypeId = professionalReference.ProviderTypeID;

        if (providerTypeId) {
            for (var provider in $scope.masterProviderTypes) {
                if ($scope.masterProviderTypes[provider].ProviderTypeID == providerTypeId) {
                    providerType = $scope.masterProviderTypes[provider];
                    break;
                }
            }
        }
        else {
            providerType = null;
        }


        if ($scope.visibilityControl == 'addpr') {

            formData1 = $('#newProfessionalReferenceFormDiv').find('form');
            url = rootDir + "/Profile/ProfessionalReference/AddProfessionalReference?profileId=" + profileId;
        }
        else if ($scope.visibilityControl == (index + '_editpr')) {
            //Update Details - Denote the URL

            formData1 = $('#professionalReferenceEditDiv' + index).find('form');
            url = rootDir + "/Profile/ProfessionalReference/UpdateProfessionalReference?profileId=" + profileId;
        }

        ResetFormForValidation(formData1);
        validationStatus = formData1.valid();

        if (validationStatus) {
            // Simple POST request example (passing data) :
            $.ajax({
                url: url,
                type: 'POST',
                data: new FormData(formData1[0]),
                async: false,
                cache: false,
                contentType: false,
                processData: false,
                success: function (data) {

                    try {
                        if (data.status == "true") {
                            if (UserRole == "PRO" && data.ActionType == "Update") {
                                data.professionalReference.TableState = true;
                            }

                            data.professionalReference.Specialty = tempSpecialty;
                            data.professionalReference.ProviderType = providerType;

                            if ($scope.visibilityControl != (index + '_editpr')) {
                                $scope.ProfessionalReferences.push(data.professionalReference);
                                for (var i = 0; i < $scope.ProfessionalReferences.length ; i++) {
                                    if (!$scope.ProfessionalReferences[i].ProviderTypeID) { $scope.ProfessionalReferences[i].ProviderTypeID = ""; }
                                    if (!$scope.ProfessionalReferences[i].SpecialtyID) { $scope.ProfessionalReferences[i].SpecialtyID = ""; }
                                    if (!$scope.ProfessionalReferences[i].Degree) { $scope.ProfessionalReferences[i].Degree = ""; }

                                }
                                $rootScope.operateCancelControl('');
                                messageAlertEngine.callAlertMessage("addedNewProfessionalReference", "Professional Reference saved successfully !!!!", "success", true);
                            }
                            else {


                                $scope.ProfessionalReferences[index] = data.professionalReference;
                                for (var i = 0; i < $scope.ProfessionalReferences.length ; i++) {
                                    if (!$scope.ProfessionalReferences[i].ProviderTypeID) { $scope.ProfessionalReferences[i].ProviderTypeID = ""; }
                                    if (!$scope.ProfessionalReferences[i].SpecialtyID) { $scope.ProfessionalReferences[i].SpecialtyID = ""; }
                                    if (!$scope.ProfessionalReferences[i].Degree) { $scope.ProfessionalReferences[i].Degree = ""; }

                                }
                                $rootScope.operateViewAndAddControl(index + '_viewpr');
                                $scope.ProfessionalReferencePendingRequest = true;
                                //messageAlertEngine.callAlertMessage("updatedProfessionalReference" + index, "Professional Reference updated successfully !!!!", "success", true);
                                messageAlertEngine.callAlertMessage("updatedProfessionalReference" + index, data.successMessage, "success", true);
                            }

                            $scope.IsProfessionalReferenceHasError = false;
                            FormReset(formData1);

                        } else {
                            messageAlertEngine.callAlertMessage('errorProfessionalReference' + index, "", "danger", true);
                            $scope.errorProfessionalReference = data.status.split(",");
                        }
                    } catch (e) {

                    }
                },
                error: function (e) {
                    messageAlertEngine.callAlertMessage('errorProfessionalReference' + index, "", "danger", true);
                    $scope.errorProfessionalReference = "Sorry for Inconvenience !!!! Please Try Again Later...";
                }
            });
        }
        loadingOff();
    };

    function ResetProfessionalReferenceForm() {
        $('#newShowProfessionalReferenceDiv').find('.professionalReferenceForm')[0].reset();
        $('#newShowProfessionalReferenceDiv').find('span').html('');
    }

    $scope.initProfessionalReferenceWarning = function (reference) {
        if (angular.isObject(reference)) {
            $scope.tempReference = reference;
        }
        $('#professionalReferenceWarningModal').modal();
    };


    //$$$$$$$$$$$$$$$$$$$$$$ Professional Reference History#########################//

    $scope.professionalReferenceHistoryArray = [];
    $scope.dataFetchedPR = false;
    var historyUrl = rootDir + "/Profile/ProfileHistory/GetAllProfessionalReferenceHistory?profileId=" + profileId;

    $scope.showProfessionalReferenceHistory = function (loadingId) {
        if ($scope.professionalReferenceHistoryArray.length == 0) {
            $("#" + loadingId).css('display', 'block');
            var historyUrl = rootDir + "/Profile/ProfileHistory/GetAllProfessionalReferenceHistory?profileId=" + profileId;
            $http.get(historyUrl).success(function (data) {
                try {
                    $scope.professionalReferenceHistoryArray = data;
                    $rootScope.GetAllUserData();
                    for (var j = 0; j < $scope.professionalReferenceHistoryArray.length; j++) {
                        for (var i = 0; i < $rootScope.userslist.length; i++) {
                            if ($scope.professionalReferenceHistoryArray[j].DeletedById != null) {
                                if ($rootScope.userslist[i].CDUserID == $scope.professionalReferenceHistoryArray[j].DeletedById) {
                                    if ($rootScope.userslist[i].FullName != null) {
                                        $scope.professionalReferenceHistoryArray[j].DeletedBy = $rootScope.userslist[i].FullName;
                                        break;
                                    }
                                    else {
                                        $scope.professionalReferenceHistoryArray[j].DeletedBy = $rootScope.userslist[i].UserName;
                                        break;
                                    }
                                }
                            }
                        }
                        if ($scope.professionalReferenceHistoryArray[j].DeletedDate != null) {
                            var date = moment.utc($scope.professionalReferenceHistoryArray[j].DeletedDate).toDate();
                            $scope.professionalReferenceHistoryArray[j].DeletedDate = moment(date).format('MM-DD-YYYY, h:mm:ss a');
                        }
                    }
                    $scope.showProfessionalReferenceHistoryTable = true;
                    $scope.dataFetchedPR = true;
                    $("#" + loadingId).css('display', 'none');
                } catch (e) {

                }
            });
        } else {
            var historyUrl = rootDir + "/Profile/ProfileHistory/GetAllProfessionalReferenceHistory?profileId=" + profileId;
            $http.get(historyUrl).success(function (data) {
                try {
                    $scope.professionalReferenceHistoryArray = data;
                    $rootScope.GetAllUserData();
                    for (var j = 0; j < $scope.professionalReferenceHistoryArray.length; j++) {
                        for (var i = 0; i < $rootScope.userslist.length; i++) {
                            if ($scope.professionalReferenceHistoryArray[j].DeletedById != null) {
                                if ($rootScope.userslist[i].CDUserID == $scope.professionalReferenceHistoryArray[j].DeletedById) {
                                    if ($rootScope.userslist[i].FullName != null) {
                                        $scope.professionalReferenceHistoryArray[j].DeletedBy = $rootScope.userslist[i].FullName;
                                        break;
                                    }
                                    else {
                                        $scope.professionalReferenceHistoryArray[j].DeletedBy = $rootScope.userslist[i].UserName;
                                        break;
                                    }
                                }
                            }
                        }
                        // var date = moment.utc().toDate($scope.professionalReferenceHistoryArray[j].DeletedDate);
                        if ($scope.professionalReferenceHistoryArray[j].DeletedDate != null) {
                            var date = moment.utc($scope.professionalReferenceHistoryArray[j].DeletedDate).toDate();
                            $scope.professionalReferenceHistoryArray[j].DeletedDate = moment(date).format('MM-DD-YYYY, h:mm:ss a');
                        }
                    }
                    $scope.showProfessionalReferenceHistoryTable = true;
                } catch (e) {

                }
            });
            //  $scope.showProfessionalReferenceHistoryTable = true;
        }

    }

    $scope.cancelProfessionalReferenceHistory = function () {
        $scope.showProfessionalReferenceHistoryTable = false;
    }

    //$$$$$$$$$$$$$$$$$$$$$$ END #########################//

    $scope.removeProfessionalReference = function (ProfessionalReferences) {
        var validationStatus = false;
        var url = null;
        var $formData = null;
        $scope.isRemoved = true;
        $formData = $('#editProfessionalReference');
        url = rootDir + "/Profile/ProfessionalReference/RemoveProfessionalReference?profileId=" + profileId;
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
                            var obj = $filter('filter')(ProfessionalReferences, { ProfessionalReferenceInfoID: data.professionalReference.ProfessionalReferenceInfoID })[0];
                            ProfessionalReferences.splice(ProfessionalReferences.indexOf(obj), 1);
                            $scope.isRemoved = false;
                            $('#professionalReferenceWarningModal').modal('hide');
                            if ($scope.dataFetchedPR == true) {
                                obj.HistoryStatus = 'Deleted';
                                obj.DeletedBy = data.UserName;
                                obj.DeletedDate = moment(new Date).format('MM/DD/YYYY, h:mm:ss a');
                                $scope.professionalReferenceHistoryArray.push(obj);
                            }
                            $rootScope.operateCancelControl('');
                            messageAlertEngine.callAlertMessage("addedNewProfessionalReference", "Professional Reference Removed successfully.", "success", true);
                            //$scope.professionalReferenceHistoryArray.push(obj);

                        } else {
                            $('#professionalReferenceWarningModal').modal('hide');
                            messageAlertEngine.callAlertMessage("removeProfessionalReference", data.status, "danger", true);
                            $scope.errorProfessionalReference = "Sorry for Inconvenience !!!! Please Try Again Later...";
                        }
                    } catch (e) {

                    }
                },
                error: function (e) {

                }
            });
        }
    };
    $rootScope.ProfessionalReferenceLoaded = true;
    $scope.dataLoaded = false;
    $rootScope.$on('ProfessionalReference', function () {
        if (!$scope.dataLoaded) {
            $rootScope.ProfessionalReferenceLoaded = false;
            $http({
                method: 'GET',
                url: rootDir + '/Profile/MasterProfile/GetProfessionalReferencesProfileDataAsync?profileId=' + profileId
            }).success(function (data, status, headers, config) {
                try {
                    for (key in data) {
                        $rootScope.$emit(key, data[key]);
                        //call respective controller to load data (PSP)
                    }
                    $rootScope.ProfessionalReferenceLoaded = true;
                    $rootScope.$broadcast("LoadRequireMasterDataProfessionalReference");
                } catch (e) {
                    $rootScope.ProfessionalReferenceLoaded = true;
                }

            }).error(function (data, status, headers, config) {
                $rootScope.ProfessionalReferenceLoaded = true;
            });
            $scope.dataLoaded = true;
        }
    });

    $scope.Providers = [];
    $rootScope.$on("LoadRequireMasterDataDemographics", function () {

        $(function () {
            try {
                $http.get("/TaskTracker/GetAllProviders")
           .then(function (response) {
               $scope.Providers = response.data;
           });
            }
            catch (e) { };
        });


    });

    $scope.showProvidersList = function (divId) {
        $("#" + divId).show();
    };

    $scope.ProviderData = "";
    $scope.SelectProvider = function (ProfileId) {

        try {
            $http.get("/Profile/ProfessionalReference/GetProfileData?profileId=" + ProfileId)
       .then(function (response) {
           $scope.ProviderData = response.data;

           $scope.tempObject.LastName = $scope.ProviderData.providers.PersonalDetail.LastName;
           $scope.tempObject.MiddleName = $scope.ProviderData.providers.PersonalDetail.MiddleName;
           $scope.tempObject.Street = $scope.ProviderData.providers.HomeAddresses[0].Street;
           $scope.tempObject.Building = $scope.ProviderData.providers.HomeAddresses[0].UnitNumber;
           $scope.tempObject.City = $scope.ProviderData.providers.HomeAddresses[0].City;
           $scope.tempObject.State = $scope.ProviderData.providers.HomeAddresses[0].State;
           $scope.tempObject.Zipcode = $scope.ProviderData.providers.HomeAddresses[0].ZipCode;
           $scope.tempObject.Country = $scope.ProviderData.providers.HomeAddresses[0].Country;
           $scope.tempObject.County = $scope.ProviderData.providers.HomeAddresses[0].County;
           $scope.tempObject.Email = $scope.ProviderData.providers.ContactDetail.EmailIDs[0].EmailAddress;
           $scope.tempObject.Telephone = $scope.ProviderData.providers.ContactDetail.PhoneDetails.Telephone;
           if ($scope.ProviderData.providers.SpecialtyDetails.length != 0) {
               $scope.tempObject.speciality = $scope.ProviderData.providers.SpecialtyDetails[0];
               $scope.tempObject.speciality.SpecialtyID = $scope.ProviderData.providers.SpecialtyDetails[0].SpecialtyID;
               $scope.tempObject.speciality.Specialty.Name = $scope.masterSpecialties[0];
               $scope.tempObject.speciality.Name = $scope.masterSpecialties[0];
           }
           for (var number in $scope.ProviderData.providers.ContactDetail.PhoneDetails) {
               if (($scope.ProviderData.providers.ContactDetail.PhoneDetails[number].Preference == "Primary")) {
                   if ($scope.ProviderData.providers.ContactDetail.PhoneDetails[number].PhoneType == "Mobile") {
                       $scope.tempObject.Telephone = (($scope.ProviderData.providers.ContactDetail.PhoneDetails[number].PhoneNumber).split('-'))[1];
                   }
               }
               else if ($scope.ProviderData.providers.ContactDetail.PhoneDetails[number].PhoneType == "Mobile") {
                   $scope.tempObject.Telephone = (($scope.ProviderData.providers.ContactDetail.PhoneDetails[number].PhoneNumber).split('-'))[1];
                   //break;
               }

               if (($scope.ProviderData.providers.ContactDetail.PhoneDetails[number].Preference == "Primary")) {
                   if ($scope.ProviderData.providers.ContactDetail.PhoneDetails[number].PhoneType == "Fax") {
                       $scope.tempObject.Fax = (($scope.ProviderData.providers.ContactDetail.PhoneDetails[number].PhoneNumber).split('-'))[1];
                   }
               }
               else if ($scope.ProviderData.providers.ContactDetail.PhoneDetails[number].PhoneType == "Fax") {
                   $scope.tempObject.Fax = (($scope.ProviderData.providers.ContactDetail.PhoneDetails[number].PhoneNumber).split('-'))[1];
                   //break;
               }
           }
           $scope.tempObject.FirstName = $scope.ProviderData.providers.PersonalDetail.FirstName;
           $("#ProvidersSearchResultDiv").hide();
       });
        }
        catch (e) { };

    }
}]);