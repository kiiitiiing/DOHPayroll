using System.Web;
using System.Web.Optimization;

namespace PayrollSystem
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                     "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/iemmis/jquery").Include(
                    "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.min.js",
                      "~/Scripts/bootstrap-datepicker.min.js",
                      "~/Clockpicker/css/bootstrap-clockpicker.min.js",
                      "~/Scripts/respond.min.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.min.css",
                      "~/Contents/bootstrap-datepicker.min.css",
                      "~/Clockpicker/css/bootstrap-clockpicker.min.css",
                      "~/Clockpicker/css/jquery-clockpicker.css",
                      "~/Content/Site.css"));
        }
    }
}
