
profileApp.controller('hospitalcltr', function ($scope, $rootScope, $http, countryDropDownService) {

    $scope.HaveHospitalPrivilege = "True";

    $scope.HospitalPrivilegeInformations = [    
    {
        AddmittingPrivilegeStatus: "Full Unrestricted",
        AdmittingPrivilegeStatus: "true",
        AffiliationEndDate: "-",
        AffilicationStartDate: new Date(2007, 07, 24),
        AnnualAdmisionPercentage: "30",
        ArePrevilegesTemporary: "False",
        City: "",
        ContactPerson: "-",
        ContactPersonFax: "-",
        ContactPersonFaxCountryCode: "+1-",
        ContactPersonPhone: "-",
        ContactPhoneCountryCode: "+1-",
        Country: "US",
        CountryCode: "+1",
        County: "",
        DepartmentChief: "-",
        DepartmentName: "",
        EmailID: "-",
        FaxCountryCode: "+1-",
        FromDate: "",
        FullUnrestrictedPrevilages: "True",
        HaveHospitalPrivilege: "True",
        HospitalFax: "-",
        HospitalName: "Columbia Regional Medical Ctr",
        HospitalPhone: "-",
        HospitalPrivilegeInformationID: "",
        HospitalStatusExplanation: "-",
        HospitalType: "1",
        Number: "",
        Primary: "True",
        Speciality: "",
        StaffCategory: "1",
        StaffCategoryValue: "Active",
        State: "",
        StreetAddress: "",
        ToDate: "",
        Zipcode: "",
        Certificate: ""
    },
    {
        AddmittingPrivilegeStatus: "Active",
        AdmittingPrivilegeStatus: "true",
        AffiliationEndDate: new Date(2016 , 06 , 31),
        AffilicationStartDate: new Date(2014, 07, 01),
        AnnualAdmisionPercentage: "NA",
        ArePrevilegesTemporary: "False",
        City: "Spring Hill",
        ContactPerson: "-",
        ContactPersonFax: "-",
        ContactPersonFaxCountryCode: "+1-",
        ContactPersonPhone: "-",
        ContactPhoneCountryCode: "+1-",
        Country: "US",
        CountryCode: "+1",
        County: "",
        DepartmentChief: "-",
        DepartmentName: "",
        EmailID: "-",
        FaxCountryCode: "+1-",
        FromDate: "",
        FullUnrestrictedPrevilages: "True",
        HaveHospitalPrivilege: "True",
        HospitalFax: "352-5973095",
        HospitalName: "Oak Hill Hospital",
        HospitalPhone: "352-5966632",
        HospitalPrivilegeInformationID: "",
        HospitalStatusExplanation: "-",
        HospitalType: "1",
        Number: "11375",
        Primary: "True",
        Speciality: "Internal Medicine",
        StaffCategory: "1",
        StaffCategoryValue: "Active",
        State: "Florida",
        StreetAddress: "Cortez Boulevard",
        ToDate: "",
        Zipcode: "34611-_ _ _ _",
        Certificate: "/Content/Document/SINGH - HOSPITAL PRIV LTRS.pdf"
    },
    {
        AddmittingPrivilegeStatus: "Active",
        AdmittingPrivilegeStatus: "true",
        AffiliationEndDate: new Date(2016 , 06 , 31),
        AffilicationStartDate: new Date(2011, 05 , 24),
        AnnualAdmisionPercentage: "NA",
        ArePrevilegesTemporary: "False",
        City: "Hudson",
        ContactPerson: "-",
        ContactPersonFax: "-",
        ContactPersonFaxCountryCode: "+1-",
        ContactPersonPhone: "-",
        ContactPhoneCountryCode: "+1-",
        Country: "US",
        CountryCode: "+1",
        County: "",
        DepartmentChief: "-",
        DepartmentName: "",
        EmailID: "-",
        FaxCountryCode: "+1-",
        FromDate: "",
        FullUnrestrictedPrevilages: "True",
        HaveHospitalPrivilege: "True",
        HospitalFax: "727-8615164",
        HospitalName: "Regional Medical Center Bayonet Point",
        HospitalPhone: "727-8632411",
        HospitalPrivilegeInformationID: "",
        HospitalStatusExplanation: "-",
        HospitalType: "1",
        Number: "14000",
        Primary: "False",
        Speciality: "Internal Medicine",
        StaffCategory: "1",
        StaffCategoryValue: "Active",
        State: "Florida",
        StreetAddress: "Fivay Road",
        ToDate: "",
        Zipcode: "34667-_ _ _ _",
        Certificate: "/Content/Document/SINGH - HOSPITAL PRIV LTRS.pdf"
    },
    {
        AddmittingPrivilegeStatus: "Active",
        AdmittingPrivilegeStatus: "true",
        AffiliationEndDate: new Date(2015, 11 ,31),
        AffilicationStartDate: new Date(2013, 12, 01),
        AnnualAdmisionPercentage: "30",
        ArePrevilegesTemporary: "False",
        City: "Brooksville",
        ContactPerson: "-",
        ContactPersonFax: "-",
        ContactPersonFaxCountryCode: "+1-",
        ContactPersonPhone: "-",
        ContactPhoneCountryCode: "+1-",
        Country: "US",
        CountryCode: "+1",
        County: "Hernando",
        DepartmentChief: "-",
        DepartmentName: "Medicine",
        EmailID: "-",
        FaxCountryCode: "+1-",
        FromDate: "10/10/2014",
        FullUnrestrictedPrevilages: "True",
        HaveHospitalPrivilege: "True",
        HospitalFax: "-",
        HospitalName: "Brooksville and Spring Hill Regional Hospital",
        HospitalPhone: "-",
        HospitalPrivilegeInformationID: "Florida Hospital",
        HospitalStatusExplanation: "-",
        HospitalType: "1",
        Number: "5350",
        Primary: "False",
        Speciality: "",
        StaffCategory: "3",
        StaffCategoryValue: "Courtesy",
        State: "Florida",
        StreetAddress: "Spring Hill Drive",
        ToDate: "10/10/2014",
        Zipcode: "34609-_ _ _ _",
        Certificate: "/Content/Document/SINGH - HOSPITAL PRIV LTRS.pdf"
    },
    {
        AddmittingPrivilegeStatus: "Inactive",
        AdmittingPrivilegeStatus: "true",
        AffiliationEndDate: new Date(2012 , 07 , 25),
        AffilicationStartDate: new Date(2010, 07, 26),
        AnnualAdmisionPercentage: "NA",
        ArePrevilegesTemporary: "False",
        City: "Brooksville",
        ContactPerson: "-",
        ContactPersonFax: "-",
        ContactPersonFaxCountryCode: "+1-",
        ContactPersonPhone: "-",
        ContactPhoneCountryCode: "+1-",
        Country: "US",
        CountryCode: "+1",
        County: "",
        DepartmentChief: "-",
        DepartmentName: "",
        EmailID: "-",
        FaxCountryCode: "+1-",
        FromDate: "",
        FullUnrestrictedPrevilages: "True",
        HaveHospitalPrivilege: "True",
        HospitalFax: "-",
        HospitalName: "HealthSouth Rehabilitation Hospital of Spring Hill",
        HospitalPhone: "352-5924250",
        HospitalPrivilegeInformationID: "",
        HospitalStatusExplanation: "-",
        HospitalType: "1",
        Number: "34613",
        Primary: "False",
        Speciality: "",
        StaffCategory: "2",
        StaffCategoryValue: "Inactive",
        State: "Florida",
        StreetAddress: "",
        ToDate: "",
        Zipcode: "34613-_ _ _ _",
        Certificate: ""
    },
    {
        AddmittingPrivilegeStatus: "Inactive",
        AdmittingPrivilegeStatus: "true",
        AffiliationEndDate: new Date(2012, 05, 21),
        AffilicationStartDate: new Date(2009, 12, 22),
        AnnualAdmisionPercentage: "NA",
        ArePrevilegesTemporary: "False",
        City: "Trinity",
        ContactPerson: "-",
        ContactPersonFax: "-",
        ContactPersonFaxCountryCode: "+1-",
        ContactPersonPhone: "-",
        ContactPhoneCountryCode: "+1-",
        Country: "US",
        CountryCode: "+1",
        County: "",
        DepartmentChief: "-",
        DepartmentName: "",
        EmailID: "-",
        FaxCountryCode: "+1-",
        FromDate: "",
        FullUnrestrictedPrevilages: "True",
        HaveHospitalPrivilege: "True",
        HospitalFax: "727- 8344726",
        HospitalName: "Medical Center of Trinity",
        HospitalPhone: "727- 8344000",
        HospitalPrivilegeInformationID: "",
        HospitalStatusExplanation: "-",
        HospitalType: "1",
        Number: "9330",
        Primary: "True",
        Speciality: "Internal Medicine",
        StaffCategory: "2",
        StaffCategoryValue: "Inactive",
        State: "Florida",
        StreetAddress: "State road 54",
        ToDate: "",
        Zipcode: "34655-_ _ _ _",
        Certificate: "/Content/Document/SINGH - HOSPITAL PRIV LTRS.pdf"
    }
    ];

    // rootScoped on emited value catches the value for the model and insert to get the old data
    //$rootScope.$on('HospitalPrivilegeInformations', function (event, val) {        
    //    $scope.HospitalPrivilegeInformations = val;
    //});

    //=============== Hospital Privilege Information Conditions ==================
    $scope.editShowHosPrv = false;
    $scope.newShowHosPrv = false;
    $scope.submitButtonText = "Add";
    $scope.IndexValue = 0;


    $scope.CountryDialCodes = countryDailCodes;


    //====================== Hospital Privilege Information ===================

    $scope.hideAdmit = function () {
        $scope.admit = false;
    }


    $scope.showAdmit = function () {
        $scope.admit = true;
    }


    $scope.addHospitalPrivilegeInformation = function () {
        $scope.newShowHosPrv = true;
        $scope.submitButtonText = "Add";
        $scope.hospitalPrivilegeInformation = {};
        //ResetHosPrvForm();
    };

    $scope.editHospitalPrivilegeInformation = function (index, hospitalPrivilegeInformation) {
        $scope.viewShowHosPrv = false;
        $scope.editShowHosPrv = true;
        $scope.submitButtonText = "Update";
        $scope.hospitalPrivilegeInformation = hospitalPrivilegeInformation;
        $scope.IndexValue = index;
    };

    $scope.viewHospitalPrivilegeInformation = function (index, hospitalPrivilegeInformation) {
        $scope.editShowHosPrv = false;
        $scope.viewShowHosPrv = true;
        $scope.hospitalPrivilegeInformation = hospitalPrivilegeInformation;
        $scope.IndexValue = index;
    };

    $scope.cancelHospitalPrivilegeInformation = function (condition) {
        setHospitalPrivilegeCancelParameters();
    };

    $scope.saveHospitalPrivilegeInformation = function (hospitalPrivilegeInformation) {

        console.log(hospitalPrivilegeInformation);

        var validationStatus;
        var url;

        if ($scope.newShowHosPrv) {
            //Add Details - Denote the URL
            validationStatus = $('#newShowHosPrvDiv').find('form').valid();
            url = "/Profile/HospitalPrivilege/AddHospitalPrivilegeAsync?profileId=1";
        }
        else if ($scope.editShowHosPrv) {
            //Update Details - Denote the URL
            validationStatus = $('#hospitalPrivilegeInformationEditDiv' + $scope.IndexValue).find('form').valid();
            url = "/Profile/HospitalPrivilege/UpdateHospitalPrivilege?profileId=1";
        }

        console.log(hospitalPrivilegeInformation);

        if (validationStatus) {
            hospitalPrivilegeInformation.HospitalPhone = "fghjk";
            hospitalPrivilegeInformation.HospitalFax = "fghjk";
            hospitalPrivilegeInformation.ContactPersonPhone = "fghjk";
            hospitalPrivilegeInformation.ContactPersonFax = "fghjk";

            // Simple POST request example (passing data) :
            $http.post(url, hospitalPrivilegeInformation).
              success(function (data, status, headers, config) {

                  alert("Success");
                  if ($scope.newShowHosPrv) {
                      //Add Details - Denote the URL
                      hospitalPrivilegeInformation.HospitalPrivilegeInformationID = data;
                      $scope.HospitalPrivilegeInformations.push(hospitalPrivilegeInformation);
                  }
                  setHospitalPrivilegeCancelParameters();
              }).
              error(function (data, status, headers, config) {
                  alert("Error");
              });
        }
    };

    function setHospitalPrivilegeCancelParameters() {
        $scope.viewShowHosPrv = false;
        $scope.editShowHosPrv = false;
        $scope.newShowHosPrv = false;
        $scope.hospitalPrivilegeInformation = {};
        $scope.IndexValue = 0;
    }

    function ResetHosPrvForm() {
        $('#newShowHosPrvDiv').find('.hospitalPrivilegeForm')[0].reset();
        $('#newShowHosPrvDiv').find('span').html('');
    }

    $scope.showContryCodeDiv = function (countryCodeDivId) {
        $("#" + countryCodeDivId).show();
    };


    //==================================== Country Code and Country List ========================
    //-------------- country data comes from CountryList.js and countryDialCodes.js---------------
    $scope.Countries = Countries;
    $scope.States = $scope.Countries[1].States;
    $scope.CountryDialCodes = countryDailCodes;
    //---------------------- get states ---------------------
    $scope.getStates = function (countryCode) {
        $scope.States = countryDropDownService.getStates($scope.Countries, countryCode);
        $scope.Counties = [];
        $scope.Cities = [];
        $scope.hospitalPrivilegeInformation.State = $scope.putempty;
        $scope.hospitalPrivilegeInformation.County = $scope.putempty;
        $scope.hospitalPrivilegeInformation.City = $scope.putempty;
        //resetStateSelectTwoStyle();
    }
    $scope.getCounties = function (state) {
        $scope.Counties = countryDropDownService.getCounties($scope.States, state)
        $scope.Cities = [];
        $scope.hospitalPrivilegeInformation.County = $scope.putempty;
        $scope.hospitalPrivilegeInformation.City = $scope.putempty;
    }
    $scope.getCities = function (county) {
        $scope.Cities = countryDropDownService.getCities($scope.Counties, county);
        $scope.hospitalPrivilegeInformation.City = $scope.putempty;
    }
});



