using System;
using System.Collections.Generic;
using System.Text;

using NZXTSharp.Effects;

namespace NZXTSharp.Devices {
    public interface IHueDevice : INZXTDevice {
        
        void ApplyEffect(Channel channel, IEffect effect);

        void ApplyCustom(byte[] Bytes);

        void UpdateChannelInfo(Channel Channel);

        Channel Both { get; }
        Channel Channel1 { get; }
        Channel Channel2 { get; }
        List<Channel> Channels { get; }
    }
}
