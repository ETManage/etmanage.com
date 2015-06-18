
using System;
using System.Data;

using System.Collections.Generic;
namespace ET.Sys_DEF
{
    [Serializable]
    [Table(TableName = "SysRole", PrimaryKey = "RoleID", PrimaryKeyType = typeof(Guid))]
    public class SysRole
	{

        public Guid RoleID { get; set; }


        public String RoleName { get; set; }

        public String RoleDescription { get; set; }


        public DateTime? RoleCreateTime { get; set; }


        public String RoleHome { get; set; }

        public Boolean? IsAllowRegist { get; set; }


        public Guid RoleCreator { get; set; }

		
	}
}
