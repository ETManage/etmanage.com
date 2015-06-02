using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;


namespace ET.Sys_DEF
{
     [Serializable]
     [Table(TableName = "ShopTypeInfo", PrimaryKey = "TypeID", PrimaryKeyType = typeof(Guid))]
    public class ShopTypeInfo
    {
         [PrimaryKey(true)]
         public Guid TypeID { get; set; }
         public String TypeName { get; set; }
         public String TypeSort { get; set; }
         public String TypeDescription { get; set; }
         public DateTime? TypeCreateTime { get; set; }
         public String TypeCreator { get; set; }
         public String TypePID { get; set; }
    }
}
