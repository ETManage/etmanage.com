using System;
using System.Collections.Generic;
using System.Text;

namespace ET.Sys_DEF
{
    [Serializable]
    public class UserFullInfo
    {
        public UserBase userbaseinfo { get; set; }
        public UserProperty userstuinfo { get; set; }
        public List<UserRoleLink> userrole { get; set; }

    }
}
