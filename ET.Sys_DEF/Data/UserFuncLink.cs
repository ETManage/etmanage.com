
using System;
using System.Data;

using System.Collections.Generic;
namespace ET.Sys_DEF
{
    [Serializable]
    [Table(TableName = "UserFuncLink", PrimaryKey = "LinkID", PrimaryKeyType = typeof(Guid))]
    public class UserFuncLink 
	{


        public Guid LinkID { get; set; }


        public Guid UserID { get; set; }
        public Guid FuncID { get; set; }
        public Guid CreatorID { get; set; }
        public DateTime? CreateTime { get; set; }
	}
}
