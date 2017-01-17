/// <reference path="../../../../Scripts/Lib/Angular/angular.min.js" />
/// <reference path="../../../../Scripts/Lib/Angular/angular.min.js" />

//------------------get profile update service---------------------------
profileApp.factory('profileUpdates', function () {
    var profileUpdate = {};

    profileUpdate.getUpdates = function (section, subsection) {
        var profileUpdateObj = JSON.parse(profileUpdates);
        var flag = false;
        if (profileUpdateObj != null) {
            for (var i = 0; i < profileUpdateObj.length; i++) {
                if (profileUpdateObj[i].Section == section && profileUpdateObj[i].SubSection == subsection) {
                    flag = true;
                    break;
                }
            }
        }
        
        return flag;

    }
    return profileUpdate;
});

//---------------------------- Order By Empty Bottom ----------------------
profileApp.filter('orderEmpty', function () {
    return function (array, key, type) {
        var present, empty, result;

        if (!angular.isArray(array))
            return;

        present = array.filter(function (item) {
            return item[key];
        });

        empty = array.filter(function (item) {
            return !item[key];
        });

        switch (type) {
            case 'toBottom':
                result = present.concat(empty);
                break;
            case 'toTop':
                result = empty.concat(present);
                break;
            default:
                result = array;
                break;
        }
        return result;
    };
});

//------------------ File Selected validation ---------------------------
profileApp.directive('validFile', function () {
    return {
        require: 'ngModel',
        link: function (scope, el, attrs, ngModel) {
            ngModel.$render = function () {
                ngModel.$setViewValue(el.val());
            };

            el.bind('change', function () {
                scope.$apply(function () {
                    ngModel.$render();
                });
            });
        }
    };
});
//=========================== Controller declaration ==========================
profileApp.controller('profileDemographicsController', ['$scope', '$rootScope', '$http', '$filter', 'masterDataService', 'locationService', 'messageAlertEngine', 'profileUpdates', '$timeout',
function ($scope, $rootScope, $http, $filter, masterDataService, locationService, messageAlertEngine, profileUpdates, $timeout) {

    $rootScope.printData = function (id, title) {
        var divToPrint = document.getElementById(id);
        $('#hiddenPrintDiv').empty();
        $('#hiddenPrintDiv').append(divToPrint.innerHTML);

        // Removing the last column of the table
        $('#hiddenPrintDiv .hideData').remove();

        $('#hiddenPrintDiv .changeWidth').removeAttr("colspan");
        $('#hiddenPrintDiv .changeWidth').attr("colspan", 3);

        // Creating a window for printing
        var mywindow = window.open('', $('#hiddenPrintDiv').html(), 'height=800,width=800');
        mywindow.document.write('<center><b style="font-size:large">' + title + '</b></center></br>');
        mywindow.document.write('<html><head><title>' + title + '</title>');
        mywindow.document.write('<link rel="stylesheet" href="/Content/SharedCss/app.css" type="text/css" media="all"/>');
        mywindow.document.write('<link rel="stylesheet" href="/Content/SharedCss/bootstrap.min.css" type="text/css" />');
        mywindow.document.write('<link rel="stylesheet" href="/Content/SharedFonts/font-awesome-4.1.0/css/font-awesome.min.css" type="text/css" />');
        mywindow.document.write('<style>.ng-hide:not(.ng-hide-animate) {display: none !important;}@page{size: auto;margin-bottom: 5mm;margin-top:7mm;}th{text-align:center;}</style>');
        mywindow.document.write('<style>table { table-layout: fixed; } table th, table td { overflow: hidden; word-wrap: break-word; }</style>');
        mywindow.document.write('</head><body media="print" style="background-color:white"><table class="table table-bordered"></td>');
        mywindow.document.write($('#hiddenPrintDiv').html());
        mywindow.document.write('</table></body></html>');
        mywindow.document.close();
        mywindow.focus();
        setTimeout(function () {
            mywindow.print();
            mywindow.close();
        }, 1000);
        return true;
    }
    $scope.setFiles = function (file) {
        $(file).parent().parent().find(".jancyFileWrapTexts").find("span").width($(file).parent().parent().width() < 243 ? $(file).parent().parent().width() : 243);

    }
    ////----------------------------------Download--------------------------------
    //$rootScope.downloadTableData = function (id,title) {
    //    var divToPrint = document.getElementById(id);
    //    //$('#hiddenPrintDiv').empty();
    //    //$('#hiddenPrintDiv').append(divToPrint.innerHTML);
    //   var element= $(divToPrint).clone();
    //   element.find('tr th:last-child, td:last-child').remove(),
    //  element.find('tr th:last-child, td:last-child').remove();
    //    //$('#hiddenPrintDiv thead:last-child,#hiddenPrintDiv th:last-child, #hiddenPrintDiv td:last-child').remove();
    //   source = element.html();

    //   var pdf = new jsPDF('p','pt','letter');
    //    var specialElementHandlers = {
    //        '#bypassme': function (element, renderer) {
    //            return true;
    //        }
    //    };
    //    margins = {
    //        top: 40,
    //        bottom: 60,
    //        left: 60,
    //        width: 700
    //    };
    //    //var dataTable = "<table border=1 class='table table-bordered'>" + $('#hiddenPrintDiv').html() + "</table>";
    //    pdf.text(220, 30,  title  );
    //    pdf.fromHTML("<table class='table table-striped table-bordered table-condensed'>" + source + "</table>", // HTML string or DOM elem ref.
    //           margins.left, // x coord
    //           margins.top, { // y coord
    //               'width': margins.width, // max width of content on PDF
    //               'elementHandlers': specialElementHandlers
    //           },
    //           function (dispose) {

    //               pdf.save(title + '.pdf');
    //           },
    //        margins);
    //}


    var isPendingUpadate = function (section, subsection) {
        var profileUpdateObj = JSON.parse(profileUpdates);
        for (var i = 0; i < profileUpdateObj.length; i++) {
            if (profileUpdateObj[i].Section == section && profileUpdateObj[i].SubSection == subsection) {

            }
        }
    }


    //--------------------------------- User Profile Data ----------------------------------
    $scope.Provider = {};
    $scope.message = {};
    $scope.isRemoved = false;
    //---------------------------- Personal Details ---------------------------
    $rootScope.$on('ProfilePhotoPath', function (event, val) {
       
        $scope.Provider.ProfilePhotoPath = val;
    });
    $rootScope.$on('PersonalDetail', function (event, val) {
        
        $scope.Provider.PersonalDetails = val;
        $rootScope.lastName = $scope.Provider.PersonalDetails.LastName;
        if (!$scope.Provider.PersonalDetails.MaritalStatusType) {
            $scope.Provider.PersonalDetails.MaritalStatusType = "";
        }
        //$scope.getTemplate('Demographic', 'Personal Detail');
        $scope.PersonalDetailsPendingRequest = profileUpdates.getUpdates('Demographic', 'Personal Detail');

    });
    $rootScope.$on('OtherLegalNames', function (event, val) {
        $scope.Provider.OtherLegalNames = val;
        $scope.OtherLegalNamesPendingRequest = profileUpdates.getUpdates('Demographic', 'Other Legal Name');

    });
    $rootScope.$on('HomeAddresses', function (event, val) {
        $scope.HomeAddresses = val;
        $scope.HomeAddressesPendingRequest = profileUpdates.getUpdates('Demographic', 'Home Address');
    });
    $rootScope.$on('ContactDetail', function (event, val) {
        if (val) {
            for (var i in val.PreferredContacts) {
                if (val.PreferredContacts[i].PreferredWrittenContactType == 1) {
                    val.PreferredContacts[i].ContactType = "Home Phone"
                }
            }
        }
        $scope.Provider.ContactDetails = val;
        $scope.ContactDetailPendingRequest = profileUpdates.getUpdates('Demographic', 'Contact Detail');
    });
    $rootScope.$on('PersonalIdentification', function (event, val) {
        $scope.Provider.PersonalIdentification = val;
        $scope.PersonalIdentificationPendingRequest = profileUpdates.getUpdates('Demographic', 'Personal Identification');
    });
    $rootScope.$on('BirthInformation', function (event, val) {
        if (val) {
            var TemporaryVariable = (val.DateOfBirth.split(" ")[0]).split("-");
            if (TemporaryVariable.length == 1) {
                var TemporaryVariable1 = TemporaryVariable[0].split("/");
                val.DateOfBirth = TemporaryVariable1[0] + "/" + TemporaryVariable1[1] + "/" + TemporaryVariable1[2];
            }
            else { val.DateOfBirth = TemporaryVariable[0] + "/" + TemporaryVariable[1] + "/" + TemporaryVariable[2]; }
            
        }
        $scope.Provider.BirthInformation = val;
        $scope.BirthInformationPendingRequest = profileUpdates.getUpdates('Demographic', 'Birth Information');
    });
    $rootScope.$on('VisaDetail', function (event, val) {
        if (val && val.VisaInfo) {
            if (!val.VisaInfo.VisaTypeID) {
                val.VisaInfo.VisaTypeID = "";
            }
            if (!val.VisaInfo.VisaStatusID) {
                val.VisaInfo.VisaStatusID = "";
            }
            if (!val.VisaInfo.CountryOfIssue) {
                val.VisaInfo.CountryOfIssue = "";
            }
        }
        $scope.Provider.VisaDetails = val;
        $scope.VisaDetailPendingRequest = profileUpdates.getUpdates('Demographic', 'Citizenship Information');
    });
    $rootScope.$on('LanguageInfo', function (event, val) {
        $scope.Provider.LanguageInfo = val;
        $scope.LanguageInfoPendingRequest = profileUpdates.getUpdates('Demographic', 'Language Info');
    });

    $rootScope.$on("LoadRequireMasterDataDemographics", function () {
        //============================= Data From Master Data Table Required For Visa Details ======================
        $(function () {
            try {
                $http.get(rootDir + "/Location/GetStates")
           .then(function (response) {
               $scope.States = response.data;
           });
            }
            catch (e) { };
        });

        masterDataService.getMasterData(rootDir + "/Profile/MasterData/GetAllProviderTypes").then(function (val) {
            $scope.ProviderTypes = val;
        });

        masterDataService.getMasterData(rootDir + "/Profile/MasterData/getallvisatypes").then(function (val) {
            $scope.VisaTypes = val;
        });
        masterDataService.getMasterData(rootDir + "/Profile/MasterData/getallvisastatus").then(function (val) {
            $scope.VisaStatuses = val;
        });

        masterDataService.getMasterData(rootDir + "/Profile/MasterData/GetAllProviderLevels").then(function (val) {
            $scope.ProviderLevels = val;
        });
    });

    //========================== list of Languages =================
    $scope.Languages = Languages;

    $scope.CountryDialCodes = countryDailCodes;

    $scope.CountryOfissue = CountryOfissue;

    $scope.PreferredContacts = [{
        PreferredContactID: null,
        ContactType: "Home Phone",
        PreferredWrittenContactType: 1,
        StatusType: 1,
        PreferredIndex: 1
    }, {
        PreferredContactID: null,
        ContactType: "Fax",
        PreferredWrittenContactType: 2,
        StatusType: 1,
        PreferredIndex: 2
    }, {
        PreferredContactID: null,
        ContactType: "Mobile",
        PreferredWrittenContactType: 3,
        StatusType: 1,
        PreferredIndex: 3
    }, {
        PreferredContactID: null,
        ContactType: "Email",
        PreferredWrittenContactType: 4,
        StatusType: 1,
        PreferredIndex: 4
    }, {
        PreferredContactID: null,
        ContactType: "Pager",
        PreferredWrittenContactType: 5,
        StatusType: 1,
        PreferredIndex: 5
    }];

    //------------------------------------------------------------------Address Auto-Complete---------------------------------------------------------------------------//
    /*
	 Method addressAutocomplete() gets the details of a location
	 Method takes input of location details entered in the text box.
	 */
    $scope.addressHomeAutocomplete = function (location) {
        $scope.resetAddressModels();

        $scope.tempObject.City = location;
        if (location.length > 1 && !angular.isObject(location)) {
            locationService.getLocations(location).then(function (val) {
                $scope.Locations = val;
            });
        } else if (angular.isObject(location)) {
            $scope.setAddressModels(location);
        }
    };
    $scope.selectedLocation1 = function (location) {
        $scope.setAddressModels(location);
        $(".ProviderTypeSelectAutoList").hide();
    };
    $scope.resetAddressModels = function () {
        $scope.tempObject.State = "";
        $scope.tempObject.Country = "";
    };

    $scope.setAddressModels = function (location) {
        $scope.tempObject.City = location.City;
        $scope.tempObject.State = location.State;
        $scope.tempObject.Country = location.Country;

    };
    //-------------------------------- Birth Information Change Status ------------------------------
    $scope.addressAutocompleteForBirthInformation = function (location) {
        $scope.resetBirthAddressModels();
        $scope.tempObject.CityOfBirth = location;
        if (location.length > 1 && !angular.isObject(location)) {
            locationService.getLocations(location).then(function (val) {
                $scope.Locations = val;
            });
        } else if (angular.isObject(location)) {
            $scope.setBirthAddressModels(location);
        }
    };

    $scope.selectedLocation = function (location) {
        $scope.setBirthAddressModels(location);
        $(".ProviderTypeSelectAutoList").hide();
    };

    $scope.resetBirthAddressModels = function () {
        $scope.tempObject.StateOfBirth = "";
        $scope.tempObject.CountryOfBirth = "";
    };

    $scope.setBirthAddressModels = function (location) {
        $scope.tempObject.CityOfBirth = location.City;
        $scope.tempObject.StateOfBirth = location.State;
        $scope.tempObject.CountryOfBirth = location.Country;
    };
    //------------------ Provider Type (Title) Select un-select Method here -----------------------------
    $scope.ViewPersonalDetails = true;
    $scope.PersonalDetailsToggle = function (condtion) {
        if (condtion == 'EditPersonalDetails') {
            $rootScope.visibilityControl = condtion;
            $scope.getProviderTypesAndTempData();
        } else {
            $rootScope.visibilityControl = condtion;
        }
    };
    $scope.getProviderTypesAndTempData = function () {
        $scope.TempPersonalDetailsForEdit = angular.copy($scope.Provider.PersonalDetails);
        $scope.tempProviderTypes = angular.copy($scope.ProviderTypes);
        $scope.tempSelectedDeactivatedProviderTypes = [];
        if ($scope.TempPersonalDetailsForEdit) {
            if ($scope.TempPersonalDetailsForEdit.ProviderTitles) {
                for (var i = 0; i < $scope.TempPersonalDetailsForEdit.ProviderTitles.length; i++) {
                    if ($scope.TempPersonalDetailsForEdit.ProviderTitles[i].StatusType == 2) {
                        $scope.tempSelectedDeactivatedProviderTypes.push($scope.TempPersonalDetailsForEdit.ProviderTitles[i]);
                        $scope.TempPersonalDetailsForEdit.ProviderTitles.splice(i, 1);
                        i--;
                    }
                }
                for (var i in $scope.TempPersonalDetailsForEdit.ProviderTitles) {
                    for (var j in $scope.tempProviderTypes) {
                        if ($scope.TempPersonalDetailsForEdit.ProviderTitles[i].ProviderType.ProviderTypeID == $scope.tempProviderTypes[j].ProviderTypeID && $scope.TempPersonalDetailsForEdit.ProviderTitles[i].StatusType == 1) {
                            $scope.tempProviderTypes.splice(j, 1);
                        } else if ($scope.TempPersonalDetailsForEdit.ProviderTitles[i].ProviderType.ProviderTypeID == $scope.tempProviderTypes[j].ProviderTypeID && $scope.TempPersonalDetailsForEdit.ProviderTitles[i].StatusType == 2) {
                            $scope.tempSelectedDeactivatedProviderTypes.push($scope.TempPersonalDetailsForEdit.ProviderTitles[i]);
                            $scope.TempPersonalDetailsForEdit.ProviderTitles.splice(i, 1);
                        }
                    }
                }
                $scope.getTitleCount();

            } else {
                $scope.TempPersonalDetailsForEdit.ProviderTitles = [];
            }
        } else {
            $scope.TempPersonalDetailsForEdit = {
                ProviderTitles: []
            };
        }

       
    };
    //-------------------------------------- Select Provider type ------------------------------------------
    $scope.getTitleCount = function () {
        $scope.titleCount = $scope.TempPersonalDetailsForEdit.ProviderTitles.length;
    };


    $scope.SelectProviderType = function (providertype) {
        var status = false;
        $scope.titleCount = $scope.titleCount + 1;

        for (var i = 0; i < $scope.tempSelectedDeactivatedProviderTypes.length; i++) {
            if ($scope.tempSelectedDeactivatedProviderTypes[i].ProviderType.ProviderTypeID == providertype.ProviderTypeID) {
                $scope.tempSelectedDeactivatedProviderTypes[i].StatusType = 1;
                $scope.TempPersonalDetailsForEdit.ProviderTitles.push($scope.tempSelectedDeactivatedProviderTypes[i]);
                $scope.tempSelectedDeactivatedProviderTypes.splice(i, 1);
                status = true;
                break;
            }
        }
        if (!status) {
            $scope.TempPersonalDetailsForEdit.ProviderTitles.push({
                ProviderType: providertype,
                ProviderTitleID: null,
                ProviderTypeId: providertype.ProviderTypeID,
                StatusType: 1
            });
        }
        $scope.searchproviderType = '';
        $("#providerTypesearch11").val("");
        $scope.tempProviderTypes.splice($scope.tempProviderTypes.indexOf(providertype), 1);
    };
    //------------------------------------- UN-select Provider type -----------------------------------------

    $scope.ActionProviderType = function (providerTitle, condition) {

        if (condition == "remove") {
            $scope.titleCount = $scope.titleCount - 1;
            $scope.TempPersonalDetailsForEdit.ProviderTitles.splice($scope.TempPersonalDetailsForEdit.ProviderTitles.indexOf(providerTitle), 1);
            $scope.tempProviderTypes.push(providerTitle.ProviderType);
        } else if (condition == "deactivate") {
            if ($scope.titleCount != 1) {
                $scope.titleCount = $scope.titleCount - 1;
            }
            var numberOfActive = 0;
            for (var i in $scope.TempPersonalDetailsForEdit.ProviderTitles) {
                if ($scope.TempPersonalDetailsForEdit.ProviderTitles[i].StatusType == 1) {
                    numberOfActive++;
                }
            }
            if (numberOfActive == 1) {
                providerTitle.StatusType = 1;
            } else {
                providerTitle.StatusType = 2;
            }

        } else if (condition == "activate") {
            if ($scope.titleCount >= 0) {
                $scope.titleCount = $scope.titleCount + 1;
            }

            providerTitle.StatusType = 1;

        }
    };

    //-------------------------------------- File Upload Action ---------------------------------------------
    $scope.ProfileUpload = function (Form_Id) {
        $scope.valstaus = true;
        ResetFormForValidation($("#" + Form_Id));
        if ($("#" + Form_Id).valid()) {

            var $form = $("#ProfilePic")[0];

            $.ajax({
                url: rootDir + '/Profile/Demographic/FileUploadAsync?profileId=' + profileId,
                type: 'POST',
                data: new FormData($form),
                async: false,
                cache: false,
                contentType: false,
                processData: false,
                success: function (data) {
                    
                    try {
                        if (data.status == "true") {
                            $scope.Provider.ProfilePhotoPath = data.ProfileImagePath;
                            $rootScope.visibilityControl = "";
                            messageAlertEngine.callAlertMessage("alertPersonalDetailsSuccess", "Profile Picture uploaded successfully.", "success", true);
                        } else {
                            messageAlertEngine.callAlertMessage("alertPersonalDetailsSuccess", data.status, "danger", true);
                        }
                    } catch (e) {
                     
                    }
                }
            });
        }
    };

    //-------------------------------------- File Upload Action ---------------------------------------------
    $scope.ProfilePictureRemove = function (profilePicpath) {
        $.ajax({
            url: rootDir + '/Profile/Demographic/FileRemoveAsync?profileId=' + profileId,
            type: 'POST',
            data: new FormData(profilePicpath),
            async: false,
            cache: false,
            contentType: false,
            processData: false,
            success: function (data) {
             
                try {
                    if (data.status == "true") {
                        $scope.Provider.ProfilePhotoPath = data.ProfileImagePath;
                        $rootScope.visibilityControl = "";
                        messageAlertEngine.callAlertMessage("alertPersonalDetailsSuccess", "Profile Picture removed successfully.", "success", true);
                    } else {
                        messageAlertEngine.callAlertMessage("alertPersonalDetailsSuccess", data.status, "danger", true);
                    }
                } catch (e) {
                   
                }
            }
        });
    };
    //----------------------------------- Update Personal Details Function --------------------------------------
    $scope.DemographicsUpdatePersonalDetails = function (Form_Id) {
        ResetFormForValidation($("#" + Form_Id));
        if ($("#" + Form_Id).valid() && $scope.TempPersonalDetailsForEdit.ProviderTitles.length > 0) {
            $scope.isHasError = false;
            $.ajax({
                url: rootDir + '/Profile/Demographic/UpdatePersonalDetailsAsync?profileId=' + profileId,
                type: 'POST',
                data: new FormData($("#" + Form_Id)[0]),
                async: false,
                cache: false,
                contentType: false,
                processData: false,
                success: function (data) {
                    
                    try {
                        if (data.status == "true") {
                            $scope.PersonalDetailsPendingRequest = true;
                            if (data.personalDetail.ProviderTitles.length > 0) {
                                for (var i in data.personalDetail.ProviderTitles) {
                                    for (var j = 0; j < $scope.ProviderTypes.length; j++) {
                                        if (data.personalDetail.ProviderTitles[i].ProviderTypeId == $scope.ProviderTypes[j].ProviderTypeID) {
                                            data.personalDetail.ProviderTitles[i].ProviderType = $scope.ProviderTypes[j];
                                        }
                                    }
                                }
                            }
                            data.personalDetail.ProviderLevel = $filter('filter')($scope.ProviderLevels, { ProviderLevelID: data.personalDetail.ProviderLevelID })[0];
                            $scope.Provider.PersonalDetails = data.personalDetail;
                            if (!$scope.Provider.PersonalDetails.MaritalStatusType) {
                                $scope.Provider.PersonalDetails.MaritalStatusType = "";
                            }
                            FormReset($("#" + Form_Id));
                            $rootScope.visibilityControl = "";
                            messageAlertEngine.callAlertMessage("alertPersonalDetailsSuccess", "Personal Details updated successfully.", "success", true);
                        } else {
                            messageAlertEngine.callAlertMessage("alertPersonalDetailsError", data.status.split(","), "danger", true);
                        }
                    } catch (e) {
                      
                    }
                }
            });
        } else {
            $scope.isHasError = true;
            //messageAlertEngine.callAlertMessage("alertPersonalDetailsError", "Sorry! Personal Information could not be updated.", "danger", true);
           
        }
    };

    //----------------- save other legal name for UI Only --------------------
    $scope.SaveOtherLegalNameTemp = function () {
        messageAlertEngine.callAlertMessage("alertOtherLegalNameSuccess", "Other Legal Name Updated successfully.", "success", true);
    };

    //----------------------------------- Add Personal Details Function --------------------------------------
    $scope.saveOtherLegalName = function (Form_Div_Id) {
        var $form = $("#" + Form_Div_Id).find("form");
        var myData = {};
        ResetFormForValidation($form);
        if ($form.valid()) {
            $.ajax({
                url: rootDir + '/Profile/Demographic/AddOtherLegalNameAsync?profileId=' + profileId,
                type: 'POST',
                data: new FormData($form[0]),
                async: false,
                cache: false,
                contentType: false,
                processData: false,
                success: function (data) {
                  
                    try {
                        if (data.status == "true") {
                            data.otherLegalName.EndDate = ConvertDateFormat(data.otherLegalName.EndDate);
                            data.otherLegalName.StartDate = ConvertDateFormat(data.otherLegalName.StartDate);
                            $scope.Provider.OtherLegalNames.push(data.otherLegalName);
                            $rootScope.visibilityControl = "";
                            //$rootScope.visibilityControl = $scope.Provider.OtherLegalNames.length-1 + "_ViewOtherLegalName";
                            myData = data;
                            FormReset($form);
                            messageAlertEngine.callAlertMessage("alertOtherLegalNameSuccess", "Other Legal Name saved successfully.", "success", true);
                        } else {
                            messageAlertEngine.callAlertMessage("alertOtherLegalNameError", data.status.split(","), "danger", true);
                        }
                    } catch (e) {
                       
                    }
                }
            });
        } else {
          
        }
        $scope.OtherLegalNamesPendingRequest = false;
        $rootScope.$broadcast('UpdateOtherLegalNames', myData);

    };
    //--------------------------- Update Other Legal Name Function ------------------------------
    $scope.updateOtherLegalName = function (Form_Div_Id, index) {
        var $form = $("#" + Form_Div_Id).find("form");
        var myData = {};
        ResetFormForValidation($form);
        if ($form.valid()) {
            $.ajax({
                url: rootDir + '/Profile/Demographic/UpdateOtherLegalNameAsync?profileId=' + profileId,
                type: 'POST',
                data: new FormData($form[0]),
                async: false,
                cache: false,
                contentType: false,
                processData: false,
                success: function (data) {
                    try {

                        if (data.status == "true") {
                            data.otherLegalName.EndDate = ConvertDateFormat(data.otherLegalName.EndDate);
                            data.otherLegalName.StartDate = ConvertDateFormat(data.otherLegalName.StartDate);
                            for (var i in $scope.Provider.OtherLegalNames) {
                                if ($scope.Provider.OtherLegalNames[i].OtherLegalNameID == data.otherLegalName.OtherLegalNameID) {
                                    $scope.Provider.OtherLegalNames[i] = data.otherLegalName;
                                    break;
                                }
                            }
                            //$rootScope.visibilityControl = "";
                            $rootScope.visibilityControl = index + "_ViewOtherLegalName";
                            myData = data;
                            FormReset($form);
                            $scope.OtherLegalNamesPendingRequest = true;
                            messageAlertEngine.callAlertMessage("alertOtherLegalNameSuccess" + index, "Other Legal Name updated successfully.", "success", true);
                        } else {
                            messageAlertEngine.callAlertMessage("alertOtherLegalNameError" + index, data.status.split(","), "danger", true);
                        }
                    } catch (e) {
                      
                    }
                }
            });
        } else {
            
        }

        $rootScope.$broadcast('UpdateOtherLegalNames', myData);

    };

    $scope.initOtherLegalNameWarning = function (oln) {
        if (angular.isObject(oln)) {
            $scope.tempOtherLegalName = oln;
        }
        $('#otherLegalNameWarningModal').modal();
    };

    $scope.removeOtherLegalName = function (OtherLegalNames) {
        var validationStatus = false;
        var url = null;
        var myData = {};
        var $formData = null;
        $formData = $('#removeOtherLegalName');
        url = rootDir + "/Profile/Demographic/RemoveOtherLegalName?profileId=" + profileId;
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
                   
                    try {
                        if (data.status == "true") {
                            $scope.OtherLegalNamesPendingRequest = true;
                            var obj = $filter('filter')($scope.Provider.OtherLegalNames, { OtherLegalNameID: data.otherLegalName.OtherLegalNameID })[0];
                            $scope.Provider.OtherLegalNames.splice($scope.Provider.OtherLegalNames.indexOf(obj), 1);
                            if ($scope.dataFetchedOLN == true) {
                                obj.HistoryStatus = 'Deleted';
                                $scope.OtherLegalNamesHistory.push(obj);
                            }
                            $('#otherLegalNameWarningModal').modal('hide');
                            $rootScope.operateCancelControl('');
                            myData = data;
                            messageAlertEngine.callAlertMessage("alertOtherLegalNameSuccess", "Other Legal Name Removed successfully.", "success", true);
                        } else {
                            $('#otherLegalNameWarningModal').modal('hide');
                            messageAlertEngine.callAlertMessage("removeOtherLegalName", data.status, "danger", true);
                            $scope.errorOtherLegalName = "Sorry for Inconvenience !!!! Please Try Again Later...";
                        }
                    } catch (e) {
                       
                    }
                },
                error: function (e) {

                }
            });
        }

        $rootScope.$broadcast('RemoveOtherLegalNames', myData);

    };

    //....................Other Legal Name History............................//
    $scope.OtherLegalNamesHistory = [];
    $scope.dataFetchedOLN = false;

    $scope.showOtherLegalNameHistory = function (loadingId) {
        if ($scope.OtherLegalNamesHistory.length == 0) {
            $("#" + loadingId).css('display', 'block');
            var url = rootDir + "/Profile/ProfileHistory/GetAllOtherLegalNamesHistory?profileId=" + profileId;
            $http.get(url).success(function (data) {
                $scope.OtherLegalNamesHistory = data;
                $scope.showOtherLegalNameHistoryTable = true;
                $scope.dataFetchedOLN = true;
                $("#" + loadingId).css('display', 'none');
            });
        }
        else {
            $scope.showOtherLegalNameHistoryTable = true;
        }

    }

    $scope.cancelOtherLegalNameHistory = function () {
        $scope.showOtherLegalNameHistoryTable = false;
    }

    //....................Home Address History............................//
    $scope.HomeAddressesHistory = [];
    $scope.dataFetchedHA = false;

    $scope.showHomeAddressHistory = function (loadingId) {
        if ($scope.HomeAddressesHistory.length == 0) {
            $("#" + loadingId).css('display', 'block');
            var url = rootDir + "/Profile/ProfileHistory/GetAllHomeAddressesHistory?profileId=" + profileId;
            $http.get(url).success(function (data) {
                $scope.HomeAddressesHistory = data;
                $scope.showHomeAddressHistoryTable = true;
                $scope.dataFetchedHA = true;
                $("#" + loadingId).css('display', 'none');
            });
        }
        else {
            $scope.showHomeAddressHistoryTable = true;
        }

    }

    $scope.cancelHomeAddressHistory = function () {
        $scope.showHomeAddressHistoryTable = false;
    }

    //--------------------------- Save Home Address Function ------------------------------
    $scope.saveHomeAddress = function (Form_Div_Id) {
        //================== Save Here ============
        var $form = $("#" + Form_Div_Id).find("form");
        ResetFormForValidation($form);
        if ($form.valid()) {
            $.ajax({
                url: rootDir + '/Profile/Demographic/AddHomeAddressAsync?profileId=' + profileId,
                type: 'POST',
                data: new FormData($form[0]),
                async: false,
                cache: false,
                contentType: false,
                processData: false,
                success: function (data) {
                   
                    try {
                        if (data.status == "true") {
                            if (data.homeAddress.AddressPreferenceType == 1) {
                                for (var i in $scope.HomeAddresses) {
                                    if ($scope.HomeAddresses[i].AddressPreferenceType == 1) {
                                        $scope.HomeAddresses[i].AddressPreferenceType = 2;
                                        $scope.HomeAddresses[i].AddressPreference = "Secondary";
                                        break;
                                    }
                                }
                            }
                            data.homeAddress.LivingEndDate = ConvertDateFormat(data.homeAddress.LivingEndDate);
                            data.homeAddress.LivingFromDate = ConvertDateFormat(data.homeAddress.LivingFromDate);
                            $scope.HomeAddresses.push(data.homeAddress);
                            $rootScope.tempObject = {};
                            $rootScope.visibilityControl = "";
                            FormReset($form);
                            messageAlertEngine.callAlertMessage("alertHomeAddressSuccess", "Home Address saved successfully.", "success", true);
                        } else {
                            messageAlertEngine.callAlertMessage("alertHomeAddressError", data.status.split(","), "danger", true);
                        }
                    } catch (e) {
                       
                    }
                }
            });
        } else {
           
        }
    };
    //--------------------------- Update Home Address Function ------------------------------
    $scope.updateHomeAddress = function (Form_Div_Id, index) {
        var $form = $("#" + Form_Div_Id).find("form");
        ResetFormForValidation($form);
        if ($form.valid()) {
            $.ajax({
                url: rootDir + '/Profile/Demographic/UpdateHomeAddressAsync?profileId=' + profileId,
                type: 'POST',
                data: new FormData($form[0]),
                async: false,
                cache: false,
                contentType: false,
                processData: false,
                success: function (data) {
                    
                    try {
                        if (data.status == "true") {
                            $scope.HomeAddressesPendingRequest = true;
                            data.homeAddress.LivingEndDate = ConvertDateFormat(data.homeAddress.LivingEndDate);
                            data.homeAddress.LivingFromDate = ConvertDateFormat(data.homeAddress.LivingFromDate);
                            if (data.homeAddress.AddressPreferenceType == 1) {
                                for (var i in $scope.HomeAddresses) {
                                    if ($scope.HomeAddresses[i].AddressPreferenceType == 1) {
                                        $scope.HomeAddresses[i].AddressPreferenceType = 2;
                                        $scope.HomeAddresses[i].AddressPreference = "Secondary";
                                        break;
                                    }
                                }
                            }
                            for (var i in $scope.HomeAddresses) {
                                if ($scope.HomeAddresses[i].HomeAddressID == data.homeAddress.HomeAddressID)
                                    $scope.HomeAddresses[i] = data.homeAddress;
                            }
                            if (data.homeAddress.AddressPreferenceType == 1) {
                                $rootScope.visibilityControl = 0 + "_ViewHomeAddress";
                                messageAlertEngine.callAlertMessage("alertHomeAddressSuccess" + 0, "Home Address updated successfully.", "success", true);
                            } else {
                                $rootScope.visibilityControl = index + "_ViewHomeAddress";
                                messageAlertEngine.callAlertMessage("alertHomeAddressSuccess" + index, "Home Address updated successfully.", "success", true);
                            }
                            FormReset($form);

                        } else {
                            messageAlertEngine.callAlertMessage("alertHomeAddressError" + index, data.status.split(","), "danger", true);
                        }
                    } catch (e) {
                       
                    }
                }
            });
        } else {
            
        }
    };

    $scope.initHomeAddressWarning = function (ha) {
        if (angular.isObject(ha)) {
            $scope.tempHomeAddress = ha;
        }
        $('#homeAddressWarningModal').modal();
    };

    $scope.removeHomeAddress = function (HomeAddresses) {
        var validationStatus = false;
        var url = null;
        var $formData = null;
        $scope.isRemoved = true;
        //$scope.isRemoved = true;
        $formData = $('#removeHomeAddress');
        url = rootDir + "/Profile/Demographic/RemoveHomeAddress?profileId=" + profileId;
        ResetFormForValidation($formData);
        validationStatus = $formData.valid();

        if (validationStatus) {
            //Simple POST request example (passing data) :
            $.ajax({
                url: url,
                type: 'POST',
                data: new FormData($formData[0]),
                cache: false,
                contentType: false,
                processData: false,
                success: function (data) {
                    
                    try {
                        if (data.status == "true") {
                            var obj = $filter('filter')($scope.HomeAddresses, { HomeAddressID: data.homeAddress.HomeAddressID })[0];
                            $scope.HomeAddresses.splice($scope.HomeAddresses.indexOf(obj), 1);
                            if ($scope.dataFetchedHA == true) {
                                obj.HistoryStatus = 'Deleted';
                                $scope.HomeAddressesHistory.push(obj);
                            }
                            $scope.isRemoved = false;
                            $('#homeAddressWarningModal').modal('hide');
                            $rootScope.operateCancelControl('');
                            messageAlertEngine.callAlertMessage("alertHomeAddressSuccess", "Home Address Removed successfully.", "success", true);
                        } else {
                            $('#homeAddressWarningModal').modal('hide');
                            messageAlertEngine.callAlertMessage("removeHomeAddress", data.status, "danger", true);
                            $scope.errorHomeAddress = "Sorry for Inconvenience !!!! Please Try Again Later...";
                        }
                    } catch (e) {
                      
                    }
                },
                error: function (e) {

                }
            });
        }
    };

    //--------------------------- Remove Home Address Function ------------------------------
    //$scope.removeHomeAddress = function (index) {
    //    for (var i in $scope.HomeAddresses) {
    //        if (index == i) {
    //            $scope.HomeAddresses.splice(index, 1);
    //            break;
    //        }
    //    }
    //};
    //---------------- Contact Details Return Phone Type ---------------
    $scope.getContactDetailsPhoneByPhoneTypeAndActiveStatus = function (data, phoneTypeEnum, Status) {
        var temp = [];
        for (var i in data) {
            if (data[i].PhoneTypeEnum == phoneTypeEnum && data[i].StatusType == 1) {
                temp.push(data[i]);
            }
        }
        return temp;
    };

    // ---------------------- contact details custom toggle function -----------------------
    $scope.ContactDetailsToggle = function (condition) {
        if (condition == 'EditContactDetails') {
            $rootScope.visibilityControl = condition;
            $scope.getTempContactDetailsForEdit();
        } else {
            $rootScope.visibilityControl = condition;
            $scope.ContactDetailsEmptyError = false;
        }
    };
    //----------------------- Get Temp Data for Edit contact details data ---------------------------
    $scope.getTempContactDetailsForEdit = function () {
        $scope.TempContactDetailsForEdit = angular.copy($scope.Provider.ContactDetails);
        $scope.TempPreferredWrittenContacts = angular.copy($scope.PreferredWrittenContacts);
        $scope.TempPreferredContacts = angular.copy($scope.PreferredContacts);

        if ($scope.TempContactDetailsForEdit == null) {
            $scope.TempContactDetailsForEdit = {
                PhoneDetails: [],
                EmailIDs: [],
                PreferredWrittenContacts: [],
                PreferredContacts: []
            };
        } else {
            if ($scope.TempContactDetailsForEdit.PhoneDetails.length > 0) {
                var filterd = $scope.getInactivatedDataArray($scope.TempContactDetailsForEdit.PhoneDetails);
                $scope.TempContactDetailsForEdit.PhoneDetails = filterd.dataArray;
                $scope.tempSelectedDeactivatedPhonesDetails = filterd.temInactivatedData;
            }
            if ($scope.TempContactDetailsForEdit.EmailIDs.length > 0) {
                var filterd = $scope.getInactivatedDataArray($scope.TempContactDetailsForEdit.EmailIDs);
                $scope.TempContactDetailsForEdit.EmailIDs = filterd.dataArray;
                $scope.tempSelectedDeactivatedEmailIds = filterd.temInactivatedData;
            }
            if ($scope.TempContactDetailsForEdit.PreferredContacts.length > 0) {
                var filterd = $scope.getInactivatedDataArray($scope.TempContactDetailsForEdit.PreferredContacts);
                $scope.TempContactDetailsForEdit.PreferredContacts = filterd.dataArray;
                $scope.tempSelectedDeactivatedPreferredContacts = filterd.temInactivatedData;
            }
            $scope.PreferredContactTypesArray = $scope.TempContactDetailsForEdit.PreferredContacts;
            $scope.getEmailCount();
        }
    };

    // ------------------------------ Method For Get Inactivated Data list for Temp to Edit ---------------------
    $scope.getInactivatedDataArray = function (dataArray) {
        var temInactivatedData = [];

        for (var i = 0; i < dataArray.length; i++) {
            if (dataArray[i].StatusType == 2) {
                temInactivatedData.push(dataArray[i]);
                dataArray.splice(i, 1);
                i--;
            }
        }

        return {
            dataArray: dataArray,
            temInactivatedData: temInactivatedData
        };
    };
    //Method for adding unique contact types to an array
    $scope.PreferredContactTypesArray = [];
    $scope.addPreferredContactTypesToArray = function (contactType) {
        var status = true;
        for (var i in $scope.PreferredContactTypesArray) {
            if ($scope.PreferredContactTypesArray[i].PreferredWrittenContactType == contactType) {
                $scope.PreferredContactTypesArray[i].StatusType = 1;
                status = false;
                break;
            }
        }
        if (status) {
            for (var i in $scope.tempSelectedDeactivatedPreferredContacts) {
                if ($scope.tempSelectedDeactivatedPreferredContacts[i].PreferredWrittenContactType == contactType) {
                    $scope.tempSelectedDeactivatedPreferredContacts[i].StatusType = 1;
                    $scope.PreferredContactTypesArray.push($scope.tempSelectedDeactivatedPreferredContacts[i]);
                    status = false;
                    break;
                }
            }
        }

        if (status) {
            $scope.PreferredContactTypesArray.push($scope.PreferredContacts[contactType - 1]);
        }
    };
    //----------------------------- Remove or deactivate contact details preferred method -----------------------
    $scope.RemovePreferredContactTypesFromArray = function (contactType) {
        for (var i in $scope.PreferredContactTypesArray) {
            if ($scope.PreferredContactTypesArray[i].PreferredWrittenContactType == contactType && $scope.PreferredContactTypesArray[i].PreferredContactID) {
                $scope.PreferredContactTypesArray[i].StatusType = 2;
                break;
            }
            if ($scope.PreferredContactTypesArray[i].PreferredWrittenContactType == contactType && !$scope.PreferredContactTypesArray[i].PreferredContactID) {
                $scope.PreferredContactTypesArray.splice($scope.PreferredContactTypesArray.indexOf($scope.PreferredContactTypesArray[i]), 1);
                break;
            }
        }
    };
    //method to set prefferd contact priority and preffered contact index
    $scope.selectedPrefferdContact = function (pc, index) {
        $scope.pcPriority = pc;
        $scope.pcIndex = index;
    };

    //--------------------------------- Method to change the priority of contact ------------------------------
    $scope.ChangePreferredContactsPriority = function (condition) {
        var index = $scope.PreferredContactTypesArray.indexOf($scope.pcPriority);
        if (condition == "increase" && index !== 0) {
            $scope.PreferredContactTypesArray[index].ProficiencyIndex = index;
            $scope.PreferredContactTypesArray[index - 1].ProficiencyIndex = index + 1;
            $scope.pcIndex = index - 1;
        } else if (condition == "decrease" && index !== $scope.PreferredContactTypesArray.length - 1) {
            $scope.PreferredContactTypesArray[index].ProficiencyIndex = index + 2;
            $scope.PreferredContactTypesArray[index + 1].ProficiencyIndex = index + 1;
            $scope.pcIndex = index + 1;
        }
        $scope.PreferredContactTypesArray.sort(function (a, b) {
            return a.ProficiencyIndex - b.ProficiencyIndex;
        });
    };
    //============================== Contact Details Phone Number, Fax, Mobile Number and Email =================================
    //---------------------------- Add home Phone Number Function ------------------------------------
    $scope.AddPhones = function (obj, condition) {
        obj.push({
            PhoneDetailID: null,
            Number: "",
            CountryCode: "+1",
            PhoneTypeEnum: condition,
            Preference: "Secondary",
            PreferenceType: 2,
            Status: "Active",
            StatusType: 1
        });

        $scope.addPreferredContactTypesToArray(condition);
    };

    //---------------------------- Add Email Ids Function ------------------------------------
    $scope.AddEmail = function (obj) {
        $scope.emailCount = $scope.emailCount + 1;
        obj.push({
            EmailDetailID: null,
            EmailAddress: "",
            Preference: "Secondary",
            PreferenceType: 2,
            Status: "Active",
            StatusType: 1
        });
        $scope.addPreferredContactTypesToArray(4);

    };

    //------------------ Add Pager function ------------------------------------
    $scope.AddPager = function () {
        $scope.TempContactDetailsForEdit.CountryCode = "+1";
        $scope.addPreferredContactTypesToArray(5);
    };
    //-------------------- Remove Pager with complete conditional ---------------------
    $scope.RemovePager = function () {
        $scope.TempContactDetailsForEdit.CountryCode = $scope.putEmpty;
        $scope.TempContactDetailsForEdit.PagerNumber = $scope.putEmpty;
        $scope.RemovePreferredContactTypesFromArray(5);
    };
    //---------------- Remove Method for Contact Details -----------------------
    $scope.RemoveContactDetails = function (index, data) {
        data.splice(index, 1);
    };
    //---------------------------- Preferred Contact change function ------------------------
    $scope.PreferredContactChange = function (obj, status) {
        if (status) {
            obj.StatusType = 1;
        } else {
            obj.StatusType = 2;
        }
    };
    $scope.PreferredContactPriority = function (condition, index, objArray) {
        if (condition == "increase") {
            objArray[index].PreferredIndex = index;
            objArray[index - 1].PreferredIndex = index + 1;
        } else if (condition == "decrease") {
            objArray[index].PreferredIndex = index + 2;
            objArray[index + 1].PreferredIndex = index + 1;
        }
        objArray.sort(function (a, b) {
            return a.PreferredIndex - b.PreferredIndex;
        });
    };
    //------------------------------- Country Code Popover Show by Id ---------------------------------------
    $scope.showContryCodeDiv = function (countryCodeDivId) {
        changeVisibilityOfCountryCode();
        $("#" + countryCodeDivId).show();
    };
    //-------------------------- Contact Details Required Conditions-----------------------------------------
    $scope.ContactDetailsPhoneConditionFunction = function (obj, data, phoneType) {
        if (data.length > 0) {
            for (var i in data) {
                if (data[i].PhoneTypeEnum == phoneType) {
                    if (i == data.indexOf(obj)) {
                        switch (data[i].PreferenceType) {
                            case 1:
                                data[i].PreferenceType = 2;
                                break;
                            case 2:
                                data[i].PreferenceType = 1;
                                break;
                            default:
                                data[i].PreferenceType = 1;
                        }
                    } else {
                        data[i].PreferenceType = 2;
                    }
                }
            }
        }

    };
    $scope.ContactDetailsEmailConditionFunction = function (index, data) {
        if (data.length > 0) {
            for (var i in data) {
                if (i == index) {
                    switch (data[i].PreferenceType) {
                        case 1:
                            data[i].PreferenceType = 2;
                            break;
                        case 2:
                            data[i].PreferenceType = 1;
                            break;
                        default:
                            data[i].PreferenceType = 1;
                    }
                } else {
                    data[i].PreferenceType = 2;
                }
            }
        }

    };
    //--------------------- Modal confirmation for Inactive database Data ----------------------
    $scope.changeStausType = function (status) {
        if (status == 1) {
            status = 2;
        } else {
            status = 1;
        }
        return status;
    };

    $scope.getEmailCount = function () {
        $scope.emailCount = $scope.TempContactDetailsForEdit.EmailIDs.length;
        $scope.kTemp = 0;
    }
    $scope.initKTempOne = function () {
        $scope.kTemp = 1;
    }
    $scope.initKTempZero = function () {
        $scope.kTemp = 0;
    }


    $scope.showConfirmation = function (arryData, obj, condition) {
        if ($scope.kTemp == 1) {
            if (obj.StatusType == 1 && $scope.emailCount >= 1) {
                $scope.emailCount = $scope.emailCount - 1;
            } else if (obj.StatusType == 2) {
                $scope.emailCount = $scope.emailCount + 1;
            }
        }
        obj.StatusType = $scope.changeStausType(obj.StatusType);
        var index = arryData.indexOf(obj);
        arryData[index] = obj;

        if (condition == "phone") {
            if (!obj.PhoneDetailID) {
                $scope.RemoveContactDetails(index, arryData);
            }
            var status = $scope.IsActiveContactDetails(arryData, obj);
            if (status) {
                $scope.addPreferredContactTypesToArray(obj.PhoneTypeEnum);
            } else {
                $scope.RemovePreferredContactTypesFromArray(obj.PhoneTypeEnum);
            }
        } else if (condition == "email") {
            if (!obj.EmailDetailID) {
                $scope.RemoveContactDetails(index, arryData);
            }
            var status = $scope.IsActiveEmailIds(arryData);
            if (status) {
                $scope.addPreferredContactTypesToArray(4);
            } else {
                $scope.RemovePreferredContactTypesFromArray(4);
            }
        }

    };
    //-------------------- Modal Confirmation Hide and Remove from Array ------------------------
    $scope.InactiveContactDetails = function () {
        $('#ConfirmationContactDetails').modal('hide');
    };

    //------------------------------- Active inactive for preferred contact details ---------------------
    $scope.IsActiveContactDetails = function (data, obj) {
        var status = false;
        for (var i in data) {
            if (obj.PhoneTypeEnum == data[i].PhoneTypeEnum && data[i].StatusType == 1) {
                status = true;
                break;
            }
        }
        return status;
    };
    //------------------------------- Get Active Inactive Status For Preferred Contact ------------------
    $scope.IsActiveEmailIds = function (data) {
        var status = false;
        for (var i in data) {
            if (data[i].StatusType == 1) {
                status = true;
                break;
            }
        }
        return status;
    };

    //------------------------- NG Change for duplicate mobile number for provider deactivated own data only --------------------------
    $scope.checkDuplicateData = function (obj, condition) {
        if (condition == "phone" && $scope.tempSelectedDeactivatedPhonesDetails) {
            for (var i = 0; i < $scope.tempSelectedDeactivatedPhonesDetails.length; i++) {
                if (obj.CountryCode == $scope.tempSelectedDeactivatedPhonesDetails[i].CountryCode && obj.Number == $scope.tempSelectedDeactivatedPhonesDetails[i].Number) {
                    obj.PhoneDetailID = $scope.tempSelectedDeactivatedPhonesDetails[i].PhoneDetailID;
                    obj.StatusType = 1;
                }
            }
        } else if (condition == "email") {

            obj.EmailAddress = angular.lowercase(obj.EmailAddress);

            if ($scope.tempSelectedDeactivatedEmailIds) {
                for (var i = 0; i < $scope.tempSelectedDeactivatedEmailIds.length; i++) {
                    if (obj.EmailAddress == $scope.tempSelectedDeactivatedEmailIds[i].EmailAddress) {
                        obj.EmailDetailID = $scope.tempSelectedDeactivatedEmailIds[i].EmailDetailID;
                        obj.StatusType = 1;
                    }
                }

            }
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

    //--------------------------------- Save Contact Details Function -----------------------------------
    $scope.saveContactDetails = function (Form_Id) {
        ResetFormForValidation($("#" + Form_Id));
        var status = $scope.IsContactDetailsFormValidate();
        if ($("#" + Form_Id).valid() && status) {
            $.ajax({
                url: rootDir + '/Profile/Demographic/UpdateContactDetailsAsync?profileId=' + profileId,
                type: 'POST',
                data: new FormData($("#" + Form_Id)[0]),
                async: false,
                success: function (data) {
                   
                    try {
                        if (data.status == "true") {
                            $scope.ContactDetailPendingRequest = true;
                            if (data.contactDetail) {
                                for (var i in data.contactDetail.PreferredContacts) {
                                    if (data.contactDetail.PreferredContacts[i].PreferredWrittenContactType == 1) {
                                        data.contactDetail.PreferredContacts[i].ContactType = "Home Phone"
                                    }
                                }
                            }
                            $scope.Provider.ContactDetails = data.contactDetail;
                            $scope.TempContactDetailsForEdit = [];
                            $scope.PreferredContactTypesArray = [];
                            $scope.ContactDetailsEmptyError = false;
                            FormReset($("#" + Form_Id));
                            $rootScope.visibilityControl = "";
                            messageAlertEngine.callAlertMessage("alertContactDetailsSuccess", "Contact Details updated successfully.", "success", true);
                        } else {
                            messageAlertEngine.callAlertMessage("alertContactDetailsError", data.status.split(","), "danger", true);
                        }
                    } catch (e) {
                       
                    }
                },
                cache: false,
                contentType: false,
                processData: false
            });
        } else {
            
            $scope.ContactDetailsEmptyError = true;
        }
    };
    //-------------- ContactDetail Validation Status  --------------------------
    $scope.IsContactDetailsFormValidate = function () {
        var status = false;
        if ($scope.TempContactDetailsForEdit.PhoneDetails.length > 0 || $scope.TempContactDetailsForEdit.EmailIDs.length > 0 || $scope.PreferredContactTypesArray.length > 0) {
            status = true;
        }
        return status;
    };
   
    $scope.$watch('tempObject.DLState', function (newV,oldV) {
        if (newV == oldV) {
            return;
        }
        if (newV != "") {
            $rootScope.dlinfoerror = false;
        }
        else {
            $rootScope.dlinfoerror = true;
        }
    })

    //========================== Personal Identification Save Method ======================
    $scope.DemographicsPersonalIdentificationSave = function (Form_Id) {
        var validCount = 0;
        var url;
        var $formData;
        
        $rootScope.dlinfoerror = false;
        $rootScope.dlinfoerror1 = false;
        $formData = $('#DemographicsPersonalIdentificationForm');
        
        //ResetForm($formdata);
        var validateDL = $($formData[0]).find($("[name='DL']")).val();
        var validateState = $($formData[0]).find($("[name='DLState']")).val();
        //var DLforIssueState = $scope.validateAssignedTo(validateDL);
        if ((validateDL != null || validateDL != "") && (validateState == null || validateState == "")) {
            $rootScope.dlinfoerror = true;
            validCount++;
        }        
        if ((validateDL == null || validateDL == "") && (validateState != null || validateState != "")) {
            $rootScope.dlinfoerror1 = true;
            validCount++;   
        }
        if ((validateDL == null || validateDL == "") && (validateState == null || validateState == "")) {
            $rootScope.dlinfoerror = false;
            $rootScope.dlinfoerror1 = false;
            validCount = 0;
        }           
        ResetFormForValidation($("#" + Form_Id));
        var FormIsValid = true;
        var myData = {};
        if ($("#" + Form_Id).valid() && validCount == 0) {
            $.ajax({
                url: rootDir + '/Profile/Demographic/UpdatePersonalIdentificationAsync?profileId=' + profileId,
                type: 'POST',
                data: new FormData($("#" + Form_Id)[0]),
                async: false,
                cache: false,
                contentType: false,
                processData: false,
                success: function (data) {
                  
                    try {
                        if (data.status == "true" ) {
                            $rootScope.dlinfoerror = false;
                            $rootScope.dlinfoerror1 = false;
                            $scope.PersonalIdentificationPendingRequest = true;
                            $scope.Provider.PersonalIdentification = data.personalIdentification;
                            $rootScope.tempObject = {};
                            $rootScope.visibilityControl = "";
                            FormReset($("#" + Form_Id));
                            myData = data;
                            messageAlertEngine.callAlertMessage("alertPersonalIdentificationSuccess", "Personal Identification updated successfully.", "success", true);
                        } else {
                            $rootScope.dlinfoerror = false;
                            $rootScope.dlinfoerror1 = false;
                            ResetForm($formData);
                            messageAlertEngine.callAlertMessage("alertPersonalIdentificationError", data.status.split(","), "danger", true);
                        }
                    } catch (e) {
                        $rootScope.dlinfoerror = false;
                        $rootScope.dlinfoerror1 = false;
                    }
                }
            });

        } else {
            
        }

        $rootScope.$broadcast('UpdatePersonalIdentification', myData);
    };
    //--------------------------- end ------------------------------
    //-------------------------------- Save Birth Information Function --------------------------------

    $scope.ConvertDateFormat11 = function (value) {
        var dttm = value.split(" ");
        var dt = null;
        if (dttm[0].indexOf('-') > -1) {
            dt = dttm[0].split('-');
        }
        if (dttm[0].indexOf('/') > -1) {
            dt = dttm[0].split('/');
        }
        var value = dt[1] + '-' + dt[0] + '-' + dt[2];
        var today = new Date(value);
        var dd = today.getDate();
        var mm = today.getMonth() + 1;
        var yyyy = today.getFullYear();
        //if (dd < 10) { dd = '0' + dd }
        //if (mm < 10) { mm = '0' + mm }
        var today = mm + '/' + dd + '/' + yyyy;
        return today;
    };

    $scope.first = 0;
    $scope.changeDOBFormat = function (DOB) {
        if ($scope.first == 0) {
            $scope.first++;
            return $scope.ConvertDateFormat11(DOB);
        }
        else
            return DOB;
    };

    $scope.saveBirthInformation = function (Form_Id) {
        ResetFormForValidation($("#" + Form_Id));
        var myData = {};
        if ($("#" + Form_Id).valid()) {
            $.ajax({
                url: rootDir + '/Profile/Demographic/UpdateBirthInformationAsync?profileId=' + profileId,
                type: 'POST',
                data: new FormData($("#" + Form_Id)[0]),
                async: false,
                cache: false,
                contentType: false,
                processData: false,
                success: function (data) {
                   
                    try {
                        if (data.status == "true") {
                            $scope.BirthInformationPendingRequest = true;
                            data.birthInformation.DateOfBirth = data.birthInformation.DateOfBirth.split(" ")[0];
                            //data.birthInformation.DateOfBirth = $scope.ConvertDateFormat11(data.birthInformation.DateOfBirth);

                            $scope.Provider.BirthInformation = data.birthInformation;
                            //alert(data.birthInformation.DateOfBirth);
                            $rootScope.tempObject = {};
                            $rootScope.visibilityControl = "";
                            myData = data;

                            FormReset($("#" + Form_Id));
                            messageAlertEngine.callAlertMessage("alertBirthInformationSuccess", "Birth Information saved successfully.", "success", true);
                        } else {
                            messageAlertEngine.callAlertMessage("alertBirthInformationError", data.status.split(","), "danger", true);
                        }
                    } catch (e) {
                      
                    }
                }
            });
        } else {
           
        }

        $rootScope.$broadcast('UpdateBirthInformation', myData);

    };
    //--------------------------- Save Visa Details Function ------------------------------------
    $scope.saveEthnicityVisaDetails = function (Form_Id) {
        ResetFormForValidation($("#" + Form_Id));
        var myData = {};
        if ($("#" + Form_Id).valid()) {
            $.ajax({
                url: rootDir + '/Profile/Demographic/UpdateVisaDetailAsync?profileId=' + profileId,
                type: 'POST',
                data: new FormData($("#" + Form_Id)[0]),
                async: false,
                success: function (data) {
                  
                    try {
                        if (data.status == "true") {
                            $scope.VisaDetailPendingRequest = true;
                            if (data.visaDetail.VisaInfo) {
                                data.visaDetail.VisaInfo.VisaExpirationDate = ConvertDateFormat(data.visaDetail.VisaInfo.VisaExpirationDate);
                                for (var i in $scope.VisaTypes) {
                                    if (data.visaDetail.VisaInfo.VisaTypeID == $scope.VisaTypes[i].VisaTypeID) {
                                        data.visaDetail.VisaInfo.VisaType = $scope.VisaTypes[i];
                                        break;
                                    }
                                }
                                for (var i in $scope.VisaStatuses) {
                                    if (data.visaDetail.VisaInfo.VisaStatusID == $scope.VisaStatuses[i].VisaStatusID) {
                                        data.visaDetail.VisaInfo.VisaStatus = $scope.VisaStatuses[i];
                                        break;
                                    }
                                }
                                if (!data.visaDetail.VisaInfo.VisaTypeID) {
                                    data.visaDetail.VisaInfo.VisaTypeID = "";
                                }
                                if (!data.visaDetail.VisaInfo.VisaStatusID) {
                                    data.visaDetail.VisaInfo.VisaStatusID = "";
                                }
                                if (!data.visaDetail.VisaInfo.CountryOfIssue) {
                                    data.visaDetail.VisaInfo.CountryOfIssue = "";
                                }
                            }
                            $scope.Provider.VisaDetails = data.visaDetail;
                            $rootScope.tempObject = {};
                            $rootScope.visibilityControl = "";
                            myData = data;
                            messageAlertEngine.callAlertMessage("alertVisaDetailsSuccess", "Visa Details updated successfully.", "success", true);
                            FormReset($("#" + Form_Id));
                        } else {
                            messageAlertEngine.callAlertMessage("alertVisaDetailsError", data.status.split(","), "danger", true);
                        }
                    } catch (e) {
                      
                    }
                },
                cache: false,
                contentType: false,
                processData: false
            });
        } else {
           
        }

        $rootScope.$broadcast('UpdateVisaInfo', myData);

    };

    //------------------------- Language know search select action function -----------------------
    $scope.TempLanguageForEdit = [];

    $scope.SelectLanguageKnown = function (lang, divId) {
        $scope.TempLanguageForEdit.KnownLanguages.push({
            KnownLanguageID: null,
            Language: lang.name,
            ProficiencyIndex: $scope.TempLanguageForEdit.length
        });
        $scope.tempLanguages.splice($scope.tempLanguages.indexOf(lang), 1);
        $("#" + divId).hide();
        $scope.searchLang = '';
        $("#searchLanguageKnow").val("");
    };
    $scope.UnselectLanguage = function (lang) {
        for (i in $scope.TempLanguageForEdit.KnownLanguages) {
            if ($scope.TempLanguageForEdit.KnownLanguages[i] == lang) {
                $scope.TempLanguageForEdit.KnownLanguages.splice($scope.TempLanguageForEdit.KnownLanguages.indexOf(lang), 1);
            }
        }
        for (var i in $scope.Languages) {
            if ($scope.Languages[i].name == lang.Language) {
                $scope.tempLanguages.push($scope.Languages[i]);
            }
        }
    };

    $scope.selectedLanguage = function (lang, index) {
        $scope.langForPriority = lang;
        $scope.selectedIndex = index;
    };

    $scope.PriorityChange = function (condition) {
        var index = $scope.TempLanguageForEdit.KnownLanguages.indexOf($scope.langForPriority);
        if (condition == "increase") {
            $scope.TempLanguageForEdit.KnownLanguages[index].ProficiencyIndex = index;
            $scope.TempLanguageForEdit.KnownLanguages[index - 1].ProficiencyIndex = index + 1;
            $scope.selectedIndex = index - 1;
        } else if (condition == "decrease") {
            $scope.TempLanguageForEdit.KnownLanguages[index].ProficiencyIndex = index + 2;
            $scope.TempLanguageForEdit.KnownLanguages[index + 1].ProficiencyIndex = index + 1;
            $scope.selectedIndex = index + 1;
        }
        $scope.TempLanguageForEdit.KnownLanguages.sort(function (a, b) {
            return a.ProficiencyIndex - b.ProficiencyIndex;
        });
    };

    //-------------- reusable for array generate ------------------
    $scope.getArray = function (number) {
        var temp = [];
        for (var i = 0; i < number; i++) {
            temp.push(i);
        }
        return temp;
    };

    $scope.shoLanguageList = function (divId) {
        $("#" + divId).show();
    };

    $scope.IsExistInArray = function (collection, key) {
        var keys = [];
        status = true;
        angular.forEach(collection, function (item) {
            if (keys.indexOf(key) === -1) {
                keys.push(key);
            } else {
                status = false;
            }
        });
        return status;
    };

    $scope.ViewLanguageKnown = true;

    $scope.LanguageKnownToggle = function (condition) {
        if (condition == 'EditKnowLanguage') {
            $rootScope.visibilityControl = condition;
            $scope.getLanguagesAndTempData();
        } else {
            $rootScope.visibilityControl = condition;
        }
    };
    $scope.getLanguagesAndTempData = function () {
        $scope.TempLanguageForEdit = angular.copy($scope.Provider.LanguageInfo);
        $scope.tempLanguages = angular.copy($scope.Languages);

        if ($scope.TempLanguageForEdit) { 
            for (var i in $scope.TempLanguageForEdit.KnownLanguages) {
                for (var j in $scope.tempLanguages) {
                    if ($scope.TempLanguageForEdit.KnownLanguages[i].Language == $scope.tempLanguages[j].name) {
                        $scope.tempLanguages.splice(j, 1);
                    }
                }
            }
            $scope.TempLanguageForEdit.KnownLanguages.sort(function (a, b) {
                return a.ProficiencyIndex - b.ProficiencyIndex;
            });
        } else {
            $scope.TempLanguageForEdit = {
                KnownLanguages: [],
            };
        }

        $scope.langForPriority = null;
        $scope.selectedIndex = {};
    };

    //------------------------------------------------------------------------------------------
    $scope.saveLanguages = function (Form_Id) {
        ResetFormForValidation($("#" + Form_Id));
        if ($("#" + Form_Id).valid()) {
            $.ajax({
                url: rootDir + "/Profile/Demographic/UpdateLanguagesAsync?profileId=" + profileId,
                type: 'POST',
                data: new FormData($("#" + Form_Id)[0]),
                async: false,
                cache: false,
                contentType: false,
                processData: false,
                success: function (data) {
                    
                    try {
                        if (data.status == "true") {
                            $scope.LanguageInfoPendingRequest = true;
                            $scope.Provider.LanguageInfo = data.languageInfo;
                            $scope.TempLanguageForEdit = [];
                            FormReset($("#" + Form_Id));
                            $rootScope.visibilityControl = "";
                            messageAlertEngine.callAlertMessage("alertLanguageKnownSuccess", "Languages Know updated successfully.", "success", true);
                        } else {
                            messageAlertEngine.callAlertMessage("alertLanguageKnownError", data.status.split(","), "danger", true);
                        }
                    } catch (e) {
                       
                    }
                }
            });
        } else {
           
        }
    };
    $rootScope.DemographicsLoaded = true;
    $scope.dataLoaded = false;
    $(function () {
        if (!$scope.dataLoaded) {
            $rootScope.DemographicsLoaded = false;
            var data = JSON.parse(demographics);
            try {

                for (key in data) {
                   
                    $rootScope.$emit(key, data[key]);
                    //call respective controller to load data (PSP)
                }
                $rootScope.DemographicsLoaded = true;

            } catch (e) {
               
                $rootScope.DemographicsLoaded = true;
            }
            $rootScope.$broadcast("LoadRequireMasterDataDemographics");
            $rootScope.$broadcast("PracticeLocation");
            $scope.dataLoaded = true;
        }
    });
}]);
