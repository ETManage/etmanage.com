using ET.Constant.DBConst;
using ET.Sys_DEF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Web.Controllers
{
    public class UserController : WebControllerBase
    {
        //
        // GET: user/

        public ActionResult Index(string page)
        {
            int pageIndex = 1;
            if (this.CheckInfoInt(page))
                pageIndex = int.Parse(page);
            int pageSize = 10;
            long RecordTotalCount = 0;
            //收藏中心-文章收藏 Start
            List<BlogArticleInfo> listCollectArticle = new ET.Sys_BLL.PublicBLL().GetListByConditionPager<BlogArticleInfo>("ArticleID,ArticleTitle,ArticleLabel,ArticleDescription,ArticleCover,CreateTime,AccessCount,LoveCount,ShareCount,ArticleUrl,TypeID,(select count(1) from BlogCommentInfo where BlogCommentInfo.ArticleID=td.ArticleID) ArticleSource", string.Format("(select a.*,b.CreateTime FavoriteTime from BlogArticleInfo a inner join BlogArticleFavoriteInfo b on a.ARTICLEID=b.ARTICLEID where  a.Status=1 )td"), "AND Status=1 AND ARTICLEID IN (SELECT ARTICLEID FROM " + ET.Constant.DBConst.TableNames.BlogArticleFavoriteInfo + " WHERE UserID='" + this.UserID + "')", "FavoriteTime desc", pageIndex, pageSize, ref RecordTotalCount, false);
            ViewBag.listCollectArticle = listCollectArticle;

            //收藏中心-文章收藏 End
            //查看记录 Start
            //List<KeyAndValue> listBlogViewRecord = new ET.Sys_BLL.PublicBLL().GetListByCondition<KeyAndValue>("")

            _GetListPager(pageIndex, pageSize, RecordTotalCount);

            return View();
        }
        public ActionResult MyPublish(string page)
        {
            int pageIndex = 1;
            if (this.CheckInfoInt(page))
                pageIndex = int.Parse(page);
            int pageSize = 10;
            long RecordTotalCount = 0;
            //收藏中心-文章收藏 Start
            List<BlogPublish> list = new ET.Sys_BLL.PublicBLL().GetListByConditionPager<BlogPublish>("*",TableNames.BlogPublish, " AND Creator='" + this.UserID + "'", "CreateTime desc", pageIndex, pageSize, ref RecordTotalCount, false);

            _GetListPager(pageIndex, pageSize, RecordTotalCount);

            return View(list);
        }
        public ActionResult MyFavorites(string page)
        {
            int pageIndex = 1;
            if (this.CheckInfoInt(page))
                pageIndex = int.Parse(page);
            int pageSize = 10;
            long RecordTotalCount = 0;
            //收藏中心-文章收藏 Start
            List<BlogArticleInfo> listCollectArticle = new ET.Sys_BLL.PublicBLL().GetListByConditionPager<BlogArticleInfo>("ArticleID,ArticleTitle,ArticleLabel,ArticleDescription,ArticleCover,CreateTime,AccessCount,LoveCount,ShareCount,ArticleUrl,TypeID,(select count(1) from BlogCommentInfo where BlogCommentInfo.ArticleID=td.ArticleID) ArticleSource", string.Format("(select a.*,b.CreateTime FavoriteTime from BlogArticleInfo a inner join BlogArticleFavoriteInfo b on a.ARTICLEID=b.ARTICLEID where  a.Status=1 )td"), "AND Status=1 AND ARTICLEID IN (SELECT ARTICLEID FROM " + ET.Constant.DBConst.TableNames.BlogArticleFavoriteInfo + " WHERE UserID='" + this.UserID + "')", "FavoriteTime desc", pageIndex, pageSize, ref RecordTotalCount, false);
            _GetListPager(pageIndex, pageSize, RecordTotalCount);

            return View(listCollectArticle);
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
        #region Ajax处理
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
                uinfo.CNName = collection["CNName"];
                uinfo.Tel = collection["Tel"];
                uinfo.Mobile = collection["Mobile"];
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
        #endregion
    }
}
