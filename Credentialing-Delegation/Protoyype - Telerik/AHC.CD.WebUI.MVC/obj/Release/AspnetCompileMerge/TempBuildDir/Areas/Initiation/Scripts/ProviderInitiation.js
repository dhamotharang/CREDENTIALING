
//============================profile initiation Angular Module ==========================
var providerApp = angular.module("providerApp", ['mgcrea.ngStrap', 'ahc.cd.autosearch']);
//
//---------------- angular unsafe chrome extension -----------------
providerApp.config([
    '$compileProvider',
    function ($compileProvider) {
        $compileProvider.aHrefSanitizationWhitelist(/^\s*(https?|ftp|mailto|chrome-extension|unsafe|file|blob):/);
        $compileProvider.imgSrcSanitizationWhitelist(/^\s*(https?|local|data):/);
    }
]);
//---------------------- angular Saving Directive .................
providerApp.directive('saving', function () {
    return {
        restrict: 'E',
        replace: true,
        template: '<div class="loading"><img src="/Content/Images/ajax-loader.gif" width="20" height="20" />SAVING...</div>',
        link: function (scope, element, attr) {
            scope.$watch('saving', function (val) {
                if (val)
                    $(element).show();
                else
                    $(element).hide();
            });
        }
    }
});
//--------------------- angular loading directive ------------------
providerApp.directive('loading', function () {
    return {
        restrict: 'E',
        replace: true,
        template: '<div class="loading"><img src="/Content/Images/ajax-loader.gif" width="20" height="20" />LOADING...</div>',
        link: function (scope, element, attr) {
            scope.$watch('loading', function (val) {
                if (val)
                    $(element).show();
                else
                    $(element).hide();
            });
        }
    }
});

//----------------- Calender hide on select data configuration ------------------
providerApp.config(function ($datepickerProvider) {

    angular.extend($datepickerProvider.defaults, {
        startDate: 'today',
        autoclose: true,
        useNative: true
    });
})

//---------------------------- service for get location ----------------------
providerApp.service('locationService', ['$http', function ($http) {
    this.getLocations = function (QueryString) {
        return $http.get(rootDir + "/Location/GetCities?city=" + QueryString)
        .then(function (response) { return response.data; });
    };
}]);


//============================= profile initiation Angular controller ==========================================
providerApp.controller("providerController", ['$scope','$timeout', '$http', 'locationService', function ($scope, $timeout, $http, locationService) {

	//============================= Data From Master Data Table Required For provider titles ======================
	
    $http.get(rootDir + '/Profile/MasterData/GetAllProviderTypes').
      success(function (data, status, headers, config) {
          try {
              $scope.ProviderTypes = data;

          } catch (e) {
             
          }
      }).
      error(function (data, status, headers, config) {
         
      });

    $http.get(rootDir + '/Profile/MasterData/GetAllProviderLevels').
      success(function (data, status, headers, config) {
          try {
              $scope.ProviderLevels = data;

          } catch (e) {
             
          }
      }).
      error(function (data, status, headers, config) {
          
      });

    //----------------------------- Get List Of Groups --------------------------
    //$http.get('/Profile/MasterData/GetAllOrganizationGroupAsync').
    $http.get(rootDir + '/MasterData/Organization/GetGroups').
      success(function (data, status, headers, config) {
          try {
              $scope.PracticingGroups = data;
              $scope.MasterGroups = angular.copy(data);
          } catch (e) {
             
          }
          
      }).
      error(function (data, status, headers, config) {
          
      });
    //------------------- country dail codes ---------------------
    $scope.CountryDailCodes = countryDailCodes;

    //------------------------ get selected Salutation Type -----------------------------------
    $scope.getSalutationTextValue = function () {
        $scope.SelectedSalutation = $("#PersonalDetail_SalutationType option:selected").text();
    };
    $scope.GroupType = "Group";
    $scope.getProviderRelationTextValue = function (condition) {
        $scope.SelectedProviderRelation = $("#ProviderRelationCheckBox input:radio:checked").next().text();
        if (condition == 1) {
            $scope.GroupType = "Group";
        } else {
            $scope.GroupType = "IPA";
        }
        $scope.ContractGroupInfoes = [];
        $scope.IsPartOfGroup == null;
        $scope.PracticingGroups = angular.copy($scope.MasterGroups);
    };
    //---------------------------- set selected file CV ---------------------
    $scope.setFiles = function (element) {
        $scope.$apply(function (scope) {
            if (element.files[0]) {
                $scope.selectedCV = element.files[0];
            } else {
                $scope.selectedCV = {};
            }
        });
    };

    //---------------------------- set selected file Contract ---------------------
    $scope.setFiles1 = function (element) {
        $scope.$apply(function (scope) {
            if (element.files[0]) {
                $scope.selectedContract = element.files[0];
            } else {
                $scope.selectedContract = {};
            }
        });
        
    };
    
    $scope.removeFile = function (FileId) {
        if (FileId == "ContractInfo_CVFile") {
            $scope.selectedCV = {};
        } else if (FileId == "ContractInfoes_0__ContractFile") {
            $scope.selectedContract = {};
        }
    };
	//================================= Provider Init data ==============================
    $scope.tabStatus = 0;
    $scope.AddProviderFormTab0 = true;
    $scope.SuccessShowStyle = {'display':'none'};

    //-------------------------------- selecting of provider type ----------------------------
	$scope.PersonalDetail = {};
	$scope.PersonalDetail.ProviderTitles = [];
	$scope.ContractGroupInfoes = [];

	$scope.SelectProviderType = function (providertype) {
	    $scope.PersonalDetail.ProviderTitles.push({
	        ProviderType: providertype,
	        ProviderTitleID: null,
	        ProviderTypeId: providertype.ProviderTypeID,
	        StatusType: 1
	    });
	    $scope.ProviderTypes.splice($scope.ProviderTypes.indexOf(providertype), 1);
        $scope.searchproviderType = "";
	};
    //---------------- removed selected provider type ----------------------------
	$scope.RemoveProviderType = function (providerTitle) {
	    $scope.PersonalDetail.ProviderTitles.splice($scope.PersonalDetail.ProviderTitles.indexOf(providerTitle), 1)
	    $scope.ProviderTypes.push(providerTitle.ProviderType);
	    $scope.ProviderTypes.sort(function (a, b) {
	        if (a.Title > b.Title) {
	            return 1;
	        }
	        if (a.Title < b.Title) {
	            return -1;
	        }
	        // a must be equal to b
	        return 0;
	    });
	};
    //------------------------- Selecting of Group Information --------------------------
	$scope.SelectPracticingGroup = function (practicingGroup) {
	    $scope.ContractGroupInfoes.push({
	        ContractGroupInfoId: null,
	        PracticingGroupId: practicingGroup.PracticingGroupID,
	        PracticingGroup: practicingGroup,
	        StatusType: 1
	    });
	    $scope.PracticingGroups.splice($scope.PracticingGroups.indexOf(practicingGroup), 1);
        $scope.searchGroupName = "";
	};
    //---------------- removed selected Group Information ----------------------------
	$scope.RemoveContractGroupInfo = function (contractGroupInfo) {
	    $scope.ContractGroupInfoes.splice($scope.ContractGroupInfoes.indexOf(contractGroupInfo), 1)
	    $scope.PracticingGroups.push(contractGroupInfo.PracticingGroup);
	};

    //----------------------------------------- address data fetch from database --------------------
	$scope.addressHomeAutocomplete = function (location) {
	    $scope.HomeAddresses.City = location;
	    if (location.length >= 1 && !angular.isObject(location)) {
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
	    $scope.HomeAddresses.State = "";
	    $scope.HomeAddresses.Country = "";
	};

	$scope.setAddressModels = function (location) {
	    $scope.HomeAddresses.City = location.City;
	    $scope.HomeAddresses.State = location.State;
	    $scope.HomeAddresses.Country = location.Country;

	};

	//=============================Gender select with title ======================================
	$scope.$watch("PersonalDetail.SalutationType", function (newValue, oldValue) {
		try {
			if (newValue == 1 ) {
			    $scope.PersonalDetail.GenderType = 1;
			} else if (newValue == 2 || newValue == 3 || newValue == 4 || newValue == 6) {
			    $scope.PersonalDetail.GenderType = 2;
			} 
		} catch (e) {
		}
	});

    //--------------------------- Provider Level -------------------------
	$scope.getProviderLevelName = function (providerLevelID) {
	    if (providerLevelID > 0) {
	        for (var i in $scope.ProviderLevels) {
	            if ($scope.ProviderLevels[i].ProviderLevelID == providerLevelID) {
	                $scope.SelectedProviderLevel = $scope.ProviderLevels[i].Name;
	                break;
	            }
	        }
	    } else {
	        $scope.SelectedProviderLevel = "";
	    }
	};

	//========================== View Popover Contact Country Code ============================
	$scope.showContryCodeDiv = function(div_Id) {
		$("#" + div_Id).show();
	};
	
	$scope.NPINumberValid = true;
	$scope.CAQHNumberValid = true;

	$scope.Profile={
	    OtherIdentificationNumber: { NPIUserName: "", NPIPassword:"", CAQHUserName:"", CAQHPassword:""}
	};
	//========================== Unique NPI Number Check ===========================
	$scope.IsValid = function (fieldName) {
	    if (fieldName == "OtherIdentificationNumber_NPINumber") {
	        if ($("#" + fieldName).valid() && $("#" + fieldName).val()) {
	            $scope.NPINumberValid = false;
	        } else if ($("#" + fieldName).val() == "") {
	            $scope.NPINumberValid = true;
	            $scope.Profile.OtherIdentificationNumber.NPIUserName = "";
	            $scope.Profile.OtherIdentificationNumber.NPIPassword = "";
	        } else {
	            $scope.NPINumberValid = true;
	        }
	    } else if (fieldName == "OtherIdentificationNumber_CAQHNumber") {
	        if ($("#" + fieldName).valid() && $("#" + fieldName).val()) {
	            $scope.CAQHNumberValid = false;
	        } else if ($("#" + fieldName).val() == "") {
	            $scope.CAQHNumberValid = true;
	            $scope.Profile.OtherIdentificationNumber.CAQHUserName = "";
                $scope.Profile.OtherIdentificationNumber.CAQHPassword = "";
	        } else {
	            $scope.CAQHNumberValid = true;
	        }
	    }
	};
   
	//================================= Tab Flow Validation Method ==============================
	$scope.nextForm = function(tabid) {
		$scope.tabStatus = tabid;
	};
	//================================= Primary info form view ===================================
	$scope.primaryInfoForm = function (tabid, Form_Id) {
	    var $form = $("#" + Form_Id);
	    ResetFormForValidation($form);
	    if ($form.valid()) {
	        $scope.AddProviderFormTab1 = true;
	        $scope.tabStatus = tabid;
	    }
	    //$scope.AddProviderFormTab1 = true;
	    //$scope.tabStatus = tabid;
	};

    //--------------------------- mobile number validation ------------------------
	$scope.MobileHasError = false;
    $scope.IsPhoneNumberUnique = function (countrycode, number) {
        if (/^\d{10}$/.test(number)) {
            var councode = countrycode.split("+")[1];
            $.ajax({
                url: rootDir + "/Validation/IsContactNumberDoesNotExists?Number=" + number + "&CountryCode=%2B" + councode + "&PhoneDetailID=" + 0,
                async: false,
                cache: false,
                contentType: false,
                processData: false,
                success: function (data) {
                    
                    try {
                        if (data) {
                            $scope.MobileHasError = false;
                        } else {
                            $scope.MobileHasError = true;
                        }
                    } catch (e) {
                     
                    }
                }
            });
        } else {
            $scope.MobileHasError = false;
        }
    };
    //--------------------------- mobile number validation ------------------------
    $scope.EmailHasError = false;
    $scope.IsEmailUnique = function (email) {

        email = angular.lowercase(email);
        $scope.ContactDetail.EmailAddress = email;

        if (/^\S+@\S+\.\S+$/.test(email)) {
            $.ajax({
                url: rootDir + "/Validation/IsEmailAddressDoesNotExists?EmailAddress=" + email + "&EmailDetailID=" + 0,
                async: false,
                cache: false,
                contentType: false,
                processData: false,
                success: function (data) {
                    try {

                        if (data) {
                            $scope.EmailHasError = false;
                        } else {
                            $scope.EmailHasError = true;
                        }
                    } catch (e) {
                       
                    }
                }
            });
        } else {
            $scope.EmailHasError = false;
        }
    };

	//================================= Contract Information ===================================
	$scope.ContractInfoForm = function (Form_Id) {
	    //if ($("#ContactDetail_PhoneDetails_0__CountryCode").hasClass("input-validation-error") && !$("#ContactDetail_PhoneDetails_0__Number").hasClass("input-validation-error")) {
	    //    $("#ContactDetail_PhoneDetails_0__Number").addClass("input-validation-error");
	    //}
	    if ($scope.NPIUserNameTempStatus < 4 && $scope.NPIPasswordTempStatus < 4) {
	        $scope.Profile.OtherIdentificationNumber.NPIUserName = "";
	        $scope.Profile.OtherIdentificationNumber.NPIPassword = "";
	    }

	    var $form = $("#" + Form_Id);
	    ResetFormForValidation($form);
	    if ($form.valid() && $scope.PersonalDetail.ProviderTitles.length > 0 && !$scope.EmailHasError && !$scope.MobileHasError) {
	        $scope.isHasError = false;
	        $scope.AddProviderFormTab2 = true;
	        $scope.tabStatus = 2;
	        $('body,html').animate({
	            scrollTop: 0
	        });
	    }
	    else if ($scope.PersonalDetail.ProviderTitles.length == 0) {
	        $scope.isHasError = true;
	        
	    }
	    //$scope.AddProviderFormTab2 = true;
	    //$scope.tabStatus = 2;
	};

	//================================= Create Provider Profile Preview Method ==============================
	$scope.previewAddProvider = function (Form_Id) {
	    var $form = $("#" + Form_Id);
	    ResetFormForValidation($form);

	    if ($form.valid() && ($scope.IsPartOfGroup == 'Yes' && $scope.ContractGroupInfoes.length > 0 || !$scope.IsPartOfGroup || $scope.IsPartOfGroup == 'No')) {
	        $scope.isHasError = false;
	        $scope.AddProviderFormTab3 = true;
	        $scope.tabStatus = 3;
	        $('body,html').animate({
	            scrollTop: 0
	        });
	    }
        else{
	        $scope.isHasError = true;
	       
	    }
	    //$scope.AddProviderFormTab3 = true;
	    //$scope.tabStatus = 3;
	};
	//============================ confirmation method for submit ===========================
	$scope.confirmationAddProvider = function(formStates) {
	    $('#myModal').modal({backdrop:'static'});
	};
	//============================ modal confirmation action ===========================
	$scope.confirmProvider = function() {
	    var formdata = new FormData();
        
	    var form5 = $('#BasicInformationForm, #PersonalInformationForm, #ContractInformationForm').serializeArray();
	    for (var i in form5) {
	        formdata.append(form5[i].name, form5[i].value);
	    }

	    formdata.append($("#CVInformation_CVFile")[0].name, $("#CVInformation_CVFile")[0].files[0]);
	    formdata.append($("#ContractInfoes_0__ContractFile")[0].name, $("#ContractInfoes_0__ContractFile")[0].files[0]);
	    
	    $.ajax({
	        url: rootDir + '/Initiation/Provider/Index',
	        type: 'POST',
	        data: formdata,
	        async: true,
	        cache: false,
	        contentType: false,
	        processData: false,
	        success: function (data) {
	           
	            try {
	                if (data.providerInitiationStatus) {
	                    $timeout(NextFunctionCall, 2000);
	                    $scope.MailStatus = data.mailStatus;
	                    $scope.profileURL = rootDir + "/Profile/MasterProfile/ProviderProfile/" + data.profileId;
	                    $scope.MailErrorList = data.errorMessage;
	                    $scope.SuccessShowStyle = { 'display': 'block' };
	                    setTimeout(function () { $("#ProfileCreateSessage").hide(); }, 5000);
	                } else {
	                    //-------------- we write message for show the error message to user -------------------
	                    $scope.saving = false;
	                    $scope.ProviderInitiationErrorList = data.errorMessage;
	                    $('#myModal').modal('hide');
	                    $("#ProviderInitiationErrorList").show();
	                    $('body,html').animate({
	                        scrollTop: 0
	                    });
	                }
	            } catch (e) {
	             
	            }
	        },
            error: function() { 
	                $scope.saving = false;
                    $scope.ProviderInitiationErrorList = "Sorry ..............................";
                    $('#myModal').modal('hide');
                    $("#ProviderInitiationErrorList").show();
                    $('body,html').animate({
                        scrollTop: 0
                    });
                }
	    });
	    //$timeout(NextFunctionCall, 2000);
	    //$scope.MailStatus = "";
	    //$scope.ProfileId = 2;
	    //$scope.MailErrorList = "";
	    //$scope.SuccessShowStyle = { 'display': 'block' };
	    //setTimeout(function () { $("#ProfileCreateSessage").hide(); }, 5000);
	};
    //------------------ tiime oute function call -------------------------
	var NextFunctionCall = function () {
	    $('#myModal').modal('hide');
	    $scope.saving = false;
	    $scope.AddProviderFormTab0 = false;
	    $scope.AddProviderFormTab1 = false;
	    $scope.AddProviderFormTab2 = false;
	    $scope.AddProviderFormTab3 = false;
	    $scope.AddProviderFormTab4 = true;
	    $scope.tabStatus = 4;
	}

    //------------------------------ return is array check status ----------------------------
	$scope.dataIsArray = function (data) {
	    return angular.isArray(data);
	};

    //----------------------- Autocomplete Handle Method for npi user name and password field --------------------
	$scope.NPIUserNameTempStatus = 0;
	$scope.NPIPasswordTempStatus = 0;

	$scope.NPIUserNameStatus = function () {
	    $scope.NPIUserNameTempStatus++;
	}
	$scope.NPIPasswordStatus = function () {
	    $scope.NPIPasswordTempStatus++;
	}
    //-------------- loading profile URL --------------------
	$scope.ViewProfile = function () {
	    $scope.loading = true;
	    $timeout(Redirect, 2000);
	}
    //----------------- Redirect URL ------------------
	function Redirect() {
	    window.location = $scope.profileURL;
	}
}]);

//================================= Hide All country code popover =========================
$(document).click(function(event) {
	if (!$(event.target).hasClass("countryCodeClass") && $(event.target).parents(".countryDailCodeContainer").length === 0) {
		$(".countryDailCodeContainer").hide();
	}
	if (!$(event.target).hasClass("form-control") && $(event.target).parents(".ProviderTypeSelectAutoList").length === 0) {
		$(".ProviderTypeSelectAutoList").hide();
	}
});

$(document).ready(function() {
	$(".countryDailCodeContainer").hide();
	$(".ProviderTypeSelectAutoList").hide();

	$("input").attr("autocomplete", "off");

    $("#ProviderInitiationErrorList").hide();

    $('#ProviderInitiationErrorListClose').on('click', function () {
        $(this).parent().parent().parent().hide();
    });
});

function ResetFormForValidation(form) {
    form.removeData('validator');
    form.removeData('unobtrusiveValidation');
    $.validator.unobtrusive.parse(form);
}

// ----------------- Mouse Up & Mouse Down Action for Use ------------------------
function PasswordVisibleMouseDown(InputId) {
    $('#' + InputId).attr('type', 'text');
}

function PasswordVisibleMouseUp(InputId) {
    $('#' + InputId).attr('type', 'password');
}

function showLocationList(ele) {
    $(ele).parent().find(".ProviderTypeSelectAutoList").first().show();
}

//--------------- file name Wrap-text author : krglv ---------------
function setFileNameWith(file) {
    $(file).parent().parent().find(".jancyFileWrapTexts").find("span").width($(file).parent().parent().width() - 197);
}