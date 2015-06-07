using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;


namespace ET.Sys_DEF
{
     [Serializable]
     [Table(TableName = "BlogMessageInfo", PrimaryKey = "MsgID", PrimaryKeyType = typeof(Guid))]
    public class BlogMessageInfo
    {
        [PrimaryKey(true)]
         public Guid MsgID { get; set; }
        public String MsgTitle { get; set; }
        public String MsgContent { get; set; }
         public Boolean? IsAnonymity { get; set; }
         public DateTime? CreateTime { get; set; }
         
         public String Creator { get; set; }
         public String CreatorEmail { get; set; }
         public String CreatorUrl { get; set; }
         public String CreatorTel { get; set; }
         public String MsgPID { get; set; }
         public Int32? Status { get; set; }
         public String MsgType { get; set; }
         public String ReplyContent { get; set; }
         public DateTime? ReplyTime { get; set; }
    }
}
