//------------------AHC cd util common Module --------------------------------
var ahc_cd_util = angular.module("ahc.cd.util", []);

//--------------------- Tool tip Directive ------------------------------
ahc_cd_util.directive('tooltip', function () {
    return function (scope, elem) {
        elem.tooltip();
    };
});



// ---------------------- HTML code for use tooltip in your HTML code ---------------------------

// <button ng-click="addGroup()" class="btn btn-xs btn-success" tooltip data-toggle="tooltip" data-placement="left" title="Tooltip on left"><i class="fa fa-plus"></i> Add</button>

//-------------------------- end --------------------------------------