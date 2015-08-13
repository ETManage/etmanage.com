using ET.Constant.DBConst;
using ET.Sys_DEF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ET.Web.Controllers
{
    public class BlogMobileController : WebControllerBase
    {
        //
        // GET: /BlogMobile/

        public ActionResult Index()
        {
            List<KeyAndValue> listType = new ET.Sys_BLL.PublicBLL().GetNestListByCondition("TYPEID id,TypeUrl reserve1,TypeKey reserve3,TypeLevel reserve2,TYPENAME text,TYPEPID pid", @" (SELECT B.* FROM  BlogTypeInfo A inner JOIN BlogTypeInfo B ON CAST(A.TypeID as varchar(36)) =B.TypePID where a.TypeKey='collect'
                union
                SELECT * FROM  BlogTypeInfo where  TypeKey='collect')a", " AND Status=1  ", "TYPESORT DESC");
            ViewBag.listArticleType = listType;
            List<KeyAndValue> listArticleLabel = new ET.Sys_BLL.PublicBLL().GetListBySql<KeyAndValue>("select  TOP 20 id, count(text) text from ( SELECT  ltrim(rtrim(ArticleLabel)) id,ArticleID  text FROM " + TableNames.BlogArticleInfo + " where len(isnull(ArticleLabel,''))>0 AND Status=1  )A group by id");
            ViewBag.listArticleLabel = listArticleLabel;
            return View();
        }
        public ActionResult Detail(string id)
        {
            if (string.IsNullOrEmpty(id))
                return Redirect("/pageerror/error404.html");
            id = ET.ToolKit.Common.StringHelper.ClearSqlDangerous(id);
            BlogArticleInfo info = new ET.Sys_BLL.BlogBLL().Get_BlogArticleInfoByID(id);
            if (info != null)
            {
                ViewBag.ArticleInfo = info;
                ViewBag.Title = info.ArticleTitle;
                BlogTypeInfo infotype = new ET.Sys_BLL.BlogBLL().Get_BlogTypeInfoByID(info.TypeID.ToString());
                if (infotype != null)
                    ViewBag.ArticleType = infotype;

                List<BlogArticleInfo> listOtherArticle = new ET.Sys_BLL.PublicBLL().GetListByCondition<BlogArticleInfo>(3, "ARTICLEID,ARTICLETITLE,ArticleDescription,ArticleCover,CreateTime,ACCESSCOUNT,LoveCount,ShareCount,ARTICLEUrl,TYPEID", TableNames.BlogArticleInfo, "AND Status=1 AND  TypeID='" + info.TypeID + "'", "CreateTime DESC");
                ViewBag.listOtherArticle = listOtherArticle;

                //收藏按钮处理
                ViewBag.HtmlIsFavorite = "<div class='cancel_share'><a href='javascript:AddFavorite();'>收藏本文</a></div>";
                if (this.IsLogin)
                {
                    BlogArticleFavorite favoriteinfo = new ET.Sys_BLL.BlogBLL().Get_BlogArticleFavorite(string.Format(" AND ArticleID='{0}' AND UserID='{1}'", id, this.UserID));
                    if (favoriteinfo != null)
                        ViewBag.HtmlIsFavorite = "<div class='cancel_share' style='background:#c8c8c8'>已收藏</div>";
                }
                ViewBag.listArticleComment = GetCommentList(info.ArticleID.ToString());
            }
            else
                return Redirect("/pageerror/error404.html");
            return View(info);
        }

        /// <summary>
        /// 返回评论列表ViewBag.listArticleComment
        /// </summary>
        public string GetCommentList(string sArticleID)
        {
            List<KeyAndValue> listType = new ET.Sys_BLL.PublicBLL().GetNestListByCondition("TOP 10 CommentID id,ArticleID reserve1,CreateTime reserve3,Creator reserve2,CommentContent text,CommentPID pid", TableNames.BlogCommentInfo, " AND Status=1 AND ArticleID='" + sArticleID + "'", "CreateTime");
            ViewBag.listArticleCommentCount = listType.Count;
            string str = "";
            GetUserCommentNest(ref str, listType, sArticleID);
            return str;
        }
        void GetUserCommentNest(ref string str, List<ET.Sys_DEF.KeyAndValue> listData, string sArticleID)
        {
            //string tmp = str;
            foreach (ET.Sys_DEF.KeyAndValue item in listData.Where(c => c.pid == "-1"))
            {


                str += @"<li postdata='" + item.id + @"' usdata='" + item.reserve2 + @"'>
                    <div class='pd5'>
                        <a class='avt fl' target='_blank' href='javascript:;'><img src='/content/blogmobile/dhead_1.png' /></a>
                        <div class='comment_content'>
                            <h5>
                                <div class='fl'><a class='comment_name' href='javascript:;'>" + item.reserve2 + @"</a><span><span class='time timeago' title='" + item.reserve3 + @"'>" + item.reserve3 + @"</span></span></div>
                                <div class='fr reply_this'>[回复]</div>
                                <div class='clear'></div>
                            </h5>
                            <div class='comment_p'>
                                <div class='comment_pct'>" + item.text + @"</div>";
                foreach (ET.Sys_DEF.KeyAndValue itemc in item.children)
                {
                    str += @"<div class='quote'><div class='pd10'>" + item.text + "</div></div>";
                }
                str += @"</div>
                        </div>
                        <div class='clear'></div>
                        <div class='comment_reply'></div>
                    </div>
                </li>";

                //GetUserCommentNest(ref str, item.children, sArticleID);
            }

        }

        #region Ajax处理
        /// <summary>
        /// 资讯列表通用方法
        /// </summary>
        /// <param name="Field"></param>
        /// <param name="Condition"></param>
        /// <param name="Order"></param>
        /// <param name="TableName"></param>
        /// <param name="IsNoLock"></param>
        public List<BlogArticleInfo> GetArticleList(int TopCount, string Field, string Condition, string Order, string TableName = TableNames.BlogArticleInfo, bool IsNoLock = true)
        {
            if (string.IsNullOrEmpty(Field))
                Field = "ArticleID,ArticleTitle,ArticleLabel,ArticleDescription,ArticleCover,(select typename from blogtypeinfo c where c.typeid=BlogArticleInfo.typeid ) Reserve1,CreateTime,ISNULL(AccessCount,0),LoveCount,ShareCount,ArticleUrl,TypeID,(select count(1) from BlogCommentInfo where BlogCommentInfo.ArticleID=BlogArticleInfo.ArticleID) Reserve2";
            if (string.IsNullOrEmpty(Order))
                Order = "CreateTime DESC";

            List<BlogArticleInfo> listArticle = new ET.Sys_BLL.PublicBLL().GetListByCondition<BlogArticleInfo>(TopCount, Field, TableName, "AND Status=1 " + Condition, Order);
            return listArticle;
        }
        [HttpPost]
        public ActionResult AjaxPostLike(string timestamp, string token, string uid)
        {
            BlogArticleInfo info = new ET.Sys_BLL.BlogBLL().Get_BlogArticleInfoByID(uid);
            if (info != null)
            {
                info.LoveCount++;
                new ET.Sys_BLL.BlogBLL().Update_BlogArticleInfo(info, false);
                return Content(info.LoveCount.ToString());
            }
            else
                return Content("0");
        }
        [HttpPost]
        public ActionResult AjaxPostRemoveLike(string timestamp, string token, string uid)
        {
            BlogArticleInfo info = new ET.Sys_BLL.BlogBLL().Get_BlogArticleInfoByID(uid);
            if (info != null)
            {
                info.LoveCount--;
                new ET.Sys_BLL.BlogBLL().Update_BlogArticleInfo(info, false);
                return Content(info.LoveCount.ToString());
            }
            else
                return Content("0");
        }

        [HttpPost]
        public ActionResult AjaxPostaddFollow(string timestamp, string token, string uid)
        {
            if (this.IsLogin)
            {
                BlogArticleFavorite info = new BlogArticleFavorite();
                info.ArticleID = new Guid(uid);
                info.UserID = Guid.Parse(this.UserID);
                info.CreateTime = DateTime.Now;
                if (new ET.Sys_BLL.BlogBLL().Update_BlogArticleFavorite(info, true))
                    return Content("true");
                else
                    return Content("error");
            }
            else
                return Content("nologin");
        }
        [HttpPost]
        public ActionResult AjaxPostremoveFollow(string timestamp, string token, string uid)
        {
            if (this.IsLogin)
            {
                if (new ET.Sys_BLL.BlogBLL().Delete_BlogArticleFavorite(string.Format("AND ArticleID='{0}' AND UserID='{1}'", uid, this.UserID)))
                    return Content("true");
                else
                    return Content("error");
            }
            else
                return Content("nologin");
        }
        [HttpGet]
        public JsonResult AjaxSearchList(string fid, string owner, string sort, string search, string page)
        {
            string strCondition = "";
            int pageIndex = 1;
            if (base.IsNumeric(page))
                pageIndex = int.Parse(page);
            int pageSize = 15;
            if (!string.IsNullOrEmpty(fid))
            {
                strCondition += "AND TYPEID='" + fid + "'";
            }
            if (!string.IsNullOrEmpty(search))
            {
                strCondition += "AND CHARINDEX('" + search + "',ARTICLETITLE)>0";
            }
            long lngRecordTotalCount = 0;
            List<BlogArticleInfo> list = new ET.Sys_BLL.BlogBLL().Pagination_BlogArticleInfo("ArticleID,ArticleTitle,ArticleLabel,ArticleDescription,ArticleCover,(select typename from blogtypeinfo c where c.typeid=BlogArticleInfo.typeid ) Reserve1,CreateTime,ISNULL(AccessCount,0) AccessCount,LoveCount,ShareCount,ArticleUrl,TypeID,(select count(1) from BlogCommentInfo where BlogCommentInfo.ArticleID=BlogArticleInfo.ArticleID) Reserve2", strCondition, sort, pageIndex, pageSize, ref lngRecordTotalCount);
            return Json(list, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult ajaxpostnewComment(string timestamp, string token, string postid, string postcid, string postuid, string content)
        {
            string ss = Request.Url.ToString();
            BlogCommentInfo info = new BlogCommentInfo();
            info.ArticleID = new Guid(postid);
            info.CommentContent = content;
            info.Creator = postuid;
            if (string.IsNullOrEmpty(postcid))
                postcid = "-1";
            info.CommentPID = postcid;
            info.CreateTime = DateTime.Now;
            new ET.Sys_BLL.BlogBLL().Update_BlogCommentInfo(info, true);
            return Json(info, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult ajaxsendSMS(string timestamp, string token, string nickname, string content)
        {
            return Content("true");
        }
        [HttpGet]
        public JsonResult Ajaxgetsms(string id)
        {
            return Json("true", JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult ajaxdelMessage(string timestamp, string token, string id)
        {
            return Content("true");
        }

        [HttpGet]
        public JsonResult Ajaxdesigners(string page, string sort)
        {
            return Json("true", JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult Ajaxgetcomment(string timestamp, string token, string page, string postid)
        {
            string strCondition = " AND Status=1 AND ArticleID='" + postid + "'";
            int pageIndex = 1;
            if (base.IsNumeric(page))
                pageIndex = Convert.ToInt32(page);
            int pageSize = 15;
            long lngRecordTotalCount = 0;
            List<KeyAndValue> list = new ET.Sys_BLL.PublicBLL().GetNestPaginationByCondition("CommentID id,ArticleID reserve1,CreateTime reserve3,Creator reserve2,CommentContent text,CommentPID pid", TableNames.BlogCommentInfo, strCondition, "CreateTime DESC", pageIndex, pageSize, ref lngRecordTotalCount);
            return Json("true", JsonRequestBehavior.AllowGet);
        }
     
        [HttpPost]
        public ActionResult ajaxeditUserCat(string timestamp, string token, string id, string title)
        {
            return Content("true");
        }
        [HttpPost]
        public ActionResult ajaxremoveUserCat(string timestamp, string token, string id)
        {
            return Content("true");
        }
        [HttpPost]
        public ActionResult ajaxaddUserCat(string timestamp, string token, string id)
        {
            return Content("true");
        }

        #endregion
    }
}
