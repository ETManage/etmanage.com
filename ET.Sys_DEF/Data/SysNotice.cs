using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;


namespace ET.Sys_DEF
{
     [Serializable]
     [Table(TableName = "SysNotice", PrimaryKey = "NoticeID", PrimaryKeyType = typeof(Guid))]
    public class SysNotice
    {
         public Guid NoticeID { get; set; }
         public String NoticeTitle { get; set; }
         public String NoticeContent { get; set; }

         public DateTime? CreateTime { get; set; }
         public Guid CreatorID { get; set; }

         public String CreatorName { get; set; }
         public Int32? Status { get; set; }
    }
}
