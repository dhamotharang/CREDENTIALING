
//------------------------------- Provider License Service --------------------
dashboardApp.service('CCOLicenseService', function ($filter) {

    var ProviderState = [];
    var ProviderDEA = [];
    var ProviderCDS = [];
    var ProviderSpeciality = [];
    var ProviderHospital = [];
    var ProviderLiability = [];
    var ProviderWorker = [];
    var ProviderMedicare = [];
    var ProviderMedicaid = [];
    var UpComingRecredentials = [];
    var ProviderCAQH = [];

    //---------------- parse data in license have Licenses with calculate Left Day ----------------------
    if (expiredLicenses != null && expiredLicenses.length != 0) {
        for (var i = 0; i < expiredLicenses.length; i++) {
            if (expiredLicenses[i].StateLicenseExpiries && expiredLicenses[i].StateLicenseExpiries.length > 0) {
                for (var j in expiredLicenses[i].StateLicenseExpiries) {
                    expiredLicenses[i].StateLicenseExpiries[j].ExpiryDate = ConvertDateFormat(expiredLicenses[i].StateLicenseExpiries[j].ExpiryDate);
                    expiredLicenses[i].StateLicenseExpiries[j].dayLeft = GetRenewalDayLeft(expiredLicenses[i].StateLicenseExpiries[j].ExpiryDate);
                    if (expiredLicenses[i].StateLicenseExpiries[j].StateLicenseInformationID == 115) {
                        expiredLicenses[i].StateLicenseExpiries[j].dayLeft = GetRenewalDayLeft(expiredLicenses[i].StateLicenseExpiries[j].ExpiryDate);
                    }
                    ProviderState.push({
                        ProfileStatus: expiredLicenses[i].ProfileStatus,
                        EmailAddress: expiredLicenses[i].EmailAddress,
                        ExpiryNotificationDetailID: expiredLicenses[i].ExpiryNotificationDetailID,
                        ProviderRelationShip: expiredLicenses[i].ProviderRelationship,
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
                        ProfileStatus: expiredLicenses[i].ProfileStatus,
                        EmailAddress: expiredLicenses[i].EmailAddress,
                        ExpiryNotificationDetailID: expiredLicenses[i].ExpiryNotificationDetailID,
                        ProviderRelationShip: expiredLicenses[i].ProviderRelationship,
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
                        ProfileStatus: expiredLicenses[i].ProfileStatus,
                        EmailAddress: expiredLicenses[i].EmailAddress,
                        ExpiryNotificationDetailID: expiredLicenses[i].ExpiryNotificationDetailID,
                        ProviderRelationShip: expiredLicenses[i].ProviderRelationship,
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
                        ProfileStatus: expiredLicenses[i].ProfileStatus,
                        EmailAddress: expiredLicenses[i].EmailAddress,
                        ExpiryNotificationDetailID: expiredLicenses[i].ExpiryNotificationDetailID,
                        ProviderRelationShip: expiredLicenses[i].ProviderRelationship,
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
                        ProfileStatus: expiredLicenses[i].ProfileStatus,
                        EmailAddress: expiredLicenses[i].EmailAddress,
                        ExpiryNotificationDetailID: expiredLicenses[i].ExpiryNotificationDetailID,
                        ProviderRelationShip: expiredLicenses[i].ProviderRelationship,
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
                        ProfileStatus: expiredLicenses[i].ProfileStatus,
                        EmailAddress: expiredLicenses[i].EmailAddress,
                        ExpiryNotificationDetailID: expiredLicenses[i].ExpiryNotificationDetailID,
                        ProviderRelationShip: expiredLicenses[i].ProviderRelationship,
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
                        ProfileStatus: expiredLicenses[i].ProfileStatus,
                        EmailAddress: expiredLicenses[i].EmailAddress,
                        ExpiryNotificationDetailID: expiredLicenses[i].ExpiryNotificationDetailID,
                        ProviderRelationShip: expiredLicenses[i].ProviderRelationship,
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

            if (expiredLicenses[i].MedicareExpiries && expiredLicenses[i].MedicareExpiries.length > 0) {
                for (var j in expiredLicenses[i].MedicareExpiries) {
                    expiredLicenses[i].MedicareExpiries[j].ExpirationDate = ConvertDateFormat(expiredLicenses[i].MedicareExpiries[j].ExpirationDate);
                    expiredLicenses[i].MedicareExpiries[j].dayLeft = GetRenewalDayLeft(expiredLicenses[i].MedicareExpiries[j].ExpirationDate);

                    ProviderMedicare.push({
                        ProfileStatus: expiredLicenses[i].ProfileStatus,
                        EmailAddress: expiredLicenses[i].EmailAddress,
                        ExpiryNotificationDetailID: expiredLicenses[i].ExpiryNotificationDetailID,
                        ProviderRelationShip: expiredLicenses[i].ProviderRelationship,
                        FirstName: expiredLicenses[i].FirstName,
                        LastName: expiredLicenses[i].LastName,
                        MiddleName: expiredLicenses[i].MiddleName,
                        LastModifiedDate: expiredLicenses[i].LastModifiedDate,
                        NPINumber: expiredLicenses[i].NPINumber,
                        ProfileId: expiredLicenses[i].ProfileId,
                        ProviderLevel: expiredLicenses[i].ProviderLevel,
                        ProviderTitles: expiredLicenses[i].ProviderTitles,
                        ProviderTypes: expiredLicenses[i].ProviderTypes,
                        License: expiredLicenses[i].MedicareExpiries[j]
                    });
                }
            }

            if (expiredLicenses[i].MedicaidExpiries && expiredLicenses[i].MedicaidExpiries.length > 0) {
                for (var j in expiredLicenses[i].MedicaidExpiries) {
                    expiredLicenses[i].MedicaidExpiries[j].ExpirationDate = ConvertDateFormat(expiredLicenses[i].MedicaidExpiries[j].ExpirationDate);
                    expiredLicenses[i].MedicaidExpiries[j].dayLeft = GetRenewalDayLeft(expiredLicenses[i].MedicaidExpiries[j].ExpirationDate);

                    ProviderMedicaid.push({
                        ProfileStatus: expiredLicenses[i].ProfileStatus,
                        EmailAddress: expiredLicenses[i].EmailAddress,
                        ExpiryNotificationDetailID: expiredLicenses[i].ExpiryNotificationDetailID,
                        ProviderRelationShip: expiredLicenses[i].ProviderRelationship,
                        FirstName: expiredLicenses[i].FirstName,
                        LastName: expiredLicenses[i].LastName,
                        MiddleName: expiredLicenses[i].MiddleName,
                        LastModifiedDate: expiredLicenses[i].LastModifiedDate,
                        NPINumber: expiredLicenses[i].NPINumber,
                        ProfileId: expiredLicenses[i].ProfileId,
                        ProviderLevel: expiredLicenses[i].ProviderLevel,
                        ProviderTitles: expiredLicenses[i].ProviderTitles,
                        ProviderTypes: expiredLicenses[i].ProviderTypes,
                        License: expiredLicenses[i].MedicaidExpiries[j]
                    });
                }
            }
            if (expiredLicenses[i].CAQHExpiries && expiredLicenses[i].CAQHExpiries.length > 0) {
                for (var j in expiredLicenses[i].CAQHExpiries) {
                    expiredLicenses[i].CAQHExpiries[j].NextAttestationDate = ConvertDateFormat(expiredLicenses[i].CAQHExpiries[j].NextAttestationDate);
                    expiredLicenses[i].CAQHExpiries[j].dayLeft = GetRenewalDayLeft(expiredLicenses[i].CAQHExpiries[j].NextAttestationDate);
                    ProviderCAQH.push({
                        ProfileStatus: expiredLicenses[i].ProfileStatus || "",
                        CAQHNumber: expiredLicenses[i].CAQHExpiries[j].CAQHNumber || "",
                        EmailAddress: expiredLicenses[i].EmailAddress,
                        ExpiryNotificationDetailID: expiredLicenses[i].ExpiryNotificationDetailID,
                        ProviderRelationShip: expiredLicenses[i].ProviderRelationship,
                        FirstName: expiredLicenses[i].FirstName,
                        LastName: expiredLicenses[i].LastName,
                        MiddleName: expiredLicenses[i].MiddleName,
                        LastModifiedDate: expiredLicenses[i].LastModifiedDate,
                        NPINumber: expiredLicenses[i].NPINumber,
                        ProfileId: expiredLicenses[i].ProfileId,
                        ProviderLevel: expiredLicenses[i].ProviderLevel,
                        ProviderTitles: expiredLicenses[i].ProviderTitles,
                        ProviderTypes: expiredLicenses[i].ProviderTypes,
                        License: expiredLicenses[i].CAQHExpiries[j]
                    });
                }
            }
            if (expiredLicenses[i].UpComingRecredentials && expiredLicenses[i].UpComingRecredentials.length > 0) {
                for (var j in expiredLicenses[i].UpComingRecredentials) {
                    expiredLicenses[i].UpComingRecredentials[j].ReCredentialingDate = ConvertDateFormat(expiredLicenses[i].UpComingRecredentials[j].ReCredentialingDate);
                    expiredLicenses[i].UpComingRecredentials[j].ExpirationDate = ConvertDateFormat(expiredLicenses[i].UpComingRecredentials[j].ExpirationDate);
                    expiredLicenses[i].UpComingRecredentials[j].dayLeft = GetRenewalDayLeft(expiredLicenses[i].UpComingRecredentials[j].ReCredentialingDate);
                    if (expiredLicenses[i].UpComingRecredentials[j] != null) {
                        expiredLicenses[i].UpComingRecredentials[j].InitiatedDate = ConvertDateFormat(expiredLicenses[i].UpComingRecredentials[j].InitiatedDate);
                        expiredLicenses[i].UpComingRecredentials[j].TerminationDate = ConvertDateFormat(expiredLicenses[i].UpComingRecredentials[j].TerminationDate);
                        expiredLicenses[i].UpComingRecredentials[j].InitialCredentialingDate = ConvertDateFormat(expiredLicenses[i].UpComingRecredentials[j].InitialCredentialingDate);
                    }
                    UpComingRecredentials.push({
                        EmailAddress: expiredLicenses[i].EmailAddress,
                        ExpiryNotificationDetailID: expiredLicenses[i].ExpiryNotificationDetailID,
                        ProviderRelationShip: expiredLicenses[i].ProviderRelationship,
                        FirstName: expiredLicenses[i].FirstName,
                        LastName: expiredLicenses[i].LastName,
                        MiddleName: expiredLicenses[i].MiddleName,
                        LastModifiedDate: expiredLicenses[i].LastModifiedDate,
                        NPINumber: expiredLicenses[i].NPINumber,
                        ProfileId: expiredLicenses[i].ProfileId,
                        ProviderLevel: expiredLicenses[i].ProviderLevel,
                        ProviderTitles: expiredLicenses[i].ProviderTitles,
                        ProviderTypes: expiredLicenses[i].ProviderTypes,
                        License: expiredLicenses[i].UpComingRecredentials[j]
                    });
                }
            }


        }
    }

    //-------------------------- Customm array parse ---------------------
    var LicenseData = [];
    var GrandTotalLicenses = 0;

    if (ProviderState) {
        LicenseData.push({
            LicenseType: "State License",
            LicenseTypeCode: "StateLicense",
            Licenses: ProviderState,
            Show: ProviderState.length?true:false
        });
    }
    if (ProviderDEA) {
        LicenseData.push({
            LicenseType: "Federal DEA",
            LicenseTypeCode: "FederalDEA",
            Licenses: ProviderDEA,
            Show: ProviderDEA.length ? true : false
          
        });
    }
    if (ProviderCDS) {
        LicenseData.push({
            LicenseType: "CDS Information",
            LicenseTypeCode: "CDSInformation",
            Licenses: ProviderCDS,
            Show: ProviderCDS.length ? true : false
        });
    }
    if (ProviderSpeciality) {
        LicenseData.push({
            LicenseType: "Specialty/Board",
            LicenseTypeCode: "SpecialityBoard",
            Licenses: ProviderSpeciality,
            Show: ProviderSpeciality.length ? true : false
        });
    }
    if (ProviderHospital) {
        LicenseData.push({
            LicenseType: "Hospital Privileges",
            LicenseTypeCode: "HospitalPrivilages",
            Licenses: ProviderHospital,
            Show: ProviderHospital.length ? true : false
        });
    }
    if (ProviderLiability) {
        LicenseData.push({
            LicenseType: "Professional Liability",
            LicenseTypeCode: "ProfessionalLiability",
            Licenses: ProviderLiability,
            Show: ProviderLiability.length ? true : false
        });
    }
    if (ProviderWorker) {
        LicenseData.push({
            LicenseType: "Worker Compensation",
            LicenseTypeCode: "WorkerCompensation",
            Licenses: ProviderWorker,
            Show: ProviderWorker.length ? true : false
        });
    }

    if (ProviderMedicare) {
        LicenseData.push({
            LicenseType: "Medicare Information",
            LicenseTypeCode: "MedicareInformation",
            Licenses: ProviderMedicare,
            Show: ProviderMedicare.length ? true : false
        });
    }

    if (ProviderMedicaid) {
        LicenseData.push({
            LicenseType: "Medicaid Information",
            LicenseTypeCode: "MedicaidInformation",
            Licenses: ProviderMedicaid,
            Show: ProviderMedicaid.length ? true : false
        });
    }


    if (ProviderCAQH) {
        LicenseData.push({
            LicenseType: "CAQH",
            LicenseTypeCode: "CAQHExpiries",
            Licenses: ProviderCAQH,
            Show: ProviderCAQH.length ? true : false
        });
    }

    if (UpComingRecredentials) {
        LicenseData.push({
            LicenseType: "UpComing Recredentials",
            LicenseTypeCode: "UpComingRecredentials",
            Licenses: UpComingRecredentials,
            Show: UpComingRecredentials.length ? true : false
        });
    }
    //--------------- Master license Data for Static Data ---------------
    var MasterLicenseData = angular.copy(LicenseData);

    //---------------------------------------------------
    this.innerDataStatus = function (data) {

        var ValidatedLicense = 0;
        var dayLeftforLicenselessthan90 = 0;
        var dayLeftforLicenselessthan60 = 0;
        var dayLeftforLicenselessthan30 = 0;
        var ExpiredLicense = 0;

        var TotalLicenses = 0;
        for (var i in data.Licenses) {
            TotalLicenses++;
            if (data.Licenses[i].License.dayLeft <= 0) {
                ExpiredLicense++;
                GrandTotalLicenses++;
            }
            else if (data.Licenses[i].License.dayLeft <= 90 && data.Licenses[i].License.dayLeft > 60) {
                dayLeftforLicenselessthan90++;
                GrandTotalLicenses++;
            }
            else if (data.Licenses[i].License.dayLeft <= 60 && data.Licenses[i].License.dayLeft > 30) {
                dayLeftforLicenselessthan60++;
                GrandTotalLicenses++;
            }
            else if (data.Licenses[i].License.dayLeft <= 30 && data.Licenses[i].License.dayLeft > 0) {
                dayLeftforLicenselessthan30++;
                GrandTotalLicenses++;
            }
            else if (data.Licenses[i].License.dayLeft < 180) {
                ValidatedLicense++;
                GrandTotalLicenses++;
            }
        }



        var orderBy = $filter('orderBy');
        data.Licenses = orderBy(data.Licenses, 'License.dayLeft', false);

        data.LicenseStatus = {
            ValidLicense: ValidatedLicense,
            PendingDaylicenseLessThan90: dayLeftforLicenselessthan90,
            PendingDaylicenseLessThan60: dayLeftforLicenselessthan60,
            PendingDaylicenseLessThan30: dayLeftforLicenselessthan30,
            ExpiredLicense: ExpiredLicense
        };
        data.TotalLicenses = TotalLicenses;
        return data;
    }
    //---------------------- license status return -------------------
    this.GetLicenseStatus = function (data) {
        GrandTotalLicenses = 0;

        for (var i in data) {

            data[i] = this.innerDataStatus(data[i]);
        }
        return data;
    };

    //------------------ Grand Total Number of License return ---------------------
    this.GetGrandTotalLicenses = function () {
        return GrandTotalLicenses;
    };

    //----------------- simply return Licese List ---------------
    this.LicensesList = function () {
        LicenseData = this.GetLicenseStatus(LicenseData);
        return LicenseData;
    };
    //----------------- License by day lefts ------------------------
    this.GetLicenseByDayLeft = function (days) {
        if (days != '') {
            var temp = [];
            for (var i in MasterLicenseData) {
                var licenses = [];
                for (var j in MasterLicenseData[i].Licenses) {
                    if (MasterLicenseData[i].Licenses[j].License.dayLeft <= days) {
                        licenses.push(MasterLicenseData[i].Licenses[j]);
                    }
                }
                temp.push({ LicenseType: MasterLicenseData[i].LicenseType, LicenseTypeCode: MasterLicenseData[i].LicenseTypeCode, Licenses: licenses, Show: MasterLicenseData[i].Show });
            }
            temp = this.GetLicenseStatus(temp);
            return temp
        }
        else {
            MasterLicenseData = this.GetLicenseStatus(MasterLicenseData);
            return angular.copy(MasterLicenseData);
        }
    };

    //----------------- License by day lefts Inner Tab------------------------
    this.GetLicenseByDayLeftForInnerTab = function (days, SelectedTab) {
        if (days > 0) {
            var temp = [];
            for (var i in MasterLicenseData) {
                if (MasterLicenseData[i].LicenseTypeCode == SelectedTab) {
                    var licenses = [];
                    for (var j in MasterLicenseData[i].Licenses) {
                        if (MasterLicenseData[i].Licenses[j].License.dayLeft <= days) {
                            licenses.push(MasterLicenseData[i].Licenses[j]);
                        }
                    }
                    temp.push({ LicenseType: MasterLicenseData[i].LicenseType, LicenseTypeCode: MasterLicenseData[i].LicenseTypeCode, Licenses: licenses,Show:MasterLicenseData[i].Show });
                }
                else {
                    temp.push({ LicenseType: MasterLicenseData[i].LicenseType, LicenseTypeCode: MasterLicenseData[i].LicenseTypeCode, Licenses: MasterLicenseData[i].Licenses, Show: MasterLicenseData[i].Show });
                }

            }
            temp = this.GetLicenseStatus(temp);
            return temp
        }
        else {
            MasterLicenseData = this.GetLicenseStatus(MasterLicenseData);
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

    this.getAllLicenseDataForTab = function (id) {
        var tempdata = [];
        if (id == "StateLicense") {
            tempdata.push(MasterLicenseData[0].Licenses);
        }
        else if (id == "FederalDEA") {
            tempdata.push(MasterLicenseData[1].Licenses);
        }
        else if (id == "CDSInformation") {
            tempdata.push(MasterLicenseData[2].Licenses);
        }
        else if (id == "SpecialityBoard") {
            tempdata.push(MasterLicenseData[3].Licenses);
        }
        else if (id == "HospitalPrivilages") {
            tempdata.push(MasterLicenseData[4].Licenses);
        }
        else if (id == "ProfessionalLiability") {
            tempdata.push(MasterLicenseData[5].Licenses);
        }
        else if (id == "WorkerCompensation") {
            tempdata.push(MasterLicenseData[6].Licenses);
        }
        else if (id == "MedicareInformation") {
            tempdata.push(MasterLicenseData[7].Licenses);
        }
        else if (id == "MedicaidInformation") {
            tempdata.push(MasterLicenseData[8].Licenses);
        }
        else if (id == "UpComingRecredentials") {
            tempdata.push(MasterLicenseData[10].Licenses);
        }

        else if (id == "CAQHExpiries") {
            tempdata.push(MasterLicenseData[9].Licenses);
        }

        return tempdata;
    };

});

//------------------- CCO Dashboard Controller --------------------
dashboardApp.controller("CCODashboardController", ["$scope", "$rootScope", "$http", "$filter", "CCOLicenseService", function ($scope, $rootScope, $http, $filter, CCOLicenseService) {

    $scope.newday = '';
    $scope.filterData = {};
    //$scope.filteredData = {};
    $scope.printData = function (str) {

        if (str == 'All') {
            $scope.printAllPagesDataById($scope.LicenseType);
        }
        else if (str == 'Current') {
            $scope.printCurrentPageDataById($scope.LicenseType);
        }
    }
    $scope.Selected_Day = '';
    $scope.tempObj = [];
    //-----------Variables for inner filter--------
    $scope.StateLicense_Count = '';
    $scope.FederalDEA_Count = '';
    $scope.CDSInformation_Count = '';
    $scope.SpecialityBoard_Count = '';
    $scope.HospitalPrivilages_Count = '';
    $scope.ProfessionalLiability_Count = '';
    $scope.WorkerCompensation_Count = '';
    $scope.MedicareInformation_Count = '';
    $scope.MedicaidInformation_Count = '';
    $scope.UpComingRecredentials_Count = '';
    $scope.CAQHExpiries_Count = '';
    $scope.newSelection = function (index, status) {

        var present = false;

        if ($scope.tempObj != null && $scope.tempObj.length != 0) {

            for (var i = 0; i < $scope.tempObj.length; i++) {

                if ($scope.tempObj[i] == index) {

                    present = true;
                    break;

                }

            }

        }

        if (!present) {

            $scope.tempObj.push(index);

        } else {

            $scope.tempObj.splice($scope.tempObj.indexOf(index), 1);

        }

    }


    $scope.saveIt = function () {

        //sessionStorage.setItem('myTabs', JSON.stringify($scope.LicenseData));
        $scope.tempObj = [];
        $scope.controlpanel = false;

    }

    $scope.revertIt = function () {

        if ($scope.tempObj != null && $scope.tempObj.length != 0) {

            for (var i = 0; i < $scope.tempObj.length; i++) {

                if ($scope.LicenseData[$scope.tempObj[i]].Show == true) {

                    $scope.LicenseData[$scope.tempObj[i]].Show = false;

                } else {

                    $scope.LicenseData[$scope.tempObj[i]].Show = true;

                }

            }

        }

        $scope.tempObj = [];

        $scope.controlpanel = false;

        //for (var i = 0; i < $scope.LicenseData.length; i++) {

        //    $scope.LicenseData[i].Show = true;
        //    $scope.controlpanel = false;

        //}


    }

    $scope.isFiltered = false;

    $scope.PlanNames = [];


    //$scope.exportToExcel = function () {

    //    if (str == 'All') {
    //        $scope.exportToExcelAllData($scope.LicenseType);
    //    }
    //    else if (str == 'Current') {
    //        $scope.printCurrentPageDataById($scope.LicenseType);
    //    }
    //}

    $scope.exportToExcel = function () {

        var divToPrint = document.getElementById($scope.LicenseType);
        $('#hiddenPrintDiv').empty();
        $('#hiddenPrintDiv').append("<table>" + divToPrint.innerHTML + "</table>");
        $('#hiddenPrintDiv thead:last-child,#hiddenPrintDiv th:last-child, #hiddenPrintDiv td:last-child').remove();
        $('#hiddenPrintDiv table').attr("id", "exportTable");
        $('#hiddenPrintDiv').attr("download", "ExportToExcel.xls");
        $('#exportTable').tableExport({ type: 'excel', escape: 'false' }, $scope.LicenseType);
    }

    $scope.printAllPagesDataById = function (id) {
        var divToPrint = document.getElementById(id);
        $(".skip").hide();
        $('#hiddenPrintDiv').empty();

        $('#hiddenPrintDiv').append(divToPrint.innerHTML);
        // Removing the last column of the table
        $('#hiddenPrintDiv thead:last-child,#hiddenPrintDiv th:last-child, #hiddenPrintDiv td:last-child').remove();
        // Creating a window for printing
        var mywindow = window.open('', $('#hiddenPrintDiv').html(), 'height=800,width=800');
        mywindow.document.write('<center><b style="font-size:large">' + id + '</b></center></br>');
        mywindow.document.write('<html><head><title>' + id + '</title>');
        mywindow.document.write('<link rel="stylesheet" href="/Content/SharedCss/bootstrap.min.css" type="text/css" />');
        mywindow.document.write('<link rel="stylesheet" href="/Content/SharedCss/app.css" type="text/css" />');
        mywindow.document.write('<style>@page{size: auto;margin-bottom:5mm;margin-top:7mm;}th{text-align: center;}</style>');
        mywindow.document.write('</head><body style="background-color:white"><table class="table table-bordered"></td>');
        mywindow.document.write($('#hiddenPrintDiv').html());
        mywindow.document.write('<script>document.getElementById("skip").innerHTML=""</script>');
        mywindow.document.write('</table></body></html>');
        mywindow.document.close();
        mywindow.focus();
        setTimeout(function () {
            mywindow.print();
            mywindow.close();
        }, 1000);
        $(".skip").show();
    }

    $scope.printCurrentPageDataById = function (id) {
        var divToPrint = document.getElementById(id);
        $(".skip").hide();
        $('#hiddenPrintDiv').empty();
        $('#hiddenPrintDiv').append(divToPrint.innerHTML);
        // Removing the last column of the table
        $('#hiddenPrintDiv thead:last-child,#hiddenPrintDiv th:last-child, #hiddenPrintDiv td:last-child').remove();
        // Creating a window for printing
        var mywindow = window.open('', $('#hiddenPrintDiv').html(), 'height=800,width=800');
        mywindow.document.write('<center><b style="font-size:large">' + id + '</b></center></br>');
        mywindow.document.write('<html><head><title>' + id + '</title>');
        mywindow.document.write('<link rel="stylesheet" href="/Content/SharedCss/bootstrap.min.css" type="text/css" />');
        mywindow.document.write('<link rel="stylesheet" href="/Content/SharedCss/app.css" type="text/css" />');
        mywindow.document.write('<style>.ng-hide:not(.ng-hide-animate) {display: none !important;}@page{size: auto;margin-bottom:5mm;margin-top:7mm;}th{text-align: center;}</style>');
        mywindow.document.write('</head><body style="background-color:white"><table class="table table-bordered"></td>');
        mywindow.document.write($('#hiddenPrintDiv').html());
        mywindow.document.write('<script>document.getElementById("skip").innerHTML=""</script>');
        mywindow.document.write('</table></body></html>');
        mywindow.document.close();
        mywindow.focus();
        setTimeout(function () {
            mywindow.print();
            mywindow.close();
        }, 1000);
        $(".skip").show();
    }

    $scope.DeactivateData = function (data, tab) {
        $scope.DeactivatedProfile = data;
        $("#DeactivateProfile").modal();
        $scope.SelectedDeactiveTab = tab;
    };

    $scope.UpCome = function (data, tab) {
        $scope.UpComingRecredentialData = data;
        //$("#upcomingReCred").modal('hide');
        $scope.SucessMessage = false;
        $("#upcomingReCred").modal();
        $scope.SelectedDeactiveTab = tab;
    };

    $scope.setCredInfo = function (id) {

        sessionStorage.setItem('credentialingInfoId', id);

    };
    $scope.hidemodal = function () {
        $("#upcomingReCred").modal('hide');
    }

    $scope.InitiateReCredentialing = function (provObj) {
        $scope.disble = true;
        var id = provObj.License.CredentialingInfoID;
        var obj2 = {
            ProfileID: provObj.ProfileId,
            NPINumber: provObj.NPINumber,
            //CAQHNumber: provObj.Profile.OtherIdentificationNumber.CAQH,
            FirstName: provObj.FirstName,
            LastName: provObj.LastName,
            PlanID: provObj.License.PlanID,
            //IsDelegatedYesNoOption: provObj.IsDelegatedYesNoOption,
            StatusType: 1
        };
        //var NotSelectedID = [];
        $scope.CredentialingContractRequestsIntArray = [];
        $scope.NonCredentialingContractRequestsIntArray = [];
        $scope.CredentialingContractRequestsIntArray = provObj.License.CredentialingContractRequestID;

        $scope.isHasError = false;
        if (provObj.License.PlanID == null) {
            $scope.isHasError = true;
        }

        if ($scope.isHasError == false) {
            $http({
                method: "POST",
                url: rootDir + "/Credentialing/Initiation/InitiateReCredentialing?id=" + id,
                data: {
                    credentialingInitiationInfo: obj2,
                    CredentialingContractRequestsArray: angular.copy($scope.CredentialingContractRequestsIntArray),
                    //LOBID: provObj.License.LOBID,
                    //ContractGridID:provObj.License.ContractGridID
                }
            }).success(function (data, status, headers, config) {
                $("#upcomingReCred").modal();
                $scope.SucessMessage = data.status;
                try {
                    if (data.status == "true") {


                        $scope.initiateSuccess = true;
                        $scope.initiateComplete = true;
                        $scope.IsMessage = true;
                        sessionStorage.setItem('CreListId', 2);
                        $scope.tempID = data.ID;
                        $scope.setCredInfo($scope.tempID);
                        $scope.Licenses.splice($scope.Licenses.indexOf($scope.Licenses.filter(function (records) { return records.index == provObj.index })[0]), 1);
                        //    //$scope.getReCredList();
                        //    $scope.disble = false;




                        //    CCOLicenseService.callAlertMessage('successfulInitiated', "ReCredentialing Initiated Successfully. !!!!", "success", true);

                        //}
                        //else {
                        //    $scope.disble = false;
                        //    CCOLicenseService.callAlertMessage('errorInitiated', "", "danger", true);
                        //    $scope.errorInitiated = data.status.split(",");
                    }
                } catch (e) {

                }
            }).error(function (data, status, headers, config) {
                $scope.SucessMessage = data.status;
                //$scope.disble = false;                
                //messageAlertEngine.callAlertMessage('errorInitiated', "", "danger", true);
                //$scope.errorInitiated = "Sorry for Inconvenience !!!! Please Try Again Later...";
            });
        }
    }

    $scope.ConfirmDeacivatoin = function (data) {
        $http.post(rootDir + '/Profile/MasterProfile/DeactivateProfile?profileID=' + data.ProfileId).
               success(function (data, status, headers, config) {
                   try {
                       //----------- success message -----------
                       if (data) {
                           for (var i in $scope.LicenseData) {

                               var tempdata = [];
                               for (var j in $scope.LicenseData[i].Licenses) {
                                   if ($scope.LicenseData[i].Licenses[j].ProfileId != $scope.DeactivatedProfile.ProfileId) {
                                       tempdata.push($scope.LicenseData[i].Licenses[j]);
                                   }
                               }
                               $scope.LicenseData[i].Licenses = tempdata;
                               if ($scope.LicenseData[i].LicenseTypeCode == $scope.SelectedDeactiveTab) {
                                   $scope.getLicensTypeData($scope.LicenseData[i]);
                               }
                           }

                           $scope.LicenseData = CCOLicenseService.GetLicenseStatus($scope.LicenseData);
                       }
                   } catch (e) {

                   }
               }).
               error(function (data, status, headers, config) {
                   //----------- error message -----------
               });
        $('#DeactivateProfile').modal('hide');
    };

    //----------------------------------Download--------------------------------
    //$scope.downloadData = function () {
    //    $scope.downloadDataById($scope.LicenseType);
    //}

    //$scope.downloadDataById = function (id) {

    //    //--------First Method---------
    //    //var divToPrint = document.getElementById(id);
    //    //$('#hiddenPrintDiv').empty();
    //    //$('#hiddenPrintDiv').append(divToPrint.innerHTML);
    //    //$('#hiddenPrintDiv thead:last-child,#hiddenPrintDiv th:last-child, #hiddenPrintDiv td:last-child').remove();

    //    //var pdf = new jsPDF('p', 'pt', 'letter');
    //    //pdf.table(tableToJson(divToPrint));
    //    //pdf.save('sample-file.pdf');

    //    //function tableToJson(table) {
    //    //    var data = [];

    //    //    // first row needs to be headers
    //    //    var headers = [];
    //    //    for (var i = 0; i < table.rows[0].cells.length; i++) {
    //    //        headers[i] = table.rows[0].cells[i];
    //    //    }

    //    //    // go through cells
    //    //    for (var i = 1; i < table.rows.length; i++) {

    //    //        var tableRow = table.rows[i];
    //    //        var rowData = {};

    //    //        for (var j = 0; j < tableRow.cells.length; j++) {

    //    //            rowData[headers[j]] = tableRow.cells[j].innerHTML;

    //    //        }

    //    //        data.push(rowData);
    //    //    }
    //    //    return data;
    //    //}


    //    //-------Second Method-------
    //    var divToPrint = document.getElementById(id);
    //    $('#hiddenPrintDiv').empty();
    //    $('#hiddenPrintDiv').append(divToPrint.innerHTML);
    //    $('#hiddenPrintDiv thead:last-child,#hiddenPrintDiv th:last-child, #hiddenPrintDiv td:last-child').remove();

    //    var pdf = new jsPDF('p', 'pt', [594, 841]);
    //    var totalCols = $("#hiddenPrintDiv tr td").length;
    //    var cellWidth = 534 / totalCols;
    //    pdf.cellInitialize();
    //    pdf.setFontSize(10);

    //    $.each($('#hiddenPrintDiv tr'), function (i, row) {
    //        pdf.text(235, 30, id);

    //        $.each($(row).find("td, th"), function (j, cell) {
    //            var txt = $(cell).text().trim().split(" ").join("\n") || " ";
    //           // var width = (j == 4) ? 40 : 90; //make with column smaller
    //            var height = (i == 0) ? 40 : 30;
    //            pdf.cell(30, 50, cellWidth, height, txt, i);
    //        });
    //    });

    //    pdf.save(id+'.pdf');


    //--------Third Method--------
    //var divToPrint = document.getElementById(id);
    //var element = $(divToPrint).clone();
    //element.find('tr th:last-child, td:last-child').remove();
    //var source = element.html();
    //var pdf = new jsPDF('p', 'pt', 'letter');
    //var specialElementHandlers = {
    //    '#bypassme': function (element, renderer) {
    //        return true;
    //    }
    //};
    //margins = {
    //    top: 80,
    //    bottom: 60,
    //    left: 60,
    //    width: 700
    //};
    //pdf.text(235, 30, id);
    //pdf.fromHTML("<table>" + source + "</table>", // HTML string or DOM elem ref.
    //       margins.left, // x coord
    //       margins.top, { // y coord
    //           'width': margins.width, // max width of content on PDF
    //           'elementHandlers': specialElementHandlers
    //       },
    //       function (dispose) {

    //           pdf.save(id + '.pdf');
    //       },
    //    margins);



    //---------Fourth Method--------
    //var l = {
    //    orientation: 'l',
    //    unit: 'mm',
    //    format: 'a4',
    //    compress: true,
    //    fontSize: 8,
    //    lineHeight: 1,
    //    autoSize: false,
    //    printHeaders: true
    //}, pdf = new jsPDF(l, '', '', ''), i, j, margins = {
    //    top: 30,
    //    bottom: 10,
    //    left: 10,
    //    width: 25
    //};

    //var divToPrint = document.getElementById(id);
    //$('#hiddenPrintDiv').empty();
    //$('#hiddenPrintDiv').append(divToPrint.innerHTML);
    //$('#hiddenPrintDiv thead:last-child,#hiddenPrintDiv th:last-child, #hiddenPrintDiv td:last-child').remove();
    //var totalCols = $("#hiddenPrintDiv tr td").length;
    //var cellWidth = 534 / totalCols;
    ////initializing the cells
    //pdf.cellInitialize();

    //$.each($('#hiddenPrintDiv tr'), function (i, row) {
    //    $.each($(row).find("td, th"), function (j, cell) {
    //        var txt = $(cell).text().trim().split(" ").join("\n") || " ";
    //        // var width = (j == 4) ? 40 : 90; //make with column smaller
    //        var height = (i == 0) ? 40 : 30;
    //        pdf.cell(margins.left, margins.top, cellWidth, height, txt, i);
    //    });
    //});
    ////pdf.cell(margins.left, margins.top, 14, 8, lines, 0);

    //pdf.save('Te.pdf');


    //-------------------Getting the Dashboard Biscuit Data---------Author Dv----------------
    $scope.GetInitialData = function () {
        $http.get(rootDir + '/Dashboard/GetInitialProviderData').
     success(function (data, status, headers, config) {
         $scope.InitBiscuitData = data;

     }).
     error(function (data, status, headers, config) {
     });
    };

    $scope.GetInitialData();




    var orderBy = $filter('orderBy');
  //  $scope.selectedSection = 6;

    //--------------- data show current page function -------------------
    $scope.GetCurrentPageData = function (data, pageNumber) {

        $scope.bigTotalItems = data.length;
        $scope.CurrentPageLicenses = [];
        $scope.bigCurrentPage = pageNumber;

        var startIndex = (pageNumber - 1) * 10;
        var endIndex = startIndex + 9;
        if (data) {
            for (startIndex; startIndex <= endIndex ; startIndex++) {
                if (data[startIndex]) {
                    $scope.CurrentPageLicenses.push(data[startIndex]);
                } else {
                    break;
                }
            }
        }
    };

    $scope.IsValidIndex = function (index) {

        var startIndex = ($scope.currentPageNumber - 1) * 10;
        var endIndex = startIndex + 9;

        if (index >= startIndex && index <= endIndex)
            return true;
        else
            return false;

    }

    //------------------- Master data comes from database ------------------------

    $http.get(rootDir + '/Profile/MasterData/GetAllProviderTypes').
      success(function (data, status, headers, config) {
          $scope.ProviderTypes = data;
      }).
      error(function (data, status, headers, config) {
      });

    $http.get(rootDir + '/Profile/MasterData/GetAllProviderLevels').
      success(function (data, status, headers, config) {
          $scope.ProviderLevels = data;
      }).
      error(function (data, status, headers, config) {
      });

    //------------Add full name attribute to License-----Author Dv---------
    $scope.addFullName = function (data) {
        for (var i in data) {
            for (var j in data[i].Licenses) {
                data[i].Licenses[j].Fullname = data[i].Licenses[j].FirstName + " " + data[i].Licenses[j].LastName;
            }
        }
        return data;
    }

    $scope.LicenseData = CCOLicenseService.LicensesList();
    $scope.LicenseData = $scope.addFullName($scope.LicenseData);
    console.log($scope.LicenseData);

    $scope.GrandTotalLicenses = CCOLicenseService.GetGrandTotalLicenses();

    if ($scope.LicenseData.length > 0) {
        $scope.Licenses = $scope.LicenseData[0].Licenses;
        $scope.LicenseType = $scope.LicenseData[0].LicenseType;
        $scope.LicenseTypeCode = $scope.LicenseData[0].LicenseTypeCode;
        $scope.Show = $scope.LicenseData[0].Show;
        $scope.MasterLicenses = angular.copy($scope.Licenses);
        $scope.GetCurrentPageData($scope.Licenses, 1);
        $scope.tempLicenseData = angular.copy($scope.Licenses);
    }


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
        if ($scope.Licenses) {
            $scope.GetCurrentPageData($scope.Licenses, newValue);
            $scope.currentPageNumber = newValue;
        }
    });


    //----------------gettng data according to count---------------Author Dv-----
    $scope.getDataBasedOnCount = function () {
        if ($scope.LicenseData.length > 0) {
            $scope.providerType = "";
            $scope.providerLevel = "";
            //$scope.LicenseData.LicenseType = LicenseType;
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
            else if ($scope.LicenseType == "CAQH") {
                count = 9;
            }
            else if ($scope.LicenseType == "UpComing Recredentials") {
                count = 10;
            }

            $scope.LicenseTypeCode = $scope.LicenseData[count].LicenseTypeCode;
            $scope.Show = $scope.LicenseData[count].Show;
            $scope.Licenses = $scope.LicenseData[count].Licenses;
            $scope.tempLicenseData = angular.copy($scope.Licenses);
            $scope.LicenseType = $scope.LicenseData[count].LicenseType;
            $scope.MasterLicenses = angular.copy($scope.Licenses);
            $scope.addflatObjectToLicense($scope.LicenseType);
        }

    }

    //----------------making the Licesnce, Flat Object---Author Dv----------------
    $scope.addflatObjectToLicense = function (templicense) {
        if (templicense == "State License") {
            for (var i in $scope.Licenses) {
                $scope.Licenses[i].dayLeft = $scope.Licenses[i].License.dayLeft || "";
                $scope.Licenses[i].ExpiryDate = $filter('date')($scope.Licenses[i].License.ExpiryDate, "MM/dd/yyyy") || "";
                $scope.Licenses[i].LicenseNumber = $scope.Licenses[i].License.LicenseNumber || "";
                $scope.Licenses[i].IssueState = $scope.Licenses[i].License.IssueState || "";
                if ($scope.Licenses[i].License.StateLicenseStatus != undefined) {
                    $scope.Licenses[i].Title = $scope.Licenses[i].License.StateLicenseStatus.Title || "";
                }
                // $scope.Licenses[i].LicenseNumber = $scope.Licenses[i].License.LicenseNumber;
            }
        }
        else if (templicense == "Federal DEA") {
            for (var i in $scope.Licenses) {
                $scope.Licenses[i].dayLeft = $scope.Licenses[i].License.dayLeft || "";
                $scope.Licenses[i].ExpiryDate = $filter('date')($scope.Licenses[i].License.ExpiryDate, "MM/dd/yyyy") || "";
                $scope.Licenses[i].DEANumber = $scope.Licenses[i].License.DEANumber || "";
                $scope.Licenses[i].StateOfReg = $scope.Licenses[i].License.StateOfReg || "";
            }
        }
        else if (templicense == "CDS Information") {
            for (var i in $scope.Licenses) {
                $scope.Licenses[i].CertNumber = $scope.Licenses[i].License.CertNumber || "";
                $scope.Licenses[i].ExpiryDate = $filter('date')($scope.Licenses[i].License.ExpiryDate, "MM/dd/yyyy") || "";
                $scope.Licenses[i].State = $scope.Licenses[i].License.State || "";
                $scope.Licenses[i].dayLeft = $scope.Licenses[i].License.dayLeft || "";

            }
        }
        else if (templicense == "Specialty/Board") {
            for (var i in $scope.Licenses) {
                $scope.Licenses[i].dayLeft = $scope.Licenses[i].License.dayLeft || "";
                $scope.Licenses[i].ExpiryDate = $filter('date')($scope.Licenses[i].License.ExpiryDate, "MM/dd/yyyy") || "";
                $scope.Licenses[i].SpecialtyBoardName = $scope.Licenses[i].License.SpecialtyBoardName || "";

            }
        }
        else if (templicense == "Hospital Privileges") {
            for (var i in $scope.Licenses) {
                $scope.Licenses[i].dayLeft = $scope.Licenses[i].License.dayLeft || "";
                $scope.Licenses[i].AffiliationEndDate = $filter('date')($scope.Licenses[i].License.AffiliationEndDate, "MM/dd/yyyy") || "";
                $scope.Licenses[i].HospitalName = $scope.Licenses[i].License.HospitalName || "";
                // $scope.Licenses[i].AffiliationEndDate = $scope.Licenses[i].License.AffiliationEndDate;

            }
        }
        else if (templicense == "Professional Liability") {
            for (var i in $scope.Licenses) {
                $scope.Licenses[i].dayLeft = $scope.Licenses[i].License.dayLeft || "";
                $scope.Licenses[i].ExpirationDate = $filter('date')($scope.Licenses[i].License.ExpirationDate, "MM/dd/yyyy") || "";
                $scope.Licenses[i].PolicyNumber = $scope.Licenses[i].License.PolicyNumber || "";
                $scope.Licenses[i].InsuranceCarrier = $scope.Licenses[i].License.InsuranceCarrier || "";

            }
        }
        else if (templicense == "Worker Compensation") {
            for (var i in $scope.Licenses) {
                $scope.Licenses[i].dayLeft = $scope.Licenses[i].License.dayLeft || "";
                $scope.Licenses[i].ExpirationDate = $filter('date')($scope.Licenses[i].License.ExpirationDate, "MM/dd/yyyy") || "";
                $scope.Licenses[i].WorkersCompensationNumber = $scope.Licenses[i].License.WorkersCompensationNumber || "";
                //  $scope.Licenses[i].IssueState = $scope.Licenses[i].License.IssueState;

            }
        }
        else if (templicense == "Medicare Information") {
            for (var i in $scope.Licenses) {
                $scope.Licenses[i].dayLeft = $scope.Licenses[i].License.dayLeft || "";
                $scope.Licenses[i].ExpirationDate = $filter('date')($scope.Licenses[i].License.ExpirationDate, "MM/dd/yyyy") || "";
                $scope.Licenses[i].LicenseNumber = $scope.Licenses[i].License.LicenseNumber || "";
                $scope.Licenses[i].IssueState = $scope.Licenses[i].License.IssueState || "";

            }
        }
        else if (templicense == "Medicaid Information") {
            for (var i in $scope.Licenses) {
                $scope.Licenses[i].dayLeft = $scope.Licenses[i].License.dayLeft || "";
                $scope.Licenses[i].ExpirationDate = $filter('date')($scope.Licenses[i].License.ExpirationDate, "MM/dd/yyyy") || "";
                $scope.Licenses[i].LicenseNumber = $scope.Licenses[i].License.LicenseNumber || "";
                $scope.Licenses[i].IssueState = $scope.Licenses[i].License.IssueState || "";

            }
        }

        else if (templicense == "CAQH") {
            for (var i in $scope.Licenses) {
                $scope.Licenses[i].dayLeft = $scope.Licenses[i].License.dayLeft || "";
                $scope.Licenses[i].CAQHNumber = $scope.Licenses[i].License.CAQHNumber || "";
                $scope.Licenses[i].NextAttestationDate = $filter('date')($scope.Licenses[i].License.NextAttestationDate, "MM/dd/yyyy") || "";
            }
        }
        else if (templicense == "UpComing Recredentials") {
            for (var i in $scope.Licenses) {
                $scope.Licenses[i].Plan = $scope.Licenses[i].License.PlanName || "";
                $scope.Licenses[i].LOBName = $scope.Licenses[i].License.LOBName || "";
                $scope.Licenses[i].ParticipatingStatus = $scope.Licenses[i].License.ParticipatingStatus || "";
                $scope.Licenses[i].GroupID = $scope.Licenses[i].License.GroupID || "";
                $scope.Licenses[i].ProviderID = $scope.Licenses[i].License.ProviderID || "";
                $scope.Licenses[i].InitiatedDate = $filter('date')($scope.Licenses[i].License.InitiatedDate, "MM/dd/yyyy") || "";
                $scope.Licenses[i].TerminationDate = $filter('date')($scope.Licenses[i].License.TerminationDate, "MM/dd/yyyy") || "";
                $scope.Licenses[i].ReCredentialingDate = $filter('date')($scope.Licenses[i].License.ReCredentialingDate, "MM/dd/yyyy") || "";
                $scope.Licenses[i].dayLeft = $scope.Licenses[i].License.dayLeft || "";
                $scope.Licenses[i].index = i;
            }
        }

    }

    //------------------- get data according Selected days -----------------------
    $scope.getDataAccordingDays = function (days) {
        $scope.InnerFilterDay = 0;
        $scope.filterDays = days;
      //  $scope.selectedSection = 6;
        //  $scope.Selected_Day = '';
        $scope.LicenseData = CCOLicenseService.GetLicenseByDayLeft(days);
        $scope.LicenseData = $scope.addFullName($scope.LicenseData);
        $scope.GrandTotalLicenses = CCOLicenseService.GetGrandTotalLicenses();
        //$scope.getDaysLeftForSelectedTab('All');
        //if ($scope.LicenseData.length > 0) {
        //    $scope.providerType = "";
        //    $scope.providerLevel = "";
        //    $scope.LicenseTypeCode = $scope.LicenseData[0].LicenseTypeCode;
        //    $scope.Licenses = $scope.LicenseData[0].Licenses;
        //    $scope.LicenseType = $scope.LicenseData[0].LicenseType;
        //    $scope.MasterLicenses = angular.copy($scope.Licenses);
        //}
        $scope.getDataBasedOnCount();

        $scope.GetCurrentPageData($scope.Licenses, 1);
    };




    //-------------editied by pritam--------
    $scope.getUpcomingRenewals = function (licenseData) {
        var licenses = [];
        for (var i = 0; i < licenseData.Licenses.length; i++) {
            if (licenseData.Licenses[i].License.dayLeft > 90 && licenseData.Licenses[i].License.dayLeft < 180) {
                licenses.push(licenseData.Licenses[i]);
                $scope.tempLicenseData = angular.copy($scope.Licenses);
            }
        }
        $scope.Licenses = angular.copy(licenses);
       // $scope.selectedSection = 6;
        $scope.LicenseType = licenseData.LicenseType;
        $scope.LicenseTypeCode = licenseData.LicenseTypeCode;
        $scope.Show = licenseData.Show;
        $scope.MasterLicenses = angular.copy($scope.Licenses);
        $scope.GetCurrentPageData($scope.Licenses, 1);
    }

    $scope.getRenewalNeededforDaysLessThan90 = function (licenseData) {
        var licenses = [];
        for (var i = 0; i < licenseData.Licenses.length; i++) {
            if (licenseData.Licenses[i].License.dayLeft > 60 && licenseData.Licenses[i].License.dayLeft <= 90) {
                licenses.push(licenseData.Licenses[i]);
                $scope.tempLicenseData = angular.copy(licenses);
            }
        }
        $scope.Licenses = angular.copy(licenses);
      //  $scope.selectedSection = 6;
        $scope.LicenseType = licenseData.LicenseType;
        $scope.LicenseTypeCode = licenseData.LicenseTypeCode;
        $scope.Show = licenseData.Show;
        $scope.MasterLicenses = angular.copy($scope.Licenses);
        $scope.GetCurrentPageData($scope.Licenses, 1);
    }
    $scope.getRenewalNeededforDaysLessThan60 = function (licenseData) {
        var licenses = [];
        for (var i = 0; i < licenseData.Licenses.length; i++) {
            if (licenseData.Licenses[i].License.dayLeft > 30 && licenseData.Licenses[i].License.dayLeft <= 60) {
                licenses.push(licenseData.Licenses[i]);
                $scope.tempLicenseData = angular.copy(licenses);
            }
        }
        $scope.Licenses = angular.copy(licenses);
       // $scope.selectedSection = 6;
        $scope.LicenseType = licenseData.LicenseType;
        $scope.LicenseTypeCode = licenseData.LicenseTypeCode;
        $scope.Show = licenseData.Show;
        $scope.MasterLicenses = angular.copy($scope.licenses);
        $scope.GetCurrentPageData(Licenses, 1);
    }
    $scope.getRenewalNeededforDaysLessThan30 = function (licenseData) {
        var licenses = [];
        for (var i = 0; i < licenseData.Licenses.length; i++) {
            if (licenseData.Licenses[i].License.dayLeft > 0 && licenseData.Licenses[i].License.dayLeft <= 30) {
                licenses.push(licenseData.Licenses[i]);
                $scope.tempLicenseData = angular.copy(licenses);
            }
        }
        $scope.Licenses = angular.copy(licenses);
        // $scope.selectedSection = 6;
        $scope.LicenseType = licenseData.LicenseType;
        $scope.LicenseTypeCode = licenseData.LicenseTypeCode;
        $scope.Show = licenseData.Show;
        $scope.MasterLicenses = angular.copy($scope.Licenses);
        $scope.GetCurrentPageData($scope.Licenses, 1);
    }

    $scope.getExpiredLicense = function (licenseData) {
        var licenses = [];
        for (var i = 0; i < licenseData.Licenses.length; i++) {
            if (licenseData.Licenses[i].License.dayLeft <= 0) {
                licenses.push(licenseData.Licenses[i]);
                $scope.tempLicenseData = angular.copy(licenses);

            }
        }
        $scope.Licenses = angular.copy(licenses);
    //    $scope.selectedSection = 6;
        $scope.LicenseType = licenseData.LicenseType;
        $scope.LicenseTypeCode = licenseData.LicenseTypeCode;
        $scope.Show = licenseData.Show;
        $scope.MasterLicenses = angular.copy($scope.Licenses);
        $scope.getDataBasedOnCount();
        $scope.GetCurrentPageData($scope.Licenses, 1);
    }

    //-------------- License change method -------------------------------

    $scope.getLicensTypeData = function (licenseData) {
        if (licenseData.LicenseType == "UpComing Recredentials") {
            $scope.stat = true;
        }
        else
            $scope.stat = false;
        $scope.filterData = {};
        $scope.Licenses = licenseData.Licenses;
        $scope.LicenseType = licenseData.LicenseType;
        $scope.LicenseTypeCode = licenseData.LicenseTypeCode;
        $scope.Show = licenseData.Show;
        $scope.MasterLicenses = angular.copy($scope.Licenses);
        $scope.GetFilterDayCount($scope.LicenseTypeCode);
        //  $scope.getDaysLeftForSelectedTab($scope.Selected_Day, $scope.LicenseTypeCode);
        //   $scope.tempLicenseData = CCOLicenseService.getDataCount();
        $scope.getDataBasedOnCount();

        $scope.GetCurrentPageData($scope.Licenses, 1);
    };


    //------------------- get data according Selected Provider Type and Provider Level-----------------------
    //$scope.getDataAccordingProviderTypeAndProviderLevel = function (providertype, providerlevel, id) {
    //    $scope.SetProviderTypeAndLevelData(providertype, providerlevel, id);
        $scope.selectedSection = 6;
    //    $scope.Licenses = CCOLicenseService.GetLicenseByProviderType(providertype, providerlevel, $scope.MasterLicenses);
    //    $scope.GetCurrentPageData($scope.Licenses, 1);
    //};

    //-----------New Method for above implementation------Dv--
    $scope.getDataAccordingProviderTypeAndProviderLevel = function (providertype, providerlevel, SelectedTab) {
        // $scope.SetProviderTypeAndLevelData(providertype, providerlevel, id);
        // $scope.Licenses = CCOLicenseService.GetLicenseByProviderType(providertype, providerlevel, $scope.MasterLicenses);
        // $scope.LicenseData = CCOLicenseService.GetLicenseStatus($scope.LicenseData);
        // $scope.LicenseData = $scope.addFullName($scope.LicenseData);
        // $scope.GrandTotalLicenses = CCOLicenseService.GetGrandTotalLicenses();
        //// $scope.Licenses = angular.copy(Templicenses);
        // $scope.GetCurrentPageData($scope.Licenses, 1);

        var Templicenses = [];

        var newTemp = (CCOLicenseService.getAllLicenseDataForTab(SelectedTab));
        $scope.Tab_License_Data = newTemp[0];
        $scope.SetProviderTypeAndLevelData(providertype, providerlevel, SelectedTab);
        Templicenses = CCOLicenseService.GetLicenseByProviderType(providertype, providerlevel, $scope.Tab_License_Data);
        for (var i in $scope.LicenseData) {
            if ($scope.LicenseData[i].LicenseTypeCode == SelectedTab) {
                $scope.LicenseData[i].Licenses = Templicenses;
                break;
            }
        }
        $scope.LicenseData = CCOLicenseService.GetLicenseStatus($scope.LicenseData);
        $scope.LicenseData = $scope.addFullName($scope.LicenseData);
        $scope.GrandTotalLicenses = CCOLicenseService.GetGrandTotalLicenses();
        $scope.Licenses = angular.copy(Templicenses);
        $scope.GetCurrentPageData($scope.Licenses, 1);
        $scope.newTemp = [];
    };
    //------Setting provider Type and Provider Level Data from Dashboard------Dv--
    $scope.SetProviderTypeAndLevelData = function (type, level, id) {
        if (id == 'StateLicense') {
            $scope.SL_Provider_Type = type;
            $scope.SL_Provider_Level = level;
        }
        else if (id == 'FederalDEA') {
            $scope.FD_Provider_Type = type;
            $scope.FD_Provider_Level = level;
        }
        else if (id == 'CDSInformation') {
            $scope.CDS_Provider_Type = type;
            $scope.CDS_Provider_Level = level;
        }
        else if (id == 'SpecialityBoard') {
            $scope.SB_Provider_Type = type;
            $scope.SB_Provider_Level = level;
        }
        else if (id == 'HospitalPrivilages') {
            $scope.HP_Provider_Type = type;
            $scope.HP_Provider_Level = level;
        }
        else if (id == 'ProfessionalLiability') {
            $scope.PL_Provider_Type = type;
            $scope.PL_Provider_Level = level;
        }
        else if (id == 'WorkerCompensation') {
            $scope.WC_Provider_Type = type;
            $scope.WC_Provider_Level = level;
        }
        else if (id == 'MedicareInformation') {
            $scope.MD_Provider_Type = type;
            $scope.MD_Provider_Level = level;
        }
        else if (id == 'MedicaidInformation') {
            $scope.MR_Provider_Type = type;
            $scope.MR_Provider_Level = level;
        }
        else if (id == 'CAQHExpiries') {
            $scope.CAQH_Provider_Type = type;
            $scope.CAQH_Provider_Level = level;
        }
        else if (id == 'UpComingRecredentials') {
            $scope.UCR_Provider_Type = type;
            $scope.UCR_Provider_Level = level;
        }

    }

    //-------------- ANgular sorting filter --------------
    $scope.order = function (predicate, reverse, section) {
        $scope.selectedSection = section;
        $scope.Licenses = orderBy($scope.Licenses, predicate, reverse);
        $scope.GetCurrentPageData($scope.Licenses, $scope.bigCurrentPage);
    };

    //if (sessionStorage.getItem('myTabs') != null) {

    //    $scope.LicenseData = JSON.parse(sessionStorage.getItem('myTabs'));

    ///}

    //-------------Angular Custom Filter function--------Author Dv----------
    // $scope.tempLicenseData.License = $scope.Licenses.License;







    $scope.getfilteredData = function (filter) {
        $scope.getDataBasedOnCount();
        $scope.SelectedData = $scope.tempLicenseData;
        $scope.filteredData = $filter('filter')($scope.SelectedData, filter);
        $scope.Licenses = $scope.filteredData;
        // $scope.GetCurrentPageData($scope.Licenses);
    };



    //------------Angular searching Filter-----------Author Dv--------------
    $scope.$watch("filterData", function (newVal, oldVal) {

        if (newVal != oldVal) {
            $scope.getfilteredData(newVal);
            $scope.isFiltered = true;
        }
    }, true);

    $scope.$watchCollection('filteredData', function (newValue, oldValue) {
        if (newValue) {
            $scope.GetCurrentPageData(newValue, 1);
            $scope.currentPageNumber = 1;
        }
    });

    $scope.SetFilterDayCount = function (day, id) {
        if (id == 'StateLicense') {
            $scope.SLCount = day;
        }
        else if (id == 'FederalDEA') {
            $scope.FDCount = day;
        }
        else if (id == 'CDSInformation') {
            $scope.CDSCount = day;
        }
        else if (id == 'SpecialityBoard') {
            $scope.SBCount = day;
        }
        else if (id == 'HospitalPrivilages') {
            $scope.HPCount = day;
        }
        else if (id == 'ProfessionalLiability') {
            $scope.PLCount = day;
        }
        else if (id == 'WorkerCompensation') {
            $scope.WCCount = day;
        }
        else if (id == 'MedicareInformation') {
            $scope.MDCount = day;
        }
        else if (id == 'MedicaidInformation') {
            $scope.MRCount = day;
        }
        else if (id == 'CAQHExpiries') {
            $scope.CAQHCount = day;
        }
        else if (id == 'UpComingRecredentials') {
            $scope.UCRCount = day;
        }

    };

    $scope.GetFilterDayCount = function (id) {

        if (id == 'StateLicense') {
            $scope.Selected_Day = $scope.SLCount;
            $("#selectedDay").val($scope.SLCount);
            $scope.providerType = $scope.SL_Provider_Type;
            $scope.providerLevel = $scope.SL_Provider_Level;
            $("#Dashboard_ProviderType").val($scope.SL_Provider_Type);
            $("#Dashboard_ProviderLevel").val($scope.SL_Provider_Level);

        }
        else if (id == 'FederalDEA') {
            $scope.Selected_Day = $scope.FDCount;
            $("#selectedDay").val($scope.FDCount);
            $scope.providerType = $scope.FD_Provider_Type;
            $scope.providerLevel = $scope.FD_Provider_Level;
            $("#Dashboard_ProviderType").val($scope.FD_Provider_Type);
            $("#Dashboard_ProviderLevel").val($scope.FD_Provider_Level);
        }
        else if (id == 'CDSInformation') {
            $scope.Selected_Day = $scope.CDSCount;
            $("#selectedDay").val($scope.CDSCount);
            $scope.providerType = $scope.CDS_Provider_Type;
            $scope.providerLevel = $scope.CDS_Provider_Level;
            $("#Dashboard_ProviderType").val($scope.CDS_Provider_Type);
            $("#Dashboard_ProviderLevel").val($scope.CDS_Provider_Level);
        }
        else if (id == 'SpecialityBoard') {
            $scope.Selected_Day = $scope.SBCount;
            $("#selectedDay").val($scope.SBCount);
            $scope.providerType = $scope.SB_Provider_Type;
            $scope.providerLevel = $scope.SB_Provider_Level;
            $("#Dashboard_ProviderType").val($scope.SB_Provider_Type);
            $("#Dashboard_ProviderLevel").val($scope.SB_Provider_Level);
        }
        else if (id == 'HospitalPrivilages') {
            $scope.Selected_Day = $scope.HPCount;
            $("#selectedDay").val($scope.HPCount);
            $scope.providerType = $scope.HP_Provider_Type;
            $scope.providerLevel = $scope.HP_Provider_Level;
            $("#Dashboard_ProviderType").val($scope.HP_Provider_Type);
            $("#Dashboard_ProviderLevel").val($scope.HP_Provider_Level);
        }
        else if (id == 'ProfessionalLiability') {
            $scope.Selected_Day = $scope.PLCount;
            $("#selectedDay").val($scope.PLCount);
            $scope.providerType = $scope.PL_Provider_Type;
            $scope.providerLevel = $scope.PL_Provider_Level;
            $("#Dashboard_ProviderType").val($scope.PL_Provider_Type);
            $("#Dashboard_ProviderLevel").val($scope.PL_Provider_Level);
        }
        else if (id == 'WorkerCompensation') {
            $scope.Selected_Day = $scope.WCCount;
            $("#selectedDay").val($scope.WCCount);
            $scope.providerType = $scope.WC_Provider_Type;
            $scope.providerLevel = $scope.WC_Provider_Level;
            $("#Dashboard_ProviderType").val($scope.WC_Provider_Type);
            $("#Dashboard_ProviderLevel").val($scope.WC_Provider_Level);
        }
        else if (id == 'MedicareInformation') {
            $scope.Selected_Day = $scope.MDCount;
            $("#selectedDay").val($scope.MDCount);
            $scope.providerType = $scope.MD_Provider_Type;
            $scope.providerLevel = $scope.MD_Provider_Level;
            $("#Dashboard_ProviderType").val($scope.MD_Provider_Type);
            $("#Dashboard_ProviderLevel").val($scope.MD_Provider_Level);
        }
        else if (id == 'MedicaidInformation') {
            $scope.Selected_Day = $scope.MRCount;
            $("#selectedDay").val($scope.MRCount);
            $scope.providerType = $scope.MR_Provider_Type;
            $scope.providerLevel = $scope.MR_Provider_Level;
            $("#Dashboard_ProviderType").val($scope.MR_Provider_Type);
            $("#Dashboard_ProviderLevel").val($scope.MR_Provider_Level);
        }
        else if (id == 'CAQHExpiries') {
            $scope.Selected_Day = $scope.CAQHCount;
            $("#selectedDay").val($scope.CAQHCount);
            $scope.providerType = $scope.CAQH_Provider_Type;
            $scope.providerLevel = $scope.CAQH_Provider_Level;
            $("#Dashboard_ProviderType").val($scope.CAQH_Provider_Type);
            $("#Dashboard_ProviderLevel").val($scope.CAQH_Provider_Level);
        }
        else if (id == 'UpComingRecredentials') {
            $scope.Selected_Day = $scope.UCRCount;
            $("#selectedDay").val($scope.UCRCount);
            $scope.providerType = $scope.UCR_Provider_Type;
            $scope.providerLevel = $scope.UCR_Provider_Level;
            $("#Dashboard_ProviderType").val($scope.providerType);
            $("#Dashboard_ProviderLevel").val($scope.providerLevel);
        }
    };

    //---------------Days Filter for Selected Tab---------Author Dv-----------
    $scope.DayFilterData = angular.copy($scope.Licenses);

    $scope.getDaysLeftForSelectedTab = function (Days, SelectedTab) {
        var Templicenses = [];
        $scope.selectedSection = 6;
        var newTemp = (CCOLicenseService.getAllLicenseDataForTab(SelectedTab));
        $scope.Tab_License_Data = newTemp[0];
        $scope.filterDays = 0;
        $scope.Selected_Day = Days;
        $scope.SetFilterDayCount(Days, SelectedTab);
        $scope.InnerFilterDay = Days;
        if (Days == "" || Days == '-') {
            Templicenses = angular.copy($scope.Tab_License_Data);
            $scope.GetCurrentPageData($scope.Licenses, 1);
        }
        else {

            for (var i in $scope.Tab_License_Data) {
                if ($scope.Tab_License_Data[i].License.dayLeft <= Days) {
                    Templicenses.push($scope.Tab_License_Data[i]);
                }
            }
            //  $scope.LicenseData = CCOLicenseService.LicensesList($scope.LicenseData);

        }
        for (var i in $scope.LicenseData) {
            if ($scope.LicenseData[i].LicenseTypeCode == SelectedTab) {
                $scope.LicenseData[i].Licenses = Templicenses;
                break;
            }
        }
        $scope.LicenseData = CCOLicenseService.GetLicenseStatus($scope.LicenseData);
        $scope.LicenseData = $scope.addFullName($scope.LicenseData);
        $scope.GrandTotalLicenses = CCOLicenseService.GetGrandTotalLicenses();
        $scope.Licenses = angular.copy(Templicenses);
        $scope.GetCurrentPageData($scope.Licenses, 1);
        $scope.newTemp = [];

    };

}]);


dashboardApp.filter("TimeDiff", function () {
    return function (followupDate) {
        var followup = moment(moment(followupDate).format('MM/DD/YYYY'));
        var currentDate = moment(moment(new Date()).format('MM/DD/YYYY'));
        var duration = moment.duration(followup.diff(currentDate));
        var days = duration.asDays();
        return Math.floor(days);
    }
})

dashboardApp.filter("Pro_Name", function () {
    return function (data, Data) {
        for (var i in Data) {
            if (Data[i].ProfileId == data) {
                return Data[i].Name;
            }
        }
    }
})
//dashboardApp.controller("DashBoardTaskTrackerController", ["$scope", "$http", "$q", "$filter", function ($scope, $http, $q, $filter) {
//    $scope.Tasks = [];
//    $scope.InsuranceCompanies = [];
//    $scope.Providers = [];
//    $scope.DisplayData = [];
//    function TasksData() {
//        var defer = $q.defer();
//        $http.get(rootDir + '/TaskTracker/GetAllTasksByUserId').
//            success(function (data, status, headers, config) {
//                $scope.Tasks = angular.copy(data);

//                for (var i in $scope.Tasks) {
//                    $scope.Tasks[i].daysleft = $filter("TimeDiff")($scope.Tasks[i].NextFollowUpDate);
//                    if ($scope.Tasks[i].ModeOfFollowUp != "") {
//                        $scope.Tasks[i].ModeOfFollowUp = JSON.parse($scope.Tasks[i].ModeOfFollowUp);
//                    }
//                }
//                defer.resolve(data);
//            }).
//        error(function (data, status, headers, config) {
//            defer.reject();
//        });
//        return defer.promise;
//    }
//    function ProvidersData() {
//        var defer = $q.defer();
//        $http.get(rootDir + '/TaskTracker/GetAllProviders').
//            success(function (data, status, headers, config) {
//                $scope.Providers = data;

//                defer.resolve(data);
//            }).
//            error(function (data, status, headers, config) {
//                defer.reject();
//            });
//        return defer.promise;
//    }
//    $scope.LoadData = function () {
//        $q.all([$q.when(TasksData()),
//           $q.when(ProvidersData())
//        ]).then(function (response) {
//            var temp = [];
//            var date = new Date($.now());
//            var dateString = (date.getFullYear() + '-'
//            + ('0' + (date.getMonth() + 1)).slice(-2)
//            + '-' + ('0' + (date.getDate())).slice(-2));
//        //    console.log(dateString);
//            for (var i in $scope.Tasks) {
//                $scope.Tasks[i].ProviderName = $filter("Pro_Name")($scope.Tasks[i].ProfileID, $scope.Providers);
//               var x = $scope.Tasks[i].NextFollowUpDate.split('T');
//                var y = x[0];
//                if (y == dateString) {
//                    temp.push($scope.Tasks[i]);
//                }

//            }
//            $scope.DisplayData = angular.copy(temp);
//            console.log($scope.DisplayData);
//        }, function (error) {

//        });
//    };
//    $scope.LoadData();
//    $scope.showDetailViewforTab1 = function () {

//    };
//}]);
$(document).ready(function () {
    $("body").tooltip({ selector: '[data-toggle=tooltip]' });
});