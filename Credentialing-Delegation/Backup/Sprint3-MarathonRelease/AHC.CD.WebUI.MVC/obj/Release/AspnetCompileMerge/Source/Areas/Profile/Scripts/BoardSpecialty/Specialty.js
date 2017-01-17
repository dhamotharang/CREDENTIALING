
//=========================== Controller declaration ==========================
profileApp.controller('SpecialtyController', function ($scope, $rootScope, $http, dynamicFormGenerateService, masterDataService) {
    
    //-----------------------------------------------------Master Data Specialties------------------------------------------------------------
    
    //------------------------------------Initializing variables to for display and fetch from database---------------------------------------
    $scope.Specialties = [];
    $scope.practiceInterest = {};

    $scope.masterSpecialties = [];
    $scope.masterSpecialtyBoards = [];
    
    // rootScoped on emitted value catches the value for the model and insert to get the old data for Specialty Details
    $rootScope.$on('SpecialtyDetails', function (event, val) {
        $scope.Specialties = val;
        for (var i = 0; i < $scope.Specialties.length ; i++) {
            if ($scope.Specialties[i].SpecialtyBoardCertifiedDetail != null) {
                $scope.Specialties[i].SpecialtyBoardCertifiedDetail.ExpirationDate = ConvertDateFormat($scope.Specialties[i].SpecialtyBoardCertifiedDetail.ExpirationDate);
                $scope.Specialties[i].SpecialtyBoardCertifiedDetail.InitialCertificationDate = ConvertDateFormat($scope.Specialties[i].SpecialtyBoardCertifiedDetail.InitialCertificationDate);
                $scope.Specialties[i].SpecialtyBoardCertifiedDetail.LastReCerificationDate = ConvertDateFormat($scope.Specialties[i].SpecialtyBoardCertifiedDetail.LastReCerificationDate);
            }
            if ($scope.Specialties[i].SpecialtyBoardNotCertifiedDetail != null) {
                $scope.Specialties[i].SpecialtyBoardNotCertifiedDetail.ExamDate = ConvertDateFormat($scope.Specialties[i].SpecialtyBoardNotCertifiedDetail.ExamDate);
            }
        }
    });

    // rootScoped on emitted value catches the value for the model and insert to get the old data for Practice Interest
    $rootScope.$on('PracticeInterest', function (event, val) {
        $scope.practiceInterest = val;
    });

    //--------------------------------------Fetching Master Data for Specialties and Specialty Boards-------------------------------------------------
    masterDataService.getMasterData("/Profile/MasterData/getAllSpecialities").then(function (masterSpecialties) {
        $scope.masterSpecialties = masterSpecialties;
    });

    masterDataService.getMasterData("/Profile/MasterData/getAllspecialtyBoards").then(function (masterSpecialtyBoards) {
        $scope.masterSpecialtyBoards = masterSpecialtyBoards;
    });

    //=============== Specialty Conditions ==================
    $scope.submitButtonText = "Add";
    $scope.IndexValue = 0;

    //====================== Specialty ===================
      
    //To save and add a new Specialty Detail to the list of Specialties.
    $scope.saveSpecialty = function (specialty) {
        var validationStatus;
        var url;
        var $formData;
        var tempSpecialtyID;
        var tempSpecialty;
        var tempSpecialtyBoardID;
        var tempSpecialtyBoard;

        //Add Details - Denote the URL
        $formData = $('#newShowSpecialtyDiv').find('form');
        url = "/Profile/BoardSpecialty/AddSpecialityDetailAsync?profileId=" + profileId;
        $scope.typeOfSaveForSpecialty = "Add";

        //To map specialty from the parameter with the Master Data object using ID
        tempSpecialtyID = specialty.SpecialtyID;
        for (var spl in $scope.masterSpecialties) {
            if ($scope.masterSpecialties[spl].SpecialtyID == tempSpecialtyID) {
                tempSpecialty = $scope.masterSpecialties[spl];
                break;
            }
        }
        console.log(specialty);
        //To map Specialty Boards from the parameter object with the Master Data using ID, on the condition that it is Board Certified.
        if (specialty.hasOwnProperty("SpecialtyBoardCertifiedDetail")) {
            tempSpecialtyBoardID = specialty.SpecialtyBoardCertifiedDetail.SpecialtyBoardID;
            for (var spl in $scope.masterSpecialtyBoards) {
                if ($scope.masterSpecialtyBoards[spl].SpecialtyBoardID == tempSpecialtyBoardID) {
                    tempSpecialtyBoard = $scope.masterSpecialtyBoards[spl];
                    break;
                }
            }
        }

        //For Client side validation in the Edit Div
        ResetFormForValidation($formData);
        validationStatus = $formData.valid();

        if (validationStatus) {
            //Simple POST request example (passing data) :
            console.log($formData);
            $.ajax({
                url: url,
                type: 'POST',
                data: new FormData($formData[0]),
                async: false,
                cache: false,
                contentType: false,
                processData: false,
                success: function (data) {
                    if (data.status == "true") {
                        //To map Specialties and Specialty Boards and the dates across the view.
                        data.specialty.Specialty = tempSpecialty;
                        if (data.specialty.SpecialtyBoardCertifiedDetail != null) {
                            data.specialty.SpecialtyBoardCertifiedDetail.SpecialtyBoard = tempSpecialtyBoard;
                            data.specialty.SpecialtyBoardCertifiedDetail.ExpirationDate = ConvertDateFormat(data.specialty.SpecialtyBoardCertifiedDetail.ExpirationDate);
                            data.specialty.SpecialtyBoardCertifiedDetail.InitialCertificationDate = ConvertDateFormat(data.specialty.SpecialtyBoardCertifiedDetail.InitialCertificationDate);
                            data.specialty.SpecialtyBoardCertifiedDetail.LastReCerificationDate = ConvertDateFormat(data.specialty.SpecialtyBoardCertifiedDetail.LastReCerificationDate);
                        }
                        if (data.specialty.SpecialtyBoardNotCertifiedDetail != null) {
                            data.specialty.SpecialtyBoardNotCertifiedDetail.ExamDate = ConvertDateFormat(data.specialty.SpecialtyBoardNotCertifiedDetail.ExamDate);
                        }
                        $scope.Specialties.push(data.specialty);
                        $scope.ErrorInSpecialtyDetails = false;
                        $rootScope.visibilityControl = "addedNewBoardSpecialty";
                    } else {
                        $scope.ErrorInSpecialtyDetails = true;
                        $scope.SpeciatyDetailsErrorList = data.status.split(",");
                    }
                },
                error: function (e) {
                    $scope.SpeciatyDetailsErrorList = "Sorry for Inconvenience !!!! Please Try Again Later...";
                }
            });
        }
    };

    //To save and update an already existing Specialty Detail.
    $scope.updateSpecialty = function (specialty, IndexValue) {
        var validationStatus;
        var url;
        var $formData;
        var tempSpecialtyID;
        var tempSpecialty;
        var tempSpecialtyBoardID;
        var tempSpecialtyBoard;

        //Update Details - Denote the URL
        $formData = $('#specialtyEditDiv' + IndexValue).find('form');
        url = "/Profile/BoardSpecialty/UpdateSpecialityDetailAsync?profileId=" + profileId;
        $scope.typeOfSaveForSpecialty = "Edit";
        
        //To map specialty from the parameter object with the Master Data using ID
        tempSpecialtyID = specialty.SpecialtyID;
        for (var spl in $scope.masterSpecialties) {
            if ($scope.masterSpecialties[spl].SpecialtyID == tempSpecialtyID) {
                tempSpecialty = $scope.masterSpecialties[spl];
                break;
            }
        }

        //To map Specialty Boards from the parameter object with the Master Data using ID, on the condition that it is Board Certified.
        if (specialty.SpecialtyBoardCertifiedDetail != null) {
            tempSpecialtyBoardID = specialty.SpecialtyBoardCertifiedDetail.SpecialtyBoardID;
            for (var spl in $scope.masterSpecialtyBoards) {
                if ($scope.masterSpecialtyBoards[spl].SpecialtyBoardID == tempSpecialtyBoardID) {
                    tempSpecialtyBoard = $scope.masterSpecialtyBoards[spl];
                    break;
                }
            }
        }

        //For Client side validation in the Edit Div
        ResetFormForValidation($formData);
        validationStatus = $formData.valid();

        if (validationStatus) {
            //Simple POST request example (passing data) :
            console.log($formData);
            $.ajax({
                url: url,
                type: 'POST',
                data: new FormData($formData[0]),
                async: false,
                cache: false,
                contentType: false,
                processData: false,
                success: function (data) {
                    if (data.status == "true") {
                        data.specialty.Specialty = tempSpecialty;
                        if (data.specialty.SpecialtyBoardCertifiedDetail != null) {
                            data.specialty.SpecialtyBoardCertifiedDetail.SpecialtyBoard = tempSpecialtyBoard;
                            data.specialty.SpecialtyBoardCertifiedDetail.ExpirationDate = ConvertDateFormat(data.specialty.SpecialtyBoardCertifiedDetail.ExpirationDate);
                            data.specialty.SpecialtyBoardCertifiedDetail.InitialCertificationDate = ConvertDateFormat(data.specialty.SpecialtyBoardCertifiedDetail.InitialCertificationDate);
                            data.specialty.SpecialtyBoardCertifiedDetail.LastReCerificationDate = ConvertDateFormat(data.specialty.SpecialtyBoardCertifiedDetail.LastReCerificationDate);
                        }
                        if (data.specialty.SpecialtyBoardNotCertifiedDetail != null) {
                            data.specialty.SpecialtyBoardNotCertifiedDetail.ExamDate = ConvertDateFormat(data.specialty.SpecialtyBoardNotCertifiedDetail.ExamDate);
                        }
                        $scope.Specialties[IndexValue] = data.specialty;
                        $scope.ErrorInSpecialtyDetails = false;
                        $rootScope.visibilityControl = "updatedBoardSpecialty";
                    } else {
                        $scope.ErrorInSpecialtyDetails = true;
                        $scope.SpeciatyDetailsErrorList = data.status.split(",");
                    }
                },
                error: function (e) {
                    $scope.SpeciatyDetailsErrorList = "Sorry for Inconvenience !!!! Please Try Again Later...";
                }
            });
        }
    };

    //To save, add and update a Practice Interest.
    $scope.savePracticeInterest = function (practiceInterest) {
        var validationStatus;
        var url;
        var $formData;

        if (practiceInterest.PracticeInterestID == null) {
            $scope.typeOfSaveForPracticeInterest = "Add";
        } else {
            $scope.typeOfSaveForPracticeInterest = "Edit";
        }

        if ($scope.visibilityControl == ('editpracticeInterest')) {
            //Update Details - Denote the URL
            $formData = $('#editShowPracticeInterest').find('form');
            url = "/Profile/BoardSpecialty/UpdatePracticeInterestAsync?profileId=" + profileId;
        }

        ResetFormForValidation($formData);
        validationStatus = $formData.valid();

        if (validationStatus) {
            //Simple POST request example (passing data) :
            $.ajax({
                url: url,
                type: 'POST',
                data: new FormData($formData[0]),
                async: false,
                cache: false,
                contentType: false,
                processData: false,
                success: function (data) {
                    if (data.status == "true") {
                        $scope.practiceInterest = data.practiceInterest;
                        $rootScope.visibilityControl = "";
                        $scope.ErrorInPracticeInterest = false;
                        if($scope.typeOfSaveForPracticeInterest == "Add") {
                            $rootScope.visibilityControl = "addedNewPracticeInterest";
                        } else {
                            $rootScope.visibilityControl = "updatedPracticeInterest";
                        }
                    } else {
                        $scope.ErrorInPracticeInterest = true;
                        $scope.PracticeInterestErrorList = data.status.split(",");
                    }
                },
                error: function (e) {
                    $scope.PracticeInterestErrorList = "Sorry for Inconvenience !!!! Please Try Again Later...";
                }
            });
            $scope.changePartial = false;
        }
    };
});

$(document).ready(function () {
    $(".selectSpecialty").select2({
        placeholder: "Select a Specialty"
    });

    $(".selectSpecialtyBoard").select2({
        placeholder: "Select a Specialty Board"
    });
});