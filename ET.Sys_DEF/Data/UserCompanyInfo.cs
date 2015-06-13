
using System;
using System.Data;

using System.Collections.Generic;
namespace ET.Sys_DEF
{
    [Serializable]
    [Table(TableName = "UserCompanyInfo", PrimaryKey = "CompanyID", PrimaryKeyType = typeof(Guid))]
    public class UserCompanyInfo
    {


        public Guid CompanyID { get; set; }


        public String CompanyName { get; set; }

        public String CompanySort { get; set; }


        public String CompanyPID { get; set; }


        public String CompanyDescription { get; set; }
        public String CreatorID { get; set; }
        public DateTime? CreateTime { get; set; }

    }
}
