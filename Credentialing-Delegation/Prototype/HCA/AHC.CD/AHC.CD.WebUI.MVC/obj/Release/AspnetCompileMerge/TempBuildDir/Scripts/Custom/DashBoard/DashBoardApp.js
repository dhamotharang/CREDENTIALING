var dashBoardApp = angular.module('dashBoardApp', []);



dashBoardApp.controller("GraphController", function ($scope, $http) {

    var data = [];

    var barData = [];

    var chartBar;

    var chartPie;

    // variable init
    $scope.progressbar = false;

    $scope.SelectedRelationId = "All"

    $scope.maxYear = new Date().getFullYear();

    $scope.selectedYear = new Date().getFullYear();

    $scope.providerRelations = [{
        relationId: 0,
        relationName: "Employee"
    }, {
        relationId: 1,
        relationName: "Affiliate"
    }];

    $scope.range = function (min, max) {        
        var input = [];
        for (var i = max; i >= min; i--) input.push(i);
        return input;
        $scope.selectedYear = input[0];
    };


    getProviderTypesData();


    function drawPie(data) {

        nv.addGraph(function () {
            chartPie = nv.models.pieChart()
           .x(function (d) { return d.label })
           .y(function (d) { return d.value })
           .showLabels(true)    //Display pie labels
           .labelThreshold(0)  //Configure the minimum slice size for labels to show up
           .labelType("percent") //Configure what type of data to show in the label. Can be "key", "value" or "percent"
           .donut(true)          //Turn on Donut mode. Makes pie chart look tasty!
           .donutRatio(0.35);   //Configure how big you want the donut hole size to be.            
           
           // chartPie.radioButtonMode(true);
            //chartPie.dispatch.on('legendClick', function (e) { myFunction(e) });
            chartPie.legend.dispatch.legendClick = function (d, i) {
                return false;
            };

            chartPie.valueFormat(d3.format('d'));

          

            d3.select("#chart2 svg")
                .datum(data)
                .transition().duration(350)
                .call(chartPie);
            d3.selectAll('.nv-slice')
          .on('click', function (e) {
              getOccupationData(e.data.label);
            
          }).on('tap', function (e) {
              getOccupationData(e.data.label);

          });

            return chartPie;
        });

       


    }


    function drawBar(data) {

       

        nv.addGraph(function () {
            chartBar = nv.models.discreteBarChart()
           .x(function (d) { return d.label })    //Specify the data accessors.
           .y(function (d) { return d.value })
           .staggerLabels(true)    //Too many bars and not enough room? Try staggering labels.
           .tooltips(true)        //Don't show tooltips
           .showValues(true)       //...instead, show the bar value right on top of each bar.
           .transitionDuration(350)
            ;

            chartBar.valueFormat(d3.format('d'));

            chartBar.yAxis.tickFormat(d3.format('d'));

           

            d3.select('#chartbar svg')
                .datum(data)
                .call(chartBar);

            nv.utils.windowResize(chartBar.update);

            $('body,html').animate({
                scrollTop: $('#providerbar').offset().top
            });

            return chartBar;
        });

    }


    function getOccupationData(providerType) {

        $scope.providerType = providerType;
        $scope.progressbar = true;

        $http.get('/Dashboard/GetProviderOccupationData?providerType=' + providerType + "&ProviderRelation=" + $scope.SelectedRelationId + "&year=" + $scope.selectedYear).success(function (recData) {
            $scope.pieReady = true;
            $scope.progressbar = false;
            barData = [];
            var tempObj = {};

            tempObj.key = "provider occupation";

            tempObj.values = recData;

            barData.push(tempObj);

            drawBar(barData);


        }).error(function () {
            $scope.progressbar = false;
            console.log("Some thing went wrong please try later");

        });

    }


    function getProviderTypesData() {
        $scope.progressbar = true;
        $http.get('/Dashboard/GetProviderTypesGraphData').success(function (recData) {
            $scope.progressbar = false;
            data = recData;

            drawPie(data);
            
        }).error(function () {
            $scope.progressbar = false;
            console.log("Some thing went wrong please try later");

        });

    }


    $scope.updatePie = function () {
        $scope.progressbar = true;
        $http.get('/Dashboard/GetProviderTypesGraphData?ProviderRelation=' + $scope.SelectedRelationId).success(function (recData) {

            $scope.progressbar = false;

            data = recData;

            drawPie(data);

            getOccupationData($scope.providerType);

        }).error(function () {
            $scope.progressbar = false;
            console.log("Some thing went wrong please try later");

        });
    }

    $scope.updateBar = function () {

        getOccupationData($scope.providerType);

    }



});
