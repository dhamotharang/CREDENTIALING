//--------------------- Angular Module ----------------------
var masterDataSchools = angular.module("masterDataSchools", ['ui.bootstrap']);

//Service for getting master data
masterDataSchools.service('masterDataService', ['$http', '$q', function ($http, $q) {

    this.getMasterData = function (URL) {
        return $http.get(URL).then(function (value) { return value.data; });
    };

}]);
masterDataSchools.service('locationService', ['$http', function ($http) {
    //location service to return array of locations relevant to the querystring
    this.getLocations = function (QueryString) {
        return $http.get(rootDir + "/Location/GetCities?city=" + QueryString)     ///locationService makes an ajax call to fetch all the cities which have relevent names as the query string.
        .then(function (response) { return response.data; });     //Which is then returned to the controller method which called the service
    };
}]);
masterDataSchools.service('messageAlertEngine', ['$rootScope', '$timeout', function ($rootScope, $timeout) {

    $rootScope.messageDesc = "";
    $rootScope.activeMessageDiv = "";
    $rootScope.messageType = "";

    var animateMessageAlertOff = function () {
        $rootScope.closeAlertMessage();
    };


    this.callAlertMessage = function (calledDiv, msg, msgType, dismissal) { //messageAlertEngine.callAlertMessage('updateSchoolPrivilege' + IndexValue, "Data Updated Successfully !!!!", "success", true);                            
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
}])

//$scope.RemoveSchool = function (SchoolID) {
//    $scope.savingStatus = true;
//    $http.post(rootDir + '/MasterDataNew/InactivateSchool?SchoolID=' + SchoolID).
//            success(function (data, status, headers, config) {
//                try {
//                    //----------- success message -----------
//                    if (data.status == "true") {
//                        $scope.savingStatus = false;

//                        messageAlertEngine.callAlertMessage("School", "School Deleted Successfully !!!!", "success", true);
//                        for (var i in $scope.NotesTemplates) {
//                            if ($scope.Schools[i].SchoolID == SchoolID) {
//                                $scope.Schools.splice(i, 1);
//                                break;
//                            }
//                        }
                       
//                        $scope.error = "";
//                        $scope.existErr = "";
//                    }
//                } catch (e) {

//                }

//            }).
//            error(function (data, status, headers, config) {
//                //----------- error message -----------
//                $scope.savingStatus = false;

//                messageAlertEngine.callAlertMessage("SchoolError", "Sorry Unable To Delete School !!!!", "danger", true);
//            });

//}
//=========================== Controller declaration ==========================
masterDataSchools.controller('masterDataSchoolsController', ['$scope', '$http', '$filter', '$rootScope', 'masterDataService', 'messageAlertEngine', 'locationService', function ($scope, $http, $filter, $rootScope, masterDataService, messageAlertEngine, locationService) {
    $scope.tempSchool = [];
    $scope.Schools = [];
    $scope.viewSchools = true;
    $scope.disableEdit = false;

    $http.get(rootDir + "/MasterDataNew/GetAllSchools").success(function (value) {
        try {

            for (var i = 0; i < value.length ; i++) {
                if (value[i] != null) {
                    value[i].LastModifiedDate = ($scope.ConvertDateFormat(value[i].LastModifiedDate)).toString();
                }

            }

            $scope.Schools = angular.copy(value);
        } catch (e) {
          
        }
    });
   
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

    function ResetFormForValidation(form) {
        form.removeData('validator');
        form.removeData('unobtrusiveValidation');
        $.validator.unobtrusive.parse(form);

    }
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
    $scope.showSchools = function () {
        $scope.viewSchools = true;
        $scope.viewSchoolAddress = false;
        $scope.viewSchoolContacts = false;
        $scope.SchoolId1 = "";
        $scope.personId = "";
    };
    $scope.editSchool = function (School) {
        $scope.tempSchool = angular.copy(School);
        $scope.disableAdd = true;
        $scope.disablecode = true;
    };
    $scope.showSchoolAddress = function () {
        $scope.SchoolAddress = [];
        $scope.viewSchools = false;
        $scope.viewSchoolAddress = true;
        $scope.viewSchoolContacts = false;
        $scope.SchoolId = "";
        $scope.personId = "";
    };
    $scope.showSchoolContacts = function () {
        $scope.SchoolContact = [];
        $scope.viewSchools = false;
        $scope.viewSchoolAddress = false;
        $scope.viewSchoolContacts = true;
        $scope.SchoolId = false;
        $scope.SchoolId1 = "";  
        $scope.personId = "";
    };
    $scope.addSchoolContacts = function () {
        $(".ProviderTypeSelectAutoList").hide();
        var temp = {
            SchoolContactInfoID: 0,
            Location: "",
            UnitNumber: "",
            Street: "",
            Country: "",
            State: "",
            County: "",
            City: "",
            ZipCode: "",
            PhoneNumber: "",
            FaxNumber: "",
            PhoneCountryCode: "",
            FaxCountryCode: "",
            Email: "",
            Status: "Active",
            LastModifiedDate: new Date(),

        };
        $scope.tempSC = angular.copy(temp);
        $scope.AddNewSchoolContact = true;
    };
    $scope.getSchoolContacts = function (SchoolID, condition) {
        if (SchoolID == "") {
            
            $scope.AddNewSchoolContact = false;
        }
        $scope.SchoolID = angular.copy(SchoolID);

        $scope.SchoolContacts = [];
        for (var i in $scope.Schools) {
            if ($scope.Schools[i].SchoolID == SchoolID) {
                $scope.SchoolContacts = $scope.Schools[i].SchoolContactInfoes;
                break;
            }
        }
        if (condition) {
         
            $scope.SelectedLocationID = "";
        }
    };
    $scope.tempSchools = {};
    $scope.tempSC = {};

    //------------ gets the template to ng-include for a table row / item -------------------
    $scope.getTemplate = function (Schools) {
       if (Schools.SchoolID === $scope.tempSchools.SchoolID) return 'editSchools';
         return 'displaySchools';
    };
    $scope.getSCTemplate = function (sc) {
        if (sc.SchoolContactInfoID === $scope.tempSC.SchoolContactInfoID) return 'editSC';
            return 'displaySC';
    };
    //-------------------- Edit School ----------
    $scope.editSchools = function (School) {
        $scope.tempSchools = angular.copy(School);
        $scope.disableAdd = true;
    };

    $scope.editSchoolsSC = function (SC) {
        $scope.tempSC = angular.copy(SC);
        $scope.SchoolId = false;
       
    };

    $scope.addressAutocomplete = function (location) {
        $(".ProviderTypeSelectAutoList").show();
        if (location.length == 0) {
            $scope.resetAddressModels();
        }

        $scope.tempSC.SchoolContactInfoes[0].City = location;
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
    
    function showLocationList(ele) {
        $(ele).parent().find(".ProviderTypeSelectAutoList").first().show();
        
    }
    //function showLocationList(ele) {
    //    $(ele).parent().find(".ProviderTypeSelectAutoList").first().show();
    //}
    //function showLocationList(ele) {
    //    $(ele).parent().find(".ProviderTypeSelectAutoList").first().show();
    //}
    $scope.resetAddressModels = function () {
        $scope.tempSC.SchoolContactInfoes[0].City = "";
        $scope.tempSC.SchoolContactInfoes[0].State = "";
        $scope.tempSC.SchoolContactInfoes[0].Country = "";
    };

    $scope.setAddressModels = function (location) {
        $scope.tempSC.SchoolContactInfoes[0].City = location.City;
        $scope.tempSC.SchoolContactInfoes[0].State = location.State;
        $scope.tempSC.SchoolContactInfoes[0].Country = location.Country;

    }

    $scope.addressAutocomplete1 = function (location) {
        $(".ProviderTypeSelectAutoList").show();
        if (location.length == 0) {
            $scope.resetAddressModels1();
        }

        $scope.tempSchool.SchoolContactInfoes[0].City = location;
        if (location.length > 1 && !angular.isObject(location)) {
            locationService.getLocations(location).then(function (val) {
                $scope.Locations = val;
            });
        } else if (angular.isObject(location)) {
            $scope.setAddressModels1(location);
        }
    };
    $scope.selectedLocation1 = function (location) {
        $scope.setAddressModels1(location);
        $(".ProviderTypeSelectAutoList").hide();
    };
    $scope.resetAddressModels1 = function () {
        $scope.tempSchool.SchoolContactInfoes[0].City = "";
        $scope.tempSchool.SchoolContactInfoes[0].State = "";
        $scope.tempSchool.SchoolContactInfoes[0].Country = "";
    };

    $scope.setAddressModels1 = function (location) {
        $scope.tempSchool.SchoolContactInfoes[0].City = location.City;
        $scope.tempSchool.SchoolContactInfoes[0].State = location.State;
        $scope.tempSchool.SchoolContactInfoes[0].Country = location.Country;

    }

    //------------------- Add School ---------------------
    //$scope.addSchools = function (School) {
    //    $scope.disableEdit = true;
    //    $scope.disableAdd = true;
    //    var Month = new Date().getMonth() + 1;
    //    var _month = Month < 10 ? '0' + Month : Month;
    //    var _date = new Date().getDate() < 10 ? '0' + new Date().getDate() : new Date().getDate();
    //    var _year = new Date().getFullYear();
    //    var temp = {
    //        SchoolID: 0,
    //        Name: "",
    //        Status: "Active",
    //        LastModifiedDate: _month + "-" + _date + "-" + _year
    //    };
    //    //$scope.Schools.splice(0, 0, angular.copy(temp));
    //    $scope.tempSchools = angular.copy(temp);
    //};
    $scope.addSchools = function () {
        
        var temp = {
            SchoolID: 0,
            Name: "",
           
            Status: "Active",
            StatusType: 1,
            LastModifiedDate: new Date(),
            SchoolContactInfoes: [
                {
                    SchoolContactInfoID: 0,
                    Location:"",
                    UnitNumber: "",
                    Street: "",
                    Country: "",
                    State: "",
                    County: "",
                    City: "",
                    ZipCode: "",
                    PhoneNumber: "",
                    PhoneCountryCode: "",
                    FaxCountryCode: "",
                    FaxNumber: "",
                    Email: "",
                    Status: "Active",
                    LastModifiedDate: new Date(),

                }
            ]
        };
        $scope.Schools.splice(0, 0, angular.copy(temp));
        $scope.tempSchoool = angular.copy(temp);
        $scope.AddNewSchool = true;
        $(".countryDailCodeContainer").hide();
        $(".ProviderTypeSelectAutoList").hide();
    }
    //------------------- Save School ---------------------
    //$scope.saveSchools = function (idx) {

    //    var addData = {
    //        SchoolID: 0,
    //        Name: $scope.tempSchools.Name,
    //        StatusType: 1,
    //    };

    //    var isExist = true;

    //    for (var i = 0; i < $scope.Schools.length; i++) {

    //        if (addData.Name && $scope.Schools[i].Name.replace(" ", "").toLowerCase() == addData.Name.replace(" ", "").toLowerCase()) {

    //            isExist = false;
    //            $scope.existErr = "The Name Already Exist";
    //            break;
    //        }
    //    }
    //    if (!addData.Name) { $scope.error = "Please Enter the Name"; }

    //    if (addData.Name && isExist) {
    //        $http.post(rootDir + '/MasterDataNew/AddSchool', addData).
    //            success(function (data, status, headers, config) {
    //                try {
    //                    //----------- success message -----------
    //                    if (data.status == "true") {
    //                        messageAlertEngine.callAlertMessage("School", "New School Details Added Successfully !!!!", "success", true);
    //                        data.schoolDetails.LastModifiedDate = $scope.ConvertDateFormat(data.schoolDetails.LastModifiedDate);
    //                        $scope.Schools[idx] = angular.copy(data.schoolDetails);
    //                        $scope.reset();
    //                        $scope.error = "";
    //                        $scope.existErr = "";
    //                    }
    //                    else {
    //                        messageAlertEngine.callAlertMessage("SchoolError", "Sorry Unable To Add School !!!!", "danger", true);
    //                        $scope.Schools.splice(idx, 1);
    //                    }
    //                } catch (e) {
                      
    //                }
    //            }).
    //            error(function (data, status, headers, config) {
    //                //----------- error message -----------
    //                messageAlertEngine.callAlertMessage("SchoolError", "Sorry Unable To Add School !!!!", "danger", true);
    //                $scope.Schools.splice(idx, 1);
    //            });
    //    }
        
    //};
    $scope.saveSchool = function () {
    
        var validationStatus;
        var $formData;

        var addData = {

            SchoolID: 0,
            Name: $scope.tempSchool.Name,
           
            StatusType: 1,

            SchoolContactInfoViewModel:
            {
                SchoolContactInfoID: 0,
                Location:$scope.tempSchool.SchoolContactInfoes[0].LocationName,
                UnitNumber: $scope.tempSchool.SchoolContactInfoes[0].UnitNumber,
                Street: $scope.tempSchool.SchoolContactInfoes[0].Street,
                Country: $scope.tempSchool.SchoolContactInfoes[0].Country,
                State: $scope.tempSchool.SchoolContactInfoes[0].State,
                County: $scope.tempSchool.SchoolContactInfoes[0].County,
                City: $scope.tempSchool.SchoolContactInfoes[0].City,
                ZipCode: $scope.tempSchool.SchoolContactInfoes[0].ZipCode,
                PhoneNumber: $scope.tempSchool.SchoolContactInfoes[0].Phone,
                FaxNumber: $scope.tempSchool.SchoolContactInfoes[0].Fax,
                PhoneCountryCode: $scope.tempSchool.SchoolContactInfoes[0].PhoneCountryCode,
                FaxCountryCode: $scope.tempSchool.SchoolContactInfoes[0].FaxCountryCode,
                Email: $scope.tempSchool.SchoolContactInfoes[0].Email,
                StatusType: 1,


            },
            

        };

        $formData = $('#newSchoolInfo').find('form');

        ResetFormForValidation($formData);
        validationStatus = $formData.valid();

        var isExist = true;
        $scope.SchoolExist = "";
        for (var i = 0; i < $scope.Schools.length; i++) {
         
            if (addData.Name && $scope.Schools[i].Name.replace(/ /g, '').toLowerCase() == addData.Name.replace(/ /g, '').toLowerCase()) {

                isExist = false;
                $scope.SchoolExist = "The Name already exist";
                break;
            }
        }
        //for (var i = 0; $scope.Schools.length; i++)
        //{
        //    if (addData.Name && $scope.Schools[i].Name.replace(" ", "").toLowerCase() == addData.Name.replace(" ", "").toLowerCase()) {

        //        isExist = false;
        //        $scope.SchoolExist = "The Name already exist";
        //        break;
        //    }
        //}
        //for (var m = 0; $scope.Schools.length; m++) {

        //    if ($scope.Schools[m].Name == addData.Name) {
        //        $scope.SchoolExist = "The School Name Exists";
        //        exist = false;
        //        break;
        //    }            
        //}
        

        if (validationStatus && isExist) {
            $scope.LoadingAjax = true;
            $http.post(rootDir + '/MasterDataNew/AddSchool', addData).
                success(function (data, status, headers, config) {
                    //----------- success message -----------
                    if (data.status == "true") {
                        $scope.AddNewSchool = false;
                        $scope.tempSchool = {};
                        messageAlertEngine.callAlertMessage("School", "New School Details Added Successfully !!!!", "success", true);
                        //data.admitingPrivilege.LastModifiedDate = $scope.ConvertDateFormat(data.admitingPrivilege.LastModifiedDate);
                        for (var i = 0; i < $scope.Schools.length; i++) {

                            if ($scope.Schools[i].SchoolID == 0) {

                                $scope.Schools[i] = data.schoolDetails;
                                $scope.Schools[i].LastModifiedDate = ($scope.ConvertDateFormat($scope.Schools[i].LastModifiedDate)).toString();
                               // $scope.Schools[i].LastModifiedDate = $scope.ConvertDateFormat(new Date());
                                $scope.Schools[i].SchoolContactInfoes[0].LastModifiedDate = new Date();
                               
                             
                            }
                        }
                        $scope.LoadingAjax = false;
                        $scope.reset();
                        $scope.error = "";
                        $scope.existErr = "";

                    }
                    else {
                        $scope.LoadingAjax = false;
                        messageAlertEngine.callAlertMessage("SchoolAddError", "Sorry Unable To Add School !!!!", "danger", true);

                    }
                }).
                error(function (data, status, headers, config) {
                    $scope.LoadingAjax = false;
                    //----------- error message -----------
                    messageAlertEngine.callAlertMessage("SchoolAddError", "Sorry Unable To Add School !!!!", "danger", true);

                });
        }
    }

    
    $scope.reset = function () {
        $scope.tempSchool = {};
        $scope.tempHC = {};
        $scope.tempHCP = {};
        $scope.tempSC = {};
        $scope.AddNewSchool = false;
        $scope.AddNewSchoolContact = false;
        $scope.disableAdd = false;
        $scope.disableEdit = false;
        $scope.SchoolId = true;
        $scope.error = "";
        $scope.existErr = "";
    }

    $scope.updateSchools = function (updateData,id) {

        //var updateData = {
        //    SchoolID: $scope.tempSchools.SchoolID,
        //    Name: $scope.tempSchools.Name,
        //    StatusType: 1,
        //};
        var schoolContacts =angular.copy(updateData.SchoolContactInfoes);
        var isExist = true;
        var idx;
        for (var i = 0; i < $scope.Schools.length; i++) {

            if (updateData.Name && $scope.Schools[i].SchoolID != updateData.SchoolID && $scope.Schools[i].Name.replace(" ", "").toLowerCase() == updateData.Name.replace(" ", "").toLowerCase()) {

                isExist = false;
                $scope.existErr = "The Name is Exist";
                break;
            }
            if ($scope.Schools[i].SchoolID == updateData.SchoolID) {
                idx = i;
            }
        }
        if (!updateData.Name) { $scope.error = "Please Enter the Name"; }

        if (updateData.Name && isExist) {
            $http.post(rootDir + '/MasterDataNew/UpdateSchool', updateData).
                success(function (data, status, headers, config) {
                    try {
                        //----------- success message -----------
                        if (data.status == "true") {
                            messageAlertEngine.callAlertMessage("School", "New School Details Updated Successfully !!!!", "success", true);
                            data.schoolDetails.LastModifiedDate = $scope.ConvertDateFormat(data.schoolDetails.LastModifiedDate);
                            $scope.Schools[idx] = angular.copy(data.schoolDetails);
                            $scope.Schools[idx].LastModifiedDate = angular.copy(data.schoolDetails.LastModifiedDate);
                            $scope.Schools[idx].SchoolContactInfoes = schoolContacts;
                            $scope.Schools[idx].Name = angular.copy(data.schoolDetails.Name)                            
                            $scope.reset();
                            $scope.error = "";
                            $scope.existErr = "";
                        }
                    } catch (e) {
                     
                    }
                    
                }).
                error(function (data, status, headers, config) {
                    //----------- error message -----------
                    messageAlertEngine.callAlertMessage("School", "Sorry Unable To Update School !!!!", "danger", true);
                    
                });
        }

    };


    $scope.updateSchoolsContactInfo = function (idx) {

        var updateSchoolsContactInfo = {
            SchoolContactInfoID: $scope.tempSC.SchoolContactInfoID,
            Location: $scope.tempSC.Location,
            UnitNumber: $scope.tempSC.UnitNumber,
            Street: $scope.tempSC.Street,
            Country: $scope.tempSC.Country,
            State: $scope.tempSC.State,
            County: $scope.tempSC.County,
            City: $scope.tempSC.City,
            ZipCode: $scope.tempSC.ZipCode,
            PhoneNumber: $scope.tempSC.PhoneNumber,
            FaxNumber: $scope.tempSC.FaxNumber,
            PhoneCountryCode: $scope.tempSC.PhoneCountryCode,
            FaxCountryCode: $scope.tempSC.FaxCountryCode,
            Email: $scope.tempSC.Email,
            StatusType: 1,
        };

        $formData = $('#editSchoolContactInfo').find('form');

        ResetFormForValidation($formData);
        validationStatus = $formData.valid();

        var isExist = true;

        if (validationStatus && isExist) {
            $http.post(rootDir + '/MasterDataNew/UpdateSchoolContactInfo', updateSchoolsContactInfo).
                success(function (data, status, headers, config) {
                    try {
                        //----------- success message -----------
                        if (data.status == "true") {
                            messageAlertEngine.callAlertMessage("School", "School Contact Details Updated Successfully !!!!", "success", true);
                            data.schoolContactDetails.LastModifiedDate = $scope.ConvertDateFormat(data.schoolContactDetails.LastModifiedDate);
                            $scope.SchoolContacts[idx] = angular.copy(data.schoolContactDetails);
                            $scope.reset();
                            $scope.error = "";
                            $scope.existErr = "";
                        }
                    } catch (e) {

                    }

                }).
                error(function (data, status, headers, config) {
                    //----------- error message -----------
                    messageAlertEngine.callAlertMessage("School", "Sorry Unable To Update School Contact details !!!!", "danger", true);

                });
        }

    };


    $scope.saveSC = function () {
        var validationStatus;
        var $formData;

        var id = angular.copy($scope.SchoolID);
        var addData = {

            SchoolContactInfoID: 0,
            Location: $scope.tempSC.SchoolContactInfoes[0].Location,
            UnitNumber: $scope.tempSC.SchoolContactInfoes[0].UnitNumber,
            Street: $scope.tempSC.SchoolContactInfoes[0].Street,
            Country: $scope.tempSC.SchoolContactInfoes[0].Country,
            State: $scope.tempSC.SchoolContactInfoes[0].State,
            County: $scope.tempSC.SchoolContactInfoes[0].County,
            City: $scope.tempSC.SchoolContactInfoes[0].City,
            ZipCode: $scope.tempSC.SchoolContactInfoes[0].ZipCode,
            PhoneNumber: $scope.tempSC.SchoolContactInfoes[0].Phone,
            FaxNumber: $scope.tempSC.SchoolContactInfoes[0].Fax,
            PhoneCountryCode: $scope.tempSC.SchoolContactInfoes[0].PhoneCountryCode,
            FaxCountryCode: $scope.tempSC.SchoolContactInfoes[0].FaxCountryCode,
            Email: $scope.tempSC.SchoolContactInfoes[0].Email,
            StatusType: 1,
           
        }

        $formData = $('#newSchoolContactInfo').find('form');

        ResetFormForValidation($formData);
        validationStatus = $formData.valid();

        if (!id) { $scope.SchoolNameError = "Please Select The School Name" };

        var exist = true;
        $scope.locationExist = "";

        for (var m = 0; m<$scope.Schools.length; m++) {

            if ($scope.Schools[m] && $scope.Schools[m].SchoolID == id) {

                for (var n = 0; n<$scope.Schools[m].SchoolContactInfoes.length; n++) {
                    if ($scope.Schools[m].SchoolContactInfoes[n].Location != "") {
                        if (addData.Location && $scope.Schools[m].SchoolContactInfoes[n].Location.replace(/ /g, '').toLowerCase() == addData.Location.replace(/ /g, '').toLowerCase()) {
                            $scope.locationExist = "The Location Name already Exist";
                            exist = false;
                            break;
                        }
                    }
                    

                }
                break;
               
            }


        }

        if (id && validationStatus && exist) {
            $http.post(rootDir + '/MasterDataNew/AddSchoolContactInfo?SchoolId=' + id, addData).
                success(function (data, status, headers, config) {
                    //----------- success message -----------
                    if (data.status == "true") {
                        $scope.AddNewSchoolContact = false;
                        $scope.tempHC = {};
                        messageAlertEngine.callAlertMessage("Contact", "New School Contact Details Added Successfully !!!!", "success", true);

                        for (var i = 0; i < $scope.Schools.length; i++) {

                            if ($scope.Schools[i].SchoolID == id) {
                                data.contactDetails.LastModifiedDate = new Date();
                             
                                $scope.Schools[i].SchoolContactInfoes.push(data.contactDetails);
                                $scope.Schools[i].NoofLocations++;
                            }
                        }
                        $scope.reset();
                        $scope.error = "";
                        $scope.existErr = "";

                    }
                    else {
                        messageAlertEngine.callAlertMessage("ContactAddError", "Sorry Unable To Add School Contact !!!!", "danger", true);

                    }
                }).
                error(function (data, status, headers, config) {
                    //----------- error message -----------
                    messageAlertEngine.callAlertMessage("ContactAddError", "Sorry Unable To Add School Contact!!!!", "danger", true);

                });
        }
    };
    $scope.showContryCodeDiv = function (countryCodeDivId) {
        changeVisibilityOfCountryCode();
        $("#" + countryCodeDivId).show();
    };
    $scope.CloseDiv=function(){


    }
  
    $scope.setErrorBitLocation = function (name) {
        if (name == "") {
            $scope.isLocationError = false;
        } else {
            $scope.isLocationError = true;
        }
    }
  
    //----------------- School new add cancel ---------------
    $scope.cancelAdd = function () {
        $scope.Schools.splice(0, 1);
        $scope.tempSchools = {};
        $scope.disableEdit = false;
        $scope.disableAdd = false;
        $scope.error = "";
        $scope.existErr = "";
        $scope.SchoolExist = "";
        $scope.locationExist = "";
    };

    //-------------------- Reset School ----------------------
    $scope.reset = function () {
        $scope.tempSchools = {};
        $scope.tempSC = {};
        $scope.SchoolExist = "";
        $scope.locationExist = "";
        $scope.AddNewSchool = false;
        $scope.AddNewSchoolContact = false;
        $scope.disableAdd = false;
        $scope.disableEdit = false;
        $scope.SchoolId = true;
        $scope.error = "";
        $scope.existErr = "";
    }
    //$scope.reset = function () {
    //    $scope.tempSchools = {};
    //    $scope.disableAdd = false;
    //    $scope.disableEdit = false;
    //    $scope.error = "";
    //    $scope.existErr = "";
    //};
    $scope.countryCodes = angular.copy(countryDailCodes);
    $scope.CurrentPage = [];
    $scope.CurrentPageSC = [];

    //-------------------------- angular bootstrap pagger with custom-----------------
    $scope.maxSize = 5;
    $scope.bigTotalItems = 0;
    $scope.bigCurrentPage = 1;
    $scope.bigTotalItemsSC = 0;
    $scope.bigCurrentPageSC = 1;

    //-------------------- page change action ---------------------
    $scope.pageChanged = function (pagnumber) {
        $scope.bigCurrentPage = pagnumber;
    };

    //-------------- current page change Scope Watch ---------------------
    $scope.$watch('bigCurrentPage', function (newValue, oldValue) {
        $scope.CurrentPage = [];
        var startIndex = (newValue - 1) * 10;
        var endIndex = startIndex + 9;
        if ($scope.Schools) {
            for (startIndex; startIndex <= endIndex ; startIndex++) {
                if ($scope.Schools[startIndex]) {
                    $scope.CurrentPage.push($scope.Schools[startIndex]);
                } else {
                    break;
                }
            }
        }
    });
    //-------------- License Scope Watch ---------------------
    $scope.$watchCollection('Schools', function (newValue, oldValue) {
        if (newValue) {
            $scope.bigTotalItems = newValue.length;

            $scope.CurrentPage = [];
            $scope.bigCurrentPage = 1;

            var startIndex = ($scope.bigCurrentPage - 1) * 10;
            var endIndex = startIndex + 9;

            for (startIndex; startIndex <= endIndex ; startIndex++) {
                if ($scope.Schools[startIndex]) {
                    $scope.CurrentPage.push($scope.Schools[startIndex]);
                } else {
                    break;
                }
            }
        }
    });
    $scope.$watchCollection('Schools.SchoolContactInfoes', function (newValue, oldValue) {
        if (newValue) {
            $scope.bigTotalItemsSC = newValue.length;
            $scope.CurrentPageSC = [];
            $scope.bigCurrentPageSC = 1;
            var startIndex = ($scope.bigCurrentPageSC - 1) * 10;
            var endIndex = startIndex + 9;
            for(startIndex;startIndex<=endIndex;startIndex++)
            {
                if ($scope.Schools.SchoolContactInfoes[startIndex]) {
                    $scope.CurrentPageSC.push($scope.Schools.SchoolContactInfoes[startIndex]);
                } else {
                    break;
                }
            }
        }

    });
    $scope.filterData = function () {
        $scope.pageChanged(1);
    }

    $scope.IsValidIndex = function (index) {

        var startIndex = ($scope.bigCurrentPage - 1) * 10;
        var endIndex = startIndex + 9;

        if (index >= startIndex && index <= endIndex)
            return true;
        else
            return false;

    }
    $(document).click(function (event) {
        if (!$(event.target).hasClass("form-control") && $(event.target).parents(".ProviderTypeSelectAutoList").length === 0) {
            $(".ProviderTypeSelectAutoList").hide();
        }
    });
    //------------------- end ------------------
}]);
var changeVisibilityOfCountryCode = function () {
    $(".countryDailCodeContainer").hide();
    // method will close any other country code div already open.
};
$(document).ready(function () {
    $(".countryDailCodeContainer").hide();
    $(".ProviderTypeSelectAutoList").hide();
    $(".LanguageSelectAutoList").hide();
});
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