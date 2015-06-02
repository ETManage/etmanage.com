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
    }

