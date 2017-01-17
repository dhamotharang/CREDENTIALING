
var pdfApp = angular.module('pdfApp', []);

pdfApp.controller('PdfMappingController', ['$scope', '$rootScope', '$timeout', '$http',
    function ($scope, $rootScope, $timeout, $http) {

        $scope.profileId = 1;        
        $scope.ViewTemplate = function (id, templateName) {
            $scope.loadingAjax = true;
            
            $scope.isError = false;
                $.ajax({
                    url: rootDir + '/PlanPDF/PDFMapping/GetPDFMappingProfileData?profileId=' + id + '&templateName=' + templateName,
                    type: 'GET',                    
                    cache: false,
                    contentType: false,
                    processData: false,
                    success: function (data) {
                       
                        try {
                            $scope.loadingAjax = false;
                            var open_link = window.open('', '_blank');
                            open_link.location = rootDir + '/Document/View?path=/GeneratedTemplatePdf/' + JSON.parse(data);
                        } catch (e) {
                           
                        }
                    }
                });
        };

    }]);