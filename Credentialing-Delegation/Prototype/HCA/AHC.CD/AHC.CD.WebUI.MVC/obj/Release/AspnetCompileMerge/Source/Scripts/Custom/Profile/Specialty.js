
//=========================== Controller declaration ==========================
profileApp.controller('SpecialtyController', function ($scope, $http, dynamicFormGenerateService) {
    
    $scope.Specialties = [
        {
            SpecialtyPreference: "Primary",
            Specialty: "Internal Medicine",
            PercentageOfTime: "Not Available.",
            ListedInHMO: "Yes",
            ListedInPPO: "Yes",
            ListedInPOS: "Yes",
            CertificatePath: "/Content/Document/DocPreview.pdf",
            SpecialityBoardDetail: {
                IsBoardCertified: "Yes",
                SpecialityBoardCetifiedDetail: {
                    SpecialityBoard: "American Board of Internal Medicine",
                    InitialCertificationDate: new Date(1996,10,01),
                    LastReCredentialingDate: new Date(2006,11,31),
                    ExpirationDate: new Date(2016,11,31),
                    BoardCertificatePath: "/Content/Document/SINGH - BOARD CERT.pdf",
                },
                SpecialityBoardNotCertifiedDetail: {
                    ExamStatus: "",
                    ExamDate: "",
                    ReasonForNotTakingExam: "",
                },
            },
        }];
    
    $scope.practiceInterest = {PracticeInterest: ""};

    $scope.splNames = ['Internal Medicine', 'Dentist', 'Surgeon', 'Dermatologist'];

    //=============== Specialty Conditions ==================
    $scope.editShowSpecialty = false;
    $scope.newShowSpecialty = false;
    $scope.viewShowSpecialty = false;
    $scope.submitButtonText = "Add";
    $scope.boardCertifiedForYes = true;
    $scope.boardCertifiedForNo = false;
    $scope.doNotIntendToSit = false;
    $scope.doIntendToSit = false;
    $scope.IndexValue = 0;

    //====================== Specialty ===================

    $scope.addSpecialty = function () {
        $scope.viewShowSpecialty = false;
        $scope.newShowSpecialty = true;
        $scope.submitButtonText = "Add";
        $scope.specialty = {};
        ResetSpecialtyForm();
    };

    $scope.viewSpecialty = function (index, specialty) {
        $scope.editShowSpecialty = false;
        $scope.newShowSpecialty = false;
        $scope.viewShowSpecialty = true;
        $scope.specialty = specialty;
        $scope.IndexValue = index;
    };

    $scope.editSpecialty = function (index, specialty) {
        $scope.viewShowSpecialty = false;
        $scope.newShowSpecialty = false;
        $scope.editShowSpecialty = true;
        $scope.submitButtonText = "Update";
        $scope.specialty = specialty;
        $scope.IndexValue = index;
    };

    $scope.cancelSpecialty = function (condition) {
        setSpecialtyCancelParameters();
    };

    $scope.saveSpecialty = function (specialty) {

        console.log(specialty);

        var validationStatus;
        var url;

        if ($scope.newShowSpecialty) {
            //Add Details - Denote the URL
            validationStatus = $('#newShowSpecialtyDiv').find('form').valid();
            //url = "/Profile/Specialty/AddSpecialty?profileId=" + profileId;
        }
        else if ($scope.editShowSpecialty) {
            //Update Details - Denote the URL
            validationStatus = $('#specialtyEditDiv' + $scope.IndexValue).find('form').valid();
            //url = "/Profile/Specialty/UpdateSpecialty?profileId=" + profileId;
        }

        console.log(specialty);

        if (validationStatus) {
            // Simple POST request example (passing data) :
            $http.post(url, specialty).
              success(function (data, status, headers, config) {

                  alert("Success");
                  if ($scope.newShowSpecialty) {
                      //Add Details - Denote the URL
                      specialty.SpecialtyInfoID = data;
                      $scope.Specialties.push(specialty);
                  }
                  setSpecialtyCancelParameters();
              }).
              error(function (data, status, headers, config) {
                  alert("Error");
              });
        }
    };

    function setSpecialtyCancelParameters() {
        $scope.viewShowSpecialty = false;
        $scope.editShowSpecialty = false;
        $scope.newShowSpecialty = false;
        $scope.boardCertifiedForNo = false;
        $scope.boardCertifiedForYes = false;
        $scope.specialty = {};
        $scope.IndexValue = 0;
    }

    function ResetSpecialtyForm() {
        $('#newShowSpecialtyDiv').find('.specialtyForm')[0].reset();
        $('#newShowSpecialtyDiv').find('span').html('');
    }

    $scope.savePracticeInterest = function (practiceInterest) {
        $scope.changePartial = false;
    }

    $scope.showViewPartial = function () {
        $scope.changePartial = false;
    }

    $scope.showEditPartial = function () {
        $scope.changePartial = true;
    }

    $scope.boardCertified = function (value) {
        if (value == "Yes") {
            this.boardCertifiedForYes = true;
            this.boardCertifiedForNo = false;
            this.doNotIntendToSit = false;
            this.doIntendToSit = false;
        } else if (value == "No") {
            this.boardCertifiedForNo = true;
            this.boardCertifiedForYes = false;
        }        
    }

    $scope.intentionOfSitting = function (value) {
        if (value == "I have taken exam, result pending") {
            this.doNotIntendToSit = false;
            this.doIntendToSit = false;
        } else if (value == "I intend to sit for exam") {
            this.doIntendToSit = true;
            this.doNotIntendToSit = false;
        } else if (value == "I do not intend to take exam") {
            this.doNotIntendToSit = true;
            this.doIntendToSit = false;
        }
    }

    $scope.intendToSit = function () {
        this.doIntendToSit = true;
        this.doNotIntendToSit = false;
    }

    $scope.intendNotToSit = function () {
        this.doNotIntendToSit = true;
        this.doIntendToSit = false;
    }

    $scope.resetIntendToSit = function () {
        this.doNotIntendToSit = false;
        this.doIntendToSit = false;
    }

});

$(document).ready(function () {
    $(".selectSpecialty").select2({
        placeholder: "Select a Specialty"
    });
});