//------------------------- document ready state ------------------
$(document).ready(function () {
    $("#ForType").hide();
    $("#ForIPA").hide();
    $("#ForSpecialty").hide();
    $("body").tooltip({ selector: '[data-toggle=tooltip]' });

    $('#SearchProviderResultPanel3').slideUp();

});

//----------------------- ng app -------------------------
var initCredApp = angular.module('InitCredApp', ['ngTable']);


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



initCredApp.controller('singleProviderCtrl', ['$scope', '$http', '$timeout', '$filter', 'ngTableParams', 'messageAlertEngine', function ($scope, $http, $timeout, $filter, ngTableParams, messageAlertEngine) {

    //----------------------- filter data ---------------------
    $scope.filterData = {
        planId: 0,
        BusinessEntityId: 0
    };

    //--------------------- init required data -----------------------
    $scope.DataSubmited = false;
    $scope.AuditDocSubmited = false;
    $scope.PSVDocSubmited = false;
    $scope.DocSubmited = false;

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

        $("#" + divId).slideToggle();
        $scope.initiate = false;

    };

    $http.get(rootDir + '/Profile/MasterData/GetAllProviderTypes').
      success(function (data, status, headers, config) {

          $scope.masterProviderTypes = data;

      }).
      error(function (data, status, headers, config) {
          //console.log("Sorry internal master data cont able to fetch.");
      });



    $http.get(rootDir + '/Profile/MasterData/GetAllSpecialities').
     success(function (data, status, headers, config) {

         $scope.masterSpecialities = data;

     }).
     error(function (data, status, headers, config) {
         //console.log("Sorry internal master data cont able to fetch.");
     });





    $scope.searchCumDropDown = function (divId) {
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
          //console.log("Sorry internal master data can not able to fetch.");
      });

    $scope.init_table = function (data) {

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
            if (resultData != null) {
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
            if (deletedProvider.ProfileID == $scope.allProviders[i].ProfileID)
            {
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
                     //console.log($scope.plans);
                 }).
                 error(function (data, status, headers, config) {
                 });
    $scope.dataLoaded = false;
    $scope.loadingBar = false;
    $scope.getData = function (profileID) {
        //console.log($scope.selectedProviders);
        //var profileID = $scope.selectedProviders[0].ProfileID
        $scope.loadingBar = true;
       
        //console.log("3");
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
        
    };

    $scope.LicenseTypes = [];
    $scope.PSVLicenses = null;

    $scope.createLicenseArray = function () {

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

    }

    //$scope.clearDocuments = function () {

        
    //    $scope.LicenseTypes = [];
    //    $scope.spaDoc = [];
    //}

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
                $scope.FormatOtherDocument(data.OtherDocuments);
            }
        }

        $scope.combine();
    }

    $scope.spaDoc = [];

    $scope.FormatStateLicense = function (Docs) {

        for (var i = 0; i < Docs.length; i++) {
            if (Docs[i].StateLicenseDocumentPath != null) {
                $scope.LicenseTypes[0].Licenses.push({ LicenseID: Docs[i].StateLicenseInformationID, LicenseName: "Medical License" + (i + 1), LicenseDocPath: Docs[i].StateLicenseDocumentPath, ModifiedDate: Docs[i].LastModifiedDate, Description: "State License" });
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
                $scope.LicenseTypes[1].Licenses.push({ LicenseID: Docs[i].OtherLegalNameID, LicenseName: "Other Legal Name" + (i + 1), LicenseDocPath: Docs[i].DocumentPath, ModifiedDate: Docs[i].LastModifiedDate, Description: "Other Legal Name" });

                if (Docs[i].OtherLegalNameHistory != null) {
                    $scope.LicenseTypes[1].LicenseHistories.push({ LicenseID: Docs[i].OtherLegalNameHistory[j].OtherLegalNameHistoryID, LicenseName: "Other Legal Name" + (j + 1), LicenseDocPath: Docs[i].OtherLegalNameHistory[j].DocumentPath, removeDate: Docs[i].OtherLegalNameHistory[j].LastModifiedDate });

                }

            }

        }

    }

    $scope.FormatPersonalIdentificationDoc = function (Docs) {
        if (Docs.SSNCertificatePath != null) {
            $scope.LicenseTypes[2].Licenses.push({ LicenseID: Docs.PersonalIdentificationID, LicenseName: "Social Security Number", LicenseDocPath: Docs.SSNCertificatePath, ModifiedDate: Docs.LastModifiedDate, Description: "Social Security Number" });

        }

        if (Docs.DLCertificatePath != null) {
            $scope.LicenseTypes[2].Licenses.push({ LicenseID: Docs.PersonalIdentificationID, LicenseName: "Driver License", LicenseDocPath: Docs.DLCertificatePath, ModifiedDate: Docs.LastModifiedDate, Description: "Driver License" });

        }

    }

    $scope.FormatBirthInformationDoc = function (Docs) {
        if (Docs.BirthCertificatePath != null) {
            $scope.LicenseTypes[3].Licenses.push({ LicenseID: Docs.BirthInformationID, LicenseName: "Birth Certificate", LicenseDocPath: Docs.BirthCertificatePath, ModifiedDate: Docs.LastModifiedDate, Description: "Birth Certificate" });

            $scope.LicenseTypes[3].LicenseHistories.push({ LicenseID: 1, LicenseName: "Birth Certificate", LicenseDocPath: Docs.BirthCertificatePath, removeDate: Docs.LastModifiedDate });

        }
    }

    $scope.FormatCVInformationDoc = function (Docs) {
        if (Docs.CVDocumentPath != null) {
            $scope.LicenseTypes[4].Licenses.push({ LicenseID: Docs.CVInformationID, LicenseName: "Curriculum Vitae", LicenseDocPath: Docs.CVDocumentPath, ModifiedDate: Docs.LastModifiedDate, Description: "Curriculum Vitae" });

        }
    }

    $scope.FormatVisaInfoDoc = function (Docs) {
        if (Docs.VisaInfo != null) {
            if (Docs.VisaInfo.VisaCertificatePath != null) {
                $scope.LicenseTypes[5].Licenses.push({ LicenseID: Docs.VisaInfo.VisaInfoID, LicenseName: "VISA", LicenseDocPath: Docs.VisaInfo.VisaCertificatePath, ModifiedDate: Docs.LastModifiedDate, Description: "Visa" });

            }

            if (Docs.VisaInfo.GreenCardCertificatePath != null) {

                $scope.LicenseTypes[5].Licenses.push({ LicenseID: Docs.VisaInfo.VisaInfoID, LicenseName: "Green Card", LicenseDocPath: Docs.VisaInfo.GreenCardCertificatePath, ModifiedDate: Docs.LastModifiedDate, Description: "Green Card" });

            }

            if (Docs.VisaInfo.NationalIDCertificatePath != null) {

                $scope.LicenseTypes[5].Licenses.push({ LicenseID: Docs.VisaInfo.VisaInfoID, LicenseName: "National Identification", LicenseDocPath: Docs.VisaInfo.NationalIDCertificatePath, ModifiedDate: Docs.LastModifiedDate, Description: "National Identification Certificate" });

            }
        }

    }

    $scope.FormatFederalDEAInformationDoc = function (Docs) {

        $scope.Licenses = [];
        $scope.LicenseHistories = [];

        for (var i = 0; i < Docs.length; i++) {

            if (Docs[i].DEALicenceCertPath != null) {

                $scope.LicenseTypes[6].Licenses.push({ LicenseID: Docs[i].FederalDEAInformationID, LicenseName: "Drug Enforcement Administration" + (i + 1), LicenseDocPath: Docs[i].DEALicenceCertPath, ModifiedDate: Docs[i].LastModifiedDate, Description: "Drug Enforcement Administration" });

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

                $scope.LicenseTypes[7].Licenses.push({ LicenseID: Docs[i].MedicareInformationID, LicenseName: "Medicare" + (i + 1), LicenseDocPath: Docs[i].CertificatePath, ModifiedDate: Docs[i].LastModifiedDate, Description: "Medicare" });

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

                $scope.LicenseTypes[8].Licenses.push({ LicenseID: Docs[i].MedicaidInformationID, LicenseName: "Medicaid" + (i + 1), LicenseDocPath: Docs[i].CertificatePath, ModifiedDate: Docs[i].LastModifiedDate, Description: "Medicaid" });

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

                $scope.LicenseTypes[9].Licenses.push({ LicenseID: Docs[i].CDSCInformationID, LicenseName: "Central Drug Standard Control" + (i + 1), LicenseDocPath: Docs[i].CDSCCerificatePath, ModifiedDate: Docs[i].LastModifiedDate, Description: "Central Drug Standard Control" });

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

                        $scope.LicenseTypes[10].Licenses.push({ LicenseID: Docs[i].EducationDetailID, LicenseName: "Under Graduate" + ($scope.countUG + 1), LicenseDocPath: Docs[i].CertificatePath, ModifiedDate: Docs[i].LastModifiedDate, Description: "Under Graduate" });
                        $scope.countUG++;

                        if (Docs[i].EducationDetailHistory != null) {

                            $scope.LicenseTypes[10].LicenseHistories.push({ LicenseID: Docs[i].EducationDetailHistory.EducationDetailHistoryID, LicenseName: "Under Graduate" + ($scope.countUG + 1), LicenseDocPath: Docs[i].EducationDetailHistory.CertificatePath, removeDate: Docs[i].EducationDetailHistory.LastModifiedDate });

                        }

                    } else if (Docs[i].QualificationType == "Graduate") {

                        $scope.LicenseTypes[10].Licenses.push({ LicenseID: Docs[i].EducationDetailID, LicenseName: "Graduate" + ($scope.countG + 1), LicenseDocPath: Docs[i].CertificatePath, ModifiedDate: Docs[i].LastModifiedDate, Description: "Graduate" });
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

            $scope.LicenseTypes[11].Licenses.push({ LicenseID: Docs.ECFMGDetailID, LicenseName: "Education Commission for Foreign Medical Graduates", LicenseDocPath: Docs.ECFMGCertPath, ModifiedDate: Docs.LastModifiedDate, Description: "Education Commission for Foreign Medical Graduates" });

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

                    $scope.LicenseTypes[12].Licenses.push({ LicenseID: Docs[i].ProgramDetailID, LicenseName: "Internship" + ($scope.countIntern + 1), LicenseDocPath: Docs[i].DocumentPath, ModifiedDate: Docs[i].LastModifiedDate, Description: "Internship" });

                    $scope.countIntern++;

                    if (Docs.ProgramDetailHistory != null) {

                        $scope.LicenseTypes[12].LicenseHistories.push({ LicenseID: Docs[i].ProgramDetailHistory.ProgramDetailHistoryID, LicenseName: "Internship" + ($scope.countIntern + 1), LicenseDocPath: Docs[i].ProgramDetailHistory.DocumentPath, removeDate: Docs[i].ProgramDetailHistory.LastModifiedDate });

                    }

                } else if (Docs[i].ProgramType == "Fellowship") {

                    $scope.LicenseTypes[12].Licenses.push({ LicenseID: Docs[i].ProgramDetailID, LicenseName: "Fellowship" + ($scope.countFell + 1), LicenseDocPath: Docs[i].DocumentPath, ModifiedDate: Docs[i].LastModifiedDate, Description: "Fellowship" });

                    $scope.countFell++;

                    if (Docs.ProgramDetailHistory != null) {

                        $scope.LicenseTypes[12].LicenseHistories.push({ LicenseID: Docs[i].ProgramDetailHistory.ProgramDetailHistoryID, LicenseName: "Fellowship" + ($scope.countFell + 1), LicenseDocPath: Docs[i].ProgramDetailHistory.DocumentPath, removeDate: Docs[i].ProgramDetailHistory.LastModifiedDate });

                    }

                } else if (Docs[i].ProgramType == "Resident") {

                    $scope.LicenseTypes[12].Licenses.push({ LicenseID: Docs[i].ProgramDetailID, LicenseName: "Resident" + ($scope.countRes + 1), LicenseDocPath: Docs[i].DocumentPath, ModifiedDate: Docs[i].LastModifiedDate, Description: "Resident" });

                    $scope.countRes++;

                    if (Docs.ProgramDetailHistory != null) {

                        $scope.LicenseTypes[12].LicenseHistories.push({ LicenseID: Docs[i].ProgramDetailHistory.ProgramDetailHistoryID, LicenseName: "Resident" + ($scope.countRes + 1), LicenseDocPath: Docs[i].ProgramDetailHistory.DocumentPath, removeDate: Docs[i].ProgramDetailHistory.LastModifiedDate });

                    }

                } else if (Docs[i].ProgramType == "Other") {

                    $scope.LicenseTypes[12].Licenses.push({ LicenseID: Docs[i].ProgramDetailID, LicenseName: "Other" + ($scope.countOther + 1), LicenseDocPath: Docs[i].DocumentPath, ModifiedDate: Docs[i].LastModifiedDate, Description: "Other" });

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

                $scope.LicenseTypes[13].Licenses.push({ LicenseID: Docs[i].CMECertificationID, LicenseName: "Continuing Medical Education" + (i + 1), LicenseDocPath: Docs[i].CertificatePath, ModifiedDate: Docs[i].LastModifiedDate, Description: "Continuing Medical Education" });

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

                $scope.LicenseTypes[14].Licenses.push({ LicenseID: Docs[i].SpecialtyDetailID, LicenseName: "Specialty Board Certificate" + (i + 1), LicenseDocPath: Docs[i].SpecialtyBoardCertifiedDetail.BoardCertificatePath, ModifiedDate: Docs[i].LastModifiedDate, Description: "Specialty Board Certificate" });

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

                $scope.LicenseTypes[15].Licenses.push({ LicenseID: Docs[i].HospitalPrivilegeDetailID, LicenseName: "Hospital Privilege Document" + (count), LicenseDocPath: Docs[i].HospitalPrevilegeLetterPath, ModifiedDate: Docs[i].LastModifiedDate, Description: "Hospital Privilege Document" });

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

                $scope.LicenseTypes[16].Licenses.push({ LicenseID: Docs[i].ProfessionalLiabilityInfoID, LicenseName: "Professional Liability Certificate" + (i + 1), LicenseDocPath: Docs[i].InsuranceCertificatePath, ModifiedDate: Docs[i].LastModifiedDate, Description: "Professional Liability Document" });

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

                $scope.LicenseTypes[17].Licenses.push({ LicenseID: Docs[i].ProfessionalWorkExperienceID, LicenseName: "Work Experience Document" + (i + 1), LicenseDocPath: Docs[i].WorkExperienceDocPath, ModifiedDate: Docs[i].LastModifiedDate, Description: "Work Experience Document" });

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

                $scope.LicenseTypes[18].Licenses.push({ LicenseID: Docs[i].ContractInfoID, LicenseName: "Contract Document", LicenseDocPath: Docs[i].ContractFilePath, ModifiedDate: Docs[i].LastModifiedDate, Description: "Contract Document" });

            }

        }
    }

    $scope.FormatOtherDocument = function (Docs) {

        $scope.Licenses = [];
        $scope.LicenseHistories = [];

        for (var i = 0; i < Docs.length; i++) {

            if (Docs[i].DocumentPath != null) {

                $scope.LicenseTypes[19].Licenses.push({ LicenseID: Docs[i].OtherDocumentID, LicenseName: Docs[i].Title, LicenseDocPath: Docs[i].DocumentPath, ModifiedDate: Docs[i].LastModifiedDate, Private: Docs[i].IsPrivate, Description: "Other Document" });

            }

        }
    }

    $scope.combine = function (license) {

        for (var i = 0; i < $scope.LicenseTypes.length; i++) {

            for (var j = 0; j < $scope.LicenseTypes[i].Licenses.length; j++) {

                $scope.spaDoc.push($scope.LicenseTypes[i].Licenses[j]);

            }

        }

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

        if (Doc.IsCkecked == true) {
            $scope.selectedProviders.push(Doc);
        }
        else {
            $scope.selectedProviders.splice(index, 1);
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

        //$scope.selectedDocument.push(Doc);
        var idx = $scope.selectedDocument.indexOf(Doc);
        if (idx > -1) {
            $scope.selectedDocument.splice(idx, 1);
        }
        else {
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

        }

    }

    $scope.tempObj = {};
    $scope.moveDocumentUp = function (index) {

        $scope.tempObj = $scope.spaDoc[index];
        $scope.spaDoc[index] = $scope.spaDoc[index - 1];
        $scope.spaDoc[index - 1] = $scope.tempObj;
        $scope.tempObj = {};
    }

    $scope.moveDocumentDown = function (index) {

        $scope.tempObj = $scope.spaDoc[index];
        $scope.spaDoc[index] = $scope.spaDoc[index + 1];
        $scope.spaDoc[index + 1] = $scope.tempObj;

        $scope.tempObj = {};

    }

    $scope.generationType = null;


    $scope.pdfTemplateList = [
        {
            Name: "GHI",
            FileName: "GHI",
            IsCkecked: false,
            IsLoading: false
        },
        {
            Name: "AETNA",
            FileName: "AETNACOVENTRYTemplate",
            IsCkecked: false,
            IsLoading: false
        },
        {
            Name: "ATPL 2015",
            FileName: "ATPL2015",
            IsCkecked: false,
            IsLoading: false
        },
        {
            Name: "TRICARE PROVIDER APPLICATION",
            FileName: "TRICARE_PROVIDER_APPLICATION",
            IsCkecked: false,
            IsLoading: false
        }, {
            Name: "LETTER OF INTENT",
            FileName: "Letter_of_Intent",
            IsCkecked: false,
            IsLoading: false
        },
        {
            Name: "TRICARE PA APPLICATION",
            FileName: "TRICARE_PA_APPLICATION",
            IsCkecked: false,
            IsLoading: false
        },
        {
            Name: "ALLIED CREDENTIALING APPLICATION",
            FileName: "ALLIED_CREDENTIALING_APPLICATION",
            IsCkecked: false,
            IsLoading: false
        },
        {
            Name: "ULTIMATE CREDENTIALING APPLICATION PRACTIONER 2015",
            FileName: "UltimateCredentialingApplicationPractitioner_2015",
            IsCkecked: false,
            IsLoading: false
        }, {
            Name: "BCBS PAYMENT AUTH FORM",
            FileName: "BCBS_PAYMENT_AUTH_FORM",
            IsCkecked: false,
            IsLoading: false
        },
        {
            Name: "BCBS PROVIDER UPDATE FORM",
            FileName: "BCBS_PROVIDER_UPDATE_FORM",
            IsCkecked: false,
            IsLoading: false,
            IsLoading: false
        }, {
            Name: "BCBS PROVIDER REGISTRATION FORM",
            FileName: "BCBS_PROVIDER_REGISTRATION_FORM",
            IsCkecked: false,
            IsLoading: false
        },
        {
            Name: "MEDICAID GROUP MEMBERSHIP AUTHORIZATION FORM",
            FileName: "MEDICAID_GROUP_MEMBERSHIP_AUTHORIZATION_FORM",
            IsCkecked: false,
            IsLoading: false
        },
        //{
        //    Name: "MEDICAID MCO TREAT PROVIDER ATTESTATION FORM",
        //    FileName: "MEDICAID_MCO_TREAT_PROVIDER_ATTESTATION",
        //    IsCkecked: false,
        //    IsLoading: false
        //},
        {
            Name: "TRICARE ARNP APPLICATION FORM",
            FileName: "TRICARE_ARNP_APPLICATION_FORM",
            IsCkecked: false,
            IsLoading: false
        },
        {
            Name: "FL INSURENCE PROVIDER ATTESTATION",
            FileName: "FL_INSURANCE_PROVIDER_ATTESTATION",
            IsCkecked: false,
            IsLoading: false
        },
        {
            Name: "Freedom_Optimum_IPA",
            FileName: "Freedom_Optimum_IPA_Enrollment_Provider_PCP",
            IsCkecked: false,
            IsLoading: false
        },
        {
            Name: "Freedom_Optimum_Specialist",
            FileName: "Freedom_Optimum_Specialist_Package",
            IsCkecked: false,
            IsLoading: false
        },
        {
            Name: "Humana_IPA",
            FileName: "Humana_IPA_New_PCP_Package",
            IsCkecked: false,
            IsLoading: false
        },
        {
            Name: "Humana_Specialist",
            FileName: "Humana_Specialist_New_Provider",
            IsCkecked: false,
            IsLoading: false
        },        
        {
            Name: "FL HOSPITAL ADMIT ATTESTATION",
            FileName: "FL_HOSPITAL_ADMIT_ATTESTATION",
            IsCkecked: false,
            IsLoading: false
        },
        {
            Name: "FL FINANCIAL RESPONSIBILITY STATEMENT GLOBAL",
            FileName: "FL_FINANCIAL_RESPONSIBILITY_STATEMENT_GLOBAL",
            IsCkecked: false,
            IsLoading: false
        },
        {
            Name: "FL 3000 PCP ATTESTATION OF PATIENT LOAD 2015 GLOBAL",
            FileName: "FL_3000_PCP_Attestation_of_Patient_Load_2015_Global",
            IsCkecked: false,
            IsLoading: false
        },
        //{
        //    Name: "PHYSICIAN CREDENTIALING APPLICATION & DISCLOSURE OF OWNERSHIP FORM",
        //    FileName: "PHYSICIAN_CREDENTIALING_APPLICATION_DISCLOSURE_OF_OWNERSHIP"
        //},
        {
            Name: "WELLCARE MIDLEVEL FORM",
            FileName: "WELLCARE_MIDLEVEL_FORMS",
            IsCkecked: false,
            IsLoading: false
        }
    ];
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
                url: rootDir + '/Credentialing/PDFMapping/GenerateBulkPlanForm',
                method: "GET",
                params: { profileIds: ids, templateNames: templateNames }
            }).
            success(function (data) {
                console.log("Bulk form");
                console.log(data);

                for (var i = 0; i < data.length; i++) {
                    data[i].FirstName = $scope.selectedProviders.filter(function (provider) { return provider.ProfileID == data[i].ProfileID })[0].FirstName;
                    data[i].LastName = $scope.selectedProviders.filter(function (provider) { return provider.ProfileID == data[i].ProfileID })[0].LastName;

                }


                $scope.showGeneratedForm=angular.copy(data);
                $scope.packageLoading = false;
                $scope.showFormDiv = true;
                $scope.createdForms = angular.copy($scope.selectedForms);
                $scope.selectedForms = [];
                for (var i = 0; i < $scope.pdfTemplateList.length; i++) {

                    $scope.pdfTemplateList[i].IsCkecked = false;
                }
                $('#SearchProviderResultPanel2').slideUp();
                $('#SearchProviderResultPanel3').slideDown();
                $('#SearchProviderResultPanel1').slideDown();

                
                messageAlertEngine.callAlertMessage('formGenerated', "Forms are generated successfully.", "success", true);

                //for (var j = 0; j < $scope.pdfTemplateList.length; j++) {
                //    if ($scope.pdfTemplateList[j].FileName == templateName) {
                //        $scope.pdfTemplateList[j].IsLoading = true;

                //    }

                //}
                //console.log(data);
                $scope.pdfLoadingAjax = false;
                //var open_link = window.open('', '_blank');
                //open_link.location = '/Document/View?path=/ApplicationDocument/GeneratedTemplatePdf/' + JSON.parse(data);
                //$scope.generatedPdfTemplateList.push({ fileName: $scope.tempObject.seletedPDFName, filePath: $scope.tempObject.selectedFileName });

            }).error(function () {
                $scope.packageLoading = false;
                for (var i = 0; i < $scope.pdfTemplateList.length; i++) {

                    $scope.pdfTemplateList[i].IsCkecked = false;
                }
                messageAlertEngine.callAlertMessage('formGeneratedError', "An Error occured !! Please Try Again !!", "danger", true);

            });
    };


    $scope.formLoading = false;
    $scope.generateForm = function () {
        
        $scope.packageLoading = true;

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
        }).error(function (data, status, headers, config) {
            $scope.loadingAjax = false;

        });
    };

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


        console.log($scope.docList);
        $scope.pdfLoadingAjax = true;
        $scope.isError = false;
        $http({
            url: rootDir + '/Credentialing/PDFMapping/GeneratePackage',
            method: "GET",
            params: { profileId: id, pdflist: $scope.docList }
        })
                .success(function (data) {
                    //console.log(data);
                    $scope.packageLoading = false;
                    $scope.pdfLoadingAjax = false;
                    var open_link = window.open('', '_blank');
                    open_link.location = '/Document/View?path=/ApplicationDocument/GeneratedPackagePdf/' + JSON.parse(data);
                    $scope.generatedPdfTemplateList.push({ fileName: $scope.tempObject.seletedPDFName, filePath: $scope.tempObject.selectedFileName });
                    $scope.docList = [];

                    for (var i = 0; i < $scope.spaDoc.length; i++) {

                        $scope.spaDoc[i].Status = false;
                    }

                    messageAlertEngine.callAlertMessage('pkgGenerated', "Package is generated successfully.", "success", true);
                }).error(function () {
                    messageAlertEngine.callAlertMessage('pkgGeneratedError', "An Error occured !! Please Try Again !!", "success", true);

                    for (var i = 0; i < $scope.spaDoc.length; i++) {

                        $scope.spaDoc[i].Status = false;
                    }
                    $scope.packageLoading = false;
                });

        //$scope.generate_package = true;
        //$scope.SearchProviderPanelToggle('SearchProviderResultPanel2');

    };

}]);