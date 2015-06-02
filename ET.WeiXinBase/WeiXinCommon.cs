using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web.Script.Serialization;
using Weixin.Mp.Sdk;
using Weixin.Mp.Sdk.Domain;
using Weixin.Mp.Sdk.Request;
using Weixin.Mp.Sdk.Response;
using Weixin.Mp.Sdk.Util;
namespace ET.WeiXinBase
{
    public class WeiXinCommon
    {
        #region 微信公众号
        static string appId = "wx0616d1546ef54be4";
        static string appSecret = "66d05f54692a547b36ea031a31045740 ";
        static string accessToken = "AmmYv4FZWWMY6Z4zNH-2E3TlUEP-iYOjR28EGKgYhsZ2kwZPqm7e4qQPP_QOosauuEO2Amb3BXyiboVQmj0cyva-mZU5GRe62T-qhb8fQhQ";
        /// <summary>
        /// 获取AccessToken测试代码
        /// </summary>
        public static void GetAccessTokenTest()
        {
            IMpClient mpClient = new MpClient();
            AccessTokenGetRequest request = new AccessTokenGetRequest()
            {
                AppIdInfo = new AppIdInfo() { AppID = appId, AppSecret = appSecret }
            };
            AccessTokenGetResponse response = mpClient.Execute(request);
            if (response.IsError)
            {
                Console.WriteLine("获取AccessToken发生错误，错误编码为：{0}，错误消息为：{1}", response.ErrInfo.ErrCode, response.ErrInfo.ErrMsg);
            }
            else
            {
                Console.WriteLine("获取到AccessToken，值为：{0}，有效期：{1}秒", response.AccessToken.AccessToken, response.AccessToken.ExpiresIn);
            }
        }

        /// <summary>
        /// 多媒体上传接口测试
        /// </summary>
        public static void MediaUploadTest()
        {
            IMpClient mpClient = new MpClient();
            UploadMediaRequest request = new UploadMediaRequest()
            {
                AccessToken = accessToken, //获取的token
                Type = "image",
                FileName = "D:\\Up.jpg"
            };

            UploadMediaResponse response = mpClient.Execute(request);
            if (response.IsError)
            {
                Console.WriteLine("上传多媒体发生错误，错误编码为：{0}，错误消息为：{1}", response.ErrInfo.ErrCode, response.ErrInfo.ErrMsg);
            }
            else
            {
                Console.WriteLine("上传成功，媒体类型为{0},媒体ID为{1},时间戳为：{2}", response.Type, response.MediaId, response.CreatedAt);
            }
        }

        /// <summary>
        /// 多媒体文件下载接口测试
        /// </summary>
        public static void MediaDownloadTest()
        {
            IMpClient mpClient = new MpClient();
            DownloadMediaRequest request = new DownloadMediaRequest()
            {
                AccessToken = accessToken, //获取的token
                MediaId = "zdpXaz1_16N37z4CY9mMsxbQoUvOCFnKnaFWPWhtKmqqmMqh9Cbwdlw9Q1J0r3jO",
                SaveDir = "D:\\"
            };

            DownloadMediaResponse response = mpClient.Execute(request);
            if (response.IsError)
            {
                Console.WriteLine("下载多媒体发生错误，错误编码为：{0}，错误消息为：{1}", response.ErrInfo.ErrCode, response.ErrInfo.ErrMsg);
            }
            else
            {
                Console.WriteLine("文件下载成功，保存路径为：{0}", response.SaveFileName);
            }
        }

        /// <summary>
        /// 发送客服信息测试
        /// </summary>
        public static void SendCustomMessageTest()
        {
            IMpClient mpClient = new MpClient();
            AccessTokenGetRequest request = new AccessTokenGetRequest()
            {
                AppIdInfo = new AppIdInfo() { AppID = appId, AppSecret = appSecret }
            };
            AccessTokenGetResponse response = mpClient.Execute(request);
            if (response.IsError)
            {
                Console.WriteLine("获取令牌环失败..");
                return;
            }
            TextCustomMessage msg = new TextCustomMessage()
            {
                AccessToken = response.AccessToken.AccessToken,
                Content = "客服消息" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),
                MsgType = "text",
                ToUser = "oSFK4twKCGHhYZazqFMIcAx4GshQ"
            };
            var response2 = MessageHandler.SendCustomMessage(response.AccessToken.AccessToken, msg);
            if (response2.IsError)
            {
                Console.WriteLine("发送失败");
            }
            else
            {
                Console.WriteLine("发送成功");
            }
        }

        /// <summary>
        /// 发送文本客服信息测试
        /// </summary>
        public static void SendTextCustomMessageTest()
        {
            IMpClient mpClient = new MpClient();
            AccessTokenGetRequest request = new AccessTokenGetRequest()
            {
                AppIdInfo = new AppIdInfo() { AppID = appId, AppSecret = appSecret }
            };
            AccessTokenGetResponse response = mpClient.Execute(request);
            if (response.IsError)
            {
                Console.WriteLine("获取令牌环失败..");
                return;
            }
            var response2 = MessageHandler.SendTextCustomMessage(response.AccessToken.AccessToken, "oSFK4twKCGHhYZazqFMIcAx4GshQ", "文本客服消息" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
            if (response2.IsError)
            {
                Console.WriteLine("发送失败");
            }
            else
            {
                Console.WriteLine("发送成功");
            }
        }

        /// <summary>
        /// 发送图片客服信息测试
        /// </summary>
        public static void SendImageCustomMessageTest()
        {
            IMpClient mpClient = new MpClient();
            AccessTokenGetRequest request = new AccessTokenGetRequest()
            {
                AppIdInfo = new AppIdInfo() { AppID = appId, AppSecret = appSecret }
            };
            AccessTokenGetResponse response = mpClient.Execute(request);
            if (response.IsError)
            {
                Console.WriteLine("获取令牌环失败..");
                return;
            }
            var response2 = MessageHandler.SendImageCustomMessage(response.AccessToken.AccessToken, "oSFK4twKCGHhYZazqFMIcAx4GshQ", "d:\\UP.jpg");
            if (response2.IsError)
            {
                Console.WriteLine("发送失败");
            }
            else
            {
                Console.WriteLine("发送成功");
            }
        }

        /// <summary>
        /// 发送语音客服信息测试
        /// </summary>
        public static void SendVoiceCustomMessageTest()
        {
            IMpClient mpClient = new MpClient();
            AccessTokenGetRequest request = new AccessTokenGetRequest()
            {
                AppIdInfo = new AppIdInfo() { AppID = appId, AppSecret = appSecret }
            };
            AccessTokenGetResponse response = mpClient.Execute(request);
            if (response.IsError)
            {
                Console.WriteLine("获取令牌环失败..");
                return;
            }
            var response2 = MessageHandler.SendVoiceCustomMessage(response.AccessToken.AccessToken, "oSFK4twKCGHhYZazqFMIcAx4GshQ", "d:\\11.mp3");
            if (response2.IsError)
            {
                Console.WriteLine("发送失败");
            }
            else
            {
                Console.WriteLine("发送成功");
            }
        }

        /// <summary>
        /// 发送视频客服信息
        /// </summary>
        public static void SendVideoCustomMessageTest()
        {
            IMpClient mpClient = new MpClient();
            AccessTokenGetRequest request = new AccessTokenGetRequest()
            {
                AppIdInfo = new AppIdInfo() { AppID = appId, AppSecret = appSecret }
            };
            AccessTokenGetResponse response = mpClient.Execute(request);
            if (response.IsError)
            {
                Console.WriteLine("获取令牌环失败..");
                return;
            }
            var response2 = MessageHandler.SendVideoCustomMessage(response.AccessToken.AccessToken, "oSFK4twKCGHhYZazqFMIcAx4GshQ", "美好的视屏", "视频描述", "d:\\2771cae7-8756-4dd4-8491-976589ab17cc.mp4");

            response2 = MessageHandler.SendVideoCustomMessage(response.AccessToken.AccessToken, "oSFK4twW7yL_vR0ya3jYpT-H19lY", "视频客服信息", "视频客服信息描述", "d:\\2771cae7-8756-4dd4-8491-976589ab17cc.mp4");



            if (response2.IsError)
            {
                Console.WriteLine("发送失败");
            }
            else
            {
                Console.WriteLine("发送成功");
            }
        }

        /// <summary>
        /// 发送音乐客服消息测试
        /// </summary>
        public static void SendMusicCustomMessageTest()
        {
            IMpClient mpClient = new MpClient();
            AccessTokenGetRequest request = new AccessTokenGetRequest()
            {
                AppIdInfo = new AppIdInfo() { AppID = appId, AppSecret = appSecret }
            };
            AccessTokenGetResponse response = mpClient.Execute(request);
            if (response.IsError)
            {
                Console.WriteLine("获取令牌环失败..");
                return;
            }
            //var response2 = MessageHandler.SendMusicCustomMessage(response.AccessToken.AccessToken, "oSFK4twKCGHhYZazqFMIcAx4GshQ", "音乐标题", "音乐描述","","" "d:\\UP.jpg");

            var response2 = MessageHandler.SendMusicCustomMessage(response.AccessToken.AccessToken, "oSFK4twKCGHhYZazqFMIcAx4GshQ", "音乐标题", "音乐描述", "http://wx.011011.com/baba.mp3", "http://wx.011011.com/baba.mp3", "d:\\UP.jpg");

            response2 = MessageHandler.SendMusicCustomMessage(response.AccessToken.AccessToken, "oSFK4twW7yL_vR0ya3jYpT-H19lY", "音乐标题", "音乐描述", "http://wx.011011.com/baba.mp3", "http://wx.011011.com/baba.mp3", "d:\\UP.jpg");





            if (response2.IsError)
            {
                Console.WriteLine("发送失败");
            }
            else
            {
                Console.WriteLine("发送成功");
            }
        }

        /// <summary>
        /// 发送图文客服信息
        /// </summary>
        /// <param name="accessToken"></param>
        /// <param name="msg"></param>
        public static void SendNewsCustomMessageTest()
        {
            IMpClient mpClient = new MpClient();
            AccessTokenGetRequest request = new AccessTokenGetRequest()
            {
                AppIdInfo = new AppIdInfo() { AppID = appId, AppSecret = appSecret }
            };
            AccessTokenGetResponse response = mpClient.Execute(request);
            if (response.IsError)
            {
                Console.WriteLine("获取令牌环失败..");
                return;
            }


            List<NewsCustomMessageItem> items = new List<NewsCustomMessageItem>();
            NewsCustomMessageItem itm;

            itm = new NewsCustomMessageItem()
            {
                Description = "汽车描述1-客服信息",
                Url = "http://www.60px.com",
                PicUrl = "http://t10.baidu.com/it/u=3676282639,2721194652&fm=23&gp=0.jpg",
                Title = "汽车标题1"
            };
            items.Add(itm);

            itm = new NewsCustomMessageItem()
            {
                Description = "汽车描述2",
                Url = "http://www.011011.com",
                PicUrl = "http://t2.baidu.com/it/u=308326304,3930874379&fm=21&gp=0.jpg",
                Title = "汽车标题2"
            };
            items.Add(itm);

            itm = new NewsCustomMessageItem()
            {
                Description = "汽车描述3",
                Url = "http://www.gd-fzc.com",
                PicUrl = "http://t2.baidu.com/it/u=2618819304,4153638089&fm=21&gp=0.jpg",
                Title = "汽车标题3"
            };
            items.Add(itm);


            NewsCustomMessage msg = new NewsCustomMessage()
            {
                Articles = items,
                AccessToken = response.AccessToken.AccessToken,
                MsgType = "news",
                ToUser = "oSFK4twKCGHhYZazqFMIcAx4GshQ"
            };
            var response2 = MessageHandler.SendNewsCustomMessage(response.AccessToken.AccessToken, msg);

            msg.ToUser = "oSFK4twW7yL_vR0ya3jYpT-H19lY";
            response2 = MessageHandler.SendNewsCustomMessage(response.AccessToken.AccessToken, msg);

            if (response2.IsError)
            {
                Console.WriteLine("发送失败");
            }
            else
            {
                Console.WriteLine("发送成功");
            }
            //string accessToken,NewsCustomMessage msg
        }

        /// <summary>
        /// 创建菜单测试
        /// </summary>
        public static void CreateMenuTest()
        {
            IMpClient mpClient = new MpClient();
            AccessTokenGetRequest request = new AccessTokenGetRequest()
            {
                AppIdInfo = new AppIdInfo() { AppID = appId, AppSecret = appSecret }
            };
            AccessTokenGetResponse response = mpClient.Execute(request);
            if (response.IsError)
            {
                File.AppendAllText(System.Web.HttpContext.Current.Request.PhysicalApplicationPath + "/UpLoad/Log/weixin/" + DateTime.Now.ToString("yyyy-MM-dd") + ".log", "获取令牌环失败..");
                return;
            }

            Menu menu = new Menu();

            List<Button> button = new List<Weixin.Mp.Sdk.Domain.Button>();

            Button subBtn1 = new Button()
            {
                key = "blog",
                name = "博客中心",
                sub_button = null,
                type = "click",
                url = "http://etmanage.com/"
            };
            Button subBtn2 = new Button()
            {
                key = "subkey1",
                name = "子按钮1",
                sub_button = null,
                type = "click",
                url = "http://"
            };
            Button subBtn3 = new Button()
            {
                key = "subkey1",
                name = "子按钮1",
                sub_button = null,
                type = "click",
                url = "http://"
            };
            List<Button> subBtnAll = new List<Button>();
            subBtnAll.Add(subBtn1);
            subBtnAll.Add(subBtn2);
            subBtnAll.Add(subBtn3);

            Button btn = new Button()
            {
                key = "key3",
                name = "联系",
                url = "httpbig",
                type = "click",
                sub_button = subBtnAll
            };
            button.Add(btn);

            btn = new Button()
            {
                key = "key1",
                name = "帮助",
                url = "httpbig",
                type = "click",
                sub_button = subBtnAll
            };
            button.Add(btn);

            btn = new Button()
            {
                key = "key2",
                name = "关于",
                url = "httpbig",
                type = "click",
                //sub_button = subBtnAll
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
                Console.WriteLine("创建菜单失败，错误信息为：" + createResponse.ErrInfo.ErrCode + "-" + createResponse.ErrInfo.ErrMsg);
            }
            else
            {
                Console.WriteLine("创建菜单成功");
            }
        }

        /// <summary>
        /// 查询菜单测试
        /// </summary>
        public static void GetMenuTest()
        {
            IMpClient mpClient = new MpClient();
            AccessTokenGetRequest request = new AccessTokenGetRequest()
            {
                AppIdInfo = new AppIdInfo() { AppID = appId, AppSecret = appSecret }
            };
            AccessTokenGetResponse response = mpClient.Execute(request);
            if (response.IsError)
            {
                Console.WriteLine("获取令牌环失败..");
                return;
            }

            GetMenuRequest getRequest = new GetMenuRequest()
            {
                AccessToken = response.AccessToken.AccessToken
            };

            var getResponse = mpClient.Execute(getRequest);
            if (getResponse.IsError)
            {
                Console.WriteLine("查询菜单发生错误，错误信息为：" + getResponse.ErrInfo.ErrCode + "-" + getResponse.ErrInfo.ErrMsg);
            }
            else
            {
                Console.WriteLine("查询菜单成功");
                var m = getResponse.Menu;
                Console.WriteLine(m.ToJsonString());
            }
        }

        /// <summary>
        /// 删除菜单测试
        /// </summary>
        public static void DeleteMenuTest()
        {
            IMpClient mpClient = new MpClient();
            AccessTokenGetRequest request = new AccessTokenGetRequest()
            {
                AppIdInfo = new AppIdInfo() { AppID = appId, AppSecret = appSecret }
            };
            AccessTokenGetResponse response = mpClient.Execute(request);
            if (response.IsError)
            {
                Console.WriteLine("获取令牌环失败..");
                return;
            }

            DeleteMenuRequest getRequest = new DeleteMenuRequest()
            {
                AccessToken = response.AccessToken.AccessToken
            };

            var delResponse = mpClient.Execute(getRequest);
            if (delResponse.IsError)
            {
                Console.WriteLine("删除菜单失败，错误信息为：" + delResponse.ErrInfo.ErrCode + "-" + delResponse.ErrInfo.ErrMsg);
            }
            else
            {
                Console.WriteLine("删除菜单成功");

                GetMenuTest(); //成功后查询一下
            }
        }

        /// <summary>
        /// 创建二维码测试
        /// </summary>
        public static void QrCodeCreateTest()
        {
            IMpClient mpClient = new MpClient();
            AccessTokenGetRequest request = new AccessTokenGetRequest()
            {
                AppIdInfo = new AppIdInfo() { AppID = appId, AppSecret = appSecret }
            };
            AccessTokenGetResponse response = mpClient.Execute(request);
            if (response.IsError)
            {
                Console.WriteLine("获取令牌环失败..");
                return;
            }

            QrCodeCreateMessage msg = new QrCodeCreateMessage()
            {
                SceneId = 22,
                ExpireSeconds = 1800,
                ActionName = "QR_LIMIT_SCENE",   //QR_SCENE为临时,QR_LIMIT_SCENE为永久
                LocalStoredDir = "D:\\"
            };

            QrCodeCreateRequest createRequest = new QrCodeCreateRequest()
            {
                AccessToken = response.AccessToken.AccessToken,
                QrCodeCreateMessage = msg,
                SendData = msg.ToJsonString()
            };

            var createResponse = mpClient.Execute(createRequest);
            if (createResponse.IsError)
            {
                Console.WriteLine("创建二维码失败，错误信息为：" + createResponse.ErrInfo.ErrCode + "-" + createResponse.ErrInfo.ErrMsg);
            }
            else
            {
                Console.WriteLine("创建二维码成功，二维码链接地址为：" + createResponse.QrCodeUrl + "本地存储路径为：" + createResponse.LocalFilePath);
            }
        }

        /// <summary>
        /// 创建分组测试
        /// </summary>
        public static void CreateGroupTest()
        {
            IMpClient mpClient = new MpClient();
            AccessTokenGetRequest request = new AccessTokenGetRequest()
            {
                AppIdInfo = new AppIdInfo() { AppID = appId, AppSecret = appSecret }
            };
            AccessTokenGetResponse response = mpClient.Execute(request);
            if (response.IsError)
            {
                Console.WriteLine("获取令牌环失败..");
                return;
            }

            Group msg = new Group()
            {
                name = "分组一"
            };

            CreateGroupRequest createRequest = new CreateGroupRequest()
            {
                AccessToken = response.AccessToken.AccessToken,
                SendData = msg.ToCreateJsonString()
            };

            var createResponse = mpClient.Execute(createRequest);
            if (createResponse.IsError)
            {
                Console.WriteLine("创建分组失败，错误信息为：" + createResponse.ErrInfo.ErrCode + "-" + createResponse.ErrInfo.ErrMsg);
            }
            else
            {
                Console.WriteLine("创建分组成功，分组ID为：" + createResponse.GroupInfo.id + "，分组名称为：" + createResponse.GroupInfo.name);
            }
        }

        /// <summary>
        /// 查询所有分组测试
        /// </summary>
        public static void GetGroupsTest()
        {
            IMpClient mpClient = new MpClient();
            AccessTokenGetRequest request = new AccessTokenGetRequest()
            {
                AppIdInfo = new AppIdInfo() { AppID = appId, AppSecret = appSecret }
            };
            AccessTokenGetResponse response = mpClient.Execute(request);
            if (response.IsError)
            {
                Console.WriteLine("获取令牌环失败..");
                return;
            }

            GetGroupsRequest createRequest = new GetGroupsRequest()
            {
                AccessToken = response.AccessToken.AccessToken,
            };

            var createResponse = mpClient.Execute(createRequest);
            if (createResponse.IsError)
            {
                Console.WriteLine("查询所有分组失败，错误信息为：" + createResponse.ErrInfo.ErrCode + "-" + createResponse.ErrInfo.ErrMsg);
            }
            else
            {
                Console.WriteLine("查询分组信息成功，共有分组" + createResponse.Groups.groups.Count.ToString() + "个");
                JavaScriptSerializer jsonSerializer = new JavaScriptSerializer();
                string groups = jsonSerializer.Serialize(createResponse.Groups);
                Console.WriteLine(groups);
            }
        }

        /// <summary>
        /// 获取用户分组ID测试
        /// </summary>
        public static void GetUserGroupIdTest()
        {
            IMpClient mpClient = new MpClient();
            AccessTokenGetRequest request = new AccessTokenGetRequest()
            {
                AppIdInfo = new AppIdInfo() { AppID = appId, AppSecret = appSecret }
            };
            AccessTokenGetResponse response = mpClient.Execute(request);
            if (response.IsError)
            {
                Console.WriteLine("获取令牌环失败..");
                return;
            }

            GetUserGroupRequest request2 = new GetUserGroupRequest()
            {
                AccessToken = response.AccessToken.AccessToken,
                UserId = "oSFK4t4xIdeddd4wpxXaUmtKKpMA"
            };

            var response2 = mpClient.Execute(request2);
            if (response2.IsError)
            {
                Console.WriteLine("查询用户分组ID失败，错误信息为：" + response2.ErrInfo.ErrCode + "-" + response2.ErrInfo.ErrMsg);
            }
            else
            {
                Console.WriteLine("查询用户分组ID成功，分组ID为：" + response2.GroupId);
            }
        }

        /// <summary>
        /// 修改分组测试
        /// </summary>
        public static void ModifyGroupTest()
        {
            IMpClient mpClient = new MpClient();
            AccessTokenGetRequest request = new AccessTokenGetRequest()
            {
                AppIdInfo = new AppIdInfo() { AppID = appId, AppSecret = appSecret }
            };
            AccessTokenGetResponse response = mpClient.Execute(request);
            if (response.IsError)
            {
                Console.WriteLine("获取令牌环失败..");
                return;
            }

            Group groupInfo = new Group()
            {
                id = "101",
                name = "修改后的分组"
            };

            ModifyGroupRequest request2 = new ModifyGroupRequest()
            {
                AccessToken = response.AccessToken.AccessToken,
                GroupInfo = groupInfo,
                SendData = groupInfo.ToModifyJsonString()
            };

            var response2 = mpClient.Execute(request2);
            if (response2.IsError)
            {
                Console.WriteLine("修改分组失败，错误信息为：" + response2.ErrInfo.ErrCode + "-" + response2.ErrInfo.ErrMsg);
            }
            else
            {
                Console.WriteLine("修改分组信息成功");
                GetGroupsTest(); //查询一把，显示一下信息
            }
        }

        /// <summary>
        /// 移动用户分组测试
        /// </summary>
        public static void SetUserGroupTest()
        {
            IMpClient mpClient = new MpClient();
            AccessTokenGetRequest request = new AccessTokenGetRequest()
            {
                AppIdInfo = new AppIdInfo() { AppID = appId, AppSecret = appSecret }
            };
            AccessTokenGetResponse response = mpClient.Execute(request);
            if (response.IsError)
            {
                Console.WriteLine("获取令牌环失败..");
                return;
            }

            SetUserGroupRequest request2 = new SetUserGroupRequest()
            {
                AccessToken = response.AccessToken.AccessToken,
                UserId = "oSFK4t4xIdeddd4wpxXaUmtKKpMA",
                ToGroupId = "101"
            };

            var response2 = mpClient.Execute(request2);
            if (response2.IsError)
            {
                Console.WriteLine("移动用户分组失败，错误信息为：" + response2.ErrInfo.ErrCode + "-" + response2.ErrInfo.ErrMsg);
            }
            else
            {
                Console.WriteLine("移动用户分组成功");
                GetGroupsTest(); //查询一把，显示一下信息
            }
        }

        /// <summary>
        /// 获取用户基本信息测试
        /// </summary>
        public static void GetUserInfoTest()
        {
            IMpClient mpClient = new MpClient();
            AccessTokenGetRequest request = new AccessTokenGetRequest()
            {
                AppIdInfo = new AppIdInfo() { AppID = appId, AppSecret = appSecret }
            };
            AccessTokenGetResponse response = mpClient.Execute(request);
            if (response.IsError)
            {
                Console.WriteLine("获取令牌环失败..");
                return;
            }

            GetUserInfoRequest request2 = new GetUserInfoRequest()
            {
                AccessToken = response.AccessToken.AccessToken,
                OpenId = "oSFK4t4xIdeddd4wpxXaUmtKKpMA",
            };

            var response2 = mpClient.Execute(request2);
            if (response2.IsError)
            {
                Console.WriteLine("获取用户基本信息失败，错误信息为：" + response2.ErrInfo.ErrCode + "-" + response2.ErrInfo.ErrMsg);
            }
            else
            {
                Console.WriteLine("获取用户信息成功");
                Console.WriteLine(Tools.ToJsonString(response2.UserInfo));
            }
        }

        /// <summary>
        /// 获取关注者列表测试
        /// </summary>
        public static void GetAttentionsTest()
        {
            IMpClient mpClient = new MpClient();
            AccessTokenGetRequest request = new AccessTokenGetRequest()
            {
                AppIdInfo = new AppIdInfo() { AppID = appId, AppSecret = appSecret }
            };
            AccessTokenGetResponse response = mpClient.Execute(request);
            if (response.IsError)
            {
                Console.WriteLine("获取令牌环失败..");
                return;
            }

            GetAttentionsRequest request2 = new GetAttentionsRequest()
            {
                AccessToken = response.AccessToken.AccessToken
            };

            var response2 = mpClient.Execute(request2);
            if (response2.IsError)
            {
                Console.WriteLine("获取关注者列表失败，错误信息为：" + response2.ErrInfo.ErrCode + "-" + response2.ErrInfo.ErrMsg);
            }
            else
            {
                Console.WriteLine("获取关注者列表成功");
                Console.WriteLine(Tools.ToJsonString(response2.Attentions));
            }
        }
        #endregion
    }
}
