using ET.Constant.DBConst;
using ET.Sys_DEF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ET.Web.Controllers
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
            if (base.IsNumeric(page))
                pageIndex = int.Parse(page);
            int pageSize = 15;
            long RecordTotalCount = 0;
            List<DesignGoodInfo> list = new ET.Sys_BLL.DesignBLL().Pagination_DesignGoodInfo("GoodID,GoodUrl,GoodName,GoodPicture,GoodDescription,CreateTime,ACCESSCOUNT,TYPEID", " AND STATUS=1 ", "CreateTime desc", pageIndex, pageSize, ref RecordTotalCount);
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

    }
}
