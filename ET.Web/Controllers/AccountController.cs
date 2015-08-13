using ET.Constant.DBConst;
using ET.Sys_DEF;
using ET.ToolKit.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ET.Web.Controllers
{
    public class AccountController : WebControllerBase
    {
        //
        // GET: /Account/

        public ActionResult Login()
        {
            if (this.IsLogin)
            {
                return Redirect("/user/");
            }
            else
            return View();
        }
        public ActionResult Logout()
        {
            ET.Sys_Base.Login_Ajax loginAjax = new ET.Sys_Base.Login_Ajax();
            loginAjax.Logout();
            return View();
        }


        public ActionResult ValidateCode()
        {
            int width = 100;
            int height = 40;
            int fontsize = 20;
            string code = string.Empty;
            byte[] bytes = ET.ToolKit.ToolKit.Drawing.ValidateCodeHelper.CreateValidateGraphic(out code, 4, width, height, fontsize);
            ET.ToolKit.ToolKit.Common.SessionHelper.Add(SystemConfigConst.SessioncheckValiCode, code);
            return File(bytes, @"image/jpeg");
        }


        #region Ajax处理
        [HttpPost]
        public ActionResult AjaxQQlogin(string u, string p, string name, string sex, string pic)
        {
            try
            {
                string strReturn = "";
                if (!string.IsNullOrEmpty(u) && !string.IsNullOrEmpty(p))
                {
                    UserBase baseinfo = new ET.Sys_BLL.OrganizationBLL().Get_UserBase(" AND UserName='" + u + "'");
                    if (baseinfo == null)
                    {
                        UserFullInfo info = new UserFullInfo();
                        baseinfo = new UserBase();
                        UserProperty stuinfo = new UserProperty();
                        baseinfo.UserName = u;
                        baseinfo.Status = 1;
                        baseinfo.UserPwd = ET.ToolKit.Encrypt.EncrypeHelper.EncryptMD5(p);
                        baseinfo.UserType = 0;
                        stuinfo.Nickname = u;
                        if (!string.IsNullOrEmpty(name))
                            stuinfo.CNName = name;
                        if (!string.IsNullOrEmpty(sex))
                            stuinfo.Sex = sex;
                        stuinfo.QQ = u;
                        if (!string.IsNullOrEmpty(pic))
                            stuinfo.Photo = pic;
                        stuinfo.Source = "QQ登录";
                        stuinfo.CreateTime = DateTime.Now;

                        info.userbaseinfo = baseinfo;
                        info.userstuinfo = stuinfo;
                        info.userrole = new List<UserRoleLink>();
                        UserRoleLink role = new UserRoleLink();
                        info.userrole.Add(role);
                        if (new ET.Sys_BLL.OrganizationBLL().Update_UserInfo(info, true))
                        {
                        }
                    }

                    if (baseinfo.Status == 1 || (baseinfo.Status == 0 && baseinfo.StartTime < DateTime.Now && baseinfo.EndTime >= DateTime.Now))
                    {
                        ET.Sys_Base.Login_Ajax loginAjax = new ET.Sys_Base.Login_Ajax();
                        try
                        {
                            if (loginAjax.SinglePointState() == "1")
                            {
                                strReturn = loginAjax.WebLoginSinglePoint(u, p, false);

                            }
                            else
                            {
                                strReturn = loginAjax.WebLoginUser(u, p, false);

                            }
                        }
                        catch (Exception ex)
                        {
                            strReturn = ex.Message;
                        }
                    }
                    else
                        strReturn = "非常抱歉！该帐号无权登录！请联系管理员";
                    if (strReturn == "true")
                    {
                        BlogUserLevelLink link = new ET.Sys_BLL.BlogBLL().Get_BlogUserLevelLink(string.Format("AND USERID='{0}' ", baseinfo.UserID));
                        if (link == null)
                        {
                            link = new BlogUserLevelLink();

                            link.UserID = baseinfo.UserID;
                             link.Exp = 10;
                            new ET.Sys_BLL.BlogBLL().Update_BlogUserLevelLink(link, true);
                        }
                        else
                        {
                            link.Exp++;
                            new ET.Sys_BLL.BlogBLL().Update_BlogUserLevelLink(link, false);
                        }
                    }
                }
                
                return Content(strReturn);
            }
            catch (Exception ex)
            {
                //throw ex;
                return Content(ex.Message);
            }


        }



        [HttpPost]
        //[AllowAnonymous]
        //[ValidateAntiForgeryToken]
        public ActionResult AjaxLogin(FormCollection collection, string l)
        {
            string strReturn = "";
            if (ModelState.IsValid)
            {
                ET.Sys_Base.Login_Ajax loginAjax = new ET.Sys_Base.Login_Ajax();
                try
                {
                    if (loginAjax.SinglePointState() == "1")
                    {
                        strReturn = loginAjax.WebLoginSinglePoint(collection["username"], collection["password"], false);

                    }
                    else
                    {
                        strReturn = loginAjax.WebLoginUser(collection["username"], collection["password"], false);

                    }
                    if (strReturn == "true")
                    {
                        BlogUserLevelLink link = new ET.Sys_BLL.BlogBLL().Get_BlogUserLevelLink(string.Format("AND USERID='{0}' ", this.UserID));
                        if (link == null)
                        {
                            link = new BlogUserLevelLink();

                            link.UserID = Guid.Parse(this.UserID);
                            link.Exp = 10;
                            new ET.Sys_BLL.BlogBLL().Update_BlogUserLevelLink(link, true);
                        }
                        else
                        {
                            link.Exp++;
                            new ET.Sys_BLL.BlogBLL().Update_BlogUserLevelLink(link, false);
                        }
                    }
                }
                catch (Exception ex)
                {
                    strReturn = ex.Message;
                }
            }
            
            return Content(strReturn);
        }
        [HttpPost]
        //[AllowAnonymous]
        //[ValidateAntiForgeryToken]
        public ActionResult AjaxMobileLogin(string username, string password)
        {
            string strReturn = "";
            if (ModelState.IsValid)
            {
                ET.Sys_Base.Login_Ajax loginAjax = new ET.Sys_Base.Login_Ajax();
                try
                {
                    strReturn = loginAjax.WebLoginUser(username, password, false);

                    
                    if (strReturn == "true")
                    {
                        BlogUserLevelLink link = new ET.Sys_BLL.BlogBLL().Get_BlogUserLevelLink(string.Format("AND USERID='{0}' ", this.UserID));
                        if (link == null)
                        {
                            link = new BlogUserLevelLink();

                            link.UserID = Guid.Parse(this.UserID);
                            link.Exp = 10;
                            new ET.Sys_BLL.BlogBLL().Update_BlogUserLevelLink(link, true);
                        }
                        else
                        {
                            link.Exp++;
                            new ET.Sys_BLL.BlogBLL().Update_BlogUserLevelLink(link, false);
                        }
                    }
                }
                catch (Exception ex)
                {
                    strReturn = ex.Message;
                }
            }

            return Content(strReturn);
        }
        [HttpPost]
        public ActionResult AjaxCheckUser(string uid)
        {
            uid = ET.ToolKit.Common.StringHelper.ClearSqlDangerous(uid);
            UserBase info = new ET.Sys_BLL.OrganizationBLL().Get_UserBase(" AND UserName='" + uid + "'");
            if (info != null)
                return Content("用户名已经存在！");
            else
                return Content("true");

        }
        [HttpPost]
        public ActionResult AjaxCheckValidatecode(string code)
        {
            string resultMsg = "true";
            string pCode = code;
            string sCode = "";
            object obj = ET.ToolKit.ToolKit.Common.SessionHelper.Get(SystemConfigConst.SessioncheckValiCode);
            if (obj != null)
                sCode = obj.ToString();
            if (string.IsNullOrEmpty(pCode))
            {
                resultMsg = "请输入验证码";
            }
            else if (string.IsNullOrEmpty(sCode))
            {
                resultMsg = "验证码过期";
            }
            else if (pCode.ToLower() != sCode.ToLower())
            {
                resultMsg = "验证码不正确";
            }

            return Content(resultMsg);

        }
        [HttpPost]
        public ActionResult AjaxRegister(FormCollection collection, string l)
        {



            UserFullInfo info = new UserFullInfo();
            UserBase baseinfo = new UserBase();
            UserProperty stuinfo = new UserProperty();



            baseinfo.UserName = collection["user"];
            baseinfo.UserPwd = ET.ToolKit.Encrypt.EncrypeHelper.EncryptMD5(collection["passwd"]);
            baseinfo.Status = 1;
            baseinfo.UserType = 1;
            stuinfo.Nickname = collection["user"];
            stuinfo.CNName = collection["user"];
            stuinfo.ENName = collection["user"];
            stuinfo.Sex = "保密";
            stuinfo.QQ = collection["qq"];
            stuinfo.CreateTime = DateTime.Now;
            info.userbaseinfo = baseinfo;
            info.userstuinfo = stuinfo;

            info.userrole = new List<UserRoleLink>();
            UserRoleLink role = new UserRoleLink();
            info.userrole.Add(role);
            if (new ET.Sys_BLL.OrganizationBLL().Update_UserInfo(info, true))
            {
                return Content("true");
            }
            else
                return Content("error");
        }
        [HttpPost]
        public ActionResult AjaxLogout()
        {

            return RedirectToAction("login");
        }


        public ActionResult QQLoginCallBack()
        {
            return View();
        }
        #endregion


    }
}
