using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;


namespace ET.Sys_DEF
{
    /// <summary>
    /// 用户对两集目录的数据进行操作的实体类
    /// </summary>
     [Serializable]
    public class TreeModuleInfo
    {
         public String id { get; set; }
         public String iconCls { get; set; }
         public String text { get; set; }
         public bool @checked { get; set; }
         public String state { get; set; }
         public String pid { get; set; }
         public TreeAttributeInfo attributes { get; set; }
         public String target { get; set; }
         public List<TreeModuleInfo> children { get; set; }
    }
}
