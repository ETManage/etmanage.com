using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;


namespace ET.Sys_DEF
{
     [Serializable]
     [Table(TableName = "BlogTypeInfo", PrimaryKey = "TypeID", PrimaryKeyType = typeof(Guid))]
    public class BlogTypeInfo
    {
         [PrimaryKey(true)]
         public Guid TypeID { get; set; }
         public String TypeName { get; set; }
         public String TypeSort { get; set; }
         public String TypeDescription { get; set; }
         public String TypeKey { get; set; }
         public String TypeCreator { get; set; }
         public String TypePID { get; set; }
         public String TypeUrl { get; set; }
         public String TypeLevel { get; set; }
         public Int32? Status { get; set; }
         public Boolean? IsOnlyNav { get; set; }
    }
}
