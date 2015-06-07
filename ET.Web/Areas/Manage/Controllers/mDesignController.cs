using ET.Sys_DEF;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Web.Areas.Manage.Controllers
{
    public class mDesignController : ManageControllerBase
    {
        //
        // GET: /Design/
        #region 创意设计操作
        public ActionResult GoodQuery()
        {
            return View();
        }
        public ActionResult GoodManage()
        {
            return View();
        }
        [HttpGet]
        public JsonResult AjaxQueryGoodPageList()
        {
            //接收datagrid传来的参数 
            int pageIndex = int.Parse(Request["page"]);
            int pageSize = int.Parse(Request["rows"]);
            string Condition = "";
            if (!string.IsNullOrEmpty(Request["name"]))
                Condition = " AND CHARINDEX('" + Request["name"] + "', GoodName)>0";
            long RecordTotalCount = 0;
            List<DesignGoodInfo> list = new ET.Sys_BLL.DesignBLL().PageList_DesignGoodInfo("GoodID,GoodName,CreateTime,ACCESSCOUNT,TYPEID", Condition, "CreateTime desc", pageIndex, pageSize, ref RecordTotalCount);
            return Json(new { total = RecordTotalCount, rows = list }, JsonRequestBehavior.AllowGet);
        }
        public JsonResult AjaxGetTypeSelectData()
        {
            List<KeyAndValue> list = new ET.Sys_BLL.PublicBLL().GetListByCondition<KeyAndValue>("TYPEID id,TYPENAME text", ET.Constant.DBConst.TableNames.DesignTypeInfo, null, "TYPESORT");
            return Json(list, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult AjaxSaveGood(FormCollection collection, string infoid)
        {
            bool IsInsert = false;
            string strResult = "false";
            DesignGoodInfo info = new ET.Sys_BLL.DesignBLL().Get_DesignGoodInfoByID(infoid);
            if (info == null)
            {
                IsInsert = true;
                info = new DesignGoodInfo();
                info.GoodID = Guid.NewGuid();
                info.CreateTime = DateTime.Now;
            }
            info.GoodSource = collection["GoodSource"];
            info.TypeID = Guid.Parse(collection["TypeID"]);
            info.GoodName = collection["GoodName"];
            if (string.IsNullOrEmpty(collection["GoodUrl"]))
            {
                info.GoodUrl = "/design/detail/" + info.GoodID + "/";
            }
            else
                info.GoodUrl = collection["GoodUrl"];
            if (!string.IsNullOrEmpty(collection["Status"]) && collection["Status"] == "on")
                info.Status = 1;
            else
                info.Status = 0;

            if (!string.IsNullOrEmpty(collection["Recommend"]) && collection["Recommend"] == "on")
                info.Recommend = 1;
            else
                info.Recommend = 0;
            string sss = collection["GoodContent"];
            info.GoodContent = collection["GoodContent"];
            info.GoodDescription = collection["GoodDescription"];
            info.GoodPicture = collection["GoodPicture"];
            
            info.Status = 1;
            if (new ET.Sys_BLL.DesignBLL().Operate_DesignGoodInfo(info, IsInsert))
                strResult = "true";
            return Content(strResult);
        }
        [HttpGet]
        public JsonResult AjaxGetGoodDetail(string infoid)
        {
            if (string.IsNullOrEmpty(infoid))
                return Json("", JsonRequestBehavior.AllowGet);
            DesignGoodInfo info = new ET.Sys_BLL.DesignBLL().Get_DesignGoodInfoByID(infoid);
            if (info == null)
                return Json("error", JsonRequestBehavior.AllowGet);
            return Json(info, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult AjaxDeleteGood(string infoid)
        {
            if (!string.IsNullOrEmpty(infoid) && new ET.Sys_BLL.DesignBLL().Delete_DesignGoodInfo(" AND GoodID in (" + infoid + ")"))
                return Content("true");
            else
                return Content("false");
        }
        [HttpGet]
        public ActionResult AjaxSearchGood(string query)
        {
            List<DesignGoodInfo> list = new ET.Sys_BLL.DesignBLL().List_DesignGoodInfo("GoodName", " AND  CHARINDEX('" + query + "', GoodName)>0", "CreateTime");
            var arrData = list.Select(c => c.GoodName);
            return Json(new { query = query, suggestions = arrData, data = arrData }, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region 类别操作方法
        public ActionResult TypeManage()
        {
            return View();
        }
        public ActionResult TypeQuery()
        {
            return View();
        }
        #region Ajax处理方法
        [HttpGet]
        public JsonResult AjaxQueryTypePageList()
        {
            //接收datagrid传来的参数 
            int pageIndex = int.Parse(Request["page"]);
            int pageSize = int.Parse(Request["rows"]);
            string Condition = "";
            if (!string.IsNullOrEmpty(Request["name"]))
                Condition = " AND CHARINDEX('" + Request["name"] + "', TYPENAME)>0";
            long RecordTotalCount = 0;
            List<DesignTypeInfo> list = new ET.Sys_BLL.DesignBLL().PageList_DesignTypeInfo("TYPEID,TYPENAME,TYPESORT", Condition, "TYPESORT", pageIndex, pageSize, ref RecordTotalCount);
            return Json(new { total = RecordTotalCount, rows = list }, JsonRequestBehavior.AllowGet);
        }
         [HttpGet]
        public JsonResult AjaxGetPTypeData(string infoid)
        {
            string condition = "";
            if (!string.IsNullOrEmpty(infoid))
                condition = " AND CHARINDEX('" + infoid + "',TYPEID)=0";
            List<KeyAndValue> list = new ET.Sys_BLL.PublicBLL().GetNestListByCondition("TYPEID id,TYPENAME text,TYPEpID pid", ET.Constant.DBConst.TableNames.DesignTypeInfo, condition, "TYPESORT desc");
            list.Insert(0, new KeyAndValue() { id = "-1", text = "根目录" });
            return Json(list, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult AjaxSaveType(FormCollection collection, string infoid)
        {
            bool IsInsert = false;
            string strResult = "false";
            DesignTypeInfo info = new ET.Sys_BLL.DesignBLL().Get_DesignTypeInfoByID(infoid);
            if (info == null)
            {
                IsInsert = true;
                info = new DesignTypeInfo();
            }
            info.TypePID = collection["TypePID"];
            info.TypeName = collection["TypeName"];
            info.TypeSort = collection["TypeSort"];
            if (new ET.Sys_BLL.DesignBLL().Operate_DesignTypeInfo(info, IsInsert))
                strResult = "true";

            return Content(strResult);
        }
        [HttpGet]
        public JsonResult AjaxGetTypeDetail(string infoid)
        {
            if (string.IsNullOrEmpty(infoid))
                return Json("", JsonRequestBehavior.AllowGet);
            DesignTypeInfo info = new ET.Sys_BLL.DesignBLL().Get_DesignTypeInfoByID(infoid);
            if (info == null)
                return Json("error", JsonRequestBehavior.AllowGet);
            return Json(info, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult AjaxDeleteType(string infoid)
        {
            if (!string.IsNullOrEmpty(infoid) && new ET.Sys_BLL.DesignBLL().Delete_DesignTypeInfo(" AND TYPEID='" + infoid + "'"))
                return Content("true");
            else
                return Content("false");
        }
        [HttpGet]
        public ActionResult AjaxSearchType(string query)
        {
            List<DesignTypeInfo> list = new ET.Sys_BLL.DesignBLL().List_DesignTypeInfo("TYPENAME", " AND  CHARINDEX('" + query + "', TYPENAME)>0", "TYPESORT");
            var arrData = list.Select(c => c.TypeName);
            return Json(new { query = query, suggestions = arrData, data = arrData }, JsonRequestBehavior.AllowGet);
        }
        #endregion
        #endregion


        [AcceptVerbs(HttpVerbs.Post)]
        public JsonResult Upload(HttpPostedFileBase fileData)
        {
            if (fileData != null)
            {
                try
                {
                    // 文件上传后的保存路径
                    string filePath = Server.MapPath("~/Upload/Design/Image/");
                    if (!Directory.Exists(filePath))
                    {
                        Directory.CreateDirectory(filePath);
                    }
                    string fileName = Path.GetFileName(fileData.FileName);// 原始文件名称
                    string fileExtension = Path.GetExtension(fileName); // 文件扩展名
                    string saveName = DateTime.Now.ToString("yyyyMMddHHmmssfff") + fileExtension; // 保存文件名称

                    fileData.SaveAs(filePath + saveName);

                    return Json(new { Success = true, FileName = fileName, SaveName = "/upload/shop/image/" + saveName });
                }
                catch (Exception ex)
                {
                    return Json(new { Success = false, Message = ex.Message }, JsonRequestBehavior.AllowGet);
                }
            }
            else
            {

                return Json(new { Success = false, Message = "请选择要上传的文件！" }, JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult DeleteUploadPic(string Url)
        {
            try
            {
                if (!string.IsNullOrEmpty(Url))
                    System.IO.File.Delete(Server.MapPath("~") + Server.UrlDecode(Url));
                return Content("true");
            }
            catch { return Content("error"); }

        }
    }
}
