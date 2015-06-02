using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;
using ET.Sys_DEF;
using ET.ToolKit.Common;

namespace Web.Areas.Manage.Controllers
{
    public class SystemController : ManageControllerBase
    {
        //
        // GET: /System/
        #region Default
        public ActionResult Default()
        {
            getSystemConfig();
            //左侧模块权限
            if (!ApplicationConfig.dirApplicationRoleConfig.Keys.Contains(this.CurrentUserInfo.RoleIDS))
            {
                ApplicationConfig.dirApplicationRoleConfig.Add(this.CurrentUserInfo.RoleIDS, new ET.Sys_BLL.SystemBLL().GetUserALLAction(this.UserID.ToString()));
            }
            ViewBag.STU_CNNAME = this.CurrentUserInfo.UserCNName;
            int i = GetOnlineUser();
            ViewBag.ONLINE_USER_COUNT = i;
            return View();
        }

        [HttpGet]
        public JsonResult AjaxQueryMenusList()
        {
            return Json(new { menus = new ET.Sys_BLL.SystemBLL().List_ModuleMenuData(this.UserID.ToString()) }, JsonRequestBehavior.AllowGet);
        }

        #endregion Default

        #region 模块管理文件夹


        public ActionResult ModuleQuery()
        {
            return View();
        }
        public ActionResult ModuleManage()
        {
            return View();
        }


        #region Ajax模块操作方法
        [HttpGet]
        public JsonResult AjaxQueryModulePageList()
        {
            //接收datagrid传来的参数 
            int pageIndex = int.Parse(Request["page"]);
            int pageSize = int.Parse(Request["rows"]);
            long RecordTotalCount = 0;
            List<SysModuleInfo> list = new ET.Sys_BLL.SystemBLL().PageList_SysModuleInfo("MODULEID,MODULENAME,ModuleKey,ModuleSort,ModuleICON,MODULEURL", null, "MODULESORT DESC", pageIndex, pageSize, ref RecordTotalCount);
            return Json(new { total = RecordTotalCount, rows = list }, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public JsonResult AjaxGetPModuleData(string infoid)
        {
            string condition = "";
            if (!string.IsNullOrEmpty(infoid))
                condition = " AND CHARINDEX('" + infoid + "',MODULEID)=0";
            List<KeyAndValue> list = new ET.Sys_BLL.PublicBLL().GetListByCondition<KeyAndValue>("MODULEID id,MODULENAME text", ET.Constant.DBConst.TableNames.SysModuleInfo, " AND MODULEPID='-1'" + condition, "MODULESORT");
            list.Insert(0, new KeyAndValue() { id = "-1", text = "根目录" });
            return Json(list, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult AjaxSaveModule(FormCollection collection, string infoid)
        {
            bool IsInsert = false;
            string strResult = "false";
            SysModuleInfo info = new ET.Sys_BLL.SystemBLL().Get_SysModuleInfo(" AND MODULEID='" + infoid + "'");
            if (info == null)
            {
                info = new SysModuleInfo();
                IsInsert = true;
            }
            info.ModulePID = collection["ModulePID"];
            info.ModuleName = collection["ModuleName"];
            info.ModuleUrl = collection["ModuleUrl"];
            info.ModuleSort = collection["ModuleSort"];

            info.ModuleKey = collection["ModuleKey"];
            if (new ET.Sys_BLL.SystemBLL().Operate_SysModuleInfo(info, IsInsert))
            {
                if (IsInsert)
                {
                    SysActionInfo ainfo = new SysActionInfo();
                    ainfo.ModuleID = info.ModuleID.ToString();
                    ainfo.ActionStatus = 1;
                    ainfo.ActionKey = info.ModuleKey + "_View";
                    ainfo.ActionName = info.ModuleName + "查看";
                    new ET.Sys_BLL.SystemBLL().Operate_SysActionInfo(ainfo, true);
                }
                strResult = "true";
            }
            return Content(strResult);
        }
        [HttpGet]
        public JsonResult AjaxGetModuleDetail(string infoid)
        {
            if (string.IsNullOrEmpty(infoid))
                return Json("error", JsonRequestBehavior.AllowGet);
            SysModuleInfo info = new ET.Sys_BLL.SystemBLL().Get_SysModuleInfo(" AND MODULEID='" + infoid + "'");
            if (info == null)
                return Json("error", JsonRequestBehavior.AllowGet);
            return Json(info, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult AjaxDeleteModule(string infoid)
        {
            if (string.IsNullOrEmpty(infoid) && new ET.Sys_BLL.SystemBLL().Delete_SysModuleInfo(" AND MODULEID='" + infoid + "'"))
                return Content("true");
            else
                return Content("false");
        }
        [HttpGet]
        public JsonResult AjaxSearchModule(string query)
        {
            List<SysModuleInfo> list = new ET.Sys_BLL.SystemBLL().List_SysModuleInfo(" top 10 MODULENAME", " AND CHARINDEX('" + query + "', MODULENAME)>0", "MODULESORT DESC");
            var arrData = list.Select(c => c.ModuleName);
            return Json(new { query = query, suggestions = arrData, data = arrData }, JsonRequestBehavior.AllowGet);

        }
        #endregion
        #endregion

        #region 角色管理文件夹


        public ActionResult RoleQuery()
        {
            return View();
        }
        public ActionResult RoleManage()
        {
            return View();
        }
        public ActionResult ActionManage()
        {
            return View();
        }

        #region Ajax模块操作方法
        [HttpGet]
        public JsonResult AjaxQueryRolePageList()
        {
            //接收datagrid传来的参数 
            int pageIndex = int.Parse(Request["page"]);
            int pageSize = int.Parse(Request["rows"]);
            long RecordTotalCount = 0;
            List<SysRoleInfo> list = new ET.Sys_BLL.SystemBLL().PageList_SysRoleInfo("ROLEID,ROLENAME,RoleDescription", null, "ROLECreateTime", pageIndex, pageSize, ref RecordTotalCount);
            return Json(new { total = RecordTotalCount, rows = list }, JsonRequestBehavior.AllowGet);
        }
        //动态加载
        [HttpPost]
        public JsonResult AjaxGetRoleActoionData(string infoid, string id)
        {
            List<ModuleLimitInfo> infos = new ET.Sys_BLL.SystemBLL().ListGroup_RoleActons(infoid, id);
            return Json(infos, JsonRequestBehavior.AllowGet);

        }

        [HttpPost]
        public ActionResult AjaxSaveRole(FormCollection collection, string infoid, string ActionIDS)
        {
            bool IsInsert = false;
            string strResult = "false";
            SysRoleInfo info = new ET.Sys_BLL.SystemBLL().Get_SysRoleInfo(" AND ROLEID='" + infoid + "'");
            if (info == null)
            {
                info = new SysRoleInfo();
                IsInsert = true;
            }
            info.RoleName = collection["RoleName"];
            info.RoleDescription = collection["RoleDescription"];

            info.RoleDescription = collection["RoleDescription"];
            List<string> RoleIDS = null;
            if (!string.IsNullOrEmpty(ActionIDS))
            {
                RoleIDS = JsonSerializeHelper.DeserializeFromJson<List<string>>(ActionIDS);
            }
            if (new ET.Sys_BLL.SystemBLL().Operate_SysRoleInfo(info, RoleIDS.Where(c => !string.IsNullOrEmpty(c)).ToList(), IsInsert))
                strResult = "true";
            return Content(strResult);
        }
        [HttpGet]
        public JsonResult AjaxGetRoleDetail(string infoid)
        {
            if (string.IsNullOrEmpty(infoid))
                return Json("error", JsonRequestBehavior.AllowGet);
            SysRoleInfo info = new ET.Sys_BLL.SystemBLL().Get_SysRoleInfo(" AND ROLEID='" + infoid + "'");
            if (info == null)
                return Json("error", JsonRequestBehavior.AllowGet);
            return Json(info, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult AjaxDeleteRole(string infoid)
        {
            if (string.IsNullOrEmpty(infoid) && new ET.Sys_BLL.SystemBLL().Delete_SysRoleInfo(" AND ROLEID=" + infoid))
                return Content("true");
            else
                return Content("false");
        }
        [HttpGet]
        public JsonResult AjaxSearchRole(string query)
        {
            List<SysRoleInfo> list = new ET.Sys_BLL.SystemBLL().List_SysRoleInfo(" top 10 Rolename", " AND  CHARINDEX('" + query + "', Rolename)>0", "ROLECreateTime");
            var arrData = list.Select(c => c.RoleName);
            return Json(new { query = query, suggestions = arrData, data = arrData }, JsonRequestBehavior.AllowGet);

        }
        #endregion

        #region 动作管理
        [HttpGet]
        public JsonResult AjaxGetActionDetail(string infoid)
        {
            if (string.IsNullOrEmpty(infoid))
                return Json("error", JsonRequestBehavior.AllowGet);
            SysActionInfo info = new ET.Sys_BLL.SystemBLL().Get_SysActionInfo(" AND ACTIONID='" + infoid + "'");
            if (info == null)
                return Json("error", JsonRequestBehavior.AllowGet);
            return Json(info, JsonRequestBehavior.AllowGet);
        }
        public ActionResult AjaxSaveAction(FormCollection collection, string infoid)
        {
            bool IsInsert = false;
            string strResult = "false";
            SysActionInfo info = new ET.Sys_BLL.SystemBLL().Get_SysActionInfo(" AND ACTIONID='" + infoid + "'");
            if (info == null)
            {
                info = new SysActionInfo();
                IsInsert = true;
            }
            info.ModuleID = collection["ModuleID"];
            info.ActionKey = collection["ActionKey"];
            info.ActionStatus = 1;
            info.ActionName = collection["ActionName"];
            if (new ET.Sys_BLL.SystemBLL().Operate_SysActionInfo(info, IsInsert))
                strResult = "true";
            return Content(strResult);
        }

        public JsonResult AjaxGetActionModuleData()
        {
            List<KeyAndValue> list = new ET.Sys_BLL.PublicBLL().GetNestListByCondition("MOduleID id,MOduleNAME text,MOdulepID pid", ET.Constant.DBConst.TableNames.SysModuleInfo, null, "MOduleSORT DESC");
            list.Insert(0, new KeyAndValue() { id = "-1", text = "根目录" });
            return Json(list, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public JsonResult AjaxQueryActionPageList()
        {
            //接收datagrid传来的参数 
            int pageIndex = int.Parse(Request["page"]);
            int pageSize = int.Parse(Request["rows"]);
            long RecordTotalCount = 0;
            List<SysActionInfo> list = new ET.Sys_BLL.SystemBLL().PageList_SysActionInfo("ActionID,ActionName,ActionKey", null, "ActionName", pageIndex, pageSize, ref RecordTotalCount);
            return Json(new { total = RecordTotalCount, rows = list }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult AjaxDeleteAction(string infoid)
        {
            if (string.IsNullOrEmpty(infoid) && new ET.Sys_BLL.SystemBLL().Delete_SysActionInfo(" AND ActionID=" + infoid))
                return Content("true");
            else
                return Content("false");
        }
        #endregion
        #endregion

        #region 我的信息
        public ActionResult EditUserPwd()
        {
            return View();
        }
        [HttpPost]
        public ActionResult AjaxUpdatePwd(FormCollection collection)
        {
            UserBaseInfo uinfo = new ET.Sys_BLL.OrganizationBLL().Get_UserBaseInfo(" AND USERID='" + this.UserID.ToString() + "' AND UserPwd='" + ET.ToolKit.Encrypt.EncrypeHelper.EncryptMD5(ET.ToolKit.Common.StringHelper.ClearSqlDangerous(collection["OldUserPwd"])) + "'");
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
        #endregion
    }

}
