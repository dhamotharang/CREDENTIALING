
angular.module('planContractApp', [])

//---------------------------- service for get location ----------------------
.service('locationService', ['$http', function ($http) {
    this.getLocations = function (QueryString) {
        return $http.get(rootDir + "/Location/GetCities?city=" + QueryString)
        .then(function (response) { return response.data; });
    };
}])
    .controller('PlanContractController', function ($http, $scope, $location, $anchorScroll, locationService) {
    
    //------------- get List Of Plan ---------------
        $http.get(rootDir + '/MasterDataNew/GetAllPlans').
        success(function (data, status, headers, config) {
            try {
                $scope.Plans = data;
                $scope.data = data;
                //$scope.init_table(data);
            } catch (e) {
               
            }
        }).
        error(function (data, status, headers, config) {
        });

        $scope.MasterLOBs = [];

        //------------- get List Of Lobs ---------------
        $http.get(rootDir + '/MasterDataNew/GetAllLobs').
       success(function (data, status, headers, config) {
           try {
               $scope.Lobs = angular.copy(data);
               $scope.MasterLOBs = data;
           } catch (e) {
             
           }
       }).
       error(function (data, status, headers, config) {
       });

        //----------------------------- Get List Of Groups --------------------------
        $http.get(rootDir + '/MasterDataNew/GetAllGroups').
        success(function (data, status, headers, config) {
            $scope.BusinessEntities = data;
        }).
        error(function (data, status, headers, config) {
        });


    //============Defining and array for lobs========
    $scope.TempLOBs = [];
    $scope.SelectedPlan = {};

        ///======To get all lob related to a plan=============
    $scope.PlanLOBs = [];
    $scope.selectPlan = function (planid) {
        for (var i in $scope.Plans) {
            if (planid == $scope.Plans[i].PlanID) {
                $scope.SelectedPlan = $scope.Plans[i];
                break;
            }
        }

        $.ajax({
            type: "POST",
            url: rootDir + "/Credentialing/Plan/GetPlanLobForPlan",
            data: JSON.stringify({ PlanId: planid }),
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            async:false,
            success: function (data) {
                try {
                    $scope.PlanLOBs = data;
                    $scope.PlanContract(planid);
                } catch (e) {
                  
                }
            },
            failure: function (errMsg) {
            }
        });

        $scope.formatData($scope.BusinessEntities, $scope.TempLOBs);
        $scope.showDiv = true;
        $scope.showPersonDetails = false;
    };

    $scope.BE_LOB_Maps = [];

    //////--------------------- Grid View Method for LOB BE Mapping Controlls -------------------------
    $scope.formatData = function (BE, LOB) {
        $scope.BE_LOB_Maps = [];
        for (var i = 0; i < LOB.length; i++) {
            var bes = [];
            for (var j = 0; j < BE.length; j++) {
                bes.push({ BE: BE[j], IsChecked: false });
            }
            $scope.BE_LOB_Maps.push({ PlanLOB: LOB[i], BEs: bes, IsChecked: false });
        }
    };

    $scope.formatDataForView = function (BE, LOB, planContracts) {
        $scope.BE_LOB_Maps = [];
        for (var i = 0; i < LOB.length; i++) {
            var bes = [];
            for (var j = 0; j < BE.length; j++) {

                var status = false;

                for (var k in planContracts) {
                    if (planContracts[k].PlanLOB.LOBID == LOB[i].LOBID && planContracts[k].OrganizationGroupID == BE[j].GroupID) {
                        status = true;
                    }
                }
                if (status) {
                    bes.push({ BE: BE[j], IsChecked: true });
                } else {
                    bes.push({ BE: BE[j], IsChecked: false });
                }
            }
            $scope.BE_LOB_Maps.push({ PlanLOB: LOB[i], BEs: bes });
        }
    };
    //-------------------------- Save Plan LOB BE Mapping --------------------------
    $scope.SaveData = function () {
        var PlanContractDetails = [];
        var LObs = [];
        for (var i in $scope.BE_LOB_Maps) {
            var BEs = [];
            for (var j in $scope.BE_LOB_Maps[i].BEs) {
                if ($scope.BE_LOB_Maps[i].BEs[j].IsChecked) {
                    BEs.push($scope.BE_LOB_Maps[i].BEs[j].BE.GroupID);
                }
            }
            LObs.push(BEs);
        }
        for (var i in $scope.BE_LOB_Maps) {
            PlanContractDetails.push(
                    {
                        PlanContractDetailID: 0,
                        LOBID: $scope.BE_LOB_Maps[i].LOB.LOBID,
                        ContactDetail: $scope.BE_LOB_Maps[i].ContactDetail,
                        AddressDetail: $scope.BE_LOB_Maps[i].AddressDetail,
                        BEs: LObs[i]
                    }
                );
        }

        var planContractViewModel = {
            PlanContractID: null,
            PlanID: $scope.SelectedPlan.PlanID,
            PlanContractDetails: PlanContractDetails,
            IsDelegated : $scope.IsDelegated
        };


        $http.post(rootDir + '/Credentialing/Plan/LobBeMapping', { planContractViewModel: planContractViewModel }).
          success(function (data, status, headers, config) {
          }).
          error(function (data, status, headers, config) {
          });

    };

    $scope.PlanContract = function (plandata) {
       $http.post(rootDir + '/Credentialing/Plan/GetPlanContractForPlan', { PlanId: plandata }).
        success(function (data, status, headers, config) {
            $scope.plancontractviewdata = data;
            $scope.formatDataForView($scope.BusinessEntities, $scope.PlanLOBs, $scope.plancontractviewdata);
        }).
   error(function (data, status, headers, config) {
   });

    };

    $scope.CancelData = function () {
        $scope.showDiv = false;
        $scope.SelectedPlan.PlanID = null;
    }

    //========================== View Popover Contact Country Code ============================
    $scope.showContryCodeDiv = function (div_Id) {
        $("#" + div_Id).show();
    };

    //------------------------------ Save Selected LOB Othre Information ----------------
    $scope.SaveLOBOtherInfo = function (data) {
        for (var i in $scope.BE_LOB_Maps) {
            if ($scope.SelectedLOB.LOB.LOBID == $scope.BE_LOB_Maps[i].LOB.LOBID) {
                $scope.BE_LOB_Maps[i].ContactDetail = angular.copy(data.ContactDetail);
                $scope.BE_LOB_Maps[i].AddressDetail = angular.copy(data.AddressDetail);
                break;
            }
        }
        $scope.showPersonDetails = false;
        $scope.tempObject = {};
    };
    $scope.checkRow = function (rowIndex, state) {

        var selectedRow = $scope.tableData[rowIndex];
        for (var i = 0; i < selectedRow.col.length; i++) {
            selectedRow.col[i].check = state;
        };
    };

    $scope.uncheck = function (rowIndex) {

        $scope.tableData[rowIndex].rowCheck = false;
    };
});


//================== To hide all country and city popover ============================

$(function () {
    $(".ProviderTypeSelectAutoList").hide();
    $(".countryDailCodeContainer").hide();
})

$(document).click(function (event) {
    if (!$(event.target).hasClass("countryCodeClass") && $(event.target).parents(".countryDailCodeContainer").length === 0) {
        $(".countryDailCodeContainer").hide();
    }
    if (!$(event.target).hasClass("form-control") && $(event.target).parents(".ProviderTypeSelectAutoList").length === 0) {
        $(".ProviderTypeSelectAutoList").hide();
    }
});

function showLocationList(ele) {
    $(ele).parent().find(".ProviderTypeSelectAutoList").first().show();
}

function goToByScroll(id) {
    if ($("#" + id) && $("#" + id).offset() && $("#" + id).offset().top) {
        $('html,body').animate({
            scrollTop: $("#" + id).offset().top - 60
        }, 'fast');
    }
}