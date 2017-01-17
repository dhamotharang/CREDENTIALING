//------------------AHC cd auto search Auto complete Module --------------------------------
var ahc_cd_autosearch = angular.module("ahc.cd.autosearch", []);

//--------------------- Angular Directive for Address Auto Complete ------------------------
ahc_cd_autosearch.directive('input', ['$compile', function ($compile) {
    return {
        restrict: 'AE',
        require: '?ngModel',
        link: function (scope, element, attr, ngModel) {
            if (attr.autocomplete == "addressautocomplete") {
                element.bind('focus', function () {
                    element.parent().find(".ProviderTypeSelectAutoList").first().show();
                });

                var contentTr = angular.element(
                    "<div class='ProviderTypeSelectAutoList popover fade bottom in' role='tooltip'>" +
                    "<table class='table table-striped table-bordered table-hover table-condensed marginBottomAutosearch'>" +
                    "<tbody><tr ng-repeat='location in Locations' ng-click='" + attr.selectmethod + "(location)' class='pointer'>" +
                    "<td>{{location.City}} - {{location.State}} - {{location.CountryCode}}</td></tr></tbody></table></div>");

                contentTr.insertAfter(element);
                $compile(contentTr)(scope);
            }
        }
    };
}]);


//--------------------- Angular Directive for Country Code ------------------------
ahc_cd_autosearch.directive('countryDialCode', ['$compile', function ($compile) {
    return {
        restrict: 'AE',
        link: function (scope, element, attr) {
            element.bind('click', function () {
                element.parent().next().show();
            });

            var contentTr = angular.element(
                "<div class='countryDailCodeContainer popover fade bottom in' role='tooltip' id='MobileNumberContact'>" +
                "<div class='arrow'></div><h3 class='popover-title'>Select Country Dial Code</h3>" +
                "<div class='popover-content'>" +
                "<select ng-model='" + attr.countrycodemodel + "'>" +
                "<option ng-repeat='code in CountryDailCodes' value='{{code.dial_code}}' ng-selected='code.dial_code == " + attr.countrycodemodel + "'>{{code.name}}({{code.dial_code}})</option>" +
                "</select></div></div>");

            contentTr.insertAfter(element.parent());
            $compile(contentTr)(scope);

        }
    };
}]);

//---------------------- Angualr Directive for Midlevel Practitioners ------------------------------//
ahc_cd_autosearch.directive('practitionersearch', ['$compile', function ($compile) {
    return {
        restrict: 'AE',
        link: function (scope, element, attr) {
            if (attr.autocomplete == "practitioner") {
                element.bind('focus', function () {
                    element.parent().find(".ProviderTypeSelectAutoList").first().show();
                });

                var contentTr = angular.element(
                    "<div class='ProviderTypeSelectAutoList popover fade bottom in' role='tooltip'>" +
                    "<table class='table table-striped table-bordered table-hover table-condensed marginBottomAutosearch'>" +
                    "<tbody><tr ng-repeat='practitioner in " + attr.practitioners + " | filter:practitioner' ng-click='" + attr.onselect + "(practitioner)' class='pointer'>" +
                    "<td>{{practitioner.PersonalDetail.FirstName}} {{practitioner.PersonalDetail.LastName}} ({{practitioner.OtherIdentificationNumber.NPINumber}})</td></tr>" +
                    "<tr ng-if='midLevelPractitioners.length<1'><td>No "+attr.msg+" Available</td></tr></tbody></table></div>");

                contentTr.insertAfter(element);
                $compile(contentTr)(scope);
            }
        }
    };
}]);

// ------------------------------------------ how can use in HTML ---------------------------------------------------

//------------------------- HTML code for use Address auto complete in your HTML Code -------------------------------

//<div class="col-lg-4 form-group">
//      @Html.LabelFor(model => model.HomeAddress.City, htmlAttributes: new { @class = "control-label small" })
//      @Html.EditorFor(model => model.HomeAddress.City, new { htmlAttributes = new { @class = "form-control input-sm", data_ng_model = "HomeAddress.City", placeholder = "city name", data_ng_change = "addressHomeAutocomplete(HomeAddress.City)", data_selectmethod = "selectedLocation", data_autocomplete = "addressautocomplete" } })
//      @Html.ValidationMessageFor(model => model.HomeAddress.City, "", new { @class = "text-danger" })
//</div>

// ------------------------------------------------------ END -------------------------------------------------------

// ---------------------- HTML code for use country dial code drop down in your HTML code ---------------------------

//<div class="col-lg-4 input-group">
//      <span class="input-group-btn" id="basic-addon1">
//          <button type="button" class="btn btn-default countryCodeClass" country-dial-code countrycodemodel="temp.CountryCode"> {{temp.CountryCode}} <span class="caret"></span></button>
//      </span>
//      @Html.EditorFor(model => model.ContactDetail.PhoneDetails[0].Number, new { htmlAttributes = new { @class = "form-control", data_ng_model = "ContactDetail.MobileNumber", placeholder = "mobile number", data_ng_blur = "IsExistMobileNumber(ContactDetail.CountryCode, ContactDetail.MobileNumber)" } })
//</div>

//-------------------------- end --------------------------------------