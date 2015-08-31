using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;


namespace ET.Sys_DEF
{
     [Serializable]
     [Table(TableName = "ChatMessage", PrimaryKey = "MsgID", PrimaryKeyType = typeof(Guid))]
    public class ChatMessage
    {
         public Guid MsgID { get; set; }
         public String MsgTitle { get; set; }
         public String MsgContent { get; set; }

         public DateTime? CreateTime { get; set; }
         public Guid Sender { get; set; }
         public Guid Receiver { get; set; }
         public DateTime? ReceiveTime { get; set; }
         public Int32? Status { get; set; }
         public String Reserve1 { get; set; }
         public String Reserve2 { get; set; }
    }
}
