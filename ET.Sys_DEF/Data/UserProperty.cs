
using System;
using System.Data;
using System.Collections.Generic;
namespace ET.Sys_DEF
{
    [Serializable]
    [Table(TableName = "UserProperty", PrimaryKey = "PropertyID", PrimaryKeyType = typeof(Guid))]
    public class UserProperty
    {
        [PrimaryKey(true)]
        public Guid PropertyID { get; set; }


        public Guid? UserID { get; set; }


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
        
    }
}
