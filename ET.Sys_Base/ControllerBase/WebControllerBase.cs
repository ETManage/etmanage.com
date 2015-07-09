﻿using System;
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
using System.IO;
using System.Text;
using System.Reflection;
using ET.Sys_DEF;
using System.Text.RegularExpressions;
using System.Diagnostics;
using System.Threading;
using System.Xml;

using ET.Sys_Base;
using ET.ToolKit;
using ET.ToolKit.Common;


namespace System.Web.Mvc
{    /// <summary>
    /// 需要需要登陆验证，页面继承类
    /// </summary>
    public class WebControllerBase : ControllerHelper
    {
        public WebControllerBase()
        {

        }
        public Boolean IsMobileDevice()
        {

            String[] mobileAgents = { "iphone", "android", "phone", "mobile", "wap", "netfront", "java", "opera mobi", "opera mini", "ucweb", "windows ce", "symbian", "series", "webos", "sony", "blackberry", "dopod", "nokia", "samsung", "palmsource", "xda", "pieplus", "meizu", "midp", "cldc", "motorola", "foma", "docomo", "up.browser", "up.link", "blazer", "helio", "hosin", "huawei", "novarra", "coolpad", "webos", "techfaith", "palmsource", "alcatel", "amoi", "ktouch", "nexian", "ericsson", "philips", "sagem", "wellcom", "bunjalloo", "maui", "smartphone", "iemobile", "spice", "bird", "zte-", "longcos", "pantech", "gionee", "portalmmm", "jig browser", "hiptop", "benq", "haier", "^lct", "320x320", "240x320", "176x220", "w3c ", "acs-", "alav", "alca", "amoi", "audi", "avan", "benq", "bird", "blac", "blaz", "brew", "cell", "cldc", "cmd-", "dang", "doco", "eric", "hipt", "inno", "ipaq", "java", "jigs", "kddi", "keji", "leno", "lg-c", "lg-d", "lg-g", "lge-", "maui", "maxo", "midp", "mits", "mmef", "mobi", "mot-", "moto", "mwbp", "nec-", "newt", "noki", "oper", "palm", "pana", "pant", "phil", "play", "port", "prox", "qwap", "sage", "sams", "sany", "sch-", "sec-", "send", "seri", "sgh-", "shar", "sie-", "siem", "smal", "smar", "sony", "sph-", "symb", "t-mo", "teli", "tim-", "tosh", "tsm-", "upg1", "upsi", "vk-v", "voda", "wap-", "wapa", "wapi", "wapp", "wapr", "webc", "winw", "winw", "xda", "xda-", "Googlebot-Mobile" };
            Boolean isMoblie = false;
            if (Request.UserAgent.ToString().ToLower() != null)
            {
                for (int i = 0; i < mobileAgents.Length; i++)
                {
                    if (Request.UserAgent.ToString().ToLower().IndexOf(mobileAgents[i]) >= 0)
                    {
                        isMoblie = true;
                        break;
                    }
                }
            }
            if (isMoblie)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
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

        public int GetOnlineUser()
        {
            return System.Web.HttpContext.Current.Cache.Count - 4;
        }

        #region 共用方法

        /// <summary>
        /// 判断所有子集中是否存在某一个值
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="pfield"></param>
        /// <param name="field"></param>
        /// <param name="typeid"></param>
        /// <param name="ptypeid"></param>
        /// <returns></returns>
        public bool IsunAccept(DataTable dt, string pfield, string field, string typeid, string ptypeid)
        {
            string ids = typeid + ",";
            GetSubType(dt, pfield, field, typeid, ref ids);
            string[] list = ids.Split(new string[] { "," }, StringSplitOptions.None);
            for (int i = 0; i < list.Length; i++)
            {
                if (list[i] == ptypeid)
                    return false;
                else
                    continue;
            }
            return true;
        }
        /// <summary>
        /// 获取到类型中的所有子集ID，例1,2
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="pfield"></param>
        /// <param name="field"></param>
        /// <param name="pid"></param>
        /// <param name="ids"></param>
        private void GetSubType(DataTable dt, string pfield, string field, string pid, ref string ids)
        {
            foreach (DataRow Row in dt.Select("[" + pfield + "] =" + int.Parse(pid)))
            {
                ids += Row["" + field + ""].ToString() + ",";
                GetSubType(dt, pfield, field, Row["" + field + ""].ToString(), ref ids);
            }
        }




        /// <summary>
        /// 分页查询，返回ViewBag.listPager
        /// </summary>
        public void _GetListPager(int pageIndex, int pageSize, long RecordTotalCount)
        {

            string strPagerText = "<div class=' pagination'><ul>";

            string strCurrUrl = Request.Path.ToString() + "?";

            if (pageIndex == 1)
            {
                strPagerText += "<li class='prev-page'></li>";
            }
            else
                strPagerText += "<li class='prev-page'><a href='" + strCurrUrl + "page=" + (pageIndex - 1) + "' >上一页</a></li>";

            for (int i = ((pageIndex - 3) > 0 ? (pageIndex - 3) : 1); i < pageIndex; i++)
            {
                strPagerText += "<li><a href='" + strCurrUrl + "page=" + i + "' >" + i + "</a></li>";
            }
            strPagerText += "<li class='active'><span>" + pageIndex + "</span></li>";


            for (int i = pageIndex + 1; i <= Math.Ceiling((decimal)RecordTotalCount / pageSize); i++)
            {
                strPagerText += "<li><a href='" + strCurrUrl + "page=" + i + "' >" + i + "</a></li>";
            }
            if (pageIndex == Math.Ceiling((decimal)RecordTotalCount / pageSize))
            {
                strPagerText += "<li class='next-page'></li>";
            }
            else
                strPagerText += "<li class='next-page'><a href='" + strCurrUrl + "page=" + (pageIndex + 1) + "' >下一页</a></li>";

            strPagerText += "</ul>        </div>";
            ViewBag.listPager = strPagerText;
        }

        #endregion



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
                    string strUserID = this.UserID;
                    if (strUserID != null)
                    {
                        if (System.Web.HttpContext.Current.Cache[strUserID] == null || System.Web.HttpContext.Current.Cache[strUserID].ToString() != "out")
                        {
                            return new ET.Sys_Base.Login_Ajax().GetCurrentUserInfo(strUserID);
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
