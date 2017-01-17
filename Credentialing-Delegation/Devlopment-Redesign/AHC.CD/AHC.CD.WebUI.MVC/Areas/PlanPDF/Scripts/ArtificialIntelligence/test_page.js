(function() {
  var app, deps;

  deps = ['angularBootstrapNavTree'];

  if (angular.version.full.indexOf("1.2") >= 0) {
    deps.push('ngAnimate');
  }

  app = angular.module('AbnTest', deps);

  app.controller('AbnTestController', function($scope, $timeout,$http) {
    var apple_selected, tree, treedata_avm, treedata_geography;
    $scope.my_tree_handler = function(branch) {
      var _ref;
      $scope.output = branch.label;
      if ((_ref = branch.data) != null ? _ref.description : void 0) {
        return $scope.output += '(' + branch.data.description + ')';
      }
    };
    apple_selected = function(branch) {
      return $scope.output = branch.label;
    };    

    $scope.isAddPlanForm = false;
    $scope.tempObject = {};

    treedata_avm = [
      {
        label: 'Demographics',
        children: [
          //{
          //  label: 'Dog',
          //  data: {
          //    description: "man's best friend"
          //  }
          //}, {
          //  label: 'Cat',
          //  data: {
          //    description: "Felis catus"
          //  }
          //}, {
          //  label: 'Hippopotamus',
          //  data: {
          //    description: "hungry, hungry"
          //  }
          //}, 
          {
              label: 'Birth Information',
              children: ['Date of Birth', 'City of Birth', 'County of Birth']
          },
          {
            label: 'Personal Detail',
            children: ['First Name', 'Middle Name', 'Last Name']
          }
        ]
      }, {
        label: 'Identification/Licence',
        data: {
          definition: "A plant or part of a plant used as food, typically as accompaniment to meat or fish, such as a cabbage, potato, carrot, or bean.",
          data_can_contain_anything: true
        },
        onSelect: function(branch) {
          return $scope.output =branch.data.definition;
        },
        children: [
          {
            label: 'UIN Number'
          }, {
            label: 'Other Identification Number',
            children: [
              {
                label: 'NPI',
                onSelect: apple_selected
              }, {
                label: 'CAQH',
                onSelect: apple_selected
              }, {
                label: 'UPIN Number',
                onSelect: apple_selected
              }
            ]
          }
        ]
      }, {
        label: 'Education History',
        children: [
          {
              label: 'UG Schools Details',
              children: ['School', 'Degree Awarded']
          }, {
              label: 'ECFMG Detail',
              children: ['ECFMG Number', 'ECFMG Issue Date', 'Supporting Document']
          }
          //, {
          //  label: 'Plastic',
          //  children: [
          //    {
          //      label: 'Thermoplastic',
          //      children: ['polyethylene', 'polypropylene', 'polystyrene', ' polyvinyl chloride']
          //    }, {
          //      label: 'Thermosetting Polymer',
          //      children: ['polyester', 'polyurethane', 'vulcanized rubber', 'bakelite', 'urea-formaldehyde']
          //    }
          //  ]
          //}
        ]
      }
    ];
    treedata_geography = [
      {
        label: 'North America',
        children: [
          {
            label: 'Canada',
            children: ['Toronto', 'Vancouver']
          }, {
            label: 'USA',
            children: ['New York', 'Los Angeles']
          }, {
            label: 'Mexico',
            children: ['Mexico City', 'Guadalajara']
          }
        ]
      }, {
        label: 'South America',
        children: [
          {
            label: 'Venezuela',
            children: ['Caracas', 'Maracaibo']
          }, {
            label: 'Brazil',
            children: ['Sao Paulo', 'Rio de Janeiro']
          }, {
            label: 'Argentina',
            children: ['Buenos Aires', 'Cordoba']
          }
        ]
      }
    ];

    $scope.PlanFormFields = {
        fields: [
            //{
            //    name: "Provider Name"
            //},
            //{
            //    name: "Date of Birth"
            //},
            //{
            //    name: "City of Birth"
            //},
            //{
            //    name: "Provider NPI"
            //},
            //{
            //    name: "Provider CAQH"
            //},
            //{
            //    name: "Provider License Number"
            //},
            //{
            //    name: "Provider License Type"
            //},
            //{
            //    name: "Provider ECFMG Number"
            //},
            //{
            //    name: "Provider ECFMG Issue Date"
            //},
            //{
            //    name: "Provider School"
            //},
            //{
            //    name: "Provider Degree Awarded"
            //}
        ]
    };

    $scope.PlanVariable = '';
    $scope.GenericVariable = '';
    $scope.planFormPath = '';
    $scope.selectedGenericVariableList = [];
    $scope.selectedPlanVariableList = [];
    $scope.selectedVariables = [{GenericVariable:'',PlanVariable:''}];

    $scope.GetAllPdfField = function (FormName) {
        $http.get(rootDir + '/PlanPDF/ArtificialIntelligence/GetAllPdfFields?PlanFormName=' + FormName).
      success(function (data, status, headers, config) {

          try {
              $scope.PlanFormFields.fields = angular.copy(data);
              console.log($scope.PlanFormFields.fields);
          } catch (e) {

          }
      }).
      error(function (data, status, headers, config) {

      });
    }

      $http.get(rootDir + '/PlanPDF/ArtificialIntelligence/GetAllPlanForms').
      success(function (data, status, headers, config) {

          try {
              $scope.PlanFormList = angular.copy(data);
              console.log($scope.PlanFormList);
          } catch (e) {

          }
      }).
      error(function (data, status, headers, config) {

      });

      $scope.isPlanFormRender = false;
      $scope.TemplateName='';

      $scope.DisplayPlanForm = function (filePath, FileName) {
          $scope.isPlanFormRender = true;
          $scope.planFormPath = filePath;
          $scope.TemplateName=FileName;
          $scope.GetAllPdfField(FileName);
          //var open_link = window.open('', '_blank');
          //open_link.location = $scope.planFormPath;
          console.log($scope.TemplateName);
      }

    //  $scope.CreateXmlDocument = function () {
    //      $http.get(rootDir + '/PlanPDF/ArtificialIntelligence/CreatePlanFormXml?PlanFormName=' + "ATPL" + '?GenericVariableList=' + $scope.selectedGenericVariableList + '?PlanVariableList=' + $scope.selectedPlanVariableList).
    //success(function (data, status, headers, config) {

    //    try {
    //        $scope.PlanFormFields.fields = angular.copy(data);
    //        $scope.isXml = true;
    //        console.log('xml file');
    //        console.log($scope.PlanFormFields.fields);
    //    } catch (e) {

    //    }
    //}).
    //error(function (data, status, headers, config) {

    //});
      //  }

      $scope.filterFieldsData = function () {
          $scope.selectedGenericVariableList = [];
          $scope.selectedPlanVariableList = [];
          for (var i = 0; i < $scope.selectedVariables.length; i++) {
              $scope.selectedGenericVariableList.push($scope.selectedVariables[i].GenericVariable);
              $scope.selectedPlanVariableList.push($scope.selectedVariables[i].PlanVariable);
          }
      }
      $scope.createdXml = '';
      $scope.CreateXmlDocument = function () {
          $scope.filterFieldsData();
          $http({
              url: rootDir + '/PlanPDF/ArtificialIntelligence/CreatePlanFormXml',
              method: "GET",
              params: { PlanFormName: $scope.TemplateName, GenericVariableList: $scope.selectedGenericVariableList, PlanVariableList: $scope.selectedPlanVariableList }
          }).success(function (data, status, headers, config) {

              try {
                  $scope.selectedVariables = angular.copy(data);
                  //$scope.PlanFormFields.fields = angular.copy(data);
                  $scope.createdXml = angular.copy(data);
                  $scope.isXml = true;
                  console.log('xml file');
                  console.log($scope.selectedVariables);
                  $scope.selectedVariables = [];
                  //var open_link = window.open('', '_blank');
                  //open_link.location = data;
              } catch (e) {

              }
          }).
    error(function (data, status, headers, config) {

    });
      }

      $scope.RemoveMappingCombination = function (j) {
          $scope.selectedVariables.splice($scope.selectedVariables.indexOf(j), 1);
      }
              
    $scope.XmlFields = {
        rootStart: '<Root>',
        rootEnd: '</Root>',
        templateStart: '<Template Name="' + $scope.TemplateName + '">',
        templateEnd: '</Template>',
        propertiesStart: '<Properties>',
        propertiesEnd: '</Properties>',
        properties1: '<Property GenericVariable="',
        properties2: '" PlanVariable="',
        properties3: '"></Property>',
        fields: [
            {
                GenericVariable: "",
                PlanVariable: ""
            }
        ]
    };

    $scope.AddFormVarToXmlFields = function (plan) {
        $scope.selectedVariables.push({ "GenericVariable": $scope.output, "PlanVariable": plan });
        $scope.XmlFields.fields.push({ "GenericVariable": $scope.output, "PlanVariable": plan });
        $scope.PlanVariable = plan;
        $scope.GenericVariable = $scope.output;        
        $scope.selectedGenericVariableList.push($scope.GenericVariable);
        $scope.selectedPlanVariableList.push($scope.PlanVariable);

        console.log($scope.XmlFields.fields);
    }
    $scope.isXml = false;
    $scope.GenerateXML = function () {    
        $scope.isXml = true;
        console.log($scope.XmlFields.fields);
    }
    $scope.Cancel = function () {
        $scope.isXml = false;
    }
    $scope.isAddEditPlanForm = false;
    $scope.AddEditPlanForm = function () {
        $scope.isAddEditPlanForm = true;
        $scope.isPlanFormRender = false;
    }

    $scope.setFiles = function (file) {
        $(file).parent().parent().find(".jancyFileWrapTexts").find("span").width($(file).parent().parent().width() < 243 ? $(file).parent().parent().width() : 243);
    }

    $scope.cancelPlanForm = function () {
        $scope.isAddEditPlanForm = false;
        $scope.isPlanFormRender = false;
        $scope.tempObject = {};
    }

    $scope.savePlanForm = function (Form_Div_Id) {
        var $form = $('#' + Form_Div_Id);
        //ResetFormForValidation($form);
        $scope.isError = false;

        if (true) {
            $.ajax({
                url: rootDir + '/PlanPDF/ArtificialIntelligence/AddPlanForm',
                type: 'POST',
                data: new FormData($form[0]),

                async: false,
                cache: false,
                contentType: false,
                processData: false,
                success: function (data) {
                    console.log('saved plan form');
                    console.log(data);
                    $scope.tempObject = {};
                    $scope.cancelPlanForm();
                    //messageAlertEngine.callAlertMessage('addedNewProvider', 'Provider is saved successfully.', 'success', true);
                }
            });
        } else {
            //messageAlertEngine.callAlertMessage('errorInitiated', "", "danger", true);
            //$scope.errorInitiated = "Sorry for Inconvenience !!!! Please Try Again Later...";
            //messageAlertEngine.callAlertMessage('addedNewProvider', 'Error occured during adding Provider.', 'danger', true);
        }
    }    

    $scope.my_data = treedata_avm;
    $scope.try_changing_the_tree_data = function() {
      if ($scope.my_data === treedata_avm) {
        return $scope.my_data = treedata_geography;
      } else {
        return $scope.my_data = treedata_avm;
      }
    };
    $scope.my_tree = tree = {};
    $scope.try_async_load = function() {
      $scope.my_data = [];
      $scope.doing_async = true;
      return $timeout(function() {
        if (Math.random() < 0.5) {
          $scope.my_data = treedata_avm;
        } else {
          $scope.my_data = treedata_geography;
        }
        $scope.doing_async = false;
        return tree.expand_all();
      }, 1000);
    };
    return $scope.try_adding_a_branch = function() {
      var b;
      b = tree.get_selected_branch();
      return tree.add_branch(b, {
        label: 'New Branch',
        data: {
          something: 42,
          "else": 43
        }
      });
    };
  });

}).call(this);
