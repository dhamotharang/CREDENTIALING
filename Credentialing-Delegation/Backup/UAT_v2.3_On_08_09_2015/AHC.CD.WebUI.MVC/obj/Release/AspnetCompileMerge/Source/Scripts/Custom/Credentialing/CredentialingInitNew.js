
//Module declaration
var providerapp = angular.module('ProviderApp', []);

//Controller declaration

providerapp.controller('ProviderCtrlNew', function ($scope, $http) {

    $("#sidemenu").addClass("menu-in");
    $("#page-wrapper").addClass("menuup");

    $http.get(rootDir + '/InitCredentialing/GetAllProviders').success(function (recData) {
        $scope.allProviders = recData;
        console.log(recData);
    });

    // Scope Variables Declaration and Initiation

    $scope.providers = [];

    $scope.currentProvider = null;

    $scope.currentPlan = null;

    $scope.allPlans = [];

    $scope.selectedPlans = [];

    $scope.searchByProviderType = false;

    $scope.planSearhed=false;

    $scope.initiated = false;

    $scope.saved = false;


    

    $('#specialities').select2();
    $('#groups').select2();
    $('#plans').select2();
    $('#plansnew').select2();

    // Hardcoded ProviderID

    $http.get(rootDir + '/InitCredentialing/Plans?providerID=1').success(function (recData) {
        $scope.allPlans = recData;

        //var optionsContent = "";
        //angular.forEach(recData, function (value, key) {
        //    optionsContent = optionsContent + "<option value='" + value.PlanID + "'>" + value.Title + "</option>";
        //});

        //initDualListBox(optionsContent);

    }).error(function () {
        $scope.error_message = "Some thing went Wrong!! Please Try Again!!"
        console.log("Some thing went wrong please try later");

    });



    $scope.addSelectedPlan = function (index) {

        $scope.selectedPlans.push($scope.allPlans[index]);

    }


    $scope.addSelectedProvider = function (index) {

        var name = $scope.allProviders[index].FirstName + " " + $scope.allProviders[index].MiddleName + " " + $scope.allProviders[index].LastName;
        var image = $scope.allProviders[index].Image;
        var title = $scope.allProviders[index].Title;
        var relatedPlans = $scope.allProviders[index].relatedPlans;

        $scope.providers.push({ "Name": name, "Title": title, "Image": image, "relatedPlans": relatedPlans });

        var name = null;
        var image = null;
        var title = null;
        var relatedPlans = null;
        $scope.allProviders[index].IsSelected = false;

    }

    $scope.deleteSelectedPlan = function (index) {


        for (var i in $scope.selectedPlans) {
            if (index == i) {
                $scope.selectedPlans.splice(index, 1);
                break;
            }
        }

    }

    $scope.unselectProvider = function (index) {

        for (var i in $scope.providers) {
            if (index == i) {
                $scope.providers.splice(index, 1);
                break;
            }
        }

    }

    $scope.addAllSelected = function () {
       
        for (var i in $scope.allProviders) {
            
            if ($scope.allProviders[i].IsSelected == true) {
                $scope.addSelectedProvider(i);
                $scope.allProviders[i].IsSelected = false;
            }
        }

       

    }


    $scope.selectProvider = function (index) {

        $scope.allProviders[index].IsSelected = !$scope.allProviders[index].IsSelected;
    }

    $scope.setCurrentProvider = function (index) {
        $scope.currentPlan = null;
        $scope.currentProvider = $scope.providers[index];


    }

    $scope.showPlanDetails = function (index) {


        $scope.currentPlan = $scope.currentProvider.relatedPlans[index];


    }


    // New plan association duallist

    function initDualListBox(optionsContent) {
        $('[name=newPlanList]').append(optionsContent);
        $('[name=newPlanList]').bootstrapDualListbox();
    }


});


// search panel toogle

var SearchProviderPanelToggle = function (divId) {
   
    $("#" + divId).slideToggle();

}