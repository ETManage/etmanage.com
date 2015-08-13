using System.Web.Mvc;

namespace ET.Web.Areas.Manage
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
            context.MapRoute("Manage_login", "manage/login",
                       new { controller = "mAccount", action = "Login" });
            context.MapRoute("Manage_Index", "manage/default",
           new { controller = "System", action = "Default" });
            context.MapRoute(
                "Manage_default",
                "Manage/{controller}/{action}/{id}",
                new { controller = "Blog", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}