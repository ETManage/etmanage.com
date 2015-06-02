
using System;
using System.Data;

using System.Collections.Generic;
namespace ET.Sys_DEF
{
    [Serializable]
    [Table(TableName = "SysRoleActionLink", PrimaryKey = "LinkID", PrimaryKeyType = typeof(Guid))]
    public class SysRoleActionLink 
	{


        public Guid LinkID { get; set; }


        public Guid RoleID { get; set; }

        public Guid ActionID { get; set; }
	}
}
