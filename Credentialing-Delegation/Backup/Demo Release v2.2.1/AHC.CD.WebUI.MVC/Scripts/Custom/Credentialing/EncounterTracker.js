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
        colors: ['#00C957', '#FF4040'],
        click: function (d) {
            $scope.messages.push('clicked!');
        },
        mouseover: function (d) {
            $scope.messages.push('mouseover!');
        },
        mouseout: function (d) {
            $scope.messages.push('mouseout!');
        },
        innerRadius: 0,
        lineLegend: 'lineEnd',
    };

    $scope.exampleData = [{
        "key": "Invalid PCP  2/3",
        "value": 22
    }, {
        "key": "Invalid DOB",
        "value": 66
    }, {
        "key": "Invalid Zip",
        "value": 46
    }, {
        "key": "Invalid Place Of Service Code",
        "value": 23
    },{
        "key": "Invalid Coding",
        "value": 55
}]; 

    $scope.noDataData = [{
        "key": "Series 1",
        "values": []
    }];

    $scope.xFunction = function () {
        return function (d) {
            return d[0];
        };
    };
    $scope.yFunction = function () {
        return function (d) {
            return d[1];
        };
    };

    $scope.xAxisTickFormatFunction = function () {
        return function (d) {
            //return d3.time.format('%m/%y')(new Date(d));
            return d;
        };
    };

    $scope.yAxisTickFormatFunction = function () {
        return function (d) {
            //return d3.format('d')(d);
            return d;
        };
    };
    var colorArray = ['#FF0000', '#0000FF', '#000000', '#33CC33'];
    $scope.colorFunction = function () {
        return function (d, i) {
            return colorArray[i];
        };
    };

    $scope.toolTipContentFunction = function () {
        return function (key, x, y, e, graph) {
            return 'Super New Tooltip' + '<h1>' + key + '</h1>' + '<p>' + y + ' at ' + x + '</p>'
        };
    };
    var format = d3.format(',.4f');
    $scope.valueFormatFunction = function () {
        return function (d) {
            return format(d);
        };
    };
    //Table part starting-----------------------------------------------------------------------
    var data = [                 
                    {EncounterNumber: "189029",
                    Plan:"Freedom",
                    PCP: "1385441",
                    ProviderName: "LUGO ARRENDELL MD,LUIS H",
                    PatientID: "800912244*01",
                    PatientName: "NORMAN GOLDZMAN",
                    PatientDOB: "11/12/1943",
                    PatientZIP: "33127",
                    DateOfService: "01-07-2014",
                    DateOfSubmission: "03-07-2014",
                    PlaceOfServiceCode: "1387039",
                    Codes: [{ Name: "99396", Decs: "" }], ResponseCodes: [{ Name: "99399", Decs: "" }], PlanResponse: [{ Name: "", Decs: "" }],
                    Sel: false
                    }, {
                        EncounterNumber: "189059",
                        Plan: "Humana",
                        PCP: "1182016",
                        ProviderName: "EVANCHO DO,WAYNE N",
                        PatientID: "801495533*01",
                        PatientName: "JIMMIE MARLIN",
                        PatientDOB: "09/25/1939",
                        PatientZIP: "33169",
                        DateOfService: "01-06-2014",
                        DateOfSubmission: "03-06-2014",
                        PlaceOfServiceCode: "686965",
                        Codes: [{ Name: " 99285", Decs: "" }, { Name: "77245", Decs: "" }], ResponseCodes: [{ Name: "77245", Decs: "" }], PlanResponse: [{ Name: "", Decs: "" }],
                        Sel: false
                    }, {
                        EncounterNumber: "189089",
                        Plan: "Unity",
                        PCP: "110329",
                        ProviderName: "GRAFF MD,ALAN",
                        PatientID: "809125802*01",
                        PatientName: "LOUELLA RIVERA-FLECHA",
                        PatientDOB: "12/14/1922",
                        PatientZIP: "33179",
                        DateOfService: "10-01-2014",
                        DateOfSubmission: "12-01-2014",
                        PlaceOfServiceCode: "1028826",
                        Codes: [{ Name: "9985", Decs: "" }], ResponseCodes: [{ Name: "9985", Decs: "" }], PlanResponse: [{ Name: "150", Decs: "Invalid Place Of Service Code" }],
                        Sel: false
                    }
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

$scope.closeDetail = function () {
    $scope.temp.sel = false;
    $scope.temp = 0;
};


//Table part ending-----------------------------------------------------------------------

});

var PanelToggle = function (id) {
    $("#"+id).slideToggle();
   
}

