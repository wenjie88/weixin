using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using weixin.Message.Model;

namespace weixin.Message
{
    public abstract class WxMessageBaseHandler : IWxEvent
    {

        public WxMessageBaseHandler()
        {
        }


        

        public ReplyBaseMessage On_DefaultEvent(ReceiveMessage ReceiveMessage)
        {
            return null;
        }

        public virtual ReplyBaseMessage On_TextEvent(ReceiveMessage ReceiveMessage)
        {
            return null;
        }
        public virtual ReplyBaseMessage On_VideoEvent(ReceiveMessage ReceiveMessage)
        {
            return null;
        }
        public virtual ReplyBaseMessage On_VoiceEvent(ReceiveMessage ReceiveMessage)
        {
            return null;
        }


        public virtual ReplyBaseMessage On_ImageEvent(ReceiveMessage ReceiveMessage)
        {
            return null;
        }

        public virtual ReplyBaseMessage On_LinkEvent(ReceiveMessage ReceiveMessage)
        {
            return null;
        }

        public virtual ReplyBaseMessage On_LocationEvent(ReceiveMessage ReceiveMessage)
        {
            return null;
        }

        public virtual ReplyBaseMessage On_Location_selectEvent(ReceiveMessage ReceiveMessage)
        {
            return null;
        }

        public virtual ReplyBaseMessage On_SubscribeEvent(ReceiveMessage ReceiveMessage)
        {
            return null;
        }

        public virtual ReplyBaseMessage On_UnSubscribeEvent(ReceiveMessage ReceiveMessage)
        {
            return null;
        }

        public virtual ReplyBaseMessage On_CLICKEvent(ReceiveMessage ReceiveMessage)
        {
            return null;
        }

        public virtual ReplyBaseMessage On_Pic_photo_or_albumEvent(ReceiveMessage ReceiveMessage)
        {
            return null;
        }


        public virtual ReplyBaseMessage On_Pic_sysphotoEvent(ReceiveMessage ReceiveMessage)
        {
            return null;
        }

        public virtual ReplyBaseMessage On_Pic_weixinEvent(ReceiveMessage ReceiveMessage)
        {
            return null;
        }

        public virtual ReplyBaseMessage On_Scancode_pushEvent(ReceiveMessage ReceiveMessage)
        {
            return null;
        }

        public virtual ReplyBaseMessage On_Scancode_waitmsgEvent(ReceiveMessage ReceiveMessage)
        {
            return null;
        }

        public virtual ReplyBaseMessage On_ShortVideoEvent(ReceiveMessage ReceiveMessage)
        {
            return null;
        }
        public virtual ReplyBaseMessage On_VIEWEvent(ReceiveMessage ReceiveMessage)
        {
            return null;
        }

    }
}
