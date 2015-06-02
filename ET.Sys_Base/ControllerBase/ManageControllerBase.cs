using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Collections.Generic;
using System.Xml;
using ET.Sys_DEF;
using ET.ToolKit.Common;


namespace System.Web.Mvc
{
    /// <summary>
    /// 需要登陆验证，页面继承类
    /// </summary>
    public class ManageControllerBase : ControllerHelper
    {
        public ManageControllerBase()
        {
            if (!this.IsLogin)
            {
                ToLogin();
                return;
            }
        }
        public int GetOnlineUser()
        {
            return System.Web.HttpContext.Current.Cache.Count - 4;
        }
        /// <summary>
        /// 读取系统配置,其中ViewBag.PageTitle：标题，ViewBag.PageLogo：Logo图片，ViewBag.PageCopyright：版权-
        /// </summary>
        /// <returns></returns>
        public void getSystemConfig()
        {
            string xmlpath = Server.MapPath(SystemConfigConst.WebSiteDir + SystemConfigConst.SystemConfigFile);
            XmlDocument xmldoc = new XmlDocument();
            xmldoc.Load(xmlpath);
            if (xmldoc.SelectSingleNode("Condition/System/IsRegist").InnerXml == "1")
            {
                Response.Write("hideRegist();");
            }
            ViewBag.PageTitleImg = xmldoc.SelectSingleNode("/Condition/System/TitleImg").InnerText;

            ViewBag.PageTitle = xmldoc.SelectSingleNode("Condition/System/PageTitle").InnerText;
            ViewBag.PageLogo = xmldoc.SelectSingleNode("Condition/System/CompanyLogo").InnerText;
            ViewBag.CompanyName = xmldoc.SelectSingleNode("Condition/System/CompanyName").InnerText;
            ViewBag.PageVer = xmldoc.SelectSingleNode("Condition/System/Ver").InnerText;

        }
       

        #region 用户及登陆管理
        /// <summary>
        /// 当前用户ID
        /// </summary>
        public Guid UserID
        {
            get
            {
                object objLoginState = CookieHelper.GetCookie(SystemConfigConst.ManageCookieUserID);
                if (objLoginState == null)
                {
                    return Guid.Empty;
                }
                else
                    return Guid.Parse(objLoginState.ToString());

            }
        }



        /// <summary>
        /// 当前用户信息
        /// </summary>
        public CurrentUserInfo CurrentUserInfo
        {
            get
            {
                CurrentUserInfo usertemp = Session[SystemConfigConst.SessionUserInfo] as CurrentUserInfo;
                if (usertemp == null)
                {
                    if (this.UserID != Guid.Empty && System.Web.HttpContext.Current.Cache[this.UserID.ToString()] != null)
                    {
                        if (System.Web.HttpContext.Current.Cache[this.UserID.ToString()].ToString() != "out")
                        {
                            new ET.Sys_Base.Login_Ajax().LoginUser(CookieHelper.GetCookie(SystemConfigConst.ManageCookieUserID).ToString(), null, true);
                        }
                        else
                            ToLogin();
                    }
                    else
                    {
                        ToLogin();
                    }
                }

                if (System.Web.HttpContext.Current.Cache[this.UserID.ToString()] == null || (System.Web.HttpContext.Current.Cache[this.UserID.ToString()] != null && System.Web.HttpContext.Current.Cache[this.UserID.ToString()].ToString() == "out"))
                {
                    ToLogin();
                }
                return usertemp;
            }
            set
            {
                Session[SystemConfigConst.SessionUserInfo] = value;
            }
        }


        /// <summary>
        /// 重新登录
        /// </summary>
        protected ActionResult ReLogin()
        {
            ClearLoginCache();
            return RedirectToAction("Login", "AccountController");
        }
        /// <summary>
        /// 清除登录缓存
        /// </summary>
        public void ClearLoginCache()
        {
            if (this.UserID != Guid.Empty)
                System.Web.HttpContext.Current.Cache.Remove(this.UserID.ToString());
            CookieHelper.ClearCookie(SystemConfigConst.ManageCookieUserID);
            Session.RemoveAll();
            Session.Abandon();
        }

        /// <summary>
        /// 获取是否登录，登录返回 True，未登录返回 False
        /// </summary>
        public bool IsLogin
        {
            get
            {
                return !(this.UserID == Guid.Empty);
            }
        }
        /// <summary>
        /// 需要登录
        /// </summary>
        public void ToLogin()
        {
            try
            {
                Response.Redirect("/Account/Login", true);
            }
            catch { }
        }
        #endregion
    }
}
