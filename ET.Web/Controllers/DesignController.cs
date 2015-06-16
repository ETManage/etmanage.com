using ET.Constant.DBConst;
using ET.Sys_DEF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Web.Controllers
{
    public class DesignController : WebControllerBase
    {
        //
        // GET: /Design/

        public void _LayoutBlog()
        {
            List<KeyAndValue> listType = new ET.Sys_BLL.PublicBLL().GetNestListByCondition("TYPEID id,TypeUrl reserve1,TypeKey reserve3,TypeLevel reserve2,TYPENAME text,TYPEPID pid", TableNames.BlogTypeInfo, " AND Status=1 ", "TYPESORT DESC");
            ViewBag.listBlogType = listType;

            List<NewInfo> listnewestNew = new ET.Sys_BLL.NewsBLL().List_NewInfo(" top 1 newid,newtitle", "AND Status=1 AND TYPEID=(SELECT TOP 1 TYPEID FROM " + TableNames.NewTypeInfo + " WHERE TYPEKEY='notice')", " CreateTime desc");
            ViewBag.listNewestNew = listnewestNew;

        }
        public ActionResult Index(string q,string page)
        {

            string strCondition = "";
            if (!string.IsNullOrEmpty(q))
                strCondition = " AND CHARINDEX('" + q + "', ArticleTitle)>0";
            int pageIndex = 1;
            if (this.CheckInfoInt(page))
                pageIndex = int.Parse(page);
            int pageSize = 15;
            long RecordTotalCount = 0;
            List<DesignGoodInfo> list = new ET.Sys_BLL.DesignBLL().PageList_DesignGoodInfo("GoodID,GoodUrl,GoodName,GoodPicture,GoodDescription,CreateTime,ACCESSCOUNT,TYPEID", " AND STATUS=1 ", "CreateTime desc", pageIndex, pageSize, ref RecordTotalCount);
            ViewBag.DesignGoodInfo = list;


            _GetListPager(pageIndex, pageSize, RecordTotalCount);
            return View();
    
        }
        public ActionResult Detail(string id)
        {
            DesignGoodInfo info = new ET.Sys_BLL.DesignBLL().Get_DesignGoodInfoByID(id);
            if (info != null)
                ViewBag.DesignGoodInfo = info;
            else
                ViewBag.DesignGoodInfo = new DesignGoodInfo();
            return View();
        }

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
    }
}
