

var PanelToggle = function (id) {
    $('#' + id).slideToggle();
}

//Module declaration
var credentialinginitiateapp = angular.module('CredentialingInitiateApp', []);
//Controller declaration
credentialinginitiateapp.controller('CredentialingInitiateAppCtrl', function ($scope, $http) {

    // models init

    $scope.credentialInit = {};

    $scope.credentialInit.SelectedPlans = [];

    $scope.credentialInit.Remarks = "";

    $scope.credentialInit.ProviderID = providerId;
    
    $scope.next = function () {

        $scope.credentialInit.SelectedPlans = new Array();
        $('#bootstrap-duallistbox-selected-list_duallistbox_demo1>option').each(function (index, elem) {

            $scope.credentialInit.SelectedPlans.push($(elem).val());

        });

        if ($scope.getSelectedPlanObj().length!=0){
            $('#formscreen').slideUp(function () { $('#conformscreen').slideDown(); });
            $('body,html').animate({
                scrollTop: 0
            });
        } else {            
            $scope.selectPlanError = "Please Select At Least One Plan !!!!";
            $('body,html').animate({
                scrollTop: $('#planerror').offset().top
            });                      
        }
    }

    

    $scope.prev = function () {
        $scope.error_message = ""
        $('#conformscreen').slideUp(function () { $('#formscreen').slideDown(); });
    }

    $scope.submitCredential = function () {
        $scope.error_message = ""
        // commented for UI demo
        //$http.post('/Credentialing/InitCredentialing', $scope.credentialInit).success(function (recData) {

        //    $('#conformscreen').effect('explode', function () { $('#finalscreen').slideDown(); });

        //    $('body,html').animate({
        //        scrollTop: $('#toppanel').offset().top
        //    });

        //}).error(function () {
        //    $scope.error_message = "Some thing went Wrong!! Please Try Again!!"
        //    console.log("Some thing went wrong please try later");

        //});

        $('#conformscreen').effect('explode', function () { $('#finalscreen').slideDown(); });

        $('body,html').animate({
            scrollTop: $('#toppanel').offset().top
        });
    }

    /* services to get JSON data
       Dev : K Ashok Kumar
    */
    $scope.obtainPlanList = [];
    $http.get('/Credentialing/Plans?providerID='+$scope.credentialInit.ProviderID).success(function (recData) {
        $scope.obtainPlanList = recData;

        var optionsContent = "";
        angular.forEach(recData, function (value, key) {
            optionsContent = optionsContent + "<option value='" + value.PlanID + "'>" + value.Title + "</option>";
        });

        initDualListBox(optionsContent);

    }).error(function () {
        $scope.error_message = "Some thing went Wrong!! Please Try Again!!"
        console.log("Some thing went wrong please try later");

    });

    $scope.inexistingPlans = [];
    $http.get('/Credentialing/ExistingPlans?providerID='+$scope.credentialInit.ProviderID).success(function (recData) {
        $scope.inexistingPlans = recData;  

    }).error(function () {
        console.log("Some thing went wrong please try later");
    });

    $scope.getSelectedPlanObj = function () {
        $scope.selectedList = [];
        for (var i in $scope.credentialInit.SelectedPlans) {
            for (var j in $scope.obtainPlanList) {
                if ($scope.obtainPlanList[j].PlanID.toString() == $scope.credentialInit.SelectedPlans[i]) {
                    var temp = $scope.obtainPlanList[j];                    
                    $scope.selectedList.push(temp);
                }
            }
        }
        return $scope.selectedList;
    }

    $scope.panel = 0;
    $scope.changetab = function (index) {
        $scope.panel = index;
    }

    function initDualListBox(optionsContent) {
        $('[name=duallistbox_demo1]').append(optionsContent);
        var demo1 = $('[name=duallistbox_demo1]').bootstrapDualListbox();       
    }
});