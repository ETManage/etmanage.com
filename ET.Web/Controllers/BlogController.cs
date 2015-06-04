using ET.Constant.DBConst;
using ET.Sys_DEF;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Xml;

namespace ET.Web.Controllers
{
    public class BlogController : WebControllerBase
    {

        #region 分布视图
        //母版页
        public BlogController()
        {
            _LayoutBlog();
        }
        public void _LayoutBlog()
        {
            List<KeyAndValue> listType = new ET.Sys_BLL.PublicBLL().GetNestListByCondition("TYPEID id,TypeUrl reserve1,TypeLevel reserve2,TYPENAME text,TYPEPID pid", TableNames.BlogTypeInfo, " AND Status=1 ", "TYPESORT DESC");
            ViewBag.listBlogType = listType;

            List<NewInfo> listnewestNew = new ET.Sys_BLL.InfoBLL().List_NewInfo(" top 1 newid,newtitle", "AND NewStatus=1 AND TYPEID=(SELECT TOP 1 TYPEID FROM " + TableNames.NewTypeInfo + " WHERE TYPEKEY='notice')", " CreateTime desc");
            ViewBag.listNewestNew = listnewestNew;

            if (this.IsLogin)
            {
                UserPropertyInfo userinfo = new ET.Sys_BLL.OrganizationBLL().Get_UserPropertyInfo(" AND UserID='" + this.UserID.ToString() + "'");
                if (userinfo != null)
                {
                    bool IsQQLogin = false;
                    ViewBag.LoginUserName = userinfo.CNName;
                    ViewBag.LoginUserEMail = userinfo.EMail;
                    ViewBag.scriptJoin = "$('#nologin').remove();";
                    ViewBag.htmlLoginOk = "<div class='user-ed' id='islogin'><div class='lot'><div class='fLeft cBlack'>" + userinfo.CNName + "</div><div class='fLeft'><a href='/blog/usercenter/' class='tit key' title='账号'><img src='/images/blog/noavatar_small.gif' onerror='this.onerror = null; this.src = '/images/blog/noavatar_small.gif''></a></div><ul><li class='um-reply'><strong class='vwmy " + (IsQQLogin ? "qq" : "") + "'><a href='/blog/usercenter/' class='user-name'>" + userinfo.CNName + "</a></strong></li><li><a href='javascript:alert(\'申请已发送\')'>申请转正</a></li><li><a href='/blog/usersetting/'>个人设置</a></li><li><a href='/blog/usercenter?s=myacticle' _style='background-image:url(/images/blog/thread_b.png) !important'>我的博文</a></li><li><a href='/blog/usercenter?s=myfavorite' _style='background-image:url(/images/blog/favorite_b.png) !important'>我的收藏</a></li><li class='last border-top1'><a href='/blog/logout'>退出</a></li></ul></div></div>";
                }
            }
        }
        public ActionResult _GetPartialDemoDesign()
        {
            return PartialView("_PartialDemoDesign");
        }
        public ActionResult _GetPartialBlogroll()
        {
            List<BlogRollInfo> listRoll = new ET.Sys_BLL.BlogBLL().List_BlogRollInfo(" *", "AND Status=1 ", " RollSort desc");
            ViewBag.listRoll = listRoll;
            return PartialView("_PartialBlogroll");
        }
        public ActionResult _GetPartialArticleLove()
        {
            List<BlogArticleInfo> listLoveArticle = new ET.Sys_BLL.PublicBLL().GetListByCondition<BlogArticleInfo>(5, "ARTICLEID,ARTICLETITLE,ArticleDescription,ArticlePicture,CreateTime,ACCESSCOUNT,LoveCount,ShareCount,ARTICLEUrl,TYPEID", ET.Constant.DBConst.TableNames.BlogArticleInfo, "AND Status=1 ", "CreateTime DESC");
            ViewBag.listLoveArticle = listLoveArticle;
            return PartialView("_PartialArticleLove");
        }
        public ActionResult _GetPartialArticleLabel()
        {
            List<KeyAndValue> listArticleLabel = new ET.Sys_BLL.PublicBLL().GetListBySql<KeyAndValue>("select  TOP 20 id, count(text) text from ( SELECT  ltrim(rtrim(ArticleLabel)) id,ArticleID  text FROM " + TableNames.BlogArticleInfo + " where len(isnull(ArticleLabel,''))>0 AND Status=1  )A group by id");
            ViewBag.listArticleLabel = listArticleLabel;
            return PartialView("_PartialArticleLabel");
        }
        public void _GetPartialArticleList(string Field = "ArticleID,ArticleTitle,ArticleLabel,ArticleDescription,ArticlePicture,CreateTime,AccessCount,LoveCount,ShareCount,ArticleUrl,TypeID,(select count(1) from BlogCommentInfo where BlogCommentInfo.ArticleID=BlogArticleInfo.ArticleID) ArticleSource", string TableName = TableNames.BlogArticleInfo, string Condition = "", string Order = "CreateTime DESC", bool IsNoLock = true)
        {
            List<BlogArticleInfo> listArticle = new ET.Sys_BLL.PublicBLL().GetListByCondition<BlogArticleInfo>(10, Field, TableName, "AND Status=1 " + Condition, Order, IsNoLock);
            ViewBag.listArticle = listArticle;
        }
        #endregion

        /// <summary>
        /// 绑定"猜你喜欢“
        /// </summary>
        public void BindlistLoveArticle(string Condition)
        {

        }
        public void BindlistArticleLabel(string Condition)
        {

        }

        /// <summary>
        /// 绑定"标签云“
        /// </summary>
        // GET: /Blog/
        public ActionResult Index()
        {
            _GetPartialArticleList();
            List<BlogArticleInfo> listTopArticle = new ET.Sys_BLL.PublicBLL().GetListByCondition<BlogArticleInfo>(5, "ArticleID,ArticleTitle,ArticleLabel,ArticleDescription,case when CHARINDEX(',',ArticlePicture)>0 then ArticlePicture else ',' end ArticlePicture,CreateTime,AccessCount,LoveCount,ShareCount,ArticleUrl,TypeID,(select count(1) from BlogCommentInfo where BlogCommentInfo.ArticleID=BlogArticleInfo.ArticleID) ArticleSource", TableNames.BlogArticleInfo, "AND Status=1 AND IsRoll=1 ", "CreateTime DESC");
            ViewBag.listTopArticle = listTopArticle;
            return View();
        }
        public ActionResult Detail(string id)
        {
            if (string.IsNullOrEmpty(id))
                return Json("", JsonRequestBehavior.AllowGet);
            BlogArticleInfo info = new ET.Sys_BLL.BlogBLL().Get_BlogArticleInfoByID(id);
            if (info != null)
            {
                ViewBag.ArticleInfo = info;
                ViewBag.Title = info.ArticleTitle;
                BlogTypeInfo infotype = new ET.Sys_BLL.BlogBLL().Get_BlogTypeInfoByID(info.TypeID.ToString());
                if (infotype != null)
                    ViewBag.ArticleType = infotype;
                List<BlogCommentInfo> listArticleComment = new ET.Sys_BLL.BlogBLL().List_BlogCommentInfo("*", "AND ArticleID='" + info.ArticleID + "'", "CreateTime desc");
                ViewBag.listArticleComment = listArticleComment;


                BindlistArticleLabel(null);


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

                List<BlogArticleInfo> listOtherArticle = new ET.Sys_BLL.PublicBLL().GetListByCondition<BlogArticleInfo>(8, "ARTICLEID,ARTICLETITLE,ArticleDescription,ArticlePicture,CreateTime,ACCESSCOUNT,LoveCount,ShareCount,ARTICLEUrl,TYPEID", TableNames.BlogArticleInfo, "AND Status=1 ", "CreateTime DESC");
                ViewBag.listOtherArticle = listOtherArticle.Take(4);
                ViewBag.listOtherArticle2 = listOtherArticle.Skip(4);

                //收藏按钮处理
                if (this.IsLogin)
                {
                    BlogArticleFavoriteInfo favoriteinfo = new ET.Sys_BLL.BlogBLL().Get_BlogArticleFavoriteInfo(string.Format(" AND ArticleID='{0}' AND UserID='{1}'", id, this.UserID));
                    if (favoriteinfo != null)
                        ViewBag.scriptIsFavorite = "$('#iAddFavorite').html('已收藏');$('#iAddFavorite').addClass('cYellow');";
                }


            }
            else
                return Redirect("/PageError/error404.html");
            return View();
        }

        public ActionResult Original(string id)
        {
            string strCondition = "AND TypeID in (SELECT TypeID FROM BlogTypeInfo WHERE TypeKey='original')";
            List<BlogArticleInfo> listArticle = new ET.Sys_BLL.PublicBLL().GetListByCondition<BlogArticleInfo>(10, "ArticleID,ArticleTitle,ArticleLabel,ArticleDescription,ArticlePicture,CreateTime,AccessCount,LoveCount,ShareCount,ArticleUrl,TypeID,(select count(1) from BlogCommentInfo where BlogCommentInfo.ArticleID=BlogArticleInfo.ArticleID) ArticleSource", TableNames.BlogArticleInfo, "AND Status=1 " + strCondition, "CreateTime DESC");
            ViewBag.listArticle = listArticle;

            BindlistArticleLabel(null);
            BindlistLoveArticle(null);

            return View();
        }
        public ActionResult Collect(string id)
        {
            string strCondition = "";
            string strTitle="资讯中心";
            if (!string.IsNullOrEmpty(id))
            {
                strCondition = "AND TypeKey='" + id + "'";
                BlogTypeInfo typeinfo = new ET.Sys_BLL.BlogBLL().Get_BlogTypeInfoByCondition(" AND TYPEKEY='" + id + "'");
                if (typeinfo != null)
                    strTitle = typeinfo.TypeName;
            }
            else
                strCondition = "AND TypeKey='collect'";
            ViewBag.InfoBlogTypeName = strTitle;
            _GetPartialArticleList(null, string.Format("(SELECT A.* FROM  BlogArticleInfo A INNER JOIN BlogTypeInfo B ON A.TypeID=B.TypeID WHERE 1=1 {0})td", strCondition), null, null, false);
            return View();
        }
        public ActionResult HotCollect(string id)
        {
            string strCondition = "AND TypeKey='collect'";
            _GetPartialArticleList(null, string.Format("(SELECT A.* FROM  BlogArticleInfo A INNER JOIN BlogTypeInfo B ON A.TypeID=B.TypeID WHERE 1=1 {0})td", strCondition), null, "Recommend desc,CreateTime desc", false);
            return View();
        }
        public ActionResult ArticleList(string id)
        {
            string strCondition = " AND TypeId='" + id + "'";
            List<BlogArticleInfo> listArticle = new ET.Sys_BLL.PublicBLL().GetListByCondition<BlogArticleInfo>(10, "ArticleID,ArticleTitle,ArticleLabel,ArticleDescription,ArticlePicture,CreateTime,AccessCount,LoveCount,ShareCount,ArticleUrl,TypeID,(select count(1) from BlogCommentInfo where BlogCommentInfo.ArticleID=BlogArticleInfo.ArticleID) ArticleSource", TableNames.BlogArticleInfo, "AND Status=1 " + strCondition, "CreateTime DESC");
            ViewBag.listArticle = listArticle;

            BindlistArticleLabel(null);
            BindlistLoveArticle(null);
            return View();
        }
        public ActionResult ArticleTag(string tag)
        {
            string strCondition = " AND ArticleLabel='" + tag + "'";
            List<BlogArticleInfo> listArticle = new ET.Sys_BLL.PublicBLL().GetListByCondition<BlogArticleInfo>(10, "ArticleID,ArticleTitle,ArticleLabel,ArticleDescription,ArticlePicture,CreateTime,AccessCount,LoveCount,ShareCount,ArticleUrl,TypeID,(select count(1) from BlogCommentInfo where BlogCommentInfo.ArticleID=BlogArticleInfo.ArticleID) ArticleSource", TableNames.BlogArticleInfo, "AND Status=1 " + strCondition, "CreateTime DESC");
            ViewBag.listArticle = listArticle;

            BindlistArticleLabel(null);
            BindlistLoveArticle(null);

            return View();
        }

        public ActionResult Picture()
        {
            int pageIndex = 1;
            int pageSize = 15;
            long RecordTotalCount = 0;
            List<ShopGoodInfo> list = new ET.Sys_BLL.ShopBLL().PageList_ShopGoodInfo("GoodID,GoodUrl,GoodName,GoodPicture,GoodDescription,CreateTime,ACCESSCOUNT,TYPEID", " AND STATUS=1 ", "CreateTime desc", pageIndex, pageSize, ref RecordTotalCount);
            ViewBag.ShopGoodInfo = list;
            return View();
        }
        public ActionResult PictureDetail(string id)
        {
            ShopGoodInfo info = new ET.Sys_BLL.ShopBLL().Get_ShopGoodInfoByID(id);
            if (info != null)
                ViewBag.ShopGoodInfo = info;
            else
                ViewBag.ShopGoodInfo = new ShopGoodInfo();
            return View();
        }

        public ActionResult Login()
        {
            return View();
        }
        public ActionResult Logout()
        {
            ClearLoginCache();
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

            #region 分页查询
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
            #endregion

            return View();
        }
        public ActionResult UserSetting()
        {
            if (this.UserID == Guid.Empty)
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
            if (this.UserID == Guid.Empty)
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
        public ActionResult AdTest()
        {
            return View();
        }
        /// <summary>
        /// 查询方法
        /// </summary>
        /// <param name="q">条件</param>
        /// <returns></returns>
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


            #region 分页查询
            string strPagerText = "<div class='pagination'><ul>";

            string strCurrUrl = Request.Path.ToString() + "?q=" + q + "&";

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
            #endregion

            BindlistLoveArticle(null);
            return View();
        }


        public ActionResult QQRegister()
        {
            return View();
        }
        public ActionResult QQLoginCallBack()
        { return View(); }
        public ActionResult ValidateCode()
        {
            int width = 100;
            int height = 40;
            int fontsize = 20;
            string code = string.Empty;
            byte[] bytes = ET.ToolKit.ToolKit.Drawing.ValidateCodeHelper.CreateValidateGraphic(out code, 4, width, height, fontsize);
            ET.ToolKit.ToolKit.Common.SessionHelper.Add(SystemConfigConst.SessioncheckValiCode, code);
            return File(bytes, @"image/jpeg");
        }
        #region Ajax处理方法
        [HttpPost]
        public ActionResult AjaxQQlogin(string u, string p, string name, string sex, string pic)
        {
            try
            {
                string strReturn = "";
                if (!string.IsNullOrEmpty(u) && !string.IsNullOrEmpty(p))
                {
                    UserBaseInfo baseinfo = new ET.Sys_BLL.OrganizationBLL().Get_UserBaseInfo(" AND UserName='" + u + "'");
                    if (baseinfo == null)
                    {
                        User_Full_Info info = new User_Full_Info();
                        baseinfo = new UserBaseInfo();
                        UserPropertyInfo stuinfo = new UserPropertyInfo();
                        //baseinfo.UserID = Guid.NewGuid();
                        baseinfo.UserName = u;
                        baseinfo.UserStatus = 1;
                        baseinfo.UserPwd = ET.ToolKit.Encrypt.EncrypeHelper.EncryptMD5(p);

                        stuinfo.Nickname = u;
                        if (!string.IsNullOrEmpty(name))
                            stuinfo.CNName = name;
                        if (!string.IsNullOrEmpty(sex))
                            stuinfo.Sex = sex;
                        stuinfo.QQ = u;
                        if (!string.IsNullOrEmpty(pic))
                            stuinfo.Photo = pic;
                        stuinfo.Source = "QQ登录";
                        stuinfo.CreateTime = DateTime.Now;

                        info.userbaseinfo = baseinfo;
                        info.userstuinfo = stuinfo;
                        info.userrole = new List<UserRoleLink>();
                        UserRoleLink role = new UserRoleLink();
                        info.userrole.Add(role);
                        if (new ET.Sys_BLL.OrganizationBLL().Operate_User_Info(info, true))
                        {
                        }
                    }

                    if (baseinfo.UserStatus == 1 || (baseinfo.UserStatus == 0 && baseinfo.UserStartTime < DateTime.Now && baseinfo.UserEndTime >= DateTime.Now))
                    {
                        ET.Sys_Base.Login_Ajax loginAjax = new ET.Sys_Base.Login_Ajax();
                        try
                        {
                            if (loginAjax.SinglePointState() == "1")
                            {
                                strReturn = loginAjax.WebLoginSinglePoint(u, p);

                            }
                            else
                            {
                                strReturn = loginAjax.WebLoginUser(u, p);

                            }
                        }
                        catch (Exception ex)
                        {
                            strReturn = ex.Message;
                        }
                    }
                    else
                        strReturn = "非常抱歉！该帐号无权登录！请联系管理员";
                }
                return Content(strReturn);
            }
            catch (Exception ex)
            {
                //throw ex;
                return Content(ex.Message);
            }


        }


        [HttpPost]
        public ActionResult AjaxSettingInfo(FormCollection collection, string s)
        {
            //switch (s)
            //{
            //    case "baseinfo":
            if (this.UserID == Guid.Empty)
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
            if (this.UserID == Guid.Empty)
            {
                this.Content("用户还未登录！或已经登录超时！请重新登录");
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
        //AJAX登录
        [HttpPost]
        public ActionResult AjaxLogin(FormCollection collection, string l)
        {
            string strReturn = "";
            if (ModelState.IsValid)
            {
                ET.Sys_Base.Login_Ajax loginAjax = new ET.Sys_Base.Login_Ajax();
                try
                {
                    if (loginAjax.SinglePointState() == "1")
                    {
                        strReturn = loginAjax.WebLoginSinglePoint(collection["username"], collection["password"]);

                    }
                    else
                    {
                        strReturn = loginAjax.WebLoginUser(collection["username"], collection["password"]);

                    }
                }
                catch (Exception ex)
                {
                    strReturn = ex.Message;
                }
            }
            //if (strReturn == "true")
            //{
            //    Response.Redirect(!string.IsNullOrEmpty(l) ? l : "/blog/index/");

            //}
            return Content(strReturn);
        }

        [HttpGet]
        public ActionResult AjaxCheckUser(string uid)
        {
            uid = ET.ToolKit.Common.StringHelper.ClearSqlDangerous(uid);
            UserBaseInfo info = new ET.Sys_BLL.OrganizationBLL().Get_UserBaseInfo(" AND UserName='" + uid + "'");
            if (info != null)
                return Content("用户名已经存在！");
            else
                return Content("true");

        }
        [HttpGet]
        public ActionResult AjaxCheckValidatecode(string code)
        {
            string resultMsg = "true";
            string pCode = code;
            string sCode = ET.ToolKit.ToolKit.Common.SessionHelper.Get(SystemConfigConst.SessioncheckValiCode).ToString();
            if (string.IsNullOrEmpty(pCode))
            {
                resultMsg = "请输入验证码";
            }
            else if (string.IsNullOrEmpty(sCode))
            {
                resultMsg = "验证码过期";
            }
            else if (pCode.ToLower() != sCode.ToLower())
            {
                resultMsg = "验证码不正确";
            }

            return Content(resultMsg);

        }
        [HttpPost]
        public ActionResult AjaxRegister(FormCollection collection, string l)
        {



            User_Full_Info info = new User_Full_Info();
            UserBaseInfo baseinfo = new UserBaseInfo();
            UserPropertyInfo stuinfo = new UserPropertyInfo();



            baseinfo.UserName = collection["user"];
            baseinfo.UserPwd = ET.ToolKit.Encrypt.EncrypeHelper.EncryptMD5(collection["passwd"]);
            baseinfo.UserStatus = 1;

            stuinfo.Nickname = collection["user"];
            stuinfo.CNName = collection["user"];
            stuinfo.ENName = collection["user"];
            stuinfo.Sex = "保密";
            stuinfo.QQ = collection["qq"];
            stuinfo.CreateTime = DateTime.Now;
            info.userbaseinfo = baseinfo;
            info.userstuinfo = stuinfo;

            info.userrole = new List<UserRoleLink>();
            UserRoleLink role = new UserRoleLink();
            info.userrole.Add(role);
            if (new ET.Sys_BLL.OrganizationBLL().Operate_User_Info(info, true))
                return Content("true");
            else
                return Content("error");
        }
        [HttpGet]
        public ActionResult AjaxLogout()
        {
            return RedirectToAction("login");
        }

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
        public ActionResult AjaxComment(FormCollection collection)
        {

            BlogCommentInfo info = new BlogCommentInfo();
            info.ArticleID = new Guid(collection["articleid"]);
            info.CommentContent = collection["comment"];
            info.Creator = collection["author"];
            info.CreatorEmail = collection["email"];
            info.CreatorUrl = collection["url"];
            info.CreateTime = DateTime.Now;
            new ET.Sys_BLL.BlogBLL().Operate_BlogCommentInfo(info, true);
            return Content("");
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
                info.UserID = this.UserID;
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
                return Json(new { total = 0, rows = new List<ShopGoodInfo>() }, JsonRequestBehavior.AllowGet);
            int pageSize = 15;
            long RecordTotalCount = 0;
            List<ShopGoodInfo> list = new ET.Sys_BLL.ShopBLL().PageList_ShopGoodInfo("GoodID,GoodUrl,GoodName,GoodPicture,GoodDescription,CreateTime,ACCESSCOUNT,TYPEID", null, "CreateTime desc", int.Parse(groupNumber), pageSize, ref RecordTotalCount);
            return Json(new { total = RecordTotalCount, rows = list }, JsonRequestBehavior.AllowGet);
        }
        #endregion
    }
}
