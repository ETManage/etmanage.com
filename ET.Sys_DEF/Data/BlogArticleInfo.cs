using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;


namespace ET.Sys_DEF
{
     [Serializable]
     [Table(TableName = "BlogArticleInfo", PrimaryKey = "ArticleID", PrimaryKeyType = typeof(Guid))]
    public class BlogArticleInfo
    {
        [PrimaryKey(true)]
         public Guid ArticleID { get; set; }
        public Guid TypeID { get; set; }
        public String Creator { get; set; }
         public String ArticleTitle { get; set; }
         public String ArticleLabel { get; set; }
         public String ArticleDescription { get; set; }
         public String ArticleContent { get; set; }
         public Int32? Recommend { get; set; }
         public Boolean? IsOutLink { get; set; }
         public DateTime? CreateTime { get; set; }
         public Int64? AccessCount { get; set; }
         public String ArticleUrl { get; set; }
         public String ArticleSource { get; set; }
         public String ArticleCover { get; set; }
         public String ArticleAuthor { get; set; }
         public Int32? Status { get; set; }
         public Int64? LoveCount { get; set; }
         public Int64? ShareCount { get; set; }
         public Boolean? IsRoll { get; set; }
         public String SpecMark { get; set; }
         public String Reserve1 { get; set; }
         public String Reserve2 { get; set; }
         
    }
}
