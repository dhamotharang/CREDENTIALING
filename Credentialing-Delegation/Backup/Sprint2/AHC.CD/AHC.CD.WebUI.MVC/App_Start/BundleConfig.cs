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

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                        "~/Scripts/Lib/Bootstrap/bootstrap.min.js"));


            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/Lib/Jquery/modernizr-2.6.2.js"));

            bundles.Add(new ScriptBundle("~/bundles/app").Include(
                        "~/Scripts/Shared/app.js"));

            bundles.Add(new ScriptBundle("~/bundles/metisMenu").Include(
                        "~/Scripts/Shared/plugins/metisMenu/metisMenu.min.js"));

        }
    }
}
