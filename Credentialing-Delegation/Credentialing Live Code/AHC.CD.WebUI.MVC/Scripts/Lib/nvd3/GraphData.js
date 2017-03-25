angular.module('myApp', ['ngTable']) 
    .controller('myCtrl', function ($scope, $filter, ngTableParams) {
        //'nvd3',
        /* Chart options */
        //$scope.options = { /* JSON data */ };
        $scope.isCollapsed = false;
        var data = [];
        $scope.columns = [{ title: 'ADMITTING DIAGNOSIS', field: 'ADMITTING_DIAGNOSIS', visible: true, filter: { 'ADMITTING_DIAGNOSIS': 'text' } },
                         { title: 'ADMITTING DIAGNOSIS DESC', field: 'ADMITTING_DIAGN_DESC', visible: true, filter: { 'ADMITTING_DIAGN_DESC': 'text' } },
                         { title: 'CLINIC FACILITY ID', field: 'CLINIC_FACILITY_ID', visible: true, filter: { 'CLINIC_FACILITY_ID': 'text' } },
                         { title: 'CLINIC FACILITY NAME', field: 'CLINIC_FACILITY_NAME', visible: true, filter: { 'CLINIC_FACILITY_NAME': 'text' } },
                         { title: 'CLINIC FACILITY TYPE', field: 'CLINIC_FACILITY_TYPE', visible: true, filter: { 'CLINIC_FACILITY_TYPE': 'text' } },
                         { title: 'Total Charges', field: 'Total_Charges', visible: true, filter: { 'Total_Charges': 'text' } },
                         { title: 'PLAN NAME', field: 'PLAN_NAME', visible: true, filter: { 'PLAN_NAME': 'text' } },
                         { title: 'PRINCIPAL DIAGNOSIS', field: 'PRINCIPAL_DIAGNOSIS', visible: true, filter: { 'PRINCIPAL_DIAGNOSIS': 'text' } },
                         { title: 'PRINCIPAL DIAGNOSIS DESCRIPTION', field: 'PRINCIPAL_DIAGN_DESC', visible: true, filter: { 'PRINCIPAL_DIAGN_DESC': 'text' } }]
        ;

        $scope.tableParams = new ngTableParams({
            page: 1,            // show first page
            count: 5          // count per page
        }, {
            total: data.length, // length of data
            getData: function ($defer, params) {
                // use build-in angular filter
                var orderedData = params.sorting() ?
                        $filter('orderBy')(data, params.orderBy()) :
                        data;
                orderedData = params.filter() ?
                               $filter('filter')(orderedData, params.filter()) :
                               orderedData;
                params.total(data.length);
                $defer.resolve(orderedData.slice((params.page() - 1) * params.count(), params.page() * params.count()));
            }
        });

        $scope.options = {
            chart: {
                type: 'discreteBarChart',
                height: 450,
                margin: {
                    top: 20,
                    right: 20,
                    bottom: 100,
                    left: 100
                },
                x: function (d) { return d.label; },
                y: function (d) { return d.value; },
                showValues: false,
                valueFormat: function (d) {
                    return d3.format(',.4f')(d);
                },
                transitionDuration: 500,
                xAxis: {
                    axisLabel: 'X Axis',
                    rotateLabels: -45
                },
                yAxis: {
                    axisLabel: 'Y Axis'
                    //axisLabelDistance: 30
                },
                tooltips: true,
                tooltipContent: function (key, x, y, e, graph) {
                    var item = [];
                    var data = x.split(" & ");
                    for (var i in data) {
                        var id = data[i].split("-");
                        if(id[0] == "P")
                        {
                            for (var j in $scope.PrincipalDiganosis) {
                                if ($scope.PrincipalDiganosis[j].id == id[1]) {
                                    item.push($scope.PrincipalDiganosis[j].value);
                                    break;
                                }
                            }
                        }
                        else if(id[0] == "A"){
                            for (var j in $scope.AdmitingDiagnosis) {
                                if ($scope.AdmitingDiagnosis[j].id == id[1]) {
                                    item.push($scope.AdmitingDiagnosis[j].value);
                                    break;
                                }
                            }
                        }
                    }

                    return '<h6>' + item.join(" - ") + '</h6>' + '<p>' + y + '</p>';
                }
            }
        };

        //$scope.data = [{
        //    key: "Cumulative Return",
        //    values: []
        //}]

        /* Chart data */
        //$scope.data = { /* JSON data */ }

        

        $scope.graphData = JSON.parse($('.data').text());
        console.log($scope.graphData);

        $scope.clinicTypes = GetClinicTypes();
        $scope.selectedClinicType = $scope.clinicTypes[0];

        $scope.clinicNames = [{ type: "-----Select Clinic------" }];
        $scope.selectedClinicName = $scope.clinicNames[0];

        $scope.ChangeClinicType = function () {

            $scope.clinicNames = [{ type: "-----Select Clinic------" }];
            $scope.selectedClinicName = $scope.clinicNames[0];

            var data = [];

            if ($scope.selectedClinicType.type != $scope.clinicTypes[0].type)
            {
                for (var i in $scope.graphData) {

                    //var planName = $scope.graphData[i].PLAN_NAME == null ? "null" : $scope.graphData[i].PLAN_NAME;
                    //var clinicName = $scope.graphData[i].CLINIC_FACILITY_NAME == null ? "null" : $scope.graphData[i].CLINIC_FACILITY_NAME;

                    //if (cliincType == $scope.selectedClinicType.type && planName == $scope.selectedPlanName.type && clinicName == $scope.selectedClinicName.type) {
                    //    result.push({ "label": ($scope.graphData[i].ADMITTING_DIAGNOSIS + " & " + $scope.graphData[i].PRINCIPAL_DIAGNOSIS), "value": $scope.graphData[i].Column1 });
                    //}

                    //var cliincType = $scope.graphData[i].CLINIC_FACILITY_TYPE == null ? "null" : $scope.graphData[i].CLINIC_FACILITY_TYPE;

                    if($scope.graphData[i].CLINIC_FACILITY_TYPE == $scope.selectedClinicType.type)
                    {
                        if ($.inArray($scope.graphData[i].CLINIC_FACILITY_NAME, data) == -1) {
                            data.push($scope.graphData[i].CLINIC_FACILITY_NAME);
                            $scope.clinicNames.push({ type: $scope.graphData[i].CLINIC_FACILITY_NAME });
                        }
                    }
                }
            }
        };

        $scope.planNames = [{ type: "-----Select Plan------" }];
        $scope.selectedPlanName = $scope.planNames[0];

        $scope.ChangeClinicName = function () {

            $scope.planNames = [{ type: "-----Select Plan------" }];
            $scope.selectedPlanName = $scope.planNames[0];

            var data = [];

            if ($scope.selectedClinicName.type != $scope.clinicNames[0].type) {
                for (var i in $scope.graphData) {

                    if ($scope.graphData[i].CLINIC_FACILITY_NAME == $scope.selectedClinicName.type && $scope.graphData[i].CLINIC_FACILITY_TYPE == $scope.selectedClinicType.type) {
                        if ($.inArray($scope.graphData[i].PLAN_NAME, data) == -1) {
                            data.push($scope.graphData[i].PLAN_NAME);
                            $scope.planNames.push({ type: $scope.graphData[i].PLAN_NAME });
                        }
                    }
                }
            }

            console.log($scope.planNames);
        };


        $scope.GenerateGraph = function()
        {
            var result = [];
            var result1 = [];
            for (var i in $scope.graphData) {

                if ($scope.graphData[i].CLINIC_FACILITY_TYPE == $scope.selectedClinicType.type &&
                    $scope.graphData[i].PLAN_NAME == $scope.selectedPlanName.type &&
                    $scope.graphData[i].CLINIC_FACILITY_NAME == $scope.selectedClinicName.type) {

                    var labelText = [];
                    if ($scope.graphData[i].ADMITTING_DIAGNOSIS != null)
                        labelText.push("P-" + $scope.graphData[i].ADMITTING_DIAGNOSIS);

                    if ($scope.graphData[i].PRINCIPAL_DIAGNOSIS != null)
                        labelText.push("A-" + $scope.graphData[i].PRINCIPAL_DIAGNOSIS)

                    result.push({ "label": labelText.join(" & "), "value": $scope.graphData[i].Column1 });
                    result1.push($scope.graphData[i]);
                }
            }

            //$scope.data[0].values = result;
            data = result1;
            console.log(data);
            $scope.tableParams.reload();
        }

        var toolTip = function (key, y, e, graph) {
            var item = [];
            var data = key.split("&");
            for (var i in data) {
                var id = data[i].split("-");
                if(id[0] == "P")
                {
                    for (var j in $scope.PrincipalDiganosis) {
                        if ($scope.PrincipalDiganosis[j].id == id[1]) {
                            item.push($scope.PrincipalDiganosis[j].value);
                            break;
                        }
                    }
                }
                else if(d[0] == "A"){
                    for (var j in $scope.AdmitingDiagnosis) {
                        if ($scope.AdmitingDiagnosis[j].id == id[1]) {
                            item.push($scope.AdmitingDiagnosis[j].value);
                            break;
                        }
                    }
                }
            }

            return '<h3>' + item.join(" - ") + '</h3>' + '<p>' + y + '</p>';
        }

        $scope.ChangePlanName = function () {
            alert("Plan");
        }

        $scope.PrincipalDiganosis = GetPrincipalDiagnosis();

        function GetPrincipalDiagnosis() {
            var data = [];
            var result = []

            for (var i in $scope.graphData) {

                if ($.inArray($scope.graphData[i].PRINCIPAL_DIAGNOSIS, data) == -1) {
                    data.push($scope.graphData[i].PRINCIPAL_DIAGNOSIS);
                    result.push({ id: $scope.graphData[i].PRINCIPAL_DIAGNOSIS, value: $scope.graphData[i].PRINCIPAL_DIAGN_DESC });
                }

            }

            console.log(result);

            return result;
        }

        $scope.AdmitingDiagnosis = GetAdmitingDiagnosis();

        function GetAdmitingDiagnosis() {
            var data = [];
            var result = []

            for (var i in $scope.graphData) {

                if ($.inArray($scope.graphData[i].ADMITTING_DIAGNOSIS, data) == -1) {
                    data.push($scope.graphData[i].ADMITTING_DIAGNOSIS);
                    result.push({ id: $scope.graphData[i].ADMITTING_DIAGNOSIS, value: $scope.graphData[i].ADMITTING_DIAGN_DESC });
                }

            }
            console.log(result);
            return result;
        }


        function GetClinicTypes() {
            $scope.clinicTypes = [{ type: "-----Select Facility------" }];
            return PushUniqueSelectData("CLINIC_FACILITY_TYPE", $scope.clinicTypes);
        };

        function GetPlanNames() {
            //return GetUniqueSelectData("PLAN_NAME");
        };

        function GetClinicNames() {
            //return GetUniqueSelectData("CLINIC_FACILITY_NAME");
        }

        function GetPrincipalDiagnDesc()
        {
            //return GetUniqueSelectData("PRINCIPAL_DIAGN_DESC");
        }

        function GetAdmitingDiagnDesc() {
            //return GetUniqueSelectData("ADMITTING_DIAGN_DESC");
        }

        function PushUniqueSelectData(property, listData)
        {
            var data = [];

            for (var i in $scope.graphData) {

                if ($.inArray($scope.graphData[i][property], data) == -1) {
                    data.push($scope.graphData[i][property]);
                    listData.push({ type: $scope.graphData[i][property] });
                }

            }

            return listData;
        }


            //ADMITTING_DIAGNOSIS: null
            //ADMITTING_DIAGN_DESC: null
            //CLINIC_FACILITY_ID: "F00857"
            //CLINIC_FACILITY_NAME: "SHANDS UF"
            //CLINIC_FACILITY_TYPE: "HOSPITAL"
            //Column1: 6277
            //PLAN_NAME: "083-FREEDOM VIP SAVINGS COPD (HMO SNP)"
            //PRINCIPAL_DIAGNOSIS: "441.4"
            //PRINCIPAL_DIAGN_DESC: "Abdominal aneurysm without mention of rupture"

    })