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
    public class TreeAttributeInfo
    {
         public String url { get; set; }
        public String price { get; set; }
        public String type { get; set; }
         
    }
}
