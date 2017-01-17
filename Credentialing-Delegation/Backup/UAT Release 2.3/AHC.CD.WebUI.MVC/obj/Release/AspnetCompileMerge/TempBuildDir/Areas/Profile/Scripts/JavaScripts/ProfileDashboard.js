//------------------------------- Provider License Service --------------------
profileApp.service('ProviderLicenseService', function ($filter) {

    var data = [];
    var GrandTotalLicenses = 0;

    var GrandTotalValidLicense = 0;
    var GrandTotalPendingDaylicense = 0;
    var GrandTotalExpiredLicense = 0;

    this.GetFormattedProfileData = function (expiredLicense) {
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

        //------------------- left day calculate ----------------------
        for (var i in data) {
            if (data[i].Licenses && (data[i].LicenseType == "State License" || data[i].LicenseType == "Federal DEA" || data[i].LicenseType == "CDS Information" || data[i].LicenseType == "Specialty/Board")) {
                for (var j in data[i].Licenses) {
                    data[i].Licenses[j].ExpiryDate = ConvertDateFormat(data[i].Licenses[j].ExpiryDate);
                    data[i].Licenses[j].dayLeft = GetRenewalDayLeft(data[i].Licenses[j].ExpiryDate);
                }
            } else if (data[i].Licenses && data[i].LicenseType == "Hospital Privileges") {
                for (var j in data[i].Licenses) {
                    data[i].Licenses[j].AffiliationEndDate = ConvertDateFormat(data[i].Licenses[j].AffiliationEndDate);
                    data[i].Licenses[j].dayLeft = GetRenewalDayLeft(data[i].Licenses[j].AffiliationEndDate);
                }
            } else if (data[i].LicenseType == "Professional Liability" || data[i].Licenses && data[i].LicenseType == "Worker Compensation") {
                for (var j in data[i].Licenses) {
                    data[i].Licenses[j].ExpirationDate = ConvertDateFormat(data[i].Licenses[j].ExpirationDate);
                    data[i].Licenses[j].dayLeft = GetRenewalDayLeft(data[i].Licenses[j].ExpirationDate);
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

profileApp.controller('profileAppCtrl', ['$scope', '$rootScope', '$http', 'masterDataService', 'messageAlertEngine', '$filter', 'ProviderLicenseService', function ($scope, $rootScope, $http, masterDataService, messageAlertEngine, $filter,ProviderLicenseService) {

    $scope.profileID = profileId;

    $scope.IsMissingCredentialing = false;
    $scope.IsUpcomingRenewal = false;
    $scope.IsUpcomingReCredentialing = false;
    $scope.IsPsvDone = false;
    $scope.IsInitiateCredentialing = false;
    $scope.loading = false;
    //$scope.errorInitiatedForNoPSV = false;

    //$rootScope.$broadcast("ProfileDashBoard");

    $rootScope.$on('ProfileDashboard', function () {
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

        //================================================

        //------------- get List Of Plan from master data ---------------
        $http.get(rootDir + '/MasterDataNew/GetAllPlans').
       success(function (data, status, headers, config) {
           $scope.Plans = data;
           $scope.data = data;
           $scope.progressbar = false;
       }).
       error(function (data, status, headers, config) {
           //console.log("Sorry internal master data cont able to fetch.");
       });

        //------------- get profile data of those section which is required for credentialing ---------------

        $scope.loading = true;
        $http.get(rootDir + '/NewQuickUpdate/TestActionMethod?profileId=' + profileId).
        success(function (data, status, headers, config) {
            $scope.providerdata = angular.copy(data.profilereviewdata);
            if ($scope.providerdata != null) {
                if ($scope.providerdata.HospitalPrivileges != null) {
                    $scope.HospitalDetailCount = 0;
                    for (var i = 0; i < $scope.providerdata.HospitalPrivileges.HospitalPrivilegeDetails.length; i++) {
                        if ($scope.providerdata.HospitalPrivileges.HospitalPrivilegeDetails[i].Status == "Active") {
                            $scope.HospitalDetailCount = $scope.HospitalDetailCount + 1;
                        }
                    }
                }
                else {
                    $scope.HospitalDetailCount = 0;
                }

                if ($scope.providerdata.PracticeLocationDetails.length != 0) {

                    $scope.CoveringCount = 0;

                    for (var i = 0; i < $scope.providerdata.PracticeLocationDetails.length; i++) {

                        if ($scope.providerdata.PracticeLocationDetails[i].length != 0) {

                            for (var j = 0; j < $scope.providerdata.PracticeLocationDetails[i].PracticeProviders.length; j++) {

                                if ($scope.providerdata.PracticeLocationDetails[i].PracticeProviders[j].Practice == "CoveringColleague" && $scope.providerdata.PracticeLocationDetails[i].PracticeProviders[j].Status == "Active") {

                                    $scope.CoveringCount = $scope.CoveringCount + 1;

                                }

                            }

                        }

                    }

                }
                else {
                    $scope.PracticeCount = 0;
                    $scope.CoveringCount = 0;
                }

                if ($scope.providerdata.ContractGroupInformation.length != 0) {
                    $scope.ContactGroupCount = 0;
                    for (var i = 0; i < $scope.providerdata.ContractGroupInformation.length; i++) {
                        if ($scope.providerdata.ContractGroupInformation[i].ContractGroupInfoes.length != 0) {
                            for (var j = 0; j < $scope.providerdata.ContractGroupInformation[i].ContractGroupInfoes.length; j++) {
                                if ($scope.providerdata.ContractGroupInformation[i].ContractGroupInfoes[j].ContractGroupStatus == "Accepted" && $scope.providerdata.ContractGroupInformation[i].ContractGroupInfoes[j].Status == "Active") {

                                    $scope.ContactGroupCount = $scope.ContactGroupCount + 1;

                                }

                            }
                        }
                    }
                }
                else {
                    $scope.GroupCount = 0;
                    $scope.ContactGroupCount = 0;
                }
                var count = 0;
                $scope.countmissingsection = 0;
                if ($scope.providerdata.PersonalDetail == null) {
                    count = count + 1;
                }
                if ($scope.providerdata.HomeAddresses.length == 0) {
                    count = count + 1;
                }
                if ($scope.providerdata.StateLicenses.length == 0) {
                    count = count + 1;
                }
                if ($scope.providerdata.FederalDEALicenses.length == 0) {
                    count = count + 1;
                }
                if ($scope.providerdata.CDSInformations.length == 0) {
                    count = count + 1;
                }
                if ($scope.providerdata.SpecialtyDetails.length == 0) {
                    count = count + 1;
                }
                if ($scope.PracticeCount == 0) {
                    count = count + 3;
                } else {
                    if ($scope.CoveringCount == 0) {
                        count = count + 1;
                    }
                }
                if ($scope.HospitalDetailCount == 0) {
                    count = count + 1;
                }
                if ($scope.providerdata.WorkGaps.length == 0) {
                    count = count + 1;
                }
                if ($scope.providerdata.CV.CVDocumentPath == null) {
                    count = count + 1;
                }
                if ($scope.GroupCount == 0) {
                    count = count + 1;
                } else {
                    if ($scope.ContactGroupCount == 0) {
                        count = count + 1;
                    }
                }
                $scope.countmissingsection = count;
                $scope.loading = false;
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
                if (data.status == true) {
                    $scope.PlanList = angular.copy(data.data1);
                    for (var i = 0; i < $scope.PlanList.length; i++) {
                        $scope.PlanListIDs.push($scope.PlanList[i].PlanID);
                    }

                    if ($scope.PlanListIDs.length != 0) {
                        $http.get(rootDir + '/Credentialing/Initiation/getCredentialingContractRequestForAllPlan', { params: { "ProviderID": profileId, "PlanIDs": $scope.PlanListIDs } }).
                            success(function (data, status, headers, config) {
                                //$scope.errorInitiated = true;
                                if (data.status == true) {
                                    if (data.data1.length > 0) {

                                   
                                    $scope.Credinfo = angular.copy(data.data1);
                                    for (var i = 0; i < $scope.Credinfo.length; i++) {
                                        if ($scope.Credinfo[i].CredentialingContractRequests.length != 0) {
                                            for (var j = 0; j < $scope.Credinfo[i].CredentialingContractRequests.length; j++) {
                                                if ($scope.Credinfo[i].CredentialingContractRequests.length != 0) {
                                                    var CredentialingContract = [];
                                                    if ($scope.Credinfo[i].CredentialingContractRequests[j].Status == 'Active' && $scope.Credinfo[i].CredentialingContractRequests[j].ContractRequestStatus == 'Active') {

                                                        if ($scope.Credinfo[i].CredentialingContractRequests[j].ContractGrid.length > 0) {

                                                            for (var k = 0; k < $scope.Credinfo[i].CredentialingContractRequests[j].ContractGrid.length; k++) {
                                                                if ($scope.Credinfo[i].CredentialingContractRequests[j].ContractGrid[k].Status == 'Active' && $scope.Credinfo[i].CredentialingContractRequests[j].ContractGrid[k].Report != null) {
                                                                    if ($scope.Credinfo[i].CredentialingContractRequests[j].ContractGrid[k].Report.ReCredentialingDate != null) {
                                                                        var d1 = new Date();
                                                                        var d2 = new Date($scope.ConvertDateFormat1($scope.Credinfo[i].CredentialingContractRequests[j].ContractGrid[k].Report.ReCredentialingDate));
                                                                        var miliseconds = d2 - d1;
                                                                        var seconds = miliseconds / 1000;
                                                                        var minutes = seconds / 60;
                                                                        var hours = minutes / 60;
                                                                        var days = hours / 24;
                                                                        if (days < 180) {

                                                                            CredentialingContract.push($scope.Credinfo[i].CredentialingContractRequests[j]);
                                                                            $scope.CredentialingContractRequests.push($scope.Credinfo[i].CredentialingContractRequests[j]);
                                                                            break;
                                                                        }
                                                                    }
                                                                }
                                                            }
                                                        }

                                                    }
                                                    if (CredentialingContract.length != 0) {
                                                        $scope.structuredata.push({ PlanObject: $scope.Credinfo[i].Plan, CredentialingContractRequest: CredentialingContract });
                                                    }
                                                }
                                                else {
                                                    $scope.errorInitiated = false;
                                                }
                                            }
                                        }
                                        if ($scope.structuredata.length != 0) {
                                            $scope.errorInitiated = true;
                                        }
                                    }
                                    }
                                    else {
                                        $scope.formattedDataForRecred1 = [];
                                    }
                                    $scope.loadingAjax1 = false;
                                    if ($scope.CredentialingContractRequests.length == 0) {
                                        messageAlertEngine.callAlertMessage('errorInitiated', "", "danger", true);
                                        $scope.errorInitiated = "Provider is not credentialed for any plan";
                                        $scope.errorInitiated = false;
                                    }
                                    $scope.NotSelected = angular.copy($scope.CredentialingContractRequests);
                                }
                                $scope.FormatDataForRecredentialing($scope.structuredata);
                                //$scope.review1 = false;
                                //$scope.review2 = false;
                                //$scope.review3 = true;
                                //$scope.review4 = false;
                                //$scope.review5 = false;
                                $scope.loading = false;

                            }).error(function (data, status, headers, config) {
                                $scope.loading = false;
                            });
                    }
                    else {
                        //$scope.review1 = false;
                        //$scope.review2 = false;
                        //$scope.review3 = true;
                        //$scope.review4 = false;
                        //$scope.review5 = false;
                        $scope.errorInitiated = false;
                        $scope.loading = false;
                    }


                }
            })
            .error(function (data, status, headers, config) {
            });

        $http.get(rootDir + '/Profile/MasterData/GetAllProfileVerificationParameter').
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
               });

        $http.get(rootDir + '/Dashboard/GetAllExpiresForAProvider?profileId=' + profileId).
            success(function (data, status, headers, config) {
                $scope.tempData = ProviderLicenseService.GetFormattedProfileData(data.data);
                $scope.GrandTotal = ProviderLicenseService.GetGrandTotalLicenseStatus();
            })
                .error(function (data, status, headers, config) {
                    $scope.loading = false;
                });


        $http.get(rootDir + '/Credentialing/Verification/GetPSVDetailsForAProvider?profileId=' + profileId).
     success(function (data, status, headers, config) {
         if (data.status == 'true') {
             if (data.psvInfo != null) { 
             if (data.psvInfo.ProfileVerificationDetails.length != 0) {
                 $scope.errorInitiatedForNoPSV = true;
                 $scope.psvInfoNew = angular.copy(data);
                 console.log("PSV Data");
                 console.log($scope.psvInfoNew);
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

     })
     .error(function (data, status, headers, config) {
     });


        $http.get(rootDir + '/Profile/MasterProfile/GetUpdatesById?profileId=' + profileId).
    success(function (data, status, headers, config) {
        if (data.length != 0) {
            $scope.FormatDataForSection(data);
        }
        else {
            $scope.showUpdateError = true;
        }


    })
    .error(function (data, status, headers, config) {
    });


    });
    $scope.review1 = true;
    $scope.ProfileReview = true;

    $scope.miss = false;

    $scope.callViewDemograhic = function () {
        $('.tab-label').removeClass('active');
        $('.tab-pane').removeClass('active');
        $('#active').addClass('active');
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
        $('#profile').addClass('active');
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
        $scope.formattedDataForRecred1 = [];
        $scope.creddata = data[0].CredentialingContractRequest;
        var formattedDataForRecred = [];
     
        for (var i = 0; i < data.length; i++) {

            for (var j = 0; j < data[i].CredentialingContractRequest.length; j++) {
                if ($scope.creddata.LastModifiedDate < data[i].CredentialingContractRequest[j].LastModifiedDate) {
                    $scope.creddata = data[i].CredentialingContractRequest[j];
                }
            }
            formattedDataForRecred.push({ PlanName: data[i].PlanObject.PlanName, ReCredData: $scope.creddata });
        }

        for (var i = 0; i < formattedDataForRecred.length; i++) {
            var d1 = new Date();
            var d2 = new Date($scope.ConvertDateFormat1(formattedDataForRecred[i].ReCredData[0].InitialCredentialingDate));
            var d3 = new Date(new Date(d2).setMonth(new Date(d2).getMonth() + 24));
            var miliseconds = d3 - d1;
            var seconds = miliseconds / 1000;
            var minutes = seconds / 60;
            var hours = minutes / 60;
            var days = hours / 24;
            $scope.formattedDataForRecred1.push({ PlanName: formattedDataForRecred[i].PlanName, Days: parseInt(days) });
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
                //----------- success message -----------
                if (data.status == "true") {
                    if ($scope.roleCCO) {
                        messageAlertEngine.callAlertMessage('successfulInitiated', "Credentialing Initiated Successfully. !!!!", "success", true);
                    }
                    else {
                        messageAlertEngine.callAlertMessage('successfulInitiatedProvider', "Credentialing Request sent Successfully. !!!!", "success", true);
                    }
                    
                }
                else {
                    messageAlertEngine.callAlertMessage('errorInitiated1', "", "danger", true);
                    $scope.errorInitiated = data.status.split(",");
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



