using System;
using System.Collections.Generic;
using System.Text;

namespace ET.Sys_DEF
{
    [Serializable]
    public class OnlineUser
    {
        string _SessionID;

        /// <summary>
        /// SessionID
        /// </summary>
        public string SessionID
        {
            get { return _SessionID; }
            set { _SessionID = value; }
        }


        string _Key;
        /// <summary>
        /// 用户自定义的Session项,如Session["UserName"]
        /// </summary>
        public string Key
        {
            get { return _Key; }
            set { _Key = value; }
        }

        string _IpAddress;
        /// <summary>
        /// 用户IP地址
        /// </summary>
        public string IpAddress
        {
            get { return _IpAddress; }
            set { _IpAddress = value; }
        }
        DateTime? _SessionStartTime = null;
        /// <summary>
        /// 会话启动时间
        /// </summary>
        public DateTime? SessionStartTime
        {
            get { return _SessionStartTime; }
            set { _SessionStartTime = value; }
        }

    }
}
