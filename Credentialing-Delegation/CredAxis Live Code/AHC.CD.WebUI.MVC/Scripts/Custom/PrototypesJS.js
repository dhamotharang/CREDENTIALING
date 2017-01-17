var prototypeApp = angular.module('prototypeApp', ['ui.bootstrap', 'smart-table', 'mgcrea.ngStrap', 'ngTable']);
var SearchProviderPanelToggle = function () {
    $("#SearchProviderPanel").slideToggle();
}
var SearchTLPanelToggle = function () {
    $("#searchTLResultID").slideToggle();
}
prototypeApp.factory('Resource', ['$q', '$rootScope', '$filter', '$timeout', function ($q, $rootScope, $filter, $timeout) {

    function getPage(start, number, params) {

        var deferred = $q.defer();

        $rootScope.filtered = params.search.predicateObject ? $filter('filter')($rootScope.PrototypeData, params.search.predicateObject) : $rootScope.PrototypeData;
        if (params.sort.predicate) {
            $rootScope.filtered = $filter('orderBy')($rootScope.filtered, params.sort.predicate, params.sort.reverse);
        }

        var result = $rootScope.filtered.slice(start, start + number);
        $timeout(function () {
            deferred.resolve({
                data: result,
                numberOfPages: Math.ceil($rootScope.filtered.length / number)
            });
        });


        return deferred.promise;
    }

    return {
        getPage: getPage
    };


}]);
prototypeApp.directive('stRatio', function () {
    return {
        link: function (scope, element, attr) {
            var ratio = +(attr.stRatio);

            element.css('width', ratio + '%');

        }
    };
});

prototypeApp.directive('pageSelect', function () {
    return {
        restrict: 'E',
        template: '<input type="text" class="select-page" ng-model="inputPage" ng-change="PageResize(inputPage,numPages)">',
        controller: function ($scope) {
            $scope.$watch('inputPage', function (newV, oldV) {
                if (newV === oldV) {
                    return;
                }
                else if (newV >= oldV) {
                    $scope.inputPage = newV;
                    $scope.selectPage(newV);
                    //$scope.currentPage = newV;
                }
                else {
                    $scope.selectPage(newV);
                }

            });
            $scope.PageResize = function (currentPage, maxPage) {
                if (currentPage >= maxPage) {
                    $scope.inputPage = maxPage;
                    $scope.selectPage(maxPage);
                }
                else {
                    $scope.selectPage(currentPage);
                }
            }
        },
        link: function (scope, element, attrs) {
            scope.$watch('currentPage', function (c) {
                scope.inputPage = c;
            });
        }
    }
});


prototypeApp.run(function ($rootScope) {
    $rootScope.PrototypeData = [];
    $rootScope.CCOData = [
  {
      "CCOName": "Anjali",
      "NoOfProviders": "4",
      "NoOfTasks": "33",
      "ApplnSubmitted": "22",
      "PackagePending": "10",
      "CredPackage": "1",
      "NoOfActivities": "112"
  },
  {
      "CCOName": "Sherry",
      "NoOfProviders": "12",
      "NoOfTasks": "56",
      "ApplnSubmitted": "11",
      "PackagePending": "9",
      "CredPackage": "7",
      "NoOfActivities": "99"
  },
  {
      "CCOName": "Elizabeth",
      "NoOfProviders": "33",
      "NoOfTasks": "110",
      "ApplnSubmitted": "2",
      "PackagePending": "7",
      "CredPackage": "3",
      "NoOfActivities": "298"
  },
  {
      "CCOName": "Josline",
      "NoOfProviders": "11",
      "NoOfTasks": "33",
      "ApplnSubmitted": "11",
      "PackagePending": "3",
      "CredPackage": "5",
      "NoOfActivities": "98"
  }
    ]

    $rootScope.ProviderData = [
  {
      "ProName": "Smith,Michale",
      "NPI": "1236257628",
      "ProStatus": "100",
      "CredCompleted": "4",
      "CredIncompleted": "3",
      "ccoAssigned": "Anjali"
  },
  {
      "ProName": "John,Smith",
      "NPI": "1236257628",
      "ProStatus": "50",
      "CredCompleted": "3",
      "CredIncompleted": "1",
      "ccoAssigned": "Sherry"
  },
  {
      "ProName": "John,Mcd",
      "NPI": "1236257628",
      "ProStatus": "100",
      "CredCompleted": "2",
      "CredIncompleted": "4",
      "ccoAssigned": "Elizabeth"
  },
  {
      "ProName": "Dr.Meghan",
      "NPI": "1236257628",
      "ProStatus": "66",
      "CredCompleted": "1",
      "CredIncompleted": "2",
      "ccoAssigned": "Elizabeth"
  }
    ]

});


prototypeApp.controller('PrototypesCtrl', function ($scope, $rootScope, $http, $q, $filter,ngTableParams, Resource) {
    var ctrl = this;
    this.displayed = [];
    $scope.showProCredData = false;
    $scope.showCCOCredData = false;
    $scope.ShowProfileDelegation = false;
    $scope.data = []; //data in scope is declared
    $scope.progressbar = false;
    $scope.ccoAssign = true;
    $scope.error_message = "";
    $scope.groupBySelected = "none";


    this.callServer = function callServer(tableState) {
        ctrl.isLoading = true;
        var pagination = tableState.pagination;
        console.log(pagination.number);
        var start = pagination.start || 0;
        var number = pagination.number || 10;
        $scope.t = tableState;
        Resource.getPage(start, number, tableState).then(function (result) {
            ctrl.displayed = result.data;
            ctrl.temp = ctrl.displayed;
            console.log(result.data);
            tableState.pagination.numberOfPages = result.numberOfPages;//set the number of pages so the pagination can update
            ctrl.isLoading = false;
        });
    };

    $scope.RefreshData = function (name) {
        if (name == 'CCO') {
            $rootScope.PrototypeData = angular.copy($rootScope.CCOData);
        }
        else if (name = 'PRO') {
            $rootScope.PrototypeData = angular.copy($rootScope.ProviderData);
        }
        var tableState = {
            sort: {},
            search: {},
            pagination: {
                start: 0
            }
        };
        ctrl.callServer(tableState);
    }

    $scope.showCred = function (role) {
        $scope.showProCredData = role == 'Pro' ? true : false;
        $scope.showCCOCredData = role == 'CCO' ? true : false;
    }

    $scope.displayData = function (assignedPerson) {
        if (assignedPerson == 'CCOAssignedData') {
            $scope.ccoAssign = true;
            $scope.ProviderList1 = angular.copy(ProviderList);
        }
        else {
            $scope.ccoAssign = false;
            $scope.ProviderList1 = angular.copy(TLList);
        }
    }

    $scope.credDetailsData = [
        {
            "PlanName": "Aetna",
            "Start": "5/10/2016",
            "Initiated": "Smith,Michale",
            "CredPhase": "Initiated",
            "CredStat": "Waiting for plan form",
            "Pending": "3",
            "AvgTime": "4"
        },
  {
      "PlanName": "Ultimate",
      "Start": "9/6/2016",
      "Initiated": "John,Mcd",
      "CredPhase": "Plan Enrollment",
      "CredStat": "Completed",
      "Pending": "0",
      "AvgTime": "0"
  },
  {
      "PlanName": "Cigna",
      "Start": "9/9/2016",
      "Initiated": "John,Smith",
      "CredPhase": "Submit to plan",
      "CredStat": "Waiting for plan announcement",
      "Pending": "30",
      "AvgTime": "3"
  },
  {
      "PlanName": "Humana",
      "Start": "9/22/2015",
      "Initiated": "John,Mcd",
      "CredPhase": "PSV completed",
      "CredStat": "PSV failed",
      "Pending": "358",
      "AvgTime": "2"
  },
  {
      "PlanName": "Ameri Health Care",
      "Start": "1/22/2016",
      "Initiated": "Dr.Meghan",
      "CredPhase": "Package Generated",
      "CredStat": "Waiting for document from provider",
      "Pending": "180",
      "AvgTime": "6"
  }
    ]

    $http.get(rootDir + '/Profile/MasterData/GetAllProviderLevels').
      success(function (data, status, headers, config) {
          $scope.ProviderLevels = data;

      }).
      error(function (data, status, headers, config) {
      });

    $http.get(rootDir + '/Profile/MasterData/GetAllProviderTypes').
      success(function (data, status, headers, config) {
          $scope.masterProviderTypes = data;

      }).
      error(function (data, status, headers, config) {
      });

    $scope.searchCumDropDown = function (divId) {
        $("#" + divId).show();
    };

    //Bind the IPA name with model class to achieve search cum drop down
    $scope.addIntoIPADropDown = function (ipa, div) {

        $scope.searchProvider.IPAGroupNameDup = ipa;
        $scope.searchProvider.IPAGroupName = ipa;

        $("#" + div).hide();
    }

    //Bind the IPA name with model class to achieve search cum drop down
    $scope.addIntoTypeDropDown = function (type, div) {
        $scope.searchProvider.ProviderType = type.Title;
        $scope.searchProvider.ProviderTypeDup = type.Title;
        $("#" + div).hide();
    }

    //============================= Data From Master Data Table  ======================    
    //----------------------------- Get List Of Groups --------------------------    
    $http.get(rootDir + '/MasterData/Organization/GetGroups').
      success(function (data, status, headers, config) {
          $scope.PracticingGroups = data;
      }).
      error(function (data, status, headers, config) {
      });

    $scope.new_search_Profile = function (condition) {
        $scope.data = [];
        $scope.error_message = "";
        $scope.progressbar = true;

        $http({
            method: "POST",
            url: rootDir + "/SearchProfile/SearchProfileJson",
            data: {
                NPINumber: $scope.searchProvider.NPINumber, FirstName: $scope.searchProvider.FirstName,
                LastName: $scope.searchProvider.LastName, ProviderRelationship: $scope.searchProvider.ProviderRelationship, IPAGroupName: $scope.searchProvider.IPAGroupName,
                ProviderLevel: $scope.searchProvider.ProviderLevel, ProviderType: $scope.searchProvider.ProviderType
            }
        }).success(function (resultData) {
            try {
                if (resultData.searchResults.length != 0) {
                    SearchProviderPanelToggle();
                    $scope.data = resultData.searchResults;
                    $scope.init_table(resultData.searchResults, condition);
                    $scope.searchProvider = "";
                    $scope.progressbar = false;
                }
                else {
                    $scope.progressbar = false;
                    messageAlertEngine.callAlertMessage('noProviderDetails', "No Record Available for the Given Option", "danger", true);
                    $scope.data = "";
                }
            } catch (e) {

            }


        }).error(function () { $scope.progressbar = false; $scope.error_message = "An Error occured !! Please Try Again !!"; })
    }

    //Created function to be called when data loaded dynamically
    $scope.init_table = function (data, condition) {

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
        if (condition == 1) {
            for (var i = 0; i < $scope.data.length; i++) {
                $scope.data[i].FirstName = ""; //initialization of new property 
                $scope.data[i].LastName = "";
                $scope.data[i].NPINumber = "";
                $scope.data[i].FullTitles = "";
                $scope.data[i].FullRelations = "";

                $scope.data[i].FirstName = $scope.data[i].PersonalDetail.FirstName;  //set the data from nested obj into new property
                $scope.data[i].LastName = $scope.data[i].PersonalDetail.LastName;
                $scope.data[i].NPINumber = $scope.data[i].OtherIdentificationNumber.NPINumber;
                //------------------ for full titles ---------------------
                var fulltitles = "";
                for (var j in $scope.data[i].PersonalDetail.ProviderTitles) {
                    if (j > 0) {
                        fulltitles += ", ";
                    }
                    fulltitles += $scope.data[i].PersonalDetail.ProviderTitles[j].ProviderType.Title;
                }
                $scope.data[i].FullTitles = fulltitles;
                //-------------- for full relations ---------------
                var fullrelations = "";
                for (var j in $scope.data[i].ContractInfoes) {
                    if (j > 0) {
                        fullrelations += ", ";
                    }
                    fullrelations += $scope.data[i].ContractInfoes[j].ProviderRelationship;
                }
                $scope.data[i].FullRelations = fullrelations;
            }
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

    $scope.clearSearch = function () {
        $scope.searchProvider = "";
        $scope.data = "";
    }

    var ProviderList = [
      { ProviderID: 1, Photo: "/Resources/Images/author.jpg", Name: 'Dr. Jennine Martin', NPI: 1223344556, Specialty: ' Dentist, Neuro Surgon', Location: 'Spring Hill, Florida', Group: 'Access',ProAssigned:'13',TaskAssigned:'670',Pending:'270' },
      { ProviderID: 2, Photo: '/Resources/Images/images (2).jpg', Name: 'Dr. Barbara Joy', NPI: 1111111111, Specialty: ' Neuro Surgon', Location: 'Spring Hill, Florida', Group: 'Access', ProAssigned: '13', TaskAssigned: '670', Pending: '270' },
      { ProviderID: 3, Photo: '/Resources/Images/images (3).jpg', Name: 'Dr. Daina Jeccob', NPI: 2222222222, Specialty: ' Dentist', Location: 'Spring Hill, Florida', Group: 'Access', ProAssigned: '13', TaskAssigned: '670', Pending: '270' },
      { ProviderID: 4, Photo: '/Resources/Images/images (4).jpg', Name: 'Dr. Parikshit Singh', NPI: 3333333333, Specialty: ' Dentist, Neuro Surgon', Location: 'Alaska', Group: 'Access2', ProAssigned: '13', TaskAssigned: '670', Pending: '270' },
      { ProviderID: 5, Photo: '/Resources/Images/images (1).jpg', Name: 'Dr. Sanjay Singh', NPI: 4444444444, Specialty: ' Dentist, Neuro Surgon', Location: 'Spring Hill, Florida', Group: 'Access', ProAssigned: '13', TaskAssigned: '670', Pending: '270' },
      { ProviderID: 6, Photo: '/Resources/Images/images (2).jpg', Name: 'Dr. Barbara Joy', NPI: 1223344556, Specialty: ' Dentist, Neuro Surgon', Location: 'Spring Hill, Florida', Group: 'Access', ProAssigned: '13', TaskAssigned: '670', Pending: '270' },
      { ProviderID: 7, Photo: '/Resources/Images/images (3).jpg', Name: 'Dr. Daina Jeccob', NPI: 1223344556, Specialty: ' Dentist, Neuro Surgon', Location: 'Spring Hill, Florida', Group: 'Access', ProAssigned: '13', TaskAssigned: '670', Pending: '270' },
      { ProviderID: 8, Photo: '/Resources/Images/images (4).jpg', Name: 'Dr. Parikshit Singh', NPI: 1223344556, Specialty: ' Dentist, Neuro Surgon', Location: 'Spring Hill, Florida', Group: 'Access', ProAssigned: '13', TaskAssigned: '670', Pending: '270' },
      { ProviderID: 9, Photo: '/Resources/Images/images (4).jpg', Name: 'Dr. Parikshit Singh', NPI: 1223344556, Specialty: ' Dentist, Neuro Surgon', Location: 'Spring Hill, Florida', Group: 'Access' }
      //{ ProviderID: 10, Photo: '/Resources/Images/images (3).jpg', Name: 'Dr. Daina Jeccob', NPI: 1223344556, Specialty: ' Dentist, Neuro Surgon', Location: 'Spring Hill, Florida', Group: 'Access' },
      //{ ProviderID: 11, Photo: '/Resources/Images/images (2).jpg', Name: 'Dr. Barbara Joy', NPI: 1223344556, Specialty: ' Dentist, Neuro Surgon', Location: 'Spring Hill, Florida', Group: 'Access' },
      //{ ProviderID: 12, Photo: '/Resources/Images/images (1).jpg', Name: 'Dr. Sanjay Singh', NPI: 1223344556, Specialty: ' Dentist, Neuro Surgon', Location: 'Spring Hill, Florida', Group: 'Access' },
      //{ ProviderID: 13, Photo: '/Resources/Images/images (2).jpg', Name: 'Dr. Barbara Joy', NPI: 1223344556, Specialty: ' Dentist, Neuro Surgon', Location: 'Spring Hill, Florida', Group: 'Access' },
      //{ ProviderID: 14, Photo: '/Resources/Images/images (3).jpg', Name: 'Dr. Daina Jeccob', NPI: 1223344556, Specialty: ' Dentist, Neuro Surgon', Location: 'Spring Hill, Florida', Group: 'Access' },
      //{ ProviderID: 15, Photo: '/Resources/Images/images (4).jpg', Name: 'Dr. Parikshit Singh', NPI: 1223344556, Specialty: ' Dentist, Neuro Surgon', Location: 'Spring Hill, Florida', Group: 'Access' },
      //{ ProviderID: 16, Photo: '/Resources/Images/images (1).jpg', Name: 'Dr. Sanjay Singh', NPI: 1223344556, Specialty: ' Dentist, Neuro Surgon', Location: 'Spring Hill, Florida', Group: 'Access' }
    ]

    var TLList = [
        { ProviderID: 10, Photo: '/Resources/Images/images (3).jpg', Name: 'Dr. Daina Jeccob', NPI: 1223344556, Specialty: ' Dentist, Neuro Surgon', Location: 'Spring Hill, Florida', Group: 'Access' },
        { ProviderID: 11, Photo: '/Resources/Images/images (2).jpg', Name: 'Dr. Barbara Joy', NPI: 1223344556, Specialty: ' Dentist, Neuro Surgon', Location: 'Spring Hill, Florida', Group: 'Access' },
        { ProviderID: 12, Photo: '/Resources/Images/images (1).jpg', Name: 'Dr. Sanjay Singh', NPI: 1223344556, Specialty: ' Dentist, Neuro Surgon', Location: 'Spring Hill, Florida', Group: 'Access' },
        { ProviderID: 13, Photo: '/Resources/Images/images (2).jpg', Name: 'Dr. Barbara Joy', NPI: 1223344556, Specialty: ' Dentist, Neuro Surgon', Location: 'Spring Hill, Florida', Group: 'Access' },
        { ProviderID: 14, Photo: '/Resources/Images/images (3).jpg', Name: 'Dr. Daina Jeccob', NPI: 1223344556, Specialty: ' Dentist, Neuro Surgon', Location: 'Spring Hill, Florida', Group: 'Access' },
        { ProviderID: 15, Photo: '/Resources/Images/images (4).jpg', Name: 'Dr. Parikshit Singh', NPI: 1223344556, Specialty: ' Dentist, Neuro Surgon', Location: 'Spring Hill, Florida', Group: 'Access' },
    ]
    $scope.ProviderList1 = angular.copy(ProviderList);    

    $scope.AssignToCCO = function () {
        $scope.ShowProfileDelegation = true;
        SearchTLPanelToggle();
    }

    $scope.showAssignmentConfirmation = function (Proname) {
        $('#inactiveWarningModal1').modal();
        $scope.ccoName = Proname;
    }

    $scope.ProviderDetails = [
        {
            Name: "Dr. Smith",
            NPI: 123445667,
            Specialty: "Family",
            Plan: "Aetna",
            Status:"Accepted",
            CCM: "Dr. Manjushree",
            CredentialingIntiatedDate: "09-24-2016",
            AppointmentDate: "09-24-2016",
            RecommendedLevel:"Level 1"
        },
        {
            Name: "Dr. Matte",
            NPI: 3463656674,
            Specialty: "Family",
            Plan: "Aetna",
            Status: "Rejected",
            CCM: "Dr. Singh",
            CredentialingIntiatedDate: "09-24-2016",
            AppointmentDate: "09-24-2016",
            RecommendedLevel: "Level 1"
        },
        {
            Name: "Dr. Amanda",
            NPI: 6765745399,
            Specialty: "Family",
            Plan: "Aetna",
            Status: "Pending",
            CCM: "Dr. Manjushree",
            CredentialingIntiatedDate: "09-24-2016",
            AppointmentDate: "09-24-2016",
            RecommendedLevel: "Level 1"
        }
    ];

    $scope.RefreshProfileData = function () {
        $rootScope.PrototypeData = angular.copy($scope.ProviderDetails);
    }

    $scope.IsProviderApprovedRejected = false;
    $scope.ApproveRejectProvider = function () {
        $scope.IsProviderApprovedRejected = true;
    }
    $scope.CancelApproveRejectProvider = function (condition) {
        if (condition == 'Yes') {
            $scope.IsProviderApprovedRejected = false;
            $('#inactiveWarningModal3').modal();
        }
        else {
            $scope.IsProviderApprovedRejected = false;
        }
        
    }

    $scope.assignTask = function () {
        $('#inactiveWarningModal2').modal();
    }
});

$(document).ready(function () {
    $("body").tooltip({ selector: '[data-toggle=tooltip]' });
    $("#sidemenu").addClass("menu-in");
    $("#page-wrapper").addClass("menuup");
    $(".ProviderTypeSelectAutoList").hide();
});

$(document).click(function (event) {
    if (!$(event.target).hasClass("form-control") && $(event.target).parents(".ProviderTypeSelectAutoList").length === 0) {
        $(".ProviderTypeSelectAutoList").hide();
    }
});

function showLocationList(ele) {
    $(ele).parent().find(".ProviderTypeSelectAutoList").first().show();
}