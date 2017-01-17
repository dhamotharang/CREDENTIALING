



Cred_SPA_App.controller('delegationProfileCtrl', function ($scope, $http, $rootScope,$filter) {

    $scope.ProfileID = sessionStorage.getItem('ProfileID');
    $scope.showLoading = true;

    $scope.delegationProfileObj = null;
    $scope.medicalEducation = [];
    $scope.languagesKnown = null;
    $scope.PrimarySpecility = null;
    $scope.SecondarySpecility = [];
    $scope.edited = sessionStorage.getItem('selectDocumentBit') == 'true' ? true : false;
    $scope.templateName = null;
    $scope.templateCode = null;
    $scope.remarks = null;
    $scope.showEditDiv = function () {
        $scope.edited = true;
        $scope.display = false;
    };

    $scope.ViewOnlyMode = sessionStorage.getItem('ViewOnlyMode') == 'true' ? true : false;
    //---date format-------------
    $scope.reformatDate = function (dateStr, year) {

        if (dateStr != null) {
            dArr1 = dateStr.split("T");//2015-06-26T00:00:00
            dArr2 = dArr1[0].split("-");

            // return dArr2[2] + 'th ' + monthNames[dArr2[1] - 1] + ',' + dArr2[0];
            return dArr2[1] + '-' + dArr2[2] + '-' + (parseInt(dArr2[0]) + year);
        }

    }

    //------------------------------------24hrs to 12 hrs------------------------------

    $scope.ConvertTo12Hrs = function (time) {
        if (time != null) {
            var times = time.split('-');
            var startTime = ConvertTime(times[0]);
            var endTime = ConvertTime(times[1]);

            return startTime + ' - ' + endTime;
        }
    }
    var ConvertTime = function (time) {

        var HRs = time.split(':');
        var hr = parseInt(HRs[0]);
        var hh = '';
        var AMPM = '';
        if (hr > 12) {
            hh = hr % 12;
            AMPM = 'PM';
        }
        else {
            hh = hr;
            AMPM = 'AM';
        }

        return hh + ':' + HRs[1] + ' ' + AMPM;
    }
    //---------------------------------------------------------------------------------------------------------------------------
    //-----------------------------------------------check in test---------------------------------------------------------------
    //---------------------------------------------------------------------------------------------------------------------------

    var ModifyDateFormat = function (date) {
        var dts = date.split('-');
        return dts[1] + '-' + dts[0] + '-' + dts[2];
    }

    $scope.GetProfileDataByIdAsync = function () {

        var ProviderPracitceInfoBusinessModel = JSON.parse(sessionStorage.getItem('ProviderPracitceInfoBusinessModel'));
        var providerProfessionalDetailBusinessModel = JSON.parse(sessionStorage.getItem('providerProfessionalDetailBusinessModel'));


        $http.post(rootDir + '/Credentialing/DelegationProfileReport/GetProviderProfile', { profileId: $scope.ProfileID, Locations: ProviderPracitceInfoBusinessModel, Specialtis: providerProfessionalDetailBusinessModel }).
  success(function (data, status, headers, config) {
      if (data.status == 'true') {
          //------------------------------selected template name----------------------------
          $scope.templateName = sessionStorage.getItem('templateName');
          $scope.templateCode = sessionStorage.getItem('templateCode');

          $scope.delegationProfileObj = data.profileReport;

          if ($scope.delegationProfileObj.DOB != null) {
              $scope.delegationProfileObj.DOB = ModifyDateFormat($scope.delegationProfileObj.DOB);
          }
          //----------------------------initial credential date-------------------------------------
          InitialCredentialingDate = sessionStorage.getItem('InitialCredentialingDate');

          if (InitialCredentialingDate != 'null') {
              $scope.delegationProfileObj.CredentialDate = $scope.reformatDate(InitialCredentialingDate, 0);
              $scope.delegationProfileObj.ReCredentialCycle1Date = $scope.reformatDate(InitialCredentialingDate, 3);

              $scope.delegationProfileObj.ReCredentialCycle2Date = $scope.reformatDate(InitialCredentialingDate, 6);
          } else {
              $scope.delegationProfileObj.CredentialDate = '';
              $scope.delegationProfileObj.ReCredentialCycle1Date = '';

              $scope.delegationProfileObj.ReCredentialCycle2Date = '';
          }

          $scope.remarks = $scope.delegationProfileObj.Remarks;

          //------------------------------Medical Education-----------------------------------------
          if ($scope.delegationProfileObj.ProviderMedicalEducations != null) {
              for (var i = 0; i < $scope.delegationProfileObj.ProviderMedicalEducations.length; i++) {
                  var obj = $scope.delegationProfileObj.ProviderMedicalEducations[i];
                  var DegreeEarned = obj.DegreeEarned != null ? obj.DegreeEarned : '';
                  $scope.medicalEducation.push(obj.SchoolName + ', ' + obj.YearsAttended + ', ' + DegreeEarned);
              }
          }

          //----------------------------Languages----------------------------------------------------
          if ($scope.delegationProfileObj.Languages != null) {
              $scope.languagesKnown = $scope.delegationProfileObj.Languages[0];
              //$scope.languagesKnown = 'English';
              for (var i = 1; i < $scope.delegationProfileObj.Languages.length; i++) {
                  var obj = $scope.delegationProfileObj.Languages[i];
                  $scope.languagesKnown = $scope.languagesKnown + ', ' + obj;

              }
          } else {
              $scope.languagesKnown = 'English';
          }

          //-------------------------Primary Specility------------------------------


          if ($scope.delegationProfileObj.ProviderProfessionalDetails != null) {
              for (var i = 0; i < $scope.delegationProfileObj.ProviderProfessionalDetails.length; i++) {
                  var obj = $scope.delegationProfileObj.ProviderProfessionalDetails[i];

                  if (i == 0) {
                      $scope.PrimarySpecility = obj;
                  } else {
                      $scope.SecondarySpecility.push(obj);
                  }
              }
          }


          //-------------------------------------practice information----------------------
          if ($scope.delegationProfileObj.ProviderPracitceInfos != null) {
              for (var i = 0; i < $scope.delegationProfileObj.ProviderPracitceInfos.length; i++) {
                  $scope.delegationProfileObj.ProviderPracitceInfos[i].OfficeHourMonday = $scope.ConvertTo12Hrs($scope.delegationProfileObj.ProviderPracitceInfos[i].OfficeHourMonday);
                  $scope.delegationProfileObj.ProviderPracitceInfos[i].OfficeHourTuesday = $scope.ConvertTo12Hrs($scope.delegationProfileObj.ProviderPracitceInfos[i].OfficeHourTuesday);
                  $scope.delegationProfileObj.ProviderPracitceInfos[i].OfficeHourWednesday = $scope.ConvertTo12Hrs($scope.delegationProfileObj.ProviderPracitceInfos[i].OfficeHourWednesday);
                  $scope.delegationProfileObj.ProviderPracitceInfos[i].OfficeHourThursday = $scope.ConvertTo12Hrs($scope.delegationProfileObj.ProviderPracitceInfos[i].OfficeHourThursday);
                  $scope.delegationProfileObj.ProviderPracitceInfos[i].OfficeHourFridayday = $scope.ConvertTo12Hrs($scope.delegationProfileObj.ProviderPracitceInfos[i].OfficeHourFridayday);
                  if ($scope.delegationProfileObj.ProviderPracitceInfos[i].PhoneNumber != null) {
                      $scope.delegationProfileObj.ProviderPracitceInfos[i].PhoneNumber = $scope.delegationProfileObj.ProviderPracitceInfos[i].PhoneNumber.substring(3);
                  }
                  if ($scope.delegationProfileObj.ProviderPracitceInfos[i].FaxNumber != null) {
                      $scope.delegationProfileObj.ProviderPracitceInfos[i].FaxNumber = $scope.delegationProfileObj.ProviderPracitceInfos[i].FaxNumber.substring(3);
                  }
                  if ($scope.delegationProfileObj.ProviderPracitceInfos[i].BillingPhoneNumber != null) {
                      $scope.delegationProfileObj.ProviderPracitceInfos[i].BillingPhoneNumber = $scope.delegationProfileObj.ProviderPracitceInfos[i].BillingPhoneNumber.substring(3);
                  }
                  if ($scope.delegationProfileObj.ProviderPracitceInfos[i].BillingFaxNumber != null) {
                      $scope.delegationProfileObj.ProviderPracitceInfos[i].BillingFaxNumber = $scope.delegationProfileObj.ProviderPracitceInfos[i].BillingFaxNumber.substring(3);
                  }
              }
          }

          //-----------------------------------hospital affiliate---------------------
          $scope.ProviderHospitalAffiliations = [];
          if ($scope.delegationProfileObj.ProviderHospitalAffiliations != null) {
              var flag = 0;
              if ($scope.delegationProfileObj.ProviderHospitalAffiliations.length % 2 != 0) {
                  $scope.delegationProfileObj.ProviderHospitalAffiliations.push({ HospitalName: '', EffectiveDates: '' });
                  flag = 1;
              }

              for (var i = 0; i < $scope.delegationProfileObj.ProviderHospitalAffiliations.length; i++) {

                  if (i % 2 == 0) {
                      var obj = new Object();
                      obj.leftObj = $scope.delegationProfileObj.ProviderHospitalAffiliations[i];
                  } else {
                      obj.rightObj = $scope.delegationProfileObj.ProviderHospitalAffiliations[i];
                      $scope.ProviderHospitalAffiliations.push(obj);
                  }

              }
              if (flag == 1) {
                  $scope.delegationProfileObj.ProviderHospitalAffiliations.splice($scope.delegationProfileObj.ProviderHospitalAffiliations.length - 1, 1);
              }
          }


          if ($scope.delegationProfileObj.ProviderPracitceInfos != null) {
              if ($scope.delegationProfileObj.ProviderPracitceInfos[0].CoveringPhysicians != null) {
                  var coveringPhy = [];
                  for (var k = 0; k < $scope.delegationProfileObj.ProviderPracitceInfos.length; k++) {
                      console.log($scope.delegationProfileObj.ProviderPracitceInfos[k].CoveringPhysicians);
                      for (var m = 0; m < $scope.delegationProfileObj.ProviderPracitceInfos[k].CoveringPhysicians.length; m++) {

                          var obj = $scope.delegationProfileObj.ProviderPracitceInfos[k].CoveringPhysicians[m];
                          coveringPhy.push(obj);
                      }
                  }
                  $scope.delegationProfileObj.CoveringPhysicians = coveringPhy;
              }
          }
      }
      $scope.showLoading = false;
  }).
  error(function (data, status, headers, config) {
      // called asynchronously if an error occurs
      // or server returns response with an error status.
  });



    };


    $scope.SaveDelegationProfileReport = function (remarks, PrintId) {

        $scope.remarks = remarks;
        $scope.showLoading = false;

        var profileReportViewModel = {};
        if ($scope.profileReport != null) {
            profileReportViewModel.ProfileReportId = $scope.profileReport.ProfileReportId;
            profileReportViewModel.TemplateName = $scope.templateName;
            profileReportViewModel.TemplateCode = $scope.templateCode;
        } else {
            profileReportViewModel.TemplateName = sessionStorage.getItem('templateName');
            profileReportViewModel.TemplateCode = sessionStorage.getItem('templateCode');
        }
        profileReportViewModel.ProfileReportData = JSON.stringify($scope.delegationProfileObj);
        profileReportViewModel.StatusType = 'Active';
        profileReportViewModel.Remarks = remarks;
        id = sessionStorage.getItem('CredentialingContractRequestID');


        $http.post(rootDir + '/Credentialing/DelegationProfileReport/SaveDelegationProfileReport', { requestId: id, report: profileReportViewModel }).
  success(function (data, status, headers, config) {


      $scope.PrintElem(PrintId);
      $scope.showLoading = true;

      //var credentialingInfoId=sessionStorage.getItem('credentialingInfoId');
      //window.location.assign('/Credentialing/CnD/Application?id=' + credentialingInfoId+'#/load_to_plan');

      // this callback will be called asynchronously
      // when the response is available
  }).
  error(function (data, status, headers, config) {
      // called asynchronously if an error occurs
      // or server returns response with an error status.
  });
    }

    $scope.profileReport = null;
    $scope.ViewDelegationProfileReport = function () {

        var profileReport = JSON.parse(sessionStorage.getItem('profileReport'));

        $scope.remarks = profileReport.Remarks;
        console.log('profileReport');
        console.log(profileReport);

        $scope.templateName = profileReport.TemplateName;
        $scope.templateCode = profileReport.TemplateCode;
        $scope.profileReport = profileReport;



        $scope.delegationProfileObj = jQuery.parseJSON($scope.profileReport.ProfileReportData);
        console.log($scope.delegationProfileObj);
        //------------------------------Medical Education-----------------------------------------
        for (var i = 0; i < $scope.delegationProfileObj.ProviderMedicalEducations.length; i++) {
            var obj = $scope.delegationProfileObj.ProviderMedicalEducations[i];

            $scope.medicalEducation.push(obj.SchoolName + ', ' + obj.YearsAttended + ', ' + obj.DegreeEarned);
        }


        //----------------------------Languages----------------------------------------------------
        if ($scope.delegationProfileObj.Languages != null) {
            $scope.languagesKnown = $scope.delegationProfileObj.Languages[0];
            for (var i = 1; i < $scope.delegationProfileObj.Languages.length; i++) {
                var obj = $scope.delegationProfileObj.Languages[i];
                $scope.languagesKnown = $scope.languagesKnown + ', ' + obj;

            }
        }

        //-------------------------Primary Specility------------------------------
        for (var i = 0; i < $scope.delegationProfileObj.ProviderProfessionalDetails.length; i++) {
            var obj = $scope.delegationProfileObj.ProviderProfessionalDetails[i];
            //if (obj.Preference == 'Primary') {
            //    $scope.PrimarySpecility = obj;
            //}
            //else {
            //    $scope.SecondarySpecility.push(obj);
            //}
            if (i == 0) {
                $scope.PrimarySpecility = obj;
            }
            else {
                $scope.SecondarySpecility.push(obj);
            }
        }
        $scope.showLoading = false;
        //--------------
    };

    $scope.GoToLOB = function () {

        window.close();

        //var credentialingInfoId = sessionStorage.getItem('credentialingInfoId');
        //window.location.assign('/Credentialing/CnD/Application?id=' + credentialingInfoId + '#/load_to_plan');
    };


    //------------------------print-----------------------------------------------

    $scope.PrintElem = function (elem) {
        Popup($('#' + elem).html());
    }

    function Popup(data) {
        var mywindow = window.open('', data, 'height=600,width=1000');
        mywindow.document.write('<html><head><title></title>');
        mywindow.document.write('<link rel="stylesheet" href="/Content/SharedCss/bootstrap.min.css" type="text/css" />');
        mywindow.document.write('<link rel="stylesheet" href="/Content/SharedCss/app.css" type="text/css" />');
        mywindow.document.write('</head><body >');
        mywindow.document.write(data);
        mywindow.document.write('</body></html>');

        mywindow.document.close(); // necessary for IE >= 10
        mywindow.focus(); // necessary for IE >= 10

        setTimeout(function () {
            mywindow.print();
            mywindow.close();

            window.close();
        }, 500);


        return true;
    }


    $scope.PrintElem1 = function (elem) {
        Popup1($('#' + elem).html());
    }

    function Popup1(data) {
        var mywindow = window.open('', data, 'height=400,width=600');
        mywindow.document.write('<html><head><title></title>');
        mywindow.document.write('<link rel="stylesheet" href="/Content/SharedCss/bootstrap.min.css" type="text/css" />');
        mywindow.document.write('<link rel="stylesheet" href="/Content/SharedCss/app.css" type="text/css" />');
        mywindow.document.write('</head><body >');
        mywindow.document.write(data);
        mywindow.document.write('</body></html>');

        mywindow.document.close(); // necessary for IE >= 10
        mywindow.focus(); // necessary for IE >= 10

        setTimeout(function () {
            mywindow.print();
            mywindow.close();

        }, 500);
        return true;
    }

});