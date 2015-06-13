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

        /// <summary>
        /// 操作信息
        /// </summary>
        /// <param name="info">部门信息</param>
       public bool Operate_UserCompanyInfo(UserCompanyInfo info, bool IsInsert)
        {
            if (IsInsert)
                return new TBaseDAL<UserCompanyInfo>().InsertInstance(info) > 0;
            else
                return new TBaseDAL<UserCompanyInfo>().UpdateInstance(info) > 0;
        }

        /// <summary>
        /// 删除信息
        /// </summary>
        /// <param name="Condition"></param>
       public bool Delete_UserCompanyInfo(string Condition)
        {
            return new TBaseDAL<UserCompanyInfo>().DeleteInstances(Condition) > 0;
        }

        /// <summary>
        /// 获取公司信息
        /// </summary>
        /// <param name="condition">查询条件</param>
        /// <returns>部门信息</returns>
       public UserCompanyInfo Get_UserCompanyInfo(string Condition)
        {
            UserCompanyInfo info = new TBaseDAL<UserCompanyInfo>().GetInstanceByCondition(Condition);
            return info;
        }

       /// <summary>
       /// 获取所有公司列表
       /// </summary>
       public List<UserCompanyInfo> List_UserCompanyInfo(string Fields, string Condition, string strOrder)
       {
           return new TBaseDAL<UserCompanyInfo>().GetListByCondition(Fields, Condition, strOrder);
       }

        #endregion 部门管理

        #region 部门管理

        /// <summary>
        /// 操作信息
        /// </summary>
        /// <param name="info">部门信息</param>
       public bool Operate_UserDepartmentInfo(UserDepartmentInfo info, bool IsInsert)
        {
            if (IsInsert)
                return new TBaseDAL<UserDepartmentInfo>().InsertInstance(info) > 0;
            else
                return new TBaseDAL<UserDepartmentInfo>().UpdateInstance(info) > 0;
        }

        /// <summary>
        /// 删除信息
        /// </summary>
        /// <param name="Condition"></param>
        public bool Delete_UserDepartmentInfo(string Condition)
        {
            return new TBaseDAL<UserDepartmentInfo>().DeleteInstances(Condition) > 0;
        }

        /// <summary>
        /// 获取部门信息
        /// </summary>
        /// <param name="condition">查询条件</param>
        /// <returns>部门信息</returns>
        public UserDepartmentInfo Get_UserDepartmentInfo(string Condition)
        {
            UserDepartmentInfo info = new TBaseDAL<UserDepartmentInfo>().GetInstanceByCondition(Condition);
            return info;
        }
        /// <summary>
        /// 获取部门信息
        /// </summary>
        /// <param name="condition">查询条件</param>
        /// <returns>部门信息</returns>
        public List<UserDepartmentInfo> Get_UserDepartmentInfos(string Condition)
        {
            List<UserDepartmentInfo> infos = new TBaseDAL<UserDepartmentInfo>().GetInstancesByCondition(Condition);
            return infos;
        }


        #endregion 部门管理

        #region 岗位管理

        /// <summary>
        /// 岗位操作
        /// </summary>
        /// <param name="info">岗位信息</param>
        public bool Operate_UserPositionInfo(UserPositionInfo info, bool IsInsert)
        {
            if (IsInsert)
                return new TBaseDAL<UserPositionInfo>().InsertInstance(info) > 0;
            else
                return new TBaseDAL<UserPositionInfo>().UpdateInstance(info) > 0;
        }

        /// <summary>
        /// 删除岗位信息
        /// </summary>
        /// <param name="Condition"></param>
        public bool Delete_UserPositionInfo(string Condition)
        {
            return new TBaseDAL<UserPositionInfo>().DeleteInstances(Condition) > 0;
        }

        /// <summary>
        /// 获取岗位信息
        /// </summary>
        /// <param name="condition">查询条件</param>
        /// <returns>岗位信息</returns>
        public UserPositionInfo Get_UserPositionInfo(string Condition)
        {
            UserPositionInfo info = new TBaseDAL<UserPositionInfo>().GetInstanceByCondition(Condition);
            return info;
        }

        /// <summary>
        /// 获取岗位信息
        /// </summary>
        /// <param name="condition">查询条件</param>
        /// <returns>岗位信息</returns>
        public List<UserPositionInfo> Get_UserPositionInfos(string Condition)
        {
            List<UserPositionInfo> infos = new TBaseDAL<UserPositionInfo>().GetInstancesByCondition(Condition);
            return infos;
        }


        #endregion 岗位管理


        #region 用户管理



        /// <summary>
        /// 操作账户信息
        /// </summary>
        /// <param name="info"></param>
        public bool Operate_User_Info(User_Full_Info info, bool IsInsert)
        {
            return new BaseDAL().TransactionForVoid(new Action<DataBase>(delegate(DataBase dataBase)
            {
                if (info.userbaseinfo.UserID == Guid.Empty)
                {
                    new TBaseDAL<UserBaseInfo>(dataBase).InsertInstance(info.userbaseinfo);
                    info.userstuinfo.UserID = info.userbaseinfo.UserID;
                    new TBaseDAL<UserPropertyInfo>(dataBase).InsertInstance(info.userstuinfo);
                    foreach (UserRoleLink role in info.userrole)
                    {
                        role.UserID = info.userbaseinfo.UserID;
                        new TBaseDAL<UserRoleLink>(dataBase).InsertInstance(role);
                    }
                }
                else
                {
                    new TBaseDAL<UserBaseInfo>(dataBase).UpdateInstance(info.userbaseinfo);
                    info.userstuinfo.UserID = info.userbaseinfo.UserID;
                    new TBaseDAL<UserPropertyInfo>(dataBase).UpdateInstance(info.userstuinfo);
                    new TBaseDAL<UserRoleLink>(dataBase).DeleteInstances(" and UserID='" + info.userbaseinfo.UserID.ToString() + "'");
                    foreach (UserRoleLink role in info.userrole)
                    {
                        role.UserID = info.userbaseinfo.UserID;
                        new TBaseDAL<UserRoleLink>(dataBase).UpdateInstance(role);
                    }
                }
            }));
        }

        /// <summary>
        /// 只能用户修改用户的基本信息，如果是用户的添加请使用Operate_User_Info
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        public bool Operate_UserPropertyInfo(UserPropertyInfo info)
        {
            if (info.UserID == null)
            {
                return false;
            }
            return new TBaseDAL<UserPropertyInfo>().UpdateInstance(info) > 0;
        }


        /// <summary>
        /// 获取单个模块信息
        /// </summary>
        /// <param name="condition">查询条件</param>
        /// <returns>模块信息</returns>
        public UserPropertyInfo Get_UserPropertyInfoByID(string infoid)
        {
            UserPropertyInfo info = null;
            info = new TBaseDAL<UserPropertyInfo>().GetInstanceById(infoid);

            return info;
        }
        public List<UserPropertyInfo> List_UserPropertyInfo(string Fields, string Condition, string strOrder)
        {
            return new TBaseDAL<UserPropertyInfo>().GetListByCondition(Fields, Condition, strOrder);
        }
        public List<UserPropertyInfo> PageList_UserPropertyInfo(string Fields, string Condition, string Orderby, int Offset, int Count, ref long RecordTotalCount)
        {
            return new TBaseDAL<UserPropertyInfo>().GetListByPager(Fields, Condition, Orderby, Offset, Count, ref  RecordTotalCount);
        }

        /// <summary>
        /// 只能用户修改用户的登录帐号信息，如果是用户的添加请使用Operate_User_Info
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        public bool Operate_UserBaseInfo(UserBaseInfo info)
        {
            if (info.UserID == null)
            {
                return false;
            }
            return new TBaseDAL<UserBaseInfo>().UpdateInstance(info) > 0;
        }

        public bool Operate_User_Info(User_Full_Info[] info, bool IsInsert)
        {

            for (int i = 0; i < info.Length; i++)
            {
                Operate_User_Info(info[i], IsInsert);
            }
            return true;
        }

        /// <summary>
        /// 删除账户信息
        /// </summary>
        /// <param name="Condition"></param>
        public bool Delete_User_Info(string userid)
        {
            return new BaseDAL().TransactionForVoid(new Action<DataBase>(delegate(DataBase dataBase)
            {
                string Condition = " AND UserID='" + userid + "'";
                new TBaseDAL<UserRoleLink>(dataBase).DeleteInstances(Condition);
                new TBaseDAL<UserPropertyInfo>(dataBase).DeleteInstances(Condition);
                new TBaseDAL<UserBaseInfo>(dataBase).DeleteInstances(Condition);
            }));
        }

        /// <summary>
        /// 获取帐号信息
        /// </summary>
        /// <param name="condition"></param>
        /// <returns></returns>
        public UserBaseInfo Get_UserBaseInfo(string Condition)
        {
            UserBaseInfo info = new TBaseDAL<UserBaseInfo>().GetInstanceByCondition(Condition);
            return info;
        }

        /// <summary>
        /// 操作用户-角色关系表
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        public bool Operate_UserRoleLink(UserRoleLink info)
        {
            if (info.LinkID == Guid.Empty)
                return new TBaseDAL<UserRoleLink>().InsertInstance(info) > 0;
            else
                return new TBaseDAL<UserRoleLink>().UpdateInstance(info) > 0;
        }

        /// <summary>
        /// 删除用户-角色关系表
        /// </summary>
        /// <param name="condition"></param>
        /// <returns></returns>
        public bool Delete_UserRoleLink(string Condition)
        {
            return new TBaseDAL<UserRoleLink>().DeleteInstances(Condition) > 0;
        }

        /// <summary>
        /// 获取帐号信息
        /// </summary>
        /// <param name="condition"></param>
        /// <returns></returns>
        public User_Full_Info Get_User_Info(string userid)
        {
            User_Full_Info info = new User_Full_Info();
            string Condition = " AND UserID='" + userid + "'";
            info.userbaseinfo = new TBaseDAL<UserBaseInfo>().GetInstanceByCondition(Condition);
            info.userstuinfo = new TBaseDAL<UserPropertyInfo>().GetInstanceByCondition(Condition);
            info.userrole = new TBaseDAL<UserRoleLink>().GetInstancesByCondition(Condition);
            return info;
        }

        /// <summary>
        /// 获取扩展信息
        /// </summary>
        /// <param name="condition"></param>
        /// <returns></returns>
        public UserPropertyInfo Get_UserPropertyInfo(string Condition)
        {
            UserPropertyInfo info = new TBaseDAL<UserPropertyInfo>().GetInstanceByCondition(Condition);
            return info;
        }

        /// <summary>
        /// 获取扩展信息
        /// </summary>
        /// <param name="querytext">查询列名</param>
        /// <param name="condition">查询条件</param>
        /// <param name="ordertext">排序列名</param>
        /// <returns></returns>

        /// <summary>
        /// 获取帐号角色信息
        /// </summary>
        /// <param name="condition"></param>
        /// <returns></returns>
        public UserRoleLink Get_UserRoleLink(string Condition)
        {
            UserRoleLink info = new TBaseDAL<UserRoleLink>().GetInstanceByCondition(Condition);
            return info;
        }





        #endregion 用户管理
        #region 用户组织架构管理

        /// <summary>
        /// 用户组织架构操作
        /// </summary>
        /// <param name="info">用户组织架构信息</param>
        /// <param name="IsInsert">是否为插入</param>
        public bool Operate_UserOrgLink(UserOrgLink info, bool IsInsert)
        {
            if (IsInsert)
                return new TBaseDAL<UserOrgLink>().InsertInstance(info) > 0;
            else
                return new TBaseDAL<UserOrgLink>().UpdateInstance(info) > 0;
        }

        /// <summary>
        /// 删除用户组织架构信息
        /// </summary>
        /// <param name="Condition"></param>
        public bool Delete_UserOrgLink(string Condition)
        {
            return new TBaseDAL<UserOrgLink>().DeleteInstances(Condition) > 0;
        }

        /// <summary>
        /// 获取用户组织架构信息
        /// </summary>
        /// <param name="condition">查询条件</param>
        /// <returns>用户组织架构信息</returns>
        public UserOrgLink Get_UserOrgLink(string Condition)
        {
            UserOrgLink info = new TBaseDAL<UserOrgLink>().GetInstanceByCondition(Condition);
            return info;
        }

        /// <summary>
        /// 获取用户组织架构信息
        /// </summary>
        /// <param name="condition">查询条件</param>
        /// <returns>用户组织架构信息</returns>
        public List<UserOrgLink> Get_UserOrgLinks(string Condition)
        {
            List<UserOrgLink> infos = new TBaseDAL<UserOrgLink>().GetInstancesByCondition(Condition);
            return infos;
        }


        #endregion 

        #region 岗位组织架构管理

        /// <summary>
        /// 岗位组织架构操作
        /// </summary>
        /// <param name="info">岗位组织架构信息</param>
        public bool Operate_UserPositionOrgLink(UserPositionOrgLink info, bool IsInsert)
        {
            if (IsInsert)
                return new TBaseDAL<UserPositionOrgLink>().InsertInstance(info) > 0;
            else
                return new TBaseDAL<UserPositionOrgLink>().UpdateInstance(info) > 0;
        }

        /// <summary>
        /// 删除岗位组织架构信息
        /// </summary>
        /// <param name="Condition"></param>
        public bool Delete_UserPositionOrgLink(string Condition)
        {
            return new TBaseDAL<UserPositionOrgLink>().DeleteInstances(Condition) > 0;
        }

        /// <summary>
        /// 获取岗位组织架构信息
        /// </summary>
        /// <param name="condition">查询条件</param>
        /// <returns>岗位组织架构信息</returns>
        public UserPositionOrgLink Get_UserPositionOrgLink(string Condition)
        {
            UserPositionOrgLink info = new TBaseDAL<UserPositionOrgLink>().GetInstanceByCondition(Condition);
            return info;
        }

        /// <summary>
        /// 获取岗位组织架构信息
        /// </summary>
        /// <param name="condition">查询条件</param>
        /// <returns>岗位组织架构信息</returns>
        public List<UserPositionOrgLink> Get_UserPositionOrgLinks(string Condition)
        {
            List<UserPositionOrgLink> infos = new TBaseDAL<UserPositionOrgLink>().GetInstancesByCondition(Condition);
            return infos;
        }


        #endregion
        #region 部门权限管理

        /// <summary>
        /// 岗位操作
        /// </summary>
        /// <param name="info">岗位信息</param>
        public bool Operate_UserDeptFuncLink(UserDeptFuncLink info, bool IsInsert)
        {
            if (IsInsert)
                return new TBaseDAL<UserDeptFuncLink>().InsertInstance(info) > 0;
            else
                return new TBaseDAL<UserDeptFuncLink>().UpdateInstance(info) > 0;
        }

        /// <summary>
        /// 删除岗位信息
        /// </summary>
        /// <param name="Condition"></param>
        public bool Delete_UserDeptFuncLink(string Condition)
        {
            return new TBaseDAL<UserDeptFuncLink>().DeleteInstances(Condition) > 0;
        }

        /// <summary>
        /// 获取岗位信息
        /// </summary>
        /// <param name="condition">查询条件</param>
        /// <returns>岗位信息</returns>
        public UserDeptFuncLink Get_UserDeptFuncLink(string Condition)
        {
            UserDeptFuncLink info = new TBaseDAL<UserDeptFuncLink>().GetInstanceByCondition(Condition);
            return info;
        }

        /// <summary>
        /// 获取岗位信息
        /// </summary>
        /// <param name="condition">查询条件</param>
        /// <returns>岗位信息</returns>
        public List<UserDeptFuncLink> Get_UserDeptFuncLinks(string Condition)
        {
            List<UserDeptFuncLink> infos = new TBaseDAL<UserDeptFuncLink>().GetInstancesByCondition(Condition);
            return infos;
        }


        #endregion 岗位管理

        #region 岗位权限管理

        /// <summary>
        /// 岗位操作
        /// </summary>
        /// <param name="info">岗位信息</param>
        public bool Operate_UserPostFuncLink(UserPostFuncLink info, bool IsInsert)
        {
            if (IsInsert)
                return new TBaseDAL<UserPostFuncLink>().InsertInstance(info) > 0;
            else
                return new TBaseDAL<UserPostFuncLink>().UpdateInstance(info) > 0;
        }

        /// <summary>
        /// 删除岗位信息
        /// </summary>
        /// <param name="Condition"></param>
        public bool Delete_UserPostFuncLink(string Condition)
        {
            return new TBaseDAL<UserPostFuncLink>().DeleteInstances(Condition) > 0;
        }

        /// <summary>
        /// 获取岗位信息
        /// </summary>
        /// <param name="condition">查询条件</param>
        /// <returns>岗位信息</returns>
        public UserPostFuncLink Get_UserPostFuncLink(string Condition)
        {
            UserPostFuncLink info = new TBaseDAL<UserPostFuncLink>().GetInstanceByCondition(Condition);
            return info;
        }

        /// <summary>
        /// 获取岗位信息
        /// </summary>
        /// <param name="condition">查询条件</param>
        /// <returns>岗位信息</returns>
        public List<UserPostFuncLink> Get_UserPostFuncLinks(string Condition)
        {
            List<UserPostFuncLink> infos = new TBaseDAL<UserPostFuncLink>().GetInstancesByCondition(Condition);
            return infos;
        }


        #endregion 岗位管理
        #region 用户权限管理

        /// <summary>
        /// 岗位操作
        /// </summary>
        /// <param name="info">岗位信息</param>
        public bool Operate_UserFuncLink(UserFuncLink info, bool IsInsert)
        {
            if (IsInsert)
                return new TBaseDAL<UserFuncLink>().InsertInstance(info) > 0;
            else
                return new TBaseDAL<UserFuncLink>().UpdateInstance(info) > 0;
        }

        /// <summary>
        /// 删除岗位信息
        /// </summary>
        /// <param name="Condition"></param>
        public bool Delete_UserFuncLink(string Condition)
        {
            return new TBaseDAL<UserFuncLink>().DeleteInstances(Condition) > 0;
        }

        /// <summary>
        /// 获取岗位信息
        /// </summary>
        /// <param name="condition">查询条件</param>
        /// <returns>岗位信息</returns>
        public UserFuncLink Get_UserFuncLink(string Condition)
        {
            UserFuncLink info = new TBaseDAL<UserFuncLink>().GetInstanceByCondition(Condition);
            return info;
        }

        /// <summary>
        /// 获取岗位信息
        /// </summary>
        /// <param name="condition">查询条件</param>
        /// <returns>岗位信息</returns>
        public List<UserFuncLink> Get_UserFuncLinks(string Condition)
        {
            List<UserFuncLink> infos = new TBaseDAL<UserFuncLink>().GetInstancesByCondition(Condition);
            return infos;
        }


        #endregion 岗位管理
    }
}
