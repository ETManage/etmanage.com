using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;


namespace ET.Sys_DEF
{
     [Serializable]
     [Table(TableName = "DesignGoodInfo", PrimaryKey = "GoodID", PrimaryKeyType = typeof(Guid))]
    public class DesignGoodInfo
    {
        [PrimaryKey(true)]
         public Guid GoodID { get; set; }
        public Guid TypeID { get; set; }
        public String Creator { get; set; }
         public String GoodName { get; set; }
         public String GoodLabel { get; set; }
         public String GoodDescription { get; set; }
         public String GoodContent { get; set; }
         public Int32? Recommend { get; set; }
         public Boolean? GoodIsLink { get; set; }
         public DateTime? CreateTime { get; set; }
         public Int64? AccessCount { get; set; }
         public String GoodUrl { get; set; }
         public String GoodSource { get; set; }
         public String GoodPicture { get; set; }

         public Int32? Status { get; set; }

         public Int64? LoveCount { get; set; }
    }
}
