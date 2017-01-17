var profileApp = angular.module('profileApp', ['ngTable', 'mgcrea.ngStrap']);

profileApp.controller('breadcrumCtrl', ['$scope', '$rootScope', '$http', function ($scope, $rootScope, $http, countryDropDownService) {
    $rootScope.$on('PersonalDetail', function (event, val) {
        //console.log("PersonalDetail............");
        //console.log(val);
        $scope.Provider = val;
    });
}])

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

//========= Country data and UniqueData Provider ==================================
profileApp.service('countryDropDownService', ['$compile', function ($compile) {
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
}]);

//Methods to control the view of the View and Edit
profileApp.run(['$rootScope', 'countryDropDownService', function ($rootScope, countryDropDownService) {
    $rootScope.visibilityControl = "";
    //Visibility of the div Control object to perform show and hide

    $rootScope.alertMessage = "";

    $rootScope.tempObject = {};
    //Temp object to hold the form data so that the data gets revert once clicked cancel while add and edit


    //Controls the View and Add feature on the page
    $rootScope.operateViewAndAddControl = function (sectionValue) {
        $rootScope.tempObject = {};
        $rootScope.buttonLabel = "Add"
        //console.log(sectionValue);
        $rootScope.visibilityControl = sectionValue;
        $('[data-toggle="tooltip"]').tooltip();
        $rootScope.tempObject.selectedLocation = {};
        $rootScope.tempObject.selectedEduLocation = {};

        try {
        $('.fileinput-exists').find('a').trigger('click');
        } catch (e) {
    
        }
    };

    //Controls the Edit feature on the page
    $rootScope.operateEditControl = function (sectionValue, obj) {
        $rootScope.tempObject = {};
        $rootScope.selectedLocation = {};
        $rootScope.tempObject.selectedEduLocation = {};
        $rootScope.buttonLabel = "Update"
        //console.log(sectionValue);
        //console.log(obj);
        $rootScope.tempObject = angular.copy(obj);
        //console.log(angular.copy(obj));
        $rootScope.visibilityControl = sectionValue;
        $('[data-toggle="tooltip"]').tooltip();
        if ($rootScope.tempObject.City) {
            $rootScope.tempObject.selectedLocation = { 'City': $rootScope.tempObject.City, 'State': $rootScope.tempObject.State, 'Zipcode': $rootScope.tempObject.Zipcode };
        }

        if ($rootScope.tempObject.SchoolInformation && $rootScope.tempObject.SchoolInformation.City) {
            $rootScope.tempObject.selectedEduLocation = { 'City': $rootScope.tempObject.SchoolInformation.City, 'State': $rootScope.tempObject.SchoolInformation.State, 'Zipcode': $rootScope.tempObject.SchoolInformation.ZipCode };
        }

        try {
            $('.fileinput-exists').find('a').trigger('click');
        } catch (e) {

        }
    };

    //Controls the View and Add feature on the page
    $rootScope.operateCancelControl = function (Form_Div_Id) {
        $rootScope.tempObject = {};
        $('.field-validation-error').removeClass('field-validation-error').addClass('field-validation-valid');
        $('.input-validation-error').removeClass('input-validation-error').addClass('valid');
        $rootScope.visibilityControl = "";
    };

    $rootScope.callAlert = function (alertName) {
        $rootScope.alertMessage = alertName;
    };

    $rootScope.closeAlert = function (alertName) {
        $rootScope.alertMessage = '';
    };

    //Second stage common controllers for view, edit, add, cancel
    $rootScope.tempSecondObject = {};
    //Temp object to hold the inner object form data so that the data gets revert once clicked cancel while add and edit

    //Controls the View and Add feature on the page of object has an object
    $rootScope.operateSecondViewAndAddControl = function (sectionValue) {
        try {
            $('.fileinput-exists').find('a').trigger('click');
        } catch (e) {

        }
        $rootScope.tempSecondObject = {};
        $('.field-validation-error').removeClass('field-validation-error').addClass('field-validation-valid');
        $('.input-validation-error').removeClass('input-validation-error').addClass('valid');
        //console.log(sectionValue);
        $rootScope.visibilitySecondControl = sectionValue;
    };

    //Controls the Edit feature on the page of object has an object
    $rootScope.operateSecondEditControl = function (sectionValue, obj) {
        try {
            $('.fileinput-exists').find('a').trigger('click');
        } catch (e) {

        }
        $rootScope.tempSecondObject = {};
        $('.field-validation-error').removeClass('field-validation-error').addClass('field-validation-valid');
        $('.input-validation-error').removeClass('input-validation-error').addClass('valid');
        //console.log(sectionValue);
        //console.log(obj);
        $rootScope.tempSecondObject = angular.copy(obj);
        //console.log(angular.copy(obj));
        $rootScope.visibilitySecondControl = sectionValue;
    };

    //Controls the  View and Add cancel feature on the page of object has an object
    $rootScope.operateSecondCancelControl = function (Form_Div_Id) {
        $rootScope.tempSecondObject = {};
        if (Form_Div_Id) {
            //FormReset($("#" + Form_Div_Id).find("form"));
        }
        $('.field-validation-error').removeClass('field-validation-error').addClass('field-validation-valid');
        $('.input-validation-error').removeClass('input-validation-error').addClass('valid');
        $rootScope.visibilitySecondControl = "";
    };

    $rootScope.saveOrUpdateData = function (urlVal, formDetail) {
        $.ajax({
            url: urlVal + profileId,
            type: 'POST',
            data: new FormData(formDetail),
            async: false,
            cache: false,
            contentType: false,
            processData: false,
            success: function (data) {
                //console.log(data);
                return (data) ? "true" : "false";
            },
            error: function (e) {
                return "error";
            }
        });
    };

    //-------------------- Address Drop-down select action ---------------------------
    //-------------- country data comes from CountryList.js and countryDialCodes.js---------------
    $rootScope.Countries = Countries;

    $rootScope.getStates = function (countryCode, stateObj, countyObj, cityObj) {
        //console.log("Root Method Called state");
        $rootScope.States = countryDropDownService.getStates($rootScope.Countries, countryCode);
        $rootScope.Counties = [];
        $rootScope.Cities = [];
        stateObj = null;
        countyObj = null;
        cityObj = null;
    };

    $rootScope.getCounties = function (state, countyObj, cityObj) {
        //console.log("Root Method Called County");
        $rootScope.Counties = countryDropDownService.getCounties($rootScope.States, state);
        $rootScope.Cities = [];
        countyObj = null;
        cityObj = null;
    };
    $rootScope.getCities = function (county, cityObj) {
        ////console.log("Root Method Called City");
        $rootScope.Cities = countryDropDownService.getCities($rootScope.Counties, county);
        cityObj = null;
    };

    //Convert the date from database to normal
    $rootScope.ConvertDateFormat = function (value) {
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

    $rootScope.ConvertCity = function (city, zipcode) {
        return { 'City': city, 'StateCode': '', 'Zipcode': zipcode };
    };


}]);

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

var ConvertDateFormat = function (value) {
    if (value) {
        return new Date(parseInt(value.replace("/Date(", "").replace(")/", ""), 10));
    } else {
        return value;
    }
};

function ResetFormForValidation(form) {
    form.removeData('validator');
    form.removeData('unobtrusiveValidation');
    $.validator.unobtrusive.parse(form);

}

var loadingOn = function () {
    $("#loading").show();
}

var loadingOff = function () {
    $("#loading").hide();
}

$(document).on({
    ajaxStart: function () {
        $("#loading").show();
    },

    ajaxStop: function () {
        $("#loading").hide();
    }
});

$(function () {
    $(document).ajaxError(function (e, xhr) {
        if (xhr.status == 403) {
            var response = $.parseJSON(xhr.responseText);
            window.location.href = response.LogOnUrl;
        }
    });
});


