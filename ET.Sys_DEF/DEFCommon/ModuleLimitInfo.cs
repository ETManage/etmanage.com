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
    public class ModuleLimitInfo
    {
         public String id { get; set; }
        public String text { get; set; }

        public string @checked { get; set; }
        public String attributes { get; set; }
        public String state { get; set; }

         

        //public List<ListActionInfo> limits { get; set; }
        public List<ModuleLimitInfo> children { get; set; }
    }
}
