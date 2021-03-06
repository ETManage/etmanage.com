
using System;
using System.Data;

using System.Collections.Generic;
namespace ET.Sys_DEF
{
    [Serializable]
    [Table(TableName = "UserOrgLink", PrimaryKey = "LinkID", PrimaryKeyType = typeof(Guid))]
    public class UserOrgLink 
	{


        public Guid LinkID { get; set; }


        public Guid OrgID { get; set; }

        public Guid UserID { get; set; }
        public Guid type { get; set; }
        public DateTime? CreateTime { get; set; }
        public Guid CreateID { get; set; }
        public DateTime? UpdateTime { get; set; }
        public Guid UpdatorID { get; set; }
	}
}
