// --------------------- Provider dashboard Angular template ------------------

var StateLicenseHTMLTemplate = "<table class='table table-striped table-bordered small'>" +
    "<thead><tr><th>License Number</th><th>State</th><th>Expiry Date</th><th>Day Left</th><th>Action</th></tr></thead>" +
    "<tbody><tr ng-repeat='license in CurrentPageLicenses' ng-class='{danger:license.dayLeft < -1, warning:license.dayLeft < 90, info:license.dayLeft < 180}'><td>{{license.LicenseNumber}}</td><td>{{license.IssueState}}</td><td>{{license.ExpiryDate | date:'MM/dd/yyyy'}}</td><td>{{license.dayLeft}}</td>" +
    "<td><a href='/Profile/MasterProfile/Index' target='_blank' class='btn btn-xs btn-default' data-toggle='tooltip' data-placement='right' title='Renew'><i class='fa fa-repeat'></i></a></td></tr>" +
    "</tbody></table>";

var FederalDEAHTMLTemplate = "<table class='table table-striped table-bordered small'>" +
    "<thead><tr><th>DEA Number</th><th>State of Register</th><th>Expiry Date</th><th>Day Left</th><th>Action</th></tr></thead>" +
    "<tbody><tr ng-repeat='license in CurrentPageLicenses' ng-class='{danger:license.dayLeft < -1, warning:license.dayLeft < 90, info:license.dayLeft < 180}'><td>{{license.DEANumber}}</td><td>{{license.StateOfReg}}</td><td>{{license.ExpiryDate | date:'MM/dd/yyyy'}}</td><td>{{license.dayLeft}}</td>" +
    "<td><a href='/Profile/MasterProfile/Index' target='_blank' class='btn btn-xs btn-default' data-toggle='tooltip' data-placement='right' title='Renew'><i class='fa fa-repeat'></i></a></td></tr>" +
    "</tbody></table>";

var CDSInformationHTMLTemplate = "<table class='table table-striped table-bordered small'>" +
    "<thead><tr><th>CDS Number</th><th>State</th><th>Expiry Date</th><th>Day Left</th><th>Action</th></tr></thead>" +
    "<tbody><tr ng-repeat='license in CurrentPageLicenses' ng-class='{danger:license.dayLeft < -1, warning:license.dayLeft < 90, info:license.dayLeft < 180}'><td>{{license.CertNumber}}</td><td>{{license.State}}</td><td>{{license.ExpiryDate | date:'MM/dd/yyyy'}}</td><td>{{license.dayLeft}}</td>" +
    "<td><a href='/Profile/MasterProfile/Index' target='_blank' class='btn btn-xs btn-default' data-toggle='tooltip' data-placement='right' title='Renew'><i class='fa fa-repeat'></i></a></td></tr>" +
    "</tbody></table>";

var SpecialityBoardHTMLTemplate = "<table class='table table-striped table-bordered small'>" +
    "<thead><tr><th>Certificate Number</th><th>Speciality Board Name</th><th>Speciality Name</th><th>Expiry Date</th><th>Day Left</th><th>Action</th></tr></thead>" +
    "<tbody><tr ng-repeat='license in CurrentPageLicenses' ng-class='{danger:license.dayLeft < -1, warning:license.dayLeft < 90, info:license.dayLeft < 180}'><td>{{license.CertificateNumber}}</td><td>{{license.SpecialtyBoardName}}</td><td>{{license.SpecialtyName}}</td><td>{{license.ExpiryDate | date:'MM/dd/yyyy'}}</td><td>{{license.dayLeft}}</td>" +
    "<td><a href='/Profile/MasterProfile/Index' target='_blank' class='btn btn-xs btn-default' data-toggle='tooltip' data-placement='right' title='Renew'><i class='fa fa-repeat'></i></a></td></tr>" +
    "</tbody></table>";

var HospitalPrivilagesHTMLTemplate = "<table class='table table-striped table-bordered small'>" +
    "<thead><tr><th>Hospital Name</th><th>Expiry Date</th><th>Day Left</th><th>Action</th></tr></thead>" +
    "<tbody><tr ng-repeat='license in CurrentPageLicenses' ng-class='{danger:license.dayLeft < -1, warning:license.dayLeft < 90, info:license.dayLeft < 180}'><td>{{license.HospitalName}}</td><td>{{license.AffiliationEndDate | date:'MM/dd/yyyy'}}</td><td>{{license.dayLeft}}</td>" +
    "<td><a href='/Profile/MasterProfile/Index' target='_blank' class='btn btn-xs btn-default' data-toggle='tooltip' data-placement='right' title='Renew'><i class='fa fa-repeat'></i></a></td></tr>" +
    "</tbody></table>";

var ProfessionalLiabilityHTMLTemplate = "<table class='table table-striped table-bordered small'>" +
    "<thead><tr><th>Policy Number</th><th>Insurance Carrier</th><th>Expiry Date</th><th>Day Left</th><th>Action</th></tr></thead>" +
    "<tbody><tr ng-repeat='license in CurrentPageLicenses' ng-class='{danger:license.dayLeft < -1, warning:license.dayLeft < 90, info:license.dayLeft < 180}'><td>{{license.PolicyNumber}}</td><td>{{license.InsuranceCarrier}}</td><td>{{license.ExpirationDate | date:'MM/dd/yyyy'}}</td><td>{{license.dayLeft}}</td>" +
    "<td><a href='/Profile/MasterProfile/Index' target='_blank' class='btn btn-xs btn-default' data-toggle='tooltip' data-placement='right' title='Renew'><i class='fa fa-repeat'></i></a></td></tr>" +
    "</tbody></table>";

var WorkerCompensationHTMLTemplate = "<table class='table table-striped table-bordered small'>" +
    "<thead><tr><th>Workers Compensation Number</th><th>Expiry Date</th><th>Day Left</th><th>Action</th></tr></thead>" +
    "<tbody><tr ng-repeat='license in CurrentPageLicenses' ng-class='{danger:license.dayLeft < -1, warning:license.dayLeft < 90, info:license.dayLeft < 180}'><td>{{license.WorkersCompensationNumber}}</td><td>{{license.ExpirationDate | date:'MM/dd/yyyy'}}</td><td>{{license.dayLeft}}</td>" +
    "<td><a href='/Profile/MasterProfile/Index' target='_blank' class='btn btn-xs btn-default' data-toggle='tooltip' data-placement='right' title='Renew'><i class='fa fa-repeat'></i></a></td></tr>" +
    "</tbody></table>";


// -------------------- Directive for generate custome table according to selected Licenses ----------------------
//--------------------- Angular Directive for Provider Table Dynamic Changes ------------------------
dashboardApp.directive('providerlicensedetailstable', ['$compile', function ($compile) {
    return {
        restrict: 'AE',
        link: function (scope, element, attr) {
            var contentTr = "";

            scope.$watch("LicenseType", function (newValue) {

                if (newValue == "State License") {
                    contentTr = angular.element(StateLicenseHTMLTemplate);
                } else if (newValue == "Federal DEA") {
                    contentTr = angular.element(FederalDEAHTMLTemplate);
                } else if (scope.LicenseType == "CDS Information") {
                    contentTr = angular.element(CDSInformationHTMLTemplate);
                } else if (scope.LicenseType == "Specialty/Board") {
                    contentTr = angular.element(SpecialityBoardHTMLTemplate);
                } else if (scope.LicenseType == "Hospital Privileges") {
                    contentTr = angular.element(HospitalPrivilagesHTMLTemplate);
                } else if (scope.LicenseType == "Professional Liability") {
                    contentTr = angular.element(ProfessionalLiabilityHTMLTemplate);
                } else if (scope.LicenseType == "Worker Compensation") {
                    contentTr = angular.element(WorkerCompensationHTMLTemplate);
                } else {
                    contentTr = angular.element(StateLicenseHTMLTemplate);
                }

                element.html(contentTr);
                $compile(contentTr)(scope);
            });
        }
    };
}]);

//------------------------------- Provider License Service --------------------
dashboardApp.service('ProviderLicenseService', function () {
    
    var data = [];
    var GrandTotalLicenses = 0;

    //-------------------------- Customm array parse ---------------------
    if (expiredLicenses[0].StateLicenseExpiries) {
        data.push({
            LicenseType: "State License",
            Licenses: expiredLicenses[0].StateLicenseExpiries
        });
    }
    if (expiredLicenses[0].DEALicenseExpiries) {
        data.push({
            LicenseType: "Federal DEA",
            Licenses: expiredLicenses[0].DEALicenseExpiries
        });
    }
    if (expiredLicenses[0].CDSCInfoExpiries) {
        data.push({
            LicenseType: "CDS Information",
            Licenses: expiredLicenses[0].CDSCInfoExpiries
        });
    }
    if (expiredLicenses[0].SpecialtyDetailExpiries) {
        data.push({
            LicenseType: "Specialty/Board",
            Licenses: expiredLicenses[0].SpecialtyDetailExpiries
        });
    }
    if (expiredLicenses[0].HospitalPrivilegeExpiries) {
        data.push({
            LicenseType: "Hospital Privileges",
            Licenses: expiredLicenses[0].HospitalPrivilegeExpiries
        });
    }
    if (expiredLicenses[0].ProfessionalLiabilityExpiries) {
        data.push({
            LicenseType: "Professional Liability",
            Licenses: expiredLicenses[0].ProfessionalLiabilityExpiries
        });
    }
    if (expiredLicenses[0].WorkerCompensationExpiries) {
        data.push({
            LicenseType: "Worker Compensation",
            Licenses: expiredLicenses[0].WorkerCompensationExpiries
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

    //----------------- simply return Licese List ---------------
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
                    temp.push({ LicenseType: MasterLicenseData[i].LicenseType, Licenses: licenses });
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

dashboardApp.controller("ProviderDashboardController", ["$scope", "$http", "ProviderLicenseService", function ($scope, $http, ProviderLicenseService) {

    //-------------- document ready condition -----------------------------
    angular.element(document).ready(function () {
        //--------------- angular scope value assign ------------------
    });

    $scope.LicenseData = ProviderLicenseService.LicensesList();

    $scope.GrandTotalLicenses = ProviderLicenseService.GetGrandTotalLicenses();

    if ($scope.LicenseData.length > 0) {
        $scope.Licenses = $scope.LicenseData[0].Licenses;
        $scope.LicenseType = $scope.LicenseData[0].LicenseType;
    }

    //console.log($scope.LicenseData);

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
        $scope.LicenseData = ProviderLicenseService.GetLicenseByDayLeft(days);

        $scope.GrandTotalLicenses = ProviderLicenseService.GetGrandTotalLicenses();

        if ($scope.LicenseData.length > 0) {
            $scope.Licenses = $scope.LicenseData[0].Licenses;
            $scope.LicenseType = $scope.LicenseData[0].LicenseType;
        }
    };

    //-------------- licenses change method -------------------------------
    $scope.getLicensTypeData = function (licenseData) {
        $scope.Licenses = licenseData.Licenses;
        $scope.LicenseType = licenseData.LicenseType;
    };
}]);
