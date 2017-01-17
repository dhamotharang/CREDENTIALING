
//--------------------- Tool tip Directive ------------------------------
profileApp.directive('tooltip', function () {
    return function (scope, elem) {
        elem.tooltip();
    };
});

profileApp.controller('DocumentRepositoryController', function ($scope, $rootScope, $timeout, $http, countryDropDownService) {

    $scope.LicenseTypes = [
        {
            LicenseTypeID: 1,
            LicenseTypeName: "State License",
            Licenses: [
                {
                    LicenseID: 1,
                    LicenseName: "State License 1",
                    LicenseDocPath: "/Content/Images/Plans/1.jpg",
                },
                {
                    LicenseID: 2,
                    LicenseName: "State License 33",
                    LicenseDocPath: "/Content/Images/Plans/Ultimate.jpg",
                },
                {
                    LicenseID: 3,
                    LicenseName: "State License 12",
                    LicenseDocPath: "/Content/Images/Plans/2.jpg",
                }
            ],
            LicenseHistories: [
                {
                    LicenseID: 10,
                    LicenseName: "State License 1",
                    LicenseDocPath: "/Content/Images/Plans/1.jpg",
                    removeDate: new Date(2014, 03, 06)
                },
                {
                    LicenseID: 20,
                    LicenseName: "State License 33",
                    LicenseDocPath: "/Content/Images/Plans/Ultimate.jpg",
                    removeDate: new Date(2014, 11, 09)
                },
                {
                    LicenseID: 30,
                    LicenseName: "State License 12",
                    LicenseDocPath: "/Content/Images/Plans/2.jpg",
                    removeDate: new Date(2015, 02, 16)
                }
            ]
        },
        {
            LicenseTypeID: 2,
            LicenseTypeName: "DEA Information",
            Licenses: [
                {
                    LicenseID: 4,
                    LicenseName: "DEA License 1",
                    LicenseDocPath: "/Content/Images/Plans/Humana.jpg",
                },
                {
                    LicenseID: 5,
                    LicenseName: "DEA License 2",
                    LicenseDocPath: "/Content/Images/Plans/WellCare.jpg",
                },
                {
                    LicenseID: 6,
                    LicenseName: "DEA License 3",
                    LicenseDocPath: "/Content/Images/Plans/2.jpg",
                }
            ],
            LicenseHistories: [
               {
                   LicenseID: 40,
                   LicenseName: "DEA License 1",
                   LicenseDocPath: "/Content/Images/Plans/Humana.jpg",
                   removeDate: new Date(2015, 03, 06)
               },
                {
                    LicenseID: 50,
                    LicenseName: "DEA License 2",
                    LicenseDocPath: "/Content/Images/Plans/WellCare.jpg",
                    removeDate: new Date(2015, 01, 15)
                },
                {
                    LicenseID: 60,
                    LicenseName: "DEA License 3",
                    LicenseDocPath: "/Content/Images/Plans/2.jpg",
                    removeDate: new Date(2015, 09, 15)
                }
            ]
        },
        {
            LicenseTypeID: 3,
            LicenseTypeName: "Medicare Information",
            Licenses: [
                {
                    LicenseID: 7,
                    LicenseName: "Medicare Information 11",
                    LicenseDocPath: "/Content/Images/Plans/1.jpg",
                },
                {
                    LicenseID: 8,
                    LicenseName: "Medicare Information 41",
                    LicenseDocPath: "/Content/Images/Plans/3.jpg",
                },
                {
                    LicenseID: 9,
                    LicenseName: "Medicare Information 14",
                    LicenseDocPath: "/Content/Images/Plans/2.jpg",
                }
            ],
            LicenseHistories: [
                {
                    LicenseID: 70,
                    LicenseName: "Medicare Information 11",
                    LicenseDocPath: "/Content/Images/Plans/1.jpg",
                    removeDate: new Date(2015, 01, 05)
                },
                {
                    LicenseID: 80,
                    LicenseName: "Medicare Information 41",
                    LicenseDocPath: "/Content/Images/Plans/3.jpg",
                    removeDate: new Date(2014, 11, 15)
                },
                {
                    LicenseID: 90,
                    LicenseName: "Medicare Information 14",
                    LicenseDocPath: "/Content/Images/Plans/2.jpg",
                    removeDate: new Date(2013, 08, 05)
                }
            ]
        }
    ];

    //-------------------- required Data Attribute --------------
    $scope.SelectedLicenseType = {};
    $scope.SelectedLicense = {};
    $scope.TempObject = {};
    $scope.IsAddNewDocument = false;
    $scope.IsMessage = false;
    $scope.IsHistoryView = false;

    //----------------------------- view license Type ---------------------
    $scope.ViewLicenseList = function (licenseType, condition) {
        if (licenseType.LicenseTypeID == $scope.SelectedLicenseType.LicenseTypeID) {
            $scope.SelectedLicenseType = {};
        } else {
            $scope.SelectedLicenseType = licenseType;
            $scope.SelectedLicense = {};
        }
        if (condition) {
            $scope.IsHistoryView = true;
        } else {
            $scope.IsHistoryView = false;
        }
    };
    //------------------- view license doc ----------------------
    $scope.ViewLicense = function (license) {
        $scope.SelectedLicense = license;
        $scope.IsAddNewDocument = false;
    };
    //----------------- add new document ---------------
    $scope.AddNewDocument = function (licenseType) {
        $scope.TempObject = {
            Title : "",
            CategoryID: licenseType.LicenseTypeID,
            IsPrivate: false,
            Document: {}
        };
        $scope.IsAddNewDocument = true;
    };

    $scope.EditDocument = function (licenseType, license) {
        $scope.TempObject = {
            Title: license.LicenseName,
            CategoryID: licenseType.LicenseTypeID,
            IsPrivate: false,
            Document: {}
        };
        $scope.IsAddNewDocument = true;
    };


    $scope.SaveData = function (data) {
        console.log(data);
        $scope.IsAddNewDocument = false;
        $scope.SuccessMessage = "Document Updated Successfully !!!!!!!!!!!";
        $scope.IsMessage = true;

        $timeout(function () {
            $scope.IsMessage = false;
        }, 5000);
    };

    $scope.CancelAdd = function () {
        $scope.IsAddNewDocument = false;
    };

    //------------------------------ Remove Modal Show data ---------------------------
    $scope.RemoveDocument = function (licenseType, license) {
        $scope.TempLicenseType = licenseType;
        $scope.TempLicense = license;
        $scope.IsAddNewDocument = false;
        $("#DocRemoveConfirmation").modal('show');
    };

    //------------------------------ Remove Confirmation data ---------------------------
    $scope.Confirmation = function (license) {
        $scope.TempLicenseType.Licenses.splice($scope.TempLicenseType.Licenses.indexOf(license), 1);
        $scope.TempLicenseType = {};
        $scope.TempLicense = {};

        $scope.SuccessMessage = "Document Removed Successfully !!!!!!!!!!!";
        $scope.IsMessage = true;

        $("#DocRemoveConfirmation").modal('hide');
    };

});



