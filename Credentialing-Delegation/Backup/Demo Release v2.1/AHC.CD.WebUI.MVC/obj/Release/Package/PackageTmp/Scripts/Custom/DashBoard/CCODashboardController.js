
//------------------------------- Provider License Service --------------------
dashboardApp.service('CCOLicenseService', function ($filter) {

    var ProviderState = [];
    var ProviderDEA = [];
    var ProviderCDS = [];
    var ProviderSpeciality = [];
    var ProviderHospital = [];
    var ProviderLiability = [];
    var ProviderWorker = [];

    //console.log(expiredLicenses);

    //---------------- parse data in license have Licenses with calculate Left Day ----------------------
    for (var i in expiredLicenses) {
        if (expiredLicenses[i].StateLicenseExpiries && expiredLicenses[i].StateLicenseExpiries.length > 0) {
            for (var j in expiredLicenses[i].StateLicenseExpiries) {
                expiredLicenses[i].StateLicenseExpiries[j].ExpiryDate = ConvertDateFormat(expiredLicenses[i].StateLicenseExpiries[j].ExpiryDate);
                expiredLicenses[i].StateLicenseExpiries[j].dayLeft = GetRenewalDayLeft(expiredLicenses[i].StateLicenseExpiries[j].ExpiryDate);

                ProviderState.push({
                    EmailAddress: expiredLicenses[i].EmailAddress,
                    ExpiryNotificationDetailID: expiredLicenses[i].ExpiryNotificationDetailID,
                    FirstName: expiredLicenses[i].FirstName,
                    LastName: expiredLicenses[i].LastName,
                    MiddleName: expiredLicenses[i].MiddleName,
                    LastModifiedDate: expiredLicenses[i].LastModifiedDate,
                    NPINumber: expiredLicenses[i].NPINumber,
                    ProfileId: expiredLicenses[i].ProfileId,
                    ProviderLevel: expiredLicenses[i].ProviderLevel,
                    ProviderTitles: expiredLicenses[i].ProviderTitles,
                    ProviderTypes: expiredLicenses[i].ProviderTypes,
                    License: expiredLicenses[i].StateLicenseExpiries[j]
                });
            }
        }

        if (expiredLicenses[i].DEALicenseExpiries && expiredLicenses[i].DEALicenseExpiries.length > 0) {
            for (var j in expiredLicenses[i].DEALicenseExpiries) {
                expiredLicenses[i].DEALicenseExpiries[j].ExpiryDate = ConvertDateFormat(expiredLicenses[i].DEALicenseExpiries[j].ExpiryDate);
                expiredLicenses[i].DEALicenseExpiries[j].dayLeft = GetRenewalDayLeft(expiredLicenses[i].DEALicenseExpiries[j].ExpiryDate);

                ProviderDEA.push({
                    EmailAddress: expiredLicenses[i].EmailAddress,
                    ExpiryNotificationDetailID: expiredLicenses[i].ExpiryNotificationDetailID,
                    FirstName: expiredLicenses[i].FirstName,
                    LastName: expiredLicenses[i].LastName,
                    MiddleName: expiredLicenses[i].MiddleName,
                    LastModifiedDate: expiredLicenses[i].LastModifiedDate,
                    NPINumber: expiredLicenses[i].NPINumber,
                    ProfileId: expiredLicenses[i].ProfileId,
                    ProviderLevel: expiredLicenses[i].ProviderLevel,
                    ProviderTitles: expiredLicenses[i].ProviderTitles,
                    ProviderTypes: expiredLicenses[i].ProviderTypes,
                    License: expiredLicenses[i].DEALicenseExpiries[j]
                });
            }
        }

        if (expiredLicenses[i].CDSCInfoExpiries && expiredLicenses[i].CDSCInfoExpiries.length > 0) {
            for (var j in expiredLicenses[i].CDSCInfoExpiries) {
                expiredLicenses[i].CDSCInfoExpiries[j].ExpiryDate = ConvertDateFormat(expiredLicenses[i].CDSCInfoExpiries[j].ExpiryDate);
                expiredLicenses[i].CDSCInfoExpiries[j].dayLeft = GetRenewalDayLeft(expiredLicenses[i].CDSCInfoExpiries[j].ExpiryDate);

                ProviderCDS.push({
                    EmailAddress: expiredLicenses[i].EmailAddress,
                    ExpiryNotificationDetailID: expiredLicenses[i].ExpiryNotificationDetailID,
                    FirstName: expiredLicenses[i].FirstName,
                    LastName: expiredLicenses[i].LastName,
                    MiddleName: expiredLicenses[i].MiddleName,
                    LastModifiedDate: expiredLicenses[i].LastModifiedDate,
                    NPINumber: expiredLicenses[i].NPINumber,
                    ProfileId: expiredLicenses[i].ProfileId,
                    ProviderLevel: expiredLicenses[i].ProviderLevel,
                    ProviderTitles: expiredLicenses[i].ProviderTitles,
                    ProviderTypes: expiredLicenses[i].ProviderTypes,
                    License: expiredLicenses[i].CDSCInfoExpiries[j]
                });
            }
        }

        if (expiredLicenses[i].SpecialtyDetailExpiries && expiredLicenses[i].SpecialtyDetailExpiries.length > 0) {
            for (var j in expiredLicenses[i].SpecialtyDetailExpiries) {
                expiredLicenses[i].SpecialtyDetailExpiries[j].ExpiryDate = ConvertDateFormat(expiredLicenses[i].SpecialtyDetailExpiries[j].ExpiryDate);
                expiredLicenses[i].SpecialtyDetailExpiries[j].dayLeft = GetRenewalDayLeft(expiredLicenses[i].SpecialtyDetailExpiries[j].ExpiryDate);

                ProviderSpeciality.push({
                    EmailAddress: expiredLicenses[i].EmailAddress,
                    ExpiryNotificationDetailID: expiredLicenses[i].ExpiryNotificationDetailID,
                    FirstName: expiredLicenses[i].FirstName,
                    LastName: expiredLicenses[i].LastName,
                    MiddleName: expiredLicenses[i].MiddleName,
                    LastModifiedDate: expiredLicenses[i].LastModifiedDate,
                    NPINumber: expiredLicenses[i].NPINumber,
                    ProfileId: expiredLicenses[i].ProfileId,
                    ProviderLevel: expiredLicenses[i].ProviderLevel,
                    ProviderTitles: expiredLicenses[i].ProviderTitles,
                    ProviderTypes: expiredLicenses[i].ProviderTypes,
                    License: expiredLicenses[i].SpecialtyDetailExpiries[j]
                });
            }
        }

        if (expiredLicenses[i].HospitalPrivilegeExpiries && expiredLicenses[i].HospitalPrivilegeExpiries.length > 0) {
            for (var j in expiredLicenses[i].HospitalPrivilegeExpiries) {
                expiredLicenses[i].HospitalPrivilegeExpiries[j].AffiliationEndDate = ConvertDateFormat(expiredLicenses[i].HospitalPrivilegeExpiries[j].AffiliationEndDate);
                expiredLicenses[i].HospitalPrivilegeExpiries[j].dayLeft = GetRenewalDayLeft(expiredLicenses[i].HospitalPrivilegeExpiries[j].AffiliationEndDate);

                ProviderHospital.push({
                    EmailAddress: expiredLicenses[i].EmailAddress,
                    ExpiryNotificationDetailID: expiredLicenses[i].ExpiryNotificationDetailID,
                    FirstName: expiredLicenses[i].FirstName,
                    LastName: expiredLicenses[i].LastName,
                    MiddleName: expiredLicenses[i].MiddleName,
                    LastModifiedDate: expiredLicenses[i].LastModifiedDate,
                    NPINumber: expiredLicenses[i].NPINumber,
                    ProfileId: expiredLicenses[i].ProfileId,
                    ProviderLevel: expiredLicenses[i].ProviderLevel,
                    ProviderTitles: expiredLicenses[i].ProviderTitles,
                    ProviderTypes: expiredLicenses[i].ProviderTypes,
                    License: expiredLicenses[i].HospitalPrivilegeExpiries[j]
                });
            }
        }

        if (expiredLicenses[i].ProfessionalLiabilityExpiries && expiredLicenses[i].ProfessionalLiabilityExpiries.length > 0) {
            for (var j in expiredLicenses[i].ProfessionalLiabilityExpiries) {
                expiredLicenses[i].ProfessionalLiabilityExpiries[j].ExpirationDate = ConvertDateFormat(expiredLicenses[i].ProfessionalLiabilityExpiries[j].ExpirationDate);
                expiredLicenses[i].ProfessionalLiabilityExpiries[j].dayLeft = GetRenewalDayLeft(expiredLicenses[i].ProfessionalLiabilityExpiries[j].ExpirationDate);

                ProviderLiability.push({
                    EmailAddress: expiredLicenses[i].EmailAddress,
                    ExpiryNotificationDetailID: expiredLicenses[i].ExpiryNotificationDetailID,
                    FirstName: expiredLicenses[i].FirstName,
                    LastName: expiredLicenses[i].LastName,
                    MiddleName: expiredLicenses[i].MiddleName,
                    LastModifiedDate: expiredLicenses[i].LastModifiedDate,
                    NPINumber: expiredLicenses[i].NPINumber,
                    ProfileId: expiredLicenses[i].ProfileId,
                    ProviderLevel: expiredLicenses[i].ProviderLevel,
                    ProviderTitles: expiredLicenses[i].ProviderTitles,
                    ProviderTypes: expiredLicenses[i].ProviderTypes,
                    License: expiredLicenses[i].ProfessionalLiabilityExpiries[j]
                });
            }
        }

        if (expiredLicenses[i].WorkerCompensationExpiries && expiredLicenses[i].WorkerCompensationExpiries.length > 0) {
            for (var j in expiredLicenses[i].WorkerCompensationExpiries) {
                expiredLicenses[i].WorkerCompensationExpiries[j].ExpirationDate = ConvertDateFormat(expiredLicenses[i].WorkerCompensationExpiries[j].ExpirationDate);
                expiredLicenses[i].WorkerCompensationExpiries[j].dayLeft = GetRenewalDayLeft(expiredLicenses[i].WorkerCompensationExpiries[j].ExpirationDate);

                ProviderWorker.push({
                    EmailAddress: expiredLicenses[i].EmailAddress,
                    ExpiryNotificationDetailID: expiredLicenses[i].ExpiryNotificationDetailID,
                    FirstName: expiredLicenses[i].FirstName,
                    LastName: expiredLicenses[i].LastName,
                    MiddleName: expiredLicenses[i].MiddleName,
                    LastModifiedDate: expiredLicenses[i].LastModifiedDate,
                    NPINumber: expiredLicenses[i].NPINumber,
                    ProfileId: expiredLicenses[i].ProfileId,
                    ProviderLevel: expiredLicenses[i].ProviderLevel,
                    ProviderTitles: expiredLicenses[i].ProviderTitles,
                    ProviderTypes: expiredLicenses[i].ProviderTypes,
                    License: expiredLicenses[i].WorkerCompensationExpiries[j]
                });
            }
        }
    }

    //-------------------------- Customm array parse ---------------------
    var LicenseData = [];
    var GrandTotalLicenses = 0;

    if (ProviderState) {
        LicenseData.push({
            LicenseType: "State License",
            LicenseTypeCode:  "StateLicense",
            Licenses: ProviderState
        });
    }
    if (ProviderDEA) {
        LicenseData.push({
            LicenseType: "Federal DEA",
            LicenseTypeCode: "FederalDEA",
            Licenses: ProviderDEA
        });
    }
    if (ProviderCDS) {
        LicenseData.push({
            LicenseType: "CDS Information",
            LicenseTypeCode: "CDSInformation",
            Licenses: ProviderCDS
        });
    }
    if (ProviderSpeciality) {
        LicenseData.push({
            LicenseType: "Specialty/Board",
            LicenseTypeCode: "SpecialityBoard",
            Licenses: ProviderSpeciality
        });
    }
    if (ProviderHospital) {
        LicenseData.push({
            LicenseType: "Hospital Privileges",
            LicenseTypeCode: "HospitalPrivilages",
            Licenses: ProviderHospital
        });
    }
    if (ProviderLiability) {
        LicenseData.push({
            LicenseType: "Professional Liability",
            LicenseTypeCode: "ProfessionalLiability",
            Licenses: ProviderLiability
        });
    }
    if (ProviderWorker) {
        LicenseData.push({
            LicenseType: "Worker Compensation",
            LicenseTypeCode: "WorkerCompensation",
            Licenses: ProviderWorker
        });
    }

    //--------------- Master license Data for Static Data ---------------
    var MasterLicenseData = angular.copy(LicenseData);
    //console.log(MasterLicenseData);
    //---------------------- license status return -------------------
    this.GetLicenseStatus = function (data) {
        GrandTotalLicenses = 0;

        for (var i in data) {

            var ValidatedLicense = 0;
            var dayLeftLicense = 0;
            var ExpiredLicense = 0;

            var TotalLicenses = 0;

            for (var j in data[i].Licenses) {
                TotalLicenses++;
                if (data[i].Licenses[j].License.dayLeft < 0) {
                    ExpiredLicense++;
                    GrandTotalLicenses++;
                } else if (data[i].Licenses[j].License.dayLeft < 90) {
                    dayLeftLicense++;
                    GrandTotalLicenses++;
                }
                else if (data[i].Licenses[j].License.dayLeft < 180) {
                    ValidatedLicense++;
                    GrandTotalLicenses++;
                }
            }

            var orderBy = $filter('orderBy');
            data[i].Licenses = orderBy(data[i].Licenses, 'License.dayLeft', false);

            data[i].LicenseStatus = {
                ValidLicense: ValidatedLicense,
                PendingDaylicense: dayLeftLicense,
                ExpiredLicense: ExpiredLicense
            };
            data[i].TotalLicenses = TotalLicenses;
        }
    };

    //------------------ Grand Total Number of License return ---------------------
    this.GetGrandTotalLicenses = function () {
        return GrandTotalLicenses;
    };

    //----------------- simply return Licese List ---------------
    this.LicensesList = function () {
        this.GetLicenseStatus(LicenseData);
        return LicenseData;
    };
    //----------------- License by day lefts ------------------------
    this.GetLicenseByDayLeft = function (days) {
        if (days > 0) {
            var temp = [];
            for (var i in MasterLicenseData) {
                var licenses = [];
                for (var j in MasterLicenseData[i].Licenses) {
                    if (MasterLicenseData[i].Licenses[j].License.dayLeft <= days) {
                        licenses.push(MasterLicenseData[i].Licenses[j]);
                    }
                }
                temp.push({ LicenseType: MasterLicenseData[i].LicenseType, LicenseTypeCode: MasterLicenseData[i].LicenseTypeCode, Licenses: licenses });
            }
            this.GetLicenseStatus(temp);
            return temp
        }
        else {
            this.GetLicenseStatus(MasterLicenseData);
            return angular.copy(MasterLicenseData);
        }
    };

    //----------------- License by Provider Types lefts ------------------------
    this.GetLicenseByProviderType = function (providertype, providerlevel, masterLicenses) {
        //--------------- provider type undefined -----------
        if (typeof providertype === "undefined") {
            providertype = "";
        }
        //--------------- provider Level undefined -----------
        if (typeof providerlevel === "undefined") {
            providerlevel = "";
        }
        if (providertype != "" || providerlevel != "") {
            if (providertype != "" && providerlevel != "") {
                var temp = [];
                for (var i in masterLicenses) {
                    var status = false;
                    for (var j in masterLicenses[i].ProviderTitles) {
                        if (masterLicenses[i].ProviderTitles[j] == providertype && masterLicenses[i].ProviderLevel == providerlevel) {
                            status = true;
                        }
                    }
                    if (status) {
                        temp.push(masterLicenses[i]);
                    }
                }
                return temp;
            } else if (providertype != "" && providerlevel == "") {
                var temp = [];
                for (var i in masterLicenses) {
                    var status = false;
                    for (var j in masterLicenses[i].ProviderTitles) {
                        if (masterLicenses[i].ProviderTitles[j] == providertype) {
                            status = true;
                        }
                    }
                    if (status) {
                        temp.push(masterLicenses[i]);
                    }
                }
                return temp;
            } else if (providertype == "" && providerlevel != "") {
                var temp = [];
                for (var i in masterLicenses) {
                    if (masterLicenses[i].ProviderLevel == providerlevel) {
                        temp.push(masterLicenses[i]);
                    }
                }
                return temp;
            } 
        }
        else {
            return masterLicenses;
        }
    };
});

//------------------- CCO Dashboard Controller --------------------
dashboardApp.controller("CCODashboardController", ["$scope", "$http", "$filter", "CCOLicenseService", function ($scope, $http, $filter, CCOLicenseService) {

    var orderBy = $filter('orderBy');
    $scope.selectedSection = 6;
    //------------------- Master data comes from database ------------------------

    $http.get('/Profile/MasterData/GetAllProviderTypes').
      success(function (data, status, headers, config) {
          $scope.ProviderTypes = data;
          //console.log("Provider Types");
          //console.log(data);
      }).
      error(function (data, status, headers, config) {
          //console.log("Sorry internal master data cont able to fetch.");
      });

    $http.get('/Profile/MasterData/GetAllProviderLevels').
      success(function (data, status, headers, config) {
          $scope.ProviderLevels = data;
          //console.log("Provider Levels");
          //console.log(data);
      }).
      error(function (data, status, headers, config) {
          //console.log("Sorry internal master data cont able to fetch.");
      });

    //console.log(expiredLicenses);

    $scope.LicenseData = CCOLicenseService.LicensesList();
    //console.log($scope.LicenseData);
    $scope.GrandTotalLicenses = CCOLicenseService.GetGrandTotalLicenses();

    if ($scope.LicenseData.length > 0) {
        $scope.Licenses = $scope.LicenseData[0].Licenses;
        $scope.LicenseType = $scope.LicenseData[0].LicenseType;
        $scope.LicenseTypeCode = $scope.LicenseData[0].LicenseTypeCode;
        $scope.MasterLicenses = angular.copy($scope.Licenses);
    }

    //console.log($scope.LicenseData);

    $scope.CurrentPageLicenses = [];

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
        $scope.CurrentPageLicenses = [];
        var startIndex = (newValue - 1) * 10;
        var endIndex = startIndex + 9;
        if ($scope.Licenses) {
            for (startIndex; startIndex <= endIndex ; startIndex++) {
                if ($scope.Licenses[startIndex]) {
                    $scope.CurrentPageLicenses.push($scope.Licenses[startIndex]);
                } else {
                    break;
                }
            }
        }
        //console.log($scope.CurrentPageLicenses);
    });
    //-------------- License Scope Watch ---------------------
    $scope.$watchCollection('Licenses', function (newLicenses, oldLicenses) {
        if (newLicenses) {
            $scope.bigTotalItems = newLicenses.length;

            $scope.CurrentPageLicenses = [];
            $scope.bigCurrentPage = 1;

            var startIndex = ($scope.bigCurrentPage - 1) * 10;
            var endIndex = startIndex + 9;

            for (startIndex; startIndex <= endIndex ; startIndex++) {
                if ($scope.Licenses[startIndex]) {
                    $scope.CurrentPageLicenses.push($scope.Licenses[startIndex]);
                } else {
                    break;
                }
            }
            //console.log($scope.CurrentPageLicenses);
        }
    });
    //------------------- end ------------------

    //------------------- get data according Selected days -----------------------
    $scope.getDataAccordingDays = function (days) {
        $scope.filterDays = days;
        $scope.selectedSection = 6;

        $scope.LicenseData = CCOLicenseService.GetLicenseByDayLeft(days);

        $scope.GrandTotalLicenses = CCOLicenseService.GetGrandTotalLicenses();

        if ($scope.LicenseData.length > 0) {
            $scope.providerType = "";
            $scope.providerLevel = "";
            $scope.LicenseTypeCode = $scope.LicenseData[0].LicenseTypeCode;
            $scope.Licenses = $scope.LicenseData[0].Licenses;
            $scope.LicenseType = $scope.LicenseData[0].LicenseType;
            $scope.MasterLicenses = angular.copy($scope.Licenses);
        }
    };

    //-------------- License change method -------------------------------
    $scope.getLicensTypeData = function (licenseData) {
        $scope.selectedSection = 6;
        $scope.providerType = "";
        $scope.providerLevel = "";
        $scope.Licenses = licenseData.Licenses;
        $scope.LicenseType = licenseData.LicenseType;
        $scope.LicenseTypeCode = licenseData.LicenseTypeCode;
        $scope.MasterLicenses = angular.copy($scope.Licenses);
    };

    //------------------- get data according Selected Provider Type and Provider Level-----------------------
    $scope.getDataAccordingProviderTypeAndProviderLevel = function (providertype, providerlevel) {
        $scope.selectedSection = 6;
        $scope.Licenses = CCOLicenseService.GetLicenseByProviderType(providertype, providerlevel, $scope.MasterLicenses);
    };

    //-------------- ANgular sorting filter --------------
    $scope.order = function (predicate, reverse, section) {
        $scope.selectedSection = section;
        $scope.Licenses = orderBy($scope.Licenses, predicate, reverse);
    };
}]);
