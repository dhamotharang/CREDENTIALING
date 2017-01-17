using System.Web;
using System.Web.Optimization;

namespace PortalTemplate
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {

            System.Web.Optimization.BundleTable.EnableOptimizations = true;

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
                    "~/Content/bootstrap.min.css",
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
                        "~/Scripts/jquery-3.1.1.min.js",
                        "~/Scripts/LibScripts/jquery-ui.min.js",
                        "~/Scripts/AppScripts/ClientCheck.js",
                        "~/Scripts/jquery.validate.min.js",
                        "~/Scripts/jquery.validate.unobtrusive.min.js",
                        "~/Scripts/bootstrap.min.js",
                        "~/Scripts/AppScripts/LoginPage/jquery.backstretch.js",
                        "~/Scripts/LibScripts/jquery.rippler.min.js"
            ));

            bundles.Add(new ScriptBundle("~/bundles/requiredjs").Include(
                     "~/Scripts/LibScripts/jquery.min.js",
                     "~/Scripts/LibScripts/jquery-ui.min.js",
                     "~/Scripts/AppScripts/ClientCheck.js",
                     "~/Scripts/LibScripts/angular.min.js",
                     "~/Scripts/LibScripts/JqueryLibScripts/chrome-tabs.js",
                     "~/Scripts/LibScripts/JqueryLibScripts/bootstrap-switch.min.js",
                     "~/Scripts/LibScripts/d3.min.js",
                     "~/Scripts/LibScripts/c3.min.js"
         ));
            //Intake Dashboard Bundles
            bundles.Add(new ScriptBundle("~/bundles/intakedashboardscripts").Include(
                       "~/Resources/Data/IntakeDashboardData.js",
                       "~/Scripts/CustomScripts/DashboardScripts/IntakeDashboard.js"
                    ));
            bundles.Add(new StyleBundle("~/Content/intakedashboardcss").Include(
                    "~/Content/CustomTheme/DashboardCSS/IntakeDashboardCSS.css"
                    ));
            //Intake Dashboard Bundles END

            //PAC Dashboard Bundles
            bundles.Add(new ScriptBundle("~/bundles/pacdashboardscripts").Include(
                      "~/Scripts/AppScripts/PACLeadDashboard/pacDashboad.js"
                   ));
            bundles.Add(new StyleBundle("~/Content/pacdashboardcss").Include(
                    "~/Content/AppCss/PACLeadDashboard/pacDashboard.css"
                    ));
            //PAC Dashboard Bundles END

            //Claims Dashboard Bundles
            bundles.Add(new ScriptBundle("~/bundles/claimsdashboardscripts").Include(
                    "~/Scripts/AppScripts/ClaimsDashboard/multi-select-searchcumdropdown.js",
                    "~/Scripts/AppScripts/ClaimsDashboard/filter.js",
                    "~/Scripts/AppScripts/ClaimsDashboard/ClaimsDashboardRewamp.js",
                    "~/Scripts/AppScripts/ClaimsDashboard/data.js"
                 ));
            bundles.Add(new StyleBundle("~/Content/claimsdashboardcss").Include(
                    "~/Content/AppCss/ClaimsDashboard/claimsDashboard.css",
                    "~/Content/AppCss/ClaimsDashboard/multiple-select.css",
                    "~/Content/AppCss/ClaimsDashboard/filter.css",
                    "~/Content/AppCss/ClaimsDashboard/multiselect-search.css",
                    "~/Content/AppCss/ClaimsDashboard/SettingsForClaimsDashBoard.css"
                    ));
            //Claims Dashboard Bundles END


            //Auth History Bundles
            bundles.Add(new ScriptBundle("~/bundles/authhistoryscripts").Include(
                   "~/Resources/Data/HistoryData.js",
                   "~/Areas/UM/Scripts/Authorization/UMAuthHistory/NonMinified/UMAuthHistory.js"
                ));
            bundles.Add(new StyleBundle("~/Content/authhistorycss").Include(
                    "~/Areas/UM/Styles/History/History.css"
                    ));
            //Auth History Bundles END

            //Create Auth Bundles
            bundles.Add(new ScriptBundle("~/bundles/createauthscripts").Include(
                   "~/Scripts/AppScripts/Authorization/CreateNewAuth.js"
                ));
            bundles.Add(new StyleBundle("~/Content/createauthcss").Include(
                    ));
            //Create Auth Bundles END

            //Member Profile Bundles
            bundles.Add(new ScriptBundle("~/bundles/memberprofilescripts").Include(
                   "~/Scripts/AppScripts/MemberProfile/MemberProfile.js"
                ));
            bundles.Add(new StyleBundle("~/Content/memberprofilecss").Include(
                    ));
            //Member Profile Bundles END

            //Queue Bundles
            bundles.Add(new ScriptBundle("~/bundles/queuescripts").Include(
                   "~/Resources/Data/QueueData.js",
                   "~/Scripts/AppScripts/Queue/Queue.js"
                ));
            bundles.Add(new StyleBundle("~/Content/queuecss").Include(
                    "~/Areas/UM/Styles/Queue/Queue.css"
                    ));
            //Queue Bundles END


            //All Styles Bundle
            bundles.Add(new StyleBundle("~/Content/allcss").Include(
                 "~/Content/CustomTheme/DashboardCSS/IntakeDashboardCSS.css",
                 "~/Content/CustomTheme/DashboardCSS/SuperUserDashboardCSS.css",
                 "~/Content/CustomTheme/DashboardCSS/IntakeLeadDashboardCSS.css",
                 "~/Content/CustomTheme/DashboardCSS/UmAdminDashboardCSS.css",
                 "~/Content/CustomTheme/DashboardCSS/IntakeDashboardCSS.css",
                 "~/Content/AppCss/ClaimsDashboard/claimsDashboard.css",
                 "~/Content/AppCss/ClaimsDashboard/multiple-select.css",
                 "~/Content/AppCss/ClaimsDashboard/filter.css",
                 "~/Content/AppCss/ClaimsDashboard/multiselect-search.css",
                 "~/Content/AppCss/ClaimsDashboard/SettingsForClaimsDashBoard.css",
                 "~/Areas/UM/Styles/History/History.css",
                 "~/Areas/UM/Styles/Queue/Queue.css",
                 "~/Content/CustomTheme/DashboardCSS/SuperUserDashboardCSS.css",
                 "~/Content/CustomTheme/DashboardCSS/IntakeLeadDashboardCSS.css",
                 "~/Content/CustomTheme/DashboardCSS/UmAdminDashboardCSS.css",
                 "~/Content/AppCss/claims/claims.css",
                 "~/Content/AppCss/EDITracking/EDITracking.css"
                   ));

               bundles.Add(new StyleBundle("~/Content/requiredstyles").Include(
                "~/Content/CustomTheme/colorpatch.css",
                "~/Content/LibCss/icheck/all.css",
                 "~/Content/AppCss/ClaimsDashboard/multiple-select.css",
                 "~/Content/c3.min.css",
                 "~/Content/LibCss/jquery-ui.min.css",
                 "~/Content/LibCss/daterangepicker.css",
                 "~/Content/LibCss/bootstrap-switch.min.css",
                 "~/Content/bootstrap-datetimepicker.css",
                 "~/Content/PratianComponentsCss/checkbox-radio.css",
                 "~/Content/LibCss/pnotify.custom.min.css",
                 "~/Content/LibCss/jquery-customselect-1.9.1.css",
                 "~/Content/LibCss/Select2/select2.min.css",
                 "~/Areas/AM/Styles/jquery.multiselect.css",
                 "~/Content/PratianComponentsCss/pt-table_v1.css",
                 "~/Areas/AM/Styles/jquery.multiselect.css",
                 "~/Content/dataTables.bootstrap.min.css"
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

            bundles.Add(new ScriptBundle("~/bundles/requiredscripts").Include(
                  "~/Scripts/jquery-3.1.1.min.js",
                  "~/Scripts/LibScripts/jquery-ui.min.js",
                  "~/Scripts/LibScripts/angular.min.js",
                  "~/Scripts/LibScripts/JqueryLibScripts/chrome-tabs.js",
                  "~/Scripts/LibScripts/JqueryLibScripts/bootstrap-switch.min.js",
                  "~/Scripts/LibScripts/d3.min.js",
                  "~/Scripts/LibScripts/c3.min.js",
                  "~/Scripts/LibScripts/moment.min.js",
                  "~/Scripts/LibScripts/daterangepicker.js",
                  "~/Scripts/bootstrap-datetimepicker.js",
                  "~/Scripts/LibScripts/underscore-min.js",
                  "~/Scripts/LibScripts/html2canvas.min.js",
                  "~/Scripts/LibScripts/jspdf.min.js",
                  "~/Scripts/LibScripts/icheck/icheck.min.js",
                  "~/Scripts/PratianComponentsScripts/RadioCheckBox-Plugin.js",
                  "~/Scripts/LibScripts/pnotify.custom.min.js",
                  "~/Scripts/LibScripts/JqueryLibScripts/jquery-customselect-1.9.1.min.js",
                  "~/Scripts/LibScripts/Select2/select2.full.min.js",
                  "~/Areas/CredAxis/Scripts/Common/Selectize/selectize.min.js",
                  "~/Scripts/LibScripts/jquery.maskedinput.js",
                  "~/Areas/AM/Scripts/Accounts/NonMinified/jquery.multiselect.js",
                  "~/Scripts/jquery.unobtrusive-ajax.min.js",
                  "~/Scripts/jquery.validate.min.js",
                  "~/Scripts/jquery.validate.unobtrusive.min.js",
                  "~/Scripts/PratianComponentsScripts/pt_grid_v1.js",
                  "~/Scripts/DataTables/jquery.dataTables.min.js",
                  "~/Scripts/DataTables/dataTables.buttons.min.js",
                  "~/Scripts/DataTables/buttons.flash.min.js",
                  "~/Scripts/DataTables/jszip.min.js",
                  "~/Scripts/DataTables/pdfmake.min.js",
                  "~/Scripts/DataTables/vfs_fonts.js",
                  "~/Scripts/DataTables/buttons.html5.min.js",
                  "~/Scripts/DataTables/buttons.print.min.js",
                  "~/Scripts/DataTables/jquery.table2excel.js"
         ));
        }
    }
}
