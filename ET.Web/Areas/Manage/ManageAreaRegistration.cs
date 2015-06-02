using System.Web.Mvc;

namespace Web.Areas.Manage
{
    public class ManageAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "Manage";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            //context.MapRoute(
            //    "Manage_default",
            //    "manage/{controller}/{action}/{id}",
            //    new { action = "Index", id = UrlParameter.Optional }
            //);

            //2015/5/11 West 修改系统默认路由器！清除掉区域前缀
            context.MapRoute(
                "Manage_default",
                "{controller}/{action}/{id}",
                new[] { "Ed.Web.Areas.Manage" }, //默认控制器的命名空间 (这里一定要加，非常重要)
                new { controller = "Account", action = "Login", id = UrlParameter.Optional }
                );

        }
    }
}
