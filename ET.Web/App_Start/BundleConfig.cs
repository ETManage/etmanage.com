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
            bundles.Add(new ScriptBundle("~/scripts/jquery1.1").Include(
                        "~/scripts/jquery/jquery-1.11.1.min.js", "~/scripts/jquery/jquery.parser.js", "~/scripts/jquery/jquery.form.js"));
            bundles.Add(new ScriptBundle("~/scripts/jquery2.1").Include(
                        "~/scripts/jquery/jquery-2.1.1.min.js", "~/scripts/jquery/jquery.parser.js", "~/scripts/jquery/jquery.form.js"));
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