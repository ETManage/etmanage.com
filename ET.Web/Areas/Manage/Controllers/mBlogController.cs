using ET.Sys_DEF;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Web.Areas.Manage.Controllers
{
    public class mBlogController : ManageControllerBase
    {
        //
        // GET: /Blog/
        #region 博客文章操作
        public ActionResult ArticleQuery()
        {
            return View();
        }
        public ActionResult ArticleManage()
        {
            return View();
        }
        [HttpGet]
        public JsonResult AjaxQueryArticlePageList()
        {
            //接收datagrid传来的参数 
            int pageIndex = int.Parse(Request["page"]);
            int pageSize = int.Parse(Request["rows"]);
            string Condition = "";
            if (!string.IsNullOrEmpty(Request["name"]))
                Condition = " AND CHARINDEX('" + Request["name"] + "', ARTICLETITLE)>0";
            long RecordTotalCount = 0;
            List<BlogArticleInfo> list = new ET.Sys_BLL.BlogBLL().PageList_BlogArticleInfo("ARTICLEID,ARTICLETITLE,ArticleLabel,ArticleUrl,ArticleUrl,LoveCount,ShareCount,Status,CreateTime,ACCESSCOUNT,TYPEID,(SELECT TYPENAME FROM BlogTypeInfo WHERE TYPEID=BlogArticleInfo.TYPEID)ArticleSource", Condition, "CreateTime desc", pageIndex, pageSize, ref RecordTotalCount);
            return Json(new { total = RecordTotalCount, rows = list }, JsonRequestBehavior.AllowGet);
        }
        public JsonResult AjaxGetTypeSelectData()
        {
            List<KeyAndValue> list = new ET.Sys_BLL.PublicBLL().GetListByCondition<KeyAndValue>("TYPEID id,TYPENAME text", ET.Constant.DBConst.TableNames.BlogTypeInfo, null, "TYPESORT");
            return Json(list, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult AjaxSaveArticle(FormCollection collection, string infoid)
        {
            bool IsInsert = false;
            string strResult = "false";
            BlogArticleInfo info = new ET.Sys_BLL.BlogBLL().Get_BlogArticleInfoByID(infoid);
            if (info == null)
            {
                IsInsert = true;
                info = new BlogArticleInfo();
                info.ArticleID = Guid.NewGuid();
                info.CreateTime = DateTime.Now;
            }
            info.ArticleSource = collection["ArticleSource"];
            info.TypeID = Guid.Parse(collection["Typeid"]);
            info.ArticleTitle = collection["ArticleTitle"];
            info.ArticleAuthor = collection["ArticleAuthor"];
            if (string.IsNullOrEmpty(collection["ArticleUrl"]))
            {
                info.ArticleUrl = "/blog/detail/" + info.ArticleID;
            }
            else
                info.ArticleUrl = collection["ArticleUrl"];
            if (!string.IsNullOrEmpty(collection["Status"]) && collection["Status"] == "on")
                info.Status = 1;
            else
                info.Status = 0;

            if (!string.IsNullOrEmpty(collection["Recommend"]) && collection["Recommend"] == "on")
                info.Recommend = 1;
            else
                info.Recommend = 0;
            if (!string.IsNullOrEmpty(collection["IsRoll"]) && collection["IsRoll"] == "on")
                info.IsRoll = true;
            else
                info.IsRoll = false;
            info.Creator = this.UserID;
            info.ArticleLabel = collection["ArticleLabel"];
            info.ArticlePicture = collection["ArticlePicture"];
            info.ArticleContent = collection["ArticleContent"];
            info.ArticleDescription = collection["ArticleDescription"];
            if (new ET.Sys_BLL.BlogBLL().Operate_BlogArticleInfo(info, IsInsert))
                strResult = "true";
            return Content(strResult);
        }
        [HttpGet]
        public JsonResult AjaxGetArticleDetail(string infoid)
        {
            if (string.IsNullOrEmpty(infoid))
                return Json("", JsonRequestBehavior.AllowGet);
            BlogArticleInfo info = new ET.Sys_BLL.BlogBLL().Get_BlogArticleInfoByID(infoid);
            if (info == null)
                return Json("error", JsonRequestBehavior.AllowGet);
            return Json(info, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult AjaxDeleteArticle(string infoid)
        {
            if (!string.IsNullOrEmpty(infoid) && new ET.Sys_BLL.BlogBLL().Delete_BlogArticleInfo(" AND ARTICLEID in (" + infoid + ")"))
                return Content("true");
            else
                return Content("false");
        }
        [HttpGet]
        public ActionResult AjaxSearchArticle(string query)
        {
            List<BlogArticleInfo> list = new ET.Sys_BLL.BlogBLL().List_BlogArticleInfo("ARTICLETITLE", " AND CHARINDEX('" + query + "', ARTICLETITLE)>0", "CreateTime");
            var arrData = list.Select(c => c.ArticleTitle);
            return Json(new { query = query, suggestions = arrData, data = arrData }, JsonRequestBehavior.AllowGet);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public JsonResult Upload(HttpPostedFileBase fileData)
        {
            if (fileData != null)
            {
                try
                {
                    // 文件上传后的保存路径
                    string filePath = Server.MapPath("~/Upload/blog/image/");
                    if (!Directory.Exists(filePath))
                    {
                        Directory.CreateDirectory(filePath);
                    }
                    string fileName = Path.GetFileName(fileData.FileName);// 原始文件名称
                    string fileExtension = Path.GetExtension(fileName); // 文件扩展名
                    string saveName = DateTime.Now.ToString("yyyyMMddHHmmssfff") + fileExtension; // 保存文件名称

                    fileData.SaveAs(filePath + saveName);

                    return Json(new { Success = true, FileName = fileName, SaveName = "/upload/blog/image/" + saveName });
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
        #endregion

        #region 博客类别操作方法
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
            List<BlogTypeInfo> list = new ET.Sys_BLL.BlogBLL().PageList_BlogTypeInfo("TYPEID,TYPENAME,TYPESORT", Condition, "TYPESORT", pageIndex, pageSize, ref RecordTotalCount);
            return Json(new { total = RecordTotalCount, rows = list }, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public JsonResult AjaxGetPTypeData(string infoid)
        {
            string condition = "";
            if (!string.IsNullOrEmpty(infoid))
                condition = " AND CHARINDEX('" + infoid + "',TYPEID)=0";
            List<KeyAndValue> list = new ET.Sys_BLL.PublicBLL().GetNestListByCondition("TYPEID id,TYPENAME text,TYPEpID pid", ET.Constant.DBConst.TableNames.BlogTypeInfo, condition, "TYPESORT desc");
            list.Insert(0, new KeyAndValue() { id = "-1", text = "根目录" });
            return Json(list, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult AjaxSaveType(FormCollection collection, string infoid)
        {
            bool IsInsert = false;
            string strResult = "false";
            BlogTypeInfo info = new ET.Sys_BLL.BlogBLL().Get_BlogTypeInfoByID(infoid);
            if (info == null)
            {
                IsInsert = true;
                info = new BlogTypeInfo();
            }
            info.TypePID = collection["TypePID"];
            info.TypeName = collection["TypeName"];
            info.TypeSort = collection["TypeSort"];
            info.TypeKey = collection["TypeKey"];
            info.TypeUrl = collection["TypeUrl"];
            info.TypeLevel = collection["TypeLevel"];
            if (!string.IsNullOrEmpty(collection["Status"]) && collection["Status"] == "on")
                info.Status = 1;
            else
                info.Status = 0;

            if (!string.IsNullOrEmpty(collection["IsOnlyNav"]) && collection["IsOnlyNav"] == "on")
                info.IsOnlyNav = true;
            else
                info.IsOnlyNav = false;

            if (new ET.Sys_BLL.BlogBLL().Operate_BlogTypeInfo(info, IsInsert))
                strResult = "true";

            return Content(strResult);
        }
        [HttpGet]
        public JsonResult AjaxGetTypeDetail(string infoid)
        {
            if (string.IsNullOrEmpty(infoid))
                return Json("", JsonRequestBehavior.AllowGet);
            BlogTypeInfo info = new ET.Sys_BLL.BlogBLL().Get_BlogTypeInfoByID(infoid);
            if (info == null)
                return Json("error", JsonRequestBehavior.AllowGet);
            return Json(info, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult AjaxDeleteType(string infoid)
        {
            if (!string.IsNullOrEmpty(infoid) && new ET.Sys_BLL.BlogBLL().Delete_BlogTypeInfo(" AND TYPEID='" + infoid + "'"))
                return Content("true");
            else
                return Content("false");
        }
        [HttpGet]
        public ActionResult AjaxSearchType(string query)
        {
            List<BlogTypeInfo> list = new ET.Sys_BLL.BlogBLL().List_BlogTypeInfo("TYPENAME", " AND  CHARINDEX('" + query + "', TYPENAME)>0", "TYPESORT");
            var arrData = list.Select(c => c.TypeName);
            return Json(new { query = query, suggestions = arrData, data = arrData }, JsonRequestBehavior.AllowGet);
        }
        #endregion
        #endregion


        #region 博客友情链接操作方法
        public ActionResult RollManage()
        {
            return View();
        }
        public ActionResult RollQuery()
        {
            return View();
        }
        #region Ajax处理方法
        [HttpGet]
        public JsonResult AjaxQueryRollPageList()
        {
            //接收datagrid传来的参数 
            int pageIndex = int.Parse(Request["page"]);
            int pageSize = int.Parse(Request["rows"]);
            string Condition = "";
            if (!string.IsNullOrEmpty(Request["name"]))
                Condition = " AND CHARINDEX('" + Request["name"] + "', RollNAME)>0";
            long RecordTotalCount = 0;
            List<BlogRollInfo> list = new ET.Sys_BLL.BlogBLL().PageList_BlogRollInfo("RollID,RollNAME,RollSORT", Condition, "RollSORT", pageIndex, pageSize, ref RecordTotalCount);
            return Json(new { total = RecordTotalCount, rows = list }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult AjaxSaveRoll(FormCollection collection, string infoid)
        {
            bool IsInsert = false;
            string strResult = "false";
            BlogRollInfo info = new ET.Sys_BLL.BlogBLL().Get_BlogRollInfoByID(infoid);
            if (info == null)
            {
                IsInsert = true;
                info = new BlogRollInfo();
            }

            info.RollName = collection["RollName"];
            info.RollSort = collection["RollSort"];

            info.RollUrl = collection["RollUrl"];
            if (!string.IsNullOrEmpty(collection["Status"]) && collection["Status"] == "on")
                info.Status = 1;
            else
                info.Status = 0;



            if (new ET.Sys_BLL.BlogBLL().Operate_BlogRollInfo(info, IsInsert))
                strResult = "true";

            return Content(strResult);
        }
        [HttpGet]
        public JsonResult AjaxGetRollDetail(string infoid)
        {
            if (string.IsNullOrEmpty(infoid))
                return Json("", JsonRequestBehavior.AllowGet);
            BlogRollInfo info = new ET.Sys_BLL.BlogBLL().Get_BlogRollInfoByID(infoid);
            if (info == null)
                return Json("error", JsonRequestBehavior.AllowGet);
            return Json(info, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult AjaxDeleteRoll(string infoid)
        {
            if (!string.IsNullOrEmpty(infoid) && new ET.Sys_BLL.BlogBLL().Delete_BlogRollInfo(" AND RollID='" + infoid + "'"))
                return Content("true");
            else
                return Content("false");
        }
        [HttpGet]
        public ActionResult AjaxSearchRoll(string query)
        {
            List<BlogRollInfo> list = new ET.Sys_BLL.BlogBLL().List_BlogRollInfo("RollNAME", " AND  CHARINDEX('" + query + "', RollNAME)>0", "RollSORT");
            var arrData = list.Select(c => c.RollName);
            return Json(new { query = query, suggestions = arrData, data = arrData }, JsonRequestBehavior.AllowGet);
        }
        #endregion
        #endregion

        #region 留言板
        public ActionResult MessageQuery()
        {
            return View();
        }
            public ActionResult MessageManage()
        {
            return View();
        }
        public JsonResult AjaxQueryMessagePageList()
        {
            //接收datagrid传来的参数 
            int pageIndex = int.Parse(Request["page"]);
            int pageSize = int.Parse(Request["rows"]);
            string Condition = "";
            if (!string.IsNullOrEmpty(Request["name"]))
                Condition = " AND CHARINDEX('" + Request["name"] + "', ARTICLETITLE)>0";
            long RecordTotalCount = 0;
            List<BlogMessageInfo> list = new ET.Sys_BLL.BlogBLL().PageList_BlogMessageInfo("*", Condition, "CreateTime desc", pageIndex, pageSize, ref RecordTotalCount);
            return Json(new { total = RecordTotalCount, rows = list }, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public JsonResult AjaxGetMessageDetail(string infoid)
        {
            if (string.IsNullOrEmpty(infoid))
                return Json("", JsonRequestBehavior.AllowGet);
            BlogMessageInfo info = new ET.Sys_BLL.BlogBLL().Get_BlogMessageInfoByID(infoid);
            if (info == null)
                return Json("error", JsonRequestBehavior.AllowGet);
            return Json(info, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult AjaxSaveMessage(FormCollection collection, string infoid)
        {
            bool IsInsert = false;
            string strResult = "false";
            BlogMessageInfo info = new ET.Sys_BLL.BlogBLL().Get_BlogMessageInfoByID(infoid);
            if (info == null)
            {
                return Content(strResult);
               
            }
            info.ReplyTime = DateTime.Now;
            info.ReplyContent = collection["ReplyContent"];
           
            if (!string.IsNullOrEmpty(collection["Status"]) && collection["Status"] == "on")
                info.Status = 1;
            else
                info.Status = 0;

            if (new ET.Sys_BLL.BlogBLL().Operate_BlogMessageInfo(info, false))
                strResult = "true";
            return Content(strResult);
        }
        public ActionResult AjaxAuditNoPass(string infoid)
        {
            if (!string.IsNullOrEmpty(infoid) && new ET.Sys_BLL.PublicBLL().ExecuteSql("update BlogMessageInfo set status=-1 where MsgID in (" + infoid + ")")>0)
                return Content("true");
            else
                return Content("false");

           
        }
        public ActionResult AjaxAuditPass(string infoid)
        {
            if (!string.IsNullOrEmpty(infoid) && new ET.Sys_BLL.PublicBLL().ExecuteSql("update BlogMessageInfo set status=1 where MsgID in (" + infoid + ")") > 0)
                return Content("true");
            else
                return Content("false");


        }
        #endregion
    }
}
