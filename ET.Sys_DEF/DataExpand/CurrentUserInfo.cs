using System;
using System.Collections.Generic;
using System.Text;

namespace ET.Sys_DEF
{
    [Serializable]
    public class CurrentUserInfo
    {

        public Guid? UserID { get; set; }

        public String UserName { get; set; }

        public String CNName { get; set; }
        public String Photo { get; set; }
        public String DepID { get; set; }

        public String PostID { get; set; }
        /// <summary>
        /// 用,隔开的角色ID
        /// </summary>
        public String RoleIDS { get; set; }
        public String UserGrade { get; set; }

        public List<string> UserLimit { get; set; }

    }
}
