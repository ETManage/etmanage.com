using ET.Constant.DBConst;
using ET.Sys_DEF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Web.Controllers
{
    [OutputCache(Duration = 60)]
    public class BlogAPIController : Controller
    {

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
                Field = "ArticleID,ArticleTitle,ArticleLabel,ArticleDescription,ArticleCover,CreateTime,AccessCount,LoveCount,ShareCount,ArticleUrl,TypeID,(select count(1) from BlogCommentInfo where BlogCommentInfo.ArticleID=BlogArticleInfo.ArticleID) ArticleSource";
            if (string.IsNullOrEmpty(Order))
                Order = "CreateTime DESC";

            List<BlogArticleInfo> listArticle = new ET.Sys_BLL.PublicBLL().GetListByCondition<BlogArticleInfo>(TopCount, Field, TableName, "AND Status=1 " + Condition, Order, IsNoLock);
            return listArticle;
        }


        #endregion
        #region 外部访问引用
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


            List<BlogArticleInfo> list = new ET.Sys_BLL.BlogBLL().PageList_BlogArticleInfo("ArticleID,ArticleTitle,ArticleLabel,ArticleDescription,ArticleCover,CreateTime,AccessCount,LoveCount,ShareCount,ArticleUrl,TypeID,(select count(1) from BlogCommentInfo where BlogCommentInfo.ArticleID=BlogArticleInfo.ArticleID) ArticleSource", strCondition, _order, int.Parse(groupNumber), intPageSize, ref lngRecordTotalCount);
            return Json(new { total = lngRecordTotalCount, rows = list }, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public JsonResult APIAjaxGetArticleList()
        {
            List<BlogArticleInfo> list = GetArticleList(3, "ArticleID,ArticleTitle,ArticleLabel,cast(ArticleDescription as nvarchar(50)) ArticleDescription ,ArticleCover,CreateTime,AccessCount,LoveCount,ShareCount,ArticleUrl,TypeID,(select count(1) from BlogCommentInfo where BlogCommentInfo.ArticleID=BlogArticleInfo.ArticleID) ArticleSource", " and LEN(ArticleCover)>0 ", "CreateTime DESC"); ;
            HttpContext.Response.AppendHeader("Access-Control-Allow-Origin", "*");
            return Json(list,JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public JsonResult APIAjaxGetNewList()
        {
            List<NewInfo> listnewestNew = new ET.Sys_BLL.NewsBLL().List_NewInfo(" top 1 newid,newtitle,cast(createtime as varchar(23)) createtime", "AND Status=1 AND TYPEID=(SELECT TOP 1 TYPEID FROM " + TableNames.NewTypeInfo + " WHERE TYPEKEY='notice')", " CreateTime desc");
            HttpContext.Response.AppendHeader("Access-Control-Allow-Origin", "*");
            return Json(listnewestNew, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public ActionResult APIAjaxGetTypeList()
        {
            List<KeyAndValue> listType = new ET.Sys_BLL.PublicBLL().GetListBySql<KeyAndValue>(@" select TYPEID id,TypeUrl reserve1,TypeKey reserve3,TypeLevel reserve2,TYPENAME text,TYPEPID pid from (SELECT B.* FROM  BlogTypeInfo A inner JOIN BlogTypeInfo B ON CAST(A.TypeID as varchar(36)) =B.TypePID where a.TypeKey='collect'
                union
                SELECT * FROM  BlogTypeInfo where  TypeKey='collect')TMP WHERE Status=1 and len(TYPEPID)>2 ORDER BY TYPESORT DESC");
            HttpContext.Response.AppendHeader("Access-Control-Allow-Origin", "*");
            return Json(listType, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public ActionResult APIAjaxGetDesignList()
        {
            List<DesignGoodInfo> listDesign = new ET.Sys_BLL.DesignBLL().List_DesignGoodInfo(" top 8 *", "AND Status=1 ", " CreateTime desc");

            HttpContext.Response.AppendHeader("Access-Control-Allow-Origin", "*");
            return Json(listDesign, JsonRequestBehavior.AllowGet);
        }
        public string Options()
        {

            return null; // HTTP 200 response with empty body

        }
        #endregion
    }
}
