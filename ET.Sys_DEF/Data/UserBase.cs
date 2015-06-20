
using System;
using System.Data;

using System.Collections.Generic;
namespace ET.Sys_DEF
{
    [Serializable]
    [Table(TableName = "UserBase", PrimaryKey = "UserID", PrimaryKeyType = typeof(Guid))]
    public class UserBase
    {
        [PrimaryKey(true)]
        public Guid UserID { get; set; }
        public String UserName { get; set; }

        public String UserPwd { get; set; }

        /// <summary>
        /// �û�״̬��1Ϊ�����ƣ�0����,-1Ϊ����
        /// </summary>
        public Int32? Status { get; set; }
        public Int32? UserType { get; set; }
        public DateTime? StartTime { get; set; }

        public DateTime? EndTime { get; set; }
		
	}
}
