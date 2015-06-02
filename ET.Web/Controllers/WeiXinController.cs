using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using ET.WeixinMpSdk;
using ET.WeixinMpSdk.Domain;
using ET.WeixinMpSdk.Request;
using ET.WeixinMpSdk.Response;
namespace Web.Controllers
{
    public class WeiXinController : Controller
    {
        //
        // GET: /WeiXin/

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult AjaxWeiXinRequest()
        {

            string WxToken = System.Configuration.ConfigurationManager.AppSettings["WxToken"];
            IMessageProcessor msgProcessor = new ET.WeiXinBase.MessageProcessor();  //处理消息，
            //msgProcessor.CreateMenu();
            if (base.Request.HttpMethod.ToLower() == "post")
            {
                var recMsg = MessageHandler.ConvertMsgToObject(WxToken);  //将消息转换成对象

                if (recMsg == null)
                {
                    return Content("1"); ;
                }


                return Content(msgProcessor.ProcessMessage(recMsg));
            }
            else
            {
                if (MessageHandler.CheckSignature(WxToken))
                {
                    return Content(Request.QueryString["echostr"]);
                }

            }
            return Content("2");
        }
    }
}
