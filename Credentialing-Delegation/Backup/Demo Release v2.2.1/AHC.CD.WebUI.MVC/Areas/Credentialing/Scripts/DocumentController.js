
//-------------------- document controller, author : krglv -------------------------

Cred_SPA_App.controller('DocumentController', ['$scope', '$rootScope', '$timeout', '$http', 'messageAlertEngine',
    function ($scope, $rootScope, $timeout, $http, messageAlertEngine) {

        $rootScope.DocumentLoaded = true;
        $scope.progressbar = false;

        $scope.getData = function () {
            console.log("2");
            if (!$scope.dataLoaded) {
                console.log("3");
                $rootScope.DocumentLoaded = false;
                $http({
                    method: 'GET',
                    url: '/Credentialing/DocChecklist/GetDocumentProfileDataAsync?profileId=' + 1
                }).success(function (data, status, headers, config) {
                    console.log("success");
                    console.log(data);

                    $scope.formatData(data);

                    try {
                        $rootScope.DocumentLoaded = true;
                    } catch (e) {
                        //console.log("error getting data back");
                        $rootScope.DocumentLoaded = true;
                    }

                }).error(function (data, status, headers, config) {
                    console.log(status);
                    $rootScope.DocumentLoaded = true;
                });
                $scope.dataLoaded = true;
            }
            console.log("end");
        };

        $scope.formatData = function (data) {

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

        $scope.LicenseTypes = [];
        $scope.spaDoc = [];

        $scope.FormatStateLicense = function (Docs) {

            $scope.Licenses = [];
            $scope.LicenseHistories = [];

            for (var i = 0; i < Docs.length; i++) {

                if (Docs[i].StateLicenseDocumentPath != null) {

                    $scope.Licenses.push({ LicenseID: Docs[i].StateLicenseInformationID, LicenseName: "ML" + (i + 1), LicenseDocPath: Docs[i].StateLicenseDocumentPath, Status: false });

                    if (Docs[i].StateLicenseInfoHistory != null) {

                        for (var j = 0; j < Docs[i].StateLicenseInfoHistory.length; j++) {

                            if (Docs[i].StateLicenseInfoHistory[j] != null) {

                                $scope.LicenseHistories.push({ LicenseID: Docs[i].StateLicenseInfoHistory[j].StatelicenseInfoHistoryID, LicenseName: "ML" + (j + 1), LicenseDocPath: Docs[i].StateLicenseInfoHistory[j].StatelicenseDocumentPath, removeDate: Docs[i].StateLicenseInfoHistory[j].LastModifiedDate });

                            }

                        }

                    }

                }

            }

            if ($scope.Licenses.length != 0) {

                $scope.LicenseTypes.push({ LicenseTypeID: 1, LicenseTypeName: "State License", Licenses: $scope.Licenses, LicenseHistories: $scope.LicenseHistories });

            }

        }

        $scope.FormatOtherLegalNamesDoc = function (Docs) {

            $scope.Licenses = [];
            $scope.LicenseHistories = [];

            for (var i = 0; i < Docs.length; i++) {

                if (Docs[i].DocumentPath != null) {

                    $scope.Licenses.push({ LicenseID: Docs[i].OtherLegalNameID, LicenseName: "OLN" + (i + 1), LicenseDocPath: Docs[i].DocumentPath, Status: false });

                    if (Docs[i].OtherLegalNameHistory != null) {

                        $scope.LicenseHistories.push({ LicenseID: Docs[i].OtherLegalNameHistory[j].OtherLegalNameHistoryID, LicenseName: "OLN" + (j + 1), LicenseDocPath: Docs[i].OtherLegalNameHistory[j].DocumentPath, removeDate: Docs[i].OtherLegalNameHistory[j].LastModifiedDate });

                    }

                }

            }

            if ($scope.Licenses.length != 0) {

                $scope.LicenseTypes.push({ LicenseTypeID: 2, LicenseTypeName: "Other Legal Name Document", Licenses: $scope.Licenses, LicenseHistories: $scope.LicenseHistories });

            }

        }

        $scope.FormatPersonalIdentificationDoc = function (Docs) {

            $scope.Licenses = [];
            $scope.LicenseHistories = [];

            if (Docs.SSNCertificatePath != null) {

                $scope.Licenses.push({ LicenseID: Docs.PersonalIdentificationID, LicenseName: "SSN", LicenseDocPath: Docs.SSNCertificatePath, Status: false });

            }

            if (Docs.DLCertificatePath != null) {

                $scope.Licenses.push({ LicenseID: Docs.PersonalIdentificationID, LicenseName: "DL", LicenseDocPath: Docs.DLCertificatePath, Status: false });

            }

            if ($scope.Licenses.length != 0) {

                $scope.LicenseTypes.push({ LicenseTypeID: 3, LicenseTypeName: "Personal Identification Document", Licenses: $scope.Licenses, LicenseHistories: $scope.LicenseHistories });

            }

        }

        $scope.FormatBirthInformationDoc = function (Docs) {

            $scope.Licenses = [];
            $scope.LicenseHistories = [];

            if (Docs.BirthCertificatePath != null) {

                $scope.Licenses.push({ LicenseID: Docs.BirthInformationID, LicenseName: "Birth Certificate", LicenseDocPath: Docs.BirthCertificatePath, Status: false });

                $scope.LicenseHistories.push({ LicenseID: 1, LicenseName: "Birth Certificate", LicenseDocPath: Docs.BirthCertificatePath, removeDate: Docs.LastModifiedDate });

                $scope.LicenseTypes.push({ LicenseTypeID: 4, LicenseTypeName: "Birth Document", Licenses: $scope.Licenses, LicenseHistories: $scope.LicenseHistories });

            }

        }

        $scope.FormatCVInformationDoc = function (Docs) {

            $scope.Licenses = [];
            $scope.LicenseHistories = [];

            if (Docs.CVDocumentPath != null) {

                $scope.Licenses.push({ LicenseID: Docs.CVInformationID, LicenseName: "CV", LicenseDocPath: Docs.CVDocumentPath, Status: false });

                $scope.LicenseTypes.push({ LicenseTypeID: 5, LicenseTypeName: "CV Document", Licenses: $scope.Licenses, LicenseHistories: $scope.LicenseHistories });

            }

        }

        $scope.FormatVisaInfoDoc = function (Docs) {

            $scope.Licenses = [];
            $scope.LicenseHistories = [];

            if (Docs.VisaInfo != null) {

                if (Docs.VisaInfo.VisaCertificatePath != null) {

                    $scope.Licenses.push({ LicenseID: Docs.VisaInfo.VisaInfoID, LicenseName: "VISA", LicenseDocPath: Docs.VisaInfo.VisaCertificatePath, Status: false });

                }

                if (Docs.VisaInfo.GreenCardCertificatePath != null) {

                    $scope.Licenses.push({ LicenseID: Docs.VisaInfo.VisaInfoID, LicenseName: "GreenCard", LicenseDocPath: Docs.VisaInfo.GreenCardCertificatePath, Status: false });

                }

                if (Docs.VisaInfo.NationalIDCertificatePath != null) {

                    $scope.Licenses.push({ LicenseID: Docs.VisaInfo.VisaInfoID, LicenseName: "National Identification", LicenseDocPath: Docs.VisaInfo.NationalIDCertificatePath, Status: false });

                }

                $scope.LicenseTypes.push({ LicenseTypeID: 6, LicenseTypeName: "Citizenship Document", Licenses: $scope.Licenses, LicenseHistories: $scope.LicenseHistories });

            }

        }

        $scope.FormatFederalDEAInformationDoc = function (Docs) {

            $scope.Licenses = [];
            $scope.LicenseHistories = [];

            for (var i = 0; i < Docs.length; i++) {

                if (Docs[i].DEALicenceCertPath != null) {

                    $scope.Licenses.push({ LicenseID: Docs[i].FederalDEAInformationID, LicenseName: "DEA" + (i + 1), LicenseDocPath: Docs[i].DEALicenceCertPath, Status: false });

                    if (Docs[i].FederalDEAInfoHistory != null) {

                        for (var j = 0; j < Docs[i].FederalDEAInfoHistory.length; j++) {
                            $scope.LicenseHistories.push({ LicenseID: Docs[i].FederalDEAInfoHistory[j].FederalDEAInfoHistoryID, LicenseName: "DEA" + (j + 1), LicenseDocPath: Docs[i].FederalDEAInfoHistory[j].FederalDEADocumentPath, removeDate: Docs[i].FederalDEAInfoHistory[j].LastModifiedDate });
                        }

                    }

                }

            }

            if ($scope.Licenses.length != 0) {

                $scope.LicenseTypes.push({ LicenseTypeID: 7, LicenseTypeName: "Federal DEA Document", Licenses: $scope.Licenses, LicenseHistories: $scope.LicenseHistories });

            }

        }

        $scope.FormatMedicareInformationDoc = function (Docs) {

            $scope.Licenses = [];
            $scope.LicenseHistories = [];

            for (var i = 0; i < Docs.length; i++) {

                if (Docs[i].CertificatePath != null) {

                    $scope.Licenses.push({ LicenseID: Docs[i].MedicareInformationID, LicenseName: "Medicare" + (i + 1), LicenseDocPath: Docs[i].CertificatePath, Status: false });

                    if (Docs[i].MedicareInformationHistory != null) {

                        $scope.LicenseHistories.push({ LicenseID: Docs[i].MedicareInformationHistory.MedicareInformationHistoryID, LicenseName: "Medicare" + (j + 1), LicenseDocPath: Docs[i].MedicareInformationHistory.CertificatePath, removeDate: Docs[i].MedicareInformationHistory.LastModifiedDate });

                    }

                }

            }

            if ($scope.Licenses.length != 0) {

                $scope.LicenseTypes.push({ LicenseTypeID: 8, LicenseTypeName: "Medicare Document", Licenses: $scope.Licenses, LicenseHistories: $scope.LicenseHistories });

            }

        }

        $scope.FormatMedicaidInformationDoc = function (Docs) {

            $scope.Licenses = [];
            $scope.LicenseHistories = [];

            for (var i = 0; i < Docs.length; i++) {

                if (Docs[i].CertificatePath != null) {

                    $scope.Licenses.push({ LicenseID: Docs[i].MedicaidInformationID, LicenseName: "Medicaid" + (i + 1), LicenseDocPath: Docs[i].CertificatePath, Status: false });

                    if (Docs[i].MedicaidInformationHistory != null) {

                        $scope.LicenseHistories.push({ LicenseID: Docs[i].MedicaidInformationHistory.MedicaidInformationHistoryID, LicenseName: "Medicade" + (j + 1), LicenseDocPath: Docs[i].MedicaidInformationHistory.CertificatePath, removeDate: Docs[i].MedicaidInformationHistory.LastModifiedDate });

                    }

                }

            }

            if ($scope.Licenses.length != 0) {

                $scope.LicenseTypes.push({ LicenseTypeID: 9, LicenseTypeName: "Medicaid Document", Licenses: $scope.Licenses, LicenseHistories: $scope.LicenseHistories });

            }

        }

        $scope.FormatCDSCInformationDoc = function (Docs) {

            $scope.Licenses = [];
            $scope.LicenseHistories = [];

            for (var i = 0; i < Docs.length; i++) {

                if (Docs[i].CDSCCerificatePath != null) {

                    $scope.Licenses.push({ LicenseID: Docs[i].CDSCInformationID, LicenseName: "CDS" + (i + 1), LicenseDocPath: Docs[i].CDSCCerificatePath, Status: false });

                    if (Docs[i].CDSCInfoHistory != null) {

                        for (var j = 0; j < Docs[i].CDSCInfoHistory.length; j++) {
                            $scope.LicenseHistories.push({ LicenseID: Docs[i].CDSCInfoHistory[j].CDSCInfoHistoryID, LicenseName: "CDS" + (j + 1), LicenseDocPath: Docs[i].CDSCInfoHistory[j].CDSCCerificatePath, removeDate: Docs[i].CDSCInfoHistory[j].LastModifiedDate });
                        }

                    }

                }

            }

            if ($scope.Licenses.length != 0) {

                $scope.LicenseTypes.push({ LicenseTypeID: 10, LicenseTypeName: "CDSC Document", Licenses: $scope.Licenses, LicenseHistories: $scope.LicenseHistories });

            }

        }

        $scope.FormatEducationDetailDoc = function (Docs) {

            $scope.Licenses = [];
            $scope.LicenseHistories = [];
            $scope.countUG = 0;
            $scope.countG = 0;

            for (var i = 0; i < Docs.length; i++) {

                if (Docs[i].CertificatePath != null) {

                    if (Docs[i].CertificatePath != null) {

                        if (Docs[i].QualificationType == "UnderGraduate") {

                            $scope.Licenses.push({ LicenseID: Docs[i].EducationDetailID, LicenseName: "UG" + ($scope.countUG + 1), LicenseDocPath: Docs[i].CertificatePath, Status: false });
                            $scope.countUG++;

                            if (Docs[i].EducationDetailHistory != null) {

                                $scope.LicenseHistories.push({ LicenseID: Docs[i].EducationDetailHistory.EducationDetailHistoryID, LicenseName: "UG" + ($scope.countUG + 1), LicenseDocPath: Docs[i].EducationDetailHistory.CertificatePath, removeDate: Docs[i].EducationDetailHistory.LastModifiedDate });

                            }

                        } else if (Docs[i].QualificationType == "Graduate") {

                            $scope.Licenses.push({ LicenseID: Docs[i].EducationDetailID, LicenseName: "Grad" + ($scope.countG + 1), LicenseDocPath: Docs[i].CertificatePath, Status: false });
                            $scope.countG++;

                            if (Docs[i].EducationDetailHistory != null) {

                                $scope.LicenseHistories.push({ LicenseID: Docs[i].EducationDetailHistory.EducationDetailHistoryID, LicenseName: "Grad" + ($scope.countG + 1), LicenseDocPath: Docs[i].EducationDetailHistory.CertificatePath, removeDate: Docs[i].EducationDetailHistory.LastModifiedDate });

                            }

                        }

                    }

                }

            }

            if ($scope.Licenses.length != 0) {

                $scope.LicenseTypes.push({ LicenseTypeID: 11, LicenseTypeName: "Education Document", Licenses: $scope.Licenses, LicenseHistories: $scope.LicenseHistories });

            }

        }

        $scope.FormatECFMGDetailDoc = function (Docs) {

            $scope.Licenses = [];
            $scope.LicenseHistories = [];

            if (Docs.ECFMGCertPath != null) {

                $scope.Licenses.push({ LicenseID: Docs.ECFMGDetailID, LicenseName: "ECFMG", LicenseDocPath: Docs.ECFMGCertPath, Status: false });

                $scope.LicenseHistories.push({ LicenseID: 2, LicenseName: "ECFMG", LicenseDocPath: Docs.ECFMGCertPath, removeDate: Docs.LastModifiedDate });

                $scope.LicenseTypes.push({ LicenseTypeID: 12, LicenseTypeName: "ECFMG Document", Licenses: $scope.Licenses, LicenseHistories: $scope.LicenseHistories });

            }

        }

        $scope.FormatProgramDetailsDoc = function (Docs) {

            $scope.Licenses = [];
            $scope.LicenseHistories = [];
            $scope.countIntern = 0;
            $scope.countFell = 0;
            $scope.countRes = 0;
            $scope.countOther = 0;

            for (var i = 0; i < Docs.length; i++) {

                if (Docs[i].DocumentPath != null) {

                    if (Docs[i].ProgramType == "Internship") {

                        $scope.Licenses.push({ LicenseID: Docs[i].ProgramDetailID, LicenseName: "Intern" + ($scope.countIntern + 1), LicenseDocPath: Docs[i].DocumentPath, Status: false });

                        $scope.countIntern++;

                        if (Docs.ProgramDetailHistory != null) {

                            $scope.LicenseHistories.push({ LicenseID: Docs[i].ProgramDetailHistory.ProgramDetailHistoryID, LicenseName: "Intern" + ($scope.countIntern + 1), LicenseDocPath: Docs[i].ProgramDetailHistory.DocumentPath, removeDate: Docs[i].ProgramDetailHistory.LastModifiedDate });

                        }

                    } else if (Docs[i].ProgramType == "Fellowship") {

                        $scope.Licenses.push({ LicenseID: Docs[i].ProgramDetailID, LicenseName: "Fell" + ($scope.countFell + 1), LicenseDocPath: Docs[i].DocumentPath, Status: false });

                        $scope.countFell++;

                        if (Docs.ProgramDetailHistory != null) {

                            $scope.LicenseHistories.push({ LicenseID: Docs[i].ProgramDetailHistory.ProgramDetailHistoryID, LicenseName: "Fell" + ($scope.countFell + 1), LicenseDocPath: Docs[i].ProgramDetailHistory.DocumentPath, removeDate: Docs[i].ProgramDetailHistory.LastModifiedDate });

                        }

                    } else if (Docs[i].ProgramType == "Resident") {

                        $scope.Licenses.push({ LicenseID: Docs[i].ProgramDetailID, LicenseName: "Res" + ($scope.countRes + 1), LicenseDocPath: Docs[i].DocumentPath, Status: false });

                        $scope.countRes++;

                        if (Docs.ProgramDetailHistory != null) {

                            $scope.LicenseHistories.push({ LicenseID: Docs[i].ProgramDetailHistory.ProgramDetailHistoryID, LicenseName: "Res" + ($scope.countRes + 1), LicenseDocPath: Docs[i].ProgramDetailHistory.DocumentPath, removeDate: Docs[i].ProgramDetailHistory.LastModifiedDate });

                        }

                    } else if (Docs[i].ProgramType == "Other") {

                        $scope.Licenses.push({ LicenseID: Docs[i].ProgramDetailID, LicenseName: "Other" + ($scope.countOther + 1), LicenseDocPath: Docs[i].DocumentPath, Status: false });

                        $scope.countOther++;

                        if (Docs.ProgramDetailHistory != null) {

                            $scope.LicenseHistories.push({ LicenseID: Docs[i].ProgramDetailHistory.ProgramDetailHistoryID, LicenseName: "Other" + ($scope.countOther + 1), LicenseDocPath: Docs[i].ProgramDetailHistory.DocumentPath, removeDate: Docs[i].ProgramDetailHistory.LastModifiedDate });

                        }

                    }

                }

            }

            if ($scope.Licenses.length != 0) {

                $scope.LicenseTypes.push({ LicenseTypeID: 13, LicenseTypeName: "Program Document", Licenses: $scope.Licenses, LicenseHistories: $scope.LicenseHistories });

            }

        }

        $scope.FormatCMECertificationsDoc = function (Docs) {

            $scope.Licenses = [];
            $scope.LicenseHistories = [];

            for (var i = 0; i < Docs.length; i++) {

                if (Docs[i].CertificatePath != null) {

                    $scope.Licenses.push({ LicenseID: Docs[i].CMECertificationID, LicenseName: "CME" + (i + 1), LicenseDocPath: Docs[i].CertificatePath, Status: false });

                    if (Docs[i].CMECertificationHistory != null) {

                        $scope.LicenseHistories.push({ LicenseID: Docs[i].CMECertificationHistory.CMECertificationHistoryID, LicenseName: "CME" + (j + 1), LicenseDocPath: Docs[i].CMECertificationHistory.CertificatePath, removeDate: Docs[i].CMECertificationHistory.LastModifiedDate });

                    }

                }

            }

            if ($scope.Licenses.length != 0) {

                $scope.LicenseTypes.push({ LicenseTypeID: 14, LicenseTypeName: "CME Certifications", Licenses: $scope.Licenses, LicenseHistories: $scope.LicenseHistories });

            }

        }

        $scope.FormatSpecialtyBoardCertifiedDetailDoc = function (Docs) {

            $scope.Licenses = [];
            $scope.LicenseHistories = [];

            for (var i = 0; i < Docs.length; i++) {

                if (Docs[i].SpecialtyBoardCertifiedDetail != null && Docs[i].SpecialtyBoardCertifiedDetail.BoardCertificatePath != null) {

                    $scope.Licenses.push({ LicenseID: Docs[i].SpecialtyBoardCertifiedDetail.SpecialtyBoardCertifiedDetailID, LicenseName: "Board" + (i + 1), LicenseDocPath: Docs[i].SpecialtyBoardCertifiedDetail.BoardCertificatePath, Status: false });

                    if (Docs[i].SpecialtyBoardCertifiedDetail.SpecialtyBoardCertifiedDetailHistory != null) {

                        for (var j = 0; j < Docs[i].SpecialtyBoardCertifiedDetail.SpecialtyBoardCertifiedDetailHistory.length; j++) {
                            $scope.LicenseHistories.push({ LicenseID: Docs[i].SpecialtyBoardCertifiedDetail.SpecialtyBoardCertifiedDetailHistory[j].SpecialtyBoardCertifiedDetailHistoryID, LicenseName: "Board" + (j + 1), LicenseDocPath: Docs[i].SpecialtyBoardCertifiedDetail.SpecialtyBoardCertifiedDetailHistory[j].BoardCertificatePath, removeDate: Docs[i].SpecialtyBoardCertifiedDetail.SpecialtyBoardCertifiedDetailHistory[j].LastModifiedDate });
                        }

                    }

                }

            }

            if ($scope.Licenses.length != 0) {

                $scope.LicenseTypes.push({ LicenseTypeID: 15, LicenseTypeName: "Specialty Board Certificates", Licenses: $scope.Licenses, LicenseHistories: $scope.LicenseHistories });

            }

        }

        $scope.FormatHospitalPrivilegeDetailDoc = function (Docs) {

            $scope.Licenses = [];
            $scope.LicenseHistories = [];
            var count = 1;

            for (var i = 0; i < Docs.length; i++) {

                if (Docs[i].HospitalPrevilegeLetterPath != null && Docs[i].Status != "Inactive") {

                    $scope.Licenses.push({ LicenseID: Docs[i].HospitalPrivilegeDetailID, LicenseName: "HPL" + (count), LicenseDocPath: Docs[i].HospitalPrevilegeLetterPath, Status: false });

                    if (Docs[i].HospitalPrivilegeDetailHistory != null) {

                        for (var j = 0; j < Docs[i].HospitalPrivilegeDetailHistory.length; j++) {
                            $scope.LicenseHistories.push({ LicenseID: Docs[i].HospitalPrivilegeDetailHistory[j].HospitalPrivilegeDetailHistoryID, LicenseName: "HPL" + (j + 1), LicenseDocPath: Docs[i].HospitalPrivilegeDetailHistory[j].HospitalPrevilegeLetterPath, removeDate: Docs[i].HospitalPrivilegeDetailHistory[j].LastModifiedDate });
                        }

                    }

                    count++;

                }

            }

            if ($scope.Licenses.length != 0) {

                $scope.LicenseTypes.push({ LicenseTypeID: 16, LicenseTypeName: "Hospital Privilege Document", Licenses: $scope.Licenses, LicenseHistories: $scope.LicenseHistories });

            }

        }

        $scope.FormatProfessionalLiabilityInfoDoc = function (Docs) {

            $scope.Licenses = [];
            $scope.LicenseHistories = [];

            for (var i = 0; i < Docs.length; i++) {

                if (Docs[i].InsuranceCertificatePath != null) {

                    $scope.Licenses.push({ LicenseID: Docs[i].ProfessionalLiabilityInfoID, LicenseName: "LIC" + (i + 1), LicenseDocPath: Docs[i].InsuranceCertificatePath, Status: false });

                    if (Docs[i].ProfessionalLiabilityInfoHistory != null) {

                        for (var j = 0; j < Docs[i].ProfessionalLiabilityInfoHistory.length; j++) {
                            $scope.LicenseHistories.push({ LicenseID: Docs[i].ProfessionalLiabilityInfoHistory[j].ProfessionalLiabilityInfoHistoryID, LicenseName: "LIC" + (j + 1), LicenseDocPath: Docs[i].ProfessionalLiabilityInfoHistory[j].InsuranceCertificatePath, removeDate: Docs[i].ProfessionalLiabilityInfoHistory[j].LastModifiedDate });
                        }

                    }

                }

            }

            if ($scope.Licenses.length != 0) {

                $scope.LicenseTypes.push({ LicenseTypeID: 17, LicenseTypeName: "Professional Liability Document", Licenses: $scope.Licenses, LicenseHistories: $scope.LicenseHistories });

            }

        }

        $scope.FormatProfessionalWorkExperienceDoc = function (Docs) {

            $scope.Licenses = [];
            $scope.LicenseHistories = [];

            for (var i = 0; i < Docs.length; i++) {

                if (Docs[i].WorkExperienceDocPath != null) {

                    $scope.Licenses.push({ LicenseID: Docs[i].ProfessionalWorkExperienceID, LicenseName: "Work Exp" + (i + 1), LicenseDocPath: Docs[i].WorkExperienceDocPath, Status: false });

                    if (Docs[i].ProfessionalWorkExperienceHistory != null) {

                        $scope.LicenseHistories.push({ LicenseID: Docs[i].ProfessionalWorkExperienceHistory.ProfessionalWorkExperienceHistoryID, LicenseName: "Work Exp" + (j + 1), LicenseDocPath: Docs[i].ProfessionalWorkExperienceHistory.WorkExperienceDocPath, removeDate: Docs[i].ProfessionalWorkExperienceHistory.LastModifiedDate });

                    }

                }

            }

            if ($scope.Licenses.length != 0) {

                $scope.LicenseTypes.push({ LicenseTypeID: 18, LicenseTypeName: "WorkExperience Document", Licenses: $scope.Licenses, LicenseHistories: $scope.LicenseHistories });

            }

        }

        $scope.FormatContractInfoeDoc = function (Docs) {

            $scope.Licenses = [];
            $scope.LicenseHistories = [];

            for (var i = 0; i < Docs.length; i++) {

                if (Docs[i].ContractFilePath != null) {

                    $scope.Licenses.push({ LicenseID: Docs[i].ContractInfoID, LicenseName: "Contract Document", LicenseDocPath: Docs[i].ContractFilePath, Status: false });

                }

            }

            if ($scope.Licenses.length != 0) {

                $scope.LicenseTypes.push({ LicenseTypeID: 19, LicenseTypeName: "Contract", Licenses: $scope.Licenses, LicenseHistories: $scope.LicenseHistories });

            }

        }

        $scope.FormatOtherDocument = function (Docs) {

            $scope.Licenses = [];
            $scope.LicenseHistories = [];

            for (var i = 0; i < Docs.length; i++) {

                if (Docs[i].DocumentPath != null) {

                    $scope.Licenses.push({ LicenseID: Docs[i].OtherDocumentID, LicenseName: Docs[i].Title, LicenseDocPath: Docs[i].DocumentPath, Status: false });

                }

            }

            if ($scope.Licenses.length != 0) {

                $scope.LicenseTypes.push({ LicenseTypeID: 20, LicenseTypeName: "Other", Licenses: $scope.Licenses, LicenseHistories: $scope.LicenseHistories });

            }

        }

        //$scope.LicenseTypes = [
        //    {
        //        LicenseTypeID: 1,
        //        LicenseTypeName: "State License",
        //        Licenses: [
        //            {
        //                LicenseID: 1,
        //                LicenseName: "State License 1",
        //                LicenseDocPath: "/Content/Images/Plans/1.jpg",
        //            },
        //            {
        //                LicenseID: 2,
        //                LicenseName: "State License 33",
        //                LicenseDocPath: "/Content/Images/Plans/Ultimate.jpg",
        //            },
        //            {
        //                LicenseID: 3,
        //                LicenseName: "State License 12",
        //                LicenseDocPath: "/Content/Images/Plans/2.jpg",
        //            }
        //        ],
        //        LicenseHistories: [
        //            {
        //                LicenseID: 10,
        //                LicenseName: "State License 1",
        //                LicenseDocPath: "/Content/Images/Plans/1.jpg",
        //                removeDate: new Date(2014, 03, 06)
        //            },
        //            {
        //                LicenseID: 20,
        //                LicenseName: "State License 33",
        //                LicenseDocPath: "/Content/Images/Plans/Ultimate.jpg",
        //                removeDate: new Date(2014, 11, 09)
        //            },
        //            {
        //                LicenseID: 30,
        //                LicenseName: "State License 12",
        //                LicenseDocPath: "/Content/Images/Plans/2.jpg",
        //                removeDate: new Date(2015, 02, 16)
        //            }
        //        ]
        //    }
        //];

        //-------------------- required Data Attribute --------------
        $scope.SelectedLicenseType = {};
        $scope.SelectedLicense = {};
        $scope.TempObject = {};
        $scope.IsAddNewDocument = false;
        $scope.IsMessage = false;
        $scope.IsHistoryView = false;

        //----------------------------- view license Type ---------------------
        $scope.ViewLicenseList = function (licenseType, condition) {
            if (licenseType.LicenseTypeID == $scope.SelectedLicenseType.LicenseTypeID) {
                $scope.SelectedLicenseType = {};
                $scope.SelectedLicense = {};
            } else {
                $scope.SelectedLicenseType = licenseType;
                $scope.SelectedLicense = {};
            }
            if (condition) {
                $scope.IsHistoryView = true;
            } else {
                $scope.IsHistoryView = false;
            }
        };
        //------------------- view license doc ----------------------
        $scope.ViewLicense = function (license) {
            $scope.SelectedLicense = license;
            $scope.IsAddNewDocument = false;
            $scope.path = "/Document/View?path=" + license.LicenseDocPath;
            //license.LicenseDocPath = path;
        };
        //----------------- add new document ---------------
        $scope.AddNewDocument = function (licenseType) {
            $scope.TempObject = {
                Title: "",
                CategoryID: licenseType.LicenseID,
                CategoryName: licenseType.LicenseName,
                IsPrivate: false,
                Document: {}
            };
            $("#ATTACHmore").show();
            $("#SubmitDone").hide();
            $("#AddDone").hide();
            $scope.IsAddNewDocument = true;
            $scope.isAdd = true;
            $scope.isUpdate = false;
        };

        $scope.EditDocument = function (licenseType, license) {
            $scope.TempObject = {
                Title: license.LicenseName,
                ID: license.LicenseID,
                CategoryID: licenseType.LicenseTypeID,
                CategoryName: licenseType.LicenseTypeName,
                FilePath: license.LicenseDocPath,
                IsPrivate: false,
                Document: {}
            };
            $scope.IsAddNewDocument = true;
            $scope.isAdd = false;
            $scope.isUpdate = true;
        };

        $scope.IsAddNewDocument = true;

        //$scope.SaveData = function (data) {
        //    console.log($scope.TempObject);
        //    $scope.IsAddNewDocument = false;
        //    $scope.SuccessMessage = "Document Updated Successfully !!!!!!!!!!!";
        //    $scope.IsMessage = true;

        //    $scope.UpdateDoc($scope.TempObject);

        //    $timeout(function () {
        //        $scope.IsMessage = false;
        //    }, 5000);
        //};

        $scope.saveOtherDocument = function (Form_Div_Id, profileId) {
            var $form = $("#" + Form_Div_Id).find("form");
            ResetFormForValidation($form);
            console.log($scope.TempObject.Title);
            var status = false;

            status = $scope.check($scope.TempObject.Title);

            if ($form.valid() && !status) {
                $.ajax({
                    url: '/Profile/DocumentRepository/AddOtherDocumentAsync?profileId=' + profileId,
                    type: 'POST',
                    data: new FormData($form[0]),
                    async: false,
                    cache: false,
                    contentType: false,
                    processData: false,
                    success: function (data) {
                        //console.log(data);
                        if (data.status == "true") {

                            $scope.IsAddNewDocument = false;
                            $("#ATTACHmore").hide();
                            $scope.SuccessMessage = "Document Updated Successfully !!!!!!!!!!!";
                            $scope.IsMessage = true;
                            console.log(data.otherDocument)
                            $scope.UpdateDocumentArray(data.otherDocument);
                            //$scope.UpdateDoc($scope.TempObject);

                            $timeout(function () {
                                $scope.IsMessage = false;
                            }, 5000);

                            $scope.TempObject = null;

                            $rootScope.visibilityControl = "";
                            //$rootScope.visibilityControl = $scope.Provider.OtherLegalNames.length-1 + "_ViewOtherLegalName";
                            $scope.resetForm();
                            window.DocumentForm.reset();
                            FormReset($form);

                            messageAlertEngine.callAlertMessage("alertOtherDocumentSuccess", "Other Document saved successfully.", "success", true);
                        } else {
                            messageAlertEngine.callAlertMessage("alertOtherDocumentError", data.status.split(","), "danger", true);
                        }
                    }
                });
            } else {

                if ($scope.TempObject.Title != "") {
                    $scope.IsAddNewDocument = false;
                    $("#ATTACHmore").hide();
                    $scope.IsFail = true;
                    $scope.FailureMessage = "Document Already Present";
                    $timeout(function () {
                        $scope.IsFail = false;
                    }, 5000);
                    FormReset($form);
                    $scope.TempObject = null;
                }

            }

        };

        $scope.resetForm = function () {
            $scope.DocumentForm.$setPristine();
        };

        $scope.check = function (title) {

            var count = 0;
            var status = false;

            for (var i = 0; i < $scope.LicenseTypes.length; i++) {

                if ($scope.LicenseTypes[i].LicenseTypeID == 20) {

                    for (var j = 0; j < $scope.LicenseTypes[i].Licenses.length; j++) {

                        if ($scope.LicenseTypes[i].Licenses[j].LicenseName == title) {

                            count = 1

                        }
                        if (count == 1) {

                            status = true;

                        }

                    }

                    return status;

                }
            }

        }

        $scope.UpdateDocumentArray = function (obj) {

            $scope.spaDoc.push({ LicenseID: obj.OtherDocumentID, LicenseName: obj.Title, LicenseDocPath: obj.DocumentPath, Status: false });

        }

        $scope.updateOtherDocument = function (Form_Div_Id) {
            var $form = $("#" + Form_Div_Id).find("form");
            ResetFormForValidation($form);
            if ($form.valid()) {
                $.ajax({
                    url: '/Profile/DocumentRepository/UpdateOtherDocumentAsync?profileId=' + profileId,
                    type: 'POST',
                    data: new FormData($form[0]),
                    async: false,
                    cache: false,
                    contentType: false,
                    processData: false,
                    success: function (data) {
                        //console.log(data);
                        if (data.status == "true") {

                            //$rootScope.visibilityControl = "";
                            //$rootScope.visibilityControl = index + "_ViewOtherDocument";
                            $scope.IsAddNewDocument = false;
                            $scope.SuccessMessage = "Document Updated Successfully !!!!!!!!!!!";
                            $scope.IsMessage = true;

                            $timeout(function () {
                                $scope.IsMessage = false;
                            }, 5000);

                            $scope.UpdateOldDocument(data.otherDocument);

                            FormReset($form);
                            messageAlertEngine.callAlertMessage("alertOtherDocumentSuccess", "Other Document updated successfully.", "success", true);
                        } else {
                            messageAlertEngine.callAlertMessage("alertOtherDocumentError", data.status.split(","), "danger", true);
                        }
                    }
                });
            } else {

            }
        };

        $scope.UpdateOldDocument = function (obj) {

            for (var i = 0; i < $scope.spaDoc.length; i++) {

                if ($scope.spaDoc[i].LicenseID == obj.OtherDocumentID) {

                    $scope.spaDoc[i].LicenseName = obj.Title;

                    $scope.spaDoc[i].LicenseDocPath = obj.DocumentPath;

                }

            }

        }

        $scope.removeOtherDocument = function (Form_Div_Id) {
            var $form = $("#" + Form_Div_Id).find("form");
            ResetFormForValidation($form);
            console.log($scope.ID);
            if ($form.valid()) {
                $.ajax({
                    url: '/Profile/DocumentRepository/RemoveOtherDocument?profileId=' + profileId + "&OtherDocumentID=" + $scope.ID,
                    type: 'POST',
                    data: new FormData($form[0]),
                    async: false,
                    cache: false,
                    contentType: false,
                    processData: false,
                    success: function (data) {
                        //console.log(data);
                        if (data.status == "true") {
                            console.log($scope.LicenseTypes)
                            var len = $scope.LicenseTypes.length - 1;
                            $scope.LicenseTypes[len].Licenses.splice($scope.LicenseTypes[len].Licenses.indexOf($scope.spliceLicense), 1);

                            $scope.SuccessMessage = "Document Removed Successfully !!!!!!!!!!!";
                            $scope.IsMessage = true;
                            $("#DocRemoveConfirmation").modal('hide');

                            $timeout(function () {
                                $scope.IsMessage = false;
                            }, 5000);

                            FormReset($form);
                            messageAlertEngine.callAlertMessage("alertOtherDocumentSuccess", "Other Document removed successfully.", "success", true);
                        } else {
                            messageAlertEngine.callAlertMessage("alertOtherDocumentError", data.status.split(","), "danger", true);
                        }
                    }
                });
            } else {

            }
        };

        //$scope.removeOtherDocument = function () {
        //    var validationStatus = false;
        //    var url = null;
        //    var $formData = null;
        //    $formData = $('#newDocumentForm');
        //    url = "/Profile/DocumentRepository/RemoveOtherDocument?profileId=" + profileId;
        //    ResetFormForValidation($formData);
        //    validationStatus = $formData.valid();

        //    if (validationStatus) {
        //        //Simple POST request example (passing data) :
        //        $.ajax({
        //            url: url,
        //            type: 'POST',
        //            data: new FormData($formData[0]),
        //            async: false,
        //            cache: false,
        //            contentType: false,
        //            processData: false,
        //            success: function (data) {
        //                //console.log(data.status);
        //                if (data.status == "true") {
        //                    messageAlertEngine.callAlertMessage("alertOtherDocumentSuccess", "Other Document Removed successfully.", "success", true);
        //                } else {
        //                    $('#otherDocumentWarningModal').modal('hide');
        //                    messageAlertEngine.callAlertMessage("removeOtherDocument", data.status, "danger", true);
        //                    $scope.errorOtherLegalName = "Sorry for Inconvenience !!!! Please Try Again Later...";
        //                }
        //            },
        //            error: function (e) {

        //            }
        //        });
        //    }
        //};

        $scope.CancelAdd = function () {
            $scope.IsAddNewDocument = false;
        };

        //------------------------------ Remove Modal Show data ---------------------------
        $scope.RemoveDocument = function (licenseType, license) {

            $scope.ID = license.LicenseID;
            $scope.spliceLicense = license;

            $scope.IsAddNewDocument = false;
            $("#DocRemoveConfirmation").modal('show');
        };

        //------------------------------ Remove Confirmation data ---------------------------
        $scope.Confirmation = function (license) {
            $scope.TempLicenseType.Licenses.splice($scope.TempLicenseType.Licenses.indexOf(license), 1);
            $scope.TempLicenseType = {};
            $scope.TempLicense = {};

            $scope.SuccessMessage = "Document Removed Successfully !!!!!!!!!!!";
            $scope.IsMessage = true;
            $("#DocRemoveConfirmation").modal('hide');

            $timeout(function () {
                $scope.IsMessage = false;
            }, 5000);
        };

        $scope.combine = function (license) {

            for (var i = 0; i < $scope.LicenseTypes.length; i++) {

                for (var j = 0; j < $scope.LicenseTypes[i].Licenses.length; j++) {

                    $scope.spaDoc.push($scope.LicenseTypes[i].Licenses[j]);

                }

            }

        }

        $scope.$on('UpdatePersonalIdentification', function (event, data) {

            var count01 = 0;
            var count02 = 0;

            if (data.personalIdentification != null) {

                for (var i = 0; i < $scope.LicenseTypes.length; i++) {

                    if ($scope.LicenseTypes[i].LicenseTypeID == 3) {

                        for (var j = 0; j < $scope.LicenseTypes[i].Licenses.length; j++) {

                            if ($scope.LicenseTypes[i].Licenses[j].LicenseID == data.personalIdentification.PersonalIdentificationID) {

                                if ($scope.LicenseTypes[i].Licenses[j].LicenseName == "SSN") {

                                    $scope.LicenseTypes[i].Licenses[j].LicenseDocPath = data.personalIdentification.SSNCertificatePath;

                                } else if ($scope.LicenseTypes[i].Licenses[j].LicenseName == "DL") {

                                    $scope.LicenseTypes[i].Licenses[j].LicenseDocPath = data.personalIdentification.DLCertificatePath;

                                }

                                count02++;

                            }

                        }

                        if (count02 == 0) {

                            if (data.personalIdentification.SSNCertificatePath != null) {

                                $scope.Licenses.push({ LicenseID: data.personalIdentification.PersonalIdentificationID, LicenseName: "SSN", LicenseDocPath: data.personalIdentification.SSNCertificatePath });

                            }

                            if (data.personalIdentification.DLCertificatePath != null) {

                                $scope.Licenses.push({ LicenseID: data.personalIdentification.PersonalIdentificationID, LicenseName: "DL", LicenseDocPath: data.personalIdentification.DLCertificatePath });

                            }

                        }

                        count01++;

                    }

                }
                //if (count01 == 0 && $scope.LicenseTypes.length != 0) {

                //    $scope.FormatPersonalIdentificationDoc(data.personalIdentification);

                //}

            }

        });
        $scope.$on('UpdateStateLicenses', function (event, data) {

            var count01 = 0;
            var count02 = 0;

            if (data.stateLicense != null) {

                for (var i = 0; i < $scope.LicenseTypes.length; i++) {

                    if ($scope.LicenseTypes[i].LicenseTypeID == 1) {

                        for (var j = 0; j < $scope.LicenseTypes[i].Licenses.length; j++) {

                            if ($scope.LicenseTypes[i].Licenses[j].LicenseID == data.stateLicense.StateLicenseInformationID) {

                                $scope.LicenseTypes[i].Licenses[j].LicenseDocPath = data.stateLicense.StateLicenseDocumentPath;

                                count02++;

                            }

                        }

                        if (count02 == 0) {

                            if (data.stateLicense.StateLicenseDocumentPath != null) {

                                $scope.LicenseTypes[i].Licenses.push({ LicenseID: data.stateLicense.StateLicenseInformationID, LicenseName: "ML" + ($scope.LicenseTypes[i].Licenses.length + 1), LicenseDocPath: data.stateLicense.StateLicenseDocumentPath });

                            }

                        }

                        count01++;

                    }

                }
                //if (count01 == 0 && $scope.LicenseTypes.length != 0) {

                //    $scope.FormatStateLicense([data.stateLicense]);

                //}

            }

        });
        $scope.$on('UpdateOtherLegalNames', function (event, data) {

            var count01 = 0;
            var count02 = 0;

            if (data.otherLegalName != null) {

                for (var i = 0; i < $scope.LicenseTypes.length; i++) {

                    if ($scope.LicenseTypes[i].LicenseTypeID == 2) {

                        for (var j = 0; j < $scope.LicenseTypes[i].Licenses.length; j++) {

                            if ($scope.LicenseTypes[i].Licenses[j].LicenseID == data.otherLegalName.OtherLegalNameID) {

                                $scope.LicenseTypes[i].Licenses[j].LicenseDocPath = data.otherLegalName.DocumentPath;

                                count02++;

                            }

                        }

                        if (count02 == 0) {

                            if (data.otherLegalName.DocumentPath != null) {

                                $scope.LicenseTypes[i].Licenses.push({ LicenseID: data.otherLegalName.OtherLegalNameID, LicenseName: "OLN" + ($scope.LicenseTypes[i].Licenses.length + 1), LicenseDocPath: data.otherLegalName.DocumentPath });

                            }

                        }

                        count01++;

                    }

                }
                //if ($scope.LicenseTypes.length != 0) {

                //    $scope.FormatOtherLegalNamesDoc([data.otherLegalName]);

                //}

            }

        });
        $scope.$on('UpdateBirthInformation', function (event, data) {

            var count01 = 0;
            var count02 = 0;

            if (data.birthInformation != null) {

                for (var i = 0; i < $scope.LicenseTypes.length; i++) {

                    if ($scope.LicenseTypes[i].LicenseTypeID == 4) {

                        for (var j = 0; j < $scope.LicenseTypes[i].Licenses.length; j++) {

                            if ($scope.LicenseTypes[i].Licenses[j].LicenseID == data.birthInformation.BirthInformationID) {

                                $scope.LicenseTypes[i].Licenses[j].LicenseDocPath = data.birthInformation.BirthCertificatePath;

                                count02++;

                            }

                        }

                        if (count02 == 0) {

                            if (data.birthInformation.BirthCertificatePath != null) {

                                $scope.LicenseTypes[i].Licenses.push({ LicenseID: data.birthInformation.BirthInformationID, LicenseName: "Birth Certificate", LicenseDocPath: data.birthInformation.BirthCertificatePath });

                            }

                        }

                        count01++;

                    }

                }
                //if (count01 == 0 && $scope.LicenseTypes.length != 0) {

                //    $scope.FormatBirthInformationDoc(data.birthInformation);

                //}

            }

        });
        $scope.$on('UpdateVisaInfo', function (event, data) {

            var count01 = 0;
            var count02 = 0;

            if (data.visaDetail != null && data.visaDetail.VisaInfo != null) {

                for (var i = 0; i < $scope.LicenseTypes.length; i++) {

                    if ($scope.LicenseTypes[i].LicenseTypeID == 6) {

                        for (var j = 0; j < $scope.LicenseTypes[i].Licenses.length; j++) {

                            if ($scope.LicenseTypes[i].Licenses[j].LicenseID == data.visaDetail.VisaInfo.VisaInfoID) {

                                if ($scope.LicenseTypes[i].Licenses[j].LicenseName == "VISA") {

                                    $scope.LicenseTypes[i].Licenses[j].LicenseDocPath = data.visaDetail.VisaInfo.VisaCertificatePath;

                                } else if ($scope.LicenseTypes[i].Licenses[j].LicenseName == "GreenCard") {

                                    $scope.LicenseTypes[i].Licenses[j].LicenseDocPath = data.visaDetail.VisaInfo.GreenCardCertificatePath;

                                } else if ($scope.LicenseTypes[i].Licenses[j].LicenseName == "NationalIdentification") {

                                    $scope.LicenseTypes[i].Licenses[j].LicenseDocPath = data.visaDetail.VisaInfo.NationalIDCertificatePath;

                                }

                                count02++;

                            }

                        }

                        if (count02 == 0) {

                            if (data.visaDetail.VisaInfo != null) {

                                if (data.visaDetail.VisaInfo.VisaCertificatePath != null) {

                                    $scope.Licenses.push({ LicenseID: data.visaDetail.VisaInfo.VisaInfoID, LicenseName: "VISA", LicenseDocPath: data.visaDetail.VisaInfo.VisaCertificatePath });

                                }

                                if (data.visaDetail.VisaInfo.GreenCardCertificatePath != null) {

                                    $scope.Licenses.push({ LicenseID: data.visaDetail.VisaInfo.VisaInfoID, LicenseName: "GreenCard", LicenseDocPath: data.visaDetail.VisaInfo.GreenCardCertificatePath });

                                }

                                if (data.visaDetail.VisaInfo.NationalIDCertificatePath != null) {

                                    $scope.Licenses.push({ LicenseID: data.visaDetail.VisaInfo.VisaInfoID, LicenseName: "National Identification", LicenseDocPath: data.visaDetail.VisaInfo.NationalIDCertificatePath });

                                }

                            }

                        }

                        count01++;

                    }

                }
                //if (count01 == 0 && $scope.LicenseTypes.length != 0) {

                //    $scope.FormatVisaInfoDoc(data.visaDetail);

                //}

            }

        });
        $scope.$on('UpdateFederalDEAInformation', function (event, data) {

            var count01 = 0;
            var count02 = 0;

            if (data.federalDea != null) {

                for (var i = 0; i < $scope.LicenseTypes.length; i++) {

                    if ($scope.LicenseTypes[i].LicenseTypeID == 7) {

                        for (var j = 0; j < $scope.LicenseTypes[i].Licenses.length; j++) {

                            if ($scope.LicenseTypes[i].Licenses[j].LicenseID == data.federalDea.FederalDEAInformationID) {

                                $scope.LicenseTypes[i].Licenses[j].LicenseDocPath = data.federalDea.DEALicenceCertPath;

                                count02++;

                            }

                        }

                        if (count02 == 0) {

                            if (data.federalDea.DEALicenceCertPath != null) {

                                $scope.LicenseTypes[i].Licenses.push({ LicenseID: data.federalDea.FederalDEAInformationID, LicenseName: "DEA" + ($scope.LicenseTypes[i].Licenses.length + 1), LicenseDocPath: data.federalDea.DEALicenceCertPath });

                            }

                        }

                        count01++;

                    }

                }
                //if (count01 == 0 && $scope.LicenseTypes.length != 0) {

                //    $scope.FormatFederalDEAInformationDoc([data.federalDea]);

                //}

            }

        });
        $scope.$on('UpdateMedicareInformation', function (event, data) {

            var count01 = 0;
            var count02 = 0;

            if (data.MedicareInformation != null) {

                for (var i = 0; i < $scope.LicenseTypes.length; i++) {

                    if ($scope.LicenseTypes[i].LicenseTypeID == 8) {

                        for (var j = 0; j < $scope.LicenseTypes[i].Licenses.length; j++) {

                            if ($scope.LicenseTypes[i].Licenses[j].LicenseID == data.MedicareInformation.MedicareInformationID) {

                                $scope.LicenseTypes[i].Licenses[j].LicenseDocPath = data.MedicareInformation.CertificatePath;

                                count02++;

                            }

                        }

                        if (count02 == 0) {

                            if (data.MedicareInformation.CertificatePath != null) {

                                $scope.LicenseTypes[i].Licenses.push({ LicenseID: data.MedicareInformation.MedicareInformationID, LicenseName: "Medicare" + ($scope.LicenseTypes[i].Licenses.length + 1), LicenseDocPath: data.MedicareInformation.CertificatePath });

                            }

                        }

                        count01++;

                    }

                }
                //if (count01 == 0 && $scope.LicenseTypes.length != 0) {

                //    $scope.FormatMedicareInformationDoc([data.MedicareInformation]);

                //}

            }

        });
        $scope.$on('UpdateMedicaidInformation', function (event, data) {

            var count01 = 0;
            var count02 = 0;

            if (data.MedicaidInformation != null) {

                for (var i = 0; i < $scope.LicenseTypes.length; i++) {

                    if ($scope.LicenseTypes[i].LicenseTypeID == 9) {

                        for (var j = 0; j < $scope.LicenseTypes[i].Licenses.length; j++) {

                            if ($scope.LicenseTypes[i].Licenses[j].LicenseID == data.MedicaidInformation.MedicaidInformationID) {

                                $scope.LicenseTypes[i].Licenses[j].LicenseDocPath = data.MedicaidInformation.CertificatePath;

                                count02++;

                            }

                        }

                        if (count02 == 0) {

                            if (data.MedicaidInformation.CertificatePath != null) {

                                $scope.LicenseTypes[i].Licenses.push({ LicenseID: data.MedicaidInformation.MedicaidInformationID, LicenseName: "Medicaid" + ($scope.LicenseTypes[i].Licenses.length + 1), LicenseDocPath: data.MedicaidInformation.CertificatePath });

                            }

                        }

                        count01++;

                    }

                }
                //if (count01 == 0 && $scope.LicenseTypes.length != 0) {

                //    $scope.FormatMedicaidInformationDoc([data.MedicaidInformation]);

                //}

            }

        });
        $scope.$on('UpdateCDSCInformation', function (event, data) {

            var count01 = 0;
            var count02 = 0;

            if (data.CDSCInformation != null) {

                for (var i = 0; i < $scope.LicenseTypes.length; i++) {

                    if ($scope.LicenseTypes[i].LicenseTypeID == 10) {

                        for (var j = 0; j < $scope.LicenseTypes[i].Licenses.length; j++) {

                            if ($scope.LicenseTypes[i].Licenses[j].LicenseID == data.CDSCInformation.CDSCInformationID) {

                                $scope.LicenseTypes[i].Licenses[j].LicenseDocPath = data.CDSCInformation.CDSCCerificatePath;

                                count02++;

                            }

                        }

                        if (count02 == 0) {

                            if (data.CDSCInformation.CDSCCerificatePath != null) {

                                $scope.LicenseTypes[i].Licenses.push({ LicenseID: data.CDSCInformation.CDSCInformationID, LicenseName: "CDS" + ($scope.LicenseTypes[i].Licenses.length + 1), LicenseDocPath: data.CDSCInformation.CDSCCerificatePath });

                            }

                        }

                        count01++;

                    }

                }
                //if (count01 == 0 && $scope.LicenseTypes.length != 0) {

                //    $scope.FormatCDSCInformationDoc([data.CDSCInformation]);

                //}

            }

        });
        $scope.$on('UpdateEducationDetailDoc', function (event, data) {

            var count01 = 0;
            var count02 = 0;

            if (data.educationDetails != null) {

                for (var i = 0; i < $scope.LicenseTypes.length; i++) {

                    if ($scope.LicenseTypes[i].LicenseTypeID == 11) {

                        for (var j = 0; j < $scope.LicenseTypes[i].Licenses.length; j++) {

                            if ($scope.LicenseTypes[i].Licenses[j].LicenseID == data.educationDetails.EducationDetailID) {

                                $scope.LicenseTypes[i].Licenses[j].LicenseDocPath = data.educationDetails.CertificatePath;

                                count02++;

                            }

                        }

                        if (count02 == 0) {

                            if (data.educationDetails.CertificatePath != null) {

                                if (data.educationDetails.QualificationType == "UnderGraduate") {

                                    $scope.LicenseTypes[i].Licenses.push({ LicenseID: data.educationDetails.EducationDetailID, LicenseName: "UG" + ($scope.countUG + 1), LicenseDocPath: data.educationDetails.CertificatePath });

                                    $scope.countUG++;

                                } else if (data.educationDetails.QualificationType == "Graduate") {

                                    $scope.LicenseTypes[i].Licenses.push({ LicenseID: data.educationDetails.EducationDetailID, LicenseName: "Grad" + ($scope.countG + 1), LicenseDocPath: data.educationDetails.CertificatePath });

                                    $scope.countG++;

                                }

                            }

                        }

                        count01++;

                    }

                }
                //if (count01 == 0 && $scope.LicenseTypes.length != 0) {

                //    $scope.FormatEducationDetailDoc([data.educationDetails]);

                //}

            }

        });
        $scope.$on('UpdateECFMGDetailDoc', function (event, data) {

            var count01 = 0;
            var count02 = 0;

            if (data.ecfmgDetails != null) {

                for (var i = 0; i < $scope.LicenseTypes.length; i++) {

                    if ($scope.LicenseTypes[i].LicenseTypeID == 12) {

                        for (var j = 0; j < $scope.LicenseTypes[i].Licenses.length; j++) {

                            if ($scope.LicenseTypes[i].Licenses[j].LicenseID == data.ecfmgDetails.ECFMGDetailID) {

                                $scope.LicenseTypes[i].Licenses[j].LicenseDocPath = data.ecfmgDetails.ECFMGCertPath;

                                count02++;

                            }

                        }

                        if (count02 == 0) {

                            if (data.ecfmgDetails.CertificatePath != null) {

                                $scope.LicenseTypes[i].Licenses.push({ LicenseID: data.ecfmgDetails.ECFMGDetailID, LicenseName: "ECFMG", LicenseDocPath: data.ecfmgDetails.ECFMGCertPath });

                            }

                        }

                        count01++;

                    }

                }
                //if (count01 == 0 && $scope.LicenseTypes.length != 0) {

                //    $scope.FormatECFMGDetailDoc(data.ecfmgDetails);

                //}

            }

        });
        $scope.$on('UpdateProgramDetailsDoc', function (event, data) {

            var count01 = 0;
            var count02 = 0;

            if (data.programDetails != null) {

                for (var i = 0; i < $scope.LicenseTypes.length; i++) {

                    if ($scope.LicenseTypes[i].LicenseTypeID == 13) {

                        for (var j = 0; j < $scope.LicenseTypes[i].Licenses.length; j++) {

                            if ($scope.LicenseTypes[i].Licenses[j].LicenseID == data.programDetails.ProgramDetailID) {

                                $scope.LicenseTypes[i].Licenses[j].LicenseDocPath = data.programDetails.DocumentPath;

                                count02++;

                            }

                        }

                        if (count02 == 0) {

                            if (data.programDetails.DocumentPath != null) {

                                if (data.programDetails.ProgramType == "Internship") {

                                    $scope.Licenses.push({ LicenseID: data.programDetails.ProgramDetailID, LicenseName: "Intern" + ($scope.countIntern + 1), LicenseDocPath: data.programDetails.DocumentPath });

                                    $scope.countIntern++;

                                } else if (data.programDetails.ProgramType == "Fellowship") {

                                    $scope.Licenses.push({ LicenseID: data.programDetails.ProgramDetailID, LicenseName: "Fell" + ($scope.countFell + 1), LicenseDocPath: data.programDetails.DocumentPath });

                                    $scope.countFell++;

                                } else if (data.programDetails.ProgramType == "Resident") {

                                    $scope.Licenses.push({ LicenseID: data.programDetails.ProgramDetailID, LicenseName: "Res" + ($scope.countRes + 1), LicenseDocPath: data.programDetails.DocumentPath });

                                    $scope.countRes++;

                                } else if (data.programDetails.ProgramType == "Other") {

                                    $scope.Licenses.push({ LicenseID: data.programDetails.ProgramDetailID, LicenseName: "Other" + ($scope.countOther + 1), LicenseDocPath: data.programDetails.DocumentPath });

                                    $scope.countOther++;

                                }

                            }

                        }

                        count01++;

                    }

                }
                //if (count01 == 0 && $scope.LicenseTypes.length != 0) {

                //    $scope.FormatProgramDetailsDoc([data.programDetails]);

                //}

            }

        });
        $scope.$on('UpdateCMECertificationsDoc', function (event, data) {

            var count01 = 0;
            var count02 = 0;

            if (data.CMEDetails != null) {

                for (var i = 0; i < $scope.LicenseTypes.length; i++) {

                    if ($scope.LicenseTypes[i].LicenseTypeID == 14) {

                        for (var j = 0; j < $scope.LicenseTypes[i].Licenses.length; j++) {

                            if ($scope.LicenseTypes[i].Licenses[j].LicenseID == data.CMEDetails.CMECertificationID) {

                                $scope.LicenseTypes[i].Licenses[j].LicenseDocPath = data.CMEDetails.CertificatePath;

                                count02++;

                            }

                        }

                        if (count02 == 0) {

                            if (data.CMEDetails.CertificatePath != null) {

                                $scope.LicenseTypes[i].Licenses.push({ LicenseID: data.CMEDetails.CMECertificationID, LicenseName: "CME" + ($scope.LicenseTypes[i].Licenses.length + 1), LicenseDocPath: data.CMEDetails.CertificatePath });

                            }

                        }

                        count01++;

                    }

                }
                //if (count01 == 0 && $scope.LicenseTypes.length != 0) {

                //    $scope.FormatCMECertificationsDoc([data.CMEDetails]);

                //}

            }

        });
        $scope.$on('UpdateSpecialtyBoardCertifiedDetailDoc', function (event, data) {

            var count01 = 0;
            var count02 = 0;

            if (data.specialty != null && data.specialty.SpecialtyBoardCertifiedDetail != null) {

                for (var i = 0; i < $scope.LicenseTypes.length; i++) {

                    if ($scope.LicenseTypes[i].LicenseTypeID == 15) {

                        for (var j = 0; j < $scope.LicenseTypes[i].Licenses.length; j++) {

                            if ($scope.LicenseTypes[i].Licenses[j].LicenseID == data.specialty.SpecialtyBoardCertifiedDetail.SpecialtyBoardCertifiedDetailID) {

                                $scope.LicenseTypes[i].Licenses[j].LicenseDocPath = data.specialty.SpecialtyBoardCertifiedDetail.BoardCertificatePath;

                                count02++;

                            }

                        }

                        if (count02 == 0) {

                            if (data.specialty.SpecialtyBoardCertifiedDetail.BoardCertificatePath != null) {

                                $scope.LicenseTypes[i].Licenses.push({ LicenseID: data.specialty.SpecialtyBoardCertifiedDetail.SpecialtyBoardCertifiedDetailID, LicenseName: "Board" + ($scope.LicenseTypes[i].Licenses.length + 1), LicenseDocPath: data.specialty.SpecialtyBoardCertifiedDetail.BoardCertificatePath });

                            }

                        }

                        count01++;

                    }

                }
                //if (count01 == 0 && $scope.LicenseTypes.length != 0) {

                //    $scope.FormatSpecialtyBoardCertifiedDetailDoc([data.specialty]);

                //}

            }

        });
        $scope.$on('UpdateHospitalPrivilegeDetailDoc', function (event, data) {

            var count01 = 0;
            var count02 = 0;

            if (data.hospitalPrivilegeDetail != null) {

                for (var i = 0; i < $scope.LicenseTypes.length; i++) {

                    if ($scope.LicenseTypes[i].LicenseTypeID == 16) {

                        for (var j = 0; j < $scope.LicenseTypes[i].Licenses.length; j++) {

                            if ($scope.LicenseTypes[i].Licenses[j].LicenseID == data.hospitalPrivilegeDetail.HospitalPrivilegeDetailID) {

                                $scope.LicenseTypes[i].Licenses[j].LicenseDocPath = data.hospitalPrivilegeDetail.HospitalPrevilegeLetterPath;

                                count02++;

                            }

                        }

                        if (count02 == 0) {

                            if (data.hospitalPrivilegeDetail.HospitalPrevilegeLetterPath != null) {

                                $scope.LicenseTypes[i].Licenses.push({ LicenseID: data.hospitalPrivilegeDetail.HospitalPrivilegeDetailID, LicenseName: "HPL" + ($scope.LicenseTypes[i].Licenses.length + 1), LicenseDocPath: data.hospitalPrivilegeDetail.HospitalPrevilegeLetterPath });

                            }

                        }

                        count01++;

                    }

                }
                //if (count01 == 0 && $scope.LicenseTypes.length != 0) {

                //    $scope.FormatHospitalPrivilegeDetailDoc([data.hospitalPrivilegeDetail]);

                //}

            }

        });
        $scope.$on('UpdateProfessionalLiabilityInfoDoc', function (event, data) {

            var count01 = 0;
            var count02 = 0;

            if (data.professionalLiability != null) {

                for (var i = 0; i < $scope.LicenseTypes.length; i++) {

                    if ($scope.LicenseTypes[i].LicenseTypeID == 17) {

                        for (var j = 0; j < $scope.LicenseTypes[i].Licenses.length; j++) {

                            if ($scope.LicenseTypes[i].Licenses[j].LicenseID == data.professionalLiability.ProfessionalLiabilityInfoID) {

                                $scope.LicenseTypes[i].Licenses[j].LicenseDocPath = data.professionalLiability.InsuranceCertificatePath;

                                count02++;

                            }

                        }

                        if (count02 == 0) {

                            if (data.professionalLiability.InsuranceCertificatePath != null) {

                                $scope.LicenseTypes[i].Licenses.push({ LicenseID: data.professionalLiability.ProfessionalLiabilityInfoID, LicenseName: "LIC" + ($scope.LicenseTypes[i].Licenses.length + 1), LicenseDocPath: data.professionalLiability.InsuranceCertificatePath });

                            }

                        }

                        count01++;

                    }

                }
                //if (count01 == 0 && $scope.LicenseTypes.length != 0) {

                //    $scope.FormatProfessionalLiabilityInfoDoc([data.professionalLiability]);

                //}

            }

        });
        $scope.$on('UpdateProfessionalWorkExperienceDoc', function (event, data) {

            var count01 = 0;
            var count02 = 0;

            if (data.professionalWorkExperience != null) {

                for (var i = 0; i < $scope.LicenseTypes.length; i++) {

                    if ($scope.LicenseTypes[i].LicenseTypeID == 18) {

                        for (var j = 0; j < $scope.LicenseTypes[i].Licenses.length; j++) {

                            if ($scope.LicenseTypes[i].Licenses[j].LicenseID == data.professionalWorkExperience.ProfessionalLiabilityInfoID) {

                                $scope.LicenseTypes[i].Licenses[j].LicenseDocPath = data.professionalWorkExperience.WorkExperienceDocPath;

                                count02++;

                            }

                        }

                        if (count02 == 0) {

                            if (data.professionalWorkExperience.WorkExperienceDocPath != null) {

                                $scope.LicenseTypes[i].Licenses.push({ LicenseID: data.professionalWorkExperience.ProfessionalLiabilityInfoID, LicenseName: "Work Exp" + ($scope.LicenseTypes[i].Licenses.length + 1), LicenseDocPath: data.professionalWorkExperience.WorkExperienceDocPath });

                            }

                        }

                        count01++;

                    }

                }
                //if (count01 == 0 && $scope.LicenseTypes.length != 0) {

                //    $scope.FormatProfessionalWorkExperienceDoc([data.professionalWorkExperience]);

                //}

            }

        });
        $scope.$on('UpdateContractInfoeDoc', function (event, data) {

            var count01 = 0;
            var count02 = 0;

            if (data.contractInformation != null) {

                for (var i = 0; i < $scope.LicenseTypes.length; i++) {

                    if ($scope.LicenseTypes[i].LicenseTypeID == 19) {

                        for (var j = 0; j < $scope.LicenseTypes[i].Licenses.length; j++) {

                            if ($scope.LicenseTypes[i].Licenses[j].LicenseID == data.contractInformation.ContractInfoID) {

                                $scope.LicenseTypes[i].Licenses[j].LicenseDocPath = data.contractInformation.ContractFilePath;

                                count02++;

                            }

                        }

                        if (count02 == 0) {

                            if (data.contractInformation.ContractFilePath != null) {

                                $scope.LicenseTypes[i].Licenses.push({ LicenseID: data.contractInformation.ContractInfoID, LicenseName: "Contract Document", LicenseDocPath: data.contractInformation.ContractFilePath });

                            }

                        }

                        count01++;

                    }

                }
                //if (count01 == 0 && $scope.LicenseTypes.length != 0) {

                //    $scope.FormatContractInfoeDoc([data.contractInformation]);

                //}

            }

        });

        $scope.$on('RemoveStateLicenses', function (event, data) {

            var count = 0;

            if (data.stateLicense != null) {

                for (var i = 0; i < $scope.LicenseTypes.length; i++) {

                    if ($scope.LicenseTypes[i].LicenseTypeID == 1) {

                        for (var j = 0; j < $scope.LicenseTypes[i].Licenses.length; j++) {

                            if ($scope.LicenseTypes[i].Licenses[j].LicenseID == data.stateLicense.StateLicenseInformationID) {

                                //if ($scope.LicenseTypes[i].Licenses.length == 1) {

                                //    $scope.LicenseTypes.splice($scope.LicenseTypes.indexOf($scope.LicenseTypes[i]), 1);

                                //} else {

                                $scope.LicenseTypes[i].Licenses.splice($scope.LicenseTypes[i].Licenses.indexOf($scope.LicenseTypes[i].Licenses[j]), 1);

                                count = 1;

                                //}

                            }
                            if (count == 1) {

                                $scope.LicenseTypes[i].Licenses[j].LicenseName = "ML" + (j + 1);

                            }

                        }

                    }

                }

            }

        });
        $scope.$on('RemoveOtherLegalNames', function (event, data) {

            var count = 0;

            if (data.otherLegalName != null) {

                for (var i = 0; i < $scope.LicenseTypes.length; i++) {

                    if ($scope.LicenseTypes[i].LicenseTypeID == 2) {

                        for (var j = 0; j < $scope.LicenseTypes[i].Licenses.length; j++) {

                            if ($scope.LicenseTypes[i].Licenses[j].LicenseID == data.otherLegalName.OtherLegalNameID) {

                                //if ($scope.LicenseTypes[i].Licenses.length == 1) {

                                //    $scope.LicenseTypes.splice($scope.LicenseTypes.indexOf($scope.LicenseTypes[i]), 1);

                                //} else {

                                $scope.LicenseTypes[i].Licenses.splice($scope.LicenseTypes[i].Licenses.indexOf($scope.LicenseTypes[i].Licenses[j]), 1);

                                count = 1;

                                //}

                            }

                            if (count == 1) {

                                $scope.LicenseTypes[i].Licenses[j].LicenseName = "OLN" + (j + 1);

                            }

                        }

                    }

                }

            }

        });
        $scope.$on('RemoveFederalDEAInformation', function (event, data) {

            var count = 0;

            if (data.federalDea != null) {

                for (var i = 0; i < $scope.LicenseTypes.length; i++) {

                    if ($scope.LicenseTypes[i].LicenseTypeID == 7) {

                        for (var j = 0; j < $scope.LicenseTypes[i].Licenses.length; j++) {

                            if ($scope.LicenseTypes[i].Licenses[j].LicenseID == data.federalDea.FederalDEAInformationID) {

                                //if ($scope.LicenseTypes[i].Licenses.length == 1) {

                                //    $scope.LicenseTypes.splice($scope.LicenseTypes.indexOf($scope.LicenseTypes[i]), 1);

                                //} else {

                                $scope.LicenseTypes[i].Licenses.splice($scope.LicenseTypes[i].Licenses.indexOf($scope.LicenseTypes[i].Licenses[j]), 1);

                                count = 1;

                                //}

                            }

                            if (count == 1) {

                                $scope.LicenseTypes[i].Licenses[j].LicenseName = "DEA" + (j + 1);

                            }

                        }

                    }

                }

            }

        });
        $scope.$on('RemoveMedicareInformation', function (event, data) {

            var count = 0;

            if (data.MedicareInfo != null) {

                for (var i = 0; i < $scope.LicenseTypes.length; i++) {

                    if ($scope.LicenseTypes[i].LicenseTypeID == 8) {

                        for (var j = 0; j < $scope.LicenseTypes[i].Licenses.length; j++) {

                            if ($scope.LicenseTypes[i].Licenses[j].LicenseID == data.MedicareInfo.MedicareInformationID) {

                                //if ($scope.LicenseTypes[i].Licenses.length == 1) {

                                //    $scope.LicenseTypes.splice($scope.LicenseTypes.indexOf($scope.LicenseTypes[i]), 1);

                                //} else {

                                $scope.LicenseTypes[i].Licenses.splice($scope.LicenseTypes[i].Licenses.indexOf($scope.LicenseTypes[i].Licenses[j]), 1);

                                count == 1;

                                //}

                            }

                            if (count == 1) {

                                $scope.LicenseTypes[i].Licenses[j].LicenseName = "Medicare" + (j + 1);

                            }

                        }

                    }

                }

            }

        });
        $scope.$on('RemoveMedicaidInformation', function (event, data) {

            var count = 0;

            if (data.MedicaidInfo != null) {

                for (var i = 0; i < $scope.LicenseTypes.length; i++) {

                    if ($scope.LicenseTypes[i].LicenseTypeID == 9) {

                        for (var j = 0; j < $scope.LicenseTypes[i].Licenses.length; j++) {

                            if ($scope.LicenseTypes[i].Licenses[j].LicenseID == data.MedicaidInfo.MedicaidInformationID) {

                                //if ($scope.LicenseTypes[i].Licenses.length == 1) {

                                //    $scope.LicenseTypes.splice($scope.LicenseTypes.indexOf($scope.LicenseTypes[i]), 1);

                                //} else {

                                $scope.LicenseTypes[i].Licenses.splice($scope.LicenseTypes[i].Licenses.indexOf($scope.LicenseTypes[i].Licenses[j]), 1);

                                count == 1;

                                //}

                            }

                            if (count == 1) {

                                $scope.LicenseTypes[i].Licenses[j].LicenseName = "Medicaid" + (j + 1);

                            }

                        }

                    }

                }

            }

        });
        $scope.$on('RemoveCDSCInformation', function (event, data) {

            var count = 0;

            if (data.cDSCInformation != null) {

                for (var i = 0; i < $scope.LicenseTypes.length; i++) {

                    if ($scope.LicenseTypes[i].LicenseTypeID == 10) {

                        for (var j = 0; j < $scope.LicenseTypes[i].Licenses.length; j++) {

                            if ($scope.LicenseTypes[i].Licenses[j].LicenseID == data.cDSCInformation.CDSCInformationID) {

                                //if ($scope.LicenseTypes[i].Licenses.length == 1) {

                                //    $scope.LicenseTypes.splice($scope.LicenseTypes.indexOf($scope.LicenseTypes[i]), 1);

                                //} else {

                                $scope.LicenseTypes[i].Licenses.splice($scope.LicenseTypes[i].Licenses.indexOf($scope.LicenseTypes[i].Licenses[j]), 1);

                                count == 1;

                                //}

                            }

                            if (count == 1) {

                                $scope.LicenseTypes[i].Licenses[j].LicenseName = "CDS" + (j + 1);

                            }

                        }

                    }

                }

            }

        });
        $scope.$on('RemoveEducationDetailDoc', function (event, data) {

            var count = 0;

            if (data.educationDetailViewModel != null) {

                for (var i = 0; i < $scope.LicenseTypes.length; i++) {

                    if ($scope.LicenseTypes[i].LicenseTypeID == 11) {

                        for (var j = 0; j < $scope.LicenseTypes[i].Licenses.length; j++) {

                            if ($scope.LicenseTypes[i].Licenses[j].LicenseID == data.educationDetailViewModel.EducationDetailID) {

                                //if ($scope.LicenseTypes[i].Licenses.length == 1) {

                                //    $scope.LicenseTypes.splice($scope.LicenseTypes.indexOf($scope.LicenseTypes[i]), 1);

                                //} else {

                                if ($scope.LicenseTypes[i].Licenses[j].LicenseName == "UG" + (j)) {

                                    $scope.countUG--;

                                } else if ($scope.LicenseTypes[i].Licenses[j].LicenseName == "Grad" + (j)) {

                                    $scope.countG--;

                                }

                                $scope.LicenseTypes[i].Licenses.splice($scope.LicenseTypes[i].Licenses.indexOf($scope.LicenseTypes[i].Licenses[j]), 1);

                                count == 1;

                                //}

                            }

                            if (count == 1) {

                                if ($scope.LicenseTypes[i].Licenses[j].LicenseName == "UG" + (j)) {

                                    $scope.LicenseTypes[i].Licenses[j].LicenseName = "UG" + (j + 1);

                                } else if ($scope.LicenseTypes[i].Licenses[j].LicenseName == "Grad" + (j)) {

                                    $scope.LicenseTypes[i].Licenses[j].LicenseName = "Grad" + (j + 1);

                                }

                            }

                        }

                    }

                }

            }

        });
        $scope.$on('RemoveProgramDetailsDoc', function (event, data) {

            var count = 0;

            if (data.residencyInternshipViewModel != null) {

                for (var i = 0; i < $scope.LicenseTypes.length; i++) {

                    if ($scope.LicenseTypes[i].LicenseTypeID == 13) {

                        for (var j = 0; j < $scope.LicenseTypes[i].Licenses.length; j++) {

                            if ($scope.LicenseTypes[i].Licenses[j].LicenseID == data.residencyInternshipViewModel.ProgramDetailID) {

                                //if ($scope.LicenseTypes[i].Licenses.length == 1) {

                                //    $scope.LicenseTypes.splice($scope.LicenseTypes.indexOf($scope.LicenseTypes[i]), 1);

                                //} else {

                                if ($scope.LicenseTypes[i].Licenses[j].LicenseName == "Intern" + (j)) {

                                    $scope.countIntern--;

                                } else if ($scope.LicenseTypes[i].Licenses[j].LicenseName == "Fell" + (j)) {

                                    $scope.countFell--;

                                } else if ($scope.LicenseTypes[i].Licenses[j].LicenseName == "Res" + (j)) {

                                    $scope.countRes--;

                                } else if ($scope.LicenseTypes[i].Licenses[j].LicenseName == "Other" + (j)) {

                                    $scope.countOther--;

                                }

                                $scope.LicenseTypes[i].Licenses.splice($scope.LicenseTypes[i].Licenses.indexOf($scope.LicenseTypes[i].Licenses[j]), 1);

                                count == 1;

                                //}

                            }

                            if (count == 1) {

                                if ($scope.LicenseTypes[i].Licenses[j].LicenseName == "Intern" + (j)) {

                                    $scope.LicenseTypes[i].Licenses[j].LicenseName = "Intern" + (j + 1);

                                } else if ($scope.LicenseTypes[i].Licenses[j].LicenseName == "Fell" + (j)) {

                                    $scope.LicenseTypes[i].Licenses[j].LicenseName = "Fell" + (j + 1);

                                } else if ($scope.LicenseTypes[i].Licenses[j].LicenseName == "Res" + (j)) {

                                    $scope.LicenseTypes[i].Licenses[j].LicenseName = "Res" + (j + 1);

                                } else if ($scope.LicenseTypes[i].Licenses[j].LicenseName == "Other" + (j)) {

                                    $scope.LicenseTypes[i].Licenses[j].LicenseName = "Other" + (j + 1);

                                }

                            }

                        }

                    }

                }

            }

        });
        $scope.$on('RemoveCMECertificationsDoc', function (event, data) {

            var count = 0;

            if (data.certificationCMEViewModel != null) {

                for (var i = 0; i < $scope.LicenseTypes.length; i++) {

                    if ($scope.LicenseTypes[i].LicenseTypeID == 14) {

                        for (var j = 0; j < $scope.LicenseTypes[i].Licenses.length; j++) {

                            if ($scope.LicenseTypes[i].Licenses[j].LicenseID == data.certificationCMEViewModel.CMECertificationID) {

                                //if ($scope.LicenseTypes[i].Licenses.length == 1) {

                                //    $scope.LicenseTypes.splice($scope.LicenseTypes.indexOf($scope.LicenseTypes[i]), 1);

                                //} else {

                                $scope.LicenseTypes[i].Licenses.splice($scope.LicenseTypes[i].Licenses.indexOf($scope.LicenseTypes[i].Licenses[j]), 1);

                                count = 1;

                                //}

                            }

                            if (count == 1) {

                                $scope.LicenseTypes[i].Licenses[j].LicenseName = "CME" + (j + 1);

                            }

                        }

                    }

                }

            }

        });
        $scope.$on('RemoveSpecialtyBoardCertifiedDetailDoc', function (event, data) {

            var count = 0;

            if (data.specialty != null && data.specialty.SpecialtyBoardCertifiedDetail != null) {

                for (var i = 0; i < $scope.LicenseTypes.length; i++) {

                    if ($scope.LicenseTypes[i].LicenseTypeID == 15) {

                        for (var j = 0; j < $scope.LicenseTypes[i].Licenses.length; j++) {

                            if ($scope.LicenseTypes[i].Licenses[j].LicenseID == data.specialty.SpecialtyBoardCertifiedDetail.SpecialtyBoardCertifiedDetailID) {

                                //if ($scope.LicenseTypes[i].Licenses.length == 1) {

                                //    $scope.LicenseTypes.splice($scope.LicenseTypes.indexOf($scope.LicenseTypes[i]), 1);

                                //} else {

                                $scope.LicenseTypes[i].Licenses.splice($scope.LicenseTypes[i].Licenses.indexOf($scope.LicenseTypes[i].Licenses[j]), 1);

                                count = 1;

                                //}

                            }

                            if (count == 1) {

                                $scope.LicenseTypes[i].Licenses[j].LicenseName = "Board" + (j + 1);

                            }

                        }

                    }

                }

            }

        });
        $scope.$on('RemoveHospitalPrivilegeDetailDoc', function (event, data) {

            var count = 0;

            if (data.hospitalPrivilege != null) {

                for (var i = 0; i < $scope.LicenseTypes.length; i++) {

                    if ($scope.LicenseTypes[i].LicenseTypeID == 16) {

                        for (var j = 0; j < $scope.LicenseTypes[i].Licenses.length; j++) {

                            if ($scope.LicenseTypes[i].Licenses[j].LicenseID == data.hospitalPrivilege.HospitalPrivilegeDetailID) {

                                //if ($scope.LicenseTypes[i].Licenses.length == 1) {

                                //    $scope.LicenseTypes.splice($scope.LicenseTypes.indexOf($scope.LicenseTypes[i]), 1);

                                //} else {

                                $scope.LicenseTypes[i].Licenses.splice($scope.LicenseTypes[i].Licenses.indexOf($scope.LicenseTypes[i].Licenses[j]), 1);

                                count = 1;

                                //}

                            }

                            if (count == 1) {

                                $scope.LicenseTypes[i].Licenses[j].LicenseName = "HPL" + (j + 1);

                            }

                        }

                    }

                }

            }

        });
        $scope.$on('RemoveProfessionalLiabilityInfoDoc', function (event, data) {

            var count = 0;

            if (data.professionalLiability != null) {

                for (var i = 0; i < $scope.LicenseTypes.length; i++) {

                    if ($scope.LicenseTypes[i].LicenseTypeID == 17) {

                        for (var j = 0; j < $scope.LicenseTypes[i].Licenses.length; j++) {

                            if ($scope.LicenseTypes[i].Licenses[j].LicenseID == data.professionalLiability.ProfessionalLiabilityInfoID) {

                                //if ($scope.LicenseTypes[i].Licenses.length == 1) {

                                //    $scope.LicenseTypes.splice($scope.LicenseTypes.indexOf($scope.LicenseTypes[i]), 1);

                                //} else {

                                $scope.LicenseTypes[i].Licenses.splice($scope.LicenseTypes[i].Licenses.indexOf($scope.LicenseTypes[i].Licenses[j]), 1);

                                count = 1;

                                //}

                            }

                            if (count == 1) {

                                $scope.LicenseTypes[i].Licenses[j].LicenseName = "LIC" + (j + 1);

                            }

                        }

                    }

                }

            }

        });
        $scope.$on('RemoveProfessionalWorkExperienceDoc', function (event, data) {

            var count = 0;

            if (data.professionalWorkExperience != null) {

                for (var i = 0; i < $scope.LicenseTypes.length; i++) {

                    if ($scope.LicenseTypes[i].LicenseTypeID == 18) {

                        for (var j = 0; j < $scope.LicenseTypes[i].Licenses.length; j++) {

                            if ($scope.LicenseTypes[i].Licenses[j].LicenseID == data.professionalWorkExperience.ProfessionalWorkExperienceID) {

                                //if ($scope.LicenseTypes[i].Licenses.length == 1) {

                                //    $scope.LicenseTypes.splice($scope.LicenseTypes.indexOf($scope.LicenseTypes[i]), 1);

                                //} else {

                                $scope.LicenseTypes[i].Licenses.splice($scope.LicenseTypes[i].Licenses.indexOf($scope.LicenseTypes[i].Licenses[j]), 1);

                                count = 1;

                                //}

                            }

                            if (count == 1) {

                                $scope.LicenseTypes[i].Licenses[j].LicenseName = "Work Exp" + (j + 1);

                            }

                        }

                    }

                }

            }

        });

        $scope.tempObj = {};

        $scope.moveDocumentUp = function (licenseType, license) {

            //var tempObj = {};

            var i = licenseType.indexOf(license);

            $scope.tempObj = licenseType[i - 1];
            licenseType[i - 1] = licenseType[i];
            licenseType[i] = $scope.tempObj;

            $scope.tempObj = {};

        }

        $scope.moveDocumentDown = function (licenseType, license) {

            //var tempObj = {};

            var i = licenseType.indexOf(license);

            $scope.tempObj = licenseType[i + 1];
            licenseType[i + 1] = licenseType[i];
            licenseType[i] = $scope.tempObj;

            $scope.tempObj = {};

        }

        $scope.countDocument = 0;

        $scope.CountDoc = function () {

            $scope.countDocument += 1;

            console.log($scope.countDocument);
            console.log(index);

        }

        $scope.SendToPrinter = function (DocPath) {

            $.ajax({
                url: '/Credentialing/DocChecklist/SendToPrinter?DocumentPath=' + DocPath,
                type: 'POST',
                data: { DocumentPath: DocPath },
                async: false,
                cache: false,
                contentType: false,
                processData: false,
                success: function (data) {

                }
            });

        }

        $scope.DownloadFile = function (DocPath) {
            $scope.progressbar = true;
            $.ajax({
                url: '/Credentialing/DocChecklist/DownloadFile?DocumentPath=' + DocPath,
                type: 'POST',
                data: { DocumentPath: DocPath },
                async: false,
                cache: false,
                contentType: false,
                processData: false,
                success: function (data) {
                    console.log(data);

                    $timeout(function () {
                        $scope.progressbar = false;

                        $scope.IsDownloadMessage = true;

                        $timeout(function () {
                            $scope.IsDownloadMessage = false;
                        }, 5000);

                    }, 5000);

                }
            });

        }

        $scope.SelectedDoc = function (Doc) {

            var i = $scope.spaDoc.indexOf(Doc);

            $scope.tempObj = $scope.spaDoc[i - 1];
            $scope.spaDoc[i - 1] = $scope.spaDoc[i];
            $scope.spaDoc[i] = $scope.tempObj;

            $scope.tempObj = {};

        }

    }]);

//------------------- Form Reset Function ------------------------
var FormReset = function ($form) {

    // get validator object
    var $validator = $form.validate();

    // get errors that were created using jQuery.validate.unobtrusive
    var $errors = $form.find(".field-validation-error span");

    // trick unobtrusive to think the elements were successfully validated
    // this removes the validation messages
    $errors.each(function () {
        $validator.settings.success($(this));
    });
    // clear errors from validation
    $validator.resetForm();
};


function ResetFormForValidation(form) {
    form.removeData('validator');
    form.removeData('unobtrusiveValidation');
    $.validator.unobtrusive.parse(form);
}