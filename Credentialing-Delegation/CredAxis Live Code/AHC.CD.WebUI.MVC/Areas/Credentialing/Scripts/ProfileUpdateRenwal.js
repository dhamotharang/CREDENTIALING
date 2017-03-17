var initCredApp = angular.module('InitCredApp', ['ui.bootstrap', 'ngTable']);

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

initCredApp.controller('listController', ['$scope', '$http', '$timeout', 'messageAlertEngine', 'ngTableParams', '$filter', '$q', function ($scope, $http, $timeout, messageAlertEngine, ngTableParams, $filter, $q) {

    //$scope.allProviders = [
    //    { "Type": "Update", "NPI": "1234567890", "FullName": "Dr. Pariksith Singh", "Section": "Demographics", "SubSection": "Personal Identification", "FieldName": "Driver's License", "For": "", "OldData": "123456", "NewData": "123459", "Date": "01-31-2015" },
    //    { "Type": "Renewal", "NPI": "1234567890", "FullName": "Dr. Pariksith Singh", "Section": "Demographics", "SubSection": "Personal Identification", "FieldName": "Driver's License", "For": "", "OldData": "123456", "NewData": "123459", "Date": "01-31-2015" },
    //    { "Type": "Renewal", "NPI": "4234233324", "FullName": "Dr. Maria Stone", "Section": " Identification & Licenses", "SubSection": "State License", "FieldName": "Issue Date & Expiry Date", "For": "License Number : ME98056", "OldData": "Issue Date: 11-15-2012, Expiry Date: 01-31-2015", "Date": "01-21-2015", "NewData": "Issue Date: 01-31-2015, Expiry Date: 01-31-2018" },
    //    { "Type": "Renewal", "NPI": "4234236624", "FullName": "Dr. K R", "Section": " Identification & Licenses", "SubSection": "State License", "FieldName": "Issue Date & Expiry Date", "For": "License Number : ME98056", "OldData": "Issue Date: 11-15-2012, Expiry Date: 01-31-2015", "Date": "01-21-2015", "NewData": "Issue Date: 01-31-2015, Expiry Date: 01-31-2018" },
    //    { "Type": "Renewal", "NPI": "4234237724", "FullName": "Dr. R S", "Section": " Identification & Licenses", "SubSection": "State License", "FieldName": "Issue Date & Expiry Date", "For": "License Number : ME98056", "OldData": "Issue Date: 11-15-2012, Expiry Date: 01-31-2015", "Date": "01-21-2015", "NewData": "Issue Date: 01-31-2015, Expiry Date: 01-31-2018" },
    //    { "Type": "Update", "NPI": "3423423453", "FullName": "Dr. Gaurav Mathur", "Section": "Demographics", "SubSection": "Personal Identification", "FieldName": "Driver's License", "For": "", "OldData": "123456", "NewData": "123459", "Date": "01-03-2015" },
    //    { "Type": "Update", "NPI": "6344545433", "FullName": "Dr. Nitesh Suresh", "Section": "Demographics", "SubSection": "Medicare Information", "FieldName": "Issue Date", "For": "Medicare Number : DSADSADSA", "OldData": "Issue Date: 01-31-2015", "NewData": "Issue Date: 01-31-2016", "Date": "01-29-2015" },
    //];

    //Convert the date from database to normal
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

    $scope.rejectionReason = '';

    $scope.temp = '';
    $scope.FilterProviders = [];
    $scope.credData = false;
    //---------------------Get All Master Data------
    $scope.GetCredentialingRequestData = function () {
        $scope.progressBarForCredentialingRequests = true;

        $http.get(rootDir + '/Profile/MasterData/GetAllCredentialingRequest').
        success(function (data, status, headers, config) {
            $scope.progressBarForCredentialingRequests = true;
            try {
                $scope.AllCredentilaingRequestData = data;
                $scope.credData = true;
                if ($scope.AllCredentilaingRequestData.length != 0) {
                    $http.get(rootDir + '/MasterDataNew/GetAllPlans').
                   success(function (data, status, headers, config) {
                       try {
                           $scope.Plans = data;
                           //for (var i = 0; i < $scope.AllCredentilaingRequestData.length; i++) {
                           //    $scope.AllCredentilaingRequestData[i].PlanName = $scope.Plans.filter(function (Plan) { return Plan.PlanID == $scope.AllCredentilaingRequestData[i].PlanID })[0].PlanName;
                           //}
                           for (var i in $scope.AllCredentilaingRequestData) {
                               for (var j in $scope.Plans) {
                                   if ($scope.AllCredentilaingRequestData[i].PlanID == $scope.Plans[j].PlanID) {
                                       $scope.AllCredentilaingRequestData[i].PlanName = $scope.Plans[j].PlanName;
                                   }
                               }
                           }
                           $scope.progressBarForCredentialingRequests = false;
                       } catch (e) {

                       }
                   }).
              error(function (data, status, headers, config) {

              });
                } else {
                    $scope.progressBarForCredentialingRequests = false;
                }
            } catch (e) {

            }
            
        }).
        error(function (data, status, headers, config) {

        });
    }

    $http.get(rootDir + '/Profile/MasterData/GetAllLicenseStatus').then(function (value) {
        $scope.LicenseStatus = value;
    });
    $http.get(rootDir + '/Profile/MasterData/GetAllStaffCategories').then(function (value) {
        $scope.StaffCategories = value;
    });
    $http.get(rootDir + '/Profile/MasterData/GetAllspecialtyBoards').then(function (value) {
        $scope.SpecialtyBoards = value;
    });
    $http.get(rootDir + '/Profile/MasterData/GetAllSpecialities').then(function (value) {
        $scope.Specialties = value;
    });
    $http.get(rootDir + '/Profile/MasterData/GetAllSchools').then(function (value) {
        $scope.Schools = value;
    });
    $http.get(rootDir + '/Profile/MasterData/GetAllQualificationDegrees').then(function (value) {
        $scope.QualificationDegrees = value;
    });
    $http.get(rootDir + '/Profile/MasterData/GetAllProviderTypes').then(function (value) {
        $scope.ProviderTypes = value;
    });
    $http.get(rootDir + '/Profile/MasterData/GetAllProfileDisclosureQuestions').then(function (value) {
        $scope.DisclosureQuestions = value;
    });
    $http.get(rootDir + '/Profile/MasterData/GetAllLoacationPracticeTypes').then(function (value) {
        $scope.LocationPracticeTypes = value;
    });
    $http.get(rootDir + '/Profile/MasterData/GetAllPracticeServiceQuestions').then(function (value) {
        $scope.PracticeServiceQuestions = value;
    });
    $http.get(rootDir + '/Profile/MasterData/GetAllPracticeOpenStatusQuestions').then(function (value) {
        $scope.PracticeOpenStatusQuestions = value;
    });
    $http.get(rootDir + '/Profile/MasterData/GetAllPracticeAccessibilityQuestions').then(function (value) {
        $scope.PracticeAccessibilityQuestions = value;
    });
    $http.get(rootDir + '/Profile/MasterData/GetAllMilitaryRanks').then(function (value) {
        $scope.MilitaryRanks = value;
    });
    $http.get(rootDir + '/Profile/MasterData/GetAllMilitaryPresentDuties').then(function (value) {
        $scope.MilitaryPresentDuties = value;
    });
    $http.get(rootDir + '/Profile/MasterData/GetAllMilitaryDischarges').then(function (value) {
        $scope.MilitaryDischarges = value;
    });
    $http.get(rootDir + '/Profile/MasterData/GetAllMilitaryBranches').then(function (value) {
        $scope.MilitaryBranches = value;
    });
    $http.get(rootDir + '/Profile/MasterData/GetAllInsuranceCarriers').then(function (value) {
        $scope.InsuranceCarriers = value;
    });
    $http.get(rootDir + '/Profile/MasterData/GetAllInsuranceCarrierAddresses').then(function (value) {
        $scope.InsuranceCarrierAddresses = value;
    });
    $http.get(rootDir + '/Profile/MasterData/GetAllHospitalContactPersons').then(function (value) {
        $scope.HospitalContactPersons = value;
    });
    //$http.get('/Profile/MasterData/GetAllHospitalContactInfoes').then(function (value) {
    //    $scope.HospitalContactInfoes = value;
    //});
    $http.get(rootDir + '/Profile/MasterData/GetAllHospitals').then(function (value) {
        $scope.Hospitals = value;
    });
    $http.get(rootDir + '/Profile/MasterData/GetAllDEASchedules').then(function (value) {
        $scope.DEASchedules = value;
    });
    $http.get(rootDir + '/Profile/MasterData/GetAllCertificates').then(function (value) {
        $scope.Certificates = value;
    });
    $http.get(rootDir + '/Profile/MasterData/GetAllAdmittingPrivileges').then(function (value) {
        $scope.AdmittingPrivileges = value;
    });
    $http.get(rootDir + '/Profile/MasterData/GetAllVisaTypes').then(function (value) {
        $scope.VisaTypes = value;
    });
    $http.get(rootDir + '/Profile/MasterData/GetAllVisaStatus').then(function (value) {
        $scope.VisaStatus = value;
    });
    $http.get(rootDir + '/Profile/MasterData/GetAllQuestions').then(function (value) {
        $scope.Questions = value;
    });
    $http.get(rootDir + '/Profile/MasterData/GetAllQuestionCategories').then(function (value) {
        $scope.QuestionCategories = value;
    });
    $http.get(rootDir + '/Profile/MasterData/GetAllProviderLevels').then(function (value) {
        $scope.ProviderLevels = value;
    });
    //$http.get('/Profile/MasterData/GetAllOrganizationGroup').then(function (value) {
    //    $scope.OrganizationGroup = value;
    //});
    $http.get(rootDir + '/Profile/MasterData/GetAllOrganizations').then(function (value) {
        $scope.Organizations = value;
    });
    //$http.get('/Profile/MasterData/GetMidlevels').then(function (value) {
    //    $scope.Midlevels = value;
    //});
    $http.get(rootDir + '/Profile/MasterData/GetAllAccessibilityQuestions').then(function (value) {
        $scope.AccessibilityQuestions = value;
    });
    $http.get(rootDir + '/Profile/MasterData/GetAllPracticeTypes').then(function (value) {
        $scope.PracticeTypes = value;
    });
    $http.get(rootDir + '/Profile/MasterData/GetAllServiceQuestions').then(function (value) {
        $scope.ServiceQuestions = value;
    });
    $http.get(rootDir + '/Profile/MasterData/GetAllOpenPracticeStatusQuestions').then(function (value) {
        $scope.OpenPracticeStatusQuestions = value;
    });
    $http.get(rootDir + '/Profile/MasterData/GetAllPaymentAndRemittance').then(function (value) {
        $scope.PaymentAndRemittance = value;
    });
    $http.get(rootDir + '/Profile/MasterData/GetAllBusinessContactPerson').then(function (value) {
        $scope.BusinessContactPerson = value;
    });
    $http.get(rootDir + '/Profile/MasterData/GetAllOrganizationGroups').then(function (value) {
        $scope.OrganizationGroups = value;
    });
    $http.get(rootDir + '/Profile/MasterData/GetAllBillingContact').then(function (value) {
        $scope.BillingContact = value;
    });
    $http.get(rootDir + '/Profile/MasterData/GetAllFacilities').then(function (value) {
        $scope.Facilities = value;
    });
    $http.get(rootDir + '/Profile/MasterData/GetAllCredentialingContact').then(function (value) {
        $scope.CredentialingContact = value;
    });

    //Get the value for the master data
    var getValue = function (data) {
        for (var i = 0; i < data.length; i++) {
            var newvalue = data[i];

            if (newvalue.FieldName == "ProviderTypeId") {
                newvalue.FieldName = "Provider Type";
                if (newvalue.OldValue != null)
                    newvalue.OldValue = $scope.ProviderTypes.data.filter(function (ProviderType) { return ProviderType.ProviderTypeID == newvalue.OldValue })[0].Title;
                if (newvalue.NewValue != null)
                    newvalue.NewValue = $scope.ProviderTypes.data.filter(function (ProviderType) { return ProviderType.ProviderTypeID == newvalue.NewValue })[0].Title;
            };
            if (newvalue.FieldName == "ProviderTypeID") {
                newvalue.FieldName = "Provider Type";
                if (newvalue.OldValue != null)
                    newvalue.OldValue = $scope.ProviderTypes.data.filter(function (ProviderType) { return ProviderType.ProviderTypeID == newvalue.OldValue })[0].Title;
                if (newvalue.NewValue != null)
                    newvalue.NewValue = $scope.ProviderTypes.data.filter(function (ProviderType) { return ProviderType.ProviderTypeID == newvalue.NewValue })[0].Title;
            };
            if (newvalue.FieldName == "ProviderTitleID") {
                newvalue.FieldName = "Provider Title";
                if (newvalue.OldValue != null)
                    newvalue.OldValue = $scope.ProviderTypes.data.filter(function (ProviderType) { return ProviderType.ProviderTypeID == newvalue.OldValue })[0].Title;
                if (newvalue.NewValue != null)
                    newvalue.NewValue = $scope.ProviderTypes.data.filter(function (ProviderType) { return ProviderType.ProviderTypeID == newvalue.NewValue })[0].Title;
            };
            if (newvalue.FieldName == "StaffCategoryID") {
                newvalue.FieldName = "Staff Category";
                if (newvalue.OldValue != null)
                    newvalue.OldValue = $scope.StaffCategories.data.filter(function (StaffCategories) { return StaffCategories.StaffCategoryID == newvalue.OldValue })[0].Title;
                if (newvalue.NewValue != null)
                    newvalue.NewValue = $scope.StaffCategories.data.filter(function (StaffCategories) { return StaffCategories.StaffCategoryID == newvalue.NewValue })[0].Title;
            };
            if (newvalue.FieldName == "SpecialtyID") {
                newvalue.FieldName = "Specialty Name";
                if (newvalue.NewValue != null)
                    newvalue.NewValue = $scope.Specialties.data.filter(function (Specialties) { return Specialties.SpecialtyID == newvalue.NewValue })[0].Name;
                if (newvalue.OldValue != null)
                    newvalue.OldValue = $scope.Specialties.data.filter(function (Specialties) { return Specialties.SpecialtyID == newvalue.OldValue })[0].Name;
            };
            if (newvalue.FieldName == "SpecialtyBoardID") {
                newvalue.FieldName = "Board Name";
                if (newvalue.OldValue != null)
                    newvalue.OldValue = $scope.SpecialtyBoards.data.filter(function (SpecialtyBoards) { return SpecialtyBoards.SpecialtyBoardID == newvalue.OldValue })[0].Name;
                if (newvalue.NewValue != null)
                    newvalue.NewValue = $scope.SpecialtyBoards.data.filter(function (SpecialtyBoards) { return SpecialtyBoards.SpecialtyBoardID == newvalue.NewValue })[0].Name;
            };
            if (newvalue.FieldName == "SchoolID") {
                newvalue.FieldName = "School Name";
                if (newvalue.OldValue != null)
                    newvalue.OldValue = $scope.Schools.data.filter(function (Schools) { return SpecialtyBoards.SchoolID == newvalue.OldValue })[0].Name;
                if (newvalue.NewValue != null)
                    newvalue.NewValue = $scope.Schools.data.filter(function (Schools) { return SpecialtyBoards.SchoolID == newvalue.NewValue })[0].Name;
            };
            if (newvalue.FieldName == "QualificationDegreeID") {
                newvalue.FieldName = "Degree Name";
                if (newvalue.OldValue != null)
                    newvalue.OldValue = $scope.QualificationDegrees.data.filter(function (QualificationDegrees) { return QualificationDegrees.QualificationDegreeID == newvalue.OldValue })[0].Title;
                if (newvalue.NewValue != null)
                    newvalue.NewValue = $scope.QualificationDegrees.data.filter(function (QualificationDegrees) { return QualificationDegrees.QualificationDegreeID == newvalue.NewValue })[0].Title;
            };
            if (newvalue.FieldName == "FacilityPracticeTypeID") {
                newvalue.FieldName = "Practice Type";
                if (newvalue.OldValue != null)
                    newvalue.OldValue = $scope.LocationPracticeTypes.data.filter(function (LocationPracticeTypes) { return LocationPracticeTypes.FacilityPracticeTypeID == newvalue.OldValue })[0].Title;
                if (newvalue.NewValue != null)
                    newvalue.NewValue = $scope.LocationPracticeTypes.data.filter(function (LocationPracticeTypes) { return LocationPracticeTypes.FacilityPracticeTypeID == newvalue.NewValue })[0].Title;
            };
            if (newvalue.FieldName == "PracticeServiceQuestionID") {
                newvalue.FieldName = "Services";
                if (newvalue.OldValue != null)
                    newvalue.OldValue = $scope.PracticeServiceQuestions.data.filter(function (PracticeServiceQuestions) { return PracticeServiceQuestions.PracticeServiceQuestionID == newvalue.OldValue })[0].Title;
                if (newvalue.NewValue != null)
                    newvalue.NewValue = $scope.PracticeServiceQuestions.data.filter(function (PracticeServiceQuestions) { return PracticeServiceQuestions.PracticeServiceQuestionID == newvalue.NewValue })[0].Title;
            };
            if (newvalue.FieldName == "PracticeOpenStatusQuestionID") {
                newvalue.FieldName = "Open Status Question";
                if (newvalue.OldValue != null)
                    newvalue.OldValue = $scope.PracticeOpenStatusQuestions.data.filter(function (PracticeOpenStatusQuestions) { return PracticeOpenStatusQuestions.PracticeOpenStatusQuestionID == newvalue.OldValue })[0].Title;
                if (newvalue.NewValue != null)
                    newvalue.NewValue = $scope.PracticeOpenStatusQuestions.data.filter(function (PracticeOpenStatusQuestions) { return PracticeOpenStatusQuestions.PracticeOpenStatusQuestionID == newvalue.NewValue })[0].Title;
            };
            if (newvalue.FieldName == "FacilityAccessibilityQuestionId") {
                newvalue.FieldName = "Accessibilities";
                if (newvalue.OldValue != null)
                    newvalue.OldValue = $scope.PracticeAccessibilityQuestions.data.filter(function (PracticeAccessibilityQuestions) { return PracticeAccessibilityQuestions.FacilityAccessibilityQuestionId == newvalue.OldValue })[0].Title;
                if (newvalue.NewValue != null)
                    newvalue.NewValue = $scope.PracticeAccessibilityQuestions.data.filter(function (PracticeAccessibilityQuestions) { return PracticeAccessibilityQuestions.FacilityAccessibilityQuestionId == newvalue.NewValue })[0].Title;
            };
            if (newvalue.FieldName == "MilitaryRankID") {
                newvalue.FieldName = "Military Rank";
                if (newvalue.OldValue != null)
                    newvalue.OldValue = $scope.MilitaryRanks.data.filter(function (MilitaryRanks) { return MilitaryRanks.MilitaryRankID == newvalue.OldValue })[0].Title;
                if (newvalue.NewValue != null)
                    newvalue.NewValue = $scope.MilitaryRanks.data.filter(function (MilitaryRanks) { return MilitaryRanks.MilitaryRankID == newvalue.NewValue })[0].Title;
            };
            if (newvalue.FieldName == "MilitaryPresentDutyID") {
                newvalue.FieldName = "Military Present Duty";
                if (newvalue.OldValue != null)
                    newvalue.OldValue = $scope.MilitaryPresentDuties.data.filter(function (MilitaryPresentDuties) { return MilitaryPresentDuties.MilitaryPresentDutyID == newvalue.OldValue })[0].Title;
                if (newvalue.NewValue != null)
                    newvalue.NewValue = $scope.MilitaryPresentDuties.data.filter(function (MilitaryPresentDuties) { return MilitaryPresentDuties.MilitaryPresentDutyID == newvalue.NewValue })[0].Title;
            };
            if (newvalue.FieldName == "MilitaryDischargeID") {
                newvalue.FieldName = "Military Discharge ";
                if (newvalue.OldValue != null)
                    newvalue.OldValue = $scope.MilitaryDischarges.data.filter(function (MilitaryDischarges) { return MilitaryDischarges.MilitaryDischargeID == newvalue.OldValue })[0].Title;
                if (newvalue.NewValue != null)
                    newvalue.NewValue = $scope.MilitaryDischarges.data.filter(function (MilitaryDischarges) { return MilitaryDischarges.MilitaryDischargeID == newvalue.NewValue })[0].Title;
            };
            if (newvalue.FieldName == "MilitaryBranchID") {
                newvalue.FieldName = "Military Branch";
                if (newvalue.OldValue != null)
                    newvalue.OldValue = $scope.MilitaryBranches.data.filter(function (MilitaryBranches) { return MilitaryBranches.MilitaryBranchID == newvalue.OldValue })[0].Title;
                if (newvalue.NewValue != null)
                    newvalue.NewValue = $scope.MilitaryBranches.data.filter(function (MilitaryBranches) { return MilitaryBranches.MilitaryBranchID == newvalue.NewValue })[0].Title;
            };
            if (newvalue.FieldName == "InsuranceCarrierID") {
                newvalue.FieldName = "Insurance Carrier Name";
                if (newvalue.OldValue != null)
                    newvalue.OldValue = $scope.InsuranceCarriers.data.filter(function (InsuranceCarriers) { return InsuranceCarriers.InsuranceCarrierID == newvalue.OldValue })[0].Name;
                if (newvalue.NewValue != null)
                    newvalue.NewValue = $scope.InsuranceCarriers.data.filter(function (InsuranceCarriers) { return InsuranceCarriers.InsuranceCarrierID == newvalue.NewValue })[0].Name;
            };
            if (newvalue.FieldName == "InsuranceCarrierAddressID") {
                newvalue.FieldName = "Location Name";
                if (newvalue.OldValue != null)
                    newvalue.OldValue = $scope.InsuranceCarrierAddresses.data.filter(function (InsuranceCarrierAddresses) { return InsuranceCarrierAddresses.InsuranceCarrierAddressID == newvalue.OldValue })[0].LocationName;
                if (newvalue.NewValue != null)
                    newvalue.NewValue = $scope.InsuranceCarrierAddresses.data.filter(function (InsuranceCarrierAddresses) { return InsuranceCarrierAddresses.InsuranceCarrierAddressID == newvalue.NewValue })[0].LocationName;
            };
            if (newvalue.FieldName == "HospitalContactPersonID") {
                newvalue.FieldName = "Contact Person Name";
                if (newvalue.OldValue != null)
                    newvalue.OldValue = $scope.HospitalContactPersons.data.filter(function (HospitalContactPersons) { return HospitalContactPersons.HospitalContactPersonID == newvalue.OldValue })[0].ContactPersonName;
                if (newvalue.NewValue != null)
                    newvalue.NewValue = $scope.HospitalContactPersons.data.filter(function (HospitalContactPersons) { return HospitalContactPersons.HospitalContactPersonID == newvalue.NewValue })[0].ContactPersonName;
            };
            if (newvalue.FieldName == "HospitalID") {
                newvalue.FieldName = "Hospital Name";
                if (newvalue.OldValue != null)
                    newvalue.OldValue = $scope.Hospitals.data.filter(function (Hospitals) { return Hospitals.HospitalID == newvalue.OldValue })[0].HospitalName;
                if (newvalue.NewValue != null)
                    newvalue.NewValue = $scope.Hospitals.data.filter(function (Hospitals) { return Hospitals.HospitalID == newvalue.NewValue })[0].HospitalName;
            };
            if (newvalue.FieldName == "DEAScheduleID") {
                newvalue.FieldName = "DEA Schedule";
                if (newvalue.OldValue != null)
                    newvalue.OldValue = $scope.DEASchedules.data.filter(function (DEASchedules) { return DEASchedules.DEAScheduleID == newvalue.OldValue })[0].ScheduleTitle;
                if (newvalue.NewValue != null)
                    newvalue.NewValue = $scope.DEASchedules.data.filter(function (DEASchedules) { return DEASchedules.DEAScheduleID == newvalue.NewValue })[0].ScheduleTitle;
            };
            if (newvalue.FieldName == "CertificationID") {
                newvalue.FieldName = "Certification Name";
                if (newvalue.OldValue != null)
                    newvalue.OldValue = $scope.Certificates.data.filter(function (Certificates) { return Certificates.CertificationID == newvalue.OldValue })[0].Name;
                if (newvalue.NewValue != null)
                    newvalue.NewValue = $scope.Certificates.data.filter(function (Certificates) { return Certificates.CertificationID == newvalue.NewValue })[0].Name;
            };
            if (newvalue.FieldName == "AdmittingPrivilegeID") {
                newvalue.FieldName = "Admitting Privileges";
                if (newvalue.OldValue != null)
                    newvalue.OldValue = $scope.AdmittingPrivileges.data.filter(function (AdmittingPrivileges) { return AdmittingPrivileges.AdmittingPrivilegeID == newvalue.OldValue })[0].Title;
                if (newvalue.NewValue != null)
                    newvalue.NewValue = $scope.AdmittingPrivileges.data.filter(function (AdmittingPrivileges) { return AdmittingPrivileges.AdmittingPrivilegeID == newvalue.NewValue })[0].Title;
            };
            if (newvalue.FieldName == "VisaTypeID") {
                newvalue.FieldName = "Visa Type";
                if (newvalue.OldValue != null)
                    newvalue.OldValue = $scope.VisaTypes.data.filter(function (VisaTypes) { return VisaTypes.VisaTypeID == newvalue.OldValue })[0].Title;
                if (newvalue.NewValue != null)
                    newvalue.NewValue = $scope.VisaTypes.data.filter(function (VisaTypes) { return VisaTypes.VisaTypeID == newvalue.NewValue })[0].Title;
            };
            if (newvalue.FieldName == "VisaStatusID") {
                newvalue.FieldName = "Visa Status";
                if (newvalue.OldValue != null)
                    newvalue.OldValue = $scope.VisaStatus.data.filter(function (VisaStatus) { return VisaStatus.VisaStatusID == newvalue.OldValue })[0].Title;
                if (newvalue.NewValue != null)
                    newvalue.NewValue = $scope.VisaStatus.data.filter(function (VisaStatus) { return VisaStatus.VisaStatusID == newvalue.NewValue })[0].Title;
            };
            if (newvalue.FieldName == "ProviderLevelID") {
                newvalue.FieldName = "Provider Level";
                if (newvalue.OldValue != null)
                    newvalue.OldValue = $scope.ProviderLevels.data.filter(function (ProviderLevels) { return ProviderLevels.ProviderLevelID == newvalue.OldValue })[0].Name;
                if (newvalue.NewValue != null)
                    newvalue.NewValue = $scope.ProviderLevels.data.filter(function (ProviderLevels) { return ProviderLevels.ProviderLevelID == newvalue.NewValue })[0].Name;
            };
            if (newvalue.FieldName == "OrganizationID") {
                newvalue.FieldName = "Organization Name";
                if (newvalue.OldValue != null)
                    newvalue.OldValue = $scope.Organizations.data.filter(function (Organizations) { return Organizations.OrganizationID == newvalue.OldValue })[0].Name;
                if (newvalue.NewValue != null)
                    newvalue.NewValue = $scope.Organizations.data.filter(function (Organizations) { return Organizations.OrganizationID == newvalue.NewValue })[0].Name;
            };
            if (newvalue.FieldName == "FacilityAccessibilityQuestionId") {
                newvalue.FieldName = "Accessibilities";
                if (newvalue.OldValue != null)
                    newvalue.OldValue = $scope.AccessibilityQuestions.data.filter(function (AccessibilityQuestions) { return AccessibilityQuestions.SchoolID == newvalue.OldValue })[0].Title;
                if (newvalue.NewValue != null)
                    newvalue.NewValue = $scope.AccessibilityQuestions.data.filter(function (AccessibilityQuestions) { return AccessibilityQuestions.SchoolID == newvalue.NewValue })[0].Title;
            };
            if (newvalue.FieldName == "FacilityPracticeTypeID") {
                newvalue.FieldName = "Practice Type Name";
                if (newvalue.OldValue != null)
                    newvalue.OldValue = $scope.PracticeTypes.data.filter(function (PracticeTypes) { return PracticeTypes.FacilityPracticeTypeID == newvalue.OldValue })[0].Title;
                if (newvalue.NewValue != null)
                    newvalue.NewValue = $scope.PracticeTypes.data.filter(function (PracticeTypes) { return PracticeTypes.FacilityPracticeTypeID == newvalue.NewValue })[0].Title;
            };
            if (newvalue.FieldName == "FacilityServiceQuestionID") {
                newvalue.FieldName = "Services";
                if (newvalue.OldValue != null)
                    newvalue.OldValue = $scope.ServiceQuestions.data.filter(function (ServiceQuestions) { return ServiceQuestions.FacilityServiceQuestionID == newvalue.OldValue })[0].Title;
                if (newvalue.NewValue != null)
                    newvalue.NewValue = $scope.ServiceQuestions.data.filter(function (ServiceQuestions) { return ServiceQuestions.FacilityServiceQuestionID == newvalue.NewValue })[0].Title;
            };
            if (newvalue.FieldName == "PracticeOpenStatusQuestionID") {
                newvalue.FieldName = "Open Practice Status";
                if (newvalue.OldValue != null)
                    newvalue.OldValue = $scope.OpenPracticeStatusQuestions.data.filter(function (OpenPracticeStatusQuestions) { return OpenPracticeStatusQuestions.PracticeOpenStatusQuestionID == newvalue.OldValue })[0].Title;
                if (newvalue.NewValue != null)
                    newvalue.NewValue = $scope.OpenPracticeStatusQuestions.data.filter(function (OpenPracticeStatusQuestions) { return OpenPracticeStatusQuestions.PracticeOpenStatusQuestionID == newvalue.NewValue })[0].Title;
            };
            if (newvalue.FieldName == "PracticePaymentAndRemittanceID") {
                newvalue.FieldName = "Payment And Remittance";
                if (newvalue.OldValue != null)
                    newvalue.OldValue = $scope.PaymentAndRemittance.data.filter(function (PaymentAndRemittance) { return PaymentAndRemittance.PracticePaymentAndRemittanceID == newvalue.OldValue })[0].Name;
                if (newvalue.NewValue != null)
                    newvalue.NewValue = $scope.PaymentAndRemittance.data.filter(function (PaymentAndRemittance) { return PaymentAndRemittance.PracticePaymentAndRemittanceID == newvalue.NewValue })[0].Name;
            };
            if (newvalue.FieldName == "EmployeeID") {
                newvalue.FieldName = "First Name";
                if (newvalue.OldValue != null)
                    newvalue.OldValue = $scope.BusinessContactPerson.data.filter(function (BusinessContactPerson) { return BusinessContactPerson.EmployeeID == newvalue.OldValue })[0].FirstName;
                if (newvalue.NewValue != null)
                    newvalue.NewValue = $scope.BusinessContactPerson.data.filter(function (BusinessContactPerson) { return BusinessContactPerson.EmployeeID == newvalue.NewValue })[0].FirstName;
            };
            if (newvalue.FieldName == "OrganizationGroupID") {
                newvalue.FieldName = "Organization Name";
                if (newvalue.OldValue != null)
                    newvalue.OldValue = $scope.OrganizationGroups.data.filter(function (OrganizationGroups) { return OrganizationGroups.OrganizationGroupID == newvalue.OldValue })[0].GroupName;
                if (newvalue.NewValue != null)
                    newvalue.NewValue = $scope.OrganizationGroups.data.filter(function (OrganizationGroups) { return OrganizationGroups.OrganizationGroupID == newvalue.NewValue })[0].GroupName;
            };
            if (newvalue.FieldName == "EmployeeID") {
                newvalue.FieldName = "First Name";
                if (newvalue.OldValue != null)
                    newvalue.OldValue = $scope.BillingContact.data.filter(function (BillingContact) { return BillingContact.EmployeeID == newvalue.OldValue })[0].FirstName;
                if (newvalue.NewValue != null)
                    newvalue.NewValue = $scope.BillingContact.data.filter(function (BillingContact) { return BillingContact.EmployeeID == newvalue.NewValue })[0].FirstName;
            };
            if (newvalue.FieldName == "OrganizationID") {
                newvalue.FieldName = "Facility Name";
                if (newvalue.OldValue != null)
                    newvalue.OldValue = $scope.Facilities.data.filter(function (Facilities) { return Facilities.OrganizationID == newvalue.OldValue })[0].Name;
                if (newvalue.NewValue != null)
                    newvalue.NewValue = $scope.Facilities.data.filter(function (Facilities) { return Facilities.OrganizationID == newvalue.NewValue })[0].Name;
            };
            if (newvalue.FieldName == "EmployeeID") {
                newvalue.FieldName = "First Name";
                if (newvalue.OldValue != null)
                    newvalue.OldValue = $scope.CredentialingContact.data.filter(function (CredentialingContact) { return CredentialingContact.EmployeeID == newvalue.OldValue })[0].FirstName;
                if (newvalue.NewValue != null)
                    newvalue.NewValue = $scope.CredentialingContact.data.filter(function (CredentialingContact) { return CredentialingContact.EmployeeID == newvalue.NewValue })[0].FirstName;
            };
            if (newvalue.FieldName == "StateLicenseStatusID") {
                newvalue.FieldName = "State License Status";
                if (newvalue.OldValue != null)
                    newvalue.OldValue = $scope.LicenseStatus.data.filter(function (LicenseStatus) { return LicenseStatus.StateLicenseStatusID == newvalue.OldValue })[0].Title;
                if (newvalue.NewValue != null)
                    newvalue.NewValue = $scope.LicenseStatus.data.filter(function (LicenseStatus) { return LicenseStatus.StateLicenseStatusID == newvalue.NewValue })[0].Title;
            };

            if (newvalue.FieldName == "FacilityId") {
                newvalue.FieldName = "Facility Name";
                if (newvalue.OldValue != null)
                    newvalue.OldValue = $scope.Facilities.data.filter(function (Facilities) { return Facilities.FacilityID == newvalue.OldValue })[0].Name;
                if (newvalue.NewValue != null)
                    newvalue.NewValue = $scope.Facilities.data.filter(function (Facilities) { return Facilities.FacilityID == newvalue.NewValue })[0].Name;
            };

            if (newvalue.FieldName == "IsAuthorizedToWorkInUS") {
                if (newvalue.OldValue == '0')
                    newvalue.OldValue = null;
                if (newvalue.NewValue == '0')
                    newvalue.NewValue = null;
            };


            if ($scope.temp.Section == 'Identification And License' && $scope.temp.SubSection == 'State License') {

                if (newvalue.FieldName == "Provider Type") {
                    newvalue.FieldName = "StateLicense Type";
                }
            }

            if (newvalue.FieldName == 'HospitalContactInfoID') {
                var ids = [];
                ids.push(parseInt(newvalue.OldValue));
                ids.push(parseInt(newvalue.NewValue));

                $.ajax({
                    url: rootDir + '/Credentialing/ProfileUpdates/getHospitalContactInfoById',
                    type: 'POST',
                    data: { contactInfoIds: ids },
                    cache: false,
                    async: false,
                    success: function (data) {
                        try {
                            data = JSON.parse(data);
                            newvalue.FieldName = 'HospitalLocation';
                            newvalue.OldValue = data[0].LocationName;
                            newvalue.NewValue = data[1].LocationName;
                        } catch (e) {

                        }
                    },
                    error: function (e) {

                    }
                });
            };
            data[i] = newvalue;
        }
        for (var i = 0; i < data.length; i++) {
            if (data.length > 0 && data[i].FieldName == 'DocumentCategory') {
                data.splice(data.indexOf(data[i]), 1);
                if (i != 0) { 
                    i--;
                }
            }
            if (data.length > 0 && data[i].FieldName == 'DocumentCategoryType') {
                data.splice(data.indexOf(data[i]), 1);
                if (i != 0) {
                    i--;
                }
            }
            if (data.length > 0 && data[i].FieldName == 'Fax') {
                data.splice(data.indexOf(data[i]), 1);
                if (i != 0) {
                    i--;
                }
            }
            if (data.length > 0 && data[i].FieldName == 'Phone') {
                data.splice(data.indexOf(data[i]), 1);
                if (i != 0) {
                    i--;
                }
            }
            if (data.length > 0 && data[i].FieldName == 'Telephone') {
                data.splice(data.indexOf(data[i]), 1);
                i--;
            }
            if (data.length > 0 && data[i].FieldName == 'EmployerFax') {
                data.splice(data.indexOf(data[i]), 1);
                i--;
            }
            if (data.length > 0 && data[i].FieldName == 'EmployerMobile') {
                data.splice(data.indexOf(data[i]), 1);
                i--;
            }
            if (data.length > 0 && data[i].FieldName == 'InsuranceCoverageType') {
                data.splice(data.indexOf(data[i]), 1);
                i--;
            }
            if (data.length > 0 && data[i].FieldName == 'PrimaryTax') {
                data.splice(data.indexOf(data[i]), 1);
                i--;
            }
            if (data.length > 0 && data[i].FieldName == 'SpecialtyBoardNotCertifiedDetailID') {
                data.splice(data.indexOf(data[i]), 1);
                i--;
            }
            if (data.length > 0 && data[i].FieldName == 'SpecialtyBoardExamStatus') {
                data.splice(data.indexOf(data[i]), 1);
                i--;
            }
            if (data.length > 0 && data[i].FieldName == 'MedicaidInformationID') {
                data.splice(data.indexOf(data[i]), 1);
                i--;
            }

            if ($scope.temp.SubSection == 'Specialty Details') {
                if (data[i].FieldName == 'SpecialtyBoardCertifiedDetail') {
                    data.splice(data.indexOf(data[i]), 1);

                }
                if (data[i].FieldName == 'SpecialtyBoardNotCertifiedDetail') {
                    data.splice(data.indexOf(data[i]), 1);

                }
            }
            if ($scope.temp.Section == 'Practice Location' && $scope.temp.SubSection == 'Practice Location Detail') {
                if (data.length > 0 && data[i].FieldName == 'OpenPracticeStatus') {
                    data.splice(data.indexOf(data[i]), 1);
                }

                if (data.length > 0 && data[i].FieldName == 'WorkersCompensationInformation') {
                    data.splice(data.indexOf(data[i]), 1);
                }

                if (data.length > 0 && data[i].FieldName == 'BusinessOfficeManagerOrStaffId') {
                    data.splice(data.indexOf(data[i]), 1);
                }

                if (data.length > 0 && data[i].FieldName == 'PaymentAndRemittanceId') {
                    data.splice(data.indexOf(data[i]), 1);
                }
                if (data.length > 0 && data[i].FieldName == 'BillingContactPersonId') {
                    data.splice(data.indexOf(data[i]), 1);
                }
                if (data.length > 0 && data[i].FieldName == 'PrimaryCredentialingContactPersonId') {
                    data.splice(data.indexOf(data[i]), 1);
                }
                if (data.length > 0 && data[i].FieldName == 'OfficeHour') {
                    data.splice(data.indexOf(data[i]), 1);
                }
                if (data.length > 0 && data[i].FieldName == 'OfficeHourId') {
                    data.splice(data.indexOf(data[i]), 1);
                }
            }
        }

        for (var i = 0; i < data.length; i++) {
            data[i].FieldName = AddSpacesInWords(data[i].FieldName);
        }
        return data;
    };

    $scope.ConvertDateTo = function (value) {
        var shortDate = null;
        if (value) {
            var regex = /-?\d+/;
            var matches = regex.exec(value);
            var dt = new Date(parseInt(matches[0]));
            var month = dt.getMonth() + 1;
            var monthString = month > 9 ? month : '0' + month;
            //var monthName = monthNames[month];
            var day = dt.getDate();
            var dayString = day > 9 ? day : '0' + day;
            var year = dt.getFullYear();
            shortDate = monthString + '/' + dayString + '/' + year;
            //shortDate = dayString + 'th ' + monthName + ',' + year;
        }
        return shortDate;
    };

    $scope.InitiateCredentialing = function (data1) {

        var obj = {
            ProfileID: data1.ProfileID,
            NPINumber: data1.NPINumber,
            CAQHNumber: data1.CAQH,
            FirstName: data1.FirstName,
            LastName: data1.LastName,
            PlanID: data1.PlanID,
            IsDelegatedYesNoOption: data1.DelegatedType,
            StatusType: 1
        };

        $http.post(rootDir + '/Credentialing/Initiation/InitiateCredentialing', obj).
            success(function (data, status, headers, config) {
                try {
                    //----------- success message -----------
                    if (data.status == "true") {
                        messageAlertEngine.callAlertMessage('successfulInitiated', "Credentialing Initiated Successfully. !!!!", "success", true);
                        $scope.CredentialingRequestInactive(data1);
                    }
                    else {
                        messageAlertEngine.callAlertMessage('errorInitiated1', "", "danger", true);
                        $scope.errorInitiated = data.status.split(",");
                    }
                } catch (e) {

                }
            }).
            error(function (data, status, headers, config) {
                //----------- error message -----------
                messageAlertEngine.callAlertMessage('errorInitiated1', "", "danger", true);
                $scope.errorInitiated = "Sorry for Inconvenience !!!! Please Try Again Later...";
            });
    }

    $scope.CredentialingRequestInactive = function (data) {

        var obj = {
            CredentialingRequestID: data.CredentialingRequestID,
            ProfileID: data.ProfileID,
            NPINumber: data.NPINumber,
            CAQHNumber: data.CAQH,
            FirstName: data.FirstName,
            LastName: data.LastName,
            PlanID: data.PlanID,
            IsDelegatedYesNoOption: data.DelegatedType,
            StatusType: 2
        };

        $http.post(rootDir + '/Profile/CredentialingRequest/CredentialingRequestInactive', obj).
            success(function (data, status, headers, config) {
                try {
                    //----------- success message -----------
                    if (data.status == "true") {
                        // messageAlertEngine.callAlertMessage('successfulInitiated', "Credentialing Initiated Successfully. !!!!", "success", true);
                    }
                    else {
                        messageAlertEngine.callAlertMessage('errorInitiated1', "", "danger", true);
                        $scope.errorInitiated = data.status.split(",");
                    }
                } catch (e) {

                }
            }).
            error(function (data, status, headers, config) {
                //----------- error message -----------
                messageAlertEngine.callAlertMessage('errorInitiated1', "", "danger", true);
                $scope.errorInitiated = "Sorry for Inconvenience !!!! Please Try Again Later...";
            });
    }

    $scope.getAllUpdates = function () {
        $scope.progressbar = true;
        $http.get(rootDir + "/Credentialing/ProfileUpdates/GetAllUpdates").then(function (value) {

            console.log('all updates');
            console.log(value);

            for (var i = 0; i < value.data.length ; i++) {
                if (value.data[i] != null) {
                    value.data[i].LastModifiedDate = $scope.ConvertDateFormat(value.data[i].LastModifiedDate);
                }
            }
            $scope.allProviders = angular.copy(value.data);

            $scope.getByType('Update');
            $scope.progressbar = false;
            $scope.historyMode = false;

            $scope.showUpdate = true;
        });
    }

    $scope.getAllUpdates();

    $scope.pushCredentialingRequestdata = function (credentialingRequestData) {
        $scope.RequestData = angular.copy(credentialingRequestData);
        $scope.businessError = "";
        $scope.hideApprovalBtn = false;
        $scope.loadBar = true;
        $scope.IsHowCredDetails = true;
        $('#list').addClass('seperate');
    }


    $scope.pushdata = function (ProfileUpdatesTrackerId, status) {
        $scope.businessError = "";
        $scope.hideApprovalBtn = false;
        $scope.loadBar = true;
        for (var i = 0; i < $scope.FilterProviders.length; i++) {
            if ($scope.FilterProviders[i].ProfileUpdatesTrackerId == ProfileUpdatesTrackerId) {
                $scope.temp = $scope.FilterProviders[i];
                data = $scope.FilterProviders[i];
                break;
            }
        }

        $scope.chnagedData = [];
        $http.post(rootDir + "/Credentialing/ProfileUpdates/GetDataById?trackerId=" + data.ProfileUpdatesTrackerId + "&status=" + status + "&modificationType=" + data.Modification).then(function (value) {


            $scope.chnagedData = angular.copy(value.data);
            var res = JSON.parse($scope.temp.NewData);
            for (var i in $scope.chnagedData) {
                if (res.hasOwnProperty($scope.chnagedData[i].FieldName)) {
                    res[$scope.chnagedData[i].FieldName] = $scope.chnagedData[i].NewValue;
                    if($scope.chnagedData[i].FieldName=="State" && $scope.chnagedData[i].NewValue=="state")
                    {
                        $scope.Declination = true;
                        $scope.Position = $scope.chnagedData.length % 2 == 0 ? $scope.chnagedData.length / 2 : $scope.chnagedData.length / 2 + 1;
                    }
                    if ($scope.chnagedData[i].FieldName == "State" && $scope.chnagedData[i].OldValue == "state") {
                        $scope.DeclinationOld = true;
                        $scope.Position = $scope.chnagedData.length % 2 == 0 ? $scope.chnagedData.length / 2 : $scope.chnagedData.length / 2 + 1;
                    }
                }
                
            }
            $scope.temp.NewData = JSON.stringify(res);
            $scope.chnagedData = getValue($scope.chnagedData);

            $scope.loadBar = false;
        });


        $scope.IsHowDetails = true;
        $('#list').addClass('seperate');
    }
   
    $scope.HideDetails = function () {
        $scope.IsHowDetails = false;
        $scope.IsHowCredDetails = false;
        $scope.temp = {};
        $('#list').removeClass('seperate');
    };


    $scope.OneIsSelected = false;
    $scope.IsAllChecked = { check: false };
    //------------- check atleast one checked ----------------
    $scope.anyOneChecked = function (checkStatus) {
        var status = false;
        for (var i in $scope.data) {
            if ($scope.data[i].IsChecked) {
                status = true;
                break;
            }
        }
        if (!checkStatus) {
            $scope.IsAllChecked.check = false;
        }
        $scope.OneIsSelected = status;

    };

    //----------------------- select all ----------------------
    $scope.SelectAll = function (status) {
        if (status) {
            for (var i in $scope.data) {
                $scope.data[i].IsChecked = true;
            }
        } else {
            for (var i in $scope.data) {
                $scope.data[i].IsChecked = false;
            }
        }
        $scope.anyOneChecked(status);
    };

    $scope.flag = false;
    var DateTimeConveter = function (value) {
        var returnValue = value;
        try {
            if (value.indexOf("/Date(") == 0) {
                returnValue = new Date(parseInt(value.replace("/Date(", "").replace(")/", ""), 10));
                var shortDate = null;
                var month = returnValue.getMonth() + 1;
                var monthString = month > 9 ? month : '0' + month;
                var day = returnValue.getDate();
                var dayString = day > 9 ? day : '0' + day;
                var year = returnValue.getFullYear();
                shortDate = monthString + '/' + dayString + '/' + year;
                returnValue = shortDate;
            }
            return returnValue;
        } catch (e) {
            return returnValue;
        }
        return returnValue;
    }
    $scope.UpdateSuccess = function (data) {
        $scope.flag = true;
        var newData = angular.copy(data);
        $scope.businessError = "";
        if (data.NewData.indexOf('"PracticeLocationDetailID":')>-1) {
            var Tempdatadata = JSON.parse(data.NewData);
            if (angular.isNumber(Tempdatadata.PracticeLocationDetailID)) {
                var Tempdatadata2 = JSON.parse(data.NewConvertedData);
                Tempdatadata2.PracticeLocationDetailID = Tempdatadata.PracticeLocationDetailID;
                if (Tempdatadata2.StartDate != null) {
                    Tempdatadata2.StartDate = DateTimeConveter(Tempdatadata2.StartDate);
                }
                if (angular.isDefined(Tempdatadata2.WorkersCompensationInformationID)) {
                    Tempdatadata2.IssueDate = DateTimeConveter(Tempdatadata2.IssueDate);
                    Tempdatadata2.ExpirationDate = DateTimeConveter(Tempdatadata2.ExpirationDate);
                }
                data.NewConvertedData = JSON.stringify(Tempdatadata2);
            }
        }
        
        if (data.NewData.indexOf('"PersonalDetailID":') > -1) {
            data.NewData = JSON.parse(data.NewData);
            data.NewData.ProviderTitles = JSON.parse(data.NewConvertedData).ProviderTitles;
            data.NewData = JSON.stringify(data.NewData);
        } else if (data.NewData.indexOf('"SpecialtyDetailID":') > -1) {
            data.NewData = angular.copy(data.NewConvertedData);
        } else if (data.NewData.indexOf('"ContactDetailID"') > -1) {
            data.NewData = angular.copy(data.NewConvertedData);
        }

        $.ajax({
            type: 'POST',
            url: rootDir + data.Url + data.ProfileId,
            //data: data.NewConvertedData,
            data: data.NewData,
            dataType: 'json',
            contentType: 'application/json; charset=utf-8',
            success: function (data) {
                try {
                    if (data.status == "true") {

                        $.ajax({
                            type: "POST",
                            url: rootDir + "/Credentialing/ProfileUpdates/SetApproval?modification=" + newData.Modification + "&approvedStatus=" + newData.ApprovalStatus,
                            data: {
                                TrackerId: newData.ProfileUpdatesTrackerId, ApprovalStatus: "Approved", RejectionReason: ""
                            },

                        }).success(function (resultData) {
                            messageAlertEngine.callAlertMessage("approved", "The Submission is Successful!!!!", "success", true);

                            $scope.hideApprovalBtn = true;
                            var obj = {};
                            for (var i = 0; i < $scope.allProviders.length; i++) {
                                if ($scope.allProviders[i].ProfileUpdatesTrackerId == newData.ProfileUpdatesTrackerId) {
                                    obj = $scope.allProviders[i];
                                }
                            }
                            $scope.allProviders.splice($scope.allProviders.indexOf(obj), 1);
                            //$scope.allProviders.splice($scope.FilterProviders.indexOf(newData), 1);
                            $scope.getByType(obj.Modification);
                            $scope.HideDetails();
                        }).error(function () { $scope.error_message = "An Error occured !! Please Try Again !!"; })
                        $scope.flag = false;
                    }
                    else {
                        $scope.businessError = data.status;
                        $scope.$apply();
                        $scope.flag = false;
                    }
                } catch (e) {

                }
            }

        });


    };


    $scope.businessError = "";
    $scope.UpdateFail = function () {

        $scope.hideApprovalBtn = true;
        $scope.showReject = true;
        $scope.showHold = false;
        $("#updateFail").show();

    };

    $scope.UpdateFailCredReq = function () {

        $scope.hideApprovalBtn = true;
        $scope.showReject = true;
        $scope.showHold = false;
        $("#UpdateFailCredReq").show();

    };

    $scope.OnHold = function () {

        $scope.hideApprovalBtn = true;
        $scope.showHold = true;
        $scope.showReject = false;
        $("#updateFail").show();
    };

    $scope.SetReject = function (data, reason) {

        var newData = angular.copy(data);
        $.ajax({
            type: "POST",
            url: rootDir + "/Credentialing/ProfileUpdates/SetApproval?modification=" + data.Modification + "&approvedStatus=" + data.ApprovalStatus,
            data: {
                TrackerId: data.ProfileUpdatesTrackerId, ApprovalStatus: "Rejected", RejectionReason: reason
            },

        }).success(function (resultData) {
            try {
                messageAlertEngine.callAlertMessage("approved", "The Update/Renewal is rejected!!!!", "success", true);
                //$("#updateFail").hide();
                //$scope.hideApprovalBtn = true;
                //var obj = {};
                //for (var i = 0; i < $scope.allProviders.length; i++) {
                //    if ($scope.allProviders[i].ProfileUpdatesTrackerId == newData.ProfileUpdatesTrackerId) {
                //        obj = $scope.allProviders[i];
                //    }
                //}

                //$scope.allProviders.splice($scope.FilterProviders.indexOf(obj), 1);
                //$scope.getByType(obj.Modification);


                //messageAlertEngine.callAlertMessage("approved", "The Submission is Successful!!!!", "success", true);

                $scope.hideApprovalBtn = true;
                var obj = {};
                for (var i = 0; i < $scope.allProviders.length; i++) {
                    if ($scope.allProviders[i].ProfileUpdatesTrackerId == newData.ProfileUpdatesTrackerId) {
                        obj = $scope.allProviders[i];
                    }
                }
                $scope.allProviders.splice($scope.allProviders.indexOf(obj), 1);
                //$scope.allProviders.splice($scope.FilterProviders.indexOf(newData), 1);
                $scope.getByType(obj.Modification);
                $scope.HideDetails();

            } catch (e) {

            }

        }).error(function () { $scope.error_message = "An Error occured !! Please Try Again !!"; })

        $scope.HideDetails();
    };

    $scope.SetCredRequestReject = function (requestdata, reason) {

        var obj = {
            CredentialingRequestTrackerID: 0,
            CredentialingRequestID: requestdata.CredentialingRequestID,
            ProfileID: requestdata.ProfileID,
            NPINumber: requestdata.NPINumber,
            CAQHNumber: requestdata.CAQH,
            FirstName: requestdata.FirstName,
            LastName: requestdata.LastName,
            RejectionReason: reason,
            PlanID: requestdata.PlanID,
            IsDelegated: requestdata.DelegatedType,
            StatusType: 1,
            ApprovalStatusType: 2
        };

        $http.post(rootDir + "/Credentialing/ProfileUpdates/CredentialingRequestTrackerSetApproval", obj).
           success(function (data, status, headers, config) {
               try {
                   //----------- success message -----------
                   if (data.status == "true") {
                       $scope.CredentialingRequestInactive(requestdata);
                       messageAlertEngine.callAlertMessage('successfulInitiated', "Credentialing Request Rejected Successfully. !!!!", "success", true);
                       $("#UpdateFailCredReq").hide();
                       $scope.hideApprovalBtn = true;
                       var obj = {};
                       for (var i = 0; i < $scope.AllCredentilaingRequestData.length; i++) {
                           if ($scope.AllCredentilaingRequestData[i].CredentialingRequestID == requestdata.CredentialingRequestID) {
                               obj = $scope.AllCredentilaingRequestData[i];
                           }
                       }
                       $scope.AllCredentilaingRequestData.splice($scope.AllCredentilaingRequestData.indexOf(obj), 1);
                   }
                   else {
                       messageAlertEngine.callAlertMessage('errorInitiated1', "", "danger", true);
                       $scope.errorInitiated = data.status.split(",");
                   }
               } catch (e) {

               }
           }).
           error(function (data, status, headers, config) {
               //----------- error message -----------
               messageAlertEngine.callAlertMessage('errorInitiated1', "", "danger", true);
               $scope.errorInitiated = "Sorry for Inconvenience !!!! Please Try Again Later...";
           });

        $scope.HideDetails();
    };


    $scope.SetCredRequestApproved = function (requestdata) {

        var obj = {
            CredentialingRequestTrackerID: 0,
            CredentialingRequestID: requestdata.CredentialingRequestID,
            ProfileID: requestdata.ProfileID,
            NPINumber: requestdata.NPINumber,
            CAQHNumber: requestdata.CAQH,
            FirstName: requestdata.FirstName,
            LastName: requestdata.LastName,
            RejectionReason: "",
            PlanID: requestdata.PlanID,
            IsDelegated: requestdata.DelegatedType,
            StatusType: 1,
            ApprovalStatusType: 1
        };

        $http.post(rootDir + "/Credentialing/ProfileUpdates/CredentialingRequestTrackerSetApproval", obj).
           success(function (data, status, headers, config) {
               try {
                   //----------- success message -----------
                   if (data.status == "true") {
                       $scope.InitiateCredentialing(requestdata);
                       $("#UpdateFailCredReq").hide();
                       $scope.hideApprovalBtn = true;
                       messageAlertEngine.callAlertMessage('successfullyInitiated', "Credentialing Request Initiated Successfully. !!!!", "success", true);
                       var obj = {};
                       for (var i = 0; i < $scope.AllCredentilaingRequestData.length; i++) {
                           if ($scope.AllCredentilaingRequestData[i].CredentialingRequestID == requestdata.CredentialingRequestID) {
                               obj = $scope.AllCredentilaingRequestData[i];
                           }
                       }
                       $scope.AllCredentilaingRequestData.splice($scope.AllCredentilaingRequestData.indexOf(obj), 1);
                   }
                   else {
                       messageAlertEngine.callAlertMessage('errorInitiated1', "", "danger", true);
                       $scope.errorInitiated = data.status.split(",");
                   }
               } catch (e) {

               }
           }).
           error(function (data, status, headers, config) {
               //----------- error message -----------
               messageAlertEngine.callAlertMessage('errorInitiated1', "", "danger", true);
               $scope.errorInitiated = "Sorry for Inconvenience !!!! Please Try Again Later...";
           });

        $scope.HideDetails();
    };

    $scope.SetOnHold = function (data, reason) {

        var newData;

        //for (var i = 0; i < $scope.FilterProviders.length; i++) {
        //    if (newData.ProfileUpdatesTrackerId == $scope.FilterProviders[i].ProfileUpdatesTrackerId) {
        //        data = $scope.FilterProviders[i];
        //        break;
        //    }
        //}
        $scope.IsHowDetails = false;

        //if (data.Modification = 'Update') {
        //    $scope.tableParamsUpdate.reload();
        //} else {
        //    $scope.tableParamsRenewal.reload();
        //}
        $.ajax({
            type: "POST",
            url: rootDir + "/Credentialing/ProfileUpdates/SetApproval?modification=" + data.Modification + "&approvedStatus=" + data.ApprovalStatus,
            data: {
                TrackerId: data.ProfileUpdatesTrackerId, ApprovalStatus: "OnHold", RejectionReason: reason
            },

        }).success(function (resultData) {
            try {
                messageAlertEngine.callAlertMessage("onHold", "The Update/Renewal is kept On hold!!!!", "success", true);

                for (var i = 0; i < $scope.data.length; i++) {
                    if (data.ProfileUpdatesTrackerId == $scope.data[i].ProfileUpdatesTrackerId) {
                        $scope.data[i].ApprovalStatus = 'OnHold';
                        $scope.data[i].RejectionReason = reason;
                        break;
                    }
                }
                for (var i = 0; i < $scope.FilterProviders.length; i++) {
                    if (data.ProfileUpdatesTrackerId == $scope.FilterProviders[i].ProfileUpdatesTrackerId) {
                        $scope.FilterProviders[i].ApprovalStatus = 'OnHold';
                        $scope.FilterProviders[i].RejectionReason = reason;
                        break;
                    }
                }

                $("#updateFail").hide();
                $scope.hideApprovalBtn = true;


                //$scope.allProviders.splice($scope.allProviders.indexOf(data), 1);
                //$scope.getByType('Update');
            } catch (e) {

            }

        }).error(function () { $scope.error_message = "An Error occured !! Please Try Again !!"; })
        $scope.HideDetails();
    };


    $scope.closedivUpdate = function () {
        $scope.hideApprovalBtn = false;
        $("#updateFail").hide();

    };

    $scope.closedivUpdateCredRequest = function () {
        $scope.hideApprovalBtn = false;
        $("#UpdateFailCredReq").hide();

    };

    //------------------ get type Type ----------------
    $scope.getByType = function (type) {
        var temp1 = [];
        $scope.SelectedType = type;
        for (var i in $scope.allProviders) {
            if ($scope.allProviders[i].Modification == type) {
                temp1.push($scope.allProviders[i]);
            }
        }
        $scope.FilterProviders = temp1;
        $scope.initTable($scope.FilterProviders, type);
        $scope.IsHowDetails = false;
        $scope.SelectAll(false);
        $scope.IsAllChecked.check = false;
        $scope.credData = false;
    };



    //--------------- accept Action ----------------------
    $scope.ActionMessage = "";

    $scope.ActionForChanges = function () {
        var newData;
        var dataToDelete = [];

        for (var i in $scope.data) {
            if ($scope.data[i].IsChecked) {

                for (var j in $scope.FilterProviders) {
                    if ($scope.FilterProviders[j].ProfileUpdatesTrackerId == $scope.data[i].ProfileUpdatesTrackerId) {
                        var newData = angular.copy($scope.FilterProviders[j]);
                        break;
                    }
                }
                //$scope.errorData = "";
                $.ajax({
                    type: 'POST',
                    async: false,
                    url: $scope.data[i].Url + $scope.data[i].ProfileId,
                    data: $scope.data[i].NewData,
                    dataType: 'json',
                    contentType: 'application/json; charset=utf-8',
                    success: function (data) {
                        try {
                            if (data.status == "true") {
                                //$scope.errorData = "true";
                                $scope.SetApproval(newData);
                                $scope.allProviders.splice($scope.FilterProviders.indexOf(newData), 1);
                                dataToDelete.push($scope.data[i]);
                                $scope.data[i].IsChecked = false;
                                //$scope.data.splice(i, 1);
                            }
                        } catch (e) {

                        }
                    }
                });
                //if ($scope.errorData == "true") {
                //    $scope.SetApproval(newData);
                //    $scope.allProviders.splice($scope.FilterProviders.indexOf(newData), 1);
                //}
            }
            //dataToDelete.push($scope.data[i]);
        }

        for (var i in dataToDelete) {
            for (var j = 0; j < $scope.data.length; j++) {
                if ($scope.data[j].ProfileUpdatesTrackerId == dataToDelete[i].ProfileUpdatesTrackerId) {
                    $scope.data.splice(j, 1);
                    $scope.FilterProviders.splice(j, 1);
                    $scope.$apply();
                }
            }
        }
        for (var i = 0; i < $scope.data.length; i++) {
            if ($scope.data[i].IsChecked == true) {
                $scope.anyBusinessError = true;
                $scope.$apply();
            }
        }

        messageAlertEngine.callAlertMessage("approved", "The Submission is Successful!!!!", "success", true);
        $scope.hideApprovalBtn = true;
        $scope.tableParamsUpdate.reload();
        $scope.tableParamsRenewal.reload();
        //$scope.getByType('Update');           
    };
    $scope.anyBusinessError = false;
    $scope.SetApproval = function (newData) {

        var data = angular.copy(newData);
        $.ajax({
            type: "POST",
            url: rootDir + "/Credentialing/ProfileUpdates/SetApproval?modification=" + newData.Modification + "&approvedStatus=" + newData.ApprovalStatus,
            data: {
                TrackerId: newData.ProfileUpdatesTrackerId, ApprovalStatus: "Approved", RejectionReason: ""
            },

        }).success(function (resultData) {

        }).error(function () { $scope.error_message = "An Error occured !! Please Try Again !!"; })


    }

    $scope.CurrentPage = [];

    var AddSpacesInWords = function (input) {
        var newString = "";
        var wasUpper = false;
        for (var i = 0; i < input.length; i++) {
            if (!wasUpper && (input[i] == input.toUpperCase()[i])) {
                if ((input[i] == input.toUpperCase()[i] && input[i - 1] == input.toUpperCase()[i - 1])) {
                    wasUpper = false;
                }
                else {
                    newString = newString + " ";
                    wasUpper = true;
                }
            }
            else {
                wasUpper = false;
            }
            newString = newString + input[i];
        }
        return newString;
    };


    //-------------------------- angular bootstrap pagger with custom-----------------
    $scope.maxSize = 5;
    $scope.bigTotalItems = 0;
    $scope.bigCurrentPage = 1;

    //-------------------- page change action ---------------------
    $scope.pageChanged = function (pagnumber) {
        $scope.bigCurrentPage = pagnumber;
    };

    //-------------- current page change Scope Watch ---------------------
    $scope.$watch('bigCurrentPage', function (newValue, oldValue) {
        $scope.CurrentPage = [];
        var startIndex = (newValue - 1) * 10;
        var endIndex = startIndex + 9;
        if ($scope.FilterProviders) {
            for (startIndex; startIndex <= endIndex ; startIndex++) {
                if ($scope.FilterProviders[startIndex]) {
                    $scope.CurrentPage.push($scope.FilterProviders[startIndex]);
                } else {
                    break;
                }
            }
        }

    });
    //-------------- License Scope Watch ---------------------
    $scope.$watchCollection('FilterProviders', function (newValue, oldValue) {
        if (newValue) {
            $scope.bigTotalItems = newValue.length;

            $scope.CurrentPage = [];
            $scope.bigCurrentPage = 1;

            var startIndex = ($scope.bigCurrentPage - 1) * 10;
            var endIndex = startIndex + 9;

            for (startIndex; startIndex <= endIndex ; startIndex++) {
                if ($scope.FilterProviders[startIndex]) {
                    $scope.CurrentPage.push($scope.FilterProviders[startIndex]);
                } else {
                    break;
                }
            }

        }
    });
    //------------------- end ------------------

    //-----edited by pritam-------------------------
    //----------profile updates history-----------
    $scope.historyMode = false;
    $scope.getProfileUpdateHistory = function () {

        $scope.progressbar = true;
        $http.get(rootDir + "/Credentialing/ProfileUpdates/GetAllUpdatesHistory").then(function (value) {

            for (var i = 0; i < value.data.length ; i++) {
                if (value.data[i] != null) {
                    value.data[i].LastModifiedDate = $scope.ConvertDateFormat(value.data[i].LastModifiedDate);
                }
            }
            $scope.allProviders = angular.copy(value.data);

            $scope.getByType('Update');
            $scope.progressbar = false;
            $scope.historyMode = true;
            $scope.showUpdate = true;

        });

    }


    //--------------ng table parmas--------------
    var formatDataForTable = function (data) {

        var formattedData = [];
        for (var i = 0; i < data.length; i++) {
            temp = new Object();
            temp.ProfileUpdatesTrackerId = data[i].ProfileUpdatesTrackerId;
            temp.NPINumber = data[i].Profile.OtherIdentificationNumber.NPINumber;
            temp.FirstName = data[i].Profile.PersonalDetail.FirstName;
            temp.Section = data[i].Section;
            temp.SubSection = data[i].SubSection;
            temp.ApprovalStatus = data[i].ApprovalStatus;
            temp.LastModifiedDate = $filter('date')(data[i].LastModifiedDate, 'MM-dd-yyyy');
            temp.IsChecked = data[i].IsChecked;
            temp.ProfileUpdatesTrackerId = data[i].ProfileUpdatesTrackerId;
            temp.Url = data[i].Url;
            temp.ProfileId = data[i].ProfileId;
            temp.NewData = data[i].NewData;
            temp.oldData = data[i].oldData;

            formattedData.push(temp);
        }
        return formattedData;
    };
    $scope.initTable = function (data, condition) {
        $scope.data = formatDataForTable(data);
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

        if (condition == 'Update') {
            if ($scope.tableParamsUpdate != null) {
                $scope.tableParamsUpdate.reload();

            } else {
                $scope.tableParamsUpdate = new ngTableParams({
                    page: 1,            // show first page
                    count: 10,          // count per page
                    sorting: {

                        LastModifiedDate: 'desc'
                        //name: 'asc'     // initial sorting
                    }
                }, {
                    counts: counts,
                    total: $scope.data.length, // length of data
                    getData: function ($defer, params) {
                        // use build-in angular filter
                        var orderedData = params.sorting() ?
                                $filter('orderBy')($scope.data, params.orderBy()) :
                                $scope.data;
                        orderedData = params.filter() ?
                                $filter('filter')(orderedData, params.filter()) :
                                orderedData;

                        $scope.users = orderedData.slice((params.page() - 1) * params.count(), params.page() * params.count());

                        params.total(orderedData.length); // set total for recalc pagination
                        $defer.resolve($scope.users);
                    }
                });
            }
        } else {
            if ($scope.tableParamsRenewal != null) {
                $scope.tableParamsRenewal.reload();

            } else {
                $scope.tableParamsRenewal = new ngTableParams({
                    page: 1,            // show first page
                    count: 10,          // count per page
                    sorting: {

                        LastModifiedDate: 'desc'
                        //name: 'asc'     // initial sorting
                    }
                }, {
                    counts: counts,
                    total: $scope.data.length, // length of data
                    getData: function ($defer, params) {
                        // use build-in angular filter
                        var orderedData = params.sorting() ?
                                $filter('orderBy')($scope.data, params.orderBy()) :
                                $scope.data;
                        orderedData = params.filter() ?
                                $filter('filter')(orderedData, params.filter()) :
                                orderedData;

                        $scope.users = orderedData.slice((params.page() - 1) * params.count(), params.page() * params.count());

                        params.total(orderedData.length); // set total for recalc pagination
                        $defer.resolve($scope.users);
                    }
                });
            }
        }
    }


}]);
