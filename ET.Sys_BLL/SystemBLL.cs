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
        #region 角色管理

        /// <summary>
        /// 操作角色权限关系
        /// </summary>
        /// <param name="info"></param>
        public bool Operate_SysRole(SysRole info, List<string> RoleIDS, bool IsInsert)
        {
            return new BaseDAL().TransactionForVoid(new Action<DataBase>(delegate(DataBase dataBase)
            {
                new TBaseDAL<SysRoleFuncLink>().DeleteInstances(" AND RoleID='" + info.RoleID + "'");
                if (IsInsert)
                {
                    info.RoleID = Guid.NewGuid();
                    new TBaseDAL<SysRole>().InsertInstance(info);
                    foreach (string item in RoleIDS)
                    {
                        SysRoleFuncLink acinfo = new SysRoleFuncLink();
                        acinfo.FuncID = Guid.Parse(item);
                        acinfo.RoleID = info.RoleID;
                        new TBaseDAL<SysRoleFuncLink>().InsertInstance(acinfo);
                    }


                }
                else
                {
                    new TBaseDAL<SysRole>().UpdateInstance(info);
                    foreach (string item in RoleIDS)
                    {
                        SysRoleFuncLink acinfo = new SysRoleFuncLink();
                        acinfo.FuncID = Guid.Parse(item);
                        acinfo.RoleID = info.RoleID;
                        new TBaseDAL<SysRoleFuncLink>().InsertInstance(acinfo);
                    }

                }

            }));
        }

        /// <summary>
        /// 删除角色信息
        /// </summary>
        /// <param name="Condition">条件需要以AND开头</param>
        public bool Delete_SysRole(string Condition)
        {
            return new BaseDAL().TransactionForVoid(new Action<DataBase>(delegate(DataBase dataBase)
            {
                new TBaseDAL<UserRoleLink>(dataBase).DeleteInstances(Condition);
                new TBaseDAL<SysRoleFuncLink>(dataBase).DeleteInstances(Condition);
                new TBaseDAL<SysRole>(dataBase).DeleteInstances(Condition);
            }));
        }

        /// <summary>
        /// 操作角色权限关系
        /// </summary>
        /// <param name="info"></param>
        public bool Operate_SysRoleFuncLink(SysRoleFuncLink info, bool IsInsert)
        {
            if (IsInsert)
                return new TBaseDAL<SysRoleFuncLink>().InsertInstance(info) > 0;
            else
                return new TBaseDAL<SysRoleFuncLink>().UpdateInstance(info) > 0;

        }

        /// <summary>
        /// 删除角色权限关系
        /// </summary>
        /// <param name="Condition"></param>
        public bool Delete_SysRoleFuncLink(string Condition)
        {
            return new TBaseDAL<SysRoleFuncLink>().DeleteInstances(Condition) > 0;
        }

        /// <summary>
        /// 获取角色信息
        /// </summary>
        /// <param name="Condition"></param>
        /// <returns></returns>
        public SysRole Get_SysRole(string Condition)
        {
            SysRole info = new TBaseDAL<SysRole>().GetInstanceByCondition(Condition);
            return info;
        }
        public List<SysRole> List_SysRole(string Fields, string Condition, string strOrder)
        {
            return new TBaseDAL<SysRole>().GetListByCondition(Fields, Condition, strOrder);
        }
        public List<SysRole> PageList_SysRole(string Fields, string Condition, string Orderby, int Offset, int Count, ref long RecordTotalCount)
        {
            return new TBaseDAL<SysRole>().GetListByPager(Fields, Condition, Orderby, Offset, Count, ref  RecordTotalCount);
        }

        /// <summary>
        /// 获取用户角色集合
        /// </summary>
        public List<SysRole> List_SysRole(string UserID)
        {
            return new BaseDAL().GetListByCondition<SysRole>("RoleID,RoleName", ViewNames.V_USERROLE, " AND UserID='" + UserID + "'", "RoleName DESC");
        }
        
        /// <summary>
        /// 获取权限信息
        /// </summary>
        /// <param name="Fields">如何为空则查询所有</param>
        /// <param name="Condition">条件需要加上关键字AND</param>
        /// <param name="Order">不接Order by的排序</param>
        /// <returns></returns>
        public List<Guid> List_SysRoleFuncLink(string Fields, string Condition, string strOrder)
        {
            List<SysRoleFuncLink> limits = new TBaseDAL<SysRoleFuncLink>().GetListByCondition(Fields, Condition, strOrder);
            List<Guid> listLimit_ids = limits.Select(c => c.FuncID).ToList();
            return listLimit_ids;
        }

      

        #endregion 角色管理


        #region 权限控制

        public List<string> GetUserALLFunc(string UserID)
        {
            List<string> list = new BaseDAL().GetListByCondition<string>("FuncKey", ViewNames.V_ALLUSERLIMIT, " and  cast(UserID as varchar(50))='" + UserID + "'", null);
            return list;
        }
        /// <summary>
        /// 递归遍历模块，得到菜单
        /// </summary>
        public List<MenuModuleInfo> List_BlogModuleMenuData(string UserID)
        {
            string currdate = DateTime.Now.ToString("yyyy-MM-dd HH:mm");
            List<SysFunction> listModules = new BaseDAL().GetListByCondition<SysFunction>(" FuncID,FuncName,FuncKey,IconPath,FUNCTYPE,Source,FuncPID,FuncSort", ViewNames.V_ALLUSERLIMIT, "  and Functype=0 and (Status=1 OR (Status=0 AND StartTime<='" + currdate + "' AND EndTime>'" + currdate + "'))  AND cast(UserID as varchar(50))='" + UserID + "'", "FuncSort DESC");
            MenuModuleInfo menu = new MenuModuleInfo();
            List<MenuModuleInfo> modulelimitGroup = new List<MenuModuleInfo>();
            RecurseModule(listModules, "9c54e604-023a-4ccf-93bb-e25ce40f02ad", ref modulelimitGroup);
            return modulelimitGroup;
        }
        void RecurseModule(List<SysFunction> listModules, string pid, ref  List<MenuModuleInfo> modulelimitGroup)
        {
            foreach (SysFunction item in listModules.Where(info => info.FuncPID == pid))
            {
                MenuModuleInfo modulelimit = new MenuModuleInfo();
                modulelimit.menuid = item.FuncID.ToString();
                modulelimit.menuname = item.FuncName;
                modulelimit.icon = item.IconPath;
                modulelimit.url = item.Source;
                List<MenuModuleInfo> menuchildren = new List<MenuModuleInfo>();
                RecurseModule(listModules, item.FuncID.ToString
                    (), ref menuchildren);
                modulelimit.menus = menuchildren;
                modulelimitGroup.Add(modulelimit);

            }
        }


        public bool Operate_SysFunction(SysFunction info, bool IsInsert)
        {
            if (IsInsert)
                return new TBaseDAL<SysFunction>().InsertInstance(info) > 0;
            else
                return new TBaseDAL<SysFunction>().UpdateInstance(info) > 0;
        }


        /// <summary>
        /// 删除角色权限关系
        /// </summary>
        /// <param name="Condition"></param>
        public bool Delete_SysFunction(string Condition)
        {
            return new TBaseDAL<SysFunction>().DeleteInstances(Condition) > 0;
        }
        public SysFunction Get_SysFunction(string Condition)
        {
            SysFunction info = new TBaseDAL<SysFunction>().GetInstanceByCondition(Condition);
            return info;
        }

        public List<SysFunction> PageList_SysFunction(string Fields, string Condition, string Orderby, int Offset, int Count, ref long RecordTotalCount)
        {
            return new TBaseDAL<SysFunction>().GetListByPager(Fields, Condition, Orderby, Offset, Count, ref  RecordTotalCount);
        }
        /// <summary>
        /// 获取全部权限点
        /// </summary>
        public List<SysFunction> List_SysFunction(string Fields, string Condition, string strOrder)
        {
            return new TBaseDAL<SysFunction>().GetListByCondition(Fields, Condition, strOrder);
        }


        //public List<TreeModuleInfo> GetNestTreeByCondition(string PID,string Fields, string TableName, string Condition, string strOrder)
        //{
        //    List<SysFunction> Alllist = new BaseDAL().GetListByCondition<SysFunction>(Fields, TableName, Condition, strOrder);
        //    List<TreeModuleInfo> Outlist = new List<TreeModuleInfo>();
        //    //NestTreeRecursion(Alllist, "-1", Outlist);
        //    return Outlist;
        //}

        public List<TreeModuleInfo> GetNestTreeByCondition(string Fields, string TableName, string Condition, string strOrder)
        {
            List<SysFunction> Alllist = new BaseDAL().GetListByCondition<SysFunction>(Fields, TableName, Condition, strOrder);
            List<TreeModuleInfo> Outlist = new List<TreeModuleInfo>();
            foreach (SysFunction item in Alllist)
            {
                TreeModuleInfo info = new TreeModuleInfo();
                TreeAttributeInfo attr = new TreeAttributeInfo();
                attr.type = item.FuncType;
                info.id = item.FuncID.ToString();
                info.pid = item.FuncPID;
                info.text = item.FuncName;
                List<TreeModuleInfo> children = new List<TreeModuleInfo>();
                NestTreeRecursion(Alllist, info.id, children);
                info.children = children;
                Outlist.Add(info);
            }
            return Outlist;
        }
        void NestTreeRecursion(List<SysFunction> Alllist, string PID, List<TreeModuleInfo> Outlist)
        {
            foreach (SysFunction item in Alllist.Where(info => info.FuncPID == PID))
            {
                TreeModuleInfo info = new TreeModuleInfo();
                TreeAttributeInfo attr = new TreeAttributeInfo();
                attr.type = item.FuncType;
                info.id = item.FuncID.ToString();
                info.pid = item.FuncPID;
                info.text = item.FuncName;
                List<TreeModuleInfo> children = new List<TreeModuleInfo>();
                NestTreeRecursion(Alllist, info.id, children);
                info.children = children;
                Outlist.Add(info);
            }

        }
        #endregion

    }
}
