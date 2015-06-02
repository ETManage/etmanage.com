using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;


namespace ET.Sys_DEF
{
     [Serializable]
     [Table(TableName = "GTDTask", PrimaryKey = "TaskID", PrimaryKeyType = typeof(Guid))]
    public class GTDTask
    {
        [PrimaryKey(true)]
         public Guid TaskID { get; set; }
        public String TaskNumber { get; set; }
        public String TaskTitle { get; set; }
        public String TaskContent { get; set; }
        public String TaskLabel { get; set; }
        public String Priority { get; set; }

        public Guid ProjectID { get; set; }
        public String ProjectNumber { get; set; }
        public Guid SceneID { get; set; }
    

 
         public DateTime? StartTime { get; set; }
         public DateTime? EndTime { get; set; }
         public DateTime? CreateTime { get; set; }
         public Guid Creator { get; set; }
         public Guid DoUserID { get; set; }

         public String Status { get; set; }
         public String TaskType { get; set; }
         public String DoType { get; set; }
         
    }
}
