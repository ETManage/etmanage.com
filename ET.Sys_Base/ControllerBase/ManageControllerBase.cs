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
            if (!this.IsLogin)
            {
                ToLogin();
                return;
            }
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
        public string UserID
        {
            get
            {
                if (User.Identity.IsAuthenticated)
                {
                    return User.Identity.Name;
                }
                else
                {
                    ToLogin();
                    return null;
                }
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

                    if (this.UserID != null)
                    {
                        string strUserID = this.UserID;
                        if (System.Web.HttpContext.Current.Cache[strUserID] == null || System.Web.HttpContext.Current.Cache[strUserID].ToString() != "out")
                        {
                            new ET.Sys_Base.Login_Ajax().GetCurrentUserInfo(strUserID);
                        }
                        else
                            ToLogin();
                    }
                    else
                    {
                        ToLogin();
                    }
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
        protected void ReLogin()
        {
            ClearLoginCache();
            ToLogin();
        }
        /// <summary>
        /// 清除登录缓存
        /// </summary>
        public void ClearLoginCache()
        {
            if (System.Web.HttpContext.Current.Cache[this.UserID] != null)
                System.Web.HttpContext.Current.Cache.Remove(this.UserID);
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
                return User.Identity.IsAuthenticated;
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
