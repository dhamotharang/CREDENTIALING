//Contract Information Controller Angular MOdule
//Author Santosh K.

var ContractInfo = angular.module("Contractinfo", ['ngTable']);

//------------- angular tool tip recall directive ---------------------------
//ContractInfo.directive('tooltip', function () {
//    return function (scope, elem) {
//        elem.tooltip();
//    };
//});
ContractInfo.controller("ContractinfoController", function ($scope,$filter,ngTableParams) {
    $scope.loadingAjax = false;
    $scope.ContractInformation = [
        {
            Name: "Mukesh Satodiya",
            Title: "Medical Doctor",
            Plan: "Wellcare-Medicare",
            Location: "402 Lake In the Woods",
            ProviderID: 101,
            CredentialingStatus: "Approved",
            EffectiveDate: "10/08/2015",
            LocationDetails: {
                Address: "402 Lake In the Woods",
                Plan: "Wellcare-Medicare",
                BE: ["Access", "Access2"],
                LOB: ["Medicare", "Medicaid", "Commercial Exchange"],
                Speciality: ["Internal Medicine"],
                GroupID: 2,
                ProviderID: 101,
                PanelStatus: "Open",
                Phone: "+1-9938395826",
                Fax: "+1-9938395834",
                OfficeManager:"Laura Lofsten"
            },
            FeeDetails: {
                FeeSchedule: "Aetna PPO FS A1",
                EffectiveDate: "10/08/2015",
                TermDate: "26/12/2015",
                SupportingDocument:true
            },
            OtherDetails: {
                InitiatedDate: "14/08/2015",
                InitialCredentialingDate: "26/10/2015",
                RecredentialingDate: "26/10/2018",
                WelcomeLetterMailedDate: "29/10/2018",
                WelcomeLetterDoc:true
            },
            ViewStatus: false,
            EditStatus: false
        },
        {
            Name: "Usha Agrawal",
            Title: "certified Registered Nurse",
            Plan: "Humana",
            Location: "482 Lake In the Woods",
            ProviderID: 102,
            CredentialingStatus: "Pending",
            EffectiveDate: "20/08/2015",
            LocationDetails: {
                Address: "482 Lake In the Woods",
                Plan: "Humana",
                BE: ["Access"],
                LOB: ["Medicare", "Medicaid", "Commercial Exchange"],
                Speciality: ["Internal Medicine"],
                GroupID: 2,
                ProviderID: 102,
                PanelStatus: "Open",
                Phone: "+1-9938395826",
                Fax: "+1-9938395834",
                OfficeManager: "Laura Lofsten"
            },
            FeeDetails: {
                FeeSchedule: "Aetna PPO FS A1",
                EffectiveDate: "20/08/2015",
                TermDate: "26/12/2016",
                SupportingDocument: true
            },
            OtherDetails: {
                InitiatedDate: "14/08/2015",
                InitialCredentialingDate: "26/10/2015",
                RecredentialingDate: "26/10/2018",
                WelcomeLetterMailedDate: "29/10/2018",
                WelcomeLetterDoc: true
            },
            ViewStatus: false,
            EditStatus: false
        },
        {
            Name: "Prakash Rana",
            Title: "Medical Doctor",
            Plan: "Ultimate",
            Location: "Tampa-4008 North America",
            ProviderID: 193,
            CredentialingStatus: "Approved",
            EffectiveDate: "10/08/2016",
            LocationDetails: {
                Address: "Tampa-4008 North America",
                Plan: "Ultimate",
                BE: ["Access"],
                LOB: ["Medicare", "Medicaid", "Commercial Exchange"],
                Speciality: ["Internal Medicine"],
                GroupID: 2,
                ProviderID: 193,
                PanelStatus: "Open",
                Phone: "+1-9938395826",
                Fax: "+1-9938395834",
                OfficeManager: "Laura Lofsten"
            },
            FeeDetails: {
                FeeSchedule: "Aetna PPO FS A1",
                EffectiveDate: "10/08/2016",
                TermDate: "26/12/2016",
                SupportingDocument: true
            },
            OtherDetails: {
                InitiatedDate: "14/08/2015",
                InitialCredentialingDate: "26/10/2015",
                RecredentialingDate: "26/10/2018",
                WelcomeLetterMailedDate: "29/10/2018",
                WelcomeLetterDoc: true
            },
            ViewStatus: false,
            EditStatus: false
        },
        {
            Name: "Monica Singh",
            Title: "certified Registered Nurse",
            Plan: "Wellcare-Medicare",
            Location: "456 Lake In the Woods",
            ProviderID: 221,
            CredentialingStatus: "Pending",
            EffectiveDate: "10/12/2015",
            LocationDetails: {
                Address: "456 Lake In the Woods",
                Plan: "Wellcare-Medicare",
                BE: ["Access"],
                LOB: ["Medicare", "Medicaid", "Commercial Exchange"],
                Speciality: ["Internal Medicine"],
                GroupID: 2,
                ProviderID: 221,
                PanelStatus: "Close",
                Phone: "+1-9938395826",
                Fax: "+1-9938395834",
                OfficeManager: "Laura Lofsten"
            },
            FeeDetails: {
                FeeSchedule: "Aetna PPO FS A1",
                EffectiveDate: "10/12/2015",
                TermDate: "26/12/2015",
                SupportingDocument: false
            },
            OtherDetails: {
                InitiatedDate: "14/08/2015",
                InitialCredentialingDate: "26/10/2015",
                RecredentialingDate: "26/10/2018",
                WelcomeLetterMailedDate: "29/10/2018",
                WelcomeLetterDoc: true
            },
            ViewStatus: false,
            EditStatus: false
        },
        {
            Name: "Jude A Pierre",
            Title: "Medical Doctor",
            Plan: "Humana",
            Location: "Tampa-4144 North America,Tampa Florida",
            ProviderID: 105,
            CredentialingStatus: "Approved",
            EffectiveDate: "19/08/2015",
            LocationDetails: {
                Address: "Tampa-4144 North America,Tampa Florida",
                Plan: "Humana",
                BE: ["Access"],
                LOB: ["Medicare", "Medicaid", "Commercial Exchange"],
                Speciality: ["Internal Medicine"],
                GroupID: 2,
                ProviderID: 105,
                PanelStatus: "Open",
                Phone: "+1-9938395826",
                Fax: "+1-9938395834",
                OfficeManager: "Laura Lofsten"
            },
            FeeDetails: {
                FeeSchedule: "Aetna PPO FS A1",
                EffectiveDate: "19/08/2015",
                TermDate: "26/12/2015",
                SupportingDocument: true
            },
            OtherDetails: {
                InitiatedDate: "14/08/2015",
                InitialCredentialingDate: "26/10/2015",
                RecredentialingDate: "26/10/2018",
                WelcomeLetterMailedDate: "29/10/2018",
                WelcomeLetterDoc: true
            },
            ViewStatus: false,
            EditStatus: false
        },
    ];
    $scope.removeContractInfo = function (data) {
        var id = $scope.data.indexOf(data);
        if (id > -1) {
            $scope.data.splice(id, 1);
            $scope.tableParams1.reload();
        }
    }

    $scope.viewContractInfo = function (data) {
        for (var i = 0; i < $scope.data.length; i++) {
            if ($scope.data[i] != data) {
                $scope.data[i].ViewStatus = false;
            }
        }
        $scope.tableParams1.reload();
        data.ViewStatus = !data.ViewStatus;
        
    }

    console.log($scope.ContractInformation);

    //Created function to be called when data loaded dynamically
    $scope.init_table = function (data) {

        $scope.data = data;
        var counts = [];

        if ($scope.data.length <= 10) {
            counts = [];
        }
        else if ($scope.data.length <= 25) {
            counts = [10, 25];
        }
        else if ($scope.data.length <= 50) {
            counts = [10, 25, 50];
        }
        else if ($scope.data.length <= 100) {
            counts = [10, 25, 50, 100];
        }
        else if ($scope.data.length > 100) {
            counts = [10, 25, 50, 100];
        }
        
        $scope.tableParams1 = new ngTableParams({
            page: 1,            // show first page
            count: 10,          // count per page
            filter: {
                //name: 'M'       // initial filter
                //FirstName : ''
            },
            sorting: {
                //name: 'asc'     // initial sorting
            }
        }, {
            counts: counts,
            total: $scope.data.length, // length of data
            getData: function ($defer, params) {
                // use build-in angular filter
                var filteredData = params.filter() ?
                        $filter('filter')($scope.data, params.filter()) :
                        $scope.data;
                var orderedData = params.sorting() ?
                        $filter('orderBy')(filteredData, params.orderBy()) :
                        $scope.data;

                params.total(orderedData.length); // set total for recalc pagination
                $defer.resolve(orderedData.slice((params.page() - 1) * params.count(), params.page() * params.count()));
            }
        });

    };

    //if filter is on
    $scope.ifFilter = function () {
        try {
            var bar;
            var obj = $scope.tableParams1.$params.filter;
            for (bar in obj) {
                if (obj[bar] != "") {
                    return false;
                }
            }
            return true;
        }
        catch (e) { return true; }
    }

    //Get index first in table
    $scope.getIndexFirst = function () {
        try {
            if ($scope.groupBySelected == 'none') {
                return ($scope.tableParams1.$params.page * $scope.tableParams1.$params.count) - ($scope.tableParams1.$params.count - 1);
            }
        }
        catch (e) { }
    }

    //Get index Last in table
    $scope.getIndexLast = function () {
        try {
            if ($scope.groupBySelected == 'none') {
                return { true: ($scope.data.length), false: ($scope.tableParams1.$params.page * $scope.tableParams1.$params.count) }[(($scope.tableParams1.$params.page * $scope.tableParams1.$params.count) > ($scope.data.length))];
            }
        }
        catch (e) { }
    }




    $scope.init_table($scope.ContractInformation);

});