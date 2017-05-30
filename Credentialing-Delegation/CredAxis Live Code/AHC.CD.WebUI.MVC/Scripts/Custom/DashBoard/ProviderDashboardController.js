
//------------------------------- Provider License Service --------------------
dashboardApp.service('ProviderLicenseService', function ($filter) {
    
    var data = [];
    var GrandTotalLicenses = 0;

    //-------------------------- Custom array parse ---------------------
    if (expiredLicenses[0].StateLicenseExpiries) {
        data.push({
            LicenseType: "State License",
            LicenseTypeCode: "StateLicense",
            Licenses: expiredLicenses[0].StateLicenseExpiries
        });
    }
    if (expiredLicenses[0].DEALicenseExpiries) {
        data.push({
            LicenseType: "Federal DEA",
            LicenseTypeCode: "FederalDEA",
            Licenses: expiredLicenses[0].DEALicenseExpiries
        });
    }
    if (expiredLicenses[0].CDSCInfoExpiries) {
        data.push({
            LicenseType: "CDS Information",
            LicenseTypeCode: "CDSInformation",
            Licenses: expiredLicenses[0].CDSCInfoExpiries
        });
    }
    if (expiredLicenses[0].SpecialtyDetailExpiries) {
        data.push({
            LicenseType: "Specialty/Board",
            LicenseTypeCode: "SpecialityBoard",
            Licenses: expiredLicenses[0].SpecialtyDetailExpiries
        });
    }
    if (expiredLicenses[0].HospitalPrivilegeExpiries) {
        data.push({
            LicenseType: "Hospital Privileges",
            LicenseTypeCode: "HospitalPrivilages",
            Licenses: expiredLicenses[0].HospitalPrivilegeExpiries
        });
    }
    if (expiredLicenses[0].ProfessionalLiabilityExpiries) {
        data.push({
            LicenseType: "Professional Liability",
            LicenseTypeCode: "ProfessionalLiability",
            Licenses: expiredLicenses[0].ProfessionalLiabilityExpiries
        });
    }
    if (expiredLicenses[0].WorkerCompensationExpiries) {
        data.push({
            LicenseType: "Worker Compensation",
            LicenseTypeCode: "WorkerCompensation",
            Licenses: expiredLicenses[0].WorkerCompensationExpiries
        });
    }
    if (expiredLicenses[0].MedicaidExpiries) {
        data.push({
            LicenseType: "Medicaid Information",
            LicenseTypeCode: "MedicaidInformation",
            Licenses: expiredLicenses[0].MedicaidExpiries
        });
    }
    if (expiredLicenses[0].MedicareExpiries) {
        data.push({
            LicenseType: "Medicare Information",
            LicenseTypeCode: "MedicareInformation",
            Licenses: expiredLicenses[0].MedicareExpiries
        });
    }

    //if (expiredLicenses[0].UpComingRecredentials) {
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
                data[i].Licenses[j].ExpiryDate = ConvertDateFormat(data[i].Licenses[j].ExpiryDate);
                data[i].Licenses[j].dayLeft = GetRenewalDayLeft(data[i].Licenses[j].ExpiryDate);
            }
        } else if (data[i].Licenses && data[i].LicenseType == "Hospital Privileges") {
            for (var j in data[i].Licenses) {
                data[i].Licenses[j].AffiliationEndDate = ConvertDateFormat(data[i].Licenses[j].AffiliationEndDate);
                data[i].Licenses[j].dayLeft = GetRenewalDayLeft(data[i].Licenses[j].AffiliationEndDate);
            }
        } else if (data[i].LicenseType == "Professional Liability" || data[i].Licenses && data[i].LicenseType == "Worker Compensation" || data[i].LicenseType == "Medicare Information" || data[i].LicenseType == "Medicaid Information") {
            for (var j in data[i].Licenses) {
                data[i].Licenses[j].ExpirationDate = ConvertDateFormat(data[i].Licenses[j].ExpirationDate);
                data[i].Licenses[j].dayLeft = GetRenewalDayLeft(data[i].Licenses[j].ExpirationDate);
            }
        }
        //else if (data[i].LicenseType == "UpComingRecredentials") {
        //    for (var j in data[i].Licenses) {
        //        data[i].Licenses[j].ReCredentialingDate = ConvertDateFormat(data[i].Licenses[j].ReCredentialingDate);
        //        data[i].Licenses[j].dayLeft = GetRenewalDayLeft(data[i].Licenses[j].ReCredentialingDate);
        //    }
        //}
    }
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
                    if (data[i].Licenses[j].dayLeft <= 0) {
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
            }
        }
    };

    //------------------ Grand Total Number of License return ---------------------
    this.GetGrandTotalLicenses = function () {
        return GrandTotalLicenses;
    };

    //----------------- simply return License List ---------------
    this.LicensesList = function () {
        this.GetLicenseStatus(data);
        return data;
    };
    
    //----------------- License by day lefts ------------------------
    this.GetLicenseByDayLeft = function (days) {
        if (days > 0) {
            var temp = [];
            for (var i in MasterLicenseData) {
                if (MasterLicenseData[i].Licenses) {
                    var licenses = [];
                    for (var j in MasterLicenseData[i].Licenses) {
                        if (MasterLicenseData[i].Licenses[j].dayLeft <= days) {
                            licenses.push(MasterLicenseData[i].Licenses[j]);
                        }
                    }
                    temp.push({ LicenseType: MasterLicenseData[i].LicenseType, LicenseTypeCode: MasterLicenseData[i].LicenseTypeCode, Licenses: licenses });
                }
            }
            this.GetLicenseStatus(temp);
            return temp
        }
        else {
            this.GetLicenseStatus(MasterLicenseData);
            return angular.copy(MasterLicenseData);
        }
    };
});

// ------------------------ Provider Dashboard Controller ---------------------------

dashboardApp.controller("ProviderDashboardController", ["$scope", "$http", "$filter", "ProviderLicenseService", function ($scope, $http, $filter, ProviderLicenseService) {

    var orderBy = $filter('orderBy');
    $scope.selectedSection = 6;

    //-------------- document ready condition -----------------------------
    angular.element(document).ready(function () {
        //--------------- angular scope value assign ------------------
    });

    $scope.LicenseData = ProviderLicenseService.LicensesList();

    $scope.GrandTotalLicenses = ProviderLicenseService.GetGrandTotalLicenses();

    if ($scope.LicenseData.length > 0) {
        $scope.Licenses = $scope.LicenseData[0].Licenses;
        $scope.LicenseTypeCode = $scope.LicenseData[0].LicenseTypeCode;
        $scope.LicenseType = $scope.LicenseData[0].LicenseType;
    }


    $scope.CurrentPageLicenses = [];

    //-------------------------- angular bootstrap pagger with custom-----------------
    $scope.maxSize = 5;
    $scope.bigTotalItems = 0;
    $scope.bigCurrentPage = 1;

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
        }

    });
    //------------------- end ------------------

    //------------------- get data according Selected days -----------------------
    $scope.getDataAccordingDays = function (days) {
        $scope.filterDays = days;
        $scope.selectedSection = 6;

        $scope.LicenseData = ProviderLicenseService.GetLicenseByDayLeft(days);

        $scope.GrandTotalLicenses = ProviderLicenseService.GetGrandTotalLicenses();

        //if ($scope.LicenseData.length > 0) {
        //    $scope.Licenses = $scope.LicenseData[0].Licenses;
        //    $scope.LicenseType = $scope.LicenseData[$scope.LicenseType].LicenseType;
        //    $scope.LicenseTypeCode = $scope.LicenseData[$scope.LicenseType].LicenseTypeCode;
        //}
        
        var count = 0;
        if ($scope.LicenseType == "State License") {
            count = 0;
        }
        else if ($scope.LicenseType == "Federal DEA") {
            count = 1;
        }
        else if ($scope.LicenseType == "CDS Information") {
            count = 2;
        }
        else if ($scope.LicenseType == "Specialty/Board") {
            count = 3;
        }
        else if ($scope.LicenseType == "Hospital Privileges") {
            count = 4;
        }
        else if ($scope.LicenseType == "Professional Liability") {
            count = 5;
        }
        else if ($scope.LicenseType == "Worker Compensation") {
            count = 6;
        }
        else if ($scope.LicenseType == "Medicare Information") {
            count = 7;
        }
        else if ($scope.LicenseType == "Medicaid Information") {
            count = 8;
        }
        //else if ($scope.LicenseType == "UpComingRecredentials") {
        //    count = 9;
        //}
        $scope.LicenseData[count].LicenseType = $scope.LicenseType;
        $scope.LicenseTypeCode = $scope.LicenseData[count].LicenseTypeCode;
        $scope.Licenses = $scope.LicenseData[count].Licenses;
        $scope.LicenseType = $scope.LicenseData[count].LicenseType;
        
    };



    $scope.getUpcomingRenewal = function (licenseData) {
        var licenses = [];
        for (var i = 0; i < licenseData.Licenses.length; i++) {
            if (licenseData.Licenses[i].dayLeft > 90 && licenseData.Licenses[i].dayLeft < 180) {
                licenses.push(licenseData.Licenses[i]);
            }
        }
        $scope.Licenses = angular.copy(licenses);
        $scope.selectedSection = 6;
        $scope.LicenseType = licenseData.LicenseType;
        $scope.LicenseTypeCode = licenseData.LicenseTypeCode;
        $scope.CurrentPageLicenses.push($scope.Licenses);
        
    }

    $scope.getRenewalsNeeded = function (licenseData) {
        var licenses = [];
        for (var i = 0; i < licenseData.Licenses.length; i++) {
            if (licenseData.Licenses[i].dayLeft > 0 && licenseData.Licenses[i].dayLeft < 90) {
                licenses.push(licenseData.Licenses[i]);
            }
        }
        $scope.Licenses = angular.copy(licenses);
        $scope.selectedSection = 6;
        $scope.LicenseType = licenseData.LicenseType;
        $scope.LicenseTypeCode = licenseData.LicenseTypeCode;
                
    }

    $scope.getExpiredLicenses = function (licenseData) {
        var licenses = [];
        for (var i = 0; i < licenseData.Licenses.length; i++) {
            if (licenseData.Licenses[i].dayLeft <= 0) {
                licenses.push(licenseData.Licenses[i]);
            }
        }
        $scope.Licenses = angular.copy(licenses);
        $scope.selectedSection = 6;
        $scope.LicenseType = licenseData.LicenseType;
        $scope.LicenseTypeCode = licenseData.LicenseTypeCode;
                
    }
    //-------------- licenses change method -------------------------------
    $scope.getLicensTypeData = function (licenseData) {
        $scope.selectedSection = 6;
        $scope.Licenses = licenseData.Licenses;
        $scope.LicenseType = licenseData.LicenseType;
        $scope.LicenseTypeCode = licenseData.LicenseTypeCode;
    };

    //-------------- ANgular sorting filter --------------
    $scope.order = function (predicate, reverse, section) {
        $scope.selectedSection = section;
        $scope.Licenses = orderBy($scope.Licenses, predicate, reverse);
    };
}]);
