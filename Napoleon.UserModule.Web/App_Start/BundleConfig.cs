
using System.Web.Optimization;

namespace Napoleon.UserModule.Web
{
    public class BundleConfig
    {

        // 有关绑定的详细信息，请访问 http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {

            bundles.Add(new ScriptBundle("~/Content/Js/seajs").Include(
                        "~/Content/Js/sea.js"
                        ));

            bundles.Add(new StyleBundle("~/Content/easyui/themes/css").Include(
                      "~/Content/easyui/themes/icon.css"
                      ));

            bundles.Add(new StyleBundle("~/Content/easyui/themes/bootstrap/css").Include(
                      "~/Content/easyui/themes/bootstrap/easyui.css"
                      ));

            bundles.Add(new StyleBundle("~/Content/Css/PublicCss/Css").Include(
                      "~/Content/Css/PublicCss/Button.css",
                      "~/Content/Css/PublicCss/DataGrid.css"
                      ));

            bundles.Add(new StyleBundle("~/Content/Css/PublicCss/Icons").Include(
                      "~/Content/Css/PublicCss/Icons.css"
                      ));

            // 使用要用于开发和学习的 Modernizr 的开发版本。然后，当你做好生产准备时，请使用 http://modernizr.com 上的生成工具来仅选择所需的测试。
            /*bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"
                        ));*/

            /*bundles.Add(new StyleBundle("~/Content").Include(
                             "~/Content/default.css"
                         ));*/
            //菜单图标



        }

    }
}