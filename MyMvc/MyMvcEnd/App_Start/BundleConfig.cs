using System.Web;
using System.Web.Optimization;

namespace MyMvcEnd
{
    public class BundleConfig
    {
        // 有关 Bundling 的详细信息，请访问 http://go.microsoft.com/fwlink/?LinkId=254725
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/login").Include(
                        "~/Scripts/jquery-1.7.1.js",
                        "~/Scripts/jquery.easyui.js",
                        "~/Scripts/jquery.validate.js"));

            bundles.Add(new ScriptBundle("~/bundles/layout").Include(
                        "~/Scripts/jquery-1.7.1.js",
                        "~/Scripts/jquery.unobtrusive-ajax.js",
                        "~/Scripts/jquery.easyui.js",
                        "~/Scripts/jquery.validate.js",
                        "~/Scripts/dealmenu.js"));

            bundles.Add(new StyleBundle("~/Content/login/css").Include(
                        "~/Content/themes/default/easyui.css",
                        "~/Content/themes/icon.css"));
            bundles.Add(new StyleBundle("~/Content/layout/css").Include(
                      "~/Content/themes/default/easyui.css",
                      "~/Content/themes/icon.css",
                      "~/Content/themes/demo.css",
                      "~/Content/common.css"));
        }
    }
}