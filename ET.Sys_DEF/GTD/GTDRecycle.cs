using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;


namespace ET.Sys_DEF
{
    [Serializable]
    [Table(TableName = "GTDRecycle", PrimaryKey = "RecycleID", PrimaryKeyType = typeof(Guid))]
    public class GTDRecycle
    {
        [PrimaryKey(true)]
        public Guid RecycleID { get; set; }
        public Guid InfoID { get; set; }
        public String InfoNumber{ get; set; }
        public String InfoTitle { get; set; }
        public String InfoContent { get; set; }
        public String InfoLabel { get; set; }
        public String Priority { get; set; }

        public DateTime? StartTime { get; set; }
        public DateTime? EndTime { get; set; }
        public DateTime? RecycleTime { get; set; }
        public DateTime? CreateTime { get; set; }
        public Guid CreatorID { get; set; }
        public String CreatorName { get; set; }
        public Guid DoUserID { get; set; }
        public String DoUserName { get; set; }
        public Guid SceneID { get; set; }
        public String SceneName { get; set; }
        public Guid ProjectID { get; set; }
        public String ProjectNumber { get; set; }
        public String ProjectName { get; set; }
        public DateTime? PStartTime { get; set; }
        public DateTime? PEndTime { get; set; }
        public String RecycleType { get; set; }

    }
}
