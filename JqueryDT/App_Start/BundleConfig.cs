using System.Web.Optimization;

namespace JqueryDT
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jquery-ui").Include(        //Jquery UI Is required
                "~/Scripts/jquery-ui-1.12.1.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                "~/Scripts/jquery.validate*"));

            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                "~/Scripts/bootstrap.js",
                "~/Scripts/respond.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                "~/Content/bootstrap.css",
                "~/Content/site.css"));

            bundles.Add(new ScriptBundle("~/bundles/jquery-datatables-js").Include(
                "~/Content/jquery.dataTables.js", // Standard DataTable
                "~/Content/jquery-datatables-column-filter/media/js/jquery.dataTables.columnFilter.js", // These ones if you want to use the column filters
                "~/Content/jquery-datatables-column-filter/jquery-ui-timepicker-addon.js", // And these if you want date time pickers in the filters
                "~/Content/dataTables.colVis.min.js")); // And these if you want to use column visibility

            bundles.Add(new StyleBundle("~/bundles/jquery-datatables-css").Include(
                "~/Content/jquery.dataTables.css", // Standard DataTable
                "~/Content/jquery-datatables-column-filter/media/js/jquery.dataTables.columnFilter.css", // These ones if you want to use the column filters
                "~/Content/jquery-datatables-column-filter/jquery-ui-timepicker-addon.css", // And these if you want date time pickers in the filters
                "~/Content/dataTables.colVis.css")); // And these if you want to use column visibility
        }
    }
}