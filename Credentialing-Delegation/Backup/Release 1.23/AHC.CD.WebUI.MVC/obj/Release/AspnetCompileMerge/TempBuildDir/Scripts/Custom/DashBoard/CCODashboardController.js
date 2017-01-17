// --------------------- CCO dashboard Angular template ------------------

var StateLicenseHTMLTemplate = "<table class='table table-striped table-bordered customtbodyStyle small wrap-words'>" +
    "<thead><tr><th>NPI Number</th><th>Provider Name</th><th>License Number</th><th>State</th><th>Expiry Date</th><th>Day Left</th><th>Action</th></tr></thead>" +
    "<tbody ng-repeat='p in CurrentPageProviders track by $index'><tr><td rowspan='{{p.Licenses.length+1}}'>{{p.PersonalDetails.NPINumber}}</td>" +
    "<td rowspan='{{p.Licenses.length+1}}'>{{p.PersonalDetails.FirstName}} {{p.PersonalDetails.LastName}}</td></tr>" +
    "<tr ng-repeat='l in p.Licenses' ng-class='{danger:l.dayLeft < -1, warning:l.dayLeft < 90, info:l.dayLeft < 180}'><td>{{l.LicenseNumber}}</td><td>{{l.IssueState}}</td><td>{{l.ExpiryDate | date:'MM/dd/yyyy'}}</td><td>{{l.dayLeft}}</td>" +
    "<td><a href='/Profile/MasterProfile/ProviderProfile/{{p.PersonalDetails.ProfileId}}' target='_blank' class='btn btn-xs btn-default' tooltip data-toggle='tooltip' data-placement='right' title='Renew'><i class='fa fa-repeat'></i></a></td>" +
    "</tr></tbody></table>";

var FederalDEAHTMLTemplate = "<table class='table table-striped table-bordered customtbodyStyle small wrap-words'>" +
    "<thead><tr><th>NPI Number</th><th>Provider Name</th><th>DEA Number</th><th>State</th><th>Expiry Date</th><th>Day Left</th><th>Action</th></tr></thead>" +
    "<tbody ng-repeat='p in CurrentPageProviders track by $index'><tr><td rowspan='{{p.Licenses.length+1}}'>{{p.PersonalDetails.NPINumber}}</td>" +
    "<td rowspan='{{p.Licenses.length+1}}'>{{p.PersonalDetails.FirstName}} {{p.PersonalDetails.LastName}}</td></tr>" +
    "<tr ng-repeat='l in p.Licenses' ng-class='{danger:l.dayLeft < -1, warning:l.dayLeft < 90, success:l.dayLeft < 180}'><td>{{l.DEANumber}}</td><td>{{l.StateOfReg}}</td><td>{{l.ExpiryDate | date:'MM/dd/yyyy'}}</td><td>{{l.dayLeft}}</td>" +
    "<td><a href='/Profile/MasterProfile/ProviderProfile/{{p.PersonalDetails.ProfileId}}' target='_blank' class='btn btn-xs btn-default' tooltip data-toggle='tooltip' data-placement='right' title='Renew'><i class='fa fa-repeat'></i></a></td>" +
    "</tr></tbody></table>";

var CDSInformationHTMLTemplate = "<table class='table table-striped table-bordered customtbodyStyle small wrap-words'>" +
    "<thead><tr><th>NPI Number</th><th>Provider Name</th><th>CDS Number</th><th>State</th><th>Expiry Date</th><th>Day Left</th><th>Action</th></tr></thead>" +
    "<tbody ng-repeat='p in CurrentPageProviders track by $index'><tr><td rowspan='{{p.Licenses.length+1}}'>{{p.PersonalDetails.NPINumber}}</td>" +
    "<td rowspan='{{p.Licenses.length+1}}'>{{p.PersonalDetails.FirstName}} {{p.PersonalDetails.LastName}}</td></tr>" +
    "<tr ng-repeat='l in p.Licenses' ng-class='{danger:l.dayLeft < -1, warning:l.dayLeft < 90, success:l.dayLeft < 180}'><td>{{l.CertNumber}}</td><td>{{l.State}}</td><td>{{l.ExpiryDate | date:'MM/dd/yyyy'}}</td><td>{{l.dayLeft}}</td>" +
    "<td><a href='/Profile/MasterProfile/ProviderProfile/{{p.PersonalDetails.ProfileId}}' target='_blank' class='btn btn-xs btn-default' tooltip data-toggle='tooltip' data-placement='right' title='Renew'><i class='fa fa-repeat'></i></a></td>" +
    "</tr></tbody></table>";

var SpecialityBoardHTMLTemplate = "<table class='table table-striped table-bordered customtbodyStyle small wrap-words'>" +
    "<thead><tr><th>NPI Number</th><th>Provider Name</th><th>Certificate Number</th><th>Specialty Board Name</th><th>Specialty Name</th><th>Expiry Date</th><th>Day Left</th><th>Action</th></tr></thead>" +
    "<tbody ng-repeat='p in CurrentPageProviders track by $index'><tr><td rowspan='{{p.Licenses.length+1}}'>{{p.PersonalDetails.NPINumber}}</td>" +
    "<td rowspan='{{p.Licenses.length+1}}'>{{p.PersonalDetails.FirstName}} {{p.PersonalDetails.LastName}}</td></tr>" +
    "<tr ng-repeat='l in p.Licenses' ng-class='{danger:l.dayLeft < -1, warning:l.dayLeft < 90, success:l.dayLeft < 180}'><td>{{l.CertificateNumber}}</td><td>{{l.SpecialtyBoardName}}</td><td>{{l.SpecialtyName}}</td><td>{{l.ExpiryDate | date:'MM/dd/yyyy'}}</td><td>{{l.dayLeft}}</td>" +
    "<td><a href='/Profile/MasterProfile/ProviderProfile/{{p.PersonalDetails.ProfileId}}' target='_blank' class='btn btn-xs btn-default' tooltip data-toggle='tooltip' data-placement='right' title='Renew'><i class='fa fa-repeat'></i></a></td>" +
    "</tr></tbody></table>";

var HospitalPrivilagesHTMLTemplate = "<table class='table table-striped table-bordered customtbodyStyle small wrap-words'>" +
    "<thead><tr><th>NPI Number</th><th>Provider Name</th><th>Hospital Name</th><th>Expiry Date</th><th>Day Left</th><th>Action</th></tr></thead>" +
    "<tbody ng-repeat='p in CurrentPageProviders track by $index'><tr><td rowspan='{{p.Licenses.length+1}}'>{{p.PersonalDetails.NPINumber}}</td>" +
    "<td rowspan='{{p.Licenses.length+1}}'>{{p.PersonalDetails.FirstName}} {{p.PersonalDetails.LastName}}</td></tr>" +
    "<tr ng-repeat='l in p.Licenses' ng-class='{danger:l.dayLeft < -1, warning:l.dayLeft < 90, success:l.dayLeft < 180}'><td>{{l.HospitalName}}</td><td>{{l.AffiliationEndDate | date:'MM/dd/yyyy'}}</td><td>{{l.dayLeft}}</td>" +
    "<td><a href='/Profile/MasterProfile/ProviderProfile/{{p.PersonalDetails.ProfileId}}' target='_blank' class='btn btn-xs btn-default' tooltip data-toggle='tooltip' data-placement='right' title='Renew'><i class='fa fa-repeat'></i></a></td>" +
    "</tr></tbody></table>";

var ProfessionalLiabilityHTMLTemplate = "<table class='table table-striped table-bordered customtbodyStyle small wrap-words'>" +
    "<thead><tr><th>NPI Number</th><th>Provider Name</th><th>Policy Number</th><th>Insurance Carrier</th><th>Expiry Date</th><th>Day Left</th><th>Action</th></tr></thead>" +
    "<tbody ng-repeat='p in CurrentPageProviders track by $index'><tr><td rowspan='{{p.Licenses.length+1}}'>{{p.PersonalDetails.NPINumber}}</td>" +
    "<td rowspan='{{p.Licenses.length+1}}'>{{p.PersonalDetails.FirstName}} {{p.PersonalDetails.LastName}}</td></tr>" +
    "<tr ng-repeat='l in p.Licenses' ng-class='{danger:l.dayLeft < -1, warning:l.dayLeft < 90, success:l.dayLeft < 180}'><td>{{l.PolicyNumber}}</td><td>{{l.InsuranceCarrier}}</td><td>{{l.ExpirationDate | date:'MM/dd/yyyy'}}</td><td>{{l.dayLeft}}</td>" +
    "<td><a href='/Profile/MasterProfile/ProviderProfile/{{p.PersonalDetails.ProfileId}}' target='_blank' class='btn btn-xs btn-default' tooltip data-toggle='tooltip' data-placement='right' title='Renew'><i class='fa fa-repeat'></i></a></td>" +
    "</tr></tbody></table>";

var WorkerCompensationHTMLTemplate = "<table class='table table-striped table-bordered customtbodyStyle small wrap-words'>" +
    "<thead><tr><th>NPI Number</th><th>Provider Name</th><th>Workers Compensation Number</th><th>Expiry Date</th><th>Day Left</th><th>Action</th></tr></thead>" +
    "<tbody ng-repeat='p in CurrentPageProviders track by $index'><tr><td rowspan='{{p.Licenses.length+1}}'>{{p.PersonalDetails.NPINumber}}</td>" +
    "<td rowspan='{{p.Licenses.length+1}}'>{{p.PersonalDetails.FirstName}} {{p.PersonalDetails.LastName}}</td></tr>" +
    "<tr ng-repeat='l in p.Licenses' ng-class='{danger:l.dayLeft < -1, warning:l.dayLeft < 90, success:l.dayLeft < 180}'><td>{{l.WorkersCompensationNumber}}</td><td>{{l.ExpirationDate | date:'MM/dd/yyyy'}}</td><td>{{l.dayLeft}}</td>" +
    "<td><a href='/Profile/MasterProfile/ProviderProfile/{{p.PersonalDetails.ProfileId}}' target='_blank' class='btn btn-xs btn-default' tooltip data-toggle='tooltip' data-placement='right' title='Renew'><i class='fa fa-repeat'></i></a></td>" +
    "</tr></tbody></table>";


// -------------------- Directive for generate custome table according to selected Licenses ----------------------
//--------------------- Angular Directive for Provider Table Dynamic Changes ------------------------
dashboardApp.directive('ccolicensedetailstable', ['$compile', function ($compile) {
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
dashboardApp.service('CCOLicenseService', function () {

    var ProviderState = [];
    var ProviderDEA = [];
    var ProviderCDS = [];
    var ProviderSpeciality = [];
    var ProviderHospital = [];
    var ProviderLiability = [];
    var ProviderWorker = [];

    //---------------- parse data in license have providers with calculate Left Day ----------------------
    for (var i in expiredLicenses) {
        if (expiredLicenses[i].StateLicenseExpiries) {
            for (var j in expiredLicenses[i].StateLicenseExpiries) {
                expiredLicenses[i].StateLicenseExpiries[j].ExpiryDate = ConvertDateFormat(expiredLicenses[i].StateLicenseExpiries[j].ExpiryDate);
                expiredLicenses[i].StateLicenseExpiries[j].dayLeft = GetRenewalDayLeft(expiredLicenses[i].StateLicenseExpiries[j].ExpiryDate);
            }
            if (expiredLicenses[i].StateLicenseExpiries.length > 0) {
                ProviderState.push({
                    PersonalDetails: {
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
                        ProviderTypes: expiredLicenses[i].ProviderTypes
                    },
                    Licenses: expiredLicenses[i].StateLicenseExpiries
                });
            }
        }

        if (expiredLicenses[i].DEALicenseExpiries) {
            for (var j in expiredLicenses[i].DEALicenseExpiries) {
                expiredLicenses[i].DEALicenseExpiries[j].ExpiryDate = ConvertDateFormat(expiredLicenses[i].DEALicenseExpiries[j].ExpiryDate);
                expiredLicenses[i].DEALicenseExpiries[j].dayLeft = GetRenewalDayLeft(expiredLicenses[i].DEALicenseExpiries[j].ExpiryDate);
            }
            if (expiredLicenses[i].DEALicenseExpiries.length > 0) {
                ProviderDEA.push({
                    PersonalDetails: {
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
                        ProviderTypes: expiredLicenses[i].ProviderTypes
                    },
                    Licenses: expiredLicenses[i].DEALicenseExpiries
                });
            }
        }

        if (expiredLicenses[i].CDSCInfoExpiries) {
            for (var j in expiredLicenses[i].CDSCInfoExpiries) {
                expiredLicenses[i].CDSCInfoExpiries[j].ExpiryDate = ConvertDateFormat(expiredLicenses[i].CDSCInfoExpiries[j].ExpiryDate);
                expiredLicenses[i].CDSCInfoExpiries[j].dayLeft = GetRenewalDayLeft(expiredLicenses[i].CDSCInfoExpiries[j].ExpiryDate);
            }
            if (expiredLicenses[i].CDSCInfoExpiries.length > 0) {
                ProviderCDS.push({
                    PersonalDetails: {
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
                        ProviderTypes: expiredLicenses[i].ProviderTypes
                    },
                    Licenses: expiredLicenses[i].CDSCInfoExpiries
                });
            }
        }

        if (expiredLicenses[i].SpecialtyDetailExpiries) {
            for (var j in expiredLicenses[i].SpecialtyDetailExpiries) {
                expiredLicenses[i].SpecialtyDetailExpiries[j].ExpiryDate = ConvertDateFormat(expiredLicenses[i].SpecialtyDetailExpiries[j].ExpiryDate);
                expiredLicenses[i].SpecialtyDetailExpiries[j].dayLeft = GetRenewalDayLeft(expiredLicenses[i].SpecialtyDetailExpiries[j].ExpiryDate);
            }
            if (expiredLicenses[i].SpecialtyDetailExpiries.length > 0) {
                ProviderSpeciality.push({
                    PersonalDetails: {
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
                        ProviderTypes: expiredLicenses[i].ProviderTypes
                    },
                    Licenses: expiredLicenses[i].SpecialtyDetailExpiries
                });
            }
        }

        if (expiredLicenses[i].HospitalPrivilegeExpiries) {
            for (var j in expiredLicenses[i].HospitalPrivilegeExpiries) {
                expiredLicenses[i].HospitalPrivilegeExpiries[j].AffiliationEndDate = ConvertDateFormat(expiredLicenses[i].HospitalPrivilegeExpiries[j].AffiliationEndDate);
                expiredLicenses[i].HospitalPrivilegeExpiries[j].dayLeft = GetRenewalDayLeft(expiredLicenses[i].HospitalPrivilegeExpiries[j].AffiliationEndDate);
            }
            if (expiredLicenses[i].HospitalPrivilegeExpiries.length > 0) {
                ProviderHospital.push({
                    PersonalDetails: {
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
                        ProviderTypes: expiredLicenses[i].ProviderTypes
                    },
                    Licenses: expiredLicenses[i].HospitalPrivilegeExpiries
                });
            }
        }

        if (expiredLicenses[i].ProfessionalLiabilityExpiries) {
            for (var j in expiredLicenses[i].ProfessionalLiabilityExpiries) {
                expiredLicenses[i].ProfessionalLiabilityExpiries[j].ExpirationDate = ConvertDateFormat(expiredLicenses[i].ProfessionalLiabilityExpiries[j].ExpirationDate);
                expiredLicenses[i].ProfessionalLiabilityExpiries[j].dayLeft = GetRenewalDayLeft(expiredLicenses[i].ProfessionalLiabilityExpiries[j].ExpirationDate);
            }
            if (expiredLicenses[i].ProfessionalLiabilityExpiries.length > 0) {
                ProviderLiability.push({
                    PersonalDetails: {
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
                        ProviderTypes: expiredLicenses[i].ProviderTypes
                    },
                    Licenses: expiredLicenses[i].ProfessionalLiabilityExpiries
                });
            }
        }

        if (expiredLicenses[i].WorkerCompensationExpiries) {
            for (var j in expiredLicenses[i].WorkerCompensationExpiries) {
                expiredLicenses[i].WorkerCompensationExpiries[j].ExpirationDate = ConvertDateFormat(expiredLicenses[i].WorkerCompensationExpiries[j].ExpirationDate);
                expiredLicenses[i].WorkerCompensationExpiries[j].dayLeft = GetRenewalDayLeft(expiredLicenses[i].WorkerCompensationExpiries[j].ExpirationDate);
            }
            if (expiredLicenses[i].WorkerCompensationExpiries.length > 0) {
                ProviderWorker.push({
                    PersonalDetails: {
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
                        ProviderTypes: expiredLicenses[i].ProviderTypes
                    },
                    Licenses: expiredLicenses[i].WorkerCompensationExpiries
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
            Providers: ProviderState
        });
    }
    if (ProviderDEA) {
        LicenseData.push({
            LicenseType: "Federal DEA",
            Providers: ProviderDEA
        });
    }
    if (ProviderCDS) {
        LicenseData.push({
            LicenseType: "CDS Information",
            Providers: ProviderCDS
        });
    }
    if (ProviderSpeciality) {
        LicenseData.push({
            LicenseType: "Specialty/Board",
            Providers: ProviderSpeciality
        });
    }
    if (ProviderHospital) {
        LicenseData.push({
            LicenseType: "Hospital Privileges",
            Providers: ProviderHospital
        });
    }
    if (ProviderLiability) {
        LicenseData.push({
            LicenseType: "Professional Liability",
            Providers: ProviderLiability
        });
    }
    if (ProviderWorker) {
        LicenseData.push({
            LicenseType: "Worker Compensation",
            Providers: ProviderWorker
        });
    }

    //--------------- Master license Data for Static Data ---------------
    var MasterLicenseData = angular.copy(LicenseData);

    //---------------------- license status return -------------------
    this.GetLicenseStatus = function (data) {
        GrandTotalLicenses = 0;

        for (var i in data) {

            var ValidatedLicense = 0;
            var dayLeftLicense = 0;
            var ExpiredLicense = 0;

            var TotalLicenses = 0;

            for (var j in data[i].Providers) {
                for (var k in data[i].Providers[j].Licenses) {
                    TotalLicenses++;
                    if (data[i].Providers[j].Licenses[k].dayLeft < 0) {
                        ExpiredLicense++;
                        GrandTotalLicenses++;
                    } else if (data[i].Providers[j].Licenses[k].dayLeft < 90) {
                        dayLeftLicense++;
                        GrandTotalLicenses++;
                    }
                    else if (data[i].Providers[j].Licenses[k].dayLeft < 180) {
                        ValidatedLicense++;
                        GrandTotalLicenses++;
                    }
                }
            }

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
                var Providers = [];
                for (var j in MasterLicenseData[i].Providers) {
                    var licenses = [];
                    for (var k in MasterLicenseData[i].Providers[j].Licenses) {
                        if (MasterLicenseData[i].Providers[j].Licenses[k].dayLeft <= days) {
                            licenses.push(MasterLicenseData[i].Providers[j].Licenses[k]);
                        }
                    }
                    if (licenses.length > 0) {
                        Providers.push({ Licenses: licenses, PersonalDetails: MasterLicenseData[i].Providers[j].PersonalDetails });
                    }
                }
                temp.push({ LicenseType: MasterLicenseData[i].LicenseType, Providers: Providers });
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
    this.GetLicenseByProviderType = function (providertype, providerlevel, masterProviders) {
        if (providertype != "" || providerlevel != "") {
            if (providertype != "" && providerlevel != "") {
                var temp = [];
                for (var i in masterProviders) {
                    var status = false;
                    for (var j in masterProviders[i].PersonalDetails.ProviderTitles) {
                        if (masterProviders[i].PersonalDetails.ProviderTitles[j] == providertype && masterProviders[i].PersonalDetails.ProviderLevel == providerlevel) {
                            status = true;
                        }
                    }
                    if (status) {
                        temp.push(masterProviders[i]);
                    }
                }
                return temp;
            } else if (providertype != "" && providerlevel == "") {
                var temp = [];
                for (var i in masterProviders) {
                    var status = false;
                    for (var j in masterProviders[i].PersonalDetails.ProviderTitles) {
                        if (masterProviders[i].PersonalDetails.ProviderTitles[j] == providertype) {
                            status = true;
                        }
                    }
                    if (status) {
                        temp.push(masterProviders[i]);
                    }
                }
                return temp;
            } else if (providertype == "" && providerlevel != "") {
                var temp = [];
                for (var i in masterProviders) {
                    if (masterProviders[i].PersonalDetails.ProviderLevel == providerlevel) {
                        temp.push(masterProviders[i]);
                    }
                }
                return temp;
            } 
        }
        else {
            return masterProviders;
        }
    };
});

//------------------- CCO Dashboard Controller --------------------
dashboardApp.controller("CCODashboardController", ["$scope", "$http", "CCOLicenseService", function ($scope, $http, CCOLicenseService) {

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

    //-------------- document ready condition -----------------------------
    angular.element(document).ready(function () {
        //--------------- angular scope value assign ------------------
    });

    $scope.LicenseData = CCOLicenseService.LicensesList();

    $scope.GrandTotalLicenses = CCOLicenseService.GetGrandTotalLicenses();

    if ($scope.LicenseData.length > 0) {
        $scope.Providers = $scope.LicenseData[0].Providers;
        $scope.LicenseType = $scope.LicenseData[0].LicenseType;
        $scope.MasterProviderByLicense = angular.copy($scope.Providers);
    }

    //console.log($scope.LicenseData);

    $scope.CurrentPageProviders = [];

    //-------------------------- angular bootstrap pagger with custom-----------------
    $scope.maxSize = 5;
    $scope.bigTotalItems = 0;
    $scope.bigCurrentPage = 1;

    //-------------- current page change Scope Watch ---------------------
    $scope.$watch('bigCurrentPage', function (newValue, oldValue) {
        $scope.CurrentPageProviders = [];
        var startIndex = (newValue - 1) * 10;
        var endIndex = startIndex + 9;
        if ($scope.Providers) {
            for (startIndex; startIndex <= endIndex ; startIndex++) {
                if ($scope.Providers[startIndex]) {
                    $scope.CurrentPageProviders.push($scope.Providers[startIndex]);
                } else {
                    break;
                }
            }
        }
        //console.log($scope.CurrentPageProviders);
    });
    //-------------- License Scope Watch ---------------------
    $scope.$watchCollection('Providers', function (newProviders, oldProviders) {
        if (newProviders) {
            $scope.bigTotalItems = newProviders.length;

            $scope.CurrentPageProviders = [];
            $scope.bigCurrentPage = 1;

            var startIndex = ($scope.bigCurrentPage - 1) * 10;
            var endIndex = startIndex + 9;

            for (startIndex; startIndex <= endIndex ; startIndex++) {
                if ($scope.Providers[startIndex]) {
                    $scope.CurrentPageProviders.push($scope.Providers[startIndex]);
                } else {
                    break;
                }
            }
            //console.log($scope.CurrentPageProviders);
        }
    });
    //------------------- end ------------------

    //------------------- get data according Selected days -----------------------
    $scope.getDataAccordingDays = function (days) {
        $scope.LicenseData = CCOLicenseService.GetLicenseByDayLeft(days);

        $scope.GrandTotalLicenses = CCOLicenseService.GetGrandTotalLicenses();

        if ($scope.LicenseData.length > 0) {
            $scope.providerType = "";
            $scope.providerLevel = "";
            $scope.Providers = $scope.LicenseData[0].Providers;
            $scope.LicenseType = $scope.LicenseData[0].LicenseType;
            $scope.MasterProviderByLicense = angular.copy($scope.Providers);
        }
    };

    //-------------- License change method -------------------------------
    $scope.getLicensTypeData = function (licenseData) {
        $scope.providerType = "";
        $scope.providerLevel = "";
        $scope.Providers = licenseData.Providers;
        $scope.LicenseType = licenseData.LicenseType;
        $scope.MasterProviderByLicense = angular.copy($scope.Providers);
    };

    //------------------- get data according Selected Provider Type and Provider Level-----------------------
    $scope.getDataAccordingProviderTypeAndProviderLevel = function (providertype, providerlevel) {
        $scope.Providers = CCOLicenseService.GetLicenseByProviderType(providertype, providerlevel, $scope.MasterProviderByLicense);
    };
}]);
