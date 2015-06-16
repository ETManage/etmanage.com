using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;


namespace ET.Sys_DEF
{
     [Serializable]
     [Table(TableName = "NewInfo", PrimaryKey = "NewID", PrimaryKeyType = typeof(Guid))]
    public class NewInfo
    {
         public Guid NewID { get; set; }
         public Guid TypeID { get; set; }
         public String NewTitle { get; set; }
         public String NewContent { get; set; }
         public String Creator { get; set; }
         public String NewSource { get; set; }
         public DateTime CreateTime { get; set; }
         public Int32? Recomment { get; set; }
         public Int32? Status { get; set; }
         public String Reserve1 { get; set; }
         public String Reserve2 { get; set; }
    }
}
