using ET.Constant.DBConst;
using ET.Sys_DEF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace Web.Controllers
{
    public class OneGTDController : WebControllerBase
    {
        //
        // GET: /WeiXin/

        public  OneGTDController()
        {
            ViewBag.GTDInboxCount = new ET.Sys_BLL.PublicBLL().GetRecordCount(TableNames.GTDInbox, null);
            ViewBag.GTDTodayCount = new ET.Sys_BLL.PublicBLL().GetRecordCount(TableNames.GTDTask, " AND DoType='today'");
            ViewBag.GTDTommorrowCount = new ET.Sys_BLL.PublicBLL().GetRecordCount(TableNames.GTDTask, " AND DoType='tomorrow'");
        }
 

        public ActionResult Index()
        {
            
            return View();
        }
        public ActionResult InBox()
        {
            //List<GTDInbox> listGTDInbox = new ET.Sys_BLL.PublicBLL().GetListByCondition<GTDInbox>(10, "*", TableNames.GTDInbox, "AND UserID='" + this.UserID.ToString() + "'", "CreateTime DESC");
            List<GTDInbox> listGTDInbox = new ET.Sys_BLL.PublicBLL().GetListByCondition<GTDInbox>(20, "*", TableNames.GTDInbox, null, "CreateTime DESC");
            ViewBag.listGTDInbox = listGTDInbox;
            return View();
        }
        public ActionResult InBoxEdit()
        {
            return View();
        }


        public ActionResult ToDay()
        {
            List<GTDTask> listGTDTask = new ET.Sys_BLL.PublicBLL().GetListByCondition<GTDTask>(20, "*", TableNames.GTDTask, " AND DoType='today'", "CreateTime DESC");
            ViewBag.listGTDTask = listGTDTask;
            return View();
        }
        public ActionResult Tomorrow()
        {
            List<GTDTask> listGTDTask = new ET.Sys_BLL.PublicBLL().GetListByCondition<GTDTask>(20, "*", TableNames.GTDTask, " AND DoType='tomorrow'", "CreateTime DESC");
            ViewBag.listGTDTask = listGTDTask;
            return View();
        }
        public ActionResult Finish()
        {
            List<GTDInbox> listGTDRecycle = new ET.Sys_BLL.PublicBLL().GetListByCondition<GTDInbox>(20, "*", TableNames.GTDRecycle, " AND RecycleType='taskover'", "CreateTime DESC");
            ViewBag.listGTDRecycle = listGTDRecycle;
            
            return View();
        }
        public ActionResult Recycle()
        {
            List<GTDInbox> listGTDRecycle = new ET.Sys_BLL.PublicBLL().GetListByCondition<GTDInbox>(20, "*", TableNames.GTDRecycle, " AND charindex(RecycleType,'taskover')=0", "CreateTime DESC");
            ViewBag.listGTDRecycle = listGTDRecycle;
            return View();
        }

        public ActionResult InBoxAdd()
        {
            return View();
        }

        public ActionResult TaskEdit()
        {
            return View();
        }
        public ActionResult RecycleDetail()
        {
            return View();
        }
        
        #region Ajax处理
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult AjaxSaveInBox(FormCollection collection, string id)
        {
            bool IsInsert = false;
            string strResult = "false";
            GTDInbox info = new ET.Sys_BLL.OneGTDBLL().Get_GTDInboxByID(id);
            if (info == null)
            {
                IsInsert = true;
                info = new GTDInbox();
                info.CreateTime = DateTime.Now;
            }
            info.BoxTitle = collection["BoxTitle"];
            info.BoxLabel = collection["BoxLabel"];
            info.BoxContent = collection["BoxContent"];
            //if( !string.IsNullOrEmpty(collection["StartTime"]))
            //info.StartTime =Convert.ToDateTime(collection["StartTime"]);
            //if (!string.IsNullOrEmpty(collection["EndTime"]))
            //info.EndTime = Convert.ToDateTime(collection["EndTime"]);
            info.Priority = collection["Priority"];
            if (new ET.Sys_BLL.OneGTDBLL().Operate_GTDInbox(info, IsInsert))
                strResult = "true";
            return Content(strResult);
        }
        [HttpGet]
        public JsonResult AjaxGetInBoxDetail(string id)
        {
            if (string.IsNullOrEmpty(id))
                return Json("", JsonRequestBehavior.AllowGet);
            GTDInbox info = new ET.Sys_BLL.OneGTDBLL().Get_GTDInboxByID(id);
            if (info == null)
                return Json("error", JsonRequestBehavior.AllowGet);
            return Json(info, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult AjaxSaveTask(FormCollection collection, string id,string t)
        {
            bool IsInsert = false;
            string strResult = "false";
            GTDTask info = new ET.Sys_BLL.OneGTDBLL().Get_GTDTaskByID(id);
            if (info == null)
            {
                IsInsert = true;
                info = new GTDTask();
                info.CreateTime = DateTime.Now;
            }
            info.TaskTitle = collection["TaskTitle"];
            info.TaskLabel = collection["TaskLabel"];
            info.TaskContent = collection["TaskContent"];

            if (!string.IsNullOrEmpty(collection["DoType"]))
                info.DoType = collection["DoType"];
            else
                info.DoType = t;
            //if( !string.IsNullOrEmpty(collection["StartTime"]))
            //info.StartTime =Convert.ToDateTime(collection["StartTime"]);
            //if (!string.IsNullOrEmpty(collection["EndTime"]))
            //info.EndTime = Convert.ToDateTime(collection["EndTime"]);
            info.Priority = collection["Priority"];
            if (new ET.Sys_BLL.OneGTDBLL().Operate_GTDTask(info, IsInsert))
                strResult = "true";
            return Content(strResult);
        }
        [HttpGet]
        public JsonResult AjaxGetTaskDetail(string id)
        {
            if (string.IsNullOrEmpty(id))
                return Json("", JsonRequestBehavior.AllowGet);
            GTDTask info = new ET.Sys_BLL.OneGTDBLL().Get_GTDTaskByID(id);
            if (info == null)
                return Json("error", JsonRequestBehavior.AllowGet);
            return Json(info, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult AjaxAddInBox(FormCollection collection, string id)
        {

            string strResult = "false";
            switch (collection["DoType"])
            {
                case "inbox":
                    GTDInbox binfo = new GTDInbox();
                    binfo.CreateTime = DateTime.Now;
                    binfo.BoxTitle = collection["BoxTitle"];
                    binfo.BoxLabel = collection["BoxLabel"];
                    binfo.BoxContent = collection["BoxContent"];
                    binfo.Priority = collection["Priority"];
                    if (new ET.Sys_BLL.OneGTDBLL().Operate_GTDInbox(binfo, true))
                        strResult = "true";
                    break;
                case "today":
                case "tomorrow":
                    GTDTask tinfo = new GTDTask();
                    tinfo.CreateTime = DateTime.Now;
                    tinfo.TaskTitle = collection["BoxTitle"];
                    tinfo.TaskLabel = collection["BoxLabel"];
                    tinfo.TaskContent = collection["BoxContent"];
                    tinfo.Priority = collection["Priority"];
                    tinfo.DoType = collection["DoType"];
                    if (new ET.Sys_BLL.OneGTDBLL().Operate_GTDTask(tinfo, true))
                        strResult = "true";
                    break;
            }
            return Content(strResult);
        }
        [HttpPost]
        public ActionResult AjaxChangeDoType(string ids,string t)
        {
            string strResult = "true";
            if (string.IsNullOrEmpty(ids))
                return Json("", JsonRequestBehavior.AllowGet);
            string[] arrid = ids.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries);

            foreach (string item in arrid)
            {
                GTDInbox boxinfo = new ET.Sys_BLL.OneGTDBLL().Get_GTDInboxByID(item);
                if (boxinfo != null)
                {
                    GTDTask info = new GTDTask();
                    info.TaskTitle = boxinfo.BoxTitle;
                    info.TaskLabel = boxinfo.BoxLabel;
                    info.TaskContent = boxinfo.BoxContent;
                    info.Priority = boxinfo.Priority;
                    info.CreateTime = DateTime.Now;
                    info.DoType = t;
                    if (new ET.Sys_BLL.OneGTDBLL().Operate_GTDTask(info, true)) ;
                    new ET.Sys_BLL.OneGTDBLL().Delete_GTDInbox(boxinfo);
                }
            }
            return Content(strResult);
        }
        [HttpPost]
        public ActionResult AjaxInBoxDelete(string ids)
        {
            string strResult = "true";
            if (string.IsNullOrEmpty(ids))
                return Json("", JsonRequestBehavior.AllowGet);
            string[] arrid = ids.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries);

            foreach (string item in arrid)
            {
                GTDInbox boxinfo = new ET.Sys_BLL.OneGTDBLL().Get_GTDInboxByID(item);
                if (boxinfo != null)
                {
                    new ET.Sys_BLL.OneGTDBLL().Delete_GTDInbox(boxinfo);
                }
            }
            return Content(strResult);
        }
        //异步加载数据
        [HttpGet]
        public JsonResult AjaxGetInBoxList(string groupNumber)
        {
            if (string.IsNullOrEmpty(groupNumber))
                return Json("", JsonRequestBehavior.AllowGet);
            int intPageSize = 20;
            long lngRecordTotalCount = 0;
            string strCondition = "";
            List<GTDInbox> list = new ET.Sys_BLL.OneGTDBLL().PageList_GTDInbox("*", strCondition, "CreateTime desc", int.Parse(groupNumber), intPageSize, ref lngRecordTotalCount);
            return Json(new { total = lngRecordTotalCount, rows = list }, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public JsonResult AjaxGetTodayList(string groupNumber)
        {
            if (string.IsNullOrEmpty(groupNumber))
                return Json("", JsonRequestBehavior.AllowGet);
            int intPageSize = 20;
            long lngRecordTotalCount = 0;
            string strCondition = "AND DOTYPE='today'";
            List<GTDTask> list = new ET.Sys_BLL.OneGTDBLL().PageList_GTDTask("*", strCondition, "CreateTime desc", int.Parse(groupNumber), intPageSize, ref lngRecordTotalCount);
            return Json(new { total = lngRecordTotalCount, rows = list }, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public JsonResult AjaxGetTomorrowList(string groupNumber)
        {
            if (string.IsNullOrEmpty(groupNumber))
                return Json("", JsonRequestBehavior.AllowGet);
            int intPageSize = 20;
            long lngRecordTotalCount = 0;
            string strCondition = "AND DOTYPE='tomorrow'";
            List<GTDTask> list = new ET.Sys_BLL.OneGTDBLL().PageList_GTDTask("*", strCondition, "CreateTime desc", int.Parse(groupNumber), intPageSize, ref lngRecordTotalCount);
            return Json(new { total = lngRecordTotalCount, rows = list }, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public JsonResult AjaxGetFinishList(string groupNumber)
        {
            if (string.IsNullOrEmpty(groupNumber))
                return Json("", JsonRequestBehavior.AllowGet);
            int intPageSize = 20;
            long lngRecordTotalCount = 0;
            string strCondition = "AND RecycleType='taskover'";
            List<GTDRecycle> list = new ET.Sys_BLL.OneGTDBLL().PageList_GTDRecycle("*", strCondition, "CreateTime desc", int.Parse(groupNumber), intPageSize, ref lngRecordTotalCount);
            return Json(new { total = lngRecordTotalCount, rows = list }, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public JsonResult AjaxGetRecycleList(string groupNumber)
        {
            if (string.IsNullOrEmpty(groupNumber))
                return Json("", JsonRequestBehavior.AllowGet);
            int intPageSize = 20;
            long lngRecordTotalCount = 0;
            string strCondition = "AND charindex(RecycleType,'taskover')=0";
            List<GTDRecycle> list = new ET.Sys_BLL.OneGTDBLL().PageList_GTDRecycle("*", strCondition, "CreateTime desc", int.Parse(groupNumber), intPageSize, ref lngRecordTotalCount);
            return Json(new { total = lngRecordTotalCount, rows = list }, JsonRequestBehavior.AllowGet);
        }
        #endregion
    }
}
