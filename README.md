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



```c#
//响应文字消息，回复文字消息  

```


> 注册回复事件消息


### 注册事件

```c#


```


### 回复消息
```c#

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

* 菜单 button 的格式完全遵循微信文档中的格式

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


