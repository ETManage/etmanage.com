
using System;
using System.Data;

using System.Collections.Generic;
namespace ET.Sys_DEF
{
    [Serializable]
    [Table(TableName = "UserBaseInfo", PrimaryKey = "UserID", PrimaryKeyType = typeof(Guid))]
    public class UserBaseInfo
    {
        [PrimaryKey(true)]
        public Guid UserID { get; set; }
        public String UserName { get; set; }

        public String UserPwd { get; set; }

        /// <summary>
        /// 用户状态，1为不限制，0限制,-1为禁用
        /// </summary>
        public Int32? UserStatus { get; set; }

        public DateTime? UserStartTime { get; set; }

        public DateTime? UserEndTime { get; set; }
		
	}
}
