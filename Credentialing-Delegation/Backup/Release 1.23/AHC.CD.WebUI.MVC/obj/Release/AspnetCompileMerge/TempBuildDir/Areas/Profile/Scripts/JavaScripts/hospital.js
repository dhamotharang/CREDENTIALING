
profileApp.controller('hospitalcltr', ['$scope', '$rootScope', '$http', 'messageAlertEngine', function ($scope, $rootScope, $http, messageAlertEngine) {

    $rootScope.$on("LoadRequireMasterData", function () {        
        $http.get("/Profile/MasterData/GetAllHospitals").then(function (value) { $scope.hospitals = value.data; });
        $http.get("/Profile/MasterData/GetAllStaffCategories").then(function (value) { $scope.staffCategories = value.data; });
        $http.get("/Profile/MasterData/GetAllAdmittingPrivileges").then(function (value) { $scope.admittingPrivileges = value.data; });
        $http.get("/Profile/MasterData/GetAllHospitalContactPersons").then(function (value) { $scope.ContactPersons = value.data; });
    });

    $scope.HaveHospitalPrivilege = "True";

    $scope.HospitalPrivilegeInformations =[];    

    //to show renew div
    $scope.ShowRenewDiv=false;

    $scope.RenewDiv = function (hospitalPrivilegeInformation) {
        if (hospitalPrivilegeInformation.AffilicationStartDate == null || hospitalPrivilegeInformation.AffiliationEndDate == null || hospitalPrivilegeInformation.HospitalPrevilegeLetterPath == null)
        { $scope.ShowRenewDiv = false; }
        else
        {
            $scope.ShowRenewDiv = true;
        }
    };




    // rootScoped on emitted value catches the value for the model and insert to get the old data
    $rootScope.$on('HospitalPrivilegeInformation', function (event, val) {
        //console.log(val);
        try {
            $scope.HospitalPrivilegeInformations = val;
            
            if ($scope.HospitalPrivilegeInformations.HospitalPrivilegeDetails.length > 0) {
                for (var i = 0; i < $scope.HospitalPrivilegeInformations.HospitalPrivilegeDetails.length ; i++) {
                    $scope.HospitalPrivilegeInformations.HospitalPrivilegeDetails[i].AffilicationStartDate = $rootScope.ConvertDateFormat($scope.HospitalPrivilegeInformations.HospitalPrivilegeDetails[i].AffilicationStartDate);
                    $scope.HospitalPrivilegeInformations.HospitalPrivilegeDetails[i].AffiliationEndDate = $rootScope.ConvertDateFormat($scope.HospitalPrivilegeInformations.HospitalPrivilegeDetails[i].AffiliationEndDate);
                    if (!$scope.HospitalPrivilegeInformations.HospitalPrivilegeDetails[i].SpecialtyID) { $scope.HospitalPrivilegeInformations.HospitalPrivilegeDetails[i].SpecialtyID = ""; }
                }
            }
       
        }
        catch (e) {

        };
    });

    $scope.locations = '';

    $scope.filterData = function (valLocaton) {        
        try {            
            $scope.locations = $scope.hospitals.filter(function (hospitals) { return hospitals.HospitalID == valLocaton })[0].HospitalContactInfoes;
        }
        catch(e){}
    };


    $scope.clear = function () {
        $scope.tempObject.HospitalContactInfo.HospitalContactInfoID = "";
        $scope.tempObject.HospitalContactInfo.Street = "";
        $scope.tempObject.HospitalContactInfo.Building = "";
        $scope.tempObject.HospitalContactInfo.City = "";
        $scope.tempObject.HospitalContactInfo.State = "";
        $scope.tempObject.HospitalContactInfo.ZipCode = "";
        $scope.tempObject.HospitalContactInfo.Country = "";
        $scope.tempObject.HospitalContactInfo.County = "";
        $scope.tempObject.HospitalContactInfo.Phone = "";
        $scope.tempObject.HospitalContactInfo.Fax = "";
        $scope.tempObject.HospitalContactInfo.Email = "";
        $scope.tempObject.HospitalContactPerson.HospitalContactPersonID = "";
        $scope.tempObject.HospitalContactPerson.ContactPersonPhone = "";
        $scope.tempObject.HospitalContactPerson.ContactPersonFax = "";
    } 
    

    $scope.clear1 = function () {
        $scope.tempObject.HospitalContactPerson.HospitalContactPersonID = "";
        $scope.tempObject.HospitalContactPerson.ContactPersonPhone = "";
        $scope.tempObject.HospitalContactPerson.ContactPersonFax = "";
    }

    $scope.getAddress = function (valAddress) {
        try {

            $scope.LocationAddress = $scope.locations.filter(function (locations) {return locations.HospitalContactInfoID == valAddress})[0];
            $scope.tempObject.HospitalContactInfo = $scope.LocationAddress;            
            $scope.contactPersons = $scope.LocationAddress.HospitalContactPersons;
        }
        catch (e) { }
    };
    $scope.getContactPersonDetail = function (valCont) {
        try {        
        $scope.contactPersonDetail = $scope.contactPersons.filter(function (contactPersons) { return contactPersons.HospitalContactPersonID == valCont })[0];
        $scope.tempObject.HospitalContactPerson = $scope.contactPersonDetail;        
        }
        catch (e) { }
    };

    $scope.specialties = $rootScope.getSpecilityForThisUser();

    $scope.getSpecialityForProvider = function (findData) {
        try{
            return $rootScope.getSpecilityForThisUser().filter(function (specialties) { return specialties.Specialty.SpecialtyID == findData })[0];
        }
        catch (e) { }
    }

    $scope.getHospitalNameForProvider = function (findData) {
        try{
            return $scope.hospitals.filter(function (hospitals) { return hospitals.HospitalID == findData })[0];
        }
        catch (e) { }
    }

    $scope.getStaffCategoryForProvider = function (findData) {
        try{
            return $scope.staffCategories.filter(function (staffCategories) { return staffCategories.StaffCategoryID == findData })[0];
        }
        catch (e) { }
    }

    $scope.getHospitalLocationForProvider = function (valLocaton,findData) {
        try {
            $scope.locations = $scope.hospitals.filter(function (hospitals) { return hospitals.HospitalID == valLocaton })[0].HospitalContactInfoes;
            return $scope.locations.filter(function (locations) { return locations.HospitalContactInfoID == findData })[0];
        }
        catch (e) { }
    }

    $scope.getContactPersonForProvider = function (findData) {
        try{
            return $scope.ContactPersons.filter(function (ContactPersons) { return ContactPersons.HospitalContactPersonID == findData })[0];
        }
        catch (e) { }
    }

    $scope.getAdmittingPrivilageStatusForProvider = function (findData) {
        try{
            return $scope.admittingPrivileges.filter(function (admittingPrivileges) { return admittingPrivileges.AdmittingPrivilegeID == findData })[0];
        }
        catch (e) { }
    }

    //set only one as Primary
    $scope.setPrimary = function () {
        try {
            for (var i = 0; i < $scope.HospitalPrivilegeInformations.HospitalPrivilegeDetails.length ; i++) {
                $scope.HospitalPrivilegeInformations.HospitalPrivilegeDetails[i].PreferenceType = "2";
            }
        }
        catch (e) { }
    };

    //=============== Hospital Privilege Information Conditions ==================

    $scope.indexVal = "";

    $scope.visibilityControl = "";
    //Visibility of the div Control object to perform show and hide    

    $scope.tempObject = {};
    //Temp object to hold the form data so that the data gets revert once clicked cancel while add and edit


    //Controls the View and Add feature on the page
    $scope.operateViewAndAddControl = function (sectionValue) {
        $rootScope.closeAlertMessage();
        $scope.tempObject = {};
        $scope.buttonLabel = "Add"
        //console.log(sectionValue);
        $scope.visibilityControl = sectionValue;
        $('[data-toggle="tooltip"]').tooltip();
        try {
            $('.fileinput-exists').find('a').trigger('click');
        } catch (e) {

        }
    };

    //Controls the Edit feature on the page
    $scope.operateEditControl = function (sectionValue, obj) {
        $rootScope.closeAlertMessage();
        $scope.tempObject = {};        
        $scope.tempObject.selectedEduLocation = {};
        $scope.buttonLabel = "Update"
        //console.log(sectionValue);
        //console.log(obj);
        $scope.tempObject = angular.copy(obj);
        //console.log(angular.copy(obj));
        $scope.visibilityControl = sectionValue;
        $('[data-toggle="tooltip"]').tooltip();      
        try {
            $('.fileinput-exists').find('a').trigger('click');
        } catch (e) {

        }
    };
    //Controls the renew Feature
    $scope.operateRenewControl = function (sectionValue, obj) {
        $rootScope.closeAlertMessage();
        $scope.tempObject = {};
        $scope.tempObject.selectedEduLocation = {};
        $scope.buttonLabel = "Renew"
        //console.log(sectionValue);
        //console.log(obj);
        $scope.tempObject = angular.copy(obj);
        //console.log(angular.copy(obj));
        $scope.visibilityControl = sectionValue;
        $('[data-toggle="tooltip"]').tooltip();
        try {
            $('.fileinput-exists').find('a').trigger('click');
        } catch (e) {

        }
    };

    //Controls the View and Add feature on the page
    $scope.operateCancelControl = function (Form_Div_Id) {
        $rootScope.closeAlertMessage();
        $scope.tempObject = {};
        $('.field-validation-error').removeClass('field-validation-error').addClass('field-validation-valid');
        $('.input-validation-error').removeClass('input-validation-error').addClass('valid');
        $scope.visibilityControl = "";
    };

    //====================== Hospital Privilege Information ===================


    $scope.message = { desc: "", status: "" };

    $scope.saveHopitalPrivilegeInformation = function (hospitalPrivilegeInformation) {
        loadingOn();
        $scope.message.desc = "";
        //console.log(hospitalPrivilegeInformation);
        var validationStatus;
        var url;

        //Add Details - Denote the URL
        
        $formDataHospitalPrivilege = $('#hospitalPrivilegeInfoForm');

       

        validationStatus = $('#hospitalPrivilegeInfoForm').valid();
       
        url = "/Profile/HospitalPrivilege/UpdateHospitalPrivilegeInfoAsync?profileId=" + profileId;


        //console.log(hospitalPrivilegeInformation);

        if (validationStatus) {
            if (hospitalPrivilegeInformation.HospitalPrivilegeYesNoOption == '1') {
                hospitalPrivilegeInformation.OtherAdmittingArrangements = "";
                $($formDataHospitalPrivilege[0]).find($("[name='OtherAdmittingArrangements']")).val("");
            }
            else {
                try {
                    $scope.HospitalPrivilegeInformations.HospitalPrivilegeDetails = [];
                } catch (e) { }
            }
            $.ajax({
                url: url,
                type: 'POST',
                data: new FormData($formDataHospitalPrivilege[0]),
                async: false,
                cache: false,
                contentType: false,
                processData: false,
                success: function (data) {                   
                    //console.log(data);
                    if (data.status == "true") {                        
                        try
                        {
                            $scope.HospitalPrivilegeInformations
                            $scope.HospitalPrivilegeInformations = angular.copy(data.hospitalPrivilegeInformation);
                        }
                        catch(e)
                        { }
                        $scope.operateCancelControl('');
                        messageAlertEngine.callAlertMessage("alertHospitalPrivilegeInformation", "Hospital Privilege Information Saved Successfully!!!!", "success", true);                        
                        $scope.resetDates();
                    } else {                        
                        messageAlertEngine.callAlertMessage("alertHospitalPrivilegeInformation", data.status, "danger", true);
                    }
                },
                error: function (e) {                    
                    messageAlertEngine.callAlertMessage("alertHospitalPrivilegeInformation", "Sorry for Inconvenience !!!! Please Try Again Later...", "danger", true);
                }
            });
        }
     
        loadingOff();
    };

    //====================== Hospital Privilege Detail ===================
    $scope.saveHospitalPrivilegeDetail = function (hospitalPrivilegeInformation, IndexValue) {
        $scope.HospitalPrivilegeError = '';
        //console.log(hospitalPrivilegeInformation);
        $scope.indexVal = IndexValue;
        var validationStatus;
        var url;
        var $formDataHospitalPrivilege;

        

        if ($scope.visibilityControl == 'addhospitalPrivilegeInformation') {
            //Add Details - Denote the URL
            

            $formDataHospitalPrivilege = $('#newShowHosPrvDiv').find('form');

            ResetFormForValidation($formDataHospitalPrivilege);
            //validationStatus = true;
            url = "/Profile/HospitalPrivilege/AddHospitalPrivilegeAsync?profileId=" + profileId;
            validationStatus = $('#newShowHosPrvDiv').find('form').valid();
        }
        else if ($scope.visibilityControl == (IndexValue + '_edithospitalPrivilegeInformation')) {
            //Update Details - Denote the URL
            //ResetFormForValidation($formDataHospitalPrivilege);
            $formDataHospitalPrivilege = $('#hospitalPrivilegeInformationEditDiv' + IndexValue).find('form');

            ResetFormForValidation($formDataHospitalPrivilege);
            //validationStatus = true;
            url = "/Profile/HospitalPrivilege/UpdateHospitalPrivilegeAsync?profileId=" + profileId;
            validationStatus = $('#hospitalPrivilegeInformationEditDiv' + IndexValue).find('form').valid();
        }
        else {
            //ResetFormForValidation($formDataHospitalPrivilege);
            $formDataHospitalPrivilege = $('#hospitalPrivilegeInformationRenewDiv' + IndexValue).find('form');

            ResetFormForValidation($formDataHospitalPrivilege);
            //validationStatus = true;
            url = "/Profile/HospitalPrivilege/RenewHospitalPrivilegeAsync?profileId=" + profileId;
           validationStatus = $('#hospitalPrivilegeInformationRenewDiv' + IndexValue).find('form').valid();
        }
        //console.log(hospitalPrivilegeInformation);
        

        if (validationStatus) {

            $.ajax({
                url: url,
                type: 'POST',
                data: new FormData($formDataHospitalPrivilege[0]),
                async: false,
                cache: false,
                contentType: false,
                processData: false,
                success: function (data) {
                    if (hospitalPrivilegeInformation.PreferenceType == "1") {
                        $scope.setPrimary();
                    }
                    if (data.status == "true") {
                        if ($scope.visibilityControl == 'addhospitalPrivilegeInformation') {                            
                            hospitalPrivilegeInformation.HospitalPrivilegeDetailID = data.id;
                            hospitalPrivilegeInformation.Hospital.HospitalName = $scope.getHospitalNameForProvider(hospitalPrivilegeInformation.Hospital.HospitalID).HospitalName;
                            hospitalPrivilegeInformation.HospitalContactInfo.LocationName = $scope.getHospitalLocationForProvider(hospitalPrivilegeInformation.Hospital.HospitalID,hospitalPrivilegeInformation.HospitalContactInfo.HospitalContactInfoID).LocationName;
                            hospitalPrivilegeInformation.HospitalPrevilegeLetterPath = data.hospitalPrivilegeDetail.HospitalPrevilegeLetterPath;
                            $scope.HospitalPrivilegeInformations.HospitalPrivilegeDetails.push(hospitalPrivilegeInformation);
                            $scope.operateCancelControl('');
                            messageAlertEngine.callAlertMessage("addedNewHospitalPrivilege", "New Hospital Privilege Added Successfully !!!!", "success", true);
                            $scope.tempObject = {};
                            $scope.tempObject = {};
                        }
                        else if ($scope.visibilityControl == (IndexValue + '_edithospitalPrivilegeInformation')) {
                            hospitalPrivilegeInformation.Hospital.HospitalName = $scope.getHospitalNameForProvider(hospitalPrivilegeInformation.Hospital.HospitalID).HospitalName;
                            hospitalPrivilegeInformation.HospitalContactInfo.LocationName = $scope.getHospitalLocationForProvider(hospitalPrivilegeInformation.Hospital.HospitalID,hospitalPrivilegeInformation.HospitalContactInfo.HospitalContactInfoID).LocationName;
                            hospitalPrivilegeInformation.HospitalPrevilegeLetterPath = data.hospitalPrivilegeDetail.HospitalPrevilegeLetterPath;
                            $scope.HospitalPrivilegeInformations.HospitalPrivilegeDetails[IndexValue] = hospitalPrivilegeInformation;
                           
                            $scope.operateViewAndAddControl(IndexValue + '_viewhospitalPrivilegeInformation');
                            messageAlertEngine.callAlertMessage('updateHospitalPrivilege' + IndexValue, "Hospital Privilege Information Updated Successfully !!!!", "success", true);
                        }
                        else {
                            hospitalPrivilegeInformation.Hospital.HospitalName = $scope.getHospitalNameForProvider(hospitalPrivilegeInformation.Hospital.HospitalID).HospitalName;
                            hospitalPrivilegeInformation.HospitalContactInfo.LocationName = $scope.getHospitalLocationForProvider(hospitalPrivilegeInformation.Hospital.HospitalID,hospitalPrivilegeInformation.HospitalContactInfo.HospitalContactInfoID).LocationName;
                            hospitalPrivilegeInformation.HospitalPrevilegeLetterPath = data.hospitalPrivilege.HospitalPrevilegeLetterPath;

                            $scope.HospitalPrivilegeInformations.HospitalPrivilegeDetails[IndexValue] = hospitalPrivilegeInformation;
                            $scope.operateViewAndAddControl(IndexValue + '_viewhospitalPrivilegeInformation');
                            messageAlertEngine.callAlertMessage('renewHospitalPrivilege' + IndexValue, "Hospital Privilege Information Renewed Successfully !!!!", "success", true);

                        }
                          

                         $scope.resetDates();

                    } else {
                        messageAlertEngine.callAlertMessage('HospitalPrivilegeError', "", "danger", true);
                        $scope.HospitalPrivilegeError = data.status.split(",");
                    }
                },
                error: function (e) {
                    messageAlertEngine.callAlertMessage('HospitalPrivilegeError', "", "danger", true);
                    $scope.HospitalPrivilegeError = "Sorry for Inconvenience !!!! Please Try Again Later...";
                }
            });
        }
       
    };

    $scope.resetDates = function () {
        try
        {
            $scope.tempObject.AffilicationStartDate = new Date();
            $scope.tempObject.AffiliationEndDate = new Date();
        }
        catch(e){}        
    };

}]);



