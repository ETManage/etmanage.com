
using System;
using System.Data;

using System.Collections.Generic;

namespace ET.Sys_DEF
{
    [Serializable]
    [Table(TableName = "SysModuleInfo", PrimaryKey = "ModuleID", PrimaryKeyType = typeof(Guid))]
    public class SysModuleInfo 
	{


        public Guid ModuleID { get; set; }



        public String ModuleName { get; set; }

        public String ModuleIcon { get; set; }


        public String ModuleSort { get; set; }

        public String ModuleUrl { get; set; }


        public String ModuleTarget { get; set; }


        public String ModulePID { get; set; }


        public String ModuleKey { get; set; }
       
        
	}
}
