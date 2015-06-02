
using System;
using System.Data;

using System.Collections.Generic;

namespace ET.Sys_DEF
{
    [Serializable]
    [Table(TableName = "UserRoleLink", PrimaryKey = "LinkID", PrimaryKeyType = typeof(Guid))]
    public class UserRoleLink 
	{
        public Guid LinkID { get; set; }


        public Guid UserID { get; set; }


        public Guid RoleID { get; set; }
	
	}
}
