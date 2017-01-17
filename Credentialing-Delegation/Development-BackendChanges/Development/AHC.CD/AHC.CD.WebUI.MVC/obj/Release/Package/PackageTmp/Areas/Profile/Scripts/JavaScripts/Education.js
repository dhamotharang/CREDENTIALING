
//=========================== Controller declaration ==========================
profileApp.controller('educationController', ['$scope', '$rootScope', '$http', 'masterDataService', 'locationService', 'messageAlertEngine', '$filter', 'profileUpdates', function ($scope, $rootScope, $http, masterDataService, locationService, messageAlertEngine, $filter, profileUpdates) {

    $scope.UnderGraduateSchoolDetailPendingRequest = profileUpdates.getUpdates('Education History', 'Under Graduate/Professional');
    $scope.GraduateSchoolDetailPendingRequest = profileUpdates.getUpdates('Education History', 'Graduate/Medical');
    $scope.ECFMGDetailPendingRequest = profileUpdates.getUpdates('Education History', 'ECFMG Details');
    $scope.ProgramDetailPendingRequest = profileUpdates.getUpdates('Education History', 'Residency/Internship/Fellowship');
    $scope.CMECertificationDetailPendingRequest = profileUpdates.getUpdates('Education History', 'PostGraduate Training/CME');




    $rootScope.$on("LoadRequireMasterDataEducationHistory", function () {
        //==================================Master Data============================================

        $scope.masterSpecialties = [];

        masterDataService.getMasterData(rootDir + "/Profile/MasterData/GetAllSpecialities").then(function (masterSpecialties) {
            $scope.masterSpecialties = masterSpecialties;
        });

        $scope.masterSchools = [];

        masterDataService.getMasterData(rootDir + "/Profile/MasterData/GetAllSchools").then(function (masterSchools) {
            $scope.masterSchools = masterSchools;
        });

        $scope.masterCertifications = [];

        masterDataService.getMasterData(rootDir + "/Profile/MasterData/GetAllCertificates").then(function (masterCertifications) {
            $scope.masterCertifications = masterCertifications;
        });

        $scope.masterDegrees = [];

        masterDataService.getMasterData(rootDir + "/Profile/MasterData/GetAllQualificationDegrees").then(function (masterDegrees) {
            $scope.masterDegrees = masterDegrees;
        });

        $scope.masterHospitals = [];

        masterDataService.getMasterData(rootDir + "/Profile/MasterData/GetAllHospitals").then(function (masterHospitals) {
            $scope.masterHospitals = masterHospitals;
        });

    });

    //------------------------------- Country Code Popover Show by Id ---------------------------------------
    $scope.CountryDialCodes = countryDailCodes;

    $scope.showContryCodeDiv = function (countryCodeDivId) {
        changeVisibilityOfCountryCode();
        $("#" + countryCodeDivId).show();
    };

    //----------------search cum dropdown
    $scope.showStates = function (event) {
        $(event.target).parent().find(".ProviderTypeSelectAutoList").first().show();
    };


    //------------------------------------------------------------------Address Auto-Complete---------------------------------------------------------------------------//

    /* Method addressAutocomplete() gets the details of a location
         Method takes input of location details entered in the text box.*/


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

    function showLocationList(ele) {
        $(ele).parent().find(".ProviderTypeSelectAutoList").first().show();
    }

    $scope.selectedLocation = function (location) {
        $scope.setAddressModels(location);
        $(".ProviderTypeSelectAutoList").hide();
    };

    $scope.resetAddressModels = function () {
        $scope.tempObject.SchoolInformation.City = "";
        $scope.tempObject.SchoolInformation.State = "";
        $scope.tempObject.SchoolInformation.Country = "";
    };

    $scope.setAddressModels = function (location) {
        $scope.tempObject.SchoolInformation.City = location.City;
        $scope.tempObject.SchoolInformation.State = location.State;
        $scope.tempObject.SchoolInformation.Country = location.Country;

    }


    //----------------------------------------------------------------------------------------------------------------------------------------------------------------//    

    //To Display the drop down div
    $scope.searchCumDropDown = function (divId) {
        $("#" + divId).show();
    };

    //Bind the school name with model class to achieve search cum drop down
    $scope.addIntoSchoolDropDown = function (name, div) {
        $scope.tempObject.SchoolInformation.SchoolName = name;
        $("#" + div).hide();
    }
    //Bind the degree name with model class to achieve search cum drop down
    $scope.addIntoDegreeDropDown = function (degree, div) {
        $scope.tempObject.QualificationDegree = degree;
        $("#" + div).hide();
    }

    //Bind the hospital name with model class to achieve search cum drop down
    $scope.addIntoHospitalDropDown = function (hospital, div) {
        $scope.tempObject.HospitalName = hospital;
        $("#" + div).hide();
    }
    //Bind the Certificate name with model class to achieve search cum drop down
    $scope.addIntoCMEDropDown = function (certificate, div) {
        $scope.tempObject.Certification = certificate;
        $("#" + div).hide();
    }

    //===========================Education Details====================================================================
    $scope.isRemoved = false;

    $scope.educationDetailViewModels = [];
    $scope.GraduationDetailViewModel = [];

    // rootScoped on emitted value catches the value for the model and insert to get the old data for Education Details
    $rootScope.$on('EducationDetails', function (event, val) {

        for (var i = 0; i < val.length ; i++) {
            if (val[i] != null) {
                //val[i].StartDate = ConvertDateFormat(val[i].StartDate);
                //val[i].EndDate = ConvertDateFormat(val[i].EndDate);

                if (val[i].EducationQualificationType == 1) {
                    $scope.educationDetailViewModels.push(val[i]);

                }
                else {
                    $scope.GraduationDetailViewModel.push(val[i]);

                }
            }
        }

    });


    //--------------------------------- History-------------------------------------

    $scope.schooDetailsArray = [];
    $scope.UndergraduateArray = [];
    $scope.GraduateArray = [];
    $scope.dataFetchedED = false;

    $scope.showUnderGraduateDetailHistory = function (loadingId) {
        if ($scope.schooDetailsArray.length == 0) {
            $("#" + loadingId).css('display', 'block');
            var url = rootDir + "/Profile/ProfileHistory/GetAllEducationDetailHistory?profileId=" + profileId;
            $http.get(url).success(function (data) {
                $scope.schooDetailsArray = data;
                for (var i = 0; i < $scope.schooDetailsArray.length; i++) {
                    $scope.dataFetchedED = true;
                    if ($scope.schooDetailsArray[i].QualificationType == 'Graduate') {
                        $scope.GraduateArray.push($scope.schooDetailsArray[i]);
                    } else {
                        $scope.UndergraduateArray.push($scope.schooDetailsArray[i]);
                    }
                }
                $scope.showUnderGraduateDetailTable = true;
                $("#" + loadingId).css('display', 'none');
            });
        }
        else {
            $scope.showUnderGraduateDetailTable = true;
        }

    }

    $scope.cancelUnderGraduateDetailHistory = function () {
        $scope.showUnderGraduateDetailTable = false;
    }

    $scope.showGraduateDetailHistory = function (loadingId) {
        if ($scope.schooDetailsArray.length == 0) {
            $("#" + loadingId).css('display', 'block');
            var url = rootDir + "/Profile/ProfileHistory/GetAllEducationDetailHistory?profileId=" + profileId;
            $http.get(url).success(function (data) {
                $scope.schooDetailsArray = data;
                for (var i = 0; i < $scope.schooDetailsArray.length; i++) {
                    $scope.dataFetchedED = true;
                    if ($scope.schooDetailsArray[i].QualificationType == 'Graduate') {
                        $scope.GraduateArray.push($scope.schooDetailsArray[i]);
                    } else {
                        $scope.UndergraduateArray.push($scope.schooDetailsArray[i]);
                    }
                }
                $scope.showGraduateDetailTable = true;
                $("#" + loadingId).css('display', 'none');
            });
        }
        else {
            $scope.showGraduateDetailTable = true;
        }

    }

    $scope.cancelGraduateDetailHistory = function () {
        $scope.showGraduateDetailTable = false;
    }

    //**************************************************************************************
    $scope.CMECertificationArray = [];
    $scope.dataFetchedCC = false;

    $scope.showCMECertificationHistory = function (loadingId) {
        if ($scope.CMECertificationArray.length == 0) {
            $("#" + loadingId).css('display', 'block');
            var url = rootDir + "/Profile/ProfileHistory/GetCMECertificationHistory?profileId=" + profileId;
            $http.get(url).success(function (data) {
                $scope.CMECertificationArray = data;
                $scope.showCMECertificationTable = true;
                $scope.dataFetchedCC = true;
                $("#" + loadingId).css('display', 'none');
            });
        } else {

            $scope.showCMECertificationTable = true;
        }
    }

    $scope.cancelCMECertificationHistory = function () {
        $scope.showCMECertificationTable = false;
    }


    //**************************************************************************************

    $scope.programDetailHistoryArray = [];
    $scope.dataFetchedPD = false;

    $scope.showProgramDetailHistory = function (loadingId) {
        if ($scope.programDetailHistoryArray.length == 0) {
            $("#" + loadingId).css('display', 'block');
            var url = rootDir + "/Profile/ProfileHistory/GetProgramDetailHistory?profileId=" + profileId;
            $http.get(url).success(function (data) {
                $scope.programDetailHistoryArray = data;
                $scope.showProgramDetailTable = true;
                $scope.dataFetchedPD = true;
                $("#" + loadingId).css('display', 'none');
            });
        } else {
            $scope.showProgramDetailTable = true;
        }
    }

    $scope.cancelProgramDetailHistory = function () {
        $scope.showProgramDetailTable = false;
    }

    //--------------------------------- End-------------------------------------

    //===============================Under Graduate/Professional Schools Details============================================

    $scope.saveUG = function (educationDetailViewModel, IndexValue) {
        loadingOn();
        var validationStatus;
        var url;
        var myData = {};
        var $formData;

        if ($scope.visibilityControl == 'addeducationDetailViewModel') {
            //Add Details - Denote the URL            
            $formData = $('#newUGFormDiv').find('form');
            url = rootDir + "/Profile/EducationHistory/AddEducationDetailAsync?profileId=" + profileId;
        }
        else if ($scope.visibilityControl == (IndexValue + '_editeducationDetailViewModel')) {
            //Update Details - Denote the URL            
            $formData = $('#ugEditDiv' + IndexValue).find('form');
            url = rootDir + "/Profile/EducationHistory/UpdateEducationDetailAsync?profileId=" + profileId;
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
                        if ($scope.visibilityControl == 'addeducationDetailViewModel') {
                            data.educationDetails.StartDate = ConvertDateFormat(data.educationDetails.StartDate);
                            data.educationDetails.EndDate = ConvertDateFormat(data.educationDetails.EndDate);
                            $scope.educationDetailViewModels.push(data.educationDetails);
                            $rootScope.operateCancelControl();
                            messageAlertEngine.callAlertMessage("addedNewUGDetails", "New Under Graduate/Professional School Details Added Successfully !!!!", "success", true);

                        }
                        else if ($scope.visibilityControl == (IndexValue + '_editeducationDetailViewModel')) {
                            $scope.UnderGraduateSchoolDetailPendingRequest = true;
                            data.educationDetails.StartDate = ConvertDateFormat(data.educationDetails.StartDate);
                            data.educationDetails.EndDate = ConvertDateFormat(data.educationDetails.EndDate);
                            $scope.educationDetailViewModels[IndexValue] = data.educationDetails;
                            $rootScope.operateViewAndAddControl(IndexValue + '_vieweducationDetailViewModel');
                            messageAlertEngine.callAlertMessage('updatedUGDetails' + IndexValue, "Under Graduate/Professional School Details Updated Successfully !!!!", "success", true);

                        }
                        $scope.datePickerReset();
                        myData = data;
                        FormReset($formData);
                    } else {
                        messageAlertEngine.callAlertMessage('ErrorInUGDetails' + IndexValue, "", "danger", true);
                        $scope.UGDetailsErrorList = data.status.split(",");
                    }
                },
                error: function (e) {
                    messageAlertEngine.callAlertMessage('ErrorInUGDetails' + IndexValue, "", "danger", true);
                    $scope.UGDetailsErrorList = "Sorry for Inconvenience !!!! Please Try Again Later...";
                }
            });
        }

        $rootScope.$broadcast('UpdateEducationDetailDoc', myData);

        loadingOff();
    };

    //To initiate Removal Confirmation Modal
    $scope.initUGSchoolDetailWarning = function (educationDetailViewModel) {
        if (angular.isObject(educationDetailViewModel)) {
            $scope.tempUGSchool = educationDetailViewModel;
        }
        $('#UGSchoolWarningModal').modal();
    };

    $scope.removeUGSchool = function (educationDetailViewModels) {
        var validationStatus = false;
        var url = null;
        var myData = {};
        var $formData = null;
        //$scope.isRemoved = true;
        $formData = $('#removeUGSchool');
        url = rootDir + "/Profile/EducationHistory/RemoveEducationDetailAsync?profileId=" + profileId;
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
                    if (data.status == "true") {
                        var obj = $filter('filter')(educationDetailViewModels, { EducationDetailID: data.educationDetailViewModel.EducationDetailID })[0];
                        educationDetailViewModels.splice(educationDetailViewModels.indexOf(obj), 1);
                        if ($scope.dataFetchedED == true) {
                            obj.HistoryStatus = 'Deleted';
                            $scope.UndergraduateArray.push(obj);
                        }
                        //$timeout(function () {
                        //    $scope.isRemoved = false;
                        //}, 5000);
                        //$scope.isRemoved = false;
                        $('#UGSchoolWarningModal').modal('hide');
                        $rootScope.operateCancelControl('');
                        myData = data;
                        messageAlertEngine.callAlertMessage("addedNewUGDetails", "Under Graduate/Professional Schools Detail Removed successfully.", "success", true);
                    } else {
                        $('#UGSchoolWarningModal').modal('hide');
                        messageAlertEngine.callAlertMessage("removeUGDetails", data.status, "danger", true);
                        $scope.errorUGDetails = "Sorry for Inconvenience !!!! Please Try Again Later...";
                    }
                },
                error: function (e) {

                }
            });
        }

        $rootScope.$broadcast('RemoveEducationDetailDoc', myData);

    };

    //============== Graduate Details  ================

    $scope.saveGraduation = function (graduationDetailViewModel, IndexValue) {
        loadingOn();
        var validationStatus;
        var url;
        var myData = {};
        var $formData;
        $scope.IndexValue = 0;
        if ($scope.visibilityControl == 'addgraduationDetailViewModel') {
            //Add Details - Denote the URL            
            $formData = $('#newGraduationFormDiv').find('form');
            url = rootDir + "/Profile/EducationHistory/AddEducationDetailAsync?profileId=" + profileId;
        }
        else if ($scope.visibilityControl == (IndexValue + '_editgraduationDetailViewModel')) {
            //Update Details - Denote the URL            
            $formData = $('#graduationEditDiv' + IndexValue).find('form');
            url = rootDir + "/Profile/EducationHistory/UpdateEducationDetailAsync?profileId=" + profileId;
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
                        if ($scope.visibilityControl == 'addgraduationDetailViewModel') {
                            data.educationDetails.StartDate = ConvertDateFormat(data.educationDetails.StartDate);
                            data.educationDetails.EndDate = ConvertDateFormat(data.educationDetails.EndDate);
                            $scope.GraduationDetailViewModel.push(data.educationDetails);
                            $rootScope.operateCancelControl();
                            messageAlertEngine.callAlertMessage("addedNewPGDetails", "New Graduate/Medical School Details Added Successfully !!!!", "success", true);
                        }
                        else if ($scope.visibilityControl == (IndexValue + '_editgraduationDetailViewModel')) {
                            $scope.GraduateSchoolDetailPendingRequest = true;
                            data.educationDetails.StartDate = ConvertDateFormat(data.educationDetails.StartDate);
                            data.educationDetails.EndDate = ConvertDateFormat(data.educationDetails.EndDate);
                            $scope.GraduationDetailViewModel[IndexValue] = data.educationDetails;
                            $rootScope.operateViewAndAddControl(IndexValue + '_viewgraduationDetailViewModel');
                            messageAlertEngine.callAlertMessage('updatedPGDetails' + IndexValue, "Graduate/Medical School Details Updated Successfully !!!!", "success", true);
                        }
                        $scope.datePickerReset();
                        myData = data;
                        FormReset($formData);
                    } else {
                        messageAlertEngine.callAlertMessage('ErrorInPGDetails' + IndexValue, "", "danger", true);
                        $scope.PGDetailsErrorList = data.status.split(",");
                    }
                },
                error: function (e) {
                    messageAlertEngine.callAlertMessage('ErrorInPGDetails' + IndexValue, "", "danger", true);
                    $scope.PGDetailsErrorList = "Sorry for Inconvenience !!!! Please Try Again Later...";
                }
            });
        }

        $rootScope.$broadcast('UpdateEducationDetailDoc', myData);

        loadingOff();
    };

    //To initiate Removal Confirmation Modal
    $scope.initGSchoolDetailWarning = function (graduationDetailViewModel) {
        if (angular.isObject(graduationDetailViewModel)) {
            $scope.tempGSchool = graduationDetailViewModel;
        }
        $('#GSchoolWarningModal').modal();
    };

    $scope.removeGSchool = function (GraduationDetailViewModel) {
        var validationStatus = false;
        var url = null;
        var myData = {};
        var $formData = null;
        $scope.isRemoved = true;
        $formData = $('#removeGSchool');
        url = rootDir + "/Profile/EducationHistory/RemoveEducationDetailAsync?profileId=" + profileId;
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
                    if (data.status == "true") {
                        var obj = $filter('filter')(GraduationDetailViewModel, { EducationDetailID: data.educationDetailViewModel.EducationDetailID })[0];
                        GraduationDetailViewModel.splice(GraduationDetailViewModel.indexOf(obj), 1);
                        if ($scope.dataFetchedED == true) {
                            obj.HistoryStatus = 'Deleted';
                            $scope.GraduateArray.push(obj);
                        }
                        $scope.isRemoved = false;
                        $('#GSchoolWarningModal').modal('hide');
                        $rootScope.operateCancelControl('');
                        myData = data;
                        messageAlertEngine.callAlertMessage("addedNewPGDetails", "Graduate/Professional Schools Detail Removed successfully.", "success", true);
                    } else {
                        $('#GSchoolWarningModal').modal('hide');
                        messageAlertEngine.callAlertMessage("removePGDetails", data.status, "danger", true);
                        $scope.errorPGDetails = "Sorry for Inconvenience !!!! Please Try Again Later...";
                    }
                },
                error: function (e) {

                }
            });
        }

        $rootScope.$broadcast('RemoveEducationDetailDoc', myData);

    };

    //=================================ECFMG Details===========================


    // rootScoped on emitted value catches the value for the model and insert to get the old data for ECFMG Details
    $rootScope.$on('ECFMGDetail', function (event, val) {

        if (val != null) {
            //val.ECFMGIssueDate = ConvertDateFormat(val.ECFMGIssueDate);
            $scope.ecfmgDetail = val;
        }

    });

    $scope.saveECFMGDetails = function (ecfmgDetail) {
        loadingOn();
        var validationStatus;
        var url;
        var myData = {};
        var $formData;

        if ($scope.visibilityControl == 'editecfmgDetail') {
            //Add Details - Denote the URL            
            $formData = $('#ecfmgEditDiv').find('form');
            url = rootDir + "/Profile/EducationHistory/UpdateECFMGDetailAsync?profileId=" + profileId;
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
                        $scope.ErrorInUGDetails = false;
                        if ($scope.visibilityControl == 'editecfmgDetail') {
                            $scope.ECFMGDetailPendingRequest = true;
                            data.ecfmgDetails.ECFMGIssueDate = ConvertDateFormat(data.ecfmgDetails.ECFMGIssueDate);
                            $rootScope.operateCancelControl();
                            $scope.ecfmgDetail = data.ecfmgDetails;
                            messageAlertEngine.callAlertMessage("addedNewECFMGDetails", "ECFMG Detail Saved Successfully !!!!", "success", true);
                        }
                        else if ($scope.visibilityControl == ('updateecfmgDetail')) {
                            data.ecfmgDetails.ECFMGIssueDate = ConvertDateFormat(data.ecfmgDetails.ECFMGIssueDate);
                            $scope.ecfmgDetail = data.ecfmgDetails;
                            $rootScope.operateViewAndAddControl('!editecfmgDetail');
                            messageAlertEngine.callAlertMessage('updatedECFMGDetails', "ECFMG Detail Saved Successfully !!!!", "success", true);
                        }
                        myData = data;
                        $scope.datePickerReset();

                    } else {
                        messageAlertEngine.callAlertMessage('ErrorInECFMGDetails', "", "danger", true);
                        $scope.ECFMGDetailsErrorList = data.status.split(",");
                    }
                },
                error: function (e) {
                    messageAlertEngine.callAlertMessage('ErrorInECFMGDetails', "", "danger", true);
                    $scope.ECFMGDetailsErrorList = "Sorry for Inconvenience !!!! Please Try Again Later...";
                }
            });
        }
        $rootScope.$broadcast('UpdateECFMGDetailDoc', myData);
        loadingOff();
    };

    //============ Training Details ==================

    $scope.ResidencyInternshipViewModel = [];
    $scope.hideDiv = function () {
        $('.ProviderTypeSelectAutoList1').hide();
        $('.ProviderTypeSelectAutoList').hide();
    }

    // rootScoped on emitted value catches the value for the model and insert to get the old data for Residency/Internship/Fellowship Details
    $rootScope.$on('TrainingDetails', function (event, val) {

        for (var i = 0; i < val.length ; i++) {
            if (val[i] != null) {
                $scope.ResidencyInternshipViewModel.push(val[i]);
            }
        }
    });

    //======================New Residency/Internship/Fellowship/Other================
    $rootScope.$on('ProgramDetails', function (event, val) {
        $scope.ResidencyInternshipViewModel = val;
        for (var i = 0; i < $scope.ResidencyInternshipViewModel.length ; i++) {
            if (!$scope.ResidencyInternshipViewModel[i].SpecialtyID) { $scope.ResidencyInternshipViewModel[i].SpecialtyID = ""; }
        }
    });

    $scope.clearReason = function (value) {
        if (value == '1') {
            $scope.tempObject.InCompleteReason = "";
        }
    }

    $scope.prefillDataForAdd = function (value) {
        for (var i = 0; i < $scope.ResidencyInternshipViewModel.length; i++) {
            if ($scope.ResidencyInternshipViewModel[i].SchoolInformation.SchoolInformationID == parseInt(value)) {
                //$scope.tempObject.InCompleteReason = $scope.ResidencyInternshipViewModel[i].InCompleteReason;
                $scope.tempObject.CompletedYesNoOption = $scope.ResidencyInternshipViewModel[i].CompletedYesNoOption;
                $scope.tempObject.HospitalName = $scope.ResidencyInternshipViewModel[i].HospitalName;
                $scope.tempObject.SchoolInformation = $scope.ResidencyInternshipViewModel[i].SchoolInformation;
            }
        }
    }

    //Set primary to secondary
    $scope.setPrimary = function () {
        try {
            if ($scope.ResidencyInternshipViewModel.length > 0) {
                for (var i = 0; i < $scope.ResidencyInternshipViewModel.length; i++) {
                    $scope.ResidencyInternshipViewModel[i].PreferenceType = "2";
                    $scope.ResidencyInternshipViewModel[i].Preference = "Secondary";
                }
            }
        }
        catch (e) { }
    };

    $scope.saveResidencyInternshipFellowshipProgram = function (residencyInternshipViewModel, IndexValue) {
        loadingOn();
        var validationStatus;
        var url;
        var myData = {};
        var $formData;

        if ($scope.visibilityControl == 'addProgramDetail') {
            //Add Details - school and list of residency/internship/fellowship details            
            $formData = $('#newProgramDetailDiv').find('form');
            url = rootDir + "/Profile/EducationHistory/AddProgramDetailAsync?profileId=" + profileId;
        }
        else if ($scope.visibilityControl == (IndexValue + '_editProgramDetail')) {
            //Update Details - update school details
            $formData = $('#programDetailDiv' + IndexValue).find('form');
            url = rootDir + "/Profile/EducationHistory/UpdateProgramDetailAsync?profileId=" + profileId;
        }

        var SpecialtyName = $($formData[0]).find($("[name='SpecialtyName']")).val();

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

                        if (residencyInternshipViewModel.PreferenceType == "1") {
                            $scope.setPrimary();
                        }

                        if (data.programDetails != null) {
                            data.programDetails.Specialty = { ID: data.programDetails.SpecialtyID, Name: SpecialtyName };
                            data.programDetails.StartDate = ConvertDateFormat(data.programDetails.StartDate);
                            data.programDetails.EndDate = ConvertDateFormat(data.programDetails.EndDate);
                            if (!data.programDetails.SpecialtyID) {
                                data.programDetails.SpecialtyID = "";
                                data.programDetails.Specialty.Name = "";
                            }
                        }

                        $scope.ErrorInProgramDetails = false;

                        if ($scope.visibilityControl == 'addProgramDetail') {
                            //$rootScope.visibilityControl = "addedNewProgramDetails";
                            $scope.ResidencyInternshipViewModel.push(data.programDetails);
                            //var addIndex = $scope.ResidencyInternshipViewModel.length - 1;
                            $rootScope.operateCancelControl();
                            //$rootScope.operateViewAndAddControl(addIndex + '_viewProgramDetail');
                            messageAlertEngine.callAlertMessage("addedNewProgramDetails", "Residency/Internship/fellowship Details Added Successfully!!!", "success", true);

                        }
                        else if ($scope.visibilityControl == (IndexValue + '_editProgramDetail')) {
                            $scope.ProgramDetailPendingRequest = true;
                            $scope.ResidencyInternshipViewModel[IndexValue] = data.programDetails;

                            $rootScope.operateViewAndAddControl(IndexValue + '_viewProgramDetail');
                            messageAlertEngine.callAlertMessage('updatedProgramDetails' + IndexValue, "Residency/Internship/fellowship Details Updated Successfully !!!!", "success", true);

                        }
                        $scope.datePickerReset();
                        myData = data;
                        FormReset($formData);
                    }
                    else {
                        messageAlertEngine.callAlertMessage('ErrorInProgramDetails' + IndexValue, "", "danger", true);
                        $scope.ProgramDetailsErrorList = data.status.split(",");
                    }
                },
                error: function (e) {
                    messageAlertEngine.callAlertMessage('ErrorInProgramDetails' + IndexValue, "", "danger", true);
                    $scope.ResidencyDetailsErrorList = "Sorry for Inconvenience !!!! Please Try Again Later...";
                }
            });
        }
        $rootScope.$broadcast('UpdateProgramDetailsDoc', myData);
        loadingOff();
    };

    //To initiate Removal Confirmation Modal
    $scope.initProgramDetailWarning = function (residencyInternshipViewModel) {
        if (angular.isObject(residencyInternshipViewModel)) {
            $scope.tempProgramDetail = residencyInternshipViewModel;
        }
        $('#programDetailWarningModal').modal();
    };

    $scope.removeProgramDetail = function (ResidencyInternshipViewModel) {
        var validationStatus = false;
        var url = null;
        var myData = {};
        var $formData = null;
        $scope.isRemoved = true;
        $formData = $('#removeProgramDetail');
        url = rootDir + "/Profile/EducationHistory/RemoveProgramDetailAsync?profileId=" + profileId;
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
                    if (data.status == "true") {
                        var obj = $filter('filter')(ResidencyInternshipViewModel, { ProgramDetailID: data.residencyInternshipViewModel.ProgramDetailID })[0];
                        ResidencyInternshipViewModel.splice(ResidencyInternshipViewModel.indexOf(obj), 1);
                        if ($scope.dataFetchedPD = true) {
                            obj.HistoryStatus = 'Deleted';
                            $scope.programDetailHistoryArray.push(obj);
                        }
                        $scope.isRemoved = false;
                        $('#programDetailWarningModal').modal('hide');
                        $rootScope.operateCancelControl('');
                        myData = data;
                        messageAlertEngine.callAlertMessage("addedNewProgramDetails", "Residency/Internship/Fellowship Detail Removed successfully.", "success", true);
                    } else {
                        $('#programDetailWarningModal').modal('hide');
                        messageAlertEngine.callAlertMessage("removeProgramDetails", data.status, "danger", true);
                        $scope.errorProgramDetails = "Sorry for Inconvenience !!!! Please Try Again Later...";
                    }
                },
                error: function (e) {

                }
            });
        }

        $rootScope.$broadcast('RemoveProgramDetailsDoc', myData);
    };

    //======================Residency/Internship/Fellowship/Other================


    $scope.saveProgram = function (residencyInternshipViewModel, IndexValue) {
        loadingOn();
        var validationStatus;
        var url;
        var $formData;

        if ($scope.visibilityControl == 'addresidencyInternshipViewModel') {
            //Add Details - school and list of residency/internship/fellowship details            
            $formData = $('#newProgramFormDiv').find('form');
            url = rootDir + "/Profile/EducationHistory/AddTrainingDetailAsync?profileId=" + profileId;
        }
        else if ($scope.visibilityControl == (IndexValue + '_editresidencyInternshipViewModel')) {
            //Update Details - update school details
            $formData = $('#programEditDiv' + IndexValue).find('form');
            url = rootDir + "/Profile/EducationHistory/UpdateTrainingDetailAsync?profileId=" + profileId;
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

                        for (var i = 0; i < data.TrainingDetails.ResidencyInternshipDetails.length ; i++) {
                            if (data.TrainingDetails.ResidencyInternshipDetails != null) {
                                data.TrainingDetails.ResidencyInternshipDetails[i].StartDate = ConvertDateFormat(data.TrainingDetails.ResidencyInternshipDetails[i].StartDate);
                                data.TrainingDetails.ResidencyInternshipDetails[i].EndDate = ConvertDateFormat(data.TrainingDetails.ResidencyInternshipDetails[i].EndDate);
                            }
                        }

                        $scope.ErrorInProgramDetails = false;
                        if ($scope.visibilityControl == 'addresidencyInternshipViewModel') {
                            //$rootScope.visibilityControl = "addedNewProgramDetails";
                            $scope.ResidencyInternshipViewModel.push(data.TrainingDetails);
                            var addIndex = $scope.ResidencyInternshipViewModel.length - 1;
                            //$rootScope.operateCancelControl();
                            $rootScope.operateViewAndAddControl(addIndex + '_viewresidencyInternshipViewModel');
                            messageAlertEngine.callAlertMessage("addedNewProgramDetails" + addIndex, "School Details Of Residency/Internship/fellowship Details Added Successfully!!!", "success", true);

                        }
                        else if ($scope.visibilityControl == (IndexValue + '_editresidencyInternshipViewModel')) {
                            $scope.ResidencyInternshipViewModel[IndexValue] = data.TrainingDetails;
                            $rootScope.operateViewAndAddControl(IndexValue + '_viewresidencyInternshipViewModel');
                            messageAlertEngine.callAlertMessage('updatedProgramDetails' + IndexValue, "School Details Of Residency/Internship/fellowship Details Updated Successfully !!!!", "success", true);

                        }
                        $scope.datePickerReset();
                        FormReset($formData);
                    }
                    else {
                        messageAlertEngine.callAlertMessage('ErrorInProgramDetails' + IndexValue, "", "danger", true);
                        $scope.ProgramDetailsErrorList = data.status.split(",");
                    }
                },
                error: function (e) {
                    messageAlertEngine.callAlertMessage('ErrorInProgramDetails' + IndexValue, "", "danger", true);
                    $scope.ResidencyDetailsErrorList = "Sorry for Inconvenience !!!! Please Try Again Later...";
                }
            });
        }

        loadingOff();
    };

    $scope.AddView = "Add";

    $scope.SetAddView = function (action) {
        $scope.AddView = action;
    };



    $scope.updateResidencyForView = function (residencyInternshipViewModel, residencyInternship, IndexValue) {

        loadingOn();
        var url;
        var $formDataResidency;
        var trainingId = residencyInternshipViewModel.TrainingDetailID;
        var SpecialtyName;


        if (($scope.visibilitySecondControl == 'addresidencyForView')) {
            //Add Details - Add residency/internship/fellowship for a existing school details            
            $formDataResidency = $('#newViewResidencyDivFor' + $scope.AddView + trainingId).find('form');
            SpecialtyName = $($formDataResidency[0]).find($("[name='SpecialtyID'] option:selected")).text();
            url = rootDir + "/Profile/EducationHistory/AddResidencyInternshipDetailAsync?profileId=" + profileId + "&trainingId=" + trainingId;
        }
        else if ($scope.visibilitySecondControl == (IndexValue + '_editresidencyForView')) {
            //Update Details - update residency/internship/fellowship details            
            $formDataResidency = $('#viewResidencyEditDiv' + residencyInternship.ResidencyInternshipDetailID).find('form');
            SpecialtyName = $($formDataResidency[0]).find($("[name='SpecialtyID'] option:selected")).text();
            url = rootDir + "/Profile/EducationHistory/UpdateResidencyInternshipDetailAsync?profileId=" + profileId + "&trainingId=" + trainingId;
        }

        ResetFormForValidation($formDataResidency);
        validationStatus = $formDataResidency.valid();

        if (validationStatus) {
            //Simple POST request example (passing data)
            $.ajax({
                url: url,
                type: 'POST',
                data: new FormData($formDataResidency[0]),
                async: false,
                cache: false,
                contentType: false,
                processData: false,
                success: function (data) {
                    if (residencyInternship.PreferenceType == "1") {
                        $scope.setPrimary();
                    }
                    if (data.status == "true") {
                        $scope.ErrorInResidencyDetails = false;
                        if ($scope.visibilitySecondControl == 'addresidencyForView') {
                            data.ResidencyDetails.Specialty = { ID: data.ResidencyDetails.SpecialtyID, Name: SpecialtyName };
                            data.ResidencyDetails.StartDate = ConvertDateFormat(data.ResidencyDetails.StartDate);
                            data.ResidencyDetails.EndDate = ConvertDateFormat(data.ResidencyDetails.EndDate);

                            for (var i = 0; i < $scope.ResidencyInternshipViewModel.length; i++) {
                                if ($scope.ResidencyInternshipViewModel[i].TrainingDetailID == trainingId) {
                                    $scope.ResidencyInternshipViewModel[i].ResidencyInternshipDetails.push(data.ResidencyDetails);
                                    //residencyInternshipViewModel.ResidencyInternshipDetails.push(data.ResidencyDetails);
                                }
                            }
                            $rootScope.operateSecondCancelControl();
                            messageAlertEngine.callAlertMessage("addedNewResidencyDetailsForView", "New Residency/Internship/fellowship Details Added Successfully!!!", "success", true);

                        }
                        else if ($scope.visibilitySecondControl == (IndexValue + '_editresidencyForView')) {
                            data.ResidencyDetails.Specialty = { ID: data.ResidencyDetails.SpecialtyID, Name: SpecialtyName };
                            data.ResidencyDetails.StartDate = ConvertDateFormat(data.ResidencyDetails.StartDate);
                            data.ResidencyDetails.EndDate = ConvertDateFormat(data.ResidencyDetails.EndDate);

                            for (var i = 0; i < $scope.ResidencyInternshipViewModel.length; i++) {
                                if ($scope.ResidencyInternshipViewModel[i].TrainingDetailID == trainingId) {
                                    $scope.ResidencyInternshipViewModel[i].ResidencyInternshipDetails[IndexValue] = data.ResidencyDetails;
                                    residencyInternshipViewModel.ResidencyInternshipDetails[IndexValue] = data.ResidencyDetails;
                                }
                            }
                            $rootScope.operateSecondViewAndAddControl(IndexValue + '_viewresidencyForView');
                            messageAlertEngine.callAlertMessage('updatedResidencyDetailsForView' + IndexValue, "Residency/Internship/fellowship Details Updated Successfully !!!!", "success", true);
                        }
                        $scope.datePickerReset();
                        FormReset($formDataResidency);
                    } else {
                        messageAlertEngine.callAlertMessage('ErrorInResidencyDetailsForView' + IndexValue, "", "danger", true);
                        $scope.ResidencyDetailsErrorListForView = data.status.split(",");
                    }
                },
                error: function (e) {
                    messageAlertEngine.callAlertMessage('ErrorInResidencyDetailsForView' + IndexValue, "", "danger", true);
                    $scope.ResidencyDetailsErrorListForView = "Sorry for Inconvenience !!!! Please Try Again Later...";
                }
            });
        }

        loadingOff();
    };

    $scope.updateResidency = function (tempObject, residencyInternship, IndexValue) {

        loadingOn();
        var url;
        var $formDataResidency;
        var trainingId = tempObject.TrainingDetailID;
        var SpecialtyName;

        if ($scope.visibilitySecondControl == 'addresidency') {
            //Add Details - Add residency/internship/fellowship for a existing school details            
            $formDataResidency = $('#newResidencyDivFor' + $scope.AddView + trainingId).find('form');
            SpecialtyName = $($formDataResidency[0]).find($("[name='SpecialtyID'] option:selected")).text();
            url = rootDir + "/Profile/EducationHistory/AddResidencyInternshipDetailAsync?profileId=" + profileId + "&trainingId=" + trainingId;
        }
        else if ($scope.visibilitySecondControl == (IndexValue + '_editresidency')) {
            //Update Details - update residency/internship/fellowship details            
            $formDataResidency = $('#residencyEditDiv' + residencyInternship.ResidencyInternshipDetailID).find('form');
            SpecialtyName = $($formDataResidency[0]).find($("[name='SpecialtyID'] option:selected")).text();
            url = rootDir + "/Profile/EducationHistory/UpdateResidencyInternshipDetailAsync?profileId=" + profileId + "&trainingId=" + trainingId;
        }


        ResetFormForValidation($formDataResidency);
        validationStatus = $formDataResidency.valid();

        if (validationStatus) {
            //Simple POST request example (passing data)
            $.ajax({
                url: url,
                type: 'POST',
                data: new FormData($formDataResidency[0]),
                async: false,
                cache: false,
                contentType: false,
                processData: false,
                success: function (data) {
                    if (residencyInternship.PreferenceType == "1") {
                        $scope.setPrimary();
                    }
                    if (data.status == "true") {
                        $scope.ErrorInResidencyDetails = false;
                        if ($scope.visibilitySecondControl == 'addresidency') {
                            data.ResidencyDetails.Specialty = { ID: data.ResidencyDetails.SpecialtyID, Name: SpecialtyName };
                            data.ResidencyDetails.StartDate = ConvertDateFormat(data.ResidencyDetails.StartDate);
                            data.ResidencyDetails.EndDate = ConvertDateFormat(data.ResidencyDetails.EndDate);

                            for (var i = 0; i < $scope.ResidencyInternshipViewModel.length; i++) {
                                if ($scope.ResidencyInternshipViewModel[i].TrainingDetailID == trainingId) {
                                    $scope.ResidencyInternshipViewModel[i].ResidencyInternshipDetails.push(data.ResidencyDetails);
                                    tempObject.ResidencyInternshipDetails.push(data.ResidencyDetails);
                                }
                            }
                            $rootScope.operateSecondCancelControl();
                            messageAlertEngine.callAlertMessage("addedNewResidencyDetails", "New Residency/Internship/fellowship Details Added Successfully!!!", "success", true);

                        }
                        else if ($scope.visibilitySecondControl == (IndexValue + '_editresidency')) {
                            data.ResidencyDetails.Specialty = { ID: data.ResidencyDetails.SpecialtyID, Name: SpecialtyName };
                            data.ResidencyDetails.StartDate = ConvertDateFormat(data.ResidencyDetails.StartDate);
                            data.ResidencyDetails.EndDate = ConvertDateFormat(data.ResidencyDetails.EndDate);

                            for (var i = 0; i < $scope.ResidencyInternshipViewModel.length; i++) {
                                if ($scope.ResidencyInternshipViewModel[i].TrainingDetailID == trainingId) {
                                    $scope.ResidencyInternshipViewModel[i].ResidencyInternshipDetails[IndexValue] = data.ResidencyDetails;
                                    tempObject.ResidencyInternshipDetails[IndexValue] = data.ResidencyDetails;
                                }
                            }
                            $rootScope.operateSecondViewAndAddControl(IndexValue + '_viewresidency');
                            messageAlertEngine.callAlertMessage('updatedResidencyDetails' + IndexValue, "Residency/Internship/fellowship Details Updated Successfully !!!!", "success", true);
                        }
                        $scope.datePickerReset();
                        FormReset($formDataResidency);
                    } else {
                        messageAlertEngine.callAlertMessage('ErrorInResidencyDetails' + IndexValue, "", "danger", true);
                        $scope.ResidencyDetailsErrorListForView = data.status.split(",");
                    }
                },
                error: function (e) {
                    messageAlertEngine.callAlertMessage('ErrorInResidencyDetails' + IndexValue, "", "danger", true);
                    $scope.ResidencyDetailsErrorListForView = "Sorry for Inconvenience !!!! Please Try Again Later...";
                }
            });
        }

        loadingOff();
    };


    //======================Post graduate Training/CME==========================


    $scope.CertificationCMEViewModel = [];


    // rootScoped on emitted value catches the value for the model and insert to get the old data for PostGratuste Training/CME Details
    $rootScope.$on('CMECertifications', function (event, val) {

        for (var i = 0; i < val.length ; i++) {
            if (val[i] != null) {
                //val[i].StartDate = ConvertDateFormat(val[i].StartDate);
                //val[i].EndDate = ConvertDateFormat(val[i].EndDate);
                //val[i].ExpiryDate = ConvertDateFormat(val[i].ExpiryDate);
                val[i].City = $rootScope.ConvertCity(val[i].City);
                $scope.CertificationCMEViewModel.push(val[i]);
            }
        }


    });

    $scope.saveCertificate = function (certificationCMEViewModel, IndexValue) {
        loadingOn();
        var validationStatus;
        var url;
        var myData = {};
        var $formData;

        if ($scope.visibilityControl == 'addcertificationCMEViewModel') {
            //Add Details - Denote the URL
            validationStatus = $('#newCertificateFormDiv').find('form').valid();
            $formData = $('#newCertificateFormDiv').find('form');
            url = rootDir + "/Profile/EducationHistory/AddCMECertificationAsync?profileId=" + profileId;
        }
        else if ($scope.visibilityControl == (IndexValue + '_editcertificationCMEViewModel')) {
            //Update Details - Denote the URL
            validationStatus = $('#certificateEditDiv' + IndexValue).find('form').valid();
            $formData = $('#certificateEditDiv' + IndexValue).find('form');
            url = rootDir + "/Profile/EducationHistory/UpdateCMECertificationAsync?profileId=" + profileId;
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
                        $scope.ErrorInCMEDetails = false;
                        if ($scope.visibilityControl == 'addcertificationCMEViewModel') {
                            data.CMEDetails.StartDate = ConvertDateFormat(data.CMEDetails.StartDate);
                            data.CMEDetails.EndDate = ConvertDateFormat(data.CMEDetails.EndDate);
                            data.CMEDetails.ExpiryDate = ConvertDateFormat(data.CMEDetails.ExpiryDate);
                            $scope.CertificationCMEViewModel.push(data.CMEDetails);
                            $rootScope.operateCancelControl();
                            messageAlertEngine.callAlertMessage("addedNewCMEDetails", "New PostGraduate Training/CME Details Added Successfully!!!", "success", true);
                        }
                        else if ($scope.visibilityControl == (IndexValue + '_editcertificationCMEViewModel')) {
                            $scope.CMECertificationDetailPendingRequest = true;
                            data.CMEDetails.StartDate = ConvertDateFormat(data.CMEDetails.StartDate);
                            data.CMEDetails.EndDate = ConvertDateFormat(data.CMEDetails.EndDate);
                            data.CMEDetails.ExpiryDate = ConvertDateFormat(data.CMEDetails.ExpiryDate);
                            $scope.CertificationCMEViewModel[IndexValue] = data.CMEDetails;
                            $rootScope.operateViewAndAddControl(IndexValue + '_viewcertificationCMEViewModel');
                            messageAlertEngine.callAlertMessage('updatedCMEDetails' + IndexValue, "PostGraduate Training/CME Details Updated Successfully !!!!", "success", true);
                        }
                        $scope.datePickerReset();
                        myData = data;
                        FormReset($formData);
                    } else {
                        messageAlertEngine.callAlertMessage('ErrorInCMEDetails' + IndexValue, "", "danger", true);
                        $scope.CMEDetailsErrorList = data.status.split(",");
                    }
                },
                error: function (e) {
                    messageAlertEngine.callAlertMessage('ErrorInCMEDetails' + IndexValue, "", "danger", true);
                    $scope.CMEDetailsErrorList = "Sorry for Inconvenience !!!! Please Try Again Later...";
                }
            });
        }

        $rootScope.$broadcast('UpdateCMECertificationsDoc', myData);

        loadingOff();
    };

    //To initiate Removal Confirmation Modal
    $scope.initCertificationDetailWarning = function (certificationCMEViewModel) {
        if (angular.isObject(certificationCMEViewModel)) {
            $scope.tempCertificationDetail = certificationCMEViewModel;
        }
        $('#certificationDetailWarningModal').modal();
    };

    $scope.removeCertificationDetail = function (CertificationCMEViewModel) {
        var validationStatus = false;
        var url = null;
        var myData = {};
        var $formData = null;
        $scope.isRemoved = true;
        $formData = $('#removeCertificationDetail');
        url = rootDir + "/Profile/EducationHistory/RemoveCertificationDetailAsync?profileId=" + profileId;
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
                    if (data.status == "true") {
                        var obj = $filter('filter')(CertificationCMEViewModel, { CMECertificationID: data.certificationCMEViewModel.CMECertificationID })[0];
                        CertificationCMEViewModel.splice(CertificationCMEViewModel.indexOf(obj), 1);
                        if ($scope.dataFetchedCC == true) {
                            obj.HistoryStatus = 'Deleted';
                            $scope.CMECertificationArray.push(obj);
                        }
                        $scope.isRemoved = false;
                        $('#certificationDetailWarningModal').modal('hide');
                        $rootScope.operateCancelControl('');
                        myData = data;
                        messageAlertEngine.callAlertMessage("addedNewCMEDetails", "Residency/Internship/Fellowship Detail Removed successfully.", "success", true);
                    } else {
                        $('#certificationDetailWarningModal').modal('hide');
                        messageAlertEngine.callAlertMessage("removeCMEDetails", data.status, "danger", true);
                        $scope.errorCMEDetails = "Sorry for Inconvenience !!!! Please Try Again Later...";
                    }
                },
                error: function (e) {

                }
            });
        }

        $rootScope.$broadcast('RemoveCMECertificationsDoc', myData);

    };

    $scope.datePickerReset = function () {

        try {
            $scope.tempObject.StartDate = new Date();
            $scope.tempObject.EndDate = new Date();
            $scope.tempSecondObject.StartDate = new Date();
            $scope.tempSecondObject.EndDate = new Date();
            $scope.tempObject.ExpiryDate = new Date();
            $scope.tempObject.ECFMGIssueDate = new Date();
        }
        catch (e) { }

    };
    $rootScope.EducationHistoryLoaded = true;
    $scope.dataLoaded = false;
    $rootScope.$on('EducationHistory', function () {
        if (!$scope.dataLoaded) {
            $rootScope.EducationHistoryLoaded = false;
            //console.log("Getting data....");
            $http({
                method: 'GET',
                url: rootDir + '/Profile/MasterProfile/GetEducationHistoriesProfileDataAsync?profileId=' + profileId
            }).success(function (data, status, headers, config) {
                //console.log(data);
                try {
                    for (key in data) {
                        //console.log(key);
                        $rootScope.$emit(key, data[key]);
                        //call respective controller to load data (PSP)
                    }

                    $rootScope.EducationHistoryLoaded = true;
                    $rootScope.$broadcast("LoadRequireMasterDataEducationHistory");
                } catch (e) {
                    //console.log("error getting data back");
                    $rootScope.EducationHistoryLoaded = true;
                }

            }).error(function (data, status, headers, config) {
                //console.log(status);
                $rootScope.EducationHistoryLoaded = true;
            });
            $scope.dataLoaded = true;
        }
    });

}]);