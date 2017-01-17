
//=========================== Controller declaration ==========================
profileApp.controller('ProfessionalReference', function ($scope, $rootScope, $http, masterDataService) {

    $scope.ProfessionalReferences = [

        ////ProviderType:"Dr",
        ////FirstName: "William",
        ////MiddleName: "",
        ////LastName: "Harvey",
        ////UnitNumber: "701",
        ////Street: "West Plymouth Avenue",
        ////Building: "Suite",
        ////Country: "US",
        ////County: "Highlands, FL",
        ////Telephone: "429-560-6677",
        ////State: "Florida",
        ////City: "Bostwick",
        ////Zipcode: "2846",
        ////Fax: "1-212-9876543",
        ////Email: "bencarson@gmail.com",
        ////IsBoardCerified:"No",
        ////Degree:"Associate Degree",
        ////Specialty: "Radiation Oncology",
        ////Relationship:""
    ];

    //calling the method using $on(PSP-public subscriber pattern)
    $rootScope.$on('ProfessionalReferenceInfos', function (event, val) {
        $scope.ProfessionalReferences = val;
      
    });

    $scope.submitButtonText = "Add";

    ////******************************Master Data*****************************
    $scope.masterSpecialties = [];
    $scope.masterDegrees = [];



    masterDataService.getMasterData("/Profile/MasterData/getAllSpecialities").then(function (masterSpecialties) {
        $scope.masterSpecialties = masterSpecialties;
    });



    $scope.masterProviderTypes = [];

    masterDataService.getMasterData("/Profile/MasterData/GetAllProviderTypes").then(function (masterProviderTypes) {
        $scope.masterProviderTypes = masterProviderTypes;
    });

    masterDataService.getMasterData("/Profile/MasterData/GetAllQualificationDegrees").then(function (masterDegrees) {
        $scope.masterDegrees = masterDegrees;
    });

    $scope.saveProfessionalReference = function (professionalReference,index) {
 
        var validationStatus;
        var url;
        var formData1;
        //var providerTypeId;
        //var providerType;
        var tempSpecialtyID;
        var tempSpecialty;

        tempSpecialtyID = professionalReference.SpecialtyID;
        for (var spl in $scope.masterSpecialties) {
            if ($scope.masterSpecialties[spl].SpecialtyID == tempSpecialtyID) {
                tempSpecialty = $scope.masterSpecialties[spl];
                break;
            }
        }

        //providerTypeId = professionalReference.ProviderTypeID;

        //for (var provider in $scope.masterProviderTypes) {
        //    if ($scope.masterProviderTypes[provider].ProviderTypeID == providerTypeId) {
        //        providerType = $scope.masterProviderTypes[provider];
        //        break;
        //    }
        //}


        if ($scope.visibilityControl == 'addpr') {
            //Add Details - Denote the URL
            validationStatus = $('#newProfessionalReferenceFormDiv').find('form').valid();

            formData1 = $('#newProfessionalReferenceFormDiv').find('form');

            url = "/Profile/ProfessionalReference/AddProfessionalReference?profileId=" + profileId;
        }
        else if ($scope.visibilityControl == (index + '_editpr')) {
            //Update Details - Denote the URL
            validationStatus = $('#professionalReferenceEditDiv' + index).find('form').valid();
            formData1 = $('#professionalReferenceEditDiv' + index).find('form');
            console.log(formData1);
            url = "/Profile/ProfessionalReference/UpdateProfessionalReference?profileId=" + profileId;
        }

        ResetFormForValidation(formData1);

        if (validationStatus) {
            // Simple POST request example (passing data) :
            $.ajax({
                url: url,
                type: 'POST',
                data: new FormData(formData1[0]),
                async: false,
                cache: false,
                contentType: false,
                processData: false,
                success: function (data) {
                  
                    if (data.status == "true") {
                        data.professionalReference.Specialty = tempSpecialty;
                        //data.professionalReference.ProviderType = providerType;
                        if ($scope.visibilityControl != (index + '_editpr')) {
                            $scope.ProfessionalReferences.push(data.professionalReference);
                            $rootScope.visibilityControl = "addedNewProfessionalReference"
                        }
                        else {
                            $scope.ProfessionalReferences[index] = data.professionalReference;
                            console.log($scope.ProfessionalReferences);
                            $rootScope.visibilityControl = "updatedProfessionalReference"
                        }

                        $scope.IsProfessionalReferenceHasError = false;
                        FormReset(formData1);
                        $rootScope.visibilityControl = "";
                      
                    } else {
                        $scope.IsProfessionalReferenceHasError = true;
                        $scope.ProfessionalReferenceErrorList = data.status.split(",");
                    }
                }
            });
        }

    };

  

    function ResetProfessionalReferenceForm() {
        $('#newShowProfessionalReferenceDiv').find('.professionalReferenceForm')[0].reset();
        $('#newShowProfessionalReferenceDiv').find('span').html('');
    }
});

