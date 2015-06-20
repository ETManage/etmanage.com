
using System;
using System.Data;

using System.Collections.Generic;
namespace ET.Sys_DEF
{
    [Serializable]
    [Table(TableName = "BlogUserSignIn", PrimaryKey = "SignID", PrimaryKeyType = typeof(Guid))]
    public class BlogUserSignIn 
	{


        public Guid SignID { get; set; }


        public Guid UserID { get; set; }
        public DateTime? CreateTime { get; set; }
        public String Reserve1 { get; set; }
        public String Reserve2 { get; set; }
	}
}
