using ET.Sys_DEF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Web.Areas.Manage.Controllers
{
    public class OrganizationController : ManageControllerBase
    {
        //
        // GET: /Organization/


        #region 公司管理
        public ActionResult CompanyManage()
        {
            return View();
        }

        [HttpPost]
        public JsonResult AjaxQueryCompanyList(string id)
        {
            List<KeyAndValue> list = new ET.Sys_BLL.PublicBLL().GetNestListByCondition("CompanyID id,CompanyName text,CompanyPID pid",ET.Constant.DBConst.TableNames.UserCompanyInfo, null, "CompanySort DESC");
            return Json(list, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult AjaxSaveCompany(FormCollection collection, string infoid)
        {
            bool IsInsert = false;
            string strResult = "false";
            UserCompanyInfo info = new ET.Sys_BLL.OrganizationBLL().Get_UserCompanyInfo(" AND CompanyID='" + infoid + "'");
            if (info == null)
            {
                info = new UserCompanyInfo();
            IsInsert=true;
            }
            if (!string.IsNullOrEmpty( collection["CompanyPID"]))
            info.CompanyPID = collection["CompanyPID"];
            else
            info.CompanyPID="-1";
            info.CompanyName = collection["CompanyName"];
            info.CompanySort = collection["CompanySort"];
            info.CompanyDescription = collection["CompanyDescription"];
            if (new ET.Sys_BLL.OrganizationBLL().Operate_UserCompanyInfo(info, IsInsert))
                strResult = "true";
            return Content(strResult);
        }
        [HttpGet]
        public JsonResult AjaxGetCompanyDetail(string infoid)
        {
            if (string.IsNullOrEmpty(infoid))
                return Json("error", JsonRequestBehavior.AllowGet);
            UserCompanyInfo info = new ET.Sys_BLL.OrganizationBLL().Get_UserCompanyInfo(" AND CompanyID='" + infoid + "'");
            if (info == null)
                return Json("error", JsonRequestBehavior.AllowGet);
            return Json(info, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult AjaxDeleteCompany(string infoid)
        {
            if (!string.IsNullOrEmpty(infoid) && new ET.Sys_BLL.OrganizationBLL().Delete_UserCompanyInfo(" AND CompanyID='" + infoid + "'"))
                return Content("true");
            else
                return Content("false");
        }

        #endregion


        #region 部门管理
        public ActionResult DepartmentManage()
        {
            return View();
        }

        [HttpPost]
        public JsonResult AjaxQueryDepartmentList(string id)
        {
            List<KeyAndValue> list = new ET.Sys_BLL.PublicBLL().GetNestListByCondition("DepID id,DepName text,DepPID pid", ET.Constant.DBConst.TableNames.UserDepartmentInfo, null, "DepSort DESC");
            return Json(list, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult AjaxSaveDepartment(FormCollection collection, string infoid)
        {
            bool IsInsert = false;
            string strResult = "false";
            UserDepartmentInfo info = new ET.Sys_BLL.OrganizationBLL().Get_UserDepartmentInfo(" AND DepID='" + infoid + "'");
            if (info == null)
            {
                info = new UserDepartmentInfo();
                IsInsert = true;
            }
            if (!string.IsNullOrEmpty(collection["DepPID"]))
                info.DepPID = collection["DepPID"];
            else
                info.DepPID = "-1";
            info.DepName = collection["DepName"];
            info.DepSort = collection["DepSort"];
            info.DepDescription = collection["DepDescription"];
            if (new ET.Sys_BLL.OrganizationBLL().Operate_UserDepartmentInfo(info, IsInsert))
                strResult = "true";
            return Content(strResult);
        }
        [HttpGet]
        public JsonResult AjaxGetDepartmentDetail(string infoid)
        {
            if (string.IsNullOrEmpty(infoid))
                return Json("error", JsonRequestBehavior.AllowGet);
            UserDepartmentInfo info = new ET.Sys_BLL.OrganizationBLL().Get_UserDepartmentInfo(" AND DepID='" + infoid + "'");
            if (info == null)
                return Json("error", JsonRequestBehavior.AllowGet);
            return Json(info, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult AjaxDeleteDepartment(string infoid)
        {
            if (!string.IsNullOrEmpty(infoid) && new ET.Sys_BLL.OrganizationBLL().Delete_UserCompanyInfo(" AND DepID='" + infoid + "'"))
                return Content("true");
            else
                return Content("false");
        }

        #endregion


        #region 岗位管理
        public ActionResult PositionManage()
        {
            return View();
        }

        [HttpPost]
        public JsonResult AjaxQueryPositionList(string id)
        {
            List<KeyAndValue> list = new ET.Sys_BLL.PublicBLL().GetNestListByCondition("PostID id,PostName text,PostPID pid", ET.Constant.DBConst.TableNames.UserPositionInfo, null, "PostSort DESC");
            return Json(list, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult AjaxSavePosition(FormCollection collection, string infoid)
        {
            bool IsInsert = false;
            string strResult = "false";
            UserPositionInfo info = new ET.Sys_BLL.OrganizationBLL().Get_UserPositionInfo(" AND PostID='" + infoid + "'");
            if (info == null)
            {
                info = new UserPositionInfo();
                IsInsert = true;
            }
            info.PostName = collection["PostName"];
            info.PostSort = collection["PostSort"];
            info.PostDescription = collection["PostDescription"];
            if (new ET.Sys_BLL.OrganizationBLL().Operate_UserPositionInfo(info, IsInsert))
                strResult = "true";
            return Content(strResult);
        }
        [HttpGet]
        public JsonResult AjaxGetPositionDetail(string infoid)
        {
            if (string.IsNullOrEmpty(infoid))
                return Json("error", JsonRequestBehavior.AllowGet);
            UserPositionInfo info = new ET.Sys_BLL.OrganizationBLL().Get_UserPositionInfo(" AND PostID='" + infoid + "'");
            if (info == null)
                return Json("error", JsonRequestBehavior.AllowGet);
            return Json(info, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult AjaxDeletePosition(string infoid)
        {
            if (!string.IsNullOrEmpty(infoid) && new ET.Sys_BLL.OrganizationBLL().Delete_UserPositionInfo(" AND PostID='" + infoid + "'"))
                return Content("true");
            else
                return Content("false");
        }

        #endregion

        #region 用户管理
        public ActionResult UserQuery()
        {
            return View();
        }
        public ActionResult UserManage()
        {
            return View();
        }
        [HttpGet]
        public JsonResult AjaxQueryUserPageList()
        {
            //接收datagrid传来的参数 
            int pageIndex = int.Parse(Request["page"]);
            int pageSize = int.Parse(Request["rows"]);
            long RecordTotalCount = 0;
            List<UserPropertyInfo> list = new ET.Sys_BLL.OrganizationBLL().PageList_UserPropertyInfo("UserID,CNName,Sex,Age", null, "CreateTime desc", pageIndex, pageSize, ref RecordTotalCount);
            return Json(new { total = RecordTotalCount, rows = list }, JsonRequestBehavior.AllowGet);
        }
        public JsonResult AjaxGetCompanySelectData()
        {
            List<KeyAndValue> list = new ET.Sys_BLL.PublicBLL().GetListByCondition<KeyAndValue>("CompanyID id,CompanyNAME text", ET.Constant.DBConst.TableNames.UserCompanyInfo, null, "CompanySORT");
            return Json(list, JsonRequestBehavior.AllowGet);
        }
        public JsonResult AjaxGetDepartmentSelectData()
        {
            List<KeyAndValue> list = new ET.Sys_BLL.PublicBLL().GetNestListByCondition("DepID id,DepNAME text,DepPID pid", ET.Constant.DBConst.TableNames.UserDepartmentInfo, null, "DepSORT");
            return Json(list, JsonRequestBehavior.AllowGet);
        }
        public JsonResult AjaxGetPositionSelectData()
        {
            List<KeyAndValue> list = new ET.Sys_BLL.PublicBLL().GetListByCondition<KeyAndValue>("PostID id,PostNAME text", ET.Constant.DBConst.TableNames.UserPositionInfo, null, "PostSORT");
            return Json(list, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult AjaxSaveUser(FormCollection collection, string infoid)
        {
            string strResult = "false";
            UserPropertyInfo info = new ET.Sys_BLL.OrganizationBLL().Get_UserPropertyInfoByID(infoid);
            if (info == null)
            {
                info = new UserPropertyInfo();
                info.UserID = Guid.NewGuid();
                info.CreateTime = DateTime.Now;
            }

            if (new ET.Sys_BLL.OrganizationBLL().Operate_UserPropertyInfo(info))
                strResult = "true";
            return Content(strResult);
        }
        [HttpGet]
        public JsonResult AjaxGetUserDetail(string infoid)
        {
            if (string.IsNullOrEmpty(infoid))
                return Json("", JsonRequestBehavior.AllowGet);
            UserPropertyInfo info = new ET.Sys_BLL.OrganizationBLL().Get_UserPropertyInfoByID(infoid);
            if (info == null)
                return Json("error", JsonRequestBehavior.AllowGet);
            return Json(info, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult AjaxDeleteUser(string infoid)
        {
            if (!string.IsNullOrEmpty(infoid) && new ET.Sys_BLL.OrganizationBLL().Delete_User_Info(infoid))
                return Content("true");
            else
                return Content("false");
        }
        [HttpGet]
        public ActionResult AjaxSearchUser(string query)
        {
            List<UserPropertyInfo> list = new ET.Sys_BLL.OrganizationBLL().List_UserPropertyInfo("CNName,Nickname", " AND ( CHARINDEX('" + query + "', CNName)>0 or CHARINDEX('" + query + "', Nickname)>0)", "CreateTime desc");
            var arrData = list.Select(c => c.CNName+"("+c.Nickname+")");
            return Json(new { query = query, suggestions = arrData, data = arrData }, JsonRequestBehavior.AllowGet);
        }
        #endregion
    }
}
