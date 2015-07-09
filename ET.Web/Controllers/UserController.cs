using ET.Constant.DBConst;
using ET.Sys_DEF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Web.Controllers
{
    [UserAuthorize(ErrorUrl = "/account/login")]
    public class UserController : WebControllerBase
    {
        //
        // GET: user/

        public ActionResult Index()
        {
            UserFullProperty info = new ET.Sys_BLL.OrganizationBLL().Get_UserFullPropertyByID(this.UserID);
            if (info == null)
                Response.Redirect("/maccount/login");
            return View(info);
        }
        public ActionResult MyPublish(string page)
        {
            int pageIndex = 1;
            if (this.CheckInfoInt(page))
                pageIndex = int.Parse(page);
            int pageSize = 10;
            long RecordTotalCount = 0;
            //收藏中心-文章收藏 Start
            List<BlogPublish> list = new ET.Sys_BLL.PublicBLL().GetListByConditionPager<BlogPublish>("*", TableNames.BlogPublish, " AND Creator='" + this.UserID + "'", "CreateTime desc", pageIndex, pageSize, ref RecordTotalCount, false);

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
            List<BlogArticleInfo> listCollectArticle = new ET.Sys_BLL.PublicBLL().GetListByConditionPager<BlogArticleInfo>("ArticleID,ArticleTitle,ArticleLabel,ArticleDescription,ArticleCover,CreateTime,AccessCount,LoveCount,ShareCount,ArticleUrl,TypeID,(select count(1) from BlogCommentInfo where BlogCommentInfo.ArticleID=td.ArticleID) ArticleSource", string.Format("(select a.*,b.CreateTime FavoriteTime from BlogArticleInfo a inner join BlogArticleFavorite b on a.ARTICLEID=b.ARTICLEID where  a.Status=1 )td"), "AND Status=1 AND ARTICLEID IN (SELECT ARTICLEID FROM " + ET.Constant.DBConst.TableNames.BlogArticleFavorite + " WHERE UserID='" + this.UserID + "')", "FavoriteTime desc", pageIndex, pageSize, ref RecordTotalCount, false);
            _GetListPager(pageIndex, pageSize, RecordTotalCount);

            return View(listCollectArticle);
        }
        public ActionResult UserSetting()
        {
            if (this.IsLogin)
            {
                this.JavaScript("alert('用户还未登录！或已经登录超时！请重新登录')");
            }
            UserProperty uinfo = new ET.Sys_BLL.OrganizationBLL().Get_UserPropertyByID(this.UserID.ToString());
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
            UserProperty info = new ET.Sys_BLL.OrganizationBLL().Get_UserProperty(" AND USERID='" + this.UserID.ToString() + "'");
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
            UserProperty uinfo = new ET.Sys_BLL.OrganizationBLL().Get_UserProperty(" AND USERID='" + this.UserID.ToString() + "'");
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
                if (new ET.Sys_BLL.OrganizationBLL().Operate_UserProperty(uinfo))
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
            UserBase uinfo = new ET.Sys_BLL.OrganizationBLL().Get_UserBase(" AND USERID='" + this.UserID.ToString() + "' AND UserPwd='" + ET.ToolKit.Encrypt.EncrypeHelper.EncryptMD5(ET.ToolKit.Common.StringHelper.ClearSqlDangerous(collection["oldUserPwd"])) + "'");
            if (uinfo != null)
            {
                uinfo.UserPwd = ET.ToolKit.Encrypt.EncrypeHelper.EncryptMD5(collection["UserPwd"]);
                if (new ET.Sys_BLL.OrganizationBLL().Operate_UserBase(uinfo))
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
        public ActionResult AjaxPostUserSignIn()
        {
            BlogUserSignIn info = new ET.Sys_BLL.BlogBLL().Get_BlogUserSignIn(string.Format("AND USERID='{0}' AND CONVERT(VARCHAR,CREATETIME,23)='{1}' ", this.UserID, DateTime.Now.ToString("yyyy-MM-dd")));
            if (info == null)
            {
                info = new BlogUserSignIn();
                info.UserID = Guid.Parse(this.UserID);
                info.CreateTime = DateTime.Now;
                if (new ET.Sys_BLL.BlogBLL().Operate_BlogUserSignIn(info, true))
                {
                    BlogUserLevelLink link = new ET.Sys_BLL.BlogBLL().Get_BlogUserLevelLink(string.Format("AND USERID='{0}' ", this.UserID));
                    if (link == null)
                    {
                        link = new BlogUserLevelLink();

                        link.UserID = Guid.Parse(this.UserID);
                        link.Exp = 10;
                        new ET.Sys_BLL.BlogBLL().Operate_BlogUserLevelLink(link, true);
                    }
                    else
                    {
                        link.Exp++;
                        new ET.Sys_BLL.BlogBLL().Operate_BlogUserLevelLink(link, false);
                    }
                    return Content("true");
                }
            }
            else
                return Content("repeat");
            return Content("false");
        }

        [HttpPost]
        public ActionResult AjaxPostVipRequest()
        {
            BlogUserRequest info = new ET.Sys_BLL.BlogBLL().Get_BlogUserRequest(string.Format("AND Requester='{0}' AND STATUS<>0 ", this.UserID));
            if (info == null)
            {
                info = new BlogUserRequest();
                info.Requester = Guid.Parse(this.UserID);
                info.CreateTime = DateTime.Now;
                info.Status = 0;
                info.RequestName = "申请成为正式会员";
                if (new ET.Sys_BLL.BlogBLL().Operate_BlogUserRequest(info, true))
                {
                    return Content("true");
                }
            }
            else
                return Content("已发送过申请了！请等待审核！");
            return Content("false");
        }

        #endregion
    }
}
