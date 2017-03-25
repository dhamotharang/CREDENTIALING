//var intelligenceApp = angular.module('IntelligenceApp', ['ngTable']);


//intelligenceApp.service('messageAlertEngine', ['$rootScope', '$timeout', function ($rootScope, $timeout) {

//    $rootScope.messageDesc = "";
//    $rootScope.activeMessageDiv = "";
//    $rootScope.messageType = "";

//    var animateMessageAlertOff = function () {
//        $rootScope.closeAlertMessage();
//    };


//    this.callAlertMessage = function (calledDiv, msg, msgType, dismissal) { //messageAlertEngine.callAlertMessage('updateHospitalPrivilege' + IndexValue, "Data Updated Successfully !!!!", "success", true);                            
//        $rootScope.activeMessageDiv = calledDiv;
//        $rootScope.messageDesc = msg;
//        $rootScope.messageType = msgType;
//        if (dismissal) {
//            $timeout(animateMessageAlertOff, 5000);
//        }
//    }

//    $rootScope.closeAlertMessage = function () {
//        $rootScope.messageDesc = "";
//        $rootScope.activeMessageDiv = "";
//        $rootScope.messageType = "";
//    }
//}])

//initCredApp.controller('IntelligenceController', ['$scope', '$http', '$timeout', '$filter', 'ngTableParams', 'messageAlertEngine', function ($scope, $http, $timeout, $filter, ngTableParams, messageAlertEngine) {


//}]);

var appBo = angular.module('appBo', []);

appBo.directive('tree', function () {
    return {
        restrict: 'E', // tells Angular to apply this to only html tag that is <tree>
        replace: true, // tells Angular to replace <tree> by the whole template
        scope: {
            t: '=src' // create an isolated scope variable 't' and pass 'src' to it.  
        },
        template: '<ul><branch ng-repeat="c in t.children" src="c"></branch></ul>'
    };
})

appBo.directive('branch', function ($compile) {
    return {
        restrict: 'E', // tells Angular to apply this to only html tag that is <branch>
        replace: true, // tells Angular to replace <branch> by the whole template
        scope: {
            b: '=src' // create an isolated scope variable 'b' and pass 'src' to it.  
        },
        template: '<li><a>{{ b.name }}</a></li>',
        link: function (scope, element, attrs) {
            //// Check if there are any children, otherwise we'll have infinite execution

            var has_children = angular.isArray(scope.b.children);

            //// Manipulate HTML in DOM
            if (has_children) {
                element.append('<tree src="b"></tree>');

                // recompile Angular because of manual appending
                $compile(element.contents())(scope);
            }

            //// Bind events
            element.on('click', function (event) {
                event.stopPropagation();

                if (has_children) {
                    element.toggleClass('collapsed');
                }
            });
        }
    };
})

appBo.controller('TreelistController', function ($scope) {

    $scope.ProfileData = {
        children: [
          {
              name: "Demographics",
              children: [
                {
                    name: "Personal Detail",
                    children: [
                      {
                          name: "First Name"
                      },
                      {
                          name: "Last Name"
                      }
                    ]
                },
                {
                    name: "Birth Information",
                    children: [
                      {
                          name: "Date of Birth"
                      },
                      {
                          name: "City of Birth"
                      }
                    ]
                }
              ]
          },
          {
              name: "Identification/Licences",
              children: [
                {
                    name: "Other Identification Numbers",
                    children: [
                      {
                          name: "NPI"
                      },
                      {
                          name: "CAQH"
                      }
                    ]
                },
                {
                    name: "State License",
                    children: [
                      {
                          name: "License Number"
                      },
                      {
                          name: "License Type"
                      }
                    ]
                }
              ]
          },
          {
              name: "Education History",
              children: [
                {
                    name: "ECFMG Detail",
                    children: [
                      {
                          name: "ECFMG Number"
                      },
                      {
                          name: "ECFMG Issue Date"
                      }
                    ]
                },
                {
                    name: "Graduate/Medical School Details",
                    children: [
                      {
                          name: "School"
                      },
                      {
                          name: "Degree Awarded"
                      }
                    ]
                }
              ]
          }
        ]
    };

    $scope.PlanFormFields = {
        fields: [
            {
                name: "Provider Name"
            },
            {
                name: "Date of Birth"
            },
            {
                name: "City of Birth"
            },
            {
                name: "Provider NPI"
            },
            {
                name: "Provider CAQH"
            },
            {
                name: "Provider License Number"
            },
            {
                name: "Provider License Type"
            },
            {
                name: "Provider ECFMG Number"
            },
            {
                name: "Provider ECFMG Issue Date"
            },
            {
                name: "Provider School"
            },
            {
                name: "Provider Degree Awarded"
            }
        ]
    };

    $scope.PlanVariable = '';
    $scope.GenericVariable = '';

    $scope.XmlFields = {
        fields: [
            {
                GenericVariable:"",
                PlanVariable: ""
            }
        ]
    };

    $scope.AddFormVarToXmlFields = function (plan) {
        //$scope.XmlFields.fields.push({ "GenericVariable": "", "PlanVariable": plan });
        $scope.PlanVariable = plan;
    }

});
