using ET.Constant.DBConst;
using ET.DALContract;
using ET.Sys_DEF;
using ET.ToolKit.DBLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ET.Sys_BLL
{
    public class OrganizationBLL
    {
        #region 公司管理

        public bool Update_UserCompany(UserCompany info, bool isinsert)
        {
            if (isinsert)
                return new TSqlBaseDAL<UserCompany>().Insert(info) > 0;
            else
                return new TSqlBaseDAL<UserCompany>().Update(info) > 0;
        }

        public bool Delete_UserCompany(string condition)
        {
            return new TSqlBaseDAL<UserCompany>().Delete(condition) > 0;
        }

        public UserCompany Get_UserCompany(string condition)
        {
            UserCompany info = new TSqlBaseDAL<UserCompany>().GetByCondition(condition);
            return info;
        }

        public List<UserCompany> List_UserCompany(string fields, string condition, string orderby)
        {
            return new TSqlBaseDAL<UserCompany>().GetListByCondition(fields, condition, orderby);
        }

        #endregion 部门管理

        #region 部门管理

        public bool Update_UserDepartment(UserDepartment info, bool isinsert)
        {
            if (isinsert)
                return new TSqlBaseDAL<UserDepartment>().Insert(info) > 0;
            else
                return new TSqlBaseDAL<UserDepartment>().Update(info) > 0;
        }

        public bool Delete_UserDepartment(string condition)
        {
            return new TSqlBaseDAL<UserDepartment>().Delete(condition) > 0;
        }

        public UserDepartment Get_UserDepartment(string condition)
        {
            UserDepartment info = new TSqlBaseDAL<UserDepartment>().GetByCondition(condition);
            return info;
        }

        public List<UserDepartment> Get_UserDepartments(string condition)
        {
            List<UserDepartment> infos = new TSqlBaseDAL<UserDepartment>().GetsByCondition(condition);
            return infos;
        }

        #endregion 部门管理

        #region 岗位管理

        public bool Update_UserPosition(UserPosition info, bool isinsert)
        {
            if (isinsert)
                return new TSqlBaseDAL<UserPosition>().Insert(info) > 0;
            else
                return new TSqlBaseDAL<UserPosition>().Update(info) > 0;
        }

        public bool Delete_UserPosition(string condition)
        {
            return new TSqlBaseDAL<UserPosition>().Delete(condition) > 0;
        }

        public UserPosition Get_UserPosition(string condition)
        {
            UserPosition info = new TSqlBaseDAL<UserPosition>().GetByCondition(condition);
            return info;
        }

        public List<UserPosition> Get_UserPositions(string condition)
        {
            List<UserPosition> infos = new TSqlBaseDAL<UserPosition>().GetsByCondition(condition);
            return infos;
        }

        #endregion 岗位管理

        #region 用户管理


        public bool Update_UserProperty(UserProperty info)
        {
            if (info.UserID == null)
            {
                return false;
            }
            return new TSqlBaseDAL<UserProperty>().Update(info) > 0;
        }

        public UserProperty Get_UserProperty(string condition)
        {
            UserProperty info = new TSqlBaseDAL<UserProperty>().GetByCondition(condition);
            return info;
        }

        public UserProperty Get_UserPropertyByID(string infoid)
        {
            UserProperty info = new TSqlBaseDAL<UserProperty>().GetById(infoid);

            return info;
        }

        public List<UserProperty> List_UserProperty(string fields, string condition, string orderby)
        {
            return new TSqlBaseDAL<UserProperty>().GetListByCondition(fields, condition, orderby);
        }

        public List<UserProperty> Pagination_UserProperty(string fields, string condition, string orderby, int pagesize, int pageindex, ref long totalcount)
        {
            return new TSqlBaseDAL<UserProperty>().GetListByPager(fields, condition, orderby, pagesize, pageindex, ref  totalcount);
        }

        public List<UserFullProperty> Pagination_UserFullProperty(string fields, string condition, string orderby, int pagesize, int pageindex, ref long totalcount)
        {
            return new PublicBLL().GetListByPager<UserFullProperty>(fields, ViewNames.V_USERFULL, condition, orderby, pagesize, pageindex, ref  totalcount);
        }

        public UserFullProperty Get_UserFullProperty(string fields, string condition)
        {
            UserFullProperty info = new PublicBLL().GetObjectByCondition<UserFullProperty>(fields, ViewNames.V_USERFULL, condition, null);
            return info;
        }

        public UserFullProperty Get_UserFullPropertyByID(string userID)
        {
            return Get_UserFullProperty("*", " AND USERID='" + userID + "'");
        }

        public bool Update_UserBase(UserBase info)
        {
            if (info.UserID == null)
            {
                return false;
            }
            return new TSqlBaseDAL<UserBase>().Update(info) > 0;
        }

        public UserBase Get_UserBase(string condition)
        {
            UserBase info = new TSqlBaseDAL<UserBase>().GetByCondition(condition);
            return info;
        }

        /// <summary>
        /// 新增或更新用户所有信息
        /// </summary>
        public bool Update_UserInfo(UserFullInfo info, bool isinsert)
        {
            return new SqlBaseDAL().TransactionForVoid(new Action<DataBase>(delegate(DataBase dataBase)
            {
                if (isinsert)
                {
                    new TSqlBaseDAL<UserBase>().Insert(dataBase, info.userbaseinfo);
                    info.userstuinfo.UserID = info.userbaseinfo.UserID;
                    new TSqlBaseDAL<UserProperty>().Insert(dataBase, info.userstuinfo);
                    if (info.userrole != null)
                        foreach (UserRoleLink role in info.userrole)
                        {
                            role.UserID = info.userbaseinfo.UserID;
                            new TSqlBaseDAL<UserRoleLink>().Insert(dataBase, role);
                        }
                }
                else
                {
                    new TSqlBaseDAL<UserBase>().Update(dataBase, info.userbaseinfo);
                    new TSqlBaseDAL<UserProperty>().Update(dataBase, info.userstuinfo);
                    new TSqlBaseDAL<UserRoleLink>().Delete(dataBase, " and UserID='" + info.userbaseinfo.UserID.ToString() + "'");
                    if (info.userrole != null)
                        foreach (UserRoleLink role in info.userrole)
                        {
                            role.UserID = info.userbaseinfo.UserID;
                            new TSqlBaseDAL<UserRoleLink>().Insert(dataBase, role);
                        }
                }
            }));
        }

        /// <summary>
        /// 新增或更新用户列表所有信息
        /// </summary>
        public bool Update_UserInfo(UserFullInfo[] info, bool isinsert)
        {

            for (int i = 0; i < info.Length; i++)
            {
                Update_UserInfo(info[i], isinsert);
            }
            return true;
        }

        /// <summary>
        /// 禁用用户状态
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        public bool Update_DisableUser(string condition)
        {
            if (string.IsNullOrEmpty(condition))
                return false;
            return new PublicBLL().ExecuteSqlNonQuery(string.Format("UPDATE {0} SET Status=-1 WHERE 1=1" + condition, TableNames.UserBase)) > 0;
        }

        /// <summary>
        /// 启用用户状态
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        public bool Update_EnabledUser(string condition)
        {
            if (string.IsNullOrEmpty(condition))
                return false;
            return new PublicBLL().ExecuteSqlNonQuery(string.Format("UPDATE {0} SET Status=1 WHERE 1=1" + condition, TableNames.UserBase)) > 0;
        }

        /// <summary>
        /// 删除账户信息
        /// </summary>
        /// <param name="condition"></param>
        public bool Delete_UserInfo(string condition)
        {
            return new SqlBaseDAL().TransactionForVoid(new Action<DataBase>(delegate(DataBase dataBase)
            {
                new TSqlBaseDAL<UserRoleLink>().Delete(dataBase, condition);
                new TSqlBaseDAL<UserProperty>().Delete(dataBase, condition);
                new TSqlBaseDAL<UserBase>().Delete(dataBase, condition);
            }));
        }

        /// <summary>
        /// 获取帐号信息
        /// </summary>
        /// <param name="userid"></param>
        /// <returns></returns>
        public UserFullInfo Get_UserInfo(string userid)
        {
            UserFullInfo info = new UserFullInfo();
            string condition = " AND UserID='" + userid + "'";
            info.userbaseinfo = new TSqlBaseDAL<UserBase>().GetByCondition(condition);
            if (info.userbaseinfo == null)
                return null;
            info.userstuinfo = new TSqlBaseDAL<UserProperty>().GetByCondition(condition);
            if (info.userstuinfo == null)
                return null;
            info.userrole = new TSqlBaseDAL<UserRoleLink>().GetsByCondition(condition);
            return info;
        }

        public bool Update_UserRoleLink(UserRoleLink info, bool isinsert)
        {
            if (isinsert)
                return new TSqlBaseDAL<UserRoleLink>().Insert(info) > 0;
            else
                return new TSqlBaseDAL<UserRoleLink>().Update(info) > 0;
        }

        public bool Delete_UserRoleLink(string condition)
        {
            return new TSqlBaseDAL<UserRoleLink>().Delete(condition) > 0;
        }

        public UserRoleLink Get_UserRoleLink(string condition)
        {
            UserRoleLink info = new TSqlBaseDAL<UserRoleLink>().GetByCondition(condition);
            return info;
        }

        #endregion 用户管理

        #region 用户组织架构管理

        public bool Update_UserOrgLink(UserOrgLink info, bool isinsert)
        {
            if (isinsert)
                return new TSqlBaseDAL<UserOrgLink>().Insert(info) > 0;
            else
                return new TSqlBaseDAL<UserOrgLink>().Update(info) > 0;
        }

        public bool Delete_UserOrgLink(string condition)
        {
            return new TSqlBaseDAL<UserOrgLink>().Delete(condition) > 0;
        }

        public UserOrgLink Get_UserOrgLink(string condition)
        {
            UserOrgLink info = new TSqlBaseDAL<UserOrgLink>().GetByCondition(condition);
            return info;
        }

        public List<UserOrgLink> List_UserOrgLink(string condition)
        {
            List<UserOrgLink> infos = new TSqlBaseDAL<UserOrgLink>().GetsByCondition(condition);
            return infos;
        }

        #endregion

        #region 岗位组织架构管理

        public bool Update_UserPositionOrgLink(UserPositionOrgLink info, bool isinsert)
        {
            if (isinsert)
                return new TSqlBaseDAL<UserPositionOrgLink>().Insert(info) > 0;
            else
                return new TSqlBaseDAL<UserPositionOrgLink>().Update(info) > 0;
        }

        public bool Delete_UserPositionOrgLink(string condition)
        {
            return new TSqlBaseDAL<UserPositionOrgLink>().Delete(condition) > 0;
        }

        public UserPositionOrgLink Get_UserPositionOrgLink(string condition)
        {
            UserPositionOrgLink info = new TSqlBaseDAL<UserPositionOrgLink>().GetByCondition(condition);
            return info;
        }

        public List<UserPositionOrgLink> List_UserPositionOrgLink(string condition)
        {
            List<UserPositionOrgLink> infos = new TSqlBaseDAL<UserPositionOrgLink>().GetsByCondition(condition);
            return infos;
        }

        #endregion

        #region 部门权限管理

        public bool Update_UserDeptFuncLink(UserDeptFuncLink info, bool isinsert)
        {
            if (isinsert)
                return new TSqlBaseDAL<UserDeptFuncLink>().Insert(info) > 0;
            else
                return new TSqlBaseDAL<UserDeptFuncLink>().Update(info) > 0;
        }

        public bool Delete_UserDeptFuncLink(string condition)
        {
            return new TSqlBaseDAL<UserDeptFuncLink>().Delete(condition) > 0;
        }

        public UserDeptFuncLink Get_UserDeptFuncLink(string condition)
        {
            UserDeptFuncLink info = new TSqlBaseDAL<UserDeptFuncLink>().GetByCondition(condition);
            return info;
        }

        public List<UserDeptFuncLink> List_UserDeptFuncLink(string condition)
        {
            List<UserDeptFuncLink> infos = new TSqlBaseDAL<UserDeptFuncLink>().GetsByCondition(condition);
            return infos;
        }

        #endregion 岗位管理

        #region 岗位权限管理

        public bool Update_UserPostFuncLink(UserPostFuncLink info, bool isinsert)
        {
            if (isinsert)
                return new TSqlBaseDAL<UserPostFuncLink>().Insert(info) > 0;
            else
                return new TSqlBaseDAL<UserPostFuncLink>().Update(info) > 0;
        }

        public bool Delete_UserPostFuncLink(string condition)
        {
            return new TSqlBaseDAL<UserPostFuncLink>().Delete(condition) > 0;
        }

        public UserPostFuncLink Get_UserPostFuncLink(string condition)
        {
            UserPostFuncLink info = new TSqlBaseDAL<UserPostFuncLink>().GetByCondition(condition);
            return info;
        }

        public List<UserPostFuncLink> List_UserPostFuncLink(string condition)
        {
            List<UserPostFuncLink> infos = new TSqlBaseDAL<UserPostFuncLink>().GetsByCondition(condition);
            return infos;
        }

        #endregion 岗位管理

        #region 用户权限管理

        public bool Update_UserFuncLink(UserFuncLink info, bool isinsert)
        {
            if (isinsert)
                return new TSqlBaseDAL<UserFuncLink>().Insert(info) > 0;
            else
                return new TSqlBaseDAL<UserFuncLink>().Update(info) > 0;
        }

        public bool Delete_UserFuncLink(string condition)
        {
            return new TSqlBaseDAL<UserFuncLink>().Delete(condition) > 0;
        }

        public UserFuncLink Get_UserFuncLink(string condition)
        {
            UserFuncLink info = new TSqlBaseDAL<UserFuncLink>().GetByCondition(condition);
            return info;
        }

        public List<UserFuncLink> List_UserFuncLink(string condition)
        {
            List<UserFuncLink> infos = new TSqlBaseDAL<UserFuncLink>().GetsByCondition(condition);
            return infos;
        }

        #endregion 岗位管理
    }
}
