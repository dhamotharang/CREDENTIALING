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

profileApp.controller('profileAppCtrl', ['$scope', '$rootScope', '$http', 'masterDataService', 'messageAlertEngine', '$filter', '$sce', 'ProviderLicenseService', function ($scope, $rootScope, $http, masterDataService, messageAlertEngine, $filter, $sce, ProviderLicenseService) {

    $scope.IsMissingCredentialing = false;
    $scope.IsUpcomingRenewal = false;
    $scope.IsUpcomingReCredentialing = false;
    $scope.IsPsvDone = false;
    $scope.IsInitiateCredentialing = false;
    $scope.loading = false;

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
      if ($scope.providerdata.HospitalPrivileges != null) {
          $scope.HospitalDetailCount = 0;
          for (var i = 0; i < $scope.providerdata.HospitalPrivileges.HospitalPrivilegeDetails.length; i++) {
              if ($scope.providerdata.HospitalPrivileges.HospitalPrivilegeDetails[i].Status == "Active")
              {
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
              if ($scope.providerdata.ContractGroupInformation[i].ContractGroupInfoes.length != 0)
              {
                  for (var j = 0; j < $scope.providerdata.ContractGroupInformation[i].ContractGroupInfoes.length; j++) {
                      if ($scope.providerdata.ContractGroupInformation[i].ContractGroupInfoes[j].Status == "Active") {

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
          if($scope.providerdata.PersonalDetail == null){
              count = count + 1;
           }
          if($scope.providerdata.HomeAddresses.length == 0){
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
  }).
  error(function (data, status, headers, config) {
  });

    //------------- get list of plan for which provider is associated ---------------
  $scope.PlanListIDs = [];
  $http.get(rootDir + '/Credentialing/Initiation/getPlanListforCredentialingContractRequest?ProviderID=' + profileId).
      success(function (data, status, headers, config) {
          if (data.status == true) {
              $scope.PlanList = angular.copy(data.data1);  
              for (var i = 0; i < $scope.PlanList.length; i++) {
                  $scope.PlanListIDs.push($scope.PlanList[i].PlanID);
              }
          }
      })
      .error(function (data, status, headers, config) {
      });


  $http.get(rootDir + '/Credentialing/Verification/GetAllPSVList').
    success(function (data, status, headers, config) {
        for (var i = 0; i < data.length; i++) {
            if (data[i].ProfileID == profileId) {
                $scope.psvData.push(data[i]);
            }
        }
        if ($scope.psvData.length != 0) {
            $scope.creddata = $scope.psvData[0];
            for (var i = 0; i < $scope.psvData.length; i++) {
                if ($scope.creddata.LastModifiedDate < $scope.psvData[i].LastModifiedDate) {
                    $scope.creddata = $scope.psvData[i];
                }
            }
            console.log("===========");
            console.log($scope.creddata);
        }
        else {
            $scope.errorInitiatedForNoPSV = false;
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

    $scope.review1 = true;

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
        $scope.CredentialingContractRequests = [];
        $scope.structuredata = [];
        $scope.loading = true;

        if ($scope.PlanListIDs.length != 0) {
            $http.get(rootDir + '/Credentialing/Initiation/getCredentialingContractRequestForAllPlan', { params: { "ProviderID": profileId, "PlanIDs": $scope.PlanListIDs } }).
                success(function (data, status, headers, config) {
                    $scope.errorInitiated = true;
                    if (data.status == true) {
                        $scope.Credinfo = angular.copy(data.data1);
                       
                        for (var i = 0; i < $scope.Credinfo.length; i++) {
                            if ($scope.Credinfo[i].CredentialingContractRequests.length != 0) {
                                for (var j = 0; j < $scope.Credinfo[i].CredentialingContractRequests.length; j++) {
                                    if ($scope.Credinfo[i].CredentialingContractRequests.length != 0) {
                                        var CredentialingContract = [];
                                        if ($scope.Credinfo[i].CredentialingContractRequests[j].Status == 'Active' && $scope.Credinfo[i].CredentialingContractRequests[j].ContractRequestStatus == 'Active') {
                                            CredentialingContract.push($scope.Credinfo[i].CredentialingContractRequests[j]);
                                            $scope.CredentialingContractRequests.push($scope.Credinfo[i].CredentialingContractRequests[j]);
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
                        $scope.loadingAjax1 = false;
                        if ($scope.CredentialingContractRequests.length == 0) {
                            messageAlertEngine.callAlertMessage('errorInitiated', "", "danger", true);
                            $scope.errorInitiated = "Provider is not credentialed for any plan";
                            $scope.errorInitiated = false;
                        }
                        $scope.NotSelected = angular.copy($scope.CredentialingContractRequests);
                    }

                    $scope.review1 = false;
                    $scope.review2 = false;
                    $scope.review3 = true;
                    $scope.review4 = false;
                    $scope.review5 = false;
                    $scope.loading = false;

            }).error(function (data, status, headers, config) {
                $scope.loading = false;
                });
        }
        else {
            $scope.review1 = false;
            $scope.review2 = false;
            $scope.review3 = true;
            $scope.review4 = false;
            $scope.review5 = false;
            $scope.errorInitiated = false;
            $scope.loading = false;
        }
    }

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
            $scope.loadData($scope.creddata.CredentialingInfoID);
            $scope.showPsvError = false;
            $scope.review1 = false;
            $scope.review2 = false;
            $scope.review3 = false;
            $scope.review4 = true;
            $scope.review5 = false;
        }
    }

    $scope.FormattedData = [];

    $scope.loadData = function (id) {

        $http.get(rootDir + '/Credentialing/Verification/InitiateNewPSVForQuickUpdate?profileId=' + profileId + '&credInfoId=' + id).
        success(function (data, status, headers, config) {
            console.log("sakhgdskdjhfk");
            console.log(data);
            if (status == 200) {
                $scope.showPsvError = false;
                $scope.FormatData(data.PSVData);
            }
            else {
                $scope.showPsvError = true;
            }
        }).
        error(function (data, status, headers, config) {
            messageAlertEngine.callAlertMessage("psvReportError", "Sorry Unable To get PSV Report !!!!", "danger", true);
        });
    };

    $scope.FormatData = function (data) {
        $scope.FormattedData = [];
        var formattedData = [];
        for (var i in data) {
            var VerificationData = new Object();
            if (data[i].VerificationData != null)
                VerificationData = jQuery.parseJSON(data[i].VerificationData);
            var VerificationDate = $scope.ConvertDateFormat1(data[i].VerificationDate)

            formattedData.push({ Id: data[i].ProfileVerificationParameterId, info: { ProfileVerficationParameterObj: data[i].ProfileVerificationParameter, VerificationResultObj: data[i].VerificationResult, VerificationData: VerificationData, VerificationDate: VerificationDate } });
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
        console.log("Formmatted data");
        console.log($scope.FormattedData);
    };

    $scope.ConvertDateFormat1 = function (value) {
        ////console.log(value);
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
        $http.post(rootDir + '/Credentialing/Initiation/InitiateCredentialing', obj).
            success(function (data, status, headers, config) {
                //----------- success message -----------
                if (data.status == "true") {
                    messageAlertEngine.callAlertMessage('successfulInitiated', "Credentialing Initiated Successfully. !!!!", "success", true);
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


}]);



