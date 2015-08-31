using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ET.Sys_Base.OnlineUser
{
    public class SupportFilterAttribute : ActionFilterAttribute
    {
        public string ActionName { get; set; }
        private string Area;
        // 方法被执行后的更新在线用户列表
        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            OnlineHttpModule.ProcessRequest();

        }
    }
}