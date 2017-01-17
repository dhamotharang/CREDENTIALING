
initCredApp.service('messageAlertEngine', ['$rootScope', '$timeout', function ($rootScope, $timeout) {

    $rootScope.messageDesc = "";
    $rootScope.activeMessageDiv = "";
    $rootScope.messageType = "";

    var animateMessageAlertOff = function () {
        $rootScope.closeAlertMessage();
    };



    this.callAlertMessage = function (calledDiv, msg, msgType, dismissal) { //messageAlertEngine.callAlertMessage('updateHospitalPrivilege' + IndexValue, "Data Updated Successfully !!!!", "success", true);                            
        $rootScope.activeMessageDiv = calledDiv;
        $rootScope.messageDesc = msg;
        $rootScope.messageType = msgType;
        if (dismissal) {
            $timeout(animateMessageAlertOff, 5000);
        }
    }

    $rootScope.closeAlertMessage = function () {
        $rootScope.messageDesc = "";
        $rootScope.activeMessageDiv = "";
        $rootScope.messageType = "";
    }
}])

initCredApp.controller('singleProviderCtrl', function ($scope, $http, $location, $filter, ngTableParams, messageAlertEngine) {


    $scope.data = []; //data in scope is declared
    $scope.progressbar = false;
    $scope.error_message = "";
    $scope.groupBySelected = "none";
    $scope.showInit = false;


    $scope.clearAction = function () {
        $scope.SearchProviderPanelToggleDown('SearchProviderPanel');
        $scope.data = "";
        $scope.showInit = false;
    };

    $scope.CCOAppointDataData = [
    {
        ActionID: 1,
        CredentialingList: [
            {
                FirstName: " PARIKSITH",
                LastName: "SINGH",
                PersonalDetailID: 91,
                ProviderTitles: "MD",
                Speciality: "Chiropractor",
                Plan: "ULTIMATE HEALTH PLANS",
                CredentialingDate: new Date(2014, 02, 09),
                Status: "Verified"
            },
            {
                FirstName: " QAHTAN A.",
                LastName: "ABDULFATTAH",
                PersonalDetailID: 92,
                ProviderTitles: "DO",
                Speciality: "Allergy & Immunology",
                Plan: "FREEDOM VIP CARE COPD (HMO SNP)",
                CredentialingDate: new Date(2015, 11, 20),
                Status: "Verified"
            },
            {
                FirstName: " IAN",
                LastName: "ADAM",
                PersonalDetailID: 93,
                ProviderTitles: "MD",
                Speciality: "Dentist",
                Plan: "OPTIMUM GOLD REWARDS PLAN (HMO-POS)",
                CredentialingDate: new Date(2015, 12, 09),
                Status: "Verified"
            },
            {
                FirstName: " MARC A",
                LastName: "ALESSANDRONI",
                PersonalDetailID: 94,
                ProviderTitles: "DO",
                Speciality: "Dentist",
                Plan: "OPTIMUM DIAMOND REWARDS (HMO-POS SNP)",
                CredentialingDate: new Date(2014, 09, 28),
                Status: "Verified"
            },
            {
                FirstName: " CRIDER",
                LastName: "NORMA",
                PersonalDetailID: 95,
                ProviderTitles: "MD",
                Speciality: "Allergy",
                Plan: "OPTIMUM PLATINUM PLAN (HMO-POS)",
                CredentialingDate: new Date(2015, 04, 19),
                Status: "Verified"
            },
            {
                FirstName: " John",
                LastName: "Smith",
                PersonalDetailID: 96,
                ProviderTitles: "DO",
                Speciality: "Dentist",
                Plan: "FREEDOM VIP CARE COPD (HMO SNP)",
                CredentialingDate: new Date(2014, 02, 09),
                Status: "Verified"
            }
        ]
    },
    {
        ActionID: 2,
        CredentialingList: [
            {
                FirstName: " QAHTAN A.",
                LastName: "ABDULFATTAH",
                PersonalDetailID: 91,
                ProviderTitles: "DO",
                Speciality: "Allergy & Immunology",
                Plan: "FREEDOM VIP CARE COPD (HMO SNP)",
                CredentialingDate: new Date(2015, 11, 20),
                Status: "Verified"
            },
            {
                FirstName: " PARIKSITH",
                LastName: "SINGH",
                PersonalDetailID: 92,
                ProviderTitles: "MD",
                Speciality: "Chiropractor",
                Plan: "ULTIMATE HEALTH PLANS",
                CredentialingDate: new Date(2014, 02, 09),
                Status: "Verified"
            },
            {
                FirstName: " CRIDER",
                LastName: "NORMA",
                PersonalDetailID: 93,
                ProviderTitles: "MD",
                Speciality: "Allergy",
                Plan: "OPTIMUM PLATINUM PLAN (HMO-POS)",
                CredentialingDate: new Date(2015, 04, 19),
                Status: "Verified7"
            },
            {
                FirstName: " IAN",
                LastName: "ADAM",
                PersonalDetailID: 94,
                ProviderTitles: "MD",
                Speciality: "Dentist",
                Plan: "OPTIMUM GOLD REWARDS PLAN (HMO-POS)",
                CredentialingDate: new Date(2015, 12, 09),
                Status: "Verified"
            },
            {
                FirstName: " MARC A",
                LastName: "ALESSANDRONI",
                PersonalDetailID: 95,
                ProviderTitles: "DO",
                Speciality: "Dentist",
                Plan: "OPTIMUM DIAMOND REWARDS (HMO-POS SNP)",
                CredentialingDate: new Date(2014, 09, 28),
                Status: "Verified"
            },
            {
                FirstName: " John",
                LastName: "Smith",
                PersonalDetailID: 96,
                ProviderTitles: "DO",
                Speciality: "Dentist",
                Plan: "FREEDOM VIP CARE COPD (HMO SNP)",
                CredentialingDate: new Date(2014, 02, 09),
                Status: "Verified"
            }
        ]
    },
    {
        ActionID: 3,
        CredentialingList: [
            {
                FirstName: " PARIKSITH",
                LastName: "SINGH",
                PersonalDetailID: 91,
                ProviderTitles: "MD",
                Speciality: "Chiropractor",
                Plan: "ULTIMATE HEALTH PLANS",
                CredentialingDate: new Date(2014, 02, 09),
                Status: "Verified"
            },
            {
                FirstName: " QAHTAN A.",
                LastName: "ABDULFATTAH",
                PersonalDetailID: 92,
                ProviderTitles: "DO",
                Speciality: "Allergy & Immunology",
                Plan: "FREEDOM VIP CARE COPD (HMO SNP)",
                CredentialingDate: new Date(2015, 11, 20),
                Status: "Verified"
            },
            {
                FirstName: " MARC A",
                LastName: "ALESSANDRONI",
                PersonalDetailID: 93,
                ProviderTitles: "DO",
                Speciality: "Dentist",
                Plan: "OPTIMUM DIAMOND REWARDS (HMO-POS SNP)",
                CredentialingDate: new Date(2014, 09, 28),
                Status: "Verified"
            },
            {
                FirstName: " IAN",
                LastName: "ADAM",
                PersonalDetailID: 94,
                ProviderTitles: "MD",
                Speciality: "Dentist",
                Plan: "OPTIMUM GOLD REWARDS PLAN (HMO-POS)",
                CredentialingDate: new Date(2015, 12, 09),
                Status: "Verified"
            },
            {
                FirstName: " John",
                LastName: "Smith",
                PersonalDetailID: 95,
                ProviderTitles: "DO",
                Speciality: "Dentist",
                Plan: "FREEDOM VIP CARE COPD (HMO SNP)",
                CredentialingDate: new Date(2014, 02, 09),
                Status: "Verified"
            },
            {
                FirstName: " CRIDER",
                LastName: "NORMA",
                PersonalDetailID: 96,
                ProviderTitles: "MD",
                Speciality: "Allergy",
                Plan: "OPTIMUM PLATINUM PLAN (HMO-POS)",
                CredentialingDate: new Date(2015, 04, 19),
                Status: "Verified"
            }
        ]
    }
    ];

    $scope.resetSelection = function (data) {
        var temp = [];
        for (var i in data) {
            temp[i] = false;
        }
        return temp;
    };
    $scope.setActionID = function (id) {
        $scope.selectedAction = angular.copy(id);
        //$scope.SearchProviderPanelToggleDown('SearchProviderPanel');
        //$scope.data = "";
        //    $scope.new_search(id);
        //    $scope.SelectedDetails = $scope.resetSelection($scope.CreadentialingData);
        //    $scope.DoneCreadentialing = [];

        //  $scope.dt = new Date(new Date().getFullYear(), new Date().getMonth(), new Date().getDate())
    };



    //-------------- selection ---------------
    $scope.setActionID(1);


    $scope.SearchProviderPanelToggle = function (divId) {

        $("#" + divId).slideToggle();

    };

    $http.get('/Profile/MasterData/GetAllProviderTypes').
      success(function (data, status, headers, config) {

          $scope.masterProviderTypes = data;

      }).
      error(function (data, status, headers, config) {
          //console.log("Sorry internal master data cont able to fetch.");
      });



    $http.get('/Profile/MasterData/GetAllSpecialities').
     success(function (data, status, headers, config) {

         $scope.masterSpecialities = data;

     }).
     error(function (data, status, headers, config) {
         //console.log("Sorry internal master data cont able to fetch.");
     });


    //To Display the drop down div
    $scope.searchCumDropDown = function (divId) {
        $("#" + divId).show();                
    };

    //Bind the IPA name with model class to achieve search cum drop down
    $scope.addIntoIPADropDown = function (ipa, div) {

        // $scope.tempObject.IPAGroupNameDup = ipa;
        $scope.tempObject.IPAGroupName = ipa;

        $("#" + div).hide();
    }

    //Bind the IPA name with model class to achieve search cum drop down
    $scope.addIntoSpecialtyDropDown = function (s, div) {

        //$scope.tempObject.SpecialtyDup = s;
        $scope.tempObject.Specialty = s;

        $("#" + div).hide();
    }

    //Bind the IPA name with model class to achieve search cum drop down
    $scope.addIntoTypeDropDown = function (type, div) {
        $scope.tempObject.ProviderType = type.Title;
        //$scope.tempObject.ProviderTypeDup = type.Title;
        $("#" + div).hide();
    }

    $scope.addIntoPlanDropDown = function (type, tempObject, div) {
        tempObject.PlanId = type.PlanID;
        tempObject.PlanName = type.PlanName;
       
        //$scope.tempObject.ProviderTypeDup = type.Title;
        $("#" + div).hide();
    }
    
    //============================= Data From Master Data Table  ======================    
    //----------------------------- Get List Of Groups --------------------------    
    $http.get('/MasterData/Organization/GetGroups').
      success(function (data, status, headers, config) {
          $scope.PracticingGroups = data;
      }).
      error(function (data, status, headers, config) {
          console.log("Sorry internal master data can not able to fetch.");
      });

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
        }
        else if (condition == 2 || condition == 3) {
            $scope.tableParams2 = new ngTableParams({
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
        }
       
    };

    //if filter is on
    $scope.ifFilter = function () {
        try {
            var bar;
            var obj;
            if ($scope.selectedAction == 1) {
                obj = $scope.tableParams1.$params.filter;
            }
            else if ($scope.selectedAction == 2 || $scope.selectedAction == 3) {
                obj = $scope.tableParams2.$params.filter;
            }
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
                if ($scope.selectedAction == 1) {
                    return ($scope.tableParams1.$params.page * $scope.tableParams1.$params.count) - ($scope.tableParams1.$params.count - 1);
                }
                else if ($scope.selectedAction == 2 || $scope.selectedAction == 3) {
                    return ($scope.tableParams2.$params.page * $scope.tableParams2.$params.count) - ($scope.tableParams2.$params.count - 1);
                }
            }
        }
        catch (e) { }
    }
    //Get index Last in table
    $scope.getIndexLast = function () {
        try {
            if ($scope.groupBySelected == 'none') {
                if ($scope.selectedAction == 1) {
                    return { true: ($scope.data.length), false: ($scope.tableParams1.$params.page * $scope.tableParams1.$params.count) }[(($scope.tableParams1.$params.page * $scope.tableParams1.$params.count) > ($scope.data.length))];
                }
                else if ($scope.selectedAction == 2 || $scope.selectedAction == 3) {
                    return { true: ($scope.data.length), false: ($scope.tableParams2.$params.page * $scope.tableParams2.$params.count) }[(($scope.tableParams2.$params.page * $scope.tableParams2.$params.count) > ($scope.data.length))];
                }
            }
        }
        catch (e) { }
    }

    //Get data on basis of the parameters ajax call
    $scope.new_search = function (id) {
        $scope.data = [];
        $scope.error_message = "";
        $scope.loadingAjax = true;

        $http({
            method: "POST",
            url: "/Credentialing/Initiation/SearchProviderJson?id=" + id,
            data: {
                NPINumber: $scope.tempObject.NPINumber,
                FirstName: $scope.tempObject.FirstName,
                LastName: $scope.tempObject.LastName,
                CAQH: $scope.tempObject.CAQH,
                IPAGroupName: $scope.tempObject.IPAGroupName,
                Specialty: $scope.tempObject.Specialty,
                ProviderType: $scope.tempObject.ProviderType
            }
        }).success(function (resultData) {
            console.log(resultData.searchResults.length);
            console.log(resultData);
            //console.log(resultData.searchResults);
            if (resultData.searchResults.length != 0) {
                $scope.SearchProviderPanelToggleDown('SearchProviderResultPanel');
                $scope.data = resultData.searchResults;
                $scope.init_table(resultData.searchResults, $scope.selectedAction);
                $scope.tempObject = "";
                $scope.loadingAjax = false;
            }
            else {
                $scope.loadingAjax = false;
                //if (resultData.searchResults.length == 0)
                //{
                //    messageAlertEngine.callAlertMessage('noProviderDetails', "Provider is already DeCredentialed or does not exit", "danger", true);
                //}
                //else 
                if (id == 1) {
                    messageAlertEngine.callAlertMessage('noProviderDetails', "No Record Available for the Given Option", "danger", true);
                }
                else if (id == 2 || id == 3 ) {

                    messageAlertEngine.callAlertMessage('noProviderDetails', "Credentialing not yet initiated for Provider", "danger", true);
                }
                $scope.data = "";
            }
        }).error(function () { $scope.loadingAjax = false; $scope.error_message = "An Error occured !! Please Try Again !!"; })

    }

    $scope.SearchProviderPanelToggleDown = function (divId) {
        $(".closePanel").slideUp();
        $("#" + divId).slideToggle();
    };

    $scope.SelectProviderForCredentialingInitiation = function (provObj) {
        $scope.showInit = true;

        $scope.SearchProviderPanelToggleDown('InitiationPanel');
        $scope.initiateSuccess = false;
        $scope.ProfileID = provObj.ProfileID;
        $scope.NPINumber = provObj.NPINumber;
        $scope.CAQH = provObj.CAQH;
        $scope.Firstname = provObj.FirstName;
        $scope.Lastname = provObj.LastName;
        $scope.Type = provObj.Titles;
        $scope.Specilities = provObj.Specialties;
        $scope.Groups = provObj.IPAGroupNames;
       

        $http.get('/MasterDataNew/GetAllPlans').
                 success(function (data, status, headers, config) {

                     $scope.plans = data;
                     console.log($scope.plans);
                 }).
                 error(function (data, status, headers, config) {
                 });
    };
    $scope.findIsDelegated = function (id) {
        for (i = 0; i < $scope.plans.length;i++) {
            if ($scope.plans[i].PlanID == id) {
                $scope.tempObject.IsDelegatedYesNoOption = $scope.plans[i].DelegatedType;
            }
        }
    };


    $scope.SelectProviderForReCredentialingInitiation = function (provObj) {
        
        $scope.showInit = true;
        $scope.SearchProviderPanelToggleDown('InitiationPanel');
        $scope.initiateSuccess = false;
        $scope.reCredData = provObj;
    };

    $scope.clearSearch = function () {
        $scope.tempObject = "";
        $scope.data = "";

        //$scope.allProviders = "";
        //$('a[href=#SearchResult]').trigger('click');
        // $scope.Npi = null;
    }

    $scope.ClearInit = function () {
        $scope.showInit = false;

    }
    $scope.msgAlert = false;
    $scope.InitiateCredentialing = function () {
        var obj = {
            ProfileID: $scope.ProfileID,
            NPINumber: $scope.NPINumber,
            CAQHNumber: $scope.CAQH,
            FirstName: $scope.Firstname,
            LastName: $scope.Lastname,
            PlanID: $scope.tempObject.PlanId,
            IsDelegatedYesNoOption: $scope.tempObject.IsDelegatedYesNoOption,
            StatusType: 1
        };


        $http.post('/Credentialing/Initiation/InitiateCredentialing', obj).
            success(function (data, status, headers, config) {
                //----------- success message -----------
                if (data.status == "true") {
                    $scope.initiateSuccess = true;
                    $scope.credinfoId = data.credentialingInfo.CredentialingInfoID;
                    messageAlertEngine.callAlertMessage('successfulInitiated', "Credentialing Initiated Successfully. !!!!", "success", true);
                }
                else {
                    messageAlertEngine.callAlertMessage('errorInitiated', "", "danger", true);
                    $scope.errorInitiated = data.status.split(",");
                }
            }).
            error(function (data, status, headers, config) {
                //----------- error message -----------
                messageAlertEngine.callAlertMessage('errorInitiated', "", "danger", true);
                $scope.errorInitiated = "Sorry for Inconvenience !!!! Please Try Again Later...";
            });
    }

    //Convert the date from database to normal

    $scope.ConvertDateFormat = function (value) {
        var shortDate = null;
        if (value) {
            var regex = /-?\d+/;
            var matches = regex.exec(value);
            var dt = new Date(parseInt(matches[0]));
            var month = dt.getMonth() + 1;
            var monthString = month > 9 ? month : '0' + month;
            var day = dt.getDate();
            var dayString = day > 9 ? day : '0' + day;
            var year = dt.getFullYear();
            shortDate = monthString + '/' + dayString + '/' + year;
        }
        return shortDate;
    };



    $scope.InitiateReCredentialing = function (provObj) {
        var id = provObj.CredentialingInfoID;
        //var obj2 = provObj.LoadedContracts[0].LoadedContractID;
        var obj2 = {
            //LoadedContractID: provObj.LoadedContracts[0].LoadedContractID,
            //LoadedByID: provObj.LoadedContracts[0].LoadedByID,
            //LoadedDate: $scope.ConvertDateFormat(provObj.LoadedContracts[0].LoadedDate),
            //CredentialingType: provObj.LoadedContracts[0].CredentialingType,
            //BusinessEntityID: provObj.LoadedContracts[0].BusinessEntityID,
            //SpecialtyID: provObj.LoadedContracts[0].SpecialtyID,
            //CredentialingRequestStatusType: provObj.LoadedContracts[0].CredentialingRequestStatusType,
            //LOBID: provObj.LoadedContracts[0].LOBID
            CredentialingType: provObj.CredentialingLogs[0].CredentialingType
        };

       

        $http({
            method: "POST",
            url: "/Credentialing/Initiation/InitiateReCredentialing?id=" + id,
            data: {
                obj1: obj2                
            }
        }).success(function (data, status, headers, config) {
            //----------- success message -----------
            if (data.status == "true") {
                $scope.initiateSuccess = true;
                messageAlertEngine.callAlertMessage('successfulInitiated', "ReCredentialing Initiated Successfully. !!!!", "success", true);
            }
            else {
                messageAlertEngine.callAlertMessage('errorInitiated', "", "danger", true);
                $scope.errorInitiated = data.status.split(",");
            }
        }).error(function (data, status, headers, config) {
            //----------- error message -----------
            messageAlertEngine.callAlertMessage('errorInitiated', "", "danger", true);
            $scope.errorInitiated = "Sorry for Inconvenience !!!! Please Try Again Later...";
        });
    }

    $scope.InitiateDeCredentialing = function (provObj) {
        var id = provObj.CredentialingInfoID;
        //var obj2 = provObj.LoadedContracts[0].LoadedContractID;
        var obj2 = {
            //LoadedContractID: provObj.LoadedContracts[0].LoadedContractID,
            //LoadedByID: provObj.LoadedContracts[0].LoadedByID,
            //LoadedDate: $scope.ConvertDateFormat(provObj.LoadedContracts[0].LoadedDate),
            //CredentialingType: provObj.LoadedContracts[0].CredentialingType,
            //BusinessEntityID: provObj.LoadedContracts[0].BusinessEntityID,
            //SpecialtyID: provObj.LoadedContracts[0].SpecialtyID,
            //CredentialingRequestStatusType: provObj.LoadedContracts[0].CredentialingRequestStatusType,
            //LOBID: provObj.LoadedContracts[0].LOBID
            CredentialingType: provObj.CredentialingLogs[0].CredentialingType
        };

        $http({
            method: "POST",
            url: "/Credentialing/Initiation/InitiateDeCredentialing?id=" + id,
            data: {
                obj1: obj2
            }
        }).success(function (data, status, headers, config) {
            //----------- success message -----------
            if (data.status == "true") {
                $scope.initiateSuccess = true;
                messageAlertEngine.callAlertMessage('successfulInitiated', "DeCredentialing Initiated Successfully. !!!!", "success", true);
            }
            else {
                messageAlertEngine.callAlertMessage('errorInitiated', "", "danger", true);
                $scope.errorInitiated = data.status.split(",");
            }
        }).error(function (data, status, headers, config) {
            //----------- error message -----------
            messageAlertEngine.callAlertMessage('errorInitiated', "", "danger", true);
            $scope.errorInitiated = "Sorry for Inconvenience !!!! Please Try Again Later...";
        });
    }

    //================================= Hide All search by type popover =========================

    $scope.showSearchByTypeDiv = function (div_Id) {
        changeVisibilityOfSearchByType();
        $("#" + div_Id).show();
    };
    var changeVisibilityOfSearchByType = function () {
        $(".ProviderTypeSelectAutoList1").hide();
        // method will close any other country code div already open.
    };

    //===========================================================================================
    $scope.popCount = 0;

    $(document).click(function (event) {
        if (!$(event.target).hasClass("form-control") && $(event.target).parents(".ProviderTypeSelectAutoList").length === 0) {
            $(".ProviderTypeSelectAutoList").hide();
        }
        if (!$(event.target).attr("data-searchdropdown") && $(event.target).parents(".ProviderTypeSelectAutoList1").length === 0) {
            $(".ProviderTypeSelectAutoList1").hide();
        }        
    });

    $(document).ready(function () {
        $(".ProviderTypeSelectAutoList").hide();
        $(".ProviderTypeSelectAutoList1").hide();
        $scope.SearchProviderPanelToggleDown('SearchProviderPanel');

    });

    function showLocationList(ele) {
        $(ele).parent().find(".ProviderTypeSelectAutoList").first().show();
    }

});