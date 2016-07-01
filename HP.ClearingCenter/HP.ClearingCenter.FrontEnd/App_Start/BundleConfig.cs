using System.Web;
using System.Web.Optimization;

namespace HP.ClearingCenter.FrontEnd
{
    public class BundleConfig
    {
        // For more information on Bundling, visit http://go.microsoft.com/fwlink/?LinkId=254725
        public static void RegisterBundles(BundleCollection bundles)
        {
            RegisterJavascript(bundles);
            RegisterCss(bundles);
        }

        private static void RegisterCss(BundleCollection bundles)
        {
            bundles.Add(new StyleBundle("~/Content/styles.css").Include(
                "~/Content/styles/screen.css",
                "~/Content/styles/global.css",
                "~/Content/styles/hpexperience.css"));

            bundles.Add(new StyleBundle("~/Content/jqueryui/themes.css").Include(
                        "~/Content/themes/base/core.css",
                        "~/Content/themes/base/theme.css",
                        "~/Content/themes/base/accordion.css",
                        "~/Content/themes/base/autocomplete.css",
                        "~/Content/themes/base/datepicker.css",
                        "~/Content/themes/base/dialog.css",
                        "~/Content/themes/base/draggable.css",
                        "~/Content/themes/base/menu.css",
                        "~/Content/themes/base/selectable.css",
                        "~/Content/themes/base/selectmenu.css",
                        "~/Content/themes/base/slider.css",
                        "~/Content/themes/base/sortable.css"));

            bundles.Add(new StyleBundle("~/Content/bootstrap/styles.css").Include(
                "~/Content/bootstrap.min.css",
                "~/Content/bootstrap-theme.min.css"));
        }

        private static void RegisterJavascript(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery.js").Include(
                        "~/Scripts/json2*",
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryui.js").Include(
                        "~/Scripts/jquery-ui-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval.js").Include(
                        "~/Scripts/jquery.validate*",
                        "~/Scripts/jquery.unobtrusive-ajax*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap.js").Include(
                "~/Scripts/bootstrap.min.js" ));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr.js").Include(
                        "~/Scripts/modernizr-*"));
        }
    }
}