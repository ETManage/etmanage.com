using ET.Sys_DEF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ET.Web.Areas.Manage.Controllers
{
    public class mChatController : ManageControllerBase
    {
        //
        // GET: /Manage/mChat/
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult MessageView(string uid)
        {
            UserProperty info = new ET.Sys_BLL.OrganizationBLL().Get_UserPropertyByID(uid);
            if (info == null)
                return this.Goto404PageError();
            ViewBag.UserList = new ET.Sys_BLL.PublicBLL().GetListBySql<UserProperty>("SELECT A.USERID,A.CNNAME, A.PHOTO FROM  ChatMessage B INNER JOIN UserProperty  A ON A.USERID=B.SENDER  WHERE Receiver='" + this.UserID + "' ORDER BY CreateTime DESC");
            ViewBag.CurrChatList = new ET.Sys_BLL.PublicBLL().GetListBySql<ChatMessage>("SELECT B.SENDER,B.MSGTITLE,B.MESSAGECONTENT,B.CREATETIME,A.CNNAME RESERVE1, A.PHOTO RESERVE2, FROM  ChatMessage B INNER JOIN UserProperty  A ON A.USERID=B.SENDER  WHERE ( SENDER='" + uid + "' and  Receiver='" + this.UserID + "') or ( Receiver='" + uid + "' and  SENDER='" + this.UserID + "')   ORDER BY CreateTime");

            return View(info);
        }

        public JsonResult AjaxSaveChatMessage(string uid,string uname,string msg)
        {
            ChatMessage info = new ChatMessage();
            info.Sender = Guid.Parse(this.UserID);
            info.Receiver = Guid.Parse(uid);
            info.MsgContent = msg;
            info.CreateTime = DateTime.Now;
            info.Status = 0;
            if (new Sys_BLL.SystemBLL().Update_ChatMessage(info, true))
            {
                return Json(new { rname = uname, sname = this.CurrentUserInfo.CNName, sphoto = this.CurrentUserInfo.Photo, stime = info.CreateTime.Value.ToString("MM-dd HH:mm") }, JsonRequestBehavior.AllowGet);
            }
            return Json(null, JsonRequestBehavior.AllowGet); ;
        }
	}
}