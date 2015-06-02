using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;


namespace ET.Sys_DEF
{
     [Serializable]
     [Table(TableName = "GTDScene", PrimaryKey = "SceneID", PrimaryKeyType = typeof(Guid))]
    public class GTDScene
    {
        [PrimaryKey(true)]
         public Guid SceneID { get; set; }
        public String SceneName { get; set; }
        public String Description { get; set; }
         
    }
}
