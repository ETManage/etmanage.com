
using System;
using System.Data;

using System.Collections.Generic;
namespace ET.Sys_DEF
{
    [Serializable]
    [Table(TableName = "BlogUserLevel", PrimaryKey = "LevelID", PrimaryKeyType = typeof(Guid))]
    public class BlogUserLevel 
	{


        public Guid LevelID { get; set; }


        public string LevelName { get; set; }
        public Int64? NeedExp { get; set; }
	}
}
