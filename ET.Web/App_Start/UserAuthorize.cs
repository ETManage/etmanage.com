using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ET.Extensions
{
    /// <summary>
    /// 自定义AuthorizeAttribute(授权属性)
    /// </summary>
    public class UserAuthorize:AuthorizeAttribute
    {
        /// <summary>
        /// 有权限的用户
        /// </summary>
        public string AllowUser { get; set; }

        /// <summary>
        /// 授权失败时呈现的视图名称
        /// </summary>
        public string View { get; set; }

        /// <summary>
        /// 请求授权时执行
        /// </summary>
        /// <param name="filterContext"></param>
        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            base.OnAuthorization(filterContext);
            //可用如下方法获得请求授权页面的controller和action
            //var controller = filterContext.RouteData.Values["controller"].ToString();
            //var action = filterContext.RouteData.Values["action"].ToString();
        }

        /// <summary>
        /// 自定义授权检查
        /// </summary>
        /// <param name="httpContext"></param>
        /// <returns></returns>
        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            //return base.AuthorizeCore(httpContext);//系统授权验证
            foreach (string s in AllowUser.Split(','))
            {
                if (ApplicationConfig.dirApplicationRoleConfig[httpContext.User.Identity.Name].Contains(s))
                {
                    return true;
                }
            }
            return false;//进入HandleUnauthorizedRequest
        }

        /// <summary>
        /// 处理授权失败的HTTP请求
        /// </summary>
        /// <param name="filterContext"></param>
        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            base.HandleUnauthorizedRequest(filterContext);
            filterContext.Result = new ViewResult { ViewName = View };
            //filterContext.Result = new RedirectResult("/Admin/Dashboard");
        }
    }
}