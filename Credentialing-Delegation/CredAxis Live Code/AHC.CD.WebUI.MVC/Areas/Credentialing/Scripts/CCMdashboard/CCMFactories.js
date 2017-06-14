


CCMDashboard.factory('CCMDashboardFactory', ['$q', '$rootScope', '$filter', '$timeout', '$window', '$log', function ($q, $rootScope, $filter, $timeout, $window, $log) {

    function getPage(start, number, params) {
        var deferred = $q.defer();

        $rootScope.filtered = params.search.predicateObject ? $filter('filter')($rootScope.TempCCMAppointments, params.search.predicateObject, params.sort.predicate, params.sort.reverse) : $rootScope.TempCCMAppointments;
        if (params.sort.predicate) {
            $rootScope.filtered = $filter('orderBy')($rootScope.filtered, params.sort.predicate, params.sort.reverse);
        }
        if ($rootScope.ToHighLightRowObject !== "") {
            var Origin = $rootScope.filtered.indexOf($rootScope.ToHighLightRowObject);
            var temp = $rootScope.filtered[0];
            $rootScope.filtered[0] = $rootScope.ToHighLightRowObject;
            $rootScope.filtered[Origin] = temp;
        }
        var result = $rootScope.filtered.slice(start, start + number);
        deferred.resolve({
            data: result,
            numberOfPages: Math.ceil($rootScope.filtered.length / number)
        });
        return deferred.promise;
    }
    function getFilteredAppointmentdataByPlan(PlanName) {
        return $filter('filter')($rootScope.CCMAppointments, { PlanName: PlanName })[0];
    }
    function resetTableState(tableState) {
        if (tableState !== undefined) {
            tableState.sort = {};
            tableState.pagination.start = 0;
            tableState.search.predicateObject = {};
            return tableState;
        }
    }
    function getFilteredAppointmentdataByAppointmentDate(AppointmentDate) {
        return $filter('filter')($rootScope.CCMAppointments, { AppointmentDate: AppointmentDate });
    }
    function exportToTable(type, tableId) {
        switch (type) {
            case "Excel":
                angular.element(tableId).tableExport({ type: 'excel', escape: 'false', ignoreColumn: '[9]' })
                break;
            case "CSV":
                angular.element(tableId).tableExport({ type: 'csv', escape: 'false', ignoreColumn: '[9]' })
                break;
            case "Pdf":
                angular.element(tableId).tableExport({ type: 'pdf', pdfFontSize: '10', escape: 'false', ignoreColumn: '[9]', htmlContent: 'true' })
                break;
        }
    }
    function ClearSelectRowStatus() {        
        angular.forEach($filter('filter')($rootScope.CCMAppointments, { SelectStatus: true }), function (object, index) {
            $rootScope.CCMAppointments[$rootScope.CCMAppointments.indexOf(object)].SelectStatus = false;
        });
        return $rootScope.CCMAppointments;
    }
    function ConvertDate(value) {
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
            shortDate = year+'-'+ monthString + '-' + dayString ;
            //shortDate = dayString + 'th ' + monthName + ',' + year;
        }
        return shortDate;
    };
   
    function resetFormForValidation(form) {
        form.removeData('validator');
        form.removeData('unobtrusiveValidation');
        $.validator.unobtrusive.parse(form);
    };


    function modalDismiss(ID) {
        angular.element('#'+ID).modal('hide');
        $('body').removeClass('modal-open');
        $('.modal-backdrop').remove();
    }

    function GetCount(obj,AppointmentStatus) {
        return obj.filter(function (x) { return (x.Status == AppointmentStatus || AppointmentStatus == 'All') }).length;
    }
    function loadCounts(obj) {
        var Counts={};
        Counts.All = GetCount(obj,'All');
        Counts.Approved = GetCount(obj, 'Approved');
        Counts.Pending = GetCount(obj, 'Onhold');
        Counts.Pending += GetCount(obj, 'New');
        Counts.Rejected = GetCount(obj, 'Rejected');
        return Counts;
    }
    function formatCCMdata(data) {
        data.CredentialingInfo.AppointmentDate = ConvertDateFormat(data.CredentialingInfo.AppointmentDate);
        data.CredentialingInfo.WelcomeLetterMailedDate = data.CredentialingInfo.WelcomeLetterMailedDate != null ? ConvertDateFormat(data.CredentialingInfo.WelcomeLetterMailedDate) : null;
        data.CredentialingInfo.SignedDate = data.CredentialingInfo.SignedDate != null ? ConvertDateFormat(data.CredentialingInfo.SignedDate) : null;
        $rootScope.tempObject = angular.copy(data);
    }
    function formatPSVData(data) {
        {
            $rootScope.tempObject.PSVdata = [];
            var formattedData = [];
            for (var i in data) {
                var VerificationData = new Object();
                if (data[i].VerificationData != null)
                    VerificationData = jQuery.parseJSON(data[i].VerificationData);
                var VerificationDate = ConvertDateFormat(data[i].VerificationDate)

                formattedData.push({ Id: data[i].ProfileVerificationParameterId, info: { ProfileVerficationParameterObj: data[i].ProfileVerificationParameter, VerificationResultObj: data[i].VerificationResult, VerificationData: VerificationData, VerificationDate: VerificationDate } });
                //formattedData.push({ Id: data[i].ProfileVerificationParameterId, info: { ProfileVerficationParameterObj: data[i].ProfileVerificationParameter, VerificationResultObj: data[i].VerificationResult, VerificationData: data[i].VerificationData } });
            }

            var UniqueIds = [];
            UniqueIds.push(formattedData[0].Id);
            for (var i = 1; i < formattedData.length; i++) {

                var CurrObj = formattedData[i];
                var flag = 0;
                for (var j = 0; j < UniqueIds.length; j++) {
                    if (CurrObj.Id == UniqueIds[j]) {
                        flag = 1;
                    }
                }
                if (flag == 0) {
                    UniqueIds.push(CurrObj.Id);
                }
            }


            for (var i = 0; i < UniqueIds.length; i++) {
                var info = [];
                for (var j = 0; j < formattedData.length; j++) {
                    if (UniqueIds[i] == formattedData[j].Id) {
                        info.push(formattedData[j].info);
                    }

                }
              
                $rootScope.tempObject.PSVdata.push({ Id: UniqueIds[i], Info: info });
            }
        };
    }
    function formatPSVId(data) {
        for (var i = 0; i < data.length; i++) {
            if (data[i].Code == 'SL') {
                $rootScope.tempObject.StateLicenseParameterID = data[i].ProfileVerificationParameterId;
            }
            if (data[i].Code == 'BC') {
                $rootScope.tempObject.BoardCertificationParameterID = data[i].ProfileVerificationParameterId;
            }
            if (data[i].Code == 'DEA') {
                $rootScope.tempObject.DEAParameterID = data[i].ProfileVerificationParameterId;
            }
            if (data[i].Code == 'CDS') {
                $rootScope.tempObject.CDSParameterID = data[i].ProfileVerificationParameterId;
            }
            if (data[i].Code == 'NPDB') {
                $rootScope.tempObject.NPDBParameterID = data[i].ProfileVerificationParameterId;
            }
            if (data[i].Code == 'MOPT') {
                $rootScope.tempObject.MOPTParameterID = data[i].ProfileVerificationParameterId;
            }
            if (data[i].Code == 'OIG') {
                $rootScope.tempObject.OIGParameterID = data[i].ProfileVerificationParameterId;
            }
        }
    }
    var ConvertDateFormat = function (value) {
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
    LicenseTypes = [];
    createLicenseArray = function () {

        LicenseTypes.push({ LicenseTypeID: 1, LicenseTypeName: "State License", Licenses: [], LicenseHistories: [] });
        LicenseTypes.push({ LicenseTypeID: 2, LicenseTypeName: "Other Legal Name Document", Licenses: [], LicenseHistories: [] });
        LicenseTypes.push({ LicenseTypeID: 3, LicenseTypeName: "Personal Identification Document", Licenses: [], LicenseHistories: [] });
        LicenseTypes.push({ LicenseTypeID: 4, LicenseTypeName: "Birth Document", Licenses: [], LicenseHistories: [] });
        LicenseTypes.push({ LicenseTypeID: 5, LicenseTypeName: "CV Document", Licenses: [], LicenseHistories: [] });
        LicenseTypes.push({ LicenseTypeID: 6, LicenseTypeName: "Citizenship Document", Licenses: [], LicenseHistories: [] });
        LicenseTypes.push({ LicenseTypeID: 7, LicenseTypeName: "Federal DEA Document", Licenses: [], LicenseHistories: [] });
        LicenseTypes.push({ LicenseTypeID: 8, LicenseTypeName: "Medicare Document", Licenses: [], LicenseHistories: [] });
        LicenseTypes.push({ LicenseTypeID: 9, LicenseTypeName: "Medicaid Document", Licenses: [], LicenseHistories: [] });
        LicenseTypes.push({ LicenseTypeID: 10, LicenseTypeName: "CDSC Document", Licenses: [], LicenseHistories: [] });
        LicenseTypes.push({ LicenseTypeID: 11, LicenseTypeName: "Education Document", Licenses: [], LicenseHistories: [] });
        LicenseTypes.push({ LicenseTypeID: 12, LicenseTypeName: "ECFMG Document", Licenses: [], LicenseHistories: [] });
        LicenseTypes.push({ LicenseTypeID: 13, LicenseTypeName: "Program Document", Licenses: [], LicenseHistories: [] });
        LicenseTypes.push({ LicenseTypeID: 14, LicenseTypeName: "CME Certifications", Licenses: [], LicenseHistories: [] });
        LicenseTypes.push({ LicenseTypeID: 15, LicenseTypeName: "Specialty Board Certificates", Licenses: [], LicenseHistories: [] });
        LicenseTypes.push({ LicenseTypeID: 16, LicenseTypeName: "Hospital Privilege Document", Licenses: [], LicenseHistories: [] });
        LicenseTypes.push({ LicenseTypeID: 17, LicenseTypeName: "Professional Liability Document", Licenses: [], LicenseHistories: [] });
        LicenseTypes.push({ LicenseTypeID: 18, LicenseTypeName: "WorkExperience Document", Licenses: [], LicenseHistories: [] });
        LicenseTypes.push({ LicenseTypeID: 19, LicenseTypeName: "Contract", Licenses: [], LicenseHistories: [] });
        LicenseTypes.push({ LicenseTypeID: 20, LicenseTypeName: "Other", Licenses: [], LicenseHistories: [] });

    }

    function formatDocuments(data) {
        $rootScope.tempObject.Docs = [];
        LicenseTypes = [];
        createLicenseArray();
        if (data.StateLicenses != null) {

            if (data.StateLicenses.length > 0) {

                FormatStateLicense(data.StateLicenses);

            }

        }

        if (data.OtherLegalNames != null) {

            if (data.OtherLegalNames.length > 0) {

                FormatOtherLegalNamesDoc(data.OtherLegalNames);

            }

        }

        if (data.PersonalIdentification != null) {

            FormatPersonalIdentificationDoc(data.PersonalIdentification);

        }

        if (data.BirthInformation != null) {

            FormatBirthInformationDoc(data.BirthInformation);

        }

        if (data.CVInformation != null) {

            FormatCVInformationDoc(data.CVInformation);

        }

        if (data.VisaDetail != null) {

            FormatVisaInfoDoc(data.VisaDetail);

        }

        if (data.FederalDEAInformations != null) {

            if (data.FederalDEAInformations.length > 0) {

                FormatFederalDEAInformationDoc(data.FederalDEAInformations);

            }

        }

        if (data.MedicareInformations != null) {

            if (data.MedicareInformations.length > 0) {

                FormatMedicareInformationDoc(data.MedicareInformations);

            }

        }

        if (data.MedicaidInformations != null) {

            if (data.MedicaidInformations.length > 0) {

                FormatMedicaidInformationDoc(data.MedicaidInformations);

            }

        }

        if (data.CDSCInformations != null) {

            if (data.CDSCInformations.length > 0) {

                FormatCDSCInformationDoc(data.CDSCInformations);

            }

        }

        if (data.EducationDetails != null) {

            if (data.EducationDetails.length > 0) {

                FormatEducationDetailDoc(data.EducationDetails);

            }

        }

        if (data.ECFMGDetail != null) {

            FormatECFMGDetailDoc(data.ECFMGDetail);

        }

        if (data.ProgramDetails != null) {

            if (data.ProgramDetails.length > 0) {

                FormatProgramDetailsDoc(data.ProgramDetails);

            }

        }

        if (data.CMECertifications != null) {

            if (data.CMECertifications.length > 0) {

                FormatCMECertificationsDoc(data.CMECertifications);

            }

        }

        if (data.SpecialtyDetails != null) {

            if (data.SpecialtyDetails.length > 0) {

                FormatSpecialtyBoardCertifiedDetailDoc(data.SpecialtyDetails);

            }

        }

        if (data.HospitalPrivilegeInformation != null) {

            if (data.HospitalPrivilegeInformation.HospitalPrivilegeDetails != null) {

                if (data.HospitalPrivilegeInformation.HospitalPrivilegeDetails.length > 0) {

                    FormatHospitalPrivilegeDetailDoc(data.HospitalPrivilegeInformation.HospitalPrivilegeDetails);

                }

            }

        }

        if (data.ProfessionalLiabilityInfoes != null) {

            if (data.ProfessionalLiabilityInfoes.length > 0) {

                FormatProfessionalLiabilityInfoDoc(data.ProfessionalLiabilityInfoes);

            }

        }

        if (data.ProfessionalWorkExperiences != null) {

            if (data.ProfessionalWorkExperiences.length > 0) {

                FormatProfessionalWorkExperienceDoc(data.ProfessionalWorkExperiences);

            }

        }

        if (data.ContractInfoes != null) {

            if (data.ContractInfoes.length > 0) {

                FormatContractInfoeDoc(data.ContractInfoes);

            }

        }

        if (data.OtherDocuments != null) {

            if (data.OtherDocuments.length > 0) {

                FormatOtherDocument(data.OtherDocuments);

            }

        }

        combine();

    }

    FormatStateLicense = function (Docs) {

        //Licenses = [];
        //LicenseHistories = [];

        for (var i = 0; i < Docs.length; i++) {

            if (Docs[i].StateLicenseDocumentPath != null) {

                LicenseTypes[0].Licenses.push({ LicenseID: Docs[i].StateLicenseInformationID, LicenseName: "ML" + (i + 1), LicenseDocPath: Docs[i].StateLicenseDocumentPath, ModifiedDate: Docs[i].LastModifiedDate, Description: "State License" });

                if (Docs[i].StateLicenseInfoHistory != null) {

                    for (var j = 0; j < Docs[i].StateLicenseInfoHistory.length; j++) {

                        if (Docs[i].StateLicenseInfoHistory[j] != null) {

                            LicenseTypes[0].LicenseHistories.push({ LicenseID: Docs[i].StateLicenseInfoHistory[j].StatelicenseInfoHistoryID, LicenseName: "ML" + (j + 1), LicenseDocPath: Docs[i].StateLicenseInfoHistory[j].StatelicenseDocumentPath, removeDate: Docs[i].StateLicenseInfoHistory[j].LastModifiedDate });

                        }

                    }

                }

            }

        }

        //if (Licenses.length != 0) {

        //LicenseTypes.push({ LicenseTypeID: 1, LicenseTypeName: "State License", Licenses: Licenses, LicenseHistories: LicenseHistories });

        //}

    }

    FormatOtherLegalNamesDoc = function (Docs) {

        //Licenses = [];
        //LicenseHistories = [];

        for (var i = 0; i < Docs.length; i++) {

            if (Docs[i].DocumentPath != null) {

                LicenseTypes[1].Licenses.push({ LicenseID: Docs[i].OtherLegalNameID, LicenseName: "OLN" + (i + 1), LicenseDocPath: Docs[i].DocumentPath, ModifiedDate: Docs[i].LastModifiedDate, Description: "Other Legal Name" });

                if (Docs[i].OtherLegalNameHistory != null) {

                    LicenseTypes[1].LicenseHistories.push({ LicenseID: Docs[i].OtherLegalNameHistory[j].OtherLegalNameHistoryID, LicenseName: "OLN" + (j + 1), LicenseDocPath: Docs[i].OtherLegalNameHistory[j].DocumentPath, removeDate: Docs[i].OtherLegalNameHistory[j].LastModifiedDate });

                }

            }

        }

        //if (Licenses.length != 0) {

        //LicenseTypes.push({ LicenseTypeID: 2, LicenseTypeName: "Other Legal Name Document", Licenses: Licenses, LicenseHistories: LicenseHistories });

        //}

    }

    FormatPersonalIdentificationDoc = function (Docs) {

        //Licenses = [];
        //LicenseHistories = [];

        if (Docs.SSNCertificatePath != null) {

            LicenseTypes[2].Licenses.push({ LicenseID: Docs.PersonalIdentificationID, LicenseName: "SSN", LicenseDocPath: Docs.SSNCertificatePath, ModifiedDate: Docs.LastModifiedDate, Description: "Social Security Number" });

        }

        if (Docs.DLCertificatePath != null) {

            LicenseTypes[2].Licenses.push({ LicenseID: Docs.PersonalIdentificationID, LicenseName: "DL", LicenseDocPath: Docs.DLCertificatePath, ModifiedDate: Docs.LastModifiedDate, Description: "Driver License" });

        }

        //if (Licenses.length != 0) {

        //LicenseTypes.push({ LicenseTypeID: 3, LicenseTypeName: "Personal Identification Document", Licenses: Licenses, LicenseHistories: LicenseHistories });

        //}

    }

    FormatBirthInformationDoc = function (Docs) {

        //Licenses = [];
        //LicenseHistories = [];

        if (Docs.BirthCertificatePath != null) {

            LicenseTypes[3].Licenses.push({ LicenseID: Docs.BirthInformationID, LicenseName: "Birth Certificate", LicenseDocPath: Docs.BirthCertificatePath, ModifiedDate: Docs.LastModifiedDate, Description: "Birth Certificate" });

            LicenseTypes[3].LicenseHistories.push({ LicenseID: 1, LicenseName: "Birth Certificate", LicenseDocPath: Docs.BirthCertificatePath, removeDate: Docs.LastModifiedDate });

        }

        //LicenseTypes.push({ LicenseTypeID: 4, LicenseTypeName: "Birth Document", Licenses: Licenses, LicenseHistories: LicenseHistories });

    }

    FormatCVInformationDoc = function (Docs) {

        //Licenses = [];
        //LicenseHistories = [];

        if (Docs.CVDocumentPath != null) {

            LicenseTypes[4].Licenses.push({ LicenseID: Docs.CVInformationID, LicenseName: "CV", LicenseDocPath: Docs.CVDocumentPath, ModifiedDate: Docs.LastModifiedDate, Description: "Curriculum Vitae" });

        }

        //LicenseTypes.push({ LicenseTypeID: 5, LicenseTypeName: "CV Document", Licenses: Licenses, LicenseHistories: LicenseHistories });

    }

    FormatVisaInfoDoc = function (Docs) {

        //Licenses = [];
        //LicenseHistories = [];

        if (Docs.VisaInfo != null) {

            if (Docs.VisaInfo.VisaCertificatePath != null) {

                LicenseTypes[5].Licenses.push({ LicenseID: Docs.VisaInfo.VisaInfoID, LicenseName: "VISA", LicenseDocPath: Docs.VisaInfo.VisaCertificatePath, ModifiedDate: Docs.LastModifiedDate, Description: "Visa" });

            }

            if (Docs.VisaInfo.GreenCardCertificatePath != null) {

                LicenseTypes[5].Licenses.push({ LicenseID: Docs.VisaInfo.VisaInfoID, LicenseName: "GreenCard", LicenseDocPath: Docs.VisaInfo.GreenCardCertificatePath, ModifiedDate: Docs.LastModifiedDate, Description: "Green Card" });

            }

            if (Docs.VisaInfo.NationalIDCertificatePath != null) {

                LicenseTypes[5].Licenses.push({ LicenseID: Docs.VisaInfo.VisaInfoID, LicenseName: "National Identification", LicenseDocPath: Docs.VisaInfo.NationalIDCertificatePath, ModifiedDate: Docs.LastModifiedDate, Description: "National Identification Certificate" });

            }

            //LicenseTypes.push({ LicenseTypeID: 6, LicenseTypeName: "Citizenship Document", Licenses: Licenses, LicenseHistories: LicenseHistories });

        }

    }

    FormatFederalDEAInformationDoc = function (Docs) {

        Licenses = [];
        LicenseHistories = [];

        for (var i = 0; i < Docs.length; i++) {

            if (Docs[i].DEALicenceCertPath != null) {

                LicenseTypes[6].Licenses.push({ LicenseID: Docs[i].FederalDEAInformationID, LicenseName: "DEA" + (i + 1), LicenseDocPath: Docs[i].DEALicenceCertPath, ModifiedDate: Docs[i].LastModifiedDate, Description: "Drug Enforcement Administration" });

                if (Docs[i].FederalDEAInfoHistory != null) {

                    for (var j = 0; j < Docs[i].FederalDEAInfoHistory.length; j++) {

                        LicenseTypes[6].LicenseHistories.push({ LicenseID: Docs[i].FederalDEAInfoHistory[j].FederalDEAInfoHistoryID, LicenseName: "DEA" + (j + 1), LicenseDocPath: Docs[i].FederalDEAInfoHistory[j].FederalDEADocumentPath, removeDate: Docs[i].FederalDEAInfoHistory[j].LastModifiedDate });

                    }

                }

            }

        }

        //if (Licenses.length != 0) {

        //LicenseTypes.push({ LicenseTypeID: 7, LicenseTypeName: "Federal DEA Document", Licenses: Licenses, LicenseHistories: LicenseHistories });

        //}

    }

    FormatMedicareInformationDoc = function (Docs) {

        Licenses = [];
        LicenseHistories = [];

        for (var i = 0; i < Docs.length; i++) {

            if (Docs[i].CertificatePath != null) {

                LicenseTypes[7].Licenses.push({ LicenseID: Docs[i].MedicareInformationID, LicenseName: "Medicare" + (i + 1), LicenseDocPath: Docs[i].CertificatePath, ModifiedDate: Docs[i].LastModifiedDate, Description: "Medicare" });

                if (Docs[i].MedicareInformationHistory != null) {

                    LicenseTypes[7].LicenseHistories.push({ LicenseID: Docs[i].MedicareInformationHistory.MedicareInformationHistoryID, LicenseName: "Medicare" + (j + 1), LicenseDocPath: Docs[i].MedicareInformationHistory.CertificatePath, removeDate: Docs[i].MedicareInformationHistory.LastModifiedDate });

                }

            }

        }

        //if (Licenses.length != 0) {

        //LicenseTypes.push({ LicenseTypeID: 8, LicenseTypeName: "Medicare Document", Licenses: Licenses, LicenseHistories: LicenseHistories });

        //}

    }

    FormatMedicaidInformationDoc = function (Docs) {

        Licenses = [];
        LicenseHistories = [];

        for (var i = 0; i < Docs.length; i++) {

            if (Docs[i].CertificatePath != null) {

                LicenseTypes[8].Licenses.push({ LicenseID: Docs[i].MedicaidInformationID, LicenseName: "Medicaid" + (i + 1), LicenseDocPath: Docs[i].CertificatePath, ModifiedDate: Docs[i].LastModifiedDate, Description: "Medicaid" });

                if (Docs[i].MedicaidInformationHistory != null) {

                    LicenseTypes[8].LicenseHistories.push({ LicenseID: Docs[i].MedicaidInformationHistory.MedicaidInformationHistoryID, LicenseName: "Medicade" + (j + 1), LicenseDocPath: Docs[i].MedicaidInformationHistory.CertificatePath, removeDate: Docs[i].MedicaidInformationHistory.LastModifiedDate });

                }

            }

        }

        //if (Licenses.length != 0) {

        //LicenseTypes.push({ LicenseTypeID: 9, LicenseTypeName: "Medicaid Document", Licenses: Licenses, LicenseHistories: LicenseHistories });

        //}

    }

    FormatCDSCInformationDoc = function (Docs) {

        Licenses = [];
        LicenseHistories = [];

        for (var i = 0; i < Docs.length; i++) {

            if (Docs[i].CDSCCerificatePath != null) {

                LicenseTypes[9].Licenses.push({ LicenseID: Docs[i].CDSCInformationID, LicenseName: "CDS" + (i + 1), LicenseDocPath: Docs[i].CDSCCerificatePath, ModifiedDate: Docs[i].LastModifiedDate, Description: "Central Drug Standard Control" });

                if (Docs[i].CDSCInfoHistory != null) {

                    for (var j = 0; j < Docs[i].CDSCInfoHistory.length; j++) {

                        LicenseTypes[9].LicenseHistories.push({ LicenseID: Docs[i].CDSCInfoHistory[j].CDSCInfoHistoryID, LicenseName: "CDS" + (j + 1), LicenseDocPath: Docs[i].CDSCInfoHistory[j].CDSCCerificatePath, removeDate: Docs[i].CDSCInfoHistory[j].LastModifiedDate });

                    }

                }

            }

        }

        //if (Licenses.length != 0) {

        //LicenseTypes.push({ LicenseTypeID: 10, LicenseTypeName: "CDSC Document", Licenses: Licenses, LicenseHistories: LicenseHistories });

        //}

    }

    countUG = 0;
    countG = 0;

    FormatEducationDetailDoc = function (Docs) {

        Licenses = [];
        LicenseHistories = [];

        for (var i = 0; i < Docs.length; i++) {

            if (Docs[i].CertificatePath != null) {

                if (Docs[i].CertificatePath != null) {

                    if (Docs[i].QualificationType == "UnderGraduate") {

                        LicenseTypes[10].Licenses.push({ LicenseID: Docs[i].EducationDetailID, LicenseName: "UG" + (countUG + 1), LicenseDocPath: Docs[i].CertificatePath, ModifiedDate: Docs[i].LastModifiedDate, Description: "Under Graduate" });
                        countUG++;

                        if (Docs[i].EducationDetailHistory != null) {

                            LicenseTypes[10].LicenseHistories.push({ LicenseID: Docs[i].EducationDetailHistory.EducationDetailHistoryID, LicenseName: "UG" + (countUG + 1), LicenseDocPath: Docs[i].EducationDetailHistory.CertificatePath, removeDate: Docs[i].EducationDetailHistory.LastModifiedDate });

                        }

                    } else if (Docs[i].QualificationType == "Graduate") {

                        LicenseTypes[10].Licenses.push({ LicenseID: Docs[i].EducationDetailID, LicenseName: "Grad" + (countG + 1), LicenseDocPath: Docs[i].CertificatePath, ModifiedDate: Docs[i].LastModifiedDate, Description: "Graduate" });
                        countG++;

                        if (Docs[i].EducationDetailHistory != null) {

                            LicenseTypes[10].LicenseHistories.push({ LicenseID: Docs[i].EducationDetailHistory.EducationDetailHistoryID, LicenseName: "Grad" + (countG + 1), LicenseDocPath: Docs[i].EducationDetailHistory.CertificatePath, removeDate: Docs[i].EducationDetailHistory.LastModifiedDate });

                        }

                    }

                }

            }

        }

        //if (Licenses.length != 0) {

        //LicenseTypes.push({ LicenseTypeID: 11, LicenseTypeName: "Education Document", Licenses: Licenses, LicenseHistories: LicenseHistories });

        //}

    }

    FormatECFMGDetailDoc = function (Docs) {

        Licenses = [];
        LicenseHistories = [];

        if (Docs.ECFMGCertPath != null) {

            LicenseTypes[11].Licenses.push({ LicenseID: Docs.ECFMGDetailID, LicenseName: "ECFMG", LicenseDocPath: Docs.ECFMGCertPath, ModifiedDate: Docs.LastModifiedDate, Description: "Education Commission for Foreign Medical Graduates" });

            LicenseTypes[11].LicenseHistories.push({ LicenseID: 2, LicenseName: "ECFMG", LicenseDocPath: Docs.ECFMGCertPath, removeDate: Docs.LastModifiedDate });

        }

        //LicenseTypes.push({ LicenseTypeID: 12, LicenseTypeName: "ECFMG Document", Licenses: Licenses, LicenseHistories: LicenseHistories });

    }

    countIntern = 0;
    countFell = 0;
    countRes = 0;
    countOther = 0;

    FormatProgramDetailsDoc = function (Docs) {

        Licenses = [];
        LicenseHistories = [];

        for (var i = 0; i < Docs.length; i++) {

            if (Docs[i].DocumentPath != null) {

                if (Docs[i].ProgramType == "Internship") {

                    LicenseTypes[12].Licenses.push({ LicenseID: Docs[i].ProgramDetailID, LicenseName: "Intern" + (countIntern + 1), LicenseDocPath: Docs[i].DocumentPath, ModifiedDate: Docs[i].LastModifiedDate, Description: "Internship" });

                    countIntern++;

                    if (Docs.ProgramDetailHistory != null) {

                        LicenseTypes[12].LicenseHistories.push({ LicenseID: Docs[i].ProgramDetailHistory.ProgramDetailHistoryID, LicenseName: "Intern" + (countIntern + 1), LicenseDocPath: Docs[i].ProgramDetailHistory.DocumentPath, removeDate: Docs[i].ProgramDetailHistory.LastModifiedDate });

                    }

                } else if (Docs[i].ProgramType == "Fellowship") {

                    LicenseTypes[12].Licenses.push({ LicenseID: Docs[i].ProgramDetailID, LicenseName: "Fell" + (countFell + 1), LicenseDocPath: Docs[i].DocumentPath, ModifiedDate: Docs[i].LastModifiedDate, Description: "Fellowship" });

                    countFell++;

                    if (Docs.ProgramDetailHistory != null) {

                        LicenseTypes[12].LicenseHistories.push({ LicenseID: Docs[i].ProgramDetailHistory.ProgramDetailHistoryID, LicenseName: "Fell" + (countFell + 1), LicenseDocPath: Docs[i].ProgramDetailHistory.DocumentPath, removeDate: Docs[i].ProgramDetailHistory.LastModifiedDate });

                    }

                } else if (Docs[i].ProgramType == "Resident") {

                    LicenseTypes[12].Licenses.push({ LicenseID: Docs[i].ProgramDetailID, LicenseName: "Res" + (countRes + 1), LicenseDocPath: Docs[i].DocumentPath, ModifiedDate: Docs[i].LastModifiedDate, Description: "Resident" });

                    countRes++;

                    if (Docs.ProgramDetailHistory != null) {

                        LicenseTypes[12].LicenseHistories.push({ LicenseID: Docs[i].ProgramDetailHistory.ProgramDetailHistoryID, LicenseName: "Res" + (countRes + 1), LicenseDocPath: Docs[i].ProgramDetailHistory.DocumentPath, removeDate: Docs[i].ProgramDetailHistory.LastModifiedDate });

                    }

                } else if (Docs[i].ProgramType == "Other") {

                    LicenseTypes[12].Licenses.push({ LicenseID: Docs[i].ProgramDetailID, LicenseName: "Other" + (countOther + 1), LicenseDocPath: Docs[i].DocumentPath, ModifiedDate: Docs[i].LastModifiedDate, Description: "Other" });

                    countOther++;

                    if (Docs.ProgramDetailHistory != null) {

                        LicenseTypes[12].LicenseHistories.push({ LicenseID: Docs[i].ProgramDetailHistory.ProgramDetailHistoryID, LicenseName: "Other" + (countOther + 1), LicenseDocPath: Docs[i].ProgramDetailHistory.DocumentPath, removeDate: Docs[i].ProgramDetailHistory.LastModifiedDate });

                    }

                }

            }

        }

        //if (Licenses.length != 0) {

        //LicenseTypes.push({ LicenseTypeID: 13, LicenseTypeName: "Program Document", Licenses: Licenses, LicenseHistories: LicenseHistories });

        //}

    }

    FormatCMECertificationsDoc = function (Docs) {

        Licenses = [];
        LicenseHistories = [];

        for (var i = 0; i < Docs.length; i++) {

            if (Docs[i].CertificatePath != null) {

                LicenseTypes[13].Licenses.push({ LicenseID: Docs[i].CMECertificationID, LicenseName: "CME" + (i + 1), LicenseDocPath: Docs[i].CertificatePath, ModifiedDate: Docs[i].LastModifiedDate, Description: "Continuing Medical Education" });

                if (Docs[i].CMECertificationHistory != null) {

                    LicenseTypes[13].LicenseHistories.push({ LicenseID: Docs[i].CMECertificationHistory.CMECertificationHistoryID, LicenseName: "CME" + (j + 1), LicenseDocPath: Docs[i].CMECertificationHistory.CertificatePath, removeDate: Docs[i].CMECertificationHistory.LastModifiedDate });

                }

            }

        }

        //if (Licenses.length != 0) {

        //LicenseTypes.push({ LicenseTypeID: 14, LicenseTypeName: "CME Certifications", Licenses: Licenses, LicenseHistories: LicenseHistories });

        //}

    }

    FormatSpecialtyBoardCertifiedDetailDoc = function (Docs) {

        Licenses = [];
        LicenseHistories = [];

        for (var i = 0; i < Docs.length; i++) {

            if (Docs[i].SpecialtyBoardCertifiedDetail != null && Docs[i].SpecialtyBoardCertifiedDetail.BoardCertificatePath != null) {

                LicenseTypes[14].Licenses.push({ LicenseID: Docs[i].SpecialtyDetailID, LicenseName: "Board" + (i + 1), LicenseDocPath: Docs[i].SpecialtyBoardCertifiedDetail.BoardCertificatePath, ModifiedDate: Docs[i].LastModifiedDate, Description: "Specialty Board Certificate" });

                if (Docs[i].SpecialtyBoardCertifiedDetail.SpecialtyBoardCertifiedDetailHistory != null) {

                    for (var j = 0; j < Docs[i].SpecialtyBoardCertifiedDetail.SpecialtyBoardCertifiedDetailHistory.length; j++) {

                        LicenseTypes[14].LicenseHistories.push({ LicenseID: Docs[i].SpecialtyBoardCertifiedDetail.SpecialtyBoardCertifiedDetailHistory[j].SpecialtyBoardCertifiedDetailHistoryID, LicenseName: "Board" + (j + 1), LicenseDocPath: Docs[i].SpecialtyBoardCertifiedDetail.SpecialtyBoardCertifiedDetailHistory[j].BoardCertificatePath, removeDate: Docs[i].SpecialtyBoardCertifiedDetail.SpecialtyBoardCertifiedDetailHistory[j].LastModifiedDate });

                    }

                }

            }

        }

        //if (Licenses.length != 0) {

        //LicenseTypes.push({ LicenseTypeID: 15, LicenseTypeName: "Specialty Board Certificates", Licenses: Licenses, LicenseHistories: LicenseHistories });

        //}

    }

    FormatHospitalPrivilegeDetailDoc = function (Docs) {

        Licenses = [];
        LicenseHistories = [];
        var count = 1;

        for (var i = 0; i < Docs.length; i++) {

            if (Docs[i].HospitalPrevilegeLetterPath != null && Docs[i].Status != "Inactive") {

                LicenseTypes[15].Licenses.push({ LicenseID: Docs[i].HospitalPrivilegeDetailID, LicenseName: "HPL" + (count), LicenseDocPath: Docs[i].HospitalPrevilegeLetterPath, ModifiedDate: Docs[i].LastModifiedDate, Description: "Hospital Privilege Document" });

                if (Docs[i].HospitalPrivilegeDetailHistory != null) {

                    for (var j = 0; j < Docs[i].HospitalPrivilegeDetailHistory.length; j++) {

                        LicenseTypes[15].LicenseHistories.push({ LicenseID: Docs[i].HospitalPrivilegeDetailHistory[j].HospitalPrivilegeDetailHistoryID, LicenseName: "HPL" + (j + 1), LicenseDocPath: Docs[i].HospitalPrivilegeDetailHistory[j].HospitalPrevilegeLetterPath, removeDate: Docs[i].HospitalPrivilegeDetailHistory[j].LastModifiedDate });

                    }

                }

                count++;

            }

        }

     
    }

    FormatProfessionalLiabilityInfoDoc = function (Docs) {

        Licenses = [];
        LicenseHistories = [];

        for (var i = 0; i < Docs.length; i++) {

            if (Docs[i].InsuranceCertificatePath != null) {

                LicenseTypes[16].Licenses.push({ LicenseID: Docs[i].ProfessionalLiabilityInfoID, LicenseName: "LIC" + (i + 1), LicenseDocPath: Docs[i].InsuranceCertificatePath, ModifiedDate: Docs[i].LastModifiedDate, Description: "Professional Liability Document" });

                if (Docs[i].ProfessionalLiabilityInfoHistory != null) {

                    for (var j = 0; j < Docs[i].ProfessionalLiabilityInfoHistory.length; j++) {

                        LicenseTypes[16].LicenseHistories.push({ LicenseID: Docs[i].ProfessionalLiabilityInfoHistory[j].ProfessionalLiabilityInfoHistoryID, LicenseName: "LIC" + (j + 1), LicenseDocPath: Docs[i].ProfessionalLiabilityInfoHistory[j].InsuranceCertificatePath, removeDate: Docs[i].ProfessionalLiabilityInfoHistory[j].LastModifiedDate });

                    }

                }

            }

        }

    }

    FormatProfessionalWorkExperienceDoc = function (Docs) {
        Licenses = [];
        LicenseHistories = [];

        for (var i = 0; i < Docs.length; i++) {

            if (Docs[i].WorkExperienceDocPath != null) {

                LicenseTypes[17].Licenses.push({ LicenseID: Docs[i].ProfessionalWorkExperienceID, LicenseName: "Work Exp" + (i + 1), LicenseDocPath: Docs[i].WorkExperienceDocPath, ModifiedDate: Docs[i].LastModifiedDate, Description: "Work Experience Document" });

                if (Docs[i].ProfessionalWorkExperienceHistory != null) {

                    LicenseTypes[17].LicenseHistories.push({ LicenseID: Docs[i].ProfessionalWorkExperienceHistory.ProfessionalWorkExperienceHistoryID, LicenseName: "Work Exp" + (j + 1), LicenseDocPath: Docs[i].ProfessionalWorkExperienceHistory.WorkExperienceDocPath, removeDate: Docs[i].ProfessionalWorkExperienceHistory.LastModifiedDate });

                }

            }

        }
    }

    FormatContractInfoeDoc = function (Docs) {
        Licenses = [];
        LicenseHistories = [];

        for (var i = 0; i < Docs.length; i++) {

            if (Docs[i].ContractFilePath != null) {

                LicenseTypes[18].Licenses.push({ LicenseID: Docs[i].ContractInfoID, LicenseName: "Contract Document", LicenseDocPath: Docs[i].ContractFilePath, ModifiedDate: Docs[i].LastModifiedDate, Description: "Contract Document" });

            }

        }


    }

    FormatOtherDocument = function (Docs) {

        Licenses = [];
        LicenseHistories = [];

        for (var i = 0; i < Docs.length; i++) {

            if (Docs[i].DocumentPath != null) {

                LicenseTypes[19].Licenses.push({ LicenseID: Docs[i].OtherDocumentID, LicenseName: Docs[i].Title, LicenseDocPath: Docs[i].DocumentPath, ModifiedDate: Docs[i].LastModifiedDate, Private: Docs[i].IsPrivate, Description: "Other Document" });

            }

        }

        //if (Licenses.length != 0) {

        //LicenseTypes.push({ LicenseTypeID: 20, LicenseTypeName: "Other", Licenses: Licenses, LicenseHistories: LicenseHistories });

        //}

    }

    combine = function (license) {

        for (var i = 0; i < LicenseTypes.length; i++) {

            for (var j = 0; j < LicenseTypes[i].Licenses.length; j++) {

                $rootScope.tempObject.Docs.push(LicenseTypes[i].Licenses[j]);

            }

        }

    }

    function resetTable() {
        $rootScope.$broadcast('AppointmentsGrid', { type: $rootScope.tableCaption, RowObject: null });
    }
    

    return {
        getPage: getPage,
        getFilteredAppointmentdataByPlan: getFilteredAppointmentdataByPlan,
        modalDismiss:modalDismiss,
        resetTableState: resetTableState,
        getFilteredAppointmentdataByAppointmentDate: getFilteredAppointmentdataByAppointmentDate,
        exportToTable: exportToTable,
        ClearSelectRowStatus: ClearSelectRowStatus,
        ConvertDate: ConvertDate,
        FormatCCMdata : formatCCMdata,
        LoadCounts: loadCounts,
        FormatPSVData: formatPSVData,
        FormatPSVId: formatPSVId,
        FormatDocuments: formatDocuments,
        ResetTable: resetTable,
        ResetFormForValidation: resetFormForValidation
    }
}]);