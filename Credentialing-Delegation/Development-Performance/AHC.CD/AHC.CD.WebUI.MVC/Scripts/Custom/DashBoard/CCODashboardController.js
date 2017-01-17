
//------------------------------- Provider License Service --------------------




dashboardApp.service('CCOLicenseService', function ($filter) {

    var ProviderState = [];
    var ProviderDEA = [];
    var ProviderCDS = [];
    var ProviderSpeciality = [];
    var ProviderHospital = [];
    var ProviderLiability = [];
    var ProviderWorker = [];


    //---------------- parse data in license have Licenses with calculate Left Day ----------------------
    for (var i in expiredLicenses) {
        if (expiredLicenses[i].StateLicenseExpiries && expiredLicenses[i].StateLicenseExpiries.length > 0) {
            for (var j in expiredLicenses[i].StateLicenseExpiries) {
                expiredLicenses[i].StateLicenseExpiries[j].ExpiryDate = ConvertDateFormat(expiredLicenses[i].StateLicenseExpiries[j].ExpiryDate);
                expiredLicenses[i].StateLicenseExpiries[j].dayLeft = GetRenewalDayLeft(expiredLicenses[i].StateLicenseExpiries[j].ExpiryDate);

                ProviderState.push({
                    EmailAddress: expiredLicenses[i].EmailAddress,
                    ExpiryNotificationDetailID: expiredLicenses[i].ExpiryNotificationDetailID,
                    FirstName: expiredLicenses[i].FirstName,
                    LastName: expiredLicenses[i].LastName,
                    MiddleName: expiredLicenses[i].MiddleName,
                    LastModifiedDate: expiredLicenses[i].LastModifiedDate,
                    NPINumber: expiredLicenses[i].NPINumber,
                    ProfileId: expiredLicenses[i].ProfileId,
                    ProviderLevel: expiredLicenses[i].ProviderLevel,
                    ProviderTitles: expiredLicenses[i].ProviderTitles,
                    ProviderTypes: expiredLicenses[i].ProviderTypes,
                    License: expiredLicenses[i].StateLicenseExpiries[j]
                });
            }
        }

        if (expiredLicenses[i].DEALicenseExpiries && expiredLicenses[i].DEALicenseExpiries.length > 0) {
            for (var j in expiredLicenses[i].DEALicenseExpiries) {
                expiredLicenses[i].DEALicenseExpiries[j].ExpiryDate = ConvertDateFormat(expiredLicenses[i].DEALicenseExpiries[j].ExpiryDate);
                expiredLicenses[i].DEALicenseExpiries[j].dayLeft = GetRenewalDayLeft(expiredLicenses[i].DEALicenseExpiries[j].ExpiryDate);

                ProviderDEA.push({
                    EmailAddress: expiredLicenses[i].EmailAddress,
                    ExpiryNotificationDetailID: expiredLicenses[i].ExpiryNotificationDetailID,
                    FirstName: expiredLicenses[i].FirstName,
                    LastName: expiredLicenses[i].LastName,
                    MiddleName: expiredLicenses[i].MiddleName,
                    LastModifiedDate: expiredLicenses[i].LastModifiedDate,
                    NPINumber: expiredLicenses[i].NPINumber,
                    ProfileId: expiredLicenses[i].ProfileId,
                    ProviderLevel: expiredLicenses[i].ProviderLevel,
                    ProviderTitles: expiredLicenses[i].ProviderTitles,
                    ProviderTypes: expiredLicenses[i].ProviderTypes,
                    License: expiredLicenses[i].DEALicenseExpiries[j]
                });
            }
        }

        if (expiredLicenses[i].CDSCInfoExpiries && expiredLicenses[i].CDSCInfoExpiries.length > 0) {
            for (var j in expiredLicenses[i].CDSCInfoExpiries) {
                expiredLicenses[i].CDSCInfoExpiries[j].ExpiryDate = ConvertDateFormat(expiredLicenses[i].CDSCInfoExpiries[j].ExpiryDate);
                expiredLicenses[i].CDSCInfoExpiries[j].dayLeft = GetRenewalDayLeft(expiredLicenses[i].CDSCInfoExpiries[j].ExpiryDate);

                ProviderCDS.push({
                    EmailAddress: expiredLicenses[i].EmailAddress,
                    ExpiryNotificationDetailID: expiredLicenses[i].ExpiryNotificationDetailID,
                    FirstName: expiredLicenses[i].FirstName,
                    LastName: expiredLicenses[i].LastName,
                    MiddleName: expiredLicenses[i].MiddleName,
                    LastModifiedDate: expiredLicenses[i].LastModifiedDate,
                    NPINumber: expiredLicenses[i].NPINumber,
                    ProfileId: expiredLicenses[i].ProfileId,
                    ProviderLevel: expiredLicenses[i].ProviderLevel,
                    ProviderTitles: expiredLicenses[i].ProviderTitles,
                    ProviderTypes: expiredLicenses[i].ProviderTypes,
                    License: expiredLicenses[i].CDSCInfoExpiries[j]
                });
            }
        }

        if (expiredLicenses[i].SpecialtyDetailExpiries && expiredLicenses[i].SpecialtyDetailExpiries.length > 0) {
            for (var j in expiredLicenses[i].SpecialtyDetailExpiries) {
                expiredLicenses[i].SpecialtyDetailExpiries[j].ExpiryDate = ConvertDateFormat(expiredLicenses[i].SpecialtyDetailExpiries[j].ExpiryDate);
                expiredLicenses[i].SpecialtyDetailExpiries[j].dayLeft = GetRenewalDayLeft(expiredLicenses[i].SpecialtyDetailExpiries[j].ExpiryDate);

                ProviderSpeciality.push({
                    EmailAddress: expiredLicenses[i].EmailAddress,
                    ExpiryNotificationDetailID: expiredLicenses[i].ExpiryNotificationDetailID,
                    FirstName: expiredLicenses[i].FirstName,
                    LastName: expiredLicenses[i].LastName,
                    MiddleName: expiredLicenses[i].MiddleName,
                    LastModifiedDate: expiredLicenses[i].LastModifiedDate,
                    NPINumber: expiredLicenses[i].NPINumber,
                    ProfileId: expiredLicenses[i].ProfileId,
                    ProviderLevel: expiredLicenses[i].ProviderLevel,
                    ProviderTitles: expiredLicenses[i].ProviderTitles,
                    ProviderTypes: expiredLicenses[i].ProviderTypes,
                    License: expiredLicenses[i].SpecialtyDetailExpiries[j]
                });
            }
        }

        if (expiredLicenses[i].HospitalPrivilegeExpiries && expiredLicenses[i].HospitalPrivilegeExpiries.length > 0) {
            for (var j in expiredLicenses[i].HospitalPrivilegeExpiries) {
                expiredLicenses[i].HospitalPrivilegeExpiries[j].AffiliationEndDate = ConvertDateFormat(expiredLicenses[i].HospitalPrivilegeExpiries[j].AffiliationEndDate);
                expiredLicenses[i].HospitalPrivilegeExpiries[j].dayLeft = GetRenewalDayLeft(expiredLicenses[i].HospitalPrivilegeExpiries[j].AffiliationEndDate);

                ProviderHospital.push({
                    EmailAddress: expiredLicenses[i].EmailAddress,
                    ExpiryNotificationDetailID: expiredLicenses[i].ExpiryNotificationDetailID,
                    FirstName: expiredLicenses[i].FirstName,
                    LastName: expiredLicenses[i].LastName,
                    MiddleName: expiredLicenses[i].MiddleName,
                    LastModifiedDate: expiredLicenses[i].LastModifiedDate,
                    NPINumber: expiredLicenses[i].NPINumber,
                    ProfileId: expiredLicenses[i].ProfileId,
                    ProviderLevel: expiredLicenses[i].ProviderLevel,
                    ProviderTitles: expiredLicenses[i].ProviderTitles,
                    ProviderTypes: expiredLicenses[i].ProviderTypes,
                    License: expiredLicenses[i].HospitalPrivilegeExpiries[j]
                });
            }
        }

        if (expiredLicenses[i].ProfessionalLiabilityExpiries && expiredLicenses[i].ProfessionalLiabilityExpiries.length > 0) {
            for (var j in expiredLicenses[i].ProfessionalLiabilityExpiries) {
                expiredLicenses[i].ProfessionalLiabilityExpiries[j].ExpirationDate = ConvertDateFormat(expiredLicenses[i].ProfessionalLiabilityExpiries[j].ExpirationDate);
                expiredLicenses[i].ProfessionalLiabilityExpiries[j].dayLeft = GetRenewalDayLeft(expiredLicenses[i].ProfessionalLiabilityExpiries[j].ExpirationDate);

                ProviderLiability.push({
                    EmailAddress: expiredLicenses[i].EmailAddress,
                    ExpiryNotificationDetailID: expiredLicenses[i].ExpiryNotificationDetailID,
                    FirstName: expiredLicenses[i].FirstName,
                    LastName: expiredLicenses[i].LastName,
                    MiddleName: expiredLicenses[i].MiddleName,
                    LastModifiedDate: expiredLicenses[i].LastModifiedDate,
                    NPINumber: expiredLicenses[i].NPINumber,
                    ProfileId: expiredLicenses[i].ProfileId,
                    ProviderLevel: expiredLicenses[i].ProviderLevel,
                    ProviderTitles: expiredLicenses[i].ProviderTitles,
                    ProviderTypes: expiredLicenses[i].ProviderTypes,
                    License: expiredLicenses[i].ProfessionalLiabilityExpiries[j]
                });
            }
        }

        if (expiredLicenses[i].WorkerCompensationExpiries && expiredLicenses[i].WorkerCompensationExpiries.length > 0) {
            for (var j in expiredLicenses[i].WorkerCompensationExpiries) {
                expiredLicenses[i].WorkerCompensationExpiries[j].ExpirationDate = ConvertDateFormat(expiredLicenses[i].WorkerCompensationExpiries[j].ExpirationDate);
                expiredLicenses[i].WorkerCompensationExpiries[j].dayLeft = GetRenewalDayLeft(expiredLicenses[i].WorkerCompensationExpiries[j].ExpirationDate);

                ProviderWorker.push({
                    EmailAddress: expiredLicenses[i].EmailAddress,
                    ExpiryNotificationDetailID: expiredLicenses[i].ExpiryNotificationDetailID,
                    FirstName: expiredLicenses[i].FirstName,
                    LastName: expiredLicenses[i].LastName,
                    MiddleName: expiredLicenses[i].MiddleName,
                    LastModifiedDate: expiredLicenses[i].LastModifiedDate,
                    NPINumber: expiredLicenses[i].NPINumber,
                    ProfileId: expiredLicenses[i].ProfileId,
                    ProviderLevel: expiredLicenses[i].ProviderLevel,
                    ProviderTitles: expiredLicenses[i].ProviderTitles,
                    ProviderTypes: expiredLicenses[i].ProviderTypes,
                    License: expiredLicenses[i].WorkerCompensationExpiries[j]
                });
            }
        }
    }

    //-------------------------- Customm array parse ---------------------
    var LicenseData = [];
    var GrandTotalLicenses = 0;

    if (ProviderState) {
        LicenseData.push({
            LicenseType: "State License",
            LicenseTypeCode: "StateLicense",
            Licenses: ProviderState,
            Show: true
        });
    }
    if (ProviderDEA) {
        LicenseData.push({
            LicenseType: "Federal DEA",
            LicenseTypeCode: "FederalDEA",
            Licenses: ProviderDEA,
            Show: true
        });
    }
    if (ProviderCDS) {
        LicenseData.push({
            LicenseType: "CDS Information",
            LicenseTypeCode: "CDSInformation",
            Licenses: ProviderCDS,
            Show: true
        });
    }
    if (ProviderSpeciality) {
        LicenseData.push({
            LicenseType: "Specialty/Board",
            LicenseTypeCode: "SpecialityBoard",
            Licenses: ProviderSpeciality,
            Show: true
        });
    }
    if (ProviderHospital) {
        LicenseData.push({
            LicenseType: "Hospital Privileges",
            LicenseTypeCode: "HospitalPrivilages",
            Licenses: ProviderHospital,
            Show: true
        });
    }
    if (ProviderLiability) {
        LicenseData.push({
            LicenseType: "Professional Liability",
            LicenseTypeCode: "ProfessionalLiability",
            Licenses: ProviderLiability,
            Show: true
        });
    }
    if (ProviderWorker) {
        LicenseData.push({
            LicenseType: "Worker Compensation",
            LicenseTypeCode: "WorkerCompensation",
            Licenses: ProviderWorker,
            Show: true
        });
    }

    //--------------- Master license Data for Static Data ---------------
    var MasterLicenseData = angular.copy(LicenseData);
    //---------------------- license status return -------------------
    this.GetLicenseStatus = function (data) {
        GrandTotalLicenses = 0;

        for (var i in data) {

            var ValidatedLicense = 0;
            var dayLeftLicense = 0;
            var ExpiredLicense = 0;

            var TotalLicenses = 0;

            for (var j in data[i].Licenses) {
                TotalLicenses++;
                if (data[i].Licenses[j].License.dayLeft < 0) {
                    ExpiredLicense++;
                    GrandTotalLicenses++;
                } else if (data[i].Licenses[j].License.dayLeft < 90) {
                    dayLeftLicense++;
                    GrandTotalLicenses++;
                }
                else if (data[i].Licenses[j].License.dayLeft < 180) {
                    ValidatedLicense++;
                    GrandTotalLicenses++;
                }
            }

            var orderBy = $filter('orderBy');
            data[i].Licenses = orderBy(data[i].Licenses, 'License.dayLeft', false);

            data[i].LicenseStatus = {
                ValidLicense: ValidatedLicense,
                PendingDaylicense: dayLeftLicense,
                ExpiredLicense: ExpiredLicense
            };
            data[i].TotalLicenses = TotalLicenses;
        }
    };

    //------------------ Grand Total Number of License return ---------------------
    this.GetGrandTotalLicenses = function () {
        return GrandTotalLicenses;
    };

    //----------------- simply return Licese List ---------------
    this.LicensesList = function () {
        this.GetLicenseStatus(LicenseData);
        return LicenseData;
    };
    //----------------- License by day lefts ------------------------
    this.GetLicenseByDayLeft = function (days) {
        if (days > 0) {
            var temp = [];
            for (var i in MasterLicenseData) {
                var licenses = [];
                for (var j in MasterLicenseData[i].Licenses) {
                    if (MasterLicenseData[i].Licenses[j].License.dayLeft <= days) {
                        licenses.push(MasterLicenseData[i].Licenses[j]);
                    }
                }
                temp.push({ LicenseType: MasterLicenseData[i].LicenseType, LicenseTypeCode: MasterLicenseData[i].LicenseTypeCode, Licenses: licenses });
            }
            this.GetLicenseStatus(temp);
            return temp
        }
        else {
            this.GetLicenseStatus(MasterLicenseData);
            return angular.copy(MasterLicenseData);
        }
    };

    //----------------- License by Provider Types lefts ------------------------
    this.GetLicenseByProviderType = function (providertype, providerlevel, masterLicenses) {
        //--------------- provider type undefined -----------
        if (typeof providertype === "undefined") {
            providertype = "";
        }
        //--------------- provider Level undefined -----------
        if (typeof providerlevel === "undefined") {
            providerlevel = "";
        }
        if (providertype != "" || providerlevel != "") {
            if (providertype != "" && providerlevel != "") {
                var temp = [];
                for (var i in masterLicenses) {
                    var status = false;
                    for (var j in masterLicenses[i].ProviderTitles) {
                        if (masterLicenses[i].ProviderTitles[j] == providertype && masterLicenses[i].ProviderLevel == providerlevel) {
                            status = true;
                        }
                    }
                    if (status) {
                        temp.push(masterLicenses[i]);
                    }
                }
                return temp;
            } else if (providertype != "" && providerlevel == "") {
                var temp = [];
                for (var i in masterLicenses) {
                    var status = false;
                    for (var j in masterLicenses[i].ProviderTitles) {
                        if (masterLicenses[i].ProviderTitles[j] == providertype) {
                            status = true;
                        }
                    }
                    if (status) {
                        temp.push(masterLicenses[i]);
                    }
                }
                return temp;
            } else if (providertype == "" && providerlevel != "") {
                var temp = [];
                for (var i in masterLicenses) {
                    if (masterLicenses[i].ProviderLevel == providerlevel) {
                        temp.push(masterLicenses[i]);
                    }
                }
                return temp;
            }
        }
        else {
            return masterLicenses;
        }
    };
});

//------------------- CCO Dashboard Controller --------------------
dashboardApp.controller("CCODashboardController", ["$scope", "$http", "$filter", "CCOLicenseService", function ($scope, $http, $filter, CCOLicenseService) {

    function loadPMDIMap() {
        //<![CDATA[

        if (GBrowserIsCompatible()) {

            var polys = [];
            var labels = [];
            var availableProviders = [];
            var availableMembers = [];


            // === A method for testing if a point is inside a polygon
            // === Returns true if poly contains point
            // === Algorithm shamelessly stolen from http://alienryderflex.com/polygon/
            GPolygon.prototype.Contains = function (point) {
                var j = 0;
                var oddNodes = false;
                var x = point.lng();
                var y = point.lat();
                for (var i = 0; i < this.getVertexCount() ; i++) {
                    j++;
                    if (j == this.getVertexCount()) { j = 0; }
                    if (((this.getVertex(i).lat() < y) && (this.getVertex(j).lat() >= y))
                    || ((this.getVertex(j).lat() < y) && (this.getVertex(i).lat() >= y))) {
                        if (this.getVertex(i).lng() + (y - this.getVertex(i).lat())
                         / (this.getVertex(j).lat() - this.getVertex(i).lat())
                         * (this.getVertex(j).lng() - this.getVertex(i).lng()) < x) {
                            oddNodes = !oddNodes
                        }
                    }
                }
                return oddNodes;
            }



            // Display the map, with some controls and set the initial location
            PMDIMap = new GMap2(document.getElementById("PMDImap"));
            PMDIMap.addControl(new GLargeMapControl());
            PMDIMap.addControl(new GMapTypeControl());
            PMDIMap.setCenter(new GLatLng(28.522432, -81.378373), 7);


            GEvent.addListener(PMDIMap, "click", function (overlay, point) {
                if (!overlay) {
                    for (var i = 0; i < polys.length; i++) {
                        if (polys[i].Contains(point)) {
                            PMDIMap.openInfoWindowHtml(point, "<h5>County: " + labels[i] + "</h5><h6>Members Available : " + availableMembers[i] + "<h6>Providers Available : " + availableProviders[i] + "</h6><h6>Ratio for 100 Members : " + (parseInt(availableProviders[i]) / parseInt(availableMembers[i]) * 100).toFixed(2) + "</h6>");
                            //i = 999; // Jump out of loop
                        }
                    }
                }
            });


            colors = ['#ff4d4d', '#ff1a1a', '#e60000', '#ffff66', '#ffff33', '#ffff00', '#85e085', '#5cd65c', '#33cc33', '#29a329'];
            // Read the data from states.xml

            var request = GXmlHttp.create();
            request.open("GET", "states.xml", true);
            request.onreadystatechange = function () {
                if (request.readyState == 4) {
                    var xmlDoc = GXml.parse(request.responseText);

                    // ========= Now process the polylines ===========
                    var states = xmlDoc.documentElement.getElementsByTagName("county");

                    // read each line
                    for (var a = 0; a < states.length; a++) {
                        // get any state attributes
                        var label = states[a].getAttribute("name");
                        var members = states[a].getAttribute("members");
                        var providers = states[a].getAttribute("providers");
                        var colour = colors[Math.floor(((parseInt(members)) / 100)) - 1];

                        // read each point on that line
                        var points = states[a].getElementsByTagName("point");
                        var pts = [];
                        for (var i = 0; i < points.length; i++) {
                            pts[i] = new GLatLng(parseFloat(points[i].getAttribute("lat")),
                                                parseFloat(points[i].getAttribute("lng")));
                        }
                        var poly = new GPolygon(pts, "rgba(100,100,100,100)", 1, 1, colour, 0.5, { clickable: false });
                        polys.push(poly);
                        labels.push(label);
                        availableProviders.push(providers);
                        availableMembers.push(members)
                        PMDIMap.addOverlay(poly);
                    }
                    // ================================================
                }
            }
            request.send(null);

        }

            // display a warning if the browser was not compatible
        else {
            alert("Sorry, the Google Maps API is not compatible with this browser");
        }

        // This Javascript is based on code provided by the
        // Community Church Javascript Team
        // http://www.bisphamchurch.org.uk/
        // http://econym.org.uk/gmap/

        //]]>
    }
    function LoadSpecificCountyOnMap(county) {
        var latlng;

        switch (county) {
            case 'DeSoto-FL':
                latlng = new GLatLng(27.525315, -82.6409317);
                PMDIMap.setCenter(latlng, 15);
                GEvent.trigger(PMDIMap, "click", null, latlng, null);
                break;
            case 'Duval-FL':
                latlng = new GLatLng(30.355357, -81.687336);
                PMDIMap.setCenter(latlng, 11);
                GEvent.trigger(PMDIMap, "click", null, latlng, null);
                break;
            case 'Escambia-FL':
                latlng = new GLatLng(30.711093, -87.369150);
                PMDIMap.setCenter(latlng, 11);
                GEvent.trigger(PMDIMap, "click", null, latlng, null);
                break;
            case 'Flagler-FL':
                latlng = new GLatLng(29.460826, -81.306052);
                PMDIMap.setCenter(latlng, 11);
                GEvent.trigger(PMDIMap, "click", null, latlng, null);
                break;
            case 'Franklin-FL':
                latlng = new GLatLng(29.865359, -84.796044);
                PMDIMap.setCenter(latlng, 11);
                GEvent.trigger(PMDIMap, "click", null, latlng, null);
                break;
            case 'Gadsden-FL':
                latlng = new GLatLng(30.585881, -84.622691);
                PMDIMap.setCenter(latlng, 11);
                GEvent.trigger(PMDIMap, "click", null, latlng, null);
                break;
            case 'Gilchrist-FL':
                latlng = new GLatLng(29.725153, -82.803684);
                PMDIMap.setCenter(latlng, 11);
                GEvent.trigger(PMDIMap, "click", null, latlng, null);
                break;
            case 'Glades-FL':
                latlng = new GLatLng(26.952561, -81.190665);
                PMDIMap.setCenter(latlng, 11);
                GEvent.trigger(PMDIMap, "click", null, latlng, null);
                break;
            case 'Gulf-FL':
                latlng = new GLatLng(29.966892, -85.213700);
                PMDIMap.setCenter(latlng, 11);
                GEvent.trigger(PMDIMap, "click", null, latlng, null);
                break;
            case 'Hardee-FL':
                latlng = new GLatLng(27.504398, -81.825332);
                PMDIMap.setCenter(latlng, 11);
                GEvent.trigger(PMDIMap, "click", null, latlng, null);
                break;
            case 'Washington-FL':
                latlng = new GLatLng(30.606090, -85.667702);
                PMDIMap.setCenter(latlng, 11);
                GEvent.trigger(PMDIMap, "click", null, latlng, null);
                break;
            default:
                PMDIMap.setCenter(new GLatLng(28.522432, -81.378373), 7);

        };
    }


    $(document).on('input', '#PMDIMapSearch', function () {
        var options = $('datalist')[0].options;
        var county = $(this).val();
        LoadSpecificCountyOnMap(county);
    });

    loadPMDIMap();
    $('#container1').highcharts({
        chart: {
            type: 'column'
        },
        title: {
            text: 'Expiry License Renewal'
        },
        subtitle: {
            text: ''
        },
        xAxis: {
            categories: [
                '15 Days',
                '30 Days',
                '45 Days',
                '60 Days',
                '90 Days',
                '120 Days',
                '135 Days',
                '150 Days',
                '180 Days'
            ],
            crosshair: true
        },
        yAxis: {
            min: 0,
            title: {
                text: 'NO. Of Provider'
            }
        },
        tooltip: {
            headerFormat: '<span style="font-size:10px">{point.key}</span><table>',
            pointFormat: '<tr><td style="color:{series.color};padding:0">{series.name}: </td>' +
                '<td style="padding:0"><b>{point.y:.0f}</b></td></tr>',
            footerFormat: '</table>',
            shared: true,
            useHTML: true
        },
        plotOptions: {
            column: {
                pointPadding: 0.2,
                borderWidth: 0
            }
        },
        series: [{
            name: 'State License',
            data: [49, 71, 106, 129, 144, 176, 135, 148, 10]

        }, {
            name: 'Hospital Privileges',
            data: [83, 78, 98, 93, 106, 84, 105, 104, 91]

        }, {
            name: 'Professional Liability',
            data: [48, 38, 39, 41, 47, 48, 59, 59, 52]

        }, {
            name: 'Specialty/Board',
            data: [42, 33, 34, 39, 52, 75, 57, 60, 47]

        }, {
            name: 'Federal DEA',
            data: [42, 33, 34, 39, 52, 33, 34, 39, 52]

        }, {
            name: 'CDSC Information',
            data: [42, 33, 34, 39, 52, 39, 52, 60, 47]

        }, {
            name: 'Worker CompenSation',
            data: [42, 33, 34, 39, 52, 75, 57, 60, 47]

        }]
    });
   

    $('#container2').highcharts({
        chart: {
            type: 'spline'
        },
        title: {
            text: 'Expiry License Renewal'
        },
        subtitle: {
            text: ''
        },
        xAxis: {
            categories: [
                '15 Days',
                '30 Days',
                '45 Days',
                '60 Days',
                '90 Days',
                '120 Days',
                '135 Days',
                '150 Days',
                '180 Days'
            ],
            crosshair: true
        },
        yAxis: {
            min: 0,
            title: {
                text: 'NO. Of Provider'
            }
        },
        tooltip: {
            headerFormat: '<span style="font-size:10px">{point.key}</span><table>',
            pointFormat: '<tr><td style="color:{series.color};padding:0">{series.name}: </td>' +
                '<td style="padding:0"><b>{point.y:.0f}</b></td></tr>',
            footerFormat: '</table>',
            shared: true,
            useHTML: true
        },
        plotOptions: {
            column: {
                pointPadding: 0.2,
                borderWidth: 0
            }
        },
        series: [{
            name: 'State License',
            data: [49, 71, 106, 129, 144, 176, 135, 148, 10]

        }, {
            name: 'Hospital Privileges',
            data: [83, 78, 98, 93, 106, 84, 105, 104, 91]

        }, {
            name: 'Professional Liability',
            data: [48, 38, 39, 41, 47, 48, 59, 59, 52]

        }, {
            name: 'Specialty/Board',
            data: [42, 33, 34, 39, 52, 75, 57, 60, 47]

        }, {
            name: 'Federal DEA',
            data: [42, 33, 34, 39, 52, 33, 34, 39, 52]

        }, {
            name: 'CDSC Information',
            data: [42, 33, 34, 39, 52, 39, 52, 60, 47]

        }, {
            name: 'Worker CompenSation',
            data: [42, 33, 34, 39, 52, 75, 57, 60, 47]

        }]
    });

    $scope.printData = function (str) {

        if (str == 'All') {
            $scope.printAllPagesDataById($scope.LicenseType);
        }
        else if (str == 'Current') {
            $scope.printCurrentPageDataById($scope.LicenseType);
        }
    }

    $scope.tempObj = [];

    $scope.newSelection = function (index, status) {

        var present = false;

        if ($scope.tempObj != null && $scope.tempObj.length != 0) {

            for (var i = 0; i < $scope.tempObj.length; i++) {

                if ($scope.tempObj[i] == index) {

                    present = true;
                    break;

                }

            }

        }

        if (!present) {

            $scope.tempObj.push(index);

        } else {

            $scope.tempObj.splice($scope.tempObj.indexOf(index), 1);

        }

    }

    $scope.saveIt = function () {

        //sessionStorage.setItem('myTabs', JSON.stringify($scope.LicenseData));
        $scope.tempObj = [];
        $scope.controlpanel = false;

    }

    $scope.revertIt = function () {

        if ($scope.tempObj != null && $scope.tempObj.length != 0) {

            for (var i = 0; i < $scope.tempObj.length; i++) {

                if ($scope.LicenseData[$scope.tempObj[i]].Show == true) {

                    $scope.LicenseData[$scope.tempObj[i]].Show = false;

                } else {

                    $scope.LicenseData[$scope.tempObj[i]].Show = true;

                }

            }

        }

        $scope.tempObj = [];

        $scope.controlpanel = false;

        //for (var i = 0; i < $scope.LicenseData.length; i++) {

        //    $scope.LicenseData[i].Show = true;
        //    $scope.controlpanel = false;

        //}


    }



    //$scope.exportToExcel = function () {

    //    if (str == 'All') {
    //        $scope.exportToExcelAllData($scope.LicenseType);
    //    }
    //    else if (str == 'Current') {
    //        $scope.printCurrentPageDataById($scope.LicenseType);
    //    }
    //}

    $scope.exportToExcel = function () {
       
        var divToPrint = document.getElementById($scope.LicenseType);
        $('#hiddenPrintDiv').empty();
        $('#hiddenPrintDiv').append("<table>"+divToPrint.innerHTML+"</table>");
        $('#hiddenPrintDiv thead:last-child,#hiddenPrintDiv th:last-child, #hiddenPrintDiv td:last-child').remove();
        $('#hiddenPrintDiv table').attr("id", "exportTable");
        $('#hiddenPrintDiv').attr("download", "ExportToExcel.xls");
        $('#exportTable').tableExport({ type: 'excel', escape: 'false' }, $scope.LicenseType);
    }

    $scope.printAllPagesDataById = function (id) {
        var divToPrint = document.getElementById(id);

        $('#hiddenPrintDiv').empty();
        
        $('#hiddenPrintDiv').append(divToPrint.innerHTML);
        // Removing the last column of the table
        $('#hiddenPrintDiv thead:last-child,#hiddenPrintDiv th:last-child, #hiddenPrintDiv td:last-child').remove();
        // Creating a window for printing
        var mywindow = window.open('', $('#hiddenPrintDiv').html(), 'height=800,width=800');
        mywindow.document.write('<center><b style="font-size:large">' + id + '</b></center></br>');
        mywindow.document.write('<html><head><title>'+id+'</title>');
        mywindow.document.write('<link rel="stylesheet" href="/Content/SharedCss/bootstrap.min.css" type="text/css" />');
        mywindow.document.write('<link rel="stylesheet" href="/Content/SharedCss/app.css" type="text/css" />');
        mywindow.document.write('<style>@page{size: auto;margin-bottom:5mm;margin-top:7mm;}th{text-align: center;}</style>');
        mywindow.document.write('</head><body style="background-color:white"><table class="table table-bordered"></td>');
        mywindow.document.write($('#hiddenPrintDiv').html());
        mywindow.document.write('</table></body></html>');      
        mywindow.document.close();
        mywindow.focus();
        setTimeout(function () {           
            mywindow.print();
            mywindow.close();
        }, 1000);
    }

    $scope.printCurrentPageDataById = function (id) {
        var divToPrint = document.getElementById(id);

        $('#hiddenPrintDiv').empty();

        $('#hiddenPrintDiv').append(divToPrint.innerHTML);
        // Removing the last column of the table
        $('#hiddenPrintDiv thead:last-child,#hiddenPrintDiv th:last-child, #hiddenPrintDiv td:last-child').remove();
        // Creating a window for printing
        var mywindow = window.open('', $('#hiddenPrintDiv').html(), 'height=800,width=800');
        mywindow.document.write('<center><b style="font-size:large">' + id + '</b></center></br>');
        mywindow.document.write('<html><head><title>' + id + '</title>');
        mywindow.document.write('<link rel="stylesheet" href="/Content/SharedCss/bootstrap.min.css" type="text/css" />');
        mywindow.document.write('<link rel="stylesheet" href="/Content/SharedCss/app.css" type="text/css" />');
        mywindow.document.write('<style>.ng-hide:not(.ng-hide-animate) {display: none !important;}@page{size: auto;margin-bottom:5mm;margin-top:7mm;}th{text-align: center;}</style>');
        mywindow.document.write('</head><body style="background-color:white"><table class="table table-bordered"></td>');
        mywindow.document.write($('#hiddenPrintDiv').html());
        mywindow.document.write('</table></body></html>');
        mywindow.document.close();
        mywindow.focus();
        setTimeout(function () {
            mywindow.print();
            mywindow.close();
        }, 1000);
    }

    //----------------------------------Download--------------------------------
    //$scope.downloadData = function () {
    //    $scope.downloadDataById($scope.LicenseType);
    //}

    //$scope.downloadDataById = function (id) {

    //    //--------First Method---------
    //    //var divToPrint = document.getElementById(id);
    //    //$('#hiddenPrintDiv').empty();
    //    //$('#hiddenPrintDiv').append(divToPrint.innerHTML);
    //    //$('#hiddenPrintDiv thead:last-child,#hiddenPrintDiv th:last-child, #hiddenPrintDiv td:last-child').remove();

    //    //var pdf = new jsPDF('p', 'pt', 'letter');
    //    //pdf.table(tableToJson(divToPrint));
    //    //pdf.save('sample-file.pdf');

    //    //function tableToJson(table) {
    //    //    var data = [];

    //    //    // first row needs to be headers
    //    //    var headers = [];
    //    //    for (var i = 0; i < table.rows[0].cells.length; i++) {
    //    //        headers[i] = table.rows[0].cells[i];
    //    //    }

    //    //    // go through cells
    //    //    for (var i = 1; i < table.rows.length; i++) {

    //    //        var tableRow = table.rows[i];
    //    //        var rowData = {};

    //    //        for (var j = 0; j < tableRow.cells.length; j++) {

    //    //            rowData[headers[j]] = tableRow.cells[j].innerHTML;

    //    //        }

    //    //        data.push(rowData);
    //    //    }
    //    //    return data;
    //    //}


    //    //-------Second Method-------
    //    var divToPrint = document.getElementById(id);
    //    $('#hiddenPrintDiv').empty();
    //    $('#hiddenPrintDiv').append(divToPrint.innerHTML);
    //    $('#hiddenPrintDiv thead:last-child,#hiddenPrintDiv th:last-child, #hiddenPrintDiv td:last-child').remove();

    //    var pdf = new jsPDF('p', 'pt', [594, 841]);
    //    var totalCols = $("#hiddenPrintDiv tr td").length;
    //    var cellWidth = 534 / totalCols;
    //    pdf.cellInitialize();
    //    pdf.setFontSize(10);
        
    //    $.each($('#hiddenPrintDiv tr'), function (i, row) {
    //        pdf.text(235, 30, id);
           
    //        $.each($(row).find("td, th"), function (j, cell) {
    //            var txt = $(cell).text().trim().split(" ").join("\n") || " ";
    //           // var width = (j == 4) ? 40 : 90; //make with column smaller
    //            var height = (i == 0) ? 40 : 30;
    //            pdf.cell(30, 50, cellWidth, height, txt, i);
    //        });
    //    });

    //    pdf.save(id+'.pdf');


        //--------Third Method--------
        //var divToPrint = document.getElementById(id);
        //var element = $(divToPrint).clone();
        //element.find('tr th:last-child, td:last-child').remove();
        //var source = element.html();
        //var pdf = new jsPDF('p', 'pt', 'letter');
        //var specialElementHandlers = {
        //    '#bypassme': function (element, renderer) {
        //        return true;
        //    }
        //};
        //margins = {
        //    top: 80,
        //    bottom: 60,
        //    left: 60,
        //    width: 700
        //};
        //pdf.text(235, 30, id);
        //pdf.fromHTML("<table>" + source + "</table>", // HTML string or DOM elem ref.
        //       margins.left, // x coord
        //       margins.top, { // y coord
        //           'width': margins.width, // max width of content on PDF
        //           'elementHandlers': specialElementHandlers
        //       },
        //       function (dispose) {

        //           pdf.save(id + '.pdf');
        //       },
        //    margins);

        

        //---------Fourth Method--------
        //var l = {
        //    orientation: 'l',
        //    unit: 'mm',
        //    format: 'a4',
        //    compress: true,
        //    fontSize: 8,
        //    lineHeight: 1,
        //    autoSize: false,
        //    printHeaders: true
        //}, pdf = new jsPDF(l, '', '', ''), i, j, margins = {
        //    top: 30,
        //    bottom: 10,
        //    left: 10,
        //    width: 25
        //};

        //var divToPrint = document.getElementById(id);
        //$('#hiddenPrintDiv').empty();
        //$('#hiddenPrintDiv').append(divToPrint.innerHTML);
        //$('#hiddenPrintDiv thead:last-child,#hiddenPrintDiv th:last-child, #hiddenPrintDiv td:last-child').remove();
        //var totalCols = $("#hiddenPrintDiv tr td").length;
        //var cellWidth = 534 / totalCols;
        ////initializing the cells
        //pdf.cellInitialize();

        //$.each($('#hiddenPrintDiv tr'), function (i, row) {
        //    $.each($(row).find("td, th"), function (j, cell) {
        //        var txt = $(cell).text().trim().split(" ").join("\n") || " ";
        //        // var width = (j == 4) ? 40 : 90; //make with column smaller
        //        var height = (i == 0) ? 40 : 30;
        //        pdf.cell(margins.left, margins.top, cellWidth, height, txt, i);
        //    });
        //});
        ////pdf.cell(margins.left, margins.top, 14, 8, lines, 0);

        //pdf.save('Te.pdf');

    







    var orderBy = $filter('orderBy');
    $scope.selectedSection = 6;

    //--------------- data show current page function -------------------
    $scope.GetCurrentPageData = function (data, pageNumber) {
        $scope.bigTotalItems = data.length;
        $scope.CurrentPageLicenses = [];
        $scope.bigCurrentPage = pageNumber;

        var startIndex = (pageNumber - 1) * 10;
        var endIndex = startIndex + 9;
        if (data) {
            for (startIndex; startIndex <= endIndex ; startIndex++) {
                if (data[startIndex]) {
                    $scope.CurrentPageLicenses.push(data[startIndex]);
                } else {
                    break;
                }
            }
        }
    };

    $scope.IsValidIndex = function (index) {

        var startIndex = ($scope.currentPageNumber - 1) * 10;
        var endIndex = startIndex + 9;
        
        if (index >= startIndex && index <= endIndex)
            return true;
        else
            return false;

    }

    //------------------- Master data comes from database ------------------------

    $http.get(rootDir + '/Profile/MasterData/GetAllProviderTypes').
      success(function (data, status, headers, config) {
          $scope.ProviderTypes = data;
      }).
      error(function (data, status, headers, config) {
      });

    $http.get(rootDir + '/Profile/MasterData/GetAllProviderLevels').
      success(function (data, status, headers, config) {
          $scope.ProviderLevels = data;
      }).
      error(function (data, status, headers, config) {
      });

    $scope.LicenseData = CCOLicenseService.LicensesList();
    $scope.GrandTotalLicenses = CCOLicenseService.GetGrandTotalLicenses();

    if ($scope.LicenseData.length > 0) {
        $scope.Licenses = $scope.LicenseData[0].Licenses;
        $scope.LicenseType = $scope.LicenseData[0].LicenseType;
        $scope.LicenseTypeCode = $scope.LicenseData[0].LicenseTypeCode;
        $scope.MasterLicenses = angular.copy($scope.Licenses);
        $scope.GetCurrentPageData($scope.Licenses, 1);
    }


    $scope.CurrentPageLicenses = [];

    //-------------------------- angular bootstrap pagger with custom-----------------
    $scope.maxSize = 5;
    $scope.bigTotalItems = 0;
    $scope.bigCurrentPage = 1;

    //-------------------- page change action ---------------------
    $scope.pageChanged = function (pagnumber) {
        $scope.bigCurrentPage = pagnumber;
    };

    //-------------- current page change Scope Watch ---------------------
    $scope.$watch('bigCurrentPage', function (newValue, oldValue) {
        if ($scope.Licenses) {
            $scope.GetCurrentPageData($scope.Licenses, newValue);
            $scope.currentPageNumber = newValue;
        }
    });

    //------------------- get data according Selected days -----------------------
    $scope.getDataAccordingDays = function (days) {
        $scope.filterDays = days;
        $scope.selectedSection = 6;

        $scope.LicenseData = CCOLicenseService.GetLicenseByDayLeft(days);

        $scope.GrandTotalLicenses = CCOLicenseService.GetGrandTotalLicenses();

        //if ($scope.LicenseData.length > 0) {
        //    $scope.providerType = "";
        //    $scope.providerLevel = "";
        //    $scope.LicenseTypeCode = $scope.LicenseData[0].LicenseTypeCode;
        //    $scope.Licenses = $scope.LicenseData[0].Licenses;
        //    $scope.LicenseType = $scope.LicenseData[0].LicenseType;
        //    $scope.MasterLicenses = angular.copy($scope.Licenses);
        //}

        if ($scope.LicenseData.length > 0) {
            $scope.providerType = "";
            $scope.providerLevel = "";
            //$scope.LicenseData.LicenseType = LicenseType;
            var count = 0;
            if ($scope.LicenseType == "State License") {
                count = 0;
            }
            else if ($scope.LicenseType == "Federal DEA") {
                count = 1;
            }
            else if ($scope.LicenseType == "CDS Information") {
                count = 2;
            }
            else if ($scope.LicenseType == "Specialty/Board") {
                count = 3;
            }
            else if ($scope.LicenseType == "Hospital Privileges") {
                count = 4;
            }
            else if ($scope.LicenseType == "Professional Liability") {
                count = 5;
            }
            else if ($scope.LicenseType == "Worker Compensation") {
                count = 6;
            }
            $scope.LicenseTypeCode = $scope.LicenseData[count].LicenseTypeCode;
            $scope.Licenses = $scope.LicenseData[count].Licenses;
            $scope.LicenseType = $scope.LicenseData[count].LicenseType;
            $scope.MasterLicenses = angular.copy($scope.Licenses);
        }
        $scope.GetCurrentPageData($scope.Licenses, 1);
    };

    //-------------editied by pritam--------
    $scope.getUpcomingRenewals = function (licenseData)
    {
        var licenses = [];
        for (var i = 0; i < licenseData.Licenses.length; i++) {
            if (licenseData.Licenses[i].License.dayLeft > 90 && licenseData.Licenses[i].License.dayLeft < 180)
            {
                licenses.push(licenseData.Licenses[i]);
            }
        }
        $scope.Licenses = angular.copy(licenses);
        $scope.selectedSection = 6;
        $scope.LicenseType = licenseData.LicenseType;
        $scope.LicenseTypeCode = licenseData.LicenseTypeCode;
        $scope.MasterLicenses = angular.copy($scope.Licenses);
        $scope.GetCurrentPageData($scope.Licenses, 1);
    }

    $scope.getRenewalNeeded = function (licenseData) {
       var licenses = [];
        for (var i = 0; i < licenseData.Licenses.length; i++) {
            if (licenseData.Licenses[i].License.dayLeft > 0 && licenseData.Licenses[i].License.dayLeft < 90) {
                licenses.push(licenseData.Licenses[i]);
            }
        }
        $scope.Licenses = angular.copy(licenses);
        $scope.selectedSection = 6;
        $scope.LicenseType = licenseData.LicenseType;
        $scope.LicenseTypeCode = licenseData.LicenseTypeCode;
        $scope.MasterLicenses = angular.copy($scope.Licenses);
        $scope.GetCurrentPageData($scope.Licenses, 1);
    }

    $scope.getExpiredLicense = function (licenseData) {
        var licenses = [];
        for (var i = 0; i < licenseData.Licenses.length; i++) {
            if (licenseData.Licenses[i].License.dayLeft < 0) {
                licenses.push(licenseData.Licenses[i]);
            }
        }
        $scope.Licenses = angular.copy(licenses);
        $scope.selectedSection = 6;
        $scope.LicenseType = licenseData.LicenseType;
        $scope.LicenseTypeCode = licenseData.LicenseTypeCode;
        $scope.MasterLicenses = angular.copy($scope.Licenses);
        $scope.GetCurrentPageData($scope.Licenses, 1);
    }

    //-------------- License change method -------------------------------
    $scope.getLicensTypeData = function (licenseData) {
        $scope.selectedSection = 6;
        $scope.providerType = "";
        $scope.providerLevel = "";
        $scope.Licenses = licenseData.Licenses;
        $scope.LicenseType = licenseData.LicenseType;
        $scope.LicenseTypeCode = licenseData.LicenseTypeCode;
        $scope.MasterLicenses = angular.copy($scope.Licenses);
        $scope.GetCurrentPageData($scope.Licenses, 1);
    };

    //------------------- get data according Selected Provider Type and Provider Level-----------------------
    $scope.getDataAccordingProviderTypeAndProviderLevel = function (providertype, providerlevel) {
        $scope.selectedSection = 6;
        $scope.Licenses = CCOLicenseService.GetLicenseByProviderType(providertype, providerlevel, $scope.MasterLicenses);
        $scope.GetCurrentPageData($scope.Licenses, 1);
    };

    //-------------- ANgular sorting filter --------------
    $scope.order = function (predicate, reverse, section) {
        $scope.selectedSection = section;
        $scope.Licenses = orderBy($scope.Licenses, predicate, reverse);
        $scope.GetCurrentPageData($scope.Licenses, $scope.bigCurrentPage);
    };

    //if (sessionStorage.getItem('myTabs') != null) {

    //    $scope.LicenseData = JSON.parse(sessionStorage.getItem('myTabs'));

    ///}
}]);
