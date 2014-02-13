
using System.Web.Optimization;
using ZaDvermi.Common;

namespace ZaDvermi
{
    public static class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            // jquery
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                "~/Content/Scripts/jquery-2.1.0.min.js"));

            // common
            var commonBundle = new ScriptBundle("~/bundles/common").Include(
                "~/Content/Scripts/bootstrap.min.js",
                "~/Content/Scripts/jquery.easing.1.3.js",
                "~/Content/Scripts/jquery.cookie.js",
                "~/Content/Scripts/jquery-ui-1.9.2.custom.min.js",
                "~/Content/Scripts/jquery.ui.datepicker-cs-CZ.min.js",
                "~/Content/Scripts/curtain.js",
                "~/ckeditor/ckeditor.js",
                "~/ckeditor/adapters/jquery.js");
            commonBundle.Orderer = new BundlesOrder();
            bundles.Add(commonBundle);

            // css
            bundles.Add(new StyleBundle("~/bundles/css").Include(
                "~/Content/Styles/bootstrap.css",
                "~/Content/Styles/jquery-ui-1.10.0.custom.css",
                "~/Content/Styles/custom.css"));
        }
    }

}