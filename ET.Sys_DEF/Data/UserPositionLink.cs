
using System;
using System.Data;

using System.Collections.Generic;
namespace ET.Sys_DEF
{
    [Serializable]
    [Table(TableName = "UserPositionLink", PrimaryKey = "LinkID", PrimaryKeyType = typeof(Guid))]
    public class UserPositionLink 
	{


        public Guid LinkID { get; set; }


        public Guid UserID { get; set; }

        public Guid PostID { get; set; }
	}
}
