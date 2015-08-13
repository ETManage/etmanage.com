using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;


    public static class PublicHelper
    {
       public static string GetHostAddress()
       {
           return "http://"+System.Web.HttpContext.Current.Request.Url.Authority;
       }

       public static Boolean IsMobileDevice()
       {

           String[] mobileAgents = { "iphone", "android", "phone", "mobile", "wap", "netfront", "java", "opera mobi", "opera mini", "ucweb", "windows ce", "symbian", "series", "webos", "sony", "blackberry", "dopod", "nokia", "samsung", "palmsource", "xda", "pieplus", "meizu", "midp", "cldc", "motorola", "foma", "docomo", "up.browser", "up.link", "blazer", "helio", "hosin", "huawei", "novarra", "coolpad", "webos", "techfaith", "palmsource", "alcatel", "amoi", "ktouch", "nexian", "ericsson", "philips", "sagem", "wellcom", "bunjalloo", "maui", "smartphone", "iemobile", "spice", "bird", "zte-", "longcos", "pantech", "gionee", "portalmmm", "jig browser", "hiptop", "benq", "haier", "^lct", "320x320", "240x320", "176x220", "w3c ", "acs-", "alav", "alca", "amoi", "audi", "avan", "benq", "bird", "blac", "blaz", "brew", "cell", "cldc", "cmd-", "dang", "doco", "eric", "hipt", "inno", "ipaq", "java", "jigs", "kddi", "keji", "leno", "lg-c", "lg-d", "lg-g", "lge-", "maui", "maxo", "midp", "mits", "mmef", "mobi", "mot-", "moto", "mwbp", "nec-", "newt", "noki", "oper", "palm", "pana", "pant", "phil", "play", "port", "prox", "qwap", "sage", "sams", "sany", "sch-", "sec-", "send", "seri", "sgh-", "shar", "sie-", "siem", "smal", "smar", "sony", "sph-", "symb", "t-mo", "teli", "tim-", "tosh", "tsm-", "upg1", "upsi", "vk-v", "voda", "wap-", "wapa", "wapi", "wapp", "wapr", "webc", "winw", "winw", "xda", "xda-", "Googlebot-Mobile" };
           Boolean isMoblie = false;
           if (System.Web.HttpContext.Current.Request.UserAgent.ToString().ToLower() != null)
           {
               for (int i = 0; i < mobileAgents.Length; i++)
               {
                   if (System.Web.HttpContext.Current.Request.UserAgent.ToString().ToLower().IndexOf(mobileAgents[i]) >= 0)
                   {
                       isMoblie = true;
                       break;
                   }
               }
           }
           if (isMoblie)
           {
               return true;
           }
           else
           {
               return false;
           }
       }

        public static bool IsInternetExplorer()
        {
            return System.Web.HttpContext.Current.Request.UserAgent.Contains("MSIE") || System.Web.HttpContext.Current.Request.UserAgent.Contains("Trident");
        }


        
       public static string WebLoginUrl="/login";
       public static string ManageLoginUrl = "/manage/login";
    }

