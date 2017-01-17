
profileApp.controller('LiabilityCtrl', function ($scope,$rootScope,$http, dynamicFormGenerateService,masterDataService) {

    $scope.ProfessionalLiabiltys = [
        //{
        //    CarrierName: "Mag Mutual Insurance Company",
        //    PolicyNumber: 12,
        //    SelfInsured: "Yes",
        //    OriginalDate:  new Date(20013, 10, 11),
        //    EffectiveDate:new Date(2013, 07, 02),
        //    ExpirationDate: new Date(2013, 10, 10),
        //    Number: 701,
        //    Country: "USA",
        //    State: "Florida",
        //    County: "Bay",
        //    City: "Spring Hill",
        //    Building: "",
        //    Street: "West Plymouth Avenue",
        //    Zipcode: 74597,
        //    TailCoverage: "Yes",
        //    TypeOfCoverage: "Shared",
        //    DeniedProfessionalInsurance: "No",
        //    DenialDate: "",
        //    DenialReason: "",
        //    UnlimitedCoverage: "Yes",
        //    AmountOfCoverageAgg: "10000",
        //    AmountOfCoverageOcc: "12000",
        //    Phone:1375135
          
      
        //}
    ];

    $scope.submitButtonText = "Add";

    $scope.getAddress = function () {
        $scope.LocationAddress = $scope.masterInsuranceCarrierAddresses.filter(function (masterInsuranceCarrierAddresses) { return masterInsuranceCarrierAddresses.InsuranceCarrierAddressID == $scope.tempObject.InsuranceCarrierAddressID })[0];
        $scope.tempObject.InsuranceCarrierAddress = $scope.LocationAddress;
    };

    ////******************************Master Data*****************************
    $scope.masterInsuranceCarriers = [];


    masterDataService.getMasterData("/Profile/MasterData/GetAllInsuranceCarriers").then(function (masterInsuranceCarriers) {
        $scope.masterInsuranceCarriers = masterInsuranceCarriers;
    });

    masterDataService.getMasterData("/Profile/MasterData/GetAllInsuranceCarrierAddresses").then(function (masterInsuranceCarrierAddress) {
        $scope.masterInsuranceCarrierAddresses = masterInsuranceCarrierAddress;
        console.log("Addressssss...............");
        console.log(masterInsuranceCarrierAddress);
    });

    //calling the method using $on(PSP-public subscriber pattern)
    $rootScope.$on('ProfessionalLiabilityInfoes', function (event, val) {
        $scope.ProfessionalLiabilityInfoes = val;
        for (var i = 0; i < $scope.ProfessionalLiabilityInfoes.length ; i++) {
            $scope.ProfessionalLiabilityInfoes[i].OriginalEffectiveDate = ConvertDateFormat($scope.ProfessionalLiabilityInfoes[i].OriginalEffectiveDate);
            $scope.ProfessionalLiabilityInfoes[i].EffectiveDate = ConvertDateFormat($scope.ProfessionalLiabilityInfoes[i].EffectiveDate);
            $scope.ProfessionalLiabilityInfoes[i].ExpirationDate = ConvertDateFormat($scope.ProfessionalLiabilityInfoes[i].ExpirationDate);
        }
    });



    //====================== Professional Liability ===================


    $scope.cancelProfessionalLiabilty = function (condition) {
        setProfessionalLiabiltyCancelParameters();
    };

    $scope.saveProfessionalLiabilty = function (professionalLiabilty,index) {

     
        

        var validationStatus;
        var url;
        var $formData1;


        if ($scope.visibilityControl == 'addLi') {
            //Add Details - Denote the URL
            validationStatus = $('#newShowProfessionalLiabiltyDiv').find('form').valid();
            $formData1 = $('#newShowProfessionalLiabiltyDiv').find('form');
            url = "/Profile/ProfessionalLiability/AddProfessionalLiabilityAsync?profileId=" + profileId;
        }
        else if ($scope.visibilityControl == (index + '_editLi')) {
            //Update Details - Denote the URL
            validationStatus = $('#professionalLiabiltyEditDiv' + index).find('form').valid();
            $formData1 = $('#professionalLiabiltyEditDiv' + index).find('form');
            url = "/Profile/ProfessionalLiability/UpdateProfessionalLiabilityAsync?profileId=" + profileId;
        }

      

        if (validationStatus) {
            // Simple POST request example (passing data) :
            $.ajax({
                url: url,
                type: 'POST',
                data: new FormData($formData1[0]),
                async: false,
                cache: false,
                contentType: false,
                processData: false,
                success: function (data) {
                    if (data.status == "true") {
                        data.professionalLiability.OriginalEffectiveDate = ConvertDateFormat(data.professionalLiability.OriginalEffectiveDate);
                        data.professionalLiability.EffectiveDate = ConvertDateFormat(data.professionalLiability.EffectiveDate);
                        data.professionalLiability.ExpirationDate = ConvertDateFormat(data.professionalLiability.ExpirationDate);
                        if ($scope.visibilityControl != (index + '_editLi'))
                            $scope.ProfessionalLiabilityInfoes.push(data.professionalLiability);
                        else
                            $scope.ProfessionalLiabilityInfoes[index] = data.professionalLiability;
                        $scope.IsProfLiabilityHasError = false;
                        $rootScope.tempObject = {};
                        $rootScope.visibilityControl = "";
                        FormReset(form);

                    } else {
                        $scope.IsProfLiabilityHasError = true;
                        $scope.ProfLiabilityErrorList = data.status.split(",");
                    }
                }
            });
        }
    };

   

    function ResetProfessionalLiabiltyForm() {
        $('#newShowProfessionalLiabiltyDiv').find('.professionalLiabiltyForm')[0].reset();
        $('#newShowProfessionalLiabiltyDiv').find('span').html('');
    }
});

$(document).ready(function () {
});