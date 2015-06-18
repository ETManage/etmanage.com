using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Web
{
    /// <summary>
    /// 自定义AuthorizeAttribute(授权属性)
    /// </summary>
    public class UserAuthorize : AuthorizeAttribute
    {
        /// <summary>
        /// 有权限的用户
        /// </summary>
        public string FuncName { get; set; }


        /// <summary>
        /// 授权失败时呈现的地址
        /// </summary>
        public string ErrorUrl { get; set; }

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
            if (!string.IsNullOrEmpty(FuncName))
            {
                if (ApplicationConfig.dirApplicationUserLimit[httpContext.User.Identity.Name].Contains(FuncName))
                {
                    return true;
                }
            }
            else
                return base.AuthorizeCore(httpContext);//系统授权验证

            return false;//进入HandleUnauthorizedRequest
        }

        /// <summary>
        /// 处理授权失败的HTTP请求
        /// </summary>
        /// <param name="filterContext"></param>
        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            base.HandleUnauthorizedRequest(filterContext);
            //filterContext.Result = new ViewResult { ViewName = View };
            base.HandleUnauthorizedRequest(filterContext);
            if (filterContext == null)
            {
                throw new ArgumentNullException("filterContext");
            }
            else
            {
                if (!string.IsNullOrEmpty(ErrorUrl))
                {
                    filterContext.HttpContext.Response.Redirect(ErrorUrl);
                }
                else
                    filterContext.HttpContext.Response.Redirect("/maccount/login");
            }
            //filterContext.Result = new RedirectResult("/Admin/Dashboard");
        }
    }
}