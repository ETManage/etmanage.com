using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;


namespace ET.Sys_DEF
{
     [Serializable]
     [Table(TableName = "BlogViewRecord", PrimaryKey = "ViewID", PrimaryKeyType = typeof(Guid))]
    public class BlogViewRecord
    {
        [PrimaryKey(true)]
         public Guid ViewID { get; set; }
        public Guid UserID { get; set; }
        public string InfoID { get; set; }
        public string InfoCategory { get; set; }
        public DateTime? CreateTime { get; set; }


        public string InfoType { get; set; }
        public string Reserve1 { get; set; } public string Reserve2 { get; set; }
    }
}
