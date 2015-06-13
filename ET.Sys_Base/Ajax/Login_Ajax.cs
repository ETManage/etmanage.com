using System;
using System.Collections.Generic;
using System.Text;
using ET.Sys_DEF;
using System.Web.Caching;
using System.Web;
using System.Data;
using System.Xml;
using System.Security.Principal;
using System.Runtime.InteropServices;
using System.DirectoryServices;
using System.Web.UI.WebControls;
using System.Web.Mvc;
using ET.ToolKit.Common;
using System.Linq;
namespace ET.Sys_Base
{
    public class Login_Ajax
    {
        public string LoginSinglePoint(string username, string pwd)
        {
            UserBaseInfo info = new ET.Sys_BLL.OrganizationBLL().Get_UserBaseInfo(string.Format(" AND UserName='{0}' AND UserPwd='{1}'  AND (UserStatus=1 OR (UserStatus=0 AND UserStartTime<='{2}' AND UserEndTime>'{2}'))", StringHelper.ClearSqlDangerous(username), ET.ToolKit.Encrypt.EncrypeHelper.EncryptMD5(StringHelper.ClearSqlDangerous(pwd)), DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")));
            if (info != null)
            {
                ET.Sys_Base.OnlineUser.OnlineUserRecorder recorder = System.Web.HttpContext.Current.Cache[ET.Sys_Base.OnlineUser.OnlineHttpModule.g_onlineUserRecorderCacheKey] as ET.Sys_Base.OnlineUser.OnlineUserRecorder;
                if (recorder.GetUserList().Where(i => i.UserName == info.UserID.ToString()).Count() == 0 || username == "etadmin")
                {
                    LoginRecord(info);
                    return "true";
                }
                else
                {
                    return "对不起，该用户已经登陆，请求被拒绝！！";
                }
            }
            else
                return "false";
        }

        public string LoginUser(string username, string pwd)
        {
            UserBaseInfo info = new ET.Sys_BLL.OrganizationBLL().Get_UserBaseInfo(string.Format(" AND UserName='{0}' AND UserPwd='{1}'  AND (UserStatus=1 OR (UserStatus=0 AND UserStartTime<='{2}' AND UserEndTime>'{2}'))", StringHelper.ClearSqlDangerous(username), ET.ToolKit.Encrypt.EncrypeHelper.EncryptMD5(StringHelper.ClearSqlDangerous(pwd)), DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")));
            if (info != null)
            {
                try
                {
                    LoginRecord(info);
                    return "true";
                }
                catch (Exception ex)
                {
                    return ex.Message;
                }
            }
            else
                return "false";
        }
        /// <summary>
        /// 前端页面登录方法
        /// </summary>
        /// <param name="username"></param>
        /// <param name="pwd"></param>
        /// <returns></returns>
        public string WebLoginSinglePoint(string username, string pwd, bool IsRememberMe)
        {
            UserBaseInfo info = new ET.Sys_BLL.OrganizationBLL().Get_UserBaseInfo(string.Format(" AND UserName='{0}' AND UserPwd='{1}'  AND (UserStatus=1 OR (UserStatus=0 AND UserStartTime<='{2}' AND UserEndTime>'{2}'))", StringHelper.ClearSqlDangerous(username), ET.ToolKit.Encrypt.EncrypeHelper.EncryptMD5(StringHelper.ClearSqlDangerous(pwd)), DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")));
            if (info != null)
            {
                ET.Sys_Base.OnlineUser.OnlineUserRecorder recorder = System.Web.HttpContext.Current.Cache[ET.Sys_Base.OnlineUser.OnlineHttpModule.g_onlineUserRecorderCacheKey] as ET.Sys_Base.OnlineUser.OnlineUserRecorder;
                if (recorder.GetUserList().Where(i => i.UserName == info.UserID.ToString()).Count() == 0 || username == "etadmin")
                {
                    WebLoginRecord(info, IsRememberMe);
                    return "true";
                }
                else
                {
                    return "对不起，该用户已经登陆，请求被拒绝！！";
                }
            }
            else
                return "false";
        }
        public string WebLoginUser(string username, string pwd, bool IsRememberMe)
        {
            UserBaseInfo info = new ET.Sys_BLL.OrganizationBLL().Get_UserBaseInfo(string.Format(" AND UserName='{0}' AND UserPwd='{1}'  AND (UserStatus=1 OR (UserStatus=0 AND UserStartTime<='{2}' AND UserEndTime>'{2}'))", StringHelper.ClearSqlDangerous(username), ET.ToolKit.Encrypt.EncrypeHelper.EncryptMD5(StringHelper.ClearSqlDangerous(pwd)), DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")));
            if (info != null)
            {
                try
                {
                    WebLoginRecord(info, IsRememberMe);
                    return "true";
                }
                catch (Exception ex)
                {
                    return ex.Message;
                }
            }
            else
                return "false";
        }
        /// <summary>
        /// 用户登录
        /// </summary>
        /// <param name="username">用户名或者用户ID，如果是用户ID则需要将blnIsUserID设置为true</param>
        /// <param name="pwd"></param>
        /// <param name="blnIsUserID">是否使用USERID登录</param>
        /// <returns></returns>
        public string LoginUser(string username, string pwd, bool blnIsUserID)
        {
            UserBaseInfo info = null;

            if (!blnIsUserID)
                info = new ET.Sys_BLL.OrganizationBLL().Get_UserBaseInfo(string.Format(" AND UserName='{0}' AND UserPwd='{1}'  AND (UserStatus=1 OR (UserStatus=0 AND UserStartTime<='{2}' AND UserEndTime>'{2}'))", StringHelper.ClearSqlDangerous(username), ET.ToolKit.Encrypt.EncrypeHelper.EncryptMD5(StringHelper.ClearSqlDangerous(pwd)), DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")));
            else
                info = new ET.Sys_BLL.OrganizationBLL().Get_UserBaseInfo(string.Format(" AND UserID='{0}'  AND (UserStatus=1 OR (UserStatus=0 AND UserStartTime<='{1}' AND UserEndTime>'{1}'))", StringHelper.ClearSqlDangerous(username), DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")));


            if (info != null)
            {
                try
                {
                    LoginRecord(info);
                    return "true";
                }
                catch (Exception ex)
                {
                    return ex.Message;
                }
            }
            else
                return "false";
        }

        public CurrentUserInfo GetCurrentUserInfo(string userID)
        {
            CurrentUserInfo userinfo = new CurrentUserInfo();
            User_Full_Info info = new ET.Sys_BLL.OrganizationBLL().Get_User_Info(userID);
            if (info != null)
            {
                
                userinfo.UserID = info.userbaseinfo.UserID;
                userinfo.UserName = info.userbaseinfo.UserName;
                userinfo.UserCNName = info.userstuinfo.CNName;
                var roleIDs = info.userrole.Select(c => c.RoleID.ToString()).ToArray().Aggregate((current, next) => String.Format("{0},{1}", current, next));
                userinfo.RoleIDS = roleIDs;
                System.Web.HttpContext.Current.Session[SystemConfigConst.SessionUserInfo] = userinfo;
                ET.Sys_Base.OnlineUser.OnlineHttpModule.ProcessRequest();
            }
            else
            {
                Logout();
            }
            return userinfo;
        }

        public void LoginRecord(UserBaseInfo info)
        {
            CurrentUserInfo userinfo = new CurrentUserInfo();
            userinfo.UserID = info.UserID;
            userinfo.UserName = info.UserName;
            UserPropertyInfo stuinfo = new ET.Sys_BLL.OrganizationBLL().Get_UserPropertyInfo(" AND UserID='" + info.UserID.ToString() + "'");
            userinfo.UserCNName = stuinfo.CNName;
            List<SysRoleInfo> sturole = new ET.Sys_BLL.SystemBLL().List_SysRoleInfo(info.UserID.ToString());
            var roleIDs = sturole.Select(c => c.RoleID.ToString()).ToArray().Aggregate((current, next) => String.Format("{0},{1}", current, next));
            userinfo.RoleIDS = roleIDs;
            FormAuthService.SignIn(info.UserID.ToString(), false, new string[] { "admin" });
            System.Web.HttpContext.Current.Session[SystemConfigConst.SessionUserInfo] = userinfo;
            ET.Sys_Base.OnlineUser.OnlineHttpModule.ProcessRequest();
            try
            {
                ET.ToolKit.Common.TxtHelper txtHelper = new ET.ToolKit.Common.TxtHelper();
                txtHelper.WriteToFile(System.Web.HttpContext.Current.Request.PhysicalApplicationPath + SystemConfigConst.ManageLoginLogDir + DateTime.Now.ToString("yyyy-MM-dd") + ".log", "用户：" + info.UserName + "(" + stuinfo.CNName + ")" + "登陆");
            }
            catch { }
        }

        /// <summary>
        /// 前端处理
        /// </summary>
        /// <returns></returns>
        public void WebLoginRecord(UserBaseInfo info, bool IsRememberMe)
        {
            CurrentUserInfo userinfo = new CurrentUserInfo();
            userinfo.UserID = info.UserID;
            userinfo.UserName = info.UserName;
            UserPropertyInfo stuinfo = new ET.Sys_BLL.OrganizationBLL().Get_UserPropertyInfo(" AND UserID='" + info.UserID.ToString() + "'");
            userinfo.UserCNName = stuinfo.CNName;
            List<SysRoleInfo> sturole = new ET.Sys_BLL.SystemBLL().List_SysRoleInfo(info.UserID.ToString());
            string roleIDs = "";
            if (sturole.Count > 0)
            {
                roleIDs = sturole.Select(c => c.RoleID.ToString()).ToArray().Aggregate((current, next) => String.Format("{0},{1}", current, next));
            }

            userinfo.RoleIDS = roleIDs;
            FormAuthService.SignIn(info.UserID.ToString(), IsRememberMe, new string[] { "user" });
            System.Web.HttpContext.Current.Session[SystemConfigConst.SessionUserInfo] = userinfo;
            ET.Sys_Base.OnlineUser.OnlineHttpModule.ProcessRequest();
            try
            {
                ET.ToolKit.Common.TxtHelper txtHelper = new ET.ToolKit.Common.TxtHelper();
                txtHelper.WriteToFile(System.Web.HttpContext.Current.Request.PhysicalApplicationPath + SystemConfigConst.WebLoginLogDir + DateTime.Now.ToString("yyyy-MM-dd") + ".log", "用户：" + info.UserName + "(" + stuinfo.CNName + ")" + "登陆");
            }
            catch { }
        }
        public string SinglePointState()
        {
            string xmlpath = System.Web.HttpContext.Current.Server.MapPath(SystemConfigConst.WebSiteDir + SystemConfigConst.SystemConfigFile);
            XmlDocument xmldoc = new XmlDocument();
            xmldoc.Load(xmlpath);
            XmlNodeList nodelist = xmldoc.SelectSingleNode("Condition").ChildNodes;
            return System.Web.HttpContext.Current.Server.HtmlDecode(xmldoc.SelectSingleNode("Condition/System/SinglePoint").InnerXml);
        }

        public string Logout()
        {
            ET.Sys_Base.FormAuthService.SignOut();
            CurrentUserInfo info = (CurrentUserInfo)System.Web.HttpContext.Current.Session[SystemConfigConst.SessionUserInfo];
            if (info != null)
                HttpContext.Current.Cache.Remove(Convert.ToString(HttpContext.Current.Cache[info.UserID.ToString()]));
            System.Web.HttpContext.Current.Session.RemoveAll();
            System.Web.HttpContext.Current.Session.Abandon();
            return "true";
        }

        public string CheckUserName(string username)
        {
            username = StringHelper.ClearSqlDangerous(username);
            UserBaseInfo info = new ET.Sys_BLL.OrganizationBLL().Get_UserBaseInfo(" AND UserName='" + username + "'");
            if (info != null)
                return "true";
            else
                return "false";
        }

        public string CheckPwd(string UserID, string pwdold)
        {
            UserBaseInfo info = new ET.Sys_BLL.OrganizationBLL().Get_UserBaseInfo(" AND UserID='" + UserID + "'");
            if (info.UserPwd == ET.ToolKit.Encrypt.EncrypeHelper.EncryptMD5(pwdold))
                return "true";
            else
                return "false";
        }

        public string LoginDomainUser(string username, string pwd, string domain)
        {
            username = StringHelper.ClearSqlDangerous(username);
            pwd = StringHelper.ClearSqlDangerous(pwd);

            UserLoginForDomain CheckUserLogin = new UserLoginForDomain();
            if (CheckUserLogin.TryAuthenticate(username, domain, pwd))
            {
                UserBaseInfo userinfo = new ET.Sys_BLL.OrganizationBLL().Get_UserBaseInfo(" AND  UserName='" + username + "'");
                if (userinfo != null)
                {
                    return checkADuser(userinfo);
                }
                else
                {
                    User_Full_Info info = new User_Full_Info();
                    UserBaseInfo baseinfo = new UserBaseInfo();
                    UserPropertyInfo stuinfo = new UserPropertyInfo();

                    info.userbaseinfo = baseinfo;
                    info.userstuinfo = stuinfo;
                    info.userstuinfo.CreateTime = DateTime.Now;
                    info.userbaseinfo.UserName = username;
                    info.userbaseinfo.UserPwd = ET.ToolKit.Encrypt.EncrypeHelper.EncryptMD5(pwd);

                    info.userstuinfo.Nickname = username;
                    info.userstuinfo.CNName = username;
                    info.userstuinfo.ENName = username;
                    info.userstuinfo.Sex = "保密";

                    UserDepartmentInfo DEPinfo = new ET.Sys_BLL.OrganizationBLL().Get_UserDepartmentInfo(" AND   DEPName='" + domain + "'");
                    if (DEPinfo == null)
                    {
                        DEPinfo = new UserDepartmentInfo();
                        DEPinfo.DepName = domain;
                        DEPinfo.DepSort = "A000";
                        DEPinfo.DepDescription = "";
                        DEPinfo.DepPID = " -1";
                        new ET.Sys_BLL.OrganizationBLL().Operate_UserDepartmentInfo(DEPinfo, true);
                    }

                    UserPositionInfo Pinfo = new ET.Sys_BLL.OrganizationBLL().Get_UserPositionInfo(" AND   PostName='" + domain + "'");
                    if (Pinfo == null)
                    {
                        Pinfo = new UserPositionInfo();
                        Pinfo.PostName = domain;
                        Pinfo.PostSort = "A000";
                        Pinfo.PostDescription = "";
                        new ET.Sys_BLL.OrganizationBLL().Operate_UserPositionInfo(Pinfo, true);
                    }


                    info.userrole = new List<UserRoleLink>();
                    UserRoleLink role = new UserRoleLink();
                    info.userrole.Add(role);
                    if (new ET.Sys_BLL.OrganizationBLL().Operate_User_Info(info, true))
                        return checkADuser(baseinfo);
                    else
                        return "false";
                }
            }
            else
                return "false";
        }

        public string checkADuser(UserBaseInfo info)
        {
            if (info != null)
            {
                string sKey = info.UserID.ToString();
                string onlineUser = Convert.ToString(HttpContext.Current.Cache[sKey]);
                TimeSpan SessTimeOut = new TimeSpan(0, 0, System.Web.HttpContext.Current.Session.Timeout, 0, 0);
                HttpContext.Current.Cache.Insert(sKey, sKey, null, DateTime.MaxValue, SessTimeOut, System.Web.Caching.CacheItemPriority.NotRemovable, null);

                CurrentUserInfo userinfo = new CurrentUserInfo();
                userinfo.UserID = info.UserID;
                userinfo.UserName = info.UserName;
                UserPropertyInfo stuinfo = new ET.Sys_BLL.OrganizationBLL().Get_UserPropertyInfo(" AND UserID='" + info.UserID.ToString() + "'");
                userinfo.UserCNName = stuinfo.CNName;
                //userinfo.Dep_id = stuinfo.Dep_id;
                //userinfo.P_id = stuinfo.P_id;
                System.Web.HttpContext.Current.Session[SystemConfigConst.SessionUserInfo] = userinfo;
                return "true";
            }
            else
                return "false";
        }




    }
    public class UserLoginForDomain
    {
        //【用户登录域】方法#region【用户登录域】方法
        public const int LOGON32_LOGON_INTERACTIVE = 2;
        public const int LOGON32_PROVIDER_DEFAULT = 0;
        WindowsImpersonationContext impersonationContext;
        [DllImport("advapi32.dll", CharSet = CharSet.Auto)]
        public static extern int LogonUser(String lpszUserName, String lpszDomain, String lpszPassword, int dwLogonType, int dwLogonProvider, ref IntPtr phToken);
        [DllImport("advapi32.dll", CharSet = System.Runtime.InteropServices.CharSet.Auto, SetLastError = true)]
        public extern static int DuplicateToken(IntPtr hToken, int impersonationLevel, ref IntPtr hNewToken);
        /**/
        /// <summary>
        /// 输入用户名、密码、登录域判断是否成功
        /// </summary>
        /// <example>
        /// if (impersonateValidUser(UserName, Domain, Password)){}
        /// </example>
        /// <param name="userName"> 账户名称</param>
        /// <param name="domain"> 要登录的域</param>
        /// <param name="password"> 账户密码</param>
        /// <returns> 成功返回true,否则返回 false</returns>
        public bool impersonateValidUser(String userName, String domain, String password)
        {
            WindowsIdentity tempWindowsIdentity;
            IntPtr token = IntPtr.Zero;
            IntPtr tokenDuplicate = IntPtr.Zero;

            if (LogonUser(userName, domain, password, LOGON32_LOGON_INTERACTIVE, LOGON32_PROVIDER_DEFAULT, ref token) != 0)
            {
                if (DuplicateToken(token, 2, ref tokenDuplicate) != 0)
                {
                    tempWindowsIdentity = new WindowsIdentity(tokenDuplicate);
                    impersonationContext = tempWindowsIdentity.Impersonate();
                    if (impersonationContext != null)
                        return true;
                    else
                        return false;
                }
                else
                    return false;
            }
            else
                return false;
        }
        public bool TryAuthenticate(string userName, string domain, string password)
        {
            bool isLogin = false;
            try
            {
                DirectoryEntry entry = new DirectoryEntry(string.Format("LDAP://{0}", domain), userName, password);
                entry.RefreshCache();
                isLogin = true;
            }
            catch
            {
                isLogin = false;
            }
            return isLogin;
        }

        public void undoImpersonation()
        {
            impersonationContext.Undo();
        }
    }

}
