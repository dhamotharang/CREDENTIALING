/// <reference path="createProvider.js" />
//================== addProvider.js ==========================

//============================ Angular Module View Controller ==========================
var providerApp = angular.module("providerApp", ["xeditable", "textAngular"]);

//========================= Editable class default defined================
providerApp.run(function (editableOptions) {
    editableOptions.theme = 'bs3'; // bootstrap3 theme. Can be also 'bs2', 'default'
});

//=========================== Angular Filter method Unique array list ===========================
providerApp.filter('unique', function () {
    return function (collection, keyname) {
        var output = [],
            keys = [];
            
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
//============================= input file validation ======================================
providerApp.directive('validFile', function () {
    return {
        require: 'ngModel',
        link: function (scope, el, attrs, ngModel) {
            //change event is fired when file is selected
            el.bind('change', function () {
                scope.$apply(function () {
                    ngModel.$setViewValue(el.val());
                    ngModel.$render();
                });
            });
        }
    };
});
//============================= Angular controller ==========================================
providerApp.controller("providerController", function ($scope, $filter, $compile, $http) {
    
    //========================= list of error messages for create provider ================ 
    $scope.Err_RequiredField = Err_RequiredField;
    $scope.Err_NameValidation = Err_NameValidation;
    $scope.Err_TextLength5_100 = Err_TextLength5_100;
    $scope.Err_OnlyNumber = Err_OnlyNumber;
    $scope.Err_IsExists = Err_IsExists;
    $scope.Err_MobileNo = Err_MobileNo;
    $scope.Err_select = Err_select;

    //==================== list of regular Expression for create provider ========================
    $scope.emailPattern = a_emailPattern;
    $scope.userNamePattern = a_userNamePattern;
    $scope.userMiddleNamePattern = a_userMiddleNamePattern;
    $scope.zipCodePattern = a_zipCodePattern;
    $scope.mobileNumberPattern = a_mobileNumberPattern;
    $scope.streetPattern = a_streetPattern;

    //=============================== Provider Categories =================
    $scope.providerCategories = [];
    $http.get('/Provider/GetCategoriesData').success(function (data) {
        //console.log(data);
        $scope.providerCategories = data;
    });

    //================================= Provider Relations data ==============================
    $scope.providerRelations = [{
        relationId: 0,
        relationName: "Employee"
    }, {
        relationId: 1,
        relationName: "Affiliate"
    }];
    //================================= Provider Titles data ==============================
    $scope.nameTitles = providerNameTitles;

    //================================= Provider Groups data ==============================
    $scope.groups = [];
    $http.get('/Provider/GetGroupData').success(function (data) {
        $scope.groups = data;
    });

    //================================= Provider Init data ==============================
    $scope.panelTitle = "Add Provider"
    $scope.providerFinalSubmitStatus = false;
    $scope.tabStatus = 0;
    $scope.newProviderTypes = [];
    $scope.partOfGroup = 0;
    $scope.addProviderData = {};

    //======== json formate data ================
    $scope.Provider = {};
    $scope.Provider.Personal = {};
    $scope.Provider.Personal.Contact = {};
    $scope.Provider.Address = {};
    $scope.Provider.Address.Country = "United States";
    $scope.Provider.Personal.Contact.CountryCode = "+1";
    $scope.Provider.ProviderDoc = {};

    $scope.ProviderID = {}; // Used for uploading CV

    $scope.countries = demoCountries;
    $scope.countryDailCodes = countryDailCodes;

    $scope.states = UsStates;
    $scope.ProviderTypeselected = false;

    $scope.AddProviderFail = false;

    $scope.isDocSent = true;

    $scope.uploadListOfProvider = false;
    $scope.listOfProviderAdded = false;

    $scope.Provider.GroupID = $scope.undefined;

    $scope.file = {};

    var fd; // cv form data reference

    //============================== Submit List Of Provider ==============================================
    $scope.submitListOfProvider = function () {
        $scope.uploadListOfProvider = false;
    };

    //================================= Provider Tyeps by Category ID Method ==============================
    $scope.getprovidertypes_by_category_Id = function (categoryId) {
        //$scope.Provider.ProviderTypeId = $scope.putempty;
        for (var i in $scope.providerCategories) {
            if ($scope.providerCategories[i].categoryId == categoryId) {
                //$scope.newProviderTypes = $scope.providerCategories[i].providerTypes;   //list of provider type according to category
                $scope.selectedCategory = $scope.providerCategories[i]; // using for preview selected category.
                //resetProviderTypeSelect("");
                break;
            }
        }

        $scope.isExistCAQHNumber($scope.Provider.CAQHNumber);
    };
    //================================ Provider Type name for view ============================
    $scope.$watch("Provider.ProviderTypeId", function (newValue, oldValue) {

        for (var i in $scope.newProviderTypes) {
            if ($scope.newProviderTypes[i].typeId == newValue) {
                $scope.selectedProvider = $scope.newProviderTypes[i];
                break;
            }
        }
        resetProviderTypeSelect(newValue);
    });
    //================================ Provider Relation name ouput for Preview =====================
    $scope.$watch("Provider.ProviderRelation", function (newValue, oldValue) {
        for (var i in $scope.providerRelations) {
            if ($scope.providerRelations[i].relationId == newValue) {
                $scope.ProviderRelationName = $scope.providerRelations[i].relationName;
                break;
            }
        }
    });
    //===================== append title according to update ============================
    $scope.$watch("Provider.Personal.Title", function (newValue, oldValue) {
        resetNameTitle(newValue);
    });
    //=============================Gender select with title ======================================
    $scope.$watch("Provider.Personal.Title", function (newValue, oldValue) {
        try {
            if (newValue.toLowerCase() == "mr" || newValue.toLowerCase() == "sir") {
                $scope.Provider.Personal.Gender = "M";
            }
            else if (newValue.toLowerCase() == "mrs" || newValue.toLowerCase() == "miss") {
                $scope.Provider.Personal.Gender = "F";
            }
            else {
                $scope.Provider.Personal.Gender = $scope.setTemp;
            }
        }
        catch (e) { }
    });
    //=========================== Method for get List of County ==========================
    $scope.$watch("Provider.Address.State", function (newValue, oldValue) {
        var Counties = [];
        for (var i in $scope.states) {
            if ($scope.states[i].State == newValue) {
                Counties.push($scope.states[i]);
            }
        }

        $scope.Counties = Counties;
        $scope.Provider.Address.County = $scope.putempty;
        $scope.Provider.Address.City = $scope.putempty;

        resetStatesSelection(newValue);
        resetCountySelection("");
        resetCitySelection("");
    });
    //=========================== Method for get List of City ==========================
    $scope.$watch("Provider.Address.County", function (newValue, oldValue) {
        var Cities = [];
        for (var i in $scope.Counties) {
            if ($scope.Counties[i].County == newValue) {
                Cities.push($scope.Counties[i]);
            }
        }
        $scope.Cities = Cities;
        $scope.Provider.Address.City = $scope.putempty;

        resetCountySelection(newValue);
        resetCitySelection("");
    });
    //=========================== Method for get List of ZipCode ==========================
    $scope.$watch("Provider.Address.City", function (newValue, oldValue) {
        resetCitySelection(newValue);
    })
    //========================== View Popover Contact Country Code ============================
    $scope.showContryCodeDiv = function () {
        $("#countryDailCodeContainer").show();
    };
    $scope.showContryCodeDiv1 = function () {
        $("#countryDailCodeContainer1").show();
    };
    //===================== append title according to update ============================
    $scope.$watch("Provider.Personal.Contact.CountryCode", function (newValue, oldValue) {
        resetContactContryCodeSelection(newValue);
    });
    //========================= Email Unique check ==================================
    $scope.$watch("Provider.Personal.Email", function (newValue, oldValue) {
        if (newValue != oldValue) {
            $http({ method: 'post', url: '/Provider/IsEmailExist', data: { email: $scope.Provider.Personal.Email }, })
        .success(function (data) {
            //console.log(data);
            $scope.IsExistEmailId = data;
        }).error(function (data) {
            $scope.IsExistEmailId = data;
            $scope.GetError = data;
        });
        }
    });
    //========================= Phone Number Unique check ==================================
    $scope.$watch("Provider.Personal.Contact.PhoneNo", function (newValue, oldValue) {
        if (newValue != oldValue) {
            $http({ method: 'post', url: '/Provider/IsPhoneNoExist', data: { contactInfo: $scope.Provider.Personal.Contact }, })
        .success(function (data) {
            //console.log(data);
            $scope.IsExistPhoneNumber = data;
        }).error(function (data) {
            $scope.IsExistPhoneNumber = data;
            $scope.GetError = data;
        });
        }
    });
    //========================== country code phone number check ===========================
    $scope.isExistPhoneNumber = function (invalid) {
        if (!invalid) {
            $http({ method: 'post', url: '/Provider/IsPhoneNoExist', data: { contactInfo: $scope.Provider.Personal.Contact }, })
        .success(function (data) {
            //console.log(data);
            $scope.IsExistPhoneNumber = data;
        }).error(function (data) {
            $scope.IsExistPhoneNumber = data;
            $scope.GetError = data;
        });
        }
    };

    //========================== Unique NPI Number Check ===========================
    $scope.isExistNPINumber = function (NPINumber) {
        if (NPINumber == "NPI123") {
            $scope.NPIExist = true;
        } else {
            $scope.NPIExist = false;
        }
    };

    //========================== Unique CAQH Number Check ===========================
    $scope.isExistCAQHNumber = function (CAQHNumber) {
        if (CAQHNumber == "CAQH123" && $scope.Provider.categoryId == "1") {
            $scope.CAQHExist = true;
        } else {
            $scope.CAQHExist = false;
        }
    };

    //=================================== Is Part of Group methods ==============================
    $scope.isPartOfGroup = function () {
        $scope.Provider.GroupID = $scope.puempty;
        resetGroupNameSelection("");
    };
    //=========================== method for reset group Name ==========================
    $scope.$watch("Provider.GroupID", function (newValue, oldValue) {
        if (newValue) {
            for (var i in $scope.groups) {
                if ($scope.groups[i].GroupID == newValue) { $scope.selectedGroup = $scope.groups[i]; break; }
            }
        } else {
            $scope.selectedGroup = {};
        }
        resetGroupNameSelection(newValue);
    });

    //================================= Tab Flow Validation Method ==============================
    $scope.nextForm = function (tabid) {
        $scope.tabStatus = tabid;
    };

    //================================= Primary info form view ===================================
    $scope.primaryInfoForm = function (tabid) {

        //if ($("#excelUpload").val() != "") {
        //  if (!isValidFileExtension($("#excelUpload").val())) {
        //    $("#excelUpload").val("");
        //        alert("Please Upload Excel File!!");
        //  } else {
        //        $scope.uploadListOfProvider = true;
        //    }
        //} else {
            
        //}
        $scope.AddProviderFormTab1 = true;
        $scope.tabStatus = tabid;
    };
    
    //================================= Organization info form view only click click ===================================
    $scope.organizationInfoForm = function () {
        $scope.AddProviderFormTab2 = true;
        $scope.tabStatus = 2;

        $('body,html').animate({
            scrollTop: 0
        });
    };

    //================================= Add Provider Preview Method ==============================
    //$scope.previewAddProvider = function (formStates) {
    //---------------- click click method -------------------
    $scope.previewAddProvider = function () {
        //if (formStates || $scope.Provider.IsPartOfGroup == 'true' || $scope.IsExistEmailId == 'True' || $scope.IsExistPhoneNumber == 'True') {
        //    if ($scope.Provider.GroupID == null || formStates || $scope.IsExistEmailId == 'True' || $scope.IsExistPhoneNumber == 'True') {
        //        $scope.hasError = true;
        //    } else {
                
        //        $scope.AddProviderFormTab2 = true;
        //        $scope.tabStatus = 2;
        //        $('body,html').animate({
        //            scrollTop: 0
        //        });
        //    }
        //} else {
        //    $scope.AddProviderFormTab2 = true;
        //    $scope.tabStatus = 2;
           
        //    $('body,html').animate({
        //        scrollTop: 0
        //    });
        //}

        $scope.AddProviderFormTab3 = true;
        $scope.tabStatus = 3;

        $('body,html').animate({
            scrollTop: 0
        });
    };
    //============================ confirmation method for submit ===========================
    $scope.confirmationAddProvider = function (formStates) {
        if (formStates || $scope.Provider.IsPartOfGroup == 'true' || $scope.IsExistEmailId == 'True' || $scope.IsExistPhoneNumber == 'True') {
            if ($scope.Provider.GroupID == null || formStates || $scope.IsExistEmailId == 'True' || $scope.IsExistPhoneNumber == 'True') {
                $scope.hasError = true;
            } else {
                $('#myModal').modal();
            }
        } else {
            $('#myModal').modal();
        }
    };
    //============================ modal confirmation action ===========================
    $scope.confirmProvider = function () {
       
        //-------- Save Provider Post Function ------
        // $http.post('/Provider/SaveProvider', $scope.Provider).success(function (data) {
        // $scope.ProviderID = data.ProviderID;
        // $scope.providerFinalSubmitStatus = true;
        // $scope.panelTitle = "Confirmation"
        // $scope.isAdded = true;
        // $scope.ProviderAddedMsg = data.Message;
        // $('#myModal').modal("hide");
        // $scope.AddProviderFail = false;
        // }).error(function (data) {
        // $scope.isAdded = false;
        // $scope.ProviderAddedMsg = data.Message;
        // $('#myModal').modal("hide");
        // $scope.AddProviderFail = true;
        // });

        //-------- demo click click action ------
        $scope.ProviderID = 1;
        $scope.providerFinalSubmitStatus = true;
        $scope.panelTitle = "Confirmation"
        $scope.isAdded = true;
        $scope.ProviderAddedMsg = "Provider Added Successfully";
        $('#myModal').modal("hide");
        $scope.AddProviderFail = false;
        //-------- demo click click action ------
        
        $scope.EmailBody = emailTemplate;
        $scope.EmailSubject = emailSubject;

    };
    $scope.sendMailProvider = function () {

        var EmailModel = {};

        var Attachments = new Array();

        EmailModel.To = $scope.Provider.Personal.Email;

        EmailModel.From = "venkat@pratian.com";  // hardcoded data 

        EmailModel.Subject = $scope.EmailSubject;

        EmailModel.Body = $scope.EmailBody;

        EmailModel.Attachments = Attachments;

        $scope.EmailModel = EmailModel;

        $scope.EmailAddedMsg = "";
        //---------------------- Email Send calling server method -----------------
        //$http.post('/EmailService/Send', $scope.EmailModel).success(function (data) {
        //    $scope.isEmailSent = true;
        //    $scope.EmailAddedMsg = data;
        //}).error(function (data) {
        //    $scope.isEmailSent = false;
        //    $scope.EmailAddedMsg = data;
        //});
        //-------- demo click click action ------
        $scope.isEmailSent = true;
        $scope.EmailAddedMsg = "Email Sent Successfully!!";
        //-------- demo click click action ------
    };

    $scope.setFile = function (files) {
        if (files[0].size <= 1024 * 1024 * 5) {

            if (!isValidExtension(files[0].name)) {
                $('input:file').val("");
            }
        }
        else {
            $('input:file').val("");
            alert("CV size cant exceed that 5 MB");
        }
 };

    function isValidExtension(fileName) {

        if (fileName.lastIndexOf(".") > 0) {
            fileExtension = fileName.substring(fileName.lastIndexOf(".") + 1, fileName.length);
        }
        if (fileExtension == "pdf" || fileExtension == "doc" || fileExtension == "docx") {
            return true;
        }
        else {
            alert("You must select a PDF/DOC/DOCX file for upload");
            return false;
        }
    }

});

//================================ Excel File Extention Validation ================================

function isValidFileExtension(fileName) {

    if (fileName.lastIndexOf(".") > 0) {
        fileExtension = fileName.substring(fileName.lastIndexOf(".") + 1, fileName.length);
    }
    if (fileExtension == "xls" || fileExtension == "xlsx") {
        return true;
    }
    else {
        //alert("You must select a PDF/DOC/DOCX file for upload");
        return false;
    }
}
//============================= apply select2 for dropdown selects and text fields =======================
$(document).ready(function () {

    $("#countryDailCodeContainer").hide();
    $("#countryDailCodeContainer1").hide();

    $("#selectProviderType").select2({
        placeholder: "Select a Provider Type"
        //allowClear: true
    });
    $("#selectNameTitle").select2({
        placeholder: "Select a Title"
    });

    $('#selectCountry').select2().select2("enable", false);

    $("#selectState").select2({
        placeholder: "Select a State"
    });
    $("#selectCounty").select2({
        placeholder: "Select a County"
    });
    $("#selectCity").select2({
        placeholder: "Select a City"
    });
    $("#selectGroup").select2({
        placeholder: "Select a Group"
    });
    $("#selectStatus").select2({
        placeholder: "Select a Status"
    });
    $("#ContactContryCode").select2({
        placeholder: "Select a Country Code"
    });
    $("#ContactContryCode1").select2({
        placeholder: "Select a Country Code"
    });
});

//================== document click for popover hide ===========================

$(document).click(function (event) {
    if (!$(event.target).hasClass("btn")
        && $(event.target).parents("#countryDailCodeContainer").length === 0) {
        $("#countryDailCodeContainer").hide();
    }
    if (!$(event.target).hasClass("btn")
            && $(event.target).parents("#countryDailCodeContainer1").length === 0) {
        $("#countryDailCodeContainer1").hide();
    }
})
//======================= reset select2 selected data methods =========================
function resetProviderTypeSelect(value) {
    $("#selectProviderType").select2("val", value).attr("placeholder", "Select a Provider Type");
}
function resetNameTitle(value) {
    $("#selectNameTitle").select2("val", value);
}
function resetGroupNameSelectTag(value) {
    $("#selectGroup").select2("val", value);
}
function resetStatesSelection(value) {
    $("#selectState").select2("val", value);
}
function resetCountySelection(value) {
    $("#selectCounty").select2("val", value);
}
function resetCitySelection(value) {
    $("#selectCity").select2("val", value);
}
function resetContactContryCodeSelection(value) {
    $("#ContactContryCode").select2("val", value);
}
function resetGroupNameSelection(value) {
    $("#selectGroup").select2("val", value);
}

var emailSubject = "New Provider Information";

var emailTemplate = "Dear Provider," +
            "<br><br>Welcome to the Access Family!" +

            "<br><br>We will be bringing you online with our systems." +

            "<br><br>Please complete the paperwork attached to get your credentialing started." +
            " Please be sure to sign all paperwork in BLUE ink at each place (most signatures are marked)." +
            " This is indicative of the required originals when we forward paperwork for processing." +
            " Please DO NOT DATE any of the pages." +

            "<br><br>Also attached is a list of documents needed." +
            " Kindly return copies of your documents (we do not need the originals) with your signed paperwork." +
            " Any missing or incomplete information will delay the credentialing process." +
            "<br><br>Please provide documents listed below :" +

            "<br><ul type='square'>" +
            "<li>Application or CAQH Application w/Attestation</li>" +
            "<li>Medical License</li>" +
            "<li>Curriculum Vitae (written explanation of gaps greater than 6 months)</li>" +
            "<li>Board Certification (if applicable)</li>" +
            "<li>ECFMG (if applicable)</li>" +
            "<li>Current DEA Certificate</li>" +
            "<li>Current Malpractice Liability Insurance Certificate</li>" +
            "<li>Copy of Hospital Privilege Letters (if applicable)</li>" +
            "<li>Attestation of Total Patient Load (PCP’s only)</li>" +
            "<li>Attestation of Site Visit</li>" +
            "<li>Medical School Diploma, Residency and Fellowship Certificate</li>" +
            "<li>Special Certifications (ex. ACLS, PALS, BLS, CPR)</li>" +
            "<li>Copy of Drivers License, SSC, and passport</li>" +
            "<li>CME Certificates within the past two years</li>" +
            "</ul>" +

            "Any missing or expired documentation will delay the credentialing process. If you have any questions, please don’t hesitate to contact me. Thank you for your cooperation." +

            "<br><br>Please contact us if you have any questions." +

            "<br><br><b>Regards,</b>" +

            "<br><br><table><tbody><tr>" +
            "<td width='150px'><b>Leslie Hedick</b><br>HR Director</td>"+
            "<td><b>Jeanine Martin</b><br>Credentialing Coordinator</td></tr></tbody></table>" +
            
            "<br><br><address><b>Jeanine Martin</b> <br>Credentialing Coordinator, Access 2 Healthcare Physicians LLC" +
            "<br>P. 352-277-5307 ext 7801, F. 352-277-5309 or 352-277-5288"+
            "<br><a href='mailto:jmartin@accesshealthcarellc.net?subject=New Provider Information'>jmartin@accesshealthcarellc.net</a></address>";