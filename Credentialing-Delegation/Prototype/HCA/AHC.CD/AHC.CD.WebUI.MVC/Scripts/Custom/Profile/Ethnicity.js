
//=========================== Controller declaration ==========================
profileApp.controller('ethnicityController', function ($scope, $http, dynamicFormGenerateService, countryDropDownService) {

    //=============================== initiation of data required ===========================
    $scope.Provider = {};
    
    //===================== Birth Information =======================
    $scope.Provider.BirthInformation = {
        BirthInformationID:1,
        CityOfBirth: "#####",
        CountryOfBirth: "India",
        CountyOfBirth: "######",
        DateOfBirth: "##-##-####",
        StateOfBirth: "######",
        BirthCertificatePath: "/Content/Document/DocPreview.pdf",
        BirthCertificateFile: null
    };

    $scope.Provider.VisaDetails = {
        VisaDetailID: 1,
        IsResidentOfUSA: "Yes",
        VisaInfo:  {
            VisaInfoID: 1,
            IsAuthorizedToWorkInUS: "Yes",
            VisaNumber: "",
            VisaType:"",
            VisaTypes: 1,
            VisaExpirationDate: new Date(),
            VisaStatus: "",
            VisaSponsor: "",
            VisaCertificatePath: "",
            VisaCertificateFile: null,
            GreenCardNumber: "072-761-811",
            GreenCardCertificatePath: "/Content/Document/SINGH - PERM RESIDENT CARD.pdf",
            GreenCardCertificateFile:null,
            NationalIDNumber: "",
            NationalIDCertificatePath: "",
            NationalIDCertificateFile:null,
            CountryOfIssue:""
        }
    };
    //========================== list of Languages =================
    $scope.Languages = Languages;
    //---------------- provider have languages --------------
    $scope.Provider.LanguageInfo = {
        LanguageInfoID: 1,
        CanSpeakAmericanSignLanguage: "Yes",
        KnownLanguages: [
            { KnownLanguageID: 1, Language: "Hindi", ProficiencyIndex: 1 },
            { KnownLanguageID: 2, Language: "Urdu", ProficiencyIndex: 2 },
            { KnownLanguageID: 3, Language: "Spanish", ProficiencyIndex: 3 }
        ]
    };
    //------------------------- Language know search select action function -----------------------
    $scope.TempLanguageForEdit = [];

    $scope.SelectLanguageKnown = function (lang) {
        $scope.TempLanguageForEdit.KnownLanguages.push({ KnownLanguageID: $scope.maxIdOfLanguageInDB + 1, Language: lang.name, ProficiencyIndex: $scope.TempLanguageForEdit.length });
        $scope.tempLanguages.splice($scope.tempLanguages.indexOf(lang), 1);
        $scope.maxIdOfLanguageInDB += 1;
    }
    $scope.UnselectLanguage = function (lang) {
        for (i in $scope.TempLanguageForEdit.KnownLanguages) {
            if ($scope.TempLanguageForEdit.KnownLanguages[i] == lang) {
                $scope.TempLanguageForEdit.KnownLanguages.splice($scope.TempLanguageForEdit.KnownLanguages.indexOf(lang), 1);
            }
        }
        for (var i in $scope.Languages) {
            if ($scope.Languages[i].name == lang.Language) {
                $scope.tempLanguages.push($scope.Languages[i]);
            }
        }
    }
    
    $scope.selectedLanguage = function (lang, index) {
        $scope.langForPriority = lang;
        $scope.selectedIndex = index;
}

    $scope.PriorityChange = function (condition) {
        var index = $scope.TempLanguageForEdit.KnownLanguages.indexOf($scope.langForPriority);
        if (condition == "increase") {
            $scope.TempLanguageForEdit.KnownLanguages[index].ProficiencyIndex = index;
            $scope.TempLanguageForEdit.KnownLanguages[index - 1].ProficiencyIndex = index + 1;
            $scope.selectedIndex = index-1;
        } else if (condition == "decrease") {
            $scope.TempLanguageForEdit.KnownLanguages[index].ProficiencyIndex = index + 2;
            $scope.TempLanguageForEdit.KnownLanguages[index + 1].ProficiencyIndex = index + 1;
            $scope.selectedIndex = index+1;
        }
        $scope.TempLanguageForEdit.KnownLanguages.sort(function (a, b) { return a.ProficiencyIndex - b.ProficiencyIndex });
    };
    //-------------- reusable for array generate ------------------
    $scope.getArray = function(number){
        var temp = [];
        for(var i=0;i<number;i++){
            temp.push(i);
        }
        return temp;
    };
    $scope.shoLanguageList = function (divId) {
        $("#" + divId).show();
    };
    $scope.IsExistInArray = function (collection, key) {
        var keys = [];
        status = true;
        angular.forEach(collection, function (item) {
            if (keys.indexOf(key) === -1) {
                keys.push(key);
            } else {
                status = false;
            }
        });
        return status;
    };
    //============================ Birth Information ====================
    $scope.ViewBirthInformation = true;
    $scope.ViewVisaDetails = true;
    $scope.ViewLanguageKnown = true;

    $scope.BirthInformationToggle = function () {
        if ($scope.ViewBirthInformation) {
            $scope.ViewBirthInformation = false;
            $scope.birthInformation = angular.copy($scope.Provider.BirthInformation);
        } else {
            $scope.ViewBirthInformation = true;
        }
    };
    $scope.VisaDetailsToggle = function () {
        if ($scope.ViewVisaDetails) {
            $scope.ViewVisaDetails = false;
            $scope.visaDetail = angular.copy($scope.Provider.VisaDetails);
        } else {
            $scope.ViewVisaDetails = true;
        }
    };
    $scope.LanguageKnownToggle = function () {
        if ($scope.ViewLanguageKnown) {
            $scope.ViewLanguageKnown = false;
            $scope.getLanguagesAndTempData();
        } else {
            $scope.ViewLanguageKnown = true;
        }
    };
    $scope.getLanguagesAndTempData = function () {
        $scope.TempLanguageForEdit = angular.copy($scope.Provider.LanguageInfo);
        $scope.tempLanguages = angular.copy($scope.Languages);

        for (var i in $scope.TempLanguageForEdit.KnownLanguages) {
            for (var j in $scope.tempLanguages) {
                if ($scope.TempLanguageForEdit.KnownLanguages[i].Language == $scope.tempLanguages[j].name) {
                    $scope.tempLanguages.splice(j, 1);
                }
            }
        }
        $scope.maxIdOfLanguageInDB = $scope.TempLanguageForEdit.KnownLanguages.sort(function (a, b) { return a.ProficiencyIndex - b.ProficiencyIndex })[$scope.TempLanguageForEdit.KnownLanguages.length - 1].ProficiencyIndex;
        $scope.removeLinkShowId = angular.copy($scope.maxIdOfLanguageInDB);
    };
     
    //==================================== Country Code and Country List ========================
    //-------------- country data comes from CountryList.js and countryDialCodes.js---------------
    $scope.Countries = Countries;
    $scope.States = $scope.Countries[1].States;
    $scope.CountryDialCodes = countryDailCodes;
    //---------------------- get states ---------------------
    $scope.getStates = function (countryCode) {
        $scope.States = countryDropDownService.getStates($scope.Countries, countryCode);
        $scope.Counties = [];
        $scope.Cities = [];
        $scope.birthInformation.StateOfBirth = $scope.putempty;
        $scope.birthInformation.CountyOfBirth = $scope.putempty;
        $scope.birthInformation.CityOfBirth = $scope.putempty;
        //resetStateSelectTwoStyle();
    }
    $scope.getCounties = function (state) {
        $scope.Counties = countryDropDownService.getCounties($scope.States, state)
        $scope.Cities = [];
        $scope.birthInformation.CountyOfBirth = $scope.putempty;
        $scope.birthInformation.CityOfBirth = $scope.putempty;
    }
    $scope.getCities = function (county) {
        $scope.Cities = countryDropDownService.getCities($scope.Counties, county);
        $scope.birthInformation.CityOfBirth = $scope.putempty;
    }
    $scope.CountryOfissue = CountryOfissue;

    //============================================ Save Birth Information Function ======================================
    $scope.IsBirthInformationHasError = false;
    $scope.saveEthnicityBirthInformation = function (Form_Id) {
        if ($("#" + Form_Id).valid()) {
            $.ajax({
                url: '/Profile/Demographic/SaveBirthInformation',
                type: 'POST',
                data: new FormData($("#" + Form_Id)[0]),
                async: false,
                success: function (data) {
                    console.log(data);
                    if (data.status == "True") {
                        $scope.Provider.BirthInformation = angular.copy($scope.birthInformation);
                        $scope.birthInformation = {};
                        $scope.ViewBirthInformation = true;
                        $scope.IsBirthInformationHasError = false;
                    } else {
                        $scope.IsBirthInformationHasError = true;
                        $scope.BirthInformationErrorList = data.status.split(",");
                    }
                },
                cache: false,
                contentType: false,
                processData: false
            });
        } else {
            console.log("Sorry Some thing Wrong with you !!!!!!!!!!!");
        }
    };
    //========================= Save Visa Details Function =====================
    $scope.IsVisaDetailsHasError = false;
    $scope.saveEthnicityVisaDetails = function (Form_Id) {
        if ($("#" + Form_Id).valid()) {
            $.ajax({
                url: '/Profile/Demographic/SaveEthnicityVisaDetail',
                type: 'POST',
                data: new FormData($("#" + Form_Id)[0]),
                async: false,
                success: function (data) {
                    console.log(data);
                    if (data.status == "True") {
                        $scope.Provider.VisaDetails = angular.copy($scope.visaDetail);
                        $scope.visaDetail = {};
                        $scope.ViewVisaDetails = true;
                        $scope.IsVisaDetailsHasError = false;
                    } else {
                        $scope.IsVisaDetailsHasError = true;
                        $scope.VisaDetailsErrorList = data.status.split(",");
                    }
                },
                cache: false,
                contentType: false,
                processData: false
            });
        } else {
            console.log("Sorry Some thing Wrong with you !!!!!!!!!!!");
        }
    };
    $scope.IsLanguageInfoHasError = false;
    $scope.saveEthnicityLanguages = function (languageInfo) {
        $http.post("/Profile/Demographic/SaveEthnicityLanguages", $scope.TempLanguageForEdit).success(function (data, status, headers, config) {
            console.log(data);
            if (data.status == "True") {
                $scope.Provider.LanguageInfo = angular.copy($scope.TempLanguageForEdit);
                $scope.TempLanguageForEdit = [];
                $scope.ViewLanguageKnown = true;
                $scope.IsLanguageInfoHasError = false;
            } else {
                $scope.IsLanguageInfoHasError = true;
                $scope.LanguageInfoErrorList = data.status.split(",");
            }
          }).error(function (data, status, headers, config) {
              console.log("Sorry Some thing Wrong with you !!!!!!!!!!!");
          });
    };
});

//=========================== Close all auto-search result data on click out side ===================
$(document).click(function (event) {
    if (!$(event.target).hasClass("form-control")
        && $(event.target).parents(".LanguageSelectAutoList").length === 0) {
        $(".LanguageSelectAutoList").hide();
    }
})