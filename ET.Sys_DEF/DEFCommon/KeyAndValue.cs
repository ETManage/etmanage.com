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
    public class KeyAndValue
    {
         public String id { get; set; }
        public String text { get; set; }

        public String @checked { get; set; }
        public String reserve1 { get; set; }
        public String reserve2 { get; set; }
         public String reserve3 { get; set; }
         
        public List<KeyAndValue> children { get; set; }

        public String pid { get; set; }
    }
}
