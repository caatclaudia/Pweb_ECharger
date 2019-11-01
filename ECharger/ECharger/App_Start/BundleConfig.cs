using System.Web;
using System.Web.Optimization;

namespace ECharger
{
    public class BundleConfig
    {
        // For more information on bundling, visit https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                "~/Scripts/jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at https://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                "~/Scripts/bootstrap.js"));
            
            bundles.Add(new ScriptBundle("~/bundles/maps").Include(
                "~/Scripts/maps.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                "~/Content/bootstrap.css",
                "~/Content/Site.css",
                "~/Content/css/font-awesome.css",
                "~/admin-lte/plugins/fontawesome-free/css/all.css",
                "~/admin-lte/css/AdminLTE.css"
            ));
            
            bundles.Add(new ScriptBundle("~/admin-lte/js").Include(
                "~/admin-lte/js/adminlte.js",
                "~/admin-lte/plugins/fastclick/fastclick.js"
            ));
            
            bundles.Add(new Bundle("~/crew/css").Include(
                "~/crew/css/bootstrap.css",
                "~/crew/css/animate.css",
                "~/crew/css/icomoon.css",
                "~/crew/css/simple-line-icons.css",
                "~/crew/css/owl.carousel.min.css",
                "~/crew/css/owl.theme.default.min.css",
                "~/crew/css/lightblue.css",
                "~/Content/Site.css"
            ));
            
            bundles.Add(new ScriptBundle("~/crew/js").Include(
                "~/crew/js/modernizr-2.6.2.min.js",
                "~/crew/js/jquery.min.js",
                "~/crew/js/jquery.easing.1.3.js",
                "~/crew/js/bootsrap.min.js",
                "~/crew/js/jquery.waypoints.min.js",
                "~/crew/js/owl.carousel.min.js",
                "~/crew/js/main.js"
            ));
        }
    }
}