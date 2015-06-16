using ET.Sys_DEF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Web.Controllers
{
    public class MessageController : WebControllerBase
    {
        //
        // GET: /Message/
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
        public ActionResult Index(string page)
        {
            string strCondition = "";
            int pageIndex = 1;
            if (this.CheckInfoInt(page))
                pageIndex = int.Parse(page);
            int pageSize = 10;
            long RecordTotalCount = 0;
            List<BlogMessageInfo> listMessage = new ET.Sys_BLL.BlogBLL().PageList_BlogMessageInfo("*", "AND Status=1 " + strCondition, "CreateTime desc", pageIndex, pageSize, ref RecordTotalCount);
            ViewBag.listMessage = listMessage;


            _GetListPager(pageIndex, pageSize, RecordTotalCount);
            return View();
        }
        public ActionResult Publish(string page)
        {
            List<KeyAndValue> list = new ET.Sys_BLL.PublicBLL().GetListByCondition<KeyAndValue>("TYPEID id,TYPENAME text", ET.Constant.DBConst.TableNames.BlogTypeInfo, " AND ISNULL(IsOnlyNav,0)=0 AND STATUS=1", "TYPESORT");
            ViewBag.listArticleType = list;
            return View();

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
            if (new ET.Sys_BLL.BlogBLL().Operate_BlogMessageInfo(info, true))
                return Content("true");
            else
                return Content("error");

        }
        [HttpPost]
        public ActionResult AjaxAddPublish(FormCollection collection)
        {
            BlogPublish info = new BlogPublish();
            info.Creator = this.UserID;
            info.PublishType = collection["PublishType"];
            info.Title = collection["Title"];
            info.PublishContent = collection["PublishContent"];
            info.Description = collection["Description"];
            info.PublishSource = collection["PublishSource"];
            info.Label = collection["Label"];
            if (!string.IsNullOrEmpty(collection["Cover"]))
                info.Cover = collection["Cover"];
            else
                info.Cover = "";
            info.CreateTime = DateTime.Now;
            info.Status = 0;
            if (new ET.Sys_BLL.BlogBLL().Operate_BlogPublish(info, true))
                return Content("true");
            else
                return Content("error");

        }

    }
}
