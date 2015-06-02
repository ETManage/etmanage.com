using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;


namespace ET.Sys_DEF
{
     [Serializable]
     [Table(TableName = "GTDProject", PrimaryKey = "ProjectID", PrimaryKeyType = typeof(Guid))]
    public class GTDProject
    {
        [PrimaryKey(true)]
         public Guid ProjectID { get; set; }
        public String ProjectNumber { get; set; }
        public String ProjectName { get; set; }
        public String Description { get; set; }

        public DateTime? StartTime { get; set; }
         public DateTime? EndTime { get; set; }
         public DateTime? CreateTime { get; set; }
         public Guid Creator { get; set; }

         public String Status { get; set; }
         
    }
}
