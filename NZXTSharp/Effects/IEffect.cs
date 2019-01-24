using System;
using System.Collections.Generic;
using System.Text;

using NZXTSharp.Devices;

namespace NZXTSharp.Effects {
    public interface IEffect {

        int EffectByte { get; }

        string EffectName { get; }

        Channel Channel { get; set; }

        List<byte[]> BuildBytes(Channel Channel);

        bool IsCompatibleWith(string Device);
        
    }
}
