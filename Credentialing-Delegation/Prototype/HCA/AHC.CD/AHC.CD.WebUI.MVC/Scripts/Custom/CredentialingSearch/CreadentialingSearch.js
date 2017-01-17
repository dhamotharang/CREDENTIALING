
angular.module('searchApp', []).controller('credentialingSearchCtrl', function ($scope, $http) {

    


    $scope.showSearchPanelToggle = function (divId) {
        //$(".closePanel").slideUp();
        $("#" + divId).slideToggle();
        $scope.showSearchResults = false;
    };

    var ProvidersInfo = [{

        ApplicationNumber: "CR-APP-101",
        NPI: "1417989625",
        FirstName: "Pariksith",
        LastName: "Singh",
        Group: "American Medical Student Association",
        Plan: "VIP Savings",
        Status: "Approved",
        CredentialingInitiatedDate: "02/12/2015"
    },
   {

       ApplicationNumber: "CR-APP-205",
       NPI: "1417989625",
       FirstName: "Pariksith",
       LastName: "Singh",
       Group: "American Medical Student Association",
       Plan: "Savings COPD",
       Status: "Initiated",
       CredentialingInitiatedDate: "01/22/2015"
   },
   {

       ApplicationNumber: "CR-APP-110",
       NPI: "17231126633",
       FirstName: "Mc.",
       LastName: "Angellica",
       Group: "Medicare Rights Center",
       Plan: "VIP Savings",
       Status: "Initiated",
       CredentialingInitiatedDate: "01/22/2015"
   },
   {

       ApplicationNumber: "CR-APP-643",
       NPI: "37233323438",
       FirstName: "Sachin",
       LastName: "Sharma",
       Group: "California Nurses",
       Plan: "VIP Savings",
       Status: "ReProcess",
       CredentialingInitiatedDate: "02/02/2015"

   },
   {

       ApplicationNumber: "CR-APP-289",
       NPI: "37233323438",
       FirstName: "Sachin",
       LastName: "Sharma",
       Group: "California Nurses",
       Plan: "Savings COPD",
       Status: "Rejected",
       CredentialingInitiatedDate: "01/02/2015"
   }  
   ]


    
    $scope.showProvidersInfo = function () {        
        $scope.Providers = ProvidersInfo;        
        $scope.showSearchPanelToggle('showSearchPanel');
        $scope.showSearchResults = true;
    }   
    
   
    
});