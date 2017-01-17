
//=========================== Controller declaration ==========================
profileApp.controller('ProfessionalReference', function ($scope, $http, dynamicFormGenerateService) {

    $scope.ProfessionalReferences = [{

        ProviderType: "MD",
        ProviderTypeVal: "4",
        FirstName: "Dalton",
        MiddleName: "M",
        LastName: "Benson",
        UnitNumber: "11479",
        Street: "Cortez Boulevard",
        Building: "111",
        Country: "US",
        County: "",
        Telephone: "352-597-3511",
        State: "Florida",
        City: "Brooksville",
        Zipcode: "34613-_ _ _ _",
        Fax: "727-869-8814",
        Email: "",
        IsBoardCerified:"",
        Degree:"",
        Speciality: "",
        Relationship:""
    },
    {

        ProviderType:"MD",
        FirstName: "Alfred",
        MiddleName: "",
        LastName: "Alingu",
        UnitNumber: "10045",
        Street: "Cortez Boulevard",
        Building: "122",
        Country: "US",
        County: "",
        Telephone: "352-596-0405",
        State: "Florida",
        City: "Brooksville",
        Zipcode: "34613-_ _ _ _",
        Fax: "",
        Email: "",
        IsBoardCerified:"",
        Degree:"",
        Speciality: "",
        Relationship:""
    },
      {

          ProviderType: "MD",
          FirstName: "Jude",
          MiddleName: "Antoine",
          LastName: "Pierre",
          UnitNumber: "5290",
          Street: "Applegate Drive",
          Building: "",
          Country: "US",
          County: "",
          Telephone: "352-686-3101",
          State: "Florida",
          City: "Spring Hill",
          Zipcode: "34606-_ _ _ _",
          Fax: "352-688-8713",
          Email: "",
          IsBoardCerified: "",
          Degree: "",
          Speciality: "",
          Relationship: ""
      }];

    
    //=============== Professional Reference Conditions ==================
    $scope.editShowProfessionalReference = false;
    $scope.newShowProfessionalReference = false;
    $scope.submitButtonText = "Add";
    $scope.IndexValue = 0;

    //====================== Professional Reference ===================

    $scope.addProfessionalReference = function () {
       
        $scope.newShowProfessionalReference = true;
        $scope.submitButtonText = "Add";
        $scope.professionalReference = {};
        ResetProfessionalReferenceForm();
    };

    $scope.editProfessionalReference = function (index, professionalReference) {
        $scope.showViewProfessionalReference = false;
        $scope.newShowProfessionalReference  = false;
        $scope.editShowProfessionalReference  = true;
        $scope.submitButtonText = "Update";
        $scope.professionalReference  = professionalReference;
        $scope.IndexValue = index;
    };

    $scope.cancelProfessionalReference  = function (condition) {
        ProfessionalReferenceCancelParameters();
    };

    //******************************function for view*****************************
    $scope.showProfessionalReference = function (index, professionalReference) {
        $scope.newShowProfessionalReference = false;
        $scope.showViewProfessionalReference = true;
        $scope.editShowProfessionalReference = false;
        $scope.professionalReference = professionalReference;
        $scope.IndexValue = index;
    }


    $scope.saveProfessionalReference = function (professionalReference) {
 
        var validationStatus;
        var url;


        if ($scope.newShowProfessionalReference) {
            //Add Details - Denote the URL
            validationStatus = $('#newProfessionalReferenceFormDiv').find('form').valid();
            url = "/Profile/ProfessionalReference/AddProfessionalReference?profileId=1";
        }
        else if ($scope.editShowProfessionalReference) {
            //Update Details - Denote the URL
            validationStatus = $('#professionalReferenceEditDiv' + $scope.IndexValue).find('form').valid();
            url = "/Profile/ProfessionalReference/UpdateProfessionalReference?profileId=1";
        }

        if (validationStatus) {
            // Simple POST request example (passing data) :
            $http.post(url, professionalReference).
              success(function (data, status, headers, config) {

                  alert("Success");
                  if ($scope.newShowProfessionalReference) {
                      //Add Details - Denote the URL
                      professionalReference.ProfessionalRefereneInfoID = data;
                      $scope.ProfessionalReferences.push(professionalReference);
                  }
                  ProfessionalReferenceCancelParameters();
              }).
              error(function (data, status, headers, config) {
                  alert("Error");
              });
        }

    };

    function ProfessionalReferenceCancelParameters() {
        $scope.showViewProfessionalReference = false;
        $scope.editShowProfessionalReference = false;
        $scope.newShowProfessionalReference = false;
        $scope.professionalReference = {};
        $scope.IndexValue = 0;
    }

    function ResetProfessionalReferenceForm() {
        $('#newShowProfessionalReferenceDiv').find('.professionalReferenceForm')[0].reset();
        $('#newShowProfessionalReferenceDiv').find('span').html('');
    }
});

