using System.Web;
using System.Web.Optimization;

namespace Proiect_TI
{
    public class BundleConfig
    {
        // For more information on bundling, visit https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new Bundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new Bundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at https://modernizr.com to pick only the tests you need.
            bundles.Add(new Bundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new Bundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.min.js"
                      ));

            bundles.Add(new Bundle("~/bundles/table").Include(
                      "~/Scripts/employee-table.js"
                      ));

            bundles.Add(new Bundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/custom.css",
                      "~/Content/bootstrap.min.css",
                      "~/Content/site.css"));

            bundles.Add(new StyleBundle("~/bundles/crystalreports").Include(
                "~/CrystalReports/aspnet_client/system_web/4_0_30319/crystalreportviewers13/css/cedefault.css",
                "~/CrystalReports/aspnet_client/system_web/4_0_30319/crystalreportviewers13/css/default.css",
                "~/CrystalReports/aspnet_client/system_web/4_0_30319/crystalreportviewers13/css/exception.css",
                "~/CrystalReports/aspnet_client/system_web/4_0_30319/crystalreportviewers13/css/prompting.css"
            ));
        }
    }
}
