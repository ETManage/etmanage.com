using ET.Sys_DEF;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Web.Areas.Manage.Controllers
{
    public class mShopController : ManageControllerBase
    {
        //
        // GET: /Shop/
        #region 购物商品操作
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
            List<ShopGoodInfo> list = new ET.Sys_BLL.ShopBLL().PageList_ShopGoodInfo("GoodID,GoodName,CreateTime,ACCESSCOUNT,TYPEID", Condition, "CreateTime desc", pageIndex, pageSize, ref RecordTotalCount);
            return Json(new { total = RecordTotalCount, rows = list }, JsonRequestBehavior.AllowGet);
        }
        public JsonResult AjaxGetTypeSelectData()
        {
            List<KeyAndValue> list = new ET.Sys_BLL.PublicBLL().GetListByCondition<KeyAndValue>("TYPEID id,TYPENAME text", ET.Constant.DBConst.TableNames.ShopTypeInfo, null, "TYPESORT");
            return Json(list, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult AjaxSaveGood(FormCollection collection, string infoid)
        {
            bool IsInsert = false;
            string strResult = "false";
            ShopGoodInfo info = new ET.Sys_BLL.ShopBLL().Get_ShopGoodInfoByID(infoid);
            if (info == null)
            {
                IsInsert = true;
                info = new ShopGoodInfo();
                info.GoodID = Guid.NewGuid();
                info.CreateTime = DateTime.Now;
            }
            info.GoodSource = collection["GoodSource"];
            info.TypeID = Guid.Parse(collection["TypeID"]);
            info.GoodName = collection["GoodName"];
            if (string.IsNullOrEmpty(collection["GoodUrl"]))
            {
                info.GoodUrl = "/shop/gooddetail/" + info.GoodID + "/";
            }
            else
                info.GoodUrl = collection["GoodUrl"];
            string sss = collection["GoodContent"];
            info.GoodContent = collection["GoodContent"];
            info.GoodDescription = collection["GoodDescription"];
            info.GoodPicture = collection["GoodPicture"];
            
            info.Status = 1;
            if (new ET.Sys_BLL.ShopBLL().Operate_ShopGoodInfo(info, IsInsert))
                strResult = "true";
            return Content(strResult);
        }
        [HttpGet]
        public JsonResult AjaxGetGoodDetail(string infoid)
        {
            if (string.IsNullOrEmpty(infoid))
                return Json("", JsonRequestBehavior.AllowGet);
            ShopGoodInfo info = new ET.Sys_BLL.ShopBLL().Get_ShopGoodInfoByID(infoid);
            if (info == null)
                return Json("error", JsonRequestBehavior.AllowGet);
            return Json(info, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult AjaxDeleteGood(string infoid)
        {
            if (!string.IsNullOrEmpty(infoid) && new ET.Sys_BLL.ShopBLL().Delete_ShopGoodInfo(" AND GoodID in (" + infoid + ")"))
                return Content("true");
            else
                return Content("false");
        }
        [HttpGet]
        public ActionResult AjaxSearchGood(string query)
        {
            List<ShopGoodInfo> list = new ET.Sys_BLL.ShopBLL().List_ShopGoodInfo("GoodName", " AND  CHARINDEX('" + query + "', GoodName)>0", "CreateTime");
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
            List<ShopTypeInfo> list = new ET.Sys_BLL.ShopBLL().PageList_ShopTypeInfo("TYPEID,TYPENAME,TYPESORT", Condition, "TYPESORT", pageIndex, pageSize, ref RecordTotalCount);
            return Json(new { total = RecordTotalCount, rows = list }, JsonRequestBehavior.AllowGet);
        }
         [HttpGet]
        public JsonResult AjaxGetPTypeData(string infoid)
        {
            string condition = "";
            if (!string.IsNullOrEmpty(infoid))
                condition = " AND CHARINDEX('" + infoid + "',TYPEID)=0";
            List<KeyAndValue> list = new ET.Sys_BLL.PublicBLL().GetNestListByCondition("TYPEID id,TYPENAME text,TYPEpID pid", ET.Constant.DBConst.TableNames.ShopTypeInfo, condition, "TYPESORT desc");
            list.Insert(0, new KeyAndValue() { id = "-1", text = "根目录" });
            return Json(list, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult AjaxSaveType(FormCollection collection, string infoid)
        {
            bool IsInsert = false;
            string strResult = "false";
            ShopTypeInfo info = new ET.Sys_BLL.ShopBLL().Get_ShopTypeInfoByID(infoid);
            if (info == null)
            {
                IsInsert = true;
                info = new ShopTypeInfo();
            }
            info.TypePID = collection["TypePID"];
            info.TypeName = collection["TypeName"];
            info.TypeSort = collection["TypeSort"];
            if (new ET.Sys_BLL.ShopBLL().Operate_ShopTypeInfo(info, IsInsert))
                strResult = "true";

            return Content(strResult);
        }
        [HttpGet]
        public JsonResult AjaxGetTypeDetail(string infoid)
        {
            if (string.IsNullOrEmpty(infoid))
                return Json("", JsonRequestBehavior.AllowGet);
            ShopTypeInfo info = new ET.Sys_BLL.ShopBLL().Get_ShopTypeInfoByID(infoid);
            if (info == null)
                return Json("error", JsonRequestBehavior.AllowGet);
            return Json(info, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult AjaxDeleteType(string infoid)
        {
            if (!string.IsNullOrEmpty(infoid) && new ET.Sys_BLL.ShopBLL().Delete_ShopTypeInfo(" AND TYPEID='" + infoid + "'"))
                return Content("true");
            else
                return Content("false");
        }
        [HttpGet]
        public ActionResult AjaxSearchType(string query)
        {
            List<ShopTypeInfo> list = new ET.Sys_BLL.ShopBLL().List_ShopTypeInfo("TYPENAME", " AND  CHARINDEX('" + query + "', TYPENAME)>0", "TYPESORT");
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
                    string filePath = Server.MapPath("~/Upload/Shop/Image/");
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
