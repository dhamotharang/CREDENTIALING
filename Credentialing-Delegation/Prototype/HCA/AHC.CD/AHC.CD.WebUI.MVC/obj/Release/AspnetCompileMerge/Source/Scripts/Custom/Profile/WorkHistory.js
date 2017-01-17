
profileApp.controller('WorkHistoryController', function ($scope, $rootScope, $http, $filter,countryDropDownService) {

    $scope.CountryDialCodes = countryDailCodes;

    $scope.showContryCodeDiv = function (countryCodeDivId) {
       
        $("#" + countryCodeDivId).show();
    };

    // country/state/cities cascading code

    $scope.Countries = Countries;
    $scope.States = $scope.Countries[1].States;
   
    
    $scope.getStates = function (countryCode) {
        $scope.States = countryDropDownService.getStates($scope.Countries, countryCode);
        $scope.Counties = [];
        $scope.Cities = [];
        
    }
    $scope.getCounties = function (state) {
        $scope.Counties = countryDropDownService.getCounties($scope.States, state)
        $scope.Cities = [];
        }
    $scope.getCities = function (county) {
        $scope.Cities = countryDropDownService.getCities($scope.Counties, county);
        
    }

    // scope variable init for work gap

    $scope.newWorkGapInfo = {};
    $scope.newProWorkInfo = {};

    $scope.WorkHistories = [ {
        EmployerName: "Access Healthcare Physicians LLC", Number: "5350", Building: "5350", Street: "Spring Hill Dr.", City: "Spring Hill",
        County: "Hernando", State: "Florida", Country: "United States", Zipcode: "34606-_ _ _ _", StartDate: "07/01/2011", EndDate: "Present", EmployerMobile: "352-688-8116", CountryCodeMobile: "352", EmployerFax: "352-686-9477", CountryCodeFax: "352"
    , JobTitle: "Medical Director"
    },
{
    EmployerName: "Access Healthcare LLC", Number: "5350", Building: "5350", Street: "Spring Hill Dr.", City: "Spring Hill",
    County: "Hernando", State: "Florida", Country: "United States", Zipcode: "34606-_ _ _ _", StartDate: "06/01/2001", EndDate: "06/01/2011", EmployerMobile: "352-688-8116", CountryCodeMobile: "352", EmployerFax: "352-686-9477", CountryCodeFax: "352"
    , JobTitle: "Medical Director"
}, {
        EmployerName : "Professional Centerof Internal Medicine", Number: "", Building: "", Street: "", City: "",
    County: "", State: "", Country: "United States", Zipcode: "", StartDate: "07/01/1997", EndDate: "05/01/2001", EmployerMobile: "", CountryCodeMobile: "", EmployerFax: "", CountryCodeFax: ""
    , JobTitle: "Internal Medicine"
}];

    $scope.WorkGaps = [];

    //$scope.WorkGaps = [{
    //    StartDate: "14-06-2007",
    //    EndDate: "17-09-2013",Description:"desc1"
    //},{
    //    StartDate: "14-06-2008",
    //    EndDate: "17-09-2014", Description: "desc2"
    //}];

    // temp data request to get work exps
    //$http.get('~/Profile/WorkHistory/GetAllWorkExp').success(function (data, status, headers, config) {

    //    alert("got the data");
    //    console.log(data);
    //});;



    // Profile data receivers from root scope
    /*
    $rootScope.$on('ProfessionalWorkExperiences', function (event, val) {
        $scope.WorkHistories = val;
    });

    $rootScope.$on('WorkGaps', function (event, val) {
        $scope.WorkGaps = val;
    });*/

    //=============== Work History Conditions ==================
    $scope.workHistoryFormStatus = false;
    $scope.newWorkHistoryForm = false;
    $scope.workGapFormStatus = false;
    $scope.newWorkGapForm = false;
    $scope.showingDetails = false

    // for work experience
    $scope.showingDetailsView = false;

    // for work gap
    $scope.showingDetailsWorkGapView = false;

    //====================== Work History ===================

    

    $scope.addWorkHistory = function () {
        $scope.workHistoryFormStatus = false;
        $scope.newWorkHistoryForm = true;
        $scope.workHistory = {};

    };

    $scope.addWorkGap = function () {

        $scope.newWorkGapForm = true;

    };

 
    $scope.updateWorkHistory = function (workHistory) {
        $scope.showingDetails = false;
        $scope.workHistoryFormStatus = false;
        $scope.newWorkHistoryForm = false;
        $scope.workHistory = {};
    };

    $scope.updateWorkGap = function (workGap) {
        $scope.showingDetails = false;
        $scope.workGapFormStatus = false;
        $scope.newWorkGapForm = false;
        $scope.workGap = {};
    };

    $scope.editWorkHistory = function (index, workHistory) {
        $scope.showingDetails = true;
        $scope.showingDetailsView = false;
        $scope.workHistoryFormStatus = true;
        $scope.newWorkHistoryForm = false;
        $scope.workHistory = workHistory;

    };

    $scope.viewWorkHistory = function (index, workHistory) {
        $scope.showingDetailsView = true;
        $scope.showingDetails = false;
        $scope.workHistoryFormStatus = true;
        $scope.newWorkHistoryForm = false;
        $scope.workHistory = workHistory;

    };



    $scope.editWorkGap = function (index, workGap) {
        $scope.showingDetails = true;
        $scope.showingDetailsWorkGapView = false;
        $scope.workGapFormStatus = true;
        $scope.newWorkGapForm = false;
        $scope.workGap = workGap;

    };

    $scope.viewWorkGap = function (index, workGap) {
        $scope.showingDetailsWorkGapView = true;
        $scope.showingDetails = false;
        $scope.workGapFormStatus = true;
        $scope.newWorkGapForm = false;
        $scope.workGap = workGap;

    };

    $scope.cancelWorkHistory = function (condition) {
        if (condition == "editCancel") {
            $scope.showingDetails = false;
        } else if (condition == "viewCancel") {
            $scope.showingDetailsView = false;
        }
        
        $scope.workHistoryFormStatus = false;
        $scope.newWorkHistoryForm = false;
        $scope.workHistory = {};
    };

    $scope.cancelWorkGap = function (condition) {

        if (condition == "saveCancel") {
            $scope.newWorkGapForm = false;
        }
        else if (condition == "editCancel") {
           
            $scope.workGap = {};
        }

        else if (condition == "viewCancel") {
            $scope.showingDetailsWorkGapView = false;
        }

        
            $scope.showingDetails = false;
            $scope.workGapFormStatus = false;
            $scope.newWorkGapForm = false;
            
        
    };

    $scope.removeWorkHistory = function (index) {
        for (var i in $scope.WorkHistories) {
            if (index == i) {
                $scope.WorkHistories.splice(index, 1);
                break;
            }
        }
    };

    $scope.removeWorkGap = function (index) {
        for (var i in $scope.WorkGaps) {
            if (index == i) {
                $scope.WorkGaps.splice(index, 1);
                break;
            }
        }
    };


    /* new code*/

    // saves new work gap info
    $scope.saveWorkGap = function () {

        console.log($scope.newWorkGapInfo);

        var valid = isValidFormData('addNewWorkGapForm');

       

        if (valid) {
            url = "/Profile/WorkHistory/SaveWorkGapInfo";
            $http.post(url, $scope.newWorkGapInfo).
             success(function (data, status, headers, config) {

                 alert("Success");
                 $scope.updateWorkGapList($scope.newWorkGapInfo);
                 $scope.newWorkGapInfo={};
                 $scope.newWorkGapForm = false;
             }).
             error(function (data, status, headers, config) {
                 alert("Error");
             });
        }

    };

    $scope.updateWorkGapList = function (newWorkGapInfo) {
        
        newWorkGapInfo.StartDate = formatDate(newWorkGapInfo.StartDate)
        newWorkGapInfo.EndDate = formatDate(newWorkGapInfo.EndDate)
        $scope.WorkGaps.push(newWorkGapInfo);

    };

    $scope.saveWorkHistory = function () {
        //================== Save Here ============
        //$scope.WorkHistories.push(workHistory);
        //================== hide Show Condition ============

        var valid = isValidFormData('newWorkHistoryForm');
         
        console.log($scope.newProWorkInfo);

        //$scope.workHistoryFormStatus = false;
        //$scope.newWorkHistoryForm = false;
        //$scope.workHistory = {};

        if (valid) {
            url = "/Profile/WorkHistory/SaveWorkExp";
            $http.post(url, $scope.newProWorkInfo).
             success(function (data, status, headers, config) {

                 alert("Success" + data.WorkGapID);
                 
                 $scope.newProWorkInfo = {};

             }).
             error(function (data, status, headers, config) {
                 alert(data);
             });
        }
    };







    $scope.MilitaryServiceInformations = null;

    $scope.PublicHealthServices = null;



    $scope.hideAdmit = function () {
        $scope.admit = false;
    }


    $scope.showAdmit = function () {
        $scope.admit = true;
    }

    //============Public Health Service===============================


    $scope.editShowPublicHealthService = false;
    $scope.viewPublicHealthServiceDetails = false;
    $scope.newShowPublicHealthService = false;
    $scope.submitButtonText = "Add";
    $scope.IndexValue = 0;
    
    //$scope.viewPublicHealthService = function (index, publicHealthService) {
    //    $scope.editShowPublicHealthService = false;
    //    $scope.viewShowPublicHealthService = true;
    //    $scope.publicHealthService = publicHealthService;
    //    $scope.IndexValue = index;
    //};


    $scope.addPublicHealthService = function () {
        $scope.newShowPublicHealthService = true;
        $scope.submitButtonText = "Add";
        $scope.publicHealthService = {};
        ResetPublicHealthServiceForm();
    };

    $scope.editPublicHealthService = function (index, publicHealthService) {
        $scope.viewShowPublicHealthService = false;
        $scope.editShowPublicHealthService = true;
        $scope.viewPublicHealthServiceDetails = false;
        $scope.submitButtonText = "Update";
        $scope.publicHealthService = publicHealthService;
        $scope.IndexValue = index;
    };

    $scope.viewPublicHealthService = function (index, publicHealthService) {
        $scope.editShowPublicHealthService = false;
        $scope.viewPublicHealthServiceDetails = true;
        $scope.publicHealthService = publicHealthService;
        $scope.IndexValue = index;
    };

    

    $scope.cancelPublicHealthService = function (condition) {
        setPublicHealthServiceCancelParameters();
    };

    $scope.cancelPublicHealthServiceView = function (condition) {
        $scope.viewShowPublicHealthService = false;
        setPublicHealthServiceCancelParameters();
    };

    //$scope.cancelPublicHealthServicView = function (condition) {
    //    $scope.viewShowPublicHealthService = false;
    //    setPublicHealthServiceCancelParameters();
    //};

    $scope.savePublicHealthService = function (publicHealthService) {

        console.log(publicHealthService);

        var validationStatus;
        var url;

        if ($scope.newShowPublicHealthService) {
            //Add Details - Denote the URL
            validationStatus = $('#newShowPublicHealthServiceDiv').find('form').valid();
            url = "/Profile/ServiceInformation/AddPublicHealthService?profileId=1";
        }
        else if ($scope.editShowPublicHealthService) {
            //Update Details - Denote the URL
            validationStatus = $('#publicHealthServiceEditDiv' + $scope.IndexValue).find('form').valid();
            url = "/Profile/ServiceInformation/UpdatePublicHealthService?profileId=1";
        }

        console.log(publicHealthService);

        if (validationStatus) {
            // Simple POST request example (passing data) :
            $http.post(url, publicHealthService).
              success(function (data, status, headers, config) {

                  alert("Success");
                  if ($scope.newShowPublicHealthService) {
                      //Add Details - Denote the URL
                      publicHealthService.PublicHealthServiceID = data;
                      $scope.PublicHealthService.push(publicHealthService);
                  }
                  setPublicHealthServiceCancelParameters();
              }).
              error(function (data, status, headers, config) {
                  alert("Error");
              });
        }
    };

    function setPublicHealthServiceCancelParameters() {
        $scope.viewShowPublicHealthService = false;
        $scope.editShowPublicHealthService = false;
        $scope.newShowPublicHealthService = false;
        $scope.publicHealthService = {};
        $scope.IndexValue = 0;
    }

    function ResetPublicHealthServiceForm() {
        $('#newShowPublicHealthServiceDiv').find('.publicHealthServiceForm')[0].reset();
        $('#newShowPublicHealthServiceDiv').find('span').html('');
    }

 

    //======================Military Service Information ===================

    $scope.editShowMilitaryServiceInformation = false;
    $scope.viewShowMilitaryServiceInformation = false;
    $scope.newShowMilitaryServiceInformation = false;
    $scope.submitButtonText = "Add";
    $scope.IndexValue = 0;

    $scope.viewMilitaryServiceInformation = function (index, militaryServiceInformation) {
        $scope.editShowMilitaryServiceInformation = false;
        $scope.viewShowMilitaryServiceInformation = true;
        $scope.militaryServiceInformation = militaryServiceInformation;
        $scope.IndexValue = index;
    };


    $scope.addMilitaryServiceInformation = function () {
        $scope.newShowMilitaryServiceInformation = true;
        $scope.submitButtonText = "Add";
        $scope.militaryServiceInformation = {};
        ResetMilitaryServiceInformationForm();
    };

    $scope.editMilitaryServiceInformation = function (index, militaryServiceInformation) {
        $scope.viewShowMilitaryServiceInformation = false;
        $scope.editShowMilitaryServiceInformation = true;
        $scope.viewShowMilitaryServiceInformation = false;
        $scope.submitButtonText = "Update";
        $scope.militaryServiceInformation = militaryServiceInformation;
        $scope.IndexValue = index;
    };

    //$scope.viewMilitaryServiceInformation = function (index, militaryServiceInformation) {
    //    $scope.viewShowMilitaryServiceInformation = true;
    //    $scope.editShowMilitaryServiceInformation = false;
    //    $scope.militaryServiceInformation = militaryServiceInformation;
    //    $scope.IndexValue = index;
    //};

    $scope.cancelMilitaryServiceInformation = function (condition) {
        setMilitaryServiceInformationCancelParameters();
    };

    $scope.saveMilitaryServiceInformation = function (militaryServiceInformation) {

        console.log(militaryServiceInformation);

        var validationStatus;
        var url;

        if ($scope.newShowMilitaryServiceInformation) {
            //Add Details - Denote the URL
            validationStatus = $('#newShowMilitaryServiceInformationDiv').find('form').valid();
            url = "/Profile/ServiceInformation/AddMilitaryServiceInformation?profileId=1";
        }
        else if ($scope.editShowMilitaryServiceInformation) {
            //Update Details - Denote the URL
            validationStatus = $('#militaryServiceInformationEditDiv' + $scope.IndexValue).find('form').valid();
            url = "/Profile/ServiceInformation/UpdateMilitaryServiceInformation?profileId=1";
        }

        console.log(militaryServiceInformation);

        if (validationStatus) {
            // Simple POST request example (passing data) :
            $http.post(url, militaryServiceInformation).
              success(function (data, status, headers, config) {

                  alert("Success");
                  if ($scope.newShowPublicHealthService) {
                      //Add Details - Denote the URL
                      militaryServiceInformation.MilitaryServiceInformationID = data;
                      $scope.MilitaryServiceInformation.push(militaryServiceInformation);
                  }
                  setMilitaryServiceInformationCancelParameters();
              }).
              error(function (data, status, headers, config) {
                  alert("Error");
              });
        }
    };

    function setMilitaryServiceInformationCancelParameters() {
        $scope.viewShowMilitaryServiceInformation = false;
        $scope.editShowMilitaryServiceInformation = false;
        $scope.newShowMilitaryServiceInformation = false;
        $scope.militaryServiceInformation = {};
        $scope.IndexValue = 0;
    }

    function ResetMilitaryServiceInformationForm() {
        $('#newShowMilitaryServiceInformationDiv').find('.militaryServiceInformationForm')[0].reset();
        $('#newShowMilitaryServiceInformationDiv').find('span').html('');
    }  
});



function formatDate(date) {

    var formattedDate = new Date(date);
    var d = formattedDate.getDate();
    var m = formattedDate.getMonth();
    m += 1;  // JavaScript months are 0-11
    var y = formattedDate.getFullYear();
    return d + "-" + m + "-" + y;
}