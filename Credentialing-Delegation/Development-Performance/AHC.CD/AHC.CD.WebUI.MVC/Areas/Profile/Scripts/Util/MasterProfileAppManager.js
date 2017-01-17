// --------------------Master Profile Manager Module -------------------------------
//--------------------- Author: KRGLV --------------------------------
//--------------- User Use Strict Section ---------------
(function () {
    'use strict';
    //------------------------- angular Authorization Manager Module - AuthorizationModule.js ----------------------------
    var app = angular.module('MasterProfileManager', ['ui.router'])

    //------------- angular pttp query factory ---------------------------
    app.factory('httpq', function ($http, $q) {
        return {
            get: function () {
                var deferred = $q.defer();
                $http.get.apply(null, arguments)
                    .success(deferred.resolve)
                    .error(deferred.resolve);
                return deferred.promise;
            },
            post: function () {
                var deferred = $q.defer();
                $http.post.apply(null, arguments)
                    .success(deferred.resolve)
                    .error(deferred.resolve);
                return deferred.promise;
            }
        }
    })
    .factory('Manager', Manager)

    //------------------get profile update service---------------------------
    .factory('profileUpdates', function () {
        var profileUpdate = {};

        profileUpdate.getUpdates = function (section, subsection) {
            var profileUpdateObj = JSON.parse(profileUpdates);
            var flag = false;
            if (profileUpdateObj != null) {
                for (var i = 0; i < profileUpdateObj.length; i++) {
                    if (profileUpdateObj[i].Section == section && profileUpdateObj[i].SubSection == subsection) {
                        flag = true;
                        break;
                    }
                }
            }

            return flag;

        }
        return profileUpdate;
    })

    //---------------------------- Order By Empty Bottom ----------------------
    .filter('orderEmpty', function () {
        return function (array, key, type) {
            var present, empty, result;

            if (!angular.isArray(array))
                return;

            present = array.filter(function (item) {
                return item[key];
            });

            empty = array.filter(function (item) {
                return !item[key];
            });

            switch (type) {
                case 'toBottom':
                    result = present.concat(empty);
                    break;
                case 'toTop':
                    result = empty.concat(present);
                    break;
                default:
                    result = array;
                    break;
            }
            return result;
        };
    })

    //------------------ File Selected validation ---------------------------
    .directive('validFile', function () {
        return {
            require: 'ngModel',
            link: function (scope, el, attrs, ngModel) {
                ngModel.$render = function () {
                    ngModel.$setViewValue(el.val());
                };

                el.bind('change', function () {
                    scope.$apply(function () {
                        ngModel.$render();
                    });
                });
            }
        };
    })

    .service('messageAlertEngine', ['$rootScope', '$timeout', function ($rootScope, $timeout) {

        $rootScope.messageDesc = "";
        $rootScope.activeMessageDiv = "";
        $rootScope.messageType = "";

        var animateMessageAlertOff = function () {
            $rootScope.closeAlertMessage();
        };


        this.callAlertMessage = function (calledDiv, msg, msgType, dismissal) { //messageAlertEngine.callAlertMessage('updateHospitalPrivilege' + IndexValue, "Data Updated Successfully !!!!", "success", true);                            
            $rootScope.activeMessageDiv = calledDiv;
            $rootScope.messageDesc = msg;
            $rootScope.messageType = msgType;
            if (dismissal) {
                $timeout(animateMessageAlertOff, 5000);
            }
        }

        $rootScope.closeAlertMessage = function () {
            $rootScope.messageDesc = "";
            $rootScope.activeMessageDiv = "";
            $rootScope.messageType = "";
        }
    }])

    //=========================== Angular Filter method Unique array list ===========================
    .filter('unique', function () {
        return function (collection, keyname) {
            var output = [], keys = [];

            angular.forEach(collection, function (item) {
                var key = item[keyname];
                if (keys.indexOf(key) === -1) {
                    keys.push(key);
                    output.push(item);
                }
            });
            return output;
        };
    })

    .service('locationService', ['$http', function ($http) {
        //location service to return array of locations relevant to the querystring
        this.getLocations = function (QueryString) {
            return $http.get(rootDir + "/Location/GetCities?city=" + QueryString)     //locationService makes an ajax call to fetch all the cities which have relevent names as the query string.
            .then(function (response) { return response.data; });     //Which is then returned to the controller method which called the service
        };
    }])

    .service('masterDataService', ['$http', '$q', function ($http, $q) {

        this.getMasterData = function (URL) {
            return $http.get(URL).then(function (value) { return value.data; });
        };

        this.getPractitioners = function (URL, level, profileId) {
            return $http({
                url: URL,
                method: "POST",
                data: { practitionerLevel: level, profileID: profileId }
            }).then(function (value) { return value.data; });
        };

        this.getProviderLevels = function (URL, profileId) {
            return $http({
                url: URL,
                method: "POST",
                data: { profileID: profileId }
            }).then(function (value) { return value.data; });
        };
    }])

    .service('dynamicFormGenerateService', function ($compile) {
        this.getForm = function (scope, formContain) {
            return $compile(formContain)(scope);
        };
    })

    //------------------------------- Provider License Service --------------------
    .service('ProviderLicenseService',["$filter", function ($filter) {

        var data = [];
        var GrandTotalLicenses = 0;

        var GrandTotalValidLicense = 0;
        var GrandTotalPendingDaylicense = 0;
        var GrandTotalExpiredLicense = 0;

        this.GetFormattedProfileData = function (expiredLicense) {
            data = [];
            GrandTotalLicenses = 0;

            GrandTotalValidLicense = 0;
            GrandTotalPendingDaylicense = 0;
            GrandTotalExpiredLicense = 0;
            //-------------------------- Custom array parse ---------------------
            if (expiredLicense.StateLicenseExpiries) {
                data.push({
                    LicenseType: "State License",
                    LicenseTypeCode: "StateLicense",
                    Licenses: expiredLicense.StateLicenseExpiries
                });
            }
            if (expiredLicense.DEALicenseExpiries) {
                data.push({
                    LicenseType: "Federal DEA",
                    LicenseTypeCode: "FederalDEA",
                    Licenses: expiredLicense.DEALicenseExpiries
                });
            }
            if (expiredLicense.CDSCInfoExpiries) {
                data.push({
                    LicenseType: "CDS Information",
                    LicenseTypeCode: "CDSInformation",
                    Licenses: expiredLicense.CDSCInfoExpiries
                });
            }
            if (expiredLicense.SpecialtyDetailExpiries) {
                data.push({
                    LicenseType: "Specialty/Board",
                    LicenseTypeCode: "SpecialityBoard",
                    Licenses: expiredLicense.SpecialtyDetailExpiries
                });
            }
            if (expiredLicense.HospitalPrivilegeExpiries) {
                data.push({
                    LicenseType: "Hospital Privileges",
                    LicenseTypeCode: "HospitalPrivilages",
                    Licenses: expiredLicense.HospitalPrivilegeExpiries
                });
            }
            if (expiredLicense.ProfessionalLiabilityExpiries) {
                data.push({
                    LicenseType: "Professional Liability",
                    LicenseTypeCode: "ProfessionalLiability",
                    Licenses: expiredLicense.ProfessionalLiabilityExpiries
                });
            }
            if (expiredLicense.WorkerCompensationExpiries) {
                data.push({
                    LicenseType: "Worker Compensation",
                    LicenseTypeCode: "WorkerCompensation",
                    Licenses: expiredLicense.WorkerCompensationExpiries
                });
            }

            //------------------- left day calculate ----------------------
            for (var i in data) {
                if (data[i].Licenses && (data[i].LicenseType == "State License" || data[i].LicenseType == "Federal DEA" || data[i].LicenseType == "CDS Information" || data[i].LicenseType == "Specialty/Board")) {
                    for (var j in data[i].Licenses) {
                        data[i].Licenses[j].ExpiryDate = ConvertDateFormats1(data[i].Licenses[j].ExpiryDate);
                        data[i].Licenses[j].dayLeft = GetRenewalDayLeft(data[i].Licenses[j].ExpiryDate);
                    }
                } else if (data[i].Licenses && data[i].LicenseType == "Hospital Privileges") {
                    for (var j in data[i].Licenses) {
                        data[i].Licenses[j].AffiliationEndDate = ConvertDateFormats1(data[i].Licenses[j].AffiliationEndDate);
                        data[i].Licenses[j].dayLeft = GetRenewalDayLeft(data[i].Licenses[j].AffiliationEndDate);
                    }
                } else if (data[i].LicenseType == "Professional Liability" || data[i].Licenses && data[i].LicenseType == "Worker Compensation") {
                    for (var j in data[i].Licenses) {
                        data[i].Licenses[j].ExpirationDate = ConvertDateFormats1(data[i].Licenses[j].ExpirationDate);
                        data[i].Licenses[j].dayLeft = GetRenewalDayLeft(data[i].Licenses[j].ExpirationDate);
                    }
                }
            }
            return this.GetLicenseStatus(data);
        }


        var GetRenewalDayLeft = function (datevalue) {
            if (datevalue) {
                var oneDay = 24 * 60 * 60 * 1000; // hours*minutes*seconds*milliseconds

                var currentdate = new Date(new Date().getFullYear(), new Date().getMonth(), new Date().getDate());

                var secondDate = new Date(2008, 1, 22);

                return Math.round((datevalue.getTime() - currentdate.getTime()) / (oneDay));
            }
            return null;
        };

        //--------------- Master license Data for Static Data ---------------
        var MasterLicenseData = angular.copy(data);

        //---------------------- license status return -------------------
        this.GetLicenseStatus = function (data) {
            GrandTotalLicenses = 0;
            GrandTotalValidLicense = 0;
            GrandTotalPendingDaylicense = 0;
            GrandTotalExpiredLicense = 0;
            for (var i in data) {
                if (data[i].Licenses) {

                    var ValidatedLicense = 0;
                    var dayLeftLicense = 0;
                    var ExpiredLicense = 0;

                    for (var j in data[i].Licenses) {
                        if (data[i].Licenses[j].dayLeft < 0) {
                            ExpiredLicense++;
                            GrandTotalLicenses++;
                        } else if (data[i].Licenses[j].dayLeft < 90) {
                            dayLeftLicense++;
                            GrandTotalLicenses++;
                        }
                        else if (data[i].Licenses[j].dayLeft < 180) {
                            ValidatedLicense++;
                            GrandTotalLicenses++;
                        }
                    }

                    var orderBy = $filter('orderBy');
                    data[i].Licenses = orderBy(data[i].Licenses, 'dayLeft', false);

                    data[i].LicenseStatus = {
                        ValidLicense: ValidatedLicense,
                        PendingDaylicense: dayLeftLicense,
                        ExpiredLicense: ExpiredLicense
                    };

                    GrandTotalValidLicense += ValidatedLicense;
                    GrandTotalPendingDaylicense += dayLeftLicense;
                    GrandTotalExpiredLicense += ExpiredLicense;
                }
            }
            return data;
        };
        //------------------ Grand Total Number of License return ---------------------
        this.GetGrandTotalLicenses = function () {
            return GrandTotalLicenses;
        };

        //------------------ Grand Total License Upcomin renewal expired of License return ---------------------
        this.GetGrandTotalLicenseStatus = function () {
            var temd = {
                GrandTotalValidLicense: GrandTotalValidLicense,
                GrandTotalPendingDaylicense: GrandTotalPendingDaylicense,
                GrandTotalExpiredLicense: GrandTotalExpiredLicense
            }
            return temd;
        };

        //----------------- simply return License List ---------------
        this.LicensesList = function () {
            this.GetLicenseStatus(data);
            return data;
        };

    }])

    //------------------- tooltip directive for dynamic data change apply function in angular -----------
    .directive('tooltip', function () {
        return function (scope, elem) {
            elem.tooltip();
        };
    })

    //------------------- popover directive for dynamic data change apply function in angular -----------
    .directive('popover', function () {
        return function (scope, elem) {
            elem.popover();
        };
    })

    .directive('samayTimePicker', function () {
        return function (scope, elem) {
            elem.clockface({
                format: 'HH:mm'
            });
        };
    })

    .directive('samayToggel', ['$compile', function ($compile) {
        return {
            restrict: 'AE',
            link: function (scope, element, attr) {
                element.bind('click', function (e) {
                    e.stopPropagation();
                    $(".clockface").hide();
                    element.parent().parent().find(".samay").clockface('toggle');
                });
            }
        };
    }])

    //----------------------- AHC Autocomplete search cum dropDown ----------------
    .directive('searchdropdown', function () {
        return {
            restrict: 'AE',
            link: function (scope, element, attr) {
                element.bind('focus', function () {
                    element.parent().find(".ProviderTypeSelectAutoList1").show();
                });
            }
        };
    })

    .directive('pageSelect', function () {
        return {
            restrict: 'E',
            template: '<input type="text" class="select-page" ng-model="inputPage" ng-change="PageResize(inputPage,numPages)">',
            controller: function ($scope) {
                $scope.$watch('inputPage', function (newV, oldV) {
                    if (newV === oldV) {
                        return;
                    }
                    else if (newV >= oldV) {
                        $scope.inputPage = newV;
                        $scope.selectPage(newV);
                        //$scope.currentPage = newV;
                    }
                    else {
                        $scope.selectPage(newV);
                    }

                });
                $scope.PageResize = function (currentPage, maxPage) {
                    if (currentPage >= maxPage) {
                        $scope.inputPage = maxPage;
                        $scope.selectPage(maxPage);
                    }
                    else {
                        $scope.selectPage(currentPage);
                    }
                }
            },
            link: function (scope, element, attrs) {
                scope.$watch('currentPage', function (c) {
                    scope.inputPage = c;
                });
            }
        }
    })

    .filter('isQID', function () {
        return function (input, QID) {
            var out = [];
            for (var i = 0; i < input.length; i++) {
                if (input[i].QuestionID == QID) {
                    for (var j = 0; j < input[i].File.length; j++) {
                        out.push(input[i].File[j]);
                    }
                    break;
                }
            }
            return out;
        };
    })

    .directive('ngMouseWheel', ['$document', function ($document) {
        return function (scope, element, attrs) {
            element.bind("DOMMouseScroll onwheel mousewheel onmousewheel", function (event) {

                // cross-browser wheel delta
                var event = window.event || event; // old IE support
                var delta = Math.max(-1, Math.min(1, (event.wheelDelta || -event.detail)));

                if (delta > 0) {
                    scope.$apply(function () {
                        scope.scrollup();
                    });
                }
                else {
                    scope.$apply(function () {
                        scope.scrolldown();
                    });
                }
                // for IE
                event.returnValue = false;
                // for Chrome and Firefox
                if (event.preventDefault) {
                    event.preventDefault();
                }
            });
        };
    }])

    .constant("Max_Size", 10)

    .directive('stRatio', function () {
        return {
            link: function (scope, element, attr) {
                var ratio = +(attr.stRatio);

                element.css('width', ratio + '%');

            }
        };
    })

    .factory('Resource1', ['$q', '$rootScope', '$filter', '$timeout', function ($q, $rootScope, $filter, $timeout) {
        function getPage(start, number, params) {

            var deferred = $q.defer();

            $rootScope.filtered = params.search.predicateObject ? $filter('filter')($rootScope.randomsItems, params.search.predicateObject) : $rootScope.randomsItems;

            if (params.sort.predicate) {
                $rootScope.filtered = $filter('orderBy')($rootScope.filtered, params.sort.predicate, params.sort.reverse);
            }
            var result = $rootScope.filtered.slice(start, start + number);
            console.log($rootScope.filtered.length);
            console.log($rootScope.filtered);
            $timeout(function () {
                //note, the server passes the information about the data set size
                deferred.resolve({
                    data: result,
                    numberOfPages: Math.ceil($rootScope.filtered.length / number)
                });
            }, 500);
            return deferred.promise;
        }
        return {
            getPage: getPage
        };

    }])

    .factory('Resource', ['$q', '$rootScope', '$filter', '$timeout', function ($q, $rootScope, $filter, $timeout) {

        function getPage(start, number, params) {

            var deferred = $q.defer();

            $rootScope.filtered = params.search.predicateObject ? $filter('filter')($rootScope.trackerItems1, params.search.predicateObject) : $rootScope.trackerItems1;

            if (params.sort.predicate) {
                $rootScope.filtered = $filter('orderBy')($rootScope.filtered, params.sort.predicate, params.sort.reverse);
            }

            var result = $rootScope.filtered.slice(start, start + number);

            $timeout(function () {
                deferred.resolve({
                    data: result,
                    numberOfPages: Math.ceil($rootScope.filtered.length / number)
                });
            });
            return deferred.promise;
        }

        return {
            getPage: getPage
        };

    }]);

    Manager.$inject = ['$rootScope', '$state', '$stateParams', 'httpq'];

    function Manager($rootScope, $state, $stateParams, httpq) {
        var service = {};

        $rootScope.CurrentUser = {};
        //-------------- Current User and Role ---------------------
        //httpq.get(rootDir + '/ManageUserRole/GetCurrentUser').then(function (data) {
        //    $rootScope.CurrentUser = JSON.parse(data.CurrentUser);
        //    $rootScope.Role = _.toLower($rootScope.CurrentUser.UserRole);
        //    $rootScope.UserFullName = _.toLower($rootScope.CurrentUser.UserFullName);

        //});
        //---------------- END Current User and Role ------------------------

        return service;
    }
})();


//------------------- Form Reset Function ------------------------
var FormReset = function ($form) {

    // get validator object
    var $validator = $form.validate();

    // get errors that were created using jQuery.validate.unobtrusive
    var $errors = $form.find(".field-validation-error span");

    // trick unobtrusive to think the elements were successfully validated
    // this removes the validation messages
    $errors.each(function () {
        $validator.settings.success($(this));
    });
    // clear errors from validation
    $validator.resetForm();
};

var ConvertDateFormat = function (value) {
    if (value) {
        return new Date(parseInt(value.replace("/Date(", "").replace(")/", ""), 10));
    } else {
        return value;
    }
};
var ConvertDateFormats1 = function (value) {
    var returnValue = value;
    try {
        if (value.indexOf("/Date(") == 0) {
            returnValue = new Date(parseInt(value.replace("/Date(", "").replace(")/", ""), 10));
        }
        return returnValue;
    } catch (e) {
        return returnValue;
    }
    return returnValue;
};

function ResetFormForValidation(form) {
    form.removeData('validator');
    form.removeData('unobtrusiveValidation');
    $.validator.unobtrusive.parse(form);

}

var loadingOn = function () {
    $("#loading").show();
}

var loadingOff = function () {
    $("#loading").hide();
}

$(document).on({
    ajaxStart: function () {
        $("#loading").show();
    },

    ajaxStop: function () {
        $("#loading").hide();
    }
});

$(function () {
    $(document).ajaxError(function (e, xhr) {
        if (xhr.status == 403) {
            var response = $.parseJSON(xhr.responseText);
            window.location.href = response.LogOnUrl;
            document.getElementById('logoutForm').submit();
        }
    });
});

//================================= Hide All country code popover =========================
$(document).click(function (event) {

    if (!$(event.target).hasClass("countryCodeClass") && $(event.target).parents(".countryDailCodeContainer").length === 0) {
        $(".countryDailCodeContainer").hide();
    }
    if (!$(event.target).hasClass("form-control") && $(event.target).parents(".LanguageSelectAutoList").length === 0) {
        $(".LanguageSelectAutoList").hide();
    }
    if (!$(event.target).hasClass("form-control") && $(event.target).parents(".ProviderTypeSelectAutoList").length === 0) {
        $(".ProviderTypeSelectAutoList").hide();
    }
    if (!$(event.target).attr("data-searchdropdown") && $(event.target).parents(".ProviderTypeSelectAutoList1").length === 0) {
        $(".ProviderTypeSelectAutoList1").hide();
    }
});

//Method to change the visiblity of country code popover
var changeVisibilityOfCountryCode = function () {
    $(".countryDailCodeContainer").hide();
    // method will close any other country code div already open.
};

$(document).ready(function () {
    $(".countryDailCodeContainer").hide();
    $(".ProviderTypeSelectAutoList").hide();

});

function showLocationList(ele) {
    $(ele).parent().find(".ProviderTypeSelectAutoList").first().show();
}

$(document).ready(function () {
    $("#EnterState").keydown(function (event) {
        $(this).next();
    });
});

function showLocationList(ele) {
    $(ele).parent().find(".ProviderTypeSelectAutoList").first().show();
}

$(document).ready(function () {
    $('#btn-upload0').click(function (e) {
        e.preventDefault();
        $('#file0').click();
    }
    );
});

$(document).ready(function () {
    $("#sidemenu").addClass("menu-in");
    $("#page-wrapper").addClass("menuup");
});
