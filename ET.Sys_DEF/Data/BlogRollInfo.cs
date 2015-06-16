using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;


namespace ET.Sys_DEF
{
     [Serializable]
     [Table(TableName = "BlogRollInfo", PrimaryKey = "RollID", PrimaryKeyType = typeof(Guid))]
    public class BlogRollInfo
    {
         [PrimaryKey(true)]
         public Guid RollID { get; set; }
         public String RollName { get; set; }
         public String RollSort { get; set; }
         public String RollUrl { get; set; }
         public Int32? Status { get; set; }
    }
}
