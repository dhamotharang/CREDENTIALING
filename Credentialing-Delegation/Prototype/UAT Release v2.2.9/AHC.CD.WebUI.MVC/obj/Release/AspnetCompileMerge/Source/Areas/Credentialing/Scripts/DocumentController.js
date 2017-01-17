
//-------------------- document controller, author : krglv -------------------------

Cred_SPA_App.controller('DocumentController',
    function ($scope, $rootScope, $timeout, $http, messageAlertEngine) {

        $rootScope.DocumentLoaded = true;
        $scope.progressbar = false;

        $scope.getData = function (profileID) {
            console.log("2");
            if (!$scope.dataLoaded) {
                console.log("3");
                $rootScope.DocumentLoaded = false;
                $http({
                    method: 'GET',
                    url: '/Credentialing/DocChecklist/GetDocumentProfileDataAsync?profileId=' + profileID
                }).success(function (data, status, headers, config) {
                    console.log("success");
                    console.log(data);
                    $scope.createLicenseArray();
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

        $scope.spaDoc = [];

        $scope.FormatStateLicense = function (Docs) {

            //$scope.Licenses = [];
            //$scope.LicenseHistories = [];

            for (var i = 0; i < Docs.length; i++) {

                if (Docs[i].StateLicenseDocumentPath != null) {

                    $scope.LicenseTypes[0].Licenses.push({ LicenseID: Docs[i].StateLicenseInformationID, LicenseName: "ML" + (i + 1), LicenseDocPath: Docs[i].StateLicenseDocumentPath, ModifiedDate: Docs[i].LastModifiedDate, Description: "State License" });

                    if (Docs[i].StateLicenseInfoHistory != null) {

                        for (var j = 0; j < Docs[i].StateLicenseInfoHistory.length; j++) {

                            if (Docs[i].StateLicenseInfoHistory[j] != null) {

                                $scope.LicenseTypes[0].LicenseHistories.push({ LicenseID: Docs[i].StateLicenseInfoHistory[j].StatelicenseInfoHistoryID, LicenseName: "ML" + (j + 1), LicenseDocPath: Docs[i].StateLicenseInfoHistory[j].StatelicenseDocumentPath, removeDate: Docs[i].StateLicenseInfoHistory[j].LastModifiedDate });

                            }

                        }

                    }

                }

            }

            //if ($scope.Licenses.length != 0) {

            //$scope.LicenseTypes.push({ LicenseTypeID: 1, LicenseTypeName: "State License", Licenses: $scope.Licenses, LicenseHistories: $scope.LicenseHistories });

            //}

        }

        $scope.FormatOtherLegalNamesDoc = function (Docs) {

            //$scope.Licenses = [];
            //$scope.LicenseHistories = [];

            for (var i = 0; i < Docs.length; i++) {

                if (Docs[i].DocumentPath != null) {

                    $scope.LicenseTypes[1].Licenses.push({ LicenseID: Docs[i].OtherLegalNameID, LicenseName: "OLN" + (i + 1), LicenseDocPath: Docs[i].DocumentPath, ModifiedDate: Docs[i].LastModifiedDate, Description: "Other Legal Name" });

                    if (Docs[i].OtherLegalNameHistory != null) {

                        $scope.LicenseTypes[1].LicenseHistories.push({ LicenseID: Docs[i].OtherLegalNameHistory[j].OtherLegalNameHistoryID, LicenseName: "OLN" + (j + 1), LicenseDocPath: Docs[i].OtherLegalNameHistory[j].DocumentPath, removeDate: Docs[i].OtherLegalNameHistory[j].LastModifiedDate });

                    }

                }

            }

            //if ($scope.Licenses.length != 0) {

            //$scope.LicenseTypes.push({ LicenseTypeID: 2, LicenseTypeName: "Other Legal Name Document", Licenses: $scope.Licenses, LicenseHistories: $scope.LicenseHistories });

            //}

        }

        $scope.FormatPersonalIdentificationDoc = function (Docs) {

            //$scope.Licenses = [];
            //$scope.LicenseHistories = [];

            if (Docs.SSNCertificatePath != null) {

                $scope.LicenseTypes[2].Licenses.push({ LicenseID: Docs.PersonalIdentificationID, LicenseName: "SSN", LicenseDocPath: Docs.SSNCertificatePath, ModifiedDate: Docs.LastModifiedDate, Description: "Social Security Number" });

            }

            if (Docs.DLCertificatePath != null) {

                $scope.LicenseTypes[2].Licenses.push({ LicenseID: Docs.PersonalIdentificationID, LicenseName: "DL", LicenseDocPath: Docs.DLCertificatePath, ModifiedDate: Docs.LastModifiedDate, Description: "Driver License" });

            }

            //if ($scope.Licenses.length != 0) {

            //$scope.LicenseTypes.push({ LicenseTypeID: 3, LicenseTypeName: "Personal Identification Document", Licenses: $scope.Licenses, LicenseHistories: $scope.LicenseHistories });

            //}

        }

        $scope.FormatBirthInformationDoc = function (Docs) {

            //$scope.Licenses = [];
            //$scope.LicenseHistories = [];

            if (Docs.BirthCertificatePath != null) {

                $scope.LicenseTypes[3].Licenses.push({ LicenseID: Docs.BirthInformationID, LicenseName: "Birth Certificate", LicenseDocPath: Docs.BirthCertificatePath, ModifiedDate: Docs.LastModifiedDate, Description: "Birth Certificate" });

                $scope.LicenseTypes[3].LicenseHistories.push({ LicenseID: 1, LicenseName: "Birth Certificate", LicenseDocPath: Docs.BirthCertificatePath, removeDate: Docs.LastModifiedDate });

            }

            //$scope.LicenseTypes.push({ LicenseTypeID: 4, LicenseTypeName: "Birth Document", Licenses: $scope.Licenses, LicenseHistories: $scope.LicenseHistories });

        }

        $scope.FormatCVInformationDoc = function (Docs) {

            //$scope.Licenses = [];
            //$scope.LicenseHistories = [];

            if (Docs.CVDocumentPath != null) {

                $scope.LicenseTypes[4].Licenses.push({ LicenseID: Docs.CVInformationID, LicenseName: "CV", LicenseDocPath: Docs.CVDocumentPath, ModifiedDate: Docs.LastModifiedDate, Description: "Curriculum Vitae" });

            }

            //$scope.LicenseTypes.push({ LicenseTypeID: 5, LicenseTypeName: "CV Document", Licenses: $scope.Licenses, LicenseHistories: $scope.LicenseHistories });

        }

        $scope.FormatVisaInfoDoc = function (Docs) {

            //$scope.Licenses = [];
            //$scope.LicenseHistories = [];

            if (Docs.VisaInfo != null) {

                if (Docs.VisaInfo.VisaCertificatePath != null) {

                    $scope.LicenseTypes[5].Licenses.push({ LicenseID: Docs.VisaInfo.VisaInfoID, LicenseName: "VISA", LicenseDocPath: Docs.VisaInfo.VisaCertificatePath, ModifiedDate: Docs.LastModifiedDate, Description: "Visa" });

                }

                if (Docs.VisaInfo.GreenCardCertificatePath != null) {

                    $scope.LicenseTypes[5].Licenses.push({ LicenseID: Docs.VisaInfo.VisaInfoID, LicenseName: "GreenCard", LicenseDocPath: Docs.VisaInfo.GreenCardCertificatePath, ModifiedDate: Docs.LastModifiedDate, Description: "Green Card" });

                }

                if (Docs.VisaInfo.NationalIDCertificatePath != null) {

                    $scope.LicenseTypes[5].Licenses.push({ LicenseID: Docs.VisaInfo.VisaInfoID, LicenseName: "National Identification", LicenseDocPath: Docs.VisaInfo.NationalIDCertificatePath, ModifiedDate: Docs.LastModifiedDate, Description: "National Identification Certificate" });

                }

                //$scope.LicenseTypes.push({ LicenseTypeID: 6, LicenseTypeName: "Citizenship Document", Licenses: $scope.Licenses, LicenseHistories: $scope.LicenseHistories });

            }

        }

        $scope.FormatFederalDEAInformationDoc = function (Docs) {

            $scope.Licenses = [];
            $scope.LicenseHistories = [];

            for (var i = 0; i < Docs.length; i++) {

                if (Docs[i].DEALicenceCertPath != null) {

                    $scope.LicenseTypes[6].Licenses.push({ LicenseID: Docs[i].FederalDEAInformationID, LicenseName: "DEA" + (i + 1), LicenseDocPath: Docs[i].DEALicenceCertPath, ModifiedDate: Docs[i].LastModifiedDate, Description: "Drug Enforcement Administration" });

                    if (Docs[i].FederalDEAInfoHistory != null) {

                        for (var j = 0; j < Docs[i].FederalDEAInfoHistory.length; j++) {

                            $scope.LicenseTypes[6].LicenseHistories.push({ LicenseID: Docs[i].FederalDEAInfoHistory[j].FederalDEAInfoHistoryID, LicenseName: "DEA" + (j + 1), LicenseDocPath: Docs[i].FederalDEAInfoHistory[j].FederalDEADocumentPath, removeDate: Docs[i].FederalDEAInfoHistory[j].LastModifiedDate });

                        }

                    }

                }

            }

            //if ($scope.Licenses.length != 0) {

            //$scope.LicenseTypes.push({ LicenseTypeID: 7, LicenseTypeName: "Federal DEA Document", Licenses: $scope.Licenses, LicenseHistories: $scope.LicenseHistories });

            //}

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

            //if ($scope.Licenses.length != 0) {

            //$scope.LicenseTypes.push({ LicenseTypeID: 8, LicenseTypeName: "Medicare Document", Licenses: $scope.Licenses, LicenseHistories: $scope.LicenseHistories });

            //}

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

            //if ($scope.Licenses.length != 0) {

            //$scope.LicenseTypes.push({ LicenseTypeID: 9, LicenseTypeName: "Medicaid Document", Licenses: $scope.Licenses, LicenseHistories: $scope.LicenseHistories });

            //}

        }

        $scope.FormatCDSCInformationDoc = function (Docs) {

            $scope.Licenses = [];
            $scope.LicenseHistories = [];

            for (var i = 0; i < Docs.length; i++) {

                if (Docs[i].CDSCCerificatePath != null) {

                    $scope.LicenseTypes[9].Licenses.push({ LicenseID: Docs[i].CDSCInformationID, LicenseName: "CDS" + (i + 1), LicenseDocPath: Docs[i].CDSCCerificatePath, ModifiedDate: Docs[i].LastModifiedDate, Description: "Central Drug Standard Control" });

                    if (Docs[i].CDSCInfoHistory != null) {

                        for (var j = 0; j < Docs[i].CDSCInfoHistory.length; j++) {

                            $scope.LicenseTypes[9].LicenseHistories.push({ LicenseID: Docs[i].CDSCInfoHistory[j].CDSCInfoHistoryID, LicenseName: "CDS" + (j + 1), LicenseDocPath: Docs[i].CDSCInfoHistory[j].CDSCCerificatePath, removeDate: Docs[i].CDSCInfoHistory[j].LastModifiedDate });

                        }

                    }

                }

            }

            //if ($scope.Licenses.length != 0) {

            //$scope.LicenseTypes.push({ LicenseTypeID: 10, LicenseTypeName: "CDSC Document", Licenses: $scope.Licenses, LicenseHistories: $scope.LicenseHistories });

            //}

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

                            $scope.LicenseTypes[10].Licenses.push({ LicenseID: Docs[i].EducationDetailID, LicenseName: "UG" + ($scope.countUG + 1), LicenseDocPath: Docs[i].CertificatePath, ModifiedDate: Docs[i].LastModifiedDate, Description: "Under Graduate" });
                            $scope.countUG++;

                            if (Docs[i].EducationDetailHistory != null) {

                                $scope.LicenseTypes[10].LicenseHistories.push({ LicenseID: Docs[i].EducationDetailHistory.EducationDetailHistoryID, LicenseName: "UG" + ($scope.countUG + 1), LicenseDocPath: Docs[i].EducationDetailHistory.CertificatePath, removeDate: Docs[i].EducationDetailHistory.LastModifiedDate });

                            }

                        } else if (Docs[i].QualificationType == "Graduate") {

                            $scope.LicenseTypes[10].Licenses.push({ LicenseID: Docs[i].EducationDetailID, LicenseName: "Grad" + ($scope.countG + 1), LicenseDocPath: Docs[i].CertificatePath, ModifiedDate: Docs[i].LastModifiedDate, Description: "Graduate" });
                            $scope.countG++;

                            if (Docs[i].EducationDetailHistory != null) {

                                $scope.LicenseTypes[10].LicenseHistories.push({ LicenseID: Docs[i].EducationDetailHistory.EducationDetailHistoryID, LicenseName: "Grad" + ($scope.countG + 1), LicenseDocPath: Docs[i].EducationDetailHistory.CertificatePath, removeDate: Docs[i].EducationDetailHistory.LastModifiedDate });

                            }

                        }

                    }

                }

            }

            //if ($scope.Licenses.length != 0) {

            //$scope.LicenseTypes.push({ LicenseTypeID: 11, LicenseTypeName: "Education Document", Licenses: $scope.Licenses, LicenseHistories: $scope.LicenseHistories });

            //}

        }

        $scope.FormatECFMGDetailDoc = function (Docs) {

            $scope.Licenses = [];
            $scope.LicenseHistories = [];

            if (Docs.ECFMGCertPath != null) {

                $scope.LicenseTypes[11].Licenses.push({ LicenseID: Docs.ECFMGDetailID, LicenseName: "ECFMG", LicenseDocPath: Docs.ECFMGCertPath, ModifiedDate: Docs.LastModifiedDate, Description: "Education Commission for Foreign Medical Graduates" });

                $scope.LicenseTypes[11].LicenseHistories.push({ LicenseID: 2, LicenseName: "ECFMG", LicenseDocPath: Docs.ECFMGCertPath, removeDate: Docs.LastModifiedDate });

            }

            //$scope.LicenseTypes.push({ LicenseTypeID: 12, LicenseTypeName: "ECFMG Document", Licenses: $scope.Licenses, LicenseHistories: $scope.LicenseHistories });

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

                        $scope.LicenseTypes[12].Licenses.push({ LicenseID: Docs[i].ProgramDetailID, LicenseName: "Intern" + ($scope.countIntern + 1), LicenseDocPath: Docs[i].DocumentPath, ModifiedDate: Docs[i].LastModifiedDate, Description: "Internship" });

                        $scope.countIntern++;

                        if (Docs.ProgramDetailHistory != null) {

                            $scope.LicenseTypes[12].LicenseHistories.push({ LicenseID: Docs[i].ProgramDetailHistory.ProgramDetailHistoryID, LicenseName: "Intern" + ($scope.countIntern + 1), LicenseDocPath: Docs[i].ProgramDetailHistory.DocumentPath, removeDate: Docs[i].ProgramDetailHistory.LastModifiedDate });

                        }

                    } else if (Docs[i].ProgramType == "Fellowship") {

                        $scope.LicenseTypes[12].Licenses.push({ LicenseID: Docs[i].ProgramDetailID, LicenseName: "Fell" + ($scope.countFell + 1), LicenseDocPath: Docs[i].DocumentPath, ModifiedDate: Docs[i].LastModifiedDate, Description: "Fellowship" });

                        $scope.countFell++;

                        if (Docs.ProgramDetailHistory != null) {

                            $scope.LicenseTypes[12].LicenseHistories.push({ LicenseID: Docs[i].ProgramDetailHistory.ProgramDetailHistoryID, LicenseName: "Fell" + ($scope.countFell + 1), LicenseDocPath: Docs[i].ProgramDetailHistory.DocumentPath, removeDate: Docs[i].ProgramDetailHistory.LastModifiedDate });

                        }

                    } else if (Docs[i].ProgramType == "Resident") {

                        $scope.LicenseTypes[12].Licenses.push({ LicenseID: Docs[i].ProgramDetailID, LicenseName: "Res" + ($scope.countRes + 1), LicenseDocPath: Docs[i].DocumentPath, ModifiedDate: Docs[i].LastModifiedDate, Description: "Resident" });

                        $scope.countRes++;

                        if (Docs.ProgramDetailHistory != null) {

                            $scope.LicenseTypes[12].LicenseHistories.push({ LicenseID: Docs[i].ProgramDetailHistory.ProgramDetailHistoryID, LicenseName: "Res" + ($scope.countRes + 1), LicenseDocPath: Docs[i].ProgramDetailHistory.DocumentPath, removeDate: Docs[i].ProgramDetailHistory.LastModifiedDate });

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

            //if ($scope.Licenses.length != 0) {

            //$scope.LicenseTypes.push({ LicenseTypeID: 13, LicenseTypeName: "Program Document", Licenses: $scope.Licenses, LicenseHistories: $scope.LicenseHistories });

            //}

        }

        $scope.FormatCMECertificationsDoc = function (Docs) {

            $scope.Licenses = [];
            $scope.LicenseHistories = [];

            for (var i = 0; i < Docs.length; i++) {

                if (Docs[i].CertificatePath != null) {

                    $scope.LicenseTypes[13].Licenses.push({ LicenseID: Docs[i].CMECertificationID, LicenseName: "CME" + (i + 1), LicenseDocPath: Docs[i].CertificatePath, ModifiedDate: Docs[i].LastModifiedDate, Description: "Continuing Medical Education" });

                    if (Docs[i].CMECertificationHistory != null) {

                        $scope.LicenseTypes[13].LicenseHistories.push({ LicenseID: Docs[i].CMECertificationHistory.CMECertificationHistoryID, LicenseName: "CME" + (j + 1), LicenseDocPath: Docs[i].CMECertificationHistory.CertificatePath, removeDate: Docs[i].CMECertificationHistory.LastModifiedDate });

                    }

                }

            }

            //if ($scope.Licenses.length != 0) {

            //$scope.LicenseTypes.push({ LicenseTypeID: 14, LicenseTypeName: "CME Certifications", Licenses: $scope.Licenses, LicenseHistories: $scope.LicenseHistories });

            //}

        }

        $scope.FormatSpecialtyBoardCertifiedDetailDoc = function (Docs) {

            $scope.Licenses = [];
            $scope.LicenseHistories = [];

            for (var i = 0; i < Docs.length; i++) {

                if (Docs[i].SpecialtyBoardCertifiedDetail != null && Docs[i].SpecialtyBoardCertifiedDetail.BoardCertificatePath != null) {

                    $scope.LicenseTypes[14].Licenses.push({ LicenseID: Docs[i].SpecialtyDetailID, LicenseName: "Board" + (i + 1), LicenseDocPath: Docs[i].SpecialtyBoardCertifiedDetail.BoardCertificatePath, ModifiedDate: Docs[i].LastModifiedDate, Description: "Specialty Board Certificate" });

                    if (Docs[i].SpecialtyBoardCertifiedDetail.SpecialtyBoardCertifiedDetailHistory != null) {

                        for (var j = 0; j < Docs[i].SpecialtyBoardCertifiedDetail.SpecialtyBoardCertifiedDetailHistory.length; j++) {

                            $scope.LicenseTypes[14].LicenseHistories.push({ LicenseID: Docs[i].SpecialtyBoardCertifiedDetail.SpecialtyBoardCertifiedDetailHistory[j].SpecialtyBoardCertifiedDetailHistoryID, LicenseName: "Board" + (j + 1), LicenseDocPath: Docs[i].SpecialtyBoardCertifiedDetail.SpecialtyBoardCertifiedDetailHistory[j].BoardCertificatePath, removeDate: Docs[i].SpecialtyBoardCertifiedDetail.SpecialtyBoardCertifiedDetailHistory[j].LastModifiedDate });

                        }

                    }

                }

            }

            //if ($scope.Licenses.length != 0) {

            //$scope.LicenseTypes.push({ LicenseTypeID: 15, LicenseTypeName: "Specialty Board Certificates", Licenses: $scope.Licenses, LicenseHistories: $scope.LicenseHistories });

            //}

        }

        $scope.FormatHospitalPrivilegeDetailDoc = function (Docs) {

            $scope.Licenses = [];
            $scope.LicenseHistories = [];
            var count = 1;

            for (var i = 0; i < Docs.length; i++) {

                if (Docs[i].HospitalPrevilegeLetterPath != null && Docs[i].Status != "Inactive") {

                    $scope.LicenseTypes[15].Licenses.push({ LicenseID: Docs[i].HospitalPrivilegeDetailID, LicenseName: "HPL" + (count), LicenseDocPath: Docs[i].HospitalPrevilegeLetterPath, ModifiedDate: Docs[i].LastModifiedDate, Description: "Hospital Privilege Document" });

                    if (Docs[i].HospitalPrivilegeDetailHistory != null) {

                        for (var j = 0; j < Docs[i].HospitalPrivilegeDetailHistory.length; j++) {

                            $scope.LicenseTypes[15].LicenseHistories.push({ LicenseID: Docs[i].HospitalPrivilegeDetailHistory[j].HospitalPrivilegeDetailHistoryID, LicenseName: "HPL" + (j + 1), LicenseDocPath: Docs[i].HospitalPrivilegeDetailHistory[j].HospitalPrevilegeLetterPath, removeDate: Docs[i].HospitalPrivilegeDetailHistory[j].LastModifiedDate });

                        }

                    }

                    count++;

                }

            }

            //if ($scope.Licenses.length != 0) {

            //$scope.LicenseTypes.push({ LicenseTypeID: 16, LicenseTypeName: "Hospital Privilege Document", Licenses: $scope.Licenses, LicenseHistories: $scope.LicenseHistories });

            //}

        }

        $scope.FormatProfessionalLiabilityInfoDoc = function (Docs) {

            $scope.Licenses = [];
            $scope.LicenseHistories = [];

            for (var i = 0; i < Docs.length; i++) {

                if (Docs[i].InsuranceCertificatePath != null) {

                    $scope.LicenseTypes[16].Licenses.push({ LicenseID: Docs[i].ProfessionalLiabilityInfoID, LicenseName: "LIC" + (i + 1), LicenseDocPath: Docs[i].InsuranceCertificatePath, ModifiedDate: Docs[i].LastModifiedDate, Description: "Professional Liability Document" });

                    if (Docs[i].ProfessionalLiabilityInfoHistory != null) {

                        for (var j = 0; j < Docs[i].ProfessionalLiabilityInfoHistory.length; j++) {

                            $scope.LicenseTypes[16].LicenseHistories.push({ LicenseID: Docs[i].ProfessionalLiabilityInfoHistory[j].ProfessionalLiabilityInfoHistoryID, LicenseName: "LIC" + (j + 1), LicenseDocPath: Docs[i].ProfessionalLiabilityInfoHistory[j].InsuranceCertificatePath, removeDate: Docs[i].ProfessionalLiabilityInfoHistory[j].LastModifiedDate });

                        }

                    }

                }

            }

            //if ($scope.Licenses.length != 0) {

            //$scope.LicenseTypes.push({ LicenseTypeID: 17, LicenseTypeName: "Professional Liability Document", Licenses: $scope.Licenses, LicenseHistories: $scope.LicenseHistories });

            //}

        }

        $scope.FormatProfessionalWorkExperienceDoc = function (Docs) {

            $scope.Licenses = [];
            $scope.LicenseHistories = [];

            for (var i = 0; i < Docs.length; i++) {

                if (Docs[i].WorkExperienceDocPath != null) {

                    $scope.LicenseTypes[17].Licenses.push({ LicenseID: Docs[i].ProfessionalWorkExperienceID, LicenseName: "Work Exp" + (i + 1), LicenseDocPath: Docs[i].WorkExperienceDocPath, ModifiedDate: Docs[i].LastModifiedDate, Description: "Work Experience Document" });

                    if (Docs[i].ProfessionalWorkExperienceHistory != null) {

                        $scope.LicenseTypes[17].LicenseHistories.push({ LicenseID: Docs[i].ProfessionalWorkExperienceHistory.ProfessionalWorkExperienceHistoryID, LicenseName: "Work Exp" + (j + 1), LicenseDocPath: Docs[i].ProfessionalWorkExperienceHistory.WorkExperienceDocPath, removeDate: Docs[i].ProfessionalWorkExperienceHistory.LastModifiedDate });

                    }

                }

            }

            //if ($scope.Licenses.length != 0) {

            //$scope.LicenseTypes.push({ LicenseTypeID: 18, LicenseTypeName: "WorkExperience Document", Licenses: $scope.Licenses, LicenseHistories: $scope.LicenseHistories });

            //}

        }

        $scope.FormatContractInfoeDoc = function (Docs) {

            $scope.Licenses = [];
            $scope.LicenseHistories = [];

            for (var i = 0; i < Docs.length; i++) {

                if (Docs[i].ContractFilePath != null) {

                    $scope.LicenseTypes[18].Licenses.push({ LicenseID: Docs[i].ContractInfoID, LicenseName: "Contract Document", LicenseDocPath: Docs[i].ContractFilePath, ModifiedDate: Docs[i].LastModifiedDate, Description: "Contract Document" });

                }

            }

            //if ($scope.Licenses.length != 0) {

            //$scope.LicenseTypes.push({ LicenseTypeID: 19, LicenseTypeName: "Contract", Licenses: $scope.Licenses, LicenseHistories: $scope.LicenseHistories });

            //}

        }

        $scope.FormatOtherDocument = function (Docs) {

            $scope.Licenses = [];
            $scope.LicenseHistories = [];

            for (var i = 0; i < Docs.length; i++) {

                if (Docs[i].DocumentPath != null) {

                    $scope.LicenseTypes[19].Licenses.push({ LicenseID: Docs[i].OtherDocumentID, LicenseName: Docs[i].Title, LicenseDocPath: Docs[i].DocumentPath, ModifiedDate: Docs[i].LastModifiedDate, Private: Docs[i].IsPrivate, Description: "Other Document" });

                }

            }

            //if ($scope.Licenses.length != 0) {

            //$scope.LicenseTypes.push({ LicenseTypeID: 20, LicenseTypeName: "Other", Licenses: $scope.Licenses, LicenseHistories: $scope.LicenseHistories });

            //}

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
            $scope.FilePath = "";
            $scope.isError = false;
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
                FilePath: licenseType.LicenseDocPath,
                IsPrivate: license.Private,
                Document: {}
            };
            $scope.isError = false;
            $scope.IsAddNewDocument = true;
            $scope.isAdd = false;
            $scope.isUpdate = true;
        };

        $scope.IsAddNewDocument = false;

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
            $scope.isError = false;
            //var status = false;

            //status = $scope.check($scope.TempObject.Title);

            if ($form.valid()) {
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

                            $scope.message = "Please Select the Document";
                            $scope.isError = true;

                            //messageAlertEngine.callAlertMessage("alertOtherDocumentError", data.status.split(","), "danger", true);
                        }
                    }
                });
            } else {

                //if ($scope.TempObject.Title != "") {
                //    if ($scope.FilePath != "") {
                //        $scope.IsAddNewDocument = false;
                //        $("#ATTACHmore").hide();
                //        $scope.IsFail = true;
                //        $scope.FailureMessage = "Document Already Present";
                //        $timeout(function () {
                //            $scope.IsFail = false;
                //        }, 5000);
                //    }
                //    $scope.FilePath = "";
                //}

            }

        };

        $scope.resetForm = function () {
            $scope.DocumentForm.$setPristine();
        };

        //$scope.check = function (title) {

        //    var count = 0;
        //    var status = false;

        //    for (var i = 0; i < $scope.spaDoc.length; i++) {

        //        if ($scope.spaDoc[i].LicenseName == title) {

        //            count = 1

        //        }
        //        if (count == 1) {

        //            status = true;

        //        }

        //    }

        //    return status;

        //}

        $scope.UpdateDocumentArray = function (obj) {

            $scope.spaDoc.push({ LicenseID: obj.OtherDocumentID, LicenseName: obj.Title, LicenseDocPath: obj.DocumentPath, Status: false });

        }

        $scope.updateOtherDocument = function (Form_Div_Id) {
            var $form = $("#" + Form_Div_Id).find("form");
            $scope.isError = false;
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
            $scope.isError = false;
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

        $scope.cancelAdd = function () {
            $scope.IsAddNewDocument = false;
            $scope.isError = false;
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

                //for (var i = 0; i < $scope.LicenseTypes.length; i++) {

                //    if ($scope.LicenseTypes[i].LicenseTypeID == 3) {

                for (var j = 0; j < $scope.LicenseTypes[2].Licenses.length; j++) {

                    if ($scope.LicenseTypes[2].Licenses[j].LicenseID == data.personalIdentification.PersonalIdentificationID) {

                        if ($scope.LicenseTypes[2].Licenses[j].LicenseName == "SSN") {

                            $scope.LicenseTypes[2].Licenses[j].LicenseDocPath = data.personalIdentification.SSNCertificatePath;

                        } else if ($scope.LicenseTypes[2].Licenses[j].LicenseName == "DL") {

                            $scope.LicenseTypes[2].Licenses[j].LicenseDocPath = data.personalIdentification.DLCertificatePath;

                        }

                        count02++;

                    }

                }

                if (count02 == 0) {

                    if (data.personalIdentification.SSNCertificatePath != null) {

                        $scope.LicenseTypes[2].Licenses.push({ LicenseID: data.personalIdentification.PersonalIdentificationID, LicenseName: "SSN", LicenseDocPath: data.personalIdentification.SSNCertificatePath, ModifiedDate: $scope.ConvertDateFormat(data.personalIdentification.LastModifiedDate), Description: "Social Security Number" });

                    }

                    if (data.personalIdentification.DLCertificatePath != null) {

                        $scope.LicenseTypes[2].Licenses.push({ LicenseID: data.personalIdentification.PersonalIdentificationID, LicenseName: "DL", LicenseDocPath: data.personalIdentification.DLCertificatePath, ModifiedDate: $scope.ConvertDateFormat(data.personalIdentification.LastModifiedDate), Description: "Driver License" });

                    }

                }

                count01++;

                //    }

                //}
                $scope.SelectedLicense = {};
                //if (count01 == 0 && $scope.LicenseTypes.length != 0) {

                //    $scope.FormatPersonalIdentificationDoc(data.personalIdentification);

                //}

            }

        });
        $scope.$on('UpdateStateLicenses', function (event, data) {

            var count01 = 0;
            var count02 = 0;

            if (data.stateLicense != null) {

                //for (var i = 0; i < $scope.LicenseTypes.length; i++) {

                //    if ($scope.LicenseTypes[i].LicenseTypeID == 1) {

                for (var j = 0; j < $scope.LicenseTypes[0].Licenses.length; j++) {

                    if ($scope.LicenseTypes[0].Licenses[j].LicenseID == data.stateLicense.StateLicenseInformationID) {

                        $scope.LicenseTypes[0].Licenses[j].LicenseDocPath = data.stateLicense.StateLicenseDocumentPath;

                        count02++;

                    }

                }

                if (count02 == 0) {

                    if (data.stateLicense.StateLicenseDocumentPath != null) {

                        $scope.LicenseTypes[0].Licenses.push({ LicenseID: data.stateLicense.StateLicenseInformationID, LicenseName: "ML" + ($scope.LicenseTypes[0].Licenses.length + 1), LicenseDocPath: data.stateLicense.StateLicenseDocumentPath, ModifiedDate: $scope.ConvertDateFormat(data.stateLicense.LastModifiedDate), Description: "State License" });

                    }

                }

                count01++;

                //    }

                //}
                $scope.SelectedLicense = {};
                //if (count01 == 0 && $scope.LicenseTypes.length != 0) {

                //    $scope.FormatStateLicense([data.stateLicense]);

                //}

            }

        });
        $scope.$on('UpdateOtherLegalNames', function (event, data) {

            var count01 = 0;
            var count02 = 0;

            if (data.otherLegalName != null) {

                //for (var i = 0; i < $scope.LicenseTypes.length; i++) {

                //    if ($scope.LicenseTypes[i].LicenseTypeID == 2) {

                for (var j = 0; j < $scope.LicenseTypes[1].Licenses.length; j++) {

                    if ($scope.LicenseTypes[1].Licenses[j].LicenseID == data.otherLegalName.OtherLegalNameID) {

                        $scope.LicenseTypes[1].Licenses[j].LicenseDocPath = data.otherLegalName.DocumentPath;

                        count02++;

                    }

                }

                if (count02 == 0) {

                    if (data.otherLegalName.DocumentPath != null) {

                        $scope.LicenseTypes[1].Licenses.push({ LicenseID: data.otherLegalName.OtherLegalNameID, LicenseName: "OLN" + ($scope.LicenseTypes[1].Licenses.length + 1), LicenseDocPath: data.otherLegalName.DocumentPath, ModifiedDate: $scope.ConvertDateFormat(data.otherLegalName.LastModifiedDate), Description: "Other Legal Name" });

                    }

                }

                count01++;

                //    }

                //}
                $scope.SelectedLicense = {};
                //if ($scope.LicenseTypes.length != 0) {

                //    $scope.FormatOtherLegalNamesDoc([data.otherLegalName]);

                //}

            }

        });
        $scope.$on('UpdateBirthInformation', function (event, data) {

            var count01 = 0;
            var count02 = 0;

            if (data.birthInformation != null) {

                //for (var i = 0; i < $scope.LicenseTypes.length; i++) {

                //    if ($scope.LicenseTypes[i].LicenseTypeID == 4) {

                for (var j = 0; j < $scope.LicenseTypes[3].Licenses.length; j++) {

                    if ($scope.LicenseTypes[3].Licenses[j].LicenseID == data.birthInformation.BirthInformationID) {

                        $scope.LicenseTypes[3].Licenses[j].LicenseDocPath = data.birthInformation.BirthCertificatePath;

                        count02++;

                    }

                }

                if (count02 == 0) {

                    if (data.birthInformation.BirthCertificatePath != null) {

                        $scope.LicenseTypes[3].Licenses.push({ LicenseID: data.birthInformation.BirthInformationID, LicenseName: "Birth Certificate", LicenseDocPath: data.birthInformation.BirthCertificatePath, ModifiedDate: $scope.ConvertDateFormat(data.birthInformation.LastModifiedDate), Description: "Birth Certificate" });

                    }

                }

                count01++;

                //    }

                //}
                $scope.SelectedLicense = {};
                //if (count01 == 0 && $scope.LicenseTypes.length != 0) {

                //    $scope.FormatBirthInformationDoc(data.birthInformation);

                //}

            }

        });
        $scope.$on('UpdateVisaInfo', function (event, data) {

            var count01 = 0;
            var count02 = 0;

            if (data.visaDetail != null && data.visaDetail.VisaInfo != null) {

                //for (var i = 0; i < $scope.LicenseTypes.length; i++) {

                //    if ($scope.LicenseTypes[i].LicenseTypeID == 6) {

                for (var j = 0; j < $scope.LicenseTypes[5].Licenses.length; j++) {

                    if ($scope.LicenseTypes[5].Licenses[j].LicenseID == data.visaDetail.VisaInfo.VisaInfoID) {

                        if ($scope.LicenseTypes[5].Licenses[j].LicenseName == "VISA") {

                            $scope.LicenseTypes[5].Licenses[j].LicenseDocPath = data.visaDetail.VisaInfo.VisaCertificatePath;

                        } else if ($scope.LicenseTypes[5].Licenses[j].LicenseName == "GreenCard") {

                            $scope.LicenseTypes[5].Licenses[j].LicenseDocPath = data.visaDetail.VisaInfo.GreenCardCertificatePath;

                        } else if ($scope.LicenseTypes[5].Licenses[j].LicenseName == "NationalIdentification") {

                            $scope.LicenseTypes[5].Licenses[j].LicenseDocPath = data.visaDetail.VisaInfo.NationalIDCertificatePath;

                        }

                        count02++;

                    }

                }

                if (count02 == 0) {

                    if (data.visaDetail.VisaInfo != null) {

                        if (data.visaDetail.VisaInfo.VisaCertificatePath != null) {

                            $scope.LicenseTypes[5].Licenses.push({ LicenseID: data.visaDetail.VisaInfo.VisaInfoID, LicenseName: "VISA", LicenseDocPath: data.visaDetail.VisaInfo.VisaCertificatePath, ModifiedDate: $scope.ConvertDateFormat(data.visaDetail.VisaInfo.LastModifiedDate), Description: "Visa" });

                        }

                        if (data.visaDetail.VisaInfo.GreenCardCertificatePath != null) {

                            $scope.LicenseTypes[5].Licenses.push({ LicenseID: data.visaDetail.VisaInfo.VisaInfoID, LicenseName: "GreenCard", LicenseDocPath: data.visaDetail.VisaInfo.GreenCardCertificatePath, ModifiedDate: $scope.ConvertDateFormat(data.visaDetail.VisaInfo.LastModifiedDate), Description: "Green Card" });

                        }

                        if (data.visaDetail.VisaInfo.NationalIDCertificatePath != null) {

                            $scope.LicenseTypes[5].Licenses.push({ LicenseID: data.visaDetail.VisaInfo.VisaInfoID, LicenseName: "National Identification", LicenseDocPath: data.visaDetail.VisaInfo.NationalIDCertificatePath, ModifiedDate: $scope.ConvertDateFormat(data.visaDetail.VisaInfo.LastModifiedDate), Description: "National Identification Certificate" });

                        }

                    }

                }

                count01++;

                //    }

                //}
                $scope.SelectedLicense = {};
                //if (count01 == 0 && $scope.LicenseTypes.length != 0) {

                //    $scope.FormatVisaInfoDoc(data.visaDetail);

                //}

            }

        });
        $scope.$on('UpdateFederalDEAInformation', function (event, data) {

            var count01 = 0;
            var count02 = 0;

            if (data.federalDea != null) {

                //for (var i = 0; i < $scope.LicenseTypes.length; i++) {

                //    if ($scope.LicenseTypes[i].LicenseTypeID == 7) {

                for (var j = 0; j < $scope.LicenseTypes[6].Licenses.length; j++) {

                    if ($scope.LicenseTypes[6].Licenses[j].LicenseID == data.federalDea.FederalDEAInformationID) {

                        $scope.LicenseTypes[6].Licenses[j].LicenseDocPath = data.federalDea.DEALicenceCertPath;

                        count02++;

                    }

                }

                if (count02 == 0) {

                    if (data.federalDea.DEALicenceCertPath != null) {

                        $scope.LicenseTypes[6].Licenses.push({ LicenseID: data.federalDea.FederalDEAInformationID, LicenseName: "DEA" + ($scope.LicenseTypes[6].Licenses.length + 1), LicenseDocPath: data.federalDea.DEALicenceCertPath, ModifiedDate: $scope.ConvertDateFormat(data.federalDea.LastModifiedDate), Description: "Drug Enforcement Administration" });

                    }

                }

                count01++;

                //    }

                //}
                $scope.SelectedLicense = {};
                //if (count01 == 0 && $scope.LicenseTypes.length != 0) {

                //    $scope.FormatFederalDEAInformationDoc([data.federalDea]);

                //}

            }

        });
        $scope.$on('UpdateMedicareInformation', function (event, data) {

            var count01 = 0;
            var count02 = 0;

            if (data.MedicareInformation != null) {

                //for (var i = 0; i < $scope.LicenseTypes.length; i++) {

                //    if ($scope.LicenseTypes[i].LicenseTypeID == 8) {

                for (var j = 0; j < $scope.LicenseTypes[7].Licenses.length; j++) {

                    if ($scope.LicenseTypes[7].Licenses[j].LicenseID == data.MedicareInformation.MedicareInformationID) {

                        $scope.LicenseTypes[7].Licenses[j].LicenseDocPath = data.MedicareInformation.CertificatePath;

                        count02++;

                    }

                }

                if (count02 == 0) {

                    if (data.MedicareInformation.CertificatePath != null) {

                        $scope.LicenseTypes[7].Licenses.push({ LicenseID: data.MedicareInformation.MedicareInformationID, LicenseName: "Medicare" + ($scope.LicenseTypes[7].Licenses.length + 1), LicenseDocPath: data.MedicareInformation.CertificatePath, ModifiedDate: $scope.ConvertDateFormat(data.MedicareInformation.LastModifiedDate), Description: "Medicare" });

                    }

                }

                count01++;

                //    }

                //}
                $scope.SelectedLicense = {};
                //if (count01 == 0 && $scope.LicenseTypes.length != 0) {

                //    $scope.FormatMedicareInformationDoc([data.MedicareInformation]);

                //}

            }

        });
        $scope.$on('UpdateMedicaidInformation', function (event, data) {

            var count01 = 0;
            var count02 = 0;

            if (data.MedicaidInformation != null) {

                //for (var i = 0; i < $scope.LicenseTypes.length; i++) {

                //    if ($scope.LicenseTypes[i].LicenseTypeID == 9) {

                for (var j = 0; j < $scope.LicenseTypes[8].Licenses.length; j++) {

                    if ($scope.LicenseTypes[8].Licenses[j].LicenseID == data.MedicaidInformation.MedicaidInformationID) {

                        $scope.LicenseTypes[8].Licenses[j].LicenseDocPath = data.MedicaidInformation.CertificatePath;

                        count02++;

                    }

                }

                if (count02 == 0) {

                    if (data.MedicaidInformation.CertificatePath != null) {

                        $scope.LicenseTypes[8].Licenses.push({ LicenseID: data.MedicaidInformation.MedicaidInformationID, LicenseName: "Medicaid" + ($scope.LicenseTypes[8].Licenses.length + 1), LicenseDocPath: data.MedicaidInformation.CertificatePath, ModifiedDate: $scope.ConvertDateFormat(data.MedicaidInformation.LastModifiedDate), Description: "Medicaid" });

                    }

                }

                count01++;

                //    }

                //}
                $scope.SelectedLicense = {};
                //if (count01 == 0 && $scope.LicenseTypes.length != 0) {

                //    $scope.FormatMedicaidInformationDoc([data.MedicaidInformation]);

                //}

            }

        });
        $scope.$on('UpdateCDSCInformation', function (event, data) {

            var count01 = 0;
            var count02 = 0;

            if (data.CDSCInformation != null) {

                //for (var i = 0; i < $scope.LicenseTypes.length; i++) {

                //    if ($scope.LicenseTypes[i].LicenseTypeID == 10) {

                for (var j = 0; j < $scope.LicenseTypes[9].Licenses.length; j++) {

                    if ($scope.LicenseTypes[9].Licenses[j].LicenseID == data.CDSCInformation.CDSCInformationID) {

                        $scope.LicenseTypes[9].Licenses[j].LicenseDocPath = data.CDSCInformation.CDSCCerificatePath;

                        count02++;

                    }

                }

                if (count02 == 0) {

                    if (data.CDSCInformation.CDSCCerificatePath != null) {

                        $scope.LicenseTypes[9].Licenses.push({ LicenseID: data.CDSCInformation.CDSCInformationID, LicenseName: "CDS" + ($scope.LicenseTypes[9].Licenses.length + 1), LicenseDocPath: data.CDSCInformation.CDSCCerificatePath, ModifiedDate: $scope.ConvertDateFormat(data.CDSCInformation.LastModifiedDate), Description: "Central Drug Standard Control" });

                    }

                }

                count01++;

                //    }

                //}
                $scope.SelectedLicense = {};
                //if (count01 == 0 && $scope.LicenseTypes.length != 0) {

                //    $scope.FormatCDSCInformationDoc([data.CDSCInformation]);

                //}

            }

        });
        $scope.$on('UpdateEducationDetailDoc', function (event, data) {

            var count01 = 0;
            var count02 = 0;

            if (data.educationDetails != null) {

                //for (var i = 0; i < $scope.LicenseTypes.length; i++) {

                //    if ($scope.LicenseTypes[i].LicenseTypeID == 11) {

                for (var j = 0; j < $scope.LicenseTypes[10].Licenses.length; j++) {

                    if ($scope.LicenseTypes[10].Licenses[j].LicenseID == data.educationDetails.EducationDetailID) {

                        $scope.LicenseTypes[10].Licenses[j].LicenseDocPath = data.educationDetails.CertificatePath;

                        count02++;

                    }

                }

                if (count02 == 0) {

                    if (data.educationDetails.CertificatePath != null) {

                        if (data.educationDetails.QualificationType == "UnderGraduate") {

                            $scope.LicenseTypes[10].Licenses.push({ LicenseID: data.educationDetails.EducationDetailID, LicenseName: "UG" + ($scope.countUG + 1), LicenseDocPath: data.educationDetails.CertificatePath, ModifiedDate: $scope.ConvertDateFormat(data.educationDetails.LastModifiedDate), Description: "Under Graduate" });

                            $scope.countUG++;

                        } else if (data.educationDetails.QualificationType == "Graduate") {

                            $scope.LicenseTypes[10].Licenses.push({ LicenseID: data.educationDetails.EducationDetailID, LicenseName: "Grad" + ($scope.countG + 1), LicenseDocPath: data.educationDetails.CertificatePath, ModifiedDate: $scope.ConvertDateFormat(data.educationDetails.LastModifiedDate), Description: "Graduate" });

                            $scope.countG++;

                        }

                    }

                }

                count01++;

                //    }

                //}
                $scope.SelectedLicense = {};
                //if (count01 == 0 && $scope.LicenseTypes.length != 0) {

                //    $scope.FormatEducationDetailDoc([data.educationDetails]);

                //}

            }

        });
        $scope.$on('UpdateECFMGDetailDoc', function (event, data) {

            var count01 = 0;
            var count02 = 0;

            if (data.ecfmgDetails != null) {

                //for (var i = 0; i < $scope.LicenseTypes.length; i++) {

                //    if ($scope.LicenseTypes[i].LicenseTypeID == 12) {

                for (var j = 0; j < $scope.LicenseTypes[11].Licenses.length; j++) {

                    if ($scope.LicenseTypes[11].Licenses[j].LicenseID == data.ecfmgDetails.ECFMGDetailID) {

                        $scope.LicenseTypes[11].Licenses[j].LicenseDocPath = data.ecfmgDetails.ECFMGCertPath;

                        count02++;

                    }

                }

                if (count02 == 0) {

                    if (data.ecfmgDetails.ECFMGCertPath != null) {

                        $scope.LicenseTypes[11].Licenses.push({ LicenseID: data.ecfmgDetails.ECFMGDetailID, LicenseName: "ECFMG", LicenseDocPath: data.ecfmgDetails.ECFMGCertPath, ModifiedDate: $scope.ConvertDateFormat(data.ecfmgDetails.LastModifiedDate), Description: "Education Commission for Foreign Medical Graduates" });

                    }

                }

                count01++;

                //    }

                //}
                $scope.SelectedLicense = {};
                //if (count01 == 0 && $scope.LicenseTypes.length != 0) {

                //    $scope.FormatECFMGDetailDoc(data.ecfmgDetails);

                //}

            }

        });
        $scope.$on('UpdateProgramDetailsDoc', function (event, data) {

            var count01 = 0;
            var count02 = 0;

            if (data.programDetails != null) {

                //for (var i = 0; i < $scope.LicenseTypes.length; i++) {

                //    if ($scope.LicenseTypes[i].LicenseTypeID == 13) {

                for (var j = 0; j < $scope.LicenseTypes[12].Licenses.length; j++) {

                    if ($scope.LicenseTypes[12].Licenses[j].LicenseID == data.programDetails.ProgramDetailID) {

                        $scope.LicenseTypes[12].Licenses[j].LicenseDocPath = data.programDetails.DocumentPath;

                        count02++;

                    }

                }

                if (count02 == 0) {

                    if (data.programDetails.DocumentPath != null) {

                        if (data.programDetails.ProgramType == "Internship") {

                            $scope.LicenseTypes[12].Licenses.push({ LicenseID: data.programDetails.ProgramDetailID, LicenseName: "Intern" + ($scope.countIntern + 1), LicenseDocPath: data.programDetails.DocumentPath, ModifiedDate: $scope.ConvertDateFormat(data.programDetails.LastModifiedDate), Description: "Internship" });

                            $scope.countIntern++;

                        } else if (data.programDetails.ProgramType == "Fellowship") {

                            $scope.LicenseTypes[12].Licenses.push({ LicenseID: data.programDetails.ProgramDetailID, LicenseName: "Fell" + ($scope.countFell + 1), LicenseDocPath: data.programDetails.DocumentPath, ModifiedDate: $scope.ConvertDateFormat(data.programDetails.LastModifiedDate), Description: "Fellowship" });

                            $scope.countFell++;

                        } else if (data.programDetails.ProgramType == "Resident") {

                            $scope.LicenseTypes[12].Licenses.push({ LicenseID: data.programDetails.ProgramDetailID, LicenseName: "Res" + ($scope.countRes + 1), LicenseDocPath: data.programDetails.DocumentPath, ModifiedDate: $scope.ConvertDateFormat(data.programDetails.LastModifiedDate), Description: "Resident" });

                            $scope.countRes++;

                        } else if (data.programDetails.ProgramType == "Other") {

                            $scope.LicenseTypes[12].Licenses.push({ LicenseID: data.programDetails.ProgramDetailID, LicenseName: "Other" + ($scope.countOther + 1), LicenseDocPath: data.programDetails.DocumentPath, ModifiedDate: $scope.ConvertDateFormat(data.programDetails.LastModifiedDate), Description: "Other" });

                            $scope.countOther++;

                        }

                    }

                }

                count01++;

                //    }

                //}
                $scope.SelectedLicense = {};
                //if (count01 == 0 && $scope.LicenseTypes.length != 0) {

                //    $scope.FormatProgramDetailsDoc([data.programDetails]);

                //}

            }

        });
        $scope.$on('UpdateCMECertificationsDoc', function (event, data) {

            var count01 = 0;
            var count02 = 0;

            if (data.CMEDetails != null) {

                //for (var i = 0; i < $scope.LicenseTypes.length; i++) {

                //    if ($scope.LicenseTypes[i].LicenseTypeID == 14) {

                for (var j = 0; j < $scope.LicenseTypes[13].Licenses.length; j++) {

                    if ($scope.LicenseTypes[13].Licenses[j].LicenseID == data.CMEDetails.CMECertificationID) {

                        $scope.LicenseTypes[13].Licenses[j].LicenseDocPath = data.CMEDetails.CertificatePath;

                        count02++;

                    }

                }

                if (count02 == 0) {

                    if (data.CMEDetails.CertificatePath != null) {

                        $scope.LicenseTypes[13].Licenses.push({ LicenseID: data.CMEDetails.CMECertificationID, LicenseName: "CME" + ($scope.LicenseTypes[13].Licenses.length + 1), LicenseDocPath: data.CMEDetails.CertificatePath, ModifiedDate: $scope.ConvertDateFormat(data.CMEDetails.LastModifiedDate), Description: "Continuing Medical Education" });

                    }

                }

                count01++;

                //    }

                //}
                $scope.SelectedLicense = {};
                //if (count01 == 0 && $scope.LicenseTypes.length != 0) {

                //    $scope.FormatCMECertificationsDoc([data.CMEDetails]);

                //}

            }

        });
        $scope.$on('UpdateSpecialtyBoardCertifiedDetailDoc', function (event, data) {

            var count01 = 0;
            var count02 = 0;

            if (data.specialty != null && data.specialty.SpecialtyBoardCertifiedDetail != null) {

                //for (var i = 0; i < $scope.LicenseTypes.length; i++) {

                //    if ($scope.LicenseTypes[i].LicenseTypeID == 15) {

                for (var j = 0; j < $scope.LicenseTypes[14].Licenses.length; j++) {

                    if ($scope.LicenseTypes[14].Licenses[j].LicenseID == data.specialty.SpecialtyDetailID) {

                        $scope.LicenseTypes[14].Licenses[j].LicenseDocPath = data.specialty.SpecialtyBoardCertifiedDetail.BoardCertificatePath;

                        count02++;

                    }

                }

                if (count02 == 0) {

                    if (data.specialty.SpecialtyBoardCertifiedDetail.BoardCertificatePath != null) {

                        $scope.LicenseTypes[14].Licenses.push({ LicenseID: data.specialty.SpecialtyBoardCertifiedDetail.SpecialtyBoardCertifiedDetailID, LicenseName: "Board" + ($scope.LicenseTypes[14].Licenses.length + 1), LicenseDocPath: data.specialty.SpecialtyBoardCertifiedDetail.BoardCertificatePath, ModifiedDate: $scope.ConvertDateFormat(data.specialty.SpecialtyBoardCertifiedDetail.LastModifiedDate), Description: "Specialty Board Certificate" });

                    }

                }

                count01++;

                //    }

                //}
                $scope.SelectedLicense = {};
                //if (count01 == 0 && $scope.LicenseTypes.length != 0) {

                //    $scope.FormatSpecialtyBoardCertifiedDetailDoc([data.specialty]);

                //}

            }

        });
        $scope.$on('UpdateHospitalPrivilegeDetailDoc', function (event, data) {

            var count01 = 0;
            var count02 = 0;

            if (data.hospitalPrivilegeDetail != null) {

                //for (var i = 0; i < $scope.LicenseTypes.length; i++) {

                //    if ($scope.LicenseTypes[i].LicenseTypeID == 16) {

                for (var j = 0; j < $scope.LicenseTypes[15].Licenses.length; j++) {

                    if ($scope.LicenseTypes[15].Licenses[j].LicenseID == data.hospitalPrivilegeDetail.HospitalPrivilegeDetailID) {

                        $scope.LicenseTypes[15].Licenses[j].LicenseDocPath = data.hospitalPrivilegeDetail.HospitalPrevilegeLetterPath;

                        count02++;

                    }

                }

                if (count02 == 0) {

                    if (data.hospitalPrivilegeDetail.HospitalPrevilegeLetterPath != null) {

                        $scope.LicenseTypes[15].Licenses.push({ LicenseID: data.hospitalPrivilegeDetail.HospitalPrivilegeDetailID, LicenseName: "HPL" + ($scope.LicenseTypes[15].Licenses.length + 1), LicenseDocPath: data.hospitalPrivilegeDetail.HospitalPrevilegeLetterPath, ModifiedDate: $scope.ConvertDateFormat(data.hospitalPrivilegeDetail.LastModifiedDate), Description: "Hospital Privilege Document" });

                    }

                }

                count01++;

                //    }

                //}
                $scope.SelectedLicense = {};
                //if (count01 == 0 && $scope.LicenseTypes.length != 0) {

                //    $scope.FormatHospitalPrivilegeDetailDoc([data.hospitalPrivilegeDetail]);

                //}

            }

        });
        $scope.$on('UpdateProfessionalLiabilityInfoDoc', function (event, data) {

            var count01 = 0;
            var count02 = 0;

            if (data.professionalLiability != null) {

                //for (var i = 0; i < $scope.LicenseTypes.length; i++) {

                //    if ($scope.LicenseTypes[i].LicenseTypeID == 17) {

                for (var j = 0; j < $scope.LicenseTypes[16].Licenses.length; j++) {

                    if ($scope.LicenseTypes[16].Licenses[j].LicenseID == data.professionalLiability.ProfessionalLiabilityInfoID) {

                        $scope.LicenseTypes[16].Licenses[j].LicenseDocPath = data.professionalLiability.InsuranceCertificatePath;

                        count02++;

                    }

                }

                if (count02 == 0) {

                    if (data.professionalLiability.InsuranceCertificatePath != null) {

                        $scope.LicenseTypes[16].Licenses.push({ LicenseID: data.professionalLiability.ProfessionalLiabilityInfoID, LicenseName: "LIC" + ($scope.LicenseTypes[16].Licenses.length + 1), LicenseDocPath: data.professionalLiability.InsuranceCertificatePath, ModifiedDate: $scope.ConvertDateFormat(data.professionalLiability.LastModifiedDate), Description: "Professional Liability Document" });

                    }

                }

                count01++;

                //    }

                //}
                $scope.SelectedLicense = {};
                //if (count01 == 0 && $scope.LicenseTypes.length != 0) {

                //    $scope.FormatProfessionalLiabilityInfoDoc([data.professionalLiability]);

                //}

            }

        });
        $scope.$on('UpdateProfessionalWorkExperienceDoc', function (event, data) {

            var count01 = 0;
            var count02 = 0;

            if (data.professionalWorkExperience != null) {

                //for (var i = 0; i < $scope.LicenseTypes.length; i++) {

                //    if ($scope.LicenseTypes[i].LicenseTypeID == 18) {

                for (var j = 0; j < $scope.LicenseTypes[17].Licenses.length; j++) {

                    if ($scope.LicenseTypes[17].Licenses[j].LicenseID == data.professionalWorkExperience.ProfessionalLiabilityInfoID) {

                        $scope.LicenseTypes[17].Licenses[j].LicenseDocPath = data.professionalWorkExperience.WorkExperienceDocPath;

                        count02++;

                    }

                }

                if (count02 == 0) {

                    if (data.professionalWorkExperience.WorkExperienceDocPath != null) {

                        $scope.LicenseTypes[17].Licenses.push({ LicenseID: data.professionalWorkExperience.ProfessionalLiabilityInfoID, LicenseName: "Work Exp" + ($scope.LicenseTypes[17].Licenses.length + 1), LicenseDocPath: data.professionalWorkExperience.WorkExperienceDocPath, ModifiedDate: $scope.ConvertDateFormat(data.professionalWorkExperience.LastModifiedDate), Description: "Work Experience Document" });

                    }

                }

                count01++;

                //    }

                //}
                $scope.SelectedLicense = {};
                //if (count01 == 0 && $scope.LicenseTypes.length != 0) {

                //    $scope.FormatProfessionalWorkExperienceDoc([data.professionalWorkExperience]);

                //}

            }

        });
        $scope.$on('UpdateContractInfoeDoc', function (event, data) {

            var count01 = 0;
            var count02 = 0;

            if (data.contractInformation != null) {

                //for (var i = 0; i < $scope.LicenseTypes.length; i++) {

                //    if ($scope.LicenseTypes[i].LicenseTypeID == 19) {

                for (var j = 0; j < $scope.LicenseTypes[18].Licenses.length; j++) {

                    if ($scope.LicenseTypes[18].Licenses[j].LicenseID == data.contractInformation.ContractInfoID) {

                        $scope.LicenseTypes[18].Licenses[j].LicenseDocPath = data.contractInformation.ContractFilePath;

                        count02++;

                    }

                }

                if (count02 == 0) {

                    if (data.contractInformation.ContractFilePath != null) {

                        $scope.LicenseTypes[18].Licenses.push({ LicenseID: data.contractInformation.ContractInfoID, LicenseName: "Contract Document", LicenseDocPath: data.contractInformation.ContractFilePath, ModifiedDate: $scope.ConvertDateFormat(data.contractInformation.LastModifiedDate), Description: "Contract Document" });

                    }

                }

                count01++;

                //    }

                //}
                $scope.SelectedLicense = {};
                //if (count01 == 0 && $scope.LicenseTypes.length != 0) {

                //    $scope.FormatContractInfoeDoc([data.contractInformation]);

                //}

            }

        });

        $scope.$on('RemoveStateLicenses', function (event, data) {

            var count = 0;

            if (data.stateLicense != null) {

                //for (var i = 0; i < $scope.LicenseTypes.length; i++) {

                //    if ($scope.LicenseTypes[i].LicenseTypeID == 1) {

                for (var j = 0; j < $scope.LicenseTypes[0].Licenses.length; j++) {

                    if ($scope.LicenseTypes[0].Licenses[j].LicenseID == data.stateLicense.StateLicenseInformationID) {

                        //if ($scope.LicenseTypes[i].Licenses.length == 1) {

                        //    $scope.LicenseTypes.splice($scope.LicenseTypes.indexOf($scope.LicenseTypes[i]), 1);

                        //} else {

                        $scope.LicenseTypes[0].Licenses.splice($scope.LicenseTypes[0].Licenses.indexOf($scope.LicenseTypes[0].Licenses[j]), 1);

                        count = 1;

                        //}

                    }
                    if (count == 1) {

                        $scope.LicenseTypes[0].Licenses[j].LicenseName = "ML" + (j + 1);

                    }

                }

                //    }

                //}

                $scope.SelectedLicense = {};

            }

        });
        $scope.$on('RemoveOtherLegalNames', function (event, data) {

            var count = 0;

            if (data.otherLegalName != null) {

                //for (var i = 0; i < $scope.LicenseTypes.length; i++) {

                //    if ($scope.LicenseTypes[i].LicenseTypeID == 2) {

                for (var j = 0; j < $scope.LicenseTypes[1].Licenses.length; j++) {

                    if ($scope.LicenseTypes[1].Licenses[j].LicenseID == data.otherLegalName.OtherLegalNameID) {

                        //if ($scope.LicenseTypes[i].Licenses.length == 1) {

                        //    $scope.LicenseTypes.splice($scope.LicenseTypes.indexOf($scope.LicenseTypes[i]), 1);

                        //} else {

                        $scope.LicenseTypes[1].Licenses.splice($scope.LicenseTypes[1].Licenses.indexOf($scope.LicenseTypes[1].Licenses[j]), 1);

                        count = 1;

                        //}

                    }

                    if (count == 1) {

                        $scope.LicenseTypes[1].Licenses[j].LicenseName = "OLN" + (j + 1);

                    }

                }

                //    }

                //}

                $scope.SelectedLicense = {};

            }

        });
        $scope.$on('RemoveFederalDEAInformation', function (event, data) {

            var count = 0;

            if (data.federalDea != null) {

                //for (var i = 0; i < $scope.LicenseTypes.length; i++) {

                //    if ($scope.LicenseTypes[i].LicenseTypeID == 7) {

                for (var j = 0; j < $scope.LicenseTypes[6].Licenses.length; j++) {

                    if ($scope.LicenseTypes[6].Licenses[j].LicenseID == data.federalDea.FederalDEAInformationID) {

                        //if ($scope.LicenseTypes[i].Licenses.length == 1) {

                        //    $scope.LicenseTypes.splice($scope.LicenseTypes.indexOf($scope.LicenseTypes[i]), 1);

                        //} else {

                        $scope.LicenseTypes[6].Licenses.splice($scope.LicenseTypes[6].Licenses.indexOf($scope.LicenseTypes[6].Licenses[j]), 1);

                        count = 1;

                        //}

                    }

                    if (count == 1) {

                        $scope.LicenseTypes[6].Licenses[j].LicenseName = "DEA" + (j + 1);

                    }

                }

                //    }

                //}

                $scope.SelectedLicense = {};

            }

        });
        $scope.$on('RemoveMedicareInformation', function (event, data) {

            var count = 0;

            if (data.MedicareInfo != null) {

                //for (var i = 0; i < $scope.LicenseTypes.length; i++) {

                //    if ($scope.LicenseTypes[i].LicenseTypeID == 8) {

                for (var j = 0; j < $scope.LicenseTypes[7].Licenses.length; j++) {

                    if ($scope.LicenseTypes[7].Licenses[j].LicenseID == data.MedicareInfo.MedicareInformationID) {

                        //if ($scope.LicenseTypes[i].Licenses.length == 1) {

                        //    $scope.LicenseTypes.splice($scope.LicenseTypes.indexOf($scope.LicenseTypes[i]), 1);

                        //} else {

                        $scope.LicenseTypes[7].Licenses.splice($scope.LicenseTypes[7].Licenses.indexOf($scope.LicenseTypes[7].Licenses[j]), 1);

                        count = 1;

                        //}

                    }

                    if (count == 1) {

                        $scope.LicenseTypes[7].Licenses[j].LicenseName = "Medicare" + (j + 1);

                    }

                }

                //    }

                //}

                $scope.SelectedLicense = {};

            }

        });
        $scope.$on('RemoveMedicaidInformation', function (event, data) {

            var count = 0;

            if (data.MedicaidInfo != null) {

                //for (var i = 0; i < $scope.LicenseTypes.length; i++) {

                //    if ($scope.LicenseTypes[i].LicenseTypeID == 9) {

                for (var j = 0; j < $scope.LicenseTypes[8].Licenses.length; j++) {

                    if ($scope.LicenseTypes[8].Licenses[j].LicenseID == data.MedicaidInfo.MedicaidInformationID) {

                        //if ($scope.LicenseTypes[i].Licenses.length == 1) {

                        //    $scope.LicenseTypes.splice($scope.LicenseTypes.indexOf($scope.LicenseTypes[i]), 1);

                        //} else {

                        $scope.LicenseTypes[8].Licenses.splice($scope.LicenseTypes[8].Licenses.indexOf($scope.LicenseTypes[8].Licenses[j]), 1);

                        count = 1;

                        //}

                    }

                    if (count == 1) {

                        $scope.LicenseTypes[8].Licenses[j].LicenseName = "Medicaid" + (j + 1);

                    }

                }

                //    }

                //}

                $scope.SelectedLicense = {};

            }

        });
        $scope.$on('RemoveCDSCInformation', function (event, data) {

            var count = 0;

            if (data.cDSCInformation != null) {

                //for (var i = 0; i < $scope.LicenseTypes.length; i++) {

                //    if ($scope.LicenseTypes[i].LicenseTypeID == 10) {

                for (var j = 0; j < $scope.LicenseTypes[9].Licenses.length; j++) {

                    if ($scope.LicenseTypes[9].Licenses[j].LicenseID == data.cDSCInformation.CDSCInformationID) {

                        //if ($scope.LicenseTypes[i].Licenses.length == 1) {

                        //    $scope.LicenseTypes.splice($scope.LicenseTypes.indexOf($scope.LicenseTypes[i]), 1);

                        //} else {

                        $scope.LicenseTypes[9].Licenses.splice($scope.LicenseTypes[9].Licenses.indexOf($scope.LicenseTypes[9].Licenses[j]), 1);

                        count = 1;

                        //}

                    }

                    if (count == 1) {

                        $scope.LicenseTypes[9].Licenses[j].LicenseName = "CDS" + (j + 1);

                    }

                }

                //    }

                //}

                $scope.SelectedLicense = {};

            }

        });
        $scope.$on('RemoveEducationDetailDoc', function (event, data) {

            var count = 0;
            var ug = "";
            var g = "";

            if (data.educationDetailViewModel != null) {

                //for (var i = 0; i < $scope.LicenseTypes.length; i++) {

                //    if ($scope.LicenseTypes[i].LicenseTypeID == 11) {

                for (var j = 0; j < $scope.LicenseTypes[10].Licenses.length; j++) {

                    if ($scope.LicenseTypes[10].Licenses[j].LicenseID == data.educationDetailViewModel.EducationDetailID) {

                        //if ($scope.LicenseTypes[i].Licenses.length == 1) {

                        //    $scope.LicenseTypes.splice($scope.LicenseTypes.indexOf($scope.LicenseTypes[i]), 1);

                        //} else {

                        ug = "UG" + (j + 1);
                        g = "Grad" + (j + 1);

                        if ($scope.LicenseTypes[10].Licenses[j].LicenseName == ug) {

                            $scope.countUG--;

                        } else if ($scope.LicenseTypes[10].Licenses[j].LicenseName == g) {

                            $scope.countG--;

                        }

                        $scope.LicenseTypes[10].Licenses.splice($scope.LicenseTypes[10].Licenses.indexOf($scope.LicenseTypes[10].Licenses[j]), 1);

                        count = 1;

                        //}

                    }

                    if (count == 1 && $scope.LicenseTypes[10].Licenses.length == 1) {

                        if ($scope.LicenseTypes[10].Licenses[j + 1].LicenseName == ug) {

                            $scope.LicenseTypes[10].Licenses[j + 1].LicenseName = "UG" + ($scope.countUG);

                        } else if ($scope.LicenseTypes[10].Licenses[j + 1].LicenseName == g) {

                            $scope.LicenseTypes[10].Licenses[j + 1].LicenseName = "Grad" + ($scope.countG);

                        }

                    }

                }

                //    }

                //}

                $scope.SelectedLicense = {};

            }

        });
        $scope.$on('RemoveProgramDetailsDoc', function (event, data) {

            var count = 0;
            var intern = "";
            var fell = "";
            var res = "";
            var other = "";

            if (data.residencyInternshipViewModel != null) {

                //for (var i = 0; i < $scope.LicenseTypes.length; i++) {

                //    if ($scope.LicenseTypes[i].LicenseTypeID == 13) {

                for (var j = 0; j < $scope.LicenseTypes[12].Licenses.length; j++) {

                    if ($scope.LicenseTypes[12].Licenses[j].LicenseID == data.residencyInternshipViewModel.ProgramDetailID) {

                        //if ($scope.LicenseTypes[i].Licenses.length == 1) {

                        //    $scope.LicenseTypes.splice($scope.LicenseTypes.indexOf($scope.LicenseTypes[i]), 1);

                        //} else {

                        intern = "Intern" + (j + 1);
                        fell = "Fell" + (j + 1);
                        res = "Res" + (j + 1);
                        other = "Other" + (j + 1);

                        if ($scope.LicenseTypes[12].Licenses[j].LicenseName == intern) {

                            $scope.countIntern--;

                        } else if ($scope.LicenseTypes[12].Licenses[j].LicenseName == fell) {

                            $scope.countFell--;

                        } else if ($scope.LicenseTypes[12].Licenses[j].LicenseName == res) {

                            $scope.countRes--;

                        } else if ($scope.LicenseTypes[12].Licenses[j].LicenseName == other) {

                            $scope.countOther--;

                        }

                        $scope.LicenseTypes[12].Licenses.splice($scope.LicenseTypes[12].Licenses.indexOf($scope.LicenseTypes[12].Licenses[j]), 1);

                        count = 1;

                        //}

                    }

                    if (count == 1) {

                        if ($scope.LicenseTypes[12].Licenses[j].LicenseName == intern) {

                            $scope.LicenseTypes[12].Licenses[j].LicenseName = "Intern" + (j);

                        } else if ($scope.LicenseTypes[12].Licenses[j].LicenseName == fell) {

                            $scope.LicenseTypes[12].Licenses[j].LicenseName = "Fell" + (j);

                        } else if ($scope.LicenseTypes[12].Licenses[j].LicenseName == res) {

                            $scope.LicenseTypes[12].Licenses[j].LicenseName = "Res" + (j);

                        } else if ($scope.LicenseTypes[12].Licenses[j].LicenseName == other) {

                            $scope.LicenseTypes[12].Licenses[j].LicenseName = "Other" + (j);

                        }

                    }

                }

                //    }

                //}

                $scope.SelectedLicense = {};

            }

        });
        $scope.$on('RemoveCMECertificationsDoc', function (event, data) {

            var count = 0;

            if (data.certificationCMEViewModel != null) {

                //for (var i = 0; i < $scope.LicenseTypes.length; i++) {

                //    if ($scope.LicenseTypes[i].LicenseTypeID == 14) {

                for (var j = 0; j < $scope.LicenseTypes[13].Licenses.length; j++) {

                    if ($scope.LicenseTypes[13].Licenses[j].LicenseID == data.certificationCMEViewModel.CMECertificationID) {

                        //if ($scope.LicenseTypes[i].Licenses.length == 1) {

                        //    $scope.LicenseTypes.splice($scope.LicenseTypes.indexOf($scope.LicenseTypes[i]), 1);

                        //} else {

                        $scope.LicenseTypes[13].Licenses.splice($scope.LicenseTypes[13].Licenses.indexOf($scope.LicenseTypes[13].Licenses[j]), 1);

                        count = 1;

                        //}

                    }

                    if (count == 1) {

                        $scope.LicenseTypes[13].Licenses[j].LicenseName = "CME" + (j + 1);

                    }

                }

                //    }

                //}

                $scope.SelectedLicense = {};

            }

        });
        $scope.$on('RemoveSpecialtyBoardCertifiedDetailDoc', function (event, data) {

            var count = 0;

            if (data.specialty != null) {

                //for (var i = 0; i < $scope.LicenseTypes.length; i++) {

                //    if ($scope.LicenseTypes[i].LicenseTypeID == 15) {

                for (var j = 0; j < $scope.LicenseTypes[14].Licenses.length; j++) {

                    if ($scope.LicenseTypes[14].Licenses[j].LicenseID == data.specialty.SpecialtyDetailID) {

                        //if ($scope.LicenseTypes[i].Licenses.length == 1) {

                        //    $scope.LicenseTypes.splice($scope.LicenseTypes.indexOf($scope.LicenseTypes[i]), 1);

                        //} else {

                        $scope.LicenseTypes[14].Licenses.splice($scope.LicenseTypes[14].Licenses.indexOf($scope.LicenseTypes[14].Licenses[j]), 1);

                        count = 1;

                        //}

                    }

                    if (count == 1) {

                        $scope.LicenseTypes[14].Licenses[j].LicenseName = "Board" + (j + 1);

                    }

                }

                //    }

                //}

                $scope.SelectedLicense = {};

            }

        });
        $scope.$on('RemoveHospitalPrivilegeDetailDoc', function (event, data) {

            var count = 0;

            if (data.hospitalPrivilege != null) {

                //for (var i = 0; i < $scope.LicenseTypes.length; i++) {

                //    if ($scope.LicenseTypes[i].LicenseTypeID == 16) {

                for (var j = 0; j < $scope.LicenseTypes[15].Licenses.length; j++) {

                    if ($scope.LicenseTypes[15].Licenses[j].LicenseID == data.hospitalPrivilege.HospitalPrivilegeDetailID) {

                        //if ($scope.LicenseTypes[i].Licenses.length == 1) {

                        //    $scope.LicenseTypes.splice($scope.LicenseTypes.indexOf($scope.LicenseTypes[i]), 1);

                        //} else {

                        $scope.LicenseTypes[15].Licenses.splice($scope.LicenseTypes[15].Licenses.indexOf($scope.LicenseTypes[15].Licenses[j]), 1);

                        count = 1;

                        //}

                    }

                    if (count == 1) {

                        $scope.LicenseTypes[15].Licenses[j].LicenseName = "HPL" + (j + 1);

                    }

                }

                //    }

                //}

                $scope.SelectedLicense = {};

            }

        });
        $scope.$on('RemoveProfessionalLiabilityInfoDoc', function (event, data) {

            var count = 0;

            if (data.professionalLiability != null) {

                //for (var i = 0; i < $scope.LicenseTypes.length; i++) {

                //    if ($scope.LicenseTypes[i].LicenseTypeID == 17) {

                for (var j = 0; j < $scope.LicenseTypes[16].Licenses.length; j++) {

                    if ($scope.LicenseTypes[16].Licenses[j].LicenseID == data.professionalLiability.ProfessionalLiabilityInfoID) {

                        //if ($scope.LicenseTypes[i].Licenses.length == 1) {

                        //    $scope.LicenseTypes.splice($scope.LicenseTypes.indexOf($scope.LicenseTypes[i]), 1);

                        //} else {

                        $scope.LicenseTypes[16].Licenses.splice($scope.LicenseTypes[16].Licenses.indexOf($scope.LicenseTypes[16].Licenses[j]), 1);

                        count = 1;

                        //}

                    }

                    if (count == 1) {

                        $scope.LicenseTypes[16].Licenses[j].LicenseName = "LIC" + (j + 1);

                    }

                }

                //    }

                //}

                $scope.SelectedLicense = {};

            }

        });
        $scope.$on('RemoveProfessionalWorkExperienceDoc', function (event, data) {

            var count = 0;

            if (data.professionalWorkExperience != null) {

                //for (var i = 0; i < $scope.LicenseTypes.length; i++) {

                //    if ($scope.LicenseTypes[i].LicenseTypeID == 18) {

                for (var j = 0; j < $scope.LicenseTypes[17].Licenses.length; j++) {

                    if ($scope.LicenseTypes[17].Licenses[j].LicenseID == data.professionalWorkExperience.ProfessionalWorkExperienceID) {

                        //if ($scope.LicenseTypes[i].Licenses.length == 1) {

                        //    $scope.LicenseTypes.splice($scope.LicenseTypes.indexOf($scope.LicenseTypes[i]), 1);

                        //} else {

                        $scope.LicenseTypes[17].Licenses.splice($scope.LicenseTypes[17].Licenses.indexOf($scope.LicenseTypes[17].Licenses[j]), 1);

                        count = 1;

                        //}

                    }

                    if (count == 1) {

                        $scope.LicenseTypes[17].Licenses[j].LicenseName = "Work Exp" + (j + 1);

                    }

                }

                //    }

                //}

                $scope.SelectedLicense = {};

            }

        });

        $scope.tempObj = {};

        $scope.moveDocumentUp = function (index) {

            //var tempObj = {};

            //var i = licenseType.indexOf(license);

            $scope.tempObj = $scope.spaDoc[index];
            $scope.spaDoc[index] = $scope.spaDoc[index - 1];
            $scope.spaDoc[index - 1] = $scope.tempObj;

            $scope.tempObj = {};

        }

        $scope.moveDocumentDown = function (index) {

            //var tempObj = {};

            //var i = licenseType.indexOf(license);

            $scope.tempObj = $scope.spaDoc[index];
            $scope.spaDoc[index] = $scope.spaDoc[index + 1];
            $scope.spaDoc[index + 1] = $scope.tempObj;

            $scope.tempObj = {};

        }

        $scope.printDiv = function (divName, DocPath) {

            $scope.path = DocPath;
            var printContents = document.getElementById(divName).innerHTML;
            var popupWin = window.open('', '_blank', 'width=auto,height=auto');
            popupWin.document.open()
            popupWin.document.write('<html><head><link rel="stylesheet" type="text/css" href="style.css" /></head><body onload="window.print()">' + printContents + '</html>');
            popupWin.document.close();
        }

        $scope.countDocument = 0;

        $scope.CountDoc = function () {

            $scope.countDocument += 1;

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
            //$scope.progressbar = true;
            $.ajax({
                url: '/Credentialing/DocChecklist/DownloadFile?CurrentFilePath=' + DocPath,
                //type: 'GET',
                //data: { DocumentPath: DocPath },
                //async: false,
                //cache: false,
                //contentType: false,
                //processData: false,
                //success: function (data) {
                    //console.log(data);

                    //$timeout(function () {
                    //    $scope.progressbar = false;

                    //    $scope.IsDownloadMessage = true;

                    //    $timeout(function () {
                    //        $scope.IsDownloadMessage = false;
                    //    }, 5000);

                    //}, 5000);

                //}
            });

        }

        $scope.countDoc = 0;

        $scope.SelectedDoc = function (Doc, index) {

            var isPresent = false;
            var tempObj = {};

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

        }

        $scope.isSelectAll = false;

        $scope.selectAll = function () {

            if (!$scope.isSelectAll) {

                for (var i = 0; i < $scope.spaDoc.length; i++) {

                    $scope.spaDoc[i].Status = true;

                }

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

    });

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