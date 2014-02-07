
using System.Web.Optimization;

namespace ZaDvermi
{
    public static class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                "~/Content/Scripts/jquery-2.0.3.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/common").Include(
                "~/Content/Scripts/bootstrap.min.js",
                "~/Content/Scripts/jquery.easing.1.3.js",
                "~/Content/Scripts/jquery.cookie.js",
                "~/Content/Scripts/curtain.js"));

            bundles.Add(new StyleBundle("~/bundles/css").Include(
                "~/Content/Styles/bootstrap.css",
                "~/Content/Styles/custom.css"));
        }
    }

}