//============================ Angular Module View Controller ==========================
var encounterTrackerApp = angular.module("encounterTrackerApp", ['ngTable', 'angularCharts']);

//============================= Angular controller =====================================
encounterTrackerApp.controller('encounterTrackerController', function ($scope, $filter, ngTableParams) {
    
    $scope.Encounters = {
        "total": 134,
        "Approved": 56,
        "Rejected": 78
    };

    $scope.dataStatus = {
        series: ['Approved', 'Rejected'],
        data: [{
            x: "Approved",
            y: [56],
            tooltip: "this is tooltip"
        }, {
            x: "Rejected",
            y: [78]
        }]
    };

     
    $scope.chartType = 'pie';
    $scope.messages = [];

    $scope.config = {
        labels: false,
        title: "",
        legend: {
            display: true,
            position: 'right'
        },
        colors: ['#33CC33', '#DC3912', '#CC0000'],
        click: function (d) {
            $scope.messages.push('clicked!');
        },
        mouseover: function (d) {
            $scope.messages.push('mouseover!');
        },
        mouseout: function (d) {
            $scope.messages.push('mouseout!');
        },
        innerRadius: 66,
        lineLegend: 'lineEnd',
    };

    $scope.exampleData = [{
        "key": "invalid npi  2/3",
        "value": 22
    }, {
        "key": "invalid dob",
        "value": 66
    }, {
        "key": "invalid zip",
        "value": 46
    }, {
        "key": "invalid place of service code",
        "value": 23
    },{
        "key": "invalid coding",
        "value": 55
}]; 

  
    //Table part starting-----------------------------------------------------------------------
    var data = [                 
                    {EncounterNumber:"1105",
                    PCP:"1245667889",
                    ProviderName:"Mc Rose",
                    PatientID:"88934",
                    PatientName:"John Smith",
                    PatirntDOB:"22/06/1965",
                    PatientZIP:"122356",
                    DateOfService:"06/12/2010",
                    DateOfSubmission: "28/12/2010",
                    PlaceOfServiceCode: "Spring Hill",
                    Codes: [{ Name: "Code1", Decs: "Desc1" }], ResponseCodes: [{ Name: "Code1", Decs: "Desc1" }], PlanResponse: [{ Name: "Response1", Decs: "Desc1" }],
                    Sel:false}
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
$scope.temp = 0;
$scope.changeSelection = function (user) {
    // console.info(user);
    if ($scope.temp != 0) {
        $scope.temp.sel = false;
    }
    user.sel = true;
    $scope.temp = user;
};

//Table part ending-----------------------------------------------------------------------

});

var PanelToggle = function (id) {
    $("#"+id).slideToggle();
   
}

$(function () {
    $('[data-toggle="tooltip"]').tooltip()
})