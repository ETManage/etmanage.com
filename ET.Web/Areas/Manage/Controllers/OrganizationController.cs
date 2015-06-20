using ET.Constant.DBConst;
using ET.Sys_DEF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Web.Areas.Manage.Controllers
{
    [UserAuthorize]
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
            List<KeyAndValue> list = new ET.Sys_BLL.PublicBLL().GetNestListByCondition("CompanyID id,CompanyName text,CompanyPID pid", ET.Constant.DBConst.TableNames.UserCompany, null, "CompanySort DESC");
            return Json(list, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult AjaxSaveCompany(FormCollection collection, string id)
        {
            bool IsInsert = false;
            string strResult = "false";
            UserCompany info = new ET.Sys_BLL.OrganizationBLL().Get_UserCompany(" AND CompanyID='" + id + "'");
            if (info == null)
            {
                info = new UserCompany();
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
            if (new ET.Sys_BLL.OrganizationBLL().Operate_UserCompany(info, IsInsert))
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
            UserCompany info = new ET.Sys_BLL.OrganizationBLL().Get_UserCompany(" AND CompanyID='" + id + "'");
            if (info == null)
                return Json("error", JsonRequestBehavior.AllowGet);
            return Json(info, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult AjaxDeleteCompany(string id)
        {
            if (!string.IsNullOrEmpty(id) && new ET.Sys_BLL.OrganizationBLL().Delete_UserCompany(" AND CompanyID='" + id + "'"))
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
            List<KeyAndValue> list = new ET.Sys_BLL.PublicBLL().GetNestListByCondition("DepID id,DepName text,DepPID pid", ET.Constant.DBConst.TableNames.UserDepartment, null, "DepSort DESC");
            return Json(list, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult AjaxSaveDepartment(FormCollection collection, string id, string cid, string pid, string ActionIDS)
        {
            bool IsInsert = false;
            string strResult = "false";
            UserDepartment info = new ET.Sys_BLL.OrganizationBLL().Get_UserDepartment(" AND DepID='" + id + "'");
            if (info == null)
            {
                info = new UserDepartment();
                info.CreateTime = DateTime.Now;
                info.CreatorID = this.UserID;
                IsInsert = true;
            }

            info.DepPID = pid;
            info.CompanyID = cid;
            info.DepName = collection["DepName"];
            info.DepSort = collection["DepSort"];
            info.DepDescription = collection["DepDescription"];
            if (new ET.Sys_BLL.OrganizationBLL().Operate_UserDepartment(info, IsInsert))
            {
                id = info.DepID.ToString();
                List<string> aIDS = null;
                if (!string.IsNullOrEmpty(ActionIDS))
                {
                    aIDS = ET.ToolKit.Common.JsonSerializeHelper.DeserializeFromJson<List<string>>(ActionIDS);
                }
                new ET.Sys_BLL.OrganizationBLL().Delete_UserDeptFuncLink(string.Format("AND DepID='{0}'", id));
                foreach (string item in aIDS)
                {
                    UserDeptFuncLink ainfo = new UserDeptFuncLink();
                    ainfo.FuncID = Guid.Parse(item);
                    ainfo.DepID = info.DepID;
                    ainfo.CreateTime = DateTime.Now;
                    ainfo.CreatorID = Guid.Parse(this.UserID);
                    new ET.Sys_BLL.OrganizationBLL().Operate_UserDeptFuncLink(ainfo, true);
                }

                strResult = "true";
            }
            return Json(new { result = strResult, id = id }, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public JsonResult AjaxGetDepartmentDetail(string id)
        {
            if (string.IsNullOrEmpty(id))
                return Json("error", JsonRequestBehavior.AllowGet);
            UserDepartment info = new ET.Sys_BLL.OrganizationBLL().Get_UserDepartment(" AND DepID='" + id + "'");
            if (info == null)
                return Json("error", JsonRequestBehavior.AllowGet);
            return Json(info, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult AjaxDeleteDepartment(string id)
        {
            if (!string.IsNullOrEmpty(id) && new ET.Sys_BLL.OrganizationBLL().Delete_UserCompany(" AND DepID='" + id + "'"))
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
            List<KeyAndValue> list = new ET.Sys_BLL.PublicBLL().GetNestListByCondition("PostID id,PostName text,PostPID pid", ET.Constant.DBConst.TableNames.UserPosition, null, "PostSort DESC");
            return Json(list, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult AjaxSavePosition(FormCollection collection, string id, string cid, string did, string ActionIDS)
        {
            bool IsInsert = false;
            string strResult = "false";
            UserPosition info = new ET.Sys_BLL.OrganizationBLL().Get_UserPosition(" AND PostID='" + id + "'");
            if (info == null)
            {
                info = new UserPosition();
                info.CreateTime = DateTime.Now;
                info.CreatorID = this.UserID;
                IsInsert = true;
            }
            info.CompanyID = cid;
            info.DepID = did;
            info.PostName = collection["PostName"];
            info.PostSort = collection["PostSort"];
            info.PostDescription = collection["PostDescription"];
            if (new ET.Sys_BLL.OrganizationBLL().Operate_UserPosition(info, IsInsert))
            {
                id = info.PostID.ToString();
                List<string> aIDS = null;
                if (!string.IsNullOrEmpty(ActionIDS))
                {
                    aIDS = ET.ToolKit.Common.JsonSerializeHelper.DeserializeFromJson<List<string>>(ActionIDS);
                }
                new ET.Sys_BLL.OrganizationBLL().Delete_UserPostFuncLink(string.Format("AND PostID='{0}'", id));
                foreach (string item in aIDS)
                {
                    UserPostFuncLink ainfo = new UserPostFuncLink();
                    ainfo.FuncID = Guid.Parse(item);
                    ainfo.PostID = info.PostID;
                    ainfo.CreateTime = DateTime.Now;
                    ainfo.CreatorID = Guid.Parse(this.UserID);
                    new ET.Sys_BLL.OrganizationBLL().Operate_UserPostFuncLink(ainfo, true);
                }

                strResult = "true";
            }
            return Json(new { result = strResult, id = id }, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public JsonResult AjaxGetPositionDetail(string id)
        {
            if (string.IsNullOrEmpty(id))
                return Json("error", JsonRequestBehavior.AllowGet);
            UserPosition info = new ET.Sys_BLL.OrganizationBLL().Get_UserPosition(" AND PostID='" + id + "'");
            if (info == null)
                return Json("error", JsonRequestBehavior.AllowGet);
            return Json(info, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult AjaxDeletePosition(string id)
        {
            if (!string.IsNullOrEmpty(id) && new ET.Sys_BLL.OrganizationBLL().Delete_UserPosition(" AND PostID='" + id + "'"))
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
            List<TreeModuleInfo> listc = new ET.Sys_BLL.PublicBLL().GetListByCondition<TreeModuleInfo>(string.Format("CompanyID id,CompanyName text,CompanyPID pid,'icon-company' iconCls,case when exists(select 1 from {0} c where c.CompanyID=CAST({1}.CompanyID as varchar(36))) then  'closed' else  'open' end state", TableNames.UserDepartment, TableNames.UserCompany), TableNames.UserCompany, null, "CompanySort DESC");
            for (int i = 0; i < listc.Count; i++)
            {
                string condition = " AND  ISNULL(DepPID,'-1')='-1' AND CompanyID='" + listc[i].id + "' ";
                List<TreeModuleInfo> Outlist = new List<TreeModuleInfo>();
                List<TreeModuleInfo> list = new ET.Sys_BLL.PublicBLL().GetNestTreeByCondition(string.Format("DepID id,DepName text,DepPID pid,'icon-dept' iconCls,case when exists(select 1 from {0} c where c.DepPID=CAST({0}.DEPID as varchar(36))) then  'closed' else  'open' end state", TableNames.UserDepartment), TableNames.UserDepartment, condition, "DepSort DESC");
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
                List<TreeModuleInfo> list = new ET.Sys_BLL.PublicBLL().GetListByCondition<TreeModuleInfo>("PostID id,PostName text,'icon-post' iconCls,'open' state", TableNames.UserPosition, condition, "PostSort DESC");

                TreeModuleInfo info = item;
                List<TreeModuleInfo> children = new List<TreeModuleInfo>();
                children.AddRange(list);
                NestOrgRecursion(Alllist, item.id, children);

                info.children = children;
                Outlist.Add(info);
            }

        }


        [HttpPost]
        public JsonResult AjaxQueryAllCompanyList(string id)
        {
            string condition = " AND  CompanyPID='-1' ";
            if (!string.IsNullOrEmpty(id))
                condition = " AND  CompanyPID='" + id + "' ";
            List<TreeModuleInfo> list = new ET.Sys_BLL.PublicBLL().GetListByCondition<TreeModuleInfo>(string.Format("CompanyID id,CompanyName text,CompanyPID pid,'icon-company' iconCls,case when exists(select 1 from {0} c where c.CompanyID=CAST({0}.CompanyID as varchar(36))) then  'closed' else  'open' end state", TableNames.UserCompany), TableNames.UserCompany, null, "CompanySort DESC");
            return Json(list, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult AjaxQueryAllDeptList()
        {
            List<TreeModuleInfo> listc = new ET.Sys_BLL.PublicBLL().GetListByCondition<TreeModuleInfo>(string.Format("CompanyID id,CompanyName text,CompanyPID pid,'icon-company' iconCls,case when exists(select 1 from {0} c where c.CompanyID=CAST({1}.CompanyID as varchar(36))) then  'closed' else  'open' end state", TableNames.UserDepartment, TableNames.UserCompany), TableNames.UserCompany, null, "CompanySort DESC");
            for (int i = 0; i < listc.Count; i++)
            {
                string condition = "  AND CompanyID='" + listc[i].id + "' ";
                List<TreeModuleInfo> list = new ET.Sys_BLL.PublicBLL().GetNestTreeByCondition(string.Format("DepID id,DepName text,DepPID pid,'icon-dept' iconCls,case when exists(select 1 from {0} c where c.DepPID=CAST({0}.DEPID as varchar(36))) then  'closed' else  'open' end state", TableNames.UserDepartment), ET.Constant.DBConst.TableNames.UserDepartment, condition, "DepSort DESC");
                listc[i].children = list;
            }

            return Json(listc, JsonRequestBehavior.AllowGet);
        }
        public JsonResult AjaxQueryAllPostList()
        {
            List<TreeModuleInfo> listc = new ET.Sys_BLL.PublicBLL().GetListByCondition<TreeModuleInfo>(string.Format("CompanyID id,CompanyName text,CompanyPID pid,'icon-company' iconCls,case when exists(select 1 from {0} c where c.CompanyID=CAST({1}.CompanyID as varchar(36))) then  'closed' else  'open' end state", TableNames.UserDepartment, TableNames.UserCompany), ET.Constant.DBConst.TableNames.UserCompany, null, "CompanySort DESC");
            for (int i = 0; i < listc.Count; i++)
            {
                string condition = " AND  ISNULL(DepPID,'-1')='-1' AND CompanyID='" + listc[i].id + "' ";
                List<TreeModuleInfo> Outlist = new List<TreeModuleInfo>();
                List<TreeModuleInfo> list = new ET.Sys_BLL.PublicBLL().GetNestTreeByCondition(string.Format("DepID id,DepName text,DepPID pid,'icon-dept' iconCls,case when exists(select 1 from {0} c where c.DepPID=CAST({0}.DEPID as varchar(36))) then  'closed' else  'open' end state", TableNames.UserDepartment), ET.Constant.DBConst.TableNames.UserDepartment, condition, "DepSort DESC");
                NestPostRecursion(list, "-1", Outlist);
                listc[i].children = list;
            }
            return Json(listc, JsonRequestBehavior.AllowGet);

        }

        public JsonResult AjaxQueryAllUserList()
        {


            List<TreeModuleInfo> listc = new ET.Sys_BLL.PublicBLL().GetListByCondition<TreeModuleInfo>(string.Format("CompanyID id,CompanyName text,CompanyPID pid,'icon-company' iconCls,case when exists(select 1 from {0} c where c.CompanyID=CAST({1}.CompanyID as varchar(36))) then  'closed' else  'open' end state", TableNames.UserDepartment, TableNames.UserCompany), ET.Constant.DBConst.TableNames.UserCompany, null, "CompanySort DESC");
            for (int i = 0; i < listc.Count; i++)
            {
                string condition = " AND  ISNULL(DepPID,'-1')='-1' AND CompanyID='" + listc[i].id + "' ";
                List<TreeModuleInfo> Outlist = new List<TreeModuleInfo>();
                List<TreeModuleInfo> list = new ET.Sys_BLL.PublicBLL().GetNestTreeByCondition(string.Format("DepID id,DepName text,DepPID pid,'icon-dept' iconCls,case when exists(select 1 from {0} c where c.DepPID=CAST({0}.DEPID as varchar(36))) then  'closed' else  'open' end state", TableNames.UserDepartment), ET.Constant.DBConst.TableNames.UserDepartment, condition, "DepSort DESC");
                NestUserRecursion(list, "-1", Outlist);
                listc[i].children = list;
            }
            return Json(listc, JsonRequestBehavior.AllowGet);
        }
        void NestUserRecursion(List<TreeModuleInfo> Alllist, string PID, List<TreeModuleInfo> Outlist)
        {
            foreach (TreeModuleInfo item in Alllist.Where(info => info.pid == PID))
            {
                List<TreeModuleInfo> list = new ET.Sys_BLL.PublicBLL().GetListBySql<TreeModuleInfo>(string.Format("SELECT A.UserID id,A.CNName text,'icon-person' iconCls,'open'  state FROM {0} A INNER JOIN {1} B ON A.USERID=B.USERID WHERE B.TYPE='dept' order by A.createtime", ET.Constant.DBConst.TableNames.UserProperty, ET.Constant.DBConst.TableNames.UserOrgLink));
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
                List<TreeModuleInfo> list = new ET.Sys_BLL.PublicBLL().GetListByCondition<TreeModuleInfo>("PostID id,PostName text,'icon-post' iconCls,'open' state", ET.Constant.DBConst.TableNames.UserPosition, condition, "PostSort DESC");

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
            ViewBag.ListSysRole = new ET.Sys_BLL.SystemBLL().List_SysRole("*", null, null);


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
                Condition += " AND (CHARINDEX('" + Request["name"] + "', CNName)>0 or CHARINDEX('" + Request["name"] + "', UserName)>0) ";
            if (!string.IsNullOrEmpty(Request["sex"]))
                Condition += " AND SEX='" + Request["sex"] + "'";
            long RecordTotalCount = 0;
            List<UserFullProperty> list = new ET.Sys_BLL.OrganizationBLL().PageList_UserFullProperty("UserID,CNName,mobile,Sex,Age,CreateTime,Source,livearea+livecity+liveprovince livearea,Status, cast(Exp as varchar(20))+'['+UserLevel+']' UserLevel", Condition, "CreateTime desc", pageIndex, pageSize, ref RecordTotalCount);
            return Json(new { total = RecordTotalCount, rows = list }, JsonRequestBehavior.AllowGet);
        }
        public JsonResult AjaxGetCompanySelectData()
        {
            List<KeyAndValue> list = new ET.Sys_BLL.PublicBLL().GetListByCondition<KeyAndValue>("CompanyID id,CompanyNAME text", ET.Constant.DBConst.TableNames.UserCompany, null, "CompanySORT");
            return Json(list, JsonRequestBehavior.AllowGet);
        }
        public JsonResult AjaxGetDepartmentSelectData()
        {
            List<KeyAndValue> list = new ET.Sys_BLL.PublicBLL().GetNestListByCondition("DepID id,DepNAME text,DepPID pid", ET.Constant.DBConst.TableNames.UserDepartment, null, "DepSORT");
            return Json(list, JsonRequestBehavior.AllowGet);
        }
        public JsonResult AjaxGetPositionSelectData()
        {
            List<KeyAndValue> list = new ET.Sys_BLL.PublicBLL().GetListByCondition<KeyAndValue>("PostID id,PostNAME text", ET.Constant.DBConst.TableNames.UserPosition, null, "PostSORT");
            return Json(list, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult AjaxSaveUser(FormCollection collection, string infoid, string ActionIDS, string RoleIDs)
        {
            string strResult = "false";
            UserFullInfo info = new ET.Sys_BLL.OrganizationBLL().Get_User_Info(infoid);
            bool IsInsert = false;
            if (info == null)
            {
                IsInsert = true;
                UserBase baseinfo = new UserBase();
                baseinfo.UserID = Guid.NewGuid();
                baseinfo.UserName = collection["UserName"];
                baseinfo.UserPwd = ET.ToolKit.Encrypt.EncrypeHelper.EncryptMD5("123456");
                info.userbaseinfo = baseinfo;
                UserProperty propertyinfo = new UserProperty();
                propertyinfo.CNName = propertyinfo.Nickname = collection["CNName"];
                propertyinfo.Sex = collection["Sex"];
                propertyinfo.Source = "后台添加";
                propertyinfo.CreateTime = DateTime.Now;
                propertyinfo.EMail = collection["EMail"];
                propertyinfo.Mobile = collection["Mobile"];
                info.userstuinfo = propertyinfo;
            }
            if (!string.IsNullOrEmpty(collection["Status"]) && collection["Status"] == "on")
            {
                info.userbaseinfo.Status = 1;
            }
            if (!string.IsNullOrEmpty(collection["IsReset"]) && collection["IsReset"] == "on")
            {
                info.userbaseinfo.UserPwd = ET.ToolKit.Encrypt.EncrypeHelper.EncryptMD5("123456");
            }
            List<UserRoleLink> listRole = new List<UserRoleLink>();
            if (!string.IsNullOrEmpty(RoleIDs))
            {
                foreach (string item in RoleIDs.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries))
                {
                    listRole.Add(new UserRoleLink() { RoleID = Guid.Parse(item) });

                }
                info.userrole = listRole;
            }



            if (new ET.Sys_BLL.OrganizationBLL().Operate_User_Info(info, IsInsert))
            {
                List<string> aIDS = null;
                if (!string.IsNullOrEmpty(ActionIDS))
                {
                    aIDS = ET.ToolKit.Common.JsonSerializeHelper.DeserializeFromJson<List<string>>(ActionIDS);
                }
                new ET.Sys_BLL.OrganizationBLL().Delete_UserFuncLink(string.Format("AND UserID='{0}'", info.userbaseinfo.UserID));
                foreach (string item in aIDS)
                {

                    UserFuncLink ainfo = new UserFuncLink();
                    ainfo.FuncID = Guid.Parse(item);
                    ainfo.UserID = info.userbaseinfo.UserID;
                    ainfo.CreateTime = DateTime.Now;
                    ainfo.CreatorID = Guid.Parse(this.UserID);
                    new ET.Sys_BLL.OrganizationBLL().Operate_UserFuncLink(ainfo, true);
                }
                strResult = "true";
            }
            return Content(strResult);
        }
        [HttpPost]
        public JsonResult AjaxGetAllFunctionData(string t,string id, string uid)
        {
            string ischecked = "";
            if (!string.IsNullOrEmpty(uid))
                switch (t)
                {
                    case "dept":
                        ischecked = ",case when exists(select 1 from UserDeptFuncLink A where  DepID='" + uid + "' AND A.FUNCID=SysFunction.FUNCID) then 1 else 0 end checked";
                        break;
                    case "post":
                        ischecked = ",case when exists(select 1 from UserPostFuncLink A where  PostID='" + uid + "' AND A.FUNCID=SysFunction.FUNCID) then 1 else 0 end checked";
                        break;
                    default:
                        ischecked = ",case when exists(select 1 from V_ALLUSERLIMIT A where  USERID='" + uid + "' AND A.FUNCID=SysFunction.FUNCID) then 1 else 0 end checked";
                        break;

                }
            //string condition = " AND  FuncPID='-1' ";
            //if (!string.IsNullOrEmpty(id))
            //    condition = " AND  FuncPID='" + id + "' ";
            List<TreeModuleInfo> list = new ET.Sys_BLL.PublicBLL().GetNestTreeByCondition("FuncID id,FuncNAME text,FuncPID pid,case when functype=1 then 'icon-company' when functype=-1 then 'icon-children' else 'icon-group' end iconCls,case when exists(select 1 from SysFunction c where c.funcpid=CAST(SysFunction.FuncID as varchar(36))) then 'closed' else 'open' end state" + ischecked, ET.Constant.DBConst.TableNames.SysFunction, null, "FuncSORT  DESC");

            return Json(list, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public JsonResult AjaxGetUserDetail(string infoid)
        {
            if (string.IsNullOrEmpty(infoid))
                return Json("", JsonRequestBehavior.AllowGet);
            UserFullProperty info = new ET.Sys_BLL.OrganizationBLL().Get_UserFullPropertyByID(infoid);
            if (info == null)
                return Json("error", JsonRequestBehavior.AllowGet);
            return Json(info, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult AjaxDeleteUser(string ids)
        {
            if (!string.IsNullOrEmpty(ids) && new ET.Sys_BLL.OrganizationBLL().Delete_User_Info(" AND USERID IN (" + ids + ")"))
                return Content("true");
            else
                return Content("false");
        }
        [HttpGet]
        public ActionResult AjaxSearchUser(string query)
        {
            List<UserProperty> list = new ET.Sys_BLL.OrganizationBLL().List_UserProperty("CNName,Nickname", " AND ( CHARINDEX('" + query + "', CNName)>0 or CHARINDEX('" + query + "', Nickname)>0)", "CreateTime desc");
            var arrData = list.Select(c => c.CNName + "(" + c.Nickname + ")");
            return Json(new { query = query, suggestions = arrData, data = arrData }, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult AjaxDisableUser(string ids)
        {
            if (!string.IsNullOrEmpty(ids) && new ET.Sys_BLL.OrganizationBLL().Operate_DisableUser(" AND USERID IN (" + ids + ")"))
                return Content("true");
            else
                return Content("false");
        }
        [HttpPost]
        public ActionResult AjaxEnabledUser(string ids)
        {
            if (!string.IsNullOrEmpty(ids) && new ET.Sys_BLL.OrganizationBLL().Operate_EnabledUser(" AND USERID IN (" + ids + ")"))
                return Content("true");
            else
                return Content("false");
        }
        #endregion
    }
}
