using System;
using System.Collections.Generic;
using System.Text;

namespace ET.Sys_DEF
{
    [Serializable]
    public class UserFullProperty
    {
        public Guid UserID { get; set; }
        public String UserName { get; set; }

        public String UserPwd { get; set; }

        /// <summary>
        /// 用户状态，1为不限制，0限制,-1为禁用
        /// </summary>
        public Int32? Status { get; set; }

        public DateTime? StartTime { get; set; }

        public DateTime? EndTime { get; set; }
        public String Nickname { get; set; }


        public String CNName { get; set; }


        public String ENName { get; set; }

        public Int32? Age { get; set; }



        public String Sex { get; set; }

        public String EMail { get; set; }


        public String Address { get; set; }


        public String IDCard { get; set; }


        public String Tel { get; set; }

        public String Mobile { get; set; }


        public String Contact { get; set; }
        public String QQ { get; set; }
        public DateTime? CreateTime { get; set; }
        public String Source { get; set; }

        public String Photo { get; set; }


        public Guid? ModitifyUser { get; set; }

        public DateTime? ModitifyTime { get; set; }

        public String BirthdayYear { get; set; }
        public String BirthdayMonth { get; set; }
        public String BirthdayDay { get; set; }
        public String LiveProvince { get; set; }
        public String LiveCity { get; set; }
        public String LiveArea { get; set; }
        public String Detail { get; set; }
        public Int32? UserGrade { get; set; }
        public String CompanyID { get; set; }
        public String CompanyName { get; set; }

        public String DepID { get; set; }
        public String DepName { get; set; }

        public String PostID { get; set; }
        public String PostName { get; set; }
        public String RoleNames { get; set; }
        public String RoleIDs { get; set; }

        public String Exp { get;set; }
        public String UserLevel { get; set; }
        public String NextExp { get; set; }
        
    } 
}
