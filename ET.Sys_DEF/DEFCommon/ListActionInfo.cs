
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
        /// �¼�״̬��1Ϊ���ã�0δ������,-1Ϊ����
        /// </summary>
        public Int32? ActionStatus { get; set; }

        public bool @checked { get; set; }

	}
}
