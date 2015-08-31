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
    public class MenuModuleInfo
    {
         public String menuid { get; set; }
         public String icon { get; set; }
         public String menuname { get; set; }
         public String url { get; set; }

         public List<MenuModuleInfo> menus { get; set; }
    }
}
