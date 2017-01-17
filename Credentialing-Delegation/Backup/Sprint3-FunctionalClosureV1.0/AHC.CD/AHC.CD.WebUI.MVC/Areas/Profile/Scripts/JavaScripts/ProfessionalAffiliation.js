
profileApp.controller('professionalAppCtrl', ['$scope', '$rootScope', '$http', 'dynamicFormGenerateService', 'masterDataService', function ($scope,$rootScope,$http, dynamicFormGenerateService,masterDataService) {

 

    $scope.ProfessionalAffiliations = [];
   
    //==================Master Data=========================



    //=============== Professional Affiliation Conditions ==================
   
    $scope.submitButtonText = "Add";

    
    //calling the method using $on(PSP-public subscriber pattern)
    $rootScope.$on('ProfessionalAffiliationInfos', function (event, val) {

        $scope.ProfessionalAffiliations = val;
        //console.log("king is here");
        //console.log(val);

        for (var i = 0; i < $scope.ProfessionalAffiliations.length ; i++) {
       
            $scope.ProfessionalAffiliations[i].StartDate = ConvertDateFormat($scope.ProfessionalAffiliations[i].StartDate);
            $scope.ProfessionalAffiliations[i].EndDate = ConvertDateFormat($scope.ProfessionalAffiliations[i].EndDate);
                
            
      }
        
    });
    

    $scope.saveProfessionalAffiliation = function (professionalAffiliation, index) {

   

        var validationStatus;
        var url;
        var formData1;

        if ($scope.visibilityControl == 'addpa') {
            //Add Details - Denote the URL
                  formData1 = $('#newShowProfessionalAffiliationDiv').find('form');
         url = "/Profile/ProfessionalAffiliation/AddProfessionalAffiliation?profileId=" + profileId;
        }
        else if ($scope.visibilityControl == (index + '_editpa')) {
            //Update Details - Denote the URL
            formData1 = $('#professionalAffiliationEditDiv' + index).find('form');
            url = "/Profile/ProfessionalAffiliation/UpdateProfessionalAffiliation?profileId=" + profileId;
        }

        ResetFormForValidation(formData1);
        validationStatus = formData1.valid()
   

        if (validationStatus) {
            
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
                        data.professionalAffiliation.StartDate = ConvertDateFormat(data.professionalAffiliation.StartDate);
                        data.professionalAffiliation.EndDate = ConvertDateFormat(data.professionalAffiliation.EndDate);
                       
                        if ($scope.visibilityControl !=(index + '_editpa')) {
                            $scope.ProfessionalAffiliations.push(data.professionalAffiliation);
                            $rootScope.visibilityControl = "addedNewProfessionalAffiliation"
                        }
                        else {
                            $scope.ProfessionalAffiliations[index] = data.professionalAffiliation;
                            $rootScope.visibilityControl = "updatedProfessionalAffiliation"
                        }

                        $scope.IsProfessionalAffiliationHasError = false;
                        FormReset(formData1);
                      
                     
                    } else {
                        $scope.IsProfessionalAffiliationHasError = true;
                        $scope.ProfessionalAffiliationErrorList = data.status.split(",");
                    }
                }
            });

        }
    };

   

    function ResetProfessionalAffiliationForm() {
        $('#newShowProfessionalAffiliationDiv').find('.professionalAffiliationForm')[0].reset();
        $('#newShowProfessionalAffiliationDiv').find('span').html('');
    }
}]);

$(document).ready(function () {
});