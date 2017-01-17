initCredApp.controller('singleProviderCtrl', ['$scope', '$http', '$timeout', '$filter', 'ngTableParams', function ($scope, $http, $timeout, $filter, ngTableParams) {

    var CredentialingInitiationViewModel = {};
    $scope.TwoDocuments = false;
    $scope.OneDocuments = false;
    var ConvertDateFormat = function (value) {
        if (value) {

        return new Date(parseInt(value.replace("/Date(", "").replace(")/", ""), 10));
    } else {
        return value;
    }
};
    $scope.allPlanProviders = [
        { "Plan": "Humana", "Group": "Access", "Specialty": "Anesthesiology", "Location": "5350 Spring Hill Drive", "CredentialledDate": "05-03-2014" },
        { "Plan": "Freedom HMO", "Group": "Access", "Specialty": "Addiction Medicine", "Location": "5350 Spring Hill Drive", "CredentialledDate": "03-03-2014" },
        { "Plan": "Freedom HMO", "Group": "Access2", "Specialty": "Addiction Medicine", "Location": "5350 Spring Hill Drive", "CredentialledDate": "01-02-2014" },
        { "Plan": "Wellcare", "Group": "MIRRA", "Specialty": "Dermatology", "Location": "5350 Spring Hill Drive", "CredentialledDate": "05-03-2013" },
    ];

    $scope.temp = '';

    $scope.pushplan = function (data) {
        $scope.temp = data;
    }
    $scope.showDocDiv = function () {
        $scope.TwoDocuments = true;
    };
    $scope.showDocDecred = function () {
        $scope.OneDocuments = true;
    };
    $scope.SearchProviderPanelToggle = function (divId) {

        $("#" + divId).slideToggle();

    };
    $scope.SearchProviderPanelToggleDown = function (divId) {
        $(".closePanel").slideUp();
        $("#" + divId).slideToggle();

    };
    var data = [];
    $scope.SearchFilter = function (obj) {

        CredentialingInitiationViewModel = {
            'FirstName': obj.Firstname,
            'LastName': obj.LastName,
            'NPI': obj.NPI,
            'CAQH': obj.CAQH,
            'Type': obj.Type,
            'Groups': obj.Groups,
            'Specialities': obj.Specialities,
            'Plan': obj.Plan,
            'CredType': obj.CredType,
            'CredDate': obj.CredDate,


        }
       
        $scope.loadingAjax = true;
        $scope.allProviders = [];
        $scope.Npi = null;
        $http({
            method: 'GET',
            url: '/InitCredentialing/GetAllProviders?tempObject=CredentialingInitiationViewModel'
        }).success(function (response, status, headers, config) {

            $scope.allProviders = response;

            data = response;
            console.log(data);
            for (var i = 0; i < $scope.allProviders.length ; i++) {
                $scope.allProviders[i].CredDate = ConvertDateFormat($scope.allProviders[i].CredDate).toDateString();
            }
            $scope.SearchProviderPanelToggleDown('SearchProviderResultPanel');
            $scope.loadingAjax = false;
            $scope.SearchProviderTable.reload();
        }).error(function (data, status, headers, config) {
            $scope.loadingAjax = false;
            
        });


    };


  
    $scope.SearchProviderTable = new ngTableParams(
    {
        page: 1,            // show first page
        count: 5          // count per page
    },
    {
        total: data.length,
        getData: function ($defer, params) {



            var orderedData = params.filter() ?
                                $filter('filter')(data, params.filter()) :
                                data;
            params.total(orderedData.length);
            $defer.resolve(orderedData.slice((params.page() - 1) * params.count(), params.page() * params.count()));


        }
    });


    $scope.clearSearch = function () {
        $scope.allProviders = null;
        //$('a[href=#SearchResult]').trigger('click');
        $scope.Npi = null;
    };

    $scope.SelectProviderForCredentialingInitiation = function (provObj) {
        //$scope.SearchProviderPanelToggle('SearchProviderResultPanel');        
        $scope.SearchProviderPanelToggleDown('InitiationPanel');
        $scope.initiateSuccess = false;
        $scope.ImageSrc = provObj.Image;
        $scope.Npi = provObj.NPI;
        $scope.Firstname = provObj.FirstName;
        $scope.Middlename = provObj.MiddleName;
        $scope.Lastname = provObj.LastName;
        $scope.Type = provObj.Type;
        $scope.Specilities = provObj.Specialities;
        $scope.Groups = provObj.Groups;
        //var tabhighlight = "initCred";
        //$('a[href=#' + tabhighlight + ']').trigger('click');
        $scope.TwoDocuments = false;
    };

    var resetMsg = function () {
        $scope.msgAlert = false;
    };

    $scope.InitiateCredentialing = function () {
        $scope.msgAlert = true;
        $scope.initiateSuccess = true;
        //$('a[href=#SearchResult]').trigger('click');
        //$scope.Npi = null;
        //$scope.SearchProviderPanelToggle('SearchProviderPanel');
        //$scope.allProviders = [];
        $timeout(resetMsg, 5000);
    };

}]);