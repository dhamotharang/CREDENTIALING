var profileApp = angular.module("ProfileReportApp", ['angular.filter']);
profileApp.controller("ProfileCntrl", ['$scope', '$http', function ($scope, $http) {
   
    $scope.GetReport = function () {
        var profiles = [];
        $http.get(rootDir + '/ProfileReport/LoadProfileReport').then(function (result) {
            angular.forEach(result.data, function (item) {
                PersonalDetail = item.PersonalDetail;
                PracticeLocationDetails = item.PracticeLocationDetails;
                LanguageDetail = item.LanguageDetails;
                SpecialityDetails = item.SpecialityDetails;
                var spec = "";
                var obj = {};
                obj.specType = "";
                if (SpecialityDetails.length == 1) {
                    if (SpecialityDetails[0].Specialty.Name == 'Family Medicine' ||
                       SpecialityDetails[0].Specialty.Name == 'Internal Medicine' ||
                       SpecialityDetails[0].Specialty.Name == 'GENERAL MEDICINE') {
                        obj.specType = "PRIMARY CARE PROVIDER";
                    } else {
                        obj.specType = "SPECIALIST";
                    }
                    
                }
                angular.forEach(SpecialityDetails, function (speciality) {
                    if (speciality.Specialty != null)
                        spec = spec + speciality.Specialty.Name;
                });
                
              
                obj.spec = (spec != "")?spec: 'Not Available';
                var languages = "";
                if (LanguageDetail != null) {
                    angular.forEach(LanguageDetail.KnownLanguages, function (lang) {
                        languages = languages + lang.Language;
                    });

                }
              
                obj.Languages = (languages != "") ? languages : 'Not Available';

                angular.forEach(PracticeLocationDetails, function (loc) {
                    if (loc) {

                        if (loc.IsPrimary == 'YES') {

                            if (loc.Facility != null) {
                                obj.county = (loc.Facility.County != null) ? loc.Facility.County : 'Not Available';

                                obj.address = loc.Facility.FacilityName + ' ' + loc.Facility.Name + ' ' +
                         loc.Facility.Street + ' ' + loc.Facility.Country;
                                obj.Office_Phone = loc.Facility.MobileNumber;
                            }
                            if (loc.OpenPracticeStatus != null) {
                                obj.Ages_Served = (loc.OpenPracticeStatus.MinimumAge != "") ? loc.OpenPracticeStatus.MinimumAge + "+" : "Not Available";
                            }
                            else {
                                obj.Ages_Served = "Not Available";
                            }

                            var offHrs = "";
                            if (loc.OfficeHour != null) {
                                angular.forEach(loc.OfficeHour.PracticeDays, function (day) {
                                    if (day.DayName != null) {
                                        offHrs = day.DayName + "";
                                        angular.forEach(day.DailyHours, function (hr) {
                                            offHrs = offHrs + hr.StartTime + "-" + hr.EndTime + " ";
                                        });
                                    }

                                });
                            }

                            obj.MTR = (offHrs != "") ? offHrs : 'Not Available';
                        }
                    }
                    else {

                        obj.address = "Not Available";
                        obj.Office_Phone = "Not Available";
                        obj.Ages_Served = "Not Available";
                        obj.OfficeHour = "Not Available";
                    }
                    
                });
                obj.providerName = PersonalDetail.FirstName +' '+PersonalDetail.LastName+ ' '+PersonalDetail.Salutation;
                obj.providerID = (item.ProfileID != 0) ? item.ProfileID : "Not Available" ;
                profiles.push(obj);
            }
            
        )

            $scope.profiles = profiles;
        });
    }
    $scope.GetReport();

    var ProfileReportColumns =
                [{ title: "County", key: "county", width: 80 },
                { title: "Provider Name", key: "providerName", width: 90 },
                { title: "Provider ID", key: "providerID", width: 70 },
                { title: "Provider Address", key: "address", width: 60 },
                { title: "Office No", key: "Office_Phone", width: 200 },
                { title: "Aged Served", key: "Ages_Served", width: 70 },
                { title: "Time", key: "MTR", width: 70 },
                { title: "Languages", key: "Languages", width: 70 },
                { title: "Provider Type", key: "specType", width: 90 },
                { title: "Speciality", key: "spec", width: 60 }, ]

    var jsonobject = { Name: ProfileReportColumns };

    $scope.headersScript = function (id, PDFData, doc) {
        doc.autoTable(id, PDFData, {
            beforePageContent: 'Test',
            startY: 60,
            drawHeaderRow: function (row, data) {
                row.height = 46;
            },
            drawHeaderCell: function (cell, data) {
                doc.rect(cell.x, cell.y, cell.width, cell.height, cell.styles.fillStyle);
                doc.setFillColor(230);
                doc.rect(cell.x, cell.y + (cell.height / 2), cell.width, cell.height / 2, cell.styles.fillStyle);
                doc.autoTableText(cell.text, cell.textPos.x, cell.textPos.y, {
                    halign: cell.styles.halign,
                    valign: cell.styles.valign
                });
                doc.setTextColor(100);
                var text = data.table.rows[0].cells[data.column.dataKey].text;
                doc.autoTableText(text, cell.textPos.x, cell.textPos.y + (cell.height / 2), {
                    halign: cell.styles.halign,
                    valign: cell.styles.valign
                });
                return false;
            },
            drawRow: function (row, data) {
                if (row.index === 0)
                    return false;
            },
            margin: {
                top: 60,
                left: 50
            },
            styles: {
                overflow: 'linebreak',
                fontSize: 8,
                tableWidth: 'auto',
                columnWidth: 'auto',
            },
            columnStyles: {
                1: {
                    columnWidth: 'auto'
                }
            },


        });
    }

    $scope.GeneratePDF = function () {

        var doc = new jsPDF('l', 'pt');
        if ($scope.profiles.length != 0) {
            var PDFData = angular.copy($scope.profiles);
            doc.text("Profile Report", 350, 50);
            //doc.text(PDFData[0].county, 50, 100);
            ////doc.text(PDFData[0].specType, 600, 100);
            //doc.text(PDFData[0].providerName, 50, 120);
            //doc.text("Provider ID : " + PDFData[0].providerID, 50, 140);
            //doc.text("Address : " + PDFData[0].address, 50, 160);
            ////doc.text(PDFData[0].Office_Phone, 50, 180);
            ////doc.text(PDFData[0].Ages_Served, 50, 200);
            ////doc.text(PDFData[0].MTR, 50, 220);
            //doc.text("Languages : " + PDFData[0].Languages, 50, 240);
            //doc.text(PDFData[0].spec, 50, 260);
            $scope.headersScript(jsonobject.Name, PDFData, doc)
        }

        doc.output('datauri');
    }

    $scope.PrintPDF = function () {

        var pdfhtml = "";
       
        var pdfhtmlString = $('#profileReport').html();
        pdfhtml += "<html><head><title></title><table><body>";
        pdfhtml += "<link rel='stylesheet' href='/Content/SharedCss/bootstrap.min.css' type='text/css' />";
        //pdfhtml += "<link rel='stylesheet' href='/Content/SharedCss/app.css' type='text/css' />";
        pdfhtmlString += "</head>";
        pdfhtml += pdfhtmlString;
        pdfhtml += "</table></body></html>";
        //$http.post('/Profile/ProfileReport/SaveProfileReport', pdfhtml)
        $http({
            url: '/Profile/ProfileReport/SaveProfileReport',
            method: "POST",
            data: { 'pdfhtml': pdfhtml }
        })
        .success(function (data) {
            if (data.status == "true") {                
                window.open("/Document/View?path=" + data.pdfPath, '_blank');
            }
        })
        .error(function (data, status) {

        });      
        
    
    }
}])