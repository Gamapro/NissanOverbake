using System.Web;
using System.Web.Optimization;

namespace NissanOverbake
{
    public class BundleConfig
    {
        // For more information on bundling, visit https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new Bundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new Bundle("~/bundles/complements").Include(
                        "~/Scripts/fontawesome/all.min.js",
                        "~/Scripts/DataTables/jquery.dataTables.js",
                        "~/Scripts/DataTables/dataTables.responsive.js",
                        "~/Scripts/LoadingOverlay/loadingoverlay.min.js",
                        "~/Scripts/sweetalert.min.js",
                        "~/Scripts/chart.min.js",
                        "~/Scripts/Charts.min.js",
                        "~/Scripts/chart.js",
                        "~/Scripts/dates.js",
                        "~/Scripts/jquery-validate.js",
                        "~/Scripts/jquery-ui.js",
                        "~/Scripts/scripts.js"));
            
            bundles.Add(new Bundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.bundle.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                        "~/Content/DataTables/css/dataTables.bootstrap.css",
                        "~/Content/DataTables/css/jquery.dataTables.css",
                        "~/Content/DataTables/css/jquery.dataTables.min.css",
                        "~/Content/DataTables/css/responsive.dataTables.css",
                        "~/Content/bootstrap.css",
                        "~/Content/sweetalert.css",
                        "~/Content/jquery-ui.css",
                        "~/Content/Site.css"));
        }
    }
}
