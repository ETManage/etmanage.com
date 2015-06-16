
using System;
using System.Data;

using System.Collections.Generic;
namespace ET.Sys_DEF
{
    [Serializable]
    [Table(TableName = "UserPostFuncLink", PrimaryKey = "LinkID", PrimaryKeyType = typeof(Guid))]
    public class UserPostFuncLink 
	{


        public Guid LinkID { get; set; }


        public Guid PostID { get; set; }
        public Guid FuncID { get; set; }
        public Guid CreatorID { get; set; }
        public DateTime? CreateTime { get; set; }
	}
}
