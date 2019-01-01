using System;
using System.Collections.Generic;
using System.Text;

namespace NZXTSharp.Devices.Hue {
    public interface IHueDevice : INZXTDevice {
        
        void ApplyEffect(Channel channel, IEffect effect);

        void ApplyCustom(byte[] Bytes);
    }
}
