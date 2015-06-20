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

        /// <summary>
        /// 操作信息
        /// </summary>
        /// <param name="info">部门信息</param>
       public bool Operate_UserCompany(UserCompany info, bool IsInsert)
        {
            if (IsInsert)
                return new TBaseDAL<UserCompany>().InsertInstance(info) > 0;
            else
                return new TBaseDAL<UserCompany>().UpdateInstance(info) > 0;
        }

        /// <summary>
        /// 删除信息
        /// </summary>
        /// <param name="Condition"></param>
       public bool Delete_UserCompany(string Condition)
        {
            return new TBaseDAL<UserCompany>().DeleteInstances(Condition) > 0;
        }

        /// <summary>
        /// 获取公司信息
        /// </summary>
        /// <param name="condition">查询条件</param>
        /// <returns>部门信息</returns>
       public UserCompany Get_UserCompany(string Condition)
        {
            UserCompany info = new TBaseDAL<UserCompany>().GetInstanceByCondition(Condition);
            return info;
        }

       /// <summary>
       /// 获取所有公司列表
       /// </summary>
       public List<UserCompany> List_UserCompany(string Fields, string Condition, string strOrder)
       {
           return new TBaseDAL<UserCompany>().GetListByCondition(Fields, Condition, strOrder);
       }

        #endregion 部门管理

        #region 部门管理

        /// <summary>
        /// 操作信息
        /// </summary>
        /// <param name="info">部门信息</param>
       public bool Operate_UserDepartment(UserDepartment info, bool IsInsert)
        {
            if (IsInsert)
                return new TBaseDAL<UserDepartment>().InsertInstance(info) > 0;
            else
                return new TBaseDAL<UserDepartment>().UpdateInstance(info) > 0;
        }

        /// <summary>
        /// 删除信息
        /// </summary>
        /// <param name="Condition"></param>
        public bool Delete_UserDepartment(string Condition)
        {
            return new TBaseDAL<UserDepartment>().DeleteInstances(Condition) > 0;
        }

        /// <summary>
        /// 获取部门信息
        /// </summary>
        /// <param name="condition">查询条件</param>
        /// <returns>部门信息</returns>
        public UserDepartment Get_UserDepartment(string Condition)
        {
            UserDepartment info = new TBaseDAL<UserDepartment>().GetInstanceByCondition(Condition);
            return info;
        }
        /// <summary>
        /// 获取部门信息
        /// </summary>
        /// <param name="condition">查询条件</param>
        /// <returns>部门信息</returns>
        public List<UserDepartment> Get_UserDepartments(string Condition)
        {
            List<UserDepartment> infos = new TBaseDAL<UserDepartment>().GetInstancesByCondition(Condition);
            return infos;
        }


        #endregion 部门管理

        #region 岗位管理

        /// <summary>
        /// 岗位操作
        /// </summary>
        /// <param name="info">岗位信息</param>
        public bool Operate_UserPosition(UserPosition info, bool IsInsert)
        {
            if (IsInsert)
                return new TBaseDAL<UserPosition>().InsertInstance(info) > 0;
            else
                return new TBaseDAL<UserPosition>().UpdateInstance(info) > 0;
        }

        /// <summary>
        /// 删除岗位信息
        /// </summary>
        /// <param name="Condition"></param>
        public bool Delete_UserPosition(string Condition)
        {
            return new TBaseDAL<UserPosition>().DeleteInstances(Condition) > 0;
        }

        /// <summary>
        /// 获取岗位信息
        /// </summary>
        /// <param name="condition">查询条件</param>
        /// <returns>岗位信息</returns>
        public UserPosition Get_UserPosition(string Condition)
        {
            UserPosition info = new TBaseDAL<UserPosition>().GetInstanceByCondition(Condition);
            return info;
        }

        /// <summary>
        /// 获取岗位信息
        /// </summary>
        /// <param name="condition">查询条件</param>
        /// <returns>岗位信息</returns>
        public List<UserPosition> Get_UserPositions(string Condition)
        {
            List<UserPosition> infos = new TBaseDAL<UserPosition>().GetInstancesByCondition(Condition);
            return infos;
        }


        #endregion 岗位管理


        #region 用户管理



        /// <summary>
        /// 操作账户信息
        /// </summary>
        /// <param name="info"></param>
        public bool Operate_User_Info(UserFullInfo info, bool IsInsert)
        {
            return new BaseDAL().TransactionForVoid(new Action<DataBase>(delegate(DataBase dataBase)
            {
                if (IsInsert)
                {
                    new TBaseDAL<UserBase>(dataBase).InsertInstance(info.userbaseinfo);
                    info.userstuinfo.UserID = info.userbaseinfo.UserID;
                    new TBaseDAL<UserProperty>(dataBase).InsertInstance(info.userstuinfo);
                    foreach (UserRoleLink role in info.userrole)
                    {
                        role.UserID = info.userbaseinfo.UserID;
                        new TBaseDAL<UserRoleLink>(dataBase).InsertInstance(role);
                    }
                }
                else
                {
                    new TBaseDAL<UserBase>(dataBase).UpdateInstance(info.userbaseinfo);
                    new TBaseDAL<UserProperty>(dataBase).UpdateInstance(info.userstuinfo);
                    new TBaseDAL<UserRoleLink>(dataBase).DeleteInstances(" and UserID='" + info.userbaseinfo.UserID.ToString() + "'");
                    foreach (UserRoleLink role in info.userrole)
                    {
                        role.UserID = info.userbaseinfo.UserID;
                        new TBaseDAL<UserRoleLink>(dataBase).InsertInstance(role);
                    }
                }
            }));
        }

        /// <summary>
        /// 只能用户修改用户的基本信息，如果是用户的添加请使用Operate_User_Info
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        public bool Operate_UserProperty(UserProperty info)
        {
            if (info.UserID == null)
            {
                return false;
            }
            return new TBaseDAL<UserProperty>().UpdateInstance(info) > 0;
        }


        /// <summary>
        /// 获取单个模块信息
        /// </summary>
        /// <param name="condition">查询条件</param>
        /// <returns>模块信息</returns>
        public UserProperty Get_UserPropertyByID(string infoid)
        {
            UserProperty info = new TBaseDAL<UserProperty>().GetInstanceById(infoid);

            return info;
        }
        public List<UserProperty> List_UserProperty(string Fields, string Condition, string strOrder)
        {
            return new TBaseDAL<UserProperty>().GetListByCondition(Fields, Condition, strOrder);
        }
        public List<UserProperty> PageList_UserProperty(string Fields, string Condition, string Orderby, int Offset, int Count, ref long RecordTotalCount)
        {
            return new TBaseDAL<UserProperty>().GetListByPager(Fields, Condition, Orderby, Offset, Count, ref  RecordTotalCount);
        }
        public List<UserFullProperty> PageList_UserFullProperty(string Fields, string Condition, string Orderby, int Offset, int Count, ref long RecordTotalCount)
        {
            return new PublicBLL().GetListByPager<UserFullProperty>(Fields, ViewNames.V_USERFULL, Condition, Orderby, Offset, Count, ref  RecordTotalCount);
        }
        public UserFullProperty Get_UserFullProperty(string fields, string condition)
        {
            UserFullProperty info = new PublicBLL().GetObjectByCondition<UserFullProperty>(fields, ViewNames.V_USERFULL,condition,null);
            return info;
        }
        public UserFullProperty Get_UserFullPropertyByID(string userID)
        {
            return Get_UserFullProperty("*"," AND USERID='"+userID+"'");
        }
        /// <summary>
        /// 只能用户修改用户的登录帐号信息，如果是用户的添加请使用Operate_User_Info
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        public bool Operate_UserBase(UserBase info)
        {
            if (info.UserID == null)
            {
                return false;
            }
            return new TBaseDAL<UserBase>().UpdateInstance(info) > 0;
        }


        public bool Operate_User_Info(UserFullInfo[] info, bool IsInsert)
        {

            for (int i = 0; i < info.Length; i++)
            {
                Operate_User_Info(info[i], IsInsert);
            }
            return true;
        }
       /// <summary>
       /// 禁用用户状态
       /// </summary>
       /// <param name="info"></param>
       /// <returns></returns>
        public bool Operate_DisableUser(string condition)
        {
            if (string.IsNullOrEmpty(condition))
                return false;
            return new PublicBLL().ExecuteSqlNonQuery(string.Format("UPDATE {0} SET Status=-1 WHERE 1=1"+condition,TableNames.UserBase))>0;
        }
        /// <summary>
        /// 启用用户状态
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        public bool Operate_EnabledUser(string condition)
        {
            if (string.IsNullOrEmpty(condition))
                return false;
            return new PublicBLL().ExecuteSqlNonQuery(string.Format("UPDATE {0} SET Status=1 WHERE 1=1" + condition, TableNames.UserBase)) > 0;
        }

        /// <summary>
        /// 删除账户信息
        /// </summary>
        /// <param name="Condition"></param>
        public bool Delete_User_Info(string condition)
        {
            return new BaseDAL().TransactionForVoid(new Action<DataBase>(delegate(DataBase dataBase)
            {
                new TBaseDAL<UserRoleLink>(dataBase).DeleteInstances(condition);
                new TBaseDAL<UserProperty>(dataBase).DeleteInstances(condition);
                new TBaseDAL<UserBase>(dataBase).DeleteInstances(condition);
            }));
        }

        /// <summary>
        /// 获取帐号信息
        /// </summary>
        /// <param name="condition"></param>
        /// <returns></returns>
        public UserBase Get_UserBase(string Condition)
        {
            UserBase info = new TBaseDAL<UserBase>().GetInstanceByCondition(Condition);
            return info;
        }

        /// <summary>
        /// 操作用户-角色关系表
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        public bool Operate_UserRoleLink(UserRoleLink info, bool IsInsert)
        {
            if (IsInsert)
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
        public UserFullInfo Get_User_Info(string userid)
        {
            UserFullInfo info = new UserFullInfo();
            string Condition = " AND UserID='" + userid + "'";
            info.userbaseinfo = new TBaseDAL<UserBase>().GetInstanceByCondition(Condition);
            if (info.userbaseinfo == null)
                return null;
            info.userstuinfo = new TBaseDAL<UserProperty>().GetInstanceByCondition(Condition);
            if (info.userstuinfo == null)
                return null;
            info.userrole = new TBaseDAL<UserRoleLink>().GetInstancesByCondition(Condition);
            return info;
        }

        /// <summary>
        /// 获取扩展信息
        /// </summary>
        /// <param name="condition"></param>
        /// <returns></returns>
        public UserProperty Get_UserProperty(string Condition)
        {
            UserProperty info = new TBaseDAL<UserProperty>().GetInstanceByCondition(Condition);
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
