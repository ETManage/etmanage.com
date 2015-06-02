using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using ET.Sys_DEF;
using ET.DALContract;
using ET.ToolKit.DBLayer;
using System.Linq;
using ET.Constant.DBConst;
namespace ET.Sys_BLL
{
    /// <summary>
    /// 系统查询
    /// </summary>
    public class SystemBLL
    {
        #region 模块管理

        /// <summary>
        /// 系统模块操作
        /// </summary>
        /// <param name="info">模块信息</param>
        public bool Operate_SysModuleInfo(SysModuleInfo info, bool IsInsert)
        {
            if (IsInsert)
                return new TBaseDAL<SysModuleInfo>().InsertInstance(info) > 0;
            else
                return new TBaseDAL<SysModuleInfo>().UpdateInstance(info) > 0;
        }

        /// <summary>
        /// 删除模块信息
        /// </summary>
        /// <param name="Condition">条件需要以AND开头</param>
        public bool Delete_SysModuleInfo(string Condition)
        {
            return new BaseDAL().TransactionForVoid(new Action<DataBase>(delegate(DataBase dataBase)
            {
                new TBaseDAL<SysActionInfo>(dataBase).DeleteInstances(Condition);
                new TBaseDAL<SysModuleInfo>(dataBase).DeleteInstances(Condition);
            }));
        }

        /// <summary>
        /// 获取单个模块信息
        /// </summary>
        /// <param name="condition">查询条件</param>
        /// <returns>模块信息</returns>
        public SysModuleInfo Get_SysModuleInfo(string Condition)
        {
            SysModuleInfo info = null;
            info = new TBaseDAL<SysModuleInfo>().GetInstanceByCondition(Condition);
            return info;
        }

        /// <summary>
        /// 获取所有模块列表
        /// </summary>
        public List<SysModuleInfo> List_SysModuleInfo(string Fields, string Condition, string strOrder)
        {
            return new TBaseDAL<SysModuleInfo>().GetListByCondition(Fields, Condition, strOrder);
        }

        /// <summary>
        /// 获取模块翻页列表
        /// </summary>
        public List<SysModuleInfo> PageList_SysModuleInfo(string Fields, string Condition, string Orderby, int Offset, int Count, ref long RecordTotalCount)
        {
            return new TBaseDAL<SysModuleInfo>().GetListByPager(Fields, Condition, Orderby, Offset, Count, ref  RecordTotalCount);
        }

        #endregion 模块管理

        #region 动作管理
        public bool Operate_SysActionInfo(SysActionInfo info, bool IsInsert)
        {
            if (info.ActionID == Guid.Empty)
                return new TBaseDAL<SysActionInfo>().InsertInstance(info) > 0;
            else
                return new TBaseDAL<SysActionInfo>().UpdateInstance(info) > 0;
        }


        /// <summary>
        /// 删除角色权限关系
        /// </summary>
        /// <param name="Condition"></param>
        public bool Delete_SysActionInfo(string Condition)
        {
            return new TBaseDAL<SysActionInfo>().DeleteInstances(Condition) > 0;
        }
        public SysActionInfo Get_SysActionInfo(string Condition)
        {
            SysActionInfo info = new TBaseDAL<SysActionInfo>().GetInstanceByCondition(Condition);
            return info;
        }

        public List<SysActionInfo> PageList_SysActionInfo(string Fields, string Condition, string Orderby, int Offset, int Count, ref long RecordTotalCount)
        {
            return new TBaseDAL<SysActionInfo>().GetListByPager(Fields, Condition, Orderby, Offset, Count, ref  RecordTotalCount);
        }
        /// <summary>
        /// 获取全部权限点
        /// </summary>
        public List<SysActionInfo> List_SysActionInfo(string Fields, string Condition, string strOrder)
        {
            return new TBaseDAL<SysActionInfo>().GetListByCondition(Fields, Condition, strOrder);
        }

        #endregion

        #region 角色管理

        /// <summary>
        /// 操作角色权限关系
        /// </summary>
        /// <param name="info"></param>
        public bool Operate_SysRoleInfo(SysRoleInfo info, List<string> RoleIDS, bool IsInsert)
        {
            return new BaseDAL().TransactionForVoid(new Action<DataBase>(delegate(DataBase dataBase)
            {
                new TBaseDAL<SysRoleActionLink>().DeleteInstances(" AND RoleID='" + info.RoleID + "'");
                if (IsInsert)
                {
                    info.RoleID = Guid.NewGuid();
                    new TBaseDAL<SysRoleInfo>().InsertInstance(info);
                    foreach (string item in RoleIDS)
                    {
                        SysRoleActionLink acinfo = new SysRoleActionLink();
                        acinfo.ActionID = Guid.Parse(item);
                        acinfo.RoleID = info.RoleID;
                        new TBaseDAL<SysRoleActionLink>().InsertInstance(acinfo);
                    }


                }
                else
                {
                    new TBaseDAL<SysRoleInfo>().UpdateInstance(info);
                    foreach (string item in RoleIDS)
                    {
                        SysRoleActionLink acinfo = new SysRoleActionLink();
                        acinfo.ActionID = Guid.Parse(item);
                        acinfo.RoleID = info.RoleID;
                        new TBaseDAL<SysRoleActionLink>().InsertInstance(acinfo);
                    }

                }

            }));
        }

        /// <summary>
        /// 删除角色信息
        /// </summary>
        /// <param name="Condition">条件需要以AND开头</param>
        public bool Delete_SysRoleInfo(string Condition)
        {
            return new BaseDAL().TransactionForVoid(new Action<DataBase>(delegate(DataBase dataBase)
            {
                new TBaseDAL<SysRoleActionLink>(dataBase).DeleteInstances(Condition);
                new TBaseDAL<SysRoleInfo>(dataBase).DeleteInstances(Condition);
            }));
        }

        /// <summary>
        /// 操作角色权限关系
        /// </summary>
        /// <param name="info"></param>
        public bool Operate_SysRoleActionLink(SysRoleActionLink info, bool IsInsert)
        {
            if (IsInsert)
                return new TBaseDAL<SysRoleActionLink>().InsertInstance(info) > 0;
            else
                return new TBaseDAL<SysRoleActionLink>().UpdateInstance(info) > 0;

        }

        /// <summary>
        /// 删除角色权限关系
        /// </summary>
        /// <param name="Condition"></param>
        public bool Delete_SysRoleActionLink(string Condition)
        {
            return new TBaseDAL<SysRoleActionLink>().DeleteInstances(Condition) > 0;
        }

        /// <summary>
        /// 获取角色信息
        /// </summary>
        /// <param name="Condition"></param>
        /// <returns></returns>
        public SysRoleInfo Get_SysRoleInfo(string Condition)
        {
            SysRoleInfo info = new TBaseDAL<SysRoleInfo>().GetInstanceByCondition(Condition);
            return info;
        }
        public List<SysRoleInfo> List_SysRoleInfo(string Fields, string Condition, string strOrder)
        {
            return new TBaseDAL<SysRoleInfo>().GetListByCondition(Fields, Condition, strOrder);
        }
        public List<SysRoleInfo> PageList_SysRoleInfo(string Fields, string Condition, string Orderby, int Offset, int Count, ref long RecordTotalCount)
        {
            return new TBaseDAL<SysRoleInfo>().GetListByPager(Fields, Condition, Orderby, Offset, Count, ref  RecordTotalCount);
        }

        /// <summary>
        /// 获取用户角色集合
        /// </summary>
        public List<SysRoleInfo> List_SysRoleInfo(string UserID)
        {
            return new BaseDAL().GetListByCondition<SysRoleInfo>("RoleID,RoleName", ViewNames.V_USER_ROLE_INFO, " AND UserID='" + UserID + "'", "RoleName DESC");
        }
        
        /// <summary>
        /// 获取权限信息
        /// </summary>
        /// <param name="Fields">如何为空则查询所有</param>
        /// <param name="Condition">条件需要加上关键字AND</param>
        /// <param name="Order">不接Order by的排序</param>
        /// <returns></returns>
        public List<Guid> List_SysRoleActionLink(string Fields, string Condition, string strOrder)
        {
            List<SysRoleActionLink> limits = new TBaseDAL<SysRoleActionLink>().GetListByCondition(Fields, Condition, strOrder);
            List<Guid> listLimit_ids = limits.Select(c => c.ActionID).ToList();
            return listLimit_ids;
        }
        /// <summary>
        /// 获取所有权限信息，同时包含当前登录用户拥有的权限信息
        /// </summary>
        /// <param name="UserID"></param>
        /// <returns></returns>
        public List<ModuleLimitInfo> ListGroup_UserActons(string UserID)
        {
            List<SysModuleInfo> listModules = List_SysModuleInfo("ModuleID,ModuleName,ModulePID", null, "ModuleSort DESC");
            List<SysActionInfo> limits = new TBaseDAL<SysActionInfo>().GetListByCondition("ActionID,ModuleID,ActionName,ActionKey,ActionStatus", null, null);
            List<string> mylimits = GetUserALLAction(UserID);
            List<ModuleLimitInfo> modulelimitGroup = new List<ModuleLimitInfo>();
            RecurseModule(listModules, limits, "-1", mylimits, modulelimitGroup);
            return modulelimitGroup;

        }
        /// <summary>
        /// 获取所有权限信息，同时包含角色拥有的权限信息
        /// </summary>
        /// <param name="UserID"></param>
        /// <returns></returns>
        public List<ModuleLimitInfo> ListGroup_RoleActons(string RoleID, string selectPID)
        {
            List<SysModuleInfo> listModules = List_SysModuleInfo("ModuleID,ModuleName,ModulePID", null, "ModuleSort DESC");
            List<SysActionInfo> limits = new TBaseDAL<SysActionInfo>().GetListByCondition("ActionID,ModuleID,ActionName,ActionKey,ActionStatus", null, null);
            List<string> rolelimits = GetRoleALLLimit(RoleID);
            List<ModuleLimitInfo> modulelimitGroup = new List<ModuleLimitInfo>();
            RecurseModule(listModules, limits, string.IsNullOrEmpty(selectPID) ? "-1" : selectPID, rolelimits, modulelimitGroup);
            return modulelimitGroup;

        }
      /// <summary>
      /// 递归遍历模块
      /// </summary>
        void RecurseModule(List<SysModuleInfo> listModules, List<SysActionInfo> limits, string Module_pid, List<string> mylimits, List<ModuleLimitInfo> modulelimitGroup)
        {
            List<SysModuleInfo> infos = listModules.Where(info => info.ModulePID.Equals(Module_pid, StringComparison.CurrentCultureIgnoreCase)).ToList();
            foreach (SysModuleInfo item in infos)
            {
                ModuleLimitInfo modulelimit = new ModuleLimitInfo();
                List<SysActionInfo> listlim = limits.Where(info => info.ModuleID.Equals(item.ModuleID.ToString(), StringComparison.CurrentCultureIgnoreCase)).ToList();
                modulelimit.id = item.ModuleID.ToString();
                modulelimit.text = item.ModuleName;
                modulelimit.state = "closed";


               List<ModuleLimitInfo> modulelimitchildren = new List<ModuleLimitInfo>();



                if (Module_pid == "-1")
                {
                    RecurseModule(listModules, limits, item.ModuleID.ToString
                        (), mylimits, modulelimitchildren);
                }
                foreach (SysActionInfo acinfo in listlim)
                {
                    if (mylimits.Count() > 0 && mylimits.Contains(acinfo.ActionKey))
                    {
                        modulelimitchildren.Add(new ModuleLimitInfo()
                        {
                            id = acinfo.ActionID.ToString(),
                            text = acinfo.ActionName,
                            attributes = acinfo.ActionKey,
                            @checked = "true"
                        });
                    }
                    else
                    {
                        modulelimitchildren.Add(new ModuleLimitInfo()
                        {
                            id = acinfo.ActionID.ToString(),
                            text = acinfo.ActionName,
                            attributes = acinfo.ActionKey
                        });
                    }
                }
                if (modulelimitchildren.Count()>0)
                modulelimit.children = modulelimitchildren;
                modulelimitGroup.Add(modulelimit);
            }
        }

        /// <summary>
        /// 递归遍历模块，得到菜单
        /// </summary>
        public List<MenuModuleInfo> List_ModuleMenuData(string UserID)
        {
            List<SysModuleInfo> listModules = new BaseDAL().GetListByCondition<SysModuleInfo>(" DISTINCT ModuleID,ModuleName,ModuleIcon,ModuleUrl,ModulePID,ModuleSort", ViewNames.V_USER_MODULE_INFO, " AND cast(UserID as varchar(50))='" + UserID + "'", "ModuleSort DESC");
            MenuModuleInfo menu = new MenuModuleInfo();
            List<MenuModuleInfo> modulelimitGroup = new List<MenuModuleInfo>();
            RecurseModule(listModules, "-1", ref modulelimitGroup);
            return modulelimitGroup;
        }
        /// <summary>
        /// 递归遍历模块
        /// </summary>
        void RecurseModule(List<SysModuleInfo> listModules, string Module_pid, ref  List<MenuModuleInfo> modulelimitGroup)
        {
            foreach (SysModuleInfo item in listModules.Where(info => info.ModulePID == Module_pid))
            {
                MenuModuleInfo modulelimit = new MenuModuleInfo();
                modulelimit.menuid = item.ModuleID.ToString();
                modulelimit.menuname = item.ModuleName;
                modulelimit.icon = item.ModuleIcon;
                modulelimit.url = item.ModuleUrl;
                List<MenuModuleInfo> menuchildren = new List<MenuModuleInfo>();
                RecurseModule(listModules, item.ModuleID.ToString
                    (), ref menuchildren);
                modulelimit.menus = menuchildren;
                modulelimitGroup.Add(modulelimit);

            }
        }

        /// <summary>
        /// 获取用户的所有动作
        /// </summary>
        /// <param name="UserID">用户ID</param>
        /// <returns></returns>
        public List<string> GetUserALLAction(string UserID)
        {
            return new BaseDAL().GetListByCondition<string>("ActionKey", ViewNames.V_USER_ACTION_INFO, " AND UserID='" + UserID + "'", null);
        }
        /// <summary>
        /// 获取角色的所有动作
        /// </summary>
        /// <param name="RoleID">角色ID</param>
        public List<string> GetRoleALLLimit(string RoleID)
        {
            return new BaseDAL().GetListByCondition<string>("ActionKey", ViewNames.V_ROLE_ACTION_INFO, " AND RoleID='" + RoleID + "'", null);
        }

        #endregion 角色管理

 
    }
}
