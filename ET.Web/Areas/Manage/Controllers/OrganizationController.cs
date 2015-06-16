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
            List<KeyAndValue> list = new ET.Sys_BLL.PublicBLL().GetNestListByCondition("CompanyID id,CompanyName text,CompanyPID pid", ET.Constant.DBConst.TableNames.UserCompanyInfo, null, "CompanySort DESC");
            return Json(list, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult AjaxSaveCompany(FormCollection collection, string id)
        {
            bool IsInsert = false;
            string strResult = "false";
            UserCompanyInfo info = new ET.Sys_BLL.OrganizationBLL().Get_UserCompanyInfo(" AND CompanyID='" + id + "'");
            if (info == null)
            {
                info = new UserCompanyInfo();
                info.CreateTime = DateTime.Now;
                info.CreatorID = this.UserID;
                IsInsert = true;
            }
            if (!string.IsNullOrEmpty(collection["CompanyPID"]))
                info.CompanyPID = collection["CompanyPID"];
            else
                info.CompanyPID = "-1";
            info.CompanyName = collection["CompanyName"];
            info.CompanySort = collection["CompanySort"];
            info.CompanyDescription = collection["CompanyDescription"];
            if (new ET.Sys_BLL.OrganizationBLL().Operate_UserCompanyInfo(info, IsInsert))
            {
                id = info.CompanyID.ToString();
                strResult = "true";
            }
            return Json(new { result = strResult, id = id }, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public JsonResult AjaxGetCompanyDetail(string id)
        {
            if (string.IsNullOrEmpty(id))
                return Json("error", JsonRequestBehavior.AllowGet);
            UserCompanyInfo info = new ET.Sys_BLL.OrganizationBLL().Get_UserCompanyInfo(" AND CompanyID='" + id + "'");
            if (info == null)
                return Json("error", JsonRequestBehavior.AllowGet);
            return Json(info, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult AjaxDeleteCompany(string id)
        {
            if (!string.IsNullOrEmpty(id) && new ET.Sys_BLL.OrganizationBLL().Delete_UserCompanyInfo(" AND CompanyID='" + id + "'"))
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
        public JsonResult AjaxSaveDepartment(FormCollection collection, string id, string cid, string pid)
        {
            bool IsInsert = false;
            string strResult = "false";
            UserDepartmentInfo info = new ET.Sys_BLL.OrganizationBLL().Get_UserDepartmentInfo(" AND DepID='" + id + "'");
            if (info == null)
            {
                info = new UserDepartmentInfo();
                info.CreateTime = DateTime.Now;
                info.CreatorID = this.UserID;
                IsInsert = true;
            }

            info.DepPID = pid;
            info.CompanyID = cid;
            info.DepName = collection["DepName"];
            info.DepSort = collection["DepSort"];
            info.DepDescription = collection["DepDescription"];
            if (new ET.Sys_BLL.OrganizationBLL().Operate_UserDepartmentInfo(info, IsInsert))
            {
                id = info.DepID.ToString();
                strResult = "true";
            }
            return Json(new { result = strResult, id = id }, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public JsonResult AjaxGetDepartmentDetail(string id)
        {
            if (string.IsNullOrEmpty(id))
                return Json("error", JsonRequestBehavior.AllowGet);
            UserDepartmentInfo info = new ET.Sys_BLL.OrganizationBLL().Get_UserDepartmentInfo(" AND DepID='" + id + "'");
            if (info == null)
                return Json("error", JsonRequestBehavior.AllowGet);
            return Json(info, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult AjaxDeleteDepartment(string id)
        {
            if (!string.IsNullOrEmpty(id) && new ET.Sys_BLL.OrganizationBLL().Delete_UserCompanyInfo(" AND DepID='" + id + "'"))
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
        public ActionResult AjaxSavePosition(FormCollection collection, string id, string cid, string did)
        {
            bool IsInsert = false;
            string strResult = "false";
            UserPositionInfo info = new ET.Sys_BLL.OrganizationBLL().Get_UserPositionInfo(" AND PostID='" + id + "'");
            if (info == null)
            {
                info = new UserPositionInfo();
                info.CreateTime = DateTime.Now;
                info.CreatorID = this.UserID;
                IsInsert = true;
            }
            info.CompanyID = cid;
            info.DepID = did;
            info.PostName = collection["PostName"];
            info.PostSort = collection["PostSort"];
            info.PostDescription = collection["PostDescription"];
            if (new ET.Sys_BLL.OrganizationBLL().Operate_UserPositionInfo(info, IsInsert))
            {
                id = info.PostID.ToString();
                strResult = "true";
            }
            return Json(new { result = strResult, id = id }, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public JsonResult AjaxGetPositionDetail(string id)
        {
            if (string.IsNullOrEmpty(id))
                return Json("error", JsonRequestBehavior.AllowGet);
            UserPositionInfo info = new ET.Sys_BLL.OrganizationBLL().Get_UserPositionInfo(" AND PostID='" + id + "'");
            if (info == null)
                return Json("error", JsonRequestBehavior.AllowGet);
            return Json(info, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult AjaxDeletePosition(string id)
        {
            if (!string.IsNullOrEmpty(id) && new ET.Sys_BLL.OrganizationBLL().Delete_UserPositionInfo(" AND PostID='" + id + "'"))
                return Content("true");
            else
                return Content("false");
        }

        #endregion

        

        #region 组织架构
        public ActionResult OrgQuery()
        {
            return View();
        }
        [HttpPost]
        public JsonResult AjaxQueryOrgList()
        {
            List<TreeModuleInfo> listc = new ET.Sys_BLL.PublicBLL().GetListByCondition<TreeModuleInfo>("CompanyID id,CompanyName text,CompanyPID pid,'icon-company' iconCls,case when exists(select 1 from UserDepartmentInfo c where c.CompanyID=CAST(UserCompanyInfo.CompanyID as varchar(36))) then  'closed' else  'open' end state", ET.Constant.DBConst.TableNames.UserCompanyInfo, null, "CompanySort DESC");
            for (int i = 0; i < listc.Count; i++)
            {
                string condition = " AND  ISNULL(DepPID,'-1')='-1' AND CompanyID='" + listc[i].id + "' ";
                List<TreeModuleInfo> Outlist = new List<TreeModuleInfo>();
                List<TreeModuleInfo> list = new ET.Sys_BLL.PublicBLL().GetNestTreeByCondition("DepID id,DepName text,DepPID pid,'icon-dept' iconCls,case when exists(select 1 from UserDepartmentInfo c where c.DepPID=CAST(UserDepartmentInfo.DEPID as varchar(36))) then  'closed' else  'open' end state", ET.Constant.DBConst.TableNames.UserDepartmentInfo, condition, "DepSort DESC");
                NestOrgRecursion(list, "-1", Outlist);
                listc[i].children = list;
            }
            return Json(listc, JsonRequestBehavior.AllowGet);

        }

        void NestOrgRecursion(List<TreeModuleInfo> Alllist, string PID, List<TreeModuleInfo> Outlist)
        {
            foreach (TreeModuleInfo item in Alllist.Where(info => info.pid == PID))
            {
                string condition = " AND  DepID='" + item.id + "' ";
                List<TreeModuleInfo> list = new ET.Sys_BLL.PublicBLL().GetListByCondition<TreeModuleInfo>("PostID id,PostName text,'icon-post' iconCls,'open' state", ET.Constant.DBConst.TableNames.UserPositionInfo, condition, "PostSort DESC");

                TreeModuleInfo info = item;
                List<TreeModuleInfo> children = new List<TreeModuleInfo>();
                children.AddRange(list);
                NestOrgRecursion(Alllist, item.id, children);

                info.children = children;
                Outlist.Add(info);
            }

        }



        public JsonResult AjaxQueryAllCompanyList(string id)
        {
            string condition = " AND  CompanyPID='-1' ";
            if (!string.IsNullOrEmpty(id))
                condition = " AND  CompanyPID='" + id + "' ";
            List<TreeModuleInfo> list = new ET.Sys_BLL.PublicBLL().GetListByCondition<TreeModuleInfo>("CompanyID id,CompanyName text,CompanyPID pid,'icon-company' iconCls,case when exists(select 1 from UserDepartmentInfo c where c.CompanyID=CAST(UserCompanyInfo.CompanyID as varchar(36))) then  'closed' else  'open' end state", ET.Constant.DBConst.TableNames.UserCompanyInfo, null, "CompanySort DESC");
            return Json(list, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult AjaxQueryAllDeptList()
        {
            List<TreeModuleInfo> listc = new ET.Sys_BLL.PublicBLL().GetListByCondition<TreeModuleInfo>("CompanyID id,CompanyName text,CompanyPID pid,'icon-company' iconCls,case when exists(select 1 from UserDepartmentInfo c where c.CompanyID=CAST(UserCompanyInfo.CompanyID as varchar(36))) then  'closed' else  'open' end state", ET.Constant.DBConst.TableNames.UserCompanyInfo, null, "CompanySort DESC");
            for (int i = 0; i < listc.Count; i++)
            {
                string condition = "  AND CompanyID='" + listc[i].id + "' ";
                List<TreeModuleInfo> list = new ET.Sys_BLL.PublicBLL().GetNestTreeByCondition("DepID id,DepName text,DepPID pid,'icon-dept' iconCls,case when exists(select 1 from UserDepartmentInfo c where c.DepPID=CAST(UserDepartmentInfo.DEPID as varchar(36))) then  'closed' else  'open' end state", ET.Constant.DBConst.TableNames.UserDepartmentInfo, condition, "DepSort DESC");
                listc[i].children = list;
            }

            return Json(listc, JsonRequestBehavior.AllowGet);
        }
        public JsonResult AjaxQueryAllPostList()
        {
            List<TreeModuleInfo> listc = new ET.Sys_BLL.PublicBLL().GetListByCondition<TreeModuleInfo>("CompanyID id,CompanyName text,CompanyPID pid,'icon-company' iconCls,case when exists(select 1 from UserDepartmentInfo c where c.CompanyID=CAST(UserCompanyInfo.CompanyID as varchar(36))) then  'closed' else  'open' end state", ET.Constant.DBConst.TableNames.UserCompanyInfo, null, "CompanySort DESC");
            for (int i = 0; i < listc.Count; i++)
            {
                string condition = " AND  ISNULL(DepPID,'-1')='-1' AND CompanyID='" + listc[i].id + "' ";
                List<TreeModuleInfo> Outlist = new List<TreeModuleInfo>();
                List<TreeModuleInfo> list = new ET.Sys_BLL.PublicBLL().GetNestTreeByCondition("DepID id,DepName text,DepPID pid,'icon-dept' iconCls,case when exists(select 1 from UserDepartmentInfo c where c.DepPID=CAST(UserDepartmentInfo.DEPID as varchar(36))) then  'closed' else  'open' end state", ET.Constant.DBConst.TableNames.UserDepartmentInfo, condition, "DepSort DESC");
                NestPostRecursion(list, "-1", Outlist);
                listc[i].children = list;
            }
            return Json(listc, JsonRequestBehavior.AllowGet);

        }

        public JsonResult AjaxQueryAllUserList()
        {


            List<TreeModuleInfo> listc = new ET.Sys_BLL.PublicBLL().GetListByCondition<TreeModuleInfo>("CompanyID id,CompanyName text,CompanyPID pid,'icon-company' iconCls,case when exists(select 1 from UserDepartmentInfo c where c.CompanyID=CAST(UserCompanyInfo.CompanyID as varchar(36))) then  'closed' else  'open' end state", ET.Constant.DBConst.TableNames.UserCompanyInfo, null, "CompanySort DESC");
            for (int i = 0; i < listc.Count; i++)
            {
                string condition = " AND  ISNULL(DepPID,'-1')='-1' AND CompanyID='" + listc[i].id + "' ";
                List<TreeModuleInfo> Outlist = new List<TreeModuleInfo>();
                List<TreeModuleInfo> list = new ET.Sys_BLL.PublicBLL().GetNestTreeByCondition("DepID id,DepName text,DepPID pid,'icon-dept' iconCls,case when exists(select 1 from UserDepartmentInfo c where c.DepPID=CAST(UserDepartmentInfo.DEPID as varchar(36))) then  'closed' else  'open' end state", ET.Constant.DBConst.TableNames.UserDepartmentInfo, condition, "DepSort DESC");
                NestUserRecursion(list, "-1", Outlist);
                listc[i].children = list;
            }
            return Json(listc, JsonRequestBehavior.AllowGet);
        }
        void NestUserRecursion(List<TreeModuleInfo> Alllist, string PID, List<TreeModuleInfo> Outlist)
        {
            foreach (TreeModuleInfo item in Alllist.Where(info => info.pid == PID))
            {
                List<TreeModuleInfo> list = new ET.Sys_BLL.PublicBLL().GetListBySql<TreeModuleInfo>("SELECT A.UserID id,A.CNName text,'icon-person' iconCls,'open'  state FROM UserPropertyInfo A INNER JOIN UserOrgLink B ON A.USERID=B.USERID WHERE B.TYPE='dept' order by A.createtime");
                TreeModuleInfo info = item;
                List<TreeModuleInfo> children = new List<TreeModuleInfo>();
                children.AddRange(list);
                NestUserRecursion(Alllist, item.id, children);

                info.children = children;
                Outlist.Add(info);
            }

        }
        void NestPostRecursion(List<TreeModuleInfo> Alllist, string PID, List<TreeModuleInfo> Outlist)
        {
            foreach (TreeModuleInfo item in Alllist.Where(info => info.pid == PID))
            {
                string condition = " AND  DepID='" + item.id + "' ";
                List<TreeModuleInfo> list = new ET.Sys_BLL.PublicBLL().GetListByCondition<TreeModuleInfo>("PostID id,PostName text,'icon-post' iconCls,'open' state", ET.Constant.DBConst.TableNames.UserPositionInfo, condition, "PostSort DESC");

                TreeModuleInfo info = item;
                List<TreeModuleInfo> children = new List<TreeModuleInfo>();
                children.AddRange(list);
                NestPostRecursion(Alllist, item.id, children);

                info.children = children;
                Outlist.Add(info);
            }

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
            string Condition = "";
            if (!string.IsNullOrEmpty(Request["name"]))
                Condition = " AND CHARINDEX('" + Request["name"] + "', CNName)>0";
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
            var arrData = list.Select(c => c.CNName + "(" + c.Nickname + ")");
            return Json(new { query = query, suggestions = arrData, data = arrData }, JsonRequestBehavior.AllowGet);
        }
        #endregion
    }
}
