using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;


namespace ET.Sys_DEF
{
     [Serializable]
     [Table(TableName = "BlogViewRecordInfo", PrimaryKey = "ViewID", PrimaryKeyType = typeof(Guid))]
    public class BlogViewRecordInfo
    {
        [PrimaryKey(true)]
         public Guid ViewID { get; set; }
        public Guid UserID { get; set; }
        public Guid InfoID { get; set; }
        public string InfoCategory { get; set; }
        public DateTime? CreateTime { get; set; }
         
    }
}
