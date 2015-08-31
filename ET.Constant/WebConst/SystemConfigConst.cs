using System;
using System.Collections.Generic;
using System.Text;

/// <summary>
/// 当前网站的所有系统配置文件和目录的常量
/// 创建人：West
/// 创建日：2014-11-08
/// </summary>
public class SystemConfigConst
{
    /// <summary>
    /// 保存用户信息的Session名称
    /// </summary>
    public const string SessionUserInfo = "SessionUserInfo";
    /// <summary>
    /// 保存用户ID信息的Cookie名称
    /// </summary>
    public const string ManageCookieUserID = "ManageCookieUserID";
    /// <summary>
    /// 保存用户ID信息的Cookie名称
    /// </summary>
    public const string WebCookieUserID = "WebCookieUserID";
    /// <summary>
    /// 网络的URL
    /// </summary>
    public const string WebSiteUrl = "http://localhost:1001/";
    
    /// <summary>
    /// 当前网站目录
    /// </summary>
    public const string WebSiteDir = "~/";
    public const string HostAddressKey = "HostAddress";

    public const string SessioncheckValiCode = "SessioncheckValiCode";

    /// <summary>
    /// 系统标识配置文件
    /// </summary>
    public const string SystemConfigFile = "UpLoad/SystemConfig/SystemConfig.xml";


    /// <summary>
    /// 登录错误日志目录
    /// </summary>
    public const string SystemLogDir = "UpLoad/Log/system/";

    /// <summary>
    /// 管理端登录日志文件目录
    /// </summary>
    public const string ManageLoginLogDir = "UpLoad/Log/managelogin/";
    /// <summary>
    /// 前端登录日志文件目录
    /// </summary>
    public const string WebLoginLogDir = "UpLoad/Log/weblogin/";
    /// <summary>
    /// 微信日志文件目录
    /// </summary>
    public const string WeiXinLogDir = "UpLoad/Log/weixin/";

    ///// <summary>
    ///// 系统图片文件目录
    ///// </summary>
    //public const string SystemImageDir = "UpLoad/SystemImage/";

    /// <summary>
    /// 用户头像文件目录
    /// </summary>
    public const string UserPictureDir = "UpLoad/SystemImage/userpic/";

    /// <summary>
    /// 系统图片文件目录
    /// </summary>
    public const string SystemImageDir = "UpLoad/SystemImage/picture/";

    /// <summary>
    /// 后台富文本编辑器工作目录
    /// </summary>
    public const string EditorFilesDir = "UpLoad/EditorFiles/";

    /// <summary>
    /// 后台富文本编辑器上传文件的格式
    /// </summary>
    public const string EditorUploadFileExts = "txt,rar,zip,jpg,jpeg,gif,png,swf,flv,mp4,wma,mp3,mid";


    
    /// <summary>
    /// 防作弊端地址
    /// </summary>
    public const string ExamIEFile = "ExamIE.rar";

    /// <summary>
    /// .NET FramWork下载地址端地址
    /// </summary>
    public const string DotFrameWorkFile = "dotnetfx.rar";

    /// <summary>
    /// 网站的临时目录
    /// </summary>
    public const string SystemTempDir = "UpLoad/Temp/";


}






