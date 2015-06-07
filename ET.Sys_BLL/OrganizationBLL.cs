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
                    new TBaseDAL<UserRoleLink>(dataBase).DeleteInstances(" and UserID='" + info.userbaseinfo.UserID.ToString()+"'");
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

        public bool Operate_User_Info(User_Full_Info[] info,bool IsInsert)
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
                string Condition = " AND UserID='" + userid+"'";
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
            string Condition = " AND UserID='" + userid+"'";
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
    }
}
