UpdateAndRenewalsApp.factory('UpdateAndRenewalsFactory', ['$q', '$rootScope', '$filter', '$timeout', '$window', '$log', 'MasterSettings', 'UpdateAndRenewalsMasterService', function ($q, $rootScope, $filter, $timeout, $window, $log, MasterSettings, UpdateAndRenewalsMasterService) {
    function demoFromHTML(TableID) {
        var pdf = new jsPDF('p', 'pt', 'a4');
        source = $(TableID)[0];
        specialElementHandlers = {
            '#bypassme': function (element, renderer) {
                return true
            }
        };
        margins = {
            top: 10,
            bottom: 60,
            left: 10,
            width: 1000
        };
        pdf.fromHTML(
        source,
        margins.left,
        margins.top, {
            'width': margins.width,
            'elementHandlers': specialElementHandlers
        },

        function (dispose) {
            pdf.save('Test.pdf');
        }, margins);
    }
    function DateTimeConveter(value) {
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
    function getPageForUpdateAndHistory(start, number, params) {
        var deferred = $q.defer();
        $rootScope.filtered = params.search.predicateObject ? $filter('filter')($rootScope.TempProfileUpdates, params.search.predicateObject, params.sort.predicate, params.sort.reverse) : $rootScope.TempProfileUpdates;
        if (params.sort.predicate) {
            $rootScope.filtered = $filter('orderBy')($rootScope.filtered, params.sort.predicate, params.sort.reverse);
        }
        var result = $rootScope.filtered.slice(start, start + number);
        deferred.resolve({
            data: result,
            numberOfPages: Math.ceil($rootScope.filtered.length / number)
        });
        return deferred.promise;
    }
    function getPageCredentialingRequest(start, number, params) {
        var deferred = $q.defer();
        $rootScope.filtered = params.search.predicateObject ? $filter('filter')($rootScope.TempCredentialingRequests, params.search.predicateObject, params.sort.predicate, params.sort.reverse) : $rootScope.TempCredentialingRequests;
        if (params.sort.predicate) {
            $rootScope.filtered = $filter('orderBy')($rootScope.filtered, params.sort.predicate, params.sort.reverse);
        }
        var result = $rootScope.filtered.slice(start, start + number);
        deferred.resolve({
            data: result,
            numberOfPages: Math.ceil($rootScope.filtered.length / number)
        });
        return deferred.promise;
    }
    function resetTableState(tableState) {
        if (tableState !== undefined) {
            tableState.sort = {};
            tableState.pagination.start = 0;
            tableState.search.predicateObject = {};
            return tableState;
        }
    }
    function exportToTable(type, tableId) {
        switch (type) {
            case "Excel":
                angular.element(tableId).tableExport({ type: 'excel', escape: 'false', ignoreColumn: '[7]' })
                break;
            case "CSV":
                angular.element(tableId).tableExport({ type: 'csv', escape: 'false', ignoreColumn: '[7]' })
                break;
            case "Pdf":
                demoFromHTML(tableId);
                break;
        }
    }
    function LowerCaseTrimedData(value) {
        return value.trim().toLowerCase();
    }
    function modalDismiss() {
        angular.element('#requestForApprovalModal').modal('hide');
        angular.element('#profileUpdateModal').modal('hide');
        angular.element('body').removeClass('modal-open');
        angular.element('.modal-backdrop').remove();
    }
    function AddSpacesInWords(input) {
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
    function getValue(data) {
        for (var i = 0; i < data.length; i++) {
            var newvalue = data[i];

            if (newvalue.FieldName == "ProviderTypeId") {
                newvalue.FieldName = "Provider Type";
                if (newvalue.OldValue != null)
                    newvalue.OldValue = $rootScope.ProviderTypes.data.filter(function (ProviderType) { return ProviderType.ProviderTypeID == newvalue.OldValue })[0].Title;
                if (newvalue.NewValue != null)
                    newvalue.NewValue = $rootScope.ProviderTypes.data.filter(function (ProviderType) { return ProviderType.ProviderTypeID == newvalue.NewValue })[0].Title;
            };
            if (newvalue.FieldName == "ProviderTypeID") {
                newvalue.FieldName = "Provider Type";
                if (newvalue.OldValue != null)
                    newvalue.OldValue = $rootScope.ProviderTypes.data.filter(function (ProviderType) { return ProviderType.ProviderTypeID == newvalue.OldValue })[0].Title;
                if (newvalue.NewValue != null)
                    newvalue.NewValue = $rootScope.ProviderTypes.data.filter(function (ProviderType) { return ProviderType.ProviderTypeID == newvalue.NewValue })[0].Title;
            };
            if (newvalue.FieldName == "ProviderTitleID") {
                newvalue.FieldName = "Provider Title";
                if (newvalue.OldValue != null)
                    newvalue.OldValue = $rootScope.ProviderTypes.data.filter(function (ProviderType) { return ProviderType.ProviderTypeID == newvalue.OldValue })[0].Title;
                if (newvalue.NewValue != null)
                    newvalue.NewValue = $rootScope.ProviderTypes.data.filter(function (ProviderType) { return ProviderType.ProviderTypeID == newvalue.NewValue })[0].Title;
            };
            if (newvalue.FieldName == "StaffCategoryID") {
                newvalue.FieldName = "Staff Category";
                if (newvalue.OldValue != null)
                    newvalue.OldValue = $rootScope.StaffCategories.data.filter(function (StaffCategories) { return StaffCategories.StaffCategoryID == newvalue.OldValue })[0].Title;
                if (newvalue.NewValue != null)
                    newvalue.NewValue = $rootScope.StaffCategories.data.filter(function (StaffCategories) { return StaffCategories.StaffCategoryID == newvalue.NewValue })[0].Title;
            };
            if (newvalue.FieldName == "SpecialtyID") {
                newvalue.FieldName = "Specialty Name";
                if (newvalue.NewValue != null)
                    newvalue.NewValue = $rootScope.Specialties.data.filter(function (Specialties) { return Specialties.SpecialtyID == newvalue.NewValue })[0].Name;
                if (newvalue.OldValue != null)
                    newvalue.OldValue = $rootScope.Specialties.data.filter(function (Specialties) { return Specialties.SpecialtyID == newvalue.OldValue })[0].Name;
            };
            if (newvalue.FieldName == "SpecialtyBoardID") {
                newvalue.FieldName = "Board Name";
                if (newvalue.OldValue != null)
                    newvalue.OldValue = $rootScope.SpecialtyBoards.data.filter(function (SpecialtyBoards) { return SpecialtyBoards.SpecialtyBoardID == newvalue.OldValue })[0].Name;
                if (newvalue.NewValue != null)
                    newvalue.NewValue = $rootScope.SpecialtyBoards.data.filter(function (SpecialtyBoards) { return SpecialtyBoards.SpecialtyBoardID == newvalue.NewValue })[0].Name;
            };
            if (newvalue.FieldName == "SchoolID") {
                newvalue.FieldName = "School Name";
                if (newvalue.OldValue != null)
                    newvalue.OldValue = $rootScope.Schools.data.filter(function (Schools) { return SpecialtyBoards.SchoolID == newvalue.OldValue })[0].Name;
                if (newvalue.NewValue != null)
                    newvalue.NewValue = $rootScope.Schools.data.filter(function (Schools) { return SpecialtyBoards.SchoolID == newvalue.NewValue })[0].Name;
            };
            if (newvalue.FieldName == "QualificationDegreeID") {
                newvalue.FieldName = "Degree Name";
                if (newvalue.OldValue != null)
                    newvalue.OldValue = $rootScope.QualificationDegrees.data.filter(function (QualificationDegrees) { return QualificationDegrees.QualificationDegreeID == newvalue.OldValue })[0].Title;
                if (newvalue.NewValue != null)
                    newvalue.NewValue = $rootScope.QualificationDegrees.data.filter(function (QualificationDegrees) { return QualificationDegrees.QualificationDegreeID == newvalue.NewValue })[0].Title;
            };
            if (newvalue.FieldName == "FacilityPracticeTypeID") {
                newvalue.FieldName = "Practice Type";
                if (newvalue.OldValue != null)
                    newvalue.OldValue = $rootScope.LocationPracticeTypes.data.filter(function (LocationPracticeTypes) { return LocationPracticeTypes.FacilityPracticeTypeID == newvalue.OldValue })[0].Title;
                if (newvalue.NewValue != null)
                    newvalue.NewValue = $rootScope.LocationPracticeTypes.data.filter(function (LocationPracticeTypes) { return LocationPracticeTypes.FacilityPracticeTypeID == newvalue.NewValue })[0].Title;
            };
            if (newvalue.FieldName == "PracticeServiceQuestionID") {
                newvalue.FieldName = "Services";
                if (newvalue.OldValue != null)
                    newvalue.OldValue = $rootScope.PracticeServiceQuestions.data.filter(function (PracticeServiceQuestions) { return PracticeServiceQuestions.PracticeServiceQuestionID == newvalue.OldValue })[0].Title;
                if (newvalue.NewValue != null)
                    newvalue.NewValue = $rootScope.PracticeServiceQuestions.data.filter(function (PracticeServiceQuestions) { return PracticeServiceQuestions.PracticeServiceQuestionID == newvalue.NewValue })[0].Title;
            };
            if (newvalue.FieldName == "PracticeOpenStatusQuestionID") {
                newvalue.FieldName = "Open Status Question";
                if (newvalue.OldValue != null)
                    newvalue.OldValue = $rootScope.PracticeOpenStatusQuestions.data.filter(function (PracticeOpenStatusQuestions) { return PracticeOpenStatusQuestions.PracticeOpenStatusQuestionID == newvalue.OldValue })[0].Title;
                if (newvalue.NewValue != null)
                    newvalue.NewValue = $rootScope.PracticeOpenStatusQuestions.data.filter(function (PracticeOpenStatusQuestions) { return PracticeOpenStatusQuestions.PracticeOpenStatusQuestionID == newvalue.NewValue })[0].Title;
            };
            if (newvalue.FieldName == "FacilityAccessibilityQuestionId") {
                newvalue.FieldName = "Accessibilities";
                if (newvalue.OldValue != null)
                    newvalue.OldValue = $rootScope.PracticeAccessibilityQuestions.data.filter(function (PracticeAccessibilityQuestions) { return PracticeAccessibilityQuestions.FacilityAccessibilityQuestionId == newvalue.OldValue })[0].Title;
                if (newvalue.NewValue != null)
                    newvalue.NewValue = $rootScope.PracticeAccessibilityQuestions.data.filter(function (PracticeAccessibilityQuestions) { return PracticeAccessibilityQuestions.FacilityAccessibilityQuestionId == newvalue.NewValue })[0].Title;
            };
            if (newvalue.FieldName == "MilitaryRankID") {
                newvalue.FieldName = "Military Rank";
                if (newvalue.OldValue != null)
                    newvalue.OldValue = $rootScope.MilitaryRanks.data.filter(function (MilitaryRanks) { return MilitaryRanks.MilitaryRankID == newvalue.OldValue })[0].Title;
                if (newvalue.NewValue != null)
                    newvalue.NewValue = $rootScope.MilitaryRanks.data.filter(function (MilitaryRanks) { return MilitaryRanks.MilitaryRankID == newvalue.NewValue })[0].Title;
            };
            if (newvalue.FieldName == "MilitaryPresentDutyID") {
                newvalue.FieldName = "Military Present Duty";
                if (newvalue.OldValue != null)
                    newvalue.OldValue = $rootScope.MilitaryPresentDuties.data.filter(function (MilitaryPresentDuties) { return MilitaryPresentDuties.MilitaryPresentDutyID == newvalue.OldValue })[0].Title;
                if (newvalue.NewValue != null)
                    newvalue.NewValue = $rootScope.MilitaryPresentDuties.data.filter(function (MilitaryPresentDuties) { return MilitaryPresentDuties.MilitaryPresentDutyID == newvalue.NewValue })[0].Title;
            };
            if (newvalue.FieldName == "MilitaryDischargeID") {
                newvalue.FieldName = "Military Discharge ";
                if (newvalue.OldValue != null)
                    newvalue.OldValue = $rootScope.MilitaryDischarges.data.filter(function (MilitaryDischarges) { return MilitaryDischarges.MilitaryDischargeID == newvalue.OldValue })[0].Title;
                if (newvalue.NewValue != null)
                    newvalue.NewValue = $rootScope.MilitaryDischarges.data.filter(function (MilitaryDischarges) { return MilitaryDischarges.MilitaryDischargeID == newvalue.NewValue })[0].Title;
            };
            if (newvalue.FieldName == "MilitaryBranchID") {
                newvalue.FieldName = "Military Branch";
                if (newvalue.OldValue != null)
                    newvalue.OldValue = $rootScope.MilitaryBranches.data.filter(function (MilitaryBranches) { return MilitaryBranches.MilitaryBranchID == newvalue.OldValue })[0].Title;
                if (newvalue.NewValue != null)
                    newvalue.NewValue = $rootScope.MilitaryBranches.data.filter(function (MilitaryBranches) { return MilitaryBranches.MilitaryBranchID == newvalue.NewValue })[0].Title;
            };
            if (newvalue.FieldName == "InsuranceCarrierID") {
                newvalue.FieldName = "Insurance Carrier Name";
                if (newvalue.OldValue != null)
                    newvalue.OldValue = $rootScope.InsuranceCarriers.data.filter(function (InsuranceCarriers) { return InsuranceCarriers.InsuranceCarrierID == newvalue.OldValue })[0].Name;
                if (newvalue.NewValue != null)
                    newvalue.NewValue = $rootScope.InsuranceCarriers.data.filter(function (InsuranceCarriers) { return InsuranceCarriers.InsuranceCarrierID == newvalue.NewValue })[0].Name;
            };
            if (newvalue.FieldName == "InsuranceCarrierAddressID") {
                newvalue.FieldName = "Location Name";
                if (newvalue.OldValue != null)
                    newvalue.OldValue = $rootScope.InsuranceCarrierAddresses.data.filter(function (InsuranceCarrierAddresses) { return InsuranceCarrierAddresses.InsuranceCarrierAddressID == newvalue.OldValue })[0].LocationName;
                if (newvalue.NewValue != null)
                    newvalue.NewValue = $rootScope.InsuranceCarrierAddresses.data.filter(function (InsuranceCarrierAddresses) { return InsuranceCarrierAddresses.InsuranceCarrierAddressID == newvalue.NewValue })[0].LocationName;
            };
            if (newvalue.FieldName == "HospitalContactPersonID") {
                newvalue.FieldName = "Contact Person Name";
                if (newvalue.OldValue != null)
                    newvalue.OldValue = $rootScope.HospitalContactPersons.data.filter(function (HospitalContactPersons) { return HospitalContactPersons.HospitalContactPersonID == newvalue.OldValue })[0].ContactPersonName;
                if (newvalue.NewValue != null)
                    newvalue.NewValue = $rootScope.HospitalContactPersons.data.filter(function (HospitalContactPersons) { return HospitalContactPersons.HospitalContactPersonID == newvalue.NewValue })[0].ContactPersonName;
            };
            if (newvalue.FieldName == "HospitalID") {
                newvalue.FieldName = "Hospital Name";
                if (newvalue.OldValue != null)
                    newvalue.OldValue = $rootScope.Hospitals.data.filter(function (Hospitals) { return Hospitals.HospitalID == newvalue.OldValue })[0].HospitalName;
                if (newvalue.NewValue != null)
                    newvalue.NewValue = $rootScope.Hospitals.data.filter(function (Hospitals) { return Hospitals.HospitalID == newvalue.NewValue })[0].HospitalName;
            };
            if (newvalue.FieldName == "DEAScheduleID") {
                newvalue.FieldName = "DEA Schedule";
                if (newvalue.OldValue != null)
                    newvalue.OldValue = $rootScope.DEASchedules.data.filter(function (DEASchedules) { return DEASchedules.DEAScheduleID == newvalue.OldValue })[0].ScheduleTitle;
                if (newvalue.NewValue != null)
                    newvalue.NewValue = $rootScope.DEASchedules.data.filter(function (DEASchedules) { return DEASchedules.DEAScheduleID == newvalue.NewValue })[0].ScheduleTitle;
            };
            if (newvalue.FieldName == "CertificationID") {
                newvalue.FieldName = "Certification Name";
                if (newvalue.OldValue != null)
                    newvalue.OldValue = $rootScope.Certificates.data.filter(function (Certificates) { return Certificates.CertificationID == newvalue.OldValue })[0].Name;
                if (newvalue.NewValue != null)
                    newvalue.NewValue = $rootScope.Certificates.data.filter(function (Certificates) { return Certificates.CertificationID == newvalue.NewValue })[0].Name;
            };
            if (newvalue.FieldName == "AdmittingPrivilegeID") {
                newvalue.FieldName = "Admitting Privileges";
                if (newvalue.OldValue != null)
                    newvalue.OldValue = $rootScope.AdmittingPrivileges.data.filter(function (AdmittingPrivileges) { return AdmittingPrivileges.AdmittingPrivilegeID == newvalue.OldValue })[0].Title;
                if (newvalue.NewValue != null)
                    newvalue.NewValue = $rootScope.AdmittingPrivileges.data.filter(function (AdmittingPrivileges) { return AdmittingPrivileges.AdmittingPrivilegeID == newvalue.NewValue })[0].Title;
            };
            if (newvalue.FieldName == "VisaTypeID") {
                newvalue.FieldName = "Visa Type";
                if (newvalue.OldValue != null)
                    newvalue.OldValue = $rootScope.VisaTypes.data.filter(function (VisaTypes) { return VisaTypes.VisaTypeID == newvalue.OldValue })[0].Title;
                if (newvalue.NewValue != null)
                    newvalue.NewValue = $rootScope.VisaTypes.data.filter(function (VisaTypes) { return VisaTypes.VisaTypeID == newvalue.NewValue })[0].Title;
            };
            if (newvalue.FieldName == "VisaStatusID") {
                newvalue.FieldName = "Visa Status";
                if (newvalue.OldValue != null)
                    newvalue.OldValue = $rootScope.VisaStatus.data.filter(function (VisaStatus) { return VisaStatus.VisaStatusID == newvalue.OldValue })[0].Title;
                if (newvalue.NewValue != null)
                    newvalue.NewValue = $rootScope.VisaStatus.data.filter(function (VisaStatus) { return VisaStatus.VisaStatusID == newvalue.NewValue })[0].Title;
            };
            if (newvalue.FieldName == "ProviderLevelID") {
                newvalue.FieldName = "Provider Level";
                if (newvalue.OldValue != null)
                    newvalue.OldValue = $rootScope.ProviderLevels.data.filter(function (ProviderLevels) { return ProviderLevels.ProviderLevelID == newvalue.OldValue })[0].Name;
                if (newvalue.NewValue != null)
                    newvalue.NewValue = $rootScope.ProviderLevels.data.filter(function (ProviderLevels) { return ProviderLevels.ProviderLevelID == newvalue.NewValue })[0].Name;
            };
            if (newvalue.FieldName == "OrganizationID") {
                newvalue.FieldName = "Organization Name";
                if (newvalue.OldValue != null)
                    newvalue.OldValue = $rootScope.Organizations.data.filter(function (Organizations) { return Organizations.OrganizationID == newvalue.OldValue })[0].Name;
                if (newvalue.NewValue != null)
                    newvalue.NewValue = $rootScope.Organizations.data.filter(function (Organizations) { return Organizations.OrganizationID == newvalue.NewValue })[0].Name;
            };
            if (newvalue.FieldName == "FacilityAccessibilityQuestionId") {
                newvalue.FieldName = "Accessibilities";
                if (newvalue.OldValue != null)
                    newvalue.OldValue = $rootScope.AccessibilityQuestions.data.filter(function (AccessibilityQuestions) { return AccessibilityQuestions.SchoolID == newvalue.OldValue })[0].Title;
                if (newvalue.NewValue != null)
                    newvalue.NewValue = $rootScope.AccessibilityQuestions.data.filter(function (AccessibilityQuestions) { return AccessibilityQuestions.SchoolID == newvalue.NewValue })[0].Title;
            };
            if (newvalue.FieldName == "FacilityPracticeTypeID") {
                newvalue.FieldName = "Practice Type Name";
                if (newvalue.OldValue != null)
                    newvalue.OldValue = $rootScope.PracticeTypes.data.filter(function (PracticeTypes) { return PracticeTypes.FacilityPracticeTypeID == newvalue.OldValue })[0].Title;
                if (newvalue.NewValue != null)
                    newvalue.NewValue = $rootScope.PracticeTypes.data.filter(function (PracticeTypes) { return PracticeTypes.FacilityPracticeTypeID == newvalue.NewValue })[0].Title;
            };
            if (newvalue.FieldName == "FacilityServiceQuestionID") {
                newvalue.FieldName = "Services";
                if (newvalue.OldValue != null)
                    newvalue.OldValue = $rootScope.ServiceQuestions.data.filter(function (ServiceQuestions) { return ServiceQuestions.FacilityServiceQuestionID == newvalue.OldValue })[0].Title;
                if (newvalue.NewValue != null)
                    newvalue.NewValue = $rootScope.ServiceQuestions.data.filter(function (ServiceQuestions) { return ServiceQuestions.FacilityServiceQuestionID == newvalue.NewValue })[0].Title;
            };
            if (newvalue.FieldName == "PracticeOpenStatusQuestionID") {
                newvalue.FieldName = "Open Practice Status";
                if (newvalue.OldValue != null)
                    newvalue.OldValue = $rootScope.OpenPracticeStatusQuestions.data.filter(function (OpenPracticeStatusQuestions) { return OpenPracticeStatusQuestions.PracticeOpenStatusQuestionID == newvalue.OldValue })[0].Title;
                if (newvalue.NewValue != null)
                    newvalue.NewValue = $rootScope.OpenPracticeStatusQuestions.data.filter(function (OpenPracticeStatusQuestions) { return OpenPracticeStatusQuestions.PracticeOpenStatusQuestionID == newvalue.NewValue })[0].Title;
            };
            if (newvalue.FieldName == "PracticePaymentAndRemittanceID") {
                newvalue.FieldName = "Payment And Remittance";
                if (newvalue.OldValue != null)
                    newvalue.OldValue = $rootScope.PaymentAndRemittance.data.filter(function (PaymentAndRemittance) { return PaymentAndRemittance.PracticePaymentAndRemittanceID == newvalue.OldValue })[0].Name;
                if (newvalue.NewValue != null)
                    newvalue.NewValue = $rootScope.PaymentAndRemittance.data.filter(function (PaymentAndRemittance) { return PaymentAndRemittance.PracticePaymentAndRemittanceID == newvalue.NewValue })[0].Name;
            };
            if (newvalue.FieldName == "EmployeeID") {
                newvalue.FieldName = "First Name";
                if (newvalue.OldValue != null)
                    newvalue.OldValue = $rootScope.BusinessContactPerson.data.filter(function (BusinessContactPerson) { return BusinessContactPerson.EmployeeID == newvalue.OldValue })[0].FirstName;
                if (newvalue.NewValue != null)
                    newvalue.NewValue = $rootScope.BusinessContactPerson.data.filter(function (BusinessContactPerson) { return BusinessContactPerson.EmployeeID == newvalue.NewValue })[0].FirstName;
            };
            if (newvalue.FieldName == "OrganizationGroupID") {
                newvalue.FieldName = "Organization Name";
                if (newvalue.OldValue != null)
                    newvalue.OldValue = $rootScope.OrganizationGroups.data.filter(function (OrganizationGroups) { return OrganizationGroups.OrganizationGroupID == newvalue.OldValue })[0].GroupName;
                if (newvalue.NewValue != null)
                    newvalue.NewValue = $rootScope.OrganizationGroups.data.filter(function (OrganizationGroups) { return OrganizationGroups.OrganizationGroupID == newvalue.NewValue })[0].GroupName;
            };
            if (newvalue.FieldName == "EmployeeID") {
                newvalue.FieldName = "First Name";
                if (newvalue.OldValue != null)
                    newvalue.OldValue = $rootScope.BillingContact.data.filter(function (BillingContact) { return BillingContact.EmployeeID == newvalue.OldValue })[0].FirstName;
                if (newvalue.NewValue != null)
                    newvalue.NewValue = $rootScope.BillingContact.data.filter(function (BillingContact) { return BillingContact.EmployeeID == newvalue.NewValue })[0].FirstName;
            };
            if (newvalue.FieldName == "OrganizationID") {
                newvalue.FieldName = "Facility Name";
                if (newvalue.OldValue != null)
                    newvalue.OldValue = $rootScope.Facilities.data.filter(function (Facilities) { return Facilities.OrganizationID == newvalue.OldValue })[0].Name;
                if (newvalue.NewValue != null)
                    newvalue.NewValue = $rootScope.Facilities.data.filter(function (Facilities) { return Facilities.OrganizationID == newvalue.NewValue })[0].Name;
            };
            if (newvalue.FieldName == "EmployeeID") {
                newvalue.FieldName = "First Name";
                if (newvalue.OldValue != null)
                    newvalue.OldValue = $rootScope.CredentialingContact.data.filter(function (CredentialingContact) { return CredentialingContact.EmployeeID == newvalue.OldValue })[0].FirstName;
                if (newvalue.NewValue != null)
                    newvalue.NewValue = $rootScope.CredentialingContact.data.filter(function (CredentialingContact) { return CredentialingContact.EmployeeID == newvalue.NewValue })[0].FirstName;
            };
            if (newvalue.FieldName == "StateLicenseStatusID") {
                newvalue.FieldName = "State License Status";
                if (newvalue.OldValue != null)
                    newvalue.OldValue = $rootScope.LicenseStatus.data.filter(function (LicenseStatus) { return LicenseStatus.StateLicenseStatusID == newvalue.OldValue })[0].Title;
                if (newvalue.NewValue != null)
                    newvalue.NewValue = $rootScope.LicenseStatus.data.filter(function (LicenseStatus) { return LicenseStatus.StateLicenseStatusID == newvalue.NewValue })[0].Title;
            };

            if (newvalue.FieldName == "FacilityId") {
                newvalue.FieldName = "Facility Name";
                if (newvalue.OldValue != null)
                    newvalue.OldValue = $rootScope.Facilities.data.filter(function (Facilities) { return Facilities.FacilityID == newvalue.OldValue })[0].Name;
                if (newvalue.NewValue != null)
                    newvalue.NewValue = $rootScope.Facilities.data.filter(function (Facilities) { return Facilities.FacilityID == newvalue.NewValue })[0].Name;
            };

            if (newvalue.FieldName == "IsAuthorizedToWorkInUS") {
                if (newvalue.OldValue == '0')
                    newvalue.OldValue = null;
                if (newvalue.NewValue == '0')
                    newvalue.NewValue = null;
            };


            if ($rootScope.TemporaryObject.Section == 'Identification And License' && $rootScope.TemporaryObject.SubSection == 'State License') {

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

            if ($rootScope.TemporaryObject.SubSection == 'Specialty Details') {
                if (data[i].FieldName == 'SpecialtyBoardCertifiedDetail') {
                    data.splice(data.indexOf(data[i]), 1);

                }
                if (data[i].FieldName == 'SpecialtyBoardNotCertifiedDetail') {
                    data.splice(data.indexOf(data[i]), 1);

                }
            }
            if ($rootScope.TemporaryObject.Section == 'Practice Location' && $rootScope.TemporaryObject.SubSection == 'Practice Location Detail') {
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
    function ResizeTheConvertedData(data) {
        if (data.NewData.indexOf('"PracticeLocationDetailID":') > -1) {
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
        return data;
    }
    function CreateTrackerObject(Data, ApprovalStatus, TemporaryObject) {
        return{
            tracker: {
                    TrackerId: Data.ProfileUpdatesTrackerId,
                    ApprovalStatus: ApprovalStatus,
                    RejectionReason: TemporaryObject.RejectionReason === undefined ? "" : TemporaryObject.RejectionReason
            },
            modification: Data.Modification,
            approvedStatus: Data.ApprovalStatus
        };
    }

    return {
        getPageForUpdateAndHistory: getPageForUpdateAndHistory,
        getPageCredentialingRequest:getPageCredentialingRequest,
        resetTableState: resetTableState,
        exportToTable: exportToTable,
        demoFromHTML: demoFromHTML,
        LowerCaseTrimedData: LowerCaseTrimedData,
        modalDismiss: modalDismiss,
        AddSpacesInWords: AddSpacesInWords,
        getValue: getValue,
        ResizeTheConvertedData: ResizeTheConvertedData,
        CreateTrackerObject: CreateTrackerObject
    }
}]);