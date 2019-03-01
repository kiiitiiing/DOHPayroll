using System.Web;
using System.Web.Optimization;
using PayrollSystem.Models;

namespace PayrollSystem
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

            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                     "~/Scripts/modernizr-*"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                       "~/Content/bootstrap.min.css",
                       "~/Content/site.css",
                       "~/admin-lte/css/AdminLTE.css",
                       "~/admin-lte/css/bootstrap-datepicker.min.css",
                       "~/admin-lte/css/font-awesome.min.css",
                       "~/admin-lte/css/skins/skin-blue.css",
                       "~/admin-lte/plugins/bootstrap-wysihtml5/bootstrap3-wysihtml5.min.css"
                       ));

            bundles.Add(new ScriptBundle("~/bundles/admin-lte/js").Include(
            "~/admin-lte/js/app.js",
            "~/admin-lte/js/jquery.min.js",
            "~/admin-lte/js/jquery-ui.min.js",
            "~/admin-lte/js/bootstrap-datepicker.min.js",
            "~/admin-lte/js/jquery.slimscroll.min.js",
            "~/admin-lte/js/adminlte.min.js",
            "~/admin-lte/plugins/fastclick/fastclick.js",
            "~/admin-lte/plugins/bootstrap-wysihtml5/bootstrap3-wysihtml5.all.min.js"
            ));

        }
    }
}
