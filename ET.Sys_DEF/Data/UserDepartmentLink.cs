
using System;
using System.Data;

using System.Collections.Generic;
namespace ET.Sys_DEF
{
    [Serializable]
    [Table(TableName = "UserDepartmentLink", PrimaryKey = "LinkID", PrimaryKeyType = typeof(Guid))]
    public class UserDepartmentLink 
	{


        public Guid LinkID { get; set; }


        public Guid UserID { get; set; }

        public Guid DepID { get; set; }
	}
}
