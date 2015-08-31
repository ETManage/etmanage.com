using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;


namespace ET.Sys_DEF
{
    /// <summary>
    /// 用于对键值对数据操作的实体类
    /// </summary>
     [Serializable]
    public class NameAndValue
    {
        public String name { get; set; }

        public String value { get; set; }
    }
}
