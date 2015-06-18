
using System;
using System.Data;

using System.Collections.Generic;
namespace ET.Sys_DEF
{
    [Serializable]
    [Table(TableName = "UserDepartment", PrimaryKey = "DepID", PrimaryKeyType = typeof(Guid))]
    public class UserDepartment
    {


        public Guid DepID { get; set; }


        public String DepName { get; set; }

        public String DepSort { get; set; }


        public String DepPID { get; set; }


        public String DepDescription { get; set; }

        public String CompanyID { get; set; }      public String CreatorID { get; set; }
        public DateTime? CreateTime { get; set; }
    }
}
