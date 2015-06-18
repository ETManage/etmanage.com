using System;
using System.Collections.Generic;
using System.Text;

namespace ET.Constant.DBConst
{
    public static class ViewNames
    {
        #region 系统框架模块

        /// <summary>
      

        /// <summary>
        /// 用户角色
        /// </summary>
        public const string V_USERROLE = "V_USERROLE";


        /// <summary>
        /// 用户信息，仅仅显示未冻结用户
        /// </summary>
        public const string V_AVAILABLEUSER = "V_AVAILABLEUSER";

        /// <summary>
        /// 所有用户信息，包括冻结和未冻结的
        /// </summary>
        public const string V_USERFULL = "V_USERFULL";
        /// <summary>
        /// 所有用户权限信息
        /// </summary>
        public const string V_ALLUSERLIMIT = "V_ALLUSERLIMIT";


        /// <summary>
        /// 所有角色权限信息
        /// </summary>
        public const string V_ALLROLELIMIT = "V_ALLROLELIMIT";
        #endregion



    }
}
