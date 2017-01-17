var profileApp = angular.module('profileApp', ['smart-table', 'ui.bootstrap', 'timepickerPop', 'mgcrea.ngStrap', 'ahc.cd.autosearch', 'angular-svg-round-progress']);


profileApp.config(function ($datepickerProvider) {

    angular.extend($datepickerProvider.defaults, {
        startDate: 'today',
        autoclose: true,
        useNative: true
    });
})

profileApp.controller('tabController', ['$scope', '$rootScope', '$http', 'messageAlertEngine', function ($scope, $rootScope, $http, messageAlertEngine) {
    $rootScope.ccoprofile = false;
    $http.get(rootDir + '/Profile/CustomFieldGeneration/getRole').
        success(function (data, status, headers, config) {
            $rootScope.ccoprofile = data.role;
        }).
        error(function (data, status, headers, config) {

        });
    $scope.getTabData = function (tabName) {
        $rootScope.$broadcast(tabName);
        if (tabName == 'DocumentationCheckList') {
            $("#tabname").text("Document CheckList");
            $('#tabDocumentationChecklist').css({ 'pointer-events': 'none' });
        }
        else { $('#tabDocumentationChecklist').css({ 'pointer-events': 'auto' }); }
        if (tabName == 'ContractGrid') {
            $('#tabContractGrid').css({ 'pointer-events': 'none' });
        } else { $('#tabContractGrid').css({ 'pointer-events': 'auto' }); }
    }

    $scope.dontReloadTab = false;    
    $rootScope.loadAllData = function () {
        if ($scope.dontReloadTab == false) {
            $rootScope.$broadcast("IdentificationLicenses");
            $rootScope.$broadcast("EducationHistory");
            $rootScope.$broadcast("Specialty");
            $rootScope.$broadcast("HospitalPrivilege");
            $rootScope.$broadcast("ProfessionalLiability");
            $rootScope.$broadcast("ProfessionalReference");
            $rootScope.$broadcast("ProfessionalAffiliation");
            $rootScope.$broadcast("DisclosureQuestion");
            $rootScope.$broadcast("ContractInformation");
            $rootScope.$broadcast("WorkHistory");
            $rootScope.$broadcast("PracticeLocation");
            if ($rootScope.ccoprofile == true) {
                $rootScope.$broadcast("CustomField");
                $rootScope.$broadcast("DocumentationCheckList");
            }
            $rootScope.$broadcast("ContractGrid");
            $scope.dontReloadTab = true;
        };

    };

}]);

profileApp.controller('breadcrumCtrl', ['$scope', '$rootScope', '$http', 'messageAlertEngine', function ($scope, $rootScope, $http, messageAlertEngine) {
    $rootScope.$on('PersonalDetail', function (event, val) {
       $scope.Provider = val;
    });

    $rootScope.changeTimeAmPm = function (value) {
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

        //minutes = minutes < 9 ? '00' : minutes;
        var strTime = hours + ':' + minutes + ' ' + ampm;
        return strTime;
    }

    $scope.PrintPDF = function () {
        $rootScope.generatePDF = true;
        try {
            var url = rootDir + "/PDFProfileDataGenerator/GetProfileData?profileId=" + profileId;
            $http.get(url)     //locationService makes an ajax call to fetch all the cities which have relevent names as the query string.
       .then(function (response) {
           var newPage = rootDir + "/Document/View?path=/ApplicationDocument/GeneratedPdf/" + response.data.path;
           if (response.data.status == 'true') {
               var win = window.open(newPage, '_blank');
               win.focus();
           }
           else {
               messageAlertEngine.callAlertMessage("showErrorPDF", "Sorry for the Inconvenience, PDF cannot be generated. Please try again later !!", "danger", true);
               //  $("#showErrorPDF").show();
               //$scope.showErrorPDF = true;
           }
           $rootScope.generatePDF = false;
           //  $scope.States = response.data;
       });
        }
        catch (e) {
            $rootScope.generatePDF = false;
            messageAlertEngine.callAlertMessage("showErrorPDF", "Sorry for the Inconvenience, PDF cannot be generated. Please try again later !!", "danger", true);

        };
    };
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
            $timeout(animateMessageAlertOff, 5000);
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
profileApp.run(['$rootScope', 'messageAlertEngine', function ($rootScope, messageAlertEngine) {
    $rootScope.visibilityControl = "";
    //Visibility of the div Control object to perform show and hide
    $rootScope.randomsItems = [];
    $rootScope.AllData = false;
    $rootScope.filtered = [];
    $rootScope.LoadForPdf = false;
    $rootScope.alertMessage = "";

    $rootScope.tempObject = {};
    //Temp object to hold the form data so that the data gets revert once clicked cancel while add and edit


    //Controls the View and Add feature on the page
    $rootScope.operateViewAndAddControl = function (sectionValue) {
        $rootScope.operateSecondCancelControl('');
        $rootScope.closeAlertMessage();
        $rootScope.tempObject = {};
        $rootScope.buttonLabel = "Add"
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
        $rootScope.tempObject = angular.copy(obj);
        
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
        finally {
            $rootScope.$broadcast("ArrayResize", $rootScope.tempObject);
        }
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
        $rootScope.tempObject = angular.copy(obj);
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
        $rootScope.dlinfoerror = false;
        $rootScope.dlinfoerror1 = false;
        //form = $('.militaryServiceInformationForm').find('form');
        //FormReset(form);
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
        $rootScope.tempSecondObject = angular.copy(obj);
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
                return (data) ? "true" : "false";
            },
            error: function (e) {
                return "error";
            }
        });
    };

    //Convert the date from database to normal
    $rootScope.ConvertDateFormat = function (value) {
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

    //--------------- autohide on select ---------------------------
    $rootScope.hideDiv = function () {
        $('.ProviderTypeSelectAutoList').hide();
    }

    //--------------------auto fill date expiry - author : krglv --------------------
    $rootScope.autoFillExpiryDate = function (objid, objstart, objend) {
        if (!objid) {
            return new Date(new Date(objstart).setMonth(new Date(objstart).getMonth() + 24));
        } else {
            return objend;
        }
    }
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
            document.getElementById('logoutForm').submit();
        }
    });
});

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
    if (!$(event.target).attr("data-searchdropdown") && $(event.target).parents(".ProviderTypeSelectAutoList1").length === 0) {
        $(".ProviderTypeSelectAutoList1").hide();
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