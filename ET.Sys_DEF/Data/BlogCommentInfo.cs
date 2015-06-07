using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;


namespace ET.Sys_DEF
{
     [Serializable]
     [Table(TableName = "BlogCommentInfo", PrimaryKey = "CommentID", PrimaryKeyType = typeof(Guid))]
    public class BlogCommentInfo
    {
        [PrimaryKey(true)]
         public Guid CommentID { get; set; }
        public Guid ArticleID { get; set; }
        public String CommentContent { get; set; }
     
         public Boolean? IsAnonymity { get; set; }
         public DateTime? CreateTime { get; set; }
         
         public String Creator { get; set; }
         public String CreatorEmail { get; set; }
         public String CreatorUrl { get; set; }

         public String CommentPID { get; set; }
         public Int32? Status { get; set; }
    }
}
