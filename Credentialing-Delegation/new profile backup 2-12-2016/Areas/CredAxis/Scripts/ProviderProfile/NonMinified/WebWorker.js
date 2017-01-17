//function webWorker() {
$(document).ready(function(){
    var ajaxTime = new Date().getTime();
    console.log(ajaxTime);
    $.ajax({
        //type: "POST",
        url: "/CredAxis/Summary/Index",
        success: function (result) {
            $("#SummaryTabView").html(result);
            var totalTime = new Date().getTime() - ajaxTime;
            console.log('Summary Done..');
            console.log("Time Taken : "+ totalTime);
            
        },
        error: function (result) {
        }
    });

    $.ajax({
        //type: "POST",
        url: "/CredAxis/Demographics/Index",
        success: function (result) {
            $("#DemographicsTabView").html(result);
            var totalTime = new Date().getTime() - ajaxTime;
            console.log('Demo Done..');
            console.log("Time Taken : " + totalTime);
            
        },
        error: function (result) {
        }
    });

    $.ajax({
        //type: "POST",
        url: "/CredAxis/Licenses/Index",
        success: function (result) {
            $("#LicencesTabView").html(result);
            var totalTime = new Date().getTime() - ajaxTime;
            console.log('Lic Done..');
            console.log("Time Taken : " + totalTime);
            
        },
        error: function (result) {
        }
    });

    $.ajax({
        //type: "POST",
        url: "/CredAxis/Speciality/Index",
        success: function (result) {
            $("#SpecialityTabView").html(result);
            var totalTime = new Date().getTime() - ajaxTime;
            console.log('Spc Done..');
            console.log("Time Taken : " + totalTime);            
        },
        error: function (result) {
        }
    });

    $.ajax({
        //type: "POST",
        url: "/CredAxis/PracticeLocation/Index",
        success: function (result) {
            $("#PracticeLocationTabView").html(result);
            var totalTime = new Date().getTime() - ajaxTime;
            console.log('Practice Done..');
            console.log("Time Taken : " + totalTime);
        },
        error: function (result) {
        }
    });

    $.ajax({
        //type: "POST",
        url: "/CredAxis/Education/Index",
        success: function (result) {
            $("#EducationTabView").html(result);
            var totalTime = new Date().getTime() - ajaxTime;
            console.log('Edu Done..');
            console.log("Time Taken : " + totalTime);
          
        },
        error: function (result) {
        }
    });

    $.ajax({
        //type: "POST",
        url: "/CredAxis/WorkHistory/Index",
        success: function (result) {
            $("#WorkTabView").html(result);
            var totalTime = new Date().getTime() - ajaxTime;
            console.log('WH Done..');
            console.log("Time Taken : " + totalTime);
            
        },
        error: function (result) {
        }
    });

    $.ajax({
        //type: "POST",
        url: "/CredAxis/ProfessionalAffiliation/Index",
        success: function (result) {
            $("#AffiliationTabView").html(result);
            var totalTime = new Date().getTime() - ajaxTime;
            console.log('PA Done..');
            console.log("Time Taken : "+ totalTime);
        },
        error: function (result) {
        }
    });

    $.ajax({
        //type: "POST",
        url: "/CredAxis/ProfessionalLiability/Index",
        success: function (result) {
            $("#LiabilityTabView").html(result);
            var totalTime = new Date().getTime() - ajaxTime;
            console.log('PL Done..');
            console.log("Time Taken : "+ totalTime);
        },
        error: function (result) {
        }
    });

    $.ajax({
        //type: "POST",
        url: "/CredAxis/Employment/Index",
        success: function (result) {
            $("#EmploymentTabView").html(result);
            var totalTime = new Date().getTime() - ajaxTime;
            console.log('Emp Done..');
            console.log("Time Taken : "+ totalTime);
        },
        error: function (result) {
        }
    });

    $.ajax({
        //type: "POST",
        url: "/CredAxis/ProfessionalReferences/Index",
        success: function (result) {
            $("#ProfessionalTabView").html(result);
            var totalTime = new Date().getTime() - ajaxTime;
            console.log('PR Done..');
            console.log("Time Taken : "+ totalTime);
        },
        error: function (result) {
        }
    });

    $.ajax({
        //type: "POST",
        url: "/CredAxis/HospitalPrivilege/Index",
        success: function (result) {
            $("#HospitalTabView").html(result);
            var totalTime = new Date().getTime() - ajaxTime;
            console.log('HosPriv Done..');
            console.log("Time Taken : " + totalTime);
        },
        error: function (result) {
        }
    });

    $.ajax({
        //type: "POST",
        url: "/CredAxis/ProviderTabs/GetDisclousureQuestionPartial",
        success: function (result) {
            $("#DisclousureQuestionTabView").html(result);
            var totalTime = new Date().getTime() - ajaxTime;
            console.log('DQ Done..');
            console.log("Time Taken : "+ totalTime);
        },
        error: function (result) {
        }
    });


});