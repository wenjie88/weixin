using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace weixin.Message
{
    public enum EventType
    {
         subscribe,
         unsubscribe,
         CLICK,
         VIEW,
         scancode_push,
         scancode_waitmsg,
         pic_sysphoto,
         pic_photo_or_album,
         pic_weixin,
         location_select,
    }
}
