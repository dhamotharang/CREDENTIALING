using System.Web;
using System.Web.Optimization;

namespace AHC.CD.WebUI.MVC
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            BundleTable.EnableOptimizations = true;

            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/Lib/Jquery/jquery-1.11.0.js"));

            bundles.Add(new ScriptBundle("~/bundles/angular").Include(
                        "~/Scripts/Lib/Angular/angular.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                        "~/Scripts/Lib/Bootstrap/bootstrap.min.js"));


            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/Lib/Jquery/modernizr-2.6.2.js"));

            bundles.Add(new ScriptBundle("~/bundles/app").Include(
                        "~/Scripts/Shared/app.js"));

            bundles.Add(new ScriptBundle("~/bundles/metisMenu").Include(
                        "~/Scripts/Shared/plugins/metisMenu/metisMenu.min.js"));


            bundles.Add(new ScriptBundle("~/bundles/profileValidation").Include(
                "~/Scripts/jquery.validate.min.js",
                "~/Scripts/jquery.validate.unobtrusive.js",
                "~/Scripts/Lib/FoolProofValidation/mvcfoolproof.unobtrusive.min.js"
                ));

            bundles.Add(new ScriptBundle("~/bundles/dataJs").Include(
                "~/Scripts/jquery.validate.min.js",
                "~/Scripts/jquery.validate.unobtrusive.js",
                "~/Scripts/Lib/FoolProofValidation/mvcfoolproof.unobtrusive.min.js"
                ));

            bundles.Add(new StyleBundle("~/Content/commonCss").Include(
                "~/Content/SharedCss/bootstrap.min.css",
                "~/Content/SharedCss/plugins/metisMenu/metisMenu.min.css",
                "~/Content/SharedCss/bootstrap-theme.min.css",
                "~/Content/SharedCss/app.css"
                ));

            bundles.Add(new StyleBundle("~/Content/profileCss").Include(
                "~/Areas/Profile/Contents/Css/ProfileCss.css",
                "~/Content/SharedCss/AngularStrapDatepicker.css",
                "~/Content/SharedCss/clockface/clockface.css",
                "~/Content/SharedCss/xeditable.css",
                "~/Content/SharedCss/jasny-bootstrap.css",
                "~/Content/SharedCss/style.css",
                "~/Content/Profile/Util.css",
                "~/Content/SharedCss/bootstrap-duallistbox.css",
                "~/Content/Provider/select2.css"
                ));

            bundles.Add(new ScriptBundle("~/Content/profileJs").Include(
                "~/Scripts/jquery.validate.min.js",
                "~/Scripts/jquery.validate.unobtrusive.js",
                "~/Scripts/Lib/FoolProofValidation/mvcfoolproof.unobtrusive.min.js",
                "~/Scripts/Custom/Validation/EndDate.js",
                "~/Scripts/Custom/Validation/PostedFileExtension.js",
                "~/Scripts/Custom/Validation/PostedFileSize.js",
                "~/Scripts/Custom/Validation/RequiredIfMonthGreaterThan.js",
                "~/Scripts/Custom/Validation/ProfileRemote.js",
                "~/Scripts/Custom/Validation/StartDate.js",
                "~/Scripts/Custom/Validation/NumberGreaterThan.js",
                //"~/Scripts/Shared/Data/CountryList.js",
                "~/Scripts/Shared/Data/countryDialCodes.js",
                "~/Scripts/Shared/Data/Languages.js",
                "~/Scripts/Lib/Jquery/select2.min.js",
                "~/Scripts/Lib/Bootstrap/jquery.bootstrap-duallistbox.js",
                "~/Scripts/Lib/Bootstrap/jasny-bootstrap.js",
                "~/Scripts/Lib/AHC_Lib/AutoCompleteLib.js",
                "~/Scripts/Lib/Angular/ng-table.js",
                "~/Scripts/Custom/Profile/xeditable.min.js",
                "~/Areas/Profile/Scripts/Util/clockface.js",
                "~/Scripts/Lib/CDN Service/angular-strap.min.js",
                "~/Scripts/Lib/CDN Service/angular-strap.tpl.min.js",
                "~/Scripts/Lib/Angular/parse-options.js"
                ));

            bundles.Add(new ScriptBundle("~/Content/angularUtilProfileJs").Include(
                "~/Areas/Profile/Scripts/Util/CreateProfile.js",
                "~/Areas/Profile/Scripts/Util/AddressAutoComplete.js"
                ));

            bundles.Add(new ScriptBundle("~/Content/jsonUtilProfileJs").Include(
                "~/Scripts/Shared/Data/CountryOfIssue.js",
                "~/Areas/Profile/Scripts/Util/Util.min.js"
                ));

        }
    }
}
