# weixin
>配置微信公众号

```c#
string token = "xxxx";
string appid = "xxx";
string secret = "xxx";
Wx.WxConfig(token, appid, secret);
```

>注册一系列微信回复消息
回复消息为一个代理方法
RequestMessage 为微信服务器发送过来的消息体
ResoineMessage 为回复消息
Wx_RegisterReplyMessage.onTextMsssage =  Func<RequestMessage, ResponeMessage>

example 当收到文字消息的时候，回复
```c#
Wx_RegisterReplyMessage.onTextMsssage = message = >
{
        if (message.Content == "1")
        {
            //回复文字消息
            ResponeTextMessage res = new ResponeTextMessage
            {
                ToUserName = message.FromUserName,
                FromUserName = message.ToUserName,
                Content = "我是1"
            };
            return res;
        }
        else if (message.Content == "2")
        {
            //回复图片消息
            ResponeImageMessage res = new ResponeImageMessage
            {
                ToUserName = message.FromUserName,
                FromUserName = message.ToUserName,
                Image = new resImg
                {
                    MediaId = message.MediaId
                }
            };
            return res;
        }
        else if (message.Content == "3")
        {
            //回复图文消息
            ResponeNewsMessage res = new ResponeNewsMessage
            {
                FromUserName = message.ToUserName,
                ToUserName = message.FromUserName,
                ArticleCount = 2,
                Articles = new List<Articles>
                {
                    new Articles{Title="第一条信息",Description="哈哈创建第一条信息",PicUrl="http://img.zcool.cn/community/01445b56f1ef176ac7257d207ce87d.jpg@900w_1l_2o_100sh.jpg",Url="http://www.baidu.com"},
                    new Articles{Title="第一条信息",Description="哈哈创建第一条信息",PicUrl= "http://img.zcool.cn/community/01445b56f1ef176ac7257d207ce87d.jpg@900w_1l_2o_100sh.jpg",Url="http://www.baidu.com"}
                }
            };

            return res;
        }
}
```


>微信回调url中的消息处理
```c#
public ActionResult Index()
{
      string signature = Request["signature"];
      string timestamp = Request["timestamp"];
      string nonce = Request["nonce"];
      string echostr = Request["echostr"];
      
      //验证
      bool isValidate = Wx.validateSignature(signature, timestamp, nonce);
      if (!isValidate) return Content("wrong");

      if (Request.HttpMethod.ToUpper() == "GET")
      {
          return Content(echostr);
      }
      else
      {
          //微信发送消息过来post
          //回复消息,根据消息类型，调用配置了的消息回复内容
          string resXml = Wx_MessageHandler.ReplyMessage(Request.InputStream);
          return Content(resXml);

      }
}
```
