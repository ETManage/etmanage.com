using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace ET.Web
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            //routes.MapRoute(
            //    name: "Default",
            //    url: "{controller}/{action}/{id}",
            //    defaults: new { controller = "Blog", action = "Index", id = UrlParameter.Optional }
            //    //让自定义Html.ActionLink为小写
            //    //routes.MapLowerCaseUrlRoute(
            //    //    name: "Default",
            //    //    url: "{controller}/{action}/{id}",
            //    //    defaults: new { controller = "Shop", action = "Index", id = UrlParameter.Optional }

            //);

            //设置默认Area
            routes.MapRoute(
                "Default", // 路由名称
                "{controller}/{action}/{id}", // 带有参数的 URL
                        new { controller = "Blog", action = "Index", id = UrlParameter.Optional }, // 参数默认值
                        new[] { "Ed.Web.Areas.Manage" } //默认控制器的命名空间
             ).DataTokens.Add("area", "Manage");//默认area 的控制器名称
        }
    }
}