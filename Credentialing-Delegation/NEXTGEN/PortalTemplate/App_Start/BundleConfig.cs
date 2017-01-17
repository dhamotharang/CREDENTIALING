using System.Web;
using System.Web.Optimization;

namespace PortalTemplate
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/respond.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/site.css"));

            bundles.Add(new StyleBundle("~/Content/defaultrequired").Include(
                    "~/Content/LibCss/bootstrap.min.css",
                    "~/fonts/css/font-awesome.min.css",
                    "~/Resources/Fonts/Roboto/roboto.css",
                    "~/Content/LibCss/jquery-ui.min.css",
                    "~/Content/AppCss/animate.min.css",
                    "~/Content/AppCss/custom.min.css",
                    "~/Content/LibCss/bootstrap-switch.min.css",
                    "~/Content/c3.min.css"));

            bundles.Add(new StyleBundle("~/Content/appstyles").Include(
                 "~/Content/CustomCss/OuterTabs.css",
                 "~/Content/JqueryCssLib/chrome-tabs.css",
                 "~/Content/CustomTheme/colorpatch.css",
                 "~/Content/LibCss/icheck/flat/green.css",
                 "~/Content/CustomTheme/DashboardCSS/IntakeDashboardCSS.css",
                 "~/Content/CustomTheme/DashboardCSS/SuperUserDashboardCSS.css",
                 "~/Content/CustomTheme/DashboardCSS/IntakeLeadDashboardCSS.css",
                 "~/Content/CustomTheme/DashboardCSS/UmAdminDashboardCSS.css"));

            bundles.Add(new StyleBundle("~/Content/logincss").Include(
                      "~/Content/AppCss/LoginPage/Garamond.css",
                        "~/Content/AppCss/LoginPage/OpenSans.css",
                        "~/Content/LibCss/jquery-ui.min.css",
                        "~/Content/AppCss/LoginPage/bootstrap.min.css",
                        "~/Content/AppCss/LoginPage/style.css",
                        "~/fonts/css/font-awesome.min.css",
                        "~/Content/LibCss/rippler.min.css"
                      ));

            bundles.Add(new ScriptBundle("~/bundles/loginscripts").Include(
                        "~/Scripts/jquery-3.0.0.min.js",
                        "~/Scripts/LibScripts/jquery-ui.min.js",
                        "~/Scripts/jquery.validate.min.js",
                        "~/Scripts/jquery.validate.unobtrusive.min.js",
                        "~/Scripts/bootstrap.min.js",
                        "~/Scripts/AppScripts/LoginPage/jquery.backstretch.js",
                        "~/Scripts/LibScripts/jquery.rippler.min.js"
            ));

            bundles.Add(new ScriptBundle("~/bundles/requiredjs").Include(
                     "~/Scripts/LibScripts/jquery.min.js",
                     "~/Scripts/LibScripts/jquery-ui.min.js",
                     "~/Scripts/LibScripts/angular.min.js",
                     "~/Scripts/LibScripts/JqueryLibScripts/chrome-tabs.js",
                     "~/Scripts/LibScripts/JqueryLibScripts/bootstrap-switch.min.js",
                     "~/Scripts/LibScripts/d3.min.js",
                     "~/Scripts/LibScripts/c3.min.js"
         ));

            bundles.Add(new ScriptBundle("~/bundles/intakedashboardscripts").Include(
                       "~/Resources/Data/IntakeDashboardData.js",
                       "~/Scripts/CustomScripts/DashboardScripts/IntakeDashboard.js"
           ));

            bundles.Add(new ScriptBundle("~/bundles/intakeleaddashboardscripts").Include(
                      "~/Scripts/CustomScripts/DashboardScripts/IntakeLeadDashboard.js"
          ));

            bundles.Add(new ScriptBundle("~/bundles/superuserscripts").Include(
                      "~/Resources/Data/SuperUserDashboardData.js",
                      "~/Resources/Data/ClaimsProcessedSummaryChartData.js",
                      "~/Resources/Data/ClaimAdmissionsSummaryChartData.js",
                      "~/Scripts/CustomScripts/DashboardScripts/UMAdminDashboard.js",
                      "~/Scripts/CustomScripts/DashboardScripts/ClaimsProcessedSummaryChartAndTable.js",
                      "~/Scripts/CustomScripts/DashboardScripts/claimAdmissionsSummaryChartAndTable.js"
          ));
        }
    }
}
