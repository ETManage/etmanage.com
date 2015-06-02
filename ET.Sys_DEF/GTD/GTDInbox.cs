using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;


namespace ET.Sys_DEF
{
     [Serializable]
     [Table(TableName = "GTDInbox", PrimaryKey = "BoxID", PrimaryKeyType = typeof(Guid))]
    public class GTDInbox
    {
        [PrimaryKey(true)]
         public Guid BoxID { get; set; }
        public String BoxTitle { get; set; }
        public String BoxContent { get; set; }
        public String BoxLabel { get; set; }
        public String Priority { get; set; }

        public Guid ProjectID { get; set; }
        public Guid SceneID { get; set; }
      

 
         public DateTime? StartTime { get; set; }
         public DateTime? EndTime { get; set; }
         public DateTime? CreateTime { get; set; }

         public Guid Creator { get; set; }
         public Guid DoUserID { get; set; }
         public String Status { get; set; }
         public String BoxType { get; set; }
         
    }
}
