using System.Web;
using System.Web.Optimization;

namespace SalesAndInventory
{
    public class BundleConfig
    {
        // For more information on bundling, visit https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at https://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-2.6.2.js"));

            bundles.Add(new ScriptBundle("~/bundles/ie").Include(
                      "~/Scripts/js/html5shiv.min.js",
                      "~/Scripts/js/respond.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/js/bootstrap.js",
                      "~/Scripts/js/respond.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/css/entypo.css",
                      "~/Content/css/font-awesome.min.css",
                      "~/Content/css/plugins/css3-animate-it-plugin/animations.css",
                      "~/Content/css/bootstrap.css",
                      "~/Content/css/mouldifi-core.css",
                      "~/Content/css/mouldifi-forms.css",
                      "~/Content/custom.css"));


            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
               "~/Scripts/js/jquery.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/css3AnimateIt").Include(
               "~/Scripts/js/plugins/css3-animate-it-plugin/css3-animate-it.js",
               "~/Scripts/js/bootstrap.min.js",
               "~/Scripts/js/plugins/metismenu/jquery.metisMenu.js",
               "~/Scripts/js/plugins/blockui-master/jquery-ui.js",
               "~/Scripts/js/plugins/blockui-master/jquery.blockUI.js"));

            bundles.Add(new ScriptBundle("~/bundles/floatChart").Include(
               "~/Scripts/js/plugins/flot/jquery.flot.min.js",
               "~/Scripts/js/plugins/flot/jquery.flot.tooltip.min.js",
               "~/Scripts/js/plugins/flot/jquery.flot.tooltip.min.js",
               "~/Scripts/js/plugins/flot/jquery.flot.selection.min.js",
               "~/Scripts/js/plugins/flot/jquery.flot.pie.min.js",
               "~/Scripts/js/plugins/flot/jquery.flot.time.min.js",
               "~/Scripts/js/functions.js"));

            bundles.Add(new ScriptBundle("~/bundles/dataTables").Include(
               "~/Scripts/js/plugins/datatables/jquery.dataTables.min.js",
               "~/Scripts/js/plugins/datatables/dataTables.bootstrap.min.js",
               "~/Scripts/js/plugins/datatables/extensions/Buttons/js/dataTables.buttons.min.js",
               "~/Scripts/js/plugins/datatables/jszip.min.js",
               "~/Scripts/js/plugins/datatables/pdfmake.min.js",
               "~/Scripts/js/plugins/datatables/vfs_fonts.js",
               "~/Scripts/js/plugins/datatables/extensions/Buttons/js/buttons.html5.js",
               "~/Scripts/js/plugins/datatables/extensions/Buttons/js/buttons.colVis.js"));
        }
    }
}
