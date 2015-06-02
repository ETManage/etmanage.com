using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ET.Sys_DEF;

namespace Web.Areas.Manage.Controllers
{
    public class AccountController : WebControllerBase
    {
        //
        // GET: /Account/
        public ActionResult Login()
        {
           getSystemConfig();
            return View();
        }

        public ActionResult GoLogin()
        {
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
            return RedirectToAction("Login");
        }
        [HttpGet]
        public JsonResult Test()
        {
            return Json(new { menus = new ET.Sys_BLL.SystemBLL().List_ModuleMenuData(this.UserID.ToString()) }, JsonRequestBehavior.AllowGet);
        }
        #endregion
    }
}
