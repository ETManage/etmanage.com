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

        public String UserCNName { get; set; }
        public string DepID { get; set; }

        public string PostID { get; set; }
        /// <summary>
        /// ��,�����Ľ�ɫID
        /// </summary>
        public string RoleIDS { get; set; }

    }
}
