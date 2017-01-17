
profileApp.controller('viewPlanAppCtrl', function ($scope, $filter, ngTableParams) {

    data = [{ PlanName: "AVMEDBLUE CROSS BLUE SHIELD", PlanImage: "AVMED.jpg", PlanType: "COMMERCIAL LOB", Location: "", Group: "", Speciality: "", Status: "", EffectiveDate: "8/1/2011", TermDate: "" }
        , { PlanName: "BLUE CROSS BLUE SHIELD", PlanImage: "BLUE CROSS BLUE SHIELD.png", PlanType: "TRADITIONAL (PPS PARTICIPATING SVC)", Location: "", Group: "", Speciality: "", Status: "", EffectiveDate: "6/1/2011", TermDate: "5/1/2014" }
        , { PlanName: "BLUE CROSS BLUE SHIELD", PlanImage: "BLUE CROSS BLUE SHIELD.png", PlanType: "PREFERRED PATIENT CARE PPO", Location: "", Group: "", Speciality: "", Status: "", EffectiveDate: "6/1/2011", TermDate: "5/1/2014" }
        , { PlanName: "BLUE CROSS BLUE SHIELD", PlanImage: "BLUE CROSS BLUE SHIELD.png", PlanType: "NETWORK BLUE", Location: "", Group: "", Speciality: "", Status: "", EffectiveDate: "6/1/2011", TermDate: "5/1/2014" }
        , { PlanName: "BLUE CROSS BLUE SHIELD", PlanImage: "BLUE CROSS BLUE SHIELD.png", PlanType: "HEALTH OPTIONS HMO", Location: "", Group: "", Speciality: "", Status: "", EffectiveDate: "6/1/2011", TermDate: "11/30/2013" }
        , { PlanName: "BLUE CROSS BLUE SHIELD", PlanImage: "BLUE CROSS BLUE SHIELD.png", PlanType: "BLUE MEDICARE PPO", Location: "", Group: "", Speciality: "", Status: "", EffectiveDate: "6/1/2011", TermDate: "11/30/2013" }
        , { PlanName: "BLUE CROSS BLUE SHIELD", PlanImage: "BLUE CROSS BLUE SHIELD.png", PlanType: "BLUE MEDICARE HMO", Location: "", Group: "", Speciality: "", Status: "", EffectiveDate: "6/1/2011", TermDate: "11/30/2013" }
        , { PlanName: "BLUE CROSS BLUE SHIELD", PlanImage: "BLUE CROSS BLUE SHIELD.png", PlanType: "BCBS Advantage 65", Location: "", Group: "", Speciality: "", Status: "", EffectiveDate: "6/1/2011", TermDate: "5/1/2014" }
        , { PlanName: "BLUE CROSS BLUE SHIELD", PlanImage: "BLUE CROSS BLUE SHIELD.png", PlanType: "BLUE SELECT", Location: "", Group: "", Speciality: "", Status: "", EffectiveDate: "6/1/2011", TermDate: "5/1/2014" }
        , { PlanName: "CHAMPUS / TRICARE PRIME NETWORK", PlanImage: "CHAMPUS  TRICARE PRIME NETWORK.jpg", PlanType: "PRIME NETWORK", Location: "", Group: "", Speciality: "", Status: "", EffectiveDate: "2/1/2012", TermDate: "4/28/2014" }
        , { PlanName: "CHAMPUS / TRICARE STANDARD", PlanImage: "CHAMPUS  TRICARE PRIME NETWORK.jpg", PlanType: "", Location: "", Group: "", Speciality: "", Status: "", EffectiveDate: "2/1/2012", TermDate: "4/28/2014" }
        , { PlanName: "CHAMPUS / TRICARE STANDARD", PlanImage: "CHAMPUS  TRICARE PRIME NETWORK.jpg", PlanType: "STANDARD", Location: "", Group: "", Speciality: "", Status: "", EffectiveDate: "6/1/2011", TermDate: "4/28/2014" }
       , { PlanName: "CHOICE PROVIDER NETWORK ", PlanImage: "CHOICE PROVIDER NETWORK.png", PlanType: "WORKERS COMP", Location: "", Group: "", Speciality: "", Status: "", EffectiveDate: "1/20/2014", TermDate: "" }
       , { PlanName: "CIGNA & GREAT WEST FFS", PlanImage: "CIGNA & GREAT WEST FFS.jpg", PlanType: "HMO / PPO", Location: "", Group: "", Speciality: "", Status: "", EffectiveDate: "7/1/2011", TermDate: "" }
       , { PlanName: "COVENTRY HEALTH CARE", PlanImage: "COVENTRY HEALTH CARE.png", PlanType: "COMMERCIAL LOB", Location: "", Group: "", Speciality: "", Status: "", EffectiveDate: "7/1/2011", TermDate: "" }
       , { PlanName: "COVENTRY HEALTH(IPA)", PlanImage: "COVENTRY HEALTH CARE.png", PlanType: "MEDICARE RISK", Location: "", Group: "", Speciality: "", Status: "", EffectiveDate: "9/1/2011", TermDate: "" }
       , { PlanName: "FREEDOM HEALTH(IPA)", PlanImage: "FREEDOM HEALTH(IPA).jpg", PlanType: "MEDICARE RISK", Location: "", Group: "", Speciality: "", Status: "", EffectiveDate: "7/1/2011", TermDate: "" }
       , { PlanName: "GHI", PlanImage: "GHI.jpg", PlanType: "", Location: "PPO", Group: "", Speciality: "", Status: "", EffectiveDate: "9/1/2011", TermDate: "" }
       , { PlanName: "HERITAGE SUMMIT", PlanImage: "HERITAGE SUMMIT.jpg", PlanType: "WORKERS COMP", Location: "", Group: "", Speciality: "", Status: "", EffectiveDate: "1/1/2012", TermDate: "" }
       , { PlanName: "HUMANA FEE FOR SVC  ", PlanImage: "HUMANA FEE FOR SVC.jpg", PlanType: "PPO", Location: "", Group: "", Speciality: "", Status: "", EffectiveDate: "7/1/2011", TermDate: "" }
       , { PlanName: "HUMANA FEE FOR SVC  ", PlanImage: "HUMANA FEE FOR SVC.jpg", PlanType: "COMMERCIAL HMO", Location: "", Group: "", Speciality: "", Status: "", EffectiveDate: "7/1/2011", TermDate: "" }
       , { PlanName: "HUMANA FEE FOR SVC  ", PlanImage: "HUMANA FEE FOR SVC.jpg", PlanType: "COMMERCIAL HMO", Location: "", Group: "", Speciality: "", Status: "", EffectiveDate: "9/1/2012", TermDate: "" }
       , { PlanName: "HUMANA FEE FOR SVC  ", PlanImage: "HUMANA FEE FOR SVC.jpg", PlanType: "COMMERCIAL LOB", Location: "", Group: "", Speciality: "", Status: "", EffectiveDate: "7/1/2011", TermDate: "" }
       , { PlanName: "HUMANA MEDICARE GOLD HMO (IPA", PlanImage: "HUMANA FEE FOR SVC.jpg", PlanType: "MEDICARE RISK", Location: "", Group: "", Speciality: "", Status: "", EffectiveDate: "6/1/2010", TermDate: "" }
       , { PlanName: "HUMANA MEDICARE GOLD HMO (IPA", PlanImage: "HUMANA FEE FOR SVC.jpg", PlanType: "Access 2 Health Care LLC", Location: "", Group: "", Speciality: "", Status: "", EffectiveDate: "", TermDate: "" }
       , { PlanName: "HUMANA MEDICARE GOLD HMO (IPA", PlanImage: "HUMANA FEE FOR SVC.jpg", PlanType: "MEDICARE RISK", Location: "", Group: "", Speciality: "", Status: "", EffectiveDate: "9/1/2012", TermDate: "" }
       , { PlanName: "INTEGRAL QUALITY CARE", PlanImage: "INTEGRAL QUALITY CARE.jpg", PlanType: "Network - Third Pary Network", Location: "", Group: "", Speciality: "", Status: "", EffectiveDate: "", TermDate: "" }
       , { PlanName: "MAGELLAN HEALTH SERVICES INC", PlanImage: "MAGELLAN HEALTH SERVICES INC.png", PlanType: "", Location: "", Group: "", Speciality: "", Status: "", EffectiveDate: "", TermDate: "" }
       , { PlanName: "MEDICAID", PlanImage: "MAGELLAN HEALTH SERVICES INC.png", PlanType: "MEDICAID LOB", Location: "", Group: "", Speciality: "", Status: "", EffectiveDate: "5/25/2011", TermDate: "11/2/2011" }
       , { PlanName: "MEDICAID", PlanImage: "MEDICAID.jpg", PlanType: "MEDICAID LOB", Location: "", Group: "", Speciality: "", Status: "", EffectiveDate: "8/9/2012", TermDate: "" }
       , { PlanName: "MEDICAID - Access Lab LLC", PlanImage: "MEDICAID.jpg", PlanType: "", Location: "", Group: "", Speciality: "", Status: "", EffectiveDate: "11/7/2013", TermDate: "" }
        , { PlanName: "MEDICAID LOCATION 2 INFO", PlanImage: "MEDICAID.jpg", PlanType: "NOT HMO", Location: "", Group: "", Speciality: "", Status: "", EffectiveDate: "9/1/2011", TermDate: "" }
        , { PlanName: "MEDICAID LOCATION 3 INFO", PlanImage: "MEDICAID.jpg", PlanType: "NOT HMO", Location: "", Group: "", Speciality: "", Status: "", EffectiveDate: "9/1/2011", TermDate: "" }
        , { PlanName: "MEDICAID LOCATION 4 INFO", PlanImage: "MEDICAID.jpg", PlanType: "NOT HMO", Location: "", Group: "", Speciality: "", Status: "", EffectiveDate: "9/1/2011", TermDate: "" }
        , { PlanName: "MEDICAID LOCATION 5 INFO", PlanImage: "MEDICAID.jpg", PlanType: "NOT HMO", Location: "", Group: "", Speciality: "", Status: "", EffectiveDate: "9/1/2011", TermDate: "" }
        , { PlanName: "MEDICAID LOCATION 6 INFO", PlanImage: "MEDICAID.jpg", PlanType: "NOT HMO", Location: "", Group: "", Speciality: "", Status: "", EffectiveDate: "9/1/2011", TermDate: "" }
        , { PlanName: "MEDICAID LOCATION 7 INFO", PlanImage: "MEDICAID.jpg", PlanType: "NOT HMO", Location: "", Group: "", Speciality: "", Status: "", EffectiveDate: "9/1/2011", TermDate: "" }
        , { PlanName: "MEDICAID LOCATION 8 INFO", PlanImage: "MEDICAID.jpg", PlanType: "NOT HMO", Location: "", Group: "", Speciality: "", Status: "", EffectiveDate: "9/1/2011", TermDate: "" }
        , { PlanName: "MEDICAID RURAL HLTH CARE ", PlanImage: "MEDICAID.jpg", PlanType: "", Location: "", Group: "", Speciality: "", Status: "", EffectiveDate: "12/20/2012", TermDate: "" }
        , { PlanName: "MEDICARE - Access Lab LLC", PlanImage: "MEDICARE - Access Lab LLC.jpg", PlanType: "", Location: "", Group: "", Speciality: "", Status: "", EffectiveDate: "10/24/2013", TermDate: "" }
        , { PlanName: "MEDICARE LOCALITY 01", PlanImage: "MEDICARE - Access Lab LLC.jpg", PlanType: "", Location: "", Group: "", Speciality: "", Status: "", EffectiveDate: "", TermDate: "" }
       , { PlanName: "MEDICARE LOCALITY 02", PlanImage: "MEDICARE - Access Lab LLC.jpg", PlanType: "MEDICARE LOB", Location: "", Group: "", Speciality: "", Status: "", EffectiveDate: "", TermDate: "" }
       , { PlanName: "MEDICARE LOCALITY 03", PlanImage: "MEDICARE - Access Lab LLC.jpg", PlanType: "NOT HMO", Location: "", Group: "", Speciality: "", Status: "", EffectiveDate: "6/1/2011", TermDate: "" }
       , { PlanName: "MULTIPLAN", PlanImage: "MULTIPLAN.png", PlanType: "", Location: "", Group: "", Speciality: "", Status: "", EffectiveDate: "1/1/2005", TermDate: "" }
       , { PlanName: "OPTIMUM HEALTH (IPA)", PlanImage: "OPTIMUM HEALTH (IPA).jpg", PlanType: "MEDICARE RISK", Location: "", Group: "", Speciality: "", Status: "", EffectiveDate: "7/1/2011", TermDate: "" }
       , { PlanName: "PREFERRED CARE PARTNERS", PlanImage: "PREFERRED CARE PARTNERS.jpg", PlanType: "MEDICARE LOB", Location: "", Group: "", Speciality: "", Status: "", EffectiveDate: "", TermDate: "" }
       , { PlanName: "RAILROAD MCR", PlanImage: "RAILROAD MCR.png", PlanType: "MEDICARE LOB", Location: "", Group: "", Speciality: "", Status: "", EffectiveDate: "6/1/2011", TermDate: "" }
       , { PlanName: "RR MCR - Access Lab LLC", PlanImage: "RAILROAD MCR.png", PlanType: "", Location: "", Group: "", Speciality: "", Status: "", EffectiveDate: "", TermDate: "" }
       , { PlanName: "SIMPLY HEALTHCARE PLANS MCD HMO", PlanImage: "SIMPLY HEALTHCARE PLANS MCD HMO.jpg", PlanType: "MIRRA HEALTH LLC", Location: "", Group: "", Speciality: "", Status: "", EffectiveDate: "3/1/2013", TermDate: "" }
       , { PlanName: "SIMPLY HEALTHCARE PLANS MCR HMO", PlanImage: "SIMPLY HEALTHCARE PLANS MCD HMO.jpg", PlanType: "MIRRA HEALTH LLC", Location: "", Group: "", Speciality: "", Status: "", EffectiveDate: "3/1/2013", TermDate: "" }
       , { PlanName: "SUNCOAST HOSPICE - PINELLAS CTY", PlanImage: "SUNCOAST HOSPICE - PINELLAS CTY.jpg", PlanType: "MEDICARE LOB", Location: "", Group: "", Speciality: "", Status: "", EffectiveDate: "", TermDate: "" }
       , { PlanName: "ULTIMATE HEALTH PLAN ( UNITY IPA)", PlanImage: "ULTIMATE HEALTH PLAN ( UNITY IPA).jpg", PlanType: "MEDICARE RISK", Location: "", Group: "", Speciality: "", Status: "", EffectiveDate: "1/1/2013", TermDate: "" }
       , { PlanName: "ULTIMATE HEALTH PLAN ( UNITY IPA)", PlanImage: "ULTIMATE HEALTH PLAN ( UNITY IPA).jpg", PlanType: "MEDICARE RISK", Location: "", Group: "", Speciality: "", Status: "", EffectiveDate: "1/1/2013", TermDate: "" }
       , { PlanName: "ULTIMATE HEALTH PLAN ( UNITY IPA)", PlanImage: "ULTIMATE HEALTH PLAN ( UNITY IPA).jpg", PlanType: "MEDICARE RISK", Location: "", Group: "", Speciality: "", Status: "", EffectiveDate: "1/1/2013", TermDate: "" }
       , { PlanName: "UNITEDHEALTHCARE FFS  ", PlanImage: "UNITEDHEALTHCARE FFS.png", PlanType: "MEDICARE LOB", Location: "", Group: "", Speciality: "", Status: "", EffectiveDate: "7/1/2011", TermDate: "" }
       , { PlanName: "UNITEDHEALTHCARE FFS  ", PlanImage: "UNITEDHEALTHCARE FFS.png", PlanType: "COMMERCIAL LOB", Location: "", Group: "", Speciality: "", Status: "", EffectiveDate: "7/1/2011", TermDate: "" }
       , { PlanName: "UNIVERSAL HEALTH CARE(IPA)", PlanImage: "UNIVERSAL HEALTH CARE(IPA).jpg", PlanType: "MEDICARE RISK", Location: "", Group: "", Speciality: "", Status: "", EffectiveDate: "9/1/2011", TermDate: "12/31/9999" }
       , { PlanName: "UNIVERSAL HEALTHCARE FFS ", PlanImage: "UNIVERSAL HEALTH CARE(IPA).jpg", PlanType: "MEDICAID LOB", Location: "", Group: "", Speciality: "", Status: "", EffectiveDate: "9/1/2011", TermDate: "12/31/9999" }
       , { PlanName: "US DEPT OF LABOR AND INDUSTRIES  ", PlanImage: "US DEPT OF LABOR AND INDUSTRIES.jpg", PlanType: "FECA", Location: "", Group: "", Speciality: "", Status: "", EffectiveDate: "7/1/2012", TermDate: "" }
       , { PlanName: "US DEPT OF LABOR AND INDUSTRIES  ", PlanImage: "US DEPT OF LABOR AND INDUSTRIES.jpg", PlanType: "ENGERY PROGRAM", Location: "", Group: "", Speciality: "", Status: "", EffectiveDate: "7/1/2012", TermDate: "" }
        , { PlanName: "US DEPT OF LABOR AND INDUSTRIES  ", PlanImage: "US DEPT OF LABOR AND INDUSTRIES.jpg", PlanType: "BLACK LUNG PROGRAM", Location: "", Group: "", Speciality: "", Status: "", EffectiveDate: "7/1/2012", TermDate: "" }
        , { PlanName: "WELLCARE (IPA)", PlanImage: "WELLCARE (IPA).jpg", PlanType: "MEDICARE RISK", Location: "", Group: "", Speciality: "", Status: "", EffectiveDate: "6/1/2010", TermDate: "" }
        , { PlanName: "WELLCARE (IPA)", PlanImage: "WELLCARE (IPA).jpg", PlanType: "MEDICARE RISK", Location: "", Group: "", Speciality: "", Status: "", EffectiveDate: "9/1/2012", TermDate: "" }
        , { PlanName: "WELLCARE (IPA)", PlanImage: "WELLCARE (IPA).jpg", PlanType: "MEDICARE RISK", Location: "", Group: "", Speciality: "", Status: "", EffectiveDate: "6/1/2010", TermDate: "" }
        , { PlanName: "WELLCARE (IPA)", PlanImage: "WELLCARE (IPA).jpg", PlanType: "MEDICARE RISK", Location: "", Group: "", Speciality: "", Status: "", EffectiveDate: "6/1/2010", TermDate: "" }
        , { PlanName: "WELLCARE (IPA)", PlanImage: "WELLCARE (IPA).jpg", PlanType: "MEDICARE RISK", Location: "", Group: "", Speciality: "", Status: "", EffectiveDate: "6/1/2010", TermDate: "" }
        , { PlanName: "WELLCARE (IPA)", PlanImage: "WELLCARE (IPA).jpg", PlanType: "Access 2 Health Care LLC", Location: "", Group: "", Speciality: "", Status: "", EffectiveDate: "8/1/2014", TermDate: "" }
        , { PlanName: "WELLCARE (IPA)", PlanImage: "WELLCARE (IPA).jpg", PlanType: "Access 2 Health Care LLC", Location: "", Group: "", Speciality: "", Status: "", EffectiveDate: "7/1/2014", TermDate: "" }
        , { PlanName: "WELLCARE FEE FOR SVC", PlanImage: "WELLCARE (IPA).jpg", PlanType: "MEDICAID LOB", Location: "", Group: "", Speciality: "", Status: "", EffectiveDate: "9/1/2012", TermDate: "" }

    ];

    $scope.data = data;

    $scope.tableParams = new ngTableParams({
        page: 1,            // show first page
        count: 10,          // count per page
        filter: {
            //name: 'M'       // initial filter
        },
        sorting: {
            //name: 'asc'     // initial sorting
        }
    }, {
        total: data.length, // length of data
        getData: function ($defer, params) {
            // use build-in angular filter
            var filteredData = params.filter() ?
                    $filter('filter')(data, params.filter()) :
                    data;
            var orderedData = params.sorting() ?
                    $filter('orderBy')(filteredData, params.orderBy()) :
                    data;

            params.total(orderedData.length); // set total for recalc pagination
            $defer.resolve(orderedData.slice((params.page() - 1) * params.count(), params.page() * params.count()));
        }
    });

    //if filter is on
    $scope.ifFilter = function () {
        try {
            var bar;
            var obj = $scope.tableParams.$params.filter;
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
            return ($scope.tableParams.$params.page * $scope.tableParams.$params.count) - ($scope.tableParams.$params.count - 1);
        }
        catch (e) { }
    }
    //Get index Last in table
    $scope.getIndexLast = function () {
        try {
            return { true: ($scope.data.length), false: ($scope.tableParams.$params.page * $scope.tableParams.$params.count) }[(($scope.tableParams.$params.page * $scope.tableParams.$params.count) > ($scope.data.length))];
        }
        catch (e) { }
    }

});