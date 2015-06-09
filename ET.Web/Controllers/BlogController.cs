using ET.Constant.DBConst;
using ET.Sys_DEF;
using ET.ToolKit.Common;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Security;
using System.Xml;

namespace ET.Web.Controllers
{
    public class BlogController : WebControllerBase
    {
        #region 分布视图

        /// <summary>
        /// 资讯列表，返回ViewBag.listArticle
        /// </summary>
        /// <param name="Field"></param>
        /// <param name="Condition"></param>
        /// <param name="Order"></param>
        /// <param name="TableName"></param>
        /// <param name="IsNoLock"></param>
        public void _GetPartialArticleList(string Field, string Condition, string Order, string TableName = TableNames.BlogArticleInfo, bool IsNoLock = true)
        {
            if (string.IsNullOrEmpty(Field))
                Field = "ArticleID,ArticleTitle,ArticleLabel,ArticleDescription,ArticlePicture,CreateTime,AccessCount,LoveCount,ShareCount,ArticleUrl,TypeID,(select count(1) from BlogCommentInfo where BlogCommentInfo.ArticleID=BlogArticleInfo.ArticleID) ArticleSource";
            if (string.IsNullOrEmpty(Order))
                Order = "CreateTime DESC";
            List<BlogArticleInfo> listArticle = new ET.Sys_BLL.PublicBLL().GetListByCondition<BlogArticleInfo>(10, Field, TableName, "AND Status=1 " + Condition, Order, IsNoLock);


            ViewBag.listArticle = GetArticleList(10, Field, Condition, Order, TableName, IsNoLock);
        }


        /// <summary>
        /// 分页查询，返回ViewBag.listPager
        /// </summary>
        public void _GetListPager(int pageIndex, int pageSize, long RecordTotalCount)
        {

            string strPagerText = "<div class=' pagination'><ul>";

            string strCurrUrl = Request.Path.ToString() + "?";

            if (pageIndex == 1)
            {
                strPagerText += "<li class='prev-page'></li>";
            }
            else
                strPagerText += "<li class='prev-page'><a href='" + strCurrUrl + "page=" + (pageIndex - 1) + "' >上一页</a></li>";

            for (int i = ((pageIndex - 3) > 0 ? (pageIndex - 3) : 1); i < pageIndex; i++)
            {
                strPagerText += "<li><a href='" + strCurrUrl + "page=" + i + "' >" + i + "</a></li>";
            }
            strPagerText += "<li class='active'><span>" + pageIndex + "</span></li>";


            for (int i = pageIndex + 1; i <= Math.Ceiling((decimal)RecordTotalCount / pageSize); i++)
            {
                strPagerText += "<li><a href='" + strCurrUrl + "page=" + i + "' >" + i + "</a></li>";
            }
            if (pageIndex == Math.Ceiling((decimal)RecordTotalCount / pageSize))
            {
                strPagerText += "<li class='next-page'></li>";
            }
            else
                strPagerText += "<li class='next-page'><a href='" + strCurrUrl + "page=" + (pageIndex + 1) + "' >下一页</a></li>";

            strPagerText += "</ul>        </div>";
            ViewBag.listPager = strPagerText;
        }



        #endregion


        #region 页面公用方法
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
                Field = "ArticleID,ArticleTitle,ArticleLabel,ArticleDescription,ArticlePicture,CreateTime,AccessCount,LoveCount,ShareCount,ArticleUrl,TypeID,(select count(1) from BlogCommentInfo where BlogCommentInfo.ArticleID=BlogArticleInfo.ArticleID) ArticleSource";
            if (string.IsNullOrEmpty(Order))
                Order = "CreateTime DESC";

            List<BlogArticleInfo> listArticle = new ET.Sys_BLL.PublicBLL().GetListByCondition<BlogArticleInfo>(TopCount, Field, TableName, "AND Status=1 " + Condition, Order, IsNoLock);
            return listArticle;
        }


        #endregion
        #region 页面控制器
        public ActionResult Index()
        {
            ViewBag.listTopArticle = GetArticleList(5, "ArticleID,ArticleTitle,ArticleLabel,ArticleDescription,case when CHARINDEX(',',ArticlePicture)>0 then ArticlePicture else ',' end ArticlePicture,CreateTime,AccessCount,LoveCount,ShareCount,ArticleUrl,TypeID,(select count(1) from BlogCommentInfo where BlogCommentInfo.ArticleID=BlogArticleInfo.ArticleID) ArticleSource", " AND IsRoll=1 ", "CreateTime DESC"); ;
            _GetPartialArticleList(null, null, null);
            return View();
        }
        public ActionResult Detail(string id)
        {

            if (string.IsNullOrEmpty(id))
                return Json("", JsonRequestBehavior.AllowGet);
            id = StringHelper.ClearSqlDangerous(id);
            BlogArticleInfo info = new ET.Sys_BLL.BlogBLL().Get_BlogArticleInfoByID(id);
            if (info != null)
            {
                ViewBag.ArticleInfo = info;
                ViewBag.Title = info.ArticleTitle;
                BlogTypeInfo infotype = new ET.Sys_BLL.BlogBLL().Get_BlogTypeInfoByID(info.TypeID.ToString());
                if (infotype != null)
                    ViewBag.ArticleType = infotype;
                //List<BlogCommentInfo> listArticleComment = new ET.Sys_BLL.BlogBLL().List_BlogCommentInfo("*", "AND ArticleID='" + info.ArticleID + "'", "CreateTime desc");
                //ViewBag.listArticleComment = listArticleComment;


                List<BlogArticleInfo> listLastArticle = new ET.Sys_BLL.PublicBLL().GetListByCondition<BlogArticleInfo>(1, " ARTICLEUrl,ARTICLETITLE", TableNames.BlogArticleInfo, "AND Status=1 AND CreateTime>'" + info.CreateTime + "'", "CreateTime DESC");
                if (listLastArticle.Count == 0)
                    listLastArticle.Add(new BlogArticleInfo { ArticleUrl = "javascript:;", ArticleTitle = "第一条了！" });
                ViewBag.listLastArticle = listLastArticle;
                List<BlogArticleInfo> listNextArticle = new ET.Sys_BLL.PublicBLL().GetListByCondition<BlogArticleInfo>(1, " ARTICLEUrl,ARTICLETITLE", TableNames.BlogArticleInfo, "AND Status=1 AND CreateTime<'" + info.CreateTime + "'", "CreateTime DESC");
                if (listNextArticle.Count == 0)
                    listNextArticle.Add(new BlogArticleInfo { ArticleUrl = "javascript:;", ArticleTitle = "最后一条了！" });
                ViewBag.listNextArticle = listNextArticle;

                List<BlogArticleInfo> listArticle = new ET.Sys_BLL.PublicBLL().GetListByCondition<BlogArticleInfo>(4, "ARTICLEID,ARTICLETITLE,ArticleDescription,ArticlePicture,CreateTime,ACCESSCOUNT,LoveCount,ShareCount,ARTICLEUrl,TYPEID", TableNames.BlogArticleInfo, "AND Status=1 ", "CreateTime DESC");
                ViewBag.listArticle = listArticle;


                List<BlogArticleInfo> listOtherArticle = new ET.Sys_BLL.PublicBLL().GetListByCondition<BlogArticleInfo>(8, "ARTICLEID,ARTICLETITLE,ArticleDescription,ArticlePicture,CreateTime,ACCESSCOUNT,LoveCount,ShareCount,ARTICLEUrl,TYPEID", TableNames.BlogArticleInfo, "AND Status=1 AND  charindex('" + info.ArticleLabel + "',ArticleLabel)>0", "CreateTime DESC");
                ViewBag.listOtherArticle = listOtherArticle.Take(4);
                ViewBag.listOtherArticle2 = listOtherArticle.Skip(4);

                //收藏按钮处理
                if (this.IsLogin)
                {
                    BlogArticleFavoriteInfo favoriteinfo = new ET.Sys_BLL.BlogBLL().Get_BlogArticleFavoriteInfo(string.Format(" AND ArticleID='{0}' AND UserID='{1}'", id, this.UserID));
                    if (favoriteinfo != null)
                        ViewBag.scriptIsFavorite = "$('#iAddFavorite').html('已收藏');$('#iAddFavorite').addClass('cYellow');";
                }

                GetCommentList(info.ArticleID.ToString());
            }
            else
                return Redirect("/PageError/error404.html");
            return View();
        }

        public ActionResult Original(string id)
        {
            string strTypeKey = "original";
            string strTitle = "原创中心";
            if (!string.IsNullOrEmpty(id))
            {
                strTypeKey = StringHelper.ClearSqlDangerous(id);
                BlogTypeInfo typeinfo = new ET.Sys_BLL.BlogBLL().Get_BlogTypeInfoByCondition(" AND TYPEKEY='" + strTypeKey + "'");
                if (typeinfo != null)
                {
                    strTitle = typeinfo.TypeName;
                }
            }
            ViewBag.InfoBlogTypeName = strTitle;
            _GetPartialArticleList(null, null, null, string.Format(@"(SELECT A.* FROM  BlogArticleInfo A INNER JOIN 
                (SELECT B.* FROM  BlogTypeInfo A inner JOIN BlogTypeInfo B ON CAST(A.TypeID as varchar(36)) =B.TypePID where a.TypeKey='{0}'
                union
                SELECT * FROM  BlogTypeInfo where  TypeKey='{0}')B ON A.TypeID=B.TypeID)BlogArticleInfo", strTypeKey), false);

            return View();
        }
        public ActionResult Collect(string id)
        {
            string strTypeKey = "collect";
            string strTitle = "资讯中心";
            if (!string.IsNullOrEmpty(id))
            {
                strTypeKey = StringHelper.ClearSqlDangerous(id);
                BlogTypeInfo typeinfo = new ET.Sys_BLL.BlogBLL().Get_BlogTypeInfoByCondition(" AND TYPEKEY='" + strTypeKey + "'");
                if (typeinfo != null)
                {
                    strTitle = typeinfo.TypeName;
                }
            }
            ViewBag.InfoBlogTypeName = strTitle;
            _GetPartialArticleList(null, null, null, string.Format(@"(SELECT A.* FROM  BlogArticleInfo A INNER JOIN 
                (SELECT B.* FROM  BlogTypeInfo A inner JOIN BlogTypeInfo B ON CAST(A.TypeID as varchar(36)) =B.TypePID where a.TypeKey='{0}'
                union
                SELECT * FROM  BlogTypeInfo where  TypeKey='{0}')B ON A.TypeID=B.TypeID)BlogArticleInfo", strTypeKey), false);
            return View();
        }
        public ActionResult HotCollect()
        {
            List<BlogArticleInfo> listOtherArticle = new ET.Sys_BLL.PublicBLL().GetListByCondition<BlogArticleInfo>(8, "ARTICLEID,ARTICLETITLE,ArticleDescription,ArticlePicture,CreateTime,ACCESSCOUNT,LoveCount,ShareCount,ARTICLEUrl,TYPEID", TableNames.BlogArticleInfo, "AND Status=1 ", "ACCESSCOUNT DESC,CreateTime DESC");
            ViewBag.listOtherArticle = listOtherArticle;

            _GetPartialArticleList(null, null, "ACCESSCOUNT DESC,CreateTime DESC");
            return View();
        }

        public ActionResult ArticleTag(string tag)
        {
            string strCondition = " AND ArticleLabel='" + StringHelper.ClearSqlDangerous(tag) + "'";
            _GetPartialArticleList(null, strCondition, null);
            return View();
        }

        public ActionResult PageOk()
        {
            return View();
        }
        public ActionResult UserCenter(string page)
        {
            int pageIndex = 1;
            if (this.CheckInfoInt(page))
                pageIndex = int.Parse(page);
            int pageSize = 10;
            long RecordTotalCount = 0;
            //收藏中心-文章收藏 Start
            List<BlogArticleInfo> listCollectArticle = new ET.Sys_BLL.PublicBLL().GetListByConditionPager<BlogArticleInfo>("ArticleID,ArticleTitle,ArticleLabel,ArticleDescription,ArticlePicture,CreateTime,AccessCount,LoveCount,ShareCount,ArticleUrl,TypeID,(select count(1) from BlogCommentInfo where BlogCommentInfo.ArticleID=td.ArticleID) ArticleSource", string.Format("(select a.*,b.CreateTime FavoriteTime from BlogArticleInfo a inner join BlogArticleFavoriteInfo b on a.ARTICLEID=b.ARTICLEID where  a.Status=1 )td"), "AND Status=1 AND ARTICLEID IN (SELECT ARTICLEID FROM " + TableNames.BlogArticleFavoriteInfo + " WHERE UserID='" + this.UserID + "')", "FavoriteTime desc", pageIndex, pageSize, ref RecordTotalCount, false);
            ViewBag.listCollectArticle = listCollectArticle;

            //收藏中心-文章收藏 End
            //查看记录 Start
            //List<KeyAndValue> listBlogViewRecord = new ET.Sys_BLL.PublicBLL().GetListByCondition<KeyAndValue>("")

            _GetListPager(pageIndex, pageSize, RecordTotalCount);

            return View();
        }
        public ActionResult UserSetting()
        {
            if (this.IsLogin)
            {
                this.JavaScript("alert('用户还未登录！或已经登录超时！请重新登录')");
            }
            UserPropertyInfo uinfo = new ET.Sys_BLL.OrganizationBLL().Get_UserPropertyInfoByID(this.UserID.ToString());
            if (uinfo != null)
            {
                ViewBag.UserInfo = uinfo;
            }
            return View();
        }
        [HttpPost]
        public ActionResult GetUserBaseSetting()
        {
            if (this.IsLogin)
            {
                this.JavaScript("alert('用户还未登录！或已经登录超时！请重新登录')");
            }
            UserPropertyInfo info = new ET.Sys_BLL.OrganizationBLL().Get_UserPropertyInfo(" AND USERID='" + this.UserID.ToString() + "'");
            if (info != null)
            {
                return Json(info, JsonRequestBehavior.AllowGet);
            }
            return Json("error", JsonRequestBehavior.AllowGet);
        }
     
        public ActionResult Search(string q, string page)
        {
            string strCondition = "";
            if (!string.IsNullOrEmpty(q))
                strCondition = " AND CHARINDEX('" + q + "', ArticleTitle)>0";
            int pageIndex = 1;
            if (this.CheckInfoInt(page))
                pageIndex = int.Parse(page);
            int pageSize = 10;
            long RecordTotalCount = 0;
            List<BlogArticleInfo> listArticle = new ET.Sys_BLL.BlogBLL().PageList_BlogArticleInfo("ArticleID,ArticleTitle,ArticleLabel,ArticleDescription,ArticlePicture,CreateTime,AccessCount,LoveCount,ShareCount,ArticleUrl,TypeID,(select count(1) from BlogCommentInfo where BlogCommentInfo.ArticleID=BlogArticleInfo.ArticleID) ArticleSource", "AND Status=1 " + strCondition, "CreateTime desc", pageIndex, pageSize, ref RecordTotalCount);
            ViewBag.listArticle = listArticle;


            _GetListPager(pageIndex, pageSize, RecordTotalCount);
            return View();
        }

        public ActionResult AdTest()
        {
            return View();
        }
        public ActionResult QQLoginCallBack()
        {
            return View();
        }
        #endregion

        #region Ajax处理方法
     

        //异步加载数据
        [HttpGet]
        public JsonResult AjaxGetArticleList(string groupNumber, string scope, string id)
        {
            if (string.IsNullOrEmpty(groupNumber))
                return Json("", JsonRequestBehavior.AllowGet);
            int intPageSize = 10;
            long lngRecordTotalCount = 0;
            string strCondition = "";
            string _order = "CreateTime desc";
            if (!string.IsNullOrEmpty(scope))
            {
                switch (scope)
                {
                    case "original":
                        strCondition = "AND TypeID=(SELECT TOP 1 TypeID FROM BlogTypeInfo WHERE TypeKey='original')";
                        break;
                    case "collect":
                        if (!String.IsNullOrEmpty(id))
                            strCondition = "AND TypeID='" + id + "')";
                        strCondition = "AND TypeID=(SELECT TOP 1 TypeID FROM BlogTypeInfo WHERE TypeKey='collect')";
                        break;
                    case "list":
                        if (!String.IsNullOrEmpty(id))
                            strCondition = "AND TypeID='" + id + "')";
                        break;
                    case "tag":
                        strCondition = "AND ArticleLabel='" + id + "')";
                        break;
                    case "hotcollect":
                        strCondition = "AND TypeID=(SELECT TOP 1 TypeID FROM BlogTypeInfo WHERE TypeKey='collect')";
                        _order = "Recommend desc,CreateTime desc";
                        break;
                }
            }


            List<BlogArticleInfo> list = new ET.Sys_BLL.BlogBLL().PageList_BlogArticleInfo("ArticleID,ArticleTitle,ArticleLabel,ArticleDescription,ArticlePicture,CreateTime,AccessCount,LoveCount,ShareCount,ArticleUrl,TypeID,(select count(1) from BlogCommentInfo where BlogCommentInfo.ArticleID=BlogArticleInfo.ArticleID) ArticleSource", strCondition, _order, int.Parse(groupNumber), intPageSize, ref lngRecordTotalCount);
            return Json(new { total = lngRecordTotalCount, rows = list }, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public JsonResult AjaxSearch(string query)
        {
            List<BlogArticleInfo> list = new ET.Sys_BLL.BlogBLL().List_BlogArticleInfo("ARTICLETITLE", " AND  CHARINDEX('" + query + "', ARTICLETITLE)>0", "CreateTime");
            var arrData = list.Select(c => c.ArticleTitle);
            return Json(new { query = query, suggestions = arrData, data = arrData }, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult AjaxComment(FormCollection collection)
        {

            BlogCommentInfo info = new BlogCommentInfo();
            info.ArticleID = new Guid(collection["articleid"]);
            info.CommentContent = collection["comment"];
            info.Creator = collection["author"];
            info.CreatorEmail = collection["email"];
            info.CreatorUrl = collection["url"];
            info.CommentPID = collection["comment_parent"];


            info.CreateTime = DateTime.Now;
            new ET.Sys_BLL.BlogBLL().Operate_BlogCommentInfo(info, true);
            return Content(@"<ol class='commentlist'>
                <li class='comment even thread-even depth-1' id='comment-" + info.CommentID + @"'>
                    <div class='c-avatar'>
                        <img alt='' class='avatar avatar-54 photo' height='54' width='54' src='/images/blog/default.png'>
                        <div class='c-main' id='div-comment-" + info.CommentID + @"'>
                           " + info.CommentContent + @"<div class='c-meta'><span class='c-author'>" + info.Creator + @"</span>" + info.CreateTime.Value.ToString("yyyy-MM-dd HH:mm") + @"<a class='comment-reply-link' href='@ViewBag.ArticleInfo.ArticleUrl?replytocom=" + info.CommentID + @"#respond' onclick=\'return addComment.moveForm('div-comment-" + info.CommentID + @"', '" + info.CommentID + @"', 'respond', '" + info.ArticleID + @"')'>回复</a></div>
                        </div>
                    </div>
                </li>
            </ol>");
        }
        /// <summary>
        /// 返回评论列表ViewBag.listArticleComment
        /// </summary>
        public void GetCommentList(string sArticleID)
        {
            List<KeyAndValue> listType = new ET.Sys_BLL.PublicBLL().GetNestListByCondition("CommentID id,ArticleID reserve1,CreateTime reserve3,Creator reserve2,CommentContent text,CommentPID pid", TableNames.BlogCommentInfo, " AND Status=1 AND ArticleID='" + sArticleID + "'", "CreateTime DESC");
            ViewBag.listArticleCommentCount = listType.Count;
            string str = "";
            GetUserCommentNest(ref str, listType, sArticleID);
            ViewBag.listArticleComment = str;
        }


        void GetUserCommentNest(ref string str, List<ET.Sys_DEF.KeyAndValue> listData, string sArticleID)
        {
            //string tmp = str;
            foreach (ET.Sys_DEF.KeyAndValue item in listData)
            {
                if (item.pid != "-1")
                {
                    str += "<ul class='children'><li class='comment even thread-even depth-1' id='comment-" + item.id + "'>                    <div class='c-avatar'>                       <img alt='' class='avatar avatar-54 photo' height='54' width='54' src='/images/blog/default.png'>                        <div class='c-main' id='div-comment-" + item.id + @"'>                           " + item.text + "<div class='c-meta'><span class='c-author'>" + item.reserve2 + @"</span>" + DateTime.Parse(item.reserve3).ToString("yyyy-MM-dd HH:mm") + "<a class='comment-reply-link' href='#respond' onclick=\"return addComment.moveForm('div-comment-" + item.id + "', '" + item.id + "', 'respond', '" + sArticleID + "')\">回复</a></div>                        </div>                    </div>                </li></ul>";
                }
                else
                    str += @"<ol class='commentlist'>                <li class='comment even thread-even depth-1' id='comment-" + item.id + "'>                    <div class='c-avatar'>                        <img alt='' class='avatar avatar-54 photo' height='54' width='54' src='/images/blog/default.png'>                        <div class='c-main' id='div-comment-" + item.id + "'>                           " + item.text + "<div class='c-meta'><span class='c-author'>" + item.reserve2 + @"</span>" + DateTime.Parse(item.reserve3).ToString("yyyy-MM-dd HH:mm") + "<a class='comment-reply-link' href='#respond' onclick=\"return addComment.moveForm('div-comment-" + item.id + "', '" + item.id + "', 'respond', '" + sArticleID + "')\">回复</a></div>                        </div>                    </div>                </li>            </ol>";
                GetUserCommentNest(ref str, item.children, sArticleID);
            }

        }

        [HttpPost]
        public ActionResult AjaxPostLike(string id)
        {
            BlogArticleInfo info = new Sys_BLL.BlogBLL().Get_BlogArticleInfoByID(id);
            if (info != null)
            {
                info.LoveCount++;
                new Sys_BLL.BlogBLL().Operate_BlogArticleInfo(info, false);
                return Content(info.LoveCount.ToString());
            }
            else
                return Content("0");
        }

        [HttpPost]
        public ActionResult AjaxPostFavorite(string id)
        {
            if (this.IsLogin)
            {
                BlogArticleFavoriteInfo info = new BlogArticleFavoriteInfo();
                info.ArticleID = new Guid(id);
                info.UserID = Guid.Parse(this.UserID);
                info.CreateTime = DateTime.Now;
                if (new Sys_BLL.BlogBLL().Operate_BlogArticleFavoriteInfo(info, true))
                    return Content("true");
                else
                    return Content("error");
            }
            else
                return Content("nologin");
        }
        [HttpGet]
        public JsonResult AjaxGetGoodList(string groupNumber)
        {
            if (string.IsNullOrEmpty(groupNumber))
                return Json(new { total = 0, rows = new List<DesignGoodInfo>() }, JsonRequestBehavior.AllowGet);
            int pageSize = 15;
            long RecordTotalCount = 0;
            List<DesignGoodInfo> list = new ET.Sys_BLL.DesignBLL().PageList_DesignGoodInfo("GoodID,GoodUrl,GoodName,GoodPicture,GoodDescription,CreateTime,ACCESSCOUNT,TYPEID", null, "CreateTime desc", int.Parse(groupNumber), pageSize, ref RecordTotalCount);
            return Json(new { total = RecordTotalCount, rows = list }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult AjaxSettingInfo(FormCollection collection, string s)
        {
            //switch (s)
            //{
            //    case "baseinfo":
            if (!this.IsLogin)
            {
                return Content("用户还未登录！或已经登录超时！请重新登录");
            }
            UserPropertyInfo uinfo = new ET.Sys_BLL.OrganizationBLL().Get_UserPropertyInfo(" AND USERID='" + this.UserID.ToString() + "'");
            if (uinfo != null)
            {
                uinfo.Sex = collection["Sex"];
                uinfo.BirthdayYear = collection["birthdayyear"];
                uinfo.BirthdayMonth = collection["birthdaymonth"];
                uinfo.BirthdayDay = collection["birthdayday"];
                uinfo.LiveProvince = collection["liveprovince"];
                uinfo.LiveCity = collection["livecity"];
                uinfo.LiveArea = collection["livearea"];
                uinfo.QQ = collection["QQ"];
                uinfo.Detail = collection["detail"];
                if (new ET.Sys_BLL.OrganizationBLL().Operate_UserPropertyInfo(uinfo))
                {
                    return Content("true");
                }
            }
            //        break;
            //}
            return Content("paramerror");
        }

        [HttpPost]
        public ActionResult AjaxSettingPass(FormCollection collection, string s)
        {
            if (!this.IsLogin)
            {
                return Content("用户还未登录！或已经登录超时！请重新登录");
            }
            UserBaseInfo uinfo = new ET.Sys_BLL.OrganizationBLL().Get_UserBaseInfo(" AND USERID='" + this.UserID.ToString() + "' AND UserPwd='" + ET.ToolKit.Encrypt.EncrypeHelper.EncryptMD5(ET.ToolKit.Common.StringHelper.ClearSqlDangerous(collection["oldUserPwd"])) + "'");
            if (uinfo != null)
            {
                uinfo.UserPwd = ET.ToolKit.Encrypt.EncrypeHelper.EncryptMD5(collection["UserPwd"]);
                if (new ET.Sys_BLL.OrganizationBLL().Operate_UserBaseInfo(uinfo))
                {
                    return Content("true");
                }
                else
                    return Content("error");
            }
            else

                return Content("原始密码不匹配");
        }


        [HttpPost]
        public ActionResult AjaxAddMessage(FormCollection collection)
        {
            BlogMessageInfo info = new BlogMessageInfo();
            info.Creator = collection["Creator"];
            info.MsgType = collection["MsgType"];
            info.CreatorTel = collection["CreatorTel"];
            info.CreatorEmail = collection["CreatorEmail"];
            info.MsgTitle = collection["MsgTitle"];
            info.MsgContent = collection["MsgContent"];
            info.CreateTime = DateTime.Now;
            if (new Sys_BLL.BlogBLL().Operate_BlogMessageInfo(info, true))
                return Content("true");
            else
                return Content("error");

        }
        #endregion
    }
}
