$(document).ready(function () {
    console.log("collapsing menu");
    $("#sidemenu").addClass("menu-in");
    $("#page-wrapper").addClass("menuup");
});

var toggleDiv = function (divId) {
    $('#' + divId).slideToggle();
};



var Cred_SPA_App = angular.module('cred_SPA_App', []);

Cred_SPA_App.service('messageAlertEngine', ['$rootScope', '$timeout', function ($rootScope, $timeout) {

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

Cred_SPA_App.controller('Cred_SPA_Ctrl', function ($scope, $http, $location, $filter, $rootScope, $window, messageAlertEngine) {

    $scope.credentialingInfoID = credId;
    $scope.credentialingInfo = JSON.parse(credInfo);
    $scope.fName = $scope.credentialingInfo.Profile.PersonalDetail.FirstName;
    console.log($scope.credentialingInfo);
    $scope.tempObject = [];

    $scope.tempObject.FirstName = $scope.credentialingInfo.Profile.PersonalDetail.FirstName;
    $scope.tempObject.MiddleName = $scope.credentialingInfo.Profile.PersonalDetail.MiddleName;
    $scope.tempObject.LastName = $scope.credentialingInfo.Profile.PersonalDetail.LastName;
    $scope.tempObject.Salutation = $scope.credentialingInfo.Profile.PersonalDetail.Salutation;
    $scope.tempObject.Specialties = $scope.credentialingInfo.Profile.SpecialtyDetails;
    $scope.tempObject.PracticeLocationDetails = $scope.credentialingInfo.Profile.PracticeLocationDetails;
    $scope.tempObject.StatusType = "Active";
    
    //=========================GetAllProfileVerificationParameter======================
    $scope.GetProfileVerificationParameter = function () {

        $http.get('/Profile/MasterData/GetAllProfileVerificationParameter').
        success(function (data, status, headers, config) {


            for (var i = 0; i < data.length; i++) {
                if (data[i].Code == 'SL') {
                    $scope.StateLicenseParameterID = data[i].ProfileVerificationParameterId;
                }
                if (data[i].Code == 'BC') {
                    $scope.BoardCertificationParameterID = data[i].ProfileVerificationParameterId;
                }
                if (data[i].Code == 'DEA') {
                    $scope.DEAParameterID = data[i].ProfileVerificationParameterId;
                }
                if (data[i].Code == 'CDS') {
                    $scope.CDSParameterID = data[i].ProfileVerificationParameterId;
                }
                if (data[i].Code == 'NPDB') {
                    $scope.NPDBParameterID = data[i].ProfileVerificationParameterId;
                }
                if (data[i].Code == 'MOPT') {
                    $scope.MOPTParameterID = data[i].ProfileVerificationParameterId;
                }
                if (data[i].Code == 'OIG') {
                    $scope.OIGParameterID = data[i].ProfileVerificationParameterId;
                }
            }

        }).
        error(function (data, status, headers, config) {
            // called asynchronously if an error occurs
            // or server returns response with an error status.
        });
    };


    $scope.getProviderTypeValue = function (type) {
        $scope.tempObject.ProviderType = type;
    };

    $scope.getBoardCertificationValue = function (type) {
        $scope.tempObject.BoardCertification = type;
    };

    $scope.getHospitalPrivilegesValue = function (type) {
        $scope.tempObject.HospitalPrivileges = type;
    };

    $scope.getGapsInPracticeValue = function (type) {
        $scope.tempObject.GapsInPractice = type;
    };

    $scope.getCleanLicenseValue = function (type) {
        $scope.tempObject.CleanLicense = type;
    };

    $scope.getNPDBIssuesValue = function (type) {
        $scope.tempObject.NPDBIssues = type;
    };

    $scope.getMalpracticeIssuesValue = function (type) {
        $scope.tempObject.MalpracticeIssues = type;
    };

    $scope.getAnyOtherIssueValue = function (type) {
        $scope.tempObject.AnyOtherIssue = type;
    };

    $scope.getProviderLevelValue = function (type) {
        $scope.tempObject.ProviderLevel = type;
    };

    $scope.SpecialityCCN = [];

    $scope.assignSpecialties = function(){
        for (index = 0; index < $scope.tempObject.Specialties.length; index++) {
            $scope.SpecialityCCN.push($scope.tempObject.Specialties[index].Specialty);
        }
    }

    $scope.assignSpecialties();

    //$scope.SpecialityCCN = [
    //    { SpecialityID: 1, Name: 'Allergy & Immunology', Status: 'Active' },
    //    { SpecialityID: 2, Name: 'Dermatology', Status: 'Active' },
    //    { SpecialityID: 3, Name: 'Colon & Rectal Surgery', Status: 'Active' },
    //    { SpecialityID: 4, Name: 'Family Practice', Status: 'Active' }
    //];
    $scope.addIntoTypeDropDown = function (type) {
        $scope.tempObject.Speciality = type.Name;
        $scope.tempObject.SpecialityID = type.SpecialityID;
        $("#ForType").hide();
    }
    $scope.searchCumDropDown = function () {
        $("#ForType").show();
    };

    $scope.CoveringPhysicians = [];

    $scope.assignCoveringColleagues = function () {
        for (index = 0; index < $scope.tempObject.PracticeLocationDetails.length; index++) {
            if ($scope.tempObject.PracticeLocationDetails[index].PracticeProviders.length > 0) {
                for (var i = 0; i < $scope.tempObject.PracticeLocationDetails[index].PracticeProviders.length; i++) {
                    if ($scope.tempObject.PracticeLocationDetails[index].PracticeProviders[i].Practice == 'CoveringColleague') {
                        $scope.CoveringPhysicians.push($scope.tempObject.PracticeLocationDetails[index].PracticeProviders[i]);
                    }
                }
            }
        }
    }

    $scope.assignCoveringColleagues();

    //$scope.CoveringPhysicians = [
    //    {CoveringPhysiciansID: 1, Name:'Dr. Maria G.', Status:'Active'},
    //    { CoveringPhysiciansID: 1, Name: 'Dr. Marry Grain', Status: 'Active' },
    //    { CoveringPhysiciansID: 1, Name: 'Dr. Kartik', Status: 'Active' },
    //];

    $scope.showContryCodeDiv = function (div_Id) {
        $("#" + div_Id).show();
    };
    $scope.tempObject.CoveringPhysicians1 = [];
    $scope.SelectCoveringPhysiciansType = function (coveringPhysicianstype) {
        $scope.tempObject.CoveringPhysicians1.push({
            CoveringPhysiciansID: coveringPhysicianstype.PracticeProviderID,
            FirstName: coveringPhysicianstype.FirstName,
            LastName: coveringPhysicianstype.LastName,
            Status: coveringPhysicianstype.Status,
        });
        $scope.CoveringPhysicians.splice($scope.CoveringPhysicians.indexOf(coveringPhysicianstype), 1);
        $scope.CoveringPhysiciansType = "";
    };

    $scope.RemoveCoveringPhysiciansType = function (coveringPhysicians) {
        $scope.tempObject.CoveringPhysicians1.splice($scope.CoveringPhysicians1.indexOf(coveringPhysicians), 1)
        $scope.CoveringPhysicians.push(coveringPhysicians);
    };


    $scope.setAppointment = function (obj) {
        //console.log(obj);
        var obj1 = {
            FirstName: obj.FirstName,
            MiddleName:obj.MiddleName,
            LastName: obj.LastName,
            AppointmentProviderType: obj.ProviderType,
            SpecialtyID:obj.SpecialityID,
            HospitalPrivilegesYesNoOption:obj.HospitalPrivileges,
            RemarksForHospitalPrivileges:obj.remarkForHospitalPrivileges,
            GapsInPracticeYesNoOption:obj.GapsInPractice,
            RemarksForGapsInPractice:obj.remarkForGapsInPractice,
            CleanLicenseYesNoOption:obj.CleanLicense,
            RemarksForCleanLicense:obj.remarkForCleanLicense,
            NPDBIssueYesNoOption:obj.NPDBIssues,
            RemarksForNPDBIssue:obj.remarkForNPDBIssues,
            MalpracticeIssueYesNoOption:obj.MalpracticeIssues,
            RemarksForMalpracticeIssue:obj.remarkForMalpracticeIssues,
            YearsInPractice:obj.YearsInPractice,
            SiteVisitRequiredYesNoOption:obj.AnyOtherIssue,
            RemarksForSiteVisitRequired:obj.remarkForAnyOtherIssue,
            RecommendedCredentialingLevel: obj.ProviderLevel,
            RemarksForBoardCertification: obj.remarkForBoardCertification,
            BoardCertifiedYesNoOption: obj.BoardCertification,
            StatusType:obj.Status,
        };
        $http.post('/Credentialing/CnD/CCMAction?credentialingInfoID='+credId, obj1).success(function (data, status, headers, config) {
            //----------- success message -----------
           
         
            $window.location = '/Credentialing/CnD/CredentialingAppointment';
            
        }).
            error(function (data, status, headers, config) {
                
            });
        
    };
    

    $scope.toggleDiv = function (divId) {
        $('#' + divId).slideToggle();
    };

    //============date=================================
    var monthNames = ["Jan", "Feb", "Mar", "Apr", "May", "Jun",
"Jul", "Aug", "Sep", "Oct", "Nov", "Dec"
    ];
    $scope.ConvertDateFormat = function (value) {
        var shortDate = null;
        if (value) {
            var regex = /-?\d+/;
            var matches = regex.exec(value);
            var dt = new Date(parseInt(matches[0]));
            var month = dt.getMonth() + 1;
            var monthString = month > 9 ? month : '0' + month;
            var monthName = monthNames[month];
            var day = dt.getDate();
            var dayString = day > 9 ? day : '0' + day;
            var year = dt.getFullYear();
            shortDate = monthString + '/' + dayString + '/' + year;
            //shortDate = dayString + 'th ' + monthName + ',' + year;
        }
        return shortDate;
    };
    $scope.reformatDate = function (dateStr) {

        if (dateStr != null) {
            dArr1 = dateStr.split("T");//2015-06-26T00:00:00
            dArr2 = dArr1[0].split("-");

           // return dArr2[2] + 'th ' + monthNames[dArr2[1] - 1] + ',' + dArr2[0];
            return dArr2[1] + '/' + dArr2[2] + '/' + dArr2[0];
        }

    }

    //========================PSV starts======================================
    //$scope.LicenseList = [{ title: 'State License for Practicing State', tabId: 'stateLicenseInfo', tabPanelId: 'perstateLicenseInfo' },
    //       { title: 'Board Certifications for Physicians', tabId: 'boardCertificationInfo', tabPanelId: 'perboardCertificationInfo' },
    //       { title: 'DEA', tabId: 'DEALicenseInfo', tabPanelId: 'perDEALicenseInfo' },
    //       { title: 'CDS', tabId: 'CDSLicenseInfo', tabPanelId: 'perCDSLicenseInfo' },
    //       { title: 'NPDB', tabId: 'NPDBLicenseInfo', tabPanelId: 'perNPDBLicenseInfo' },
    //       { title: 'Medicare OPT', tabId: 'medicareInfo', tabPanelId: 'permedicareInfo' },
    //       { title: 'OIG', tabId: 'OIGInfo', tabPanelId: 'perOIGInfo' }, ];

    //$scope.stateLicenses = { tabId: 'stateLicenseInfo', info: [{ state: 'Florida', licenseNumber: 'ME71088', issueDate: '04/03/2015', expirationDate: '03/03/2015', status: 'Valid', source: 'Source1', remarks: 'required', verifiedDocument: '/Content/Document/DocPreview.pdf' }, { state: 'USA', licenseNumber: 'US71099', issueDate: '04/03/2015', expirationDate: '04/03/2015', status: '', source: '', remarks: '', verifiedDocument: '/Content/Document/DocPreview.pdf' }] };
    //$scope.BoradCertifications = { tabId: 'boardCertificationInfo', info: [{ specialityBoardName: 'American Board of Internal Medicine', certificationStatus: 'Active', certificationDate: '16/12/2012', expirationDate: '04/03/2015', status: '', source: '', remarks: '', verifiedDocument: '' }, { specialityBoardName: 'American Board of External Medicine', certificationStatus: 'Active', certificationDate: '20/12/2012', expirationDate: '04/04/2015', status: '', source: '', remarks: '', verifiedDocument: '' }] };
    //$scope.DEAs = { tabId: 'DEALicenseInfo', info: [{ licenseNumber: '24EC077', issueDate: '04/03/2015', expirationDate: '03/03/2015', status: '', source: '', remarks: '', verifiedDocument: '' }, { licenseNumber: '24EC078', issueDate: '04/03/2015', expirationDate: '04/03/2015', status: '', source: '', remarks: '', verifiedDocument: '' }, { licenseNumber: '24EC079', issueDate: '04/03/2015', expirationDate: '03/05/2015', status: '', source: '', remarks: '', verifiedDocument: '' }] };
    //$scope.CDSs = { tabId: 'CDSLicenseInfo', info: [{ licenseNumber: '24EC078', issueDate: '04/03/2015', expirationDate: '04/03/2015', status: '', source: '', remarks: '', verifiedDocument: '' }, { licenseNumber: '24EC072', issueDate: '04/03/2015', expirationDate: '04/03/2015', status: '', source: '', remarks: '', verifiedDocument: '' }] };
    //$scope.NPDBs = { tabId: 'NPDBLicenseInfo', info: [] };
    //$scope.Medicares = { tabId: 'medicareInfo', info: [] };
    //$scope.OIGs = { tabId: 'OIGInfo', info: [] };


    $scope.FormattedData = [];
   
    $scope.loadData = function (id) {

        $http.get('/Credentialing/Verification/GetPSVReport?credinfoId='+id).
        success(function (data, status, headers, config) {

            if (data.status == "true") {
                $scope.showPsvError = false;
                $scope.FormatData(data.psvReport);
            }
            else {
                $scope.showPsvError = true;
                //messageAlertEngine.callAlertMessage("psvReportError", data.status, "danger", true);
            }          
            
            
        }).
        error(function (data, status, headers, config) {
            messageAlertEngine.callAlertMessage("psvReportError", "Sorry Unable To get PSV Report !!!!", "danger", true);
        });
    };

    //--------------------data format------------------------
    $scope.FormatData = function (data) {

        var formattedData = [];
        for (var i in data) {
            var VerificationData=new Object();
            if (data[i].VerificationData!=null)
                VerificationData = jQuery.parseJSON(data[i].VerificationData);
            
            formattedData.push({ Id: data[i].ProfileVerificationParameterId, info: { ProfileVerficationParameterObj: data[i].ProfileVerificationParameter, VerificationResultObj: data[i].VerificationResult, VerificationData: VerificationData } });
            //formattedData.push({ Id: data[i].ProfileVerificationParameterId, info: { ProfileVerficationParameterObj: data[i].ProfileVerificationParameter, VerificationResultObj: data[i].VerificationResult, VerificationData: data[i].VerificationData } });
        }

        var UniqueIds = [];
        UniqueIds.push(formattedData[0].Id);
        for (var i = 1; i < formattedData.length; i++) {

            var CurrObj = formattedData[i];
            var flag = 0;
            for (var j = 0; j < UniqueIds.length; j++) {
                if (CurrObj.Id == UniqueIds[j])
                {
                    flag = 1;
                }
            }
            if (flag == 0) {
                UniqueIds.push(CurrObj.Id);
            }
        }
        

        for (var i = 0; i < UniqueIds.length; i++) {
            var info=[];
            for (var j = 0; j < formattedData.length; j++) {
                if (UniqueIds[i] == formattedData[j].Id) {
                    info.push(formattedData[j].info);
                }

            }
            $scope.FormattedData.push({ Id: UniqueIds[i], Info: info });
        }
    };

    //=======================PSV ends=========================================

    $scope.profileData = function (id) {
        $http({
            method: "POST",
            url: "/Credentialing/CnD/GetProfile?id=" + id,
           
        }).success(function (resultData) {
            console.log(resultData);
           
        }).error(function () { $scope.loadingAjax = false; $scope.error_message = "An Error occured !! Please Try Again !!"; })
    }


   
    $scope.formPath = "";
    $scope.formTypeCheckbox = [{ FormName: 'CAQH', TemplateName: null, IsChecked: false, FileGenerationStatus: false, Pdfpath: null }, { FormName: 'Profile Access', TemplateName: 'A2HC Provider Profile for Wellcare2 - BLANK_new.pdf', IsChecked: false, FileGenerationStatus: false, Pdfpath: null }, { FormName: 'Profile Access 2', TemplateName: 'AHC Provider Profile for Wellcare - BLANK_new.pdf', IsChecked: false, FileGenerationStatus: false, Pdfpath: null }, { FormName: 'Template 2', TemplateName: null, IsChecked: false, FileGenerationStatus: false, Pdfpath: null }];
    $scope.selectedFormType = [];


    $scope.showPDF = function (pdfPath) {

        $scope.generatePDF = true;
        $scope.pdfSrc = pdfPath;
    };

    var GoToApplicationRepository = function (profileId, templateName, index) {
        $http.post('/Credentialing/CnD/ApplicationRepository?profileId=' + profileId + '&template=' + templateName).
        success(function (data, status, headers, config) {
            
            if (data.status == "true") {
                $scope.selectedFormType[index].FileGenerationStatus = true;
                console.log(data);
                console.log("Form Success");
                console.log(data.path);
                $scope.formPath = angular.copy(data.path);
                //$scope.pdfPath = $sce.trustAsResourceUrl("../../GeneratedForm/" + data.path);
                //$scope.pdfPath = "/Document/View?path=/GeneratedForm/" + data.path;
                $scope.selectedFormType[index].Pdfpath = "/Document/View?path=/GeneratedForm/" + data.path;
                console.log("URL");
                console.log($scope.pdfPath);
                //$scope.pdfPath = data.path;
                //$scope.generatePDF = true;
                //$scope.addRepo = false;
                $scope.progressbar = false;

            }

        }).
        error(function (data, status, headers, config) {
            //alert('Error');
        });
    };

    $scope.generateForm = function () {

        var profileId = 1;
        $scope.progressbar = true;
        //var templateName = $scope.template;
        //$scope.pdfPath = "Prakash_06-18-2015.pdf";
        //$scope.generatePDF = true;
        //$scope.addRepo = false;

        for (var i = 0; i < $scope.formTypeCheckbox.length; i++) {
            if ($scope.formTypeCheckbox[i].IsChecked == true)
            {
                $scope.selectedFormType.push($scope.formTypeCheckbox[i]);
                
            }
        }

        for (var i = 0; i < $scope.selectedFormType.length; i++) {
            if ($scope.selectedFormType[i].IsChecked == true) {
                GoToApplicationRepository(profileId, $scope.selectedFormType[i].TemplateName, i);
            }
        }

       
    }

    $scope.saveForm = function () {
        var path = $scope.formPath;
        var profileId = 1;
        $http.post('/Credentialing/CnD/AddApplication?profileId=' + profileId + '&path=' + path).
        success(function (data, status, headers, config) {
            if (data.status == "true") {

                $scope.generatePDF = false;
                $scope.addRepo = true;
                
            }

        }).
        error(function (data, status, headers, config) {

        });
    }

    $scope.showForm = function () {
        $scope.generatePDF = true;
    }




});

Cred_SPA_App.service('messageAlertEngine', ['$rootScope', '$timeout', function ($rootScope, $timeout) {

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
}]);

$(document).ready(function () {
    $(".ProviderTypeSelectAutoList").hide();
});
$(document).click(function (event) {

    if (!$(event.target).hasClass("form-control") && $(event.target).parents(".ProviderTypeSelectAutoList").length === 0) {
        $(".ProviderTypeSelectAutoList").hide();
    }
});