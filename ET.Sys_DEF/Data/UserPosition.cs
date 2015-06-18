
using System;
using System.Data;

using System.Collections.Generic;

namespace ET.Sys_DEF
{
    [Serializable]
    [Table(TableName = "UserPosition", PrimaryKey = "PostID", PrimaryKeyType = typeof(Guid))]
    public class UserPosition 
	{

        public Guid PostID { get; set; }


        public String PostName { get; set; }


        public String PostSort { get; set; }


        public String PostDescription { get; set; }

        public String DepID { get; set; }
        public String CompanyID { get; set; }
        public String CreatorID { get; set; }
        public DateTime? CreateTime { get; set; }
	}
}
