
profileApp.controller('hospitalcltr', ['$scope', '$rootScope', '$http', function ($scope, $rootScope, $http, countryDropDownService) {

    
    $(document).ready(function () {
        $http.get(rootDir + "/Profile/MasterData/GetAllHospitals").then(function (value) { $scope.hospitals = value.data; });
        $http.get(rootDir + "/Profile/MasterData/GetAllStaffCategories").then(function (value) { $scope.staffCategories = value.data; });
        $http.get(rootDir + "/Profile/MasterData/GetAllAdmittingPrivileges").then(function (value) { $scope.admittingPrivileges = value.data; });
        $http.get(rootDir + "/Profile/MasterData/GetAllHospitalContactPersons").then(function (value) { $scope.ContactPersons = value.data; });
    });

    
    $scope.HaveHospitalPrivilege = "True";

    $scope.HospitalPrivilegeInformations = [];

    // //rootScoped on emitted value catches the value for the model and insert to get the old data
    $rootScope.$on('HospitalPrivilegeInformation', function (event, val) {
        $scope.HospitalPrivilegeInformations = val;        
    });


    $scope.filterData = function () {
        $scope.locations = $scope.hospitals.filter(function (hospitals) { return hospitals.HospitalID == $scope.tempObject.HospitalID })[0].HospitalContactInfoes;
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
        return $rootScope.getSpecilityForThisUser().filter(function (specialties) { return specialties.Specialty.SpecialtyID == findData })[0];
    }

    $scope.getContactPersonForProvider = function (findData) {
        return $scope.ContactPersons.filter(function (ContactPersons) { return ContactPersons.HospitalContactPersonID == findData })[0];
    }

    $scope.getAdmittingPrivilageStatusForProvider = function (findData) {
        return $scope.admittingPrivileges.filter(function (admittingPrivileges) { return admittingPrivileges.AdmittingPrivilegeID == findData })[0];
    }
    //=============== Hospital Privilege Information Conditions ==================

    $scope.submitButtonText = "Add";

    $scope.indexVal = "";

    //====================== Hospital Privilege Information ===================


    $scope.message = { desc: "", status: "" };

    $scope.saveHopitalPrivilegeInformation = function (hospitalPrivilegeInformation) {
        $scope.message.desc = "";

        var validationStatus;
        var url;


        //Add Details - Denote the URL
        validationStatus = $('#hospitalPrivilegeInfoForm').valid();
        $formDataHospitalPrivilege = $('#hospitalPrivilegeInfoForm');
        url = "/Profile/HospitalPrivilege/UpdateHospitalPrivilegeInfoAsync?profileId=" + profileId;



        if (validationStatus) {

            $.ajax({
                url: rootDir + url,
                type: 'POST',
                data: new FormData($formDataHospitalPrivilege[0]),
                async: false,
                cache: false,
                contentType: false,
                processData: false,
                success: function (data) {
                    try {
                        if (data.status == "true") {
                            $scope.message.status = "success";
                            $scope.HospitalPrivilegeInformations.HospitalPrivilegeYesNoOption = hospitalPrivilegeInformation.HospitalPrivilegeYesNoOption;
                            $scope.HospitalPrivilegeInformations.OtherAdmittingArrangements = hospitalPrivilegeInformation.OtherAdmittingArrangements;
                            $scope.message.desc = "Hospital Privilege Information Saved Successfully!!!!";
                            $rootScope.operateCancelControl('');
                        } else {
                            $scope.message.status = "danger";
                            $scope.message.desc = data.status;
                        }
                    } catch (e) {
                       
                    }
                },
                error: function (e) {
                    $scope.message.status = "danger";
                    $scope.message.desc = "Sorry for Inconvenience !!!! Please Try Again Later...";
                }
            });
        }
    };

    //====================== Hospital Privilege Detail ===================
    $scope.saveHospitalPrivilegeDetail = function (hospitalPrivilegeInformation, IndexValue) {
        $scope.HospitalPrivilegeError = '';
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


        if (validationStatus) {

            $.ajax({
                url: rootDir + url,
                type: 'POST',
                data: new FormData($formDataHospitalPrivilege[0]),
                async: false,
                cache: false,
                contentType: false,
                processData: false,
                success: function (data) {
                    if (data.status == "true") {
                        if ($scope.visibilityControl == 'addhospitalPrivilegeInformation') {
                            hospitalPrivilegeInformation.HospitalPrivilegeDetailID = data.id;                            
                            $scope.HospitalPrivilegeInformations.HospitalPrivilegeDetails.push(hospitalPrivilegeInformation);
                            $rootScope.visibilityControl = "addedNewHospitalPrivilege"
                        }
                        else {
                            $scope.HospitalPrivilegeInformations.HospitalPrivilegeDetails[IndexValue] = hospitalPrivilegeInformation;                            
                            $rootScope.visibilityControl = "updatedHospitalPrivilege"
                        }

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



