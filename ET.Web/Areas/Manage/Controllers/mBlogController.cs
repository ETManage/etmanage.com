using ET.Constant.DBConst;
using ET.Sys_DEF;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ET.Web.Areas.Manage.Controllers
{
    [UserAuthorize]
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
            List<BlogArticleInfo> list = new ET.Sys_BLL.BlogBLL().Pagination_BlogArticleInfo("ARTICLEID,ARTICLETITLE,ArticleLabel,ArticleUrl,LoveCount,ShareCount,Status,CreateTime,ACCESSCOUNT,TYPEID,(SELECT TYPENAME FROM BlogTypeInfo WHERE TYPEID=BlogArticleInfo.TYPEID)Reserve1", Condition, "CreateTime desc", pageIndex, pageSize, ref RecordTotalCount);
            return Json(new { total = RecordTotalCount, rows = list }, JsonRequestBehavior.AllowGet);
        }
        public JsonResult AjaxGetTypeSelectData()
        {
            List<KeyAndValue> list = new ET.Sys_BLL.PublicBLL().GetListByCondition<KeyAndValue>("TYPEID id,TYPENAME text", ET.Constant.DBConst.TableNames.BlogTypeInfo, " AND ISNULL(IsOnlyNav,0)=0 AND STATUS=1", "TYPESORT");
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

            info.ArticleSource = collection["SpecMark"];
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
            if (!string.IsNullOrEmpty(collection["IsOutLink"]) && collection["IsOutLink"] == "on")
                info.IsOutLink = true;
            else
                info.IsOutLink = false;
            info.Creator = this.UserID;
            info.ArticleLabel = collection["ArticleLabel"];
            info.ArticleCover = collection["ArticleCover"];
            info.ArticleContent = collection["ArticleContent"];
            info.ArticleDescription = collection["ArticleDescription"];
            if (new ET.Sys_BLL.BlogBLL().Update_BlogArticleInfo(info, IsInsert))
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
        [HttpPost]
        public ActionResult AjaxDisableArticle(string ids)
        {
            if (!string.IsNullOrEmpty(ids) && new ET.Sys_BLL.BlogBLL().Update_DisableArticle(" AND ArticleID IN (" + ids + ")"))
                return Content("true");
            else
                return Content("false");
        }
        [HttpPost]
        public ActionResult AjaxEnabledArticle(string ids)
        {
            if (!string.IsNullOrEmpty(ids) && new ET.Sys_BLL.BlogBLL().Update_EnabledArticle(" AND ArticleID IN (" + ids + ")"))
                return Content("true");
            else
                return Content("false");
        }

        [HttpGet]
        public ActionResult AjaxSearchArticle(string query)
        {
            List<BlogArticleInfo> list = new ET.Sys_BLL.BlogBLL().List_BlogArticleInfo("ARTICLETITLE", " AND CHARINDEX('" + query + "', ARTICLETITLE)>0", "CreateTime DESC");
            var arrData = list.Select(c => c.ArticleTitle);
            return Json(new { query = query, suggestions = arrData, data = arrData }, JsonRequestBehavior.AllowGet);
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
            List<BlogTypeInfo> list = new ET.Sys_BLL.BlogBLL().Pagination_BlogTypeInfo("TYPEID,TYPENAME,TYPESORT", Condition, "TYPESORT", pageIndex, pageSize, ref RecordTotalCount);
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

            if (new ET.Sys_BLL.BlogBLL().Update_BlogTypeInfo(info, IsInsert))
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
            if (!string.IsNullOrEmpty(infoid) && new ET.Sys_BLL.BlogBLL().Delete_BlogTypeInfo(" AND TYPEID  in (" + infoid + ")"))
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


        #region 博客友情链接
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
            List<BlogRollInfo> list = new ET.Sys_BLL.BlogBLL().Pagination_BlogRollInfo("RollID,RollNAME,RollSORT", Condition, "RollSORT", pageIndex, pageSize, ref RecordTotalCount);
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
                info.CreateTime = DateTime.Now;
            }

            info.RollName = collection["RollName"];
            info.RollSort = collection["RollSort"];

            info.RollUrl = collection["RollUrl"];
            if (!string.IsNullOrEmpty(collection["Status"]) && collection["Status"] == "on")
                info.Status = 1;
            else
                info.Status = 0;



            if (new ET.Sys_BLL.BlogBLL().Update_BlogRollInfo(info, IsInsert))
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
            if (!string.IsNullOrEmpty(infoid) && new ET.Sys_BLL.BlogBLL().Delete_BlogRollInfo(" AND RollID in (" + infoid + ")"))
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
                Condition = " AND CHARINDEX('" + Request["name"] + "', MsgTitle)>0";
            long RecordTotalCount = 0;
            List<BlogMessageInfo> list = new ET.Sys_BLL.BlogBLL().Pagination_BlogMessageInfo("*", Condition, "CreateTime desc", pageIndex, pageSize, ref RecordTotalCount);
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

            if (new ET.Sys_BLL.BlogBLL().Update_BlogMessageInfo(info, false))
                strResult = "true";
            return Content(strResult);
        }
        public ActionResult AjaxAuditNoPass(string infoid)
        {
            if (!string.IsNullOrEmpty(infoid) && new ET.Sys_BLL.PublicBLL().ExecuteSql("update BlogMessageInfo set status=-1 where MsgID in (" + infoid + ")") > 0)
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
        [HttpPost]
        public ActionResult AjaxDeleteMessage(string infoid)
        {
            if (!string.IsNullOrEmpty(infoid) && new ET.Sys_BLL.BlogBLL().Delete_BlogMessageInfo(" AND MsgID in (" + infoid + ")"))
                return Content("true");
            else
                return Content("false");
        }
        #endregion



        #region 投稿方法
        public ActionResult PublishManage()
        {
            return View();
        }
        public ActionResult PublishQuery()
        {
            return View();
        }
        #region Ajax处理方法
        [HttpGet]
        public JsonResult AjaxQueryPublishPageList()
        {
            //接收datagrid传来的参数 
            int pageIndex = int.Parse(Request["page"]);
            int pageSize = int.Parse(Request["rows"]);
            string Condition = "";
            if (!string.IsNullOrEmpty(Request["name"]))
                Condition = " AND CHARINDEX('" + Request["name"] + "', Title)>0";
            long RecordTotalCount = 0;
            List<BlogPublish> list = new ET.Sys_BLL.BlogBLL().Pagination_BlogPublish("PublishID,Title,PublishSource,PublishType,Status,CreateTime", Condition, "CreateTime desc", pageIndex, pageSize, ref RecordTotalCount);
            return Json(new { total = RecordTotalCount, rows = list }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult AjaxSavePublish(FormCollection collection, string infoid)
        {
            bool IsInsert = false;
            string strResult = "false";
            BlogPublish info = new ET.Sys_BLL.BlogBLL().Get_BlogPublishByID(infoid);
            if (info == null)
            {
                IsInsert = true;
                info = new BlogPublish();
            }


            info.Creator = this.UserID;
            info.PublishType = collection["PublishType"];
            info.Title = collection["Title"];
            info.PublishContent = collection["PublishContent"];
            info.Description = collection["Description"];
            info.PublishSource = collection["PublishSource"];
            info.Label = collection["Label"];
            info.Cover = collection["Cover"];
            info.TypeID = Guid.Parse(collection["TypeID"]);
            info.CreateTime = DateTime.Now;


            if (!string.IsNullOrEmpty(collection["Status"]) && collection["Status"] == "on")
            {
                info.Status = 1;
                BlogArticleInfo arinfo = new BlogArticleInfo();
                arinfo.ArticleID = Guid.NewGuid();
                arinfo.CreateTime = DateTime.Now;

                arinfo.ArticleSource = info.PublishSource;
                arinfo.TypeID = info.TypeID;
                arinfo.ArticleTitle = info.Title;
                arinfo.ArticleAuthor = info.Author;
                //if (string.IsNullOrEmpty(info.URL))
                //{
                arinfo.ArticleUrl = "/blog/detail/" + arinfo.ArticleID;
                //}
                //else
                //arinfo.ArticleUrl = collection["ArticleUrl"];
                arinfo.Status = 1;


                arinfo.Creator = info.Creator;
                arinfo.ArticleLabel = info.Label;
                arinfo.ArticleCover = info.Cover;
                arinfo.ArticleContent = info.PublishContent;
                arinfo.ArticleDescription = info.Description;
                new ET.Sys_BLL.BlogBLL().Update_BlogArticleInfo(arinfo, true);
            }
            else
                info.Status = 0;



            if (new ET.Sys_BLL.BlogBLL().Update_BlogPublish(info, IsInsert))
                strResult = "true";

            return Content(strResult);
        }
        [HttpGet]
        public JsonResult AjaxGetPublishDetail(string infoid)
        {
            if (string.IsNullOrEmpty(infoid))
                return Json("", JsonRequestBehavior.AllowGet);
            BlogPublish info = new ET.Sys_BLL.BlogBLL().Get_BlogPublishByID(infoid);
            if (info == null)
                return Json("error", JsonRequestBehavior.AllowGet);
            return Json(info, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult AjaxPassPublish(string infoid)
        {
            if (string.IsNullOrEmpty(infoid))
                return Json("", JsonRequestBehavior.AllowGet);
            foreach (string item in infoid.Split(new[] { "," }, StringSplitOptions.RemoveEmptyEntries))
            {
                BlogPublish info = new ET.Sys_BLL.BlogBLL().Get_BlogPublishByID(infoid);
                if (info != null)
                {
                    if (info.Status == 1)
                        continue;

                    BlogArticleInfo arinfo = new BlogArticleInfo();
                    arinfo.ArticleID = Guid.NewGuid();
                    arinfo.CreateTime = DateTime.Now;

                    arinfo.ArticleSource = info.PublishSource;
                    arinfo.TypeID = info.TypeID;
                    arinfo.ArticleTitle = info.Title;
                    arinfo.ArticleAuthor = info.Author;
                    //if (string.IsNullOrEmpty(info.URL))
                    //{
                    arinfo.ArticleUrl = "/blog/detail/" + arinfo.ArticleID;
                    //}
                    //else
                    //arinfo.ArticleUrl = collection["ArticleUrl"];
                    arinfo.Status = 1;


                    arinfo.Creator = info.Creator;
                    arinfo.ArticleLabel = info.Label;
                    arinfo.ArticleCover = info.Cover;
                    arinfo.ArticleContent = info.PublishContent;
                    arinfo.ArticleDescription = info.Description;
                    new ET.Sys_BLL.BlogBLL().Update_BlogArticleInfo(arinfo, true);



                    info.Status = 1;
                    new ET.Sys_BLL.BlogBLL().Update_BlogPublish(info, false);
                }
            }

            return Json("true", JsonRequestBehavior.AllowGet);

        }

        [HttpPost]
        public ActionResult AjaxDeletePublish(string infoid)
        {
            if (!string.IsNullOrEmpty(infoid) && new ET.Sys_BLL.BlogBLL().Delete_BlogPublish(" AND PublishID in (" + infoid + ")"))
                return Content("true");
            else
                return Content("false");
        }
        [HttpGet]
        public ActionResult AjaxSearchPublish(string query)
        {
            List<BlogPublish> list = new ET.Sys_BLL.BlogBLL().List_BlogPublish("Title", " AND  CHARINDEX('" + query + "', Title)>0", "CreateTime");
            var arrData = list.Select(c => c.Title);
            return Json(new { query = query, suggestions = arrData, data = arrData }, JsonRequestBehavior.AllowGet);
        }
        #endregion
        #endregion



        #region 博客用户等级
        public ActionResult UserLevelManage()
        {
            return View();
        }
        public ActionResult UserLevelQuery()
        {
            return View();
        }


        #region Ajax处理方法
        [HttpGet]
        public JsonResult AjaxQueryLevelPageList()
        {
            //接收datagrid传来的参数 
            int pageIndex = int.Parse(Request["page"]);
            int pageSize = int.Parse(Request["rows"]);
            string Condition = "";
            if (!string.IsNullOrEmpty(Request["name"]))
                Condition = " AND CHARINDEX('" + Request["name"] + "', LevelNAME)>0";
            long RecordTotalCount = 0;
            List<BlogUserLevel> list = new ET.Sys_BLL.BlogBLL().Pagination_BlogUserLevel("LevelID,LevelNAME,NeedExp", Condition, "NeedExp", pageIndex, pageSize, ref RecordTotalCount);
            return Json(new { total = RecordTotalCount, rows = list }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult AjaxSaveLevel(FormCollection collection, string infoid)
        {
            bool IsInsert = false;
            string strResult = "false";
            BlogUserLevel info = new ET.Sys_BLL.BlogBLL().Get_BlogUserLevelByID(infoid);
            if (info == null)
            {
                IsInsert = true;
                info = new BlogUserLevel();
            }

            info.LevelName = collection["LevelName"];
            info.NeedExp = Convert.ToInt64(collection["NeedExp"]);




            if (new ET.Sys_BLL.BlogBLL().Update_BlogUserLevel(info, IsInsert))
                strResult = "true";

            return Content(strResult);
        }
        [HttpGet]
        public JsonResult AjaxGetLevelDetail(string infoid)
        {
            if (string.IsNullOrEmpty(infoid))
                return Json("", JsonRequestBehavior.AllowGet);
            BlogUserLevel info = new ET.Sys_BLL.BlogBLL().Get_BlogUserLevelByID(infoid);
            if (info == null)
                return Json("error", JsonRequestBehavior.AllowGet);
            return Json(info, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult AjaxDeleteLevel(string infoid)
        {
            if (!string.IsNullOrEmpty(infoid) && new ET.Sys_BLL.BlogBLL().Delete_BlogUserLevel(" AND LevelID in ('" + infoid + ")"))
                return Content("true");
            else
                return Content("false");
        }
        [HttpGet]
        public ActionResult AjaxSearchLevel(string query)
        {
            List<BlogUserLevel> list = new ET.Sys_BLL.BlogBLL().List_BlogUserLevel("LevelNAME", " AND  CHARINDEX('" + query + "', LevelNAME)>0", "NeedExp");
            var arrData = list.Select(c => c.LevelName);
            return Json(new { query = query, suggestions = arrData, data = arrData }, JsonRequestBehavior.AllowGet);
        }
        #endregion
        #endregion


        #region 博客用户签到

        public ActionResult UserSignQuery()
        {
            return View();
        }

        #region Ajax处理方法
        [HttpGet]
        public JsonResult AjaxQuerySignPageList()
        {
            //接收datagrid传来的参数 
            int pageIndex = int.Parse(Request["page"]);
            int pageSize = int.Parse(Request["rows"]);
            string Condition = "";
            if (!string.IsNullOrEmpty(Request["name"]))
                Condition = " AND CHARINDEX('" + Request["name"] + "', CNNAME)>0";
            long RecordTotalCount = 0;
            List<BlogUserSignIn> list = new ET.Sys_BLL.PublicBLL().GetListByPager<BlogUserSignIn>("SignID,UserID,CreateTime,cnname Reserve1,cast(Exp as varchar(20))+'['+UserLevel+']' Reserve2", string.Format("(SELECT A.*,B.CNNAME,B.EXP,B.USERLEVEL FROM {0} B INNER JOIN {1} A ON A.USERID=B.USERID)a", ViewNames.V_USERFULL, TableNames.BlogUserSignIn), Condition, "CreateTime desc", pageIndex, pageSize, ref RecordTotalCount);
            return Json(new { total = RecordTotalCount, rows = list }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult AjaxDeleteSign(string infoid)
        {
            if (!string.IsNullOrEmpty(infoid) && new ET.Sys_BLL.BlogBLL().Delete_BlogUserSignIn(" AND SignID in (" + infoid + ")"))
                return Content("true");
            else
                return Content("false");
        }

        #endregion
        #endregion

        #region 博客用户预览

        public ActionResult UserViewQuery()
        {
            return View();
        }

        #region Ajax处理方法
        [HttpGet]
        public JsonResult AjaxQueryViewPageList()
        {
            //接收datagrid传来的参数 
            int pageIndex = int.Parse(Request["page"]);
            int pageSize = int.Parse(Request["rows"]);
            string Condition = "";
            if (!string.IsNullOrEmpty(Request["name"]))
                Condition = " AND CHARINDEX('" + Request["name"] + "', CNNAME)>0";
            long RecordTotalCount = 0;
            List<BlogViewRecord> list = new ET.Sys_BLL.PublicBLL().GetListByPager<BlogViewRecord>("ViewID,UserID,CreateTime,InfoType,(select top 1 ArticleTitle from BlogArticleInfo art where art.ArticleID=tmp.InfoID) Reserve2,cnname Reserve1", string.Format("(SELECT A.*,B.CNNAME FROM {0} B INNER JOIN {1} A ON A.USERID=B.USERID)tmp", TableNames.UserProperty, TableNames.BlogViewRecord), Condition, "CreateTime desc", pageIndex, pageSize, ref RecordTotalCount);
            return Json(new { total = RecordTotalCount, rows = list }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult AjaxDeleteView(string infoid)
        {
            if (!string.IsNullOrEmpty(infoid) && new ET.Sys_BLL.BlogBLL().Delete_BlogViewRecord(" AND ViewID in (" + infoid + ")"))
                return Content("true");
            else
                return Content("false");
        }

        #endregion
        #endregion


        #region 博客友情链接
        public ActionResult UserRequestManage()
        {
            return View();
        }
        public ActionResult UserRequestQuery()
        {
            return View();
        }
        #region Ajax处理方法
        [HttpGet]
        public JsonResult AjaxQueryRequestPageList()
        {
            //接收datagrid传来的参数 
            int pageIndex = int.Parse(Request["page"]);
            int pageSize = int.Parse(Request["rows"]);
            string Condition = "";
            if (!string.IsNullOrEmpty(Request["name"]))
                Condition = " AND CHARINDEX('" + Request["name"] + "', RequestNAME)>0";
            long RecordTotalCount = 0;
            List<BlogUserRequest> list = new ET.Sys_BLL.BlogBLL().Pagination_BlogUserRequest("*", Condition, "CreateTime desc", pageIndex, pageSize, ref RecordTotalCount);
            return Json(new { total = RecordTotalCount, rows = list }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult AjaxSaveRequest(FormCollection collection, string infoid)
        {
            string strResult = "false";
            BlogUserRequest info = new ET.Sys_BLL.BlogBLL().Get_BlogUserRequestByID(infoid);
            if (info != null)
            {
                info.AuditeTime = DateTime.Now;
                info.AuditeResult = collection["AuditeResult"];

                info.Auditor = Guid.Parse(this.UserID);

                info.Status = Convert.ToInt32(collection["Status"]);




                if (new ET.Sys_BLL.BlogBLL().Update_BlogUserRequest(info, false))
                    strResult = "true";
            }
            return Content(strResult);
        }
        [HttpGet]
        public JsonResult AjaxGetRequestDetail(string infoid)
        {
            if (string.IsNullOrEmpty(infoid))
                return Json("", JsonRequestBehavior.AllowGet);
            BlogUserRequest info = new ET.Sys_BLL.BlogBLL().Get_BlogUserRequestByID(infoid);
            if (info == null)
                return Json("error", JsonRequestBehavior.AllowGet);
            return Json(info, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult AjaxDeleteRequest(string infoid)
        {
            if (!string.IsNullOrEmpty(infoid) && new ET.Sys_BLL.BlogBLL().Delete_BlogUserRequest(" AND RequestID in (" + infoid + ")"))
                return Content("true");
            else
                return Content("false");
        }
        [HttpGet]
        public ActionResult AjaxSearchRequest(string query)
        {
            List<BlogUserRequest> list = new ET.Sys_BLL.BlogBLL().List_BlogUserRequest("RequestNAME", " AND  CHARINDEX('" + query + "', RequestNAME)>0", "RequestSORT");
            var arrData = list.Select(c => c.RequestName);
            return Json(new { query = query, suggestions = arrData, data = arrData }, JsonRequestBehavior.AllowGet);
        }
        #endregion
        #endregion

    }
}
