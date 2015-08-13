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
        public bool Update_SysRole(SysRole info, List<string> roleids, bool isinsert)
        {
            return new SqlBaseDAL().TransactionForVoid(new Action<DataBase>(delegate(DataBase dataBase)
            {
                new TSqlBaseDAL<SysRoleFuncLink>().Delete(" AND RoleID='" + info.RoleID + "'");
                if (isinsert)
                {
                    info.RoleID = Guid.NewGuid();
                    new TSqlBaseDAL<SysRole>().Insert(info);
                    foreach (string item in roleids)
                    {
                        SysRoleFuncLink acinfo = new SysRoleFuncLink();
                        acinfo.FuncID = Guid.Parse(item);
                        acinfo.RoleID = info.RoleID;
                        new TSqlBaseDAL<SysRoleFuncLink>().Insert(acinfo);
                    }


                }
                else
                {
                    new TSqlBaseDAL<SysRole>().Update(info);
                    foreach (string item in roleids)
                    {
                        SysRoleFuncLink acinfo = new SysRoleFuncLink();
                        acinfo.FuncID = Guid.Parse(item);
                        acinfo.RoleID = info.RoleID;
                        new TSqlBaseDAL<SysRoleFuncLink>().Insert(acinfo);
                    }

                }

            }));
        }

        public bool Delete_SysRole(string condition)
        {
            return new SqlBaseDAL().TransactionForVoid(new Action<DataBase>(delegate(DataBase dataBase)
            {
                new TSqlBaseDAL<UserRoleLink>().Delete(dataBase, condition);
                new TSqlBaseDAL<SysRoleFuncLink>().Delete(dataBase, condition);
                new TSqlBaseDAL<SysRole>().Delete(dataBase, condition);
            }));
        }

        /// <summary>
        /// 根据ID删除,如果只有一个ID则不用带""，如果有多个则需要每个值带""
        /// </summary>
        /// <param name="condition"></param>
        public bool Delete_SysRoleByID(string ids)
        {
            return new TSqlBaseDAL<SysRole>().DeleteByID(ids) > 0;
        }

        public SysRole Get_SysRole(string condition)
        {
            SysRole info = new TSqlBaseDAL<SysRole>().GetByCondition(condition);
            return info;
        }

        public List<SysRole> List_SysRole(string fields, string condition, string orderby)
        {
            return new TSqlBaseDAL<SysRole>().GetListByCondition(fields, condition, orderby);
        }
        /// <summary>
        /// 根据用户ID获取该用户的所有角色
        /// </summary>
        public List<SysRole> List_SysRole(string userid)
        {
            return new SqlBaseDAL().GetListByCondition<SysRole>("RoleID,RoleName", ViewNames.V_USERROLE, " AND userid='" + userid + "'", "RoleName DESC");
        }

        public List<SysRole> Pagination_SysRole(string fields, string condition, string orderby, int pagesize, int pageindex, ref long totalcount)
        {
            return new TSqlBaseDAL<SysRole>().GetListByPager(fields, condition, orderby, pagesize, pageindex, ref  totalcount);
        }
        
        public bool Update_SysRoleFuncLink(SysRoleFuncLink info, bool isinsert)
        {
            if (isinsert)
                return new TSqlBaseDAL<SysRoleFuncLink>().Insert(info) > 0;
            else
                return new TSqlBaseDAL<SysRoleFuncLink>().Update(info) > 0;

        }

        public bool Delete_SysRoleFuncLink(string condition)
        {
            return new TSqlBaseDAL<SysRoleFuncLink>().Delete(condition) > 0;
        }

        /// <summary>
        /// 获取权限信息
        /// </summary>
        /// <param name="fields">如何为空则查询所有</param>
        /// <param name="condition">条件需要加上关键字AND</param>
        /// <param name="Order">不接Order by的排序</param>
        /// <returns></returns>
        public List<Guid> List_SysRoleFuncLink(string fields, string condition, string orderby)
        {
            List<SysRoleFuncLink> limits = new TSqlBaseDAL<SysRoleFuncLink>().GetListByCondition(fields, condition, orderby);
            List<Guid> listLimit_ids = limits.Select(c => c.FuncID).ToList();
            return listLimit_ids;
        }

        #endregion 角色管理

        #region 权限控制
        public bool Update_SysFunction(SysFunction info, bool isinsert)
        {
            if (isinsert)
                return new TSqlBaseDAL<SysFunction>().Insert(info) > 0;
            else
                return new TSqlBaseDAL<SysFunction>().Update(info) > 0;
        }

        public bool Delete_SysFunction(string condition)
        {
            new OrganizationBLL().Delete_UserFuncLink(condition);
            new OrganizationBLL().Delete_UserDeptFuncLink(condition);
            new OrganizationBLL().Delete_UserPostFuncLink(condition);
            new SystemBLL().Delete_SysRoleFuncLink(condition);
            return new TSqlBaseDAL<SysFunction>().Delete(condition) > 0;
        }

        public SysFunction Get_SysFunction(string condition)
        {
            SysFunction info = new TSqlBaseDAL<SysFunction>().GetByCondition(condition);
            return info;
        }

        public List<SysFunction> List_SysFunction(string fields, string condition, string orderby)
        {
            return new TSqlBaseDAL<SysFunction>().GetListByCondition(fields, condition, orderby);
        }

        public List<SysFunction> Pagination_SysFunction(string fields, string condition, string orderby, int pagesize, int pageindex, ref long totalcount)
        {
            return new TSqlBaseDAL<SysFunction>().GetListByPager(fields, condition, orderby, pagesize, pageindex, ref  totalcount);
        }

        public List<TreeModuleInfo> GetNestTreeByCondition(string fields, string tablename, string condition, string orderby)
        {
            List<SysFunction> alllist = new SqlBaseDAL().GetListByCondition<SysFunction>(fields, tablename, condition, orderby);
            List<TreeModuleInfo> Outlist = new List<TreeModuleInfo>();
            foreach (SysFunction item in alllist)
            {
                TreeModuleInfo info = new TreeModuleInfo();
                TreeAttributeInfo attr = new TreeAttributeInfo();
                attr.type = item.FuncType;
                info.id = item.FuncID.ToString();
                info.pid = item.FuncPID;
                info.text = item.FuncName;
                List<TreeModuleInfo> children = new List<TreeModuleInfo>();
                NestTreeRecursion(alllist, info.id, children);
                info.children = children;
                Outlist.Add(info);
            }
            return Outlist;
        }
        private void NestTreeRecursion(List<SysFunction> alllist, string pid, List<TreeModuleInfo> outlist)
        {
            foreach (SysFunction item in alllist.Where(info => info.FuncPID == pid))
            {
                TreeModuleInfo info = new TreeModuleInfo();
                TreeAttributeInfo attr = new TreeAttributeInfo();
                attr.type = item.FuncType;
                info.id = item.FuncID.ToString();
                info.pid = item.FuncPID;
                info.text = item.FuncName;
                List<TreeModuleInfo> children = new List<TreeModuleInfo>();
                NestTreeRecursion(alllist, info.id, children);
                info.children = children;
                outlist.Add(info);
            }

        }


        /// <summary>
        /// 根据用户ID获取该用户的所有权限
        /// </summary>
        public List<string> GetUserALLFunc(string userid)
        {
            List<string> list = new SqlBaseDAL().GetListByCondition<string>("FuncKey", ViewNames.V_ALLUSERLIMIT, " and  cast(userid as varchar(50))='" + userid + "'", null);
            return list;
        }

        /// <summary>
        /// 递归遍历模块，得到菜单
        /// </summary>
        public List<MenuModuleInfo> List_BlogModuleMenuData(string userid)
        {
            string currdate = DateTime.Now.ToString("yyyy-MM-dd HH:mm");
            List<SysFunction> listModules = new SqlBaseDAL().GetListByCondition<SysFunction>("distinct FuncID,FuncName,FuncKey,IconPath,FUNCTYPE,Source,FuncPID,FuncSort", ViewNames.V_ALLUSERLIMIT, "  and Functype=0 and (Status=1 OR (Status=0 AND StartTime<='" + currdate + "' AND EndTime>'" + currdate + "')) " + (userid == "a50ca689-1748-455d-b4b4-2c9303e186be" ? "" : " AND cast(userid as varchar(50))='" + userid + "'"), "FuncSort DESC");
            MenuModuleInfo menu = new MenuModuleInfo();
            List<MenuModuleInfo> modulelimitGroup = new List<MenuModuleInfo>();
            RecurseModule(listModules, "9c54e604-023a-4ccf-93bb-e25ce40f02ad", ref modulelimitGroup);
            return modulelimitGroup;
        }

        private void RecurseModule(List<SysFunction> listmodules, string pid, ref  List<MenuModuleInfo> modulelimitgroup)
        {
            foreach (SysFunction item in listmodules.Where(info => info.FuncPID == pid))
            {
                MenuModuleInfo modulelimit = new MenuModuleInfo();
                modulelimit.menuid = item.FuncID.ToString();
                modulelimit.menuname = item.FuncName;
                modulelimit.icon = item.IconPath;
                modulelimit.url = item.Source;
                List<MenuModuleInfo> menuchildren = new List<MenuModuleInfo>();
                RecurseModule(listmodules, item.FuncID.ToString
                    (), ref menuchildren);
                modulelimit.menus = menuchildren;
                modulelimitgroup.Add(modulelimit);

            }
        }

        #endregion

    }
}
