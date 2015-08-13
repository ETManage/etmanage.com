<%@ WebHandler Language="C#" Class="upload" %>

using System;
using System.Web;
using System.IO;
using System.Net;
using System.Text.RegularExpressions;
public class upload : IHttpHandler
{
    
    public void ProcessRequest (HttpContext context) {
        context.Response.ContentType = "text/plain";
        context.Response.Charset = "UTF-8";

        // 初始化一大堆变量
        string inputname = "filedata";//表单文件域name
        string attachDir = "UpLoad/Blog/EditorFiles/";     // 上传文件保存目录
        int dirtype = 1;                 // 1:按天存入目录 2:按月存入目录 3:按扩展名存目录  建议使用按天存
        int maxattachsize = 209715200;     // 最大上传大小，默认是200M
        string upext =SystemConfigConst.EditorUploadFileExts;    // 上传扩展名
        int msgtype = 2;                 //返回上传参数的格式：1，只返回url，2，返回参数数组
        string immediate = context.Request.QueryString["immediate"];//立即上传模式，仅为演示用
        byte[] file;                     // 统一转换为byte数组处理
        string localname = "";
        string disposition = context.Request.ServerVariables["HTTP_CONTENT_DISPOSITION"];
        string err = "";
        string msg = "''";

        if (disposition != null)
        {
            // HTML5上传
            file = context.Request.BinaryRead(context.Request.TotalBytes);
            localname = context.Server.UrlDecode(Regex.Match(disposition, "filename=\"(.+?)\"").Groups[1].Value);// 读取原始文件名
        }
        else
        {
            HttpFileCollection filecollection = context.Request.Files;
            HttpPostedFile postedfile = filecollection.Get(inputname);
            if (postedfile == null)
                return;
            // 读取原始文件名
            localname = postedfile.FileName;
            // 初始化byte长度.
            file = new Byte[postedfile.ContentLength];

            // 转换为byte类型
            System.IO.Stream stream = postedfile.InputStream;
            stream.Read(file, 0, postedfile.ContentLength);
            stream.Close();

            filecollection = null;
        }

        if (file.Length == 0) err = "无数据提交";
        else
        {
            if (file.Length > maxattachsize) err = "文件大小超过" + maxattachsize + "字节";
            else
            {
                string attach_dir, attach_subdir, filename, extension, target;

                // 取上载文件后缀名
                extension = GetFileExt(localname);

                if (("," + upext + ",").IndexOf("," + extension + ",") < 0) err = "上传文件扩展名必需为：" + upext;
                else
                {
                    switch (dirtype)
                    {
                        case 2:
                            attach_subdir = "month_" + DateTime.Now.ToString("yyMM");
                            break;
                        case 3:
                            attach_subdir = "ext_" + extension;
                            break;
                        default:
                            attach_subdir = "day_" + DateTime.Now.ToString("yyMMdd");
                            break;
                    }
                     
                    

                    // 生成随机文件名
                    Random random = new Random(DateTime.Now.Millisecond);
                    filename = DateTime.Now.ToString("yyyyMMddHHmmssfff") + random.Next(10000) + "." + extension;
                    attach_dir = attachDir + attach_subdir + "/" ;
                    
                    try
                    {
                        CreateFolder(context.Server.MapPath(SystemConfigConst.WebSiteDir+attach_dir));

                        System.IO.FileStream fs = new System.IO.FileStream(context.Server.MapPath(SystemConfigConst.WebSiteDir + attach_dir + filename), System.IO.FileMode.Create, System.IO.FileAccess.Write);
                        fs.Write(file, 0, file.Length);
                        fs.Flush();
                        fs.Close();
                    }
                    catch (Exception ex)
                    {
                        err = ex.Message.ToString();
                    }
                    target = "/" + attach_dir + filename;
                    // 立即模式判断
                    if (immediate == "1") target = "!" + target;
                    target = jsonString(target);
                    if (msgtype == 1) msg = "'" + target + "'";
                    else msg = "{'url':'" + target.ToLower() + "','localname':'" + jsonString(localname) + "','id':'1'}";
                }
            }
        }

        file = null;

        context.Response.Write("{'err':'" + jsonString(err) + "','msg':" + msg + "}");
    }
 
    public bool IsReusable {
        get {
            return false;
        }
    }
    string jsonString(string str)
    {
        str = str.Replace("\\", "\\\\");
        str = str.Replace("/", "\\/");
        str = str.Replace("'", "\\'");
        return str;
    }


    string GetFileExt(string FullPath)
    {
        if (FullPath != "") return FullPath.Substring(FullPath.LastIndexOf('.') + 1).ToLower();
        else return "";
    }

    void CreateFolder(string FolderPath)
    {
        if (!System.IO.Directory.Exists(FolderPath)) System.IO.Directory.CreateDirectory(FolderPath);
    }


}