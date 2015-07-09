using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;


namespace ET.Sys_DEF
{
     [Serializable]
     [Table(TableName = "BlogUserRequest", PrimaryKey = "RequestID", PrimaryKeyType = typeof(Guid))]
    public class BlogUserRequest
    {
         [PrimaryKey(true)]
         public Guid RequestID { get; set; }
         public String RequestName { get; set; }
         public Guid Requester { get; set; }
         public Guid Auditor { get; set; }
         public Int32? Status { get; set; }
         public DateTime? CreateTime { get; set; }
         public String AuditeResult { get; set; }
         public DateTime? AuditeTime { get; set; }
    }
}
