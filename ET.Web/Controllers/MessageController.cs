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

    }
}
