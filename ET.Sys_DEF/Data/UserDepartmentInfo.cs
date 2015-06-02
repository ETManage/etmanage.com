
using System;
using System.Data;

using System.Collections.Generic;
namespace ET.Sys_DEF
{
    [Serializable]
    [Table(TableName = "UserDepartmentInfo", PrimaryKey = "DepID", PrimaryKeyType = typeof(Guid))]
    public class UserDepartmentInfo
    {


        public Guid DepID { get; set; }


        public String DepName { get; set; }

        public String DepSort { get; set; }


        public String DepPID { get; set; }


        public String DepDescription { get; set; }


    }
}
