using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;


namespace ET.Sys_DEF
{
     [Serializable]
     [Table(TableName = "NewTypeInfo", PrimaryKey = "TypeID", PrimaryKeyType = typeof(Guid))]
    public class NewTypeInfo
    {
         [PrimaryKey(true)]
         public Guid TypeID { get; set; }
         public String TypeName { get; set; }
         public String TypeSort { get; set; }
         public String TypeDescription { get; set; }
         public String TypeKey { get; set; }
         public String TypePID { get; set; }
    }
}
