using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;


namespace ET.Sys_DEF
{
     [Serializable]
     [Table(TableName = "BlogPublish", PrimaryKey = "PublishID", PrimaryKeyType = typeof(Guid))]
    public class BlogPublish
    {
        [PrimaryKey(true)]
         public Guid PublishID { get; set; }
        public Guid TypeID { get; set; }
        public String Title { get; set; }
        public String Description { get; set; }
        public String PublishContent { get; set; }
        public String PublishSource { get; set; }
        public String Author { get; set; }
        public String PublishType { get; set; }
     
         public DateTime? CreateTime { get; set; }
         public String Cover { get; set; }
         public String Label { get; set; }
         public String URL { get; set; }
         public String Creator { get; set; }
         public Int32? Status { get; set; }
         
    }
}
