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
using System.Linq;

namespace System.Web.Mvc
{
    /// <summary>
    /// 需要登陆验证，页面继承类
    /// </summary>
    public class ManageControllerBase : WebControllerBase
    {
        #region 用户及登陆管理
        /// <summary>
        /// 需要登录
        /// </summary>
        public void ToLogin()
        {
            try
            {
                Response.Redirect(PublicHelper.ManageLoginUrl, true);
            }
            catch { }
        }
        #endregion

        
    }
}
