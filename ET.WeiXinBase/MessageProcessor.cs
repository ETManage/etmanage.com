using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ET.WeixinMpSdk;
using ET.WeixinMpSdk.Domain;
using ET.WeixinMpSdk.Util;
using ET.WeixinMpSdk.Request;
using ET.WeixinMpSdk.Response;
using ET.Sys_DEF;
using ET.Constant.DBConst;

namespace ET.WeiXinBase
{
    ///
    /// 消息处理类
    ///
    public class MessageProcessor : IMessageProcessor
    {

        ///
        /// 处理消息
        ///
        ///消息对象
        ///参数（用于具体业务传递参数用）
        ///是否处理成功
        string WxAccessToken = "";
        string WxAppId = System.Configuration.ConfigurationManager.AppSettings["WxAppId"];
        string WxAppSecret = System.Configuration.ConfigurationManager.AppSettings["WxAppSecret"];
        public string ProcessMessage(ReceiveMessageBase msg, params object[] args)
        {

            switch (msg.MsgType)
            {
                case MsgType.Text:
                    return ProcessTextMessage(msg as TextReceiveMessage, args);
                case MsgType.Image:
                    return ProcessImageMessage(msg as ImageReceiveMessage, args);
                case MsgType.Link:
                    return ProcessLinkMessage(msg as LinkReceiveMessage, args);
                case MsgType.Location:
                    return ProcessLocationMessage(msg as LocationReceiveMessage, args);
                case MsgType.UnKnown:
                    return ProcessNotHandlerMsg(msg, args);
                case MsgType.Video:
                    return ProcessVideoMessage(msg as VideoReceiveMessage, args);
                case MsgType.Voice:
                    return ProcessVoiceMessage(msg as VoiceReceiveMessage, args);
                case MsgType.VoiceResult:
                    return ProcessVoiceMessage(msg as VoiceReceiveMessage, args);
                case MsgType.Event:
                    EventMessage evtMsg = msg as EventMessage;
                    switch (evtMsg.EventType)
                    {
                        case EventType.Click:
                            return ProcessMenuEvent(msg as MenuEventMessage, args);
                        case EventType.Location:
                            return ProcessUploadLocationEvent(msg as UploadLocationEventMessage, args);
                        case EventType.Scan:
                            return ProcessScanEvent(msg as ScanEventMessage, args);
                        case EventType.Subscribe:
                            return ProcessSubscribeEvent(msg as SubscribeEventMessage, args);
                        case EventType.UnKnown:
                            return ProcessNotHandlerMsg(msg, args);
                        case EventType.UnSubscribe:
                            return ProcessUnSubscribeEvent(msg as UnSubscribeEventMessage, args);
                        default:
                            return ProcessNotHandlerMsg(msg, args);
                    }
                default:
                    return ProcessNotHandlerMsg(msg, args);
            }
        }

        /// <summary>
        /// 创建菜单，只能使用非个人性质公众号
        /// </summary>
        public void CreateMenu()
        {
            IMpClient mpClient = new MpClient();
            AccessTokenGetRequest request = new AccessTokenGetRequest()
            {
                AppIdInfo = new AppIdInfo() { AppID = WxAppId, AppSecret = WxAppSecret }
            };
            AccessTokenGetResponse response = mpClient.Execute(request);
            if (response.IsError)
            {
                WriteProcessLog(string.Format("获取令牌环失败.."));
                return;
            }

            Menu menu = new Menu();

            List<Button> button = new List<ET.WeixinMpSdk.Domain.Button>();

            Button subBtn1 = new Button()
            {
                key = "article",
                name = "历史推送",
                sub_button = null,
                type = "click",
                url = "http://etmanage.com/weixin/articlepush/"
            };
            Button subBtn2 = new Button()
            {
                key = "picture",
                name = "文章列表",
                sub_button = null,
                type = "click",
                url = "http://etmanage.com/weixin/articlelist/"
            };
            List<Button> subBtnAll = new List<Button>();
            subBtnAll.Add(subBtn1);
            subBtnAll.Add(subBtn2);

            Button btn = new Button()
            {
                key = "article",
                name = "文章中心",
                url = "httpbig",
                type = "click",
                sub_button = subBtnAll
            };
            button.Add(btn);

            btn = new Button()
            {
                key = "picture",
                name = "图库中心",
                url = "http://etmanage.com/weixin/picture/",
                type = "click"
            };
            button.Add(btn);

            btn = new Button()
            {
                key = "pcpage",
                name = "PC版",
                type = "click",
                url = "http://etmanage.com/blog/"
            };
            button.Add(btn);

            menu.button = button;

            MenuGroup mg = new MenuGroup()
            {
                menu = menu
            };

            string postData = mg.ToJsonString();

            CreateMenuRequest createRequest = new CreateMenuRequest()
            {
                AccessToken = response.AccessToken.AccessToken,
                SendData = postData
            };

            CreateMenuResponse createResponse = mpClient.Execute(createRequest);

            if (createResponse.IsError)
            {
                WriteProcessLog(string.Format("创建菜单失败，错误信息为：" + createResponse.ErrInfo.ErrCode + "-" + createResponse.ErrInfo.ErrMsg));
            }
        }


        ///
        /// 处理文本消息
        ///
        ///消息对象
        ///参数（用于具体业务传递参数用）
        ///是否处理成功
        public string ProcessTextMessage(TextReceiveMessage msg, params object[] args)
        {
            //这里回应1条文本消息，当然您也可以回应其他消息
            //return MessageHandler.GetXmlTextReplyMessage(msg.ToUserName, msg.FromUserName, "您给我发文本，我也给您发文本" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));

            //if (msg.Content.Equals("v", StringComparison.InvariantCultureIgnoreCase))
            //{
            //    IMpClient mpClient = new MpClient();
            //    AccessTokenGetRequest request = new AccessTokenGetRequest()
            //    {
            //        AppIdInfo = new AppIdInfo() { AppID = WxAppId, AppSecret = WxAppSecret }
            //    };
            //    AccessTokenGetResponse response = mpClient.Execute(request);
            //    if (response.IsError)
            //    {
            //        //这里回应1条文本消息，当然您也可以回应其他消息
            //       return MessageHandler.GetXmlTextReplyMessage(msg.ToUserName, msg.FromUserName, "发送视频时,获取AccessToken失败");
            //       
            //    }
            //    else
            //    {
            //        AccessToken = response.AccessToken.AccessToken;
            //        return MessageHandler.GetXmlVideoReplyMessage(AccessToken, msg.ToUserName, msg.FromUserName, "精美视频", "这个是很漂亮的流畅高清视频", AppDomain.CurrentDomain.BaseDirectory + "Upload/temp/Test.mp4");
            //    }
            //    return true;
            //}



            //发送1条图文信息测试看
            List<NewsReplyMessageItem> items = new List<NewsReplyMessageItem>();
            switch (msg.Content)
            {
                case "时间":
                    items.Add(new NewsReplyMessageItem()
                  {
                      Description = "GTD微信快捷平台",
                      Url = "http://etmanage.com/onegtd/index",
                      PicUrl = "http://etmanage.com/Images/Public/nopicture.jpg",
                      Title = "您的访问是易特网改进的动力！"
                  });
                    break;
                case "易特网":
                case "首页":
                case "文章":
                case "关于我们":
                    items.Add(new NewsReplyMessageItem()
                    {
                        Description = "专业IT文章分享基地，您的访问是易特网改进的动力",
                        Url = "http://etmanage.com/",
                        PicUrl = "http://etmanage.com/Images/Public/nopicture.jpg",
                        Title = "您的访问是易特网改进的动力！"
                    });

                    break;
                default:
                    string strCondition = "";
                    if (!string.IsNullOrEmpty(msg.Content))
                        strCondition = " AND charindex('" + msg.Content + "', ArticleTitle)>0";
                    List<BlogArticleInfo> listArticle = new ET.Sys_BLL.PublicBLL().GetListByCondition<BlogArticleInfo>(3, "ArticleID,ArticleTitle,ArticleLabel,ArticleDescription,ArticlePicture,CreateTime,AccessCount,LoveCount,ShareCount,ArticleUrl,TypeID,(select count(1) from BlogCommentInfo where BlogCommentInfo.ArticleID=BlogArticleInfo.ArticleID) ArticleSource", TableNames.BlogArticleInfo, "AND Status=1 " + strCondition, "CreateTime DESC");
                    foreach (BlogArticleInfo info in listArticle)
                    {
                        NewsReplyMessageItem infoitm = new NewsReplyMessageItem()
                        {
                            Description = info.ArticleDescription,
                            Url = info.ArticleUrl,
                            PicUrl = PublicHelper.GetHostAddress() + info.ArticlePicture,
                            Title = info.ArticleTitle
                        };
                        items.Add(infoitm);

                    }
                    break;
            }
            //NewsReplyMessageItem itm = new NewsReplyMessageItem()
            //{
            //    Description = "产品经理如何应对：这个功能别人都有了1",
            //    Url = "http://www.yixieshi.com/it/21231.html",
            //    PicUrl = "http://img.yixieshi.com/105Z35N9-0.jpg!680",
            //    Title = "产品经理如何应对：这个功能别人都有了1"
            //};
            //items.Add(itm);

            //itm = new NewsReplyMessageItem()
            //{
            //    Description = "产品经理如何应对：这个功能别人都有了2",
            //    Url = "http://www.yixieshi.com/it/21231.html",
            //    PicUrl = "http://img.yixieshi.com/105Z35N9-0.jpg!680",
            //    Title = "产品经理如何应对：这个功能别人都有了1"
            //};
            //items.Add(itm);

            //itm = new NewsReplyMessageItem()
            //{
            //    Description = "产品经理如何应对：这个功能别人都有了3",
            //    Url = "http://www.yixieshi.com/it/21231.html",
            //    PicUrl = "http://img.yixieshi.com/105Z35N9-0.jpg!680",
            //    Title = "产品经理如何应对：这个功能别人都有了1"
            //};
            //items.Add(itm);

            NewsReplyMessage replyMsg = new NewsReplyMessage()
            {
                CreateTime = Tools.ConvertDateTimeInt(DateTime.Now),
                FromUserName = msg.ToUserName,
                ToUserName = msg.FromUserName,
                Articles = items
            };
            return MessageHandler.GetXmlReplyMessage(replyMsg);

        }

        ///

        /// 处理图片消息
        ///

        /// 消息对象
        /// 参数（用于具体业务传递参数用）
        /// 是否处理成功
        public string ProcessImageMessage(ImageReceiveMessage msg, params object[] args)
        {
            try
            {
                IMpClient mpClient = new MpClient();
                AccessTokenGetRequest request = new AccessTokenGetRequest()
                {
                    AppIdInfo = new AppIdInfo() { AppID = WxAppId, AppSecret = WxAppSecret }
                };
                AccessTokenGetResponse response = mpClient.Execute(request);
                if (response.IsError)
                {
                    //这里回应1条文本消息，当然您也可以回应其他消息

                    return MessageHandler.GetXmlTextReplyMessage(msg.ToUserName, msg.FromUserName, "您发送了语音消息"); ;
                }
                else
                {
                    WxAccessToken = response.AccessToken.AccessToken;
                    //这里回复一个图片，当然您也可以回复其他类型的消息
                    return MessageHandler.GetXmlImageReplyMessage(WxAccessToken, msg.ToUserName, msg.FromUserName, AppDomain.CurrentDomain.BaseDirectory + "Images/Public/nopicture.jpg");
                }
            }
            catch (Exception ex)
            {
                //这里回应1条文本消息，当然您也可以回应其他消息
                return MessageHandler.GetXmlTextReplyMessage(msg.ToUserName, msg.FromUserName, "出错了：" + ex.ToString()); ;
            }
        }

        ///
        /// 处理语音消息
        ///
        /// 消息对象
        /// 参数（用于具体业务传递参数用）
        /// 是否处理成功
        public string ProcessVoiceMessage(VoiceReceiveMessage msg, params object[] args)
        {
            IMpClient mpClient = new MpClient();
            AccessTokenGetRequest request = new AccessTokenGetRequest()
            {
                AppIdInfo = new AppIdInfo() { AppID = WxAppId, AppSecret = WxAppSecret }
            };
            AccessTokenGetResponse response = mpClient.Execute(request);
            if (response.IsError)
            {
                //这里回应1条文本消息，当然您也可以回应其他消息

                return MessageHandler.GetXmlTextReplyMessage(msg.ToUserName, msg.FromUserName, "您发送了语音消息"); ;
            }
            else
            {
                WxAccessToken = response.AccessToken.AccessToken;
                //这里回复1条语音消息，当然您也可以回复其他类型的信息
                return MessageHandler.GetXmlVoiceReplyMessage(WxAccessToken, msg.ToUserName, msg.FromUserName, AppDomain.CurrentDomain.BaseDirectory + "Upload/Temp/李宗盛-山丘.mp3");
            }

        }

        ///
        /// 处理视频消息
        ///
        /// 消息对象
        /// 参数（用于具体业务传递参数用）
        /// 是否处理成功
        public string ProcessVideoMessage(VideoReceiveMessage msg, params object[] args)
        {
            //这里回应1条文本消息，当然您也可以回应其他消息
            return MessageHandler.GetXmlTextReplyMessage(msg.ToUserName, msg.FromUserName, "您发送的视频" + msg.MediaId.ToString() + "-" + msg.ThumbMediaId.ToString());

        }

        ///
        /// 处理地理位置消息
        ///
        /// 消息对象
        /// 参数（用于具体业务传递参数用）
        /// 是否处理成功
        public string ProcessLocationMessage(LocationReceiveMessage msg, params object[] args)
        {
            //这里回应1条文本消息，当然您也可以回应其他消息
            return MessageHandler.GetXmlTextReplyMessage(msg.ToUserName, msg.FromUserName, "您的地里位置为：" + msg.Label + "(" + msg.Location_X + "," + msg.Location_Y + ")");
        }

        ///

        /// 处理链接消息
        ///

        /// 消息对象
        /// 参数（用于具体业务传递参数用）
        /// 是否处理成功
        public string ProcessLinkMessage(LinkReceiveMessage msg, params object[] args)
        {
            //这里回应1条文本消息，当然您也可以回应其他消息
            return MessageHandler.GetXmlTextReplyMessage(msg.ToUserName, msg.FromUserName, "您发送的是连接信息");

        }

        ///

        /// 处理关注事件
        ///

        /// 事件消息
        /// 参数（用于具体业务传递参数用）
        /// 是否处理成功
        public string ProcessSubscribeEvent(SubscribeEventMessage msg, params object[] args)
        {
            //这里回应1条文本消息，当然您也可以回应其他消息
            return MessageHandler.GetXmlTextReplyMessage(msg.ToUserName, msg.FromUserName, "您触发了关注事件，欢迎关注我们的公众号");

        }

        ///

        /// 处理取消关注事件
        ///

        /// 事件消息
        /// 参数（用于具体业务传递参数用）
        /// 是否处理成功
        public string ProcessUnSubscribeEvent(UnSubscribeEventMessage msg, params object[] args)
        {
            //这里回应1条文本消息，当然您也可以回应其他消息
            return MessageHandler.GetXmlTextReplyMessage(msg.ToUserName, msg.FromUserName, "您触发了取消关注事件，欢迎您下次再光临哦");

        }

        ///

        /// 处理扫描二维码关注事件
        ///

        /// 事件消息
        /// 参数（用于具体业务传递参数用）
        /// 是否处理成功
        public string ProcessScanSubscribeEvent(ScanSubscribeEventMessage msg, params object[] args)
        {
            //这里回应1条文本消息，当然您也可以回应其他消息
            return MessageHandler.GetXmlTextReplyMessage(msg.ToUserName, msg.FromUserName, "您扫描了我们的二维码关注我们");

        }

        ///

        /// 处理扫描二维码事件
        ///

        /// 事件消息
        /// 参数（用于具体业务传递参数用）
        /// 是否处理成功
        public string ProcessScanEvent(ScanEventMessage msg, params object[] args)
        {
            //这里回应1条文本消息，当然您也可以回应其他消息
            return MessageHandler.GetXmlTextReplyMessage(msg.ToUserName, msg.FromUserName, "您扫描了我们的二维码");

        }

        ///

        /// 处理上报地理位置事件
        ///

        /// 事件消息
        /// 参数（用于具体业务传递参数用）
        /// 是否处理成功
        public string ProcessUploadLocationEvent(UploadLocationEventMessage msg, params object[] args)
        {
            //这里回应1条文本消息，当然您也可以回应其他消息
            return MessageHandler.GetXmlTextReplyMessage(msg.ToUserName, msg.FromUserName, "您的地里位置为：" + msg.Latitude + "-" + msg.Longitude);

        }

        ///

        /// 处理自定义菜单事件
        ///

        /// 事件消息
        /// 参数（用于具体业务传递参数用）
        /// 是否处理成功
        public string ProcessMenuEvent(MenuEventMessage msg, params object[] args)
        {
            //这里回应1条文本消息，当然您也可以回应其他消息
            return MessageHandler.GetXmlTextReplyMessage(msg.ToUserName, msg.FromUserName, "您触发了自定义事件" + msg.EventKey.ToString());

        }

        ///

        /// 处理未定义处理方法消息
        ///

        /// 消息对象
        /// 参数（用于具体业务传递参数用）
        /// 是否处理成功
        public string ProcessNotHandlerMsg(ReceiveMessageBase msg, params object[] args)
        {
            //这里回应1条文本消息，当然您也可以回应其他消息
            return MessageHandler.GetXmlTextReplyMessage(msg.ToUserName, msg.FromUserName, "您输入的指令不正确，请发送H寻求帮助");

        }


        private void WriteProcessLog(string msg)
        {
            //try
            //{
            //    ET.ToolKit.Common.TxtHelper txtHelper = new ET.ToolKit.Common.TxtHelper();
            //    txtHelper.WriteToFile(System.Web.HttpContext.Current.Request.PhysicalApplicationPath + SystemConfigConst.ManageLoginLogDir + DateTime.Now.ToString("yyyy-MM-dd") + ".log", "用户：" + info.UserName + "(" + stuinfo.CNName + ")" + "登陆");
            //}
            //catch { }
        }
    } // class end
}

