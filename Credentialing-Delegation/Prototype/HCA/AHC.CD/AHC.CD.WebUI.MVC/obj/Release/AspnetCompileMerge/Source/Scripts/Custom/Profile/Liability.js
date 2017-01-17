
profileApp.controller('LiabilityCtrl', function ($scope, $http, dynamicFormGenerateService) {

    $scope.ProfessionalLiabiltys = [
        {
            CarrierName: "Mag Mutual Insurance Company",
            PolicyNumber: "PSL160036713",
            SelfInsured: "No",
            OriginalDate: "02-27-2012",//new Date(2012, 02, 27),
            EffectiveDate: "04-27-2014", //new Date(2014, 04, 27),
            ExpirationDate: "04-27-2015", //new Date(2015, 04, 27),
            Number: 8345,
            Country: "USA",
            State: "Florida",
            County: "",
            City: "Tampa",
            Building: "",
            Street: "Gunn Hwy",
            Zipcode: "33628-_ _ _ _",
            TailCoverage: "No",
            TypeOfCoverage: "Individual",
            DeniedProfessionalInsurance: "No",
            DenialDate: "",
            DenialReason: "",
            UnlimitedCoverage: "No",
            AmountOfCoverageAgg: "$3000000",
            AmountOfCoverageOcc: "$1000000",
            Phone: "",
            Certificate: "/Content/Document/SINGH - LIABILITY INS.pdf"
        }
    ];
    $scope.showDenied = function () {

        console.log("ehjfvbebvfhhefe");
        $scope.showInput = true;
    }

    $scope.hideDenied = function () {

        $scope.showInput = false;
    }
    //=============== Professional Liabilty Conditions ==================
    $scope.editShowProfessionalLiabilty = false;
    $scope.newShowProfessionalLiabilty = false;
    $scope.submitButtonText = "Add";
    $scope.IndexValue = 0;

    //====================== Professional Liabilty ===================

    $scope.addProfessionalLiabilty = function () {
        $scope.newShowProfessionalLiabilty = true;
        $scope.submitButtonText = "Add";
        $scope.professionalLiabilty = {};
        ResetProfessionalLiabiltyForm();
    };

    $scope.editProfessionalLiabilty = function (index, professionalLiabilty) {
        $scope.showViewLiabilityInsurance = false;
        $scope.newShowProfessionalLiabilty = false;
        $scope.editShowProfessionalLiabilty = true;
        $scope.submitButtonText = "Update";
        $scope.professionalLiabilty = professionalLiabilty;
        $scope.IndexValue = index;
    };


    $scope.showLiabilityInsurance = function (index, professionalLiabilty) {
        $scope.newShowProfessionalLiabilty = false;
        $scope.showViewLiabilityInsurance = true;
        $scope.editShowProfessionalLiabilty = false;
        $scope.professionalLiabilty = professionalLiabilty;
        $scope.IndexValue = index;
    }

    $scope.cancelProfessionalLiabilty = function (condition) {
        setProfessionalLiabiltyCancelParameters();
    };

    $scope.saveProfessionalLiabilty = function (professionalLiabilty) {

        console.log(professionalLiabilty);
        console.log("fcees");


        var validationStatus;
        var url;

        if ($scope.newShowProfessionalLiabilty) {
            //Add Details - Denote the URL
            validationStatus = $('#newShowProfessionalLiabiltyDiv').find('form').valid();
            url = "/Profile/ProfessionalLiabilty/AddProfessionalLiabilty?profileId=1";
        }
        else if ($scope.editShowProfessionalLiabilty) {
            //Update Details - Denote the URL
            validationStatus = $('#professionalLiabiltyEditDiv' + $scope.IndexValue).find('form').valid();
            url = "/Profile/ProfessionalLiabilty/UpdateProfessionalLiabilty?profileId=1";
        }

        console.log(professionalLiabilty);

        if (validationStatus) {
            // Simple POST request example (passing data) :
            $http.post(url, professionalLiabilty).
              success(function (data, status, headers, config) {

                  alert("Success");
                  if ($scope.newShowProfessionalLiabilty) {
                      //Add Details - Denote the URL
                      professionalLiabilty.ProfessionalLiabiltyInfoID = data;
                      $scope.ProfessionalLiabiltys.push(professionalLiabilty);
                  }
                  setProfessionalLiabiltyCancelParameters();
              }).
              error(function (data, status, headers, config) {
                  alert("Error");
              });
        }
    };

    function setProfessionalLiabiltyCancelParameters() {
        $scope.showViewLiabilityInsurance = false;
        $scope.editShowProfessionalLiabilty = false;
        $scope.newShowProfessionalLiabilty = false;
        $scope.professionalLiabilty = {};
        $scope.IndexValue = 0;
    }

    function ResetProfessionalLiabiltyForm() {
        $('#newShowProfessionalLiabiltyDiv').find('.professionalLiabiltyForm')[0].reset();
        $('#newShowProfessionalLiabiltyDiv').find('span').html('');
    }
});

$(document).ready(function () {
});