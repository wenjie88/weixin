# weixin

### 配置微信公众号
 添加全局配置
```c#
string token = "xxxx";
string appid = "xxx";
string secret = "xxx";
Wx.WxConfig(token, appid, secret);
```

### 注册一系列微信回复消息

1.所有回复消息的字段都和微信公众号一模一样。
2.默认情况下，ReplyMessage已经默认赋值MsgType和CreateTime

> 注册回复普通消息

> weixin.WxMessage.RegisterReplyMessage(MsgType onMsgType, Func<ReceiveMessage, ReplyMessage> HowToReply)

**
receiveMsg 为微信服务器发送过来的消息体,包涵所有消息的字段。根据不同的消息类型，有些字段会为空。
MsgType 为接受到的消息类型 包含 text,image,voice,video,shortvideo,location,link 。注意 event 不包含在MsgType内！
ReplyMessage 为回复消息的抽象类。子类暂时有：ReplyImageMessage,ReplyTextMessage,ReplyNewsMessage。
**


```c#
//响应文字消息，回复文字消息  
weixin.WxMessage.RegisterReplyMessage(MsgType.text, (receiveMsg) =>
{
        //do some thing....

        return new ReplyTextMessage
        {
            Content = "你好",
            FromUserName = receiveMsg.ToUserName,
            ToUserName = receiveMsg.FromUserName
        };
});

//响应文字消息，回复图文消息
 weixin.WxMessage.RegisterReplyMessage(MsgType.text, (receiveMsg) =>
{
        //do some thing....

        return new ReplyNewsMessage
        {
            FromUserName = receiveMsg.ToUserName,
            ToUserName = receiveMsg.FromUserName,
            ArticleCount = 2,
            Articles = new List<Articles>
            {
                new Articles
                {
                    Title ="第一条信息",
                    Description ="这是第一条新闻",
                    PicUrl ="http://img.zcool.cn/community/01445b56f1ef176ac7257d207ce87d.jpg@900w_1l_2o_100sh.jpg",
                    Url ="http://www.baidu.com"
                },
                new Articles
                {
                    Title ="第二条信息",
                    Description ="这是第二条新闻",
                    PicUrl = "http://img.zcool.cn/community/01445b56f1ef176ac7257d207ce87d.jpg@900w_1l_2o_100sh.jpg",
                    Url ="http://www.baidu.com"
                }
            }
        };
});
```


> 注册回复事件消息

> weixin.WxMessage.RegisterReplyMessage(EventType onEventType, Func<ReceiveMessage, ReplyMessage> HowToReply)

**
receiveMsg 为微信服务器发送过来的消息体,包涵所有消息的字段。根据不同的消息类型，有些字段会为空。
onEventType 为接受到的消息类型 基本包含所有事件
ReplyMessage 为回复消息的抽象类。子类暂时有：ReplyImageMessage,ReplyTextMessage,ReplyNewsMessage。
**

### 菜单按钮注册事件

```c#
weixin.WxMessage.RegisterReplyMessage(EventType.CLICK, (receiveMsg) =>
{
        //do some thing....
        
        
        return new ReplyTextMessage
        {
            Content = $"你点击了 {receiveMsg.EventKey} 按钮",
            FromUserName = receiveMsg.ToUserName,
            ToUserName = receiveMsg.FromUserName
        };
});

```


### 推送模版消息

```c#
weixin.WxMessage.pushMessage(new PushTemplateMessage
{
        touser = "xxxx",
        template_id = "xxxxx",
        url = "http://www.xxxx.com",
        data = new
        {
            first = new { value = "消费成功 !", color = "#173177" },
            keynote1 = new { value = "一块巧克力", color = "#173177" },
            keynote2 = new { value = "19.80", color = "#e40d0d" },
            keynote3 = new { value = DateTime.Now.ToString("yyyy年MM月dd日 HH:mm:ss"), color = "#173177" },
            remark = new { value = "欢迎再次购买", color = "#173177" }
        }
});
```



### 创建菜单

**
菜单 button 的格式完全遵循微信文档中的格式
**

```c#
weixin.WxMenu.CreatMenu(new MenuButton
{
        button = new List<MenuButton.Button>
        {
            new MenuButton.Button
            {
                name = "菜单一",
                type = "click",
                key = "this_is_one"
            },
            new MenuButton.Button
            {
                name= "多菜单",
                sub_button = new List<MenuButton.Button.subButton>
                {
                    new MenuButton.Button.subButton
                    {
                        name = "子菜单一",
                        type = "click",
                        key = "this_is_sub_One"
                    },
                    new MenuButton.Button.subButton
                    {
                        name = "子菜单二",
                        type = "view",
                        url = "http://www.baidu.com"
                    }
                }
            }
        }
});
```

### 网页授权

```c#
/// <summary>
/// 生成授权url
/// </summary>
/// <param name="url">授权后回调的url</param>
/// <param name="state">授权后 重定向后会带上state参数</param>
/// <returns></returns>
string oauthUrl = weixin.WxWebOauth.CreateOathUrl(string url, string state = "");

//----- -----
//回调的url中 可以获取 code 和 state 
string code = reuqest["code"];
string state = request["state"];

//通过code换取网页授权access_token
//Oauth2 为一个对象，里面包含公众号文档中授权信息的所有字段
var Oauth2 = weixin.WxWebOauth.GetWebAccess_Token(code);

//拉取用户信息
//user 包含公众号文档中用户信息的所有字段
var user = weixin.WxWebOauth.GetWx_user(Oauth2)
```


