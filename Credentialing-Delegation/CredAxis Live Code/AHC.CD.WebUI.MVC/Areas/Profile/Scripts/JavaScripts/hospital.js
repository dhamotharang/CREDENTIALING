
profileApp.controller('hospitalcltr', ['$scope', '$rootScope', '$http', 'messageAlertEngine', '$filter', 'profileUpdates', function ($scope, $rootScope, $http, messageAlertEngine, $filter, profileUpdates) {

    //-------------------------- MAster Data Get Function --------------------------------
    $rootScope.$on("LoadRequireMasterDataHospitalPrivilege", function () {
        $http.get(rootDir + "/Profile/MasterData/GetAllStaffCategories").then(function (value) { $scope.staffCategories = value.data; });
        $http.get(rootDir + "/Profile/MasterData/GetAllAdmittingPrivileges").then(function (value) { $scope.admittingPrivileges = value.data; });
        $http.get(rootDir + "/Profile/MasterData/GetAllHospitals").then(function (value) { $scope.hospitals = value.data; });
    });

    $scope.HaveHospitalPrivilege = "True";

    $scope.HospitalPrivilegeInformations = [];

    //to show renew div
    $scope.ShowRenewDiv = false;

    $scope.RenewDiv = function (hospitalPrivilegeInformation) {
        if (hospitalPrivilegeInformation.AffiliationEndDate == null)
        { $scope.ShowRenewDiv = false; }
        else
        {
            $scope.ShowRenewDiv = true;
        }
    };

    $scope.setFiles = function (file) {
        $(file).parent().parent().find(".jancyFileWrapTexts").find("span").width($(file).parent().parent().width() < 243 ? $(file).parent().parent().width() : 243);

    }
    // rootScoped on emitted value catches the value for the model and insert to get the old data
    $rootScope.$on('HospitalPrivilegeInformation', function (event, val) {

        $scope.HospitalInformationPendingRequest = profileUpdates.getUpdates('Hospital Privilege', 'Hospital Privilege Information');
        $scope.HospitalPrivilegeInformationPendingRequest = profileUpdates.getUpdates('Hospital Privilege', 'Hospital Privilege Detail');


        try {
            $scope.HospitalPrivilegeInformations = val;
            var tempHospitalPrivilegeDetails = [];
            for (var i = 0; i < $scope.HospitalPrivilegeInformations.HospitalPrivilegeDetails.length; i++) {
                if ($scope.HospitalPrivilegeInformations.HospitalPrivilegeDetails[i].Status != 'Inactive') {
                    tempHospitalPrivilegeDetails.push($scope.HospitalPrivilegeInformations.HospitalPrivilegeDetails[i]);
                }
            }
            $scope.HospitalPrivilegeInformations.HospitalPrivilegeDetails = tempHospitalPrivilegeDetails;
            if ($scope.HospitalPrivilegeInformations.HospitalPrivilegeDetails.length > 0) {
                for (var i = 0; i < $scope.HospitalPrivilegeInformations.HospitalPrivilegeDetails.length ; i++) {
                    if (!$scope.HospitalPrivilegeInformations.HospitalPrivilegeDetails[i].SpecialtyID) { $scope.HospitalPrivilegeInformations.HospitalPrivilegeDetails[i].SpecialtyID = ""; }
                    // $scope.HospitalPrivilegeInformations.HospitalPrivilegeDetails[i].TableState = $scope.HospitalPrivilegeInformations.TableState;
                }
            }

        }
        catch (e) {

        };
    });

    //---------------------------- author : krglv ---------------------
    //--------------------- watch for data change ---------------------------
    $scope.clearHospitalLevel = function () {
        $scope.tempObject.Hospital.HospitalContactInfoes = [];
        $scope.tempObject.HospitalContactInfo = {};
        $scope.tempObject.HospitalContactPerson = {};
        $scope.tempHospitalLocationName = "";
    };

    $scope.clearLocationLevel = function () {
        $scope.tempObject.HospitalContactInfo.City = null;
        $scope.tempObject.HospitalContactInfo.Country = null;
        $scope.tempObject.HospitalContactInfo.County = null;
        $scope.tempObject.HospitalContactInfo.Email = null;
        $scope.tempObject.HospitalContactInfo.Fax = null;
        $scope.tempObject.HospitalContactInfo.FaxCountryCode = null;
        $scope.tempObject.HospitalContactInfo.FaxNumber = null;
        $scope.tempObject.HospitalContactInfo.HospitalContactInfoID = null;
        $scope.tempObject.HospitalContactInfo.HospitalContactPersons = [];
        $scope.tempObject.HospitalContactInfo.LastModifiedDate = null;
        //$scope.tempObject.HospitalContactInfo.LocationName = null;
        $scope.tempObject.HospitalContactInfo.Phone = null;
        $scope.tempObject.HospitalContactInfo.PhoneCountryCode = null;
        $scope.tempObject.HospitalContactInfo.PhoneNumber = null;
        $scope.tempObject.HospitalContactInfo.State = null;
        $scope.tempObject.HospitalContactInfo.Status = null;
        $scope.tempObject.HospitalContactInfo.StatusType = null;
        $scope.tempObject.HospitalContactInfo.Street = null;
        $scope.tempObject.HospitalContactInfo.UnitNumber = null;
        $scope.tempObject.HospitalContactInfo.ZipCode = null;

        $scope.tempObject.HospitalContactPerson = {};
    };

    //To Display the drop down div
    $scope.searchCumDropDown = function (divId) {
        $("#" + divId).show();
        if (divId.includes("ForContact")) {
            $scope.tempObject.HospitalContactInfo = {};
            $scope.tempObject.HospitalContactInfo.HospitalContactPersons = [];
            $scope.tempObject.HospitalContactInfo.HospitalContactPersons = $scope.tempObject.Hospital.HospitalContactInfoes[0].HospitalContactPersons;
        }
        //alert($scope.tempObject.HospitalContactPerson.ContactPersonName);
        if ($scope.tempObject.HospitalContactPerson != null && $scope.tempObject.HospitalContactPerson != undefined) {
            if ($scope.tempObject.HospitalContactPerson.ContactPersonName == '') {
                $scope.tempObject.HospitalContactPerson.HospitalContactPersonID = null;
                $scope.tempObject.HospitalContactPerson.ContactPersonPhone = '';
                $scope.tempObject.HospitalContactPerson.ContactPersonFax = '';
            }
        }
    };

    $scope.addIntoHospitalDropDown = function (hospital, div) {
        $scope.tempObject.Hospital = angular.copy(hospital);
        $("#" + div).hide();
    }

    $scope.addIntoLocationDropDown = function (location, div) {
        $scope.tempObject.HospitalContactInfo = angular.copy(location);
        $("#" + div).hide();
    }

    $scope.addIntoContactDropDown = function (contact, div) {
        $scope.tempObject.HospitalContactPerson = angular.copy(contact);
        $("#" + div).hide();

    }

    $scope.specialties = $rootScope.getSpecilityForThisUser();

    $scope.getSpecialityForProvider = function (findData) {
        try {
            return $rootScope.getSpecilityForThisUser().filter(function (specialties) { return specialties.Specialty.SpecialtyID == findData })[0];
        }
        catch (e) { }
    }

    $scope.getHospitalNameForProvider = function (findData) {
        try {
            return $scope.hospitals.filter(function (hospitals) { return hospitals.HospitalID == findData })[0];
        }
        catch (e) { }
    }

    $scope.getStaffCategoryForProvider = function (findData) {
        try {
            return $scope.staffCategories.filter(function (staffCategories) { return staffCategories.StaffCategoryID == findData })[0];
        }
        catch (e) { }
    }

    $scope.getHospitalLocationForProvider = function (valLocaton, findData) {
        try {
            $scope.locations = $scope.hospitals.filter(function (hospitals) { return hospitals.HospitalID == valLocaton })[0].HospitalContactInfoes;
            return $scope.locations.filter(function (locations) { return locations.HospitalContactInfoID == findData })[0];
        }
        catch (e) { }
    }

    $scope.getContactPersonForProvider = function (findData) {
        try {
            return $scope.ContactPersons.filter(function (ContactPersons) { return ContactPersons.HospitalContactPersonID == findData })[0];
        }
        catch (e) { }
    }

    $scope.getAdmittingPrivilageStatusForProvider = function (findData) {
        try {
            return $scope.admittingPrivileges.filter(function (admittingPrivileges) { return admittingPrivileges.AdmittingPrivilegeID == findData })[0];
        }
        catch (e) { }
    }

    //set only one as Primary
    $scope.setPrimary = function () {
        try {
            for (var i = 0; i < $scope.HospitalPrivilegeInformations.HospitalPrivilegeDetails.length ; i++) {
                $scope.HospitalPrivilegeInformations.HospitalPrivilegeDetails[i].PreferenceType = "2";
            }
        }
        catch (e) { }
    };

    //=============== Hospital Privilege Information Conditions ==================

    $scope.indexVal = "";

    $scope.visibilityControl = "";
    //Visibility of the div Control object to perform show and hide    

    $scope.tempObject = {};
    //Temp object to hold the form data so that the data gets revert once clicked cancel while add and edit


    //Controls the View and Add feature on the page
    $scope.operateViewAndAddControl = function (sectionValue) {
        $rootScope.closeAlertMessage();
        // $scope.HospitalInformationPendingRequest = false;
        $scope.tempObject = {};
        $scope.buttonLabel = "Add"
        $scope.visibilityControl = sectionValue;
        $('[data-toggle="tooltip"]').tooltip();
        try {
            $('.fileinput-exists').find('a').trigger('click');
        } catch (e) {

        }
    };

    //Controls the Edit feature on the page
    $scope.operateEditControl = function (sectionValue, obj) {
        $rootScope.closeAlertMessage();
        $scope.tempObject = {};
        $scope.tempObject.selectedEduLocation = {};
        $scope.buttonLabel = "Update"
        $scope.tempObject = angular.copy(obj);

        if (obj && obj.HospitalContactInfo) {
            $scope.tempObject.HospitalContactInfo = angular.copy(obj.HospitalContactInfo);
        }
        if (obj && obj.HospitalContactPerson) {
            $scope.tempObject.HospitalContactPerson = angular.copy(obj.HospitalContactPerson);
        }

        $scope.visibilityControl = sectionValue;
        $('[data-toggle="tooltip"]').tooltip();
        try {
            $('.fileinput-exists').find('a').trigger('click');
        } catch (e) {

        }
    };
    //Controls the renew Feature
    $scope.operateRenewControl = function (sectionValue, obj) {
        $rootScope.closeAlertMessage();
        $scope.tempObject = {};
        $scope.tempObject.selectedEduLocation = {};
        $scope.buttonLabel = "Renew"
        $scope.tempObject = angular.copy(obj);
        if (obj && obj.HospitalContactInfo) {
            $scope.tempObject.HospitalContactInfo = angular.copy(obj.HospitalContactInfo);
        }
        if (obj && obj.HospitalContactPerson) {
            $scope.tempObject.HospitalContactPerson = angular.copy(obj.HospitalContactPerson);
        }

        $scope.visibilityControl = sectionValue;
        $('[data-toggle="tooltip"]').tooltip();
        try {
            $('.fileinput-exists').find('a').trigger('click');
        } catch (e) {

        }
    };

    //Controls the View and Add feature on the page
    $scope.operateCancelControl = function (Form_Div_Id) {
        $rootScope.closeAlertMessage();
        $scope.tempObject = {};
        $scope.tempHospitalLocationName = "";
        $('.field-validation-error').removeClass('field-validation-error').addClass('field-validation-valid');
        $('.input-validation-error').removeClass('input-validation-error').addClass('valid');
        $scope.visibilityControl = "";
    };

    //====================== Hospital Privilege Information ===================

    $scope.message = { desc: "", status: "" };

    $scope.saveHopitalPrivilegeInformation = function (hospitalPrivilegeInformation) {
        loadingOn();
        $scope.message.desc = "";

        var validationStatus;
        var url;

        //Add Details - Denote the URL

        $formDataHospitalPrivilege = $('#hospitalPrivilegeInfoForm');

        validationStatus = $('#hospitalPrivilegeInfoForm').valid();

        url = rootDir + "/Profile/HospitalPrivilege/UpdateHospitalPrivilegeInfoAsync?profileId=" + profileId;

        if (validationStatus) {
            if (hospitalPrivilegeInformation.HospitalPrivilegeYesNoOption == '1') {
                hospitalPrivilegeInformation.OtherAdmittingArrangements = "";
                $($formDataHospitalPrivilege[0]).find($("[name='OtherAdmittingArrangements']")).val("");
            }
            else {
                try {
                    $scope.HospitalPrivilegeInformations.HospitalPrivilegeDetails = [];
                } catch (e) { }
            }
            $.ajax({
                url: url,
                type: 'POST',
                data: new FormData($formDataHospitalPrivilege[0]),
                async: false,
                cache: false,
                contentType: false,
                processData: false,
                success: function (data) {

                    try {
                        if (data.status == "true") {
                            try {
                                if ($scope.HospitalPrivilegeInformations != null) {
                                    $scope.HospitalInformationPendingRequest = true;
                                }
                                if (data.ActionType == "Update") {
                                    $scope.HospitalPrivilegeInformations.TableState = true;
                                }
                                $scope.HospitalPrivilegeInformations
                                $scope.HospitalPrivilegeInformations = angular.copy(data.hospitalPrivilegeInformation);
                            }
                            catch (e)
                            { }
                            $scope.operateCancelControl('');

                            //messageAlertEngine.callAlertMessage("alertHospitalPrivilegeInformation", "Hospital Privilege Information Saved Successfully!!!!", "success", true);
                            messageAlertEngine.callAlertMessage("alertHospitalPrivilegeInformation", data.successMessage, "success", true);
                            $scope.resetDates();
                        } else {
                            messageAlertEngine.callAlertMessage("alertHospitalPrivilegeInformation", data.status, "danger", true);
                        }
                    } catch (e) {

                    }
                },
                error: function (e) {
                    messageAlertEngine.callAlertMessage("alertHospitalPrivilegeInformation", "Sorry for Inconvenience !!!! Please Try Again Later...", "danger", true);
                }
            });
        }

        loadingOff();
    };

    $scope.$watch('tempObject.Hospital.HospitalName', function (newV, oldV) {
        if (newV == oldV) {
            return;
        }
        if (newV != null || newV != "") {
            $scope.isHospitalError = true;
        }
    });

    $scope.$watch('tempObject.HospitalContactInfo.LocationName', function (newV, oldV) {
        if (newV == oldV) {
            return;
        }
        if (newV != null || newV != "") {
            $scope.isLocationError = true;
        }
    });

    $scope.ClearHospital = function () {
        $scope.isHospitalError = true;
        $scope.isLocationError = true;
    }

    $scope.isHospitalError = true;
    $scope.isLocationError = true;
    $scope.setErrorBitHospital = function (name) {
        if (name == "") {
            $scope.isHospitalError = false;
        } else {
            $scope.isHospitalError = true;
        }
    }

    $scope.setErrorBitLocation = function (name) {
        if (name == "") {
            $scope.isLocationError = false;
        } else {
            $scope.isLocationError = true;
        }
    }

    //====================== Hospital Privilege Detail ===================
    $scope.saveHospitalPrivilegeDetail = function (hospitalPrivilegeInformation, IndexValue) {
        $scope.HospitalPrivilegeError = '';

        $scope.indexVal = IndexValue;
        var validationStatus;
        var url;
        var myData = {};
        var $formDataHospitalPrivilege;



        if ($scope.visibilityControl == 'addhospitalPrivilegeInformation') {
            //Add Details - Denote the URL


            $formDataHospitalPrivilege = $('#newShowHosPrvDiv').find('form');

            ResetFormForValidation($formDataHospitalPrivilege);
            //validationStatus = true;
            url = rootDir + "/Profile/HospitalPrivilege/AddHospitalPrivilegeAsync?profileId=" + profileId;
            validationStatus = $('#newShowHosPrvDiv').find('form').valid();
        }
        else if ($scope.visibilityControl == (IndexValue + '_edithospitalPrivilegeInformation')) {
            //Update Details - Denote the URL
            //ResetFormForValidation($formDataHospitalPrivilege);
            $formDataHospitalPrivilege = $('#hospitalPrivilegeInformationEditDiv' + IndexValue).find('form');

            ResetFormForValidation($formDataHospitalPrivilege);
            //validationStatus = true;
            url = rootDir + "/Profile/HospitalPrivilege/UpdateHospitalPrivilegeAsync?profileId=" + profileId;
            validationStatus = $('#hospitalPrivilegeInformationEditDiv' + IndexValue).find('form').valid();
        }
        else {
            //ResetFormForValidation($formDataHospitalPrivilege);
            $formDataHospitalPrivilege = $('#hospitalPrivilegeInformationRenewDiv' + IndexValue).find('form');

            ResetFormForValidation($formDataHospitalPrivilege);
            //validationStatus = true;
            url = rootDir + "/Profile/HospitalPrivilege/RenewHospitalPrivilegeAsync?profileId=" + profileId;
            validationStatus = $('#hospitalPrivilegeInformationRenewDiv' + IndexValue).find('form').valid();
        }
        var hospitalname = $($formDataHospitalPrivilege).find("[name='Hospital.HospitalName']").val();
        var locationname = $($formDataHospitalPrivilege).find("[name='LocationName']").val();
        if (hospitalname == "" || typeof (hospitalname) === undefined) {
            $scope.isHospitalError = false;
        } else { $scope.isHospitalError = true; }
        if (locationname == "" || typeof (locationname) === undefined) {
            $scope.isLocationError = false;
        } else { $scope.isLocationError = true; }

        if (validationStatus && $scope.isHospitalError && $scope.isLocationError) {

            $.ajax({
                url: url,
                type: 'POST',
                data: new FormData($formDataHospitalPrivilege[0]),
                async: false,
                cache: false,
                contentType: false,
                processData: false,
                success: function (data) {

                    try {
                        if (hospitalPrivilegeInformation.PreferenceType == "1") {
                            $scope.setPrimary();
                        }
                        if (data.status == "true") {

                            if (UserRole == "PRO" && data.ActionType == "Update") {
                                hospitalPrivilegeInformation.TableState = true;
                            }

                            if ($scope.visibilityControl == 'addhospitalPrivilegeInformation') {
                                for (var i = 0; i < $scope.admittingPrivileges.length; i++) {

                                    if ($scope.admittingPrivileges[i].AdmittingPrivilegeID == data.hospitalPrivilegeDetail.AdmittingPrivilegeID) {
                                        hospitalPrivilegeInformation.AdmittingPrivilege = $scope.admittingPrivileges[i];
                                    }
                                }
                                for (var i = 0; i < $scope.staffCategories.length; i++) {

                                    if ($scope.staffCategories[i].StaffCategoryID == data.hospitalPrivilegeDetail.StaffCategoryID) {
                                        hospitalPrivilegeInformation.StaffCategory = $scope.staffCategories[i];
                                    }
                                }
                                hospitalPrivilegeInformation.HospitalPrivilegeDetailID = data.id;
                                hospitalPrivilegeInformation.Hospital.HospitalName = $scope.getHospitalNameForProvider(hospitalPrivilegeInformation.Hospital.HospitalID).HospitalName;
                                hospitalPrivilegeInformation.HospitalContactInfo.LocationName = $scope.getHospitalLocationForProvider(hospitalPrivilegeInformation.Hospital.HospitalID, hospitalPrivilegeInformation.HospitalContactInfo.HospitalContactInfoID).LocationName;
                                hospitalPrivilegeInformation.HospitalPrevilegeLetterPath = data.hospitalPrivilegeDetail.HospitalPrevilegeLetterPath;
                                $scope.HospitalPrivilegeInformations.HospitalPrivilegeDetails.push(hospitalPrivilegeInformation);
                                $scope.operateCancelControl('');
                                messageAlertEngine.callAlertMessage("addedNewHospitalPrivilege", "New Hospital Privilege Added Successfully !!!!", "success", true);
                                $scope.tempObject = {};
                                $scope.tempObject = {};
                            }
                            else if ($scope.visibilityControl == (IndexValue + '_edithospitalPrivilegeInformation')) {

                                $scope.HospitalPrivilegeInformationPendingRequest = true;
                                for (var i = 0; i < $scope.admittingPrivileges.length; i++) {

                                    if ($scope.admittingPrivileges[i].AdmittingPrivilegeID == data.hospitalPrivilegeDetail.AdmittingPrivilegeID) {
                                        hospitalPrivilegeInformation.AdmittingPrivilege = $scope.admittingPrivileges[i];
                                    }
                                }


                                for (var i = 0; i < $scope.staffCategories.length; i++) {

                                    if ($scope.staffCategories[i].StaffCategoryID == data.hospitalPrivilegeDetail.StaffCategoryID) {
                                        hospitalPrivilegeInformation.StaffCategory = $scope.staffCategories[i];
                                    }
                                }


                                //var obj = $filter('filter')($scope.admittingPrivileges, { AdmittingPrivilegeID: data.hospitalPrivilegeDetail.AdmittingPrivilegeID })[0];

                                hospitalPrivilegeInformation.Hospital.HospitalName = $scope.getHospitalNameForProvider(hospitalPrivilegeInformation.Hospital.HospitalID).HospitalName;
                                hospitalPrivilegeInformation.HospitalContactInfo.LocationName = $scope.getHospitalLocationForProvider(hospitalPrivilegeInformation.Hospital.HospitalID, hospitalPrivilegeInformation.HospitalContactInfo.HospitalContactInfoID).LocationName;
                                hospitalPrivilegeInformation.HospitalPrevilegeLetterPath = data.hospitalPrivilegeDetail.HospitalPrevilegeLetterPath;
                                $scope.HospitalPrivilegeInformations.HospitalPrivilegeDetails[IndexValue] = hospitalPrivilegeInformation;

                                $scope.operateViewAndAddControl(IndexValue + '_viewhospitalPrivilegeInformation');
                                //messageAlertEngine.callAlertMessage('updateHospitalPrivilege' + IndexValue, "Hospital Privilege Information Updated Successfully !!!!", "success", true);
                                messageAlertEngine.callAlertMessage('updateHospitalPrivilege' + IndexValue, data.successMessage, "success", true);
                            }
                            else {
                                $scope.HospitalPrivilegeInformationPendingRequest = true;
                                for (var i = 0; i < $scope.admittingPrivileges.length; i++) {

                                    if ($scope.admittingPrivileges[i].AdmittingPrivilegeID == data.hospitalPrivilege.AdmittingPrivilegeID) {
                                        hospitalPrivilegeInformation.AdmittingPrivilege.Title = $scope.admittingPrivileges[i].Title;
                                    }
                                }


                                for (var i = 0; i < $scope.staffCategories.length; i++) {

                                    if ($scope.staffCategories[i].StaffCategoryID == data.StaffCategoryID) {
                                        hospitalPrivilegeInformation.StaffCategory.Title = $scope.staffCategories[i].Title;
                                    }
                                }


                                hospitalPrivilegeInformation.Hospital.HospitalName = $scope.getHospitalNameForProvider(hospitalPrivilegeInformation.Hospital.HospitalID).HospitalName;
                                hospitalPrivilegeInformation.HospitalContactInfo.LocationName = $scope.getHospitalLocationForProvider(hospitalPrivilegeInformation.Hospital.HospitalID, hospitalPrivilegeInformation.HospitalContactInfo.HospitalContactInfoID).LocationName;
                                hospitalPrivilegeInformation.HospitalPrevilegeLetterPath = data.hospitalPrivilege.HospitalPrevilegeLetterPath;

                                $scope.HospitalPrivilegeInformations.HospitalPrivilegeDetails[IndexValue] = hospitalPrivilegeInformation;
                                $scope.operateViewAndAddControl(IndexValue + '_viewhospitalPrivilegeInformation');
                                //messageAlertEngine.callAlertMessage('renewHospitalPrivilege' + IndexValue, "Hospital Privilege Information Renewed Successfully !!!!", "success", true);
                                messageAlertEngine.callAlertMessage('renewHospitalPrivilege' + IndexValue, data.successMessage, "success", true);
                            }

                            myData = data;
                            $scope.resetDates();

                        } else {
                            messageAlertEngine.callAlertMessage('HospitalPrivilegeError', "", "danger", true);
                            $scope.HospitalPrivilegeError = data.status.split(",");
                        }
                    } catch (e) {

                    }
                },
                error: function (e) {
                    messageAlertEngine.callAlertMessage('HospitalPrivilegeError', "", "danger", true);
                    $scope.HospitalPrivilegeError = "Sorry for Inconvenience !!!! Please Try Again Later...";
                }
            });
        }
        else {
            //$scope.isHospitalError = true;
            //$scope.isLocationError = true;
        }
        if (myData.hospitalPrivilegeDetail != null) {

            $rootScope.$broadcast('UpdateHospitalPrivilegeDetailDoc', myData);

        } else {

            $rootScope.$broadcast('UpdateHospitalPrivilegeDetailDoc', { hospitalPrivilegeDetail: myData.hospitalPrivilege });

        }

    };

    $scope.initHospitalPrivilegeWarning = function (hospitalPrivilegeInformation) {
        if (angular.isObject(hospitalPrivilegeInformation)) {
            $scope.tempHospitalPrivilege = hospitalPrivilegeInformation;
        }
        $('#hospitalPrivilegeWarningModal').modal();
    };

    $scope.removeHospitalPrivilege = function (hospitalPrivilege) {
        var validationStatus = false;
        var url = null;
        var myData = {};
        var $formData = null;
        $scope.isRemoved = true;
        $formData = $('#removeHospitalPrivilege');
        url = rootDir + "/Profile/HospitalPrivilege/RemoveHospitalPrivilegeAsync?profileId=" + profileId;
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
                    try {

                        if (data.status == "true") {
                            var obj = $filter('filter')($scope.HospitalPrivilegeInformations.HospitalPrivilegeDetails, { HospitalPrivilegeDetailID: data.hospitalPrivilege.HospitalPrivilegeDetailID })[0];
                            myData = data;
                            $scope.HospitalPrivilegeInformations.HospitalPrivilegeDetails.splice($scope.HospitalPrivilegeInformations.HospitalPrivilegeDetails.indexOf(obj), 1);
                            if ($scope.dataFetchedHP == true) {
                                obj.HistoryStatus = 'Deleted';
                                obj.DeletedBy = data.UserName;
                                obj.DeletedDate = moment(new Date).format('MM/DD/YYYY, h:mm:ss a');
                                $scope.HospitalPrivilegeDetailsHistory.push(obj);
                            }
                            $scope.isRemoved = false;
                            $('#hospitalPrivilegeWarningModal').modal('hide');
                            $rootScope.operateCancelControl('');
                            myData = data;
                            messageAlertEngine.callAlertMessage("addedNewHospitalPrivilege", "Hospital Privilege Detail Removed successfully.", "success", true);
                        } else {
                            $('#hospitalPrivilegeWarningModal').modal('hide');
                            messageAlertEngine.callAlertMessage("removeHospitalPrivilegeInformation", data.status, "danger", true);
                            $scope.errorHospitalPrivilegeInformation = "Sorry for Inconvenience !!!! Please Try Again Later...";
                        }
                    } catch (e) {

                    }
                },
                error: function (e) {

                }
            });
        }

        $rootScope.$broadcast('RemoveHospitalPrivilegeDetailDoc', myData);

    };

    $scope.resetDates = function () {

        $scope.isHospitalError = true;
        $scope.isLocationError = true;
        try {
            $scope.tempObject.AffilicationStartDate = new Date();
            $scope.tempObject.AffiliationEndDate = new Date();
        }
        catch (e) { }
    };

    $rootScope.HospitalPrivilegeLoaded = true;
    $scope.dataLoaded = false;
    $rootScope.$on('HospitalPrivilege', function () {
        if (!$scope.dataLoaded) {
            $rootScope.HospitalPrivilegeLoaded = false;
            $http({
                method: 'GET',
                url: rootDir + '/Profile/MasterProfile/GetHospitalPrivilegesProfileDataAsync?profileId=' + profileId
            }).success(function (data, status, headers, config) {
                try {
                    //data.HospitalPrivilegeInformation.HospitalPrivilegeDetails = data.HospitalPrivilegeDetail;
                    for (key in data) {
                        $rootScope.$emit(key, data[key]);
                        $scope.HospitalPrivilegeInformations = data.HospitalPrivilegeInformation;
                        //call respective controller to load data (PSP)
                    }

                    $rootScope.HospitalPrivilegeLoaded = true;
                    $rootScope.$broadcast("LoadRequireMasterDataHospitalPrivilege");
                } catch (e) {
                    $rootScope.HospitalPrivilegeLoaded = true;
                }

            }).error(function (data, status, headers, config) {
                $rootScope.HospitalPrivilegeLoaded = true;
            });
            $scope.dataLoaded = true;
        }
    });

    //....................Hospital Privilege History............................//
    $scope.HospitalPrivilegeDetailsHistory = [];
    $scope.dataFetchedHP = false;

    $scope.showHospitalHistory = function () {
        if ($scope.HospitalPrivilegeDetailsHistory.length == 0) {
            var url = rootDir + "/Profile/ProfileHistory/GetAllHospitalHistory?profileId=" + profileId;
            $http.get(url).success(function (data) {
                try {
                    $scope.HospitalPrivilegeDetailsHistory = data;
                    for (var i = 0; i < $scope.HospitalPrivilegeDetailsHistory.length; i++) {
                        if ($scope.HospitalPrivilegeDetailsHistory[i].HistoryStatus == '' || !$scope.HospitalPrivilegeDetailsHistory[i].HistoryStatus) {
                            $scope.HospitalPrivilegeDetailsHistory[i].HistoryStatus = 'Renewed';
                        }
                    }
                    $rootScope.GetAllUserData();
                    for (var j = 0; j < $scope.HospitalPrivilegeDetailsHistory.length; j++) {
                        for (var i = 0; i < $rootScope.userslist.length; i++) {
                            if ($scope.HospitalPrivilegeDetailsHistory[j].DeletedById != null) {
                                if ($rootScope.userslist[i].CDUserID == $scope.HospitalPrivilegeDetailsHistory[j].DeletedById) {
                                    if ($rootScope.userslist[i].FullName != null) {
                                        $scope.HospitalPrivilegeDetailsHistory[j].DeletedBy = $rootScope.userslist[i].FullName;
                                        break;
                                    }
                                    else {
                                        $scope.HospitalPrivilegeDetailsHistory[j].DeletedBy = $rootScope.userslist[i].UserName;
                                        break;
                                    }
                                }
                            }
                        }
                        if ($scope.HospitalPrivilegeDetailsHistory[j].DeletedDate != null) {
                            var date = moment.utc($scope.HospitalPrivilegeDetailsHistory[j].DeletedDate).toDate();
                            $scope.HospitalPrivilegeDetailsHistory[j].DeletedDate = moment(date).format('MM/DD/YYYY, h:mm:ss a');
                        }
                    }
                } catch (e) {

                }
            });
        }
        $scope.showHospitalHistoryTable = true;
        $scope.dataFetchedHP = true;
    }

    $scope.cancelHospitalHistory = function () {
        $scope.showHospitalHistoryTable = false;
    }

}]);