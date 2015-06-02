using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;


namespace ET.Sys_DEF
{
     [Serializable]
     [Table(TableName = "BlogArticleFavoriteInfo", PrimaryKey = "FavoriteID", PrimaryKeyType = typeof(Guid))]
    public class BlogArticleFavoriteInfo
    {
        [PrimaryKey(true)]
         public Guid FavoriteID { get; set; }
        public Guid UserID { get; set; }
        public Guid ArticleID { get; set; }
        public DateTime? CreateTime { get; set; }
         
    }
}
