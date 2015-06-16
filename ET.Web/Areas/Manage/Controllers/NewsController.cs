using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ET.Sys_DEF;
namespace Web.Areas.Manage.Controllers
{
    public class NewsController : ManageControllerBase
    {
        //
        // GET: /Manage/News/

      

        #region 新闻公告操作方法
        public ActionResult NewsQuery()
        {
            return View();
        }
        public ActionResult NewsManage()
        {
            return View();
        }
        #region Ajax处理方法
        [HttpGet]
        public JsonResult AjaxQueryNewPageList()
        {
            //接收datagrid传来的参数 
            int pageIndex = int.Parse(Request["page"]);
            int pageSize = int.Parse(Request["rows"]);
            string Condition = "";
            if (!string.IsNullOrEmpty(Request["name"]))
                Condition = " AND CHARINDEX('" + Request["name"] + "', NewTitle)>0";
            long RecordTotalCount = 0;
            List<NewInfo> list = new ET.Sys_BLL.NewsBLL().PageList_NewInfo("NewID,NewTitle,CreateTime", Condition, "CreateTime desc", pageIndex, pageSize, ref RecordTotalCount);
            return Json(new { total = RecordTotalCount, rows = list }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult AjaxSaveNew(FormCollection collection, string infoid)
        {
            bool IsInsert = false;
            string strResult = "false";
            NewInfo info = new ET.Sys_BLL.NewsBLL().Get_NewInfoByID(infoid);
            if (info == null)
            {
                IsInsert = true;
                info = new NewInfo();
            }

            info.NewTitle = collection["NewTitle"];
            info.NewContent = collection["NewContent"];

            info.NewSource = collection["NewSource"];
            if (!string.IsNullOrEmpty(collection["Status"]) && collection["Status"] == "on")
                info.Status = 1;
            else
                info.Status = 0;



            if (new ET.Sys_BLL.NewsBLL().Operate_NewInfo(info, IsInsert))
                strResult = "true";

            return Content(strResult);
        }
        [HttpGet]
        public JsonResult AjaxGetNewDetail(string infoid)
        {
            if (string.IsNullOrEmpty(infoid))
                return Json("", JsonRequestBehavior.AllowGet);
            NewInfo info = new ET.Sys_BLL.NewsBLL().Get_NewInfoByID(infoid);
            if (info == null)
                return Json("error", JsonRequestBehavior.AllowGet);
            return Json(info, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult AjaxDeleteNew(string infoid)
        {
            if (!string.IsNullOrEmpty(infoid) && new ET.Sys_BLL.NewsBLL().Delete_NewInfo(" AND NewID='" + infoid + "'"))
                return Content("true");
            else
                return Content("false");
        }
        [HttpGet]
        public ActionResult AjaxSearchNew(string query)
        {
            List<NewInfo> list = new ET.Sys_BLL.NewsBLL().List_NewInfo("NewTitle", " AND  CHARINDEX('" + query + "', NewTitle)>0", "CreateTime");
            var arrData = list.Select(c => c.NewTitle);
            return Json(new { query = query, suggestions = arrData, data = arrData }, JsonRequestBehavior.AllowGet);
        }
        #endregion
        #endregion
    }
}
