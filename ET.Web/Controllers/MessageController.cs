using ET.Sys_DEF;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ET.Web.Controllers
{
    public class MessageController : WebControllerBase
    {
        //
        // GET: /Message/
        public  void _GetListPager(int pageIndex, int pageSize, long RecordTotalCount)
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
            if (base.IsNumeric(page))
                pageIndex = int.Parse(page);
            int pageSize = 10;
            long RecordTotalCount = 0;
            List<BlogMessageInfo> listMessage = new ET.Sys_BLL.BlogBLL().Pagination_BlogMessageInfo("*", "AND Status=1 " + strCondition, "CreateTime desc", pageIndex, pageSize, ref RecordTotalCount);
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

        public ActionResult PageOk()
        {return View();
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
            if (new ET.Sys_BLL.BlogBLL().Update_BlogMessageInfo(info, true))
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
            info.Cover = collection["Cover"];
          

            info.CreateTime = DateTime.Now;
            info.Status = 0;
            if (new ET.Sys_BLL.BlogBLL().Update_BlogPublish(info, true))
            {
                return Content("true");
                
            }
            else
                return Content("error");

        }
        [AcceptVerbs(HttpVerbs.Post)]
        public JsonResult Upload(HttpPostedFileBase fileData)
        {
            if (fileData != null)
            {
                try
                {
                    // 文件上传后的保存路径
                    string filePath = Server.MapPath("~/Upload/blog/webimage/");
                    if (!Directory.Exists(filePath))
                    {
                        Directory.CreateDirectory(filePath);
                    }
                    string fileName = Path.GetFileName(fileData.FileName);// 原始文件名称
                    string fileExtension = Path.GetExtension(fileName); // 文件扩展名
                    string saveName = DateTime.Now.ToString("yyyyMMddHHmmssfff") + fileExtension; // 保存文件名称

                    fileData.SaveAs(filePath + saveName);

                    return Json(new { Success = true, FileName = fileName, SaveName = "/upload/blog/webimage/" + saveName });
                }
                catch (Exception ex)
                {
                    return Json(new { Success = false, Message = ex.Message }, JsonRequestBehavior.AllowGet);
                }
            }
            else
            {

                return Json(new { Success = false, Message = "请选择要上传的文件！" }, JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult DeleteUploadPic(string Url)
        {
            try
            {
                if (!string.IsNullOrEmpty(Url))
                    System.IO.File.Delete(Server.MapPath("~") + Server.UrlDecode(Url));
                return Content("true");
            }
            catch { return Content("error"); }

        }
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
        [HttpPost]
        public ActionResult AjaxCheckValidatecode(string code)
        {
            string resultMsg = "true";
            string pCode = code;
            string sCode = "";
            object obj = ET.ToolKit.ToolKit.Common.SessionHelper.Get(SystemConfigConst.SessioncheckValiCode);
            if (obj != null)
                sCode = obj.ToString();
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
    }
}
