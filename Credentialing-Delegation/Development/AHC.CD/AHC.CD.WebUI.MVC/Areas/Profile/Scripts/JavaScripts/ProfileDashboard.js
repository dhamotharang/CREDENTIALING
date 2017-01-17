//------------------------------- Provider License Service --------------------
profileApp.service('ProviderLicenseService', function ($filter) {

    var data = [];
    var GrandTotalLicenses = 0;

    var GrandTotalValidLicense = 0;
    var GrandTotalPendingDaylicense = 0;
    var GrandTotalExpiredLicense = 0;

    this.GetFormattedProfileData = function (expiredLicense) {
        data = [];
        GrandTotalLicenses = 0;

        GrandTotalValidLicense = 0;
        GrandTotalPendingDaylicense = 0;
        GrandTotalExpiredLicense = 0;
        //-------------------------- Custom array parse ---------------------
        if (expiredLicense.StateLicenseExpiries) {
            data.push({
                LicenseType: "State License",
                LicenseTypeCode: "StateLicense",
                Licenses: expiredLicense.StateLicenseExpiries
            });
        }
        if (expiredLicense.DEALicenseExpiries) {
            data.push({
                LicenseType: "Federal DEA",
                LicenseTypeCode: "FederalDEA",
                Licenses: expiredLicense.DEALicenseExpiries
            });
        }
        if (expiredLicense.CDSCInfoExpiries) {
            data.push({
                LicenseType: "CDS Information",
                LicenseTypeCode: "CDSInformation",
                Licenses: expiredLicense.CDSCInfoExpiries
            });
        }
        if (expiredLicense.SpecialtyDetailExpiries) {
            data.push({
                LicenseType: "Specialty/Board",
                LicenseTypeCode: "SpecialityBoard",
                Licenses: expiredLicense.SpecialtyDetailExpiries
            });
        }
        if (expiredLicense.HospitalPrivilegeExpiries) {
            data.push({
                LicenseType: "Hospital Privileges",
                LicenseTypeCode: "HospitalPrivilages",
                Licenses: expiredLicense.HospitalPrivilegeExpiries
            });
        }
        if (expiredLicense.ProfessionalLiabilityExpiries) {
            data.push({
                LicenseType: "Professional Liability",
                LicenseTypeCode: "ProfessionalLiability",
                Licenses: expiredLicense.ProfessionalLiabilityExpiries
            });
        }
        if (expiredLicense.WorkerCompensationExpiries) {
            data.push({
                LicenseType: "Worker Compensation",
                LicenseTypeCode: "WorkerCompensation",
                Licenses: expiredLicense.WorkerCompensationExpiries
            });
        }

        if (expiredLicense.MedicareExpiries) {
            data.push({
                LicenseType: "Medicare Information",
                LicenseTypeCode: "MedicareInformation",
                Licenses: expiredLicense.MedicareExpiries
            });
        }

        if (expiredLicense.MedicaidExpiries) {
            data.push({
                LicenseType: "Medicaid Information",
                LicenseTypeCode: "MedicaidInformation",
                Licenses: expiredLicense.MedicaidExpiries
            });
        }

        //if (expiredLicense.UpComingRecredentials) {
        //    data.push({
        //        LicenseType: "UpComing Recredentials",
        //        LicenseTypeCode: "UpComingRecredentials",
        //        Licenses: expiredLicenses[0].UpComingRecredentials
        //    });
        //}

        //------------------- left day calculate ----------------------
        for (var i in data) {
            if (data[i].Licenses && (data[i].LicenseType == "State License" || data[i].LicenseType == "Federal DEA" || data[i].LicenseType == "CDS Information" || data[i].LicenseType == "Specialty/Board")) {
                for (var j in data[i].Licenses) {
                    data[i].Licenses[j].ExpiryDate = ConvertDateFormats1(data[i].Licenses[j].ExpiryDate);
                    data[i].Licenses[j].dayLeft = GetRenewalDayLeft(data[i].Licenses[j].ExpiryDate);
                }
            } else if (data[i].Licenses && data[i].LicenseType == "Hospital Privileges") {
                for (var j in data[i].Licenses) {
                    data[i].Licenses[j].AffiliationEndDate = ConvertDateFormats1(data[i].Licenses[j].AffiliationEndDate);
                    data[i].Licenses[j].dayLeft = GetRenewalDayLeft(data[i].Licenses[j].AffiliationEndDate);
                }
            } else if (data[i].LicenseType == "Professional Liability" || data[i].Licenses && data[i].LicenseType == "Worker Compensation" || data[i].LicenseType == "Medicare Information" || data[i].LicenseType == "Medicaid Information") {
                for (var j in data[i].Licenses) {
                    data[i].Licenses[j].ExpirationDate = ConvertDateFormats1(data[i].Licenses[j].ExpirationDate);
                    data[i].Licenses[j].dayLeft = GetRenewalDayLeft(data[i].Licenses[j].ExpirationDate);
                }
            }
            else if (data[i].Licenses && data[i].LicenseType == "UpComingRecredentials") {
                for (var j in data[i].Licenses) {
                    data[i].Licenses[j].ReCredentialingDate = ConvertDateFormats1(data[i].Licenses[j].ReCredentialingDate);
                    data[i].Licenses[j].dayLeft = GetRenewalDayLeft(data[i].Licenses[j].ReCredentialingDate);
                }
            }

        }
        return this.GetLicenseStatus(data);
    }


    GetRenewalDayLeft = function (datevalue) {
        if (datevalue) {
            var oneDay = 24 * 60 * 60 * 1000; // hours*minutes*seconds*milliseconds

            var currentdate = new Date(new Date().getFullYear(), new Date().getMonth(), new Date().getDate());

            var secondDate = new Date(2008, 01, 22);

            return Math.round((datevalue.getTime() - currentdate.getTime()) / (oneDay));
        }
        return null;
    };

    //--------------- Master license Data for Static Data ---------------
    var MasterLicenseData = angular.copy(data);

    //---------------------- license status return -------------------
    this.GetLicenseStatus = function (data) {
        GrandTotalLicenses = 0;
        GrandTotalValidLicense = 0;
        GrandTotalPendingDaylicense = 0;
        GrandTotalExpiredLicense = 0;
        for (var i in data) {
            if (data[i].Licenses) {

                var ValidatedLicense = 0;
                var dayLeftLicense = 0;
                var ExpiredLicense = 0;

                for (var j in data[i].Licenses) {
                    if (data[i].Licenses[j].dayLeft < 0) {
                        ExpiredLicense++;
                        GrandTotalLicenses++;
                    } else if (data[i].Licenses[j].dayLeft < 90) {
                        dayLeftLicense++;
                        GrandTotalLicenses++;
                    }
                    else if (data[i].Licenses[j].dayLeft < 180) {
                        ValidatedLicense++;
                        GrandTotalLicenses++;
                    }
                }

                var orderBy = $filter('orderBy');
                data[i].Licenses = orderBy(data[i].Licenses, 'dayLeft', false);

                data[i].LicenseStatus = {
                    ValidLicense: ValidatedLicense,
                    PendingDaylicense: dayLeftLicense,
                    ExpiredLicense: ExpiredLicense
                };

                GrandTotalValidLicense += ValidatedLicense;
                GrandTotalPendingDaylicense += dayLeftLicense;
                GrandTotalExpiredLicense += ExpiredLicense;
            }
        }
        return data;
    };
    //------------------ Grand Total Number of License return ---------------------
    this.GetGrandTotalLicenses = function () {
        return GrandTotalLicenses;
    };

    //------------------ Grand Total License Upcomin renewal expired of License return ---------------------
    this.GetGrandTotalLicenseStatus = function () {
        var temd = {
            GrandTotalValidLicense: GrandTotalValidLicense,
            GrandTotalPendingDaylicense: GrandTotalPendingDaylicense,
            GrandTotalExpiredLicense: GrandTotalExpiredLicense
        }
        return temd;
    };

    //----------------- simply return License List ---------------
    this.LicensesList = function () {
        this.GetLicenseStatus(data);
        return data;
    };

});

profileApp.controller('profileAppCtrl', ['$scope', '$rootScope', '$http', 'masterDataService', 'messageAlertEngine', '$filter', 'ProviderLicenseService', function ($scope, $rootScope, $http, masterDataService, messageAlertEngine, $filter, ProviderLicenseService) {

    $scope.profileID = profileId;
    $scope.IsMissingCredentialing = false;
    $scope.IsUpcomingRenewal = false;
    $scope.IsUpcomingReCredentialing = false;
    $scope.IsPsvDone = false;
    $scope.IsInitiateCredentialing = false;
    $scope.loading = true;

    $scope.setFiles = function (file) {
        $(file).parent().parent().find(".jancyFileWrapTexts").find("span").width($(file).parent().parent().width() < 243 ? $(file).parent().parent().width() : 243);

    }

    $rootScope.$on('ProfileDashboard', function () {
        $scope.loading = true;
        //========= for profile summary master data ======

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

        $http.get(rootDir + '/Dashboard/GetAllExpiresForAProvider?profileId=' + profileId).
           success(function (data, status, headers, config) {
               $scope.tempData = ProviderLicenseService.GetFormattedProfileData(data.data);
               $scope.GrandTotal = ProviderLicenseService.GetGrandTotalLicenseStatus();
           })
               .error(function (data, status, headers, config) {
                   $scope.loading = false;
               });

        $scope.profileViewSection = [];
        $http.get(rootDir + '/Profile/MasterData/GetAllProfileSections').then(function (value) {
            $scope.ProfileSections = value;
            $scope.OutOffCount = 0;
            $scope.SucessCount = 0;
            $scope.DangerCount = 0;
            if ($scope.ProfileSections.data.length != 0) {
                $http.get(rootDir + '/Profile/MasterData/GetAllNotApplicableSectoin?profileId=' + profileId).then(function (value) {

                    $scope.ProfileNotApplicableSections = value;

                    $http.get(rootDir + '/NewQuickUpdate/TestActionMethod?profileId=' + profileId).
                    success(function (data, status, headers, config) {

                        try {
                            $scope.providerdata = angular.copy(data.profilereviewdata);

                            if ($scope.ProfileNotApplicableSections.data.length != 0) {
                                for (var i = 0; i < $scope.ProfileSections.data.length; i++) {
                                    var flag = 0;
                                    for (var j = 0; j < $scope.ProfileNotApplicableSections.data.length; j++) {

                                        if ($scope.ProfileSections.data[i].ProfileSectionID == $scope.ProfileNotApplicableSections.data[j].ProfileSectionID) {
                                            flag = 1;
                                            break;
                                        }
                                    }
                                    if (flag == 0) {

                                        var obj = {
                                            ProfileSectionID: $scope.ProfileSections.data[i].ProfileSectionID,
                                            ProfileSectionName: $scope.ProfileSections.data[i].ProfileSectionName,
                                        };
                                        $scope.profileViewSection.push(obj);
                                    }

                                }
                                if ($scope.profileViewSection.length != 0) {
                                    for (var i = 0; i < $scope.profileViewSection.length; i++) {
                                        if ($scope.profileViewSection[i].ProfileSectionName == "Personal Details") {
                                            $scope.OutOffCount = $scope.OutOffCount + 1;
                                            if ($scope.providerdata.PersonalDetail != null) {
                                                $scope.profileViewSection[i].class = "successClass";
                                                $scope.SucessCount = $scope.SucessCount + 1;
                                            }
                                            else {
                                                $scope.profileViewSection[i].class = "dangerClass";
                                                $scope.DangerCount = $scope.DangerCount + 1;
                                            }
                                        }
                                        if ($scope.profileViewSection[i].ProfileSectionName == "Home Address") {
                                            $scope.OutOffCount = $scope.OutOffCount + 1;
                                            if ($scope.providerdata.HomeAddresses.length != 0) {
                                                $scope.profileViewSection[i].class = "successClass";
                                                $scope.SucessCount = $scope.SucessCount + 1;
                                            }
                                            else {
                                                $scope.profileViewSection[i].class = "dangerClass";
                                                $scope.DangerCount = $scope.DangerCount + 1;
                                            }
                                        }
                                        if ($scope.profileViewSection[i].ProfileSectionName == "State License") {
                                            $scope.OutOffCount = $scope.OutOffCount + 1;
                                            if ($scope.providerdata.StateLicenses.length != 0) {
                                                $scope.profileViewSection[i].class = "successClass";
                                                $scope.SucessCount = $scope.SucessCount + 1;
                                            }
                                            else {
                                                $scope.profileViewSection[i].class = "dangerClass";
                                                $scope.DangerCount = $scope.DangerCount + 1;
                                            }
                                        }
                                        if ($scope.profileViewSection[i].ProfileSectionName == "Federal DEA Information") {
                                            $scope.OutOffCount = $scope.OutOffCount + 1;
                                            if ($scope.providerdata.FederalDEALicenses.length != 0) {
                                                $scope.profileViewSection[i].class = "successClass";
                                                $scope.SucessCount = $scope.SucessCount + 1;
                                            }
                                            else {
                                                $scope.profileViewSection[i].class = "dangerClass";
                                                $scope.DangerCount = $scope.DangerCount + 1;
                                            }
                                        }
                                        if ($scope.profileViewSection[i].ProfileSectionName == "CDS Information") {
                                            $scope.OutOffCount = $scope.OutOffCount + 1;
                                            if ($scope.providerdata.CDSInformations.length > 0) {
                                                $scope.profileViewSection[i].class = "successClass";
                                                $scope.SucessCount = $scope.SucessCount + 1;
                                            }
                                            else {
                                                $scope.profileViewSection[i].class = "dangerClass";
                                                $scope.DangerCount = $scope.DangerCount + 1;
                                            }
                                        }
                                        if ($scope.profileViewSection[i].ProfileSectionName == "Specialty Details") {
                                            $scope.OutOffCount = $scope.OutOffCount + 1;
                                            if ($scope.providerdata.SpecialtyDetails.length != 0) {
                                                $scope.profileViewSection[i].class = "successClass";
                                                $scope.SucessCount = $scope.SucessCount + 1;
                                            }
                                            else {
                                                $scope.profileViewSection[i].class = "dangerClass";
                                                $scope.DangerCount = $scope.DangerCount + 1;
                                            }
                                        }
                                        if ($scope.profileViewSection[i].ProfileSectionName == "Covering Physicians") {
                                            $scope.OutOffCount = $scope.OutOffCount + 1;
                                            if ($scope.providerdata.CoveringPhysicians.length != 0) {
                                                $scope.profileViewSection[i].class = "successClass";
                                                $scope.SucessCount = $scope.SucessCount + 1;
                                            }
                                            else {
                                                $scope.profileViewSection[i].class = "dangerClass";
                                                $scope.DangerCount = $scope.DangerCount + 1;
                                            }
                                        }
                                        if ($scope.profileViewSection[i].ProfileSectionName == "Facility Details") {
                                            $scope.OutOffCount = $scope.OutOffCount + 1;
                                            if ($scope.providerdata.Facilities.length != 0) {
                                                $scope.profileViewSection[i].class = "successClass";
                                                $scope.SucessCount = $scope.SucessCount + 1;
                                            }
                                            else {
                                                $scope.profileViewSection[i].class = "dangerClass";
                                                $scope.DangerCount = $scope.DangerCount + 1;
                                            }
                                        }
                                        if ($scope.profileViewSection[i].ProfileSectionName == "Services") {
                                            $scope.OutOffCount = $scope.OutOffCount + 1;
                                            if ($scope.providerdata.Services.length != 0) {
                                                $scope.profileViewSection[i].class = "successClass";
                                                $scope.SucessCount = $scope.SucessCount + 1;
                                            }
                                            else {
                                                $scope.profileViewSection[i].class = "dangerClass";
                                                $scope.DangerCount = $scope.DangerCount + 1;
                                            }
                                        }
                                        if ($scope.profileViewSection[i].ProfileSectionName == "Hospital Privilege Detail") {
                                            $scope.OutOffCount = $scope.OutOffCount + 1;
                                            if ($scope.providerdata.HospitalPrivileges.length != 0) {
                                                $scope.profileViewSection[i].class = "successClass";
                                                $scope.SucessCount = $scope.SucessCount + 1;
                                            }
                                            else {
                                                $scope.profileViewSection[i].class = "dangerClass";
                                                $scope.DangerCount = $scope.DangerCount + 1;
                                            }
                                        }
                                        if ($scope.profileViewSection[i].ProfileSectionName == "Work Gap") {
                                            $scope.OutOffCount = $scope.OutOffCount + 1;
                                            if ($scope.providerdata.WorkGaps.length != 0) {
                                                $scope.profileViewSection[i].class = "successClass";
                                                $scope.SucessCount = $scope.SucessCount + 1;

                                            }
                                            else {
                                                $scope.profileViewSection[i].class = "dangerClass";
                                                $scope.DangerCount = $scope.DangerCount + 1;
                                            }
                                        }
                                        if ($scope.profileViewSection[i].ProfileSectionName == "CV Information") {
                                            $scope.OutOffCount = $scope.OutOffCount + 1;
                                            if ($scope.providerdata.CV.CVDocumentPath != null) {
                                                $scope.profileViewSection[i].class = "successClass";
                                                $scope.SucessCount = $scope.SucessCount + 1;

                                            }
                                            else {
                                                $scope.profileViewSection[i].class = "dangerClass";
                                                $scope.DangerCount = $scope.DangerCount + 1;
                                            }
                                        }
                                        if ($scope.profileViewSection[i].ProfileSectionName == "Group Information") {
                                            $scope.OutOffCount = $scope.OutOffCount + 1;
                                            if ($scope.providerdata.ContractGroupInformation.length > 0) {
                                                if ($scope.providerdata.ContractGroupInformation[0].ContractGroupInfoes.length > 0) {
                                                    $scope.profileViewSection[i].class = "successClass";
                                                    $scope.SucessCount = $scope.SucessCount + 1;
                                                }
                                                else {
                                                    $scope.profileViewSection[i].class = "dangerClass";
                                                    $scope.DangerCount = $scope.DangerCount + 1;
                                                }

                                            }
                                            else {
                                                $scope.profileViewSection[i].class = "dangerClass";
                                                $scope.DangerCount = $scope.DangerCount + 1;
                                            }
                                        }

                                    }
                                }
                            }
                            else {
                                for (var i = 0; i < $scope.ProfileSections.data.length; i++) {
                                    var obj = {
                                        ProfileSectionID: $scope.ProfileSections.data[i].ProfileSectionID,
                                        ProfileSectionName: $scope.ProfileSections.data[i].ProfileSectionName,
                                    };
                                    $scope.profileViewSection.push(obj);
                                }

                                if ($scope.profileViewSection.length != 0) {
                                    for (var i = 0; i < $scope.profileViewSection.length; i++) {
                                        if ($scope.profileViewSection[i].ProfileSectionName == "Personal Details") {
                                            $scope.OutOffCount = $scope.OutOffCount + 1;
                                            if ($scope.providerdata.PersonalDetail != null) {
                                                $scope.profileViewSection[i].class = "successClass";
                                                $scope.SucessCount = $scope.SucessCount + 1;

                                            }
                                            else {
                                                $scope.profileViewSection[i].class = "dangerClass";
                                                $scope.DangerCount = $scope.DangerCount + 1;
                                            }
                                        }
                                        if ($scope.profileViewSection[i].ProfileSectionName == "Home Address") {
                                            $scope.OutOffCount = $scope.OutOffCount + 1;
                                            if ($scope.providerdata.HomeAddresses.length != 0) {
                                                $scope.profileViewSection[i].class = "successClass";
                                                $scope.SucessCount = $scope.SucessCount + 1;

                                            }
                                            else {
                                                $scope.profileViewSection[i].class = "dangerClass";
                                                $scope.DangerCount = $scope.DangerCount + 1;
                                            }
                                        }
                                        if ($scope.profileViewSection[i].ProfileSectionName == "State License") {
                                            $scope.OutOffCount = $scope.OutOffCount + 1;
                                            if ($scope.providerdata.StateLicenses.length != 0) {
                                                $scope.profileViewSection[i].class = "successClass";
                                                $scope.SucessCount = $scope.SucessCount + 1;

                                            }
                                            else {
                                                $scope.profileViewSection[i].class = "dangerClass";
                                                $scope.DangerCount = $scope.DangerCount + 1;
                                            }
                                        }
                                        if ($scope.profileViewSection[i].ProfileSectionName == "Federal DEA Information") {
                                            $scope.OutOffCount = $scope.OutOffCount + 1;
                                            if ($scope.providerdata.FederalDEALicenses.length != 0) {
                                                $scope.profileViewSection[i].class = "successClass";
                                                $scope.SucessCount = $scope.SucessCount + 1;

                                            }
                                            else {
                                                $scope.profileViewSection[i].class = "dangerClass";
                                                $scope.DangerCount = $scope.DangerCount + 1;
                                            }
                                        }
                                        if ($scope.profileViewSection[i].ProfileSectionName == "CDS Information") {
                                            $scope.OutOffCount = $scope.OutOffCount + 1;
                                            if ($scope.providerdata.CDSInformations.length > 0) {
                                                $scope.profileViewSection[i].class = "successClass";
                                                $scope.SucessCount = $scope.SucessCount + 1;

                                            }
                                            else {
                                                $scope.profileViewSection[i].class = "dangerClass";
                                                $scope.DangerCount = $scope.DangerCount + 1;
                                            }
                                        }
                                        if ($scope.profileViewSection[i].ProfileSectionName == "Specialty Details") {
                                            $scope.OutOffCount = $scope.OutOffCount + 1;
                                            if ($scope.providerdata.SpecialtyDetails.length != 0) {
                                                $scope.profileViewSection[i].class = "successClass";
                                                $scope.SucessCount = $scope.SucessCount + 1;

                                            }
                                            else {
                                                $scope.profileViewSection[i].class = "dangerClass";
                                                $scope.DangerCount = $scope.DangerCount + 1;
                                            }
                                        }
                                        if ($scope.profileViewSection[i].ProfileSectionName == "Covering Physicians") {
                                            $scope.OutOffCount = $scope.OutOffCount + 1;
                                            if ($scope.providerdata.CoveringPhysicians.length != 0) {
                                                $scope.profileViewSection[i].class = "successClass";
                                                $scope.SucessCount = $scope.SucessCount + 1;

                                            }
                                            else {
                                                $scope.profileViewSection[i].class = "dangerClass";
                                                $scope.DangerCount = $scope.DangerCount + 1;
                                            }
                                        }
                                        if ($scope.profileViewSection[i].ProfileSectionName == "Facility Details") {
                                            $scope.OutOffCount = $scope.OutOffCount + 1;
                                            if ($scope.providerdata.Facilities.length != 0) {
                                                $scope.profileViewSection[i].class = "successClass";
                                                $scope.SucessCount = $scope.SucessCount + 1;

                                            }
                                            else {
                                                $scope.profileViewSection[i].class = "dangerClass";
                                                $scope.DangerCount = $scope.DangerCount + 1;
                                            }
                                        }
                                        if ($scope.profileViewSection[i].ProfileSectionName == "Services") {
                                            $scope.OutOffCount = $scope.OutOffCount + 1;
                                            if ($scope.providerdata.Services.length != 0) {
                                                $scope.profileViewSection[i].class = "successClass";
                                                $scope.SucessCount = $scope.SucessCount + 1;

                                            }
                                            else {
                                                $scope.profileViewSection[i].class = "dangerClass";
                                                $scope.DangerCount = $scope.DangerCount + 1;
                                            }
                                        }
                                        if ($scope.profileViewSection[i].ProfileSectionName == "Hospital Privilege Detail") {
                                            $scope.OutOffCount = $scope.OutOffCount + 1;
                                            if ($scope.providerdata.HospitalPrivileges.length != 0) {
                                                $scope.profileViewSection[i].class = "successClass";
                                                $scope.SucessCount = $scope.SucessCount + 1;

                                            }
                                            else {
                                                $scope.profileViewSection[i].class = "dangerClass";
                                                $scope.DangerCount = $scope.DangerCount + 1;
                                            }
                                        }
                                        if ($scope.profileViewSection[i].ProfileSectionName == "Work Gap") {
                                            $scope.OutOffCount = $scope.OutOffCount + 1;
                                            if ($scope.providerdata.WorkGaps.length != 0) {
                                                $scope.profileViewSection[i].class = "successClass";
                                                $scope.SucessCount = $scope.SucessCount + 1;

                                            }
                                            else {
                                                $scope.profileViewSection[i].class = "dangerClass";
                                                $scope.DangerCount = $scope.DangerCount + 1;
                                            }
                                        }
                                        if ($scope.profileViewSection[i].ProfileSectionName == "CV Information") {
                                            $scope.OutOffCount = $scope.OutOffCount + 1;
                                            if ($scope.providerdata.CV.CVDocumentPath != null) {
                                                $scope.profileViewSection[i].class = "successClass";
                                                $scope.SucessCount = $scope.SucessCount + 1;

                                            }
                                            else {
                                                $scope.profileViewSection[i].class = "dangerClass";
                                                $scope.DangerCount = $scope.DangerCount + 1;
                                            }
                                        }
                                        if ($scope.profileViewSection[i].ProfileSectionName == "Group Information") {
                                            $scope.OutOffCount = $scope.OutOffCount + 1;
                                            if ($scope.providerdata.ContractGroupInformation.length > 0) {
                                                if ($scope.providerdata.ContractGroupInformation[0].ContractGroupInfoes.length > 0) {
                                                    $scope.profileViewSection[i].class = "successClass";
                                                    $scope.SucessCount = $scope.SucessCount + 1;
                                                }
                                                else {
                                                    $scope.profileViewSection[i].class = "dangerClass";
                                                    $scope.DangerCount = $scope.DangerCount + 1;
                                                }                                                

                                            }
                                            else {
                                                $scope.profileViewSection[i].class = "dangerClass";
                                                $scope.DangerCount = $scope.DangerCount + 1;
                                            }
                                        }

                                    }
                                }

                            }

                            $scope.loading = false;
                        } catch (e) {
                        }
                    }).
       error(function (data, status, headers, config) {
       });

                });
            }
        });
        //================================================

        //------------- get List Of Plan from master data ---------------
        $http.get(rootDir + '/MasterDataNew/GetAllPlans').
       success(function (data, status, headers, config) {
           try {
               $scope.Plans = data;
               $scope.data = data;
               $scope.progressbar = false;
           } catch (e) {
              
           }
       }).
       error(function (data, status, headers, config) {
       });

        //------------- get list of plan for which provider is associated ---------------
        $scope.PlanListIDs = [];
        $scope.CredentialingContractRequests = [];
        $scope.structuredata = [];
        $http.get(rootDir + '/Credentialing/Initiation/getPlanListforCredentialingContractRequest?ProviderID=' + profileId).
            success(function (data, status, headers, config) {
                try {
                    if (data.status == true) {
                        $scope.PlanList = angular.copy(data.data1);
                        for (var i = 0; i < $scope.PlanList.length; i++) {
                            $scope.PlanListIDs.push($scope.PlanList[i].PlanID);
                        }
                        if ($scope.PlanListIDs.length != 0) {
                            $http.get(rootDir + '/Credentialing/Initiation/getCredentialingContractRequestForAllPlan', { params: { "ProviderID": profileId, "PlanIDs": $scope.PlanListIDs } }).
                                success(function (data, status, headers, config) {

                                    try {

                                        $scope.CredContractRequest = angular.copy(data);

                                        for (var i = 0; i < $scope.CredContractRequest.length; i++) {
                                            $scope.CredContractRequest[i].CredentialingContractRequests.TableRowStatus = $scope.CredContractRequest[i].TableRowStatus;
                                            $scope.CredContractRequest[i].CredentialingContractRequests.credID = $scope.CredContractRequest[i].credID;
                                            $scope.structuredata.push({ PlanObject: $scope.CredContractRequest[i].PlanObj, CredentialingContractRequest: $scope.CredContractRequest[i].CredentialingContractRequests });
                                        }


                                        $scope.FormatDataForRecredentialing($scope.structuredata);
                                        $scope.loading = false;

                                    } catch (e) {
                                      
                                    }
                                }).error(function (data, status, headers, config) {
                                    $scope.loading = false;
                                });
                        }
                        else {
                            $scope.errorInitiated = false;
                            $scope.loading = false;
                        }
                    }
                } catch (e) {
                   
                }
            })
            .error(function (data, status, headers, config) {
            });

        $http.get(rootDir + '/Profile/MasterData/GetAllProfileVerificationParameter').
               success(function (data, status, headers, config) {
                   try {
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
                   } catch (e) {
                     
                   }

               }).
               error(function (data, status, headers, config) {
               });

        //$http.get(rootDir + '/Dashboard/GetAllExpiresForAProvider?profileId=' + profileId).
        //    success(function (data, status, headers, config) {
        //        $scope.tempData = ProviderLicenseService.GetFormattedProfileData(data.data);
        //        $scope.GrandTotal = ProviderLicenseService.GetGrandTotalLicenseStatus();
        //    })
        //        .error(function (data, status, headers, config) {
        //            $scope.loading = false;
        //        });


        $http.get(rootDir + '/Credentialing/Verification/GetPSVDetailsForAProvider?profileId=' + profileId).
     success(function (data, status, headers, config) {
         try {
             if (data.status == 'true') {
                 if (data.psvInfo != null) {
                     if (data.psvInfo.ProfileVerificationDetails.length != 0) {
                         $scope.errorInitiatedForNoPSV = true;
                         $scope.psvInfoNew = angular.copy(data);
                         if ($scope.psvInfoNew.psvInfo.ProfileVerificationDetails.length > 0) {
                             $scope.psvData = $scope.psvInfoNew.psvInfo.ProfileVerificationDetails[0];
                             for (var i = 0; i < $scope.psvInfoNew.psvInfo.ProfileVerificationDetails.length; i++) {
                                 if ($scope.psvData.VerificationDate < $scope.psvInfoNew.psvInfo.ProfileVerificationDetails[i].VerificationDate) {
                                     $scope.psvData = $scope.psvInfoNew.psvInfo.ProfileVerificationDetails[i];
                                 }
                             }
                             $scope.psvDate = $scope.ConvertDateTo($scope.psvData.VerificationDate);
                         }
                         else {
                             $scope.errorInitiatedForNoPSV = false;
                         }

                     }
                     else {
                         $scope.errorInitiatedForNoPSV = false;
                     }
                 }
                 else {
                     $scope.errorInitiatedForNoPSV = false;
                 }
             }
             else {
                 $scope.errorInitiatedForNoPSV = false;
             }

         } catch (e) {
            
         }
     })
     .error(function (data, status, headers, config) {
     });


        $http.get(rootDir + '/Profile/MasterProfile/GetUpdatesById?profileId=' + profileId).
    success(function (data, status, headers, config) {
        try {
            if (data.length != 0) {
                $scope.FormatDataForSection(data);
            }
            else {
                $scope.showUpdateError = true;
            }
        } catch (e) {
          
        }


    })
    .error(function (data, status, headers, config) {
    });
    });

    $scope.review1 = true;
    $scope.ProfileReview = true;

    $scope.miss = false;
    $scope.loading = true;
    $scope.confirmationNotApplicable = function (data) {
        $('#notApplicableModal').modal({
            backdrop: 'static'
        });
        $scope.RemoveSection = data;
    };

    $scope.NotApplicable = function (sectionData) {

        var obj = {
            ProfileReviewSectionID: 0,
            ProfileSectionID: sectionData.ProfileSectionID,
            ProfileID: profileId,
            DisplayType: 2,
            StatusType: 1,
        }

        $http.post(rootDir + '/Profile/NewQuickUpdate/DisplayProfileSectionNotApplicable', obj).
           success(function (data, status, headers, config) {
               try {
                   if (data.status == "true") {
                       if (sectionData.class == "dangerClass") {
                           $scope.DangerCount = $scope.DangerCount - 1;
                       }
                       $scope.OutOffCount = $scope.OutOffCount - 1;
                       $('#notApplicableModal').modal('hide');
                       messageAlertEngine.callAlertMessage('successfulNotApplicable', "Section Removed Successfully.", "success", true);
                       sectionData.class = 'hide';
                   }
               } catch (e) {
                 
               }

           }).
           error(function (data, status, headers, config) {
               //----------- error message -----------
               messageAlertEngine.callAlertMessage('errorNotApplicable', "Sorry for Inconvenience !!!! Please Try Again Later...", "danger", true);
           });

    }


    $scope.Opensection = function (p) {

        if (p.ProfileSectionName == "Personal Details" || p.ProfileSectionName == "Home Address") {
            $scope.callViewDemograhic();
        }

        if (p.ProfileSectionName == "State License" || p.ProfileSectionName == "Federal DEA Information" || p.ProfileSectionName == "CDS Information") {
            $scope.callViewIdentification();
        }

        if (p.ProfileSectionName == "Specialty Details") {
            $scope.callViewSpecialty();
        }

        if (p.ProfileSectionName == "Covering Physicians" || p.ProfileSectionName == "Facility Details" || p.ProfileSectionName == "Services") {
            $scope.callViewPractice();
        }

        if (p.ProfileSectionName == "Hospital Privilege Detail") {
            $scope.callViewHospital();
        }

        if (p.ProfileSectionName == "Work Gap" || p.ProfileSectionName == "CV Information") {
            $scope.callViewWorkHistory();
        }

        if (p.ProfileSectionName == "Group Information") {
            $scope.callViewContractInfo();
        }

    }

    $scope.closeAlertMessageDiv = function () {
        $("#RenewUpdateError").hide();
    }

    $scope.callViewDemograhic = function () {
        $('.tab-label').removeClass('active');
        $('.tab-pane').removeClass('active');
        $('#tabhome').addClass('active');
        $('#home').addClass('active');
    };

    $scope.callViewIdentification = function () {
        $rootScope.$broadcast("IdentificationLicenses");
        $('.tab-label').removeClass('active');
        $('.tab-pane').removeClass('active');
        $('#tabidentification').addClass('active');
        $('#identification').addClass('active');
    };

    $scope.callViewPractice = function () {
        $rootScope.$broadcast("PracticeLocation");
        $('.tab-label').removeClass('active');
        $('.tab-pane').removeClass('active');
        $('#tabpractice').addClass('active');
        $('#practice').addClass('active');
    };

    $scope.callViewHospital = function () {
        $rootScope.$broadcast("HospitalPrivilege");
        $('.tab-label').removeClass('active');
        $('.tab-pane').removeClass('active');
        $('#tabhospital').addClass('active');
        $('#hospital').addClass('active');
    };

    $scope.callViewWorkHistory = function () {
        $rootScope.$broadcast("WorkHistory");
        $('.tab-label').removeClass('active');
        $('.tab-pane').removeClass('active');
        $('#tabworkhistory').addClass('active');
        $('#workHistory').addClass('active');
    };

    $scope.callViewSpecialty = function () {
        $rootScope.$broadcast("Specialty");
        $('.tab-label').removeClass('active');
        $('.tab-pane').removeClass('active');
        $('#tabspecialty').addClass('active');
        $('#specialty').addClass('active');
    };

    $scope.callViewContractInfo = function () {
        $rootScope.$broadcast("ContractInformation");
        $('.tab-label').removeClass('active');
        $('.tab-pane').removeClass('active');
        $('#tabContractInfo').addClass('active');
        $('#ContractInfo').addClass('active');
    };

    $scope.callViewProfessional = function () {
        $rootScope.$broadcast("ProfessionalLiability");
        $('.tab-label').removeClass('active');
        $('.tab-pane').removeClass('active');
        $('#tabliability').addClass('active');
        $('#liability').addClass('active');
    };

    $scope.CallView1 = function () {
        $scope.ProfileReview = true;
        $scope.review1 = true;
        $scope.review2 = false;
        $scope.review3 = false;
        $scope.review4 = false;
        $scope.review5 = false;
        $scope.loading = true;
        $scope.loading = false;
    }

    $scope.CallView2 = function () {
        $scope.loading = true;
        $scope.review1 = false;
        $scope.review2 = true;
        $scope.review3 = false;
        $scope.review4 = false;
        $scope.review5 = false;
        $scope.loading = false;
    }

    $scope.CallView3 = function () {
        $scope.isHasError = false;
        $scope.loadingAjax1 = true;
        $scope.selection = [];
        $scope.loading = true;
        $scope.review1 = false;
        $scope.review2 = false;
        $scope.review3 = true;
        $scope.review4 = false;
        $scope.review5 = false;
        $scope.loading = false;
    }

    $scope.FormatDataForRecredentialing = function (data) {

        if (data.length != 0) {

            $scope.formattedDataForRecred1 = [];

            for (var i = 0; i < data.length; i++) {

                if (data[i].CredentialingContractRequest.TableRowStatus == true) {

                    if (data[i].CredentialingContractRequest.ContractGrid.length != 0) {

                        for (var j = 0; j < data[i].CredentialingContractRequest.ContractGrid.length; j++) {

                            if (data[i].CredentialingContractRequest.ContractGrid[j].Report != null) {

                                $scope.DisplayDate = data[i].CredentialingContractRequest.ContractGrid[0].Report.ReCredentialingDate;

                                if ($scope.DisplayDate == null) {

                                    $scope.DisplayDate = "Not Available"
                                }
                                else if ($scope.DisplayDate < data[i].CredentialingContractRequest.ContractGrid[j].Report.ReCredentialingDate) {

                                    $scope.DisplayDate = data[i].CredentialingContractRequest.ContractGrid[j].Report.ReCredentialingDate;
                                }
                            }
                        }

                        if ($scope.DisplayDate == "Not Available") {
                            var days = "Not Available";
                            $scope.formattedDataForRecred1.push({ PlanName: data[i].PlanObject.PlanName, Days: days.toString() });
                        }
                        else if ($scope.DisplayDate != null) {

                            var d1 = new Date();
                            var d2 = new Date($scope.ConvertDateFormat1($scope.DisplayDate));
                            var miliseconds = d2 - d1;
                            var seconds = miliseconds / 1000;
                            var minutes = seconds / 60;
                            var hours = minutes / 60;
                            var days = hours / 24;
                            $scope.formattedDataForRecred1.push({ PlanName: data[i].PlanObject.PlanName, Days: parseInt(days) });
                        }

                        //for (var i = 0; i < $scope.formattedDataForRecred1.length; i++) {

                        //    $scope.DisplayData = $scope.formattedDataForRecred1[0];

                        //    if (true) {

                        //    }


                        //}
                        $scope.errorInitiated = true;
                        $scope.loading = false;
                    }
                }

                else {
                    $scope.errorInitiated = false;
                    $scope.loading = false;
                }

            }
        }
        else {
            $scope.errorInitiated = false;
            $scope.loading = false;
        }

    };

    $scope.psvData = [];
    $scope.CallView4 = function () {
        $scope.FormattedData = [];
        if ($scope.errorInitiatedForNoPSV == false) {
            $scope.showPsvError = true;
            $scope.review1 = false;
            $scope.review2 = false;
            $scope.review3 = false;
            $scope.review4 = true;
            $scope.review5 = false;
        }
        else {
            $scope.FormatData($scope.psvInfoNew.psvInfo.ProfileVerificationDetails);
            // $scope.loadData($scope.creddata.CredentialingInfoID);
            $scope.showPsvError = false;
            $scope.review1 = false;
            $scope.review2 = false;
            $scope.review3 = false;
            $scope.review4 = true;
            $scope.review5 = false;
        }
    }

    $scope.FormattedData = [];

    $scope.FormatData = function (data) {
        $scope.FormattedData = [];
        var formattedData = [];
        for (var i in data) {
            var VerificationData = new Object();
            if (data[i].Status != 'Inactive') {
                VerificationData = jQuery.parseJSON(data[i].VerificationData);
                var VerificationDate = $scope.ConvertDateFormat1(data[i].VerificationDate)

                formattedData.push({ Id: data[i].ProfileVerificationParameterId, info: { ProfileVerficationParameterObj: data[i].ProfileVerificationParameter, VerificationResultObj: data[i].VerificationResult, VerificationData: VerificationData, VerificationDate: VerificationDate } });
            }

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
            $scope.FormattedData.push({ Id: UniqueIds[i], Info: info });
        }

        var sequenceData = [];
        var sequenceIds = [1, 2, 3, 4, 5, 6, 7];
        for (var i = 0; i < sequenceIds.length; i++) {
            for (var j = 0; j < $scope.FormattedData.length; j++) {
                if (sequenceIds[i] == $scope.FormattedData[j].Id) {
                    sequenceData.push($scope.FormattedData[j]);
                }
            }
        }

        $scope.FormattedData = sequenceData;

    };

    $scope.ConvertDateFormat1 = function (value) {
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

    $scope.CallView5 = function () {
        if ($scope.NoPlan == true) {
            $scope.NoPlan = true;
            $scope.review1 = false;
            $scope.review2 = false;
            $scope.review3 = false;
            $scope.review4 = false;
            $scope.review5 = true;
            $scope.loading = false;
            messageAlertEngine.callAlertMessage("PlanError", "No plan is selected !!!!", "danger", true);
        }
        else {
            var data = JSON.parse(demographics);
            $scope.ProviderName = angular.copy(data.PersonalDetail.FirstName) + " " + angular.copy(data.PersonalDetail.LastName);
            $scope.error_message = "";
            $scope.searchData = {};
            $scope.loading = true;
            $http({
                method: "POST",
                url: rootDir + "/Credentialing/Initiation/SearchProviderJson?id=" + 1,
                data: {
                    NPINumber: null,
                    FirstName: data.PersonalDetail.FirstName,
                    LastName: null,
                    CAQH: null,
                    IPAGroupName: null,
                    Specialty: null,
                    ProviderType: null
                }
            }).success(function (resultData) {
                try {
                    for (var i = 0; i < resultData.length; i++) {
                        if (resultData[i].ProfileID == profileId) {
                            $scope.searchData = resultData[i];
                        }
                    }
                    $scope.NoPlan = false;
                    $scope.review1 = false;
                    $scope.review2 = false;
                    $scope.review3 = false;
                    $scope.review4 = false;
                    $scope.review5 = true;
                    $scope.loading = false;
                } catch (e) {
                  
                }
            })
              .error(function () {
                  $scope.progressbar = false;
                  $scope.error_message = "An Error occured !! Please Try Again !!";
                  $scope.loading = false;
              })
        }
    }

    $scope.NoPlan = true;
    $scope.selectPlan = function (plan) {
        $scope.selectedPlan = JSON.parse(plan);
        if ($scope.selectedPlan.PlanID != 0) {
            $scope.NoPlan = false;
        }
        else {
            $scope.NoPlan = true;
        }
    }


    //====================Intiation of Decredentialing ===============

    function getDecredplans() {
        $http.get(rootDir + '/Credentialing/Initiation/getPlanListforCredentialingContractRequest?ProviderID=' + $scope.profileID).
             success(function (data, status, headers, config) {
                 try {
                     if (data.status == true) {
                         $scope.PlanListForDeCred = angular.copy(data.data1);

                     }
                 } catch (e) {

                 }
             }).error(function (data, status, headers, config) {

             });

    }
    //for choosing the particular plan for Decred and Fetching that Particular Information
    $scope.NoPlanforDecred = true;
    $scope.selectPlanforDecred = function (plan) {
    
        $scope.selectedPlanforDecred = JSON.parse(plan);
        if ($scope.selectedPlanforDecred.PlanID != 0) {
            $scope.NoPlanforDecred = false;
        }
        else {
            $scope.NoPlanforDecred = true;
        }

    }

    //selecting the LOBs
    $scope.toggleSelectionForDecred = function (CredentialingContractGrid) {
        var idxForDecred = $scope.selectionForDecred.indexOf(CredentialingContractGrid);
        if (idxForDecred > -1) {
            $scope.selectionForDecred.splice(idxForDecred, 1);
        }
        else {
            $scope.selectionForDecred.push(CredentialingContractGrid);
        }
    }


    //Fetching LOBs under that selected plan
    $scope.GetAllLobsForDeCred = function () {
        $scope.ContractGridDetailForDeCred = [];
        $http.get(rootDir + '/Credentialing/Initiation/getCredentialingContractGridForDecred?ProviderID=' + $scope.profileID + '&PlanID=' + $scope.selectedPlanforDecred.PlanID).
            success(function (data, status, headers, config) {
                try {
                   
                    $scope.loadingmessagefordecred = false;
                    for (var i = 0; i < data.length; i++) {
                        if (data[i].ContractGridObject != null) {
                            //var selectedObj = obj.CredentialingContractRequests[i].ContractGrid[j];
                            //$scope.profileDetail.push(selectedObj);
                            var currObj = new Object();
                            currObj.CredentialingInfoID = data[i].CredInfoID;
                            currObj.CredentialingContractRequestID = data[i].CredRequestID;
                            currObj.Speciality = data[i].ContractGridObject.ProfileSpecialty.Specialty.Name;
                            currObj.LOB = data[i].ContractGridObject.LOB.LOBName;
                            currObj.Location = data[i].ContractGridObject.ProfilePracticeLocation.Facility.FacilityName + "-" + data[i].ContractGridObject.ProfilePracticeLocation.Facility.Street + "," + data[i].ContractGridObject.ProfilePracticeLocation.Facility.City + "," + data[i].ContractGridObject.ProfilePracticeLocation.Facility.State + "," + data[i].ContractGridObject.ProfilePracticeLocation.Facility.Country;
                            currObj.Location = currObj.Location;
                            currObj.GroupName = data[i].ContractGridObject.BusinessEntity.GroupName;
                            currObj.Check = false;
                            var inititatedDate = $scope.ConvertDateFormat(data[i].ContractGridObject.InitialCredentialingDate);
                            currObj.InitiationDate = $filter('date')(new Date(inititatedDate), 'MM-dd-yyyy');
                            currObj.ContractGridID = data[i].ContractGridObject.ContractGridID;
                            $scope.ContractGridDetailForDeCred.push(currObj);
                        }
                    }
                    if ($scope.ContractGridDetailForDeCred.length > 0) {
                        $scope.init_table($scope.ContractGridDetailForDeCred, 4);

                    }
                    else if ($scope.ContractGridDetailForDeCred.length == 0) {
                        messageAlertEngine.callAlertMessage('noProviderDetails', "No Plan Report Found for Decredentialing", "danger", true);
                    }

                    $scope.loadingAjax1 = false;
                } catch (e) {

                }
            }).error(function (data, status, headers, config) {
                messageAlertEngine.callAlertMessage('noProviderDetails', "Please Try Again Later", "danger", true);

            })

    }

    //for selecting all functionality
    $scope.selectAllForDecred = function () {
        if ($scope.tempo.allItemsSelectedForDecred == true) {
            $scope.selectionForDecred = [];
            for (var i = 0; i < $scope.ContractGridDetailForDeCred.length; i++) {
                $scope.selectionForDecred.push($scope.ContractGridDetailForDeCred[i]);
                $scope.ContractGridDetailForDeCred[i].Check = $scope.tempo.allItemsSelectedForDecred;
            }
        }
        else {
            $scope.selectionForDecred = [];
            for (var i = 0; i < $scope.ContractGridDetailForDeCred.length; i++) {
                $scope.ContractGridDetailForDeCred[i].Check = $scope.tempo.allItemsSelectedForDecred;
            }
        }
     //   $scope.tableParams4.reload();
    }


    //for fetching intial data
    getDecredplans();


    //$scope.sleep = function (milliseconds) {
    //    var start = new Date().getTime();
    //    for (var i = 0; i < 1e7; i++) {
    //        if ((new Date().getTime() - start) > milliseconds) {
    //            break;
    //        }
    //    }
    //}
    
    $scope.InitiateCredentialing = function () {

        var url = null;

        $scope.roleCCO = $rootScope.ccoprofile;

        if ($scope.roleCCO) {
            var obj = {
                ProfileID: $scope.searchData.ProfileID,
                NPINumber: $scope.searchData.NPINumber,
                CAQHNumber: $scope.searchData.CAQH,
                FirstName: $scope.searchData.FirstName,
                LastName: $scope.searchData.LastName,
                PlanID: $scope.selectedPlan.PlanID,
                IsDelegatedYesNoOption: $scope.selectedPlan.DelegatedType,
                StatusType: 1
            };
            url = '/Credentialing/Initiation/InitiateCredentialing';
        }
        else {
            var obj = {
                CredentialingRequestID: 0,
                ProfileID: $scope.searchData.ProfileID,
                NPINumber: $scope.searchData.NPINumber,
                CAQHNumber: $scope.searchData.CAQH,
                FirstName: $scope.searchData.FirstName,
                LastName: $scope.searchData.LastName,
                PlanID: $scope.selectedPlan.PlanID,
                IsDelegated: $scope.selectedPlan.DelegatedType,
                StatusType: 1
            };
            url = '/Profile/CredentialingRequest/InitiateCredentialingRequest';
        }

        $http.post(rootDir + url, obj).
            success(function (data, status, headers, config) {
                try {
                    //----------- "success" message -----------
                    if (data.status == "true") {
                        if ($scope.roleCCO) {
                            messageAlertEngine.callAlertMessage('successfulInitiated', "Credentialing Initiated successfully. !!!!", "success", true);
                        }
                        else {
                            messageAlertEngine.callAlertMessage('successfulInitiatedProvider', "Credentialing Request sent successfully. !!!!", "success", true);
                        }
                       // $scope.sleep(1000000);
                        
                      //  $scope.review5 = false;
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

   

    //================http ==============
    $scope.checkForHttp = function (value) {
        if (value.indexOf('http') > -1) {
            value = value;
        } else {
            value = 'http://' + value;
        }
        var open_link = window.open('', '_blank');
        open_link.location = value;
    }

    $scope.ShowProfileReview = function () {
        $scope.ProfileReview = true;
    }

    $scope.ShowProfileSummary = function () {
        $scope.ProfileReview = false;
        if ($scope.SectionDisplayData.length == 0) {
            $("#RenewUpdateError").show();
        }
        //$("#RenewUpdateError").hide();
        $scope.review1 = true;
        $scope.review2 = false;
        $scope.review3 = false;
        $scope.review4 = false;
        $scope.review5 = false;
        $scope.loading = true;
        $scope.loading = false;

    }

    //================ for profile summary===============

    $scope.temp = '';
    $scope.FilterProviders = [];

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
        // $scope.GetAllProfileUpdates($scope.FormattedDataForSection[1].UpdateData);
    };

    $scope.GetAllProfileUpdates = function (data) {
        $scope.SectionDisplayData = [];
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

            for (var i = 0; i < UniqueSubSections.length; i++) {
                var updatesubsectionwiseData = [];
                for (var j = 0; j < data.length; j++) {
                    if (UniqueSubSections[i] == data[j].SubsectionName) {

                        updatesubsectionwiseData.push({ profiletrackerId: data[j].profiletrackerId, status: data[j].AprovalStatus });

                    }
                }
                formattedDataforsubsection.push({ SubSectionName: UniqueSubSections[i], ProfileTrackerIds: updatesubsectionwiseData });
            }


            var testData = [];
            for (var i = 0; i < formattedDataforsubsection.length; i++) {

                var trackerIds = [];
                var trackerStatus = [];
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
            $scope.SectionDisplayData = testData;

            //========================================

            for (var i = 0; i < $scope.SectionDisplayData.length; i++) {
                for (var j = 0; j < $scope.SectionDisplayData[i].fields.length; j++) {


                    var newvalue = $scope.SectionDisplayData[i].fields[j];

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

                    if ($scope.SectionDisplayData[i].subsectionName == 'State License') {

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
                                data = JSON.parse(data);
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

                    if ($scope.SectionDisplayData[i].fields[j].FieldName == 'InsuranceCoverageType') {
                        $scope.SectionDisplayData[i].fields.splice($scope.SectionDisplayData[i].fields.indexOf($scope.SectionDisplayData[i].fields[j]), 1);
                        j--;
                    }
                    if ($scope.SectionDisplayData[i].fields[j].FieldName == 'PrimaryTax') {
                        $scope.SectionDisplayData[i].fields.splice($scope.SectionDisplayData[i].fields.indexOf($scope.SectionDisplayData[i].fields[j]), 1);
                        j--;
                    }
                    if ($scope.SectionDisplayData[i].fields[j].FieldName == 'SpecialtyBoardNotCertifiedDetailID') {
                        $scope.SectionDisplayData[i].fields.splice($scope.SectionDisplayData[i].fields.indexOf($scope.SectionDisplayData[i].fields[j]), 1);
                        j--;
                    }
                    if ($scope.SectionDisplayData[i].fields[j].FieldName == 'SpecialtyBoardExamStatus') {
                        $scope.SectionDisplayData[i].fields.splice($scope.SectionDisplayData[i].fields.indexOf($scope.SectionDisplayData[i].fields[j]), 1);
                        j--;
                    }

                    if ($scope.SectionDisplayData[i].fields[j].FieldName == 'MedicaidInformationID') {
                        $scope.SectionDisplayData[i].fields.splice($scope.SectionDisplayData[i].fields.indexOf($scope.SectionDisplayData[i].fields[j]), 1);
                        j--;
                    }
                    if ($scope.SectionDisplayData[i].subsectionName == 'Specialty Details') {
                        if ($scope.SectionDisplayData[i].fields[j].FieldName == 'SpecialtyBoardCertifiedDetail') {
                            $scope.SectionDisplayData[i].fields.splice($scope.SectionDisplayData[i].fields.indexOf($scope.SectionDisplayData[i].fields[j]), 1);

                        }
                        if ($scope.SectionDisplayData[i].fields[j].FieldName == 'SpecialtyBoardNotCertifiedDetail') {
                            $scope.SectionDisplayData[i].fields.splice($scope.SectionDisplayData[i].fields.indexOf($scope.SectionDisplayData[i].fields[j]), 1);

                        }
                    }

                    if ($scope.SectionDisplayData[i].subsectionName == 'Practice Location Detail') {
                        if ($scope.SectionDisplayData[i].fields[j].FieldName == 'OpenPracticeStatus') {
                            $scope.SectionDisplayData[i].fields.splice($scope.SectionDisplayData[i].fields.indexOf($scope.SectionDisplayData[i].fields[j]), 1);
                        }

                        if ($scope.SectionDisplayData[i].fields[j].FieldName == 'WorkersCompensationInformation') {
                            $scope.SectionDisplayData[i].fields.splice($scope.SectionDisplayData[i].fields.indexOf($scope.SectionDisplayData[i].fields[j]), 1);
                        }

                        if ($scope.SectionDisplayData[i].fields[j].FieldName == 'BusinessOfficeManagerOrStaffId') {
                            $scope.SectionDisplayData[i].fields.splice($scope.SectionDisplayData[i].fields.indexOf($scope.SectionDisplayData[i].fields[j]), 1);
                        }

                        if ($scope.SectionDisplayData[i].fields[j].FieldName == 'PaymentAndRemittanceId') {
                            $scope.SectionDisplayData[i].fields.splice($scope.SectionDisplayData[i].fields.indexOf($scope.SectionDisplayData[i].fields[j]), 1);
                        }
                        if ($scope.SectionDisplayData[i].fields[j].FieldName == 'BillingContactPersonId') {
                            $scope.SectionDisplayData[i].fields.splice($scope.SectionDisplayData[i].fields.indexOf($scope.SectionDisplayData[i].fields[j]), 1);
                        }
                        if ($scope.SectionDisplayData[i].fields[j].FieldName == 'PrimaryCredentialingContactPersonId') {
                            $scope.SectionDisplayData[i].fields.splice($scope.SectionDisplayData[i].fields.indexOf($scope.SectionDisplayData[i].fields[j]), 1);
                        }
                    }
                }
            }
            //========================================
            $scope.showUpdateError = false;

            for (var i = 0; i < $scope.SectionDisplayData.length; i++) {

                for (var j = 0; j < $scope.SectionDisplayData[i].fields.length; j++) {
                    $scope.SectionDisplayData[i].fields[j].FieldName = AddSpacesInWords($scope.SectionDisplayData[i].fields[j].FieldName);
                }

            }

        } else {
            $scope.SectionDisplayData = [];
        }
    }

    var AddSpacesInWords = function (input) {
        var newString = "";
        var wasUpper = false;
        for (var i = 0; i < input.length; i++) {
            if (!wasUpper && input[i] == input.toUpperCase()[i]) {
                newString = newString + " ";
                wasUpper = true;
            }
            else {
                wasUpper = false;
            }
            newString = newString + input[i];
        }
        return newString;
    };

    $scope.SectionDisplayData = [];

    $scope.openDiv = function (className, idName) {
        $scope.subClassName = className;
        $scope.subIdName = idName;

        $('.' + className).hide();
        $('#' + idName).show();
    }

    $scope.closeDiv = function (className, idName) {

        //$('.' + className).hide();
        $('#' + idName).hide();
    }

    //====================================================
}]);



