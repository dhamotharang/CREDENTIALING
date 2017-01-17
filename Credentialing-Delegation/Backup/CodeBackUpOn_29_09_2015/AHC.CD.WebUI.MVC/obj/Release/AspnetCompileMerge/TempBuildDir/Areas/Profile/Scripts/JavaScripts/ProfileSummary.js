profileApp.controller('summaryAppCtrl', ['$scope', '$rootScope', '$http', 'masterDataService', 'messageAlertEngine', '$filter', function ($scope, $rootScope, $http, masterDataService, messageAlertEngine, $filter) {

    //Convert the date from database to normal
    $scope.ConvertDateFormat = function (value) {
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

    $scope.temp = '';
    $scope.FilterProviders = [];

    //---------------------Get All Master Data------
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
    $http.get(rootDir + '/Profile/MasterData/GetAllOrganizations').then(function (value) {
        $scope.Organizations = value;
    });
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

    $http.get(rootDir + '/Profile/MasterProfile/GetUpdatesById?profileId=' + profileId).
    success(function (data, status, headers, config) {
        console.log("Update Data");
        console.log(data);
        if (data.length != 0) {
            $scope.FormatDataForSection(data);
        }
        else
        {
            $scope.showUpdateError = true;
        }
        

    })
    .error(function (data, status, headers, config) {
    });

    $scope.FormattedDataForSection = [];
    $scope.FormatDataForSection = function (data) {

        var formattedData = [];
        var UniqueSections = [];
        UniqueSections.push(data[0].Section);
        for (var i = 1; i < data.length; i++) {

            var CurrObj = data[i];
            var flag = 0;
            for (var j = 0; j < UniqueSections.length; j++) {
                if (CurrObj.Section == UniqueSections[j]) {
                    flag = 1;
                }
            }
            if (flag == 0) {
                UniqueSections.push(CurrObj.Section);
            }
        }

        console.log("unique sections data");
        console.log(UniqueSections)

        for (var i = 0; i < UniqueSections.length; i++) {
            var updatecount = 0;
            var renewalcount = 0;
            var updateData = [];
            var renewalData = [];
            for (var j = 0; j < data.length; j++) {
                if (UniqueSections[i] == data[j].Section) {
                    if (data[j].Modification == 'Update') {
                        updatecount++;
                        updateData.push({ profiletrackerId: data[j].ProfileUpdatesTrackerId, ModifiedDate: data[j].LastModifiedDate, AprovalStatus: data[j].ApprovalStatus, SubsectionName: data[j].SubSection });

                    } else {
                        renewalcount++;
                        renewalData.push({ profiletrackerId: data[j].ProfileUpdatesTrackerId, ModifiedDate: data[j].LastModifiedDate, AprovalStatus: data[j].ApprovalStatus, SubsectionName: data[j].SubSection });

                    }
                }
            }
            $scope.FormattedDataForSection.push({ SectionName: UniqueSections[i], UpdateCnt: updatecount, RenewalCnt: renewalcount, UpdateData: updateData, RenewalData: renewalData });
        }
        console.log("Formmatted for Section data");
        console.log($scope.FormattedDataForSection);
       // $scope.GetAllProfileUpdates($scope.FormattedDataForSection[1].UpdateData);
    };

    $scope.GetAllProfileUpdates = function (data) {

        if (data.length != 0) {
            var formattedDataforsubsection = [];
            var UniqueSubSections = [];
            UniqueSubSections.push(data[0].SubsectionName);
            for (var i = 1; i < data.length; i++) {

                var CurrObj = data[i];
                var flag = 0;
                for (var j = 0; j < UniqueSubSections.length; j++) {
                    if (CurrObj.SubsectionName == UniqueSubSections[j]) {
                        flag = 1;
                    }
                }
                if (flag == 0) {
                    UniqueSubSections.push(CurrObj.SubsectionName);
                }
            }

            console.log("UniqueSubSections");
            console.log(UniqueSubSections);

            for (var i = 0; i < UniqueSubSections.length; i++) {
                var updatesubsectionwiseData = [];
                for (var j = 0; j < data.length; j++) {
                    if (UniqueSubSections[i] == data[j].SubsectionName) {

                        updatesubsectionwiseData.push({ profiletrackerId: data[j].profiletrackerId, status: data[j].AprovalStatus });

                    }
                }
                formattedDataforsubsection.push({ SubSectionName: UniqueSubSections[i], ProfileTrackerIds: updatesubsectionwiseData });
            }

            console.log("Formmatted for sub - Section data");
            console.log(formattedDataforsubsection);

            var testData = [];
            for (var i = 0; i < formattedDataforsubsection.length; i++) {

                var trackerIds = [];
                var trackerStatus=[];
                for (var j = 0; j < formattedDataforsubsection[i].ProfileTrackerIds.length; j++) {
                    trackerIds.push(formattedDataforsubsection[i].ProfileTrackerIds[j].profiletrackerId);
                    trackerStatus.push(formattedDataforsubsection[i].ProfileTrackerIds[j].status);
                }


                $.ajax({
                    url: rootDir + '/Profile/MasterProfile/getProfileUpdateDataByIdWithStatus',
                    type: 'POST',
                    data: {
                        profileUpdateTrackerIds: trackerIds,
                        Status: trackerStatus
                    },
                    async: false,
                    cache: false,
                    success: function (data) {
                        testData.push({ subsectionName: formattedDataforsubsection[i].SubSectionName, fields: data });
                    }
                });

            }

            console.log('testData');
            console.log(testData);
            $scope.SectionDisplayData = testData;

            //========================================

            for (var i = 0; i < $scope.SectionDisplayData.length; i++) {
                for (var j = 0; j < $scope.SectionDisplayData[i].fields.length; j++) {


                    var newvalue = $scope.SectionDisplayData[i].fields[j];

                    if (newvalue.FieldName == "ProviderTypeId") {
                        newvalue.FieldName = "Provider Type";
                        newvalue.OldValue = $scope.ProviderTypes.data.filter(function (ProviderType) { return ProviderType.ProviderTypeID == newvalue.OldValue })[0].Title;
                        newvalue.NewValue = $scope.ProviderTypes.data.filter(function (ProviderType) { return ProviderType.ProviderTypeID == newvalue.NewValue })[0].Title;
                    };
                    if (newvalue.FieldName == "ProviderTypeID") {
                        newvalue.FieldName = "Provider Type";
                        newvalue.OldValue = $scope.ProviderTypes.data.filter(function (ProviderType) { return ProviderType.ProviderTypeID == newvalue.OldValue })[0].Title;
                        newvalue.NewValue = $scope.ProviderTypes.data.filter(function (ProviderType) { return ProviderType.ProviderTypeID == newvalue.NewValue })[0].Title;
                    };
                    if (newvalue.FieldName == "ProviderTitleID") {
                        newvalue.FieldName = "Provider Title";
                        newvalue.OldValue = $scope.ProviderTypes.data.filter(function (ProviderType) { return ProviderType.ProviderTypeID == newvalue.OldValue })[0].Title;
                        newvalue.NewValue = $scope.ProviderTypes.data.filter(function (ProviderType) { return ProviderType.ProviderTypeID == newvalue.NewValue })[0].Title;
                    };
                    if (newvalue.FieldName == "StaffCategoryID") {
                        newvalue.FieldName = "Staff Category";
                        newvalue.OldValue = $scope.StaffCategories.data.filter(function (StaffCategories) { return StaffCategories.StaffCategoryID == newvalue.OldValue })[0].Title;
                        newvalue.NewValue = $scope.StaffCategories.data.filter(function (StaffCategories) { return StaffCategories.StaffCategoryID == newvalue.NewValue })[0].Title;
                    };
                    if (newvalue.FieldName == "SpecialtyID") {
                        newvalue.FieldName = "Specialty Name";
                        newvalue.OldValue = $scope.Specialties.data.filter(function (Specialties) { return Specialties.SpecialtyID == newvalue.OldValue })[0].Name;
                        newvalue.NewValue = $scope.Specialties.data.filter(function (Specialties) { return Specialties.SpecialtyID == newvalue.NewValue })[0].Name;
                    };
                    if (newvalue.FieldName == "SpecialtyBoardID") {
                        newvalue.FieldName = "Board Name";
                        newvalue.OldValue = $scope.SpecialtyBoards.data.filter(function (SpecialtyBoards) { return SpecialtyBoards.SpecialtyBoardID == newvalue.OldValue })[0].Name;
                        newvalue.NewValue = $scope.SpecialtyBoards.data.filter(function (SpecialtyBoards) { return SpecialtyBoards.SpecialtyBoardID == newvalue.NewValue })[0].Name;
                    };
                    if (newvalue.FieldName == "SchoolID") {
                        newvalue.FieldName = "School Name";
                        newvalue.OldValue = $scope.Schools.data.filter(function (Schools) { return SpecialtyBoards.SchoolID == newvalue.OldValue })[0].Name;
                        newvalue.NewValue = $scope.Schools.data.filter(function (Schools) { return SpecialtyBoards.SchoolID == newvalue.NewValue })[0].Name;
                    };
                    if (newvalue.FieldName == "QualificationDegreeID") {
                        newvalue.FieldName = "Degree Name";
                        newvalue.OldValue = $scope.QualificationDegrees.data.filter(function (QualificationDegrees) { return QualificationDegrees.QualificationDegreeID == newvalue.OldValue })[0].Title;
                        newvalue.NewValue = $scope.QualificationDegrees.data.filter(function (QualificationDegrees) { return QualificationDegrees.QualificationDegreeID == newvalue.NewValue })[0].Title;
                    };
                    if (newvalue.FieldName == "FacilityPracticeTypeID") {
                        newvalue.FieldName = "Practice Type";
                        newvalue.OldValue = $scope.LocationPracticeTypes.data.filter(function (LocationPracticeTypes) { return LocationPracticeTypes.FacilityPracticeTypeID == newvalue.OldValue })[0].Title;
                        newvalue.NewValue = $scope.LocationPracticeTypes.data.filter(function (LocationPracticeTypes) { return LocationPracticeTypes.FacilityPracticeTypeID == newvalue.NewValue })[0].Title;
                    };
                    if (newvalue.FieldName == "PracticeServiceQuestionID") {
                        newvalue.FieldName = "Services";
                        newvalue.OldValue = $scope.PracticeServiceQuestions.data.filter(function (PracticeServiceQuestions) { return PracticeServiceQuestions.PracticeServiceQuestionID == newvalue.OldValue })[0].Title;
                        newvalue.NewValue = $scope.PracticeServiceQuestions.data.filter(function (PracticeServiceQuestions) { return PracticeServiceQuestions.PracticeServiceQuestionID == newvalue.NewValue })[0].Title;
                    };
                    if (newvalue.FieldName == "PracticeOpenStatusQuestionID") {
                        newvalue.FieldName = "Open Status Question";
                        newvalue.OldValue = $scope.PracticeOpenStatusQuestions.data.filter(function (PracticeOpenStatusQuestions) { return PracticeOpenStatusQuestions.PracticeOpenStatusQuestionID == newvalue.OldValue })[0].Title;
                        newvalue.NewValue = $scope.PracticeOpenStatusQuestions.data.filter(function (PracticeOpenStatusQuestions) { return PracticeOpenStatusQuestions.PracticeOpenStatusQuestionID == newvalue.NewValue })[0].Title;
                    };
                    if (newvalue.FieldName == "FacilityAccessibilityQuestionId") {
                        newvalue.FieldName = "Accessibilities";
                        newvalue.OldValue = $scope.PracticeAccessibilityQuestions.data.filter(function (PracticeAccessibilityQuestions) { return PracticeAccessibilityQuestions.FacilityAccessibilityQuestionId == newvalue.OldValue })[0].Title;
                        newvalue.NewValue = $scope.PracticeAccessibilityQuestions.data.filter(function (PracticeAccessibilityQuestions) { return PracticeAccessibilityQuestions.FacilityAccessibilityQuestionId == newvalue.NewValue })[0].Title;
                    };
                    if (newvalue.FieldName == "MilitaryRankID") {
                        newvalue.FieldName = "Military Rank";
                        newvalue.OldValue = $scope.MilitaryRanks.data.filter(function (MilitaryRanks) { return MilitaryRanks.MilitaryRankID == newvalue.OldValue })[0].Title;
                        newvalue.NewValue = $scope.MilitaryRanks.data.filter(function (MilitaryRanks) { return MilitaryRanks.MilitaryRankID == newvalue.NewValue })[0].Title;
                    };
                    if (newvalue.FieldName == "MilitaryPresentDutyID") {
                        newvalue.FieldName = "Military Present Duty";
                        newvalue.OldValue = $scope.MilitaryPresentDuties.data.filter(function (MilitaryPresentDuties) { return MilitaryPresentDuties.MilitaryPresentDutyID == newvalue.OldValue })[0].Title;
                        newvalue.NewValue = $scope.MilitaryPresentDuties.data.filter(function (MilitaryPresentDuties) { return MilitaryPresentDuties.MilitaryPresentDutyID == newvalue.NewValue })[0].Title;
                    };
                    if (newvalue.FieldName == "MilitaryDischargeID") {
                        newvalue.FieldName = "Military Discharge ";
                        newvalue.OldValue = $scope.MilitaryDischarges.data.filter(function (MilitaryDischarges) { return MilitaryDischarges.MilitaryDischargeID == newvalue.OldValue })[0].Title;
                        newvalue.NewValue = $scope.MilitaryDischarges.data.filter(function (MilitaryDischarges) { return MilitaryDischarges.MilitaryDischargeID == newvalue.NewValue })[0].Title;
                    };
                    if (newvalue.FieldName == "MilitaryBranchID") {
                        newvalue.FieldName = "Military Branch";
                        newvalue.OldValue = $scope.MilitaryBranches.data.filter(function (MilitaryBranches) { return MilitaryBranches.MilitaryBranchID == newvalue.OldValue })[0].Title;
                        newvalue.NewValue = $scope.MilitaryBranches.data.filter(function (MilitaryBranches) { return MilitaryBranches.MilitaryBranchID == newvalue.NewValue })[0].Title;
                    };
                    if (newvalue.FieldName == "InsuranceCarrierID") {
                        newvalue.FieldName = "Insurance Carrier Name";
                        newvalue.OldValue = $scope.InsuranceCarriers.data.filter(function (InsuranceCarriers) { return InsuranceCarriers.InsuranceCarrierID == newvalue.OldValue })[0].Name;
                        newvalue.NewValue = $scope.InsuranceCarriers.data.filter(function (InsuranceCarriers) { return InsuranceCarriers.InsuranceCarrierID == newvalue.NewValue })[0].Name;
                    };
                    if (newvalue.FieldName == "InsuranceCarrierAddressID") {
                        newvalue.FieldName = "Location Name";
                        newvalue.OldValue = $scope.InsuranceCarrierAddresses.data.filter(function (InsuranceCarrierAddresses) { return InsuranceCarrierAddresses.InsuranceCarrierAddressID == newvalue.OldValue })[0].LocationName;
                        newvalue.NewValue = $scope.InsuranceCarrierAddresses.data.filter(function (InsuranceCarrierAddresses) { return InsuranceCarrierAddresses.InsuranceCarrierAddressID == newvalue.NewValue })[0].LocationName;
                    };
                    if (newvalue.FieldName == "HospitalContactPersonID") {
                        newvalue.FieldName = "Contact Person Name";
                        newvalue.OldValue = $scope.HospitalContactPersons.data.filter(function (HospitalContactPersons) { return HospitalContactPersons.HospitalContactPersonID == newvalue.OldValue })[0].ContactPersonName;
                        newvalue.NewValue = $scope.HospitalContactPersons.data.filter(function (HospitalContactPersons) { return HospitalContactPersons.HospitalContactPersonID == newvalue.NewValue })[0].ContactPersonName;
                    };
                    if (newvalue.FieldName == "HospitalID") {
                        newvalue.FieldName = "Hospital Name";
                        newvalue.OldValue = $scope.Hospitals.data.filter(function (Hospitals) { return Hospitals.HospitalID == newvalue.OldValue })[0].HospitalName;
                        newvalue.NewValue = $scope.Hospitals.data.filter(function (Hospitals) { return Hospitals.HospitalID == newvalue.NewValue })[0].HospitalName;
                    };
                    if (newvalue.FieldName == "DEAScheduleID") {
                        newvalue.FieldName = "DEA Schedule";
                        newvalue.OldValue = $scope.DEASchedules.data.filter(function (DEASchedules) { return DEASchedules.DEAScheduleID == newvalue.OldValue })[0].ScheduleTitle;
                        newvalue.NewValue = $scope.DEASchedules.data.filter(function (DEASchedules) { return DEASchedules.DEAScheduleID == newvalue.NewValue })[0].ScheduleTitle;
                    };
                    if (newvalue.FieldName == "CertificationID") {
                        newvalue.FieldName = "Certification Name";
                        newvalue.OldValue = $scope.Certificates.data.filter(function (Certificates) { return Certificates.CertificationID == newvalue.OldValue })[0].Name;
                        newvalue.NewValue = $scope.Certificates.data.filter(function (Certificates) { return Certificates.CertificationID == newvalue.NewValue })[0].Name;
                    };
                    if (newvalue.FieldName == "AdmittingPrivilegeID") {
                        newvalue.FieldName = "Admitting Privileges";
                        newvalue.OldValue = $scope.AdmittingPrivileges.data.filter(function (AdmittingPrivileges) { return AdmittingPrivileges.AdmittingPrivilegeID == newvalue.OldValue })[0].Title;
                        newvalue.NewValue = $scope.AdmittingPrivileges.data.filter(function (AdmittingPrivileges) { return AdmittingPrivileges.AdmittingPrivilegeID == newvalue.NewValue })[0].Title;
                    };
                    if (newvalue.FieldName == "VisaTypeID") {
                        newvalue.FieldName = "Visa Type";
                        newvalue.OldValue = $scope.VisaTypes.data.filter(function (VisaTypes) { return VisaTypes.VisaTypeID == newvalue.OldValue })[0].Title;
                        newvalue.NewValue = $scope.VisaTypes.data.filter(function (VisaTypes) { return VisaTypes.VisaTypeID == newvalue.NewValue })[0].Title;
                    };
                    if (newvalue.FieldName == "VisaStatusID") {
                        newvalue.FieldName = "Visa Status";
                        newvalue.OldValue = $scope.VisaStatus.data.filter(function (VisaStatus) { return VisaStatus.VisaStatusID == newvalue.OldValue })[0].Title;
                        newvalue.NewValue = $scope.VisaStatus.data.filter(function (VisaStatus) { return VisaStatus.VisaStatusID == newvalue.NewValue })[0].Title;
                    };
                    if (newvalue.FieldName == "ProviderLevelID") {
                        newvalue.FieldName = "Provider Level";
                        newvalue.OldValue = $scope.ProviderLevels.data.filter(function (ProviderLevels) { return ProviderLevels.ProviderLevelID == newvalue.OldValue })[0].Name;
                        newvalue.NewValue = $scope.ProviderLevels.data.filter(function (ProviderLevels) { return ProviderLevels.ProviderLevelID == newvalue.NewValue })[0].Name;
                    };
                    if (newvalue.FieldName == "OrganizationID") {
                        newvalue.FieldName = "Organization Name";
                        newvalue.OldValue = $scope.Organizations.data.filter(function (Organizations) { return Organizations.OrganizationID == newvalue.OldValue })[0].Name;
                        newvalue.NewValue = $scope.Organizations.data.filter(function (Organizations) { return Organizations.OrganizationID == newvalue.NewValue })[0].Name;
                    };
                    if (newvalue.FieldName == "FacilityAccessibilityQuestionId") {
                        newvalue.FieldName = "Accessibilities";
                        newvalue.OldValue = $scope.AccessibilityQuestions.data.filter(function (AccessibilityQuestions) { return AccessibilityQuestions.SchoolID == newvalue.OldValue })[0].Title;
                        newvalue.NewValue = $scope.AccessibilityQuestions.data.filter(function (AccessibilityQuestions) { return AccessibilityQuestions.SchoolID == newvalue.NewValue })[0].Title;
                    };
                    if (newvalue.FieldName == "FacilityPracticeTypeID") {
                        newvalue.FieldName = "Practice Type Name";
                        newvalue.OldValue = $scope.PracticeTypes.data.filter(function (PracticeTypes) { return PracticeTypes.FacilityPracticeTypeID == newvalue.OldValue })[0].Title;
                        newvalue.NewValue = $scope.PracticeTypes.data.filter(function (PracticeTypes) { return PracticeTypes.FacilityPracticeTypeID == newvalue.NewValue })[0].Title;
                    };
                    if (newvalue.FieldName == "FacilityServiceQuestionID") {
                        newvalue.FieldName = "Services";
                        newvalue.OldValue = $scope.ServiceQuestions.data.filter(function (ServiceQuestions) { return ServiceQuestions.FacilityServiceQuestionID == newvalue.OldValue })[0].Title;
                        newvalue.NewValue = $scope.ServiceQuestions.data.filter(function (ServiceQuestions) { return ServiceQuestions.FacilityServiceQuestionID == newvalue.NewValue })[0].Title;
                    };
                    if (newvalue.FieldName == "PracticeOpenStatusQuestionID") {
                        newvalue.FieldName = "Open Practice Status";
                        newvalue.OldValue = $scope.OpenPracticeStatusQuestions.data.filter(function (OpenPracticeStatusQuestions) { return OpenPracticeStatusQuestions.PracticeOpenStatusQuestionID == newvalue.OldValue })[0].Title;
                        newvalue.NewValue = $scope.OpenPracticeStatusQuestions.data.filter(function (OpenPracticeStatusQuestions) { return OpenPracticeStatusQuestions.PracticeOpenStatusQuestionID == newvalue.NewValue })[0].Title;
                    };
                    if (newvalue.FieldName == "PracticePaymentAndRemittanceID") {
                        newvalue.FieldName = "Payment And Remittance";
                        newvalue.OldValue = $scope.PaymentAndRemittance.data.filter(function (PaymentAndRemittance) { return PaymentAndRemittance.PracticePaymentAndRemittanceID == newvalue.OldValue })[0].Name;
                        newvalue.NewValue = $scope.PaymentAndRemittance.data.filter(function (PaymentAndRemittance) { return PaymentAndRemittance.PracticePaymentAndRemittanceID == newvalue.NewValue })[0].Name;
                    };
                    if (newvalue.FieldName == "EmployeeID") {
                        newvalue.FieldName = "First Name";
                        newvalue.OldValue = $scope.BusinessContactPerson.data.filter(function (BusinessContactPerson) { return BusinessContactPerson.EmployeeID == newvalue.OldValue })[0].FirstName;
                        newvalue.NewValue = $scope.BusinessContactPerson.data.filter(function (BusinessContactPerson) { return BusinessContactPerson.EmployeeID == newvalue.NewValue })[0].FirstName;
                    };
                    if (newvalue.FieldName == "OrganizationGroupID") {
                        newvalue.FieldName = "Organization Name";
                        newvalue.OldValue = $scope.OrganizationGroups.data.filter(function (OrganizationGroups) { return OrganizationGroups.OrganizationGroupID == newvalue.OldValue })[0].GroupName;
                        newvalue.NewValue = $scope.OrganizationGroups.data.filter(function (OrganizationGroups) { return OrganizationGroups.OrganizationGroupID == newvalue.NewValue })[0].GroupName;
                    };
                    if (newvalue.FieldName == "EmployeeID") {
                        newvalue.FieldName = "First Name";
                        newvalue.OldValue = $scope.BillingContact.data.filter(function (BillingContact) { return BillingContact.EmployeeID == newvalue.OldValue })[0].FirstName;
                        newvalue.NewValue = $scope.BillingContact.data.filter(function (BillingContact) { return BillingContact.EmployeeID == newvalue.NewValue })[0].FirstName;
                    };
                    if (newvalue.FieldName == "OrganizationID") {
                        newvalue.FieldName = "Facility Name";
                        newvalue.OldValue = $scope.Facilities.data.filter(function (Facilities) { return Facilities.OrganizationID == newvalue.OldValue })[0].Name;
                        newvalue.NewValue = $scope.Facilities.data.filter(function (Facilities) { return Facilities.OrganizationID == newvalue.NewValue })[0].Name;
                    };
                    if (newvalue.FieldName == "EmployeeID") {
                        newvalue.FieldName = "First Name";
                        newvalue.OldValue = $scope.CredentialingContact.data.filter(function (CredentialingContact) { return CredentialingContact.EmployeeID == newvalue.OldValue })[0].FirstName;
                        newvalue.NewValue = $scope.CredentialingContact.data.filter(function (CredentialingContact) { return CredentialingContact.EmployeeID == newvalue.NewValue })[0].FirstName;
                    };
                    if (newvalue.FieldName == "StateLicenseStatusID") {
                        newvalue.FieldName = "State License Status";
                        newvalue.OldValue = $scope.LicenseStatus.data.filter(function (LicenseStatus) { return LicenseStatus.StateLicenseStatusID == newvalue.OldValue })[0].Title;
                        newvalue.NewValue = $scope.LicenseStatus.data.filter(function (LicenseStatus) { return LicenseStatus.StateLicenseStatusID == newvalue.NewValue })[0].Title;
                    };

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
                                newvalue.FieldName = 'HospitalLocation';
                                newvalue.OldValue = data[0].LocationName;
                                newvalue.NewValue = data[1].LocationName;
                            },
                            error: function (e) {

                            }
                        });
                    };

                    $scope.SectionDisplayData[i].fields[j] = newvalue;
                }
            }
            for (var i = 0; i < $scope.SectionDisplayData.length; i++) {
                for (var j = 0; j < $scope.SectionDisplayData[i].fields.length; j++) {

                    if ($scope.SectionDisplayData[i].fields[j].FieldName == 'Fax') {
                        $scope.SectionDisplayData[i].fields.splice($scope.SectionDisplayData[i].fields.indexOf($scope.SectionDisplayData[i].fields[j]), 1);
                        j--;
                    }
                    if ($scope.SectionDisplayData[i].fields[j].FieldName == 'Phone') {
                        $scope.SectionDisplayData[i].fields.splice($scope.SectionDisplayData[i].fields.indexOf($scope.SectionDisplayData[i].fields[j]), 1);
                        j--;
                    }
                    if ($scope.SectionDisplayData[i].fields[j].FieldName == 'Telephone') {
                        $scope.SectionDisplayData[i].fields.splice($scope.SectionDisplayData[i].fields.indexOf($scope.SectionDisplayData[i].fields[j]), 1);
                        j--;
                    }
                    if ($scope.SectionDisplayData[i].fields[j].FieldName == 'EmployerFax') {
                        $scope.SectionDisplayData[i].fields.splice($scope.SectionDisplayData[i].fields.indexOf($scope.SectionDisplayData[i].fields[j]), 1);
                        j--;
                    }
                    if ($scope.SectionDisplayData[i].fields[j].FieldName == 'EmployerMobile') {
                        $scope.SectionDisplayData[i].fields.splice($scope.SectionDisplayData[i].fields.indexOf($scope.SectionDisplayData[i].fields[j]), 1);
                        j--;
                    }
                }
            }
            //========================================
            $scope.showUpdateError = false;
        } else {
            $scope.SectionDisplayData = null;
        }
    }
    $scope.SectionDisplayData = null;

    $scope.openDiv = function (className,idName) {

        $('.' + className).hide();
        $('#' + idName).show();
    }

}]);
