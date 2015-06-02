
using System;
using System.Data;

using System.Collections.Generic;

namespace ET.Sys_DEF
{
    [Serializable]
    [Table(TableName = "UserPositionInfo", PrimaryKey = "PostID", PrimaryKeyType = typeof(Guid))]
    public class UserPositionInfo 
	{

        public Guid PostID { get; set; }


        public String PostName { get; set; }


        public String PostSort { get; set; }


        public String PostDescription { get; set; }


	
	}
}
