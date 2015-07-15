using System.Web;
using System.Web.Optimization;

namespace ET.Web
{
    public class BundleConfig
    {
        // 有关 Bundling 的详细信息，请访问 http://go.microsoft.com/fwlink/?LinkId=254725
        public static void RegisterBundles(BundleCollection bundles)
        {
            //可以利用*代替字符
            bundles.Add(new ScriptBundle("~/scripts/jquery").Include(
                        "~/scripts/jquery-min.js", "~/scripts/jquery.parser.js", "~/scripts/jquery.form.js"));
            bundles.Add(new ScriptBundle("~/scripts/jquery1-8").Include(
                        "~/scripts/jquery-min1-8.js", "~/scripts/jquery.parser.js", "~/scripts/jquery.form.js"));
            bundles.Add(new StyleBundle("~/content/basestyle").Include(
                 "~/content/base.css"));
            bundles.Add(new StyleBundle("~/content/manage/managestyle").Include(
               "~/content/manage/common.css"));


            // 使用 Modernizr 的开发版本进行开发和了解信息。然后，当你做好
            // 生产准备时，请使用 http://modernizr.com 上的生成工具来仅选择所需的测试。
            bundles.Add(new ScriptBundle("~/scripts/modernizr").Include(
                        "~/Scripts/modernizr-*"));
        }
    }
}