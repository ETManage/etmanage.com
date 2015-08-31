using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;

namespace ET.Sys_Base
{
    public class FormAuthService
    {
        public static void SignIn(string userID, bool createPersistentCookie, IEnumerable<string> roles)
        {
            //var str = string.Join(",", roles);
            var userData = string.Format("{0}:{1}", userID,
                                                       string.Join(",", roles));
            var authTicket = new FormsAuthenticationTicket(
                1,
                userID,  //user id
                DateTime.Now,
                DateTime.Now.AddDays(30),  // expiry
                createPersistentCookie,
                userData,
                "/");
            var cookie = new HttpCookie(FormsAuthentication.FormsCookieName, FormsAuthentication.Encrypt(authTicket));

            if (authTicket.IsPersistent)
            {
                cookie.Expires = authTicket.Expiration;
            }
            //FormsAuthentication.SetAuthCookie(userID, createPersistentCookie);
            HttpContext.Current.Response.Cookies.Add(cookie);
        }

        public static void SignOut()
        {
            FormsAuthentication.SignOut();
        }

        public static void RememberLoginName(string name)
        {
            var cookie = new HttpCookie(System.Web.HttpContext.Current.Request.Url.Authority + "_loginname", name);
            cookie.Expires = DateTime.MaxValue;
            HttpContext.Current.Response.Cookies.Add(cookie);
        }
        public static void RemoveRememberLoginName()
        {

            string strCookieName = System.Web.HttpContext.Current.Request.Url.Authority + "_loginname";
            HttpCookie cook = HttpContext.Current.Request.Cookies[strCookieName];
            if (cook != null)
            {
                TimeSpan ts = new TimeSpan(-1, 0, 0, 0);
                cook.Expires = DateTime.Now.Add(ts);//删除整个Cookie，只要把过期时间设置为现在
                HttpContext.Current.Response.AppendCookie(cook);
            }
        }

    }
}