var profileApp = angular.module('profileApp', ['ngTable', 'mgcrea.ngStrap','ahc.cd.autosearch']);


profileApp.config(function ($datepickerProvider) {

    angular.extend($datepickerProvider.defaults, {
        startDate: 'today',
        autoclose: true,
        useNative: true
    });
})

profileApp.controller('breadcrumCtrl', ['$scope', '$rootScope', '$http', function ($scope, $rootScope, $http) {
    $rootScope.$on('PersonalDetail', function (event, val) {
        //console.log("PersonalDetail............");
        //console.log(val);
        $scope.Provider = val;
    });
}])

profileApp.service('messageAlertEngine', ['$rootScope', '$timeout', function ($rootScope, $timeout) {

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
            $timeout(animateMessageAlertOff,5000);
        }
    }

    $rootScope.closeAlertMessage = function () {
        $rootScope.messageDesc = "";
        $rootScope.activeMessageDiv = "";
        $rootScope.messageType = "";
    }    
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

//Methods to control the view of the View and Edit
profileApp.run(['$rootScope','messageAlertEngine', function ($rootScope, messageAlertEngine) {
    $rootScope.visibilityControl = "";
    //Visibility of the div Control object to perform show and hide

    $rootScope.alertMessage = "";

    $rootScope.tempObject = {};
    //Temp object to hold the form data so that the data gets revert once clicked cancel while add and edit


    //Controls the View and Add feature on the page
    $rootScope.operateViewAndAddControl = function (sectionValue) {
        $rootScope.operateSecondCancelControl('');
        $rootScope.closeAlertMessage();
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
        $rootScope.operateSecondCancelControl('');
        $rootScope.closeAlertMessage();
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
        try {
            if ($rootScope.tempObject.City) {
                $rootScope.tempObject.selectedLocation = { 'City': $rootScope.tempObject.City, 'State': $rootScope.tempObject.State, 'CountryCode': '' };
            }

            if ($rootScope.tempObject.SchoolInformation && $rootScope.tempObject.SchoolInformation.City) {
                $rootScope.tempObject.selectedEduLocation = { 'City': $rootScope.tempObject.SchoolInformation.City, 'State': $rootScope.tempObject.SchoolInformation.State, 'CountryCode': '' };
            }
        } catch (e) { }

        try {
            $('.fileinput-exists').find('a').trigger('click');
        } catch (e) {

        }
    };

    //Control the Renew feature on a page
    $rootScope.operateRenewControl = function (sectionValue, obj) {
        $rootScope.closeAlertMessage();
        $rootScope.tempObject = {};
        $rootScope.selectedLocation = {};
        $rootScope.tempObject.selectedEduLocation = {};
        $rootScope.buttonLabel = "Renew"
        //console.log(sectionValue);
        //console.log(obj);
        $rootScope.tempObject = angular.copy(obj);
        //console.log(angular.copy(obj));
        $rootScope.visibilityControl = sectionValue;
        $('[data-toggle="tooltip"]').tooltip();
        //try {
        //    if ($rootScope.tempObject.City) {
        //        $rootScope.tempObject.selectedLocation = { 'City': $rootScope.tempObject.City, 'State': $rootScope.tempObject.State, 'CountryCode': '' };
        //    }

        //    if ($rootScope.tempObject.SchoolInformation && $rootScope.tempObject.SchoolInformation.City) {
        //        $rootScope.tempObject.selectedEduLocation = { 'City': $rootScope.tempObject.SchoolInformation.City, 'State': $rootScope.tempObject.SchoolInformation.State, 'CountryCode': '' };
        //    }
        //} catch (e) { }

        try {
            $('.fileinput-exists').find('a').trigger('click');
        } catch (e) {

        }
    };

    //Controls the View and Add feature on the page
    $rootScope.operateCancelControl = function (Form_Div_Id) {
        $rootScope.operateSecondCancelControl('');
        $rootScope.operateThirdCancelControl('');
        $rootScope.closeAlertMessage();
        $rootScope.tempObject = {};
        $('.field-validation-error').removeClass('field-validation-error').addClass('field-validation-valid');
        $('.input-validation-error').removeClass('input-validation-error').addClass('valid');
        $rootScope.visibilityControl = "";
    };

    $rootScope.callAlert = function (alertName) {
        $rootScope.alertMessage = alertName;
        setTimeout(function () {
            $rootScope.alertMessage = alertName;
        }, 4000);
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
        $rootScope.operateThirdCancelControl('');
        $rootScope.tempSecondObject = {};
        if (Form_Div_Id) {
            //FormReset($("#" + Form_Div_Id).find("form"));
        }
        $('.field-validation-error').removeClass('field-validation-error').addClass('field-validation-valid');
        $('.input-validation-error').removeClass('input-validation-error').addClass('valid');
        $rootScope.visibilitySecondControl = "";
    };



    $rootScope.operateThirdViewAndAddControl = function (sectionValue) {
        try {
            $('.fileinput-exists').find('a').trigger('click');
        } catch (e) {

        }
        $rootScope.tempThirdObject = {};
        $('.field-validation-error').removeClass('field-validation-error').addClass('field-validation-valid');
        $('.input-validation-error').removeClass('input-validation-error').addClass('valid');
        //console.log(sectionValue);
        $rootScope.visibilityThirdControl = sectionValue;
    };

    //Controls the Edit feature on the page of object has an object
    $rootScope.operateThirdEditControl = function (sectionValue, obj) {
        try {
            $('.fileinput-exists').find('a').trigger('click');
        } catch (e) {

        }
        $rootScope.tempThirdObject = {};
        $('.field-validation-error').removeClass('field-validation-error').addClass('field-validation-valid');
        $('.input-validation-error').removeClass('input-validation-error').addClass('valid');
        //console.log(sectionValue);
        //console.log(obj);
        $rootScope.tempThirddObject = angular.copy(obj);
        //console.log(angular.copy(obj));
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


