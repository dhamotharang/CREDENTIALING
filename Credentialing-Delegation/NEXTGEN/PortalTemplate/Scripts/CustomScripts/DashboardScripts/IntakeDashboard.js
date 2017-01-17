$(document).ready(function () {
    //Code for the Authorization Summary Graph Starts Here//
    presentgraph = "Week";
    GraphsArray = ["Week", "Month", "Quarter", "Halfyear", "Year"];
    reqdata = [];

    //Ajax Call to get the Json Data for Chart and Table//
    function GetGarphData(id) {
        $.ajax({
            type: "GET",
            url: "/DashBoards/GetGraphData",
            data: { input: id },
            dataType: "json",
            async: false,
            success: function (data) {
                console.log(data);
                reqdata = data;
            }
        });
        return reqdata;

    }

    PD = GetGarphData("PendingData");

    //Initializing the chart//
    var authlistchart = c3.generate({
        bindto: "#AuthListchart",
        data: {
            x: 'x',
            columns: PD.WeekDataPending,
            type: 'bar',
            labels: true,
            groups: [ ['Standard', 'Expedited'] ]
        },
        bar: {
            width: {
                ratio: 0.25
            }
        },
        axis: {
            x: {
                type: 'category',
                categories: ["Monday", "Tuesday", "Wednesday", "Thursday", "Friday"]
            }
        },
        grid: {

            y: {
                lines: [{ value: 0 }]
            }
        }, color: {
            pattern: ['#d58512', '#3C8DBC', '#ff7f0e', '#ffbb78', '#2ca02c', '#98df8a', '#d62728', '#ff9896', '#9467bd', '#c5b0d5', '#8c564b', '#c49c94', '#e377c2', '#f7b6d2', '#7f7f7f', '#c7c7c7', '#bcbd22', '#dbdb8d', '#17becf', '#9edae5']
        },
        tooltip: {
            format: {
                value: function (value, ratio, id) {
                    var format = id === 'data1' ? d3.format(',') : d3.format('$');
                    return format(value);
                }
            }
        }
    });
   
    //Function called for switching of  Garphs and Tables
    function SwitchGraphs(reqgraph, currentData) {
        switch (reqgraph) {
            case "Month":
                if (currentData === "Submitted") {
                    authlistchart.load({
                        columns: SD.MonthDataSubmitted
                    }, 1000);
                    if (isTable) {
                        $("#TableBody").html("");
                        $("#TableBody").append(GenerateTableBody(currentData + "Auths", SD.MonthDataSubmitted));
                    }
                }
                else if (currentData === "Completed") {
                    authlistchart.load({
                        columns: CD.MonthDataCompleted
                    }, 1000);
                    if (isTable) {
                        $("#TableBody").html("");
                        $("#TableBody").append(GenerateTableBody(currentData + "Auths", CD.MonthDataCompleted));
                    }
                }
                else {
                    authlistchart.load({
                        columns: PD.MonthDataPending
                    }, 1000);
                    if (isTable) {
                        $("#TableBody").html("");
                        $("#TableBody").append(GenerateTableBody(currentData + "Auths", PD.MonthDataPending));
                    }
                }
                break;
            case "Quarter":
                if (currentData === "Submitted") {
                    authlistchart.load({
                        columns: SD.QuarterDataSubmitted
                    }, 1000);
                    if (isTable) {
                        $("#TableBody").html("");
                        $("#TableBody").append(GenerateTableBody(currentData + "Auths", SD.QuarterDataSubmitted));
                    }
                }
                else if (currentData === "Completed") {
                    authlistchart.load({
                        columns: CD.QuarterDataCompleted
                    }, 1000);
                    if (isTable) {
                        $("#TableBody").html("");
                        $("#TableBody").append(GenerateTableBody(currentData + "Auths", CD.QuarterDataCompleted));
                    }
                }
                else {
                    authlistchart.load({
                        columns: PD.QuarterDataPending
                    }, 1000);
                    if (isTable) {
                        $("#TableBody").html("");
                        $("#TableBody").append(GenerateTableBody(currentData + "Auths", PD.QuarterDataPending));
                    }
                }
                break;
            case "Halfyear":
                if (currentData === "Submitted") {
                    authlistchart.load({
                        columns: SD.HalfYearDataSubmitted
                    }, 1000);
                    if (isTable) {
                        $("#TableBody").html("");
                        $("#TableBody").append(GenerateTableBody(currentData + "Auths", SD.HalfYearDataSubmitted));
                    }
                }
                else if (currentData === "Completed") {
                    authlistchart.load({
                        columns: CD.HalfYearDataCompleted
                    }, 1000);
                    if (isTable) {
                        $("#TableBody").html("");
                        $("#TableBody").append(GenerateTableBody(currentData + "Auths", CD.HalfYearDataCompleted));
                    }
                }
                else {
                    authlistchart.load({
                        columns: PD.HalfYearDataPending
                    }, 1000);
                    if (isTable) {
                        $("#TableBody").html("");
                        $("#TableBody").append(GenerateTableBody(currentData + "Auths", PD.HalfYearDataPending));
                    }
                }
                break;
            case "Year":
                if (currentData === "Submitted") {
                    authlistchart.load({
                        columns: SD.YearDataSubmitted
                    }, 1000);
                    if (isTable) {
                        $("#TableBody").html("");
                        $("#TableBody").append(GenerateTableBody(currentData + "Auths", SD.YearDataSubmitted));
                    }
                }
                else if (currentData === "Completed") {
                    authlistchart.load({
                        columns: CD.YearDataCompleted
                    }, 1000);
                    if (isTable) {
                        $("#TableBody").html("");
                        $("#TableBody").append(GenerateTableBody(currentData + "Auths", CD.YearDataCompleted));
                    }
                }
                else {
                    authlistchart.load({
                        columns: PD.YearDataPending
                    }, 1000);
                    if (isTable) {
                        $("#TableBody").html("");
                        $("#TableBody").append(GenerateTableBody(currentData + "Auths", PD.YearDataPending));
                    }
                }
                break;
            default:
                if (currentData === "Submitted") {
                    authlistchart.load({
                        columns: SD.WeekDataSubmitted
                    }, 1000);
                    if (isTable) {
                        $("#TableBody").html("");
                        $("#TableBody").append(GenerateTableBody(currentData + "Auths", SD.WeekDataSubmitted));
                    }
                }
                else if (currentData === "Completed") {
                    authlistchart.load({
                        columns: CD.WeekDataCompleted
                    }, 1000);
                    if (isTable) {
                        $("#TableBody").html("");
                        $("#TableBody").append(GenerateTableBody(currentData + "Auths", CD.WeekDataCompleted));
                    }
                }
                else {
                    authlistchart.load({
                        columns: PD.WeekDataPending
                    }, 1000);
                    if (isTable) {
                        $("#TableBody").html("");
                        $("#TableBody").append(GenerateTableBody(currentData + "Auths", PD.WeekDataPending));
                    }
                }
                break;
        }
    }

    //Function Called for Selecting Weekly/Monthly/Quarterly/Halfyearly/Yearly Charts or Tables
    currentgraph = "Week";
    $('.NewAuthList .btn-default').click(function () {
        id = $(this).attr('id');
        currentgraph = id;
        SwitchGraphs(id, currentData);
        for (var i = 0; i < GraphsArray.length; i++) {
            if (GraphsArray[i] !== id) {
                if ($("#" + GraphsArray[i]).css("background-color")) {
                    $("#" + GraphsArray[i]).css("background-color", "");
                }
            }
            else {
                $("#" + GraphsArray[i]).css("background-color", "skyblue");
            }
        }
    });

    //Function Called for Selecting Pending/Submitted/Completed Auths from Dropdown
    currentData = "Pending";
    $('select[name="DropdownforAuthList"]').change(function () {
        if ($(this).val() !== "" || $(this).val() !== null) {
            switch ($(this).val()) {
                case "2":
                    SD = GetGarphData("SubmittedData"); //Calling Ajax function to Get Submitted Data
                    currentData = "Submitted";
                    SwitchGraphs(currentgraph, currentData);  //Calling 'SwitchGraphs' function to load Submitted Data on Current Graph
                    break;
                case "3":
                    CD = GetGarphData("CompletedData"); //Calling Ajax function to Get Completed Data
                    currentData = "Completed";
                    SwitchGraphs(currentgraph, currentData);  //Calling 'SwitchGraphs' function to load Completed Data on Current Graph
                    break;
                default:
                    PD = GetGarphData("PendingData"); //Calling Ajax function to Get Pending Data
                    currentData = "Pending";
                    SwitchGraphs(currentgraph, currentData);  //Calling 'SwitchGraphs' function to load Pending Data on Current Graph
                    break;
            }
        }

    });

    //Code for the Table Goes here
    isTable = false;
    $('.Authviewlist .btn-default').click(function () {
        id = $(this).attr('id');
        if (id === "Chart") {
            $("#" + id).css("background-color", "skyblue");
            $("#Table").css("background-color", "white");
            $('.Auth_Chart').show("slide", { direction: "left" }, 1000, setTimeout(function () { }, 1000));
            $('.Auth_Table').hide("slide", { direction: "left" }, 1000, setTimeout(function () { }, 1000));
            isTable = false;
        } else if (id === "Table") {
            $("#" + id).css("background-color", "skyblue");
            $("#Chart").css("background-color", "white");
            $('.Auth_Chart').hide("slide", { direction: "left" }, 1000, setTimeout(function () { }, 1000));
            $('.Auth_Table').show("slide", { direction: "left" }, 1000, setTimeout(function () { }, 1000));
            isTable = true;
        }
    });

    //Function for Generating the Table Body
    function GenerateTableBody(authtype, data) {
        var standardCount = data[1];
        var expediteCount = data[2];
        var categoryValue = data[0];
        var tbody = "";
       
        for (var i = 1; i < data[0].length; i++) {
            tbody = tbody + '<tr>' +
               '<th>' + i + '</th>' +
               '<th><span class="h6">' + authtype + '</span></th>' +
               '<td>' + categoryValue[i] + '</td>' +
               '<td><span class="h6"><b>' + standardCount[i] + '</b></span></td>' +
               '<td><span class="h6"><b>' + expediteCount[i] + '</b></span></td>' +
                '</tr>';
        }
        return tbody;
    };
    

    $("#TableBody").append(GenerateTableBody("Pending Auths", PD.WeekDataPending));
    
});




