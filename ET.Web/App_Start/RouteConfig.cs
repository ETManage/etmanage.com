using ET.Web.Code;
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

            routes.IgnoreRoute("favicon.ico");
            routes.MapMvcAttributeRoutes();
            routes.MapRoute("login", "login",
                            new { controller = "Account", action = "Login" });

            //routes.Add("area_default", new DomainRoute(
            //    "manage.etmanage.com",
            //    "manage/{controller}/{action}/{id}",
            //    new { area = "Manage", controller = "mAccount", action = "Login", id = "" }
            //));

            //routes.Add("DomainRouteForMutiWebSite", new DomainRoute(
            //    "manage.etmanage.com",                             // {area}作为二级域名
            //    "manage{controller}/{action}/{id}",                  // URL with parameters
            //    new
            //    {
            //        area = "Manage",
            //        controller = "mAccount",
            //        action = "Login",
            //        id = "",
            //        Namespaces = new[] { "ET.Web.Areas.Manage.Controllers" }
            //    }  // Parameter defaults
            //));
//            routes.Add("DomainRoute", new DomainRoute(
//"{controller}.etmanage.com",     // Domain with parameters< br />    
//"{action}/{id}",    // URL with parameters
//new { controller = "Blog", action = "Index", id = "" }  // Parameter defaults
//));
            //routes.MapRoute(
            //                name: "API_Default",
            //                url: "api/{controller}/{action}/{id}",
            //                defaults: new { id = UrlParameter.Optional }
            //            );



            //   routes.Add("demo", new DomainRoute(
            //    "{area}.demo.com",
            //    "{controller}/{action}/{id}",
            //    new { area = "", controller = "Home", action = "Index", id = "" }
            //));

            //   routes.Add("web", new DomainRoute(
            //       "{area}.web.com",
            //       "{controller}/{action}/{id}",
            //       new { area = "", controller = "Web", action = "Index", id = "" }
            //   ));


            routes.MapRoute(
             name: "Default",
             url: "{controller}/{action}/{id}",
             defaults: new { controller = "Blog", action = "Index", id = UrlParameter.Optional }
         );
            //routes.Add("DomainRouteForMutiWebSite", new DomainRoute(
            //    "{area}.domain.com",                             // {area}作为二级域名
            //    "{controller}/{action}/{id}",                  // URL with parameters
            //    new
            //    {
            //        area = "TZ",
            //        controller = "Home",
            //        action = "Index",
            //        id = "",
            //        Namespaces = new[] { "Portal.Areas.TZ.Controllers" }
            //    }  // Parameter defaults
            //));


            //  routes.Add(
            //   "DomainRoute", new DomainRoute(
            //   "domain.com",
            //   "{controller}/{action}/{id}",
            //   new
            //   {
            //       controller = "Home",
            //       action = "Index",
            //       id = UrlParameter.Optional,
            //       Namespaces = new[] { "Portal.Controllers" }
            //   }
            // ));

            //  routes.Add(
            //  "WWWDomainRoute", new DomainRoute(
            //  "www.domain.com",
            //  "{controller}/{action}/{id}",
            //  new
            //  {
            //      controller = "Home",
            //      action = "Index",
            //      id = UrlParameter.Optional,
            //      Namespaces = new[] { "Portal.Controllers" }
            //  }
            //));

            //  routes.Add("DomainRouteForMutiWebSite", new DomainRoute(
            //      "{area}.domain.com",                             // {area}作为二级域名
            //      "{controller}/{action}/{id}",                  // URL with parameters
            //      new
            //      {
            //          area = "TZ",
            //          controller = "Home",
            //          action = "Index",
            //          id = "",
            //          Namespaces = new[] { "Portal.Areas.TZ.Controllers" }
            //      }  // Parameter defaults
            //  ));

            //  routes.Add("DomainRouteForMutilDomain", new DomainRoute(
            //    "{ShopName}.{area}.domain.com",                             // {ShopName}作为3级域名
            //    "{controller}/{action}/{id}",                  // URL with parameters
            //    new
            //    {
            //        ShopName = "",
            //        area = "B2B",
            //        controller = "Shops",
            //        action = "Index",
            //        id = UrlParameter.Optional,
            //        Namespaces = new[] { "Portal.Areas.B2B.Controllers" }
            //    }
            //  ));
        }
    }
}
