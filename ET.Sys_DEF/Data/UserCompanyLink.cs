
using System;
using System.Data;

using System.Collections.Generic;
namespace ET.Sys_DEF
{
    [Serializable]
    [Table(TableName = "UserCompanyLink", PrimaryKey = "LinkID", PrimaryKeyType = typeof(Guid))]
    public class UserCompanyLink 
	{


        public Guid LinkID { get; set; }


        public Guid UserID { get; set; }

        public Guid CompanyID { get; set; }
	}
}
