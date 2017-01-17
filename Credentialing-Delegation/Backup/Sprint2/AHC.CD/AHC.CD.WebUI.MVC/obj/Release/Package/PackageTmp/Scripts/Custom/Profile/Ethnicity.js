
//=========================== Controller declaration ==========================
profileApp.controller('ethnicityController', function ($scope, $http, dynamicFormGenerateService) {

    $scope.Visas = [{
        Number: "63",
        Type: "Bussiness",
        Status: "Active",
        Sponsor: "Company ji",
        Expiration: "04 March 2003",
        Certificate: "certificat.pdf"
    }, {
        Number: "6533",
        Type: "Bussiness",
        Status: "Active",
        Sponsor: "Company 1",
        Expiration: "20 June 2013",
        Certificate: "Visacertificat1.pdf"
    }];

    //=============== Visa Conditions ==================
    $scope.visaFormStatus = false;
    $scope.newVisaForm = false;
    $scope.showingDetails = false;

    $scope.addVisa = function () {
        $scope.visaFormStatus = false;
        $scope.newVisaForm = true;
        $scope.Visa = {};
        $("#newVisaFormDiv").html(dynamicFormGenerateService.getForm($scope, $("#visaForm").html()));
    };

    $scope.saveVisa = function (Visa) {
        //================== Save Here ============
        //$scope.Visas.push(Visa);
        //================== hide Show Condition ============
        $scope.visaFormStatus = false;
        $scope.newVisaForm = false;
        $scope.Visa = {};
    };

    $scope.updateVisa = function (Visa) {
        $scope.showingDetails = false;
        $scope.visaFormStatus = false;
        $scope.newVisaForm = false;
        $scope.Visa = {};
    };

    $scope.editVisa = function (index, Visa) {
        $scope.showingDetails = true;
        $scope.visaFormStatus = true;
        $scope.newVisaForm = false;
        $scope.Visa = Visa;
        $("#visaEditDiv" + index).html(dynamicFormGenerateService.getForm($scope, $("#visaForm").html()));
    };

    $scope.cancelVisa = function (condition) {
        if (condition == "editCancel") {
            $scope.showingDetails = false;
            $scope.visaFormStatus = false;
            $scope.newVisaForm = false;
            $scope.Visa = {};
        } else {
            $scope.showingDetails = false;
            $scope.newVisaForm = false;
            $scope.newLegalNameForm = false;
            $scope.Visa = {};
        }
    };

    $scope.removeVisa = function (index) {
        for (var i in $scope.Visas) {
            if (index == i) {
                $scope.Visas.splice(index, 1);
                break;
            }
        }
    };

    $("#selectCity").select2({
        placeholder: "Select a City"
    });

    function resetCitySelection(value) {
        $("#selectCity").select2("val", value);
    }
});





