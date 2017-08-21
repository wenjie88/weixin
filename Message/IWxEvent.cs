using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using weixin.Message.Model;

namespace weixin.Message
{
    interface IWxEvent
    {
        ReplyBaseMessage On_DefaultEvent(ReceiveMessage ReceiveMessage);
       
        ReplyBaseMessage On_TextEvent(ReceiveMessage ReceiveMessage);

        ReplyBaseMessage On_ImageEvent(ReceiveMessage ReceiveMessage);

        ReplyBaseMessage On_VoiceEvent(ReceiveMessage ReceiveMessage);

        ReplyBaseMessage On_VideoEvent(ReceiveMessage ReceiveMessage);

        ReplyBaseMessage On_ShortVideoEvent(ReceiveMessage ReceiveMessage);

        ReplyBaseMessage On_LocationEvent(ReceiveMessage ReceiveMessage);

        ReplyBaseMessage On_LinkEvent(ReceiveMessage ReceiveMessage);

        ReplyBaseMessage On_SubscribeEvent(ReceiveMessage ReceiveMessage);


        ReplyBaseMessage On_UnSubscribeEvent(ReceiveMessage ReceiveMessage);

        ReplyBaseMessage On_CLICKEvent(ReceiveMessage ReceiveMessage);

        ReplyBaseMessage On_VIEWEvent(ReceiveMessage ReceiveMessage);

        ReplyBaseMessage On_Scancode_pushEvent(ReceiveMessage ReceiveMessage);

        ReplyBaseMessage On_Scancode_waitmsgEvent(ReceiveMessage ReceiveMessage);

        ReplyBaseMessage On_Pic_sysphotoEvent(ReceiveMessage ReceiveMessage);

        ReplyBaseMessage On_Pic_photo_or_albumEvent(ReceiveMessage ReceiveMessage);

        ReplyBaseMessage On_Pic_weixinEvent(ReceiveMessage ReceiveMessage);

        ReplyBaseMessage On_Location_selectEvent(ReceiveMessage ReceiveMessage);
        
    }
}
