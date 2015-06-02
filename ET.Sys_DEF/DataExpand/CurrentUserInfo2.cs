using System;
using System.Collections.Generic;
using System.Text;

namespace ET.Sys_DEF
{
    [Serializable]
    public class CurrentUserInfo
    {
        public User_Condition UserCondition { get; set; }
        public List<string> UserLimit { get; set; }
    }
}
