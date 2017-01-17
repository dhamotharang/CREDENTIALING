angular.module('editPlanContractApp', []).controller('PlanContractController', function ($http, $scope) {

    $scope.BE_LOB = [{ LOBName: 'lob1', IsChecked: false, BE: [{ BEName: 'BE1', IsChecked: false }, { BEName: 'BE2', IsChecked: true }, { BEName: 'BE3', IsChecked: false }] },
   { LOBName: 'lob2', IsChecked: false, BE: [{ BEName: 'BE1', IsChecked: true }, { BEName: 'BE2', IsChecked: false }, { BEName: 'BE3', IsChecked: false }] },
   { LOBName: 'lob3', IsChecked: false, BE: [{ BEName: 'BE1', IsChecked: false }, { BEName: 'BE2', IsChecked: true }, { BEName: 'BE3', IsChecked: true }] }];

    $scope.selectedPlan = 'Plan1';
    $scope.popover = { content: null };

    //pass LOB & BE as  parameters to function and assign to variable
    $scope.loadTemplate = function () {
        var Street = "Street1";
        var Appartment = "Apartmennt 1";
        var City = "Belgaum";
        var State = "Karnataka";
        var Zip = "123456";
        var Country = "India";
        var County = "county1";
        var ContactPerson = "Pritam kurunnkar";
        var Phone = "123456";
        var Fax = "12345678";
        var Email = "pkpritam@gmail.com";

        $scope.first = "off";
        $scope.oncheckboxclick = function () {
            if ($scope.first == "off") {
                return;
            }

            
        }
        $scope.popover = {
            content: '<a class="btn pull-right close-popover">X</a>' +
                    '<div class="row" ng-click="oncheckboxclick()">' +
                    '<div class="col-md-12 form-group"  ng-click="oncheckboxclick()">' +
                    '<fieldset  ng-click="oncheckboxclick()"><legend>Address Details</legend>' +
                    '<input ng-model="first" type="checkbox" style="display:none" /><input ng-model="first" type="checkbox" /><label>&nbsp;Same As Plan Address Details</label>' +
                    '<div class="row">' +
                    '<div class="col-md-4 form-group">' +
                   '<label class="control-label small" for="SalutationType">Street/P.O Box:</label>' +
                '<div><i class="data-label">' + Street + '</i></div>' +
                '</div>' +
                '<div class="col-md-4 form-group">' +
                   '<label class="control-label small" for="SalutationType">Appartment/Unit No.:</label>' +
                '<div>' +
                    '<i class="data-label">' + Appartment + '</i>' +
                '</div>' +
                '</div>' +
                '<div class="col-md-4 form-group">' +
                    '<label class="control-label small" for="SalutationType">City:</label>' +
                '<div>' +
                    '<i class="data-label">' + City + '</i>' +
                '</div>' +
                '</div>' +
                '</div><div class="row">' +
                '<div class="col-md-4 form-group">' +
                   '<label class="control-label small" for="SalutationType">State:</label>' +
                '<div>' +
                    '<i class="data-label">' + State + '</i>' +
                '</div>' +
                '</div>' +
                '<div class="col-md-4 form-group">' +
                   '<label class="control-label small" for="SalutationType">Zip Code:</label>' +
                '<div>' +
                    '<i class="data-label">' + Zip + '</i>' +
                '</div>' +
                '</div>' +
                '<div class="col-md-4 form-group">' +
                   '<label class="control-label small" for="SalutationType">Country:</label>' +
                '<div>' +
                    '<i class="data-label">' + Country + '</i>' +
                '</div>' +
                '</div>' +
                '</div><div class="row">' +
                '<div class="col-md-6 form-group">' +
                   '<label class="control-label small" for="SalutationType">County:</label>' +
                '<div>' +
                    '<i class="data-label">' + County + '</i>' +
                '</div>' +
                '</div>' +
                 '</div>' +
                 '</div>' +
                '</fieldset>' +
                '</div></div>' +
                   '<div class="row">' +
                                '<div class="col-md-12 form-group">' +
                                '<fieldset><legend>Contact Details</legend>' +
                                '<input type="checkbox" /><label>&nbsp;Same As Plan Contact Details</label>' +
                                '<div class="row">' +
                                '<div class="col-md-4 form-group">' +
                   '<label class="control-label small" for="SalutationType">Contact Person Name:</label>' +
                '<div><i class="data-label">' + ContactPerson + '</i></div>' +
                '</div>' +
                '<div class="col-md-4 form-group">' +
                   '<label class="control-label small" for="SalutationType">Phone Number:</label>' +
                '<div>' +
                    '<i class="data-label">' + Phone + '</i>' +
                '</div>' +
                '</div>' +

                '<div class="col-md-4 form-group">' +
                    '<label class="control-label small" for="SalutationType">Fax Number:</label>' +
                '<div>' +
                    '<i class="data-label">' + Fax + '</i>' +
                '</div>' +
                '</div>' +
                '</div><div class="row">' +
                '<div class="col-md-6 form-group">' +
                   '<label class="control-label small" for="SalutationType">Email Address:</label>' +
                '<div>' +
                    '<i class="data-label">' + Email + '</i>' +
                '</div>' +
                '</div>' +
        '</div>' +
        '</div>' +
    '</fieldset>' +
 '<a href="#" class="btn btn-sm btn-warning close-popover" style="margin-left:10px">Cancel</a>' +
 '<a href="#" class="btn btn-sm btn-primary pull-right close-popover" style="margin-right:10px">Update</a>' +
        '</div></div>' +
                '<script>$(".close-popover").click(function () {$("[data-toggle=\'popover\']").popover("hide");});</script>'
        };
    }
});