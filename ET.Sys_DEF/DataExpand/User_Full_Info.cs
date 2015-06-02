using System;
using System.Collections.Generic;
using System.Text;

namespace ET.Sys_DEF
{
    [Serializable]
    public class User_Full_Info
    {
        public UserBaseInfo userbaseinfo { get; set; }
        public UserPropertyInfo userstuinfo { get; set; }
        public List<UserRoleLink> userrole { get; set; }

    }
}
