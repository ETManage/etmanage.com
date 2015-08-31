using System;
using System.Collections.Generic;
using System.Text;

namespace ET.Constant.DBConst
{
    public class Functions
    {
        #region 系统框架模块
        /// <summary>
        /// 将字符串分割为一个数据表，参数：@STR：要分割的字符 ,@splitStr:分隔符：如','
        /// </summary>
        public const string F_StringSplitToTable = "F_StringSplitToTable";

        /// <summary>
        /// 获取用户的所有角色名称@USERID为用户ID
        /// </summary>
        public const string F_GetUserRoleName = "F_GetUserRoleName";
        #endregion
    }
}
