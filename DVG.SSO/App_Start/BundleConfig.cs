using System.Web;
using System.Web.Optimization;

namespace DVG.SSO
{
    public class BundleConfig
    {
        // For more information on bundling, visit https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new StyleBundle("~/Resources/css/main").Include(
                        "~/Resources/jquery.treeview.css",
                        "~/Resources/css/plugins.css",
                        "~/Resources/css/font-awesome.css",
                        "~/Resources/css/fam-icons.css",
                        "~/Resources/css/icons.css",
                        "~/Resources/css/jquery.custom.css",
                        "~/Resources/css/elfinder.css",
                        "~/Resources/css/main.css"
            ));
            bundles.Add(new ScriptBundle("~/Resources/js/plugins/forms/jquery").Include(
                "~/Resources/js/plugins/charts/jquery.flot.js",
                 "~/Resources/js/plugins/charts/jquery.flot.resize.js",
                "~/Resources/js/plugins/ui/jquery.mousewheel.js",
                "~/Resources/js/plugins/ui/prettify.js",
                "~/Resources/js/plugins/ui/jquery.colorpicker.js",
                "~/Resources/js/plugins/ui/jquery.jgrowl.js",
                "~/Resources/js/plugins/ui/jquery.fancybox.js",
                "~/Resources/js/plugins/ui/jquery.elfinder.js",
                //"~/Resources/js/plugins/uploader/jquery.plupload.queue.js",

                //"~/Resources/js/plugins/forms/jquery.uniform.min.js",
                "~/Resources/js/plugins/forms/jquery.autosize.js",
                //"~/Resources/js/plugins/forms/jquery.inputlimiter.min.js",
                //"~/Resources/js/plugins/forms/jquery.tagsinput.min.js",
                "~/Resources/js/plugins/forms/jquery.inputmask.js",
                //"~/Resources/js/plugins/forms/jquery.select2.min.js",
                "~/Resources/js/plugins/forms/jquery.listbox.js",
                "~/Resources/js/plugins/forms/jquery.validation.js",
                "~/Resources/js/plugins/forms/jquery.validationEngine-en.js",
                "~/Resources/js/plugins/forms/jquery.form.wizard.js",
                "~/Resources/js/plugins/forms/jquery.form.js",
                "~/Scripts/jquery.validate.js",
                "~/Scripts/jquery.validate.unobtrusive.js",
                "~/Resources/js/custom.js",
                "~/Resources/js/jquery.treeview.js"
               ));
            BundleTable.EnableOptimizations = true;
        }
    }
}
