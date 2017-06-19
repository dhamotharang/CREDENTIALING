/// <reference path="../../Shared/Data/Languages.js" />
//----------------------Angular Module--------------------
function showLocationList(ele) {
    $(ele).parent().find(".ProviderTypeSelectAutoList").first().show();
}
var FacilityInformationApp = angular.module("FacilityInformationApp", ['ui.bootstrap', 'timepickerPop']);
//----------------------- AHC Autocomplete search cum dropDown ----------------
FacilityInformationApp.directive('searchdropdown', function () {
    return {
        restrict: 'AE',
        link: function (scope, element, attr) {
            element.bind('focus', function () {
                element.parent().find(".ProviderTypeSelectAutoList").show();
            });
        }
    };
});

// service responsible for getting all active profile master data
FacilityInformationApp.service('masterDataService', ['$http', '$q', function ($http, $q) {

    this.getMasterData = function (URL) {
        return $http.get(URL).then(function (value) { return value.data; });
    };

    this.getPractitioners = function (URL, level, profileId) {
        return $http({
            url: URL,
            method: "POST",
            data: { practitionerLevel: level, profileID: profileId }
        }).then(function (value) { return value.data; });
    };

    this.getProviderLevels = function (URL, profileId) {
        return $http({
            url: URL,
            method: "POST",
            data: { profileID: profileId }
        }).then(function (value) { return value.data; });
    };
}]);

//---------------------------- service for get location ----------------------
FacilityInformationApp.service('locationService', ['$http', function ($http) {
    this.getLocations = function (QueryString) {
        return $http.get(rootDir + "/Location/GetCities?city=" + QueryString)
        .then(function (response) { return response.data; });
    };
}]);

FacilityInformationApp.service('masterDataService', ['$http', '$q', function ($http, $q) {

    this.getMasterData = function (URL) {
        return $http.get(URL).then(function (value) { return value.data; });
    };

}]);

FacilityInformationApp.service('messageAlertEngine', ['$rootScope', '$timeout', function ($rootScope, $timeout) {

    $rootScope.messageDesc = "";
    $rootScope.activeMessageDiv = "";
    $rootScope.messageType = "";

    var animateMessageAlertOff = function () {
        $rootScope.closeAlertMessage();
    };

    this.callAlertMessage = function (calledDiv, msg, msgType, dismissal) { //messageAlertEngine.callAlertMessage('updateHospitalPrivilege' + IndexValue, "Data Updated Successfully !!!!", "success", true);                            
        $rootScope.activeMessageDiv = calledDiv;
        $rootScope.messageDesc = msg;
        $rootScope.messageType = msgType;
        if (dismissal) {
            $timeout(animateMessageAlertOff, 5000);
        }
    }

    $rootScope.closeAlertMessage = function () {
        $rootScope.messageDesc = "";
        $rootScope.activeMessageDiv = "";
        $rootScope.messageType = "";
    }

    //$rootScope.selectedAction1 = 1;
    this.setActionID1 = function (id) {

        sessionStorage.setItem('key', id);
    };

}])

FacilityInformationApp.filter("AMPM", function () {
    return function (value) {
   //     var returnValue = value.toDateString();
        if (value == 'Not Available' || value == 'Invalid Date' || value == 'Day Off') { return 'Day Off'; }
        if (!value) { return ''; }
        if (angular.isDate(value)) {
            value = value.getHours() + ":" + value.getMinutes();
        }
        var time = value.split(":");
        var hours = time[0];
        var minutes = time[1];
        var ampm = hours >= 12 ? 'PM' : 'AM';
        hours = hours % 12;
        hours = hours ? hours : 12; // the hour '0' should be '12'
        minutes = minutes.length == 1 ? minutes < 10 ? '0' + minutes : minutes : minutes;
        var strTime =  hours + ':' + minutes + ' ' + ampm;
        return strTime;
       
    }
})
//=========================== Controller declaration ==========================
FacilityInformationApp.controller('FacilityInformationController', ['$scope', '$http', '$filter', 'messageAlertEngine', 'masterDataService', '$scope', '$rootScope', 'masterDataService', 'locationService', function ($scope, $http, $filter, messageAlertEngine, masterDataService, $scope, $rootScope, masterDataService, locationService) {
    $scope.FaciltyInformations = [];
    $rootScope.tempObject = {};
    $scope.Facility = [];
    $scope.IsLoadingData = true;
    masterDataService.getMasterData(rootDir + "/MasterDataNew/GetAllMasterFacilityInformation").then(function (FaciltyInformations) {
        $scope.IsLoadingData = false;
        $scope.FaciltyInformations = FaciltyInformations;
        $scope.tempFaciltyInformations = FaciltyInformations;
    });
    masterDataService.getMasterData(rootDir + "/MasterDataNew/GetAllAccessibilityQuestions").then(function (GetAllAccessibilityQuestions) {
        $scope.masterAccessibilityQuestions = GetAllAccessibilityQuestions;
    });

    masterDataService.getMasterData(rootDir + "/MasterDataNew/GetAllCities").then(function (Locations) {
        $scope.Locations = Locations;
    });

    masterDataService.getMasterData(rootDir + "/MasterDataNew/GetAllMidLevelPractitioners").then(function (Providers) {
        $scope.MidLevelProviders = Providers;
        for(var m=0;m<$scope.MidLevelProviders.length;m++)
        {
            $scope.MidLevelProviders[m].PersonalDetail.FullName = $scope.MidLevelProviders[m].PersonalDetail.FirstName + " " + (($scope.MidLevelProviders[m].PersonalDetail.MiddleName != null)?$scope.MidLevelProviders[m].PersonalDetail.MiddleName:"") + " " + $scope.MidLevelProviders[m].PersonalDetail.LastName;
        }
    });


    $scope.AddMode = false;
    $scope.EditMode = false;
    $scope.ViewMode = false;
    $scope.ListMode = true;
    $scope.currentIndex = null;

    $scope.changeTimeAmPm = function (value) {
        var returnValue = value.toDateString();
        if (value == 'Not Available' || value == 'Invalid Date' || value == 'Day Off') { return 'Day Off'; }
        if (!value) { return ''; }
        if (angular.isDate(value)) {
            value = value.getHours() + ":" + value.getMinutes();
        }
        var time = value.split(":");
        var hours = time[0];
        var minutes = time[1];
        var ampm = hours >= 12 ? 'PM' : 'AM';
        hours = hours % 12;
        hours = hours ? hours : 12; // the hour '0' should be '12'
        minutes = minutes.length == 1 ? minutes < 10 ? '0' + minutes : minutes : minutes;
        var strTime = returnValue + " " + hours + ':' + minutes + ' ' + ampm;
        return strTime;
    }

    $scope.operateAddControlPracLoc = function (sectionValue) {
        //$scope.ListMode = false;
        //$scope.AddMode = true;
        $scope.tempObject={};
        $scope.ViewMode = false;
        $scope.ListMode = false;
        $scope.EditMode = true;
        $scope.displayMe = "";
        $scope.errorMsg = false;
       
        $rootScope.tempSecondObject = {};
        //$scope.tempSecondObject.StartDate = "";
        //$scope.tempSecondObject.GeneralCorrespondenceYesNoOption = "";
        //$scope.tempSecondObject.PracticeExclusivelyYesNoOption = "";
        //$scope.tempSecondObject.PrimaryTax = "";
        //$scope.tempSecondObject.PrimaryYesNoOption = "";
        //$scope.tempSecondObject.CurrentlyPracticingYesNoOption = "";
        $scope.visibilityControlPracLoc = sectionValue;
        $('.field-validation-error').removeClass('field-validation-error').addClass('field-validation-valid');
        $('.input-validation-error').removeClass('input-validation-error').addClass('valid');
        $scope.tempObject.FacilityDetail = { Language: { NonEnglishLanguages: [] } };
        $rootScope.tempObject.PracticeOfficeHours = {};
        $scope.MidLevelProviderPractitioner = {};
        //$("MIDLEVELPRACT").click(function () {
        //    this.value = '';
        //});
        $("#MIDLEVELPRACT").value = '';
           
    }

    $scope.Cancel = function () {
        $scope.ListMode = true;
        $scope.AddMode = false;
        $scope.EditMode = false;
        $("#MIDLEVELPRACT").value = '';
    }
    $scope.CancelView = function () {
        $scope.ListMode = true;
        $scope.ViewMode = false;

    }

    $scope.editFacilityInformation = function (obj, index) {
        $scope.tempid = obj.FacilityID;
        $scope.showMeridian = true;
        $scope.currentIndex = index;
        $scope.tempObject = angular.copy(obj);
        $scope.tempObject.PracticeDays = $scope.tempObject.FacilityDetail.PracticeOfficeHour.PracticeDays;
        for (var i = 0; i < $scope.tempObject.PracticeDays.length; i++) {
            for (var j = 0; j < $scope.tempObject.PracticeDays[i].DailyHours.length; j++) {
                var start = $scope.tempObject.PracticeDays[i].DailyHours[j].StartTime;
                var temp1;
                var a = new Date();
                a=a.setHours(start.split(':')[0]);
                temp1 = new Date(a);
                a=temp1.setMinutes(start.split(':')[1]);
                temp1 = new Date(a);
                $scope.tempObject.PracticeDays[i].DailyHours[j].StartTime = temp1;

                var end = $scope.tempObject.PracticeDays[i].DailyHours[j].EndTime;
                var temp2;
                var a = new Date();
                a = a.setHours(end.split(':')[0]);
                temp2 = new Date(a);
                a = temp2.setMinutes(end.split(':')[1]);
                temp2 = new Date(a);
                $scope.tempObject.PracticeDays[i].DailyHours[j].EndTime = temp2;
            }
        }
        
        $scope.ViewMode = false;
        $scope.AddMode = false;
        $scope.ListMode = false;
        $scope.EditMode = true;
        $scope.practitioner = '';
    }
    //Loading Div
    $scope.$on('httpCallStarted', function (e) {
        $("#LoadingModal").modal({
            backdrop: "static",
            show: true
        })
        //$("body > :not(#LoadingModal)").css("opacity", "0.4");
        $("body > div.modal-backdrop.fade.in").css("opacity", "0");
    });
    $scope.$on('httpCallStopped', function (e) {
        $("#LoadingModal").modal('hide');
        $('.modal-backdrop').remove();
        //$("body > :not(#LoadingModal)").css("opacity", "1");
        $("body > div.modal-backdrop.fade.in").css("opacity", "0");
    });

    $scope.$broadcast('httpCallStarted');

    //-------------------------------- Master Data get All Code -------------------------------


    $scope.tempLanguages = angular.copy(Languages);


    $scope.showLanguageList = function (divToBeDisplayed) {
        $("#" + divToBeDisplayed).show();
    };

    $scope.selectedLocation = function (location) {
        $scope.setAddressModels(location);
        $(".ProviderTypeSelectAutoList").hide();
    };

    $scope.setAddressModels = function (location) {
        $scope.tempObject.City = location.City;
        $scope.tempObject.State = location.State;
        $scope.tempObject.Country = location.Country;

    }

    $scope.SelectLanguage = function (selectedLanguage) {

        $scope.tempObject.FacilityDetail.Language.NonEnglishLanguages.push({
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
        $scope.tempObject.FacilityDetail.Language.NonEnglishLanguages.splice($scope.tempObject.FacilityDetail.Language.NonEnglishLanguages.indexOf(language), 1);

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

    //Convert the date from database to normal
    $scope.ConvertDateFormat = function (value) {
        var returnValue = value;
        var dateData = "";
        try {
            if (value.indexOf("/Date(") == 0) {
                returnValue = new Date(parseInt(value.replace("/Date(", "").replace(")/", ""), 10));
                var day = returnValue.getDate() < 10 ? '0' + returnValue.getDate() : returnValue.getDate();
                var tempo = returnValue.getMonth() + 1;
                var month = tempo < 10 ? '0' + tempo : tempo;
                var year = returnValue.getFullYear();
                dateData = month + "-" + day + "-" + year;
            }
            return dateData;
        } catch (e) {
            return dateData;
        }
        return dateData;
    };
    ////Convert the date from database to normal
    $scope.ConvertDateFormat1 = function (value) {
        from = value.split("-");
        var dateData = "";
        var year = from[0];
        var mnth = from[1];
        var date = from[2].slice(0, 2);
        dateData = mnth + "-" + date + "-" + year;
        return dateData;
    };
    $scope.CurrentPage = [];
    //-------------------------- angular bootstrap pagger with custom-----------------
    $scope.maxSize = 5;
    $scope.bigTotalItems = 0;
    $scope.bigCurrentPage = 1;

    //-------------------- page change action ---------------------
    $scope.pageChanged = function (pagnumber) {
        $scope.bigCurrentPage = pagnumber;
    };


    //Active and De active Modal
    $scope.FacilityStatus = function (Facility, stat, index) {
        $scope.StatusValue = stat;
        StatusIndexValue = $scope.Facility.indexOf(Facility);
        if ($scope.StatusValue == 'Active') {

            $scope.ModalStatusValue = 'Inactive';
        }
        else { $scope.ModalStatusValue = 'Active'; }

    };

    $scope.ChangeStatus = function () {
        if ($scope.StatusValue == 'Active') {
            $scope.Facility[StatusIndexValue].Status = 'Inactive';

            var updateData = {
                FacilityID: $scope.Facility[StatusIndexValue].FacilityID,
                FacilityName: $scope.Facility[StatusIndexValue].FacilityName,
                Status: $scope.Facility[StatusIndexValue].Status,
                StatusType: "Inactive",
            };
            $scope.Facility[StatusIndexValue].StatusType = 0;
        }
        else {
            $scope.Facility[StatusIndexValue].Status = 'Active';

            var updateData = {
                FacilityID: $scope.Facility[StatusIndexValue].FacilityID,
                FacilityName: $scope.Facility[StatusIndexValue].FacilityName,
                Status: $scope.Facility[StatusIndexValue].Status,
                StatusType: "Active",
            };
            $scope.Facility[StatusIndexValue].StatusType = 1;
        }




        $http.post(rootDir + '/MasterData/UpdateFacilityAsync', updateData).
            success(function (data, status, headers, config) {
                try {
                    //----------- success message -----------
                    if (data.status == "true") {
                        messageAlertEngine.callAlertMessage("Facility", "Facility Details Updated Successfully !!!!", "success", true);
                        data.facility.LastModifiedDate = $scope.ConvertDateFormat(data.facility.LastModifiedDate);
                        $scope.Facility[StatusIndexValue] = angular.copy(data.facility);
                        $scope.reset();

                    }
                    else {
                        messageAlertEngine.callAlertMessage("FacilityError", "Sorry Unable To Update Facility !!!!", "danger", true);
                        $scope.ValidationError = data.status;
                    }
                } catch (e) {

                }

            }).
            error(function (data, status, headers, config) {
                //----------- error message -----------
                $scope.ValidationError = data.status;
                messageAlertEngine.callAlertMessage("FacilityError", "Sorry Unable To Update Facility !!!!", "danger", true);
            });


    };

    //-------------- current page change Scope Watch ---------------------
    $scope.$watch('bigCurrentPage', function (newValue, oldValue) {
        $scope.CurrentPage = [];
        var startIndex = (newValue - 1) * 10;
        var endIndex = startIndex + 9;
        if ($scope.FaciltyInformations) {
            for (startIndex; startIndex <= endIndex ; startIndex++) {
                if ($scope.FaciltyInformations[startIndex]) {
                    $scope.CurrentPage.push($scope.FaciltyInformations[startIndex]);
                } else {
                    break;
                }
            }
        }
    });

    //-------------- License Scope Watch ---------------------
    $scope.$watchCollection('FaciltyInformations', function (newValue, oldValue) {
        if (newValue) {
            $scope.bigTotalItems = newValue.length;

            $scope.CurrentPage = [];
            $scope.bigCurrentPage = 1;

            var startIndex = ($scope.bigCurrentPage - 1) * 10;
            var endIndex = startIndex + 9;

            for (startIndex; startIndex <= endIndex ; startIndex++) {
                if ($scope.FaciltyInformations[startIndex]) {
                    $scope.CurrentPage.push($scope.FaciltyInformations[startIndex]);
                } else {
                    break;
                }
            }
        }
    });

    $scope.tempFacility = {};
    $scope.newFacility = {};

    var Month = new Date().getMonth() + 1;
    var _month = Month < 10 ? '0' + Month : Month;
    var _date = new Date().getDate() < 10 ? '0' + new Date().getDate() : new Date().getDate();
    var _year = new Date().getFullYear();



    $scope.addMidLevelPractitioner = true;



    $scope.getTemplate = function (facility) {
        if (facility.FacilityID == $scope.tempFacility.FacilityID)
            return 'editFacility';
        else
            return 'displayFacility';
    };


    //------------------- Reset Facility ----------
    $scope.reset = function () {
        $scope.tempFacility = {};
        $scope.add = false;
        $scope.disableAdd = false;
        $scope.disableAI = false;
        $scope.disableEdit = false;
        $scope.resetPracticeDaysList();
        $scope.existErr = "";
    };
    //-------------------- Add Facility ----------
    $scope.addFacility = function () {
        var filtered = $scope.filtered;
        $scope.AddNewBtn = true;
        $scope.existErr = "";
        $scope.disableEdit = false;
        $scope.disableAdd = true;
        $scope.disableAI = true;

        $scope.add = true;
        $scope.disableAdd = true;
        $scope.SaveOrUpdatebtn = true;
        $scope.resetPracticeDaysList();
        $scope.tempObject.FacilityDetail.Language.NonEnglishLanguages.splice(0, 0);
        $scope.Facility.splice(0, 0, angular.copy(temp));
        $scope.tempFacility = angular.copy(temp);
    };

    $scope.facilities = [];
    $scope.$on("ArrayResize", function (event, args) {
        if (args != null && angular.isObject(args.FacilityDetail)) {
            var TemporaryOBJ = [];
            for (i in $scope.masterServiceQuestions) {

                for (j in args.FacilityDetail.Service.FacilityServiceQuestionAnswers) {
                    if ($scope.masterServiceQuestions[i].FacilityServiceQuestionID == args.FacilityDetail.Service.FacilityServiceQuestionAnswers[j].FacilityServiceQuestionId) {
                        TemporaryOBJ.push(args.FacilityDetail.Service.FacilityServiceQuestionAnswers[j]);
                    }
                }

            }
            args.FacilityDetail.Service.FacilityServiceQuestionAnswers = [];

            args.FacilityDetail.Service.FacilityServiceQuestionAnswers = angular.copy(TemporaryOBJ);
        }
    })


    $scope.IsValidIndex = function (index) {

        var startIndex = ($scope.bigCurrentPage - 1) * 10;
        var endIndex = startIndex + 9;

        if (index >= startIndex && index <= endIndex)
            return true;
        else
            return false;

    }




    // Calling master data service to get all Service Questions
    masterDataService.getMasterData(rootDir + "/MasterDataNew/GetAllServiceQuestions").then(function (GetAllServiceQuestions) {
        $scope.masterServiceQuestions = GetAllServiceQuestions;
    });

    // Calling master data service to get all Practice Types
    masterDataService.getMasterData(rootDir + "/MasterDataNew/GetAllPracticeTypes").then(function (GetAllPracticeTypes) {
        $scope.masterPracticeTypes = GetAllPracticeTypes;
    });

    // Calling master data service to get all Country Code
    //masterDataService.getMasterData(rootDir + "/MasterData/GetAllCountryCode").then(function (GetAllCountryCodes) {
    $scope.CountryDialCodes = countryDailCodes;  //});
    

    $scope.saveFacilityInformaton = function (typeOfSave, index) {

        var validationStatus = false;
        var url = null;
        var $formData = null;

        $formData = $('#newFacilityDataForm_New');
        url = rootDir + "/MasterDataNew/AddFacilityAsync";

        $scope.ResetFormForValidation($formData);


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

                        if (typeOfSave == 'Update') {
                            $scope.PracticeLocationDetails[index].Facility = data.facility;
                            $scope.operateAddControlPracLoc(index + '_viewPracticeLOcation');
                            for (var i = 0; i < $scope.PracticeLocationDetails.length; i++) {
                                if ($scope.PracticeLocationDetails[i].FacilityId == data.facility.FacilityID) {
                                    $scope.PracticeLocationDetails[i].Facility = data.facility;
                                    if (typeof $scope.PracticeLocationDetails[i].OfficeHour.AnyTimePhoneCoverage == 'undefined') {
                                        $scope.PracticeLocationDetails[i].OfficeHour.PracticeDays = data.facility.FacilityDetail.PracticeOfficeHour.PracticeDays;

                                        $scope.PracticeLocationtemporary = angular.copy(TimeConversionForOfficeHour($scope.PracticeLocationDetails[i].OfficeHour));
                                        $scope.PracticeLocationtemporary.PracticeDays = [];
                                        $scope.PracticeLocationtemporary.PracticeDays = $scope.OriginalPracticeDays;

                                    }
                                }
                            }

                            count1 = 0;
                            $scope.operateCancelControl('');
                            messageAlertEngine.callAlertMessage("updatedFacility", "Facility Information updated successfully !!!!", "success", true);
                        }
                        else {
                            count1 = 0;
                            $scope.facilities.push(data.facility);
                            $scope.FaciltyInformations.push(data.facility);
                            $scope.operateAddControlPracLoc('addPracticeLocation');
                            messageAlertEngine.callAlertMessage("addedNewFacility", "New Facility Information saved successfully !!!!", "success", true);

                            $scope.tempSecondObject.FacilityId = data.facility.FacilityID;
                            $scope.tempSecondObject.PracticeLocationCorporateName = data.facility.Name;
                        }
                        $scope.ViewMode = false;
                        //    $scope.AddMode = true;
                        $scope.ListMode = true;
                        $scope.EditMode = false;
                        FormReset($formData);

                    } else {
                        messageAlertEngine.callAlertMessage("facilityDataErrorMsg", data.status, "danger", true);
                    }
                },
                error: function (e) {

                }
            });
            $scope.ListMode = true;
            $scope.AddMode = false;

        }
    };

    $scope.UpdateFacilityInformaton = function (typeOfSave, index) {
        var validationStatus = false;
        var url = null;
        var $formData = null;
        $formData = $('#newFacilityDataForm_New');
        url = rootDir + "/MasterDataNew/UpdateFacilityAsync";
        //$scope.ResetFormForValidation($formData);
        validationStatus = $formData.valid();
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
                        if (typeOfSave == 'Update') {
                           // $scope.FaciltyInformations = $scope.tempFaciltyInformations;
                            data.facility.LastModifiedDate = $scope.ConvertDateFormat(data.facility.LastModifiedDate);
                            //$scope.Facility.push(data.facility);
                            for (var i in $scope.FaciltyInformations) {
                                if ($scope.FaciltyInformations[i].FacilityID == $scope.tempid) {
                                    $scope.FaciltyInformations[i] = {};
                                    $scope.FaciltyInformations[i][i] = data.facility;
                                }
                            }
                            $scope.FaciltyInformations[index] = data.facility;
                            $scope.operateAddControlPracLoc(index + '_viewPracticeLOcation');
                        }
                        $scope.reset();
                        $scope.ViewMode = false;
                       // $scope.AddMode = true;
                        $scope.ListMode = true;
                        $scope.EditMode = false;
                        messageAlertEngine.callAlertMessage("facility", "facility Details Updated Successfully !!!!", "success", true);


                    } else {
                        messageAlertEngine.callAlertMessage("facilityDataErrorMsg", data.status, "danger", true);
                    }
                },
                error: function (e) {

                }
            });
        }
    };

    $scope.viewFacilityInformation = function (obj) {

        $scope.tempSecondObject = obj;
        $scope.ViewMode = true;
        $scope.AddMode = false;
        $scope.ListMode = false;
        $scope.EditMode = false;
    }
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

    /* Facility Information Action Methods End*/

    //------------------------------------------------------------------------------------------------------------------//
    //-------------------------------------------------- Office Hours --------------------------------------------------//
    //------------------------------------------------------------------------------------------------------------------//
    var TimeTempo1 = new Date();
    var TimeTempo2 = new Date();
    TimeTempo1.setHours(8);
    TimeTempo1.setMinutes(30);
    var tempStartTime = TimeTempo1;
    TimeTempo2.setHours(16);
    TimeTempo2.setMinutes(30);
    var tempEndTime = TimeTempo2;

    $scope.OriginalPracticeDays = [
        { DayName: "Monday", DayOfWeek: 0, DayOff: 'NO', DailyHours: [{ StartTime: tempStartTime, EndTime: tempEndTime }] },
        { DayName: "Tuesday", DayOfWeek: 1, DayOff: 'NO', DailyHours: [{ StartTime: tempStartTime, EndTime: tempEndTime }] },
        { DayName: "Wednesday", DayOfWeek: 2, DayOff: 'NO', DailyHours: [{ StartTime: tempStartTime, EndTime: tempEndTime }] },
        { DayName: "Thursday", DayOfWeek: 3, DayOff: 'NO', DailyHours: [{ StartTime: tempStartTime, EndTime: tempEndTime }] },
        { DayName: "Friday", DayOfWeek: 4, DayOff: 'NO', DailyHours: [{ StartTime: tempStartTime, EndTime: tempEndTime }] },
        { DayName: "Saturday", DayOfWeek: 5, DayOff: 'YES', DailyHours: [{ StartTime: tempStartTime, EndTime: tempEndTime }] },
        { DayName: "Sunday", DayOfWeek: 6, DayOff: 'YES', DailyHours: [{ StartTime: tempStartTime, EndTime: tempEndTime }] }
    ];

    $scope.resetPracticeDaysList = function () {
        $scope.tempObject.PracticeDays = $scope.OriginalPracticeDays;

    };

    $scope.setFacilityPracticeDays = function (practiceLocationDetail, index) {

        var tempArray = [];
        if (!$scope.PracticeLocationDetails[index].Facility.FacilityDetail.PracticeOfficeHour) {
            for (var j in $scope.PracticeLocationDetails[index].Facility.FacilityDetail.PracticeOfficeHour.PracticeDays) {
                tempArray.push($scope.PracticeLocationDetails[index].Facility.FacilityDetail.PracticeOfficeHour.PracticeDays[j].PracticeDailyHourID);
            }
            $scope.PracticeLocationDetails[index].Facility.FacilityDetail.PracticeOfficeHour = {};
            $scope.PracticeLocationDetails[index].Facility.FacilityDetail.PracticeOfficeHour.PracticeDays = angular.copy($scope.OriginalPracticeDays);
            for (var k in tempArray) {
                $scope.PracticeLocationDetails[index].Facility.FacilityDetail.PracticeOfficeHour.PracticeDays[k] = tempArray[k];
            }
        }
        else {
            for (temporary in $scope.PracticeLocationDetails[index].Facility.FacilityDetail.PracticeOfficeHour.PracticeDays) {
                for (d in $scope.PracticeLocationDetails[index].Facility.FacilityDetail.PracticeOfficeHour.PracticeDays[temporary].DailyHours) {
                    $scope.PracticeLocationDetails[index].Facility.FacilityDetail.PracticeOfficeHour.PracticeDays[temporary].DailyHours[d].StartTime = convertTEmp($scope.PracticeLocationDetails[index].Facility.FacilityDetail.PracticeOfficeHour.PracticeDays[temporary].DailyHours[d].StartTime);
                    $scope.PracticeLocationDetails[index].Facility.FacilityDetail.PracticeOfficeHour.PracticeDays[temporary].DailyHours[d].EndTime = convertTEmp($scope.PracticeLocationDetails[index].Facility.FacilityDetail.PracticeOfficeHour.PracticeDays[temporary].DailyHours[d].EndTime);
                }
            }
        }
        $scope.tempObject.PracticeDays = practiceLocationDetail.Facility.FacilityDetail.PracticeOfficeHour.PracticeDays;

    };

    $scope.addDailyHour = function (DailyHours) {
        var startTime = new Date();
        var endTime = new Date();
        DailyHours.push({ StartTime: startTime, EndTime: endTime });

    };
    $scope.addDailyHourMain = function (id) {
        var startTime = new Date();
        var endTime = new Date();
        $scope.PracticeLocationtemporary.PracticeDays.filter(function (PracticeDays) { return PracticeDays.DayOfWeek == id.DayOfWeek })[0].DailyHours.push({ StartTime: startTime, EndTime: endTime });

    };
    $scope.removeDailyHour = function (DailyHours, index) {
        DailyHours.splice(index, 1);
    };

    $scope.dayOffToggel = function (PracticeDay) {

        var changeTimeForStartTime = [];
        var changeTimeForEndTime = [];

        PracticeDay.DayOff = PracticeDay.DayOff == 'YES' ? 'NO' : 'YES';
        if (PracticeDay.DayOff == 'YES') {
            PracticeDay.DailyHours.splice(1, PracticeDay.DailyHours.length);
        }
        if ((PracticeDay.DayOff == 'NO' && PracticeDay.DailyHours[0].StartTime == 'Invalid Date') || (PracticeDay.DayOff == 'NO' && PracticeDay.DailyHours[0].StartTime == 'Day Off') || (PracticeDay.DayOff == 'NO' && PracticeDay.DailyHours[0].StartTime == 'Not Available')) {

            var newDate = new Date();
            PracticeDay.DailyHours[0].EndTime = newDate;
            PracticeDay.DailyHours[0].StartTime = newDate;

        }
        else if (PracticeDay.DayOff == 'NO' && PracticeDay.DailyHours[0].StartTime != 'Invalid Date') {
            return;
        }



    };
    $scope.Convert42HoursTimeFormat = function (value, data) {
        if (data == 'NO') {
            if (!isNaN(Date.parse(value))) {
                var Hour = value.getHours() < 10 ? '0' + value.getHours() : value.getHours();
                var Min = value.getMinutes() < 10 ? '0' + value.getMinutes() : value.getMinutes();
                return Hour + ":" + Min;
            }
            else {
                return value;
            }
            
        }

        return 'Day Off';
    }

    $scope.validateDailyHours = function (PracticeDays, parent, subsection, index, typeOfSave) {
       
        index = $scope.currentIndex;
        $scope.tempLanguages = [];
        index = $scope.currentIndex;
        $scope.tempLanguages = angular.copy(Languages);
        $scope.hideClock();
        var status = true;

        for (practiceDay in PracticeDays) {
            var prevStartHour = "";
            var prevStartMin = "";
            var prevEndHour = "";
            var prevEndMin = ""
            for (dailyHour in PracticeDays[practiceDay].DailyHours) {
                if (!$('#startTime_' + practiceDay + dailyHour).prop('disabled') || !$('#endTime_' + practiceDay + dailyHour).prop('disabled')) {
                    var startTime = $scope.Convert42HoursTimeFormat(PracticeDays[practiceDay].DailyHours[dailyHour].StartTime, PracticeDays[practiceDay].DayOff);
                    var endTime = $scope.Convert42HoursTimeFormat(PracticeDays[practiceDay].DailyHours[dailyHour].EndTime, PracticeDays[practiceDay].DayOff);
                    if (startTime != 'Day Off' && endTime != 'Day Off') {
                        var startHour = parseInt(startTime.split(':')[0]);
                        var startMin = parseInt(startTime.split(':')[1]);

                        var endHour = parseInt(endTime.split(':')[0]);
                        var endMin = parseInt(endTime.split(':')[1]);



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
        else if (status && subsection == 'Updatefacility') {
            $scope.UpdateFacilityInformaton(typeOfSave, index);
        }
        $scope.PracticeLocationPendingRequest = true;
        $("#MIDLEVELPRACT").value = '';
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

                        $scope.PracticeLocationDetails[index].OfficeHour = data.providerPracticeOfficeHours;
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

    };

    $scope.filterData = function () {
        $scope.pageChanged(1);
    }    


    //------------------------------------------------------------------------------------------------------------------//
    //----------------------------------------------- Facility Languages -----------------------------------------------//
    //------------------------------------------------------------------------------------------------------------------//


    //$scope.tempLanguages = angular.copy(Languages);

    //$scope.showLanguageList = function (divToBeDisplayed) {
    //    $("#" + divToBeDisplayed).show();
    //};

    //$scope.SelectLanguage = function (selectedLanguage) {

    //    $scope.tempObject.FacilityDetail.Language.NonEnglishLanguages.push({
    //        NonEnglishLanguageID: null,
    //        Language: selectedLanguage.name,
    //        InterpretersAvailableYesNoOption: 1,
    //        StatusType: 1
    //    });
    //    $scope.tempLanguages.splice($scope.tempLanguages.indexOf(selectedLanguage), 1);
    //    $scope.searchLang = "";
    //    $(".LanguageSelectAutoList").hide();
    //};


    //$scope.DeselectLanguage = function (language) {
    //    $scope.tempObject.FacilityDetail.Language.NonEnglishLanguages.splice($scope.tempObject.FacilityDetail.Language.NonEnglishLanguages.indexOf(language), 1);

    //    for (var i in Languages) {
    //        if (Languages[i].name == language.Language) {
    //            $scope.tempLanguages.push(Languages[i]);
    //        }
    //    }

    //    $scope.tempLanguages.sort(function (a, b) {
    //        if (a.name > b.name) {
    //            return 1;
    //        }
    //        if (a.name < b.name) {
    //            return -1;
    //        }
    //        // a must be equal to b
    //        return 0;
    //    });

    //};


    //------edit mid level practitioner-----------


    $scope.MidLevelProviderPractitioner = {};



    $scope.operateMidlevelAddControl = function (viewSwitch) {

        $scope.addmidlevelNew = true;
        if (viewSwitch == 'edit')
            $scope.addmidlevelNew = false;
    };

    $scope.$watch('MidLevelProviderPractitioner.FirstName', function (newV, oldV) {
        if (newV == oldV) return;
        if (newV != "") {
            $('#FnameMsg').hide();
        }
        else {
            $('#FnameMsg').show();
        }
    })
    $scope.$watch('MidLevelProviderPractitioner.LastName', function (newV, oldV) {
        if (newV == oldV) return;
        if (newV != "") {
            $('#LnameMsg').hide();
        }
        else {
            $('#LFacilityNameMsg').show();
        }
    })
    $scope.MidLevelProviderPractitioner = {};
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


        if (dataObject.MiddleName == null || dataObject.MiddleName == "") {
            $(mname).text("");
        }


        if (dataObject.LastName == null || dataObject.LastName == "") {
            status = false;
            $(lname).text("Please enter Last Name.");
        }




        return status;
    };
    var TemporayFacilityObject = {};
    var TemporayFacilityObjectIndex = -1;
    var count1 = 0;
    $scope.RevertBackTemp = function (data, parentData) {
        if (count1 == 0) {
            TemporayFacilityObject = angular.copy(data);
            TemporayFacilityObjectIndex = $scope.PracticeLocationDetails.indexOf(parentData);
            count1++;
        }
    };
    $scope.CancelFacilityForOfficeHour = function () {
        if (TemporayFacilityObjectIndex != -1) {
            $scope.PracticeLocationDetails[TemporayFacilityObjectIndex].Facility = angular.copy(TemporayFacilityObject);
        }
    }


    $scope.addFacilityMidlevelPractioners = function (dataObject, dataScope) {
        if (!angular.isDefined($scope.tempObject.FacilityDetail.FacilityPracticeProviders)) {
            $scope.tempObject.FacilityDetail.FacilityPracticeProviders = [];
        }
        if ($scope.validateMidLevelProviderPractitioner(dataObject, dataScope, 'add')) {
            $scope.tempObject.FacilityDetail.FacilityPracticeProviders.push({
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

    $scope.tempMidLevelPractitioner = {};

    $scope.editFacilityMidlevelPractioners = function (ind, practitioner) {

        $scope.tempMidLevelPractitioner = angular.copy(practitioner)
        $scope.tempMidLevelIndex = ind;
    };
    $scope.dataLoaded = false;



    $scope.updateFacilityMidlevelPractioners = function (midLevels, tempThirdObject, index, dataScope, sectionValue) {
        if ($scope.validateMidLevelProviderPractitioner(tempThirdObject, dataScope, 'update')) {
            if (!(tempThirdObject.FirstName == ''
                || tempThirdObject.FirstName == null
                || tempThirdObject.MiddleName == ''
                || tempThirdObject.MiddleName == null
                || tempThirdObject.LastName == ''
                || tempThirdObject.LastName == null
                || tempThirdObject.NPINumber == ''
                || tempThirdObject.NPINumber == null
            )) {
                midLevels[index] = angular.copy(tempThirdObject);
            }
            $rootScope.visibilityThirdControl = sectionValue;
        }

    };

    $scope.setFacilityMidLevelPractitioner = function (practitioner) {
        if (angular.isObject(practitioner)) {
            $scope.MidLevelProviderPractitioner.FirstName = practitioner.PersonalDetail.FirstName;
            $scope.MidLevelProviderPractitioner.MiddleName = practitioner.PersonalDetail.MiddleName;
            $scope.MidLevelProviderPractitioner.LastName = practitioner.PersonalDetail.LastName;
            $scope.MidLevelProviderPractitioner.NPINumber = practitioner.OtherIdentificationNumber.NPINumber;
            $(".ProviderTypeSelectAutoList").hide();
            $scope.disableBit = true;
        }
    };


    //Preeti Controls the Edit feature on the page of object has an object
    $rootScope.operateThirdEditControl = function (sectionValue, obj) {
        try {
            $('.fileinput-exists').find('a').trigger('click');
        } catch (e) {

        }
        $rootScope.tempThirdObject = {};
        $('.field-validation-error').removeClass('field-validation-error').addClass('field-validation-valid');
        $('.input-validation-error').removeClass('input-validation-error').addClass('valid');
        $rootScope.tempThirdObject = angular.copy(obj);
        $rootScope.visibilityThirdControl = sectionValue;
    };

    //Controls the  View and Add cancel feature on the page of object has an object
    $rootScope.operateThirdCancelControl = function (Form_Div_Id) {
        $rootScope.tempThirdObject = {};
        if (Form_Div_Id) {
            //FormReset($("#" + Form_Div_Id).find("form"));
        }
        $('.field-validation-error').removeClass('field-validation-error').addClass('field-validation-valid');
        $('.input-validation-error').removeClass('input-validation-error').addClass('valid');
        $rootScope.visibilityThirdControl = "";
    };

    $scope.removeMidLevelProviderPractitioner = function (practitioner, visibilityControlPracLoc) {

        if (visibilityControlPracLoc == "addPracticeLocationNew") {
            $scope.tempObject.FacilityDetail.FacilityPracticeProviders.splice($scope.tempObject.FacilityDetail.FacilityPracticeProviders.indexOf(practitioner), 1);
        }
        else {

            practitioner.StatusType = 2;
            $scope.tempObject.FacilityDetail.FacilityPracticeProviders.splice($scope.tempObject.FacilityDetail.FacilityPracticeProviders.indexOf(practitioner), 1);
        }
        if ($scope.tempObject.FacilityDetail.FacilityPracticeProviders.length < 1) {
            $scope.resetMidLevelProviderPractitioner();
        }
    };
    $scope.resetmidlevel = function () {
        $scope.addmidlevelNew = false;
    }
    //$scope.resetMidLevelProviderPractitioner = function (param) {
    //    if (param == 'cancelEdit')
    //        $scope.tempMidLevelIndex = -1;
    //    else {
    //        $scope.visibilityThirdControl = "";
    //        $scope.MidLevelProviderPractitioner = {};
    //        $scope.tempMidLevelPractitioner = {};
    //        $scope.addmidlevelNew ? $scope.addmidlevelNew = false : $scope.addmidlevelNew = true;
    //        $scope.addMidLevelPractitioner ? $scope.addMidLevelPractitioner = false : $scope.addMidLevelPractitioner = true;
    //    }
    //};

    $scope.resetMidLevelProviderPractitioner = function () {

        $rootScope.visibilityThirdControl = "";
        $scope.MidLevelProviderPractitioner = {};
        $scope.addmidlevelNew ? $scope.addmidlevelNew = false : $scope.addmidlevelNew = true;

    };

    /// Dvsh
    $scope.showContryCodeDiv = function (div_Id) {
       
        $("#" + div_Id).show();
    };

    $scope.hideLabels = function () {
        $scope.IsSelected_Mid_Level = false;
    };
    $scope.Select_Mid_Level_Practioner = function (c, div) {

        if (c != null) {
            $scope.disableBit = false;
            $scope.IsSelected_Mid_Level = true;
            $scope.MidLevelProviderPractitioner.FirstName = c.PersonalDetail.FirstName || "";
            $scope.MidLevelProviderPractitioner.LastName = c.PersonalDetail.LastName || "";
            $scope.MidLevelProviderPractitioner.MiddleName = c.PersonalDetail.MiddleName || "";
            $scope.MidLevelProviderPractitioner.NPINumber = c.OtherIdentificationNumber.NPINumber || "";
           
         //   $scope.MidLevelProviders.splice($scope.MidLevelProviders.indexOf(c), 1);

        }

        $scope.SpecialtyName = "";
        $("#" + div).hide();
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

    $scope.setMidLevelPractitioner = function (practitioner) {
        if (angular.isObject(practitioner)) {
            $scope.tempThirdObject.FirstFacilityName = practitioner.FirstFacilityName;
            $scope.tempThirdObject.MiddleFacilityName = practitioner.MiddleFacilityName;
            $scope.tempThirdObject.LastFacilityName = practitioner.LastFacilityName;
            $scope.tempThirdObject.NPINumber = practitioner.NPINumber;
            $(".ProviderTypeSelectAutoList").hide();
        }
    };

    $scope.resetMidLevelPractitioner = function () {
        $scope.MidLevelPractitioner = {};
        $scope.tempMidLevelPractitionersList = [];
    };

    $scope.initWarning = function (practitioner) {
        $($('#warningModal').find('button')[2]).attr('disabled', false);
        if (angular.isObject(practitioner)) {
            $scope.tempMidLevelpractitioner = practitioner;
        }
        $('#warningModal').modal();
    };


    $scope.toggleList = function () {
        $(".ProviderTypeSelectAutoList").show();
    };

    $scope.ResetFormForValidation = function (form) {
        form.removeData('validator');
        form.removeData('unobtrusiveValidation');
        $.validator.unobtrusive.parse(form);
    }

    $scope.showContryCodeDiv = function (countryCodeDivId) {
        changeVisibilityOfCountryCode();
        $("#" + countryCodeDivId).show();
    };
    $scope.hideDiv = function () {
        $(".countryDailCodeContainer").hide();
    }


}]);

function showLocationList(ele) {
    $(ele).parent().find(".ProviderTypeSelectAutoList").first().show();
}



//================================= Hide All country code popover =========================
$(document).click(function (event) {
    if (!$(event.target).hasClass("countryCodeClass") && $(event.target).parents(".countryDailCodeContainer").length === 0) {
        $(".countryDailCodeContainer").hide();
    }
    if (!$(event.target).hasClass("form-control") && $(event.target).parents(".ProviderTypeSelectAutoList").length === 0) {
        $(".ProviderTypeSelectAutoList").hide();
    }
    if (!$(event.target).hasClass("form-control") && $(event.target).parents(".LanguageSelectAutoList").length === 0) {
        $(".LanguageSelectAutoList").hide();
    }
});

var changeVisibilityOfCountryCode = function () {
    $(".countryDailCodeContainer").hide();
    // method will close any other country code div already open.
};
$(document).ready(function () {
    $(".countryDailCodeContainer").hide();
    $(".ProviderTypeSelectAutoList").hide();
    $(".LanguageSelectAutoList").hide();
});