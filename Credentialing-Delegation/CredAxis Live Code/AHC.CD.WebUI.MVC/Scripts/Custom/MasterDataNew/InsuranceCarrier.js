//--------------------- Angular Module ----------------------
var masterDataInsuranceCarriers = angular.module("masterDataInsuranceCarriers", ["ahc.cd.util", 'ui.bootstrap']);

//Service for getting master data
masterDataInsuranceCarriers.service('masterDataService', ['$http', '$q', function ($http, $q) {

    this.getMasterData = function (URL) {
        return $http.get(URL).then(function (value) { return value.data; });
    };

}]);

masterDataInsuranceCarriers.service('locationService', ['$http', function ($http) {
    //location service to return array of locations relevant to the querystring
    this.getLocations = function (QueryString) {
        return $http.get(rootDir + "/Location/GetCities?city=" + QueryString)     ///locationService makes an ajax call to fetch all the cities which have relevent names as the query string.
        .then(function (response) { return response.data; });     //Which is then returned to the controller method which called the service
    };
}]);

masterDataInsuranceCarriers.service('messageAlertEngine', ['$rootScope', '$timeout', function ($rootScope, $timeout) {

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
}])


//=========================== Controller declaration ==========================
masterDataInsuranceCarriers.controller('masterDataInsuranceCarriersController', ['$scope', '$http', '$filter', 'masterDataService', 'messageAlertEngine', 'locationService', function ($scope, $http, $filter, masterDataService, messageAlertEngine, locationService) {

    
    //------------------------------------------------------------------Address Auto-Complete---------------------------------------------------------------------------//

    /* Method addressAutocomplete() gets the details of a location
         Method takes input of location details entered in the text box.*/

    $scope.addressAutocomplete = function (location) {

        if (location.length == 0) {
            $scope.resetAddressModels1();
        }

        $scope.tempInsuranceCarrier.InsuranceCarrierAddress[0].City = location;
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
        $scope.tempInsuranceCarrier.InsuranceCarrierAddress[0].City = "";
        $scope.tempInsuranceCarrier.InsuranceCarrierAddress[0].State = "";
        $scope.tempInsuranceCarrier.InsuranceCarrierAddress[0].Country = "";
    };

    $scope.setAddressModels = function (location) {
        $scope.tempInsuranceCarrier.InsuranceCarrierAddress[0].City = location.City;
        $scope.tempInsuranceCarrier.InsuranceCarrierAddress[0].State = location.State;
        $scope.tempInsuranceCarrier.InsuranceCarrierAddress[0].Country = location.Country;

    }


    $scope.addressAutocomplete1 = function (location) {

        if (location.length == 0) {
            $scope.resetAddressModels1();
        }

        $scope.tempInsuranceCarrierAddress.City = location;
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
        $scope.tempInsuranceCarrierAddress.City = "";
        $scope.tempInsuranceCarrierAddress.State = "";
        $scope.tempInsuranceCarrierAddress.Country = "";
    };

    $scope.setAddressModels1 = function (location) {
        $scope.tempInsuranceCarrierAddress.City = location.City;
        $scope.tempInsuranceCarrierAddress.State = location.State;
        $scope.tempInsuranceCarrierAddress.Country = location.Country;

    }





    $scope.addressAutocomplete2 = function (location) {

        if (location.length == 0) {
            $scope.resetAddressModels2();
        }
       
        $scope.tempInsuranceCarrierAddress.City = location;
        if (location.length > 1 && !angular.isObject(location)) {
            locationService.getLocations(location).then(function (val) {
                $scope.Locations = val;
            });
        } else if (angular.isObject(location)) {
            $scope.setAddressModels2(location);
        }
    };



    $scope.selectedLocation2 = function (location) {
        $scope.setAddressModels2(location);
        $(".ProviderTypeSelectAutoList").hide();
    };

    $scope.resetAddressModels2 = function () {
        $scope.tempInsuranceCarrierAddress.City = "";
        $scope.tempInsuranceCarrierAddress.State = "";
        $scope.tempInsuranceCarrierAddress.Country = "";
    };

    $scope.setAddressModels2 = function (location) {
        $scope.tempInsuranceCarrierAddress.City = location.City;
        $scope.tempInsuranceCarrierAddress.State = location.State;
        $scope.tempInsuranceCarrierAddress.Country = location.Country;
        

    }

    //----------------------------------------------------------------------------------------------------------------------------------------------------------------//  

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


    $http.get(rootDir + "/MasterDataNew/GetAllInsuranceCarriers").then(function (value) {
       
        for (var i = 0; i < value.data.length ; i++) {
            if (value.data[i].Code == null)
            {
                value.data[i].Code = "";
            }
            if (value.data[i] != null) {
                value.data[i].LastModifiedDate = $scope.ConvertDateFormat(value.data[i].LastModifiedDate);

                for (var j = 0; j < value.data[i].InsuranceCarrierAddresses.length ; j++) {
                    value.data[i].InsuranceCarrierAddresses[j].LastModifiedDate = $scope.ConvertDateFormat(value.data[i].InsuranceCarrierAddresses[j].LastModifiedDate);
                    //$scope.data[i].NoofLocations = value.data[i].InsuranceCarrierAddresses.length;
                }

            }
        }

        $scope.InsuranceCarriers = angular.copy(value.data);
        for (var i in $scope.InsuranceCarriers) {
            if ($scope.InsuranceCarriers[i].InsuranceCarrierAddresses != null) {
                $scope.InsuranceCarriers[i].NoofLocations = $scope.InsuranceCarriers[i].InsuranceCarrierAddresses.length;
            }
            else {
                $scope.InsuranceCarriers[i].NoofLocations = 0;
            }
        }
    });

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



    $scope.tempInsuranceCarrier = {};
    $scope.tempInsuranceCarrierAddress = {};

    //------------ InsuranceCarrier Template return -------------------
    $scope.getInsuranceCarrierTemplate = function (InsuranceCarrier) {
        if (InsuranceCarrier.InsuranceCarrierID === $scope.tempInsuranceCarrier.InsuranceCarrierID) return 'editInsuranceCarrier';
        else return 'displayInsuranceCarrier';
    };

    //------------ InsuranceCarrier Address Template return -------------------
    $scope.getHCTemplate = function (hc) {
        if (hc.InsuranceCarrierAddressID === $scope.tempInsuranceCarrierAddress.InsuranceCarrierAddressID) return 'editHC';
        else return 'displayHC';

    };

    //-------------------- Edit InsuranceCarrier ----------
    $scope.editInsuranceCarrier = function (InsuranceCarrier) {
        $scope.tempInsuranceCarrier = angular.copy(InsuranceCarrier);
    };

    //------------------- Add InsuranceCarrier ---------------------
    $scope.addInsuranceCarrier = function () {
        var temp = {
            InsuranceCarrierID: 0,
            Name: "",
            Code: "",
            Status: "Active",
            StatusType: 1,
            LastModifiedDate: new Date(),
            InsuranceCarrierAddress: [
                {
                    InsuranceCarrierAddressID: 0,
                    LocationName: "",
                    Building: "",
                    Street: "",
                    Country: "",
                    State: "",
                    County: "",
                    City: "",
                    ZipCode: "",
                    Status: "Active",
                    LastModifiedDate: new Date()

                }
            ]
        };
        //$scope.InsuranceCarriers.push(angular.copy(temp));
        $scope.tempInsuranceCarrier = angular.copy(temp);
        $scope.AddNewInsuranceCarrier = true;
    };
    //------------------- Save InsuranceCarrier ---------------------
    $scope.saveInsuranceCarrier = function () {

        var validationStatus;
        var $formData;

        var exist = true;
        var addData = {
            InsuranceCarrierID: 0,
            Name: $scope.tempInsuranceCarrier.Name,
            Code: $scope.tempInsuranceCarrier.Code,
            StatusType: 1,
           
            InsuranceCarrierAddress:
                {
                    InsuranceCarrierAddressID: 0,
                    LocationName: $scope.tempInsuranceCarrier.InsuranceCarrierAddress[0].LocationName,
                    Building: $scope.tempInsuranceCarrier.InsuranceCarrierAddress[0].Building,
                    Street: $scope.tempInsuranceCarrier.InsuranceCarrierAddress[0].Street,
                    Country: $scope.tempInsuranceCarrier.InsuranceCarrierAddress[0].Country,
                    State: $scope.tempInsuranceCarrier.InsuranceCarrierAddress[0].State,
                    County: $scope.tempInsuranceCarrier.InsuranceCarrierAddress[0].County,
                    City: $scope.tempInsuranceCarrier.InsuranceCarrierAddress[0].City,
                    ZipCode: $scope.tempInsuranceCarrier.InsuranceCarrierAddress[0].ZipCode,
                    StatusType: 1,
                    

                }

        };

        $formData = $('#newInsuranceInfo').find('form');

        ResetFormForValidation($formData);
        validationStatus = $formData.valid();

        for (var i = 0; i < $scope.InsuranceCarriers.length; i++) {
            if (addData.Name && $scope.InsuranceCarriers[i].Name.replace(" ", "").toLowerCase() == addData.Name.replace(" ", "").toLowerCase()) {

                exist = false;
                $scope.existErr = "The Name is Exist";
                break;
            }
        }

        if (validationStatus && exist) {
            $http.post(rootDir + '/MasterDataNew/AddInsuranceCarrier', addData).
                success(function (data, status, headers, config) {
                    try {
                        //----------- success message -----------
                        if (data.status == "true") {
                            $scope.AddNewInsuranceCarrier = false;

                            messageAlertEngine.callAlertMessage("Carrier", "Insurance Carrier Details Added Successfully !!!!", "success", true);


                            data.insuranceDetails.LastModifiedDate = $scope.ConvertDateFormat(data.insuranceDetails.LastModifiedDate);
                            data.insuranceDetails.InsuranceCarrierAddresses.LastModifiedDate = $scope.ConvertDateFormat(data.insuranceDetails.InsuranceCarrierAddresses.LastModifiedDate);

                            $scope.InsuranceCarriers.push(data.insuranceDetails);
                            $scope.reset();


                        }
                        else {
                            messageAlertEngine.callAlertMessage("CarrierAddError", "Sorry Unable To Add Insurance Carrier !!!!", "danger", true);

                        }
                    } catch (e) {
                      
                    }
                }).
                error(function (data, status, headers, config) {
                    //----------- error message -----------
                    messageAlertEngine.callAlertMessage("CarrierAddError", "Sorry Unable To Add Insurance Carrier !!!!", "danger", true);

                });
        }

    };
    //------------------- update InsuranceCarrier ---------------------
    $scope.updateInsuranceCarrier = function (idx) {

        var exist = true;

        var updateData = {
            InsuranceCarrierID: $scope.tempInsuranceCarrier.InsuranceCarrierID,
            Name: $scope.tempInsuranceCarrier.Name,
            Code: $scope.tempInsuranceCarrier.Code,
            StatusType: 1,

        };

        for (var i = 0; i < $scope.InsuranceCarriers.length; i++) {
            if (updateData.Name && $scope.InsuranceCarriers[i].InsuranceCarrierID != updateData.InsuranceCarrierID && $scope.InsuranceCarriers[i].Name.replace(" ", "").toLowerCase() == updateData.Name.replace(" ", "").toLowerCase()) {

                exist = false;
                $scope.existErr = "The Name is Exist";
                break;
            }
            //if ( $scope.InsuranceCarriers[i].InsuranceCarrierID != updateData.InsuranceCarrierID) {
                
            //    idx = i;
            //    }
        }

        if (!updateData.Name) { $scope.insuranceError = "Please enter Insurance Carrier Name"; };

        if (updateData.Name && exist) {
            $http.post(rootDir + '/MasterDataNew/UpdateInsuranceCarrier', updateData).
                success(function (data, status, headers, config) {
                    try {
                        //----------- success message -----------
                        if (data.status == "true") {
                            $scope.AddNewInsuranceCarrier = false;
                            $scope.tempInsuranceCarrier = {};
                            messageAlertEngine.callAlertMessage("Carrier", "Insurance Carrier Details Updated Successfully !!!!", "success", true);

                            //for (var i = 0; i < $scope.InsuranceCarriers.length; i++) {
                            //    if ($scope.InsuranceCarriers[i].Code == null)


                            //    if ($scope.InsuranceCarriers[i].InsuranceCarrierID == updateData.InsuranceCarrierID) {

                            //        $scope.InsuranceCarriers[i].Name = data.insuranceDetails.Name;
                            //        $scope.InsuranceCarriers[i].Code = data.insuranceDetails.Code;
                            //        $scope.InsuranceCarriers[i].LastModifiedDate = new Date();
                            //        $scope.InsuranceCarriers[i].Status = "Active";

                            //    }
                            //}

                            $scope.InsuranceCarriers[idx].Name = data.insuranceDetails.Name;
                            $scope.InsuranceCarriers[idx].Code = data.insuranceDetails.Code;
                            $scope.InsuranceCarriers[idx].LastModifiedDate = new Date();
                            $scope.InsuranceCarriers[idx].Status = "Active";

                            $scope.reset();
                        }
                        else {
                            messageAlertEngine.callAlertMessage("CarrierUpdateError", "Sorry Unable To Update Insurance Carrier !!!!", "danger", true);

                        }
                    } catch (e) {
                     
                    }
                }).
                error(function (data, status, headers, config) {
                    //----------- error message -----------
                    messageAlertEngine.callAlertMessage("CarrierUpdateError", "Sorry Unable To Update Insurance Carrier !!!!", "danger", true);

                });
        }
    };

    //----------- add new data ----------------
    $scope.addInsuranceCarrierAddresss = function () {
        var temp = {
            InsuranceCarrierAddressID: 0,
            LocationName: "",
            Building: "",
            Street: "",
            Country: "",
            State: "",
            County: "",
            City: "",
            ZipCode: "",
            Phone: "",
            Fax: "",
            Email: "",
            Status: "Active",
            LastModifiedDate: new Date(),

        };

        $scope.tempInsuranceCarrierAddress = angular.copy(temp);

        $scope.AddNewInsuranceCarrierAddress = true;
        $(".ProviderTypeSelectAutoList").hide();
    };

    //-------------------- Edit Insurance Carrier Address ----------
    $scope.editHC = function (hc) {
        $scope.tempInsuranceCarrierAddress = angular.copy(hc);
        $(".ProviderTypeSelectAutoList").hide();

    };

    //------------------- Save Insurance Carrier Address ---------------------
    $scope.saveHC = function () {

        var validationStatus;
        var $formData;

        var exist = true;
        var id = $scope.carrierId;
        var addData = {
            InsuranceCarrierAddressID: 0,
            LocationName: $scope.tempInsuranceCarrierAddress.LocationName,
            Building: $scope.tempInsuranceCarrierAddress.Building,
            Street: $scope.tempInsuranceCarrierAddress.Street,
            Country: $scope.tempInsuranceCarrierAddress.Country,
            State: $scope.tempInsuranceCarrierAddress.State,
            County: $scope.tempInsuranceCarrierAddress.County,
            City: $scope.tempInsuranceCarrierAddress.City,
            ZipCode: $scope.tempInsuranceCarrierAddress.ZipCode,
            StatusType: 1,
            

            
        };
       

        $formData = $('#newInsuranceCarrierAddressInfo').find('form');

        ResetFormForValidation($formData);
        validationStatus = $formData.valid();

        if (!id) { $scope.insuranceNameError = "Please select the Insurance Carrier Name"; };

        if (id && validationStatus && exist) {
            $http.post(rootDir + '/MasterDataNew/AddInsuranceCarrierAddress?insuranceId=' + id, addData).
                success(function (data, status, headers, config) {
                    try {
                        //----------- success message -----------
                        if (data.status == "true") {
                            $scope.AddNewInsuranceCarrierAddress = false;
                            $scope.tempInsuranceCarrierAddress = {};
                            messageAlertEngine.callAlertMessage("Address", "New Insurance Carrier Address Details Added Successfully !!!!", "success", true);

                            for (var i = 0; i < $scope.InsuranceCarriers.length; i++) {

                                if ($scope.InsuranceCarriers[i].InsuranceCarrierID == id) {

                                    data.addressDetails.LastModifiedDate = $scope.ConvertDateFormat(data.addressDetails.LastModifiedDate);
                                    $scope.InsuranceCarriers[i].InsuranceCarrierAddresses.push(data.addressDetails);
                                    


                                }
                            }

                            $scope.reset();
                        }
                        else {
                            messageAlertEngine.callAlertMessage("AddressAddError", "Sorry Unable To Add Insurance Carrier Address !!!!", "danger", true);

                        }
                        $(".ProviderTypeSelectAutoList").hide();

                    } catch (e) {
                      
                    }
                }).
                error(function (data, status, headers, config) {
                    //----------- error message -----------
                    messageAlertEngine.callAlertMessage("AddressAddError", "Sorry Unable To Add Insurance Carrier Address !!!!", "danger", true);

                });
        }
    };

    //------------------- update InsuranceCarrier ---------------------
    $scope.updateHC = function (idx) {

        var validationStatus;
        var $formData;

        var exist = true;
        var id = $scope.carrierId;
        var updateData = {
            InsuranceCarrierAddressID: $scope.tempInsuranceCarrierAddress.InsuranceCarrierAddressID,
            LocationName: $scope.tempInsuranceCarrierAddress.LocationName,
            Building: $scope.tempInsuranceCarrierAddress.Building,
            Street: $scope.tempInsuranceCarrierAddress.Street,
            Country: $scope.tempInsuranceCarrierAddress.Country,
            State: $scope.tempInsuranceCarrierAddress.State,
            County: $scope.tempInsuranceCarrierAddress.County,
            City: $scope.tempInsuranceCarrierAddress.City,
            ZipCode: $scope.tempInsuranceCarrierAddress.ZipCode,
            StatusType: 1

        };

        $formData = $('#editInsuranceCarrierAddressInfo').find('form');

        ResetFormForValidation($formData);
        validationStatus = $formData.valid();

        if (validationStatus && exist) {
            $http.post(rootDir + '/MasterDataNew/UpdateInsuranceCarrierAddress', updateData).
                success(function (data, status, headers, config) {
                    try {
                        //----------- success message -----------
                        if (data.status == "true") {
                            $scope.AddNewInsuranceCarrierAddress = false;
                            messageAlertEngine.callAlertMessage("Address", "Insurance Carrier Address Details Updated Successfully !!!!", "success", true);

                            for (var i = 0; i < $scope.InsuranceCarriers.length; i++) {

                                if ($scope.InsuranceCarriers[i].InsuranceCarrierID == id) {

                                    data.addressDetails.LastModifiedDate = $scope.ConvertDateFormat(data.addressDetails.LastModifiedDate);
                                    for (var j = 0; $scope.InsuranceCarriers[i].InsuranceCarrierAddresses.length; j++) {

                                        if ($scope.InsuranceCarriers[i].InsuranceCarrierAddresses[j].InsuranceCarrierAddressID == data.addressDetails.InsuranceCarrierAddressID) {
                                            $scope.InsuranceCarriers[i].InsuranceCarrierAddresses[j] = data.addressDetails;
                                            break;
                                        }
                                    }
                                }
                            }

                            $scope.reset();
                        }
                        else {
                            messageAlertEngine.callAlertMessage("AddressUpdateError", "Sorry Unable To Update Insurance Carrier Address !!!!", "danger", true);

                        }
                        $(".ProviderTypeSelectAutoList").hide();

                    } catch (e) {
                      
                    }
                }).
                error(function (data, status, headers, config) {
                    //----------- error message -----------
                    messageAlertEngine.callAlertMessage("AddressUpdateError", "Sorry Unable To Update Insurance Carrier Address !!!!", "danger", true);

                });
        }
    };




    //----------------- Group new add cancel ---------------
    $scope.cancelAdd = function () {
        $scope.InsuranceCarriers.splice(0, 1);
        $scope.tempInsuranceCarrier = {};
        $scope.insuranceNameError = "";
        $(".ProviderTypeSelectAutoList").hide();

    };

    //-------------------- Reset Group ----------------------
    $scope.reset = function () {
        $scope.tempInsuranceCarrier = {};
        $scope.tempInsuranceCarrierAddress = {};
        $scope.AddNewInsuranceCarrier = false;
        $scope.AddNewInsuranceCarrierAddress = false;
        $scope.insuranceNameError = "";
        $scope.insuranceError = "";
        $scope.existErr = "";
        $(".ProviderTypeSelectAutoList").hide();

    };

    //------------------------ hide show manage ----------------------
    $scope.viewInsuranceCarriers = true;

    $scope.showInsuranceCarriers = function () {
        $scope.viewInsuranceCarriers = true;
        $scope.viewInsuranceCarrierAddresss = false;

    };

    $scope.showInsuranceCarrierAddresss = function () {
        $scope.carrierId = false;
        $scope.viewInsuranceCarriers = false;
        $scope.viewInsuranceCarrierAddresss = true;
        $scope.InsuranceCarrierAddresss = null;

    };

    $scope.showInsuranceCarrierAddressPersons = function () {
        $scope.viewInsuranceCarriers = false;
        $scope.viewInsuranceCarrierAddresss = false;
    };

    //----------------- get InsuranceCarrier Addresss ---------------------
    $scope.getInsuranceCarrierAddresss = function (InsuranceCarrierId) {
        $scope.carrierId = InsuranceCarrierId;
        for (var i in $scope.InsuranceCarriers) {
            if ($scope.InsuranceCarriers[i].InsuranceCarrierID == InsuranceCarrierId) {
                $scope.InsuranceCarrierAddresss = $scope.InsuranceCarriers[i].InsuranceCarrierAddresses;
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
        if ($scope.InsuranceCarriers) {
            for (startIndex; startIndex <= endIndex ; startIndex++) {
                if ($scope.InsuranceCarriers[startIndex]) {
                    $scope.CurrentPage.push($scope.InsuranceCarriers[startIndex]);
                } else {
                    break;
                }
            }
        }
    });
    //-------------- License Scope Watch ---------------------
    $scope.$watchCollection('InsuranceCarriers', function (newValue, oldValue) {
        if (newValue) {
            $scope.bigTotalItems = newValue.length;

            $scope.CurrentPage = [];
            $scope.bigCurrentPage = 1;

            var startIndex = ($scope.bigCurrentPage - 1) * 10;
            var endIndex = startIndex + 9;

            for (startIndex; startIndex <= endIndex ; startIndex++) {
                if ($scope.InsuranceCarriers[startIndex]) {
                    $scope.CurrentPage.push($scope.InsuranceCarriers[startIndex]);
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