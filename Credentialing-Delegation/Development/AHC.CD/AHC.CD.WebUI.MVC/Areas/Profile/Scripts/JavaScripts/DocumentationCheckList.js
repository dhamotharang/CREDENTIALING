/// <reference path="../../../../Scripts/Lib/Angular/angular.min.js" />
/// <reference path="../../../../Scripts/Lib/Angular/angular.min.js" />

profileApp.filter("SectionCustomFilter", function () {
    return function (Inputs) {

        var filtered = [];
        for(i in Inputs){
            for (j in Inputs[i].docs) {
                if (Inputs[i].docs[j].value==true) {
                    filtered.push(Inputs[i]);
                    break;
                }
            }
        }
        
        return filtered;
    };
})


profileApp.controller('DocumentationchecklistController', ['$scope', '$sce', '$http', '$q', '$rootScope', '$timeout', '$compile', 'messageAlertEngine', function ($scope, $sce, $http, $q, $rootScope, $timeout, $compile, messageAlertEngine) {
    $scope.PrintValue = true;
    var OutputFile = "";
    $scope.loadingsymbol = false;
    $scope.Documents = ["Driver's License", "Social Security Card", "Visa or Passport", "Florida Home Address", "State Medical License(Florida and any additional states)", "Board Certificate", "DEA Certificate", "CDS Certificate", "ACLS/BLS/CPR PALS(Must be current)", "Undergraduate(BS,MS,AS,AA)", "Graduate Medical School", "ECFMG", "Residency(ALL)", "Internship(ALL)", "Fellowship(ALL)", "CME Certificates(Last 24 Months)", "CV Must include current employer(Access Health Care Physicians LLC) all entries must include MM/YYYY-MM/YYYY Start/End", "Work Gap Explanation must include explanation of any gap in employment for a period of 3 months.Must contain MM/YYYY-MM/YYYY Start and End dates", "PPD Results(Last 12 months)", "Last Flu Shot(Last 12 months)", "Hospital Privilege Letters for all current facilities"];
    $scope.dataLoaded = false;
    $rootScope.DocCheckListLoaded = false;
    $scope.templates = [];
    $scope.EmailsIds = [];
    $scope.toggleApplicability = function(doc)
    {
        for (var k = 0; k < $scope.sections.length; k++) {
            for (var l = 0; l < $scope.sections[k].docs.length; l++) {
                if ($scope.sections[k].docs[l].title == doc.title) {
                    if (doc.na == true)
                    {
                        if ($scope.sections[k].docs[l].value == true)
                            $scope.sections[k].docs[l].value = false;
                    }
                    else
                    {
                        if ($scope.sections[k].docs[l].value == false)
                            $scope.sections[k].docs[l].value = true;
                    }
                  
                }
            }
        }
    }
    $scope.saveProfileDocumentCriteria = function()
    {
        if ($scope.myconfig)
        {
            var config = $scope.myconfig;
        }
        else
        {
            var config = {
                ProfileDocumentConfigID: 0,


            };
        }
       
        
        var docs = [];
        for (var k = 0; k < $scope.sections.length; k++) {
            for (var l = 0; l < $scope.sections[k].docs.length; l++) {
                docs.push({ title: $scope.sections[k].docs[l].title, na: $scope.sections[k].docs[l].na });
                
            }
        }
        config.ProfileDocumentConfiguration = JSON.stringify(docs);

        $http.post(rootDir + '/DocumentationCheckList/SaveProfileDocumentConfig?ProfileId=' + profileId, config)
        .success(function (data, status, headers, config) {
            try {
                if (data != null) {
                    
                    $scope.myconfig = data;
                    var documents = JSON.parse($scope.myconfig.ProfileDocumentConfiguration);
                    $scope.sections = [
      {
          name: 'Demographic Documentation Needed(Current clear copies needed)',
          docs: [{ title: "Driver's License", value: true, na: false }, { title: "Florida Home Address", value: true, na: false }, { title: "Social Security Card", value: true, na: false }, { title: "Visa or Passport", value: true, na: false }],
          length: 5
      },
      {
          name: 'Licensure And Certificates(Current clear copies needed)',
          docs: [{ title: "ACLS/BLS/CPR PALS(Must be current)", value: true, na: false }, { title: "Board Certificate", value: true, na: false }, { title: "CDS Certificate", value: true }, { title: "DEA Certificate", value: true, na: false }, { title: "State Medical License(Florida and any additional states)", value: true, na: false }],
          length: 6
      },
{
    name: 'Education Documentation(Current clear copies needed)',
    docs: [{ title: "CME Certificates(Last 24 Months)", value: true, na: false }, { title: "ECFMG", value: true, na: false }, { title: "Fellowship(ALL)", value: true, na: false }, { title: "Graduate Medical School", value: true, na: false }, { title: "Internship(ALL)", value: true, na: false }, { title: "Residency(ALL)", value: true, na: false }, { title: "Undergraduate(BS,MS,AS,AA)", value: true, na: false }],
    length: 8
},
{
    name: 'Work History',
    docs: [{ title: "CV Must include current employer(Access Health Care Physicians LLC) all entries must include MM/YYYY-MM/YYYY Start/End", value: true, na: false }, { title: "Work Gap Explanation must include explanation of any gap in employment for a period of 3 months.Must contain MM/YYYY-MM/YYYY Start and End dates", value: true, na: false }],
    length: 3
},
{
    name: 'Hospital Privilege Documentation/Information',
    docs: [{ title: "Hospital Privilege Letters for all current facilities", value: true, na: false }, { title: "Last Flu Shot(Last 12 months)", value: true, na: false }, { title: "PPD Results(Last 12 months)", value: true, na: false }],
    length: 4
},
                    ];
                    $scope.InitialiseSections(documents);

                    $scope.InitialiseBasedonDocuments();
                    $scope.formatforPDf();
                    $scope.criteriaEdit = false;
                    messageAlertEngine.callAlertMessage("successMsgDiv", "Document Applicability Criteria saved successfully.", "success", true);

                }
            }
            catch (e) {

            }
        }).error(function (data, status, headers, config) {

        });
    }
    $scope.EditCriteria = function()
    {
        $scope.previoussections = angular.copy($scope.sections);
        $scope.criteriaEdit = true;
    }
    $scope.InitialiseSections = function(docs)
    {
       
        for (i = 0; i < $scope.Documents.length; i++) {
            var title = $scope.Documents[i];
            for (var j = 0; j < docs.length; j++) {
                if (title == docs[j].title) {
                    for (var k = 0; k < $scope.sections.length; k++) {
                        for (var l = 0; l < $scope.sections[k].docs.length; l++) {
                            if ($scope.sections[k].docs[l].title == title ) {
                                if (docs[j].na == true) {


                                    $scope.sections[k].docs[l].value = false;
                                    $scope.sections[k].docs[l].na = true;
                                    $scope.sections[k].docs[l].remarks = 'N/A';
                                }
                                else
                                {
                                    $scope.sections[k].docs[l].remarks = 'Applicable';
                                }
                            }
                        }
                    }
                }
            }
        }
        
        
    }
    $scope.InitialiseBasedonDocuments = function () {

        for (i = 0; i < $scope.Documents.length; i++) {
            var title = $scope.Documents[i];
            for (var j = 0; j < $scope.mydata.length; j++) {
                if (title == $scope.mydata[j].Title) {
                    for (var k = 0; k < $scope.sections.length; k++) {
                        for (var l = 0; l < $scope.sections[k].docs.length; l++) {
                            if ($scope.sections[k].docs[l].title == title) {
                                if ($scope.sections[k].docs[l].na == false)
                                {
                                    $scope.sections[k].docs[l].value = false;
                                    $scope.sections[k].docs[l].remarks = 'Available';
                                }
                                else {
                                    $scope.sections[k].docs[l].value = false;
                                    $scope.sections[k].docs[l].remarks = 'N/A';
                                }
                               
                            }
                        }
                    }
                }
            }
        }

    }
    $scope.cancelInitialisation = function()
    {
       
        $scope.criteriaEdit = false;
        $scope.sections = angular.copy($scope.previoussections);
    }
    $scope.$watch("sections", function (newValue, oldValue) {
        if (newValue === oldValue) {
            return;
        }
        else {
        }
    }, true)
    $rootScope.$on('DocumentationCheckList', function () {
        $scope.loadData = false;
        $scope.sections = [
       {
           name: 'Demographic Documentation Needed(Current clear copies needed)',
           docs: [{ title: "Driver's License", value: true, na: false, remarks: 'Applicable' }, { title: "Florida Home Address", value: true, na: false, remarks: 'Applicable' }, { title: "Social Security Card", value: true, na: false, remarks: 'Applicable' }, { title: "Visa or Passport", value: true, na: false, remarks: 'Applicable' }],
           length: 5
       },
       {
           name: 'Licensure And Certificates(Current clear copies needed)',
           docs: [{ title: "ACLS/BLS/CPR PALS(Must be current)", value: true, na: false, remarks: 'Applicable' }, { title: "Board Certificate", value: true, na: false, remarks: 'Applicable' }, { title: "CDS Certificate", value: true, remarks: 'Applicable' }, { title: "DEA Certificate", value: true, na: false, remarks: 'Applicable' }, { title: "State Medical License(Florida and any additional states)", value: true, na: false, remarks: 'Applicable' }],
           length: 6
       },
{
    name: 'Education Documentation(Current clear copies needed)',
    docs: [{ title: "CME Certificates(Last 24 Months)", value: true, na: false, remarks: 'Applicable' }, { title: "ECFMG", value: true, na: false, remarks: 'Applicable' }, { title: "Fellowship(ALL)", value: true, na: false, remarks: 'Applicable' }, { title: "Graduate Medical School", value: true, na: false, remarks: 'Applicable' }, { title: "Internship(ALL)", value: true, na: false, remarks: 'Applicable' }, { title: "Residency(ALL)", value: true, na: false, remarks: 'Applicable' }, { title: "Undergraduate(BS,MS,AS,AA)", value: true, na: false, remarks: 'Applicable' }],
    length: 8
},
{
    name: 'Work History',
    docs: [{ title: "CV Must include current employer(Access Health Care Physicians LLC) all entries must include MM/YYYY-MM/YYYY Start/End", value: true, na: false, remarks: 'Applicable' }, { title: "Work Gap Explanation must include explanation of any gap in employment for a period of 3 months.Must contain MM/YYYY-MM/YYYY Start and End dates", value: true, na: false, remarks: 'Applicable' }],
    length: 3
},
{
    name: 'Hospital Privilege Documentation/Information',
    docs: [{ title: "Hospital Privilege Letters for all current facilities", value: true, na: false, remarks: 'Applicable' }, { title: "Last Flu Shot(Last 12 months)", value: true, na: false, remarks: 'Applicable' }, { title: "PPD Results(Last 12 months)", value: true, na: false, remarks: 'Applicable' }],
    length: 4
},
        ];
        
       
        if (!$scope.dataLoaded) {
            $rootScope.DocCheckListLoaded = false;

             var getDocuments = $http.get(rootDir + '/DocumentationCheckList/GetAllProfileDocuments?ProfileID=' + profileId)
           .success(function (data, status, headers, config) {
               $scope.mydata = data;    
           }).
            error(function (data, status, headers, config) {
            });

            var getCriteria = $http.get(rootDir + '/DocumentationCheckList/GetProfileDocumentConfig?ProfileID=' + profileId)
          .success(function (data, status, headers, config) {
              if (data != 'null') {
                  $scope.myconfig = data;
                  }
              
          });
            var combinedPromise = $q.all({
                documents: getDocuments,
                criteria: getCriteria
            }).then(function (response) {
                if ($scope.myconfig) {
                    var documents = JSON.parse($scope.myconfig.ProfileDocumentConfiguration);
                    $scope.InitialiseSections(documents);
                }
                $scope.InitialiseBasedonDocuments();
                $scope.formatforPDf();
                $rootScope.DocCheckListLoaded = true;
            });

            if ($scope.templates != []) {
                $http.get(rootDir + '/EmailService/GetAllEmailTemplates').
           success(function (data, status, headers, config) {

               try {
                   if (data != null) {
                       $scope.templates = data;
                   }
                   for (var i = 0; i < $scope.templates.length; i++) {
                       $scope.templates[i].LastModifiedDate = $scope.ConvertDateFormat($scope.templates[i].LastModifiedDate);
                   }
               } catch (e) {

               }

           }).
           error(function (data, status, headers, config) {
           });
            }
            if ($scope.EmailsIds != []) {
                //$http.get(rootDir + '/EmailService/GetAllEmailIds').
                //  success(function (data, status, headers, config) {
                //      try {
                //          $scope.EmailsIds = angular.copy(data);
                //      } catch (e) {
                //      }
                //  }).
                //  error(function (data, status, headers, config) {
                //  });

                $scope.GetAllEmailIds = function () {
                    var d1 = new $.Deferred();
                    $http.get(rootDir + '/EmailService/GetAllEmailIds').
                      success(function (data, status, headers, config) {
                          try {
                              $scope.EmailsIds = angular.copy(data);
                              //$scope.MailIdsforGroup = angular.copy(data);
                              d1.resolve(data);
                          } catch (e) {
                          }
                      }).
                      error(function (data, status, headers, config) {
                          return d1.promise();
                      });
                    return d1.promise();
                }

                $scope.GetAllGroupMails = function () {
                    var d2 = new $.Deferred();
                    $http.get(rootDir + '/EmailService/GetAllGroupMailNames').
                      success(function (data, status, headers, config) {
                          try {
                              $scope.groupNames = data;
                              d2.resolve(data);
                          } catch (e) {
                              throw e;
                          }
                      }).
                      error(function (data, status, headers, config) {
                          return d2.promise();
                      });
                    return d2.promise();
                }

                var promise = $scope.GetAllEmailIds().then(function () {
                    $scope.GetAllGroupMails().then(function () {
                        for (i = 0; i < $scope.groupNames.length; i++) {
                            $scope.EmailsIds.push($scope.groupNames[i]);
                        }
                    });
                });                
            }
        }
    });   

    $scope.ConvertDateFormat = function (value) {
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
    
    $rootScope.printDCData = function (title) {
       
        $rootScope.LoadForDCPdf = true;
        //$scope.loadData = true;


        var pdfhtmlString = "";
       
        var pdfhtml = $('#documentchecklisthidden').html();

        pdfhtmlString += "<html><head><title></title>";

        pdfhtmlString += "<link rel='stylesheet' href='/Content/SharedCss/bootstrap.min.css' type='text/css' />";
        pdfhtmlString += "<link rel='stylesheet' href='/Content/SharedCss/app.css' type='text/css' />";
        pdfhtmlString += "<style>.ng-hide:not(.ng-hide-animate) {display: none !important;}@page{size: auto;margin-bottom: 5mm;margin-top:7mm;}th{text-align:center;}</style>";
        pdfhtmlString += "<style>table { table-layout: fixed; } table th, table td { overflow: hidden; word-wrap: break-word; }</style>";
        pdfhtmlString += "</head><center><b style='font-size:large'>Document Checklist</b></center></br> <img style='width:120px;height:60px' src='/Content/Images/logo2.png' /> <body style='background-color:white; padding:50px'><table class='table table-bordered' >";
        pdfhtmlString += pdfhtml;
        pdfhtmlString += '</table></body>';
        pdfhtmlString += "<footer>";
        pdfhtmlString += "<p style='color: #337ab7;'>Please contact us if you have any questions</p>";
        pdfhtmlString += "<p style='color: #337ab7;'>Regards,</p>";
        var cdm = $('#cdm').val();
        var pm = $('#pm').val();
        var isPrimeCare = $('#isPrimeCare').val();
        if (isPrimeCare=="true")
        {
            pdfhtmlString += "<i style='font-weight:bold;color: #337ab7;'>" + cdm + "&nbsp;&nbsp;&nbsp;" + pm + "&thinsp;</i> <br/>";
            pdfhtmlString += "<i style='font-weight:bold;color: #337ab7;'>Credentialing Manager&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&thinsp;&thinsp;Project Manager</i> <br/> <br/>";

        }
        pdfhtmlString += "<img style='width:100px;height:30px' src='/Content/Images/logo2.png' />";
        if (isPrimeCare=="true") {
            pdfhtmlString += "<p style='color: #337ab7;'>Attention: Credentialing Department <br/> 1214 Mariner Blvd <br/> Spring Hill, FL 34609-5657</p>";
        }
        pdfhtmlString += "</footer></html>";
        var obj = { ProfileID: profileId, PdfText: pdfhtmlString, PageSize: 'A4', PageOrientation: "Portrait",PageWidth:1024,PageHeight:0 };
        $http.post('/Credentialing/DelegationProfileReport/SaveDocumentChecklist', obj)
        //$http({
        //    method: 'POST',
        //    url: rootDir + '/Credentialing/DelegationProfileReport/SaveDocumentChecklist',
        //    async: false,
        //    cache: false,
        //    contentType: false,
        //    data: {
        //        profileId: profileId,
        //        Pdfvalue: pdfhtmlString
        //    },
        //    transformRequest: function (data, headersGetter) {
        //        var formData = new FormData();
        //        angular.forEach(data, function (value, key) {
        //            formData.append(key, value);
        //        });
        //        var headers = headersGetter();
        //        delete headers['Content-Type'];


        //        return formData;
        //    }
        //})
        .success(function (data) {
            if (data.status == "true") {
                $rootScope.LoadForDCPdf = false;
                //$scope.loadData = false;
                var currentDate = moment(new Date()).format('MM/DD/YYYY, h:mm:ss a');
                var docpath = data.pdfPath;
                var dl = document.createElement('a');
                dl.setAttribute('href', docpath);
                dl.setAttribute('download', 'documentchecklist_'+currentDate+'.pdf');
                dl.click();
            }
        })
        .error(function (data, status) {

        });
       // var FORMDATA = new FormData();
       //FORMDATA.append("ProfileID", profileId);
       //FORMDATA.append("PdfText", pdfhtmlString); 
       //FORMDATA.append("PageSize", 'A4');
       //FORMDATA.append("PageOrientation", "Portrait");
       //FORMDATA.append("PageWidth", 1024);
       //FORMDATA.append("PageHeight", 0);
       // $.ajax({
       //     url: rootDir + '/Credentialing/DelegationProfileReport/SaveDocumentChecklist',
       //     type: 'POST',
       //     data: FORMDATA,
       //     async: false,
       //     cache: false,
       //     contentType: false,
       //     processData: false,
       //     success: function (data) {




       //         if (data.status == "true") {
       //             $rootScope.LoadForDCPdf = false;
       //             //$scope.loadData = false;
       //             var currentDate = moment(new Date()).format('MM/DD/YYYY, h:mm:ss a');
       //             var docpath = data.pdfPath;
       //             var dl = document.createElement('a');
       //             dl.setAttribute('href', docpath);
       //             dl.setAttribute('download', 'documentchecklist_' + currentDate + '.pdf');
       //             dl.click();
       //         }
                
       //     }
       // });
        
    };
    $rootScope.DocCheckListLoaded = true;

    $scope.tempObject;
    $scope.FilesizeError = false;

    var QID = 0;
    var index1 = -1;
    $scope.addingDocument = function () {
        $('#file').click();
    }

    $scope.fileList = [];
    $scope.curFile;
    $scope.ImageProperty = {
        file: '',
        FileListID: -1,
        FileID: -1,
        FileStatus: ''
    }
    $scope.removeFile = function (index, fileObj) {
        $scope.fileList.splice(index, 1)
    }

    $scope.getStyle = function () {
        var transform = ($scope.isSemi ? '' : 'translateY(-50%) ') + 'translateX(-50%)';
        return {
            'top': $scope.isSemi ? 'auto' : '50%',
            'bottom': $scope.isSemi ? '5%' : 'auto',
            'left': '35%',
            'transform': transform,
            '-moz-transform': transform,
            '-webkit-transform': transform,
            'font-size': $scope.radius / 3.5 + 'px'
        };
    };
    var files = [];
    var tempIndex = 0;
    var tempmultiplefilelength;
    $scope.setFile = function (element) {
        var count = 0;
        tempIndex = 0;
        var index = -1;
        var totalAttachmentSize = 0;
        files = [];
        files = element.files;
        for (var i = 0; i < $scope.fileList.length; i++)
        {
            totalAttachmentSize += $scope.fileList[i].File[0].file.size;
        }
        for (var j = 0; j < files.length; j++) {
            totalAttachmentSize += files[j].size;
        }
        var totalfilesize = 0;
        tempmultiplefilelength = $scope.fileList.length;
        if (count == 0 && totalAttachmentSize < 15728640) {
            for (var i = 0; i < files.length; i++) {
               
                    $('.badge').removeAttr("style");
                    totalfilesize += files[i].size;
                    var TempArray = [];
                    $scope.ImageProperty.file = files[i];
                    $scope.ImageProperty.FileStatus = 'Active';
                    $scope.ImageProperty.FileListID = $scope.fileList.length;
                    $scope.ImageProperty.FileID = i;
                    TempArray.push($scope.ImageProperty);
                    $scope.fileList.push({ File: TempArray });
                    $scope.ImageProperty = {};
                    if (!$scope.$$fetch)
                        $scope.$apply();
                }
            $scope.UploadFile();

        }
        else
        {
            $('#file').val("");
            $('.badge').attr("style", "background-color:white;color:indianred");
            
        }
       
    }

    $scope.UploadFile = function () {
        for (var i = 0; i < $scope.fileList.length; i++) {
            for (var j = 0; j < $scope.fileList[i].File.length; j++) {
                if ($scope.fileList[i].File[j].UploadDone != true) $scope.fileList[i].File[j].UploadDone = false;
                if ($scope.fileList[i].File[j].FileStatus == 'Active') {
                    $scope.UploadFileIndividual($scope.fileList[i].File[j].file,
                                        $scope.fileList[i].File[j].file.name,
                                        $scope.fileList[i].File[j].file.type,
                                        $scope.fileList[i].File[j].file.size,
                                        $scope.fileList[i].File[j].FileListID,
                                        $scope.fileList[i].File[j].FileID
                                        );
                    $scope.fileList[i].File[j].FileStatus = 'Inactive';
                }
            }
        }
    }

    $scope.UploadFileIndividual = function (fileToUpload, name, type, size, Qindex, FLindex, Findex) {
        $scope.current = 0;
        var reqObj = new XMLHttpRequest();
        reqObj.upload.addEventListener("progress", uploadProgress, false)
        reqObj.addEventListener("load", uploadComplete, false)
        reqObj.addEventListener("error", uploadFailed, false)
        reqObj.addEventListener("abort", uploadCanceled, false)
        reqObj.open("POST", rootDir + "/Profile/DisclosureQuestion/FileUpload", true);
        reqObj.setRequestHeader("Content-Type", "multipart/form-data");
        reqObj.setRequestHeader('X-File-Name', name);
        reqObj.setRequestHeader('X-File-Type', type);
        reqObj.setRequestHeader('X-File-Size', size);
        reqObj.send(fileToUpload);

        function uploadProgress(evt) {
            if (evt.lengthComputable) {
                var uploadProgressCount = Math.round(evt.loaded * 100 / evt.total);
                $scope.current = uploadProgressCount;
                if (uploadProgressCount == 100) {
                    $scope.current = uploadProgressCount;
                }
            }
        }

        function uploadComplete(evt) {
            var resultdata = JSON.parse(evt.currentTarget.responseText);
            $scope.Attachments.push(resultdata.FilePath);
            if (files.length == 1) {
                $scope.fileList[$scope.fileList.length - 1].File[0].path = resultdata.FilePath;
                $scope.fileList[$scope.fileList.length - 1].File[0].relativePath = resultdata.RelativePath;
                $scope.fileList[$scope.fileList.length - 1].File[0].UploadDone = true;
            } else if (files.length != 1 && tempmultiplefilelength != 0) {
                $scope.fileList[tempmultiplefilelength].File[0].path = resultdata.FilePath;
                $scope.fileList[tempmultiplefilelength].File[0].relativePath = resultdata.RelativePath;
                $scope.fileList[tempmultiplefilelength].File[0].UploadDone = true;
                tempmultiplefilelength++;
            } else {
                $scope.fileList[tempIndex].File[0].path = resultdata.FilePath;
                $scope.fileList[tempIndex].File[0].relativePath = resultdata.RelativePath;
                $scope.fileList[tempIndex].File[0].UploadDone = true;
                tempIndex++;
            }
            $scope.NoOfFileSaved++;
            $scope.$apply();
            $('#file').val("");
        }

        function uploadFailed(evt) {
        }

        function uploadCanceled(evt) {
        }
    }
    var $div;
    $scope.renderHtml = function (htmlCode) {
        return $sce.trustAsHtml(htmlCode);
    };
    $scope.Attachments = [];
    //END

    $scope.formatforPDf = function()
    {
        $scope.sections1 = [];
        $scope.sections2 = [];
        for (var k = 0; k < $scope.sections.length; k++) {
            var section = { sectionName: $scope.sections[k].name, adocs: [], nadocs: [] };

            for (var l = 0; l < $scope.sections[k].docs.length; l++) {
                if ($scope.sections[k].docs[l].remarks == 'Applicable') {
                    section.nadocs.push({ title: $scope.sections[k].docs[l].title });
                }
                else if ($scope.sections[k].docs[l].remarks == 'Available') {
                    section.adocs.push({ title: $scope.sections[k].docs[l].title });
                }
            }
            if (section.nadocs.length > 0)
                $scope.sections1.push(section);
            if (section.adocs.length > 0)
                $scope.sections2.push(section);
        }
    }


    $scope.GenerateEmailPopUp = function () {
        $("#Subject").removeClass("input-validation-error");
        availableTags = angular.copy($scope.EmailsIds);
       
        //$timeout(function () {
        //    var doc = new jsPDF();
        //    var specialElementHandlers = {
        //        '#editor': function (element, renderer) {
        //            return true;
        //        }
        //    };
        //    doc.fromHTML($('#tulasi').html(), 15, 15, {
        //        'width': 170,
        //        'elementHandlers': specialElementHandlers
        //    });
        //    var binary = doc.output();
        //    OutputFile = binary ? btoa(binary) : "";
        //}, 500)
        $scope.PrintValue = true;
        //$timeout(function () {
        //    html2canvas(document.getElementById("magicTemplate"), {
        //        onrendered: function (canvas) {
        //            var img = canvas.toDataURL("image/jpeg", 1.0);
        //            var doc = new jsPDF("l", "mm", "a4");
        //            doc.addImage(img, 'JPEG', 10, 10, 270, 150);
        //            if(!$scope.$$phase){
        //                $scope.$apply(function () {
        //                    $scope.PrintValue = true;
        //                })
        //            }
        //            else {
        //                $scope.PrintValue = true;
        //            }
        //            //doc.save("sdfsdfsd.pdf");
        //            var binary = doc.output();
        //            OutputFile = binary ? btoa(binary) : "";

        //        }
        //    })
        //}, 500)
        
        $scope.errmsgforTo = false;
        $scope.errmsgforCC = false;
        $scope.errmsgforBCC = false;
        var formdata = $('#newEmailForm');
        formdata[0].reset();
        var rootPath = $('#rootPath').val();
        var isPrimeCare = $('#isPrimeCare').val();
        
        $scope.tempObject.Body = "";
        $scope.tempObject.Body += "<img style='width:120px;height:60px' src='" + rootPath + "/Content/Images/logo2.png' />";
        $scope.tempObject.Body += $('#documentchecklisthidden').html();
        $scope.tempObject.Body += "<footer>";
        $scope.tempObject.Body += "<p style='color: #337ab7;'>Please contact us if you have any questions</p> <br/>";
        $scope.tempObject.Body += "<p style='color: #337ab7;'>Regards,</p> <br/>";
        if (isPrimeCare == "true") {
            $scope.tempObject.Body += "<i style='font-weight:bold;color: #337ab7;'>Elizabeth Duffin (Collins)&nbsp;&nbsp;&nbsp;Matt Romeo</i> <br/>";
            $scope.tempObject.Body += "<i style='font-weight:bold;color: #337ab7;'>Credentialing Manager&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&thinsp;&thinsp;Project Manager</i> <br/> <br/>";
        }
        else
        {
            $scope.tempObject.Body += "<i style='font-weight:bold;color: #337ab7;'>Credentialing Manager</i> <br/> <br/>";

        }
        $scope.tempObject.Body += "<img style='width:100px;height:30px' src='" + rootPath + "/Content/Images/logo2.png' />";
        if (isPrimeCare == "true") {
            $scope.tempObject.Body += "<p style='color: #337ab7;'>Attention: Credentialing Department <br/> 1214 Mariner Blvd <br/> Spring Hill, FL 34609-5657</p>";

        }
        $scope.tempObject.Body += "</footer>";
        $scope.tempObject.SaveAsTemplateYesNoOption = '2';
    }

    $scope.changeYesNoOption = function (value) {
        $("#newEmailForm .field-validation-error").remove();
        $scope.templateSelected = false;
        $scope.tempObject.Subject = "";
        $scope.tempObject.Body = "";
        if (value == 2) {
            $scope.templateSelected = true;
        } else {
            $scope.tempObject.Title = "";
        }
    }

    $scope.EditCancle = function (temp) {
        $scope.errmsgforTo = false;
        $scope.errmsgforCC = false;
        $scope.errmsgforBCC = false;
        $scope.errmsg = false;
        $scope.fileList = [];
        var formdata = $('#newEmailForm');
        formdata[0].reset();
        if ($scope.data != null && temp != null) {
            for (var c = 0; c < $scope.data.length; c++) {
                if ($scope.data[c].EmailTemplateID == temp.EmailTemplateID) {
                    $scope.data[c].EditStatus = false;
                    break;
                }
            }
        }
        $('.badge').removeAttr('style');
        $scope.ClearModalData();
    }
    $scope.ExportPDF = function () {
        $scope.PrintValue = false;
        $timeout(function () {
            html2canvas(document.getElementById("magicTemplate"), {
                onrendered :function(canvas){
                    var img = canvas.toDataURL("image/jpeg",1.0);
                    var doc = new jsPDF("l", "mm", "a4");
                    doc.addImage(img, 'JPEG', 10, 10, 270, 150);
                    if (!$scope.$$phase) {
                        $scope.$apply(function () {
                            $scope.PrintValue = true;
                        })
                    }
                    else {
                        $scope.PrintValue = true;
                    }
                    doc.output('dataurlnewwindow');
                }
            })
        }, 200)
        //$scope.loadData = true;
        //$timeout(function () {
        //    var doc = new jsPDF();
        //    var specialElementHandlers = {
        //        '#editor': function (element, renderer) {
        //            return true;
        //        }
        //    };
        //    doc.fromHTML($('#tulasi').html(), 15, 15, {
        //        'width': 170,
        //        'elementHandlers': specialElementHandlers
        //    });
        //    //doc.save('sample-file.pdf');
        //    //var binary = doc.output();
        //    //OutputFile = binary ? btoa(binary) : "";

        //    doc.output('dataurlnewwindow');

        //    //$scope.loadData = false;
        //}, 500)
        
    }
    $scope.ClearModalData = function () {
        $scope.tempObject.To = "";
        $scope.tempObject.CC = "";
        $scope.tempObject.BCC = "";
        $scope.tempObject.Subject = "";
        $scope.tempObject.Body = "";
        $scope.tempObject.IsRecurrenceEnabledYesNoOption = "";
        $scope.tempObject.RecurrenceIntervalTypeCategory = "";
        $scope.tempObject.FromDate = "";
        $scope.tempObject.ToDate = "";
        $scope.tempObject.IntervalFactor = "";
        $scope.tempObject.SaveAsTemplateYesNoOption = '';
    }

    $scope.AddOrSaveEmail = function (Form_Id) {
        $scope.FileUploadProgress = false;
        $scope.errmsgforBCC = false;
        $scope.errmsgforCC = false;
        var AttachmentsSize = 0;
        for (i = 0; i < $scope.fileList.length; i++) {
            AttachmentsSize += $scope.fileList[i].File[0].file.size;
            if ($scope.fileList[i].File[0].relativePath == "" || $scope.fileList[i].File[0].relativePath === undefined) {
                $scope.FileUploadProgress = true;
                messageAlertEngine.callAlertMessage('warningdiv', "File Upload is in Progress", "info", true);
                break;
            }
        }
        if (AttachmentsSize > 15728640) {
            messageAlertEngine.callAlertMessage('warningdiv', 'Files exceeded the size limit!', "info", true);
        }
        else {
        }
        if ($('#tags').val() == "") {
            $scope.errmsg = true;
            $scope.errmsgforTo = false;
        }
        else {
            $scope.errmsg = false;
            var regx1 = /^[a-z][a-zA-Z0-9_]*(\.[a-zA-Z][a-zA-Z0-9_]*)?@[a-z][a-zA-Z-0-9]*\.[a-z]+(\.[a-z]+)?$/;

            var emailData = $('#tags').val().split(';');
            for (i = 0; i < emailData.length; i++) {
                if (emailData[i] != "") {
                    if (regx1.test(emailData[i].toLowerCase())) {
                        $scope.errmsgforTo = false;
                    }
                    else { $scope.errmsgforTo = true; }
                }
            }

            var emailDataCC = $('#tagsCC').val().split(';');
            for (i = 0; i < emailDataCC.length; i++) {
                if (emailDataCC[i] != "") {
                    if (regx1.test(emailDataCC[i].toLowerCase())) {
                        $scope.errmsgforCC = false;
                    }
                    else { $scope.errmsgforCC = true; }
                }
            }

            var emailDataBCC = $('#tagsBCC').val().split(';');
            for (i = 0; i < emailDataBCC.length; i++) {
                if (emailDataBCC[i] != "") {
                    if (regx1.test(emailDataBCC[i].toLowerCase())) {
                        $scope.errmsgforBCC = false;
                    }
                    else { $scope.errmsgforBCC = true; }
                }
            }
        }
        var checkVariable = false;
        if (!$scope.errmsg && !$scope.errmsgforBCC && !$scope.errmsgforCC && !$scope.errmsgforTo) { checkVariable = true; }
        resetFormValidation($("#" + Form_Id));
        if ($("#" + Form_Id).valid() && checkVariable && !$scope.FileUploadProgress && AttachmentsSize < 15728640) {

            var ltcharCheck = true;
            var gtcharCheck = true;
            while (gtcharCheck == true || ltcharCheck == true) {
                if ($('#Body').val().indexOf('<') > -1) {
                    $('#Body').val($('#Body').val().replace("<", "&lt;"));
                    ltcharCheck = true;
                }
                else {
                    ltcharCheck = false;
                }
                if ($('#Body').val().indexOf('>') > -1) {
                    $('#Body').val($('#Body').val().replace(">", "&gt;"));
                    gtcharCheck = true;
                }
                else {
                    gtcharCheck = false;
                }
            }

            var FORMDATA = new FormData();
            var other_data = $('#' + Form_Id).serializeArray();
            $.each(other_data, function (key, input) {
                FORMDATA.append(input.name, input.value);
            });
            $.ajax({
                url: rootDir + '/EmailService/AddEmail',
                type: 'POST',
                data: FORMDATA,
                async: false,
                cache: false,
                contentType: false,
                processData: false,
                success: function (data) {
                    try {
                        var formdata = $('#newEmailForm');
                        if (data.status == "true") {
                            $scope.ClearModalData();
                            $scope.fileList = [];
                            formdata[0].reset();
                            $scope.modalDismiss();
                           
                            messageAlertEngine.callAlertMessage('successMsgDiv', "Email Sent Successfully.", "success", true);
                        }
                        else {
                            $scope.modalDismiss();
                            $scope.ClearModalData();
                            
                            messageAlertEngine.callAlertMessage('errorMsgDiv', data.status, "danger", true);
                            $scope.errorMsg = data.status;
                        }
                    } catch (e) {
                        $scope.modalDismiss();
                    }
                },
                error: function (data) {
                    $scope.modalDismiss();
                    $scope.ClearModalData();
                    var formdata = $('#newEmailForm');
                    formdata[0].reset();
                    messageAlertEngine.callAlertMessage('errorMsgDiv', data.status, "danger", true);
                    $scope.errorMsg = "Unable to schedule Email.";
                }
            });
        }
        else {
        }
    }
    $scope.hideDiv = function () {
        $(".ProviderTypeSelectAutoList").hide();
    }


    $scope.modalDismiss = function () {
        $('#composeMail').modal('hide');
        $('body').removeClass('modal-open');
        $('.modal-backdrop').remove();
    }


    $scope.showContent = function () {
        $scope.templateSelected = true;

    }
    $scope.searchCumDropDown = function (divId) {
        $(".ProviderTypeSelectAutoList").hide();
        $("#" + divId).show();
    };

    $(document).ready(function () {
        $(".ProviderTypeSelectAutoList").hide();
        $("body").tooltip({ selector: '[data-toggle=tooltip]' });
    });

    $(document).click(function (event) {
        if (!$(event.target).hasClass("form-control") && $(event.target).parents(".ProviderTypeSelectAutoList").length === 0) {
            $(".ProviderTypeSelectAutoList").hide();
        }
    });
    var availableTags = [];
    $('.badge').removeAttr("style");
    //availableTags = angular.copy($scope.EmailsIds);
    availableTags =
        [
            'tulasidhar.salla@pratian.com',
            'bindupriya.ambati@pratian.com',
            'manideep.innamuri@pratian.com',
            'sharath.km@pratian.com',
            'shalabh@pratian.com'
        ];

    $scope.emailsAutoFill = function () {
        $(function () {

            //availableTags = $scope.EmailsIds;

            function split(val) {
                return val.split(/;\s*/);
            }
            function extractLast(term) {
                return split(term).pop();
            }

            $("#tags,#tagsCC,#tagsBCC")
                // don't navigate away from the field on tab when selecting an item
                .bind("keydown", function (event) {
                    if (event.keyCode === $.ui.keyCode.TAB &&
                            $(this).autocomplete("instance").menu.active) {
                        event.preventDefault();
                    }
                })
                .autocomplete({
                    minLength: 0,
                    source: function (request, response) {
                        // delegate back to autocomplete, but extract the last term
                        response($.ui.autocomplete.filter(
                            availableTags, extractLast(request.term)));
                    },
                    focus: function () {
                        // prevent value inserted on focus
                        return false;
                    },
                    select: function (event, ui) {
                        var terms = split(this.value);
                        // remove the current input
                        terms.pop();
                        // add the selected item
                        terms.push(ui.item.value);
                        // add placeholder to get the comma-and-space at the end
                        terms.push("");
                        this.value = terms.join(";");
                        return false;
                    }
                });
        });
    }

}]);
function resetFormValidation($formdata) {
    $formdata.removeData('validator');
    $formdata.removeData('unobtrusiveValidation');
    $.validator.unobtrusive.parse($formdata);
}