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
         
            if (authTicket.IsPersistent){
                cookie.Expires = authTicket.Expiration;
            }
            //FormsAuthentication.SetAuthCookie(userID, createPersistentCookie);
            HttpContext.Current.Response.Cookies.Add(cookie);
        }

        public static void SignOut()
        {
            FormsAuthentication.SignOut();
        }
    }
}