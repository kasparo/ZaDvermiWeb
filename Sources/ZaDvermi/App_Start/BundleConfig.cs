
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
                "~/Content/Scripts/curtain.js");
            commonBundle.Orderer = new BundlesOrder();
            bundles.Add(commonBundle);

            // ckeditor
            /*var ckEditorBundle = new ScriptBundle("~/bundles/ckeditor").Include(
                "~/ckeditor/ckeditor.js",
                "~/ckeditor/adapters/jquery.js");
            ckEditorBundle.Orderer = new BundlesOrder();
            bundles.Add(ckEditorBundle);*/

            // colorpicker
            bundles.Add(new ScriptBundle("~/bundles/colorpicker").Include(
                "~/Content/Scripts/jquery.minicolors.min.js"));

            // fileupload
            var fileUploadBundle = new ScriptBundle("~/bundles/fileupload").Include(
                "~/Content/Scripts/jquery.ui.widget.js",
                "~/Content/Scripts/tmpl.min.js",
                "~/Content/Scripts/load-image.min.js",
                "~/Content/Scripts/canvas-to-blob.min.js",
                "~/Content/Scripts/jquery.iframe-transport.js",
                "~/Content/Scripts/jquery.fileupload.js",
                "~/Content/Scripts/jquery.fileupload-process.js",
                "~/Content/Scripts/jquery.fileupload-image.js",
                "~/Content/Scripts/jquery.fileupload-ui.js");
            fileUploadBundle.Orderer = new BundlesOrder();
            bundles.Add(fileUploadBundle);

            // image gallery
            bundles.Add(new ScriptBundle("~/bundles/imagegallery").Include(
                "~/Content/Scripts/blueimp-gallery.js",
                "~/Content/Scripts/jquery.blueimp-gallery.js"));

            // css
            bundles.Add(new StyleBundle("~/bundles/css").Include(
                "~/Content/Styles/bootstrap.css",
                "~/Content/Styles/jquery-ui-1.10.0.custom.css",
                "~/Content/Styles/jquery.minicolors.css",
                "~/Content/Styles/jquery.fileupload-ui.css",
                "~/Content/Styles/blueimp-gallery.min.css",
                "~/Content/Styles/custom.css"));
        }
    }

}