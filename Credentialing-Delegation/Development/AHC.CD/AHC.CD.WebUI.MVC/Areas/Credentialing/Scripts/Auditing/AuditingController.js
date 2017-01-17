//------------------------- document ready state ------------------

$(document).ready(function () {
    $("#ForType").hide();
    $("#ForIPA").hide();
    $("#ForSpecialty").hide();
    $("body").tooltip({ selector: '[data-toggle=tooltip]' });

    $('#SearchProviderResultPanel3').slideUp();

});

//----------------------- ng app -------------------------
var initCredApp = angular.module('InitCredApp', ['ui.filters', 'mgcrea.ngStrap', "wysiwyg.module", 'colorpicker.module', 'ngTable']);
initCredApp.directive('searchdropdown', function () {
    return {
        restrict: 'AE',
        link: function (scope, element, attr) {
            element.bind('focus', function () {
                element.parent().find(".TemplateSelectAutoList").show();
            });
        }
    };
});

initCredApp.service('messageAlertEngine', ['$rootScope', '$timeout', function ($rootScope, $timeout) {

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

initCredApp.config(function ($datepickerProvider) {
    var nowDate = new Date();
    var today = new Date(nowDate.getFullYear(), nowDate.getMonth(), nowDate.getDate(), 0, 0, 0, 0);
    angular.extend($datepickerProvider.defaults, {
        //startDate: 'today',
        minDate: today,
        autoclose: true,
        useNative: true
    });
})
initCredApp.filter('formatSize', function () {
    return function (docSize) {
        var documentSize = "0 KB";

        if (docSize < 1048576) {
            documentSize = Math.round((docSize) / (1024)) + " KB";
        }
        else {
            documentSize = Math.round((docSize) / (1048576)) + " MB";
        }

        return documentSize;
    }
});

initCredApp.controller('singleProviderCtrl', ['$scope', '$http', '$timeout', '$filter', 'ngTableParams', 'messageAlertEngine', function ($scope, $http, $timeout, $filter, ngTableParams, messageAlertEngine) {
    $scope.EditCancle = function (temp) {
        //ResetFormForValidation($("#newEmailForm"));
        $scope.errmsgforTo = false;
        $scope.errmsgforCC = false;
        $scope.errmsgforBCC = false;
        $scope.fileList = [];
        var formdata = $('#newEmailForm');
        formdata[0].reset();
        if ($scope.data != null && temp != null) {
            for (var c = 0; c < $scope.data.length; c++) {
                if ($scope.data[c].EmailTemplateID == temp.EmailTemplateID) {
                    $scope.data[c].EditStatus = false;
                    //$scope.tableParams.reload();
                    break;
                }
            }
        }
        $scope.compose = false;
        $scope.tempObject.To = "";
        $scope.tempObject.CC = "";
        $scope.tempObject.BCC = "";
        $scope.tempObject.Subject = "";
        $scope.tempObject.Body = "";
        $scope.tempObject.IsRecurrenceEnabledYesNoOption = "";
        $scope.tempObject.RecurrenceIntervalTypeCategory = "";
        $scope.tempObject.FromDate = "";
        $scope.tempObject.ToDate = "";
        $scope.tempObject.IntervalFactor = "";
        $("#newEmailForm .field-validation-error").hide();
        $scope.tempObject.SaveAsTemplateYesNoOption = 2;
    }

    $scope.FilesizeError = false;

    var QID = 0;
    var index1 = -1;
    $scope.addingDocument = function () {
        $('#file').click();
    }
    //----------------------- filter data ---------------------
    $scope.filterData = {
        planId: 0,
        BusinessEntityId: 0
    };
    $http.get(rootDir + '/EmailService/GetAllEmailTemplates').
       success(function (data, status, headers, config) {

           try {
               if (data != null) {
                   $scope.templates = data;
               }
               for (var i = 0; i < $scope.templates.length; i++) {
                   $scope.templates[i].LastModifiedDate = $scope.ConvertDateFormat($scope.templates[i].LastModifiedDate);
               }
           } catch (e) {

           }

       }).
       error(function (data, status, headers, config) {
       });
    $scope.selectTemplate = function (type) {

        $('#template').val(type.Title);
        $scope.tempObject.Subject = type.Subject;
        $scope.tempObject.Body = type.Body;
        $scope.showContent();

    }
    $scope.hideDiv = function () {
        $(".ProviderTypeSelectAutoList").hide();
        $scope.errorMsg = false;
    }
    $scope.searchCumDropDown = function (divId) {
        $(".ProviderTypeSelectAutoList").hide();
        $("#" + divId).show();
    };
    $(document).click(function (event) {
        if (!$(event.target).hasClass("form-control") && $(event.target).parents(".ProviderTypeSelectAutoList").length === 0) {
            $(".ProviderTypeSelectAutoList").hide();
        }
    });
    function showTemplateList(ele) {
        $(ele).parent().find(".ProviderTypeSelectAutoList").first().show();
    }

    //--------------------- init required data -----------------------
    $scope.DataSubmited = false;
    $scope.AuditDocSubmited = false;
    $scope.PSVDocSubmited = false;
    $scope.DocSubmited = false;
    $scope.compose = false;

    $("#ForType").hide();
    $("#ForIPA").hide();
    $("#ForSpecialty").hide();

    var CredentialingInitiationViewModel = {};
    $scope.TwoDocuments = false;
    $scope.OneDocuments = false;

    $scope.ConvertDateFormat = function (value) {
        var today = new Date(value);
        var dd = today.getDate();
        var mm = today.getMonth() + 1;
        var yyyy = today.getFullYear();
        if (dd < 10) { dd = '0' + dd }
        if (mm < 10) { mm = '0' + mm }
        var today = mm + '-' + dd + '-' + yyyy;
        return today;
    };

    //====================================Package Generation start===========================================

    $scope.templateSelected = true;
    $scope.changeYesNoOption = function (value) {
        $("#newEmailForm .field-validation-error").remove();
        $scope.templateSelected = false;
        $scope.tempObject.Subject = "";
        $scope.tempObject.Body = "";
        if (value == 2) {
            $scope.templateSelected = true;
        } else {
            $scope.tempObject.Title = "";
        }
    }

    $scope.showContent = function () {
        $scope.templateSelected = true;
    }

    $scope.data = []; //data in scope is declared
    $scope.progressbar = false;
    $scope.error_message = "";
    $scope.groupBySelected = "none";
    $scope.showInit = false;
    $scope.result = false;
    $scope.isSelected = true;
    $scope.isFound = true;

    $scope.resetSelection = function (data) {
        var temp = [];
        for (var i in data) {
            temp[i] = false;
        }
        return temp;
    };

    $scope.squash = function (arr) {
        var tmp = [];
        for (var i = 0; i < arr.length; i++) {
            if (tmp.indexOf(arr[i]) == -1) {
                tmp.push(arr[i]);
            }
        }
        return tmp;
    }

    //-------------- selection ---------------

    $scope.SearchProviderPanelToggle = function (divId) {
        $('div').tooltip.title = "show";
        $("#" + divId).slideToggle();
        $scope.initiate = false;

    };

    $http.get(rootDir + '/Profile/MasterData/GetAllProviderTypes').
      success(function (data, status, headers, config) {

          $scope.masterProviderTypes = data;

      }).
      error(function (data, status, headers, config) {

      });



    $http.get(rootDir + '/Profile/MasterData/GetAllSpecialities').
     success(function (data, status, headers, config) {

         $scope.masterSpecialities = data;

     }).
     error(function (data, status, headers, config) {

     });


    //==============Get All Form Data form Back-end=========================//

    $scope.pdfTemplateList = [];
    $scope.NewpdfTemplateList = [];


    $http.get(rootDir + '/Credentialing/PDFMapping/GetAllPlanFormData').success(function (data) {
        try {
            if (data != null) {

                var NewFormData = [];

                $scope.NewData = data;
                console.log("FormData");
                console.log($scope.NewData);
                for (var i = 0; i < $scope.NewData.length; i++) {
                    var temp = [];
                    if ($scope.NewData[i].PlanFormPayer != null) {
                        temp.Payer = $scope.NewData[i].PlanFormPayer.Payer;
                    }
                    if ($scope.NewData[i].PlanFormRegion != null) {
                        temp.Region = $scope.NewData[i].PlanFormRegion.Region;
                    }
                    temp.Name = $scope.NewData[i].PlanFormName;
                    temp.FileName = $scope.NewData[i].PlanFormFileName;


                    temp.IsCkecked = false;
                    temp.IsLoading = false;
                    NewFormData.push(temp);
                }

                $scope.tempPdfFileList = NewFormData;
                $scope.pdfTemplateList = NewFormData;
                $scope.NewpdfTemplateList = $scope.pdfTemplateList;
            }
        } catch (e) {

        }
    }).error(function () {
    })

    $scope.searchCumDropDown = function (divId) {
        $('.ProviderTypeSelectAutoList1').hide();
        $("#" + divId).show();


    };
    $scope.tempObject = {};
    //Bind the IPA name with model class to achieve search cum drop down
    $scope.addIntoIPADropDown = function (ipa, div) {

        // $scope.tempObject.IPAGroupNameDup = ipa;
        $scope.tempObject.IPAGroupName = ipa;

        $("#" + div).hide();
    }

    //Bind the IPA name with model class to achieve search cum drop down
    $scope.addIntoSpecialtyDropDown = function (s, div) {

        //$scope.tempObject.SpecialtyDup = s;
        $scope.tempObject.Specialty = s;

        $("#" + div).hide();
    }

    //Bind the IPA name with model class to achieve search cum drop down
    $scope.addIntoTypeDropDown = function (type, div) {
        $scope.tempObject.ProviderType = type.Title;
        //$scope.tempObject.ProviderTypeDup = type.Title;
        $("#" + div).hide();
    }

    $scope.addIntoPlanDropDown = function (type, tempObject, div) {
        tempObject.PlanId = type.PlanID;
        tempObject.PlanName = type.PlanName;

        //$scope.tempObject.ProviderTypeDup = type.Title;
        $("#" + div).hide();
    }

    //============================= Data From Master Data Table  ======================    
    //----------------------------- Get List Of Groups --------------------------    
    $http.get(rootDir + '/MasterData/Organization/GetGroups').
      success(function (data, status, headers, config) {
          $scope.PracticingGroups = data;
      }).
      error(function (data, status, headers, config) {

      });

    $scope.init_table = function (data, selectedtab) {

        $scope.data = data;
        var counts = [];

        if ($scope.data.length <= 10) {
            counts = [];
        }
        else if ($scope.data.length <= 25) {
            counts = [10, 25];
        }
        else if ($scope.data.length <= 50) {
            counts = [10, 25, 50];
        }
        else if ($scope.data.length <= 100) {
            counts = [10, 25, 50, 100];
        }
        else if ($scope.data.length > 100) {
            counts = [10, 25, 50, 100];
        }
        if (selectedtab == "packagetracker") {
            $scope.tableParams2 = new ngTableParams({
                page: 1,            // show first page
                count: 10,          // count per page
                filter: {
                    //name: 'M'       // initial filter
                    //FirstName : ''
                },
                sorting: {
                    //name: 'asc'     // initial sorting
                }
            }, {
                counts: counts,
                total: $scope.data.length, // length of data
                getData: function ($defer, params) {
                    // use build-in angular filter
                    var filteredData = params.filter() ?
                            $filter('filter')($scope.data, params.filter()) :
                            $scope.data;
                    var orderedData = params.sorting() ?
                            $filter('orderBy')(filteredData, params.orderBy()) :
                            $scope.data;

                    params.total(orderedData.length); // set total for recalc pagination
                    $defer.resolve(orderedData.slice((params.page() - 1) * params.count(), params.page() * params.count()));
                }
            });
        }
        else {
            $scope.tableParams1 = new ngTableParams({
                page: 1,            // show first page
                count: 10,          // count per page
                filter: {
                    //name: 'M'       // initial filter
                    //FirstName : ''
                },
                sorting: {
                    //name: 'asc'     // initial sorting
                }
            }, {
                counts: counts,
                total: $scope.data.length, // length of data
                getData: function ($defer, params) {
                    // use build-in angular filter
                    var filteredData = params.filter() ?
                            $filter('filter')($scope.data, params.filter()) :
                            $scope.data;
                    var orderedData = params.sorting() ?
                            $filter('orderBy')(filteredData, params.orderBy()) :
                            $scope.data;

                    params.total(orderedData.length); // set total for recalc pagination
                    $defer.resolve(orderedData.slice((params.page() - 1) * params.count(), params.page() * params.count()));
                }
            });
        }

    };

    //if filter is on
    $scope.ifFilter = function () {
        try {
            var bar;
            var obj;
            if ($scope.selectedAction == 1) {
                obj = $scope.tableParams1.$params.filter;
            }
            else if ($scope.selectedAction == 2 || $scope.selectedAction == 3) {
                obj = $scope.tableParams2.$params.filter;
            }
            for (bar in obj) {
                if (obj[bar] != "") {
                    return false;
                }
            }
            return true;
        }
        catch (e) { return true; }

    }

    //Get index first in table
    $scope.getIndexFirst = function () {
        try {
            if ($scope.groupBySelected == 'none') {
                if ($scope.selectedAction == 1) {
                    return ($scope.tableParams1.$params.page * $scope.tableParams1.$params.count) - ($scope.tableParams1.$params.count - 1);
                }
                else if ($scope.selectedAction == 2 || $scope.selectedAction == 3) {
                    return ($scope.tableParams2.$params.page * $scope.tableParams2.$params.count) - ($scope.tableParams2.$params.count - 1);
                }
            }
        }
        catch (e) { }
    }
    //Get index Last in table
    $scope.getIndexLast = function () {
        try {
            if ($scope.groupBySelected == 'none') {
                if ($scope.selectedAction == 1) {
                    return { true: ($scope.data.length), false: ($scope.tableParams1.$params.page * $scope.tableParams1.$params.count) }[(($scope.tableParams1.$params.page * $scope.tableParams1.$params.count) > ($scope.data.length))];
                }
                else if ($scope.selectedAction == 2 || $scope.selectedAction == 3) {
                    return { true: ($scope.data.length), false: ($scope.tableParams2.$params.page * $scope.tableParams2.$params.count) }[(($scope.tableParams2.$params.page * $scope.tableParams2.$params.count) > ($scope.data.length))];
                }
            }
        }
        catch (e) { }
    }
    $scope.data = [];
    $scope.allProviders = [];
    $scope.genPackageType = null;

    $scope.GenerateEmailPopUp = function () {
        $("#newEmailForm .text-danger").hide();
        $scope.errmsgforTo = false;
        $scope.errmsgforCC = false;
        $scope.errmsgforBCC = false;
        //GetTemplates();
        var formdata = $('#newEmailForm');
        formdata[0].reset();
        $scope.templateSelected = true;
        $scope.tempObject.SaveAsTemplateYesNoOption = 2;
    }
    $("#composeMail").on('shown.bs.modal', function () {
        $('body').addClass('modal-open');
    });
    $scope.closeCompose = function () {
        $scope.compose = false;
    }


    //Get data on basis of the parameters ajax call
    $scope.new_search = function (id) {
        $scope.data = [];
        $scope.error_message = "";
        $scope.loadingAjax = true;

        $http({
            method: "POST",
            url: rootDir + "/Credentialing/Initiation/SearchProviderJson?id=" + 1,
            data: {
                NPINumber: $scope.tempObject.NPINumber,
                FirstName: $scope.tempObject.FirstName,
                LastName: $scope.tempObject.LastName,
                CAQH: $scope.tempObject.CAQH,
                IPAGroupName: $scope.tempObject.IPAGroupName,
                Specialty: $scope.tempObject.Specialty,
                ProviderType: $scope.tempObject.ProviderType
            }
        }).success(function (resultData) {

            try {
                if (resultData.length != 0) {
                    //$scope.SearchProviderPanelToggleDown('SearchProviderResultPanel');
                    $scope.result = true;
                    $('#AuditSearch').slideUp();
                    $scope.data = angular.copy(resultData);

                    for (var i = 0; i < $scope.data.length; i++) {
                        if ($scope.data[i].Titles.length == 0) {
                            $scope.data[i].Title = '';
                        }
                        else {
                            var titles = $scope.data[i].Titles[0];
                            for (var j = 1; j < $scope.data[i].Titles.length; j++) {
                                titles = titles + ', ' + $scope.data[i].Titles[j];
                            }
                            $scope.data[i].Title = titles;
                        }

                        if ($scope.data[i].Specialties.length == 0) {
                            $scope.data[i].Specialty = '';
                        } else {
                            var Specialties = $scope.data[i].Specialties[0];
                            for (var j = 1; j < $scope.data[i].Specialties.length; j++) {
                                Specialties = Specialties + ', ' + $scope.data[i].Specialties[j];
                            }
                            $scope.data[i].Specialty = Specialties;
                        }
                    }
                    $scope.allProviders = angular.copy($scope.data);
                    $scope.credInfo = resultData;
                    $scope.init_table($scope.allProviders);
                    $scope.tempObject = "";
                    $scope.loadingAjax = false;

                    for (var i = 0; i < $scope.allProviders.length ; i++) {
                        //$scope.allProviders[i].CredDate = ConvertDateFormat($scope.allProviders[i].CredDate).toDateString();
                        $scope.allProviders[i].IsCkecked = false;
                        $scope.data[i].IsCkecked = false;
                    }
                    // $("#providerSearchForPackageGeneration").slideToggle();
                    // $("#SearchProviderResultPanel1").slideToggle();

                    $scope.SearchProviderTable.reload();
                    $('#SearchProviderResultPanel').slideDown();
                }
                else {
                    $scope.loadingAjax = false;
                    messageAlertEngine.callAlertMessage('noProviderDetails', "No Record Available for the Given Option", "danger", true);
                    $scope.data = "";
                }
            } catch (e) {

            }
        }).error(function () { $scope.loadingAjax = false; $scope.error_message = "An Error occured !! Please Try Again !!"; })
    }


    $scope.goToNextTab = function (tab) {
        $('.nav-tabs a[href="#' + tab + '"]').tab('show');
    };

    $scope.$watchCollection('selectedProviders', function (newValue, oldValue) {
        if (newValue.length > 0) {
            if (newValue[0].IsCkecked == false) {

                for (var i = 0; i < $scope.pdfTemplateList.length; i++) {

                    $scope.pdfTemplateList[i].IsCkecked = false;
                }

            }
            for (var i = 0; i < $scope.pdfTemplateList.length; i++) {
                $scope.pdfTemplateList[i].IsCkecked = false;
            }

            for (var i = 0; i < $scope.spaDoc.length; i++) {

                $scope.spaDoc[i].Status = false;
            }
            $scope.selectedForms = [];
            $scope.selectedDocument = [];
            $scope.generationType = false;
            $scope.showFormDiv = false;
        }

    });

    //remove providers

    $scope.removeProvider = function (index) {

        var deletedProvider = $scope.selectedProviders[index];
        $scope.selectedProviders.splice(index, 1);

        for (var i = 0; i < $scope.allProviders.length; i++) {
            if (deletedProvider.ProfileID == $scope.allProviders[i].ProfileID) {
                $scope.allProviders[i].IsCkecked = false;
                break;
            }
        }

        if ($scope.selectedProviders.length == 0) {
            $scope.goToNextTab('ProviderTab');
            $scope.selectedProviders = [];
        }

    }

    $scope.SearchProviderPanelToggleDown = function (divId) {
        $(".closePanel").slideUp();
        $("#" + divId).slideDown();
    };

    $http.get(rootDir + '/MasterDataNew/GetAllPlans').
                 success(function (data, status, headers, config) {

                     $scope.plans = data;

                 }).
                 error(function (data, status, headers, config) {
                 });
    $scope.dataLoaded = false;
    $scope.loadingBar = false;
    $scope.getData = function (profileID) {

        //var profileID = $scope.selectedProviders[0].ProfileID
        $scope.loadingBar = true;


        $scope.DocumentLoaded = false;
        $http({
            method: 'GET',
            url: rootDir + '/Credentialing/DocChecklist/GetDocumentProfileDataAsync?profileId=' + profileID
        }).success(function (data, status, headers, config) {
            $scope.spaDoc = [];
            $scope.createLicenseArray();
            //$scope.clearDocuments();

            $scope.formatData(data);
            $scope.$emit('someEvent', $scope.LicenseTypes);
            $scope.loadingBar = false;
            try {
                $scope.DocumentLoaded = true;
            } catch (e) {
                $scope.DocumentLoaded = true;
            }
        }).error(function (data, status, headers, config) {
            $scope.DocumentLoaded = true;
        });
        $scope.selectedDocument = [];
        $scope.isSelectAll = false;
        $scope.countDoc = 0;

    };

    $scope.LicenseTypes = [];
    $scope.PSVLicenses = null;

    $scope.createLicenseArray = function () {

        $scope.GHIHospitalDocuments = [];
        $scope.GHIProviderDocuments = [];
        $scope.AetnaDocuments = [];
        $scope.AtplDocuments = [];
        $scope.LOIDocuments = [];
        $scope.TricarePADocuments = [];
        $scope.AlliedAccess2Documents = [];
        $scope.AlliedAccessDocuments = [];
        $scope.UltimateCredDocuments = [];
        $scope.UltimateReCredDocuments = [];
        $scope.BcbsPaymentDocuments = [];
        $scope.BcbsUpdateDocuments = [];
        $scope.BcbsRegDocuments = [];
        $scope.MedicadeGroupDocuments = [];
        $scope.TricareARNPDocuments = [];
        $scope.FreedomIPADocuments = [];
        $scope.FreedomSplDocuments = [];
        $scope.HumanaIPADocuments = [];
        $scope.HumanaSplDocuments = [];
        $scope.FLInsuranceDocuments = [];
        $scope.FLHospitalDocuments = [];
        $scope.FL3000Documents = [];
        $scope.WellcareDocuments = [];

        $scope.LicenseTypes.push({ LicenseTypeID: 1, LicenseTypeName: "State License", Licenses: [], LicenseHistories: [] });
        $scope.LicenseTypes.push({ LicenseTypeID: 2, LicenseTypeName: "Other Legal Name Document", Licenses: [], LicenseHistories: [] });
        $scope.LicenseTypes.push({ LicenseTypeID: 3, LicenseTypeName: "Personal Identification Document", Licenses: [], LicenseHistories: [] });
        $scope.LicenseTypes.push({ LicenseTypeID: 4, LicenseTypeName: "Birth Document", Licenses: [], LicenseHistories: [] });
        $scope.LicenseTypes.push({ LicenseTypeID: 5, LicenseTypeName: "CV Document", Licenses: [], LicenseHistories: [] });
        $scope.LicenseTypes.push({ LicenseTypeID: 6, LicenseTypeName: "Citizenship Document", Licenses: [], LicenseHistories: [] });
        $scope.LicenseTypes.push({ LicenseTypeID: 7, LicenseTypeName: "Federal DEA Document", Licenses: [], LicenseHistories: [] });
        $scope.LicenseTypes.push({ LicenseTypeID: 8, LicenseTypeName: "Medicare Document", Licenses: [], LicenseHistories: [] });
        $scope.LicenseTypes.push({ LicenseTypeID: 9, LicenseTypeName: "Medicaid Document", Licenses: [], LicenseHistories: [] });
        $scope.LicenseTypes.push({ LicenseTypeID: 10, LicenseTypeName: "CDSC Document", Licenses: [], LicenseHistories: [] });
        $scope.LicenseTypes.push({ LicenseTypeID: 11, LicenseTypeName: "Education Document", Licenses: [], LicenseHistories: [] });
        $scope.LicenseTypes.push({ LicenseTypeID: 12, LicenseTypeName: "ECFMG Document", Licenses: [], LicenseHistories: [] });
        $scope.LicenseTypes.push({ LicenseTypeID: 13, LicenseTypeName: "Program Document", Licenses: [], LicenseHistories: [] });
        $scope.LicenseTypes.push({ LicenseTypeID: 14, LicenseTypeName: "CME Certifications", Licenses: [], LicenseHistories: [] });
        $scope.LicenseTypes.push({ LicenseTypeID: 15, LicenseTypeName: "Specialty Board Certificates", Licenses: [], LicenseHistories: [] });
        $scope.LicenseTypes.push({ LicenseTypeID: 16, LicenseTypeName: "Hospital Privilege Document", Licenses: [], LicenseHistories: [] });
        $scope.LicenseTypes.push({ LicenseTypeID: 17, LicenseTypeName: "Professional Liability Document", Licenses: [], LicenseHistories: [] });
        $scope.LicenseTypes.push({ LicenseTypeID: 18, LicenseTypeName: "WorkExperience Document", Licenses: [], LicenseHistories: [] });
        $scope.LicenseTypes.push({ LicenseTypeID: 19, LicenseTypeName: "Contract", Licenses: [], LicenseHistories: [] });
        $scope.LicenseTypes.push({ LicenseTypeID: 20, LicenseTypeName: "Other", Licenses: [], LicenseHistories: [] });
        $scope.LicenseTypes.push({ LicenseTypeID: 21, LicenseTypeName: "Plan Form", Licenses: [], LicenseHistories: [] });
        $scope.LicenseTypes.push({ LicenseTypeID: 21, LicenseTypeName: "PSV", Licenses: [], LicenseHistories: [] });

        //$scope.LicenseTypes.push({ LicenseTypeID: 22, LicenseTypeName: "Plan Form", Licenses: [], LicenseHistories: [] });
        //$scope.LicenseTypes.push({ LicenseTypeID: 23, LicenseTypeName: "AETNACOVENTRYTemplate", Licenses: [], LicenseHistories: [] });
        //$scope.LicenseTypes.push({ LicenseTypeID: 24, LicenseTypeName: "TRICARE_PROVIDER_APPLICATION", Licenses: [], LicenseHistories: [] });
        //$scope.LicenseTypes.push({ LicenseTypeID: 25, LicenseTypeName: "Letter_of_Intent", Licenses: [], LicenseHistories: [] });
        //$scope.LicenseTypes.push({ LicenseTypeID: 26, LicenseTypeName: "TRICARE_PA_APPLICATION", Licenses: [], LicenseHistories: [] });
        //$scope.LicenseTypes.push({ LicenseTypeID: 27, LicenseTypeName: "ALLIED_CREDENTIALING_APPLICATION_ACCESS2", Licenses: [], LicenseHistories: [] });
        //$scope.LicenseTypes.push({ LicenseTypeID: 28, LicenseTypeName: "ALLIED_CREDENTIALING_APPLICATION_ACCESS", Licenses: [], LicenseHistories: [] });
        //$scope.LicenseTypes.push({ LicenseTypeID: 29, LicenseTypeName: "UltimateCredentialingApplicationPractitioner_2015", Licenses: [], LicenseHistories: [] });
        //$scope.LicenseTypes.push({ LicenseTypeID: 30, LicenseTypeName: "BCBS_PAYMENT_AUTH_FORM", Licenses: [], LicenseHistories: [] });
        //$scope.LicenseTypes.push({ LicenseTypeID: 31, LicenseTypeName: "BCBS_PROVIDER_UPDATE_FORM", Licenses: [], LicenseHistories: [] });
        //$scope.LicenseTypes.push({ LicenseTypeID: 32, LicenseTypeName: "BCBS_PROVIDER_REGISTRATION_FORM", Licenses: [], LicenseHistories: [] });
        //$scope.LicenseTypes.push({ LicenseTypeID: 33, LicenseTypeName: "MEDICAID_GROUP_MEMBERSHIP_AUTHORIZATION_FORM", Licenses: [], LicenseHistories: [] });
        //$scope.LicenseTypes.push({ LicenseTypeID: 34, LicenseTypeName: "TRICARE_ARNP_APPLICATION_FORM", Licenses: [], LicenseHistories: [] });
        //$scope.LicenseTypes.push({ LicenseTypeID: 35, LicenseTypeName: "Freedom_Optimum_IPA_Enrollment_Provider_PCP", Licenses: [], LicenseHistories: [] });
        //$scope.LicenseTypes.push({ LicenseTypeID: 36, LicenseTypeName: "Freedom_Optimum_Specialist_Package", Licenses: [], LicenseHistories: [] });
        //$scope.LicenseTypes.push({ LicenseTypeID: 37, LicenseTypeName: "Humana_IPA_New_PCP_Package", Licenses: [], LicenseHistories: [] });
        //$scope.LicenseTypes.push({ LicenseTypeID: 38, LicenseTypeName: "Humana_Specialist_New_Provider", Licenses: [], LicenseHistories: [] });
        //$scope.LicenseTypes.push({ LicenseTypeID: 39, LicenseTypeName: "FL_INSURANCE_PROVIDER_ATTESTATION", Licenses: [], LicenseHistories: [] });
        //$scope.LicenseTypes.push({ LicenseTypeID: 40, LicenseTypeName: "FL_HOSPITAL_ADMIT_ATTESTATION", Licenses: [], LicenseHistories: [] });
        //$scope.LicenseTypes.push({ LicenseTypeID: 41, LicenseTypeName: "FL_3000_PCP_Attestation_of_Patient_Load_2015_Global", Licenses: [], LicenseHistories: [] });
        //$scope.LicenseTypes.push({ LicenseTypeID: 42, LicenseTypeName: "WELLCARE_MIDLEVEL_FORMS", Licenses: [], LicenseHistories: [] });

    }

    //$scope.clearDocuments = function () {


    //    $scope.LicenseTypes = [];
    //    $scope.spaDoc = [];
    //}
    $scope.curFile;
    $scope.ImageProperty = {
        file: '',
        FileListID: -1,
        FileID: -1,
        FileStatus: ''
    }
    $scope.removeFile = function (index, fileObj) {
        $scope.fileList.splice(index, 1)
    }

    var files = [];
    var tempIndex = 0;
    var tempmultiplefilelength;
    $scope.getStyle = function () {
        var transform = ($scope.isSemi ? '' : 'translateY(-50%) ') + 'translateX(-50%)';

        return {
            'top': $scope.isSemi ? 'auto' : '50%',
            'bottom': $scope.isSemi ? '5%' : 'auto',
            'left': '35%',
            'transform': transform,
            '-moz-transform': transform,
            '-webkit-transform': transform,
            'font-size': $scope.radius / 3.5 + 'px'
        };
    };

    $scope.ExportPDF = function () {
        $timeout(function () {
            var doc = new jsPDF();
            var specialElementHandlers = {
                '#editor': function (element, renderer) {
                    return true;
                }
            };
            doc.fromHTML($('#deepu').html(), 15, 15, {
                'width': 170,
                'elementHandlers': specialElementHandlers
            });
            doc.output('dataurlnewwindow');
        }, 500)

    }

    $scope.setFile = function (element) {
        var count = 0;
        tempIndex = 0;
        var index = -1;
        files = [];
        files = element.files;
        var totalfilesize = 0;
        tempmultiplefilelength = $scope.fileList.length;
        if (count == 0) {
            for (var i = 0; i < files.length; i++) {
                if (files[i].size > 2000000) {
                    //$('#Filesizeerror').show();
                    //$scope.FilesizeError = true;
                    $('.badge').attr("style", "background-color:white;color:indianred");
                    // $('.badge').attr("style", "color:red");
                    //$scope.fileList = [];
                    break;
                } else {
                    //$('#Filesizeerror').hide();
                    $('.badge').removeAttr("style");
                    totalfilesize += files[i].size;
                    var TempArray = [];
                    $scope.ImageProperty.file = files[i];
                    $scope.ImageProperty.FileStatus = 'Active';
                    $scope.ImageProperty.FileListID = $scope.fileList.length;
                    $scope.ImageProperty.FileID = i;
                    TempArray.push($scope.ImageProperty);
                    $scope.fileList.push({ File: TempArray });
                    $scope.ImageProperty = {};

                    if (!$scope.$$fetch)
                        $scope.$apply();
                }
            }
        }
        $scope.UploadFile();
    }


    $scope.UploadFile = function () {

        for (var i = 0; i < $scope.fileList.length; i++) {
            for (var j = 0; j < $scope.fileList[i].File.length; j++) {
                if ($scope.fileList[i].File[j].UploadDone != true) $scope.fileList[i].File[j].UploadDone = false;
                if ($scope.fileList[i].File[j].FileStatus == 'Active') {
                    $scope.UploadFileIndividual($scope.fileList[i].File[j].file,
                                        $scope.fileList[i].File[j].file.name,
                                        $scope.fileList[i].File[j].file.type,
                                        $scope.fileList[i].File[j].file.size,
                                        $scope.fileList[i].File[j].FileListID,
                                        $scope.fileList[i].File[j].FileID
                                        );
                    $scope.fileList[i].File[j].FileStatus = 'Inactive';
                }
            }
        }
    }

    $scope.UploadFileIndividual = function (fileToUpload, name, type, size, Qindex, FLindex, Findex) {
        $scope.current = 0;
        var reqObj = new XMLHttpRequest();
        reqObj.upload.addEventListener("progress", uploadProgress, false)
        reqObj.addEventListener("load", uploadComplete, false)
        reqObj.addEventListener("error", uploadFailed, false)
        reqObj.addEventListener("abort", uploadCanceled, false)
        reqObj.open("POST", rootDir + "/Profile/DisclosureQuestion/FileUpload", true);
        reqObj.setRequestHeader("Content-Type", "multipart/form-data");
        reqObj.setRequestHeader('X-File-Name', name);
        reqObj.setRequestHeader('X-File-Type', type);
        reqObj.setRequestHeader('X-File-Size', size);


        reqObj.send(fileToUpload);

        function uploadProgress(evt) {
            if (evt.lengthComputable) {

                var uploadProgressCount = Math.round(evt.loaded * 100 / evt.total);
                $scope.current = uploadProgressCount;

                if (uploadProgressCount == 100) {
                    $scope.current = uploadProgressCount;
                }

            }
        }

        function uploadComplete(evt) {
            var resultdata = JSON.parse(evt.currentTarget.responseText);
            $scope.Attachments.push(resultdata.FilePath);
            if (files.length == 1) {
                $scope.fileList[$scope.fileList.length - 1].File[0].path = resultdata.FilePath;
                $scope.fileList[$scope.fileList.length - 1].File[0].relativePath = resultdata.RelativePath;
                $scope.fileList[$scope.fileList.length - 1].File[0].UploadDone = true;
            } else if (files.length != 1 && tempmultiplefilelength != 0) {
                $scope.fileList[tempmultiplefilelength].File[0].path = resultdata.FilePath;
                $scope.fileList[tempmultiplefilelength].File[0].relativePath = resultdata.RelativePath;
                $scope.fileList[tempmultiplefilelength].File[0].UploadDone = true;
                tempmultiplefilelength++;
            } else {
                $scope.fileList[tempIndex].File[0].path = resultdata.FilePath;
                $scope.fileList[tempIndex].File[0].relativePath = resultdata.RelativePath;
                $scope.fileList[tempIndex].File[0].UploadDone = true;
                tempIndex++;
            }
            $scope.NoOfFileSaved++;
            $scope.$apply();
            $('#file').val("");
        }

        function uploadFailed(evt) {
        }

        function uploadCanceled(evt) {
        }

    }
    $scope.Attachments = [];

    $scope.AddOrSaveEmail = function (Form_Id, ProfileId) {
        $scope.FileUploadProgress = false;
        var AttachmentsSize = 0;
        for (i = 0; i < $scope.fileList.length; i++) {
            AttachmentsSize += $scope.fileList[i].File[0].file.size;
            if ($scope.fileList[i].File[0].relativePath == "" || $scope.fileList[i].File[0].relativePath === undefined) {
                $scope.FileUploadProgress = true;
                messageAlertEngine.callAlertMessage('warningdiv', "File Upload is in Progress", "info", true);
                //$('#composeMail').animate({ scrollBottom: 0 }, 'medium');
                break;
            }
        }
        if (AttachmentsSize > 15728640) {
            messageAlertEngine.callAlertMessage('warningdiv', 'Files exceeded the size limit!', "info", true);
        }
        else {
        }
        //ResetFormForValidation($("#" + Form_Id));       
        $scope.docList1 = [];
        $scope.packageLoading = false;


        //$scope.generatedPdfTemplateList = [];
        for (var i = 0; i < $scope.selectedDocument.length; i++) {

            $scope.docList1.push($scope.selectedDocument[i].LicenseDocPath);

        }
        $scope.docList1 = JSON.stringify($scope.docList1);
        var regx1 = /^[a-z][a-zA-Z0-9_]*(\.[a-zA-Z][a-zA-Z0-9_]*)?@[a-z][a-zA-Z-0-9]*\.[a-z]+(\.[a-z]+)?$/;
        ResetFormForValidation($("#" + Form_Id));
        var emailids = $('#tags').val().split(';');
        for (var i in emailids) {
            if (emailids[i] != "") {
                $scope.errmsg = false;
                if (regx1.test(emailids[i].toLowerCase())) {
                    $scope.errmsgforTo = false;
                }
                else {
                    if (i == 0)
                        $scope.errmsgforTo = true;
                }
            }
            else {
                if (i == 0)
                    $scope.errmsg = true;
            }

        }
        if ($scope.tempObject.ToDate && $scope.tempObject.FromDate) {
            if ($scope.tempObject.ToDate < $scope.tempObject.FromDate)

                $scope.errmsgforToDate = true;

            else
                $scope.errmsgforToDate = false;

        }
        var emailDataCC = $('#tagsCC').val().split(';');
        for (var i in emailDataCC) {
            if (emailDataCC[i] != "") {
                if (regx1.test(emailDataCC[i].toLowerCase())) {
                    $scope.errmsgforCC = false;
                }
                else { $scope.errmsgforCC = true; }
            }
        }

        var emailDataBCC = $('#tagsBCC').val().split(';');
        for (var i in emailDataBCC) {
            if (emailDataBCC[i] != "") {
                if (regx1.test(emailDataBCC[i].toLowerCase())) {
                    $scope.errmsgforBCC = false;
                }
                else { $scope.errmsgforBCC = true; }
            }
        }
        var checkVariable = false;
        if (!$scope.errmsgforBCC && !$scope.errmsgforCC && !$scope.errmsgforTo && !$scope.errmsg && !$scope.errmsgforToDate) { checkVariable = true; }

        if ($("#" + Form_Id).valid() && checkVariable && AttachmentsSize < 15728640) {
            //if ($("#" + Form_Id).valid()) {

            var ltcharCheck = true;
            var gtcharCheck = true;
            while (gtcharCheck == true || ltcharCheck == true) {
                if ($('#Body').val().indexOf('<') > -1) {
                    $('#Body').val($('#Body').val().replace("<", "&lt;"));
                    ltcharCheck = true;
                }
                else {
                    ltcharCheck = false;
                }
                if ($('#Body').val().indexOf('>') > -1) {
                    $('#Body').val($('#Body').val().replace(">", "&gt;"));
                    gtcharCheck = true;
                }
                else {
                    gtcharCheck = false;
                }
            }

            var $form = ($("#" + Form_Id)[0]);
            $.ajax({
                url: rootDir + '/Credentialing/Auditing/SendPackageGeneratorEmail?doclist=' + $scope.docList1,
                type: 'POST',
                data: new FormData($form),
                async: false,
                cache: false,
                contentType: false,
                processData: false,
                success: function (data) {
                    try {
                        if (data.status == "true") {
                            $('#composeMail').modal('toggle');
                            $scope.compose = false;
                            $scope.tempObject.IntervalFactor = null;
                            $scope.ClearModalData();
                            messageAlertEngine.callAlertMessage('LoadedSuccess', "Email scheduled for sending.", "success", true);
                        }
                        else if (data.status == "File Size Exceeded") {
                            messageAlertEngine.callAlertMessage('FilesizeExceedError', "Selected File(s) size exceeded 3MB.", "danger", true);
                        }
                        else {
                            $scope.ClearModalData();
                            $('#composeMail').modal('toggle');
                            messageAlertEngine.callAlertMessage('pkgGeneratedError', data.status, "danger", true);
                        }
                    } catch (e) {

                    }
                    finally {
                    }
                },
                error: function (data) {
                    messageAlertEngine.callAlertMessage('errorMsgDiv', data.status, "danger", true);
                }
            });
        }
        else {
            $("#newEmailForm .field-validation-error").show();
        }
    }

    $scope.ClearModalData = function () {
        $scope.tempObject.To = "";
        $scope.tempObject.CC = "";
        $scope.tempObject.BCC = "";
        $scope.tempObject.Subject = "";
        $scope.tempObject.Body = "";
        $scope.tempObject.IsRecurrenceEnabledYesNoOption = "";
        $scope.tempObject.RecurrenceIntervalTypeCategory = "";
        $scope.tempObject.FromDate = "";
        $scope.tempObject.ToDate = "";
        $scope.tempObject.IntervalFactor = "";
    }

    $scope.formatData = function (data) {

        for (var i = 0; i < $scope.LicenseTypes.length; i++) {
            $scope.LicenseTypes[i].Licenses = [];
            $scope.LicenseTypes[i].LicenseHistories = [];
        }

        if (data.StateLicenses != null) {
            if (data.StateLicenses.length > 0) {
                $scope.FormatStateLicense(data.StateLicenses);
            }
        }
        if (data.OtherLegalNames != null) {
            if (data.OtherLegalNames.length > 0) {
                $scope.FormatOtherLegalNamesDoc(data.OtherLegalNames);
            }
        }
        if (data.PersonalIdentification != null) {
            $scope.FormatPersonalIdentificationDoc(data.PersonalIdentification);
        }

        if (data.BirthInformation != null) {
            $scope.FormatBirthInformationDoc(data.BirthInformation);
        }

        if (data.CVInformation != null) {
            $scope.FormatCVInformationDoc(data.CVInformation);
        }

        if (data.VisaDetail != null) {
            $scope.FormatVisaInfoDoc(data.VisaDetail);
        }

        if (data.FederalDEAInformations != null) {
            if (data.FederalDEAInformations.length > 0) {
                $scope.FormatFederalDEAInformationDoc(data.FederalDEAInformations);
            }
        }

        if (data.MedicareInformations != null) {
            if (data.MedicareInformations.length > 0) {
                $scope.FormatMedicareInformationDoc(data.MedicareInformations);
            }
        }

        if (data.MedicaidInformations != null) {
            if (data.MedicaidInformations.length > 0) {
                $scope.FormatMedicaidInformationDoc(data.MedicaidInformations);
            }
        }

        if (data.CDSCInformations != null) {
            if (data.CDSCInformations.length > 0) {
                $scope.FormatCDSCInformationDoc(data.CDSCInformations);
            }
        }

        if (data.EducationDetails != null) {
            if (data.EducationDetails.length > 0) {
                $scope.FormatEducationDetailDoc(data.EducationDetails);
            }
        }

        if (data.ECFMGDetail != null) {
            $scope.FormatECFMGDetailDoc(data.ECFMGDetail);
        }

        if (data.ProgramDetails != null) {
            if (data.ProgramDetails.length > 0) {
                $scope.FormatProgramDetailsDoc(data.ProgramDetails);
            }
        }

        if (data.CMECertifications != null) {
            if (data.CMECertifications.length > 0) {
                $scope.FormatCMECertificationsDoc(data.CMECertifications);
            }
        }

        if (data.SpecialtyDetails != null) {
            if (data.SpecialtyDetails.length > 0) {
                $scope.FormatSpecialtyBoardCertifiedDetailDoc(data.SpecialtyDetails);
            }

        }

        if (data.HospitalPrivilegeInformation != null) {
            if (data.HospitalPrivilegeInformation.HospitalPrivilegeDetails != null) {
                if (data.HospitalPrivilegeInformation.HospitalPrivilegeDetails.length > 0) {
                    $scope.FormatHospitalPrivilegeDetailDoc(data.HospitalPrivilegeInformation.HospitalPrivilegeDetails);
                }
            }
        }

        if (data.ProfessionalLiabilityInfoes != null) {
            if (data.ProfessionalLiabilityInfoes.length > 0) {
                $scope.FormatProfessionalLiabilityInfoDoc(data.ProfessionalLiabilityInfoes);
            }
        }

        if (data.ProfessionalWorkExperiences != null) {
            if (data.ProfessionalWorkExperiences.length > 0) {
                $scope.FormatProfessionalWorkExperienceDoc(data.ProfessionalWorkExperiences);
            }
        }

        if (data.ContractInfoes != null) {
            if (data.ContractInfoes.length > 0) {
                $scope.FormatContractInfoeDoc(data.ContractInfoes);
            }
        }


        if (data.OtherDocuments != null) {
            if (data.OtherDocuments.length > 0) {
                $scope.PlanFormDocuments = [];
                $scope.Others = [];
                //$scope.FormatOtherDocument(data.OtherDocuments);
                for (var i = 0; i < data.OtherDocuments.length; i++) {
                    if (data.OtherDocuments[i].DocumentCategory != null && data.OtherDocuments[i].DocumentCategory == "PlanFormPdf") {
                        $scope.PlanFormDocuments.push(data.OtherDocuments[i]);
                    }
                    else {
                        $scope.Others.push(data.OtherDocuments[i]);
                    }
                }

                //$filter('orderBy')($scope.PlanFormDocuments, 'LastModifiedDate',true)

                for (var j = 0; j < $scope.PlanFormDocuments.length; j++) {

                    if ($scope.PlanFormDocuments[j].Title == "GHI_HOSPITALIST_FORM") {

                        $scope.GHIHospitalDocuments.push($scope.PlanFormDocuments[j]);
                    }
                    if ($scope.PlanFormDocuments[j].Title == "GHI_Provider_Form") {

                        $scope.GHIProviderDocuments.push($scope.PlanFormDocuments[j]);
                    }
                    if ($scope.PlanFormDocuments[j].Title == "AETNACOVENTRYTemplate") {

                        $scope.AetnaDocuments.push($scope.PlanFormDocuments[j]);
                    }
                    if ($scope.PlanFormDocuments[j].Title == "ATPL2016") {

                        $scope.AtplDocuments.push($scope.PlanFormDocuments[j]);
                    }
                    if ($scope.PlanFormDocuments[j].Title == "Letter_of_Intent") {

                        $scope.LOIDocuments.push($scope.PlanFormDocuments[j]);
                    }
                    if ($scope.PlanFormDocuments[j].Title == "TRICARE_PA_APPLICATION") {

                        $scope.TricarePADocuments.push($scope.PlanFormDocuments[j]);
                    }
                    if ($scope.PlanFormDocuments[j].Title == "ALLIED_CREDENTIALING_APPLICATION_ACCESS2") {

                        $scope.AlliedAccess2Documents.push($scope.PlanFormDocuments[j]);
                    }
                    if ($scope.PlanFormDocuments[j].Title == "ALLIED_CREDENTIALING_APPLICATION_ACCESS") {

                        $scope.AlliedAccessDocuments.push($scope.PlanFormDocuments[j]);
                    }
                    if ($scope.PlanFormDocuments[j].Title == "Ultimate_Credentialing_Application") {

                        $scope.UltimateCredDocuments.push($scope.PlanFormDocuments[j]);
                    }
                    if ($scope.PlanFormDocuments[j].Title == "Ultimate_Re-Credentialing_Application") {

                        $scope.UltimateReCredDocuments.push($scope.PlanFormDocuments[j]);
                    }
                    if ($scope.PlanFormDocuments[j].Title == "BCBS_PAYMENT_AUTH_FORM") {

                        $scope.BcbsPaymentDocuments.push($scope.PlanFormDocuments[j]);
                    }
                    if ($scope.PlanFormDocuments[j].Title == "BCBS_PROVIDER_UPDATE_FORM") {

                        $scope.BcbsUpdateDocuments.push($scope.PlanFormDocuments[j]);
                    }
                    if ($scope.PlanFormDocuments[j].Title == "BCBS_PROVIDER_REGISTRATION_FORM") {

                        $scope.BcbsRegDocuments.push($scope.PlanFormDocuments[j]);
                    }
                    if ($scope.PlanFormDocuments[j].Title == "MEDICAID_GROUP_MEMBERSHIP_AUTHORIZATION_FORM") {

                        $scope.MedicadeGroupDocuments.push($scope.PlanFormDocuments[j]);
                    }
                    if ($scope.PlanFormDocuments[j].Title == "TRICARE_ARNP_APPLICATION_FORM") {

                        $scope.TricareARNPDocuments.push($scope.PlanFormDocuments[j]);
                    }
                    if ($scope.PlanFormDocuments[j].Title == "Freedom_Optimum_IPA_Enrollment_Provider_PCP") {

                        $scope.FreedomIPADocuments.push($scope.PlanFormDocuments[j]);
                    }
                    if ($scope.PlanFormDocuments[j].Title == "Freedom_Optimum_Specialist_Package") {

                        $scope.FreedomSplDocuments.push($scope.PlanFormDocuments[j]);
                    }
                    if ($scope.PlanFormDocuments[j].Title == "Humana_IPA_New_PCP_Package") {

                        $scope.HumanaIPADocuments.push($scope.PlanFormDocuments[j]);
                    }
                    if ($scope.PlanFormDocuments[j].Title == "Humana_Specialist_New_Provider") {

                        $scope.HumanaSplDocuments.push($scope.PlanFormDocuments[j]);
                    }
                    if ($scope.PlanFormDocuments[j].Title == "FL_INSURANCE_PROVIDER_ATTESTATION") {

                        $scope.FLInsuranceDocuments.push($scope.PlanFormDocuments[j]);
                    }
                    if ($scope.PlanFormDocuments[j].Title == "FL_HOSPITAL_ADMIT_ATTESTATION") {

                        $scope.FLHospitalDocuments.push($scope.PlanFormDocuments[j]);
                    }
                    if ($scope.PlanFormDocuments[j].Title == "FL_3000_PCP_Attestation_of_Patient_Load_2016_Global") {

                        $scope.FL3000Documents.push($scope.PlanFormDocuments[j]);
                    }
                    if ($scope.PlanFormDocuments[j].Title == "WELLCARE_MIDLEVEL_FORMS") {

                        $scope.WellcareDocuments.push($scope.PlanFormDocuments[j]);
                    }


                }



                $scope.FormatPlanFormDocument($scope.GHIHospitalDocuments);
                $scope.FormatPlanFormDocument($scope.GHIProviderDocuments);
                $scope.FormatPlanFormDocument($scope.AetnaDocuments);
                $scope.FormatPlanFormDocument($scope.AtplDocuments);
                $scope.FormatPlanFormDocument($scope.LOIDocuments);
                $scope.FormatPlanFormDocument($scope.TricarePADocuments);
                $scope.FormatPlanFormDocument($scope.AlliedAccess2Documents);
                $scope.FormatPlanFormDocument($scope.AlliedAccessDocuments);
                $scope.FormatPlanFormDocument($scope.UltimateCredDocuments);
                $scope.FormatPlanFormDocument($scope.UltimateReCredDocuments);
                $scope.FormatPlanFormDocument($scope.BcbsPaymentDocuments);
                $scope.FormatPlanFormDocument($scope.BcbsUpdateDocuments);
                $scope.FormatPlanFormDocument($scope.BcbsRegDocuments);
                $scope.FormatPlanFormDocument($scope.MedicadeGroupDocuments);
                $scope.FormatPlanFormDocument($scope.TricareARNPDocuments);
                $scope.FormatPlanFormDocument($scope.FreedomIPADocuments);
                $scope.FormatPlanFormDocument($scope.FreedomSplDocuments);
                $scope.FormatPlanFormDocument($scope.HumanaIPADocuments);
                $scope.FormatPlanFormDocument($scope.HumanaSplDocuments);
                $scope.FormatPlanFormDocument($scope.FLInsuranceDocuments);
                $scope.FormatPlanFormDocument($scope.FLHospitalDocuments);
                $scope.FormatPlanFormDocument($scope.FL3000Documents);
                $scope.FormatPlanFormDocument($scope.WellcareDocuments);

                //$scope.FormatPlanFormDocument($scope.PlanFormDocuments);
                $scope.FormatOtherDocument($scope.Others);
            }
        }

        if (data.ProfileVerificationInfo != null) {

            $scope.FormatProfileVerificationInfo(data.ProfileVerificationInfo);

        }

        $scope.combine();
    }

    //var orderBy = $filter('orderBy');

    //$scope.order = function (predicate, reverse) {
    //    $scope.friends = orderBy($scope.friends, predicate, reverse);
    //};

    $scope.spaDoc = [];

    $scope.checkEmailFileSize = function () {
        console.log($scope.selectedDocument);
        var attachmentSize = 0;
        for (var i = 0; i < $scope.selectedDocument.length; i++) {
            attachmentSize += $scope.selectedDocument[i].LicenseDocSize;
        }
        if (attachmentSize < 31457280) {
            $('#warningmodal').modal();
        }
        else {
            $('#emailattachmentsizewarningmodal').modal();
        }
    }
    $scope.FormatStateLicense = function (Docs) {

        for (var i = 0; i < Docs.length; i++) {
            if (Docs[i].StateLicenseDocumentPath != null) {
                $scope.LicenseTypes[0].Licenses.push({ LicenseID: Docs[i].StateLicenseInformationID, LicenseName: "Medical License" + (i + 1), LicenseDocPath: Docs[i].StateLicenseDocumentPath, LicenseDocSize: Docs[i].StateLicenseDocumentSize, ModifiedDate: Docs[i].LastModifiedDate, Description: "State License" });
                if (Docs[i].StateLicenseInfoHistory != null) {
                    for (var j = 0; j < Docs[i].StateLicenseInfoHistory.length; j++) {
                        if (Docs[i].StateLicenseInfoHistory[j] != null) {
                            $scope.LicenseTypes[0].LicenseHistories.push({ LicenseID: Docs[i].StateLicenseInfoHistory[j].StatelicenseInfoHistoryID, LicenseName: "Medical License" + (j + 1), LicenseDocPath: Docs[i].StateLicenseInfoHistory[j].StatelicenseDocumentPath, removeDate: Docs[i].StateLicenseInfoHistory[j].LastModifiedDate });

                        }
                    }
                }
            }
        }
    }

    $scope.FormatOtherLegalNamesDoc = function (Docs) {
        for (var i = 0; i < Docs.length; i++) {
            if (Docs[i].DocumentPath != null) {
                $scope.LicenseTypes[1].Licenses.push({ LicenseID: Docs[i].OtherLegalNameID, LicenseName: "Other Legal Name" + (i + 1), LicenseDocPath: Docs[i].DocumentPath, LicenseDocSize: Docs[i].DocumentSize, ModifiedDate: Docs[i].LastModifiedDate, Description: "Other Legal Name" });

                if (Docs[i].OtherLegalNameHistory != null) {
                    $scope.LicenseTypes[1].LicenseHistories.push({ LicenseID: Docs[i].OtherLegalNameHistory[j].OtherLegalNameHistoryID, LicenseName: "Other Legal Name" + (j + 1), LicenseDocPath: Docs[i].OtherLegalNameHistory[j].DocumentPath, removeDate: Docs[i].OtherLegalNameHistory[j].LastModifiedDate });

                }

            }

        }

    }

    $scope.FormatPersonalIdentificationDoc = function (Docs) {
        if (Docs.SSNCertificatePath != null) {
            $scope.LicenseTypes[2].Licenses.push({ LicenseID: Docs.PersonalIdentificationID, LicenseName: "Social Security Number", LicenseDocPath: Docs.SSNCertificatePath, LicenseDocSize: Docs.SSNCertificateSize, ModifiedDate: Docs.LastModifiedDate, Description: "Social Security Number" });

        }

        if (Docs.DLCertificatePath != null) {
            $scope.LicenseTypes[2].Licenses.push({ LicenseID: Docs.PersonalIdentificationID, LicenseName: "Driver License", LicenseDocPath: Docs.DLCertificatePath, LicenseDocSize: Docs.DLCertificateSize, ModifiedDate: Docs.LastModifiedDate, Description: "Driver License" });

        }

    }

    $scope.FormatBirthInformationDoc = function (Docs) {
        if (Docs.BirthCertificatePath != null) {
            $scope.LicenseTypes[3].Licenses.push({ LicenseID: Docs.BirthInformationID, LicenseName: "Birth Certificate", LicenseDocPath: Docs.BirthCertificatePath, LicenseDocSize: Docs.BirthCertificateSize, ModifiedDate: Docs.LastModifiedDate, Description: "Birth Certificate" });

            $scope.LicenseTypes[3].LicenseHistories.push({ LicenseID: 1, LicenseName: "Birth Certificate", LicenseDocPath: Docs.BirthCertificatePath, removeDate: Docs.LastModifiedDate });

        }
    }

    $scope.FormatCVInformationDoc = function (Docs) {
        if (Docs.CVDocumentPath != null) {
            $scope.LicenseTypes[4].Licenses.push({ LicenseID: Docs.CVInformationID, LicenseName: "Curriculum Vitae", LicenseDocPath: Docs.CVDocumentPath, LicenseDocSize: Docs.CVDocumentSize, ModifiedDate: Docs.LastModifiedDate, Description: "Curriculum Vitae" });

        }
    }

    $scope.FormatVisaInfoDoc = function (Docs) {
        if (Docs.VisaInfo != null) {
            if (Docs.VisaInfo.VisaCertificatePath != null) {
                $scope.LicenseTypes[5].Licenses.push({ LicenseID: Docs.VisaInfo.VisaInfoID, LicenseName: "VISA", LicenseDocPath: Docs.VisaInfo.VisaCertificatePath, LicenseDocSize: Docs.VisaInfo.VisaCertificateSize, ModifiedDate: Docs.LastModifiedDate, Description: "Visa" });

            }

            if (Docs.VisaInfo.GreenCardCertificatePath != null) {

                $scope.LicenseTypes[5].Licenses.push({ LicenseID: Docs.VisaInfo.VisaInfoID, LicenseName: "Green Card", LicenseDocPath: Docs.VisaInfo.GreenCardCertificatePath, LicenseDocSize: Docs.VisaInfo.GreenCardCertificateSize, ModifiedDate: Docs.LastModifiedDate, Description: "Green Card" });

            }

            if (Docs.VisaInfo.NationalIDCertificatePath != null) {

                $scope.LicenseTypes[5].Licenses.push({ LicenseID: Docs.VisaInfo.VisaInfoID, LicenseName: "National Identification", LicenseDocPath: Docs.VisaInfo.NationalIDCertificatePath, LicenseDocSize: Docs.VisaInfo.NationalIDCertificateSize, ModifiedDate: Docs.LastModifiedDate, Description: "National Identification Certificate" });

            }
        }

    }

    $scope.FormatFederalDEAInformationDoc = function (Docs) {

        $scope.Licenses = [];
        $scope.LicenseHistories = [];

        for (var i = 0; i < Docs.length; i++) {

            if (Docs[i].DEALicenceCertPath != null) {

                $scope.LicenseTypes[6].Licenses.push({ LicenseID: Docs[i].FederalDEAInformationID, LicenseName: "Drug Enforcement Administration" + (i + 1), LicenseDocPath: Docs[i].DEALicenceCertPath, LicenseDocSize: Docs[i].DEALicenceCertSize, ModifiedDate: Docs[i].LastModifiedDate, Description: "Drug Enforcement Administration" });

                if (Docs[i].FederalDEAInfoHistory != null) {

                    for (var j = 0; j < Docs[i].FederalDEAInfoHistory.length; j++) {

                        $scope.LicenseTypes[6].LicenseHistories.push({ LicenseID: Docs[i].FederalDEAInfoHistory[j].FederalDEAInfoHistoryID, LicenseName: "Drug Enforcement Administration" + (j + 1), LicenseDocPath: Docs[i].FederalDEAInfoHistory[j].FederalDEADocumentPath, removeDate: Docs[i].FederalDEAInfoHistory[j].LastModifiedDate });

                    }

                }

            }

        }
    }

    $scope.FormatMedicareInformationDoc = function (Docs) {

        $scope.Licenses = [];
        $scope.LicenseHistories = [];

        for (var i = 0; i < Docs.length; i++) {

            if (Docs[i].CertificatePath != null) {

                $scope.LicenseTypes[7].Licenses.push({ LicenseID: Docs[i].MedicareInformationID, LicenseName: "Medicare" + (i + 1), LicenseDocPath: Docs[i].CertificatePath, LicenseDocSize: Docs[i].CertificateSize, ModifiedDate: Docs[i].LastModifiedDate, Description: "Medicare" });

                if (Docs[i].MedicareInformationHistory != null) {

                    $scope.LicenseTypes[7].LicenseHistories.push({ LicenseID: Docs[i].MedicareInformationHistory.MedicareInformationHistoryID, LicenseName: "Medicare" + (j + 1), LicenseDocPath: Docs[i].MedicareInformationHistory.CertificatePath, removeDate: Docs[i].MedicareInformationHistory.LastModifiedDate });

                }

            }

        }
    }

    $scope.FormatMedicaidInformationDoc = function (Docs) {

        $scope.Licenses = [];
        $scope.LicenseHistories = [];

        for (var i = 0; i < Docs.length; i++) {

            if (Docs[i].CertificatePath != null) {

                $scope.LicenseTypes[8].Licenses.push({ LicenseID: Docs[i].MedicaidInformationID, LicenseName: "Medicaid" + (i + 1), LicenseDocPath: Docs[i].CertificatePath, LicenseDocSize: Docs[i].CertificateSize, ModifiedDate: Docs[i].LastModifiedDate, Description: "Medicaid" });

                if (Docs[i].MedicaidInformationHistory != null) {

                    $scope.LicenseTypes[8].LicenseHistories.push({ LicenseID: Docs[i].MedicaidInformationHistory.MedicaidInformationHistoryID, LicenseName: "Medicade" + (j + 1), LicenseDocPath: Docs[i].MedicaidInformationHistory.CertificatePath, removeDate: Docs[i].MedicaidInformationHistory.LastModifiedDate });

                }
            }
        }
    }

    $scope.FormatCDSCInformationDoc = function (Docs) {

        $scope.Licenses = [];
        $scope.LicenseHistories = [];

        for (var i = 0; i < Docs.length; i++) {

            if (Docs[i].CDSCCerificatePath != null) {

                $scope.LicenseTypes[9].Licenses.push({ LicenseID: Docs[i].CDSCInformationID, LicenseName: "Central Drug Standard Control" + (i + 1), LicenseDocPath: Docs[i].CDSCCerificatePath, LicenseDocSize: Docs[i].CDSCCerificateSize, ModifiedDate: Docs[i].LastModifiedDate, Description: "Central Drug Standard Control" });

                if (Docs[i].CDSCInfoHistory != null) {

                    for (var j = 0; j < Docs[i].CDSCInfoHistory.length; j++) {

                        $scope.LicenseTypes[9].LicenseHistories.push({ LicenseID: Docs[i].CDSCInfoHistory[j].CDSCInfoHistoryID, LicenseName: "Central Drug Standard Control" + (j + 1), LicenseDocPath: Docs[i].CDSCInfoHistory[j].CDSCCerificatePath, removeDate: Docs[i].CDSCInfoHistory[j].LastModifiedDate });

                    }

                }

            }

        }
    }

    $scope.countUG = 0;
    $scope.countG = 0;

    $scope.FormatEducationDetailDoc = function (Docs) {

        $scope.Licenses = [];
        $scope.LicenseHistories = [];

        for (var i = 0; i < Docs.length; i++) {

            if (Docs[i].CertificatePath != null) {

                if (Docs[i].CertificatePath != null) {

                    if (Docs[i].QualificationType == "UnderGraduate") {

                        $scope.LicenseTypes[10].Licenses.push({ LicenseID: Docs[i].EducationDetailID, LicenseName: "Under Graduate" + ($scope.countUG + 1), LicenseDocPath: Docs[i].CertificatePath, LicenseDocSize: Docs[i].CertificateSize, ModifiedDate: Docs[i].LastModifiedDate, Description: "Under Graduate" });
                        $scope.countUG++;

                        if (Docs[i].EducationDetailHistory != null) {

                            $scope.LicenseTypes[10].LicenseHistories.push({ LicenseID: Docs[i].EducationDetailHistory.EducationDetailHistoryID, LicenseName: "Under Graduate" + ($scope.countUG + 1), LicenseDocPath: Docs[i].EducationDetailHistory.CertificatePath, removeDate: Docs[i].EducationDetailHistory.LastModifiedDate });

                        }

                    } else if (Docs[i].QualificationType == "Graduate") {

                        $scope.LicenseTypes[10].Licenses.push({ LicenseID: Docs[i].EducationDetailID, LicenseName: "Graduate" + ($scope.countG + 1), LicenseDocPath: Docs[i].CertificatePath, LicenseDocSize: Docs[i].CertificateSize, ModifiedDate: Docs[i].LastModifiedDate, Description: "Graduate" });
                        $scope.countG++;

                        if (Docs[i].EducationDetailHistory != null) {

                            $scope.LicenseTypes[10].LicenseHistories.push({ LicenseID: Docs[i].EducationDetailHistory.EducationDetailHistoryID, LicenseName: "Graduate" + ($scope.countG + 1), LicenseDocPath: Docs[i].EducationDetailHistory.CertificatePath, removeDate: Docs[i].EducationDetailHistory.LastModifiedDate });

                        }

                    }

                }

            }

        }
    }

    $scope.FormatECFMGDetailDoc = function (Docs) {

        $scope.Licenses = [];
        $scope.LicenseHistories = [];

        if (Docs.ECFMGCertPath != null) {

            $scope.LicenseTypes[11].Licenses.push({ LicenseID: Docs.ECFMGDetailID, LicenseName: "Education Commission for Foreign Medical Graduates", LicenseDocPath: Docs.ECFMGCertPath, LicenseDocSize: Docs.ECFMGCertSize, ModifiedDate: Docs.LastModifiedDate, Description: "Education Commission for Foreign Medical Graduates" });

            $scope.LicenseTypes[11].LicenseHistories.push({ LicenseID: 2, LicenseName: "Education Commission for Foreign Medical Graduates", LicenseDocPath: Docs.ECFMGCertPath, removeDate: Docs.LastModifiedDate });

        }
    }

    $scope.countIntern = 0;
    $scope.countFell = 0;
    $scope.countRes = 0;
    $scope.countOther = 0;

    $scope.FormatProgramDetailsDoc = function (Docs) {

        $scope.Licenses = [];
        $scope.LicenseHistories = [];

        for (var i = 0; i < Docs.length; i++) {

            if (Docs[i].DocumentPath != null) {

                if (Docs[i].ProgramType == "Internship") {

                    $scope.LicenseTypes[12].Licenses.push({ LicenseID: Docs[i].ProgramDetailID, LicenseName: "Internship" + ($scope.countIntern + 1), LicenseDocPath: Docs[i].DocumentPath, LicenseDocSize: Docs[i].DocumentSize, ModifiedDate: Docs[i].LastModifiedDate, Description: "Internship" });

                    $scope.countIntern++;

                    if (Docs.ProgramDetailHistory != null) {

                        $scope.LicenseTypes[12].LicenseHistories.push({ LicenseID: Docs[i].ProgramDetailHistory.ProgramDetailHistoryID, LicenseName: "Internship" + ($scope.countIntern + 1), LicenseDocPath: Docs[i].ProgramDetailHistory.DocumentPath, removeDate: Docs[i].ProgramDetailHistory.LastModifiedDate });

                    }

                } else if (Docs[i].ProgramType == "Fellowship") {

                    $scope.LicenseTypes[12].Licenses.push({ LicenseID: Docs[i].ProgramDetailID, LicenseName: "Fellowship" + ($scope.countFell + 1), LicenseDocPath: Docs[i].DocumentPath, LicenseDocSize: Docs[i].DocumentSize, ModifiedDate: Docs[i].LastModifiedDate, Description: "Fellowship" });

                    $scope.countFell++;

                    if (Docs.ProgramDetailHistory != null) {

                        $scope.LicenseTypes[12].LicenseHistories.push({ LicenseID: Docs[i].ProgramDetailHistory.ProgramDetailHistoryID, LicenseName: "Fellowship" + ($scope.countFell + 1), LicenseDocPath: Docs[i].ProgramDetailHistory.DocumentPath, removeDate: Docs[i].ProgramDetailHistory.LastModifiedDate });

                    }

                } else if (Docs[i].ProgramType == "Resident") {

                    $scope.LicenseTypes[12].Licenses.push({ LicenseID: Docs[i].ProgramDetailID, LicenseName: "Resident" + ($scope.countRes + 1), LicenseDocPath: Docs[i].DocumentPath, LicenseDocSize: Docs[i].DocumentSize, ModifiedDate: Docs[i].LastModifiedDate, Description: "Resident" });

                    $scope.countRes++;

                    if (Docs.ProgramDetailHistory != null) {

                        $scope.LicenseTypes[12].LicenseHistories.push({ LicenseID: Docs[i].ProgramDetailHistory.ProgramDetailHistoryID, LicenseName: "Resident" + ($scope.countRes + 1), LicenseDocPath: Docs[i].ProgramDetailHistory.DocumentPath, removeDate: Docs[i].ProgramDetailHistory.LastModifiedDate });

                    }

                } else if (Docs[i].ProgramType == "Other") {

                    $scope.LicenseTypes[12].Licenses.push({ LicenseID: Docs[i].ProgramDetailID, LicenseName: "Other" + ($scope.countOther + 1), LicenseDocPath: Docs[i].DocumentPath, LicenseDocSize: Docs[i].DocumentSize, ModifiedDate: Docs[i].LastModifiedDate, Description: "Other" });

                    $scope.countOther++;

                    if (Docs.ProgramDetailHistory != null) {

                        $scope.LicenseTypes[12].LicenseHistories.push({ LicenseID: Docs[i].ProgramDetailHistory.ProgramDetailHistoryID, LicenseName: "Other" + ($scope.countOther + 1), LicenseDocPath: Docs[i].ProgramDetailHistory.DocumentPath, removeDate: Docs[i].ProgramDetailHistory.LastModifiedDate });

                    }

                }

            }

        }
    }

    $scope.FormatCMECertificationsDoc = function (Docs) {

        $scope.Licenses = [];
        $scope.LicenseHistories = [];

        for (var i = 0; i < Docs.length; i++) {

            if (Docs[i].CertificatePath != null) {

                $scope.LicenseTypes[13].Licenses.push({ LicenseID: Docs[i].CMECertificationID, LicenseName: "Continuing Medical Education" + (i + 1), LicenseDocPath: Docs[i].CertificatePath, LicenseDocSize: Docs[i].CertificateSize, ModifiedDate: Docs[i].LastModifiedDate, Description: "Continuing Medical Education" });

                if (Docs[i].CMECertificationHistory != null) {

                    $scope.LicenseTypes[13].LicenseHistories.push({ LicenseID: Docs[i].CMECertificationHistory.CMECertificationHistoryID, LicenseName: "Continuing Medical Education" + (j + 1), LicenseDocPath: Docs[i].CMECertificationHistory.CertificatePath, removeDate: Docs[i].CMECertificationHistory.LastModifiedDate });

                }

            }

        }
    }

    $scope.FormatSpecialtyBoardCertifiedDetailDoc = function (Docs) {

        $scope.Licenses = [];
        $scope.LicenseHistories = [];

        for (var i = 0; i < Docs.length; i++) {

            if (Docs[i].SpecialtyBoardCertifiedDetail != null && Docs[i].SpecialtyBoardCertifiedDetail.BoardCertificatePath != null) {

                $scope.LicenseTypes[14].Licenses.push({ LicenseID: Docs[i].SpecialtyDetailID, LicenseName: "Specialty Board Certificate" + (i + 1), LicenseDocPath: Docs[i].SpecialtyBoardCertifiedDetail.BoardCertificatePath, LicenseDocSize: Docs[i].SpecialtyBoardCertifiedDetail.BoardCertificateSize, ModifiedDate: Docs[i].LastModifiedDate, Description: "Specialty Board Certificate" });

                if (Docs[i].SpecialtyBoardCertifiedDetail.SpecialtyBoardCertifiedDetailHistory != null) {

                    for (var j = 0; j < Docs[i].SpecialtyBoardCertifiedDetail.SpecialtyBoardCertifiedDetailHistory.length; j++) {

                        $scope.LicenseTypes[14].LicenseHistories.push({ LicenseID: Docs[i].SpecialtyBoardCertifiedDetail.SpecialtyBoardCertifiedDetailHistory[j].SpecialtyBoardCertifiedDetailHistoryID, LicenseName: "Specialty Board Certificate" + (j + 1), LicenseDocPath: Docs[i].SpecialtyBoardCertifiedDetail.SpecialtyBoardCertifiedDetailHistory[j].BoardCertificatePath, removeDate: Docs[i].SpecialtyBoardCertifiedDetail.SpecialtyBoardCertifiedDetailHistory[j].LastModifiedDate });

                    }

                }

            }

        }
    }

    $scope.FormatHospitalPrivilegeDetailDoc = function (Docs) {

        $scope.Licenses = [];
        $scope.LicenseHistories = [];
        var count = 1;

        for (var i = 0; i < Docs.length; i++) {

            if (Docs[i].HospitalPrevilegeLetterPath != null && Docs[i].Status != "Inactive") {

                $scope.LicenseTypes[15].Licenses.push({ LicenseID: Docs[i].HospitalPrivilegeDetailID, LicenseName: "Hospital Privilege Document" + (count), LicenseDocPath: Docs[i].HospitalPrevilegeLetterPath, LicenseDocSize: Docs[i].HospitalPrevilegeLetterSize, ModifiedDate: Docs[i].LastModifiedDate, Description: "Hospital Privilege Document" });

                if (Docs[i].HospitalPrivilegeDetailHistory != null) {

                    for (var j = 0; j < Docs[i].HospitalPrivilegeDetailHistory.length; j++) {

                        $scope.LicenseTypes[15].LicenseHistories.push({ LicenseID: Docs[i].HospitalPrivilegeDetailHistory[j].HospitalPrivilegeDetailHistoryID, LicenseName: "Hospital Privilege Document" + (j + 1), LicenseDocPath: Docs[i].HospitalPrivilegeDetailHistory[j].HospitalPrevilegeLetterPath, removeDate: Docs[i].HospitalPrivilegeDetailHistory[j].LastModifiedDate });

                    }

                }

                count++;

            }

        }
    }

    $scope.FormatProfessionalLiabilityInfoDoc = function (Docs) {

        $scope.Licenses = [];
        $scope.LicenseHistories = [];

        for (var i = 0; i < Docs.length; i++) {

            if (Docs[i].InsuranceCertificatePath != null) {

                $scope.LicenseTypes[16].Licenses.push({ LicenseID: Docs[i].ProfessionalLiabilityInfoID, LicenseName: "Professional Liability Certificate" + (i + 1), LicenseDocPath: Docs[i].InsuranceCertificatePath, LicenseDocSize: Docs[i].InsuranceCertificateSize, ModifiedDate: Docs[i].LastModifiedDate, Description: "Professional Liability Document" });

                if (Docs[i].ProfessionalLiabilityInfoHistory != null) {

                    for (var j = 0; j < Docs[i].ProfessionalLiabilityInfoHistory.length; j++) {

                        $scope.LicenseTypes[16].LicenseHistories.push({ LicenseID: Docs[i].ProfessionalLiabilityInfoHistory[j].ProfessionalLiabilityInfoHistoryID, LicenseName: "Professional Liability Certificate" + (j + 1), LicenseDocPath: Docs[i].ProfessionalLiabilityInfoHistory[j].InsuranceCertificatePath, removeDate: Docs[i].ProfessionalLiabilityInfoHistory[j].LastModifiedDate });

                    }

                }

            }

        }
    }

    $scope.FormatProfessionalWorkExperienceDoc = function (Docs) {

        $scope.Licenses = [];
        $scope.LicenseHistories = [];

        for (var i = 0; i < Docs.length; i++) {

            if (Docs[i].WorkExperienceDocPath != null) {

                $scope.LicenseTypes[17].Licenses.push({ LicenseID: Docs[i].ProfessionalWorkExperienceID, LicenseName: "Work Experience Document" + (i + 1), LicenseDocPath: Docs[i].WorkExperienceDocPath, LicenseDocSize: Docs[i].WorkExperienceDocSize, ModifiedDate: Docs[i].LastModifiedDate, Description: "Work Experience Document" });

                if (Docs[i].ProfessionalWorkExperienceHistory != null) {

                    $scope.LicenseTypes[17].LicenseHistories.push({ LicenseID: Docs[i].ProfessionalWorkExperienceHistory.ProfessionalWorkExperienceHistoryID, LicenseName: "Work Experience Document" + (j + 1), LicenseDocPath: Docs[i].ProfessionalWorkExperienceHistory.WorkExperienceDocPath, removeDate: Docs[i].ProfessionalWorkExperienceHistory.LastModifiedDate });

                }

            }

        }
    }

    $scope.FormatContractInfoeDoc = function (Docs) {

        $scope.Licenses = [];
        $scope.LicenseHistories = [];

        for (var i = 0; i < Docs.length; i++) {

            if (Docs[i].ContractFilePath != null) {

                $scope.LicenseTypes[18].Licenses.push({ LicenseID: Docs[i].ContractInfoID, LicenseName: "Contract Document", LicenseDocPath: Docs[i].ContractFilePath, LicenseDocSize: Docs[i].ContractFileSize, ModifiedDate: Docs[i].LastModifiedDate, Description: "Contract Document" });

            }

        }
    }

    $scope.FormatOtherDocument = function (Docs) {

        $scope.Licenses = [];
        $scope.LicenseHistories = [];

        for (var i = 0; i < Docs.length; i++) {


            if (Docs[i].DocumentPath != null) {

                $scope.LicenseTypes[19].Licenses.push({ LicenseID: Docs[i].OtherDocumentID, LicenseName: Docs[i].Title, LicenseDocPath: Docs[i].DocumentPath, LicenseDocSize: Docs[i].DocumentSize, ModifiedDate: Docs[i].LastModifiedDate, Private: Docs[i].IsPrivate, Description: "Other Document" });

            }

        }
    }

    $scope.FormatPlanFormDocument = function (Docs) {

        $scope.Licenses = [];
        $scope.LicenseHistories = [];
        if (Docs.length > 0) {

            $scope.LicenseTypes[20].Licenses.push({ LicenseID: Docs[Docs.length - 1].OtherDocumentID, LicenseName: Docs[Docs.length - 1].Title, LicenseDocPath: Docs[Docs.length - 1].DocumentPath, LicenseDocSize: Docs[Docs.length - 1].DocumentSize, ModifiedDate: Docs[Docs.length - 1].LastModifiedDate, Private: Docs[Docs.length - 1].IsPrivate, Description: "Plan Form Document" });
        }

        //for (var i = 0; i < Docs.length; i++) {

        //    if (Docs[i].DocumentPath != null) {

        //        $scope.LicenseTypes[20].Licenses.push({ LicenseID: Docs[i].OtherDocumentID, LicenseName: Docs[i].Title, LicenseDocPath: Docs[i].DocumentPath, ModifiedDate: Docs[i].LastModifiedDate, Private: Docs[i].IsPrivate, Description: "Plan Form Document" });

        //    }

        //}
    }

    $scope.FormatProfileVerificationInfo = function (Docs) {

        //$scope.Licenses = [];
        $scope.SLicenses = [];
        var count = 1;
        var scount = 1;

        for (var i = 0; i < Docs.length; i++) {

            if (Docs[i].ProfileVerificationDetails != null) {

                for (var j = 0; j < Docs[i].ProfileVerificationDetails.length; j++) {

                    if (Docs[i].ProfileVerificationDetails[j].VerificationResult != null && Docs[i].ProfileVerificationDetails[j].ProfileVerificationParameter != null) {

                        if (Docs[i].ProfileVerificationDetails[j].VerificationResult.VerificationDocumentPath != null) {

                            //if (Docs[i].ProfileVerificationDetails[j].ProfileVerificationParameter.Code == "MOPT" || Docs[i].ProfileVerificationDetails[j].ProfileVerificationParameter.Code == "OIG") {
                            //    $scope.SLicenses.push({ LicenseID: Docs[i].ProfileVerificationDetails[j].ProfileVerificationDetailId, LicenseName: "PSV" + (scount), Title: Docs[i].ProfileVerificationDetails[j].ProfileVerificationParameter.Title, LicenseDocPath: Docs[i].ProfileVerificationDetails[j].VerificationResult.VerificationDocumentPath, VerificationDate: Docs[i].ProfileVerificationDetails[j].VerificationDate });
                            //    scount++;
                            //} else {

                            //    $scope.Licenses.push({ LicenseID: Docs[i].ProfileVerificationDetails[j].ProfileVerificationDetailId, LicenseName: "PSV" + (count), Title: Docs[i].ProfileVerificationDetails[j].ProfileVerificationParameter.Title, LicenseDocPath: Docs[i].ProfileVerificationDetails[j].VerificationResult.VerificationDocumentPath, VerificationDate: Docs[i].ProfileVerificationDetails[j].VerificationDate });
                            //    count++;
                            //}

                            //$scope.LicenseTypes[20].Licenses.push({ LicenseID: Docs[i].ProfileVerificationDetails[j].ProfileVerificationDetailId, LicenseName: Docs[i].Title, LicenseDocPath: Docs[i].DocumentPath, ModifiedDate: Docs[i].LastModifiedDate, Private: Docs[i].IsPrivate, Description: "Other Document" });
                            $scope.LicenseTypes[21].Licenses.push({ LicenseID: Docs[i].ProfileVerificationDetails[j].ProfileVerificationDetailId, LicenseName: "PSV" + " - " + Docs[i].ProfileVerificationDetails[j].ProfileVerificationParameter.Title, LicenseDocPath: Docs[i].ProfileVerificationDetails[j].VerificationResult.VerificationDocumentPath, LicenseDocSize: Docs[i].ProfileVerificationDetails[j].VerificationResult.VerificationDocumentSize, ModifiedDate: Docs[i].LastModifiedDate, Private: Docs[i].IsPrivate, Description: "PSV Document" });
                            count++;

                        }

                    }

                }

            }

        }


        //$scope.PSVLicenses = { LicenseTypeID: 20, LicenseTypeName: "PSV Document", Licenses: $scope.Licenses };

        //}

    }

    $scope.copyspaDoc = [];
    $scope.combine = function (license) {

        for (var i = 0; i < $scope.LicenseTypes.length; i++) {

            for (var j = 0; j < $scope.LicenseTypes[i].Licenses.length; j++) {

                $scope.spaDoc.push($scope.LicenseTypes[i].Licenses[j]);

            }

        }

        $scope.copyspaDoc = angular.copy($scope.spaDoc);

    }

    $scope.countDoc = 0;
    $scope.selectedDocument = [];
    $scope.selectedForms = [];
    $scope.selectedProviders = [];

    $scope.SelectProviders = function (Doc, index) {

        //if (Doc.IsCkecked == true) {
        //    $scope.selectedProviders = Doc;
        //    for (var i = 0; i < $scope.allProviders.length; i++) {
        //        if (i != index) {

        //            $scope.allProviders[i].IsCkecked = false;
        //        }
        //    }
        //}
        //else {
        //    $scope.selectedProviders = null;
        //}
        $scope.isSelectAll = false;
        $scope.countDoc = 0;
        if (Doc.IsCkecked == true) {
            $scope.selectedProviders.push(Doc);
        }
        else {
            var RemovedItem = $scope.selectedProviders.indexOf(Doc) > -1 ? $scope.selectedProviders.splice($scope.selectedProviders.indexOf(Doc), 1) : '';
            //$scope.selectedProviders.splice(index, 1);
        }
    }

    $scope.SelectForms = function (Doc, index) {
        var idx = $scope.selectedForms.indexOf(Doc);
        if (idx > -1) {
            $scope.selectedForms.splice(idx, 1);
        }
        else {
            $scope.selectedForms.push(Doc);
        }
        //$scope.selectedForms.push(Doc);
        //alert($scope.selectedForms);

    }
    $scope.selectAllForms = function () {
        $scope.selectedForms = angular.copy($scope.pdfTemplateList);
        for (var i = 0; i < $scope.pdfTemplateList.length; i++) {
            $scope.pdfTemplateList[i].IsCkecked = true;
        }
    }

    $scope.SelectedDoc = function (Doc, index) {

        var isPresent = false;
        var tempObj = {};
        var flag = 0;
        for (var i = 0; i < $scope.selectedDocument.length; i++) {
            if ($scope.selectedDocument[i].LicenseID == Doc.LicenseID) {
                flag = 1;
                $scope.selectedDocument.splice(i, 1);
                break;
            }
        }

        if (flag == 0) {
            $scope.selectedDocument.push(Doc);
        }

        //alert(Doc.LicenseDocPath);
        //alert($scope.selectedDocument);

        if ($scope.spaDoc[index].Status == true) {

            tempObj = $scope.spaDoc[index];

            for (var i = index; i > $scope.countDoc; i--) {

                $scope.spaDoc[i] = $scope.spaDoc[i - 1];

            }

            $scope.spaDoc[$scope.countDoc] = tempObj;

            $scope.countDoc++;

        } else {

            tempObj = $scope.spaDoc[index];

            for (var i = index; i < $scope.countDoc - 1; i++) {

                $scope.spaDoc[i] = $scope.spaDoc[i + 1];

            }

            $scope.spaDoc[$scope.countDoc - 1] = tempObj;

            $scope.countDoc--;

        }
        var divID = '#topTable';
        $('html, body').animate({
            scrollTop: $(divID).offset().top
        }, 10);

    }

    $scope.isSelectAll = false;

    $scope.selectAll = function () {

        if (!$scope.isSelectAll) {

            for (var i = 0; i < $scope.spaDoc.length; i++) {

                $scope.spaDoc[i].Status = true;

            }
            $scope.selectedDocument = angular.copy($scope.spaDoc);
            //alert($scope.selectedDocument);
            $scope.isSelectAll = true;

            $scope.countDoc = $scope.spaDoc.length;

        } else {

            for (var i = 0; i < $scope.spaDoc.length; i++) {

                $scope.spaDoc[i].Status = false;

            }

            $scope.isSelectAll = false;

            $scope.countDoc = 0;
            $scope.selectedDocument = [];
        }

    }

    $scope.tempObj = {};
    $scope.moveDocumentUp = function (index) {

        $scope.tempObj = $scope.spaDoc[index];
        $scope.spaDoc[index] = $scope.spaDoc[index - 1];
        $scope.spaDoc[index - 1] = $scope.tempObj;
        $scope.tempObj = {};
        $scope.tempObj = $scope.selectedDocument[index];
        $scope.selectedDocument[index] = $scope.selectedDocument[index - 1];
        $scope.selectedDocument[index - 1] = $scope.tempObj;
        $scope.tempObj = {};

    }

    $scope.moveDocumentDown = function (index) {

        $scope.tempObj = $scope.spaDoc[index];
        $scope.spaDoc[index] = $scope.spaDoc[index + 1];
        $scope.spaDoc[index + 1] = $scope.tempObj;

        $scope.tempObj = {};

        $scope.tempObj = $scope.selectedDocument[index];
        $scope.selectedDocument[index] = $scope.selectedDocument[index + 1];
        $scope.selectedDocument[index + 1] = $scope.tempObj;

        $scope.tempObj = {};
    }

    $scope.generationType = null;


    //$scope.pdfTemplateList = [
    //    {
    //        Name: "GHI HOSPITALIST FORM",
    //        FileName: "GHI_HOSPITALIST_FORM",
    //        IsCkecked: false,
    //        IsLoading: false
    //    },
    //    {
    //        Name: "GHI PROVIDER FORM",
    //        FileName: "GHI_Provider_Form",
    //        IsCkecked: false,
    //        IsLoading: false
    //    },
    //    {
    //        Name: "AETNA - NEW PROVIDER CREDENTIALING FORM",
    //        FileName: "AETNACOVENTRYTemplate",
    //        IsCkecked: false,
    //        IsLoading: false
    //    },
    //    {
    //        Name: "ATPL 2016",
    //        FileName: "ATPL2016",
    //        IsCkecked: false,
    //        IsLoading: false
    //    },
    //    {
    //        Name: "TRICARE PROVIDER APPLICATION - SOUTH",
    //        FileName: "TRICARE_PROVIDER_APPLICATION_SOUTH",
    //        IsCkecked: false,
    //        IsLoading: false
    //    },
    //    {
    //        Name: "TRICARE PROVIDER APPLICATION",
    //        FileName: "TRICARE_PROVIDER_APPLICATION",
    //        IsCkecked: false,
    //        IsLoading: false
    //    },
    //    {
    //        Name: "LETTER OF INTENT",
    //        FileName: "Letter_of_Intent",
    //        IsCkecked: false,
    //        IsLoading: false
    //    },
    //    {
    //        Name: "TRICARE PA APPLICATION - SOUTH",
    //        FileName: "TRICARE_PA_APPLICATION_SOUTH",
    //        IsCkecked: false,
    //        IsLoading: false
    //    },
    //    {
    //        Name: "TRICARE PA APPLICATION",
    //        FileName: "TRICARE_PA_APPLICATION",
    //        IsCkecked: false,
    //        IsLoading: false
    //    },
    //    {
    //        Name: "ALLIED CREDENTIALING APPLICATION ACCESS2",
    //        FileName: "ALLIED_CREDENTIALING_APPLICATION_ACCESS2",
    //        IsCkecked: false,
    //        IsLoading: false
    //    },
    //    {
    //        Name: "ALLIED CREDENTIALING APPLICATION ACCESS",
    //        FileName: "ALLIED_CREDENTIALING_APPLICATION_ACCESS",
    //        IsCkecked: false,
    //        IsLoading: false
    //    },
    //    //{
    //    //    Name: "ULTIMATE CREDENTIALING APPLICATION PRACTIONER FOR Specialist 2015",
    //    //    FileName: "Ultimate_Credentialing_Application_Practitioner_Specialist_2015",
    //    //    IsCkecked: false,
    //    //    IsLoading: false
    //    //},
    //    //{
    //    //    Name: "ULTIMATE CREDENTIALING APPLICATION PRACTIONER FOR PCP 2015",
    //    //    FileName: "Ultimate_Credentialing_Application_Practitioner_PCP_2015",
    //    //    IsCkecked: false,
    //    //    IsLoading: false
    //    //},
    //    //{
    //    //    Name: "ULTIMATE CREDENTIALING APPLICATION PRACTIONER FOR MIDLEVEL 2015",
    //    //    FileName: "Ultimate_Credentialing_Application_Practitioner_Midlevel_2015",
    //    //    IsCkecked: false,
    //    //    IsLoading: false
    //    //},
    //    {
    //        Name: "ULTIMATE CREDENTIALING APPLICATION",
    //        FileName: "Ultimate_Credentialing_Application",
    //        IsCkecked: false,
    //        IsLoading: false
    //    },
    //    {
    //        Name: "ULTIMATE RE-CREDENTIALING APPLICATION",
    //        FileName: "Ultimate_Re-Credentialing_Application",
    //        IsCkecked: false,
    //        IsLoading: false
    //    },
    //    {
    //        Name: "BCBS PAYMENT AUTH FORM",
    //        FileName: "BCBS_PAYMENT_AUTH_FORM",
    //        IsCkecked: false,
    //        IsLoading: false
    //    },
    //    {
    //        Name: "BCBS PROVIDER UPDATE FORM",
    //        FileName: "BCBS_PROVIDER_UPDATE_FORM",
    //        IsCkecked: false,
    //        IsLoading: false,
    //        IsLoading: false
    //    }, {
    //        Name: "BCBS PROVIDER REGISTRATION FORM",
    //        FileName: "BCBS_PROVIDER_REGISTRATION_FORM",
    //        IsCkecked: false,
    //        IsLoading: false
    //    },
    //    {
    //        Name: "MEDICAID GROUP MEMBERSHIP AUTHORIZATION FORM",
    //        FileName: "MEDICAID_GROUP_MEMBERSHIP_AUTHORIZATION_FORM",
    //        IsCkecked: false,
    //        IsLoading: false
    //    },
    //    //{
    //    //    Name: "MEDICAID MCO TREAT PROVIDER ATTESTATION FORM",
    //    //    FileName: "MEDICAID_MCO_TREAT_PROVIDER_ATTESTATION",
    //    //    IsCkecked: false,
    //    //    IsLoading: false
    //    //},
    //    {
    //        Name: "TRICARE ARNP APPLICATION FORM - SOUTH",
    //        FileName: "TRICARE_ARNP_APPLICATION_FORM_SOUTH",
    //        IsCkecked: false,
    //        IsLoading: false
    //    },
    //    {
    //        Name: "TRICARE ARNP APPLICATION FORM",
    //        FileName: "TRICARE_ARNP_APPLICATION_FORM",
    //        IsCkecked: false,
    //        IsLoading: false
    //    },
    //    {
    //        Name: "FL INSURENCE PROVIDER ATTESTATION",
    //        FileName: "FL_INSURANCE_PROVIDER_ATTESTATION",
    //        IsCkecked: false,
    //        IsLoading: false
    //    },
    //    {
    //        Name: "Freedom Optimum IPA",
    //        FileName: "Freedom_Optimum_IPA_Enrollment_Provider_PCP",
    //        IsCkecked: false,
    //        IsLoading: false
    //    },
    //    {
    //        Name: "Freedom Optimum Specialist",
    //        FileName: "Freedom_Optimum_Specialist_Package",
    //        IsCkecked: false,
    //        IsLoading: false
    //    },
    //    {
    //        Name: "Humana IPA",
    //        FileName: "Humana_IPA_New_PCP_Package",
    //        IsCkecked: false,
    //        IsLoading: false
    //    },
    //    {
    //        Name: "Humana Specialist",
    //        FileName: "Humana_Specialist_New_Provider",
    //        IsCkecked: false,
    //        IsLoading: false
    //    },
    //    {
    //        Name: "FL HOSPITAL ADMIT ATTESTATION",
    //        FileName: "FL_HOSPITAL_ADMIT_ATTESTATION",
    //        IsCkecked: false,
    //        IsLoading: false
    //    },
    //    //{
    //    //    Name: "FL FINANCIAL RESPONSIBILITY STATEMENT GLOBAL",
    //    //    FileName: "FL_FINANCIAL_RESPONSIBILITY_STATEMENT_GLOBAL",
    //    //    IsCkecked: false,
    //    //    IsLoading: false
    //    //},
    //    {
    //        Name: "FL 3000 PCP ATTESTATION OF PATIENT LOAD 2016 GLOBAL",
    //        FileName: "FL_3000_PCP_Attestation_of_Patient_Load_2016_Global",
    //        IsCkecked: false,
    //        IsLoading: false
    //    },
    //    //{
    //    //    Name: "PHYSICIAN CREDENTIALING APPLICATION & DISCLOSURE OF OWNERSHIP FORM",
    //    //    FileName: "PHYSICIAN_CREDENTIALING_APPLICATION_DISCLOSURE_OF_OWNERSHIP"
    //    //},
    //    {
    //        Name: "WELLCARE MIDLEVEL FORM",
    //        FileName: "WELLCARE_MIDLEVEL_FORMS",
    //        IsCkecked: false,
    //        IsLoading: false
    //    },
    //    //{
    //    //    Name: "ULTIMATE  RE-CREDENTIALING APPLICATION PRACTIONER FOR Specialist 2015",
    //    //    FileName: "Ultimate_Re-Credentialing_Application_Practitioner_Specialist_2015",
    //    //    IsCkecked: false,
    //    //    IsLoading: false
    //    //},
    //    //{
    //    //    Name: "ULTIMATE RE-CREDENTIALING APPLICATION PRACTIONER FOR PCP 2015",
    //    //    FileName: "Ultimate_Re-Credentialing_Application_Practitioner_PCP_2015",
    //    //    IsCkecked: false,
    //    //    IsLoading: false
    //    //},
    //    //{
    //    //    Name: "ULTIMATE RE-CREDENTIALING APPLICATION PRACTIONER FOR MIDLEVEL 2015",
    //    //    FileName: "Ultimate_Re-Credentialing_Application_Practitioner_Midlevel_2015",
    //    //    IsCkecked: false,
    //    //    IsLoading: false
    //    //},
    //    {
    //        Name: "ADMITING ARRANGEMENT FORM",
    //        FileName: "Admitting_Arrangement_Form",
    //        IsCkecked: false,
    //        IsLoading: false
    //    },
    //    {
    //        Name: "ATTESTATION OF SITE VISIT",
    //        FileName: "ATTESTATION_OF_SITE_VISIT",
    //        IsCkecked: false,
    //        IsLoading: false
    //    },
    //    //,
    //    //{
    //    //    Name: "UNITY JOINDER",
    //    //    FileName: "Unity_Joinder",
    //    //    IsCkecked: false,
    //    //    IsLoading: false
    //    //}
    //    {
    //        Name: "AHC PROVIDER PROFILE FOR WELLCARE FORM",
    //        FileName: "AHC_Provider_Profile_for_Wellcare",
    //        IsCkecked: false,
    //        IsLoading: false
    //    },
    //    {
    //        Name: "A2HC PROVIDER PROFILE FOR WELLCARE FORM",
    //        FileName: "A2HC_Provider_Profile_for_Wellcare",
    //        IsCkecked: false,
    //        IsLoading: false
    //    },
    //    {
    //        Name: "TRICARE PRIME CREDENTIALING-Primecare",
    //        FileName: "Tricare_Prime_Credentialing_Application",
    //        IsCkecked: false,
    //        IsLoading: false
    //    },
    //    {
    //        Name: "LOI FORM 2016",
    //        FileName: "LOI_Template_2016",
    //        IsCkecked: false,
    //        IsLoading: false
    //    },
    //     {
    //         Name: "STANDARD AUTHORIZATION, ATTESTATION AND RELEASE",
    //         FileName: "Authorization_Attestation_and_Release",
    //         IsCkecked: false,
    //         IsLoading: false
    //     },
    //    {

    //        Name: "TRICARE CLINICAL PSYCHOLOGIST PROVIDER APPLICATION-Primecare",
    //        FileName: "TRICARE_CLINICAL_PSYCHOLOGIST_PROVIDER_APPLICATION",
    //        IsCkecked: false,
    //        IsLoading: false
    //    },
    //    {

    //        Name: "TRICARE CLINICAL SOCIAL WORKER PROVIDER APPLICATION-Primecare",
    //        FileName: "TRICARE_CLINICAL_SOCIAL_WORKER_PROVIDER_APPLICATION",
    //        IsCkecked: false,
    //        IsLoading: false
    //    },
    //    {
    //        Name: "TRICARE PHYSICIAN / DENTIST PROVIDER APPLICATION-Primecare",
    //        FileName: "TRICARE_PHYSICIAN_DENTIST_PROVIDER_APPLICATION",
    //        IsCkecked: false,
    //        IsLoading: false
    //    },
    //    {

    //        Name: "TRICARE PHYSICIAN ASSISTANT PROVIDER APPLICATION-Primecare",
    //        FileName: "TRICARE_PHYSICIAN_ASSISTANT_PROVIDER_APPLICATION",
    //        IsCkecked: false,
    //        IsLoading: false
    //    },
    //    {
    //        Name: "TRICARE RN LPN NP PROVIDER APPLICATION-Primecare",
    //        FileName: "TRICARE_RN_LPN_NP_PROVIDER_APPLICATION",
    //        IsCkecked: false,
    //        IsLoading: false
    //    },
    //    {
    //        Name: "ELECTRONIC FUNDS TRANSFER AUTHORIZATION AGREEMENT-Primecare",
    //        FileName: "ELECTRONIC_FUNDS_TRANSFER_AUTHORIZATION_AGREEMENT",
    //        IsCkecked: false,
    //        IsLoading: false
    //    },
    //     {
    //         Name: "NON-NETWORK TRICARE PROVIDER FILE GROUP APPLICATION-Primecare",
    //         FileName: "NON-NETWORK_TRICARE_PROVIDER_FILE_GROUP_APPLICATION",
    //         IsCkecked: false,
    //         IsLoading: false
    //     },
    //    {
    //        Name: "CAREFIRST CAQH FORM-Primecare",
    //        FileName: "CAREFIRST_CAQH_FORM",
    //        IsCkecked: false,
    //        IsLoading: false

    //    },
    //    {
    //        Name: "NPI REGISTRATION FORM-Primecare",
    //        FileName: "NPI_REGISTRATION_FORM",
    //        IsCkecked: false,
    //        IsLoading: false
    //    },
    //     {
    //         Name: "CAREFIRST PRACTICE QUESTIONNAIRE-Primecare",
    //         FileName: "CAREFIRST_PRACTICE_QUESTIONNAIRE",
    //         IsCkecked: false,
    //         IsLoading: false
    //     },
    //      {

    //          Name: "CONFIDENTIAL PROTECTED PEER REVIEW DOCUMENT",
    //          FileName: "CONFIDENTIAL_PROTECTED_PEER_REVIEW_DOCUMENT",
    //          IsCkecked: false,
    //          IsLoading: false
    //      },
    //       {
    //           Name: "HCA-REQUEST FOR CONSIDERATION",
    //           FileName: "HCA-REQUEST_FOR_CONSIDERATION",
    //           IsCkecked: false,
    //           IsLoading: false
    //       },
    //       {
    //           Name: "HCA-PINELLAS MARKET BYLAWS AGREEMENT",
    //           FileName: "HCA-PINELLAS_MARKET_BYLAWS_AGREEMENT",
    //           IsCkecked: false,
    //           IsLoading: false

    //       },
    //       {
    //           Name: "PROVIDER SIGNATURE STATEMENT",
    //           FileName: "PROVIDER_SIGNATURE_STATEMENT",
    //           IsCkecked: false,
    //           IsLoading: false
    //       },
    //       {
    //           Name: "OHH REFLEX TESTING ACKNOWLEDGEMENT",
    //           FileName: "OHH_REFLEX_TESTING_ACKNOWLEDGEMENT",
    //           IsCkecked: false,
    //           IsLoading: false
    //       },

    //       {
    //           Name: "LARGO MEDICAL CENTER REFLEX TESTING ACKNOWLEDGEMENT",
    //           FileName: "LARGO_MEDICAL_CENTER_REFLEX_TESTING_ACKNOWLEDGEMENT",
    //           IsCkecked: false,
    //           IsLoading: false
    //       },
    //       {
    //           Name: "NORTHSIDE HOSPITAL REFLEX TESTING ACKNOWLEDGEMENT",
    //           FileName: "NORTHSIDE_HOSPITAL_REFLEX_TESTING_ACKNOWLEDGEMENT",
    //           IsCkecked: false,
    //           IsLoading: false
    //       },
    //       {

    //         Name: "ST. PETE GENERAL HOSPITAL REFLEX TESTING ACKNOWLEDGEMENT",
    //        FileName: "ST_PETE_GENERAL_HOSPITAL_REFLEX_TESTING_ACKNOWLEDGEMENT",
    //           IsCkecked: false,
    //           IsLoading: false
    //       },
    //       {
    //           Name: "NORTHSIDE HOSPITAL ACKNOWLEDGMENT CARD",
    //           FileName: "NORTHSIDE_HOSPITAL_ACKNOWLEDGMENT_CARD",
    //           IsCkecked: false,
    //           IsLoading: false
    //       },
    //       {
    //           Name: "NORTHSIDE HOSPITAL CODE OF CONDUCT AGREEMENT",
    //           FileName: "NORTHSIDE_HOSPITAL_CODE_OF_CONDUCT_AGREEMENT",
    //           IsCkecked: false,
    //           IsLoading: false
    //       },
    //       {
    //           Name: "NORTHSIDE HOSPITAL HAND HYGIENE",
    //           FileName: "NORTHSIDE_HOSPITAL_HAND_HYGIENE",
    //           IsCkecked: false,
    //           IsLoading: false
    //       },
    //       {
    //           Name: "LARGO MEDICAL CENTER MEDICAL STAFF COVERAGE AGREEMENT",
    //           FileName: "LARGO_MEDICAL_CENTER_MEDICAL_STAFF_COVERAGE_AGREEMENT",
    //           IsCkecked: false,
    //           IsLoading: false
    //       },
    //       {
    //           Name: "BAYFRONT-ALLIED PRE APPLICATION",
    //           FileName: "BAYFRONT-ALLIED_PRE_APPLICATION",
    //           IsCkecked: false,
    //           IsLoading: false
    //       },
    //       {
    //           Name: "BAYFRONT-PHYSICIAN PRE APPLICATION",
    //           FileName: "BAYFRONT-PHYSICIAN_PRE_APPLICATION",
    //           IsCkecked: false,
    //           IsLoading: false
    //       },
    //       {
    //           Name: "FLORIDA FINANCIAL RESPONSIBILITY FORM",
    //           FileName: "FLORIDA_FINANCIAL_RESPONSIBILITY_FORM",
    //           IsCkecked: false,
    //           IsLoading: false
    //       },
    //       {
    //           Name: "BCBS NC RELEASE ATTESTATION STATEMENT",
    //           FileName: "BCBS_NC_RELEASE_ATTESTATION_STATEMENT",
    //           IsCkecked: false,
    //           IsLoading: false
    //       },
    //       {
    //           Name: "FEDERAL COMMUNICATIONS COMMISSION ATTESTATION STATEMENT-Primecare",
    //           FileName: "FEDERAL_COMMUNICATIONS_COMMISSION_ATTESTATION_STATEMENT",
    //           IsCkecked: false,
    //           IsLoading: false
    //       }
    //       ,
    //       {
    //           Name: "FIRST CAROLINA CARE PROVIDER INFORMATION CHANGE FORM-Primecare",
    //           FileName: "FIRST_CAROLINA_CARE_PROVIDER_INFORMATION_CHANGE_FORM",
    //           IsCkecked: false,
    //           IsLoading: false
    //       }
    //       ,
    //       {
    //           Name: "NORTH CAROLINA COVENTRY UNIFORM CREDENTIALING APPLICATION-Primecare",
    //           FileName: "North_Carolina_Coventry_Uniform_Credentialing_Application",
    //           IsCkecked: false,
    //           IsLoading: false

    //       }
    //];
    $scope.createdForms = [];
    $scope.showFormDiv = false;
    $scope.showGeneratedForm = [];
    $scope.generatedPdfTemplateList = [];
    $scope.loadFormAjax = false;
    var cnt = 0;

    $scope.ViewTemplate = function (ids, templateNames) {

        $scope.pdfLoadingAjax = true;
        $scope.isError = false;
        //$http.get(rootDir + '/Credentialing/PDFMapping/GenerateBulkPlanForm?profileIds=' + ids + '&templateName=' + templateNames).
        $http({
            url: rootDir + '/Credentialing/PDFMapping/GeneratePlanForm',
            method: "GET",
            params: { profileIds: ids, templateNames: templateNames }
        }).
        success(function (data) {

            try {

                for (var i = 0; i < data.length; i++) {

                    data[i].FirstName = $scope.selectedProviders.filter(function (provider) { return provider.ProfileID == data[i].ProfileID })[0].FirstName;
                    data[i].LastName = $scope.selectedProviders.filter(function (provider) { return provider.ProfileID == data[i].ProfileID })[0].LastName;
                }
                $scope.showGeneratedForm = angular.copy(data);
                $scope.packageLoading = false;
                $scope.showFormDiv = true;
                $scope.createdForms = $scope.selectedForms;
                $scope.selectedForms = [];
                for (var i = 0; i < $scope.pdfTemplateList.length; i++) {

                    $scope.pdfTemplateList[i].IsCkecked = false;
                }

                $('#Provider').show();
                $('#SearchProviderResultPanel2').slideUp();
                $('#SearchProviderResultPanel3').slideDown();
                $('#SearchProviderResultPanel1').slideDown();
                messageAlertEngine.callAlertMessage('formGenerated', "Forms are generated successfully.", "success", true);

                $scope.pdfLoadingAjax = false;
            } catch (e) {

            }
        }).error(function () {
            $('#Provider').show();
            $scope.packageLoading = false;
            $scope.selectedForms = [];
            for (var i = 0; i < $scope.pdfTemplateList.length; i++) {

                $scope.pdfTemplateList[i].IsCkecked = false;
            }
            messageAlertEngine.callAlertMessage('formGeneratedError', "An Error occurred !! Please Try Again !!", "danger", true);

        });
    };


    $scope.tempObject.seletedPDFName = "";
    $scope.tempObject.seletedPDFPayerName = "";
    $scope.tempObject.seletedPDFStateName = "";



    $scope.searchCumDropDownforSelectPayer = function (divId) {
        if ($scope.tempObject.seletedPDFStateName == "" || $scope.tempObject.seletedPDFStateName == undefined) {
            $scope.NewpdfTemplateList = $scope.tempPdfFileList;
        }
        else {
            $scope.NewpdfTemplateList = [];
            for (var i = 0 ; i < $scope.tempPdfFileList.length ; i++) {
                if ($scope.tempObject.seletedPDFStateName == $scope.tempPdfFileList[i].Region) {
                    $scope.NewpdfTemplateList.push($scope.tempPdfFileList[i]);
                }
            }
        }
        $("#" + divId).show();
        $("#ForTypePlanState").hide();
    };

    //---------Code to get the selected option of Payer-------//

    $scope.addIntoTypePdfDropDown1 = function (payer, div) {
        $scope.tempObject.seletedPDFPayerName = payer.Payer;

        $scope.NewpdfTemplateList = [];
        if ($scope.tempObject.seletedPDFPayerName == "" || $scope.tempObject.seletedPDFPayerName == undefined) {
            $scope.tempObject.seletedPDFName = "";
            $scope.tempObject.selectedFileName = "";
        }
        else {
            if ($scope.tempObject.seletedPDFStateName == "" || $scope.tempObject.seletedPDFStateName == undefined) {
                $scope.NewpdfTemplateList = [];
                for (var i = 0 ; i < $scope.tempPdfFileList.length ; i++) {
                    if ($scope.tempObject.seletedPDFPayerName == $scope.tempPdfFileList[i].Payer) {
                        $scope.NewpdfTemplateList.push($scope.tempPdfFileList[i]);
                    }
                }
            }
            else {
                for (var i = 0 ; i < $scope.tempPdfFileList.length ; i++) {
                    if ($scope.tempObject.seletedPDFStateName == $scope.tempPdfFileList[i].Region) {
                        if ($scope.tempObject.seletedPDFPayerName == $scope.tempPdfFileList[i].Payer) {
                            $scope.NewpdfTemplateList.push($scope.tempPdfFileList[i]);
                        }

                    }
                }
            }
        }
        $scope.IsFileSelected = false;
        $("#" + div).hide();
    }


    $scope.searchCumDropDownforSelectState = function (divId) {
        if ($scope.tempObject.seletedPDFPayerName == "" || $scope.tempObject.seletedPDFPayerName == undefined) {
            $scope.NewpdfTemplateList = $scope.tempPdfFileList;
        }
        else {
            $scope.NewpdfTemplateList = [];
            for (var i = 0 ; i < $scope.tempPdfFileList.length ; i++) {
                if ($scope.tempObject.seletedPDFPayerName == $scope.tempPdfFileList[i].Payer) {
                    $scope.NewpdfTemplateList.push($scope.tempPdfFileList[i]);
                }
            }
        }
        $scope.IsFileSelected = false;
        $("#" + divId).show();
        $("#ForTypePlanPayer").hide();
    };

    //---------Code to get the selected option of Region-------//
    $scope.addIntoTypePdfDropDown2 = function (state, div) {

        $scope.tempObject.seletedPDFStateName = state.Region;
        $scope.NewpdfTemplateList = [];
        if ($scope.tempObject.seletedPDFStateName == "" || $scope.tempObject.seletedPDFStateName == undefined) {
            $scope.NewpdfTemplateList = $scope.tempPdfFileList;
        }
        else {
            if ($scope.tempObject.seletedPDFPayerName == "" || $scope.tempObject.seletedPDFPayerName == undefined) {
                $scope.NewpdfTemplateList = [];
                for (var i = 0 ; i < $scope.tempPdfFileList.length ; i++) {
                    if ($scope.tempObject.seletedPDFStateName == $scope.tempPdfFileList[i].Region) {
                        $scope.NewpdfTemplateList.push($scope.tempPdfFileList[i]);
                    }
                }
            }
            else {
                for (var i = 0 ; i < $scope.tempPdfFileList.length ; i++) {
                    if ($scope.tempObject.seletedPDFStateName == $scope.tempPdfFileList[i].Region) {
                        if ($scope.tempObject.seletedPDFPayerName == $scope.tempPdfFileList[i].Payer) {
                            $scope.NewpdfTemplateList.push($scope.tempPdfFileList[i]);
                        }

                    }
                }
            }

        }
        $scope.IsFileSelected = false;
        $("#" + div).hide();
    }

    $scope.formLoading = false;
    $scope.generateForm = function () {

        $scope.packageLoading = true;
        $('#Provider').hide();
        $scope.ProfileIds = [];
        $scope.templates = [];

        for (var i = 0; i < $scope.selectedProviders.length; i++) {

            if ($scope.selectedProviders[i] != null) {

                $scope.ProfileIds.push($scope.selectedProviders[i].ProfileID);
            }
        }

        for (var i = 0; i < $scope.selectedForms.length; i++) {
            $scope.templates.push($scope.selectedForms[i].FileName);
        }

        $scope.ViewTemplate($scope.ProfileIds, $scope.templates);

        //for (planForm in $scope.selectedForms) {
        //    $scope.ViewTemplate($scope.ProfileIds, $scope.selectedForms[planForm].FileName);
        //}

        //for (var i = 0; i < $scope.pdfTemplateList.length; i++) {
        //    $scope.pdfTemplateList[i].IsCkecked=false;
        ////}
        //$scope.selectedForms = [];
    }

    //====================================Package Generation End=============================================


    $scope.allPlanProviders = [
        { "Plan": "Humana", "Group": "Access", "Specialty": "Anesthesiology", "Location": "5350 Spring Hill Drive", "CredentialledDate": "05-03-2014" },
        { "Plan": "Freedom HMO", "Group": "Access", "Specialty": "Addiction Medicine", "Location": "5350 Spring Hill Drive", "CredentialledDate": "03-03-2014" },
        { "Plan": "Freedom HMO", "Group": "Access2", "Specialty": "Addiction Medicine", "Location": "5350 Spring Hill Drive", "CredentialledDate": "01-02-2014" },
        { "Plan": "Wellcare", "Group": "MIRRA", "Specialty": "Dermatology", "Location": "5350 Spring Hill Drive", "CredentialledDate": "05-03-2013" },
    ];

    $scope.temp = '';

    $scope.pushplan = function (data) {
        $scope.temp = data;
    }
    $scope.showDocDiv = function () {
        $scope.TwoDocuments = true;
    };
    $scope.showDocDecred = function () {
        $scope.OneDocuments = true;
    };
    $scope.SearchProviderPanelToggle = function (divId) {
        $("#" + divId).slideToggle();
    };
    $scope.SearchProviderPanelToggleDown = function (divId) {
        $(".closePanel").slideUp();
        $("#" + divId).slideToggle();

    };

    $scope.SearchFilter = function (obj) {

        $scope.loadingAjax = true;
        $scope.allProviders = [];
        $scope.Npi = null;
        $http({
            method: 'GET',
            url: '/InitCredentialing/GetAllProviders?tempObject=obj'
        }).success(function (response, status, headers, config) {

            try {
                $scope.allProviders = response;
                data = response;

                for (var i = 0; i < $scope.allProviders.length ; i++) {
                    //$scope.allProviders[i].CredDate = ConvertDateFormat($scope.allProviders[i].CredDate).toDateString();
                    $scope.allProviders[i].IsCkecked = false;
                    data[i].IsCkecked = false;
                }
                //$scope.SearchProviderPanelToggleDown('SearchProviderResultPanel');
                $("#AuditSearch").slideToggle();
                //$("#SearchProviderResultPanel").slideToggle();
                //$("#SearchProviderResultPanel1").slideToggle();

                $scope.loadingAjax = false;
                if ($scope.SearchProviderTable != null) {
                    $scope.SearchProviderTable.reload();
                }
                if ($scope.SearchProviderTable1 != null) {
                    $scope.SearchProviderTable1.reload();
                }
                if ($scope.SearchProviderTable3 != null) {
                    $scope.SearchProviderTable3.reload();
                }
            } catch (e) {

            }
        }).error(function (data, status, headers, config) {
            $scope.loadingAjax = false;

        });
    };

    $scope.GetAllPackageTracker = function () {
        $scope.showTracker = true;
        $scope.loading = true;
        $http.get(rootDir + '/Credentialing/Auditing/GetAllPackageGenerationTracker').
          success(function (data, status, headers, config) {
              try {
                  var datas = [];
                  for (var i = 0; i < data.length; i++) {
                      data.GeneratedDate = $scope.ConvertDateFormat(data.GeneratedDate);
                      datas.push(data[i]);
                  }
                  $scope.init_table(datas, "packagetracker");
                  //$scope.data = datas;
                  //var counts = [];

                  //if ($scope.data.length <= 10) {
                  //    counts = [];
                  //}
                  //else if ($scope.data.length <= 25) {
                  //    counts = [10, 25];
                  //}
                  //else if ($scope.data.length <= 50) {
                  //    counts = [10, 25, 50];
                  //}
                  //else if ($scope.data.length <= 100) {
                  //    counts = [10, 25, 50, 100];
                  //}
                  //else if ($scope.data.length > 100) {
                  //    counts = [10, 25, 50, 100];
                  //}

                  //$scope.tableParams2 = new ngTableParams({
                  //    page: 1,            // show first page
                  //    count: 10,          // count per page
                  //    filter: {
                  //        //name: 'M'       // initial filter
                  //        //FirstName : ''
                  //    },
                  //    sorting: {
                  //        //name: 'asc'     // initial sorting
                  //    }
                  //}, {
                  //    counts: counts,
                  //    total: $scope.data.length, // length of data
                  //    getData: function ($defer, params) {
                  //        // use build-in angular filter
                  //        var filteredData = params.filter() ?
                  //                $filter('filter')($scope.data, params.filter()) :
                  //                $scope.data;
                  //        var orderedData = params.sorting() ?
                  //                $filter('orderBy')(filteredData, params.orderBy()) :
                  //                $scope.data;

                  //        params.total(orderedData.length); // set total for recalc pagination
                  //        $defer.resolve(orderedData.slice((params.page() - 1) * params.count(), params.page() * params.count()));
                  //    }
                  //});
                  $scope.loading = false;
              } catch (e) {

              }
          }).
          error(function (data, status, headers, config) {

              $scope.loading = false;
          });
    }

    $scope.ConvertDateFormat = function (value) {

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

    $scope.closeTracker = function () {
        $('.nav-tabs a:first').tab('show');
        $scope.showTracker = false;
    }
    $scope.currentPage = 0;
    $scope.currentCount = 0;
    $scope.params = null;

    $scope.SearchProviderTable = new ngTableParams(
    {
        page: 1,            // show first page
        count: 10          // count per page
    },
    {
        total: $scope.data.length,
        getData: function ($defer, params) {
            var orderedData = params.filter() ?
                                $filter('filter')($scope.data, params.filter()) :
                                $scope.data;
            params.total(orderedData.length);
            $scope.currentPage = params.page();
            $scope.currentCount = params.count();
            $scope.params = params;
            $defer.resolve(orderedData);
        }
    });

    $scope.TrackerTable = new ngTableParams(
    {
        page: 1,            // show first page
        count: 10          // count per page
    },
    {
        total: $scope.data.length,
        getData: function ($defer, params) {
            var orderedData = params.filter() ?
                                $filter('filter')($scope.data, params.filter()) :
                                $scope.data;
            params.total(orderedData.length);
            $scope.currentPage = params.page();
            $scope.currentCount = params.count();
            $scope.params = params;
            $defer.resolve(orderedData);
        }
    });

    $scope.SearchProviderTable1 = new ngTableParams(
    {
        page: 1,            // show first page
        count: 5          // count per page
    },
    {
        total: $scope.data.length,
        getData: function ($defer, params) {
            var orderedData = params.filter() ?
                                $filter('filter')($scope.data, params.filter()) :
                                $scope.data;
            params.total(orderedData.length);
            $defer.resolve(orderedData.slice((params.page() - 1) * params.count(), params.page() * params.count()));
        }
    });


    $scope.SearchProviderTable3 = new ngTableParams(
    {
        page: 1,            // show first page
        count: 5          // count per page
    },
    {
        total: $scope.data.length,
        getData: function ($defer, params) {
            var orderedData = params.filter() ?
                                $filter('filter')($scope.data, params.filter()) :
                                $scope.data;
            params.total(orderedData.length);
            $defer.resolve(orderedData.slice((params.page() - 1) * params.count(), params.page() * params.count()));
        }
    });

    $scope.IsValidIndex = function (index) {

        if (index >= (($scope.currentPage - 1) * $scope.currentCount) && index < ($scope.currentPage * $scope.currentCount))
            return true;
        else
            return false;
    }

    $scope.filterData = function () {
        $scope.params.page(1);
    }


    $scope.clearSearch = function () {
        $scope.allProviders = null;
        $scope.Npi = null;
        $scope.DataSubmited = false;
        $scope.AuditDocSubmited = false;
        $scope.PSVDocSubmited = false;
        $scope.DocSubmited = false;
        $scope.generate_package = false;
    };

    $scope.InitiateCredentialing = function () {
        $scope.DataSubmited = true;
        //$scope.SearchProviderPanelToggleDown('SearchProviderResultPanel2');
    };

    $scope.AuditDoctSubmit = function () {
        $scope.AuditDocSubmited = true;
        $scope.SearchProviderPanelToggleDown('SearchProviderResultPanel3');
    };

    $scope.PSVDoctSubmit = function () {
        $scope.PSVDocSubmited = true;
        $scope.SearchProviderPanelToggleDown('SearchProviderResultPanel4');
    };
    $scope.DoctSubmit = function () {
        $scope.DocSubmited = true;
        $(".closePanel").slideUp();
        $timeout(function () {
            $("#SuccessMessage").hide();
        }, 5000);
    };

    $scope.PSVChecked = '1';

    $scope.PSVCheck = function () {
        if ($scope.PSVChecked == '1') {
            $scope.PSVChecked = '2';
        }
        else {
            $scope.PSVChecked = '1';
        }
    }

    $scope.DocumentRepoChecked = '1';

    $scope.DocumentRepoCheck = function () {
        if ($scope.DocumentRepoChecked == '1') {
            $scope.DocumentRepoChecked = '2';
        }
        else {
            $scope.DocumentRepoChecked = '1';
        }
    }

    $scope.docList = [];
    $scope.packageLoading = false;
    $scope.generatePackage = function (id) {
        $scope.packageLoading = true;

        $scope.generatedPdfTemplateList = [];
        for (var i = 0; i < $scope.selectedDocument.length; i++) {

            $scope.docList.push($scope.selectedDocument[i].LicenseDocPath);
        }
        $('#Provider').hide();


        $scope.pdfLoadingAjax = true;
        $scope.isError = false;
        $http({
            url: rootDir + '/Credentialing/PDFMapping/GeneratePackage',
            method: "GET",
            params: { profileId: id, pdflist: $scope.docList }
        })
                .success(function (data) {
                    try {
                        $('#Provider').show();

                        $scope.packageLoading = false;
                        $scope.pdfLoadingAjax = false;
                        var open_link = window.open('', '_blank');
                        open_link.location = rootDir + '/Document/View?path=/ApplicationDocument/GeneratedPackagePdf/' + JSON.parse(data);
                        $scope.generatedPdfTemplateList.push({ fileName: $scope.tempObject.seletedPDFName, filePath: $scope.tempObject.selectedFileName });
                        $scope.docList = [];

                        //for (var i = 0; i < $scope.spaDoc.length; i++) {

                        //    $scope.spaDoc[i].Status = false;
                        //}

                        //for (var i = 0; i < $scope.LicenseTypes.length; i++) {
                        //    $scope.LicenseTypes[i].Licenses = [];
                        //    $scope.LicenseTypes[i].LicenseHistories = [];
                        //}
                        $scope.countDoc = 0;
                        $scope.spaDoc = [];
                        $scope.spaDoc = angular.copy($scope.copyspaDoc);
                        $scope.selectedDocument = [];
                        messageAlertEngine.callAlertMessage('pkgGenerated', "Package is generated successfully.", "success", true);
                        $('#selectAllId').prop('checked', false);
                        $scope.isSelectAll = false;
                        $scope.getData(id);
                    } catch (e) {

                    }
                }).error(function () {
                    $scope.docList = [];
                    $('#Provider').show();
                    messageAlertEngine.callAlertMessage('pkgGeneratedError', "An Error occured !! Please Try Again !!", "success", true);
                    $('#selectAllId').prop('checked', false);
                    $scope.isSelectAll = false;
                    //for (var i = 0; i < $scope.spaDoc.length; i++) {

                    //    $scope.spaDoc[i].Status = false;
                    //}
                    $scope.packageLoading = false;

                    //for (var i = 0; i < $scope.LicenseTypes.length; i++) {
                    //    $scope.LicenseTypes[i].Licenses = [];
                    //    $scope.LicenseTypes[i].LicenseHistories = [];
                    //}
                    $scope.countDoc = 0;
                    $scope.spaDoc = [];
                    $scope.spaDoc = angular.copy($scope.copyspaDoc);
                    $scope.selectedDocument = [];
                });

        //$scope.generate_package = true;
        //$scope.SearchProviderPanelToggle('SearchProviderResultPanel2');

    };
    function ResetFormForValidation(form) {
        form.removeData('validator');
        form.removeData('unobtrusiveValidation');
        $.validator.unobtrusive.parse(form);
    };
    var availableTags = [];
    $('.badge').removeAttr("style");
    availableTags =
        [
            'tulasidhar.salla@pratian.com',
            'bindupriya.ambati@pratian.com',
            'manideep.innamuri@pratian.com',
            'sharath.km@pratian.com',
            'shalabh@pratian.com'
        ];

    $scope.emailsAutoFill = function () {
        $(function () {

            //availableTags = $scope.EmailsIds;

            function split(val) {
                return val.split(/;\s*/);
            }
            function extractLast(term) {
                return split(term).pop();
            }

            $("#tags,#tagsCC,#tagsBCC")
                // don't navigate away from the field on tab when selecting an item
                .bind("keydown", function (event) {
                    if (event.keyCode === $.ui.keyCode.TAB &&
                            $(this).autocomplete("instance").menu.active) {
                        event.preventDefault();
                    }
                })
                .autocomplete({
                    minLength: 0,
                    source: function (request, response) {
                        // delegate back to autocomplete, but extract the last term
                        response($.ui.autocomplete.filter(
                            availableTags, extractLast(request.term)));
                    },
                    focus: function () {
                        // prevent value inserted on focus
                        return false;
                    },
                    select: function (event, ui) {
                        var terms = split(this.value);
                        // remove the current input
                        terms.pop();
                        // add the selected item
                        terms.push(ui.item.value);
                        // add placeholder to get the comma-and-space at the end
                        terms.push("");
                        this.value = terms.join(";");
                        return false;
                    }
                });
        });
    }
}]);

$(document).click(function (event) {
    if (!$(event.target).hasClass("form-control") && $(event.target).parents(".TemplateSelectAutoList").length === 0) {
        $(".TemplateSelectAutoList").hide();
    }
});
$(document).ready(function () {
    $(".ProviderTypeSelectAutoList").hide();
    $("body").tooltip({ selector: '[data-toggle=tooltip]' });
});
initCredApp.config(function ($datepickerProvider) {
    var today = new Date();
    angular.extend($datepickerProvider.defaults, {
        //startDate: 'today',
        minDate: today,
        startDate: 'today',
        autoclose: true,
        useNative: true
    });
});
