profileApp.directive('popover', function () {
    return function (scope, elem) {
        elem.popover();
    };
});
profileApp.filter('isQID', function () {
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
});
profileApp.controller('DisclosureController', ['$scope', '$rootScope', '$http', 'masterDataService', 'messageAlertEngine', 'profileUpdates', function ($scope, $rootScope, $http, masterDataService, messageAlertEngine, profileUpdates) {

    //Get all the data for the Disclosure Question on document on ready
    //$(document).ready(function () {
    //    $("#Loading_Message").text("Gathering Profile Data...");
   
    //    $http({
    //        method: 'GET',
    //        url: '/Profile/MasterProfile/GetDisclosureQuestionsProfileDataAsync?profileId=' + profileId
    //    }).success(function (data, status, headers, config) {
   
    //        try {
    //            for (key in data) {
   
    //                $rootScope.$emit(key, data[key]);
    //                //call respective controller to load data (PSP)
    //            }

    //            $rootScope.DisclosureQuestionLoaded = true;
    //            //$rootScope.$broadcast("LoadRequireMasterData");
    //        } catch (e) {
    
    //            $rootScope.DisclosureQuestionLoaded = true;
    //        }

    //    }).error(function (data, status, headers, config) {
    
    //        $rootScope.DisclosureQuestionLoaded = true;
    //    });
    //});

    // rootScoped on emitted value catches the value for the model and insert to get the old data for Practice Interest
    $scope.setFiles = function (file) {
        $(file).parent().parent().find(".jancyFileWrapTexts").find("span").width($(file).parent().parent().width() < 243 ? $(file).parent().parent().width() : 243);

    }
    $scope.tempObject = {};
    $scope.max=100;
    $scope.current = 0;
    $scope.setAllNo = function () {
        $scope.isDataPresent = true;
        //$('input:radio[class^=selectNo][value=2]').each(function (i) {
        //    this.checked = true;
        //});
        $('input:radio[class^=selectNo][value=2]').trigger("click");
        $('input:radio[class^=selectNo][value=2]').trigger("click");
    }


    $rootScope.$on('ProfileDisclosure', function (event, val) {

        $scope.ProfileDisclosurePendingRequest = profileUpdates.getUpdates('Disclosure Question', 'Profile Disclosure');

        $scope.disclosureQuestions = val;
    });
    
    $rootScope.$on("LoadRequireMasterDataDisclosureQuestion", function () {

        masterDataService.getMasterData(rootDir + "/Profile/MasterData/getAllQuestions").then(function (masterQuestions) {
            $scope.masterQuestions = masterQuestions;
            for (i in $scope.masterQuestions) {
                $scope.masterQuestions[i].TableStatus = false;
            }
        });

        masterDataService.getMasterData(rootDir + "/Profile/MasterData/getAllQuestionCategories").then(function (masterQuestionCategories) {
            $scope.masterQuestionCategories = masterQuestionCategories;
            console.log($scope.masterQuestionCategories);
        });

    });
    

    //Multiple Document ADD to DisClosure Question Start

    var QID = 0;
    var index1 = -1;
    $scope.addingDocument = function (index, QuestionID, QData) {
        index1 = $scope.masterQuestions.indexOf(QData);
        QID = QuestionID;
        $('#file' + index).click();
    }
    
    $scope.fileList = [];
    $scope.curFile;
    $scope.ImageProperty = {
        file: '',
        QID: -1,
        FileListID: -1,
        FileID:-1,
        FileStatus:''
    }
    $scope.removeFile = function (fileObj, QID,QData) {
        var index = -1;
        for (var i in $scope.fileList) {
            if ($scope.fileList[i].QuestionID == QID) {
                index = i;
                break;
            }
        }
        $scope.fileList[index].File.splice($scope.fileList[index].File.indexOf(fileObj), 1);
        if ($scope.fileList[index].File.length==0) {
            $scope.masterQuestions[$scope.masterQuestions.indexOf(QData)].TableStatus = false;
        }
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

    $scope.setFile = function (element) {
        var count = 0;
        var TempArray = [];
        var index = -1;
        var files = [];
        var files = element.files;
        for (var i in $scope.fileList) {
            if ($scope.fileList[i].QuestionID == QID) {
                index = i;
                count++;
                break;
            }
        }
        if(count==0){
            for (var i = 0; i < files.length; i++) {
                $scope.ImageProperty.file = files[i];
                $scope.ImageProperty.QID = QID;
                $scope.ImageProperty.FileStatus = 'Active';
                $scope.ImageProperty.FileListID = $scope.fileList.length;
                $scope.ImageProperty.FileID = i;
                TempArray.push($scope.ImageProperty);
                $scope.fileList.push({ QuestionID: QID, File: TempArray });
                $scope.masterQuestions[index1].TableStatus = true;
                $scope.ImageProperty = {};
                $scope.$apply();
            }
        }
        else {
            for (var i = 0; i < files.length; i++) {
                $scope.ImageProperty.file = files[i];
                $scope.ImageProperty.QID = QID;
                $scope.ImageProperty.FileStatus = 'Active';
                $scope.ImageProperty.FileListID = index;
                $scope.ImageProperty.FileID = $scope.fileList[index].File.length+i;
                $scope.fileList[index].File.push($scope.ImageProperty);
                $scope.masterQuestions[index1].TableStatus = true;
                $scope.ImageProperty = {};
                $scope.$apply();
            }
        }
        $scope.UploadFile();

    }

    $scope.UploadFile = function () {

        for (var i = 0; i < $scope.fileList.length; i++) {
            for (var j = 0; j < $scope.fileList[i].File.length; j++) {
                if ($scope.fileList[i].File[j].FileStatus=='Active') {
                    $scope.UploadFileIndividual($scope.fileList[i].File[j].file,
                                        $scope.fileList[i].File[j].file.name,
                                        $scope.fileList[i].File[j].file.type,
                                        $scope.fileList[i].File[j].file.size,
                                        $scope.fileList[i].File[j].QID,
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

            
            $scope.NoOfFileSaved++;
            $scope.$apply();
        }

        function uploadFailed(evt) {
            alert("Failed");
        }

        function uploadCanceled(evt) {

            alert("Canceled");
        }

    }


    //END








    
    $scope.enableSave = function (obj, value) {
        if (value == 2) {
            obj.Reason = '';
        }
        $scope.isDataPresent = true;
    }
    $scope.disableSave = function () {
        $scope.isDataPresent = false;
    }

    //To save, add and update Disclosure Questions.
    $scope.saveQuestions = function (disclosureQuestions, type) {
        loadingOn();
        var validationStatus;
        var url;
        var $formData;

        validationStatus = $('#editShowDisclosureQuestions').find('form').valid();
        //validationStatus = $('#disclosureForm').find('form').valid();
        $formData = $('#editShowDisclosureQuestions').find('form');
        //$formData = $('#disclosureForm').find('form');
        url = rootDir + "/Profile/DisclosureQuestion/UpdateDisclosureQuestionAsync?profileId=" + profileId;

        $scope.typeOfSaveForDisclosureQuestions = type;

        //ResetFormForValidation($formData);
        validationStatus = $formData.valid();

        if (validationStatus) {
            //Simple POST request example (passing data) :
            $.ajax({
                url: url,
                type: 'POST',
                //data: new FormData($formData[0]),
                params: { profileId: disclosureQuestions.ProfileDisclosureID, disclosureQuestion: { ProfileDisclosureID: disclosureQuestions.ProfileDisclosureID, ProfileDisclosureQuestionAnswers: disclosureQuestions.ProfileDisclosureQuestionAnswers } },
                async: false,
                cache: false,
                contentType: false,
                processData: false,
                success: function (data) {
                    try {
                        if (data.status == "true") {
                            $scope.disclosureQuestions = data.disclosureQuestion;
                            console.log('dq');
                            console.log($scope.disclosureQuestions);
                            $scope.ErrorInDisclosureQuestions = false;
                            $rootScope.operateCancelControl('');
                            if ($scope.typeOfSaveForDisclosureQuestions == "Add") {
                                $rootScope.visibilityControl = "addedNewDisclosureQuestions";
                                messageAlertEngine.callAlertMessage('addedNewDisclosureQuestions', "Disclosure Questions saved successfully !!!!", "success", true);
                            } else {
                                $scope.ProfileDisclosurePendingRequest = true;
                                $rootScope.visibilityControl = "updatedDisclosureQuestions";
                                messageAlertEngine.callAlertMessage('updatedDisclosureQuestions', "Disclosure Questions updated successfully !!!!", "success", true);
                            }
                        } else {
                            messageAlertEngine.callAlertMessage('errorDisclosureQuestions', "", "danger", true);
                            $scope.errorDisclosureQuestions = data.status.split(",");
                        }
                    } catch (e) {
                      
                    }
                },
                error: function (e) {
                    messageAlertEngine.callAlertMessage('errorDisclosureQuestions', "", "danger", true);
                    $scope.errorDisclosureQuestions = "Sorry for Inconvenience !!!! Please Try Again Later...";
                }
            });
        }
        loadingOff();
    }
    $rootScope.DisclosureQuestionLoaded = true;
    $scope.dataLoaded = false;
    $rootScope.$on('DisclosureQuestion', function () {
        if (!$scope.dataLoaded) {
            $rootScope.DisclosureQuestionLoaded = false;
           
            $http({
                method: 'GET',
                url: rootDir + '/Profile/MasterProfile/GetDisclosureQuestionsProfileDataAsync?profileId=' + profileId
            }).success(function (data, status, headers, config) {
                console.log('Disclosure Question');
                console.log(data);
                $scope.tempObject = data;
                console.log($scope.tempObject);
                try {
                    for (key in data) {
                       
                        $rootScope.$emit(key, data[key]);
                        //call respective controller to load data (PSP)
                    }

                    $rootScope.DisclosureQuestionLoaded = true;
                    $rootScope.$broadcast("LoadRequireMasterDataDisclosureQuestion");
                } catch (e) {
                   
                    $rootScope.DisclosureQuestionLoaded = true;
                }

            }).error(function (data, status, headers, config) {
               
                $rootScope.DisclosureQuestionLoaded = true;
            });
            $scope.dataLoaded = true;
        }
    });
}]);
$(document).ready(function () {
    $('#btn-upload0').click(function (e) {
        e.preventDefault();
        $('#file0').click();
    }
    );
});