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
    public class WebControllerBase : Controller
    {
        public string ErrorPage404Url = "/PageError/Error404";

        public void getSystemConfig()
        {
            string xmlpath = Server.MapPath(SystemConfigConst.WebSiteDir + SystemConfigConst.SystemConfigFile);
            XmlDocument xmldoc = new XmlDocument();
            xmldoc.Load(xmlpath);
            ViewBag.ApplicationImg = xmldoc.SelectSingleNode("/Condition/System/ApplicationImg").InnerText;
            ViewBag.ApplicationName = xmldoc.SelectSingleNode("Condition/System/ApplicationName").InnerText;
            ViewBag.PageLogo = xmldoc.SelectSingleNode("Condition/System/CompanyLogo").InnerText;
            ViewBag.CompanyName = xmldoc.SelectSingleNode("Condition/System/CompanyName").InnerText;
            ViewBag.CompanyUrl = xmldoc.SelectSingleNode("Condition/System/CompanyUrl").InnerText;
            ViewBag.PageVer = xmldoc.SelectSingleNode("Condition/System/Ver").InnerText;
            ViewBag.IsCanRegist = xmldoc.SelectSingleNode("Condition/System/IsRegist").InnerText;
        }

        public int GetOnlineUser()
        {
            ET.Sys_Base.OnlineUser.OnlineUserRecorder recorder = System.Web.HttpContext.Current.Cache[ET.Sys_Base.OnlineUser.OnlineHttpModule.g_onlineUserRecorderCacheKey] as ET.Sys_Base.OnlineUser.OnlineUserRecorder;
            return recorder != null ? recorder.GetUserCount() : 0;
        }
        public IList<ET.Sys_Base.OnlineUser.OnlineUser> GetListOnlineUser()
        {
            ET.Sys_Base.OnlineUser.OnlineUserRecorder recorder = System.Web.HttpContext.Current.Cache[ET.Sys_Base.OnlineUser.OnlineHttpModule.g_onlineUserRecorderCacheKey] as ET.Sys_Base.OnlineUser.OnlineUserRecorder;
            return recorder != null ? recorder.GetUserList() : null;
        }
        #region 共用方法
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

        public RedirectResult Goto404PageError()
        {
            return Redirect(this.ErrorPage404Url);
        }

        #region 参数验证

        /// <summary>
        /// 验证字符串是否为数值
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        protected bool IsNumeric(string str)
        {
            if (string.IsNullOrEmpty(str))
                return false;
            System.Text.RegularExpressions.Regex rx = new System.Text.RegularExpressions.Regex(@"^[0123456789]+$");
            return rx.IsMatch(str);
        }

        #endregion


        #region 通用方法处理
        private void FuncCellBack(IAsyncResult ar)
        {
            try
            {
                BeginInvokeDelegate dl = (BeginInvokeDelegate)(ar as System.Runtime.Remoting.Messaging.AsyncResult).AsyncDelegate;
                dl.EndInvoke(ar);
            }
            catch { }
        }
        private delegate void BeginInvokeDelegate();
        /// <summary>
        /// 异步处理方法
        /// </summary>
        /// <param name="action"></param>
        public void BeginInvoke(Action action)
        {
            BeginInvokeDelegate update = new BeginInvokeDelegate(delegate()
            {
                action();
            });
            update.BeginInvoke(new AsyncCallback(FuncCellBack), null);
        }
        /// <summary>
        /// 异步处理方法
        /// </summary>
        /// <param name="action"></param>
        public void BeginInvoke<T>(Action<T> action, T t)
        {
            BeginInvokeDelegate update = new BeginInvokeDelegate(delegate()
            {
                action(t);
            });
            update.BeginInvoke(new AsyncCallback(FuncCellBack), null);
        }
        /// <summary>
        /// 异步处理方法
        /// </summary>
        /// <param name="action"></param>
        public void BeginInvoke<T1, T2>(Action<T1, T2> action, T1 t1, T2 t2)
        {
            BeginInvokeDelegate update = new BeginInvokeDelegate(delegate()
            {
                action(t1, t2);
            });
            update.BeginInvoke(new AsyncCallback(FuncCellBack), null);
        }
        /// <summary>
        /// 异步处理方法
        /// </summary>
        /// <param name="action"></param>
        public void BeginInvoke<T1, T2, T3>(Action<T1, T2, T3> action, T1 t1, T2 t2, T3 t3)
        {
            BeginInvokeDelegate update = new BeginInvokeDelegate(delegate()
            {
                action(t1, t2, t3);
            });
            update.BeginInvoke(new AsyncCallback(FuncCellBack), null);
        }
        /// <summary>
        /// 异步处理方法
        /// </summary>
        /// <param name="action"></param>
        public void BeginInvoke<T1, T2, T3, T4>(Action<T1, T2, T3, T4> action, T1 t1, T2 t2, T3 t3, T4 t4)
        {
            BeginInvokeDelegate update = new BeginInvokeDelegate(delegate()
            {
                action(t1, t2, t3, t4);
            });
            update.BeginInvoke(new AsyncCallback(FuncCellBack), null);
        }
        /// <summary>
        /// 异步处理方法
        /// </summary>
        /// <param name="action"></param>
        public void BeginInvoke<T1, T2, T3, T4, T5>(Action<T1, T2, T3, T4, T5> action, T1 t1, T2 t2, T3 t3, T4 t4, T5 t5)
        {
            BeginInvokeDelegate update = new BeginInvokeDelegate(delegate()
            {
                action(t1, t2, t3, t4, t5);
            });
            update.BeginInvoke(new AsyncCallback(FuncCellBack), null);
        }


        #endregion


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
                Response.Redirect(PublicHelper.WebLoginUrl, true);
            }
            catch { }
        }
        #endregion
    }
}
