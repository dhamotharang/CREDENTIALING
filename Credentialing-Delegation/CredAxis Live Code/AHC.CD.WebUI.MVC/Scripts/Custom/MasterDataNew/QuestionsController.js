//--------------------- Angular Module ----------------------
var masterDataQuestions = angular.module("masterDataQuestions", ["ahc.cd.util", 'ui.bootstrap']);

masterDataQuestions.service('messageAlertEngine', ['$rootScope', '$timeout', function ($rootScope, $timeout) {

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

//=========================== Controller declaration ==========================
masterDataQuestions.controller('masterDatamasterDataQuestionsController', ['$scope', '$http', '$filter', 'messageAlertEngine', function ($scope, $http, $filter, messageAlertEngine) {

    //----------------------------Get Questions------------------------------------------
    $http.get(rootDir + "/MasterDataNew/GetAllQuestions").then(function (value) {
        
        try {
            for (var i = 0; i < value.data.length ; i++) {
                if (value.data[i] != null) {
                    value.data[i].LastModifiedDate = $scope.ConvertDateFormat(value.data[i].LastModifiedDate);
                }
            }

            $scope.Questions = angular.copy(value.data);
        } catch (e) {
            throw e;
        }
    });


    //----------------------------Get Questions Category------------------------------------------
    $http.get(rootDir + "/MasterDataNew/GetAllQuestionCategories").then(function (value) {
       
        for (var i = 0; i < value.data.length ; i++) {
            if (value.data[i] != null) {
                value.data[i].LastModifiedDate = $scope.ConvertDateFormat(value.data[i].LastModifiedDate);
            }
        }

        $scope.QuestionCategories = angular.copy(value.data);        
    });

    //Convert the date from database to normal
    $scope.ConvertDateFormat = function (value) {
        var returnValue = value;
        var dateData = "";
        try {
            if (value.indexOf("/Date(") == 0) {
                returnValue = new Date(parseInt(value.replace("/Date(", "").replace(")/", ""), 10));
                var day = returnValue.getDate() < 10 ? '0' + returnValue.getDate() : returnValue.getDate();
                var tempo = returnValue.getMonth() + 1;
                var month = tempo < 10 ? '0' + tempo : tempo;
                var year = returnValue.getFullYear();
                dateData = month + "-" + day + "-" + year;
            }
            return dateData;
        } catch (e) {
            return dateData;
        }
        return dateData;
    };

    

    //------------------- data init -------------------
    $scope.tempQuestion = {};
    $scope.tempQC = {};
    $scope.tempQT = {};

    //------------ Question Template return -------------------
    $scope.getHospitalTemplate = function (hospital) {
        if (hospital.QuestionID === $scope.tempQuestion.QuestionID) return 'editQuestion';
        else return 'displayQuestion';
    };

    
    $scope.getQCTemplate = function (qc) {
        if (qc.QuestionCategoryID === $scope.tempQC.QuestionCategoryID) return 'editQC';
        else return 'displayQC';
    };

    
    $scope.getQTTemplate = function (qt) {
        if (qt.QuestionThemeID === $scope.tempQT.QuestionThemeID) return 'editQT';
        else return 'displayQT';
    };

    //-------------------- Edit Group ----------
    $scope.editQuestion = function (hospital) {
        $scope.reset();
        $scope.disableAdd = true;
        $scope.tempQuestion = angular.copy(hospital);
    };

    //------------------- Add Group ---------------------
    $scope.addQuestion = function () {
        $scope.disableAdd = true;
        var Month = new Date().getMonth() + 1;
        var _month = Month < 10 ? '0' + Month : Month;
        var _date = new Date().getDate() < 10 ? '0' + new Date().getDate() : new Date().getDate();
        var _year = new Date().getFullYear();
        $scope.categoryerror = false;
        var temp = {
            QuestionID: 0,
            QuestionCategory:{
                QuestionCategoryID: 0,
                Title: "",
            },
            Title: "",
            Status: "Active",            
            LastModifiedDate: _month + "-" + _date + "-" + _year
        };
        $scope.Questions.splice(0, 0, angular.copy(temp));
        $scope.tempQuestion = angular.copy(temp);
    };
    $scope.questionError = false;
    //------------------- Save Question ---------------------
    $scope.saveQuestion = function (idx) {

        var addData = {
            QuestionID: 0,
            QuestionCategoryId: $scope.tempQuestion.QuestionCategory.QuestionCategoryID,
            Title: $scope.tempQuestion.Title,
            StatusType: 1,
            
        };

        if (!addData.Title) { $scope.questionError = true; };
        if (!addData.QuestionCategoryId) {
            $scope.categoryerror = true;
            //$scope.categoryError = "Please select the Question Category";
        };

        if (addData.Title && addData.QuestionCategoryId){
            $http.post(rootDir + '/MasterDataNew/AddQuestion', addData).
            success(function (data, status, headers, config) {
                try {
                    //----------- success message -----------
                    if (data.status == "true") {
                        messageAlertEngine.callAlertMessage("Question", "New Question Details Added Successfully !!!!", "success", true);
                        data.questionDetails.LastModifiedDate = $scope.ConvertDateFormat(data.questionDetails.LastModifiedDate);
                        var obj = $filter('filter')($scope.QuestionCategories, { QuestionCategoryID: data.questionDetails.QuestionCategoryId })[0];

                        for (var i = 0; i < $scope.Questions.length; i++) {

                            if ($scope.Questions[i].QuestionID == 0) {
                                $scope.Questions[i] = angular.copy(data.questionDetails);
                                $scope.Questions[i].QuestionCategory = obj;
                            }
                        }
                        $scope.reset();
                    }
                } catch (e) {

                }
            }).
            error(function (data, status, headers, config) {
                //----------- error message -----------
                messageAlertEngine.callAlertMessage("QuestionError", "Sorry Unable To Add Question !!!!", "danger", true);
            });
        }
        else {
           // $scope.categoryerror = false;

        }
        
    };

    //------------------- Update Question ---------------------
    $scope.updateQuestion = function (idx) {

        var updateData = {
            QuestionID: $scope.tempQuestion.QuestionID,
            QuestionCategoryId: $scope.tempQuestion.QuestionCategory.QuestionCategoryID,
            Title: $scope.tempQuestion.Title,
            StatusType: 1,
            
        };

        if (!updateData.Title) { $scope.questionError = true; };
        if (!updateData.QuestionCategoryId) { $scope.categoryError = "Please select the Question Category"; };

        if (updateData.Title && updateData.QuestionCategoryId) {
            $http.post(rootDir + '/MasterDataNew/UpdateQuestion', updateData).
                success(function (data, status, headers, config) {
                    try {
                        //----------- success message -----------
                        if (data.status == "true") {
                            messageAlertEngine.callAlertMessage("Question", "Question Details Updated Successfully !!!!", "success", true);
                            data.questionDetails.LastModifiedDate = $scope.ConvertDateFormat(data.questionDetails.LastModifiedDate);
                            var obj = $filter('filter')($scope.QuestionCategories, { QuestionCategoryID: data.questionDetails.QuestionCategoryId })[0];
                            for (var i = 0; i < $scope.Questions.length; i++) {

                                if ($scope.Questions[i].QuestionID == data.questionDetails.QuestionID) {
                                    $scope.Questions[i] = angular.copy(data.questionDetails);
                                    $scope.Questions[i].QuestionCategory = obj;
                                }
                            }
                            $scope.reset();
                        }
                    } catch (e) {
                     
                    }
                }).
                error(function (data, status, headers, config) {
                    //----------- error message -----------
                    messageAlertEngine.callAlertMessage("QuestionError", "Sorry Unable To Update Question !!!!", "danger", true);
                });
        }

    };
    
    //----------- add new data ----------------
    $scope.addQC = function () {
        $scope.disableAdd = true;
        var temp = {
            QuestionCategoryID: 0,
            Title: "",
            Status: "Active",
            LastModifiedDate: null
        };

        $scope.QuestionCategories.splice(0, 0, angular.copy(temp));
        $scope.tempQC = angular.copy(temp);
    };

    //-------------------- Edit Group ----------
    $scope.editQC = function (qc) {
        $scope.disableAdd = false;
        $scope.tempQC = angular.copy(qc);
    };
    $scope.existErr = "";
    $scope.existErr11 = false;
    //------------------- Save Question Category ---------------------
    $scope.saveQC = function (idx) {

        var addData = {
            QuestionCategoryID: 0,
            Title: $scope.tempQC.Title,
            StatusType: 1,
            
        };

         $scope.isExist = true;

        for (var i = 0; i < $scope.QuestionCategories.length; i++) {

            if (addData.Title && $scope.QuestionCategories[i].Title.replace(" ", "").toLowerCase() == addData.Title.replace(" ", "").toLowerCase()) {

                $scope.isExist = false;
                $scope.existErr11 = true;
                $scope.existErr = "The Title is already Exist";
                break;
            }
        }
        if (!addData.Title) {
            $scope.isExist = false;
            $scope.questionCategoryError = "Please enter the Question Category";
        };
        if ($scope.isExist) {
        if (addData.Title){
            $http.post(rootDir + '/MasterDataNew/AddQuestionCategory', addData).
            success(function (data, status, headers, config) {
                try {
                    //----------- success message -----------
                    if (data.status == "true") {
                        messageAlertEngine.callAlertMessage("QuestionCategory", "New QuestionCategory Details Added Successfully !!!!", "success", true);
                        data.questionCategoryDetails.LastModifiedDate = $scope.ConvertDateFormat(data.questionCategoryDetails.LastModifiedDate);
                        for (var i = 0; i < $scope.QuestionCategories.length; i++) {

                            if ($scope.QuestionCategories[i].QuestionCategoryID == 0) {
                                $scope.QuestionCategories[i] = angular.copy(data.questionCategoryDetails);
                            }
                        }
                       
                    }
                    else {
                        messageAlertEngine.callAlertMessage("QuestionCategoryError", "Sorry Unable To Add QuestionCategory !!!!", "danger", true);
                        $scope.QuestionCategories.splice(0, 1);
                    }
                    $scope.reset();
                } catch (e) {
                  
                }
            }).
            error(function (data, status, headers, config) {
                //----------- error message -----------
                messageAlertEngine.callAlertMessage("QuestionCategoryError", "Sorry Unable To Add QuestionCategory !!!!", "danger", true);
                $scope.QuestionCategories.splice(0, 1);
            });
        }
   
        }
    };

    //------------------- Update Question Category ---------------------
    $scope.updateQC = function (idx) {

        var updateData = {
            QuestionCategoryID: $scope.tempQC.QuestionCategoryID,
            Title: $scope.tempQC.Title,
            StatusType: 1,
            
        };
        if (!updateData.Title) { $scope.questionCategoryError = "Please enter the Question Category"; };
        if (updateData.Title){
            $http.post(rootDir + '/MasterDataNew/UpdateQuestionCategory', updateData).
            success(function (data, status, headers, config) {
                try {
                    //----------- success message -----------
                    if (data.status == "true") {
                        messageAlertEngine.callAlertMessage("QuestionCategory", "QuestionCategory Details Updated Successfully !!!!", "success", true);
                        data.questionCategoryDetails.LastModifiedDate = $scope.ConvertDateFormat(data.questionCategoryDetails.LastModifiedDate);
                        for (var i = 0; i < $scope.QuestionCategories.length; i++) {

                            if ($scope.QuestionCategories[i].QuestionCategoryID == data.questionCategoryDetails.QuestionCategoryID) {
                                $scope.QuestionCategories[i] = angular.copy(data.questionCategoryDetails);
                            }
                        }
                        $scope.reset();
                    }
                } catch (e) {
                  
                }
            }).
            error(function (data, status, headers, config) {
                //----------- error message -----------
                messageAlertEngine.callAlertMessage("QuestionCategoryError", "Sorry Unable To Update QuestionCategory !!!!", "danger", true);
            });
        }
    };

    //----------------- Group new add cancel ---------------
    $scope.cancelAdd = function (data) {
        $scope.questionError = false;
        $scope.categoryError = false;
        $scope.disableAdd = false;
        data.splice(0, 1);
        $scope.tempQT = {};
        $scope.questionCategoryError = "";
        $scope.existErr = "";
    };

    //-------------------- Reset Group ----------------------
    $scope.reset = function () {
        $scope.existErr = "";
        $scope.disableAdd = false;
        $scope.tempQuestion = {};
        $scope.tempQC = {};
        $scope.questionCategoryError = "";
        $scope.questionError = false;
        $scope.categoryerror = false;

    };

    //------------------------ hide show manage ----------------------
    $scope.viewQuestions = true;

    $scope.showQuestions = function () {
        $scope.viewQuestions = true;
        $scope.viewQuestionCategories = false;
    };

    $scope.showQuestionCategories = function () {
        $scope.viewQuestions = false;
        $scope.viewQuestionCategories = true;
    };

    //----------------- get Question Category ---------------------
    $scope.getQuestionCategory = function (categoryId) {
        for (var i in $scope.QuestionCategories) {
            if (categoryId == $scope.QuestionCategories[i].QuestionCategoryID) {
                $scope.tempQuestion.QuestionCategory = $scope.QuestionCategories[i];
                break;
            }
        }
    };

    $scope.CurrentPage = [];

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
        $scope.CurrentPage = [];
        var startIndex = (newValue - 1) * 10;
        var endIndex = startIndex + 9;
        if ($scope.Questions) {
            for (startIndex; startIndex <= endIndex ; startIndex++) {
                if ($scope.Questions[startIndex]) {
                    $scope.CurrentPage.push($scope.Questions[startIndex]);
                } else {
                    break;
                }
            }
        }
    });
    //-------------- License Scope Watch ---------------------
    $scope.$watchCollection('Questions', function (newValue, oldValue) {
        if (newValue) {
            $scope.bigTotalItems = newValue.length;

            $scope.CurrentPage = [];
            $scope.bigCurrentPage = 1;

            var startIndex = ($scope.bigCurrentPage - 1) * 10;
            var endIndex = startIndex + 9;

            for (startIndex; startIndex <= endIndex ; startIndex++) {
                if ($scope.Questions[startIndex]) {
                    $scope.CurrentPage.push($scope.Questions[startIndex]);
                } else {
                    break;
                }
            }
        }
    });
    //------------------- end ------------------

    $scope.CurrentPage1 = [];

    //-------------------------- angular bootstrap pagger with custom-----------------
    $scope.maxSize1 = 5;
    $scope.bigTotalItems1 = 0;
    $scope.bigCurrentPage1 = 1;

    //-------------------- page change action ---------------------
    $scope.pageChanged1 = function (pagnumber) {
        $scope.bigCurrentPage1 = pagnumber;
    };

    //-------------- current page change Scope Watch ---------------------
    $scope.$watch('bigCurrentPage1', function (newValue, oldValue) {
        $scope.CurrentPage1 = [];
        var startIndex = (newValue - 1) * 10;
        var endIndex = startIndex + 9;
        if ($scope.QuestionCategories) {
            for (startIndex; startIndex <= endIndex ; startIndex++) {
                if ($scope.QuestionCategories[startIndex]) {
                    $scope.CurrentPage1.push($scope.QuestionCategories[startIndex]);
                } else {
                    break;
                }
            }
        }
    });
    //-------------- License Scope Watch ---------------------
    $scope.$watchCollection('QuestionCategories', function (newValue, oldValue) {
        if (newValue) {
            $scope.bigTotalItems1 = newValue.length;

            $scope.CurrentPage1 = [];
            $scope.bigCurrentPage1 = 1;

            var startIndex = ($scope.bigCurrentPage1 - 1) * 10;
            var endIndex = startIndex + 9;

            for (startIndex; startIndex <= endIndex ; startIndex++) {
                if ($scope.QuestionCategories[startIndex]) {
                    $scope.CurrentPage1.push($scope.QuestionCategories[startIndex]);
                } else {
                    break;
                }
            }
        }
    });
    $scope.filterData = function () {
        $scope.pageChanged(1);
    }

    $scope.IsValidIndex = function (index) {

        var startIndex = ($scope.bigCurrentPage - 1) * 10;
        var endIndex = startIndex + 9;

        if (index >= startIndex && index <= endIndex)
            return true;
        else
            return false;

    }
    //------------------- end ------------------

}]);
