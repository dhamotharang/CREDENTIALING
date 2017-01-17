
profileApp.controller('hospitalcltr', ['$scope', '$rootScope', '$http', function ($scope, $rootScope, $http, countryDropDownService) {

    $(document).ready(function () {
        $http.get("/Profile/MasterData/GetAllHospitals").then(function (value) { $scope.hospitals = value.data; });
        $http.get("/Profile/MasterData/GetAllStaffCategories").then(function (value) { $scope.staffCategories = value.data; });
        $http.get("/Profile/MasterData/GetAllAdmittingPrivileges").then(function (value) { $scope.admittingPrivileges = value.data; });
        $http.get("/Profile/MasterData/GetAllHospitalContactPersons").then(function (value) { $scope.ContactPersons = value.data; });
    });


    $scope.HaveHospitalPrivilege = "True";

    $scope.HospitalPrivilegeInformations = [];

    // rootScoped on emitted value catches the value for the model and insert to get the old data
    $rootScope.$on('HospitalPrivilegeInformation', function (event, val) {
        console.log(val);
        try {
            $scope.HospitalPrivilegeInformations = val;
            if ($scope.HospitalPrivilegeInformations.HospitalPrivilegeDetails.length != 0) {
                for (var i = 0; i < $scope.HospitalPrivilegeInformations.HospitalPrivilegeDetails.length ; i++) {
                    $scope.HospitalPrivilegeInformations.HospitalPrivilegeDetails[i].AffilicationStartDate = $rootScope.ConvertDateFormat($scope.HospitalPrivilegeInformations.HospitalPrivilegeDetails[i].AffilicationStartDate);
                    $scope.HospitalPrivilegeInformations.HospitalPrivilegeDetails[i].AffiliationEndDate = $rootScope.ConvertDateFormat($scope.HospitalPrivilegeInformations.HospitalPrivilegeDetails[i].AffiliationEndDate);
                }
            }
        }
        catch (e) {

        };
    });


    $scope.filterData = function () {
        $scope.locations = $scope.hospitals.filter(function (hospitals) { return hospitals.HospitalID == $scope.tempObject.Hospital.HospitalID })[0].HospitalContactInfoes;
    };

    $scope.getAddress = function () {
        $scope.LocationAddress = $scope.locations.filter(function (locations) { return locations.HospitalContactInfoID == $scope.tempObject.HospitalContactInfo.HospitalContactInfoID })[0];
        $scope.tempObject.HospitalContactInfo.Street = $scope.LocationAddress.Street;
        $scope.tempObject.HospitalContactInfo.Country = $scope.LocationAddress.Country;
        $scope.tempObject.HospitalContactInfo.County = $scope.LocationAddress.County;
        $scope.tempObject.HospitalContactInfo.City = $scope.LocationAddress.City;
        $scope.tempObject.HospitalContactInfo.ZipCode = $scope.LocationAddress.ZipCode;
        $scope.tempObject.HospitalContactInfo.Phone = $scope.LocationAddress.Phone;
        $scope.tempObject.HospitalContactInfo.Fax = $scope.LocationAddress.Fax;
        $scope.tempObject.HospitalContactInfo.Email = $scope.LocationAddress.Email;
        $scope.tempObject.HospitalContactInfo.State = $scope.LocationAddress.State;

        $scope.contactPersons = $scope.LocationAddress.HospitalContactPersons;
    };

    $scope.getContactPersonDetail = function () {
        $scope.contactPersonDetail = $scope.contactPersons.filter(function (contactPersons) { return contactPersons.HospitalContactPersonID == $scope.tempObject.HospitalContactPerson.HospitalContactPersonID })[0];
        $scope.tempObject.HospitalContactPerson.ContactPersonPhone = $scope.contactPersonDetail.ContactPersonPhone;
        $scope.tempObject.HospitalContactPerson.ContactPersonFax = $scope.contactPersonDetail.ContactPersonFax;
    };

    $scope.specialties = $rootScope.getSpecilityForThisUser();

    $scope.getSpecialityForProvider = function (findData) {
        return $rootScope.getSpecilityForThisUser().filter(function (specialties) { return specialties.Specialty.SpecialtyID == findData })[0];
    }

    $scope.getHospitalNameForProvider = function (findData) {        
        return $scope.hospitals.filter(function (hospitals) { return hospitals.HospitalID == findData })[0];
    }

    $scope.getStaffCategoryForProvider = function (findData) {
        return $scope.staffCategories.filter(function (staffCategories) { return staffCategories.StaffCategoryID == findData })[0];
    }

    $scope.getHospitalLocationForProvider = function (findData) {
        return $scope.locations.filter(function (locations) { return locations.HospitalContactInfoID == findData })[0];
    }

    $scope.getContactPersonForProvider = function (findData) {
        return $scope.ContactPersons.filter(function (ContactPersons) { return ContactPersons.HospitalContactPersonID == findData })[0];
    }

    $scope.getAdmittingPrivilageStatusForProvider = function (findData) {
        return $scope.admittingPrivileges.filter(function (admittingPrivileges) { return admittingPrivileges.AdmittingPrivilegeID == findData })[0];
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

    // $scope.submitButtonText = "Add";

    $scope.indexVal = "";

    //====================== Hospital Privilege Information ===================


    $scope.message = { desc: "", status: "" };

    $scope.saveHopitalPrivilegeInformation = function (hospitalPrivilegeInformation) {
        loadingOn();

        $scope.message.desc = "";
        //console.log(hospitalPrivilegeInformation);
        var validationStatus;
        var url;


        //Add Details - Denote the URL
        validationStatus = $('#hospitalPrivilegeInfoForm').valid();
        $formDataHospitalPrivilege = $('#hospitalPrivilegeInfoForm');
        url = "/Profile/HospitalPrivilege/UpdateHospitalPrivilegeInfoAsync?profileId=" + profileId;


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
                    //console.log(data);
                    if (data.status == "true") {
                        $scope.message.status = "success";
                        try
                        {
                            hospitalPrivilegeInformation.HospitalPrivilegeInformationID = data.hospitalPrivilegeInformation.HospitalPrivilegeInformationID;
                            $scope.HospitalPrivilegeInformations = angular.copy(hospitalPrivilegeInformation);
                            //$scope.HospitalPrivilegeInformations.push(data.hospitalPrivilegeInformation);
                        }
                        catch(e)
                        {}
                        
                        //$scope.HospitalPrivilegeInformations.HospitalPrivilegeYesNoOption = hospitalPrivilegeInformation.HospitalPrivilegeYesNoOption;
                        //$scope.HospitalPrivilegeInformations.OtherAdmittingArrangements = hospitalPrivilegeInformation.OtherAdmittingArrangements;
                        $scope.message.desc = "Hospital Privilege Information Saved Successfully!!!!";
                        $rootScope.callAlert('alertHospitalPrivilegeInformation');
                        $rootScope.operateCancelControl('');
                    } else {
                        $scope.message.status = "danger";
                        $scope.message.desc = data.status;
                        $rootScope.callAlert('alertHospitalPrivilegeInformation');
                    }

                    if (hospitalPrivilegeInformation.HospitalPrivilegeYesNoOption == '1') {
                        $scope.HospitalPrivilegeInformations.OtherAdmittingArrangements = "";
                    }
                    else if (hospitalPrivilegeInformation.HospitalPrivilegeYesNoOption == "2") {
                        try {
                            $scope.HospitalPrivilegeInformations.HospitalPrivilegeDetails = [];
                        } catch (e) { }
                    }
                },
                error: function (e) {
                    $scope.message.status = "danger";
                    $scope.message.desc = "Sorry for Inconvenience !!!! Please Try Again Later...";
                    $rootScope.callAlert('alertHospitalPrivilegeInformation');
                }
            });
            
        }
        loadingOff();
    };

    //====================== Hospital Privilege Detail ===================
    $scope.saveHospitalPrivilegeDetail = function (hospitalPrivilegeInformation, IndexValue) {
        $scope.HospitalPrivilegeError = '';
        console.log(hospitalPrivilegeInformation);
        $scope.indexVal = IndexValue;
        var validationStatus;
        var url;

        if ($scope.visibilityControl == 'addhospitalPrivilegeInformation') {
            //Add Details - Denote the URL
            validationStatus = $('#newShowHosPrvDiv').find('form').valid();
            $formDataHospitalPrivilege = $('#newShowHosPrvDiv').find('form');
            url = "/Profile/HospitalPrivilege/AddHospitalPrivilegeAsync?profileId=" + profileId;
        }
        else {
            //Update Details - Denote the URL
            validationStatus = $('#hospitalPrivilegeInformationEditDiv' + IndexValue).find('form').valid();
            $formDataHospitalPrivilege = $('#hospitalPrivilegeInformationEditDiv' + IndexValue).find('form');
            url = "/Profile/HospitalPrivilege/UpdateHospitalPrivilegeAsync?profileId=" + profileId;
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
                            hospitalPrivilegeInformation.Hospital.HospitalPrivilegeDetailID = data.id;
                            hospitalPrivilegeInformation.Hospital.HospitalName = $scope.getHospitalNameForProvider(hospitalPrivilegeInformation.Hospital.HospitalID).HospitalName;
                            hospitalPrivilegeInformation.HospitalContactInfo.LocationName = $scope.getHospitalLocationForProvider(hospitalPrivilegeInformation.HospitalContactInfo.HospitalContactInfoID).LocationName;
                            hospitalPrivilegeInformation.HospitalPrevilegeLetterPath = data.hospitalPrivilegeDetail.HospitalPrevilegeLetterPath;
                            $scope.HospitalPrivilegeInformations.HospitalPrivilegeDetails.push(hospitalPrivilegeInformation);
                            $rootScope.callAlert("addedNewHospitalPrivilege");                            
                        }
                        else {
                            hospitalPrivilegeInformation.HospitalPrevilegeLetterPath = data.hospitalPrivilegeDetail.HospitalPrevilegeLetterPath;
                            $scope.HospitalPrivilegeInformations.HospitalPrivilegeDetails[IndexValue] = hospitalPrivilegeInformation;
                            $rootScope.callAlert("updatedHospitalPrivilege");
                        }
                        $rootScope.operateCancelControl('');

                    } else {
                        $scope.HospitalPrivilegeError = data.status.split(",");
                    }
                },
                error: function (e) {
                    $scope.HospitalPrivilegeError = "Sorry for Inconvenience !!!! Please Try Again Later...";
                }
            });
        }
    };
}]);



