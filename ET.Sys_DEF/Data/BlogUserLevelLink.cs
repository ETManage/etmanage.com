
using System;
using System.Data;

using System.Collections.Generic;
namespace ET.Sys_DEF
{
    [Serializable]
    [Table(TableName = "BlogUserLevelLink", PrimaryKey = "LinkID", PrimaryKeyType = typeof(Guid))]
    public class BlogUserLevelLink 
	{


        public Guid LinkID { get; set; }


        public Guid UserID { get; set; }
        public Int64? Exp { get; set; }
	}
}
