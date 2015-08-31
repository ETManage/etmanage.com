
using System;
using System.Data;

using System.Collections.Generic;

namespace ET.Sys_DEF
{
    [Serializable]
    [Table(TableName = "SysFunction", PrimaryKey = "FuncID", PrimaryKeyType = typeof(Guid))]
    public class SysFunction 
	{

        public Guid FuncID { get; set; }

        public Guid SystemID { get; set; }
        public String FuncPID { get; set; }

        public String FuncKey { get; set; }


        public String FuncName { get; set; }
    
        public String FuncPName { get; set; }
        public String FuncType { get; set; }


        public String ShowStyle { get; set; }


        public String FuncSort { get; set; }

        public String IconPath { get; set; }
        public String Source { get; set; }
        public String Remark { get; set; }
        public DateTime? CreateTime { get; set; }
        public Guid CreatorID { get; set; }
        public DateTime? UpdateTime { get; set; }
        public Guid UpdatorID { get; set; }
        public Int32? Status { get; set; }
        public Int32? PublicLevel { get; set; }
        public DateTime? StartTime { get; set; }public DateTime? EndTime { get; set; }

        public String Reserve1 { get; set; }
        public String Reserve2 { get; set; }
	}
}
