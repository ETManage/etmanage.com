﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;
using ET.Sys_DEF;
using ET.ToolKit.Common;
using ET.Constant.DBConst;

namespace ET.Web.Areas.Manage.Controllers
{
    [UserAuthorize]
    public class SystemController : ManageControllerBase
    {
        //
        // GET: /System/
        public ActionResult Index()
        {
            getSystemConfig();
            ViewBag.OnlineUserCount = this.GetOnlineUser();
            ViewBag.List_ChatMessage = new ET.Sys_BLL.SystemBLL().List_ChatMessage("[MsgID],[MsgTitle],[MsgContent],[CreateTime],[Sender],[Receiver],[ReceiveTime],[Status],(SELECT TOP 1 PHOTO FROM UserProperty WHERE USERID=ChatMessage.SENDER ) Reserve1,(SELECT TOP 1 CNNAME FROM UserProperty WHERE USERID=ChatMessage.SENDER ) Reserve2", "AND STATUS=0 AND Receiver='" + this.UserID + "'", "CreateTime DESC");


            ViewBag.AccessCount = new ET.Sys_BLL.PublicBLL().GetRecordCount(ET.Constant.DBConst.TableNames.SysPageAccess, null);
            ViewBag.BlogCommentCount = new ET.Sys_BLL.PublicBLL().GetRecordCount(ET.Constant.DBConst.TableNames.BlogCommentInfo, null);
            ViewBag.SystemUserCount = new ET.Sys_BLL.PublicBLL().GetRecordCount(ET.Constant.DBConst.TableNames.UserBase, null);
            ViewBag.List_SysNotice = new ET.Sys_BLL.SystemBLL().List_SysNotice(null, "AND STATUS=0", "CreateTime DESC");
            ViewBag.List_NewUser = new ET.Sys_BLL.PublicBLL().GetListByCondition<UserProperty>("TOP 14 USERID,PHOTO,CNNAME,CREATETIME,STATUS Age", ET.Constant.DBConst.ViewNames.V_USERFULL, null, "CreateTime DESC");
            ViewBag.List_BlogComment = new ET.Sys_BLL.BlogBLL().List_BlogCommentInfo("Top 4 CommentID,[CreateTime],[Creator],[IsAnonymity],[CreatorID],(select photo from UserProperty where userid=BlogCommentInfo.CreatorID) CreatorUrl", "AND STATUS=1", "CreateTime DESC");






            IList<ET.Sys_Base.OnlineUser.OnlineUser> listOnlineUser = this.GetListOnlineUser();
            string ids = "";
            if (listOnlineUser != null && listOnlineUser.Count > 0)
            {
                ids = string.Join(",", listOnlineUser.Select(o => "'" + o.UserName + "'"));
            }
            ViewBag.List_OnlineUser = new ET.Sys_BLL.OrganizationBLL().List_UserProperty(null, !string.IsNullOrEmpty(ids) && ids != "''" ? "AND USERID IN (" + ids + ")" : null, "CreateTime DESC");
            return View(this.CurrentUserInfo);
        }

        [HttpGet]
        public JsonResult AjaxSearchModule(string query)
        {
            string currdate = DateTime.Now.ToString("yyyy-MM-dd HH:mm");
            List<SysFunction> list = new ET.Sys_BLL.PublicBLL().GetListByCondition<SysFunction>("distinct FuncID,FuncName,FuncSort", ViewNames.V_ALLUSERLIMIT, " AND  CHARINDEX('" + query + "', FuncName)>0  and not EXISTS(select 1 from sysfunction where funcpid=cast(V_ALLUSERLIMIT.funcid as varchar(50))) and Functype=0 and (Status=1 OR (Status=0 AND StartTime<='" + currdate + "' AND EndTime>'" + currdate + "')) " + (this.UserID == "a50ca689-1748-455d-b4b4-2c9303e186be" ? "" : " AND cast(userid as varchar(50))='" + this.UserID + "'"), "FuncSort DESC");
            var arrData = list.Select(c => c.FuncName);
            return Json(new { query = query, suggestions = arrData, data = list }, JsonRequestBehavior.AllowGet);

        }

        #region Default
        public ActionResult Default()
        {
            getSystemConfig();
            ViewBag.CurrentUserInfo = this.CurrentUserInfo;
            ViewBag.OnlineUserCount = this.GetOnlineUser();
            //ViewBag.List_SysNotice = new ET.Sys_BLL.SystemBLL().List_SysNotice(null, "AND STATUS=0", "CreateTime DESC");
            //ViewBag.List_ChatMessage = new ET.Sys_BLL.SystemBLL().List_ChatMessage("[MsgID],[MsgTitle],[MsgContent],[CreateTime],[Sender],[Receiver],[ReceiveTime],[Status],(SELECT TOP 1 PHOTO FROM UserProperty WHERE USERID=ChatMessage.SENDER ) Reserve1", "AND STATUS=0 AND Receiver='" + this.UserID + "'", "CreateTime DESC");

            return View();
        }
        

        [HttpPost]
        public JsonResult AjaxQueryMenusList()
        {
            return Json(new { menus = new ET.Sys_BLL.SystemBLL().List_BlogModuleMenuData(this.UserID.ToString()) }, JsonRequestBehavior.AllowGet);
        }

       
        #endregion Default

        #region 角色管理文件夹


        public ActionResult RoleQuery()
        {
            return View();
        }
        public ActionResult RoleManage()
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
            string Condition = "";
            if (!string.IsNullOrEmpty(Request["name"]))
                Condition = " AND CHARINDEX('" + Request["name"] + "', ROLENAME)>0";
            long RecordTotalCount = 0;
            List<SysRole> list = new ET.Sys_BLL.SystemBLL().Pagination_SysRole("ROLEID,ROLENAME,RoleDescription", Condition, "ROLECreateTime", pageIndex, pageSize, ref RecordTotalCount);
            return Json(new { total = RecordTotalCount, rows = list }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult AjaxSaveRole(FormCollection collection, string id, string limitIds)
        {
            bool IsInsert = false;
            string strResult = "false";
            SysRole info = new ET.Sys_BLL.SystemBLL().Get_SysRole(" AND ROLEID='" + id + "'");
            if (info == null)
            {
                info = new SysRole();
                IsInsert = true;
            }
            info.RoleName = collection["RoleName"];
            info.RoleDescription = collection["RoleDescription"];
            info.RoleDescription = collection["RoleDescription"];
            List<string> RoleIDS = new List<string>();
            if (!string.IsNullOrEmpty(limitIds))
            {
                RoleIDS = JsonSerializeHelper.DeserializeFromJson<List<string>>(limitIds);
            }
            if (new ET.Sys_BLL.SystemBLL().Update_SysRole(info, RoleIDS.Where(c => !string.IsNullOrEmpty(c)).ToList(), IsInsert))
                strResult = "true";
            return Content(strResult);
        }
        [HttpGet]
        public JsonResult AjaxGetRoleDetail(string id)
        {
            if (string.IsNullOrEmpty(id))
                return Json("error", JsonRequestBehavior.AllowGet);
            SysRole info = new ET.Sys_BLL.SystemBLL().Get_SysRole(" AND ROLEID='" + id + "'");
            if (info == null)
                return Json("error", JsonRequestBehavior.AllowGet);
            return Json(info, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult AjaxDeleteRole(string ids)
        {
            if (!string.IsNullOrEmpty(ids) && new ET.Sys_BLL.SystemBLL().Delete_SysRole(" AND ROLEID in (" + ids + ")"))
                return Content("true");
            else
                return Content("false");
        }
        [HttpGet]
        public JsonResult AjaxSearchRole(string query)
        {
            List<SysRole> list = new ET.Sys_BLL.SystemBLL().List_SysRole(" top 10 Rolename", " AND  CHARINDEX('" + query + "', Rolename)>0", "ROLECreateTime");
            var arrData = list.Select(c => c.RoleName);
            return Json(new { query = query, suggestions = arrData, data = arrData }, JsonRequestBehavior.AllowGet);

        }
       [HttpPost]
        public JsonResult AjaxGetAllFunctionData(string id,string rid)
        {
            string ischecked = "";
            if (!string.IsNullOrEmpty(rid))
                ischecked = ",case when exists(select 1 from V_ALLROLELIMIT A where  ROLEID='" + rid + "' AND A.FUNCID=SysFunction.FUNCID) then 1 else 0 end checked";
            //string condition = " AND  FuncPID='-1' ";
            //if (!string.IsNullOrEmpty(id))
            //    condition = " AND  FuncPID='" + id + "' ";
            List<TreeModuleInfo> list = new ET.Sys_BLL.PublicBLL().GetNestTreeByCondition("FuncID id,FuncNAME text,FuncPID pid,case when functype=1 then 'icon-company' when functype=-1 then 'icon-children' else 'icon-group' end iconCls,case when exists(select 1 from SysFunction c where c.funcpid=CAST(SysFunction.FuncID as varchar(36))) then 'closed' else 'open' end state" + ischecked, ET.Constant.DBConst.TableNames.SysFunction, null, "FuncSORT  DESC");

            return Json(list, JsonRequestBehavior.AllowGet);
        }
      
        #endregion
        #endregion

       #region 我的信息
       public ActionResult EditUserPwd()
       {
           return View();
       }
       public ActionResult UserInfo()
       {
           return View();
       }

       [HttpPost]
       public ActionResult AjaxUpdatePwd(FormCollection collection)
       {
           UserBase uinfo = new ET.Sys_BLL.OrganizationBLL().Get_UserBase(" AND USERID='" + this.UserID + "' AND UserPwd='" + ET.ToolKit.Encrypt.EncrypeHelper.EncryptMD5(ET.ToolKit.Common.StringHelper.ClearSqlDangerous(collection["OldUserPwd"])) + "'");
           if (uinfo != null)
           {
               uinfo.UserPwd = ET.ToolKit.Encrypt.EncrypeHelper.EncryptMD5(collection["UserPwd"]);
               if (new ET.Sys_BLL.OrganizationBLL().Update_UserBase(uinfo))
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

        #region 权限管理

        public ActionResult LimitManage()
        {
            return View();
        }
        public ActionResult LimitOrgManag()
        {
            return View();
        }
        public ActionResult LimitOrgContact()
        {
            return View();
        }
        [HttpPost]
        public JsonResult AjaxGetLimitData(string id)
        {
            string condition = " AND  FuncPID='-1' ";
            if (!string.IsNullOrEmpty(id))
                condition = " AND  FuncPID='" + id + "' ";
            List<TreeModuleInfo> list = new ET.Sys_BLL.PublicBLL().GetListByCondition<TreeModuleInfo>("FuncID id,FuncNAME text,FuncPID pid,case when functype=1 then 'icon-company' when functype=-1 then 'icon-children' else 'icon-group' end iconCls,case when exists(select 1 from SysFunction c where c.funcpid=CAST(SysFunction.FuncID as varchar(36))) then 'closed' else 'open' end state", ET.Constant.DBConst.TableNames.SysFunction, condition, "FuncSORT desc");
            if (string.IsNullOrEmpty(id))
                list.Insert(0, new TreeModuleInfo() { id = "-1", text = "所有资源", state = "open", iconCls = "icon-root" });
            return Json(list, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult AjaxSaveFunction(FormCollection collection, string id,string pid)
        {
            bool IsInsert = false;
            string strResult = "false";
            SysFunction info = new ET.Sys_BLL.SystemBLL().Get_SysFunction(" AND FuncID='" + id + "'");
            if (info == null)
            {
                info = new SysFunction();
                info.CreatorID = Guid.Parse(this.UserID);
                info.CreateTime = DateTime.Now;
                IsInsert = true;
            }

            info.UpdateTime = DateTime.Now;
            info.FuncPID = pid;
            SysFunction keyinfo = new ET.Sys_BLL.SystemBLL().Get_SysFunction(" AND FuncKey='" + collection["FuncKey"] + "'");
            if (keyinfo != null)
                info.FuncKey = collection["FuncKey"] + "_" + DateTime.Now.ToString("yyMMddHHmm");
            else
                info.FuncKey = collection["FuncKey"];
            info.FuncName = collection["FuncName"];
            info.FuncType = collection["FuncType"];
            info.ShowStyle = collection["ShowStyle"];
            info.FuncSort = collection["FuncSort"];
            info.Source = collection["Source"];
            info.Remark = collection["Remark"];
            if (!string.IsNullOrEmpty(collection["IconPath"]))
                info.IconPath = collection["IconPath"];
            else
                info.IconPath = "icon-group";
            if (!string.IsNullOrEmpty(collection["StartTime"]))
                info.StartTime = Convert.ToDateTime(collection["StartTime"]);
            if (!string.IsNullOrEmpty(collection["EndTime"]))
                info.EndTime = Convert.ToDateTime(collection["EndTime"]);
            info.Status = Convert.ToInt32(collection["Status"]);

            info.PublicLevel = Convert.ToInt32(collection["PublicLevel"]);
            if (new ET.Sys_BLL.SystemBLL().Update_SysFunction(info, IsInsert))
            {
                strResult = "true";
            }
            return Json(new { result = strResult, id = info.FuncID }, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public JsonResult AjaxGetFunctionDetail(string id)
        {
            if (string.IsNullOrEmpty(id))
                return Json("error", JsonRequestBehavior.AllowGet);
            SysFunction info = new ET.Sys_BLL.SystemBLL().Get_SysFunction(" AND FuncID='" + id + "'");
            if (info != null)
            {
                SysFunction pinfo = new ET.Sys_BLL.SystemBLL().Get_SysFunction(" AND FuncID='" + info.FuncPID + "'");
                if (pinfo != null)
                {
                    info.Reserve1 = pinfo.FuncName;
                }
                else
                {
                    info.Reserve1 = "根节点";
                }
                return Json(info, JsonRequestBehavior.AllowGet);
            }
            return Json("error", JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult AjaxGetBelongLimitData(string type, string fid)
        {
            List<StringArray> list = new List<StringArray>();

            string table = ET.Constant.DBConst.TableNames.UserDeptFuncLink;
            string field = "depid value";
            switch (type)
            {
                case "dept":
                    table = ET.Constant.DBConst.TableNames.UserDeptFuncLink;
                    field = "depid value";
                    break;
                case "post":
                    table = ET.Constant.DBConst.TableNames.UserPostFuncLink;
                    field = "postid value";
                    break;
                case "person":
                    table = ET.Constant.DBConst.TableNames.UserFuncLink;
                    field = "userid value";
                    break;

            }

            list = new ET.Sys_BLL.PublicBLL().GetListByCondition<StringArray>(field, table, " AND cast(FUNCID as varchar(36))='" + fid + "'", null);


            return Json(list, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult AjaxDeleteFunction(string id)
        {
     if (new ET.Sys_BLL.PublicBLL().GetRecordCount(ET.Constant.DBConst.TableNames.SysFunction, "AND FUNCPID='" + id + "'")>0)
            {
                return Content("有子项，无法删除!");
            }
            if (!string.IsNullOrEmpty(id) && new ET.Sys_BLL.SystemBLL().Delete_SysFunction(" AND FuncID='" + id + "'"))
                return Content("true");
            else
                return Content("false");
        }
        [HttpPost]
        public ActionResult AjaxFunctionParentChange(string ids)
        {

            List<KeyAndValue> ChangeIDS = null;
            if (!string.IsNullOrEmpty(ids))
            {
                ChangeIDS = JsonSerializeHelper.DeserializeFromJson<List<KeyAndValue>>(ids);
            }
            foreach (KeyAndValue item in ChangeIDS)
            {
                SysFunction info = new ET.Sys_BLL.SystemBLL().Get_SysFunction(" AND FuncID='" + item.id + "'");
                if (info != null)
                {
                    info.FuncPID = item.pid;
                    new ET.Sys_BLL.SystemBLL().Update_SysFunction(info, false);
                }
            }
            return Content("true");
        }
        [HttpPost]
        public ActionResult AjaxSaveFuncLimit(string fid, string ids, string type)
        {

            switch (type)
            {
                case "dept":

                    new ET.Sys_BLL.OrganizationBLL().Delete_UserDeptFuncLink(string.Format("AND FUNCID='{0}'", fid));
                    foreach (string item in ids.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries))
                    {

                        UserDeptFuncLink info = new UserDeptFuncLink();
                        info.FuncID = Guid.Parse(fid);
                        info.DepID = Guid.Parse(item);
                        info.CreateTime = DateTime.Now;
                        info.CreatorID = Guid.Parse(this.UserID);
                        new ET.Sys_BLL.OrganizationBLL().Update_UserDeptFuncLink(info, true);

                    }


                    break;
                case "post":
                    new ET.Sys_BLL.OrganizationBLL().Delete_UserDeptFuncLink(string.Format("AND FUNCID='{0}'", fid));
                    foreach (string item in ids.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries))
                    {

                        UserPostFuncLink info = new UserPostFuncLink();
                        info.FuncID = Guid.Parse(fid);
                        info.PostID = Guid.Parse(item);
                        info.CreateTime = DateTime.Now;
                        info.CreatorID = Guid.Parse(this.UserID);
                        new ET.Sys_BLL.OrganizationBLL().Update_UserPostFuncLink(info, true);

                    }
                    break;
                case "person":
                    new ET.Sys_BLL.OrganizationBLL().Delete_UserFuncLink(string.Format("AND FUNCID='{0}'", fid));
                    foreach (string item in ids.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries))
                    {

                        UserFuncLink info = new UserFuncLink();
                        info.FuncID = Guid.Parse(fid);
                        info.UserID = Guid.Parse(item);
                        info.CreateTime = DateTime.Now;
                        info.CreatorID = Guid.Parse(this.UserID);
                        new ET.Sys_BLL.OrganizationBLL().Update_UserFuncLink(info, true);
                    }

                    break;

            }
            return Content("true");
        }

        #endregion

        #region 系统提醒管理
        public ActionResult NoticeView(string id)
        {
            SysNotice info = new ET.Sys_BLL.SystemBLL().Get_SysNotice(" AND NOTICEID='" + id + "'");
            if (info == null)
                return this.Goto404PageError();
            return View(info);
        }
        #endregion
    }

}
