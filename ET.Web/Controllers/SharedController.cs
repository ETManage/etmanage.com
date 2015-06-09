using ET.Constant.DBConst;
using ET.Sys_DEF;
using ET.ToolKit.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Web.Controllers
{
    public class SharedController : WebControllerBase
    {
        //
        // GET: /_Shared/
        [ChildActionOnly]
        [OutputCache(Duration = 60, VaryByParam = "none")]  
        public ActionResult _PartialNav()
        {
            List<KeyAndValue> listType = new ET.Sys_BLL.PublicBLL().GetNestListByCondition("TYPEID id,TypeUrl reserve1,TypeKey reserve3,TypeLevel reserve2,TYPENAME text,TYPEPID pid", TableNames.BlogTypeInfo, " AND Status=1 ", "TYPESORT DESC");
            return PartialView(listType);
        }
        [ChildActionOnly]
        [OutputCache(Duration = 60, VaryByParam = "none")]  
        public ActionResult _PartialNotice()
        {
 List<NewInfo> listnewestNew = new ET.Sys_BLL.InfoBLL().List_NewInfo(" top 1 newid,newtitle", "AND NewStatus=1 AND TYPEID=(SELECT TOP 1 TYPEID FROM " + TableNames.NewTypeInfo + " WHERE TYPEKEY='notice')", " CreateTime desc");
            return PartialView(listnewestNew);
        }
        [ChildActionOnly]
        [OutputCache(Duration = 60, VaryByParam = "none")]  
        public ActionResult _PartialBlogFooter()
        {
            List<KeyAndValue> listType = new ET.Sys_BLL.PublicBLL().GetListBySql<KeyAndValue>(@" select TYPEID id,TypeUrl reserve1,TypeKey reserve3,TypeLevel reserve2,TYPENAME text,TYPEPID pid from (SELECT B.* FROM  BlogTypeInfo A inner JOIN BlogTypeInfo B ON CAST(A.TypeID as varchar(36)) =B.TypePID where a.TypeKey='collect'
                union
                SELECT * FROM  BlogTypeInfo where  TypeKey='collect')TMP WHERE Status=1 and len(TYPEPID)>2 ORDER BY TYPESORT DESC");
            return PartialView(listType);
        }
        [ChildActionOnly]
        public ActionResult _PartialLoginStatus()
        {
            string strHtml = @"<div class='user-ed' id='nologin'>
                <a href='" + PublicHelper.GetHostAddress() + "/account/login?t=0&l=" + Request.Url.ToString() + "' rel=\"nofollow\">免费注册</a><span class=\"ml10 mr10\">|</span><a href='" + PublicHelper.GetHostAddress() + "/account/login?l=" + Request.Url.ToString() + "' rel=\"nofollow\">登录</a><br>                <span id=\"ibtnQQLogin\"></span></div>";
            if (this.IsLogin)
            {
                UserPropertyInfo userinfo = new ET.Sys_BLL.OrganizationBLL().Get_UserPropertyInfo(" AND UserID='" + this.UserID.ToString() + "'");

                if (userinfo != null)
                {
                    bool IsQQLogin = false;
                    ViewBag.LoginUserName = userinfo.CNName;
                    ViewBag.LoginUserEMail = userinfo.EMail;
                    strHtml = "<div class='user-ed' id='islogin'><div class='lot'><div class='fLeft cBlack'>" + userinfo.CNName + "</div><div class='fLeft'><a href='/blog/usercenter/' class='tit key' title='账号'><img src='/images/blog/noavatar_small.gif' onerror='this.onerror = null; this.src = '/images/blog/noavatar_small.gif''></a></div><ul><li class='um-reply'><strong class='vwmy " + (IsQQLogin ? "qq" : "") + "'><a href='/blog/usercenter/' class='user-name'>" + userinfo.CNName + "</a></strong></li><li><a href='javascript:alert(\'申请已发送\')'>申请转正</a></li><li><a href='/blog/usersetting/'>个人设置</a></li><li><a href='/blog/usercenter?s=myacticle' _style='background-image:url(/images/blog/thread_b.png) !important'>我的博文</a></li><li><a href='/blog/usercenter?s=myfavorite' _style='background-image:url(/images/blog/favorite_b.png) !important'>我的收藏</a></li><li class='last border-top1'><a href='/account/logout'>退出</a></li></ul></div></div>";
                }
            }

            return Content(strHtml);
        }
        [ChildActionOnly]
        [OutputCache(Duration = 60, VaryByParam = "none")]  
        public ActionResult _PartialDemoDesign()
        {
            List<DesignGoodInfo> listDesign = new ET.Sys_BLL.DesignBLL().List_DesignGoodInfo(" *", "AND Status=1 ", " CreateTime desc");
            return PartialView(listDesign);
        }
        [ChildActionOnly]
        [OutputCache(Duration = 60, VaryByParam = "none")]  
        public ActionResult _PartialBlogroll()
        {
            List<BlogRollInfo> listRoll = new ET.Sys_BLL.BlogBLL().List_BlogRollInfo(" *", "AND Status=1 ", " RollSort desc");
            return PartialView(listRoll);
        }
        [ChildActionOnly]
        [OutputCache(Duration = 60, VaryByParam = "none")]  
        public ActionResult _PartialArticleLove()
        {
            return PartialView(GetArticleList(5, null, null, "LoveCount desc,CreateTime DESC"));
        }
        [ChildActionOnly]
        [OutputCache(Duration = 60, VaryByParam = "none")]  
        public ActionResult _PartialArticleLabel()
        {
            List<KeyAndValue> listArticleLabel = new ET.Sys_BLL.PublicBLL().GetListBySql<KeyAndValue>("select  TOP 20 id, count(text) text from ( SELECT  ltrim(rtrim(ArticleLabel)) id,ArticleID  text FROM " + TableNames.BlogArticleInfo + " where len(isnull(ArticleLabel,''))>0 AND Status=1  )A group by id");
            return PartialView(listArticleLabel);
        }
        [ChildActionOnly]
        [OutputCache(Duration = 60, VaryByParam = "none")]  
        public ActionResult _PartialArticleComment()
        {
            return PartialView(GetArticleList(5, null, null, "ArticleSource desc,CreateTime DESC"));
        }
        public ActionResult _PartialToolbar()
        {
            return PartialView(this.CurrentUserInfo);

          }

        
        /// <summary>
        /// 分页查询，返回ViewBag.listPager
        /// </summary>
        public void _GetListPager(int pageIndex, int pageSize, long RecordTotalCount)
        {

            string strPagerText = "<div class='pagination'><ul>";

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
    }
}
