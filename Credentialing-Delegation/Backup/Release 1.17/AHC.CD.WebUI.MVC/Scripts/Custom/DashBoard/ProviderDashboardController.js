// ------------------------------ Provider Dashboard Controller ---------------------------

dashboardApp.controller("ProviderDashboardController", ["$scope", "$http", function ($scope, $http) {

    var licenses = [{
        LicenseType: "State License",
        Licenses: [
            {
                LicenseNumber: "MEbbbbbbbbbbb93756",
                IssueState: "Florida",
                IssueDate: new Date(2015, 02, 22),
                ExpiryDate: new Date(2015, 05, 10)
            },
            {
                LicenseNumber: "MM26mmmmmmmmmmmmm123",
                IssueState: "Florida1",
                IssueDate: new Date(2014, 09, 10),
                ExpiryDate: new Date(2015, 06, 01)
            },
            {
                LicenseNumber: "MM26123",
                IssueState: "Florida1",
                IssueDate: new Date(2014, 09, 10),
                ExpiryDate: new Date(2015, 06, 01)
            },
            {
                LicenseNumber: "MM26123",
                IssueState: "Florida1",
                IssueDate: new Date(2014, 09, 10),
                ExpiryDate: new Date(2015, 06, 01)
            },
            {
                LicenseNumber: "MM26123",
                IssueState: "Florida1",
                IssueDate: new Date(2014, 09, 10),
                ExpiryDate: new Date(2015, 06, 01)
            },
            {
                LicenseNumber: "MM26123",
                IssueState: "Florida1",
                IssueDate: new Date(2014, 09, 10),
                ExpiryDate: new Date(2015, 06, 01)
            },
            {
                LicenseNumber: "MM26123",
                IssueState: "Florida1",
                IssueDate: new Date(2014, 09, 10),
                ExpiryDate: new Date(2015, 06, 01)
            },
            {
                LicenseNumber: "MMxxxxxxxxxxxxxbbbbbbbb26123",
                IssueState: "Florida1",
                IssueDate: new Date(2014, 09, 10),
                ExpiryDate: new Date(2015, 06, 01)
            },
            {
                LicenseNumber: "MM26123",
                IssueState: "Florida1",
                IssueDate: new Date(2014, 09, 10),
                ExpiryDate: new Date(2015, 06, 01)
            },
            {
                LicenseNumber: "MM26123",
                IssueState: "Florida1",
                IssueDate: new Date(2014, 09, 10),
                ExpiryDate: new Date(2015, 06, 01)
            },
            {
                LicenseNumber: "MM26123",
                IssueState: "Florida1",
                IssueDate: new Date(2014, 09, 10),
                ExpiryDate: new Date(2015, 06, 01)
            },
            {
                LicenseNumber: "MM2xxxxxxxxxxxxx6123",
                IssueState: "Florida1",
                IssueDate: new Date(2014, 09, 10),
                ExpiryDate: new Date(2015, 06, 01)
            },
            {
                LicenseNumber: "MM26123",
                IssueState: "Florida1",
                IssueDate: new Date(2014, 09, 10),
                ExpiryDate: new Date(2015, 06, 01)
            },
            {
                LicenseNumber: "MM26nnnnnnnnnnnnnnnn123",
                IssueState: "Florida1",
                IssueDate: new Date(2014, 09, 10),
                ExpiryDate: new Date(2015, 06, 01)
            },
            {
                LicenseNumber: "MM26cccccccccccccc123",
                IssueState: "Florida1",
                IssueDate: new Date(2014, 09, 10),
                ExpiryDate: new Date(2015, 06, 01)
            },
            {
                LicenseNumber: "MMddddddddddddddd26123",
                IssueState: "Florida1",
                IssueDate: new Date(2014, 09, 10),
                ExpiryDate: new Date(2015, 06, 01)
            },
            {
                LicenseNumber: "MM26ggggggggggggggg123",
                IssueState: "Florida1",
                IssueDate: new Date(2014, 09, 10),
                ExpiryDate: new Date(2015, 06, 01)
            },
            {
                LicenseNumber: "ghfjjgj",
                IssueState: "Florida1",
                IssueDate: new Date(2014, 09, 10),
                ExpiryDate: new Date(2015, 06, 01)
            },
            {
                LicenseNumber: "MM2gjgjgjgjgj6123",
                IssueState: "Florida1",
                IssueDate: new Date(2014, 09, 10),
                ExpiryDate: new Date(2015, 06, 01)
            },
            {
                LicenseNumber: "ghgjgggggggggggggjgj",
                IssueState: "Florida1",
                IssueDate: new Date(2014, 09, 10),
                ExpiryDate: new Date(2015, 06, 01)
            },
            {
                LicenseNumber: "fdhgfhfhgh",
                IssueState: "Florida1",
                IssueDate: new Date(2014, 09, 10),
                ExpiryDate: new Date(2015, 06, 01)
            },
            {
                LicenseNumber: "fhghgfh",
                IssueState: "Florida1",
                IssueDate: new Date(2014, 09, 10),
                ExpiryDate: new Date(2015, 06, 01)
            },
            {
                LicenseNumber: "gjgfjghj",
                IssueState: "Florida1",
                IssueDate: new Date(2014, 09, 10),
                ExpiryDate: new Date(2015, 06, 01)
            },
            {
                LicenseNumber: "MM26123",
                IssueState: "Florida1",
                IssueDate: new Date(2014, 09, 10),
                ExpiryDate: new Date(2015, 06, 01)
            }
        ]
    },
    {
        LicenseType: "Federal DEA",
        Licenses: [{
            DEANumber: "BV9382386",
            StateOfReg: "Florida",
            IssueDate: new Date(2015, 01, 10),
            ExpiryDate: new Date(2015, 04, 20)
        }, {
            DEANumber: "BS4921816",
            StateOfReg: "Florida",
            IssueDate: new Date(2014, 05, 13),
            ExpiryDate: new Date(2015, 02, 11)
        }]
    },
    {
        LicenseType: "CDS Information",
        Licenses: [{
            CertNumber: "CDSC8637653",
            State: "Florida",
            IssueDate: new Date(2014, 09, 10),
            ExpiryDate: new Date(2015, 06, 17)
        }]
    }, {
        LicenseType: "Specialty/Board",
        Licenses: [{
            DEANumber: "SB9382386",
            StateOfReg: "Florida",
            IssueDate: new Date(2015, 01, 10),
            ExpiryDate: new Date(2015, 03, 20)
        }, {
            DEANumber: "SB86676565",
            StateOfReg: "Florida",
            IssueDate: new Date(2014, 05, 13),
            ExpiryDate: new Date(2015, 07, 11)
        }]
    }, {
        LicenseType: "Hospital Privileges",
        Licenses: [{
            AffilicationStartDate: new Date(2014, 09, 10),
            AffilicationEndDate: new Date(2015, 07, 20)
        }]
    }, {
        LicenseType: "Professional Liability",
        Licenses: [{
            DEANumber: "PL922226",
            StateOfReg: "Florida",
            IssueDate: new Date(2015, 01, 10),
            ExpiryDate: new Date(2015, 06, 20)
        }, {
            DEANumber: "PL4463656",
            StateOfReg: "Florida",
            IssueDate: new Date(2014, 05, 13),
            ExpiryDate: new Date(2015, 10, 11)
        }, {
            DEANumber: "PL4463656",
            StateOfReg: "Florida",
            IssueDate: new Date(2014, 05, 13),
            ExpiryDate: new Date(2015, 10, 11)
        }, {
            DEANumber: "PL4463656",
            StateOfReg: "Florida",
            IssueDate: new Date(2014, 05, 13),
            ExpiryDate: new Date(2015, 10, 11)
        }, {
            DEANumber: "PL44636vvvv56",
            StateOfReg: "Florida",
            IssueDate: new Date(2014, 05, 13),
            ExpiryDate: new Date(2015, 10, 11)
        }, {
            DEANumber: "PL446eeeee3656",
            StateOfReg: "Florida",
            IssueDate: new Date(2014, 05, 13),
            ExpiryDate: new Date(2015, 10, 11)
        }, {
            DEANumber: "PL4463656",
            StateOfReg: "Florida",
            IssueDate: new Date(2014, 05, 13),
            ExpiryDate: new Date(2015, 10, 11)
        }, {
            DEANumber: "PL4463656",
            StateOfReg: "Florida",
            IssueDate: new Date(2014, 05, 13),
            ExpiryDate: new Date(2015, 10, 11)
        }, {
            DEANumber: "PL446dddd3656",
            StateOfReg: "Florida",
            IssueDate: new Date(2014, 05, 13),
            ExpiryDate: new Date(2015, 10, 11)
        }, {
            DEANumber: "PL446ddddddd3656",
            StateOfReg: "Florida",
            IssueDate: new Date(2014, 05, 13),
            ExpiryDate: new Date(2015, 10, 11)
        }, {
            DEANumber: "PL446vvv3656",
            StateOfReg: "Florida",
            IssueDate: new Date(2014, 05, 13),
            ExpiryDate: new Date(2015, 10, 11)
        }, {
            DEANumber: "PL4463bbbbbb656",
            StateOfReg: "Florida",
            IssueDate: new Date(2014, 05, 13),
            ExpiryDate: new Date(2015, 10, 11)
        }, {
            DEANumber: "PL4463656",
            StateOfReg: "Florida",
            IssueDate: new Date(2014, 05, 13),
            ExpiryDate: new Date(2015, 10, 11)
        }]
    },
    {
        LicenseType: "Worker Compensation",
        Licenses: [{
            WorkersCompensationNumber: "WCN75445433",
            IssueDate: new Date(2014, 09, 10),
            ExpirationDate: new Date(2015, 03, 30)
        }, {
            WorkersCompensationNumber: "WCN96755656",
            IssueDate: new Date(2014, 09, 10),
            ExpirationDate: new Date(2015, 10, 30)
        }]
    }
    ];

    var CCOLicenseData = [{
        LicenseType: "State License",
        Providers: [{
            PersonalDetails: {
                NPINumber: "1234567890",
                FirstName: "Smith",
                MiddleName: "",
                LastName: "Sachin"
            },
            Licenses: [
                {
                    LicenseNumber: "ME93756",
                    IssueState: "Florida",
                    IssueDate: new Date(2014, 02, 22),
                    ExpiryDate: new Date(2016, 03, 30)
                },
                {
                    LicenseNumber: "MM26123",
                    IssueState: "Florida1",
                    IssueDate: new Date(2014, 09, 10),
                    ExpiryDate: new Date(2016, 08, 20)
                }
            ]

        }]
    },
    {
        //------- same provider Details Here according to license type ----------
    }
    ];


    //--------------- angular scope value assign ------------------
    $scope.LicenseData = licenses;


    //----------------------- Day Left function return Left Days ---------------------------
    $scope.GetRenewalDayLeft = function (datevalue) {
        if (datevalue) {
            var oneDay = 24 * 60 * 60 * 1000; // hours*minutes*seconds*milliseconds

            var currentdate = new Date(new Date().getFullYear(), new Date().getMonth(), new Date().getDate());

            var secondDate = new Date(2008, 01, 22);

            return Math.round((datevalue.getTime() - currentdate.getTime()) / (oneDay));
        }
        return null;
    };

    //------------------- left day calculate ----------------------

    for (var i in $scope.LicenseData) {
        if ($scope.LicenseData[i].Licenses && ($scope.LicenseData[i].LicenseType == "State License" || $scope.LicenseData[i].LicenseType == "Federal DEA" || $scope.LicenseData[i].LicenseType == "CDS Information" || $scope.LicenseData[i].LicenseType == "Specialty/Board" || $scope.LicenseData[i].LicenseType == "Professional Liability")) {
            for (var j in $scope.LicenseData[i].Licenses) {
                $scope.LicenseData[i].Licenses[j].dayLeft = $scope.GetRenewalDayLeft($scope.LicenseData[i].Licenses[j].ExpiryDate);
            }
        } else if ($scope.LicenseData[i].Licenses && $scope.LicenseData[i].LicenseType == "Hospital Privileges") {
            for (var j in $scope.LicenseData[i].Licenses) {
                $scope.LicenseData[i].Licenses[j].dayLeft = $scope.GetRenewalDayLeft($scope.LicenseData[i].Licenses[j].AffilicationEndDate);
            }
        } else if ($scope.LicenseData[i].Licenses && $scope.LicenseData[i].LicenseType == "Worker Compensation") {
            for (var j in $scope.LicenseData[i].Licenses) {
                $scope.LicenseData[i].Licenses[j].dayLeft = $scope.GetRenewalDayLeft($scope.LicenseData[i].Licenses[j].ExpirationDate);
            }
        }
    }

    //---------------------- license status return -------------------
    for (var i in $scope.LicenseData) {
        if ($scope.LicenseData[i].Licenses) {

            var ValidatedLicense = 0;
            var dayLeftLicense = 0;
            var ExpiredLicense = 0;

            for (var j in $scope.LicenseData[i].Licenses) {
                if ($scope.LicenseData[i].Licenses[j].dayLeft < 0) {
                    ExpiredLicense++;
                } else if ($scope.LicenseData[i].Licenses[j].dayLeft < 90) {
                    dayLeftLicense++;
                }
                else if ($scope.LicenseData[i].Licenses[j].dayLeft < 180) {
                    ValidatedLicense++;
                }
            }

            $scope.LicenseData[i].LicenseStatus = {
                ValidLicense: ValidatedLicense,
                PendingDaylicense: dayLeftLicense,
                ExpiredLicense: ExpiredLicense
            };
        }
    }

    console.log($scope.LicenseData);

    $scope.CurrentPageLicenses = [];

    $scope.Licenses = $scope.LicenseData[0].Licenses;
    $scope.LicenseType = $scope.LicenseData[0].LicenseType;

    //-------------- licenses change method -------------------------------
    $scope.getLicensTypeData = function (licenseData) {
        $scope.Licenses = licenseData.Licenses;
        $scope.LicenseType = licenseData.LicenseType;
    };

    //-------------------------- angular bootstrap pagger with custom-----------------
    $scope.maxSize = 5;
    $scope.bigTotalItems = 0;
    $scope.bigCurrentPage = 1;

    //-------------- current page change Scope Watch ---------------------
    $scope.$watch('bigCurrentPage', function (newValue, oldValue) {
        $scope.CurrentPageLicenses = [];
        var startIndex = (newValue - 1) * 10;
        var endIndex = startIndex + 9;

        for (startIndex; startIndex <= endIndex ; startIndex++) {
            if ($scope.Licenses[startIndex]) {
                $scope.CurrentPageLicenses.push($scope.Licenses[startIndex]);
            } else {
                break;
            }
        }
        console.log($scope.CurrentPageLicenses);
    });
    //-------------- License Scope Watch ---------------------
    $scope.$watchCollection('Licenses', function (newLicenses, oldLicenses) {
        $scope.bigTotalItems = newLicenses.length;

        $scope.CurrentPageLicenses = [];
        $scope.bigCurrentPage = 1;

        var startIndex = ($scope.bigCurrentPage - 1) * 10;
        var endIndex = startIndex + 9;

        for (startIndex; startIndex <= endIndex ; startIndex++) {
            if ($scope.Licenses[startIndex]) {
                $scope.CurrentPageLicenses.push($scope.Licenses[startIndex]);
            } else {
                break;
            }
        }
        console.log($scope.CurrentPageLicenses);
    });
    //------------------- end ------------------

    //$scope.IsRenewalDayPending = function (datevalue) {
    //    var currentdate = new Date();
    //    //------------ change date as year month and day ----------------

    //    if (datevalue < currentdate) {
    //        return 1;
    //    } else if (datevalue < currentdate) {
    //        return 0;
    //    } else if (datevalue < currentdate) {
    //        return -1;
    //    }
    //};
}]);
