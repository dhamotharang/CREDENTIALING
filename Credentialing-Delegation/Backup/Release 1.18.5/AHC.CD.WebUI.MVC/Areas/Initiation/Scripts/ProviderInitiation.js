
//============================profile initiation Angular Module ==========================
var providerApp = angular.module("providerApp", ['mgcrea.ngStrap', 'ahc.cd.autosearch']);

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
        return $http.get("/Location/GetCities?city=" + QueryString)
        .then(function (response) { return response.data; });
    };
}]);


//============================= profile initiation Angular controller ==========================================
providerApp.controller("providerController", ['$scope', '$http', 'locationService', function ($scope, $http, locationService) {

	//============================= Data From Master Data Table Required For provider titles ======================
	
    $http.get('/Profile/MasterData/GetAllProviderTypes').
      success(function (data, status, headers, config) {
          $scope.ProviderTypes = data;
          //console.log("Provider Types");
          //console.log(data);
      }).
      error(function (data, status, headers, config) {
          //console.log("Sorry internal master data cont able to fetch.");
      });

    $http.get('/Profile/MasterData/GetAllProviderLevels').
      success(function (data, status, headers, config) {
          $scope.ProviderLevels = data;
          //console.log("Provider Types");
          //console.log(data);
      }).
      error(function (data, status, headers, config) {
          //console.log("Sorry internal master data cont able to fetch.");
      });

    //----------------------------- Get List Of Groups --------------------------
    //$http.get('/Profile/MasterData/GetAllOrganizationGroupAsync').
    $http.get('/MasterData/Organization/GetGroups').
      success(function (data, status, headers, config) {
          $scope.PracticingGroups = data;
          //console.log("Group Informations");
          //console.log(data);
      }).
      error(function (data, status, headers, config) {
          //console.log("Sorry internal master data cont able to fetch.");
      });
    //------------------- country dail codes ---------------------
    $scope.CountryDailCodes = countryDailCodes;

    //------------------------ get selected Salutation Type -----------------------------------
    $scope.getSalutationTextValue = function () {
        $scope.SelectedSalutation = $("#PersonalDetail_SalutationType option:selected").text();
    };
    $scope.getProviderRelationTextValue = function () {
        $scope.SelectedProviderRelation = $("#ProviderRelationCheckBox input:radio:checked").next().text();
    };
    //---------------------------- set selected file CV ---------------------
    $scope.setFiles = function (element) {
        $scope.$apply(function (scope) {
            if (element.files[0]) {
                $scope.selectedCV = element.files[0];
                $scope.CVFileTempPath = URL.createObjectURL(element.files[0]);
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
                $scope.ContractFileTempPath = URL.createObjectURL(element.files[0]);
            } else {
                $scope.selectedContract = {};
            }
        });
        //console.log($scope.selectedContract);
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
	$scope.SelectPracticingGroup = function (practicingGroup, divId) {
	    $scope.ContractGroupInfoes.push({
	        ContractGroupInfoId: null,
	        PracticingGroupId: practicingGroup.PracticingGroupID,
	        PracticingGroup: practicingGroup,
	        StatusType: 1
	    });
	    $scope.PracticingGroups.splice($scope.PracticingGroups.indexOf(practicingGroup), 1);
	    $("#" + divId).hide();
	};
    //---------------- removed selected Group Information ----------------------------
	$scope.RemoveContractGroupInfo = function (contractGroupInfo) {
	    $scope.ContractGroupInfoes.splice($scope.ContractGroupInfoes.indexOf(contractGroupInfo), 1)
	    $scope.PracticingGroups.push(contractGroupInfo.PracticingGroup);
	};

    //----------------------------------------- address data fetch from database --------------------
	$scope.addressHomeAutocomplete = function (location) {
	    $scope.HomeAddresses.City = location;
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
	    if ($form.valid() && $scope.PersonalDetail.ProviderTitles.length > 0) {
	        $scope.isHasError = false;
	        $scope.AddProviderFormTab2 = true;
	        $scope.tabStatus = 2;
	        $('body,html').animate({
	            scrollTop: 0
	        });
	    }
	    else if ($scope.PersonalDetail.ProviderTitles.length == 0) {
	        $scope.isHasError = true;
	        //console.log("Sorry! Personal Information could not be Validated.");
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
	        //console.log("Sorry! Contract Information could not be Validated.");
	    }
	    //$scope.AddProviderFormTab3 = true;
	    //$scope.tabStatus = 3;
	};
	//============================ confirmation method for submit ===========================
	$scope.confirmationAddProvider = function(formStates) {
	    $('#myModal').modal();
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
	        url: '/Initiation/Provider/Index',
	        type: 'POST',
	        data: formdata,
	        async: false,
	        cache: false,
	        contentType: false,
	        processData: false,
	        success: function (data) {
	            //console.log(data);
	            if (data.status == "true") {
	                $scope.AddProviderFormTab0 = false;
	                $scope.AddProviderFormTab1 = false;
	                $scope.AddProviderFormTab2 = false;
	                $scope.AddProviderFormTab3 = false;
	                $scope.AddProviderFormTab4 = true;
	                $scope.tabStatus = 4;
	                $scope.ProfileId = data.profileId;
	                $('#myModal').modal('hide');
	                $scope.SuccessShowStyle = { 'display': 'block' };
	                setTimeout(function () { $("#ProfileCreateSessage").hide();}, 5000);
	            } else {
	                //-------------- we write message for show the error message to user -------------------
	                $scope.ProviderInitiationErrorList = data.status;
	                $('#myModal').modal('hide');
	                $("#ProviderInitiationErrorList").show();
	                $('body,html').animate({
	                    scrollTop: 0
	                });
	            }
	        }
	    });

	    //$scope.ProviderInitiationErrorList = ["sjfhjdhgjdfgkjfkjgkfjhkjf","sjgfagsdsdhjghjdhgfg","kkjhfdjsagfjkgskjsdhg"];//data.status;
	    ////console.log(typeof $scope.ProviderInitiationErrorList == "string");
	    ////console.log("ttttttttttttttttttttttttttttttttttttt");
	    ////console.log($scope.ProviderInitiationErrorList instanceof Array);

	    //$scope.isArray = angular.isArray($scope.ProviderInitiationErrorList);

	    //$('#myModal').modal('hide');
	    //$("#ProviderInitiationErrorList").show();
	    //$('body,html').animate({
	    //    scrollTop: 0
	    //});
	};
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