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

            bundles.Add(new StyleBundle("~/Content/CommonCSS").Include(
                //"~/Content/SharedCss/AngularStrapDatepicker.css", why we need that.
                "~/Content/SharedCss/clockface/clockface.css",
                "~/Content/SharedCss/bootstrap-duallistbox.css",
                //"~/Content/Provider/select2.css", where we are using.
                //"~/Content/SharedCss/SmartTable.css",
                "~/Content/SharedCss/app.css",
                "~/Content/SharedCss/style.css"
                ));

            bundles.Add(new StyleBundle("~/Content/MasterProfileCSS").Include(
                "~/Content/Profile/Util.css",
                "~/Areas/Profile/Contents/Css/ProfileCss.css"
                ));

            bundles.Add(new ScriptBundle("~/bundles/CommonJS").Include(
                //"~/Scripts/Lib/Jquery/select2.min.js",
                "~/Scripts/Lib/Bootstrap/jquery.bootstrap-duallistbox.js",
                "~/Scripts/Lib/pdfGeneration/tableExport.js",
                "~/Scripts/App/app.js"
                ));
            bundles.Add(new ScriptBundle("~/bundles/ValidationJS").Include(
                "~/Scripts/Lib/FoolProofValidation/mvcfoolproof.unobtrusive.min.js",
                "~/Scripts/Custom/Validation/EndDate.js",
                "~/Scripts/Custom/Validation/PostedFileExtension.js",
                "~/Scripts/Custom/Validation/PostedFileSize.js",
                "~/Scripts/Custom/Validation/RequiredIfMonthGreaterThan.js",
                "~/Scripts/Custom/Validation/ProfileRemote.js",
                "~/Scripts/Custom/Validation/StartDate.js",
                "~/Scripts/Custom/Validation/NumberGreaterThan.js"
                ));

            bundles.Add(new ScriptBundle("~/bundles/AngularJS").Include(
                "~/Scripts/Lib/AHC_Lib/AutoCompleteLib.js",
                "~/Areas/Profile/Scripts/Util/clockface.js"
                ));
            bundles.Add(new ScriptBundle("~/bundles/MasterProfileJS").Include(
                "~/Scripts/Shared/Data/Languages.js",
                "~/Scripts/Shared/Data/countryDialCodes.js",
                "~/Scripts/Shared/Data/CountryOfIssue.js",
                "~/Areas/Profile/Scripts/Util/Util.js"
                ));

        }
    }
}
