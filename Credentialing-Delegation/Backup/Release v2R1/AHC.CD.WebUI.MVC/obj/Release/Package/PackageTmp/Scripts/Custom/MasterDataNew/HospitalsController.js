//--------------------- Angular Module ----------------------
var masterDataHospitals = angular.module("masterDataHospitals", ["ahc.cd.util", 'ui.bootstrap']);

//Service for getting master data
masterDataHospitals.service('masterDataService', ['$http', '$q', function ($http, $q) {

    this.getMasterData = function (URL) {
        return $http.get(URL).then(function (value) { return value.data; });
    };
}]);

masterDataHospitals.service('locationService', ['$http', function ($http) {
    //location service to return array of locations relevant to the querystring
    this.getLocations = function (QueryString) {
        return $http.get("/Location/GetCities?city=" + QueryString)     //locationService makes an ajax call to fetch all the cities which have relevent names as the query string.
        .then(function (response) { return response.data; });     //Which is then returned to the controller method which called the service
    };
}]);

masterDataHospitals.service('messageAlertEngine', ['$rootScope', '$timeout', function ($rootScope, $timeout) {

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
}]);

//=========================== Controller declaration ==========================
masterDataHospitals.controller('masterDatamasterDataHospitalsController', ['$scope', '$http', '$filter', 'masterDataService', 'messageAlertEngine', 'locationService', function ($scope, $http, $filter, masterDataService, messageAlertEngine, locationService) {

    //------------------------------------------------------------------Address Auto-Complete---------------------------------------------------------------------------//

    /* Method addressAutocomplete() gets the details of a location
         Method takes input of location details entered in the text box.*/

    $scope.addressAutocomplete = function (location) {

        if (location.length == 0) {
            $scope.resetAddressModels();
        }

        $scope.tempHC.City = location;
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
        $scope.tempHC.City = "";
        $scope.tempHC.State = "";
        $scope.tempHC.Country = "";
    };

    $scope.setAddressModels = function (location) {
        $scope.tempHC.City = location.City;
        $scope.tempHC.State = location.State;
        $scope.tempHC.Country = location.Country;

    }


    $scope.addressAutocomplete1 = function (location) {

        if (location.length == 0) {
            $scope.resetAddressModels1();
        }

        $scope.tempHospital.HospitalContactInfoes[0].City = location;
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
        $scope.tempHospital.HospitalContactInfoes[0].City = "";
        $scope.tempHospital.HospitalContactInfoes[0].State = "";
        $scope.tempHospital.HospitalContactInfoes[0].Country = "";
    };

    $scope.setAddressModels1 = function (location) {
        $scope.tempHospital.HospitalContactInfoes[0].City = location.City;
        $scope.tempHospital.HospitalContactInfoes[0].State = location.State;
        $scope.tempHospital.HospitalContactInfoes[0].Country = location.Country;

    }


    //----------------------------------------------------------------------------------------------------------------------------------------------------------------//    

    $http.get("/MasterDataNew/GetAllHospitals").then(function (value) {
        console.log("Hospitals");
        console.log(value);

        for (var i = 0; i < value.data.length ; i++) {
            if (value.data[i] != null) {
                value.data[i].LastModifiedDate = $scope.ConvertDateFormat(value.data[i].LastModifiedDate);
                
                for (var j = 0; j < value.data[i].HospitalContactInfoes.length ; j++) {
                    value.data[i].HospitalContactInfoes[j].LastModifiedDate = $scope.ConvertDateFormat(value.data[i].HospitalContactInfoes[j].LastModifiedDate);

                    for (k = 0; k < value.data[i].HospitalContactInfoes[j].HospitalContactPersons.length ; k++) {
                        value.data[i].HospitalContactInfoes[j].HospitalContactPersons[k].LastModifiedDate = $scope.ConvertDateFormat(value.data[i].HospitalContactInfoes[j].HospitalContactPersons[k].LastModifiedDate);
                    }
                }

            }
        }

        $scope.Hospitals = angular.copy(value.data);
        //$scope.data = angular.copy(value.data);
        //$scope.pagination($scope.data);
        console.log($scope.Hospitals);
    });

    //Convert the date from database to normal
    $scope.ConvertDateFormat = function (value) {
        ////console.log(value);
        var returnValue = value;
        try {
            if (value.indexOf("/Date(") == 0) {
                returnValue = new Date(parseInt(value.replace("/Date(", "").replace(")/", ""), 10));
            }
            return returnValue;
        } catch (e) {
            return returnValue;
        }
        return returnValue;
    };

    //------------------- Form Reset Function ------------------------
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
    
    //------------------------------- Country Code Popover Show by Id ---------------------------------------
    $scope.CountryDialCodes = countryDailCodes;

    $scope.showContryCodeDiv = function (countryCodeDivId) {
        changeVisibilityOfCountryCode();
        $("#" + countryCodeDivId).show();
    };

    $scope.hideDiv = function () {
        $(".countryDailCodeContainer").hide();
    }

    $scope.tempHospital = {};
    $scope.tempHC = {};
    $scope.tempHCP = {};

    //------------ hospital Template return -------------------
    $scope.getHospitalTemplate = function (hospital) {
        if (hospital.HospitalID === $scope.tempHospital.HospitalID) return 'editHospital';
        else return 'displayHospital';
    };

    //------------ hospital Contact Template return -------------------
    $scope.getHCTemplate = function (hc) {
        if (hc.HospitalContactInfoID === $scope.tempHC.HospitalContactInfoID) return 'editHC';
        else return 'displayHC';
    };

    //------------ hospital Contact Person Template return -------------------
    $scope.getHCPTemplate = function (hcp) {
        if (hcp.HospitalContactPersonID === $scope.tempHCP.HospitalContactPersonID) return 'editHCP';
        else return 'displayHCP';
    };

    //-------------------- Edit Group ----------
    $scope.editHospital = function (hospital) {
        $scope.tempHospital = angular.copy(hospital);
        $scope.disableAdd = true;
    };

    //------------------- Add Group ---------------------
    $scope.addHospital = function () {
        var temp = {
            HospitalID: 0,
            HospitalName: "",
            Code: "",
            Status: "Active",
            StatusType: 1,
            LastModifiedDate: new Date(),
            HospitalContactInfoes: [
                {
                    HospitalContactInfoID: 0,
                    LocationName: "",
                    UnitNumber: "",
                    Street: "",
                    Country: "",
                    State: "",
                    County: "",
                    City: "",
                    ZipCode: "",
                    PhoneNumber: "",
                    PhoneCountryCode:"",
                    FaxCountryCode:"",
                    FaxNumber: "",
                    Email: "",
                    Status: "Active",
                    LastModifiedDate: new Date(),
                    HospitalContactPersons: [
                        {
                            HospitalContactPersonID: 0,
                            ContactPersonName: "",
                            Phone: "",
                            Fax: "",
                            PhoneCountryCode: "",
                            FaxCountryCode: "",
                            Status: "Active",
                            LastModifiedDate: new Date(),
                        }
                    ]
                }
            ]
        };
        $scope.Hospitals.splice(0, 0, angular.copy(temp));
        $scope.tempHospital = angular.copy(temp);
        $scope.AddNewHospital = true;
        $(".countryDailCodeContainer").hide();
    };
    //------------------- Save Group ---------------------
    $scope.saveHospital = function () {

        var validationStatus;
        var $formData;

        var addData = {
           
            HospitalID: 0,
            HospitalName: $scope.tempHospital.HospitalName,
            Code: $scope.tempHospital.Code,
            StatusType: 1,
            
            HospitalContactInfoViewModel:
            {
                HospitalContactInfoID: 0,
                LocationName: $scope.tempHospital.HospitalContactInfoes[0].LocationName,
                UnitNumber: $scope.tempHospital.HospitalContactInfoes[0].UnitNumber,
                Street: $scope.tempHospital.HospitalContactInfoes[0].Street,
                Country: $scope.tempHospital.HospitalContactInfoes[0].Country,
                State: $scope.tempHospital.HospitalContactInfoes[0].State,
                County: $scope.tempHospital.HospitalContactInfoes[0].County,
                City: $scope.tempHospital.HospitalContactInfoes[0].City,
                ZipCode: $scope.tempHospital.HospitalContactInfoes[0].ZipCode,
                PhoneNumber: $scope.tempHospital.HospitalContactInfoes[0].Phone,
                FaxNumber: $scope.tempHospital.HospitalContactInfoes[0].Fax,
                PhoneCountryCode: $scope.tempHospital.HospitalContactInfoes[0].PhoneCountryCode,
                FaxCountryCode: $scope.tempHospital.HospitalContactInfoes[0].FaxCountryCode,
                Email: $scope.tempHospital.HospitalContactInfoes[0].Email,
                StatusType: 1,                    
                    
                    
            },
            HospitalContactPersonViewModel:
            {
                HospitalContactPersonID: 0,
                ContactPersonName: $scope.tempHospital.HospitalContactInfoes[0].HospitalContactPersons[0].ContactPersonName,
                Phone: $scope.tempHospital.HospitalContactInfoes[0].HospitalContactPersons[0].Phone,
                Fax: $scope.tempHospital.HospitalContactInfoes[0].HospitalContactPersons[0].Fax,
                PhoneCountryCode: $scope.tempHospital.HospitalContactInfoes[0].HospitalContactPersons[0].PhoneCountryCode,
                FaxCountryCode: $scope.tempHospital.HospitalContactInfoes[0].HospitalContactPersons[0].FaxCountryCode,
                StatusType: 1
            }
            
        };

        $formData = $('#newHospitalInfo').find('form');

        ResetFormForValidation($formData);
        validationStatus = $formData.valid();

        var exist = true;

        //for (var m = 0; $scope.Hospitals.length; m++) {

        //    if ($scope.Hospitals[m].HospitalName == addData.HospitalName) {
        //        $scope.hospitalExist = "The Hospital Name Exists";
        //        exist = false;
        //        break;
        //    }

        //    for (var n = 0; $scope.Hospitals[m].HospitalContactInfoes.length; n++) {

        //        if ($scope.Hospitals[m].HospitalContactInfoes[n].LocationName == addData.HospitalContactInfoViewModel.LocationName) {
        //            $scope.locationExist = "The Location Name Exists";
        //            exist = false;
        //            break;
        //        }

        //        for (var o = 0; $scope.Hospitals[m].HospitalContactInfoes[n].HospitalContactPersons.length; o++) {

        //            if ($scope.Hospitals[m].HospitalContactInfoes[n].HospitalContactPersons[o].ContactPersonName == addData.HospitalContactPersonViewModel.LocationName) {
        //                $scope.personExist = "The Contact Person Name Exists";
        //                exist = false;
        //                break;
        //            }
        //        }
        //    }
        //}
        
        if (validationStatus && exist) {
            $http.post('/MasterDataNew/AddHospital', addData).
                success(function (data, status, headers, config) {
                    //----------- success message -----------
                    if (data.status == "true") {
                        $scope.AddNewHospital = false;
                        $scope.tempHospital = {};
                        messageAlertEngine.callAlertMessage("Hospital", "New Hospital Details Added Successfully !!!!", "success", true);
                        //data.admitingPrivilege.LastModifiedDate = $scope.ConvertDateFormat(data.admitingPrivilege.LastModifiedDate);
                        for (var i = 0; i < $scope.Hospitals.length; i++) {

                            if ($scope.Hospitals[i].HospitalID == 0) {

                                $scope.Hospitals[i] = data.hospitalDetails;
                                $scope.Hospitals[i].LastModifiedDate = new Date();
                                $scope.Hospitals[i].HospitalContactInfoes[0].LastModifiedDate = new Date();
                                $scope.Hospitals[i].HospitalContactInfoes[0].HospitalContactPersons[0].LastModifiedDate = new Date();
                                
                                //$scope.Hospitals[i].HospitalName = angular.copy(data.hospitalDetails.HospitalName);
                                //$scope.Hospitals[i].Code = angular.copy(data.hospitalDetails.Code);                                
                                //$scope.Hospitals[i].LastModifiedDate = new Date();
                                //for (var j = 0; j < $scope.Hospitals[i].HospitalContactInfoes.length; j++) {
                                //    if ($scope.Hospitals[i].HospitalContactInfoes[j].HospitalContactInfoID == 0) {
                                //        //$scope.Hospitals[i].HospitalContactInfoes[j] = angular.copy(data.hospitalDetails.HospitalContactInfoViewModel);                                        
                                //        $scope.Hospitals[i].HospitalContactInfoes[j].LastModifiedDate = new Date();

                                //        for (var k = 0; k < $scope.Hospitals[i].HospitalContactInfoes[j].HospitalContactPersons.length; k++) {
                                //            if ($scope.Hospitals[i].HospitalContactInfoes[j].HospitalContactPersons[k].HospitalContactPersonID == 0) {
                                //                //$scope.Hospitals[i].HospitalContactInfoes[j] = angular.copy(data.hospitalDetails.HospitalContactPersonViewModel);                                                
                                //                $scope.Hospitals[i].HospitalContactInfoes[j].HospitalContactPersons[k].LastModifiedDate = new Date();
                                //            }

                                //        }
                                //    }

                                //}
                            }
                        }
                        $scope.reset();
                        $scope.error = "";
                        $scope.existErr = "";

                    }
                    else {
                        messageAlertEngine.callAlertMessage("HospitalAddError", "Sorry Unable To Add Hospital !!!!", "danger", true);

                    }
                }).
                error(function (data, status, headers, config) {
                    //----------- error message -----------
                    messageAlertEngine.callAlertMessage("HospitalAddError", "Sorry Unable To Add Hospital !!!!", "danger", true);

                });
        }
    }

       

    //------------------- update hospital ---------------------
    $scope.updateHospital = function (idx) {
        
        var updateHospital = {

            HospitalID: $scope.tempHospital.HospitalID,
            HospitalName: $scope.tempHospital.HospitalName,
            Code: $scope.tempHospital.Code,
            StatusType: 1
        }


        var exist = true;

        //for (var m = 0; $scope.Hospitals.length; m++) {

        //    if ($scope.Hospitals[m].HospitalName == updateHospital.HospitalName && $scope.Hospitals[m].HospitalID != updateHospital.HospitalID) {
        //        $scope.hospitalExist = "The Hospital Name Is Exist";
        //        exist = false;
        //        break;
        //    }

        //}

        if (!updateHospital.HospitalName) { $scope.hospitalError = "Please Enter The Hospital Name" };

        if (updateHospital.HospitalName && exist) {
            $http.post('/MasterDataNew/UpdateHospital', updateHospital).
                success(function (data, status, headers, config) {
                    //----------- success message -----------
                    if (data.status == "true") {
                        messageAlertEngine.callAlertMessage("Hospital", "Hospital Details Updated Successfully !!!!", "success", true);
                        //data.admitingPrivilege.LastModifiedDate = $scope.ConvertDateFormat(data.admitingPrivilege.LastModifiedDate);                    

                        $scope.Hospitals[idx].HospitalName = angular.copy(data.hospitalDetails.HospitalName);
                        $scope.Hospitals[idx].Code = angular.copy(data.hospitalDetails.Code);
                        $scope.Hospitals[idx].Status = "Active";
                        $scope.Hospitals[idx].LastModifiedDate = new Date();
                        $scope.reset();
                        $scope.error = "";
                        $scope.existErr = "";

                    }
                    else {
                        messageAlertEngine.callAlertMessage("HospitalUpdateError", "Sorry Unable To Update Hospital !!!!", "danger", true);

                    }
                }).
                error(function (data, status, headers, config) {
                    //----------- error message -----------
                    messageAlertEngine.callAlertMessage("HospitalUpdateError", "Sorry Unable To Update Hospital !!!!", "danger", true);

                });

        }
    };

    //----------- add new data ----------------
    $scope.addHospitalContacts = function () {
        var temp = {
            HospitalContactInfoID: 0,
            LocationName: "",
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
            HospitalContactPersons: [
                {
                    HospitalContactPersonID: 0,
                    ContactPersonName: "",
                    Phone: "",
                    Fax: "",
                    PhoneCountryCode: "",
                    FaxCountryCode: "",
                    Status: "Active",
                    LastModifiedDate: new Date,
                }
            ]
        };
        //$scope.HospitalContacts.splice(0, 0, angular.copy(temp));
        $scope.tempHC = angular.copy(temp);
        $scope.AddNewHospitalContact = true;
    };

    //-------------------- Edit Group ----------
    $scope.editHC = function (hc) {
        $scope.tempHC = angular.copy(hc);
    };

    //------------------- Save Group ---------------------
    $scope.saveHC = function () {

        var validationStatus;
        var $formData;

        var id = angular.copy($scope.hspId);
        console.log(id);
        var addData = {

                HospitalContactInfoID: 0,
                LocationName: $scope.tempHC.LocationName,
                UnitNumber: $scope.tempHC.UnitNumber,
                Street: $scope.tempHC.Street,
                Country: $scope.tempHC.Country,
                State: $scope.tempHC.State,
                County: $scope.tempHC.County,
                City: $scope.tempHC.City,
                ZipCode: $scope.tempHC.ZipCode,
                PhoneNumber: $scope.tempHC.PhoneNumber,
                FaxNumber: $scope.tempHC.FaxNumber,
                PhoneCountryCode: $scope.tempHC.PhoneCountryCode,
                FaxCountryCode: $scope.tempHC.FaxCountryCode,
                Email: $scope.tempHC.Email,
                StatusType: 1,
                HospitalContactPersonViewModel:
                {
                    HospitalContactPersonID: 0,
                    ContactPersonName: $scope.tempHC.HospitalContactPersons[0].ContactPersonName,
                    Phone: $scope.tempHC.HospitalContactPersons[0].Phone,
                    Fax: $scope.tempHC.HospitalContactPersons[0].Fax,
                    PhoneCountryCode: $scope.tempHC.HospitalContactPersons[0].PhoneCountryCode,
                    FaxCountryCode: $scope.tempHC.HospitalContactPersons[0].FaxCountryCode,
                    StatusType: 1
                }
        }

        $formData = $('#newHospitalContactInfo').find('form');

        ResetFormForValidation($formData);
        validationStatus = $formData.valid();

        if (!id) { $scope.hospNameError = "Please Select The Hospital Name" };

        var exist = true;

        //for (var m = 0; $scope.Hospitals.length; m++) {

        //    if ($scope.Hospitals[m].HospitalID == id) {

        //        for (var n = 0; $scope.Hospitals[m].HospitalContactInfoes.length; n++) {

        //            if ($scope.Hospitals[m].HospitalContactInfoes[n].LocationName == addData.HospitalContactInfoViewModel.LocationName) {
        //                $scope.locationExist = "The Location Name Is Exist";
        //                exist = false;
        //                break;
        //            }

        //            for (var o = 0; $scope.Hospitals[m].HospitalContactInfoes[n].HospitalContactPersons.length; o++) {

        //                if ($scope.Hospitals[m].HospitalContactInfoes[n].HospitalContactPersons[o].ContactPersonName == addData.HospitalContactPersonViewModel.LocationName) {
        //                    $scope.personExist = "The Contact Person Name Is Exist";
        //                    exist = false;
        //                    break;
        //                }
        //            }
        //        }
        //    }


        //}

        if (id && validationStatus && exist) {
            $http.post('/MasterDataNew/AddHospitalContactInfo?hospitalId=' + id, addData).
                success(function (data, status, headers, config) {
                    //----------- success message -----------
                    if (data.status == "true") {
                        $scope.AddNewHospitalContact = false;
                        $scope.tempHC = {};
                        messageAlertEngine.callAlertMessage("Contact", "New Hospital Contact Details Added Successfully !!!!", "success", true);

                        for (var i = 0; i < $scope.Hospitals.length; i++) {

                            if ($scope.Hospitals[i].HospitalID == id) {                                
                                data.contactDetails.LastModifiedDate = new Date();                                
                                data.contactDetails.HospitalContactPersons[0].LastModifiedDate = new Date();
                                $scope.Hospitals[i].HospitalContactInfoes.push(data.contactDetails);

                            }
                        }
                        $scope.reset();
                        $scope.error = "";
                        $scope.existErr = "";

                    }
                    else {
                        messageAlertEngine.callAlertMessage("ContactAddError", "Sorry Unable To Add Hospital Contact !!!!", "danger", true);

                    }
                }).
                error(function (data, status, headers, config) {
                    //----------- error message -----------
                    messageAlertEngine.callAlertMessage("ContactAddError", "Sorry Unable To Add Hospital Contact!!!!", "danger", true);

                });
        }
    };

    //$scope.checkSelected = function () {

    //    if (!$scope.SelectedHospitalID) { $scope.hospitalNameError ="Please Select The Hospital Name"};
    //    if (!$scope.SelectedLocationID) { $scope.locationnError = "Please Select The Location Name" };
    //}
       

    
    //------------------- update hospital ---------------------
    $scope.updateHC = function (idx) {

        var validationStatus;        
        var $formData;

        var id = angular.copy($scope.hspId);
        var updateContactInfo = {
            HospitalID :id,
            HospitalContactInfoID: $scope.tempHC.HospitalContactInfoID,
            LocationName: $scope.tempHC.LocationName,
            UnitNumber: $scope.tempHC.UnitNumber,
            Street: $scope.tempHC.Street,
            Country: $scope.tempHC.Country,
            State: $scope.tempHC.State,
            County: $scope.tempHC.County,
            City: $scope.tempHC.City,
            ZipCode: $scope.tempHC.ZipCode,
            PhoneNumber: $scope.tempHC.PhoneNumber,
            FaxNumber: $scope.tempHC.FaxNumber,
            PhoneCountryCode: $scope.tempHC.PhoneCountryCode,
            FaxCountryCode: $scope.tempHC.FaxCountryCode,
            Email: $scope.tempHC.Email,
            StatusType: 1,
        }

        $formData = $('#editHospitalContactInfo').find('form');
        ResetFormForValidation($formData);
        validationStatus = $formData.valid();

        var exist = true;

        //for (var m = 0; $scope.Hospitals.length; m++) {

        //    if ($scope.Hospitals[m].HospitalID == id) {

        //        for (var n = 0; $scope.Hospitals[m].HospitalContactInfoes.length; n++) {

        //            if ($scope.Hospitals[m].HospitalContactInfoes[n].LocationName == updateContactInfo.LocationName && $scope.Hospitals[m].HospitalContactInfoes[n].HospitalContactInfoID != updateContactInfo.HospitalContactInfoID) {
        //                $scope.locationExist = "The Location Name Is Exist";
        //                exist = false;
        //                break;
        //            }


        //        }
        //    }

        //}

        if (validationStatus && exist) {
            $http.post('/MasterDataNew/UpdateHospitalContactInfo', updateContactInfo).
                success(function (data, status, headers, config) {
                    //----------- success message -----------
                    if (data.status == "true") {
                        $scope.AddNewHospitalContact = false;
                        messageAlertEngine.callAlertMessage("Contact", "Hospital Contact Details Updated Successfully !!!!", "success", true);

                        for (var i = 0; i < $scope.Hospitals.length; i++) {
                            if ($scope.Hospitals[i].HospitalID == updateContactInfo.HospitalID) {

                                for (var j = 0; j < $scope.Hospitals[i].HospitalContactInfoes.length; j++) {

                                    if ($scope.Hospitals[i].HospitalContactInfoes[j].HospitalContactInfoID == updateContactInfo.HospitalContactInfoID) {

                                        $scope.Hospitals[i].HospitalContactInfoes[j].LocationName = angular.copy(data.HospitalContact.LocationName);
                                        $scope.Hospitals[i].HospitalContactInfoes[j].UnitNumber = angular.copy(data.HospitalContact.UnitNumber);                                        
                                        $scope.Hospitals[i].HospitalContactInfoes[j].LastModifiedDate = new Date();
                                        $scope.Hospitals[i].HospitalContactInfoes[j].Street = angular.copy(data.HospitalContact.Street);
                                        $scope.Hospitals[i].HospitalContactInfoes[j].Country = angular.copy(data.HospitalContact.Country);
                                        $scope.Hospitals[i].HospitalContactInfoes[j].State = angular.copy(data.HospitalContact.State);
                                        $scope.Hospitals[i].HospitalContactInfoes[j].County = angular.copy(data.HospitalContact.County);
                                        $scope.Hospitals[i].HospitalContactInfoes[j].City = angular.copy(data.HospitalContact.City);
                                        $scope.Hospitals[i].HospitalContactInfoes[j].ZipCode = angular.copy(data.HospitalContact.ZipCode);
                                        $scope.Hospitals[i].HospitalContactInfoes[j].PhoneNumber = angular.copy(data.HospitalContact.PhoneNumber);
                                        $scope.Hospitals[i].HospitalContactInfoes[j].FaxNumber = angular.copy(data.HospitalContact.FaxNumber);
                                        $scope.Hospitals[i].HospitalContactInfoes[j].Email = angular.copy(data.HospitalContact.Email);

                                    }
                                }

                            }

                        }


                        $scope.reset();
                        $scope.error = "";
                        $scope.existErr = "";

                    }
                    else {
                        messageAlertEngine.callAlertMessage("ContactUpdateError", "Sorry Unable To Update Hospital Contact !!!!", "danger", true);

                    }
                }).
                error(function (data, status, headers, config) {
                    //----------- error message -----------
                    messageAlertEngine.callAlertMessage("ContactUpdateError", "Sorry Unable To Update Hospital Contact !!!!", "danger", true);

                });

        }
    };

    //----------- add new data ----------------
    $scope.addHCP = function () {
        var temp = {
            HospitalContactPersonID: 0,
            ContactPersonName: "",
            Phone: "",
            Fax: "",
            PhoneCountryCode: "",
            FaxCountryCode: "",
            Status: "Active",
            LastModifiedDate: new Date(),
        };

        $scope.HospitalContactPersons.splice(0, 0, angular.copy(temp));
        $scope.tempHCP = angular.copy(temp);
        $scope.disablePersonAdd = true;
        $scope.disablePersonEdit = true;
    };

    //-------------------- Edit Person ----------
    $scope.editHCP = function (hcp) {
        $scope.tempHCP = angular.copy(hcp);
        $scope.disablePersonAdd = true;
    };

    //------------------- Save Group ---------------------
    $scope.saveHCP = function (idx) {

        var prsnId = angular.copy($scope.personId);
        var id = angular.copy($scope.hspId1);
        var addData = {
            HospitalContactPersonID: 0,
            ContactPersonName: $scope.tempHCP.ContactPersonName,
            Phone: $scope.tempHCP.Phone,
            Fax: $scope.tempHCP.Fax,
            PhoneCountryCode: $scope.tempHCP.PhoneCountryCode,
            FaxCountryCode: $scope.tempHCP.FaxCountryCode,
            StatusType: 1
        }

        var exist = true;

        //for (var m = 0; $scope.Hospitals.length; m++) {

        //    if ($scope.Hospitals[m].HospitalID == id) {

        //        for (var n = 0; $scope.Hospitals[m].HospitalContactInfoes.length; n++) {

        //            if ($scope.Hospitals[m].HospitalContactInfoes[n].HospitalContactInfoID == contactId) {

        //                for (var o = 0; $scope.Hospitals[m].HospitalContactInfoes[n].HospitalContactPersons.length; o++) {

        //                    if ($scope.Hospitals[m].HospitalContactInfoes[n].HospitalContactPersons[o].ContactPersonName == addData.ContactPersonName) {
        //                        $scope.personExist = "The Contact Person Name Is Exist";
        //                        exist = false;
        //                        break;
        //                    }
        //                }
        //            }


        //        }
        //    }
        //}

        if (!addData.ContactPersonName) { $scope.personError = "Please Enter Person Name"; };
        if (!addData.Phone) { $scope.phoneError = "Please Enter Phone Number"; };

        if (addData.ContactPersonName && addData.Phone && exist) {
            $http.post('/MasterDataNew/AddHospitalContactPerson?hospitalId=' + id + '&contactId=' + prsnId, addData).
                success(function (data, status, headers, config) {
                    //----------- success message -----------
                    if (data.status == "true") {

                        messageAlertEngine.callAlertMessage("Person", "Hospital Contact Person Details Added Successfully !!!!", "success", true);

                        for (var i = 0; i < $scope.Hospitals.length; i++) {

                            if ($scope.Hospitals[i].HospitalID == id) {

                                for (var j = 0; j < $scope.Hospitals[i].HospitalContactInfoes.length; j++) {
                                    if ($scope.Hospitals[i].HospitalContactInfoes[j].HospitalContactInfoID == prsnId) {

                                        for (var k = 0; k < $scope.Hospitals[i].HospitalContactInfoes[j].HospitalContactPersons.length; k++) {
                                            if ($scope.Hospitals[i].HospitalContactInfoes[j].HospitalContactPersons[k].HospitalContactPersonID == 0) {
                                                $scope.Hospitals[i].HospitalContactInfoes[j].HospitalContactPersons[k].HospitalContactPersonID = angular.copy(data.personDetails.HospitalContactPersonID);
                                                $scope.Hospitals[i].HospitalContactInfoes[j].HospitalContactPersons[k].ContactPersonName = angular.copy(data.personDetails.ContactPersonName);
                                                $scope.Hospitals[i].HospitalContactInfoes[j].HospitalContactPersons[k].ContactPersonPhone = angular.copy(data.personDetails.ContactPersonPhone);
                                                $scope.Hospitals[i].HospitalContactInfoes[j].HospitalContactPersons[k].ContactPersonFax = angular.copy(data.personDetails.ContactPersonFax);
                                                $scope.Hospitals[i].HospitalContactInfoes[j].HospitalContactPersons[k].Phone = angular.copy(data.personDetails.Phone);
                                                $scope.Hospitals[i].HospitalContactInfoes[j].HospitalContactPersons[k].Fax = angular.copy(data.personDetails.Fax);
                                                $scope.Hospitals[i].HospitalContactInfoes[j].HospitalContactPersons[k].LastModifiedDate = new Date();
                                            }

                                        }
                                    }

                                }
                            }
                        }

                        $scope.reset();
                        $scope.error = "";
                        $scope.existErr = "";

                    }
                    else {
                        messageAlertEngine.callAlertMessage("PersonError", "Sorry Unable To Add Hospital Contact Person !!!!", "danger", true);
                        $scope.HospitalContactPersons.splice(0, 1);
                    }
                }).
                error(function (data, status, headers, config) {
                    //----------- error message -----------
                    messageAlertEngine.callAlertMessage("PersonError", "Sorry Unable To Add Hospital Contact Person !!!!", "danger", true);
                    $scope.HospitalContactPersons.splice(0, 1);
                });
        }
    };

    $scope.updateHCP = function (idx) {
        var prsnId = angular.copy($scope.personId);
        var id = angular.copy($scope.hspId1);
        var updatePerson =
        {
            HospitalContactPersonID: $scope.tempHCP.HospitalContactPersonID,
            ContactPersonName: $scope.tempHCP.ContactPersonName,
            Phone: $scope.tempHCP.Phone,
            Fax: $scope.tempHCP.Fax,
            PhoneCountryCode: $scope.tempHCP.PhoneCountryCode,
            FaxCountryCode: $scope.tempHCP.FaxCountryCode,
            StatusType:1,
            
        }

        var exist = true;

        //for (var m = 0; $scope.Hospitals.length; m++) {

        //    if ($scope.Hospitals[m].HospitalID == id) {

        //        for (var n = 0; $scope.Hospitals[m].HospitalContactInfoes.length; n++) {

        //            if ($scope.Hospitals[m].HospitalContactInfoes[n].HospitalContactInfoID == contactId) {

        //                for (var o = 0; $scope.Hospitals[m].HospitalContactInfoes[n].HospitalContactPersons.length; o++) {

        //                    if ($scope.Hospitals[m].HospitalContactInfoes[n].HospitalContactPersons[o].ContactPersonName == updatePerson.ContactPersonName) {
        //                        $scope.personExist = "The Contact Person Name Is Exist";
        //                        exist = false;
        //                        break;
        //                    }
        //                }
        //            }


        //        }
        //    }
        //}

        if (!updatePerson.ContactPersonName) { $scope.personError = "Please Enter Person Name"; };
        if (!updatePerson.Phone) { $scope.phoneError = "Please Enter Phone Number"; };

        if (updatePerson.ContactPersonName && updatePerson.Phone && exist) {

            $http.post('/MasterDataNew/UpdateHospitalContactPerson', updatePerson).
                success(function (data, status, headers, config) {
                    //----------- success message -----------
                    if (data.status == "true") {
                        messageAlertEngine.callAlertMessage("Person", "Hospital Contact Person Details Updated Successfully !!!!", "success", true);

                        for (var i = 0; i < $scope.Hospitals.length; i++) {

                            if ($scope.Hospitals[i].HospitalID == id) {

                                for (var j = 0; j < $scope.Hospitals[i].HospitalContactInfoes.length; j++) {
                                    if ($scope.Hospitals[i].HospitalContactInfoes[j].HospitalContactInfoID == prsnId) {

                                        for (var k = 0; k < $scope.Hospitals[i].HospitalContactInfoes[j].HospitalContactPersons.length; k++) {
                                            if ($scope.Hospitals[i].HospitalContactInfoes[j].HospitalContactPersons[k].HospitalContactPersonID == data.HospitalContactPerson.HospitalContactPersonID) {
                                                $scope.Hospitals[i].HospitalContactInfoes[j].HospitalContactPersons[k].ContactPersonName = angular.copy(data.HospitalContactPerson.ContactPersonName);
                                                $scope.Hospitals[i].HospitalContactInfoes[j].HospitalContactPersons[k].Phone = angular.copy(data.HospitalContactPerson.Phone);
                                                $scope.Hospitals[i].HospitalContactInfoes[j].HospitalContactPersons[k].Fax = angular.copy(data.HospitalContactPerson.Fax);
                                                $scope.Hospitals[i].HospitalContactInfoes[j].HospitalContactPersons[k].PhoneCountryCode = angular.copy(data.HospitalContactPerson.PhoneCountryCode);
                                                $scope.Hospitals[i].HospitalContactInfoes[j].HospitalContactPersons[k].FaxCountryCode = angular.copy(data.HospitalContactPerson.FaxCountryCode);
                                                $scope.Hospitals[i].HospitalContactInfoes[j].HospitalContactPersons[k].ContactPersonPhone = angular.copy(data.HospitalContactPerson.ContactPersonPhone);
                                                $scope.Hospitals[i].HospitalContactInfoes[j].HospitalContactPersons[k].ContactPersonFax = angular.copy(data.HospitalContactPerson.ContactPersonFax);
                                                $scope.Hospitals[i].HospitalContactInfoes[j].HospitalContactPersons[k].Status = "Active";
                                                $scope.Hospitals[i].HospitalContactInfoes[j].HospitalContactPersons[k].LastModifiedDate = new Date();
                                            }

                                        }
                                    }

                                }
                            }
                        }

                        $scope.reset();
                        $scope.error = "";
                        $scope.existErr = "";

                    }
                    else {
                        messageAlertEngine.callAlertMessage("PersonError", "Sorry Unable To Update Hospital Contact Person !!!!", "danger", true);

                    }
                }).
                error(function (data, status, headers, config) {
                    //----------- error message -----------
                    messageAlertEngine.callAlertMessage("PersonError", "Sorry Unable To Update Hospital Contact Person !!!!", "danger", true);

                });

        }
        
    };

    //----------------- Group new add cancel ---------------
    $scope.cancelAdd = function () {        
        //$scope.HospitalContactPersons.splice(0, 1);
        $scope.tempHCP = {};
        $scope.hospitalExist = "";
        $scope.locationExist = "";
        $scope.personExist = "";              
        $scope.disableAdd = false;
        $scope.disablePersonAdd = false;
        $scope.disablePersonEdit = false;
    };

    //-------------------- Reset Group ----------------------
    $scope.reset = function () {
        //$scope.Hospitals.splice(0, 1);
        $scope.tempHospital = {};
        $scope.tempHC = {};
        $scope.tempHCP = {};
        $scope.AddNewHospital = false;
        $scope.AddNewHospitalContact = false;
        $scope.AddNewHCP = false;
        $scope.hospitalExist = "";
        $scope.locationExist = "";
        $scope.personExist = "";        
        $scope.disableAdd = false;
        $scope.disablePersonAdd = false;
        $scope.disablePersonEdit = false;
    };

    //------------------------ hide show manage ----------------------
    $scope.viewHospitals = true;

    $scope.showHospitals = function () {
        $scope.viewHospitals = true;
        $scope.viewHospitalContacts = false;
        $scope.viewHospitalContactPersons = false;
        $scope.hspId1 = "";
        $scope.personId = "";
    };

    $scope.showHospitalContacts = function () {
        $scope.viewHospitals = false;
        $scope.viewHospitalContacts = true;
        $scope.viewHospitalContactPersons = false;
        $scope.hspId1 = "";
        $scope.personId = "";
    };

    $scope.showHospitalContactPersons = function () {
        $scope.viewHospitals = false;
        $scope.viewHospitalContacts = false;
        $scope.viewHospitalContactPersons = true;
        $scope.hspId1 = "";
        $scope.personId = "";
    };

    //----------------- get Hospital Contacts ---------------------
    $scope.getHospitalContacts = function (hospitalId, condition) {
        $scope.hspId = angular.copy(hospitalId);

        $scope.HospitalContacts = [];
        for (var i in $scope.Hospitals) {
            if ($scope.Hospitals[i].HospitalID == hospitalId) {
                $scope.HospitalContacts = $scope.Hospitals[i].HospitalContactInfoes;
                break;
            }
        }
        if (condition) {
            $scope.HospitalContactPersons = [];
            $scope.SelectedLocationID = "";
        }
    };

    //----------------- get Hospital Contacts ---------------------
    $scope.getHospitalContacts1 = function (hospitalId, condition) {
        $scope.hspId1 = angular.copy(hospitalId);
        $scope.personId = "";
        $scope.HospitalContacts = [];
        for (var i in $scope.Hospitals) {
            if ($scope.Hospitals[i].HospitalID == hospitalId) {
                $scope.HospitalContacts = $scope.Hospitals[i].HospitalContactInfoes;
                break;
            }
        }
        if (condition) {
            $scope.HospitalContactPersons = [];
            $scope.SelectedLocationID = "";
        }
    };

    //----------------- get Hospital Contacts ---------------------
    $scope.getHospitalContactPersons = function (locatoionId) {
        $scope.personId = angular.copy(locatoionId);
        $scope.HospitalContactPersons = [];
        for (var i in $scope.HospitalContacts) {
            if ($scope.HospitalContacts[i].HospitalContactInfoID == locatoionId) {
                $scope.HospitalContactPersons = $scope.HospitalContacts[i].HospitalContactPersons;
                break;
            }
        }
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

    //-------------- current page change Scope Watch ---------------------
    $scope.$watch('bigCurrentPage', function (newValue, oldValue) {
        $scope.CurrentPage = [];
        var startIndex = (newValue - 1) * 10;
        var endIndex = startIndex + 9;
        if ($scope.Hospitals) {
            for (startIndex; startIndex <= endIndex ; startIndex++) {
                if ($scope.Hospitals[startIndex]) {
                    $scope.CurrentPage.push($scope.Hospitals[startIndex]);
                } else {
                    break;
                }
            }
        }
        //console.log($scope.CurrentPageProviders);
    });
    //-------------- License Scope Watch ---------------------
    $scope.$watchCollection('Hospitals', function (newValue, oldValue) {
        if (newValue) {
            $scope.bigTotalItems = newValue.length;

            $scope.CurrentPage = [];
            $scope.bigCurrentPage = 1;

            var startIndex = ($scope.bigCurrentPage - 1) * 10;
            var endIndex = startIndex + 9;

            for (startIndex; startIndex <= endIndex ; startIndex++) {
                if ($scope.Hospitals[startIndex]) {
                    $scope.CurrentPage.push($scope.Hospitals[startIndex]);
                } else {
                    break;
                }
            }
            //console.log($scope.CurrentPageProviders);
        }
    });
    //------------------- end ------------------

    

}]);



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
    $(".countryDailCodeContainer").hide();
    // method will close any other country code div already open.
};

$(document).ready(function () {
    $(".countryDailCodeContainer").hide();
    $(".ProviderTypeSelectAutoList").hide();

});

function showLocationList(ele) {
    $(ele).parent().find(".ProviderTypeSelectAutoList").first().show();
}