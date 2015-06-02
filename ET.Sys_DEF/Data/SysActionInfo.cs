
using System;
using System.Data;

using System.Collections.Generic;
namespace ET.Sys_DEF
{
    [Serializable]
    [Table(TableName = "SysActionInfo", PrimaryKey = "ActionID", PrimaryKeyType = typeof(Guid))]
    public class SysActionInfo 
	{
        public Guid ActionID { get; set; }

        public String ModuleID { get; set; }

        public String ActionName { get; set; }


        public String ActionKey { get; set; }

        /// <summary>
        /// �¼�״̬��1Ϊ���ã�0δ������,-1Ϊ����
        /// </summary>
        public Int32? ActionStatus { get; set; }


	}
}
