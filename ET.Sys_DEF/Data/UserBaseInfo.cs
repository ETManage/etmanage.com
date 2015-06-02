
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
        /// �û�״̬��1Ϊ�����ƣ�0����,-1Ϊ����
        /// </summary>
        public Int32? UserStatus { get; set; }

        public DateTime? UserStartTime { get; set; }

        public DateTime? UserEndTime { get; set; }
		
	}
}
