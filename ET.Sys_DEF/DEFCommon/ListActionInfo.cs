
using System;
using System.Data;

using System.Collections.Generic;
namespace ET.Sys_DEF
{
    [Serializable]
    [Table(TableName = "ListActionInfo", PrimaryKey = "ActionID", PrimaryKeyType = typeof(Guid))]
    public class ListActionInfo 
	{
        public Guid ActionID { get; set; }

        public String ModuleID { get; set; }

        public String ActionName { get; set; }


        public String ActionKey { get; set; }

        /// <summary>
        /// 事件状态，1为启用，0未开发中,-1为禁用
        /// </summary>
        public Int32? ActionStatus { get; set; }

        public bool @checked { get; set; }

	}
}
