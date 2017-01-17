$(document).ready(function () {
    //Code for the Authorization Summary Graph Starts Here//
    presentgraph = "Week";
    GraphsArray = ["Week", "Month", "Quarter", "Halfyear", "Year"];
    reqdata = [];

    //Ajax Call to get the Json Data for Chart and Table//
    function GetMemberData(MemberId) {
        $.ajax({
            type: "GET",
            url: "/CaseManagement/GetMemberData",
            data: { input: MemberId },
            dataType: "json",
            async: false,
            success: function (data) {
                console.log(data);
                memData = data;
            }
        });
        return memData;
    }
    MemberDataCM = GetMemberData();
    $("#caseSummaryTable").html("");
    $("#caseStatusTable").html("");
    $("#carePlanProblemsTable").html("");
    $("#carePlanInterventionsTable").html("");
    $("#carePlanGoalsTable").html("");
    $("#carePlanOutcomesTable").html("");
    function GenerateCaseSummary(data, header) {
        var tdata = "";
        var thead = '<thead style="font-weight: normal; background-color:#F0F0F0; color:black;"><tr>';
        var tbody = '<tbody>';
        for (var i in data) {
           
            tbody = tbody + '<tr>';
            for (var k in data[i]) {
                if (i == 0) { thead = thead + '<th>' + header[k] + '</th>'; }
                tbody = tbody + '<td>' + data[i][k] + '</td>';
            }
            tbody = tbody + '</tr>';
        }
        tdata = thead + '</tr>' + tbody;
        return tdata;
    }
    function GenerateCarePlan(data) {
        var tdata = "";
        for (var k in data) {
            tdata = tdata + '<tr class="well well-sm">';
            tdata = tdata + '<td style="font-weight:bold;">' + data[k].Description + '</td>';
            tdata = tdata + '<td style=" width: 1%;"><button class="btn btn-xs btn-primary" data-toggle="tooltip" tooltip data-placement="top" title="Information" ><i class="fa fa-info-circle"></i></button></td>';
            tdata = tdata + '</tr>';
        }
        return tdata;
    }
    $("#caseSummaryTable").append(GenerateCaseSummary([MemberDataCM.CaseSummary], MemberDataCM.CaseSummaryHeader));
    $("#caseStatusTable").append(GenerateCaseSummary(MemberDataCM.CaseStatus, MemberDataCM.CaseStatusHeader));
    $("#carePlanProblemsTable").append(GenerateCarePlan(MemberDataCM.carePlanProblems));
    $("#carePlanInterventionsTable").append(GenerateCarePlan(MemberDataCM.carePlanInterventions));
    $("#carePlanGoalsTable").append(GenerateCarePlan(MemberDataCM.carePlanGoals));
    $("#carePlanOutcomesTable").append(GenerateCarePlan(MemberDataCM.carePlanOutcomes));
});

