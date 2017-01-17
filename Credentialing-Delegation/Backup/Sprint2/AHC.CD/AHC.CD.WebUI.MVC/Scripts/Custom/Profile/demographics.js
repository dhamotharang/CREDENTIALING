//============================ Directive for return Template ===========================
//========= Dynamic Form Generator ==================================
profileApp.service('dynamicFormGenerateService', function ($compile) {
    this.getForm = function (scope, formContain) {
        return $compile(formContain)(scope)
    };
});

//=========================== Controller declaration ==========================
profileApp.controller('profileDemographicsController', function ($scope, $http, dynamicFormGenerateService) {

    // Demographics control variable initialization

    $scope.OtherLegalNames = [{
        FirstName: "Ram",
        MiddleName: "Krishna",
        LastName: "Shankar",
        StartUsing: "20-06-2006",
        StopUsing: "28-12-2013",
        Certificate: "changeNameApproval.pdf"
    }, {
        FirstName: "Aho",
        MiddleName: "K",
        LastName: "John",
        StartUsing: "20-06-2006",
        StopUsing: "28-12-2013",
        Certificate: "newNameApproval.docx"
    }, {
        FirstName: "Giri",
        MiddleName: "B",
        LastName: "Giri",
        StartUsing: "20-06-2006",
        StopUsing: "28-12-2013",
        Certificate: "certifiedName.pdf"
    }, {
        FirstName: "K",
        MiddleName: "John",
        LastName: "Smith",
        StartUsing: "20-06-2006",
        StopUsing: "28-12-2013",
        Certificate: "newchangenow.docx"
    }];

    $scope.HomeAddresses = [{
        UnitNumber: "#6533",
        Country: "USA",
        Street: "34604, Masaryktown",
        State: "Florida",
        City: "Putname",
        County: "County1",
        Zipcode: "2846",
        StartLiving: "20-02-2000",
        StopLiving: "12-11-2011"
    }, {
        UnitNumber: "#444",
        Street: "5350 Spring Hill Drive",
        Country: "USA",
        State: "Florida",
        City: "Putname",
        County: "County2",
        Zipcode: "44544",
        StartLiving: "20-12-2000",
        StopLiving: "22-11-2001"
    }];

    //=============== Other Legal Name Conditions ==================
    $scope.legalNameFormStatus = false;
    $scope.showingDetails = false;
    $scope.newLegalNameForm = false;
    //$scope.selectedOtherLegalName = {};

    //=============== Home Address Conditions ==================
    $scope.homeAddressFormStatus = false;
    $scope.newHomeAddressForm = false;

    $scope.addOtherLegalName = function () {
        $scope.legalNameFormStatus = false;
        $scope.newLegalNameForm = true;
        $scope.OtherLegalName = {};
        $("#newOtherLegalFormDiv").html(dynamicFormGenerateService.getForm($scope, $("#otherLegalNameForm").html()));
    };

    $scope.saveOtherLegalName = function (OtherLegalName) {
        //================== Save Here ============
        //$scope.OtherLegalNames.push(OtherLegalName);
        //================== hide Show Condition ============
        $scope.legalNameFormStatus = false;
        $scope.newLegalNameForm = false;
        $scope.OtherLegalName = {};
    };

    $scope.updateOtherLegalName = function (OtherLegalName) {
        $scope.showingDetails = false;
        $scope.legalNameFormStatus = false;
        $scope.newLegalNameForm = false;
        $scope.OtherLegalName = {};
    };

    $scope.editOtherLegalName = function (index, OtherLegalName) {
        $scope.showingDetails = true;
        $scope.legalNameFormStatus = true;
        $scope.newLegalNameForm = false;
        $scope.OtherLegalName = OtherLegalName;
        $("#otherLegalNameEditDiv" + index).html(dynamicFormGenerateService.getForm($scope, $("#otherLegalNameForm").html()));
    };

    $scope.cancelOtherLegalName = function (condition) {
        if (condition == "editCancel") {
            $scope.showingDetails = false;
            $scope.legalNameFormStatus = false;
            $scope.newLegalNameForm = false;
            $scope.OtherLegalName = {};
        } else {
            $scope.showingDetails = false;
            $scope.legalNameFormStatus = false;
            $scope.newLegalNameForm = false;
            $scope.OtherLegalName = {};
        }
    };

    $scope.removeOtherLegalName = function (index) {
        for (var i in $scope.OtherLegalNames) {
            if (index == i) {
                $scope.OtherLegalNames.splice(index, 1);
                break;
            }
        }
    };

    //====================== Home Address ===================

    $scope.addHomeAddress = function () {
        $scope.homeAddressFormStatus = false;
        $scope.newHomeAddressForm = true;
        $scope.HomeAddress = {};
        $("#newHomeAddressFormDiv").html(dynamicFormGenerateService.getForm($scope, $("#homeAddressForm").html()));
    };

    $scope.saveHomeAddress = function (HomeAddress) {
        //================== Save Here ============
        //$scope.HomeAddresses.push(HomeAddress);
        //================== hide Show Condition ============
        $scope.homeAddressFormStatus = false;
        $scope.newHomeAddressForm = false;
        $scope.HomeAddress = {};
    };

    $scope.updateHomeAddress = function (HomeAddress) {
        $scope.showingDetails = false;
        $scope.homeAddressFormStatus = false;
        $scope.newHomeAddressForm = false;
        $scope.HomeAddress = {};
    };

    $scope.editHomeAddress = function (index, HomeAddress) {
        $scope.showingDetails = true;
        $scope.homeAddressFormStatus = true;
        $scope.newHomeAddressForm = false;
        $scope.HomeAddress = HomeAddress;
        $("#homeAddressEditDiv" + index).html(dynamicFormGenerateService.getForm($scope, $("#homeAddressForm").html()));
    };

    $scope.cancelHomeAddress = function (condition) {
        if (condition == "editCancel") {
            $scope.showingDetails = false;
            $scope.homeAddressFormStatus = false;
            $scope.newHomeAddressForm = false;
            $scope.HomeAddress = {};
        } else {
            $scope.showingDetails = false;
            $scope.homeAddressFormStatus = false;
            $scope.newHomeAddressForm = false;
            $scope.HomeAddress = {};
        }

    };

    $scope.removeHomeAddress = function (index) {
        for (var i in $scope.HomeAddresses) {
            if (index == i) {
                $scope.HomeAddresses.splice(index, 1);
                break;
            }
        }
    };


    $scope.phone = 0;
    $scope.phoneList = [];
    $scope.increase=function (newValue) {
        for (var i = $scope.phone; i < newValue; i++) {
            $scope.phoneList.push(i);
        }
        $scope.phone = newValue;
    };

    $scope.decrease = function (index) {
        for (var i in $scope.phoneList) {
            if (index == i) {
                $scope.phoneList.splice(index, 1);
                break;
            }
        }
    };

   




    $scope.fax = 0;
    $scope.faxList = [];
    $scope.increase1 = function (newValue) {
        for (var i = $scope.fax; i < newValue; i++) {
            $scope.faxList.push(i);
        }
        $scope.fax = newValue;
    };
    $scope.decrease1 = function (index) {
        for (var i in $scope.faxList) {
            if (index == i) {
                $scope.faxList.splice(index, 1);
                break;
            }
        }
    };


  


    $scope.mobile = 0;
    $scope.mobileList = [];
    $scope.increase2 = function (newValue) {
        for (var i = $scope.mobile; i < newValue; i++) {
            $scope.mobileList.push(i);
        }
        $scope.mobile = newValue;
    };
    $scope.decrease2 = function (index) {
        for (var i in $scope.mobileList) {
            if (index == i) {
                $scope.mobileList.splice(index, 1);
                break;
            }
        }
    };
  



    $scope.email = 0;
    $scope.emailList = [];
    $scope.increase3 = function (newValue) {
        for (var i = $scope.email; i < newValue; i++) {
            $scope.emailList.push(i);
        }
        $scope.email = newValue;
    };
    $scope.decrease3 = function (index) {
        for (var i in $scope.emailList) {
            if (index == i) {
                $scope.emailList.splice(index, 1);
                break;
            }
        }
    };



});