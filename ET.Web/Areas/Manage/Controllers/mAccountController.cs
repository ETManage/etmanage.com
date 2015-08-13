using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ET.Sys_DEF;
using System.Web.Routing;

namespace ET.Web.Areas.Manage.Controllers
{
    public class mAccountController : WebControllerBase
    {
        //
        // GET: /Account/
        //[Route("manage/login")]
        public ActionResult Login()
        {
            base.getSystemConfig();
            return View();
        }

        #region Ajax处理方法

        //AJAX登录
        [HttpPost]
        public ActionResult AjaxLogin(string strUserName, string strUserPass, string strRememberMe)
        {
            string strReturn = "";
            if (ModelState.IsValid)
            {
                ET.Sys_Base.Login_Ajax loginAjax = new ET.Sys_Base.Login_Ajax();
                try
                {
                    if (loginAjax.SinglePointState() == "1")
                    {
                        strReturn = loginAjax.LoginSinglePoint(strUserName, strUserPass);

                    }
                    else
                    {
                        strReturn = loginAjax.LoginUser(strUserName, strUserPass);

                    }
                }
                catch (Exception ex)
                {
                    strReturn = ex.Message;
                }
            }
            return Content(strReturn);
        }

        [HttpGet]
        public ActionResult AjaxLogout()
        {
            ET.Sys_Base.Login_Ajax loginAjax = new ET.Sys_Base.Login_Ajax();
            loginAjax.Logout();
            return Redirect(PublicHelper.ManageLoginUrl);
        }
        #endregion
    }
}
